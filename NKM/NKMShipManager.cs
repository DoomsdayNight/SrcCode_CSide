using System;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.Mode;
using Cs.Logging;
using NKC;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200046D RID: 1133
	public class NKMShipManager
	{
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06001EC7 RID: 7879 RVA: 0x00092400 File Offset: 0x00090600
		public static Dictionary<int, NKMShipBuildTemplet> DicNKMShipBuildTemplet
		{
			get
			{
				return NKMShipManager.m_DicNKMShipBuildTemplet;
			}
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x00092407 File Offset: 0x00090607
		public static NKMShipBuildTemplet GetShipBuildTemplet(int ship_id)
		{
			return NKMTempletContainer<NKMShipBuildTemplet>.Find(ship_id);
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x0009240F File Offset: 0x0009060F
		public static NKMShipLevelUpTemplet GetShipLevelupTemplet(int ship_star_grade, int limitBreakeLevel)
		{
			return NKMShipLevelUpTemplet.Find(ship_star_grade, NKM_UNIT_GRADE.NUG_N, limitBreakeLevel);
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x00092419 File Offset: 0x00090619
		public static NKMShipLevelUpTemplet GetShipLevelupTempletByLevel(int level, NKM_UNIT_GRADE unitGrade = NKM_UNIT_GRADE.NUG_N, int ShipLimitBreakGrade = 0)
		{
			return NKMShipLevelUpTemplet.GetShipLevelupTempletByLevel(level, unitGrade, ShipLimitBreakGrade);
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x00092424 File Offset: 0x00090624
		public static NKM_ERROR_CODE CanShipLevelup(NKMUserData userData, NKMUnitData shipData, int nextLevel)
		{
			if (shipData == null || userData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_NOT_EXIST;
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = NKMUnitManager.IsUnitBusy(userData, shipData, false);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				return nkm_ERROR_CODE;
			}
			if (NKMShipLevelUpTemplet.GetMaxLevel(shipData.GetStarGrade(), shipData.GetUnitGrade(), (int)shipData.m_LimitBreakLevel) < nextLevel)
			{
				return NKM_ERROR_CODE.NEC_FAIL_SHIP_MAX_LEVEL;
			}
			foreach (KeyValuePair<int, int> keyValuePair in NKMShipManager.GetMaterialListInLevelup(shipData.m_UnitID, shipData.m_UnitLevel, nextLevel, (int)shipData.m_LimitBreakLevel))
			{
				if (userData.m_InventoryData.GetCountMiscItem(keyValuePair.Key) < (long)keyValuePair.Value)
				{
					return NKM_ERROR_CODE.NEC_FAIL_INVALID_ITEM_ID;
				}
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x000924E4 File Offset: 0x000906E4
		public static NKM_ERROR_CODE CanShipUpgrade(NKMUserData user_data, NKMUnitData ship_data, int next_ship_id)
		{
			if (NKMUnitManager.GetUnitTempletBase(next_ship_id) == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_SHIP_INVALID_SHIP_ID;
			}
			if (ship_data == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_SHIP_INVALID_SHIP_UID;
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = NKMUnitManager.IsUnitBusy(user_data, ship_data, false);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				return nkm_ERROR_CODE;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(ship_data.m_UnitID);
			if (unitTempletBase == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_GET_UNIT_BASE_TEMPLET_NULL;
			}
			ship_data.GetStarGrade(unitTempletBase);
			int maxLevel = NKMShipLevelUpTemplet.GetMaxLevel(ship_data.GetStarGrade(), ship_data.GetUnitGrade(), (int)ship_data.m_LimitBreakLevel);
			if (ship_data.m_UnitLevel < maxLevel)
			{
				return NKM_ERROR_CODE.NEC_FAIL_SHIP_REMODEL_NOT_ENOUGH_LEVEL;
			}
			NKMShipBuildTemplet shipBuildTemplet = NKMShipManager.GetShipBuildTemplet(ship_data.m_UnitID);
			if (shipBuildTemplet == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_SHIP_INVALID_SHIP_ID;
			}
			if (shipBuildTemplet.ShipUpgradeTarget1 != next_ship_id && shipBuildTemplet.ShipUpgradeTarget2 != next_ship_id)
			{
				return NKM_ERROR_CODE.NEC_FAIL_SHIP_INVALID_SHIP_ID;
			}
			NKMShipBuildTemplet shipBuildTemplet2 = NKMShipManager.GetShipBuildTemplet(next_ship_id);
			if (shipBuildTemplet2 == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_SHIP_INVALID_SHIP_ID;
			}
			NKMInventoryData inventoryData = user_data.m_InventoryData;
			if (inventoryData.GetCountMiscItem(1) < (long)shipBuildTemplet2.ShipUpgradeCredit)
			{
				return NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_ITEM;
			}
			foreach (UpgradeMaterial upgradeMaterial in shipBuildTemplet2.UpgradeMaterialList)
			{
				if (inventoryData.GetCountMiscItem(upgradeMaterial.m_ShipUpgradeMaterial) < (long)upgradeMaterial.m_ShipUpgradeMaterialCount)
				{
					return NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_ITEM;
				}
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x0009261C File Offset: 0x0009081C
		public static NKM_ERROR_CODE CanShipDivision(NKMUserData user_data, NKMUnitData ship_data)
		{
			if (ship_data == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_SHIP_INVALID_SHIP_UID;
			}
			if (NKMConst.Ship.CoffinIds.Contains(ship_data.m_UnitID))
			{
				return NKM_ERROR_CODE.NEC_FAIL_SHIP_INVALID_SHIP_ID;
			}
			if (user_data.m_ArmyData.GetDeckDataByShipUID(ship_data.m_UnitUID) != null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_IN_DECK;
			}
			if (ship_data.m_bLock)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_LOCKED;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001ECE RID: 7886 RVA: 0x00092674 File Offset: 0x00090874
		public static Dictionary<int, int> GetMaterialListInLevelup(int shipId, int startLevel, int endLevel, int limitBreakLevel = 0)
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(shipId);
			if (unitTempletBase == null)
			{
				return dictionary;
			}
			int num = startLevel;
			while (num < endLevel && startLevel != endLevel)
			{
				NKMShipLevelUpTemplet shipLevelupTempletByLevel = NKMShipManager.GetShipLevelupTempletByLevel(num, unitTempletBase.m_NKM_UNIT_GRADE, limitBreakLevel);
				foreach (LevelupMaterial levelupMaterial in shipLevelupTempletByLevel.ShipLevelupMaterialList)
				{
					int num2;
					if (!dictionary.TryGetValue(levelupMaterial.m_LevelupMaterialItemID, out num2))
					{
						dictionary.Add(levelupMaterial.m_LevelupMaterialItemID, levelupMaterial.m_LevelupMaterialCount);
					}
					else
					{
						dictionary[levelupMaterial.m_LevelupMaterialItemID] = num2 + levelupMaterial.m_LevelupMaterialCount;
					}
				}
				int num3;
				if (!dictionary.TryGetValue(1, out num3))
				{
					dictionary.Add(1, shipLevelupTempletByLevel.ShipUpgradeCredit);
				}
				else
				{
					dictionary[1] = num3 + shipLevelupTempletByLevel.ShipUpgradeCredit;
				}
				num++;
			}
			return dictionary;
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x00092764 File Offset: 0x00090964
		public static bool LoadFromLUA()
		{
			return true;
		}

		// Token: 0x06001ED0 RID: 7888 RVA: 0x00092767 File Offset: 0x00090967
		public static bool IsSameKindShip(int shipID, int targetShipID)
		{
			return shipID == targetShipID || ((shipID >= 10000 || targetShipID >= 10000) && shipID % 1000 == targetShipID % 1000);
		}

		// Token: 0x06001ED1 RID: 7889 RVA: 0x00092794 File Offset: 0x00090994
		public static bool CheckShipLevel(NKMUserData userData, int shipID, int targetLevel)
		{
			foreach (NKMUnitData nkmunitData in userData.m_ArmyData.m_dicMyShip.Values)
			{
				if (NKMShipManager.IsSameKindShip(shipID, nkmunitData.m_UnitID) && nkmunitData.m_UnitLevel >= targetLevel)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x00092808 File Offset: 0x00090A08
		public static NKMShipLimitBreakTemplet GetShipLimitBreakTemplet(int shipId, int nextLimitBreakLevel)
		{
			if (NKMOpenTagManager.IsOpened("SHIP_LIMITBREAK"))
			{
				return NKMTempletContainer<NKMShipLimitBreakTemplet>.Find((NKMShipLimitBreakTemplet x) => x.ShipId == shipId && x.ShipLimitBreakGrade == nextLimitBreakLevel);
			}
			return null;
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x00092848 File Offset: 0x00090A48
		public static bool IsModuleUnlocked(NKMUnitData shipData)
		{
			return shipData != null && NKMOpenTagManager.IsOpened("SHIP_LIMITBREAK") && NKMOpenTagManager.IsOpened("SHIP_COMMANDMODULE") && shipData.m_LimitBreakLevel > 0;
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x00092878 File Offset: 0x00090A78
		public static NKMUnitTempletBase GetMaxGradeShipTemplet(NKMUnitTempletBase curShipTempletBase)
		{
			int num = curShipTempletBase.m_UnitID;
			for (;;)
			{
				NKMShipBuildTemplet shipBuildTemplet = NKMShipManager.GetShipBuildTemplet(num);
				if (shipBuildTemplet.ShipUpgradeTarget1 <= 0)
				{
					break;
				}
				num = shipBuildTemplet.ShipUpgradeTarget1;
			}
			return NKMUnitManager.GetUnitTempletBase(num);
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x000928AC File Offset: 0x00090AAC
		public static NKM_ERROR_CODE CanModuleOptionChange(NKMUnitData shipData)
		{
			if (shipData.IsSeized)
			{
				return NKM_ERROR_CODE.NEC_FAIL_SHIP_IS_SEIZED;
			}
			NKMDeckData deckDataByShipUID = NKCScenManager.CurrentUserData().m_ArmyData.GetDeckDataByShipUID(shipData.m_UnitUID);
			if (!NKCUtil.IsNullObject<NKMDeckData>(deckDataByShipUID, ""))
			{
				NKM_DECK_STATE state = deckDataByShipUID.GetState();
				if (state == NKM_DECK_STATE.DECK_STATE_WARFARE)
				{
					return NKM_ERROR_CODE.NEC_FAIL_WARFARE_DOING;
				}
				if (state == NKM_DECK_STATE.DECK_STATE_DIVE)
				{
					return NKM_ERROR_CODE.NEC_FAIL_DIVE_DOING;
				}
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x00092908 File Offset: 0x00090B08
		public static NKM_ERROR_CODE CanShipLimitBreak(NKMUserData user_data, NKMUnitData ship_data, long costShipUID)
		{
			if (ship_data == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_SHIP_NOT_EXISTS;
			}
			if (costShipUID < 0L)
			{
				return NKM_ERROR_CODE.NEC_FAIL_SHIP_LIMITBREAK_INVALID_CONSUMED_SHIP_ID;
			}
			if (NKMUnitManager.GetUnitTempletBase(ship_data.m_UnitID) == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_GET_UNIT_BASE_TEMPLET_NULL;
			}
			int maxLevel = NKMShipLevelUpTemplet.GetMaxLevel(ship_data.GetStarGrade(), ship_data.GetUnitGrade(), (int)ship_data.m_LimitBreakLevel);
			if (ship_data.m_UnitLevel < maxLevel)
			{
				return NKM_ERROR_CODE.NEC_FAIL_SHIP_REMODEL_NOT_ENOUGH_LEVEL;
			}
			NKMUnitData shipFromUID = user_data.m_ArmyData.GetShipFromUID(costShipUID);
			if (shipFromUID == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_SHIP_LIMITBREAK_INVALID_CONSUMED_SHIP_ID;
			}
			if (shipFromUID.m_bLock)
			{
				return NKM_ERROR_CODE.NEC_FAIL_SHIP_LIMITBREAK_LOCKED_CONSUMED_SHIP;
			}
			NKMShipLimitBreakTemplet shipLimitBreakTemplet = NKMShipManager.GetShipLimitBreakTemplet(ship_data.m_UnitID, (int)(ship_data.m_LimitBreakLevel + 1));
			if (shipLimitBreakTemplet == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_SHIP_LIMITBREAK_TEMPLET;
			}
			if (!shipLimitBreakTemplet.ListMaterialShipId.Contains(shipFromUID.m_UnitID))
			{
				return NKM_ERROR_CODE.NEC_FAIL_SHIP_LIMITBREAK_INVALID_CONSUMED_SHIP_ID;
			}
			for (int i = 0; i < shipLimitBreakTemplet.ShipLimitBreakItems.Count; i++)
			{
				if (user_data.m_InventoryData.GetCountMiscItem(shipLimitBreakTemplet.ShipLimitBreakItems[i].ItemId) < shipLimitBreakTemplet.ShipLimitBreakItems[i].Count)
				{
					return NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_ITEM;
				}
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x00092A08 File Offset: 0x00090C08
		public static bool IsMaxLimitBreak(NKMUnitData unitData)
		{
			IEnumerable<NKMShipLimitBreakTemplet> enumerable = from x in NKMTempletContainer<NKMShipLimitBreakTemplet>.Values
			where x.ShipId == unitData.m_UnitID
			select x;
			return enumerable != null && enumerable.Count<NKMShipLimitBreakTemplet>() > 0 && (int)unitData.m_LimitBreakLevel >= enumerable.Count<NKMShipLimitBreakTemplet>();
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x00092A60 File Offset: 0x00090C60
		public static bool IsMaxLimitBreak(int shipId, int limitBreakCount)
		{
			IEnumerable<NKMShipLimitBreakTemplet> enumerable = from x in NKMTempletContainer<NKMShipLimitBreakTemplet>.Values
			where x.ShipId == shipId
			select x;
			return enumerable != null && enumerable.Count<NKMShipLimitBreakTemplet>() > 0 && limitBreakCount >= enumerable.Count<NKMShipLimitBreakTemplet>();
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x00092AAC File Offset: 0x00090CAC
		public static bool CanUseForShipLimitBreakMaterial(NKMUnitData shipData, int costShipId)
		{
			NKMShipLimitBreakTemplet shipLimitBreakTemplet = NKMShipManager.GetShipLimitBreakTemplet(shipData.m_UnitID, (int)(shipData.m_LimitBreakLevel + 1));
			return shipLimitBreakTemplet != null && shipLimitBreakTemplet.ListMaterialShipId.Contains(costShipId);
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x00092AE0 File Offset: 0x00090CE0
		public static bool HasNKMShipCommandModuleTemplet(NKMUnitData shipData)
		{
			if (shipData == null)
			{
				return false;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(shipData.m_UnitID);
			return unitTempletBase != null && NKMShipManager.GetNKMShipCommandModuleTemplet(unitTempletBase.m_NKM_UNIT_STYLE_TYPE, unitTempletBase.m_NKM_UNIT_GRADE, (int)shipData.m_LimitBreakLevel) != null;
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x00092B20 File Offset: 0x00090D20
		public static NKMShipCommandModuleTemplet GetNKMShipCommandModuleTemplet(NKM_UNIT_STYLE_TYPE unitStyleType, NKM_UNIT_GRADE unitGrade, int moduleNum)
		{
			NKMShipCommandModuleTemplet nkmshipCommandModuleTemplet = (from e in NKMTempletContainer<NKMShipCommandModuleTemplet>.Values
			where e.IsTargetTemplet(unitStyleType, unitGrade, moduleNum) && e.LimitBreakGrade == moduleNum
			select e).FirstOrDefault<NKMShipCommandModuleTemplet>();
			if (nkmshipCommandModuleTemplet == null)
			{
				return null;
			}
			return nkmshipCommandModuleTemplet;
		}

		// Token: 0x06001EDC RID: 7900 RVA: 0x00092B6C File Offset: 0x00090D6C
		public static bool IsMaxStat(int id, NKMShipCmdSlot slot)
		{
			if (slot == null || slot.statType == NKM_STAT_TYPE.NST_RANDOM)
			{
				return false;
			}
			int statGroupId = 0;
			IReadOnlyList<NKMCommandModulePassiveTemplet> passiveListsByGroupId = NKMShipModuleGroupTemplet.GetPassiveListsByGroupId(id);
			if (passiveListsByGroupId != null)
			{
				for (int i = 0; i < passiveListsByGroupId.Count; i++)
				{
					if (passiveListsByGroupId[i].RoleTypes.SetEquals(slot.targetRoleType) && passiveListsByGroupId[i].StyleTypes.SetEquals(slot.targetStyleType))
					{
						statGroupId = passiveListsByGroupId[i].StatGroupId;
					}
				}
			}
			IReadOnlyList<NKMCommandModuleRandomStatTemplet> statListsByGroupId = NKMShipModuleGroupTemplet.GetStatListsByGroupId(statGroupId);
			if (statListsByGroupId != null)
			{
				for (int j = 0; j < statListsByGroupId.Count; j++)
				{
					if (statListsByGroupId[j].StatType == slot.statType)
					{
						if (NKCUtilString.IsNameReversedIfNegative(slot.statType))
						{
							if (statListsByGroupId[j].MinStatValue >= slot.statValue && statListsByGroupId[j].MinStatFactor >= slot.statFactor)
							{
								return true;
							}
						}
						else if (statListsByGroupId[j].MaxStatValue <= slot.statValue && statListsByGroupId[j].MaxStatFactor <= slot.statFactor)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x00092C86 File Offset: 0x00090E86
		public static int GetShipMaxLevel(NKMUnitData shipData)
		{
			return NKMShipLevelUpTemplet.GetMaxLevel(shipData.GetStarGrade(), shipData.GetUnitGrade(), (int)shipData.m_LimitBreakLevel);
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x00092C9F File Offset: 0x00090E9F
		public static int GetMaxLevelShipID(int ShipID)
		{
			if ((int)((float)ShipID * 0.001f % 10f) == 100)
			{
				return ShipID;
			}
			return 26000 + ShipID % 1000;
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x00092CC3 File Offset: 0x00090EC3
		public static int GetMinLevelShipID(int ShipID)
		{
			return 21000 + ShipID % 1000;
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x00092CD2 File Offset: 0x00090ED2
		public static bool IsPercentStat(NKMCommandModuleRandomStatTemplet statTemplet)
		{
			if (statTemplet != null)
			{
				if (NKMUnitStatManager.IsPercentStat(statTemplet.StatType))
				{
					return true;
				}
				if (statTemplet.MinStatFactor > 0f)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x00092CF6 File Offset: 0x00090EF6
		public static bool IsPercentStat(NKM_STAT_TYPE statType, float statFactor)
		{
			return NKMUnitStatManager.IsPercentStat(statType) || statFactor > 0f;
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x00092D10 File Offset: 0x00090F10
		public static NKM_ERROR_CODE CanShipBuild(NKMUserData user_data, int ship_id)
		{
			if (!user_data.m_ArmyData.CanGetMoreShip(0))
			{
				return NKM_ERROR_CODE.NEC_FAIL_SHIP_FULL;
			}
			if (NKMUnitManager.GetUnitTempletBase(ship_id) == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_SHIP_INVALID_SHIP_ID;
			}
			NKMShipBuildTemplet shipBuildTemplet = NKMShipManager.GetShipBuildTemplet(ship_id);
			if (shipBuildTemplet == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_SHIP_INVALID_SHIP_ID;
			}
			foreach (BuildMaterial buildMaterial in shipBuildTemplet.BuildMaterialList)
			{
				if (user_data.m_InventoryData.GetCountMiscItem(buildMaterial.m_ShipBuildMaterialID) < (long)buildMaterial.m_ShipBuildMaterialCount)
				{
					return NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_ITEM;
				}
			}
			if (!NKMShipManager.CanUnlockShip(user_data, shipBuildTemplet))
			{
				return NKM_ERROR_CODE.NEC_FAIL_SHIP_NOT_UNLOCKED;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x00092DBC File Offset: 0x00090FBC
		public static bool CanUnlockShip(NKMUserData userData, NKMShipBuildTemplet shipBuildTemplet)
		{
			if (shipBuildTemplet == null)
			{
				return false;
			}
			if (userData == null)
			{
				return false;
			}
			switch (shipBuildTemplet.ShipBuildUnlockType)
			{
			case NKMShipBuildTemplet.BuildUnlockType.BUT_UNABLE:
				return false;
			case NKMShipBuildTemplet.BuildUnlockType.BUT_ALWAYS:
				return true;
			case NKMShipBuildTemplet.BuildUnlockType.BUT_PLAYER_LEVEL:
				return shipBuildTemplet.UnlockValue <= userData.m_UserLevel;
			case NKMShipBuildTemplet.BuildUnlockType.BUT_DUNGEON_CLEAR:
				return userData.CheckDungeonClear(shipBuildTemplet.UnlockValue);
			case NKMShipBuildTemplet.BuildUnlockType.BUT_WARFARE_CLEAR:
				return userData.CheckWarfareClear(shipBuildTemplet.UnlockValue);
			case NKMShipBuildTemplet.BuildUnlockType.BUT_SHIP_GET:
				return userData.m_ArmyData.IsCollectedUnit(shipBuildTemplet.UnlockValue);
			case NKMShipBuildTemplet.BuildUnlockType.BUT_SHIP_LV100:
				return NKMShipManager.CheckShipLevel(userData, shipBuildTemplet.UnlockValue, 100);
			case NKMShipBuildTemplet.BuildUnlockType.BUT_WORLDMAP_CITY_COUNT:
				return userData.m_WorldmapData.GetUnlockedCityCount() >= shipBuildTemplet.UnlockValue;
			case NKMShipBuildTemplet.BuildUnlockType.BUT_SHADOW_CLEAR:
			{
				NKMShadowPalaceTemplet shadowPalaceTemplet = NKMTempletContainer<NKMShadowPalaceTemplet>.Find(shipBuildTemplet.UnlockValue);
				if (shadowPalaceTemplet != null)
				{
					List<NKMShadowBattleTemplet> battleTemplets = NKMShadowPalaceManager.GetBattleTemplets(shadowPalaceTemplet.PALACE_ID);
					if (battleTemplets != null)
					{
						NKMPalaceData nkmpalaceData = userData.m_ShadowPalace.palaceDataList.Find((NKMPalaceData x) => x.palaceId == shadowPalaceTemplet.PALACE_ID);
						if (nkmpalaceData != null)
						{
							using (List<NKMShadowBattleTemplet>.Enumerator enumerator = battleTemplets.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									NKMShadowBattleTemplet battleTemplet = enumerator.Current;
									NKMPalaceDungeonData nkmpalaceDungeonData = nkmpalaceData.dungeonDataList.Find((NKMPalaceDungeonData x) => x.dungeonId == battleTemplet.DUNGEON_ID);
									if (nkmpalaceDungeonData == null)
									{
										return false;
									}
									if (nkmpalaceDungeonData.bestTime == 0)
									{
										return false;
									}
								}
							}
							return true;
						}
					}
				}
				return false;
			}
			default:
				Log.Error("Undefined Unlock type", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMShipSkillManagerEx.cs", 379);
				return false;
			}
		}

		// Token: 0x04001F44 RID: 8004
		public const int LEVELUP_MATERIAL_MAX_COUNT = 3;

		// Token: 0x04001F45 RID: 8005
		public const int UPGRADE_MATERIAL_MAX_COUNT = 4;

		// Token: 0x04001F46 RID: 8006
		public const int BUILD_MATERIAL_MAX_COUNT = 4;

		// Token: 0x04001F47 RID: 8007
		public static Dictionary<int, NKMShipBuildTemplet> m_DicNKMShipBuildTemplet;

		// Token: 0x04001F48 RID: 8008
		public const string SHIP_LIMITBREAK_TAG = "SHIP_LIMITBREAK";

		// Token: 0x04001F49 RID: 8009
		public const string SHIP_COMMONDMODULE_TAG = "SHIP_COMMANDMODULE";
	}
}
