using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D99 RID: 3481
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_PVP_ACCEPT_NOT)]
	public sealed class NKMPacket_PRIVATE_PVP_ACCEPT_NOT : ISerializable
	{
		// Token: 0x0600962B RID: 38443 RVA: 0x0032B57E File Offset: 0x0032977E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMPvpGameLobbyState>(ref this.lobbyState);
		}

		// Token: 0x04008841 RID: 34881
		public NKMPvpGameLobbyState lobbyState = new NKMPvpGameLobbyState();
	}
}
