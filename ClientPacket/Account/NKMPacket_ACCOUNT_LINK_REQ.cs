using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001070 RID: 4208
	[PacketId(ClientPacketId.kNKMPacket_ACCOUNT_LINK_REQ)]
	public sealed class NKMPacket_ACCOUNT_LINK_REQ : ISerializable
	{
		// Token: 0x06009B9D RID: 39837 RVA: 0x00333A03 File Offset: 0x00331C03
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
