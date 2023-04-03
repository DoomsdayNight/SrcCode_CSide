using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F1D RID: 3869
	[PacketId(ClientPacketId.kNKMPacket_GUILD_DUNGEON_SESSION_REWARD_ACK)]
	public sealed class NKMPacket_GUILD_DUNGEON_SESSION_REWARD_ACK : ISerializable
	{
		// Token: 0x0600991A RID: 39194 RVA: 0x0032F7C4 File Offset: 0x0032D9C4
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.stageIndex);
			stream.PutOrGet(ref this.remainHp);
			stream.PutOrGet(ref this.clearPoint);
			stream.PutOrGet<NKMItemMiscData>(ref this.rewardList);
			stream.PutOrGet<NKMItemMiscData>(ref this.artifactReward);
		}

		// Token: 0x04008BEF RID: 35823
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008BF0 RID: 35824
		public int stageIndex;

		// Token: 0x04008BF1 RID: 35825
		public long remainHp;

		// Token: 0x04008BF2 RID: 35826
		public int clearPoint;

		// Token: 0x04008BF3 RID: 35827
		public List<NKMItemMiscData> rewardList = new List<NKMItemMiscData>();

		// Token: 0x04008BF4 RID: 35828
		public List<NKMItemMiscData> artifactReward = new List<NKMItemMiscData>();
	}
}
