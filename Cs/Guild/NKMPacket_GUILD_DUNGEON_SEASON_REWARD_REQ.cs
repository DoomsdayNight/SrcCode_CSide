using System;
using ClientPacket.Common;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F1A RID: 3866
	[PacketId(ClientPacketId.kNKMPacket_GUILD_DUNGEON_SEASON_REWARD_REQ)]
	public sealed class NKMPacket_GUILD_DUNGEON_SEASON_REWARD_REQ : ISerializable
	{
		// Token: 0x06009914 RID: 39188 RVA: 0x0032F75E File Offset: 0x0032D95E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<GuildDungeonRewardCategory>(ref this.rewardCategory);
			stream.PutOrGet(ref this.rewardCountValue);
		}

		// Token: 0x04008BE9 RID: 35817
		public GuildDungeonRewardCategory rewardCategory;

		// Token: 0x04008BEA RID: 35818
		public int rewardCountValue;
	}
}
