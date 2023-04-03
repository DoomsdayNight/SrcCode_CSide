using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F9A RID: 3994
	[PacketId(ClientPacketId.kNKMPacket_WECHAT_COUPON_CHECK_REQ)]
	public sealed class NKMPacket_WECHAT_COUPON_CHECK_REQ : ISerializable
	{
		// Token: 0x06009A0A RID: 39434 RVA: 0x00330D97 File Offset: 0x0032EF97
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.templetId);
			stream.PutOrGet(ref this.zlongServerId);
		}

		// Token: 0x04008D3D RID: 36157
		public int templetId;

		// Token: 0x04008D3E RID: 36158
		public int zlongServerId;
	}
}
