using System;
using ClientPacket.Common;
using Cs.Protocol;

namespace ClientPacket.Raid
{
	// Token: 0x02000D56 RID: 3414
	public sealed class NKMRaidJoinData : ISerializable
	{
		// Token: 0x060095A7 RID: 38311 RVA: 0x0032A674 File Offset: 0x00328874
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.userUID);
			stream.PutOrGet(ref this.friendCode);
			stream.PutOrGet(ref this.nickName);
			stream.PutOrGet(ref this.mainUnitID);
			stream.PutOrGet(ref this.mainUnitSkinID);
			stream.PutOrGet(ref this.damage);
			stream.PutOrGet(ref this.highScore);
			stream.PutOrGet(ref this.tryCount);
			stream.PutOrGet<NKMGuildSimpleData>(ref this.guildData);
			stream.PutOrGet(ref this.tryAssist);
			stream.PutOrGet(ref this.level);
		}

		// Token: 0x04008759 RID: 34649
		public long userUID;

		// Token: 0x0400875A RID: 34650
		public long friendCode;

		// Token: 0x0400875B RID: 34651
		public string nickName;

		// Token: 0x0400875C RID: 34652
		public int mainUnitID;

		// Token: 0x0400875D RID: 34653
		public int mainUnitSkinID;

		// Token: 0x0400875E RID: 34654
		public float damage;

		// Token: 0x0400875F RID: 34655
		public bool highScore;

		// Token: 0x04008760 RID: 34656
		public short tryCount;

		// Token: 0x04008761 RID: 34657
		public NKMGuildSimpleData guildData = new NKMGuildSimpleData();

		// Token: 0x04008762 RID: 34658
		public bool tryAssist;

		// Token: 0x04008763 RID: 34659
		public int level;
	}
}
