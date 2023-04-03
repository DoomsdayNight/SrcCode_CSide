using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000E9A RID: 3738
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_TUNING_STAT_CHANGE_ACK)]
	public sealed class NKMPacket_EQUIP_TUNING_STAT_CHANGE_ACK : ISerializable
	{
		// Token: 0x06009820 RID: 38944 RVA: 0x0032DFDD File Offset: 0x0032C1DD
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.equipOptionID);
			stream.PutOrGet<NKMEquipItemData>(ref this.equipItemData);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
			stream.PutOrGet<NKMEquipTuningCandidate>(ref this.equipTuningCandidate);
		}

		// Token: 0x04008A78 RID: 35448
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008A79 RID: 35449
		public int equipOptionID;

		// Token: 0x04008A7A RID: 35450
		public NKMEquipItemData equipItemData;

		// Token: 0x04008A7B RID: 35451
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();

		// Token: 0x04008A7C RID: 35452
		public NKMEquipTuningCandidate equipTuningCandidate = new NKMEquipTuningCandidate();
	}
}
