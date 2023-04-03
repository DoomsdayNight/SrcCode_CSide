using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Warfare
{
	// Token: 0x02000CA9 RID: 3241
	[PacketId(ClientPacketId.kNKMPacket_WARFARE_RECOVER_ACK)]
	public sealed class NKMPacket_WARFARE_RECOVER_ACK : ISerializable
	{
		// Token: 0x0600944F RID: 37967 RVA: 0x003287BF File Offset: 0x003269BF
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<WarfareSyncData>(ref this.warfareSyncData);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
		}

		// Token: 0x040085A0 RID: 34208
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040085A1 RID: 34209
		public WarfareSyncData warfareSyncData = new WarfareSyncData();

		// Token: 0x040085A2 RID: 34210
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();
	}
}
