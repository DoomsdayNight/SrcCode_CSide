using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DBA RID: 3514
	[PacketId(ClientPacketId.kNKMPacket_LEAGUE_PVP_WEEKLY_RANKER_REQ)]
	public sealed class NKMPacket_LEAGUE_PVP_WEEKLY_RANKER_REQ : ISerializable
	{
		// Token: 0x0600966D RID: 38509 RVA: 0x0032B8AF File Offset: 0x00329AAF
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
