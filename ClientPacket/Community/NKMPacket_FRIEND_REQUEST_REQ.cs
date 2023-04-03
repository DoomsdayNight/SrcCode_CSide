using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FD4 RID: 4052
	[PacketId(ClientPacketId.kNKMPacket_FRIEND_REQUEST_REQ)]
	public sealed class NKMPacket_FRIEND_REQUEST_REQ : ISerializable
	{
		// Token: 0x06009A78 RID: 39544 RVA: 0x003317E8 File Offset: 0x0032F9E8
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.friendCode);
		}

		// Token: 0x04008DD3 RID: 36307
		public long friendCode;
	}
}
