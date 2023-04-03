using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E34 RID: 3636
	[PacketId(ClientPacketId.kNKMPacket_DIVE_START_REQ)]
	public sealed class NKMPacket_DIVE_START_REQ : ISerializable
	{
		// Token: 0x06009758 RID: 38744 RVA: 0x0032CD82 File Offset: 0x0032AF82
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.cityID);
			stream.PutOrGet(ref this.stageID);
			stream.PutOrGet(ref this.deckIndexeList);
			stream.PutOrGet(ref this.isDiveStorm);
		}

		// Token: 0x0400897A RID: 35194
		public int cityID;

		// Token: 0x0400897B RID: 35195
		public int stageID;

		// Token: 0x0400897C RID: 35196
		public List<int> deckIndexeList = new List<int>();

		// Token: 0x0400897D RID: 35197
		public bool isDiveStorm;
	}
}
