using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F44 RID: 3908
	[PacketId(ClientPacketId.kNKMPacket_GAME_GIVEUP_REQ)]
	public sealed class NKMPacket_GAME_GIVEUP_REQ : ISerializable
	{
		// Token: 0x06009968 RID: 39272 RVA: 0x0033001E File Offset: 0x0032E21E
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
