using System;
using ClientPacket.Account;
using Cs.Logging;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000915 RID: 2325
	public class NKCPopupServiceTransferUserData : NKCUIBase
	{
		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x06005D07 RID: 23815 RVA: 0x001CB5D4 File Offset: 0x001C97D4
		public static NKCPopupServiceTransferUserData Instance
		{
			get
			{
				if (NKCPopupServiceTransferUserData.m_Instance == null)
				{
					NKCPopupServiceTransferUserData.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupServiceTransferUserData>("AB_UI_SERVICE_TRANSFER", "AB_UI_SERVICE_TRANSFER_INFO_CONFIRM", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupServiceTransferUserData.CleanupInstance)).GetInstance<NKCPopupServiceTransferUserData>();
					NKCPopupServiceTransferUserData.m_Instance.InitUI();
				}
				return NKCPopupServiceTransferUserData.m_Instance;
			}
		}

		// Token: 0x06005D08 RID: 23816 RVA: 0x001CB623 File Offset: 0x001C9823
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupServiceTransferUserData.m_Instance != null && NKCPopupServiceTransferUserData.m_Instance.IsOpen)
			{
				NKCPopupServiceTransferUserData.m_Instance.Close();
			}
		}

		// Token: 0x06005D09 RID: 23817 RVA: 0x001CB648 File Offset: 0x001C9848
		private static void CleanupInstance()
		{
			NKCPopupServiceTransferUserData.m_Instance = null;
		}

		// Token: 0x170010FC RID: 4348
		// (get) Token: 0x06005D0A RID: 23818 RVA: 0x001CB650 File Offset: 0x001C9850
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170010FD RID: 4349
		// (get) Token: 0x06005D0B RID: 23819 RVA: 0x001CB653 File Offset: 0x001C9853
		public override string MenuName
		{
			get
			{
				return "ServiceTransfer";
			}
		}

		// Token: 0x06005D0C RID: 23820 RVA: 0x001CB65A File Offset: 0x001C985A
		public void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06005D0D RID: 23821 RVA: 0x001CB67C File Offset: 0x001C987C
		public void Open(NKMAccountLinkUserProfile userProfile)
		{
			Log.Debug("[ServiceTransfer][UserData] Open", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/ServiceTransfer/NKCPopupServiceTransferUserData.cs", 69);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			NKCUIComStateButton ok = this.m_Ok;
			if (ok != null)
			{
				ok.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton cancel = this.m_Cancel;
			if (cancel != null)
			{
				cancel.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton ok2 = this.m_Ok;
			if (ok2 != null)
			{
				ok2.PointerClick.AddListener(new UnityAction(this.OnClickSendConfirm));
			}
			NKCUIComStateButton cancel2 = this.m_Cancel;
			if (cancel2 != null)
			{
				cancel2.PointerClick.AddListener(new UnityAction(NKCServiceTransferMgr.CancelServiceTransferProcess));
			}
			NKCUtil.SetLabelText(this.m_lbCreditCount, userProfile.creditCount.ToString());
			NKCUtil.SetLabelText(this.m_lbEterniumCount, userProfile.eterniumCount.ToString());
			NKCUtil.SetLabelText(this.m_lbCashCount, userProfile.cashCount.ToString());
			NKCUtil.SetLabelText(this.m_lbMedalCount, userProfile.medalCount.ToString());
			NKCUtil.SetLabelText(this.m_lbNickName, userProfile.commonProfile.nickname.ToString());
			NKCUtil.SetLabelText(this.m_lbLevel, "Lv." + userProfile.commonProfile.level.ToString());
			NKCUtil.SetLabelText(this.m_lbFriendCode, NKCUtilString.GetFriendCode(userProfile.commonProfile.friendCode));
			base.UIOpened(true);
		}

		// Token: 0x06005D0E RID: 23822 RVA: 0x001CB7DB File Offset: 0x001C99DB
		private void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x06005D0F RID: 23823 RVA: 0x001CB7F0 File Offset: 0x001C99F0
		public override void CloseInternal()
		{
			Log.Debug("[ServiceTransfer][UserData] CloseInternal", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/ServiceTransfer/NKCPopupServiceTransferUserData.cs", 100);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06005D10 RID: 23824 RVA: 0x001CB80F File Offset: 0x001C9A0F
		public void OnClickSendConfirm()
		{
			NKCServiceTransferMgr.Send_NKMPacket_SERVICE_TRANSFER_CONFIRM_REQ();
		}

		// Token: 0x0400491D RID: 18717
		private const string DEBUG_HEADER = "[ServiceTransfer][UserData]";

		// Token: 0x0400491E RID: 18718
		private const string ASSET_BUNDLE_NAME = "AB_UI_SERVICE_TRANSFER";

		// Token: 0x0400491F RID: 18719
		private const string UI_ASSET_NAME = "AB_UI_SERVICE_TRANSFER_INFO_CONFIRM";

		// Token: 0x04004920 RID: 18720
		private static NKCPopupServiceTransferUserData m_Instance;

		// Token: 0x04004921 RID: 18721
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x04004922 RID: 18722
		public NKCUIComStateButton m_Ok;

		// Token: 0x04004923 RID: 18723
		public NKCUIComStateButton m_Cancel;

		// Token: 0x04004924 RID: 18724
		public Text m_lbCreditCount;

		// Token: 0x04004925 RID: 18725
		public Text m_lbEterniumCount;

		// Token: 0x04004926 RID: 18726
		public Text m_lbCashCount;

		// Token: 0x04004927 RID: 18727
		public Text m_lbMedalCount;

		// Token: 0x04004928 RID: 18728
		public Text m_lbNickName;

		// Token: 0x04004929 RID: 18729
		public Text m_lbLevel;

		// Token: 0x0400492A RID: 18730
		public Text m_lbFriendCode;
	}
}
