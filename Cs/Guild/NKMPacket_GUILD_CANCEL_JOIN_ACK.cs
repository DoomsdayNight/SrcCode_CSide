using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EDC RID: 3804
	[PacketId(ClientPacketId.kNKMPacket_GUILD_CANCEL_JOIN_ACK)]
	public sealed class NKMPacket_GUILD_CANCEL_JOIN_ACK : ISerializable
	{
		// Token: 0x06009898 RID: 39064 RVA: 0x0032EBC4 File Offset: 0x0032CDC4
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.guildUid);
		}

		// Token: 0x04008B3A RID: 35642
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008B3B RID: 35643
		public long guildUid;
	}
}
