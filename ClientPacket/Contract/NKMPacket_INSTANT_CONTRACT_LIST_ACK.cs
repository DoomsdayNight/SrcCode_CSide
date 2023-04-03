using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Contract
{
	// Token: 0x02000FC7 RID: 4039
	[PacketId(ClientPacketId.kNKMPacket_INSTANT_CONTRACT_LIST_ACK)]
	public sealed class NKMPacket_INSTANT_CONTRACT_LIST_ACK : ISerializable
	{
		// Token: 0x06009A5E RID: 39518 RVA: 0x00331545 File Offset: 0x0032F745
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMInstantContract>(ref this.InstantContract);
		}

		// Token: 0x04008DAF RID: 36271
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008DB0 RID: 36272
		public List<NKMInstantContract> InstantContract = new List<NKMInstantContract>();
	}
}
