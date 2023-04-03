using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F86 RID: 3974
	[PacketId(ClientPacketId.kNKMPacket_EVENT_PASS_PURCHASE_CORE_PASS_PLUS_REQ)]
	public sealed class NKMPacket_EVENT_PASS_PURCHASE_CORE_PASS_PLUS_REQ : ISerializable
	{
		// Token: 0x060099E8 RID: 39400 RVA: 0x00330AA4 File Offset: 0x0032ECA4
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
