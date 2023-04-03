using System;
using Cs.Logging;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000912 RID: 2322
	public class NKCPopupServiceTransferOutput : NKCUIBase
	{
		// Token: 0x170010F0 RID: 4336
		// (get) Token: 0x06005CE0 RID: 23776 RVA: 0x001CB0BC File Offset: 0x001C92BC
		public static NKCPopupServiceTransferOutput Instance
		{
			get
			{
				if (NKCPopupServiceTransferOutput.m_Instance == null)
				{
					NKCPopupServiceTransferOutput.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupServiceTransferOutput>("AB_UI_SERVICE_TRANSFER", "AB_UI_SERVICE_TRANSFER_CODE_COPY", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupServiceTransferOutput.CleanupInstance)).GetInstance<NKCPopupServiceTransferOutput>();
					NKCPopupServiceTransferOutput.m_Instance.InitUI();
				}
				return NKCPopupServiceTransferOutput.m_Instance;
			}
		}

		// Token: 0x06005CE1 RID: 23777 RVA: 0x001CB10B File Offset: 0x001C930B
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupServiceTransferOutput.m_Instance != null && NKCPopupServiceTransferOutput.m_Instance.IsOpen)
			{
				NKCPopupServiceTransferOutput.m_Instance.Close();
			}
		}

		// Token: 0x06005CE2 RID: 23778 RVA: 0x001CB130 File Offset: 0x001C9330
		private static void CleanupInstance()
		{
			NKCPopupServiceTransferOutput.m_Instance = null;
		}

		// Token: 0x170010F1 RID: 4337
		// (get) Token: 0x06005CE3 RID: 23779 RVA: 0x001CB138 File Offset: 0x001C9338
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x06005CE4 RID: 23780 RVA: 0x001CB13B File Offset: 0x001C933B
		public override string MenuName
		{
			get
			{
				return "CodeOutput";
			}
		}

		// Token: 0x06005CE5 RID: 23781 RVA: 0x001CB142 File Offset: 0x001C9342
		private void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			NKCUtil.SetGameobjectActive(this.m_objReward, false);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06005CE6 RID: 23782 RVA: 0x001CB170 File Offset: 0x001C9370
		private void SetUI(string code, bool bCanGetReward)
		{
			this.SetReward(bCanGetReward);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			NKCUIComStateButton btnOk = this.m_btnOk;
			if (btnOk != null)
			{
				btnOk.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton btnOk2 = this.m_btnOk;
			if (btnOk2 != null)
			{
				btnOk2.PointerClick.AddListener(new UnityAction(NKCServiceTransferMgr.CancelServiceTransferRegistProcess));
			}
			NKCUIComStateButton btnCopy = this.m_btnCopy;
			if (btnCopy != null)
			{
				btnCopy.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton btnCopy2 = this.m_btnCopy;
			if (btnCopy2 != null)
			{
				btnCopy2.PointerClick.AddListener(delegate()
				{
					this.OnClickCopy(code);
				});
			}
			NKCUtil.SetLabelText(this.m_lbCode, code);
		}

		// Token: 0x06005CE7 RID: 23783 RVA: 0x001CB224 File Offset: 0x001C9424
		public void Open(string code, bool bCanGetReward)
		{
			Log.Debug("[ServiceTransfer][Output] Open", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/ServiceTransfer/NKCPopupServiceTransferOutput.cs", 79);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.SetUI(code, bCanGetReward);
			base.UIOpened(true);
		}

		// Token: 0x06005CE8 RID: 23784 RVA: 0x001CB252 File Offset: 0x001C9452
		private void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x06005CE9 RID: 23785 RVA: 0x001CB267 File Offset: 0x001C9467
		public override void CloseInternal()
		{
			Log.Debug("[ServiceTransfer][Output] CloseInternal", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/ServiceTransfer/NKCPopupServiceTransferOutput.cs", 96);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06005CEA RID: 23786 RVA: 0x001CB286 File Offset: 0x001C9486
		private void OnClickCopy(string code)
		{
			TextEditor textEditor = new TextEditor();
			textEditor.text = code;
			textEditor.OnFocus();
			textEditor.Copy();
			if (this.m_bCanGetReward)
			{
				NKCServiceTransferMgr.Send_NKMPacket_SERVICE_TRANSFER_CODE_COPY_REWARD_REQ();
			}
		}

		// Token: 0x06005CEB RID: 23787 RVA: 0x001CB2AC File Offset: 0x001C94AC
		public void SetReward(bool bEnable)
		{
			this.m_bCanGetReward = bEnable;
			NKCUtil.SetGameobjectActive(this.m_objReward, bEnable);
		}

		// Token: 0x04004903 RID: 18691
		private const string DEBUG_HEADER = "[ServiceTransfer][Output]";

		// Token: 0x04004904 RID: 18692
		private const string ASSET_BUNDLE_NAME = "AB_UI_SERVICE_TRANSFER";

		// Token: 0x04004905 RID: 18693
		private const string UI_ASSET_NAME = "AB_UI_SERVICE_TRANSFER_CODE_COPY";

		// Token: 0x04004906 RID: 18694
		private static NKCPopupServiceTransferOutput m_Instance;

		// Token: 0x04004907 RID: 18695
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x04004908 RID: 18696
		public NKCUIComStateButton m_btnOk;

		// Token: 0x04004909 RID: 18697
		public NKCUIComStateButton m_btnCopy;

		// Token: 0x0400490A RID: 18698
		public Text m_lbCode;

		// Token: 0x0400490B RID: 18699
		public GameObject m_objReward;

		// Token: 0x0400490C RID: 18700
		private bool m_bCanGetReward;
	}
}
