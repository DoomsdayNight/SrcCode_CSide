using System;
using System.Collections.Generic;
using ClientPacket.Unit;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006E2 RID: 1762
	public static class NKCUnitMissionManager
	{
		// Token: 0x06003D7B RID: 15739 RVA: 0x0013C818 File Offset: 0x0013AA18
		public static void Init()
		{
			if (NKCUnitMissionManager.unitMissionStepTempletDic != null && NKCUnitMissionManager.unitMissionStepTempletDic.Count > 0)
			{
				return;
			}
			NKCUnitMissionManager.unitMissionStepTempletDic = new Dictionary<NKM_UNIT_GRADE, List<NKMUnitMissionStepTemplet>>();
			foreach (NKMUnitMissionTemplet nkmunitMissionTemplet in NKMTempletContainer<NKMUnitMissionTemplet>.Values)
			{
				if (!NKCUnitMissionManager.unitMissionStepTempletDic.ContainsKey(nkmunitMissionTemplet.UnitGrade))
				{
					NKCUnitMissionManager.unitMissionStepTempletDic.Add(nkmunitMissionTemplet.UnitGrade, new List<NKMUnitMissionStepTemplet>());
				}
				int count = nkmunitMissionTemplet.Steps.Count;
				for (int i = 0; i < count; i++)
				{
					NKCUnitMissionManager.unitMissionStepTempletDic[nkmunitMissionTemplet.UnitGrade].Add(nkmunitMissionTemplet.Steps[i]);
				}
			}
			foreach (KeyValuePair<NKM_UNIT_GRADE, List<NKMUnitMissionStepTemplet>> keyValuePair in NKCUnitMissionManager.unitMissionStepTempletDic)
			{
				keyValuePair.Value.Sort(delegate(NKMUnitMissionStepTemplet e1, NKMUnitMissionStepTemplet e2)
				{
					if (e1.StepId > e2.StepId)
					{
						return 1;
					}
					if (e1.StepId < e2.StepId)
					{
						return -1;
					}
					return 0;
				});
			}
		}

		// Token: 0x06003D7C RID: 15740 RVA: 0x0013C944 File Offset: 0x0013AB44
		public static void SetCompletedUnitMissionData(List<NKMUnitMissionData> completedUnitMissions)
		{
			NKCUnitMissionManager.completedUnitMissionList = completedUnitMissions;
		}

		// Token: 0x06003D7D RID: 15741 RVA: 0x0013C94C File Offset: 0x0013AB4C
		public static void SetRewardUnitMissionData(List<NKMUnitMissionData> rewardEnableUnitMissions)
		{
			NKCUnitMissionManager.rewardEnableUnitMissionList = rewardEnableUnitMissions;
		}

		// Token: 0x06003D7E RID: 15742 RVA: 0x0013C954 File Offset: 0x0013AB54
		public static void UpdateCompletedUnitMissionData(NKMUnitMissionData completedMissionData)
		{
			if (completedMissionData == null)
			{
				return;
			}
			if (NKCUnitMissionManager.GetCompletedUnitMissionData(completedMissionData.unitId, completedMissionData.missionId, completedMissionData.stepId) == null)
			{
				if (NKCUnitMissionManager.completedUnitMissionList == null)
				{
					NKCUnitMissionManager.completedUnitMissionList = new List<NKMUnitMissionData>();
				}
				NKCUnitMissionManager.completedUnitMissionList.Add(completedMissionData);
			}
			if (NKCUnitMissionManager.rewardEnableUnitMissionList != null)
			{
				int num = NKCUnitMissionManager.rewardEnableUnitMissionList.FindIndex((NKMUnitMissionData e) => e.missionId == completedMissionData.missionId && e.unitId == completedMissionData.unitId && e.stepId == completedMissionData.stepId);
				if (num >= 0 && num < NKCUnitMissionManager.rewardEnableUnitMissionList.Count)
				{
					NKCUnitMissionManager.rewardEnableUnitMissionList.RemoveAt(num);
				}
			}
		}

		// Token: 0x06003D7F RID: 15743 RVA: 0x0013C9FC File Offset: 0x0013ABFC
		public static void UpdateCompletedUnitMissionData(List<NKMUnitMissionData> missionDataList)
		{
			int count = missionDataList.Count;
			for (int i = 0; i < count; i++)
			{
				NKCUnitMissionManager.UpdateCompletedUnitMissionData(missionDataList[i]);
			}
		}

		// Token: 0x06003D80 RID: 15744 RVA: 0x0013CA28 File Offset: 0x0013AC28
		public static void UpdateRewardEnableMissionData(List<NKMUnitMissionData> missionDataList)
		{
			if (NKCUnitMissionManager.rewardEnableUnitMissionList == null)
			{
				NKCUnitMissionManager.rewardEnableUnitMissionList = missionDataList;
				return;
			}
			int count = missionDataList.Count;
			int i;
			Predicate<NKMUnitMissionData> <>9__0;
			int j;
			for (i = 0; i < count; i = j)
			{
				List<NKMUnitMissionData> list = NKCUnitMissionManager.rewardEnableUnitMissionList;
				Predicate<NKMUnitMissionData> match;
				if ((match = <>9__0) == null)
				{
					match = (<>9__0 = ((NKMUnitMissionData e) => e.unitId == missionDataList[i].unitId && e.missionId == missionDataList[i].missionId && e.stepId == missionDataList[i].stepId));
				}
				if (list.Find(match) == null)
				{
					NKCUnitMissionManager.rewardEnableUnitMissionList.Add(missionDataList[i]);
				}
				j = i + 1;
			}
		}

		// Token: 0x06003D81 RID: 15745 RVA: 0x0013CACC File Offset: 0x0013ACCC
		private static NKMUnitMissionData GetCompletedUnitMissionData(int unitId, int missionId, int stepId)
		{
			if (NKCUnitMissionManager.completedUnitMissionList == null)
			{
				return null;
			}
			return NKCUnitMissionManager.completedUnitMissionList.Find((NKMUnitMissionData e) => e.unitId == unitId && e.missionId == missionId && e.stepId == stepId);
		}

		// Token: 0x06003D82 RID: 15746 RVA: 0x0013CB14 File Offset: 0x0013AD14
		private static NKMUnitMissionData GetRewardEnableUnitMissionData(int unitId, int missionId, int stepId)
		{
			if (NKCUnitMissionManager.rewardEnableUnitMissionList == null)
			{
				return null;
			}
			return NKCUnitMissionManager.rewardEnableUnitMissionList.Find((NKMUnitMissionData e) => e.unitId == unitId && e.missionId == missionId && e.stepId == stepId);
		}

		// Token: 0x06003D83 RID: 15747 RVA: 0x0013CB5C File Offset: 0x0013AD5C
		public static void GetUnitMissionAchievedCount(int unitId, ref int total, ref int achieved)
		{
			if (NKMUnitManager.GetUnitTempletBase(unitId) == null)
			{
				return;
			}
			List<NKMUnitMissionStepTemplet> unitMissionStepTempletList = NKCUnitMissionManager.GetUnitMissionStepTempletList(unitId);
			int count = unitMissionStepTempletList.Count;
			total += count;
			for (int i = 0; i < count; i++)
			{
				if (NKCUnitMissionManager.GetCompletedUnitMissionData(unitId, unitMissionStepTempletList[i].Owner.MissionId, unitMissionStepTempletList[i].StepId) != null)
				{
					achieved++;
				}
			}
		}

		// Token: 0x06003D84 RID: 15748 RVA: 0x0013CBC0 File Offset: 0x0013ADC0
		public static void GetUnitMissionRewardEnableCount(int unitId, ref int total, ref int completed, ref int rewardEnable)
		{
			if (NKMUnitManager.GetUnitTempletBase(unitId) == null)
			{
				return;
			}
			List<NKMUnitMissionStepTemplet> unitMissionStepTempletList = NKCUnitMissionManager.GetUnitMissionStepTempletList(unitId);
			int count = unitMissionStepTempletList.Count;
			total += count;
			for (int i = 0; i < count; i++)
			{
				if (NKCUnitMissionManager.GetCompletedUnitMissionData(unitId, unitMissionStepTempletList[i].Owner.MissionId, unitMissionStepTempletList[i].StepId) != null)
				{
					completed++;
				}
				if (NKCUnitMissionManager.GetRewardEnableUnitMissionData(unitId, unitMissionStepTempletList[i].Owner.MissionId, unitMissionStepTempletList[i].StepId) != null)
				{
					rewardEnable++;
				}
			}
		}

		// Token: 0x06003D85 RID: 15749 RVA: 0x0013CC4C File Offset: 0x0013AE4C
		public static List<NKMUnitMissionStepTemplet> GetUnitMissionStepTempletList(int unitId)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitId);
			if (unitTempletBase == null)
			{
				return new List<NKMUnitMissionStepTemplet>();
			}
			if (NKCUnitMissionManager.unitMissionStepTempletDic == null || !NKCUnitMissionManager.unitMissionStepTempletDic.ContainsKey(unitTempletBase.m_NKM_UNIT_GRADE))
			{
				return new List<NKMUnitMissionStepTemplet>();
			}
			return NKCUnitMissionManager.unitMissionStepTempletDic[unitTempletBase.m_NKM_UNIT_GRADE];
		}

		// Token: 0x06003D86 RID: 15750 RVA: 0x0013CC98 File Offset: 0x0013AE98
		public static bool GetOpenTagCollectionMission()
		{
			return NKMOpenTagManager.IsOpened("TAG_COLLECTION_MISSION");
		}

		// Token: 0x06003D87 RID: 15751 RVA: 0x0013CCA4 File Offset: 0x0013AEA4
		public static bool GetOpenTagCollectionTeamUp()
		{
			return NKMOpenTagManager.IsOpened("TAG_COLLECTION_TEAMUP_REWARD");
		}

		// Token: 0x06003D88 RID: 15752 RVA: 0x0013CCB0 File Offset: 0x0013AEB0
		public static bool HasRewardEnableMission()
		{
			return NKCUnitMissionManager.GetOpenTagCollectionMission() && NKCUnitMissionManager.rewardEnableUnitMissionList != null && NKCUnitMissionManager.rewardEnableUnitMissionList.Count > 0;
		}

		// Token: 0x06003D89 RID: 15753 RVA: 0x0013CCD4 File Offset: 0x0013AED4
		public static bool HasRewardEnableMission(int unitId)
		{
			List<NKMUnitMissionStepTemplet> unitMissionStepTempletList = NKCUnitMissionManager.GetUnitMissionStepTempletList(unitId);
			int count = unitMissionStepTempletList.Count;
			for (int i = 0; i < count; i++)
			{
				if (NKCUnitMissionManager.GetRewardEnableUnitMissionData(unitId, unitMissionStepTempletList[i].Owner.MissionId, unitMissionStepTempletList[i].StepId) != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003D8A RID: 15754 RVA: 0x0013CD24 File Offset: 0x0013AF24
		public static int GetRewardEnableStepId(int unitId, int missionId)
		{
			if (NKCUnitMissionManager.rewardEnableUnitMissionList == null || NKCUnitMissionManager.rewardEnableUnitMissionList.Count <= 0)
			{
				return 0;
			}
			List<NKMUnitMissionData> list = NKCUnitMissionManager.rewardEnableUnitMissionList.FindAll((NKMUnitMissionData e) => e.unitId == unitId && e.missionId == missionId);
			if (list == null || list.Count <= 0)
			{
				return 0;
			}
			list.Sort(delegate(NKMUnitMissionData e1, NKMUnitMissionData e2)
			{
				if (e1.stepId > e2.stepId)
				{
					return 1;
				}
				if (e1.stepId < e2.stepId)
				{
					return -1;
				}
				return 0;
			});
			return list[0].stepId;
		}

		// Token: 0x06003D8B RID: 15755 RVA: 0x0013CDB4 File Offset: 0x0013AFB4
		public static NKMMissionManager.MissionStateData GetMissionState(int unitId, NKMUnitMissionStepTemplet unitMissionStepTemplet)
		{
			if (unitMissionStepTemplet == null)
			{
				return new NKMMissionManager.MissionStateData(NKMMissionManager.MissionState.LOCKED);
			}
			bool completedUnitMissionData = NKCUnitMissionManager.GetCompletedUnitMissionData(unitId, unitMissionStepTemplet.Owner.MissionId, unitMissionStepTemplet.StepId) != null;
			int num = 0;
			if (completedUnitMissionData)
			{
				num = unitMissionStepTemplet.MissionValue;
				return new NKMMissionManager.MissionStateData(NKMMissionManager.MissionState.COMPLETED, (long)num);
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			List<NKMUnitData> list = (nkmuserData != null) ? nkmuserData.m_ArmyData.GetUnitListByUnitID(unitId) : null;
			int count = list.Count;
			NKM_MISSION_COND missionCondition = unitMissionStepTemplet.Owner.MissionCondition;
			if (missionCondition != NKM_MISSION_COND.UNIT_GROWTH_LEVEL)
			{
				if (missionCondition != NKM_MISSION_COND.UNIT_GROWTH_LOYALTY)
				{
					if (missionCondition == NKM_MISSION_COND.UNIT_GROWTH_PERMANENT)
					{
						for (int i = 0; i < count; i++)
						{
							if (list[i].IsPermanentContract)
							{
								num = 1;
								break;
							}
						}
					}
				}
				else
				{
					for (int j = 0; j < count; j++)
					{
						if (num < list[j].loyalty)
						{
							num = list[j].loyalty;
						}
					}
				}
			}
			else
			{
				for (int k = 0; k < count; k++)
				{
					if (num < list[k].m_UnitLevel)
					{
						num = list[k].m_UnitLevel;
					}
				}
			}
			num = Mathf.Min(num, unitMissionStepTemplet.MissionValue);
			if (NKCUnitMissionManager.GetRewardEnableUnitMissionData(unitId, unitMissionStepTemplet.Owner.MissionId, unitMissionStepTemplet.StepId) != null)
			{
				return new NKMMissionManager.MissionStateData(NKMMissionManager.MissionState.CAN_COMPLETE, (long)unitMissionStepTemplet.MissionValue);
			}
			return new NKMMissionManager.MissionStateData(NKMMissionManager.MissionState.ONGOING, (long)num);
		}

		// Token: 0x040036E8 RID: 14056
		public static List<NKMUnitMissionData> completedUnitMissionList;

		// Token: 0x040036E9 RID: 14057
		public static List<NKMUnitMissionData> rewardEnableUnitMissionList;

		// Token: 0x040036EA RID: 14058
		private static Dictionary<NKM_UNIT_GRADE, List<NKMUnitMissionStepTemplet>> unitMissionStepTempletDic;
	}
}
