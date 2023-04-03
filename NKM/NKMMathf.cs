using System;

namespace NKM
{
	// Token: 0x02000505 RID: 1285
	public static class NKMMathf
	{
		// Token: 0x060024CA RID: 9418 RVA: 0x000BE6D6 File Offset: 0x000BC8D6
		public static float Sin(float degree)
		{
			return (float)Math.Sin((double)(degree * 0.017453292f));
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x000BE6E6 File Offset: 0x000BC8E6
		public static float Cos(float degree)
		{
			return (float)Math.Cos((double)(degree * 0.017453292f));
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x000BE6F6 File Offset: 0x000BC8F6
		public static float SinRad(float rad)
		{
			return (float)Math.Sin((double)rad);
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x000BE700 File Offset: 0x000BC900
		public static float CosRad(float rad)
		{
			return (float)Math.Cos((double)rad);
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x000BE70C File Offset: 0x000BC90C
		public static void RotateVector2(float x, float y, float degree, out float xOut, out float yOut)
		{
			float rad = degree * 0.017453292f;
			float num = NKMMathf.CosRad(rad);
			float num2 = NKMMathf.SinRad(rad);
			xOut = x * num - y * num2;
			yOut = x * num2 + y * num;
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x000BE740 File Offset: 0x000BC940
		public static int Ceiling(float x)
		{
			return (int)Math.Ceiling((double)x);
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x000BE74A File Offset: 0x000BC94A
		public static float Max(float a, float b)
		{
			if (a <= b)
			{
				return b;
			}
			return a;
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x000BE753 File Offset: 0x000BC953
		public static float Min(float a, float b)
		{
			if (a <= b)
			{
				return a;
			}
			return b;
		}

		// Token: 0x04002651 RID: 9809
		public const float DEG2RAD = 0.017453292f;
	}
}
