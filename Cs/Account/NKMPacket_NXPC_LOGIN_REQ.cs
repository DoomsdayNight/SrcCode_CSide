using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x0200107F RID: 4223
	[PacketId(ClientPacketId.kNKMPacket_NXPC_LOGIN_REQ)]
	public sealed class NKMPacket_NXPC_LOGIN_REQ : ISerializable
	{
		// Token: 0x06009BBB RID: 39867 RVA: 0x00333C5B File Offset: 0x00331E5B
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.protocolVersion);
			stream.PutOrGet(ref this.nexonPassport);
			stream.PutOrGet<NKMUserMobileData>(ref this.userMobileData);
			stream.PutOrGet(ref this.ssoLoginDate);
			stream.PutOrGet(ref this.deviceUid);
		}

		// Token: 0x04008FFA RID: 36858
		public int protocolVersion;

		// Token: 0x04008FFB RID: 36859
		public string nexonPassport;

		// Token: 0x04008FFC RID: 36860
		public NKMUserMobileData userMobileData;

		// Token: 0x04008FFD RID: 36861
		public DateTime ssoLoginDate;

		// Token: 0x04008FFE RID: 36862
		public string deviceUid;
	}
}
