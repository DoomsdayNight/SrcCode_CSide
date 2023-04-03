using System;

namespace Cs.Core.Util
{
	// Token: 0x020010D6 RID: 4310
	public static class DateTimeExt
	{
		// Token: 0x06009E28 RID: 40488 RVA: 0x0033A6B3 File Offset: 0x003388B3
		public static bool IsBetween(this DateTime self, DateTime start, DateTime end)
		{
			return start <= self && self < end;
		}

		// Token: 0x06009E29 RID: 40489 RVA: 0x0033A6C7 File Offset: 0x003388C7
		public static string ToFileString(this DateTime self)
		{
			return self.ToString("yyyy.MM.dd-HH.mm.ss.fff");
		}
	}
}
