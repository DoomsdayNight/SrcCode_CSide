using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D05 RID: 3333
	[PacketId(ClientPacketId.kNKMPacket_EXTRACT_UNIT_ACK)]
	public sealed class NKMPacket_EXTRACT_UNIT_ACK : ISerializable
	{
		// Token: 0x06009507 RID: 38151 RVA: 0x003298F1 File Offset: 0x00327AF1
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.extractUnitUidList);
			stream.PutOrGet<NKMRewardData>(ref this.rewardItems);
			stream.PutOrGet<NKMRewardData>(ref this.synergyItems);
		}

		// Token: 0x04008697 RID: 34455
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008698 RID: 34456
		public List<long> extractUnitUidList = new List<long>();

		// Token: 0x04008699 RID: 34457
		public NKMRewardData rewardItems;

		// Token: 0x0400869A RID: 34458
		public NKMRewardData synergyItems;
	}
}
