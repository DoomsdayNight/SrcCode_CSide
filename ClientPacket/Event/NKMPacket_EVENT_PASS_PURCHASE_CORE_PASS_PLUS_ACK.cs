using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F87 RID: 3975
	[PacketId(ClientPacketId.kNKMPacket_EVENT_PASS_PURCHASE_CORE_PASS_PLUS_ACK)]
	public sealed class NKMPacket_EVENT_PASS_PURCHASE_CORE_PASS_PLUS_ACK : ISerializable
	{
		// Token: 0x060099EA RID: 39402 RVA: 0x00330AAE File Offset: 0x0032ECAE
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.totalExp);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemList);
		}

		// Token: 0x04008D05 RID: 36101
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008D06 RID: 36102
		public int totalExp;

		// Token: 0x04008D07 RID: 36103
		public List<NKMItemMiscData> costItemList = new List<NKMItemMiscData>();
	}
}
