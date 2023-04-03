using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000495 RID: 1173
	public class GameUnitData : ISerializable
	{
		// Token: 0x060020A1 RID: 8353 RVA: 0x000A6458 File Offset: 0x000A4658
		public GameUnitData()
		{
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x000A646C File Offset: 0x000A466C
		public GameUnitData(NKMUnitData unitData, NKMInventoryData inventoryData)
		{
			NKMUnitData copied = new NKMUnitData();
			copied.DeepCopyFrom(unitData);
			this.unit = copied;
			foreach (long itemUid in this.unit.GetValidEquipUids())
			{
				NKMEquipItemData itemEquip = inventoryData.GetItemEquip(itemUid);
				if (itemEquip != null)
				{
					NKMEquipItemData nkmequipItemData = new NKMEquipItemData();
					nkmequipItemData.DeepCopyFrom(itemEquip);
					this.equip_item_list.Add(nkmequipItemData);
				}
			}
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x000A6504 File Offset: 0x000A4704
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMUnitData>(ref this.unit);
			stream.PutOrGet<NKMEquipItemData>(ref this.equip_item_list);
		}

		// Token: 0x0400214C RID: 8524
		public NKMUnitData unit;

		// Token: 0x0400214D RID: 8525
		public List<NKMEquipItemData> equip_item_list = new List<NKMEquipItemData>();
	}
}
