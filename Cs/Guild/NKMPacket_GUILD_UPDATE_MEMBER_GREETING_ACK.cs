using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EFD RID: 3837
	[PacketId(ClientPacketId.kNKMPacket_GUILD_UPDATE_MEMBER_GREETING_ACK)]
	public sealed class NKMPacket_GUILD_UPDATE_MEMBER_GREETING_ACK : ISerializable
	{
		// Token: 0x060098DA RID: 39130 RVA: 0x0032F173 File Offset: 0x0032D373
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.greeting);
		}

		// Token: 0x04008B93 RID: 35731
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008B94 RID: 35732
		public string greeting;
	}
}
