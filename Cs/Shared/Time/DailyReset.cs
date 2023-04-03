using System;

namespace Cs.Shared.Time
{
	// Token: 0x020010B7 RID: 4279
	public static class DailyReset
	{
		// Token: 0x06009CA9 RID: 40105 RVA: 0x003363F3 File Offset: 0x003345F3
		public static bool IsOutOfDate(DateTime current, DateTime dateTime)
		{
			return dateTime <= DailyReset.CalcLastReset(current);
		}

		// Token: 0x06009CAA RID: 40106 RVA: 0x00336404 File Offset: 0x00334604
		public static DateTime CalcNextReset(DateTime current)
		{
			return DailyReset.CalcLastReset(current).AddDays(1.0);
		}

		// Token: 0x06009CAB RID: 40107 RVA: 0x00336428 File Offset: 0x00334628
		public static DateTime CalcNextMidnight(DateTime current)
		{
			return current.Date.AddDays(1.0);
		}

		// Token: 0x06009CAC RID: 40108 RVA: 0x00336450 File Offset: 0x00334650
		public static DateTime CalcLastReset(DateTime current)
		{
			DateTime dateTime = current.Date + TimeReset.ResetHourSpan;
			if (current.TimeOfDay < TimeReset.ResetHourSpan)
			{
				dateTime -= TimeSpan.FromDays(1.0);
			}
			return dateTime;
		}

		// Token: 0x06009CAD RID: 40109 RVA: 0x00336498 File Offset: 0x00334698
		public static DateTime GetDateExpression(DateTime current)
		{
			DateTime dateTime = current.Date;
			if (current.TimeOfDay < TimeReset.ResetHourSpan)
			{
				dateTime -= TimeSpan.FromDays(1.0);
			}
			return dateTime;
		}
	}
}
