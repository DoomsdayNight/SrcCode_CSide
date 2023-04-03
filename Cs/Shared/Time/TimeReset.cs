using System;
using Cs.Core.Util;

namespace Cs.Shared.Time
{
	// Token: 0x020010BA RID: 4282
	public static class TimeReset
	{
		// Token: 0x06009CB2 RID: 40114 RVA: 0x0033656C File Offset: 0x0033476C
		public static DateTime CalcNextResetUtc(TimeResetType resetType)
		{
			DateTime recent = ServiceTime.Recent;
			switch (resetType)
			{
			case TimeResetType.Day:
				return ServiceTime.ToUtcTime(DailyReset.CalcNextReset(recent));
			case TimeResetType.Week:
				return ServiceTime.ToUtcTime(WeeklyReset.CalcNextReset(recent, DayOfWeek.Monday));
			case TimeResetType.Month:
				return ServiceTime.ToUtcTime(MonthlyReset.CalcNextReset(recent));
			default:
				throw new Exception(string.Format("[TimeUtil] invalid resetType:{0}", resetType));
			}
		}

		// Token: 0x06009CB3 RID: 40115 RVA: 0x003365D0 File Offset: 0x003347D0
		public static bool IsOutOfDateUtc(DateTime utcTime, TimeResetType resetType)
		{
			DateTime serviceTime = TimeReset.CalcLastReset(resetType);
			return utcTime <= ServiceTime.ToUtcTime(serviceTime);
		}

		// Token: 0x06009CB4 RID: 40116 RVA: 0x003365F0 File Offset: 0x003347F0
		public static DateTime CalcLastReset(TimeResetType resetType)
		{
			DateTime recent = ServiceTime.Recent;
			switch (resetType)
			{
			case TimeResetType.Day:
				return DailyReset.CalcLastReset(recent);
			case TimeResetType.Week:
				return WeeklyReset.CalcLastReset(recent, DayOfWeek.Monday);
			case TimeResetType.Month:
				return MonthlyReset.CalcLastReset(recent);
			default:
				throw new Exception(string.Format("[TimeUtil] invalid resetType:{0}", resetType));
			}
		}

		// Token: 0x04009078 RID: 36984
		public const int ResetHour = 4;

		// Token: 0x04009079 RID: 36985
		public static readonly TimeSpan ResetHourSpan = TimeSpan.FromHours(4.0);
	}
}
