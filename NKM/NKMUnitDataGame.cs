using System;

namespace NKM
{
	// Token: 0x020004B0 RID: 1200
	public class NKMUnitDataGame
	{
		// Token: 0x06002153 RID: 8531 RVA: 0x000AA11F File Offset: 0x000A831F
		public NKMUnitDataGame()
		{
			this.Init();
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x000AA130 File Offset: 0x000A8330
		public void Init()
		{
			this.m_UnitUID = 0L;
			this.m_MasterGameUnitUID = 0;
			this.m_GameUnitUID = 0;
			this.m_bChangeUnit = false;
			this.m_NKM_TEAM_TYPE = NKM_TEAM_TYPE.NTT_A1;
			this.m_RespawnPosX = 0f;
			this.m_RespawnPosZ = 0f;
			this.m_RespawnJumpYPos = 0f;
			this.m_fTargetNearRange = 0f;
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x000AA190 File Offset: 0x000A8390
		public void DeepCopyFromSource(NKMUnitDataGame source)
		{
			this.m_UnitUID = source.m_UnitUID;
			this.m_MasterGameUnitUID = source.m_MasterGameUnitUID;
			this.m_GameUnitUID = source.m_GameUnitUID;
			this.m_bChangeUnit = source.m_bChangeUnit;
			this.m_NKM_TEAM_TYPE_ORG = source.m_NKM_TEAM_TYPE_ORG;
			this.m_NKM_TEAM_TYPE = source.m_NKM_TEAM_TYPE;
			this.m_RespawnPosX = source.m_RespawnPosX;
			this.m_RespawnPosZ = source.m_RespawnPosZ;
			this.m_RespawnJumpYPos = source.m_RespawnJumpYPos;
			this.m_fTargetNearRange = source.m_fTargetNearRange;
		}

		// Token: 0x04002233 RID: 8755
		public long m_UnitUID;

		// Token: 0x04002234 RID: 8756
		public short m_MasterGameUnitUID;

		// Token: 0x04002235 RID: 8757
		public short m_GameUnitUID;

		// Token: 0x04002236 RID: 8758
		public bool m_bSummonUnit;

		// Token: 0x04002237 RID: 8759
		public bool m_bChangeUnit;

		// Token: 0x04002238 RID: 8760
		public NKM_TEAM_TYPE m_NKM_TEAM_TYPE_ORG;

		// Token: 0x04002239 RID: 8761
		public NKM_TEAM_TYPE m_NKM_TEAM_TYPE;

		// Token: 0x0400223A RID: 8762
		public float m_RespawnPosX;

		// Token: 0x0400223B RID: 8763
		public float m_RespawnPosZ;

		// Token: 0x0400223C RID: 8764
		public float m_RespawnJumpYPos;

		// Token: 0x0400223D RID: 8765
		public float m_fTargetNearRange;
	}
}
