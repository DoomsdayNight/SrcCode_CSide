using System;
using Cs.Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000ECC RID: 3788
	public sealed class GuildDungeonMemberArena : ISerializable
	{
		// Token: 0x06009878 RID: 39032 RVA: 0x0032E8D5 File Offset: 0x0032CAD5
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.arenaId);
			stream.PutOrGet(ref this.grade);
			stream.PutOrGet(ref this.regDate);
		}

		// Token: 0x04008B12 RID: 35602
		public int arenaId;

		// Token: 0x04008B13 RID: 35603
		public int grade;

		// Token: 0x04008B14 RID: 35604
		public DateTime regDate;
	}
}
