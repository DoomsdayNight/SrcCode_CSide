using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D1A RID: 3354
	[PacketId(ClientPacketId.kNKMPacket_SHIP_SLOT_OPTION_CANCEL_REQ)]
	public sealed class NKMPacket_SHIP_SLOT_OPTION_CANCEL_REQ : ISerializable
	{
		// Token: 0x06009531 RID: 38193 RVA: 0x00329CBA File Offset: 0x00327EBA
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
