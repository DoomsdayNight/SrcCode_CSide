using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D98 RID: 3480
	public sealed class NKMPvpGameLobbyState : ISerializable
	{
		// Token: 0x06009629 RID: 38441 RVA: 0x0032B523 File Offset: 0x00329723
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_GAME_STATE>(ref this.gameState);
			stream.PutOrGet<NKMPrivateGameConfig>(ref this.config);
			stream.PutOrGet<NKMPvpGameLobbyUserState>(ref this.users);
			stream.PutOrGet<NKMPvpGameLobbyUserState>(ref this.observers);
		}

		// Token: 0x0400883D RID: 34877
		public NKM_GAME_STATE gameState;

		// Token: 0x0400883E RID: 34878
		public NKMPrivateGameConfig config = new NKMPrivateGameConfig();

		// Token: 0x0400883F RID: 34879
		public List<NKMPvpGameLobbyUserState> users = new List<NKMPvpGameLobbyUserState>();

		// Token: 0x04008840 RID: 34880
		public List<NKMPvpGameLobbyUserState> observers = new List<NKMPvpGameLobbyUserState>();
	}
}
