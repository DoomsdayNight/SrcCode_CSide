using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D7C RID: 3452
	[PacketId(ClientPacketId.kNKMPacket_PVP_RANK_WEEK_REWARD_ACK)]
	public sealed class NKMPacket_PVP_RANK_WEEK_REWARD_ACK : ISerializable
	{
		// Token: 0x060095F3 RID: 38387 RVA: 0x0032B025 File Offset: 0x00329225
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet<PvpState>(ref this.pvpData);
			stream.PutOrGet(ref this.isScoreChanged);
		}

		// Token: 0x040087EB RID: 34795
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040087EC RID: 34796
		public NKMRewardData rewardData;

		// Token: 0x040087ED RID: 34797
		public PvpState pvpData;

		// Token: 0x040087EE RID: 34798
		public bool isScoreChanged;
	}
}
