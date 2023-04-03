using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E56 RID: 3670
	[PacketId(ClientPacketId.kNKMPacket_TRIM_START_REQ)]
	public sealed class NKMPacket_TRIM_START_REQ : ISerializable
	{
		// Token: 0x0600979C RID: 38812 RVA: 0x0032D33D File Offset: 0x0032B53D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.trimId);
			stream.PutOrGet(ref this.trimLevel);
			stream.PutOrGet<NKMEventDeckData>(ref this.eventDeckList);
		}

		// Token: 0x040089C9 RID: 35273
		public int trimId;

		// Token: 0x040089CA RID: 35274
		public int trimLevel;

		// Token: 0x040089CB RID: 35275
		public List<NKMEventDeckData> eventDeckList = new List<NKMEventDeckData>();
	}
}
