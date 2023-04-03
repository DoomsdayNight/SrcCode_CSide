using System;
using System.Collections.Generic;
using ClientPacket.Guild;
using ClientPacket.LeaderBoard;
using ClientPacket.Mode;
using Cs.Logging;
using NKC.UI;
using NKM;
using NKM.Templet;

namespace NKC
{
	// Token: 0x02000691 RID: 1681
	public class NKCLeaderBoardManager
	{
		// Token: 0x060036EF RID: 14063 RVA: 0x0011AC60 File Offset: 0x00118E60
		public static void Initialize()
		{
			NKCLeaderBoardManager.m_dicLeaderBoardData.Clear();
			NKCLeaderBoardManager.m_dicLastUpdateTime.Clear();
			NKCLeaderBoardManager.m_dicAllReq.Clear();
			NKCLeaderBoardManager.m_dicMyRankSlotData.Clear();
		}

		// Token: 0x060036F0 RID: 14064 RVA: 0x0011AC8A File Offset: 0x00118E8A
		public static bool HasLeaderBoardData(NKMLeaderBoardTemplet boardTemplet)
		{
			return !NKCLeaderBoardManager.NeedRefreshData(boardTemplet) && NKCLeaderBoardManager.m_dicLeaderBoardData.ContainsKey(boardTemplet.m_BoardID);
		}

		// Token: 0x060036F1 RID: 14065 RVA: 0x0011ACA6 File Offset: 0x00118EA6
		public static List<LeaderBoardSlotData> GetLeaderBoardData(int boardId)
		{
			if (NKCLeaderBoardManager.m_dicLeaderBoardData.ContainsKey(boardId))
			{
				return NKCLeaderBoardManager.m_dicLeaderBoardData[boardId];
			}
			return new List<LeaderBoardSlotData>();
		}

		// Token: 0x060036F2 RID: 14066 RVA: 0x0011ACC6 File Offset: 0x00118EC6
		public static LeaderBoardSlotData GetMyRankSlotData(int boardId)
		{
			if (NKCLeaderBoardManager.m_dicMyRankSlotData.ContainsKey(boardId))
			{
				return NKCLeaderBoardManager.m_dicMyRankSlotData[boardId];
			}
			return new LeaderBoardSlotData();
		}

		// Token: 0x060036F3 RID: 14067 RVA: 0x0011ACE6 File Offset: 0x00118EE6
		private static DateTime GetLastUpdateTime(int boardId)
		{
			if (NKCLeaderBoardManager.m_dicLastUpdateTime.ContainsKey(boardId))
			{
				return NKCLeaderBoardManager.m_dicLastUpdateTime[boardId];
			}
			return DateTime.MinValue;
		}

		// Token: 0x060036F4 RID: 14068 RVA: 0x0011AD08 File Offset: 0x00118F08
		public static DateTime GetNextResetTime(NKMLeaderBoardTemplet templet)
		{
			LeaderBoardType boardTab = templet.m_BoardTab;
			if (boardTab == LeaderBoardType.BT_ACHIEVE || boardTab != LeaderBoardType.BT_SHADOW)
			{
			}
			return DateTime.MaxValue;
		}

		// Token: 0x060036F5 RID: 14069 RVA: 0x0011AD2C File Offset: 0x00118F2C
		public static bool GetReceivedAllData(int boardId)
		{
			return NKCLeaderBoardManager.m_dicAllReq.ContainsKey(boardId) && NKCLeaderBoardManager.m_dicAllReq[boardId];
		}

		// Token: 0x060036F6 RID: 14070 RVA: 0x0011AD48 File Offset: 0x00118F48
		public static void SendReq(NKMLeaderBoardTemplet templet, bool bAllReq)
		{
			switch (templet.m_BoardTab)
			{
			case LeaderBoardType.BT_ACHIEVE:
				NKCPacketSender.Send_NKMPacket_LEADERBOARD_ACHIEVE_LIST_REQ(bAllReq);
				return;
			case LeaderBoardType.BT_COLLECTION:
			case LeaderBoardType.BT_PVP_RANK:
			case LeaderBoardType.BT_PVP_LEAGUE_TOP:
				break;
			case LeaderBoardType.BT_SHADOW:
				NKCPacketSender.Send_NKMPacket_LEADERBOARD_SHADOWPALACE_LIST_REQ(templet.m_BoardCriteria, bAllReq);
				return;
			case LeaderBoardType.BT_FIERCE:
			{
				NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
				if (nkcfierceBattleSupportDataMgr != null && nkcfierceBattleSupportDataMgr.FierceTemplet != null && nkcfierceBattleSupportDataMgr.GetStatus() != NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_UNUSABLE && nkcfierceBattleSupportDataMgr.GetStatus() != NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_WAIT)
				{
					NKCPacketSender.Send_NKMPacket_LEADERBOARD_FIERCE_LIST_REQ(bAllReq);
					return;
				}
				break;
			}
			case LeaderBoardType.BT_GUILD:
				if (templet.m_BoardCriteria == 1)
				{
					NKCPacketSender.Send_NKMPacket_LEADERBOARD_GUILD_LEVEL_RANK_LIST_REQ();
					return;
				}
				NKCPacketSender.Send_NKMPacket_LEADERBOARD_GUILD_UNION_RANK_LIST_REQ(templet.m_BoardCriteria);
				return;
			case LeaderBoardType.BT_TIMEATTACK:
				NKCPacketSender.Send_NKMPacket_LEADERBOARD_TIMEATTACK_LIST_REQ(templet.m_BoardCriteria, bAllReq);
				break;
			default:
				return;
			}
		}

