using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DBC RID: 3516
	[PacketId(ClientPacketId.kNKMPacket_LEAGUE_PVP_GIVEUP_REQ)]
	public sealed class NKMPacket_LEAGUE_PVP_GIVEUP_REQ : ISerializable
	{
		// Token: 0x06009671 RID: 38513 RVA: 0x0032B8E6 File Offset: 0x00329AE6
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
