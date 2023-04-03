using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001094 RID: 4244
	[PacketId(ClientPacketId.kNKMPacket_BSIDE_ACCOUNT_LINK_CANCEL_REQ)]
	public sealed class NKMPacket_BSIDE_ACCOUNT_LINK_CANCEL_REQ : ISerializable
	{
		// Token: 0x06009BE5 RID: 39909 RVA: 0x00333FCA File Offset: 0x003321CA
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
