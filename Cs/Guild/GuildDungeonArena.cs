using System;
using Cs.Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000ECB RID: 3787
	public sealed class GuildDungeonArena : ISerializable
	{
		// Token: 0x06009876 RID: 39030 RVA: 0x0032E8A7 File Offset: 0x0032CAA7
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.arenaIndex);
			stream.PutOrGet(ref this.totalMedalCount);
			stream.PutOrGet(ref this.playUserUid);
		}

		// Token: 0x04008B0F RID: 35599
		public int arenaIndex;

		// Token: 0x04008B10 RID: 35600
		public int totalMedalCount;

		// Token: 0x04008B11 RID: 35601
		public long playUserUid;
	}
}
