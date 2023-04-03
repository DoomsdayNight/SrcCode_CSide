using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000ED8 RID: 3800
	[PacketId(ClientPacketId.kNKMPacket_GUILD_LIST_ACK)]
	public sealed class NKMPacket_GUILD_LIST_ACK : ISerializable
	{
		// Token: 0x06009890 RID: 39056 RVA: 0x0032EB1A File Offset: 0x0032CD1A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<GuildListData>(ref this.list);
		}

		// Token: 0x04008B31 RID: 35633
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008B32 RID: 35634
		public List<GuildListData> list = new List<GuildListData>();
	}
}
