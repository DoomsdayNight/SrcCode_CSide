using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DB8 RID: 3512
	[PacketId(ClientPacketId.kNKMPacket_LEAGUE_PVP_SEASON_REWARD_REQ)]
	public sealed class NKMPacket_LEAGUE_PVP_SEASON_REWARD_REQ : ISerializable
	{
		// Token: 0x06009669 RID: 38505 RVA: 0x0032B877 File Offset: 0x00329A77
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
