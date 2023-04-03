using System;
using NKC.Publisher;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using ZenFulcrum.EmbeddedBrowser;

namespace NKC.UI
{
	// Token: 0x02000A71 RID: 2673
	public class NKCPopupNoticeWeb : NKCUIBase
	{
		// Token: 0x170013A9 RID: 5033
		// (get) Token: 0x060075F5 RID: 30197 RVA: 0x002738BC File Offset: 0x00271ABC
		public static NKCPopupNoticeWeb Instance
		{
			get
			{
				if (NKCPopupNoticeWeb.m_Instance == null)
				{
					NKCPopupNoticeWeb.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupNoticeWeb>("AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX", "NKM_UI_POPUP_WEB_NOTICE", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupNoticeWeb.CleanupInstance)).GetInstance<NKCPopupNoticeWeb>();
					NKCPopupNoticeWeb.m_Instance.InitUI();
				}
				return NKCPopupNoticeWeb.m_Instance;
			}
		}

		// Token: 0x060075F6 RID: 30198 RVA: 0x0027390B File Offset: 0x00271B0B
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupNoticeWeb.m_Instance != null && NKCPopupNoticeWeb.m_Instance.IsOpen)
			{
				NKCPopupNoticeWeb.m_Instance.Close();
			}
		}

		// Token: 0x060075F7 RID: 30199 RVA: 0x00273930 File Offset: 0x00271B30
		private static void CleanupInstance()
		{
			NKCPopupNoticeWeb.m_Instance = null;
		}

		// Token: 0x170013AA RID: 5034
		// (get) Token: 0x060075F8 RID: 30200 RVA: 0x00273938 File Offset: 0x00271B38
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170013AB RID: 5035
		// (get) Token: 0x060075F9 RID: 30201 RVA: 0x0027393B File Offset: 0x00271B3B
		public override string MenuName
		{
			get
			{
				return "PopupWebNotice";
			}
		}

		// Token: 0x060075FA RID: 30202 RVA: 0x00273944 File Offset: 0x00271B44
		private void SetUrl(string url)
		{
			if (this.m_Browser == null)
			{
				this.m_Browser = this.m_objWebView.AddComponent<Browser>();
				this.m_Browser.Resize(1312, 656);
				this.m_objWebView.AddComponent<PointerUIGUI>();
				this.m_objWebView.AddComponent<CursorRendererOS>();
			}
			if (this.m_Browser == null)
			{
				return;
			}
			this.m_Browser.Url = url;
			this.m_HomeUrl = url;
		}

		// Token: 0x060075FB RID: 30203 RVA: 0x002739C0 File Offset: 0x00271BC0
		public void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			this.m_BtnClose.PointerClick.RemoveAllListeners();
			this.m_BtnClose.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_csbtnHome.PointerClick.RemoveAllListeners();
			this.m_csbtnHome.PointerClick.AddListener(new UnityAction(this.OnClickHome));
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(delegate(BaseEventData e)
			{
				base.Close();
			});
			this.m_etBG.triggers.Add(entry);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060075FC RID: 30204 RVA: 0x00273A77 File Offset: 0x00271C77
		private void OnClickHome()
		{
			this.SetUrl(this.m_HomeUrl);
		}

		// Token: 0x060075FD RID: 30205 RVA: 0x00273A85 File Offset: 0x00271C85
		public void Open(string url, NKCPublisherModule.OnComplete onWindowClosed, bool bPatcher = false)
		{
			if (bPatcher)
			{
				this.InitUI();
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.SetUrl(url);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			this.dOnWindowClosed = onWindowClosed;
			base.UIOpened(true);
		}

		// Token: 0x060075FE RID: 30206 RVA: 0x00273ABC File Offset: 0x00271CBC
		private void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x060075FF RID: 30207 RVA: 0x00273AD1 File Offset: 0x00271CD1
		public override void CloseInternal()
		{
			NKCPublisherModule.OnComplete onComplete = this.dOnWindowClosed;
			if (onComplete != null)
			{
				onComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x0400625D RID: 25181
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX";

		// Token: 0x0400625E RID: 25182
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_WEB_NOTICE";

		// Token: 0x0400625F RID: 25183
		private static NKCPopupNoticeWeb m_Instance;

		// Token: 0x04006260 RID: 25184
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x04006261 RID: 25185
		public NKCUIComStateButton m_BtnClose;

		// Token: 0x04006262 RID: 25186
		public NKCUIComStateButton m_csbtnHome;

		// Token: 0x04006263 RID: 25187
		public EventTrigger m_etBG;

		// Token: 0x04006264 RID: 25188
		public GameObject m_objWebView;

		// Token: 0x04006265 RID: 25189
		private string m_HomeUrl = "";

		// Token: 0x04006266 RID: 25190
		private NKCPublisherModule.OnComplete dOnWindowClosed;

		// Token: 0x04006267 RID: 25191
		private Browser m_Browser;
	}
}
