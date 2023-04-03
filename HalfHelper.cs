using System;
using System.Runtime.InteropServices;

namespace NKM
{
	// Token: 0x0200037C RID: 892
	[ComVisible(false)]
	internal static class HalfHelper
	{
		// Token: 0x060016B6 RID: 5814 RVA: 0x0005AB48 File Offset: 0x00058D48
		private static uint ConvertMantissa(int i)
		{
			uint num = (uint)((uint)i << 13);
			uint num2 = 0U;
			while ((num & 8388608U) == 0U)
			{
				num2 -= 8388608U;
				num <<= 1;
			}
			num &= 4286578687U;
			num2 += 947912704U;
			return num | num2;
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x0005AB88 File Offset: 0x00058D88
		private static uint[] GenerateMantissaTable()
		{
			uint[] array = new uint[2048];
			array[0] = 0U;
			for (int i = 1; i < 1024; i++)
			{
				array[i] = HalfHelper.ConvertMantissa(i);
			}
			for (int j = 1024; j < 2048; j++)
			{
				array[j] = (uint)(939524096 + (j - 1024 << 13));
			}
			return array;
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x0005ABE8 File Offset: 0x00058DE8
		private static uint[] GenerateExponentTable()
		{
			uint[] array = new uint[64];
			array[0] = 0U;
			for (int i = 1; i < 31; i++)
			{
				array[i] = (uint)((uint)i << 23);
			}
			array[31] = 1199570944U;
			array[32] = 2147483648U;
			for (int j = 33; j < 63; j++)
			{
				array[j] = (uint)((ulong)int.MinValue + (ulong)((long)((long)(j - 32) << 23)));
			}
			array[63] = 3347054592U;
			return array;
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x0005AC54 File Offset: 0x00058E54
		private static ushort[] GenerateOffsetTable()
		{
			ushort[] array = new ushort[64];
			array[0] = 0;
			for (int i = 1; i < 32; i++)
			{
				array[i] = 1024;
			}
			array[32] = 0;
			for (int j = 33; j < 64; j++)
			{
				array[j] = 1024;
			}
			return array;
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x0005ACA0 File Offset: 0x00058EA0
		private static ushort[] GenerateBaseTable()
		{
			ushort[] array = new ushort[512];
			for (int i = 0; i < 256; i++)
			{
				sbyte b = (sbyte)(127 - i);
				if (b > 24)
				{
					array[i | 0] = 0;
					array[i | 256] = 32768;
				}
				else if (b > 14)
				{
					array[i | 0] = (ushort)(1024L >> (int)(18 + b));
					array[i | 256] = (ushort)(1024L >> (int)(18 + b) | 32768L);
				}
				else if (b >= -15)
				{
					array[i | 0] = (ushort)(15 - b << 10);
					array[i | 256] = (ushort)((int)(15 - b) << 10 | 32768);
				}
				else if (b > -128)
				{
					array[i | 0] = 31744;
					array[i | 256] = 64512;
				}
				else
				{
					array[i | 0] = 31744;
					array[i | 256] = 64512;
				}
			}
			return array;
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x0005AD90 File Offset: 0x00058F90
		private static sbyte[] GenerateShiftTable()
		{
			sbyte[] array = new sbyte[512];
			for (int i = 0; i < 256; i++)
			{
				sbyte b = (sbyte)(127 - i);
				if (b > 24)
				{
					array[i | 0] = 24;
					array[i | 256] = 24;
				}
				else if (b > 14)
				{
					array[i | 0] = b - 1;
					array[i | 256] = b - 1;
				}
				else if (b >= -15)
				{
					array[i | 0] = 13;
					array[i | 256] = 13;
				}
				else if (b > -128)
				{
					array[i | 0] = 24;
					array[i | 256] = 24;
				}
				else
				{
					array[i | 0] = 13;
					array[i | 256] = 13;
				}
			}
			return array;
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x0005AE40 File Offset: 0x00059040
		public unsafe static float HalfToSingle(Half half)
		{
			uint num = HalfHelper.mantissaTable[(int)(HalfHelper.offsetTable[half.value >> 10] + (half.value & 1023))] + HalfHelper.exponentTable[half.value >> 10];
			return *(float*)(&num);
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x0005AE84 File Offset: 0x00059084
		public unsafe static Half SingleToHalf(float single)
		{
			uint num = *(uint*)(&single);
			return Half.ToHalf((ushort)((uint)HalfHelper.baseTable[(int)(num >> 23 & 511U)] + ((num & 8388607U) >> (int)HalfHelper.shiftTable[(int)(num >> 23)])));
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x0005AEC2 File Offset: 0x000590C2
		public static Half Negate(Half half)
		{
			return Half.ToHalf(half.value ^ 32768);
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x0005AED6 File Offset: 0x000590D6
		public static Half Abs(Half half)
		{
			return Half.ToHalf(half.value & 32767);
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x0005AEEA File Offset: 0x000590EA
		public static bool IsNaN(Half half)
		{
			return (half.value & 32767) > 31744;
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0005AEFF File Offset: 0x000590FF
		public static bool IsInfinity(Half half)
		{
			return (half.value & 32767) == 31744;
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x0005AF14 File Offset: 0x00059114
		public static bool IsPositiveInfinity(Half half)
		{
			return half.value == 31744;
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x0005AF23 File Offset: 0x00059123
		public static bool IsNegativeInfinity(Half half)
		{
			return half.value == 64512;
		}

		// Token: 0x04000F06 RID: 3846
		private static uint[] mantissaTable = HalfHelper.GenerateMantissaTable();

		// Token: 0x04000F07 RID: 3847
		private static uint[] exponentTable = HalfHelper.GenerateExponentTable();

		// Token: 0x04000F08 RID: 3848
		private static ushort[] offsetTable = HalfHelper.GenerateOffsetTable();

		// Token: 0x04000F09 RID: 3849
		private static ushort[] baseTable = HalfHelper.GenerateBaseTable();

		// Token: 0x04000F0A RID: 3850
		private static sbyte[] shiftTable = HalfHelper.GenerateShiftTable();
	}
}
