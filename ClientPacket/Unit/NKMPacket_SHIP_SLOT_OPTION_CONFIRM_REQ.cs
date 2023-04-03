using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D16 RID: 3350
	[PacketId(ClientPacketId.kNKMPacket_SHIP_SLOT_OPTION_CONFIRM_REQ)]
	public sealed class NKMPacket_SHIP_SLOT_OPTION_CONFIRM_REQ : ISerializable
	{
		// Token: 0x06009529 RID: 38185 RVA: 0x00329C32 File Offset: 0x00327E32
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.shipUid);
			stream.PutOrGet(ref this.moduleId);
		}

		// Token: 0x040086C6 RID: 34502
		public long shipUid;

		// Token: 0x040086C7 RID: 34503
		public int moduleId;
	}
}
