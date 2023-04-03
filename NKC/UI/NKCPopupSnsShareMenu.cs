using System;
using NKC.Publisher;
using UnityEngine.EventSystems;

namespace NKC.UI
{
	// Token: 0x02000A89 RID: 2697
	public class NKCPopupSnsShareMenu : NKCUIBase
	{
		// Token: 0x170013F1 RID: 5105
		// (get) Token: 0x06007754 RID: 30548 RVA: 0x0027B084 File Offset: 0x00279284
		public static NKCPopupSnsShareMenu Instance
		{
			get
			{
				if (NKCPopupSnsShareMenu.m_Instance == null)
				{
					NKCPopupSnsShareMenu.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupSnsShareMenu>("AB_UI_NKM_UI_POPUP_SHARE", "NKM_UI_POPUP_BOX_SHARE", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupSnsShareMenu.CleanupInstance)).GetInstance<NKCPopupSnsShareMenu>();
					NKCPopupSnsShareMenu.m_Instance.InitUI();
				}
				return NKCPopupSnsShareMenu.m_Instance;
			}
		}

		// Token: 0x06007755 RID: 30549 RVA: 0x0027B0D3 File Offset: 0x002792D3
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupSnsShareMenu.m_Instance != null && NKCPopupSnsShareMenu.m_Instance.IsOpen)
			{
				NKCPopupSnsShareMenu.m_Instance.Close();
			}
		}

		// Token: 0x06007756 RID: 30550 RVA: 0x0027B0F8 File Offset: 0x002792F8
		private static void CleanupInstance()
		{
			NKCPopupSnsShareMenu.m_Instance = null;
		}

		// Token: 0x170013F2 RID: 5106
		// (get) Token: 0x06007757 RID: 30551 RVA: 0x0027B100 File Offset: 0x00279300
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170013F3 RID: 5107
		// (get) Token: 0x06007758 RID: 30552 RVA: 0x0027B103 File Offset: 0x00279303
		public override string MenuName
		{
			get
			{
				return "PopupSnsShareMenu";
			}
		}

		// Token: 0x06007759 RID: 30553 RVA: 0x0027B10A File Offset: 0x0027930A
		private void OnClickShare(NKCPublisherModule.SNS_SHARE_TYPE eSNS_SHARE_TYPE)
		{
			base.Close();
			if (this.m_dOnClickSnsIcon != null)
			{
				this.m_dOnClickSnsIcon(eSNS_SHARE_TYPE);
			}
		}

		// Token: 0x0600775A RID: 30554 RVA: 0x0027B128 File Offset: 0x00279328
		public void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			this.m_BtnWechat.PointerClick.RemoveAllListeners();
			this.m_BtnWechat.PointerClick.AddListener(delegate()
			{
				this.OnClickShare(NKCPublisherModule.SNS_SHARE_TYPE.SST_WECHAT);
			});
			this.m_BtnWechatMoments.PointerClick.RemoveAllListeners();
			this.m_BtnWechatMoments.PointerClick.AddListener(delegate()
			{
				this.OnClickShare(NKCPublisherModule.SNS_SHARE_TYPE.SST_WECHAT_MOMENTS);
			});
			this.m_BtnQQ.PointerClick.RemoveAllListeners();
			this.m_BtnQQ.PointerClick.AddListener(delegate()
			{
				this.OnClickShare(NKCPublisherModule.SNS_SHARE_TYPE.SST_QQ);
			});
			this.m_BtnWeibo.PointerClick.RemoveAllListeners();
			this.m_BtnWeibo.PointerClick.AddListener(delegate()
			{
				this.OnClickShare(NKCPublisherModule.SNS_SHARE_TYPE.SST_WEIBO);
			});
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(delegate()
			{
				base.Close();
			});
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(delegate(BaseEventData e)
			{
				base.Close();
			});
			this.m_etBG.triggers.Add(entry);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x0600775B RID: 30555 RVA: 0x0027B263 File Offset: 0x00279463
		public void Open(NKCPopupSnsShareMenu.OnClickSnsIcon dOnClickSnsIcon)
		{
			this.m_dOnClickSnsIcon = dOnClickSnsIcon;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			base.UIOpened(true);
		}

		// Token: 0x0600775C RID: 30556 RVA: 0x0027B28A File Offset: 0x0027948A
		private void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x0600775D RID: 30557 RVA: 0x0027B29F File Offset: 0x0027949F
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x040063DE RID: 25566
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_POPUP_SHARE";

		// Token: 0x040063DF RID: 25567
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_BOX_SHARE";

		// Token: 0x040063E0 RID: 25568
		private static NKCPopupSnsShareMenu m_Instance;

		// Token: 0x040063E1 RID: 25569
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x040063E2 RID: 25570
		public NKCUIComStateButton m_BtnWechat;

		// Token: 0x040063E3 RID: 25571
		public NKCUIComStateButton m_BtnWechatMoments;

		// Token: 0x040063E4 RID: 25572
		public NKCUIComStateButton m_BtnQQ;

		// Token: 0x040063E5 RID: 25573
		public NKCUIComStateButton m_BtnWeibo;

		// Token: 0x040063E6 RID: 25574
		public EventTrigger m_etBG;

		// Token: 0x040063E7 RID: 25575
		public NKCUIComStateButton m_btnClose;

		// Token: 0x040063E8 RID: 25576
		private NKCPopupSnsShareMenu.OnClickSnsIcon m_dOnClickSnsIcon;

		// Token: 0x020017E6 RID: 6118
		// (Invoke) Token: 0x0600B486 RID: 46214
		public delegate void OnClickSnsIcon(NKCPublisherModule.SNS_SHARE_TYPE eSNS_SHARE_TYPE);
	}
}