		// Token: 0x060036F7 RID: 14071 RVA: 0x0011ADEC File Offset: 0x00118FEC
		public static bool NeedRefreshData(NKMLeaderBoardTemplet templet)
		{
			return !(NKCSynchronizedTime.GetServerUTCTime(0.0) - NKCLeaderBoardManager.GetLastUpdateTime(templet.m_BoardID) < TimeSpan.FromSeconds((double)NKCLeaderBoardManager.m_fRefreshInterval));
		}

		// Token: 0x060036F8 RID: 14072 RVA: 0x0011AE24 File Offset: 0x00119024
		private static void SaveLeaderBoardData(LeaderBoardType boardType, int boardID, int userRank, long myScore, bool bIsAll, List<LeaderBoardSlotData> lstSlotData)
		{
			if (NKCLeaderBoardManager.m_dicLastUpdateTime.ContainsKey(boardID))
			{
				NKCLeaderBoardManager.m_dicLastUpdateTime[boardID] = NKCSynchronizedTime.GetServerUTCTime(0.0);
			}
			else
			{
				NKCLeaderBoardManager.m_dicLastUpdateTime.Add(boardID, NKCSynchronizedTime.GetServerUTCTime(0.0));
			}
			if (NKCLeaderBoardManager.m_dicLeaderBoardData.ContainsKey(boardID))
			{
				NKCLeaderBoardManager.m_dicLeaderBoardData[boardID] = lstSlotData;
			}
			else
			{
				NKCLeaderBoardManager.m_dicLeaderBoardData.Add(boardID, lstSlotData);
			}
			if (NKCLeaderBoardManager.m_dicMyRankSlotData.ContainsKey(boardID))
			{
				NKCLeaderBoardManager.m_dicMyRankSlotData[boardID].rank = userRank;
				NKCLeaderBoardManager.m_dicMyRankSlotData[boardID].score = LeaderBoardSlotData.GetScoreByBoardType(boardType, myScore);
			}
			else
			{
				LeaderBoardSlotData value = LeaderBoardSlotData.MakeMySlotData(boardType, userRank, LeaderBoardSlotData.GetScoreByBoardType(boardType, myScore), boardType == LeaderBoardType.BT_GUILD);
				NKCLeaderBoardManager.m_dicMyRankSlotData.Add(boardID, value);
			}
			if (NKCLeaderBoardManager.m_dicAllReq.ContainsKey(boardID))
			{
				NKCLeaderBoardManager.m_dicAllReq[boardID] = bIsAll;
				return;
			}
			NKCLeaderBoardManager.m_dicAllReq.Add(boardID, bIsAll);
		}

