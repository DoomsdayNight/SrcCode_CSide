using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000FA3 RID: 4003
	[PacketId(ClientPacketId.kNKMPacket_ZLONG_CBT_PAYMENT_REWARD_ACK)]
	public sealed class NKMPacket_ZLONG_CBT_PAYMENT_REWARD_ACK : ISerializable
	{
		// Token: 0x06009A1A RID: 39450 RVA: 0x00330EB0 File Offset: 0x0032F0B0
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet<ZlongCbtPaymentData>(ref this.paymentData);
		}

		// Token: 0x04008D50 RID: 36176
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008D51 RID: 36177
		public NKMRewardData rewardData;

		// Token: 0x04008D52 RID: 36178
		public ZlongCbtPaymentData paymentData = new ZlongCbtPaymentData();
	}
}
