using System;
using System.Collections.Generic;
using System.Linq;
using Cs.Logging;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x02000456 RID: 1110
	public class NKMPVPManager
	{
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06001E1D RID: 7709 RVA: 0x0008EF66 File Offset: 0x0008D166
		// (set) Token: 0x06001E1E RID: 7710 RVA: 0x0008EF6D File Offset: 0x0008D16D
		public static DateTime WeekCalcStartDateUtc { get; set; }

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06001E1F RID: 7711 RVA: 0x0008EF75 File Offset: 0x0008D175
		// (set) Token: 0x06001E20 RID: 7712 RVA: 0x0008EF7C File Offset: 0x0008D17C
		public static DateTime WeekCalcEndDateUtc { get; set; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06001E21 RID: 7713 RVA: 0x0008EF84 File Offset: 0x0008D184
		// (set) Token: 0x06001E22 RID: 7714 RVA: 0x0008EF8B File Offset: 0x0008D18B
		public static DateTime ScoreBaseTime { get; private set; } = new DateTime(2080, 1, 1, 0, 0, 0);

		// Token: 0x06001E23 RID: 7715 RVA: 0x0008EF94 File Offset: 0x0008D194
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
					if (!NKMPVPManager.dicPvpRankTemplet.TryGetValue(nkmpvpRankTemplet.RankGroup, out dictionary))
					{
						dictionary = new Dictionary<int, NKMPvpRankTemplet>();
						NKMPVPManager.dicPvpRankTemplet.Add(nkmpvpRankTemplet.RankGroup, dictionary);
					}
					dictionary[nkmpvpRankTemplet.LeagueTier] = nkmpvpRankTemplet;
					num++;
					nkmlua.CloseTable();
				}
				nkmlua.CloseTable();
			}
			nkmlua.LuaClose();
			NKMPVPManager.dicPvpRankSeasonTemplet = NKMTempletLoader.LoadDictionary<NKMPvpRankSeasonTemplet>("AB_SCRIPT", "LUA_PVP_RANK_SEASON", "PVP_RANK_SEASON", new Func<NKMLua, NKMPvpRankSeasonTemplet>(NKMPvpRankSeasonTemplet.LoadFromLUA));
			if (NKMPVPManager.dicPvpRankSeasonTemplet == null)
			{
				Log.ErrorAndExit("PvpRankSeason load failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPManager.cs", 56);
				return false;
			}
			NKMPVPManager.dicAsyncPvpSeasonTemplet = NKMTempletLoader.LoadDictionary<NKMPvpRankSeasonTemplet>("AB_SCRIPT", "LUA_PVP_ASYNC_SEASON", "PVP_ASYNC_SEASON", new Func<NKMLua, NKMPvpRankSeasonTemplet>(NKMPvpRankSeasonTemplet.LoadFromLUA));
			if (NKMPVPManager.dicAsyncPvpSeasonTemplet == null)
			{
				Log.ErrorAndExit("PvpAsyncSeason load failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPManager.cs", 65);
				return false;
			}
			NKMPvpRankSeasonTemplet nkmpvpRankSeasonTemplet = null;
			foreach (NKMPvpRankSeasonTemplet nkmpvpRankSeasonTemplet2 in NKMPVPManager.dicAsyncPvpSeasonTemplet.Values)
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
						Log.ErrorAndExit(string.Format("Invalid Async SeasonId. Prev:{0}, Cur:{1}", nkmpvpRankSeasonTemplet.SeasonID, nkmpvpRankSeasonTemplet2.SeasonID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPManager.cs", 82);
						return false;
					}
					nkmpvpRankSeasonTemplet = nkmpvpRankSeasonTemplet2;
				}
			}
			NKMTempletContainer<NKMLeaguePvpRankSeasonTemplet>.Load("AB_SCRIPT", "LUA_PVP_LEAGUE_SEASON", "PVP_LEAGUE_SEASON", new Func<NKMLua, NKMLeaguePvpRankSeasonTemplet>(NKMLeaguePvpRankSeasonTemplet.LoadFromLUA));
			NKMLeaguePvpRankGroupTemplet.LoadFile();
			NKMPvpRankTemplet nkmpvpRankTemplet2 = null;
			List<NKMPvpRankTemplet> list = null;
			foreach (KeyValuePair<int, Dictionary<int, NKMPvpRankTemplet>> keyValuePair in NKMPVPManager.dicPvpRankTemplet)
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
							if (!NKMPVPManager.listPvpRankTemplet.TryGetValue(keyValuePair.Key, out list))
							{
								list = new List<NKMPvpRankTemplet>();
								NKMPVPManager.listPvpRankTemplet.Add(keyValuePair.Key, list);
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

		// Token: 0x06001E24 RID: 7716 RVA: 0x0008F2A0 File Offset: 0x0008D4A0
		public static void Join()
		{
			foreach (NKMPvpRankSeasonTemplet nkmpvpRankSeasonTemplet in NKMPVPManager.dicPvpRankSeasonTemplet.Values)
			{
				nkmpvpRankSeasonTemplet.Join();
			}
			foreach (NKMPvpRankSeasonTemplet nkmpvpRankSeasonTemplet2 in NKMPVPManager.dicAsyncPvpSeasonTemplet.Values)
			{
				nkmpvpRankSeasonTemplet2.Join();
			}
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x0008F338 File Offset: 0x0008D538
		public static void Validate()
		{
			foreach (NKMPvpRankTemplet nkmpvpRankTemplet in NKMPVPManager.listPvpRankTemplet.SelectMany((KeyValuePair<int, List<NKMPvpRankTemplet>> e) => e.Value))
			{
				if (NKMGameStatRateTemplet.Find(nkmpvpRankTemplet.m_GameStatRateID) == null)
				{
					NKMTempletError.Add("PvpRankTemplet 의 m_GameStatRateID(" + nkmpvpRankTemplet.m_GameStatRateID + ") 에 해당하는 GameStatRateTemplet 이 존재하지 않음", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPManager.cs", 148);
				}
			}
			NKMLeaguePvpRankSeasonTemplet nkmleaguePvpRankSeasonTemplet = null;
			foreach (NKMLeaguePvpRankSeasonTemplet nkmleaguePvpRankSeasonTemplet2 in NKMTempletContainer<NKMLeaguePvpRankSeasonTemplet>.Values)
			{
				if (nkmleaguePvpRankSeasonTemplet == null)
				{
					nkmleaguePvpRankSeasonTemplet = nkmleaguePvpRankSeasonTemplet2;
				}
				else
				{
					if (nkmleaguePvpRankSeasonTemplet.SeasonId != nkmleaguePvpRankSeasonTemplet2.SeasonId - 1)
					{
						NKMTempletError.Add(string.Format("Invalid League SeasonId. Prev:{0}, Cur:{1}", nkmleaguePvpRankSeasonTemplet.SeasonId, nkmleaguePvpRankSeasonTemplet2.SeasonId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPManager.cs", 163);
					}
					if (nkmleaguePvpRankSeasonTemplet2.Interval.StartDate - nkmleaguePvpRankSeasonTemplet.Interval.EndDate > TimeSpan.FromDays(1.0))
					{
						NKMTempletError.Add(string.Format("[LeaguePvp] {0} 종료:{1}, {2} 시작:{3}", new object[]
						{
							nkmleaguePvpRankSeasonTemplet.Name,
							nkmleaguePvpRankSeasonTemplet.Interval.EndDate,
							nkmleaguePvpRankSeasonTemplet2.Name,
							nkmleaguePvpRankSeasonTemplet2.Interval.StartDate
						}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPManager.cs", 168);
					}
					nkmleaguePvpRankSeasonTemplet = nkmleaguePvpRankSeasonTemplet2;
				}
			}
			NKMPvpRankSeasonTemplet nkmpvpRankSeasonTemplet = null;
			foreach (NKMPvpRankSeasonTemplet nkmpvpRankSeasonTemplet2 in NKMPVPManager.dicPvpRankSeasonTemplet.Values)
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
						NKMTempletError.Add(string.Format("Invalid Rank SeasonId. Prev:{0} Cur:{1}", nkmpvpRankSeasonTemplet.SeasonID, nkmpvpRankSeasonTemplet2.SeasonID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPManager.cs", 187);
					}
					if (nkmpvpRankSeasonTemplet2.Interval.StartDate - nkmpvpRankSeasonTemplet.Interval.EndDate > TimeSpan.FromDays(1.0))
					{
						NKMTempletError.Add(string.Format("[RankPvp] {0} 종료:{1}, {2} 시작:{3}", new object[]
						{
							nkmpvpRankSeasonTemplet.Name,
							nkmpvpRankSeasonTemplet.Interval.EndDate,
							nkmpvpRankSeasonTemplet2.Name,
							nkmpvpRankSeasonTemplet2.Interval.StartDate
						}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMPVPManager.cs", 192);
					}
					nkmpvpRankSeasonTemplet = nkmpvpRankSeasonTemplet2;
				}
			}
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x0008F618 File Offset: 0x0008D818
		public static int GetFinalTier(int tier)
		{
			if (tier == 0)
			{
				tier = 1;
			}
			return tier;
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x0008F624 File Offset: 0x0008D824
		public static NKMPvpRankTemplet GetPvpRankTempletByTier(int seasonID, int leagueTier)
		{
			NKMPvpRankSeasonTemplet nkmpvpRankSeasonTemplet;
			if (!NKMPVPManager.dicPvpRankSeasonTemplet.TryGetValue(seasonID, out nkmpvpRankSeasonTemplet))
			{
				return null;
			}
			int rankGroup = nkmpvpRankSeasonTemplet.RankGroup;
			Dictionary<int, NKMPvpRankTemplet> dictionary;
			if (!NKMPVPManager.dicPvpRankTemplet.TryGetValue(rankGroup, out dictionary))
			{
				return null;
			}
			leagueTier = NKMPVPManager.GetFinalTier(leagueTier);
			NKMPvpRankTemplet result;
			if (!dictionary.TryGetValue(leagueTier, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x0008F670 File Offset: 0x0008D870
		public static NKMPvpRankTemplet GetPvpRankTempletByScore(int seasonID, int leagueScore)
		{
			if (leagueScore < 0)
			{
				return null;
			}
			NKMPvpRankSeasonTemplet pvpRankSeasonTemplet = NKMPVPManager.GetPvpRankSeasonTemplet(seasonID);
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
			if (!NKMPVPManager.listPvpRankTemplet.TryGetValue(rankGroup, out list))
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

		// Token: 0x06001E29 RID: 7721 RVA: 0x0008F6D8 File Offset: 0x0008D8D8
		public static NKMPvpRankSeasonTemplet GetPvpRankSeasonTemplet(int seasonID)
		{
			NKMPvpRankSeasonTemplet result;
			NKMPVPManager.dicPvpRankSeasonTemplet.TryGetValue(seasonID, out result);
			return result;
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x0008F6F4 File Offset: 0x0008D8F4
		public static NKM_ERROR_CODE CanPlayPVPRankGame(NKMUserData userData, int seasonID, int weekID, DateTime current)
		{
			if (userData.m_PvpData.SeasonID != 0 && userData.m_PvpData.SeasonID == seasonID && userData.m_PvpData.WeekID != 0 && userData.m_PvpData.WeekID != weekID)
			{
				return NKM_ERROR_CODE.NEC_FAIL_HAVE_NOT_BEEN_REWARDED;
			}
			NKMPvpRankSeasonTemplet nkmpvpRankSeasonTemplet;
			if (!NKMPVPManager.dicPvpRankSeasonTemplet.TryGetValue(seasonID, out nkmpvpRankSeasonTemplet))
			{
				return NKM_ERROR_CODE.NEC_FAIL_INVALID_PVP_SEASON_DATA;
			}
			if (nkmpvpRankSeasonTemplet.EndDate < current)
			{
				return NKM_ERROR_CODE.NEC_FAIL_END_SEASON;
			}
			if (NKMPVPManager.WeekCalcStartDateUtc < current && current < NKMPVPManager.WeekCalcEndDateUtc)
			{
				return NKM_ERROR_CODE.NEC_FAIL_END_WEEK;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x0008F788 File Offset: 0x0008D988
		public static NKM_ERROR_CODE CanRewardWeek(PvpState pvpData, int seasonID, int weekID, DateTime today)
		{
			if (pvpData.SeasonID == 0)
			{
				return NKM_ERROR_CODE.NEC_FAIL_PVP_SEASON_ID_0;
			}
			if (pvpData.WeekID == 0)
			{
				return NKM_ERROR_CODE.NEC_FAIL_PVP_WEEK_ID_0;
			}
			if (pvpData.SeasonID != seasonID)
			{
				return NKM_ERROR_CODE.NEC_FAIL_INVALID_PVP_SEASON_DATA;
			}
			if (pvpData.WeekID == weekID)
			{
				return NKM_ERROR_CODE.NEC_FAIL_ALREADY_REWARD_WEEK_DATA;
			}
			if (NKMPVPManager.WeekCalcStartDateUtc < today && today < NKMPVPManager.WeekCalcEndDateUtc)
			{
				return NKM_ERROR_CODE.NEC_FAIL_END_WEEK;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x0008F7F0 File Offset: 0x0008D9F0
		public static NKM_ERROR_CODE CanRewardSeason(PvpState pvpData, int seasonID, DateTime today)
		{
			if (pvpData.SeasonID == 0 || pvpData.SeasonID != seasonID - 1)
			{
				return NKM_ERROR_CODE.NEC_FAIL_INVALID_PVP_SEASON_DATA;
			}
			if (pvpData.SeasonID == seasonID)
			{
				return NKM_ERROR_CODE.NEC_FAIL_ALREADY_REWARD_SEASON_DATA;
			}
			if (NKMPVPManager.WeekCalcStartDateUtc < today && today < NKMPVPManager.WeekCalcEndDateUtc)
			{
				return NKM_ERROR_CODE.NEC_FAIL_END_WEEK;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x0008F848 File Offset: 0x0008DA48
		public static NKMPvpRankTemplet GetAsyncPvpRankTempletByTier(int seasonID, int leagueTier)
		{
			NKMPvpRankSeasonTemplet nkmpvpRankSeasonTemplet;
			if (!NKMPVPManager.dicAsyncPvpSeasonTemplet.TryGetValue(seasonID, out nkmpvpRankSeasonTemplet))
			{
				return null;
			}
			int rankGroup = nkmpvpRankSeasonTemplet.RankGroup;
			Dictionary<int, NKMPvpRankTemplet> dictionary;
			if (!NKMPVPManager.dicPvpRankTemplet.TryGetValue(rankGroup, out dictionary))
			{
				return null;
			}
			leagueTier = NKMPVPManager.GetFinalTier(leagueTier);
			NKMPvpRankTemplet result;
			if (!dictionary.TryGetValue(leagueTier, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x0008F894 File Offset: 0x0008DA94
		public static NKMPvpRankTemplet GetAsyncPvpRankTempletByScore(int seasonID, int leagueScore)
		{
			if (leagueScore < 0)
			{
				return null;
			}
			NKMPvpRankSeasonTemplet pvpAsyncSeasonTemplet = NKMPVPManager.GetPvpAsyncSeasonTemplet(seasonID);
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
			if (!NKMPVPManager.listPvpRankTemplet.TryGetValue(rankGroup, out list))
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

		// Token: 0x06001E2F RID: 7727 RVA: 0x0008F8FC File Offset: 0x0008DAFC
		public static NKMPvpRankSeasonTemplet GetPvpAsyncSeasonTemplet(int seasonID)
		{
			NKMPvpRankSeasonTemplet result;
			NKMPVPManager.dicAsyncPvpSeasonTemplet.TryGetValue(seasonID, out result);
			return result;
		}

		// Token: 0x04001ED3 RID: 7891
		public static Dictionary<int, Dictionary<int, NKMPvpRankTemplet>> dicPvpRankTemplet = new Dictionary<int, Dictionary<int, NKMPvpRankTemplet>>();

		// Token: 0x04001ED4 RID: 7892
		public static Dictionary<int, List<NKMPvpRankTemplet>> listPvpRankTemplet = new Dictionary<int, List<NKMPvpRankTemplet>>();

		// Token: 0x04001ED5 RID: 7893
		public static Dictionary<int, NKMPvpRankSeasonTemplet> dicPvpRankSeasonTemplet = new Dictionary<int, NKMPvpRankSeasonTemplet>();

		// Token: 0x04001ED6 RID: 7894
		public static Dictionary<int, NKMPvpRankSeasonTemplet> dicAsyncPvpSeasonTemplet = new Dictionary<int, NKMPvpRankSeasonTemplet>();
	}
}
