using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C8C RID: 3212
	[PacketId(ClientPacketId.kNKMPacket_WORLDMAP_BUILD_EXPIRE_ACK)]
	public sealed class NKMPacket_WORLDMAP_BUILD_EXPIRE_ACK : ISerializable
	{
		// Token: 0x06009417 RID: 37911 RVA: 0x00328069 File Offset: 0x00326269
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.cityID);
			stream.PutOrGet(ref this.buildID);
			stream.PutOrGet<NKMItemMiscData>(ref this.itemData);
		}

		// Token: 0x0400852C RID: 34092
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400852D RID: 34093
		public int cityID;

		// Token: 0x0400852E RID: 34094
		public int buildID;

		// Token: 0x0400852F RID: 34095
		public NKMItemMiscData itemData;
	}
}
