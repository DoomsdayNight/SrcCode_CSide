using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x020003AC RID: 940
	public class NKMBuffSyncData : ISerializable
	{
		// Token: 0x060018B2 RID: 6322 RVA: 0x00063654 File Offset: 0x00061854
		public void Init()
		{
			this.m_BuffID = 0;
			this.m_BuffStatLevel = 0;
			this.m_BuffTimeLevel = 0;
			this.m_bNew = false;
			this.m_bAffect = false;
			this.m_OverlapCount = 1;
			this.m_bRangeSon = false;
			this.m_MasterGameUnitUID = 0;
			this.m_bUseMasterStat = true;
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x000636A0 File Offset: 0x000618A0
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_BuffID);
			stream.PutOrGet(ref this.m_BuffStatLevel);
			stream.PutOrGet(ref this.m_BuffTimeLevel);
			stream.PutOrGet(ref this.m_bNew);
			stream.PutOrGet(ref this.m_bAffect);
			stream.PutOrGet(ref this.m_OverlapCount);
			stream.PutOrGet(ref this.m_bRangeSon);
			stream.PutOrGet(ref this.m_MasterGameUnitUID);
			stream.PutOrGet(ref this.m_bUseMasterStat);
		}

		// Token: 0x04001063 RID: 4195
		public short m_BuffID;

		// Token: 0x04001064 RID: 4196
		public byte m_BuffStatLevel;

		// Token: 0x04001065 RID: 4197
		public byte m_BuffTimeLevel;

		// Token: 0x04001066 RID: 4198
		public bool m_bNew;

		// Token: 0x04001067 RID: 4199
		public bool m_bAffect;

		// Token: 0x04001068 RID: 4200
		public byte m_OverlapCount = 1;

		// Token: 0x04001069 RID: 4201
		public bool m_bRangeSon;

		// Token: 0x0400106A RID: 4202
		public short m_MasterGameUnitUID;

		// Token: 0x0400106B RID: 4203
		public bool m_bUseMasterStat = true;
	}
}
