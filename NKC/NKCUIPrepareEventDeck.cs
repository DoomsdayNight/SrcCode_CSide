using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using ClientPacket.Common;
using NKC.UI.Fierce;
using NKM;
using NKM.Guild;
using NKM.Shop;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009D0 RID: 2512
	public class NKCUIPrepareEventDeck : NKCUIBase
	{
		// Token: 0x1700125B RID: 4699
		// (get) Token: 0x06006B2B RID: 27435 RVA: 0x0022CB38 File Offset: 0x0022AD38
		public static NKCUIPrepareEventDeck Instance
		{
			get
			{
				if (NKCUIPrepareEventDeck.m_Instance == null)
				{
					NKCUIPrepareEventDeck.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPrepareEventDeck>("ab_ui_nkm_ui_operation", "NKM_UI_OPERATION_EVENTDECK", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPrepareEventDeck.CleanupInstance)).GetInstance<NKCUIPrepareEventDeck>();
					NKCUIPrepareEventDeck.m_Instance.Init();
				}
				return NKCUIPrepareEventDeck.m_Instance;
			}
		}

		// Token: 0x1700125C RID: 4700
		// (get) Token: 0x06006B2C RID: 27436 RVA: 0x0022CB87 File Offset: 0x0022AD87
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIPrepareEventDeck.m_Instance != null && NKCUIPrepareEventDeck.m_Instance.IsOpen;
			}
		}

		// Token: 0x06006B2D RID: 27437 RVA: 0x0022CBA2 File Offset: 0x0022ADA2
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPrepareEventDeck.m_Instance != null && NKCUIPrepareEventDeck.m_Instance.IsOpen)
			{
				NKCUIPrepareEventDeck.m_Instance.Close();
			}
		}

		// Token: 0x06006B2E RID: 27438 RVA: 0x0022CBC7 File Offset: 0x0022ADC7
		private static void CleanupInstance()
		{
			NKCUIPrepareEventDeck.m_Instance = null;
		}

		// Token: 0x1700125D RID: 4701
		// (get) Token: 0x06006B2F RID: 27439 RVA: 0x0022CBCF File Offset: 0x0022ADCF
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x1700125E RID: 4702
		// (get) Token: 0x06006B30 RID: 27440 RVA: 0x0022CBD2 File Offset: 0x0022ADD2
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_EVENT_DECK;
			}
		}

		// Token: 0x06006B31 RID: 27441 RVA: 0x0022CBD9 File Offset: 0x0022ADD9
		public int GetCurrMultiplyRewardCount()
		{
			return this.m_CurrMultiplyRewardCount;
		}

		// Token: 0x06006B32 RID: 27442 RVA: 0x0022CBE1 File Offset: 0x0022ADE1
		public bool GetOperationSkipState()
		{
			return this.m_bOperationSkip;
		}

		// Token: 0x1700125F RID: 4703
		// (get) Token: 0x06006B33 RID: 27443 RVA: 0x0022CBE9 File Offset: 0x0022ADE9
		private bool OperatorEnabled
		{
			get
			{
				return NKMContentsVersionManager.HasTag("OPERATOR");
			}
		}

		// Token: 0x17001260 RID: 4704
		// (get) Token: 0x06006B34 RID: 27444 RVA: 0x0022CBF5 File Offset: 0x0022ADF5
		private bool OperatorUnlocked
		{
			get
			{
				return this.OperatorEnabled && NKCContentManager.IsContentsUnlocked(ContentsType.OPERATOR, 0, 0);
			}
		}

		// Token: 0x17001261 RID: 4705
		// (get) Token: 0x06006B35 RID: 27445 RVA: 0x0022CC0C File Offset: 0x0022AE0C
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				if (this.m_eventDeckContents == DeckContents.SHADOW_PALACE)
				{
					return new List<int>
					{
						1,
						19,
						20
					};
				}
				if (this.m_StageTemplet != null)
				{
					List<int> list = new List<int>();
					NKMEpisodeTempletV2 episodeTemplet = this.m_StageTemplet.EpisodeTemplet;
					if (episodeTemplet != null && episodeTemplet.ResourceIdList != null && episodeTemplet.ResourceIdList.Count > 0)
					{
						list = episodeTemplet.ResourceIdList;
					}
					if (!list.Contains(this.m_iCostItemID))
					{
						list.Add(this.m_iCostItemID);
					}
					return list;
				}
				if (this.m_iCostItemCount == 0 || this.m_iCostItemID == 0 || base.UpsideMenuShowResourceList.Contains(this.m_iCostItemID))
				{
					return new List<int>
					{
						1,
						2,
						3,
						101
					};
				}
				List<int> list2 = new List<int>();
				list2.Add(this.m_iCostItemID);
				list2.AddRange(base.UpsideMenuShowResourceList);
				return list2;
			}
		}

		// Token: 0x06006B36 RID: 27446 RVA: 0x0022CCF8 File Offset: 0x0022AEF8
		public override void CloseInternal()
		{
			this.m_dicSelectedUnits.Clear();
			this.CloseShipIllust();
			this.m_SelectedShipUid = 0L;
			this.m_StageTemplet = null;
			this.m_currentDungeonTempletBase = null;
			this.m_currentDeckTemplet = null;
			base.gameObject.SetActive(false);
			this.ResetDrag();
			this.m_oldSlotType = NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE;
			if (NKCUIUnitSelectList.IsInstanceLoaded)
			{
				NKCUIUnitSelectList.Instance.ClearCachOption();
			}
		}

		// Token: 0x06006B37 RID: 27447 RVA: 0x0022CD5D File Offset: 0x0022AF5D
		public override void Hide()
		{
			base.Hide();
			this.ResetDrag();
		}

		// Token: 0x06006B38 RID: 27448 RVA: 0x0022CD6B File Offset: 0x0022AF6B
		public override void OnBackButton()
		{
			if (this.dOnBackButtonEvent != null)
			{
				this.dOnBackButtonEvent();
				return;
			}
			base.OnBackButton();
		}

		// Token: 0x06006B39 RID: 27449 RVA: 0x0022CD88 File Offset: 0x0022AF88
		public void Init()
		{
			foreach (NKCUIUnitSelectListEventDeckSlot nkcuiunitSelectListEventDeckSlot in this.m_lstSlot)
			{
				if (nkcuiunitSelectListEventDeckSlot != null)
				{
					nkcuiunitSelectListEventDeckSlot.Init(false);
					nkcuiunitSelectListEventDeckSlot.SetDragHandler(new NKCUIUnitSelectListEventDeckSlot.DragHandler(this.OnDragBegin), new NKCUIUnitSelectListEventDeckSlot.DragHandler(this.OnDrag), new NKCUIUnitSelectListEventDeckSlot.DragHandler(this.OnDragEnd), new NKCUIUnitSelectListEventDeckSlot.DragHandler(this.OnDrop));
				}
			}
			this.m_cbtnBegin.PointerClick.RemoveAllListeners();
			this.m_cbtnBegin.PointerClick.AddListener(new UnityAction(this.OnBtnBegin));
			this.m_cbtnChngeShip.PointerClick.RemoveAllListeners();
			this.m_cbtnChngeShip.PointerClick.AddListener(new UnityAction(this.OpenShipSelectList));
			this.m_cbtnAutoSetup.PointerClick.RemoveAllListeners();
			this.m_cbtnAutoSetup.PointerClick.AddListener(new UnityAction(this.AutoPrepare));
			this.m_cbtnClearAll.PointerClick.RemoveAllListeners();
			this.m_cbtnClearAll.PointerClick.AddListener(new UnityAction(this.ClearAll));
			if (null != this.m_tglSkip)
			{
				this.m_tglSkip.OnValueChanged.RemoveAllListeners();
				this.m_tglSkip.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickSkip));
			}
			this.m_CurrMultiplyRewardCount = 1;
			if (this.m_NKCUIOperationSkip != null)
			{
				this.m_NKCUIOperationSkip.Init(new NKCUIOperationSkip.OnCountUpdated(this.OnOperationSkipUpdated), new UnityAction(this.OnClickOperationSkipClose));
			}
			this.m_NKCUIComEnemyList.InitUI();
			this.m_OperatorSlot.Init(new NKCUIOperatorDeckSlot.OnSelectOperator(this.OnClickOperatorSlot));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnCanChangeOperator, new UnityAction(this.OpenOperatorSelectList));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnLeaderSelect, new UnityAction(this.OnClickLeaderSelect));
			base.gameObject.SetActive(false);
		}

		// Token: 0x06006B3A RID: 27450 RVA: 0x0022CF98 File Offset: 0x0022B198
		private void SetMultiplyRewardCountUIData()
		{
			NKMRewardMultiplyTemplet.GetCostItem(NKMRewardMultiplyTemplet.ScopeType.General);
			NKMStageTempletV2 stageTemplet = this.m_StageTemplet;
			if (stageTemplet != null && stageTemplet.EnterLimit > 0)
			{
				int statePlayCnt = NKCScenManager.CurrentUserData().GetStatePlayCnt(stageTemplet.Key, false, false, false);
				int enterLimit = stageTemplet.EnterLimit;
			}
			int num = 0;
			if (this.m_StageTemplet != null)
			{
				int stageReqItemID = this.m_StageTemplet.m_StageReqItemID;
				num = this.m_StageTemplet.m_StageReqItemCount;
				if (stageReqItemID == 2)
				{
					NKCCompanyBuff.SetDiscountOfEterniumInEnteringDungeon(NKCScenManager.CurrentUserData().m_companyBuffDataList, ref num);
				}
			}
		}

		// Token: 0x06006B3B RID: 27451 RVA: 0x0022D010 File Offset: 0x0022B210
		private void SetSkipCountUIData()
		{
			int maxCount = 99;
			NKMStageTempletV2 stageTemplet = this.m_StageTemplet;
			if (stageTemplet != null && stageTemplet.EnterLimit > 0)
			{
				int statePlayCnt = NKCScenManager.CurrentUserData().GetStatePlayCnt(stageTemplet.Key, false, false, false);
				maxCount = stageTemplet.EnterLimit - statePlayCnt;
			}
			int num = 0;
			int dungeonCostItemCount = 0;
			if (this.m_StageTemplet != null)
			{
				num = this.m_StageTemplet.m_StageReqItemID;
				dungeonCostItemCount = this.m_StageTemplet.m_StageReqItemCount;
				if (num == 2)
				{
					NKCCompanyBuff.SetDiscountOfEterniumInEnteringDungeon(NKCScenManager.CurrentUserData().m_companyBuffDataList, ref dungeonCostItemCount);
				}
			}
			this.m_NKCUIOperationSkip.SetData(NKMCommonConst.SkipCostMiscItemId, NKMCommonConst.SkipCostMiscItemCount, num, dungeonCostItemCount, this.m_CurrMultiplyRewardCount, 1, maxCount);
		}

		// Token: 0x06006B3C RID: 27452 RVA: 0x0022D0AA File Offset: 0x0022B2AA
		private void OnOperationMultiplyUpdated(int newCount)
		{
			this.m_CurrMultiplyRewardCount = newCount;
			this.UpdateAttackCost();
		}

		// Token: 0x06006B3D RID: 27453 RVA: 0x0022D0B9 File Offset: 0x0022B2B9
		private void OnOperationSkipUpdated(int newCount)
		{
			this.m_CurrMultiplyRewardCount = newCount;
			this.UpdateAttackCost();
		}

		// Token: 0x06006B3E RID: 27454 RVA: 0x0022D0C8 File Offset: 0x0022B2C8
		private void OnClickOperationSkipClose()
		{
			this.m_tglSkip.Select(false, false, false);
		}

		// Token: 0x06006B3F RID: 27455 RVA: 0x0022D0DC File Offset: 0x0022B2DC
		public void Open(NKMStageTempletV2 stageTemplet, NKMDungeonTempletBase dungeonTempletBase, NKCUIPrepareEventDeck.OnEventDeckConfirm onEventDeckConfirm, NKCUIPrepareEventDeck.OnBackButtonEvent onBackButtonEvent = null, DeckContents eventDeckContents = DeckContents.NORMAL)
		{
			this.m_dicSelectedUnits.Clear();
			this.m_SelectedShipUid = 0L;
			this.m_SelectedOperatorUid = 0L;
			this.m_CurrMultiplyRewardCount = 1;
			this.dOnEventDeckConfirm = onEventDeckConfirm;
			this.dOnBackButtonEvent = onBackButtonEvent;
			this.m_eventDeckContents = eventDeckContents;
			this.ClearBCUntSlots();
			this.m_currentLeaderIndex = 0;
			this.m_isSelectingLeader = false;
			NKCUIComStateButton csbtnLeaderSelect = this.m_csbtnLeaderSelect;
			if (csbtnLeaderSelect != null)
			{
				csbtnLeaderSelect.Select(false, false, false);
			}
			if (stageTemplet == null && dungeonTempletBase == null)
			{
				return;
			}
			this.m_StageTemplet = stageTemplet;
			this.m_currentDungeonTempletBase = dungeonTempletBase;
			NKMDungeonEventDeckTemplet nkmdungeonEventDeckTemplet = null;
			if (eventDeckContents == DeckContents.PHASE)
			{
				if (stageTemplet.PhaseTemplet != null)
				{
					nkmdungeonEventDeckTemplet = stageTemplet.PhaseTemplet.EventDeckTemplet;
				}
			}
			else if (dungeonTempletBase != null)
			{
				nkmdungeonEventDeckTemplet = dungeonTempletBase.EventDeckTemplet;
			}
			if (nkmdungeonEventDeckTemplet == null)
			{
				Debug.LogError("Dungeon does not using eventDeck");
				return;
			}
			base.gameObject.SetActive(true);
			this.OnClickOperationSkipClose();
			this.m_currentDeckTemplet = nkmdungeonEventDeckTemplet;
			this.InitEventDeckData(this.m_currentDeckTemplet);
			if (stageTemplet != null)
			{
				NKCUtil.SetLabelText(this.m_lbTitle, "");
				NKCUtil.SetLabelText(this.m_lbSubTitle, stageTemplet.GetDungeonName());
			}
			else if (this.m_currentDungeonTempletBase != null)
			{
				NKCUtil.SetLabelText(this.m_lbTitle, "");
				NKCUtil.SetLabelText(this.m_lbSubTitle, dungeonTempletBase.GetDungeonName());
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbTitle, "");
				NKCUtil.SetLabelText(this.m_lbSubTitle, "");
			}
			this.LoadDungeonDeck();
			this.RecalculateDeckAllConditionCache();
			this.UpdateConditionUI();
			this.UpdateAttackCost();
			this.SetVisibleMultiplyRewardUI();
			this.SetUIByContents();
			this.SetEnemyList();
			this.ResetDrag();
			this.SetBattleCondition();
			if (!this.SetAsLeader(this.m_currentLeaderIndex))
			{
				this.SetDefaultLeader();
			}
			base.UIOpened(true);
			this.CheckTutorial();
		}

		// Token: 0x06006B40 RID: 27456 RVA: 0x0022D284 File Offset: 0x0022B484
		private void SetVisibleMultiplyRewardUI()
		{
			bool flag = NKCContentManager.IsContentsUnlocked(ContentsType.OPERATION_MULTIPLY, 0, 0);
			bool flag2 = false;
			bool flag3 = true;
			if (this.m_StageTemplet != null)
			{
				NKMEpisodeTempletV2 episodeTemplet = this.m_StageTemplet.EpisodeTemplet;
				if (episodeTemplet != null)
				{
					switch (episodeTemplet.m_EPCategory)
					{
					case EPISODE_CATEGORY.EC_MAINSTREAM:
					case EPISODE_CATEGORY.EC_DAILY:
					case EPISODE_CATEGORY.EC_SIDESTORY:
					case EPISODE_CATEGORY.EC_FIELD:
					case EPISODE_CATEGORY.EC_EVENT:
					case EPISODE_CATEGORY.EC_SUPPLY:
					case EPISODE_CATEGORY.EC_CHALLENGE:
						flag2 = true;
						goto IL_5F;
					}
					flag2 = false;
				}
				IL_5F:
				NKMDungeonTempletBase currentDungeonTempletBase = this.m_currentDungeonTempletBase;
				if (currentDungeonTempletBase != null)
				{
					flag3 = (currentDungeonTempletBase.m_RewardMultiplyMax > 1);
				}
			}
			bool flag4 = true;
			if (this.m_StageTemplet != null)
			{
				if (this.m_StageTemplet.m_STAGE_TYPE == STAGE_TYPE.ST_PHASE)
				{
					flag4 = false;
				}
				if (this.m_StageTemplet.m_bNoAutoRepeat)
				{
					flag4 = false;
				}
			}
			bool flag5 = this.m_StageTemplet != null && this.m_StageTemplet.m_bActiveBattleSkip;
			NKCUtil.SetGameobjectActive(this.m_objSkip, flag && flag2 && flag3 && flag4 && flag5);
		}

		// Token: 0x06006B41 RID: 27457 RVA: 0x0022D360 File Offset: 0x0022B560
		private bool CheckMultiply(bool bMsg)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null || this.m_currentDungeonTempletBase == null)
			{
				return false;
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.OPERATION_MULTIPLY, 0, 0))
			{
				return false;
			}
			if (!NKCUtil.IsCanStartEterniumStage(this.m_StageTemplet, true))
			{
				return false;
			}
			if (!nkmuserData.IsSuperUser())
			{
				if (!nkmuserData.CheckDungeonClear(this.m_currentDungeonTempletBase.m_DungeonStrID))
				{
					if (bMsg)
					{
						NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_MULTIPLY_OPERATION_MEDAL_COND, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					}
					return false;
				}
				NKMDungeonClearData dungeonClearData = nkmuserData.GetDungeonClearData(this.m_currentDungeonTempletBase.m_DungeonStrID);
				if (dungeonClearData == null || !dungeonClearData.missionResult1 || !dungeonClearData.missionResult2)
				{
					if (bMsg)
					{
						NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_MULTIPLY_OPERATION_MEDAL_COND, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					}
					return false;
				}
			}
			if (this.m_currentDungeonTempletBase.m_RewardMultiplyMax <= 1)
			{
				return false;
			}
			if (this.m_StageTemplet.EnterLimit > 0)
			{
				int statePlayCnt = NKCScenManager.CurrentUserData().GetStatePlayCnt(this.m_StageTemplet.Key, false, false, false);
				if (this.m_StageTemplet.EnterLimit - statePlayCnt <= 1)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006B42 RID: 27458 RVA: 0x0022D458 File Offset: 0x0022B658
		private bool CheckSkip(bool bMsg)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null || this.m_currentDungeonTempletBase == null)
			{
				return false;
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.OPERATION_MULTIPLY, 0, 0))
			{
				return false;
			}
			if (!NKCUtil.IsCanStartEterniumStage(this.m_StageTemplet, true))
			{
				return false;
			}
			if (!nkmuserData.IsSuperUser())
			{
				if (!nkmuserData.CheckDungeonClear(this.m_currentDungeonTempletBase.m_DungeonStrID))
				{
					if (bMsg)
					{
						NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_MULTIPLY_OPERATION_MEDAL_COND, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					}
					return false;
				}
				NKMDungeonClearData dungeonClearData = nkmuserData.GetDungeonClearData(this.m_currentDungeonTempletBase.m_DungeonStrID);
				if (dungeonClearData == null || !dungeonClearData.missionResult1 || !dungeonClearData.missionResult2)
				{
					if (bMsg)
					{
						NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_MULTIPLY_OPERATION_MEDAL_COND, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					}
					return false;
				}
			}
			return this.m_currentDungeonTempletBase.m_RewardMultiplyMax > 1;
		}

		// Token: 0x06006B43 RID: 27459 RVA: 0x0022D518 File Offset: 0x0022B718
		private void OnClickSkip(bool bSet)
		{
			if (bSet)
			{
				if (!this.CheckSkip(true))
				{
					this.m_tglSkip.Select(false, false, false);
					return;
				}
				this.m_bOperationSkip = true;
				this.UpdateAttackCost();
				this.SetSkipCountUIData();
			}
			NKCUtil.SetGameobjectActive(this.m_NKCUIOperationSkip, bSet);
			if (!bSet)
			{
				this.m_CurrMultiplyRewardCount = 1;
				this.m_bOperationSkip = false;
				this.UpdateAttackCost();
				this.SetSkipCountUIData();
			}
		}

		// Token: 0x06006B44 RID: 27460 RVA: 0x0022D580 File Offset: 0x0022B780
		public void UpdateEnterLimitUI()
		{
			bool bValue = false;
			if (this.m_StageTemplet != null && this.m_StageTemplet.EnterLimit > 0)
			{
				bValue = true;
				NKCUtil.SetGameobjectActive(this.m_AB_UI_NKM_UI_OPERATION_EVENTDECK_EnterLimit, true);
				int statePlayCnt = NKCScenManager.CurrentUserData().GetStatePlayCnt(this.m_StageTemplet.Key, false, false, false);
				string msg;
				switch (this.m_StageTemplet.EnterLimitCond)
				{
				case SHOP_RESET_TYPE.DAY:
					msg = string.Format(NKCUtilString.GET_STRING_POPUP_DUNGEON_ENTER_LIMIT_DESC_DAY_02, this.m_StageTemplet.EnterLimit - statePlayCnt, this.m_StageTemplet.EnterLimit);
					break;
				case SHOP_RESET_TYPE.WEEK:
				case SHOP_RESET_TYPE.WEEK_SUN:
				case SHOP_RESET_TYPE.WEEK_MON:
				case SHOP_RESET_TYPE.WEEK_TUE:
				case SHOP_RESET_TYPE.WEEK_WED:
				case SHOP_RESET_TYPE.WEEK_THU:
				case SHOP_RESET_TYPE.WEEK_FRI:
				case SHOP_RESET_TYPE.WEEK_SAT:
					msg = string.Format(NKCUtilString.GET_STRING_POPUP_DUNGEON_ENTER_LIMIT_DESC_WEEK_02, this.m_StageTemplet.EnterLimit - statePlayCnt, this.m_StageTemplet.EnterLimit);
					break;
				case SHOP_RESET_TYPE.MONTH:
					msg = string.Format(NKCUtilString.GET_STRING_POPUP_DUNGEON_ENTER_LIMIT_DESC_MONTH_02, this.m_StageTemplet.EnterLimit - statePlayCnt, this.m_StageTemplet.EnterLimit);
					break;
				default:
					msg = string.Format(NKCUtilString.GET_STRING_POPUP_DUNGEON_ENTER_LIMIT_DESC_DAY_02, this.m_StageTemplet.EnterLimit - statePlayCnt, this.m_StageTemplet.EnterLimit);
					break;
				}
				NKCUtil.SetLabelText(this.m_EnterLimit_TEXT, msg);
				if (this.m_StageTemplet.EnterLimit - statePlayCnt <= 0)
				{
					NKCUtil.SetLabelTextColor(this.m_EnterLimit_TEXT, Color.red);
					this.m_cbtnBegin.PointerClick.RemoveAllListeners();
					this.m_cbtnBegin.PointerClick.AddListener(new UnityAction(this.ConfirmResetStagePlayCnt));
					NKCUtil.SetLabelText(this.m_AB_UI_NKM_UI_OPERATION_EVENTDECK_BUTTON_TEXT, NKCUtilString.GET_STRING_WARFARE_GAME_HUD_OPERATION_RESTORE);
					NKCUtil.SetImageSprite(this.m_AB_UI_NKM_UI_OPERATION_EVENTDECK_BUTTON_ICON, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX_SPRITE", "NKM_UI_COMMON_ICON_ENTERLIMIT_RECOVER_SMALL", false), false);
				}
				else
				{
					NKCUtil.SetLabelTextColor(this.m_EnterLimit_TEXT, Color.white);
					this.m_cbtnBegin.PointerClick.RemoveAllListeners();
					this.m_cbtnBegin.PointerClick.AddListener(new UnityAction(this.OnBtnBegin));
					NKCUtil.SetLabelText(this.m_AB_UI_NKM_UI_OPERATION_EVENTDECK_BUTTON_TEXT, NKCUtilString.GET_STRING_WARFARE_GAME_HUD_OPERATION_START);
					NKCUtil.SetImageSprite(this.m_AB_UI_NKM_UI_OPERATION_EVENTDECK_BUTTON_ICON, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX_SPRITE", "NKM_UI_COMMON_ICON_GAUNTLET", false), false);
				}
			}
			else
			{
				NKCUtil.SetLabelTextColor(this.m_EnterLimit_TEXT, Color.white);
				this.m_cbtnBegin.PointerClick.RemoveAllListeners();
				this.m_cbtnBegin.PointerClick.AddListener(new UnityAction(this.OnBtnBegin));
				NKCUtil.SetLabelText(this.m_AB_UI_NKM_UI_OPERATION_EVENTDECK_BUTTON_TEXT, NKCUtilString.GET_STRING_WARFARE_GAME_HUD_OPERATION_START);
				NKCUtil.SetImageSprite(this.m_AB_UI_NKM_UI_OPERATION_EVENTDECK_BUTTON_ICON, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX_SPRITE", "NKM_UI_COMMON_ICON_GAUNTLET", false), false);
			}
			NKCUtil.SetGameobjectActive(this.m_AB_UI_NKM_UI_OPERATION_EVENTDECK_EnterLimit, bValue);
		}

		// Token: 0x06006B45 RID: 27461 RVA: 0x0022D840 File Offset: 0x0022BA40
		private void SetDeckConditionUI(NKMDungeonEventDeckTemplet eventDeckTemplet)
		{
			if (eventDeckTemplet.m_DeckCondition == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objRootDeckCondition, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objRootDeckCondition, true);
			int num = eventDeckTemplet.m_DeckCondition.ConditionCount + eventDeckTemplet.m_DeckCondition.m_dicGameCondition.Count;
			while (this.m_lstConditionSlots.Count < num)
			{
				NKCUIPrepareEventDeckConditionSlot item = UnityEngine.Object.Instantiate<NKCUIPrepareEventDeckConditionSlot>(this.m_pfbConditionSlot, this.m_rtConditionSlotRoot);
				this.m_lstConditionSlots.Add(item);
			}
			List<NKMDeckCondition.SingleCondition> list = new List<NKMDeckCondition.SingleCondition>(eventDeckTemplet.m_DeckCondition.AllConditionEnumerator());
			List<NKMDeckCondition.GameCondition> list2 = new List<NKMDeckCondition.GameCondition>(eventDeckTemplet.m_DeckCondition.m_dicGameCondition.Values);
			for (int i = 0; i < this.m_lstConditionSlots.Count; i++)
			{
				if (i < list.Count)
				{
					int teamTotalCount;
					if (this.m_dicAllDeckValueCache.TryGetValue(list[i].eCondition, out teamTotalCount))
					{
						this.m_lstConditionSlots[i].SetCondition(list[i], teamTotalCount);
					}
					else
					{
						this.m_lstConditionSlots[i].SetCondition(list[i]);
					}
					NKCUtil.SetGameobjectActive(this.m_lstConditionSlots[i].gameObject, true);
				}
				else if (list.Count <= i && i < list.Count + list2.Count)
				{
					this.m_lstConditionSlots[i].SetCondition(list2[i - list.Count]);
					NKCUtil.SetGameobjectActive(this.m_lstConditionSlots[i].gameObject, true);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lstConditionSlots[i].gameObject, false);
				}
			}
		}

		// Token: 0x06006B46 RID: 27462 RVA: 0x0022D9E8 File Offset: 0x0022BBE8
		private void SetUIByContents()
		{
			switch (this.m_eventDeckContents)
			{
			case DeckContents.SHADOW_PALACE:
			{
				NKCUtil.SetGameobjectActive(this.m_objNormalInfo, true);
				NKCUtil.SetGameobjectActive(this.m_objGuildCoopInfo, false);
				NKCUtil.SetImageColor(this.m_imgInfoBG, this.m_ShadowColor);
				Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_OPERATION_SPRITE", "NKM_UI_OPERATION_EVENTDECK_TITLE_DECO_SHADOW", false);
				NKCUtil.SetImageSprite(this.m_imgTitleDeco, orLoadAssetResource, false);
				orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_WORLD_MAP_SHADOW_BG", "AB_UI_SHADOW_BG", false);
				NKCUtil.SetImageSprite(this.m_imgDeckBG, orLoadAssetResource, false);
				NKCUtil.SetGameobjectActive(this.m_objBlackBG, true);
				NKCUtil.SetGameobjectActive(this.m_objShadowDust, true);
				return;
			}
			case DeckContents.FIERCE_BATTLE_SUPPORT:
			{
				NKCUtil.SetGameobjectActive(this.m_objNormalInfo, true);
				NKCUtil.SetGameobjectActive(this.m_objGuildCoopInfo, false);
				NKCUtil.SetImageColor(this.m_imgInfoBG, this.m_FierceColor);
				Sprite orLoadAssetResource2 = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_OPERATION_SPRITE", "NKM_UI_OPERATION_EVENTDECK_TITLE_DECO_FIERCE", false);
				NKCUtil.SetImageSprite(this.m_imgTitleDeco, orLoadAssetResource2, false);
				bool bNightMareMode = false;
				NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
				if (nkcfierceBattleSupportDataMgr != null)
				{
					bNightMareMode = (nkcfierceBattleSupportDataMgr.GetSelfPenalty().Count > 0);
				}
				NKCUtil.SetImageSprite(this.m_imgDeckBG, NKCUtil.GetSpriteFierceBattleBackgroud(bNightMareMode), false);
				NKCUtil.SetGameobjectActive(this.m_objBlackBG, false);
				NKCUtil.SetGameobjectActive(this.m_objShadowDust, false);
				return;
			}
			case DeckContents.GUILD_COOP:
			{
				NKCUtil.SetGameobjectActive(this.m_objNormalInfo, false);
				NKCUtil.SetGameobjectActive(this.m_objGuildCoopInfo, true);
				GuildSeasonTemplet guildSeasonTemplet = GuildDungeonTempletManager.GetGuildSeasonTemplet(NKCGuildCoopManager.m_SeasonId);
				if (guildSeasonTemplet == null || this.m_currentDungeonTempletBase == null)
				{
					return;
				}
				GuildDungeonInfoTemplet guildDungeonInfoTemplet = GuildDungeonTempletManager.GetDungeonInfoList(guildSeasonTemplet.GetSeasonDungeonGroup()).Find((GuildDungeonInfoTemplet x) => x.GetSeasonDungeonId() == this.m_currentDungeonTempletBase.m_DungeonID);
				if (guildDungeonInfoTemplet != null)
				{
					NKCUtil.SetLabelText(this.m_lbTitleGuildCoop, this.m_currentDungeonTempletBase.GetDungeonName());
					NKCUtil.SetLabelText(this.m_lbArenaNum, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_DUNGEON_DUNGEON_UI_ARENA_INFO, guildDungeonInfoTemplet.GetArenaIndex()));
					NKCUtil.SetLabelText(this.m_lbNextArtifactCount, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_DUNGEON_ARTIFACT_DUNGEON_CHALLENGE_INFO, NKCGuildCoopManager.GetCurrentArtifactCountByArena(guildDungeonInfoTemplet.GetArenaIndex()) + 1));
					float clearPointPercentage = NKCGuildCoopManager.GetClearPointPercentage(guildDungeonInfoTemplet.GetArenaIndex());
					NKCUtil.SetLabelText(this.m_lbClearPercent, string.Format("{0}%", (clearPointPercentage * 100f).ToString("N0")));
					this.m_imgClearGauge.fillAmount = clearPointPercentage;
					return;
				}
				return;
			}
			}
			NKCUtil.SetGameobjectActive(this.m_objNormalInfo, true);
			NKCUtil.SetGameobjectActive(this.m_objGuildCoopInfo, false);
			NKCUtil.SetImageColor(this.m_imgInfoBG, this.m_NormalColor);
			Sprite orLoadAssetResource3 = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_OPERATION_SPRITE", "NKM_UI_OPERATION_EVENTDECK_TITLE_DECO", false);
			NKCUtil.SetImageSprite(this.m_imgTitleDeco, orLoadAssetResource3, false);
			orLoadAssetResource3 = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_BG_TEXTURE", "AB_UI_BG_2", false);
			NKCUtil.SetImageSprite(this.m_imgDeckBG, orLoadAssetResource3, false);
			NKCUtil.SetGameobjectActive(this.m_objBlackBG, false);
			NKCUtil.SetGameobjectActive(this.m_objShadowDust, false);
		}

		// Token: 0x06006B47 RID: 27463 RVA: 0x0022DCAC File Offset: 0x0022BEAC
		private void SetEnemyList()
		{
			NKCUtil.SetGameobjectActive(this.m_NKCUIComEnemyList, this.m_eventDeckContents != DeckContents.FIERCE_BATTLE_SUPPORT);
			NKCUtil.SetGameobjectActive(this.m_AB_UI_NKM_UI_OPERATION_EVENTDECK_ENEMY_FIERCE_BATTLE, this.m_eventDeckContents == DeckContents.FIERCE_BATTLE_SUPPORT);
			if (this.m_eventDeckContents == DeckContents.FIERCE_BATTLE_SUPPORT)
			{
				using (IEnumerator<NKMFierceBossGroupTemplet> enumerator = NKMFierceBossGroupTemplet.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						NKMFierceBossGroupTemplet nkmfierceBossGroupTemplet = enumerator.Current;
						if (nkmfierceBossGroupTemplet.DungeonID == this.m_currentDungeonTempletBase.m_DungeonID)
						{
							Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_fierce_battle_boss_thumbnail", nkmfierceBossGroupTemplet.UI_BossFaceSlot, false);
							NKCUtil.SetImageSprite(this.m_FIERCE_BATTLE_BOSS_IMAGE_Root, orLoadAssetResource, false);
							NKMDungeonTemplet dungeonTemplet = NKMDungeonManager.GetDungeonTemplet(this.m_currentDungeonTempletBase.m_DungeonStrID);
							if (dungeonTemplet != null)
							{
								NKCUtil.SetLabelText(this.m_BOSS_LEVEL_Text, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, this.m_currentDungeonTempletBase.m_DungeonLevel));
								NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(dungeonTemplet.m_BossUnitStrID);
								if (unitTempletBase != null)
								{
									NKCUtil.SetImageSprite(this.m_CLASS_Icon, NKCResourceUtility.GetOrLoadUnitRoleIcon(unitTempletBase, false), false);
									NKCUtil.SetLabelText(this.m_CLASS_Text, NKCUtilString.GetRoleText(unitTempletBase));
								}
								NKCUtil.SetGameobjectActive(this.m_FIERCE_BATTLE_BOSS_INFO, unitTempletBase != null);
								break;
							}
							break;
						}
					}
					return;
				}
			}
			if (this.m_StageTemplet != null)
			{
				this.m_NKCUIComEnemyList.SetData(this.m_StageTemplet);
				return;
			}
			this.m_NKCUIComEnemyList.SetData(this.m_currentDungeonTempletBase);
		}

		// Token: 0x06006B48 RID: 27464 RVA: 0x0022DE10 File Offset: 0x0022C010
		private void InitEventDeckData(NKMDungeonEventDeckTemplet eventDeckTemplet)
		{
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				NKCUIUnitSelectListEventDeckSlot nkcuiunitSelectListEventDeckSlot = this.m_lstSlot[i];
				NKMDungeonEventDeckTemplet.EventDeckSlot unitSlot = eventDeckTemplet.GetUnitSlot(i);
				nkcuiunitSelectListEventDeckSlot.InitEventSlot(unitSlot, i, true, new NKCUIUnitSelectListEventDeckSlot.OnSelectEventDeckSlot(this.OnEventDeckSlotSelect), new NKCUIUnitSelectListEventDeckSlot.OnUnitDetail(this.OpenUnitData));
				nkcuiunitSelectListEventDeckSlot.ClearTouchHoldEvent();
			}
			this.m_cbtnChngeShip.dOnPointerHolding = null;
			switch (eventDeckTemplet.ShipSlot.m_eType)
			{
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE:
				this.SetShipData(null, 0);
				NKCUtil.SetGameobjectActive(this.m_cbtnChngeShip, true);
				goto IL_176;
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FIXED:
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(eventDeckTemplet.ShipSlot.m_ID);
				this.CloseShipIllust();
				this.m_lbShipLevel.text = "";
				this.m_lbShipName.text = string.Format(NKCUtilString.GET_STRING_EVENT_DECK_FIXED_TWO_PARAM, unitTempletBase.m_StarGradeMax, unitTempletBase.GetUnitName());
				NKCUtil.SetGameobjectActive(this.m_cbtnChngeShip, true);
				goto IL_176;
			}
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_GUEST:
			{
				NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(eventDeckTemplet.ShipSlot.m_ID);
				this.SetShipData(unitTempletBase2, eventDeckTemplet.ShipSlot.m_Level);
				NKCUtil.SetGameobjectActive(this.m_cbtnChngeShip, true);
				goto IL_176;
			}
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_NPC:
			{
				NKMUnitTempletBase unitTempletBase3 = NKMUnitManager.GetUnitTempletBase(eventDeckTemplet.ShipSlot.m_ID);
				this.SetShipData(unitTempletBase3, eventDeckTemplet.ShipSlot.m_Level);
				NKCUtil.SetGameobjectActive(this.m_cbtnChngeShip, false);
				goto IL_176;
			}
			}
			Debug.LogError("invalid ship slot setup!");
			IL_176:
			if (this.OperatorEnabled)
			{
				if (this.m_OperatorSlot != null && this.m_OperatorSlot.m_NKM_UI_OPERATOR_DECK_SLOT != null)
				{
					this.m_OperatorSlot.m_NKM_UI_OPERATOR_DECK_SLOT.dOnPointerHolding = null;
				}
				NKCUtil.SetGameobjectActive(this.m_OperatorSlot, true);
				NKCUtil.SetGameobjectActive(this.m_OperatorSkill, true);
				NKCUtil.SetGameobjectActive(this.m_OperatorSkillCombo, true);
				switch (eventDeckTemplet.OperatorSlot.m_eType)
				{
				case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_CLOSED:
					this.SetOperatorLock();
					NKCUtil.SetGameobjectActive(this.m_csbtnCanChangeOperator, false);
					return;
				case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE:
					if (this.OperatorUnlocked)
					{
						this.SetOperatorData(null, 0, 0);
						NKCUtil.SetGameobjectActive(this.m_csbtnCanChangeOperator, true);
						return;
					}
					this.SetOperatorLock();
					NKCUtil.SetGameobjectActive(this.m_csbtnCanChangeOperator, false);
					return;
				case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FIXED:
					if (this.OperatorUnlocked)
					{
						NKMUnitTempletBase unitTempletBase4 = NKMUnitManager.GetUnitTempletBase(eventDeckTemplet.OperatorSlot.m_ID);
						this.SetOperatorData(unitTempletBase4, 0, 0);
						NKCUtil.SetGameobjectActive(this.m_csbtnCanChangeOperator, true);
						return;
					}
					this.SetOperatorLock();
					NKCUtil.SetGameobjectActive(this.m_csbtnCanChangeOperator, false);
					return;
				case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_GUEST:
				{
					NKMUnitTempletBase unitTempletBase5 = NKMUnitManager.GetUnitTempletBase(eventDeckTemplet.OperatorSlot.m_ID);
					this.SetOperatorData(unitTempletBase5, eventDeckTemplet.OperatorSlot.m_Level, eventDeckTemplet.OperatorSubSkillID);
					NKCUtil.SetGameobjectActive(this.m_csbtnCanChangeOperator, this.OperatorUnlocked);
					return;
				}
				case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_NPC:
				{
					NKMUnitTempletBase unitTempletBase6 = NKMUnitManager.GetUnitTempletBase(eventDeckTemplet.OperatorSlot.m_ID);
					this.SetOperatorData(unitTempletBase6, eventDeckTemplet.OperatorSlot.m_Level, eventDeckTemplet.OperatorSubSkillID);
					NKCUtil.SetGameobjectActive(this.m_csbtnCanChangeOperator, false);
					return;
				}
				}
				Debug.LogError("invalid operator slot setup!");
				this.SetOperatorLock();
				NKCUtil.SetGameobjectActive(this.m_csbtnCanChangeOperator, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_OperatorSlot, false);
			NKCUtil.SetGameobjectActive(this.m_csbtnCanChangeOperator, false);
			NKCUtil.SetGameobjectActive(this.m_OperatorSkill, false);
			NKCUtil.SetGameobjectActive(this.m_OperatorSkillCombo, false);
		}

		// Token: 0x06006B49 RID: 27465 RVA: 0x0022E178 File Offset: 0x0022C378
		private void OpenUnitSelectList(int targetIndex)
		{
			this.m_currentIndex = targetIndex;
			NKMDungeonEventDeckTemplet.EventDeckSlot unitSlot = this.m_currentDeckTemplet.GetUnitSlot(targetIndex);
			NKMDungeonEventDeckTemplet.SLOT_TYPE eType = unitSlot.m_eType;
			if (eType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_CLOSED || eType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_NPC)
			{
				return;
			}
			if (unitSlot.m_eType != this.m_oldSlotType)
			{
				this.m_oldSlotType = unitSlot.m_eType;
				NKCUIComUnitSortOptions sortUI = NKCUIUnitSelectList.Instance.m_SortUI;
				if (sortUI != null)
				{
					sortUI.ClearFilterSet();
				}
			}
			NKCUIUnitSelectList.UnitSelectListOptions options = this.MakeUnitSelectOptions(targetIndex, false);
			options.m_strCachingUIName = this.MenuName;
			NKCUIUnitSelectList.Instance.Open(options, new NKCUIUnitSelectList.OnUnitSelectCommand(this.OnUnitSelected), null, null, null, null);
		}

		// Token: 0x06006B4A RID: 27466 RVA: 0x0022E208 File Offset: 0x0022C408
		private NKCUIUnitSelectList.UnitSelectListOptions MakeUnitSelectOptions(int targetIndex, bool bIsAutoSelect)
		{
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			NKMDungeonEventDeckTemplet.EventDeckSlot slotData = this.m_currentDeckTemplet.GetUnitSlot(targetIndex);
			NKCUIUnitSelectList.UnitSelectListOptions unitSelectListOptions = new NKCUIUnitSelectList.UnitSelectListOptions(NKM_UNIT_TYPE.NUT_NORMAL, false, NKM_DECK_TYPE.NDT_NORMAL, NKCUIUnitSelectList.eUnitSelectListMode.Normal, true);
			unitSelectListOptions.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
			unitSelectListOptions.lstSortOption = NKCUnitSortSystem.GetDefaultSortOptions(NKM_UNIT_TYPE.NUT_NORMAL, false, false);
			unitSelectListOptions.bDescending = true;
			unitSelectListOptions.bShowRemoveSlot = (this.m_dicSelectedUnits.ContainsKey(targetIndex) && this.m_dicSelectedUnits[targetIndex] != 0L);
			unitSelectListOptions.bExcludeLockedUnit = false;
			unitSelectListOptions.bExcludeDeckedUnit = false;
			unitSelectListOptions.bCanSelectUnitInMission = true;
			unitSelectListOptions.bShowHideDeckedUnitMenu = false;
			unitSelectListOptions.bHideDeckedUnit = false;
			unitSelectListOptions.setExcludeUnitUID = new HashSet<long>();
			unitSelectListOptions.setExcludeUnitBaseID = new HashSet<int>();
			unitSelectListOptions.setDuplicateUnitID = new HashSet<int>();
			unitSelectListOptions.bIncludeUndeckableUnit = false;
			unitSelectListOptions.m_SortOptions.bIgnoreCityState = true;
			unitSelectListOptions.m_SortOptions.bIgnoreWorldMapLeader = true;
			unitSelectListOptions.m_SortOptions.bIgnoreMissionState = true;
			unitSelectListOptions.strEmptyMessage = NKCUtilString.GET_STRING_NO_EXIST_SELECT_UNIT;
			unitSelectListOptions.setUnitFilterCategory = NKCUnitSortSystem.setDefaultUnitFilterCategory;
			unitSelectListOptions.setUnitSortCategory = NKCUnitSortSystem.setDefaultUnitSortCategory;
			unitSelectListOptions.m_bUseFavorite = true;
			switch (slotData.m_eType)
			{
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FIXED:
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_GUEST:
				unitSelectListOptions.setOnlyIncludeUnitBaseID = new HashSet<int>();
				unitSelectListOptions.setOnlyIncludeUnitBaseID.Add(slotData.m_ID);
				break;
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_COUNTER:
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_SOLDIER:
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_MECHANIC:
				unitSelectListOptions.m_SortOptions.setOnlyIncludeFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
				unitSelectListOptions.m_SortOptions.setOnlyIncludeFilterOption.Add(NKCUnitSortSystem.GetFilterOption(NKMDungeonManager.GetUnitStyleTypeFromEventDeckType(slotData.m_eType)));
				break;
			}
			for (int i = 0; i < this.m_currentDeckTemplet.m_lstUnitSlot.Count; i++)
			{
				if (i != targetIndex)
				{
					NKMDungeonEventDeckTemplet.EventDeckSlot unitSlot = this.m_currentDeckTemplet.GetUnitSlot(i);
					NKMDungeonEventDeckTemplet.SLOT_TYPE eType = unitSlot.m_eType;
					long unitUid;
					if (eType - NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FIXED <= 2)
					{
						unitSelectListOptions.setExcludeUnitBaseID.Add(unitSlot.m_ID);
					}
					else if (this.m_dicSelectedUnits.TryGetValue(i, out unitUid))
					{
						NKMUnitData unitFromUID = armyData.GetUnitFromUID(unitUid);
						unitSelectListOptions.setDuplicateUnitID.Add(unitFromUID.m_UnitID);
					}
				}
			}
			foreach (KeyValuePair<int, long> keyValuePair in this.m_dicSelectedUnits)
			{
				unitSelectListOptions.setExcludeUnitUID.Add(keyValuePair.Value);
			}
			if (this.m_dicSelectedUnits.ContainsKey(targetIndex))
			{
				unitSelectListOptions.beforeUnit = armyData.GetUnitFromUID(this.m_dicSelectedUnits[targetIndex]);
				unitSelectListOptions.beforeUnitDeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_NONE);
			}
			if (bIsAutoSelect && slotData.m_eType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_GUEST)
			{
				unitSelectListOptions.m_SortOptions.AdditionalExcludeFilterFunc = ((NKMUnitData unitData) => unitData.m_UnitLevel >= slotData.m_Level);
			}
			unitSelectListOptions.m_SortOptions.AdditionalUnitStateFunc = ((NKMUnitData unitData) => this.CheckUnitCondition(unitData, targetIndex, bIsAutoSelect));
			return unitSelectListOptions;
		}

		// Token: 0x06006B4B RID: 27467 RVA: 0x0022E558 File Offset: 0x0022C758
		private NKCUnitSortSystem.eUnitState CheckUnitCondition(NKMUnitData unitData, int slotIndex, bool bCheckAllDeckCondition)
		{
			if (this.m_currentDeckTemplet.m_DeckCondition == null)
			{
				return NKCUnitSortSystem.eUnitState.NONE;
			}
			NKMDungeonEventDeckTemplet.EventDeckSlot unitSlot = this.m_currentDeckTemplet.GetUnitSlot(slotIndex);
			if (bCheckAllDeckCondition)
			{
				NKCUnitSortSystem.eUnitState eUnitState = this.CanAddThisUnitToDeck(unitData);
				if (eUnitState != NKCUnitSortSystem.eUnitState.NONE)
				{
					return eUnitState;
				}
			}
			if (this.m_currentDeckTemplet.m_DeckCondition.CheckEventUnitCondition(unitData, unitSlot) != NKM_ERROR_CODE.NEC_OK)
			{
				return NKCUnitSortSystem.eUnitState.DUNGEON_RESTRICTED;
			}
			return NKCUnitSortSystem.eUnitState.NONE;
		}

		// Token: 0x06006B4C RID: 27468 RVA: 0x0022E5AC File Offset: 0x0022C7AC
		private void OpenShipSelectList()
		{
			NKMDungeonEventDeckTemplet.SLOT_TYPE eType = this.m_currentDeckTemplet.ShipSlot.m_eType;
			if (eType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_CLOSED || eType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_NPC)
			{
				return;
			}
			NKCUIUnitSelectList.UnitSelectListOptions options = this.MakeShipSelectOptions();
			options.m_strCachingUIName = this.MenuName;
			NKCUIUnitSelectList.Instance.Open(options, new NKCUIUnitSelectList.OnUnitSelectCommand(this.OnShipSelected), null, null, null, null);
		}

		// Token: 0x06006B4D RID: 27469 RVA: 0x0022E604 File Offset: 0x0022C804
		private NKCUIUnitSelectList.UnitSelectListOptions MakeOperatorSelectOptions()
		{
			NKMDungeonEventDeckTemplet.EventDeckSlot operatorSlot = this.m_currentDeckTemplet.OperatorSlot;
			NKCUIUnitSelectList.UnitSelectListOptions result = new NKCUIUnitSelectList.UnitSelectListOptions(NKM_UNIT_TYPE.NUT_OPERATOR, false, NKM_DECK_TYPE.NDT_NORMAL, NKCUIUnitSelectList.eUnitSelectListMode.Normal, true);
			result.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
			result.lstSortOption = NKCOperatorSortSystem.ConvertSortOption(NKCOperatorSortSystem.GetDefaultSortOptions(false, false));
			result.bDescending = true;
			result.bShowRemoveSlot = (this.m_SelectedOperatorUid != 0L);
			result.bExcludeLockedUnit = false;
			result.bExcludeDeckedUnit = false;
			result.bCanSelectUnitInMission = true;
			result.bShowHideDeckedUnitMenu = false;
			result.bHideDeckedUnit = false;
			result.strEmptyMessage = NKCUtilString.GET_STRING_NO_EXIST_TARGET_TO_SELECT;
			result.m_OperatorSortOptions.bIgnoreMissionState = true;
			result.m_bUseFavorite = true;
			result.setOperatorFilterCategory = NKCPopupFilterOperator.MakeDefaultFilterCategory(NKCPopupFilterOperator.FILTER_OPEN_TYPE.NORMAL);
			result.setOperatorSortCategory = NKCOperatorSortSystem.setDefaultOperatorSortCategory;
			if (operatorSlot.m_eType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FIXED || operatorSlot.m_eType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_GUEST)
			{
				result.setOnlyIncludeUnitBaseID = new HashSet<int>();
				result.setOnlyIncludeUnitBaseID.Add(operatorSlot.m_ID);
			}
			if (this.m_SelectedOperatorUid != 0L)
			{
				result.setExcludeOperatorUID = new HashSet<long>();
				result.setExcludeOperatorUID.Add(this.m_SelectedOperatorUid);
			}
			return result;
		}

		// Token: 0x06006B4E RID: 27470 RVA: 0x0022E71C File Offset: 0x0022C91C
		private bool CheckOperatorCondition(NKMUnitData unitData)
		{
			return this.m_currentDeckTemplet.m_DeckCondition == null || this.m_currentDeckTemplet.m_DeckCondition.CheckEventUnitCondition(unitData, this.m_currentDeckTemplet.OperatorSlot) == NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06006B4F RID: 27471 RVA: 0x0022E74C File Offset: 0x0022C94C
		private bool CheckShipCondition(NKMUnitData unitData)
		{
			return this.m_currentDeckTemplet.m_DeckCondition == null || this.m_currentDeckTemplet.m_DeckCondition.CheckEventUnitCondition(unitData, this.m_currentDeckTemplet.ShipSlot) == NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06006B50 RID: 27472 RVA: 0x0022E77C File Offset: 0x0022C97C
		private void CloseShipIllust()
		{
			if (this.m_shipIllust != null)
			{
				NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_shipIllust);
				this.m_shipIllust = null;
			}
		}

		// Token: 0x06006B51 RID: 27473 RVA: 0x0022E7A4 File Offset: 0x0022C9A4
		private void SetShipData(NKMUnitData unitData)
		{
			this.CloseShipIllust();
			bool bValue = false;
			if (unitData != null)
			{
				this.m_shipIllust = NKCResourceUtility.OpenSpineIllust(unitData, false);
				if (this.m_shipIllust != null)
				{
					this.m_shipIllust.SetParent(this.m_rtShipRoot, false);
					this.m_shipIllust.SetAnchoredPosition(Vector2.zero);
				}
				this.m_lbShipLevel.text = string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, unitData.m_UnitLevel);
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID);
				this.m_lbShipName.text = unitTempletBase.GetUnitName();
			}
			else
			{
				this.m_lbShipLevel.text = "";
				this.m_lbShipName.text = NKCUtilString.GET_STRING_DECK_SELECT_SHIP;
			}
			NKCUtil.SetGameobjectActive(this.m_AB_UI_NKM_UI_OPERATION_EVENTDECK_SHIP_FIERCE_BATTLE, bValue);
		}

		// Token: 0x06006B52 RID: 27474 RVA: 0x0022E860 File Offset: 0x0022CA60
		private void SetShipData(NKMUnitTempletBase shipTemplet, int level)
		{
			this.CloseShipIllust();
			if (shipTemplet != null)
			{
				this.m_shipIllust = NKCResourceUtility.OpenSpineIllust(shipTemplet, false);
				if (this.m_shipIllust != null)
				{
					this.m_shipIllust.SetParent(this.m_rtShipRoot, false);
					this.m_shipIllust.SetAnchoredPosition(Vector2.zero);
				}
				this.m_lbShipLevel.text = string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, level);
				this.m_lbShipName.text = shipTemplet.GetUnitName();
			}
			else
			{
				this.m_lbShipLevel.text = "";
				this.m_lbShipName.text = NKCUtilString.GET_STRING_DECK_SELECT_SHIP;
				NKCUtil.SetGameobjectActive(this.m_AB_UI_NKM_UI_OPERATION_EVENTDECK_SHIP_FIERCE_BATTLE, false);
				NKCUtil.SetGameobjectActive(this.m_objNotPossibleOperator, false);
			}
			NKCUtil.SetGameobjectActive(this.m_AB_UI_NKM_UI_OPERATION_EVENTDECK_SHIP_FIERCE_BATTLE, false);
		}

		// Token: 0x06006B53 RID: 27475 RVA: 0x0022E920 File Offset: 0x0022CB20
		private void OnEventDeckSlotSelect(int index)
		{
			if (!this.m_isSelectingLeader)
			{
				this.OpenUnitSelectList(index);
				return;
			}
			if (this.m_lstSlot == null || this.m_lstSlot.Count <= index || index < 0)
			{
				return;
			}
			if (this.m_lstSlot[index].CanBecomeLeader())
			{
				this.SetAsLeader(index);
			}
		}

		// Token: 0x06006B54 RID: 27476 RVA: 0x0022E974 File Offset: 0x0022CB74
		private void OnShipSelected(List<long> lstUID)
		{
			if (lstUID.Count <= 0)
			{
				return;
			}
			NKCUIUnitSelectList.CheckInstanceAndClose();
			long targetUID = lstUID[0];
			this.OnShipSelected(targetUID);
		}

		// Token: 0x06006B55 RID: 27477 RVA: 0x0022E9A0 File Offset: 0x0022CBA0
		private void OnShipSelected(long targetUID)
		{
			NKMUnitData shipFromUID = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetShipFromUID(targetUID);
			this.m_SelectedShipUid = targetUID;
			this.SetShipData(shipFromUID);
			if (targetUID == 0L)
			{
				this.m_cbtnChngeShip.dOnPointerHolding = null;
				return;
			}
			this.m_cbtnChngeShip.dOnPointerHolding = new NKCUIComButton.OnPointerHolding(this.OpenShipData);
		}

		// Token: 0x06006B56 RID: 27478 RVA: 0x0022E9F8 File Offset: 0x0022CBF8
		private void OnUnitSelected(List<long> lstUID)
		{
			if (lstUID.Count <= 0)
			{
				return;
			}
			NKCUIUnitSelectList.CheckInstanceAndClose();
			long unitUID = lstUID[0];
			this.OnUnitSelected(this.m_currentIndex, unitUID);
			this.UpdateConditionUI();
		}

		// Token: 0x06006B57 RID: 27479 RVA: 0x0022EA30 File Offset: 0x0022CC30
		private void OnUnitSelected(int index, long unitUID)
		{
			NKMDungeonEventDeckTemplet.EventDeckSlot unitSlot = this.m_currentDeckTemplet.GetUnitSlot(index);
			NKMDungeonEventDeckTemplet.SLOT_TYPE eType = unitSlot.m_eType;
			if (eType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_CLOSED || eType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_NPC)
			{
				return;
			}
			if (unitUID == 0L)
			{
				if (index < this.m_lstSlot.Count)
				{
					if (unitSlot.m_eType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_GUEST)
					{
						NKMDungeonEventDeckTemplet.EventDeckSlot unitSlot2 = this.m_currentDeckTemplet.GetUnitSlot(index);
						this.m_lstSlot[index].InitEventSlot(unitSlot2, index, true, new NKCUIUnitSelectListEventDeckSlot.OnSelectEventDeckSlot(this.OnEventDeckSlotSelect), new NKCUIUnitSelectListEventDeckSlot.OnUnitDetail(this.OpenUnitData));
					}
					else
					{
						this.m_lstSlot[index].SetEmpty(false, null, null);
					}
					this.m_lstSlot[index].ClearTouchHoldEvent();
				}
				this.m_lstSlot[index].ClearTouchHoldEvent();
				this.m_dicSelectedUnits.Remove(index);
			}
			else
			{
				NKMUnitData unitFromUID = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetUnitFromUID(unitUID);
				if ((unitSlot.m_eType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FIXED || unitSlot.m_eType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_GUEST) && !unitFromUID.IsSameBaseUnit(unitSlot.m_ID))
				{
					return;
				}
				int num = -1;
				foreach (KeyValuePair<int, long> keyValuePair in this.m_dicSelectedUnits)
				{
					if (keyValuePair.Value == unitUID)
					{
						num = keyValuePair.Key;
					}
				}
				if (num != -1)
				{
					this.m_dicSelectedUnits.Remove(num);
					if (index < this.m_lstSlot.Count)
					{
						this.m_lstSlot[num].SetEmpty(false, null, null);
					}
				}
				this.m_dicSelectedUnits[index] = unitUID;
				this.m_lstSlot[index].SetData(unitFromUID, index, false, new NKCUIUnitSelectListEventDeckSlot.OnSelectEventDeckSlot(this.OnEventDeckSlotSelect), this.m_eventDeckContents == DeckContents.FIERCE_BATTLE_SUPPORT);
				this.m_lstSlot[index].SetTouchHoldEvent(new UnityAction<NKMUnitData>(this.OpenUnitData));
				this.m_lstSlot[index].ConfirmLeader(this.m_currentLeaderIndex);
			}
			this.RecalculateDeckAllConditionCache();
		}

		// Token: 0x06006B58 RID: 27480 RVA: 0x0022EC30 File Offset: 0x0022CE30
		private void OpenUnitData(NKMUnitData unitData)
		{
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			if (unitData == null)
			{
				return;
			}
			NKCUIUnitInfo.OpenOption openOption = new NKCUIUnitInfo.OpenOption(new List<long>(this.m_dicSelectedUnits.Values), 0);
			openOption.m_bShowFierceInfo = (this.m_eventDeckContents == DeckContents.FIERCE_BATTLE_SUPPORT);
			NKCUIUnitInfo.Instance.Open(unitData, null, openOption, NKC_SCEN_UNIT_LIST.eUIOpenReserve.Nothing);
		}

		// Token: 0x06006B59 RID: 27481 RVA: 0x0022EC88 File Offset: 0x0022CE88
		private void OnOperatorSelected(List<long> lstUID)
		{
			if (lstUID.Count <= 0)
			{
				return;
			}
			NKCUIUnitSelectList.CheckInstanceAndClose();
			long targetUID = lstUID[0];
			this.OnOperatorSelected(targetUID);
		}

		// Token: 0x06006B5A RID: 27482 RVA: 0x0022ECB4 File Offset: 0x0022CEB4
		private void OnOperatorSelected(long targetUID)
		{
			this.m_SelectedOperatorUid = targetUID;
			NKMOperator operatorData = NKCOperatorUtil.GetOperatorData(this.m_SelectedOperatorUid);
			this.m_OperatorSlot.SetData(operatorData, false);
			if (operatorData != null)
			{
				this.m_OperatorSkillCombo.SetData(operatorData.id);
				this.m_OperatorMainSkill.SetData(operatorData.mainSkill.id, (int)operatorData.mainSkill.level, false);
				this.m_OperatorSubSkill.SetData(operatorData.subSkill.id, (int)operatorData.subSkill.level, false);
			}
			NKCUtil.SetGameobjectActive(this.m_OperatorSkillCombo, operatorData != null);
			NKCUtil.SetGameobjectActive(this.m_OperatorSkill, operatorData != null);
			NKCUtil.SetGameobjectActive(this.m_objNotPossibleOperator, false);
			if (this.m_OperatorSlot != null && this.m_OperatorSlot.m_NKM_UI_OPERATOR_DECK_SLOT != null)
			{
				if (targetUID == 0L)
				{
					this.m_OperatorSlot.m_NKM_UI_OPERATOR_DECK_SLOT.dOnPointerHolding = null;
					return;
				}
				this.m_OperatorSlot.m_NKM_UI_OPERATOR_DECK_SLOT.dOnPointerHolding = new NKCUIComStateButtonBase.OnPointerHolding(this.OpenOperatorData);
			}
		}

		// Token: 0x06006B5B RID: 27483 RVA: 0x0022EDB8 File Offset: 0x0022CFB8
		private void SetOperatorData(NKMUnitTempletBase unitTempletBase, int level, int subSkillID = 0)
		{
			this.m_OperatorSlot.SetData(unitTempletBase, level);
			NKCUtil.SetGameobjectActive(this.m_OperatorSkillCombo, unitTempletBase != null);
			NKCUtil.SetGameobjectActive(this.m_OperatorSkill, unitTempletBase != null);
			if (unitTempletBase != null)
			{
				this.m_OperatorSkillCombo.SetData(unitTempletBase.m_UnitID);
				NKMOperatorSkillTemplet skillTemplet = NKCOperatorUtil.GetSkillTemplet(unitTempletBase.m_lstSkillStrID[0]);
				if (skillTemplet != null)
				{
					this.m_OperatorMainSkill.SetData(skillTemplet.m_OperSkillID, skillTemplet.m_MaxSkillLevel, false);
				}
				NKCUtil.SetGameobjectActive(this.m_OperatorSubSkill, skillTemplet != null);
				NKMOperatorSkillTemplet skillTemplet2 = NKCOperatorUtil.GetSkillTemplet(subSkillID);
				if (skillTemplet2 != null)
				{
					this.m_OperatorSubSkill.SetData(skillTemplet2.m_OperSkillID, skillTemplet2.m_MaxSkillLevel, false);
				}
				NKCUtil.SetGameobjectActive(this.m_OperatorSubSkill, skillTemplet2 != null);
			}
			NKCUtil.SetGameobjectActive(this.m_objNotPossibleOperator, false);
		}

		// Token: 0x06006B5C RID: 27484 RVA: 0x0022EE7D File Offset: 0x0022D07D
		private void SetOperatorLock()
		{
			NKCUIOperatorDeckSlot operatorSlot = this.m_OperatorSlot;
			if (operatorSlot != null)
			{
				operatorSlot.SetLock();
			}
			NKCUtil.SetGameobjectActive(this.m_objNotPossibleOperator, false);
			NKCUtil.SetGameobjectActive(this.m_OperatorSkill, false);
			NKCUtil.SetGameobjectActive(this.m_OperatorSkillCombo, false);
		}

		// Token: 0x06006B5D RID: 27485 RVA: 0x0022EEB4 File Offset: 0x0022D0B4
		private void OpenShipData()
		{
			if (this.m_SelectedShipUid == 0L)
			{
				return;
			}
			NKMUnitData shipFromUID = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetShipFromUID(this.m_SelectedShipUid);
			NKCUIShipInfo.Instance.Open(shipFromUID, NKMDeckIndex.None, null, NKC_SCEN_UNIT_LIST.eUIOpenReserve.Nothing);
		}

		// Token: 0x06006B5E RID: 27486 RVA: 0x0022EEF7 File Offset: 0x0022D0F7
		private void OpenOperatorData()
		{
			if (this.m_SelectedOperatorUid == 0L)
			{
				return;
			}
			NKCUIOperatorInfo.Instance.Open(NKCOperatorUtil.GetOperatorData(this.m_SelectedOperatorUid), new NKCUIOperatorInfo.OpenOption(new List<long>
			{
				this.m_SelectedOperatorUid
			}, 0));
		}

		// Token: 0x06006B5F RID: 27487 RVA: 0x0022EF30 File Offset: 0x0022D130
		public void StartGame()
		{
			if (this.m_StageTemplet != null && this.m_StageTemplet.EpisodeTemplet != null && !this.m_StageTemplet.EpisodeTemplet.IsOpen)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_EXCEPTION_EVENT_EXPIRED_POPUP, delegate()
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				}, "");
				return;
			}
			if (this.dOnEventDeckConfirm != null)
			{
				NKMEventDeckData eventDeckData = new NKMEventDeckData(this.m_dicSelectedUnits, this.m_SelectedShipUid, this.m_SelectedOperatorUid, this.m_currentLeaderIndex);
				this.dOnEventDeckConfirm(this.m_StageTemplet, this.m_currentDungeonTempletBase, eventDeckData);
				this.SaveDungeonDeck();
			}
		}

		// Token: 0x06006B60 RID: 27488 RVA: 0x0022EFDC File Offset: 0x0022D1DC
		public bool CheckStartPossible()
		{
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			NKMEventDeckData eventDeckData = new NKMEventDeckData(this.m_dicSelectedUnits, this.m_SelectedShipUid, this.m_SelectedOperatorUid, this.m_currentLeaderIndex);
			NKM_ERROR_CODE nkm_ERROR_CODE = NKMDungeonManager.IsValidEventDeck(armyData, this.m_currentDeckTemplet, eventDeckData, this.OperatorEnabled);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupOKCancel.OpenOKBox(nkm_ERROR_CODE, null, "");
				return false;
			}
			if (!NKCUtil.IsCanStartEterniumStage(this.m_StageTemplet, true))
			{
				return false;
			}
			this.UpdateAttackCost();
			if (!NKCScenManager.GetScenManager().GetMyUserData().CheckPrice(this.m_iCostItemCount, this.m_iCostItemID))
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_ATTACK_COST_IS_NOT_ENOUGH, null, "");
				return false;
			}
			return true;
		}

		// Token: 0x06006B61 RID: 27489 RVA: 0x0022F085 File Offset: 0x0022D285
		private void OnBtnBegin()
		{
			if (!this.CheckStartPossible())
			{
				return;
			}
			this.StartGame();
		}

		// Token: 0x06006B62 RID: 27490 RVA: 0x0022F098 File Offset: 0x0022D298
		private void AutoPrepare()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			HashSet<int> setExcludeIndex = new HashSet<int>(this.m_dicSelectedUnits.Keys);
			for (int i = 0; i < 8; i++)
			{
				NKMDungeonEventDeckTemplet.EventDeckSlot unitSlot = this.m_currentDeckTemplet.GetUnitSlot(i);
				NKMDungeonEventDeckTemplet.SLOT_TYPE eType = unitSlot.m_eType;
				if (eType != NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_CLOSED && eType != NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_NPC && !this.m_dicSelectedUnits.ContainsKey(i))
				{
					NKCUIUnitSelectList.UnitSelectListOptions unitSelectListOptions = this.MakeUnitSelectOptions(i, true);
					unitSelectListOptions.lstSortOption = new List<NKCUnitSortSystem.eSortOption>
					{
						NKCUnitSortSystem.eSortOption.Power_High
					};
					NKMUnitData nkmunitData = new NKCUnitSort(myUserData, unitSelectListOptions.m_SortOptions).AutoSelect(null, null);
					if (nkmunitData != null && (unitSlot.m_eType != NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_GUEST || nkmunitData.m_UnitLevel >= unitSlot.m_Level))
					{
						this.OnUnitSelected(i, nkmunitData.m_UnitUID);
					}
				}
			}
			NKMDeckCondition deckCondition = this.m_currentDeckTemplet.m_DeckCondition;
			NKMDeckCondition.SingleCondition singleCondition = (deckCondition != null) ? deckCondition.GetAllDeckCondition(NKMDeckCondition.DECK_CONDITION.UNIT_COST_TOTAL) : null;
			if (singleCondition != null && !singleCondition.IsValueOk(this.GetDeckAllValueCache(NKMDeckCondition.DECK_CONDITION.UNIT_COST_TOTAL)))
			{
				NKCUIPrepareEventDeck.<>c__DisplayClass147_0 CS$<>8__locals1;
				CS$<>8__locals1.hsDuplicateUnit = new HashSet<int>();
				NKCUnitSortSystem.UnitListOptions unitListOptions = default(NKCUnitSortSystem.UnitListOptions);
				unitListOptions.eDeckType = NKM_DECK_TYPE.NDT_NORMAL;
				unitListOptions.setExcludeUnitBaseID = new HashSet<int>();
				foreach (NKMDungeonEventDeckTemplet.EventDeckSlot eventDeckSlot in this.m_currentDeckTemplet.m_lstUnitSlot)
				{
					NKMDungeonEventDeckTemplet.SLOT_TYPE eType = eventDeckSlot.m_eType;
					if (eType - NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FIXED <= 2)
					{
						unitListOptions.setExcludeUnitBaseID.Add(eventDeckSlot.m_ID);
						CS$<>8__locals1.hsDuplicateUnit.Add(eventDeckSlot.m_ID);
					}
				}
				unitListOptions.lstSortOption = new List<NKCUnitSortSystem.eSortOption>
				{
					NKCUnitSortSystem.eSortOption.Power_High
				};
				unitListOptions.bExcludeLockedUnit = false;
				unitListOptions.bExcludeDeckedUnit = false;
				unitListOptions.bHideDeckedUnit = false;
				unitListOptions.setExcludeUnitUID = new HashSet<long>();
				unitListOptions.bIncludeUndeckableUnit = false;
				unitListOptions.bIgnoreCityState = true;
				unitListOptions.bIgnoreWorldMapLeader = true;
				unitListOptions.bIgnoreMissionState = true;
				unitListOptions.AdditionalUnitStateFunc = new NKCUnitSortSystem.UnitListOptions.CustomUnitStateFunc(this.CanAddThisUnitToDeck);
				NKCUnitSort nkcunitSort = new NKCUnitSort(myUserData, unitListOptions);
				HashSet<long> hashSet = new HashSet<long>(this.m_dicSelectedUnits.Values);
				Dictionary<int, long> dictionary = new Dictionary<int, long>(this.m_dicSelectedUnits);
				Dictionary<int, long> dictionary2 = new Dictionary<int, long>();
				using (Dictionary<int, long>.Enumerator enumerator2 = this.m_dicSelectedUnits.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						KeyValuePair<int, long> keyValuePair = enumerator2.Current;
						NKMUnitData unitFromUID = myUserData.m_ArmyData.GetUnitFromUID(keyValuePair.Value);
						if (unitFromUID != null)
						{
							CS$<>8__locals1.hsDuplicateUnit.Add(unitFromUID.m_UnitID);
						}
					}
					goto IL_2FC;
				}
				IL_27C:
				NKMUnitData nkmunitData2 = nkcunitSort.AutoSelect(hashSet, null);
				if (nkmunitData2 == null)
				{
					goto IL_30F;
				}
				hashSet.Add(nkmunitData2.m_UnitUID);
				if (!NKCUIPrepareEventDeck.<AutoPrepare>g__CheckDuplicateUnit|147_0(nkmunitData2.m_UnitID, ref CS$<>8__locals1))
				{
					int num = this.FindTotalCostAutoSelectChangeableIndex(nkmunitData2, this.GetCurrentUnitTotalCost(dictionary), dictionary, setExcludeIndex);
					if (num != -1)
					{
						dictionary[num] = nkmunitData2.m_UnitUID;
						dictionary2[num] = nkmunitData2.m_UnitUID;
						CS$<>8__locals1.hsDuplicateUnit.Add(nkmunitData2.m_UnitID);
					}
				}
				IL_2FC:
				if (!singleCondition.IsValueOk(this.GetCurrentUnitTotalCost(dictionary)))
				{
					goto IL_27C;
				}
				IL_30F:
				foreach (KeyValuePair<int, long> keyValuePair2 in dictionary2)
				{
					this.OnUnitSelected(keyValuePair2.Key, keyValuePair2.Value);
				}
			}
			if (this.m_SelectedShipUid == 0L)
			{
				NKMDungeonEventDeckTemplet.SLOT_TYPE eType = this.m_currentDeckTemplet.ShipSlot.m_eType;
				if (eType != NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_CLOSED && eType != NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_NPC)
				{
					NKCUIUnitSelectList.UnitSelectListOptions unitSelectListOptions2 = this.MakeShipSelectOptions();
					unitSelectListOptions2.lstSortOption = new List<NKCUnitSortSystem.eSortOption>
					{
						NKCUnitSortSystem.eSortOption.Power_High
					};
					NKMUnitData nkmunitData3 = new NKCShipSort(myUserData, unitSelectListOptions2.m_SortOptions).AutoSelect(null, null);
					if (nkmunitData3 != null)
					{
						this.OnShipSelected(nkmunitData3.m_UnitUID);
					}
				}
			}
			if (this.m_SelectedOperatorUid == 0L)
			{
				NKMDungeonEventDeckTemplet.SLOT_TYPE eType = this.m_currentDeckTemplet.OperatorSlot.m_eType;
				if (eType != NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_CLOSED && eType != NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_NPC)
				{
					NKCUIUnitSelectList.UnitSelectListOptions unitSelectListOptions3 = this.MakeOperatorSelectOptions();
					unitSelectListOptions3.lstOperatorSortOption = new List<NKCOperatorSortSystem.eSortOption>
					{
						NKCOperatorSortSystem.eSortOption.Power_High
					};
					NKMOperator nkmoperator = new NKCOperatorSort(myUserData, unitSelectListOptions3.m_OperatorSortOptions).AutoSelect(null, null);
					if (nkmoperator != null)
					{
						this.OnOperatorSelected(nkmoperator.uid);
					}
				}
			}
			this.UpdateConditionUI();
			this.SetAsLeader(this.m_currentLeaderIndex);
		}

		// Token: 0x06006B63 RID: 27491 RVA: 0x0022F4FC File Offset: 0x0022D6FC
		private int GetDeckAllValueCache(NKMDeckCondition.DECK_CONDITION type)
		{
			int result;
			if (this.m_dicAllDeckValueCache.TryGetValue(type, out result))
			{
				return result;
			}
			return 0;
		}

		// Token: 0x06006B64 RID: 27492 RVA: 0x0022F51C File Offset: 0x0022D71C
		private void RecalculateDeckAllConditionCache()
		{
			Debug.Log("RecalculateDeckAllConditionCache");
			this.m_dicAllDeckValueCache.Clear();
			NKMDungeonEventDeckTemplet currentDeckTemplet = this.m_currentDeckTemplet;
			bool flag;
			if (currentDeckTemplet == null)
			{
				flag = (null != null);
			}
			else
			{
				NKMDeckCondition deckCondition = currentDeckTemplet.m_DeckCondition;
				flag = (((deckCondition != null) ? deckCondition.m_dicAllDeckCondition : null) != null);
			}
			if (flag)
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				foreach (NKMDeckCondition.SingleCondition singleCondition in this.m_currentDeckTemplet.m_DeckCondition.m_dicAllDeckCondition.Values)
				{
					int num = 0;
					for (int i = 0; i < 8; i++)
					{
						NKMDungeonEventDeckTemplet.EventDeckSlot unitSlot = this.m_currentDeckTemplet.GetUnitSlot(i);
						NKMDungeonEventDeckTemplet.SLOT_TYPE eType = unitSlot.m_eType;
						if (eType != NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_CLOSED)
						{
							long unitUid;
							if (eType - NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_GUEST <= 1)
							{
								NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitSlot.m_ID);
								num += singleCondition.GetAllDeckConditionValue(unitTempletBase);
							}
							else if (this.m_dicSelectedUnits.TryGetValue(i, out unitUid))
							{
								NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(nkmuserData.m_ArmyData.GetUnitFromUID(unitUid));
								num += singleCondition.GetAllDeckConditionValue(unitTempletBase2);
							}
						}
					}
					this.m_dicAllDeckValueCache[singleCondition.eCondition] = num;
				}
			}
		}

		// Token: 0x06006B65 RID: 27493 RVA: 0x0022F650 File Offset: 0x0022D850
		private NKCUnitSortSystem.eUnitState CanAddThisUnitToDeck(NKMUnitData unitData)
		{
			NKMDungeonEventDeckTemplet currentDeckTemplet = this.m_currentDeckTemplet;
			bool flag;
			if (currentDeckTemplet == null)
			{
				flag = (null != null);
			}
			else
			{
				NKMDeckCondition deckCondition = currentDeckTemplet.m_DeckCondition;
				flag = (((deckCondition != null) ? deckCondition.m_dicAllDeckCondition : null) != null);
			}
			if (flag)
			{
				foreach (NKMDeckCondition.SingleCondition singleCondition in this.m_currentDeckTemplet.m_DeckCondition.m_dicAllDeckCondition.Values)
				{
					if (singleCondition.eCondition != NKMDeckCondition.DECK_CONDITION.UNIT_COST_TOTAL)
					{
						NKCScenManager.GetScenManager().GetMyUserData();
						int currentValue = this.m_dicAllDeckValueCache[singleCondition.eCondition];
						if (!singleCondition.CanAddThisUnit(unitData, currentValue))
						{
							return NKCUnitSortSystem.eUnitState.DUNGEON_RESTRICTED;
						}
					}
				}
			}
			if (this.m_currentDeckTemplet.m_DeckCondition.CheckUnitCondition(unitData) != NKM_ERROR_CODE.NEC_OK)
			{
				return NKCUnitSortSystem.eUnitState.DUNGEON_RESTRICTED;
			}
			return NKCUnitSortSystem.eUnitState.NONE;
		}

		// Token: 0x06006B66 RID: 27494 RVA: 0x0022F71C File Offset: 0x0022D91C
		private int FindTotalCostAutoSelectChangeableIndex(NKMUnitData changeCandidate, int currentTotalCost, Dictionary<int, long> selectedUnits, HashSet<int> setExcludeIndex)
		{
			int respawnCost = NKMUnitManager.GetUnitStatTemplet(changeCandidate.m_UnitID).GetRespawnCost(false, null, null);
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			NKMDeckCondition deckCondition = this.m_currentDeckTemplet.m_DeckCondition;
			NKMDeckCondition.SingleCondition singleCondition = (deckCondition != null) ? deckCondition.GetAllDeckCondition(NKMDeckCondition.DECK_CONDITION.UNIT_COST_TOTAL) : null;
			foreach (KeyValuePair<int, long> keyValuePair in selectedUnits)
			{
				if (setExcludeIndex == null || !setExcludeIndex.Contains(keyValuePair.Key))
				{
					NKMDungeonEventDeckTemplet.EventDeckSlot unitSlot = this.m_currentDeckTemplet.GetUnitSlot(keyValuePair.Key);
					NKMDungeonEventDeckTemplet.SLOT_TYPE eType = unitSlot.m_eType;
					if (eType != NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_CLOSED && eType - NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FIXED > 2 && this.m_currentDeckTemplet.IsUnitFitInSlot(unitSlot, changeCandidate))
					{
						NKMUnitData unitFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(keyValuePair.Value);
						int respawnCost2 = NKMUnitManager.GetUnitStatTemplet(unitFromUID.m_UnitID).GetRespawnCost(false, null, null);
						int num4 = unitFromUID.CalculateOperationPower(NKCScenManager.CurrentUserData().m_InventoryData, 0, null, null);
						bool flag = false;
						switch (singleCondition.eMoreLess)
						{
						case NKMDeckCondition.MORE_LESS.EQUAL:
						{
							int num5 = Math.Abs(currentTotalCost - singleCondition.Value);
							if (currentTotalCost > singleCondition.Value)
							{
								if (respawnCost2 < respawnCost)
								{
									int num6 = respawnCost - respawnCost2;
									if (num6 == num5)
									{
										return keyValuePair.Key;
									}
									if (num6 < num5)
									{
										flag = (num < 0 || num2 > respawnCost2 || (num2 == respawnCost2 && num3 > num4));
									}
								}
							}
							else if (currentTotalCost < singleCondition.Value && respawnCost2 > respawnCost)
							{
								int num7 = respawnCost2 - respawnCost;
								if (num7 == num5)
								{
									return keyValuePair.Key;
								}
								if (num7 < num5)
								{
									flag = (num < 0 || num2 < respawnCost2 || (num2 == respawnCost2 && num3 > num4));
								}
							}
							break;
						}
						case NKMDeckCondition.MORE_LESS.NOT:
							if (respawnCost2 != respawnCost)
							{
								flag = (num3 > num4);
							}
							break;
						case NKMDeckCondition.MORE_LESS.MORE:
							if (respawnCost2 < respawnCost)
							{
								flag = (num < 0 || num2 < respawnCost2 || (num2 == respawnCost2 && num3 > num4));
							}
							break;
						case NKMDeckCondition.MORE_LESS.LESS:
							if (respawnCost2 > respawnCost)
							{
								flag = (num < 0 || num2 > respawnCost2 || (num2 == respawnCost2 && num3 > num4));
							}
							break;
						}
						if (flag)
						{
							num = keyValuePair.Key;
							num2 = respawnCost2;
							num3 = num4;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06006B67 RID: 27495 RVA: 0x0022F98C File Offset: 0x0022DB8C
		private void ClearAll()
		{
			this.m_dicSelectedUnits.Clear();
			this.m_SelectedShipUid = 0L;
			this.m_SelectedOperatorUid = 0L;
			this.InitEventDeckData(this.m_currentDeckTemplet);
			this.SetAsLeader(this.m_currentLeaderIndex);
			this.RecalculateDeckAllConditionCache();
			this.UpdateConditionUI();
		}

		// Token: 0x06006B68 RID: 27496 RVA: 0x0022F9D9 File Offset: 0x0022DBD9
		private void UpdateConditionUI()
		{
			this.SetDeckConditionUI(this.m_currentDeckTemplet);
			this.UpdateEnterLimitUI();
			this.UpdateUnitTotalCost();
		}

		// Token: 0x06006B69 RID: 27497 RVA: 0x0022F9F4 File Offset: 0x0022DBF4
		private void UpdateAttackCost()
		{
			bool flag = false;
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			bool flag2 = this.m_eventDeckContents == DeckContents.SHADOW_PALACE;
			if (this.m_eventDeckContents == DeckContents.FIERCE_BATTLE_SUPPORT)
			{
				NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
				if (nkcfierceBattleSupportDataMgr != null)
				{
					NKMFierceBossGroupTemplet bossGroupTemplet = nkcfierceBattleSupportDataMgr.GetBossGroupTemplet();
					if (bossGroupTemplet != null)
					{
						flag = true;
						this.m_iCostItemID = bossGroupTemplet.StageReqItemID;
						this.m_iCostItemCount = bossGroupTemplet.StageReqItemCount;
					}
				}
			}
			else if (this.m_StageTemplet == null || flag2)
			{
				this.m_iCostItemID = 0;
				this.m_iCostItemCount = 0;
			}
			else if (nkmuserData.GetStatePlayCnt(this.m_StageTemplet.Key, false, false, false) == this.m_StageTemplet.EnterLimit && this.m_StageTemplet.RestoreReqItem != null)
			{
				flag = true;
				this.m_iCostItemID = this.m_StageTemplet.RestoreReqItem.ItemId;
				this.m_iCostItemCount = this.m_StageTemplet.RestoreReqItem.Count32;
			}
			else
			{
				if (this.m_StageTemplet.m_StageReqItemID <= 0)
				{
					if (this.m_StageTemplet.m_STAGE_TYPE == STAGE_TYPE.ST_WARFARE)
					{
						this.m_iCostItemID = 2;
						this.m_iCostItemCount = this.m_StageTemplet.m_StageReqItemCount;
					}
					else
					{
						this.m_iCostItemID = 0;
						this.m_iCostItemCount = 0;
					}
				}
				else
				{
					flag = true;
					this.m_iCostItemID = this.m_StageTemplet.m_StageReqItemID;
					this.m_iCostItemCount = this.m_StageTemplet.m_StageReqItemCount;
				}
				if (this.m_iCostItemID == 2)
				{
					NKCCompanyBuff.SetDiscountOfEterniumInEnteringDungeon(NKCScenManager.CurrentUserData().m_companyBuffDataList, ref this.m_iCostItemCount);
				}
				this.m_iCostItemCount *= this.m_CurrMultiplyRewardCount;
			}
			if (NKMItemManager.GetItemMiscTempletByID(this.m_iCostItemID) == null)
			{
				this.m_ResourceBtn.OnShow(false);
				return;
			}
			if (this.m_iCostItemID == 0)
			{
				this.m_ResourceBtn.OnShow(false);
				return;
			}
			if (flag)
			{
				this.m_ResourceBtn.SetData(this.m_iCostItemID, this.m_iCostItemCount);
				this.m_ResourceBtn.OnShow(true);
				return;
			}
			this.m_ResourceBtn.OnShow(false);
		}

		// Token: 0x06006B6A RID: 27498 RVA: 0x0022FBDC File Offset: 0x0022DDDC
		private void UpdateUnitTotalCost()
		{
			if (this.m_currentDeckTemplet == null || this.m_currentDeckTemplet.m_DeckCondition == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objUnitConditionCostTotal, false);
				return;
			}
			NKMDeckCondition deckCondition = this.m_currentDeckTemplet.m_DeckCondition;
			NKMDeckCondition.SingleCondition singleCondition = (deckCondition != null) ? deckCondition.GetAllDeckCondition(NKMDeckCondition.DECK_CONDITION.UNIT_COST_TOTAL) : null;
			if (singleCondition == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objUnitConditionCostTotal, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objUnitConditionCostTotal, true);
			int value = singleCondition.Value;
			NKMDeckCondition.MORE_LESS eMoreLess = singleCondition.eMoreLess;
			int deckAllValueCache = this.GetDeckAllValueCache(NKMDeckCondition.DECK_CONDITION.UNIT_COST_TOTAL);
			NKCUtil.SetLabelText(this.m_lbUnitConditionCostTotal, NKCUtilString.GetDeckConditionString(singleCondition));
			if (singleCondition.IsValueOk(deckAllValueCache))
			{
				NKCUtil.SetLabelText(this.m_lbUnitConditionCostTotalCurrent, "{0}/{1}", new object[]
				{
					deckAllValueCache,
					value
				});
				return;
			}
			NKCUtil.SetLabelText(this.m_lbUnitConditionCostTotalCurrent, "<color=#FF2626>{0}</color>/{1}", new object[]
			{
				deckAllValueCache,
				value
			});
		}

		// Token: 0x06006B6B RID: 27499 RVA: 0x0022FCC4 File Offset: 0x0022DEC4
		private int GetCurrentUnitTotalCost(Dictionary<int, long> dicSelectedUnit)
		{
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			int num = 0;
			int i = 0;
			while (i < this.m_currentDeckTemplet.m_lstUnitSlot.Count)
			{
				NKMDungeonEventDeckTemplet.EventDeckSlot unitSlot = this.m_currentDeckTemplet.GetUnitSlot(i);
				switch (unitSlot.m_eType)
				{
				case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_CLOSED:
					break;
				case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE:
				case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FIXED:
				case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_COUNTER:
				case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_SOLDIER:
				case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_MECHANIC:
					goto IL_73;
				case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_GUEST:
				case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_NPC:
				{
					NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(unitSlot.m_ID);
					num += unitStatTemplet.GetRespawnCost(false, null, null);
					break;
				}
				default:
					goto IL_73;
				}
				IL_A7:
				i++;
				continue;
				IL_73:
				long unitUid;
				if (!dicSelectedUnit.TryGetValue(i, out unitUid))
				{
					goto IL_A7;
				}
				NKMUnitData unitFromUID = armyData.GetUnitFromUID(unitUid);
				if (unitFromUID != null)
				{
					NKMUnitStatTemplet unitStatTemplet2 = NKMUnitManager.GetUnitStatTemplet(unitFromUID.m_UnitID);
					num += unitStatTemplet2.GetRespawnCost(false, null, null);
					goto IL_A7;
				}
				goto IL_A7;
			}
			return num;
		}

		// Token: 0x06006B6C RID: 27500 RVA: 0x0022FD93 File Offset: 0x0022DF93
		private void CheckTutorial()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.EventDeck, true);
		}

		// Token: 0x06006B6D RID: 27501 RVA: 0x0022FDA0 File Offset: 0x0022DFA0
		private void ConfirmResetStagePlayCnt()
		{
			if (this.m_StageTemplet != null)
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				int num = 0;
				if (nkmuserData != null)
				{
					num = nkmuserData.GetStageRestoreCnt(this.m_StageTemplet.Key);
				}
				if (!this.m_StageTemplet.Restorable)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_ENTER_LIMIT_OVER, null, "");
					return;
				}
				if (num >= this.m_StageTemplet.RestoreLimit)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_WARFARE_GAEM_HUD_RESTORE_LIMIT_OVER_DESC, null, "");
					return;
				}
				NKCPopupResourceWithdraw.Instance.OpenForRestoreEnterLimit(this.m_StageTemplet, delegate
				{
					NKCPacketSender.Send_NKMPacket_RESET_STAGE_PLAY_COUNT_REQ(this.m_StageTemplet.Key);
				}, num);
			}
		}

		// Token: 0x06006B6E RID: 27502 RVA: 0x0022FE3C File Offset: 0x0022E03C
		private void SetBattleCondition()
		{
			if (this.m_bInitBCTeamUpUnits)
			{
				return;
			}
			bool bValue = false;
			if (this.m_eventDeckContents == DeckContents.FIERCE_BATTLE_SUPPORT)
			{
				List<NKMBattleConditionTemplet> curBattleCondition = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr().GetCurBattleCondition(false);
				bValue = (curBattleCondition.Count > 0);
				for (int i = 0; i < curBattleCondition.Count; i++)
				{
					if (curBattleCondition[i] != null)
					{
						NKCUIFierceBattleCondition nkcuifierceBattleCondition = UnityEngine.Object.Instantiate<NKCUIFierceBattleCondition>(this.m_pfbBCSlots);
						if (nkcuifierceBattleCondition == null)
						{
							break;
						}
						nkcuifierceBattleCondition.gameObject.transform.SetParent(this.m_rtBCSlotParents);
						nkcuifierceBattleCondition.SetData(i + 1, curBattleCondition[i], false);
						this.m_lstBattleCond.Add(nkcuifierceBattleCondition);
					}
				}
			}
			else if (this.m_eventDeckContents == DeckContents.GUILD_COOP && this.m_currentDungeonTempletBase != null && this.m_currentDungeonTempletBase.BattleCondition != null)
			{
				bValue = true;
				NKCUIFierceBattleCondition nkcuifierceBattleCondition2 = UnityEngine.Object.Instantiate<NKCUIFierceBattleCondition>(this.m_pfbBCSlots);
				if (nkcuifierceBattleCondition2 != null)
				{
					nkcuifierceBattleCondition2.gameObject.transform.SetParent(this.m_rtBCSlotParents);
					nkcuifierceBattleCondition2.SetData(1, this.m_currentDungeonTempletBase.BattleCondition, true);
					this.m_lstBattleCond.Add(nkcuifierceBattleCondition2);
				}
			}
			this.m_bInitBCTeamUpUnits = true;
			NKCUtil.SetGameobjectActive(this.m_AB_UI_NKM_UI_OPERATION_EVENTDECK_BATTLE_CONDITION, bValue);
		}

		// Token: 0x06006B6F RID: 27503 RVA: 0x0022FF64 File Offset: 0x0022E164
		private void ClearBCUntSlots()
		{
			for (int i = 0; i < this.m_lstBattleCond.Count; i++)
			{
				if (!(null == this.m_lstBattleCond[i]))
				{
					this.m_lstBattleCond[i].Clear();
					UnityEngine.Object.Destroy(this.m_lstBattleCond[i].gameObject);
					this.m_lstBattleCond[i] = null;
				}
			}
			this.m_lstBattleCond.Clear();
			this.m_bInitBCTeamUpUnits = false;
		}

		// Token: 0x06006B70 RID: 27504 RVA: 0x0022FFE1 File Offset: 0x0022E1E1
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			this.UpdateAttackCost();
		}

		// Token: 0x06006B71 RID: 27505 RVA: 0x0022FFEC File Offset: 0x0022E1EC
		public override void OnUnitUpdate(NKMUserData.eChangeNotifyType eEventType, NKM_UNIT_TYPE eUnitType, long uid, NKMUnitData unitData)
		{
			foreach (KeyValuePair<int, long> keyValuePair in this.m_dicSelectedUnits)
			{
				if (keyValuePair.Value == uid)
				{
					this.m_lstSlot[keyValuePair.Key].SetData(unitData, keyValuePair.Key, false, new NKCUIUnitSelectListEventDeckSlot.OnSelectEventDeckSlot(this.OnEventDeckSlotSelect), this.m_eventDeckContents == DeckContents.FIERCE_BATTLE_SUPPORT);
					this.m_lstSlot[keyValuePair.Key].ConfirmLeader(this.m_currentLeaderIndex);
				}
			}
		}

		// Token: 0x06006B72 RID: 27506 RVA: 0x00230098 File Offset: 0x0022E298
		public override void OnOperatorUpdate(NKMUserData.eChangeNotifyType eEventType, long uid, NKMOperator operatorData)
		{
			if (eEventType == NKMUserData.eChangeNotifyType.Update && this.m_SelectedOperatorUid == uid)
			{
				this.m_OperatorSlot.SetData(operatorData, false);
				if (operatorData != null)
				{
					this.m_OperatorMainSkill.SetData(operatorData.mainSkill.id, (int)operatorData.mainSkill.level, false);
					this.m_OperatorSubSkill.SetData(operatorData.subSkill.id, (int)operatorData.subSkill.level, false);
				}
			}
		}

		// Token: 0x06006B73 RID: 27507 RVA: 0x00230106 File Offset: 0x0022E306
		public void RefreshUIByContents()
		{
			this.SetUIByContents();
		}

		// Token: 0x06006B74 RID: 27508 RVA: 0x00230110 File Offset: 0x0022E310
		private NKCUIUnitSelectList.UnitSelectListOptions MakeShipSelectOptions()
		{
			NKMDungeonEventDeckTemplet.EventDeckSlot shipSlot = this.m_currentDeckTemplet.ShipSlot;
			NKCUIUnitSelectList.UnitSelectListOptions result = new NKCUIUnitSelectList.UnitSelectListOptions(NKM_UNIT_TYPE.NUT_SHIP, false, NKM_DECK_TYPE.NDT_NORMAL, NKCUIUnitSelectList.eUnitSelectListMode.Normal, true);
			result.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
			result.lstSortOption = NKCUnitSortSystem.GetDefaultSortOptions(NKM_UNIT_TYPE.NUT_SHIP, false, false);
			result.bDescending = true;
			result.bShowRemoveSlot = (this.m_SelectedShipUid != 0L);
			result.bExcludeLockedUnit = false;
			result.bExcludeDeckedUnit = false;
			result.bCanSelectUnitInMission = true;
			result.bShowHideDeckedUnitMenu = false;
			result.bHideDeckedUnit = false;
			result.strEmptyMessage = NKCUtilString.GET_STRING_NO_EXIST_TARGET_TO_SELECT;
			result.m_SortOptions.AdditionalExcludeFilterFunc = new NKCUnitSortSystem.UnitListOptions.CustomFilterFunc(this.CheckShipCondition);
			result.m_SortOptions.bIgnoreMissionState = true;
			result.setShipFilterCategory = NKCUnitSortSystem.setDefaultShipFilterCategory;
			result.setShipSortCategory = NKCUnitSortSystem.setDefaultShipSortCategory;
			if (shipSlot.m_eType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FIXED || shipSlot.m_eType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_GUEST)
			{
				result.setOnlyIncludeUnitBaseID = new HashSet<int>();
				result.setOnlyIncludeUnitBaseID.Add(shipSlot.m_ID);
			}
			if (this.m_SelectedShipUid != 0L)
			{
				result.setExcludeUnitUID = new HashSet<long>();
				result.setExcludeUnitUID.Add(this.m_SelectedShipUid);
			}
			return result;
		}

		// Token: 0x06006B75 RID: 27509 RVA: 0x00230234 File Offset: 0x0022E434
		private void OpenOperatorSelectList()
		{
			if (!this.OperatorUnlocked)
			{
				return;
			}
			NKMDungeonEventDeckTemplet.SLOT_TYPE eType = this.m_currentDeckTemplet.OperatorSlot.m_eType;
			if (eType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_CLOSED || eType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_NPC)
			{
				return;
			}
			NKCUIUnitSelectList.UnitSelectListOptions options = this.MakeOperatorSelectOptions();
			if (this.m_SelectedOperatorUid != 0L)
			{
				options.beforeOperator = NKCOperatorUtil.GetOperatorData(this.m_SelectedOperatorUid);
			}
			options.m_strCachingUIName = this.MenuName;
			NKCUIUnitSelectList.Instance.Open(options, new NKCUIUnitSelectList.OnUnitSelectCommand(this.OnOperatorSelected), null, null, null, null);
		}

		// Token: 0x06006B76 RID: 27510 RVA: 0x002302AC File Offset: 0x0022E4AC
		private void OnClickOperatorSlot(long operatorUID)
		{
			this.OpenOperatorSelectList();
		}

		// Token: 0x06006B77 RID: 27511 RVA: 0x002302B4 File Offset: 0x0022E4B4
		private bool SetAsLeader(int leaderIndex)
		{
			if (this.m_lstSlot == null)
			{
				return false;
			}
			bool result = false;
			int count = this.m_lstSlot.Count;
			for (int i = 0; i < count; i++)
			{
				if (!(this.m_lstSlot[i] == null) && this.m_lstSlot[i].ConfirmLeader(leaderIndex))
				{
					result = true;
					this.m_currentLeaderIndex = leaderIndex;
				}
			}
			this.m_isSelectingLeader = false;
			NKCUIComStateButton csbtnLeaderSelect = this.m_csbtnLeaderSelect;
			if (csbtnLeaderSelect != null)
			{
				csbtnLeaderSelect.Select(false, false, false);
			}
			return result;
		}

		// Token: 0x06006B78 RID: 27512 RVA: 0x00230334 File Offset: 0x0022E534
		private void SetDefaultLeader()
		{
			this.m_currentLeaderIndex = -1;
			if (this.m_lstSlot == null)
			{
				return;
			}
			int count = this.m_lstSlot.Count;
			for (int i = 0; i < count; i++)
			{
				if (!(this.m_lstSlot[i] == null))
				{
					if (this.m_currentLeaderIndex < 0)
					{
						if (this.m_lstSlot[i].ConfirmLeader(i))
						{
							this.m_currentLeaderIndex = i;
						}
					}
					else
					{
						this.m_lstSlot[i].ConfirmLeader(this.m_currentLeaderIndex);
					}
				}
			}
		}

		// Token: 0x06006B79 RID: 27513 RVA: 0x002303BC File Offset: 0x0022E5BC
		private void OnClickLeaderSelect()
		{
			if (this.m_lstSlot == null)
			{
				return;
			}
			this.m_isSelectingLeader = !this.m_isSelectingLeader;
			this.m_csbtnLeaderSelect.Select(this.m_isSelectingLeader, false, false);
			int count = this.m_lstSlot.Count;
			for (int i = 0; i < count; i++)
			{
				NKCUIUnitSelectListEventDeckSlot nkcuiunitSelectListEventDeckSlot = this.m_lstSlot[i];
				if (nkcuiunitSelectListEventDeckSlot != null)
				{
					nkcuiunitSelectListEventDeckSlot.LeaderSelectState(this.m_isSelectingLeader);
				}
			}
		}

		// Token: 0x06006B7A RID: 27514 RVA: 0x0023042C File Offset: 0x0022E62C
		private void LoadDungeonDeck()
		{
			string curEventDeckKey = this.GetCurEventDeckKey();
			if (string.IsNullOrEmpty(curEventDeckKey))
			{
				return;
			}
			if (PlayerPrefs.HasKey(curEventDeckKey))
			{
				NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
				string @string = PlayerPrefs.GetString(curEventDeckKey);
				foreach (string text in @string.Split(new char[]
				{
					'&'
				}))
				{
					int num = text.IndexOf('/');
					if (num < 0)
					{
						break;
					}
					int index;
					int.TryParse(text.Substring(0, num), out index);
					long num2;
					long.TryParse(text.Substring(num + 1, text.Length - (num + 1)), out num2);
					NKMUnitData unitFromUID = armyData.GetUnitFromUID(num2);
					if (this.m_currentDeckTemplet.IsUnitFitInSlot(this.m_currentDeckTemplet.GetUnitSlot(index), unitFromUID))
					{
						this.OnUnitSelected(index, num2);
					}
				}
				int num3 = @string.IndexOf('_') + 1;
				if (num3 > 0)
				{
					int num4 = @string.IndexOf('o');
					int num5 = @string.IndexOf('l');
					int num6 = @string.Length - num3;
					if (num4 > 0)
					{
						num6 -= @string.Length - num4;
					}
					else if (num5 > 0)
					{
						num6 -= @string.Length - num5;
					}
					long num7;
					long.TryParse(@string.Substring(num3, num6), out num7);
					NKMUnitData shipFromUID = armyData.GetShipFromUID(num7);
					if (this.m_currentDeckTemplet.IsUnitFitInSlot(this.m_currentDeckTemplet.ShipSlot, shipFromUID))
					{
						this.OnShipSelected(num7);
					}
				}
				int num8 = @string.IndexOf('|') + 1;
				if (num8 > 0)
				{
					int num9 = @string.IndexOf('l');
					int num10 = @string.Length - num8;
					if (num9 > 0)
					{
						num10 -= @string.Length - num9;
					}
					long num11;
					long.TryParse(@string.Substring(num8, num10), out num11);
					NKMOperator operatorFromUId = armyData.GetOperatorFromUId(num11);
					if (this.m_currentDeckTemplet.IsOperatorFitInSlot(operatorFromUId))
					{
						this.OnOperatorSelected(num11);
					}
				}
				int num12 = @string.IndexOf('^') + 1;
				if (num12 > 0)
				{
					int length = @string.Length - num12;
					string text2 = @string.Substring(num12, length);
					NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
					long num13 = (nkmuserData != null) ? nkmuserData.m_UserUID : 0L;
					long num14 = 0L;
					if (text2.Length > 1)
					{
						long.TryParse(text2.Substring(1), out num14);
					}
					int currentLeaderIndex;
					if (num14 == num13 && int.TryParse(text2.Substring(0, 1), out currentLeaderIndex))
					{
						this.m_currentLeaderIndex = currentLeaderIndex;
					}
				}
			}
		}

		// Token: 0x06006B7B RID: 27515 RVA: 0x0023068C File Offset: 0x0022E88C
		private string GetCurEventDeckKey()
		{
			if (this.m_currentDungeonTempletBase != null)
			{
				if (this.m_eventDeckContents == DeckContents.FIERCE_BATTLE_SUPPORT)
				{
					NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
					if (nkcfierceBattleSupportDataMgr != null && nkcfierceBattleSupportDataMgr.GetStatus() == NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_ACTIVATE)
					{
						foreach (NKMFierceBossGroupTemplet nkmfierceBossGroupTemplet in NKMFierceBossGroupTemplet.Values)
						{
							if (nkmfierceBossGroupTemplet.DungeonID == this.m_currentDungeonTempletBase.m_DungeonID)
							{
								return string.Format(string.Format("NKM_PREPARE_EVENT_DECK_F_{0}", nkmfierceBossGroupTemplet.FierceBossGroupID), Array.Empty<object>());
							}
						}
					}
				}
				return string.Format(string.Format("NKM_PREPARE_EVENT_DECK_{0}", this.m_currentDungeonTempletBase.m_DungeonID), Array.Empty<object>());
			}
			if (this.m_StageTemplet != null && this.m_StageTemplet.PhaseTemplet != null)
			{
				return string.Format(string.Format("NKM_PREPARE_EVENT_DECK_{0}", this.m_StageTemplet.PhaseTemplet.Id), Array.Empty<object>());
			}
			return "";
		}

		// Token: 0x06006B7C RID: 27516 RVA: 0x0023079C File Offset: 0x0022E99C
		private void SaveDungeonDeck()
		{
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			StringBuilder stringBuilder = new StringBuilder();
			string curEventDeckKey = this.GetCurEventDeckKey();
			if (string.IsNullOrEmpty(curEventDeckKey))
			{
				return;
			}
			for (int i = 0; i < 8; i++)
			{
				if (this.m_dicSelectedUnits.ContainsKey(i))
				{
					stringBuilder.Append(string.Format("{0}/{1}&", i, this.m_dicSelectedUnits[i]));
				}
			}
			if (this.m_SelectedShipUid != 0L && armyData.IsHaveShipFromUID(this.m_SelectedShipUid))
			{
				stringBuilder.Append(string.Format("s_{0}", this.m_SelectedShipUid));
			}
			if (this.m_SelectedOperatorUid != 0L && armyData.IsHaveOperatorFromUID(this.m_SelectedOperatorUid))
			{
				stringBuilder.Append(string.Format("o|{0}", this.m_SelectedOperatorUid));
			}
			if (this.m_currentLeaderIndex != 0)
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				long num = (nkmuserData != null) ? nkmuserData.m_UserUID : 0L;
				stringBuilder.Append(string.Format("l^{0}{1}", this.m_currentLeaderIndex, num));
			}
			PlayerPrefs.SetString(curEventDeckKey, stringBuilder.ToString());
		}

		// Token: 0x06006B7D RID: 27517 RVA: 0x002308C4 File Offset: 0x0022EAC4
		private void OnDragBegin(PointerEventData eventData, int beginIndex)
		{
			if (this.m_bDrag)
			{
				return;
			}
			NKMDungeonEventDeckTemplet.SLOT_TYPE eType = this.m_currentDeckTemplet.GetUnitSlot(beginIndex).m_eType;
			if (eType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_CLOSED || eType - NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FIXED <= 2)
			{
				return;
			}
			long unitUid;
			if (this.m_dicSelectedUnits.TryGetValue(beginIndex, out unitUid))
			{
				this.m_dragBeginIndex = beginIndex;
				NKMUnitData unitFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(unitUid);
				NKCUtil.SetImageSprite(this.m_imgDragObject, NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, unitFromUID), false);
				NKCUtil.SetGameobjectActive(this.m_imgDragObject, true);
				this.MoveDragObject(eventData.position);
				this.m_bDrag = true;
			}
		}

		// Token: 0x06006B7E RID: 27518 RVA: 0x00230950 File Offset: 0x0022EB50
		private void OnDrag(PointerEventData eventData, int endIndex)
		{
			if (this.m_bDrag)
			{
				this.MoveDragObject(eventData.position);
			}
		}

		// Token: 0x06006B7F RID: 27519 RVA: 0x00230966 File Offset: 0x0022EB66
		private void OnDragEnd(PointerEventData eventData, int endIndex)
		{
			if (this.m_bDrag)
			{
				this.ResetDrag();
			}
		}

		// Token: 0x06006B80 RID: 27520 RVA: 0x00230976 File Offset: 0x0022EB76
		private void OnDrop(PointerEventData eventData, int endIndex)
		{
			if (this.m_bDrag)
			{
				this.SwapSlot(this.m_dragBeginIndex, endIndex);
				this.ResetDrag();
			}
		}

		// Token: 0x06006B81 RID: 27521 RVA: 0x00230994 File Offset: 0x0022EB94
		private void SwapSlot(int indexA, int indexB)
		{
			if (indexA == indexB)
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			long num;
			bool flag;
			if (this.m_dicSelectedUnits.TryGetValue(indexA, out num))
			{
				flag = (NKMDungeonManager.CheckEventSlot(nkmuserData.m_ArmyData, this.m_currentDeckTemplet, this.m_currentDeckTemplet.GetUnitSlot(indexB), num, NKM_UNIT_TYPE.NUT_NORMAL) == NKM_ERROR_CODE.NEC_OK);
			}
			else
			{
				num = 0L;
				flag = true;
			}
			long num2;
			bool flag2;
			if (this.m_dicSelectedUnits.TryGetValue(indexB, out num2))
			{
				flag2 = (NKMDungeonManager.CheckEventSlot(nkmuserData.m_ArmyData, this.m_currentDeckTemplet, this.m_currentDeckTemplet.GetUnitSlot(indexA), num2, NKM_UNIT_TYPE.NUT_NORMAL) == NKM_ERROR_CODE.NEC_OK);
			}
			else
			{
				num2 = 0L;
				flag2 = true;
			}
			if (!flag || !flag2)
			{
				return;
			}
			this.m_dicSelectedUnits.Remove(indexA);
			this.m_dicSelectedUnits.Remove(indexB);
			this.OnUnitSelected(indexA, num2);
			this.OnUnitSelected(indexB, num);
			if (this.m_currentLeaderIndex == indexA)
			{
				this.SetAsLeader(indexB);
				return;
			}
			if (this.m_currentLeaderIndex == indexB)
			{
				this.SetAsLeader(indexA);
				return;
			}
			this.SetAsLeader(this.m_currentLeaderIndex);
		}

		// Token: 0x06006B82 RID: 27522 RVA: 0x00230A85 File Offset: 0x0022EC85
		private void ResetDrag()
		{
			this.m_bDrag = false;
			this.m_dragBeginIndex = -1;
			NKCUtil.SetGameobjectActive(this.m_imgDragObject, false);
		}

		// Token: 0x17001262 RID: 4706
		// (get) Token: 0x06006B83 RID: 27523 RVA: 0x00230AA1 File Offset: 0x0022ECA1
		private RectTransform rectTransform
		{
			get
			{
				if (this.m_rectTransform == null)
				{
					this.m_rectTransform = base.GetComponent<RectTransform>();
				}
				return this.m_rectTransform;
			}
		}

		// Token: 0x06006B84 RID: 27524 RVA: 0x00230AC4 File Offset: 0x0022ECC4
		private void MoveDragObject(Vector2 touchPos)
		{
			Vector3 zero = Vector3.zero;
			RectTransformUtility.ScreenPointToWorldPointInRectangle(this.rectTransform, touchPos, NKCCamera.GetSubUICamera(), out zero);
			zero.x /= this.rectTransform.lossyScale.x;
			zero.y /= this.rectTransform.lossyScale.y;
			zero.z = 0f;
			this.m_imgDragObject.transform.localPosition = zero;
		}

		// Token: 0x06006B87 RID: 27527 RVA: 0x00230BB8 File Offset: 0x0022EDB8
		[CompilerGenerated]
		internal static bool <AutoPrepare>g__CheckDuplicateUnit|147_0(int unitID, ref NKCUIPrepareEventDeck.<>c__DisplayClass147_0 A_1)
		{
			NKMUnitTempletBase nkmunitTempletBase = NKMUnitTempletBase.Find(unitID);
			foreach (int targetUnit in A_1.hsDuplicateUnit)
			{
				if (nkmunitTempletBase.IsSameBaseUnit(targetUnit))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040056CF RID: 22223
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_operation";

		// Token: 0x040056D0 RID: 22224
		private const string UI_ASSET_NAME = "NKM_UI_OPERATION_EVENTDECK";

		// Token: 0x040056D1 RID: 22225
		private static NKCUIPrepareEventDeck m_Instance;

		// Token: 0x040056D2 RID: 22226
		[Header("일반 전투")]
		public GameObject m_objNormalInfo;

		// Token: 0x040056D3 RID: 22227
		public Text m_lbTitle;

		// Token: 0x040056D4 RID: 22228
		public Text m_lbSubTitle;

		// Token: 0x040056D5 RID: 22229
		[Header("길드 협력전 전용")]
		public GameObject m_objGuildCoopInfo;

		// Token: 0x040056D6 RID: 22230
		public Text m_lbTitleGuildCoop;

		// Token: 0x040056D7 RID: 22231
		public Text m_lbArenaNum;

		// Token: 0x040056D8 RID: 22232
		public Text m_lbClearPercent;

		// Token: 0x040056D9 RID: 22233
		public Image m_imgClearGauge;

		// Token: 0x040056DA RID: 22234
		public Text m_lbNextArtifactCount;

		// Token: 0x040056DB RID: 22235
		[Header("함선")]
		public Text m_lbShipLevel;

		// Token: 0x040056DC RID: 22236
		public Text m_lbShipName;

		// Token: 0x040056DD RID: 22237
		public RectTransform m_rtShipRoot;

		// Token: 0x040056DE RID: 22238
		public NKCUIComButton m_cbtnChngeShip;

		// Token: 0x040056DF RID: 22239
		[Header("오퍼레이터")]
		public NKCUIOperatorDeckSlot m_OperatorSlot;

		// Token: 0x040056E0 RID: 22240
		public NKCUIComStateButton m_csbtnCanChangeOperator;

		// Token: 0x040056E1 RID: 22241
		public GameObject m_OperatorSkill;

		// Token: 0x040056E2 RID: 22242
		public NKCUIOperatorSkill m_OperatorMainSkill;

		// Token: 0x040056E3 RID: 22243
		public NKCUIOperatorSkill m_OperatorSubSkill;

		// Token: 0x040056E4 RID: 22244
		public NKCUIOperatorTacticalSkillCombo m_OperatorSkillCombo;

		// Token: 0x040056E5 RID: 22245
		[Header("우하단 버튼")]
		public NKCUIComButton m_cbtnAutoSetup;

		// Token: 0x040056E6 RID: 22246
		public NKCUIComButton m_cbtnClearAll;

		// Token: 0x040056E7 RID: 22247
		public NKCUIComButton m_cbtnBegin;

		// Token: 0x040056E8 RID: 22248
		public NKCUIComResourceButton m_ResourceBtn;

		// Token: 0x040056E9 RID: 22249
		public List<NKCUIUnitSelectListEventDeckSlot> m_lstSlot;

		// Token: 0x040056EA RID: 22250
		[Header("편성제한")]
		public GameObject m_objRootDeckCondition;

		// Token: 0x040056EB RID: 22251
		public RectTransform m_rtConditionSlotRoot;

		// Token: 0x040056EC RID: 22252
		public NKCUIPrepareEventDeckConditionSlot m_pfbConditionSlot;

		// Token: 0x040056ED RID: 22253
		private List<NKCUIPrepareEventDeckConditionSlot> m_lstConditionSlots = new List<NKCUIPrepareEventDeckConditionSlot>();

		// Token: 0x040056EE RID: 22254
		[Header("총 출격비용(오른쪽 출격버튼 위)")]
		public GameObject m_objUnitConditionCostTotal;

		// Token: 0x040056EF RID: 22255
		public Text m_lbUnitConditionCostTotal;

		// Token: 0x040056F0 RID: 22256
		public Text m_lbUnitConditionCostTotalCurrent;

		// Token: 0x040056F1 RID: 22257
		[Header("입장 제한")]
		public GameObject m_AB_UI_NKM_UI_OPERATION_EVENTDECK_EnterLimit;

		// Token: 0x040056F2 RID: 22258
		public Text m_EnterLimit_TEXT;

		// Token: 0x040056F3 RID: 22259
		public Image m_AB_UI_NKM_UI_OPERATION_EVENTDECK_BUTTON_ICON;

		// Token: 0x040056F4 RID: 22260
		public Text m_AB_UI_NKM_UI_OPERATION_EVENTDECK_BUTTON_TEXT;

		// Token: 0x040056F5 RID: 22261
		[Header("전투스킵")]
		public GameObject m_objSkip;

		// Token: 0x040056F6 RID: 22262
		public NKCUIOperationSkip m_NKCUIOperationSkip;

		// Token: 0x040056F7 RID: 22263
		public NKCUIComToggle m_tglSkip;

		// Token: 0x040056F8 RID: 22264
		[Header("등장 적 부대")]
		public NKCUIComEnemyList m_NKCUIComEnemyList;

		// Token: 0x040056F9 RID: 22265
		[Header("컨텐츠에 따라 변경되는 항목")]
		public Image m_imgTitleDeco;

		// Token: 0x040056FA RID: 22266
		public Image m_imgInfoBG;

		// Token: 0x040056FB RID: 22267
		public Color m_ShadowColor;

		// Token: 0x040056FC RID: 22268
		public Color m_FierceColor;

		// Token: 0x040056FD RID: 22269
		public Color m_NormalColor;

		// Token: 0x040056FE RID: 22270
		public Image m_imgDeckBG;

		// Token: 0x040056FF RID: 22271
		public GameObject m_objBlackBG;

		// Token: 0x04005700 RID: 22272
		public GameObject m_objShadowDust;

		// Token: 0x04005701 RID: 22273
		[Header("드래그")]
		public Image m_imgDragObject;

		// Token: 0x04005702 RID: 22274
		[Header("격전지원")]
		public GameObject m_AB_UI_NKM_UI_OPERATION_EVENTDECK_SHIP_FIERCE_BATTLE;

		// Token: 0x04005703 RID: 22275
		public Text m_NKM_UI_UNIT_SELECT_LIST_FIERCE_BATTLE_TEXT;

		// Token: 0x04005704 RID: 22276
		public GameObject m_AB_UI_NKM_UI_OPERATION_EVENTDECK_BATTLE_CONDITION;

		// Token: 0x04005705 RID: 22277
		public GameObject m_objNotPossibleOperator;

		// Token: 0x04005706 RID: 22278
		public Text m_lbNotPossibleOpeatorDescription;

		// Token: 0x04005707 RID: 22279
		[Header("battle condition")]
		public RectTransform m_rtBCSlotParents;

		// Token: 0x04005708 RID: 22280
		public NKCUIFierceBattleCondition m_pfbBCSlots;

		// Token: 0x04005709 RID: 22281
		private List<NKCUIFierceBattleCondition> m_lstBattleCond = new List<NKCUIFierceBattleCondition>();

		// Token: 0x0400570A RID: 22282
		[Space]
		public GameObject m_AB_UI_NKM_UI_OPERATION_EVENTDECK_ENEMY_FIERCE_BATTLE;

		// Token: 0x0400570B RID: 22283
		public Image m_FIERCE_BATTLE_BOSS_IMAGE_Root;

		// Token: 0x0400570C RID: 22284
		public GameObject m_FIERCE_BATTLE_BOSS_INFO;

		// Token: 0x0400570D RID: 22285
		public Text m_BOSS_LEVEL_Text;

		// Token: 0x0400570E RID: 22286
		public Image m_CLASS_Icon;

		// Token: 0x0400570F RID: 22287
		public Text m_CLASS_Text;

		// Token: 0x04005710 RID: 22288
		public NKCUIComStateButton m_csbtnLeaderSelect;

		// Token: 0x04005711 RID: 22289
		private int m_CurrMultiplyRewardCount = 1;

		// Token: 0x04005712 RID: 22290
		private bool m_bOperationSkip;

		// Token: 0x04005713 RID: 22291
		private NKMStageTempletV2 m_StageTemplet;

		// Token: 0x04005714 RID: 22292
		private NKMDungeonTempletBase m_currentDungeonTempletBase;

		// Token: 0x04005715 RID: 22293
		private NKMDungeonEventDeckTemplet m_currentDeckTemplet;

		// Token: 0x04005716 RID: 22294
		private NKCASUIUnitIllust m_shipIllust;

		// Token: 0x04005717 RID: 22295
		private int m_currentIndex;

		// Token: 0x04005718 RID: 22296
		private int m_currentLeaderIndex;

		// Token: 0x04005719 RID: 22297
		private long m_SelectedShipUid;

		// Token: 0x0400571A RID: 22298
		private long m_SelectedOperatorUid;

		// Token: 0x0400571B RID: 22299
		private Dictionary<int, long> m_dicSelectedUnits = new Dictionary<int, long>();

		// Token: 0x0400571C RID: 22300
		private DeckContents m_eventDeckContents;

		// Token: 0x0400571D RID: 22301
		private NKCUIPrepareEventDeck.OnEventDeckConfirm dOnEventDeckConfirm;

		// Token: 0x0400571E RID: 22302
		private NKCUIPrepareEventDeck.OnBackButtonEvent dOnBackButtonEvent;

		// Token: 0x0400571F RID: 22303
		private List<NKCDeckViewUnitSlot> m_lstEnemySlot = new List<NKCDeckViewUnitSlot>();

		// Token: 0x04005720 RID: 22304
		private bool m_isSelectingLeader;

		// Token: 0x04005721 RID: 22305
		private NKMDungeonEventDeckTemplet.SLOT_TYPE m_oldSlotType = NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE;

		// Token: 0x04005722 RID: 22306
		private Dictionary<NKMDeckCondition.DECK_CONDITION, int> m_dicAllDeckValueCache = new Dictionary<NKMDeckCondition.DECK_CONDITION, int>();

		// Token: 0x04005723 RID: 22307
		private int m_iCostItemID;

		// Token: 0x04005724 RID: 22308
		private int m_iCostItemCount;

		// Token: 0x04005725 RID: 22309
		private bool m_bInitBCTeamUpUnits;

		// Token: 0x04005726 RID: 22310
		private bool m_bDrag;

		// Token: 0x04005727 RID: 22311
		private int m_dragBeginIndex = -1;

		// Token: 0x04005728 RID: 22312
		private RectTransform m_rectTransform;

		// Token: 0x020016D5 RID: 5845
		// (Invoke) Token: 0x0600B179 RID: 45433
		public delegate void OnEventDeckConfirm(NKMStageTempletV2 stageTemplet, NKMDungeonTempletBase dungeonTempletBase, NKMEventDeckData eventDeckData);

		// Token: 0x020016D6 RID: 5846
		// (Invoke) Token: 0x0600B17D RID: 45437
		public delegate void OnBackButtonEvent();
	}
}
