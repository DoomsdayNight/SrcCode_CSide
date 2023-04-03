using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x0200108A RID: 4234
	[PacketId(ClientPacketId.kNKMPacket_STEAM_LOGIN_REQ)]
	public sealed class NKMPacket_STEAM_LOGIN_REQ : ISerializable
	{
		// Token: 0x06009BD1 RID: 39889 RVA: 0x00333E57 File Offset: 0x00332057
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.protocolVersion);
			stream.PutOrGet(ref this.deviceUid);
			stream.PutOrGet(ref this.accessToken);
			stream.PutOrGet(ref this.accountId);
			stream.PutOrGet<NKMUserMobileData>(ref this.userMobileData);
		}

		// Token: 0x04009017 RID: 36887
		public int protocolVersion;

		// Token: 0x04009018 RID: 36888
		public string deviceUid;

		// Token: 0x04009019 RID: 36889
		public string accessToken;

		// Token: 0x0400901A RID: 36890
		public string accountId;

		// Token: 0x0400901B RID: 36891
		public NKMUserMobileData userMobileData;
	}
}
