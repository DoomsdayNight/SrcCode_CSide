using System;

namespace NKM
{
	// Token: 0x020003AD RID: 941
	public struct NKMBuffCreateData
	{
		// Token: 0x060018B4 RID: 6324 RVA: 0x00063719 File Offset: 0x00061919
		public NKMBuffCreateData(string _buffID, byte _buffStatLevel, byte _buffTimeLevel, short _masterGameUnitUID, bool _bUseMasterStat, bool _bRangeSon, bool _stateEndRemove, byte _overlapCount)
		{
			this.m_buffID = _buffID;
			this.m_buffStatLevel = _buffStatLevel;
			this.m_buffTimeLevel = _buffTimeLevel;
			this.m_masterGameUnitUID = _masterGameUnitUID;
			this.m_bUseMasterStat = _bUseMasterStat;
			this.m_bRangeSon = _bRangeSon;
			this.m_stateEndRemove = _stateEndRemove;
			this.m_overlapCount = _overlapCount;
		}

		// Token: 0x0400106C RID: 4204
		public string m_buffID;

		// Token: 0x0400106D RID: 4205
		public byte m_buffStatLevel;

		// Token: 0x0400106E RID: 4206
		public byte m_buffTimeLevel;

		// Token: 0x0400106F RID: 4207
		public short m_masterGameUnitUID;

		// Token: 0x04001070 RID: 4208
		public bool m_bUseMasterStat;

		// Token: 0x04001071 RID: 4209
		public bool m_bRangeSon;

		// Token: 0x04001072 RID: 4210
		public bool m_stateEndRemove;

		// Token: 0x04001073 RID: 4211
		public byte m_overlapCount;
	}
}
