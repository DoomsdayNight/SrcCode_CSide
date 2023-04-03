using System;
using Cs.Protocol;
using NKM;

namespace ClientPacket.Common
{
	// Token: 0x02001055 RID: 4181
	public sealed class NKMGameRecordUnitData : ISerializable
	{
		// Token: 0x06009B6A RID: 39786 RVA: 0x00332EA0 File Offset: 0x003310A0
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitId);
			stream.PutOrGet(ref this.changeUnitName);
			stream.PutOrGet(ref this.unitLevel);
			stream.PutOrGet(ref this.isSummonee);
			stream.PutOrGet(ref this.isAssistUnit);
			stream.PutOrGet(ref this.isLeader);
			stream.PutOrGetEnum<NKM_TEAM_TYPE>(ref this.teamType);
			stream.PutOrGet(ref this.recordGiveDamage);
			stream.PutOrGet(ref this.recordTakeDamage);
			stream.PutOrGet(ref this.recordHeal);
			stream.PutOrGet(ref this.recordSummonCount);
			stream.PutOrGet(ref this.recordDieCount);
			stream.PutOrGet(ref this.recordKillCount);
			stream.PutOrGet(ref this.playtime);
		}

		// Token: 0x04008F2F RID: 36655
		public int unitId;

		// Token: 0x04008F30 RID: 36656
		public string changeUnitName;

		// Token: 0x04008F31 RID: 36657
		public int unitLevel;

		// Token: 0x04008F32 RID: 36658
		public bool isSummonee;

		// Token: 0x04008F33 RID: 36659
		public bool isAssistUnit;

		// Token: 0x04008F34 RID: 36660
		public bool isLeader;

		// Token: 0x04008F35 RID: 36661
		public NKM_TEAM_TYPE teamType;

		// Token: 0x04008F36 RID: 36662
		public float recordGiveDamage;

		// Token: 0x04008F37 RID: 36663
		public float recordTakeDamage;

		// Token: 0x04008F38 RID: 36664
		public float recordHeal;

		// Token: 0x04008F39 RID: 36665
		public int recordSummonCount;

		// Token: 0x04008F3A RID: 36666
		public int recordDieCount;

		// Token: 0x04008F3B RID: 36667
		public int recordKillCount;

		// Token: 0x04008F3C RID: 36668
		public int playtime;
	}
}
