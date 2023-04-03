using System;
using Cs.Protocol;

namespace ClientPacket.Raid
{
	// Token: 0x02000D55 RID: 3413
	public sealed class NKMMyRaidData : ISerializable
	{
		// Token: 0x060095A5 RID: 38309 RVA: 0x0032A5F0 File Offset: 0x003287F0
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.raidUID);
			stream.PutOrGet(ref this.stageID);
			stream.PutOrGet(ref this.cityID);
			stream.PutOrGet(ref this.curHP);
			stream.PutOrGet(ref this.maxHP);
			stream.PutOrGet(ref this.isCoop);
			stream.PutOrGet(ref this.isNew);
			stream.PutOrGet(ref this.expireDate);
			stream.PutOrGet(ref this.seasonID);
		}

		// Token: 0x04008750 RID: 34640
		public long raidUID;

		// Token: 0x04008751 RID: 34641
		public int stageID;

		// Token: 0x04008752 RID: 34642
		public int cityID;

		// Token: 0x04008753 RID: 34643
		public float curHP;

		// Token: 0x04008754 RID: 34644
		public float maxHP;

		// Token: 0x04008755 RID: 34645
		public bool isCoop;

		// Token: 0x04008756 RID: 34646
		public bool isNew;

		// Token: 0x04008757 RID: 34647
		public long expireDate;

		// Token: 0x04008758 RID: 34648
		public int seasonID;
	}
}
