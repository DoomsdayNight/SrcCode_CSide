using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DD8 RID: 3544
	[PacketId(ClientPacketId.kNKMPacket_PVP_ROOM_ACCEPT_INVITE_REQ)]
	public sealed class NKMPacket_PVP_ROOM_ACCEPT_INVITE_REQ : ISerializable
	{
		// Token: 0x060096A7 RID: 38567 RVA: 0x0032BC6D File Offset: 0x00329E6D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.targetUserUid);
			stream.PutOrGet(ref this.accept);
		}

		// Token: 0x04008891 RID: 34961
		public long targetUserUid;

		// Token: 0x04008892 RID: 34962
		public bool accept;
	}
}
