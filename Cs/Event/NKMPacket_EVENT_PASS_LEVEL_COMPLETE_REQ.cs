using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F79 RID: 3961
	[PacketId(ClientPacketId.kNKMPacket_EVENT_PASS_LEVEL_COMPLETE_REQ)]
	public sealed class NKMPacket_EVENT_PASS_LEVEL_COMPLETE_REQ : ISerializable
	{
		// Token: 0x060099CE RID: 39374 RVA: 0x003308C9 File Offset: 0x0032EAC9
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
