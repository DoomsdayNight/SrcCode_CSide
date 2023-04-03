using System;

namespace Cs.Shared.Time
{
	// Token: 0x020010B8 RID: 4280
	public static class MonthlyReset
	{
		// Token: 0x06009CAE RID: 40110 RVA: 0x003364D6 File Offset: 0x003346D6
		public static bool IsOutOfDate(DateTime current, DateTime dateTime)
		{
			return dateTime <= MonthlyReset.CalcLastReset(current);
		}

		// Token: 0x06009CAF RID: 40111 RVA: 0x003364E4 File Offset: 0x003346E4
		public static DateTime CalcNextReset(DateTime current)
		{
			return MonthlyReset.CalcLastReset(current).AddMonths(1);
		}

		// Token: 0x06009CB0 RID: 40112 RVA: 0x00336500 File Offset: 0x00334700
		public static DateTime CalcNextMidnight(DateTime current)
		{
			return new DateTime(current.Year, current.Month, 1, 0, 0, 0).AddMonths(1);
		}

		// Token: 0x06009CB1 RID: 40113 RVA: 0x00336530 File Offset: 0x00334730
		internal static DateTime CalcLastReset(DateTime current)
		{
			DateTime dateTime = new DateTime(current.Year, current.Month, 1, 4, 0, 0);
			if (current < dateTime)
			{
				return dateTime.AddMonths(-1);
			}
			return dateTime;
		}
	}
}
