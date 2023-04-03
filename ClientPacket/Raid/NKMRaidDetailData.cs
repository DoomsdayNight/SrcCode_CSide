using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace ClientPacket.Raid
{
	// Token: 0x02000D58 RID: 3416
	public sealed class NKMRaidDetailData : ISerializable
	{
		// Token: 0x060095AB RID: 38315 RVA: 0x0032A7C8 File Offset: 0x003289C8
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.raidUID);
			stream.PutOrGet(ref this.stageID);
			stream.PutOrGet(ref this.userUID);
			stream.PutOrGet(ref this.friendCode);
			stream.PutOrGet(ref this.cityID);
			stream.PutOrGet(ref this.curHP);
			stream.PutOrGet(ref this.maxHP);
			stream.PutOrGet(ref this.isCoop);
			stream.PutOrGet(ref this.isNew);
			stream.PutOrGet(ref this.expireDate);
			stream.PutOrGet<NKMRaidJoinData>(ref this.raidJoinDataList);
			stream.PutOrGet(ref this.seasonID);
		}

		// Token: 0x04008770 RID: 34672
		public long raidUID;

		// Token: 0x04008771 RID: 34673
		public int stageID;

		// Token: 0x04008772 RID: 34674
		public long userUID;

		// Token: 0x04008773 RID: 34675
		public long friendCode;

		// Token: 0x04008774 RID: 34676
		public int cityID;

		// Token: 0x04008775 RID: 34677
		public float curHP;

		// Token: 0x04008776 RID: 34678
		public float maxHP;

		// Token: 0x04008777 RID: 34679
		public bool isCoop;

		// Token: 0x04008778 RID: 34680
		public bool isNew;

		// Token: 0x04008779 RID: 34681
		public long expireDate;

		// Token: 0x0400877A RID: 34682
		public List<NKMRaidJoinData> raidJoinDataList = new List<NKMRaidJoinData>();

		// Token: 0x0400877B RID: 34683
		public int seasonID;
	}
}
