using System;
using Cs.Protocol;

namespace ClientPacket.Common
{
	// Token: 0x02001030 RID: 4144
	public sealed class NKMUserSimpleProfileData : ISerializable
	{
		// Token: 0x06009B30 RID: 39728 RVA: 0x003324F8 File Offset: 0x003306F8
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.userUid);
			stream.PutOrGet(ref this.friendCode);
			stream.PutOrGet(ref this.nickname);
			stream.PutOrGet(ref this.level);
			stream.PutOrGet(ref this.lastLoginDate);
			stream.PutOrGet(ref this.pvpScore);
			stream.PutOrGet(ref this.pvpTier);
			stream.PutOrGet(ref this.mainUnitId);
			stream.PutOrGet(ref this.mainUnitSkinId);
			stream.PutOrGet(ref this.selfieFrameId);
			stream.PutOrGet<NKMGuildSimpleData>(ref this.guildData);
		}

		// Token: 0x04008E82 RID: 36482
		public long userUid;

		// Token: 0x04008E83 RID: 36483
		public long friendCode;

		// Token: 0x04008E84 RID: 36484
		public string nickname;

		// Token: 0x04008E85 RID: 36485
		public int level;

		// Token: 0x04008E86 RID: 36486
		public DateTime lastLoginDate;

		// Token: 0x04008E87 RID: 36487
		public int pvpScore;

		// Token: 0x04008E88 RID: 36488
		public int pvpTier;

		// Token: 0x04008E89 RID: 36489
		public int mainUnitId;

		// Token: 0x04008E8A RID: 36490
		public int mainUnitSkinId;

		// Token: 0x04008E8B RID: 36491
		public int selfieFrameId;

		// Token: 0x04008E8C RID: 36492
		public NKMGuildSimpleData guildData = new NKMGuildSimpleData();
	}
}
