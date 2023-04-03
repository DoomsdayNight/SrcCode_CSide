using System;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.Common;
using Cs.Logging;
using NKM;
using NKM.Contract2;
using NKM.Templet;
using NKM.Templet.Recall;
using NKM.Unit;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006A2 RID: 1698
	public static class NKCRecallManager
	{
		// Token: 0x060037F6 RID: 14326 RVA: 0x001207DC File Offset: 0x0011E9DC
		public static void UnEquipAndRecall(NKMUnitData sourceUnitData, int targetUnitId = 0)
		{
			NKCRecallManager.m_EquippedCount = 0;
			NKCRecallManager.m_SourceUnitData = sourceUnitData;
			NKCRecallManager.m_TargetUnitId = targetUnitId;
			NKCRecallManager.m_EquippedCount += NKCRecallManager.SendEquipItem(false, NKCRecallManager.m_SourceUnitData.GetEquipItemWeaponUid(), ITEM_EQUIP_POSITION.IEP_WEAPON);
			NKCRecallManager.m_EquippedCount += NKCRecallManager.SendEquipItem(false, NKCRecallManager.m_SourceUnitData.GetEquipItemDefenceUid(), ITEM_EQUIP_POSITION.IEP_DEFENCE);
			NKCRecallManager.m_EquippedCount += NKCRecallManager.SendEquipItem(false, NKCRecallManager.m_SourceUnitData.GetEquipItemAccessoryUid(), ITEM_EQUIP_POSITION.IEP_ACC);
			NKCRecallManager.m_EquippedCount += NKCRecallManager.SendEquipItem(false, NKCRecallManager.m_SourceUnitData.GetEquipItemAccessory2Uid(), ITEM_EQUIP_POSITION.IEP_ACC2);
			NKCRecallManager.m_bWaitingRecallProcess = true;
			if (NKCRecallManager.m_EquippedCount == 0)
			{
				NKCRecallManager.OnUnequipComplete();
			}
		}

		// Token: 0x060037F7 RID: 14327 RVA: 0x0012087D File Offset: 0x0011EA7D
		private static int SendEquipItem(bool bEquip, long targetEquipUId, ITEM_EQUIP_POSITION equipPosition)
		{
			if (NKCRecallManager.m_SourceUnitData == null || targetEquipUId <= 0L)
			{
				return 0;
			}
			NKCPacketSender.Send_NKMPacket_EQUIP_ITEM_EQUIP_REQ(bEquip, NKCRecallManager.m_SourceUnitData.m_UnitUID, targetEquipUId, equipPosition);
			return 1;
		}

		// Token: 0x060037F8 RID: 14328 RVA: 0x001208A0 File Offset: 0x0011EAA0
		public static void OnUnequipComplete()
		{
			if (NKCRecallManager.m_bWaitingRecallProcess && NKCRecallManager.m_EquippedCount > 0)
			{
				NKCRecallManager.m_EquippedCount--;
				if (NKCRecallManager.m_bWaitingRecallProcess && NKCRecallManager.m_EquippedCount == 0)
				{
					NKCPacketSender.Send_NKMPacket_RECALL_UNIT_REQ(NKCRecallManager.m_SourceUnitData.m_UnitUID, NKCRecallManager.m_TargetUnitId);
					NKCRecallManager.m_bWaitingRecallProcess = false;
					NKCRecallManager.m_SourceUnitData = null;
					NKCRecallManager.m_TargetUnitId = 0;
				}
				return;
			}
			if (NKCRecallManager.m_EquippedCount == 0)
			{
				NKCPacketSender.Send_NKMPacket_RECALL_UNIT_REQ(NKCRecallManager.m_SourceUnitData.m_UnitUID, NKCRecallManager.m_TargetUnitId);
				NKCRecallManager.m_bWaitingRecallProcess = false;
				NKCRecallManager.m_SourceUnitData = null;
				NKCRecallManager.m_TargetUnitId = 0;
			}
		}

		// Token: 0x060037F9 RID: 14329 RVA: 0x0012092C File Offset: 0x0011EB2C
		public static bool IsRecallTargetUnit(NKMUnitData unitData, DateTime utcNow)
		{
			if (unitData == null)
			{
				return false;
			}
			NKMUnitTempletBase nkmunitTempletBase = NKMUnitTempletBase.Find(unitData.m_UnitID);
			if (nkmunitTempletBase == null)
			{
				return false;
			}
			int num = unitData.m_UnitID;
			if (nkmunitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
			{
				num = NKCRecallManager.GetFirstLevelShipID(num);
			}
			NKMRecallTemplet nkmrecallTemplet = NKMRecallTemplet.Find(num, NKMTime.UTCtoLocal(utcNow, 0));
			return nkmrecallTemplet != null && NKCRecallManager.IsValidRegTime(nkmrecallTemplet, unitData.m_regDate) && NKCRecallManager.IsRecallTargetUnit(num, utcNow);
		}

		// Token: 0x060037FA RID: 14330 RVA: 0x00120990 File Offset: 0x0011EB90
		private static bool IsRecallTargetUnit(int unitID, DateTime utcNow)
		{
			NKMRecallTemplet nkmrecallTemplet = NKMRecallTemplet.Find(unitID, NKMTime.UTCtoLocal(utcNow, 0));
			RecallHistoryInfo recallHistoryInfo;
			return nkmrecallTemplet != null && (!NKCScenManager.CurrentUserData().m_RecallHistoryData.TryGetValue(unitID, out recallHistoryInfo) || NKCRecallManager.IsValidTime(nkmrecallTemplet, recallHistoryInfo.lastUpdateDate));
		}

		// Token: 0x060037FB RID: 14331 RVA: 0x001209D8 File Offset: 0x0011EBD8
		public static int GetFirstLevelShipID(int shipID)
		{
			NKMShipBuildTemplet nkmshipBuildTemplet = NKMShipBuildTemplet.Find(shipID);
			if (nkmshipBuildTemplet == null)
			{
				return shipID;
			}
			while (NKMShipBuildTemplet.Find(shipID) != null)
			{
				nkmshipBuildTemplet = NKMShipBuildTemplet.Find(shipID);
				shipID -= 1000;
			}
			return nkmshipBuildTemplet.ShipID;
		}

		// Token: 0x060037FC RID: 14332 RVA: 0x00120A0F File Offset: 0x0011EC0F
		public static bool IsValidTime(NKMRecallTemplet recallTemplet, DateTime utcNow)
		{
			return !(utcNow < recallTemplet.IntervalTemplet.GetStartDateUtc()) && !(utcNow >= recallTemplet.IntervalTemplet.GetEndDateUtc());
		}

		// Token: 0x060037FD RID: 14333 RVA: 0x00120A3A File Offset: 0x0011EC3A
		public static bool IsValidRegTime(NKMRecallTemplet recallTemplet, DateTime regDateUtc)
		{
			return regDateUtc < recallTemplet.IntervalTemplet.GetStartDateUtc() || regDateUtc >= recallTemplet.IntervalTemplet.GetEndDateUtc();
		}

		// Token: 0x060037FE RID: 14334 RVA: 0x00120A68 File Offset: 0x0011EC68
		public static Dictionary<int, NKMItemMiscData> ConvertLimitBreakToResources(NKMUnitData unitData)
		{
			Dictionary<int, NKMItemMiscData> dictionary = new Dictionary<int, NKMItemMiscData>();
			if (unitData != null)
			{
				NKMUnitTempletBase nkmunitTempletBase = NKMUnitTempletBase.Find(unitData.m_UnitID);
				if (nkmunitTempletBase != null)
				{
					int i = (int)unitData.m_LimitBreakLevel;
					if (nkmunitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL)
					{
						while (i > 0)
						{
							NKMLimitBreakItemTemplet lbsubstituteItemInfo = NKMUnitLimitBreakManager.GetLBSubstituteItemInfo(nkmunitTempletBase.m_NKM_UNIT_STYLE_TYPE, nkmunitTempletBase.m_NKM_UNIT_GRADE, i);
							NKMLimitBreakTemplet lbinfo = NKMUnitLimitBreakManager.GetLBInfo(i);
							if (lbsubstituteItemInfo == null)
							{
								Log.Error(string.Format("Can not found NKMLimitBreakItemTemplet {0}, {1}, {2}", nkmunitTempletBase.m_NKM_UNIT_STYLE_TYPE, nkmunitTempletBase.m_NKM_UNIT_GRADE, i), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCManager/NKCRecallManager.cs", 177);
								i--;
							}
							else if (lbinfo == null)
							{
								Log.Error(string.Format("Can not found NKMLimitBreakTemplet {0}", i), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCManager/NKCRecallManager.cs", 184);
								i--;
							}
							else
							{
								foreach (NKMLimitBreakItemTemplet.ItemRequirement itemRequirement in lbsubstituteItemInfo.m_lstRequiredItem)
								{
									if (dictionary.ContainsKey(itemRequirement.itemID))
									{
										dictionary[itemRequirement.itemID].CountFree += (long)(itemRequirement.count * lbinfo.m_iUnitRequirement);
									}
									else
									{
										dictionary.Add(itemRequirement.itemID, new NKMItemMiscData(itemRequirement.itemID, (long)(itemRequirement.count * lbinfo.m_iUnitRequirement), 0L, 0));
									}
								}
								if (dictionary.ContainsKey(1))
								{
									dictionary[1].CountFree += (long)lbsubstituteItemInfo.m_CreditReq;
								}
								else
								{
									dictionary.Add(1, new NKMItemMiscData(1, (long)lbsubstituteItemInfo.m_CreditReq, 0L, 0));
								}
								i--;
							}
						}
					}
					else if (nkmunitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
					{
						while (i > 0)
						{
							NKMShipLimitBreakTemplet shipLimitBreakTemplet = NKMShipManager.GetShipLimitBreakTemplet(nkmunitTempletBase.m_UnitID, i);
							if (shipLimitBreakTemplet == null)
							{
								Log.Error(string.Format("Can not found NKMShipLimitBreakTemplet {0}, {1}", nkmunitTempletBase.m_UnitID, i), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCManager/NKCRecallManager.cs", 213);
								i--;
							}
							else
							{
								if (shipLimitBreakTemplet.ListMaterialShipId.Count > 0)
								{
									int num = int.MaxValue;
									for (int j = 0; j < shipLimitBreakTemplet.ListMaterialShipId.Count; j++)
									{
										if (num < shipLimitBreakTemplet.ListMaterialShipId[j])
										{
											num = shipLimitBreakTemplet.ListMaterialShipId[j];
										}
									}
									NKMShipBuildTemplet nkmshipBuildTemplet = NKMShipBuildTemplet.Find(NKMShipManager.GetMinLevelShipID(num));
									if (nkmshipBuildTemplet == null)
									{
										continue;
									}
									NKCRecallManager.AddResource(nkmshipBuildTemplet.BuildMaterialList, ref dictionary);
								}
								foreach (MiscItemUnit miscItemUnit in shipLimitBreakTemplet.ShipLimitBreakItems)
								{
									if (dictionary.ContainsKey(miscItemUnit.ItemId))
									{
										dictionary[miscItemUnit.ItemId].CountFree += miscItemUnit.Count;
									}
									else
									{
										dictionary.Add(miscItemUnit.ItemId, new NKMItemMiscData(miscItemUnit.ItemId, miscItemUnit.Count, 0L, 0));
									}
								}
								i--;
							}
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060037FF RID: 14335 RVA: 0x00120D90 File Offset: 0x0011EF90
		public static Dictionary<int, NKMItemMiscData> ConvertUnitExpToResources(NKMUnitData unitData)
		{
			Dictionary<int, NKMItemMiscData> result = new Dictionary<int, NKMItemMiscData>();
			NKMUnitExpTemplet nkmunitExpTemplet = (unitData != null) ? unitData.GetExpTemplet() : null;
			if (nkmunitExpTemplet == null)
			{
				return result;
			}
			int num = nkmunitExpTemplet.m_iExpCumulated + unitData.m_iUnitLevelEXP;
			if (num <= 0)
			{
				return result;
			}
			if (NKMCommonConst.Negotiation.Materials.Count == 0)
			{
				return result;
			}
			NegotiationMaterial negotiationMaterial = (from e in NKMCommonConst.Negotiation.Materials
			orderby e.Exp descending
			select e).First<NegotiationMaterial>();
			int num2 = (int)Math.Ceiling((double)((float)num / (float)negotiationMaterial.Exp));
			if (num2 == 0)
			{
				return result;
			}
			NKCRecallManager.AddResource(negotiationMaterial.ItemId, num2, ref result);
			NKCRecallManager.AddResource(1, negotiationMaterial.Credit * num2, ref result);
			return result;
		}

		// Token: 0x06003800 RID: 14336 RVA: 0x00120E4C File Offset: 0x0011F04C
		public static Dictionary<int, NKMItemMiscData> ConvertSkillToResources(NKMUnitData unitData)
		{
			Dictionary<int, NKMItemMiscData> result = new Dictionary<int, NKMItemMiscData>();
			NKMUnitTempletBase nkmunitTempletBase = NKMUnitTempletBase.Find(unitData.m_UnitID);
			if (nkmunitTempletBase == null)
			{
				return result;
			}
			int num = 0;
			int num2 = 0;
			while (num2 < unitData.m_aUnitSkillLevel.Length && num2 < nkmunitTempletBase.m_lstSkillStrID.Count)
			{
				int i = unitData.m_aUnitSkillLevel[num2];
				string text = nkmunitTempletBase.m_lstSkillStrID[num2];
				while (i > 1)
				{
					NKMUnitSkillTemplet skillTemplet = NKMUnitSkillManager.GetSkillTemplet(text, i);
					if (skillTemplet == null)
					{
						Log.Error(string.Format("Can not found UnitSkillTemplet. {0}, {1}", text, i), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCManager/NKCRecallManager.cs", 313);
					}
					else
					{
						foreach (NKMUnitSkillTemplet.NKMUpgradeReqItem nkmupgradeReqItem in skillTemplet.m_lstUpgradeReqItem)
						{
							if (NKMItemMiscTemplet.Find(nkmupgradeReqItem.ItemID).m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_PIECE)
							{
								num += nkmupgradeReqItem.ItemCount;
							}
							else
							{
								NKCRecallManager.AddResource(nkmupgradeReqItem.ItemID, nkmupgradeReqItem.ItemCount, ref result);
							}
						}
						i--;
					}
				}
				num2++;
			}
			if (num > 0)
			{
				NKCRecallManager.AddResource(401, Mathf.CeilToInt((float)num * NKMCommonConst.RecallRewardUnitPieceToPoint), ref result);
			}
			return result;
		}

		// Token: 0x06003801 RID: 14337 RVA: 0x00120F90 File Offset: 0x0011F190
		public static Dictionary<int, NKMItemMiscData> ConvertUnitLifeTimeToResource(NKMUnitData unitData)
		{
			Dictionary<int, NKMItemMiscData> result = new Dictionary<int, NKMItemMiscData>();
			if (unitData.IsPermanentContract)
			{
				NKCRecallManager.AddResource(1024, 1, ref result);
			}
			return result;
		}

		// Token: 0x06003802 RID: 14338 RVA: 0x00120FBC File Offset: 0x0011F1BC
		public static Dictionary<int, NKMItemMiscData> ConvertShipBuildToResource(NKMUnitData unitData)
		{
			Dictionary<int, NKMItemMiscData> result = new Dictionary<int, NKMItemMiscData>();
			if (unitData.m_UnitLevel < 1)
			{
				return result;
			}
			int num = unitData.m_UnitID;
			NKMShipBuildTemplet nkmshipBuildTemplet = NKMShipBuildTemplet.Find(num);
			while (NKMShipBuildTemplet.Find(num) != null)
			{
				nkmshipBuildTemplet = NKMShipBuildTemplet.Find(num);
				num -= 1000;
			}
			NKCRecallManager.AddResource(nkmshipBuildTemplet.BuildMaterialList, ref result);
			return result;
		}

		// Token: 0x06003803 RID: 14339 RVA: 0x00121010 File Offset: 0x0011F210
		public static Dictionary<int, NKMItemMiscData> ConvertShipToResources(NKMUnitData unitData)
		{
			Dictionary<int, NKMItemMiscData> result = new Dictionary<int, NKMItemMiscData>();
			if (unitData.m_UnitLevel <= 1)
			{
				return result;
			}
			List<int> list = new List<int>();
			int num = unitData.m_UnitID;
			for (NKMShipBuildTemplet shipBuildTemplet = NKMShipManager.GetShipBuildTemplet(num); shipBuildTemplet != null; shipBuildTemplet = NKMShipManager.GetShipBuildTemplet(num))
			{
				NKCRecallManager.AddResource(shipBuildTemplet.UpgradeMaterialList, ref result);
				if (shipBuildTemplet.ShipUpgradeCredit > 0)
				{
					NKCRecallManager.AddResource(1, shipBuildTemplet.ShipUpgradeCredit, ref result);
				}
				list.Insert(0, num);
				num -= 1000;
			}
			int num2 = 1;
			int i = (int)unitData.m_LimitBreakLevel;
			if (i > 0)
			{
				while (i > 0)
				{
					NKMUnitTempletBase nkmunitTempletBase = NKMUnitTempletBase.Find(num);
					if (nkmunitTempletBase == null)
					{
						Log.Error(string.Format("Can not found UnitTempletBase. ShipId:{0}", num), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCManager/NKCRecallManager.cs", 410);
					}
					else
					{
						NKMShipLevelUpTemplet nkmshipLevelUpTemplet = NKMShipLevelUpTemplet.Find(nkmunitTempletBase.m_StarGradeMax, nkmunitTempletBase.m_NKM_UNIT_GRADE, i);
						if (nkmshipLevelUpTemplet == null)
						{
							Log.Error(string.Format("Can not found ShipLevelIpTemplet. GradeMax:{0} Grade:{1} LimitBreakLevel:{2}", nkmunitTempletBase.m_StarGradeMax, nkmunitTempletBase.m_NKM_UNIT_GRADE, unitData.m_LimitBreakLevel), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCManager/NKCRecallManager.cs", 417);
						}
						else
						{
							int num3 = Math.Min(nkmshipLevelUpTemplet.ShipMaxLevel, unitData.m_UnitLevel);
							num3 -= num2;
							if (num3 <= 0)
							{
								break;
							}
							foreach (LevelupMaterial levelupMaterial in nkmshipLevelUpTemplet.ShipLevelupMaterialList)
							{
								int itemCount = levelupMaterial.m_LevelupMaterialCount * num3;
								NKCRecallManager.AddResource(levelupMaterial.m_LevelupMaterialItemID, itemCount, ref result);
							}
							int itemCount2 = nkmshipLevelUpTemplet.ShipUpgradeCredit * num3;
							NKCRecallManager.AddResource(1, itemCount2, ref result);
							num2 += num3;
							i--;
						}
					}
				}
			}
			foreach (int num4 in list)
			{
				NKMUnitTempletBase nkmunitTempletBase2 = NKMUnitTempletBase.Find(num4);
				if (nkmunitTempletBase2 == null)
				{
					Log.Error(string.Format("Can not found UnitTempletBase. ShipId:{0}", num4), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCManager/NKCRecallManager.cs", 450);
				}
				else
				{
					NKMShipLevelUpTemplet nkmshipLevelUpTemplet2 = NKMShipLevelUpTemplet.Find(nkmunitTempletBase2.m_StarGradeMax, nkmunitTempletBase2.m_NKM_UNIT_GRADE, 0);
					if (nkmshipLevelUpTemplet2 == null)
					{
						Log.Error(string.Format("Can not found ShipLevelIpTemplet. GradeMax:{0} Grade:{1} LimitBreakLevel:{2}", nkmunitTempletBase2.m_StarGradeMax, nkmunitTempletBase2.m_NKM_UNIT_GRADE, 0), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCManager/NKCRecallManager.cs", 457);
					}
					else
					{
						int num5 = Math.Min(nkmshipLevelUpTemplet2.ShipMaxLevel, unitData.m_UnitLevel);
						num5 -= num2;
						if (num5 <= 0)
						{
							break;
						}
						foreach (LevelupMaterial levelupMaterial2 in nkmshipLevelUpTemplet2.ShipLevelupMaterialList)
						{
							int itemCount3 = levelupMaterial2.m_LevelupMaterialCount * num5;
							NKCRecallManager.AddResource(levelupMaterial2.m_LevelupMaterialItemID, itemCount3, ref result);
						}
						int itemCount4 = nkmshipLevelUpTemplet2.ShipUpgradeCredit * num5;
						NKCRecallManager.AddResource(1, itemCount4, ref result);
						num2 += num5;
					}
				}
			}
			return result;
		}

		// Token: 0x06003804 RID: 14340 RVA: 0x0012134C File Offset: 0x0011F54C
		private static void AddResource(List<BuildMaterial> lstMaterial, ref Dictionary<int, NKMItemMiscData> resourcesMap)
		{
			for (int i = 0; i < lstMaterial.Count; i++)
			{
				NKCRecallManager.AddResource(lstMaterial[i].m_ShipBuildMaterialID, lstMaterial[i].m_ShipBuildMaterialCount, ref resourcesMap);
			}
		}

		// Token: 0x06003805 RID: 14341 RVA: 0x00121388 File Offset: 0x0011F588
		private static void AddResource(List<UpgradeMaterial> lstMaterial, ref Dictionary<int, NKMItemMiscData> resourcesMap)
		{
			for (int i = 0; i < lstMaterial.Count; i++)
			{
				NKCRecallManager.AddResource(lstMaterial[i].m_ShipUpgradeMaterial, lstMaterial[i].m_ShipUpgradeMaterialCount, ref resourcesMap);
			}
		}

		// Token: 0x06003806 RID: 14342 RVA: 0x001213C4 File Offset: 0x0011F5C4
		private static void AddResource(int itemID, int itemCount, ref Dictionary<int, NKMItemMiscData> resourcesMap)
		{
			if (resourcesMap.ContainsKey(itemID))
			{
				resourcesMap[itemID].CountFree += (long)itemCount;
				return;
			}
			resourcesMap.Add(itemID, new NKMItemMiscData
			{
				ItemID = itemID,
				CountFree = (long)itemCount,
				CountPaid = 0L,
				BonusRatio = 0
			});
		}

		// Token: 0x04003476 RID: 13430
		public static bool m_bWaitingRecallProcess;

		// Token: 0x04003477 RID: 13431
		private static int m_EquippedCount;

		// Token: 0x04003478 RID: 13432
		private static NKMUnitData m_SourceUnitData;

		// Token: 0x04003479 RID: 13433
		private static int m_TargetUnitId;
	}
}
