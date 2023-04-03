using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x0200106C RID: 4204
	[PacketId(ClientPacketId.kNKMPacket_ZLONG_LOGIN_REQ)]
	public sealed class NKMPacket_ZLONG_LOGIN_REQ : ISerializable
	{
		// Token: 0x06009B95 RID: 39829 RVA: 0x003334AC File Offset: 0x003316AC
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.protocolVersion);
			stream.PutOrGet<NKMUserMobileData>(ref this.userMobileData);
			stream.PutOrGet(ref this.deviceUid);
			stream.PutOrGet(ref this.opcode);
			stream.PutOrGet(ref this.channelId);
			stream.PutOrGet(ref this.tokenData);
			stream.PutOrGet(ref this.zlDeviceId);
			stream.PutOrGet(ref this.operators);
		}

		// Token: 0x04008F90 RID: 36752
		public int protocolVersion;

		// Token: 0x04008F91 RID: 36753
		public NKMUserMobileData userMobileData;

		// Token: 0x04008F92 RID: 36754
		public string deviceUid;

		// Token: 0x04008F93 RID: 36755
		public string opcode;

		// Token: 0x04008F94 RID: 36756
		public long channelId;

		// Token: 0x04008F95 RID: 36757
		public string tokenData;

		// Token: 0x04008F96 RID: 36758
		public string zlDeviceId;

		// Token: 0x04008F97 RID: 36759
		public string operators;
	}
}
