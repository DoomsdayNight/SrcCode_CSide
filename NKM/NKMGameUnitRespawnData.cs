using System;

namespace NKM
{
	// Token: 0x0200040F RID: 1039
	public class NKMGameUnitRespawnData : NKMObjectPoolData
	{
		// Token: 0x06001B2A RID: 6954 RVA: 0x000773BA File Offset: 0x000755BA
		public NKMGameUnitRespawnData()
		{
			this.m_NKM_OBJECT_POOL_TYPE = NKM_OBJECT_POOL_TYPE.NOPT_NKMGameUnitRespawnData;
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x000773C9 File Offset: 0x000755C9
		public override void Close()
		{
			this.Init();
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x000773D1 File Offset: 0x000755D1
		public void Init()
		{
			this.m_UnitUID = 0L;
			this.m_fRespawnCoolTime = 0f;
			this.m_fRespawnPosX = -1f;
			this.m_eNKM_TEAM_TYPE = NKM_TEAM_TYPE.NTT_INVALID;
			this.m_fRollbackTime = 0f;
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x00077403 File Offset: 0x00075603
		public void DeepCopyFromSource(NKMGameUnitRespawnData source)
		{
			this.m_UnitUID = source.m_UnitUID;
			this.m_fRespawnCoolTime = source.m_fRespawnCoolTime;
			this.m_fRespawnPosX = source.m_fRespawnPosX;
			this.m_eNKM_TEAM_TYPE = source.m_eNKM_TEAM_TYPE;
			this.m_fRollbackTime = source.m_fRollbackTime;
		}

		// Token: 0x04001AF5 RID: 6901
		public long m_UnitUID;

		// Token: 0x04001AF6 RID: 6902
		public float m_fRespawnCoolTime;

		// Token: 0x04001AF7 RID: 6903
		public float m_fRespawnPosX;

		// Token: 0x04001AF8 RID: 6904
		public NKM_TEAM_TYPE m_eNKM_TEAM_TYPE;

		// Token: 0x04001AF9 RID: 6905
		public float m_fRollbackTime;
	}
}
