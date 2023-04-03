using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DBF RID: 3519
	[PacketId(ClientPacketId.kNKMPacket_PVP_CASTING_VOTE_UNIT_REQ)]
	public sealed class NKMPacket_PVP_CASTING_VOTE_UNIT_REQ : ISerializable
	{
		// Token: 0x06009677 RID: 38519 RVA: 0x0032B955 File Offset: 0x00329B55
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitIdList);
		}

		// Token: 0x0400886B RID: 34923
		public List<int> unitIdList = new List<int>();
	}
}
