using System;
using System.Collections.Generic;
using ClientPacket.Common;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x0200092C RID: 2348
	public class FirstAllClearData : StageRewardData
	{
		// Token: 0x06005DF5 RID: 24053 RVA: 0x001D078A File Offset: 0x001CE98A
		public FirstAllClearData(Transform slotParent) : base(slotParent)
		{
		}

		// Token: 0x06005DF6 RID: 24054 RVA: 0x001D0794 File Offset: 0x001CE994
		public override void CreateSlotData(NKMStageTempletV2 stageTemplet, NKMUserData cNKMUserData, List<int> listNRGI, List<NKCUISlot> listSlotToShow)
		{
			if (stageTemplet == null || cNKMUserData == null)
			{
				return;
			}
			bool completeMark = false;
			switch (stageTemplet.m_STAGE_TYPE)
			{
			case STAGE_TYPE.ST_WARFARE:
			{
				NKMWarfareTemplet warfareTemplet = stageTemplet.WarfareTemplet;
				if (warfareTemplet == null)
				{
					return;
				}
				NKMWarfareClearData warfareClearData = cNKMUserData.GetWarfareClearData(warfareTemplet.m_WarfareID);
				if (warfareClearData != null)
				{
					completeMark = (warfareClearData.m_mission_result_1 && warfareClearData.m_mission_result_2 && warfareClearData.m_MissionRewardResult);
				}
				break;
			}
			case STAGE_TYPE.ST_DUNGEON:
			{
				if (stageTemplet.DungeonTempletBase == null)
				{
					return;
				}
				NKMDungeonClearData dungeonClearData = cNKMUserData.GetDungeonClearData(stageTemplet.DungeonTempletBase.m_DungeonID);
				if (dungeonClearData != null)
				{
					completeMark = (dungeonClearData.missionResult1 && dungeonClearData.missionResult2);
				}
				break;
			}
			case STAGE_TYPE.ST_PHASE:
			{
				if (stageTemplet.PhaseTemplet == null)
				{
					return;
				}
				NKMPhaseClearData phaseClearData = NKCPhaseManager.GetPhaseClearData(stageTemplet.PhaseTemplet);
				if (phaseClearData != null)
				{
					completeMark = (phaseClearData.missionResult1 && phaseClearData.missionResult2);
				}
				break;
			}
			default:
				return;
			}
			bool flag = true;
			if (stageTemplet.MissionReward == null)
			{
				flag = false;
			}
			else
			{
				if (stageTemplet.MissionReward.rewardType == NKM_REWARD_TYPE.RT_NONE || stageTemplet.MissionReward.ID == 0)
				{
					flag = false;
				}
				if (!NKMRewardTemplet.IsOpenedReward(stageTemplet.MissionReward.rewardType, stageTemplet.MissionReward.ID, false))
				{
					flag = false;
				}
			}
			if (flag)
			{
				NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeRewardTypeData(stageTemplet.MissionReward.rewardType, stageTemplet.MissionReward.ID, stageTemplet.MissionReward.Count, 0);
				this.m_cSlot.SetData(data, true, null);
				this.m_cSlot.SetFirstAllClearMark(true);
				this.m_cSlot.SetCompleteMark(completeMark);
				listSlotToShow.Add(this.m_cSlot);
			}
		}
	}
}
