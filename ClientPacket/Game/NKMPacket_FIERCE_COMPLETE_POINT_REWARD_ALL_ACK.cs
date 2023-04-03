using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F62 RID: 3938
	[PacketId(ClientPacketId.kNKMPacket_FIERCE_COMPLETE_POINT_REWARD_ALL_ACK)]
	public sealed class NKMPacket_FIERCE_COMPLETE_POINT_REWARD_ALL_ACK : ISerializable
	{
		// Token: 0x060099A4 RID: 39332 RVA: 0x0033045A File Offset: 0x0032E65A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.pointRewardIds);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
		}

		// Token: 0x04008CA9 RID: 36009
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008CAA RID: 36010
		public List<int> pointRewardIds = new List<int>();

		// Token: 0x04008CAB RID: 36011
		public NKMRewardData rewardData;
	}
}
