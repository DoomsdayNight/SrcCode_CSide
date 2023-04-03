using System;
using Cs.Math;

namespace NKM
{
	// Token: 0x020003E4 RID: 996
	public class NKMDungeonEventTiming
	{
		// Token: 0x06001A4B RID: 6731 RVA: 0x0007146C File Offset: 0x0006F66C
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			cNKMLua.GetData("m_fEventTimeStart", ref this.m_fEventTimeStart);
			cNKMLua.GetData("m_fEventTimeEnd", ref this.m_fEventTimeEnd);
			cNKMLua.GetData("m_fEventBossHPLess", ref this.m_fEventBossHPLess);
			cNKMLua.GetData("m_fEventBossHPUpper", ref this.m_fEventBossHPUpper);
			cNKMLua.GetData("m_fEventIgnoreBossInitHPLess", ref this.m_fEventIgnoreBossInitHPLess);
			cNKMLua.GetData("m_fEventTimeGap", ref this.m_fEventTimeGap);
			cNKMLua.GetData("m_EventLiveDeckTag", ref this.m_EventLiveDeckTag);
			cNKMLua.GetData("m_EventLiveWarfareDungeonTag", ref this.m_EventLiveWarfareDungeonTag);
			cNKMLua.GetData("m_EventDieWarfareDungeonTag", ref this.m_EventDieWarfareDungeonTag);
			cNKMLua.GetData("m_EventDieUnitTag", ref this.m_EventDieUnitTag);
			cNKMLua.GetData("m_EventDieUnitTagCount", ref this.m_EventDieUnitTagCount);
			cNKMLua.GetData("m_EventDieDeckTag", ref this.m_EventDieDeckTag);
			cNKMLua.GetData("m_EventDieDeckTagCount", ref this.m_EventDieDeckTagCount);
			cNKMLua.GetData("m_EventTag", ref this.m_EventTag);
			cNKMLua.GetData("m_EventTagCount", ref this.m_EventTagCount);
			cNKMLua.GetData("m_fEventPos", ref this.m_fEventPos);
			cNKMLua.GetData<NKM_DUNGEON_EVENT_TYPE>("m_NKM_DUNGEON_EVENT_TYPE", ref this.m_NKM_DUNGEON_EVENT_TYPE);
			return true;
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x000715AC File Offset: 0x0006F7AC
		public bool EventTimeCheck(float fGameTime)
		{
			return (fGameTime >= this.m_fEventTimeStart && fGameTime <= this.m_fEventTimeEnd) || (fGameTime >= this.m_fEventTimeStart && this.m_fEventTimeEnd.IsNearlyZero(1E-05f));
		}

		// Token: 0x0400133F RID: 4927
		public float m_fEventTimeStart;

		// Token: 0x04001340 RID: 4928
		public float m_fEventTimeEnd;

		// Token: 0x04001341 RID: 4929
		public float m_fEventBossHPLess;

		// Token: 0x04001342 RID: 4930
		public float m_fEventBossHPUpper;

		// Token: 0x04001343 RID: 4931
		public bool m_fEventIgnoreBossInitHPLess = true;

		// Token: 0x04001344 RID: 4932
		public float m_fEventTimeGap;

		// Token: 0x04001345 RID: 4933
		public string m_EventLiveDeckTag = "";

		// Token: 0x04001346 RID: 4934
		public string m_EventLiveWarfareDungeonTag = "";

		// Token: 0x04001347 RID: 4935
		public string m_EventDieWarfareDungeonTag = "";

		// Token: 0x04001348 RID: 4936
		public string m_EventDieUnitTag = "";

		// Token: 0x04001349 RID: 4937
		public int m_EventDieUnitTagCount;

		// Token: 0x0400134A RID: 4938
		public string m_EventDieDeckTag = "";

		// Token: 0x0400134B RID: 4939
		public int m_EventDieDeckTagCount;

		// Token: 0x0400134C RID: 4940
		public string m_EventTag = "";

		// Token: 0x0400134D RID: 4941
		public int m_EventTagCount;

		// Token: 0x0400134E RID: 4942
		public float m_fEventPos;

		// Token: 0x0400134F RID: 4943
		public NKM_DUNGEON_EVENT_TYPE m_NKM_DUNGEON_EVENT_TYPE = NKM_DUNGEON_EVENT_TYPE.NDET_DECK;
	}
}
