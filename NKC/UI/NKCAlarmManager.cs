using System;
using System.Collections.Generic;
using ClientPacket.Guild;
using ClientPacket.Office;
using ClientPacket.WorldMap;
using Cs.Core.Util;
using NKM;
using NKM.Event;
using NKM.Guild;
using NKM.Shop;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x0200090D RID: 2317
	public static class NKCAlarmManager
	{
		// Token: 0x06005C86 RID: 23686 RVA: 0x001C9C54 File Offset: 0x001C7E54
		public static void Update(float deltaTime)
		{
			if (NKCAlarmManager.s_bServerShutdownScheduled && NKCAlarmManager.s_dtServerShutdownUTCTime.AddMinutes(-1.0) <= NKCSynchronizedTime.GetServerUTCTime(0.0))
			{
				NKCAlarmManager.s_bServerShutdownScheduled = false;
				NKCUIPopupMessageServer.Instance.Open(NKCUIPopupMessageServer.eMessageStyle.Slide, NKCStringTable.GetString("SI_SERVER_SHUTDOWN_NOW", false), 1);
			}
		}

		// Token: 0x06005C87 RID: 23687 RVA: 0x001C9CB0 File Offset: 0x001C7EB0
		public static void Init()
		{
			NKCAlarmManager.m_dicNotify.Clear();
			NKCAlarmManager.m_dicNotify.Add(NKCAlarmManager.ALARM_TYPE.FRIEND, new NKCAlarmManager.OnNotify(NKCAlarmManager.CheckFriendNotify));
			NKCAlarmManager.m_dicNotify.Add(NKCAlarmManager.ALARM_TYPE.HANGAR, new NKCAlarmManager.OnNotify(NKCAlarmManager.CheckHangarNotify));
			NKCAlarmManager.m_dicNotify.Add(NKCAlarmManager.ALARM_TYPE.INVENTORY, new NKCAlarmManager.OnNotify(NKCAlarmManager.CheckInventoryNotify));
			NKCAlarmManager.m_dicNotify.Add(NKCAlarmManager.ALARM_TYPE.COLLECTION, new NKCAlarmManager.OnNotify(NKCAlarmManager.CheckCollectionNotify));
			NKCAlarmManager.m_dicNotify.Add(NKCAlarmManager.ALARM_TYPE.FACTORY, new NKCAlarmManager.OnNotify(NKCAlarmManager.CheckFactoryNotify));
			NKCAlarmManager.m_dicNotify.Add(NKCAlarmManager.ALARM_TYPE.WORLDMAP, new NKCAlarmManager.OnNotify(NKCAlarmManager.CheckWorldMapNotify));
			NKCAlarmManager.m_dicNotify.Add(NKCAlarmManager.ALARM_TYPE.MISSION, new NKCAlarmManager.OnNotify(NKCAlarmManager.CheckMissionNotify));
			NKCAlarmManager.m_dicNotify.Add(NKCAlarmManager.ALARM_TYPE.CONTRACT, new NKCAlarmManager.OnNotify(NKCAlarmManager.CheckContractNotify));
			NKCAlarmManager.m_dicNotify.Add(NKCAlarmManager.ALARM_TYPE.OFFICE_DORM, new NKCAlarmManager.OnNotify(NKCAlarmManager.CheckOfficeDormNotify));
			NKCAlarmManager.m_dicNotify.Add(NKCAlarmManager.ALARM_TYPE.CHAT, new NKCAlarmManager.OnNotify(NKCAlarmManager.CheckChatNotify));
			NKCAlarmManager.m_dicNotify.Add(NKCAlarmManager.ALARM_TYPE.LEADERBOARD, new NKCAlarmManager.OnNotify(NKCAlarmManager.CheckleaderBoardNotify));
			NKCAlarmManager.m_dicNotify.Add(NKCAlarmManager.ALARM_TYPE.PVP, new NKCAlarmManager.OnNotify(NKCAlarmManager.CheckPVPNotify));
			NKCAlarmManager.m_dicNotify.Add(NKCAlarmManager.ALARM_TYPE.BASE, new NKCAlarmManager.OnNotify(NKCAlarmManager.CheckBaseNotify));
			NKCAlarmManager.m_dicNotify.Add(NKCAlarmManager.ALARM_TYPE.ALL, new NKCAlarmManager.OnNotify(NKCAlarmManager.CheckAllNotify));
			NKCAlarmManager.m_bInitComplete = true;
		}

		// Token: 0x06005C88 RID: 23688 RVA: 0x001C9E15 File Offset: 0x001C8015
		public static bool CheckNotify(NKMUserData userData, NKCAlarmManager.ALARM_TYPE alarmType)
		{
			if (!NKCAlarmManager.m_bInitComplete)
			{
				NKCAlarmManager.Init();
			}
			return NKCAlarmManager.m_dicNotify.ContainsKey(alarmType) && NKCAlarmManager.m_dicNotify[alarmType](userData);
		}

		// Token: 0x06005C89 RID: 23689 RVA: 0x001C9E44 File Offset: 0x001C8044
		public static bool CheckAllNotify(NKMUserData userData)
		{
			if (!NKCAlarmManager.m_bInitComplete)
			{
				NKCAlarmManager.Init();
			}
			foreach (KeyValuePair<NKCAlarmManager.ALARM_TYPE, NKCAlarmManager.OnNotify> keyValuePair in NKCAlarmManager.m_dicNotify)
			{
				if (keyValuePair.Key != NKCAlarmManager.ALARM_TYPE.ALL && keyValuePair.Value(userData))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005C8A RID: 23690 RVA: 0x001C9EBC File Offset: 0x001C80BC
		public static bool CheckMailNotify(NKMUserData userData)
		{
			return NKCMailManager.HasNewMail();
		}

		// Token: 0x06005C8B RID: 23691 RVA: 0x001C9EC4 File Offset: 0x001C80C4
		public static bool CheckGuildNotify(NKMUserData userData)
		{
			if (userData == null)
			{
				return false;
			}
			if (!NKCGuildManager.HasGuild())
			{
				return false;
			}
			NKMGuildMemberData nkmguildMemberData = NKCGuildManager.MyGuildData.members.Find((NKMGuildMemberData x) => x.commonProfile.userUid == userData.m_UserUID);
			if (nkmguildMemberData == null)
			{
				return false;
			}
			if (!nkmguildMemberData.HasAttendanceData(ServiceTime.Recent))
			{
				return true;
			}
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(NKM_MISSION_TYPE.GUILD);
			bool flag;
			return (missionTabTemplet != null && userData.m_MissionData.CheckCompletableMission(userData, missionTabTemplet.m_tabID, false)) || (NKCContentManager.CheckContentStatus(ContentsType.GUILD_DUNGEON, out flag, 0, 0) == NKCContentManager.eContentStatus.Open && NKCGuildCoopManager.CheckFirstSeasonStarted() && NKCGuildCoopManager.CheckSeasonRewardEnable());
		}

		// Token: 0x06005C8C RID: 23692 RVA: 0x001C9F6B File Offset: 0x001C816B
		public static bool CheckChatNotify(NKMUserData userData)
		{
			return userData != null && NKCChatManager.IsContentsUnlocked() && NKCScenManager.GetScenManager().GetGameOptionData().UseChatContent && (NKCChatManager.CheckPrivateChatNotify(userData, 0L) || NKCChatManager.CheckGuildChatNotify());
		}

		// Token: 0x06005C8D RID: 23693 RVA: 0x001C9FA0 File Offset: 0x001C81A0
		public static bool CheckFriendNotify(NKMUserData userData)
		{
			bool flag;
			return NKCContentManager.CheckContentStatus(ContentsType.FRIENDS, out flag, 0, 0) == NKCContentManager.eContentStatus.Open && NKCScenManager.GetScenManager().Get_SCEN_HOME().GetHasNewFriendRequest();
		}

		// Token: 0x06005C8E RID: 23694 RVA: 0x001C9FCC File Offset: 0x001C81CC
		public static bool CheckHangarNotify(NKMUserData userData)
		{
			bool flag;
			if (NKCContentManager.CheckContentStatus(ContentsType.HANGER_SHIPBUILD, out flag, 0, 0) != NKCContentManager.eContentStatus.Open)
			{
				return false;
			}
			foreach (NKMShipBuildTemplet nkmshipBuildTemplet in NKMTempletContainer<NKMShipBuildTemplet>.Values)
			{
				if (nkmshipBuildTemplet.ShipBuildUnlockType != NKMShipBuildTemplet.BuildUnlockType.BUT_UNABLE)
				{
					bool flag2 = false;
					foreach (KeyValuePair<long, NKMUnitData> keyValuePair in NKCScenManager.CurrentUserData().m_ArmyData.m_dicMyShip)
					{
						if (NKMShipManager.IsSameKindShip(keyValuePair.Value.m_UnitID, nkmshipBuildTemplet.Key))
						{
							flag2 = true;
							break;
						}
					}
					if (!flag2 && NKMShipManager.CanUnlockShip(userData, nkmshipBuildTemplet) && !PlayerPrefs.HasKey(string.Format("{0}_{1}_{2}", "SHIP_BUILD_SLOT_CHECK", userData.m_UserUID, nkmshipBuildTemplet.ShipID)))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06005C8F RID: 23695 RVA: 0x001CA0D8 File Offset: 0x001C82D8
		public static bool CheckInventoryNotify(NKMUserData userData)
		{
			foreach (NKMItemMiscData nkmitemMiscData in userData.m_InventoryData.MiscItems.Values)
			{
				if (nkmitemMiscData.TotalCount > 0L)
				{
					NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(nkmitemMiscData.ItemID);
					if (itemMiscTempletByID != null && itemMiscTempletByID.IsUsable() && itemMiscTempletByID.IsTimeIntervalItem)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06005C90 RID: 23696 RVA: 0x001CA158 File Offset: 0x001C8358
		public static bool CheckCollectionNotify(NKMUserData userData)
		{
			if (NKCUnitMissionManager.GetOpenTagCollectionTeamUp())
			{
				foreach (NKMCollectionTeamUpGroupTemplet nkmcollectionTeamUpGroupTemplet in NKMTempletContainer<NKMCollectionTeamUpGroupTemplet>.Values)
				{
					if (nkmcollectionTeamUpGroupTemplet != null)
					{
						int unitCollectCount = userData.m_ArmyData.GetUnitCollectCount(nkmcollectionTeamUpGroupTemplet.UnitIDList);
						if (userData.m_ArmyData.GetTeamCollectionData(nkmcollectionTeamUpGroupTemplet.TeamID) == null && nkmcollectionTeamUpGroupTemplet.RewardCriteria <= unitCollectCount)
						{
							return true;
						}
					}
				}
			}
			return NKCUnitMissionManager.HasRewardEnableMission();
		}

		// Token: 0x06005C91 RID: 23697 RVA: 0x001CA1E8 File Offset: 0x001C83E8
		public static bool CheckFactoryNotify(NKMUserData userData)
		{
			bool flag;
			if (NKCContentManager.CheckContentStatus(ContentsType.BASE_FACTORY, out flag, 0, 0) != NKCContentManager.eContentStatus.Open)
			{
				return false;
			}
			foreach (KeyValuePair<byte, NKMCraftSlotData> keyValuePair in userData.m_CraftData.SlotList)
			{
				if (keyValuePair.Value.GetState(NKCSynchronizedTime.GetServerUTCTime(0.0)) == NKM_CRAFT_SLOT_STATE.NECSS_COMPLETED)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005C92 RID: 23698 RVA: 0x001CA26C File Offset: 0x001C846C
		public static bool CheckScoutNotify(NKMUserData userData)
		{
			foreach (NKMPieceTemplet nkmpieceTemplet in NKMTempletContainer<NKMPieceTemplet>.Values)
			{
				if (NKCUIScout.IsReddotNeeded(userData, nkmpieceTemplet.Key))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005C93 RID: 23699 RVA: 0x001CA2C8 File Offset: 0x001C84C8
		public static bool CheckWorldMapNotify(NKMUserData userData)
		{
			bool flag;
			if (NKCContentManager.CheckContentStatus(ContentsType.WORLDMAP, out flag, 0, 0) != NKCContentManager.eContentStatus.Open)
			{
				return false;
			}
			foreach (KeyValuePair<int, NKMWorldMapCityData> keyValuePair in userData.m_WorldmapData.worldMapCityDataMap)
			{
				NKMWorldMapCityData value = keyValuePair.Value;
				if (value != null && value.HasMission() && value.IsMissionFinished(NKCSynchronizedTime.GetServerUTCTime(0.0)))
				{
					return true;
				}
			}
			return NKCAlarmManager.CheckFierceDailyRewardNotify(userData) || NKCAlarmManager.CheckFierceRewardNotify(userData) || NKCAlarmManager.CheckRaidSeasonRewardNotify(userData);
		}

		// Token: 0x06005C94 RID: 23700 RVA: 0x001CA378 File Offset: 0x001C8578
		public static bool CheckMissionNotify(NKMUserData userData)
		{
			bool flag;
			return NKCContentManager.CheckContentStatus(ContentsType.LOBBY_SUBMENU, out flag, 0, 0) == NKCContentManager.eContentStatus.Open && NKMMissionManager.GetHaveClearedMission();
		}

		// Token: 0x06005C95 RID: 23701 RVA: 0x001CA398 File Offset: 0x001C8598
		public static bool CheckContractNotify(NKMUserData userData)
		{
			bool flag;
			if (NKCContentManager.CheckContentStatus(ContentsType.CONTRACT, out flag, 0, 0) != NKCContentManager.eContentStatus.Open)
			{
				return false;
			}
			NKCContractDataMgr nkccontractDataMgr = NKCScenManager.GetScenManager().GetNKCContractDataMgr();
			return nkccontractDataMgr.IsPossibleFreeChance() || nkccontractDataMgr.IsActiveNewFreeChance();
		}

		// Token: 0x06005C96 RID: 23702 RVA: 0x001CA3D4 File Offset: 0x001C85D4
		public static bool CheckOfficeDormNotify(NKMUserData userData)
		{
			bool flag;
			return NKCContentManager.CheckContentStatus(ContentsType.OFFICE, out flag, 0, 0) == NKCContentManager.eContentStatus.Open && (NKCAlarmManager.CheckOfficeCommunityNotify(userData) || NKCAlarmManager.CheckOfficeLoyaltyNotify(userData));
		}

		// Token: 0x06005C97 RID: 23703 RVA: 0x001CA408 File Offset: 0x001C8608
		public static bool CheckOfficeCommunityNotify(NKMUserData userData)
		{
			bool flag;
			if (NKCContentManager.CheckContentStatus(ContentsType.OFFICE, out flag, 0, 0) != NKCContentManager.eContentStatus.Open)
			{
				return false;
			}
			bool flag2 = userData.OfficeData.BizcardCount != 0;
			bool canReceiveBizcard = userData.OfficeData.CanReceiveBizcard;
			return (flag2 && canReceiveBizcard) || (NKCFriendManager.FriendList.Count > 0 && userData.OfficeData.CanSendBizcardBroadcast);
		}

		// Token: 0x06005C98 RID: 23704 RVA: 0x001CA464 File Offset: 0x001C8664
		public static bool CheckOfficeLoyaltyNotify(NKMUserData userData)
		{
			bool flag;
			if (NKCContentManager.CheckContentStatus(ContentsType.OFFICE, out flag, 0, 0) != NKCContentManager.eContentStatus.Open)
			{
				return false;
			}
			foreach (NKMOfficeRoom nkmofficeRoom in userData.OfficeData.Rooms)
			{
				if (nkmofficeRoom != null && nkmofficeRoom.unitUids != null)
				{
					foreach (long unitUid in nkmofficeRoom.unitUids)
					{
						NKMUnitData unitFromUID = userData.m_ArmyData.GetUnitFromUID(unitUid);
						if (unitFromUID != null && unitFromUID.CheckOfficeRoomHeartFull())
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06005C99 RID: 23705 RVA: 0x001CA52C File Offset: 0x001C872C
		public static bool CheckBaseNotify(NKMUserData userData)
		{
			bool flag;
			return NKCContentManager.CheckContentStatus(ContentsType.BASE, out flag, 0, 0) == NKCContentManager.eContentStatus.Open && (NKCAlarmManager.CheckHangarNotify(userData) || NKCAlarmManager.CheckFactoryNotify(userData) || NKCAlarmManager.CheckScoutNotify(userData));
		}

		// Token: 0x06005C9A RID: 23706 RVA: 0x001CA560 File Offset: 0x001C8760
		public static bool CheckShopNotify(NKMUserData userData)
		{
			bool flag;
			ShopReddotType shopReddotType;
			return NKCContentManager.CheckContentStatus(ContentsType.LOBBY_SUBMENU, out flag, 0, 0) == NKCContentManager.eContentStatus.Open && NKCShopManager.CheckTabReddotCount(out shopReddotType, "TAB_NONE", 0) > 0;
		}

		// Token: 0x06005C9B RID: 23707 RVA: 0x001CA58B File Offset: 0x001C878B
		public static bool CheckJukeBoxNotifiy()
		{
			return NKCContentManager.IsContentsUnlocked(ContentsType.BASE_PERSONNAL, 0, 0) && NKCUIJukeBox.HasNewMusic();
		}

		// Token: 0x06005C9C RID: 23708 RVA: 0x001CA5A0 File Offset: 0x001C87A0
		public static bool CheckleaderBoardNotify(NKMUserData userData)
		{
			bool flag;
			return userData != null && NKCContentManager.CheckContentStatus(ContentsType.LEADERBOARD, out flag, 0, 0) == NKCContentManager.eContentStatus.Open && NKCAlarmManager.CheckFierceDailyRewardNotify(userData);
		}

		// Token: 0x06005C9D RID: 23709 RVA: 0x001CA5C8 File Offset: 0x001C87C8
		public static bool CheckPVPNotify(NKMUserData userData)
		{
			DateTime serverUTCTime = NKCSynchronizedTime.GetServerUTCTime(0.0);
			PvpState pvpData = userData.m_PvpData;
			int num = NKCUtil.FindPVPSeasonIDForRank(serverUTCTime);
			if (num != 0)
			{
				int weekIDForRank = NKCPVPManager.GetWeekIDForRank(serverUTCTime, num);
				if (NKCPVPManager.CanRewardWeek(NKM_GAME_TYPE.NGT_PVP_RANK, pvpData, num, weekIDForRank, serverUTCTime) == NKM_ERROR_CODE.NEC_OK)
				{
					return true;
				}
				if (NKCPVPManager.CanRewardSeason(pvpData, num, serverUTCTime) == NKM_ERROR_CODE.NEC_OK)
				{
					return true;
				}
			}
			PvpState asyncData = userData.m_AsyncData;
			int num2 = NKCUtil.FindPVPSeasonIDForAsync(serverUTCTime);
			if (num2 != 0)
			{
				int weekIDForAsync = NKCPVPManager.GetWeekIDForAsync(serverUTCTime, num2);
				if (NKCPVPManager.CanRewardWeek(NKM_GAME_TYPE.NGT_ASYNC_PVP, asyncData, num2, weekIDForAsync, serverUTCTime) == NKM_ERROR_CODE.NEC_OK)
				{
					return true;
				}
				if (NKCPVPManager.CanRewardSeason(asyncData, num2, serverUTCTime) == NKM_ERROR_CODE.NEC_OK)
				{
					return true;
				}
			}
			PvpState leagueData = userData.m_LeagueData;
			int num3 = NKCUtil.FindPVPSeasonIDForLeague(serverUTCTime);
			if (num3 != 0)
			{
				int weekIDForLeague = NKCPVPManager.GetWeekIDForLeague(serverUTCTime, num3);
				if (NKCPVPManager.CanRewardWeek(NKM_GAME_TYPE.NGT_PVP_LEAGUE, leagueData, num3, weekIDForLeague, serverUTCTime) == NKM_ERROR_CODE.NEC_OK)
				{
					return true;
				}
				if (NKCPVPManager.CanRewardSeason(leagueData, num3, serverUTCTime) == NKM_ERROR_CODE.NEC_OK)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005C9E RID: 23710 RVA: 0x001CA694 File Offset: 0x001C8894
		public static bool CheckFierceRedDot()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			return nkmuserData != null && (NKCAlarmManager.CheckFierceDailyRewardNotify(nkmuserData) || NKCAlarmManager.CheckFierceRewardNotify(nkmuserData));
		}

		// Token: 0x06005C9F RID: 23711 RVA: 0x001CA6C4 File Offset: 0x001C88C4
		public static bool CheckFierceDailyRewardNotify(NKMUserData userData)
		{
			NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
			bool flag;
			return nkcfierceBattleSupportDataMgr != null && NKCContentManager.CheckContentStatus(ContentsType.FIERCE, out flag, 0, 0) == NKCContentManager.eContentStatus.Open && nkcfierceBattleSupportDataMgr.GetStatus() == NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_ACTIVATE && NKCScenManager.CurrentUserData().GetStatePlayCnt(NKMFierceConst.StageId, true, false, false) > 0 && !nkcfierceBattleSupportDataMgr.m_fierceDailyRewardReceived;
		}

		// Token: 0x06005CA0 RID: 23712 RVA: 0x001CA71C File Offset: 0x001C891C
		public static bool CheckFierceRewardNotify(NKMUserData userData)
		{
			NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
			if (nkcfierceBattleSupportDataMgr != null)
			{
				NKCFierceBattleSupportDataMgr.FIERCE_STATUS status = nkcfierceBattleSupportDataMgr.GetStatus();
				if (status == NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_ACTIVATE)
				{
					return nkcfierceBattleSupportDataMgr.IsCanReceivePointReward();
				}
				if (status == NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_REWARD)
				{
					return nkcfierceBattleSupportDataMgr.IsPossibleRankReward();
				}
			}
			return false;
		}

		// Token: 0x06005CA1 RID: 23713 RVA: 0x001CA758 File Offset: 0x001C8958
		public static bool CheckRaidSeasonRewardNotify(NKMUserData userData)
		{
			foreach (NKMRaidSeasonRewardTemplet nkmraidSeasonRewardTemplet in NKMRaidSeasonRewardTemplet.Values)
			{
				if (nkmraidSeasonRewardTemplet.RaidPoint > NKCRaidSeasonManager.RaidSeason.recvRewardRaidPoint && nkmraidSeasonRewardTemplet.RaidPoint <= NKCRaidSeasonManager.RaidSeason.monthlyPoint)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005CA2 RID: 23714 RVA: 0x001CA7C8 File Offset: 0x001C89C8
		public static void SetServiceShutdownTime(DateTime UTCTime)
		{
			NKCAlarmManager.s_bServerShutdownScheduled = true;
			NKCAlarmManager.s_dtServerShutdownUTCTime = UTCTime;
			DateTime dateTime = UTCTime.ToLocalTime();
			string message = string.Format(NKCStringTable.GetString("SI_SERVER_SHUTDOWN_TIME", false), dateTime.Hour, dateTime.Minute);
			NKCUIPopupMessageServer.Instance.Open(NKCUIPopupMessageServer.eMessageStyle.Slide, message, 1);
		}

		// Token: 0x06005CA3 RID: 23715 RVA: 0x001CA820 File Offset: 0x001C8A20
		public static bool CheckAlarmByShortcut(NKM_SHORTCUT_TYPE shortcutType, string shortCutParam)
		{
			int num;
			if (!int.TryParse(shortCutParam, out num))
			{
				num = -1;
			}
			if (shortcutType != NKM_SHORTCUT_TYPE.SHORTCUT_EVENT)
			{
				if (shortcutType == NKM_SHORTCUT_TYPE.SHORTCUT_PT_EXCHANGE)
				{
					NKMPointExchangeTemplet nkmpointExchangeTemplet;
					if (num > 0)
					{
						nkmpointExchangeTemplet = NKMPointExchangeTemplet.Find(num);
					}
					else
					{
						nkmpointExchangeTemplet = NKMPointExchangeTemplet.GetByTime(NKCSynchronizedTime.ServiceTime);
					}
					return nkmpointExchangeTemplet != null && NKCAlarmManager.CheckShopCanPurchase(nkmpointExchangeTemplet.ShopTabStrId, nkmpointExchangeTemplet.ShopTabSubIndex);
				}
				if (shortcutType == NKM_SHORTCUT_TYPE.SHORTCUT_EVENT_COLLECTION)
				{
					NKMEventCollectionIndexTemplet nkmeventCollectionIndexTemplet;
					if (num > 0)
					{
						nkmeventCollectionIndexTemplet = NKMEventCollectionIndexTemplet.Find(num);
					}
					else
					{
						nkmeventCollectionIndexTemplet = NKCAlarmManager.GetEventCollectionIndexTemplet();
					}
					return nkmeventCollectionIndexTemplet != null && NKCAlarmManager.HasCompletableMission(nkmeventCollectionIndexTemplet.MissionTabIds);
				}
			}
			else if (num > 0)
			{
				return NKMEventManager.CheckRedDot(NKMEventTabTemplet.Find(num));
			}
			return false;
		}

		// Token: 0x06005CA4 RID: 23716 RVA: 0x001CA8B0 File Offset: 0x001C8AB0
		public static bool CheckShopCanPurchase(string shopTabStrId, int shopTabSubIndex)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			List<ShopItemTemplet> itemTempletListByTab = NKCShopManager.GetItemTempletListByTab(ShopTabTemplet.Find(shopTabStrId, shopTabSubIndex), false);
			if (itemTempletListByTab == null || nkmuserData == null)
			{
				return false;
			}
			int count = itemTempletListByTab.Count;
			for (int i = 0; i < count; i++)
			{
				if (itemTempletListByTab[i] != null)
				{
					NKMShopData shopData = nkmuserData.m_ShopData;
					bool flag = nkmuserData.m_InventoryData.GetCountMiscItem(itemTempletListByTab[i].m_PriceItemID) >= (long)itemTempletListByTab[i].m_Price;
					bool flag2 = true;
					if (shopData.histories.ContainsKey(itemTempletListByTab[i].m_ProductID))
					{
						flag2 = (shopData.histories[itemTempletListByTab[i].m_ProductID].purchaseCount < itemTempletListByTab[i].m_QuantityLimit);
					}
					if (flag2 && flag)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06005CA5 RID: 23717 RVA: 0x001CA988 File Offset: 0x001C8B88
		public static bool HasCompletableMission(IEnumerable<int> lstMissionTab)
		{
			if (lstMissionTab == null)
			{
				return false;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return false;
			}
			NKMUserMissionData missionData = myUserData.m_MissionData;
			if (missionData == null)
			{
				return false;
			}
			foreach (int nkm_MISSION_TAB_ID in lstMissionTab)
			{
				if (missionData.CheckCompletableMission(myUserData, nkm_MISSION_TAB_ID, false))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005CA6 RID: 23718 RVA: 0x001CAA00 File Offset: 0x001C8C00
		public static NKMEventCollectionIndexTemplet GetEventCollectionIndexTemplet()
		{
			foreach (NKMEventCollectionIndexTemplet nkmeventCollectionIndexTemplet in NKMEventCollectionIndexTemplet.Values)
			{
				if (nkmeventCollectionIndexTemplet != null && nkmeventCollectionIndexTemplet.IsOpen)
				{
					NKMIntervalTemplet nkmintervalTemplet = NKMIntervalTemplet.Find(nkmeventCollectionIndexTemplet.DateStrId);
					if (nkmintervalTemplet != null && nkmintervalTemplet.IsValidTime(NKCSynchronizedTime.ServiceTime))
					{
						return nkmeventCollectionIndexTemplet;
					}
				}
			}
			return null;
		}

		// Token: 0x040048E6 RID: 18662
		private static Dictionary<NKCAlarmManager.ALARM_TYPE, NKCAlarmManager.OnNotify> m_dicNotify = new Dictionary<NKCAlarmManager.ALARM_TYPE, NKCAlarmManager.OnNotify>(21);

		// Token: 0x040048E7 RID: 18663
		private static bool m_bInitComplete = false;

		// Token: 0x040048E8 RID: 18664
		private static bool s_bServerShutdownScheduled = false;

		// Token: 0x040048E9 RID: 18665
		private static DateTime s_dtServerShutdownUTCTime;

		// Token: 0x040048EA RID: 18666
		private static DateTime s_dtServerShutdownNextAlarmUTCTime;

		// Token: 0x020015A5 RID: 5541
		public enum ALARM_TYPE
		{
			// Token: 0x0400A230 RID: 41520
			MAIL,
			// Token: 0x0400A231 RID: 41521
			FRIEND,
			// Token: 0x0400A232 RID: 41522
			HANGAR,
			// Token: 0x0400A233 RID: 41523
			INVENTORY,
			// Token: 0x0400A234 RID: 41524
			COLLECTION,
			// Token: 0x0400A235 RID: 41525
			FACTORY,
			// Token: 0x0400A236 RID: 41526
			WORLDMAP,
			// Token: 0x0400A237 RID: 41527
			MISSION,
			// Token: 0x0400A238 RID: 41528
			CONTRACT,
			// Token: 0x0400A239 RID: 41529
			OFFICE_DORM,
			// Token: 0x0400A23A RID: 41530
			CHAT,
			// Token: 0x0400A23B RID: 41531
			LEADERBOARD,
			// Token: 0x0400A23C RID: 41532
			PVP,
			// Token: 0x0400A23D RID: 41533
			BASE,
			// Token: 0x0400A23E RID: 41534
			ALL,
			// Token: 0x0400A23F RID: 41535
			Count,
			// Token: 0x0400A240 RID: 41536
			AT_END
		}

		// Token: 0x020015A6 RID: 5542
		// (Invoke) Token: 0x0600ADD1 RID: 44497
		public delegate bool OnNotify(NKMUserData userData);
	}
}
