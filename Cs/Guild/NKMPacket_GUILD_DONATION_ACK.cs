using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F0D RID: 3853
	[PacketId(ClientPacketId.kNKMPacket_GUILD_DONATION_ACK)]
	public sealed class NKMPacket_GUILD_DONATION_ACK : ISerializable
	{
		// Token: 0x060098FA RID: 39162 RVA: 0x0032F42C File Offset: 0x0032D62C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.donationId);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet<NKMAdditionalReward>(ref this.additionalReward);
			stream.PutOrGet(ref this.donationCount);
			stream.PutOrGet(ref this.lastDailyResetDate);
		}

		// Token: 0x04008BB9 RID: 35769
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008BBA RID: 35770
		public int donationId;

		// Token: 0x04008BBB RID: 35771
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();

		// Token: 0x04008BBC RID: 35772
		public NKMRewardData rewardData;

		// Token: 0x04008BBD RID: 35773
		public NKMAdditionalReward additionalReward = new NKMAdditionalReward();

		// Token: 0x04008BBE RID: 35774
		public int donationCount;

		// Token: 0x04008BBF RID: 35775
		public DateTime lastDailyResetDate;
	}
}
