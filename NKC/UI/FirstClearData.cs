using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x0200092B RID: 2347
	public class FirstClearData : StageRewardData
	{
		// Token: 0x06005DF3 RID: 24051 RVA: 0x001D06FC File Offset: 0x001CE8FC
		public FirstClearData(Transform slotParent) : base(slotParent)
		{
		}

		// Token: 0x06005DF4 RID: 24052 RVA: 0x001D0708 File Offset: 0x001CE908
		public override void CreateSlotData(NKMStageTempletV2 stageTemplet, NKMUserData cNKMUserData, List<int> listNRGI, List<NKCUISlot> listSlotToShow)
		{
			if (stageTemplet == null || cNKMUserData == null)
			{
				return;
			}
			FirstRewardData firstRewardData = stageTemplet.GetFirstRewardData();
			bool completeMark = NKMEpisodeMgr.CheckClear(cNKMUserData, stageTemplet);
			if (firstRewardData != null && firstRewardData.Type != NKM_REWARD_TYPE.RT_NONE && firstRewardData.RewardId != 0)
			{
				NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeRewardTypeData(firstRewardData.Type, firstRewardData.RewardId, firstRewardData.RewardQuantity, 0);
				this.m_cSlot.SetData(data, true, null);
				this.m_cSlot.SetCompleteMark(completeMark);
				this.m_cSlot.SetFirstGetMark(true);
				listSlotToShow.Add(this.m_cSlot);
			}
		}
	}
}
