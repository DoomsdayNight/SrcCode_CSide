using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Raid
{
	// Token: 0x02000D64 RID: 3428
	[PacketId(ClientPacketId.kNKMPacket_RAID_RESULT_LIST_ACK)]
	public sealed class NKMPacket_RAID_RESULT_LIST_ACK : ISerializable
	{
		// Token: 0x060095C3 RID: 38339 RVA: 0x0032AAC0 File Offset: 0x00328CC0
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRaidResultData>(ref this.raidResultDataList);
		}

		// Token: 0x0400879E RID: 34718
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400879F RID: 34719
		public List<NKMRaidResultData> raidResultDataList = new List<NKMRaidResultData>();
	}
}
