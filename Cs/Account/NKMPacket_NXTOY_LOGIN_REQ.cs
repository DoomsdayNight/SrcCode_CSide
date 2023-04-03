using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x0200106B RID: 4203
	[PacketId(ClientPacketId.kNKMPacket_NXTOY_LOGIN_REQ)]
	public sealed class NKMPacket_NXTOY_LOGIN_REQ : ISerializable
	{
		// Token: 0x06009B93 RID: 39827 RVA: 0x00333466 File Offset: 0x00331666
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.protocolVersion);
			stream.PutOrGet<NKMNXToyData>(ref this.nxToyData);
			stream.PutOrGet<NKMUserMobileData>(ref this.userMobileData);
			stream.PutOrGet(ref this.toyLoginDate);
			stream.PutOrGet(ref this.deviceUid);
		}

		// Token: 0x04008F8B RID: 36747
		public int protocolVersion;

		// Token: 0x04008F8C RID: 36748
		public NKMNXToyData nxToyData;

		// Token: 0x04008F8D RID: 36749
		public NKMUserMobileData userMobileData;

		// Token: 0x04008F8E RID: 36750
		public DateTime toyLoginDate;

		// Token: 0x04008F8F RID: 36751
		public string deviceUid;
	}
}
