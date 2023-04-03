using System;
using ClientPacket.Common;
using NKM;
using NKM.Templet;

namespace NKC.UI.Friend
{
	// Token: 0x02000B12 RID: 2834
	public static class NKCMentoringUtil
	{
		// Token: 0x060080AE RID: 32942 RVA: 0x002B540C File Offset: 0x002B360C
		public static NKMMentoringTemplet GetCurrentTempet()
		{
			return NKMMentoringTemplet.Find(NKCScenManager.CurrentUserData().MentoringData.SeasonId);
		}

		// Token: 0x060080AF RID: 32943 RVA: 0x002B5422 File Offset: 0x002B3622
		public static MentoringIdentity GetMentoringIdentity(NKMUserData userData)
		{
			if (userData != null && userData.UserLevel > NKMMentoringConst.MentorAddLimitLevel && NKCScenManager.CurrentUserData().MentoringData.MyMentor == null)
			{
				return MentoringIdentity.Mentor;
			}
			return MentoringIdentity.Mentee;
		}

		// Token: 0x060080B0 RID: 32944 RVA: 0x002B5448 File Offset: 0x002B3648
		public static int GetMenteeMissionMaxCount()
		{
			return NKCMentoringUtil.GetMenteeMissionMaxCount(NKCMentoringUtil.GetCurrentTempet());
		}

		// Token: 0x060080B1 RID: 32945 RVA: 0x002B5454 File Offset: 0x002B3654
		public static int GetMenteeMissionMaxCount(NKMMentoringTemplet templet)
		{
			int result = 0;
			if (templet != null)
			{
				NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(templet.AllClearMissionId);
				if (missionTemplet != null)
				{
					result = missionTemplet.m_MissionCond.value1.Count + 1;
				}
			}
			return result;
		}

		// Token: 0x060080B2 RID: 32946 RVA: 0x002B548C File Offset: 0x002B368C
		public static NKCMentoringUtil.MENTORING_STATS CheckMentoringSeason()
		{
			if (NKCScenManager.CurrentUserData().MentoringData.SeasonId == 0)
			{
				return NKCMentoringUtil.MENTORING_STATS.MS_NONE;
			}
			NKMMentoringTemplet currentTempet = NKCMentoringUtil.GetCurrentTempet();
			if (currentTempet != null)
			{
				NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(currentTempet.MissionTabId);
				if (missionTabTemplet != null)
				{
					if (missionTabTemplet.m_endTime <= NKCSynchronizedTime.ServiceTime)
					{
						if (missionTabTemplet.m_endTime.AddMinutes((double)NKMMentoringConst.MentoringSeasonInitMinutes) <= NKCSynchronizedTime.ServiceTime)
						{
							return NKCMentoringUtil.MENTORING_STATS.MS_MAINTENANCE;
						}
						NKCPacketSender.Send_NKMPacket_MENTORING_SEASON_ID_REQ();
						return NKCMentoringUtil.MENTORING_STATS.MS_SEASON_ID_REQ;
					}
					else
					{
						if (missionTabTemplet.m_startTime.AddMinutes((double)NKMMentoringConst.MentoringSeasonInitMinutes) >= NKCSynchronizedTime.ServiceTime)
						{
							return NKCMentoringUtil.MENTORING_STATS.MS_MAINTENANCE;
						}
						return NKCMentoringUtil.MENTORING_STATS.MS_ACTIVE;
					}
				}
			}
			return NKCMentoringUtil.MENTORING_STATS.MS_NONE;
		}

		// Token: 0x060080B3 RID: 32947 RVA: 0x002B5524 File Offset: 0x002B3724
		public static bool IsCanReceiveMenteeMissionReward(NKMUserData userData)
		{
			if (userData == null)
			{
				return false;
			}
			if (NKCMentoringUtil.GetMentoringIdentity(userData) == MentoringIdentity.Mentor)
			{
				return false;
			}
			NKMMentoringTemplet currentTempet = NKCMentoringUtil.GetCurrentTempet();
			if (currentTempet == null)
			{
				return false;
			}
			if (userData.MentoringData.MyMentor != null && !userData.MentoringData.bMenteeGraduate)
			{
				NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(currentTempet.AllClearMissionId);
				if (missionTemplet != null)
				{
					foreach (int mission_id in missionTemplet.m_MissionCond.value1)
					{
						NKMMissionTemplet missionTemplet2 = NKMMissionManager.GetMissionTemplet(mission_id);
						if (missionTemplet2 != null)
						{
							NKMMissionManager.MissionState missionState = NKMMissionManager.GetMissionState(missionTemplet2);
							if (missionState == NKMMissionManager.MissionState.CAN_COMPLETE || missionState == NKMMissionManager.MissionState.REPEAT_CAN_COMPLETE)
							{
								return true;
							}
						}
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x060080B4 RID: 32948 RVA: 0x002B55DC File Offset: 0x002B37DC
		public static bool IsDontHaveMentor(NKMUserData userData)
		{
			return userData != null && NKCMentoringUtil.GetMentoringIdentity(userData) != MentoringIdentity.Mentor && userData.MentoringData.MyMentor == null;
		}

		// Token: 0x020018AA RID: 6314
		public enum MENTORING_STATS
		{
			// Token: 0x0400A976 RID: 43382
			MS_NONE,
			// Token: 0x0400A977 RID: 43383
			MS_MAINTENANCE,
			// Token: 0x0400A978 RID: 43384
			MS_SEASON_ID_REQ,
			// Token: 0x0400A979 RID: 43385
			MS_ACTIVE
		}
	}
}
