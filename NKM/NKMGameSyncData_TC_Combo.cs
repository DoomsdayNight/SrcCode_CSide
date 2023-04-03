using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x0200040C RID: 1036
	public class NKMGameSyncData_TC_Combo : ISerializable
	{
		// Token: 0x06001B24 RID: 6948 RVA: 0x000771CF File Offset: 0x000753CF
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_TEAM_TYPE>(ref this.m_NKM_TEAM_TYPE);
			stream.PutOrGet(ref this.m_TCID);
			stream.PutOrGet(ref this.m_Combo);
		}

		// Token: 0x04001AD8 RID: 6872
		public NKM_TEAM_TYPE m_NKM_TEAM_TYPE;

		// Token: 0x04001AD9 RID: 6873
		public int m_TCID;

		// Token: 0x04001ADA RID: 6874
		public int m_Combo;
	}
}
