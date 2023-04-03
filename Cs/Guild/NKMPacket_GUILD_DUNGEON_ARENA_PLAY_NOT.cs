using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F1E RID: 3870
	[PacketId(ClientPacketId.kNKMPacket_GUILD_DUNGEON_ARENA_PLAY_NOT)]
	public sealed class NKMPacket_GUILD_DUNGEON_ARENA_PLAY_NOT : ISerializable
	{
		// Token: 0x0600991C RID: 39196 RVA: 0x0032F837 File Offset: 0x0032DA37
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.arenaId);
			stream.PutOrGet(ref this.userUid);
		}

		// Token: 0x04008BF5 RID: 35829
		public int arenaId;

		// Token: 0x04008BF6 RID: 35830
		public long userUid;
	}
}
