using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FD7 RID: 4055
	[PacketId(ClientPacketId.kNKMPacket_FRIEND_DELETE_REQ)]
	public sealed class NKMPacket_FRIEND_DELETE_REQ : ISerializable
	{
		// Token: 0x06009A7E RID: 39550 RVA: 0x00331841 File Offset: 0x0032FA41
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.friendCode);
		}

		// Token: 0x04008DD7 RID: 36311
		public long friendCode;
	}
}
