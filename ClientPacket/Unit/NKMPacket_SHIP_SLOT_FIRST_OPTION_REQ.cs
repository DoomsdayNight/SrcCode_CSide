using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D18 RID: 3352
	[PacketId(ClientPacketId.kNKMPacket_SHIP_SLOT_FIRST_OPTION_REQ)]
	public sealed class NKMPacket_SHIP_SLOT_FIRST_OPTION_REQ : ISerializable
	{
		// Token: 0x0600952D RID: 38189 RVA: 0x00329C76 File Offset: 0x00327E76
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.shipUid);
			stream.PutOrGet(ref this.moduleId);
		}

		// Token: 0x040086CA RID: 34506
		public long shipUid;

		// Token: 0x040086CB RID: 34507
		public int moduleId;
	}
}
