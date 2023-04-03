using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E19 RID: 3609
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_PARTY_ACK)]
	public sealed class NKMPacket_OFFICE_PARTY_ACK : ISerializable
	{
		// Token: 0x06009726 RID: 38694 RVA: 0x0032C937 File Offset: 0x0032AB37
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.roomId);
			stream.PutOrGet<NKMUnitData>(ref this.units);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItems);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
		}

		// Token: 0x0400893A RID: 35130
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400893B RID: 35131
		public int roomId;

		// Token: 0x0400893C RID: 35132
		public List<NKMUnitData> units = new List<NKMUnitData>();

		// Token: 0x0400893D RID: 35133
		public List<NKMItemMiscData> costItems = new List<NKMItemMiscData>();

		// Token: 0x0400893E RID: 35134
		public NKMRewardData rewardData;
	}
}
