using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Contract
{
	// Token: 0x02000FBC RID: 4028
	[PacketId(ClientPacketId.kNKMPacket_CONTRACT_ACK)]
	public sealed class NKMPacket_CONTRACT_ACK : ISerializable
	{
		// Token: 0x06009A48 RID: 39496 RVA: 0x003312F8 File Offset: 0x0032F4F8
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGetEnum<ContractCostType>(ref this.costType);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItems);
			stream.PutOrGet<NKMUnitData>(ref this.units);
			stream.PutOrGet<NKMOperator>(ref this.operators);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet<NKMContractState>(ref this.contractState);
			stream.PutOrGet<NKMContractBonusState>(ref this.contractBonusState);
			stream.PutOrGet(ref this.contractId);
			stream.PutOrGet(ref this.requestCount);
		}

		// Token: 0x04008D93 RID: 36243
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008D94 RID: 36244
		public ContractCostType costType;

		// Token: 0x04008D95 RID: 36245
		public List<NKMItemMiscData> costItems = new List<NKMItemMiscData>();

		// Token: 0x04008D96 RID: 36246
		public List<NKMUnitData> units = new List<NKMUnitData>();

		// Token: 0x04008D97 RID: 36247
		public List<NKMOperator> operators = new List<NKMOperator>();

		// Token: 0x04008D98 RID: 36248
		public NKMRewardData rewardData;

		// Token: 0x04008D99 RID: 36249
		public NKMContractState contractState = new NKMContractState();

		// Token: 0x04008D9A RID: 36250
		public NKMContractBonusState contractBonusState = new NKMContractBonusState();

		// Token: 0x04008D9B RID: 36251
		public int contractId;

		// Token: 0x04008D9C RID: 36252
		public int requestCount;
	}
}
