using System;
using Cs.Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D20 RID: 3360
	public sealed class InstantProduct : ISerializable
	{
		// Token: 0x0600953D RID: 38205 RVA: 0x00329DB6 File Offset: 0x00327FB6
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.productId);
			stream.PutOrGet(ref this.endDate);
		}

		// Token: 0x040086DE RID: 34526
		public int productId;

		// Token: 0x040086DF RID: 34527
		public DateTime endDate;
	}
}