		// Token: 0x060036F9 RID: 14073 RVA: 0x0011AF1C File Offset: 0x0011911C
		public static void OnRecv(NKMPacket_LEADERBOARD_ACHIEVE_LIST_ACK sPacket)
		{
			if (sPacket.leaderBoardAchieveData.achieveData == null)
			{
				sPacket.leaderBoardAchieveData.achieveData = new List<NKMAchieveData>();
			}
			NKMLeaderBoardTemplet nkmleaderBoardTemplet = NKMLeaderBoardTemplet.Find(LeaderBoardType.BT_ACHIEVE, 0);
			if (nkmleaderBoardTemplet == null)
			{
				Log.Error(string.Format("NKMLeaderBoardTemplet is null - tab : {0} / criteria : {1}", LeaderBoardType.BT_ACHIEVE, 0), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCLeaderBoardManager.cs", 461);
				return;
			}
			List<LeaderBoardSlotData> list = new List<LeaderBoardSlotData>();
			for (int i = 0; i < sPacket.leaderBoardAchieveData.achieveData.Count; i++)
			{
				LeaderBoardSlotData item = LeaderBoardSlotData.MakeSlotData(sPacket.leaderBoardAchieveData.achieveData[i], i + 1);
				list.Add(item);
			}
			NKCLeaderBoardManager.SaveLeaderBoardData(LeaderBoardType.BT_ACHIEVE, nkmleaderBoardTemplet.m_BoardID, sPacket.userRank, NKCScenManager.CurrentUserData().m_MissionData.GetAchiecePoint(), sPacket.isAll, list);
			if (NKCUILeaderBoard.IsInstanceOpen)
			{
				NKCUILeaderBoard.Instance.RefreshUI(!sPacket.isAll);
			}
		}

		// Token: 0x060036FA RID: 14074 RVA: 0x0011AFFB File Offset: 0x001191FB
		public static int GetMyShadowPalaceTimeByLeaderBoardTemplet(NKMLeaderBoardTemplet templet)
		{
			return NKCLeaderBoardManager.GetMyShadowPalaceTime(templet.m_BoardCriteria);
		}

		// Token: 0x060036FB RID: 14075 RVA: 0x0011B008 File Offset: 0x00119208
		public static int GetMyShadowPalaceTime(int palaceId)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return 0;
			}
			if (nkmuserData.m_ShadowPalace == null || nkmuserData.m_ShadowPalace.palaceDataList == null)
			{
				return 0;
			}
			NKMPalaceData nkmpalaceData = nkmuserData.m_ShadowPalace.palaceDataList.Find((NKMPalaceData x) => x.palaceId == palaceId);
			if (nkmpalaceData != null && nkmpalaceData.dungeonDataList.Count == NKMShadowPalaceManager.GetBattleTemplets(palaceId).Count)
			{
				int num = 0;
				for (int i = 0; i < nkmpalaceData.dungeonDataList.Count; i++)
				{
					num += nkmpalaceData.dungeonDataList[i].bestTime;
				}
				return num;
			}
			return 0;
		}

