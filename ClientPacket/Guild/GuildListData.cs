using System;
using Cs.Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EC9 RID: 3785
	public sealed class GuildListData : ISerializable
	{
		// Token: 0x06009872 RID: 39026 RVA: 0x0032E7EC File Offset: 0x0032C9EC
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.name);
			stream.PutOrGet(ref this.badgeId);
			stream.PutOrGet(ref this.guildLevel);
			stream.PutOrGetEnum<GuildJoinType>(ref this.guildJoinType);
			stream.PutOrGet(ref this.masterNickname);
			stream.PutOrGet(ref this.memberCount);
			stream.PutOrGet(ref this.greeting);
		}

		// Token: 0x04008B02 RID: 35586
		public long guildUid;

		// Token: 0x04008B03 RID: 35587
		public string name;

		// Token: 0x04008B04 RID: 35588
		public long badgeId;

		// Token: 0x04008B05 RID: 35589
		public int guildLevel;

		// Token: 0x04008B06 RID: 35590
		public GuildJoinType guildJoinType;

		// Token: 0x04008B07 RID: 35591
		public string masterNickname;

		// Token: 0x04008B08 RID: 35592
		public int memberCount;

		// Token: 0x04008B09 RID: 35593
		public string greeting;
	}
}
