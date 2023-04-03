using System;
using System.Collections;
using System.Collections.Generic;
using ClientPacket.Warfare;
using DG.Tweening;
using NKC.UI;
using NKC.UI.Warfare;
using NKM;
using NKM.Shop;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007DD RID: 2013
	public class NKCWarfareGameHUD : MonoBehaviour
	{
		// Token: 0x06004F5D RID: 20317 RVA: 0x0017F888 File Offset: 0x0017DA88
		public int GetCurrMultiplyRewardCount()
		{
			return this.m_CurrMultiplyRewardCount;
		}

		// Token: 0x06004F5E RID: 20318 RVA: 0x0017F890 File Offset: 0x0017DA90
		public void InitUI(NKCWarfareGame cNKCWarfareGame, NKCWarfareGameHUD.OnStartUserPhase onStartUserPhase, NKCWarfareGameHUD.OnStartEnemyPhase onStartEnemyPhase)
		{
			this.m_NKCWarfareGame = cNKCWarfareGame;
			this.m_NUF_WARFARE = base.gameObject;
			this.m_NUF_WARFARE.SetActive(false);
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_INFO_MEDALINFO_DETAIL, true);
			this.m_NUF_WARFARE_INFO_MEDALINFO_DETAIL.transform.localScale = new Vector3(1f, 1f, 1f);
			if (this.m_ResourceButton != null)
			{
				this.m_ResourceButton.PointerClick.RemoveAllListeners();
				this.m_ResourceButton.PointerClick.AddListener(delegate()
				{
					cNKCWarfareGame.OnClickGameStart(false);
				});
			}
			this.m_NUF_WARFARE_PAUSE_BUTTON.PointerClick.RemoveAllListeners();
			this.m_NUF_WARFARE_PAUSE_BUTTON.PointerClick.AddListener(new UnityAction(cNKCWarfareGame.OnClickPause));
			this.m_NUF_WARFARE_AUTO_ON_btn.PointerClick.RemoveAllListeners();
			this.m_NUF_WARFARE_AUTO_ON_btn.PointerClick.AddListener(delegate()
			{
				this.SendAutoReq(false);
			});
			this.m_NUF_WARFARE_AUTO_OFF_Btn.PointerClick.RemoveAllListeners();
			this.m_NUF_WARFARE_AUTO_OFF_Btn.PointerClick.AddListener(delegate()
			{
				this.SendAutoReq(true);
			});
			this.m_NUF_WARFARE_REPEAT_ON_btn.PointerClick.RemoveAllListeners();
			this.m_NUF_WARFARE_REPEAT_ON_btn.PointerClick.AddListener(new UnityAction(this.OnClickOperationRepeat));
			this.m_NUF_WARFARE_REPEAT_OFF_Btn.PointerClick.RemoveAllListeners();
			this.m_NUF_WARFARE_REPEAT_OFF_Btn.PointerClick.AddListener(new UnityAction(this.OnClickOperationRepeat));
			this.m_NUF_WARFARE_AUTO_SUPPLY_OFF_btn.PointerClick.RemoveAllListeners();
			this.m_NUF_WARFARE_AUTO_SUPPLY_OFF_btn.PointerClick.AddListener(delegate()
			{
				this.SendAutoSupplyReq(true);
			});
			this.m_NUF_WARFARE_AUTO_SUPPLY_ON_btn.PointerClick.RemoveAllListeners();
			this.m_NUF_WARFARE_AUTO_SUPPLY_ON_btn.PointerClick.AddListener(delegate()
			{
				this.SendAutoSupplyReq(false);
			});
			this.m_NUF_WARFARE_INFO_WARINFO.PointerClick.RemoveAllListeners();
			this.m_NUF_WARFARE_INFO_WARINFO.PointerClick.AddListener(new UnityAction(this.OpenWarfareInfoPopup));
			this.m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_BUTTON.PointerClick.RemoveAllListeners();
			this.m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_BUTTON.PointerClick.AddListener(new UnityAction(cNKCWarfareGame.OnClickNextTurn));
			this.m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_BUTTON_RED.PointerClick.RemoveAllListeners();
			this.m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_BUTTON_RED.PointerClick.AddListener(new UnityAction(cNKCWarfareGame.OnClickNextTurn));
			this.m_NUF_WARFARE_SELECTED_DETAIL.PointerClick.RemoveAllListeners();
			this.m_NUF_WARFARE_SELECTED_DETAIL.PointerClick.AddListener(new UnityAction(cNKCWarfareGame.OnClickSquadInfo));
			this.m_NUF_WARFARE_SELECTED_SUPPLY.PointerClick.RemoveAllListeners();
			this.m_NUF_WARFARE_SELECTED_SUPPLY.PointerClick.AddListener(new UnityAction(cNKCWarfareGame.UseSupplyItem));
			this.m_NUF_WARFARE_SELECTED_REPAIR.PointerClick.RemoveAllListeners();
			this.m_NUF_WARFARE_SELECTED_REPAIR.PointerClick.AddListener(new UnityAction(cNKCWarfareGame.UseRepairItem));
			this.m_NUF_WARFARE_INFO_MEDALINFO_toggle.OnValueChanged.RemoveAllListeners();
			this.m_NUF_WARFARE_INFO_MEDALINFO_toggle.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickMedalInfo));
			this.m_NUF_WARFARE_SUB_MENU_OPERATION_RECOVERY_btn.PointerClick.RemoveAllListeners();
			this.m_NUF_WARFARE_SUB_MENU_OPERATION_RECOVERY_btn.PointerClick.AddListener(new UnityAction(cNKCWarfareGame.OnTouchRecoveryBtn));
			this.SetActiveRecovery(false);
			this.m_tgReadyMultiply.OnValueChanged.RemoveAllListeners();
			this.m_tgReadyMultiply.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickMultiply));
			this.InitAniEvent(this.m_NKM_UI_POPUP_WARFARE_PHASE_PLAYER, new UnityAction(this.OnCompleteUserPhaseAni));
			this.InitAniEvent(this.m_NKM_UI_POPUP_WARFARE_PHASE_ENEMY, new UnityAction(this.OnCompleteEnemyPhaseAni));
			this.InitAniEvent(this.m_NKM_UI_POPUP_WARFARE_PHASE_COMPLETE, new UnityAction(this.OnCompleteCompleteOrFailPhaseAni));
			this.InitAniEvent(this.m_NKM_UI_POPUP_WARFARE_PHASE_FAIL, new UnityAction(this.OnCompleteCompleteOrFailPhaseAni));
			this.dOnStartUserPhase = onStartUserPhase;
			this.dOnStartEnemyPhase = onStartEnemyPhase;
			this.m_CurrMultiplyRewardCount = 1;
			this.m_NKCUIOperationMultiply.Init(new NKCUIOperationMultiply.OnCountUpdated(this.OnOperationMultiplyUpdated), new UnityAction(this.OnClickMultiplyRewardClose));
			NKCUtil.SetGameobjectActive(this.m_NKCUIOperationMultiply, false);
			NKCUtil.SetGameobjectActive(this.m_objReadyMultiply, false);
			NKCUtil.SetGameobjectActive(this.m_objPlayingMultiply, false);
		}

		// Token: 0x06004F5F RID: 20319 RVA: 0x0017FCE5 File Offset: 0x0017DEE5
		public void TurnOffOperationMultiplyUI()
		{
			NKCUtil.SetGameobjectActive(this.m_NKCUIOperationMultiply, false);
		}

		// Token: 0x06004F60 RID: 20320 RVA: 0x0017FCF4 File Offset: 0x0017DEF4
		private void OnClickOperationRepeat()
		{
			if (!NKCRepeatOperaion.CheckPossibleForWarfare(NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareStrID(), true))
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetMyUserData() == null)
			{
				return;
			}
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			if (warfareGameData == null)
			{
				return;
			}
			if (warfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_STOP)
			{
				if (!this.IsCanStartEterniumStage(true))
				{
					return;
				}
				this.m_tgReadyMultiply.Select(false, false, false);
				if (this.m_NKCWarfareGame.GetNKCWarfareGameUnitMgr().GetCurrentUserUnit(true) == 0)
				{
					NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_WARFARE_CANNOT_START_BECAUSE_NO_USER_UNIT, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
				NKCPopupRepeatOperation.Instance.Open(null);
				return;
			}
			else
			{
				if (warfareGameData.rewardMultiply > 1)
				{
					NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString("SI_DP_DOUBLE_OPERATION_CANNOT_REPEAT", false), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
				if (NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetRetryData() == null)
				{
					NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_WARFARE_CANNOT_FIND_RETRY_DATA, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
				if (!this.m_NKCWarfareGame.CheckEnablePause())
				{
					NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_WARFARE_CANNOT_PAUSE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
				this.m_NKCWarfareGame.SetPause(true);
				NKCPopupRepeatOperation.Instance.Open(delegate
				{
					if (this.m_NKCWarfareGame != null)
					{
						this.m_NKCWarfareGame.SetPause(false);
					}
				});
				return;
			}
		}

		// Token: 0x06004F61 RID: 20321 RVA: 0x0017FE14 File Offset: 0x0017E014
		private bool IsCanStartEterniumStage(bool bCallLackPopup)
		{
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			if (warfareGameData != null && warfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_STOP)
			{
				NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(warfareGameData.warfareTempletID);
				if (nkmwarfareTemplet != null && nkmwarfareTemplet.StageTemplet != null)
				{
					return NKCUtil.IsCanStartEterniumStage(nkmwarfareTemplet.StageTemplet, bCallLackPopup);
				}
			}
			return true;
		}

		// Token: 0x06004F62 RID: 20322 RVA: 0x0017FE5C File Offset: 0x0017E05C
		private void InitAniEvent(GameObject obj, UnityAction callback)
		{
			NKCUIComAniEventHandler component = obj.GetComponent<NKCUIComAniEventHandler>();
			if (component != null)
			{
				component.m_NKCUIComAniEvent.RemoveAllListeners();
				component.m_NKCUIComAniEvent.AddListener(callback);
			}
			EventTrigger component2 = obj.GetComponent<EventTrigger>();
			if (component2 != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(delegate(BaseEventData e)
				{
					UnityAction callback2 = callback;
					if (callback2 == null)
					{
						return;
					}
					callback2();
				});
				component2.triggers.Add(entry);
			}
		}

		// Token: 0x06004F63 RID: 20323 RVA: 0x0017FEE2 File Offset: 0x0017E0E2
		public bool IsOpenSelectedSquadUI()
		{
			return this.m_NUF_WARFARE_SELECTED_SQUAD.activeSelf;
		}

		// Token: 0x06004F64 RID: 20324 RVA: 0x0017FEEF File Offset: 0x0017E0EF
		public NKMDeckIndex GetNKMDeckIndexSelected()
		{
			return this.m_NKMDeckIndexSelected;
		}

		// Token: 0x06004F65 RID: 20325 RVA: 0x0017FEF7 File Offset: 0x0017E0F7
		public void OpenSelectedSquadUI(NKMDeckIndex sNKMDeckIndex, bool bRepair = false, bool bResupply = false)
		{
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_SELECTED_SQUAD, true);
			this.m_NKMDeckIndexSelected = sNKMDeckIndex;
			this.UpdateSelectedSquadUI(bRepair, bResupply);
			NKCOperatorUtil.PlayVoice(this.m_NKMDeckIndexSelected, VOICE_TYPE.VT_SHIP_SELECT, true);
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_OPERATION_TITLE, false);
		}

		// Token: 0x06004F66 RID: 20326 RVA: 0x0017FF30 File Offset: 0x0017E130
		public void UpdateSelectedSquadUI(bool bRepair = false, bool bResupply = false)
		{
			if (!this.IsOpenSelectedSquadUI())
			{
				return;
			}
			this.m_NUF_WARFARE_SELECTED_SQUAD_Text1.text = string.Format(NKCUtilString.GET_STRING_SQUAD_ONE_PARAM, NKCUtilString.GetDeckNumberString(this.m_NKMDeckIndexSelected));
			int num = (int)(this.m_NKMDeckIndexSelected.m_iIndex + 1);
			this.m_NUF_WARFARE_SELECTED_SQUAD_Text2.text = string.Format(NKCUtilString.GET_STRING_SQUAD_TWO_PARAM, num, NKCUtilString.GetRankNumber(num, false).ToUpper());
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_SELECTED_REPAIR, bRepair);
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_SELECTED_SUPPLY, bResupply);
		}

		// Token: 0x06004F67 RID: 20327 RVA: 0x0017FFB3 File Offset: 0x0017E1B3
		public void CloseSelectedSquadUI()
		{
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_SELECTED_SQUAD, false);
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_OPERATION_TITLE, true);
		}

		// Token: 0x06004F68 RID: 20328 RVA: 0x0017FFD0 File Offset: 0x0017E1D0
		public void UpdateMedalInfo()
		{
			if (!this.m_NUF_WARFARE_INFO_MEDALINFO_DETAIL.activeSelf)
			{
				return;
			}
			this.m_MEDALINFO_DETAIL_SLOT1_TEXT.text = "";
			this.m_MEDALINFO_DETAIL_SLOT2_TEXT.text = "";
			this.m_MEDALINFO_DETAIL_SLOT3_TEXT.text = NKCUtilString.GetWFMissionText(WARFARE_GAME_MISSION_TYPE.WFMT_CLEAR, 0);
			if (NKCScenManager.GetScenManager().GetMyUserData() == null)
			{
				return;
			}
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			if (warfareGameData == null)
			{
				return;
			}
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(warfareGameData.warfareTempletID);
			if (nkmwarfareTemplet == null)
			{
				return;
			}
			this.m_MEDALINFO_DETAIL_SLOT2_TEXT.text = NKCUtilString.GetWFMissionText(nkmwarfareTemplet.m_WFMissionType_1, nkmwarfareTemplet.m_WFMissionValue_1);
			this.m_MEDALINFO_DETAIL_SLOT1_TEXT.text = NKCUtilString.GetWFMissionText(nkmwarfareTemplet.m_WFMissionType_2, nkmwarfareTemplet.m_WFMissionValue_2);
			if (warfareGameData.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_STOP)
			{
				Text medalinfo_DETAIL_SLOT2_TEXT = this.m_MEDALINFO_DETAIL_SLOT2_TEXT;
				medalinfo_DETAIL_SLOT2_TEXT.text += this.GetCurrentStateOfWFMission(nkmwarfareTemplet.m_WFMissionType_1, nkmwarfareTemplet.m_WFMissionValue_1);
				Text medalinfo_DETAIL_SLOT1_TEXT = this.m_MEDALINFO_DETAIL_SLOT1_TEXT;
				medalinfo_DETAIL_SLOT1_TEXT.text += this.GetCurrentStateOfWFMission(nkmwarfareTemplet.m_WFMissionType_2, nkmwarfareTemplet.m_WFMissionValue_2);
				NKCUtil.SetGameobjectActive(this.m_MEDALINFO_DETAIL_SLOT2_line, this.IsFailWFMission(nkmwarfareTemplet.m_WFMissionType_1, nkmwarfareTemplet.m_WFMissionValue_1));
				NKCUtil.SetGameobjectActive(this.m_MEDALINFO_DETAIL_SLOT1_line, this.IsFailWFMission(nkmwarfareTemplet.m_WFMissionType_2, nkmwarfareTemplet.m_WFMissionValue_2));
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_MEDALINFO_DETAIL_SLOT2_line, false);
			NKCUtil.SetGameobjectActive(this.m_MEDALINFO_DETAIL_SLOT1_line, false);
		}

		// Token: 0x06004F69 RID: 20329 RVA: 0x00180130 File Offset: 0x0017E330
		private string GetCurrentStateOfWFMission(WARFARE_GAME_MISSION_TYPE missionType, int value)
		{
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			if (warfareGameData == null)
			{
				return "";
			}
			switch (missionType)
			{
			case WARFARE_GAME_MISSION_TYPE.WFMT_PHASE:
				return NKCUtilString.GetCurrentProgress(warfareGameData.turnCount, value);
			case WARFARE_GAME_MISSION_TYPE.WFMT_KILL:
				return NKCUtilString.GetCurrentProgress((int)warfareGameData.enemiesKillCount, value);
			case WARFARE_GAME_MISSION_TYPE.WFMT_FIRST_ATTACK:
				return NKCUtilString.GetCurrentProgress(warfareGameData.firstAttackCount, value);
			case WARFARE_GAME_MISSION_TYPE.WFMT_ASSIST:
				return NKCUtilString.GetCurrentProgress((int)warfareGameData.assistCount, value);
			case WARFARE_GAME_MISSION_TYPE.WFMT_CONTAINER:
				return NKCUtilString.GetCurrentProgress((int)warfareGameData.containerCount, value);
			}
			return "";
		}

		// Token: 0x06004F6A RID: 20330 RVA: 0x001801BC File Offset: 0x0017E3BC
		private bool IsFailWFMission(WARFARE_GAME_MISSION_TYPE missionType, int value)
		{
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			if (warfareGameData == null)
			{
				return false;
			}
			switch (missionType)
			{
			case WARFARE_GAME_MISSION_TYPE.WFMT_ALLKILL:
				return warfareGameData.alliesKillCount > 0;
			case WARFARE_GAME_MISSION_TYPE.WFMT_PHASE:
				return warfareGameData.turnCount > value || warfareGameData.alliesKillCount > 0;
			case WARFARE_GAME_MISSION_TYPE.WFMT_NO_SHIPWRECK:
				return warfareGameData.alliesKillCount > 0;
			case WARFARE_GAME_MISSION_TYPE.WFMT_NOSUPPLY_WIN:
			case WARFARE_GAME_MISSION_TYPE.WFMT_NOSUPPLY_ALLKILL:
				return warfareGameData.supplyUseCount > 0;
			}
			return false;
		}

		// Token: 0x06004F6B RID: 20331 RVA: 0x0018023C File Offset: 0x0017E43C
		public void UpdateWinCondition()
		{
			if (!this.m_NUF_WARFARE_INFO_MEDALINFO_DETAIL.activeSelf)
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetMyUserData() == null)
			{
				return;
			}
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			if (warfareGameData == null)
			{
				return;
			}
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(warfareGameData.warfareTempletID);
			if (nkmwarfareTemplet == null)
			{
				return;
			}
			this.m_NUF_WARFARE_INFO_VICTORY_1_TEXT.text = NKCUtilString.GetWFWinContionText(nkmwarfareTemplet.m_WFWinCondition);
			this.m_NUF_WARFARE_INFO_DEFEAT_TEXT.text = NKCUtilString.GetWFLoseConditionText(nkmwarfareTemplet.m_WFLoseCondition);
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_INFO_VICTORY_1_ICON_TARGET, nkmwarfareTemplet.m_WFWinCondition == WARFARE_GAME_CONDITION.WFC_KILL_TARGET);
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_INFO_VICTORY_1_ICON_WC_ENTER, nkmwarfareTemplet.m_WFWinCondition == WARFARE_GAME_CONDITION.WFC_TILE_ENTER);
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_INFO_VICTORY_1_ICON_WC_HOLD, nkmwarfareTemplet.m_WFWinCondition == WARFARE_GAME_CONDITION.WFC_PHASE_TILE_HOLD);
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_INFO_VICTORY_1_ICON_WC_DEFENSE, nkmwarfareTemplet.m_WFLoseCondition == WARFARE_GAME_CONDITION.WFC_TILE_ENTER);
			bool bPlay = warfareGameData.warfareGameState > NKM_WARFARE_GAME_STATE.NWGS_STOP;
			Text nuf_WARFARE_INFO_VICTORY_1_TEXT = this.m_NUF_WARFARE_INFO_VICTORY_1_TEXT;
			nuf_WARFARE_INFO_VICTORY_1_TEXT.text += this.GetCurrentWinCondition(bPlay, nkmwarfareTemplet.m_WFWinCondition, nkmwarfareTemplet.m_WFWinValue);
			Text nuf_WARFARE_INFO_DEFEAT_TEXT = this.m_NUF_WARFARE_INFO_DEFEAT_TEXT;
			nuf_WARFARE_INFO_DEFEAT_TEXT.text += this.GetCurrentLoseCondition(bPlay, nkmwarfareTemplet.m_WFLoseCondition, nkmwarfareTemplet.m_WFLoseValue);
			bool activeTurnFinishWarningBtn = false;
			if (nkmwarfareTemplet.m_WFLoseCondition == WARFARE_GAME_CONDITION.WFC_PHASE && warfareGameData.turnCount >= nkmwarfareTemplet.m_WFLoseValue)
			{
				activeTurnFinishWarningBtn = true;
			}
			this.SetActiveTurnFinishWarningBtn(activeTurnFinishWarningBtn);
		}

		// Token: 0x06004F6C RID: 20332 RVA: 0x00180380 File Offset: 0x0017E580
		private string GetCurrentWinCondition(bool bPlay, WARFARE_GAME_CONDITION winCondition, int value)
		{
			if (!bPlay)
			{
				return "";
			}
			if (NKCScenManager.GetScenManager().GetMyUserData() == null)
			{
				return "";
			}
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			if (warfareGameData == null)
			{
				return "";
			}
			if (winCondition == WARFARE_GAME_CONDITION.WFC_KILL_TARGET)
			{
				return NKCUtilString.GetCurrentProgress((int)warfareGameData.targetKillCount, value);
			}
			if (winCondition == WARFARE_GAME_CONDITION.WFC_PHASE_TILE_HOLD)
			{
				int holdCount = warfareGameData.holdCount;
				return NKCUtilString.GetCurrentProgress(value - holdCount, value);
			}
			return "";
		}

		// Token: 0x06004F6D RID: 20333 RVA: 0x001803E8 File Offset: 0x0017E5E8
		private string GetCurrentLoseCondition(bool bPlay, WARFARE_GAME_CONDITION loseCondition, int value)
		{
			if (NKCScenManager.GetScenManager().GetMyUserData() == null)
			{
				return "";
			}
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			if (warfareGameData == null)
			{
				return "";
			}
			if (loseCondition == WARFARE_GAME_CONDITION.WFC_PHASE)
			{
				if (bPlay)
				{
					return NKCUtilString.GetCurrentProgress(warfareGameData.turnCount, value);
				}
				return NKCUtilString.GetCurrentProgress(0, value);
			}
			else
			{
				if (bPlay && loseCondition == WARFARE_GAME_CONDITION.WFC_KILL_COUNT)
				{
					return NKCUtilString.GetCurrentProgress((int)warfareGameData.alliesKillCount, value);
				}
				return "";
			}
		}

		// Token: 0x06004F6E RID: 20334 RVA: 0x00180450 File Offset: 0x0017E650
		public void OnClickMedalInfo(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_INFO_MEDALINFO_DETAIL, bSet);
			if (bSet)
			{
				this.m_NUF_WARFARE_INFO_MEDALINFO_DETAIL_DTA.DORestart();
				this.UpdateMedalInfo();
			}
		}

		// Token: 0x06004F6F RID: 20335 RVA: 0x00180472 File Offset: 0x0017E672
		public void OpenWarfareInfoPopup()
		{
			NKCPopupWarfareInfo.Instance.Open();
		}

		// Token: 0x06004F70 RID: 20336 RVA: 0x0018047E File Offset: 0x0017E67E
		public void SetUpperRightMenuPosition(bool bPlaying)
		{
			if (this.m_rtUpperRightMenuRoot != null)
			{
				this.m_rtUpperRightMenuRoot.anchoredPosition = new Vector2(this.m_rtUpperRightMenuRoot.anchoredPosition.x, bPlaying ? 0f : -85.88f);
			}
		}

		// Token: 0x06004F71 RID: 20337 RVA: 0x001804C0 File Offset: 0x0017E6C0
		public void SetPauseState(bool bSet)
		{
			if (this.m_NKM_UI_POPUP_WARFARE_PHASE_PLAYER != null)
			{
				this.m_NKM_UI_POPUP_WARFARE_PHASE_PLAYER.GetComponent<Animator>().enabled = !bSet;
			}
			if (this.m_NKM_UI_POPUP_WARFARE_PHASE_ENEMY != null)
			{
				this.m_NKM_UI_POPUP_WARFARE_PHASE_ENEMY.GetComponent<Animator>().enabled = !bSet;
			}
			if (this.m_NKM_UI_POPUP_WARFARE_PHASE_COMPLETE != null)
			{
				this.m_NKM_UI_POPUP_WARFARE_PHASE_COMPLETE.GetComponent<Animator>().enabled = !bSet;
			}
			if (this.m_NKM_UI_POPUP_WARFARE_PHASE_FAIL != null)
			{
				this.m_NKM_UI_POPUP_WARFARE_PHASE_FAIL.GetComponent<Animator>().enabled = !bSet;
			}
		}

		// Token: 0x06004F72 RID: 20338 RVA: 0x00180555 File Offset: 0x0017E755
		public void SetTurnCount(int count)
		{
			this.m_NUF_WARFARE_PHASE_NUM_TEXT.text = string.Format("{0:00}", count);
		}

		// Token: 0x06004F73 RID: 20339 RVA: 0x00180572 File Offset: 0x0017E772
		public void SetAttackCost(int itemID, int itemCount)
		{
			this.m_AttackCostItemCount = itemCount;
			if (this.m_ResourceButton != null)
			{
				this.m_ResourceButton.SetData(itemID, this.m_CurrMultiplyRewardCount * this.m_AttackCostItemCount);
			}
		}

		// Token: 0x06004F74 RID: 20340 RVA: 0x001805A2 File Offset: 0x0017E7A2
		private void UpdateAttckCostUI()
		{
			if (this.m_ResourceButton != null && this.m_ResourceButton.GetItemID() != 0)
			{
				this.m_ResourceButton.SetData(this.m_ResourceButton.GetItemID(), this.m_CurrMultiplyRewardCount * this.m_AttackCostItemCount);
			}
		}

		// Token: 0x06004F75 RID: 20341 RVA: 0x001805E2 File Offset: 0x0017E7E2
		public void SetPhaseUserType(bool bUser)
		{
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_PHASE_USER, bUser);
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_PHASE_ENEMY, !bUser);
		}

		// Token: 0x06004F76 RID: 20342 RVA: 0x001805FF File Offset: 0x0017E7FF
		public void DeActivateAllTriggerUI()
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_WARFARE_PHASE_ENEMY, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_WARFARE_PHASE_PLAYER, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_WARFARE_PHASE_COMPLETE, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_WARFARE_PHASE_FAIL, false);
		}

		// Token: 0x06004F77 RID: 20343 RVA: 0x00180631 File Offset: 0x0017E831
		public void TriggerPlayerTurnUI()
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_WARFARE_PHASE_ENEMY, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_WARFARE_PHASE_PLAYER, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_WARFARE_PHASE_PLAYER, true);
		}

		// Token: 0x06004F78 RID: 20344 RVA: 0x00180657 File Offset: 0x0017E857
		public void TriggerEnemyTurnUI()
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_WARFARE_PHASE_PLAYER, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_WARFARE_PHASE_ENEMY, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_WARFARE_PHASE_ENEMY, true);
		}

		// Token: 0x06004F79 RID: 20345 RVA: 0x0018067D File Offset: 0x0017E87D
		public void TriggerCompleteUI()
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_WARFARE_PHASE_COMPLETE, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_WARFARE_PHASE_COMPLETE, true);
		}

		// Token: 0x06004F7A RID: 20346 RVA: 0x00180697 File Offset: 0x0017E897
		public void TriggerFailUI()
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_WARFARE_PHASE_FAIL, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_WARFARE_PHASE_FAIL, true);
		}

		// Token: 0x06004F7B RID: 20347 RVA: 0x001806B1 File Offset: 0x0017E8B1
		public bool CheckVisibleWarfareStateEffectUI()
		{
			return this.m_NKM_UI_POPUP_WARFARE_PHASE_ENEMY.activeSelf || this.m_NKM_UI_POPUP_WARFARE_PHASE_PLAYER.activeSelf || this.m_NKM_UI_POPUP_WARFARE_PHASE_COMPLETE.activeSelf || this.m_NKM_UI_POPUP_WARFARE_PHASE_FAIL.activeSelf;
		}

		// Token: 0x06004F7C RID: 20348 RVA: 0x001806EA File Offset: 0x0017E8EA
		public void SetActiveTurnFinishBtn(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT, bSet);
		}

		// Token: 0x06004F7D RID: 20349 RVA: 0x001806F8 File Offset: 0x0017E8F8
		private void SetActiveTurnFinishWarningBtn(bool bWarning)
		{
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_TURN, !bWarning);
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_TURN_RED, bWarning);
		}

		// Token: 0x06004F7E RID: 20350 RVA: 0x00180715 File Offset: 0x0017E915
		public void SetRemainTurnOnUnitCount(int count)
		{
			this.SetRemainTurnOnUnitCountNormal(count);
			this.SetRemainTurnOnUnitCountRed(count);
		}

		// Token: 0x06004F7F RID: 20351 RVA: 0x00180728 File Offset: 0x0017E928
		private void SetRemainTurnOnUnitCountNormal(int count)
		{
			if (this.m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_TURN_COUNT != null)
			{
				this.m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_TURN_COUNT.text = count.ToString();
			}
			if (this.m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_TEXT != null)
			{
				if (count <= 0)
				{
					this.m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_TEXT.text = NKCUtilString.GET_STRING_WARFARE_PHASE_FINISH;
					if (this.m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT.activeSelf)
					{
						if (NKCScenManager.GetScenManager().WarfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_PLAYING)
						{
							this.m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_FX.Play("BASE_READY");
							return;
						}
						this.m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_FX.Play("BASE");
						return;
					}
				}
				else
				{
					this.m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_TEXT.text = NKCUtilString.GET_STRING_WARFARE_PHASE_FINISH;
					if (this.m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT.activeSelf)
					{
						this.m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_FX.Play("BASE");
					}
				}
			}
		}

		// Token: 0x06004F80 RID: 20352 RVA: 0x001807E8 File Offset: 0x0017E9E8
		public void SetRemainTurnOnUnitCountRed(int count)
		{
			if (!this.m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_TURN_RED.activeSelf)
			{
				return;
			}
			if (this.m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_TURN_COUNT_RED != null)
			{
				this.m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_TURN_COUNT_RED.text = count.ToString();
			}
			if (this.m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_TEXT_RED != null)
			{
				if (count <= 0)
				{
					this.m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_TEXT_RED.text = NKCUtilString.GET_STRING_WARFARE_PHASE_FINISH;
					if (this.m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT.activeSelf)
					{
						if (NKCScenManager.GetScenManager().WarfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_PLAYING)
						{
							this.m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_FX_RED.Play("BASE_READY");
							return;
						}
						this.m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_FX_RED.Play("BASE");
						return;
					}
				}
				else
				{
					this.m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_TEXT_RED.text = NKCUtilString.GET_STRING_WARFARE_PHASE_FINISH;
					if (this.m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT.activeSelf)
					{
						this.m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_FX_RED.Play("BASE");
					}
				}
			}
		}

		// Token: 0x06004F81 RID: 20353 RVA: 0x001808B6 File Offset: 0x0017EAB6
		public void SetActiveBatchCountUI(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_SHIP_NUM, bSet);
			this.SetActiveSupportBatchCountUI(bSet);
		}

		// Token: 0x06004F82 RID: 20354 RVA: 0x001808CC File Offset: 0x0017EACC
		private void SetActiveSupportBatchCountUI(bool bSet)
		{
			bool bValue = false;
			if (bSet)
			{
				bValue = this.IsCanSpawnSupportShip();
			}
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_SHIP_NUM_SUPPORT, bValue);
		}

		// Token: 0x06004F83 RID: 20355 RVA: 0x001808F1 File Offset: 0x0017EAF1
		public void SetActiveTitle(bool bReadyTitle)
		{
			NKCUtil.SetGameobjectActive(this.m_ReadyTitle, bReadyTitle);
			NKCUtil.SetGameobjectActive(this.m_PlayTitle, !bReadyTitle);
		}

		// Token: 0x06004F84 RID: 20356 RVA: 0x0018090E File Offset: 0x0017EB0E
		public void SetActiveBatchGuideText(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_SUB_MENU_OPERATION_TEXT, bSet);
		}

		// Token: 0x06004F85 RID: 20357 RVA: 0x0018091C File Offset: 0x0017EB1C
		public void SetActiveBatchSupportGuideText(bool bSet)
		{
			bool bValue = false;
			if (bSet)
			{
				bValue = this.IsCanSpawnSupportShip();
			}
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_SUB_MENU_OPERATION_SUPPORT_TEXT, bValue);
		}

		// Token: 0x06004F86 RID: 20358 RVA: 0x00180944 File Offset: 0x0017EB44
		private bool IsCanSpawnSupportShip()
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
			NKMWarfareMapTemplet mapTemplet = nkmwarfareTemplet.MapTemplet;
			if (mapTemplet == null)
			{
				return false;
			}
			int spawnPointCountByType = mapTemplet.GetSpawnPointCountByType(NKM_WARFARE_SPAWN_POINT_TYPE.NWSPT_DIVE);
			int spawnPointCountByType2 = mapTemplet.GetSpawnPointCountByType(NKM_WARFARE_SPAWN_POINT_TYPE.NWSPT_ASSAULT);
			return nkmwarfareTemplet.m_bFriendSummon && spawnPointCountByType + spawnPointCountByType2 > 1;
		}

		// Token: 0x06004F87 RID: 20359 RVA: 0x001809A0 File Offset: 0x0017EBA0
		public void SetActiveOperationBtn(bool bSet)
		{
			if (NKCScenManager.CurrentUserData() == null)
			{
				return;
			}
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			if (warfareGameData == null)
			{
				return;
			}
			if (NKMWarfareTemplet.Find(warfareGameData.warfareTempletID) == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_SUB_MENU_OPERATION_BUTTON, bSet);
		}

		// Token: 0x06004F88 RID: 20360 RVA: 0x001809DE File Offset: 0x0017EBDE
		public void SetActiveDeco(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_DECO, bSet);
		}

		// Token: 0x06004F89 RID: 20361 RVA: 0x001809EC File Offset: 0x0017EBEC
		public void SetActiveAuto(bool bVisible, bool bUsable)
		{
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_AUTO, bVisible);
			if (bVisible)
			{
				string text = "#FFFFFF";
				string text2 = "#7B7B7B";
				this.m_NUF_WARFARE_AUTO_OFF_Txt.color = NKCUtil.GetColor(bUsable ? text : text2);
				this.m_NUF_WARFARE_AUTO_OFF_Img.color = NKCUtil.GetColor(bUsable ? text : text2);
			}
		}

		// Token: 0x06004F8A RID: 20362 RVA: 0x00180A44 File Offset: 0x0017EC44
		public void SetActiveAutoOnOff(bool bOn, bool bAutoSupply)
		{
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_AUTO_ON, bOn);
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_AUTO_OFF, !bOn);
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_AUTO_SUPPLY, bOn);
			if (bOn)
			{
				NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_AUTO_SUPPLY_ON_btn, bAutoSupply);
				NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_AUTO_SUPPLY_OFF_btn, !bAutoSupply);
			}
		}

		// Token: 0x06004F8B RID: 20363 RVA: 0x00180A96 File Offset: 0x0017EC96
		public void SetActiveRepeatOperation(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_REPEAT, bSet);
		}

		// Token: 0x06004F8C RID: 20364 RVA: 0x00180AA4 File Offset: 0x0017ECA4
		public void SetActiveRepeatOperationOnOff(bool bOn)
		{
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_REPEAT_ON_btn, bOn);
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_REPEAT_OFF_Btn, !bOn);
		}

		// Token: 0x06004F8D RID: 20365 RVA: 0x00180AC4 File Offset: 0x0017ECC4
		public void SendAutoReq(bool bAuto)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			this.NKMPacket_WARFARE_GAME_AUTO_REQ(bAuto, myUserData.m_UserOption.m_bAutoWarfareRepair);
		}

		// Token: 0x06004F8E RID: 20366 RVA: 0x00180AF0 File Offset: 0x0017ECF0
		public void SendAutoSupplyReq(bool bAutoSupply)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (!myUserData.m_UserOption.m_bAutoWarfare)
			{
				Debug.LogError("warfare - auto가 아닌데 autoSupply가 들어옴");
				return;
			}
			this.NKMPacket_WARFARE_GAME_AUTO_REQ(myUserData.m_UserOption.m_bAutoWarfare, bAutoSupply);
		}

		// Token: 0x06004F8F RID: 20367 RVA: 0x00180B34 File Offset: 0x0017ED34
		public void NKMPacket_WARFARE_GAME_AUTO_REQ(bool bAuto, bool bAutoSupply)
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.WARFARE_AUTO_MOVE, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.WARFARE_AUTO_MOVE, 0);
				return;
			}
			if (NKCGameEventManager.IsWaiting())
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (NKCWarfareManager.CheckPossibleAuto(myUserData, bAuto, bAutoSupply) != NKM_ERROR_CODE.NEC_OK)
			{
				return;
			}
			if (NKMPopUpBox.IsOpenedWaitBox())
			{
				return;
			}
			Debug.Log("NKMPacket_WARFARE_GAME_AUTO_REQ - NKCWarfareGameHUD");
			this.m_NKCWarfareGame.SetPause(true);
			this.m_NKCWarfareGame.WaitAutoPacket = true;
			NKCPacketSender.Send_NKMPacket_WARFARE_GAME_AUTO_REQ(bAuto, bAutoSupply);
		}

		// Token: 0x06004F90 RID: 20368 RVA: 0x00180BAF File Offset: 0x0017EDAF
		public void SetActivePhase(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_PHASE, bSet);
		}

		// Token: 0x06004F91 RID: 20369 RVA: 0x00180BC0 File Offset: 0x0017EDC0
		public void SetBatchedShipCount(int count)
		{
			int num = -1;
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (nkmwarfareTemplet != null)
			{
				num = nkmwarfareTemplet.m_UserTeamCount;
			}
			this.m_iCurrentBatchedShipCnt = count;
			if (this.m_iCurrentBatchedShipCnt <= 0 && this.m_tgReadyMultiply.m_bSelect)
			{
				this.m_tgReadyMultiply.Select(false, false, false);
			}
			this.SetActiveOperationBtn(this.m_iCurrentBatchedShipCnt > 0);
			this.m_NUF_WARFARE_SHIP_NUM_TEXT.text = string.Format("{0}/{1}", this.m_iCurrentBatchedShipCnt, num);
			if (this.m_iCurrentBatchedShipCnt >= num)
			{
				if (this.m_NUF_WARFARE_SUB_MENU_OPERATION_BUTTON.activeSelf)
				{
					NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_SUB_MENU_OPERATION_BUTTON_FX.gameObject, true);
					this.m_NUF_WARFARE_SUB_MENU_OPERATION_BUTTON_FX.Play("BASE_READY");
				}
			}
			else
			{
				this.m_NUF_WARFARE_SHIP_NUM_TEXT.color = new Color(1f, 1f, 1f);
				if (this.m_NUF_WARFARE_SUB_MENU_OPERATION_BUTTON_FX.gameObject.activeInHierarchy)
				{
					NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_SUB_MENU_OPERATION_BUTTON_FX.gameObject, false);
				}
			}
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_SUB_MENU_OPERATION_TEXT, count > 0 && count < num);
			NKCUtil.SetLabelTextColor(this.m_EnterLimit_TEXT, Color.white);
			if (count > 0)
			{
				bool bValue = false;
				if (nkmwarfareTemplet != null && nkmwarfareTemplet.StageTemplet != null)
				{
					bValue = (nkmwarfareTemplet.StageTemplet.EnterLimit > 0);
				}
				this.UpdateStagePlayState();
				NKCUtil.SetGameobjectActive(this.m_OPERATION_EnterLimit, bValue);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_OPERATION_EnterLimit, false);
		}

		// Token: 0x06004F92 RID: 20370 RVA: 0x00180D28 File Offset: 0x0017EF28
		public void UpdateSupportShipTile(List<int> userUnitTileIndex)
		{
			if (this.IsCanSpawnSupportShip() && userUnitTileIndex.Count > 0 && !this.m_bBatchedSupportShip)
			{
				WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
				if (warfareGameData == null)
				{
					return;
				}
				NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(warfareGameData.warfareTempletID);
				if (nkmwarfareTemplet == null)
				{
					return;
				}
				NKMWarfareMapTemplet mapTemplet = nkmwarfareTemplet.MapTemplet;
				if (mapTemplet == null)
				{
					return;
				}
				int divePointTileCount = mapTemplet.GetDivePointTileCount();
				bool activeBatchSupportGuideText = false;
				for (int i = 0; i < divePointTileCount; i++)
				{
					int divePointTileIndex = mapTemplet.GetDivePointTileIndex(i);
					if (divePointTileIndex >= 0 && !userUnitTileIndex.Contains(divePointTileIndex))
					{
						activeBatchSupportGuideText = true;
						break;
					}
				}
				this.SetActiveBatchSupportGuideText(activeBatchSupportGuideText);
			}
		}

		// Token: 0x06004F93 RID: 20371 RVA: 0x00180DB8 File Offset: 0x0017EFB8
		public void SetBatchedSupportShipCount(bool bSet)
		{
			if (bSet)
			{
				NKCUtil.SetLabelText(this.m_NUF_WARFARE_SHIP_NUM_SUPPORT_TEXT, "1/1");
			}
			else
			{
				NKCUtil.SetLabelText(this.m_NUF_WARFARE_SHIP_NUM_SUPPORT_TEXT, "0/1");
			}
			this.m_bBatchedSupportShip = bSet;
			this.SetActiveBatchSupportGuideText(!bSet);
		}

		// Token: 0x06004F94 RID: 20372 RVA: 0x00180DF0 File Offset: 0x0017EFF0
		private void ConfirmResetStagePlayCnt()
		{
			NKMWarfareTemplet cNKMWarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (cNKMWarfareTemplet != null && cNKMWarfareTemplet.StageTemplet != null)
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				int num = 0;
				if (nkmuserData != null)
				{
					num = nkmuserData.GetStageRestoreCnt(cNKMWarfareTemplet.StageTemplet.Key);
				}
				if (!cNKMWarfareTemplet.StageTemplet.Restorable)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_ENTER_LIMIT_OVER, null, "");
					return;
				}
				if (num >= cNKMWarfareTemplet.StageTemplet.RestoreLimit)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_WARFARE_GAEM_HUD_RESTORE_LIMIT_OVER_DESC, null, "");
					return;
				}
				NKCPopupResourceWithdraw.Instance.OpenForRestoreEnterLimit(cNKMWarfareTemplet.StageTemplet, delegate
				{
					NKCPacketSender.Send_NKMPacket_RESET_STAGE_PLAY_COUNT_REQ(cNKMWarfareTemplet.StageTemplet.Key);
				}, num);
			}
		}

		// Token: 0x06004F95 RID: 20373 RVA: 0x00180EC4 File Offset: 0x0017F0C4
		public void SetActivePause(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_PAUSE, bSet);
		}

		// Token: 0x06004F96 RID: 20374 RVA: 0x00180ED4 File Offset: 0x0017F0D4
		public void OnCompleteCompleteOrFailPhaseAni()
		{
			this.DeActivateAllTriggerUI();
			if (NKCScenManager.GetScenManager().GetMyUserData() == null)
			{
				return;
			}
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			if (warfareGameData == null)
			{
				return;
			}
			if (warfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_STOP)
			{
				return;
			}
			this.SendGetNextOrderREQ();
		}

		// Token: 0x06004F97 RID: 20375 RVA: 0x00180F12 File Offset: 0x0017F112
		public void OnCompleteUserPhaseAni()
		{
			this.DeActivateAllTriggerUI();
			NKCWarfareGameHUD.OnStartUserPhase onStartUserPhase = this.dOnStartUserPhase;
			if (onStartUserPhase == null)
			{
				return;
			}
			onStartUserPhase();
		}

		// Token: 0x06004F98 RID: 20376 RVA: 0x00180F2A File Offset: 0x0017F12A
		public void OnCompleteEnemyPhaseAni()
		{
			this.DeActivateAllTriggerUI();
			NKCWarfareGameHUD.OnStartEnemyPhase onStartEnemyPhase = this.dOnStartEnemyPhase;
			if (onStartEnemyPhase == null)
			{
				return;
			}
			onStartEnemyPhase();
		}

		// Token: 0x06004F99 RID: 20377 RVA: 0x00180F42 File Offset: 0x0017F142
		private void SendGetNextOrderREQ()
		{
			if (NKCWarfareManager.CheckGetNextOrderCond(NKCScenManager.GetScenManager().GetMyUserData()) != NKM_ERROR_CODE.NEC_OK)
			{
				return;
			}
			this.m_NKCWarfareGame.SetPause(true);
			NKCPacketSender.Send_NKMPacket_WARFARE_GAME_NEXT_ORDER_REQ();
		}

		// Token: 0x06004F9A RID: 20378 RVA: 0x00180F67 File Offset: 0x0017F167
		public void SetWarfareStrID(string warfareStrID)
		{
			this.m_WarfareStrID = warfareStrID;
		}

		// Token: 0x06004F9B RID: 20379 RVA: 0x00180F70 File Offset: 0x0017F170
		public void Open()
		{
			this.m_CurrMultiplyRewardCount = 1;
			if (!this.m_NUF_WARFARE.activeSelf)
			{
				this.SetWaitBox(false);
				this.m_NUF_WARFARE.SetActive(true);
				this.ResetUI();
			}
		}

		// Token: 0x06004F9C RID: 20380 RVA: 0x00180FA0 File Offset: 0x0017F1A0
		private void ResetUI()
		{
			if (this.m_WarfareStrID == "")
			{
				return;
			}
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (nkmwarfareTemplet == null)
			{
				return;
			}
			NKMStageTempletV2 nkmstageTempletV = NKMEpisodeMgr.FindStageTempletByBattleStrID(this.m_WarfareStrID);
			if (nkmstageTempletV != null)
			{
				NKMEpisodeTempletV2 episodeTemplet = nkmstageTempletV.EpisodeTemplet;
				if (episodeTemplet != null)
				{
					this.m_OPERATION_TITLE_EP_TEXT.text = episodeTemplet.GetEpisodeTitle();
					this.m_OPERATION_TITLE_EP_TEXT_FOR_PLAY.text = episodeTemplet.GetEpisodeTitle();
				}
				this.m_OPERATION_TITLE_TEXT.text = string.Format("{0}-{1} {2}", nkmstageTempletV.ActId, nkmstageTempletV.m_StageUINum, nkmwarfareTemplet.GetWarfareName());
				this.m_OPERATION_TITLE_TEXT_FOR_PLAY.text = string.Format("{0}-{1} {2}", nkmstageTempletV.ActId, nkmstageTempletV.m_StageUINum, nkmwarfareTemplet.GetWarfareName());
				if (nkmstageTempletV.m_BuffType.Equals(RewardTuningType.None))
				{
					NKCUtil.SetGameobjectActive(this.m_OPERATION_TITLE_BONUS, false);
					NKCUtil.SetGameobjectActive(this.m_NUF_DECK_WARFARE_OPERATION_TEXT_OPERATION_TITLE_BONUS, false);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_OPERATION_TITLE_BONUS, true);
					NKCUtil.SetImageSprite(this.m_BONUS_ICON, NKCUtil.GetBounsTypeIcon(nkmstageTempletV.m_BuffType, false), false);
					NKCUtil.SetGameobjectActive(this.m_NUF_DECK_WARFARE_OPERATION_TEXT_OPERATION_TITLE_BONUS, true);
					NKCUtil.SetImageSprite(this.m_NUF_DECK_WARFARE_OPERATION_TEXT_BONUS_ICON, NKCUtil.GetBounsTypeIcon(nkmstageTempletV.m_BuffType, false), false);
				}
				this.UpdateStagePlayState();
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_OPERATION_TITLE_BOUNS.gameObject, false);
			}
			this.SetActiveRepeatOperationOnOff(NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing());
			this.InitContainerWhenOpen();
			this.UpdateMultiplyUI();
			this.SetOperationMultiplyData();
			NKCUtil.SetGameobjectActive(this.m_NKCUIOperationMultiply, false);
		}

		// Token: 0x06004F9D RID: 20381 RVA: 0x00181138 File Offset: 0x0017F338
		public void UpdateStagePlayState()
		{
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (nkmwarfareTemplet == null)
			{
				return;
			}
			NKMStageTempletV2 stageTemplet = nkmwarfareTemplet.StageTemplet;
			if (stageTemplet == null)
			{
				return;
			}
			if (stageTemplet.EnterLimit > 0)
			{
				int statePlayCnt = NKCScenManager.CurrentUserData().GetStatePlayCnt(stageTemplet.Key, false, false, false);
				string msg;
				switch (stageTemplet.EnterLimitCond)
				{
				case SHOP_RESET_TYPE.DAY:
					msg = string.Format(NKCUtilString.GET_STRING_POPUP_DUNGEON_ENTER_LIMIT_DESC_DAY_02, stageTemplet.EnterLimit - statePlayCnt, stageTemplet.EnterLimit);
					break;
				case SHOP_RESET_TYPE.WEEK:
				case SHOP_RESET_TYPE.WEEK_SUN:
				case SHOP_RESET_TYPE.WEEK_MON:
				case SHOP_RESET_TYPE.WEEK_TUE:
				case SHOP_RESET_TYPE.WEEK_WED:
				case SHOP_RESET_TYPE.WEEK_THU:
				case SHOP_RESET_TYPE.WEEK_FRI:
				case SHOP_RESET_TYPE.WEEK_SAT:
					msg = string.Format(NKCUtilString.GET_STRING_POPUP_DUNGEON_ENTER_LIMIT_DESC_WEEK_02, stageTemplet.EnterLimit - statePlayCnt, stageTemplet.EnterLimit);
					break;
				case SHOP_RESET_TYPE.MONTH:
					msg = string.Format(NKCUtilString.GET_STRING_POPUP_DUNGEON_ENTER_LIMIT_DESC_MONTH_02, stageTemplet.EnterLimit - statePlayCnt, stageTemplet.EnterLimit);
					break;
				default:
					msg = string.Format(NKCUtilString.GET_STRING_POPUP_DUNGEON_ENTER_LIMIT_DESC_DAY_02, stageTemplet.EnterLimit - statePlayCnt, stageTemplet.EnterLimit);
					break;
				}
				NKCUtil.SetLabelText(this.m_EnterLimit_TEXT, msg);
				if (stageTemplet.EnterLimit - statePlayCnt <= 0)
				{
					NKCUtil.SetLabelTextColor(this.m_EnterLimit_TEXT, Color.red);
					NKCUtil.SetBindFunction(this.m_ResourceButton, new UnityAction(this.ConfirmResetStagePlayCnt));
					NKCUtil.SetLabelText(this.m_OPERATION_START_TEXT, NKCUtilString.GET_STRING_WARFARE_GAME_HUD_OPERATION_RESTORE);
					NKCUtil.SetImageSprite(this.m_OPERATION_START_ICON, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX_SPRITE", "NKM_UI_COMMON_ICON_ENTERLIMIT_RECOVER_SMALL", false), false);
					return;
				}
				NKCUtil.SetLabelTextColor(this.m_EnterLimit_TEXT, Color.white);
				NKCUtil.SetLabelText(this.m_OPERATION_START_TEXT, NKCUtilString.GET_STRING_WARFARE_GAME_HUD_OPERATION_START);
				NKCUtil.SetBindFunction(this.m_ResourceButton, delegate()
				{
					this.m_NKCWarfareGame.OnClickGameStart(false);
				});
				NKCUtil.SetImageSprite(this.m_OPERATION_START_ICON, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX_SPRITE", "NKM_UI_COMMON_ICON_GAUNTLET", false), false);
			}
		}

		// Token: 0x06004F9E RID: 20382 RVA: 0x00181314 File Offset: 0x0017F514
		public void SelfUpdateAttackCost()
		{
			int num = 0;
			int num2 = 0;
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (nkmwarfareTemplet.StageTemplet != null && nkmwarfareTemplet.StageTemplet.EnterLimit > 0)
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				if (nkmuserData != null)
				{
					int statePlayCnt = nkmuserData.GetStatePlayCnt(nkmwarfareTemplet.StageTemplet.Key, false, false, false);
					int enterLimit = nkmwarfareTemplet.StageTemplet.EnterLimit;
					if (statePlayCnt >= enterLimit && nkmwarfareTemplet.StageTemplet.RestoreLimit > 0)
					{
						num = nkmwarfareTemplet.StageTemplet.RestoreReqItem.ItemId;
						num2 = nkmwarfareTemplet.StageTemplet.RestoreReqItem.Count32;
					}
				}
			}
			if (num == 0 || num2 == 0)
			{
				NKCWarfareManager.GetCurrWarfareAttackCost(out num, out num2);
			}
			this.SetAttackCost(num, num2);
		}

		// Token: 0x06004F9F RID: 20383 RVA: 0x001813BC File Offset: 0x0017F5BC
		public void SetWaitBox(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_WAIT_BOX_Panel, bSet);
		}

		// Token: 0x06004FA0 RID: 20384 RVA: 0x001813CA File Offset: 0x0017F5CA
		public void SetActiveContainer(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_obj_container, bSet);
		}

		// Token: 0x06004FA1 RID: 20385 RVA: 0x001813D8 File Offset: 0x0017F5D8
		public void SetContainerCount(int count)
		{
			if (count <= 0)
			{
				if (this.m_obj_container.activeSelf)
				{
					this.SetActiveContainer(false);
				}
				return;
			}
			if (!this.m_obj_container.activeSelf)
			{
				this.SetActiveContainer(true);
			}
			this.m_NUF_WARFARE_CONTAINER_COUNT.text = string.Format(NKCUtilString.GET_STRING_COUNTING_ONE_PARAM, count.ToString());
		}

		// Token: 0x06004FA2 RID: 20386 RVA: 0x00181430 File Offset: 0x0017F630
		public void InitContainerWhenOpen()
		{
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_CONTAINER_FX, false);
			for (int i = 0; i < 2; i++)
			{
				NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_WARFARE", "NUM_WARFARE_CONTAINER_MOVER", false, null);
				GameObject instant = nkcassetInstanceData.m_Instant;
				this.m_listContainerMover.Add(nkcassetInstanceData);
				if (instant != null)
				{
					instant.transform.SetParent(this.m_NUF_WARFARE_CONTAINER.transform);
					instant.SetActive(false);
					this.m_moverPoolList.Add(instant);
				}
			}
		}

		// Token: 0x06004FA3 RID: 20387 RVA: 0x001814AC File Offset: 0x0017F6AC
		public void PlayGetContainer(Vector3 itemPos, int count)
		{
			if (this.m_moverPoolList.Count == 0)
			{
				return;
			}
			GameObject gameObject = this.m_moverPoolList[0];
			for (int i = 0; i < this.m_moverPoolList.Count; i++)
			{
				GameObject gameObject2 = this.m_moverPoolList[i];
				if (!gameObject2.activeSelf)
				{
					gameObject = gameObject2;
					break;
				}
			}
			gameObject.transform.position = new Vector3(itemPos.x, itemPos.y, 0f);
			this.m_moverCoroutine = base.StartCoroutine(this.moveContainer(gameObject, count));
		}

		// Token: 0x06004FA4 RID: 20388 RVA: 0x00181538 File Offset: 0x0017F738
		private IEnumerator moveContainer(GameObject obj, int count)
		{
			obj.SetActive(true);
			Vector3 beginPos = obj.transform.localPosition;
			Vector3 endPos = this.m_obj_container.transform.localPosition;
			float deltaTime = 0f;
			for (deltaTime += Time.deltaTime; deltaTime < 0.5f; deltaTime += Time.deltaTime)
			{
				float progress = NKMTrackingFloat.TrackRatio(TRACKING_DATA_TYPE.TDT_SLOWER, deltaTime, 0.5f, 3f);
				obj.transform.localPosition = NKCUtil.Lerp(beginPos, endPos, progress);
				yield return null;
			}
			obj.transform.localPosition = endPos;
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_CONTAINER_FX, false);
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_CONTAINER_FX, true);
			this.SetContainerCount(count);
			obj.SetActive(false);
			yield break;
		}

		// Token: 0x06004FA5 RID: 20389 RVA: 0x00181555 File Offset: 0x0017F755
		public void SetActiveRecovery(bool active)
		{
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_SUB_MENU_OPERATION_RECOVERY, active);
			if (!active)
			{
				this.SetRecoveryFx(false);
			}
		}

		// Token: 0x06004FA6 RID: 20390 RVA: 0x0018156D File Offset: 0x0017F76D
		public void SetRecoveryFx(bool active)
		{
			NKCUtil.SetGameobjectActive(this.m_NUF_WARFARE_SUB_MENU_OPERATION_RECOVERY_fx, active);
		}

		// Token: 0x06004FA7 RID: 20391 RVA: 0x0018157C File Offset: 0x0017F77C
		public void SetRecoveryCount(int count)
		{
			string msg = string.Format(NKCUtilString.GET_STRING_WARFARE_RECOVERY_COUNT_ONE_PARAM, count);
			NKCUtil.SetLabelText(this.m_NUF_WARFARE_SUB_MENU_OPERATION_RECOVERY_text, msg);
		}

		// Token: 0x06004FA8 RID: 20392 RVA: 0x001815A8 File Offset: 0x0017F7A8
		public void Close()
		{
			this.SetWaitBox(false);
			if (this.m_NUF_WARFARE.activeSelf)
			{
				this.m_NUF_WARFARE.SetActive(false);
			}
			if (this.m_moverCoroutine != null)
			{
				base.StopCoroutine(this.m_moverCoroutine);
				this.m_moverCoroutine = null;
			}
			this.m_moverPoolList.Clear();
			for (int i = 0; i < this.m_listContainerMover.Count; i++)
			{
				NKCAssetResourceManager.CloseInstance(this.m_listContainerMover[i]);
			}
			this.m_listContainerMover.Clear();
			NKCPopupWarfareInfo.CheckInstanceAndClose();
		}

		// Token: 0x06004FA9 RID: 20393 RVA: 0x00181634 File Offset: 0x0017F834
		private void Update()
		{
			if (this.m_fPrevUpdateTime >= Time.time + 1f)
			{
				return;
			}
			this.m_fPrevUpdateTime = Time.time;
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			if (warfareGameData == null)
			{
				return;
			}
			if (warfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_STOP)
			{
				TimeSpan timeSpan = new TimeSpan(12, 0, 0);
				this.m_NUF_WARFARE_PAUSE_GAME_TIME_Text.text = NKCUtilString.GetTimeSpanString(timeSpan);
				return;
			}
			DateTime serverUTCTime = NKCSynchronizedTime.GetServerUTCTime(0.0);
			TimeSpan timeSpan2 = new DateTime(warfareGameData.expireTimeTick) - serverUTCTime;
			this.m_NUF_WARFARE_PAUSE_GAME_TIME_Text.text = NKCUtilString.GetTimeSpanString(timeSpan2);
		}

		// Token: 0x06004FAA RID: 20394 RVA: 0x001816C8 File Offset: 0x0017F8C8
		public void HideOperationEnterLimit()
		{
			NKCUtil.SetGameobjectActive(this.m_OPERATION_EnterLimit, false);
		}

		// Token: 0x06004FAB RID: 20395 RVA: 0x001816D6 File Offset: 0x0017F8D6
		private void OnOperationMultiplyUpdated(int newCount)
		{
			this.m_CurrMultiplyRewardCount = newCount;
			this.UpdateAttckCostUI();
		}

		// Token: 0x06004FAC RID: 20396 RVA: 0x001816E5 File Offset: 0x0017F8E5
		private void OnClickMultiplyRewardClose()
		{
			this.m_tgReadyMultiply.Select(false, false, false);
		}

		// Token: 0x06004FAD RID: 20397 RVA: 0x001816F8 File Offset: 0x0017F8F8
		private void SetOperationMultiplyData()
		{
			NKMRewardMultiplyTemplet.RewardMultiplyItem costItem = NKMRewardMultiplyTemplet.GetCostItem(NKMRewardMultiplyTemplet.ScopeType.General);
			int num = 99;
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			NKMWarfareTemplet nkmwarfareTemplet = (warfareGameData != null) ? NKMWarfareTemplet.Find(warfareGameData.warfareTempletID) : null;
			NKMStageTempletV2 nkmstageTempletV = (nkmwarfareTemplet != null) ? NKMEpisodeMgr.FindStageTempletByBattleStrID(nkmwarfareTemplet.m_WarfareStrID) : null;
			if (nkmstageTempletV != null && nkmstageTempletV.EnterLimit > 0)
			{
				int statePlayCnt = NKCScenManager.CurrentUserData().GetStatePlayCnt(nkmstageTempletV.Key, false, false, false);
				num = nkmstageTempletV.EnterLimit - statePlayCnt;
			}
			if (nkmwarfareTemplet != null && nkmwarfareTemplet.m_RewardMultiplyMax != 0)
			{
				num = Mathf.Min(num, nkmwarfareTemplet.m_RewardMultiplyMax);
			}
			this.m_NKCUIOperationMultiply.SetData(costItem.MiscItemId, costItem.MiscItemCount, this.m_ResourceButton.GetItemID(), this.m_AttackCostItemCount, this.m_CurrMultiplyRewardCount, 2, num);
		}

		// Token: 0x06004FAE RID: 20398 RVA: 0x001817B8 File Offset: 0x0017F9B8
		private void OnClickMultiply(bool bSet)
		{
			if (bSet)
			{
				if (this.m_iCurrentBatchedShipCnt <= 0)
				{
					NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_WARFARE_CANNOT_START_BECAUSE_NO_USER_UNIT, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					this.m_tgReadyMultiply.Select(false, false, false);
					return;
				}
				if (!this.CheckMultiply(true))
				{
					this.m_tgReadyMultiply.Select(false, false, false);
					return;
				}
				if (this.m_CurrMultiplyRewardCount == 1)
				{
					this.m_CurrMultiplyRewardCount = 2;
					this.UpdateAttckCostUI();
					this.SetOperationMultiplyData();
				}
			}
			NKCUtil.SetGameobjectActive(this.m_NKCUIOperationMultiply, bSet);
			if (!bSet)
			{
				this.m_CurrMultiplyRewardCount = 1;
				this.UpdateAttckCostUI();
				this.SetOperationMultiplyData();
			}
		}

		// Token: 0x06004FAF RID: 20399 RVA: 0x0018184C File Offset: 0x0017FA4C
		private bool CheckMultiply(bool bMsg)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return false;
			}
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
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.OPERATION_MULTIPLY, 0, 0))
			{
				return false;
			}
			if (!this.IsCanStartEterniumStage(true))
			{
				return false;
			}
			if (!nkmuserData.IsSuperUser())
			{
				if (!nkmuserData.CheckWarfareClear(warfareGameData.warfareTempletID))
				{
					if (bMsg)
					{
						NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_MULTIPLY_OPERATION_MEDAL_COND, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					}
					return false;
				}
				NKMWarfareClearData warfareClearData = nkmuserData.GetWarfareClearData(warfareGameData.warfareTempletID);
				if (warfareClearData == null || !warfareClearData.m_mission_result_1 || !warfareClearData.m_mission_result_2)
				{
					if (bMsg)
					{
						NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_MULTIPLY_OPERATION_MEDAL_COND, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					}
					return false;
				}
			}
			if (nkmwarfareTemplet.m_RewardMultiplyMax <= 1)
			{
				return false;
			}
			NKMStageTempletV2 nkmstageTempletV = NKMEpisodeMgr.FindStageTempletByBattleStrID(nkmwarfareTemplet.m_WarfareStrID);
			if (nkmstageTempletV.EnterLimit > 0)
			{
				int statePlayCnt = NKCScenManager.CurrentUserData().GetStatePlayCnt(nkmstageTempletV.Key, false, false, false);
				if (nkmstageTempletV.EnterLimit - statePlayCnt <= 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004FB0 RID: 20400 RVA: 0x0018194C File Offset: 0x0017FB4C
		public void UpdateMultiplyUI()
		{
			if (NKCScenManager.CurrentUserData() == null)
			{
				return;
			}
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			if (warfareGameData == null)
			{
				return;
			}
			bool flag = true;
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_WarfareStrID);
			if (nkmwarfareTemplet != null && nkmwarfareTemplet.StageTemplet != null)
			{
				NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(nkmwarfareTemplet.StageTemplet.m_StageBattleStrID);
				if (dungeonTempletBase != null)
				{
					flag = (dungeonTempletBase.m_RewardMultiplyMax > 1);
				}
			}
			bool flag2 = NKCContentManager.IsContentsUnlocked(ContentsType.OPERATION_MULTIPLY, 0, 0);
			if (!flag2 || !flag)
			{
				NKCUtil.SetGameobjectActive(this.m_objReadyMultiply, false);
				NKCUtil.SetGameobjectActive(this.m_objPlayingMultiply, false);
				return;
			}
			this.m_NKCUIOperationMultiply.SetLockUI(!flag2);
			if (warfareGameData.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_STOP)
			{
				NKCUtil.SetGameobjectActive(this.m_NKCUIOperationMultiply, false);
				bool flag3 = warfareGameData.rewardMultiply > 1;
				NKCUtil.SetGameobjectActive(this.m_objReadyMultiply, false);
				NKCUtil.SetGameobjectActive(this.m_objPlayingMultiply, flag3);
				if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.WARFARE_REPEAT))
				{
					this.SetActiveRepeatOperation(false);
				}
				else
				{
					this.SetActiveRepeatOperation(!flag3);
				}
				NKCUtil.SetLabelText(this.m_txtPlayingMultiply, NKCUtilString.GET_MULTIPLY_REWARD_ONE_PARAM, new object[]
				{
					warfareGameData.rewardMultiply
				});
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objReadyMultiply, NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.WARFARE_REPEAT));
			NKCUtil.SetGameobjectActive(this.m_objPlayingMultiply, false);
		}

		// Token: 0x04003F51 RID: 16209
		private GameObject m_NUF_WARFARE;

		// Token: 0x04003F52 RID: 16210
		private string m_WarfareStrID = "";

		// Token: 0x04003F53 RID: 16211
		public GameObject m_NUF_WARFARE_SHIP_NUM;

		// Token: 0x04003F54 RID: 16212
		public Text m_NUF_WARFARE_SHIP_NUM_TEXT;

		// Token: 0x04003F55 RID: 16213
		public GameObject m_NUF_WARFARE_SHIP_NUM_SUPPORT;

		// Token: 0x04003F56 RID: 16214
		public Text m_NUF_WARFARE_SHIP_NUM_SUPPORT_TEXT;

		// Token: 0x04003F57 RID: 16215
		public GameObject m_NUF_WARFARE_OPERATION_TITLE;

		// Token: 0x04003F58 RID: 16216
		public Text m_OPERATION_TITLE_EP_TEXT;

		// Token: 0x04003F59 RID: 16217
		public Text m_OPERATION_TITLE_TEXT;

		// Token: 0x04003F5A RID: 16218
		public Image m_OPERATION_TITLE_BOUNS;

		// Token: 0x04003F5B RID: 16219
		public Text m_OPERATION_TITLE_EP_TEXT_FOR_PLAY;

		// Token: 0x04003F5C RID: 16220
		public Text m_OPERATION_TITLE_TEXT_FOR_PLAY;

		// Token: 0x04003F5D RID: 16221
		[Header("우측 하단")]
		public GameObject m_NUF_WARFARE_SUB_MENU_OPERATION_SUPPORT_TEXT;

		// Token: 0x04003F5E RID: 16222
		public GameObject m_NUF_WARFARE_SUB_MENU_OPERATION_TEXT;

		// Token: 0x04003F5F RID: 16223
		public GameObject m_NUF_WARFARE_SUB_MENU_OPERATION_BUTTON;

		// Token: 0x04003F60 RID: 16224
		public Animator m_NUF_WARFARE_SUB_MENU_OPERATION_BUTTON_FX;

		// Token: 0x04003F61 RID: 16225
		public GameObject m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT;

		// Token: 0x04003F62 RID: 16226
		public NKCUIComButton m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_BUTTON;

		// Token: 0x04003F63 RID: 16227
		public NKCUIComButton m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_BUTTON_RED;

		// Token: 0x04003F64 RID: 16228
		public Text m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_TEXT;

		// Token: 0x04003F65 RID: 16229
		public Text m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_TURN_COUNT;

		// Token: 0x04003F66 RID: 16230
		public Animator m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_FX;

		// Token: 0x04003F67 RID: 16231
		public GameObject m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_TURN;

		// Token: 0x04003F68 RID: 16232
		public Text m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_TEXT_RED;

		// Token: 0x04003F69 RID: 16233
		public Text m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_TURN_COUNT_RED;

		// Token: 0x04003F6A RID: 16234
		public Animator m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_FX_RED;

		// Token: 0x04003F6B RID: 16235
		public GameObject m_NUF_WARFARE_SUB_MENU_OPERATION_NEXT_TURN_RED;

		// Token: 0x04003F6C RID: 16236
		public GameObject m_NUF_WARFARE_SUB_MENU_OPERATION_RECOVERY;

		// Token: 0x04003F6D RID: 16237
		public NKCUIComStateButton m_NUF_WARFARE_SUB_MENU_OPERATION_RECOVERY_btn;

		// Token: 0x04003F6E RID: 16238
		public GameObject m_NUF_WARFARE_SUB_MENU_OPERATION_RECOVERY_fx;

		// Token: 0x04003F6F RID: 16239
		public Text m_NUF_WARFARE_SUB_MENU_OPERATION_RECOVERY_text;

		// Token: 0x04003F70 RID: 16240
		[Header("중첩작전")]
		public GameObject m_objReadyMultiply;

		// Token: 0x04003F71 RID: 16241
		public NKCUIComToggle m_tgReadyMultiply;

		// Token: 0x04003F72 RID: 16242
		public NKCUIOperationMultiply m_NKCUIOperationMultiply;

		// Token: 0x04003F73 RID: 16243
		public GameObject m_objPlayingMultiply;

		// Token: 0x04003F74 RID: 16244
		public Text m_txtPlayingMultiply;

		// Token: 0x04003F75 RID: 16245
		private int m_CurrMultiplyRewardCount = 1;

		// Token: 0x04003F76 RID: 16246
		[Space]
		public GameObject m_ReadyTitle;

		// Token: 0x04003F77 RID: 16247
		public GameObject m_PlayTitle;

		// Token: 0x04003F78 RID: 16248
		[Header("페이즈")]
		public GameObject m_NUF_WARFARE_PHASE;

		// Token: 0x04003F79 RID: 16249
		public GameObject m_NUF_WARFARE_PHASE_USER;

		// Token: 0x04003F7A RID: 16250
		public GameObject m_NUF_WARFARE_PHASE_ENEMY;

		// Token: 0x04003F7B RID: 16251
		public Text m_NUF_WARFARE_PHASE_NUM_TEXT;

		// Token: 0x04003F7C RID: 16252
		[Header("우측 상단")]
		public RectTransform m_rtUpperRightMenuRoot;

		// Token: 0x04003F7D RID: 16253
		public Text m_NUF_WARFARE_PAUSE_GAME_TIME_Text;

		// Token: 0x04003F7E RID: 16254
		public GameObject m_NUF_WARFARE_AUTO;

		// Token: 0x04003F7F RID: 16255
		public GameObject m_NUF_WARFARE_AUTO_ON;

		// Token: 0x04003F80 RID: 16256
		public NKCUIComButton m_NUF_WARFARE_AUTO_ON_btn;

		// Token: 0x04003F81 RID: 16257
		public GameObject m_NUF_WARFARE_AUTO_OFF;

		// Token: 0x04003F82 RID: 16258
		public NKCUIComButton m_NUF_WARFARE_AUTO_OFF_Btn;

		// Token: 0x04003F83 RID: 16259
		public Text m_NUF_WARFARE_AUTO_OFF_Txt;

		// Token: 0x04003F84 RID: 16260
		public Image m_NUF_WARFARE_AUTO_OFF_Img;

		// Token: 0x04003F85 RID: 16261
		public GameObject m_NUF_WARFARE_REPEAT;

		// Token: 0x04003F86 RID: 16262
		public NKCUIComButton m_NUF_WARFARE_REPEAT_ON_btn;

		// Token: 0x04003F87 RID: 16263
		public NKCUIComButton m_NUF_WARFARE_REPEAT_OFF_Btn;

		// Token: 0x04003F88 RID: 16264
		public GameObject m_NUF_WARFARE_AUTO_SUPPLY;

		// Token: 0x04003F89 RID: 16265
		public NKCUIComButton m_NUF_WARFARE_AUTO_SUPPLY_OFF_btn;

		// Token: 0x04003F8A RID: 16266
		public NKCUIComButton m_NUF_WARFARE_AUTO_SUPPLY_ON_btn;

		// Token: 0x04003F8B RID: 16267
		[Space]
		public GameObject m_NUF_WARFARE_DECO;

		// Token: 0x04003F8C RID: 16268
		public GameObject m_NUF_WARFARE_PAUSE;

		// Token: 0x04003F8D RID: 16269
		public NKCUIComButton m_NUF_WARFARE_PAUSE_BUTTON;

		// Token: 0x04003F8E RID: 16270
		public GameObject m_NKM_UI_POPUP_WARFARE_PHASE_PLAYER;

		// Token: 0x04003F8F RID: 16271
		public GameObject m_NKM_UI_POPUP_WARFARE_PHASE_ENEMY;

		// Token: 0x04003F90 RID: 16272
		public GameObject m_NKM_UI_POPUP_WARFARE_PHASE_COMPLETE;

		// Token: 0x04003F91 RID: 16273
		public GameObject m_NKM_UI_POPUP_WARFARE_PHASE_FAIL;

		// Token: 0x04003F92 RID: 16274
		public NKCUIComResourceButton m_ResourceButton;

		// Token: 0x04003F93 RID: 16275
		public NKCUIComButton m_NUF_WARFARE_INFO_WARINFO;

		// Token: 0x04003F94 RID: 16276
		public GameObject m_NUF_WARFARE_INFO_MEDALINFO_DETAIL;

		// Token: 0x04003F95 RID: 16277
		public DOTweenAnimation m_NUF_WARFARE_INFO_MEDALINFO_DETAIL_DTA;

		// Token: 0x04003F96 RID: 16278
		public Text m_MEDALINFO_DETAIL_SLOT1_TEXT;

		// Token: 0x04003F97 RID: 16279
		public Text m_MEDALINFO_DETAIL_SLOT2_TEXT;

		// Token: 0x04003F98 RID: 16280
		public Text m_MEDALINFO_DETAIL_SLOT3_TEXT;

		// Token: 0x04003F99 RID: 16281
		public GameObject m_MEDALINFO_DETAIL_SLOT1_line;

		// Token: 0x04003F9A RID: 16282
		public GameObject m_MEDALINFO_DETAIL_SLOT2_line;

		// Token: 0x04003F9B RID: 16283
		public NKCUIComToggle m_NUF_WARFARE_INFO_MEDALINFO_toggle;

		// Token: 0x04003F9C RID: 16284
		[Header("승리패배 조건")]
		public GameObject m_NUF_WARFARE_INFO_VICTORY_1_ICON_TARGET;

		// Token: 0x04003F9D RID: 16285
		public GameObject m_NUF_WARFARE_INFO_VICTORY_1_ICON_WC_ENTER;

		// Token: 0x04003F9E RID: 16286
		public GameObject m_NUF_WARFARE_INFO_VICTORY_1_ICON_WC_HOLD;

		// Token: 0x04003F9F RID: 16287
		public Text m_NUF_WARFARE_INFO_VICTORY_1_TEXT;

		// Token: 0x04003FA0 RID: 16288
		public GameObject m_NUF_WARFARE_INFO_VICTORY_1_ICON_WC_DEFENSE;

		// Token: 0x04003FA1 RID: 16289
		public Text m_NUF_WARFARE_INFO_DEFEAT_TEXT;

		// Token: 0x04003FA2 RID: 16290
		[Header("선택된 스쿼드")]
		public GameObject m_NUF_WARFARE_SELECTED_SQUAD;

		// Token: 0x04003FA3 RID: 16291
		public Text m_NUF_WARFARE_SELECTED_SQUAD_Text1;

		// Token: 0x04003FA4 RID: 16292
		public Text m_NUF_WARFARE_SELECTED_SQUAD_Text2;

		// Token: 0x04003FA5 RID: 16293
		public NKCUIComStateButton m_NUF_WARFARE_SELECTED_SUPPLY;

		// Token: 0x04003FA6 RID: 16294
		public NKCUIComStateButton m_NUF_WARFARE_SELECTED_REPAIR;

		// Token: 0x04003FA7 RID: 16295
		public NKCUIComStateButton m_NUF_WARFARE_SELECTED_DETAIL;

		// Token: 0x04003FA8 RID: 16296
		private NKMDeckIndex m_NKMDeckIndexSelected;

		// Token: 0x04003FA9 RID: 16297
		public GameObject m_NUF_WARFARE_WAIT_BOX_Panel;

		// Token: 0x04003FAA RID: 16298
		public GameObject m_NUF_WARFARE_CONTAINER;

		// Token: 0x04003FAB RID: 16299
		public GameObject m_obj_container;

		// Token: 0x04003FAC RID: 16300
		public Text m_NUF_WARFARE_CONTAINER_COUNT;

		// Token: 0x04003FAD RID: 16301
		public GameObject m_NUF_WARFARE_CONTAINER_FX;

		// Token: 0x04003FAE RID: 16302
		private List<GameObject> m_moverPoolList = new List<GameObject>();

		// Token: 0x04003FAF RID: 16303
		private Coroutine m_moverCoroutine;

		// Token: 0x04003FB0 RID: 16304
		private const int MOVER_POOL_COUNT = 2;

		// Token: 0x04003FB1 RID: 16305
		private const float MOVER_TIME = 0.5f;

		// Token: 0x04003FB2 RID: 16306
		private float m_fPrevUpdateTime;

		// Token: 0x04003FB3 RID: 16307
		private NKCWarfareGameHUD.OnStartUserPhase dOnStartUserPhase;

		// Token: 0x04003FB4 RID: 16308
		private NKCWarfareGameHUD.OnStartEnemyPhase dOnStartEnemyPhase;

		// Token: 0x04003FB5 RID: 16309
		private NKCWarfareGame m_NKCWarfareGame;

		// Token: 0x04003FB6 RID: 16310
		[Header("입장 제한")]
		public GameObject m_OPERATION_EnterLimit;

		// Token: 0x04003FB7 RID: 16311
		public Text m_EnterLimit_TEXT;

		// Token: 0x04003FB8 RID: 16312
		public Image m_OPERATION_START_ICON;

		// Token: 0x04003FB9 RID: 16313
		public Text m_OPERATION_START_TEXT;

		// Token: 0x04003FBA RID: 16314
		public GameObject m_OPERATION_TITLE_BONUS;

		// Token: 0x04003FBB RID: 16315
		public Image m_BONUS_ICON;

		// Token: 0x04003FBC RID: 16316
		private int m_AttackCostItemCount;

		// Token: 0x04003FBD RID: 16317
		public GameObject m_NUF_DECK_WARFARE_OPERATION_TEXT_OPERATION_TITLE_BONUS;

		// Token: 0x04003FBE RID: 16318
		public Image m_NUF_DECK_WARFARE_OPERATION_TEXT_BONUS_ICON;

		// Token: 0x04003FBF RID: 16319
		private int m_iCurrentBatchedShipCnt;

		// Token: 0x04003FC0 RID: 16320
		private bool m_bBatchedSupportShip;

		// Token: 0x04003FC1 RID: 16321
		private List<NKCAssetInstanceData> m_listContainerMover = new List<NKCAssetInstanceData>();

		// Token: 0x02001492 RID: 5266
		// (Invoke) Token: 0x0600A946 RID: 43334
		public delegate void OnStartUserPhase();

		// Token: 0x02001493 RID: 5267
		// (Invoke) Token: 0x0600A94A RID: 43338
		public delegate void OnStartEnemyPhase();
	}
}
