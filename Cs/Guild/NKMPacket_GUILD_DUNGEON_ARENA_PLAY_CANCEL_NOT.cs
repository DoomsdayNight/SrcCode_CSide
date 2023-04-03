using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F25 RID: 3877
	[PacketId(ClientPacketId.kNKMPacket_GUILD_DUNGEON_ARENA_PLAY_CANCEL_NOT)]
	public sealed class NKMPacket_GUILD_DUNGEON_ARENA_PLAY_CANCEL_NOT : ISerializable
	{
		// Token: 0x0600992A RID: 39210 RVA: 0x0032F949 File Offset: 0x0032DB49
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.arenaIndex);
		}

		// Token: 0x04008C06 RID: 35846
		public int arenaIndex;
	}
}
