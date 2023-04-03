using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000FA1 RID: 4001
	[PacketId(ClientPacketId.kNKMPacket_ZLONG_CBT_PAYMENT_STATE_ACK)]
	public sealed class NKMPacket_ZLONG_CBT_PAYMENT_STATE_ACK : ISerializable
	{
		// Token: 0x06009A16 RID: 39446 RVA: 0x00330E79 File Offset: 0x0032F079
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<ZlongCbtPaymentData>(ref this.paymentData);
		}

		// Token: 0x04008D4E RID: 36174
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008D4F RID: 36175
		public ZlongCbtPaymentData paymentData = new ZlongCbtPaymentData();
	}
}
