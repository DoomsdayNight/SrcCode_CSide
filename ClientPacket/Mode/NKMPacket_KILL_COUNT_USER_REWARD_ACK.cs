using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E52 RID: 3666
	[PacketId(ClientPacketId.kNKMPacket_KILL_COUNT_USER_REWARD_ACK)]
	public sealed class NKMPacket_KILL_COUNT_USER_REWARD_ACK : ISerializable
	{
		// Token: 0x06009794 RID: 38804 RVA: 0x0032D24D File Offset: 0x0032B44D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet<NKMKillCountData>(ref this.killCountData);
		}

		// Token: 0x040089BC RID: 35260
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040089BD RID: 35261
		public NKMRewardData rewardData;

		// Token: 0x040089BE RID: 35262
		public List<NKMKillCountData> killCountData = new List<NKMKillCountData>();
	}
}
