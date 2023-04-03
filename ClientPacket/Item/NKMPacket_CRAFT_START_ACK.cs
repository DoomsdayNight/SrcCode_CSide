using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000E92 RID: 3730
	[PacketId(ClientPacketId.kNKMPacket_CRAFT_START_ACK)]
	public sealed class NKMPacket_CRAFT_START_ACK : ISerializable
	{
		// Token: 0x06009810 RID: 38928 RVA: 0x0032DE6D File Offset: 0x0032C06D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMCraftSlotData>(ref this.craftSlotData);
			stream.PutOrGet<NKMItemMiscData>(ref this.materialItemDataList);
		}

		// Token: 0x04008A63 RID: 35427
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008A64 RID: 35428
		public NKMCraftSlotData craftSlotData;

		// Token: 0x04008A65 RID: 35429
		public List<NKMItemMiscData> materialItemDataList = new List<NKMItemMiscData>();
	}
}
