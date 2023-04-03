using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001022 RID: 4130
	[PacketId(ClientPacketId.kNKMPacket_MENTORING_INVITE_REWARD_LIST_ACK)]
	public sealed class NKMPacket_MENTORING_INVITE_REWARD_LIST_ACK : ISerializable
	{
		// Token: 0x06009B14 RID: 39700 RVA: 0x0033234C File Offset: 0x0033054C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.rewardHistories);
		}

		// Token: 0x04008E6C RID: 36460
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E6D RID: 36461
		public HashSet<int> rewardHistories = new HashSet<int>();
	}
}
