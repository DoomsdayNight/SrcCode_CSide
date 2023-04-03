using System;

namespace NKM
{
	// Token: 0x02000438 RID: 1080
	public struct MissionUpdateData
	{
		// Token: 0x06001D52 RID: 7506 RVA: 0x00089CE5 File Offset: 0x00087EE5
		public MissionUpdateData(NKM_MISSION_COND missionCond, long value1, long value2 = 0L, long value3 = 0L)
		{
			this.missionCond = missionCond;
			this.value1 = value1;
			this.value2 = value2;
			this.value3 = value3;
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x00089D04 File Offset: 0x00087F04
		public MissionUpdateData(NKM_MISSION_COND missionCond, long value1, long value2)
		{
			this.missionCond = missionCond;
			this.value1 = value1;
			this.value2 = value2;
			this.value3 = 0L;
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x00089D23 File Offset: 0x00087F23
		public MissionUpdateData(NKM_MISSION_COND missionCond, long value1)
		{
			this.missionCond = missionCond;
			this.value1 = value1;
			this.value2 = 0L;
			this.value3 = 0L;
		}

		// Token: 0x04001D9E RID: 7582
		private NKM_MISSION_COND missionCond;

		// Token: 0x04001D9F RID: 7583
		private long value1;

		// Token: 0x04001DA0 RID: 7584
		private long value2;

		// Token: 0x04001DA1 RID: 7585
		private long value3;
	}
}
