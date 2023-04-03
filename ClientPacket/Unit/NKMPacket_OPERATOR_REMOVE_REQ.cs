using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D00 RID: 3328
	[PacketId(ClientPacketId.kNKMPacket_OPERATOR_REMOVE_REQ)]
	public sealed class NKMPacket_OPERATOR_REMOVE_REQ : ISerializable
	{
		// Token: 0x060094FD RID: 38141 RVA: 0x003297F8 File Offset: 0x003279F8
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.removeUnitUIDList);
		}

		// Token: 0x0400868B RID: 34443
		public List<long> removeUnitUIDList = new List<long>();
	}
}
