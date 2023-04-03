using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001078 RID: 4216
	[PacketId(ClientPacketId.kNKMPacket_RECONNECT_ACK)]
	public sealed class NKMPacket_RECONNECT_ACK : ISerializable
	{
		// Token: 0x06009BAD RID: 39853 RVA: 0x00333AB4 File Offset: 0x00331CB4
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.accessToken);
			stream.PutOrGet(ref this.gameServerIp);
			stream.PutOrGet(ref this.gameServerPort);
			stream.PutOrGet(ref this.contentsVersion);
			stream.PutOrGet(ref this.contentsTag);
			stream.PutOrGet(ref this.openTag);
		}

		// Token: 0x04008FE3 RID: 36835
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008FE4 RID: 36836
		public string accessToken;

		// Token: 0x04008FE5 RID: 36837
		public string gameServerIp;

		// Token: 0x04008FE6 RID: 36838
		public int gameServerPort;

		// Token: 0x04008FE7 RID: 36839
		public string contentsVersion;

		// Token: 0x04008FE8 RID: 36840
		public List<string> contentsTag = new List<string>();

		// Token: 0x04008FE9 RID: 36841
		public List<string> openTag = new List<string>();
	}
}
