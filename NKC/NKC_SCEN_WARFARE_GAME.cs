using System;
using ClientPacket.Warfare;
using Cs.Logging;
using NKC.UI;
using NKC.UI.Warfare;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000733 RID: 1843
	public class NKC_SCEN_WARFARE_GAME : NKC_SCEN_BASIC
	{
		// Token: 0x0600495A RID: 18778 RVA: 0x0016109A File Offset: 0x0015F29A
		public Transform GetWarfareParentTransform()
		{
			GameObject num_WARFARE = this.m_NUM_WARFARE;
			if (num_WARFARE == null)
			{
				return null;
			}
			return num_WARFARE.transform;
		}

		// Token: 0x0600495B RID: 18779 RVA: 0x001610AD File Offset: 0x0015F2AD
		public NKC_SCEN_WARFARE_GAME()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_WARFARE_GAME;
			this.m_NUM_WARFARE = GameObject.Find("NUM_WARFARE");
		}

		// Token: 0x0600495C RID: 18780 RVA: 0x001610DF File Offset: 0x0015F2DF
		public NKCWarfareGame.RetryData GetRetryData()
		{
			return this.m_retryData;
		}

		// Token: 0x0600495D RID: 18781 RVA: 0x001610E7 File Offset: 0x0015F2E7
		public void OpenWaitBox()
		{
			if (this.m_NKCWarfareGame != null)
			{
				this.m_NKCWarfareGame.OpenWaitBox();
			}
		}

		// Token: 0x0600495E RID: 18782 RVA: 0x00161102 File Offset: 0x0015F302
		public string GetWarfareStrID()
		{
			return this.m_WarfareStrID;
		}

		// Token: 0x0600495F RID: 18783 RVA: 0x0016110A File Offset: 0x0015F30A
		public void SetWarfareStrID(string warfareStrID)
		{
			this.m_WarfareStrID = warfareStrID;
			if (this.m_NKCWarfareGame != null)
			{
				this.m_NKCWarfareGame.SetWarfareStrID(this.m_WarfareStrID);
			}
		}

		// Token: 0x06004960 RID: 18784 RVA: 0x00161132 File Offset: 0x0015F332
		public void SetRetry(bool bSet)
		{
			this.m_bRetry = bSet;
		}

		// Token: 0x06004961 RID: 18785 RVA: 0x0016113B File Offset: 0x0015F33B
		public void SetBattleInfo(WarfareGameData gameData, WarfareSyncData syncData)
		{
			if (gameData == null || syncData == null)
			{
				return;
			}
			this.m_DataBeforeBattle = new NKCWarfareGame.DataBeforeBattle(gameData, syncData);
		}

		// Token: 0x06004962 RID: 18786 RVA: 0x00161151 File Offset: 0x0015F351
		public void OnRecv(NKMPacket_WARFARE_GAME_START_ACK cNKMPacket_WARFARE_GAME_START_ACK)
		{
			if (cNKMPacket_WARFARE_GAME_START_ACK.errorCode == NKM_ERROR_CODE.NEC_OK)
			{
				this.m_retryData = new NKCWarfareGame.RetryData(this.m_WarfareStrID, cNKMPacket_WARFARE_GAME_START_ACK.warfareGameData.warfareTeamDataA);
			}
			if (this.m_NKCWarfareGame != null)
			{
				this.m_NKCWarfareGame.OnRecv(cNKMPacket_WARFARE_GAME_START_ACK);
			}
		}

		// Token: 0x06004963 RID: 18787 RVA: 0x00161191 File Offset: 0x0015F391
		public void OnRecv(NKMPacket_WARFARE_GAME_MOVE_ACK cNKMPacket_WARFARE_GAME_MOVE_ACK)
		{
			if (this.m_NKCWarfareGame != null)
			{
				this.m_NKCWarfareGame.OnRecv(cNKMPacket_WARFARE_GAME_MOVE_ACK);
			}
		}

		// Token: 0x06004964 RID: 18788 RVA: 0x001611AD File Offset: 0x0015F3AD
		public void OnRecv(NKMPacket_WARFARE_GAME_TURN_FINISH_ACK cNKMPacket_WARFARE_GAME_TURN_FINISH_ACK)
		{
			if (this.m_NKCWarfareGame != null)
			{
				this.m_NKCWarfareGame.OnRecv(cNKMPacket_WARFARE_GAME_TURN_FINISH_ACK);
			}
		}

		// Token: 0x06004965 RID: 18789 RVA: 0x001611C9 File Offset: 0x0015F3C9
		public void InitWaitNextOrder()
		{
			NKCWarfareGame nkcwarfareGame = this.m_NKCWarfareGame;
			if (nkcwarfareGame == null)
			{
				return;
			}
			nkcwarfareGame.InitWaitNextOrder();
		}

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x06004966 RID: 18790 RVA: 0x001611DB File Offset: 0x0015F3DB
		// (set) Token: 0x06004967 RID: 18791 RVA: 0x001611EE File Offset: 0x0015F3EE
		public bool WaitAutoPacekt
		{
			get
			{
				NKCWarfareGame nkcwarfareGame = this.m_NKCWarfareGame;
				return nkcwarfareGame != null && nkcwarfareGame.WaitAutoPacket;
			}
			set
			{
				if (this.m_NKCWarfareGame != null)
				{
					this.m_NKCWarfareGame.WaitAutoPacket = value;
				}
			}
		}

		// Token: 0x06004968 RID: 18792 RVA: 0x0016120A File Offset: 0x0015F40A
		public void OnRecv(NKMPacket_WARFARE_GAME_NEXT_ORDER_ACK cNKMPacket_WARFARE_GAME_NEXT_ORDER_ACK)
		{
			if (this.m_NKCWarfareGame != null)
			{
				this.m_NKCWarfareGame.OnRecv(cNKMPacket_WARFARE_GAME_NEXT_ORDER_ACK);
			}
		}

		// Token: 0x06004969 RID: 18793 RVA: 0x00161226 File Offset: 0x0015F426
		public void OnRecv(NKMPacket_WARFARE_GAME_USE_SERVICE_ACK cNKMPacket_WARFARE_GAME_USE_SERVICE_ACK)
		{
			if (this.m_NKCWarfareGame != null)
			{
				this.m_NKCWarfareGame.OnRecv(cNKMPacket_WARFARE_GAME_USE_SERVICE_ACK);
			}
		}

		// Token: 0x0600496A RID: 18794 RVA: 0x00161242 File Offset: 0x0015F442
		public void OnRecv(NKMPacket_WARFARE_EXPIRED_NOT cNKMPacket_WARFARE_EXPIRED_NOT)
		{
			if (NKCWarfareGame.IsInstanceOpen)
			{
				NKCWarfareGame.GetInstance().ForceBack();
			}
		}

		// Token: 0x0600496B RID: 18795 RVA: 0x00161258 File Offset: 0x0015F458
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			this.m_NUM_WARFARE.SetActive(true);
			if (!NKCUIManager.IsValid(this.m_WarfareGameUIData))
			{
				this.m_WarfareGameUIData = NKCWarfareGame.OpenNewInstanceAsync();
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			if (warfareGameData == null)
			{
				Debug.LogError("NKC_SCEN_WARFARE_GAME.LoadUIStart - WarfareGameData is null");
				return;
			}
			NKM_WARFARE_GAME_STATE warfareGameState = warfareGameData.warfareGameState;
			if (warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_STOP)
			{
				if (warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_RESULT)
				{
					return;
				}
				NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
				if (warfareGameData.isWinTeamA && !NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing() && (NKCUtil.m_sHsFirstClearWarfare.Contains(warfareGameData.warfareTempletID) || NKCScenManager.CurrentUserData().m_UserOption.m_bPlayCutscene) && nkmwarfareTemplet != null && NKCCutScenManager.GetCutScenTemple(nkmwarfareTemplet.m_CutScenStrIDAfter) != null)
				{
					NKCUICutScenPlayer.Instance.UnLoad();
					NKCUICutScenPlayer.Instance.Load(nkmwarfareTemplet.m_CutScenStrIDAfter, true);
				}
			}
			else
			{
				NKMWarfareTemplet nkmwarfareTemplet2 = NKMWarfareTemplet.Find(this.m_WarfareStrID);
				if (nkmwarfareTemplet2 != null && !NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing() && (!myUserData.CheckWarfareClear(this.m_WarfareStrID) || NKCScenManager.CurrentUserData().m_UserOption.m_bPlayCutscene) && NKCCutScenManager.GetCutScenTemple(nkmwarfareTemplet2.m_CutScenStrIDBefore) != null)
				{
					NKCUICutScenPlayer.Instance.UnLoad();
					NKCUICutScenPlayer.Instance.Load(nkmwarfareTemplet2.m_CutScenStrIDBefore, true);
					return;
				}
			}
		}

		// Token: 0x0600496C RID: 18796 RVA: 0x001613B0 File Offset: 0x0015F5B0
		public override void ScenLoadUIComplete()
		{
			base.ScenLoadUIComplete();
			if (!this.m_bLoadedUI && this.m_NKCWarfareGame == null)
			{
				if (this.m_WarfareGameUIData == null || !this.m_WarfareGameUIData.CheckLoadAndGetInstance<NKCWarfareGame>(out this.m_NKCWarfareGame))
				{
					Debug.LogError("Error - NKC_SCEN_WARFARE_GAME.ScenLoadComplete() : UI Load Failed!");
					return;
				}
				this.m_NKCWarfareGame.transform.SetParent(this.m_NUM_WARFARE.transform, false);
				this.m_NKCWarfareGame.InitUI();
			}
			if (this.m_NKCWarfareGame != null)
			{
				this.m_NKCWarfareGame.SetWarfareStrID(this.m_WarfareStrID);
			}
		}

		// Token: 0x0600496D RID: 18797 RVA: 0x00161447 File Offset: 0x0015F647
		public override void ScenLoadLastStart()
		{
			base.ScenLoadLastStart();
			this.m_NKC_SCEN_STATE = NKC_SCEN_STATE.NSS_LOADING_LAST;
		}

		// Token: 0x0600496E RID: 18798 RVA: 0x00161456 File Offset: 0x0015F656
		public override void ScenLoadComplete()
		{
			base.ScenLoadComplete();
		}

		// Token: 0x0600496F RID: 18799 RVA: 0x0016145E File Offset: 0x0015F65E
		public void SetEpisodeID(bool bReservedDetailOption = false)
		{
			if (NKMWarfareTemplet.Find(this.m_WarfareStrID) == null || NKMEpisodeMgr.FindStageTempletByBattleStrID(this.m_WarfareStrID) != null)
			{
			}
		}

		// Token: 0x06004970 RID: 18800 RVA: 0x0016147C File Offset: 0x0015F67C
		public void TryPause()
		{
			if (this.m_NKCWarfareGame == null)
			{
				return;
			}
			if (!this.m_NKCWarfareGame.IsOpen)
			{
				return;
			}
			this.m_NKCWarfareGame.OnClickPause();
		}

		// Token: 0x06004971 RID: 18801 RVA: 0x001614A6 File Offset: 0x0015F6A6
		public void TryGiveUp()
		{
			if (this.m_NKCWarfareGame != null)
			{
				this.m_NKCWarfareGame.GiveUp();
			}
		}

		// Token: 0x06004972 RID: 18802 RVA: 0x001614C1 File Offset: 0x0015F6C1
		public void TryTempLeave()
		{
			if (this.m_NKCWarfareGame != null)
			{
				this.m_NKCWarfareGame.TempLeave();
			}
		}

		// Token: 0x06004973 RID: 18803 RVA: 0x001614DC File Offset: 0x0015F6DC
		public void ProcessReLogin()
		{
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			if (warfareGameData != null)
			{
				if (warfareGameData.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_STOP)
				{
					if (!NKCUICutScenPlayer.IsInstanceOpen || !NKCUICutScenPlayer.Instance.IsPlaying())
					{
						this.SaveTutorialSelectDeck();
						Log.Debug("WarfareGame - ScenChangeFade(WarfareGame)", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKC_SCEN_WARFARE_GAME.cs", 322);
						NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_WARFARE_GAME, true);
						return;
					}
					Log.Debug("WarfareGame - 컷씬 플레이중", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKC_SCEN_WARFARE_GAME.cs", 313);
					if (this.m_NKCWarfareGame != null)
					{
						this.m_NKCWarfareGame.ResetGameOption();
						return;
					}
				}
				else
				{
					NKCScenManager.GetScenManager().WarfareGameData.warfareTempletID = NKCWarfareManager.GetWarfareID(this.m_WarfareStrID);
					if (this.m_NKCWarfareGame != null)
					{
						this.m_NKCWarfareGame.SetUserUnitDeckWarfareState();
					}
				}
			}
		}

		// Token: 0x06004974 RID: 18804 RVA: 0x001615A0 File Offset: 0x0015F7A0
		public override void ScenStart()
		{
			base.ScenStart();
			NKCWarfareGame.RetryData retryData = null;
			if (this.m_bRetry)
			{
				retryData = this.m_retryData;
				this.m_retryData = null;
				this.m_bRetry = false;
			}
			if (this.m_NKCWarfareGame != null)
			{
				this.m_NKCWarfareGame.Open(this.m_bAfterBattle, this.m_DataBeforeBattle, retryData);
			}
			this.m_bAfterBattle = false;
			this.m_DataBeforeBattle = null;
			this.SetEpisodeID(false);
			this.RefreshTutorialSelectDeck();
		}

		// Token: 0x06004975 RID: 18805 RVA: 0x00161613 File Offset: 0x0015F813
		public void OnRecv(NKMPacket_WARFARE_GAME_GIVE_UP_ACK cNKMPacket_WARFARE_GAME_GIVE_UP_ACK)
		{
			NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().SetEpisodeID(true);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OPERATION, true);
			NKCScenManager.GetScenManager().GetNKCRepeatOperaion().Init();
		}

		// Token: 0x06004976 RID: 18806 RVA: 0x00161641 File Offset: 0x0015F841
		public void OnRecv(NKMPacket_WARFARE_GAME_AUTO_ACK cNKMPacket_WARFARE_GAME_AUTO_ACK)
		{
			if (this.m_NKCWarfareGame != null)
			{
				this.m_NKCWarfareGame.SetActiveAutoOnOff(cNKMPacket_WARFARE_GAME_AUTO_ACK.isAuto, cNKMPacket_WARFARE_GAME_AUTO_ACK.isAutoRepair);
			}
		}

		// Token: 0x06004977 RID: 18807 RVA: 0x00161668 File Offset: 0x0015F868
		public void OnRecv(NKMPacket_WARFARE_FRIEND_LIST_ACK sPacket)
		{
			if (this.m_NKCWarfareGame != null)
			{
				this.m_NKCWarfareGame.OnRecv(sPacket);
			}
		}

		// Token: 0x06004978 RID: 18808 RVA: 0x00161684 File Offset: 0x0015F884
		public void OnRecv(NKMPacket_WARFARE_RECOVER_ACK sPacket)
		{
			if (this.m_NKCWarfareGame != null)
			{
				this.m_NKCWarfareGame.OnRecv(sPacket);
			}
		}

		// Token: 0x06004979 RID: 18809 RVA: 0x001616A0 File Offset: 0x0015F8A0
		public void SetReservedShowBattleResult(bool bSet)
		{
			this.m_bAfterBattle = bSet;
		}

		// Token: 0x0600497A RID: 18810 RVA: 0x001616AC File Offset: 0x0015F8AC
		public override void ScenEnd()
		{
			if (NKCUICutScenPlayer.HasInstance)
			{
				NKCUICutScenPlayer.Instance.StopWithInvalidatingCallBack();
				NKCUICutScenPlayer.Instance.UnLoad();
			}
			if (this.m_NKCWarfareGame != null)
			{
				this.m_NKCWarfareGame.Close();
			}
			base.ScenEnd();
			if (this.m_NUM_WARFARE != null)
			{
				this.m_NUM_WARFARE.SetActive(false);
			}
			this.UnloadUI();
		}

		// Token: 0x0600497B RID: 18811 RVA: 0x00161713 File Offset: 0x0015F913
		public override void UnloadUI()
		{
			base.UnloadUI();
			this.m_NKCWarfareGame = null;
			NKCUIManager.LoadedUIData warfareGameUIData = this.m_WarfareGameUIData;
			if (warfareGameUIData != null)
			{
				warfareGameUIData.CloseInstance();
			}
			this.m_WarfareGameUIData = null;
		}

		// Token: 0x0600497C RID: 18812 RVA: 0x0016173A File Offset: 0x0015F93A
		public override void ScenUpdate()
		{
			base.ScenUpdate();
		}

		// Token: 0x0600497D RID: 18813 RVA: 0x00161742 File Offset: 0x0015F942
		public override bool ScenMsgProc(NKCMessageData cNKCMessageData)
		{
			return false;
		}

		// Token: 0x0600497E RID: 18814 RVA: 0x00161745 File Offset: 0x0015F945
		public void DoAfterLogout()
		{
			this.m_retryData = null;
		}

		// Token: 0x0600497F RID: 18815 RVA: 0x0016174E File Offset: 0x0015F94E
		public NKCWarfareGame GetWarfareGame()
		{
			return this.m_NKCWarfareGame;
		}

		// Token: 0x06004980 RID: 18816 RVA: 0x00161756 File Offset: 0x0015F956
		private void SaveTutorialSelectDeck()
		{
			NKCWarfareGame nkcwarfareGame = this.m_NKCWarfareGame;
			this.TutorialSelectDeckIndex = ((nkcwarfareGame != null) ? nkcwarfareGame.GetTutorialSelectDeck() : -1);
		}

		// Token: 0x06004981 RID: 18817 RVA: 0x00161770 File Offset: 0x0015F970
		public void RefreshTutorialSelectDeck()
		{
			if (!NKCGameEventManager.IsEventPlaying())
			{
				return;
			}
			NKCWarfareGame nkcwarfareGame = this.m_NKCWarfareGame;
			if (nkcwarfareGame != null)
			{
				nkcwarfareGame.RefreshTutorialSelectDeck(this.TutorialSelectDeckIndex);
			}
			this.TutorialSelectDeckIndex = -1;
			NKCGameEventManager.ResumeEvent();
		}

		// Token: 0x0400389B RID: 14491
		private GameObject m_NUM_WARFARE;

		// Token: 0x0400389C RID: 14492
		public NKCUIManager.LoadedUIData m_WarfareGameUIData;

		// Token: 0x0400389D RID: 14493
		private NKCWarfareGame m_NKCWarfareGame;

		// Token: 0x0400389E RID: 14494
		private string m_WarfareStrID = "";

		// Token: 0x0400389F RID: 14495
		private bool m_bAfterBattle;

		// Token: 0x040038A0 RID: 14496
		private bool m_bRetry;

		// Token: 0x040038A1 RID: 14497
		private NKCWarfareGame.DataBeforeBattle m_DataBeforeBattle;

		// Token: 0x040038A2 RID: 14498
		private NKCWarfareGame.RetryData m_retryData;

		// Token: 0x040038A3 RID: 14499
		private int TutorialSelectDeckIndex = -1;
	}
}
