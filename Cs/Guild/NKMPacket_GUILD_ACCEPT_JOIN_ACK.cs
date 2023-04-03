using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EE1 RID: 3809
	[PacketId(ClientPacketId.kNKMPacket_GUILD_ACCEPT_JOIN_ACK)]
	public sealed class NKMPacket_GUILD_ACCEPT_JOIN_ACK : ISerializable
	{
		// Token: 0x060098A2 RID: 39074 RVA: 0x0032EC84 File Offset: 0x0032CE84
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.isAllow);
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.joinUserUid);
		}

		// Token: 0x04008B44 RID: 35652
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008B45 RID: 35653
		public bool isAllow;

		// Token: 0x04008B46 RID: 35654
		public long guildUid;

		// Token: 0x04008B47 RID: 35655
		public long joinUserUid;
	}
}
