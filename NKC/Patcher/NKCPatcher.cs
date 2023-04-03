using System;
using System.Collections;
using System.Collections.Generic;
using AssetBundles;
using Cs.Engine.Util;
using Cs.Logging;
using NKC.Publisher;
using NKC.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

namespace NKC.Patcher
{
	// Token: 0x02000884 RID: 2180
	public class NKCPatcher : MonoBehaviour, IPatcher
	{
		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x060056BD RID: 22205 RVA: 0x001A21EA File Offset: 0x001A03EA
		// (set) Token: 0x060056BE RID: 22206 RVA: 0x001A220B File Offset: 0x001A040B
		private float ProgressBarValue
		{
			get
			{
				if (!(this.sdProgressBar != null))
				{
					return 0f;
				}
				return this.sdProgressBar.value;
			}
			set
			{
				if (this.sdProgressBar != null)
				{
					this.sdProgressBar.value = value;
				}
			}
		}

		// Token: 0x1700108C RID: 4236
		// (set) Token: 0x060056BF RID: 22207 RVA: 0x001A2227 File Offset: 0x001A0427
		private string NoticeText
		{
			set
			{
				if (this.lbNoticeText != null)
				{
					this.lbNoticeText.text = value;
				}
			}
		}

		// Token: 0x1700108D RID: 4237
		// (get) Token: 0x060056C0 RID: 22208 RVA: 0x001A2243 File Offset: 0x001A0443
		// (set) Token: 0x060056C1 RID: 22209 RVA: 0x001A2264 File Offset: 0x001A0464
		private string ProgressText
		{
			get
			{
				if (!(this.lbProgressText != null))
				{
					return "";
				}
				return this.lbProgressText.text;
			}
			set
			{
				if (this.lbProgressText != null)
				{
					this.lbProgressText.text = value;
				}
			}
		}

		// Token: 0x1700108E RID: 4238
		// (get) Token: 0x060056C2 RID: 22210 RVA: 0x001A2280 File Offset: 0x001A0480
		// (set) Token: 0x060056C3 RID: 22211 RVA: 0x001A22A1 File Offset: 0x001A04A1
		private string DebugText
		{
			get
			{
				if (!(this.lbDebugProgressText != null))
				{
					return "";
				}
				return this.lbDebugProgressText.text;
			}
			set
			{
				if (this.lbDebugProgressText != null)
				{
					this.lbDebugProgressText.text = value;
				}
			}
		}

		// Token: 0x1700108F RID: 4239
		// (get) Token: 0x060056C4 RID: 22212 RVA: 0x001A22BD File Offset: 0x001A04BD
		// (set) Token: 0x060056C5 RID: 22213 RVA: 0x001A22DE File Offset: 0x001A04DE
		private string VersionText
		{
			get
			{
				if (!(this.lbVersionCode != null))
				{
					return "";
				}
				return this.lbVersionCode.text;
			}
			set
			{
				if (this.lbVersionCode != null)
				{
					this.lbVersionCode.text = value;
				}
			}
		}

		// Token: 0x17001090 RID: 4240
		// (get) Token: 0x060056C6 RID: 22214 RVA: 0x001A22FA File Offset: 0x001A04FA
		// (set) Token: 0x060056C7 RID: 22215 RVA: 0x001A231B File Offset: 0x001A051B
		private string AppVersionText
		{
			get
			{
				if (!(this.lbAppVersion != null))
				{
					return "";
				}
				return this.lbAppVersion.text;
			}
			set
			{
				if (this.lbAppVersion != null)
				{
					this.lbAppVersion.text = value;
				}
			}
		}

		// Token: 0x17001091 RID: 4241
		// (get) Token: 0x060056C8 RID: 22216 RVA: 0x001A2337 File Offset: 0x001A0537
		// (set) Token: 0x060056C9 RID: 22217 RVA: 0x001A2358 File Offset: 0x001A0558
		private string ProtocolVersionText
		{
			get
			{
				if (!(this.lbProtocolVersion != null))
				{
					return "";
				}
				return this.lbProtocolVersion.text;
			}
			set
			{
				if (this.lbProtocolVersion != null)
				{
					this.lbProtocolVersion.text = value;
				}
			}
		}

