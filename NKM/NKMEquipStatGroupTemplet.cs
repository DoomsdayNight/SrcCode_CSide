using System;
using System.Collections.Generic;
using System.Linq;
using Cs.Math;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x020003F6 RID: 1014
	public sealed class NKMEquipStatGroupTemplet
	{
		// Token: 0x06001AF6 RID: 6902 RVA: 0x00076908 File Offset: 0x00074B08
		public NKMEquipStatGroupTemplet(IGrouping<int, NKMEquipRandomStatTemplet> group)
		{
			this.GroupId = group.Key;
			NKM_STAT_TYPE[] array = (from e in @group
			group e by e.m_StatType into e
			where e.Count<NKMEquipRandomStatTemplet>() > 1
			select e.Key).ToArray<NKM_STAT_TYPE>();
			if (array.Any<NKM_STAT_TYPE>())
			{
				NKMTempletError.Add(string.Format("[EquipStat] duplicated type exist. groupId:{0} stats:{1}", this.GroupId, string.Join<NKM_STAT_TYPE>(", ", array)), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMEquipRandomStatTemplet.cs", 208);
			}
			this.stats = group.ToDictionary((NKMEquipRandomStatTemplet e) => e.m_StatType);
			this.list = group.ToList<NKMEquipRandomStatTemplet>();
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x00076A08 File Offset: 0x00074C08
		public int GroupId { get; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06001AF8 RID: 6904 RVA: 0x00076A10 File Offset: 0x00074C10
		public int Count
		{
			get
			{
				return this.stats.Count;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x00076A1D File Offset: 0x00074C1D
		public IReadOnlyList<NKMEquipRandomStatTemplet> Values
		{
			get
			{
				return this.list;
			}
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x00076A25 File Offset: 0x00074C25
		public bool TryGetValue(NKM_STAT_TYPE statType, out NKMEquipRandomStatTemplet result)
		{
			return this.stats.TryGetValue(statType, out result);
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x00076A34 File Offset: 0x00074C34
		public EQUIP_ITEM_STAT GenerateSubStat(NKM_STAT_TYPE statType, int precision)
		{
			NKMEquipRandomStatTemplet nkmequipRandomStatTemplet;
			if (!this.stats.TryGetValue(statType, out nkmequipRandomStatTemplet))
			{
				return null;
			}
			return nkmequipRandomStatTemplet.GenerateSubStat(precision);
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x00076A5C File Offset: 0x00074C5C
		public bool PickRandomStat(NKM_STAT_TYPE? exception, out NKMEquipRandomStatTemplet statTemplet)
		{
			IReadOnlyList<NKMEquipRandomStatTemplet> readOnlyList;
			if (exception != null)
			{
				readOnlyList = this.list.Where(delegate(NKMEquipRandomStatTemplet e)
				{
					NKM_STAT_TYPE statType = e.m_StatType;
					NKM_STAT_TYPE? exception2 = exception;
					return !(statType == exception2.GetValueOrDefault() & exception2 != null);
				}).ToArray<NKMEquipRandomStatTemplet>();
			}
			else
			{
				readOnlyList = this.list;
			}
			if (readOnlyList.Count == 0)
			{
				statTemplet = null;
				return false;
			}
			int index = RandomGenerator.ArrayIndex(readOnlyList.Count);
			statTemplet = readOnlyList[index];
			return true;
		}

		// Token: 0x040013EA RID: 5098
		private readonly Dictionary<NKM_STAT_TYPE, NKMEquipRandomStatTemplet> stats;

		// Token: 0x040013EB RID: 5099
		private readonly IReadOnlyList<NKMEquipRandomStatTemplet> list;
	}
}
