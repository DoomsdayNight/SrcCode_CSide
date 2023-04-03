using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001090 RID: 4240
	[PacketId(ClientPacketId.kNKMPacket_BSIDE_ACCOUNT_LINK_CODE_NOT)]
	public sealed class NKMPacket_BSIDE_ACCOUNT_LINK_CODE_NOT : ISerializable
	{
		// Token: 0x06009BDD RID: 39901 RVA: 0x00333F45 File Offset: 0x00332145
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMAccountLinkUserProfile>(ref this.requestUserProfile);
			stream.PutOrGet<NKMAccountLinkUserProfile>(ref this.targetUserProfile);
		}

		// Token: 0x04009024 RID: 36900
		public NKMAccountLinkUserProfile requestUserProfile = new NKMAccountLinkUserProfile();

		// Token: 0x04009025 RID: 36901
		public NKMAccountLinkUserProfile targetUserProfile = new NKMAccountLinkUserProfile();
	}
}
