using System;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.Common;
using ClientPacket.Guild;
using Cs.Logging;
using NKM;
using NKM.Guild;
using NKM.Templet;

namespace NKC
{
	// Token: 0x02000689 RID: 1673
	public static class NKCGuildCoopManager
	{
		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06003651 RID: 13905 RVA: 0x001185C4 File Offset: 0x001167C4
		// (set) Token: 0x06003652 RID: 13906 RVA: 0x001185CB File Offset: 0x001167CB
		public static GuildDungeonState m_GuildDungeonState { get; private set; }

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06003653 RID: 13907 RVA: 0x001185D3 File Offset: 0x001167D3
		// (set) Token: 0x06003654 RID: 13908 RVA: 0x001185DA File Offset: 0x001167DA
		public static int m_SeasonId { get; private set; }

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06003655 RID: 13909 RVA: 0x001185E2 File Offset: 0x001167E2
		// (set) Token: 0x06003656 RID: 13910 RVA: 0x001185E9 File Offset: 0x001167E9
		public static int m_SessionId { get; private set; }

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06003657 RID: 13911 RVA: 0x001185F1 File Offset: 0x001167F1
		// (set) Token: 0x06003658 RID: 13912 RVA: 0x001185F8 File Offset: 0x001167F8
		public static DateTime m_SessionEndDateUTC { get; private set; }

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06003659 RID: 13913 RVA: 0x00118600 File Offset: 0x00116800
		// (set) Token: 0x0600365A RID: 13914 RVA: 0x00118607 File Offset: 0x00116807
		public static DateTime m_NextSessionStartDateUTC { get; private set; }

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x0600365B RID: 13915 RVA: 0x0011860F File Offset: 0x0011680F
		// (set) Token: 0x0600365C RID: 13916 RVA: 0x00118616 File Offset: 0x00116816
		public static long m_KillPoint { get; private set; }

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x0600365D RID: 13917 RVA: 0x0011861E File Offset: 0x0011681E
		// (set) Token: 0x0600365E RID: 13918 RVA: 0x00118625 File Offset: 0x00116825
		public static int m_TryCount { get; private set; }

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x0600365F RID: 13919 RVA: 0x0011862D File Offset: 0x0011682D
		// (set) Token: 0x06003660 RID: 13920 RVA: 0x00118634 File Offset: 0x00116834
		public static bool m_bCanReward { get; private set; }

