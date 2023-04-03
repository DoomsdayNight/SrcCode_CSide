using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Core.Util;
using Cs.Logging;
using NKC;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200043D RID: 1085
	public class NKMMissionTabTemplet : INKMTemplet, INKMTempletEx
	{
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06001D67 RID: 7527 RVA: 0x0008A6D7 File Offset: 0x000888D7
		public int Key
		{
			get
			{
				return this.m_tabID;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06001D68 RID: 7528 RVA: 0x0008A6DF File Offset: 0x000888DF
		public DateTime m_startTime
		{
			get
			{
				return this.intervalTemplet.StartDate;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06001D69 RID: 7529 RVA: 0x0008A6EC File Offset: 0x000888EC
		public DateTime m_endTime
		{
			get
			{
				return this.intervalTemplet.EndDate;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06001D6A RID: 7530 RVA: 0x0008A6F9 File Offset: 0x000888F9
		public DateTime m_startTimeUtc
		{
			get
			{
				return ServiceTime.ToUtcTime(this.intervalTemplet.StartDate);
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06001D6B RID: 7531 RVA: 0x0008A70B File Offset: 0x0008890B
		public DateTime m_endTimeUtc
		{
			get
			{
				return ServiceTime.ToUtcTime(this.intervalTemplet.EndDate);
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06001D6C RID: 7532 RVA: 0x0008A71D File Offset: 0x0008891D
		public bool HasDateLimit
		{
			get
			{
				return this.intervalTemplet.IsValid;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06001D6D RID: 7533 RVA: 0x0008A72A File Offset: 0x0008892A
		public bool IsReturningMission
		{
			get
			{
				return this.m_ReturningUserType > ReturningUserType.None;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06001D6E RID: 7534 RVA: 0x0008A735 File Offset: 0x00088935
		public bool IsNewbieMission
		{
			get
			{
				return this.m_NewbieDate > 0;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06001D6F RID: 7535 RVA: 0x0008A740 File Offset: 0x00088940
		public bool EnableByTag
		{
			get
			{
				return NKMOpenTagManager.IsOpened(this.m_OpenTag);
			}
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x0008A750 File Offset: 0x00088950
		public static NKMMissionTabTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 767))
			{
				return null;
			}
			NKMMissionTabTemplet nkmmissionTabTemplet = new NKMMissionTabTemplet();
			bool flag = true & cNKMLua.GetData("m_TabID", ref nkmmissionTabTemplet.m_tabID) & cNKMLua.GetData<NKM_MISSION_TYPE>("m_MissionTab", ref nkmmissionTabTemplet.m_MissionType);
			cNKMLua.GetData("m_OpenTag", ref nkmmissionTabTemplet.m_OpenTag);
			cNKMLua.GetData("m_MissionTabDesc", ref nkmmissionTabTemplet.m_MissionTabDesc);
			cNKMLua.GetData("m_MissionTabIconName", ref nkmmissionTabTemplet.m_MissionTabIconName);
			cNKMLua.GetData("m_MainUnitStrID", ref nkmmissionTabTemplet.m_MainUnitStrID);
			cNKMLua.GetData("m_SlotBannerName", ref nkmmissionTabTemplet.m_SlotBannerName);
			cNKMLua.GetData("m_LobbyIconDisplayBool", ref nkmmissionTabTemplet.m_LobbyIconDisplayBool);
			cNKMLua.GetData("m_LobbyIconName", ref nkmmissionTabTemplet.m_LobbyIconName);
			cNKMLua.GetData("m_LobbyIconDesc", ref nkmmissionTabTemplet.m_LobbyIconDesc);
			cNKMLua.GetData("m_MissionTotalPointBool", ref nkmmissionTabTemplet.m_MissionTotalPointBool);
			cNKMLua.GetData("m_MissionTotalPointID", ref nkmmissionTabTemplet.m_MissionTotalPointID);
			cNKMLua.GetData("m_Visible", ref nkmmissionTabTemplet.m_Visible);
			cNKMLua.GetData("m_VisibleWhenLocked", ref nkmmissionTabTemplet.m_VisibleWhenLocked);
			cNKMLua.GetData("m_firstMissionID", ref nkmmissionTabTemplet.m_firstMissionID);
			cNKMLua.GetData("m_completeMissionID", ref nkmmissionTabTemplet.m_completeMissionID);
			cNKMLua.GetData("m_NewbieDate", ref nkmmissionTabTemplet.m_NewbieDate);
			cNKMLua.GetData<ReturningUserType>("m_ReturningUserType", ref nkmmissionTabTemplet.m_ReturningUserType);
			cNKMLua.GetData("m_MissionPoolID", ref nkmmissionTabTemplet.m_MissionPoolID);
			cNKMLua.GetData("m_MissionDisplayCount", ref nkmmissionTabTemplet.m_MissionDisplayCount);
			cNKMLua.GetData("m_MissionRefreshFreeCount", ref nkmmissionTabTemplet.m_MissionRefreshFreeCount);
			cNKMLua.GetData("m_MissionRefreshReqItemID", ref nkmmissionTabTemplet.m_MissionRefreshReqItemID);
			cNKMLua.GetData("m_MissionRefreshReqItemValue", ref nkmmissionTabTemplet.m_MissionRefreshReqItemValue);
			cNKMLua.GetData("m_DateStrID", ref nkmmissionTabTemplet.intervalId);
			cNKMLua.GetData("m_MissionBannerImage", ref nkmmissionTabTemplet.m_MissionBannerImage);
			nkmmissionTabTemplet.m_UnlockInfo = UnlockInfo.LoadFromLua2(cNKMLua);
			if (!flag)
			{
				Log.Error(string.Format("NKMMissionTabTemplet Load - {0}", nkmmissionTabTemplet.m_tabID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 809);
				return null;
			}
			return nkmmissionTabTemplet;
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x0008A96F File Offset: 0x00088B6F
		public void Join()
		{
			if (NKMUtil.IsServer)
			{
				this.JoinIntervalTemplet();
			}
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x0008A980 File Offset: 0x00088B80
		public void JoinIntervalTemplet()
		{
			if (!string.IsNullOrEmpty(this.intervalId))
			{
				this.intervalTemplet = NKMIntervalTemplet.Find(this.intervalId);
				if (this.intervalTemplet == null)
				{
					this.intervalTemplet = NKMIntervalTemplet.Unuseable;
					Log.ErrorAndExit(string.Format("[Mission:{0}] �߸��� interval id:{1}", this.Key, this.intervalId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 830);
					return;
				}
				if (this.intervalTemplet.IsRepeatDate)
				{
					Log.ErrorAndExit(string.Format("[Mission:{0}] �ݺ� �Ⱓ���� ��� �Ұ�. id:{1}", this.Key, this.intervalId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 836);
					return;
				}
			}
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x0008AA24 File Offset: 0x00088C24
		public void Validate()
		{
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x0008AA26 File Offset: 0x00088C26
		public void PostJoin()
		{
			this.JoinIntervalTemplet();
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x0008AA2E File Offset: 0x00088C2E
		public string GetDesc()
		{
			return NKCStringTable.GetString(this.m_MissionTabDesc, false);
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x0008AA3C File Offset: 0x00088C3C
		public string GetLobbyIconDesc()
		{
			return NKCStringTable.GetString(this.m_LobbyIconDesc, false);
		}

		// Token: 0x04001DC6 RID: 7622
		private NKMIntervalTemplet intervalTemplet = NKMIntervalTemplet.Invalid;

		// Token: 0x04001DC7 RID: 7623
		public int m_tabID;

		// Token: 0x04001DC8 RID: 7624
		public NKM_MISSION_TYPE m_MissionType;

		// Token: 0x04001DC9 RID: 7625
		private string m_OpenTag;

		// Token: 0x04001DCA RID: 7626
		public string m_MissionTabDesc = string.Empty;

		// Token: 0x04001DCB RID: 7627
		public string m_MissionTabIconName = string.Empty;

		// Token: 0x04001DCC RID: 7628
		public string m_MainUnitStrID = string.Empty;

		// Token: 0x04001DCD RID: 7629
		public string m_SlotBannerName = string.Empty;

		// Token: 0x04001DCE RID: 7630
		public bool m_LobbyIconDisplayBool;

		// Token: 0x04001DCF RID: 7631
		public string m_LobbyIconName = string.Empty;

		// Token: 0x04001DD0 RID: 7632
		public string m_LobbyIconDesc = string.Empty;

		// Token: 0x04001DD1 RID: 7633
		public bool m_MissionTotalPointBool;

		// Token: 0x04001DD2 RID: 7634
		public int m_MissionTotalPointID;

		// Token: 0x04001DD3 RID: 7635
		public int m_NewbieDate;

		// Token: 0x04001DD4 RID: 7636
		public ReturningUserType m_ReturningUserType;

		// Token: 0x04001DD5 RID: 7637
		public int m_MissionPoolID;

		// Token: 0x04001DD6 RID: 7638
		public int m_MissionDisplayCount;

		// Token: 0x04001DD7 RID: 7639
		public int m_MissionRefreshFreeCount;

		// Token: 0x04001DD8 RID: 7640
		public int m_MissionRefreshReqItemID;

		// Token: 0x04001DD9 RID: 7641
		public int m_MissionRefreshReqItemValue;

		// Token: 0x04001DDA RID: 7642
		public string intervalId;

		// Token: 0x04001DDB RID: 7643
		public string m_MissionBannerImage;

		// Token: 0x04001DDC RID: 7644
		public bool m_Visible = true;

		// Token: 0x04001DDD RID: 7645
		public bool m_VisibleWhenLocked;

		// Token: 0x04001DDE RID: 7646
		public List<UnlockInfo> m_UnlockInfo = new List<UnlockInfo>();

		// Token: 0x04001DDF RID: 7647
		public int m_firstMissionID;

		// Token: 0x04001DE0 RID: 7648
		public int m_completeMissionID;
	}
}
