using System;

namespace NKM
{
	// Token: 0x0200042A RID: 1066
	public class NKMBloomPoint
	{
		// Token: 0x06001D04 RID: 7428 RVA: 0x00087304 File Offset: 0x00085504
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			this.m_LensFlareName.LoadFromLua(cNKMLua, "m_LensFlareName");
			cNKMLua.GetData("m_fBloomPointX", ref this.m_fBloomPointX);
			cNKMLua.GetData("m_fBloomPointY", ref this.m_fBloomPointY);
			cNKMLua.GetData("m_fBloomAddIntensity", ref this.m_fBloomAddIntensity);
			cNKMLua.GetData("m_fBloomAddThreshHold", ref this.m_fBloomAddThreshHold);
			cNKMLua.GetData("m_fBloomDistance", ref this.m_fBloomDistance);
			return true;
		}

		// Token: 0x04001C68 RID: 7272
		public NKMAssetName m_LensFlareName = new NKMAssetName();

		// Token: 0x04001C69 RID: 7273
		public float m_fBloomPointX;

		// Token: 0x04001C6A RID: 7274
		public float m_fBloomPointY;

		// Token: 0x04001C6B RID: 7275
		public float m_fBloomAddIntensity;

		// Token: 0x04001C6C RID: 7276
		public float m_fBloomAddThreshHold;

		// Token: 0x04001C6D RID: 7277
		public float m_fBloomDistance;
	}
}
