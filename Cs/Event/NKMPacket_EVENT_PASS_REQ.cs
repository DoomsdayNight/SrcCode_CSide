using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F7B RID: 3963
	[PacketId(ClientPacketId.kNKMPacket_EVENT_PASS_REQ)]
	public sealed class NKMPacket_EVENT_PASS_REQ : ISerializable
	{
		// Token: 0x060099D2 RID: 39378 RVA: 0x0033090D File Offset: 0x0032EB0D
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
