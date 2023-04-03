using System;
using System.Collections.Generic;
using Cs.Core.Util;
using Cs.Logging;
using Cs.Shared.Time;
using NKC;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x020003A0 RID: 928
	public class NKMAttendanceTabTemplet : INKMTemplet, INKMTempletEx
	{
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x00061C64 File Offset: 0x0005FE64
		public int Key
		{
			get
			{
				return this.idx;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06001836 RID: 6198 RVA: 0x00061C6C File Offset: 0x0005FE6C
		public int IDX
		{
			get
			{
				return this.idx;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x00061C74 File Offset: 0x0005FE74
		public int TabID
		{
			get
			{
				return this.tabID;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06001838 RID: 6200 RVA: 0x00061C7C File Offset: 0x0005FE7C
		public NKM_ATTENDANCE_EVENT_TYPE EventType
		{
			get
			{
				return this.eventType;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x00061C84 File Offset: 0x0005FE84
		public int RewardGroup
		{
			get
			{
				return this.rewardGroup;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x0600183A RID: 6202 RVA: 0x00061C8C File Offset: 0x0005FE8C
		public string TabNameMain
		{
			get
			{
				return this.tabNameMain;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x00061C94 File Offset: 0x0005FE94
		public string TabNameSub
		{
			get
			{
				return this.tabNameSub;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x0600183C RID: 6204 RVA: 0x00061C9C File Offset: 0x0005FE9C
		public string BackgroundImage
		{
			get
			{
				return this.backgroundImage;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x0600183D RID: 6205 RVA: 0x00061CA4 File Offset: 0x0005FEA4
		public NKMIntervalTemplet IntervalTemplet
		{
			get
			{
				return this.intervalTemplet;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x0600183E RID: 6206 RVA: 0x00061CAC File Offset: 0x0005FEAC
		public DateTime StartDateUtc
		{
			get
			{
				return ServiceTime.ToUtcTime(this.intervalTemplet.StartDate);
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x0600183F RID: 6207 RVA: 0x00061CBE File Offset: 0x0005FEBE
		public DateTime EndDateUtc
		{
			get
			{
				return ServiceTime.ToUtcTime(this.intervalTemplet.EndDate);
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06001840 RID: 6208 RVA: 0x00061CD0 File Offset: 0x0005FED0
		public int LimitDayCount
		{
			get
			{
				return this.limitDayCount;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06001841 RID: 6209 RVA: 0x00061CD8 File Offset: 0x0005FED8
		public int ReturnDayCount
		{
			get
			{
				return this.returnDayCount;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06001842 RID: 6210 RVA: 0x00061CE0 File Offset: 0x0005FEE0
		public string PrefabName
		{
			get
			{
				return this.prefabName;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x00061CE8 File Offset: 0x0005FEE8
		public string TabIconName
		{
			get
			{
				return this.tabIconName;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06001844 RID: 6212 RVA: 0x00061CF0 File Offset: 0x0005FEF0
		public int MaxAttCount
		{
			get
			{
				return this.maxAttCount;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06001845 RID: 6213 RVA: 0x00061CF8 File Offset: 0x0005FEF8
		// (set) Token: 0x06001846 RID: 6214 RVA: 0x00061D00 File Offset: 0x0005FF00
		public IReadOnlyDictionary<int, NKMAttendanceRewardTemplet> RewardTemplets { get; private set; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06001847 RID: 6215 RVA: 0x00061D09 File Offset: 0x0005FF09
		public bool EnableByTag
		{
			get
			{
				return NKMOpenTagManager.IsOpened(this.m_OpenTag);
			}
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x00061D18 File Offset: 0x0005FF18
		public static NKMAttendanceTabTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMAttendanceManager.cs", 89))
			{
				return null;
			}
			NKMAttendanceTabTemplet nkmattendanceTabTemplet = new NKMAttendanceTabTemplet();
			bool flag = true;
			flag &= cNKMLua.GetData("IDX", ref nkmattendanceTabTemplet.idx);
			flag &= cNKMLua.GetData("m_TabID", ref nkmattendanceTabTemplet.tabID);
			flag &= cNKMLua.GetData<NKM_ATTENDANCE_EVENT_TYPE>("m_EventType", ref nkmattendanceTabTemplet.eventType);
			flag &= cNKMLua.GetData("m_RewardGroup", ref nkmattendanceTabTemplet.rewardGroup);
			flag &= cNKMLua.GetData("m_TabNameMain", ref nkmattendanceTabTemplet.tabNameMain);
			flag &= cNKMLua.GetData("m_TabNameSub", ref nkmattendanceTabTemplet.tabNameSub);
			flag &= cNKMLua.GetData("m_TabBackgroundImage", ref nkmattendanceTabTemplet.backgroundImage);
			flag &= cNKMLua.GetData("m_LimitDayCount", ref nkmattendanceTabTemplet.limitDayCount);
			flag &= cNKMLua.GetData("m_ReturnDayCount", ref nkmattendanceTabTemplet.returnDayCount);
			flag &= cNKMLua.GetData("m_PrefabName", ref nkmattendanceTabTemplet.prefabName);
			flag &= cNKMLua.GetData("m_TabIconName", ref nkmattendanceTabTemplet.tabIconName);
			flag &= cNKMLua.GetData("m_MaxAttCount", ref nkmattendanceTabTemplet.maxAttCount);
			flag &= cNKMLua.GetData("m_DateStrID", ref nkmattendanceTabTemplet.intervalId);
			if (NKMContentsVersionManager.HasDFChangeTagType(DataFormatChangeTagType.OPEN_TAG_ATTENDANCE))
			{
				flag &= cNKMLua.GetData("m_OpenTag", ref nkmattendanceTabTemplet.m_OpenTag);
			}
			if (!flag)
			{
				Log.Error("NKMAttendanceTabTemplet LoadFromLUA Fail", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMAttendanceManager.cs", 116);
				return null;
			}
			return nkmattendanceTabTemplet;
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x00061E78 File Offset: 0x00060078
		public void Join()
		{
			if (NKMUtil.IsServer)
			{
				this.JoinIntervalTemplet();
			}
			this.RewardTemplets = NKMAttendanceManager.GetAttendanceRewardTemplet(this.RewardGroup);
			if (this.RewardTemplets == null)
			{
				Log.ErrorAndExit(string.Format("Attendance Reward Group is null. Rewardgroup is {0}", this.RewardGroup), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMAttendanceManager.cs", 132);
			}
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x00061ED0 File Offset: 0x000600D0
		public void JoinIntervalTemplet()
		{
			if (!string.IsNullOrEmpty(this.intervalId))
			{
				this.intervalTemplet = NKMIntervalTemplet.Find(this.intervalId);
				if (this.intervalTemplet == null)
				{
					this.intervalTemplet = NKMIntervalTemplet.Unuseable;
					Log.ErrorAndExit(string.Format("[Attendance:{0}]�߸��� interval id:{1}", this.Key, this.intervalId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMAttendanceManager.cs", 144);
					return;
				}
				if (this.intervalTemplet.IsRepeatDate)
				{
					Log.ErrorAndExit(string.Format("[Attendance:{0}] �ݺ� �Ⱓ���� ��� �Ұ�. id:{1}", this.Key, this.intervalId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMAttendanceManager.cs", 150);
					return;
				}
			}
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x00061F74 File Offset: 0x00060174
		public void Validate()
		{
			if (this.IntervalTemplet.StartDate.TimeOfDay != TimeReset.ResetHourSpan)
			{
				NKMTempletError.Add(string.Format("[Attendance{0}] �⼮ ���۽ð��� ������ ���� �ð��� ���� ����. intervalId:{1} startdate:{2}", this.tabID, this.intervalId, this.intervalTemplet.StartDate), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMAttendanceManager.cs", 160);
			}
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x00061FDA File Offset: 0x000601DA
		public void PostJoin()
		{
			this.JoinIntervalTemplet();
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x00061FE4 File Offset: 0x000601E4
		public NKMIntervalTemplet GetIntervalTemplet()
		{
			if (string.IsNullOrEmpty(this.intervalId))
			{
				return NKMIntervalTemplet.Invalid;
			}
			if (!this.intervalTemplet.IsValid)
			{
				this.intervalTemplet = NKMIntervalTemplet.Find(this.intervalId);
				if (this.intervalTemplet == null)
				{
					Log.ErrorAndExit(string.Format("[Attendance:{0}] 잘못된 interval id:{1}", this.Key, this.intervalId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMAttendanceManagerEx.cs", 204);
					return null;
				}
				if (this.intervalTemplet.IsRepeatDate)
				{
					Log.ErrorAndExit(string.Format("[Attendance:{0}] 반복 기간설정 사용 불가. id:{1}", this.Key, this.intervalId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMAttendanceManagerEx.cs", 210);
					return null;
				}
			}
			return this.intervalTemplet;
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x00062098 File Offset: 0x00060298
		public DateTime GetStartTime(bool bUTC)
		{
			if (!this.IntervalTemplet.IsValid)
			{
				Log.ErrorAndExit("Invalid intervalTemplet. IntervalID [" + this.intervalId + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMAttendanceManagerEx.cs", 222);
			}
			if (this.IntervalTemplet == null)
			{
				Log.ErrorAndExit("Null intervalTemplet. IntervalID [" + this.intervalId + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMAttendanceManagerEx.cs", 227);
			}
			if (bUTC)
			{
				return ServiceTime.ToUtcTime(this.IntervalTemplet.StartDate);
			}
			return this.IntervalTemplet.StartDate;
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x00062124 File Offset: 0x00060324
		public DateTime GetEndTime(bool bUTC)
		{
			if (!this.IntervalTemplet.IsValid)
			{
				Log.ErrorAndExit("Invalid intervalTemplet. IntervalID [" + this.intervalId + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMAttendanceManagerEx.cs", 242);
			}
			if (this.IntervalTemplet == null)
			{
				Log.ErrorAndExit("Null intervalTemplet. IntervalID [" + this.intervalId + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMAttendanceManagerEx.cs", 247);
			}
			if (bUTC)
			{
				return ServiceTime.ToUtcTime(this.intervalTemplet.EndDate);
			}
			return this.intervalTemplet.EndDate;
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x000621AD File Offset: 0x000603AD
		public string GetTabNameMain()
		{
			return NKCStringTable.GetString(this.TabNameMain, false);
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x000621BB File Offset: 0x000603BB
		public string GetTabNameSub()
		{
			return NKCStringTable.GetString(this.TabNameSub, false);
		}

		// Token: 0x0400101A RID: 4122
		private int idx;

		// Token: 0x0400101B RID: 4123
		private int tabID;

		// Token: 0x0400101C RID: 4124
		private NKM_ATTENDANCE_EVENT_TYPE eventType;

		// Token: 0x0400101D RID: 4125
		private int rewardGroup;

		// Token: 0x0400101E RID: 4126
		private string tabNameMain;

		// Token: 0x0400101F RID: 4127
		private string tabNameSub;

		// Token: 0x04001020 RID: 4128
		private string backgroundImage;

		// Token: 0x04001021 RID: 4129
		private int limitDayCount;

		// Token: 0x04001022 RID: 4130
		private int returnDayCount;

		// Token: 0x04001023 RID: 4131
		private string prefabName;

		// Token: 0x04001024 RID: 4132
		private string tabIconName;

		// Token: 0x04001025 RID: 4133
		private int maxAttCount;

		// Token: 0x04001026 RID: 4134
		private string intervalId;

		// Token: 0x04001027 RID: 4135
		private string m_OpenTag;

		// Token: 0x04001028 RID: 4136
		private NKMIntervalTemplet intervalTemplet = NKMIntervalTemplet.Invalid;
	}
}
