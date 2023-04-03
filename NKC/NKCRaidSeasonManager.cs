using System;
using ClientPacket.Raid;
using NKM.Templet;

namespace NKC
{
	// Token: 0x020006BE RID: 1726
	public static class NKCRaidSeasonManager
	{
		// Token: 0x06003AE4 RID: 15076 RVA: 0x0012EBF0 File Offset: 0x0012CDF0
		public static NKMRaidSeasonTemplet GetNowSeasonTemplet()
		{
			NKMRaidSeasonTemplet nkmraidSeasonTemplet = NKMRaidSeasonTemplet.Find(NKCRaidSeasonManager.RaidSeason.seasonId);
			if (nkmraidSeasonTemplet == null)
			{
				return null;
			}
			if (nkmraidSeasonTemplet.IntervalTemplet.GetStartDateUtc() > NKCSynchronizedTime.GetServerUTCTime(0.0) || nkmraidSeasonTemplet.IntervalTemplet.GetEndDateUtc() <= NKCSynchronizedTime.GetServerUTCTime(0.0))
			{
				return null;
			}
			return nkmraidSeasonTemplet;
		}

		// Token: 0x04003530 RID: 13616
		public static NKMRaidSeason RaidSeason = new NKMRaidSeason();
	}
}
