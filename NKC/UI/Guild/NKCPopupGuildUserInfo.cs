using System;
using ClientPacket.Common;
using ClientPacket.Guild;
using Cs.Core.Util;
using NKC.UI.Friend;
using NKM;
using NKM.Guild;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B48 RID: 2888
	public class NKCPopupGuildUserInfo : MonoBehaviour
	{
		// Token: 0x06008383 RID: 33667 RVA: 0x002C50B0 File Offset: 0x002C32B0
		public void InitUI()
		{
			this.m_btnChangeGrade.PointerClick.RemoveAllListeners();
			this.m_btnChangeGrade.PointerClick.AddListener(new UnityAction(this.OnClickChangeGrade));
			this.m_btnGiveMaster.PointerClick.RemoveAllListeners();
			this.m_btnGiveMaster.PointerClick.AddListener(new UnityAction(this.OnClickGiveMaster));
			this.m_btnExit.PointerClick.RemoveAllListeners();
			this.m_btnExit.PointerClick.AddListener(new UnityAction(this.OnClickExit));
			this.m_btnBan.PointerClick.RemoveAllListeners();
			this.m_btnBan.PointerClick.AddListener(new UnityAction(this.OnClickBan));
			this.m_btnChat.PointerClick.RemoveAllListeners();
			this.m_btnChat.PointerClick.AddListener(new UnityAction(this.OnClickChat));
			this.m_btnChat.m_bGetCallbackWhileLocked = true;
		}

		// Token: 0x06008384 RID: 33668 RVA: 0x002C51A8 File Offset: 0x002C33A8
		public void SetData(NKMUserProfileData cNKMUserProfileData)
		{
			NKMGuildData myGuildData = NKCGuildManager.MyGuildData;
			NKMGuildMemberData nkmguildMemberData;
			if (myGuildData == null)
			{
				nkmguildMemberData = null;
			}
			else
			{
				nkmguildMemberData = myGuildData.members.Find((NKMGuildMemberData x) => x.commonProfile.userUid == NKCScenManager.CurrentUserData().m_UserUID);
			}
			NKMGuildMemberData myData = nkmguildMemberData;
			NKMGuildData myGuildData2 = NKCGuildManager.MyGuildData;
			NKMGuildMemberData userData = (myGuildData2 != null) ? myGuildData2.members.Find((NKMGuildMemberData x) => x.commonProfile.userUid == cNKMUserProfileData.commonProfile.userUid) : null;
			this.m_TargetUserName = cNKMUserProfileData.commonProfile.nickname;
			this.m_TargetUserUid = cNKMUserProfileData.commonProfile.userUid;
			this.SetButtons(myData, userData);
		}

		// Token: 0x06008385 RID: 33669 RVA: 0x002C5250 File Offset: 0x002C3450
		private void SetButtons(NKMGuildMemberData myData, NKMGuildMemberData userData)
		{
			if (myData != null && userData != null)
			{
				NKCUtil.SetGameobjectActive(this.m_objState, true);
				NKCUtil.SetGameobjectActive(this.m_objButtons, true);
				NKCUtil.SetGameobjectActive(this.m_objPoint, true);
				NKCUtil.SetGameobjectActive(this.m_objChecklist, true);
				TimeSpan timeSpan = NKCSynchronizedTime.GetServerUTCTime(0.0) - userData.lastOnlineTime;
				NKCUtil.SetLabelText(this.m_lbState, NKCUtilString.GetLastTimeString(userData.lastOnlineTime));
				if (timeSpan.TotalDays > 3.0)
				{
					NKCUtil.SetImageColor(this.m_imgState, NKCUtil.GetColor("#de0a0a"));
				}
				else if (timeSpan.TotalDays > 1.0)
				{
					NKCUtil.SetImageColor(this.m_imgState, NKCUtil.GetColor("#db650e"));
				}
				else
				{
					NKCUtil.SetImageColor(this.m_imgState, NKCUtil.GetColor("#0aca0a"));
				}
				NKCUtil.SetGameobjectActive(this.m_btnChangeGrade, myData.grade == GuildMemberGrade.Master && myData.commonProfile.userUid != userData.commonProfile.userUid);
				NKCUtil.SetGameobjectActive(this.m_btnGiveMaster, myData.grade == GuildMemberGrade.Master && userData.grade == GuildMemberGrade.Staff);
				NKCUtil.SetGameobjectActive(this.m_btnBan, myData.grade != GuildMemberGrade.Member && myData.grade < userData.grade);
				NKCUtil.SetGameobjectActive(this.m_btnExit, userData.commonProfile.userUid == NKCScenManager.CurrentUserData().m_UserUID && myData.grade > GuildMemberGrade.Master);
				NKCUtil.SetGameobjectActive(this.m_btnChat, userData.commonProfile.userUid != NKCScenManager.CurrentUserData().m_UserUID && NKCGuildManager.IsGuildMemberByUID(userData.commonProfile.userUid));
				if (NKCFriendManager.IsBlockedUser(userData.commonProfile.friendCode))
				{
					this.m_btnChat.Lock(false);
				}
				else
				{
					this.m_btnChat.UnLock(false);
				}
				if (this.m_btnChangeGrade.gameObject.activeSelf)
				{
					if (userData.grade == GuildMemberGrade.Staff)
					{
						this.m_bGradeUp = false;
						NKCUtil.SetLabelText(this.m_lbChangeGrade, NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_GRADE_DOWN);
					}
					else if (userData.grade == GuildMemberGrade.Member)
					{
						this.m_bGradeUp = true;
						NKCUtil.SetLabelText(this.m_lbChangeGrade, NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_GRADE_UP);
					}
				}
				NKCUtil.SetGameobjectActive(this.m_objCheckAttendance, userData.HasAttendanceData(ServiceTime.Recent));
				NKCUtil.SetGameobjectActive(this.m_objCheckMission, false);
				NKCUtil.SetLabelText(this.m_lbWeeklyPoint, userData.weeklyContributionPoint.ToString());
				NKCUtil.SetLabelText(this.m_lbTotalPoint, userData.totalContributionPoint.ToString());
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objState, false);
			NKCUtil.SetGameobjectActive(this.m_objButtons, false);
			NKCUtil.SetGameobjectActive(this.m_objPoint, false);
			NKCUtil.SetGameobjectActive(this.m_objChecklist, false);
			NKCUtil.SetGameobjectActive(this.m_btnChat, false);
		}

		// Token: 0x06008386 RID: 33670 RVA: 0x002C5514 File Offset: 0x002C3714
		private void OnClickChangeGrade()
		{
			GuildMemberGrade targetGrade = GuildMemberGrade.Member;
			string title;
			string format;
			if (this.m_bGradeUp)
			{
				title = NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_GRADE_UP;
				format = NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_GRADE_UP_CONFIRM_POPUP_BODY_DESC;
				targetGrade = GuildMemberGrade.Staff;
			}
			else
			{
				title = NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_GRADE_DOWN;
				format = NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_GRADE_DOWN_CONFIRM_POPUP_BODY_DESC;
				targetGrade = GuildMemberGrade.Member;
			}
			NKCPopupOKCancel.OpenOKCancelBox(title, string.Format(format, this.m_TargetUserName), delegate()
			{
				this.OnConfirmChangeGrade(targetGrade);
			}, null, false);
		}

		// Token: 0x06008387 RID: 33671 RVA: 0x002C5585 File Offset: 0x002C3785
		private void OnConfirmChangeGrade(GuildMemberGrade targetGrade)
		{
			NKCPopupFriendInfo.Instance.Close();
			NKCPacketSender.Send_NKMPacket_GUILD_SET_MEMBER_GRADE_REQ(NKCGuildManager.MyGuildData.guildUid, this.m_TargetUserUid, targetGrade);
		}

		// Token: 0x06008388 RID: 33672 RVA: 0x002C55A7 File Offset: 0x002C37A7
		private void OnClickGiveMaster()
		{
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_GRADE_HANDOVER_CONFIRM_POPUP_TITLE_DESC, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_GRADE_HANDOVER_CONFIRM_POPUP_BODY_DESC, this.m_TargetUserName), new NKCPopupOKCancel.OnButton(this.OnGiveMaster), null, false);
		}

		// Token: 0x06008389 RID: 33673 RVA: 0x002C55D1 File Offset: 0x002C37D1
		private void OnGiveMaster()
		{
			NKCPopupFriendInfo.Instance.Close();
			NKCPacketSender.Send_NKMPacket_GUILD_MASTER_SPECIFIED_MIGRATION_REQ(NKCGuildManager.MyData.guildUid, this.m_TargetUserUid);
		}

		// Token: 0x0600838A RID: 33674 RVA: 0x002C55F2 File Offset: 0x002C37F2
		private void OnClickBan()
		{
			NKCPopupGuildKick.Instance.Open(this.m_TargetUserName, new NKCPopupGuildKick.OnClose(this.OnBan));
		}

		// Token: 0x0600838B RID: 33675 RVA: 0x002C5610 File Offset: 0x002C3810
		private void OnBan(int banReason)
		{
			NKCPopupFriendInfo.Instance.Close();
			NKCPacketSender.Send_NKMPacket_GUILD_BAN_REQ(NKCGuildManager.MyGuildData.guildUid, this.m_TargetUserUid, banReason);
		}

		// Token: 0x0600838C RID: 33676 RVA: 0x002C5632 File Offset: 0x002C3832
		private void OnClickExit()
		{
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_EXIT_CONFIRM_POPUP_TITLE_DESC, NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_EXIT_CONFIRM_POPUP_BODY_DESC, delegate()
			{
				NKCPacketSender.Send_NKMPacket_GUILD_EXIT_REQ(NKCGuildManager.MyGuildData.guildUid);
			}, null, false);
		}

		// Token: 0x0600838D RID: 33677 RVA: 0x002C5664 File Offset: 0x002C3864
		private void OnClickChat()
		{
			if (NKMOpenTagManager.IsOpened("CHAT_PRIVATE"))
			{
				if (this.m_btnChat.m_bLock)
				{
					NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_CHAT_BLOCKED, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
					return;
				}
				if (NKCGuildManager.IsGuildMemberByUID(this.m_TargetUserUid))
				{
					bool flag;
					NKCContentManager.eContentStatus eContentStatus = NKCContentManager.CheckContentStatus(ContentsType.FRIENDS, out flag, 0, 0);
					if (eContentStatus == NKCContentManager.eContentStatus.Open)
					{
						if (NKCScenManager.GetScenManager().GetGameOptionData().UseChatContent)
						{
							NKCPopupFriendInfo.Instance.Close();
							NKCPacketSender.Send_NKMPacket_PRIVATE_CHAT_LIST_REQ(this.m_TargetUserUid);
							return;
						}
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_OPTION_GAME_CHAT_NOTICE, null, "");
						return;
					}
					else if (eContentStatus == NKCContentManager.eContentStatus.Lock)
					{
						NKCContentManager.ShowLockedMessagePopup(ContentsType.FRIENDS, 0);
						return;
					}
				}
			}
			else
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_COMING_SOON_SYSTEM, null, "");
			}
		}

		// Token: 0x04006FA9 RID: 28585
		[Header("길드 관련")]
		public GameObject m_objState;

		// Token: 0x04006FAA RID: 28586
		public Image m_imgState;

		// Token: 0x04006FAB RID: 28587
		public Text m_lbState;

		// Token: 0x04006FAC RID: 28588
		public GameObject m_objPoint;

		// Token: 0x04006FAD RID: 28589
		public Text m_lbWeeklyPoint;

		// Token: 0x04006FAE RID: 28590
		public Text m_lbTotalPoint;

		// Token: 0x04006FAF RID: 28591
		public GameObject m_objButtons;

		// Token: 0x04006FB0 RID: 28592
		public NKCUIComStateButton m_btnChangeGrade;

		// Token: 0x04006FB1 RID: 28593
		public Text m_lbChangeGrade;

		// Token: 0x04006FB2 RID: 28594
		public NKCUIComStateButton m_btnGiveMaster;

		// Token: 0x04006FB3 RID: 28595
		public NKCUIComStateButton m_btnExit;

		// Token: 0x04006FB4 RID: 28596
		public NKCUIComStateButton m_btnBan;

		// Token: 0x04006FB5 RID: 28597
		public NKCUIComStateButton m_btnChat;

		// Token: 0x04006FB6 RID: 28598
		public GameObject m_objChecklist;

		// Token: 0x04006FB7 RID: 28599
		public GameObject m_objCheckAttendance;

		// Token: 0x04006FB8 RID: 28600
		public GameObject m_objCheckMission;

		// Token: 0x04006FB9 RID: 28601
		private bool m_bGradeUp = true;

		// Token: 0x04006FBA RID: 28602
		private string m_TargetUserName = "";

		// Token: 0x04006FBB RID: 28603
		private long m_TargetUserUid;
	}
}
