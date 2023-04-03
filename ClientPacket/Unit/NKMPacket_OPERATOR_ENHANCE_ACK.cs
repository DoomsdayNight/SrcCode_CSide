using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CFD RID: 3325
	[PacketId(ClientPacketId.kNKMPacket_OPERATOR_ENHANCE_ACK)]
	public sealed class NKMPacket_OPERATOR_ENHANCE_ACK : ISerializable
	{
		// Token: 0x060094F7 RID: 38135 RVA: 0x00329762 File Offset: 0x00327962
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMOperator>(ref this.operatorUnit);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemData);
			stream.PutOrGet(ref this.sourceUnitUid);
			stream.PutOrGet(ref this.transSkill);
		}

		// Token: 0x04008681 RID: 34433
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008682 RID: 34434
		public NKMOperator operatorUnit;

		// Token: 0x04008683 RID: 34435
		public NKMItemMiscData costItemData;

		// Token: 0x04008684 RID: 34436
		public long sourceUnitUid;

		// Token: 0x04008685 RID: 34437
		public bool transSkill;
	}
}
