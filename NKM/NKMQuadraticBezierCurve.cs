using System;

namespace NKM
{
	// Token: 0x020003AA RID: 938
	public class NKMQuadraticBezierCurve : IBezierCurve
	{
		// Token: 0x060018A7 RID: 6311 RVA: 0x00063419 File Offset: 0x00061619
		private NKMQuadraticBezierCurve()
		{
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x00063421 File Offset: 0x00061621
		public NKMQuadraticBezierCurve(NKMVector3 p0, NKMVector3 p1, NKMVector3 p2)
		{
			this.P0 = p0;
			this.P1 = p1;
			this.P2 = p2;
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x00063440 File Offset: 0x00061640
		public NKMVector3 GetPosition(float t)
		{
			float num = 1f - t;
			return num * num * this.P0 + 2f * t * num * this.P1 + t * t * this.P2;
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x00063490 File Offset: 0x00061690
		public static NKMVector3 GetPosition(float t, NKMVector3 p0, NKMVector3 p1, NKMVector3 p2)
		{
			float num = 1f - t;
			return num * num * p0 + 2f * t * num * p1 + t * t * p2;
		}

		// Token: 0x0400105F RID: 4191
		private NKMVector3 P0;

		// Token: 0x04001060 RID: 4192
		private NKMVector3 P1;

		// Token: 0x04001061 RID: 4193
		private NKMVector3 P2;
	}
}
