using System;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000644 RID: 1604
	public class NKCQuadraticBezierCurve : IBezierCurve
	{
		// Token: 0x06003203 RID: 12803 RVA: 0x000F85E5 File Offset: 0x000F67E5
		private NKCQuadraticBezierCurve()
		{
		}

		// Token: 0x06003204 RID: 12804 RVA: 0x000F85ED File Offset: 0x000F67ED
		public NKCQuadraticBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2)
		{
			this.P0 = p0;
			this.P1 = p1;
			this.P2 = p2;
		}

		// Token: 0x06003205 RID: 12805 RVA: 0x000F860C File Offset: 0x000F680C
		public Vector3 GetPosition(float t)
		{
			float num = 1f - t;
			return num * num * this.P0 + 2f * t * num * this.P1 + t * t * this.P2;
		}

		// Token: 0x06003206 RID: 12806 RVA: 0x000F865C File Offset: 0x000F685C
		public static Vector3 GetPosition(float t, Vector3 p0, Vector3 p1, Vector3 p2)
		{
			float num = 1f - t;
			return num * num * p0 + 2f * t * num * p1 + t * t * p2;
		}

		// Token: 0x04003109 RID: 12553
		private Vector3 P0;

		// Token: 0x0400310A RID: 12554
		private Vector3 P1;

		// Token: 0x0400310B RID: 12555
		private Vector3 P2;
	}
}
