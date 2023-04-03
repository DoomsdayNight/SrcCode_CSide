using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FDC RID: 4060
	[PacketId(ClientPacketId.kNKMPacket_FRIEND_CANCEL_REQUEST_REQ)]
	public sealed class NKMPacket_FRIEND_CANCEL_REQUEST_REQ : ISerializable
	{
		// Token: 0x06009A88 RID: 39560 RVA: 0x003318EB File Offset: 0x0032FAEB
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.friendCode);
		}

		// Token: 0x04008DE1 RID: 36321
		public long friendCode;
	}
}
