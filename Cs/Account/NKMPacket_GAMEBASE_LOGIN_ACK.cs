using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001089 RID: 4233
	[PacketId(ClientPacketId.kNKMPacket_GAMEBASE_LOGIN_ACK)]
	public sealed class NKMPacket_GAMEBASE_LOGIN_ACK : ISerializable
	{
		// Token: 0x06009BCF RID: 39887 RVA: 0x00333DCC File Offset: 0x00331FCC
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.accessToken);
			stream.PutOrGet(ref this.gameServerIP);
			stream.PutOrGet(ref this.gameServerPort);
			stream.PutOrGet(ref this.contentsVersion);
			stream.PutOrGet(ref this.contentsTag);
			stream.PutOrGet(ref this.openTag);
			stream.PutOrGet(ref this.resultCode);
		}

		// Token: 0x0400900F RID: 36879
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04009010 RID: 36880
		public string accessToken;

		// Token: 0x04009011 RID: 36881
		public string gameServerIP;

		// Token: 0x04009012 RID: 36882
		public int gameServerPort;

		// Token: 0x04009013 RID: 36883
		public string contentsVersion;

		// Token: 0x04009014 RID: 36884
		public List<string> contentsTag = new List<string>();

		// Token: 0x04009015 RID: 36885
		public List<string> openTag = new List<string>();

		// Token: 0x04009016 RID: 36886
		public int resultCode;
	}
}
