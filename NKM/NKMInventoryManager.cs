using System;

namespace NKM
{
	// Token: 0x02000423 RID: 1059
	public class NKMInventoryManager
	{
		// Token: 0x06001C94 RID: 7316 RVA: 0x00084D37 File Offset: 0x00082F37
		public static bool IsValidExpandType(NKM_INVENTORY_EXPAND_TYPE type)
		{
			return NKM_INVENTORY_EXPAND_TYPE.NIET_NONE <= type && type < NKM_INVENTORY_EXPAND_TYPE.NEIT_MAX;
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x00084D44 File Offset: 0x00082F44
		public static bool ExpandInventory(NKM_INVENTORY_EXPAND_TYPE type, NKMUserData userData, int count, out int maxEquipCount, out int maxUnitCount, out int maxShipCount, out int maxTrophyCount, out int expandedCount)
		{
			maxEquipCount = NKMInventoryManager.GetCurrentInventoryCount(NKM_INVENTORY_EXPAND_TYPE.NIET_EQUIP, userData);
			maxUnitCount = NKMInventoryManager.GetCurrentInventoryCount(NKM_INVENTORY_EXPAND_TYPE.NIET_UNIT, userData);
			maxShipCount = NKMInventoryManager.GetCurrentInventoryCount(NKM_INVENTORY_EXPAND_TYPE.NIET_SHIP, userData);
			maxTrophyCount = NKMInventoryManager.GetCurrentInventoryCount(NKM_INVENTORY_EXPAND_TYPE.NIET_TROPHY, userData);
			expandedCount = 0;
			int num;
			int num2;
			NKMInventoryManager.GetInventoryExpandData(type, out num, out num2);
			switch (type)
			{
			case NKM_INVENTORY_EXPAND_TYPE.NIET_EQUIP:
				maxEquipCount += num * count;
				expandedCount = maxEquipCount;
				break;
			case NKM_INVENTORY_EXPAND_TYPE.NIET_UNIT:
				maxUnitCount += num * count;
				expandedCount = maxUnitCount;
				break;
			case NKM_INVENTORY_EXPAND_TYPE.NIET_SHIP:
				maxShipCount += num * count;
				expandedCount = maxShipCount;
				break;
			case NKM_INVENTORY_EXPAND_TYPE.NIET_TROPHY:
				maxTrophyCount += num * count;
				expandedCount = maxTrophyCount;
				break;
			}
			return expandedCount <= num2;
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x00084DF0 File Offset: 0x00082FF0
		public static bool CanExpandInventory(NKM_INVENTORY_EXPAND_TYPE type, NKMUserData userData, int count, out int resultCount)
		{
			if (!NKMInventoryManager.IsValidExpandType(type))
			{
				resultCount = 0;
				return false;
			}
			resultCount = NKMInventoryManager.GetCurrentInventoryCount(type, userData);
			int num;
			int num2;
			NKMInventoryManager.GetInventoryExpandData(type, out num, out num2);
			resultCount += num * count;
			return resultCount <= num2;
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x00084E30 File Offset: 0x00083030
		public static bool CanExpandInventoryByAd(NKM_INVENTORY_EXPAND_TYPE type, NKMUserData userData, int count, out int resultCount)
		{
			if (!NKMInventoryManager.IsValidExpandType(type))
			{
				resultCount = 0;
				return false;
			}
			resultCount = NKMInventoryManager.GetCurrentInventoryCount(type, userData);
			int num;
			int num2;
			NKMInventoryManager.GetInventoryExpandData(type, out num, out num2);
			resultCount += count;
			return resultCount <= num2;
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x00084E6C File Offset: 0x0008306C
		public static int GetExpandInventoryCount(NKM_INVENTORY_EXPAND_TYPE type, NKMUserData userData, int MaxCount)
		{
			int currentInventoryCount = NKMInventoryManager.GetCurrentInventoryCount(type, userData);
			int num;
			int num2;
			NKMInventoryManager.GetInventoryExpandData(type, out num, out num2);
			return currentInventoryCount + num;
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x00084E8C File Offset: 0x0008308C
		private static int GetCurrentInventoryCount(NKM_INVENTORY_EXPAND_TYPE type, NKMUserData userData)
		{
			int result = 0;
			switch (type)
			{
			case NKM_INVENTORY_EXPAND_TYPE.NIET_EQUIP:
				result = userData.m_InventoryData.m_MaxItemEqipCount;
				break;
			case NKM_INVENTORY_EXPAND_TYPE.NIET_UNIT:
				result = userData.m_ArmyData.m_MaxUnitCount;
				break;
			case NKM_INVENTORY_EXPAND_TYPE.NIET_SHIP:
				result = userData.m_ArmyData.m_MaxShipCount;
				break;
			case NKM_INVENTORY_EXPAND_TYPE.NIET_OPERATOR:
				result = userData.m_ArmyData.m_MaxOperatorCount;
				break;
			case NKM_INVENTORY_EXPAND_TYPE.NIET_TROPHY:
				result = userData.m_ArmyData.m_MaxTrophyCount;
				break;
			}
			return result;
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x00084F00 File Offset: 0x00083100
		private static void GetInventoryExpandData(NKM_INVENTORY_EXPAND_TYPE type, out int expandCount, out int maxCount)
		{
			expandCount = 0;
			maxCount = 0;
			switch (type)
			{
			case NKM_INVENTORY_EXPAND_TYPE.NIET_EQUIP:
				expandCount = 5;
				maxCount = 2000;
				return;
			case NKM_INVENTORY_EXPAND_TYPE.NIET_UNIT:
				expandCount = 5;
				maxCount = 1100;
				return;
			case NKM_INVENTORY_EXPAND_TYPE.NIET_SHIP:
				expandCount = 1;
				maxCount = 60;
				return;
			case NKM_INVENTORY_EXPAND_TYPE.NIET_OPERATOR:
				expandCount = 5;
				maxCount = 500;
				return;
			case NKM_INVENTORY_EXPAND_TYPE.NIET_TROPHY:
				expandCount = 10;
				maxCount = 2000;
				return;
			default:
				return;
			}
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x00084F64 File Offset: 0x00083164
		public static void UpdateInventoryCount(NKM_INVENTORY_EXPAND_TYPE type, int count, NKMUserData userData)
		{
			switch (type)
			{
			case NKM_INVENTORY_EXPAND_TYPE.NIET_EQUIP:
				userData.m_InventoryData.m_MaxItemEqipCount = count;
				return;
			case NKM_INVENTORY_EXPAND_TYPE.NIET_UNIT:
				userData.m_ArmyData.m_MaxUnitCount = count;
				return;
			case NKM_INVENTORY_EXPAND_TYPE.NIET_SHIP:
				userData.m_ArmyData.m_MaxShipCount = count;
				return;
			case NKM_INVENTORY_EXPAND_TYPE.NIET_OPERATOR:
				userData.m_ArmyData.m_MaxOperatorCount = count;
				return;
			case NKM_INVENTORY_EXPAND_TYPE.NIET_TROPHY:
				userData.m_ArmyData.m_MaxTrophyCount = count;
				return;
			default:
				return;
			}
		}

		// Token: 0x04001BD2 RID: 7122
		public const int INVENTORY_EXPAND_ITEM_ID = 101;

		// Token: 0x04001BD3 RID: 7123
		public const int UNIT_EXPAND_ITEM_COUNT = 100;

		// Token: 0x04001BD4 RID: 7124
		public const int SHIP_EXPAND_ITEM_COUNT = 100;

		// Token: 0x04001BD5 RID: 7125
		public const int EQUIP_EXPAND_ITEM_COUNT = 50;

		// Token: 0x04001BD6 RID: 7126
		public const int OPERATOR_EXPAND_ITEM_COUNT = 100;

		// Token: 0x04001BD7 RID: 7127
		public const int TROPHY_EXPAND_ITEM_COUNT = 50;

		// Token: 0x04001BD8 RID: 7128
		public const int UNIT_EXPAND_COUNT = 5;

		// Token: 0x04001BD9 RID: 7129
		public const int SHIP_EXPAND_COUNT = 1;

		// Token: 0x04001BDA RID: 7130
		public const int EQUIP_EXPAND_COUNT = 5;

		// Token: 0x04001BDB RID: 7131
		public const int OPERATOR_EXPAND_COUNT = 5;

		// Token: 0x04001BDC RID: 7132
		public const int TROPHY_EXPAND_COUNT = 10;

		// Token: 0x04001BDD RID: 7133
		public const int MIN_UNIT_EXPAND_COUNT = 200;

		// Token: 0x04001BDE RID: 7134
		public const int MIN_SHIP_EXPAND_COUNT = 10;

		// Token: 0x04001BDF RID: 7135
		public const int MIN_EQUIP_EXPAND_COUNT = 300;

		// Token: 0x04001BE0 RID: 7136
		public const int MIN_OPERATOR_EXPAND_COUNT = 300;

		// Token: 0x04001BE1 RID: 7137
		public const int MIN_TROPHY_EXPAND_COUNT = 2000;

		// Token: 0x04001BE2 RID: 7138
		public const int MAX_UNIT_EXPAND_COUNT = 1100;

		// Token: 0x04001BE3 RID: 7139
		public const int MAX_SHIP_EXPAND_COUNT = 60;

		// Token: 0x04001BE4 RID: 7140
		public const int MAX_EQUIP_EXPAND_COUNT = 2000;

		// Token: 0x04001BE5 RID: 7141
		public const int MAX_OPERATOR_EXPAND_COUNT = 500;

		// Token: 0x04001BE6 RID: 7142
		public const int MAX_TROPHY_EXPAND_COUNT = 2000;
	}
}
