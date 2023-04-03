using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F5E RID: 3934
	[PacketId(ClientPacketId.kNKMPacket_FIERCE_COMPLETE_RANK_REWARD_ACK)]
	public sealed class NKMPacket_FIERCE_COMPLETE_RANK_REWARD_ACK : ISerializable
	{
		// Token: 0x0600999C RID: 39324 RVA: 0x003303EA File Offset: 0x0032E5EA
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
		}

		// Token: 0x04008CA3 RID: 36003
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008CA4 RID: 36004
		public NKMRewardData rewardData;
	}
}
