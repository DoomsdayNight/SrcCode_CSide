using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DA8 RID: 3496
	[PacketId(ClientPacketId.kNKMPacket_DRAFT_PVP_GLOBAL_BAN_REQ)]
	public sealed class NKMPacket_DRAFT_PVP_GLOBAL_BAN_REQ : ISerializable
	{
		// Token: 0x06009649 RID: 38473 RVA: 0x0032B70B File Offset: 0x0032990B
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitId);
		}

		// Token: 0x04008851 RID: 34897
		public int unitId;
	}
}
