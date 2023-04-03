using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F2C RID: 3884
	[PacketId(ClientPacketId.kNKMPacket_MATCH_GAME_LOAD_REQ)]
	public sealed class NKMPacket_MATCH_GAME_LOAD_REQ : ISerializable
	{
		// Token: 0x06009938 RID: 39224 RVA: 0x0032FA7E File Offset: 0x0032DC7E
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
