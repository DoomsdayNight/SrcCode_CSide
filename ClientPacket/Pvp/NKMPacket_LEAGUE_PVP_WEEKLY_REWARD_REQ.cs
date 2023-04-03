using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DB6 RID: 3510
	[PacketId(ClientPacketId.kNKMPacket_LEAGUE_PVP_WEEKLY_REWARD_REQ)]
	public sealed class NKMPacket_LEAGUE_PVP_WEEKLY_REWARD_REQ : ISerializable
	{
		// Token: 0x06009665 RID: 38501 RVA: 0x0032B83F File Offset: 0x00329A3F
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
