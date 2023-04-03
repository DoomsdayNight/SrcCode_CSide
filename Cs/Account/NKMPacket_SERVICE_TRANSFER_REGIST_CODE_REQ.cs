using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001097 RID: 4247
	[PacketId(ClientPacketId.kNKMPacket_SERVICE_TRANSFER_REGIST_CODE_REQ)]
	public sealed class NKMPacket_SERVICE_TRANSFER_REGIST_CODE_REQ : ISerializable
	{
		// Token: 0x06009BEB RID: 39915 RVA: 0x00333FF4 File Offset: 0x003321F4
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
