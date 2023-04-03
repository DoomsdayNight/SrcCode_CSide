using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CFB RID: 3323
	[PacketId(ClientPacketId.kNKMPacket_OPERATOR_LEVELUP_ACK)]
	public sealed class NKMPacket_OPERATOR_LEVELUP_ACK : ISerializable
	{
		// Token: 0x060094F3 RID: 38131 RVA: 0x003296FB File Offset: 0x003278FB
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemData);
			stream.PutOrGet<NKMOperator>(ref this.operatorUnit);
		}

		// Token: 0x0400867B RID: 34427
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400867C RID: 34428
		public List<NKMItemMiscData> costItemData = new List<NKMItemMiscData>();

		// Token: 0x0400867D RID: 34429
		public NKMOperator operatorUnit;
	}
}
