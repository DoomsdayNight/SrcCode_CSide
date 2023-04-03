using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Tooltip
{
	// Token: 0x02000B06 RID: 2822
	public class NKCUITooltipItem : NKCUITooltipBase
	{
		// Token: 0x06008050 RID: 32848 RVA: 0x002B4532 File Offset: 0x002B2732
		public override void Init()
		{
			this.m_slot.Init();
		}

		// Token: 0x06008051 RID: 32849 RVA: 0x002B4540 File Offset: 0x002B2740
		public override void SetData(NKCUITooltip.Data data)
		{
			NKCUITooltip.ItemData itemData = data as NKCUITooltip.ItemData;
			if (itemData == null)
			{
				Debug.LogError("Tooltip Item Data is null");
				return;
			}
			this.m_slot.SetData(itemData.Slot, false, false, false, null);
			this.m_type.text = NKCUtilString.GetSlotModeTypeString(itemData.Slot.eType, itemData.Slot.ID);
			this.m_name.text = NKCUISlot.GetName(itemData.Slot.eType, itemData.Slot.ID);
			string text;
			if (this.ShowAmount(itemData.Slot.eType, itemData.Slot.ID, out text))
			{
				this.m_amount.text = text;
			}
			else
			{
				this.m_amount.text = "";
			}
			if (itemData.Slot.eType != NKCUISlot.eSlotMode.Equip && itemData.Slot.eType != NKCUISlot.eSlotMode.EquipCount)
			{
				NKCUtil.SetGameobjectActive(this.m_lbPrivateEquip, false);
				return;
			}
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(itemData.Slot.ID);
			if (equipTemplet != null)
			{
				NKCUtil.SetGameobjectActive(this.m_lbPrivateEquip, true);
				NKCUtil.SetLabelText(this.m_lbPrivateEquip, NKCUtilString.GetEquipPositionStringByUnitStyle(equipTemplet, false));
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_lbPrivateEquip, false);
		}

		// Token: 0x06008052 RID: 32850 RVA: 0x002B466C File Offset: 0x002B286C
		private bool ShowAmount(NKCUISlot.eSlotMode type, int id, out string strAmount)
		{
			strAmount = "";
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return false;
			}
			NKMInventoryData inventoryData = myUserData.m_InventoryData;
			if (inventoryData == null)
			{
				return false;
			}
			if (type != NKCUISlot.eSlotMode.ItemMisc)
			{
				if (type == NKCUISlot.eSlotMode.Mold)
				{
					NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(id);
					if (itemMoldTempletByID == null)
					{
						return false;
					}
					List<int> list;
					if (NKMItemManager.m_dicRandomMoldBox.TryGetValue(itemMoldTempletByID.m_RewardGroupID, out list))
					{
						if (list == null || list.Count != 1)
						{
							return false;
						}
						int num = list[0];
						NKCRandomMoldBoxTemplet randomMoldBoxTemplet = NKMItemManager.GetRandomMoldBoxTemplet(num);
						if (randomMoldBoxTemplet == null)
						{
							return false;
						}
						if (randomMoldBoxTemplet.m_reward_type != NKM_REWARD_TYPE.RT_MISC)
						{
							return false;
						}
						NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(num);
						if (itemMiscTempletByID == null)
						{
							return false;
						}
						if (itemMiscTempletByID.m_ItemMiscType != NKM_ITEM_MISC_TYPE.IMT_VIEW)
						{
							long num2 = inventoryData.GetCountMiscItem(num);
							strAmount = itemMiscTempletByID.GetItemName() + " " + string.Format(NKCUtilString.GET_STRING_TOOLTIP_QUANTITY_ONE_PARAM, num2);
							return true;
						}
					}
				}
			}
			else
			{
				NKMItemMiscTemplet itemMiscTempletByID2 = NKMItemManager.GetItemMiscTempletByID(id);
				if (itemMiscTempletByID2 == null)
				{
					return false;
				}
				NKM_ITEM_MISC_TYPE itemMiscType = itemMiscTempletByID2.m_ItemMiscType;
				if (itemMiscType != NKM_ITEM_MISC_TYPE.IMT_VIEW)
				{
					long num2;
					if (itemMiscType == NKM_ITEM_MISC_TYPE.IMT_INTERIOR)
					{
						num2 = NKCScenManager.CurrentUserData().OfficeData.GetInteriorCount(id);
						strAmount = string.Format(NKCUtilString.GET_STRING_TOOLTIP_QUANTITY_ONE_PARAM, num2);
						return true;
					}
					num2 = inventoryData.GetCountMiscItem(id);
					strAmount = string.Format(NKCUtilString.GET_STRING_TOOLTIP_QUANTITY_ONE_PARAM, num2);
					return true;
				}
			}
			return false;
		}

		// Token: 0x04006C89 RID: 27785
		public NKCUISlot m_slot;

		// Token: 0x04006C8A RID: 27786
		public Text m_type;

		// Token: 0x04006C8B RID: 27787
		public Text m_name;

		// Token: 0x04006C8C RID: 27788
		public Text m_amount;

		// Token: 0x04006C8D RID: 27789
		public Text m_lbPrivateEquip;
	}
}
