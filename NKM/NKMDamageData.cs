using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000489 RID: 1161
	public class NKMDamageData : ISerializable
	{
		// Token: 0x06001F73 RID: 8051 RVA: 0x00094FF1 File Offset: 0x000931F1
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_GameUnitUIDAttacker);
			stream.PutOrGet(ref this.m_bAttackCountOver);
			stream.PutOrGet(ref this.m_FinalDamage);
			stream.PutOrGetEnum<NKM_DAMAGE_RESULT_TYPE>(ref this.m_NKM_DAMAGE_RESULT_TYPE);
		}

		// Token: 0x0400207D RID: 8317
		public long m_GameUnitUIDAttacker;

		// Token: 0x0400207E RID: 8318
		public bool m_bAttackCountOver;

		// Token: 0x0400207F RID: 8319
		public int m_FinalDamage;

		// Token: 0x04002080 RID: 8320
		public NKM_DAMAGE_RESULT_TYPE m_NKM_DAMAGE_RESULT_TYPE;
	}
}
