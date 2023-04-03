using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Service
{
	// Token: 0x02000D4A RID: 3402
	[PacketId(ClientPacketId.kNKMPacket_ACCOUNT_KICK_NOT)]
	public sealed class NKMPacket_ACCOUNT_KICK_NOT : ISerializable
	{
		// Token: 0x06009591 RID: 38289 RVA: 0x0032A4FE File Offset: 0x003286FE
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
