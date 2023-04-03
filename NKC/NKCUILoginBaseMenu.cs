using System;
using System.Collections.Generic;
using Cs.Logging;
using NKC.Publisher;
using NKC.Templet;
using NKC.UI.Component;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NKC.UI
{
	// Token: 0x020009B3 RID: 2483
	public class NKCUILoginBaseMenu : NKCUIBase
	{
		// Token: 0x1700120B RID: 4619
		// (get) Token: 0x0600686D RID: 26733 RVA: 0x0021C1C5 File Offset: 0x0021A3C5
		public override NKCUIManager.eUIUnloadFlag UnloadFlag
		{
			get
			{
				return NKCUIManager.eUIUnloadFlag.DEFAULT;
			}
		}

		// Token: 0x0600686E RID: 26734 RVA: 0x0021C1C8 File Offset: 0x0021A3C8
		public static NKCUIManager.LoadedUIData OpenNewInstanceAsync()
		{
			if (!NKCUIManager.IsValid(NKCUILoginBaseMenu.s_LoadedUIData))
			{
				NKCUILoginBaseMenu.s_LoadedUIData = NKCUIManager.OpenNewInstanceAsync<NKCUILoginBaseMenu>("ab_ui_login_base", "NUF_LOGIN_BASE_MENU", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUILoginBaseMenu.CleanupInstance));
			}
			return NKCUILoginBaseMenu.s_LoadedUIData;
		}

		// Token: 0x1700120C RID: 4620
		// (get) Token: 0x0600686F RID: 26735 RVA: 0x0021C1FC File Offset: 0x0021A3FC
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUILoginBaseMenu.s_LoadedUIData != null && NKCUILoginBaseMenu.s_LoadedUIData.IsUIOpen;
			}
		}

		// Token: 0x1700120D RID: 4621
		// (get) Token: 0x06006870 RID: 26736 RVA: 0x0021C211 File Offset: 0x0021A411
		public static bool IsInstanceLoaded
		{
			get
			{
				return NKCUILoginBaseMenu.s_LoadedUIData != null && NKCUILoginBaseMenu.s_LoadedUIData.IsLoadComplete;
			}
		}

		// Token: 0x06006871 RID: 26737 RVA: 0x0021C226 File Offset: 0x0021A426
		public static void CleanupInstance()
		{
			NKCUILoginBaseMenu.s_LoadedUIData = null;
		}

		// Token: 0x06006872 RID: 26738 RVA: 0x0021C22E File Offset: 0x0021A42E
		public static NKCUILoginBaseMenu GetInstance()
		{
			if (NKCUILoginBaseMenu.s_LoadedUIData != null && NKCUILoginBaseMenu.s_LoadedUIData.IsLoadComplete)
			{
				return NKCUILoginBaseMenu.s_LoadedUIData.GetInstance<NKCUILoginBaseMenu>();
			}
			return null;
		}

		// Token: 0x1700120E RID: 4622
		// (get) Token: 0x06006873 RID: 26739 RVA: 0x0021C24F File Offset: 0x0021A44F
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_LOGIN;
			}
		}

		// Token: 0x06006874 RID: 26740 RVA: 0x0021C256 File Offset: 0x0021A456
		public override void OnBackButton()
		{
			this.OpenQuitApplicationPopup();
		}

		// Token: 0x1700120F RID: 4623
		// (get) Token: 0x06006875 RID: 26741 RVA: 0x0021C25E File Offset: 0x0021A45E
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001210 RID: 4624
		// (get) Token: 0x06006876 RID: 26742 RVA: 0x0021C261 File Offset: 0x0021A461
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x06006877 RID: 26743 RVA: 0x0021C264 File Offset: 0x0021A464
		public void UpdateLoginMsgUI()
		{
			if (this.m_NKCUILoginViewerMsg != null)
			{
				this.m_NKCUILoginViewerMsg.ForceUpdateMsg();
			}
		}

		// Token: 0x06006878 RID: 26744 RVA: 0x0021C27F File Offset: 0x0021A47F
		private void OnDestroy()
		{
			if (this.m_NKCAssetInstanceDataMessage != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_NKCAssetInstanceDataMessage);
			}
			this.m_NKCAssetInstanceDataMessage = null;
			this.m_NKCUILoginViewerMsg = null;
		}

		// Token: 0x06006879 RID: 26745 RVA: 0x0021C2A4 File Offset: 0x0021A4A4
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
					this.m_BackResource = NKCAssetResourceManager.OpenResource<GameObject>("AB_LOGIN_SCREEN_NKM_LS_VER_6", "AB_LOGIN_SCREEN_NKM_LS_VER_6", false, null);
				}
			}
			this.m_objBack = UnityEngine.Object.Instantiate<GameObject>(this.m_BackResource.GetAsset<GameObject>(), NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIMidCanvas));
			this.m_objBack.transform.localPosition = Vector3.zero;
		}

		// Token: 0x0600687A RID: 26746 RVA: 0x0021C374 File Offset: 0x0021A574
		private void LoadPrefabList()
		{
			this.ReleaseEnabledPrefabs();
			foreach (NKCLoginBackgroundTemplet nkcloginBackgroundTemplet in NKCLoginBackgroundTemplet.GetEnabledPrefabList())
			{
				NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>(nkcloginBackgroundTemplet.BundleName, nkcloginBackgroundTemplet.AssetName, false, this.m_LoginPrefabBase);
				if (nkcassetInstanceData == null)
				{
					Log.Error(string.Format("[NKCUILoginViewer] LoadPrefab failed  id[{0}] asset[{1}/{2}]", nkcloginBackgroundTemplet.ID, nkcloginBackgroundTemplet.AssetName, nkcloginBackgroundTemplet.BundleName), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUILoginBaseMenu.cs", 165);
				}
				else
				{
					this.m_instancedPrefabList.Add(nkcassetInstanceData);
				}
			}
		}

		// Token: 0x0600687B RID: 26747 RVA: 0x0021C420 File Offset: 0x0021A620
		private void ReleaseEnabledPrefabs()
		{
			foreach (NKCAssetInstanceData nkcassetInstanceData in this.m_instancedPrefabList)
			{
				nkcassetInstanceData.Close();
			}
			this.m_instancedPrefabList.Clear();
		}

		// Token: 0x0600687C RID: 26748 RVA: 0x0021C47C File Offset: 0x0021A67C
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

		// Token: 0x0600687D RID: 26749 RVA: 0x0021C4CE File Offset: 0x0021A6CE
		public NKCUILoginDevMenu GetLoginDevMenu()
		{
			return this.m_LoginDevMenu;
		}

		// Token: 0x0600687E RID: 26750 RVA: 0x0021C4D8 File Offset: 0x0021A6D8
		public void InitUI()
		{
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
			if (this.m_comBtnChangeAccount != null)
			{
				if (NKCPublisherModule.IsPublisherNoneType())
				{
					this.m_comBtnChangeAccount.PointerClick.RemoveAllListeners();
					NKCUtil.SetGameobjectActive(this.m_comBtnChangeAccount, true);
				}
				else if (NKCPublisherModule.IsPCBuild())
				{
					NKCUtil.SetGameobjectActive(this.m_comBtnChangeAccount, false);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_comBtnChangeAccount, NKCPublisherModule.Auth.LoginToPublisherCompleted);
				}
			}
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
			this.LoadBackgroundTemplet();
			this.LoadPrefabList();
			base.gameObject.SetActive(false);
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
			if (this.m_comBtnSelectServer != null)
			{
				if (NKCDefineManager.DEFINE_SELECT_SERVER() && NKCConnectionInfo.GetLoginServerCount() > 1)
				{
					NKCUtil.SetGameobjectActive(this.m_comBtnSelectServer, true);
					NKCUtil.SetButtonClickDelegate(this.m_comBtnSelectServer, new UnityAction(this.OnClickSelectServer));
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_comBtnSelectServer, false);
				}
			}
			if (this.m_objSelectedServerStatus != null && this.m_lbSelectedServerStatus != null)
			{
				if (NKCDefineManager.DEFINE_SELECT_SERVER())
				{
					NKCUtil.SetGameobjectActive(this.m_objSelectedServerStatus, true);
					NKCUtil.SetLabelText(this.m_lbSelectedServerStatus, NKCConnectionInfo.GetCurrentLoginServerString("SI_PF_LOGIN_SELECTSERVER_STATUS", false));
					return;
				}
				NKCUtil.SetGameobjectActive(this.m_objSelectedServerStatus, false);
			}
		}

		// Token: 0x0600687F RID: 26751 RVA: 0x0021C759 File Offset: 0x0021A959
		private void OnClickPcQR()
		{
			NKMPopUpBox.OpenWaitBox(0f, "");
			NKCPublisherModule.Auth.QR_Login(new NKCPublisherModule.OnComplete(this.OnCompleteQR_Login));
		}

		// Token: 0x06006880 RID: 26752 RVA: 0x0021C780 File Offset: 0x0021A980
		private void OnCompleteQR_Login(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
		{
			NKCPublisherModule.CheckError(resultCode, additionalError, true, null, false);
		}

		// Token: 0x06006881 RID: 26753 RVA: 0x0021C78D File Offset: 0x0021A98D
		private void OnClickNoticeButton()
		{
			NKCPublisherModule.Notice.OpenNotice(null);
		}

		// Token: 0x06006882 RID: 26754 RVA: 0x0021C79C File Offset: 0x0021A99C
		public void Open()
		{
			this.RescaleBack();
			base.gameObject.SetActive(true);
			this.m_fUpdateTime = 0f;
			Log.Debug("[Login] Open PublisherModule[" + NKCPublisherModule.PublisherType.ToString() + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUILoginBaseMenu.cs", 350);
			this.SetLoginMessageUI();
			base.UIOpened(true);
		}

		// Token: 0x06006883 RID: 26755 RVA: 0x0021C804 File Offset: 0x0021AA04
		private void SetLoginMessageUI()
		{
			if (NKCPublisherModule.PublisherType == NKCPublisherModule.ePublisherType.Zlong && this.m_NUF_LOGIN_MESSAGE != null && this.m_NKCUILoginViewerMsg == null)
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
					this.m_NKCAssetInstanceDataMessage.m_Instant.transform.SetParent(this.m_NUF_LOGIN_MESSAGE.transform, false);
					this.m_NKCAssetInstanceDataMessage.m_Instant.transform.localPosition = new Vector3(this.m_NKCAssetInstanceDataMessage.m_Instant.transform.localPosition.x, this.m_NKCAssetInstanceDataMessage.m_Instant.transform.localPosition.y, 0f);
				}
				if (this.m_NKCUILoginViewerMsg != null)
				{
					this.m_NKCUILoginViewerMsg.ForceUpdateMsg();
				}
			}
		}

		// Token: 0x06006884 RID: 26756 RVA: 0x0021C92E File Offset: 0x0021AB2E
		public override void Hide()
		{
			base.Hide();
			NKCUtil.SetGameobjectActive(this.m_objBack, false);
		}

		// Token: 0x06006885 RID: 26757 RVA: 0x0021C942 File Offset: 0x0021AB42
		public override void UnHide()
		{
			base.UnHide();
			NKCUtil.SetGameobjectActive(this.m_objBack, true);
		}

		// Token: 0x06006886 RID: 26758 RVA: 0x0021C956 File Offset: 0x0021AB56
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

		// Token: 0x06006887 RID: 26759 RVA: 0x0021C993 File Offset: 0x0021AB93
		public string GetCurrentServerAddress()
		{
			return NKCConnectionInfo.ServiceIP;
		}

		// Token: 0x06006888 RID: 26760 RVA: 0x0021C99A File Offset: 0x0021AB9A
		public int GetCurrentServerPort()
		{
			return NKCConnectionInfo.ServicePort;
		}

		// Token: 0x06006889 RID: 26761 RVA: 0x0021C9A1 File Offset: 0x0021ABA1
		private void InitConnect()
		{
			NKCConnectLogin connectLogin = NKCScenManager.GetScenManager().GetConnectLogin();
			connectLogin.SetRemoteAddress(this.GetCurrentServerAddress(), this.GetCurrentServerPort());
			connectLogin.ResetConnection();
		}

		// Token: 0x0600688A RID: 26762 RVA: 0x0021C9C4 File Offset: 0x0021ABC4
		public void TouchLogin()
		{
			this.OnLogin();
		}

		// Token: 0x0600688B RID: 26763 RVA: 0x0021C9CC File Offset: 0x0021ABCC
		public void OnLogin()
		{
			Log.Debug(string.Format("[Agreement] OnLogin Opened[{0}]", NKMContentsVersionManager.HasTag("CHECK_AGREEMENT_NOTICE")), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUILoginBaseMenu.cs", 457);
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

		// Token: 0x0600688C RID: 26764 RVA: 0x0021CA73 File Offset: 0x0021AC73
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

		// Token: 0x0600688D RID: 26765 RVA: 0x0021CA9F File Offset: 0x0021AC9F
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(this.m_objBack, false);
			base.gameObject.SetActive(false);
		}

		// Token: 0x0600688E RID: 26766 RVA: 0x0021CAB9 File Offset: 0x0021ACB9
		public void Update()
		{
			this.m_fUpdateTime += Time.deltaTime;
			if (this.m_loginTouchDelay > 0f)
			{
				this.m_loginTouchDelay -= Time.deltaTime;
			}
		}

		// Token: 0x0600688F RID: 26767 RVA: 0x0021CAEC File Offset: 0x0021ACEC
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
				if (NKCPublisherModule.Auth.LoginToPublisherCompleted)
				{
					Debug.Log("bSyncAccountActive true");
					NKCUtil.SetGameobjectActive(this.m_comBtnChangeAccount, true);
					this.m_comBtnChangeAccount.PointerClick.RemoveAllListeners();
					this.m_comBtnChangeAccount.PointerClick.AddListener(new UnityAction(this.OnChangeAccountPublisher));
					if (NKCPublisherModule.Auth.CheckNeedToCheckEnableQR_AfterPubLogin())
					{
						NKCUtil.SetGameobjectActive(this.m_csbtnPcQR, NKCPublisherModule.Auth.CheckEnableQR_Login());
						return;
					}
				}
				else
				{
					Debug.Log("bSyncAccountActive false");
					NKCUtil.SetGameobjectActive(this.m_comBtnChangeAccount, false);
					this.m_comBtnChangeAccount.PointerClick.RemoveAllListeners();
				}
			}
		}

		// Token: 0x06006890 RID: 26768 RVA: 0x0021CBBE File Offset: 0x0021ADBE
		private void OnChangeAccountPublisher()
		{
			NKCPublisherModule.Auth.ChangeAccount(new NKCPublisherModule.OnComplete(this.AfterLoginToPublisherCompleted), false);
		}

		// Token: 0x06006891 RID: 26769 RVA: 0x0021CBD7 File Offset: 0x0021ADD7
		private void OnClickSelectServer()
		{
			NKCPopupSelectServer.Instance.Open(true, true, null);
		}

		// Token: 0x06006892 RID: 26770 RVA: 0x0021CBE8 File Offset: 0x0021ADE8
		private void OpenQuitApplicationPopup()
		{
			if (NKCPublisherModule.Auth.CheckExitCallFirst())
			{
				NKCPublisherModule.Auth.Exit(new NKCPublisherModule.OnComplete(this.OnCompleteExitFirst));
				return;
			}
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_LOBBY_CHECK_QUIT_GAME, new NKCPopupOKCancel.OnButton(this.OnQuit), null, false);
		}

		// Token: 0x06006893 RID: 26771 RVA: 0x0021CC35 File Offset: 0x0021AE35
		private void OnQuit()
		{
			NKMPopUpBox.OpenWaitBox(0f, "");
			NKCPublisherModule.Auth.Exit(new NKCPublisherModule.OnComplete(this.OnCompleteExit));
		}

		// Token: 0x06006894 RID: 26772 RVA: 0x0021CC5C File Offset: 0x0021AE5C
		private void OnCompleteExit(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
		{
			if (!NKCPublisherModule.CheckError(resultCode, additionalError, true, null, false))
			{
				return;
			}
			Application.Quit();
		}

		// Token: 0x06006895 RID: 26773 RVA: 0x0021CC70 File Offset: 0x0021AE70
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

		// Token: 0x04005465 RID: 21605
		private const string ASSET_BUNDLE_NAME = "ab_ui_login_base";

		// Token: 0x04005466 RID: 21606
		private const string UI_ASSET_NAME = "NUF_LOGIN_BASE_MENU";

		// Token: 0x04005467 RID: 21607
		private static NKCUIManager.LoadedUIData s_LoadedUIData;

		// Token: 0x04005468 RID: 21608
		public GameObject m_NUF_LOGIN_SCREEN;

		// Token: 0x04005469 RID: 21609
		public NKCUIComStateButton m_comBtnChangeAccount;

		// Token: 0x0400546A RID: 21610
		public NKCUIComStateButton m_comBtnSelectServer;

		// Token: 0x0400546B RID: 21611
		public GameObject m_objSelectedServerStatus;

		// Token: 0x0400546C RID: 21612
		public NKCComTMPUIText m_lbSelectedServerStatus;

		// Token: 0x0400546D RID: 21613
		public NKCUIComStateButton m_csbtnPcQR;

		// Token: 0x0400546E RID: 21614
		public GameObject m_NUF_LOGIN_MESSAGE;

		// Token: 0x0400546F RID: 21615
		private NKCUILoginViewerMsg m_NKCUILoginViewerMsg;

		// Token: 0x04005470 RID: 21616
		private NKCAssetInstanceData m_NKCAssetInstanceDataMessage;

		// Token: 0x04005471 RID: 21617
		public NKCUIComStateButton NUF_LOGIN_NOTICE;

		// Token: 0x04005472 RID: 21618
		private float m_fUpdateTime;

		// Token: 0x04005473 RID: 21619
		private const float LOGIN_TOUCH_DELAY_TIME = 60f;

		// Token: 0x04005474 RID: 21620
		private float m_loginTouchDelay;

		// Token: 0x04005475 RID: 21621
		private NKCAssetResourceData m_BackResource;

		// Token: 0x04005476 RID: 21622
		private GameObject m_objBack;

		// Token: 0x04005477 RID: 21623
		private List<NKCAssetInstanceData> m_instancedPrefabList = new List<NKCAssetInstanceData>();

		// Token: 0x04005478 RID: 21624
		public Transform m_LoginPrefabBase;

		// Token: 0x04005479 RID: 21625
		public NKCUILoginDevMenu m_LoginDevMenu;
	}
}
