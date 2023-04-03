using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D89 RID: 3465
	[PacketId(ClientPacketId.kNKMPacket_ASYNC_PVP_RANK_SEASON_REWARD_REQ)]
	public sealed class NKMPacket_ASYNC_PVP_RANK_SEASON_REWARD_REQ : ISerializable
	{
		// Token: 0x0600960D RID: 38413 RVA: 0x0032B2D3 File Offset: 0x003294D3
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
