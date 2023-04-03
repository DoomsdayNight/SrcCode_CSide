using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001088 RID: 4232
	[PacketId(ClientPacketId.kNKMPacket_GAMEBASE_LOGIN_REQ)]
	public sealed class NKMPacket_GAMEBASE_LOGIN_REQ : ISerializable
	{
		// Token: 0x06009BCD RID: 39885 RVA: 0x00333D6C File Offset: 0x00331F6C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.protocolVersion);
			stream.PutOrGet<NKMUserMobileData>(ref this.userMobileData);
			stream.PutOrGet(ref this.deviceUid);
			stream.PutOrGet(ref this.userId);
			stream.PutOrGet(ref this.accessToken);
			stream.PutOrGet(ref this.idpCode);
		}

		// Token: 0x04009009 RID: 36873
		public int protocolVersion;

		// Token: 0x0400900A RID: 36874
		public NKMUserMobileData userMobileData;

		// Token: 0x0400900B RID: 36875
		public string deviceUid;

		// Token: 0x0400900C RID: 36876
		public string userId;

		// Token: 0x0400900D RID: 36877
		public string accessToken;

		// Token: 0x0400900E RID: 36878
		public string idpCode;
	}
}
