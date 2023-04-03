using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F60 RID: 3936
	[PacketId(ClientPacketId.kNKMPacket_FIERCE_COMPLETE_POINT_REWARD_ACK)]
	public sealed class NKMPacket_FIERCE_COMPLETE_POINT_REWARD_ACK : ISerializable
	{
		// Token: 0x060099A0 RID: 39328 RVA: 0x00330422 File Offset: 0x0032E622
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet(ref this.pointRewardId);
		}

		// Token: 0x04008CA6 RID: 36006
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008CA7 RID: 36007
		public NKMRewardData rewardData;

		// Token: 0x04008CA8 RID: 36008
		public int pointRewardId;
	}
}
