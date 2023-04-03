using System;
using System.Collections.Generic;
using System.Linq;
using Cs.Core.Util;
using Cs.Logging;
using Cs.Protocol;
using Cs.Shared.Time;
using NKC;

namespace NKM
{
	// Token: 0x02000501 RID: 1281
	public class NKMUserMissionData : ISerializable
	{
		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060024A0 RID: 9376 RVA: 0x000BDCB6 File Offset: 0x000BBEB6
		public List<NKMMissionData> GetAllMissions
		{
			get
			{
				return this.dicMissions.Values.ToList<NKMMissionData>();
			}
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x000BDCC8 File Offset: 0x000BBEC8
		public NKMMissionData GetCompletedMissionData(int missionID)
		{
			NKMMissionData missionDataByMissionId = this.GetMissionDataByMissionId(missionID);
			if (missionDataByMissionId == null)
			{
				return null;
			}
			if (!missionDataByMissionId.isComplete)
			{
				NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(missionID);
				if (missionTemplet == null)
				{
					Log.Error(string.Format("Can not found MissionTemplet. missionId : {0}", missionID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUserMissionData.cs", 32);
					return null;
				}
				if (missionTemplet.m_MissionRequire == 0)
				{
					return null;
				}
			}
			return missionDataByMissionId;
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x000BDD1D File Offset: 0x000BBF1D
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.dicRefreshInfo);
			stream.PutOrGet<NKMMissionData>(ref this.dicMissions);
			stream.PutOrGet(ref this.achievePoint);
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x000BDD43 File Offset: 0x000BBF43
		public void SetAchievePoint(long point)
		{
			this.achievePoint = point;
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x000BDD4C File Offset: 0x000BBF4C
		public void AddAchievePoint(long addPoint)
		{
			this.achievePoint += addPoint;
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x000BDD5C File Offset: 0x000BBF5C
		public long GetAchiecePoint()
		{
			return this.achievePoint;
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x000BDD64 File Offset: 0x000BBF64
		public void AddMission(NKMMissionData missionData)
		{
			NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(missionData.mission_id);
			if (missionTemplet == null)
			{
				Log.Error(string.Format("Invalid MissionId. {0}", missionData.mission_id), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUserMissionData.cs", 62);
				return;
			}
			if (this.dicMissions.ContainsKey(missionData.group_id))
			{
				Log.Error(string.Format("MissionGroup Already exist. {0}", missionData.mission_id), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUserMissionData.cs", 68);
				return;
			}
			this.dicMissions.Add(missionData.group_id, missionData);
			if (missionTemplet.IsRandomMission && !this.dicRefreshInfo.ContainsKey(missionData.tabId))
			{
				this.dicRefreshInfo.Add(missionData.tabId, missionTemplet.m_TabTemplet.m_MissionRefreshFreeCount);
			}
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x000BDE20 File Offset: 0x000BC020
		public void RemoveMission(int groupId)
		{
			this.dicMissions.Remove(groupId);
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x000BDE30 File Offset: 0x000BC030
		public void RemoveAllRandomMissionInTab(int tabId)
		{
			foreach (NKMMissionData nkmmissionData in (from e in this.dicMissions.Values
			where e.tabId == tabId && NKMMissionManager.GetMissionTemplet(e.mission_id).IsRandomMission
			select e).ToList<NKMMissionData>())
			{
				this.dicMissions.Remove(nkmmissionData.group_id);
			}
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x000BDEB8 File Offset: 0x000BC0B8
		public NKMMissionData GetMissionData(NKMMissionTemplet templet)
		{
			if (templet == null)
			{
				return null;
			}
			NKMMissionData result;
			this.dicMissions.TryGetValue(templet.m_GroupId, out result);
			return result;
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x000BDEE0 File Offset: 0x000BC0E0
		public NKMMissionData GetMissionDataByMissionId(int missionId)
		{
			NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(missionId);
			if (missionTemplet == null)
			{
				return null;
			}
			NKMMissionData result;
			this.dicMissions.TryGetValue(missionTemplet.m_GroupId, out result);
			return result;
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x000BDF10 File Offset: 0x000BC110
		public NKMMissionData GetMissionDataByGroupId(int groupId)
		{
			NKMMissionData result;
			this.dicMissions.TryGetValue(groupId, out result);
			return result;
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x000BDF2D File Offset: 0x000BC12D
		public void SetRandomMissionRefreshCount(int tabId, int refreshCount)
		{
			if (!this.dicRefreshInfo.ContainsKey(tabId))
			{
				this.dicRefreshInfo.Add(tabId, refreshCount);
				return;
			}
			this.dicRefreshInfo[tabId] = refreshCount;
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x000BDF58 File Offset: 0x000BC158
		public int GetRandomMissionRefreshCount(int tabId)
		{
			int result;
			if (this.dicRefreshInfo.TryGetValue(tabId, out result))
			{
				return result;
			}
			return 0;
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x000BDF78 File Offset: 0x000BC178
		public bool DecreaseRandomMissionRefreshCount(int tabId)
		{
			int num;
			if (!this.dicRefreshInfo.TryGetValue(tabId, out num))
			{
				return false;
			}
			if (num <= 0)
			{
				return false;
			}
			this.dicRefreshInfo[tabId] = num - 1;
			return true;
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x000BDFB0 File Offset: 0x000BC1B0
		public List<NKMMissionData> GetAllMissionList(int tabId)
		{
			return (from e in this.dicMissions.Values
			where e.tabId == tabId
			select e).ToList<NKMMissionData>();
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x000BDFEC File Offset: 0x000BC1EC
		public List<NKMMissionData> GetRandomMissionList(int tabId)
		{
			IEnumerable<NKMMissionData> enumerable = from e in this.dicMissions.Values
			where e.tabId == tabId && NKMMissionManager.GetMissionTemplet(e.mission_id).IsRandomMission
			select e;
			if (enumerable == null)
			{
				return null;
			}
			return enumerable.ToList<NKMMissionData>();
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x000BE02D File Offset: 0x000BC22D
		public bool IsTabComplete(int tabId)
		{
			return this.completeFlag.Contains(tabId);
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x000BE03B File Offset: 0x000BC23B
		public void SetTabComplete(int tabId)
		{
			if (!this.completeFlag.Contains(tabId))
			{
				this.completeFlag.Add(tabId);
			}
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x000BE058 File Offset: 0x000BC258
		public bool HasRandomMission(int tabId)
		{
			List<NKMMissionData> allMissionList = this.GetAllMissionList(tabId);
			if (allMissionList.Count == 0)
			{
				return false;
			}
			IEnumerable<NKMMissionData> enumerable = from e in allMissionList
			where NKMMissionManager.GetMissionTemplet(e.mission_id).IsRandomMission
			select e;
			return enumerable == null || enumerable.Count<NKMMissionData>() != 0;
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x000BE0AA File Offset: 0x000BC2AA
		public bool WaitingForRandomMissionRefresh()
		{
			return this.m_dLastRandomMissionRefreshTime.Ticks > 0L && this.m_dLastRandomMissionRefreshTime < WeeklyReset.CalcLastReset(ServiceTime.Recent, DayOfWeek.Monday);
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x000BE0D3 File Offset: 0x000BC2D3
		public void OnRandomMissionRefresh()
		{
			this.m_dLastRandomMissionRefreshTime = WeeklyReset.CalcLastReset(ServiceTime.Recent, DayOfWeek.Monday);
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_LOBBY().RefreshUI();
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x000BE0F8 File Offset: 0x000BC2F8
		public bool CheckCompletableMission(NKMUserData user_data, bool bOnlyVisibleTab = true)
		{
			foreach (NKMMissionTabTemplet nkmmissionTabTemplet in NKMMissionManager.DicMissionTab.Values)
			{
				if (nkmmissionTabTemplet.m_MissionType != NKM_MISSION_TYPE.COMBINE_GUIDE_MISSION && this.CheckCompletableMission(user_data, nkmmissionTabTemplet.m_tabID, bOnlyVisibleTab))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x000BE16C File Offset: 0x000BC36C
		public bool CheckCompletableGuideMission(NKMUserData user_data, bool bOnlyVisibleTab = true)
		{
			foreach (NKMMissionTabTemplet nkmmissionTabTemplet in NKMMissionManager.DicMissionTab.Values)
			{
				if (nkmmissionTabTemplet.m_MissionType == NKM_MISSION_TYPE.COMBINE_GUIDE_MISSION && this.CheckCompletableMission(user_data, nkmmissionTabTemplet.m_tabID, bOnlyVisibleTab))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x000BE1E0 File Offset: 0x000BC3E0
		public bool CheckCompletableMission(NKMUserData user_data, int _NKM_MISSION_TAB_ID, bool bOnlyVisibleTab = false)
		{
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(_NKM_MISSION_TAB_ID);
			if (missionTabTemplet == null || (bOnlyVisibleTab && !missionTabTemplet.m_Visible))
			{
				return false;
			}
			if (!missionTabTemplet.EnableByTag)
			{
				return false;
			}
			if (!NKMMissionManager.CheckMissionTabUnlocked(missionTabTemplet.m_tabID, user_data))
			{
				return false;
			}
			foreach (NKMMissionTemplet templet in NKMMissionManager.GetMissionTempletListByType(_NKM_MISSION_TAB_ID))
			{
				NKMMissionData missionData = this.GetMissionData(templet);
				if (missionData != null && !missionData.isComplete && NKMMissionManager.CanComplete(templet, user_data, missionData) == NKM_ERROR_CODE.NEC_OK)
				{
					return true;
				}
			}
			if (missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.COMBINE_GUIDE_MISSION)
			{
				NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(missionTabTemplet.m_completeMissionID);
				if (missionTemplet != null && NKMMissionManager.GetMissionStateData(missionTemplet.m_MissionID).IsMissionCanClear)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x000BE2B8 File Offset: 0x000BC4B8
		public bool HasAlreadyCompleteMission(int missionTabID)
		{
			foreach (NKMMissionData nkmmissionData in NKCScenManager.CurrentUserData().m_MissionData.GetAllMissionList(missionTabID))
			{
				if (nkmmissionData != null && nkmmissionData.isComplete)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x000BE320 File Offset: 0x000BC520
		public Dictionary<int, NKMMissionData> GetAlreadyCompleteMission(int missionTabID)
		{
			Dictionary<int, NKMMissionData> dictionary = new Dictionary<int, NKMMissionData>();
			foreach (NKMMissionData nkmmissionData in NKCScenManager.CurrentUserData().m_MissionData.GetAllMissionList(missionTabID))
			{
				if (nkmmissionData != null && nkmmissionData.isComplete && !dictionary.ContainsKey(nkmmissionData.mission_id))
				{
					dictionary.Add(nkmmissionData.mission_id, nkmmissionData);
				}
			}
			return dictionary;
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x000BE3A4 File Offset: 0x000BC5A4
		public void AddOrUpdateMission(NKMMissionData missionData)
		{
			if (NKMMissionManager.GetMissionTemplet(missionData.mission_id) == null)
			{
				Log.Error(string.Format("Invalid MissionId. {0}", missionData.mission_id), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMMissionManagerEx.cs", 1356);
				return;
			}
			this.dicMissions[missionData.group_id] = missionData;
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x000BE3F8 File Offset: 0x000BC5F8
		public void SetCompleteMissionData(int missionID)
		{
			NKMMissionData missionDataByMissionId = this.GetMissionDataByMissionId(missionID);
			NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(missionID);
			if (missionTemplet == null)
			{
				return;
			}
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(missionTemplet.m_MissionTabId);
			if (missionDataByMissionId != null)
			{
				this.SetComplete(missionDataByMissionId);
			}
			else if (missionTabTemplet != null && missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.TUTORIAL)
			{
				this.SetComplete(new NKMMissionData
				{
					mission_id = missionID,
					group_id = missionTemplet.m_GroupId,
					tabId = missionTabTemplet.m_tabID,
					times = 1L
				});
			}
			if (NKMMissionManager.GetTrackingMissionTemplet() == missionTemplet)
			{
				NKMMissionManager.SetTrackingMissionTemplet(null);
			}
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x000BE480 File Offset: 0x000BC680
		private void SetComplete(NKMMissionData missionData)
		{
			NKMMissionTemplet nextMissionTemplet = NKMMissionManager.GetNextMissionTemplet(missionData);
			if (nextMissionTemplet != null)
			{
				if (nextMissionTemplet.m_GroupId == missionData.group_id)
				{
					missionData.mission_id = nextMissionTemplet.m_MissionID;
					missionData.isComplete = false;
				}
				else
				{
					if (nextMissionTemplet.m_GroupId != missionData.group_id && !NKMMissionManager.IsCumulativeCondition(nextMissionTemplet.m_MissionCond.mission_cond))
					{
						this.AddMission(new NKMMissionData
						{
							tabId = nextMissionTemplet.m_MissionTabId,
							mission_id = nextMissionTemplet.m_MissionID,
							group_id = nextMissionTemplet.m_GroupId,
							times = 0L,
							last_update_date = NKCSynchronizedTime.GetServerUTCTime(0.0).Ticks,
							isComplete = false
						});
					}
					missionData.isComplete = true;
				}
			}
			else
			{
				missionData.isComplete = true;
			}
			missionData.last_update_date = NKCSynchronizedTime.GetServerUTCTime(0.0).Ticks;
			if (NKMMissionManager.GetMissionTemplet(missionData.mission_id) != null && !this.dicMissions.ContainsKey(missionData.group_id))
			{
				this.AddMission(missionData);
			}
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x000BE590 File Offset: 0x000BC790
		public bool IsMissionCompleted(NKMMissionTemplet cNKMMissionTemplet)
		{
			if (cNKMMissionTemplet == null)
			{
				return false;
			}
			NKMMissionData missionData = this.GetMissionData(cNKMMissionTemplet);
			if (missionData != null)
			{
				if (NKMMissionManager.CheckCanReset(cNKMMissionTemplet.m_ResetInterval, missionData))
				{
					return false;
				}
				if (missionData.isComplete)
				{
					return true;
				}
				if (missionData.mission_id > cNKMMissionTemplet.m_MissionID)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400261C RID: 9756
		private long achievePoint;

		// Token: 0x0400261D RID: 9757
		private Dictionary<int, int> dicRefreshInfo = new Dictionary<int, int>();

		// Token: 0x0400261E RID: 9758
		private Dictionary<int, NKMMissionData> dicMissions = new Dictionary<int, NKMMissionData>();

		// Token: 0x0400261F RID: 9759
		private HashSet<int> completeFlag = new HashSet<int>();

		// Token: 0x04002620 RID: 9760
		private DateTime m_dLastRandomMissionRefreshTime;
	}
}
