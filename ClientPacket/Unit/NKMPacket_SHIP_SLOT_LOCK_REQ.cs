using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D12 RID: 3346
	[PacketId(ClientPacketId.kNKMPacket_SHIP_SLOT_LOCK_REQ)]
	public sealed class NKMPacket_SHIP_SLOT_LOCK_REQ : ISerializable
	{
		// Token: 0x06009521 RID: 38177 RVA: 0x00329B4D File Offset: 0x00327D4D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.shipUid);
			stream.PutOrGet(ref this.moduleId);
			stream.PutOrGet(ref this.slotId);
			stream.PutOrGet(ref this.locked);
		}

		// Token: 0x040086B9 RID: 34489
		public long shipUid;

		// Token: 0x040086BA RID: 34490
		public int moduleId;

		// Token: 0x040086BB RID: 34491
		public int slotId;

		// Token: 0x040086BC RID: 34492
		public bool locked;
	}
}
