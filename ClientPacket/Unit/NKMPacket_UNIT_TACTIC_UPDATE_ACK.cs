using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D1D RID: 3357
	[PacketId(ClientPacketId.kNKMPacket_UNIT_TACTIC_UPDATE_ACK)]
	public sealed class NKMPacket_UNIT_TACTIC_UPDATE_ACK : ISerializable
	{
		// Token: 0x06009537 RID: 38199 RVA: 0x00329CFC File Offset: 0x00327EFC
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMUnitData>(ref this.unitData);
			stream.PutOrGet(ref this.consumeUnitUid);
			stream.PutOrGet<NKMRewardData>(ref this.rewardItems);
			stream.PutOrGet(ref this.unitTacticReturnCount);
		}

		// Token: 0x040086D1 RID: 34513
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040086D2 RID: 34514
		public NKMUnitData unitData;

		// Token: 0x040086D3 RID: 34515
		public long consumeUnitUid;

		// Token: 0x040086D4 RID: 34516
		public NKMRewardData rewardItems;

		// Token: 0x040086D5 RID: 34517
		public int unitTacticReturnCount;
	}
}
