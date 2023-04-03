using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x02000930 RID: 2352
	public class MiscItemReward : StageRewardData
	{
		// Token: 0x06005DFE RID: 24062 RVA: 0x001D0B96 File Offset: 0x001CED96
		public MiscItemReward(Transform slotParent, int miscItemId) : base(slotParent)
		{
			this.m_miscItemId = miscItemId;
		}

		// Token: 0x06005DFF RID: 24063 RVA: 0x001D0BA8 File Offset: 0x001CEDA8
		public override void CreateSlotData(NKMStageTempletV2 stageTemplet, NKMUserData cNKMUserData, List<int> listNRGI, List<NKCUISlot> listSlotToShow)
		{
			if (stageTemplet == null || cNKMUserData == null)
			{
				return;
			}
			int num = 0;
			switch (stageTemplet.m_STAGE_TYPE)
			{
			case STAGE_TYPE.ST_WARFARE:
			{
				NKMWarfareTemplet warfareTemplet = stageTemplet.WarfareTemplet;
				if (warfareTemplet == null)
				{
					return;
				}
				int miscItemId = this.m_miscItemId;
				if (miscItemId != 1)
				{
					if (miscItemId != 2)
					{
						if (miscItemId == 501)
						{
							num = warfareTemplet.m_RewardUserEXP;
						}
					}
					else
					{
						num = warfareTemplet.m_RewardEternium_Min;
					}
				}
				else
				{
					num = warfareTemplet.m_RewardCredit_Min;
				}
				break;
			}
			case STAGE_TYPE.ST_DUNGEON:
			{
				NKMDungeonTempletBase dungeonTempletBase = stageTemplet.DungeonTempletBase;
				if (dungeonTempletBase == null)
				{
					return;
				}
				int miscItemId = this.m_miscItemId;
				switch (miscItemId)
				{
				case 1:
					num = dungeonTempletBase.m_RewardCredit_Min;
					break;
				case 2:
					num = dungeonTempletBase.m_RewardEternium_Min;
					break;
				case 3:
					num = dungeonTempletBase.m_RewardInformation_Min;
					break;
				default:
					if (miscItemId == 501)
					{
						num = dungeonTempletBase.m_RewardUserEXP;
					}
					break;
				}
				break;
			}
			case STAGE_TYPE.ST_PHASE:
			{
				if (stageTemplet.PhaseTemplet == null)
				{
					return;
				}
				int miscItemId = this.m_miscItemId;
				if (miscItemId != 1)
				{
					if (miscItemId == 501)
					{
						num = stageTemplet.PhaseTemplet.m_RewardUserEXP;
					}
				}
				else
				{
					num = stageTemplet.PhaseTemplet.m_RewardCredit_Min;
				}
				break;
			}
			default:
				return;
			}
			if (num > 0)
			{
				NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeMiscItemData(new NKMItemMiscData(this.m_miscItemId, (long)num, 0L, 0), 0);
				this.m_cSlot.SetData(data, false, false, true, null);
				this.m_cSlot.SetOpenItemBoxOnClick();
				listSlotToShow.Add(this.m_cSlot);
			}
		}

		// Token: 0x04004A36 RID: 18998
		private int m_miscItemId;
	}
}
