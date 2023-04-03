using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200049B RID: 1179
	public static class NKMUnitLimitBreakManager
	{
		// Token: 0x060020FB RID: 8443 RVA: 0x000A7F52 File Offset: 0x000A6152
		public static NKMLimitBreakTemplet GetLBInfo(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				return null;
			}
			return NKMUnitLimitBreakManager.GetLBInfo((int)unitData.m_LimitBreakLevel);
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x000A7F64 File Offset: 0x000A6164
		public static NKMLimitBreakTemplet GetLBInfo(int currentLimitBreakLevel)
		{
			if (NKMUnitLimitBreakManager.m_dicLimitBreakTemplet.ContainsKey(currentLimitBreakLevel))
			{
				return NKMUnitLimitBreakManager.m_dicLimitBreakTemplet[currentLimitBreakLevel];
			}
			return null;
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x000A7F80 File Offset: 0x000A6180
		public static NKMLimitBreakItemTemplet GetLBSubstituteItemInfo(NKMUnitData targetUnit)
		{
			if (targetUnit == null)
			{
				return null;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(targetUnit);
			if (unitTempletBase == null)
			{
				return null;
			}
			return NKMUnitLimitBreakManager.GetLBSubstituteItemInfo(unitTempletBase.m_NKM_UNIT_STYLE_TYPE, unitTempletBase.m_NKM_UNIT_GRADE, (int)(targetUnit.m_LimitBreakLevel + 1));
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x000A7FB8 File Offset: 0x000A61B8
		public static NKMLimitBreakItemTemplet GetLBSubstituteItemInfo(NKM_UNIT_STYLE_TYPE eUnitStyle, NKM_UNIT_GRADE eUnitGrade, int targetLBLevel)
		{
			if (NKMUnitLimitBreakManager.m_dicLimitBreakTemplet.Count > targetLBLevel)
			{
				int key = NKMUnitLimitBreakManager.MakeKey(eUnitStyle, eUnitGrade, targetLBLevel);
				if (NKMUnitLimitBreakManager.m_dicLimitBreakItemTemplet.ContainsKey(key))
				{
					return NKMUnitLimitBreakManager.m_dicLimitBreakItemTemplet[key];
				}
			}
			return null;
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x000A7FF5 File Offset: 0x000A61F5
		public static int GetMaxLimitBreakLevelByUnitLevel(int level)
		{
			if (level <= 100)
			{
				return 3;
			}
			return 8;
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x000A7FFF File Offset: 0x000A61FF
		public static bool IsMaxLimitBreak(NKMUnitData unitData, bool bIsMaxTrancendence)
		{
			if (bIsMaxTrancendence)
			{
				return NKMUnitLimitBreakManager.GetUnitLimitbreakStatus(unitData) == NKMUnitLimitBreakManager.UnitLimitBreakStatus.TranscendenceMax;
			}
			return unitData.m_LimitBreakLevel >= 3;
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x000A801C File Offset: 0x000A621C
		public static bool CanThisUnitLimitBreak(NKMUnitData unitData)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID);
			if (unitTempletBase != null && unitTempletBase.m_NKM_UNIT_STYLE_TYPE == NKM_UNIT_STYLE_TYPE.NUST_TRAINER)
			{
				return false;
			}
			switch (NKMUnitLimitBreakManager.GetUnitLimitbreakStatus(unitData))
			{
			case NKMUnitLimitBreakManager.UnitLimitBreakStatus.Invalid:
			case NKMUnitLimitBreakManager.UnitLimitBreakStatus.LimitBreakLevelNotEnough:
			case NKMUnitLimitBreakManager.UnitLimitBreakStatus.LimitBreakMax:
			case NKMUnitLimitBreakManager.UnitLimitBreakStatus.TranscendenceLevelNotEnough:
			case NKMUnitLimitBreakManager.UnitLimitBreakStatus.TranscendenceMax:
				return false;
			case NKMUnitLimitBreakManager.UnitLimitBreakStatus.CanLimitBreak:
			case NKMUnitLimitBreakManager.UnitLimitBreakStatus.CanTranscendence:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x000A8073 File Offset: 0x000A6273
		public static int GetTranscendenceCount(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				return 0;
			}
			return Math.Max((int)(unitData.m_LimitBreakLevel - 3), 0);
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x000A8088 File Offset: 0x000A6288
		public static bool IsTranscendenceUnit(NKMUnitData unitData)
		{
			if (NKMUnitManager.GetUnitTempletBase(unitData).m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
			{
				return unitData.m_LimitBreakLevel > 0;
			}
			return unitData.m_LimitBreakLevel > 3;
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x000A80AC File Offset: 0x000A62AC
		public static NKMUnitLimitBreakManager.UnitLimitBreakStatus GetUnitLimitbreakStatus(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				return NKMUnitLimitBreakManager.UnitLimitBreakStatus.Invalid;
			}
			if (unitData.m_LimitBreakLevel >= 8)
			{
				return NKMUnitLimitBreakManager.UnitLimitBreakStatus.TranscendenceMax;
			}
			NKMLimitBreakTemplet lbinfo = NKMUnitLimitBreakManager.GetLBInfo((int)(unitData.m_LimitBreakLevel + 1));
			NKMLimitBreakItemTemplet lbsubstituteItemInfo = NKMUnitLimitBreakManager.GetLBSubstituteItemInfo(unitData);
			if (lbinfo == null || lbsubstituteItemInfo == null)
			{
				NKMLimitBreakTemplet lbinfo2 = NKMUnitLimitBreakManager.GetLBInfo((int)unitData.m_LimitBreakLevel);
				if (lbinfo2 != null)
				{
					if (!lbinfo2.m_bTranscendence)
					{
						return NKMUnitLimitBreakManager.UnitLimitBreakStatus.LimitBreakMax;
					}
					return NKMUnitLimitBreakManager.UnitLimitBreakStatus.TranscendenceMax;
				}
				else
				{
					if (unitData.m_LimitBreakLevel > 3)
					{
						return NKMUnitLimitBreakManager.UnitLimitBreakStatus.TranscendenceMax;
					}
					return NKMUnitLimitBreakManager.UnitLimitBreakStatus.LimitBreakMax;
				}
			}
			else if (unitData.m_LimitBreakLevel >= 3)
			{
				if (unitData.m_UnitLevel < lbinfo.m_iRequiredLevel)
				{
					return NKMUnitLimitBreakManager.UnitLimitBreakStatus.TranscendenceLevelNotEnough;
				}
				return NKMUnitLimitBreakManager.UnitLimitBreakStatus.CanTranscendence;
			}
			else
			{
				if (unitData.m_UnitLevel < lbinfo.m_iRequiredLevel)
				{
					return NKMUnitLimitBreakManager.UnitLimitBreakStatus.LimitBreakLevelNotEnough;
				}
				return NKMUnitLimitBreakManager.UnitLimitBreakStatus.CanLimitBreak;
			}
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x000A8138 File Offset: 0x000A6338
		public static int CanThisUnitLimitBreakNow(NKMUnitData unitData, NKMUserData userData)
		{
			if (unitData == null || userData == null)
			{
				return -1;
			}
			if (!NKMUnitLimitBreakManager.CanThisUnitLimitBreak(unitData))
			{
				return -1;
			}
			if (NKMUnitLimitBreakManager.GetLBInfo((int)(unitData.m_LimitBreakLevel + 1)) == null)
			{
				return -1;
			}
			NKMLimitBreakItemTemplet lbsubstituteItemInfo = NKMUnitLimitBreakManager.GetLBSubstituteItemInfo(unitData);
			if (lbsubstituteItemInfo == null)
			{
				return -1;
			}
			for (int i = 0; i < lbsubstituteItemInfo.m_lstRequiredItem.Count; i++)
			{
				int itemID = lbsubstituteItemInfo.m_lstRequiredItem[i].itemID;
				int count = lbsubstituteItemInfo.m_lstRequiredItem[i].count;
				if (userData.m_InventoryData.GetCountMiscItem(itemID) < (long)count)
				{
					return -1;
				}
			}
			return 0;
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x000A81C4 File Offset: 0x000A63C4
		public static NKM_ERROR_CODE CanLimitBreak(NKMUserData userData, NKMUnitData targetUnitData, out List<NKMItemMiscData> lstCost)
		{
			lstCost = new List<NKMItemMiscData>();
			NKMArmyData armyData = userData.m_ArmyData;
			if (targetUnitData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_NOT_EXIST;
			}
			if (NKMUnitLimitBreakManager.IsMaxLimitBreak(targetUnitData, true))
			{
				return NKM_ERROR_CODE.NEC_FAIL_LIMITBREAK_ALREADY_MAX_LEVEL;
			}
			NKMLimitBreakTemplet lbinfo = NKMUnitLimitBreakManager.GetLBInfo((int)(targetUnitData.m_LimitBreakLevel + 1));
			if (lbinfo == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_GET_UNIT_LIMIT_BREAK_TEMPLET_NULL;
			}
			if (targetUnitData.m_UnitLevel < lbinfo.m_iRequiredLevel)
			{
				return NKM_ERROR_CODE.NEC_FAIL_LIMITBREAK_LOW_LEVEL;
			}
			NKMLimitBreakItemTemplet lbsubstituteItemInfo = NKMUnitLimitBreakManager.GetLBSubstituteItemInfo(targetUnitData);
			if (lbsubstituteItemInfo == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_GET_ITEM_LIMIT_BREAK_TEMPLET_NULL;
			}
			if (userData.GetCredit() < (long)lbsubstituteItemInfo.m_CreditReq)
			{
				return NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_CREDIT;
			}
			lstCost.Add(new NKMItemMiscData(1, (long)lbsubstituteItemInfo.m_CreditReq, 0L, 0));
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x000A825C File Offset: 0x000A645C
		public static float GetLimitBreakStatMultiplier(int LimitBreakLevel)
		{
			int num = Math.Min(LimitBreakLevel, 3);
			int num2 = Math.Max(0, LimitBreakLevel - 3);
			return 1f + 0.1f * (float)num + 0.02f * (float)num2;
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x000A8294 File Offset: 0x000A6494
		public static float GetLimitBreakStatMultiplierForShip(int lImitBreakLevel)
		{
			int num = Math.Max(0, lImitBreakLevel);
			return 1f + 0.02f * (float)num;
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x000A82B8 File Offset: 0x000A64B8
		public static int GetRequiredDuplicateCount(NKMUnitData unitData, bool bIncludeTranscendence)
		{
			if (unitData == null)
			{
				return 0;
			}
			int result = 0;
			if (NKMUnitManager.GetUnitTempletBase(unitData) == null)
			{
				return 0;
			}
			return result;
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x000A82D9 File Offset: 0x000A64D9
		private static bool LoadFromLUA_LIMITBREAK_SUBSTITUTE_ITEM(string fileName)
		{
			NKMUnitLimitBreakManager.m_dicLimitBreakItemTemplet = NKMTempletLoader.LoadDictionary<NKMLimitBreakItemTemplet>("AB_SCRIPT", fileName, "m_LBSubstitute", new Func<NKMLua, NKMLimitBreakItemTemplet>(NKMLimitBreakItemTemplet.LoadFromLUA));
			return NKMUnitLimitBreakManager.m_dicLimitBreakItemTemplet != null;
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x000A8304 File Offset: 0x000A6504
		private static bool LoadFromLUA_LUA_LIMITBREAK_INFO(string fileName)
		{
			NKMUnitLimitBreakManager.m_dicLimitBreakTemplet = NKMTempletLoader.LoadDictionary<NKMLimitBreakTemplet>("AB_SCRIPT", fileName, "m_LimitBreakInfo", new Func<NKMLua, NKMLimitBreakTemplet>(NKMLimitBreakTemplet.LoadFromLUA));
			return NKMUnitLimitBreakManager.m_dicLimitBreakTemplet != null;
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x000A8330 File Offset: 0x000A6530
		public static bool LoadFromLua(string LBInfoFileName, string SubstituteItemFileName)
		{
			bool result = true;
			if (!NKMUnitLimitBreakManager.LoadFromLUA_LUA_LIMITBREAK_INFO(LBInfoFileName))
			{
				Log.Error("LoadFromLUA_LUA_LIMITBREAK_INFO Fail", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitLimitBreakManager.cs", 455);
				result = false;
			}
			if (!NKMUnitLimitBreakManager.LoadFromLUA_LIMITBREAK_SUBSTITUTE_ITEM(SubstituteItemFileName))
			{
				Log.Error("LoadFromLUA_LIMITBREAK_SUBSTITUTE_ITEM Fail", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitLimitBreakManager.cs", 462);
				result = false;
			}
			NKMUnitLimitBreakManager.CheckValidation();
			return result;
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x000A8381 File Offset: 0x000A6581
		public static int MakeKey(NKM_UNIT_STYLE_TYPE eUnitStyle, NKM_UNIT_GRADE eUnitGrade, int targetLBLevel)
		{
			return (int)(eUnitStyle * (NKM_UNIT_STYLE_TYPE)1000 + (short)(eUnitGrade * (NKM_UNIT_GRADE)10) + (short)targetLBLevel);
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x000A8394 File Offset: 0x000A6594
		private static void CheckValidation()
		{
			foreach (KeyValuePair<int, NKMLimitBreakItemTemplet> keyValuePair in NKMUnitLimitBreakManager.m_dicLimitBreakItemTemplet)
			{
				foreach (NKMLimitBreakItemTemplet.ItemRequirement itemRequirement in keyValuePair.Value.m_lstRequiredItem)
				{
					int itemID = itemRequirement.itemID;
					int count = itemRequirement.count;
					if (NKMItemManager.GetItemMiscTempletByID(itemID) == null || count <= 0)
					{
						Log.ErrorAndExit(string.Format("[LimitBreakTemplet] 초월 아이템 정보가 존재하지 않음 eUnitStyle : {0}, eUnitGrade : {1}, targetLBLevel : {2}, m_ItemMiscID : {3}, m_Count : {4}", new object[]
						{
							keyValuePair.Value.m_NKM_UNIT_STYLE_TYPE,
							keyValuePair.Value.m_NKM_UNIT_GRADE,
							keyValuePair.Value.m_TargetLimitbreakLevel,
							itemID,
							count
						}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitLimitBreakManager.cs", 489);
					}
				}
			}
		}

		// Token: 0x04002181 RID: 8577
		public static Dictionary<int, NKMLimitBreakTemplet> m_dicLimitBreakTemplet;

		// Token: 0x04002182 RID: 8578
		public static Dictionary<int, NKMLimitBreakItemTemplet> m_dicLimitBreakItemTemplet;

		// Token: 0x02001214 RID: 4628
		public enum UnitLimitBreakStatus
		{
			// Token: 0x04009469 RID: 37993
			Invalid,
			// Token: 0x0400946A RID: 37994
			LimitBreakLevelNotEnough,
			// Token: 0x0400946B RID: 37995
			CanLimitBreak,
			// Token: 0x0400946C RID: 37996
			LimitBreakMax,
			// Token: 0x0400946D RID: 37997
			TranscendenceLevelNotEnough,
			// Token: 0x0400946E RID: 37998
			CanTranscendence,
			// Token: 0x0400946F RID: 37999
			TranscendenceMax
		}
	}
}
