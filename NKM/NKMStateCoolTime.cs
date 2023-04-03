using System;

namespace NKM
{
	// Token: 0x02000491 RID: 1169
	public class NKMStateCoolTime : NKMObjectPoolData
	{
		// Token: 0x06001F8A RID: 8074 RVA: 0x00095AAE File Offset: 0x00093CAE
		public NKMStateCoolTime()
		{
			this.m_NKM_OBJECT_POOL_TYPE = NKM_OBJECT_POOL_TYPE.NOPT_NKMStateCoolTime;
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x00095ABD File Offset: 0x00093CBD
		public override void Close()
		{
			this.m_CoolTime = 0f;
		}

		// Token: 0x04002107 RID: 8455
		public float m_CoolTime;
	}
}
