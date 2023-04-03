using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D14 RID: 3348
	[PacketId(ClientPacketId.kNKMPacket_SHIP_SLOT_OPTION_CHANGE_REQ)]
	public sealed class NKMPacket_SHIP_SLOT_OPTION_CHANGE_REQ : ISerializable
	{
		// Token: 0x06009525 RID: 38181 RVA: 0x00329BC0 File Offset: 0x00327DC0
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.shipUid);
			stream.PutOrGet(ref this.moduleId);
		}

		// Token: 0x040086C0 RID: 34496
		public long shipUid;

		// Token: 0x040086C1 RID: 34497
		public int moduleId;
	}
}
