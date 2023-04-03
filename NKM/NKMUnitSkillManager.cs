using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x020004B7 RID: 1207
	public static class NKMUnitSkillManager
	{
		// Token: 0x06002196 RID: 8598 RVA: 0x000ABC21 File Offset: 0x000A9E21
		public static IEnumerable<NKMUnitSkillTemplet> AllSkillTempletEnumerator()
		{
			foreach (NKMUnitSkillTempletContainer nkmunitSkillTempletContainer in NKMUnitSkillManager.m_dicSkillTempletContainer.Values)
			{
				foreach (NKMUnitSkillTemplet nkmunitSkillTemplet in nkmunitSkillTempletContainer.dicTemplets.Values)
				{
					yield return nkmunitSkillTemplet;
				}
				IEnumerator<NKMUnitSkillTemplet> enumerator2 = null;
			}
			Dictionary<int, NKMUnitSkillTempletContainer>.ValueCollection.Enumerator enumerator = default(Dictionary<int, NKMUnitSkillTempletContainer>.ValueCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x000ABC2C File Offset: 0x000A9E2C
		public static NKMUnitSkillTempletContainer GetSkillTempletContainer(int skillID)
		{
			NKMUnitSkillTempletContainer result;
			if (NKMUnitSkillManager.m_dicSkillTempletContainer.TryGetValue(skillID, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x000ABC4C File Offset: 0x000A9E4C
		public static NKMUnitSkillTempletContainer GetSkillTempletContainer(string skillStrID)
		{
			NKMUnitSkillTempletContainer result;
			if (NKMUnitSkillManager.m_dicSkillTempletContainerByStrID.TryGetValue(skillStrID, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x000ABC6C File Offset: 0x000A9E6C
		public static List<NKMUnitSkillTemplet> GetUnitAllSkillTemplets(NKMUnitData unitData)
		{
			List<NKMUnitSkillTemplet> list = new List<NKMUnitSkillTemplet>();
			if (unitData != null)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID);
				if (unitTempletBase != null)
				{
					for (int i = 0; i < unitTempletBase.GetSkillCount(); i++)
					{
						string skillStrID = unitTempletBase.GetSkillStrID(i);
						if (!string.IsNullOrEmpty(skillStrID))
						{
							NKMUnitSkillTempletContainer skillTempletContainer = NKMUnitSkillManager.GetSkillTempletContainer(skillStrID);
							if (skillTempletContainer != null)
							{
								NKMUnitSkillTemplet skillTemplet = skillTempletContainer.GetSkillTemplet(unitData.GetSkillLevel(skillStrID));
								if (skillTemplet != null)
								{
									list.Add(skillTemplet);
								}
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x000ABCDC File Offset: 0x000A9EDC
		public static NKM_ERROR_CODE CanTrainSkill(NKMUserData userData, NKMUnitData targetUnit, int skillID)
		{
			if (targetUnit == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_NOT_EXIST;
			}
			if (targetUnit.GetUnitSkillIndex(skillID) < 0)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_SKILL_NOT_EXIST;
			}
			int unitSkillLevel = targetUnit.GetUnitSkillLevel(skillID);
			NKMUnitSkillTemplet skillTemplet = NKMUnitSkillManager.GetSkillTemplet(skillID, unitSkillLevel);
			NKMUnitSkillTemplet skillTemplet2 = NKMUnitSkillManager.GetSkillTemplet(skillID, unitSkillLevel + 1);
			if (skillTemplet == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_SKILL_NOT_EXIST;
			}
			if (skillTemplet2 == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_SKILL_ALREADY_MAX;
			}
			if (NKMUnitSkillManager.IsLockedSkill(skillTemplet.m_ID, (int)targetUnit.m_LimitBreakLevel))
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_SKILL_NEED_LIMIT_BREAK;
			}
			int maxSkillLevelFromLimitBreakLevel = NKMUnitSkillManager.GetMaxSkillLevelFromLimitBreakLevel(skillID, (int)targetUnit.m_LimitBreakLevel);
			if (skillTemplet2.m_Level > maxSkillLevelFromLimitBreakLevel)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_SKILL_NEED_LIMIT_BREAK;
			}
			foreach (NKMUnitSkillTemplet.NKMUpgradeReqItem nkmupgradeReqItem in skillTemplet2.m_lstUpgradeReqItem)
			{
				if (userData.m_InventoryData.GetCountMiscItem(nkmupgradeReqItem.ItemID) < (long)nkmupgradeReqItem.ItemCount)
				{
					return NKM_ERROR_CODE.NEC_FAIL_UNIT_SKILL_NOT_ENOUGH_ITEM;
				}
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x000ABDD0 File Offset: 0x000A9FD0
		public static int GetMaxSkillLevel(string skillStrID)
		{
			NKMUnitSkillTempletContainer skillTempletContainer = NKMUnitSkillManager.GetSkillTempletContainer(skillStrID);
			if (skillTempletContainer != null && skillTempletContainer.dicTemplets != null)
			{
				return skillTempletContainer.dicTemplets.Count;
			}
			return 0;
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x000ABDFC File Offset: 0x000A9FFC
		public static int GetMaxSkillLevel(int skillID)
		{
			NKMUnitSkillTempletContainer skillTempletContainer = NKMUnitSkillManager.GetSkillTempletContainer(skillID);
			if (skillTempletContainer != null && skillTempletContainer.dicTemplets != null)
			{
				return skillTempletContainer.dicTemplets.Count;
			}
			return 0;
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x000ABE28 File Offset: 0x000AA028
		public static int GetMaxSkillLevelFromLimitBreakLevel(int skillID, int LBLevel)
		{
			NKMUnitSkillTempletContainer skillTempletContainer = NKMUnitSkillManager.GetSkillTempletContainer(skillID);
			if (skillTempletContainer != null && skillTempletContainer.dicTemplets != null)
			{
				foreach (KeyValuePair<int, NKMUnitSkillTemplet> keyValuePair in skillTempletContainer.dicTemplets)
				{
					NKMUnitSkillTemplet value = keyValuePair.Value;
					if (value != null && value.m_UnlockReqUpgrade == LBLevel + 1)
					{
						return value.m_Level - 1;
					}
				}
				return skillTempletContainer.dicTemplets.Count;
			}
			return 0;
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x000ABEB4 File Offset: 0x000AA0B4
		public static bool IsLockedSkill(int skillId, int unitLimitBreakLevel)
		{
			return NKMUnitSkillManager.GetUnlockReqUpgradeFromSkillId(skillId) > unitLimitBreakLevel;
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x000ABEC0 File Offset: 0x000AA0C0
		public static int GetUnlockReqUpgradeFromSkillId(int skillId)
		{
			NKMUnitSkillTemplet skillTemplet = NKMUnitSkillManager.GetSkillTemplet(skillId, 1);
			if (skillTemplet != null)
			{
				return skillTemplet.m_UnlockReqUpgrade;
			}
			return 0;
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x000ABEE0 File Offset: 0x000AA0E0
		public static bool CheckHaveUpgradableSkill(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				return false;
			}
			if (NKMUnitManager.GetUnitTempletBase(unitData) == null)
			{
				return false;
			}
			int unitSkillCount = unitData.GetUnitSkillCount();
			for (int i = 0; i < unitSkillCount; i++)
			{
				NKMUnitSkillTemplet unitSkillTempletByIndex = unitData.GetUnitSkillTempletByIndex(i);
				if (unitSkillTempletByIndex == null)
				{
					Log.Error("Unit has skill but can't find templet. unitID : " + unitData.m_UnitID.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitSkillManager.cs", 362);
				}
				else if (!NKMUnitSkillManager.IsLockedSkill(unitSkillTempletByIndex.m_ID, (int)unitData.m_LimitBreakLevel))
				{
					string strID = unitSkillTempletByIndex.m_strID;
					int level = unitSkillTempletByIndex.m_Level;
					NKMUnitSkillTemplet skillTemplet = NKMUnitSkillManager.GetSkillTemplet(strID, level + 1);
					if (skillTemplet != null)
					{
						int maxSkillLevelFromLimitBreakLevel = NKMUnitSkillManager.GetMaxSkillLevelFromLimitBreakLevel(unitSkillTempletByIndex.m_ID, (int)unitData.m_LimitBreakLevel);
						if (skillTemplet.m_Level <= maxSkillLevelFromLimitBreakLevel)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x000ABF9C File Offset: 0x000AA19C
		public static NKMUnitSkillTemplet GetUnitSkillTemplet(string strID, NKMUnitData unitData)
		{
			if (string.IsNullOrEmpty(strID))
			{
				return null;
			}
			int skillLevel = unitData.GetSkillLevel(strID);
			return NKMUnitSkillManager.GetSkillTemplet(strID, skillLevel);
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x000ABFC2 File Offset: 0x000AA1C2
		public static NKMUnitSkillTemplet GetUnitSkillTemplet(string strID, int skillLevel)
		{
			if (string.IsNullOrEmpty(strID))
			{
				return null;
			}
			return NKMUnitSkillManager.GetSkillTemplet(strID, skillLevel);
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x000ABFD5 File Offset: 0x000AA1D5
		public static NKMUnitSkillTemplet GetUnitSkillTemplet(int skillID, NKMUnitData unitData)
		{
			return NKMUnitSkillManager.GetUnitSkillTemplet(NKMUnitSkillManager.GetSkillStrID(skillID), unitData);
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x000ABFE4 File Offset: 0x000AA1E4
		public static string GetSkillStrID(int ID)
		{
			NKMUnitSkillTempletContainer skillTempletContainer = NKMUnitSkillManager.GetSkillTempletContainer(ID);
			if (skillTempletContainer != null)
			{
				return skillTempletContainer.SkillStrID;
			}
			Log.Error("Skill Templet for ID " + ID.ToString() + " Not Found", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitSkillManager.cs", 419);
			return string.Empty;
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x000AC02C File Offset: 0x000AA22C
		public static int GetSkillID(string strID)
		{
			NKMUnitSkillTempletContainer skillTempletContainer = NKMUnitSkillManager.GetSkillTempletContainer(strID);
			if (skillTempletContainer != null)
			{
				return skillTempletContainer.SkillID;
			}
			return -1;
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x000AC04C File Offset: 0x000AA24C
		public static NKMUnitSkillTemplet GetSkillTemplet(int ID, int level)
		{
			NKMUnitSkillTempletContainer skillTempletContainer = NKMUnitSkillManager.GetSkillTempletContainer(ID);
			if (skillTempletContainer != null)
			{
				return skillTempletContainer.GetSkillTemplet(level);
			}
			return null;
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x000AC06C File Offset: 0x000AA26C
		public static NKMUnitSkillTemplet GetSkillTemplet(string strID, int level)
		{
			if (string.IsNullOrEmpty(strID))
			{
				return null;
			}
			NKMUnitSkillTempletContainer skillTempletContainer = NKMUnitSkillManager.GetSkillTempletContainer(strID);
			if (skillTempletContainer != null)
			{
				return skillTempletContainer.GetSkillTemplet(level);
			}
			return null;
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x000AC098 File Offset: 0x000AA298
		public static bool LoadFromLUA(string filename)
		{
			Dictionary<int, NKMUnitSkillTempletContainer> dicSkillTempletContainer = NKMUnitSkillManager.m_dicSkillTempletContainer;
			if (dicSkillTempletContainer != null)
			{
				dicSkillTempletContainer.Clear();
			}
			Dictionary<string, NKMUnitSkillTempletContainer> dicSkillTempletContainerByStrID = NKMUnitSkillManager.m_dicSkillTempletContainerByStrID;
			if (dicSkillTempletContainerByStrID != null)
			{
				dicSkillTempletContainerByStrID.Clear();
			}
			IEnumerable<NKMUnitSkillTemplet> enumerable = NKMTempletLoader.LoadCommonPath<NKMUnitSkillTemplet>("AB_SCRIPT_UNIT_DATA", filename, "m_UnitSkillTemplet", new Func<NKMLua, NKMUnitSkillTemplet>(NKMUnitSkillTemplet.LoadFromLUA));
			if (enumerable == null)
			{
				Log.ErrorAndExit("Cannot found " + filename + "-m_UnitSkillTemplet", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitSkillManager.cs", 466);
				return false;
			}
			foreach (NKMUnitSkillTemplet nkmunitSkillTemplet in enumerable)
			{
				NKMUnitSkillTempletContainer nkmunitSkillTempletContainer;
				if (!NKMUnitSkillManager.m_dicSkillTempletContainer.TryGetValue(nkmunitSkillTemplet.m_ID, out nkmunitSkillTempletContainer))
				{
					nkmunitSkillTempletContainer = new NKMUnitSkillTempletContainer(nkmunitSkillTemplet.m_ID, nkmunitSkillTemplet.m_strID);
					NKMUnitSkillManager.m_dicSkillTempletContainer.Add(nkmunitSkillTemplet.m_ID, nkmunitSkillTempletContainer);
					NKMUnitSkillManager.m_dicSkillTempletContainerByStrID.Add(nkmunitSkillTemplet.m_strID, nkmunitSkillTempletContainer);
				}
				if (nkmunitSkillTempletContainer.SkillStrID != nkmunitSkillTemplet.m_strID)
				{
					Log.ErrorAndExit(string.Format("Skill ID and SkillStrID Mismatch : ID {0}, StrID {1}, level {2}", nkmunitSkillTemplet.m_ID, nkmunitSkillTemplet.m_strID, nkmunitSkillTemplet.m_Level), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitSkillManager.cs", 482);
				}
				nkmunitSkillTempletContainer.AddSkillTemplet(nkmunitSkillTemplet);
			}
			return true;
		}

		// Token: 0x04002262 RID: 8802
		private static Dictionary<int, NKMUnitSkillTempletContainer> m_dicSkillTempletContainer = new Dictionary<int, NKMUnitSkillTempletContainer>();

		// Token: 0x04002263 RID: 8803
		private static Dictionary<string, NKMUnitSkillTempletContainer> m_dicSkillTempletContainerByStrID = new Dictionary<string, NKMUnitSkillTempletContainer>();
	}
}
