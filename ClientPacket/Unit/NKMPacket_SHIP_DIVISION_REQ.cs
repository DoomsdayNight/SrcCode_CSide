using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CF2 RID: 3314
	[PacketId(ClientPacketId.kNKMPacket_SHIP_DIVISION_REQ)]
	public sealed class NKMPacket_SHIP_DIVISION_REQ : ISerializable
	{
		// Token: 0x060094E1 RID: 38113 RVA: 0x0032957A File Offset: 0x0032777A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.removeShipUIDList);
		}

		// Token: 0x04008667 RID: 34407
		public List<long> removeShipUIDList = new List<long>();
	}
}
