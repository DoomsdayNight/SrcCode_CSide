using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D9E RID: 3486
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_PVP_LOBBY_STATE_NOT)]
	public sealed class NKMPacket_PRIVATE_PVP_LOBBY_STATE_NOT : ISerializable
	{
		// Token: 0x06009635 RID: 38453 RVA: 0x0032B5F7 File Offset: 0x003297F7
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMPvpGameLobbyState>(ref this.lobbyState);
		}

		// Token: 0x04008846 RID: 34886
		public NKMPvpGameLobbyState lobbyState = new NKMPvpGameLobbyState();
	}
}
