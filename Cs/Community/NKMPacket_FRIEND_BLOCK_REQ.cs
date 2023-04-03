using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FDA RID: 4058
	[PacketId(ClientPacketId.kNKMPacket_FRIEND_BLOCK_REQ)]
	public sealed class NKMPacket_FRIEND_BLOCK_REQ : ISerializable
	{
		// Token: 0x06009A84 RID: 39556 RVA: 0x0033189B File Offset: 0x0032FA9B
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.friendCode);
			stream.PutOrGet(ref this.isCancel);
		}

		// Token: 0x04008DDC RID: 36316
		public long friendCode;

		// Token: 0x04008DDD RID: 36317
		public bool isCancel;
	}
}
