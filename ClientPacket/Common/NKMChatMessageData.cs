using System;
using Cs.Protocol;

namespace ClientPacket.Common
{
	// Token: 0x0200105A RID: 4186
	public sealed class NKMChatMessageData : ISerializable
	{
		// Token: 0x06009B72 RID: 39794 RVA: 0x003330F8 File Offset: 0x003312F8
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.messageUid);
			stream.PutOrGetEnum<ChatMessageType>(ref this.messageType);
			stream.PutOrGet<NKMCommonProfile>(ref this.commonProfile);
			stream.PutOrGet(ref this.emotionId);
			stream.PutOrGet(ref this.message);
			stream.PutOrGet(ref this.createdAt);
			stream.PutOrGet(ref this.typeParam);
			stream.PutOrGet(ref this.blocked);
		}

		// Token: 0x04008F59 RID: 36697
		public long messageUid;

		// Token: 0x04008F5A RID: 36698
		public ChatMessageType messageType;

		// Token: 0x04008F5B RID: 36699
		public NKMCommonProfile commonProfile = new NKMCommonProfile();

		// Token: 0x04008F5C RID: 36700
		public int emotionId;

		// Token: 0x04008F5D RID: 36701
		public string message;

		// Token: 0x04008F5E RID: 36702
		public DateTime createdAt;

		// Token: 0x04008F5F RID: 36703
		public long typeParam;

		// Token: 0x04008F60 RID: 36704
		public bool blocked;
	}
}
