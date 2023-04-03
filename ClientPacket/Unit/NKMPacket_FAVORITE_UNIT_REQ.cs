using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D0E RID: 3342
	[PacketId(ClientPacketId.kNKMPacket_FAVORITE_UNIT_REQ)]
	public sealed class NKMPacket_FAVORITE_UNIT_REQ : ISerializable
	{
		// Token: 0x06009519 RID: 38169 RVA: 0x00329A96 File Offset: 0x00327C96
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitUid);
			stream.PutOrGet(ref this.isFavorite);
		}

		// Token: 0x040086AE RID: 34478
		public long unitUid;

		// Token: 0x040086AF RID: 34479
		public bool isFavorite;
	}
}
