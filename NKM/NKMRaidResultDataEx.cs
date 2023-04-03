using System;
using System.Collections.Generic;
using ClientPacket.Raid;
using NKC;

namespace NKM
{
	// Token: 0x02000367 RID: 871
	public static class NKMRaidResultDataEx
	{
		// Token: 0x06001513 RID: 5395 RVA: 0x0004F5D0 File Offset: 0x0004D7D0
		public static bool IsOnGoing(this NKMRaidResultData data)
		{
			return !NKCSynchronizedTime.IsFinished(data.expireDate) && data.curHP > 0f;
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x0004F5EF File Offset: 0x0004D7EF
		public static void SortJoinDataByDamage(this NKMRaidResultData data)
		{
			data.raidJoinDataList.Sort(new NKMRaidResultDataEx.Comp());
		}

		// Token: 0x02001180 RID: 4480
		public class Comp : IComparer<NKMRaidJoinData>
		{
			// Token: 0x06009FE2 RID: 40930 RVA: 0x0033D5D2 File Offset: 0x0033B7D2
			public int Compare(NKMRaidJoinData x, NKMRaidJoinData y)
			{
				if (y.damage > x.damage)
				{
					return 1;
				}
				if (y.damage < x.damage)
				{
					return -1;
				}
				if (y.userUID <= x.userUID)
				{
					return 1;
				}
				return -1;
			}
		}
	}
}
