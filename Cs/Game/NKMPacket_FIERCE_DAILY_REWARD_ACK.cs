using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F65 RID: 3941
	[PacketId(ClientPacketId.kNKMPacket_FIERCE_DAILY_REWARD_ACK)]
	public sealed class NKMPacket_FIERCE_DAILY_REWARD_ACK : ISerializable
	{
		// Token: 0x060099AA RID: 39338 RVA: 0x003304B3 File Offset: 0x0032E6B3
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet(ref this.fierceDailyRewardReceived);
		}

		// Token: 0x04008CAD RID: 36013
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008CAE RID: 36014
		public NKMRewardData rewardData;

		// Token: 0x04008CAF RID: 36015
		public bool fierceDailyRewardReceived;
	}
}
