using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F29 RID: 3881
	[PacketId(ClientPacketId.kNKMPacket_GUILD_LOG_LIST_NOT)]
	public sealed class NKMPacket_GUILD_LOG_LIST_NOT : ISerializable
	{
		// Token: 0x06009932 RID: 39218 RVA: 0x0032F9D1 File Offset: 0x0032DBD1
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet<NKMGuildLogMessageData>(ref this.logs);
		}

		// Token: 0x04008C0E RID: 35854
		public long guildUid;

		// Token: 0x04008C0F RID: 35855
		public List<NKMGuildLogMessageData> logs = new List<NKMGuildLogMessageData>();
	}
}
