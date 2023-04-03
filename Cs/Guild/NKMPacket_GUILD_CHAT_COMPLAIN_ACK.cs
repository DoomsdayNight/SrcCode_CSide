using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F14 RID: 3860
	[PacketId(ClientPacketId.kNKMPacket_GUILD_CHAT_COMPLAIN_ACK)]
	public sealed class NKMPacket_GUILD_CHAT_COMPLAIN_ACK : ISerializable
	{
		// Token: 0x06009908 RID: 39176 RVA: 0x0032F5E1 File Offset: 0x0032D7E1
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.messageUid);
		}

		// Token: 0x04008BD2 RID: 35794
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008BD3 RID: 35795
		public long guildUid;

		// Token: 0x04008BD4 RID: 35796
		public long messageUid;
	}
}
