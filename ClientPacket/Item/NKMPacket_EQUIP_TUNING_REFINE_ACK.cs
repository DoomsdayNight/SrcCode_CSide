using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000E98 RID: 3736
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_TUNING_REFINE_ACK)]
	public sealed class NKMPacket_EQUIP_TUNING_REFINE_ACK : ISerializable
	{
		// Token: 0x0600981C RID: 38940 RVA: 0x0032DF63 File Offset: 0x0032C163
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGetEnum<NKM_EQUIP_REFINE_RESULT>(ref this.equipRefineResult);
			stream.PutOrGet(ref this.precision);
			stream.PutOrGet<NKMEquipItemData>(ref this.equipItemData);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
		}

		// Token: 0x04008A71 RID: 35441
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008A72 RID: 35442
		public NKM_EQUIP_REFINE_RESULT equipRefineResult;

		// Token: 0x04008A73 RID: 35443
		public int precision;

		// Token: 0x04008A74 RID: 35444
		public NKMEquipItemData equipItemData;

		// Token: 0x04008A75 RID: 35445
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();
	}
}
