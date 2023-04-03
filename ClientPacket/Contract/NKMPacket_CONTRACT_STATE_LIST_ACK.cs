using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Contract
{
	// Token: 0x02000FC2 RID: 4034
	[PacketId(ClientPacketId.kNKMPacket_CONTRACT_STATE_LIST_ACK)]
	public sealed class NKMPacket_CONTRACT_STATE_LIST_ACK : ISerializable
	{
		// Token: 0x06009A54 RID: 39508 RVA: 0x00331486 File Offset: 0x0032F686
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMContractState>(ref this.contractState);
		}

		// Token: 0x04008DA6 RID: 36262
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008DA7 RID: 36263
		public List<NKMContractState> contractState = new List<NKMContractState>();
	}
}
