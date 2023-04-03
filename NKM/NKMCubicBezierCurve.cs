using System;

namespace NKM
{
	// Token: 0x020003A9 RID: 937
	public class NKMCubicBezierCurve : IBezierCurve
	{
		// Token: 0x060018A3 RID: 6307 RVA: 0x0006331E File Offset: 0x0006151E
		private NKMCubicBezierCurve()
		{
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x00063326 File Offset: 0x00061526
		public NKMCubicBezierCurve(NKMVector3 p0, NKMVector3 p1, NKMVector3 p2, NKMVector3 p3)
		{
			this.P0 = p0;
			this.P1 = p1;
			this.P2 = p2;
			this.P3 = p3;
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x0006334C File Offset: 0x0006154C
		public NKMVector3 GetPosition(float t)
		{
			float num = 1f - t;
			return num * num * num * this.P0 + 3f * num * num * t * this.P1 + 3f * num * t * t * this.P2 + t * t * t * this.P3;
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x000633BC File Offset: 0x000615BC
		public static NKMVector3 GetPosition(float t, NKMVector3 p0, NKMVector3 p1, NKMVector3 p2, NKMVector3 p3)
		{
			float num = 1f - t;
			return num * num * num * p0 + 3f * num * num * t * p1 + 3f * num * t * t * p2 + t * t * t * p3;
		}

		// Token: 0x0400105B RID: 4187
		private NKMVector3 P0;

		// Token: 0x0400105C RID: 4188
		private NKMVector3 P1;

		// Token: 0x0400105D RID: 4189
		private NKMVector3 P2;

		// Token: 0x0400105E RID: 4190
		private NKMVector3 P3;
	}
}
