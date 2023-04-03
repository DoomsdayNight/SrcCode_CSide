using System;
using ClientPacket.Account;
using Cs.Logging;
using NKC.PacketHandler;
using NKC.Publisher;
using NKC.UI;
using NKM;

namespace NKC
{
	// Token: 0x0200073C RID: 1852
	public static class NKCServiceTransferMgr
	{
		// Token: 0x06004A26 RID: 18982 RVA: 0x001640DD File Offset: 0x001622DD
		public static void StartServiceTransferRegistProcess()
		{
			if (NKCPublisherModule.Auth.IsGuest())
			{
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_SERVICE_TRANSFER_GUEST_ACCOUNT, null, "");
				return;
			}
			NKCPopupOKCancel.ClosePopupBox();
			if (NKMContentsVersionManager.HasCountryTag(CountryTagType.JPN))
			{
				NKCServiceTransferMgr.OpenServiceTransferTermsJpn();
				return;
			}
			NKCServiceTransferMgr.OpenServiceTransferTerms();
		}

		// Token: 0x06004A27 RID: 18983 RVA: 0x00164114 File Offset: 0x00162314
		public static void CancelServiceTransferRegistProcess()
		{
			NKMPopUpBox.CloseWaitBox();
			NKCPopupOKCancel.ClosePopupBox();
			NKCServiceTransferMgr.CloseAllServiceTransferRegist();
		}

