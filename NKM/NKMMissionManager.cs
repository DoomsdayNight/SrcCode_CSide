using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cs.Logging;
using NKC;
using NKC.UI;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200043E RID: 1086
	public static class NKMMissionManager
	{
		// Token: 0x06001D78 RID: 7544 RVA: 0x0008AAC0 File Offset: 0x00088CC0
		public static bool LoadFromLUA(string filename, string tabFileName)
		{
			NKMMissionManager.DicMissionTab = NKMTempletLoader.LoadDictionary<NKMMissionTabTemplet>("AB_SCRIPT", tabFileName, "MissionTabTemplet", new Func<NKMLua, NKMMissionTabTemplet>(NKMMissionTabTemplet.LoadFromLUA));
			if (NKMMissionManager.DicMissionTab == null)
			{
				return false;
			}
			NKMMissionManager.DicMissionId = NKMTempletLoader.LoadDictionary<NKMMissionTemplet>("AB_SCRIPT", filename, "m_dicNKMMissionTempletByID", new Func<NKMLua, NKMMissionTemplet>(NKMMissionTemplet.LoadFromLUA));
			if (NKMMissionManager.DicMissionId == null)
			{
				return false;
			}
			foreach (NKMMissionTemplet templet in NKMMissionManager.DicMissionId.Values)
			{
				NKMMissionManager.MakeDicPoolList(templet);
				NKMMissionManager.MakeDicMissionTab(templet);
			}
			foreach (NKMMissionTabTemplet nkmmissionTabTemplet in NKMMissionManager.DicMissionTab.Values)
			{
				nkmmissionTabTemplet.Join();
			}
			if (NKMMissionManager.DicMissionTab.Values.Any((NKMMissionTabTemplet e) => e.m_MissionType == NKM_MISSION_TYPE.GUILD))
			{
				NKMMissionManager.GuildTabId = NKMMissionManager.DicMissionTab.Values.First((NKMMissionTabTemplet e) => e.m_MissionType == NKM_MISSION_TYPE.GUILD).m_tabID;
			}
			return true;
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x0008AC1C File Offset: 0x00088E1C
		public static NKMMissionTemplet GetMissionTemplet(int mission_id)
		{
			if (NKMMissionManager.DicMissionId == null)
			{
				return null;
			}
			NKMMissionTemplet result;
			if (!NKMMissionManager.DicMissionId.TryGetValue(mission_id, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x0008AC44 File Offset: 0x00088E44
		public static List<NKMMissionTemplet> GetMissionTempletListByType(int missionTabId)
		{
			List<NKMMissionTemplet> list = new List<NKMMissionTemplet>();
			foreach (KeyValuePair<int, NKMMissionTemplet> keyValuePair in NKMMissionManager.DicMissionId)
			{
				NKMMissionTemplet value = keyValuePair.Value;
				if (value != null && value.EnableByTag && value.EnableByInterval && value.m_MissionTabId == missionTabId)
				{
					list.Add(value);
				}
			}
			return list;
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x0008ACC4 File Offset: 0x00088EC4
		public static List<NKMMissionTemplet> GetMissionTempletListByGroupID(int counterGroupId)
		{
			List<NKMMissionTemplet> list = new List<NKMMissionTemplet>();
			foreach (NKMMissionTemplet nkmmissionTemplet in NKMMissionManager.DicMissionId.Values)
			{
				if (nkmmissionTemplet != null && nkmmissionTemplet.m_GroupId == counterGroupId)
				{
					list.Add(nkmmissionTemplet);
				}
			}
			return list;
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x0008AD30 File Offset: 0x00088F30
		public static List<NKMMissionTemplet> GetMissionTempletListByTabID(int missionTabId)
		{
			List<NKMMissionTemplet> list = new List<NKMMissionTemplet>();
			foreach (NKMMissionTemplet nkmmissionTemplet in NKMMissionManager.DicMissionId.Values)
			{
				if (nkmmissionTemplet != null && nkmmissionTemplet.m_MissionTabId == missionTabId)
				{
					list.Add(nkmmissionTemplet);
				}
			}
			return list;
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x0008AD9C File Offset: 0x00088F9C
		public static bool ContainsCounterGroupResetData(int counterGroupId)
		{
			return NKMMissionManager.DicCounterGroupResetDate != null && NKMMissionManager.DicCounterGroupResetDate.ContainsKey(counterGroupId);
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x0008ADB4 File Offset: 0x00088FB4
		public static void AddCounterGroupResetData(int counterGroupId, NKMMissionTemplet missionTemplet)
		{
			if (NKMMissionManager.DicCounterGroupResetDate == null)
			{
				NKMMissionManager.DicCounterGroupResetDate = new Dictionary<int, DateTime>();
			}
			DateTime t;
			if (NKMMissionManager.DicCounterGroupResetDate.TryGetValue(counterGroupId, out t))
			{
				if (t < missionTemplet.m_TabTemplet.m_endTime)
				{
					NKMMissionManager.DicCounterGroupResetDate[counterGroupId] = missionTemplet.m_TabTemplet.m_endTime;
					return;
				}
			}
			else
			{
				NKMMissionManager.DicCounterGroupResetDate.Add(counterGroupId, missionTemplet.m_TabTemplet.m_endTime);
			}
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x0008AE24 File Offset: 0x00089024
		public static void CheckValidation()
		{
			MissionGroupIdValidator missionGroupIdValidator = new MissionGroupIdValidator();
			using (Dictionary<int, NKMMissionTemplet>.ValueCollection.Enumerator enumerator = NKMMissionManager.DicMissionId.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					NKMMissionTemplet templet = enumerator.Current;
					if (templet.m_Times < 0L)
					{
						Log.ErrorAndExit(string.Format("[NKMMissionManager] Invalid m_Times value. m_Times:{0} < 0", templet.m_Times), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 1018);
					}
					if (templet.m_MissionCond.mission_cond == NKM_MISSION_COND.NONE)
					{
						Log.ErrorAndExit(string.Format("Invalid Mission CondData. MissionId:{0}", templet.m_MissionID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 1023);
						return;
					}
					templet.Join();
					missionGroupIdValidator.Add(templet);
					foreach (MissionReward missionReward in templet.m_MissionReward)
					{
						if (missionReward.reward_type != NKM_REWARD_TYPE.RT_NONE && missionReward.reward_id > 0)
						{
							if (!NKMRewardTemplet.IsValidReward(missionReward.reward_type, missionReward.reward_id) || missionReward.reward_value <= 0)
							{
								Log.ErrorAndExit(string.Format("[MissionTemplet] �̼� ���� ������ �������� ���� m_MissionID : {0}, reward_type : {1}, reward_id : {2}, reward_value : {3}", new object[]
								{
									templet.m_MissionID,
									missionReward.reward_type,
									missionReward.reward_id,
									missionReward.reward_value
								}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 1038);
							}
							NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(templet.m_MissionTabId);
							if (missionTabTemplet == null)
							{
								Log.ErrorAndExit(string.Format("[MissionTemplet] �� ���ø��� �������� ����. missionId:{0} tabId:{1}", templet.m_MissionID, templet.m_MissionTabId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 1044);
								return;
							}
							if (missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.BINGO && missionReward.reward_type != NKM_REWARD_TYPE.RT_BINGO_TILE)
							{
								Log.ErrorAndExit(string.Format("[MissionTemplet] �̼� �� ������ Ÿ�ϸ� �Է� ���� m_MissionID : {0}, reward_type : {1}", templet.m_MissionID, missionReward.reward_type), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 1052);
							}
						}
					}
					if (templet.m_MissionRequire > 0 && !NKMMissionManager.DicMissionId.ContainsKey(templet.m_MissionRequire))
					{
						Log.ErrorAndExit(string.Format("[MissionTemplet] ���� Ŭ���� �̼� ������ �������� ���� m_MissionID : {0}, m_MissionRequire : {1}", templet.m_MissionID, templet.m_MissionRequire), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 1062);
					}
					IEnumerable<NKM_MISSION_COND> enumerable = (from e in NKMMissionManager.DicMissionId.Values
					where e.m_GroupId == templet.m_GroupId
					select e.m_MissionCond.mission_cond).Distinct<NKM_MISSION_COND>();
					if (enumerable.Count<NKM_MISSION_COND>() > 1)
					{
						StringBuilder builder = NKMString.GetBuilder();
						foreach (NKM_MISSION_COND nkm_MISSION_COND in enumerable)
						{
							builder.Append(nkm_MISSION_COND.ToString() + ", ");
						}
						Log.ErrorAndExit(string.Format("[MissionTemplet] ���� �׷��� �������� �̼� ������ �ٸ� �����Ͱ� ������ m_GroupId : {0}, MissionConds : {1}", templet.m_GroupId, builder.ToString()), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 1075);
					}
					NKM_MISSION_COND mission_cond = templet.m_MissionCond.mission_cond;
					if (mission_cond <= NKM_MISSION_COND.ACHIEVEMENT_CLEARED)
					{
						if (mission_cond - NKM_MISSION_COND.USE_RESOURCE > 1)
						{
							if (mission_cond != NKM_MISSION_COND.COLLECT_EQUIP_ENCHANT_LEVEL)
							{
								if (mission_cond == NKM_MISSION_COND.ACHIEVEMENT_CLEARED)
								{
									if (templet.m_MissionCond.value1.Count > 0 && templet.m_Times > (long)templet.m_MissionCond.value1.Count)
									{
										Log.ErrorAndExit(string.Format("[NKMMissionManager]ACHIEVEMENT_CLEARED �̼ǿ� value�� ���� �ִµ�, times ���� mission_value�� �������� Ŭ�� ����. missionId:{0} m_Times:{1} valueCount:{2} ", templet.m_MissionID, templet.m_Times, templet.m_MissionCond.value1.Count), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 1110);
									}
								}
							}
							else if (templet.m_MissionCond.value1.Count != 1)
							{
								Log.ErrorAndExit(string.Format("[MissionTemplet] COLLECT_EQUIP_ENCHANT_LEVEL�� values ������ 1�̾�� ��. missionId:{0}", templet.m_MissionID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 1101);
							}
						}
						else if (templet.m_MissionCond.value1.Count <= 0)
						{
							Log.ErrorAndExit(string.Format("[MissionTemplet] USE_RESOURCE, COLLECT_RESOURCE�� values ī��Ʈ�� 0 ���ϸ� �ȵ�. missionId:{0}", templet.m_MissionID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 1084);
						}
					}
					else if (mission_cond <= NKM_MISSION_COND.LOGIN_TIMES)
					{
						if (mission_cond != NKM_MISSION_COND.DONATE_MISSION_ITEM)
						{
							if (mission_cond == NKM_MISSION_COND.LOGIN_TIMES)
							{
								if (templet.m_MissionCond.value1.Count == 0 || templet.m_MissionCond.value2 == 0 || templet.m_Times != 1L || templet.m_ResetInterval != NKM_MISSION_RESET_INTERVAL.DAILY)
								{
									Log.ErrorAndExit(string.Format("LOGIN_TIMES ������ �Է� ����. {0},{1},{2},{3},{4}", new object[]
									{
										templet.m_MissionID,
										templet.m_MissionCond.value1.Count,
										templet.m_MissionCond.value2,
										templet.m_Times,
										templet.m_ResetInterval
									}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 1144);
									return;
								}
								if (templet.m_MissionCond.value1[0] > templet.m_MissionCond.value2)
								{
									Log.ErrorAndExit(string.Format("LOGIN_TIMES ������ �Է� ����. ���۽ð��� ����ð����� ŭ {0},{1},{2},{3},{4}", new object[]
									{
										templet.m_MissionID,
										templet.m_MissionCond.value1.Count,
										templet.m_MissionCond.value2,
										templet.m_Times,
										templet.m_ResetInterval
									}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 1150);
									return;
								}
								if (templet.m_MissionCond.value1[0] < 0 || 24 < templet.m_MissionCond.value1[0])
								{
									Log.ErrorAndExit(string.Format("LOGIN_TIMES ������ �Է� ����. �ð� �������� �߸��� {0},{1},{2},{3},{4}", new object[]
									{
										templet.m_MissionID,
										templet.m_MissionCond.value1.Count,
										templet.m_MissionCond.value2,
										templet.m_Times,
										templet.m_ResetInterval
									}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 1156);
									return;
								}
							}
						}
						else
						{
							if (templet.m_MissionCond.value1.Count != 1)
							{
								Log.ErrorAndExit(string.Format("[MissionTemplet] DONATE_MISSION_ITEM, values ī��Ʈ�� 1�� �̾����. mission id: {0}", templet.m_MissionID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 1118);
								return;
							}
							if (templet.m_Times <= 0L)
							{
								Log.ErrorAndExit(string.Format("[MissionTemplet] DONATE_MISSION_ITEM, m_Times�� ���� 0���� �۰ų� �����ϴ�. mission id: {0}, value: {1}", templet.m_MissionID, templet.m_Times), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 1124);
								return;
							}
							int num = templet.m_MissionCond.value1[0];
							if (NKMItemManager.GetItemMiscTempletByID(num) == null)
							{
								Log.ErrorAndExit(string.Format("[MissionTemplet] ITEM_MISC_ACCEPT, values 0���� ���� misc item�� �����ϴ�. mission id: {0}, misc item id: {1}", templet.m_MissionID, num), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 1131);
								return;
							}
						}
					}
					else if (mission_cond != NKM_MISSION_COND.USE_ETERNIUM)
					{
						if (mission_cond - NKM_MISSION_COND.PVP_NPC_CLEAR <= 1)
						{
							if (templet.m_MissionCond.value1.Count > 1)
							{
								NKMTempletError.Add(string.Format("[NKMMissionManager] NPC PVP �̼ǿ� Index ������ 1�� �̻� �������. missionId:{0} cond:{1} valueData:{2}", templet.m_MissionID, templet.m_MissionCond.mission_cond, string.Join<int>(",", templet.m_MissionCond.value1)), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 1166);
							}
						}
					}
					else
					{
						if (templet.m_Times <= 0L)
						{
							Log.ErrorAndExit(string.Format("[MissionTemplet] USE_ETERNIUM, times ���� 0 ���ϸ� �ȵ�. missionId:{0}", templet.m_MissionID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 1090);
						}
						if (templet.m_MissionCond.value1.Count > 0)
						{
							Log.ErrorAndExit(string.Format("[MissionTemplet] USE_ETERNIUM, values�� ������ �ȵ�. missionId:{0} valuesCount:{1}", templet.m_MissionID, templet.m_MissionCond.value1.Count), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 1095);
						}
					}
				}
			}
			foreach (NKMMissionTemplet nkmmissionTemplet in NKMMissionManager.DicMissionId.Values)
			{
				nkmmissionTemplet.Validate();
			}
			missionGroupIdValidator.Validate();
			foreach (NKMMissionTabTemplet nkmmissionTabTemplet in NKMMissionManager.DicMissionTab.Values)
			{
				foreach (UnlockInfo unlockInfo in nkmmissionTabTemplet.m_UnlockInfo)
				{
					if (!NKMContentUnlockManager.IsValidMissionUnlockType(unlockInfo))
					{
						Log.ErrorAndExit(string.Format("[MissionTabTemplet] ��ȿ���� ���� ������ ���� ������  m_MissionTab : {0}, reqType : {1}, reqValue : {2}, reqStr : {3}, reqDateTime : {4}", new object[]
						{
							nkmmissionTabTemplet.m_MissionType,
							unlockInfo.eReqType,
							unlockInfo.reqValue,
							unlockInfo.reqValueStr,
							unlockInfo.reqDateTime
						}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 1189);
					}
					if (unlockInfo.eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_REGISTER_DATE && unlockInfo.reqDateTime == DateTime.MinValue)
					{
						Log.ErrorAndExit(string.Format("[NKMMissionManager] Invalid Data. m_UnlockReqType must have m_UnlockReqValueStr value. m_tabId:{0} m_UnlockInfo.eReqType:{1}, m_UnlockReqValueStr:{2}", nkmmissionTabTemplet.m_tabID, unlockInfo.eReqType, unlockInfo.reqDateTime), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 1194);
					}
				}
				if (nkmmissionTabTemplet.m_tabID == 0)
				{
					Log.ErrorAndExit("[MissionTabTemplet] Tab Templet Id �� 0 �Դϴ�.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 1200);
				}
				if ((nkmmissionTabTemplet.m_startTime.Ticks > 0L || nkmmissionTabTemplet.m_endTime.Ticks > 0L) && nkmmissionTabTemplet.m_startTime.Ticks > nkmmissionTabTemplet.m_endTime.Ticks)
				{
					Log.ErrorAndExit(string.Format("[MissionTabTemplet] ���۽ð��� ����ð����� �������  m_MissionTab : {0}, m_startTime : {1}, m_endTime : {2}", nkmmissionTabTemplet.m_MissionType, nkmmissionTabTemplet.m_startTime, nkmmissionTabTemplet.m_endTime), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 1206);
				}
			}
			if ((from e in NKMMissionManager.DicMissionTab.Values
			where e.m_MissionType == NKM_MISSION_TYPE.GUILD
			select e).Count<NKMMissionTabTemplet>() > 1)
			{
				Log.ErrorAndExit("[NKMMissionManager]��� �̼� ���� 1���� ���� �Ͽ��� ��.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 1212);
			}
			foreach (NKMMissionTabTemplet nkmmissionTabTemplet2 in NKMMissionManager.DicMissionTab.Values)
			{
				if (nkmmissionTabTemplet2.m_MissionPoolID > 0)
				{
					if (!NKMMissionManager.DicPoolList.ContainsKey(nkmmissionTabTemplet2.m_MissionPoolID))
					{
						Log.ErrorAndExit(string.Format("MissionPool is not exist - m_MissionPoolID : {0}", nkmmissionTabTemplet2.m_MissionPoolID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 1221);
					}
					if (nkmmissionTabTemplet2.m_MissionDisplayCount > NKMMissionManager.DicPoolList[nkmmissionTabTemplet2.m_MissionPoolID].Count)
					{
						Log.ErrorAndExit(string.Format("MissionPoolSize is less than DisplayCount - m_MissionPoolID : {0}", nkmmissionTabTemplet2.m_MissionPoolID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMissionManager.cs", 1226);
					}
				}
			}
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x0008BB78 File Offset: 0x00089D78
		public static NKMMissionTabTemplet GetMissionTabTemplet(int tabID)
		{
			NKMMissionTabTemplet result = null;
			if (!NKMMissionManager.DicMissionTab.TryGetValue(tabID, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x0008BB9C File Offset: 0x00089D9C
		public static NKMMissionTabTemplet GetMissionTabTemplet(NKM_MISSION_TYPE missionType)
		{
			NKMMissionTabTemplet result = null;
			foreach (KeyValuePair<int, NKMMissionTabTemplet> keyValuePair in NKMMissionManager.DicMissionTab)
			{
				if (keyValuePair.Value.m_MissionType == missionType)
				{
					return keyValuePair.Value;
				}
			}
			return result;
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x0008BC08 File Offset: 0x00089E08
		public static bool IsCumulativeCondition(NKM_MISSION_COND cond)
		{
			switch (cond)
			{
			case NKM_MISSION_COND.ACCOUNT_LEVEL:
			case NKM_MISSION_COND.COLLECT_UNIT_LEVEL:
			case NKM_MISSION_COND.COLLECT_EQUIP_ENCHANT_LEVEL:
			case NKM_MISSION_COND.COLLECT_SHIP_LEVEL:
			case NKM_MISSION_COND.WARFARE_CLEARED:
			case NKM_MISSION_COND.DUNGEON_CLEARED:
			case NKM_MISSION_COND.PHASE_CLEAR:
			case NKM_MISSION_COND.WORLDMAP_BRANCH_NUMBER:
			case NKM_MISSION_COND.JUST_OPEN:
			case NKM_MISSION_COND.COLLECT_SHIP_GET:
			case NKM_MISSION_COND.COLLECT_SHIP_GET_LEVEL:
			case NKM_MISSION_COND.COLLECT_SHIP_UPGRADE:
			case NKM_MISSION_COND.COUNTER_CASE_OPENED:
			case NKM_MISSION_COND.PVP_HIGHEST_TIER_ASYNC:
			case NKM_MISSION_COND.ACHIEVEMENT_CLEARED:
			case NKM_MISSION_COND.DIVE_HIGHEST_CLEARED:
			case NKM_MISSION_COND.PVP_HIGHEST_TIER_CLEARED:
			case NKM_MISSION_COND.MISSION_EVENT_TAB_CLEAR:
			case NKM_MISSION_COND.MENTORING_MISSION_CLEARED:
			case NKM_MISSION_COND.UNIT_POWER_HIGHEST:
			case NKM_MISSION_COND.DUNGEON_SQUAD_UNIT_POWER_HIGHEST:
			case NKM_MISSION_COND.WARFARE_SQUAD_UNIT_POWER_HIGHEST:
			case NKM_MISSION_COND.PVP_TOTAL_CLEAR_BOTH:
			case NKM_MISSION_COND.PVP_TOTAL_CLEAR_ASYNC:
			case NKM_MISSION_COND.PVP_TOTAL_CLEAR_RANK:
				return false;
			case NKM_MISSION_COND.LOGIN_DAYS:
			case NKM_MISSION_COND.USE_RESOURCE:
			case NKM_MISSION_COND.COLLECT_RESOURCE:
			case NKM_MISSION_COND.COLLECT_UNIT:
			case NKM_MISSION_COND.COLLECT_UNIT_GRADE:
			case NKM_MISSION_COND.COLLECT_EQUIP:
			case NKM_MISSION_COND.COLLECT_EQUIP_GRADE:
			case NKM_MISSION_COND.COLLECT_SHIP:
			case NKM_MISSION_COND.COLLECT_SHIP_GRADE:
			case NKM_MISSION_COND.COLLECT_MEDAL_GOLD_MAINSTREAM:
			case NKM_MISSION_COND.COLLECT_MEDAL_SILVER_MAINSTREAM:
			case NKM_MISSION_COND.COLLECT_MEDAL_BRONZE_MAINSTREAM:
			case NKM_MISSION_COND.UNIT_CONTRACT:
			case NKM_MISSION_COND.UNIT_DISMISS:
			case NKM_MISSION_COND.UNIT_ENCHANT:
			case NKM_MISSION_COND.UNIT_TRAINING:
			case NKM_MISSION_COND.UNIT_LIMITBREAK:
			case NKM_MISSION_COND.UNIT_LIMITBREAK_CONFLUENCE:
			case NKM_MISSION_COND.EQUIP_ENCHANT:
			case NKM_MISSION_COND.SHIP_MAKE:
			case NKM_MISSION_COND.SHIP_UPGRADE:
			case NKM_MISSION_COND.SHIP_LEVELUP:
			case NKM_MISSION_COND.SHIP_LIMITBREAK:
			case NKM_MISSION_COND.WARFARE_CLEAR:
			case NKM_MISSION_COND.WARFARE_DEFEATED:
			case NKM_MISSION_COND.DUNGEON_CLEAR:
			case NKM_MISSION_COND.DUNGEON_DEFEATED:
			case NKM_MISSION_COND.DAILY_DUNGEON_PLAY:
			case NKM_MISSION_COND.DIVE_CLEAR:
			case NKM_MISSION_COND.DIVE_PLAY_RECORD:
			case NKM_MISSION_COND.RAID_FIND:
			case NKM_MISSION_COND.RAID_PLAY:
			case NKM_MISSION_COND.RAID_HIGH_SCORE:
			case NKM_MISSION_COND.PVP_PLAY_RANK:
			case NKM_MISSION_COND.PVP_CLEAR_RANK:
			case NKM_MISSION_COND.PVP_CLEAR_FRIENDLY:
			case NKM_MISSION_COND.PVP_DEFEATED_RANK:
			case NKM_MISSION_COND.PVP_DEFEATED_FRIENDLY:
			case NKM_MISSION_COND.PVP_HIGHEST_TIER:
			case NKM_MISSION_COND.WORLDMAP_BRANCH_TOTAL_LEVEL:
			case NKM_MISSION_COND.WORLDMAP_MISSION_TOTAL_LEVEL:
			case NKM_MISSION_COND.WORLDMAP_MISSION_TOTAL_TIME:
			case NKM_MISSION_COND.WORLDMAP_MISSION_CLEAR:
			case NKM_MISSION_COND.SHOP_BUY:
			case NKM_MISSION_COND.HAVE_FRIEND:
			case NKM_MISSION_COND.TUTORIAL:
			case NKM_MISSION_COND.MISSION_CLEAR_DAILY:
			case NKM_MISSION_COND.MISSION_CLEAR_WEEKLY:
			case NKM_MISSION_COND.MISSION_CLEAR_MONTHLY:
			case NKM_MISSION_COND.COUNTER_CASE_OPEN:
			case NKM_MISSION_COND.NEGOTIATION_TRY:
			case NKM_MISSION_COND.NEGOTIATION_SUCCESS:
			case NKM_MISSION_COND.NEGOTIATION_FAIL:
			case NKM_MISSION_COND.EQUIP_MAKE:
			case NKM_MISSION_COND.EQUIP_TUNING:
			case NKM_MISSION_COND.ACHIEVEMENT_CLEAR:
			case NKM_MISSION_COND.HAVE_DAILY_POINT:
			case NKM_MISSION_COND.HAVE_WEEKLY_POINT:
			case NKM_MISSION_COND.PVP_PLAY_ASYNC:
			case NKM_MISSION_COND.PVP_CLEAR_ASYNC:
			case NKM_MISSION_COND.PVP_DEFEATED_ASYNC:
			case NKM_MISSION_COND.EPISODE_PLAY_COUNT:
			case NKM_MISSION_COND.EPISODE_PLAY_COUNT_HARD:
			case NKM_MISSION_COND.DUNGEON_CLEAR_PERFECT:
			case NKM_MISSION_COND.DUNGEON_CLEARED_PERFECT:
			case NKM_MISSION_COND.WARFARE_CLEAR_PERFECT:
			case NKM_MISSION_COND.WARFARE_CLEARED_PERFECT:
			case NKM_MISSION_COND.PHASE_CLEAR_PERFECT:
			case NKM_MISSION_COND.PVP_LEAGUE_POINT_RANK:
			case NKM_MISSION_COND.PVP_LEAGUE_POINT_ASYNC:
			case NKM_MISSION_COND.UNIT_LEVEL_CHECK:
			case NKM_MISSION_COND.SHIP_LEVEL_CHECK:
			case NKM_MISSION_COND.PALACE_CLEAR:
			case NKM_MISSION_COND.SUPPORT_PLATOON_USED:
			case NKM_MISSION_COND.GUILD_DONATE:
			case NKM_MISSION_COND.GUILD_ATTENDANCE:
			case NKM_MISSION_COND.FIERCE_RANK_TOP:
			case NKM_MISSION_COND.EC_SUPPLY_CLEAR:
			case NKM_MISSION_COND.DONATE_MISSION_ITEM:
			case NKM_MISSION_COND.UNIT_GROWTH_GET:
			case NKM_MISSION_COND.UNIT_GROWTH_LEVEL:
			case NKM_MISSION_COND.UNIT_GROWTH_LIMIT:
			case NKM_MISSION_COND.UNIT_GROWTH_SKILL_LEVEL_3:
			case NKM_MISSION_COND.UNIT_GROWTH_SKILL_LEVEL_MAX:
			case NKM_MISSION_COND.UNIT_GROWTH_LOYALTY:
			case NKM_MISSION_COND.UNIT_GROWTH_PERMANENT:
			case NKM_MISSION_COND.UNIT_USE_CLEAR_DUNGEON:
			case NKM_MISSION_COND.UNIT_USE_CLEAR_DAILY:
			case NKM_MISSION_COND.UNIT_USE_CLEAR_SUPPLY:
			case NKM_MISSION_COND.UNIT_USE_CLEAR_PHASE:
			case NKM_MISSION_COND.UNIT_USE_PLAY_PVP_ASYNC:
			case NKM_MISSION_COND.UNIT_USE_PLAY_PVP_RANK:
			case NKM_MISSION_COND.UNIT_USE_GO:
			case NKM_MISSION_COND.UNIT_USE_TIMEATK_SUCCESS:
			case NKM_MISSION_COND.UNIT_USE_GO_SUCCESS:
			case NKM_MISSION_COND.UNIT_USE_LIFE_SUCCESS:
			case NKM_MISSION_COND.UNIT_USE_LOBBY:
			case NKM_MISSION_COND.UNIT_USE_WORLDMAP:
			case NKM_MISSION_COND.UNIT_USE_GO_UNIT_ID:
			case NKM_MISSION_COND.LOGIN_TIMES:
			case NKM_MISSION_COND.RAID_HELP_PUSH:
			case NKM_MISSION_COND.RAID_FIND_LEVEL_HIGH:
			case NKM_MISSION_COND.RAID_PLAY_LEVEL_HIGH:
			case NKM_MISSION_COND.RAID_REWARD_FRIEND:
			case NKM_MISSION_COND.RAID_STAGE_CLEARED:
			case NKM_MISSION_COND.RAID_CLEAR_MVP:
			case NKM_MISSION_COND.RAID_ASSIST_COUNT:
			case NKM_MISSION_COND.WORLDMAP_REWARD_SUCCESS:
			case NKM_MISSION_COND.COLLECT_EQUIP_TIER:
			case NKM_MISSION_COND.PVP_PLAY_LEAGUE:
			case NKM_MISSION_COND.COLLECT_ITEM_INTERIOR:
			case NKM_MISSION_COND.COLLECT_OFFICE_ROOM:
			case NKM_MISSION_COND.GET_OFFICE_HEART:
			case NKM_MISSION_COND.GIVE_NAME_CARD:
			case NKM_MISSION_COND.GIVE_NAME_CARD_ALL:
			case NKM_MISSION_COND.TRY_EXTRACT_UNIT:
			case NKM_MISSION_COND.USE_ETERNIUM:
			case NKM_MISSION_COND.PVP_NPC_CLEAR_ASYNC:
			case NKM_MISSION_COND.PVP_NPC_PLAY_ASYNC:
			case NKM_MISSION_COND.PVP_NPC_CLEAR:
			case NKM_MISSION_COND.PVP_NPC_PLAY:
			case NKM_MISSION_COND.EVENT_COLLECT_UNIT_COLLECT:
			case NKM_MISSION_COND.EVENT_COLLECT_UNIT_COUNT:
			case NKM_MISSION_COND.PVP_CLEAR_BOTH:
			case NKM_MISSION_COND.PVP_PLAY_BOTH:
				return true;
			default:
				throw new InvalidOperationException(string.Format("[IsCumulativeCondition] unspecified mission type, value: {0}", cond));
			}
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x0008BE8E File Offset: 0x0008A08E
		public static bool CanCompleteCumulative(NKMMissionTemplet templet, NKMMissionData missionData)
		{
			return missionData.times >= templet.m_Times;
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x0008BEA4 File Offset: 0x0008A0A4
		private static void MakeDicPoolList(NKMMissionTemplet templet)
		{
			List<int> list;
			if (!NKMMissionManager.DicPoolList.TryGetValue(templet.m_MissionPoolID, out list))
			{
				list = new List<int>();
				NKMMissionManager.DicPoolList.Add(templet.m_MissionPoolID, list);
			}
			list.Add(templet.m_MissionID);
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x0008BEE8 File Offset: 0x0008A0E8
		private static void MakeDicMissionTab(NKMMissionTemplet templet)
		{
			HashSet<int> hashSet;
			if (!NKMMissionManager.DicGroupListByTab.TryGetValue(templet.m_MissionTabId, out hashSet))
			{
				hashSet = new HashSet<int>();
				NKMMissionManager.DicGroupListByTab.Add(templet.m_MissionTabId, hashSet);
			}
			if (!hashSet.Contains(templet.m_GroupId))
			{
				hashSet.Add(templet.m_GroupId);
			}
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x0008BF3C File Offset: 0x0008A13C
		public static void PostJoin()
		{
			foreach (NKMMissionTabTemplet nkmmissionTabTemplet in NKMMissionManager.DicMissionTab.Values)
			{
				nkmmissionTabTemplet.PostJoin();
			}
			foreach (NKMMissionTemplet nkmmissionTemplet in NKMMissionManager.DicMissionId.Values)
			{
				nkmmissionTemplet.PostJoin();
			}
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x0008BFD4 File Offset: 0x0008A1D4
		public static NKMMissionTemplet GetNextMissionTemplet(NKMMissionData missionData)
		{
			foreach (NKMMissionTemplet nkmmissionTemplet in NKMMissionManager.DicMissionId.Values)
			{
				if (nkmmissionTemplet.m_MissionRequire == missionData.mission_id)
				{
					return nkmmissionTemplet;
				}
			}
			return null;
		}

		// Token: 0x06001D88 RID: 7560 RVA: 0x0008C03C File Offset: 0x0008A23C
		public static NKMMissionTabTemplet GetNextMissionTabTemplet(int missionTabID)
		{
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(missionTabID);
			if (missionTabTemplet != null)
			{
				foreach (NKMMissionTemplet nkmmissionTemplet in NKMMissionManager.DicMissionId.Values)
				{
					if (nkmmissionTemplet.m_MissionRequire == missionTabTemplet.m_completeMissionID)
					{
						return NKMMissionManager.GetMissionTabTemplet(nkmmissionTemplet.m_MissionTabId);
					}
				}
			}
			return null;
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x0008C0B8 File Offset: 0x0008A2B8
		public static bool CheckMissionTabUnlocked(int tabID, NKMUserData userData)
		{
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(tabID);
			return missionTabTemplet == null || (NKMContentUnlockManager.IsContentUnlocked(userData, missionTabTemplet.m_UnlockInfo, false) && !NKMMissionManager.IsMissionTabExpired(missionTabTemplet, userData));
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x0008C0EE File Offset: 0x0008A2EE
		public static bool IsMissionTabExpired(int tabID, NKMUserData userData)
		{
			return NKMMissionManager.IsMissionTabExpired(NKMMissionManager.GetMissionTabTemplet(tabID), userData);
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x0008C0FC File Offset: 0x0008A2FC
		public static bool IsMissionTabExpired(NKMMissionTabTemplet tabTemplet, NKMUserData userData)
		{
			if (tabTemplet == null || userData == null)
			{
				return true;
			}
			if (tabTemplet.HasDateLimit)
			{
				DateTime utcCurrent;
				if (tabTemplet.IsReturningMission)
				{
					if (!userData.IsReturnUser(tabTemplet.m_ReturningUserType))
					{
						return true;
					}
					utcCurrent = userData.GetReturnStartDate(tabTemplet.m_ReturningUserType);
				}
				else if (tabTemplet.IsNewbieMission)
				{
					if (!userData.IsNewbieUser(tabTemplet.m_NewbieDate))
					{
						return true;
					}
					utcCurrent = userData.m_NKMUserDateData.m_RegisterTime;
				}
				else
				{
					utcCurrent = NKCSynchronizedTime.GetServerUTCTime(0.0);
				}
				if (!NKCSynchronizedTime.IsEventTime(utcCurrent, tabTemplet.intervalId, tabTemplet.m_startTimeUtc, tabTemplet.m_endTimeUtc))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x0008C194 File Offset: 0x0008A394
		public static bool TryGetMissionTabExpireUtcTime(NKMMissionTabTemplet tabTemplet, NKMUserData userData, out DateTime endUtcTime)
		{
			if (userData == null)
			{
				endUtcTime = DateTime.MinValue;
				return false;
			}
			if (!tabTemplet.HasDateLimit)
			{
				endUtcTime = DateTime.MinValue;
				return false;
			}
			if (tabTemplet.IsNewbieMission)
			{
				endUtcTime = userData.GetNewbieEndDate(tabTemplet.m_NewbieDate);
				return true;
			}
			if (tabTemplet.IsReturningMission)
			{
				endUtcTime = userData.GetReturnEndDate(tabTemplet.m_ReturningUserType);
				return true;
			}
			endUtcTime = tabTemplet.m_endTimeUtc;
			return true;
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x0008C20C File Offset: 0x0008A40C
		public static NKM_ERROR_CODE CanComplete(NKMMissionTemplet templet, NKMUserData userData, NKMMissionData missionData)
		{
			if (templet == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_MISSION_INVALID_MISSION_ID;
			}
			if (missionData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_MISSION_INVALID_MISSION_ID;
			}
			if (userData == null || userData.m_MissionData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_USER_DATA_NULL;
			}
			if (missionData.isComplete)
			{
				return NKM_ERROR_CODE.NEC_FAIL_MISSION_ALREADY_COMPLETED;
			}
			if (missionData.mission_id != templet.m_MissionID)
			{
				return NKM_ERROR_CODE.NEC_FAIL_MISSION_INVALID_MISSION_ID;
			}
			if (templet.m_MissionRequire > 0)
			{
				NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(templet.m_MissionRequire);
				NKMMissionData missionDataByMissionId = userData.m_MissionData.GetMissionDataByMissionId(templet.m_MissionRequire);
				if (missionTemplet == null || missionDataByMissionId == null)
				{
					return NKM_ERROR_CODE.NEC_FAIL_MISSION_NOT_ENOUGH_MISSION_COND_VALUE;
				}
				if (templet.m_GroupId != missionTemplet.m_GroupId && !missionDataByMissionId.isComplete)
				{
					return NKM_ERROR_CODE.NEC_FAIL_MISSION_NOT_ENOUGH_MISSION_COND_VALUE;
				}
			}
			if (!NKMMissionManager.CheckMissionTabUnlocked(templet.m_MissionTabId, userData))
			{
				return NKM_ERROR_CODE.NEC_FAIL_MISSION_NOT_ENOUGH_MISSION_COND_VALUE;
			}
			if (NKMMissionManager.IsCumulativeCondition(templet.m_MissionCond.mission_cond))
			{
				if (!NKMMissionManager.CanCompleteCumulative(templet, missionData))
				{
					return NKM_ERROR_CODE.NEC_FAIL_MISSION_NOT_ENOUGH_MISSION_COND_VALUE;
				}
			}
			else if (!NKMMissionManager.CanCompleteNonCumulative(templet, userData))
			{
				return NKM_ERROR_CODE.NEC_FAIL_MISSION_NOT_ENOUGH_MISSION_COND_VALUE;
			}
			if (NKMMissionManager.CheckCanReset(templet.m_ResetInterval, missionData))
			{
				return NKM_ERROR_CODE.NEC_FAIL_MISSION_INVALID_MISSION_ID;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x0008C308 File Offset: 0x0008A508
		public static bool CheckCanReset(NKM_MISSION_RESET_INTERVAL resetInterval, NKMMissionData missionData)
		{
			if (missionData == null)
			{
				return resetInterval != NKM_MISSION_RESET_INTERVAL.NONE;
			}
			switch (resetInterval)
			{
			case NKM_MISSION_RESET_INTERVAL.ALWAYS:
				return true;
			case NKM_MISSION_RESET_INTERVAL.DAILY:
				return missionData.last_update_date < NKMTime.GetResetTime(NKCSynchronizedTime.GetServerUTCTime(0.0), NKMTime.TimePeriod.Day).Ticks;
			case NKM_MISSION_RESET_INTERVAL.WEEKLY:
				return missionData.last_update_date < NKMTime.GetResetTime(NKCSynchronizedTime.GetServerUTCTime(0.0), NKMTime.TimePeriod.Week).Ticks;
			case NKM_MISSION_RESET_INTERVAL.MONTHLY:
				return missionData.last_update_date < NKMTime.GetResetTime(NKCSynchronizedTime.GetServerUTCTime(0.0), NKMTime.TimePeriod.Month).Ticks;
			case NKM_MISSION_RESET_INTERVAL.NONE:
				return false;
			default:
				return false;
			}
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x0008C3B0 File Offset: 0x0008A5B0
		public static NKMMissionTemplet GetLastCompletedMissionTempletByTab(int missionTabID)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			List<NKMMissionTemplet> missionTempletListByType = NKMMissionManager.GetMissionTempletListByType(missionTabID);
			for (int i = missionTempletListByType.Count - 1; i >= 0; i--)
			{
				NKMMissionData missionData = myUserData.m_MissionData.GetMissionData(missionTempletListByType[i]);
				if (missionData != null && missionData.isComplete)
				{
					return missionTempletListByType[i];
				}
			}
			return null;
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x0008C40C File Offset: 0x0008A60C
		public static NKMMissionTemplet GetCurrentGrowthMissionTemplet()
		{
			NKMMissionTemplet nkmmissionTemplet = null;
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return nkmmissionTemplet;
			}
			bool flag = false;
			List<NKMMissionTabTemplet> list = new List<NKMMissionTabTemplet>();
			foreach (NKMMissionTabTemplet nkmmissionTabTemplet in NKMMissionManager.DicMissionTab.Values)
			{
				if (nkmmissionTabTemplet.m_MissionType == NKM_MISSION_TYPE.COMBINE_GUIDE_MISSION && !NKMMissionManager.IsMissionTabExpired(nkmmissionTabTemplet, nkmuserData))
				{
					list.Add(nkmmissionTabTemplet);
				}
			}
			list.Sort(new Comparison<NKMMissionTabTemplet>(NKMMissionManager.CompGrowthSort));
			for (int i = 0; i < list.Count; i++)
			{
				nkmmissionTemplet = NKMMissionManager.GetGrowthMissionIngTempletByTab(list[i].m_tabID, out flag);
				if (!flag || nkmmissionTemplet != null)
				{
					break;
				}
			}
			if (flag)
			{
				return null;
			}
			if (nkmmissionTemplet == null && list.Count > 0)
			{
				return NKMMissionManager.GetMissionTemplet(list[0].m_firstMissionID);
			}
			return nkmmissionTemplet;
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x0008C4F8 File Offset: 0x0008A6F8
		private static int CompGrowthSort(NKMMissionTabTemplet lTemplet, NKMMissionTabTemplet rTemplet)
		{
			return lTemplet.m_tabID.CompareTo(rTemplet.m_tabID);
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x0008C50C File Offset: 0x0008A70C
		public static NKMMissionTemplet GetGrowthMissionIngTempletByTab(int missionTabID, out bool bCompleteAll)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			bCompleteAll = false;
			if (nkmuserData == null)
			{
				return null;
			}
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(missionTabID);
			if (missionTabTemplet == null)
			{
				return null;
			}
			if (NKMMissionManager.IsMissionTabExpired(missionTabTemplet, nkmuserData))
			{
				return null;
			}
			NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(missionTabTemplet.m_completeMissionID);
			if (missionTemplet != null)
			{
				if (nkmuserData.m_MissionData.GetMissionData(missionTemplet) != null)
				{
					if (nkmuserData.m_MissionData.GetMissionData(missionTemplet).isComplete)
					{
						bCompleteAll = true;
						return null;
					}
					return missionTemplet;
				}
				else
				{
					NKMMissionData missionDataByMissionId = nkmuserData.m_MissionData.GetMissionDataByMissionId(missionTemplet.m_MissionRequire);
					if (missionDataByMissionId != null && missionDataByMissionId.isComplete)
					{
						return missionTemplet;
					}
				}
			}
			NKMMissionTemplet templet = NKMMissionManager.GetLastCompletedMissionTempletByTab(missionTabID);
			if (templet != null)
			{
				return NKMMissionManager.GetMissionTempletListByType(missionTabID).Find((NKMMissionTemplet x) => x.m_MissionRequire == templet.m_MissionID);
			}
			NKMMissionTemplet missionTemplet2 = NKMMissionManager.GetMissionTemplet(missionTabTemplet.m_firstMissionID);
			if (missionTemplet2 != null && missionTemplet2.m_MissionRequire != 0 && nkmuserData.m_MissionData.GetMissionDataByMissionId(missionTemplet2.m_MissionRequire) != null)
			{
				return missionTemplet2;
			}
			return NKMMissionManager.GetMissionTempletListByType(missionTabID).Find((NKMMissionTemplet x) => x.m_MissionRequire == 0);
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x0008C627 File Offset: 0x0008A827
		public static void SetDefaultTrackingMissionToGrowthMission()
		{
			NKMMissionManager.SetTrackingMissionTemplet(NKMMissionManager.GetCurrentGrowthMissionTemplet());
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x0008C633 File Offset: 0x0008A833
		public static void SetTrackingMissionTemplet(NKMMissionTemplet templet)
		{
			NKMMissionManager.m_TrackingMissionTemplet = templet;
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x0008C63B File Offset: 0x0008A83B
		public static NKMMissionTemplet GetTrackingMissionTemplet()
		{
			return NKMMissionManager.m_TrackingMissionTemplet;
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x0008C642 File Offset: 0x0008A842
		public static NKMMissionTabTemplet GetFirstGrowthUnitMissionTabTemplet()
		{
			return null;
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x0008C648 File Offset: 0x0008A848
		public static long GetRepeatMissionDataTimes(NKM_MISSION_TYPE tabType)
		{
			NKMMissionTemplet nkmmissionTemplet = null;
			NKM_MISSION_COND nkm_MISSION_COND;
			if (tabType == NKM_MISSION_TYPE.REPEAT_DAILY)
			{
				nkm_MISSION_COND = NKM_MISSION_COND.HAVE_DAILY_POINT;
			}
			else
			{
				nkm_MISSION_COND = NKM_MISSION_COND.HAVE_WEEKLY_POINT;
			}
			foreach (NKMMissionTemplet nkmmissionTemplet2 in NKMMissionManager.DicMissionId.Values)
			{
				if (nkmmissionTemplet2.m_MissionCond.mission_cond == nkm_MISSION_COND)
				{
					nkmmissionTemplet = nkmmissionTemplet2;
					break;
				}
			}
			if (nkmmissionTemplet != null)
			{
				NKMMissionData missionData = NKCScenManager.CurrentUserData().m_MissionData.GetMissionData(nkmmissionTemplet);
				if (missionData != null)
				{
					if (!NKMMissionManager.CheckCanReset(nkmmissionTemplet.m_ResetInterval, missionData))
					{
						return missionData.times;
					}
					return 0L;
				}
			}
			return 0L;
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x0008C6F4 File Offset: 0x0008A8F4
		public static string GetMissionTabUnlockCondition(int tabID, NKMUserData userData)
		{
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(tabID);
			if (missionTabTemplet == null)
			{
				return "";
			}
			if (missionTabTemplet.IsNewbieMission && !userData.IsNewbieUser(missionTabTemplet.m_NewbieDate))
			{
				return NKCStringTable.GetString("SI_DP_MISSION_TAB_FINISHED", false);
			}
			if (missionTabTemplet.IsReturningMission && !userData.IsReturnUser(missionTabTemplet.m_ReturningUserType))
			{
				return NKCStringTable.GetString("SI_DP_MISSION_TAB_FINISHED", false);
			}
			if (missionTabTemplet.HasDateLimit)
			{
				DateTime serverUTCTime = NKCSynchronizedTime.GetServerUTCTime(0.0);
				DateTime t;
				if (serverUTCTime < missionTabTemplet.m_startTimeUtc)
				{
					TimeSpan timeSpan = missionTabTemplet.m_startTimeUtc - serverUTCTime;
					if (timeSpan.Days > 0)
					{
						return NKCStringTable.GetString("SI_DP_MISSION_OPEN_UNTIL_DAY", new object[]
						{
							timeSpan.Days,
							timeSpan.Hours
						});
					}
					if (timeSpan.Hours <= 0)
					{
						return NKCStringTable.GetString("SI_DP_MISSION_OPEN_UNTIL_MINUTE", new object[]
						{
							timeSpan.Minutes
						});
					}
					return NKCStringTable.GetString("SI_DP_MISSION_OPEN_UNTIL_HOUR", new object[]
					{
						timeSpan.Hours,
						timeSpan.Minutes
					});
				}
				else if (NKMMissionManager.TryGetMissionTabExpireUtcTime(missionTabTemplet, userData, out t) && serverUTCTime > t)
				{
					return NKCStringTable.GetString("SI_DP_MISSION_TAB_FINISHED", false);
				}
			}
			return NKCUtilString.GetUnlockConditionRequireDesc(missionTabTemplet.m_UnlockInfo, true, false);
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x0008C84C File Offset: 0x0008AA4C
		public static bool IsMissionOpened(NKMMissionTemplet cNKMMissionTemplet)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			return cNKMMissionTemplet != null && nkmuserData != null && (cNKMMissionTemplet.m_MissionRequire == 0 || NKMMissionManager.GetMissionStateData(cNKMMissionTemplet.m_MissionRequire).IsMissionCompleted);
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x0008C888 File Offset: 0x0008AA88
		public static bool IsGuideMissionAllClear()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return false;
			}
			List<NKMMissionTabTemplet> list = new List<NKMMissionTabTemplet>();
			foreach (NKMMissionTabTemplet nkmmissionTabTemplet in NKMMissionManager.DicMissionTab.Values)
			{
				if (nkmmissionTabTemplet.m_MissionType == NKM_MISSION_TYPE.COMBINE_GUIDE_MISSION && !NKMMissionManager.IsMissionTabExpired(nkmmissionTabTemplet, nkmuserData))
				{
					list.Add(nkmmissionTabTemplet);
				}
			}
			bool flag = false;
			for (int i = 0; i < list.Count; i++)
			{
				NKMMissionTemplet growthMissionIngTempletByTab = NKMMissionManager.GetGrowthMissionIngTempletByTab(list[i].m_tabID, out flag);
				if (!flag || growthMissionIngTempletByTab != null)
				{
					break;
				}
			}
			return flag;
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x0008C93C File Offset: 0x0008AB3C
		public static int GetGuideMissionClearCount()
		{
			int num = 0;
			if (NKCScenManager.CurrentUserData() == null)
			{
				return num;
			}
			foreach (NKMMissionTabTemplet nkmmissionTabTemplet in NKMMissionManager.DicMissionTab.Values)
			{
				if (nkmmissionTabTemplet.m_MissionType == NKM_MISSION_TYPE.COMBINE_GUIDE_MISSION)
				{
					using (List<NKMMissionTemplet>.Enumerator enumerator2 = NKMMissionManager.GetMissionTempletListByType(nkmmissionTabTemplet.m_tabID).GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							if (NKMMissionManager.GetMissionStateData(enumerator2.Current).IsMissionCanClear)
							{
								num++;
							}
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x0008C9F8 File Offset: 0x0008ABF8
		public static List<NKMMissionTemplet> GetGuildUserMissionTemplets()
		{
			List<NKMMissionTemplet> list = new List<NKMMissionTemplet>();
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKMUserMissionData nkmuserMissionData = (nkmuserData != null) ? nkmuserData.m_MissionData : null;
			if (nkmuserMissionData == null)
			{
				return list;
			}
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(NKM_MISSION_TYPE.GUILD);
			if (missionTabTemplet == null)
			{
				Log.Error(string.Format("tabTemplet is null - NKM_MISSION_TYPE : {0}", NKM_MISSION_TYPE.GUILD), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMMissionManagerEx.cs", 664);
				return list;
			}
			List<NKMMissionTemplet> missionTempletListByType = NKMMissionManager.GetMissionTempletListByType(missionTabTemplet.m_tabID);
			for (int i = 0; i < missionTempletListByType.Count; i++)
			{
				NKMMissionTemplet nkmmissionTemplet = missionTempletListByType[i];
				if (!nkmmissionTemplet.IsRandomMission || nkmuserMissionData.GetMissionData(nkmmissionTemplet) != null)
				{
					if (nkmmissionTemplet.m_MissionRequire > 0)
					{
						if (nkmuserMissionData.WaitingForRandomMissionRefresh())
						{
							goto IL_114;
						}
						NKMMissionData nkmmissionData = nkmuserMissionData.GetMissionDataByGroupId(nkmmissionTemplet.m_GroupId);
						if (nkmmissionData == null)
						{
							goto IL_114;
						}
						if (nkmmissionData.mission_id == nkmmissionTemplet.m_MissionID)
						{
							list.Add(nkmmissionTemplet);
							goto IL_114;
						}
						nkmmissionData = nkmuserMissionData.GetMissionDataByMissionId(nkmmissionTemplet.m_MissionRequire);
						if (nkmmissionData == null)
						{
							goto IL_114;
						}
						if (nkmmissionData.isComplete && nkmmissionData.mission_id == nkmmissionTemplet.m_MissionRequire)
						{
							list.Add(nkmmissionTemplet);
							goto IL_114;
						}
						if (nkmmissionData.mission_id <= nkmmissionTemplet.m_MissionRequire)
						{
							goto IL_114;
						}
					}
					list.Add(missionTempletListByType[i]);
				}
				IL_114:;
			}
			list.Sort(new Comparison<NKMMissionTemplet>(NKMMissionManager.Comparer));
			return list;
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x0008CB40 File Offset: 0x0008AD40
		public static int Comparer(NKMMissionTemplet x, NKMMissionTemplet y)
		{
			NKMMissionManager.MissionStateData missionStateData = NKMMissionManager.GetMissionStateData(x);
			NKMMissionManager.MissionStateData missionStateData2 = NKMMissionManager.GetMissionStateData(y);
			if (missionStateData.state != missionStateData2.state)
			{
				return missionStateData.state.CompareTo(missionStateData2.state);
			}
			if (x.m_MissionPoolID != y.m_MissionPoolID)
			{
				return x.m_MissionPoolID.CompareTo(y.m_MissionPoolID);
			}
			return x.m_MissionID.CompareTo(y.m_MissionID);
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x0008CBB8 File Offset: 0x0008ADB8
		public static NKMMissionManager.MissionState GetMissionState(NKMMissionTemplet missionTemplet)
		{
			return NKMMissionManager.GetMissionStateData(missionTemplet).state;
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x0008CBC5 File Offset: 0x0008ADC5
		public static NKMMissionManager.MissionStateData GetMissionStateData(int missionID)
		{
			return NKMMissionManager.GetMissionStateData(NKMMissionManager.GetMissionTemplet(missionID));
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x0008CBD4 File Offset: 0x0008ADD4
		public static NKMMissionManager.MissionStateData GetMissionStateData(NKMMissionTemplet missionTemplet)
		{
			if (missionTemplet == null)
			{
				return new NKMMissionManager.MissionStateData(NKMMissionManager.MissionState.LOCKED);
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return new NKMMissionManager.MissionStateData(NKMMissionManager.MissionState.LOCKED);
			}
			if (!NKMMissionManager.CheckMissionTabUnlocked(missionTemplet.m_MissionTabId, nkmuserData))
			{
				return new NKMMissionManager.MissionStateData(NKMMissionManager.MissionState.LOCKED);
			}
			if (missionTemplet == null)
			{
				return new NKMMissionManager.MissionStateData(NKMMissionManager.MissionState.LOCKED);
			}
			NKMMissionData missionDataByGroupId = NKCScenManager.GetScenManager().GetMyUserData().m_MissionData.GetMissionDataByGroupId(missionTemplet.m_GroupId);
			bool flag;
			bool flag2;
			bool flag3;
			if (missionDataByGroupId != null)
			{
				flag = NKMMissionManager.CheckCanReset(missionTemplet.m_ResetInterval, missionDataByGroupId);
				if (flag)
				{
					flag2 = false;
					flag3 = false;
				}
				else if (missionDataByGroupId.isComplete)
				{
					flag3 = true;
					flag2 = false;
				}
				else if (missionDataByGroupId.mission_id > missionTemplet.m_MissionID)
				{
					flag3 = true;
					flag2 = false;
				}
				else if (missionDataByGroupId.mission_id == missionTemplet.m_MissionID)
				{
					flag3 = false;
					flag2 = true;
				}
				else
				{
					flag3 = false;
					flag2 = false;
				}
			}
			else
			{
				flag3 = false;
				flag2 = false;
				flag = false;
			}
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(missionTemplet.m_MissionTabId);
			if (missionTabTemplet != null && missionTabTemplet.m_MissionPoolID > 0 && NKCScenManager.CurrentUserData().m_MissionData.WaitingForRandomMissionRefresh())
			{
				flag = false;
			}
			long count = 0L;
			if (NKMMissionManager.IsCumulativeCondition(missionTemplet.m_MissionCond.mission_cond) && !flag)
			{
				if (flag2)
				{
					count = missionDataByGroupId.times;
				}
				else if (flag3)
				{
					count = missionTemplet.m_Times;
				}
			}
			else
			{
				count = NKMMissionManager.GetNonCumulativeMissionTimes(missionTemplet, NKCScenManager.GetScenManager().GetMyUserData(), false);
			}
			if (flag2)
			{
				if (flag || NKMMissionManager.CanComplete(missionTemplet, nkmuserData, missionDataByGroupId) != NKM_ERROR_CODE.NEC_OK)
				{
					return new NKMMissionManager.MissionStateData(NKMMissionManager.MissionState.ONGOING, count);
				}
				if (missionTemplet.m_ResetInterval != NKM_MISSION_RESET_INTERVAL.NONE)
				{
					return new NKMMissionManager.MissionStateData(NKMMissionManager.MissionState.REPEAT_CAN_COMPLETE, count);
				}
				return new NKMMissionManager.MissionStateData(NKMMissionManager.MissionState.CAN_COMPLETE, count);
			}
			else if (flag3)
			{
				if (missionTemplet.m_ResetInterval != NKM_MISSION_RESET_INTERVAL.NONE)
				{
					return new NKMMissionManager.MissionStateData(NKMMissionManager.MissionState.REPEAT_COMPLETED, count);
				}
				return new NKMMissionManager.MissionStateData(NKMMissionManager.MissionState.COMPLETED, count);
			}
			else
			{
				if (NKMMissionManager.IsMissionOpened(missionTemplet))
				{
					return new NKMMissionManager.MissionStateData(NKMMissionManager.MissionState.ONGOING, 0L);
				}
				return new NKMMissionManager.MissionStateData(NKMMissionManager.MissionState.LOCKED);
			}
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x0008CD78 File Offset: 0x0008AF78
		public static NKMMissionData GetMissionData(NKMMissionTemplet missionTemplet)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return null;
			}
			return nkmuserData.m_MissionData.GetMissionData(missionTemplet);
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x0008CD9C File Offset: 0x0008AF9C
		public static bool CanCompleteNonCumulative(NKMMissionTemplet templet, NKMUserData userData)
		{
			return NKMMissionManager.GetNonCumulativeMissionTimes(templet, userData, true) >= templet.m_Times;
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x0008CDB4 File Offset: 0x0008AFB4
		public static long GetNonCumulativeMissionTimes(NKMMissionTemplet templet, NKMUserData userData, bool bShowErrorlog = true)
		{
			long num = 0L;
			if (templet.m_MissionCond.value1.Count == 0)
			{
				NKM_MISSION_COND mission_cond = templet.m_MissionCond.mission_cond;
				if (mission_cond <= NKM_MISSION_COND.WORLDMAP_BRANCH_NUMBER)
				{
					if (mission_cond == NKM_MISSION_COND.ACCOUNT_LEVEL)
					{
						return (long)userData.m_UserLevel;
					}
					if (mission_cond == NKM_MISSION_COND.WORLDMAP_BRANCH_NUMBER)
					{
						return (long)userData.m_WorldmapData.GetUnlockedCityCount();
					}
				}
				else
				{
					if (mission_cond == NKM_MISSION_COND.JUST_OPEN)
					{
						return 1L;
					}
					if (mission_cond == NKM_MISSION_COND.COUNTER_CASE_OPENED)
					{
						return num + (long)userData.GetCounterCaseClearCount(0);
					}
				}
				if (bShowErrorlog)
				{
					Log.Error(string.Format("[NKMMissionManager] �̼��� values ���� 0�� �̼�. missionId:{0}", templet.m_MissionID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMMissionManagerEx.cs", 1025);
				}
				return 0L;
			}
			using (List<int>.Enumerator enumerator = templet.m_MissionCond.value1.GetEnumerator())
			{
				Func<NKMMissionTemplet, bool> <>9__1;
				while (enumerator.MoveNext())
				{
					int value = enumerator.Current;
					NKM_MISSION_COND mission_cond = templet.m_MissionCond.mission_cond;
					if (mission_cond <= NKM_MISSION_COND.DUNGEON_CLEARED)
					{
						if (mission_cond <= NKM_MISSION_COND.COLLECT_EQUIP_ENCHANT_LEVEL)
						{
							if (mission_cond == NKM_MISSION_COND.COLLECT_UNIT_LEVEL)
							{
								num += (long)userData.m_ArmyData.GetUnitCountByLevel(value);
								continue;
							}
							if (mission_cond == NKM_MISSION_COND.COLLECT_EQUIP_ENCHANT_LEVEL)
							{
								num += (long)userData.m_InventoryData.GetEquipCountByEnchantLevel(value);
								continue;
							}
						}
						else
						{
							if (mission_cond == NKM_MISSION_COND.COLLECT_SHIP_LEVEL)
							{
								num += (long)userData.m_ArmyData.GetShipCountByLevel(value);
								continue;
							}
							if (mission_cond == NKM_MISSION_COND.WARFARE_CLEARED)
							{
								num += (userData.CheckWarfareClear(value) ? 1L : 0L);
								continue;
							}
							if (mission_cond == NKM_MISSION_COND.DUNGEON_CLEARED)
							{
								num += (userData.CheckDungeonClear(value) ? 1L : 0L);
								continue;
							}
						}
					}
					else if (mission_cond <= NKM_MISSION_COND.COUNTER_CASE_OPENED)
					{
						if (mission_cond == NKM_MISSION_COND.PHASE_CLEAR)
						{
							num += (userData.CheckPhaseClear(value) ? 1L : 0L);
							continue;
						}
						switch (mission_cond)
						{
						case NKM_MISSION_COND.COLLECT_SHIP_GET:
							num += (userData.m_ArmyData.SearchUnitByID(NKM_UNIT_TYPE.NUT_SHIP, value, NKMArmyData.UNIT_SEARCH_OPTION.None, 0) ? 1L : 0L);
							continue;
						case NKM_MISSION_COND.COLLECT_SHIP_GET_LEVEL:
							num += (userData.m_ArmyData.SearchUnitByID(NKM_UNIT_TYPE.NUT_SHIP, value, NKMArmyData.UNIT_SEARCH_OPTION.Level, (int)templet.m_Times) ? templet.m_Times : 0L);
							continue;
						case NKM_MISSION_COND.COLLECT_SHIP_UPGRADE:
							num += (userData.m_ArmyData.SearchUnitByID(NKM_UNIT_TYPE.NUT_SHIP, value, NKMArmyData.UNIT_SEARCH_OPTION.StarGrade, (int)templet.m_Times) ? templet.m_Times : 0L);
							continue;
						case NKM_MISSION_COND.COUNTER_CASE_OPENED:
							num += (long)userData.GetCounterCaseClearCount(value);
							continue;
						}
					}
					else
					{
						if (mission_cond == NKM_MISSION_COND.PVP_HIGHEST_TIER_ASYNC)
						{
							num += ((userData.m_AsyncData.MaxLeagueTierID >= value) ? 1L : 0L);
							continue;
						}
						switch (mission_cond)
						{
						case NKM_MISSION_COND.ACHIEVEMENT_CLEARED:
						{
							NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(value);
							if (missionTemplet == null)
							{
								Log.Error(string.Format("[NKMMissionManager] Find Templet Fail. missionId:{0}", value), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMMissionManagerEx.cs", 1075);
								continue;
							}
							NKMMissionData missionDataByGroupId = userData.m_MissionData.GetMissionDataByGroupId(missionTemplet.m_GroupId);
							if (missionDataByGroupId == null)
							{
								continue;
							}
							if (missionDataByGroupId.isComplete)
							{
								num += 1L;
								continue;
							}
							if (value < missionDataByGroupId.mission_id)
							{
								num += 1L;
								continue;
							}
							continue;
						}
						case NKM_MISSION_COND.PALACE_CLEAR:
						case NKM_MISSION_COND.SUPPORT_PLATOON_USED:
							break;
						case NKM_MISSION_COND.DIVE_HIGHEST_CLEARED:
							num += (userData.m_DiveClearData.Contains(value) ? 1L : 0L);
							continue;
						case NKM_MISSION_COND.PVP_HIGHEST_TIER_CLEARED:
							num += ((userData.m_PvpData.MaxLeagueTierID >= value) ? 1L : 0L);
							continue;
						case NKM_MISSION_COND.MISSION_EVENT_TAB_CLEAR:
						{
							IEnumerable<NKMMissionTemplet> source = from e in NKMMissionManager.DicMissionId.Values
							where e.m_MissionTabId == value
							select e;
							Func<NKMMissionTemplet, bool> predicate;
							if ((predicate = <>9__1) == null)
							{
								predicate = (<>9__1 = ((NKMMissionTemplet e) => userData.m_MissionData.IsMissionCompleted(e)));
							}
							int num2 = source.Count(predicate);
							num += (long)num2;
							continue;
						}
						case NKM_MISSION_COND.MENTORING_MISSION_CLEARED:
						{
							NKMMissionData missionDataByMissionId = userData.m_MissionData.GetMissionDataByMissionId(value);
							if (missionDataByMissionId != null && missionDataByMissionId.isComplete)
							{
								num += 1L;
								continue;
							}
							continue;
						}
						default:
							switch (mission_cond)
							{
							case NKM_MISSION_COND.UNIT_POWER_HIGHEST:
							{
								int num3 = 0;
								foreach (NKMUnitData nkmunitData in userData.m_ArmyData.m_dicMyUnit.Values)
								{
									NKMEquipmentSet equipmentSet = nkmunitData.GetEquipmentSet(userData.m_InventoryData);
									if (equipmentSet != null && nkmunitData.CalculateUnitOperationPower(equipmentSet) >= value)
									{
										num3++;
									}
								}
								num += (long)num3;
								continue;
							}
							case NKM_MISSION_COND.DUNGEON_SQUAD_UNIT_POWER_HIGHEST:
							{
								int num4 = (from e in userData.m_ArmyData.GetDeckList(NKM_DECK_TYPE.NDT_DAILY)
								where userData.m_ArmyData.GetArmyAvarageOperationPower(e, false, null, null) >= value
								select e).Count<NKMDeckData>();
								num += (long)num4;
								continue;
							}
							case NKM_MISSION_COND.WARFARE_SQUAD_UNIT_POWER_HIGHEST:
							{
								int num5 = (from e in userData.m_ArmyData.GetDeckList(NKM_DECK_TYPE.NDT_NORMAL)
								where userData.m_ArmyData.GetArmyAvarageOperationPower(e, false, null, null) >= value
								select e).Count<NKMDeckData>();
								num += (long)num5;
								continue;
							}
							case NKM_MISSION_COND.PVP_TOTAL_CLEAR_BOTH:
								num += (long)userData.m_AsyncData.WinCount;
								num += (long)userData.m_PvpData.WinCount;
								continue;
							case NKM_MISSION_COND.PVP_TOTAL_CLEAR_ASYNC:
								num += (long)userData.m_AsyncData.WinCount;
								continue;
							case NKM_MISSION_COND.PVP_TOTAL_CLEAR_RANK:
								num += (long)userData.m_PvpData.WinCount;
								continue;
							}
							break;
						}
					}
					num = num;
				}
			}
			return num;
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x0008D490 File Offset: 0x0008B690
		public static bool GetHaveClearedMission()
		{
			return NKMMissionManager.m_bHaveCompletedMission;
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x0008D497 File Offset: 0x0008B697
		public static void SetHaveClearedMission(bool bSet, bool bVisible = true)
		{
			if (bVisible)
			{
				NKMMissionManager.m_bHaveCompletedMission = bSet;
			}
			NKCUIManager.OnMissionUpdated();
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x0008D4A8 File Offset: 0x0008B6A8
		public static bool IsGuideMissionOpen()
		{
			foreach (KeyValuePair<int, NKMMissionTabTemplet> keyValuePair in NKMMissionManager.DicMissionTab)
			{
				if (keyValuePair.Value.m_MissionType == NKM_MISSION_TYPE.COMBINE_GUIDE_MISSION && keyValuePair.Value.EnableByTag && keyValuePair.Value.m_firstMissionID > 0)
				{
					NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(keyValuePair.Value.m_firstMissionID);
					if (missionTemplet != null && missionTemplet.EnableByTag)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x0008D544 File Offset: 0x0008B744
		public static bool GetHaveClearedMissionGuide()
		{
			return NKMMissionManager.m_bHaveCompletedMissionGuide;
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x0008D54B File Offset: 0x0008B74B
		public static void SetHaveClearedMissionGuide(bool bSet, bool bVisible = true)
		{
			if (bVisible)
			{
				NKMMissionManager.m_bHaveCompletedMissionGuide = bSet;
			}
			NKCUIManager.OnMissionUpdated();
		}

		// Token: 0x04001DE1 RID: 7649
		public static Dictionary<int, NKMMissionTemplet> DicMissionId = null;

		// Token: 0x04001DE2 RID: 7650
		public static Dictionary<int, NKMMissionTabTemplet> DicMissionTab = null;

		// Token: 0x04001DE3 RID: 7651
		public static Dictionary<int, DateTime> DicCounterGroupResetDate = null;

		// Token: 0x04001DE4 RID: 7652
		public static Dictionary<int, List<int>> StageClearToMissionMap = new Dictionary<int, List<int>>();

		// Token: 0x04001DE5 RID: 7653
		public static Dictionary<int, List<int>> DicPoolList = new Dictionary<int, List<int>>();

		// Token: 0x04001DE6 RID: 7654
		public static Dictionary<int, HashSet<int>> DicGroupListByTab = new Dictionary<int, HashSet<int>>();

		// Token: 0x04001DE7 RID: 7655
		public static int GuildTabId = 0;

		// Token: 0x04001DE8 RID: 7656
		private static NKMMissionTemplet m_TrackingMissionTemplet = null;

		// Token: 0x04001DE9 RID: 7657
		private static bool m_bHaveCompletedMission = false;

		// Token: 0x04001DEA RID: 7658
		private static bool m_bHaveCompletedMissionGuide = false;

		// Token: 0x020011ED RID: 4589
		public enum MissionState
		{
			// Token: 0x040093DE RID: 37854
			CAN_COMPLETE,
			// Token: 0x040093DF RID: 37855
			REPEAT_CAN_COMPLETE,
			// Token: 0x040093E0 RID: 37856
			ONGOING,
			// Token: 0x040093E1 RID: 37857
			REPEAT_COMPLETED,
			// Token: 0x040093E2 RID: 37858
			LOCKED,
			// Token: 0x040093E3 RID: 37859
			COMPLETED
		}

		// Token: 0x020011EE RID: 4590
		public struct MissionStateData
		{
			// Token: 0x0600A0FB RID: 41211 RVA: 0x0033F171 File Offset: 0x0033D371
			public MissionStateData(NKMMissionManager.MissionState missionState)
			{
				this.state = missionState;
				this.progressCount = 0L;
			}

			// Token: 0x0600A0FC RID: 41212 RVA: 0x0033F182 File Offset: 0x0033D382
			public MissionStateData(NKMMissionManager.MissionState missionState, long count)
			{
				this.state = missionState;
				this.progressCount = count;
			}

			// Token: 0x1700179A RID: 6042
			// (get) Token: 0x0600A0FD RID: 41213 RVA: 0x0033F194 File Offset: 0x0033D394
			public bool IsMissionOngoing
			{
				get
				{
					NKMMissionManager.MissionState missionState = this.state;
					return missionState <= NKMMissionManager.MissionState.ONGOING;
				}
			}

			// Token: 0x1700179B RID: 6043
			// (get) Token: 0x0600A0FE RID: 41214 RVA: 0x0033F1B0 File Offset: 0x0033D3B0
			public bool IsMissionCompleted
			{
				get
				{
					NKMMissionManager.MissionState missionState = this.state;
					return missionState == NKMMissionManager.MissionState.REPEAT_COMPLETED || missionState == NKMMissionManager.MissionState.COMPLETED;
				}
			}

			// Token: 0x1700179C RID: 6044
			// (get) Token: 0x0600A0FF RID: 41215 RVA: 0x0033F1D0 File Offset: 0x0033D3D0
			public bool IsMissionCanClear
			{
				get
				{
					NKMMissionManager.MissionState missionState = this.state;
					return missionState <= NKMMissionManager.MissionState.REPEAT_CAN_COMPLETE;
				}
			}

			// Token: 0x1700179D RID: 6045
			// (get) Token: 0x0600A100 RID: 41216 RVA: 0x0033F1EB File Offset: 0x0033D3EB
			public bool IsLocked
			{
				get
				{
					return this.state == NKMMissionManager.MissionState.LOCKED;
				}
			}

			// Token: 0x040093E4 RID: 37860
			public NKMMissionManager.MissionState state;

			// Token: 0x040093E5 RID: 37861
			public long progressCount;
		}
	}
}
