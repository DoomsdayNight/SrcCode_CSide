using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Raid
{
	// Token: 0x02000D66 RID: 3430
	[PacketId(ClientPacketId.kNKMPacket_RAID_RESULT_ACCEPT_ACK)]
	public sealed class NKMPacket_RAID_RESULT_ACCEPT_ACK : ISerializable
	{
		// Token: 0x060095C7 RID: 38343 RVA: 0x0032AB03 File Offset: 0x00328D03
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.raidUID);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet(ref this.rewardRaidPoint);
		}

		// Token: 0x040087A1 RID: 34721
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040087A2 RID: 34722
		public long raidUID;

		// Token: 0x040087A3 RID: 34723
		public NKMRewardData rewardData;

		// Token: 0x040087A4 RID: 34724
		public int rewardRaidPoint;
	}
}
