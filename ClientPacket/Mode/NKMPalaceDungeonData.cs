using System;
using Cs.Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E43 RID: 3651
	public sealed class NKMPalaceDungeonData : ISerializable
	{
		// Token: 0x06009776 RID: 38774 RVA: 0x0032CF71 File Offset: 0x0032B171
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.dungeonId);
			stream.PutOrGet(ref this.recentTime);
			stream.PutOrGet(ref this.bestTime);
		}

		// Token: 0x04008994 RID: 35220
		public int dungeonId;

		// Token: 0x04008995 RID: 35221
		public int recentTime;

		// Token: 0x04008996 RID: 35222
		public int bestTime;
	}
}
