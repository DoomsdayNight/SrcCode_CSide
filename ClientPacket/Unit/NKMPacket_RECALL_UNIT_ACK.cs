using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D03 RID: 3331
	[PacketId(ClientPacketId.kNKMPacket_RECALL_UNIT_ACK)]
	public sealed class NKMPacket_RECALL_UNIT_ACK : ISerializable
	{
		// Token: 0x06009503 RID: 38147 RVA: 0x0032987F File Offset: 0x00327A7F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.removeUnitUid);
			stream.PutOrGet<NKMUnitData>(ref this.exchangeUnitData);
			stream.PutOrGet<RecallHistoryInfo>(ref this.historyInfo);
			stream.PutOrGet<NKMItemMiscData>(ref this.rewardList);
		}

		// Token: 0x04008691 RID: 34449
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008692 RID: 34450
		public long removeUnitUid;

		// Token: 0x04008693 RID: 34451
		public NKMUnitData exchangeUnitData;

		// Token: 0x04008694 RID: 34452
		public RecallHistoryInfo historyInfo;

		// Token: 0x04008695 RID: 34453
		public List<NKMItemMiscData> rewardList = new List<NKMItemMiscData>();
	}
}
