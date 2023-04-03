using System;
using System.Collections.Generic;
using System.Linq;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;

namespace NKC
{
	// Token: 0x020007FF RID: 2047
	public static class NKCRearmamentUtil
	{
		// Token: 0x0600510F RID: 20751 RVA: 0x00189668 File Offset: 0x00187868
		public static void Init()
		{
			NKMUnitExtractBonuseTemplet.LoadFromLua();
			NKMTempletContainer<NKMUnitRearmamentTemplet>.Load("AB_SCRIPT", "LUA_REARMAMENT_TEMPLET", "REARMAMENT_TEMPLET", new Func<NKMLua, NKMUnitRearmamentTemplet>(NKMUnitRearmamentTemplet.LoadFromLua));
			foreach (NKMUnitRearmamentTemplet nkmunitRearmamentTemplet in NKMTempletContainer<NKMUnitRearmamentTemplet>.Values)
			{
				nkmunitRearmamentTemplet.Join();
			}
		}

		// Token: 0x06005110 RID: 20752 RVA: 0x001896D8 File Offset: 0x001878D8
		public static int GetFromUnitID(int rearmUnitID)
		{
			foreach (NKMUnitRearmamentTemplet nkmunitRearmamentTemplet in NKMTempletContainer<NKMUnitRearmamentTemplet>.Values)
			{
				if (nkmunitRearmamentTemplet.EnableByTag && nkmunitRearmamentTemplet.Key == rearmUnitID)
				{
					return nkmunitRearmamentTemplet.FromUnitTemplet.m_UnitID;
				}
			}
			return 0;
		}

		// Token: 0x06005111 RID: 20753 RVA: 0x00189740 File Offset: 0x00187940
		public static bool IsCanRearmamentUnit(long unitUID)
		{
			NKMUnitData unitFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(unitUID);
			return unitFromUID != null && unitFromUID.m_UnitLevel >= 110 && NKMUnitLimitBreakManager.GetUnitLimitbreakStatus(unitFromUID) == NKMUnitLimitBreakManager.UnitLimitBreakStatus.TranscendenceMax && NKCRearmamentUtil.IsCanRearmamentUnit(NKMUnitManager.GetUnitTempletBase(unitFromUID.m_UnitID));
		}

