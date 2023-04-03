using System;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.Common;
using ClientPacket.Event;
using Cs.Logging;
using NKC;
using NKC.Templet;
using NKM.Event;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200037A RID: 890
	public class NKMEventManager
	{
		// Token: 0x06001641 RID: 5697 RVA: 0x000598E8 File Offset: 0x00057AE8
		public static bool LoadFromLua()
		{
			NKMTempletContainer<NKMEventTabTemplet>.Load("AB_SCRIPT", "LUA_EVENT_TAB_TEMPLET", "EVENT_TAB_TEMPLET", new Func<NKMLua, NKMEventTabTemplet>(NKMEventTabTemplet.LoadFromLUA));
			NKMTempletContainer<NKMEventBingoTemplet>.Load("AB_SCRIPT", "LUA_EVENT_BINGO_TEMPLET", "EVENT_BINGO_TEMPLET", new Func<NKMLua, NKMEventBingoTemplet>(NKMEventBingoTemplet.LoadFromLUA));
			NKMEventManager.dicBingoTempletByMissionTab = NKMTempletContainer<NKMEventBingoTemplet>.Values.ToDictionary((NKMEventBingoTemplet e) => e.m_BingoMissionTabId, (NKMEventBingoTemplet e) => e);
			NKMEventManager.dicBingoRewardTemplet = NKMTempletLoader<NKMEventBingoRewardTemplet>.LoadGroup("AB_SCRIPT", "LUA_EVENT_BINGO_REWARD_TEMPLET", "EVENT_BINGO_REWARD_TEMPLET", new Func<NKMLua, NKMEventBingoRewardTemplet>(NKMEventBingoRewardTemplet.LoadFromLUA));
			if (NKMEventManager.dicBingoRewardTemplet == null)
			{
				Log.ErrorAndExit("NKMEventBingoRewardTemplet load failed", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Event/NKMEventManager.cs", 33);
				return false;
			}
			NKMTempletContainer<NKMEventBarTemplet>.Load("AB_SCRIPT", "LUA_EVENT_BAR_TEMPLET", "EVENT_BAR_TEMPLET", new Func<NKMLua, NKMEventBarTemplet>(NKMEventBarTemplet.LoadFromLua));
			return true;
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x000599E2 File Offset: 0x00057BE2
		public static NKMEventTabTemplet GetTabTemplet(int eventId)
		{
			return NKMTempletContainer<NKMEventTabTemplet>.Find(eventId);
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x000599EA File Offset: 0x00057BEA
		public static NKMEventBingoTemplet GetBingoTemplet(int eventId)
		{
			return NKMTempletContainer<NKMEventBingoTemplet>.Find(eventId);
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x000599F4 File Offset: 0x00057BF4
		public static List<NKMEventBingoRewardTemplet> GetBingoRewardTempletList(int eventID)
		{
			NKMEventBingoTemplet nkmeventBingoTemplet = NKMTempletContainer<NKMEventBingoTemplet>.Find(eventID);
			List<NKMEventBingoRewardTemplet> result;
			if (nkmeventBingoTemplet != null && NKMEventManager.dicBingoRewardTemplet.TryGetValue(nkmeventBingoTemplet.m_BingoCompletRewardGroupID, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x00059A24 File Offset: 0x00057C24
		public static NKMEventBingoRewardTemplet GetBingoRewardTemplet(int groupId, BingoCompleteType completeType, int completeTypeValue)
		{
			List<NKMEventBingoRewardTemplet> list;
			if (!NKMEventManager.dicBingoRewardTemplet.TryGetValue(groupId, out list))
			{
				return null;
			}
			return list.Find((NKMEventBingoRewardTemplet e) => e.m_BingoCompletType.Equals(completeType) && e.m_BingoCompletTypeValue.Equals(completeTypeValue));
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x00059A68 File Offset: 0x00057C68
		public static NKMEventBingoRewardTemplet GetBingoRewardTemplet(int groupId, int rewardIndex)
		{
			List<NKMEventBingoRewardTemplet> list;
			if (!NKMEventManager.dicBingoRewardTemplet.TryGetValue(groupId, out list))
			{
				return null;
			}
			return list.Find((NKMEventBingoRewardTemplet e) => e.ZeroBaseTileIndex == rewardIndex);
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x00059AA5 File Offset: 0x00057CA5
		public static List<NKMEventTabTemplet> GetBingoEvents()
		{
			return (from e in NKMTempletContainer<NKMEventTabTemplet>.Values
			where e.m_EventType == NKM_EVENT_TYPE.BINGO && e.EnableByTag
			select e).ToList<NKMEventTabTemplet>();
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x00059AD8 File Offset: 0x00057CD8
		public static void CheckValidation()
		{
			foreach (List<NKMEventBingoRewardTemplet> list in NKMEventManager.dicBingoRewardTemplet.Values)
			{
				foreach (NKMEventBingoRewardTemplet nkmeventBingoRewardTemplet in list)
				{
					nkmeventBingoRewardTemplet.Validate();
				}
			}
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x00059B60 File Offset: 0x00057D60
		public static bool CheckRedDot()
		{
			using (IEnumerator<NKMEventTabTemplet> enumerator = NKMTempletContainer<NKMEventTabTemplet>.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (NKMEventManager.CheckRedDot(enumerator.Current))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x00059BB4 File Offset: 0x00057DB4
		public static bool CheckRedDot(NKMEventTabTemplet templet)
		{
			if (templet == null)
			{
				return false;
			}
			if (!templet.IsAvailable)
			{
				return false;
			}
			switch (templet.m_EventType)
			{
			case NKM_EVENT_TYPE.BINGO:
			{
				int eventID = templet.m_EventID;
				if (NKMEventManager.CheckRedDotBingoSingle(eventID) || NKMEventManager.CheckRedDotBingoSet(eventID) || NKMEventManager.CheckRedDotBingoMission(eventID))
				{
					return true;
				}
				break;
			}
			case NKM_EVENT_TYPE.MISSION:
			case NKM_EVENT_TYPE.ONTIME:
			case NKM_EVENT_TYPE.RACE:
			case NKM_EVENT_TYPE.MISSION_ROW:
			{
				NKCEventMissionTemplet nkceventMissionTemplet = NKCEventMissionTemplet.Find(templet.m_EventID);
				if (nkceventMissionTemplet != null)
				{
					NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
					foreach (int nkm_MISSION_TAB_ID in nkceventMissionTemplet.m_lstMissionTab)
					{
						if (nkmuserData.m_MissionData.CheckCompletableMission(nkmuserData, nkm_MISSION_TAB_ID, false))
						{
							return true;
						}
					}
					if (nkmuserData.m_MissionData.CheckCompletableMission(nkmuserData, nkceventMissionTemplet.m_SpecialMissionTab, false))
					{
						return true;
					}
				}
				break;
			}
			case NKM_EVENT_TYPE.KAKAOEMOTE:
			{
				NKCEventMissionTemplet nkceventMissionTemplet2 = NKCEventMissionTemplet.Find(templet.m_EventID);
				NKMUserData nkmuserData2 = NKCScenManager.CurrentUserData();
				if (nkmuserData2.kakaoMissionData != null)
				{
					switch (nkmuserData2.kakaoMissionData.state)
					{
					case KakaoMissionState.Initialized:
					case KakaoMissionState.Registered:
					case KakaoMissionState.Sent:
					case KakaoMissionState.Failed:
					case KakaoMissionState.Flopped:
						return true;
					}
				}
				if (nkceventMissionTemplet2 != null)
				{
					foreach (int nkm_MISSION_TAB_ID2 in nkceventMissionTemplet2.m_lstMissionTab)
					{
						if (nkmuserData2.m_MissionData.CheckCompletableMission(nkmuserData2, nkm_MISSION_TAB_ID2, false))
						{
							return true;
						}
					}
					if (nkmuserData2.m_MissionData.CheckCompletableMission(nkmuserData2, nkceventMissionTemplet2.m_SpecialMissionTab, false))
					{
						return true;
					}
				}
				break;
			}
			case NKM_EVENT_TYPE.KILLCOUNT:
			{
				int num = 0;
				int num2 = 0;
				long num3 = 0L;
				NKMKillCountData killCountData = NKCKillCountManager.GetKillCountData(templet.m_EventID);
				if (killCountData != null)
				{
					num = killCountData.serverCompleteStep;
					num2 = killCountData.userCompleteStep;
					num3 = killCountData.killCount;
				}
				NKMKillCountTemplet nkmkillCountTemplet = NKMKillCountTemplet.Find(templet.m_EventID);
				if (nkmkillCountTemplet == null)
				{
					return false;
				}
				int maxServerStep = nkmkillCountTemplet.GetMaxServerStep();
				if (num < maxServerStep)
				{
					NKMKillCountStepTemplet nkmkillCountStepTemplet = null;
					nkmkillCountTemplet.TryGetServerStep(num + 1, out nkmkillCountStepTemplet);
					NKMServerKillCountData killCountServerData = NKCKillCountManager.GetKillCountServerData(templet.m_EventID);
					if (nkmkillCountStepTemplet != null && killCountServerData != null && (long)nkmkillCountStepTemplet.KillCount <= killCountServerData.killCount)
					{
						return true;
					}
				}
				int maxUserStep = nkmkillCountTemplet.GetMaxUserStep();
				if (num2 < maxUserStep)
				{
					NKMKillCountStepTemplet nkmkillCountStepTemplet2 = null;
					nkmkillCountTemplet.TryGetUserStep(num2 + 1, out nkmkillCountStepTemplet2);
					if (nkmkillCountStepTemplet2 != null && (long)nkmkillCountStepTemplet2.KillCount <= num3)
					{
						return true;
					}
				}
				break;
			}
			case NKM_EVENT_TYPE.BAR:
			{
				NKMUserData nkmuserData3 = NKCScenManager.CurrentUserData();
				if (nkmuserData3 != null)
				{
					long countMiscItem = nkmuserData3.m_InventoryData.GetCountMiscItem(NKCEventBarManager.DailyCocktailItemID);
					NKMEventBarTemplet nkmeventBarTemplet = NKMEventBarTemplet.Find(NKCEventBarManager.DailyCocktailItemID);
					if (nkmeventBarTemplet != null)
					{
						if (countMiscItem >= (long)nkmeventBarTemplet.DeliveryValue && NKCEventBarManager.RemainDeliveryLimitValue > 0)
						{
							return true;
						}
						NKCEventMissionTemplet nkceventMissionTemplet3 = NKCEventMissionTemplet.Find(templet.m_EventID);
						if (nkceventMissionTemplet3 != null)
						{
							int count = nkceventMissionTemplet3.m_lstMissionTab.Count;
							for (int i = 0; i < count; i++)
							{
								if (nkmuserData3.m_MissionData.CheckCompletableMission(nkmuserData3, nkceventMissionTemplet3.m_lstMissionTab[i], false))
								{
									return true;
								}
							}
						}
					}
				}
				break;
			}
			}
			return false;
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x00059EF4 File Offset: 0x000580F4
		public static void SetEventInfo(EventInfo eventInfo)
		{
			NKMEventManager.dicEventBingo.Clear();
			foreach (BingoInfo bingoInfo in eventInfo.bingoInfo)
			{
				NKMEventManager.AddBingo(bingoInfo.eventId, bingoInfo);
			}
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x00059F58 File Offset: 0x00058158
		public static bool IsEventCompleted(NKMEventTabTemplet tabTemplet)
		{
			if (tabTemplet == null)
			{
				return false;
			}
			switch (tabTemplet.m_EventType)
			{
			case NKM_EVENT_TYPE.BINGO:
			{
				NKMEventBingoTemplet bingoTemplet = NKMEventManager.GetBingoTemplet(tabTemplet.m_EventID);
				if (bingoTemplet == null)
				{
					return false;
				}
				EventBingo bingoData = NKMEventManager.GetBingoData(bingoTemplet.m_EventID);
				if (bingoData != null && bingoData.Completed())
				{
					return true;
				}
				break;
			}
			case NKM_EVENT_TYPE.MISSION:
			case NKM_EVENT_TYPE.ONTIME:
			case NKM_EVENT_TYPE.RACE:
			case NKM_EVENT_TYPE.MISSION_ROW:
			{
				NKCEventMissionTemplet nkceventMissionTemplet = NKCEventMissionTemplet.Find(tabTemplet.m_EventID);
				if (nkceventMissionTemplet != null)
				{
					NKMUserData cNKMUserData = NKCScenManager.CurrentUserData();
					foreach (int reqValue in nkceventMissionTemplet.m_lstMissionTab)
					{
						UnlockInfo unlockInfo = new UnlockInfo(STAGE_UNLOCK_REQ_TYPE.SURT_MISSION_TAB_ALL_CLEAR, reqValue);
						if (!NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, unlockInfo, true))
						{
							return false;
						}
					}
					UnlockInfo unlockInfo2 = new UnlockInfo(STAGE_UNLOCK_REQ_TYPE.SURT_MISSION_TAB_ALL_CLEAR, nkceventMissionTemplet.m_SpecialMissionTab);
					return NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, unlockInfo2, true);
				}
				break;
			}
			case NKM_EVENT_TYPE.KAKAOEMOTE:
			{
				if (NKCScenManager.CurrentUserData().kakaoMissionData != null)
				{
					switch (NKCScenManager.CurrentUserData().kakaoMissionData.state)
					{
					default:
						return false;
					case KakaoMissionState.Confirmed:
					case KakaoMissionState.NotEnoughBudget:
					case KakaoMissionState.OutOfDate:
						break;
					}
				}
				NKCEventMissionTemplet nkceventMissionTemplet2 = NKCEventMissionTemplet.Find(tabTemplet.m_EventID);
				if (nkceventMissionTemplet2 != null)
				{
					NKMUserData cNKMUserData2 = NKCScenManager.CurrentUserData();
					foreach (int reqValue2 in nkceventMissionTemplet2.m_lstMissionTab)
					{
						UnlockInfo unlockInfo3 = new UnlockInfo(STAGE_UNLOCK_REQ_TYPE.SURT_MISSION_TAB_ALL_CLEAR, reqValue2);
						if (!NKMContentUnlockManager.IsContentUnlocked(cNKMUserData2, unlockInfo3, true))
						{
							return false;
						}
					}
					UnlockInfo unlockInfo4 = new UnlockInfo(STAGE_UNLOCK_REQ_TYPE.SURT_MISSION_TAB_ALL_CLEAR, nkceventMissionTemplet2.m_SpecialMissionTab);
					return NKMContentUnlockManager.IsContentUnlocked(cNKMUserData2, unlockInfo4, true);
				}
				break;
			}
			}
			return false;
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x0005A144 File Offset: 0x00058344
		private static void AddBingo(int eventID, BingoInfo bingoInfo)
		{
			if (!NKMEventManager.dicEventBingo.ContainsKey(eventID))
			{
				NKMEventManager.dicEventBingo.Add(eventID, new EventBingo(eventID, bingoInfo));
			}
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x0005A168 File Offset: 0x00058368
		public static EventBingo GetBingoData(int eventID)
		{
			EventBingo result;
			if (NKMEventManager.dicEventBingo.TryGetValue(eventID, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x0005A188 File Offset: 0x00058388
		public static bool IsReceiveableBingoReward(int eventID, int rewardIndex)
		{
			EventBingo eventBingo;
			if (!NKMEventManager.dicEventBingo.TryGetValue(eventID, out eventBingo))
			{
				return false;
			}
			if (eventBingo.m_bingoInfo.rewardList.Contains(rewardIndex))
			{
				return false;
			}
			NKMEventBingoRewardTemplet bingoRewardTemplet = NKMEventManager.GetBingoRewardTemplet(eventBingo.m_bingoTemplet.m_BingoCompletRewardGroupID, rewardIndex);
			if (bingoRewardTemplet == null)
			{
				return false;
			}
			List<int> bingoLine = eventBingo.GetBingoLine();
			if (bingoRewardTemplet.m_BingoCompletType == BingoCompleteType.LINE_SINGLE)
			{
				return bingoLine.Contains(bingoRewardTemplet.m_BingoCompletTypeValue - 1);
			}
			return bingoRewardTemplet.m_BingoCompletType == BingoCompleteType.LINE_SET && bingoLine.Count >= bingoRewardTemplet.m_BingoCompletTypeValue;
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x0005A210 File Offset: 0x00058410
		public static bool CheckRedDotBingoSingle(int eventID)
		{
			EventBingo bingoData = NKMEventManager.GetBingoData(eventID);
			if (bingoData == null)
			{
				return false;
			}
			List<int> bingoLine = bingoData.GetBingoLine();
			for (int i = 0; i < bingoLine.Count; i++)
			{
				int num = bingoLine[i];
				NKMEventBingoRewardTemplet bingoRewardTemplet = NKMEventManager.GetBingoRewardTemplet(bingoData.m_bingoTemplet.m_BingoCompletRewardGroupID, BingoCompleteType.LINE_SINGLE, num + 1);
				if (bingoRewardTemplet != null && NKMEventManager.IsReceiveableBingoReward(eventID, bingoRewardTemplet.ZeroBaseTileIndex))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x0005A278 File Offset: 0x00058478
		public static bool CheckRedDotBingoSet(int eventID)
		{
			EventBingo bingoData = NKMEventManager.GetBingoData(eventID);
			if (bingoData == null)
			{
				return false;
			}
			List<int> bingoLine = bingoData.GetBingoLine();
			foreach (NKMEventBingoRewardTemplet nkmeventBingoRewardTemplet in NKMEventManager.GetBingoRewardTempletList(eventID))
			{
				if (nkmeventBingoRewardTemplet.m_BingoCompletType == BingoCompleteType.LINE_SET && bingoLine.Count >= nkmeventBingoRewardTemplet.m_BingoCompletTypeValue && !bingoData.m_bingoInfo.rewardList.Contains(nkmeventBingoRewardTemplet.ZeroBaseTileIndex))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x0005A310 File Offset: 0x00058510
		public static bool CheckRedDotBingoMission(int eventID)
		{
			NKMEventBingoTemplet bingoTemplet = NKMEventManager.GetBingoTemplet(eventID);
			if (bingoTemplet == null)
			{
				return false;
			}
			List<NKMMissionTemplet> missionTempletListByType = NKMMissionManager.GetMissionTempletListByType(bingoTemplet.m_BingoMissionTabId);
			if (missionTempletListByType == null)
			{
				return false;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			for (int i = 0; i < missionTempletListByType.Count; i++)
			{
				NKMMissionTemplet templet = missionTempletListByType[i];
				NKMMissionData missionData = nkmuserData.m_MissionData.GetMissionData(templet);
				if (missionData != null && !missionData.isComplete && NKMMissionManager.CanComplete(templet, nkmuserData, missionData) == NKM_ERROR_CODE.NEC_OK)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x0005A388 File Offset: 0x00058588
		public static NKMEventBingoRewardTemplet GetBingoLastRewardTemplet(int eventID)
		{
			List<NKMEventBingoRewardTemplet> list = NKMEventManager.GetBingoRewardTempletList(eventID);
			if (list == null)
			{
				return null;
			}
			list = list.FindAll((NKMEventBingoRewardTemplet v) => v.m_BingoCompletType == BingoCompleteType.LINE_SET);
			if (list.Count == 0)
			{
				return null;
			}
			return list[list.Count - 1];
		}

		// Token: 0x04000EFC RID: 3836
		private static Dictionary<int, NKMEventBingoTemplet> dicBingoTempletByMissionTab = new Dictionary<int, NKMEventBingoTemplet>();

		// Token: 0x04000EFD RID: 3837
		private static Dictionary<int, List<NKMEventBingoRewardTemplet>> dicBingoRewardTemplet = new Dictionary<int, List<NKMEventBingoRewardTemplet>>();

		// Token: 0x04000EFE RID: 3838
		private static Dictionary<int, EventBingo> dicEventBingo = new Dictionary<int, EventBingo>();
	}
}
