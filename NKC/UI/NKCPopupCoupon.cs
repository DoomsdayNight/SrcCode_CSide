using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A38 RID: 2616
	public class NKCPopupCoupon : NKCUIBase
	{
		// Token: 0x1700131A RID: 4890
		// (get) Token: 0x06007290 RID: 29328 RVA: 0x002610C4 File Offset: 0x0025F2C4
		public static NKCPopupCoupon Instance
		{
			get
			{
				if (NKCPopupCoupon.m_Instance == null)
				{
					NKCPopupCoupon.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupCoupon>("AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX", "NKM_UI_POPUP_COUPON_BOX", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupCoupon.CleanupInstance)).GetInstance<NKCPopupCoupon>();
					NKCPopupCoupon.m_Instance.InitUI();
				}
				return NKCPopupCoupon.m_Instance;
			}
		}

		// Token: 0x06007291 RID: 29329 RVA: 0x00261113 File Offset: 0x0025F313
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupCoupon.m_Instance != null && NKCPopupCoupon.m_Instance.IsOpen)
			{
				NKCPopupCoupon.m_Instance.Close();
			}
		}

		// Token: 0x06007292 RID: 29330 RVA: 0x00261138 File Offset: 0x0025F338
		private static void CleanupInstance()
		{
			NKCPopupCoupon.m_Instance = null;
		}

		// Token: 0x1700131B RID: 4891
		// (get) Token: 0x06007293 RID: 29331 RVA: 0x00261140 File Offset: 0x0025F340
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700131C RID: 4892
		// (get) Token: 0x06007294 RID: 29332 RVA: 0x00261143 File Offset: 0x0025F343
		public override string MenuName
		{
			get
			{
				return "PopupCoupon";
			}
		}

		// Token: 0x06007295 RID: 29333 RVA: 0x0026114C File Offset: 0x0025F34C
		public void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			this.m_BtnClose.PointerClick.RemoveAllListeners();
			this.m_BtnClose.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_BtnOk.PointerClick.RemoveAllListeners();
			this.m_BtnOk.PointerClick.AddListener(new UnityAction(this.OK));
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(delegate(BaseEventData e)
			{
				base.Close();
			});
			this.m_etBG.triggers.Add(entry);
			this.m_IFCouponCode.onEndEdit.RemoveAllListeners();
			this.m_IFCouponCode.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditCoupon));
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06007296 RID: 29334 RVA: 0x0026122F File Offset: 0x0025F42F
		public void Open(NKCPopupCoupon.OnClickOK onClickOK)
		{
			this.m_OnClickOK = onClickOK;
			this.m_IFCouponCode.text = "";
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			base.UIOpened(true);
		}

		// Token: 0x06007297 RID: 29335 RVA: 0x00261266 File Offset: 0x0025F466
		private void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x06007298 RID: 29336 RVA: 0x0026127B File Offset: 0x0025F47B
		public void OK()
		{
			if (this.m_OnClickOK != null)
			{
				this.m_OnClickOK(this.m_IFCouponCode.text);
			}
		}

		// Token: 0x06007299 RID: 29337 RVA: 0x0026129B File Offset: 0x0025F49B
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x0600729A RID: 29338 RVA: 0x002612A9 File Offset: 0x0025F4A9
		private void OnEndEditCoupon(string input)
		{
			if (NKCInputManager.IsChatSubmitEnter())
			{
				if (!this.m_BtnOk.m_bLock)
				{
					this.OK();
				}
				EventSystem.current.SetSelectedGameObject(null);
			}
		}

		// Token: 0x04005E7D RID: 24189
		private NKCPopupCoupon.OnClickOK m_OnClickOK;

		// Token: 0x04005E7E RID: 24190
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX";

		// Token: 0x04005E7F RID: 24191
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_COUPON_BOX";

		// Token: 0x04005E80 RID: 24192
		private static NKCPopupCoupon m_Instance;

		// Token: 0x04005E81 RID: 24193
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x04005E82 RID: 24194
		public NKCUIComStateButton m_BtnClose;

		// Token: 0x04005E83 RID: 24195
		public NKCUIComStateButton m_BtnOk;

		// Token: 0x04005E84 RID: 24196
		public EventTrigger m_etBG;

		// Token: 0x04005E85 RID: 24197
		public InputField m_IFCouponCode;

		// Token: 0x02001778 RID: 6008
		// (Invoke) Token: 0x0600B365 RID: 45925
		public delegate void OnClickOK(string code);
	}
}
