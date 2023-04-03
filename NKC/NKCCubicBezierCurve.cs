using System;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000643 RID: 1603
	public class NKCCubicBezierCurve : IBezierCurve
	{
		// Token: 0x060031FF RID: 12799 RVA: 0x000F84EA File Offset: 0x000F66EA
		private NKCCubicBezierCurve()
		{
		}

		// Token: 0x06003200 RID: 12800 RVA: 0x000F84F2 File Offset: 0x000F66F2
		public NKCCubicBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
		{
			this.P0 = p0;
			this.P1 = p1;
			this.P2 = p2;
			this.P3 = p3;
		}

		// Token: 0x06003201 RID: 12801 RVA: 0x000F8518 File Offset: 0x000F6718
		public Vector3 GetPosition(float t)
		{
			float num = 1f - t;
			return num * num * num * this.P0 + 3f * num * num * t * this.P1 + 3f * num * t * t * this.P2 + t * t * t * this.P3;
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x000F8588 File Offset: 0x000F6788
		public static Vector3 GetPosition(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
		{
			float num = 1f - t;
			return num * num * num * p0 + 3f * num * num * t * p1 + 3f * num * t * t * p2 + t * t * t * p3;
		}

		// Token: 0x04003105 RID: 12549
		private Vector3 P0;

		// Token: 0x04003106 RID: 12550
		private Vector3 P1;

		// Token: 0x04003107 RID: 12551
		private Vector3 P2;

		// Token: 0x04003108 RID: 12552
		private Vector3 P3;
	}
}
