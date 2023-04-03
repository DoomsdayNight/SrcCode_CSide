using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CBC RID: 3260
	[PacketId(ClientPacketId.kNKMPacket_POST_RECEIVE_REQ)]
	public sealed class NKMPacket_POST_RECEIVE_REQ : ISerializable
	{
		// Token: 0x06009475 RID: 38005 RVA: 0x00328C0F File Offset: 0x00326E0F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.postIndex);
		}

		// Token: 0x040085E4 RID: 34276
		public long postIndex;
	}
}
