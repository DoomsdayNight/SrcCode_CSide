using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Negotiation
{
	// Token: 0x02000E2D RID: 3629
	[PacketId(ClientPacketId.kNKMPacket_NEGOTIATE_ACK)]
	public sealed class NKMPacket_NEGOTIATE_ACK : ISerializable
	{
		// Token: 0x0600974A RID: 38730 RVA: 0x0032CC2C File Offset: 0x0032AE2C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGetEnum<NEGOTIATE_RESULT>(ref this.negotiateResult);
			stream.PutOrGet(ref this.finalSalary);
			stream.PutOrGet(ref this.targetUnitUid);
			stream.PutOrGet(ref this.targetUnitLevel);
			stream.PutOrGet(ref this.targetUnitLoyalty);
			stream.PutOrGet(ref this.targetUnitExp);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
		}

		// Token: 0x04008967 RID: 35175
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008968 RID: 35176
		public NEGOTIATE_RESULT negotiateResult;

		// Token: 0x04008969 RID: 35177
		public int finalSalary;

		// Token: 0x0400896A RID: 35178
		public long targetUnitUid;

		// Token: 0x0400896B RID: 35179
		public int targetUnitLevel;

		// Token: 0x0400896C RID: 35180
		public int targetUnitLoyalty;

		// Token: 0x0400896D RID: 35181
		public int targetUnitExp;

		// Token: 0x0400896E RID: 35182
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();
	}
}
