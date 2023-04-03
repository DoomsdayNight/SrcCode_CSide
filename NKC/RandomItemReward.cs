using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x0200092F RID: 2351
	public class RandomItemReward : StageRewardData
	{
		// Token: 0x06005DFB RID: 24059 RVA: 0x001D0ADF File Offset: 0x001CECDF
		public RandomItemReward(Transform slotParent, NKM_REWARD_TYPE rewardType, int itemId, NKCUISlot.OnClick onSlotClick) : base(slotParent)
		{
			this.m_rewardType = rewardType;
			this.m_itemId = itemId;
			this.m_onSlotClick = onSlotClick;
		}

		// Token: 0x06005DFC RID: 24060 RVA: 0x001D0B00 File Offset: 0x001CED00
		public override void CreateSlotData(NKMStageTempletV2 stageTemplet, NKMUserData cNKMUserData, List<int> listNRGI, List<NKCUISlot> listSlotToShow)
		{
			if (stageTemplet == null || cNKMUserData == null)
			{
				return;
			}
			bool flag = NKCUtil.CheckExistRewardType(listNRGI, this.m_rewardType);
			int maxGradeInRewardGroups = NKCUtil.GetMaxGradeInRewardGroups(listNRGI, this.m_rewardType);
			if (flag)
			{
				NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeMiscItemData(new NKMItemMiscData(this.m_itemId, 1L, 0L, 0), 0);
				this.m_cSlot.SetData(data, false, false, true, this.m_onSlotClick);
				this.m_cSlot.SetBackGround(maxGradeInRewardGroups);
				if (this.m_onSlotClick == null)
				{
					this.m_cSlot.SetOpenItemBoxOnClick();
				}
				listSlotToShow.Add(this.m_cSlot);
			}
		}

		// Token: 0x06005DFD RID: 24061 RVA: 0x001D0B87 File Offset: 0x001CED87
		public override void Release()
		{
			base.Release();
			this.m_onSlotClick = null;
		}

		// Token: 0x04004A33 RID: 18995
		private NKM_REWARD_TYPE m_rewardType;

		// Token: 0x04004A34 RID: 18996
		private int m_itemId;

		// Token: 0x04004A35 RID: 18997
		private NKCUISlot.OnClick m_onSlotClick;
	}
}
