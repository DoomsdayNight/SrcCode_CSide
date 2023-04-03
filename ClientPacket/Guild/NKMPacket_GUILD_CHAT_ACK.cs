using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F03 RID: 3843
	[PacketId(ClientPacketId.kNKMPacket_GUILD_CHAT_ACK)]
	public sealed class NKMPacket_GUILD_CHAT_ACK : ISerializable
	{
		// Token: 0x060098E6 RID: 39142 RVA: 0x0032F2C2 File Offset: 0x0032D4C2
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.messageUid);
		}

		// Token: 0x04008BA8 RID: 35752
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008BA9 RID: 35753
		public long messageUid;
	}
}
