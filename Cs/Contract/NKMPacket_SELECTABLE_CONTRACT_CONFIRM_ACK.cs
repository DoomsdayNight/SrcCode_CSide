using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Contract
{
	// Token: 0x02000FC0 RID: 4032
	[PacketId(ClientPacketId.kNKMPacket_SELECTABLE_CONTRACT_CONFIRM_ACK)]
	public sealed class NKMPacket_SELECTABLE_CONTRACT_CONFIRM_ACK : ISerializable
	{
		// Token: 0x06009A50 RID: 39504 RVA: 0x00331415 File Offset: 0x0032F615
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.contractId);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItems);
			stream.PutOrGet<NKMUnitData>(ref this.units);
			stream.PutOrGet<NKMSelectableContractState>(ref this.selectableContractState);
		}

		// Token: 0x04008DA1 RID: 36257
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008DA2 RID: 36258
		public int contractId;

		// Token: 0x04008DA3 RID: 36259
		public List<NKMItemMiscData> costItems = new List<NKMItemMiscData>();

		// Token: 0x04008DA4 RID: 36260
		public List<NKMUnitData> units = new List<NKMUnitData>();

		// Token: 0x04008DA5 RID: 36261
		public NKMSelectableContractState selectableContractState = new NKMSelectableContractState();
	}
}
