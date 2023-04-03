using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cs.Core.Util;
using Cs.Logging;
using NKC;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x02000366 RID: 870
	public class NKCPVPManager
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x0004E4AE File Offset: 0x0004C6AE
		public static int WEEK_CALC_END_TIME
		{
			get
			{
				if (4 >= NKMTime.INTERVAL_FROM_UTC)
				{
					return 4 - NKMTime.INTERVAL_FROM_UTC;
				}
				return 28 - NKMTime.INTERVAL_FROM_UTC;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x0004E4C8 File Offset: 0x0004C6C8
		public static int WEEK_CALC_START_TIME
		{
			get
			{
				if (22 >= NKMTime.INTERVAL_FROM_UTC)
				{
					return 22 - NKMTime.INTERVAL_FROM_UTC;
				}
				return 46 - NKMTime.INTERVAL_FROM_UTC;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x0004E4E4 File Offset: 0x0004C6E4
		// (set) Token: 0x060014D6 RID: 5334 RVA: 0x0004E4EB File Offset: 0x0004C6EB
		public static DateTime WeekCalcStartDateUtc { get; set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x0004E4F3 File Offset: 0x0004C6F3
		// (set) Token: 0x060014D8 RID: 5336 RVA: 0x0004E4FA File Offset: 0x0004C6FA
		public static DateTime WeekCalcEndDateUtc { get; set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x0004E502 File Offset: 0x0004C702
		// (set) Token: 0x060014DA RID: 5338 RVA: 0x0004E509 File Offset: 0x0004C709
		private static int WeekID_Rank { get; set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060014DB RID: 5339 RVA: 0x0004E511 File Offset: 0x0004C711
		// (set) Token: 0x060014DC RID: 5340 RVA: 0x0004E518 File Offset: 0x0004C718
		private static int WeekID_Async { get; set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060014DD RID: 5341 RVA: 0x0004E520 File Offset: 0x0004C720
		// (set) Token: 0x060014DE RID: 5342 RVA: 0x0004E527 File Offset: 0x0004C727
		private static int WeekID_League { get; set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060014DF RID: 5343 RVA: 0x0004E52F File Offset: 0x0004C72F
		// (set) Token: 0x060014E0 RID: 5344 RVA: 0x0004E536 File Offset: 0x0004C736
		private static DateTime RankNextWeekIDResetDate { get; set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060014E1 RID: 5345 RVA: 0x0004E53E File Offset: 0x0004C73E
		// (set) Token: 0x060014E2 RID: 5346 RVA: 0x0004E545 File Offset: 0x0004C745
		private static DateTime AsyncNextWeekIDResetDate { get; set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060014E3 RID: 5347 RVA: 0x0004E54D File Offset: 0x0004C74D
		// (set) Token: 0x060014E4 RID: 5348 RVA: 0x0004E554 File Offset: 0x0004C754
		private static DateTime LeagueNextWeekIDResetDate { get; set; }

		// Token: 0x060014E5 RID: 5349 RVA: 0x0004E55C File Offset: 0x0004C75C
		public static string GetLeagueTop3Key()
		{
			return string.Format(string.Format("{0}_LEAGUE_TOP3_UPDATE_TICK", NKCScenManager.CurrentUserData().m_UserUID), Array.Empty<object>());
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x0004E584 File Offset: 0x0004C784
		public static bool LoadFromLua()
		{
			bool flag = true;
			NKMLua nkmlua = new NKMLua();
			if (nkmlua.LoadCommonPath("AB_SCRIPT", "LUA_PVP_RANK", true) && nkmlua.OpenTable("PVP_RANK"))
			{
				int num = 1;
				while (nkmlua.OpenTable(num))
				{
					NKMPvpRankTemplet nkmpvpRankTemplet = new NKMPvpRankTemplet();
					flag &= nkmpvpRankTemplet.LoadFromLUA(nkmlua);
					Dictionary<int, NKMPvpRankTemplet> dictionary;
					if (!NKCPVPManager.dicPvpRankTemplet.TryGetValue(nkmpvpRankTemplet.RankGroup, out dictionary))
					{
						dictionary = new Dictionary<int, NKMPvpRankTemplet>();
						NKCPVPManager.dicPvpRankTemplet.Add(nkmpvpRankTemplet.RankGroup, dictionary);
					}
					dictionary[nkmpvpRankTemplet.LeagueTier] = nkmpvpRankTemplet;
					num++;
					nkmlua.CloseTable();
				}
				nkmlua.CloseTable();
			}
			nkmlua.LuaClose();
			NKCPVPManager.dicPvpRankSeasonTemplet = NKMTempletLoader.LoadDictionary<NKMPvpRankSeasonTemplet>("AB_SCRIPT", "LUA_PVP_RANK_SEASON", "PVP_RANK_SEASON", new Func<NKMLua, NKMPvpRankSeasonTemplet>(NKMPvpRankSeasonTemplet.LoadFromLUA));
			if (NKCPVPManager.dicPvpRankSeasonTemplet == null)
			{
				Log.ErrorAndExit("PvpRankSeason load failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCPVPManager.cs", 73);
				return false;
			}
			NKCPVPManager.dicAsyncPvpSeasonTemplet = NKMTempletLoader.LoadDictionary<NKMPvpRankSeasonTemplet>("AB_SCRIPT", "LUA_PVP_ASYNC_SEASON", "PVP_ASYNC_SEASON", new Func<NKMLua, NKMPvpRankSeasonTemplet>(NKMPvpRankSeasonTemplet.LoadFromLUA));
			if (NKCPVPManager.dicAsyncPvpSeasonTemplet == null)
			{
				Log.ErrorAndExit("PvpAsyncSeason load failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCPVPManager.cs", 82);
				return false;
			}
			NKMPvpRankSeasonTemplet nkmpvpRankSeasonTemplet = null;
			foreach (NKMPvpRankSeasonTemplet nkmpvpRankSeasonTemplet2 in NKCPVPManager.dicAsyncPvpSeasonTemplet.Values)
			{
				nkmpvpRankSeasonTemplet2.Validate();
				if (nkmpvpRankSeasonTemplet == null)
				{
					nkmpvpRankSeasonTemplet = nkmpvpRankSeasonTemplet2;
				}
				else
				{
					if (nkmpvpRankSeasonTemplet.SeasonID != nkmpvpRankSeasonTemplet2.SeasonID - 1)
					{
						Log.ErrorAndExit(string.Format("Invalid Async SeasonId. Prev:{0}, Cur:{1}", nkmpvpRankSeasonTemplet.SeasonID, nkmpvpRankSeasonTemplet2.SeasonID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCPVPManager.cs", 100);
						return false;
					}
					nkmpvpRankSeasonTemplet = nkmpvpRankSeasonTemplet2;
				}
			}
			NKMTempletContainer<NKMLeaguePvpRankSeasonTemplet>.Load("AB_SCRIPT", "LUA_PVP_LEAGUE_SEASON", "PVP_LEAGUE_SEASON", new Func<NKMLua, NKMLeaguePvpRankSeasonTemplet>(NKMLeaguePvpRankSeasonTemplet.LoadFromLUA));
			NKMLeaguePvpRankGroupTemplet.LoadFile();
			NKMPvpRankTemplet nkmpvpRankTemplet2 = null;
			List<NKMPvpRankTemplet> list = null;
			foreach (KeyValuePair<int, Dictionary<int, NKMPvpRankTemplet>> keyValuePair in NKCPVPManager.dicPvpRankTemplet)
			{
				foreach (KeyValuePair<int, NKMPvpRankTemplet> keyValuePair2 in keyValuePair.Value)
				{
					if (nkmpvpRankTemplet2 == null)
					{
						nkmpvpRankTemplet2 = keyValuePair2.Value;
					}
					else
					{
						int num2 = (keyValuePair2.Value.LeaguePointReq - nkmpvpRankTemplet2.LeaguePointReq) / NKMPvpCommonConst.Instance.SCORE_MIN_INTERVAL_UNIT;
						for (int i = 0; i < num2; i++)
						{
							if (!NKCPVPManager.listPvpRankTemplet.TryGetValue(keyValuePair.Key, out list))
							{
								list = new List<NKMPvpRankTemplet>();
								NKCPVPManager.listPvpRankTemplet.Add(keyValuePair.Key, list);
							}
							list.Add(nkmpvpRankTemplet2);
						}
						nkmpvpRankTemplet2 = keyValuePair2.Value;
					}
				}
				if (list != null)
				{
					list.Add(nkmpvpRankTemplet2);
				}
			}
			return flag;
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x0004E890 File Offset: 0x0004CA90
		public static void Join()
		{
			foreach (NKMPvpRankSeasonTemplet nkmpvpRankSeasonTemplet in NKCPVPManager.dicPvpRankSeasonTemplet.Values)
			{
				nkmpvpRankSeasonTemplet.Join();
			}
			foreach (NKMPvpRankSeasonTemplet nkmpvpRankSeasonTemplet2 in NKCPVPManager.dicAsyncPvpSeasonTemplet.Values)
			{
				nkmpvpRankSeasonTemplet2.Join();
			}
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x0004E928 File Offset: 0x0004CB28
		public static void PostJoin()
		{
			foreach (NKMPvpRankSeasonTemplet nkmpvpRankSeasonTemplet in NKCPVPManager.dicPvpRankSeasonTemplet.Values)
			{
				nkmpvpRankSeasonTemplet.PostJoin();
			}
			foreach (NKMPvpRankSeasonTemplet nkmpvpRankSeasonTemplet2 in NKCPVPManager.dicAsyncPvpSeasonTemplet.Values)
			{
				nkmpvpRankSeasonTemplet2.PostJoin();
			}
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x0004E9C0 File Offset: 0x0004CBC0
		public static int GetFinalTier(int tier)
		{
			if (tier == 0)
			{
				tier = 1;
			}
			return tier;
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x0004E9CC File Offset: 0x0004CBCC
		public static LEAGUE_TIER_ICON GetTierIconByScore(NKM_GAME_TYPE gameType, int seasonID, int score)
		{
			if (gameType <= NKM_GAME_TYPE.NGT_ASYNC_PVP)
			{
				if (gameType != NKM_GAME_TYPE.NGT_PVP_RANK && gameType != NKM_GAME_TYPE.NGT_ASYNC_PVP)
				{
					return LEAGUE_TIER_ICON.LTI_NONE;
				}
			}
			else if (gameType != NKM_GAME_TYPE.NGT_PVP_LEAGUE)
			{
				if (gameType - NKM_GAME_TYPE.NGT_PVP_STRATEGY > 2)
				{
					return LEAGUE_TIER_ICON.LTI_NONE;
				}
			}
			else
			{
				NKMLeaguePvpRankSeasonTemplet nkmleaguePvpRankSeasonTemplet = NKMLeaguePvpRankSeasonTemplet.Find(ServiceTime.Recent);
				if (nkmleaguePvpRankSeasonTemplet == null)
				{
					return LEAGUE_TIER_ICON.LTI_NONE;
				}
				NKMLeaguePvpRankTemplet byScore = nkmleaguePvpRankSeasonTemplet.RankGroup.GetByScore(score);
				if (byScore != null)
				{
					return byScore.LeagueTierIcon;
				}
				return LEAGUE_TIER_ICON.LTI_NONE;
			}
			NKMPvpRankTemplet rankTempletByScore = NKCPVPManager.GetRankTempletByScore(gameType, seasonID, score);
			if (rankTempletByScore != null)
			{
				return rankTempletByScore.LeagueTierIcon;
			}
			return LEAGUE_TIER_ICON.LTI_NONE;
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x0004EA30 File Offset: 0x0004CC30
		public static LEAGUE_TIER_ICON GetTierIconByTier(NKM_GAME_TYPE gameType, int seasonID, int leagueTier)
		{
			if (gameType <= NKM_GAME_TYPE.NGT_ASYNC_PVP)
			{
				if (gameType != NKM_GAME_TYPE.NGT_PVP_RANK && gameType != NKM_GAME_TYPE.NGT_ASYNC_PVP)
				{
					return LEAGUE_TIER_ICON.LTI_NONE;
				}
			}
			else if (gameType != NKM_GAME_TYPE.NGT_PVP_LEAGUE)
			{
				if (gameType - NKM_GAME_TYPE.NGT_PVP_STRATEGY > 2)
				{
					return LEAGUE_TIER_ICON.LTI_NONE;
				}
			}
			else
			{
				NKMLeaguePvpRankTemplet nkmleaguePvpRankTemplet;
				if (NKMLeaguePvpRankTemplet.FindByTier(seasonID, leagueTier, out nkmleaguePvpRankTemplet))
				{
					return nkmleaguePvpRankTemplet.LeagueTierIcon;
				}
				return LEAGUE_TIER_ICON.LTI_NONE;
			}
			NKMPvpRankTemplet rankTempletByTier = NKCPVPManager.GetRankTempletByTier(gameType, seasonID, leagueTier);
			if (rankTempletByTier != null)
			{
				return rankTempletByTier.LeagueTierIcon;
			}
			return LEAGUE_TIER_ICON.LTI_NONE;
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x0004EA84 File Offset: 0x0004CC84
		public static int GetTierNumberByScore(NKM_GAME_TYPE gameType, int seasonID, int score)
		{
			if (gameType <= NKM_GAME_TYPE.NGT_ASYNC_PVP)
			{
				if (gameType != NKM_GAME_TYPE.NGT_PVP_RANK && gameType != NKM_GAME_TYPE.NGT_ASYNC_PVP)
				{
					return 0;
				}
			}
			else if (gameType != NKM_GAME_TYPE.NGT_PVP_LEAGUE)
			{
				if (gameType - NKM_GAME_TYPE.NGT_PVP_STRATEGY > 2)
				{
					return 0;
				}
			}
			else
			{
				NKMLeaguePvpRankSeasonTemplet nkmleaguePvpRankSeasonTemplet = NKMLeaguePvpRankSeasonTemplet.Find(ServiceTime.Recent);
				if (nkmleaguePvpRankSeasonTemplet == null)
				{
					return 0;
				}
				NKMLeaguePvpRankTemplet byScore = nkmleaguePvpRankSeasonTemplet.RankGroup.GetByScore(score);
				if (byScore != null)
				{
					return byScore.LeagueTierIconNumber;
				}
				return 0;
			}
			NKMPvpRankTemplet rankTempletByScore = NKCPVPManager.GetRankTempletByScore(gameType, seasonID, score);
			if (rankTempletByScore != null)
			{
				return rankTempletByScore.LeagueTierIconNumber;
			}
			return 0;
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x0004EAE8 File Offset: 0x0004CCE8
		public static int GetTierNumberByTier(NKM_GAME_TYPE gameType, int seasonID, int leagueTier)
		{
			if (gameType <= NKM_GAME_TYPE.NGT_ASYNC_PVP)
			{
				if (gameType != NKM_GAME_TYPE.NGT_PVP_RANK && gameType != NKM_GAME_TYPE.NGT_ASYNC_PVP)
				{
					return 0;
				}
			}
			else if (gameType != NKM_GAME_TYPE.NGT_PVP_LEAGUE)
			{
				if (gameType - NKM_GAME_TYPE.NGT_PVP_STRATEGY > 2)
				{
					return 0;
				}
			}
			else
			{
				NKMLeaguePvpRankTemplet nkmleaguePvpRankTemplet;
				if (NKMLeaguePvpRankTemplet.FindByTier(seasonID, leagueTier, out nkmleaguePvpRankTemplet))
				{
					return nkmleaguePvpRankTemplet.LeagueTierIconNumber;
				}
				return 0;
			}
			NKMPvpRankTemplet rankTempletByTier = NKCPVPManager.GetRankTempletByTier(gameType, seasonID, leagueTier);
			if (rankTempletByTier != null)
			{
				return rankTempletByTier.LeagueTierIconNumber;
			}
			return 0;
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x0004EB3C File Offset: 0x0004CD3C
		public static string GetLeagueNameByTier(NKM_GAME_TYPE gameType, int seasonID, int leagueTier)
		{
			if (gameType != NKM_GAME_TYPE.NGT_PVP_RANK && gameType != NKM_GAME_TYPE.NGT_ASYNC_PVP)
			{
				switch (gameType)
				{
				case NKM_GAME_TYPE.NGT_PVP_LEAGUE:
				{
					NKMLeaguePvpRankTemplet nkmleaguePvpRankTemplet;
					if (NKMLeaguePvpRankTemplet.FindByTier(seasonID, leagueTier, out nkmleaguePvpRankTemplet))
					{
						return NKCStringTable.GetString(nkmleaguePvpRankTemplet.LeagueName, false);
					}
					goto IL_4F;
				}
				case NKM_GAME_TYPE.NGT_PVP_STRATEGY:
				case NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC:
					break;
				case NKM_GAME_TYPE.NGT_PVP_STRATEGY_REVENGE:
					goto IL_4F;
				default:
					goto IL_4F;
				}
			}
			NKMPvpRankTemplet rankTempletByTier = NKCPVPManager.GetRankTempletByTier(gameType, seasonID, leagueTier);
			if (rankTempletByTier != null)
			{
				return rankTempletByTier.GetLeagueName();
			}
			IL_4F:
			return "";
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x0004EB9D File Offset: 0x0004CD9D
		public static bool UseBan(NKM_GAME_TYPE gameType)
		{
			if (gameType <= NKM_GAME_TYPE.NGT_ASYNC_PVP)
			{
				if (gameType != NKM_GAME_TYPE.NGT_PVP_RANK && gameType != NKM_GAME_TYPE.NGT_ASYNC_PVP)
				{
				}
			}
			else
			{
				if (gameType == NKM_GAME_TYPE.NGT_PVP_LEAGUE)
				{
					return true;
				}
				if (gameType - NKM_GAME_TYPE.NGT_PVP_STRATEGY > 2)
				{
				}
			}
			return false;
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x0004EBC0 File Offset: 0x0004CDC0
		public static NKMPvpRankTemplet GetPvpRankTempletByTier(int seasonID, int leagueTier)
		{
			NKMPvpRankSeasonTemplet nkmpvpRankSeasonTemplet;
			if (!NKCPVPManager.dicPvpRankSeasonTemplet.TryGetValue(seasonID, out nkmpvpRankSeasonTemplet))
			{
				return null;
			}
			int rankGroup = nkmpvpRankSeasonTemplet.RankGroup;
			Dictionary<int, NKMPvpRankTemplet> dictionary;
			if (!NKCPVPManager.dicPvpRankTemplet.TryGetValue(rankGroup, out dictionary))
			{
				return null;
			}
			leagueTier = NKCPVPManager.GetFinalTier(leagueTier);
			NKMPvpRankTemplet result;
			if (dictionary.TryGetValue(leagueTier, out result))
			{
				return result;
			}
			int num = NKCPVPManager.dicPvpRankTemplet.Keys.Max();
			if (leagueTier > num && dictionary.TryGetValue(num, out result))
			{
				return result;
			}
			Log.Warn(string.Format("[PvpRankTemplet] templet not found. seasonId:{0} tierId:{1}", seasonID, leagueTier), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCPVPManager.cs", 350);
			return null;
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x0004EC58 File Offset: 0x0004CE58
		public static NKMPvpRankTemplet GetPvpRankTempletByScore(int seasonID, int leagueScore)
		{
			if (leagueScore < 0)
			{
				return null;
			}
			NKMPvpRankSeasonTemplet pvpRankSeasonTemplet = NKCPVPManager.GetPvpRankSeasonTemplet(seasonID);
			if (pvpRankSeasonTemplet == null)
			{
				return null;
			}
			int rankGroup = pvpRankSeasonTemplet.RankGroup;
			if (rankGroup == 0)
			{
				return null;
			}
			List<NKMPvpRankTemplet> list;
			if (!NKCPVPManager.listPvpRankTemplet.TryGetValue(rankGroup, out list))
			{
				return null;
			}
			int num = leagueScore / NKMPvpCommonConst.Instance.SCORE_MIN_INTERVAL_UNIT;
			if (num >= list.Count)
			{
				return list[list.Count - 1];
			}
			return list[num];
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x0004ECC0 File Offset: 0x0004CEC0
		public static NKMPvpRankSeasonTemplet GetPvpRankSeasonTemplet(int seasonID)
		{
			NKMPvpRankSeasonTemplet result;
			NKCPVPManager.dicPvpRankSeasonTemplet.TryGetValue(seasonID, out result);
			return result;
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0004ECDC File Offset: 0x0004CEDC
		public static NKM_ERROR_CODE CanPlayPVPRankGame(NKMUserData userData, int seasonID, int weekID, DateTime current)
		{
			if (!userData.m_RankOpen)
			{
				return NKM_ERROR_CODE.NEC_FAIL_PVP_INSUFFICIENT_OPEN_SCORE;
			}
			NKMPvpRankSeasonTemplet nkmpvpRankSeasonTemplet;
			if (!NKCPVPManager.dicPvpRankSeasonTemplet.TryGetValue(seasonID, out nkmpvpRankSeasonTemplet))
			{
				return NKM_ERROR_CODE.NEC_FAIL_INVALID_PVP_SEASON_DATA;
			}
			if (nkmpvpRankSeasonTemplet.EndDate < current)
			{
				return NKM_ERROR_CODE.NEC_FAIL_END_SEASON;
			}
			if (NKCPVPManager.WeekCalcStartDateUtc < current && current < NKCPVPManager.WeekCalcEndDateUtc)
			{
				return NKM_ERROR_CODE.NEC_FAIL_END_WEEK;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x0004ED44 File Offset: 0x0004CF44
		public static NKM_ERROR_CODE CanPlayPVPLeagueGame(NKMUserData userData, int seasonID, DateTime currentUTC)
		{
			if (!userData.m_LeagueOpen)
			{
				return NKM_ERROR_CODE.NEC_FAIL_PVP_INSUFFICIENT_OPEN_SCORE;
			}
			if (userData.m_ArmyData.GetUnitTypeCount() < NKMPvpCommonConst.Instance.DraftBan.MinUnitCount)
			{
				return NKM_ERROR_CODE.NEC_FAIL_DRAFT_PVP_NOT_ENOUGH_UNIT_COUNT;
			}
			if (userData.m_ArmyData.GetShipTypeCount() < NKMPvpCommonConst.Instance.DraftBan.MinShipCount)
			{
				return NKM_ERROR_CODE.NEC_FAIL_DRAFT_PVP_NOT_ENOUGH_SHIP_COUNT;
			}
			if (!NKMPvpCommonConst.Instance.LeaguePvp.IsValidTime(ServiceTime.Recent))
			{
				return NKM_ERROR_CODE.NEC_FAIL_DRAFT_PVP_INVALID_TIME;
			}
			NKMLeaguePvpRankSeasonTemplet nkmleaguePvpRankSeasonTemplet = NKMLeaguePvpRankSeasonTemplet.Find(seasonID);
			if (nkmleaguePvpRankSeasonTemplet == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_INVALID_PVP_SEASON_DATA;
			}
			if (nkmleaguePvpRankSeasonTemplet.EndDateUTC < currentUTC)
			{
				return NKM_ERROR_CODE.NEC_FAIL_END_SEASON;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x0004EDE4 File Offset: 0x0004CFE4
		public static NKM_ERROR_CODE CanRewardWeek(NKM_GAME_TYPE gameType, PvpState pvpData, int seasonID, int weekID, DateTime todayUTC)
		{
			if (pvpData.SeasonID == 0)
			{
				return NKM_ERROR_CODE.NEC_FAIL_PVP_SEASON_ID_0;
			}
			if (pvpData.SeasonID != seasonID)
			{
				return NKM_ERROR_CODE.NEC_FAIL_INVALID_PVP_SEASON_DATA;
			}
			if (gameType != NKM_GAME_TYPE.NGT_PVP_LEAGUE)
			{
				if (pvpData.WeekID == 0)
				{
					return NKM_ERROR_CODE.NEC_FAIL_PVP_WEEK_ID_0;
				}
				if (pvpData.WeekID == weekID)
				{
					return NKM_ERROR_CODE.NEC_FAIL_ALREADY_REWARD_WEEK_DATA;
				}
				if (NKCPVPManager.WeekCalcStartDateUtc < todayUTC && todayUTC < NKCPVPManager.WeekCalcEndDateUtc)
				{
					return NKM_ERROR_CODE.NEC_FAIL_END_WEEK;
				}
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x0004EE54 File Offset: 0x0004D054
		public static NKM_ERROR_CODE CanRewardSeason(PvpState pvpData, int seasonID, DateTime todayUTC)
		{
			if (pvpData.SeasonID == 0 || pvpData.SeasonID != seasonID - 1)
			{
				return NKM_ERROR_CODE.NEC_FAIL_INVALID_PVP_SEASON_DATA;
			}
			if (pvpData.SeasonID == seasonID)
			{
				return NKM_ERROR_CODE.NEC_FAIL_ALREADY_REWARD_SEASON_DATA;
			}
			if (NKCPVPManager.WeekCalcStartDateUtc < todayUTC && todayUTC < NKCPVPManager.WeekCalcEndDateUtc)
			{
				return NKM_ERROR_CODE.NEC_FAIL_END_WEEK;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x0004EEAA File Offset: 0x0004D0AA
		public static long GetLastUpdateChargePointTicks()
		{
			return NKCScenManager.CurrentUserData().LastPvpPointChargeTimeUTC.Ticks;
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x0004EEBC File Offset: 0x0004D0BC
		public static NKMPvpRankTemplet GetAsyncPvpRankTempletByTier(int seasonID, int leagueTier)
		{
			NKMPvpRankSeasonTemplet nkmpvpRankSeasonTemplet;
			if (!NKCPVPManager.dicAsyncPvpSeasonTemplet.TryGetValue(seasonID, out nkmpvpRankSeasonTemplet))
			{
				return null;
			}
			int rankGroup = nkmpvpRankSeasonTemplet.RankGroup;
			Dictionary<int, NKMPvpRankTemplet> dictionary;
			if (!NKCPVPManager.dicPvpRankTemplet.TryGetValue(rankGroup, out dictionary))
			{
				return null;
			}
			leagueTier = NKCPVPManager.GetFinalTier(leagueTier);
			NKMPvpRankTemplet result;
			if (dictionary.TryGetValue(leagueTier, out result))
			{
				return result;
			}
			int num = NKCPVPManager.dicPvpRankTemplet.Keys.Max();
			if (leagueTier > num && dictionary.TryGetValue(num, out result))
			{
				return result;
			}
			Log.Warn(string.Format("[PvpRankTemplet] templet not found. seasonId:{0} tierId:{1}", seasonID, leagueTier), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCPVPManager.cs", 569);
			return null;
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x0004EF54 File Offset: 0x0004D154
		public static NKMPvpRankTemplet GetAsyncPvpRankTempletByScore(int seasonID, int leagueScore)
		{
			if (leagueScore < 0)
			{
				return null;
			}
			NKMPvpRankSeasonTemplet pvpAsyncSeasonTemplet = NKCPVPManager.GetPvpAsyncSeasonTemplet(seasonID);
			if (pvpAsyncSeasonTemplet == null)
			{
				return null;
			}
			int rankGroup = pvpAsyncSeasonTemplet.RankGroup;
			if (rankGroup == 0)
			{
				return null;
			}
			List<NKMPvpRankTemplet> list;
			if (!NKCPVPManager.listPvpRankTemplet.TryGetValue(rankGroup, out list))
			{
				return null;
			}
			int num = leagueScore / NKMPvpCommonConst.Instance.SCORE_MIN_INTERVAL_UNIT;
			if (num >= list.Count)
			{
				return list[list.Count - 1];
			}
			return list[num];
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x0004EFBC File Offset: 0x0004D1BC
		public static NKMPvpRankSeasonTemplet GetPvpAsyncSeasonTemplet(int seasonID)
		{
			NKMPvpRankSeasonTemplet result;
			NKCPVPManager.dicAsyncPvpSeasonTemplet.TryGetValue(seasonID, out result);
			return result;
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x0004EFD8 File Offset: 0x0004D1D8
		public static void Init(DateTime date)
		{
			NKCPVPManager.ReloadWeekIDForRank(date, NKCUtil.FindPVPSeasonIDForRank(NKCSynchronizedTime.GetServerUTCTime(0.0)));
			NKCPVPManager.ReloadWeekIDForAsync(date, NKCUtil.FindPVPSeasonIDForAsync(NKCSynchronizedTime.GetServerUTCTime(0.0)));
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x0004F00C File Offset: 0x0004D20C
		public static int GetResetScore(int seasonID, int orgScore, NKM_GAME_TYPE gameType)
		{
			if (gameType <= NKM_GAME_TYPE.NGT_ASYNC_PVP)
			{
				if (gameType != NKM_GAME_TYPE.NGT_PVP_RANK && gameType != NKM_GAME_TYPE.NGT_ASYNC_PVP)
				{
				}
			}
			else
			{
				if (gameType == NKM_GAME_TYPE.NGT_PVP_LEAGUE)
				{
					return NKCPVPManager.GetLeagueResetScore(orgScore);
				}
				if (gameType - NKM_GAME_TYPE.NGT_PVP_STRATEGY > 2)
				{
				}
			}
			NKMPvpRankTemplet rankTempletByScore = NKCPVPManager.GetRankTempletByScore(gameType, seasonID, orgScore);
			if (rankTempletByScore != null && rankTempletByScore.LeagueDemotePoint < orgScore)
			{
				return rankTempletByScore.LeagueDemotePoint;
			}
			return orgScore;
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x0004F05B File Offset: 0x0004D25B
		private static int GetLeagueResetScore(int orgScore)
		{
			if (NKMPvpCommonConst.Instance.LEAGUE_PVP_RESET_SCORE < orgScore)
			{
				return NKMPvpCommonConst.Instance.LEAGUE_PVP_RESET_SCORE;
			}
			return orgScore;
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x0004F078 File Offset: 0x0004D278
		public static bool IsRewardWeek(int seasonID, DateTime weekCalcStartDate)
		{
			NKMPvpRankSeasonTemplet nkmpvpRankSeasonTemplet;
			return NKCPVPManager.dicPvpRankSeasonTemplet.TryGetValue(seasonID, out nkmpvpRankSeasonTemplet) && weekCalcStartDate != nkmpvpRankSeasonTemplet.EndDate;
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x0004F0A2 File Offset: 0x0004D2A2
		public static bool IsRewardWeek(NKMPvpRankSeasonTemplet seasonTemplet, DateTime weekCalcStartDate)
		{
			return seasonTemplet != null && weekCalcStartDate != seasonTemplet.EndDate;
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x0004F0B5 File Offset: 0x0004D2B5
		public static int GetWeekIDForRank(DateTime today, int seasonID)
		{
			if (NKCPVPManager.RankNextWeekIDResetDate < today)
			{
				NKCPVPManager.ReloadWeekIDForRank(today, seasonID);
			}
			return NKCPVPManager.WeekID_Rank;
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x0004F0D0 File Offset: 0x0004D2D0
		public static int GetWeekIDForAsync(DateTime today, int seasonID)
		{
			if (NKCPVPManager.AsyncNextWeekIDResetDate < today)
			{
				NKCPVPManager.ReloadWeekIDForAsync(today, seasonID);
			}
			return NKCPVPManager.WeekID_Async;
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x0004F0EB File Offset: 0x0004D2EB
		public static int GetWeekIDForLeague(DateTime today, int seasonID)
		{
			if (NKCPVPManager.LeagueNextWeekIDResetDate < today)
			{
				NKCPVPManager.ReloadWeekIDForLeague(today, seasonID);
			}
			return NKCPVPManager.WeekID_League;
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x0004F108 File Offset: 0x0004D308
		public static NKMPvpRankTemplet GetRankTempletByRankGroupScore(int rankGroup, int leagueScore)
		{
			if (leagueScore < 0)
			{
				return null;
			}
			if (rankGroup == 0)
			{
				return null;
			}
			List<NKMPvpRankTemplet> list;
			if (!NKCPVPManager.listPvpRankTemplet.TryGetValue(rankGroup, out list))
			{
				return null;
			}
			int num = leagueScore / NKMPvpCommonConst.Instance.SCORE_MIN_INTERVAL_UNIT;
			if (num >= list.Count)
			{
				return list[list.Count - 1];
			}
			return list[num];
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x0004F160 File Offset: 0x0004D360
		public static NKMPvpRankTemplet GetRankTempletByRankGroupTier(int rankGroup, int leagueTier)
		{
			Dictionary<int, NKMPvpRankTemplet> dictionary;
			if (!NKCPVPManager.dicPvpRankTemplet.TryGetValue(rankGroup, out dictionary))
			{
				return null;
			}
			leagueTier = NKCPVPManager.GetFinalTier(leagueTier);
			NKMPvpRankTemplet result;
			if (!dictionary.TryGetValue(leagueTier, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x0004F194 File Offset: 0x0004D394
		private static void ReloadWeekIDForRank(DateTime today, int seasonID)
		{
			NKMPvpRankSeasonTemplet nkmpvpRankSeasonTemplet;
			NKCPVPManager.dicPvpRankSeasonTemplet.TryGetValue(seasonID, out nkmpvpRankSeasonTemplet);
			if (nkmpvpRankSeasonTemplet == null)
			{
				Log.Error(string.Format("Invalid SeasonID seasonID : {0}", seasonID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCPVPManager.cs", 737);
				return;
			}
			int num = (int)((today - nkmpvpRankSeasonTemplet.StartDate).TotalDays / 7.0) + 1;
			if (num != NKCPVPManager.WeekID_Rank)
			{
				NKCPVPManager.ReloadWeekCalcDate(today);
				NKCPVPManager.WeekID_Rank = num;
			}
			NKCPVPManager.RankNextWeekIDResetDate = NKMTime.GetNextResetTime(today, NKMTime.TimePeriod.Day, NKCPVPManager.WEEK_CALC_END_TIME);
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x0004F21C File Offset: 0x0004D41C
		private static void ReloadWeekIDForAsync(DateTime today, int seasonID)
		{
			NKMPvpRankSeasonTemplet nkmpvpRankSeasonTemplet;
			NKCPVPManager.dicAsyncPvpSeasonTemplet.TryGetValue(seasonID, out nkmpvpRankSeasonTemplet);
			if (nkmpvpRankSeasonTemplet == null)
			{
				Log.Error(string.Format("Invalid SeasonID seasonID : {0}", seasonID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCPVPManager.cs", 758);
				return;
			}
			int num = (int)((today - nkmpvpRankSeasonTemplet.StartDate).TotalDays / 7.0) + 1;
			if (num != NKCPVPManager.WeekID_Async)
			{
				NKCPVPManager.ReloadWeekCalcDate(today);
				NKCPVPManager.WeekID_Async = num;
			}
			NKCPVPManager.AsyncNextWeekIDResetDate = NKMTime.GetNextResetTime(today, NKMTime.TimePeriod.Day, NKCPVPManager.WEEK_CALC_END_TIME);
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x0004F2A4 File Offset: 0x0004D4A4
		private static void ReloadWeekIDForLeague(DateTime today, int seasonID)
		{
			NKMLeaguePvpRankSeasonTemplet nkmleaguePvpRankSeasonTemplet = NKMLeaguePvpRankSeasonTemplet.Find(seasonID);
			if (nkmleaguePvpRankSeasonTemplet == null)
			{
				Log.Error(string.Format("Invalid League SeasonID seasonID : {0}", seasonID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCPVPManager.cs", 779);
				return;
			}
			int num = (int)((today - nkmleaguePvpRankSeasonTemplet.StartDateUTC).TotalDays / 7.0) + 1;
			if (num != NKCPVPManager.WeekID_League)
			{
				NKCPVPManager.ReloadWeekCalcDate(today);
				NKCPVPManager.WeekID_League = num;
			}
			NKCPVPManager.LeagueNextWeekIDResetDate = NKMTime.GetNextResetTime(today, NKMTime.TimePeriod.Day, NKCPVPManager.WEEK_CALC_END_TIME);
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x0004F324 File Offset: 0x0004D524
		private static void ReloadWeekCalcDate(DateTime today)
		{
			NKCPVPManager.WeekCalcEndDateUtc = NKMTime.GetNextResetTime(today, NKMTime.TimePeriod.Week, NKCPVPManager.WEEK_CALC_END_TIME);
			NKCPVPManager.WeekCalcStartDateUtc = NKCPVPManager.WeekCalcEndDateUtc.AddHours(-6.0);
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x0004F360 File Offset: 0x0004D560
		public static NKM_ERROR_CODE CanPlayPVPAsyncGame(NKMUserData userData, int seasonID, int weekID, DateTime currentDate)
		{
			NKMPvpRankSeasonTemplet nkmpvpRankSeasonTemplet;
			if (!NKCPVPManager.dicAsyncPvpSeasonTemplet.TryGetValue(seasonID, out nkmpvpRankSeasonTemplet))
			{
				return NKM_ERROR_CODE.NEC_FAIL_INVALID_PVP_SEASON_DATA;
			}
			if (nkmpvpRankSeasonTemplet.EndDate < currentDate)
			{
				return NKM_ERROR_CODE.NEC_FAIL_END_SEASON;
			}
			if (NKCPVPManager.WeekCalcStartDateUtc < currentDate && currentDate < NKCPVPManager.WeekCalcEndDateUtc)
			{
				return NKM_ERROR_CODE.NEC_FAIL_END_WEEK;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x0004F3B8 File Offset: 0x0004D5B8
		public static bool IsPvpRankUnlocked()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			return nkmuserData != null && nkmuserData.m_RankOpen;
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x0004F3D8 File Offset: 0x0004D5D8
		public static bool IsPvpLeagueUnlocked()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			return nkmuserData != null && nkmuserData.m_LeagueOpen;
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x0004F3F8 File Offset: 0x0004D5F8
		public static string GetLeagueOpenDaysString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < NKMPvpCommonConst.Instance.LeaguePvp.OpenDaysOfWeek.Count; i++)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" / ");
				}
				stringBuilder.Append(NKCUtilString.GetDayString(NKMPvpCommonConst.Instance.LeaguePvp.OpenDaysOfWeek[i]));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x0004F468 File Offset: 0x0004D668
		public static string GetLeagueOpenTimeString()
		{
			return string.Format("{0:00}:{1:00} ~ {2:00}:{3:00}", new object[]
			{
				NKMPvpCommonConst.Instance.LeaguePvp.OpenTimeStart.Hours,
				NKMPvpCommonConst.Instance.LeaguePvp.OpenTimeStart.Minutes,
				NKMPvpCommonConst.Instance.LeaguePvp.OpenTimeEnd.Hours,
				NKMPvpCommonConst.Instance.LeaguePvp.OpenTimeEnd.Minutes
			});
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x0004F504 File Offset: 0x0004D704
		public static int FindPvPSeasonID(NKM_GAME_TYPE gameType, DateTime nowUTC)
		{
			if (gameType <= NKM_GAME_TYPE.NGT_ASYNC_PVP)
			{
				if (gameType == NKM_GAME_TYPE.NGT_PVP_RANK)
				{
					return NKCUtil.FindPVPSeasonIDForRank(nowUTC);
				}
				if (gameType != NKM_GAME_TYPE.NGT_ASYNC_PVP)
				{
					return 0;
				}
			}
			else if (gameType != NKM_GAME_TYPE.NGT_PVP_LEAGUE)
			{
				if (gameType - NKM_GAME_TYPE.NGT_PVP_STRATEGY > 2)
				{
					return 0;
				}
			}
			else
			{
				NKMLeaguePvpRankSeasonTemplet nkmleaguePvpRankSeasonTemplet = NKMLeaguePvpRankSeasonTemplet.Find(NKCSynchronizedTime.ToServerServiceTime(nowUTC));
				if (nkmleaguePvpRankSeasonTemplet != null)
				{
					return nkmleaguePvpRankSeasonTemplet.SeasonId;
				}
				return 0;
			}
			return NKCUtil.FindPVPSeasonIDForAsync(nowUTC);
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x0004F554 File Offset: 0x0004D754
		public static NKMPvpRankTemplet GetRankTempletByScore(NKM_GAME_TYPE gameType, int seasonID, int leagueScore)
		{
			if (gameType == NKM_GAME_TYPE.NGT_PVP_RANK)
			{
				return NKCPVPManager.GetPvpRankTempletByScore(seasonID, leagueScore);
			}
			if (gameType != NKM_GAME_TYPE.NGT_ASYNC_PVP && gameType - NKM_GAME_TYPE.NGT_PVP_STRATEGY > 2)
			{
				return null;
			}
			return NKCPVPManager.GetAsyncPvpRankTempletByScore(seasonID, leagueScore);
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x0004F579 File Offset: 0x0004D779
		public static NKMPvpRankTemplet GetRankTempletByTier(NKM_GAME_TYPE gameType, int seasonID, int leagueTier)
		{
			if (gameType == NKM_GAME_TYPE.NGT_PVP_RANK)
			{
				return NKCPVPManager.GetPvpRankTempletByTier(seasonID, leagueTier);
			}
			if (gameType != NKM_GAME_TYPE.NGT_ASYNC_PVP && gameType - NKM_GAME_TYPE.NGT_PVP_STRATEGY > 2)
			{
				return null;
			}
			return NKCPVPManager.GetAsyncPvpRankTempletByTier(seasonID, leagueTier);
		}

		// Token: 0x04000E6C RID: 3692
		private const int WEEK_CALC_START_HOUR = 22;

		// Token: 0x04000E6D RID: 3693
		private const int WEEK_CALC_END_HOUR = 4;

		// Token: 0x04000E70 RID: 3696
		public static Dictionary<int, Dictionary<int, NKMPvpRankTemplet>> dicPvpRankTemplet = new Dictionary<int, Dictionary<int, NKMPvpRankTemplet>>();

		// Token: 0x04000E71 RID: 3697
		public static Dictionary<int, List<NKMPvpRankTemplet>> listPvpRankTemplet = new Dictionary<int, List<NKMPvpRankTemplet>>();

		// Token: 0x04000E72 RID: 3698
		public static Dictionary<int, NKMPvpRankSeasonTemplet> dicPvpRankSeasonTemplet = new Dictionary<int, NKMPvpRankSeasonTemplet>();

		// Token: 0x04000E73 RID: 3699
		public static Dictionary<int, NKMPvpRankSeasonTemplet> dicAsyncPvpSeasonTemplet = new Dictionary<int, NKMPvpRankSeasonTemplet>();
	}
}
