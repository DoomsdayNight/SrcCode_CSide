using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x0200107A RID: 4218
	[PacketId(ClientPacketId.kNKMPacket_CONTENTS_VERSION_REQ)]
	public sealed class NKMPacket_CONTENTS_VERSION_REQ : ISerializable
	{
		// Token: 0x06009BB1 RID: 39857 RVA: 0x00333B49 File Offset: 0x00331D49
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
