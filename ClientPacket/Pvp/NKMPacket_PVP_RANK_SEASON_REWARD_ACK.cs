using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D7E RID: 3454
	[PacketId(ClientPacketId.kNKMPacket_PVP_RANK_SEASON_REWARD_ACK)]
	public sealed class NKMPacket_PVP_RANK_SEASON_REWARD_ACK : ISerializable
	{
		// Token: 0x060095F7 RID: 38391 RVA: 0x0032B069 File Offset: 0x00329269
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet<PvpState>(ref this.pvpData);
			stream.PutOrGet<PvpState>(ref this.reducedPvpData);
			stream.PutOrGet(ref this.isScoreChanged);
		}

		// Token: 0x040087EF RID: 34799
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040087F0 RID: 34800
		public NKMRewardData rewardData;

		// Token: 0x040087F1 RID: 34801
		public PvpState pvpData;

		// Token: 0x040087F2 RID: 34802
		public PvpState reducedPvpData;

		// Token: 0x040087F3 RID: 34803
		public bool isScoreChanged;
	}
}