		// Token: 0x060036FC RID: 14076 RVA: 0x0011B0B8 File Offset: 0x001192B8
		public static void OnRecv(NKMPacket_LEADERBOARD_SHADOWPALACE_LIST_ACK cPacket)
		{
			if (cPacket.leaderBoardShadowPalaceData.shadowPalaceData == null)
			{
				cPacket.leaderBoardShadowPalaceData.shadowPalaceData = new List<NKMShadowPalaceData>();
			}
			NKMLeaderBoardTemplet nkmleaderBoardTemplet = NKMLeaderBoardTemplet.Find(LeaderBoardType.BT_SHADOW, cPacket.actId);
			if (nkmleaderBoardTemplet == null)
			{
				Log.Error(string.Format("NKMLeaderBoardTemplet is null - tab : {0}, id : {1}", LeaderBoardType.BT_SHADOW, cPacket.actId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCLeaderBoardManager.cs", 518);
				return;
			}
			List<LeaderBoardSlotData> list = new List<LeaderBoardSlotData>();
			for (int i = 0; i < cPacket.leaderBoardShadowPalaceData.shadowPalaceData.Count; i++)
			{
				LeaderBoardSlotData item = LeaderBoardSlotData.MakeSlotData(cPacket.leaderBoardShadowPalaceData.shadowPalaceData[i], i + 1);
				list.Add(item);
			}
			NKCLeaderBoardManager.SaveLeaderBoardData(LeaderBoardType.BT_SHADOW, nkmleaderBoardTemplet.m_BoardID, cPacket.userRank, (long)NKCLeaderBoardManager.GetMyShadowPalaceTime(nkmleaderBoardTemplet.m_BoardCriteria), cPacket.isAll, list);
			if (NKCUILeaderBoard.IsInstanceOpen)
			{
				NKCUILeaderBoard.Instance.RefreshUI(!cPacket.isAll);
			}
		}

		// Token: 0x060036FD RID: 14077 RVA: 0x0011B1A0 File Offset: 0x001193A0
		public static void OnRecv(NKMPacket_LEADERBOARD_FIERCE_LIST_ACK cPacket)
		{
			if (cPacket.leaderBoardfierceData == null)
			{
				cPacket.leaderBoardfierceData = new NKMLeaderBoardFierceData();
			}
			NKMLeaderBoardTemplet nkmleaderBoardTemplet = NKMLeaderBoardTemplet.Find(LeaderBoardType.BT_FIERCE, 0);
			if (nkmleaderBoardTemplet == null)
			{
				Log.Error(string.Format("NKMLeaderBoardTemplet is null - tab : {0}", LeaderBoardType.BT_FIERCE), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCLeaderBoardManager.cs", 551);
				return;
			}
			List<LeaderBoardSlotData> list = new List<LeaderBoardSlotData>();
			for (int i = 0; i < cPacket.leaderBoardfierceData.fierceData.Count; i++)
			{
				LeaderBoardSlotData item = LeaderBoardSlotData.MakeSlotData(cPacket.leaderBoardfierceData.fierceData[i], i + 1);
				list.Add(item);
			}
			int num = cPacket.userRank;
			if (num == 0 || num > 100)
			{
				num = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr().GetRankingTotalPercent();
			}
			NKCLeaderBoardManager.SaveLeaderBoardData(LeaderBoardType.BT_FIERCE, nkmleaderBoardTemplet.m_BoardID, num, (long)NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr().GetTotalPoint(), cPacket.isAll, list);
			if (NKCUILeaderBoard.IsInstanceOpen)
			{
				NKCUILeaderBoard.Instance.RefreshUI(!cPacket.isAll);
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_FIERCE_BATTLE_SUPPORT)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_FIERCE_BATTLE_SUPPORT().RefreshLeaderBoard();
			}
		}

		// Token: 0x060036FE RID: 14078 RVA: 0x0011B2AC File Offset: 0x001194AC
		public static void OnRecv(NKMPacket_LEADERBOARD_GUILD_LEVEL_RANK_LIST_ACK sPacket)
		{
			if (sPacket.leaderBoard.rankDatas == null)
			{
				sPacket.leaderBoard.rankDatas = new List<NKMGuildRankData>();
			}
			NKMLeaderBoardTemplet nkmleaderBoardTemplet = NKMLeaderBoardTemplet.Find(LeaderBoardType.BT_GUILD, 1);
			if (nkmleaderBoardTemplet == null)
			{
				Log.Error(string.Format("NKMLeaderBoardTemplet is null - tab : {0}, criteria : {1}", LeaderBoardType.BT_GUILD, 1), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCLeaderBoardManager.cs", 590);
				return;
			}
			List<LeaderBoardSlotData> list = new List<LeaderBoardSlotData>();
			for (int i = 0; i < sPacket.leaderBoard.rankDatas.Count; i++)
			{
				LeaderBoardSlotData item = LeaderBoardSlotData.MakeSlotData(sPacket.leaderBoard.rankDatas[i], i + 1);
				list.Add(item);
			}
			NKCLeaderBoardManager.SaveLeaderBoardData(LeaderBoardType.BT_GUILD, nkmleaderBoardTemplet.m_BoardID, sPacket.myRankData.rank, sPacket.myRankData.score, true, list);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GUILD_LOBBY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_LOBBY().RefreshUI();
			}
			if (NKCUILeaderBoard.IsInstanceOpen)
			{
				NKCUILeaderBoard.Instance.RefreshUI(true);
			}
		}

		// Token: 0x060036FF RID: 14079 RVA: 0x0011B39C File Offset: 0x0011959C
		public static void OnRecv(NKMPacket_LEADERBOARD_GUILD_UNION_RANK_LIST_ACK sPacket)
		{
			if (sPacket.leaderBoard.rankDatas == null)
			{
				sPacket.leaderBoard.rankDatas = new List<NKMGuildRankData>();
			}
			NKMLeaderBoardTemplet nkmleaderBoardTemplet = NKMLeaderBoardTemplet.Find(LeaderBoardType.BT_GUILD, sPacket.seasonId);
			if (nkmleaderBoardTemplet == null)
			{
				Log.Error(string.Format("NKMLeaderBoardTemplet is null - tab : {0}, criteria : {1}", LeaderBoardType.BT_GUILD, sPacket.seasonId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCLeaderBoardManager.cs", 618);
				return;
			}
			List<LeaderBoardSlotData> list = new List<LeaderBoardSlotData>();
			for (int i = 0; i < sPacket.leaderBoard.rankDatas.Count; i++)
			{
				LeaderBoardSlotData item = LeaderBoardSlotData.MakeSlotData(sPacket.leaderBoard.rankDatas[i], i + 1);
				list.Add(item);
			}
			NKCLeaderBoardManager.SaveLeaderBoardData(LeaderBoardType.BT_GUILD, nkmleaderBoardTemplet.m_BoardID, sPacket.myRankData.rank, sPacket.myRankData.score, true, list);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GUILD_LOBBY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_LOBBY().RefreshUI();
			}
			if (NKCUILeaderBoard.IsInstanceOpen)
			{
				NKCUILeaderBoard.Instance.RefreshUI(true);
			}
		}

		// Token: 0x06003700 RID: 14080 RVA: 0x0011B498 File Offset: 0x00119698
		public static NKMGuildRankData MakeMyGuildRankData(int boardId, out int myRank)
		{
			myRank = 0;
			if (!NKCGuildManager.HasGuild())
			{
				return new NKMGuildRankData();
			}
			NKMGuildRankData nkmguildRankData = new NKMGuildRankData();
			nkmguildRankData.badgeId = NKCGuildManager.MyGuildData.badgeId;
			nkmguildRankData.guildLevel = NKCGuildManager.MyGuildData.guildLevel;
			nkmguildRankData.guildName = NKCGuildManager.MyGuildData.name;
			nkmguildRankData.guildUid = NKCGuildManager.MyGuildData.guildUid;
			nkmguildRankData.masterNickname = NKCGuildManager.MyGuildData.members.Find((NKMGuildMemberData x) => x.grade == GuildMemberGrade.Master).commonProfile.nickname;
			nkmguildRankData.memberCount = NKCGuildManager.MyGuildData.members.Count;
			if (!long.TryParse(NKCLeaderBoardManager.GetMyRankSlotData(boardId).score, out nkmguildRankData.rankValue))
			{
				nkmguildRankData.rankValue = 0L;
			}
			myRank = NKCLeaderBoardManager.GetMyRankSlotData(boardId).rank;
			return nkmguildRankData;
		}

		// Token: 0x06003701 RID: 14081 RVA: 0x0011B57C File Offset: 0x0011977C
		public static void OnRecv(NKMPacket_LEADERBOARD_TIMEATTACK_LIST_ACK sPacket)
		{
			if (sPacket.leaderBoardTimeAttackData.timeAttackData == null)
			{
				sPacket.leaderBoardTimeAttackData.timeAttackData = new List<NKMTimeAttackData>();
			}
			NKMLeaderBoardTemplet nkmleaderBoardTemplet = NKMLeaderBoardTemplet.Find(LeaderBoardType.BT_TIMEATTACK, sPacket.stageId);
			if (nkmleaderBoardTemplet == null)
			{
				Log.Error(string.Format("NKMLeaderBoardTemplet is null - tab : {0}, id : {1}", LeaderBoardType.BT_TIMEATTACK, sPacket.stageId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCLeaderBoardManager.cs", 672);
				return;
			}
			List<LeaderBoardSlotData> list = new List<LeaderBoardSlotData>();
			for (int i = 0; i < sPacket.leaderBoardTimeAttackData.timeAttackData.Count; i++)
			{
				LeaderBoardSlotData item = LeaderBoardSlotData.MakeSlotData(sPacket.leaderBoardTimeAttackData.timeAttackData[i], i + 1);
				list.Add(item);
			}
			NKCLeaderBoardManager.SaveLeaderBoardData(LeaderBoardType.BT_TIMEATTACK, nkmleaderBoardTemplet.m_BoardID, sPacket.userRank, (long)NKCScenManager.CurrentUserData().GetStageBestClearSec(nkmleaderBoardTemplet.m_BoardCriteria), sPacket.isAll, list);
			if (NKCUILeaderBoard.IsInstanceOpen)
			{
				NKCUILeaderBoard.Instance.RefreshUI(!sPacket.isAll);
				return;
			}
			if (NKCPopupLeaderBoardSingle.IsInstanceOpen)
			{
				NKCPopupLeaderBoardSingle.Instance.RefreshUI(!sPacket.isAll);
			}
		}

		// Token: 0x04003409 RID: 13321
		public const int GUILD_LEVEL_RANK_CRITERIA = 1;

		// Token: 0x0400340A RID: 13322
		public static Dictionary<int, List<LeaderBoardSlotData>> m_dicLeaderBoardData = new Dictionary<int, List<LeaderBoardSlotData>>();

		// Token: 0x0400340B RID: 13323
		public static Dictionary<int, LeaderBoardSlotData> m_dicMyRankSlotData = new Dictionary<int, LeaderBoardSlotData>();

		// Token: 0x0400340C RID: 13324
		public static Dictionary<int, DateTime> m_dicLastUpdateTime = new Dictionary<int, DateTime>();

		// Token: 0x0400340D RID: 13325
		public static Dictionary<int, bool> m_dicAllReq = new Dictionary<int, bool>();

		// Token: 0x0400340E RID: 13326
		private static float m_fRefreshInterval = 10f;
	}
}
