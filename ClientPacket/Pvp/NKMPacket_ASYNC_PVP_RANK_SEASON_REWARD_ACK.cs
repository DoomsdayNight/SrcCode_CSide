using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D8A RID: 3466
	[PacketId(ClientPacketId.kNKMPacket_ASYNC_PVP_RANK_SEASON_REWARD_ACK)]
	public sealed class NKMPacket_ASYNC_PVP_RANK_SEASON_REWARD_ACK : ISerializable
	{
		// Token: 0x0600960F RID: 38415 RVA: 0x0032B2DD File Offset: 0x003294DD
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet<PvpState>(ref this.pvpState);
			stream.PutOrGet<NpcPvpData>(ref this.npcPvpData);
		}

		// Token: 0x04008812 RID: 34834
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008813 RID: 34835
		public NKMRewardData rewardData;

		// Token: 0x04008814 RID: 34836
		public PvpState pvpState;

		// Token: 0x04008815 RID: 34837
		public NpcPvpData npcPvpData;
	}
}
