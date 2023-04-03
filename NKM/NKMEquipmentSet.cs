using System;

namespace NKM
{
	// Token: 0x020004F5 RID: 1269
	public sealed class NKMEquipmentSet
	{
		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x060023CE RID: 9166 RVA: 0x000BAB0F File Offset: 0x000B8D0F
		public long WeaponUid
		{
			get
			{
				if (this.Weapon == null)
				{
					return 0L;
				}
				return this.Weapon.m_ItemUid;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x060023CF RID: 9167 RVA: 0x000BAB27 File Offset: 0x000B8D27
		public long DefenceUid
		{
			get
			{
				if (this.Defence == null)
				{
					return 0L;
				}
				return this.Defence.m_ItemUid;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x060023D0 RID: 9168 RVA: 0x000BAB3F File Offset: 0x000B8D3F
		public long AccessoryUid
		{
			get
			{
				if (this.Accessory == null)
				{
					return 0L;
				}
				return this.Accessory.m_ItemUid;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x060023D1 RID: 9169 RVA: 0x000BAB57 File Offset: 0x000B8D57
		public long Accessory2Uid
		{
			get
			{
				if (this.Accessory2 == null)
				{
					return 0L;
				}
				return this.Accessory2.m_ItemUid;
			}
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x000BAB6F File Offset: 0x000B8D6F
		public NKMEquipmentSet(NKMEquipItemData weapon, NKMEquipItemData defence, NKMEquipItemData accessory, NKMEquipItemData accessory2)
		{
			this.Weapon = weapon;
			this.Defence = defence;
			this.Accessory = accessory;
			this.Accessory2 = accessory2;
		}

		// Token: 0x040025B1 RID: 9649
		public readonly NKMEquipItemData Weapon;

		// Token: 0x040025B2 RID: 9650
		public readonly NKMEquipItemData Defence;

		// Token: 0x040025B3 RID: 9651
		public readonly NKMEquipItemData Accessory;

		// Token: 0x040025B4 RID: 9652
		public readonly NKMEquipItemData Accessory2;
	}
}
