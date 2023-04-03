using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DC1 RID: 3521
	[PacketId(ClientPacketId.kNKMPacket_PVP_CASTING_VOTE_SHIP_REQ)]
	public sealed class NKMPacket_PVP_CASTING_VOTE_SHIP_REQ : ISerializable
	{
		// Token: 0x0600967B RID: 38523 RVA: 0x0032B9A3 File Offset: 0x00329BA3
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.shipIdList);
		}

		// Token: 0x0400886E RID: 34926
		public List<int> shipIdList = new List<int>();
	}
}
