using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C78 RID: 3192
	[PacketId(ClientPacketId.kNKMPacket_WORLDMAP_SET_CITY_ACK)]
	public sealed class NKMPacket_WORLDMAP_SET_CITY_ACK : ISerializable
	{
		// Token: 0x060093EF RID: 37871 RVA: 0x00327CCC File Offset: 0x00325ECC
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMWorldMapCityData>(ref this.worldMapCityData);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemData);
		}

		// Token: 0x040084F8 RID: 34040
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040084F9 RID: 34041
		public NKMWorldMapCityData worldMapCityData = new NKMWorldMapCityData();

		// Token: 0x040084FA RID: 34042
		public NKMItemMiscData costItemData;
	}
}
