using System;
using NKC.UI;
using NKC.UI.Warfare;
using NKM;
using NKM.Templet;

namespace NKC
{
	// Token: 0x020006C1 RID: 1729
	public class NKCRepeatOperaion
	{
		// Token: 0x06003AEA RID: 15082 RVA: 0x0012ED76 File Offset: 0x0012CF76
		public void SetIsOnGoing(bool bSet)
		{
			this.m_bIsOnGoing = bSet;
		}

		// Token: 0x06003AEB RID: 15083 RVA: 0x0012ED7F File Offset: 0x0012CF7F
		public bool GetIsOnGoing()
		{
			return this.m_bIsOnGoing;
		}

		// Token: 0x06003AEC RID: 15084 RVA: 0x0012ED87 File Offset: 0x0012CF87
		public long GetMaxRepeatCount()
		{
			return this.m_MaxRepeatCount;
		}

		// Token: 0x06003AED RID: 15085 RVA: 0x0012ED8F File Offset: 0x0012CF8F
		public void SetMaxRepeatCount(long count)
		{
			this.m_MaxRepeatCount = count;
		}

		// Token: 0x06003AEE RID: 15086 RVA: 0x0012ED98 File Offset: 0x0012CF98
		public long GetCurrRepeatCount()
		{
			return this.m_CurrRepeatCount;
		}

		// Token: 0x06003AEF RID: 15087 RVA: 0x0012EDA0 File Offset: 0x0012CFA0
		public void SetCurrRepeatCount(long count)
		{
			this.m_CurrRepeatCount = count;
		}

		// Token: 0x06003AF0 RID: 15088 RVA: 0x0012EDA9 File Offset: 0x0012CFA9
		public long GetCostIncreaseCount()
		{
			return this.m_CostIncreaseCount;
		}

		// Token: 0x06003AF1 RID: 15089 RVA: 0x0012EDB1 File Offset: 0x0012CFB1
		public void SetCostIncreaseCount(long count)
		{
			this.m_CostIncreaseCount = count;
		}

		// Token: 0x06003AF2 RID: 15090 RVA: 0x0012EDBA File Offset: 0x0012CFBA
		public long GetPrevCostIncreaseCount()
		{
			return this.m_PrevCostIncreaseCount;
		}

		// Token: 0x06003AF3 RID: 15091 RVA: 0x0012EDC2 File Offset: 0x0012CFC2
		public void ResetReward()
		{
			this.m_NKMRewardData = new NKMRewardData();
		}

		// Token: 0x06003AF4 RID: 15092 RVA: 0x0012EDCF File Offset: 0x0012CFCF
		public void AddReward(NKMRewardData newNKMRewardData)
		{
			this.m_NKMRewardData.AddRewardDataForRepeatOperation(newNKMRewardData);
		}

		// Token: 0x06003AF5 RID: 15093 RVA: 0x0012EDDD File Offset: 0x0012CFDD
		public NKMRewardData GetReward()
		{
			return this.m_NKMRewardData;
		}

		// Token: 0x06003AF6 RID: 15094 RVA: 0x0012EDE5 File Offset: 0x0012CFE5
		public NKMRewardData GetPrevReward()
		{
			return this.m_NKMRewardDataPrev;
		}

		// Token: 0x06003AF7 RID: 15095 RVA: 0x0012EDED File Offset: 0x0012CFED
		public void SetAlarmRepeatOperationQuitByDefeat(bool bSet)
		{
			this.m_bAlarmRepeatOperationQuitByDefeat = bSet;
		}

		// Token: 0x06003AF8 RID: 15096 RVA: 0x0012EDF6 File Offset: 0x0012CFF6
		public bool GetAlarmRepeatOperationQuitByDefeat()
		{
			return this.m_bAlarmRepeatOperationQuitByDefeat;
		}

		// Token: 0x06003AF9 RID: 15097 RVA: 0x0012EDFE File Offset: 0x0012CFFE
		public void SetAlarmRepeatOperationSuccess(bool bSet)
		{
			this.m_bAlarmRepeatOperationSuccess = bSet;
		}

