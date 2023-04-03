using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000E8B RID: 3723
	[PacketId(ClientPacketId.kNKMPacket_REMOVE_EQUIP_ITEM_REQ)]
	public sealed class NKMPacket_REMOVE_EQUIP_ITEM_REQ : ISerializable
	{
		// Token: 0x06009802 RID: 38914 RVA: 0x0032DD47 File Offset: 0x0032BF47
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.removeEquipItemUIDList);
		}

		// Token: 0x04008A54 RID: 35412
		public List<long> removeEquipItemUIDList = new List<long>();
	}
}
