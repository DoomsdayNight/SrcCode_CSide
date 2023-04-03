using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000403 RID: 1027
	public class NKMGameSyncData_DeckAssist : ISerializable
	{
		// Token: 0x06001B13 RID: 6931 RVA: 0x00076FF8 File Offset: 0x000751F8
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_TEAM_TYPE>(ref this.m_NKM_TEAM_TYPE);
			stream.PutOrGet(ref this.m_AutoRespawnIndexAssist);
		}

		// Token: 0x04001ABE RID: 6846
		public NKM_TEAM_TYPE m_NKM_TEAM_TYPE;

		// Token: 0x04001ABF RID: 6847
		public sbyte m_AutoRespawnIndexAssist = -1;
	}
}