		// Token: 0x06003AFA RID: 15098 RVA: 0x0012EE07 File Offset: 0x0012D007
		public bool GetAlarmRepeatOperationSuccess()
		{
			return this.m_bAlarmRepeatOperationSuccess;
		}

		// Token: 0x06003AFB RID: 15099 RVA: 0x0012EE0F File Offset: 0x0012D00F
		public void SetNeedToSaveRewardData(bool bSet)
		{
			this.m_bNeedToSavePrevData = bSet;
		}

		// Token: 0x06003AFC RID: 15100 RVA: 0x0012EE18 File Offset: 0x0012D018
		public void SetStartTime(DateTime StartDateTime)
		{
			this.m_StartDateTime = StartDateTime;
		}

		// Token: 0x06003AFD RID: 15101 RVA: 0x0012EE21 File Offset: 0x0012D021
		public DateTime GetStartTime()
		{
			return this.m_StartDateTime;
		}

		// Token: 0x06003AFE RID: 15102 RVA: 0x0012EE29 File Offset: 0x0012D029
		public TimeSpan GetPrevProgressDuration()
		{
			return this.m_tsPrevProgressDuration;
		}

		// Token: 0x06003AFF RID: 15103 RVA: 0x0012EE31 File Offset: 0x0012D031
		public int GetPrevCostItemID()
		{
			return this.m_PrevCostItemID;
		}

		// Token: 0x06003B00 RID: 15104 RVA: 0x0012EE39 File Offset: 0x0012D039
		public int GetPrevCostItemCount()
		{
			return this.m_PrevCostItemCount;
		}

		// Token: 0x06003B01 RID: 15105 RVA: 0x0012EE41 File Offset: 0x0012D041
		public long GetPrevRepeatCount()
		{
			return this.m_PrevRepeatCount;
		}

		// Token: 0x06003B02 RID: 15106 RVA: 0x0012EE49 File Offset: 0x0012D049
		public string GetPrevEPTitle()
		{
			return this.m_PrevEPTitle;
		}

		// Token: 0x06003B03 RID: 15107 RVA: 0x0012EE51 File Offset: 0x0012D051
		public string GetPrevEPName()
		{
			return this.m_PrevEPName;
		}

		// Token: 0x06003B04 RID: 15108 RVA: 0x0012EE59 File Offset: 0x0012D059
		public void SetStopReason(string stopReason)
		{
			this.m_StopReason = stopReason;
			this.SetAlarmRepeatOperationQuitByDefeat(false);
			this.SetAlarmRepeatOperationSuccess(false);
		}

		// Token: 0x06003B05 RID: 15109 RVA: 0x0012EE70 File Offset: 0x0012D070
		public string GetStopReason()
		{
			return this.m_StopReason;
		}

		// Token: 0x06003B06 RID: 15110 RVA: 0x0012EE78 File Offset: 0x0012D078
		public void Init()
		{
			if (this.m_bNeedToSavePrevData)
			{
				this.m_bNeedToSavePrevData = false;
				this.m_NKMRewardDataPrev = this.m_NKMRewardData;
				this.m_NKMRewardData = new NKMRewardData();
				this.m_tsPrevProgressDuration = NKCSynchronizedTime.GetServerUTCTime(0.0) - this.GetStartTime();
				this.m_StartDateTime = default(DateTime);
				this.m_PrevRepeatCount = this.m_CurrRepeatCount;
				NKCRepeatOperaion.GetCostInfo(out this.m_PrevCostItemID, out this.m_PrevCostItemCount);
				this.m_PrevEPTitle = NKCPopupRepeatOperation.m_LastEPTitle;
				this.m_PrevEPName = NKCPopupRepeatOperation.m_LastEPName;
				this.m_PrevCostIncreaseCount = this.m_CostIncreaseCount;
			}
			this.SetIsOnGoing(false);
			this.SetMaxRepeatCount(0L);
			this.SetCurrRepeatCount(0L);
			this.SetCostIncreaseCount(0L);
		}

