using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EB4 RID: 3764
	[PacketId(ClientPacketId.kNKMPacket_CHARGE_ITEM_NOT)]
	public sealed class NKMPacket_CHARGE_ITEM_NOT : ISerializable
	{
		// Token: 0x06009854 RID: 38996 RVA: 0x0032E453 File Offset: 0x0032C653
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.lastUpdateDate);
			stream.PutOrGet<NKMItemMiscData>(ref this.itemData);
		}

		// Token: 0x04008AB5 RID: 35509
		public DateTime lastUpdateDate;

		// Token: 0x04008AB6 RID: 35510
		public NKMItemMiscData itemData;
	}
}
