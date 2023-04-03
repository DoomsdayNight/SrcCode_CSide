using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F1F RID: 3871
	[PacketId(ClientPacketId.kNKMPacket_GUILD_DUNGEON_BOSS_PLAY_NOT)]
	public sealed class NKMPacket_GUILD_DUNGEON_BOSS_PLAY_NOT : ISerializable
	{
		// Token: 0x0600991E RID: 39198 RVA: 0x0032F859 File Offset: 0x0032DA59
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.userUid);
		}

		// Token: 0x04008BF7 RID: 35831
		public long userUid;
	}
}
