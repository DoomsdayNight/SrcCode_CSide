using System;
using System.Collections.Generic;

namespace NKM
{
	// Token: 0x020003AB RID: 939
	public class NKMGenericBezierCurve : IBezierCurve
	{
		// Token: 0x060018AB RID: 6315 RVA: 0x000634D0 File Offset: 0x000616D0
		private NKMGenericBezierCurve()
		{
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x000634D8 File Offset: 0x000616D8
		public NKMGenericBezierCurve(params NKMVector3[] points)
		{
			this.lstPoints = new List<NKMVector3>(points);
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x000634EC File Offset: 0x000616EC
		public NKMVector3 GetPosition(float t)
		{
			float num = 1f - t;
			NKMVector3 nkmvector = new NKMVector3(0f, 0f, 0f);
			int num2 = this.lstPoints.Count - 1;
			for (int i = 0; i < this.lstPoints.Count; i++)
			{
				nkmvector += (float)NKMGenericBezierCurve.Binomial(num2, i) * NKMGenericBezierCurve.Pow(num, num2 - i) * NKMGenericBezierCurve.Pow(t, i) * this.lstPoints[i];
			}
			return nkmvector;
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x00063570 File Offset: 0x00061770
		private static int Binomial(int n, int k)
		{
			int num = 1;
			for (int i = 1; i <= k; i++)
			{
				num *= n - (k - i);
				num /= i;
			}
			return num;
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x00063598 File Offset: 0x00061798
		private static float Pow(float num, int exp)
		{
			float num2 = 1f;
			while (exp > 0)
			{
				if (exp % 2 == 1)
				{
					num2 *= num;
				}
				exp >>= 1;
				num *= num;
			}
			return num2;
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x000635C8 File Offset: 0x000617C8
		public static NKMVector3 GetPosition(float t, params NKMVector3[] points)
		{
			float num = 1f - t;
			NKMVector3 nkmvector = new NKMVector3(0f, 0f, 0f);
			int num2 = points.Length - 1;
			for (int i = 0; i < points.Length; i++)
			{
				nkmvector += (float)NKMGenericBezierCurve.Binomial(num2, i) * (float)Math.Pow((double)num, (double)(num2 - i)) * (float)Math.Pow((double)t, (double)i) * points[i];
			}
			return nkmvector;
		}

		// Token: 0x04001062 RID: 4194
		private List<NKMVector3> lstPoints;
	}
}
