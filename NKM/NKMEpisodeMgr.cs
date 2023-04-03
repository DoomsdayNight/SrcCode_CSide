using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ClientPacket.Common;
using Cs.Core.Util;
using Cs.Logging;
using NKC;
using NKC.Templet;
using NKC.UI;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;

namespace NKM
{
	// Token: 0x020003F1 RID: 1009
	public static class NKMEpisodeMgr
	{
		// Token: 0x06001A9E RID: 6814 RVA: 0x00074943 File Offset: 0x00072B43
		public static void LoadFromLUA(string fileName)
		{
			NKMTempletContainer<NKMStageTempletV2>.Load("AB_SCRIPT", fileName, "STAGE_TEMPLET", new Func<NKMLua, NKMStageTempletV2>(NKMStageTempletV2.LoadFromLUA), (NKMStageTempletV2 e) => e.StrId);
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x00074980 File Offset: 0x00072B80
		public static void Initialize()
		{
			foreach (NKMEpisodeTempletV2 episodeTemplet in NKMEpisodeTempletV2.Values)
			{
				NKMEpisodeMgr.AddCounterCaseTemplets(episodeTemplet);
			}
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x000749CC File Offset: 0x00072BCC
		public static NKM_ERROR_CODE CanGetEpisodeCompleteReward(NKMUserData cNKMUserData, int episodeID)
		{
			NKM_ERROR_CODE nkm_ERROR_CODE = NKM_ERROR_CODE.NEC_OK;
			for (int i = 0; i <= 1; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					nkm_ERROR_CODE = NKMEpisodeMgr.CanGetEpisodeCompleteReward(cNKMUserData, episodeID, (EPISODE_DIFFICULTY)i, j);
					if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_OK)
					{
						return nkm_ERROR_CODE;
					}
				}
			}
			return nkm_ERROR_CODE;
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x00074A04 File Offset: 0x00072C04
		public static NKM_ERROR_CODE CanGetEpisodeCompleteReward(NKMUserData cNKMUserData, int episodeID, EPISODE_DIFFICULTY episodeDifficulty, int rewardIndex)
		{
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(episodeID, episodeDifficulty);
			if (nkmepisodeTempletV == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_INVALID_EPISODE_ID;
			}
			if (rewardIndex < 0 || rewardIndex >= 3)
			{
				return NKM_ERROR_CODE.NEC_FAIL_EPISODE_COMPLETE_REWARD_INVALID_REWARD;
			}
			CompletionReward completionReward = nkmepisodeTempletV.m_CompletionReward[rewardIndex];
			if (completionReward == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_EPISODE_COMPLETE_REWARD_INVALID_REWARD;
			}
			NKMEpisodeCompleteData episodeCompleteData = cNKMUserData.GetEpisodeCompleteData(episodeID, episodeDifficulty);
			if (episodeCompleteData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_EPISODE_COMPLETE_REWARD_NOT_ENOUGH_COUNT;
			}
			if (episodeCompleteData.m_bRewards[rewardIndex])
			{
				return NKM_ERROR_CODE.NEC_FAIL_EPISODE_COMPLETE_REWARD_ALREADY_GIVEN;
			}
			float num = (float)(NKMEpisodeMgr.GetTotalMedalCount(nkmepisodeTempletV) * completionReward.m_CompleteRate) * 0.01f;
			if (episodeCompleteData.m_EpisodeCompleteCount < (int)num)
			{
				return NKM_ERROR_CODE.NEC_FAIL_EPISODE_COMPLETE_REWARD_NOT_ENOUGH_COUNT;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x00074A88 File Offset: 0x00072C88
		public static List<NKMStageTempletV2> GetCounterCaseTemplets(int unitID)
		{
			List<NKMStageTempletV2> result;
			NKMEpisodeMgr.CounterCaseTemplets.TryGetValue(unitID, out result);
			return result;
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x00074AA4 File Offset: 0x00072CA4
		public static List<NKMStageTempletV2> GetAllCounterCaseTemplets()
		{
			return NKMEpisodeMgr.CounterCaseTemplets.Values.SelectMany((List<NKMStageTempletV2> e) => e).ToList<NKMStageTempletV2>();
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x00074ADC File Offset: 0x00072CDC
		public static int GetTotalMedalCount(int episodeID)
		{
			int num = 0;
			for (int i = 0; i < 1; i++)
			{
				NKMEpisodeTempletV2 episodeTemplet = NKMEpisodeTempletV2.Find(episodeID, (EPISODE_DIFFICULTY)i);
				num += NKMEpisodeMgr.GetTotalMedalCount(episodeTemplet);
			}
			return num;
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x00074B0C File Offset: 0x00072D0C
		public static int GetTotalMedalCount(NKMEpisodeTempletV2 episodeTemplet)
		{
			int result = 0;
			if (episodeTemplet != null)
			{
				if (episodeTemplet.m_HasCompleteReward)
				{
					result = NKMEpisodeMgr.GetTotalMedalCountByStageType(episodeTemplet);
				}
				else
				{
					result = NKMEpisodeMgr.GetTotalClearCount(episodeTemplet);
				}
			}
			return result;
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x00074B38 File Offset: 0x00072D38
		private static int GetTotalMedalCountByStageType(NKMEpisodeTempletV2 episodeTemplet)
		{
			if (episodeTemplet == null)
			{
				return 0;
			}
			int num = 0;
			foreach (KeyValuePair<int, List<NKMStageTempletV2>> keyValuePair in episodeTemplet.m_DicStage)
			{
				for (int i = 0; i < keyValuePair.Value.Count; i++)
				{
					NKMStageTempletV2 nkmstageTempletV = keyValuePair.Value[i];
					if (nkmstageTempletV.EnableByTag)
					{
						switch (nkmstageTempletV.m_STAGE_TYPE)
						{
						case STAGE_TYPE.ST_WARFARE:
							num += 3;
							break;
						case STAGE_TYPE.ST_DUNGEON:
						{
							NKMDungeonTempletBase dungeonTempletBase = nkmstageTempletV.DungeonTempletBase;
							if (dungeonTempletBase.m_DGMissionType_1 == DUNGEON_GAME_MISSION_TYPE.DGMT_NONE && dungeonTempletBase.m_DGMissionValue_1 == 0 && dungeonTempletBase.m_DGMissionType_2 == DUNGEON_GAME_MISSION_TYPE.DGMT_NONE && dungeonTempletBase.m_DGMissionValue_2 == 0)
							{
								num++;
							}
							else
							{
								num += 3;
							}
							break;
						}
						case STAGE_TYPE.ST_PHASE:
						{
							NKMPhaseTemplet phaseTemplet = nkmstageTempletV.PhaseTemplet;
							if (phaseTemplet.m_DGMissionType_1 == DUNGEON_GAME_MISSION_TYPE.DGMT_NONE && phaseTemplet.m_DGMissionValue_1 == 0 && phaseTemplet.m_DGMissionType_2 == DUNGEON_GAME_MISSION_TYPE.DGMT_NONE && phaseTemplet.m_DGMissionValue_2 == 0)
							{
								num++;
							}
							else
							{
								num += 3;
							}
							break;
						}
						default:
							Log.Warn(string.Format("Invalid STAGE_TYPE, StageID : {0}", nkmstageTempletV.Key), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMEpisodeMgr.cs", 207);
							break;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x00074C98 File Offset: 0x00072E98
		private static int GetTotalClearCount(NKMEpisodeTempletV2 episodeTemplet)
		{
			int num = 0;
			foreach (KeyValuePair<int, List<NKMStageTempletV2>> keyValuePair in episodeTemplet.m_DicStage)
			{
				for (int i = 0; i < keyValuePair.Value.Count; i++)
				{
					if (keyValuePair.Value[i].EnableByTag)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x00074D18 File Offset: 0x00072F18
		private static bool IsCounterCaseUnlockType(STAGE_UNLOCK_REQ_TYPE type)
		{
			return type - STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_GET <= 9;
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x00074D24 File Offset: 0x00072F24
		private static void AddCounterCaseTemplets(NKMEpisodeTempletV2 episodeTemplet)
		{
			if (episodeTemplet.m_EPCategory != EPISODE_CATEGORY.EC_COUNTERCASE)
			{
				return;
			}
			foreach (KeyValuePair<int, List<NKMStageTempletV2>> keyValuePair in episodeTemplet.m_DicStage)
			{
				for (int i = 0; i < keyValuePair.Value.Count; i++)
				{
					NKMStageTempletV2 nkmstageTempletV = keyValuePair.Value[i];
					if (NKMEpisodeMgr.IsCounterCaseUnlockType(nkmstageTempletV.m_UnlockInfo.eReqType))
					{
						int reqValue = nkmstageTempletV.m_UnlockInfo.reqValue;
						if (reqValue > 0)
						{
							List<NKMStageTempletV2> list;
							if (!NKMEpisodeMgr.CounterCaseTemplets.TryGetValue(reqValue, out list))
							{
								list = new List<NKMStageTempletV2>();
								NKMEpisodeMgr.CounterCaseTemplets.Add(reqValue, list);
							}
							list.Add(nkmstageTempletV);
						}
					}
				}
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06001AAA RID: 6826 RVA: 0x00074DF4 File Offset: 0x00072FF4
		// (set) Token: 0x06001AAB RID: 6827 RVA: 0x00074DFB File Offset: 0x00072FFB
		public static IReadOnlyList<NKMEpisodeTempletV2> EpisodeTemplets { get; private set; }

		// Token: 0x06001AAC RID: 6828 RVA: 0x00074E04 File Offset: 0x00073004
		public static void LoadClientOnlyData()
		{
			NKMEpisodeMgr.m_dicListEPTempletByCategory = (from episode in NKMEpisodeTempletV2.Values
			group episode by episode.m_EPCategory).ToDictionary((IGrouping<EPISODE_CATEGORY, NKMEpisodeTempletV2> group) => group.Key, (IGrouping<EPISODE_CATEGORY, NKMEpisodeTempletV2> group) => group.ToList<NKMEpisodeTempletV2>());
			NKMEpisodeMgr.m_dicStageTempletByBattleStrID = NKMTempletContainer<NKMStageTempletV2>.Values.ToDictionary((NKMStageTempletV2 stage) => stage.m_StageBattleStrID);
			NKMEpisodeMgr.EpisodeTemplets = NKMEpisodeTempletV2.Values.ToList<NKMEpisodeTempletV2>();
			NKMEpisodeMgr.UpdateUnlockInfoDateToIntervalTime();
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x00074EC4 File Offset: 0x000730C4
		public static void SortEpisodeTemplets()
		{
			foreach (KeyValuePair<EPISODE_CATEGORY, List<NKMEpisodeTempletV2>> keyValuePair in NKMEpisodeMgr.m_dicListEPTempletByCategory)
			{
				keyValuePair.Value.Sort(new Comparison<NKMEpisodeTempletV2>(NKMEpisodeMgr.<SortEpisodeTemplets>g__EpisodeComp|23_0));
			}
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x00074F28 File Offset: 0x00073128
		public static void UpdateUnlockInfoDateToIntervalTime()
		{
			foreach (NKMStageTempletV2 nkmstageTempletV in NKMTempletContainer<NKMStageTempletV2>.Values)
			{
				if (NKMEpisodeMgr.IsIntervalType(nkmstageTempletV.m_UnlockInfo.eReqType))
				{
					NKMIntervalTemplet nkmintervalTemplet = NKMIntervalTemplet.Find(nkmstageTempletV.m_UnlockInfo.reqValueStr);
					if (nkmintervalTemplet != null)
					{
						nkmstageTempletV.m_UnlockInfo = new UnlockInfo(nkmstageTempletV.m_UnlockInfo.eReqType, nkmstageTempletV.m_UnlockInfo.reqValue, nkmstageTempletV.m_UnlockInfo.reqValueStr, nkmintervalTemplet.GetStartDateUtc());
					}
				}
			}
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x00074FC8 File Offset: 0x000731C8
		private static bool IsIntervalType(STAGE_UNLOCK_REQ_TYPE type)
		{
			return type - STAGE_UNLOCK_REQ_TYPE.SURT_INTERVAL <= 3;
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x00074FD4 File Offset: 0x000731D4
		public static List<NKMEpisodeTempletV2> GetListNKMEpisodeTempletByCategory(EPISODE_CATEGORY category, bool bOnlyOpen = false, EPISODE_DIFFICULTY maxDifficulty = EPISODE_DIFFICULTY.NORMAL)
		{
			List<NKMEpisodeTempletV2> list;
			if (!NKMEpisodeMgr.m_dicListEPTempletByCategory.TryGetValue(category, out list))
			{
				return new List<NKMEpisodeTempletV2>();
			}
			if (bOnlyOpen)
			{
				return list.FindAll((NKMEpisodeTempletV2 v) => v.IsOpen && v.IsOpenedDayOfWeek() && v.m_Difficulty <= maxDifficulty);
			}
			return list.FindAll((NKMEpisodeTempletV2 v) => v.EnableByTag && v.m_Difficulty <= maxDifficulty);
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x0007502C File Offset: 0x0007322C
		public static float GetEPProgressPercent(NKMUserData cNKMUserData, NKMEpisodeTempletV2 episodeTemplet)
		{
			int epprogressTotalCount = NKMEpisodeMgr.GetEPProgressTotalCount(episodeTemplet);
			int epprogressClearCount = NKMEpisodeMgr.GetEPProgressClearCount(cNKMUserData, episodeTemplet);
			if (epprogressTotalCount <= 0)
			{
				return 0f;
			}
			float num = (float)epprogressClearCount;
			int num2 = 0;
			while (num2 < 3 && num2 < episodeTemplet.m_CompletionReward.Length && episodeTemplet.m_CompletionReward[num2] != null)
			{
				float num3 = (float)epprogressTotalCount * ((float)episodeTemplet.m_CompletionReward[num2].m_CompleteRate * 0.01f);
				if (epprogressClearCount < (int)num3)
				{
					break;
				}
				num = num3;
				num2++;
			}
			if ((float)epprogressClearCount < num)
			{
				return num / (float)epprogressTotalCount;
			}
			return (float)epprogressClearCount / (float)epprogressTotalCount;
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x000750A8 File Offset: 0x000732A8
		public static bool IsPossibleEpisode(NKMUserData cNKMUserData, NKMEpisodeTempletV2 episodeTemplet)
		{
			if (cNKMUserData == null || episodeTemplet == null)
			{
				return false;
			}
			if (!episodeTemplet.EnableByTag)
			{
				return false;
			}
			if (!episodeTemplet.IsOpen)
			{
				return false;
			}
			if (episodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_COUNTERCASE)
			{
				return !(episodeTemplet.m_EpisodeStrID == "NKM_DUNGEON_CC_SECRET");
			}
			NKMStageTempletV2 firstStage = episodeTemplet.GetFirstStage(1);
			if (firstStage == null)
			{
				return false;
			}
			if (!firstStage.EnableByTag)
			{
				return false;
			}
			EPISODE_CATEGORY epcategory = episodeTemplet.m_EPCategory;
			return (epcategory <= EPISODE_CATEGORY.EC_DAILY || epcategory - EPISODE_CATEGORY.EC_SIDESTORY <= 5) && NKMEpisodeMgr.CheckEpisodeMission(cNKMUserData, firstStage);
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x00075124 File Offset: 0x00073324
		public static bool IsPossibleEpisode(NKMUserData cNKMUserData, int episodeID, EPISODE_DIFFICULTY difficulty)
		{
			NKMEpisodeTempletV2 episodeTemplet = NKMEpisodeTempletV2.Find(episodeID, difficulty);
			return NKMEpisodeMgr.IsPossibleEpisode(cNKMUserData, episodeTemplet);
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x00075140 File Offset: 0x00073340
		public static bool CheckLockCounterCase(NKMUserData cNKMUserData, NKMEpisodeTempletV2 episodeTemplet, int actID)
		{
			if (episodeTemplet == null)
			{
				return true;
			}
			if (cNKMUserData == null)
			{
				return true;
			}
			for (int i = 0; i < episodeTemplet.m_DicStage[actID].Count; i++)
			{
				if (NKMEpisodeMgr.CheckEpisodeMission(cNKMUserData, episodeTemplet.m_DicStage[actID][i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x00075190 File Offset: 0x00073390
		public static bool CheckPossibleAct(NKMUserData cNKMUserData, int episodeID, int actID, EPISODE_DIFFICULTY difficulty = EPISODE_DIFFICULTY.NORMAL)
		{
			if (cNKMUserData == null)
			{
				return false;
			}
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(episodeID, difficulty);
			return nkmepisodeTempletV != null && nkmepisodeTempletV.GetFirstStage(actID) != null && NKMEpisodeMgr.CheckEpisodeMission(cNKMUserData, nkmepisodeTempletV.GetFirstStage(actID));
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x000751C8 File Offset: 0x000733C8
		public static bool CheckOpenedAct(int episodeID, int actID)
		{
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(episodeID, EPISODE_DIFFICULTY.NORMAL);
			return nkmepisodeTempletV != null && (nkmepisodeTempletV.m_DicStage.ContainsKey(actID) && nkmepisodeTempletV.m_DicStage[actID] != null && nkmepisodeTempletV.m_DicStage[actID].Count >= 1) && nkmepisodeTempletV.m_DicStage[actID][0].EnableByTag;
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x0007522C File Offset: 0x0007342C
		public static List<int> GetEPTabLstID(EPISODE_CATEGORY epCate)
		{
			List<int> result = new List<int>();
			if (NKMEpisodeMgr.m_dicEpisodeTabResource.ContainsKey(epCate))
			{
				result = NKMEpisodeMgr.m_dicEpisodeTabResource[epCate];
			}
			return result;
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x0007525C File Offset: 0x0007345C
		public static int GetUnitID(NKMEpisodeTempletV2 episodeTemplet, int actID)
		{
			if (episodeTemplet == null)
			{
				return 0;
			}
			if (episodeTemplet.m_DicStage != null && episodeTemplet.m_DicStage[actID].Count > 0)
			{
				return episodeTemplet.m_DicStage[actID][0].m_UnlockInfo.reqValue;
			}
			return 0;
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x000752A8 File Offset: 0x000734A8
		public static bool CheckClear(NKMUserData cNKMUserData, NKMStageTempletV2 stageTemplet)
		{
			if (stageTemplet == null || cNKMUserData == null)
			{
				return false;
			}
			switch (stageTemplet.m_STAGE_TYPE)
			{
			case STAGE_TYPE.ST_WARFARE:
				return cNKMUserData.CheckWarfareClear(stageTemplet.m_StageBattleStrID);
			case STAGE_TYPE.ST_DUNGEON:
				return cNKMUserData.CheckDungeonClear(stageTemplet.m_StageBattleStrID);
			case STAGE_TYPE.ST_PHASE:
				return NKCPhaseManager.CheckPhaseStageClear(stageTemplet);
			default:
				return false;
			}
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x000752FC File Offset: 0x000734FC
		public static bool GetMedalAllClear(NKMUserData userData, NKMStageTempletV2 stageTemplet)
		{
			switch (stageTemplet.m_STAGE_TYPE)
			{
			case STAGE_TYPE.ST_WARFARE:
			{
				if (stageTemplet.WarfareTemplet == null)
				{
					return false;
				}
				NKMWarfareClearData warfareClearData = userData.GetWarfareClearData(stageTemplet.WarfareTemplet.m_WarfareID);
				if (warfareClearData != null)
				{
					return warfareClearData.m_mission_result_1 && warfareClearData.m_mission_result_2 && warfareClearData.m_MissionRewardResult;
				}
				break;
			}
			case STAGE_TYPE.ST_DUNGEON:
			{
				if (stageTemplet.DungeonTempletBase == null)
				{
					return false;
				}
				NKMDungeonClearData dungeonClearData = userData.GetDungeonClearData(stageTemplet.DungeonTempletBase.m_DungeonID);
				if (dungeonClearData != null)
				{
					bool flag = true;
					if (stageTemplet.DungeonTempletBase.m_DGMissionType_1 != DUNGEON_GAME_MISSION_TYPE.DGMT_NONE)
					{
						flag &= dungeonClearData.missionResult1;
					}
					if (stageTemplet.DungeonTempletBase.m_DGMissionType_2 != DUNGEON_GAME_MISSION_TYPE.DGMT_NONE)
					{
						flag &= dungeonClearData.missionResult2;
					}
					return flag;
				}
				break;
			}
			case STAGE_TYPE.ST_PHASE:
			{
				if (stageTemplet.PhaseTemplet == null)
				{
					return false;
				}
				NKMPhaseClearData phaseClearData = NKCPhaseManager.GetPhaseClearData(stageTemplet.PhaseTemplet);
				if (phaseClearData != null)
				{
					bool flag2 = true;
					if (stageTemplet.PhaseTemplet.m_DGMissionType_1 != DUNGEON_GAME_MISSION_TYPE.DGMT_NONE)
					{
						flag2 &= phaseClearData.missionResult1;
					}
					if (stageTemplet.PhaseTemplet.m_DGMissionType_2 != DUNGEON_GAME_MISSION_TYPE.DGMT_NONE)
					{
						flag2 &= phaseClearData.missionResult2;
					}
					return flag2;
				}
				break;
			}
			}
			return false;
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x0007540A File Offset: 0x0007360A
		public static int GetEPProgressTotalCount(NKMEpisodeTempletV2 episodeTemplet)
		{
			if (episodeTemplet == null)
			{
				return 0;
			}
			return NKMEpisodeMgr.GetTotalMedalCount(episodeTemplet);
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x00075418 File Offset: 0x00073618
		public static int GetEPProgressClearCount(int episodeID)
		{
			int num = 0;
			NKMUserData cNKMUserData = NKCScenManager.CurrentUserData();
			for (int i = 0; i <= 1; i++)
			{
				NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(episodeID, (EPISODE_DIFFICULTY)i);
				if (nkmepisodeTempletV != null)
				{
					num += NKMEpisodeMgr.GetEPProgressClearCount(cNKMUserData, nkmepisodeTempletV);
				}
			}
			return num;
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x00075450 File Offset: 0x00073650
		public static int GetEPProgressClearCount(NKMUserData cNKMUserData, NKMEpisodeTempletV2 episodeTemplet)
		{
			int result = 0;
			NKMEpisodeCompleteData episodeCompleteData = cNKMUserData.GetEpisodeCompleteData(episodeTemplet.m_EpisodeID, episodeTemplet.m_Difficulty);
			if (episodeCompleteData != null)
			{
				result = episodeCompleteData.m_EpisodeCompleteCount;
			}
			return result;
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x00075480 File Offset: 0x00073680
		public static NKMStageTempletV2 FindStageTemplet(int episodeID, int actID, int stageIndex, EPISODE_DIFFICULTY episodeDifficulty = EPISODE_DIFFICULTY.NORMAL)
		{
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(episodeID, episodeDifficulty);
			if (nkmepisodeTempletV == null)
			{
				return null;
			}
			if (nkmepisodeTempletV.m_DicStage.Count <= actID)
			{
				return null;
			}
			if (stageIndex <= 0)
			{
				return null;
			}
			if (stageIndex <= nkmepisodeTempletV.m_DicStage[actID].Count)
			{
				return nkmepisodeTempletV.m_DicStage[actID][stageIndex - 1];
			}
			return null;
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x000754DC File Offset: 0x000736DC
		public static NKMStageTempletV2 FindStageTempletByBattleStrID(string missionStrID)
		{
			NKMStageTempletV2 result;
			NKMEpisodeMgr.m_dicStageTempletByBattleStrID.TryGetValue(missionStrID, out result);
			return result;
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x000754F8 File Offset: 0x000736F8
		public static string GetEpisodeBattleName(string battleStrID)
		{
			return NKMEpisodeMgr.GetEpisodeBattleName(NKMEpisodeMgr.FindStageTempletByBattleStrID(battleStrID));
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x00075505 File Offset: 0x00073705
		public static string GetEpisodeBattleName(int stageID)
		{
			return NKMEpisodeMgr.GetEpisodeBattleName(NKMStageTempletV2.Find(stageID));
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x00075514 File Offset: 0x00073714
		public static string GetEpisodeBattleName(NKMStageTempletV2 cNKMStageTemplet)
		{
			string result = "";
			if (cNKMStageTemplet == null)
			{
				return result;
			}
			switch (cNKMStageTemplet.m_STAGE_TYPE)
			{
			case STAGE_TYPE.ST_WARFARE:
			{
				NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(cNKMStageTemplet.m_StageBattleStrID);
				if (nkmwarfareTemplet != null)
				{
					result = nkmwarfareTemplet.GetWarfareName();
				}
				break;
			}
			case STAGE_TYPE.ST_DUNGEON:
			{
				NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(cNKMStageTemplet.m_StageBattleStrID);
				if (dungeonTempletBase != null)
				{
					result = dungeonTempletBase.GetDungeonName();
				}
				break;
			}
			case STAGE_TYPE.ST_PHASE:
			{
				NKMPhaseTemplet nkmphaseTemplet = NKMPhaseTemplet.Find(cNKMStageTemplet.m_StageBattleStrID);
				if (nkmphaseTemplet != null)
				{
					result = nkmphaseTemplet.GetName();
				}
				break;
			}
			}
			return result;
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x00075594 File Offset: 0x00073794
		public static int GetBattleID(NKMStageTempletV2 cNKMStageTemplet)
		{
			if (cNKMStageTemplet == null)
			{
				return 0;
			}
			switch (cNKMStageTemplet.m_STAGE_TYPE)
			{
			case STAGE_TYPE.ST_WARFARE:
			{
				NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(cNKMStageTemplet.m_StageBattleStrID);
				if (nkmwarfareTemplet != null)
				{
					return nkmwarfareTemplet.Key;
				}
				break;
			}
			case STAGE_TYPE.ST_DUNGEON:
			{
				NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(cNKMStageTemplet.m_StageBattleStrID);
				if (dungeonTempletBase != null)
				{
					return dungeonTempletBase.Key;
				}
				break;
			}
			case STAGE_TYPE.ST_PHASE:
			{
				NKMPhaseTemplet nkmphaseTemplet = NKMPhaseTemplet.Find(cNKMStageTemplet.m_StageBattleStrID);
				if (nkmphaseTemplet != null)
				{
					return nkmphaseTemplet.Key;
				}
				break;
			}
			}
			return 0;
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x00075606 File Offset: 0x00073806
		public static int GetDailyMissionEPId(NKCUIOPDailyMission.NKC_DAILY_MISSION_TYPE dailyMissionType)
		{
			switch (dailyMissionType)
			{
			case NKCUIOPDailyMission.NKC_DAILY_MISSION_TYPE.NDMT_ATTACK:
				return 101;
			case NKCUIOPDailyMission.NKC_DAILY_MISSION_TYPE.NDMT_DEFENSE:
				return 103;
			case NKCUIOPDailyMission.NKC_DAILY_MISSION_TYPE.NDMT_SEARCH:
				return 102;
			default:
				return 0;
			}
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x00075626 File Offset: 0x00073826
		public static int GetDailyMissionTicketID(int episodeID)
		{
			switch (episodeID)
			{
			case 101:
				return 15;
			case 102:
				return 17;
			case 103:
				return 16;
			default:
				return -1;
			}
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x00075649 File Offset: 0x00073849
		public static int GetDailyMissionTicketID(NKCUIOPDailyMission.NKC_DAILY_MISSION_TYPE dailyMissionType)
		{
			return NKMEpisodeMgr.GetDailyMissionTicketID(NKMEpisodeMgr.GetDailyMissionEPId(dailyMissionType));
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x00075656 File Offset: 0x00073856
		public static bool CheckEpisodeMission(NKMUserData cNKMUserData, NKMStageTempletV2 stageTemplet)
		{
			return stageTemplet.EnableByTag && NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, stageTemplet.m_UnlockInfo, false);
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x0007566F File Offset: 0x0007386F
		public static bool CheckEpisodeHasEventDrop(NKMEpisodeTempletV2 episodeTemplet)
		{
			return episodeTemplet != null && episodeTemplet.HaveEventDrop;
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x0007567C File Offset: 0x0007387C
		public static bool CheckStageHasEventDrop(NKMStageTempletV2 stageTemplet)
		{
			if (stageTemplet == null || stageTemplet.m_EventDrop == null)
			{
				return false;
			}
			bool result = false;
			int count = stageTemplet.m_EventDrop.Count;
			for (int i = 0; i < count; i++)
			{
				NKMRewardGroupTemplet rewardGroup = NKMRewardManager.GetRewardGroup(stageTemplet.m_EventDrop[i].Item1);
				if (rewardGroup != null)
				{
					int count2 = rewardGroup.List.Count;
					for (int j = 0; j < count2; j++)
					{
						if (rewardGroup.List[j].intervalTemplet.IsValidTime(NKCSynchronizedTime.ServiceTime))
						{
							stageTemplet.HaveEventDrop = true;
							result = true;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x00075710 File Offset: 0x00073910
		public static void SetUnlockedStage(int unlockedStageId)
		{
			if (!NKMEpisodeMgr.m_lstUnlockedStageIds.Contains(unlockedStageId))
			{
				NKMEpisodeMgr.m_lstUnlockedStageIds.Add(unlockedStageId);
			}
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x0007572A File Offset: 0x0007392A
		public static void SetUnlockedStage(List<int> lstStageIds)
		{
			NKMEpisodeMgr.m_lstUnlockedStageIds = lstStageIds;
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x00075732 File Offset: 0x00073932
		public static List<int> GetUnlockedStageIds()
		{
			return NKMEpisodeMgr.m_lstUnlockedStageIds;
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x00075739 File Offset: 0x00073939
		public static bool IsUnlockedStage(NKMStageTempletV2 stageTemplet)
		{
			return !stageTemplet.NeedToUnlock || NKMEpisodeMgr.m_lstUnlockedStageIds.Contains(stageTemplet.Key);
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x00075755 File Offset: 0x00073955
		public static void DoAfterLogOut()
		{
			NKMEpisodeMgr.m_lstUnlockedStageIds.Clear();
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x00075761 File Offset: 0x00073961
		public static bool HasHardDifficulty(int episodeID)
		{
			return NKMEpisodeTempletV2.Find(episodeID, EPISODE_DIFFICULTY.HARD) != null;
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x00075770 File Offset: 0x00073970
		public static bool HasEnoughResource(NKMStageTempletV2 stageTemplet, int m_LastMultiplyRewardCount = 1)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			int costItemID = 0;
			int num = 0;
			costItemID = stageTemplet.m_StageReqItemID;
			num = stageTemplet.m_StageReqItemCount;
			if (stageTemplet.m_StageReqItemID == 2)
			{
				NKCCompanyBuff.SetDiscountOfEterniumInEnteringDungeon(NKCScenManager.CurrentUserData().m_companyBuffDataList, ref num);
			}
			int realCostCount = num * m_LastMultiplyRewardCount;
			if (!myUserData.CheckPrice(realCostCount, costItemID))
			{
				if (!NKCAdManager.IsAdRewardItem(costItemID))
				{
					NKCShopManager.OpenItemLackPopup(costItemID, realCostCount);
				}
				else
				{
					NKCPopupItemLack.Instance.OpenItemLackAdRewardPopup(costItemID, delegate
					{
						NKCShopManager.OpenItemLackPopup(costItemID, realCostCount);
					});
				}
				return false;
			}
			return true;
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x00075820 File Offset: 0x00073A20
		public static bool IsClearedEpisode(NKMEpisodeTempletV2 epTemplet)
		{
			if (epTemplet == null)
			{
				return false;
			}
			if (!epTemplet.IsOpen)
			{
				return false;
			}
			if (epTemplet.m_DicStage == null || epTemplet.m_DicStage.Count == 0)
			{
				return false;
			}
			foreach (KeyValuePair<int, List<NKMStageTempletV2>> keyValuePair in epTemplet.m_DicStage)
			{
				for (int i = 0; i < keyValuePair.Value.Count; i++)
				{
					if (keyValuePair.Value[i].EnableByTag && !NKCScenManager.CurrentUserData().CheckStageCleared(keyValuePair.Value[i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x000758E0 File Offset: 0x00073AE0
		public static bool HasReddot(EPISODE_GROUP epGroup)
		{
			List<NKMEpisodeGroupTemplet> list = new List<NKMEpisodeGroupTemplet>();
			foreach (NKMEpisodeGroupTemplet nkmepisodeGroupTemplet in NKMTempletContainer<NKMEpisodeGroupTemplet>.Values)
			{
				if (nkmepisodeGroupTemplet.GroupCategory == epGroup)
				{
					list.Add(nkmepisodeGroupTemplet);
				}
			}
			for (int i = 0; i < list.Count; i++)
			{
				for (int j = 0; j < list[i].lstEpisodeTemplet.Count; j++)
				{
					if (NKMEpisodeMgr.HasReddot(list[i].lstEpisodeTemplet[j].m_EpisodeID))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x00075990 File Offset: 0x00073B90
		public static bool HasReddot(int episodeID, EPISODE_DIFFICULTY difficulty, int actID = 0)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			foreach (KeyValuePair<int, List<NKMStageTempletV2>> keyValuePair in NKMEpisodeTempletV2.Find(episodeID, difficulty).m_DicStage)
			{
				if (actID <= 0 || keyValuePair.Key == actID)
				{
					int num = 0;
					while (num < keyValuePair.Value.Count && NKMContentUnlockManager.IsContentUnlocked(nkmuserData, keyValuePair.Value[num].m_UnlockInfo, false))
					{
						if (!PlayerPrefs.HasKey(string.Format("{0}_{1}", nkmuserData.m_UserUID, keyValuePair.Value[num].m_StageBattleStrID)) && !nkmuserData.CheckStageCleared(keyValuePair.Value[num]))
						{
							return true;
						}
						num++;
					}
				}
			}
			return false;
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x00075A7C File Offset: 0x00073C7C
		public static bool HasReddot(int episodeID)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			for (int i = 0; i <= 1; i++)
			{
				NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(episodeID, (EPISODE_DIFFICULTY)i);
				if (nkmepisodeTempletV != null && NKMEpisodeMgr.IsPossibleEpisode(nkmuserData, nkmepisodeTempletV.m_EpisodeID, EPISODE_DIFFICULTY.NORMAL))
				{
					NKMUserData cNKMUserData = nkmuserData;
					UnlockInfo unlockInfo = nkmepisodeTempletV.GetUnlockInfo();
					if (NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, unlockInfo, false))
					{
						if (NKMEpisodeMgr.CanGetEpisodeCompleteReward(nkmuserData, nkmepisodeTempletV.m_EpisodeID) == NKM_ERROR_CODE.NEC_OK)
						{
							return true;
						}
						foreach (KeyValuePair<int, List<NKMStageTempletV2>> keyValuePair in nkmepisodeTempletV.m_DicStage)
						{
							for (int j = 0; j < keyValuePair.Value.Count; j++)
							{
								NKMStageTempletV2 nkmstageTempletV = keyValuePair.Value[j];
								if (!NKMContentUnlockManager.IsContentUnlocked(nkmuserData, nkmstageTempletV.m_UnlockInfo, false))
								{
									break;
								}
								if (!PlayerPrefs.HasKey(string.Format("{0}_{1}", nkmuserData.m_UserUID, nkmstageTempletV.m_StageBattleStrID)) && !nkmuserData.CheckStageCleared(nkmstageTempletV))
								{
									return true;
								}
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x00075B98 File Offset: 0x00073D98
		public static Dictionary<int, NKMStageTempletV2> GetFavoriteStageList()
		{
			return NKMEpisodeMgr.m_dicFavoriteStages;
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x00075B9F File Offset: 0x00073D9F
		public static void ClearFavoriteStage()
		{
			NKMEpisodeMgr.m_dicFavoriteStages.Clear();
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x00075BAC File Offset: 0x00073DAC
		public static void SetFavoriteStage(Dictionary<int, int> dicFavoritesStage)
		{
			NKMEpisodeMgr.m_dicFavoriteStages.Clear();
			if (dicFavoritesStage == null)
			{
				return;
			}
			foreach (KeyValuePair<int, int> keyValuePair in dicFavoritesStage)
			{
				NKMStageTempletV2 nkmstageTempletV = NKMStageTempletV2.Find(keyValuePair.Value);
				if (nkmstageTempletV == null)
				{
					Log.Error(string.Format("StageTemplet is null - Key : {0}", keyValuePair.Value), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMEpisodeMgrEx.cs", 846);
				}
				else if (!NKMEpisodeMgr.m_dicFavoriteStages.ContainsKey(keyValuePair.Key))
				{
					NKMEpisodeMgr.m_dicFavoriteStages.Add(keyValuePair.Key, nkmstageTempletV);
				}
				else
				{
					NKMEpisodeMgr.m_dicFavoriteStages[keyValuePair.Key] = nkmstageTempletV;
				}
			}
			if (NKCUIOperationNodeViewer.isOpen())
			{
				NKCUIOperationNodeViewer.Instance.RefreshFavoriteInfo();
			}
			if (NKCPopupFavorite.isOpen())
			{
				NKCPopupFavorite.Instance.RefreshList();
			}
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x00075C98 File Offset: 0x00073E98
		public static NKCEpisodeSummaryTemplet GetMainSummaryTemplet()
		{
			List<NKCEpisodeSummaryTemplet> list = new List<NKCEpisodeSummaryTemplet>();
			foreach (NKCEpisodeSummaryTemplet nkcepisodeSummaryTemplet in NKMTempletContainer<NKCEpisodeSummaryTemplet>.Values)
			{
				if (nkcepisodeSummaryTemplet.CheckEnable(ServiceTime.Recent) && nkcepisodeSummaryTemplet.EpisodeTemplet != null && nkcepisodeSummaryTemplet.EpisodeTemplet.IsOpen && nkcepisodeSummaryTemplet.ShowInPVE_01())
				{
					list.Add(nkcepisodeSummaryTemplet);
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			list.Sort(new Comparison<NKCEpisodeSummaryTemplet>(NKMEpisodeMgr.SortBySortIndex));
			for (int i = 0; i < list.Count; i++)
			{
				if (!string.IsNullOrEmpty(list[i].m_LobbyResourceID))
				{
					return list[i];
				}
			}
			return null;
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x00075D60 File Offset: 0x00073F60
		public static void BuildSummaryTemplet(out NKCEpisodeSummaryTemplet mainTemplet, out List<NKCEpisodeSummaryTemplet> lstTempletPVE_01, out List<NKCEpisodeSummaryTemplet> lstTempletPVE_02)
		{
			mainTemplet = null;
			lstTempletPVE_01 = new List<NKCEpisodeSummaryTemplet>();
			lstTempletPVE_02 = new List<NKCEpisodeSummaryTemplet>();
			foreach (NKCEpisodeSummaryTemplet nkcepisodeSummaryTemplet in NKMTempletContainer<NKCEpisodeSummaryTemplet>.Values)
			{
				if (nkcepisodeSummaryTemplet.CheckEnable(ServiceTime.Recent))
				{
					if (nkcepisodeSummaryTemplet.m_EPCategory == EPISODE_CATEGORY.EC_FIERCE)
					{
						lstTempletPVE_01.Add(nkcepisodeSummaryTemplet);
					}
					else if (nkcepisodeSummaryTemplet.EpisodeTemplet != null && nkcepisodeSummaryTemplet.EpisodeTemplet.IsOpen && nkcepisodeSummaryTemplet.CheckEpisodeEnable(ServiceTime.Recent))
					{
						if (nkcepisodeSummaryTemplet.ShowInPVE_01())
						{
							lstTempletPVE_01.Add(nkcepisodeSummaryTemplet);
						}
						else if (nkcepisodeSummaryTemplet.ShowInPVE_02())
						{
							lstTempletPVE_02.Add(nkcepisodeSummaryTemplet);
						}
					}
				}
			}
			lstTempletPVE_01.Sort(new Comparison<NKCEpisodeSummaryTemplet>(NKMEpisodeMgr.SortBySortIndex));
			lstTempletPVE_02.Sort(new Comparison<NKCEpisodeSummaryTemplet>(NKMEpisodeMgr.SortBySortIndex));
			for (int i = 0; i < lstTempletPVE_01.Count; i++)
			{
				if (!string.IsNullOrEmpty(lstTempletPVE_01[i].m_BigResourceID))
				{
					mainTemplet = lstTempletPVE_01[i];
					break;
				}
			}
			if (mainTemplet != null)
			{
				lstTempletPVE_01.Remove(mainTemplet);
			}
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x00075E84 File Offset: 0x00074084
		private static int SortBySortIndex(NKCEpisodeSummaryTemplet lItem, NKCEpisodeSummaryTemplet rItem)
		{
			return lItem.m_SortIndex.CompareTo(rItem.m_SortIndex);
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x00075ED8 File Offset: 0x000740D8
		[CompilerGenerated]
		internal static int <SortEpisodeTemplets>g__EpisodeComp|23_0(NKMEpisodeTempletV2 a, NKMEpisodeTempletV2 b)
		{
			int num = a.m_SortIndex.CompareTo(b.m_SortIndex);
			if (num != 0)
			{
				return num;
			}
			return a.m_EpisodeID.CompareTo(b.m_EpisodeID);
		}

		// Token: 0x040013CA RID: 5066
		private static readonly Dictionary<int, List<NKMStageTempletV2>> CounterCaseTemplets = new Dictionary<int, List<NKMStageTempletV2>>();

		// Token: 0x040013CB RID: 5067
		private static Dictionary<EPISODE_CATEGORY, List<int>> m_dicEpisodeTabResource = new Dictionary<EPISODE_CATEGORY, List<int>>();

		// Token: 0x040013CC RID: 5068
		private static Dictionary<EPISODE_CATEGORY, List<NKMEpisodeTempletV2>> m_dicListEPTempletByCategory = new Dictionary<EPISODE_CATEGORY, List<NKMEpisodeTempletV2>>();

		// Token: 0x040013CD RID: 5069
		private static Dictionary<string, NKMStageTempletV2> m_dicStageTempletByBattleStrID = new Dictionary<string, NKMStageTempletV2>();

		// Token: 0x040013CE RID: 5070
		private static List<int> m_lstUnlockedStageIds = new List<int>();

		// Token: 0x040013CF RID: 5071
		private static Dictionary<int, NKMStageTempletV2> m_dicFavoriteStages = new Dictionary<int, NKMStageTempletV2>();
	}
}
