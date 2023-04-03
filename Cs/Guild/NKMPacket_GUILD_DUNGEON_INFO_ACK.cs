using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F17 RID: 3863
	[PacketId(ClientPacketId.kNKMPacket_GUILD_DUNGEON_INFO_ACK)]
	public sealed class NKMPacket_GUILD_DUNGEON_INFO_ACK : ISerializable
	{
		// Token: 0x0600990E RID: 39182 RVA: 0x0032F648 File Offset: 0x0032D848
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGetEnum<GuildDungeonState>(ref this.guildDungeonState);
			stream.PutOrGet(ref this.seasonId);
			stream.PutOrGet(ref this.sessionId);
			stream.PutOrGet(ref this.currentSessionEndDate);
			stream.PutOrGet(ref this.NextSessionStartDate);
			stream.PutOrGet(ref this.bossStageId);
			stream.PutOrGet(ref this.bossPlayCount);
			stream.PutOrGet(ref this.bossRemainHp);
			stream.PutOrGet(ref this.bossPlayUserUid);
			stream.PutOrGet(ref this.arenaTicketBuyCount);
			stream.PutOrGet<GuildDungeonArena>(ref this.arenaList);
			stream.PutOrGet<GuildDungeonSeasonRewardData>(ref this.lastSeasonRewardData);
			stream.PutOrGet(ref this.canReward);
		}

		// Token: 0x04008BD8 RID: 35800
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008BD9 RID: 35801
		public GuildDungeonState guildDungeonState;

		// Token: 0x04008BDA RID: 35802
		public int seasonId;

		// Token: 0x04008BDB RID: 35803
		public int sessionId;

		// Token: 0x04008BDC RID: 35804
		public DateTime currentSessionEndDate;

		// Token: 0x04008BDD RID: 35805
		public DateTime NextSessionStartDate;

		// Token: 0x04008BDE RID: 35806
		public int bossStageId;

		// Token: 0x04008BDF RID: 35807
		public int bossPlayCount;

		// Token: 0x04008BE0 RID: 35808
		public float bossRemainHp;

		// Token: 0x04008BE1 RID: 35809
		public long bossPlayUserUid;

		// Token: 0x04008BE2 RID: 35810
		public int arenaTicketBuyCount;

		// Token: 0x04008BE3 RID: 35811
		public List<GuildDungeonArena> arenaList = new List<GuildDungeonArena>();

		// Token: 0x04008BE4 RID: 35812
		public List<GuildDungeonSeasonRewardData> lastSeasonRewardData = new List<GuildDungeonSeasonRewardData>();

		// Token: 0x04008BE5 RID: 35813
		public bool canReward;
	}
}
