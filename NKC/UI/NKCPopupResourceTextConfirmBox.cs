using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A7A RID: 2682
	public class NKCPopupResourceTextConfirmBox : NKCUIBase
	{
		// Token: 0x170013CC RID: 5068
		// (get) Token: 0x060076BE RID: 30398 RVA: 0x00278778 File Offset: 0x00276978
		public static NKCPopupResourceTextConfirmBox Instance
		{
			get
			{
				if (NKCPopupResourceTextConfirmBox.m_Instance == null)
				{
					NKCPopupResourceTextConfirmBox.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupResourceTextConfirmBox>("AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX", "NKM_UI_POPUP_RESOURCE_USE_CONFIRM_TXT", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupResourceTextConfirmBox.CleanupInstance)).GetInstance<NKCPopupResourceTextConfirmBox>();
					NKCPopupResourceTextConfirmBox instance = NKCPopupResourceTextConfirmBox.m_Instance;
					if (instance != null)
					{
						instance.Init();
					}
				}
				return NKCPopupResourceTextConfirmBox.m_Instance;
			}
		}

		// Token: 0x060076BF RID: 30399 RVA: 0x002787CD File Offset: 0x002769CD
		private static void CleanupInstance()
		{
			NKCPopupResourceTextConfirmBox.m_Instance = null;
		}

		// Token: 0x170013CD RID: 5069
		// (get) Token: 0x060076C0 RID: 30400 RVA: 0x002787D5 File Offset: 0x002769D5
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170013CE RID: 5070
		// (get) Token: 0x060076C1 RID: 30401 RVA: 0x002787D8 File Offset: 0x002769D8
		public override string MenuName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x060076C2 RID: 30402 RVA: 0x002787E0 File Offset: 0x002769E0
		private void Init()
		{
			if (this.m_cbtnOK != null)
			{
				this.m_cbtnOK.PointerClick.RemoveAllListeners();
				this.m_cbtnOK.PointerClick.AddListener(new UnityAction(this.OnOK));
				NKCUtil.SetHotkey(this.m_cbtnOK, HotkeyEventType.Confirm);
			}
			if (this.m_cbtnCancel != null)
			{
				this.m_cbtnCancel.PointerClick.RemoveAllListeners();
				this.m_cbtnCancel.PointerClick.AddListener(new UnityAction(this.OnCancel));
			}
		}

		// Token: 0x060076C3 RID: 30403 RVA: 0x00278870 File Offset: 0x00276A70
		public void Open(string Title, string Content, string strPoint, NKCPopupResourceTextConfirmBox.OnButton onOkButton, NKCPopupResourceTextConfirmBox.OnButton onCancelButton = null)
		{
			if (this.m_NKCUIOpenAnimator == null)
			{
				this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			}
			NKCUtil.SetLabelText(this.m_lbTitle, Title);
			NKCUtil.SetLabelText(this.m_lbMessage, Content);
			NKCUtil.SetGameobjectActive(this.m_objPoint, true);
			NKCUtil.SetLabelText(this.m_txtPoint, strPoint);
			this.dOnOKButton = onOkButton;
			this.dOnCancelButton = onCancelButton;
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			base.UIOpened(true);
		}

		// Token: 0x060076C4 RID: 30404 RVA: 0x002788E8 File Offset: 0x00276AE8
		private void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x060076C5 RID: 30405 RVA: 0x002788FD File Offset: 0x00276AFD
		public void OnOK()
		{
			base.Close();
			if (this.dOnOKButton != null)
			{
				this.dOnOKButton();
			}
		}

		// Token: 0x060076C6 RID: 30406 RVA: 0x00278918 File Offset: 0x00276B18
		public void OnCancel()
		{
			base.Close();
			if (this.dOnCancelButton != null)
			{
				this.dOnCancelButton();
			}
		}

		// Token: 0x060076C7 RID: 30407 RVA: 0x00278933 File Offset: 0x00276B33
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x0400633F RID: 25407
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX";

		// Token: 0x04006340 RID: 25408
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_RESOURCE_USE_CONFIRM_TXT";

		// Token: 0x04006341 RID: 25409
		private static NKCPopupResourceTextConfirmBox m_Instance;

		// Token: 0x04006342 RID: 25410
		private NKCPopupResourceTextConfirmBox.OnButton dOnOKButton;

		// Token: 0x04006343 RID: 25411
		private NKCPopupResourceTextConfirmBox.OnButton dOnCancelButton;

		// Token: 0x04006344 RID: 25412
		public Text m_lbTitle;

		// Token: 0x04006345 RID: 25413
		public Text m_lbMessage;

		// Token: 0x04006346 RID: 25414
		public GameObject m_objPoint;

		// Token: 0x04006347 RID: 25415
		public Text m_txtPoint;

		// Token: 0x04006348 RID: 25416
		public NKCUIComButton m_cbtnOK;

		// Token: 0x04006349 RID: 25417
		public NKCUIComButton m_cbtnCancel;

		// Token: 0x0400634A RID: 25418
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x020017D9 RID: 6105
		// (Invoke) Token: 0x0600B460 RID: 46176
		public delegate void OnButton();
	}
}
