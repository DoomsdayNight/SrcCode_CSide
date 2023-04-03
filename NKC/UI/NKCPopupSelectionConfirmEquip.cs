using System;
using NKM;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x02000A7F RID: 2687
	public class NKCPopupSelectionConfirmEquip : MonoBehaviour
	{
		// Token: 0x06007706 RID: 30470 RVA: 0x00279798 File Offset: 0x00277998
		public void SetData(int equipID, int setItemID)
		{
			if (NKMItemManager.GetEquipTemplet(equipID) == null)
			{
				return;
			}
			NKMEquipItemData cNKMEquipItemData = NKCEquipSortSystem.MakeTempEquipData(equipID, setItemID, false);
			this.m_slot.SetData(cNKMEquipItemData, false, false);
		}

		// Token: 0x04006384 RID: 25476
		public NKCUIInvenEquipSlot m_slot;
	}
}
