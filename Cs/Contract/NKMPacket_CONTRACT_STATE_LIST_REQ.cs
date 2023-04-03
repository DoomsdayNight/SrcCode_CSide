using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Contract
{
	// Token: 0x02000FC1 RID: 4033
	[PacketId(ClientPacketId.kNKMPacket_CONTRACT_STATE_LIST_REQ)]
	public sealed class NKMPacket_CONTRACT_STATE_LIST_REQ : ISerializable
	{
		// Token: 0x06009A52 RID: 39506 RVA: 0x0033147C File Offset: 0x0032F67C
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
