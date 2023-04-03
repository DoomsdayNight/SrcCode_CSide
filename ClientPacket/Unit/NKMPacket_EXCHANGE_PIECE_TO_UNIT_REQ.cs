using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CF8 RID: 3320
	[PacketId(ClientPacketId.kNKMPacket_EXCHANGE_PIECE_TO_UNIT_REQ)]
	public sealed class NKMPacket_EXCHANGE_PIECE_TO_UNIT_REQ : ISerializable
	{
		// Token: 0x060094ED RID: 38125 RVA: 0x00329673 File Offset: 0x00327873
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.itemId);
			stream.PutOrGet(ref this.count);
		}

		// Token: 0x04008674 RID: 34420
		public int itemId;

		// Token: 0x04008675 RID: 34421
		public int count;
	}
}
