using System;
using ClientPacket.Service;
using NKM.Templet;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006D4 RID: 1748
	public static class NKCSynchronizedTime
	{
		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06003D0A RID: 15626 RVA: 0x0013A579 File Offset: 0x00138779
		public static ref readonly TimeSpan ServiceTimeOffet
		{
			get
			{
				return ref NKCSynchronizedTime.s_tsServiceTimeOffset;
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06003D0B RID: 15627 RVA: 0x0013A580 File Offset: 0x00138780
		public static DateTime ServiceTime
		{
			get
			{
				return NKCSynchronizedTime.GetServerUTCTime(0.0) + NKCSynchronizedTime.s_tsServiceTimeOffset;
			}
		}

		// Token: 0x06003D0C RID: 15628 RVA: 0x0013A59C File Offset: 0x0013879C
		public static void Update(float deltaTime)
		{
			long ticks = DateTime.Now.Ticks;
			if (Math.Abs(ticks - NKCSynchronizedTime.s_lastTick) > 300000000L)
			{
				NKCSynchronizedTime.s_fTimeSinceLastUpdate = 298f;
			}
			NKCSynchronizedTime.s_lastTick = ticks;
			NKCSynchronizedTime.s_fTimeSinceLastUpdate += deltaTime;
			if (300f < NKCSynchronizedTime.s_fTimeSinceLastUpdate && NKCScenManager.GetScenManager().GetConnectGame().HasLoggedIn)
			{
				NKCSynchronizedTime.NKMPacket_SERVER_TIME_REQ();
			}
		}

		// Token: 0x06003D0D RID: 15629 RVA: 0x0013A607 File Offset: 0x00138807
		public static void ForceUpdateTime()
		{
			NKCSynchronizedTime.s_fTimeSinceLastUpdate = float.MaxValue;
		}

		// Token: 0x06003D0E RID: 15630 RVA: 0x0013A614 File Offset: 0x00138814
		private static void NKMPacket_SERVER_TIME_REQ()
		{
			NKMPacket_SERVER_TIME_REQ packet = new NKMPacket_SERVER_TIME_REQ();
			NKCSynchronizedTime.s_fTimeSinceLastUpdate = 270f;
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003D0F RID: 15631 RVA: 0x0013A644 File Offset: 0x00138844
		public static void OnRecv(DateTime utcServerTime, TimeSpan tsServiceTimeOffset)
		{
			NKCSynchronizedTime.s_tsServiceTimeOffset = tsServiceTimeOffset;
			NKMTime.s_tsServiceTimeOffset = NKCSynchronizedTime.s_tsServiceTimeOffset;
			NKCSynchronizedTime.SetUTCServerTime(utcServerTime);
		}

		// Token: 0x06003D10 RID: 15632 RVA: 0x0013A65C File Offset: 0x0013885C
		private static void SetUTCServerTime(DateTime utcServerTime)
		{
			DateTime utcNow = DateTime.UtcNow;
			NKCSynchronizedTime.s_tsServerTimeDifference = utcServerTime - utcNow;
			NKCSynchronizedTime.s_fTimeSinceLastUpdate = 0f;
		}

		// Token: 0x06003D11 RID: 15633 RVA: 0x0013A685 File Offset: 0x00138885
		public static void OnRecv(NKMPacket_SERVER_TIME_ACK sPacket)
		{
			NKCSynchronizedTime.SetUTCServerTime(new DateTime(sPacket.utcServerTimeTicks));
		}

		// Token: 0x06003D12 RID: 15634 RVA: 0x0013A698 File Offset: 0x00138898
		public static DateTime GetServerUTCTime(double intervalSeconds = 0.0)
		{
			return DateTime.UtcNow.Add(NKCSynchronizedTime.s_tsServerTimeDifference).AddSeconds(intervalSeconds);
		}

		// Token: 0x06003D13 RID: 15635 RVA: 0x0013A6C0 File Offset: 0x001388C0
		public static DateTime ToServerServiceTime(DateTime UtcTime)
		{
			return UtcTime.Add(NKCSynchronizedTime.s_tsServiceTimeOffset);
		}

		// Token: 0x06003D14 RID: 15636 RVA: 0x0013A6CE File Offset: 0x001388CE
		public static DateTime ToUtcTime(DateTime serverServiceTime)
		{
			if (serverServiceTime.Ticks == 0L || serverServiceTime == DateTime.MinValue || serverServiceTime == DateTime.MaxValue)
			{
				return serverServiceTime;
			}
			return serverServiceTime.Subtract(NKCSynchronizedTime.s_tsServiceTimeOffset);
		}

		// Token: 0x06003D15 RID: 15637 RVA: 0x0013A701 File Offset: 0x00138901
		public static TimeSpan GetTimeLeft(long finishTimeTicks)
		{
			return NKCSynchronizedTime.GetTimeLeft(new DateTime(finishTimeTicks));
		}

		// Token: 0x06003D16 RID: 15638 RVA: 0x0013A710 File Offset: 0x00138910
		public static TimeSpan GetTimeLeft(DateTime finishTime)
		{
			DateTime serverUTCTime = NKCSynchronizedTime.GetServerUTCTime(0.0);
			if (finishTime > serverUTCTime)
			{
				return finishTime - serverUTCTime;
			}
			return new TimeSpan(0, 0, 0);
		}

		// Token: 0x06003D17 RID: 15639 RVA: 0x0013A745 File Offset: 0x00138945
		public static string GetTimeLeftString(long finishTimeTicks)
		{
			return NKCSynchronizedTime.GetTimeLeftString(new DateTime(finishTimeTicks));
		}

		// Token: 0x06003D18 RID: 15640 RVA: 0x0013A752 File Offset: 0x00138952
		public static string GetTimeLeftString(DateTime finishTime)
		{
			return NKCSynchronizedTime.GetTimeSpanString(NKCSynchronizedTime.GetTimeLeft(finishTime));
		}

		// Token: 0x06003D19 RID: 15641 RVA: 0x0013A760 File Offset: 0x00138960
		public static string GetTimeSpanString(TimeSpan timeSpan)
		{
			if (timeSpan.Days > 0)
			{
				return string.Format("{0} {1:00}:{2:00}:{3:00}", new object[]
				{
					string.Format(NKCUtilString.GET_STRING_TIME_DAY_ONE_PARAM, timeSpan.Days),
					timeSpan.Hours,
					timeSpan.Minutes,
					timeSpan.Seconds
				});
			}
			return string.Format("{0:00}:{1:00}:{2:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
		}

		// Token: 0x06003D1A RID: 15642 RVA: 0x0013A7FC File Offset: 0x001389FC
		public static bool IsStarted(long startTimeUTCTicks)
		{
			return NKCSynchronizedTime.IsFinished(startTimeUTCTicks);
		}

		// Token: 0x06003D1B RID: 15643 RVA: 0x0013A804 File Offset: 0x00138A04
		public static bool IsStarted(DateTime startTimeUTC)
		{
			return NKCSynchronizedTime.IsFinished(startTimeUTC);
		}

		// Token: 0x06003D1C RID: 15644 RVA: 0x0013A80C File Offset: 0x00138A0C
		public static bool IsFinished(long finishTimeUTCTicks)
		{
			return NKCSynchronizedTime.IsFinished(new DateTime(finishTimeUTCTicks));
		}

		// Token: 0x06003D1D RID: 15645 RVA: 0x0013A81C File Offset: 0x00138A1C
		public static bool IsFinished(DateTime finishTimeUTC)
		{
			DateTime serverUTCTime = NKCSynchronizedTime.GetServerUTCTime(0.0);
			return finishTimeUTC < serverUTCTime;
		}

		// Token: 0x06003D1E RID: 15646 RVA: 0x0013A840 File Offset: 0x00138A40
		public static bool IsEventTime(DateTime StartTimeUTC, DateTime finishTimeUTC)
		{
			if ((StartTimeUTC.Ticks == 0L && finishTimeUTC.Ticks == 0L) || (StartTimeUTC == DateTime.MinValue && finishTimeUTC == DateTime.MaxValue))
			{
				return true;
			}
			DateTime serverUTCTime = NKCSynchronizedTime.GetServerUTCTime(0.0);
			return !(serverUTCTime < StartTimeUTC) && !(finishTimeUTC < serverUTCTime);
		}

		// Token: 0x06003D1F RID: 15647 RVA: 0x0013A8A4 File Offset: 0x00138AA4
		public static bool IsEventTime(DateTime currentTime, DateTime StartTimeUTC, DateTime finishTimeUTC)
		{
			return (StartTimeUTC.Ticks == 0L && finishTimeUTC.Ticks == 0L) || (StartTimeUTC == DateTime.MinValue && finishTimeUTC == DateTime.MaxValue) || (!(currentTime < StartTimeUTC) && !(finishTimeUTC < currentTime));
		}

		// Token: 0x06003D20 RID: 15648 RVA: 0x0013A8F6 File Offset: 0x00138AF6
		public static bool IsEventTime(NKMIntervalTemplet interval)
		{
			return interval == null || interval.IsValidTime(NKCSynchronizedTime.ServiceTime);
		}

		// Token: 0x06003D21 RID: 15649 RVA: 0x0013A908 File Offset: 0x00138B08
		public static bool IsEventTime(DateTime utcCurrent, NKMIntervalTemplet interval)
		{
			return interval == null || interval.IsValidTime(NKCSynchronizedTime.ToServerServiceTime(utcCurrent));
		}

		// Token: 0x06003D22 RID: 15650 RVA: 0x0013A91C File Offset: 0x00138B1C
		public static bool IsEventTime(string intervalStrID)
		{
			if (string.IsNullOrEmpty(intervalStrID))
			{
				return true;
			}
			NKMIntervalTemplet nkmintervalTemplet = NKMIntervalTemplet.Find(intervalStrID);
			if (nkmintervalTemplet == null)
			{
				Debug.LogError("intervalTemplet " + intervalStrID + " not found!");
				return false;
			}
			return NKCSynchronizedTime.IsEventTime(nkmintervalTemplet);
		}

		// Token: 0x06003D23 RID: 15651 RVA: 0x0013A95C File Offset: 0x00138B5C
		public static bool IsEventTime(DateTime utcNow, string intervalStrID)
		{
			if (string.IsNullOrEmpty(intervalStrID))
			{
				return true;
			}
			NKMIntervalTemplet nkmintervalTemplet = NKMIntervalTemplet.Find(intervalStrID);
			if (nkmintervalTemplet == null)
			{
				Debug.LogError("intervalTemplet " + intervalStrID + " not found!");
				return false;
			}
			return NKCSynchronizedTime.IsEventTime(utcNow, nkmintervalTemplet);
		}

		// Token: 0x06003D24 RID: 15652 RVA: 0x0013A99B File Offset: 0x00138B9B
		public static bool IsEventTime(NKMIntervalTemplet interval, DateTime startUtc, DateTime finishUtc)
		{
			if (interval != null)
			{
				return NKCSynchronizedTime.IsEventTime(interval);
			}
			return NKCSynchronizedTime.IsEventTime(startUtc, finishUtc);
		}

		// Token: 0x06003D25 RID: 15653 RVA: 0x0013A9AE File Offset: 0x00138BAE
		public static bool IsEventTime(DateTime utcCurrent, NKMIntervalTemplet interval, DateTime startUtc, DateTime finishUtc)
		{
			if (interval != null)
			{
				return NKCSynchronizedTime.IsEventTime(utcCurrent, interval);
			}
			return NKCSynchronizedTime.IsEventTime(utcCurrent, startUtc, finishUtc);
		}

		// Token: 0x06003D26 RID: 15654 RVA: 0x0013A9C4 File Offset: 0x00138BC4
		public static bool IsEventTime(string intervalStrID, DateTime startUtc, DateTime finishUtc)
		{
			if (string.IsNullOrEmpty(intervalStrID))
			{
				return NKCSynchronizedTime.IsEventTime(startUtc, finishUtc);
			}
			NKMIntervalTemplet nkmintervalTemplet = NKMIntervalTemplet.Find(intervalStrID);
			if (nkmintervalTemplet == null)
			{
				Debug.LogError("intervalTemplet " + intervalStrID + " not found!");
				return false;
			}
			return NKCSynchronizedTime.IsEventTime(nkmintervalTemplet);
		}

		// Token: 0x06003D27 RID: 15655 RVA: 0x0013AA08 File Offset: 0x00138C08
		public static bool IsEventTime(DateTime utcCurrent, string intervalStrID, DateTime startUtc, DateTime finishUtc)
		{
			if (string.IsNullOrEmpty(intervalStrID))
			{
				return NKCSynchronizedTime.IsEventTime(utcCurrent, startUtc, finishUtc);
			}
			NKMIntervalTemplet nkmintervalTemplet = NKMIntervalTemplet.Find(intervalStrID);
			if (nkmintervalTemplet == null)
			{
				Debug.LogError("intervalTemplet " + intervalStrID + " not found!");
				return false;
			}
			return NKCSynchronizedTime.IsEventTime(utcCurrent, nkmintervalTemplet);
		}

		// Token: 0x06003D28 RID: 15656 RVA: 0x0013AA4E File Offset: 0x00138C4E
		public static bool IsBeforeLastServerRefresh(long Ticks)
		{
			return NKCSynchronizedTime.IsBeforeLastServerRefresh(new DateTime(Ticks));
		}

		// Token: 0x06003D29 RID: 15657 RVA: 0x0013AA5C File Offset: 0x00138C5C
		public static bool IsBeforeLastServerRefresh(DateTime Time)
		{
			DateTime serverUTCTime = NKCSynchronizedTime.GetServerUTCTime(0.0);
			DateTime t = serverUTCTime.Date.AddHours((double)NKMTime.RESET_TIME_BASE_UTC);
			if (serverUTCTime.Hour < NKMTime.RESET_TIME_BASE_UTC)
			{
				t = t.AddDays(-1.0);
			}
			return Time < t;
		}

		// Token: 0x06003D2A RID: 15658 RVA: 0x0013AAB4 File Offset: 0x00138CB4
		public static DateTime GetSystemLocalTime(DateTime UtcTime)
		{
			return UtcTime.ToLocalTime();
		}

		// Token: 0x06003D2B RID: 15659 RVA: 0x0013AAC0 File Offset: 0x00138CC0
		public static DateTime GetSystemLocalTime(DateTime ServiceTime, int UtcInterval)
		{
			return ServiceTime.AddHours((double)(-(double)UtcInterval)).ToLocalTime();
		}

		// Token: 0x06003D2C RID: 15660 RVA: 0x0013AADF File Offset: 0x00138CDF
		public static DateTime GetIntervalUtc(NKMIntervalTemplet intervalTemplet, bool bStart)
		{
			if (bStart)
			{
				return intervalTemplet.GetStartDateUtc();
			}
			return intervalTemplet.GetEndDateUtc();
		}

		// Token: 0x06003D2D RID: 15661 RVA: 0x0013AAF1 File Offset: 0x00138CF1
		public static DateTime GetIntervalStartUtc(NKMIntervalTemplet intervalTemplet)
		{
			return intervalTemplet.GetStartDateUtc();
		}

		// Token: 0x06003D2E RID: 15662 RVA: 0x0013AAF9 File Offset: 0x00138CF9
		public static DateTime GetIntervalEndUtc(NKMIntervalTemplet intervalTemplet)
		{
			return intervalTemplet.GetEndDateUtc();
		}

		// Token: 0x06003D2F RID: 15663 RVA: 0x0013AB01 File Offset: 0x00138D01
		public static DateTime GetIntervalStartUtc(string intervalID)
		{
			return NKCSynchronizedTime.GetIntervalUtc(intervalID, DateTime.MinValue, true);
		}

		// Token: 0x06003D30 RID: 15664 RVA: 0x0013AB0F File Offset: 0x00138D0F
		public static DateTime GetIntervalEndUtc(string intervalID)
		{
			return NKCSynchronizedTime.GetIntervalUtc(intervalID, DateTime.MaxValue, false);
		}

		// Token: 0x06003D31 RID: 15665 RVA: 0x0013AB20 File Offset: 0x00138D20
		private static DateTime GetIntervalUtc(string intervalID, DateTime templetServiceTime, bool bStart)
		{
			if (string.IsNullOrEmpty(intervalID))
			{
				return NKMTime.LocalToUTC(templetServiceTime, 0);
			}
			NKMIntervalTemplet nkmintervalTemplet = NKMIntervalTemplet.Find(intervalID);
			if (nkmintervalTemplet == null)
			{
				Debug.LogError("IntervalTemplet " + intervalID + " not found");
				return DateTime.MinValue;
			}
			if (bStart)
			{
				return nkmintervalTemplet.GetStartDateUtc();
			}
			return nkmintervalTemplet.GetEndDateUtc();
		}

		// Token: 0x06003D32 RID: 15666 RVA: 0x0013AB74 File Offset: 0x00138D74
		public static DateTime GetStartOfServiceTime(int dayOffset)
		{
			return NKCSynchronizedTime.ToUtcTime(new DateTime(NKCSynchronizedTime.ServiceTime.Year, NKCSynchronizedTime.ServiceTime.Month, NKCSynchronizedTime.ServiceTime.Day, 0, 0, 0).AddDays((double)dayOffset));
		}

		// Token: 0x04003628 RID: 13864
		public static int UNLIMITD_REMAIN_DAYS = 36000;

		// Token: 0x04003629 RID: 13865
		private static float s_fTimeSinceLastUpdate = float.MaxValue;

		// Token: 0x0400362A RID: 13866
		private static TimeSpan s_tsServerTimeDifference;

		// Token: 0x0400362B RID: 13867
		private static TimeSpan s_tsServiceTimeOffset;

		// Token: 0x0400362C RID: 13868
		private const float SERVER_TIME_SYNC_INTERVAL = 300f;

		// Token: 0x0400362D RID: 13869
		private const long THIRTY_SECOND = 300000000L;

		// Token: 0x0400362E RID: 13870
		private static long s_lastTick;
	}
}
