using System;
using System.Collections.Generic;
using ClientPacket.Raid;
using NKC;

namespace NKM
{
	// Token: 0x02000368 RID: 872
	public static class NKMCoopRaidDataEx
	{
		// Token: 0x06001515 RID: 5397 RVA: 0x0004F601 File Offset: 0x0004D801
		public static bool IsOnGoing(this NKMCoopRaidData data)
		{
			return !NKCSynchronizedTime.IsFinished(data.expireDate) && data.curHP > 0f;
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x0004F620 File Offset: 0x0004D820
		public static void SortJoinDataByDamage(this NKMCoopRaidData data)
		{
			data.raidJoinDataList.Sort(new NKMCoopRaidDataEx.Comp());
		}

		// Token: 0x02001181 RID: 4481
		public class Comp : IComparer<NKMRaidJoinData>
		{
			// Token: 0x06009FE4 RID: 40932 RVA: 0x0033D60D File Offset: 0x0033B80D
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
