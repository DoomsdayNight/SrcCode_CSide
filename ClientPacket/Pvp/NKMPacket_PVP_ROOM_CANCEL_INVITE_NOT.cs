using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DD7 RID: 3543
	[PacketId(ClientPacketId.kNKMPacket_PVP_ROOM_CANCEL_INVITE_NOT)]
	public sealed class NKMPacket_PVP_ROOM_CANCEL_INVITE_NOT : ISerializable
	{
		// Token: 0x060096A5 RID: 38565 RVA: 0x0032BC4B File Offset: 0x00329E4B
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.targetUserUid);
			stream.PutOrGetEnum<PrivatePvpCancelType>(ref this.cancelType);
		}

		// Token: 0x0400888F RID: 34959
		public long targetUserUid;

		// Token: 0x04008890 RID: 34960
		public PrivatePvpCancelType cancelType;
	}
}
