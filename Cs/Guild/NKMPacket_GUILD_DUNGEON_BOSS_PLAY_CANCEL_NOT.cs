using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F26 RID: 3878
	[PacketId(ClientPacketId.kNKMPacket_GUILD_DUNGEON_BOSS_PLAY_CANCEL_NOT)]
	public sealed class NKMPacket_GUILD_DUNGEON_BOSS_PLAY_CANCEL_NOT : ISerializable
	{
		// Token: 0x0600992C RID: 39212 RVA: 0x0032F95F File Offset: 0x0032DB5F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.playUserUid);
		}

		// Token: 0x04008C07 RID: 35847
		public long playUserUid;
	}
}
