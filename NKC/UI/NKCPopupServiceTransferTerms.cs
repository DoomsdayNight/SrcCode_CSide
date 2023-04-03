using System;
using Cs.Logging;
using UnityEngine.Events;

namespace NKC.UI
{
	// Token: 0x02000913 RID: 2323
	public class NKCPopupServiceTransferTerms : NKCUIBase
	{
		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x06005CED RID: 23789 RVA: 0x001CB2CC File Offset: 0x001C94CC
		public static NKCPopupServiceTransferTerms Instance
		{
			get
			{
				if (NKCPopupServiceTransferTerms.m_Instance == null)
				{
					NKCPopupServiceTransferTerms.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupServiceTransferTerms>("AB_UI_SERVICE_TRANSFER", "AB_UI_SERVICE_TRANSFER_TERMS", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupServiceTransferTerms.CleanupInstance)).GetInstance<NKCPopupServiceTransferTerms>();
					NKCPopupServiceTransferTerms.m_Instance.InitUI();
				}
				return NKCPopupServiceTransferTerms.m_Instance;
			}
		}

		// Token: 0x06005CEE RID: 23790 RVA: 0x001CB31B File Offset: 0x001C951B
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupServiceTransferTerms.m_Instance != null && NKCPopupServiceTransferTerms.m_Instance.IsOpen)
			{
				NKCPopupServiceTransferTerms.m_Instance.Close();
			}
		}

		// Token: 0x06005CEF RID: 23791 RVA: 0x001CB340 File Offset: 0x001C9540
		private static void CleanupInstance()
		{
			NKCPopupServiceTransferTerms.m_Instance = null;
		}

		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x06005CF0 RID: 23792 RVA: 0x001CB348 File Offset: 0x001C9548
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170010F5 RID: 4341
		// (get) Token: 0x06005CF1 RID: 23793 RVA: 0x001CB34B File Offset: 0x001C954B
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x06005CF2 RID: 23794 RVA: 0x001CB34E File Offset: 0x001C954E
		public override string MenuName
		{
			get
			{
				return "ServiceTransfer";
			}
		}

		// Token: 0x06005CF3 RID: 23795 RVA: 0x001CB355 File Offset: 0x001C9555
		private void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06005CF4 RID: 23796 RVA: 0x001CB374 File Offset: 0x001C9574
		private void SetUI()
		{
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			NKCUtil.SetButtonClickDelegate(this.m_btnOk, new UnityAction(this.OnClickTermsOk));
			NKCUtil.SetButtonClickDelegate(this.m_btnCancel, new UnityAction(NKCServiceTransferMgr.CancelServiceTransferRegistProcess));
		}

		// Token: 0x06005CF5 RID: 23797 RVA: 0x001CB3AF File Offset: 0x001C95AF
		public void Open()
		{
			Log.Debug("[ServiceTransfer][Terms] Open", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/ServiceTransfer/NKCPopupServiceTransferTerms.cs", 70);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.SetUI();
			base.UIOpened(true);
		}

		// Token: 0x06005CF6 RID: 23798 RVA: 0x001CB3DB File Offset: 0x001C95DB
		private void OnClickTermsOk()
		{
			NKCServiceTransferMgr.Send_NKMPacket_SERVICE_TRANSFER_REGIST_CODE_REQ();
		}

		// Token: 0x06005CF7 RID: 23799 RVA: 0x001CB3E2 File Offset: 0x001C95E2
		private void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x06005CF8 RID: 23800 RVA: 0x001CB3F7 File Offset: 0x001C95F7
		public override void CloseInternal()
		{
			Log.Debug("[ServiceTransfer][Terms] CloseInternal", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/ServiceTransfer/NKCPopupServiceTransferTerms.cs", 92);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x0400490D RID: 18701
		private const string DEBUG_HEADER = "[ServiceTransfer][Terms]";

		// Token: 0x0400490E RID: 18702
		private const string ASSET_BUNDLE_NAME = "AB_UI_SERVICE_TRANSFER";

		// Token: 0x0400490F RID: 18703
		private const string UI_ASSET_NAME = "AB_UI_SERVICE_TRANSFER_TERMS";

		// Token: 0x04004910 RID: 18704
		private static NKCPopupServiceTransferTerms m_Instance;

		// Token: 0x04004911 RID: 18705
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x04004912 RID: 18706
		public NKCUIComStateButton m_btnOk;

		// Token: 0x04004913 RID: 18707
		public NKCUIComStateButton m_btnCancel;
	}
}
