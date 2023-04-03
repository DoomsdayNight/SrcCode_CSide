using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x0200107B RID: 4219
	[PacketId(ClientPacketId.kNKMPacket_CONTENTS_VERSION_ACK)]
	public sealed class NKMPacket_CONTENTS_VERSION_ACK : ISerializable
	{
		// Token: 0x06009BB3 RID: 39859 RVA: 0x00333B53 File Offset: 0x00331D53
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.contentsVersion);
			stream.PutOrGet(ref this.contentsTag);
			stream.PutOrGet(ref this.utcTime);
			stream.PutOrGet(ref this.utcOffset);
		}

		// Token: 0x04008FEB RID: 36843
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008FEC RID: 36844
		public string contentsVersion;

		// Token: 0x04008FED RID: 36845
		public List<string> contentsTag = new List<string>();

		// Token: 0x04008FEE RID: 36846
		public DateTime utcTime;

		// Token: 0x04008FEF RID: 36847
		public TimeSpan utcOffset;
	}
}
