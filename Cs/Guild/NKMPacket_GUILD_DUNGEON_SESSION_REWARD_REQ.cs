using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F1C RID: 3868
	[PacketId(ClientPacketId.kNKMPacket_GUILD_DUNGEON_SESSION_REWARD_REQ)]
	public sealed class NKMPacket_GUILD_DUNGEON_SESSION_REWARD_REQ : ISerializable
	{
		// Token: 0x06009918 RID: 39192 RVA: 0x0032F7BA File Offset: 0x0032D9BA
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
