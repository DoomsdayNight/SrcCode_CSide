using System;
using System.Collections;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Community;
using ClientPacket.Game;
using ClientPacket.Mode;
using ClientPacket.Pvp;
using ClientPacket.User;
using ClientPacket.Warfare;
using NKC.Publisher;
using NKC.UI;
using NKC.UI.Event;
using NKC.UI.Fierce;
using NKC.UI.Friend;
using NKC.UI.Lobby;
using NKC.UI.Module;
using NKM;
using NKM.Event;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200071E RID: 1822
	public class NKC_SCEN_HOME : NKC_SCEN_BASIC
	{
		// Token: 0x06004832 RID: 18482 RVA: 0x0015CC9C File Offset: 0x0015AE9C
		public NKC_SCEN_HOME()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_HOME;
		}

		// Token: 0x06004833 RID: 18483 RVA: 0x0015CCC8 File Offset: 0x0015AEC8
		public void SetFriendNewIcon(bool bSet)
		{
			this.m_bHaveNewFriendRequest = bSet;
		}

		// Token: 0x06004834 RID: 18484 RVA: 0x0015CCD4 File Offset: 0x0015AED4
		public bool GetHasNewFriendRequest()
		{
			bool flag;
			return NKCContentManager.CheckContentStatus(ContentsType.FRIENDS, out flag, 0, 0) == NKCContentManager.eContentStatus.Open && this.m_bHaveNewFriendRequest;
		}

		// Token: 0x06004835 RID: 18485 RVA: 0x0015CCF6 File Offset: 0x0015AEF6
		public void SetMentoringRewardAlarm()
		{
		}

		// Token: 0x06004836 RID: 18486 RVA: 0x0015CCF8 File Offset: 0x0015AEF8
		public void SetAttendanceRequired(bool bSet)
		{
			this.m_bAttendanceRequired = bSet;
			if (this.m_UILobby != null && this.m_UILobby.IsOpen)
			{
				NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
				this.m_UILobby.UpdateButton(NKCUILobbyV2.eUIMenu.Attendance, myUserData);
			}
		}

		// Token: 0x06004837 RID: 18487 RVA: 0x0015CD40 File Offset: 0x0015AF40
		public bool GetAttendanceRequired()
		{
			return this.m_bAttendanceRequired;
		}

		// Token: 0x06004838 RID: 18488 RVA: 0x0015CD48 File Offset: 0x0015AF48
		public void SetReservedOpenUI(NKC_SCEN_HOME.RESERVE_OPEN_TYPE _reserveType, int _reservedID)
		{
			this.m_eReservedOpendUIType = _reserveType;
			this.m_iReservedOpenUIID = _reservedID;
		}

		// Token: 0x06004839 RID: 18489 RVA: 0x0015CD58 File Offset: 0x0015AF58
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			if (!NKCUIManager.IsValid(this.m_UILobbyData))
			{
				this.m_UILobbyData = NKCUILobbyV2.OpenNewInstanceAsync();
			}
		}

		// Token: 0x0600483A RID: 18490 RVA: 0x0015CD78 File Offset: 0x0015AF78
		public override void ScenLoadUIComplete()
		{
			base.ScenLoadUIComplete();
			if (this.m_UILobby == null)
			{
				if (this.m_UILobbyData == null || !this.m_UILobbyData.CheckLoadAndGetInstance<NKCUILobbyV2>(out this.m_UILobby))
				{
					Debug.LogError("Error - NKC_SCEN_HOME.ScenLoadComplete() : UI Load Failed!");
					return;
				}
				this.m_UILobby.Init();
			}
			this.SetAttendanceRequired(this.m_bAttendanceRequired);
		}

		// Token: 0x0600483B RID: 18491 RVA: 0x0015CDD8 File Offset: 0x0015AFD8
		public override void ScenStart()
		{
			base.ScenStart();
			NKCCamera.EnableBloom(true);
			NKCCamera.GetCamera().orthographic = false;
			this.Open();
			NKCCamera.GetTrackingPos().SetNowValue(0f, 0f, -1000f);
			NKCCamera.GetCamera().transform.position = new Vector3(0f, 0f, -1000f);
			NKCScenManager.GetScenManager().GetNKCRepeatOperaion().Init();
			NKCScenManager.GetScenManager().GetNKCRepeatOperaion().SetStopReason("");
			NKCScenManager.GetScenManager().Get_SCEN_OPERATION().PlayByFavorite = false;
			NKCScenManager.GetScenManager().Get_SCEN_OPERATION().SetReservedEpisodeCategory(EPISODE_CATEGORY.EC_COUNT);
			this.OnHomeEnter();
		}

		// Token: 0x0600483C RID: 18492 RVA: 0x0015CE88 File Offset: 0x0015B088
		public override void ScenEnd()
		{
			base.ScenEnd();
			if (this.m_UILobby.IsOpen)
			{
				this.m_UILobby.Close();
			}
			this.Close();
			if (this.ProcessCoroutine != null)
			{
				NKCScenManager.GetScenManager().StopCoroutine(this.ProcessCoroutine);
			}
			this.ProcessCoroutine = null;
			this.m_bRunningLobbyProcess = false;
			this.m_bWait = false;
			if (this.m_UILobby != null)
			{
				this.m_UILobby.Close();
			}
			this.m_UILobby = null;
			this.UnloadUI();
		}

		// Token: 0x0600483D RID: 18493 RVA: 0x0015CF0C File Offset: 0x0015B10C
		public override void UnloadUI()
		{
			base.UnloadUI();
			NKCUIManager.LoadedUIData uilobbyData = this.m_UILobbyData;
			if (uilobbyData != null)
			{
				uilobbyData.CloseInstance();
			}
			this.m_UILobbyData = null;
		}

		// Token: 0x0600483E RID: 18494 RVA: 0x0015CF2C File Offset: 0x0015B12C
		public void UpdateRightSide3DButton(NKCUILobbyV2.eUIMenu _eUIMenu)
		{
			if (this.m_UILobby != null && this.m_UILobby.IsOpen)
			{
				this.m_UILobby.UpdateButton(_eUIMenu, NKCScenManager.CurrentUserData());
			}
		}

		// Token: 0x0600483F RID: 18495 RVA: 0x0015CF5C File Offset: 0x0015B15C
		public void Open()
		{
			this.m_fElapsedTimeToRefreshDailyContents = 0f;
			this.m_UILobby.Open(NKCScenManager.GetScenManager().GetMyUserData());
			switch (this.m_eReservedOpendUIType)
			{
			case NKC_SCEN_HOME.RESERVE_OPEN_TYPE.ROT_MISSION:
				NKCUIMissionAchievement.Instance.Open(this.m_iReservedOpenUIID);
				break;
			case NKC_SCEN_HOME.RESERVE_OPEN_TYPE.ROT_RANKING_BOARD:
			{
				NKMLeaderBoardTemplet reservedTemplet = NKMTempletContainer<NKMLeaderBoardTemplet>.Find(this.m_ReservedRankingBoardID);
				NKCUILeaderBoard.Instance.Open(reservedTemplet, true);
				break;
			}
			case NKC_SCEN_HOME.RESERVE_OPEN_TYPE.ROT_GUIDE_MISSION:
				NKCUIMissionGuide.Instance.Open(this.m_eReservedGuideMissionTabID);
				break;
			case NKC_SCEN_HOME.RESERVE_OPEN_TYPE.ROT_EVENT_COLLECTION:
				NKCUIModuleHome.OpenEventModule(NKMTempletContainer<NKMEventCollectionIndexTemplet>.Find(this.m_iReservedOpenUIID));
				break;
			case NKC_SCEN_HOME.RESERVE_OPEN_TYPE.ROT_EVENT_BANNER:
			{
				NKMEventTabTemplet reservedTabTemplet = NKMEventTabTemplet.Find(this.m_iReservedOpenUIID);
				NKCUIEvent.Instance.Open(reservedTabTemplet);
				break;
			}
			}
			this.m_eReservedOpendUIType = NKC_SCEN_HOME.RESERVE_OPEN_TYPE.ROT_NONE;
			this.m_iReservedOpenUIID = 0;
			if (NKCDefineManager.DEFINE_ZLONG() && NKCDefineManager.DEFINE_ANDROID() && NKCDefineManager.DEFINE_USE_CHEAT() && !NKCDefineManager.DEFINE_UNITY_EDITOR())
			{
				Debug.Log(string.Concat(new string[]
				{
					"SystemInfo.deviceModel : ",
					SystemInfo.deviceModel,
					", Screen.safeArea.x : ",
					Screen.safeArea.x.ToString(),
					", Screen.safeArea.y : ",
					Screen.safeArea.y.ToString(),
					", Screen.safeArea.width : ",
					Screen.safeArea.width.ToString(),
					", Screen.safeArea.height : ",
					Screen.safeArea.height.ToString(),
					", Screen.currentResolution.width : ",
					Screen.currentResolution.width.ToString(),
					", Screen.currentResolution.height : ",
					Screen.currentResolution.height.ToString()
				}));
			}
		}

		// Token: 0x06004840 RID: 18496 RVA: 0x0015D13C File Offset: 0x0015B33C
		public void ForceOpenLobbyChange()
		{
			NKCUIManager.CloseAllPopup();
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKCUIChangeLobby.Instance.Open(myUserData);
		}

		// Token: 0x06004841 RID: 18497 RVA: 0x0015D164 File Offset: 0x0015B364
		public void Close()
		{
			NKCUIMissionAchievement.CheckInstanceAndClose();
			NKCUIMissionGuide.CheckInstanceAndClose();
		}

		// Token: 0x06004842 RID: 18498 RVA: 0x0015D170 File Offset: 0x0015B370
		public override void ScenLoadComplete()
		{
			base.ScenLoadComplete();
			NKCUtil.ClearGauntletCacheData(NKCScenManager.GetScenManager());
		}

		// Token: 0x06004843 RID: 18499 RVA: 0x0015D184 File Offset: 0x0015B384
		public override void ScenUpdate()
		{
			base.ScenUpdate();
			if (this.m_UILobby != null && this.m_UILobby.UseCameraTracking() && !NKCCamera.IsTrackingCameraPos() && !NKCUIChangeLobby.IsInstanceOpen)
			{
				NKCCamera.TrackingPos(10f, NKMRandom.Range(-50f, 50f), NKMRandom.Range(-50f, 50f), NKMRandom.Range(-1000f, -900f));
			}
			this.m_BloomIntensity.Update(Time.deltaTime);
			if (!this.m_BloomIntensity.IsTracking())
			{
				this.m_BloomIntensity.SetTracking(NKMRandom.Range(1f, 2f), 4f, TRACKING_DATA_TYPE.TDT_SLOWER);
			}
			NKCCamera.SetBloomIntensity(this.m_BloomIntensity.GetNowValue());
			if (this.m_bRunningLobbyProcess)
			{
				return;
			}
			if (NKCGameEventManager.IsEventPlaying() || NKCTutorialManager.IsCloseDailyContents())
			{
				return;
			}
			if (!NKCUIManager.IsAnyPopupOpened() && !NKMPopUpBox.IsOpenedWaitBox() && this.m_fElapsedTimeToRefreshDailyContents < Time.time)
			{
				this.m_fElapsedTimeToRefreshDailyContents = Time.time + 60f;
				this.CheckDailyContentsReset();
			}
		}

		// Token: 0x06004844 RID: 18500 RVA: 0x0015D290 File Offset: 0x0015B490
		private void CheckDailyContentsReset()
		{
			if (NKC_SCEN_HOME.m_bNeedNewsPopup && NKCNewsManager.CheckNeedNewsPopup(NKCSynchronizedTime.GetServerUTCTime(0.0)))
			{
				NKCUINews.Instance.SetDataAndOpen(NKC_SCEN_HOME.m_bNeedNewsPopup, eNewsFilterType.NOTICE, -1);
				NKC_SCEN_HOME.m_bNeedNewsPopup = false;
			}
			if (this.m_bNeedRefreshMail)
			{
				NKCMailManager.RefreshMailList();
				this.m_bNeedRefreshMail = false;
			}
			NKCContentManager.ShowContentUnlockPopup(null, Array.Empty<STAGE_UNLOCK_REQ_TYPE>());
		}

		// Token: 0x06004845 RID: 18501 RVA: 0x0015D2F0 File Offset: 0x0015B4F0
		private bool CheckAttandence()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return false;
			}
			bool flag;
			if (NKCContentManager.CheckContentStatus(ContentsType.LOBBY_SUBMENU, out flag, 0, 0) != NKCContentManager.eContentStatus.Open)
			{
				return false;
			}
			if (this.m_bReserverAttendance)
			{
				if (NKCUIAttendance.IsInstanceOpen)
				{
					NKCUIAttendance.Instance.Close();
				}
				this.SetAttendanceRequired(NKMAttendanceManager.CheckNeedAttendance(myUserData.m_AttendanceData, NKCSynchronizedTime.GetServerUTCTime(0.0), 0));
				this.OpenAttendanceUI(this.GetAttendanceKeyList());
			}
			return false;
		}

		// Token: 0x06004846 RID: 18502 RVA: 0x0015D364 File Offset: 0x0015B564
		public List<int> GetAttendanceKeyList()
		{
			List<int> list = new List<int>();
			if (this.m_bReserverAttendance && this.m_lstAttendance != null)
			{
				NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
				if (myUserData != null)
				{
					myUserData.m_AttendanceData.LastUpdateDate = new DateTime(this.m_lAttendanceUpdateTime);
					int i;
					Predicate<NKMAttendance> <>9__0;
					Predicate<NKMAttendance> <>9__1;
					Predicate<NKMAttendance> <>9__2;
					int j;
					for (i = 0; i < this.m_lstAttendance.Count; i = j + 1)
					{
						List<NKMAttendance> attList = myUserData.m_AttendanceData.AttList;
						Predicate<NKMAttendance> match;
						if ((match = <>9__0) == null)
						{
							match = (<>9__0 = ((NKMAttendance x) => x.IDX == this.m_lstAttendance[i].IDX));
						}
						if (attList.Find(match) == null)
						{
							myUserData.m_AttendanceData.AttList.Add(this.m_lstAttendance[i]);
						}
						else
						{
							List<NKMAttendance> attList2 = myUserData.m_AttendanceData.AttList;
							Predicate<NKMAttendance> match2;
							if ((match2 = <>9__1) == null)
							{
								match2 = (<>9__1 = ((NKMAttendance x) => x.IDX == this.m_lstAttendance[i].IDX));
							}
							attList2.Find(match2).Count = this.m_lstAttendance[i].Count;
							List<NKMAttendance> attList3 = myUserData.m_AttendanceData.AttList;
							Predicate<NKMAttendance> match3;
							if ((match3 = <>9__2) == null)
							{
								match3 = (<>9__2 = ((NKMAttendance x) => x.IDX == this.m_lstAttendance[i].IDX));
							}
							attList3.Find(match3).EventEndDate = this.m_lstAttendance[i].EventEndDate;
						}
						list.Add(this.m_lstAttendance[i].IDX);
						j = i;
					}
				}
				this.m_bReserverAttendance = false;
				this.m_lAttendanceUpdateTime = 0L;
				this.m_lstAttendance.Clear();
			}
			return list;
		}

		// Token: 0x06004847 RID: 18503 RVA: 0x0015D517 File Offset: 0x0015B717
		public void ReserveAttendanceData(List<NKMAttendance> lstAttendance, long updateTime)
		{
			if (lstAttendance.Count > 0)
			{
				this.m_bReserverAttendance = true;
			}
			else
			{
				this.m_bReserverAttendance = false;
			}
			this.m_lstAttendance = lstAttendance;
			this.m_lAttendanceUpdateTime = updateTime;
		}

		// Token: 0x06004848 RID: 18504 RVA: 0x0015D540 File Offset: 0x0015B740
		public void OpenAttendanceUI(List<int> lstAttendanceKey)
		{
			if (this.m_UILobby != null && this.m_UILobby.IsOpen)
			{
				this.m_UILobby.SetUIVisible(true);
			}
			NKCUIAttendance.Instance.Open(lstAttendanceKey);
		}

		// Token: 0x06004849 RID: 18505 RVA: 0x0015D574 File Offset: 0x0015B774
		public override bool ScenMsgProc(NKCMessageData cNKCMessageData)
		{
			return false;
		}

		// Token: 0x0600484A RID: 18506 RVA: 0x0015D578 File Offset: 0x0015B778
		private void CheckFierceNotice()
		{
			bool flag;
			if (NKCContentManager.CheckContentStatus(ContentsType.WORLDMAP, out flag, 0, 0) != NKCContentManager.eContentStatus.Open || NKCContentManager.CheckContentStatus(ContentsType.FIERCE, out flag, 0, 0) != NKCContentManager.eContentStatus.Open)
			{
				return;
			}
			NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
			if (nkcfierceBattleSupportDataMgr == null)
			{
				return;
			}
			if (nkcfierceBattleSupportDataMgr.GetStatus() != NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_ACTIVATE)
			{
				return;
			}
			string key = "FIERCE_KEY_" + NKCScenManager.CurrentUserData().m_UserUID.ToString();
			if (!PlayerPrefs.HasKey(key))
			{
				PlayerPrefs.SetString(key, nkcfierceBattleSupportDataMgr.GetFierceBattleID().ToString());
			}
			else
			{
				string @string = PlayerPrefs.GetString(key, "");
				string[] array = @string.Split(new char[]
				{
					':'
				});
				for (int i = 0; i < array.Length; i++)
				{
					if (string.Equals(array[i], nkcfierceBattleSupportDataMgr.GetFierceBattleID().ToString()))
					{
						return;
					}
				}
				PlayerPrefs.SetString(key, @string + ":" + nkcfierceBattleSupportDataMgr.GetFierceBattleID().ToString());
			}
			NKCUIPopupFierceBattleNotice.Instance.Open();
		}

		// Token: 0x0600484B RID: 18507 RVA: 0x0015D668 File Offset: 0x0015B868
		private void CheckGauntletLeagueTopPlayers()
		{
			if (PlayerPrefs.HasKey(NKCPVPManager.GetLeagueTop3Key()))
			{
				long ticks = long.Parse(PlayerPrefs.GetString(NKCPVPManager.GetLeagueTop3Key()));
				if (new DateTime(ticks) < NKMTime.GetResetTime(NKCSynchronizedTime.GetServerUTCTime(0.0), NKMTime.TimePeriod.Week))
				{
					PlayerPrefs.SetString(NKCPVPManager.GetLeagueTop3Key(), NKCSynchronizedTime.GetServerUTCTime(0.0).Ticks.ToString());
					NKCPacketSender.Send_NKMPacket_LEAGUE_PVP_WEEKLY_RANKER_REQ();
					this.m_bWaitGauntletLeagueTopAck = true;
					return;
				}
			}
			else
			{
				PlayerPrefs.SetString(NKCPVPManager.GetLeagueTop3Key(), NKCSynchronizedTime.GetServerUTCTime(0.0).Ticks.ToString());
				NKCPacketSender.Send_NKMPacket_LEAGUE_PVP_WEEKLY_RANKER_REQ();
				this.m_bWaitGauntletLeagueTopAck = true;
			}
		}

		// Token: 0x0600484C RID: 18508 RVA: 0x0015D71B File Offset: 0x0015B91B
		public void OnRecv(NKMPacket_GAME_LOAD_ACK cNKMPacket_GAME_LOAD_ACK, int multiply = 1)
		{
			NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.UpdateItemInfo(cNKMPacket_GAME_LOAD_ACK.costItemDataList);
			NKCScenManager.GetScenManager().GetGameClient().SetGameDataDummy(cNKMPacket_GAME_LOAD_ACK.gameData, false);
			NKCUtil.PlayStartCutscenAndStartGame(cNKMPacket_GAME_LOAD_ACK.gameData);
		}

		// Token: 0x0600484D RID: 18509 RVA: 0x0015D758 File Offset: 0x0015B958
		public void OnRecv(NKMPacket_PVP_CHARGE_POINT_REFRESH_ACK cNKMPacket_PVP_CHARGE_POINT_REFRESH_ACK)
		{
			if (this.m_UILobby != null)
			{
				this.m_UILobby.UpdateButton(NKCUILobbyV2.eUIMenu.PVP, NKCScenManager.CurrentUserData());
			}
		}

		// Token: 0x0600484E RID: 18510 RVA: 0x0015D779 File Offset: 0x0015B979
		public void OnRecv(NKMPacket_WARFARE_EXPIRED_NOT cNKMPacket_WARFARE_EXPIRED_NOT)
		{
			if (this.m_UILobby != null)
			{
				this.m_UILobby.UpdateButton(NKCUILobbyV2.eUIMenu.Operation, NKCScenManager.CurrentUserData());
			}
		}

		// Token: 0x0600484F RID: 18511 RVA: 0x0015D79A File Offset: 0x0015B99A
		public void OnRecv(NKMPacket_DIVE_EXPIRE_NOT cNKMPacket_DIVE_EXPIRE_NOT)
		{
			if (this.m_UILobby != null)
			{
				this.m_UILobby.UpdateButton(NKCUILobbyV2.eUIMenu.Worldmap, NKCScenManager.CurrentUserData());
			}
		}

		// Token: 0x06004850 RID: 18512 RVA: 0x0015D7BC File Offset: 0x0015B9BC
		public void OnRecv(NKMPacket_MENTORING_DATA_ACK sPacket)
		{
			NKMMentoringTemplet currentTempet = NKCMentoringUtil.GetCurrentTempet();
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (!this.m_bHaveNewFriendRequest && currentTempet != null && nkmuserData != null)
			{
				MentoringIdentity mentoringIdentity = NKCMentoringUtil.GetMentoringIdentity(nkmuserData);
				if (mentoringIdentity != MentoringIdentity.Mentor && mentoringIdentity == MentoringIdentity.Mentee && (NKCMentoringUtil.IsCanReceiveMenteeMissionReward(nkmuserData) || NKCMentoringUtil.IsDontHaveMentor(nkmuserData)))
				{
					nkmuserData.SetMentoringNotify(true);
					this.SetMentoringRewardAlarm();
					return;
				}
			}
		}

		// Token: 0x06004851 RID: 18513 RVA: 0x0015D810 File Offset: 0x0015BA10
		public void OnRecv(NKMPacket_LEAGUE_PVP_WEEKLY_RANKER_ACK sPacket)
		{
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_LEAGUE_MODE) && sPacket.userProfileData.Count > 0)
			{
				List<LeaderBoardSlotData> lstSlotData = LeaderBoardSlotData.MakeSlotDataList(LeaderBoardType.BT_PVP_LEAGUE_TOP, sPacket.userProfileData);
				if (this.m_PopupTopPlayer == null)
				{
					this.m_PopupTopPlayer = NKCPopupTopPlayer.OpenInstance("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_POPUP_LEAGUE_TOP3");
				}
				this.m_PopupTopPlayer.Open("", NKCStringTable.GetString("SI_PF_LEAGUEMATCH_CHAMPION", false), NKCStringTable.GetString("SI_PF_LEAGUEMATCH", false), null, lstSlotData, sPacket.userProfileData[0].emblems, 0L, null);
			}
			this.m_bWaitGauntletLeagueTopAck = false;
		}

		// Token: 0x06004852 RID: 18514 RVA: 0x0015D8A7 File Offset: 0x0015BAA7
		public void OnRecvAutoSupplyMsg()
		{
		}

		// Token: 0x06004853 RID: 18515 RVA: 0x0015D8A9 File Offset: 0x0015BAA9
		public void RefreshBuff()
		{
			if (this.m_UILobby != null)
			{
				this.m_UILobby.RefreshUserBuff();
			}
		}

		// Token: 0x06004854 RID: 18516 RVA: 0x0015D8C4 File Offset: 0x0015BAC4
		public void TryPause()
		{
			this.m_pauseTime = Time.time;
		}

		// Token: 0x06004855 RID: 18517 RVA: 0x0015D8D1 File Offset: 0x0015BAD1
		public void OnReturnApp()
		{
			if (Time.time < this.m_pauseTime + 60f)
			{
				return;
			}
			this.m_pauseTime = 0f;
			this.PlayVoice(VOICE_TYPE.VT_LOBBY_RETURN);
		}

		// Token: 0x06004856 RID: 18518 RVA: 0x0015D8F9 File Offset: 0x0015BAF9
		public void PlayConnectVoice()
		{
			this.PlayVoice(VOICE_TYPE.VT_LOBBY_CONNECT);
		}

		// Token: 0x06004857 RID: 18519 RVA: 0x0015D904 File Offset: 0x0015BB04
		private void PlayVoice(VOICE_TYPE type)
		{
			NKMBackgroundUnitInfo backgroundUnitInfo = NKCScenManager.GetScenManager().GetMyUserData().GetBackgroundUnitInfo(0);
			if (backgroundUnitInfo == null)
			{
				return;
			}
			long unitUid = backgroundUnitInfo.unitUid;
			NKM_UNIT_TYPE unitType = backgroundUnitInfo.unitType;
			if (unitType != NKM_UNIT_TYPE.NUT_NORMAL)
			{
				if (unitType != NKM_UNIT_TYPE.NUT_OPERATOR)
				{
					return;
				}
				NKMOperator operatorFromUId = NKCScenManager.CurrentUserData().m_ArmyData.GetOperatorFromUId(unitUid);
				if (operatorFromUId != null)
				{
					NKCUIVoiceManager.PlayVoice(type, operatorFromUId, true, true);
				}
			}
			else
			{
				NKMUnitData unitFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(unitUid);
				if (unitFromUID != null)
				{
					NKCUIVoiceManager.PlayVoice(type, unitFromUID, false, true);
					return;
				}
			}
		}

		// Token: 0x06004858 RID: 18520 RVA: 0x0015D97D File Offset: 0x0015BB7D
		public void RefreshNickname()
		{
			NKCUILobbyV2 uilobby = this.m_UILobby;
			if (uilobby == null)
			{
				return;
			}
			uilobby.RefreshNickname();
		}

		// Token: 0x06004859 RID: 18521 RVA: 0x0015D98F File Offset: 0x0015BB8F
		public void DoAfterLogout()
		{
			this.m_bFirstLobby = true;
		}

		// Token: 0x0600485A RID: 18522 RVA: 0x0015D998 File Offset: 0x0015BB98
		public void UnhideLobbyUI()
		{
			if (this.m_UILobby != null)
			{
				this.m_UILobby.TryUIUnhide();
			}
		}

		// Token: 0x0600485B RID: 18523 RVA: 0x0015D9B3 File Offset: 0x0015BBB3
		public void RefreshRechargeEternium()
		{
			NKCUILobbyV2 uilobby = this.m_UILobby;
			if (uilobby == null)
			{
				return;
			}
			uilobby.RefreshRechargeEternium();
		}

		// Token: 0x0600485C RID: 18524 RVA: 0x0015D9C8 File Offset: 0x0015BBC8
		public void OnHomeEnter()
		{
			if (this.ProcessCoroutine != null)
			{
				NKCScenManager.GetScenManager().StopCoroutine(this.ProcessCoroutine);
				this.m_bRunningLobbyProcess = false;
			}
			this.m_NKCLocalLoginData = NKCLocalLoginData.LoadLastLoginData();
			this.ProcessCoroutine = NKCScenManager.GetScenManager().StartCoroutine(this.Process());
		}

		// Token: 0x0600485D RID: 18525 RVA: 0x0015DA15 File Offset: 0x0015BC15
		public bool IsRunningLobbyProcess()
		{
			return this.m_bRunningLobbyProcess;
		}

		// Token: 0x0600485E RID: 18526 RVA: 0x0015DA1D File Offset: 0x0015BC1D
		private IEnumerator Process()
		{
			if (this.m_bRunningLobbyProcess)
			{
				yield break;
			}
			Debug.Log("Home Process : Begin");
			this.m_bRunningLobbyProcess = true;
			while (!NKCUIManager.IsTopmostUI(this.m_UILobby))
			{
				yield return null;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			Debug.Log("Home Process : Check login cutscene");
			if (NKCLoginCutSceneManager.CheckLoginCutScene(null))
			{
				Debug.Log("Home Process : Play login cutscene");
				this.m_bRunningLobbyProcess = false;
				this.ProcessCoroutine = null;
				yield break;
			}
			Debug.Log("Home Process : Check Tutorial");
			this.TutorialCheck();
			if (NKCGameEventManager.IsEventPlaying() || NKCTutorialManager.IsCloseDailyContents())
			{
				Debug.Log("Home Process : Waiting Tutorial. Process break.");
				this.m_bRunningLobbyProcess = false;
				this.ProcessCoroutine = null;
				yield break;
			}
			this.m_UILobby.SetEventPanelAutoScroll(false);
			if (NKCScenManager.GetScenManager().WarfareGameData != null && NKCScenManager.GetScenManager().WarfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_STOP)
			{
				NKMStageTempletV2 stageTemplet = NKMEpisodeMgr.FindStageTempletByBattleStrID("NKM_WARFARE_EP1_4_1");
				if (nkmuserData != null && !NKMTutorialManager.IsTutorialCompleted(TutorialStep.SecondDeckSetup, nkmuserData) && NKMEpisodeMgr.CheckClear(NKCScenManager.CurrentUserData(), stageTemplet))
				{
					this.m_bWait = true;
					Debug.Log("Home Process : 140 tutorial force fix");
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString("SI_SECOND_DECK_TUTORIAL_ERROR_NOTICE", false), delegate()
					{
						NKCPacketSender.Send_NKMPacket_MISSION_COMPLETE_REQ(NKMMissionManager.GetMissionTabTemplet(NKM_MISSION_TYPE.TUTORIAL).m_tabID, 140, 140);
						this.WaitFinished();
					}, "");
					while (this.m_bWait)
					{
						yield return null;
					}
				}
			}
			if (this.m_bFirstLobby)
			{
				NKMSkinTemplet loginSkinCutin = this.GetLoginSkinCutin();
				if (loginSkinCutin != null && !string.IsNullOrEmpty(loginSkinCutin.m_LoginCutin))
				{
					if (this.m_NKCLocalLoginData != null)
					{
						this.m_NKCLocalLoginData.m_hsPlayedCutin.Add(loginSkinCutin.Key);
					}
					this.m_bWait = true;
					Debug.Log("Home Process : skin Cutin " + loginSkinCutin.m_LoginCutin + " found. play");
					NKCUIEventSequence.PlaySkinCutin(loginSkinCutin, new NKCUIEventSequence.OnClose(this.WaitFinished));
					while (this.m_bWait)
					{
						yield return null;
					}
				}
			}
			if (NKCPopupFirstRunOptionSetup.IsOptionSetupRequired())
			{
				Debug.Log("Home Process : First play option setup");
				this.m_bWait = true;
				NKCPopupFirstRunOptionSetup.Instance.Open(new NKCPopupFirstRunOptionSetup.OnClose(this.WaitFinished));
				while (this.m_bWait)
				{
					yield return null;
				}
			}
			if (this.m_bFirstLobby)
			{
				Debug.Log("Home Process : firstlobby actions");
				NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
				if (gameOptionData != null)
				{
					Debug.Log("Home Process : push alarm setup");
					NKCPublisherModule.Push.SetAlarm(NKC_GAME_OPTION_ALARM_GROUP.ALLOW_ALL_ALARM, gameOptionData.GetAllowAlarm(NKC_GAME_OPTION_ALARM_GROUP.ALLOW_ALL_ALARM));
				}
				while (NKCPublisherModule.Busy)
				{
					yield return null;
				}
				Debug.Log("Home Process : purchase restore");
				NKCPublisherModule.InAppPurchase.BillingRestore(new NKCPublisherModule.OnComplete(NKCShopManager.OnBillingRestore));
				yield return null;
				while (NKCPublisherModule.Busy)
				{
					yield return null;
				}
				Debug.Log("Home Process : check attandence");
				this.CheckAttandence();
				while (NKCUIAttendance.IsInstanceOpen)
				{
					yield return null;
				}
				if (this.m_NKCLocalLoginData != null)
				{
					this.m_NKCLocalLoginData.SaveLastLoginData();
				}
				this.m_bFirstLobby = false;
				Debug.Log("Home Process : first lobby flag off");
				this.m_bWait = true;
				Debug.Log("Home Process : OpenPromotionalBanner");
				NKCPublisherModule.Notice.OpenPromotionalBanner(NKCPublisherModule.NKCPMNotice.eOptionalBannerPlaces.EnterLobby, new NKCPublisherModule.OnComplete(this.WaitFinished));
				while (this.m_bWait)
				{
					yield return null;
				}
				Debug.Log("Home Process : check OpenNotice");
				if (NKCPublisherModule.Notice.CheckOpenNoticeWhenFirstLobbyVisit())
				{
					Debug.Log("Home Process : OpenNotice");
					this.m_bWait = true;
					NKCPublisherModule.Statistics.LogClientAction(NKCPublisherModule.NKCPMStatistics.eClientAction.Lobby_ShowNotice, 0, null);
					NKCPublisherModule.Notice.OpenNotice(new NKCPublisherModule.OnComplete(this.WaitFinished));
					while (this.m_bWait)
					{
						yield return null;
					}
				}
				Debug.Log("Home Process : check event");
				bool flag;
				if (NKCContentManager.CheckContentStatus(ContentsType.LOBBY_EVENT, out flag, 0, 0) == NKCContentManager.eContentStatus.Open)
				{
					NKMEventTabTemplet requiredEventTemplet = NKCUIEvent.GetRequiredEventTemplet();
					if (requiredEventTemplet != null)
					{
						Debug.Log("Home Process : Event open");
						NKCUIEvent.Instance.Open(requiredEventTemplet);
					}
					while (NKCUIEvent.IsInstanceOpen)
					{
						yield return null;
					}
				}
				Debug.Log("Home Process : fierce notice");
				this.CheckFierceNotice();
				while (NKCUIPopupFierceBattleNotice.IsInstanceOpen)
				{
					yield return null;
				}
				if (NKCContentManager.CheckContentStatus(ContentsType.PVP, out flag, 0, 0) == NKCContentManager.eContentStatus.Open && NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_LEAGUE_MODE))
				{
					Debug.Log("Home Process : Gauntlet Top3 Check");
					this.CheckGauntletLeagueTopPlayers();
					while (this.m_bWaitGauntletLeagueTopAck)
					{
						yield return null;
					}
					while (this.m_PopupTopPlayer != null && this.m_PopupTopPlayer.gameObject.activeSelf)
					{
						yield return null;
					}
				}
				this.PlayConnectVoice();
			}
			else
			{
				Debug.Log("Home Process : unlockedcontent popup");
				NKCContentManager.SetUnlockedContent(STAGE_UNLOCK_REQ_TYPE.SURT_ALWAYS_UNLOCKED, -1);
				NKCContentManager.ShowContentUnlockPopup(null, Array.Empty<STAGE_UNLOCK_REQ_TYPE>());
			}
			this.m_UILobby.SetEventPanelAutoScroll(true);
			Debug.Log("Home Process : finished");
			this.ProcessCoroutine = null;
			this.m_bRunningLobbyProcess = false;
			yield break;
		}

		// Token: 0x0600485F RID: 18527 RVA: 0x0015DA2C File Offset: 0x0015BC2C
		public void ResetFirstLobby()
		{
			this.m_bFirstLobby = false;
		}

		// Token: 0x06004860 RID: 18528 RVA: 0x0015DA35 File Offset: 0x0015BC35
		private void WaitFinished()
		{
			this.m_bWait = false;
		}

		// Token: 0x06004861 RID: 18529 RVA: 0x0015DA3E File Offset: 0x0015BC3E
		private void WaitFinished(NKC_PUBLISHER_RESULT_CODE code, string additionalError)
		{
			if (code == NKC_PUBLISHER_RESULT_CODE.NPRC_INAPP_NOT_EXIST_RESTORE_ITEM)
			{
				this.m_bWait = false;
				NKMPopUpBox.CloseWaitBox();
				return;
			}
			if (NKCPublisherModule.CheckError(code, additionalError, true, delegate
			{
				this.m_bWait = false;
			}, true))
			{
				this.m_bWait = false;
			}
		}

		// Token: 0x06004862 RID: 18530 RVA: 0x0015DA74 File Offset: 0x0015BC74
		public NKMSkinTemplet GetLoginSkinCutin()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData.LoginCutin == NKCGameOptionDataSt.GraphicOptionLoginCutin.Off)
			{
				return null;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return null;
			}
			NKMBackgroundInfo backGroundInfo = nkmuserData.backGroundInfo;
			if (backGroundInfo == null)
			{
				return null;
			}
			List<NKMSkinTemplet> list = new List<NKMSkinTemplet>();
			foreach (NKMBackgroundUnitInfo nkmbackgroundUnitInfo in backGroundInfo.unitInfoList)
			{
				if (nkmbackgroundUnitInfo.unitUid != 0L)
				{
					NKMUnitData unitOrTrophyFromUID = nkmuserData.m_ArmyData.GetUnitOrTrophyFromUID(nkmbackgroundUnitInfo.unitUid);
					if (unitOrTrophyFromUID != null && unitOrTrophyFromUID.m_SkinID != 0)
					{
						NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(unitOrTrophyFromUID.m_SkinID);
						if (skinTemplet != null && !string.IsNullOrEmpty(skinTemplet.m_LoginCutin))
						{
							list.Add(skinTemplet);
						}
					}
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			switch (gameOptionData.LoginCutin)
			{
			case NKCGameOptionDataSt.GraphicOptionLoginCutin.Always:
				return list[0];
			case NKCGameOptionDataSt.GraphicOptionLoginCutin.Random:
			{
				int index = UnityEngine.Random.Range(0, list.Count);
				return list[index];
			}
			case NKCGameOptionDataSt.GraphicOptionLoginCutin.OncePerDay:
				foreach (NKMSkinTemplet nkmskinTemplet in list)
				{
					if (this.m_NKCLocalLoginData == null || !this.m_NKCLocalLoginData.m_hsPlayedCutin.Contains(nkmskinTemplet.Key))
					{
						return nkmskinTemplet;
					}
				}
				break;
			}
			return null;
		}

		// Token: 0x06004863 RID: 18531 RVA: 0x0015DBF0 File Offset: 0x0015BDF0
		public void TutorialCheck()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.Lobby, true);
		}

		// Token: 0x04003822 RID: 14370
		private NKMTrackingFloat m_BloomIntensity = new NKMTrackingFloat();

		// Token: 0x04003823 RID: 14371
		private float m_fElapsedTimeToRefreshDailyContents;

		// Token: 0x04003824 RID: 14372
		private NKCUILobbyV2 m_UILobby;

		// Token: 0x04003825 RID: 14373
		private NKCUIManager.LoadedUIData m_UILobbyData;

		// Token: 0x04003826 RID: 14374
		private NKCPopupTopPlayer m_PopupTopPlayer;

		// Token: 0x04003827 RID: 14375
		private bool m_bHaveNewFriendRequest;

		// Token: 0x04003828 RID: 14376
		private bool m_bAttendanceRequired;

		// Token: 0x04003829 RID: 14377
		private bool m_bFirstLobby = true;

		// Token: 0x0400382A RID: 14378
		private float m_pauseTime;

		// Token: 0x0400382B RID: 14379
		private int m_ReservedRankingBoardID;

		// Token: 0x0400382C RID: 14380
		private int m_eReservedGuideMissionTabID;

		// Token: 0x0400382D RID: 14381
		private Coroutine ProcessCoroutine;

		// Token: 0x0400382E RID: 14382
		private NKC_SCEN_HOME.RESERVE_OPEN_TYPE m_eReservedOpendUIType;

		// Token: 0x0400382F RID: 14383
		private int m_iReservedOpenUIID;

		// Token: 0x04003830 RID: 14384
		private static bool m_bNeedNewsPopup = true;

		// Token: 0x04003831 RID: 14385
		private bool m_bNeedRefreshMail;

		// Token: 0x04003832 RID: 14386
		private bool m_bReserverAttendance;

		// Token: 0x04003833 RID: 14387
		private List<NKMAttendance> m_lstAttendance = new List<NKMAttendance>();

		// Token: 0x04003834 RID: 14388
		private long m_lAttendanceUpdateTime;

		// Token: 0x04003835 RID: 14389
		private bool m_bWaitGauntletLeagueTopAck;

		// Token: 0x04003836 RID: 14390
		private bool m_bWait;

		// Token: 0x04003837 RID: 14391
		private bool m_bRunningLobbyProcess;

		// Token: 0x04003838 RID: 14392
		private NKCLocalLoginData m_NKCLocalLoginData;

		// Token: 0x020013F3 RID: 5107
		public enum RESERVE_OPEN_TYPE
		{
			// Token: 0x04009CAD RID: 40109
			ROT_NONE,
			// Token: 0x04009CAE RID: 40110
			ROT_MISSION,
			// Token: 0x04009CAF RID: 40111
			ROT_RANKING_BOARD,
			// Token: 0x04009CB0 RID: 40112
			ROT_GUIDE_MISSION,
			// Token: 0x04009CB1 RID: 40113
			ROT_EVENT_COLLECTION,
			// Token: 0x04009CB2 RID: 40114
			ROT_EVENT_BANNER
		}
	}
}
