using System;
using System.Collections.Generic;
using ClientPacket.Guild;
using ClientPacket.Warfare;
using ClientPacket.WorldMap;
using NKC.UI.Option;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006DA RID: 1754
	public static class NKCTutorialManager
	{
		// Token: 0x06003D41 RID: 15681 RVA: 0x0013AE7A File Offset: 0x0013907A
		public static bool CheckTutoGameCondAtLogin(NKMUserData userData)
		{
			return userData != null && ((userData.m_eAuthLevel == NKM_USER_AUTH_LEVEL.NORMAL_USER || userData.m_eAuthLevel == NKM_USER_AUTH_LEVEL.NORMAL_ADMIN) && userData.UserLevel == 1);
		}

		// Token: 0x06003D42 RID: 15682 RVA: 0x0013AE9F File Offset: 0x0013909F
		public static bool IsTutorialDungeon(int dungeonID)
		{
			return NKMDungeonManager.IsTutorialDungeon(dungeonID);
		}

		// Token: 0x06003D43 RID: 15683 RVA: 0x0013AEA7 File Offset: 0x001390A7
		public static bool IsPrologueDungeon(int dungeonID)
		{
			return dungeonID - 1004 <= 2;
		}

		// Token: 0x06003D44 RID: 15684 RVA: 0x0013AEB6 File Offset: 0x001390B6
		public static bool CanGiveupDungeon(int dungeonID)
		{
			return dungeonID - 1004 > 3 || NKCScenManager.CurrentUserData().CheckDungeonClear(dungeonID);
		}

		// Token: 0x06003D45 RID: 15685 RVA: 0x0013AED0 File Offset: 0x001390D0
		public static bool TutorialRequired(TutorialStep step)
		{
			foreach (KeyValuePair<int, NKCTutorialReqTemplet> keyValuePair in NKCTutorialManager.m_dicReqTemplet)
			{
				NKCTutorialReqTemplet value = keyValuePair.Value;
				if (step == value.Step)
				{
					return NKCTutorialManager.CheckTutorial(value);
				}
			}
			return false;
		}

		// Token: 0x06003D46 RID: 15686 RVA: 0x0013AF38 File Offset: 0x00139138
		public static void TutorialRequiredByLastPoint()
		{
			if (NKCTutorialManager.lastPoint != TutorialPoint.None)
			{
				NKCTutorialManager.TutorialRequired(NKCTutorialManager.lastPoint, true);
			}
		}

		// Token: 0x06003D47 RID: 15687 RVA: 0x0013AF50 File Offset: 0x00139150
		public static TutorialStep TutorialRequired(TutorialPoint point, bool play = true)
		{
			NKCTutorialManager.lastPoint = TutorialPoint.None;
			foreach (KeyValuePair<int, NKCTutorialReqTemplet> keyValuePair in NKCTutorialManager.m_dicReqTemplet)
			{
				NKCTutorialReqTemplet value = keyValuePair.Value;
				if (value.EventPoint == point && NKCTutorialManager.CheckTutorial(value))
				{
					if (play)
					{
						NKCTutorialManager.PlayTutorial(value.EventID);
					}
					NKCTutorialManager.lastPoint = point;
					return value.Step;
				}
			}
			return TutorialStep.None;
		}

		// Token: 0x06003D48 RID: 15688 RVA: 0x0013AFDC File Offset: 0x001391DC
		public static bool TutorialCompleted(TutorialStep step)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			return myUserData == null || NKMTutorialManager.IsTutorialCompleted(step, myUserData);
		}

		// Token: 0x06003D49 RID: 15689 RVA: 0x0013B000 File Offset: 0x00139200
		public static bool EveryTutorialCompleted()
		{
			return NKCTutorialManager.TutorialCompleted(TutorialStep.FactoryTuning) && NKCTutorialManager.TutorialCompleted(TutorialStep.RaidEvent);
		}

		// Token: 0x06003D4A RID: 15690 RVA: 0x0013B01A File Offset: 0x0013921A
		public static bool IsCloseDailyContents()
		{
			return !NKCTutorialManager.TutorialCompleted(TutorialStep.Achieventment) && !NKCContentManager.IsContentsUnlocked(ContentsType.LOBBY_SUBMENU, 0, 0);
		}

		// Token: 0x06003D4B RID: 15691 RVA: 0x0013B032 File Offset: 0x00139232
		public static void CompleteTutorial(TutorialStep step)
		{
			NKCTutorialManager.CompleteTutorial((int)step);
		}

		// Token: 0x06003D4C RID: 15692 RVA: 0x0013B03C File Offset: 0x0013923C
		public static void CompleteTutorial(int missionID)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			if (myUserData.m_MissionData.GetCompletedMissionData(missionID) != null)
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_MISSION_COMPLETE_REQ(NKMMissionManager.GetMissionTabTemplet(NKM_MISSION_TYPE.TUTORIAL).m_tabID, missionID, missionID);
		}

		// Token: 0x06003D4D RID: 15693 RVA: 0x0013B079 File Offset: 0x00139279
		public static void PlayTutorial(int eventID)
		{
			Debug.Log("Tutorial Playing : " + eventID.ToString());
			if (NKCUIGameOption.IsInstanceOpen)
			{
				NKCUIGameOption.Instance.Close();
			}
			if (NKCGameEventManager.GetCurrentEventID() != eventID)
			{
				NKCGameEventManager.PlayGameEvent(eventID, false, null);
			}
			NKCAdjustManager.OnPlayTutorial(eventID);
		}

		// Token: 0x06003D4E RID: 15694 RVA: 0x0013B0B8 File Offset: 0x001392B8
		public static void PlayTutorial(TutorialStep step)
		{
			Debug.Log("Tutorial Playing : " + step.ToString());
			NKCTutorialManager.PlayTutorial((int)step);
		}

		// Token: 0x06003D4F RID: 15695 RVA: 0x0013B0DC File Offset: 0x001392DC
		public static bool LoadFromLua()
		{
			NKCTutorialManager.m_dicReqTemplet = NKMTempletLoader.LoadDictionary<NKCTutorialReqTemplet>("ab_script", "LUA_TUTORIAL_REQ_TEMPLET", "m_TutorialReqTable", new Func<NKMLua, NKCTutorialReqTemplet>(NKCTutorialReqTemplet.LoadFromLUA));
			return true;
		}

		// Token: 0x06003D50 RID: 15696 RVA: 0x0013B104 File Offset: 0x00139304
		public static bool CheckTutorial(NKCTutorialReqTemplet templet)
		{
			if (NKCTutorialManager.TutorialCompleted(templet.Step))
			{
				return false;
			}
			foreach (KeyValuePair<TutorialReq, string> keyValuePair in templet.dicReq)
			{
				if (!NKCTutorialManager.CheckReq(templet.EventID, templet.Step, keyValuePair.Key, keyValuePair.Value))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003D51 RID: 15697 RVA: 0x0013B188 File Offset: 0x00139388
		private static bool CheckReq(int eventID, TutorialStep step, TutorialReq req, string value)
		{
			if (req == TutorialReq.None)
			{
				return true;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return false;
			}
			if (myUserData.m_eAuthLevel == NKM_USER_AUTH_LEVEL.SUPER_ADMIN)
			{
				return false;
			}
			switch (req)
			{
			case TutorialReq.EventClear:
			{
				TutorialStep step2;
				bool flag = Enum.TryParse<TutorialStep>(value, out step2);
				int step3;
				bool flag2 = int.TryParse(value, out step3);
				if (flag)
				{
					if (!NKCTutorialManager.TutorialCompleted(step2))
					{
						return false;
					}
				}
				else
				{
					if (!flag2)
					{
						return false;
					}
					if (!NKCTutorialManager.TutorialCompleted((TutorialStep)step3))
					{
						return false;
					}
				}
				break;
			}
			case TutorialReq.DungeonClear:
				if (!myUserData.CheckDungeonClear(value))
				{
					return false;
				}
				break;
			case TutorialReq.WarfarePlay:
			{
				WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
				if (warfareGameData == null)
				{
					return false;
				}
				NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(warfareGameData.warfareTempletID);
				if (nkmwarfareTemplet == null)
				{
					return false;
				}
				if (nkmwarfareTemplet.m_WarfareStrID != value)
				{
					return false;
				}
				break;
			}
			case TutorialReq.WarfareClear:
				if (!myUserData.CheckWarfareClear(value))
				{
					return false;
				}
				break;
			case TutorialReq.WarfareUseSupply:
			{
				WarfareGameData warfareGameData2 = NKCScenManager.GetScenManager().WarfareGameData;
				if (warfareGameData2 == null)
				{
					return false;
				}
				NKMWarfareTemplet nkmwarfareTemplet2 = NKMWarfareTemplet.Find(warfareGameData2.warfareTempletID);
				if (nkmwarfareTemplet2 == null)
				{
					return false;
				}
				if (nkmwarfareTemplet2.m_WarfareStrID != value)
				{
					return false;
				}
				if (warfareGameData2.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_PLAYING)
				{
					return false;
				}
				bool flag3 = false;
				foreach (WarfareUnitData warfareUnitData in warfareGameData2.warfareTeamDataA.warfareUnitDataByUIDMap.Values)
				{
					if (warfareUnitData.hp > 0f && warfareUnitData.supply < 2)
					{
						flag3 = true;
						break;
					}
				}
				if (!flag3)
				{
					return false;
				}
				break;
			}
			case TutorialReq.WarfareSupplyTile:
			{
				WarfareGameData warfareGameData3 = NKCScenManager.GetScenManager().WarfareGameData;
				if (warfareGameData3 == null)
				{
					return false;
				}
				NKMWarfareTemplet nkmwarfareTemplet3 = NKMWarfareTemplet.Find(warfareGameData3.warfareTempletID);
				if (nkmwarfareTemplet3 == null)
				{
					return false;
				}
				if (nkmwarfareTemplet3.m_WarfareStrID != value)
				{
					return false;
				}
				if (warfareGameData3.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_PLAYING)
				{
					return false;
				}
				bool flag4 = false;
				foreach (WarfareUnitData warfareUnitData2 in warfareGameData3.warfareTeamDataA.warfareUnitDataByUIDMap.Values)
				{
					if (warfareUnitData2.hp != 0f)
					{
						WarfareTileData tileData = warfareGameData3.GetTileData((int)warfareUnitData2.tileIndex);
						if (tileData != null && tileData.tileType == NKM_WARFARE_MAP_TILE_TYPE.NWMTT_RESUPPLY)
						{
							flag4 = true;
							break;
						}
					}
				}
				if (!flag4)
				{
					return false;
				}
				break;
			}
			case TutorialReq.WarfareSquadCount:
			{
				WarfareGameData warfareGameData4 = NKCScenManager.GetScenManager().WarfareGameData;
				if (warfareGameData4 == null)
				{
					return false;
				}
				if (warfareGameData4.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_PLAYING)
				{
					return false;
				}
				int num;
				if (!int.TryParse(value, out num))
				{
					return false;
				}
				if (warfareGameData4.warfareTeamDataA.warfareUnitDataByUIDMap.Count < num)
				{
					NKCTutorialManager.CompleteTutorial(step);
					return false;
				}
				break;
			}
			case TutorialReq.UnlockDeckCount:
				if (myUserData.m_ArmyData.GetUnlockedDeckCount(NKM_DECK_TYPE.NDT_NORMAL) == myUserData.m_ArmyData.GetMaxDeckCount(NKM_DECK_TYPE.NDT_NORMAL))
				{
					NKCTutorialManager.CompleteTutorial(step);
					return false;
				}
				break;
			case TutorialReq.DeckSlotEmpty:
			{
				string[] array = value.Split(new char[]
				{
					','
				});
				List<int> list = new List<int>();
				for (int i = 0; i < array.Length; i++)
				{
					int item;
					if (int.TryParse(array[i], out item))
					{
						list.Add(item);
					}
				}
				if (list.Count < 1)
				{
					return false;
				}
				NKMDeckData deckData = myUserData.m_ArmyData.GetDeckData(NKM_DECK_TYPE.NDT_NORMAL, list[0]);
				if (deckData == null)
				{
					return false;
				}
				for (int j = 1; j < list.Count; j++)
				{
					int num2 = list[j];
					if (num2 < 0 || num2 >= deckData.m_listDeckUnitUID.Count)
					{
						return false;
					}
					if (deckData.m_listDeckUnitUID[num2] != 0L)
					{
						NKCTutorialManager.CompleteTutorial(step);
						return false;
					}
				}
				break;
			}
			case TutorialReq.WorldMapCityLevel:
			{
				int num3;
				if (!int.TryParse(value, out num3))
				{
					return false;
				}
				using (Dictionary<int, NKMWorldMapCityData>.ValueCollection.Enumerator enumerator2 = myUserData.m_WorldmapData.worldMapCityDataMap.Values.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.level >= num3)
						{
							return true;
						}
					}
				}
				return false;
			}
			case TutorialReq.FierceCond:
			{
				NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
				if (nkcfierceBattleSupportDataMgr != null)
				{
					NKCFierceBattleSupportDataMgr.FIERCE_STATUS status = nkcfierceBattleSupportDataMgr.GetStatus();
					int num4;
					if (int.TryParse(value, out num4))
					{
						if (status != NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_ACTIVATE)
						{
							if (status - NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_REWARD <= 1)
							{
								if (num4 == 2)
								{
									return true;
								}
							}
						}
						else if (num4 == 1)
						{
							return true;
						}
						return false;
					}
				}
				return false;
			}
			case TutorialReq.GuildDungeonCond:
			{
				int num5;
				bool flag5 = int.TryParse(value, out num5);
				GuildDungeonState guildDungeonState = NKCGuildCoopManager.m_GuildDungeonState;
				if (flag5)
				{
					switch (guildDungeonState)
					{
					case GuildDungeonState.Invalid:
					case GuildDungeonState.SeasonOut:
					case GuildDungeonState.SessionOut:
						if (num5 == 0)
						{
							return true;
						}
						break;
					case GuildDungeonState.PlayableGuildDungeon:
						if (num5 == 1)
						{
							return true;
						}
						break;
					}
					return false;
				}
				return false;
			}
			}
			return true;
		}

		// Token: 0x04003696 RID: 13974
		private static Dictionary<int, NKCTutorialReqTemplet> m_dicReqTemplet = new Dictionary<int, NKCTutorialReqTemplet>();

		// Token: 0x04003697 RID: 13975
		private static TutorialPoint lastPoint = TutorialPoint.None;
	}
}
