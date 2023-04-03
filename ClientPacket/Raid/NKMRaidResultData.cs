using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace ClientPacket.Raid
{
	// Token: 0x02000D59 RID: 3417
	public sealed class NKMRaidResultData : ISerializable
	{
		// Token: 0x060095AD RID: 38317 RVA: 0x0032A878 File Offset: 0x00328A78
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.raidUID);
			stream.PutOrGet(ref this.stageID);
			stream.PutOrGet(ref this.userUID);
			stream.PutOrGet(ref this.friendCode);
			stream.PutOrGet(ref this.nickname);
			stream.PutOrGet(ref this.mainUnitID);
			stream.PutOrGet(ref this.mainUnitSkinID);
			stream.PutOrGet(ref this.cityID);
			stream.PutOrGet(ref this.curHP);
			stream.PutOrGet(ref this.maxHP);
			stream.PutOrGet(ref this.isCoop);
			stream.PutOrGet(ref this.expireDate);
			stream.PutOrGet(ref this.highScore);
			stream.PutOrGet(ref this.tryCount);
			stream.PutOrGet(ref this.damage);
			stream.PutOrGet(ref this.tryAssist);
			stream.PutOrGet(ref this.seasonID);
			stream.PutOrGet<NKMRaidJoinData>(ref this.raidJoinDataList);
		}

		// Token: 0x0400877C RID: 34684
		public long raidUID;

		// Token: 0x0400877D RID: 34685
		public int stageID;

		// Token: 0x0400877E RID: 34686
		public long userUID;

		// Token: 0x0400877F RID: 34687
		public long friendCode;

		// Token: 0x04008780 RID: 34688
		public string nickname;

		// Token: 0x04008781 RID: 34689
		public int mainUnitID;

		// Token: 0x04008782 RID: 34690
		public int mainUnitSkinID;

		// Token: 0x04008783 RID: 34691
		public int cityID;

		// Token: 0x04008784 RID: 34692
		public float curHP;

		// Token: 0x04008785 RID: 34693
		public float maxHP;

		// Token: 0x04008786 RID: 34694
		public bool isCoop;

		// Token: 0x04008787 RID: 34695
		public long expireDate;

		// Token: 0x04008788 RID: 34696
		public bool highScore;

		// Token: 0x04008789 RID: 34697
		public short tryCount;

		// Token: 0x0400878A RID: 34698
		public float damage;

		// Token: 0x0400878B RID: 34699
		public bool tryAssist;

		// Token: 0x0400878C RID: 34700
		public int seasonID;

		// Token: 0x0400878D RID: 34701
		public List<NKMRaidJoinData> raidJoinDataList = new List<NKMRaidJoinData>();
	}
}
