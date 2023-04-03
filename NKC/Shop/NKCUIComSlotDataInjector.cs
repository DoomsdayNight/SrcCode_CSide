using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Shop
{
	// Token: 0x02000AD7 RID: 2775
	public class NKCUIComSlotDataInjector : MonoBehaviour
	{
		// Token: 0x06007C43 RID: 31811 RVA: 0x0029822C File Offset: 0x0029642C
		private void Start()
		{
			if (!this.m_bSetDataComplete)
			{
				this.SetData();
			}
		}

		// Token: 0x06007C44 RID: 31812 RVA: 0x0029823C File Offset: 0x0029643C
		public void SetData()
		{
			this.SetData(this.m_rewardType, this.m_rewardID, this.m_count, 0);
		}

		// Token: 0x06007C45 RID: 31813 RVA: 0x00298257 File Offset: 0x00296457
		public void SetData(NKMRewardInfo rewardInfo)
		{
			this.SetData(rewardInfo.rewardType, rewardInfo.ID, rewardInfo.Count, 0);
		}

		// Token: 0x06007C46 RID: 31814 RVA: 0x00298272 File Offset: 0x00296472
		public void SetData(NKMRandomBoxItemTemplet boxItemTemplet)
		{
			this.SetData(boxItemTemplet.m_reward_type, boxItemTemplet.m_RewardID, boxItemTemplet.FreeQuantity_Max, boxItemTemplet.PaidQuantity_Max);
		}

		// Token: 0x06007C47 RID: 31815 RVA: 0x00298294 File Offset: 0x00296494
		public void SetData(NKM_REWARD_TYPE rewardType, int rewardID, int freeCount, int paidCount)
		{
			int num = freeCount + paidCount;
			if (this.m_itemSlot != null)
			{
				this.m_itemSlot.Init();
				NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeRewardTypeData(rewardType, rewardID, num, 0);
				this.m_itemSlot.SetData(slotData, true, null);
				if (this.m_bClickAction)
				{
					this.m_itemSlot.SetOnClickAction(new NKCUISlot.SlotClickType[]
					{
						NKCUISlot.SlotClickType.RatioList,
						NKCUISlot.SlotClickType.ChoiceList,
						NKCUISlot.SlotClickType.BoxList,
						NKCUISlot.SlotClickType.MoldList,
						NKCUISlot.SlotClickType.ItemBox
					});
				}
				NKCShopManager.ShowShopItemCashCount(this.m_itemSlot, slotData, freeCount, paidCount);
			}
			string text = null;
			string text2 = null;
			string countString = this.GetCountString(freeCount, paidCount);
			switch (rewardType)
			{
			case NKM_REWARD_TYPE.RT_UNIT:
			case NKM_REWARD_TYPE.RT_SHIP:
			case NKM_REWARD_TYPE.RT_OPERATOR:
			{
				NKMUnitTempletBase nkmunitTempletBase = NKMUnitTempletBase.Find(rewardID);
				if (nkmunitTempletBase != null)
				{
					text = nkmunitTempletBase.GetUnitName();
					text2 = nkmunitTempletBase.GetUnitDesc();
					goto IL_1DE;
				}
				goto IL_1DE;
			}
			case NKM_REWARD_TYPE.RT_MISC:
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(rewardID);
				if (itemMiscTempletByID != null)
				{
					text = itemMiscTempletByID.GetItemName();
					text2 = itemMiscTempletByID.GetItemDesc();
					goto IL_1DE;
				}
				goto IL_1DE;
			}
			case NKM_REWARD_TYPE.RT_EQUIP:
			{
				NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(rewardID);
				if (equipTemplet != null)
				{
					text = equipTemplet.GetItemName();
					text2 = equipTemplet.GetItemDesc();
					goto IL_1DE;
				}
				goto IL_1DE;
			}
			case NKM_REWARD_TYPE.RT_SKIN:
			{
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(rewardID);
				if (skinTemplet != null)
				{
					text = skinTemplet.GetTitle();
					text2 = skinTemplet.GetSkinDesc();
					goto IL_1DE;
				}
				goto IL_1DE;
			}
			case NKM_REWARD_TYPE.RT_EMOTICON:
			{
				NKMEmoticonTemplet nkmemoticonTemplet = NKMEmoticonTemplet.Find(rewardID);
				if (nkmemoticonTemplet != null)
				{
					text = nkmemoticonTemplet.GetEmoticonName();
					text2 = nkmemoticonTemplet.GetEmoticonDesc();
					goto IL_1DE;
				}
				goto IL_1DE;
			}
			}
			NKCUtil.SetGameobjectActive(this.m_lbName, false);
			NKCUtil.SetGameobjectActive(this.m_lbDesc, false);
			NKCUtil.SetGameobjectActive(this.m_lbCount, true);
			if (!NKCShopManager.ShowShopItemCashCount(this.m_lbCount, freeCount, paidCount))
			{
				if (string.IsNullOrEmpty(this.CountFormat))
				{
					NKCUtil.SetLabelText(this.m_lbCount, num.ToString());
				}
				else
				{
					NKCUtil.SetLabelText(this.m_lbCount, string.Format(this.CountFormat, num));
				}
			}
			IL_1DE:
			NKCUtil.SetGameobjectActive(this.m_lbName, text != null);
			NKCUtil.SetGameobjectActive(this.m_lbDesc, text2 != null);
			NKCUtil.SetGameobjectActive(this.m_lbCount, countString != null);
			if (text != null)
			{
				NKCUtil.SetLabelText(this.m_lbName, text);
			}
			if (text2 != null)
			{
				NKCUtil.SetLabelText(this.m_lbDesc, text2);
			}
			if (countString != null)
			{
				NKCUtil.SetLabelText(this.m_lbCount, countString);
			}
			this.m_bSetDataComplete = true;
		}

		// Token: 0x06007C48 RID: 31816 RVA: 0x002984E0 File Offset: 0x002966E0
		private string GetCountString(int freeCount, int paidCount)
		{
			int num = freeCount + paidCount;
			if (NKCShopManager.UseSuperuserItemCount())
			{
				return NKCShopManager.GetItemCountString((long)freeCount, (long)paidCount);
			}
			if (string.IsNullOrEmpty(this.CountFormat))
			{
				return num.ToString();
			}
			return string.Format(this.CountFormat, num);
		}

		// Token: 0x04006911 RID: 26897
		[Header("UI")]
		public NKCUISlot m_itemSlot;

		// Token: 0x04006912 RID: 26898
		public Text m_lbName;

		// Token: 0x04006913 RID: 26899
		public Text m_lbDesc;

		// Token: 0x04006914 RID: 26900
		public Text m_lbCount;

		// Token: 0x04006915 RID: 26901
		[Header("UI 컨트롤")]
		public bool m_bClickAction = true;

		// Token: 0x04006916 RID: 26902
		public string CountFormat = "{0:n0}";

		// Token: 0x04006917 RID: 26903
		[Header("Data")]
		public NKM_REWARD_TYPE m_rewardType;

		// Token: 0x04006918 RID: 26904
		public int m_rewardID;

		// Token: 0x04006919 RID: 26905
		public int m_count;

		// Token: 0x0400691A RID: 26906
		private bool m_bSetDataComplete;
	}
}
