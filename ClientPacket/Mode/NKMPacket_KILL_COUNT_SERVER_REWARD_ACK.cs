using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E54 RID: 3668
	[PacketId(ClientPacketId.kNKMPacket_KILL_COUNT_SERVER_REWARD_ACK)]
	public sealed class NKMPacket_KILL_COUNT_SERVER_REWARD_ACK : ISerializable
	{
		// Token: 0x06009798 RID: 38808 RVA: 0x0032D2A8 File Offset: 0x0032B4A8
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet<NKMKillCountData>(ref this.killCountData);
		}

		// Token: 0x040089C1 RID: 35265
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040089C2 RID: 35266
		public NKMRewardData rewardData;

		// Token: 0x040089C3 RID: 35267
		public List<NKMKillCountData> killCountData = new List<NKMKillCountData>();
	}
}
