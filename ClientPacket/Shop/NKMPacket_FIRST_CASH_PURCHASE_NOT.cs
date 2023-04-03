using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D2B RID: 3371
	[PacketId(ClientPacketId.kNKMPacket_FIRST_CASH_PURCHASE_NOT)]
	public sealed class NKMPacket_FIRST_CASH_PURCHASE_NOT : ISerializable
	{
		// Token: 0x06009553 RID: 38227 RVA: 0x0032A009 File Offset: 0x00328209
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
