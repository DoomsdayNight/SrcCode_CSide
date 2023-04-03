using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x0200106D RID: 4205
	[PacketId(ClientPacketId.kNKMPacket_LOGIN_ACK)]
	public sealed class NKMPacket_LOGIN_ACK : ISerializable
	{
		// Token: 0x06009B97 RID: 39831 RVA: 0x00333524 File Offset: 0x00331724
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.accessToken);
			stream.PutOrGet(ref this.gameServerIP);
			stream.PutOrGet(ref this.gameServerPort);
			stream.PutOrGet(ref this.contentsVersion);
			stream.PutOrGet(ref this.contentsTag);
			stream.PutOrGet(ref this.openTag);
		}

		// Token: 0x04008F98 RID: 36760
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008F99 RID: 36761
		public string accessToken;

		// Token: 0x04008F9A RID: 36762
		public string gameServerIP;

		// Token: 0x04008F9B RID: 36763
		public int gameServerPort;

		// Token: 0x04008F9C RID: 36764
		public string contentsVersion;

		// Token: 0x04008F9D RID: 36765
		public List<string> contentsTag = new List<string>();

		// Token: 0x04008F9E RID: 36766
		public List<string> openTag = new List<string>();
	}
}
