using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F3B RID: 3899
	[PacketId(ClientPacketId.kNKMPacket_GAME_CHECK_DIE_UNIT_REQ)]
	public sealed class NKMPacket_GAME_CHECK_DIE_UNIT_REQ : ISerializable
	{
		// Token: 0x06009956 RID: 39254 RVA: 0x0032FEC1 File Offset: 0x0032E0C1
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
