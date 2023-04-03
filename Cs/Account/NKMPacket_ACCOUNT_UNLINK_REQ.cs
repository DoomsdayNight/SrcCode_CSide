using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001073 RID: 4211
	[PacketId(ClientPacketId.kNKMPacket_ACCOUNT_UNLINK_REQ)]
	public sealed class NKMPacket_ACCOUNT_UNLINK_REQ : ISerializable
	{
		// Token: 0x06009BA3 RID: 39843 RVA: 0x00333A39 File Offset: 0x00331C39
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
