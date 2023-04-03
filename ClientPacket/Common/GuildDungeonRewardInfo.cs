using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace ClientPacket.Common
{
	// Token: 0x02001050 RID: 4176
	public sealed class GuildDungeonRewardInfo : ISerializable
	{
		// Token: 0x06009B60 RID: 39776 RVA: 0x00332DBA File Offset: 0x00330FBA
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.currentSeasonId);
			stream.PutOrGet<GuildDungeonSeasonRewardData>(ref this.lastSeasonRewardData);
			stream.PutOrGet(ref this.canReward);
		}

		// Token: 0x04008F22 RID: 36642
		public int currentSeasonId;

		// Token: 0x04008F23 RID: 36643
		public List<GuildDungeonSeasonRewardData> lastSeasonRewardData = new List<GuildDungeonSeasonRewardData>();

		// Token: 0x04008F24 RID: 36644
		public bool canReward;
	}
}
