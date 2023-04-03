using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000401 RID: 1025
	public class NKMGameSyncData_GameState : ISerializable
	{
		// Token: 0x06001B0F RID: 6927 RVA: 0x00076F17 File Offset: 0x00075117
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_GAME_STATE>(ref this.m_NKM_GAME_STATE);
			stream.PutOrGetEnum<NKM_TEAM_TYPE>(ref this.m_WinTeam);
			stream.PutOrGet(ref this.m_WaveID);
		}

		// Token: 0x04001AB3 RID: 6835
		public NKM_GAME_STATE m_NKM_GAME_STATE;

		// Token: 0x04001AB4 RID: 6836
		public NKM_TEAM_TYPE m_WinTeam;

		// Token: 0x04001AB5 RID: 6837
		public int m_WaveID;
	}
}
