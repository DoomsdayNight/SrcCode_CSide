using System;
using System.Collections.Generic;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000645 RID: 1605
	public class NKCGenericBezierCurve : IBezierCurve
	{
		// Token: 0x06003207 RID: 12807 RVA: 0x000F869C File Offset: 0x000F689C
		private NKCGenericBezierCurve()
		{
		}

		// Token: 0x06003208 RID: 12808 RVA: 0x000F86A4 File Offset: 0x000F68A4
		public NKCGenericBezierCurve(params Vector3[] points)
		{
			this.lstPoints = new List<Vector3>(points);
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x000F86B8 File Offset: 0x000F68B8
		public Vector3 GetPosition(float t)
		{
			float num = 1f - t;
			Vector3 vector = Vector3.zero;
			int num2 = this.lstPoints.Count - 1;
			for (int i = 0; i < this.lstPoints.Count; i++)
			{
				vector += (float)NKCGenericBezierCurve.Binomial(num2, i) * NKCGenericBezierCurve.Pow(num, num2 - i) * NKCGenericBezierCurve.Pow(t, i) * this.lstPoints[i];
			}
			return vector;
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x000F872C File Offset: 0x000F692C
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

		// Token: 0x0600320B RID: 12811 RVA: 0x000F8754 File Offset: 0x000F6954
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

		// Token: 0x0600320C RID: 12812 RVA: 0x000F8784 File Offset: 0x000F6984
		public static Vector3 GetPosition(float t, params Vector3[] points)
		{
			float num = 1f - t;
			Vector3 vector = Vector3.zero;
			int num2 = points.Length - 1;
			for (int i = 0; i < points.Length; i++)
			{
				vector += (float)NKCGenericBezierCurve.Binomial(num2, i) * NKCGenericBezierCurve.Pow(num, num2 - i) * NKCGenericBezierCurve.Pow(t, i) * points[i];
			}
			return vector;
		}

		// Token: 0x0400310C RID: 12556
		private List<Vector3> lstPoints;
	}
}
