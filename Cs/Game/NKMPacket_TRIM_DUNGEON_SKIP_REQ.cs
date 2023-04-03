using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F6A RID: 3946
	[PacketId(ClientPacketId.kNKMPacket_TRIM_DUNGEON_SKIP_REQ)]
	public sealed class NKMPacket_TRIM_DUNGEON_SKIP_REQ : ISerializable
	{
		// Token: 0x060099B4 RID: 39348 RVA: 0x003305F9 File Offset: 0x0032E7F9
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.trimId);
			stream.PutOrGet(ref this.trimLevel);
			stream.PutOrGet(ref this.skipCount);
			stream.PutOrGet<NKMEventDeckData>(ref this.eventDeckList);
		}

		// Token: 0x04008CBD RID: 36029
		public int trimId;

		// Token: 0x04008CBE RID: 36030
		public int trimLevel;

		// Token: 0x04008CBF RID: 36031
		public int skipCount;

		// Token: 0x04008CC0 RID: 36032
		public List<NKMEventDeckData> eventDeckList = new List<NKMEventDeckData>();
	}
}
