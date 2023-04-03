using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D7D RID: 3453
	[PacketId(ClientPacketId.kNKMPacket_PVP_RANK_SEASON_REWARD_REQ)]
	public sealed class NKMPacket_PVP_RANK_SEASON_REWARD_REQ : ISerializable
	{
		// Token: 0x060095F5 RID: 38389 RVA: 0x0032B05F File Offset: 0x0032925F
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
