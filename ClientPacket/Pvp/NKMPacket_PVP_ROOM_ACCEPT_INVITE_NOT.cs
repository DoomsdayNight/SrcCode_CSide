using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DDA RID: 3546
	[PacketId(ClientPacketId.kNKMPacket_PVP_ROOM_ACCEPT_INVITE_NOT)]
	public sealed class NKMPacket_PVP_ROOM_ACCEPT_INVITE_NOT : ISerializable
	{
		// Token: 0x060096AB RID: 38571 RVA: 0x0032BCD5 File Offset: 0x00329ED5
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMPvpGameLobbyState>(ref this.lobbyState);
		}

		// Token: 0x04008898 RID: 34968
		public NKMPvpGameLobbyState lobbyState = new NKMPvpGameLobbyState();
	}
}
