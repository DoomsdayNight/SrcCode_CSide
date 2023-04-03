using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Raid
{
	// Token: 0x02000D62 RID: 3426
	[PacketId(ClientPacketId.kNKMPacket_RAID_DETAIL_INFO_ACK)]
	public sealed class NKMPacket_RAID_DETAIL_INFO_ACK : ISerializable
	{
		// Token: 0x060095BF RID: 38335 RVA: 0x0032AA89 File Offset: 0x00328C89
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRaidDetailData>(ref this.raidDetailData);
		}

		// Token: 0x0400879C RID: 34716
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400879D RID: 34717
		public NKMRaidDetailData raidDetailData = new NKMRaidDetailData();
	}
}
