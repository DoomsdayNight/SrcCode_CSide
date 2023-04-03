using System;
using NKM;

namespace NKC
{
	// Token: 0x020006F5 RID: 1781
	public static class NKMTime
	{
		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06003F37 RID: 16183 RVA: 0x0014887E File Offset: 0x00146A7E
		public static DateTime UtcEpoch
		{
			get
			{
				return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06003F38 RID: 16184 RVA: 0x00148890 File Offset: 0x00146A90
		public static DateTime LocalEpoch
		{
			get
			{
				return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06003F39 RID: 16185 RVA: 0x001488A2 File Offset: 0x00146AA2
		public static int INTERVAL_FROM_UTC
		{
			get
			{
				return (int)NKMTime.s_tsServiceTimeOffset.TotalHours;
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06003F3A RID: 16186 RVA: 0x001488AF File Offset: 0x00146AAF
		public static int RESET_TIME_BASE_UTC
		{
			get
			{
				if (4 >= NKMTime.INTERVAL_FROM_UTC)
				{
					return 4 - NKMTime.INTERVAL_FROM_UTC;
				}
				return 28 - NKMTime.INTERVAL_FROM_UTC;
			}
		}

		// Token: 0x06003F3B RID: 16187 RVA: 0x001488CC File Offset: 0x00146ACC
		public static long ToUnixTime(this DateTime datetime)
		{
			if (datetime.Kind == DateTimeKind.Utc)
			{
				return (long)(datetime - NKMTime.UtcEpoch).TotalSeconds;
			}
			return (long)(datetime - NKMTime.LocalEpoch).TotalSeconds;
		}

		// Token: 0x06003F3C RID: 16188 RVA: 0x0014890C File Offset: 0x00146B0C
		public static DateTime GetNextResetTime(DateTime baseTimeUTC, NKM_MISSION_RESET_INTERVAL resetInterval)
		{
			NKMTime.TimePeriod timePeriod = NKMTime.TimePeriod.Day;
			if (resetInterval == NKM_MISSION_RESET_INTERVAL.DAILY)
			{
				timePeriod = NKMTime.TimePeriod.Day;
			}
			else if (resetInterval == NKM_MISSION_RESET_INTERVAL.WEEKLY)
			{
				timePeriod = NKMTime.TimePeriod.Week;
			}
			else if (resetInterval == NKM_MISSION_RESET_INTERVAL.MONTHLY)
			{
				timePeriod = NKMTime.TimePeriod.Month;
			}
			return NKMTime.GetNextResetTime(baseTimeUTC, timePeriod);
		}

		// Token: 0x06003F3D RID: 16189 RVA: 0x00148938 File Offset: 0x00146B38
		public static DateTime GetNextResetTime(DateTime baseTimeUTC, NKMTime.TimePeriod timePeriod)
		{
			switch (timePeriod)
			{
			case NKMTime.TimePeriod.Week:
			{
				DateTime dateTime = new DateTime(baseTimeUTC.Year, baseTimeUTC.Month, baseTimeUTC.Day, NKMTime.RESET_TIME_BASE_UTC, 0, 0);
				int num = (int)((DayOfWeek.Sunday - (int)dateTime.DayOfWeek + 7) % (DayOfWeek)7);
				DateTime dateTime2 = dateTime.AddDays((double)num);
				if (baseTimeUTC < dateTime2)
				{
					return dateTime2;
				}
				return dateTime2.AddDays(7.0);
			}
			case NKMTime.TimePeriod.Month:
			{
				DateTime dateTime3 = new DateTime(baseTimeUTC.Year, baseTimeUTC.Month, DateTime.DaysInMonth(baseTimeUTC.Year, baseTimeUTC.Month), NKMTime.RESET_TIME_BASE_UTC, 0, 0);
				if (baseTimeUTC < dateTime3)
				{
					return dateTime3;
				}
				DateTime dateTime4 = dateTime3.AddMonths(1);
				return new DateTime(dateTime4.Year, dateTime4.Month, DateTime.DaysInMonth(dateTime4.Year, dateTime4.Month), NKMTime.RESET_TIME_BASE_UTC, 0, 0);
			}
			}
			DateTime dateTime5 = new DateTime(baseTimeUTC.Year, baseTimeUTC.Month, baseTimeUTC.Day, NKMTime.RESET_TIME_BASE_UTC, 0, 0);
			if (baseTimeUTC < dateTime5)
			{
				return dateTime5;
			}
			return dateTime5.AddDays(1.0);
		}

		// Token: 0x06003F3E RID: 16190 RVA: 0x00148A68 File Offset: 0x00146C68
		public static DateTime GetNextResetTime(DateTime baseTimeUTC, NKMTime.TimePeriod timePeriod, int resetHour)
		{
			switch (timePeriod)
			{
			case NKMTime.TimePeriod.Week:
			{
				DateTime dateTime = new DateTime(baseTimeUTC.Year, baseTimeUTC.Month, baseTimeUTC.Day, resetHour, 0, 0);
				int num = (int)((DayOfWeek.Sunday - (int)dateTime.DayOfWeek + 7) % (DayOfWeek)7);
				DateTime dateTime2 = dateTime.AddDays((double)num);
				if (baseTimeUTC < dateTime2)
				{
					return dateTime2;
				}
				return dateTime2.AddDays(7.0);
			}
			case NKMTime.TimePeriod.Month:
			{
				DateTime dateTime3 = new DateTime(baseTimeUTC.Year, baseTimeUTC.Month, DateTime.DaysInMonth(baseTimeUTC.Year, baseTimeUTC.Month), resetHour, 0, 0);
				if (baseTimeUTC < dateTime3)
				{
					return dateTime3;
				}
				DateTime dateTime4 = dateTime3.AddMonths(1);
				return new DateTime(dateTime4.Year, dateTime4.Month, DateTime.DaysInMonth(dateTime4.Year, dateTime4.Month), NKMTime.RESET_TIME_BASE_UTC, 0, 0);
			}
			}
			DateTime dateTime5 = new DateTime(baseTimeUTC.Year, baseTimeUTC.Month, baseTimeUTC.Day, resetHour, 0, 0);
			if (baseTimeUTC < dateTime5)
			{
				return dateTime5;
			}
			return dateTime5.AddDays(1.0);
		}

		// Token: 0x06003F3F RID: 16191 RVA: 0x00148B8C File Offset: 0x00146D8C
		public static DateTime GetResetTime(DateTime baseTimeUTC, NKMTime.TimePeriod timePeriod)
		{
			switch (timePeriod)
			{
			case NKMTime.TimePeriod.Week:
				return NKMTime.GetNextResetTime(baseTimeUTC, timePeriod).AddDays(-7.0);
			case NKMTime.TimePeriod.Month:
				return NKMTime.GetNextResetTime(baseTimeUTC, timePeriod).AddMonths(-1);
			}
			return NKMTime.GetNextResetTime(baseTimeUTC, timePeriod).AddDays(-1.0);
		}

		// Token: 0x06003F40 RID: 16192 RVA: 0x00148BEF File Offset: 0x00146DEF
		[Obsolete("NKCSynchronizedTime.IsEventTime 사용할 것. interval 관련 인터페이스들이 구현되어 있음.")]
		public static bool IsEventTime(DateTime currentTime, DateTime StartTime, DateTime finishTime)
		{
			return NKCSynchronizedTime.IsEventTime(currentTime, StartTime, finishTime);
		}

		// Token: 0x06003F41 RID: 16193 RVA: 0x00148BFC File Offset: 0x00146DFC
		public static DateTime LocalToUTC(DateTime koreaLocalTime, int fAddHour = 0)
		{
			if (koreaLocalTime.Ticks == 0L || koreaLocalTime == DateTime.MinValue || koreaLocalTime == DateTime.MaxValue)
			{
				return koreaLocalTime;
			}
			return koreaLocalTime.AddHours((double)(-(double)NKMTime.INTERVAL_FROM_UTC)).AddHours((double)fAddHour);
		}

		// Token: 0x06003F42 RID: 16194 RVA: 0x00148C48 File Offset: 0x00146E48
		public static DateTime UTCtoLocal(DateTime utcTime, int fAddHour = 0)
		{
			if (utcTime.Ticks == 0L || utcTime == DateTime.MinValue || utcTime == DateTime.MaxValue)
			{
				return utcTime;
			}
			return utcTime.AddHours((double)NKMTime.INTERVAL_FROM_UTC).AddHours((double)fAddHour);
		}

		// Token: 0x0400373D RID: 14141
		private const DayOfWeek WEEKLY_RESET_DAY = DayOfWeek.Sunday;

		// Token: 0x0400373E RID: 14142
		public static TimeSpan s_tsServiceTimeOffset;

		// Token: 0x020013CF RID: 5071
		public enum TimePeriod
		{
			// Token: 0x04009C38 RID: 39992
			Day,
			// Token: 0x04009C39 RID: 39993
			Week,
			// Token: 0x04009C3A RID: 39994
			Month
		}
	}
}
