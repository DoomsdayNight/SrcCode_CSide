using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000419 RID: 1049
	public class NKMGameRuntimeTeamData : ISerializable
	{
		// Token: 0x06001B5F RID: 7007 RVA: 0x00077FFC File Offset: 0x000761FC
		public virtual void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_UserUID);
			stream.PutOrGet(ref this.m_bAutoRespawn);
			stream.PutOrGet(ref this.m_bAIDisable);
			stream.PutOrGet(ref this.m_bGodMode);
			stream.PutOrGet(ref this.m_fRespawnCost);
			stream.PutOrGet(ref this.m_fRespawnCostAssist);
			stream.PutOrGet(ref this.m_fUsedRespawnCost);
			stream.PutOrGet(ref this.m_respawn_count);
			stream.PutOrGetEnum<NKM_GAME_AUTO_SKILL_TYPE>(ref this.m_NKM_GAME_AUTO_SKILL_TYPE);
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x00078078 File Offset: 0x00076278
		public void DeepCopyFromSource(NKMGameRuntimeTeamData source)
		{
			this.m_bAutoRespawn = source.m_bAutoRespawn;
			this.m_bAIDisable = source.m_bAIDisable;
			this.m_bGodMode = source.m_bGodMode;
			this.m_fRespawnCost = source.m_fRespawnCost;
			this.m_fRespawnCostAssist = source.m_fRespawnCostAssist;
			this.m_fUsedRespawnCost = source.m_fUsedRespawnCost;
			this.m_respawn_count = source.m_respawn_count;
			this.m_NKM_GAME_AUTO_SKILL_TYPE = source.m_NKM_GAME_AUTO_SKILL_TYPE;
		}

		// Token: 0x04001B40 RID: 6976
		public long m_UserUID;

		// Token: 0x04001B41 RID: 6977
		public bool m_bAutoRespawn = true;

		// Token: 0x04001B42 RID: 6978
		public bool m_bAIDisable;

		// Token: 0x04001B43 RID: 6979
		public bool m_bGodMode;

		// Token: 0x04001B44 RID: 6980
		public float m_fRespawnCost = 3f;

		// Token: 0x04001B45 RID: 6981
		public float m_fRespawnCostAssist;

		// Token: 0x04001B46 RID: 6982
		public float m_fUsedRespawnCost;

		// Token: 0x04001B47 RID: 6983
		public int m_respawn_count;

		// Token: 0x04001B48 RID: 6984
		public NKM_GAME_AUTO_SKILL_TYPE m_NKM_GAME_AUTO_SKILL_TYPE = NKM_GAME_AUTO_SKILL_TYPE.NGST_OFF_HYPER;
	}
}
