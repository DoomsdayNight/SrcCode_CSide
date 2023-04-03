using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001083 RID: 4227
	[PacketId(ClientPacketId.kNKMPacket_DUPLICATED_CONNECTED_NOT)]
	public sealed class NKMPacket_DUPLICATED_CONNECTED_NOT : ISerializable
	{
		// Token: 0x06009BC3 RID: 39875 RVA: 0x00333CFB File Offset: 0x00331EFB
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
