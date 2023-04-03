using System;
using System.Collections.Generic;
using Cs.Logging;
using NKC.Localization;
using NKC.Patcher;
using NKC.Publisher;
using NKC.Templet;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009B5 RID: 2485
	public class NKCUILoginViewer : NKCUIBase
	{
		// Token: 0x17001212 RID: 4626
		// (get) Token: 0x060068BD RID: 26813 RVA: 0x0021DC05 File Offset: 0x0021BE05
		public override NKCUIManager.eUIUnloadFlag UnloadFlag
		{
			get
			{
				return NKCUIManager.eUIUnloadFlag.DEFAULT;
			}
		}

		// Token: 0x060068BE RID: 26814 RVA: 0x0021DC08 File Offset: 0x0021BE08
		public static NKCUIManager.LoadedUIData OpenNewInstanceAsync()
		{
			if (!NKCUIManager.IsValid(NKCUILoginViewer.s_LoadedUIData))
			{
				NKCUILoginViewer.s_LoadedUIData = NKCUIManager.OpenNewInstanceAsync<NKCUILoginViewer>("ab_ui_login_loc", "NUF_LOGIN_PREFAB", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUILoginViewer.CleanupInstance));
			}
			return NKCUILoginViewer.s_LoadedUIData;
		}

		// Token: 0x17001213 RID: 4627
		// (get) Token: 0x060068BF RID: 26815 RVA: 0x0021DC3C File Offset: 0x0021BE3C
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUILoginViewer.s_LoadedUIData != null && NKCUILoginViewer.s_LoadedUIData.IsUIOpen;
			}
		}

		// Token: 0x17001214 RID: 4628
		// (get) Token: 0x060068C0 RID: 26816 RVA: 0x0021DC51 File Offset: 0x0021BE51
		public static bool IsInstanceLoaded
		{
			get
			{
				return NKCUILoginViewer.s_LoadedUIData != null && NKCUILoginViewer.s_LoadedUIData.IsLoadComplete;
			}
		}

		// Token: 0x060068C1 RID: 26817 RVA: 0x0021DC66 File Offset: 0x0021BE66
		public static NKCUILoginViewer GetInstance()
		{
			if (NKCUILoginViewer.s_LoadedUIData != null && NKCUILoginViewer.s_LoadedUIData.IsLoadComplete)
			{
				return NKCUILoginViewer.s_LoadedUIData.GetInstance<NKCUILoginViewer>();
			}
			return null;
		}

		// Token: 0x060068C2 RID: 26818 RVA: 0x0021DC87 File Offset: 0x0021BE87
		public static void CleanupInstance()
		{
			NKCUILoginViewer.s_LoadedUIData = null;
		}

		// Token: 0x17001215 RID: 4629
		// (get) Token: 0x060068C3 RID: 26819 RVA: 0x0021DC8F File Offset: 0x0021BE8F
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_LOGIN;
			}
		}

		// Token: 0x060068C4 RID: 26820 RVA: 0x0021DC96 File Offset: 0x0021BE96
		public override void OnBackButton()
		{
			this.OpenQuitApplicationPopup();
		}

		// Token: 0x17001216 RID: 4630
		// (get) Token: 0x060068C5 RID: 26821 RVA: 0x0021DC9E File Offset: 0x0021BE9E
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001217 RID: 4631
		// (get) Token: 0x060068C6 RID: 26822 RVA: 0x0021DCA1 File Offset: 0x0021BEA1
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x17001218 RID: 4632
		// (get) Token: 0x060068C7 RID: 26823 RVA: 0x0021DCA4 File Offset: 0x0021BEA4
		public NKM_USER_AUTH_LEVEL AuthLevel
		{
			get
			{
				return this.m_NKM_USER_AUTH_LEVEL;
			}
		}

		// Token: 0x060068C8 RID: 26824 RVA: 0x0021DCAC File Offset: 0x0021BEAC
		public void UpdateLoginMsgUI()
		{
			if (this.m_NKCUILoginViewerMsg != null)
			{
				this.m_NKCUILoginViewerMsg.ForceUpdateMsg();
			}
		}

		// Token: 0x060068C9 RID: 26825 RVA: 0x0021DCC7 File Offset: 0x0021BEC7
		public NKCUILoginViewer GetLoginDevMenu()
		{
			return this;
		}

		// Token: 0x060068CA RID: 26826 RVA: 0x0021DCCA File Offset: 0x0021BECA
		private void OnDestroy()
		{
			if (this.m_NKCAssetInstanceDataMessage != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_NKCAssetInstanceDataMessage);
			}
			this.m_NKCAssetInstanceDataMessage = null;
			this.m_NKCUILoginViewerMsg = null;
		}

		// Token: 0x060068CB RID: 26827 RVA: 0x0021DCF0 File Offset: 0x0021BEF0
		private void LoadBackgroundTemplet()
		{
			if (this.m_objBack != null)
			{
				return;
			}
			if (this.m_BackResource == null)
			{
				NKCLoginBackgroundTemplet currentBackgroundTemplet = NKCLoginBackgroundTemplet.GetCurrentBackgroundTemplet();
				if (currentBackgroundTemplet != null && NKCAssetResourceManager.IsBundleExists(currentBackgroundTemplet.BundleName, currentBackgroundTemplet.AssetName))
				{
					this.m_BackResource = NKCAssetResourceManager.OpenResource<GameObject>(currentBackgroundTemplet.BundleName, currentBackgroundTemplet.AssetName, false, null);
				}
				if (this.m_BackResource == null || this.m_BackResource.GetAsset<GameObject>() == null)
				{
					if (this.m_BackResource != null)
					{
						NKCAssetResourceManager.CloseResource(this.m_BackResource);
					}
					this.m_BackResource = NKCAssetResourceManager.OpenResource<GameObject>("AB_LOGIN_SCREEN_NKM_LS_202001_OPEN", "AB_LOGIN_SCREEN_NKM_LS_202001_OPEN", false, null);
				}
			}
			this.m_objBack = UnityEngine.Object.Instantiate<GameObject>(this.m_BackResource.GetAsset<GameObject>(), NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIMidCanvas));
			this.m_objBack.transform.localPosition = Vector3.zero;
		}

		// Token: 0x060068CC RID: 26828 RVA: 0x0021DDC0 File Offset: 0x0021BFC0
		private void LoadPrefabList()
		{
			this.ReleaseEnabledPrefabs();
			foreach (NKCLoginBackgroundTemplet nkcloginBackgroundTemplet in NKCLoginBackgroundTemplet.GetEnabledPrefabList())
			{
				NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>(nkcloginBackgroundTemplet.BundleName, nkcloginBackgroundTemplet.AssetName, false, base.transform);
				if (nkcassetInstanceData == null)
				{
					Log.Error(string.Format("[NKCUILoginViewer] LoadPrefab failed  id[{0}] asset[{1}/{2}]", nkcloginBackgroundTemplet.ID, nkcloginBackgroundTemplet.AssetName, nkcloginBackgroundTemplet.BundleName), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUILoginViewer.cs", 233);
				}
				else
				{
					this.m_instancedPrefabList.Add(nkcassetInstanceData);
				}
			}
		}

		// Token: 0x060068CD RID: 26829 RVA: 0x0021DE6C File Offset: 0x0021C06C
		private void ReleaseEnabledPrefabs()
		{
			foreach (NKCAssetInstanceData nkcassetInstanceData in this.m_instancedPrefabList)
			{
				nkcassetInstanceData.Close();
			}
			this.m_instancedPrefabList.Clear();
		}

		// Token: 0x060068CE RID: 26830 RVA: 0x0021DEC8 File Offset: 0x0021C0C8
		private void RescaleBack()
		{
			if (this.m_objBack == null)
			{
				return;
			}
			RectTransform component = this.m_objBack.GetComponent<RectTransform>();
			if (component != null)
			{
				NKCCamera.RescaleRectToCameraFrustrum(component, NKCCamera.GetCamera(), Vector2.zero, NKCScenManager.GetScenManager().Get_SCEN_LOGIN().GetCameraDistance(), NKCCamera.FitMode.FitAuto, NKCCamera.ScaleMode.Scale);
			}
		}

		// Token: 0x060068CF RID: 26831 RVA: 0x0021DF1C File Offset: 0x0021C11C
		public void InitUI()
		{
			if (this.m_comBtnServer1STSave != null)
			{
				this.m_comBtnServer1STSave.PointerClick.RemoveAllListeners();
				this.m_comBtnServer1STSave.PointerClick.AddListener(new UnityAction(this.OnSaveServer1ST));
			}
			if (this.m_comBtnServer1STLoad != null)
			{
				this.m_comBtnServer1STLoad.PointerClick.RemoveAllListeners();
				this.m_comBtnServer1STLoad.PointerClick.AddListener(new UnityAction(this.OnLoadServer1ST));
			}
			if (this.m_comBtnServer2NDSave != null)
			{
				this.m_comBtnServer2NDSave.PointerClick.RemoveAllListeners();
				this.m_comBtnServer2NDSave.PointerClick.AddListener(new UnityAction(this.OnSaveServer2ND));
			}
			if (this.m_comBtnServer2NDLoad != null)
			{
				this.m_comBtnServer2NDLoad.PointerClick.RemoveAllListeners();
				this.m_comBtnServer2NDLoad.PointerClick.AddListener(new UnityAction(this.OnLoadServer2ND));
			}
			if (this.m_comBtnServer3RDSave != null)
			{
				this.m_comBtnServer3RDSave.PointerClick.RemoveAllListeners();
				this.m_comBtnServer3RDSave.PointerClick.AddListener(new UnityAction(this.OnSaveServer3RD));
			}
			if (this.m_comBtnServer3RDLoad != null)
			{
				this.m_comBtnServer3RDLoad.PointerClick.RemoveAllListeners();
				this.m_comBtnServer3RDLoad.PointerClick.AddListener(new UnityAction(this.OnLoadServer3RD));
			}
			if (this.m_comBtnLogin != null)
			{
				this.m_comBtnLogin.PointerClick.RemoveAllListeners();
				this.m_comBtnLogin.PointerClick.AddListener(new UnityAction(this.OnLogin));
			}
			if (this.NUF_LOGIN_NOTICE != null)
			{
				if (NKCPublisherModule.IsZlongPublished())
				{
					NKCUtil.SetGameobjectActive(this.NUF_LOGIN_NOTICE.gameObject, false);
					NKCUtil.SetGameobjectActive(this.NUF_LOGIN_NOTICE.gameObject, true);
					this.NUF_LOGIN_NOTICE.PointerClick.RemoveAllListeners();
					this.NUF_LOGIN_NOTICE.PointerClick.AddListener(new UnityAction(this.OnClickNoticeButton));
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.NUF_LOGIN_NOTICE.gameObject, false);
				}
			}
			if (NKCPublisherModule.IsPublisherNoneType())
			{
				if (this.m_comBtnChangeAccount != null)
				{
					this.m_comBtnChangeAccount.PointerClick.RemoveAllListeners();
					this.m_comBtnChangeAccount.PointerClick.AddListener(new UnityAction(this.OnChangeAccountDev));
				}
			}
			else
			{
				NKCUtil.SetImageSprite(this.m_NUF_LOGIN_DEV_CHANGE_ACCOUNT_BUTTON_BG, NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY), false);
				NKCUtil.SetLabelText(this.m_NUF_LOGIN_DEV_CHANGE_ACCOUNT_BUTTON_TEXT, NKCUtilString.GET_STRING_TOY_LOGIN_NO_AUTH);
				NKCUtil.SetLabelTextColor(this.m_NUF_LOGIN_DEV_CHANGE_ACCOUNT_BUTTON_TEXT, Color.black);
			}
			NKCUtil.SetButtonClickDelegate(this.m_comBtnPlaySingle, new UnityAction(this.OnPlaySingle));
			if (this.m_comBtnCutscen != null)
			{
				this.m_comBtnCutscen.PointerClick.RemoveAllListeners();
				this.m_comBtnCutscen.PointerClick.AddListener(new UnityAction(this.OnCutScenSim));
			}
			if (this.m_comBtnVoiceList != null)
			{
				this.m_comBtnVoiceList.PointerClick.RemoveAllListeners();
				this.m_comBtnVoiceList.PointerClick.AddListener(new UnityAction(this.OnVoiceList));
			}
			if (this.m_comBtnLanguageSelect != null)
			{
				this.m_comBtnLanguageSelect.PointerClick.RemoveAllListeners();
				this.m_comBtnLanguageSelect.PointerClick.AddListener(new UnityAction(this.OnLanguageSelect));
			}
			NKCUtil.SetButtonClickDelegate(this.m_comBtnMoveToPatch, new UnityAction(this.OnMoveToPatch));
			NKCUtil.SetButtonClickDelegate(this.m_comBtnPatchSkipTest, new UnityAction(this.OnPatchSkipTest));
			NKCUtil.SetGameobjectActive(this.m_comBtnPatchSkipTest, NKCDefineManager.DEFINE_PATCH_SKIP());
			if (this.m_NUF_LOGIN_SCREEN != null)
			{
				EventTrigger eventTrigger = this.m_NUF_LOGIN_SCREEN.GetComponent<EventTrigger>();
				if (eventTrigger == null)
				{
					eventTrigger = this.m_NUF_LOGIN_SCREEN.AddComponent<EventTrigger>();
				}
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(delegate(BaseEventData data)
				{
					this.TouchLogin();
				});
				eventTrigger.triggers.Clear();
				eventTrigger.triggers.Add(entry);
			}
			if (this.m_NUF_LOGIN_DEV_LOGIN_AUTH_LEVEL != null)
			{
				this.m_NUF_LOGIN_DEV_LOGIN_AUTH_LEVEL.PointerClick.RemoveAllListeners();
				this.m_NUF_LOGIN_DEV_LOGIN_AUTH_LEVEL.PointerClick.AddListener(new UnityAction(this.OnClickAuthLevelChange));
			}
			string text = string.Format("{0} {1}", NKCUtilString.GetAppVersionText(), NKCConnectionInfo.s_ServerType);
			if (!string.IsNullOrEmpty(NKCUtil.PatchVersion))
			{
				string[] array = NKCUtil.PatchVersion.Split(new char[]
				{
					'_'
				});
				if (array.Length != 0)
				{
					text = text + " A." + array[array.Length - 1];
				}
			}
			if (!string.IsNullOrEmpty(NKCUtil.PatchVersionEA))
			{
				string[] array2 = NKCUtil.PatchVersionEA.Split(new char[]
				{
					'_'
				});
				if (array2.Length != 0)
				{
					text = text + " E." + array2[array2.Length - 1];
				}
			}
			NKCUtil.SetLabelText(this.m_lbVersion, text);
			this.LoadBackgroundTemplet();
			this.LoadPrefabList();
			base.gameObject.SetActive(false);
			if (this.m_NUF_LOGIN_DEV_SERVER_LIST != null)
			{
				this.m_NUF_LOGIN_DEV_SERVER_LIST_DROPDOWN = this.m_NUF_LOGIN_DEV_SERVER_LIST.transform.GetComponentInChildren<Dropdown>();
				if (this.m_NUF_LOGIN_DEV_SERVER_LIST_DROPDOWN != null)
				{
					this.m_NUF_LOGIN_DEV_SERVER_LIST_DROPDOWN.onValueChanged.AddListener(delegate(int <p0>)
					{
						this.OnServerListDropDown(this.m_NUF_LOGIN_DEV_SERVER_LIST_DROPDOWN);
					});
				}
			}
			if (!NKCDefineManager.DEFINE_SERVICE())
			{
				this.m_NUF_LOGIN_DEV_SERVER_LIST.SetActive(!NKCDefineManager.DEFINE_SELECT_SERVER());
			}
			else
			{
				this.m_NUF_LOGIN_DEV_SERVER_LIST.SetActive(false);
				this.m_NUF_LOGIN_DEV_CUTSCEN.SetActive(false);
				this.m_NUF_LOGIN_DEV_VOICE_LIST.SetActive(false);
				NKCUtil.SetGameobjectActive(this.m_comBtnLanguageSelect, false);
				NKCUtil.SetGameobjectActive(this.m_comBtnMoveToPatch, false);
				NKCUtil.SetGameobjectActive(this.m_comBtnPatchSkipTest, false);
			}
			if (this.m_NUF_LOGIN_DEV_LOGIN_AUTH_LEVEL != null)
			{
				NKCUtil.SetGameobjectActive(this.m_NUF_LOGIN_DEV_LOGIN_AUTH_LEVEL.gameObject, false);
			}
			if (NKCPublisherModule.IsPCBuild())
			{
				NKCUtil.SetGameobjectActive(this.m_NUF_LOGIN_DEV_CHANGE_ACCOUNT_BUTTON, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NUF_LOGIN_DEV_CHANGE_ACCOUNT_BUTTON, !NKCPublisherModule.IsPublisherNoneType());
			}
			if (this.m_csbtnPcQR != null)
			{
				if (NKCPublisherModule.PublisherType == NKCPublisherModule.ePublisherType.Zlong)
				{
					if (NKCPublisherModule.Auth.CheckNeedToCheckEnableQR_AfterPubLogin())
					{
						NKCUtil.SetGameobjectActive(this.m_csbtnPcQR, false);
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_csbtnPcQR, NKCPublisherModule.Auth.CheckEnableQR_Login());
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_csbtnPcQR, false);
				}
				this.m_csbtnPcQR.PointerClick.RemoveAllListeners();
				this.m_csbtnPcQR.PointerClick.AddListener(new UnityAction(this.OnClickPcQR));
			}
		}

		// Token: 0x060068D0 RID: 26832 RVA: 0x0021E578 File Offset: 0x0021C778
		private void OnClickPcQR()
		{
			NKMPopUpBox.OpenWaitBox(0f, "");
			NKCPublisherModule.Auth.QR_Login(new NKCPublisherModule.OnComplete(this.OnCompleteQR_Login));
		}

		// Token: 0x060068D1 RID: 26833 RVA: 0x0021E59F File Offset: 0x0021C79F
		private void OnCompleteQR_Login(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
		{
			NKCPublisherModule.CheckError(resultCode, additionalError, true, null, false);
		}

		// Token: 0x060068D2 RID: 26834 RVA: 0x0021E5AC File Offset: 0x0021C7AC
		private void OnClickAuthLevelChange()
		{
			int num = Enum.GetNames(typeof(NKM_USER_AUTH_LEVEL)).Length;
			if (this.m_NKM_USER_AUTH_LEVEL >= (NKM_USER_AUTH_LEVEL)num)
			{
				this.m_NKM_USER_AUTH_LEVEL = NKM_USER_AUTH_LEVEL.NORMAL_USER;
			}
			else
			{
				this.m_NKM_USER_AUTH_LEVEL += 1;
			}
			this.ApplyNowAuthLevelToText();
		}

		// Token: 0x060068D3 RID: 26835 RVA: 0x0021E5F3 File Offset: 0x0021C7F3
		private void OnClickNoticeButton()
		{
			NKCPublisherModule.Notice.OpenNotice(null);
		}

		// Token: 0x060068D4 RID: 26836 RVA: 0x0021E600 File Offset: 0x0021C800
		public void Open()
		{
			this.RescaleBack();
			base.gameObject.SetActive(true);
			this.m_fUpdateTime = 0f;
			this.LoadLastEditServerAddress();
			Log.Debug("[Login] Open PublisherModule[" + NKCPublisherModule.PublisherType.ToString() + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUILoginViewer.cs", 547);
			if (NKCPublisherModule.IsPublisherNoneType())
			{
				if (PlayerPrefs.HasKey("NKM_LOCAL_SAVE_KEY_LOGIN_ID_STRING"))
				{
					this.m_objNUF_LOGIN_DEV_LOGIN.SetActive(false);
					this.m_bShowUpLoginPanel = false;
					NKCUtil.SetGameobjectActive(this.m_lbNUF_LOGIN_UPDATE_Text, true);
					Log.Debug(string.Concat(new string[]
					{
						"[Login] HasKey[",
						"NKM_LOCAL_SAVE_KEY_LOGIN_ID_STRING".ToString(),
						"] Value[",
						PlayerPrefs.GetString("NKM_LOCAL_SAVE_KEY_LOGIN_ID_STRING"),
						"]"
					}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUILoginViewer.cs", 557);
				}
				else
				{
					this.m_objNUF_LOGIN_DEV_LOGIN.SetActive(true);
					this.m_bShowUpLoginPanel = true;
					NKCUtil.SetGameobjectActive(this.m_lbNUF_LOGIN_UPDATE_Text, false);
					Log.Debug("[Login] NUF_LOGIN_DEV_LOGIN active", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUILoginViewer.cs", 567);
					Log.Debug(string.Concat(new string[]
					{
						"[Login] HasKey[",
						"NKM_LOCAL_SAVE_KEY_LOGIN_ID_STRING".ToString(),
						"] Value[",
						PlayerPrefs.GetString("NKM_LOCAL_SAVE_KEY_LOGIN_ID_STRING"),
						"]"
					}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUILoginViewer.cs", 568);
				}
			}
			else
			{
				this.m_objNUF_LOGIN_DEV_LOGIN.SetActive(false);
				this.m_bShowUpLoginPanel = false;
			}
			if (NKCPublisherModule.IsPublisherNoneType())
			{
				NKCUtil.SetGameobjectActive(this.m_NUF_LOGIN_DEV_CHANGE_ACCOUNT_BUTTON, true);
				NKCUtil.SetGameobjectActive(this.m_NUF_LOGIN_DEV_CUTSCEN, true);
				NKCUtil.SetGameobjectActive(this.m_NUF_LOGIN_DEV_VOICE_LIST, true);
				NKCUtil.SetGameobjectActive(this.m_comBtnLanguageSelect, true);
				NKCUtil.SetGameobjectActive(this.m_comBtnMoveToPatch, true);
				NKCUtil.SetGameobjectActive(this.m_comBtnPatchSkipTest, true);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NUF_LOGIN_DEV_CUTSCEN, false);
				NKCUtil.SetGameobjectActive(this.m_NUF_LOGIN_DEV_VOICE_LIST, false);
				NKCUtil.SetGameobjectActive(this.m_comBtnLanguageSelect, false);
				NKCUtil.SetGameobjectActive(this.m_comBtnMoveToPatch, false);
				NKCUtil.SetGameobjectActive(this.m_comBtnPatchSkipTest, false);
			}
			if (NKCDefineManager.DEFINE_USE_CHEAT())
			{
				NKCUtil.SetGameobjectActive(this.m_comBtnPlaySingle, true);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_comBtnPlaySingle, false);
			}
			NKCUtil.SetGameobjectActive(this.m_objBsideLogo, !NKCPublisherModule.IsZlongPublished());
			NKCUtil.SetGameobjectActive(this.m_objNexonLogo, NKCPublisherModule.IsNexonPublished());
			NKCUtil.SetGameobjectActive(this.m_objZlongLogo, NKCPublisherModule.IsZlongPublished());
			this.SetLoginMessageUI();
			base.UIOpened(true);
		}

		// Token: 0x060068D5 RID: 26837 RVA: 0x0021E86C File Offset: 0x0021CA6C
		private void SetLoginMessageUI()
		{
			if (NKCPublisherModule.PublisherType == NKCPublisherModule.ePublisherType.Zlong && this.m_objMessage != null && this.m_NKCUILoginViewerMsg == null)
			{
				if (this.m_NKCAssetInstanceDataMessage != null)
				{
					NKCAssetResourceManager.CloseInstance(this.m_NKCAssetInstanceDataMessage);
				}
				this.m_NKCAssetInstanceDataMessage = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_LOGIN", "NUF_LOGIN_MESSAGE", false, null);
				if (this.m_NKCAssetInstanceDataMessage != null && this.m_NKCAssetInstanceDataMessage.m_Instant != null)
				{
					NKCUILoginViewerMsg componentInChildren = this.m_NKCAssetInstanceDataMessage.m_Instant.GetComponentInChildren<NKCUILoginViewerMsg>();
					this.m_NKCUILoginViewerMsg = componentInChildren;
					this.m_NKCAssetInstanceDataMessage.m_Instant.transform.SetParent(this.m_objMessage.transform, false);
					this.m_NKCAssetInstanceDataMessage.m_Instant.transform.localPosition = new Vector3(this.m_NKCAssetInstanceDataMessage.m_Instant.transform.localPosition.x, this.m_NKCAssetInstanceDataMessage.m_Instant.transform.localPosition.y, 0f);
				}
				if (this.m_NKCUILoginViewerMsg != null)
				{
					this.m_NKCUILoginViewerMsg.ForceUpdateMsg();
				}
			}
		}

		// Token: 0x060068D6 RID: 26838 RVA: 0x0021E996 File Offset: 0x0021CB96
		public override void Hide()
		{
			base.Hide();
			NKCUtil.SetGameobjectActive(this.m_objBack, false);
		}

		// Token: 0x060068D7 RID: 26839 RVA: 0x0021E9AA File Offset: 0x0021CBAA
		public override void UnHide()
		{
			base.UnHide();
			NKCUtil.SetGameobjectActive(this.m_objBack, true);
		}

		// Token: 0x060068D8 RID: 26840 RVA: 0x0021E9BE File Offset: 0x0021CBBE
		public override void OnCloseInstance()
		{
			if (this.m_objBack != null)
			{
				UnityEngine.Object.Destroy(this.m_objBack);
				this.m_objBack = null;
			}
			if (this.m_BackResource != null)
			{
				NKCAssetResourceManager.CloseResource(this.m_BackResource);
				this.m_BackResource = null;
			}
		}

		// Token: 0x060068D9 RID: 26841 RVA: 0x0021E9FB File Offset: 0x0021CBFB
		private void ApplyNowAuthLevelToText()
		{
			if (this.m_NUF_LOGIN_DEV_LOGIN_AUTH_LEVEL_TEXT != null)
			{
				this.m_NUF_LOGIN_DEV_LOGIN_AUTH_LEVEL_TEXT.text = this.m_NKM_USER_AUTH_LEVEL.ToString();
			}
		}

		// Token: 0x060068DA RID: 26842 RVA: 0x0021EA28 File Offset: 0x0021CC28
		private void LoadLastEditServerAddress()
		{
			if (PlayerPrefs.HasKey(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_LAST_EDITED))
			{
				string @string = PlayerPrefs.GetString(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_LAST_EDITED);
				this.m_IFServerAddress.text = @string;
				return;
			}
			this.m_IFServerAddress.text = "192.168.0.201";
			PlayerPrefs.SetString(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_LAST_EDITED, this.m_IFServerAddress.text);
		}

		// Token: 0x060068DB RID: 26843 RVA: 0x0021EA81 File Offset: 0x0021CC81
		public void OnEndEditServerAddress()
		{
			PlayerPrefs.SetString(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_LAST_EDITED, this.m_IFServerAddress.text);
		}

		// Token: 0x060068DC RID: 26844 RVA: 0x0021EA9C File Offset: 0x0021CC9C
		public void SaveIDPass()
		{
			Log.Debug(string.Format("[Login] SaveIDPass ShowUpLoginPanel[{0}]  ID[{1}]", this.m_bShowUpLoginPanel, this.m_IFID.text), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUILoginViewer.cs", 717);
			if (this.m_bShowUpLoginPanel)
			{
				PlayerPrefs.SetString("NKM_LOCAL_SAVE_KEY_LOGIN_ID_STRING", this.m_IFID.text);
				PlayerPrefs.SetString("NKM_LOCAL_SAVE_KEY_LOGIN_PASSWORD_STRING", this.m_IFPassword.text);
			}
		}

		// Token: 0x060068DD RID: 26845 RVA: 0x0021EB0C File Offset: 0x0021CD0C
		private void OnServerListDropDown(Dropdown change)
		{
			string text = this.m_IFServerAddress.text;
			string text2 = change.captionText.text;
			if (text2 != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text2);
				if (num <= 2031899703U)
				{
					if (num <= 789859694U)
					{
						if (num <= 433112685U)
						{
							if (num != 215274041U)
							{
								if (num == 433112685U)
								{
									if (text2 == "JapanQa")
									{
										text = "Build02:5310";
									}
								}
							}
							else if (text2 == "JapanLive")
							{
								text = "Build02:7310";
							}
						}
						else if (num != 773196749U)
						{
							if (num == 789859694U)
							{
								if (text2 == "ChinaStage")
								{
									text = "DataManager:9100";
								}
							}
						}
						else if (text2 == "Stage")
						{
							text = "52.231.15.96";
						}
					}
					else if (num <= 1793414657U)
					{
						if (num != 979277916U)
						{
							if (num == 1793414657U)
							{
								if (text2 == "Review")
								{
									text = "52.141.21.24";
								}
							}
						}
						else if (text2 == "Dev")
						{
							text = "studiobsidedev.com";
						}
					}
					else if (num != 1833094123U)
					{
						if (num == 2031899703U)
						{
							if (text2 == "JapanStage")
							{
								text = "Build02:6310";
							}
						}
					}
					else if (text2 == "Vietnam")
					{
						text = "192.168.0.145";
					}
				}
				else if (num <= 3288986427U)
				{
					if (num <= 3067769471U)
					{
						if (num != 2043358034U)
						{
							if (num == 3067769471U)
							{
								if (text2 == "devChina")
								{
									text = "DataManager:8100";
								}
							}
						}
						else if (text2 == "SeaQa")
						{
							text = "DataManager:42000";
						}
					}
					else if (num != 3154005398U)
					{
						if (num == 3288986427U)
						{
							if (text2 == "TaiwanQa")
							{
								text = "build02";
							}
						}
					}
					else if (text2 == "ChinaQa")
					{
						text = "DataManager:7100";
					}
				}
				else if (num <= 3806450730U)
				{
					if (num != 3705854472U)
					{
						if (num == 3806450730U)
						{
							if (text2 == "SeaStage")
							{
								text = "DataManager:52000";
							}
						}
					}
					else if (text2 == "Next")
					{
						text = "Build02:32000";
					}
				}
				else if (num != 3918095846U)
				{
					if (num == 3946807265U)
					{
						if (text2 == "TaiwanStage")
						{
							text = "DataManager";
						}
					}
				}
				else if (text2 == "SeaLive")
				{
					text = "DataManager:32000";
				}
			}
			this.m_IFServerAddress.text = text;
		}

		// Token: 0x060068DE RID: 26846 RVA: 0x0021EE1C File Offset: 0x0021D01C
		private void LoadConnectionAddressFromUI()
		{
			Log.Debug("[Login] LoadConnectionAddressFromUI m_IFServerAddress[" + this.m_IFServerAddress.text + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUILoginViewer.cs", 819);
			string text = this.m_IFServerAddress.text;
			int num = 22000;
			string[] array = this.m_IFServerAddress.text.Split(new char[]
			{
				':'
			});
			text = array[0];
			if (array.Length > 1)
			{
				num = Convert.ToInt32(array[1]);
			}
			NKCConnectionInfo.SetLoginServerInfo(NKCConnectionInfo.LOGIN_SERVER_TYPE.Default, text, num, null);
			Log.Debug(string.Format("[Login] NKCConnectionInfo s_ServiceIP[{0}] s_ServicePort[{1}]", text, num), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUILoginViewer.cs", 832);
		}

		// Token: 0x060068DF RID: 26847 RVA: 0x0021EEBC File Offset: 0x0021D0BC
		public string GetCurrentServerAddress()
		{
			return NKCConnectionInfo.ServiceIP;
		}

		// Token: 0x060068E0 RID: 26848 RVA: 0x0021EEC3 File Offset: 0x0021D0C3
		public int GetCurrentServerPort()
		{
			return NKCConnectionInfo.ServicePort;
		}

		// Token: 0x060068E1 RID: 26849 RVA: 0x0021EECA File Offset: 0x0021D0CA
		private void InitConnect()
		{
			NKCConnectLogin connectLogin = NKCScenManager.GetScenManager().GetConnectLogin();
			connectLogin.SetRemoteAddress(this.GetCurrentServerAddress(), this.GetCurrentServerPort());
			connectLogin.ResetConnection();
		}

		// Token: 0x060068E2 RID: 26850 RVA: 0x0021EEED File Offset: 0x0021D0ED
		public void TouchLogin()
		{
			if (this.m_bShowUpLoginPanel)
			{
				return;
			}
			this.OnLogin();
		}

		// Token: 0x060068E3 RID: 26851 RVA: 0x0021EF00 File Offset: 0x0021D100
		private void OnLogin()
		{
			Log.Debug(string.Format("[Agreement] OnLogin Opened[{0}]", NKMContentsVersionManager.HasTag("CHECK_AGREEMENT_NOTICE")), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUILoginViewer.cs", 865);
			if (NKMContentsVersionManager.HasTag("CHECK_AGREEMENT_NOTICE") && !NKCUIAgreementNotice.IsAgreementChecked())
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUIAgreementNotice.PopupMessage, NKCPopupMessage.eMessagePosition.Middle, 0f, true, false, false));
				return;
			}
			if (this.m_loginTouchDelay > 0f)
			{
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_UNDER_MAINTENANCE, null, "");
				NKC_SCEN_LOGIN scen_LOGIN = NKCScenManager.GetScenManager().Get_SCEN_LOGIN();
				if (scen_LOGIN != null)
				{
					scen_LOGIN.UpdateLoginMsgUI();
				}
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_LOGIN().TryLogin();
		}

		// Token: 0x060068E4 RID: 26852 RVA: 0x0021EFA8 File Offset: 0x0021D1A8
		private void OnChangeAccountDev()
		{
			this.m_bShowUpLoginPanel = true;
			if (!this.m_objNUF_LOGIN_DEV_LOGIN.activeSelf)
			{
				this.m_objNUF_LOGIN_DEV_LOGIN.SetActive(true);
			}
			NKCUtil.SetGameobjectActive(this.m_lbNUF_LOGIN_UPDATE_Text, false);
			if (PlayerPrefs.HasKey("NKM_LOCAL_SAVE_KEY_LOGIN_ID_STRING"))
			{
				string @string = PlayerPrefs.GetString("NKM_LOCAL_SAVE_KEY_LOGIN_ID_STRING");
				string string2 = PlayerPrefs.GetString("NKM_LOCAL_SAVE_KEY_LOGIN_PASSWORD_STRING");
				this.m_IFID.text = @string;
				this.m_IFPassword.text = string2;
			}
		}

		// Token: 0x060068E5 RID: 26853 RVA: 0x0021F01B File Offset: 0x0021D21B
		public void SetLoginTouchDelay()
		{
			if (NKCDefineManager.DEFINE_UNITY_EDITOR())
			{
				return;
			}
			if (NKCDefineManager.DEFINE_PC_EXTRA_DOWNLOAD_IN_EXE_FOLDER())
			{
				return;
			}
			if (!NKCDefineManager.DEFINE_SB_GB() && !NKCDefineManager.DEFINE_USE_TOUCH_DELAY())
			{
				return;
			}
			this.m_loginTouchDelay = 60f;
		}

		// Token: 0x060068E6 RID: 26854 RVA: 0x0021F047 File Offset: 0x0021D247
		private void OnPlaySingle()
		{
			if (NKCDefineManager.DEFINE_USE_CHEAT())
			{
				NKCPacketSender.Send_NKMPacket_DEV_GAME_LOAD_REQ("NKM_DUNGEON_TEST");
			}
		}

		// Token: 0x060068E7 RID: 26855 RVA: 0x0021F05A File Offset: 0x0021D25A
		public void OnCutScenSim()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_CUTSCENE_SIM, true);
		}

		// Token: 0x060068E8 RID: 26856 RVA: 0x0021F068 File Offset: 0x0021D268
		public void OnVoiceList()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_VOICE_LIST, true);
		}

		// Token: 0x060068E9 RID: 26857 RVA: 0x0021F077 File Offset: 0x0021D277
		public void OnLanguageSelect()
		{
			NKCUIPopupLanguageSelect.Instance.Open(NKCLocalization.GetSelectLanguageSet(), new NKCUIPopupLanguageSelect.OnClose(this.OnChangeLanguage));
		}

		// Token: 0x060068EA RID: 26858 RVA: 0x0021F094 File Offset: 0x0021D294
		private void OnMoveToPatch()
		{
			NKCScenManager.GetScenManager().ShowBundleUpdate(false);
		}

		// Token: 0x060068EB RID: 26859 RVA: 0x0021F0A1 File Offset: 0x0021D2A1
		private void OnPatchSkipTest()
		{
			NKCPatchUtility.DeleteTutorialClearedStatus();
			NKCPatchUtility.ReservePatchSkipTest();
			Application.Quit();
		}

		// Token: 0x060068EC RID: 26860 RVA: 0x0021F0B4 File Offset: 0x0021D2B4
		private void OnChangeLanguage(NKM_NATIONAL_CODE eNKM_NATIONAL_CODE)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			if (eNKM_NATIONAL_CODE != NKM_NATIONAL_CODE.NNC_END)
			{
				gameOptionData.NKM_NATIONAL_CODE = eNKM_NATIONAL_CODE;
				NKCGameOptionData.SaveOnlyLang(eNKM_NATIONAL_CODE);
				Application.Quit();
			}
		}

		// Token: 0x060068ED RID: 26861 RVA: 0x0021F0E7 File Offset: 0x0021D2E7
		public void OnGoogleLogin()
		{
		}

		// Token: 0x060068EE RID: 26862 RVA: 0x0021F0E9 File Offset: 0x0021D2E9
		public void OnCollection()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_COLLECTION, true);
		}

		// Token: 0x060068EF RID: 26863 RVA: 0x0021F0F8 File Offset: 0x0021D2F8
		private void OnSaveServer1ST()
		{
			PlayerPrefs.SetString(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_1ST, this.m_IFServerAddress.text);
		}

		// Token: 0x060068F0 RID: 26864 RVA: 0x0021F110 File Offset: 0x0021D310
		private void OnLoadServer1ST()
		{
			if (PlayerPrefs.HasKey(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_1ST))
			{
				string @string = PlayerPrefs.GetString(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_1ST);
				this.m_IFServerAddress.text = @string;
			}
			else
			{
				this.m_IFServerAddress.text = "192.168.0.201";
			}
			PlayerPrefs.SetString(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_LAST_EDITED, this.m_IFServerAddress.text);
		}

		// Token: 0x060068F1 RID: 26865 RVA: 0x0021F16A File Offset: 0x0021D36A
		private void OnSaveServer2ND()
		{
			PlayerPrefs.SetString(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_2ND, this.m_IFServerAddress.text);
		}

		// Token: 0x060068F2 RID: 26866 RVA: 0x0021F184 File Offset: 0x0021D384
		private void OnLoadServer2ND()
		{
			if (PlayerPrefs.HasKey(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_2ND))
			{
				string @string = PlayerPrefs.GetString(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_2ND);
				this.m_IFServerAddress.text = @string;
			}
			else
			{
				this.m_IFServerAddress.text = "";
			}
			PlayerPrefs.SetString(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_LAST_EDITED, this.m_IFServerAddress.text);
		}

		// Token: 0x060068F3 RID: 26867 RVA: 0x0021F1DE File Offset: 0x0021D3DE
		private void OnSaveServer3RD()
		{
			PlayerPrefs.SetString(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_3RD, this.m_IFServerAddress.text);
		}

		// Token: 0x060068F4 RID: 26868 RVA: 0x0021F1F8 File Offset: 0x0021D3F8
		private void OnLoadServer3RD()
		{
			if (PlayerPrefs.HasKey(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_3RD))
			{
				string @string = PlayerPrefs.GetString(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_3RD);
				this.m_IFServerAddress.text = @string;
			}
			else
			{
				this.m_IFServerAddress.text = "";
			}
			PlayerPrefs.SetString(this.NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_LAST_EDITED, this.m_IFServerAddress.text);
		}

		// Token: 0x060068F5 RID: 26869 RVA: 0x0021F252 File Offset: 0x0021D452
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(this.m_objBack, false);
			base.gameObject.SetActive(false);
		}

		// Token: 0x060068F6 RID: 26870 RVA: 0x0021F26C File Offset: 0x0021D46C
		public void Update()
		{
			this.m_fUpdateTime += Time.deltaTime;
			if (this.m_loginTouchDelay > 0f)
			{
				this.m_loginTouchDelay -= Time.deltaTime;
			}
			if (!this.m_objNUF_LOGIN_DEV_LOGIN.activeSelf && this.m_bShowUpLoginPanel && this.m_fUpdateTime > 2.5f)
			{
				this.m_objNUF_LOGIN_DEV_LOGIN.SetActive(true);
			}
		}

		// Token: 0x060068F7 RID: 26871 RVA: 0x0021F2D8 File Offset: 0x0021D4D8
		public void AfterLoginToPublisherCompleted(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
		{
			NKMPopUpBox.CloseWaitBox();
			Debug.Log("AfterLoginToPublisherCompleted");
			NKCPublisherModule.Statistics.LogClientAction(NKCPublisherModule.NKCPMStatistics.eClientAction.AfterSyncAccountComplete, 0, null);
			if (NKCPublisherModule.IsPCBuild())
			{
				return;
			}
			if (!NKCPublisherModule.IsPublisherNoneType())
			{
				bool loginToPublisherCompleted = NKCPublisherModule.Auth.LoginToPublisherCompleted;
				if (loginToPublisherCompleted)
				{
					Debug.Log("bSyncAccountActive true");
					NKCUtil.SetImageSprite(this.m_NUF_LOGIN_DEV_CHANGE_ACCOUNT_BUTTON_BG, NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_LOGIN_YELLOW), false);
					this.m_comBtnChangeAccount.PointerClick.RemoveAllListeners();
					this.m_comBtnChangeAccount.PointerClick.AddListener(new UnityAction(this.OnChangeAccountPublisher));
					if (NKCPublisherModule.Auth.CheckNeedToCheckEnableQR_AfterPubLogin())
					{
						NKCUtil.SetGameobjectActive(this.m_csbtnPcQR, NKCPublisherModule.Auth.CheckEnableQR_Login());
					}
				}
				else
				{
					Debug.Log("bSyncAccountActive false");
					NKCUtil.SetImageSprite(this.m_NUF_LOGIN_DEV_CHANGE_ACCOUNT_BUTTON_BG, NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY), false);
					this.m_comBtnChangeAccount.PointerClick.RemoveAllListeners();
				}
				NKCUtil.SetLabelText(this.m_NUF_LOGIN_DEV_CHANGE_ACCOUNT_BUTTON_TEXT, loginToPublisherCompleted ? NKCUtilString.GET_STRING_TOY_LOGIN_CHANGE_ACCOUNT : NKCUtilString.GET_STRING_TOY_LOGIN_NO_AUTH);
				NKCUtil.SetLabelTextColor(this.m_NUF_LOGIN_DEV_CHANGE_ACCOUNT_BUTTON_TEXT, Color.black);
			}
		}

		// Token: 0x060068F8 RID: 26872 RVA: 0x0021F3E3 File Offset: 0x0021D5E3
		private void OnChangeAccountPublisher()
		{
			NKCPublisherModule.Auth.ChangeAccount(new NKCPublisherModule.OnComplete(this.AfterLoginToPublisherCompleted), false);
		}

		// Token: 0x060068F9 RID: 26873 RVA: 0x0021F3FC File Offset: 0x0021D5FC
		private void OpenQuitApplicationPopup()
		{
			if (NKCPublisherModule.Auth.CheckExitCallFirst())
			{
				NKCPublisherModule.Auth.Exit(new NKCPublisherModule.OnComplete(this.OnCompleteExitFirst));
				return;
			}
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_LOBBY_CHECK_QUIT_GAME, new NKCPopupOKCancel.OnButton(this.OnQuit), null, false);
		}

		// Token: 0x060068FA RID: 26874 RVA: 0x0021F449 File Offset: 0x0021D649
		private void OnQuit()
		{
			NKMPopUpBox.OpenWaitBox(0f, "");
			NKCPublisherModule.Auth.Exit(new NKCPublisherModule.OnComplete(this.OnCompleteExit));
		}

		// Token: 0x060068FB RID: 26875 RVA: 0x0021F470 File Offset: 0x0021D670
		private void OnCompleteExit(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
		{
			if (!NKCPublisherModule.CheckError(resultCode, additionalError, true, null, false))
			{
				return;
			}
			Application.Quit();
		}

		// Token: 0x060068FC RID: 26876 RVA: 0x0021F484 File Offset: 0x0021D684
		private void OnCompleteExitFirst(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
		{
			if (!NKCPublisherModule.CheckError(resultCode, additionalError, true, null, false))
			{
				return;
			}
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_LOBBY_CHECK_QUIT_GAME, delegate()
			{
				Application.Quit();
			}, null, false);
		}

		// Token: 0x0400549E RID: 21662
		private const string ASSET_BUNDLE_NAME = "ab_ui_login_loc";

		// Token: 0x0400549F RID: 21663
		private const string UI_ASSET_NAME = "NUF_LOGIN_PREFAB";

		// Token: 0x040054A0 RID: 21664
		private static NKCUIManager.LoadedUIData s_LoadedUIData;

		// Token: 0x040054A1 RID: 21665
		public GameObject m_NUF_LOGIN_DEV_SERVER_LIST;

		// Token: 0x040054A2 RID: 21666
		public GameObject m_NUF_LOGIN_DEV_LOGIN_SIGN_BUTTON;

		// Token: 0x040054A3 RID: 21667
		private Dropdown m_NUF_LOGIN_DEV_SERVER_LIST_DROPDOWN;

		// Token: 0x040054A4 RID: 21668
		public NKCUIComButton m_comBtnServer1STSave;

		// Token: 0x040054A5 RID: 21669
		public NKCUIComButton m_comBtnServer1STLoad;

		// Token: 0x040054A6 RID: 21670
		public NKCUIComButton m_comBtnServer2NDSave;

		// Token: 0x040054A7 RID: 21671
		public NKCUIComButton m_comBtnServer2NDLoad;

		// Token: 0x040054A8 RID: 21672
		public NKCUIComButton m_comBtnServer3RDSave;

		// Token: 0x040054A9 RID: 21673
		public NKCUIComButton m_comBtnServer3RDLoad;

		// Token: 0x040054AA RID: 21674
		public InputField m_IFServerAddress;

		// Token: 0x040054AB RID: 21675
		public InputField m_IFID;

		// Token: 0x040054AC RID: 21676
		public InputField m_IFPassword;

		// Token: 0x040054AD RID: 21677
		public NKCUIComButton m_comBtnLogin;

		// Token: 0x040054AE RID: 21678
		public NKCUIComButton m_comBtnChangeAccount;

		// Token: 0x040054AF RID: 21679
		public NKCUIComButton m_comBtnPlaySingle;

		// Token: 0x040054B0 RID: 21680
		public NKCUIComButton m_comBtnCutscen;

		// Token: 0x040054B1 RID: 21681
		public NKCUIComButton m_comBtnVoiceList;

		// Token: 0x040054B2 RID: 21682
		public NKCUIComButton m_comBtnLanguageSelect;

		// Token: 0x040054B3 RID: 21683
		public NKCUIComButton m_comBtnMoveToPatch;

		// Token: 0x040054B4 RID: 21684
		public NKCUIComButton m_comBtnPatchSkipTest;

		// Token: 0x040054B5 RID: 21685
		public GameObject m_NUF_LOGIN_SCREEN;

		// Token: 0x040054B6 RID: 21686
		public GameObject m_objBsideLogo;

		// Token: 0x040054B7 RID: 21687
		public GameObject m_objNexonLogo;

		// Token: 0x040054B8 RID: 21688
		public GameObject m_objZlongLogo;

		// Token: 0x040054B9 RID: 21689
		public NKCUIComStateButton m_csbtnPcQR;

		// Token: 0x040054BA RID: 21690
		public GameObject m_objMessage;

		// Token: 0x040054BB RID: 21691
		private NKCUILoginViewerMsg m_NKCUILoginViewerMsg;

		// Token: 0x040054BC RID: 21692
		private NKCAssetInstanceData m_NKCAssetInstanceDataMessage;

		// Token: 0x040054BD RID: 21693
		public const string NKM_LOCAL_SAVE_KEY_LOGIN_ID_STRING = "NKM_LOCAL_SAVE_KEY_LOGIN_ID_STRING";

		// Token: 0x040054BE RID: 21694
		public const string NKM_LOCAL_SAVE_KEY_LOGIN_PASSWORD_STRING = "NKM_LOCAL_SAVE_KEY_LOGIN_PASSWORD_STRING";

		// Token: 0x040054BF RID: 21695
		private string NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_LAST_EDITED = "NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_LAST_EDITED";

		// Token: 0x040054C0 RID: 21696
		private string NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_1ST = "NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_1ST";

		// Token: 0x040054C1 RID: 21697
		private string NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_2ND = "NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_2ND";

		// Token: 0x040054C2 RID: 21698
		private string NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_3RD = "NKM_LOCAL_SAVE_KEY_LOGIN_SERVER_3RD";

		// Token: 0x040054C3 RID: 21699
		public NKCUIComStateButton NUF_LOGIN_NOTICE;

		// Token: 0x040054C4 RID: 21700
		public GameObject m_objNUF_LOGIN_DEV_LOGIN;

		// Token: 0x040054C5 RID: 21701
		public Text m_lbNUF_LOGIN_UPDATE_Text;

		// Token: 0x040054C6 RID: 21702
		[Obsolete]
		public GameObject m_NUF_LOGIN_DEV_SINGLE;

		// Token: 0x040054C7 RID: 21703
		public GameObject m_NUF_LOGIN_DEV_CUTSCEN;

		// Token: 0x040054C8 RID: 21704
		public GameObject m_NUF_LOGIN_DEV_VOICE_LIST;

		// Token: 0x040054C9 RID: 21705
		public NKCUIComStateButton m_NUF_LOGIN_DEV_LOGIN_AUTH_LEVEL;

		// Token: 0x040054CA RID: 21706
		public Text m_NUF_LOGIN_DEV_LOGIN_AUTH_LEVEL_TEXT;

		// Token: 0x040054CB RID: 21707
		private NKM_USER_AUTH_LEVEL m_NKM_USER_AUTH_LEVEL = NKM_USER_AUTH_LEVEL.NORMAL_USER;

		// Token: 0x040054CC RID: 21708
		public GameObject m_NUF_LOGIN_DEV_CHANGE_ACCOUNT_BUTTON;

		// Token: 0x040054CD RID: 21709
		public Image m_NUF_LOGIN_DEV_CHANGE_ACCOUNT_BUTTON_BG;

		// Token: 0x040054CE RID: 21710
		public Text m_NUF_LOGIN_DEV_CHANGE_ACCOUNT_BUTTON_TEXT;

		// Token: 0x040054CF RID: 21711
		public Text m_lbVersion;

		// Token: 0x040054D0 RID: 21712
		private float m_fUpdateTime;

		// Token: 0x040054D1 RID: 21713
		private const float LOGIN_PANEL_SHOW_UP_TIME = 2.5f;

		// Token: 0x040054D2 RID: 21714
		private bool m_bShowUpLoginPanel;

		// Token: 0x040054D3 RID: 21715
		private const float LOGIN_TOUCH_DELAY_TIME = 60f;

		// Token: 0x040054D4 RID: 21716
		private float m_loginTouchDelay;

		// Token: 0x040054D5 RID: 21717
		private NKCAssetResourceData m_BackResource;

		// Token: 0x040054D6 RID: 21718
		private GameObject m_objBack;

		// Token: 0x040054D7 RID: 21719
		private List<NKCAssetInstanceData> m_instancedPrefabList = new List<NKCAssetInstanceData>();
	}
}
