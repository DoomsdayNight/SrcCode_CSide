using System;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EA2 RID: 3746
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_ITEM_CONFIRM_SET_OPTION_ACK)]
	public sealed class NKMPacket_EQUIP_ITEM_CONFIRM_SET_OPTION_ACK : ISerializable
	{
		// Token: 0x06009830 RID: 38960 RVA: 0x0032E18B File Offset: 0x0032C38B
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.equipUID);
			stream.PutOrGet(ref this.setOptionId);
			stream.PutOrGet<NKMEquipTuningCandidate>(ref this.equipTuningCandidate);
		}

		// Token: 0x04008A90 RID: 35472
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008A91 RID: 35473
		public long equipUID;

		// Token: 0x04008A92 RID: 35474
		public int setOptionId;

		// Token: 0x04008A93 RID: 35475
		public NKMEquipTuningCandidate equipTuningCandidate = new NKMEquipTuningCandidate();
	}
}
