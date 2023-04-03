using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001021 RID: 4129
	[PacketId(ClientPacketId.kNKMPacket_MENTORING_INVITE_REWARD_LIST_REQ)]
	public sealed class NKMPacket_MENTORING_INVITE_REWARD_LIST_REQ : ISerializable
	{
		// Token: 0x06009B12 RID: 39698 RVA: 0x00332342 File Offset: 0x00330542
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
