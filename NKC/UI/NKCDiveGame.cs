using System;
using System.Collections;
using ClientPacket.Mode;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKC.PacketHandler;
using NKC.UI.NPC;
using NKC.UI.Option;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;

namespace NKC.UI
{
	// Token: 0x0200096A RID: 2410
	public class NKCDiveGame : NKCUIBase
	{
		// Token: 0x17001137 RID: 4407
		// (get) Token: 0x060060C1 RID: 24769 RVA: 0x001E42C9 File Offset: 0x001E24C9
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_DIVE;
			}
		}

		// Token: 0x17001138 RID: 4408
		// (get) Token: 0x060060C2 RID: 24770 RVA: 0x001E42D0 File Offset: 0x001E24D0
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001139 RID: 4409
		// (get) Token: 0x060060C3 RID: 24771 RVA: 0x001E42D3 File Offset: 0x001E24D3
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x1700113A RID: 4410
		// (get) Token: 0x060060C4 RID: 24772 RVA: 0x001E42D6 File Offset: 0x001E24D6
		// (set) Token: 0x060060C5 RID: 24773 RVA: 0x001E42F3 File Offset: 0x001E24F3
		public float DIVE_UNIT_MOVE_TIME
		{
			get
			{
				if (this.CheckAuto())
				{
					return this.DIVE_UNIT_MOVE_TIME_ / NKCClientConst.DiveAutoSpeed;
				}
				return this.DIVE_UNIT_MOVE_TIME_;
			}
			set
			{
				this.DIVE_UNIT_MOVE_TIME_ = value;
			}
		}

		// Token: 0x1700113B RID: 4411
		// (get) Token: 0x060060C6 RID: 24774 RVA: 0x001E42FC File Offset: 0x001E24FC
		// (set) Token: 0x060060C7 RID: 24775 RVA: 0x001E4319 File Offset: 0x001E2519
		public float MOVE_REQ_COROUTINE_WAIT_TIME
		{
			get
			{
				if (this.CheckAuto())
				{
					return this.MOVE_REQ_COROUTINE_WAIT_TIME_ / NKCClientConst.DiveAutoSpeed;
				}
				return this.MOVE_REQ_COROUTINE_WAIT_TIME_;
			}
			set
			{
				this.MOVE_REQ_COROUTINE_WAIT_TIME_ = value;
			}
		}

		// Token: 0x1700113C RID: 4412
		// (get) Token: 0x060060C8 RID: 24776 RVA: 0x001E4322 File Offset: 0x001E2522
		// (set) Token: 0x060060C9 RID: 24777 RVA: 0x001E4329 File Offset: 0x001E2529
		public static int WarfareRecoverItemCost { get; private set; }

		// Token: 0x060060CA RID: 24778 RVA: 0x001E4331 File Offset: 0x001E2531
		public void SetLastSelectedArtifactSlotIndex(int index)
		{
			this.m_LastSelectedArtifactSlotIndex = index;
		}

		// Token: 0x1700113D RID: 4413
		// (get) Token: 0x060060CB RID: 24779 RVA: 0x001E433C File Offset: 0x001E253C
		private NKCPopupDiveEvent NKCPopupDiveEvent
		{
			get
			{
				if (this.m_NKCPopupDiveEvent == null)
				{
					NKCUIManager.LoadedUIData loadedUIData = NKCUIManager.OpenNewInstance<NKCPopupDiveEvent>("AB_UI_NKM_UI_WORLD_MAP_DIVE", "NKM_UI_DIVE_EVENT_POPUP", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), delegate()
					{
						this.m_NKCPopupDiveEvent = null;
					});
					this.m_NKCPopupDiveEvent = loadedUIData.GetInstance<NKCPopupDiveEvent>();
					this.m_NKCPopupDiveEvent.InitUI();
				}
				return this.m_NKCPopupDiveEvent;
			}
		}

		// Token: 0x1700113E RID: 4414
		// (get) Token: 0x060060CC RID: 24780 RVA: 0x001E4398 File Offset: 0x001E2598
		public NKCPopupDiveArtifactGet NKCPopupDiveArtifactGet
		{
			get
			{
				if (this.m_NKCPopupDiveArtifactGet == null)
				{
					NKCUIManager.LoadedUIData loadedUIData = NKCUIManager.OpenNewInstance<NKCPopupDiveArtifactGet>("AB_UI_NKM_UI_WORLD_MAP_DIVE", "NKM_UI_DIVE_ARTIFACT_POPUP", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), delegate()
					{
						this.m_NKCPopupDiveArtifactGet = null;
					});
					this.m_NKCPopupDiveArtifactGet = loadedUIData.GetInstance<NKCPopupDiveArtifactGet>();
					this.m_NKCPopupDiveArtifactGet.InitUI(new NKCPopupDiveArtifactGet.dOnEffectExplode(this.OnArtifactEffectExplode), new NKCPopupDiveArtifactGet.dOnEffectDestSetting(this.OnFinishScrollToArtifactDummySlot));
				}
				return this.m_NKCPopupDiveArtifactGet;
			}
		}

		// Token: 0x1700113F RID: 4415
		// (get) Token: 0x060060CD RID: 24781 RVA: 0x001E440A File Offset: 0x001E260A
		public bool IsOpenNKCPopupDiveArtifactGet
		{
			get
			{
				return this.m_NKCPopupDiveArtifactGet != null && this.m_NKCPopupDiveArtifactGet.IsOpen;
			}
		}

		// Token: 0x060060CE RID: 24782 RVA: 0x001E4427 File Offset: 0x001E2627
		public void OnArtifactEffectExplode()
		{
			this.m_NKCDiveGameHUD.m_NKCDiveGameHUDArtifact.InvalidDummySlot();
			this.m_NKCDiveGameHUD.m_NKCDiveGameHUDArtifact.m_LoopScrollRect.RefreshCells(false);
			this.m_NKCDiveGameHUD.m_NKCDiveGameHUDArtifact.UpdateTotalViewTextUI();
		}

		// Token: 0x060060CF RID: 24783 RVA: 0x001E445F File Offset: 0x001E265F
		public static void SetReservedUnitDieShow(bool bSet, int prevDiedDeckIndex = -1, NKC_DIVE_GAME_UNIT_DIE_TYPE dieType = NKC_DIVE_GAME_UNIT_DIE_TYPE.NDGUDT_EXPLOSION)
		{
			NKCDiveGame.m_NKC_DIVE_GAME_UNIT_DIE_TYPE = dieType;
			NKCDiveGame.m_bReservedUnitDieShow = bSet;
			NKCDiveGame.m_PrevDeckIndexDied = prevDiedDeckIndex;
		}

		// Token: 0x060060D0 RID: 24784 RVA: 0x001E4473 File Offset: 0x001E2673
		public static void SetDieType(NKC_DIVE_GAME_UNIT_DIE_TYPE _type)
		{
			NKCDiveGame.m_NKC_DIVE_GAME_UNIT_DIE_TYPE = _type;
		}

		// Token: 0x060060D1 RID: 24785 RVA: 0x001E447C File Offset: 0x001E267C
		public static NKCDiveGame Init()
		{
			NKCDiveGame nkcdiveGame = NKCUIManager.OpenUI<NKCDiveGame>("NKM_UI_DIVE_PROCESS_3D");
			nkcdiveGame.gameObject.SetActive(false);
			nkcdiveGame.m_NKCDiveGameHUD = NKCDiveGameHUD.InitUI(nkcdiveGame);
			nkcdiveGame.m_StartSector.SetUI(new NKMDiveSlot(NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_START, NKM_DIVE_EVENT_TYPE.NDET_NONE, 0, 0, 0));
			nkcdiveGame.m_NKCDiveGameSectorSetMgr = new NKCDiveGameSectorSetMgr();
			nkcdiveGame.m_NKCDiveGameSectorSetMgr.Init(nkcdiveGame.m_NKM_UI_DIVE_PROCESS_SECTOR_GRID.transform);
			nkcdiveGame.m_SectorLinesFromSelected.Init();
			nkcdiveGame.m_SectorLinesFromMyPos.Init();
			nkcdiveGame.m_SectorLinesFromSelectedMyPos.Init();
			NKCNPCTemplet npctemplet = NKCUINPCBase.GetNPCTemplet(NPC_TYPE.OPERATOR_CHLOE, NPC_ACTION_TYPE.IDLE);
			if (npctemplet != null && npctemplet.m_ConditionType == NPC_CONDITION.IDLE_TIME && npctemplet.m_ConditionValue > 0)
			{
				nkcdiveGame.m_IdleVoiceInterval = (float)npctemplet.m_ConditionValue;
			}
			nkcdiveGame.m_bWaitingBossBeforeCutscenInAuto = false;
			return nkcdiveGame;
		}

		// Token: 0x060060D2 RID: 24786 RVA: 0x001E4538 File Offset: 0x001E2738
		public void OnChangedAuto(bool bSet)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null && myUserData.m_UserOption != null)
			{
				NKMPacket_DIVE_AUTO_REQ nkmpacket_DIVE_AUTO_REQ = new NKMPacket_DIVE_AUTO_REQ();
				nkmpacket_DIVE_AUTO_REQ.isAuto = bSet;
				NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_DIVE_AUTO_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
			}
		}

		// Token: 0x060060D3 RID: 24787 RVA: 0x001E457C File Offset: 0x001E277C
		private bool CheckAuto()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				NKMUserOption userOption = myUserData.m_UserOption;
				if (userOption != null)
				{
					return userOption.m_bAutoDive;
				}
			}
			return false;
		}

		// Token: 0x060060D4 RID: 24788 RVA: 0x001E45A9 File Offset: 0x001E27A9
		private void ClearMoveReqCoroutine()
		{
			if (this.m_coMoveReq != null)
			{
				base.StopCoroutine(this.m_coMoveReq);
				this.m_coMoveReq = null;
			}
		}

		// Token: 0x060060D5 RID: 24789 RVA: 0x001E45C6 File Offset: 0x001E27C6
		private IEnumerator MoveReqCoroutine()
		{
			yield return new WaitForSeconds(this.MOVE_REQ_COROUTINE_WAIT_TIME);
			while (this.m_bPause)
			{
				yield return null;
			}
			this.OnClickSectorInfoSearch();
			this.m_coMoveReq = null;
			yield break;
		}

		// Token: 0x060060D6 RID: 24790 RVA: 0x001E45D8 File Offset: 0x001E27D8
		private void DoNextThingByAutoWhenExploring()
		{
			NKMDiveGameData diveGameData = this.GetDiveGameData();
			if (diveGameData == null)
			{
				return;
			}
			NKCDiveGameSector nextDiveGameSectorByAuto = this.m_NKCDiveGameSectorSetMgr.GetNextDiveGameSectorByAuto(diveGameData.Player.PlayerBase.Distance == 0);
			if (nextDiveGameSectorByAuto != null)
			{
				this.OnClickSector(nextDiveGameSectorByAuto, true);
				this.m_coMoveReq = base.StartCoroutine(this.MoveReqCoroutine());
			}
		}

		// Token: 0x060060D7 RID: 24791 RVA: 0x001E4634 File Offset: 0x001E2834
		private bool CheckGiveUpRecommendPopupOpenTiming()
		{
			NKMDiveGameData diveGameData = this.GetDiveGameData();
			return diveGameData != null && diveGameData.Player.PlayerBase.State == NKMDivePlayerState.Exploring && (!diveGameData.Player.CheckExistPossibleSquadForBattle() && !this.m_NKCDiveGameSectorSetMgr.CheckExistEuclidInNextSectors());
		}

		// Token: 0x060060D8 RID: 24792 RVA: 0x001E4680 File Offset: 0x001E2880
		private void DoNextThingByAuto()
		{
			NKMDiveGameData diveGameData = this.GetDiveGameData();
			if (diveGameData == null)
			{
				return;
			}
			if (this.CheckUnitMoving())
			{
				return;
			}
			if (diveGameData.Player.PlayerBase.State != NKMDivePlayerState.Exploring)
			{
				if (diveGameData.Player.PlayerBase.State == NKMDivePlayerState.BattleReady)
				{
					if (!this.m_bWaitingBossBeforeCutscenInAuto && this.m_Selected_NKCDiveGameSector != null && this.m_Selected_NKCDiveGameSector.GetNKMDiveSlot().EventType == NKM_DIVE_EVENT_TYPE.NDET_DUNGEON_BOSS && !string.IsNullOrEmpty(this.GetDiveTemplet().CutsceneDiveBossBefore) && !NKCScenManager.CurrentUserData().CheckDiveHistory(this.GetDiveTemplet().StageID))
					{
						this.m_bWaitingBossBeforeCutscenInAuto = true;
						NKCUICutScenPlayer.Instance.LoadAndPlay(this.GetDiveTemplet().CutsceneDiveBossBefore, 0, new NKCUICutScenPlayer.CutScenCallBack(this.DoNextThingByAuto), true);
						return;
					}
					if (diveGameData.Player.PlayerBase.ReservedDeckIndex >= 0)
					{
						NKCPacketSender.Send_NKMPacket_GAME_LOAD_REQ((byte)diveGameData.Player.PlayerBase.ReservedDeckIndex, 0, diveGameData.Floor.Templet.StageID, NKMDungeonManager.GetDungeonStrID(diveGameData.Player.PlayerBase.ReservedDungeonID), 0, false, 1, 0);
						return;
					}
					NKMDiveSquad squadForBattleByAuto = diveGameData.Player.GetSquadForBattleByAuto();
					if (squadForBattleByAuto != null && squadForBattleByAuto.CurHp >= 0f)
					{
						if (squadForBattleByAuto.Supply <= 0)
						{
							this.Send_NKMPacket_DIVE_SUICIDE_REQ((byte)squadForBattleByAuto.DeckIndex);
							return;
						}
						NKCPacketSender.Send_NKMPacket_GAME_LOAD_REQ((byte)squadForBattleByAuto.DeckIndex, 0, diveGameData.Floor.Templet.StageID, NKMDungeonManager.GetDungeonStrID(diveGameData.Player.PlayerBase.ReservedDungeonID), 0, false, 1, 0);
						return;
					}
				}
				else if (diveGameData.Player.PlayerBase.State == NKMDivePlayerState.SelectArtifact)
				{
					this.NKCPopupDiveArtifactGet.Open(diveGameData.Player.PlayerBase.ReservedArtifacts, this.CheckAuto(), new NKCPopupDiveArtifactGet.dOnCloseCallBack(this.OnCloseArtifactGetPopup));
				}
				return;
			}
			if (this.CheckGiveUpRecommendPopupOpenTiming())
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_DIVE_GIVE_UP_RECOMMEND, new NKCPopupOKCancel.OnButton(this.OnClickOkGiveUpINGDive), new NKCPopupOKCancel.OnButton(this.DoNextThingByAutoWhenExploring), false);
				return;
			}
			this.DoNextThingByAutoWhenExploring();
		}

		// Token: 0x060060D9 RID: 24793 RVA: 0x001E4882 File Offset: 0x001E2A82
		private void OnCloseArtifactGetPopup()
		{
			if (this.CheckAuto())
			{
				this.DoNextThingByAuto();
				return;
			}
			if (this.CheckGiveUpRecommendPopupOpenTiming())
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_DIVE_GIVE_UP_RECOMMEND, new NKCPopupOKCancel.OnButton(this.OnClickOkGiveUpINGDive), null, false);
			}
		}

		// Token: 0x060060DA RID: 24794 RVA: 0x001E48B8 File Offset: 0x001E2AB8
		public void OnClickPause()
		{
			if (this.m_bPause)
			{
				return;
			}
			if (NKMPopUpBox.IsOpenedWaitBox())
			{
				return;
			}
			if (this.m_NKCDiveGameUnit != null)
			{
				this.m_NKCDiveGameUnit.SetPause(true);
			}
			NKCCamera.GetTrackingPos().SetPause(true);
			NKCUIGameOption.Instance.Open(NKC_GAME_OPTION_MENU_TYPE.DIVE, new NKCUIGameOption.OnCloseCallBack(this.OnCloseGameOption));
			this.m_bPause = true;
		}

		// Token: 0x060060DB RID: 24795 RVA: 0x001E4919 File Offset: 0x001E2B19
		public void GiveUp()
		{
			if (this.GetDiveGameData() == null)
			{
				return;
			}
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_DIVE_GIVE_UP, new NKCPopupOKCancel.OnButton(this.OnClickOkGiveUpINGDive), null, false);
		}

		// Token: 0x060060DC RID: 24796 RVA: 0x001E4944 File Offset: 0x001E2B44
		private void OnClickOkGiveUpINGDive()
		{
			NKCUIGameOption.CheckInstanceAndClose();
			NKMPacket_DIVE_GIVE_UP_REQ packet = new NKMPacket_DIVE_GIVE_UP_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060060DD RID: 24797 RVA: 0x001E496F File Offset: 0x001E2B6F
		public void TempLeave()
		{
			NKCUIGameOption.CheckInstanceAndClose();
			NKCUtil.SetDiveTargetEventID();
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_DIVE_READY, true);
		}

		// Token: 0x060060DE RID: 24798 RVA: 0x001E4988 File Offset: 0x001E2B88
		private void OnCloseGameOption()
		{
			if (this.m_NKCDiveGameUnit != null)
			{
				this.m_NKCDiveGameUnit.SetPause(false);
			}
			NKCCamera.GetTrackingPos().SetPause(false);
			this.m_bPause = false;
		}

		// Token: 0x060060DF RID: 24799 RVA: 0x001E49B6 File Offset: 0x001E2BB6
		public static void SetIntro(bool bSet)
		{
			NKCDiveGame.m_bIntro = bSet;
		}

		// Token: 0x060060E0 RID: 24800 RVA: 0x001E49BE File Offset: 0x001E2BBE
		public static void SetSectorAddEvent(bool bSet)
		{
			NKCDiveGame.m_bSectorAddEvent = bSet;
			NKCDiveGame.m_fElapsedTimeForSectorAddEvent = 0f;
		}

		// Token: 0x060060E1 RID: 24801 RVA: 0x001E49D0 File Offset: 0x001E2BD0
		public static void SetSectorAddEventWhenStart(bool bSet)
		{
			NKCDiveGame.m_bSectorAddEventWhenStart = bSet;
			NKCDiveGame.m_fElapsedTimeForSectorAddEvent = 0f;
		}

		// Token: 0x060060E2 RID: 24802 RVA: 0x001E49E4 File Offset: 0x001E2BE4
		private NKMDiveTemplet GetDiveTemplet()
		{
			NKMDiveGameData diveGameData = NKCScenManager.GetScenManager().GetMyUserData().m_DiveGameData;
			if (diveGameData == null)
			{
				return null;
			}
			if (diveGameData.Floor == null)
			{
				return null;
			}
			return diveGameData.Floor.Templet;
		}

		// Token: 0x060060E3 RID: 24803 RVA: 0x001E4A1B File Offset: 0x001E2C1B
		private NKMDiveGameData GetDiveGameData()
		{
			return NKCScenManager.GetScenManager().GetMyUserData().m_DiveGameData;
		}

		// Token: 0x060060E4 RID: 24804 RVA: 0x001E4A2C File Offset: 0x001E2C2C
		private float GetDefaultZDist()
		{
			return -800f;
		}

		// Token: 0x060060E5 RID: 24805 RVA: 0x001E4A33 File Offset: 0x001E2C33
		private bool CheckUnitMoving()
		{
			return this.m_NKCDiveGameUnit != null && this.m_NKCDiveGameUnit.IsMoving();
		}

		// Token: 0x060060E6 RID: 24806 RVA: 0x001E4A53 File Offset: 0x001E2C53
		private void CloseSelectedSectorLines()
		{
			this.m_SectorLinesFromSelected.Close();
			this.m_SectorLinesFromSelectedMyPos.Close();
		}

		// Token: 0x060060E7 RID: 24807 RVA: 0x001E4A6B File Offset: 0x001E2C6B
		private void CloseAllSectorLines()
		{
			this.CloseSelectedSectorLines();
			this.m_SectorLinesFromMyPos.Close();
		}

		// Token: 0x060060E8 RID: 24808 RVA: 0x001E4A80 File Offset: 0x001E2C80
		private void OnClickSector(NKCDiveGameSector cNKCDiveGameSector, bool bByAuto)
		{
			if (cNKCDiveGameSector == null)
			{
				return;
			}
			if (this.GetDiveGameData() == null)
			{
				return;
			}
			if (this.CheckUnitMoving())
			{
				return;
			}
			if (!bByAuto && this.CheckAuto())
			{
				return;
			}
			bool flag = !cNKCDiveGameSector.GetGrey();
			if (flag)
			{
				flag = false;
				NKMDiveSlot nkmdiveSlot = cNKCDiveGameSector.GetNKMDiveSlot();
				if (nkmdiveSlot != null && this.GetDiveGameData().Player.PlayerBase.Distance + 1 == cNKCDiveGameSector.GetDistance())
				{
					if (this.IsSameCol())
					{
						if (this.IsSameRow(cNKCDiveGameSector.GetSlotIndex()))
						{
							flag = this.m_NKCDiveGameHUD.OpenSectorInfo(nkmdiveSlot, this.IsSameCol());
						}
					}
					else
					{
						flag = this.m_NKCDiveGameHUD.OpenSectorInfo(nkmdiveSlot, this.IsSameCol());
					}
				}
			}
			if (this.m_Selected_NKCDiveGameSector != null)
			{
				this.m_Selected_NKCDiveGameSector.SetSelected(false);
			}
			if (!flag)
			{
				this.m_NKCDiveGameHUD.CloseSectorInfo();
				this.InvalidSelectedSector();
				NKCCamera.GetTrackingPos().SetTracking(new NKMVector3(cNKCDiveGameSector.GetFinalPos().x, NKCCamera.GetTrackingPos().GetNowValueY(), this.GetDefaultZDist()), 1.3f, TRACKING_DATA_TYPE.TDT_SLOWER);
				return;
			}
			this.m_Selected_NKCDiveGameSector = cNKCDiveGameSector;
			this.m_Selected_NKCDiveGameSector.SetSelected(true);
			if (this.m_Selected_NKCDiveGameSector.GetNKMDiveSlot().EventType == NKM_DIVE_EVENT_TYPE.NDET_DUNGEON_BOSS)
			{
				this.m_SectorLinesFromSelected.Close();
				this.OpenSectorLinesFromSelectedMyPos();
			}
			else
			{
				int realSetSize = this.m_Selected_NKCDiveGameSector.GetRealSetSize();
				int uislotIndex = this.m_Selected_NKCDiveGameSector.GetUISlotIndex();
				this.m_SectorLinesFromSelected.OpenFromSelected(realSetSize, uislotIndex, this.m_Selected_NKCDiveGameSector.GetSlotIndex(), NKCDiveManager.IsDiveJump() || this.m_Selected_NKCDiveGameSector.GetDistance() == this.GetDiveGameData().Floor.Templet.RandomSetCount);
				this.m_SectorLinesFromSelected.transform.localPosition = this.m_Selected_NKCDiveGameSector.GetFinalPos();
				this.OpenSectorLinesFromSelectedMyPos();
			}
			NKCCamera.GetTrackingPos().SetTracking(new NKMVector3(cNKCDiveGameSector.GetFinalPos().x, cNKCDiveGameSector.GetFinalPos().y, this.GetDefaultZDist() + 200f), 1.3f, TRACKING_DATA_TYPE.TDT_SLOWER);
		}

		// Token: 0x060060E9 RID: 24809 RVA: 0x001E4C78 File Offset: 0x001E2E78
		private void OpenSectorLinesFromMyPos()
		{
			NKCDiveGameSector sectorUnderMyShip = this.GetSectorUnderMyShip();
			if (sectorUnderMyShip != null)
			{
				int realSetSize = sectorUnderMyShip.GetRealSetSize();
				int uislotIndex = sectorUnderMyShip.GetUISlotIndex();
				int toRealSetSize = NKCDiveManager.IsDiveJump() ? 1 : this.GetDiveTemplet().SlotCount;
				this.m_SectorLinesFromMyPos.OpenFromMyPos(realSetSize, toRealSetSize, uislotIndex, sectorUnderMyShip.GetSlotIndex(), sectorUnderMyShip == this.m_StartSector, NKCDiveManager.IsDiveJump() || sectorUnderMyShip.GetDistance() == this.GetDiveGameData().Floor.Templet.RandomSetCount);
				this.m_SectorLinesFromMyPos.transform.localPosition = sectorUnderMyShip.GetFinalPos();
			}
		}

		// Token: 0x060060EA RID: 24810 RVA: 0x001E4D1C File Offset: 0x001E2F1C
		private void OpenSectorLinesFromSelectedMyPos()
		{
			NKCDiveGameSector sectorUnderMyShip = this.GetSectorUnderMyShip();
			if (sectorUnderMyShip != null && this.m_Selected_NKCDiveGameSector != null)
			{
				int realSetSize = sectorUnderMyShip.GetRealSetSize();
				int realSetSize2 = this.m_Selected_NKCDiveGameSector.GetRealSetSize();
				int uislotIndex = sectorUnderMyShip.GetUISlotIndex();
				int uislotIndex2 = this.m_Selected_NKCDiveGameSector.GetUISlotIndex();
				this.m_SectorLinesFromSelectedMyPos.OpenFromSelectedMyPos(realSetSize, realSetSize2, uislotIndex, uislotIndex2, sectorUnderMyShip.GetSlotIndex(), this.m_Selected_NKCDiveGameSector.GetSlotIndex(), sectorUnderMyShip == this.m_StartSector, sectorUnderMyShip.GetDistance() == this.GetDiveGameData().Floor.Templet.RandomSetCount);
				this.m_SectorLinesFromSelectedMyPos.transform.localPosition = this.GetPlayerPosByData(false);
			}
		}

		// Token: 0x060060EB RID: 24811 RVA: 0x001E4DD5 File Offset: 0x001E2FD5
		private void InvalidSelectedSector()
		{
			if (this.m_Selected_NKCDiveGameSector != null)
			{
				this.m_Selected_NKCDiveGameSector.SetSelected(false);
			}
			this.m_Selected_NKCDiveGameSector = null;
			this.CloseSelectedSectorLines();
		}

		// Token: 0x060060EC RID: 24812 RVA: 0x001E4E00 File Offset: 0x001E3000
		private void PlayPrevLeaderUnitDieAni()
		{
			NKCUIManager.SetScreenInputBlock(true);
			this.m_cgCIRCLESET.DOFade(0f, 2.6f);
			if (NKCDiveGame.m_NKC_DIVE_GAME_UNIT_DIE_TYPE == NKC_DIVE_GAME_UNIT_DIE_TYPE.NDGUDT_EXPLOSION)
			{
				this.m_NUM_WARFARE_FX_UNIT_EXPLOSION.transform.localPosition = new Vector3(this.m_NKCDiveGameUnit.transform.localPosition.x, this.m_NKCDiveGameUnit.transform.localPosition.y - 40f, this.m_NKCDiveGameUnit.transform.localPosition.z);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_UNIT_EXPLOSION, false);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_UNIT_EXPLOSION, true);
				this.m_NKCDiveGameUnit.PlayDieAniExplosion(new NKCDiveGameUnit.OnAniComplete(this.SpawnNewLeaderUnit));
				return;
			}
			if (NKCDiveGame.m_NKC_DIVE_GAME_UNIT_DIE_TYPE == NKC_DIVE_GAME_UNIT_DIE_TYPE.NDGUDT_WARP)
			{
				this.m_NUM_WARFARE_FX_UNIT_ESCAPE.transform.localPosition = new Vector3(this.m_NKCDiveGameUnit.transform.localPosition.x, this.m_NKCDiveGameUnit.transform.localPosition.y - 40f, this.m_NKCDiveGameUnit.transform.localPosition.z);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_UNIT_ESCAPE, false);
				NKCUtil.SetGameobjectActive(this.m_NUM_WARFARE_FX_UNIT_ESCAPE, true);
				this.m_NKCDiveGameUnit.PlayDieAniWarp(new NKCDiveGameUnit.OnAniComplete(this.SpawnNewLeaderUnit));
			}
		}

		// Token: 0x060060ED RID: 24813 RVA: 0x001E4F54 File Offset: 0x001E3154
		private void SpawnNewLeaderUnit()
		{
			this.UpdateDiveGameUnitUI(false);
			this.m_NKCDiveGameUnit.transform.localPosition = this.GetPlayerPosByData(false);
			NKCCamera.GetTrackingPos().SetTracking(new NKMVector3(this.m_NKCDiveGameUnit.transform.localPosition.x, this.m_NKCDiveGameUnit.transform.localPosition.y - 40f, this.GetDefaultZDist()), 1f, TRACKING_DATA_TYPE.TDT_SLOWER);
			this.m_NKCDiveGameUnit.PlaySpawnAni(new NKCDiveGameUnit.OnAniComplete(this.OnCompleteSpawnAni));
		}

		// Token: 0x060060EE RID: 24814 RVA: 0x001E4FE4 File Offset: 0x001E31E4
		private void OnCompleteSpawnAni()
		{
			this.OpenSectorLinesFromMyPos();
			NKCUIManager.SetScreenInputBlock(false);
			if (this.CheckAuto())
			{
				this.DoNextThingByAuto();
				return;
			}
			if (this.CheckGiveUpRecommendPopupOpenTiming())
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_DIVE_GIVE_UP_RECOMMEND, new NKCPopupOKCancel.OnButton(this.OnClickOkGiveUpINGDive), null, false);
			}
		}

		// Token: 0x060060EF RID: 24815 RVA: 0x001E5031 File Offset: 0x001E3231
		private IEnumerator DoAfterSectorEventAniEnd()
		{
			while (this.m_NKCDiveGameSectorSetMgr.IsAnimating())
			{
				yield return null;
			}
			this.DoAfterSectorEvent();
			yield break;
		}

		// Token: 0x060060F0 RID: 24816 RVA: 0x001E5040 File Offset: 0x001E3240
		private void Update()
		{
			if (base.IsOpen)
			{
				if (this.m_NKCDiveGameSectorSetMgr != null)
				{
					this.m_NKCDiveGameSectorSetMgr.Update();
				}
				if (this.m_NKCDiveGameUnit != null)
				{
					if (this.m_CIRCLESET != null)
					{
						this.m_CIRCLESET.localPosition = this.m_NKCDiveGameUnit.transform.localPosition;
					}
					if (this.m_tr_NKM_UI_DIVE_PROCESS_3D_SEARCH_ANI != null)
					{
						this.m_tr_NKM_UI_DIVE_PROCESS_3D_SEARCH_ANI.localPosition = this.m_NKCDiveGameUnit.transform.localPosition;
					}
					NKCDiveGame.m_lastGameUnitPos = this.m_NKCDiveGameUnit.transform.localPosition;
				}
				if (this.m_coIntro == null && (NKCDiveGame.m_bSectorAddEvent || NKCDiveGame.m_bSectorAddEventWhenStart))
				{
					float num = Time.deltaTime;
					if (num > NKCScenManager.GetScenManager().GetFixedFrameTime() * 2f)
					{
						num = NKCScenManager.GetScenManager().GetFixedFrameTime() * 2f;
					}
					NKCDiveGame.m_fElapsedTimeForSectorAddEvent += num;
					if (NKCDiveGame.m_fElapsedTimeForSectorAddEvent >= this.m_fTimeForSectorAddEvent)
					{
						NKCDiveGame.m_bSectorAddEvent = false;
						NKCDiveGame.m_bSectorAddEventWhenStart = false;
						this.m_bRealSetSectorSets = true;
						this.m_fElapsedTimeForRealSetSecterSets = 0f;
						if (this.m_NKCDiveGameUnit != null)
						{
							this.m_NKCDiveGameUnit.PlaySearch();
						}
						if (this.m_NKM_UI_DIVE_PROCESS_3D_SEARCH_ANI != null)
						{
							this.m_NKM_UI_DIVE_PROCESS_3D_SEARCH_ANI.Play("NKM_UI_DIVE_PROCESS_3D_SEARCH_ANI");
						}
						this.m_CIRCLESET.transform.DOScale(new Vector3(0f, 0f, 1f), 1.3f).SetEase(Ease.OutQuad).From<TweenerCore<Vector3, Vector3, VectorOptions>>();
					}
				}
				if (this.m_bRealSetSectorSets)
				{
					this.m_fElapsedTimeForRealSetSecterSets += Time.deltaTime;
					if (this.m_fElapsedTimeForRealSetSecterSets >= this.m_fTimeForRealSetSecterSets)
					{
						this.m_bRealSetSectorSets = false;
						this.m_NKCDiveGameSectorSetMgr.SetUI(this.GetDiveGameData(), true);
						this.m_coDoAfterSectorEventAniEnd = base.StartCoroutine(this.DoAfterSectorEventAniEnd());
						if (this.m_Selected_NKCDiveGameSector != null && !this.m_Selected_NKCDiveGameSector.CheckSelectable())
						{
							this.InvalidSelectedSector();
							this.m_NKCDiveGameHUD.CloseSectorInfo();
						}
					}
				}
				if (Input.anyKeyDown)
				{
					this.t_IdleDeltaTime = 0f;
				}
				if (!NKCSoundManager.IsPlayingVoice(-1))
				{
					this.t_IdleDeltaTime += Time.deltaTime;
					if (this.t_IdleDeltaTime > this.m_IdleVoiceInterval)
					{
						this.t_IdleDeltaTime = 0f;
						NKCUINPCOperatorChloe.PlayVoice(NPC_TYPE.OPERATOR_CHLOE, NPC_ACTION_TYPE.IDLE, true);
					}
				}
				bool flag = false;
				if (Input.GetKey(KeyCode.W))
				{
					flag = this.KeyScroll(KeyCode.W);
				}
				if (Input.GetKey(KeyCode.S))
				{
					flag = this.KeyScroll(KeyCode.S);
				}
				if (Input.GetKey(KeyCode.A))
				{
					flag = this.KeyScroll(KeyCode.A);
				}
				if (Input.GetKey(KeyCode.D))
				{
					flag = this.KeyScroll(KeyCode.D);
				}
				if (!flag)
				{
					this.KeyScroll(KeyCode.None);
				}
			}
		}

		// Token: 0x060060F1 RID: 24817 RVA: 0x001E52F0 File Offset: 0x001E34F0
		private void UpdateDiveGameUnitUI(bool bUsePrevDiedDeck = false)
		{
			if (this.m_NKCDiveGameUnit != null)
			{
				int leaderUnitID = this.GetLeaderUnitID(bUsePrevDiedDeck);
				if (leaderUnitID > 0)
				{
					this.m_NKCDiveGameUnit.SetUI(leaderUnitID);
				}
				this.m_NKCDiveGameUnit.ResetRotation();
				this.m_cgCIRCLESET.alpha = 1f;
			}
		}

		// Token: 0x060060F2 RID: 24818 RVA: 0x001E5340 File Offset: 0x001E3540
		private int GetLeaderUnitID(bool bUsePrevDiedDeck = false)
		{
			NKMDiveGameData diveGameData = this.GetDiveGameData();
			if (diveGameData == null)
			{
				return 0;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return 0;
			}
			NKMDeckIndex deckIndex;
			if (bUsePrevDiedDeck)
			{
				deckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_DIVE, NKCDiveGame.m_PrevDeckIndexDied);
			}
			else
			{
				deckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_DIVE, diveGameData.Player.PlayerBase.LeaderDeckIndex);
			}
			NKMDeckData deckData = myUserData.m_ArmyData.GetDeckData(deckIndex);
			if (deckData != null)
			{
				NKMUnitData shipFromUID = myUserData.m_ArmyData.GetShipFromUID(deckData.m_ShipUID);
				if (shipFromUID != null)
				{
					return shipFromUID.m_UnitID;
				}
			}
			return 0;
		}

		// Token: 0x060060F3 RID: 24819 RVA: 0x001E53C4 File Offset: 0x001E35C4
		private void SetUIAfterMovie()
		{
			this.m_coIntro = null;
			NKMDiveGameData diveGameData = this.GetDiveGameData();
			NKMDiveTemplet diveTemplet = this.GetDiveTemplet();
			if (diveGameData == null || diveTemplet == null)
			{
				return;
			}
			if (!string.IsNullOrWhiteSpace(diveTemplet.MusicBundleName) && !string.IsNullOrWhiteSpace(diveTemplet.MusicFileName))
			{
				NKCSoundManager.PlayMusic(diveTemplet.MusicFileName, true, 1f, false, 0f, 0f);
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_PROCESS_3D_CONTENT, true);
			this.m_NKCDiveGameHUD.Open();
			this.m_NKCDiveGameHUD.PlayIntro();
			this.m_NKCDiveGameHUD.SetSelectedSquadSlot(-1);
			this.InvalidSelectedSector();
			this.UpdateDiveGameUnitUI(NKCDiveGame.m_bReservedUnitDieShow);
			if (NKCDiveGame.m_bReservedUnitDieShow)
			{
				this.m_NKCDiveGameUnit.transform.localPosition = NKCDiveGame.m_lastGameUnitPos;
				this.PlayPrevLeaderUnitDieAni();
			}
			else
			{
				this.m_NKCDiveGameUnit.transform.localPosition = this.GetPlayerPosByData(false);
				this.OpenSectorLinesFromMyPos();
			}
			NKCCamera.SetPos(this.m_NKCDiveGameUnit.transform.localPosition.x, this.m_NKCDiveGameUnit.transform.localPosition.y, this.GetDefaultZDist() + 150f, true, true);
			NKCCamera.GetTrackingPos().SetTracking(new NKMVector3(this.m_NKCDiveGameUnit.transform.localPosition.x, this.m_NKCDiveGameUnit.transform.localPosition.y, this.GetDefaultZDist()), 1f, TRACKING_DATA_TYPE.TDT_SLOWER);
			NKCCamera.GetTrackingRotation().SetNowValue(0f, 0f, 0f);
			if (!NKCDiveGame.m_bSectorAddEvent && !NKCDiveGame.m_bSectorAddEventWhenStart)
			{
				if (this.CheckAuto())
				{
					if (!NKCDiveGame.m_bReservedUnitDieShow)
					{
						this.DoNextThingByAuto();
					}
				}
				else if (diveGameData.Player.PlayerBase.State == NKMDivePlayerState.BattleReady)
				{
					NKMDiveSlot slot = diveGameData.Floor.GetSlot(diveGameData.Player.PlayerBase.SlotSetIndex, diveGameData.Player.PlayerBase.SlotIndex);
					if (slot != null)
					{
						this.m_NKCDiveGameHUD.OpenSectorInfo(slot, true);
						this.m_NKCDiveGameHUD.OpenSquadList();
						this.m_NKCDiveGameHUD.OpenSquadView(diveGameData.Player.PlayerBase.LeaderDeckIndex);
						this.m_Selected_NKCDiveGameSector = this.m_NKCDiveGameSectorSetMgr.GetSector(slot);
						if (this.m_Selected_NKCDiveGameSector != null)
						{
							this.m_Selected_NKCDiveGameSector.SetSelected(true);
						}
						if (diveGameData.Player.PlayerBase.ReservedDeckIndex >= 0)
						{
							this.m_NKCDiveGameHUD.OpenSquadView(diveGameData.Player.PlayerBase.ReservedDeckIndex);
							this.OnClickBattle();
						}
					}
				}
				else if (diveGameData.Player.PlayerBase.State == NKMDivePlayerState.SelectArtifact)
				{
					this.NKCPopupDiveArtifactGet.Open(diveGameData.Player.PlayerBase.ReservedArtifacts, this.CheckAuto(), new NKCPopupDiveArtifactGet.dOnCloseCallBack(this.OnCloseArtifactGetPopup));
				}
				else if (diveGameData.Player.PlayerBase.State == NKMDivePlayerState.Exploring && this.CheckGiveUpRecommendPopupOpenTiming() && !NKCDiveGame.m_bReservedUnitDieShow)
				{
					NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_DIVE_GIVE_UP_RECOMMEND, new NKCPopupOKCancel.OnButton(this.OnClickOkGiveUpINGDive), null, false);
				}
			}
			NKCDiveGame.m_bReservedUnitDieShow = false;
			NKCCamera.GetCamera().orthographic = false;
			this.m_rtCamBound = new Rect
			{
				xMin = this.m_StartSector.GetFinalPos().x,
				xMax = this.GetBossPos().x,
				yMin = -200f,
				yMax = 200f
			};
			this.CheckTutorial();
		}

		// Token: 0x060060F4 RID: 24820 RVA: 0x001E5748 File Offset: 0x001E3948
		private void AddBG(string prefabName)
		{
			NKCAssetResourceData nkcassetResourceData = NKCAssetResourceManager.OpenResource<GameObject>("AB_UI_NKM_UI_WORLD_MAP_DIVE", prefabName, false, null);
			if (nkcassetResourceData != null)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(nkcassetResourceData.GetAsset<GameObject>());
				gameObject.transform.SetParent(this.m_NKM_UI_DIVE_PROCESS_3D_CONTENT.transform);
				gameObject.transform.SetAsFirstSibling();
				this.m_NKM_UI_DIVE_PROCESS_3D_SEARCH_ANI = gameObject.transform.Find("NKM_UI_DIVE_PROCESS_3D_SEARCH_ANI").gameObject.GetComponent<Animator>();
				this.m_tr_NKM_UI_DIVE_PROCESS_3D_SEARCH_ANI = gameObject.transform.Find("NKM_UI_DIVE_PROCESS_3D_SEARCH_ANI").gameObject.transform;
				GameObject gameObject2 = gameObject.transform.Find("CIRCLESET").gameObject;
				this.m_CIRCLESET = gameObject2.transform;
				this.m_cgCIRCLESET = gameObject2.gameObject.AddComponent<CanvasGroup>();
				NKCAssetResourceManager.CloseResource(nkcassetResourceData);
			}
		}

		// Token: 0x060060F5 RID: 24821 RVA: 0x001E5810 File Offset: 0x001E3A10
		public void Open()
		{
			NKMDiveGameData diveGameData = this.GetDiveGameData();
			NKMDiveTemplet diveTemplet = this.GetDiveTemplet();
			if (diveGameData == null || diveTemplet == null)
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				return;
			}
			this.m_LastSelectedArtifactSlotIndex = -1;
			base.UIOpened(true);
			this.CloseAllSectorLines();
			if (diveTemplet.StageType == NKM_DIVE_STAGE_TYPE.NDST_HARD)
			{
				this.AddBG("NKM_UI_DIVE_PROCESS_3D_BG_HURDLE");
			}
			else
			{
				this.AddBG("NKM_UI_DIVE_PROCESS_3D_BG");
			}
			int depth = NKCDiveManager.IsDiveJump() ? 1 : diveGameData.Floor.Templet.RandomSetCount;
			this.m_NKCDiveGameSectorSetMgr.Reset(depth, diveTemplet.SlotCount, new NKCDiveGameSector.OnClickSector(this.OnClickSector));
			if (NKCDiveGame.m_bSectorAddEvent)
			{
				this.m_NKCDiveGameSectorSetMgr.SetUIWhenAddSectorBeforeScan(diveGameData);
			}
			else if (NKCDiveGame.m_bSectorAddEventWhenStart)
			{
				this.m_NKCDiveGameSectorSetMgr.SetUIWhenStartBeforeScan(diveGameData);
			}
			else
			{
				this.m_NKCDiveGameSectorSetMgr.SetUI(diveGameData, false);
			}
			if (this.m_NKCDiveGameUnit == null)
			{
				this.m_NKCDiveGameUnit = NKCDiveGameUnit.GetNewInstance(this.m_NKM_UI_DIVE_PROCESS_UNIT_LAYER.transform);
				NKCUtil.SetGameobjectActive(this.m_NKCDiveGameUnit, true);
			}
			Canvas.ForceUpdateCanvases();
			NKCDiveGame.m_fElapsedTimeForSectorAddEvent = 0f;
			this.m_fElapsedTimeForRealSetSecterSets = 0f;
			this.m_bRealSetSectorSets = false;
			if (NKCDiveGame.m_bIntro && NKCDiveGame.m_bSectorAddEventWhenStart && !string.IsNullOrEmpty(diveTemplet.CutsceneDiveEnter) && !NKCScenManager.CurrentUserData().CheckDiveHistory(diveTemplet.StageID))
			{
				NKCUICutScenPlayer.Instance.LoadAndPlay(diveTemplet.CutsceneDiveEnter, 0, new NKCUICutScenPlayer.CutScenCallBack(this.AfterEnterCutscene), true);
				return;
			}
			this.AfterEnterCutscene();
		}

		// Token: 0x060060F6 RID: 24822 RVA: 0x001E5988 File Offset: 0x001E3B88
		private void AfterEnterCutscene()
		{
			if (NKCDiveGame.m_bIntro)
			{
				NKCUINPCOperatorChloe.PlayVoice(NPC_TYPE.OPERATOR_CHLOE, NPC_ACTION_TYPE.DIVE_START, true);
				NKCDiveGame.m_bIntro = false;
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_PROCESS_3D_CONTENT, false);
				this.m_NKCDiveGameHUD.Close();
				if (this.m_coIntro != null)
				{
					base.StopCoroutine(this.m_coIntro);
				}
				this.m_coIntro = base.StartCoroutine(this.DiveGameUIOpenProcess());
				return;
			}
			this.SetUIAfterMovie();
		}

		// Token: 0x060060F7 RID: 24823 RVA: 0x001E59EF File Offset: 0x001E3BEF
		private void VideoPlayMessageCallback(NKCUIComVideoPlayer.eVideoMessage message)
		{
			switch (message)
			{
			case NKCUIComVideoPlayer.eVideoMessage.PlayFailed:
			case NKCUIComVideoPlayer.eVideoMessage.PlayComplete:
				this.m_bWaitingMovie = false;
				break;
			case NKCUIComVideoPlayer.eVideoMessage.PlayBegin:
				break;
			default:
				return;
			}
		}

		// Token: 0x060060F8 RID: 24824 RVA: 0x001E5A0B File Offset: 0x001E3C0B
		private IEnumerator DiveGameUIOpenProcess()
		{
			NKCUIComVideoCamera videoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
			if (videoPlayer != null)
			{
				videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
				videoPlayer.m_fMoviePlaySpeed = this.MoviePlaySpeed;
				this.m_bWaitingMovie = true;
				videoPlayer.Play("Worldmap_Dive_Intro.mp4", false, false, new NKCUIComVideoPlayer.VideoPlayMessageCallback(this.VideoPlayMessageCallback), false);
				this.m_introSoundUID = NKCSoundManager.PlaySound("FX_UI_DIVE_START_MOVIE_FRONT", 1f, 0f, 0f, false, 0f, false, 0f);
				while (this.m_bWaitingMovie)
				{
					yield return null;
					if (Input.anyKeyDown && PlayerPrefs.GetInt("DIVE_GAME_INTRO_SKIP", 0) == 1)
					{
						break;
					}
				}
				videoPlayer.Stop();
				NKCSoundManager.StopSound(this.m_introSoundUID);
				this.m_introSoundUID = 0;
				NKCSoundManager.PlaySound("FX_UI_DIVE_START_MOVIE_BACK", 1f, 0f, 0f, false, 0f, false, 0f);
				if (PlayerPrefs.GetInt("DIVE_GAME_INTRO_SKIP", 0) == 0)
				{
					PlayerPrefs.SetInt("DIVE_GAME_INTRO_SKIP", 1);
				}
			}
			this.m_bWaitingMovie = false;
			NKMDiveTemplet diveTemplet = this.GetDiveTemplet();
			if (NKCDiveGame.m_bSectorAddEventWhenStart && !string.IsNullOrEmpty(diveTemplet.CutsceneDiveStart) && !NKCScenManager.CurrentUserData().CheckDiveHistory(diveTemplet.StageID))
			{
				NKCUICutScenPlayer.Instance.LoadAndPlay(diveTemplet.CutsceneDiveStart, 0, new NKCUICutScenPlayer.CutScenCallBack(this.SetUIAfterMovie), true);
			}
			else
			{
				this.SetUIAfterMovie();
			}
			yield break;
		}

		// Token: 0x060060F9 RID: 24825 RVA: 0x001E5A1C File Offset: 0x001E3C1C
		public NKCDiveGameSector GetSectorUnderMyShip()
		{
			NKMDiveGameData diveGameData = this.GetDiveGameData();
			if (diveGameData == null)
			{
				return null;
			}
			if (diveGameData.Player.PlayerBase.Distance == 0 && diveGameData.Player.PlayerBase.SlotSetIndex == -1)
			{
				return this.m_StartSector;
			}
			int slotSetIndex = diveGameData.Player.PlayerBase.SlotSetIndex;
			return this.m_NKCDiveGameSectorSetMgr.GetActiveDiveGameSector(slotSetIndex, diveGameData.Player.PlayerBase.SlotIndex);
		}

		// Token: 0x060060FA RID: 24826 RVA: 0x001E5A90 File Offset: 0x001E3C90
		public Vector3 GetBossPos()
		{
			Vector3 result = new Vector3(0f, 0f, 0f);
			NKCDiveGameSector bossSector = this.m_NKCDiveGameSectorSetMgr.GetBossSector();
			if (bossSector != null)
			{
				return bossSector.GetFinalPos();
			}
			return result;
		}

		// Token: 0x060060FB RID: 24827 RVA: 0x001E5AD0 File Offset: 0x001E3CD0
		public Vector3 GetPlayerPosByData(bool bMoveACK = false)
		{
			Vector3 result = new Vector3(0f, 0f, 0f);
			NKMDiveGameData diveGameData = this.GetDiveGameData();
			if (diveGameData == null)
			{
				return result;
			}
			if (diveGameData.Player.PlayerBase.Distance == 0 && diveGameData.Player.PlayerBase.SlotSetIndex == -1)
			{
				return this.m_StartSector.transform.localPosition;
			}
			int num = diveGameData.Player.PlayerBase.SlotSetIndex;
			if (bMoveACK && diveGameData.Player.PlayerBase.Distance != 1 && diveGameData.Player.PlayerBase.Distance != 0 && (diveGameData.Player.PlayerBase.Distance != diveGameData.Floor.Templet.RandomSetCount || diveGameData.Player.PlayerBase.SlotSetIndex != 1))
			{
				num++;
				if (num > 1)
				{
					num = 1;
				}
			}
			NKCDiveGameSector activeDiveGameSector = this.m_NKCDiveGameSectorSetMgr.GetActiveDiveGameSector(num, diveGameData.Player.PlayerBase.SlotIndex);
			if (activeDiveGameSector != null)
			{
				return activeDiveGameSector.GetFinalPos();
			}
			return result;
		}

		// Token: 0x060060FC RID: 24828 RVA: 0x001E5BE0 File Offset: 0x001E3DE0
		private bool KeyScroll(KeyCode keyCode)
		{
			if (this.CheckUnitMoving())
			{
				return false;
			}
			if (this.m_NKCDiveGameHUD.IsOpenSquadView())
			{
				return false;
			}
			if (this.CheckAuto())
			{
				return false;
			}
			float num = NKCCamera.GetPosNowX(false);
			float num2 = NKCCamera.GetPosNowY(false);
			bool flag = true;
			if (keyCode <= KeyCode.D)
			{
				if (keyCode == KeyCode.A)
				{
					this.m_ScrollReduceTime = 1f;
					num -= Time.deltaTime * this.m_KeyScrollSensitivity;
					this.m_prevKeyCode = KeyCode.A;
					goto IL_10A;
				}
				if (keyCode == KeyCode.D)
				{
					this.m_ScrollReduceTime = 1f;
					num += Time.deltaTime * this.m_KeyScrollSensitivity;
					this.m_prevKeyCode = KeyCode.D;
					goto IL_10A;
				}
			}
			else
			{
				if (keyCode == KeyCode.S)
				{
					this.m_ScrollReduceTime = 1f;
					num2 -= Time.deltaTime * this.m_KeyScrollSensitivity * 0.5f;
					this.m_prevKeyCode = KeyCode.S;
					goto IL_10A;
				}
				if (keyCode == KeyCode.W)
				{
					this.m_ScrollReduceTime = 1f;
					num2 += Time.deltaTime * this.m_KeyScrollSensitivity * 0.5f;
					this.m_prevKeyCode = KeyCode.W;
					goto IL_10A;
				}
			}
			this.m_ScrollReduceTime -= Time.deltaTime;
			flag = false;
			IL_10A:
			if (this.m_ScrollReduceTime <= 0f)
			{
				return false;
			}
			if (!flag)
			{
				float num3 = NKMTrackingFloat.TrackRatio(TRACKING_DATA_TYPE.TDT_FASTER, this.m_ScrollReduceTime, 1f, 3f);
				KeyCode prevKeyCode = this.m_prevKeyCode;
				if (prevKeyCode <= KeyCode.D)
				{
					if (prevKeyCode != KeyCode.A)
					{
						if (prevKeyCode == KeyCode.D)
						{
							num += Time.deltaTime * num3 * this.m_KeyScrollSensitivity;
						}
					}
					else
					{
						num -= Time.deltaTime * num3 * this.m_KeyScrollSensitivity;
					}
				}
				else if (prevKeyCode != KeyCode.S)
				{
					if (prevKeyCode == KeyCode.W)
					{
						num2 += Time.deltaTime * num3 * this.m_KeyScrollSensitivity * 0.5f;
					}
				}
				else
				{
					num2 -= Time.deltaTime * num3 * this.m_KeyScrollSensitivity * 0.5f;
				}
			}
			else
			{
				this.m_NKCDiveGameHUD.CloseSectorInfo();
				this.InvalidSelectedSector();
			}
			num = Mathf.Clamp(num, this.m_rtCamBound.xMin, this.m_rtCamBound.xMax);
			num2 = Mathf.Clamp(num2, this.m_rtCamBound.yMin, this.m_rtCamBound.yMax);
			NKCCamera.SetPos(num, num2, this.GetDefaultZDist(), true, false);
			return flag;
		}

		// Token: 0x060060FD RID: 24829 RVA: 0x001E5DFE File Offset: 0x001E3FFE
		public override void Hide()
		{
			base.Hide();
			this.m_NKCDiveGameHUD.Close();
		}

		// Token: 0x060060FE RID: 24830 RVA: 0x001E5E11 File Offset: 0x001E4011
		public override void UnHide()
		{
			base.UnHide();
			this.m_NKCDiveGameHUD.Open();
		}

		// Token: 0x060060FF RID: 24831 RVA: 0x001E5E24 File Offset: 0x001E4024
		public void OnClickBG(BaseEventData cBaseEventData)
		{
			if (this.CheckUnitMoving())
			{
				return;
			}
			if (this.CheckAuto())
			{
				return;
			}
			this.m_NKCDiveGameHUD.CloseSectorInfo();
			NKCCamera.TrackingPos(1.3f, -1f, -1f, this.GetDefaultZDist());
			this.InvalidSelectedSector();
		}

		// Token: 0x06006100 RID: 24832 RVA: 0x001E5E64 File Offset: 0x001E4064
		public void OnDragByInstance(BaseEventData cBaseEventData)
		{
			if (this.CheckUnitMoving())
			{
				return;
			}
			if (this.m_NKCDiveGameHUD.IsOpenSquadView())
			{
				return;
			}
			PointerEventData pointerEventData = cBaseEventData as PointerEventData;
			float num = NKCCamera.GetPosNowX(false) - pointerEventData.delta.x * 10f;
			float num2 = NKCCamera.GetPosNowY(false) - pointerEventData.delta.y * 10f;
			num = Mathf.Clamp(num, this.m_rtCamBound.xMin, this.m_rtCamBound.xMax);
			num2 = Mathf.Clamp(num2, this.m_rtCamBound.yMin, this.m_rtCamBound.yMax);
			NKCCamera.TrackingPos(1f, num, num2, this.GetDefaultZDist());
			if (this.CheckAuto())
			{
				return;
			}
			this.m_NKCDiveGameHUD.CloseSectorInfo();
			this.InvalidSelectedSector();
		}

		// Token: 0x06006101 RID: 24833 RVA: 0x001E5F28 File Offset: 0x001E4128
		public void OnClickSectorInfoSearch()
		{
			if (this.m_Selected_NKCDiveGameSector == null)
			{
				return;
			}
			if (this.CheckUnitMoving())
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKM_ERROR_CODE nkm_ERROR_CODE = NKMDiveGameManager.CanMoveForward(this.m_Selected_NKCDiveGameSector.GetSlotIndex(), myUserData);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCPacketHandlers.GetErrorMessage(nkm_ERROR_CODE), null, "");
				return;
			}
			NKMPacket_DIVE_MOVE_FORWARD_REQ nkmpacket_DIVE_MOVE_FORWARD_REQ = new NKMPacket_DIVE_MOVE_FORWARD_REQ();
			nkmpacket_DIVE_MOVE_FORWARD_REQ.slotIndex = this.m_Selected_NKCDiveGameSector.GetSlotIndex();
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_DIVE_MOVE_FORWARD_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
			NKCUINPCOperatorChloe.PlayVoice(NPC_TYPE.OPERATOR_CHLOE, NKCUINPCOperatorChloe.GetNPCActionType(this.m_Selected_NKCDiveGameSector.GetNKMDiveSlot()), true);
			this.ClearMoveReqCoroutine();
		}

		// Token: 0x06006102 RID: 24834 RVA: 0x001E5FCC File Offset: 0x001E41CC
		public void OnClickBattle()
		{
			if (this.m_Selected_NKCDiveGameSector == null)
			{
				return;
			}
			NKMDiveGameData diveGameData = this.GetDiveGameData();
			if (diveGameData == null)
			{
				return;
			}
			if (diveGameData.Player.PlayerBase.State != NKMDivePlayerState.BattleReady)
			{
				return;
			}
			NKMDiveSquad squad = diveGameData.Player.GetSquad((int)this.m_NKCDiveGameHUD.GetLastSelectedDeckIndex().m_iIndex);
			if (squad == null)
			{
				return;
			}
			if (squad.Supply <= 0)
			{
				if (!this.CheckAuto())
				{
					string get_STRING_WARNING = NKCUtilString.GET_STRING_WARNING;
					string get_STRING_DIVE_WARNING_SUPPLY = NKCUtilString.GET_STRING_DIVE_WARNING_SUPPLY;
					NKCPopupOKCancel.OpenOKCancelBox(get_STRING_WARNING, get_STRING_DIVE_WARNING_SUPPLY, delegate()
					{
						this.Send_NKMPacket_DIVE_SUICIDE_REQ(this.m_NKCDiveGameHUD.GetLastSelectedDeckIndex().m_iIndex);
					}, null, false);
					return;
				}
				this.Send_NKMPacket_DIVE_SUICIDE_REQ(this.m_NKCDiveGameHUD.GetLastSelectedDeckIndex().m_iIndex);
				return;
			}
			else
			{
				if (this.m_Selected_NKCDiveGameSector.GetNKMDiveSlot().EventType == NKM_DIVE_EVENT_TYPE.NDET_DUNGEON_BOSS && !string.IsNullOrEmpty(this.GetDiveTemplet().CutsceneDiveBossBefore) && !NKCScenManager.CurrentUserData().CheckDiveHistory(this.GetDiveTemplet().StageID))
				{
					NKCUICutScenPlayer.Instance.LoadAndPlay(this.GetDiveTemplet().CutsceneDiveBossBefore, 0, new NKCUICutScenPlayer.CutScenCallBack(this.SendGameLoadREQ), true);
					return;
				}
				this.SendGameLoadREQ();
				return;
			}
		}

		// Token: 0x06006103 RID: 24835 RVA: 0x001E60D5 File Offset: 0x001E42D5
		private void Send_NKMPacket_DIVE_SUICIDE_REQ(byte deckIndex)
		{
			if (this.GetDiveGameData() == null)
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_DIVE_SUICIDE_REQ(deckIndex);
		}

		// Token: 0x06006104 RID: 24836 RVA: 0x001E60E8 File Offset: 0x001E42E8
		private void SendGameLoadREQ()
		{
			NKMDiveGameData diveGameData = this.GetDiveGameData();
			if (diveGameData == null)
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_GAME_LOAD_REQ(this.m_NKCDiveGameHUD.GetLastSelectedDeckIndex().m_iIndex, 0, diveGameData.Floor.Templet.StageID, NKMDungeonManager.GetDungeonStrID(diveGameData.Player.PlayerBase.ReservedDungeonID), 0, false, 1, 0);
		}

		// Token: 0x06006105 RID: 24837 RVA: 0x001E6140 File Offset: 0x001E4340
		public override void CloseInternal()
		{
			NKCUIManager.SetScreenInputBlock(false);
			if (this.m_coIntro != null)
			{
				base.StopCoroutine(this.m_coIntro);
				this.m_coIntro = null;
			}
			this.ClearMoveReqCoroutine();
			if (this.m_introSoundUID != 0)
			{
				NKCSoundManager.StopSound(this.m_introSoundUID);
				this.m_introSoundUID = 0;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_NKCDiveGameHUD.Close();
			if (this.m_NKCDiveGameUnit != null)
			{
				this.m_NKCDiveGameUnit.Clear();
			}
			if (this.m_cgCIRCLESET != null)
			{
				this.m_cgCIRCLESET.DOKill(false);
			}
			if (this.m_CIRCLESET != null)
			{
				this.m_CIRCLESET.transform.DOKill(false);
			}
			this.NKCPopupDiveEvent.Close();
			this.NKCPopupDiveArtifactGet.Close();
			NKCUIComVideoCamera subUICameraVideoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
			if (subUICameraVideoPlayer != null)
			{
				subUICameraVideoPlayer.Stop();
			}
			this.m_NKCDiveGameSectorSetMgr.StopAni();
			if (this.m_coDoAfterSectorEventAniEnd != null)
			{
				base.StopCoroutine(this.m_coDoAfterSectorEventAniEnd);
				this.m_coDoAfterSectorEventAniEnd = null;
			}
		}

		// Token: 0x06006106 RID: 24838 RVA: 0x001E624A File Offset: 0x001E444A
		public override void OnBackButton()
		{
			if (NKCUIManager.CheckScreenInputBlock())
			{
				return;
			}
			if (this.m_bWaitingMovie)
			{
				this.m_bWaitingMovie = false;
				return;
			}
			this.OnClickPause();
		}

		// Token: 0x06006107 RID: 24839 RVA: 0x001E626C File Offset: 0x001E446C
		private bool IsSameCol()
		{
			if (this.GetDiveGameData().Player.PlayerBase.Distance == 0)
			{
				return this.GetDiveGameData().Player.PlayerBase.SlotSetIndex == 0;
			}
			return this.GetDiveGameData().Player.PlayerBase.SlotSetIndex == 1;
		}

		// Token: 0x06006108 RID: 24840 RVA: 0x001E62C1 File Offset: 0x001E44C1
		private bool IsSameRow(int row)
		{
			return this.GetDiveGameData().Player.PlayerBase.SlotIndex == row;
		}

		// Token: 0x06006109 RID: 24841 RVA: 0x001E62DC File Offset: 0x001E44DC
		private void UpdateBattleReadyHUD()
		{
			if (this.m_NKCDGArrival.m_NKCDiveGameSector == null)
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_WORLDMAP, true);
				return;
			}
			this.m_NKCDiveGameHUD.UpdateSectorInfoUI(this.m_NKCDGArrival.m_NKCDiveGameSector.GetNKMDiveSlot(), this.IsSameCol());
			this.m_NKCDiveGameHUD.OpenSquadList();
			NKMDiveGameData diveGameData = this.GetDiveGameData();
			if (diveGameData != null)
			{
				if (this.CheckAuto())
				{
					NKMDiveSquad squadForBattleByAuto = diveGameData.Player.GetSquadForBattleByAuto();
					if (squadForBattleByAuto != null)
					{
						this.m_NKCDiveGameHUD.OpenSquadView(squadForBattleByAuto.DeckIndex);
						return;
					}
				}
				else
				{
					this.m_NKCDiveGameHUD.OpenSquadView(diveGameData.Player.PlayerBase.LeaderDeckIndex);
				}
			}
		}

		// Token: 0x0600610A RID: 24842 RVA: 0x001E6384 File Offset: 0x001E4584
		private void UpdateHUDByMoveACK()
		{
			NKMDiveGameData diveGameData = this.GetDiveGameData();
			if (diveGameData == null)
			{
				return;
			}
			if (diveGameData.Player.PlayerBase.State == NKMDivePlayerState.BattleReady)
			{
				this.UpdateBattleReadyHUD();
				return;
			}
			this.m_NKCDiveGameHUD.CloseSectorInfo();
		}

		// Token: 0x0600610B RID: 24843 RVA: 0x001E63C4 File Offset: 0x001E45C4
		private void DoAfterSectorEvent()
		{
			if (this.CheckAuto())
			{
				this.DoNextThingByAuto();
				return;
			}
			if (this.GetDiveGameData().Player.PlayerBase.State == NKMDivePlayerState.SelectArtifact)
			{
				this.NKCPopupDiveArtifactGet.Open(this.GetDiveGameData().Player.PlayerBase.ReservedArtifacts, this.CheckAuto(), new NKCPopupDiveArtifactGet.dOnCloseCallBack(this.OnCloseArtifactGetPopup));
				return;
			}
			if (this.GetDiveGameData().Player.PlayerBase.State == NKMDivePlayerState.Exploring && this.CheckGiveUpRecommendPopupOpenTiming())
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_DIVE_GIVE_UP_RECOMMEND, new NKCPopupOKCancel.OnButton(this.OnClickOkGiveUpINGDive), null, false);
			}
		}

		// Token: 0x0600610C RID: 24844 RVA: 0x001E6468 File Offset: 0x001E4668
		private void OnUnitMoveComplete()
		{
			if (this.m_NKCDGArrival.m_bUpdatedPlayerData)
			{
				this.UpdateDiveGameUnitUI(false);
				this.m_NKCDiveGameHUD.UpdateExploreCountLeftUI();
			}
			if (this.m_NKCDGArrival.m_bOpenEvent)
			{
				if (this.m_NKCDGArrival.m_bSectorAddEvent)
				{
					if (this.m_NKCDGArrival.m_bChangedFloor && this.GetDiveGameData() != null)
					{
						this.m_NKCDiveGameSectorSetMgr.SetUIWhenAddSectorBeforeScan(this.GetDiveGameData());
					}
					this.NKCPopupDiveEvent.Open(this.CheckAuto(), this.m_NKCDGArrival.m_NKCDiveGameSector.GetNKMDiveSlot(), this.m_NKCDGArrival.m_RewardData, delegate
					{
						NKCDiveGame.SetSectorAddEvent(true);
					});
				}
				else
				{
					if (this.m_NKCDGArrival.m_bChangedFloor && this.GetDiveGameData() != null)
					{
						this.m_NKCDiveGameSectorSetMgr.SetUI(this.GetDiveGameData(), false);
					}
					this.NKCPopupDiveEvent.Open(this.CheckAuto(), this.m_NKCDGArrival.m_NKCDiveGameSector.GetNKMDiveSlot(), this.m_NKCDGArrival.m_RewardData, delegate
					{
						this.DoAfterSectorEvent();
					});
				}
			}
			else if (this.m_NKCDGArrival.m_bSectorAddEvent)
			{
				NKCDiveGame.SetSectorAddEvent(true);
				this.m_NKCDiveGameSectorSetMgr.SetUIWhenAddSectorBeforeScan(this.GetDiveGameData());
			}
			else
			{
				if (this.m_NKCDGArrival.m_bChangedFloor && this.GetDiveGameData() != null)
				{
					this.m_NKCDiveGameSectorSetMgr.SetUI(this.GetDiveGameData(), false);
				}
				this.DoAfterSectorEvent();
			}
			if (this.m_NKCDGArrival.m_bUpdateSquadListUI)
			{
				this.m_NKCDiveGameHUD.UpdateSquadListUI();
			}
			this.UpdateHUDByMoveACK();
			this.OpenSectorLinesFromMyPos();
		}

		// Token: 0x0600610D RID: 24845 RVA: 0x001E6600 File Offset: 0x001E4800
		public void OnRecv(NKMPacket_DIVE_SUICIDE_ACK cNKMPacket_DIVE_SUICIDE_ACK)
		{
			if (this.m_Selected_NKCDiveGameSector != null)
			{
				this.m_Selected_NKCDiveGameSector.SetSelected(false);
				this.m_Selected_NKCDiveGameSector = null;
			}
			this.m_NKCDiveGameHUD.SetSelectedSquadSlot(-1);
			this.m_NKCDiveGameHUD.CloseSectorInfo();
			this.m_NKCDiveGameHUD.CloseSquadView();
			this.m_NKCDiveGameHUD.UpdateSquadListUI();
			this.m_SectorLinesFromMyPos.Close();
			this.m_NKCDiveGameSectorSetMgr.SetUI(this.GetDiveGameData(), false);
			this.PlayPrevLeaderUnitDieAni();
			NKCDiveGame.m_bReservedUnitDieShow = false;
		}

		// Token: 0x0600610E RID: 24846 RVA: 0x001E6684 File Offset: 0x001E4884
		public void OnRecv(NKMPacket_DIVE_AUTO_ACK cNKMPacket_DIVE_AUTO_ACK)
		{
			if (cNKMPacket_DIVE_AUTO_ACK.isAuto)
			{
				this.DoNextThingByAuto();
				return;
			}
			if (this.m_coMoveReq != null)
			{
				base.StopCoroutine(this.m_coMoveReq);
				this.m_coMoveReq = null;
			}
		}

		// Token: 0x0600610F RID: 24847 RVA: 0x001E66B0 File Offset: 0x001E48B0
		public void OnFinishScrollToArtifactDummySlot()
		{
			NKCUIDiveGameArtifactSlot component = this.m_NKCDiveGameHUD.m_NKCDiveGameHUDArtifact.m_LoopScrollRect.GetLastActivatedItem().GetComponent<NKCUIDiveGameArtifactSlot>();
			if (component == null)
			{
				return;
			}
			this.NKCPopupDiveArtifactGet.SetEffectDestPos(component.m_NKCUISlot.transform.position);
		}

		// Token: 0x06006110 RID: 24848 RVA: 0x001E6700 File Offset: 0x001E4900
		public void OnRecv(NKMPacket_DIVE_SELECT_ARTIFACT_ACK cNKMPacket_DIVE_SELECT_ARTIFACT_ACK)
		{
			if (this.m_LastSelectedArtifactSlotIndex >= 0)
			{
				this.NKCPopupDiveArtifactGet.PlayOutroAni(this.m_LastSelectedArtifactSlotIndex);
				this.m_NKCDiveGameHUD.m_NKCDiveGameHUDArtifact.SetDummySlot();
				return;
			}
			this.NKCPopupDiveArtifactGet.Close();
			this.m_NKCDiveGameHUD.m_NKCDiveGameHUDArtifact.RefreshInvenry();
		}

		// Token: 0x06006111 RID: 24849 RVA: 0x001E6754 File Offset: 0x001E4954
		public void OnRecv(NKMPacket_DIVE_MOVE_FORWARD_ACK cNKMPacket_DIVE_MOVE_FORWARD_ACK)
		{
			if (cNKMPacket_DIVE_MOVE_FORWARD_ACK.diveSyncData == null)
			{
				return;
			}
			if (this.GetDiveGameData() == null)
			{
				return;
			}
			this.CloseAllSectorLines();
			this.m_NKCDGArrival.Reset();
			NKMDiveSyncData diveSyncData = cNKMPacket_DIVE_MOVE_FORWARD_ACK.diveSyncData;
			this.m_NKCDGArrival.m_bChangedFloor = true;
			if (diveSyncData.AddedSlotSets.Count > 0)
			{
				this.m_NKCDGArrival.m_bSectorAddEvent = true;
			}
			if (diveSyncData.UpdatedPlayer != null)
			{
				this.m_NKCDGArrival.m_bUpdatedPlayerData = true;
			}
			if (diveSyncData.UpdatedSquads.Count > 0)
			{
				this.m_NKCDGArrival.m_bUpdateSquadListUI = true;
			}
			this.m_NKCDiveGameUnit.Move(this.GetPlayerPosByData(true), this.DIVE_UNIT_MOVE_TIME, new NKCDiveGameUnitMover.OnCompleteMove(this.OnUnitMoveComplete));
			NKCCamera.TrackingPos(1.3f, -1f, -1f, this.GetDefaultZDist());
			this.m_NKCDGArrival.m_NKCDiveGameSector = this.m_Selected_NKCDiveGameSector;
			this.m_NKCDGArrival.m_RewardData = cNKMPacket_DIVE_MOVE_FORWARD_ACK.diveSyncData.RewardData;
			if (this.m_Selected_NKCDiveGameSector != null && this.m_Selected_NKCDiveGameSector.GetNKMDiveSlot() != null && NKCDiveManager.IsEuclidSectorType(this.m_Selected_NKCDiveGameSector.GetNKMDiveSlot().SectorType) && this.m_Selected_NKCDiveGameSector.GetNKMDiveSlot().EventType != NKM_DIVE_EVENT_TYPE.NDET_BLANK)
			{
				this.m_NKCDGArrival.m_bOpenEvent = true;
			}
		}

		// Token: 0x06006112 RID: 24850 RVA: 0x001E6894 File Offset: 0x001E4A94
		public void OnRecv(NKMPacket_DIVE_EXPIRE_NOT cNKMPacket_DIVE_EXPIRE_NOT)
		{
			NKMDiveTemplet nkmdiveTemplet = NKMDiveTemplet.Find(cNKMPacket_DIVE_EXPIRE_NOT.stageID);
			if (nkmdiveTemplet == null)
			{
				return;
			}
			if (nkmdiveTemplet.IsEventDive)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().SetReservedDiveReverseAni(true);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_WORLDMAP, true);
				return;
			}
			NKCScenManager.GetScenManager().Get_NKC_SCEN_DIVE_READY().SetTargetEventID(0, 0);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_DIVE_READY, true);
		}

		// Token: 0x06006113 RID: 24851 RVA: 0x001E68F5 File Offset: 0x001E4AF5
		private void CheckTutorial()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.DiveStart, true);
		}

		// Token: 0x04004D18 RID: 19736
		private NKCDiveGameHUD m_NKCDiveGameHUD;

		// Token: 0x04004D19 RID: 19737
		private NKCDiveGame.NKCDGArrival m_NKCDGArrival;

		// Token: 0x04004D1A RID: 19738
		public NKCDiveGameSectorLines m_SectorLinesFromSelected;

		// Token: 0x04004D1B RID: 19739
		public NKCDiveGameSectorLines m_SectorLinesFromMyPos;

		// Token: 0x04004D1C RID: 19740
		public NKCDiveGameSectorLines m_SectorLinesFromSelectedMyPos;

		// Token: 0x04004D1D RID: 19741
		private NKCDiveGameSectorSetMgr m_NKCDiveGameSectorSetMgr;

		// Token: 0x04004D1E RID: 19742
		public NKCDiveGameSector m_StartSector;

		// Token: 0x04004D1F RID: 19743
		public GameObject m_NKM_UI_DIVE_PROCESS_SECTOR_GRID;

		// Token: 0x04004D20 RID: 19744
		private NKCDiveGameSector m_Selected_NKCDiveGameSector;

		// Token: 0x04004D21 RID: 19745
		public GameObject m_NKM_UI_DIVE_PROCESS_UNIT_LAYER;

		// Token: 0x04004D22 RID: 19746
		private NKCDiveGameUnit m_NKCDiveGameUnit;

		// Token: 0x04004D23 RID: 19747
		private Rect m_rtCamBound;

		// Token: 0x04004D24 RID: 19748
		public GameObject m_NKM_UI_DIVE_PROCESS_3D_CONTENT;

		// Token: 0x04004D25 RID: 19749
		private Transform m_CIRCLESET;

		// Token: 0x04004D26 RID: 19750
		private CanvasGroup m_cgCIRCLESET;

		// Token: 0x04004D27 RID: 19751
		private Animator m_NKM_UI_DIVE_PROCESS_3D_SEARCH_ANI;

		// Token: 0x04004D28 RID: 19752
		private Transform m_tr_NKM_UI_DIVE_PROCESS_3D_SEARCH_ANI;

		// Token: 0x04004D29 RID: 19753
		private static bool m_bIntro = false;

		// Token: 0x04004D2A RID: 19754
		private Coroutine m_coIntro;

		// Token: 0x04004D2B RID: 19755
		private static bool m_bSectorAddEventWhenStart = false;

		// Token: 0x04004D2C RID: 19756
		private static bool m_bSectorAddEvent = false;

		// Token: 0x04004D2D RID: 19757
		private static float m_fElapsedTimeForSectorAddEvent = 0f;

		// Token: 0x04004D2E RID: 19758
		private float m_fTimeForSectorAddEvent = 0.35f;

		// Token: 0x04004D2F RID: 19759
		private bool m_bRealSetSectorSets;

		// Token: 0x04004D30 RID: 19760
		private float m_fElapsedTimeForRealSetSecterSets;

		// Token: 0x04004D31 RID: 19761
		private float m_fTimeForRealSetSecterSets = 0.35f;

		// Token: 0x04004D32 RID: 19762
		public float m_KeyScrollSensitivity;

		// Token: 0x04004D33 RID: 19763
		private float m_ScrollReduceTime;

		// Token: 0x04004D34 RID: 19764
		private KeyCode m_prevKeyCode;

		// Token: 0x04004D35 RID: 19765
		private float DIVE_UNIT_MOVE_TIME_ = 1.3f;

		// Token: 0x04004D36 RID: 19766
		private float MOVE_REQ_COROUTINE_WAIT_TIME_ = 2.5f;

		// Token: 0x04004D38 RID: 19768
		private bool m_bPause;

		// Token: 0x04004D39 RID: 19769
		private float t_IdleDeltaTime;

		// Token: 0x04004D3A RID: 19770
		private float m_IdleVoiceInterval = 10f;

		// Token: 0x04004D3B RID: 19771
		private int m_LastSelectedArtifactSlotIndex = -1;

		// Token: 0x04004D3C RID: 19772
		private Coroutine m_coDoAfterSectorEventAniEnd;

		// Token: 0x04004D3D RID: 19773
		private NKCPopupDiveEvent m_NKCPopupDiveEvent;

		// Token: 0x04004D3E RID: 19774
		private NKCPopupDiveArtifactGet m_NKCPopupDiveArtifactGet;

		// Token: 0x04004D3F RID: 19775
		private Coroutine m_coMoveReq;

		// Token: 0x04004D40 RID: 19776
		private static bool m_bReservedUnitDieShow = false;

		// Token: 0x04004D41 RID: 19777
		private static int m_PrevDeckIndexDied = -1;

		// Token: 0x04004D42 RID: 19778
		private static Vector3 m_lastGameUnitPos = new Vector3(0f, 0f, 0f);

		// Token: 0x04004D43 RID: 19779
		public GameObject m_NUM_WARFARE_FX_UNIT_EXPLOSION;

		// Token: 0x04004D44 RID: 19780
		public GameObject m_NUM_WARFARE_FX_UNIT_ESCAPE;

		// Token: 0x04004D45 RID: 19781
		private static NKC_DIVE_GAME_UNIT_DIE_TYPE m_NKC_DIVE_GAME_UNIT_DIE_TYPE = NKC_DIVE_GAME_UNIT_DIE_TYPE.NDGUDT_EXPLOSION;

		// Token: 0x04004D46 RID: 19782
		private bool m_bWaitingBossBeforeCutscenInAuto;

		// Token: 0x04004D47 RID: 19783
		public float MoviePlaySpeed = 1f;

		// Token: 0x04004D48 RID: 19784
		private bool m_bWaitingMovie;

		// Token: 0x04004D49 RID: 19785
		private int m_introSoundUID;

		// Token: 0x020015FF RID: 5631
		public struct NKCDGArrival
		{
			// Token: 0x0600AEDF RID: 44767 RVA: 0x0035B4CE File Offset: 0x003596CE
			public void Reset()
			{
				this.m_bOpenEvent = false;
				this.m_NKCDiveGameSector = null;
				this.m_RewardData = null;
				this.m_bUpdateSquadListUI = false;
				this.m_bSectorAddEvent = false;
				this.m_bChangedFloor = false;
				this.m_bUpdatedPlayerData = false;
			}

			// Token: 0x0400A2D5 RID: 41685
			public bool m_bOpenEvent;

			// Token: 0x0400A2D6 RID: 41686
			public NKCDiveGameSector m_NKCDiveGameSector;

			// Token: 0x0400A2D7 RID: 41687
			public NKMRewardData m_RewardData;

			// Token: 0x0400A2D8 RID: 41688
			public bool m_bUpdateSquadListUI;

			// Token: 0x0400A2D9 RID: 41689
			public bool m_bSectorAddEvent;

			// Token: 0x0400A2DA RID: 41690
			public bool m_bChangedFloor;

			// Token: 0x0400A2DB RID: 41691
			public bool m_bUpdatedPlayerData;
		}
	}
}
