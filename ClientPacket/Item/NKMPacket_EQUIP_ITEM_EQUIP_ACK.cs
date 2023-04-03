using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000E86 RID: 3718
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_ITEM_EQUIP_ACK)]
	public sealed class NKMPacket_EQUIP_ITEM_EQUIP_ACK : ISerializable
	{
		// Token: 0x060097F8 RID: 38904 RVA: 0x0032DC0E File Offset: 0x0032BE0E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.unitUID);
			stream.PutOrGet(ref this.equipItemUID);
			stream.PutOrGet(ref this.unequipItemUID);
			stream.PutOrGetEnum<ITEM_EQUIP_POSITION>(ref this.equipPosition);
		}

		// Token: 0x04008A42 RID: 35394
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008A43 RID: 35395
		public long unitUID;

		// Token: 0x04008A44 RID: 35396
		public long equipItemUID;

		// Token: 0x04008A45 RID: 35397
		public long unequipItemUID;

		// Token: 0x04008A46 RID: 35398
		public ITEM_EQUIP_POSITION equipPosition;
	}
}
