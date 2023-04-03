using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E69 RID: 3689
	[PacketId(ClientPacketId.kNKMPacket_SHADOW_PALACE_SKIP_ACK)]
	public sealed class NKMPacket_SHADOW_PALACE_SKIP_ACK : ISerializable
	{
		// Token: 0x060097C2 RID: 38850 RVA: 0x0032D65D File Offset: 0x0032B85D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardDatas);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItems);
		}

		// Token: 0x040089EF RID: 35311
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040089F0 RID: 35312
		public List<NKMRewardData> rewardDatas = new List<NKMRewardData>();

		// Token: 0x040089F1 RID: 35313
		public List<NKMItemMiscData> costItems = new List<NKMItemMiscData>();
	}
}
