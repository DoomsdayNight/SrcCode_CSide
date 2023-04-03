using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x0200108C RID: 4236
	[PacketId(ClientPacketId.kNKMPacket_BSIDE_ACCOUNT_LINK_ACK)]
	public sealed class NKMPacket_BSIDE_ACCOUNT_LINK_ACK : ISerializable
	{
		// Token: 0x06009BD5 RID: 39893 RVA: 0x00333EB3 File Offset: 0x003320B3
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMAccountLinkUserProfile>(ref this.targetUserProfile);
		}

		// Token: 0x0400901D RID: 36893
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400901E RID: 36894
		public NKMAccountLinkUserProfile targetUserProfile = new NKMAccountLinkUserProfile();
	}
}
