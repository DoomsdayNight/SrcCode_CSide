using System;
using System.Collections.Generic;
using ClientPacket.User;
using NKC.Publisher;
using NKC.UI.NPC;
using NKC.UI.Tooltip;
using NKM;
using NKM.Shop;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A3D RID: 2621
	public class NKCPopupDungeonInfo : NKCUIBase
	{
		// Token: 0x17001324 RID: 4900
		// (get) Token: 0x060072CC RID: 29388 RVA: 0x00262389 File Offset: 0x00260589
		public override NKCUIManager.eUIUnloadFlag UnloadFlag
		{
			get
			{
				return NKCUIManager.eUIUnloadFlag.DEFAULT;
			}
		}

		// Token: 0x17001325 RID: 4901
		// (get) Token: 0x060072CD RID: 29389 RVA: 0x0026238C File Offset: 0x0026058C
		public static NKCPopupDungeonInfo Instance
		{
			get
			{
				if (NKCPopupDungeonInfo.m_Instance == null)
				{
					NKCPopupDungeonInfo.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupDungeonInfo>("ab_ui_nkm_ui_operation", "NKM_UI_POPUP_DUNGEON", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupDungeonInfo.CleanupInstance)).GetInstance<NKCPopupDungeonInfo>();
					NKCPopupDungeonInfo.m_Instance.InitUI();
				}
				return NKCPopupDungeonInfo.m_Instance;
			}
		}

		// Token: 0x17001326 RID: 4902
		// (get) Token: 0x060072CE RID: 29390 RVA: 0x002623DB File Offset: 0x002605DB
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001327 RID: 4903
		// (get) Token: 0x060072CF RID: 29391 RVA: 0x002623DE File Offset: 0x002605DE
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_MENU_NAME_DUNGEON_POPUP;
			}
		}

		// Token: 0x17001328 RID: 4904
		// (get) Token: 0x060072D0 RID: 29392 RVA: 0x002623E5 File Offset: 0x002605E5
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupDungeonInfo.m_Instance != null && NKCPopupDungeonInfo.m_Instance.IsOpen;
			}
		}

		// Token: 0x060072D1 RID: 29393 RVA: 0x00262400 File Offset: 0x00260600
		public static void PreloadInstance()
		{
			if (NKCPopupDungeonInfo.m_Instance == null)
			{
				NKCUtil.SetGameobjectActive(NKCPopupDungeonInfo.Instance, false);
			}
		}

		// Token: 0x060072D2 RID: 29394 RVA: 0x0026241C File Offset: 0x0026061C
		public void InitUI()
		{
			if (this.m_NKCUIOpenAnimator == null)
			{
				this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			}
			this.m_NKCUIComDungeonRewardList.InitUI();
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(delegate(BaseEventData eventData)
			{
				base.Close();
			});
			this.m_eventTriggerBG.triggers.Add(entry);
			this.m_btnCancel.PointerClick.RemoveAllListeners();
			this.m_btnCancel.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_btnNext.PointerClick.RemoveAllListeners();
			this.m_btnNext.PointerClick.AddListener(new UnityAction(this.OnOK));
			NKCUtil.SetHotkey(this.m_btnNext, HotkeyEventType.Confirm);
			this.m_Tgl_CUT_SCEN_CHECK.OnValueChanged.RemoveAllListeners();
			this.m_Tgl_CUT_SCEN_CHECK.OnValueChanged.AddListener(new UnityAction<bool>(this.OnChangedCutscenCheck));
			this.m_NKCUIComEnemyList.InitUI();
			base.gameObject.SetActive(false);
		}

		// Token: 0x060072D3 RID: 29395 RVA: 0x0026252C File Offset: 0x0026072C
		public void Open(NKMStageTempletV2 stageTemplet, NKCPopupDungeonInfo.OnButton onOkButton = null, bool isPlaying = false)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null || stageTemplet == null)
			{
				return;
			}
			this.dOnOKButton = onOkButton;
			base.gameObject.SetActive(true);
			this.m_StageTemplet = stageTemplet;
			this.m_NKCUIComDungeonRewardList.CreateRewardSlotDataList(myUserData, stageTemplet, stageTemplet.m_StageBattleStrID);
			NKCUtil.SetGameobjectActive(this.m_objDungeonBoss, false);
			NKMEpisodeTempletV2 episodeTemplet = stageTemplet.EpisodeTemplet;
			this.SetEpisodeTitle(this.m_lbEpisodeTitle, episodeTemplet);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_POPUP_TRAINING_ICON, stageTemplet.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_PRACTICE);
			bool bCutscenDungeon = false;
			Color successTextColor = new Color(1f, 1f, 1f, 1f);
			Color failTextColor = new Color(0.4392157f, 0.48235294f, 0.5176471f, 1f);
			string stageName;
			switch (stageTemplet.m_STAGE_TYPE)
			{
			case STAGE_TYPE.ST_WARFARE:
			{
				NKMWarfareTemplet warfareTemplet = this.m_StageTemplet.WarfareTemplet;
				if (warfareTemplet == null)
				{
					return;
				}
				stageName = warfareTemplet.GetWarfareName();
				this.SetUIByStageWarfareData(myUserData, warfareTemplet, successTextColor, failTextColor);
				break;
			}
			case STAGE_TYPE.ST_DUNGEON:
			{
				NKMDungeonTempletBase dungeonTempletBase = this.m_StageTemplet.DungeonTempletBase;
				if (dungeonTempletBase == null)
				{
					return;
				}
				bCutscenDungeon = (dungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_CUTSCENE);
				stageName = dungeonTempletBase.GetDungeonName();
				this.SetUIByStageDungeonData(myUserData, stageTemplet, dungeonTempletBase, episodeTemplet, successTextColor, failTextColor);
				break;
			}
			case STAGE_TYPE.ST_PHASE:
			{
				NKMPhaseTemplet phaseTemplet = this.m_StageTemplet.PhaseTemplet;
				if (phaseTemplet == null)
				{
					return;
				}
				bCutscenDungeon = false;
				stageName = phaseTemplet.GetName();
				this.SetUIByStagePhaseData(myUserData, stageTemplet, phaseTemplet, episodeTemplet, successTextColor, failTextColor);
				break;
			}
			default:
				return;
			}
			bool bCheckDailyDungeon = this.IsDailyDungeon(episodeTemplet);
			this.SetDungeonName(stageTemplet, this.m_lbDungeonName, stageName, bCutscenDungeon, bCheckDailyDungeon);
			this.SetDungeonDesc(this.m_lbDungeonDesc, stageTemplet.GetStageDesc());
			if (this.m_NKCUIComEnemyList != null && this.m_NKCUIComEnemyList.gameObject.activeSelf)
			{
				this.m_NKCUIComEnemyList.SetData(stageTemplet);
			}
			bool flag = this.IsEnterConditionLimited(stageTemplet);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_POPUP_EnterLimit, flag);
			if (flag)
			{
				this.SetEnterLimitCondition(this.m_EnterLimit_TEXT, stageTemplet);
			}
			this.SetDungeonBonus(this.m_BonusType, this.m_BonusType_Icon, stageTemplet);
			this.SetStageRequiredItem(this.m_NKM_UI_OPERATION_POPUP_ETERNIUM, this.m_NKM_UI_OPERATION_POPUP_ETERNIUM_ICON, this.m_NKM_UI_OPERATION_POPUP_ETERNIUM_TEXT, stageTemplet);
			if (string.Equals(stageTemplet.m_StageBattleStrID, "NKM_WARFARE_EP1_1_1"))
			{
				NKCPublisherModule.Notice.OpenPromotionalBanner(NKCPublisherModule.NKCPMNotice.eOptionalBannerPlaces.EP1Start, null);
			}
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			if (stageTemplet.m_STAGE_TYPE == STAGE_TYPE.ST_PHASE && stageTemplet.PhaseTemplet != null)
			{
				NKCUtil.SetGameobjectActive(this.m_objPhase, true);
				NKCUtil.SetLabelText(this.m_lbPhase, NKCStringTable.GetString("SI_DP_STAGE_PHASE_COUNT", new object[]
				{
					stageTemplet.PhaseTemplet.GetPhaseCount()
				}));
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objPhase, false);
			}
			this.SetOKButtonText(isPlaying);
			base.UIOpened(true);
		}

		// Token: 0x060072D4 RID: 29396 RVA: 0x002627CE File Offset: 0x002609CE
		public void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
				this.m_NKCUIComDungeonRewardList.ShowRewardListUpdate();
			}
		}

		// Token: 0x060072D5 RID: 29397 RVA: 0x002627EE File Offset: 0x002609EE
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
			if (this.m_unitIllust != null)
			{
				this.m_unitIllust.Unload();
				this.m_unitIllust = null;
			}
			this.ClearTeamUPData();
		}

		// Token: 0x060072D6 RID: 29398 RVA: 0x0026281C File Offset: 0x00260A1C
		public void OnOK()
		{
			this.CloseWithCallback();
		}

		// Token: 0x060072D7 RID: 29399 RVA: 0x00262824 File Offset: 0x00260A24
		public void CloseWithCallback()
		{
			base.Close();
			if (this.dOnOKButton != null)
			{
				this.dOnOKButton(this.m_StageTemplet);
			}
		}

		// Token: 0x060072D8 RID: 29400 RVA: 0x00262845 File Offset: 0x00260A45
		public void OnRecv(NKMPacket_GAME_OPTION_PLAY_CUTSCENE_ACK cNKMPacket_GAME_OPTION_PLAY_CUTSCENE_ACK)
		{
			if (!cNKMPacket_GAME_OPTION_PLAY_CUTSCENE_ACK.isPlayCutscene)
			{
				this.m_Tgl_CUT_SCEN_CHECK.Select(false, true, false);
				return;
			}
			this.m_Tgl_CUT_SCEN_CHECK.Select(true, true, false);
		}

		// Token: 0x060072D9 RID: 29401 RVA: 0x00262870 File Offset: 0x00260A70
		private void OnChangedCutscenCheck(bool bCheck)
		{
			NKMPacket_GAME_OPTION_PLAY_CUTSCENE_REQ nkmpacket_GAME_OPTION_PLAY_CUTSCENE_REQ = new NKMPacket_GAME_OPTION_PLAY_CUTSCENE_REQ();
			nkmpacket_GAME_OPTION_PLAY_CUTSCENE_REQ.isPlayCutscene = this.m_Tgl_CUT_SCEN_CHECK.m_bChecked;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GAME_OPTION_PLAY_CUTSCENE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060072DA RID: 29402 RVA: 0x002628A7 File Offset: 0x00260AA7
		private bool IsEnterConditionLimited(NKMStageTempletV2 stageTemplet)
		{
			return stageTemplet != null && stageTemplet.EnterLimit > 0;
		}

		// Token: 0x060072DB RID: 29403 RVA: 0x002628B7 File Offset: 0x00260AB7
		private void SetEpisodeTitle(Text episodeTitleText, NKMEpisodeTempletV2 cNKMEpisodeTemplet)
		{
			if (cNKMEpisodeTemplet == null)
			{
				return;
			}
			NKCUtil.SetLabelText(this.m_lbEpisodeTitle, cNKMEpisodeTemplet.GetEpisodeTitle());
		}

		// Token: 0x060072DC RID: 29404 RVA: 0x002628D0 File Offset: 0x00260AD0
		private void SetUIByStageWarfareData(NKMUserData cNKMUserData, NKMWarfareTemplet cNKMWarfareTemplet, Color successTextColor, Color failTextColor)
		{
			if (cNKMUserData == null || cNKMWarfareTemplet == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_NKCUIComDungeonMission, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_POPUP_CHARACTER, false);
			NKCUtil.SetGameobjectActive(this.m_NKCUIComEnemyList, true);
			this.SetCutscenBtnUI(NKCPopupDungeonInfo.HasCutScen(cNKMWarfareTemplet));
			this.m_NKCUIComDungeonMission.SetData(cNKMWarfareTemplet, false);
			this.SetBattleConditionUI(cNKMWarfareTemplet);
		}

		// Token: 0x060072DD RID: 29405 RVA: 0x00262928 File Offset: 0x00260B28
		private void SetUIByStageDungeonData(NKMUserData cNKMUserData, NKMStageTempletV2 stageTemplet, NKMDungeonTempletBase cNKMDungeonTempletBase, NKMEpisodeTempletV2 cNKMEpisodeTemplet, Color successTextColor, Color failTextColor)
		{
			if (cNKMUserData == null || stageTemplet == null || cNKMDungeonTempletBase == null || cNKMEpisodeTemplet == null)
			{
				return;
			}
			if (cNKMEpisodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_DAILY)
			{
				NKCUtil.SetGameobjectActive(this.m_NKCUIComDungeonMission, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_POPUP_CHARACTER, false);
				NKCUtil.SetGameobjectActive(this.m_NKCUIComEnemyList, true);
				this.SetCutscenBtnUI(cNKMDungeonTempletBase.HasCutscen());
			}
			else if (cNKMDungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_CUTSCENE || stageTemplet.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_PRACTICE || stageTemplet.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_TUTORIAL)
			{
				if (cNKMDungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_CUTSCENE || stageTemplet.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_TUTORIAL)
				{
					if (string.IsNullOrEmpty(stageTemplet.m_StageCharStr))
					{
						this.m_unitIllust = this.AddSpineIllustration("NKM_NPC_OPERATOR_LENA");
					}
					else
					{
						this.m_unitIllust = this.AddSpineIllustration(stageTemplet.m_StageCharStr);
					}
					if (this.m_unitIllust != null && this.m_NKCUINPCSpineIllust != null)
					{
						this.m_NKCUINPCSpineIllust.m_spUnitIllust = this.m_unitIllust.m_SpineIllustInstant_SkeletonGraphic;
						this.m_unitIllust.SetParent(this.m_NKCUINPCSpineIllust.transform, false);
						this.m_unitIllust.SetAnchoredPosition(this.DEFAULT_CHAR_POS);
						if (this.m_unitIllust.HasAnimation(NKCASUIUnitIllust.eAnimation.UNIT_IDLE))
						{
							this.m_unitIllust.SetAnimation(NKCASUIUnitIllust.eAnimation.UNIT_IDLE, true, 0, true, 0f, true);
						}
					}
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_POPUP_CHARACTER, true);
					if (stageTemplet.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_TUTORIAL)
					{
						this.SetCutscenBtnUI(cNKMDungeonTempletBase.HasCutscen());
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_POPUP_CUT_SCEN, false);
					}
				}
				else if (stageTemplet.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_PRACTICE)
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_POPUP_CHARACTER, false);
					this.SetCutscenBtnUI(cNKMDungeonTempletBase.HasCutscen());
				}
				NKCUtil.SetGameobjectActive(this.m_NKCUIComDungeonMission, false);
				NKCUtil.SetGameobjectActive(this.m_NKCUIComEnemyList, false);
			}
			else
			{
				this.SetCutscenBtnUI(cNKMDungeonTempletBase.HasCutscen());
				NKCUtil.SetGameobjectActive(this.m_NKCUIComDungeonMission, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_POPUP_CHARACTER, false);
				NKCUtil.SetGameobjectActive(this.m_NKCUIComEnemyList, true);
			}
			if (this.m_NKCUIComDungeonMission.gameObject.activeSelf)
			{
				this.m_NKCUIComDungeonMission.SetData(cNKMDungeonTempletBase, false);
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_POPUP_SCENARIO_BC, false);
			this.SetBattleConditionUI(cNKMDungeonTempletBase);
		}

		// Token: 0x060072DE RID: 29406 RVA: 0x00262B2C File Offset: 0x00260D2C
		private void SetUIByStagePhaseData(NKMUserData cNKMUserData, NKMStageTempletV2 stageTemplet, NKMPhaseTemplet phaseTemplet, NKMEpisodeTempletV2 cNKMEpisodeTemplet, Color successTextColor, Color failTextColor)
		{
			if (cNKMUserData == null || stageTemplet == null || phaseTemplet == null || cNKMEpisodeTemplet == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_NKCUIComDungeonMission, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_POPUP_CHARACTER, false);
			NKCUtil.SetGameobjectActive(this.m_NKCUIComEnemyList, true);
			this.SetCutscenBtnUI(NKCPopupDungeonInfo.HasCutScen(phaseTemplet));
			this.m_NKCUIComDungeonMission.SetData(phaseTemplet, false);
			this.SetBattleConditionUI(phaseTemplet);
		}

		// Token: 0x060072DF RID: 29407 RVA: 0x00262B8B File Offset: 0x00260D8B
		private bool IsDailyDungeon(NKMEpisodeTempletV2 cNKMEpisodeTemplet)
		{
			return cNKMEpisodeTemplet != null && cNKMEpisodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_DAILY;
		}

		// Token: 0x060072E0 RID: 29408 RVA: 0x00262B9C File Offset: 0x00260D9C
		private void SetDungeonName(NKMStageTempletV2 stageTemplet, Text lbStage, string stageName, bool bCutscenDungeon, bool bCheckDailyDungeon)
		{
			if (stageTemplet == null || lbStage == null)
			{
				return;
			}
			if (stageTemplet.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_PRACTICE)
			{
				lbStage.text = string.Format(NKCUtilString.GET_STRING_EP_TRAINING_NUMBER, stageTemplet.m_StageUINum) + " " + stageName;
				lbStage.color = NKCUtil.GetColor("#4EC2F3");
				return;
			}
			lbStage.color = NKCUtil.GetColor("#FFFFFF");
			if (bCutscenDungeon)
			{
				lbStage.text = string.Format(NKCUtilString.GET_STRING_EP_CUTSCEN_NUMBER, stageTemplet.m_StageUINum) + " " + stageName;
				return;
			}
			if (bCheckDailyDungeon)
			{
				lbStage.text = string.Format("{0} {1}", stageName, NKCUtilString.GetDailyDungeonLVDesc(stageTemplet.m_StageUINum));
				return;
			}
			lbStage.text = string.Format("{0}-{1}. {2}", stageTemplet.ActId, stageTemplet.m_StageUINum, stageName);
		}

		// Token: 0x060072E1 RID: 29409 RVA: 0x00262C79 File Offset: 0x00260E79
		private void SetDungeonDesc(Text dungeonDesc, string desc)
		{
			NKCUtil.SetLabelText(dungeonDesc, desc);
		}

		// Token: 0x060072E2 RID: 29410 RVA: 0x00262C84 File Offset: 0x00260E84
		private void SetEnterLimitCondition(Text enterLimitText, NKMStageTempletV2 stageTemplet)
		{
			if (stageTemplet == null)
			{
				return;
			}
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
			NKCUtil.SetLabelText(enterLimitText, msg);
			if (stageTemplet.EnterLimit - statePlayCnt <= 0)
			{
				NKCUtil.SetLabelTextColor(enterLimitText, Color.red);
				return;
			}
			NKCUtil.SetLabelTextColor(enterLimitText, Color.white);
		}

		// Token: 0x060072E3 RID: 29411 RVA: 0x00262DA4 File Offset: 0x00260FA4
		private void SetDungeonBonus(GameObject bonusType, Image bonusTypeIcon, NKMStageTempletV2 stageTemplet)
		{
			if (stageTemplet == null)
			{
				return;
			}
			if (stageTemplet.m_BuffType.Equals(RewardTuningType.None))
			{
				NKCUtil.SetGameobjectActive(bonusType, false);
				return;
			}
			NKCUtil.SetGameobjectActive(bonusType, true);
			NKCUtil.SetImageSprite(bonusTypeIcon, NKCUtil.GetBounsTypeIcon(stageTemplet.m_BuffType, false), false);
		}

		// Token: 0x060072E4 RID: 29412 RVA: 0x00262DF0 File Offset: 0x00260FF0
		private void SetStageRequiredItem(GameObject itemObject, Image itemIcon, Text itemCount, NKMStageTempletV2 stageTemplet)
		{
			if (stageTemplet == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(itemObject, stageTemplet.m_StageReqItemID != 0 && stageTemplet.m_StageReqItemCount > 0);
			int stageReqItemCount = stageTemplet.m_StageReqItemCount;
			if (stageTemplet.m_StageReqItemID == 2)
			{
				if (stageTemplet.WarfareTemplet != null)
				{
					NKCCompanyBuff.SetDiscountOfEterniumInEnteringWarfare(NKCScenManager.CurrentUserData().m_companyBuffDataList, ref stageReqItemCount);
				}
				else if (stageTemplet.DungeonTempletBase != null)
				{
					NKCCompanyBuff.SetDiscountOfEterniumInEnteringDungeon(NKCScenManager.CurrentUserData().m_companyBuffDataList, ref stageReqItemCount);
				}
				else if (stageTemplet.PhaseTemplet != null)
				{
					NKCCompanyBuff.SetDiscountOfEterniumInEnteringDungeon(NKCScenManager.CurrentUserData().m_companyBuffDataList, ref stageReqItemCount);
				}
			}
			NKCUtil.SetLabelText(itemCount, stageReqItemCount.ToString());
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(stageTemplet.m_StageReqItemID);
			if (itemMiscTempletByID != null)
			{
				Sprite orLoadMiscItemSmallIcon = NKCResourceUtility.GetOrLoadMiscItemSmallIcon(itemMiscTempletByID);
				NKCUtil.SetImageSprite(itemIcon, orLoadMiscItemSmallIcon, false);
			}
		}

		// Token: 0x060072E5 RID: 29413 RVA: 0x00262EAE File Offset: 0x002610AE
		private void SetOKButtonText(bool isPlaying)
		{
			this.m_NKM_UI_OPERATION_POPUP_BOTTOM_OK_TEXT.text = (isPlaying ? NKCUtilString.GET_STRING_OPERATION_POPUP_BUTTON_PLAYING : NKCUtilString.GET_STRING_OPERATION_POPUP_BUTTON);
		}

		// Token: 0x060072E6 RID: 29414 RVA: 0x00262ECC File Offset: 0x002610CC
		private void SetCutscenBtnUI(bool bExistCutscen)
		{
			if (!bExistCutscen)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_POPUP_CUT_SCEN, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_POPUP_CUT_SCEN, true);
			bool bPlayCutscene = NKCScenManager.GetScenManager().GetMyUserData().m_UserOption.m_bPlayCutscene;
			this.m_Tgl_CUT_SCEN_CHECK.Select(bPlayCutscene, true, false);
		}

		// Token: 0x060072E7 RID: 29415 RVA: 0x00262F1C File Offset: 0x0026111C
		private void SetBattleConditionUI(NKMWarfareTemplet warfareTemplet)
		{
			List<NKMBattleConditionTemplet> list = new List<NKMBattleConditionTemplet>();
			if (warfareTemplet != null && warfareTemplet.MapTemplet != null)
			{
				for (int i = 0; i < warfareTemplet.MapTemplet.TileCount; i++)
				{
					NKMWarfareTileTemplet tile = warfareTemplet.MapTemplet.GetTile(i);
					if (tile != null && tile.BattleCondition != null && !list.Contains(tile.BattleCondition))
					{
						list.Add(tile.BattleCondition);
					}
				}
				foreach (string dungeonStrID in warfareTemplet.MapTemplet.GetDungeonStrIDList())
				{
					NKMDungeonTempletBase cNKMDungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(dungeonStrID);
					if (cNKMDungeonTempletBase != null && list.FindIndex((NKMBattleConditionTemplet e) => e == cNKMDungeonTempletBase.BattleCondition) == -1)
					{
						list.Add(cNKMDungeonTempletBase.BattleCondition);
					}
				}
			}
			this.UpdateBattleConditionUI(list);
		}

		// Token: 0x060072E8 RID: 29416 RVA: 0x00263014 File Offset: 0x00261214
		private void SetBattleConditionUI(NKMDungeonTempletBase dungeonTempletBase)
		{
			List<NKMBattleConditionTemplet> list = new List<NKMBattleConditionTemplet>();
			if (dungeonTempletBase != null && dungeonTempletBase.BattleCondition != null)
			{
				list.Add(dungeonTempletBase.BattleCondition);
			}
			this.UpdateBattleConditionUI(list);
		}

		// Token: 0x060072E9 RID: 29417 RVA: 0x00263048 File Offset: 0x00261248
		private void SetBattleConditionUI(NKMPhaseTemplet phaseTemplet)
		{
			List<NKMBattleConditionTemplet> list = new List<NKMBattleConditionTemplet>();
			if (phaseTemplet != null)
			{
				using (IEnumerator<NKMPhaseOrderTemplet> enumerator = phaseTemplet.PhaseList.List.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						NKMPhaseOrderTemplet phase = enumerator.Current;
						if (phase.Dungeon != null && list.FindIndex((NKMBattleConditionTemplet e) => e == phase.Dungeon.BattleCondition) == -1)
						{
							list.Add(phase.Dungeon.BattleCondition);
						}
					}
				}
			}
			this.UpdateBattleConditionUI(list);
		}

		// Token: 0x060072EA RID: 29418 RVA: 0x002630E8 File Offset: 0x002612E8
		private void UpdateBattleConditionUI(List<NKMBattleConditionTemplet> listBattleConditionTemplet)
		{
			if (listBattleConditionTemplet == null || listBattleConditionTemplet.Count == 0)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_POPUP_SCENARIO_BC, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_POPUP_SCENARIO_BC, true);
			int count = listBattleConditionTemplet.Count;
			int num = count - this.m_listBattleConditionSlot.Count;
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_NKM_UI_OPERATION_POPUP_SCENARIO_BC_ICON, this.m_NKM_UI_OPERATION_POPUP_SCENARIO_BC_LAYOUT.transform);
				Image component = gameObject.GetComponent<Image>();
				NKCUIComStateButton component2 = gameObject.GetComponent<NKCUIComStateButton>();
				if (component != null && component2 != null)
				{
					this.m_listBattleConditionSlot.Add(new NKCPopupDungeonInfo.BattleCondition(component, component2));
				}
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_POPUP_SCENARIO_BC_ICON, false);
			int j = 0;
			while (j < this.m_listBattleConditionSlot.Count)
			{
				Image img = this.m_listBattleConditionSlot[j].Img;
				if (j >= count)
				{
					goto IL_152;
				}
				NKMBattleConditionTemplet cNKMBattleConditionTemplet = listBattleConditionTemplet[j];
				if (cNKMBattleConditionTemplet == null)
				{
					goto IL_152;
				}
				Sprite spriteBattleConditionICon = NKCUtil.GetSpriteBattleConditionICon(cNKMBattleConditionTemplet);
				if (spriteBattleConditionICon != null)
				{
					NKCUtil.SetImageSprite(img, spriteBattleConditionICon, false);
					NKCUtil.SetGameobjectActive(img.gameObject, true);
				}
				NKCUIComStateButton btn = this.m_listBattleConditionSlot[j].Btn;
				if (btn != null)
				{
					btn.PointerDown.RemoveAllListeners();
					btn.PointerDown.AddListener(delegate(PointerEventData e)
					{
						NKCUITooltip.Instance.Open(NKCUISlot.eSlotMode.Etc, cNKMBattleConditionTemplet.BattleCondName_Translated, cNKMBattleConditionTemplet.BattleCondDesc_Translated, new Vector2?(e.position));
					});
				}
				IL_15F:
				j++;
				continue;
				IL_152:
				NKCUtil.SetGameobjectActive(img.gameObject, false);
				goto IL_15F;
			}
			this.UpdateBattleConditionTeamUpUI(listBattleConditionTemplet);
		}

		// Token: 0x060072EB RID: 29419 RVA: 0x00263274 File Offset: 0x00261474
		private void UpdateBattleConditionTeamUpUI(List<NKMBattleConditionTemplet> listBattleConditionTemplet)
		{
			this.ClearTeamUPData();
			List<int> list = new List<int>();
			if (listBattleConditionTemplet != null && listBattleConditionTemplet.Count > 0)
			{
				List<string> list2 = new List<string>();
				foreach (NKMBattleConditionTemplet nkmbattleConditionTemplet in listBattleConditionTemplet)
				{
					if (nkmbattleConditionTemplet != null)
					{
						foreach (string item in nkmbattleConditionTemplet.AffectTeamUpID)
						{
							if (!list2.Contains(item))
							{
								list2.Add(item);
							}
						}
						foreach (int item2 in nkmbattleConditionTemplet.hashAffectUnitID)
						{
							if (!list.Contains(item2))
							{
								list.Add(item2);
							}
						}
					}
				}
				List<NKMUnitTempletBase> list3 = new List<NKMUnitTempletBase>();
				if (list2.Count > 0)
				{
					foreach (string teamUp in list2)
					{
						foreach (NKMUnitTempletBase item3 in NKMUnitManager.GetListTeamUPUnitTempletBase(teamUp))
						{
							if (!list3.Contains(item3))
							{
								list3.Add(item3);
							}
						}
					}
				}
				if (list3.Count == 0 && list.Count > 0)
				{
					foreach (int unitID in list)
					{
						NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
						if (unitTempletBase != null && !list3.Contains(unitTempletBase))
						{
							list3.Add(unitTempletBase);
						}
					}
				}
				foreach (NKMUnitTempletBase nkmunitTempletBase in list3)
				{
					if (nkmunitTempletBase.PickupEnableByTag && nkmunitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL && nkmunitTempletBase.m_UnitID >= this.m_iMinDisplayUnitID && nkmunitTempletBase.m_UnitID <= this.m_iMaxDisplayUnitID && (nkmunitTempletBase.m_ShipGroupID == 0 || nkmunitTempletBase.m_ShipGroupID == nkmunitTempletBase.m_UnitID))
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_objTeamUpBox);
						if (!(null == gameObject))
						{
							gameObject.transform.localScale = Vector3.one;
							NKCUtil.SetGameobjectActive(gameObject, true);
							gameObject.transform.SetParent(this.m_rtTeamUpParent, false);
							this.m_lstTeamUpBox.Add(gameObject);
							NKCUISlot newInstance = NKCUISlot.GetNewInstance(gameObject.transform);
							if (null != newInstance)
							{
								newInstance.transform.localPosition = Vector3.zero;
								newInstance.transform.localScale = Vector3.one;
								NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeRewardTypeData(NKM_REWARD_TYPE.RT_UNIT, nkmunitTempletBase.m_UnitID, 1, 0);
								NKCUtil.SetGameobjectActive(newInstance.gameObject, true);
								newInstance.SetData(data, true, null);
								this.lstTeamUpUnits.Add(newInstance);
							}
						}
					}
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objTeamUP, this.m_lstTeamUpBox.Count > 0);
		}

		// Token: 0x060072EC RID: 29420 RVA: 0x0026364C File Offset: 0x0026184C
		private void ClearTeamUPData()
		{
			for (int i = 0; i < this.m_lstTeamUpBox.Count; i++)
			{
				UnityEngine.Object.Destroy(this.m_lstTeamUpBox[i]);
				this.m_lstTeamUpBox[i] = null;
			}
			this.m_lstTeamUpBox.Clear();
		}

		// Token: 0x060072ED RID: 29421 RVA: 0x00263698 File Offset: 0x00261898
		private NKCASUISpineIllust AddSpineIllustration(string prefabStrID)
		{
			return (NKCASUISpineIllust)NKCResourceUtility.OpenSpineIllustWithManualNaming(prefabStrID, false);
		}

		// Token: 0x060072EE RID: 29422 RVA: 0x002636A8 File Offset: 0x002618A8
		private static bool HasCutScen(NKMWarfareTemplet templet)
		{
			if (templet.m_CutScenStrIDAfter.Length > 0 || templet.m_CutScenStrIDBefore.Length > 0)
			{
				return true;
			}
			foreach (string dungeonStrID in templet.MapTemplet.GetDungeonStrIDList())
			{
				NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(dungeonStrID);
				if (dungeonTempletBase != null && dungeonTempletBase.HasCutscen())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060072EF RID: 29423 RVA: 0x00263728 File Offset: 0x00261928
		private static bool HasCutScen(NKMPhaseTemplet templet)
		{
			if (templet.m_CutScenStrIDAfter.Length > 0 || templet.m_CutScenStrIDBefore.Length > 0)
			{
				return true;
			}
			foreach (NKMPhaseOrderTemplet nkmphaseOrderTemplet in templet.PhaseList.List)
			{
				if (nkmphaseOrderTemplet.Dungeon != null && nkmphaseOrderTemplet.Dungeon.HasCutscen())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060072F0 RID: 29424 RVA: 0x002637B0 File Offset: 0x002619B0
		private static void CleanupInstance()
		{
			NKCPopupDungeonInfo.m_Instance = null;
		}

		// Token: 0x04005EBE RID: 24254
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_operation";

		// Token: 0x04005EBF RID: 24255
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_DUNGEON";

		// Token: 0x04005EC0 RID: 24256
		private static NKCPopupDungeonInfo m_Instance;

		// Token: 0x04005EC1 RID: 24257
		public Vector2 DEFAULT_CHAR_POS = new Vector2(-9.97f, -104.7f);

		// Token: 0x04005EC2 RID: 24258
		public GameObject m_objDungeonBoss;

		// Token: 0x04005EC3 RID: 24259
		public Text m_lbDungeonName;

		// Token: 0x04005EC4 RID: 24260
		public Text m_lbDungeonDesc;

		// Token: 0x04005EC5 RID: 24261
		public Text m_lbEpisodeTitle;

		// Token: 0x04005EC6 RID: 24262
		[Header("훈련")]
		public GameObject m_NKM_UI_OPERATION_POPUP_TRAINING_ICON;

		// Token: 0x04005EC7 RID: 24263
		[Header("컷씬")]
		public GameObject m_NKM_UI_OPERATION_POPUP_CUT_SCEN;

		// Token: 0x04005EC8 RID: 24264
		public GameObject m_NKM_UI_OPERATION_POPUP_CHARACTER;

		// Token: 0x04005EC9 RID: 24265
		public Image m_NKM_UI_OPERATION_POPUP_CUT_SCEN_BTN_ON;

		// Token: 0x04005ECA RID: 24266
		public NKCUIComToggle m_Tgl_CUT_SCEN_CHECK;

		// Token: 0x04005ECB RID: 24267
		public NKCUINPCSpineIllust m_NKCUINPCSpineIllust;

		// Token: 0x04005ECC RID: 24268
		[Header("등장하는 적 표시")]
		public NKCUIComEnemyList m_NKCUIComEnemyList;

		// Token: 0x04005ECD RID: 24269
		[Header("던전 달성 목표")]
		public NKCUIComDungeonMission m_NKCUIComDungeonMission;

		// Token: 0x04005ECE RID: 24270
		[Header("입장 제한")]
		public GameObject m_NKM_UI_OPERATION_POPUP_EnterLimit;

		// Token: 0x04005ECF RID: 24271
		public Text m_EnterLimit_TEXT;

		// Token: 0x04005ED0 RID: 24272
		[Header("전투환경")]
		public GameObject m_NKM_UI_OPERATION_POPUP_SCENARIO_BC;

		// Token: 0x04005ED1 RID: 24273
		public GameObject m_NKM_UI_OPERATION_POPUP_SCENARIO_BC_LAYOUT;

		// Token: 0x04005ED2 RID: 24274
		public GameObject m_NKM_UI_OPERATION_POPUP_SCENARIO_BC_ICON;

		// Token: 0x04005ED3 RID: 24275
		[Header("던전 보너스")]
		public GameObject m_BonusType;

		// Token: 0x04005ED4 RID: 24276
		public Image m_BonusType_Icon;

		// Token: 0x04005ED5 RID: 24277
		[Header("던전 입장 요구 아이템")]
		public GameObject m_NKM_UI_OPERATION_POPUP_ETERNIUM;

		// Token: 0x04005ED6 RID: 24278
		public Image m_NKM_UI_OPERATION_POPUP_ETERNIUM_ICON;

		// Token: 0x04005ED7 RID: 24279
		public Text m_NKM_UI_OPERATION_POPUP_ETERNIUM_TEXT;

		// Token: 0x04005ED8 RID: 24280
		[Header("던전 보상 리스트 컴포넌트")]
		public NKCUIComDungeonRewardList m_NKCUIComDungeonRewardList;

		// Token: 0x04005ED9 RID: 24281
		[Header("페이즈 관련")]
		public GameObject m_objPhase;

		// Token: 0x04005EDA RID: 24282
		public Text m_lbPhase;

		// Token: 0x04005EDB RID: 24283
		[Header("Etc")]
		public Text m_NKM_UI_OPERATION_POPUP_BOTTOM_OK_TEXT;

		// Token: 0x04005EDC RID: 24284
		[Header("이벤트 트리거")]
		public EventTrigger m_eventTriggerBG;

		// Token: 0x04005EDD RID: 24285
		[Header("버튼")]
		public NKCUIComButton m_btnCancel;

		// Token: 0x04005EDE RID: 24286
		public NKCUIComButton m_btnNext;

		// Token: 0x04005EDF RID: 24287
		[Header("치트")]
		public EventTrigger m_ETDungeonClearReward;

		// Token: 0x04005EE0 RID: 24288
		[Header("팀업")]
		public GameObject m_objTeamUP;

		// Token: 0x04005EE1 RID: 24289
		public RectTransform m_rtTeamUpParent;

		// Token: 0x04005EE2 RID: 24290
		public GameObject m_objTeamUpBox;

		// Token: 0x04005EE3 RID: 24291
		[Header("버프 적용 유닛 노출 ID 범위")]
		public int m_iMinDisplayUnitID = 1001;

		// Token: 0x04005EE4 RID: 24292
		public int m_iMaxDisplayUnitID = 10000;

		// Token: 0x04005EE5 RID: 24293
		private List<NKCPopupDungeonInfo.BattleCondition> m_listBattleConditionSlot = new List<NKCPopupDungeonInfo.BattleCondition>();

		// Token: 0x04005EE6 RID: 24294
		private const string DEFAULT_CHAR_STR = "NKM_NPC_OPERATOR_LENA";

		// Token: 0x04005EE7 RID: 24295
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x04005EE8 RID: 24296
		private NKCASUISpineIllust m_unitIllust;

		// Token: 0x04005EE9 RID: 24297
		private NKMStageTempletV2 m_StageTemplet;

		// Token: 0x04005EEA RID: 24298
		private NKCPopupDungeonInfo.OnButton dOnOKButton;

		// Token: 0x04005EEB RID: 24299
		private List<GameObject> m_lstTeamUpBox = new List<GameObject>();

		// Token: 0x04005EEC RID: 24300
		private List<NKCUISlot> lstTeamUpUnits = new List<NKCUISlot>();

		// Token: 0x0200177E RID: 6014
		private struct BattleCondition
		{
			// Token: 0x0600B37C RID: 45948 RVA: 0x003638A8 File Offset: 0x00361AA8
			public BattleCondition(Image _img, NKCUIComStateButton _btn)
			{
				this.Img = _img;
				this.Btn = _btn;
			}

			// Token: 0x0400A6FD RID: 42749
			public Image Img;

			// Token: 0x0400A6FE RID: 42750
			public NKCUIComStateButton Btn;
		}

		// Token: 0x0200177F RID: 6015
		// (Invoke) Token: 0x0600B37E RID: 45950
		public delegate void OnButton(NKMStageTempletV2 stageTemplet);
	}
}
