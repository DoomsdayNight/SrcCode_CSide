using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CE6 RID: 3302
	[PacketId(ClientPacketId.kNKMPacket_REMOVE_UNIT_REQ)]
	public sealed class NKMPacket_REMOVE_UNIT_REQ : ISerializable
	{
		// Token: 0x060094C9 RID: 38089 RVA: 0x0032934E File Offset: 0x0032754E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.removeUnitUIDList);
		}

		// Token: 0x0400864A RID: 34378
		public List<long> removeUnitUIDList = new List<long>();
	}
}
