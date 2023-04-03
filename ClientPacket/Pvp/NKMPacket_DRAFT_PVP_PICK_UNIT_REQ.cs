using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DAA RID: 3498
	[PacketId(ClientPacketId.kNKMPacket_DRAFT_PVP_PICK_UNIT_REQ)]
	public sealed class NKMPacket_DRAFT_PVP_PICK_UNIT_REQ : ISerializable
	{
		// Token: 0x0600964D RID: 38477 RVA: 0x0032B737 File Offset: 0x00329937
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitUid);
		}

		// Token: 0x04008853 RID: 34899
		public long unitUid;
	}
}
