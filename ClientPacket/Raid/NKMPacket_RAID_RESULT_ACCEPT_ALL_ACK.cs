using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Raid
{
	// Token: 0x02000D68 RID: 3432
	[PacketId(ClientPacketId.kNKMPacket_RAID_RESULT_ACCEPT_ALL_ACK)]
	public sealed class NKMPacket_RAID_RESULT_ACCEPT_ALL_ACK : ISerializable
	{
		// Token: 0x060095CB RID: 38347 RVA: 0x0032AB47 File Offset: 0x00328D47
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.raidUids);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet(ref this.rewardRaidPoint);
		}

		// Token: 0x040087A5 RID: 34725
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040087A6 RID: 34726
		public List<long> raidUids = new List<long>();

		// Token: 0x040087A7 RID: 34727
		public NKMRewardData rewardData;

		// Token: 0x040087A8 RID: 34728
		public int rewardRaidPoint;
	}
}
