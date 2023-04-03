using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DCD RID: 3533
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_PVP_CREATE_ROOM_REQ)]
	public sealed class NKMPacket_PRIVATE_PVP_CREATE_ROOM_REQ : ISerializable
	{
		// Token: 0x06009693 RID: 38547 RVA: 0x0032BB11 File Offset: 0x00329D11
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMPrivateGameConfig>(ref this.config);
		}

		// Token: 0x0400887D RID: 34941
		public NKMPrivateGameConfig config = new NKMPrivateGameConfig();
	}
}
