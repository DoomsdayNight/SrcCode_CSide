using System;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A80 RID: 2688
	public class NKCPopupSelectionConfirmMisc : MonoBehaviour
	{
		// Token: 0x06007708 RID: 30472 RVA: 0x002797D0 File Offset: 0x002779D0
		public void SetData(int itemID, long count)
		{
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(itemID);
			if (itemMiscTempletByID == null)
			{
				return;
			}
			NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeMiscItemData(itemID, count, 0);
			this.m_slot.SetData(data, false, true, false, null);
			NKCUtil.SetLabelText(this.m_lbName, itemMiscTempletByID.GetItemName());
			NKCUtil.SetLabelText(this.m_lbHaveCount, NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(itemID).ToString("N0"));
			NKCUtil.SetLabelText(this.m_lbDesc, itemMiscTempletByID.GetItemDesc());
		}

		// Token: 0x04006385 RID: 25477
		public NKCUISlot m_slot;

		// Token: 0x04006386 RID: 25478
		public Text m_lbName;

		// Token: 0x04006387 RID: 25479
		public Text m_lbHaveCount;

		// Token: 0x04006388 RID: 25480
		public Text m_lbDesc;
	}
}