		// Token: 0x06004A28 RID: 18984 RVA: 0x00164128 File Offset: 0x00162328
		public static void Send_NKMPacket_SERVICE_TRANSFER_REGIST_CODE_REQ()
		{
			Log.Debug("[ServiceTransferRegist] Send_NKMPacket_SERVICE_TRANSFER_REGIST_CODE_REQ]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/ServiceTransfer/NKCServiceTransferMgr.cs", 44);
			NKMPacket_SERVICE_TRANSFER_REGIST_CODE_REQ packet = new NKMPacket_SERVICE_TRANSFER_REGIST_CODE_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06004A29 RID: 18985 RVA: 0x0016415F File Offset: 0x0016235F
		public static void OnRecv(NKMPacket_SERVICE_TRANSFER_REGIST_CODE_ACK sPacket)
		{
			Log.Debug("[ServiceTransferRegist] OnRecv NKMPacket_SERVICE_TRANSFER_REGIST_CODE_ACK", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/ServiceTransfer/NKCServiceTransferMgr.cs", 52);
			NKMPopUpBox.CloseWaitBox();
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCServiceTransferMgr.OpenServiceTransferOutput(sPacket.code, sPacket.canReceiveReward);
		}

		// Token: 0x06004A2A RID: 18986 RVA: 0x001641A0 File Offset: 0x001623A0
		public static void Send_NKMPacket_SERVICE_TRANSFER_CODE_COPY_REWARD_REQ()
		{
			Log.Debug("[ServiceTransferRegist]  Send_NKMPacket_SERVICE_TRANSFER_CODE_COPY_REWARD_REQ]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/ServiceTransfer/NKCServiceTransferMgr.cs", 63);
			NKMPacket_SERVICE_TRANSFER_CODE_COPY_REWARD_REQ packet = new NKMPacket_SERVICE_TRANSFER_CODE_COPY_REWARD_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06004A2B RID: 18987 RVA: 0x001641D7 File Offset: 0x001623D7
		public static void OnRecv(NKMPacket_SERVICE_TRANSFER_CODE_COPY_REWARD_ACK sPacket)
		{
			Log.Debug("[ServiceTransferRegist] OnRecv NKMPacket_SERVICE_TRANSFER_CODE_COPY_REWARD_ACK", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/ServiceTransfer/NKCServiceTransferMgr.cs", 71);
			NKMPopUpBox.CloseWaitBox();
			NKCPopupServiceTransferOutput.Instance.SetReward(false);
			NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, int.MinValue);
		}

		// Token: 0x06004A2C RID: 18988 RVA: 0x0016420D File Offset: 0x0016240D
		public static void StartServiceTransferProcess()
		{
			if (NKCPublisherModule.Auth.IsGuest())
			{
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_SERVICE_TRANSFER_GUEST_ACCOUNT, null, "");
				return;
			}
			NKCServiceTransferMgr.Send_NKMPacket_SERVICE_TRANSFER_USER_VALIDATION_REQ();
		}

		// Token: 0x06004A2D RID: 18989 RVA: 0x00164231 File Offset: 0x00162431
		public static void CancelServiceTransferProcess()
		{
			NKMPopUpBox.CloseWaitBox();
			NKCPopupOKCancel.ClosePopupBox();
			NKCServiceTransferMgr.CloseAllServiceTransfer();
		}

		// Token: 0x06004A2E RID: 18990 RVA: 0x00164244 File Offset: 0x00162444
		public static void Send_NKMPacket_SERVICE_TRANSFER_USER_VALIDATION_REQ()
		{
			Log.Debug("[ServiceTransfer] Send_NKMPacket_SERVICE_TRANSFER_USER_VALIDATION_REQ", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/ServiceTransfer/NKCServiceTransferMgr.cs", 106);
			NKMPacket_SERVICE_TRANSFER_USER_VALIDATION_REQ packet = new NKMPacket_SERVICE_TRANSFER_USER_VALIDATION_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06004A2F RID: 18991 RVA: 0x0016427C File Offset: 0x0016247C
		public static void OnRecv(NKMPacket_SERVICE_TRANSFER_USER_VALIDATION_ACK sPacket)
		{
			Log.Debug("[ServiceTransfer] OnRecv NKMPacket_SERVICE_TRANSFER_USER_VALIDATION_ACK", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/ServiceTransfer/NKCServiceTransferMgr.cs", 114);
			NKMPopUpBox.CloseWaitBox();
			if (sPacket.errorCode == NKM_ERROR_CODE.NEC_FAIL_SERVICE_TRANSFER_USER_ALREADY_TRANSFERRED)
			{
				NKCPopupOKCancel.OpenOKBox(NKCStringTable.GetString("SI_PF_SERVICE_TRANSFER_COMPLETE_NOTICE_TITLE", false), NKCStringTable.GetString("SI_PF_SERVICE_TRANSFER_SUCCESS", false), null, "");
				return;
			}
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCServiceTransferMgr.OpenServiceTransferInput();
		}

		// Token: 0x06004A30 RID: 18992 RVA: 0x001642E8 File Offset: 0x001624E8
		public static void Send_NKMPacket_SERVICE_TRANSFER_CODE_VALIDATION_REQ(string code)
		{
			Log.Debug("[ServiceTransfer] Send_NKMPacket_SERVICE_TRANSFER_CODE_VALIDATION_REQ[" + code + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/ServiceTransfer/NKCServiceTransferMgr.cs", 132);
			NKMPacket_SERVICE_TRANSFER_CODE_VALIDATION_REQ nkmpacket_SERVICE_TRANSFER_CODE_VALIDATION_REQ = new NKMPacket_SERVICE_TRANSFER_CODE_VALIDATION_REQ();
			nkmpacket_SERVICE_TRANSFER_CODE_VALIDATION_REQ.code = code;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_SERVICE_TRANSFER_CODE_VALIDATION_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06004A31 RID: 18993 RVA: 0x00164334 File Offset: 0x00162534
		public static void OnRecv(NKMPacket_SERVICE_TRANSFER_CODE_VALIDATION_ACK sPacket)
		{
			Log.Debug("[ServiceTransfer] OnRecv NKMPacket_SERVICE_TRANSFER_CODE_VALIDATION_ACK", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/ServiceTransfer/NKCServiceTransferMgr.cs", 141);
			NKMPopUpBox.CloseWaitBox();
			switch (sPacket.errorCode)
			{
			case NKM_ERROR_CODE.NEC_FAIL_SERVICE_TRANSFER_NOT_REGIST_CODE:
				NKCPopupOKCancel.OpenOKBox(NKCStringTable.GetString("SI_PF_SERVICE_TRANSFER_NOTICE_TITLE", false), NKCStringTable.GetString(sPacket.errorCode.ToString(), new object[]
				{
					sPacket.failCount
				}), null, "");
				return;
			case NKM_ERROR_CODE.NEC_FAIL_SERVICE_TRANSFER_REGIST_CODE_BLOCKED:
			case NKM_ERROR_CODE.NEC_FAIL_SERVICE_TRANSFER_USED_REGIST_CODE:
				NKCPopupOKCancel.OpenOKBox(NKCStringTable.GetString("SI_PF_SERVICE_TRANSFER_NOTICE_TITLE", false), NKCStringTable.GetString(sPacket.errorCode.ToString(), false), null, "");
				return;
			}
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCPopupServiceTransferInput.CheckInstanceAndClose();
			NKCServiceTransferMgr.OpenServiceTransferUserData(sPacket.userProfile);
		}

		// Token: 0x06004A32 RID: 18994 RVA: 0x00164414 File Offset: 0x00162614
		public static void Send_NKMPacket_SERVICE_TRANSFER_CONFIRM_REQ()
		{
			Log.Debug("[ServiceTransfer]Send_NKMPacket_SERVICE_TRANSFER_CONFIRM_REQ", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/ServiceTransfer/NKCServiceTransferMgr.cs", 173);
			NKMPacket_SERVICE_TRANSFER_CONFIRM_REQ packet = new NKMPacket_SERVICE_TRANSFER_CONFIRM_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06004A33 RID: 18995 RVA: 0x00164450 File Offset: 0x00162650
		public static void OnRecv(NKMPacket_SERVICE_TRANSFER_CONFIRM_ACK sPacket)
		{
			Log.Debug("[ServiceTransfer] OnRecv NKMPacket_SERVICE_TRANSFER_CONFIRM_ACK", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/ServiceTransfer/NKCServiceTransferMgr.cs", 181);
			NKCServiceTransferMgr.CloseAllServiceTransfer();
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				if (sPacket.errorCode == NKM_ERROR_CODE.NEC_FAIL_SERVICE_TRANSFER_USED_REGIST_CODE)
				{
					NKCServiceTransferMgr.OpenServiceTransferInput();
				}
				return;
			}
			NKCScenManager.GetScenManager().GetConnectLogin().ResetConnection();
			NKCScenManager.GetScenManager().GetConnectGame().ResetConnection();
			NKCPopupOKCancel.OpenOKBox(NKCStringTable.GetString("SI_PF_SERVICE_TRANSFER_COMPLETE_NOTICE_TITLE", false), NKCStringTable.GetString("SI_PF_SERVICE_TRANSFER_REGIST_SUCCESS", false), delegate()
			{
				NKCMain.QuitGame();
			}, "");
		}

		// Token: 0x06004A34 RID: 18996 RVA: 0x001644FA File Offset: 0x001626FA
		private static void CloseAllServiceTransferRegist()
		{
			NKCPopupServiceTransferTermsJpn.CheckInstanceAndClose();
			NKCPopupServiceTransferTerms.CheckInstanceAndClose();
			NKCPopupServiceTransferOutput.CheckInstanceAndClose();
		}

		// Token: 0x06004A35 RID: 18997 RVA: 0x0016450B File Offset: 0x0016270B
		private static void CloseAllServiceTransfer()
		{
			NKCPopupServiceTransferInput.CheckInstanceAndClose();
			NKCPopupServiceTransferUserData.CheckInstanceAndClose();
		}

		// Token: 0x06004A36 RID: 18998 RVA: 0x00164517 File Offset: 0x00162717
		private static void OpenServiceTransferTermsJpn()
		{
			NKCPopupServiceTransferTermsJpn.Instance.Open();
		}

		// Token: 0x06004A37 RID: 18999 RVA: 0x00164523 File Offset: 0x00162723
		public static void OpenServiceTransferTerms()
		{
			NKCPopupServiceTransferTerms.Instance.Open();
		}

		// Token: 0x06004A38 RID: 19000 RVA: 0x0016452F File Offset: 0x0016272F
		private static void OpenServiceTransferOutput(string code, bool bCanGetReward)
		{
			NKCPopupServiceTransferOutput.Instance.Open(code, bCanGetReward);
		}

		// Token: 0x06004A39 RID: 19001 RVA: 0x0016453D File Offset: 0x0016273D
		public static void OpenServiceTransferInput()
		{
			NKCPopupServiceTransferInput.Instance.Open();
		}

		// Token: 0x06004A3A RID: 19002 RVA: 0x00164549 File Offset: 0x00162749
		public static void OpenServiceTransferUserData(NKMAccountLinkUserProfile userProfile)
		{
			NKCPopupServiceTransferUserData.Instance.Open(userProfile);
		}
	}
}
