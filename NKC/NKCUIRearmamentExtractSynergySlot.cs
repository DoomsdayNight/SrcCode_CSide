using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A9C RID: 2716
	public class NKCUIRearmamentExtractSynergySlot : MonoBehaviour
	{
		// Token: 0x06007863 RID: 30819 RVA: 0x0027F3BC File Offset: 0x0027D5BC
		public void SetData(int itemID, int itemCnt, int percent)
		{
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(itemID);
			NKMRewardInfo nkmrewardInfo = new NKMRewardInfo();
			nkmrewardInfo.rewardType = NKM_REWARD_TYPE.RT_MISC;
			nkmrewardInfo.ID = itemID;
			nkmrewardInfo.Count = itemCnt;
			this.m_Slot.SetData(NKCUISlot.SlotData.MakeRewardTypeData(nkmrewardInfo, 0), true, null);
			NKCUtil.SetLabelText(this.m_lbName, itemMiscTempletByID.GetItemName());
			NKCUtil.SetLabelText(this.m_lbCount, itemCnt.ToString());
			NKCUtil.SetLabelText(this.m_lbPercent, string.Format("{0}%", ((double)percent * 0.01).ToString("N2")));
		}

		// Token: 0x040064E9 RID: 25833
		public NKCUISlot m_Slot;

		// Token: 0x040064EA RID: 25834
		public Text m_lbName;

		// Token: 0x040064EB RID: 25835
		public Text m_lbCount;

		// Token: 0x040064EC RID: 25836
		public Text m_lbPercent;
	}
}
