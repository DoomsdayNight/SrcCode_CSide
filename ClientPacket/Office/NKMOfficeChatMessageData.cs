using System;
using ClientPacket.Common;
using Cs.Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E12 RID: 3602
	public sealed class NKMOfficeChatMessageData : ISerializable
	{
		// Token: 0x06009718 RID: 38680 RVA: 0x0032C811 File Offset: 0x0032AA11
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.messageUid);
			stream.PutOrGet<NKMCommonProfile>(ref this.commonProfile);
			stream.PutOrGet(ref this.emotionId);
			stream.PutOrGet(ref this.createdAt);
		}

		// Token: 0x0400892B RID: 35115
		public long messageUid;

		// Token: 0x0400892C RID: 35116
		public NKMCommonProfile commonProfile = new NKMCommonProfile();

		// Token: 0x0400892D RID: 35117
		public int emotionId;

		// Token: 0x0400892E RID: 35118
		public DateTime createdAt;
	}
}
