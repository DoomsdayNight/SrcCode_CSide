using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D34 RID: 3380
	[PacketId(ClientPacketId.kNKMPacket_ZLONG_USE_COUPON_ACK)]
	public sealed class NKMPacket_ZLONG_USE_COUPON_ACK : ISerializable
	{
		// Token: 0x06009565 RID: 38245 RVA: 0x0032A1B7 File Offset: 0x003283B7
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.zlongInfoCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
		}

		// Token: 0x04008714 RID: 34580
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008715 RID: 34581
		public int zlongInfoCode;

		// Token: 0x04008716 RID: 34582
		public NKMRewardData rewardData;
	}
}
