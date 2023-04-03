using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C88 RID: 3208
	[PacketId(ClientPacketId.kNKMPacket_WORLDMAP_BUILD_ACK)]
	public sealed class NKMPacket_WORLDMAP_BUILD_ACK : ISerializable
	{
		// Token: 0x0600940F RID: 37903 RVA: 0x00327F90 File Offset: 0x00326190
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.cityID);
			stream.PutOrGet(ref this.buildID);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
		}

		// Token: 0x04008520 RID: 34080
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008521 RID: 34081
		public int cityID;

		// Token: 0x04008522 RID: 34082
		public int buildID;

		// Token: 0x04008523 RID: 34083
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();
	}
}
