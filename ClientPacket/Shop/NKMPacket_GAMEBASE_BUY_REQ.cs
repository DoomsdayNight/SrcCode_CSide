using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D36 RID: 3382
	[PacketId(ClientPacketId.kNKMPacket_GAMEBASE_BUY_REQ)]
	public sealed class NKMPacket_GAMEBASE_BUY_REQ : ISerializable
	{
		// Token: 0x06009569 RID: 38249 RVA: 0x0032A207 File Offset: 0x00328407
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.paymentSeq);
			stream.PutOrGet(ref this.accessToken);
			stream.PutOrGet(ref this.selectIndices);
			stream.PutOrGet(ref this.paymentId);
		}

		// Token: 0x04008719 RID: 34585
		public string paymentSeq;

		// Token: 0x0400871A RID: 34586
		public string accessToken;

		// Token: 0x0400871B RID: 34587
		public List<int> selectIndices = new List<int>();

		// Token: 0x0400871C RID: 34588
		public string paymentId;
	}
}
