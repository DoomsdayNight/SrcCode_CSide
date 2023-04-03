using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DA1 RID: 3489
	[PacketId(ClientPacketId.kNKMPacket_LEAGUE_PVP_MATCH_CANCEL_REQ)]
	public sealed class NKMPacket_LEAGUE_PVP_MATCH_CANCEL_REQ : ISerializable
	{
		// Token: 0x0600963B RID: 38459 RVA: 0x0032B638 File Offset: 0x00329838
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
