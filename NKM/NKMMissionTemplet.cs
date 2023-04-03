using System;
using System.Collections.Generic;
using System.Linq;
using Cs.Core.Util;
using Cs.Logging;
using NKC;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200043C RID: 1084
	public class NKMMissionTemplet : INKMTemplet
	{
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06001D58 RID: 7512 RVA: 0x00089D66 File Offset: 0x00087F66
		public List<MissionReward> m_MissionReward
		{
			get
			{
				return this.m_MissionRewardOpened;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06001D59 RID: 7513 RVA: 0x00089D6E File Offset: 0x00087F6E
		public bool EnableByInterval
		{
			get
			{
				return !this.intervalTemplet.IsValid || this.intervalTemplet.IsValidTime(ServiceTime.Now);
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06001D5A RID: 7514 RVA: 0x00089D8F File Offset: 0x00087F8F
		public int Key
		{
			get
			{
				return this.m_MissionID;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06001D5B RID: 7515 RVA: 0x00089D97 File Offset: 0x00087F97
		public bool IsRandomMission
		{
			get
			{
				return this.m_MissionPoolID != 0;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06001D5C RID: 7516 RVA: 0x00089DA2 File Offset: 0x00087FA2
		public bool EnableByTag
		{
			get
			{
				return NKMOpenTagManager.IsOpened(this.m_OpenTag);
			}
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x00089DB0 File Offset: 0x00087FB0
		public static NKMMissionTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 483))
			{
				return null;
			}
			NKMMissionTemplet nkmmissionTemplet = new NKMMissionTemplet();
			bool flag = true;
			cNKMLua.GetData("m_OpenTag", ref nkmmissionTemplet.m_OpenTag);
			cNKMLua.GetData("m_ResetCounterGroupId", ref nkmmissionTemplet.m_ResetCounterGroupId);
			nkmmissionTemplet.m_MissionCond.value1 = new List<int>();
			flag &= cNKMLua.GetData("m_MissionCounterGroupID", ref nkmmissionTemplet.m_GroupId);
			flag &= cNKMLua.GetData("m_MissionID", ref nkmmissionTemplet.m_MissionID);
			flag &= cNKMLua.GetData("m_MissionTabId", ref nkmmissionTemplet.m_MissionTabId);
			flag &= cNKMLua.GetData<NKM_MISSION_RESET_INTERVAL>("m_ResetInterval", ref nkmmissionTemplet.m_ResetInterval);
			flag &= cNKMLua.GetData("m_Times", ref nkmmissionTemplet.m_Times);
			cNKMLua.GetData("m_MissionIcon", ref nkmmissionTemplet.m_MissionIcon);
			cNKMLua.GetData("m_MissionTitle", ref nkmmissionTemplet.m_MissionTitle);
			cNKMLua.GetData("m_MissionDesc", ref nkmmissionTemplet.m_MissionDesc);
			cNKMLua.GetData("m_MissionTip", ref nkmmissionTemplet.m_MissionTip);
			cNKMLua.GetData("m_MissionRequire", ref nkmmissionTemplet.m_MissionRequire);
			cNKMLua.GetData<NKM_SHORTCUT_TYPE>("m_ShortCutType", ref nkmmissionTemplet.m_ShortCutType);
			cNKMLua.GetData("m_ShortCut", ref nkmmissionTemplet.m_ShortCut);
			cNKMLua.GetData<NKM_MISSION_COND>("m_MissionCond", ref nkmmissionTemplet.m_MissionCond.mission_cond);
			cNKMLua.GetData("m_MissionPoolID", ref nkmmissionTemplet.m_MissionPoolID);
			string text = null;
			cNKMLua.GetData("m_MissionValue", ref text);
			if (!string.IsNullOrEmpty(text))
			{
				nkmmissionTemplet.m_MissionCond.value1 = (from e in text.Split(new char[]
				{
					','
				})
				select int.Parse(e)).ToList<int>();
			}
			cNKMLua.GetData("m_MissionValue2", ref nkmmissionTemplet.m_MissionCond.value2);
			cNKMLua.GetData("m_MissionValue3", ref nkmmissionTemplet.m_MissionCond.value3);
			cNKMLua.GetData("m_ForceClearStage", ref nkmmissionTemplet.m_ForceClearStage);
			if (nkmmissionTemplet.m_MissionCond.mission_cond == NKM_MISSION_COND.USE_RESOURCE)
			{
				List<int> list = new List<int>();
				int num = 0;
				for (;;)
				{
					if (cNKMLua.OpenTable(string.Format("m_MissionValueChange{0}", num + 1)))
					{
						int num2 = 1;
						int item = 0;
						while (cNKMLua.GetData(num2, ref item))
						{
							list.Add(item);
							num2++;
						}
						cNKMLua.CloseTable();
					}
					if (list.Count != 2)
					{
						break;
					}
					nkmmissionTemplet.m_MissionChange.Add(new MissionChange
					{
						value = list[0],
						tiems = list[1]
					});
					list.Clear();
					num++;
				}
			}
			int num3 = 0;
			for (;;)
			{
				MissionReward missionReward = new MissionReward();
				cNKMLua.GetData(string.Format("m_RewardID_{0}", num3 + 1), ref missionReward.reward_id);
				cNKMLua.GetData(string.Format("m_RewardValue_{0}", num3 + 1), ref missionReward.reward_value);
				if (!cNKMLua.GetData<NKM_REWARD_TYPE>(string.Format("m_RewardType_{0}", num3 + 1), ref missionReward.reward_type))
				{
					break;
				}
				if (nkmmissionTemplet.m_MissionRewardOriginal == null)
				{
					nkmmissionTemplet.m_MissionRewardOriginal = new List<MissionReward>();
				}
				nkmmissionTemplet.m_MissionRewardOriginal.Add(missionReward);
				num3++;
			}
			cNKMLua.GetData("m_bResetCounterGroup", ref nkmmissionTemplet.m_bResetCounterGroup);
			cNKMLua.GetData("m_TrackingEvent", ref nkmmissionTemplet.m_TrackingEvent);
			cNKMLua.GetData("m_AdjustEvent", ref nkmmissionTemplet.m_AdjustEvent);
			if (!flag)
			{
				Log.Error(string.Format("NKMMissionTemplet Load failed. groupId:{0} missionId:{1}", nkmmissionTemplet.m_GroupId, nkmmissionTemplet.m_MissionID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 582);
				return null;
			}
			nkmmissionTemplet.m_TabTemplet = NKMMissionManager.GetMissionTabTemplet(nkmmissionTemplet.m_MissionTabId);
			if (nkmmissionTemplet.m_TabTemplet == null)
			{
				Log.Error(string.Format("NKMMissioNTemplet Load failed, tabTemplet is null, tab id: {0} missionId:{1}", nkmmissionTemplet.m_MissionTabId, nkmmissionTemplet.m_MissionID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 589);
				return null;
			}
			cNKMLua.GetData("m_DateStrID", ref nkmmissionTemplet.m_DateStrID);
			return nkmmissionTemplet;
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x0008A1B8 File Offset: 0x000883B8
		public void RecalculateMissionTemplets()
		{
			if (!this.EnableByTag)
			{
				return;
			}
			NKM_MISSION_TYPE missionType = this.m_TabTemplet.m_MissionType;
			if ((missionType == NKM_MISSION_TYPE.EVENT || missionType - NKM_MISSION_TYPE.BINGO <= 1) && this.m_ResetCounterGroupId && !string.IsNullOrWhiteSpace(this.m_TabTemplet.intervalId))
			{
				NKMMissionManager.AddCounterGroupResetData(this.m_GroupId, this);
			}
			this.m_MissionRewardOpened.Clear();
			foreach (MissionReward missionReward in this.m_MissionRewardOriginal)
			{
				if (NKMRewardTemplet.IsOpenedReward(missionReward.reward_type, missionReward.reward_id, false))
				{
					this.m_MissionRewardOpened.Add(missionReward);
				}
				else
				{
					NKMTempletError.Add(string.Format("[RecalculateMission] �̼ǿ� �ɸ� ������ �±׿� ���� ���ܵ˴ϴ�. missionId:{0} rewardId:{1} RewardType:{2}", this.m_MissionID, missionReward.reward_id, missionReward.reward_type), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 635);
				}
			}
			if (NKMUtil.IsServer)
			{
				this.JoinIntervalTemplet();
			}
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x0008A2C0 File Offset: 0x000884C0
		public void JoinIntervalTemplet()
		{
			if (string.IsNullOrEmpty(this.m_DateStrID))
			{
				return;
			}
			this.intervalTemplet = NKMIntervalTemplet.Find(this.m_DateStrID);
			if (this.intervalTemplet == null)
			{
				NKMTempletError.Add(string.Format("[MissionTemplet: {0}] ���͹��� ã�� �� �����ϴ�. m_DateStrID:{1}", this.Key, this.m_DateStrID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 655);
			}
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x0008A320 File Offset: 0x00088520
		public void Join()
		{
			this.RecalculateMissionTemplets();
			NKMOpenTagManager.AddRecalculateAction("Mission_" + this.Key.ToString(), new Action(this.RecalculateMissionTemplets));
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x0008A35C File Offset: 0x0008855C
		public void Validate()
		{
			if (NKMMissionManager.ContainsCounterGroupResetData(this.m_GroupId))
			{
				if (!this.m_ResetCounterGroupId)
				{
					Log.ErrorAndExit(string.Format("[MissionTemplet: {0}] ���ºҰ� �̼� ������ CounterGroupId: {1} MissionTabID: {2}", this.Key, this.m_GroupId, this.m_MissionTabId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 671);
				}
				if (string.IsNullOrWhiteSpace(this.m_TabTemplet.intervalId))
				{
					Log.ErrorAndExit(string.Format("[MissionTemplet: {0}] ��ȣ�� ���� �ð� CounterGroupId: {1} MissionTabID: {2}", this.Key, this.m_GroupId, this.m_MissionTabId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 676);
				}
			}
			if (this.m_MissionRequire == this.m_MissionID)
			{
				Log.ErrorAndExit(string.Format("[MissionTemplet: {0}] �ڱ� �ڽ��� �������� ����: {1} MissionTabID: {2}", this.Key, this.m_GroupId, this.m_MissionTabId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 682);
			}
			NKM_MISSION_TYPE missionType = this.m_TabTemplet.m_MissionType;
			if (missionType - NKM_MISSION_TYPE.TUTORIAL <= 3 && !string.IsNullOrEmpty(this.m_DateStrID))
			{
				NKMTempletError.Add(string.Format("[MissionTemplet: {0}] ���͹��� ���� �� ���� �̼��Դϴ�. MissionType:{1} MissionTabID: {2}", this.Key, this.m_TabTemplet.m_MissionType, this.m_MissionTabId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 693);
			}
			if (this.m_MissionChange.Count > 0)
			{
				if (this.m_MissionChange.Any((MissionChange e) => e.value <= 0))
				{
					NKMTempletError.Add(string.Format("[MissionTemplet:{0}] �̼ǰ� ��ȯ value ���� ���������� ���� ����", this.Key), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 702);
				}
				if (this.m_MissionChange.Any((MissionChange e) => e.tiems <= 0))
				{
					NKMTempletError.Add(string.Format("[MissionTemplet:{0}] �̼ǰ� ��ȯ times�� ���������� ���� ����", this.Key), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 707);
				}
				if ((from e in this.m_MissionChange
				group e by e.value).Any((IGrouping<int, MissionChange> e) => e.Count<MissionChange>() > 1))
				{
					NKMTempletError.Add(string.Format("[MissionTemplet:{0}] �̼ǰ� ��ȯ �����Ϳ� �ߺ��� value ���� ����", this.Key), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 712);
				}
			}
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x0008A5D2 File Offset: 0x000887D2
		public void PostJoin()
		{
			this.JoinIntervalTemplet();
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x0008A5DA File Offset: 0x000887DA
		public string GetTitle()
		{
			return NKCStringTable.GetString(this.m_MissionTitle, false);
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x0008A5E8 File Offset: 0x000887E8
		public string GetDesc()
		{
			return NKCStringTable.GetString(this.m_MissionDesc, false);
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x0008A5F6 File Offset: 0x000887F6
		public string GetTip()
		{
			if (string.IsNullOrEmpty(NKCStringTable.GetString(this.m_MissionTip, false)))
			{
				return this.m_MissionTip;
			}
			return NKCStringTable.GetString(this.m_MissionTip, false);
		}

		// Token: 0x04001DAB RID: 7595
		private string m_DateStrID = string.Empty;

		// Token: 0x04001DAC RID: 7596
		public int m_MissionTabId;

		// Token: 0x04001DAD RID: 7597
		public int m_GroupId;

		// Token: 0x04001DAE RID: 7598
		public int m_MissionID;

		// Token: 0x04001DAF RID: 7599
		public long m_Times;

		// Token: 0x04001DB0 RID: 7600
		public int m_MissionRequire;

		// Token: 0x04001DB1 RID: 7601
		public string m_MissionIcon = string.Empty;

		// Token: 0x04001DB2 RID: 7602
		public string m_MissionTitle = string.Empty;

		// Token: 0x04001DB3 RID: 7603
		public string m_MissionDesc = string.Empty;

		// Token: 0x04001DB4 RID: 7604
		public string m_MissionTip = string.Empty;

		// Token: 0x04001DB5 RID: 7605
		public string m_ShortCut = string.Empty;

		// Token: 0x04001DB6 RID: 7606
		public int m_ForceClearStage;

		// Token: 0x04001DB7 RID: 7607
		public NKM_SHORTCUT_TYPE m_ShortCutType;

		// Token: 0x04001DB8 RID: 7608
		public NKM_MISSION_RESET_INTERVAL m_ResetInterval = NKM_MISSION_RESET_INTERVAL.NONE;

		// Token: 0x04001DB9 RID: 7609
		public bool m_Enabled = true;

		// Token: 0x04001DBA RID: 7610
		public int m_MissionPoolID;

		// Token: 0x04001DBB RID: 7611
		public bool m_bResetCounterGroup;

		// Token: 0x04001DBC RID: 7612
		private string m_OpenTag;

		// Token: 0x04001DBD RID: 7613
		private bool m_ResetCounterGroupId = true;

		// Token: 0x04001DBE RID: 7614
		public string m_TrackingEvent = string.Empty;

		// Token: 0x04001DBF RID: 7615
		public string m_AdjustEvent = string.Empty;

		// Token: 0x04001DC0 RID: 7616
		public MissionCond m_MissionCond = new MissionCond();

		// Token: 0x04001DC1 RID: 7617
		public List<MissionChange> m_MissionChange = new List<MissionChange>();

		// Token: 0x04001DC2 RID: 7618
		public List<MissionReward> m_MissionRewardOpened = new List<MissionReward>();

		// Token: 0x04001DC3 RID: 7619
		public List<MissionReward> m_MissionRewardOriginal = new List<MissionReward>();

		// Token: 0x04001DC4 RID: 7620
		public NKMMissionTabTemplet m_TabTemplet;

		// Token: 0x04001DC5 RID: 7621
		public NKMIntervalTemplet intervalTemplet = NKMIntervalTemplet.Invalid;
	}
}
