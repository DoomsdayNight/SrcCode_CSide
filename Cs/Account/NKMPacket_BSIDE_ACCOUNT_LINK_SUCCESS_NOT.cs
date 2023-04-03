using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001093 RID: 4243
	[PacketId(ClientPacketId.kNKMPacket_BSIDE_ACCOUNT_LINK_SUCCESS_NOT)]
	public sealed class NKMPacket_BSIDE_ACCOUNT_LINK_SUCCESS_NOT : ISerializable
	{
		// Token: 0x06009BE3 RID: 39907 RVA: 0x00333FA9 File Offset: 0x003321A9
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMAccountLinkUserProfile>(ref this.selectedUserProfile);
		}

		// Token: 0x04009028 RID: 36904
		public NKMAccountLinkUserProfile selectedUserProfile = new NKMAccountLinkUserProfile();
	}
}
