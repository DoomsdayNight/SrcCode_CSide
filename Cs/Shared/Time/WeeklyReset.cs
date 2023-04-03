using System;

namespace Cs.Shared.Time
{
	// Token: 0x020010BB RID: 4283
	public static class WeeklyReset
	{
		// Token: 0x06009CB6 RID: 40118 RVA: 0x00336657 File Offset: 0x00334857
		public static bool IsOutOfDate(DateTime current, DateTime dateTime, DayOfWeek dayOfWeek)
		{
			return dateTime <= WeeklyReset.CalcLastReset(current, dayOfWeek);
		}

		// Token: 0x06009CB7 RID: 40119 RVA: 0x00336668 File Offset: 0x00334868
		public static DateTime CalcNextReset(DateTime current, DayOfWeek dayOfWeek)
		{
			return WeeklyReset.CalcLastReset(current, dayOfWeek).AddDays(7.0);
		}

		// Token: 0x06009CB8 RID: 40120 RVA: 0x00336690 File Offset: 0x00334890
		internal static DateTime CalcLastReset(DateTime current, DayOfWeek dayOfWeek)
		{
			DateTime dateTime = new DateTime(current.Year, current.Month, current.Day, 4, 0, 0);
			int num = (dayOfWeek - dateTime.DayOfWeek + 7) % 7;
			DateTime dateTime2 = dateTime.AddDays((double)num);
			if (current < dateTime2)
			{
				return dateTime2.AddDays(-7.0);
			}
			return dateTime2;
		}

		// Token: 0x06009CB9 RID: 40121 RVA: 0x003366F0 File Offset: 0x003348F0
		public static DateTime CalcNextReset(DateTime current, DayOfWeek dayOfWeek, TimeSpan resetHourSpan)
		{
			return WeeklyReset.CalcLastReset(current, dayOfWeek, resetHourSpan).AddDays(7.0);
		}

		// Token: 0x06009CBA RID: 40122 RVA: 0x00336718 File Offset: 0x00334918
		internal static DateTime CalcLastReset(DateTime current, DayOfWeek dayOfWeek, TimeSpan ResetHourSpan)
		{
			DateTime dateTime = current.Date + ResetHourSpan + TimeSpan.FromDays((double)(dayOfWeek - current.DayOfWeek));
			if (current < dateTime)
			{
				return dateTime.AddDays(-7.0);
			}
			return dateTime;
		}
	}
}
