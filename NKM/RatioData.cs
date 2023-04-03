using System;

namespace NKM
{
	// Token: 0x0200045F RID: 1119
	public struct RatioData
	{
		// Token: 0x06001E51 RID: 7761 RVA: 0x0008FF29 File Offset: 0x0008E129
		public RatioData(float ssr, float sr, float r, float n)
		{
			this.ratio_SSR = ssr;
			this.ratio_SR = sr;
			this.ratio_R = r;
			this.ratio_N = n;
		}

		// Token: 0x04001EF9 RID: 7929
		public float ratio_SSR;

		// Token: 0x04001EFA RID: 7930
		public float ratio_SR;

		// Token: 0x04001EFB RID: 7931
		public float ratio_R;

		// Token: 0x04001EFC RID: 7932
		public float ratio_N;
	}
}
