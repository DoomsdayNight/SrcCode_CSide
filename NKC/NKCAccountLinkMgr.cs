using System;
using ClientPacket.Account;
using ClientPacket.Common;
using Cs.Logging;
using NKC.PacketHandler;
using NKC.Publisher;
using NKC.UI;
using NKC.UI.Option;
using NKM;

namespace NKC
{
	// Token: 0x0200073D RID: 1853
	public static class NKCAccountLinkMgr
	{
		// Token: 0x06004A3B RID: 19003 RVA: 0x00164558 File Offset: 0x00162758
		public static void StartLinkProcess()
		{
			NKCPopupOKCancel.OpenOKCancelBox(NKCStringTable.GetString("SI_PF_STEAMLINK_NOTICE_TITLE", false), NKCStringTable.GetString("SI_PF_STEAMLINK_NOTICE_DESC", false) + "\n" + NKCStringTable.GetString("SI_PF_STEAMLINK_NOTICE_WARNING", false), new NKCPopupOKCancel.OnButton(NKCAccountLinkMgr.OpenAccountCodeInput), new NKCPopupOKCancel.OnButton(NKCAccountLinkMgr.CancelLinkProcess), false);
		}

		// Token: 0x06004A3C RID: 19004 RVA: 0x001645AE File Offset: 0x001627AE
		public static void CheckForCancelProcess()
		{
			NKCPopupOKCancel.OpenOKCancelBox(NKCStringTable.GetString("SI_PF_STEAMLINK_CODE_ENTER_TITLE", false), NKCStringTable.GetString("SI_PF_STEAMLINK_CANCEL_PROCESS", false), new NKCPopupOKCancel.OnButton(NKCAccountLinkMgr.Send_NKMPacket_ACCOUNT_LINK_CANCEL_REQ), null, false);
		}

		// Token: 0x06004A3D RID: 19005 RVA: 0x001645DC File Offset: 0x001627DC
		public static void CancelLinkProcess()
		{
			NKMPopUpBox.CloseWaitBox();
			NKCPopupOKCancel.ClosePopupBox();
			NKCPopupAccountCodeInput.Instance.Close();
			NKCPopupAccountCodeOutput.Instance.Close();
			NKCPopupAccountSelect.Instance.Close();
			NKCPopupAccountSelectConfirm.Instance.Close();
			NKCAccountLinkMgr.m_requestUserProfile = null;
			NKCAccountLinkMgr.m_fCodeInputRemainingTime = 0f;
			if (NKCUIGameOption.IsInstanceOpen)
			{
				NKCUIGameOption.Instance.UpdateOptionContent(NKCUIGameOption.GameOptionGroup.Account);
			}
		}

		// Token: 0x06004A3E RID: 19006 RVA: 0x0016463D File Offset: 0x0016283D
		public static void OpenAccountCodeInput()
		{
			NKCPopupOKCancel.ClosePopupBox();
			NKCPopupAccountCodeInput.Instance.Open(true);
		}

		// Token: 0x06004A3F RID: 19007 RVA: 0x0016464F File Offset: 0x0016284F
		public static void OpenPrivateLinkCodeInput()
		{
			if (NKCAccountLinkMgr.m_requestUserProfile != null)
			{
				NKCPopupAccountCodeInput.Instance.Open(false);
			}
		}

		// Token: 0x06004A40 RID: 19008 RVA: 0x00164664 File Offset: 0x00162864
		public static void Send_NKMPacket_BSIDE_ACCOUNT_LINK_REQ(string friendCodeString)
		{
			long num = Convert.ToInt64(friendCodeString);
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null && myUserData.m_FriendCode == num)
			{
				Log.Debug("[AccountLink] Send_NKMPacket_BSIDE_ACCOUNT_LINK_REQ - my friendCode", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Steam/NKCAccountLinkMgr.cs", 76);
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_STEAM_LINK_INVALID_PUBLISHER_CODE, null, "");
				return;
			}
			Log.Debug("[AccountLink] Send_NKMPacket_BSIDE_ACCOUNT_LINK_REQ[" + friendCodeString + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Steam/NKCAccountLinkMgr.cs", 81);
			NKMPacket_BSIDE_ACCOUNT_LINK_REQ nkmpacket_BSIDE_ACCOUNT_LINK_REQ = new NKMPacket_BSIDE_ACCOUNT_LINK_REQ();
			nkmpacket_BSIDE_ACCOUNT_LINK_REQ.friendCode = num;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_BSIDE_ACCOUNT_LINK_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
			if (NKCAccountLinkMgr._testMode)
			{
				NKCAccountLinkMgr.OnRecv(new NKMPacket_BSIDE_ACCOUNT_LINK_ACK
				{
					errorCode = NKM_ERROR_CODE.NEC_OK
				});
				NKMPacket_BSIDE_ACCOUNT_LINK_NOT nkmpacket_BSIDE_ACCOUNT_LINK_NOT = new NKMPacket_BSIDE_ACCOUNT_LINK_NOT();
				if (friendCodeString.Equals("111"))
				{
					nkmpacket_BSIDE_ACCOUNT_LINK_NOT.linkCode = "123456789";
				}
				else
				{
					nkmpacket_BSIDE_ACCOUNT_LINK_NOT.linkCode = "";
				}
				NKCAccountLinkMgr.OnRecv(nkmpacket_BSIDE_ACCOUNT_LINK_NOT);
			}
		}

