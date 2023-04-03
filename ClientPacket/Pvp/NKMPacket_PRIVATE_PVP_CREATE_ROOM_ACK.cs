using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DCE RID: 3534
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_PVP_CREATE_ROOM_ACK)]
	public sealed class NKMPacket_PRIVATE_PVP_CREATE_ROOM_ACK : ISerializable
	{
		// Token: 0x06009695 RID: 38549 RVA: 0x0032BB32 File Offset: 0x00329D32
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMPvpGameLobbyState>(ref this.lobbyState);
		}

		// Token: 0x0400887E RID: 34942
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400887F RID: 34943
		public NKMPvpGameLobbyState lobbyState = new NKMPvpGameLobbyState();
	}
}
