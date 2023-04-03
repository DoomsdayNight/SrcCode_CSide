using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Contract
{
	// Token: 0x02000FBB RID: 4027
	[PacketId(ClientPacketId.kNKMPacket_CONTRACT_REQ)]
	public sealed class NKMPacket_CONTRACT_REQ : ISerializable
	{
		// Token: 0x06009A46 RID: 39494 RVA: 0x003312C8 File Offset: 0x0032F4C8
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.contractId);
			stream.PutOrGet(ref this.count);
			stream.PutOrGetEnum<ContractCostType>(ref this.costType);
		}

		// Token: 0x04008D90 RID: 36240
		public int contractId;

		// Token: 0x04008D91 RID: 36241
		public int count;

		// Token: 0x04008D92 RID: 36242
		public ContractCostType costType;
	}
}
