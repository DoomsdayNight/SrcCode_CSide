using System;
using System.Collections.Generic;
using System.Linq;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x020003F8 RID: 1016
	public class NKMEquipTuningManager
	{
		// Token: 0x06001AFD RID: 6909 RVA: 0x00076AD0 File Offset: 0x00074CD0
		public static void LoadFromLUA_EquipRandomStat(string fileName)
		{
			NKMEquipTuningManager.equipRandomStatGroups = (from e in NKMTempletLoader.LoadCommonPath<NKMEquipRandomStatTemplet>("AB_SCRIPT", fileName, "ITEM_EQUIP_RANDOM_STAT", new Func<NKMLua, NKMEquipRandomStatTemplet>(NKMEquipRandomStatTemplet.LoadFromLUA))
			group e by e.m_StatGroupID).ToDictionary((IGrouping<int, NKMEquipRandomStatTemplet> e) => e.Key, (IGrouping<int, NKMEquipRandomStatTemplet> e) => new NKMEquipStatGroupTemplet(e));
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x00076B68 File Offset: 0x00074D68
		public static void Validate()
		{
			foreach (NKMEquipRandomStatTemplet nkmequipRandomStatTemplet in NKMEquipTuningManager.equipRandomStatGroups.Values.SelectMany((NKMEquipStatGroupTemplet e) => e.Values))
			{
				nkmequipRandomStatTemplet.Validate();
			}
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x00076BDC File Offset: 0x00074DDC
		public static bool TryGetStatGroupTemplet(int groupId, out NKMEquipStatGroupTemplet result)
		{
			return NKMEquipTuningManager.equipRandomStatGroups.TryGetValue(groupId, out result);
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x00076BEC File Offset: 0x00074DEC
		public static IReadOnlyList<NKMEquipRandomStatTemplet> GetEquipRandomStatGroupList(int statGroupID)
		{
			NKMEquipStatGroupTemplet nkmequipStatGroupTemplet;
			NKMEquipTuningManager.equipRandomStatGroups.TryGetValue(statGroupID, out nkmequipStatGroupTemplet);
			return ((nkmequipStatGroupTemplet != null) ? nkmequipStatGroupTemplet.Values : null) ?? null;
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x00076C18 File Offset: 0x00074E18
		public static bool IsChangeableStatGroup(int statGroupID)
		{
			NKMEquipStatGroupTemplet nkmequipStatGroupTemplet;
			return NKMEquipTuningManager.equipRandomStatGroups.TryGetValue(statGroupID, out nkmequipStatGroupTemplet) && nkmequipStatGroupTemplet.Count > 1;
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x00076C40 File Offset: 0x00074E40
		public static NKMEquipRandomStatTemplet GetEquipRandomStat(int statGroupID, NKM_STAT_TYPE statType)
		{
			NKMEquipStatGroupTemplet nkmequipStatGroupTemplet;
			if (!NKMEquipTuningManager.equipRandomStatGroups.TryGetValue(statGroupID, out nkmequipStatGroupTemplet))
			{
				return null;
			}
			NKMEquipRandomStatTemplet result;
			nkmequipStatGroupTemplet.TryGetValue(statType, out result);
			return result;
		}

		// Token: 0x040013F2 RID: 5106
		private static Dictionary<int, NKMEquipStatGroupTemplet> equipRandomStatGroups;

		// Token: 0x040013F3 RID: 5107
		public const int MAX_PRECISION = 100;

		// Token: 0x040013F4 RID: 5108
		public const int CREDIT_COST_REFINE = 150;

		// Token: 0x040013F5 RID: 5109
		public const int TUNING_MATERIAL_COST_REFINE = 1;

		// Token: 0x040013F6 RID: 5110
		public const int CREDIT_COST_STAT_CHANGE = 750;

		// Token: 0x040013F7 RID: 5111
		public const int TUNING_MATERIAL_COST_STAT_CHANGE = 1;
	}
}
