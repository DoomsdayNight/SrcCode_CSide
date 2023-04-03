using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x0200109B RID: 4251
	[PacketId(ClientPacketId.kNKMPacket_SERVICE_TRANSFER_USER_VALIDATION_REQ)]
	public sealed class NKMPacket_SERVICE_TRANSFER_USER_VALIDATION_REQ : ISerializable
	{
		// Token: 0x06009BF3 RID: 39923 RVA: 0x0033404C File Offset: 0x0033224C
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
