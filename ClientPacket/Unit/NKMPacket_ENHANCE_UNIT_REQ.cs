using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CE2 RID: 3298
	[PacketId(ClientPacketId.kNKMPacket_ENHANCE_UNIT_REQ)]
	public sealed class NKMPacket_ENHANCE_UNIT_REQ : ISerializable
	{
		// Token: 0x060094C1 RID: 38081 RVA: 0x00329275 File Offset: 0x00327475
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitUID);
			stream.PutOrGet(ref this.consumeUnitUIDList);
		}

		// Token: 0x0400863E RID: 34366
		public long unitUID;

		// Token: 0x0400863F RID: 34367
		public List<long> consumeUnitUIDList = new List<long>();
	}
}
