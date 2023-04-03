using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CBA RID: 3258
	[PacketId(ClientPacketId.kNKMPacket_POST_LIST_REQ)]
	public sealed class NKMPacket_POST_LIST_REQ : ISerializable
	{
		// Token: 0x06009471 RID: 38001 RVA: 0x00328BC0 File Offset: 0x00326DC0
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.lastPostIndex);
		}

		// Token: 0x040085E0 RID: 34272
		public long lastPostIndex;
	}
}
