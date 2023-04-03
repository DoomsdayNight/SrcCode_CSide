using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F24 RID: 3876
	[PacketId(ClientPacketId.kNKMPacket_GUILD_DUNGEON_BOSS_GAME_LOAD_REQ)]
	public sealed class NKMPacket_GUILD_DUNGEON_BOSS_GAME_LOAD_REQ : ISerializable
	{
		// Token: 0x06009928 RID: 39208 RVA: 0x0032F927 File Offset: 0x0032DB27
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.selectDeckIndex);
			stream.PutOrGet(ref this.bossStageId);
		}

		// Token: 0x04008C04 RID: 35844
		public byte selectDeckIndex;

		// Token: 0x04008C05 RID: 35845
		public int bossStageId;
	}
}
