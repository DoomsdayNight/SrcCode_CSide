using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001025 RID: 4133
	[PacketId(ClientPacketId.kNKMPacket_MENTORING_COMPLETE_INVITE_REWARD_ALL_REQ)]
	public sealed class NKMPacket_MENTORING_COMPLETE_INVITE_REWARD_ALL_REQ : ISerializable
	{
		// Token: 0x06009B1A RID: 39706 RVA: 0x003323BD File Offset: 0x003305BD
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