		// Token: 0x060056CA RID: 22218 RVA: 0x001A2374 File Offset: 0x001A0574
		private void Awake()
		{
			this.AppVersionText = NKCUtilString.GetAppVersionText();
			this.ProtocolVersionText = NKCUtilString.GetProtocolVersionText();
			NKCUtil.SetGameobjectActive(this.lbDebugProgressText, false);
			if (NKCPatcher.IsIntegrityCheckReserved())
			{
				NKCUtil.SetGameobjectActive(this.m_ctglIntegrityCheck, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_ctglIntegrityCheck, true);
				this.m_ctglIntegrityCheck.OnValueChanged.RemoveAllListeners();
				this.m_ctglIntegrityCheck.OnValueChanged.AddListener(new UnityAction<bool>(NKCPatcher.ReserveIntegrityCheck));
			}
			if (this.evtBackground != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(delegate(BaseEventData eventData)
				{
					this.m_bBGTouch = true;
				});
				this.evtBackground.triggers.Add(entry);
			}
			NKCUtil.SetGameobjectActive(this.evtBackground, false);
		}

		// Token: 0x060056CB RID: 22219 RVA: 0x001A2440 File Offset: 0x001A0640
		private void Start()
		{
			this.m_RectTransform = base.gameObject.GetComponentInChildren<RectTransform>();
			Vector3 localPosition = this.m_RectTransform.localPosition;
			localPosition.Set(0f, 0f, 0f);
			this.m_RectTransform.localPosition = localPosition;
		}

		// Token: 0x060056CC RID: 22220 RVA: 0x001A248C File Offset: 0x001A068C
		private void OnApplicationFocus(bool focus)
		{
			VideoPlayer videoPlayer = this.m_videoPlayer;
			if (videoPlayer == null)
			{
				return;
			}
			videoPlayer.SetDirectAudioMute(0, !focus);
		}

		// Token: 0x060056CD RID: 22221 RVA: 0x001A24A4 File Offset: 0x001A06A4
		private void OnIntegrityCheckProgress(int fileCount, int totalCount)
		{
			if (totalCount != 0)
			{
				float num = (float)fileCount / (float)totalCount;
				this.NoticeText = string.Format("{0} ({1:0.00%})", NKCStringTable.GetString("SI_DP_PATCHER_INTEGRITY_CHECK", false), num);
			}
		}

		// Token: 0x060056CE RID: 22222 RVA: 0x001A24DC File Offset: 0x001A06DC
		private void OnFileDownloadProgressTotal(long currentByte, long maxByte)
		{
			float progressBarValue = (float)currentByte / (float)maxByte;
			this.ProgressBarValue = progressBarValue;
			this.ProgressText = string.Format("{0:0.00%}", this.ProgressBarValue);
		}

		// Token: 0x060056CF RID: 22223 RVA: 0x001A2511 File Offset: 0x001A0711
		public void SetActive(bool active)
		{
			NKCUtil.SetGameobjectActive(this, active);
		}

		// Token: 0x17001092 RID: 4242
		// (get) Token: 0x060056D0 RID: 22224 RVA: 0x001A251A File Offset: 0x001A071A
		public bool PatchSuccess { get; } = 1;

		// Token: 0x17001093 RID: 4243
		// (get) Token: 0x060056D1 RID: 22225 RVA: 0x001A2522 File Offset: 0x001A0722
		public string ReasonOfFailure { get; } = string.Empty;

