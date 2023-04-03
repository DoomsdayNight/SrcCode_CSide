using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DB9 RID: 3513
	[PacketId(ClientPacketId.kNKMPacket_LEAGUE_PVP_SEASON_REWARD_ACK)]
	public sealed class NKMPacket_LEAGUE_PVP_SEASON_REWARD_ACK : ISerializable
	{
		// Token: 0x0600966B RID: 38507 RVA: 0x0032B881 File Offset: 0x00329A81
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet<PvpState>(ref this.pvpData);
		}

		// Token: 0x04008862 RID: 34914
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008863 RID: 34915
		public NKMRewardData rewardData;

		// Token: 0x04008864 RID: 34916
		public PvpState pvpData;
	}
}
