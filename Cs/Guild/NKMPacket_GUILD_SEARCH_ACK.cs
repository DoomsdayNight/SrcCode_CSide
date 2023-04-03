using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000ED6 RID: 3798
	[PacketId(ClientPacketId.kNKMPacket_GUILD_SEARCH_ACK)]
	public sealed class NKMPacket_GUILD_SEARCH_ACK : ISerializable
	{
		// Token: 0x0600988C RID: 39052 RVA: 0x0032EAD7 File Offset: 0x0032CCD7
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<GuildListData>(ref this.list);
		}

		// Token: 0x04008B2E RID: 35630
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008B2F RID: 35631
		public List<GuildListData> list = new List<GuildListData>();
	}
}