		// Token: 0x060056D2 RID: 22226 RVA: 0x001A252A File Offset: 0x001A072A
		public IEnumerator ProcessPatch()
		{
			this.DebugText = "";
			this.VersionText = NKCConnectionInfo.s_ServerType;
			this.ProgressText = "";
			while (Application.internetReachability == NetworkReachability.NotReachable)
			{
				yield return NKCPatcherManager.GetPatcherManager().WaitForOKBox(NKCUtilString.GET_STRING_PATCHER_WARNING, NKCUtilString.GET_STRING_DECONNECT_INTERNET, NKCUtilString.GET_STRING_RETRY);
			}
			string noticeTextProgress = NKCUtilString.GET_STRING_PATCHER_CHECKING_VERSION_INFORMATION;
			this.NoticeText = noticeTextProgress;
			Debug.Log("Begin ProcessPatch");
			NKCPublisherModule.Statistics.OnPatchStart();
			if (NKCPatchDownloader.Instance == null)
			{
				if (NKCDefineManager.DEFINE_EXTRA_ASSET() || NKCDefineManager.DEFINE_ZLONG_CHN())
				{
					LegacyPatchDownloader.InitInstance(new NKCPatchDownloader.OnError(NKCPatcherManager.GetPatcherManager().ShowError));
				}
				else
				{
					NKCPatchParallelDownloader.InitInstance(new NKCPatchDownloader.OnError(NKCPatcherManager.GetPatcherManager().ShowError));
				}
			}
			NKCPatchDownloader.Instance.onIntegrityCheckProgress = new NKCPatchDownloader.OnIntegrityCheckProgress(this.OnIntegrityCheckProgress);
			if (NKCDefineManager.DEFINE_USE_CHEAT())
			{
				NKCPatchUtility.ProcessPatchSkipTest(NKCPatchDownloader.Instance.LocalDownloadPath);
			}
			NKCPatchDownloader.Instance.StopBackgroundDownload();
			noticeTextProgress += ".";
			this.NoticeText = noticeTextProgress;
			while (!NKCPatchDownloader.Instance.IsInit)
			{
				yield return null;
			}
			Debug.Log("Begin VersionCheck");
			bool bIntegrityCheck = NKCPatcher.IsIntegrityCheckReserved();
			if (bIntegrityCheck)
			{
				noticeTextProgress = NKCStringTable.GetString("SI_DP_PATCHER_INTEGRITY_CHECK", false);
				this.NoticeText = noticeTextProgress;
				yield return null;
			}
			Debug.Log("integrityCheck Reserve Check End");
			noticeTextProgress += ".";
			this.NoticeText = noticeTextProgress;
			NKCPatchDownloader.Instance.InitCheckTime();
			NKCPatchDownloader.Instance.CheckVersion(new List<string>(AssetBundleManager.ActiveVariants), bIntegrityCheck);
			NKCPatcher.ReserveIntegrityCheck(false);
			while (NKCPatchDownloader.Instance.BuildCheckStatus == NKCPatchDownloader.BuildStatus.Unchecked)
			{
				yield return null;
			}
			NKCPublisherModule.Statistics.LogClientAction(NKCPublisherModule.NKCPMStatistics.eClientAction.Patch_VersionCheckComplete, 0, null);
			Debug.Log("End Version Check");
			noticeTextProgress += ".";
			this.NoticeText = noticeTextProgress;
			switch (NKCPatchDownloader.Instance.BuildCheckStatus)
			{
			case NKCPatchDownloader.BuildStatus.UpdateAvailable:
				yield return NKCPatcherManager.GetPatcherManager().WaitForOKCancel(NKCUtilString.GET_STRING_PATCHER_NOTICE, NKCUtilString.GET_STRING_PATCHER_CAN_UPDATE, NKCUtilString.GET_STRING_PATCHER_MOVE_TO_MARKET, NKCUtilString.GET_STRING_PATCHER_CONTINUE, new NKCPopupOKCancel.OnButton(NKCPatcherManager.GetPatcherManager().MoveToMarket));
				break;
			case NKCPatchDownloader.BuildStatus.RequireAppUpdate:
				NKCPatcherManager.GetPatcherManager().ShowUpdate();
				break;
			case NKCPatchDownloader.BuildStatus.Error:
				NKCPatcherManager.GetPatcherManager().ShowError(NKCPatchDownloader.Instance.ErrorString);
				yield break;
			}
			noticeTextProgress += ".";
			this.NoticeText = noticeTextProgress;
			while (NKCPatchDownloader.Instance.VersionCheckStatus == NKCPatchDownloader.VersionStatus.Unchecked)
			{
				yield return null;
			}
			noticeTextProgress += ".";
			this.NoticeText = noticeTextProgress;
			bool bDownload = false;
			NKCPatchDownloader.Instance.ProloguePlay = false;
			switch (NKCPatchDownloader.Instance.VersionCheckStatus)
			{
			case NKCPatchDownloader.VersionStatus.UpToDate:
				if (NKCDefineManager.DEFINE_SEMI_FULL_BUILD())
				{
					NKCPatchDownloader.Instance.DoWhenEndDownload();
				}
				break;
			case NKCPatchDownloader.VersionStatus.RequireDownload:
			{
				NKCPublisherModule.Statistics.LogClientAction(NKCPublisherModule.NKCPMStatistics.eClientAction.Patch_DownloadAvailable, 0, null);
				float num = (float)NKCPatchDownloader.Instance.TotalSize / 1048576f;
				if (NKCPatchUtility.IsPatchSkip())
				{
					NKCPatcher.<>c__DisplayClass46_0 CS$<>8__locals1 = new NKCPatcher.<>c__DisplayClass46_0();
					Debug.LogWarning("First run detected. skipping patch..");
					NKCPatchDownloader.Instance.ProloguePlay = true;
					string message = string.Format(NKCUtilString.GET_STRING_NOTICE_DOWNLOAD_ONE_PARAM, num);
					CS$<>8__locals1.m_bUserPermission = false;
					CS$<>8__locals1.bDownloadPatch = false;
					while (!CS$<>8__locals1.m_bUserPermission)
					{
						yield return NKCPatcherManager.GetPatcherManager().WaitForOKCancel(NKCUtilString.GET_STRING_PATCHER_WARNING, message, "", "", delegate
						{
							CS$<>8__locals1.bDownloadPatch = true;
							CS$<>8__locals1.m_bUserPermission = true;
						});
						if (!CS$<>8__locals1.m_bUserPermission)
						{
							yield return NKCPatcherManager.GetPatcherManager().WaitForOKCancel(NKCUtilString.GET_STRING_PATCHER_WARNING, NKCUtilString.GET_STRING_NOTICE_PLAY_WITHOUT_DOWNLOAD, "", "", delegate
							{
								CS$<>8__locals1.m_bUserPermission = true;
								CS$<>8__locals1.bDownloadPatch = false;
							});
						}
						else
						{
							yield return NKCPatcherManager.GetPatcherManager().WaitForOKCancel(NKCUtilString.GET_STRING_PATCHER_WARNING, NKCUtilString.GET_STRING_NOTICE_ASK_DOWNLOAD_IMMEDIATELY_OR_WITH_PROLOGUE, "", "", delegate
							{
								NKCPatchDownloader.Instance.ProloguePlay = false;
							});
						}
					}
					if (!CS$<>8__locals1.bDownloadPatch)
					{
						break;
					}
					CS$<>8__locals1 = null;
					message = null;
				}
				else
				{
					NKCPatcher.<>c__DisplayClass46_1 CS$<>8__locals2 = new NKCPatcher.<>c__DisplayClass46_1();
					string message = string.Format(NKCUtilString.GET_STRING_NOTICE_DOWNLOAD_ONE_PARAM, num);
					CS$<>8__locals2.m_bUserPermission = false;
					while (!CS$<>8__locals2.m_bUserPermission)
					{
						yield return NKCPatcherManager.GetPatcherManager().WaitForOKCancel(NKCUtilString.GET_STRING_PATCHER_WARNING, message, "", "", delegate
						{
							CS$<>8__locals2.m_bUserPermission = true;
						});
						if (!CS$<>8__locals2.m_bUserPermission)
						{
							yield return NKCPatcherManager.GetPatcherManager().WaitForOKCancel(NKCUtilString.GET_STRING_PATCHER_WARNING, NKCStringTable.GetString("SI_DP_PATCHER_QUIT_CONFIRM", false), "", "", new NKCPopupOKCancel.OnButton(Application.Quit));
						}
					}
					CS$<>8__locals2 = null;
					message = null;
				}
				NKCAdjustManager.OnCustomEvent("02_downLoad_start");
				NKCPatchDownloader.Instance.StartFileDownload();
				NKCPublisherModule.Statistics.LogClientAction(NKCPublisherModule.NKCPMStatistics.eClientAction.Patch_DownloadStart, 0, null);
				this.PlayBGMovie();
				bDownload = true;
				break;
			}
			case NKCPatchDownloader.VersionStatus.Downloading:
				bDownload = true;
				break;
			case NKCPatchDownloader.VersionStatus.Error:
				NKCPatcherManager.GetPatcherManager().ShowError(NKCPatchDownloader.Instance.ErrorString);
				yield break;
			}
			noticeTextProgress += ".";
			this.NoticeText = noticeTextProgress;
			yield return null;
			if (!NKCPatchDownloader.Instance.ProloguePlay && NKCPatchDownloader.Instance.DownloadStatus == NKCPatchDownloader.PatchDownloadStatus.Downloading)
			{
				this.NoticeText = NKCUtilString.GET_STRING_PATCHER_DOWNLOADING;
				NKCUtil.SetGameobjectActive(this.lbCanDownloadBackground, NKCPatchDownloader.Instance.BackgroundDownloadAvailble);
				if (NKCPatchDownloader.Instance.BackgroundDownloadAvailble)
				{
					NKCUtil.SetLabelText(this.lbCanDownloadBackground, NKCUtilString.GET_STRING_PATCHER_CAN_BACKGROUND_DOWNLOAD);
				}
				while (NKCPatchDownloader.Instance.DownloadStatus == NKCPatchDownloader.PatchDownloadStatus.Downloading)
				{
					this.OnFileDownloadProgressTotal(NKCPatchDownloader.Instance.CurrentSize, NKCPatchDownloader.Instance.TotalSize);
					yield return null;
				}
				Debug.Log("PatchLoop finished, patcherStatus " + NKCPatchDownloader.Instance.DownloadStatus.ToString());
				switch (NKCPatchDownloader.Instance.DownloadStatus)
				{
				case NKCPatchDownloader.PatchDownloadStatus.Idle:
					NKCPatcherManager.GetPatcherManager().ShowError(NKCStringTable.GetString("SI_DP_PATCHER_DOWNLOAD_ERROR", new object[]
					{
						""
					}));
					break;
				case NKCPatchDownloader.PatchDownloadStatus.UserCancel:
					NKCPatcherManager.GetPatcherManager().ShowError("User Canceled");
					break;
				case NKCPatchDownloader.PatchDownloadStatus.Finished:
					Debug.Log("Download finished");
					NKCPublisherModule.Statistics.LogClientAction(NKCPublisherModule.NKCPMStatistics.eClientAction.Patch_DownloadComplete, 0, null);
					this.NoticeText = NKCUtilString.GET_STRING_PATCHER_FINISHING_PATCHPROCESS;
					break;
				case NKCPatchDownloader.PatchDownloadStatus.Error:
					NKCPatcherManager.GetPatcherManager().ShowError(NKCStringTable.GetString("SI_DP_PATCHER_DOWNLOAD_ERROR", new object[]
					{
						NKCPatchDownloader.Instance.ErrorString
					}));
					break;
				case NKCPatchDownloader.PatchDownloadStatus.UpdateRequired:
					NKCPatcherManager.GetPatcherManager().ShowUpdate();
					break;
				}
			}
			noticeTextProgress += ".";
			this.NoticeText = noticeTextProgress;
			NKCAdjustManager.OnCustomEvent("03_downLoad_complete");
			noticeTextProgress += ".";
			this.NoticeText = noticeTextProgress;
			this.ProgressBarValue = 1f;
			this.ProgressText = "100%";
			if (!NKCPatchDownloader.Instance.ProloguePlay && bDownload && this.lbCanDownloadBackground != null)
			{
				this.lbCanDownloadBackground.text = NKCStringTable.GetString("SI_DP_PATCHER_TOUCH_TO_START", false);
				NKCUtil.SetGameobjectActive(this.lbCanDownloadBackground, true);
				NKCUtil.SetGameobjectActive(this.sdProgressBar, false);
				NKCUtil.SetGameobjectActive(this.lbNoticeText, false);
				NKCUtil.SetGameobjectActive(this.lbProgressText, false);
				float fTime = 0f;
				if (this.evtBackground != null)
				{
					NKCUtil.SetGameobjectActive(this.evtBackground, true);
					this.m_bBGTouch = false;
					while (!this.m_bBGTouch)
					{
						this.lbCanDownloadBackground.color = new Color(1f, 1f, 1f, (Mathf.Cos(fTime * 3f) + 1f) * 0.5f);
						fTime += Time.unscaledDeltaTime;
						yield return null;
						if (fTime > 2f && Input.anyKey)
						{
							this.m_bBGTouch = true;
						}
					}
				}
			}
			this.NoticeText = NKCUtilString.GET_STRING_PATCHER_INITIALIZING;
			yield return null;
			yield return AssetBundleManager.Initialize();
			Debug.Log("Manifest Load Finished");
			NKCAdjustManager.OnCustomEvent("04_loading_start");
			NKCPublisherModule.Statistics.LogClientAction(NKCPublisherModule.NKCPMStatistics.eClientAction.Patch_MoveToMainScene, 0, null);
			NKCPublisherModule.Statistics.OnPatchEnd();
			if (NKCDefineManager.DEFINE_CHECKVERSION())
			{
				if (ContentsVersionChecker.VersionAckReceived)
				{
					this.StopBG();
				}
				else
				{
					float fTime = 0f;
					int versionRequestRetryCount = 0;
					while (!ContentsVersionChecker.VersionAckReceived)
					{
						fTime -= Time.deltaTime;
						if (fTime <= 0f)
						{
							NKCPatcherManager.GetPatcherManager().ShowRequestTimer(true);
							string serviceIP = NKCConnectionInfo.ServiceIP;
							Log.Debug("Trying to retrieve server tag from " + serviceIP, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatcher.cs", 555);
							yield return ContentsVersionChecker.GetVersion(serviceIP, -1, false);
							fTime = ContentsVersionChecker.RetryInterval;
							versionRequestRetryCount++;
						}
						if (versionRequestRetryCount >= 1)
						{
							break;
						}
						yield return null;
					}
					if (ContentsVersionChecker.Ack != null)
					{
						NKCPublisherModule.Statistics.LogClientAction(NKCPublisherModule.NKCPMStatistics.eClientAction.Patch_TagProvided, 0, null);
					}
					NKCPatcherManager.GetPatcherManager().ShowRequestTimer(false);
					this.StopBG();
				}
			}
			else
			{
				this.StopBG();
			}
			yield break;
		}

		// Token: 0x060056D3 RID: 22227 RVA: 0x001A2539 File Offset: 0x001A0739
		private void PlayBGMovie()
		{
			base.StartCoroutine(this.PlayBG());
		}

		// Token: 0x060056D4 RID: 22228 RVA: 0x001A2548 File Offset: 0x001A0748
		private IEnumerator PlayBG()
		{
			if (this.m_videoPlayer == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objFallbackBG, true);
				yield break;
			}
			string rawFilePath = AssetBundleManager.GetRawFilePath("Movie/PatchMovieHiRes.mp4");
			if (string.IsNullOrEmpty(rawFilePath) || NKCPatchDownloader.Instance.IsFileWillDownloaded("ASSET_RAW/Movie/PatchMovieHiRes.mp4"))
			{
				if (string.IsNullOrEmpty(rawFilePath))
				{
					Debug.Log("Hi-res movie not found. playing default movie");
				}
				else
				{
					Debug.Log("Hi-res movie found but will updated. playing default movie");
				}
				this.m_videoPlayer.clip = Resources.Load<VideoClip>("PatchMovie");
				if (this.m_videoPlayer.clip == null)
				{
					NKCUtil.SetGameobjectActive(this.m_objFallbackBG, true);
					yield break;
				}
			}
			else
			{
				Debug.Log("playing hi-res movie");
				this.m_videoPlayer.url = rawFilePath;
			}
			bool flag = PlayerPrefs.GetInt("NKM_LOCAL_SAVE_GAME_OPTION_SOUND_MUTE", 0) == 1;
			float num = 1f;
			string @string = PlayerPrefs.GetString("NKM_LOCAL_SAVE_GAME_OPTION_SOUND_VOLUMES", "");
			if (@string != "")
			{
				int num2 = 3;
				string[] array = @string.Split(new char[]
				{
					':'
				});
				int num3;
				if (array.Length > num2 && int.TryParse(array[num2], out num3))
				{
					num = (float)num3 / 100f;
				}
			}
			float volume = flag ? 0f : num;
			this.m_videoPlayer.renderMode = VideoRenderMode.CameraFarPlane;
			this.m_videoPlayer.enabled = true;
			this.m_videoPlayer.isLooping = true;
			this.m_videoPlayer.audioOutputMode = VideoAudioOutputMode.Direct;
			this.m_videoPlayer.EnableAudioTrack(0, true);
			this.m_videoPlayer.controlledAudioTrackCount = 1;
			this.m_videoPlayer.SetDirectAudioVolume(0, volume);
			this.m_videoPlayer.SetDirectAudioMute(0, !Application.isFocused);
			this.m_videoPlayer.playbackSpeed = 1f;
			this.m_videoPlayer.Prepare();
			while (!this.m_videoPlayer.isPrepared)
			{
				yield return null;
			}
			this.m_videoPlayer.Play();
			Debug.Log("[VideoPlayer] Play");
			NKCUtil.SetGameobjectActive(this.m_objFallbackBG, false);
			yield break;
		}