		// Token: 0x06003B07 RID: 15111 RVA: 0x0012EF38 File Offset: 0x0012D138
		public void UpdateRepeatOperationGameHudUI()
		{
			NKM_SCEN_ID nowScenID = NKCScenManager.GetScenManager().GetNowScenID();
			if (nowScenID == NKM_SCEN_ID.NSI_WARFARE_GAME)
			{
				NKCWarfareGame warfareGame = NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareGame();
				if (warfareGame != null && warfareGame.m_NKCWarfareGameHUD != null)
				{
					warfareGame.m_NKCWarfareGameHUD.SetActiveRepeatOperationOnOff(this.GetIsOnGoing());
					return;
				}
			}
			else if (nowScenID == NKM_SCEN_ID.NSI_GAME)
			{
				NKCGameClient gameClient = NKCScenManager.GetScenManager().GetGameClient();
				if (gameClient != null && gameClient.GetGameHud() != null && gameClient.GetGameHud().GetNKCGameHUDRepeatOperation() != null)
				{
					gameClient.GetGameHud().GetNKCGameHUDRepeatOperation().ResetBtnOnOffUI();
				}
			}
		}

		// Token: 0x06003B08 RID: 15112 RVA: 0x0012EFD1 File Offset: 0x0012D1D1
		public void OnClickCancel()
		{
			NKCScenManager.GetScenManager().GetNKCRepeatOperaion().Init();
			this.UpdateRepeatOperationGameHudUI();
		}

