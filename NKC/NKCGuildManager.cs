using System;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.Common;
using ClientPacket.Guild;
using Cs.Core.Util;
using Cs.Shared.Time;
using NKC.UI;
using NKC.UI.Guild;
using NKM;
using NKM.Guild;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKC
{
	// Token: 0x0200068B RID: 1675
	public class NKCGuildManager
	{
		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06003699 RID: 13977 RVA: 0x00119A1E File Offset: 0x00117C1E
		// (set) Token: 0x0600369A RID: 13978 RVA: 0x00119A25 File Offset: 0x00117C25
		public static PrivateGuildData MyData { get; private set; }

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x0600369B RID: 13979 RVA: 0x00119A2D File Offset: 0x00117C2D
		// (set) Token: 0x0600369C RID: 13980 RVA: 0x00119A34 File Offset: 0x00117C34
		public static DateTime LastNoticeChangedTimeUTC { get; private set; }

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x0600369D RID: 13981 RVA: 0x00119A3C File Offset: 0x00117C3C
		// (set) Token: 0x0600369E RID: 13982 RVA: 0x00119A43 File Offset: 0x00117C43
		public static NKMGuildData MyGuildData { get; private set; }

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x0600369F RID: 13983 RVA: 0x00119A4C File Offset: 0x00117C4C
		public static List<NKMGuildMemberData> MyGuildMemberDataList
		{
			get
			{
				if (NKCGuildManager.MyGuildData == null)
				{
					return new List<NKMGuildMemberData>();
				}
				return (from e in NKCGuildManager.MyGuildData.members
				where e.commonProfile.userUid != NKCScenManager.CurrentUserData().m_UserUID
				select e).ToList<NKMGuildMemberData>();
			}
		}

		// Token: 0x060036A0 RID: 13984 RVA: 0x00119A9C File Offset: 0x00117C9C
		public static void Initialize()
		{
			NKCGuildManager.m_lstFrameTemplet = NKMTempletContainer<NKMGuildBadgeFrameTemplet>.Values.ToList<NKMGuildBadgeFrameTemplet>();
			NKCGuildManager.m_lstMarkTemplet = NKMTempletContainer<NKMGuildBadgeMarkTemplet>.Values.ToList<NKMGuildBadgeMarkTemplet>();
			NKCGuildManager.m_lstBadgeColorTemplet = NKMTempletContainer<NKMGuildBadgeColorTemplet>.Values.ToList<NKMGuildBadgeColorTemplet>();
			NKCGuildManager.m_lstSearchData = new List<GuildListData>();
			NKCGuildManager.m_lstRequestedData = new List<GuildListData>();
			NKCGuildManager.m_lstInvitedData = new List<GuildListData>();
			NKCGuildManager.MyData = new PrivateGuildData();
			NKCGuildManager.MyGuildData = null;
		}

		// Token: 0x060036A1 RID: 13985 RVA: 0x00119B04 File Offset: 0x00117D04
		public static void SetMyData(PrivateGuildData data)
		{
			NKCGuildManager.MyData = data;
		}

		// Token: 0x060036A2 RID: 13986 RVA: 0x00119B0C File Offset: 0x00117D0C
		public static void SetGuildJoinDisableTime(DateTime serviceTime)
		{
			NKCGuildManager.MyData.guildJoinDisableTime = serviceTime;
		}

		// Token: 0x060036A3 RID: 13987 RVA: 0x00119B1C File Offset: 0x00117D1C
		public static void SetMyGuildData(NKMGuildData data)
		{
			NKCGuildManager.MyGuildData = data;
			if (data != null && data.guildUid > 0L && NKCGuildManager.MyData.guildUid == 0L)
			{
				NKCGuildManager.SetMyDataFromMyGuildData(data);
			}
			if (NKCGuildManager.MyData.guildUid == 0L && NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GUILD_LOBBY)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_CONFORTIUM_FAIL_GUILD_NOT_BELONG_AT_PRESENT_POPUP_TEXT, delegate()
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GUILD_INTRO, true);
				}, "");
			}
			NKCUIManager.OnGuildDataChanged();
		}

		// Token: 0x060036A4 RID: 13988 RVA: 0x00119BA0 File Offset: 0x00117DA0
		private static void SetMyDataFromMyGuildData(NKMGuildData data)
		{
			NKCGuildManager.MyData.guildUid = data.guildUid;
		}

		// Token: 0x060036A5 RID: 13989 RVA: 0x00119BB2 File Offset: 0x00117DB2
		public static bool HasGuild()
		{
			return NKCContentManager.IsContentsUnlocked(ContentsType.GUILD, 0, 0) && NKCGuildManager.MyData.guildUid > 0L && NKCGuildManager.MyGuildData != null;
		}

		// Token: 0x060036A6 RID: 13990 RVA: 0x00119BD8 File Offset: 0x00117DD8
		public static bool IsGuildMember(long friendCode)
		{
			return NKCGuildManager.HasGuild() && NKCGuildManager.MyGuildData.members.Find((NKMGuildMemberData x) => x.commonProfile.friendCode == friendCode) != null;
		}

		// Token: 0x060036A7 RID: 13991 RVA: 0x00119C1C File Offset: 0x00117E1C
		public static bool IsGuildMemberByUID(long userUid)
		{
			return NKCGuildManager.HasGuild() && NKCGuildManager.MyGuildData.members.Find((NKMGuildMemberData x) => x.commonProfile.userUid == userUid) != null;
		}

		// Token: 0x060036A8 RID: 13992 RVA: 0x00119C60 File Offset: 0x00117E60
		public static bool IsFirstDay()
		{
			NKMGuildMemberData nkmguildMemberData = NKCGuildManager.MyGuildData.members.Find((NKMGuildMemberData x) => x.commonProfile.userUid == NKCScenManager.CurrentUserData().m_UserUID);
			return !DailyReset.IsOutOfDate(ServiceTime.Recent, nkmguildMemberData.createdAt);
		}

		// Token: 0x060036A9 RID: 13993 RVA: 0x00119CB0 File Offset: 0x00117EB0
		public static void RemoveSearchData(long guildUid)
		{
			GuildListData guildListData = NKCGuildManager.m_lstSearchData.Find((GuildListData x) => x.guildUid == guildUid);
			if (guildListData != null)
			{
				NKCGuildManager.m_lstSearchData.Remove(guildListData);
			}
		}

		// Token: 0x060036AA RID: 13994 RVA: 0x00119CF0 File Offset: 0x00117EF0
		public static void RemoveRequestedData(long guildUid)
		{
			GuildListData guildListData = NKCGuildManager.m_lstRequestedData.Find((GuildListData x) => x.guildUid == guildUid);
			if (guildListData != null)
			{
				NKCGuildManager.m_lstRequestedData.Remove(guildListData);
			}
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x00119D30 File Offset: 0x00117F30
		public static void RemoveInvitedData(long guildUid)
		{
			GuildListData guildListData = NKCGuildManager.m_lstInvitedData.Find((GuildListData x) => x.guildUid == guildUid);
			if (guildListData != null)
			{
				NKCGuildManager.m_lstInvitedData.Remove(guildListData);
			}
		}

		// Token: 0x060036AC RID: 13996 RVA: 0x00119D70 File Offset: 0x00117F70
		public static void RemoveLastJoinRequestedGuildData()
		{
			NKCGuildManager.RemoveSearchData(NKCGuildManager.m_lastJoinRequestedGuildUid);
			NKCGuildManager.m_lastJoinRequestedGuildUid = 0L;
		}

		// Token: 0x060036AD RID: 13997 RVA: 0x00119D84 File Offset: 0x00117F84
		public static bool AlreadyRequested(long guildUid)
		{
			return NKCGuildManager.m_lstRequestedData.Find((GuildListData x) => x.guildUid == guildUid) != null;
		}

		// Token: 0x060036AE RID: 13998 RVA: 0x00119DBC File Offset: 0x00117FBC
		public static bool AlreadyInvited(long guildUid)
		{
			return NKCGuildManager.m_lstInvitedData.Find((GuildListData x) => x.guildUid == guildUid) != null;
		}

		// Token: 0x060036AF RID: 13999 RVA: 0x00119DF1 File Offset: 0x00117FF1
		public static NKMGuildBadgeFrameTemplet GetFrameTempletByIndex(int idx)
		{
			if (NKCGuildManager.m_lstFrameTemplet.Count > idx)
			{
				return NKCGuildManager.m_lstFrameTemplet[idx];
			}
			return null;
		}

		// Token: 0x060036B0 RID: 14000 RVA: 0x00119E0D File Offset: 0x0011800D
		public static NKMGuildBadgeMarkTemplet GetMarkTempletByIndex(int idx)
		{
			if (NKCGuildManager.m_lstMarkTemplet.Count > idx)
			{
				return NKCGuildManager.m_lstMarkTemplet[idx];
			}
			return null;
		}

		// Token: 0x060036B1 RID: 14001 RVA: 0x00119E29 File Offset: 0x00118029
		public static NKMGuildBadgeColorTemplet GetBadgeColorTempletByIndex(int idx)
		{
			if (NKCGuildManager.m_lstBadgeColorTemplet.Count > idx)
			{
				return NKCGuildManager.m_lstBadgeColorTemplet[idx];
			}
			return null;
		}

		// Token: 0x060036B2 RID: 14002 RVA: 0x00119E45 File Offset: 0x00118045
		public static void Send_GUILD_LIST_REQ(GuildListType guildListType)
		{
			NKCGuildManager.m_GuildListType = guildListType;
			NKCPacketSender.Send_NKMPacket_GUILD_LIST_REQ(guildListType);
		}

		// Token: 0x060036B3 RID: 14003 RVA: 0x00119E54 File Offset: 0x00118054
		public static void ChangeGuildMemberData(NKMCommonProfile commonProfile, DateTime lastOnlineTime)
		{
			if (NKCGuildManager.MyGuildData == null)
			{
				return;
			}
			bool flag = false;
			NKMGuildMemberData nkmguildMemberData = NKCGuildManager.MyGuildData.members.Find((NKMGuildMemberData x) => x.commonProfile.userUid == commonProfile.userUid);
			if (nkmguildMemberData != null)
			{
				nkmguildMemberData.commonProfile = commonProfile;
				nkmguildMemberData.lastOnlineTime = lastOnlineTime;
				flag = true;
			}
			if (!flag)
			{
				FriendListData friendListData = NKCGuildManager.MyGuildData.joinWaitingList.Find((FriendListData x) => x.commonProfile.userUid == commonProfile.userUid);
				if (friendListData != null)
				{
					friendListData.commonProfile = commonProfile;
					friendListData.lastLoginDate = lastOnlineTime;
					flag = true;
				}
				FriendListData friendListData2 = NKCGuildManager.MyGuildData.inviteList.Find((FriendListData x) => x.commonProfile.userUid == commonProfile.userUid);
				if (friendListData2 != null)
				{
					friendListData2.commonProfile = commonProfile;
					friendListData2.lastLoginDate = lastOnlineTime;
					flag = true;
				}
			}
			if (flag)
			{
				NKCUIManager.OnGuildDataChanged();
			}
		}

		// Token: 0x060036B4 RID: 14004 RVA: 0x00119F23 File Offset: 0x00118123
		public static void OnRecv(NKMPacket_GUILD_CREATE_ACK cNKMPacket_GUILD_CREATE_ACK)
		{
			if (cNKMPacket_GUILD_CREATE_ACK.guildData != null)
			{
				if (NKCUIGuildCreate.IsInstanceOpen)
				{
					NKCUIGuildCreate.Instance.Close();
				}
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GUILD_LOBBY, true);
			}
		}

		// Token: 0x060036B5 RID: 14005 RVA: 0x00119F4B File Offset: 0x0011814B
		public static void OnRecv(NKMPacket_GUILD_SEARCH_ACK cNKMPacket_GUILD_SEARCH_ACK)
		{
			NKCGuildManager.m_lstSearchData = cNKMPacket_GUILD_SEARCH_ACK.list;
			if (NKCUIGuildJoin.IsInstanceOpen)
			{
				NKCUIGuildJoin.Instance.RefreshUI();
			}
		}

		// Token: 0x060036B6 RID: 14006 RVA: 0x00119F6C File Offset: 0x0011816C
		public static void OnRecv(NKMPacket_GUILD_LIST_ACK cNKMPacket_GUILD_LIST_ACK)
		{
			GuildListType guildListType = NKCGuildManager.m_GuildListType;
			if (guildListType != GuildListType.SendRequest)
			{
				if (guildListType == GuildListType.ReceiveInvite)
				{
					NKCGuildManager.m_lstInvitedData = cNKMPacket_GUILD_LIST_ACK.list;
				}
			}
			else
			{
				NKCGuildManager.m_lstRequestedData = cNKMPacket_GUILD_LIST_ACK.list;
			}
			if (NKCUIGuildJoin.IsInstanceOpen)
			{
				NKCUIGuildJoin.Instance.RefreshUI();
			}
		}

		// Token: 0x060036B7 RID: 14007 RVA: 0x00119FAF File Offset: 0x001181AF
		public static void Send_GUILD_JOIN_REQ(long guildUid, string guildName, GuildJoinType joinType)
		{
			NKCGuildManager.m_lastJoinRequestedGuildName = guildName;
			NKCGuildManager.m_lastJoinRequestedGuildUid = guildUid;
			NKCPacketSender.Send_NKMPacket_GUILD_JOIN_REQ(guildUid, joinType);
		}

		// Token: 0x060036B8 RID: 14008 RVA: 0x00119FC4 File Offset: 0x001181C4
		public static void OnRecv(NKMPacket_GUILD_JOIN_ACK cNKMPacket_GUILD_JOIN_ACK)
		{
			if (cNKMPacket_GUILD_JOIN_ACK.needApproval)
			{
				NKCGuildManager.RemoveSearchData(cNKMPacket_GUILD_JOIN_ACK.guildUid);
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_CONSORTIUM_JOIN_CONFIRM_JOIN_SUCCESS_POPUP_TITLE_DESC, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_JOIN_CONFIRM_JOIN_SUCCESS_POPUP_BODY_DESC, NKCGuildManager.m_lastJoinRequestedGuildName), null, "");
				NKCGuildManager.Send_GUILD_LIST_REQ(GuildListType.SendRequest);
			}
			NKCGuildManager.m_lastJoinRequestedGuildUid = 0L;
			NKCGuildManager.m_lastJoinRequestedGuildName = string.Empty;
		}

		// Token: 0x060036B9 RID: 14009 RVA: 0x0011A01C File Offset: 0x0011821C
		public static void OnRecv(NKMPacket_GUILD_CANCEL_JOIN_ACK cNKMPacket_GUILD_CANCEL_JOIN_ACK)
		{
			GuildListData guildListData = NKCGuildManager.m_lstRequestedData.Find((GuildListData x) => x.guildUid == cNKMPacket_GUILD_CANCEL_JOIN_ACK.guildUid);
			if (guildListData != null)
			{
				NKCGuildManager.m_lstRequestedData.Remove(guildListData);
			}
			if (NKCUIGuildJoin.IsInstanceOpen)
			{
				NKCUIGuildJoin.Instance.RefreshUI();
			}
		}

		// Token: 0x060036BA RID: 14010 RVA: 0x0011A070 File Offset: 0x00118270
		public static bool CheckNameLength(string nickName, int minByte, int maxByte)
		{
			if (string.IsNullOrEmpty(nickName))
			{
				return false;
			}
			int nickNameLength = NKM_USER_COMMON.GetNickNameLength(nickName);
			return nickNameLength >= minByte && nickNameLength <= maxByte;
		}

		// Token: 0x060036BB RID: 14011 RVA: 0x0011A099 File Offset: 0x00118299
		public static void SetLastNoticeChangedTimeUTC(DateTime changedTimeUTC)
		{
			NKCGuildManager.LastNoticeChangedTimeUTC = changedTimeUTC;
		}

		// Token: 0x060036BC RID: 14012 RVA: 0x0011A0A4 File Offset: 0x001182A4
		public static int GetRemainDonationCount()
		{
			if (NKCGuildManager.IsFirstDay())
			{
				return 0;
			}
			if (DailyReset.CalcNextReset(NKCGuildManager.MyData.lastDailyResetDate) > ServiceTime.Recent)
			{
				return NKMCommonConst.Guild.DailyDonationCount - NKCGuildManager.MyData.donationCount;
			}
			return NKMCommonConst.Guild.DailyDonationCount;
		}

		// Token: 0x060036BD RID: 14013 RVA: 0x0011A0F8 File Offset: 0x001182F8
		public static NKMGuildSimpleData GetMyGuildSimpleData()
		{
			NKMGuildSimpleData nkmguildSimpleData = new NKMGuildSimpleData();
			if (NKCGuildManager.MyGuildData == null || NKCGuildManager.MyGuildData.guildUid <= 0L)
			{
				return nkmguildSimpleData;
			}
			nkmguildSimpleData.badgeId = NKCGuildManager.MyGuildData.badgeId;
			nkmguildSimpleData.guildUid = NKCGuildManager.MyGuildData.guildUid;
			nkmguildSimpleData.guildName = NKCGuildManager.MyGuildData.name;
			return nkmguildSimpleData;
		}

		// Token: 0x060036BE RID: 14014 RVA: 0x0011A154 File Offset: 0x00118354
		public static int GetMaxGuildMemberCount(int guildLevel)
		{
			GuildExpTemplet guildExpTemplet = NKMTempletContainer<GuildExpTemplet>.Find(guildLevel);
			if (guildExpTemplet != null)
			{
				return guildExpTemplet.MaxMemberCount;
			}
			return 0;
		}

		// Token: 0x040033E5 RID: 13285
		public const int MaxGuildNoticeLength = 36;

		// Token: 0x040033E6 RID: 13286
		public const int MaxGuildGreetingLength = 40;

		// Token: 0x040033E7 RID: 13287
		public const int MaxUserGreetingLength = 13;

		// Token: 0x040033E8 RID: 13288
		private static List<NKMGuildBadgeFrameTemplet> m_lstFrameTemplet = new List<NKMGuildBadgeFrameTemplet>();

		// Token: 0x040033E9 RID: 13289
		private static List<NKMGuildBadgeMarkTemplet> m_lstMarkTemplet = new List<NKMGuildBadgeMarkTemplet>();

		// Token: 0x040033EA RID: 13290
		private static List<NKMGuildBadgeColorTemplet> m_lstBadgeColorTemplet = new List<NKMGuildBadgeColorTemplet>();

		// Token: 0x040033EB RID: 13291
		public static List<GuildListData> m_lstSearchData = new List<GuildListData>();

		// Token: 0x040033EC RID: 13292
		public static List<GuildListData> m_lstRequestedData = new List<GuildListData>();

		// Token: 0x040033ED RID: 13293
		public static List<GuildListData> m_lstInvitedData = new List<GuildListData>();

		// Token: 0x040033F1 RID: 13297
		private static GuildListType m_GuildListType;

		// Token: 0x040033F2 RID: 13298
		private static string m_lastJoinRequestedGuildName = "";

		// Token: 0x040033F3 RID: 13299
		private static long m_lastJoinRequestedGuildUid = 0L;
	}
}