		// Token: 0x060056D5 RID: 22229 RVA: 0x001A2557 File Offset: 0x001A0757
		private void StopBG()
		{
			Debug.Log("[VideoPlayer] StopBG");
			if (this.m_videoPlayer != null)
			{
				this.m_videoPlayer.Stop();
			}
			NKCUtil.SetGameobjectActive(this.m_objFallbackBG, true);
		}

		// Token: 0x060056D6 RID: 22230 RVA: 0x001A2588 File Offset: 0x001A0788
		private void OnDestroy()
		{
			Debug.Log(string.Format("[VideoPlayer] OnDestroy VideoPlayer[{0}]", this.m_videoPlayer));
		}

		// Token: 0x060056D7 RID: 22231 RVA: 0x001A259F File Offset: 0x001A079F
		public static void ReserveIntegrityCheck(bool value)
		{
			if (value)
			{
				PlayerPrefs.SetInt("PatchIntegrityCheck", 1);
			}
			else
			{
				PlayerPrefs.DeleteKey("PatchIntegrityCheck");
			}
			PlayerPrefs.Save();
		}

		// Token: 0x060056D8 RID: 22232 RVA: 0x001A25C0 File Offset: 0x001A07C0
		public static bool IsIntegrityCheckReserved()
		{
			return PlayerPrefs.GetInt("PatchIntegrityCheck", 0) == 1;
		}

