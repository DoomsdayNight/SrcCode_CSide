using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E35 RID: 3637
	[PacketId(ClientPacketId.kNKMPacket_DIVE_START_ACK)]
	public sealed class NKMPacket_DIVE_START_ACK : ISerializable
	{
		// Token: 0x0600975A RID: 38746 RVA: 0x0032CDC7 File Offset: 0x0032AFC7
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.cityID);
			stream.PutOrGet<NKMDiveGameData>(ref this.diveGameData);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
		}

		// Token: 0x0400897E RID: 35198
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400897F RID: 35199
		public int cityID;

		// Token: 0x04008980 RID: 35200
		public NKMDiveGameData diveGameData;

		// Token: 0x04008981 RID: 35201
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();
	}
}
