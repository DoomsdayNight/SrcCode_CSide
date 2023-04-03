using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F67 RID: 3943
	[PacketId(ClientPacketId.kNKMPacket_DUNGEON_SKIP_ACK)]
	public sealed class NKMPacket_DUNGEON_SKIP_ACK : ISerializable
	{
		// Token: 0x060099AE RID: 39342 RVA: 0x00330521 File Offset: 0x0032E721
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMStagePlayData>(ref this.stagePlayData);
			stream.PutOrGet<NKMDungeonRewardSet>(ref this.rewardDatas);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItems);
			stream.PutOrGet<UnitLoyaltyUpdateData>(ref this.updatedUnits);
		}

		// Token: 0x04008CB3 RID: 36019
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008CB4 RID: 36020
		public NKMStagePlayData stagePlayData = new NKMStagePlayData();

		// Token: 0x04008CB5 RID: 36021
		public List<NKMDungeonRewardSet> rewardDatas = new List<NKMDungeonRewardSet>();

		// Token: 0x04008CB6 RID: 36022
		public List<NKMItemMiscData> costItems = new List<NKMItemMiscData>();

		// Token: 0x04008CB7 RID: 36023
		public List<UnitLoyaltyUpdateData> updatedUnits = new List<UnitLoyaltyUpdateData>();
	}
}
