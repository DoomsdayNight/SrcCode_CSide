using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000408 RID: 1032
	public class NKMGameSyncData_DungeonEvent : ISerializable
	{
		// Token: 0x06001B1C RID: 6940 RVA: 0x0007710C File Offset: 0x0007530C
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_EVENT_ACTION_TYPE>(ref this.m_eEventActionType);
			stream.PutOrGet(ref this.m_EventID);
			stream.PutOrGet(ref this.m_iEventActionValue);
			stream.PutOrGet(ref this.m_strEventActionValue);
			stream.PutOrGet(ref this.m_bPause);
			stream.PutOrGetEnum<NKM_TEAM_TYPE>(ref this.m_eTeam);
		}

		// Token: 0x04001ACC RID: 6860
		public NKM_EVENT_ACTION_TYPE m_eEventActionType;

		// Token: 0x04001ACD RID: 6861
		public int m_EventID;

		// Token: 0x04001ACE RID: 6862
		public int m_iEventActionValue;

		// Token: 0x04001ACF RID: 6863
		public string m_strEventActionValue;

		// Token: 0x04001AD0 RID: 6864
		public bool m_bPause;

		// Token: 0x04001AD1 RID: 6865
		public NKM_TEAM_TYPE m_eTeam;
	}
}
