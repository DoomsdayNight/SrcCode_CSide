using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A8D RID: 2701
	public class NKCPopupUnitReviewDelete : NKCUIBase
	{
		// Token: 0x170013FA RID: 5114
		// (get) Token: 0x06007784 RID: 30596 RVA: 0x0027BF38 File Offset: 0x0027A138
		public static NKCPopupUnitReviewDelete Instance
		{
			get
			{
				if (NKCPopupUnitReviewDelete.m_Instance == null)
				{
					NKCPopupUnitReviewDelete.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupUnitReviewDelete>("ab_ui_nkm_ui_unit_review", "NKM_UI_UNIT_REVIEW_POPUP_POST_DELETE", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupUnitReviewDelete.CleanupInstance)).GetInstance<NKCPopupUnitReviewDelete>();
					NKCPopupUnitReviewDelete.m_Instance.InitUI();
				}
				return NKCPopupUnitReviewDelete.m_Instance;
			}
		}

		// Token: 0x06007785 RID: 30597 RVA: 0x0027BF87 File Offset: 0x0027A187
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupUnitReviewDelete.m_Instance != null && NKCPopupUnitReviewDelete.m_Instance.IsOpen)
			{
				NKCPopupUnitReviewDelete.m_Instance.Close();
			}
		}

		// Token: 0x06007786 RID: 30598 RVA: 0x0027BFAC File Offset: 0x0027A1AC
		private static void CleanupInstance()
		{
			NKCPopupUnitReviewDelete.m_Instance = null;
		}

		// Token: 0x170013FB RID: 5115
		// (get) Token: 0x06007787 RID: 30599 RVA: 0x0027BFB4 File Offset: 0x0027A1B4
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170013FC RID: 5116
		// (get) Token: 0x06007788 RID: 30600 RVA: 0x0027BFB7 File Offset: 0x0027A1B7
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_POPUP_UNIT_REVIEW_DELETE;
			}
		}

		// Token: 0x06007789 RID: 30601 RVA: 0x0027BFBE File Offset: 0x0027A1BE
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x0600778A RID: 30602 RVA: 0x0027BFCC File Offset: 0x0027A1CC
		private void InitUI()
		{
			this.m_openAni = new NKCUIOpenAnimator(base.gameObject);
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_btnDelete.PointerClick.RemoveAllListeners();
			this.m_btnDelete.PointerClick.AddListener(new UnityAction(this.OnClickDelete));
			NKCUtil.SetHotkey(this.m_btnDelete, HotkeyEventType.Confirm, null, false);
			this.m_btnCancel.PointerClick.RemoveAllListeners();
			this.m_btnCancel.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_bInitComplete = true;
		}

		// Token: 0x0600778B RID: 30603 RVA: 0x0027C084 File Offset: 0x0027A284
		public void OpenUI(string title, string desc, NKCPopupUnitReviewDelete.OnButton onDeleteButton)
		{
			if (!this.m_bInitComplete)
			{
				this.InitUI();
			}
			this.m_openAni.PlayOpenAni();
			if (onDeleteButton != null)
			{
				this.dOnDeleteButton = onDeleteButton;
			}
			this.m_lbTitle.text = title;
			this.m_lbDesc.text = desc;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			base.UIOpened(true);
		}

		// Token: 0x0600778C RID: 30604 RVA: 0x0027C0DF File Offset: 0x0027A2DF
		private void OnClickDelete()
		{
			NKCPopupUnitReviewDelete.OnButton onButton = this.dOnDeleteButton;
			if (onButton != null)
			{
				onButton();
			}
			base.Close();
		}

		// Token: 0x0600778D RID: 30605 RVA: 0x0027C0F8 File Offset: 0x0027A2F8
		private void Update()
		{
			if (base.IsOpen)
			{
				this.m_openAni.Update();
			}
		}

		// Token: 0x04006416 RID: 25622
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_unit_review";

		// Token: 0x04006417 RID: 25623
		private const string UI_ASSET_NAME = "NKM_UI_UNIT_REVIEW_POPUP_POST_DELETE";

		// Token: 0x04006418 RID: 25624
		private static NKCPopupUnitReviewDelete m_Instance;

		// Token: 0x04006419 RID: 25625
		private NKCUIOpenAnimator m_openAni;

		// Token: 0x0400641A RID: 25626
		public NKCUIComStateButton m_btnClose;

		// Token: 0x0400641B RID: 25627
		public NKCUIComStateButton m_btnDelete;

		// Token: 0x0400641C RID: 25628
		public NKCUIComStateButton m_btnCancel;

		// Token: 0x0400641D RID: 25629
		public Text m_lbTitle;

		// Token: 0x0400641E RID: 25630
		public Text m_lbDesc;

		// Token: 0x0400641F RID: 25631
		private NKCPopupUnitReviewDelete.OnButton dOnDeleteButton;

		// Token: 0x04006420 RID: 25632
		private bool m_bInitComplete;

		// Token: 0x020017EA RID: 6122
		// (Invoke) Token: 0x0600B492 RID: 46226
		public delegate void OnButton();
	}
}
