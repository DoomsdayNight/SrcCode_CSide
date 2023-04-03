using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FD2 RID: 4050
	[PacketId(ClientPacketId.kNKMPacket_FRIEND_SEARCH_REQ)]
	public sealed class NKMPacket_FRIEND_SEARCH_REQ : ISerializable
	{
		// Token: 0x06009A74 RID: 39540 RVA: 0x003317A5 File Offset: 0x0032F9A5
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.searchKeyword);
		}

		// Token: 0x04008DD0 RID: 36304
		public string searchKeyword;
	}
}