		// Token: 0x040044DA RID: 17626
		public Slider sdProgressBar;

		// Token: 0x040044DB RID: 17627
		public Text lbNoticeText;

		// Token: 0x040044DC RID: 17628
		public Text lbProgressText;

		// Token: 0x040044DD RID: 17629
		public Text lbDebugProgressText;

		// Token: 0x040044DE RID: 17630
		public Text lbVersionCode;

		// Token: 0x040044DF RID: 17631
		public Text lbAppVersion;

		// Token: 0x040044E0 RID: 17632
		public Text lbProtocolVersion;

		// Token: 0x040044E1 RID: 17633
		public Text lbCanDownloadBackground;

		// Token: 0x040044E2 RID: 17634
		[Header("비디오 플레이어")]
		public VideoPlayer m_videoPlayer;

		// Token: 0x040044E3 RID: 17635
		[Header("비디오 플레이 안 될때 대비용 배경")]
		public GameObject m_objFallbackBG;

		// Token: 0x040044E4 RID: 17636
		[Header("파일 무결섬 검사 버튼")]
		public NKCUIComToggle m_ctglIntegrityCheck;

		// Token: 0x040044E5 RID: 17637
		[Header("배경 터치")]
		public EventTrigger evtBackground;

		// Token: 0x040044E6 RID: 17638
		private bool m_bBGTouch;

		// Token: 0x040044E7 RID: 17639
		private RectTransform m_RectTransform;

		// Token: 0x040044EA RID: 17642
		public const string PATCH_INTERGRITY_CHECK_KEY = "PatchIntegrityCheck";
	}
}
