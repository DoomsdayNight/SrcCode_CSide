using System;
using ClientPacket.Guild;
using Cs.Core.Util;
using NKC.UI.Office;
using NKM;
using NKM.Guild;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B5B RID: 2907
	public class NKCUIGuildMemberSlotNormal : MonoBehaviour
	{
		// Token: 0x06008491 RID: 33937 RVA: 0x002CB66C File Offset: 0x002C986C
		public void InitUI()
		{
			this.m_lUserUId = 0L;
			this.m_bHasOffice = false;
			NKCUtil.SetButtonClickDelegate(this.m_csbtnDormitory, new UnityAction(this.OnClickDormitory));
		}

		// Token: 0x06008492 RID: 33938 RVA: 0x002CB694 File Offset: 0x002C9894
		public void SetData(NKMGuildMemberData guildMemberData, bool bIsMyGuild)
		{
			this.m_slot.Init();
			this.m_slot.SetProfiledata(guildMemberData.commonProfile, null);
			switch (guildMemberData.grade)
			{
			case GuildMemberGrade.Master:
				NKCUtil.SetGameobjectActive(this.m_imgLeader, true);
				NKCUtil.SetImageSprite(this.m_imgLeader, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_consortium_sprite", "AB_UI_NKM_UI_CONSORTIUM_ICON_LEADER", false), false);
				break;
			case GuildMemberGrade.Staff:
				NKCUtil.SetGameobjectActive(this.m_imgLeader, true);
				NKCUtil.SetImageSprite(this.m_imgLeader, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_consortium_sprite", "AB_UI_NKM_UI_CONSORTIUM_ICON_OFFICER", false), false);
				break;
			case GuildMemberGrade.Member:
				NKCUtil.SetGameobjectActive(this.m_imgLeader, false);
				break;
			}
			NKCUtil.SetLabelText(this.m_lbLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, guildMemberData.commonProfile.level));
			NKCUtil.SetLabelText(this.m_lbName, guildMemberData.commonProfile.nickname);
			NKCUtil.SetGameobjectActive(this.m_objPointParent, true);
			NKCUtil.SetLabelText(this.m_lbWeeklyPoint, guildMemberData.weeklyContributionPoint.ToString());
			NKCUtil.SetLabelText(this.m_lbTotalPoint, guildMemberData.totalContributionPoint.ToString());
			NKCUtil.SetLabelText(this.m_lbComment, guildMemberData.greeting);
			TimeSpan timeSpan = NKCSynchronizedTime.GetServerUTCTime(0.0) - guildMemberData.lastOnlineTime;
			NKCUtil.SetLabelText(this.m_lbState, NKCUtilString.GetLastTimeString(guildMemberData.lastOnlineTime));
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
			NKCUtil.SetGameobjectActive(this.m_objMyGuildOnly, bIsMyGuild);
			if (bIsMyGuild)
			{
				NKCUtil.SetGameobjectActive(this.m_objAttendanceDone, guildMemberData.HasAttendanceData(ServiceTime.Recent));
				NKCUtil.SetGameobjectActive(this.m_objMissionDone, false);
			}
			NKCUtil.SetGameobjectActive(this.m_objMySlot, guildMemberData.commonProfile.userUid == NKCScenManager.CurrentUserData().m_UserUID);
			NKCUtil.SetGameobjectActive(this.m_objRedDot, false);
			this.m_lUserUId = guildMemberData.commonProfile.userUid;
			this.m_bHasOffice = guildMemberData.hasOffice;
		}

		// Token: 0x06008493 RID: 33939 RVA: 0x002CB8D0 File Offset: 0x002C9AD0
		private void OnClickDormitory()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null && nkmuserData.m_UserUID == this.m_lUserUId)
			{
				NKCUIOfficeMapFront.ReserveScenID = NKCScenManager.GetScenManager().GetNowScenID();
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_LOBBY().SetReserveLobbyTab(NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Member);
				NKCScenManager scenManager = NKCScenManager.GetScenManager();
				if (scenManager == null)
				{
					return;
				}
				scenManager.ScenChangeFade(NKM_SCEN_ID.NSI_OFFICE, false);
				return;
			}
			else
			{
				if (this.m_bHasOffice)
				{
					NKCPacketSender.Send_NKMPacket_OFFICE_STATE_REQ(this.m_lUserUId);
					return;
				}
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_OFFICE_FRIEND_CANNOT_VISIT, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return;
			}
		}

		// Token: 0x040070C1 RID: 28865
		public NKCUISlotProfile m_slot;

		// Token: 0x040070C2 RID: 28866
		public Image m_imgLeader;

		// Token: 0x040070C3 RID: 28867
		public Text m_lbLevel;

		// Token: 0x040070C4 RID: 28868
		public Text m_lbName;

		// Token: 0x040070C5 RID: 28869
		public GameObject m_objPointParent;

		// Token: 0x040070C6 RID: 28870
		public Text m_lbWeeklyPoint;

		// Token: 0x040070C7 RID: 28871
		public Text m_lbTotalPoint;

		// Token: 0x040070C8 RID: 28872
		public Text m_lbComment;

		// Token: 0x040070C9 RID: 28873
		public Image m_imgState;

		// Token: 0x040070CA RID: 28874
		public Text m_lbState;

		// Token: 0x040070CB RID: 28875
		public GameObject m_objMyGuildOnly;

		// Token: 0x040070CC RID: 28876
		public GameObject m_objAttendanceDone;

		// Token: 0x040070CD RID: 28877
		public GameObject m_objMissionDone;

		// Token: 0x040070CE RID: 28878
		public GameObject m_objMySlot;

		// Token: 0x040070CF RID: 28879
		public GameObject m_objRedDot;

		// Token: 0x040070D0 RID: 28880
		public NKCUIComStateButton m_csbtnDormitory;

		// Token: 0x040070D1 RID: 28881
		private long m_lUserUId;

		// Token: 0x040070D2 RID: 28882
		private bool m_bHasOffice;
	}
}
