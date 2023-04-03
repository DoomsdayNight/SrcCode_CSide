using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x0200109E RID: 4254
	[PacketId(ClientPacketId.kNKMPacket_SERVICE_TRANSFER_CODE_VALIDATION_ACK)]
	public sealed class NKMPacket_SERVICE_TRANSFER_CODE_VALIDATION_ACK : ISerializable
	{
		// Token: 0x06009BF9 RID: 39929 RVA: 0x00334082 File Offset: 0x00332282
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMAccountLinkUserProfile>(ref this.userProfile);
			stream.PutOrGet(ref this.failCount);
		}

		// Token: 0x04009030 RID: 36912
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04009031 RID: 36913
		public NKMAccountLinkUserProfile userProfile = new NKMAccountLinkUserProfile();

		// Token: 0x04009032 RID: 36914
		public int failCount;
	}
}
