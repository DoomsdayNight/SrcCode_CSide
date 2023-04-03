using System;

namespace NKM
{
	// Token: 0x020003E7 RID: 999
	public class NKMDungeonEventTemplet
	{
		// Token: 0x06001A4D RID: 6733 RVA: 0x000715E0 File Offset: 0x0006F7E0
		public static bool IsPermanent(NKM_EVENT_ACTION_TYPE type)
		{
			return type - NKM_EVENT_ACTION_TYPE.UNLOCK_TUTORIAL_GAME_RE_RESPAWN <= 2;
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x00071620 File Offset: 0x0006F820
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			bool flag = true;
			if (cNKMLua.OpenTable("m_NKMDungeonEventTiming"))
			{
				flag &= this.m_NKMDungeonEventTiming.LoadFromLUA(cNKMLua);
				cNKMLua.CloseTable();
				cNKMLua.GetData("m_bPause", ref this.m_bPause);
				flag &= cNKMLua.GetData("m_EventID", ref this.m_EventID);
				flag &= cNKMLua.GetData<NKM_EVENT_START_CONDITION_TYPE>("m_EventCondition", ref this.m_EventCondition);
				cNKMLua.GetData("m_EventConditionValue1", ref this.m_EventConditionValue1);
				cNKMLua.GetData("m_EventConditionValue2", ref this.m_EventConditionValue2);
				cNKMLua.GetData("m_EventConditionNumValue", ref this.m_EventConditionNumValue);
				flag &= cNKMLua.GetData<NKM_EVENT_ACTION_TYPE>("m_dungeonEventType", ref this.m_dungeonEventType);
				cNKMLua.GetData("m_EventActionValue", ref this.m_EventActionValue);
				cNKMLua.GetData("m_EventActionStrValue", ref this.m_EventActionStrValue);
				cNKMLua.GetData("m_fEventDelay", ref this.m_fEventDelay);
				return flag;
			}
			return false;
		}

		// Token: 0x0400136D RID: 4973
		public NKMDungeonEventTiming m_NKMDungeonEventTiming = new NKMDungeonEventTiming();

		// Token: 0x0400136E RID: 4974
		public int m_EventID;

		// Token: 0x0400136F RID: 4975
		public NKM_EVENT_ACTION_TYPE m_dungeonEventType;

		// Token: 0x04001370 RID: 4976
		public int m_EventActionValue;

		// Token: 0x04001371 RID: 4977
		public string m_EventActionStrValue = "";

		// Token: 0x04001372 RID: 4978
		public NKM_EVENT_START_CONDITION_TYPE m_EventCondition;

		// Token: 0x04001373 RID: 4979
		public string m_EventConditionValue1 = "";

		// Token: 0x04001374 RID: 4980
		public string m_EventConditionValue2 = "";

		// Token: 0x04001375 RID: 4981
		public int m_EventConditionNumValue;

		// Token: 0x04001376 RID: 4982
		public bool m_bPause;

		// Token: 0x04001377 RID: 4983
		public float m_fEventDelay;
	}
}
