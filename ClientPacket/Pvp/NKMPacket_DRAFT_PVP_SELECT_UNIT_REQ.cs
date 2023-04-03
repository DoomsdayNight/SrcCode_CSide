using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DB4 RID: 3508
	[PacketId(ClientPacketId.kNKMPacket_DRAFT_PVP_SELECT_UNIT_REQ)]
	public sealed class NKMPacket_DRAFT_PVP_SELECT_UNIT_REQ : ISerializable
	{
		// Token: 0x06009661 RID: 38497 RVA: 0x0032B813 File Offset: 0x00329A13
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitUid);
		}

		// Token: 0x0400885D RID: 34909
		public long unitUid;
	}
}
