using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x0200092D RID: 2349
	public class OneTimeReward : StageRewardData
	{
		// Token: 0x06005DF7 RID: 24055 RVA: 0x001D0913 File Offset: 0x001CEB13
		public OneTimeReward(Transform slotParent, int index) : base(slotParent)
		{
			this.m_index = index;
		}

		// Token: 0x06005DF8 RID: 24056 RVA: 0x001D0924 File Offset: 0x001CEB24
		public override void CreateSlotData(NKMStageTempletV2 stageTemplet, NKMUserData cNKMUserData, List<int> listNRGI, List<NKCUISlot> listSlotToShow)
		{
			if (stageTemplet == null || cNKMUserData == null)
			{
				return;
			}
			bool completeMark;
			switch (stageTemplet.m_STAGE_TYPE)
			{
			case STAGE_TYPE.ST_WARFARE:
			{
				NKMWarfareTemplet warfareTemplet = stageTemplet.WarfareTemplet;
				if (warfareTemplet == null)
				{
					return;
				}
				completeMark = cNKMUserData.CheckWarfareOneTimeReward(warfareTemplet.m_WarfareID, this.m_index);
				break;
			}
			case STAGE_TYPE.ST_DUNGEON:
			{
				NKMDungeonTempletBase dungeonTempletBase = stageTemplet.DungeonTempletBase;
				if (dungeonTempletBase == null)
				{
					return;
				}
				completeMark = cNKMUserData.CheckDungeonOneTimeReward(dungeonTempletBase.m_DungeonID, this.m_index);
				break;
			}
			case STAGE_TYPE.ST_PHASE:
				completeMark = NKCPhaseManager.CheckOneTimeReward(stageTemplet, this.m_index);
				break;
			default:
				return;
			}
			NKMOnetimeRewardTemplet oneTimeReward = stageTemplet.GetOneTimeReward(this.m_index);
			NKMRewardInfo nkmrewardInfo = (oneTimeReward != null) ? oneTimeReward.GetRewardInfo() : null;
			if (nkmrewardInfo != null)
			{
				NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeRewardTypeData(nkmrewardInfo.rewardType, nkmrewardInfo.ID, nkmrewardInfo.Count, 0);
				this.m_cSlot.SetData(data, true, null);
				this.m_cSlot.SetCompleteMark(completeMark);
				this.m_cSlot.SetOnetimeMark(true);
				listSlotToShow.Add(this.m_cSlot);
			}
		}

		// Token: 0x04004A32 RID: 18994
		private int m_index;
	}
}
