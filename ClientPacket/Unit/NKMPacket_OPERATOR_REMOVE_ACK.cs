using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D01 RID: 3329
	[PacketId(ClientPacketId.kNKMPacket_OPERATOR_REMOVE_ACK)]
	public sealed class NKMPacket_OPERATOR_REMOVE_ACK : ISerializable
	{
		// Token: 0x060094FF RID: 38143 RVA: 0x00329819 File Offset: 0x00327A19
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.removeUnitUIDList);
			stream.PutOrGet<NKMItemMiscData>(ref this.rewardItemDataList);
		}

		// Token: 0x0400868C RID: 34444
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400868D RID: 34445
		public List<long> removeUnitUIDList = new List<long>();

		// Token: 0x0400868E RID: 34446
		public List<NKMItemMiscData> rewardItemDataList = new List<NKMItemMiscData>();
	}
}
