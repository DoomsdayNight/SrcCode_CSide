using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.LeaderBoard;
using ClientPacket.Raid;
using NKM.Templet;

namespace NKC
{
	// Token: 0x02000690 RID: 1680
	public class LeaderBoardSlotData
	{
		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x060036D8 RID: 14040 RVA: 0x0011A5A0 File Offset: 0x001187A0
		public long userUid
		{
			get
			{
				if (this.Profile != null)
				{
					return this.Profile.userUid;
				}
				return 0L;
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x060036D9 RID: 14041 RVA: 0x0011A5B8 File Offset: 0x001187B8
		public long friendCode
		{
			get
			{
				if (this.Profile != null)
				{
					return this.Profile.friendCode;
				}
				return 0L;
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x060036DA RID: 14042 RVA: 0x0011A5D0 File Offset: 0x001187D0
		public string nickname
		{
			get
			{
				if (this.Profile != null)
				{
					return this.Profile.nickname;
				}
				return "";
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x060036DB RID: 14043 RVA: 0x0011A5EB File Offset: 0x001187EB
		public int level
		{
			get
			{
				if (this.Profile != null)
				{
					return this.Profile.level;
				}
				return 1;
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x060036DC RID: 14044 RVA: 0x0011A602 File Offset: 0x00118802
		public int mainUnitId
		{
			get
			{
				if (this.Profile != null)
				{
					return this.Profile.mainUnitId;
				}
				return 0;
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x060036DD RID: 14045 RVA: 0x0011A619 File Offset: 0x00118819
		public int mainUnitSkinId
		{
			get
			{
				if (this.Profile != null)
				{
					return this.Profile.mainUnitSkinId;
				}
				return 0;
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x060036DE RID: 14046 RVA: 0x0011A630 File Offset: 0x00118830
		public int frameID
		{
			get
			{
				if (this.Profile != null)
				{
					return this.Profile.frameId;
				}
				return 0;
			}
		}

		// Token: 0x060036DF RID: 14047 RVA: 0x0011A648 File Offset: 0x00118848
		public static LeaderBoardSlotData MakeMySlotData(LeaderBoardType type, int rank, string score, bool bIsGuild)
		{
			LeaderBoardSlotData leaderBoardSlotData = new LeaderBoardSlotData();
			leaderBoardSlotData.bIsGuild = bIsGuild;
			leaderBoardSlotData.boardType = type;
			leaderBoardSlotData.Profile = new NKMCommonProfile();
			if (NKCScenManager.CurrentUserData().UserProfileData != null)
			{
				leaderBoardSlotData.Profile = NKCScenManager.CurrentUserData().UserProfileData.commonProfile;
			}
			else
			{
				leaderBoardSlotData.Profile.level = NKCScenManager.CurrentUserData().m_UserLevel;
				leaderBoardSlotData.Profile.nickname = NKCScenManager.CurrentUserData().m_UserNickName;
				leaderBoardSlotData.Profile.userUid = NKCScenManager.CurrentUserData().m_UserUID;
				leaderBoardSlotData.Profile.friendCode = NKCScenManager.CurrentUserData().m_FriendCode;
			}
			leaderBoardSlotData.GuildData = NKCGuildManager.GetMyGuildSimpleData();
			leaderBoardSlotData.score = score;
			leaderBoardSlotData.rank = rank;
			return leaderBoardSlotData;
		}

		// Token: 0x060036E0 RID: 14048 RVA: 0x0011A705 File Offset: 0x00118905
		public static LeaderBoardSlotData MakeSlotData(LeaderBoardType type, bool bIsGuild, int rank)
		{
			return new LeaderBoardSlotData
			{
				bIsGuild = bIsGuild,
				boardType = type,
				Profile = new NKMCommonProfile(),
				GuildData = new NKMGuildSimpleData(),
				score = LeaderBoardSlotData.GetScoreByBoardType(type, 0L),
				rank = rank
			};
		}

		// Token: 0x060036E1 RID: 14049 RVA: 0x0011A748 File Offset: 0x00118948
		public static LeaderBoardSlotData MakeSlotData(NKMAchieveData achieveData, int rank)
		{
			return new LeaderBoardSlotData
			{
				bIsGuild = false,
				boardType = LeaderBoardType.BT_ACHIEVE,
				Profile = achieveData.commonProfile,
				GuildData = achieveData.guildData,
				score = LeaderBoardSlotData.GetScoreByBoardType(LeaderBoardType.BT_ACHIEVE, achieveData.achievePoint),
				rank = rank
			};
		}

		// Token: 0x060036E2 RID: 14050 RVA: 0x0011A79C File Offset: 0x0011899C
		public static LeaderBoardSlotData MakeSlotData(NKMShadowPalaceData shadowPalaceData, int rank)
		{
			return new LeaderBoardSlotData
			{
				bIsGuild = false,
				boardType = LeaderBoardType.BT_SHADOW,
				Profile = shadowPalaceData.commonProfile,
				GuildData = shadowPalaceData.guildData,
				score = LeaderBoardSlotData.GetScoreByBoardType(LeaderBoardType.BT_SHADOW, (long)shadowPalaceData.bestTime),
				rank = rank
			};
		}

		// Token: 0x060036E3 RID: 14051 RVA: 0x0011A7F0 File Offset: 0x001189F0
		public static LeaderBoardSlotData MakeSlotData(NKMFierceData cNKMFierceData, int rank)
		{
			return new LeaderBoardSlotData
			{
				bIsGuild = false,
				boardType = LeaderBoardType.BT_FIERCE,
				Profile = cNKMFierceData.commonProfile,
				GuildData = cNKMFierceData.guildData,
				score = LeaderBoardSlotData.GetScoreByBoardType(LeaderBoardType.BT_FIERCE, cNKMFierceData.fiercePoint),
				rank = rank
			};
		}

		// Token: 0x060036E4 RID: 14052 RVA: 0x0011A844 File Offset: 0x00118A44
		public static LeaderBoardSlotData MakeSlotData(NKMTimeAttackData timeAttackData, int rank)
		{
			return new LeaderBoardSlotData
			{
				bIsGuild = false,
				boardType = LeaderBoardType.BT_TIMEATTACK,
				Profile = timeAttackData.commonProfile,
				GuildData = timeAttackData.guildData,
				score = LeaderBoardSlotData.GetScoreByBoardType(LeaderBoardType.BT_TIMEATTACK, (long)timeAttackData.bestTime),
				rank = rank
			};
		}

		// Token: 0x060036E5 RID: 14053 RVA: 0x0011A898 File Offset: 0x00118A98
		public static List<LeaderBoardSlotData> MakeSlotDataList(NKMLeaderBoardFierceData cNKMLeaderBoardFierceData)
		{
			List<LeaderBoardSlotData> list = new List<LeaderBoardSlotData>();
			for (int i = 0; i < cNKMLeaderBoardFierceData.fierceData.Count; i++)
			{
				LeaderBoardSlotData item = LeaderBoardSlotData.MakeSlotData(cNKMLeaderBoardFierceData.fierceData[i], i + 1);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x0011A8E0 File Offset: 0x00118AE0
		public static LeaderBoardSlotData MakeSlotData(NKMGuildRankData rankData, int rank)
		{
			return new LeaderBoardSlotData
			{
				bIsGuild = true,
				boardType = LeaderBoardType.BT_GUILD,
				GuildData = new NKMGuildSimpleData(),
				GuildData = 
				{
					badgeId = rankData.badgeId,
					guildName = rankData.guildName,
					guildUid = rankData.guildUid
				},
				Profile = new NKMCommonProfile(),
				Profile = 
				{
					level = rankData.guildLevel,
					nickname = rankData.masterNickname
				},
				score = LeaderBoardSlotData.GetScoreByBoardType(LeaderBoardType.BT_GUILD, rankData.rankValue),
				memberCount = rankData.memberCount,
				rank = rank
			};
		}

		// Token: 0x060036E7 RID: 14055 RVA: 0x0011A990 File Offset: 0x00118B90
		public static List<LeaderBoardSlotData> MakeSlotDataList(NKMLeaderBoardGuildData cNKMLeaderBoardGuildData)
		{
			List<LeaderBoardSlotData> list = new List<LeaderBoardSlotData>();
			for (int i = 0; i < cNKMLeaderBoardGuildData.rankDatas.Count; i++)
			{
				LeaderBoardSlotData item = LeaderBoardSlotData.MakeSlotData(cNKMLeaderBoardGuildData.rankDatas[i], i + 1);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x060036E8 RID: 14056 RVA: 0x0011A9D8 File Offset: 0x00118BD8
		public static LeaderBoardSlotData MakeSlotData(LeaderBoardType boardType, NKMUserProfileData cNKMUserProfileData, int rank)
		{
			return new LeaderBoardSlotData
			{
				bIsGuild = false,
				boardType = boardType,
				GuildData = cNKMUserProfileData.guildData,
				Profile = new NKMCommonProfile(),
				Profile = 
				{
					level = cNKMUserProfileData.commonProfile.level,
					nickname = cNKMUserProfileData.commonProfile.nickname
				},
				score = LeaderBoardSlotData.GetScoreByBoardType(boardType, (long)cNKMUserProfileData.leaguePvpData.score),
				memberCount = 0,
				rank = rank
			};
		}

		// Token: 0x060036E9 RID: 14057 RVA: 0x0011AA64 File Offset: 0x00118C64
		public static List<LeaderBoardSlotData> MakeSlotDataList(LeaderBoardType boardType, List<NKMUserProfileData> lstData)
		{
			List<LeaderBoardSlotData> list = new List<LeaderBoardSlotData>();
			for (int i = 0; i < lstData.Count; i++)
			{
				LeaderBoardSlotData item = LeaderBoardSlotData.MakeSlotData(boardType, lstData[i], i + 1);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x060036EA RID: 14058 RVA: 0x0011AAA4 File Offset: 0x00118CA4
		public static LeaderBoardSlotData MakeSlotData(NKMRaidJoinData raidJoinData, int rank, int raidMaxCount)
		{
			return new LeaderBoardSlotData
			{
				bIsGuild = false,
				boardType = LeaderBoardType.BT_ACHIEVE,
				GuildData = raidJoinData.guildData,
				Profile = new NKMCommonProfile(),
				Profile = 
				{
					userUid = raidJoinData.userUID,
					level = raidJoinData.level,
					nickname = raidJoinData.nickName,
					mainUnitId = raidJoinData.mainUnitID,
					mainUnitSkinId = raidJoinData.mainUnitSkinID,
					friendCode = raidJoinData.friendCode
				},
				score = raidJoinData.damage.ToString("N0"),
				memberCount = (int)raidJoinData.tryCount,
				raidTryCount = (int)raidJoinData.tryCount,
				raidTryMaxCount = raidMaxCount,
				rank = rank
			};
		}

		// Token: 0x060036EB RID: 14059 RVA: 0x0011AB80 File Offset: 0x00118D80
		public static List<LeaderBoardSlotData> MakeSlotDataList(List<NKMRaidJoinData> lstData, int maxCount)
		{
			List<LeaderBoardSlotData> list = new List<LeaderBoardSlotData>();
			lstData.Sort(new Comparison<NKMRaidJoinData>(LeaderBoardSlotData.CompByDamage));
			for (int i = 0; i < lstData.Count; i++)
			{
				LeaderBoardSlotData item = LeaderBoardSlotData.MakeSlotData(lstData[i], i + 1, maxCount);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x060036EC RID: 14060 RVA: 0x0011ABCF File Offset: 0x00118DCF
		private static int CompByDamage(NKMRaidJoinData lData, NKMRaidJoinData rData)
		{
			return rData.damage.CompareTo(lData.damage);
		}

		// Token: 0x060036ED RID: 14061 RVA: 0x0011ABE4 File Offset: 0x00118DE4
		public static string GetScoreByBoardType(LeaderBoardType type, long score)
		{
			switch (type)
			{
			default:
				if (score > 0L)
				{
					return score.ToString("N0");
				}
				return "-";
			case LeaderBoardType.BT_SHADOW:
			case LeaderBoardType.BT_TIMEATTACK:
				if (score > 0L)
				{
					return NKCUtilString.GetTimeSpanString(TimeSpan.FromSeconds((double)score));
				}
				return "-:--:--";
			}
		}

		// Token: 0x04003400 RID: 13312
		public LeaderBoardType boardType = LeaderBoardType.BT_NONE;

		// Token: 0x04003401 RID: 13313
		public NKMCommonProfile Profile;

		// Token: 0x04003402 RID: 13314
		public NKMGuildSimpleData GuildData;

		// Token: 0x04003403 RID: 13315
		public string score = "";

		// Token: 0x04003404 RID: 13316
		public int memberCount;

		// Token: 0x04003405 RID: 13317
		public bool bIsGuild;

		// Token: 0x04003406 RID: 13318
		public int rank;

		// Token: 0x04003407 RID: 13319
		public int raidTryCount;

		// Token: 0x04003408 RID: 13320
		public int raidTryMaxCount;
	}
}
