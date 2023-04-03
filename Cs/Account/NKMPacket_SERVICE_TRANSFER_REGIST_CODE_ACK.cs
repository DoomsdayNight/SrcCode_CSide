using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001098 RID: 4248
	[PacketId(ClientPacketId.kNKMPacket_SERVICE_TRANSFER_REGIST_CODE_ACK)]
	public sealed class NKMPacket_SERVICE_TRANSFER_REGIST_CODE_ACK : ISerializable
	{
		// Token: 0x06009BED RID: 39917 RVA: 0x00333FFE File Offset: 0x003321FE
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.code);
			stream.PutOrGet(ref this.canReceiveReward);
		}

		// Token: 0x0400902A RID: 36906
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400902B RID: 36907
		public string code;

		// Token: 0x0400902C RID: 36908
		public bool canReceiveReward;
	}
}
