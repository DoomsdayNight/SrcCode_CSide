using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E67 RID: 3687
	[PacketId(ClientPacketId.kNKMPacket_DIVE_SKIP_ACK)]
	public sealed class NKMPacket_DIVE_SKIP_ACK : ISerializable
	{
		// Token: 0x060097BE RID: 38846 RVA: 0x0032D5F7 File Offset: 0x0032B7F7
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardDatas);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItems);
		}

		// Token: 0x040089EA RID: 35306
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040089EB RID: 35307
		public List<NKMRewardData> rewardDatas = new List<NKMRewardData>();

		// Token: 0x040089EC RID: 35308
		public List<NKMItemMiscData> costItems = new List<NKMItemMiscData>();
	}
}
