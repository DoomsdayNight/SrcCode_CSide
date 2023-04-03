using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F8A RID: 3978
	[PacketId(ClientPacketId.kNKMPacket_EVENT_PASS_LEVEL_UP_ACK)]
	public sealed class NKMPacket_EVENT_PASS_LEVEL_UP_ACK : ISerializable
	{
		// Token: 0x060099F0 RID: 39408 RVA: 0x00330B32 File Offset: 0x0032ED32
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.totalExp);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemList);
		}

		// Token: 0x04008D0C RID: 36108
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008D0D RID: 36109
		public int totalExp;

		// Token: 0x04008D0E RID: 36110
		public List<NKMItemMiscData> costItemList = new List<NKMItemMiscData>();
	}
}
