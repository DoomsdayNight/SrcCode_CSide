using System;

namespace NKM
{
	// Token: 0x02000492 RID: 1170
	public class NKMTimeStamp : NKMObjectPoolData
	{
		// Token: 0x06001F8C RID: 8076 RVA: 0x00095ACA File Offset: 0x00093CCA
		public NKMTimeStamp()
		{
			this.m_NKM_OBJECT_POOL_TYPE = NKM_OBJECT_POOL_TYPE.NOPT_NKMTimeStamp;
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x00095AD9 File Offset: 0x00093CD9
		public override void Close()
		{
			this.m_FramePass = false;
		}

		// Token: 0x04002108 RID: 8456
		public bool m_FramePass;
	}
}
