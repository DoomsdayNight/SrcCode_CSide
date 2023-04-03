using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000404 RID: 1028
	public class NKMGameSyncData_ShipSkill : ISerializable
	{
		// Token: 0x06001B15 RID: 6933 RVA: 0x0007701A File Offset: 0x0007521A
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMUnitSyncData>(ref this.m_NKMGameUnitSyncData);
			stream.PutOrGet(ref this.m_ShipSkillID);
			stream.AsHalf(ref this.m_SkillPosX);
		}

		// Token: 0x04001AC0 RID: 6848
		public NKMUnitSyncData m_NKMGameUnitSyncData;

		// Token: 0x04001AC1 RID: 6849
		public int m_ShipSkillID;

		// Token: 0x04001AC2 RID: 6850
		public float m_SkillPosX;
	}
}
