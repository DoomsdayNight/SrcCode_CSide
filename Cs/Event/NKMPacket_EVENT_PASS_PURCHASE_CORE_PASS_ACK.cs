using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F85 RID: 3973
	[PacketId(ClientPacketId.kNKMPacket_EVENT_PASS_PURCHASE_CORE_PASS_ACK)]
	public sealed class NKMPacket_EVENT_PASS_PURCHASE_CORE_PASS_ACK : ISerializable
	{
		// Token: 0x060099E6 RID: 39398 RVA: 0x00330A77 File Offset: 0x0032EC77
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemList);
		}

		// Token: 0x04008D03 RID: 36099
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008D04 RID: 36100
		public List<NKMItemMiscData> costItemList = new List<NKMItemMiscData>();
	}
}
