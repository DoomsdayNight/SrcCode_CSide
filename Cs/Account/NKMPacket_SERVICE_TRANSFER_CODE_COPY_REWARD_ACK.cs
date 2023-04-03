using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x0200109A RID: 4250
	[PacketId(ClientPacketId.kNKMPacket_SERVICE_TRANSFER_CODE_COPY_REWARD_ACK)]
	public sealed class NKMPacket_SERVICE_TRANSFER_CODE_COPY_REWARD_ACK : ISerializable
	{
		// Token: 0x06009BF1 RID: 39921 RVA: 0x00334036 File Offset: 0x00332236
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x0400902D RID: 36909
		public NKM_ERROR_CODE errorCode;
	}
}
