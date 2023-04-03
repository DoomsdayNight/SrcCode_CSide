using System;
using System.Collections.Generic;
using ClientPacket.Warfare;
using NKM;
using NKM.Shop;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A77 RID: 2679
	public class NKCPopupRepeatOperation : NKCUIBase
	{
		// Token: 0x170013BE RID: 5054
		// (get) Token: 0x06007675 RID: 30325 RVA: 0x002768C3 File Offset: 0x00274AC3
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170013BF RID: 5055
		// (get) Token: 0x06007676 RID: 30326 RVA: 0x002768C6 File Offset: 0x00274AC6
		public override string MenuName
		{
			get
			{
				return "NKCPopupRepeatOperation";
			}
		}

		// Token: 0x170013C0 RID: 5056
		// (get) Token: 0x06007677 RID: 30327 RVA: 0x002768D0 File Offset: 0x00274AD0
		// (set) Token: 0x06007678 RID: 30328 RVA: 0x0027691F File Offset: 0x00274B1F
		public static NKCPopupRepeatOperation Instance
		{
			get
			{
				if (NKCPopupRepeatOperation.m_Instance == null)
				{
					NKCPopupRepeatOperation.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupRepeatOperation>("AB_UI_NKM_UI_OPERATION", "NKM_UI_POPUP_OPERATION REPEAT", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupRepeatOperation.CleanupInstance)).GetInstance<NKCPopupRepeatOperation>();
					NKCPopupRepeatOperation.m_Instance.Init();
				}
				return NKCPopupRepeatOperation.m_Instance;
			}
			private set
			{
			}
		}

		// Token: 0x170013C1 RID: 5057
		// (get) Token: 0x06007679 RID: 30329 RVA: 0x00276921 File Offset: 0x00274B21
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupRepeatOperation.m_Instance != null && NKCPopupRepeatOperation.m_Instance.IsOpen;
			}
		}

		// Token: 0x0600767A RID: 30330 RVA: 0x0027693C File Offset: 0x00274B3C
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupRepeatOperation.m_Instance != null && NKCPopupRepeatOperation.m_Instance.IsOpen)
			{
				NKCPopupRepeatOperation.m_Instance.Close();
			}
		}

		// Token: 0x0600767B RID: 30331 RVA: 0x00276961 File Offset: 0x00274B61
		private static void CleanupInstance()
		{
			NKCPopupRepeatOperation.m_Instance = null;
		}

		// Token: 0x0600767C RID: 30332 RVA: 0x0027696C File Offset: 0x00274B6C
		public void Init()
		{
			this.m_etBG.triggers.Clear();
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(delegate(BaseEventData e)
			{
				base.Close();
			});
			this.m_etBG.triggers.Add(entry);
			this.m_csbtnClose.PointerClick.RemoveAllListeners();
			this.m_csbtnClose.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_csbtnStart.PointerClick.RemoveAllListeners();
			this.m_csbtnStart.PointerClick.AddListener(new UnityAction(this.OnClickStart));
			NKCUtil.SetHotkey(this.m_csbtnStart, HotkeyEventType.Confirm, null, false);
			this.m_csbtnStartDisabled.PointerClick.RemoveAllListeners();
			this.m_csbtnStartDisabled.PointerClick.AddListener(new UnityAction(this.OnClickStart));
			this.m_csbtnCancel.PointerClick.RemoveAllListeners();
			this.m_csbtnCancel.PointerClick.AddListener(new UnityAction(this.OnClickCancel));
			this.m_csbtnContinue.PointerClick.RemoveAllListeners();
			this.m_csbtnContinue.PointerClick.AddListener(delegate()
			{
				NKCScenManager.GetScenManager().GetNKCPowerSaveMode().SetEnable(this.m_ctPowerSaveMode.m_bChecked);
				this.OnClickClose();
			});
			NKCUtil.SetHotkey(this.m_csbtnContinue, HotkeyEventType.Confirm, null, false);
			this.m_csbtnOK.PointerClick.RemoveAllListeners();
			this.m_csbtnOK.PointerClick.AddListener(new UnityAction(this.OnClickClose));
			NKCUtil.SetHotkey(this.m_csbtnOK, HotkeyEventType.Confirm, null, false);
			this.m_csbtnRepeatCountStopReset.PointerClick.RemoveAllListeners();
			this.m_csbtnRepeatCountStopReset.PointerClick.AddListener(new UnityAction(this.OnClickResetRepeatCount));
			this.m_csbtnRepeatCountStopAdd1.PointerClick.RemoveAllListeners();
			this.m_csbtnRepeatCountStopAdd1.PointerClick.AddListener(delegate()
			{
				this.OnClickRepeatCountAdd(1);
			});
			this.m_csbtnRepeatCountStopAdd10.PointerClick.RemoveAllListeners();
			this.m_csbtnRepeatCountStopAdd10.PointerClick.AddListener(delegate()
			{
				this.OnClickRepeatCountAdd(10);
			});
			this.m_csbtnRepeatCountStopAdd100.PointerClick.RemoveAllListeners();
			this.m_csbtnRepeatCountStopAdd100.PointerClick.AddListener(delegate()
			{
				this.OnClickRepeatCountAdd(100);
			});
			this.m_csbtnRepeatCountStopAddMax.PointerClick.RemoveAllListeners();
			this.m_csbtnRepeatCountStopAddMax.PointerClick.AddListener(new UnityAction(this.OnClickRepeatAddMax));
			this.m_NKCUISlot.Init();
			this.m_lsrReward.dOnGetObject += this.GetRewardSlot;
			this.m_lsrReward.dOnReturnObject += this.ReturnRewardSlot;
			this.m_lsrReward.dOnProvideData += this.ProvideRewardSlot;
			NKCUtil.SetScrollHotKey(this.m_lsrReward, null);
			NKCPopupRepeatOperation.m_fElapsedTime = 0f;
		}

		// Token: 0x0600767D RID: 30333 RVA: 0x00276C34 File Offset: 0x00274E34
		public RectTransform GetRewardSlot(int index)
		{
			NKCUISlot newInstance = NKCUISlot.GetNewInstance(null);
			if (newInstance != null)
			{
				return newInstance.GetComponent<RectTransform>();
			}
			return null;
		}

		// Token: 0x0600767E RID: 30334 RVA: 0x00276C59 File Offset: 0x00274E59
		public void ReturnRewardSlot(Transform tr)
		{
			tr.SetParent(base.transform);
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x0600767F RID: 30335 RVA: 0x00276C74 File Offset: 0x00274E74
		public void ProvideRewardSlot(Transform tr, int index)
		{
			NKCUISlot component = tr.GetComponent<NKCUISlot>();
			if (component != null)
			{
				if (this.m_lstList.Count > index && index >= 0)
				{
					NKCUtil.SetGameobjectActive(component, true);
					component.SetData(this.m_lstList[index], true, null);
					this.SetEquipSlotHaveNoMenu(component);
					return;
				}
				NKCUtil.SetGameobjectActive(component, false);
			}
		}

		// Token: 0x06007680 RID: 30336 RVA: 0x00276CD0 File Offset: 0x00274ED0
		private void UpdateRepeatCountUI()
		{
			if (NKCPopupRepeatOperation.m_RepeatCount >= 1L)
			{
				NKCUtil.SetLabelText(this.m_lbRepeatCount, string.Format(NKCUtilString.GET_STRING_REPEAT_OPERATION_REPEAT_COUNT_ONE_PARAM, NKCPopupRepeatOperation.m_RepeatCount));
				return;
			}
			NKCUtil.SetLabelText(this.m_lbRepeatCount, string.Format(NKCUtilString.GET_STRING_REPEAT_OPERATION_RED_COLOR_REPEAT_COUNT_ONE_PARAM, NKCPopupRepeatOperation.m_RepeatCount));
		}

		// Token: 0x06007681 RID: 30337 RVA: 0x00276D28 File Offset: 0x00274F28
		private void OnClickResetRepeatCount()
		{
			if (this.m_bCheatMode)
			{
				NKCPopupRepeatOperation.m_RepeatCount = 1L;
			}
			else if (NKCPopupRepeatOperation.m_CostHavingCount >= (long)NKCPopupRepeatOperation.m_CostItemCount)
			{
				NKCPopupRepeatOperation.m_RepeatCount = 1L;
			}
			else
			{
				NKCPopupRepeatOperation.m_RepeatCount = 0L;
			}
			this.UpdateRepeatCountUI();
			this.UpdateHavingCountUI();
			this.UpdateCostItemCountUI();
			NKCUtil.SetGameobjectActive(this.m_csbtnStart, NKCPopupRepeatOperation.m_RepeatCount == 1L);
			NKCUtil.SetGameobjectActive(this.m_csbtnStartDisabled, NKCPopupRepeatOperation.m_RepeatCount == 0L);
		}

		// Token: 0x06007682 RID: 30338 RVA: 0x00276DA0 File Offset: 0x00274FA0
		private void OnClickRepeatCountAdd(int count)
		{
			long num = NKCPopupRepeatOperation.m_RepeatCount;
			num += (long)count;
			long maxRepeatCount = this.GetMaxRepeatCount();
			if (maxRepeatCount < num)
			{
				num = maxRepeatCount;
			}
			NKCPopupRepeatOperation.m_RepeatCount = num;
			this.UpdateRepeatCountUI();
			this.UpdateHavingCountUI();
			this.UpdateCostItemCountUI();
		}

		// Token: 0x06007683 RID: 30339 RVA: 0x00276DE0 File Offset: 0x00274FE0
		private long GetMaxRepeatCount()
		{
			if (this.m_bCheatMode)
			{
				return (long)this.MAX_REPEAT_COUNT_FOR_CHEAT;
			}
			if (NKCPopupRepeatOperation.m_CostItemCount <= 0)
			{
				return 0L;
			}
			int num = -1;
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null && this.m_iCurEpisodeKey != 0)
			{
				NKMStageTempletV2 nkmstageTempletV = NKMStageTempletV2.Find(this.m_iCurEpisodeKey);
				if (nkmstageTempletV != null && nkmstageTempletV.EnterLimit > 0)
				{
					if (nkmuserData.IsHaveStatePlayData(this.m_iCurEpisodeKey))
					{
						num = nkmstageTempletV.EnterLimit - nkmuserData.GetStatePlayCnt(this.m_iCurEpisodeKey, false, false, false);
					}
					else
					{
						num = nkmstageTempletV.EnterLimit;
					}
				}
			}
			if (num >= 0)
			{
				return Math.Min((long)num, NKCPopupRepeatOperation.m_CostHavingCount / (long)NKCPopupRepeatOperation.m_CostItemCount);
			}
			return NKCPopupRepeatOperation.m_CostHavingCount / (long)NKCPopupRepeatOperation.m_CostItemCount;
		}

		// Token: 0x06007684 RID: 30340 RVA: 0x00276E85 File Offset: 0x00275085
		private void OnClickRepeatAddMax()
		{
			NKCPopupRepeatOperation.m_RepeatCount = this.GetMaxRepeatCount();
			this.UpdateRepeatCountUI();
			this.UpdateHavingCountUI();
			this.UpdateCostItemCountUI();
		}

		// Token: 0x06007685 RID: 30341 RVA: 0x00276EA4 File Offset: 0x002750A4
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			if (this.m_dOnClose != null)
			{
				this.m_dOnClose();
				this.m_dOnClose = null;
			}
		}

		// Token: 0x06007686 RID: 30342 RVA: 0x00276ECC File Offset: 0x002750CC
		public void OpenForResult(NKCPopupRepeatOperation.dOnClose _dOnClose = null)
		{
			this.m_bCheatMode = false;
			this.m_dOnClose = _dOnClose;
			base.UIOpened(true);
			if (this.m_bFirstOpen)
			{
				this.m_lsrReward.PrepareCells(0);
				this.m_bFirstOpen = false;
			}
			this.UpdateEnterLimitData();
			this.SetUIForResult();
		}

		// Token: 0x06007687 RID: 30343 RVA: 0x00276F0C File Offset: 0x0027510C
		private void SetUIForResult()
		{
			NKCRepeatOperaion nkcrepeatOperaion = NKCScenManager.GetScenManager().GetNKCRepeatOperaion();
			if (nkcrepeatOperaion == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objPowerSaveMode, false);
			NKCUtil.SetLabelText(this.m_lbEPTitle, nkcrepeatOperaion.GetPrevEPTitle());
			NKCUtil.SetLabelText(this.m_lbEPName, nkcrepeatOperaion.GetPrevEPName());
			int prevCostItemID = nkcrepeatOperaion.GetPrevCostItemID();
			nkcrepeatOperaion.GetPrevCostItemCount();
			if (prevCostItemID > 0)
			{
				NKCUtil.SetGameobjectActive(this.m_NKCUISlot, true);
				this.m_NKCUISlot.SetData(NKCUISlot.SlotData.MakeMiscItemData(prevCostItemID, 1L, 0), true, null);
				NKCUtil.SetLabelText(this.m_lbCostDesc, NKCUtilString.GET_STRING_REPEAT_OPERATION_COST_COUNT_UNTIL_NOW);
				NKCUtil.SetGameobjectActive(this.m_objHavingCount, false);
				NKCUtil.SetGameobjectActive(this.m_objHavingCountNone, true);
				long num = nkcrepeatOperaion.GetPrevCostIncreaseCount() * (long)NKCPopupRepeatOperation.m_CostItemCount;
				NKCUtil.SetLabelText(this.m_lbCostCount, num.ToString());
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NKCUISlot, false);
			}
			NKCUtil.SetGameobjectActive(this.m_objRepeatCountStop, false);
			NKCUtil.SetGameobjectActive(this.m_objRepeatCountProgress, true);
			NKCUtil.SetGameobjectActive(this.m_objStateNone, false);
			NKCUtil.SetGameobjectActive(this.m_objState, true);
			NKCUtil.SetGameobjectActive(this.m_objStateProgressingIcon, false);
			NKCUtil.SetLabelText(this.m_lbRemainRepeatCount, "");
			NKCUtil.SetLabelText(this.m_lbRepeatCountInProgress, string.Format(NKCUtilString.GET_STRING_REPEAT_OPERATION_COMPLETE_REPEAT_COUNT_ONE_PARAM, nkcrepeatOperaion.GetPrevRepeatCount().ToString()));
			NKCUtil.SetGameobjectActive(this.m_csbtnStart, false);
			NKCUtil.SetGameobjectActive(this.m_csbtnStartDisabled, false);
			NKCUtil.SetGameobjectActive(this.m_csbtnCancel, false);
			NKCUtil.SetGameobjectActive(this.m_csbtnContinue, false);
			NKCUtil.SetGameobjectActive(this.m_csbtnOK, true);
			NKCUtil.SetLabelText(this.m_lbState, nkcrepeatOperaion.GetStopReason());
			this.UpdateResultTime();
			List<NKCUISlot.SlotData> list = NKCUISlot.MakeSlotDataListFromReward(nkcrepeatOperaion.GetPrevReward(), false, false);
			if (list.Count == 0)
			{
				this.m_lsrReward.TotalCount = 0;
				this.m_lsrReward.RefreshCells(false);
				NKCUtil.SetGameobjectActive(this.m_objRewardNone, true);
				return;
			}
			this.m_lstList = list;
			this.m_lsrReward.TotalCount = this.m_lstList.Count;
			NKCUtil.SetGameobjectActive(this.m_objRewardNone, false);
			this.m_lsrReward.velocity = new Vector2(0f, 0f);
			this.m_lsrReward.SetIndexPosition(0);
		}

		// Token: 0x06007688 RID: 30344 RVA: 0x00277130 File Offset: 0x00275330
		public void Open(NKCPopupRepeatOperation.dOnClose _dOnClose = null)
		{
			this.m_bCheatMode = false;
			this.m_dOnClose = _dOnClose;
			base.UIOpened(true);
			if (this.m_bFirstOpen)
			{
				this.m_lsrReward.PrepareCells(0);
				this.m_bFirstOpen = false;
			}
			this.UpdateEnterLimitData();
			this.SetUIByCurrScen();
		}

		// Token: 0x06007689 RID: 30345 RVA: 0x00277170 File Offset: 0x00275370
		private void UpdateEnterLimitData()
		{
			this.m_iCurEpisodeKey = 0;
			NKMGameData gameData = NKCScenManager.GetScenManager().GetGameClient().GetGameData();
			NKMStageTempletV2 nkmstageTempletV = null;
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WARFARE_GAME && NKCScenManager.GetScenManager().WarfareGameData != null)
			{
				NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(NKCScenManager.GetScenManager().WarfareGameData.warfareTempletID);
				if (nkmwarfareTemplet != null)
				{
					nkmstageTempletV = nkmwarfareTemplet.StageTemplet;
				}
			}
			if (nkmstageTempletV == null && NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_DUNGEON_ATK_READY)
			{
				nkmstageTempletV = NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY().GetStageTemplet();
			}
			if (nkmstageTempletV == null && gameData != null && gameData.GetGameType() == NKM_GAME_TYPE.NGT_WARFARE)
			{
				nkmstageTempletV = NKMWarfareTemplet.Find(gameData.m_WarfareID).StageTemplet;
			}
			else if (nkmstageTempletV == null && gameData != null)
			{
				NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(NKMDungeonManager.GetDungeonStrID(gameData.m_DungeonID));
				if (dungeonTempletBase != null)
				{
					nkmstageTempletV = dungeonTempletBase.StageTemplet;
				}
			}
			if (nkmstageTempletV == null)
			{
				NKCUtil.SetGameobjectActive(this.m_OPERATION_REPEAT_EnterLimit, false);
				return;
			}
			int statePlayCnt = NKCScenManager.CurrentUserData().GetStatePlayCnt(nkmstageTempletV.Key, false, false, false);
			string msg;
			switch (nkmstageTempletV.EnterLimitCond)
			{
			case SHOP_RESET_TYPE.DAY:
				msg = string.Format(NKCUtilString.GET_STRING_POPUP_DUNGEON_ENTER_LIMIT_DESC_DAY_02, nkmstageTempletV.EnterLimit - statePlayCnt, nkmstageTempletV.EnterLimit);
				break;
			case SHOP_RESET_TYPE.WEEK:
			case SHOP_RESET_TYPE.WEEK_SUN:
			case SHOP_RESET_TYPE.WEEK_MON:
			case SHOP_RESET_TYPE.WEEK_TUE:
			case SHOP_RESET_TYPE.WEEK_WED:
			case SHOP_RESET_TYPE.WEEK_THU:
			case SHOP_RESET_TYPE.WEEK_FRI:
			case SHOP_RESET_TYPE.WEEK_SAT:
				msg = string.Format(NKCUtilString.GET_STRING_POPUP_DUNGEON_ENTER_LIMIT_DESC_WEEK_02, nkmstageTempletV.EnterLimit - statePlayCnt, nkmstageTempletV.EnterLimit);
				break;
			case SHOP_RESET_TYPE.MONTH:
				msg = string.Format(NKCUtilString.GET_STRING_POPUP_DUNGEON_ENTER_LIMIT_DESC_MONTH_02, nkmstageTempletV.EnterLimit - statePlayCnt, nkmstageTempletV.EnterLimit);
				break;
			default:
				msg = string.Format(NKCUtilString.GET_STRING_POPUP_DUNGEON_ENTER_LIMIT_DESC_DAY_02, nkmstageTempletV.EnterLimit - statePlayCnt, nkmstageTempletV.EnterLimit);
				break;
			}
			this.m_iCurEpisodeKey = nkmstageTempletV.Key;
			NKCUtil.SetGameobjectActive(this.m_OPERATION_REPEAT_EnterLimit, nkmstageTempletV.EnterLimit > 0);
			NKCUtil.SetLabelText(this.m_EnterLimit_TEXT, msg);
			if (nkmstageTempletV.m_BuffType.Equals(RewardTuningType.None))
			{
				NKCUtil.SetGameobjectActive(this.m_BonusType, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_BonusType, true);
				NKCUtil.SetImageSprite(this.m_BonusType_Icon, NKCUtil.GetBounsTypeIcon(nkmstageTempletV.m_BuffType, false), false);
			}
			if (nkmstageTempletV.EnterLimit - statePlayCnt <= 0)
			{
				NKCUtil.SetLabelTextColor(this.m_EnterLimit_TEXT, Color.red);
				NKCUtil.SetGameobjectActive(this.m_csbtnStart, false);
				NKCUtil.SetGameobjectActive(this.m_csbtnStartDisabled, true);
				return;
			}
			NKCUtil.SetLabelTextColor(this.m_EnterLimit_TEXT, Color.white);
			NKCUtil.SetGameobjectActive(this.m_csbtnStart, true);
			NKCUtil.SetGameobjectActive(this.m_csbtnStartDisabled, false);
		}

		// Token: 0x0600768A RID: 30346 RVA: 0x0027740C File Offset: 0x0027560C
		private void SetEP_UI(NKMStageTempletV2 cNKMStageTemplet, string dungeonOrWarfareName)
		{
			if (cNKMStageTemplet == null)
			{
				return;
			}
			NKMEpisodeTempletV2 episodeTemplet = cNKMStageTemplet.EpisodeTemplet;
			if (episodeTemplet != null)
			{
				NKCUtil.SetLabelText(this.m_lbEPTitle, episodeTemplet.GetEpisodeTitle());
			}
			if (cNKMStageTemplet.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_PRACTICE)
			{
				NKCUtil.SetLabelText(this.m_lbEPName, string.Format(NKCUtilString.GET_STRING_EP_TRAINING_NUMBER, cNKMStageTemplet.m_StageUINum) + " " + dungeonOrWarfareName);
				return;
			}
			if (episodeTemplet != null && episodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_DAILY)
			{
				NKCUtil.SetLabelText(this.m_lbEPName, dungeonOrWarfareName + " " + NKCUtilString.GetDailyDungeonLVDesc(cNKMStageTemplet.m_StageUINum));
				return;
			}
			NKCUtil.SetLabelText(this.m_lbEPName, string.Concat(new string[]
			{
				cNKMStageTemplet.ActId.ToString(),
				"-",
				cNKMStageTemplet.m_StageUINum.ToString(),
				". ",
				dungeonOrWarfareName
			}));
		}

		// Token: 0x0600768B RID: 30347 RVA: 0x002774E8 File Offset: 0x002756E8
		private void SetUIByCurrScen()
		{
			NKCRepeatOperaion nkcrepeatOperaion = NKCScenManager.GetScenManager().GetNKCRepeatOperaion();
			if (nkcrepeatOperaion == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objPowerSaveMode, true);
			this.m_ctPowerSaveMode.Select(false, false, false);
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMStageTempletV2 cNKMStageTemplet = null;
			int num = 0;
			int costItemCount = 0;
			NKC_REPEAT_OPERATION_TYPE nkc_REPEAT_OPERATION_TYPE;
			if (!NKCRepeatOperaion.GetRepeatOperationType(out nkc_REPEAT_OPERATION_TYPE, out cNKMStageTemplet))
			{
				return;
			}
			string episodeBattleName = NKCRepeatOperaion.GetEpisodeBattleName();
			NKCRepeatOperaion.GetCostInfo(out num, out costItemCount);
			this.SetEP_UI(cNKMStageTemplet, episodeBattleName);
			if (this.m_lbEPName != null)
			{
				NKCPopupRepeatOperation.m_LastEPName = this.m_lbEPName.text;
			}
			if (this.m_lbEPTitle != null)
			{
				NKCPopupRepeatOperation.m_LastEPTitle = this.m_lbEPTitle.text;
			}
			if (num > 0)
			{
				NKCUtil.SetGameobjectActive(this.m_NKCUISlot, true);
				this.m_NKCUISlot.SetData(NKCUISlot.SlotData.MakeMiscItemData(num, 1L, 0), true, null);
				if (nkcrepeatOperaion.GetIsOnGoing())
				{
					NKCUtil.SetLabelText(this.m_lbCostDesc, NKCUtilString.GET_STRING_REPEAT_OPERATION_COST_COUNT_UNTIL_NOW);
					NKCUtil.SetGameobjectActive(this.m_objHavingCount, false);
					NKCUtil.SetGameobjectActive(this.m_objHavingCountNone, true);
				}
				else
				{
					NKCUtil.SetLabelText(this.m_lbCostDesc, NKCUtilString.GET_STRING_REPEAT_OPERATION_COST_COUNT);
					NKCUtil.SetGameobjectActive(this.m_objHavingCount, true);
					NKCUtil.SetGameobjectActive(this.m_objHavingCountNone, false);
					NKCPopupRepeatOperation.m_CostItemCount = costItemCount;
					NKCPopupRepeatOperation.m_CostHavingCount = myUserData.m_InventoryData.GetCountMiscItem(num);
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NKCUISlot, false);
			}
			NKCUtil.SetGameobjectActive(this.m_objRepeatCountStop, !nkcrepeatOperaion.GetIsOnGoing());
			NKCUtil.SetGameobjectActive(this.m_objRepeatCountProgress, nkcrepeatOperaion.GetIsOnGoing());
			NKCUtil.SetGameobjectActive(this.m_objStateNone, !nkcrepeatOperaion.GetIsOnGoing());
			NKCUtil.SetGameobjectActive(this.m_objState, nkcrepeatOperaion.GetIsOnGoing());
			NKCUtil.SetGameobjectActive(this.m_objStateProgressingIcon, nkcrepeatOperaion.GetIsOnGoing());
			if (nkcrepeatOperaion.GetIsOnGoing())
			{
				NKCUtil.SetLabelText(this.m_lbRemainRepeatCount, string.Format(NKCUtilString.GET_STRING_REPEAT_OPERATION_REMAIN_REPEAT_COUNT_ONE_PARAM, (nkcrepeatOperaion.GetMaxRepeatCount() - nkcrepeatOperaion.GetCurrRepeatCount()).ToString()));
				NKCUtil.SetLabelText(this.m_lbRepeatCountInProgress, string.Format(NKCUtilString.GET_STRING_REPEAT_OPERATION_COMPLETE_REPEAT_COUNT_ONE_PARAM, nkcrepeatOperaion.GetCurrRepeatCount().ToString()));
				NKCUtil.SetGameobjectActive(this.m_csbtnStart, false);
				NKCUtil.SetGameobjectActive(this.m_csbtnStartDisabled, false);
				NKCUtil.SetGameobjectActive(this.m_csbtnCancel, true);
				NKCUtil.SetGameobjectActive(this.m_csbtnContinue, true);
				NKCUtil.SetGameobjectActive(this.m_csbtnOK, false);
				NKCUtil.SetLabelText(this.m_lbState, NKCUtilString.GET_STRING_REPEAT_OPERATION_IS_ON_GOING);
				this.UpdateProgressTime();
			}
			else
			{
				this.OnClickResetRepeatCount();
				NKCUtil.SetGameobjectActive(this.m_csbtnCancel, false);
				NKCUtil.SetGameobjectActive(this.m_csbtnContinue, false);
				NKCUtil.SetGameobjectActive(this.m_csbtnOK, false);
			}
			this.UpdateCostItemCountUI();
			this.UpdateHavingCountUI();
			List<NKCUISlot.SlotData> list = NKCUISlot.MakeSlotDataListFromReward(nkcrepeatOperaion.GetReward(), false, false);
			if (list.Count == 0)
			{
				this.m_lsrReward.TotalCount = 0;
				this.m_lsrReward.RefreshCells(false);
				NKCUtil.SetGameobjectActive(this.m_objRewardNone, true);
				return;
			}
			this.m_lstList = list;
			this.m_lsrReward.TotalCount = this.m_lstList.Count;
			NKCUtil.SetGameobjectActive(this.m_objRewardNone, false);
			this.m_lsrReward.velocity = new Vector2(0f, 0f);
			this.m_lsrReward.SetIndexPosition(0);
		}

		// Token: 0x0600768C RID: 30348 RVA: 0x0027780F File Offset: 0x00275A0F
		private void SetEquipSlotHaveNoMenu(NKCUISlot cNKCUISlot)
		{
			if (cNKCUISlot.GetSlotData() != null && (cNKCUISlot.GetSlotData().eType == NKCUISlot.eSlotMode.Equip || cNKCUISlot.GetSlotData().eType == NKCUISlot.eSlotMode.EquipCount))
			{
				cNKCUISlot.Set_EQUIP_BOX_BOTTOM_MENU_TYPE(NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_NONE);
			}
		}

		// Token: 0x0600768D RID: 30349 RVA: 0x0027783C File Offset: 0x00275A3C
		private void UpdateCostItemCountUI()
		{
			if (this.m_bCheatMode)
			{
				NKCUtil.SetLabelText(this.m_lbCostCount, "");
				return;
			}
			NKCRepeatOperaion nkcrepeatOperaion = NKCScenManager.GetScenManager().GetNKCRepeatOperaion();
			if (nkcrepeatOperaion == null)
			{
				return;
			}
			if (nkcrepeatOperaion.GetIsOnGoing())
			{
				long num = nkcrepeatOperaion.GetCostIncreaseCount() * (long)NKCPopupRepeatOperation.m_CostItemCount;
				NKCUtil.SetLabelText(this.m_lbCostCount, num.ToString());
				return;
			}
			if (NKCPopupRepeatOperation.m_RepeatCount >= 1L)
			{
				NKCUtil.SetLabelText(this.m_lbCostCount, ((long)NKCPopupRepeatOperation.m_CostItemCount * NKCPopupRepeatOperation.m_RepeatCount).ToString());
				return;
			}
			NKCUtil.SetLabelText(this.m_lbCostCount, NKCPopupRepeatOperation.m_CostItemCount.ToString());
		}

		// Token: 0x0600768E RID: 30350 RVA: 0x002778DC File Offset: 0x00275ADC
		private void UpdateHavingCountUI()
		{
			if (this.m_bCheatMode)
			{
				NKCUtil.SetLabelText(this.m_lbHavingCount, "");
				return;
			}
			if (NKCPopupRepeatOperation.m_RepeatCount >= 1L)
			{
				NKCUtil.SetLabelText(this.m_lbHavingCount, NKCPopupRepeatOperation.m_CostHavingCount.ToString());
				return;
			}
			NKCUtil.SetLabelText(this.m_lbHavingCount, "<color=red>" + NKCPopupRepeatOperation.m_CostHavingCount.ToString() + "</color>");
		}

		// Token: 0x0600768F RID: 30351 RVA: 0x00277948 File Offset: 0x00275B48
		private void UpdateProgressTime()
		{
			NKCRepeatOperaion nkcrepeatOperaion = NKCScenManager.GetScenManager().GetNKCRepeatOperaion();
			if (nkcrepeatOperaion == null)
			{
				return;
			}
			if (!nkcrepeatOperaion.GetIsOnGoing())
			{
				return;
			}
			TimeSpan timeSpan = NKCSynchronizedTime.GetServerUTCTime(0.0) - nkcrepeatOperaion.GetStartTime();
			NKCUtil.SetLabelText(this.m_lbProgressTime, NKCUtilString.GetTimeSpanString(timeSpan));
		}

		// Token: 0x06007690 RID: 30352 RVA: 0x00277998 File Offset: 0x00275B98
		private void UpdateResultTime()
		{
			NKCRepeatOperaion nkcrepeatOperaion = NKCScenManager.GetScenManager().GetNKCRepeatOperaion();
			if (nkcrepeatOperaion == null)
			{
				return;
			}
			TimeSpan prevProgressDuration = nkcrepeatOperaion.GetPrevProgressDuration();
			NKCUtil.SetLabelText(this.m_lbProgressTime, string.Format(NKCUtilString.GET_STRING_REPEAT_OPERATION_RESULT_TOTAL_TIME, NKCUtilString.GetTimeSpanString(prevProgressDuration)));
		}

		// Token: 0x06007691 RID: 30353 RVA: 0x002779D6 File Offset: 0x00275BD6
		private void Update()
		{
			if (NKCPopupRepeatOperation.m_fElapsedTime + 1f < Time.time)
			{
				NKCPopupRepeatOperation.m_fElapsedTime = Time.time;
				this.UpdateProgressTime();
			}
		}

		// Token: 0x06007692 RID: 30354 RVA: 0x002779FC File Offset: 0x00275BFC
		private void OnClickStart()
		{
			if (this.m_bCheatMode)
			{
				return;
			}
			if (this.m_NKCUISlot.gameObject.activeSelf && NKCPopupRepeatOperation.m_CostHavingCount < (long)NKCPopupRepeatOperation.m_CostItemCount)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_REPEAT_OPERATION_COST_MORE_REQUIRED, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			NKM_SCEN_ID nowScenID = NKCScenManager.GetScenManager().GetNowScenID();
			if (nowScenID == NKM_SCEN_ID.NSI_WARFARE_GAME)
			{
				WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
				if (warfareGameData != null)
				{
					if (warfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_STOP)
					{
						if (!NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareGame().OnClickGameStart(true))
						{
							return;
						}
						this.CloseAndStartWithCurrOption();
						return;
					}
					else
					{
						this.CloseAndStartWithCurrOption();
						if (!NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareGame().IsAutoWarfare())
						{
							NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareGame().m_NKCWarfareGameHUD.SendAutoReq(true);
							return;
						}
					}
				}
			}
			else if (nowScenID == NKM_SCEN_ID.NSI_GAME)
			{
				NKCGameClient gameClient = NKCScenManager.GetScenManager().GetGameClient();
				if (gameClient == null)
				{
					return;
				}
				NKMGameRuntimeTeamData myRunTimeTeamData = gameClient.GetMyRunTimeTeamData();
				if (myRunTimeTeamData == null)
				{
					return;
				}
				this.CloseAndStartWithCurrOption();
				if (!myRunTimeTeamData.m_bAutoRespawn)
				{
					gameClient.Send_Packet_GAME_AUTO_RESPAWN_REQ(true, true);
					return;
				}
			}
			else if (nowScenID == NKM_SCEN_ID.NSI_DUNGEON_ATK_READY)
			{
				this.CloseAndStartWithCurrOption();
				NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY().StartByRepeatOperation();
			}
		}

		// Token: 0x06007693 RID: 30355 RVA: 0x00277B14 File Offset: 0x00275D14
		public void CloseAndStartWithCurrOption()
		{
			if (!base.IsOpen)
			{
				return;
			}
			NKCScenManager.GetScenManager().GetNKCRepeatOperaion().SetIsOnGoing(true);
			NKCScenManager.GetScenManager().GetNKCRepeatOperaion().SetNeedToSaveRewardData(true);
			NKCScenManager.GetScenManager().GetNKCRepeatOperaion().SetStartTime(NKCSynchronizedTime.GetServerUTCTime(0.0));
			NKCScenManager.GetScenManager().GetNKCRepeatOperaion().SetCurrRepeatCount(0L);
			NKCScenManager.GetScenManager().GetNKCRepeatOperaion().SetMaxRepeatCount(NKCPopupRepeatOperation.m_RepeatCount);
			NKCScenManager.GetScenManager().GetNKCRepeatOperaion().ResetReward();
			NKCScenManager.GetScenManager().GetNKCRepeatOperaion().UpdateRepeatOperationGameHudUI();
			NKCScenManager.GetScenManager().GetNKCPowerSaveMode().SetEnable(this.m_ctPowerSaveMode.m_bChecked);
			base.Close();
		}

		// Token: 0x06007694 RID: 30356 RVA: 0x00277BCA File Offset: 0x00275DCA
		private void OnClickCancel()
		{
			NKCScenManager.GetScenManager().GetNKCRepeatOperaion().OnClickCancel();
			base.Close();
		}

		// Token: 0x06007695 RID: 30357 RVA: 0x00277BE1 File Offset: 0x00275DE1
		private void OnClickClose()
		{
			base.Close();
		}

		// Token: 0x040062DE RID: 25310
		public EventTrigger m_etBG;

		// Token: 0x040062DF RID: 25311
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x040062E0 RID: 25312
		[Header("왼쪽 UI")]
		public Text m_lbEPTitle;

		// Token: 0x040062E1 RID: 25313
		public Text m_lbEPName;

		// Token: 0x040062E2 RID: 25314
		public static string m_LastEPTitle = "";

		// Token: 0x040062E3 RID: 25315
		public static string m_LastEPName = "";

		// Token: 0x040062E4 RID: 25316
		public GameObject m_objRewardNone;

		// Token: 0x040062E5 RID: 25317
		public GameObject m_objReward;

		// Token: 0x040062E6 RID: 25318
		public LoopScrollRect m_lsrReward;

		// Token: 0x040062E7 RID: 25319
		public GridLayoutGroup m_GridLayoutGroup;

		// Token: 0x040062E8 RID: 25320
		[Header("오른쪽 메뉴")]
		public NKCUIComStateButton m_csbtnStart;

		// Token: 0x040062E9 RID: 25321
		public NKCUIComStateButton m_csbtnStartDisabled;

		// Token: 0x040062EA RID: 25322
		public NKCUIComStateButton m_csbtnCancel;

		// Token: 0x040062EB RID: 25323
		public NKCUIComStateButton m_csbtnContinue;

		// Token: 0x040062EC RID: 25324
		public NKCUIComStateButton m_csbtnOK;

		// Token: 0x040062ED RID: 25325
		public GameObject m_objRepeatCountStop;

		// Token: 0x040062EE RID: 25326
		public NKCUIComStateButton m_csbtnRepeatCountStopReset;

		// Token: 0x040062EF RID: 25327
		public NKCUIComStateButton m_csbtnRepeatCountStopAdd1;

		// Token: 0x040062F0 RID: 25328
		public NKCUIComStateButton m_csbtnRepeatCountStopAdd10;

		// Token: 0x040062F1 RID: 25329
		public NKCUIComStateButton m_csbtnRepeatCountStopAdd100;

		// Token: 0x040062F2 RID: 25330
		public NKCUIComStateButton m_csbtnRepeatCountStopAddMax;

		// Token: 0x040062F3 RID: 25331
		public Text m_lbRepeatCount;

		// Token: 0x040062F4 RID: 25332
		public GameObject m_objRepeatCountProgress;

		// Token: 0x040062F5 RID: 25333
		public Text m_lbRepeatCountInProgress;

		// Token: 0x040062F6 RID: 25334
		public Text m_lbRemainRepeatCount;

		// Token: 0x040062F7 RID: 25335
		public NKCUISlot m_NKCUISlot;

		// Token: 0x040062F8 RID: 25336
		public Text m_lbCostDesc;

		// Token: 0x040062F9 RID: 25337
		public Text m_lbCostCount;

		// Token: 0x040062FA RID: 25338
		public GameObject m_objHavingCount;

		// Token: 0x040062FB RID: 25339
		public Text m_lbHavingCount;

		// Token: 0x040062FC RID: 25340
		public GameObject m_objHavingCountNone;

		// Token: 0x040062FD RID: 25341
		public GameObject m_objStateNone;

		// Token: 0x040062FE RID: 25342
		public GameObject m_objState;

		// Token: 0x040062FF RID: 25343
		public Text m_lbState;

		// Token: 0x04006300 RID: 25344
		public GameObject m_objStateProgressingIcon;

		// Token: 0x04006301 RID: 25345
		public Text m_lbProgressTime;

		// Token: 0x04006302 RID: 25346
		public GameObject m_objPowerSaveMode;

		// Token: 0x04006303 RID: 25347
		public NKCUIComToggle m_ctPowerSaveMode;

		// Token: 0x04006304 RID: 25348
		private NKCPopupRepeatOperation.dOnClose m_dOnClose;

		// Token: 0x04006305 RID: 25349
		private static int m_CostItemCount = 0;

		// Token: 0x04006306 RID: 25350
		private static long m_CostHavingCount = 0L;

		// Token: 0x04006307 RID: 25351
		private static long m_RepeatCount = 0L;

		// Token: 0x04006308 RID: 25352
		private bool m_bFirstOpen = true;

		// Token: 0x04006309 RID: 25353
		private List<NKCUISlot.SlotData> m_lstList = new List<NKCUISlot.SlotData>();

		// Token: 0x0400630A RID: 25354
		private static float m_fElapsedTime = 0f;

		// Token: 0x0400630B RID: 25355
		private bool m_bCheatMode;

		// Token: 0x0400630C RID: 25356
		private int MAX_REPEAT_COUNT_FOR_CHEAT = 3000;

		// Token: 0x0400630D RID: 25357
		[Header("입장 제한")]
		public GameObject m_OPERATION_REPEAT_EnterLimit;

		// Token: 0x0400630E RID: 25358
		public Text m_EnterLimit_TEXT;

		// Token: 0x0400630F RID: 25359
		public GameObject m_BonusType;

		// Token: 0x04006310 RID: 25360
		public Image m_BonusType_Icon;

		// Token: 0x04006311 RID: 25361
		private int m_iCurEpisodeKey;

		// Token: 0x04006312 RID: 25362
		private static NKCPopupRepeatOperation m_Instance = null;

		// Token: 0x020017D7 RID: 6103
		// (Invoke) Token: 0x0600B458 RID: 46168
		public delegate void dOnClose();
	}
}
