using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FDF RID: 4063
	[PacketId(ClientPacketId.kNKMPacket_FRIEND_ACCEPT_REQ)]
	public sealed class NKMPacket_FRIEND_ACCEPT_REQ : ISerializable
	{
		// Token: 0x06009A8E RID: 39566 RVA: 0x00331945 File Offset: 0x0032FB45
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.friendCode);
			stream.PutOrGet(ref this.isAllow);
		}

		// Token: 0x04008DE6 RID: 36326
		public long friendCode;

		// Token: 0x04008DE7 RID: 36327
		public bool isAllow;
	}
}
