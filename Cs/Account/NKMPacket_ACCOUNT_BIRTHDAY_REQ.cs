using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001084 RID: 4228
	[PacketId(ClientPacketId.kNKMPacket_ACCOUNT_BIRTHDAY_REQ)]
	public sealed class NKMPacket_ACCOUNT_BIRTHDAY_REQ : ISerializable
	{
		// Token: 0x06009BC5 RID: 39877 RVA: 0x00333D05 File Offset: 0x00331F05
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
