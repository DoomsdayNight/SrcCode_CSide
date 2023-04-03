using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x0200107C RID: 4220
	[PacketId(ClientPacketId.kNKMPacket_ZLONG_LOGIN_ACK)]
	public sealed class NKMPacket_ZLONG_LOGIN_ACK : ISerializable
	{
		// Token: 0x06009BB5 RID: 39861 RVA: 0x00333BA4 File Offset: 0x00331DA4
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.accessToken);
			stream.PutOrGet(ref this.gameServerIP);
			stream.PutOrGet(ref this.gameServerPort);
			stream.PutOrGet(ref this.contentsVersion);
			stream.PutOrGet(ref this.contentsTag);
			stream.PutOrGet(ref this.openTag);
			stream.PutOrGet(ref this.status);
		}

		// Token: 0x04008FF0 RID: 36848
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008FF1 RID: 36849
		public string accessToken;

		// Token: 0x04008FF2 RID: 36850
		public string gameServerIP;

		// Token: 0x04008FF3 RID: 36851
		public int gameServerPort;

		// Token: 0x04008FF4 RID: 36852
		public string contentsVersion;

		// Token: 0x04008FF5 RID: 36853
		public List<string> contentsTag = new List<string>();

		// Token: 0x04008FF6 RID: 36854
		public List<string> openTag = new List<string>();

		// Token: 0x04008FF7 RID: 36855
		public int status;
	}
}
