using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FDE RID: 4062
	[PacketId(ClientPacketId.kNKMPacket_FRIEND_CANCEL_REQUEST_NOT)]
	public sealed class NKMPacket_FRIEND_CANCEL_REQUEST_NOT : ISerializable
	{
		// Token: 0x06009A8C RID: 39564 RVA: 0x00331923 File Offset: 0x0032FB23
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.userUid);
			stream.PutOrGet(ref this.friendCode);
		}

		// Token: 0x04008DE4 RID: 36324
		public long userUid;

		// Token: 0x04008DE5 RID: 36325
		public long friendCode;
	}
}
