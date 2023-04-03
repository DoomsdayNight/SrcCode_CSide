using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CE7 RID: 3303
	[PacketId(ClientPacketId.kNKMPacket_REMOVE_UNIT_ACK)]
	public sealed class NKMPacket_REMOVE_UNIT_ACK : ISerializable
	{
		// Token: 0x060094CB RID: 38091 RVA: 0x0032936F File Offset: 0x0032756F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.removeUnitUIDList);
			stream.PutOrGet<NKMItemMiscData>(ref this.rewardItemDataList);
		}

		// Token: 0x0400864B RID: 34379
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400864C RID: 34380
		public List<long> removeUnitUIDList = new List<long>();

		// Token: 0x0400864D RID: 34381
		public List<NKMItemMiscData> rewardItemDataList = new List<NKMItemMiscData>();
	}
}
