using System;
using Cs.Logging;
using NKM;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000911 RID: 2321
	public class NKCPopupServiceTransferInput : NKCUIBase
	{
		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x06005CD3 RID: 23763 RVA: 0x001CAE88 File Offset: 0x001C9088
		public static NKCPopupServiceTransferInput Instance
		{
			get
			{
				if (NKCPopupServiceTransferInput.m_Instance == null)
				{
					NKCPopupServiceTransferInput.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupServiceTransferInput>("AB_UI_SERVICE_TRANSFER", "AB_UI_SERVICE_TRANSFER_INPUT", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupServiceTransferInput.CleanupInstance)).GetInstance<NKCPopupServiceTransferInput>();
					NKCPopupServiceTransferInput.m_Instance.InitUI();
				}
				return NKCPopupServiceTransferInput.m_Instance;
			}
		}

		// Token: 0x06005CD4 RID: 23764 RVA: 0x001CAED7 File Offset: 0x001C90D7
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupServiceTransferInput.m_Instance != null && NKCPopupServiceTransferInput.m_Instance.IsOpen)
			{
				NKCPopupServiceTransferInput.m_Instance.Close();
			}
		}

		// Token: 0x06005CD5 RID: 23765 RVA: 0x001CAEFC File Offset: 0x001C90FC
		private static void CleanupInstance()
		{
			NKCPopupServiceTransferInput.m_Instance = null;
		}

		// Token: 0x170010EE RID: 4334
		// (get) Token: 0x06005CD6 RID: 23766 RVA: 0x001CAF04 File Offset: 0x001C9104
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x06005CD7 RID: 23767 RVA: 0x001CAF07 File Offset: 0x001C9107
		public override string MenuName
		{
			get
			{
				return "Input";
			}
		}

		// Token: 0x06005CD8 RID: 23768 RVA: 0x001CAF0E File Offset: 0x001C910E
		private void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06005CD9 RID: 23769 RVA: 0x001CAF30 File Offset: 0x001C9130
		private void SetUI()
		{
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			NKCUIComStateButton csbtnOk = this.m_csbtnOk;
			if (csbtnOk != null)
			{
				csbtnOk.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton csbtnOk2 = this.m_csbtnOk;
			if (csbtnOk2 != null)
			{
				csbtnOk2.PointerClick.AddListener(new UnityAction(this.OnClickOk));
			}
			NKCUIComStateButton csbtnCancel = this.m_csbtnCancel;
			if (csbtnCancel != null)
			{
				csbtnCancel.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton csbtnCancel2 = this.m_csbtnCancel;
			if (csbtnCancel2 != null)
			{
				csbtnCancel2.PointerClick.AddListener(new UnityAction(NKCServiceTransferMgr.CancelServiceTransferProcess));
			}
			this.m_inputField.text = "";
		}

		// Token: 0x06005CDA RID: 23770 RVA: 0x001CAFC8 File Offset: 0x001C91C8
		public void Open()
		{
			Log.Debug("[ServiceTransfer][Input] Open", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/ServiceTransfer/NKCPopupServiceTransferInput.cs", 74);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.SetUI();
			base.UIOpened(true);
		}

		// Token: 0x06005CDB RID: 23771 RVA: 0x001CAFF4 File Offset: 0x001C91F4
		private void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x06005CDC RID: 23772 RVA: 0x001CB009 File Offset: 0x001C9209
		public override void CloseInternal()
		{
			Log.Debug("[ServiceTransfer][Input] CloseInternal", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/ServiceTransfer/NKCPopupServiceTransferInput.cs", 91);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06005CDD RID: 23773 RVA: 0x001CB028 File Offset: 0x001C9228
		private void OnClickOk()
		{
			string text = this.m_inputField.text;
			if (!this.ValidateCodeFormat(text))
			{
				NKCPopupOKCancel.OpenOKBox(NKCStringTable.GetString("SI_PF_SERVICE_TRANSFER_NOTICE_TITLE", false), NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_SERVICE_TRANSFER_WRONG_FORMAT_REGIST_CODE), null, "");
				return;
			}
			NKCServiceTransferMgr.Send_NKMPacket_SERVICE_TRANSFER_CODE_VALIDATION_REQ(text);
		}

		// Token: 0x06005CDE RID: 23774 RVA: 0x001CB074 File Offset: 0x001C9274
		private bool ValidateCodeFormat(string code)
		{
			if (string.IsNullOrEmpty(code))
			{
				return false;
			}
			if (code.Length != 16)
			{
				return false;
			}
			for (int i = 0; i < code.Length; i++)
			{
				if (!char.IsUpper(code, i))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040048FB RID: 18683
		private const string DEBUG_HEADER = "[ServiceTransfer][Input]";

		// Token: 0x040048FC RID: 18684
		private const string ASSET_BUNDLE_NAME = "AB_UI_SERVICE_TRANSFER";

		// Token: 0x040048FD RID: 18685
		private const string UI_ASSET_NAME = "AB_UI_SERVICE_TRANSFER_INPUT";

		// Token: 0x040048FE RID: 18686
		private static NKCPopupServiceTransferInput m_Instance;

		// Token: 0x040048FF RID: 18687
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x04004900 RID: 18688
		public NKCUIComStateButton m_csbtnOk;

		// Token: 0x04004901 RID: 18689
		public NKCUIComStateButton m_csbtnCancel;

		// Token: 0x04004902 RID: 18690
		public InputField m_inputField;
	}
}
