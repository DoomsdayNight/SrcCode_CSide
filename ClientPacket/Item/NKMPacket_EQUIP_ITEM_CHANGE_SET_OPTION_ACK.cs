using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EA0 RID: 3744
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_ITEM_CHANGE_SET_OPTION_ACK)]
	public sealed class NKMPacket_EQUIP_ITEM_CHANGE_SET_OPTION_ACK : ISerializable
	{
		// Token: 0x0600982C RID: 38956 RVA: 0x0032E119 File Offset: 0x0032C319
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.equipUID);
			stream.PutOrGet(ref this.setOptionId);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemData);
			stream.PutOrGet<NKMEquipTuningCandidate>(ref this.equipTuningCandidate);
		}

		// Token: 0x04008A8A RID: 35466
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008A8B RID: 35467
		public long equipUID;

		// Token: 0x04008A8C RID: 35468
		public int setOptionId;

		// Token: 0x04008A8D RID: 35469
		public List<NKMItemMiscData> costItemData = new List<NKMItemMiscData>();

		// Token: 0x04008A8E RID: 35470
		public NKMEquipTuningCandidate equipTuningCandidate = new NKMEquipTuningCandidate();
	}
}
