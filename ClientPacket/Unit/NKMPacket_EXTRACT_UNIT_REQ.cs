using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D04 RID: 3332
	[PacketId(ClientPacketId.kNKMPacket_EXTRACT_UNIT_REQ)]
	public sealed class NKMPacket_EXTRACT_UNIT_REQ : ISerializable
	{
		// Token: 0x06009505 RID: 38149 RVA: 0x003298D0 File Offset: 0x00327AD0
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.extractUnitUidList);
		}

		// Token: 0x04008696 RID: 34454
		public List<long> extractUnitUidList = new List<long>();
	}
}