		// Token: 0x06005112 RID: 20754 RVA: 0x0018978C File Offset: 0x0018798C
		public static bool IsCanRearmamentUnit(NKMUnitTempletBase unitTempletBase)
		{
			if (unitTempletBase == null)
			{
				return false;
			}
			bool result = false;
			foreach (NKMUnitRearmamentTemplet nkmunitRearmamentTemplet in NKMTempletContainer<NKMUnitRearmamentTemplet>.Values)
			{
				if (nkmunitRearmamentTemplet.EnableByTag && nkmunitRearmamentTemplet.FromUnitTemplet.m_UnitID == unitTempletBase.m_UnitID)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x06005113 RID: 20755 RVA: 0x00189800 File Offset: 0x00187A00
		public static bool IsHasLeaderSkill(long unitUID)
		{
			NKMUnitData unitFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(unitUID);
			return unitFromUID != null && NKCRearmamentUtil.IsHasLeaderSkill(unitFromUID);
		}

		// Token: 0x06005114 RID: 20756 RVA: 0x0018982C File Offset: 0x00187A2C
		public static bool IsHasLeaderSkill(NKMUnitData unitData)
		{
			bool result = false;
			using (List<NKMUnitSkillTemplet>.Enumerator enumerator = NKMUnitSkillManager.GetUnitAllSkillTemplets(unitData).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_LEADER)
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06005115 RID: 20757 RVA: 0x00189888 File Offset: 0x00187A88
		public static List<int> GetSameBaseUnitIDList(NKMUnitData unitData)
		{
			List<int> list = new List<int>();
			if (NKCRearmamentUtil.IsHasLeaderSkill(unitData))
			{
				list.Add(unitData.GetUnitTemplet().m_UnitTempletBase.m_BaseUnitID);
				NKMUnitRearmamentTemplet rearmamentTemplet = NKCRearmamentUtil.GetRearmamentTemplet(unitData.m_UnitID);
				if (rearmamentTemplet != null)
				{
					list.AddRange(NKCRearmamentUtil.GetSameBaseUnitIDList(rearmamentTemplet.FromUnitTemplet));
				}
			}
			else
			{
				list = NKCRearmamentUtil.GetSameBaseUnitIDList(unitData.GetUnitTemplet().m_UnitTempletBase);
			}
			return list;
		}

		// Token: 0x06005116 RID: 20758 RVA: 0x001898F0 File Offset: 0x00187AF0
		private static List<int> GetSameBaseUnitIDList(NKMUnitTempletBase targetBaseUnitTemplet)
		{
			List<int> list = new List<int>();
			if (NKCRearmamentUtil.IsCanRearmamentUnit(targetBaseUnitTemplet))
			{
				foreach (NKMUnitRearmamentTemplet nkmunitRearmamentTemplet in NKCRearmamentUtil.GetRearmamentTargetTemplets(targetBaseUnitTemplet))
				{
					if (nkmunitRearmamentTemplet.EnableByTag)
					{
						list.Add(nkmunitRearmamentTemplet.ToUnitTemplet.m_UnitID);
					}
				}
			}
			return list;
		}

		// Token: 0x06005117 RID: 20759 RVA: 0x00189964 File Offset: 0x00187B64
		public static List<NKMUnitRearmamentTemplet> GetRearmamentTargetTemplets(NKMUnitTempletBase rearmTargetUnitTemplet)
		{
			if (rearmTargetUnitTemplet == null)
			{
				return null;
			}
			return NKCRearmamentUtil.GetRearmamentTargetTemplets(rearmTargetUnitTemplet.m_UnitID);
		}

		// Token: 0x06005118 RID: 20760 RVA: 0x00189978 File Offset: 0x00187B78
		public static List<NKMUnitRearmamentTemplet> GetRearmamentTargetTemplets(int unitID)
		{
			List<NKMUnitRearmamentTemplet> list = new List<NKMUnitRearmamentTemplet>();
			foreach (NKMUnitRearmamentTemplet nkmunitRearmamentTemplet in NKMTempletContainer<NKMUnitRearmamentTemplet>.Values)
			{
				if (nkmunitRearmamentTemplet.EnableByTag && nkmunitRearmamentTemplet.FromUnitTemplet.m_UnitID == unitID)
				{
					list.Add(nkmunitRearmamentTemplet);
				}
			}
			from x in list
			orderby x.Key
			select x;
			return list;
		}

		// Token: 0x06005119 RID: 20761 RVA: 0x00189A08 File Offset: 0x00187C08
		public static NKMUnitRearmamentTemplet GetRearmamentTemplet(int unitID)
		{
			foreach (NKMUnitRearmamentTemplet nkmunitRearmamentTemplet in NKMTempletContainer<NKMUnitRearmamentTemplet>.Values)
			{
				if (nkmunitRearmamentTemplet.EnableByTag && nkmunitRearmamentTemplet.Key == unitID)
				{
					return nkmunitRearmamentTemplet;
				}
			}
			return null;
		}

		// Token: 0x0600511A RID: 20762 RVA: 0x00189A68 File Offset: 0x00187C68
		public static bool IsCanUseContent()
		{
			return NKMContentsVersionManager.HasTag("REARMAMENT_BASE");
		}

		// Token: 0x0600511B RID: 20763 RVA: 0x00189A74 File Offset: 0x00187C74
		public static int GetSynergyIncreasePercentage(List<long> lstUnits)
		{
			int num = 0;
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			foreach (long unitUid in lstUnits)
			{
				NKMUnitData unitFromUID = armyData.GetUnitFromUID(unitUid);
				if (unitFromUID != null)
				{
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitFromUID.m_UnitID);
					if (unitTempletBase.m_bAwaken)
					{
						num += NKMCommonConst.ExtractBonusRatePercent_Awaken;
					}
					else if (unitTempletBase.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_SSR)
					{
						num += NKMCommonConst.ExtractBonusRatePercent_SSR;
					}
					else if (unitTempletBase.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_SR)
					{
						num += NKMCommonConst.ExtractBonusRatePercent_SR;
					}
				}
			}
			num = Mathf.Min(num, 100);
			return num;
		}
	}
}
