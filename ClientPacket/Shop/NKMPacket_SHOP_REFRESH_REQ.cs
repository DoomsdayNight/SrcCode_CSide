using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D29 RID: 3369
	[PacketId(ClientPacketId.kNKMPacket_SHOP_REFRESH_REQ)]
	public sealed class NKMPacket_SHOP_REFRESH_REQ : ISerializable
	{
		// Token: 0x0600954F RID: 38223 RVA: 0x00329FC5 File Offset: 0x003281C5
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.isUseCash);
		}

		// Token: 0x040086FB RID: 34555
		public bool isUseCash;
	}
}
