using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;

namespace ClientPacket.Common
{
	// Token: 0x02001057 RID: 4183
	public sealed class NKMPvpBanResult : ISerializable
	{
		// Token: 0x06009B6E RID: 39790 RVA: 0x00332F8C File Offset: 0x0033118C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMBanData>(ref this.unitBanList);
			stream.PutOrGet<NKMBanShipData>(ref this.shipBanList);
			stream.PutOrGet<NKMBanOperatorData>(ref this.operatorBanList);
			stream.PutOrGet<NKMUnitUpData>(ref this.unitUpList);
			stream.PutOrGet<NKMBanData>(ref this.unitCastingBanList);
			stream.PutOrGet<NKMBanShipData>(ref this.shipCastingBanList);
			stream.PutOrGet<NKMBanOperatorData>(ref this.operatorCastingBanList);
			stream.PutOrGet<NKMBanData>(ref this.unitFinalBanList);
			stream.PutOrGet<NKMBanShipData>(ref this.shipFinalBanList);
			stream.PutOrGet<NKMBanOperatorData>(ref this.operatorFinalBanList);
		}

		// Token: 0x04008F40 RID: 36672
		public Dictionary<int, NKMBanData> unitBanList = new Dictionary<int, NKMBanData>();

		// Token: 0x04008F41 RID: 36673
		public Dictionary<int, NKMBanShipData> shipBanList = new Dictionary<int, NKMBanShipData>();

		// Token: 0x04008F42 RID: 36674
		public Dictionary<int, NKMBanOperatorData> operatorBanList = new Dictionary<int, NKMBanOperatorData>();

		// Token: 0x04008F43 RID: 36675
		public Dictionary<int, NKMUnitUpData> unitUpList = new Dictionary<int, NKMUnitUpData>();

		// Token: 0x04008F44 RID: 36676
		public Dictionary<int, NKMBanData> unitCastingBanList = new Dictionary<int, NKMBanData>();

		// Token: 0x04008F45 RID: 36677
		public Dictionary<int, NKMBanShipData> shipCastingBanList = new Dictionary<int, NKMBanShipData>();

		// Token: 0x04008F46 RID: 36678
		public Dictionary<int, NKMBanOperatorData> operatorCastingBanList = new Dictionary<int, NKMBanOperatorData>();

		// Token: 0x04008F47 RID: 36679
		public Dictionary<int, NKMBanData> unitFinalBanList = new Dictionary<int, NKMBanData>();

		// Token: 0x04008F48 RID: 36680
		public Dictionary<int, NKMBanShipData> shipFinalBanList = new Dictionary<int, NKMBanShipData>();

		// Token: 0x04008F49 RID: 36681
		public Dictionary<int, NKMBanOperatorData> operatorFinalBanList = new Dictionary<int, NKMBanOperatorData>();
	}
}
