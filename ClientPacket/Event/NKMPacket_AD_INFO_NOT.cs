using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000FAA RID: 4010
	[PacketId(ClientPacketId.kNKMPacket_AD_INFO_NOT)]
	public sealed class NKMPacket_AD_INFO_NOT : ISerializable
	{
		// Token: 0x06009A28 RID: 39464 RVA: 0x00330FFB File Offset: 0x0032F1FB
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMADItemRewardInfo>(ref this.itemRewardInfos);
			stream.PutOrGetEnum<NKM_INVENTORY_EXPAND_TYPE>(ref this.inventoryExpandRewardInfos);
		}

		// Token: 0x04008D63 RID: 36195
		public List<NKMADItemRewardInfo> itemRewardInfos = new List<NKMADItemRewardInfo>();

		// Token: 0x04008D64 RID: 36196
		public List<NKM_INVENTORY_EXPAND_TYPE> inventoryExpandRewardInfos = new List<NKM_INVENTORY_EXPAND_TYPE>();
	}
}
