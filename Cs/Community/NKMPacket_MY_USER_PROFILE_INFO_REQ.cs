using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001001 RID: 4097
	[PacketId(ClientPacketId.kNKMPacket_MY_USER_PROFILE_INFO_REQ)]
	public sealed class NKMPacket_MY_USER_PROFILE_INFO_REQ : ISerializable
	{
		// Token: 0x06009AD2 RID: 39634 RVA: 0x00331F01 File Offset: 0x00330101
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
