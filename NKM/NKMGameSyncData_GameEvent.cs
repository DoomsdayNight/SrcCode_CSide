using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x0200040B RID: 1035
	public class NKMGameSyncData_GameEvent : ISerializable
	{
		// Token: 0x06001B22 RID: 6946 RVA: 0x00077195 File Offset: 0x00075395
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_GAME_EVENT_TYPE>(ref this.m_NKM_GAME_EVENT_TYPE);
			stream.PutOrGetEnum<NKM_TEAM_TYPE>(ref this.m_NKM_TEAM_TYPE);
			stream.PutOrGet(ref this.m_EventID);
			stream.PutOrGet(ref this.m_fValue);
		}

		// Token: 0x04001AD4 RID: 6868
		public NKM_GAME_EVENT_TYPE m_NKM_GAME_EVENT_TYPE;

		// Token: 0x04001AD5 RID: 6869
		public NKM_TEAM_TYPE m_NKM_TEAM_TYPE;

		// Token: 0x04001AD6 RID: 6870
		public int m_EventID;

		// Token: 0x04001AD7 RID: 6871
		public float m_fValue;
	}
}
