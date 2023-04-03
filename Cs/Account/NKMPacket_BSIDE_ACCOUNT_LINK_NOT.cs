using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x0200108D RID: 4237
	[PacketId(ClientPacketId.kNKMPacket_BSIDE_ACCOUNT_LINK_NOT)]
	public sealed class NKMPacket_BSIDE_ACCOUNT_LINK_NOT : ISerializable
	{
		// Token: 0x06009BD7 RID: 39895 RVA: 0x00333EE0 File Offset: 0x003320E0
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.linkCode);
			stream.PutOrGet<NKMAccountLinkUserProfile>(ref this.requestUserProfile);
			stream.PutOrGet(ref this.remainingTime);
		}

		// Token: 0x0400901F RID: 36895
		public string linkCode;

		// Token: 0x04009020 RID: 36896
		public NKMAccountLinkUserProfile requestUserProfile = new NKMAccountLinkUserProfile();

		// Token: 0x04009021 RID: 36897
		public float remainingTime;
	}
}
