using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F6B RID: 3947
	[PacketId(ClientPacketId.kNKMPacket_TRIM_DUNGEON_SKIP_ACK)]
	public sealed class NKMPacket_TRIM_DUNGEON_SKIP_ACK : ISerializable
	{
		// Token: 0x060099B6 RID: 39350 RVA: 0x0033063E File Offset: 0x0032E83E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMTrimClearData>(ref this.trimClearData);
			stream.PutOrGet<NKMRewardData>(ref this.rewardDatas);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItems);
			stream.PutOrGet<UnitLoyaltyUpdateData>(ref this.updatedUnits);
		}

		// Token: 0x04008CC1 RID: 36033
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008CC2 RID: 36034
		public NKMTrimClearData trimClearData = new NKMTrimClearData();

		// Token: 0x04008CC3 RID: 36035
		public List<NKMRewardData> rewardDatas = new List<NKMRewardData>();

		// Token: 0x04008CC4 RID: 36036
		public List<NKMItemMiscData> costItems = new List<NKMItemMiscData>();

		// Token: 0x04008CC5 RID: 36037
		public List<UnitLoyaltyUpdateData> updatedUnits = new List<UnitLoyaltyUpdateData>();
	}
}
