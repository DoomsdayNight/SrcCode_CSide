using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D9F RID: 3487
	[PacketId(ClientPacketId.kNKMPacket_LEAGUE_PVP_MATCH_REQ)]
	public sealed class NKMPacket_LEAGUE_PVP_MATCH_REQ : ISerializable
	{
		// Token: 0x06009637 RID: 38455 RVA: 0x0032B618 File Offset: 0x00329818
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
