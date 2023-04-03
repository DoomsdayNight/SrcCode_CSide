using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CD1 RID: 3281
	[PacketId(ClientPacketId.kNKMPacket_INVENTORY_EXPAND_ACK)]
	public sealed class NKMPacket_INVENTORY_EXPAND_ACK : ISerializable
	{
		// Token: 0x0600949F RID: 38047 RVA: 0x00328FF3 File Offset: 0x003271F3
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGetEnum<NKM_INVENTORY_EXPAND_TYPE>(ref this.inventoryExpandType);
			stream.PutOrGet(ref this.expandedCount);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
		}

		// Token: 0x0400861E RID: 34334
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400861F RID: 34335
		public NKM_INVENTORY_EXPAND_TYPE inventoryExpandType;

		// Token: 0x04008620 RID: 34336
		public int expandedCount;

		// Token: 0x04008621 RID: 34337
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();
	}
}
