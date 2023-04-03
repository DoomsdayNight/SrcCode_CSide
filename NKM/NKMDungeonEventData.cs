using System;

namespace NKM
{
	// Token: 0x020003E8 RID: 1000
	public class NKMDungeonEventData
	{
		// Token: 0x04001378 RID: 4984
		public NKMDungeonEventTemplet m_DungeonEventTemplet;

		// Token: 0x04001379 RID: 4985
		public float m_fEventLastStartTime = -1f;

		// Token: 0x0400137A RID: 4986
		public float m_fEventLastEndTime = -1f;

		// Token: 0x0400137B RID: 4987
		public int EventConditionCache1 = -1;

		// Token: 0x0400137C RID: 4988
		public float m_fEventExecuteReserveTime;

		// Token: 0x0400137D RID: 4989
		public bool m_bEvokeReserved;
	}
}
