using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000E90 RID: 3728
	[PacketId(ClientPacketId.kNKMPacket_CRAFT_UNLOCK_SLOT_ACK)]
	public sealed class NKMPacket_CRAFT_UNLOCK_SLOT_ACK : ISerializable
	{
		// Token: 0x0600980C RID: 38924 RVA: 0x0032DE06 File Offset: 0x0032C006
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMCraftSlotData>(ref this.craftSlotData);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
		}

		// Token: 0x04008A5D RID: 35421
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008A5E RID: 35422
		public NKMCraftSlotData craftSlotData;

		// Token: 0x04008A5F RID: 35423
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();
	}
}
