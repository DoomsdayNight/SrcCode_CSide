using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000E89 RID: 3721
	[PacketId(ClientPacketId.kNKMPacket_LOCK_EQUIP_ITEM_REQ)]
	public sealed class NKMPacket_LOCK_EQUIP_ITEM_REQ : ISerializable
	{
		// Token: 0x060097FE RID: 38910 RVA: 0x0032DCF7 File Offset: 0x0032BEF7
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.equipItemUID);
			stream.PutOrGet(ref this.isLock);
		}

		// Token: 0x04008A4F RID: 35407
		public long equipItemUID;

		// Token: 0x04008A50 RID: 35408
		public bool isLock;
	}
}
