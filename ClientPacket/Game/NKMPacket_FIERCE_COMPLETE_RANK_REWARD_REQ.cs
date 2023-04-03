using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F5D RID: 3933
	[PacketId(ClientPacketId.kNKMPacket_FIERCE_COMPLETE_RANK_REWARD_REQ)]
	public sealed class NKMPacket_FIERCE_COMPLETE_RANK_REWARD_REQ : ISerializable
	{
		// Token: 0x0600999A RID: 39322 RVA: 0x003303E0 File Offset: 0x0032E5E0
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
