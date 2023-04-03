using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x0200092E RID: 2350
	public class MainReward : StageRewardData
	{
		// Token: 0x06005DF9 RID: 24057 RVA: 0x001D0A10 File Offset: 0x001CEC10
		public MainReward(Transform slotParent) : base(slotParent)
		{
		}

		// Token: 0x06005DFA RID: 24058 RVA: 0x001D0A1C File Offset: 0x001CEC1C
		public override void CreateSlotData(NKMStageTempletV2 stageTemplet, NKMUserData cNKMUserData, List<int> listNRGI, List<NKCUISlot> listSlotToShow)
		{
			if (stageTemplet == null || cNKMUserData == null)
			{
				return;
			}
			if (stageTemplet.MainRewardData == null)
			{
				return;
			}
			if (!NKCUtil.IsValidReward(stageTemplet.MainRewardData.rewardType, stageTemplet.MainRewardData.ID))
			{
				return;
			}
			if (!NKMRewardTemplet.IsOpenedReward(stageTemplet.MainRewardData.rewardType, stageTemplet.MainRewardData.ID, false))
			{
				return;
			}
			if (stageTemplet.MainRewardData.ID != 0 && stageTemplet.MainRewardData.MaxValue != 0)
			{
				NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeRewardTypeData(stageTemplet.MainRewardData.rewardType, stageTemplet.MainRewardData.ID, stageTemplet.MainRewardData.MinValue, 0);
				this.m_cSlot.SetData(data, true, null);
				this.m_cSlot.SetMainRewardMark(true);
				listSlotToShow.Add(this.m_cSlot);
			}
		}
	}
}
