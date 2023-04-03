using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EBA RID: 3770
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_UPGRADE_REQ)]
	public sealed class NKMPacket_EQUIP_UPGRADE_REQ : ISerializable
	{
		// Token: 0x06009860 RID: 39008 RVA: 0x0032E51D File Offset: 0x0032C71D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.equipUid);
			stream.PutOrGet(ref this.consumeEquipItemUidList);
		}

		// Token: 0x04008ABF RID: 35519
		public long equipUid;

		// Token: 0x04008AC0 RID: 35520
		public List<long> consumeEquipItemUidList = new List<long>();
	}
}
