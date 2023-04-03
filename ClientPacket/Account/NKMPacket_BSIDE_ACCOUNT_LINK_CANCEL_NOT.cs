using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001096 RID: 4246
	[PacketId(ClientPacketId.kNKMPacket_BSIDE_ACCOUNT_LINK_CANCEL_NOT)]
	public sealed class NKMPacket_BSIDE_ACCOUNT_LINK_CANCEL_NOT : ISerializable
	{
		// Token: 0x06009BE9 RID: 39913 RVA: 0x00333FEA File Offset: 0x003321EA
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
