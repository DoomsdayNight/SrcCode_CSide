using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A7C RID: 2684
	public class NKCPopupReviewInduce : NKCUIBase
	{
		// Token: 0x170013D2 RID: 5074
		// (get) Token: 0x060076D5 RID: 30421 RVA: 0x00278D94 File Offset: 0x00276F94
		public static NKCPopupReviewInduce Instance
		{
			get
			{
				if (NKCPopupReviewInduce.m_Instance == null)
				{
					NKCPopupReviewInduce.m_Instance = NKCUIManager.OpenNewInstance<NKCUISlotListViewer>("ab_ui_nkm_ui_popup_ok_cancel_box", "NKM_UI_POPUP_REVIEW_INDUCE", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupReviewInduce.CleanupInstance)).GetInstance<NKCPopupReviewInduce>();
					NKCPopupReviewInduce.m_Instance.InitUI();
				}
				return NKCPopupReviewInduce.m_Instance;
			}
		}

		// Token: 0x060076D6 RID: 30422 RVA: 0x00278DE3 File Offset: 0x00276FE3
		private static void CleanupInstance()
		{
			NKCPopupReviewInduce.m_Instance = null;
		}

		// Token: 0x170013D3 RID: 5075
		// (get) Token: 0x060076D7 RID: 30423 RVA: 0x00278DEB File Offset: 0x00276FEB
		public override string MenuName
		{
			get
			{
				return "리뷰 확인/취소 팝업";
			}
		}

		// Token: 0x170013D4 RID: 5076
		// (get) Token: 0x060076D8 RID: 30424 RVA: 0x00278DF2 File Offset: 0x00276FF2
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x060076D9 RID: 30425 RVA: 0x00278DF8 File Offset: 0x00276FF8
		public override void OnBackButton()
		{
			NKCPopupReviewInduce.eOpenType type = this.m_Type;
			if (type == NKCPopupReviewInduce.eOpenType.OK)
			{
				this.OnOK();
				return;
			}
			if (type != NKCPopupReviewInduce.eOpenType.OKCancel)
			{
				return;
			}
			this.OnCancel();
		}

		// Token: 0x060076DA RID: 30426 RVA: 0x00278E24 File Offset: 0x00277024
		private void InitUI()
		{
			if (this.m_cbtnOKCancel_OK != null)
			{
				this.m_cbtnOKCancel_OK.PointerClick.RemoveAllListeners();
				this.m_cbtnOKCancel_OK.PointerClick.AddListener(new UnityAction(this.OnOK));
			}
			if (this.m_cbtnOKCancel_Cancel != null)
			{
				this.m_cbtnOKCancel_Cancel.PointerClick.RemoveAllListeners();
				this.m_cbtnOKCancel_Cancel.PointerClick.AddListener(new UnityAction(this.OnCancel));
			}
			base.gameObject.SetActive(false);
			this.m_bInitComplete = true;
		}

		// Token: 0x060076DB RID: 30427 RVA: 0x00278EB8 File Offset: 0x002770B8
		public void OpenOKCancel(NKCPopupReviewInduce.OnButton onOkButton, NKCPopupReviewInduce.OnButton onCancelButton = null)
		{
			if (!this.m_bInitComplete)
			{
				this.InitUI();
			}
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(delegate(BaseEventData data)
			{
				this.OnCancel();
			});
			if (this.m_etBG != null)
			{
				this.m_etBG.triggers.Clear();
				this.m_etBG.triggers.Add(entry);
			}
			this.dOnOKButton = onOkButton;
			this.dOnCancelButton = onCancelButton;
			base.gameObject.SetActive(true);
			this.m_Type = NKCPopupReviewInduce.eOpenType.OKCancel;
			base.UIOpened(true);
		}

		// Token: 0x060076DC RID: 30428 RVA: 0x00278F4E File Offset: 0x0027714E
		public void ClosePopupBox()
		{
			base.Close();
		}

		// Token: 0x060076DD RID: 30429 RVA: 0x00278F56 File Offset: 0x00277156
		public void OnOK()
		{
			base.Close();
			if (this.dOnOKButton != null)
			{
				this.dOnOKButton();
			}
		}

		// Token: 0x060076DE RID: 30430 RVA: 0x00278F71 File Offset: 0x00277171
		public void OnCancel()
		{
			base.Close();
			if (this.dOnCancelButton != null)
			{
				this.dOnCancelButton();
			}
		}

		// Token: 0x060076DF RID: 30431 RVA: 0x00278F8C File Offset: 0x0027718C
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x060076E0 RID: 30432 RVA: 0x00278F9C File Offset: 0x0027719C
		public void SetOnTop(bool bOverDevConsle = false)
		{
			GameObject gameObject = GameObject.Find("NKM_SCEN_UI_FRONT");
			Transform transform = (gameObject != null) ? gameObject.transform : null;
			if (transform != null)
			{
				base.gameObject.transform.SetParent(transform);
				if (bOverDevConsle)
				{
					base.gameObject.transform.SetAsLastSibling();
					return;
				}
				base.gameObject.transform.SetSiblingIndex(transform.childCount - 2);
			}
		}

		// Token: 0x0400635B RID: 25435
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_ok_cancel_box";

		// Token: 0x0400635C RID: 25436
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_REVIEW_INDUCE";

		// Token: 0x0400635D RID: 25437
		private static NKCPopupReviewInduce m_Instance;

		// Token: 0x0400635E RID: 25438
		private NKCPopupReviewInduce.eOpenType m_Type;

		// Token: 0x0400635F RID: 25439
		[Header("BG")]
		public EventTrigger m_etBG;

		// Token: 0x04006360 RID: 25440
		[Header("OK/Cancel Box")]
		public NKCUIComButton m_cbtnOKCancel_OK;

		// Token: 0x04006361 RID: 25441
		public NKCUIComButton m_cbtnOKCancel_Cancel;

		// Token: 0x04006362 RID: 25442
		public Text m_lbBtnOKCancel_OK;

		// Token: 0x04006363 RID: 25443
		public Text m_lbBtnOKCancel_Cancel;

		// Token: 0x04006364 RID: 25444
		private NKCPopupReviewInduce.OnButton dOnOKButton;

		// Token: 0x04006365 RID: 25445
		private NKCPopupReviewInduce.OnButton dOnCancelButton;

		// Token: 0x04006366 RID: 25446
		private bool m_bInitComplete;

		// Token: 0x020017DB RID: 6107
		private enum eOpenType
		{
			// Token: 0x0400A79F RID: 42911
			OK,
			// Token: 0x0400A7A0 RID: 42912
			OKCancel
		}

		// Token: 0x020017DC RID: 6108
		// (Invoke) Token: 0x0600B468 RID: 46184
		public delegate void OnButton();
	}
}
