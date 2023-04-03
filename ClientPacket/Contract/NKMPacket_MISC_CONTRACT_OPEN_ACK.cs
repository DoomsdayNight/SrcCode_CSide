using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Contract
{
	// Token: 0x02000FC4 RID: 4036
	[PacketId(ClientPacketId.kNKMPacket_MISC_CONTRACT_OPEN_ACK)]
	public sealed class NKMPacket_MISC_CONTRACT_OPEN_ACK : ISerializable
	{
		// Token: 0x06009A58 RID: 39512 RVA: 0x003314D5 File Offset: 0x0032F6D5
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItems);
			stream.PutOrGet<MiscContractResult>(ref this.result);
		}

		// Token: 0x04008DAA RID: 36266
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008DAB RID: 36267
		public List<NKMItemMiscData> costItems = new List<NKMItemMiscData>();

		// Token: 0x04008DAC RID: 36268
		public List<MiscContractResult> result = new List<MiscContractResult>();
	}
}
