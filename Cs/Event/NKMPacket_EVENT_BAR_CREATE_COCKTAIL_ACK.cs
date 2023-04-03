using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000FA6 RID: 4006
	[PacketId(ClientPacketId.kNKMPacket_EVENT_BAR_CREATE_COCKTAIL_ACK)]
	public sealed class NKMPacket_EVENT_BAR_CREATE_COCKTAIL_ACK : ISerializable
	{
		// Token: 0x06009A20 RID: 39456 RVA: 0x00330F2D File Offset: 0x0032F12D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItems);
		}

		// Token: 0x04008D57 RID: 36183
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008D58 RID: 36184
		public NKMRewardData rewardData;

		// Token: 0x04008D59 RID: 36185
		public List<NKMItemMiscData> costItems = new List<NKMItemMiscData>();
	}
}
