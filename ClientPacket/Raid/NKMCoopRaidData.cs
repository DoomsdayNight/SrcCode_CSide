using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace ClientPacket.Raid
{
	// Token: 0x02000D57 RID: 3415
	public sealed class NKMCoopRaidData : ISerializable
	{
		// Token: 0x060095A9 RID: 38313 RVA: 0x0032A718 File Offset: 0x00328918
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.raidUID);
			stream.PutOrGet(ref this.stageID);
			stream.PutOrGet(ref this.userUID);
			stream.PutOrGet(ref this.friendCode);
			stream.PutOrGet(ref this.nickname);
			stream.PutOrGet(ref this.mainUnitID);
			stream.PutOrGet(ref this.mainUnitSkinID);
			stream.PutOrGet(ref this.curHP);
			stream.PutOrGet(ref this.maxHP);
			stream.PutOrGet(ref this.expireDate);
			stream.PutOrGet(ref this.seasonID);
			stream.PutOrGet<NKMRaidJoinData>(ref this.raidJoinDataList);
		}

		// Token: 0x04008764 RID: 34660
		public long raidUID;

		// Token: 0x04008765 RID: 34661
		public int stageID;

		// Token: 0x04008766 RID: 34662
		public long userUID;

		// Token: 0x04008767 RID: 34663
		public long friendCode;

		// Token: 0x04008768 RID: 34664
		public string nickname;

		// Token: 0x04008769 RID: 34665
		public int mainUnitID;

		// Token: 0x0400876A RID: 34666
		public int mainUnitSkinID;

		// Token: 0x0400876B RID: 34667
		public float curHP;

		// Token: 0x0400876C RID: 34668
		public float maxHP;

		// Token: 0x0400876D RID: 34669
		public long expireDate;

		// Token: 0x0400876E RID: 34670
		public int seasonID;

		// Token: 0x0400876F RID: 34671
		public List<NKMRaidJoinData> raidJoinDataList = new List<NKMRaidJoinData>();
	}
}
