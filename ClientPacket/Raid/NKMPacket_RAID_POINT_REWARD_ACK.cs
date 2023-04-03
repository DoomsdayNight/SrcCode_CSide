using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Raid
{
	// Token: 0x02000D6B RID: 3435
	[PacketId(ClientPacketId.kNKMPacket_RAID_POINT_REWARD_ACK)]
	public sealed class NKMPacket_RAID_POINT_REWARD_ACK : ISerializable
	{
		// Token: 0x060095D1 RID: 38353 RVA: 0x0032ABC3 File Offset: 0x00328DC3
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
		}

		// Token: 0x040087AB RID: 34731
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040087AC RID: 34732
		public NKMRewardData rewardData;
	}
}
