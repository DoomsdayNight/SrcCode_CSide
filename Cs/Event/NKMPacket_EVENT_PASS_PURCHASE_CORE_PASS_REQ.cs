using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F84 RID: 3972
	[PacketId(ClientPacketId.kNKMPacket_EVENT_PASS_PURCHASE_CORE_PASS_REQ)]
	public sealed class NKMPacket_EVENT_PASS_PURCHASE_CORE_PASS_REQ : ISerializable
	{
		// Token: 0x060099E4 RID: 39396 RVA: 0x00330A6D File Offset: 0x0032EC6D
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
