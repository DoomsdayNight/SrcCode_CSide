using System;

namespace Cs.Core.Util
{
	// Token: 0x020010D7 RID: 4311
	public static class NumericExt
	{
		// Token: 0x06009E2A RID: 40490 RVA: 0x0033A6D5 File Offset: 0x003388D5
		public static int Clamp(this int val, int min, int max)
		{
			return Math.Min(Math.Max(val, min), max);
		}

		// Token: 0x06009E2B RID: 40491 RVA: 0x0033A6E4 File Offset: 0x003388E4
		public static long Clamp(this long val, long min, long max)
		{
			return Math.Min(Math.Max(val, min), max);
		}

		// Token: 0x06009E2C RID: 40492 RVA: 0x0033A6F3 File Offset: 0x003388F3
		public static T Clamp<T>(this T val, T min, T max) where T : class, IComparable<T>
		{
			if (val.CompareTo(min) < 0)
			{
				return min;
			}
			if (val.CompareTo(max) <= 0)
			{
				return val;
			}
			return max;
		}

		// Token: 0x06009E2D RID: 40493 RVA: 0x0033A718 File Offset: 0x00338918
		public static ushort DirectToUint16(this byte[] buffer, int startIndex)
		{
			return (ushort)((int)buffer[startIndex + 1] << 8 | (int)buffer[startIndex]);
		}

		// Token: 0x06009E2E RID: 40494 RVA: 0x0033A726 File Offset: 0x00338926
		public static uint DirectToUint32(this byte[] buffer, int startIndex)
		{
			return (uint)((((int)buffer[startIndex + 3] << 8 | (int)buffer[startIndex + 2]) << 8 | (int)buffer[startIndex + 1]) << 8 | (int)buffer[startIndex]);
		}

		// Token: 0x06009E2F RID: 40495 RVA: 0x0033A744 File Offset: 0x00338944
		public static ulong DirectToUint64(this byte[] buffer, int startIndex)
		{
			return (((((((ulong)buffer[startIndex + 7] << 8 | (ulong)buffer[startIndex + 6]) << 8 | (ulong)buffer[startIndex + 5]) << 8 | (ulong)buffer[startIndex + 4]) << 8 | (ulong)buffer[startIndex + 3]) << 8 | (ulong)buffer[startIndex + 2]) << 8 | (ulong)buffer[startIndex + 1]) << 8 | (ulong)buffer[startIndex];
		}

		// Token: 0x06009E30 RID: 40496 RVA: 0x0033A794 File Offset: 0x00338994
		public static void DirectWriteTo(this int data, byte[] buffer, int position)
		{
			buffer[position] = (byte)data;
			buffer[position + 1] = (byte)(data >> 8);
			buffer[position + 2] = (byte)(data >> 16);
			buffer[position + 3] = (byte)(data >> 24);
		}

		// Token: 0x06009E31 RID: 40497 RVA: 0x0033A7B8 File Offset: 0x003389B8
		public static void DirectWriteTo(this long data, byte[] buffer, int position)
		{
			buffer[position] = (byte)data;
			buffer[position + 1] = (byte)(data >> 8);
			buffer[position + 2] = (byte)(data >> 16);
			buffer[position + 3] = (byte)(data >> 24);
			buffer[position + 4] = (byte)(data >> 32);
			buffer[position + 5] = (byte)(data >> 40);
			buffer[position + 6] = (byte)(data >> 48);
			buffer[position + 7] = (byte)(data >> 56);
		}

		// Token: 0x06009E32 RID: 40498 RVA: 0x0033A810 File Offset: 0x00338A10
		public static void DirectWriteTo(this ulong data, byte[] buffer, int position)
		{
			buffer[position] = (byte)data;
			buffer[position + 1] = (byte)(data >> 8);
			buffer[position + 2] = (byte)(data >> 16);
			buffer[position + 3] = (byte)(data >> 24);
			buffer[position + 4] = (byte)(data >> 32);
			buffer[position + 5] = (byte)(data >> 40);
			buffer[position + 6] = (byte)(data >> 48);
			buffer[position + 7] = (byte)(data >> 56);
		}
	}
}
