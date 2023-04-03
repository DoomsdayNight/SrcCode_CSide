using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;

namespace NKC
{
	// Token: 0x020006F0 RID: 1776
	public class CompTemplet
	{
		// Token: 0x020013C8 RID: 5064
		public class CompNUTB : IComparer<NKMUnitTempletBase>
		{
			// Token: 0x0600A6B2 RID: 42674 RVA: 0x0034786C File Offset: 0x00345A6C
			public int Compare(NKMUnitTempletBase x, NKMUnitTempletBase y)
			{
				if (x == null)
				{
					return 1;
				}
				if (y == null)
				{
					return -1;
				}
				if (y.m_NKM_UNIT_GRADE > x.m_NKM_UNIT_GRADE)
				{
					return 1;
				}
				if (y.m_NKM_UNIT_GRADE < x.m_NKM_UNIT_GRADE)
				{
					return -1;
				}
				if (x.m_NKM_UNIT_STYLE_TYPE < y.m_NKM_UNIT_STYLE_TYPE)
				{
					return -1;
				}
				if (x.m_NKM_UNIT_STYLE_TYPE > y.m_NKM_UNIT_STYLE_TYPE)
				{
					return 1;
				}
				return x.m_UnitID.CompareTo(y.m_UnitID);
			}
		}

		// Token: 0x020013C9 RID: 5065
		public class CompNET : IComparer<NKMEquipTemplet>, IComparer<int>
		{
			// Token: 0x0600A6B4 RID: 42676 RVA: 0x003478DC File Offset: 0x00345ADC
			public int Compare(NKMEquipTemplet x, NKMEquipTemplet y)
			{
				if (x == null)
				{
					return 1;
				}
				if (y == null)
				{
					return -1;
				}
				if (x.m_EquipUnitStyleType == NKM_UNIT_STYLE_TYPE.NUST_ENCHANT && y.m_EquipUnitStyleType != NKM_UNIT_STYLE_TYPE.NUST_ENCHANT)
				{
					return -1;
				}
				if (x.m_EquipUnitStyleType != NKM_UNIT_STYLE_TYPE.NUST_ENCHANT && y.m_EquipUnitStyleType == NKM_UNIT_STYLE_TYPE.NUST_ENCHANT)
				{
					return 1;
				}
				if (x.m_NKM_ITEM_TIER > y.m_NKM_ITEM_TIER)
				{
					return -1;
				}
				if (x.m_NKM_ITEM_TIER < y.m_NKM_ITEM_TIER)
				{
					return 1;
				}
				if (y.m_NKM_ITEM_GRADE > x.m_NKM_ITEM_GRADE)
				{
					return 1;
				}
				if (x.m_NKM_ITEM_GRADE > y.m_NKM_ITEM_GRADE)
				{
					return -1;
				}
				if (x.m_EquipUnitStyleType < y.m_EquipUnitStyleType)
				{
					return -1;
				}
				if (x.m_EquipUnitStyleType > y.m_EquipUnitStyleType)
				{
					return 1;
				}
				if (x.m_ItemEquipPosition < y.m_ItemEquipPosition)
				{
					return -1;
				}
				if (x.m_ItemEquipPosition > y.m_ItemEquipPosition)
				{
					return 1;
				}
				return x.m_ItemEquipID.CompareTo(y.m_ItemEquipID);
			}

			// Token: 0x0600A6B5 RID: 42677 RVA: 0x003479B0 File Offset: 0x00345BB0
			public int Compare(int equipItem_A, int equipItem_B)
			{
				NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(equipItem_A);
				NKMEquipTemplet equipTemplet2 = NKMItemManager.GetEquipTemplet(equipItem_B);
				return this.Compare(equipTemplet, equipTemplet2);
			}
		}

		// Token: 0x020013CA RID: 5066
		public class CompNMT : IComparer<NKMItemMoldTemplet>
		{
			// Token: 0x0600A6B7 RID: 42679 RVA: 0x003479DC File Offset: 0x00345BDC
			public int Compare(NKMItemMoldTemplet x, NKMItemMoldTemplet y)
			{
				if (x == null)
				{
					return 1;
				}
				if (y == null)
				{
					return -1;
				}
				if (x.m_Tier > y.m_Tier)
				{
					return -1;
				}
				if (x.m_Tier < y.m_Tier)
				{
					return 1;
				}
				if (y.m_Grade > x.m_Grade)
				{
					return 1;
				}
				if (x.m_Grade > y.m_Grade)
				{
					return -1;
				}
				if (x.m_RewardEquipUnitType < y.m_RewardEquipUnitType)
				{
					return -1;
				}
				if (x.m_RewardEquipUnitType > y.m_RewardEquipUnitType)
				{
					return 1;
				}
				if (x.m_RewardEquipPosition < y.m_RewardEquipPosition)
				{
					return -1;
				}
				if (x.m_RewardEquipPosition > y.m_RewardEquipPosition)
				{
					return 1;
				}
				return x.Key.CompareTo(y.Key);
			}
		}

		// Token: 0x020013CB RID: 5067
		public class CompNIMT : IComparer<NKMItemMiscTemplet>
		{
			// Token: 0x0600A6B9 RID: 42681 RVA: 0x00347A90 File Offset: 0x00345C90
			public int Compare(NKMItemMiscTemplet x, NKMItemMiscTemplet y)
			{
				if (x == null)
				{
					return 1;
				}
				if (y == null)
				{
					return -1;
				}
				if (x.m_NKM_ITEM_GRADE > y.m_NKM_ITEM_GRADE)
				{
					return -1;
				}
				if (x.m_NKM_ITEM_GRADE < y.m_NKM_ITEM_GRADE)
				{
					return 1;
				}
				if (x.m_ItemMiscType < y.m_ItemMiscType)
				{
					return -1;
				}
				if (x.m_ItemMiscType > y.m_ItemMiscType)
				{
					return 1;
				}
				return x.m_ItemMiscID.CompareTo(y.m_ItemMiscID);
			}
		}
	}
}
