using System;
using Cs.Protocol;

namespace ClientPacket.Common
{
	// Token: 0x0200104F RID: 4175
	public sealed class GuildDungeonSeasonRewardData : ISerializable
	{
		// Token: 0x06009B5E RID: 39774 RVA: 0x00332D8C File Offset: 0x00330F8C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<GuildDungeonRewardCategory>(ref this.category);
			stream.PutOrGet(ref this.totalValue);
			stream.PutOrGet(ref this.receivedValue);
		}

		// Token: 0x04008F1F RID: 36639
		public GuildDungeonRewardCategory category;

		// Token: 0x04008F20 RID: 36640
		public int totalValue;

		// Token: 0x04008F21 RID: 36641
		public int receivedValue;
	}
}
