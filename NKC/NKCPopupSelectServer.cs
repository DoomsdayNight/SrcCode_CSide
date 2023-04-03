using System;
using NKC.Patcher;
using NKC.UI;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006B9 RID: 1721
	public class NKCPopupSelectServer : NKCUIBase
	{
		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06003A5B RID: 14939 RVA: 0x0012CCC8 File Offset: 0x0012AEC8
		public static NKCPopupSelectServer Instance
		{
			get
			{
				if (NKCPopupSelectServer.m_Instance == null)
				{
					NKCPopupSelectServer.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupSelectServer>("AB_UI_LOGIN_SELECT", "AB_UI_LOGIN_SELECT_SERVER", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupSelectServer.CleanupInstance)).GetInstance<NKCPopupSelectServer>();
				}
				return NKCPopupSelectServer.m_Instance;
			}
		}

		// Token: 0x06003A5C RID: 14940 RVA: 0x0012CD02 File Offset: 0x0012AF02
		private static void CleanupInstance()
		{
			NKCPopupSelectServer.m_Instance = null;
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06003A5D RID: 14941 RVA: 0x0012CD0A File Offset: 0x0012AF0A
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06003A5E RID: 14942 RVA: 0x0012CD0D File Offset: 0x0012AF0D
		public override string MenuName
		{
			get
			{
				return "SelectServer";
			}
		}

		// Token: 0x06003A5F RID: 14943 RVA: 0x0012CD14 File Offset: 0x0012AF14
		public void Open(bool bShowCloseMenu, bool bMoveToPatcher, Action onClosed = null)
		{
			this.m_selectedServerType = NKCConnectionInfo.LOGIN_SERVER_TYPE.None;
			this.m_onClosed = onClosed;
			this.m_bShowCloseMenu = bShowCloseMenu;
			this.m_bMoveToPatcher = bMoveToPatcher;
			this.InitUI();
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			base.UIOpened(true);
		}

		// Token: 0x06003A60 RID: 14944 RVA: 0x0012CD4C File Offset: 0x0012AF4C
		private void InitUI()
		{
			NKCUtil.SetGameobjectActive(this.m_csbtnClose, this.m_bShowCloseMenu);
			if (this.m_bShowCloseMenu)
			{
				this.m_csbtnClose.PointerClick.RemoveAllListeners();
				this.m_csbtnClose.PointerClick.AddListener(delegate()
				{
					this.OnClickClose();
				});
			}
			else
			{
				this.m_rectTransform = base.GetComponent<RectTransform>();
				this.m_rectTransform.localPosition = new Vector3(0f, 0f, 0f);
			}
			NKCUtil.SetGameobjectActive(this.m_ctglGroup, true);
			this.m_ctglGroup.SetAllToggleUnselected();
			NKCUtil.SetGameobjectActive(this.m_ctglKorea, NKCConnectionInfo.HasLoginServerInfo(NKCConnectionInfo.LOGIN_SERVER_TYPE.Korea));
			this.m_ctglKorea.OnValueChanged.RemoveAllListeners();
			this.m_ctglKorea.OnValueChanged.AddListener(delegate(bool selected)
			{
				this.OnClickServer(NKCConnectionInfo.LOGIN_SERVER_TYPE.Korea);
			});
			NKCUtil.SetGameobjectActive(this.m_ctglGlobal, NKCConnectionInfo.HasLoginServerInfo(NKCConnectionInfo.LOGIN_SERVER_TYPE.Global));
			this.m_ctglGlobal.OnValueChanged.RemoveAllListeners();
			this.m_ctglGlobal.OnValueChanged.AddListener(delegate(bool selected)
			{
				this.OnClickServer(NKCConnectionInfo.LOGIN_SERVER_TYPE.Global);
			});
			this.m_csbtnOk.PointerClick.RemoveAllListeners();
			this.m_csbtnOk.PointerClick.AddListener(delegate()
			{
				this.OnClickOk();
			});
		}

		// Token: 0x06003A61 RID: 14945 RVA: 0x0012CE88 File Offset: 0x0012B088
		private void OnClickServer(NKCConnectionInfo.LOGIN_SERVER_TYPE type)
		{
			this.m_selectedServerType = type;
		}

		// Token: 0x06003A62 RID: 14946 RVA: 0x0012CE91 File Offset: 0x0012B091
		private void OnClickClose()
		{
			Action onClosed = this.m_onClosed;
			if (onClosed != null)
			{
				onClosed();
			}
			base.Close();
		}

		// Token: 0x06003A63 RID: 14947 RVA: 0x0012CEAC File Offset: 0x0012B0AC
		private void OnClickOk()
		{
			if (this.m_selectedServerType == NKCConnectionInfo.LOGIN_SERVER_TYPE.None)
			{
				return;
			}
			if (this.m_selectedServerType == NKCConnectionInfo.CurrentLoginServerType)
			{
				this.OnClickClose();
				return;
			}
			NKCConnectionInfo.CurrentLoginServerType = this.m_selectedServerType;
			NKCConnectionInfo.SaveCurrentLoginServerType();
			if (NKCPatchDownloader.Instance != null)
			{
				NKCPatchDownloader.Instance.VersionCheckStatus = NKCPatchDownloader.VersionStatus.Unchecked;
			}
			this.OnClickClose();
			if (this.m_bMoveToPatcher)
			{
				NKCScenManager.GetScenManager().MoveToPatchScene();
			}
		}

		// Token: 0x06003A64 RID: 14948 RVA: 0x0012CF16 File Offset: 0x0012B116
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x04003504 RID: 13572
		private const string ASSET_BUNDLE_NAME = "AB_UI_LOGIN_SELECT";

		// Token: 0x04003505 RID: 13573
		private const string UI_ASSET_NAME = "AB_UI_LOGIN_SELECT_SERVER";

		// Token: 0x04003506 RID: 13574
		private static NKCPopupSelectServer m_Instance;

		// Token: 0x04003507 RID: 13575
		private RectTransform m_rectTransform;

		// Token: 0x04003508 RID: 13576
		public NKCUIComToggleGroup m_ctglGroup;

		// Token: 0x04003509 RID: 13577
		public NKCUIComToggle m_ctglKorea;

		// Token: 0x0400350A RID: 13578
		public NKCUIComToggle m_ctglGlobal;

		// Token: 0x0400350B RID: 13579
		public NKCUIComStateButton m_csbtnOk;

		// Token: 0x0400350C RID: 13580
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x0400350D RID: 13581
		private NKCConnectionInfo.LOGIN_SERVER_TYPE m_selectedServerType;

		// Token: 0x0400350E RID: 13582
		public Action m_onClosed;

		// Token: 0x0400350F RID: 13583
		private bool m_bShowCloseMenu;

		// Token: 0x04003510 RID: 13584
		private bool m_bMoveToPatcher;
	}
}
