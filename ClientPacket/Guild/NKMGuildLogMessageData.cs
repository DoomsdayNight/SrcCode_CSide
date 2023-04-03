using System;
using ClientPacket.Common;
using Cs.Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000ECE RID: 3790
	public sealed class NKMGuildLogMessageData : ISerializable
	{
		// Token: 0x0600987C RID: 39036 RVA: 0x0032E948 File Offset: 0x0032CB48
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.messageUid);
			stream.PutOrGetEnum<GuildLogType>(ref this.guildLogType);
			stream.PutOrGet<NKMCommonProfile>(ref this.commonProfile);
			stream.PutOrGet(ref this.emotionId);
			stream.PutOrGet(ref this.message);
			stream.PutOrGet(ref this.createdAt);
			stream.PutOrGet(ref this.typeParam);
			stream.PutOrGet(ref this.blocked);
		}

		// Token: 0x04008B18 RID: 35608
		public long messageUid;

		// Token: 0x04008B19 RID: 35609
		public GuildLogType guildLogType;

		// Token: 0x04008B1A RID: 35610
		public NKMCommonProfile commonProfile = new NKMCommonProfile();

		// Token: 0x04008B1B RID: 35611
		public int emotionId;

		// Token: 0x04008B1C RID: 35612
		public string message;

		// Token: 0x04008B1D RID: 35613
		public DateTime createdAt;

		// Token: 0x04008B1E RID: 35614
		public long typeParam;

		// Token: 0x04008B1F RID: 35615
		public bool blocked;
	}
}
