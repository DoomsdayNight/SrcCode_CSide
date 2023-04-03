using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x0200106A RID: 4202
	[PacketId(ClientPacketId.kNKMPacket_LOGIN_REQ)]
	public sealed class NKMPacket_LOGIN_REQ : ISerializable
	{
		// Token: 0x06009B91 RID: 39825 RVA: 0x00333420 File Offset: 0x00331620
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.protocolVersion);
			stream.PutOrGet(ref this.accountID);
			stream.PutOrGet(ref this.password);
			stream.PutOrGetEnum<NKM_USER_AUTH_LEVEL>(ref this.userAuthLevel);
			stream.PutOrGet(ref this.deviceUid);
		}

		// Token: 0x04008F86 RID: 36742
		public long protocolVersion;

		// Token: 0x04008F87 RID: 36743
		public string accountID;

		// Token: 0x04008F88 RID: 36744
		public string password;

		// Token: 0x04008F89 RID: 36745
		public NKM_USER_AUTH_LEVEL userAuthLevel;

		// Token: 0x04008F8A RID: 36746
		public string deviceUid;
	}
}
