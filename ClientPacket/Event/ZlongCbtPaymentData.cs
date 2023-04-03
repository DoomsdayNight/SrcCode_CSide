using System;
using Cs.Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F9F RID: 3999
	public sealed class ZlongCbtPaymentData : ISerializable
	{
		// Token: 0x06009A12 RID: 39442 RVA: 0x00330E41 File Offset: 0x0032F041
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.totalPayment);
			stream.PutOrGet(ref this.rewardCount);
			stream.PutOrGetEnum<ZlongCbtPaymentState>(ref this.state);
		}

		// Token: 0x04008D4B RID: 36171
		public double totalPayment;

		// Token: 0x04008D4C RID: 36172
		public long rewardCount;

		// Token: 0x04008D4D RID: 36173
		public ZlongCbtPaymentState state;
	}
}
