using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000E8C RID: 3724
	[PacketId(ClientPacketId.kNKMPacket_REMOVE_EQUIP_ITEM_ACK)]
	public sealed class NKMPacket_REMOVE_EQUIP_ITEM_ACK : ISerializable
	{
		// Token: 0x06009804 RID: 38916 RVA: 0x0032DD68 File Offset: 0x0032BF68
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.removeEquipItemUIDList);
			stream.PutOrGet<NKMItemMiscData>(ref this.rewardItemDataList);
		}

		// Token: 0x04008A55 RID: 35413
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008A56 RID: 35414
		public List<long> removeEquipItemUIDList = new List<long>();

		// Token: 0x04008A57 RID: 35415
		public List<NKMItemMiscData> rewardItemDataList = new List<NKMItemMiscData>();
	}
}
