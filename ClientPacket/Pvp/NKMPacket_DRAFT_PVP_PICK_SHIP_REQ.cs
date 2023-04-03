using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DAE RID: 3502
	[PacketId(ClientPacketId.kNKMPacket_DRAFT_PVP_PICK_SHIP_REQ)]
	public sealed class NKMPacket_DRAFT_PVP_PICK_SHIP_REQ : ISerializable
	{
		// Token: 0x06009655 RID: 38485 RVA: 0x0032B78F File Offset: 0x0032998F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.shipUid);
		}

		// Token: 0x04008857 RID: 34903
		public long shipUid;
	}
}
