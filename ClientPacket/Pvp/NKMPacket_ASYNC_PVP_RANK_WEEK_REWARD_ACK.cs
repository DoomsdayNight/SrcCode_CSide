using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D8C RID: 3468
	[PacketId(ClientPacketId.kNKMPacket_ASYNC_PVP_RANK_WEEK_REWARD_ACK)]
	public sealed class NKMPacket_ASYNC_PVP_RANK_WEEK_REWARD_ACK : ISerializable
	{
		// Token: 0x06009613 RID: 38419 RVA: 0x0032B321 File Offset: 0x00329521
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet(ref this.weekID);
		}

		// Token: 0x04008816 RID: 34838
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008817 RID: 34839
		public NKMRewardData rewardData;

		// Token: 0x04008818 RID: 34840
		public int weekID;
	}
}
