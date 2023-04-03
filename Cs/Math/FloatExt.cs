using System;

namespace Cs.Math
{
	// Token: 0x020010C9 RID: 4297
	public static class FloatExt
	{
		// Token: 0x06009DE8 RID: 40424 RVA: 0x00339DE5 File Offset: 0x00337FE5
		public static bool IsNearlyEqual(this float self, float operand, float epsilon = 1E-05f)
		{
			return Math.Abs(self - operand) < epsilon;
		}

		// Token: 0x06009DE9 RID: 40425 RVA: 0x00339DF2 File Offset: 0x00337FF2
		public static bool IsNearlyZero(this float self, float epsilon = 1E-05f)
		{
			return Math.Abs(self) < epsilon;
		}

		// Token: 0x06009DEA RID: 40426 RVA: 0x00339DFD File Offset: 0x00337FFD
		public static bool IsNearlyEqual(this double self, double operand, double epsilon = 1E-05)
		{
			return Math.Abs(self - operand) < epsilon;
		}

		// Token: 0x06009DEB RID: 40427 RVA: 0x00339E0A File Offset: 0x0033800A
		public static bool IsNearlyZero(this double self, double epsilon = 1E-05)
		{
			return Math.Abs(self) < epsilon;
		}

		// Token: 0x06009DEC RID: 40428 RVA: 0x00339E15 File Offset: 0x00338015
		public static float Clamp(this float value, float min, float max)
		{
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}
	}
}
