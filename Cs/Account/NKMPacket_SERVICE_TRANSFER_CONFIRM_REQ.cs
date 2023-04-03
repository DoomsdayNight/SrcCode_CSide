using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x0200109F RID: 4255
	[PacketId(ClientPacketId.kNKMPacket_SERVICE_TRANSFER_CONFIRM_REQ)]
	public sealed class NKMPacket_SERVICE_TRANSFER_CONFIRM_REQ : ISerializable
	{
		// Token: 0x06009BFB RID: 39931 RVA: 0x003340BB File Offset: 0x003322BB
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
