using System;
using Cs.Core.Util;

namespace Cs.Math
{
	// Token: 0x020010CA RID: 4298
	public static class RandomGenerator
	{
		// Token: 0x06009DED RID: 40429 RVA: 0x00339E24 File Offset: 0x00338024
		public static int Range(int min, int max)
		{
			return RandomGenerator.PerThreadRandom.Instance.Next(min, max);
		}

		// Token: 0x06009DEE RID: 40430 RVA: 0x00339E32 File Offset: 0x00338032
		public static int ArrayIndex(int count)
		{
			return RandomGenerator.PerThreadRandom.Instance.Next(count);
		}

		// Token: 0x06009DEF RID: 40431 RVA: 0x00339E3F File Offset: 0x0033803F
		public static int Next(int maxValue)
		{
			return RandomGenerator.PerThreadRandom.Instance.Next(maxValue);
		}

		// Token: 0x06009DF0 RID: 40432 RVA: 0x00339E4C File Offset: 0x0033804C
		public static float Range(float min, float max)
		{
			if (min > max)
			{
				throw new ArgumentException(string.Format("[RandomGenerator] min:{0} max:{1}", min, max));
			}
			return (float)RandomGenerator.PerThreadRandom.Instance.NextDouble() * (max - min) + min;
		}

		// Token: 0x06009DF1 RID: 40433 RVA: 0x00339E7F File Offset: 0x0033807F
		public static long LongRandom()
		{
			return (long)(RandomGenerator.PerThreadRandom.Instance.NextDouble() * 9.223372036854776E+18);
		}

		// Token: 0x06009DF2 RID: 40434 RVA: 0x00339E98 File Offset: 0x00338098
		public static long LongRandom(long min, long max)
		{
			byte[] buffer = new byte[8];
			RandomGenerator.PerThreadRandom.Instance.NextBytes(buffer);
			return (long)(buffer.DirectToUint64(0) % (ulong)(max - min) + (ulong)min);
		}

		// Token: 0x02001A3A RID: 6714
		private static class PerThreadRandom
		{
			// Token: 0x17001A03 RID: 6659
			// (get) Token: 0x0600BB74 RID: 47988 RVA: 0x0036F6A1 File Offset: 0x0036D8A1
			public static Random Instance
			{
				get
				{
					if (RandomGenerator.PerThreadRandom.random == null)
					{
						RandomGenerator.PerThreadRandom.random = new Random();
					}
					return RandomGenerator.PerThreadRandom.random;
				}
			}

			// Token: 0x0400AE17 RID: 44567
			[ThreadStatic]
			private static Random random;
		}
	}
}
