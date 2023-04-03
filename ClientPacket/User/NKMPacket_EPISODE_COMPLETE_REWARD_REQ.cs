using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CC8 RID: 3272
	[PacketId(ClientPacketId.kNKMPacket_EPISODE_COMPLETE_REWARD_REQ)]
	public sealed class NKMPacket_EPISODE_COMPLETE_REWARD_REQ : ISerializable
	{
		// Token: 0x0600948D RID: 38029 RVA: 0x00328E31 File Offset: 0x00327031
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.episodeID);
			stream.PutOrGet(ref this.episodeDifficulty);
			stream.PutOrGet(ref this.rewardIndex);
		}

		// Token: 0x04008602 RID: 34306
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008603 RID: 34307
		public int episodeID;

		// Token: 0x04008604 RID: 34308
		public int episodeDifficulty;

		// Token: 0x04008605 RID: 34309
		public sbyte rewardIndex;
	}
}
