using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F27 RID: 3879
	[PacketId(ClientPacketId.kNKMPacket_GUILD_CHAT_TRANSLATE_REQ)]
	public sealed class NKMPacket_GUILD_CHAT_TRANSLATE_REQ : ISerializable
	{
		// Token: 0x0600992E RID: 39214 RVA: 0x0032F975 File Offset: 0x0032DB75
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.messageUid);
			stream.PutOrGet(ref this.targetLanguage);
		}

		// Token: 0x04008C08 RID: 35848
		public long guildUid;

		// Token: 0x04008C09 RID: 35849
		public long messageUid;

		// Token: 0x04008C0A RID: 35850
		public string targetLanguage;
	}
}
