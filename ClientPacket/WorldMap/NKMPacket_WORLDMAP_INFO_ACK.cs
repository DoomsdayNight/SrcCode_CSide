using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C76 RID: 3190
	[PacketId(ClientPacketId.kNKMPacket_WORLDMAP_INFO_ACK)]
	public sealed class NKMPacket_WORLDMAP_INFO_ACK : ISerializable
	{
		// Token: 0x060093EB RID: 37867 RVA: 0x00327C7D File Offset: 0x00325E7D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMWorldMapData>(ref this.worldMapData);
		}

		// Token: 0x040084F4 RID: 34036
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040084F5 RID: 34037
		public NKMWorldMapData worldMapData = new NKMWorldMapData();
	}
}
