using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DB7 RID: 3511
	[PacketId(ClientPacketId.kNKMPacket_LEAGUE_PVP_WEEKLY_REWARD_ACK)]
	public sealed class NKMPacket_LEAGUE_PVP_WEEKLY_REWARD_ACK : ISerializable
	{
		// Token: 0x06009667 RID: 38503 RVA: 0x0032B849 File Offset: 0x00329A49
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet(ref this.weekId);
		}

		// Token: 0x0400885F RID: 34911
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008860 RID: 34912
		public NKMRewardData rewardData;

		// Token: 0x04008861 RID: 34913
		public int weekId;
	}
}
