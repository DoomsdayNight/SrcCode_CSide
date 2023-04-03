using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F9C RID: 3996
	[PacketId(ClientPacketId.kNKMPacket_WECHAT_COUPON_REWARD_REQ)]
	public sealed class NKMPacket_WECHAT_COUPON_REWARD_REQ : ISerializable
	{
		// Token: 0x06009A0E RID: 39438 RVA: 0x00330DF2 File Offset: 0x0032EFF2
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.templetId);
		}

		// Token: 0x04008D42 RID: 36162
		public int templetId;
	}
}
