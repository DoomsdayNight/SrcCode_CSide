using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x0200109C RID: 4252
	[PacketId(ClientPacketId.kNKMPacket_SERVICE_TRANSFER_USER_VALIDATION_ACK)]
	public sealed class NKMPacket_SERVICE_TRANSFER_USER_VALIDATION_ACK : ISerializable
	{
		// Token: 0x06009BF5 RID: 39925 RVA: 0x00334056 File Offset: 0x00332256
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x0400902E RID: 36910
		public NKM_ERROR_CODE errorCode;
	}
}
