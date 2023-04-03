using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Contract
{
	// Token: 0x02000FC6 RID: 4038
	[PacketId(ClientPacketId.kNKMPacket_INSTANT_CONTRACT_LIST_REQ)]
	public sealed class NKMPacket_INSTANT_CONTRACT_LIST_REQ : ISerializable
	{
		// Token: 0x06009A5C RID: 39516 RVA: 0x0033153B File Offset: 0x0032F73B
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
