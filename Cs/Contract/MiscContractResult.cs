using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;

namespace ClientPacket.Contract
{
	// Token: 0x02000FB8 RID: 4024
	public sealed class MiscContractResult : ISerializable
	{
		// Token: 0x06009A44 RID: 39492 RVA: 0x0033129B File Offset: 0x0032F49B
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.miscItemId);
			stream.PutOrGet<NKMUnitData>(ref this.units);
		}

		// Token: 0x04008D87 RID: 36231
		public int miscItemId;

		// Token: 0x04008D88 RID: 36232
		public List<NKMUnitData> units = new List<NKMUnitData>();
	}
}
