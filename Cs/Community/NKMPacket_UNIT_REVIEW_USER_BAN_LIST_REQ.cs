using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x0200100F RID: 4111
	[PacketId(ClientPacketId.kNKMPacket_UNIT_REVIEW_USER_BAN_LIST_REQ)]
	public sealed class NKMPacket_UNIT_REVIEW_USER_BAN_LIST_REQ : ISerializable
	{
		// Token: 0x06009AEE RID: 39662 RVA: 0x003320C2 File Offset: 0x003302C2
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
