using System;

namespace NKM
{
	// Token: 0x02000482 RID: 1154
	public class NKMTutorialManager
	{
		// Token: 0x06001F6F RID: 8047 RVA: 0x00094FBC File Offset: 0x000931BC
		public static bool IsTutorialCompleted(TutorialStep step, NKMUserData userData)
		{
			return userData.IsSuperUser() || userData.m_MissionData.GetCompletedMissionData(NKMTutorialManager.GetMissionID(step)) != null;
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x00094FDE File Offset: 0x000931DE
		private static int GetMissionID(TutorialStep step)
		{
			return (int)step;
		}
	}
}
