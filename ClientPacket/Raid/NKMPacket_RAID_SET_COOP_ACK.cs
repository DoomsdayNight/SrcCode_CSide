using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Raid
{
	// Token: 0x02000D60 RID: 3424
	[PacketId(ClientPacketId.kNKMPacket_RAID_SET_COOP_ACK)]
	public sealed class NKMPacket_RAID_SET_COOP_ACK : ISerializable
	{
		// Token: 0x060095BB RID: 38331 RVA: 0x0032AA3A File Offset: 0x00328C3A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.raidUID);
			stream.PutOrGet<NKMRaidJoinData>(ref this.raidJoinDataList);
		}

		// Token: 0x04008798 RID: 34712
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008799 RID: 34713
		public long raidUID;

		// Token: 0x0400879A RID: 34714
		public List<NKMRaidJoinData> raidJoinDataList = new List<NKMRaidJoinData>();
	}
}
