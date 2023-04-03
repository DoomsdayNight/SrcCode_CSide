using System;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F1B RID: 3867
	[PacketId(ClientPacketId.kNKMPacket_GUILD_DUNGEON_SEASON_REWARD_ACK)]
	public sealed class NKMPacket_GUILD_DUNGEON_SEASON_REWARD_ACK : ISerializable
	{
		// Token: 0x06009916 RID: 39190 RVA: 0x0032F780 File Offset: 0x0032D980
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGetEnum<GuildDungeonRewardCategory>(ref this.rewardCategory);
			stream.PutOrGet(ref this.rewardCountValue);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
		}

		// Token: 0x04008BEB RID: 35819
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008BEC RID: 35820
		public GuildDungeonRewardCategory rewardCategory;

		// Token: 0x04008BED RID: 35821
		public int rewardCountValue;

		// Token: 0x04008BEE RID: 35822
		public NKMRewardData rewardData;
	}
}
