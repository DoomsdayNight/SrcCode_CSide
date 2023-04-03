using System;
using System.Collections.Generic;
using System.Linq;
using Cs.Logging;
using NKC;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x020003A2 RID: 930
	public sealed class NKMAttendanceManager
	{
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600185E RID: 6238 RVA: 0x00062313 File Offset: 0x00060513
		public static IEnumerable<NKMAttendanceTabTemplet> Values
		{
			get
			{
				return NKMAttendanceManager.templets.Values;
			}
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x00062320 File Offset: 0x00060520
		public static bool LoadFromLua()
		{
			bool flag = true;
			NKMAttendanceManager.templets = NKMTempletLoader.LoadDictionary<NKMAttendanceTabTemplet>("AB_SCRIPT", "LUA_ATTENDANCE_TAB_TEMPLET", "ATTENDANCE_TAB_TEMPLET", new Func<NKMLua, NKMAttendanceTabTemplet>(NKMAttendanceTabTemplet.LoadFromLUA));
			if (NKMAttendanceManager.templets == null)
			{
				Log.ErrorAndExit("AttendanceTemplet Load Failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMAttendanceManager.cs", 221);
				return false;
			}
			NKMLua nkmlua = new NKMLua();
			if (nkmlua.LoadCommonPath("AB_SCRIPT", "LUA_ATTENDANCE_REWARD_TEMPLET", true) && nkmlua.OpenTable("ATTENDANCE_REWARD_TEMPLET"))
			{
				int num = 1;
				while (nkmlua.OpenTable(num))
				{
					if (NKMContentsVersionManager.CheckContentsVersion(nkmlua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMAttendanceManager.cs", 233))
					{
						NKMAttendanceRewardTemplet nkmattendanceRewardTemplet = new NKMAttendanceRewardTemplet();
						flag &= nkmattendanceRewardTemplet.LoadFromLUA(nkmlua);
						if (!NKMAttendanceManager.rewards.ContainsKey(nkmattendanceRewardTemplet.RewardGroup))
						{
							NKMAttendanceManager.rewards.Add(nkmattendanceRewardTemplet.RewardGroup, new Dictionary<int, NKMAttendanceRewardTemplet>());
						}
						NKMAttendanceManager.rewards[nkmattendanceRewardTemplet.RewardGroup][nkmattendanceRewardTemplet.LoginDate] = nkmattendanceRewardTemplet;
					}
					num++;
					nkmlua.CloseTable();
				}
				nkmlua.CloseTable();
			}
			nkmlua.LuaClose();
			foreach (KeyValuePair<int, NKMAttendanceTabTemplet> keyValuePair in NKMAttendanceManager.templets)
			{
				if (!NKMAttendanceManager.rewards.ContainsKey(keyValuePair.Value.RewardGroup))
				{
					Log.ErrorAndExit(string.Format("RewardGroup �� �ش��ϴ� ������ ���� - {0}", keyValuePair.Value.RewardGroup), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMAttendanceManager.cs", 257);
					return false;
				}
				if (NKMAttendanceManager.rewards[keyValuePair.Value.RewardGroup].Count != keyValuePair.Value.MaxAttCount)
				{
					Log.ErrorAndExit(string.Format("RewardGroup �� ���� ������ TabTemplet�� ���ǵ� ������ �ٸ� - {0}", keyValuePair.Value.RewardGroup), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMAttendanceManager.cs", 263);
					return false;
				}
				keyValuePair.Value.Join();
			}
			return true;
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x00062520 File Offset: 0x00060720
		public static NKMAttendanceTabTemplet GetAttendanceTabTamplet(int idx)
		{
			NKMAttendanceTabTemplet result;
			NKMAttendanceManager.templets.TryGetValue(idx, out result);
			return result;
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x0006253C File Offset: 0x0006073C
		public static Dictionary<int, NKMAttendanceRewardTemplet> GetAttendanceRewardTemplet(int rewardGroupID)
		{
			Dictionary<int, NKMAttendanceRewardTemplet> result;
			NKMAttendanceManager.rewards.TryGetValue(rewardGroupID, out result);
			return result;
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x00062558 File Offset: 0x00060758
		public static void Validate()
		{
			foreach (NKMAttendanceRewardTemplet nkmattendanceRewardTemplet in NKMAttendanceManager.rewards.Values.SelectMany((Dictionary<int, NKMAttendanceRewardTemplet> e) => e.Values))
			{
				nkmattendanceRewardTemplet.Validate();
			}
			foreach (NKMAttendanceTabTemplet nkmattendanceTabTemplet in NKMAttendanceManager.templets.Values)
			{
				nkmattendanceTabTemplet.Validate();
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06001863 RID: 6243 RVA: 0x00062610 File Offset: 0x00060810
		public static bool IsAttendanceBlocked
		{
			get
			{
				return NKMAttendanceManager.m_bContentBlocked;
			}
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x00062618 File Offset: 0x00060818
		public static void PostJoin()
		{
			foreach (KeyValuePair<int, NKMAttendanceTabTemplet> keyValuePair in NKMAttendanceManager.templets)
			{
				keyValuePair.Value.PostJoin();
			}
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x00062670 File Offset: 0x00060870
		public static DateTime GetTodayResetDate(DateTime date)
		{
			return NKMTime.GetResetTime(NKCSynchronizedTime.GetServerUTCTime(0.0), NKMTime.TimePeriod.Day);
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x00062686 File Offset: 0x00060886
		public static void Init(DateTime date)
		{
			NKMAttendanceManager.TodayAttendanceDate = NKMTime.GetResetTime(NKCSynchronizedTime.GetServerUTCTime(0.0), NKMTime.TimePeriod.Day);
			NKMAttendanceManager.m_tNextKeySettingTime = default(DateTime);
			NKMAttendanceManager.m_bContentBlocked = false;
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x000626B4 File Offset: 0x000608B4
		public static bool CheckNeedAttendance(NKMAttendanceData attendanceData, DateTime now, int idx = 0)
		{
			if (NKMAttendanceManager.m_bContentBlocked)
			{
				return false;
			}
			if (attendanceData == null || attendanceData.AttList.Count == 0)
			{
				return true;
			}
			if (idx != 0 && attendanceData.AttList.Find((NKMAttendance x) => x.IDX == idx) == null)
			{
				return true;
			}
			if (idx == 0)
			{
				attendanceData = NKMAttendanceManager.AddNeedAttendanceKeyByTemplet(attendanceData);
			}
			foreach (NKMAttendance nkmattendance in attendanceData.AttList)
			{
				if ((idx == 0 || nkmattendance.IDX == idx) && NKMAttendanceManager.CheckNeedAttendance(nkmattendance, now))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x00062784 File Offset: 0x00060984
		public static bool CheckNeedAttendance(NKMAttendance attendance, DateTime now)
		{
			if (NKMAttendanceManager.m_bContentBlocked)
			{
				return false;
			}
			if (attendance == null)
			{
				return true;
			}
			if (!NKMAttendanceManager.templets.ContainsKey(attendance.IDX))
			{
				Log.Error(string.Format("IDX {0} 가 존재하지 않음", attendance.IDX), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMAttendanceManagerEx.cs", 88);
				return false;
			}
			if (attendance.EventEndDate < now)
			{
				return false;
			}
			if (attendance.Count == 0)
			{
				return true;
			}
			if (NKMTime.GetNextResetTime(NKCScenManager.CurrentUserData().m_AttendanceData.LastUpdateDate, NKMTime.TimePeriod.Day) < now)
			{
				NKMAttendanceTabTemplet attendanceTabTamplet = NKMAttendanceManager.GetAttendanceTabTamplet(attendance.IDX);
				if (attendanceTabTamplet != null && attendance.Count < attendanceTabTamplet.MaxAttCount)
				{
					return true;
				}
			}
			if (!NKMAttendanceManager.rewards.ContainsKey(NKMAttendanceManager.templets[attendance.IDX].RewardGroup))
			{
				Log.Error(string.Format("RewardGroup {0} 가 존재하지 않음", NKMAttendanceManager.templets[attendance.IDX].RewardGroup), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMAttendanceManagerEx.cs", 110);
				return false;
			}
			return false;
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x0006287E File Offset: 0x00060A7E
		public static void ResetNeedAttendanceKey()
		{
			NKMAttendanceManager.m_lstAttendanceKey = new List<int>();
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x0006288A File Offset: 0x00060A8A
		public static List<int> GetNeedAttendanceKey()
		{
			return NKMAttendanceManager.m_lstAttendanceKey;
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x00062894 File Offset: 0x00060A94
		public static NKMAttendanceData AddNeedAttendanceKeyByTemplet(NKMAttendanceData attData)
		{
			if (NKMAttendanceManager.m_tNextKeySettingTime > NKCSynchronizedTime.GetServerUTCTime(0.0) && attData != null && attData.LastUpdateDate.Ticks > 0L)
			{
				return attData;
			}
			NKMAttendanceManager.m_tNextKeySettingTime = NKMTime.GetResetTime(NKCSynchronizedTime.GetServerUTCTime(0.0), NKMTime.TimePeriod.Day);
			if (attData == null)
			{
				attData = new NKMAttendanceData();
			}
			using (IEnumerator<NKMAttendanceTabTemplet> enumerator = NKMAttendanceManager.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					NKMAttendanceTabTemplet attendanceTemplet = enumerator.Current;
					if ((!NKMContentsVersionManager.HasDFChangeTagType(DataFormatChangeTagType.OPEN_TAG_ATTENDANCE) || attendanceTemplet.EnableByTag) && NKCSynchronizedTime.IsEventTime(attendanceTemplet.StartDateUtc, attendanceTemplet.EndDateUtc) && attData.AttList.Find((NKMAttendance e) => e.IDX == attendanceTemplet.IDX) == null && attendanceTemplet.EventType != NKM_ATTENDANCE_EVENT_TYPE.NEW && attendanceTemplet.EventType != NKM_ATTENDANCE_EVENT_TYPE.RETURN)
					{
						NKMAttendance nkmattendance = new NKMAttendance();
						nkmattendance.IDX = attendanceTemplet.IDX;
						nkmattendance.Count = 0;
						nkmattendance.EventEndDate = attendanceTemplet.EndDateUtc;
						attData.AttList.Add(nkmattendance);
					}
				}
			}
			NKMAttendanceManager.m_lstAttendanceKey = new List<int>();
			for (int i = 0; i < attData.AttList.Count; i++)
			{
				if (NKMAttendanceManager.CheckNeedAttendance(attData.AttList[i], NKCSynchronizedTime.GetServerUTCTime(0.0)))
				{
					NKMAttendanceManager.m_lstAttendanceKey.Add(attData.AttList[i].IDX);
				}
			}
			return attData;
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x00062A40 File Offset: 0x00060C40
		public static void SetContentBlock()
		{
			NKMAttendanceManager.m_bContentBlocked = true;
		}

		// Token: 0x04001032 RID: 4146
		private static Dictionary<int, NKMAttendanceTabTemplet> templets = null;

		// Token: 0x04001033 RID: 4147
		private static readonly Dictionary<int, Dictionary<int, NKMAttendanceRewardTemplet>> rewards = new Dictionary<int, Dictionary<int, NKMAttendanceRewardTemplet>>();

		// Token: 0x04001034 RID: 4148
		private static DateTime TodayAttendanceDate;

		// Token: 0x04001035 RID: 4149
		private static bool m_bContentBlocked = false;

		// Token: 0x04001036 RID: 4150
		private static List<int> m_lstAttendanceKey = new List<int>();

		// Token: 0x04001037 RID: 4151
		private static DateTime m_tNextKeySettingTime = default(DateTime);
	}
}
