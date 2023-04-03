using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F21 RID: 3873
	[PacketId(ClientPacketId.kNKMPacket_GUILD_DUNGEON_BOSS_PLAY_END_NOT)]
	public sealed class NKMPacket_GUILD_DUNGEON_BOSS_PLAY_END_NOT : ISerializable
	{
		// Token: 0x06009922 RID: 39202 RVA: 0x0032F8A9 File Offset: 0x0032DAA9
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.playedUserUid);
			stream.PutOrGet(ref this.bossStageId);
			stream.PutOrGet(ref this.damage);
			stream.PutOrGet(ref this.remainHp);
			stream.PutOrGet(ref this.point);
		}

		// Token: 0x04008BFC RID: 35836
		public long playedUserUid;

		// Token: 0x04008BFD RID: 35837
		public int bossStageId;

		// Token: 0x04008BFE RID: 35838
		public float damage;

		// Token: 0x04008BFF RID: 35839
		public float remainHp;

		// Token: 0x04008C00 RID: 35840
		public int point;
	}
}
