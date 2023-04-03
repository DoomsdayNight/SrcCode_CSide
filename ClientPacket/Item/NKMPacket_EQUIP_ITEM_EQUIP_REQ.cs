using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000E85 RID: 3717
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_ITEM_EQUIP_REQ)]
	public sealed class NKMPacket_EQUIP_ITEM_EQUIP_REQ : ISerializable
	{
		// Token: 0x060097F6 RID: 38902 RVA: 0x0032DBD4 File Offset: 0x0032BDD4
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.isEquip);
			stream.PutOrGet(ref this.unitUID);
			stream.PutOrGet(ref this.equipItemUID);
			stream.PutOrGetEnum<ITEM_EQUIP_POSITION>(ref this.equipPosition);
		}

		// Token: 0x04008A3E RID: 35390
		public bool isEquip;

		// Token: 0x04008A3F RID: 35391
		public long unitUID;

		// Token: 0x04008A40 RID: 35392
		public long equipItemUID;

		// Token: 0x04008A41 RID: 35393
		public ITEM_EQUIP_POSITION equipPosition;
	}
}
