using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F28 RID: 3880
	[PacketId(ClientPacketId.kNKMPacket_GUILD_CHAT_TRANSLATE_ACK)]
	public sealed class NKMPacket_GUILD_CHAT_TRANSLATE_ACK : ISerializable
	{
		// Token: 0x06009930 RID: 39216 RVA: 0x0032F9A3 File Offset: 0x0032DBA3
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.messageUid);
			stream.PutOrGet(ref this.textTranslated);
		}

		// Token: 0x04008C0B RID: 35851
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008C0C RID: 35852
		public long messageUid;

		// Token: 0x04008C0D RID: 35853
		public string textTranslated;
	}
}
