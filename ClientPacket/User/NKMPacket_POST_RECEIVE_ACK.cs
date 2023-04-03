using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CBD RID: 3261
	[PacketId(ClientPacketId.kNKMPacket_POST_RECEIVE_ACK)]
	public sealed class NKMPacket_POST_RECEIVE_ACK : ISerializable
	{
		// Token: 0x06009477 RID: 38007 RVA: 0x00328C25 File Offset: 0x00326E25
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.postIndex);
			stream.PutOrGet<NKMRewardData>(ref this.rewardDate);
			stream.PutOrGet(ref this.postCount);
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x040085E5 RID: 34277
		public long postIndex;

		// Token: 0x040085E6 RID: 34278
		public NKMRewardData rewardDate;

		// Token: 0x040085E7 RID: 34279
		public int postCount;

		// Token: 0x040085E8 RID: 34280
		public NKM_ERROR_CODE errorCode;
	}
}
