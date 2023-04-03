using System;
using Cs.Core.Util;
using Cs.Logging;

namespace NKM
{
	// Token: 0x0200045C RID: 1116
	public class NKMRandom
	{
		// Token: 0x06001E3E RID: 7742 RVA: 0x0008FAD2 File Offset: 0x0008DCD2
		public static int Range(int min, int max)
		{
			return PerThreadRandom.Instance.Next(min, max);
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x0008FAE0 File Offset: 0x0008DCE0
		public static float Range(float min, float max)
		{
			if (min > max)
			{
				Log.Error(string.Format("NKMRandom min({0}) > max({1})", min, max), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMRandom.cs", 17);
			}
			return (float)PerThreadRandom.Instance.NextDouble() * (max - min) + min;
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x0008FB19 File Offset: 0x0008DD19
		public static int RandomInt()
		{
			return PerThreadRandom.Instance.Next();
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x0008FB25 File Offset: 0x0008DD25
		public static long LongRandom()
		{
			return (long)(PerThreadRandom.Instance.NextDouble() * 9.223372036854776E+18);
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x0008FB3C File Offset: 0x0008DD3C
		public static long LongRandom(long min, long max)
		{
			byte[] buffer = new byte[8];
			PerThreadRandom.Instance.NextBytes(buffer);
			return (long)(buffer.DirectToUint64(0) % (ulong)(max - min) + (ulong)min);
		}
	}
}