		// Token: 0x06004A41 RID: 19009 RVA: 0x00164738 File Offset: 0x00162938
		public static void OnRecv(NKMPacket_BSIDE_ACCOUNT_LINK_ACK sPacket)
		{
			Log.Debug("[AccountLink] OnRecv NKMPacket_BSIDE_ACCOUNT_LINK_ACK", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Steam/NKCAccountLinkMgr.cs", 108);
			NKMPopUpBox.CloseWaitBox();
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCPopupAccountCodeInput.Instance.Close();
			NKMPopUpBox.OpenWaitBox(0f, "");
		}

		// Token: 0x06004A42 RID: 19010 RVA: 0x0016478C File Offset: 0x0016298C
		public static void OnRecv(NKMPacket_BSIDE_ACCOUNT_LINK_NOT sPacket)
		{
			Log.Debug("[AccountLink] OnRecv NKMPacket_BSIDE_ACCOUNT_LINK_NOT", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Steam/NKCAccountLinkMgr.cs", 120);
			NKMPopUpBox.CloseWaitBox();
			float remainingTime = sPacket.remainingTime;
			if (string.IsNullOrEmpty(sPacket.linkCode))
			{
				NKCAccountLinkMgr.m_requestUserProfile = sPacket.requestUserProfile;
				NKCAccountLinkMgr.m_fCodeInputRemainingTime = sPacket.remainingTime;
				if (NKCUIGameOption.IsInstanceOpen)
				{
					NKCUIGameOption.Instance.UpdateOptionContent(NKCUIGameOption.GameOptionGroup.Account);
					return;
				}
			}
			else
			{
				NKCPopupAccountCodeOutput.Instance.Open(sPacket.linkCode, remainingTime);
			}
		}

		// Token: 0x06004A43 RID: 19011 RVA: 0x00164800 File Offset: 0x00162A00
		public static void Send_NKMPacket_BSIDE_ACCOUNT_LINK_CODE_REQ(string privateLinkCode)
		{
			Log.Debug("[AccountLink] Send_NKMPacket_BSIDE_ACCOUNT_LINK_CODE_REQ[" + privateLinkCode + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Steam/NKCAccountLinkMgr.cs", 144);
			NKMPacket_BSIDE_ACCOUNT_LINK_CODE_REQ nkmpacket_BSIDE_ACCOUNT_LINK_CODE_REQ = new NKMPacket_BSIDE_ACCOUNT_LINK_CODE_REQ();
			nkmpacket_BSIDE_ACCOUNT_LINK_CODE_REQ.linkCode = privateLinkCode;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_BSIDE_ACCOUNT_LINK_CODE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
			if (NKCAccountLinkMgr._testMode)
			{
				NKMPacket_BSIDE_ACCOUNT_LINK_CODE_ACK nkmpacket_BSIDE_ACCOUNT_LINK_CODE_ACK = new NKMPacket_BSIDE_ACCOUNT_LINK_CODE_ACK();
				if (!privateLinkCode.Equals("123456789"))
				{
					nkmpacket_BSIDE_ACCOUNT_LINK_CODE_ACK.errorCode = NKM_ERROR_CODE.NEC_FAIL_STEAM_LINK_INVALID_PRIVATE_CODE;
				}
				else
				{
					nkmpacket_BSIDE_ACCOUNT_LINK_CODE_ACK.errorCode = NKM_ERROR_CODE.NEC_OK;
				}
				NKCAccountLinkMgr.OnRecv(nkmpacket_BSIDE_ACCOUNT_LINK_CODE_ACK);
			}
		}

		// Token: 0x06004A44 RID: 19012 RVA: 0x00164880 File Offset: 0x00162A80
		public static void OnRecv(NKMPacket_BSIDE_ACCOUNT_LINK_CODE_ACK sPacket)
		{
			Log.Debug("[AccountLink] OnRecv NKMPacket_BSIDE_ACCOUNT_LINK_CODE_ACK", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Steam/NKCAccountLinkMgr.cs", 167);
			NKMPopUpBox.CloseWaitBox();
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCPopupAccountCodeInput.Instance.Close();
			NKMPopUpBox.OpenWaitBox(0f, "");
			if (NKCAccountLinkMgr._testMode)
			{
				NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
				NKCAccountLinkMgr.OnRecv(new NKMPacket_BSIDE_ACCOUNT_LINK_CODE_NOT
				{
					requestUserProfile = new NKMAccountLinkUserProfile(),
					requestUserProfile = 
					{
						commonProfile = new NKMCommonProfile(),
						commonProfile = 
						{
							nickname = "모바일순돌",
							level = 50,
							userUid = myUserData.m_UserUID
						},
						creditCount = 100L,
						eterniumCount = 9999L,
						cashCount = 400L,
						medalCount = 30L,
						publisherType = NKM_PUBLISHER_TYPE.NPT_STUDIO_BSIDE
					},
					targetUserProfile = new NKMAccountLinkUserProfile(),
					targetUserProfile = 
					{
						commonProfile = new NKMCommonProfile(),
						commonProfile = 
						{
							nickname = "스팀순돌",
							level = 3,
							userUid = myUserData.m_UserUID
						},
						creditCount = 5L,
						eterniumCount = 10L,
						cashCount = 55L,
						medalCount = 3L,
						publisherType = NKM_PUBLISHER_TYPE.NPT_STEAM
					}
				});
			}
		}

		// Token: 0x06004A45 RID: 19013 RVA: 0x00164A30 File Offset: 0x00162C30
		public static void OnRecv(NKMPacket_BSIDE_ACCOUNT_LINK_CODE_NOT sPacket)
		{
			Log.Debug("[AccountLink] OnRecv NKMPacket_BSIDE_ACCOUNT_LINK_CODE_NOT", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Steam/NKCAccountLinkMgr.cs", 214);
			NKMPopUpBox.CloseWaitBox();
			NKCPopupAccountCodeOutput.Instance.Close();
			NKCPopupAccountCodeInput.Instance.Close();
			NKCPopupAccountSelect.Instance.Open(sPacket.requestUserProfile, sPacket.targetUserProfile);
		}

		// Token: 0x06004A46 RID: 19014 RVA: 0x00164A80 File Offset: 0x00162C80
		public static void Send_NKMPacket_ACCOUNT_LINK_SELECT_USERDATA_REQ(long userUID)
		{
			Log.Debug(string.Format("[AccountLink] Send_NKMPacket_ACCOUNT_LINK_SELECT_USERDATA_REQ[{0}]", userUID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Steam/NKCAccountLinkMgr.cs", 226);
			NKMPacket_BSIDE_ACCOUNT_LINK_SELECT_USERDATA_REQ nkmpacket_BSIDE_ACCOUNT_LINK_SELECT_USERDATA_REQ = new NKMPacket_BSIDE_ACCOUNT_LINK_SELECT_USERDATA_REQ();
			nkmpacket_BSIDE_ACCOUNT_LINK_SELECT_USERDATA_REQ.selectedUserUid = userUID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_BSIDE_ACCOUNT_LINK_SELECT_USERDATA_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
			if (NKCAccountLinkMgr._testMode)
			{
				NKCAccountLinkMgr.OnRecv(new NKMPacket_BSIDE_ACCOUNT_LINK_SELECT_USERDATA_ACK
				{
					errorCode = NKM_ERROR_CODE.NEC_OK
				});
			}
		}

		// Token: 0x06004A47 RID: 19015 RVA: 0x00164AE4 File Offset: 0x00162CE4
		public static void OnRecv(NKMPacket_BSIDE_ACCOUNT_LINK_SELECT_USERDATA_ACK sPacket)
		{
			Log.Debug("[AccountLink] OnRecv NKMPacket_ACCOUNT_LINK_SELECT_USERDATA_ACK", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Steam/NKCAccountLinkMgr.cs", 241);
			NKMPopUpBox.CloseWaitBox();
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCPopupAccountSelect.Instance.Close();
			NKCPopupAccountSelectConfirm.Instance.Close();
			if (NKCAccountLinkMgr._testMode)
			{
				NKCPopupOKCancel.OpenOKBox("테스트 성공", "로그아웃을 기다리세요", new NKCPopupOKCancel.OnButton(NKCAccountLinkMgr.CancelLinkProcess), "");
			}
		}

		// Token: 0x06004A48 RID: 19016 RVA: 0x00164B5C File Offset: 0x00162D5C
		public static void OnRecv(NKMPacket_BSIDE_ACCOUNT_LINK_SUCCESS_NOT sPacket)
		{
			Log.Debug("[AccountLink] OnRecv NKMPacket_BSIDE_ACCOUNT_LINK_SUCCESS_NOT", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Steam/NKCAccountLinkMgr.cs", 260);
			NKMPopUpBox.CloseWaitBox();
			NKCPopupAccountSelectConfirm.Instance.Close();
			NKCScenManager.GetScenManager().GetConnectLogin().ResetConnection();
			NKCScenManager.GetScenManager().GetConnectGame().ResetConnection();
			NKCPopupOKCancel.OpenOKBox(NKCStringTable.GetString("SI_PF_STEAMLINK_NOTICE_TITLE", false), NKCStringTable.GetString("SI_PF_STEAMLINK_NOTICE_SUCCESS", false), delegate()
			{
				NKCMain.QuitGame();
			}, "");
		}

		// Token: 0x06004A49 RID: 19017 RVA: 0x00164BEC File Offset: 0x00162DEC
		public static void Send_NKMPacket_ACCOUNT_LINK_CANCEL_REQ()
		{
			Log.Debug("[AccountLink] Send_NKMPacket_ACCOUNT_LINK_CANCEL_REQ", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Steam/NKCAccountLinkMgr.cs", 272);
			NKMPacket_BSIDE_ACCOUNT_LINK_CANCEL_REQ packet = new NKMPacket_BSIDE_ACCOUNT_LINK_CANCEL_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
			if (NKCAccountLinkMgr._testMode)
			{
				NKCAccountLinkMgr.OnRecv(new NKMPacket_BSIDE_ACCOUNT_LINK_CANCEL_NOT());
			}
		}

		// Token: 0x06004A4A RID: 19018 RVA: 0x00164C38 File Offset: 0x00162E38
		public static void OnRecv(NKMPacket_BSIDE_ACCOUNT_LINK_CANCEL_ACK sPacket)
		{
			Log.Debug("[AccountLink] NKMPacket_BSIDE_ACCOUNT_LINK_CANCEL_ACK", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Steam/NKCAccountLinkMgr.cs", 284);
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				if (sPacket.errorCode == NKM_ERROR_CODE.NEC_FAIL_STEAM_LINK_NOT_EXIST_LINK_INFO)
				{
					NKCAccountLinkMgr.CancelLinkProcess();
				}
				return;
			}
			NKCAccountLinkMgr.CancelLinkProcess();
		}

		// Token: 0x06004A4B RID: 19019 RVA: 0x00164C85 File Offset: 0x00162E85
		public static void OnRecv(NKMPacket_BSIDE_ACCOUNT_LINK_CANCEL_NOT sPacket)
		{
			NKCAccountLinkMgr.CancelLinkProcess();
		}

		// Token: 0x06004A4C RID: 19020 RVA: 0x00164C8C File Offset: 0x00162E8C
		public static void OnClickSuccessConfirm()
		{
		}

		// Token: 0x06004A4D RID: 19021 RVA: 0x00164C8E File Offset: 0x00162E8E
		private static void OnLogoutComplete(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
		{
			if (!NKCPublisherModule.CheckError(resultCode, additionalError, true, null, false))
			{
				return;
			}
			Log.Debug("[AccountLink] OnLogoutComplete", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Steam/NKCAccountLinkMgr.cs", 314);
			NKCPacketHandlersLobby.MoveToLogin();
		}

		// Token: 0x040038EC RID: 14572
		private static bool _testMode;

		// Token: 0x040038ED RID: 14573
		public static NKMAccountLinkUserProfile m_requestUserProfile;

		// Token: 0x040038EE RID: 14574
		public static float m_fCodeInputRemainingTime;
	}
}
