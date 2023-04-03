using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;

namespace ClientPacket.Common
{
	// Token: 0x02001040 RID: 4160
	public sealed class NKMAsyncDeckData : ISerializable
	{
		// Token: 0x06009B42 RID: 39746 RVA: 0x00332888 File Offset: 0x00330A88
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.leaderIndex);
			stream.PutOrGet<NKMAsyncUnitData>(ref this.ship);
			stream.PutOrGet<NKMAsyncUnitData>(ref this.units);
			stream.PutOrGet<NKMEquipItemData>(ref this.equips);
			stream.PutOrGet(ref this.operationPower);
			stream.PutOrGet<NKMOperator>(ref this.operatorUnit);
			stream.PutOrGet<NKMAsyncUnitData>(ref this.banishedUnit);
			stream.PutOrGet<NKMUnitUpData>(ref this.unitUpData);
			stream.PutOrGet<NKMBanData>(ref this.unitBanData);
		}

		// Token: 0x04008ED1 RID: 36561
		public short leaderIndex;

		// Token: 0x04008ED2 RID: 36562
		public NKMAsyncUnitData ship = new NKMAsyncUnitData();

		// Token: 0x04008ED3 RID: 36563
		public List<NKMAsyncUnitData> units = new List<NKMAsyncUnitData>();

		// Token: 0x04008ED4 RID: 36564
		public List<NKMEquipItemData> equips = new List<NKMEquipItemData>();

		// Token: 0x04008ED5 RID: 36565
		public int operationPower;

		// Token: 0x04008ED6 RID: 36566
		public NKMOperator operatorUnit;

		// Token: 0x04008ED7 RID: 36567
		public NKMAsyncUnitData banishedUnit = new NKMAsyncUnitData();

		// Token: 0x04008ED8 RID: 36568
		public Dictionary<int, NKMUnitUpData> unitUpData = new Dictionary<int, NKMUnitUpData>();

		// Token: 0x04008ED9 RID: 36569
		public Dictionary<int, NKMBanData> unitBanData = new Dictionary<int, NKMBanData>();
	}
}
