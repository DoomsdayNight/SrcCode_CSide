using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using AssetBundles;
using Cs.Engine.Util;
using Cs.Logging;
using DG.Tweening;
using NKC.Localization;
using NKC.Patcher;
using NKC.Publisher;
using NKC.UI;
using NKC.UI.Option;
using NKM;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x0200073B RID: 1851
	public class NKCPatcherManager : MonoBehaviour
	{
		// Token: 0x06004A01 RID: 18945 RVA: 0x0016361B File Offset: 0x0016181B
		public static NKCPatcherManager GetPatcherManager()
		{
			return NKCPatcherManager.m_PatcherManager;
		}

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x06004A03 RID: 18947 RVA: 0x0016362B File Offset: 0x0016182B
		// (set) Token: 0x06004A02 RID: 18946 RVA: 0x00163622 File Offset: 0x00161822
		private NKC_PUBLISHER_RESULT_CODE m_eResultCode { get; set; }

		// Token: 0x06004A04 RID: 18948 RVA: 0x00163634 File Offset: 0x00161834
		private void Start()
		{
			NKCUtil.SetGameobjectActive(this.m_NKCLogo, false);
			this.m_NkcPatchChecker.SetActive(false);
			NKCUtil.SetGameobjectActive(this.m_NKCSelectServer, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_WAIT, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_TAG_REQUEST_NOTICE, false);
			this.m_NKC_PATCHER_UI_FRONT_RectTransform = this.m_NKC_PATCHER_UI_FRONT.GetComponent<RectTransform>();
			this.m_NKC_PATCHER_UI_FRONT_Mask = this.m_NKC_PATCHER_UI_FRONT.GetComponent<Mask>();
			this.m_NKC_PATCHER_UI_FRONT_Mask.enabled = true;
			base.StartCoroutine(this.TryPatcherProcess());
		}

		// Token: 0x06004A05 RID: 18949 RVA: 0x001636B8 File Offset: 0x001618B8
		private IEnumerator TryPatcherProcess()
		{
			Coroutine<string> routine = this.StartCoroutine(this.PatcherProcess());
			yield return routine.coroutine;
			try
			{
				UnityEngine.Debug.Log(routine.Value);
				yield break;
			}
			catch (WebException ex)
			{
				if (ex.Status == WebExceptionStatus.ProtocolError)
				{
					string text = string.Format("{0} : {1}", ((HttpWebResponse)ex.Response).StatusCode, ((HttpWebResponse)ex.Response).StatusDescription);
					UnityEngine.Debug.LogError(text);
					this.ShowError(text);
				}
				else
				{
					this.ShowError(ex);
				}
				yield break;
			}
			catch (Exception e)
			{
				this.ShowError(e);
				yield break;
			}
			yield break;
		}

		// Token: 0x06004A06 RID: 18950 RVA: 0x001636C7 File Offset: 0x001618C7
		private IEnumerator PatcherProcess()
		{
			this.SetState(NKCPatcherManager.PATCHER_STATE.Init);
			int pmConnectionErrorCount = 0;
			for (;;)
			{
				switch (this.m_currentPatcherState)
				{
				case NKCPatcherManager.PATCHER_STATE.Init:
					this.Init();
					this.SetState(NKCPatcherManager.PATCHER_STATE.DisplayLogo);
					break;
				case NKCPatcherManager.PATCHER_STATE.DisplayLogo:
					base.StartCoroutine(this.m_NKCLogo.DisplayLogo());
					this.SetState(NKCPatcherManager.PATCHER_STATE.InitPM);
					break;
				case NKCPatcherManager.PATCHER_STATE.InitPM:
					yield return this.PublisherModuleInitProcess();
					if (this.m_eResultCode == NKC_PUBLISHER_RESULT_CODE.NPRC_GAMEBASE_WEBSOCKET_READ_TIMEOUT && pmConnectionErrorCount < 10)
					{
						UnityEngine.Debug.Log(string.Format("[PatcherManager] InitPM Gamebase Timeout Count[{0}]", pmConnectionErrorCount));
						this.m_eResultCode = NKC_PUBLISHER_RESULT_CODE.NPRC_OK;
						this.SetState(NKCPatcherManager.PATCHER_STATE.InitPM);
						int num = pmConnectionErrorCount;
						pmConnectionErrorCount = num + 1;
					}
					else if (this.m_eResultCode != NKC_PUBLISHER_RESULT_CODE.NPRC_OK)
					{
						UnityEngine.Debug.Log("[PatcherManager] InitPM failed.");
						this.SetState(NKCPatcherManager.PATCHER_STATE.WaitStartUpProcess);
					}
					else
					{
						NKCPatcherManager.s_bPubModuleInit = true;
						UnityEngine.Debug.Log("[PatcherManager] InitPM finished.");
						NKCPublisherModule.Statistics.LogClientAction(NKCPublisherModule.NKCPMStatistics.eClientAction.AppStart, 0, null);
						UnityEngine.Debug.Log("[PatcherManager] AppStart start.");
						NKCAdjustManager.OnCustomEvent("01_appLaunch");
						if (NKCDefineManager.DEFINE_DOWNLOAD_CONFIG())
						{
							this.SetState(NKCPatcherManager.PATCHER_STATE.UpdateDownloadConfigProcess);
						}
						else
						{
							this.SetState(NKCPatcherManager.PATCHER_STATE.UpdateServerInfomation);
						}
					}
					break;
				case NKCPatcherManager.PATCHER_STATE.UpdateDownloadConfigProcess:
					yield return this.UpdateDownloadConfigProcess();
					if (this.m_eResultCode != NKC_PUBLISHER_RESULT_CODE.NPRC_OK)
					{
						this.SetState(NKCPatcherManager.PATCHER_STATE.WaitStartUpProcess);
					}
					else
					{
						this.SetState(NKCPatcherManager.PATCHER_STATE.GetTagFromLoginServer);
					}
					break;
				case NKCPatcherManager.PATCHER_STATE.UpdateServerInfomation:
					yield return this.UpdateServerInfomationProcess();
					if (this.m_eResultCode != NKC_PUBLISHER_RESULT_CODE.NPRC_OK || NKCDefineManager.DEFINE_SELECT_SERVER())
					{
						this.SetState(NKCPatcherManager.PATCHER_STATE.WaitStartUpProcess);
					}
					else
					{
						this.SetState(NKCPatcherManager.PATCHER_STATE.GetTagFromLoginServer);
					}
					break;
				case NKCPatcherManager.PATCHER_STATE.GetTagFromLoginServer:
					yield return this.GetServerContentTagProcess();
					this.SetState(NKCPatcherManager.PATCHER_STATE.WaitStartUpProcess);
					break;
				case NKCPatcherManager.PATCHER_STATE.WaitStartUpProcess:
					while (!NKCLogo.s_bLogoPlayed)
					{
						yield return null;
					}
					if (this.m_eResultCode != NKC_PUBLISHER_RESULT_CODE.NPRC_OK)
					{
						this.SetState(NKCPatcherManager.PATCHER_STATE.TryRecoverTag);
					}
					else if (NKCDefineManager.DEFINE_SELECT_SERVER())
					{
						this.SetState(NKCPatcherManager.PATCHER_STATE.SelectServer);
					}
					else
					{
						this.SetState(NKCPatcherManager.PATCHER_STATE.TryRecoverTag);
					}
					break;
				case NKCPatcherManager.PATCHER_STATE.SelectServer:
					if (NKCConnectionInfo.GetLoginServerCount() == 1)
					{
						NKCConnectionInfo.CurrentLoginServerType = NKCConnectionInfo.GetFirstLoginServerType();
					}
					else if (NKCConnectionInfo.LastLoginServerType != NKCConnectionInfo.LOGIN_SERVER_TYPE.None && NKCConnectionInfo.HasLoginServerInfo(NKCConnectionInfo.LastLoginServerType))
					{
						NKCConnectionInfo.CurrentLoginServerType = NKCConnectionInfo.LastLoginServerType;
					}
					else
					{
						yield return this.ProcessSelectServer();
					}
					this.SetState(NKCPatcherManager.PATCHER_STATE.SetDefaultTagFromSelectedServer);
					break;
				case NKCPatcherManager.PATCHER_STATE.SetDefaultTagFromSelectedServer:
					if (NKCConnectionInfo.CurrentLoginServerTagSet != null)
					{
						foreach (string tag in NKCConnectionInfo.CurrentLoginServerTagSet)
						{
							NKMContentsVersionManager.AddTag(tag);
						}
					}
					if (NKCDefineManager.DEFINE_USE_CHEAT())
					{
						NKMContentsVersionManager.PrintCurrentTagSet();
					}
					this.SetState(NKCPatcherManager.PATCHER_STATE.SetNationCode);
					break;
				case NKCPatcherManager.PATCHER_STATE.TryRecoverTag:
					NKCContentsVersionManager.TryRecoverTag();
					this.SetState(NKCPatcherManager.PATCHER_STATE.SetNationCode);
					break;
				case NKCPatcherManager.PATCHER_STATE.SetNationCode:
					yield return this.SetNationCode();
					this.SetState(NKCPatcherManager.PATCHER_STATE.LoadPatcherString);
					break;
				case NKCPatcherManager.PATCHER_STATE.LoadPatcherString:
					NKCPatcherStringList.LoadStrings("LUA_PATCH_STRING", "m_dicString", true);
					this.SetState(NKCPatcherManager.PATCHER_STATE.SelectVoice);
					break;
				case NKCPatcherManager.PATCHER_STATE.SelectVoice:
					if (!NKCUIVoiceManager.NeedSelectVoice())
					{
						UnityEngine.Debug.Log("[PatcherManager] Skip select voice process.");
					}
					else
					{
						yield return this.ProcessSelectVoice();
					}
					this.SetState(NKCPatcherManager.PATCHER_STATE.DisplayGameGrade);
					break;
				case NKCPatcherManager.PATCHER_STATE.DisplayGameGrade:
					if (NKCDefineManager.DEFINE_STEAM() && NKCConnectionInfo.CurrentLoginServerType == NKCConnectionInfo.LOGIN_SERVER_TYPE.Korea)
					{
						yield return this.m_NKCLogo.DisplayGameGrade();
					}
					this.SetState(NKCPatcherManager.PATCHER_STATE.Localizing);
					break;
				case NKCPatcherManager.PATCHER_STATE.Localizing:
				{
					this.m_NKCFontChanger.ChagneAllMainFont(NKCStringTable.GetNationalCode());
					NKC_VOICE_CODE nkc_VOICE_CODE = NKCUIVoiceManager.LoadLocalVoiceCode();
					NKCUIVoiceManager.SetVoiceCode(nkc_VOICE_CODE);
					AssetBundleManager.ActiveVariants = NKCLocalization.GetVariants(NKCStringTable.GetNationalCode(), nkc_VOICE_CODE);
					if (NKCDefineManager.DEFINE_PC_FORCE_VERSION_UP())
					{
						this.SetState(NKCPatcherManager.PATCHER_STATE.CheckPCVersionUp);
					}
					else
					{
						this.SetState(NKCPatcherManager.PATCHER_STATE.CheckCanStartPatch);
					}
					break;
				}
				case NKCPatcherManager.PATCHER_STATE.CheckPCVersionUp:
					if (this.ProcessPcForceVersionUp())
					{
						goto Block_21;
					}
					this.SetState(NKCPatcherManager.PATCHER_STATE.CheckCanStartPatch);
					break;
				case NKCPatcherManager.PATCHER_STATE.CheckCanStartPatch:
					if (!this.CheckCanStartPatch())
					{
						goto Block_22;
					}
					if (NKCDefineManager.DEFINE_OBB())
					{
						this.SetState(NKCPatcherManager.PATCHER_STATE.InitObb);
					}
					else
					{
						this.SetState(NKCPatcherManager.PATCHER_STATE.Patch);
					}
					break;
				case NKCPatcherManager.PATCHER_STATE.InitObb:
					NKCObbUtil.Init();
					this.SetState(NKCPatcherManager.PATCHER_STATE.Patch);
					break;
				case NKCPatcherManager.PATCHER_STATE.MakeDownloadList:
					yield return NKCPatchManifestManager.MakeDownloadList();
					this.SetState(NKCPatcherManager.PATCHER_STATE.MakeDownloadListForExtraAsset);
					break;
				case NKCPatcherManager.PATCHER_STATE.MakeDownloadListForExtraAsset:
					yield return NKCPatchManifestManager.MakeDownloadListForExtraAsset();
					this.SetState(NKCPatcherManager.PATCHER_STATE.MakeDownloadListForTutorialAsset);
					break;
				case NKCPatcherManager.PATCHER_STATE.MakeDownloadListForTutorialAsset:
					yield return NKCPatchManifestManager.MakeDownloadListForTutorialAsset();
					this.SetState(NKCPatcherManager.PATCHER_STATE.SelectPatchDownloadType);
					break;
				case NKCPatcherManager.PATCHER_STATE.SelectPatchDownloadType:
					if (NKCPatchUtility.BackgroundPatchEnabled())
					{
						List<NKCPopupDownloadTypeData> downloadTypeDataList = new List<NKCPopupDownloadTypeData>();
						this.m_popupDownloadTypeSelection.Open(new Action<NKCPatchDownloader.DownType>(NKCPatchUtility.SaveDownloadType), downloadTypeDataList);
						yield return this.m_popupDownloadTypeSelection.WaitForClick();
					}
					break;
				case NKCPatcherManager.PATCHER_STATE.Patch:
					yield return this.m_NkcPatchChecker.ProcessPatch();
					if (!this.m_NkcPatchChecker.PatchSuccess)
					{
						UnityEngine.Debug.LogError("ProcessPatch Error Occurred");
					}
					if (NKCDefineManager.DEFINE_SELECT_SERVER())
					{
						this.SetState(NKCPatcherManager.PATCHER_STATE.GetTagFromSelectedLoginServer);
					}
					else
					{
						this.SetState(NKCPatcherManager.PATCHER_STATE.StartGame);
					}
					break;
				case NKCPatcherManager.PATCHER_STATE.GetTagFromSelectedLoginServer:
					yield return this.UpdateServerMaintenanceData();
					if (NKCConnectionInfo.IsServerUnderMaintenance())
					{
						Log.Debug("[PatcherManager] Selected server is under maintenance.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatcherManager.cs", 477);
						bool bWait = true;
						NKCUtil.SetGameobjectActive(this.m_popupBox, true);
						NKCPublisherModule.OnComplete <>9__2;
						Action <>9__3;
						this.m_popupBox.OpenOKCancel(NKCUtilString.GET_STRING_PATCHER_NOTICE, NKCStringTable.GetString("SI_SYSTEM_NOTICE_MAINTENANCE_DESC", false), delegate
						{
							NKCPopupNoticeWeb nkcnoticeWeb = this.m_NKCNoticeWeb;
							string url = NKCPublisherModule.Notice.NoticeUrl(true);
							NKCPublisherModule.OnComplete onWindowClosed;
							if ((onWindowClosed = <>9__2) == null)
							{
								onWindowClosed = (<>9__2 = delegate(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
								{
									bWait = false;
								});
							}
							nkcnoticeWeb.Open(url, onWindowClosed, true);
						}, delegate
						{
							NKCPopupOKCancel.ClosePopupBox();
							NKCPopupSelectServer nkcselectServer = this.m_NKCSelectServer;
							bool bShowCloseMenu = false;
							bool bMoveToPatcher = true;
							Action onClosed;
							if ((onClosed = <>9__3) == null)
							{
								onClosed = (<>9__3 = delegate()
								{
									bWait = false;
								});
							}
							nkcselectServer.Open(bShowCloseMenu, bMoveToPatcher, onClosed);
						}, null, NKCStringTable.GetString("SI_DP_LOGIN_SELECTSERVER", false), false, false);
						while (bWait)
						{
							yield return null;
						}
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_NKM_UI_WAIT, true);
						yield return this.GetServerContentTagProcess();
						NKCUtil.SetGameobjectActive(this.m_NKM_UI_WAIT, false);
						if (this.m_eResultCode != NKC_PUBLISHER_RESULT_CODE.NPRC_OK)
						{
							NKCConnectionInfo.DeleteLocalTag();
							this.ShowRequestTimer(false);
							bool bWait = true;
							NKCUtil.SetGameobjectActive(this.m_popupBox, true);
							NKCPublisherModule.OnComplete <>9__5;
							this.m_popupBox.OpenOK(NKCUtilString.GET_STRING_PATCHER_NOTICE, NKCStringTable.GetString("SI_DP_PATCHER_ERROR_SERVER_TAG_UPDATE_FAILED", false), delegate
							{
								NKCPopupNoticeWeb nkcnoticeWeb = this.m_NKCNoticeWeb;
								string url = NKCPublisherModule.Notice.NoticeUrl(true);
								NKCPublisherModule.OnComplete onWindowClosed;
								if ((onWindowClosed = <>9__5) == null)
								{
									onWindowClosed = (<>9__5 = delegate(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
									{
										bWait = false;
									});
								}
								nkcnoticeWeb.Open(url, onWindowClosed, true);
							}, "", false);
							while (bWait)
							{
								yield return null;
							}
						}
						else
						{
							NKCConnectionInfo.SaveCurrentLoginServerType();
							this.SetState(NKCPatcherManager.PATCHER_STATE.StartGame);
						}
					}
					break;
				case NKCPatcherManager.PATCHER_STATE.StartGame:
					goto IL_813;
				}
			}
			Block_21:
			yield break;
			Block_22:
			yield break;
			IL_813:
			this.StartGame();
			yield return "Patcher Process Succeed.";
			yield break;
		}

		// Token: 0x06004A07 RID: 18951 RVA: 0x001636D6 File Offset: 0x001618D6
		private void SetState(NKCPatcherManager.PATCHER_STATE nextState)
		{
			if (this.m_currentPatcherState != NKCPatcherManager.PATCHER_STATE.None)
			{
				this.OnExit();
			}
			this.m_currentPatcherState = nextState;
			this.OnEnter();
		}

		// Token: 0x06004A08 RID: 18952 RVA: 0x001636F4 File Offset: 0x001618F4
		private void OnEnter()
		{
			UnityEngine.Debug.Log(string.Format("[PatcherManager] EnterState [{0}]", this.m_currentPatcherState));
			NKCPatcherManager.PATCHER_STATE currentPatcherState = this.m_currentPatcherState;
			if (currentPatcherState <= NKCPatcherManager.PATCHER_STATE.SelectServer)
			{
				if (currentPatcherState == NKCPatcherManager.PATCHER_STATE.DisplayLogo)
				{
					NKCUtil.SetGameobjectActive(this.m_NKCLogo, true);
					this.m_NKCLogo.Init();
					return;
				}
				if (currentPatcherState != NKCPatcherManager.PATCHER_STATE.SelectServer)
				{
					return;
				}
				NKM_NATIONAL_CODE nkm_NATIONAL_CODE = NKCGameOptionData.LoadLanguageCode(NKM_NATIONAL_CODE.NNC_END);
				if (nkm_NATIONAL_CODE == NKM_NATIONAL_CODE.NNC_END)
				{
					nkm_NATIONAL_CODE = NKCPublisherModule.Localization.GetDefaultLanguage();
				}
				UnityEngine.Debug.Log(string.Format("[PatcherManager] Set nation code for select server. : [{0}]", nkm_NATIONAL_CODE));
				NKCStringTable.SetNationalCode(nkm_NATIONAL_CODE);
				UnityEngine.Debug.Log(string.Format("[PatcherManager] Load patcher string table for select server. : [{0}]", nkm_NATIONAL_CODE));
				NKCPatcherStringList.LoadStrings("LUA_PATCH_STRING", "m_dicString", false);
				if (this.m_eResultCode == NKC_PUBLISHER_RESULT_CODE.NPRC_SERVERINFO_FAIL_SERVERINFO_UPDATE)
				{
					this.SetState(NKCPatcherManager.PATCHER_STATE.CheckCanStartPatch);
					return;
				}
			}
			else
			{
				if (currentPatcherState == NKCPatcherManager.PATCHER_STATE.DisplayGameGrade)
				{
					NKCUtil.SetGameobjectActive(this.m_NKCLogo, true);
					this.m_NKCLogo.Init();
					return;
				}
				if (currentPatcherState != NKCPatcherManager.PATCHER_STATE.Patch)
				{
					return;
				}
				this.m_NkcPatchChecker.SetActive(true);
			}
		}

		// Token: 0x06004A09 RID: 18953 RVA: 0x001637EF File Offset: 0x001619EF
		private void OnExit()
		{
			UnityEngine.Debug.Log(string.Format("[PatcherManager] ExitState [{0}]", this.m_currentPatcherState));
		}

		// Token: 0x06004A0A RID: 18954 RVA: 0x0016380C File Offset: 0x00161A0C
		private void Init()
		{
			if (!NKCPatcherManager.s_bInit)
			{
				NKCLogManager.Init();
				UnityEngine.Debug.Log("[PatcherManager] Logmanager Init");
				UnityEngine.Debug.Log("[PatcherManager] Aplication Version [" + Application.version + "]");
				UnityEngine.Debug.Log("[PatcherManager] Aplication ProductName [" + Application.productName + "]");
				UnityEngine.Debug.Log(string.Format("[PatcherManager] Aplication Platform [{0}]", Application.platform));
				UnityEngine.Debug.Log("[PatcherManager] Aplication DataPath [" + Application.dataPath + "]");
				UnityEngine.Debug.Log("[PatcherManager] Aplication persistentDataPath [" + Application.persistentDataPath + "]");
				UnityEngine.Debug.unityLogger.logEnabled = NKCDefineManager.DEFINE_UNITY_DEBUG_LOG();
				NKCAdjustManager.Init();
				this.QuitAppIfMultiClient();
				this.WindowedFullScreenForPC();
				BetterStreamingAssets.Initialize();
				if (NKCDefineManager.DEFINE_USE_CHEAT())
				{
					DOTween.Init(new bool?(true), new bool?(true), new LogBehaviour?(LogBehaviour.Verbose));
				}
				else
				{
					DOTween.Init(new bool?(true), new bool?(true), new LogBehaviour?(LogBehaviour.ErrorsOnly));
				}
				DOTween.useSafeMode = true;
				DOTween.SetTweensCapacity(750, 50);
				NKCPatcherManager.s_bInit = true;
			}
			NKMContentsVersionManager.Drop();
			NKCConnectionInfo.Clear();
			if (this.m_popupLanguageSelect != null)
			{
				this.m_popupLanguageSelect.Init();
				this.m_popupLanguageSelect.gameObject.SetActive(false);
			}
			if (NKCPatcherManager.m_PatcherManager == null)
			{
				NKCPatcherManager.m_PatcherManager = this;
				Screen.sleepTimeout = -1;
				Application.targetFrameRate = 30;
				return;
			}
			UnityEngine.Object.Destroy(this);
		}

		// Token: 0x06004A0B RID: 18955 RVA: 0x00163980 File Offset: 0x00161B80
		private void WindowedFullScreenForPC()
		{
			if (NKCDefineManager.DEFINE_UNITY_STANDALONE_WIN())
			{
				GameObject gameObject = new GameObject("AspectRatioController");
				AspectRatioController aspectRatioController = gameObject.AddComponent<AspectRatioController>();
				aspectRatioController.allowFullscreen = true;
				ValueTuple<int, int> aspect = NKCGameOptionDataSt.GetAspect((NKCGameOptionDataSt.GameOptionGraphicAspectRatio)PlayerPrefs.GetInt("NKM_LOCAL_SAVE_GAME_OPTION_GRAPHIC_ASPECT_RATIO", 1));
				aspectRatioController.aspectRatioWidth = (float)aspect.Item1;
				aspectRatioController.aspectRatioHeight = (float)aspect.Item2;
				aspectRatioController.minWidthPixel = 512;
				aspectRatioController.minHeightPixel = 512;
				aspectRatioController.maxWidthPixel = 20480;
				aspectRatioController.maxHeightPixel = 20480;
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
			}
			if (NKCDefineManager.DEFINE_UNITY_STANDALONE() && !NKCDefineManager.DEFINE_UNITY_EDITOR() && PlayerPrefs.GetInt("NKM_LOCAL_SAVE_WINDOWED_FULL_RESOLUTION_FOR_PC", 0) == 0)
			{
				Resolution[] resolutions = Screen.resolutions;
				if (resolutions != null && resolutions.Length > 1)
				{
					Screen.SetResolution(resolutions[resolutions.Length - 1].width, resolutions[resolutions.Length - 1].height, FullScreenMode.Windowed);
				}
				PlayerPrefs.SetInt("NKM_LOCAL_SAVE_WINDOWED_FULL_RESOLUTION_FOR_PC", 1);
			}
		}

		// Token: 0x06004A0C RID: 18956 RVA: 0x00163A64 File Offset: 0x00161C64
		private void QuitAppIfMultiClient()
		{
			UnityEngine.Debug.Log("[PatcherManager] QuitAppIfMultiClient Prepare");
			if (NKCDefineManager.DEFINE_NX_PC())
			{
				UnityEngine.Debug.Log("[PatcherManager] NKCMultiClientPrevent prepare");
				if (NKCPatcherManager.s_objMultiClientPrevent == null)
				{
					NKCPatcherManager.s_objMultiClientPrevent = new GameObject("NKCMultiClientPrevent");
					UnityEngine.Object.DontDestroyOnLoad(NKCPatcherManager.s_objMultiClientPrevent);
					NKCPatcherManager.s_objMultiClientPrevent.AddComponent<NKCMultiClientPrevent>();
				}
			}
			if (NKCDefineManager.DEFINE_ALLOW_MULTIPC())
			{
				return;
			}
			if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
			{
				UnityEngine.Debug.Log("QuitAppIfMultiClient");
				Application.Quit();
				return;
			}
		}

		// Token: 0x06004A0D RID: 18957 RVA: 0x00163AE9 File Offset: 0x00161CE9
		private IEnumerator PublisherModuleInitProcess()
		{
			if (NKCPatcherManager.s_bPubModuleInit)
			{
				yield break;
			}
			UnityEngine.Debug.Log("[PatcherManager] startup.initinstance start");
			bool bWait = true;
			NKCPublisherModule.InitInstance(delegate(NKC_PUBLISHER_RESULT_CODE resultCode, string add)
			{
				UnityEngine.Debug.Log(string.Format("[PatcherManager] startup.initinstance. resultCode : {0}", resultCode));
				bWait = false;
				if (resultCode == NKC_PUBLISHER_RESULT_CODE.NPRC_STEAM_INITIALIZE_FAIL || resultCode == NKC_PUBLISHER_RESULT_CODE.NPRC_GAMEBASE_WEBSOCKET_READ_TIMEOUT || resultCode == NKC_PUBLISHER_RESULT_CODE.NPRC_GAMEBASE_INITIALIZE_FAIL)
				{
					this.m_eResultCode = resultCode;
				}
			});
			while (bWait)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x06004A0E RID: 18958 RVA: 0x00163AF8 File Offset: 0x00161CF8
		private IEnumerator UpdateDownloadConfigProcess()
		{
			string localDownloadPath = AssetBundleManager.GetLocalDownloadPath();
			string str = "CSConfigServerAddress.txt";
			if (NKCDefineManager.DEFINE_SB_GB())
			{
				str = "csconfigserveraddress.txt";
			}
			string text = Application.streamingAssetsPath + "/" + str;
			UnityEngine.Debug.Log("[PatcherManager] CSConfigServerAddressPath : " + text);
			if (NKCPatchUtility.IsFileExists(text))
			{
				UnityEngine.Debug.Log("[PatcherManager] CSConfigServerAddress exist");
				string aJSON;
				if (text.Contains("jar:"))
				{
					aJSON = BetterStreamingAssets.ReadAllText(NKCAssetbundleInnerStream.GetJarRelativePath(text));
				}
				else
				{
					aJSON = File.ReadAllText(text);
				}
				JSONNode jsonnode = JSONNode.Parse(aJSON);
				if (jsonnode != null)
				{
					string targetFileName = Path.Combine(localDownloadPath, "CSConfig.txt");
					string vidSavePath = targetFileName;
					string url = jsonnode["address"];
					string text2 = jsonnode["languageTag"];
					if (text2 != null)
					{
						NKCPublisherModule.Localization.SetDefaultLanage((NKM_NATIONAL_CODE)Enum.Parse(typeof(NKM_NATIONAL_CODE), text2));
					}
					if (!Directory.Exists(Path.GetDirectoryName(vidSavePath)))
					{
						Directory.CreateDirectory(Path.GetDirectoryName(vidSavePath));
					}
					int tryCountMax = 1;
					if (NKCDefineManager.DEFINE_ZLONG())
					{
						tryCountMax = 10;
					}
					bool flag = false;
					int i = 0;
					while (i < tryCountMax)
					{
						using (UnityWebRequest uwr = new UnityWebRequest(url))
						{
							uwr.method = "GET";
							uwr.downloadHandler = new DownloadHandlerFile(vidSavePath)
							{
								removeFileOnAbort = true
							};
							yield return uwr.SendWebRequest();
							if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
							{
								UnityEngine.Debug.Log("[PatcherManager] WebRequest error : " + uwr.error);
								if (i + 1 >= tryCountMax)
								{
									this.m_eResultCode = NKC_PUBLISHER_RESULT_CODE.NPRC_SERVERINFO_FAIL_FETCH_DOWNLOAD_ADDRESS;
									yield break;
								}
								yield return new WaitForSecondsRealtime(1f);
								goto IL_4C0;
							}
							else
							{
								UnityEngine.Debug.Log("[PatcherManager] Download saved to: " + vidSavePath.Replace("/", "\\") + "\r\n" + uwr.error);
								flag = true;
								if (NKCPatchUtility.IsFileExists(targetFileName))
								{
									aJSON = File.ReadAllText(targetFileName);
									JSONNode jsonnode2 = JSONNode.Parse(aJSON);
									if (jsonnode2 != null)
									{
										NKCConnectionInfo.DownloadServerAddress = jsonnode2["PatchServerAddress1"];
										NKCConnectionInfo.DownloadServerAddress2 = jsonnode2["PatchServerAddress2"];
										NKCDownloadConfig.s_ServerID = jsonnode2["ServerId"];
										NKCDownloadConfig.s_ServerName = jsonnode2["ServerName"];
										NKCConnectionInfo.SetLoginServerInfo(NKCConnectionInfo.LOGIN_SERVER_TYPE.Default, jsonnode2["CSLoginServerIP"], int.Parse(jsonnode2["CSLoginServerPort"]), null);
										JSONNode jsonnode3 = jsonnode2["IgnoreVariantList"];
										foreach (JSONNode d in ((jsonnode3 != null) ? jsonnode3.AsArray : null).Children)
										{
											NKCConnectionInfo.IgnoreVariantList.Add(d);
										}
										if (jsonnode2["DefaultCountryTagSet"] != null)
										{
											NKCContentsVersionManager.TagVariableName = jsonnode2["DefaultCountryTagSet"];
										}
										if (jsonnode2["LOGIN_FAIL_MSG"] != null)
										{
											NKCConnectionInfo.s_LoginFailMsg = jsonnode2["LOGIN_FAIL_MSG"];
										}
										if (NKCDefineManager.DEFINE_USE_CUSTOM_SERVERS())
										{
											UnityEngine.Debug.Log("[PatcherManager] Defined custom servers - checking for ServiceAddress Redirection");
											string @string = PlayerPrefs.GetString("LOCAL_SAVE_CONTENTS_TAG_LAST_SERVER_IP");
											int @int = PlayerPrefs.GetInt("LOCAL_SAVE_CONTENTS_TAG_LAST_SERVER_PORT");
											if (!string.IsNullOrEmpty(@string))
											{
												UnityEngine.Debug.Log(string.Concat(new string[]
												{
													"[PatcherManager] ServiceIP Redirected [",
													NKCConnectionInfo.ServiceIP,
													"] -> [",
													@string,
													"]"
												}));
												NKCConnectionInfo.SetLoginServerInfo(NKCConnectionInfo.LOGIN_SERVER_TYPE.Default, @string, -1, null);
											}
											if (@int != 0)
											{
												UnityEngine.Debug.Log(string.Format("[PatcherManager] ServicePort Redirected [{0}] -> [{1}]", NKCConnectionInfo.ServicePort, @int));
												NKCConnectionInfo.SetLoginServerInfo(NKCConnectionInfo.LOGIN_SERVER_TYPE.Default, "", @int, null);
											}
										}
									}
								}
							}
						}
						goto JumpOutOfTryFinally-3;
						IL_4D3:
						int num = i;
						i = num + 1;
						continue;
						JumpOutOfTryFinally-3:
						UnityWebRequest uwr = null;
						if (flag)
						{
							break;
						}
						IL_4C0:
						goto IL_4D3;
					}
					targetFileName = null;
					vidSavePath = null;
					url = null;
				}
			}
			this.m_eResultCode = NKC_PUBLISHER_RESULT_CODE.NPRC_OK;
			yield break;
			yield break;
		}

		// Token: 0x06004A0F RID: 18959 RVA: 0x00163B07 File Offset: 0x00161D07
		private IEnumerator UpdateServerInfomationProcess()
		{
			UnityEngine.Debug.Log(string.Format("[PatcherManager] UpdateServerInfomationProcess ServerInfo[{0}] ServerInfoFileName[{1}]", NKCPublisherModule.ServerInfo, NKCConnectionInfo.ServerInfoFileName));
			string serverConfigPath = NKCPublisherModule.ServerInfo.GetServerConfigPath();
			UnityEngine.Debug.Log("[PatcherManager] ServerInfo from " + serverConfigPath);
			using (UnityWebRequest uwr = new UnityWebRequest(serverConfigPath))
			{
				uwr.downloadHandler = new DownloadHandlerBuffer();
				yield return uwr.SendWebRequest();
				if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
				{
					UnityEngine.Debug.Log("[PatcherManager] " + uwr.error);
					this.m_eResultCode = NKC_PUBLISHER_RESULT_CODE.NPRC_SERVERINFO_FAIL_SERVERINFO_UPDATE;
					yield break;
				}
				NKCConnectionInfo.LoadFromJSON(uwr.downloadHandler.text);
			}
			UnityWebRequest uwr = null;
			this.m_eResultCode = NKC_PUBLISHER_RESULT_CODE.NPRC_OK;
			yield break;
			yield break;
		}

		// Token: 0x06004A10 RID: 18960 RVA: 0x00163B16 File Offset: 0x00161D16
		public IEnumerator UpdateServerMaintenanceData()
		{
			Log.Debug(string.Format("[PatcherManager][Maintenance] UpdateServerMaintenanceData ServerInfo[{0}] ServerInfoFileName[{1}]", NKCPublisherModule.ServerInfo, NKCConnectionInfo.ServerInfoFileName), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatcherManager.cs", 959);
			string serverConfigPath = NKCPublisherModule.ServerInfo.GetServerConfigPath();
			Log.Debug("[PatcherManager][Maintenance] UpdateServerMaintenanceData ServerInfo from " + serverConfigPath, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatcherManager.cs", 963);
			if (!NKCConnectionInfo.CheckDownloadInterval())
			{
				Log.Debug("[PatcherManager][Maintenance][CheckDownloadInterval] Interval", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatcherManager.cs", 967);
			}
			else
			{
				using (UnityWebRequest uwr = new UnityWebRequest(serverConfigPath))
				{
					uwr.downloadHandler = new DownloadHandlerBuffer();
					yield return uwr.SendWebRequest();
					if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
					{
						Log.Debug("[PatcherManager] " + uwr.error, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatcherManager.cs", 979);
						this.m_eResultCode = NKC_PUBLISHER_RESULT_CODE.NPRC_SERVERINFO_FAIL_SERVERINFO_UPDATE;
						yield break;
					}
					NKCConnectionInfo.SetConfigJSONString(uwr.downloadHandler.text);
					Log.Debug("[PatcherManager][Maintenance] request success", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatcherManager.cs", 986);
				}
				UnityWebRequest uwr = null;
			}
			NKCConnectionInfo.LoadMaintenanceDataFromJSON();
			this.m_eResultCode = NKC_PUBLISHER_RESULT_CODE.NPRC_OK;
			yield break;
			yield break;
		}

		// Token: 0x06004A11 RID: 18961 RVA: 0x00163B25 File Offset: 0x00161D25
		private IEnumerator GetServerContentTagProcess()
		{
			string serviceIP = NKCConnectionInfo.ServiceIP;
			UnityEngine.Debug.Log("[PatcherManager] Trying to retrieve server tag from " + serviceIP);
			yield return ContentsVersionChecker.GetVersion(serviceIP, -1, NKCPublisherModule.ServerInfo.GetUseLocalSaveLastServerInfoToGetTags());
			if (ContentsVersionChecker.Ack != null)
			{
				this.m_eResultCode = NKC_PUBLISHER_RESULT_CODE.NPRC_OK;
				NKCPublisherModule.Statistics.LogClientAction(NKCPublisherModule.NKCPMStatistics.eClientAction.Patch_TagProvided, 0, null);
			}
			else
			{
				this.m_eResultCode = NKC_PUBLISHER_RESULT_CODE.NPRC_SERVERINFO_FAIL_FETCH_TAG;
				NKCPublisherModule.Statistics.LogClientAction(NKCPublisherModule.NKCPMStatistics.eClientAction.Patch_TagGetFailed, 0, null);
			}
			yield break;
		}

		// Token: 0x06004A12 RID: 18962 RVA: 0x00163B34 File Offset: 0x00161D34
		public IEnumerator ProcessSelectServer()
		{
			this.m_NKCSelectServer.Open(false, false, null);
			NKCConnectionInfo.CurrentLoginServerType = NKCConnectionInfo.LOGIN_SERVER_TYPE.None;
			while (NKCConnectionInfo.CurrentLoginServerType == NKCConnectionInfo.LOGIN_SERVER_TYPE.None)
			{
				yield return null;
			}
			this.m_NKCSelectServer.Close();
			yield break;
		}

		// Token: 0x06004A13 RID: 18963 RVA: 0x00163B43 File Offset: 0x00161D43
		private IEnumerator ProcessSelectVoice()
		{
			bool bWait = true;
			this.m_popupVoiceSelect.Init(delegate
			{
				bWait = false;
			}, true);
			this.m_popupVoiceSelect.Open();
			while (bWait)
			{
				yield return null;
			}
			this.m_popupVoiceSelect.Close();
			yield break;
		}

		// Token: 0x06004A14 RID: 18964 RVA: 0x00163B52 File Offset: 0x00161D52
		private IEnumerator SetNationCode()
		{
			NKM_NATIONAL_CODE currentCode = NKCGameOptionData.LoadLanguageCode(NKM_NATIONAL_CODE.NNC_END);
			UnityEngine.Debug.Log("[PatcherManager] LoadedLanguageCode : [" + currentCode.ToString() + "]");
			HashSet<NKM_NATIONAL_CODE> setLanguages = NKCLocalization.GetSelectLanguageSet();
			if (currentCode == NKM_NATIONAL_CODE.NNC_END)
			{
				if (NKCPublisherModule.Localization.UseDefaultLanguageOnFirstRun)
				{
					currentCode = NKCPublisherModule.Localization.GetDefaultLanguage();
				}
				else if (setLanguages.Count == 0)
				{
					currentCode = NKM_NATIONAL_CODE.NNC_KOREA;
				}
				else
				{
					if (setLanguages.Count == 1)
					{
						using (HashSet<NKM_NATIONAL_CODE>.Enumerator enumerator = setLanguages.GetEnumerator())
						{
							if (!enumerator.MoveNext())
							{
								goto IL_168;
							}
							NKM_NATIONAL_CODE currentCode3 = enumerator.Current;
							currentCode = currentCode3;
							goto IL_168;
						}
					}
					bool bWait = true;
					this.m_popupLanguageSelect.Open(setLanguages, delegate(NKM_NATIONAL_CODE language)
					{
						currentCode = language;
						bWait = false;
					});
					while (bWait)
					{
						yield return null;
					}
				}
			}
			IL_168:
			if (setLanguages.Count > 0 && !setLanguages.Contains(currentCode))
			{
				UnityEngine.Debug.Log(string.Format("[PatcherManager] LanguageSet does not contains selected code. : selected code : [{0}]", currentCode));
				using (HashSet<NKM_NATIONAL_CODE>.Enumerator enumerator = setLanguages.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						NKM_NATIONAL_CODE currentCode2 = enumerator.Current;
						currentCode = currentCode2;
					}
				}
			}
			UnityEngine.Debug.Log(string.Format("[PatcherManager] Set NationalCode : [{0}]", currentCode));
			this.SaveLanguageCode(currentCode);
			NKCStringTable.SetNationalCode(currentCode);
			yield break;
		}

		// Token: 0x06004A15 RID: 18965 RVA: 0x00163B64 File Offset: 0x00161D64
		private bool CheckCanStartPatch()
		{
			UnityEngine.Debug.Log(string.Format("[PatcherManager] m_eResultCode : {0}]", this.m_eResultCode));
			if (this.m_eResultCode != NKC_PUBLISHER_RESULT_CODE.NPRC_OK)
			{
				string strID = "";
				bool flag = true;
				NKC_PUBLISHER_RESULT_CODE eResultCode = this.m_eResultCode;
				if (eResultCode <= NKC_PUBLISHER_RESULT_CODE.NPRC_STEAM_INITIALIZE_FAIL)
				{
					switch (eResultCode)
					{
					case NKC_PUBLISHER_RESULT_CODE.NPRC_SERVERINFO_FAIL_SERVERINFO_UPDATE:
						strID = "SI_DP_PATCHER_ERROR_SERVER_INFO_UPDATE_FAILED";
						goto IL_B2;
					case NKC_PUBLISHER_RESULT_CODE.NPRC_SERVERINFO_FAIL_FETCH_DOWNLOAD_ADDRESS:
						strID = "SI_DP_PATCHER_ERROR_DOWNLOAD_ADDRESS_FETCH_FAILED";
						goto IL_B2;
					case NKC_PUBLISHER_RESULT_CODE.NPRC_SERVERINFO_FAIL_FETCH_TAG:
						flag = false;
						AssetBundleManager.ActiveVariants = NKCLocalization.GetVariants(NKCStringTable.GetNationalCode(), NKCUIVoiceManager.LoadLocalVoiceCode());
						goto IL_B2;
					default:
						if (eResultCode == NKC_PUBLISHER_RESULT_CODE.NPRC_STEAM_INITIALIZE_FAIL)
						{
							strID = "SI_DP_PATCHER_FAIL_STEAM_INITIALIZE";
							goto IL_B2;
						}
						break;
					}
				}
				else
				{
					if (eResultCode == NKC_PUBLISHER_RESULT_CODE.NPRC_GAMEBASE_WEBSOCKET_READ_TIMEOUT)
					{
						strID = "SI_DP_PATCHER_DOWNLOAD_ERROR";
						goto IL_B2;
					}
					if (eResultCode == NKC_PUBLISHER_RESULT_CODE.NPRC_GAMEBASE_INITIALIZE_FAIL)
					{
						strID = "SI_DP_PATCHER_DOWNLOAD_ERROR";
						goto IL_B2;
					}
				}
				flag = false;
				IL_B2:
				if (flag)
				{
					NKCUtil.SetGameobjectActive(this.m_popupBox, true);
					this.m_popupBox.OpenOK(NKCUtilString.GET_STRING_PATCHER_ERROR, NKCStringTable.GetString(strID, false), delegate
					{
						Application.Quit();
					}, "", false);
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004A16 RID: 18966 RVA: 0x00163C74 File Offset: 0x00161E74
		private bool ProcessPcForceVersionUp()
		{
			if (NKCDownloadConfig.s_vecAllowedVersion.Count <= 0)
			{
				return false;
			}
			bool flag = false;
			for (int i = 0; i < NKCDownloadConfig.s_vecAllowedVersion.Count; i++)
			{
				if (Application.version == NKCDownloadConfig.s_vecAllowedVersion[i])
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				this.m_NkcPatchChecker.gameObject.SetActive(true);
				NKCUtil.SetGameobjectActive(this.m_popupBox, true);
				string content;
				if (NKCPublisherModule.PublisherType == NKCPublisherModule.ePublisherType.Zlong)
				{
					content = NKCUtilString.GET_STRING_PATCHER_PC_NEW_APP_AVAILABLE_ZLONG;
				}
				else
				{
					content = NKCUtilString.GET_STRING_PATCHER_NEED_UPDATE;
				}
				this.m_popupBox.OpenOK(NKCUtilString.GET_STRING_PATCHER_ERROR, content, delegate
				{
					Application.Quit();
				}, "", false);
				return true;
			}
			return false;
		}

		// Token: 0x06004A17 RID: 18967 RVA: 0x00163D37 File Offset: 0x00161F37
		private void SaveLanguageCode(NKM_NATIONAL_CODE code)
		{
			NKCGameOptionData.SaveOnlyLang(code);
			NKCPublisherModule.Localization.SetPublisherModuleLanguage(code);
		}

		// Token: 0x06004A18 RID: 18968 RVA: 0x00163D4C File Offset: 0x00161F4C
		public void SetSafeArea()
		{
			if (this.m_NKC_PATCHER_UI_FRONT_RectTransform != null)
			{
				Vector2 vector = this.m_NKC_PATCHER_UI_FRONT_RectTransform.localScale;
				vector.x = Screen.safeArea.width / (float)Screen.currentResolution.width;
				vector.y = Screen.safeArea.height / (float)Screen.currentResolution.height;
				if (vector.x > vector.y)
				{
					vector.x = vector.y;
				}
				else
				{
					vector.y = vector.x;
				}
				this.m_NKC_PATCHER_UI_FRONT_RectTransform.localScale = vector;
			}
		}

		// Token: 0x06004A19 RID: 18969 RVA: 0x00163DFC File Offset: 0x00161FFC
		public void StartGame()
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_WAIT, true);
			Font[] fonts = Resources.FindObjectsOfTypeAll<Font>();
			Application.backgroundLoadingPriority = ThreadPriority.High;
			AssetBundleManager.LoadLevelAsync("NKM_SCEN_APP", false, delegate()
			{
				this.UnloadFonts(fonts);
			});
		}

		// Token: 0x06004A1A RID: 18970 RVA: 0x00163E4C File Offset: 0x0016204C
		private void UnloadFonts(Font[] fonts)
		{
			foreach (Font font in fonts)
			{
				if (font.name.Contains("MainFont") || font.name.Contains("Rajdhani"))
				{
					Resources.UnloadAsset(font);
				}
			}
		}

		// Token: 0x06004A1B RID: 18971 RVA: 0x00163E98 File Offset: 0x00162098
		public void ShowRequestTimer(bool bShow)
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_WAIT, bShow);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_TAG_REQUEST_NOTICE, bShow);
			NKCUtil.SetGameobjectActive(this.m_TagListRequestMsg, false);
			if (!string.IsNullOrEmpty(NKCConnectionInfo.s_LoginFailMsg))
			{
				NKCUtil.SetGameobjectActive(this.m_TagListRequestMsg, true);
				NKCUtil.SetLabelText(this.m_TagListRequestMsg, NKCConnectionInfo.s_LoginFailMsg);
				return;
			}
			if (!this.m_bRunningMsgCoroutine)
			{
				base.StartCoroutine(this.UpdateLoginMsg());
			}
		}

		// Token: 0x06004A1C RID: 18972 RVA: 0x00163F07 File Offset: 0x00162107
		private IEnumerator UpdateLoginMsg()
		{
			this.m_bRunningMsgCoroutine = true;
			string localDownloadPath = AssetBundleManager.GetLocalDownloadPath();
			string text = Application.streamingAssetsPath + "/CSConfigServerAddress.txt";
			if (!NKCPatchUtility.IsFileExists(text))
			{
				yield break;
			}
			UnityEngine.Debug.Log("[PatcherManager] CSConfigServerAddress exist");
			string aJSON;
			if (text.Contains("jar:"))
			{
				aJSON = BetterStreamingAssets.ReadAllText(NKCAssetbundleInnerStream.GetJarRelativePath(text));
			}
			else
			{
				aJSON = File.ReadAllText(text);
			}
			JSONNode jsonnode = JSONNode.Parse(aJSON);
			if (jsonnode == null)
			{
				yield break;
			}
			string url = jsonnode["address"];
			string targetFileName = Path.Combine(localDownloadPath, "CSConfig.txt");
			if (!Directory.Exists(Path.GetDirectoryName(targetFileName)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(targetFileName));
			}
			using (UnityWebRequest uwr = new UnityWebRequest(url))
			{
				uwr.method = "GET";
				uwr.downloadHandler = new DownloadHandlerFile(targetFileName)
				{
					removeFileOnAbort = true
				};
				yield return uwr.SendWebRequest();
				if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
				{
					UnityEngine.Debug.Log("[PatcherManager] " + uwr.error);
					yield break;
				}
				UnityEngine.Debug.Log("[PatcherManager] Download saved to: " + targetFileName.Replace("/", "\\") + "\r\n" + uwr.error);
				if (!NKCPatchUtility.IsFileExists(targetFileName))
				{
					yield break;
				}
				aJSON = File.ReadAllText(targetFileName);
				JSONNode jsonnode2 = JSONNode.Parse(aJSON);
				if (jsonnode2 != null)
				{
					NKCConnectionInfo.s_LoginFailMsg = jsonnode2["LOGIN_FAIL_MSG"];
					NKCUtil.SetLabelText(this.m_TagListRequestMsg, NKCConnectionInfo.s_LoginFailMsg);
				}
			}
			UnityWebRequest uwr = null;
			this.m_bRunningMsgCoroutine = false;
			yield break;
			yield break;
		}

		// Token: 0x06004A1D RID: 18973 RVA: 0x00163F18 File Offset: 0x00162118
		public void ShowError(Exception e)
		{
			UnityEngine.Debug.LogError(e);
			int currentPatcherState = (int)this.m_currentPatcherState;
			string text = currentPatcherState.ToString();
			string msg;
			if (NKCStringTable.CheckExistString("SI_DP_PATCH_PROCESS_EXCEPTION"))
			{
				msg = NKCStringTable.GetString("SI_DP_PATCH_PROCESS_EXCEPTION", false, new object[]
				{
					text
				});
			}
			else
			{
				msg = "SI_DP_PATCH_PROCESS_EXCEPTION [" + text + "]";
			}
			this.ShowError(msg);
		}

		// Token: 0x06004A1E RID: 18974 RVA: 0x00163F78 File Offset: 0x00162178
		public void ShowError(string msg)
		{
			NKCUtil.SetGameobjectActive(this.m_popupBox, true);
			if (NKCDefineManager.DEFINE_USE_CHEAT() || !NKCDefineManager.DEFINE_SERVICE())
			{
				msg = msg + "\nNationTag: " + NKMContentsVersionManager.GetCountryTag();
			}
			this.m_popupBox.OpenOK(NKCUtilString.GET_STRING_PATCHER_ERROR, msg, delegate
			{
				Application.Quit();
			}, "", false);
			base.StopAllCoroutines();
		}

		// Token: 0x06004A1F RID: 18975 RVA: 0x00163FED File Offset: 0x001621ED
		public void ShowUpdate()
		{
			base.StopAllCoroutines();
			NKCUtil.SetGameobjectActive(this.m_popupBox, true);
			this.m_popupBox.OpenOK(NKCUtilString.GET_STRING_PATCHER_NOTICE, NKCUtilString.GET_STRING_PATCHER_NEED_UPDATE, new NKCPopupOKCancel.OnButton(this.MoveToMarket), "", false);
		}

		// Token: 0x06004A20 RID: 18976 RVA: 0x00164028 File Offset: 0x00162228
		public void MoveToMarket()
		{
			if (NKCPatchDownloader.Instance != null)
			{
				NKCPatchDownloader.Instance.MoveToMarket();
				return;
			}
			Application.Quit();
		}

		// Token: 0x06004A21 RID: 18977 RVA: 0x00164047 File Offset: 0x00162247
		public IEnumerator WaitForOKCancel(string title, string msg, string OKButtonString, string cancelButtonString, NKCPopupOKCancel.OnButton onOK)
		{
			bool bChecked = false;
			NKCUtil.SetGameobjectActive(this.m_popupBox, true);
			this.m_popupBox.OpenOKCancel(title, msg, delegate
			{
				NKCPopupOKCancel.OnButton onOK2 = onOK;
				if (onOK2 != null)
				{
					onOK2();
				}
				bChecked = true;
			}, delegate
			{
				bChecked = true;
			}, OKButtonString, cancelButtonString, false, false);
			while (!bChecked)
			{
				if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Keypad5))
				{
					this.m_popupBox.OnOK();
				}
				else if (Input.GetKeyUp(KeyCode.Escape))
				{
					this.m_popupBox.OnBackButton();
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x06004A22 RID: 18978 RVA: 0x0016407B File Offset: 0x0016227B
		public IEnumerator WaitForDownloadTypeSelect(Action<NKCPatchDownloader.DownType> okAction, float totalDownloadSize, float essentialDownloadSize, float nonEssentialDownloadSize, float tutorialDownloadSize)
		{
			if (this.m_popupDownloadTypeSelection == null)
			{
				NKCPatchUtility.SaveDownloadType(NKCPatchDownloader.DownType.FullDownload);
				UnityEngine.Debug.LogWarning("DownloadTypeSelect is null");
				yield break;
			}
			this.m_popupDownloadTypeSelection.Open(okAction, totalDownloadSize, essentialDownloadSize, nonEssentialDownloadSize, tutorialDownloadSize);
			yield return this.m_popupDownloadTypeSelection.WaitForClick();
			yield break;
		}

		// Token: 0x06004A23 RID: 18979 RVA: 0x001640AF File Offset: 0x001622AF
		public IEnumerator WaitForOKBox(string title, string msg, string OKButtonString = "")
		{
			bool bChecked = false;
			NKCUtil.SetGameobjectActive(this.m_popupBox, true);
			this.m_popupBox.OpenOK(title, msg, delegate
			{
				bChecked = true;
			}, OKButtonString, true);
			while (!bChecked)
			{
				if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Keypad5))
				{
					this.m_popupBox.OnOK();
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x040038D6 RID: 14550
		private static NKCPatcherManager m_PatcherManager;

		// Token: 0x040038D7 RID: 14551
		public GameObject m_NKC_PATCHER_UI_FRONT;

		// Token: 0x040038D8 RID: 14552
		private RectTransform m_NKC_PATCHER_UI_FRONT_RectTransform;

		// Token: 0x040038D9 RID: 14553
		private Mask m_NKC_PATCHER_UI_FRONT_Mask;

		// Token: 0x040038DA RID: 14554
		public GameObject m_NKM_UI_WAIT;

		// Token: 0x040038DB RID: 14555
		public GameObject m_NKM_UI_TAG_REQUEST_NOTICE;

		// Token: 0x040038DC RID: 14556
		public Text m_TagListRequestMsg;

		// Token: 0x040038DD RID: 14557
		public NKCPatchChecker m_NkcPatchChecker;

		// Token: 0x040038DE RID: 14558
		public NKCLogo m_NKCLogo;

		// Token: 0x040038DF RID: 14559
		public NKCPopupSelectServer m_NKCSelectServer;

		// Token: 0x040038E0 RID: 14560
		public NKCPopupNoticeWeb m_NKCNoticeWeb;

		// Token: 0x040038E1 RID: 14561
		public NKCFontChanger m_NKCFontChanger;

		// Token: 0x040038E2 RID: 14562
		[Header("언어 선택 팝업")]
		public NKCUIPopupLanguageSelect m_popupLanguageSelect;

		// Token: 0x040038E3 RID: 14563
		[Header("음성 선택 팝업")]
		public NKCPopupVoiceLanguageSelect m_popupVoiceSelect;

		// Token: 0x040038E4 RID: 14564
		[Header("오류 표시용 팝업")]
		public NKCPopupOKCancel m_popupBox;

		// Token: 0x040038E5 RID: 14565
		[Header("다운 선택 팝업")]
		public NKCPopupDownloadTypeSelection m_popupDownloadTypeSelection;

		// Token: 0x040038E6 RID: 14566
		private static bool s_bInit;

		// Token: 0x040038E7 RID: 14567
		private static bool s_bPubModuleInit;

		// Token: 0x040038E8 RID: 14568
		private bool m_bRunningMsgCoroutine;

		// Token: 0x040038EA RID: 14570
		private NKCPatcherManager.PATCHER_STATE m_currentPatcherState;

		// Token: 0x040038EB RID: 14571
		private static GameObject s_objMultiClientPrevent;

		// Token: 0x02001407 RID: 5127
		private enum PATCHER_STATE
		{
			// Token: 0x04009D08 RID: 40200
			None,
			// Token: 0x04009D09 RID: 40201
			Init,
			// Token: 0x04009D0A RID: 40202
			DisplayLogo,
			// Token: 0x04009D0B RID: 40203
			InitPM,
			// Token: 0x04009D0C RID: 40204
			UpdateDownloadConfigProcess,
			// Token: 0x04009D0D RID: 40205
			UpdateServerInfomation,
			// Token: 0x04009D0E RID: 40206
			GetTagFromLoginServer,
			// Token: 0x04009D0F RID: 40207
			WaitStartUpProcess,
			// Token: 0x04009D10 RID: 40208
			SelectServer,
			// Token: 0x04009D11 RID: 40209
			SetDefaultTagFromSelectedServer,
			// Token: 0x04009D12 RID: 40210
			TryRecoverTag,
			// Token: 0x04009D13 RID: 40211
			SetNationCode,
			// Token: 0x04009D14 RID: 40212
			LoadPatcherString,
			// Token: 0x04009D15 RID: 40213
			SelectVoice,
			// Token: 0x04009D16 RID: 40214
			DisplayGameGrade,
			// Token: 0x04009D17 RID: 40215
			Localizing,
			// Token: 0x04009D18 RID: 40216
			CheckPCVersionUp,
			// Token: 0x04009D19 RID: 40217
			CheckCanStartPatch,
			// Token: 0x04009D1A RID: 40218
			InitObb,
			// Token: 0x04009D1B RID: 40219
			MakeDownloadList,
			// Token: 0x04009D1C RID: 40220
			MakeDownloadListForExtraAsset,
			// Token: 0x04009D1D RID: 40221
			MakeDownloadListForTutorialAsset,
			// Token: 0x04009D1E RID: 40222
			SelectPatchDownloadType,
			// Token: 0x04009D1F RID: 40223
			DownloadRequiredPatchFiles,
			// Token: 0x04009D20 RID: 40224
			Patch,
			// Token: 0x04009D21 RID: 40225
			GetTagFromSelectedLoginServer,
			// Token: 0x04009D22 RID: 40226
			StartGame
		}
	}
}
