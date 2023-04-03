using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D27 RID: 3367
	[PacketId(ClientPacketId.kNKMPacket_SHOP_FIXED_LIST_REQ)]
	public sealed class NKMPacket_SHOP_FIXED_LIST_REQ : ISerializable
	{
		// Token: 0x0600954B RID: 38219 RVA: 0x00329F77 File Offset: 0x00328177
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