		// Token: 0x06003661 RID: 13921 RVA: 0x0011863C File Offset: 0x0011683C
		public static List<GuildDungeonMemberInfo> GetGuildMemberInfo()
		{
			return NKCGuildCoopManager.m_lstGuildDungeonMemberInfo;
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06003662 RID: 13922 RVA: 0x00118643 File Offset: 0x00116843
		// (set) Token: 0x06003663 RID: 13923 RVA: 0x0011864A File Offset: 0x0011684A
		public static int m_ArenaPlayableCount { get; private set; }

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06003664 RID: 13924 RVA: 0x00118652 File Offset: 0x00116852
		// (set) Token: 0x06003665 RID: 13925 RVA: 0x00118659 File Offset: 0x00116859
		public static GuildRaidTemplet m_cGuildRaidTemplet { get; private set; }

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06003666 RID: 13926 RVA: 0x00118661 File Offset: 0x00116861
		// (set) Token: 0x06003667 RID: 13927 RVA: 0x00118668 File Offset: 0x00116868
		public static float m_BossRemainHp { get; private set; }

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06003668 RID: 13928 RVA: 0x00118670 File Offset: 0x00116870
		// (set) Token: 0x06003669 RID: 13929 RVA: 0x00118677 File Offset: 0x00116877
		public static float m_BossMaxHp { get; private set; }

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x0600366A RID: 13930 RVA: 0x0011867F File Offset: 0x0011687F
		// (set) Token: 0x0600366B RID: 13931 RVA: 0x00118686 File Offset: 0x00116886
		public static long m_BossPlayUserUid { get; private set; }

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x0600366C RID: 13932 RVA: 0x0011868E File Offset: 0x0011688E
		// (set) Token: 0x0600366D RID: 13933 RVA: 0x00118695 File Offset: 0x00116895
		public static int m_BossPlayableCount { get; private set; }

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x0600366E RID: 13934 RVA: 0x0011869D File Offset: 0x0011689D
		// (set) Token: 0x0600366F RID: 13935 RVA: 0x001186A4 File Offset: 0x001168A4
		public static bool m_bGuildCoopDataRecved { get; private set; }

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06003670 RID: 13936 RVA: 0x001186AC File Offset: 0x001168AC
		// (set) Token: 0x06003671 RID: 13937 RVA: 0x001186B3 File Offset: 0x001168B3
		public static bool m_bGuildCoopMemberDataRecved { get; private set; }

		// Token: 0x06003672 RID: 13938 RVA: 0x001186BC File Offset: 0x001168BC
		public static void Initialize()
		{
			NKCGuildCoopManager.m_GuildDungeonState = GuildDungeonState.Invalid;
			NKCGuildCoopManager.m_SessionEndDateUTC = default(DateTime);
			NKCGuildCoopManager.m_NextSessionStartDateUTC = default(DateTime);
			NKCGuildCoopManager.m_SeasonId = 0;
			NKCGuildCoopManager.m_SessionId = 0;
			NKCGuildCoopManager.m_BossRemainHp = 0f;
			NKCGuildCoopManager.m_BossMaxHp = 0f;
			NKCGuildCoopManager.m_BossPlayUserUid = 0L;
			NKCGuildCoopManager.m_TryCount = 0;
			NKCGuildCoopManager.m_KillPoint = 0L;
			NKCGuildCoopManager.m_ArenaTicketBuyCount = 0;
			NKCGuildCoopManager.m_dicGuildDungeonInfoTemplet.Clear();
			NKCGuildCoopManager.m_lstGuildDungeonArena.Clear();
			NKCGuildCoopManager.m_lstGuildDungeonMemberInfo.Clear();
			NKCGuildCoopManager.m_LastReceivedSeasonRewardData.Clear();
			NKCGuildCoopManager.m_lstKillPointReward.Clear();
			NKCGuildCoopManager.m_lstTryCountReward.Clear();
			NKCGuildCoopManager.m_BossPlayableCount = 0;
			NKCGuildCoopManager.m_bCanReward = false;
			NKCGuildCoopManager.m_cGuildRaidTemplet = null;
			NKCGuildCoopManager.m_bGuildCoopDataRecved = false;
			NKCGuildCoopManager.m_bGuildCoopMemberDataRecved = false;
		}

		// Token: 0x06003673 RID: 13939 RVA: 0x0011877F File Offset: 0x0011697F
		public static void ResetGuildCoopState()
		{
			NKCGuildCoopManager.m_bGuildCoopDataRecved = false;
			NKCGuildCoopManager.m_bGuildCoopMemberDataRecved = false;
			NKCGuildCoopManager.m_GuildDungeonState = GuildDungeonState.Invalid;
		}

		// Token: 0x06003674 RID: 13940 RVA: 0x00118793 File Offset: 0x00116993
		public static void ResetGuildCoopSessionData()
		{
			NKCGuildCoopManager.m_dicGuildDungeonInfoTemplet.Clear();
			NKCGuildCoopManager.m_lstGuildDungeonMemberInfo.Clear();
			NKCGuildCoopManager.m_ArenaTicketBuyCount = 0;
			NKCGuildCoopManager.m_ArenaPlayableCount = 0;
			NKCGuildCoopManager.m_BossPlayableCount = 0;
			NKCGuildCoopManager.m_cGuildRaidTemplet = null;
			NKCGuildCoopManager.m_bGuildCoopMemberDataRecved = false;
		}

		// Token: 0x06003675 RID: 13941 RVA: 0x001187C7 File Offset: 0x001169C7
		public static void AddMyPoint(GuildDungeonRewardCategory category, int point)
		{
			if (category == GuildDungeonRewardCategory.RANK)
			{
				NKCGuildCoopManager.m_KillPoint += (long)point;
				return;
			}
			if (category == GuildDungeonRewardCategory.DUNGEON_TRY)
			{
				NKCGuildCoopManager.m_TryCount += point;
				return;
			}
		}

		// Token: 0x06003676 RID: 13942 RVA: 0x001187EB File Offset: 0x001169EB
		public static long GetMyPoint(GuildDungeonRewardCategory category)
		{
			if (category == GuildDungeonRewardCategory.RANK)
			{
				return NKCGuildCoopManager.m_KillPoint;
			}
			if (category == GuildDungeonRewardCategory.DUNGEON_TRY)
			{
				return (long)NKCGuildCoopManager.m_TryCount;
			}
			return 0L;
		}

		// Token: 0x06003677 RID: 13943 RVA: 0x00118804 File Offset: 0x00116A04
		public static int GetLastReceivedPoint(GuildDungeonRewardCategory category)
		{
			GuildDungeonSeasonRewardData guildDungeonSeasonRewardData = NKCGuildCoopManager.m_LastReceivedSeasonRewardData.Find((GuildDungeonSeasonRewardData x) => x.category == category);
			if (guildDungeonSeasonRewardData != null)
			{
				return guildDungeonSeasonRewardData.receivedValue;
			}
			return 0;
		}

		// Token: 0x06003678 RID: 13944 RVA: 0x00118840 File Offset: 0x00116A40
		public static bool CheckSeasonRewardEnable()
		{
			return NKCGuildCoopManager.CheckSeasonRewardEnable(GuildDungeonRewardCategory.DUNGEON_TRY) || NKCGuildCoopManager.CheckSeasonRewardEnable(GuildDungeonRewardCategory.RANK);
		}

		// Token: 0x06003679 RID: 13945 RVA: 0x00118854 File Offset: 0x00116A54
		public static bool CheckSeasonRewardEnable(GuildDungeonRewardCategory category)
		{
			GuildSeasonTemplet guildSeasonTemplet = GuildSeasonTemplet.Find(NKCGuildCoopManager.m_SeasonId);
			if (guildSeasonTemplet != null)
			{
				List<GuildSeasonRewardTemplet> list = GuildDungeonTempletManager.GetSeasonRewardList(guildSeasonTemplet.GetSeasonRewardGroup()).FindAll((GuildSeasonRewardTemplet x) => x.GetRewardCategory() == category);
				int num = 0;
				Predicate<GuildDungeonSeasonRewardData> <>9__1;
				while (num < list.Count && (long)list[num].GetRewardCountValue() <= NKCGuildCoopManager.GetMyPoint(category))
				{
					int rewardCountValue = list[num].GetRewardCountValue();
					List<GuildDungeonSeasonRewardData> lastReceivedSeasonRewardData = NKCGuildCoopManager.m_LastReceivedSeasonRewardData;
					Predicate<GuildDungeonSeasonRewardData> match;
					if ((match = <>9__1) == null)
					{
						match = (<>9__1 = ((GuildDungeonSeasonRewardData x) => x.category == category));
					}
					if (rewardCountValue > lastReceivedSeasonRewardData.Find(match).receivedValue)
					{
						return true;
					}
					num++;
				}
			}
			return false;
		}

		// Token: 0x0600367A RID: 13946 RVA: 0x00118908 File Offset: 0x00116B08
		public static int GetNextArtifactID(int arenaIdx)
		{
			if (!NKCGuildCoopManager.m_dicGuildDungeonInfoTemplet.ContainsKey(arenaIdx))
			{
				return 0;
			}
			GuildDungeonArtifactTemplet guildDungeonArtifactTemplet = GuildDungeonTempletManager.GetDungeonArtifactList(NKCGuildCoopManager.m_dicGuildDungeonInfoTemplet[arenaIdx].GetStageRewardArtifactGroup()).Find((GuildDungeonArtifactTemplet x) => x.GetOrder() == NKCGuildCoopManager.GetCurrentArtifactCountByArena(arenaIdx) + 1);
			if (guildDungeonArtifactTemplet != null)
			{
				return guildDungeonArtifactTemplet.GetArtifactId();
			}
			return 0;
		}

		// Token: 0x0600367B RID: 13947 RVA: 0x00118970 File Offset: 0x00116B70
		public static float GetClearPointPercentage(int arenaIdx)
		{
			GuildDungeonArena guildDungeonArena = NKCGuildCoopManager.m_lstGuildDungeonArena.Find((GuildDungeonArena x) => x.arenaIndex == arenaIdx);
			if (guildDungeonArena != null)
			{
				return (float)(guildDungeonArena.totalMedalCount % NKMCommonConst.GuildDungeonConstTemplet.ArtifactFulificationCount) / (float)NKMCommonConst.GuildDungeonConstTemplet.ArtifactFulificationCount;
			}
			return 0f;
		}

		// Token: 0x0600367C RID: 13948 RVA: 0x001189C8 File Offset: 0x00116BC8
		public static int GetTotalMedalCountByArena(int arenaIdx)
		{
			GuildDungeonArena guildDungeonArena = NKCGuildCoopManager.m_lstGuildDungeonArena.Find((GuildDungeonArena x) => x.arenaIndex == arenaIdx);
			if (guildDungeonArena != null)
			{
				return guildDungeonArena.totalMedalCount;
			}
			return 0;
		}

		// Token: 0x0600367D RID: 13949 RVA: 0x00118A04 File Offset: 0x00116C04
		public static int GetCurrentArtifactCountByArena(int arenaIdx)
		{
			GuildDungeonArena guildDungeonArena = NKCGuildCoopManager.m_lstGuildDungeonArena.Find((GuildDungeonArena x) => x.arenaIndex == arenaIdx);
			if (guildDungeonArena != null)
			{
				return guildDungeonArena.totalMedalCount / NKMCommonConst.GuildDungeonConstTemplet.ArtifactFulificationCount;
			}
			return 0;
		}

		// Token: 0x0600367E RID: 13950 RVA: 0x00118A4C File Offset: 0x00116C4C
		public static Dictionary<int, List<GuildDungeonArtifactTemplet>> GetAllArtifactDictionary()
		{
			Dictionary<int, List<GuildDungeonArtifactTemplet>> dictionary = new Dictionary<int, List<GuildDungeonArtifactTemplet>>();
			for (int i = 0; i < NKCGuildCoopManager.m_lstGuildDungeonArena.Count; i++)
			{
				int arenaIndex = NKCGuildCoopManager.m_lstGuildDungeonArena[i].arenaIndex;
				if (NKCGuildCoopManager.m_dicGuildDungeonInfoTemplet.ContainsKey(arenaIndex))
				{
					List<GuildDungeonArtifactTemplet> dungeonArtifactList = GuildDungeonTempletManager.GetDungeonArtifactList(NKCGuildCoopManager.m_dicGuildDungeonInfoTemplet[arenaIndex].GetStageRewardArtifactGroup());
					dictionary.Add(arenaIndex, dungeonArtifactList);
				}
			}
			return dictionary;
		}

		// Token: 0x0600367F RID: 13951 RVA: 0x00118AB4 File Offset: 0x00116CB4
		public static Dictionary<int, List<GuildDungeonArtifactTemplet>> GetMyArtifactDictionary()
		{
			Dictionary<int, List<GuildDungeonArtifactTemplet>> dictionary = new Dictionary<int, List<GuildDungeonArtifactTemplet>>();
			for (int i = 0; i < NKCGuildCoopManager.m_lstGuildDungeonArena.Count; i++)
			{
				int arenaIndex = NKCGuildCoopManager.m_lstGuildDungeonArena[i].arenaIndex;
				int currentArtifactCountByArena = NKCGuildCoopManager.GetCurrentArtifactCountByArena(arenaIndex);
				if (currentArtifactCountByArena > 0 && NKCGuildCoopManager.m_dicGuildDungeonInfoTemplet.ContainsKey(arenaIndex))
				{
					List<GuildDungeonArtifactTemplet> dungeonArtifactList = GuildDungeonTempletManager.GetDungeonArtifactList(NKCGuildCoopManager.m_dicGuildDungeonInfoTemplet[arenaIndex].GetStageRewardArtifactGroup());
					dictionary.Add(arenaIndex, dungeonArtifactList.GetRange(0, currentArtifactCountByArena));
				}
			}
			return dictionary;
		}

		// Token: 0x06003680 RID: 13952 RVA: 0x00118B30 File Offset: 0x00116D30
		public static NKM_ERROR_CODE CanStartBoss()
		{
			if (NKCGuildCoopManager.m_GuildDungeonState != GuildDungeonState.PlayableGuildDungeon)
			{
				return NKM_ERROR_CODE.NEC_FAIL_GUILD_DUNGEON_SESSION_OUT;
			}
			NKMUserData userData = NKCScenManager.CurrentUserData();
			if (NKCGuildCoopManager.m_lstGuildDungeonMemberInfo.Find((GuildDungeonMemberInfo x) => x.profile.userUid == userData.m_UserUID) == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_GUILD_DUNGEON_INVALID_SESSION_DUNGEON_ID;
			}
			if (NKCGuildCoopManager.m_BossPlayableCount == 0)
			{
				return NKM_ERROR_CODE.NEC_FAIL_GUILD_DUNGEON_BOSS_PLAYABLE;
			}
			if (NKCGuildCoopManager.m_BossPlayUserUid > 0L)
			{
				return NKM_ERROR_CODE.NEC_FAIL_GUILD_DUNGEON_ARENA_PLAYING;
			}
			if (NKCGuildCoopManager.m_BossRemainHp == 0f)
			{
				return NKM_ERROR_CODE.NEC_FAIL_GUILD_DUNGEON_BOSS_ALL_CLEAR;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06003681 RID: 13953 RVA: 0x00118BAC File Offset: 0x00116DAC
		public static NKM_ERROR_CODE CanStartArena(NKMDungeonTempletBase dungeonTempletBase)
		{
			foreach (KeyValuePair<int, GuildDungeonInfoTemplet> keyValuePair in NKCGuildCoopManager.m_dicGuildDungeonInfoTemplet)
			{
				if (keyValuePair.Value.GetSeasonDungeonId() == dungeonTempletBase.Key)
				{
					return NKCGuildCoopManager.CanStartArena(keyValuePair.Key);
				}
			}
			return NKM_ERROR_CODE.NEC_FAIL_GUILD_DUNGEON_INVALID_SESSION_DUNGEON_ID;
		}

		// Token: 0x06003682 RID: 13954 RVA: 0x00118C24 File Offset: 0x00116E24
		public static NKM_ERROR_CODE CanStartArena(int arenaIdx)
		{
			if (NKCGuildCoopManager.m_GuildDungeonState != GuildDungeonState.PlayableGuildDungeon)
			{
				return NKM_ERROR_CODE.NEC_FAIL_GUILD_DUNGEON_SESSION_OUT;
			}
			NKMUserData userData = NKCScenManager.CurrentUserData();
			if (NKCGuildCoopManager.m_lstGuildDungeonMemberInfo.Find((GuildDungeonMemberInfo x) => x.profile.userUid == userData.m_UserUID) == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_GUILD_DUNGEON_INVALID_SESSION_DUNGEON_ID;
			}
			if (NKCGuildCoopManager.m_ArenaPlayableCount <= 0 && NKCGuildCoopManager.m_ArenaTicketBuyCount >= NKMCommonConst.GuildDungeonConstTemplet.ArenaTicketBuyCount)
			{
				return NKM_ERROR_CODE.NEC_FAIL_GUILD_DUNGEON_ARENA_OVER_PLAY_COUNT;
			}
			GuildDungeonArena guildDungeonArena = NKCGuildCoopManager.m_lstGuildDungeonArena.Find((GuildDungeonArena x) => x.arenaIndex == arenaIdx);
			if (guildDungeonArena != null)
			{
				if (guildDungeonArena.playUserUid > 0L)
				{
					return NKM_ERROR_CODE.NEC_FAIL_GUILD_DUNGEON_ARENA_PLAYING;
				}
				List<GuildDungeonArtifactTemplet> list = NKCGuildCoopManager.GetAllArtifactDictionary()[arenaIdx];
				if (guildDungeonArena.totalMedalCount >= list.Count * NKMCommonConst.GuildDungeonConstTemplet.ArtifactFulificationCount)
				{
					return NKM_ERROR_CODE.NEC_FAIL_GUILD_DUNGEON_ARENA_MAX_ARTIFACT;
				}
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06003683 RID: 13955 RVA: 0x00118CF0 File Offset: 0x00116EF0
		public static void SetArenaPlayStart(int arenaIdx, long userUid)
		{
			if (!NKCGuildCoopManager.m_bGuildCoopDataRecved)
			{
				return;
			}
			GuildDungeonArena guildDungeonArena = NKCGuildCoopManager.m_lstGuildDungeonArena.Find((GuildDungeonArena x) => x.arenaIndex == arenaIdx);
			if (guildDungeonArena != null)
			{
				guildDungeonArena.playUserUid = userUid;
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_COOP().RefreshArenaSlot(arenaIdx);
			}
		}

		// Token: 0x06003684 RID: 13956 RVA: 0x00118D48 File Offset: 0x00116F48
		public static void SetArenaPlayEnd(NKMPacket_GUILD_DUNGEON_ARENA_PLAY_END_NOT sPacket)
		{
			if (!NKCGuildCoopManager.m_bGuildCoopDataRecved)
			{
				return;
			}
			GuildDungeonArena guildDungeonArena = NKCGuildCoopManager.m_lstGuildDungeonArena.Find((GuildDungeonArena x) => x.arenaIndex == sPacket.arenaId);
			if (guildDungeonArena != null)
			{
				if (sPacket.errorCode == NKM_ERROR_CODE.NEC_OK)
				{
					int grade = sPacket.totalGrade - guildDungeonArena.totalMedalCount;
					GuildDungeonMemberInfo guildDungeonMemberInfo = NKCGuildCoopManager.m_lstGuildDungeonMemberInfo.Find((GuildDungeonMemberInfo x) => x.profile.userUid == sPacket.playedUserUid);
					if (guildDungeonMemberInfo != null)
					{
						GuildDungeonMemberArena guildDungeonMemberArena = new GuildDungeonMemberArena();
						guildDungeonMemberArena.arenaId = sPacket.arenaId;
						guildDungeonMemberArena.grade = grade;
						guildDungeonMemberInfo.arenaList.Add(guildDungeonMemberArena);
						if (sPacket.playedUserUid == NKCScenManager.CurrentUserData().m_UserUID)
						{
							NKCGuildCoopManager.m_ArenaPlayableCount--;
							NKCGuildCoopManager.m_TryCount++;
						}
					}
					guildDungeonArena.totalMedalCount = sPacket.totalGrade;
				}
				guildDungeonArena.playUserUid = 0L;
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_COOP().RefreshArenaSlot(sPacket.arenaId);
			}
		}

		// Token: 0x06003685 RID: 13957 RVA: 0x00118E54 File Offset: 0x00117054
		public static void SetArenaPlayCancel(NKMPacket_GUILD_DUNGEON_ARENA_PLAY_CANCEL_NOT sPacket)
		{
			if (!NKCGuildCoopManager.m_bGuildCoopDataRecved)
			{
				return;
			}
			GuildDungeonArena guildDungeonArena = NKCGuildCoopManager.m_lstGuildDungeonArena.Find((GuildDungeonArena x) => x.arenaIndex == sPacket.arenaIndex);
			if (guildDungeonArena != null)
			{
				guildDungeonArena.playUserUid = 0L;
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_COOP().RefreshArenaSlot(sPacket.arenaIndex);
			}
		}

		// Token: 0x06003686 RID: 13958 RVA: 0x00118EB2 File Offset: 0x001170B2
		public static void SetBossPlayCancel(NKMPacket_GUILD_DUNGEON_BOSS_PLAY_CANCEL_NOT sPacket)
		{
			if (!NKCGuildCoopManager.m_bGuildCoopDataRecved)
			{
				return;
			}
			NKCGuildCoopManager.m_BossPlayUserUid = 0L;
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_COOP().RefreshBossInfo();
		}

		// Token: 0x06003687 RID: 13959 RVA: 0x00118ED2 File Offset: 0x001170D2
		public static void SetBossPlayStart(long userUid)
		{
			if (!NKCGuildCoopManager.m_bGuildCoopDataRecved)
			{
				return;
			}
			NKCGuildCoopManager.m_BossPlayUserUid = userUid;
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_COOP().RefreshBossInfo();
		}

		// Token: 0x06003688 RID: 13960 RVA: 0x00118EF4 File Offset: 0x001170F4
		public static void SetBossPlayEnd(NKMPacket_GUILD_DUNGEON_BOSS_PLAY_END_NOT sPacket)
		{
			if (!NKCGuildCoopManager.m_bGuildCoopDataRecved)
			{
				return;
			}
			NKCGuildCoopManager.m_cGuildRaidTemplet.GetStageIndex();
			NKCGuildCoopManager.m_cGuildRaidTemplet = GuildDungeonTempletManager.GetGuildRaidTemplet(NKCGuildCoopManager.m_cGuildRaidTemplet.GetSeasonRaidGrouop(), sPacket.bossStageId);
			if (NKCGuildCoopManager.m_cGuildRaidTemplet != null)
			{
				NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(sPacket.bossStageId);
				if (dungeonTempletBase != null)
				{
					GuildDungeonMemberInfo guildDungeonMemberInfo = NKCGuildCoopManager.m_lstGuildDungeonMemberInfo.Find((GuildDungeonMemberInfo x) => x.profile.userUid == sPacket.playedUserUid);
					if (guildDungeonMemberInfo != null)
					{
						guildDungeonMemberInfo.bossPoint += sPacket.point;
						if (sPacket.playedUserUid == NKCScenManager.CurrentUserData().m_UserUID)
						{
							NKCGuildCoopManager.m_TryCount++;
							NKCGuildCoopManager.m_BossPlayableCount--;
						}
					}
					NKCGuildCoopManager.m_BossPlayUserUid = 0L;
					NKCGuildCoopManager.m_BossRemainHp = sPacket.remainHp;
					NKCGuildCoopManager.m_BossMaxHp = NKMDungeonManager.GetBossHp(sPacket.bossStageId, dungeonTempletBase.m_DungeonLevel);
				}
				else
				{
					Log.Error(string.Format("NKMDungeonTempletBase is null - bossStageID : {0}", sPacket.bossStageId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCGuildCoopManager.cs", 462);
				}
			}
			else
			{
				Log.Error(string.Format("GuildRaidTemplet is null - GroupID : {0}, bossStageID : {1}", NKCGuildCoopManager.m_cGuildRaidTemplet.GetSeasonRaidGrouop(), sPacket.bossStageId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCGuildCoopManager.cs", 467);
			}
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_COOP().RefreshBossInfo();
		}

		// Token: 0x06003689 RID: 13961 RVA: 0x00119068 File Offset: 0x00117268
		public static void SetMyData(GuildDungeonRewardInfo myData)
		{
			NKCGuildCoopManager.Initialize();
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.GUILD_DUNGEON, 0, 0))
			{
				return;
			}
			NKCGuildCoopManager.m_SeasonId = myData.currentSeasonId;
			if (NKCGuildCoopManager.m_SeasonId < 0)
			{
				GuildSeasonTemplet firstSeasonTemplet = NKCGuildCoopManager.GetFirstSeasonTemplet();
				if (firstSeasonTemplet == null)
				{
					return;
				}
				NKCGuildCoopManager.m_SeasonId = firstSeasonTemplet.Key;
				NKCGuildCoopManager.m_NextSessionStartDateUTC = NKMTime.LocalToUTC(firstSeasonTemplet.GetSeasonStartDate(), 0);
			}
			NKCGuildCoopManager.m_KillPoint = (long)myData.lastSeasonRewardData.Find((GuildDungeonSeasonRewardData x) => x.category == GuildDungeonRewardCategory.RANK).totalValue;
			NKCGuildCoopManager.m_TryCount = myData.lastSeasonRewardData.Find((GuildDungeonSeasonRewardData x) => x.category == GuildDungeonRewardCategory.DUNGEON_TRY).totalValue;
			NKCGuildCoopManager.m_LastReceivedSeasonRewardData = myData.lastSeasonRewardData;
			NKCGuildCoopManager.m_bCanReward = myData.canReward;
		}

		// Token: 0x0600368A RID: 13962 RVA: 0x00119140 File Offset: 0x00117340
		public static GuildSeasonTemplet GetFirstSeasonTemplet()
		{
			if (NKMContentsVersionManager.HasDFChangeTagType(DataFormatChangeTagType.OPEN_TAG_GUILD_DUNGEON))
			{
				return (from e in GuildSeasonTemplet.Values
				where e.EnableByTag
				orderby e.GetSeasonStartDate()
				select e).FirstOrDefault<GuildSeasonTemplet>();
			}
			return (from e in GuildSeasonTemplet.Values
			orderby e.GetSeasonStartDate()
			select e).FirstOrDefault<GuildSeasonTemplet>();
		}

		// Token: 0x0600368B RID: 13963 RVA: 0x001191D8 File Offset: 0x001173D8
		public static bool HasNextSessionData(DateTime nextSessionStartTime)
		{
			if (NKCGuildCoopManager.m_bGuildCoopDataRecved && nextSessionStartTime <= DateTime.MinValue)
			{
				return false;
			}
			if (NKMContentsVersionManager.HasDFChangeTagType(DataFormatChangeTagType.OPEN_TAG_GUILD_DUNGEON))
			{
				return (from e in GuildSeasonTemplet.Values
				where e.EnableByTag && e.GetSeasonEndDate() > nextSessionStartTime
				orderby e.GetSeasonEndDate()
				select e).FirstOrDefault<GuildSeasonTemplet>() != null;
			}
			from e in GuildSeasonTemplet.Values
			where e.GetSeasonEndDate() > nextSessionStartTime
			select e;
			return (from e in GuildSeasonTemplet.Values
			orderby e.GetSeasonEndDate()
			select e).FirstOrDefault<GuildSeasonTemplet>() != null;
		}

		// Token: 0x0600368C RID: 13964 RVA: 0x001192A0 File Offset: 0x001174A0
		public static bool CheckFirstSeasonStarted()
		{
			return NKCGuildCoopManager.GetFirstSeasonTemplet() != null && NKCSynchronizedTime.IsFinished(NKMTime.LocalToUTC(NKCGuildCoopManager.GetFirstSeasonTemplet().GetSeasonStartDate(), 0));
		}

		// Token: 0x0600368D RID: 13965 RVA: 0x001192C5 File Offset: 0x001174C5
		public static void SetArenaTicketBuyCount(int count)
		{
			NKCGuildCoopManager.m_ArenaPlayableCount = NKCGuildCoopManager.m_ArenaPlayableCount - NKCGuildCoopManager.m_ArenaTicketBuyCount + count;
			NKCGuildCoopManager.m_ArenaTicketBuyCount = count;
		}

		// Token: 0x0600368E RID: 13966 RVA: 0x001192DF File Offset: 0x001174DF
		public static void SetArenaPlayableCount(int count)
		{
			if (count < 0)
			{
				count = 0;
			}
			NKCGuildCoopManager.m_ArenaPlayableCount = count;
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GUILD_COOP)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_COOP().Refresh();
			}
		}

		// Token: 0x0600368F RID: 13967 RVA: 0x0011930B File Offset: 0x0011750B
		public static void SetBossPlayableCount(int count)
		{
			NKCGuildCoopManager.m_BossPlayableCount = count;
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GUILD_COOP)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_COOP().Refresh();
			}
		}

		// Token: 0x06003690 RID: 13968 RVA: 0x00119330 File Offset: 0x00117530
		public static void OnRecv(NKMPacket_GUILD_DUNGEON_INFO_ACK sPacket)
		{
			if (sPacket.guildDungeonState == GuildDungeonState.Invalid)
			{
				NKCGuildCoopManager.m_GuildDungeonState = sPacket.guildDungeonState;
				NKCGuildCoopManager.m_NextSessionStartDateUTC = NKMTime.LocalToUTC(sPacket.NextSessionStartDate, 0);
				NKCGuildCoopManager.m_bGuildCoopDataRecved = true;
				return;
			}
			NKCGuildCoopManager.ResetGuildCoopSessionData();
			NKCGuildCoopManager.m_GuildDungeonState = sPacket.guildDungeonState;
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(sPacket.bossStageId);
			if (dungeonTempletBase == null)
			{
				Log.Error(string.Format("NKMDungeonTempletBase is null - id : {0}", sPacket.bossStageId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCGuildCoopManager.cs", 606);
				return;
			}
			NKCGuildCoopManager.m_SeasonId = sPacket.seasonId;
			NKCGuildCoopManager.m_SessionId = sPacket.sessionId;
			NKCGuildCoopManager.m_lstGuildDungeonArena = sPacket.arenaList;
			NKCGuildCoopManager.m_BossRemainHp = sPacket.bossRemainHp;
			NKCGuildCoopManager.m_BossMaxHp = NKMDungeonManager.GetBossHp(sPacket.bossStageId, dungeonTempletBase.m_DungeonLevel);
			NKCGuildCoopManager.m_BossPlayUserUid = sPacket.bossPlayUserUid;
			NKCGuildCoopManager.m_BossPlayableCount = sPacket.bossPlayCount;
			NKCGuildCoopManager.m_KillPoint = (long)sPacket.lastSeasonRewardData.Find((GuildDungeonSeasonRewardData e) => e.category == GuildDungeonRewardCategory.RANK).totalValue;
			NKCGuildCoopManager.m_TryCount = sPacket.lastSeasonRewardData.Find((GuildDungeonSeasonRewardData e) => e.category == GuildDungeonRewardCategory.DUNGEON_TRY).totalValue;
			NKCGuildCoopManager.m_SessionEndDateUTC = NKMTime.LocalToUTC(sPacket.currentSessionEndDate, 0);
			NKCGuildCoopManager.m_NextSessionStartDateUTC = NKMTime.LocalToUTC(sPacket.NextSessionStartDate, 0);
			NKCGuildCoopManager.m_LastReceivedSeasonRewardData = sPacket.lastSeasonRewardData;
			NKCGuildCoopManager.m_bCanReward = sPacket.canReward;
			NKCGuildCoopManager.m_ArenaTicketBuyCount = sPacket.arenaTicketBuyCount;
			NKCGuildCoopManager.m_dicGuildDungeonInfoTemplet.Clear();
			GuildSeasonTemplet guildSeasonTemplet = GuildDungeonTempletManager.GetGuildSeasonTemplet(NKCGuildCoopManager.m_SeasonId);
			if (guildSeasonTemplet != null)
			{
				GuildDungeonScheduleTemplet guildDungeonScheduleTemplet = GuildDungeonTempletManager.GetDungeonScheduleList(guildSeasonTemplet.GetSeasonDungeonGroup()).Find((GuildDungeonScheduleTemplet x) => x.GetSessionIndex() == NKCGuildCoopManager.m_SessionId);
				if (guildDungeonScheduleTemplet != null)
				{
					List<int> lstDungeonId = guildDungeonScheduleTemplet.GetDungeonList();
					int j;
					int i;
					Predicate<GuildDungeonInfoTemplet> <>9__3;
					for (i = 0; i < lstDungeonId.Count; i = j + 1)
					{
						List<GuildDungeonInfoTemplet> dungeonInfoList = GuildDungeonTempletManager.GetDungeonInfoList(NKCGuildCoopManager.m_SeasonId);
						Predicate<GuildDungeonInfoTemplet> match;
						if ((match = <>9__3) == null)
						{
							match = (<>9__3 = ((GuildDungeonInfoTemplet x) => x.GetSeasonDungeonId() == lstDungeonId[i]));
						}
						GuildDungeonInfoTemplet guildDungeonInfoTemplet = dungeonInfoList.Find(match);
						NKCGuildCoopManager.m_dicGuildDungeonInfoTemplet.Add(guildDungeonInfoTemplet.GetArenaIndex(), guildDungeonInfoTemplet);
						j = i;
					}
				}
				NKCGuildCoopManager.m_cGuildRaidTemplet = GuildDungeonTempletManager.GetGuildRaidTemplet(guildSeasonTemplet.GetSeasonRaidGroup(), sPacket.bossStageId);
			}
			NKCGuildCoopManager.m_bGuildCoopDataRecved = true;
		}

		// Token: 0x06003691 RID: 13969 RVA: 0x001195A4 File Offset: 0x001177A4
		public static void OnRecv(NKMPacket_GUILD_DUNGEON_MEMBER_INFO_ACK sPacket)
		{
			if (sPacket.memberInfoList != null)
			{
				NKCGuildCoopManager.m_lstGuildDungeonMemberInfo = sPacket.memberInfoList;
			}
			GuildDungeonMemberInfo guildDungeonMemberInfo = NKCGuildCoopManager.m_lstGuildDungeonMemberInfo.Find((GuildDungeonMemberInfo x) => x.profile.userUid == NKCScenManager.CurrentUserData().m_UserUID);
			int arenaPlayableCount = 0;
			if (guildDungeonMemberInfo != null)
			{
				arenaPlayableCount = Math.Max(0, NKMCommonConst.GuildDungeonConstTemplet.ArenaPlayCountBasic - guildDungeonMemberInfo.arenaList.Count + NKCGuildCoopManager.m_ArenaTicketBuyCount);
			}
			NKCGuildCoopManager.SetArenaPlayableCount(arenaPlayableCount);
			NKCGuildCoopManager.m_bGuildCoopMemberDataRecved = true;
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_RAID_READY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_RAID_READY().OnRecv(sPacket);
			}
		}

		// Token: 0x06003692 RID: 13970 RVA: 0x00119640 File Offset: 0x00117840
		public static void OnRecv(NKMPacket_GUILD_DUNGEON_SEASON_REWARD_ACK sPacket)
		{
			GuildDungeonSeasonRewardData guildDungeonSeasonRewardData = NKCGuildCoopManager.m_LastReceivedSeasonRewardData.Find((GuildDungeonSeasonRewardData x) => x.category == sPacket.rewardCategory);
			if (guildDungeonSeasonRewardData != null)
			{
				guildDungeonSeasonRewardData.receivedValue = sPacket.rewardCountValue;
				return;
			}
			Log.Error(string.Format("m_LastReceivedSeasonRewardData  - {0} 에 해당하는 값이 없음", sPacket.rewardCategory), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCGuildCoopManager.cs", 677);
		}

		// Token: 0x06003693 RID: 13971 RVA: 0x001196B0 File Offset: 0x001178B0
		public static int CompMember(GuildDungeonMemberInfo left, GuildDungeonMemberInfo right)
		{
			if (left.bossPoint != right.bossPoint)
			{
				return right.bossPoint.CompareTo(left.bossPoint);
			}
			NKMGuildMemberData nkmguildMemberData = NKCGuildManager.MyGuildData.members.Find((NKMGuildMemberData x) => x.commonProfile.userUid == left.profile.userUid);
			NKMGuildMemberData nkmguildMemberData2 = NKCGuildManager.MyGuildData.members.Find((NKMGuildMemberData x) => x.commonProfile.userUid == right.profile.userUid);
			if (nkmguildMemberData.grade == nkmguildMemberData2.grade)
			{
				return nkmguildMemberData.commonProfile.nickname.CompareTo(nkmguildMemberData2.commonProfile.nickname);
			}
			return nkmguildMemberData.grade.CompareTo(nkmguildMemberData2.grade);
		}

		// Token: 0x06003694 RID: 13972 RVA: 0x00119783 File Offset: 0x00117983
		public static void SetRewarded()
		{
			NKCGuildCoopManager.m_bCanReward = false;
		}

		// Token: 0x040033CE RID: 13262
		public static List<GuildDungeonArena> m_lstGuildDungeonArena = new List<GuildDungeonArena>();

		// Token: 0x040033D1 RID: 13265
		public static List<NKMRewardData> m_lstKillPointReward = new List<NKMRewardData>();

		// Token: 0x040033D2 RID: 13266
		public static List<NKMRewardData> m_lstTryCountReward = new List<NKMRewardData>();

		// Token: 0x040033D3 RID: 13267
		public static List<GuildDungeonSeasonRewardData> m_LastReceivedSeasonRewardData = new List<GuildDungeonSeasonRewardData>();

		// Token: 0x040033D5 RID: 13269
		public static Dictionary<int, GuildDungeonInfoTemplet> m_dicGuildDungeonInfoTemplet = new Dictionary<int, GuildDungeonInfoTemplet>();

		// Token: 0x040033D6 RID: 13270
		private static List<GuildDungeonMemberInfo> m_lstGuildDungeonMemberInfo = new List<GuildDungeonMemberInfo>();

		// Token: 0x040033D7 RID: 13271
		public static int m_ArenaTicketBuyCount = 0;
	}
}
