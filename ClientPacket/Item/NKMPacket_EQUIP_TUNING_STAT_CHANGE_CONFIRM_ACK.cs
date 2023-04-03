using System;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000E9C RID: 3740
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_TUNING_STAT_CHANGE_CONFIRM_ACK)]
	public sealed class NKMPacket_EQUIP_TUNING_STAT_CHANGE_CONFIRM_ACK : ISerializable
	{
		// Token: 0x06009824 RID: 38948 RVA: 0x0032E062 File Offset: 0x0032C262
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMEquipItemData>(ref this.equipItemData);
			stream.PutOrGet<NKMEquipTuningCandidate>(ref this.equipTuningCandidate);
		}

		// Token: 0x04008A7F RID: 35455
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008A80 RID: 35456
		public NKMEquipItemData equipItemData;

		// Token: 0x04008A81 RID: 35457
		public NKMEquipTuningCandidate equipTuningCandidate = new NKMEquipTuningCandidate();
	}
}
