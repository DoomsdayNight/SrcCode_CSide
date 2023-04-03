using System;
using NKC;

namespace Cs.Core.Util
{
	// Token: 0x020010D8 RID: 4312
	public static class ServiceTime
	{
		// Token: 0x17001739 RID: 5945
		// (get) Token: 0x06009E33 RID: 40499 RVA: 0x0033A867 File Offset: 0x00338A67
		public static DateTime Now
		{
			get
			{
				return NKCSynchronizedTime.ServiceTime;
			}
		}

		// Token: 0x1700173A RID: 5946
		// (get) Token: 0x06009E34 RID: 40500 RVA: 0x0033A86E File Offset: 0x00338A6E
		public static DateTime Recent
		{
			get
			{
				return NKCSynchronizedTime.ServiceTime;
			}
		}

		// Token: 0x1700173B RID: 5947
		// (get) Token: 0x06009E35 RID: 40501 RVA: 0x0033A875 File Offset: 0x00338A75
		public static DateTime UtcNow
		{
			get
			{
				return DateTime.UtcNow;
			}
		}

		// Token: 0x1700173C RID: 5948
		// (get) Token: 0x06009E36 RID: 40502 RVA: 0x0033A87C File Offset: 0x00338A7C
		public static DateTime Forever { get; } = DateTime.Parse("9000-1-1");

		// Token: 0x06009E37 RID: 40503 RVA: 0x0033A883 File Offset: 0x00338A83
		public unsafe static DateTime FromUtcTime(DateTime utcTime)
		{
			return utcTime + *NKCSynchronizedTime.ServiceTimeOffet;
		}

		// Token: 0x06009E38 RID: 40504 RVA: 0x0033A895 File Offset: 0x00338A95
		public unsafe static DateTime ToUtcTime(DateTime serviceTime)
		{
			return serviceTime - *NKCSynchronizedTime.ServiceTimeOffet;
		}
	}
}
