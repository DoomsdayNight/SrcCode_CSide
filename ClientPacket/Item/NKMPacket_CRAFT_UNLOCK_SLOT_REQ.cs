using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000E8F RID: 3727
	[PacketId(ClientPacketId.kNKMPacket_CRAFT_UNLOCK_SLOT_REQ)]
	public sealed class NKMPacket_CRAFT_UNLOCK_SLOT_REQ : ISerializable
	{
		// Token: 0x0600980A RID: 38922 RVA: 0x0032DDFC File Offset: 0x0032BFFC
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
