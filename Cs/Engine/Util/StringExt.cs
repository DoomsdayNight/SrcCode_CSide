using System;
using System.Text;

namespace Cs.Engine.Util
{
	// Token: 0x020010A8 RID: 4264
	public static class StringExt
	{
		// Token: 0x06009C1A RID: 39962 RVA: 0x003346D4 File Offset: 0x003328D4
		public static string ToByteFormat(this ushort bytes)
		{
			string[] array = new string[]
			{
				"b",
				"Kb",
				"Mb",
				"Gb",
				"Tb",
				"Pb",
				"Eb",
				"Zb",
				"Yb"
			};
			long num = 0L;
			long num2 = (long)((ulong)bytes);
			int num3 = 0;
			while (num2 >= 1024L)
			{
				num = (num2 & 1023L);
				num2 >>= 10;
				num3++;
			}
			if (num > 100L)
			{
				return string.Format("{0}.{1:##}{2}", num2, num / 10L, array[num3]);
			}
			return string.Format("{0}{1}", num2, array[num3]);
		}

		// Token: 0x06009C1B RID: 39963 RVA: 0x00334790 File Offset: 0x00332990
		public static string ToByteFormat(this int bytes)
		{
			string[] array = new string[]
			{
				"b",
				"Kb",
				"Mb",
				"Gb",
				"Tb",
				"Pb",
				"Eb",
				"Zb",
				"Yb"
			};
			long num = 0L;
			long num2 = (long)bytes;
			int num3 = 0;
			while (num2 >= 1024L)
			{
				num = (num2 & 1023L);
				num2 >>= 10;
				num3++;
			}
			if (num > 100L)
			{
				return string.Format("{0}.{1:##}{2}", num2, num / 10L, array[num3]);
			}
			return string.Format("{0}{1}", num2, array[num3]);
		}

		// Token: 0x06009C1C RID: 39964 RVA: 0x0033484C File Offset: 0x00332A4C
		public static string ToByteFormat(this long bytes)
		{
			string[] array = new string[]
			{
				"b",
				"Kb",
				"Mb",
				"Gb",
				"Tb",
				"Pb",
				"Eb",
				"Zb",
				"Yb"
			};
			long num = 0L;
			long num2 = bytes;
			int num3 = 0;
			while (num2 >= 1024L)
			{
				num = (num2 & 1023L);
				num2 >>= 10;
				num3++;
			}
			if (num > 100L)
			{
				return string.Format("{0}.{1:##}{2}", num2, num / 10L, array[num3]);
			}
			return string.Format("{0}{1}", num2, array[num3]);
		}

		// Token: 0x06009C1D RID: 39965 RVA: 0x00334904 File Offset: 0x00332B04
		public static string ToByteFormat(this ulong bytes)
		{
			string[] array = new string[]
			{
				"b",
				"Kb",
				"Mb",
				"Gb",
				"Tb",
				"Pb",
				"Eb",
				"Zb",
				"Yb"
			};
			ulong num = 0UL;
			ulong num2 = bytes;
			int num3 = 0;
			while (num2 >= 1024UL)
			{
				num = (num2 & 1023UL);
				num2 >>= 10;
				num3++;
			}
			if (num > 100UL)
			{
				return string.Format("{0}.{1:##}{2}", num2, num / 10UL, array[num3]);
			}
			return string.Format("{0}{1}", num2, array[num3]);
		}

		// Token: 0x06009C1E RID: 39966 RVA: 0x003349BC File Offset: 0x00332BBC
		public static string ToTimeFormat(this long msec)
		{
			if (msec < 1000L)
			{
				return msec.ToString() + "ms";
			}
			float num = (float)msec / 1000f;
			if (num < 60f)
			{
				return string.Format("{0:.##}s", num);
			}
			TimeSpan timeSpan = TimeSpan.FromMilliseconds((double)msec);
			if (timeSpan.TotalHours < 1.0)
			{
				return timeSpan.ToString("mm\\:ss\\.ff");
			}
			return timeSpan.ToString("hh\\:mm\\:ss\\.ff");
		}

		// Token: 0x06009C1F RID: 39967 RVA: 0x00334A39 File Offset: 0x00332C39
		public static string EncodeBase64(this string data)
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
		}

		// Token: 0x06009C20 RID: 39968 RVA: 0x00334A4B File Offset: 0x00332C4B
		public static string DecodeBase64(this string data)
		{
			return Encoding.UTF8.GetString(Convert.FromBase64String(data));
		}
	}
}
