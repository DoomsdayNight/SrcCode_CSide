using System;
using Cs.Logging;
using NKC.Publisher;
using UnityEngine.Events;

namespace NKC.UI
{
	// Token: 0x02000914 RID: 2324
	public class NKCPopupServiceTransferTermsJpn : NKCUIBase
	{
		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x06005CFA RID: 23802 RVA: 0x001CB420 File Offset: 0x001C9620
		public static NKCPopupServiceTransferTermsJpn Instance
		{
			get
			{
				if (NKCPopupServiceTransferTermsJpn.m_Instance == null)
				{
					NKCPopupServiceTransferTermsJpn.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupServiceTransferTermsJpn>("AB_UI_SERVICE_TRANSFER", "AB_UI_SERVICE_TRANSFER_TERMS_JPN", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupServiceTransferTermsJpn.CleanupInstance)).GetInstance<NKCPopupServiceTransferTermsJpn>();
					NKCPopupServiceTransferTermsJpn.m_Instance.InitUI();
				}
				return NKCPopupServiceTransferTermsJpn.m_Instance;
			}
		}

		// Token: 0x06005CFB RID: 23803 RVA: 0x001CB46F File Offset: 0x001C966F
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupServiceTransferTermsJpn.m_Instance != null && NKCPopupServiceTransferTermsJpn.m_Instance.IsOpen)
			{
				NKCPopupServiceTransferTermsJpn.m_Instance.Close();
			}
		}

		// Token: 0x06005CFC RID: 23804 RVA: 0x001CB494 File Offset: 0x001C9694
		private static void CleanupInstance()
		{
			NKCPopupServiceTransferTermsJpn.m_Instance = null;
		}

		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x06005CFD RID: 23805 RVA: 0x001CB49C File Offset: 0x001C969C
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170010F9 RID: 4345
		// (get) Token: 0x06005CFE RID: 23806 RVA: 0x001CB49F File Offset: 0x001C969F
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x06005CFF RID: 23807 RVA: 0x001CB4A2 File Offset: 0x001C96A2
		public override string MenuName
		{
			get
			{
				return "ServiceTransfer";
			}
		}

		// Token: 0x06005D00 RID: 23808 RVA: 0x001CB4A9 File Offset: 0x001C96A9
		private void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06005D01 RID: 23809 RVA: 0x001CB4C8 File Offset: 0x001C96C8
		private void SetUI()
		{
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			NKCUtil.SetButtonClickDelegate(this.m_btnOk, new UnityAction(this.OnClickTermsOk));
			NKCUtil.SetButtonClickDelegate(this.m_btnCancel, new UnityAction(NKCServiceTransferMgr.CancelServiceTransferRegistProcess));
			NKCUtil.SetButtonClickDelegate(this.m_btnDetailNJ, delegate()
			{
				NKCPublisherModule.Notice.OpenURL("https://m.nexon.com/terms/12", null);
			});
			NKCUtil.SetButtonClickDelegate(this.m_btnDetailSB, delegate()
			{
				NKCPublisherModule.Notice.OpenURL("https://counterside.com/?page_id=854", null);
			});
		}

		// Token: 0x06005D02 RID: 23810 RVA: 0x001CB562 File Offset: 0x001C9762
		public void Open()
		{
			Log.Debug("[ServiceTransfer][Terms] Open", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/ServiceTransfer/NKCPopupServiceTransferTermsJpn.cs", 77);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.SetUI();
			base.UIOpened(true);
		}

		// Token: 0x06005D03 RID: 23811 RVA: 0x001CB58E File Offset: 0x001C978E
		private void OnClickTermsOk()
		{
			NKCServiceTransferMgr.OpenServiceTransferTerms();
		}

		// Token: 0x06005D04 RID: 23812 RVA: 0x001CB595 File Offset: 0x001C9795
		private void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x06005D05 RID: 23813 RVA: 0x001CB5AA File Offset: 0x001C97AA
		public override void CloseInternal()
		{
			Log.Debug("[ServiceTransfer][Terms] CloseInternal", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/ServiceTransfer/NKCPopupServiceTransferTermsJpn.cs", 99);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x04004914 RID: 18708
		private const string DEBUG_HEADER = "[ServiceTransfer][Terms]";

		// Token: 0x04004915 RID: 18709
		private const string ASSET_BUNDLE_NAME = "AB_UI_SERVICE_TRANSFER";

		// Token: 0x04004916 RID: 18710
		private const string UI_ASSET_NAME = "AB_UI_SERVICE_TRANSFER_TERMS_JPN";

		// Token: 0x04004917 RID: 18711
		private static NKCPopupServiceTransferTermsJpn m_Instance;

		// Token: 0x04004918 RID: 18712
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x04004919 RID: 18713
		public NKCUIComStateButton m_btnOk;

		// Token: 0x0400491A RID: 18714
		public NKCUIComStateButton m_btnCancel;

		// Token: 0x0400491B RID: 18715
		public NKCUIComStateButton m_btnDetailNJ;

		// Token: 0x0400491C RID: 18716
		public NKCUIComStateButton m_btnDetailSB;
	}
}
