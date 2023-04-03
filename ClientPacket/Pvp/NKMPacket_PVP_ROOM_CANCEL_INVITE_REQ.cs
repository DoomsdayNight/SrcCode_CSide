using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DD5 RID: 3541
	[PacketId(ClientPacketId.kNKMPacket_PVP_ROOM_CANCEL_INVITE_REQ)]
	public sealed class NKMPacket_PVP_ROOM_CANCEL_INVITE_REQ : ISerializable
	{
		// Token: 0x060096A1 RID: 38561 RVA: 0x0032BC13 File Offset: 0x00329E13
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.targetUserUid);
		}

		// Token: 0x0400888C RID: 34956
		public long targetUserUid;
	}
}
