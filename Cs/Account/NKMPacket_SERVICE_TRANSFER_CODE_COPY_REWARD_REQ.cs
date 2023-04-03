using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001099 RID: 4249
	[PacketId(ClientPacketId.kNKMPacket_SERVICE_TRANSFER_CODE_COPY_REWARD_REQ)]
	public sealed class NKMPacket_SERVICE_TRANSFER_CODE_COPY_REWARD_REQ : ISerializable
	{
		// Token: 0x06009BEF RID: 39919 RVA: 0x0033402C File Offset: 0x0033222C
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