		// Token: 0x06003B09 RID: 15113 RVA: 0x0012EFE8 File Offset: 0x0012D1E8
		public bool CheckRepeatOperationRealStop()
		{
			if (NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing())
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString("SI_POPUP_REPEAT_OPERATION_SCENE_EXIT", false), delegate()
				{
					NKCScenManager.GetScenManager().GetNKCRepeatOperaion().Init();
					NKCScenManager.GetScenManager().GetNKCRepeatOperaion().SetStopReason(NKCUtilString.GET_STRING_REPEAT_OPERATION_IS_TERMINATED);
					NKCPopupRepeatOperation.Instance.OpenForResult(null);
					this.OnClickCancel();
				}, null, false);
				return true;
			}
			return false;
		}

		// Token: 0x06003B0A RID: 15114 RVA: 0x0012F024 File Offset: 0x0012D224
		public static void GetCostInfo(out int costItemID, out int costItemCount)
		{
			costItemID = 0;
			costItemCount = 0;
			NKC_REPEAT_OPERATION_TYPE nkc_REPEAT_OPERATION_TYPE;
			NKMStageTempletV2 nkmstageTempletV;
			if (!NKCRepeatOperaion.GetRepeatOperationType(out nkc_REPEAT_OPERATION_TYPE, out nkmstageTempletV))
			{
				return;
			}
			if (nkc_REPEAT_OPERATION_TYPE == NKC_REPEAT_OPERATION_TYPE.NROT_WARFARE)
			{
				NKCWarfareManager.GetCurrWarfareAttackCost(out costItemID, out costItemCount);
				return;
			}
			if (nkmstageTempletV != null && nkmstageTempletV.m_StageReqItemID > 0)
			{
				costItemID = nkmstageTempletV.m_StageReqItemID;
				costItemCount = nkmstageTempletV.m_StageReqItemCount;
				if (nkmstageTempletV.m_StageReqItemID == 2)
				{
					NKCCompanyBuff.SetDiscountOfEterniumInEnteringDungeon(NKCScenManager.CurrentUserData().m_companyBuffDataList, ref costItemCount);
				}
			}
		}

		// Token: 0x06003B0B RID: 15115 RVA: 0x0012F084 File Offset: 0x0012D284
		public static string GetEpisodeBattleName()
		{
			NKC_REPEAT_OPERATION_TYPE nkc_REPEAT_OPERATION_TYPE;
			NKMStageTempletV2 cNKMStageTemplet;
			if (!NKCRepeatOperaion.GetRepeatOperationType(out nkc_REPEAT_OPERATION_TYPE, out cNKMStageTemplet))
			{
				return "";
			}
			return NKMEpisodeMgr.GetEpisodeBattleName(cNKMStageTemplet);
		}

		// Token: 0x06003B0C RID: 15116 RVA: 0x0012F0A8 File Offset: 0x0012D2A8
		public static bool GetRepeatOperationType(out NKC_REPEAT_OPERATION_TYPE eNKC_REPEAT_OPERATION_TYPE, out NKMStageTempletV2 stageTemplet)
		{
			eNKC_REPEAT_OPERATION_TYPE = NKC_REPEAT_OPERATION_TYPE.NROT_NONE;
			stageTemplet = null;
			NKM_SCEN_ID nowScenID = NKCScenManager.GetScenManager().GetNowScenID();
			if (nowScenID == NKM_SCEN_ID.NSI_WARFARE_GAME)
			{
				eNKC_REPEAT_OPERATION_TYPE = NKC_REPEAT_OPERATION_TYPE.NROT_WARFARE;
			}
			else if (nowScenID == NKM_SCEN_ID.NSI_GAME)
			{
				NKCGameClient gameClient = NKCScenManager.GetScenManager().GetGameClient();
				if (gameClient == null || gameClient.GetGameData() == null)
				{
					return false;
				}
				NKM_GAME_TYPE gameType = gameClient.GetGameData().GetGameType();
				if (gameType != NKM_GAME_TYPE.NGT_WARFARE)
				{
					if (gameType == NKM_GAME_TYPE.NGT_PHASE)
					{
						stageTemplet = NKCPhaseManager.GetStageTemplet();
					}
					else
					{
						eNKC_REPEAT_OPERATION_TYPE = NKC_REPEAT_OPERATION_TYPE.NROT_DUNGEON;
						NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(gameClient.GetGameData().m_DungeonID);
						stageTemplet = ((dungeonTempletBase != null) ? dungeonTempletBase.StageTemplet : null);
					}
				}
				else
				{
					eNKC_REPEAT_OPERATION_TYPE = NKC_REPEAT_OPERATION_TYPE.NROT_WARFARE;
				}
			}
			else if (nowScenID == NKM_SCEN_ID.NSI_DUNGEON_ATK_READY)
			{
				eNKC_REPEAT_OPERATION_TYPE = NKC_REPEAT_OPERATION_TYPE.NROT_DUNGEON;
				NKC_SCEN_DUNGEON_ATK_READY scen_DUNGEON_ATK_READY = NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY();
				stageTemplet = scen_DUNGEON_ATK_READY.GetStageTemplet();
			}
			else
			{
				if (nowScenID != NKM_SCEN_ID.NSI_GAME_RESULT)
				{
					return false;
				}
				NKC_SCEN_GAME_RESULT nkc_SCEN_GAME_RESULT = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAME_RESULT();
				stageTemplet = NKMStageTempletV2.Find(nkc_SCEN_GAME_RESULT.GetStageID());
			}
			if (eNKC_REPEAT_OPERATION_TYPE == NKC_REPEAT_OPERATION_TYPE.NROT_WARFARE)
			{
				NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareStrID());
				stageTemplet = ((nkmwarfareTemplet != null) ? nkmwarfareTemplet.StageTemplet : null);
			}
			return true;
		}

		// Token: 0x06003B0D RID: 15117 RVA: 0x0012F1A4 File Offset: 0x0012D3A4
		public static bool CheckPossibleForWarfare(string warfareStrID, bool bShowMessage = true)
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.OPERATION_REPEAT, 0, 0))
			{
				if (bShowMessage)
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.OPERATION_REPEAT, 0);
				}
				return false;
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.WARFARE_AUTO_MOVE, 0, 0))
			{
				if (bShowMessage)
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.WARFARE_AUTO_MOVE, 0);
				}
				return false;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (!myUserData.IsSuperUser() && !myUserData.CheckWarfareClear(warfareStrID))
			{
				if (bShowMessage)
				{
					NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_CONTENTS_UNLOCK_CLEAR_STAGE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				}
				return false;
			}
			return true;
		}

		// Token: 0x06003B0E RID: 15118 RVA: 0x0012F218 File Offset: 0x0012D418
		public static bool CheckPossible(int stageID, bool bShowMessage = true)
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.OPERATION_REPEAT, 0, 0))
			{
				if (bShowMessage)
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.OPERATION_REPEAT, 0);
				}
				return false;
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.BATTLE_AUTO_RESPAWN, 0, 0))
			{
				if (bShowMessage)
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.BATTLE_AUTO_RESPAWN, 0);
				}
				return false;
			}
			NKMStageTempletV2 nkmstageTempletV = NKMStageTempletV2.Find(stageID);
			if (nkmstageTempletV == null)
			{
				return false;
			}
			if (nkmstageTempletV.EnterLimit > 0)
			{
				int statePlayCnt = NKCScenManager.CurrentUserData().GetStatePlayCnt(nkmstageTempletV.Key, false, false, false);
				if (nkmstageTempletV.EnterLimit - statePlayCnt <= 0)
				{
					return false;
				}
			}
			bool flag = false;
			if (nkmstageTempletV.IsUsingEventDeck())
			{
				if (NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY().GetLastEventDeck() == null)
				{
					flag = true;
				}
			}
			else if (NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY().GetLastDeckIndex().m_eDeckType == NKM_DECK_TYPE.NDT_NONE)
			{
				flag = true;
			}
			if (flag)
			{
				if (bShowMessage)
				{
					NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString("SI_TOAST_REPEAT_UNABLE_BY_RECONNECT_DUNGEON", false), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				}
				return false;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (!myUserData.IsSuperUser() && !myUserData.CheckStageCleared(stageID))
			{
				if (bShowMessage)
				{
					NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_CONTENTS_UNLOCK_CLEAR_STAGE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				}
				return false;
			}
			return true;
		}

		// Token: 0x06003B0F RID: 15119 RVA: 0x0012F31A File Offset: 0x0012D51A
		public static bool CheckVisible(NKMStageTempletV2 stageTemplet)
		{
			return stageTemplet != null && (stageTemplet.m_STAGE_TYPE != STAGE_TYPE.ST_DUNGEON || stageTemplet.DungeonTempletBase == null || !NKMDungeonManager.IsTutorialDungeon(stageTemplet.DungeonTempletBase.m_DungeonID)) && !stageTemplet.m_bNoAutoRepeat;
		}

		// Token: 0x06003B10 RID: 15120 RVA: 0x0012F351 File Offset: 0x0012D551
		public static bool CheckVisible(int stageID)
		{
			return NKCRepeatOperaion.CheckVisible(NKMStageTempletV2.Find(stageID));
		}

		// Token: 0x04003536 RID: 13622
		private bool m_bIsOnGoing;

		// Token: 0x04003537 RID: 13623
		private long m_MaxRepeatCount;

		// Token: 0x04003538 RID: 13624
		private long m_CurrRepeatCount;

		// Token: 0x04003539 RID: 13625
		private long m_CostIncreaseCount;

		// Token: 0x0400353A RID: 13626
		private long m_PrevCostIncreaseCount;

		// Token: 0x0400353B RID: 13627
		private NKMRewardData m_NKMRewardDataPrev = new NKMRewardData();

		// Token: 0x0400353C RID: 13628
		private NKMRewardData m_NKMRewardData = new NKMRewardData();

		// Token: 0x0400353D RID: 13629
		private bool m_bAlarmRepeatOperationQuitByDefeat;

		// Token: 0x0400353E RID: 13630
		private bool m_bAlarmRepeatOperationSuccess;

		// Token: 0x0400353F RID: 13631
		private bool m_bNeedToSavePrevData;

		// Token: 0x04003540 RID: 13632
		private DateTime m_StartDateTime;

		// Token: 0x04003541 RID: 13633
		private TimeSpan m_tsPrevProgressDuration = new TimeSpan(0L);

		// Token: 0x04003542 RID: 13634
		private int m_PrevCostItemID;

		// Token: 0x04003543 RID: 13635
		private int m_PrevCostItemCount;

		// Token: 0x04003544 RID: 13636
		private long m_PrevRepeatCount;

		// Token: 0x04003545 RID: 13637
		private string m_PrevEPTitle = "";

		// Token: 0x04003546 RID: 13638
		private string m_PrevEPName = "";

		// Token: 0x04003547 RID: 13639
		private string m_StopReason = "";
	}
}
