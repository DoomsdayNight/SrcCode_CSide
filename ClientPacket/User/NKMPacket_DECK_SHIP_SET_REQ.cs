using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CB6 RID: 3254
	[PacketId(ClientPacketId.kNKMPacket_DECK_SHIP_SET_REQ)]
	public sealed class NKMPacket_DECK_SHIP_SET_REQ : ISerializable
	{
		// Token: 0x06009469 RID: 37993 RVA: 0x00328B08 File Offset: 0x00326D08
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMDeckIndex>(ref this.deckIndex);
			stream.PutOrGet(ref this.shipUID);
		}

		// Token: 0x040085D4 RID: 34260
		public NKMDeckIndex deckIndex;

		// Token: 0x040085D5 RID: 34261
		public long shipUID;
	}
}
