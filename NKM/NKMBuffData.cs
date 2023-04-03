using System;

namespace NKM
{
	// Token: 0x020003AE RID: 942
	public class NKMBuffData : NKMObjectPoolData
	{
		// Token: 0x060018B5 RID: 6325 RVA: 0x00063758 File Offset: 0x00061958
		public NKMBuffData()
		{
			this.m_NKM_OBJECT_POOL_TYPE = NKM_OBJECT_POOL_TYPE.NOPT_NKMBuffData;
			this.Init();
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x000637A4 File Offset: 0x000619A4
		public void Init()
		{
			this.m_BuffSyncData.Init();
			this.m_fLifeTime = -1f;
			this.m_fBarrierHP = -1f;
			this.m_fBuffPosX = 0f;
			this.m_StateEndRemove = false;
			this.m_StateEndCheck = false;
			this.m_NKMBuffTemplet = null;
			this.m_DamageInstBuff.Init();
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x000637FD File Offset: 0x000619FD
		public override void Close()
		{
			this.Init();
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x00063805 File Offset: 0x00061A05
		public float GetLifeTimeMax()
		{
			if (this.m_NKMBuffTemplet == null)
			{
				return -1f;
			}
			return this.m_NKMBuffTemplet.GetLifeTimeMax((int)this.m_BuffSyncData.m_BuffTimeLevel);
		}

		// Token: 0x04001074 RID: 4212
		public NKMBuffSyncData m_BuffSyncData = new NKMBuffSyncData();

		// Token: 0x04001075 RID: 4213
		public float m_fLifeTime = -1f;

		// Token: 0x04001076 RID: 4214
		public float m_fBarrierHP = -1f;

		// Token: 0x04001077 RID: 4215
		public float m_fBuffPosX;

		// Token: 0x04001078 RID: 4216
		public bool m_StateEndRemove;

		// Token: 0x04001079 RID: 4217
		public bool m_StateEndCheck;

		// Token: 0x0400107A RID: 4218
		public NKMBuffTemplet m_NKMBuffTemplet;

		// Token: 0x0400107B RID: 4219
		public NKMDamageInst m_DamageInstBuff = new NKMDamageInst();
	}
}
