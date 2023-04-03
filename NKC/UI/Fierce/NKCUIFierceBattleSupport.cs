using System;
using System.Collections.Generic;
using ClientPacket.LeaderBoard;
using NKC.UI.Shop;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Fierce
{
	// Token: 0x02000BC0 RID: 3008
	public class NKCUIFierceBattleSupport : NKCUIBase
	{
		// Token: 0x17001632 RID: 5682
		// (get) Token: 0x06008AE4 RID: 35556 RVA: 0x002F3667 File Offset: 0x002F1867
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_FIERCE;
			}
		}

		// Token: 0x17001633 RID: 5683
		// (get) Token: 0x06008AE5 RID: 35557 RVA: 0x002F366E File Offset: 0x002F186E
		public override string GuideTempletID
		{
			get
			{
				return "ARTICLE_FIERCE_INFO";
			}
		}

		// Token: 0x17001634 RID: 5684
		// (get) Token: 0x06008AE6 RID: 35558 RVA: 0x002F3675 File Offset: 0x002F1875
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return new List<int>
				{
					25,
					1,
					2,
					101
				};
			}
		}

		// Token: 0x17001635 RID: 5685
		// (get) Token: 0x06008AE7 RID: 35559 RVA: 0x002F369A File Offset: 0x002F189A
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x06008AE8 RID: 35560 RVA: 0x002F369D File Offset: 0x002F189D
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.Clear();
		}

		// Token: 0x06008AE9 RID: 35561 RVA: 0x002F36B1 File Offset: 0x002F18B1
		public override void OnBackButton()
		{
			if (this.m_curUIStatus == NKCUIFierceBattleSupport.UI_STATUS.US_READY)
			{
				this.UpdateUIAni(NKCUIFierceBattleSupport.UI_STATUS.US_BACK, false);
				return;
			}
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OPERATION, true);
		}

		// Token: 0x06008AEA RID: 35562 RVA: 0x002F36D4 File Offset: 0x002F18D4
		public override void UnHide()
		{
			base.UnHide();
			if (NKCTutorialManager.TutorialRequired(TutorialStep.FierceLobby))
			{
				this.ResetUI();
				this.UpdateUIAni(NKCUIFierceBattleSupport.UI_STATUS.US_INTRO_IDLE, true);
				return;
			}
			switch (this.m_curUIStatus)
			{
			case NKCUIFierceBattleSupport.UI_STATUS.US_INTRO:
				this.UpdateUIAni(NKCUIFierceBattleSupport.UI_STATUS.US_INTRO_IDLE, true);
				return;
			case NKCUIFierceBattleSupport.UI_STATUS.US_READY:
				this.UpdateUIAni(NKCUIFierceBattleSupport.UI_STATUS.US_READY_IDLE, true);
				return;
			case NKCUIFierceBattleSupport.UI_STATUS.US_BACK:
				break;
			case NKCUIFierceBattleSupport.UI_STATUS.US_RESULT:
				this.UpdateUIAni(NKCUIFierceBattleSupport.UI_STATUS.US_RESULT_IDLE, true);
				break;
			default:
				return;
			}
		}

		// Token: 0x06008AEB RID: 35563 RVA: 0x002F373C File Offset: 0x002F193C
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			base.OnInventoryChange(itemData);
			this.UpdateDailyRewardRedDot();
		}

		// Token: 0x06008AEC RID: 35564 RVA: 0x002F374C File Offset: 0x002F194C
		public void Init()
		{
			NKCUtil.SetToggleValueChangedDelegate(this.m_MY_BEST_DECK_Toggle, new UnityAction<bool>(this.OnClickBestLineUp));
			NKCUtil.SetBindFunction(this.m_FIERCE_BATTLE_BUTTON_READY, new UnityAction(this.OnClickReady));
			NKCUtil.SetBindFunction(this.m_FIERCE_BATTLE_BUTTON_START, new UnityAction(this.OnClickPrepare));
			NKCUtil.SetBindFunction(this.m_FIERCE_BATTLE_BUTTON_SCORE_REWARD, new UnityAction(this.OnClickPointReward));
			NKCUtil.SetBindFunction(this.m_FIERCE_BATTLE_BUTTON_RANK_INFO, new UnityAction(this.OnClickRankReward));
			NKCUtil.SetBindFunction(this.m_FIERCE_BATTLE_BUTTON_RANK_SHORTCUT, new UnityAction(this.OnClickPopUpLeaderBoard));
			NKCUtil.SetBindFunction(this.m_FIERCE_BATTLE_BUTTON_BOSS_PERSONAL_RANK, new UnityAction(this.OnClickOpenRankingPopup));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnShop, new UnityAction(this.OnShop));
			NKCUtil.SetBindFunction(this.m_csbtnSelfPenalty, new UnityAction(this.OnClickSelfPenalty));
			NKCUtil.SetBindFunction(this.m_csbtnDailyReward, new UnityAction(this.OnClickDailyReward));
			this.m_curUIStatus = NKCUIFierceBattleSupport.UI_STATUS.US_NONE;
			this.m_FierceDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
			if (this.m_FIERCE_BATTLE_BOSS_RANK_FINAL_ScrollRect != null)
			{
				this.m_FIERCE_BATTLE_BOSS_RANK_FINAL_ScrollRect.dOnGetObject += this.GetObject;
				this.m_FIERCE_BATTLE_BOSS_RANK_FINAL_ScrollRect.dOnReturnObject += this.ReturnObject;
				this.m_FIERCE_BATTLE_BOSS_RANK_FINAL_ScrollRect.dOnProvideData += this.ProvideData;
				this.m_FIERCE_BATTLE_BOSS_RANK_FINAL_ScrollRect.PrepareCells(0);
			}
			for (int i = 0; i < this.m_lstNKCDeckViewUnitSlot.Count; i++)
			{
				this.m_lstNKCDeckViewUnitSlot[i].Init(i, false);
			}
			foreach (NKCUIFierceBattleBossPersonalRankSlot nkcuifierceBattleBossPersonalRankSlot in this.m_lstRankSlot)
			{
				if (nkcuifierceBattleBossPersonalRankSlot != null)
				{
					nkcuifierceBattleBossPersonalRankSlot.Init();
				}
			}
			if (this.m_FIERCE_BATTLE_BOSS_IllustView != null)
			{
				this.m_rtFierceBossIllust = this.m_FIERCE_BATTLE_BOSS_IllustView.GetComponent<RectTransform>();
			}
			NKCUtil.SetToggleValueChangedDelegate(this.m_lstDifficultToggle[0], delegate(bool bSet)
			{
				this.OnClickDifficultTab(NKCUIFierceBattleSupport.DIFFICULT_LEVEL.EASY);
			});
			NKCUtil.SetToggleValueChangedDelegate(this.m_lstDifficultToggle[1], delegate(bool bSet)
			{
				this.OnClickDifficultTab(NKCUIFierceBattleSupport.DIFFICULT_LEVEL.NORMAL);
			});
			NKCUtil.SetToggleValueChangedDelegate(this.m_lstDifficultToggle[2], delegate(bool bSet)
			{
				this.OnClickDifficultTab(NKCUIFierceBattleSupport.DIFFICULT_LEVEL.CHALLENGE);
			});
		}

		// Token: 0x06008AED RID: 35565 RVA: 0x002F39A4 File Offset: 0x002F1BA4
		private void Clear()
		{
			for (int i = 0; i < this.m_lstVisible.Count; i++)
			{
				if (null != this.m_lstVisible[i])
				{
					this.m_stk.Push(this.m_lstVisible[i]);
				}
			}
			while (this.m_stk.Count > 0)
			{
				NKCUIFierceBattleBossPersonalRankSlot nkcuifierceBattleBossPersonalRankSlot = this.m_stk.Pop();
				if (nkcuifierceBattleBossPersonalRankSlot != null)
				{
					nkcuifierceBattleBossPersonalRankSlot.DestoryInstance();
				}
			}
		}

		// Token: 0x06008AEE RID: 35566 RVA: 0x002F3A18 File Offset: 0x002F1C18
		public void Open()
		{
			try
			{
				if (this.m_FierceDataMgr.IsCanAccessFierce())
				{
					this.m_curFierceTemplet = this.m_FierceDataMgr.FierceTemplet;
					this.m_icurFierceBossGroupID = this.m_curFierceTemplet.FierceBossGroupIdList[0];
					if (!NKMFierceBossGroupTemplet.Groups.ContainsKey(this.m_icurFierceBossGroupID))
					{
						Debug.LogError(string.Format("NKCUIFierceBattleSupport::Open() - plz check fierce boss group data - boss group id : {0}", this.m_icurFierceBossGroupID));
					}
					else
					{
						if (this.m_FierceDataMgr.GetStatus() == NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_ACTIVATE)
						{
							this.m_bFierceActivateTimeCnt = true;
						}
						else if (this.m_FierceDataMgr.IsPossibleRankReward())
						{
							NKCPacketSender.Send_NKMPacket_FIERCE_COMPLETE_RANK_REWARD_REQ();
						}
						this.UpdateFierceRanking();
						this.ResetUI();
						base.UIOpened(true);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError("[Error]NKCUIFierceBattleSupport::Open() Failed with exception : " + ex.Message);
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_UI_LOADING_ERROR, delegate()
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_WORLDMAP, true);
				}, "");
			}
		}

		// Token: 0x06008AEF RID: 35567 RVA: 0x002F3B2C File Offset: 0x002F1D2C
		public void ResetUI()
		{
			this.UpdateMainUI();
			this.UpdateSideUI(0);
			if (this.m_FierceDataMgr.GetStatus() == NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_ACTIVATE)
			{
				this.UpdateUIAni(NKCUIFierceBattleSupport.UI_STATUS.US_INTRO, false);
			}
			else
			{
				this.UpdateUIAni(NKCUIFierceBattleSupport.UI_STATUS.US_RESULT, false);
			}
			if (this.m_MY_BEST_DECK_Toggle != null)
			{
				this.m_MY_BEST_DECK_Toggle.Select(false, false, false);
			}
			NKCUtil.SetGameobjectActive(this.m_DECK_List, false);
		}

		// Token: 0x06008AF0 RID: 35568 RVA: 0x002F3B90 File Offset: 0x002F1D90
		private void UpdateUIAni(NKCUIFierceBattleSupport.UI_STATUS newAni, bool bForce = false)
		{
			if (this.m_curUIStatus == newAni && !bForce)
			{
				return;
			}
			switch (newAni)
			{
			case NKCUIFierceBattleSupport.UI_STATUS.US_INTRO:
				this.m_AB_UI_NKM_UI_FIERCE_BATTLE_CONTENT.SetTrigger("INTRO");
				break;
			case NKCUIFierceBattleSupport.UI_STATUS.US_READY:
				this.m_AB_UI_NKM_UI_FIERCE_BATTLE_CONTENT.SetTrigger("READY");
				NKCUtil.SetGameobjectActive(this.m_FIERCE_BATTLE_NIGHTMARE, this.m_bNightMareMode);
				if (this.m_bNightMareMode)
				{
					this.m_aniNightMare.SetTrigger("INTRO");
				}
				break;
			case NKCUIFierceBattleSupport.UI_STATUS.US_BACK:
				this.m_AB_UI_NKM_UI_FIERCE_BATTLE_CONTENT.SetTrigger("BACK");
				newAni = NKCUIFierceBattleSupport.UI_STATUS.US_INTRO;
				if (this.m_bNightMareMode)
				{
					this.m_aniNightMare.SetTrigger("BACK");
				}
				break;
			case NKCUIFierceBattleSupport.UI_STATUS.US_RESULT:
				this.m_AB_UI_NKM_UI_FIERCE_BATTLE_CONTENT.SetTrigger("RESULT");
				break;
			case NKCUIFierceBattleSupport.UI_STATUS.US_INTRO_IDLE:
				this.m_AB_UI_NKM_UI_FIERCE_BATTLE_CONTENT.SetTrigger("INTRO_IDLE");
				newAni = NKCUIFierceBattleSupport.UI_STATUS.US_INTRO;
				break;
			case NKCUIFierceBattleSupport.UI_STATUS.US_READY_IDLE:
				this.m_AB_UI_NKM_UI_FIERCE_BATTLE_CONTENT.SetTrigger("READY_IDLE");
				newAni = NKCUIFierceBattleSupport.UI_STATUS.US_READY;
				break;
			case NKCUIFierceBattleSupport.UI_STATUS.US_RESULT_IDLE:
				this.m_AB_UI_NKM_UI_FIERCE_BATTLE_CONTENT.SetTrigger("RESULT_IDLE");
				newAni = NKCUIFierceBattleSupport.UI_STATUS.US_RESULT;
				break;
			}
			this.m_curUIStatus = newAni;
			NKCUtil.SetGameobjectActive(this.m_csbtnSelfPenalty.gameObject, this.m_curUIStatus == NKCUIFierceBattleSupport.UI_STATUS.US_READY);
			this.UpdatePenaltyUI();
			Debug.Log(string.Format("<color=green>update ani : {0}</color>", this.m_curUIStatus));
			NKCUtil.SetGameobjectActive(this.m_FIERCE_BATTLE_BOSS_INFO_TYPE_01, this.m_curUIStatus == NKCUIFierceBattleSupport.UI_STATUS.US_INTRO);
			NKCUtil.SetGameobjectActive(this.m_BOTTOM_Type_01, this.m_curUIStatus == NKCUIFierceBattleSupport.UI_STATUS.US_INTRO || this.m_curUIStatus == NKCUIFierceBattleSupport.UI_STATUS.US_RESULT);
			NKCUtil.SetGameobjectActive(this.m_BOTTOM_Type_02, this.m_curUIStatus == NKCUIFierceBattleSupport.UI_STATUS.US_READY);
			NKCUtil.SetGameobjectActive(this.m_FIERCE_BATTLE_BOSS_INFO_TYPE_02, this.m_curUIStatus == NKCUIFierceBattleSupport.UI_STATUS.US_READY);
			NKCUtil.SetGameobjectActive(this.m_FIERCE_BATTLE_BOSS_INFO_TYPE_03, this.m_curUIStatus == NKCUIFierceBattleSupport.UI_STATUS.US_RESULT);
			NKCUtil.SetGameobjectActive(this.m_FIERCE_BATTLE_BUTTON_READY_Disable, this.m_curUIStatus == NKCUIFierceBattleSupport.UI_STATUS.US_RESULT);
			NKCUtil.SetGameobjectActive(this.m_FIERCE_BATTLE_BUTTON_READY_Normal, this.m_curUIStatus == NKCUIFierceBattleSupport.UI_STATUS.US_INTRO);
			NKCUtil.SetGameobjectActive(this.m_FIERCE_BATTLE_BUTTON_READY, this.m_curUIStatus == NKCUIFierceBattleSupport.UI_STATUS.US_INTRO || this.m_curUIStatus == NKCUIFierceBattleSupport.UI_STATUS.US_RESULT);
			NKCUtil.SetGameobjectActive(this.m_FIERCE_BATTLE_BUTTON_START, this.m_curUIStatus == NKCUIFierceBattleSupport.UI_STATUS.US_READY);
			NKCUtil.SetGameobjectActive(this.m_objDifficultSelect, this.m_curUIStatus != NKCUIFierceBattleSupport.UI_STATUS.US_RESULT);
			NKCUtil.SetGameobjectActive(this.m_objDailyReward, this.m_curUIStatus != NKCUIFierceBattleSupport.UI_STATUS.US_RESULT);
		}

		// Token: 0x06008AF1 RID: 35569 RVA: 0x002F3DDC File Offset: 0x002F1FDC
		private void UpdateMainUI()
		{
			NKCUtil.SetGameobjectActive(this.m_FIERCE_BATTLE_BUTTON_RANK_SHORTCUT_REDDOT, false);
			if (this.m_curFierceTemplet == null)
			{
				return;
			}
			NKCUtil.SetLabelText(this.m_FIERCE_BATTLE_TOTAL_SCORE_TEXT_1, this.m_FierceDataMgr.GetTotalPoint().ToString());
			NKCUtil.SetLabelText(this.m_FIERCE_BATTLE_TOTAL_SCORE_TEXT_2, this.m_FierceDataMgr.GetRankingTotalDesc());
			this.m_FierceDataMgr.GetRecommandOperationPower();
		}

		// Token: 0x06008AF2 RID: 35570 RVA: 0x002F3E40 File Offset: 0x002F2040
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
		}

		// Token: 0x06008AF3 RID: 35571 RVA: 0x002F3EB8 File Offset: 0x002F20B8
		private void UpdateSideUI(int targetBossID = 0)
		{
			if (this.m_curFierceTemplet == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_FIERCE_BATTLE_NIGHTMARE, false);
			int num = 0;
			if (targetBossID != 0 && NKMFierceBossGroupTemplet.Groups.ContainsKey(this.m_icurFierceBossGroupID))
			{
				foreach (NKMFierceBossGroupTemplet nkmfierceBossGroupTemplet in NKMFierceBossGroupTemplet.Groups[this.m_icurFierceBossGroupID])
				{
					if (nkmfierceBossGroupTemplet.FierceBossID == targetBossID)
					{
						num = nkmfierceBossGroupTemplet.Level;
					}
				}
			}
			if (num == 0)
			{
				num = this.m_FierceDataMgr.GetClearLevel(this.m_icurFierceBossGroupID);
			}
			switch (num)
			{
			case 0:
			case 1:
				this.OnClickDifficultTab(NKCUIFierceBattleSupport.DIFFICULT_LEVEL.EASY);
				break;
			case 2:
				this.OnClickDifficultTab(NKCUIFierceBattleSupport.DIFFICULT_LEVEL.NORMAL);
				break;
			case 3:
				this.OnClickDifficultTab(NKCUIFierceBattleSupport.DIFFICULT_LEVEL.CHALLENGE);
				break;
			}
			this.UpdateBossUI();
			NKCUtil.SetLabelText(this.m_FIERCE_BATTLE_NOW_BOSS_SCORE_TEXT, this.m_FierceDataMgr.GetMaxPoint(0).ToString());
			NKCUtil.SetLabelText(this.m_FIERCE_BATTLE_NOW_BOSS_SCORE_TEXT_3, this.m_FierceDataMgr.GetMaxPoint(0).ToString());
			NKCUtil.SetLabelText(this.m_FIERCE_BATTLE_NOW_BOSS_SCORE_TEXT_2, this.m_FierceDataMgr.GetRankingDesc());
			NKCUtil.SetLabelText(this.m_FIERCE_BATTLE_NOW_BOSS_SCORE_TEXT_4, this.m_FierceDataMgr.GetRankingDesc());
			this.UpdateBattleCond();
			this.UpdateFierceBattleRank();
			if (this.m_FierceDataMgr.GetStatus() == NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_ACTIVATE)
			{
				NKCUtil.SetGameobjectActive(this.m_FIERCE_BATTLE_BUTTON_EnterLimit, this.m_curFierceTemplet.DailyEnterLimit > 0);
			}
			NKCUtil.SetGameobjectActive(this.m_BUTTON_Root, this.m_FierceDataMgr.GetStatus() == NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_ACTIVATE);
			this.UpdateDailyRewardRedDot();
			this.UpdatePointRewardRedDot();
			this.UpdatePenaltyUI();
		}

		// Token: 0x06008AF4 RID: 35572 RVA: 0x002F4060 File Offset: 0x002F2260
		private void UpdateBattleCond()
		{
			this.ClearBCUntSlots();
			List<NKMBattleConditionTemplet> curBattleCondition = this.m_FierceDataMgr.GetCurBattleCondition(false);
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
					nkcuifierceBattleCondition.SetData(i + 1, curBattleCondition[i], true);
					this.m_lstBattleCond.Add(nkcuifierceBattleCondition);
				}
			}
		}

		// Token: 0x06008AF5 RID: 35573 RVA: 0x002F40E4 File Offset: 0x002F22E4
		private void UpdatePenaltyUI()
		{
			this.UpdateNightMareMode();
			NKCUtil.SetGameobjectActive(this.m_FIERCE_BATTLE_NIGHTMARE, this.m_bNightMareMode);
			NKCUtil.SetImageSprite(this.m_Background, NKCUtil.GetSpriteFierceBattleBackgroud(this.m_bNightMareMode), false);
			NKCUtil.SetGameobjectActive(this.m_NightmareMode, this.m_bNightMareMode && this.m_curUIStatus == NKCUIFierceBattleSupport.UI_STATUS.US_READY);
			NKCUtil.SetGameobjectActive(this.m_NightmareModeBGOFF, !this.m_bNightMareMode || this.m_curUIStatus != NKCUIFierceBattleSupport.UI_STATUS.US_READY);
			NKCUtil.SetGameobjectActive(this.m_NightmareModeBGON, this.m_bNightMareMode && this.m_curUIStatus == NKCUIFierceBattleSupport.UI_STATUS.US_READY);
			NKCUtil.SetGameobjectActive(this.m_objSelfPenaltyBtnNornal, this.m_bNightMareMode);
			NKCUtil.SetGameobjectActive(this.m_objSelfPenaltyBtnDisable, !this.m_bNightMareMode);
			string text = this.m_FierceDataMgr.GetStringCurBossSelfPenalty();
			if (!this.m_bNightMareMode || string.IsNullOrEmpty(text))
			{
				text = "-";
			}
			NKCUtil.SetLabelText(this.m_lbSelfPenaltyDesc, text);
		}

		// Token: 0x06008AF6 RID: 35574 RVA: 0x002F41D4 File Offset: 0x002F23D4
		public void UpdateFierceBattleRank()
		{
			bool flag = this.m_FierceDataMgr.IsHasFierceRankingData(false);
			NKCFierceBattleSupportDataMgr.FIERCE_STATUS status = this.m_FierceDataMgr.GetStatus();
			if (status == NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_ACTIVATE)
			{
				if (flag)
				{
					for (int i = 0; i < this.m_lstRankSlot.Count; i++)
					{
						NKMFierceData fierceRankingData = this.m_FierceDataMgr.GetFierceRankingData(i);
						if (fierceRankingData == null)
						{
							NKCUtil.SetGameobjectActive(this.m_lstRankSlot[i].gameObject, false);
						}
						else
						{
							NKCUtil.SetGameobjectActive(this.m_lstRankSlot[i].gameObject, true);
							this.m_lstRankSlot[i].SetData(fierceRankingData, i + 1);
						}
					}
				}
				NKCUtil.SetGameobjectActive(this.m_FIERCE_BATTLE_TOP3_LIST_ScrollRect, flag);
				NKCUtil.SetGameobjectActive(this.m_FIERCE_BATTLE_TOP3_LIST_NODATA, !flag);
				return;
			}
			if (status - NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_REWARD > 1)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_FIERCE_BATTLE_NOTICE_2, !this.m_FierceDataMgr.IsPossibleRankReward());
			if (flag)
			{
				this.m_FIERCE_BATTLE_BOSS_RANK_FINAL_ScrollRect.TotalCount = Mathf.Min(this.m_FierceDataMgr.GetBossGroupRankingDataCnt(0), 50);
			}
			else
			{
				this.m_FIERCE_BATTLE_BOSS_RANK_FINAL_ScrollRect.TotalCount = 0;
			}
			this.m_FIERCE_BATTLE_BOSS_RANK_FINAL_ScrollRect.SetIndexPosition(0);
		}

		// Token: 0x06008AF7 RID: 35575 RVA: 0x002F42E8 File Offset: 0x002F24E8
		private void OnClickBestLineUp(bool bSet)
		{
			bool flag = false;
			if (bSet)
			{
				NKMEventDeckData bestLineUp = this.m_FierceDataMgr.GetBestLineUp();
				if (bestLineUp != null && bestLineUp.m_dicUnit.Count > 0)
				{
					flag = true;
					NKMUnitData shipFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetShipFromUID(bestLineUp.m_ShipUID);
					if (shipFromUID != null)
					{
						NKMUnitTemplet unitTemplet = shipFromUID.GetUnitTemplet();
						if (unitTemplet != null)
						{
							NKCUtil.SetImageSprite(this.m_ANIM_SHIP_IMG, NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, unitTemplet.m_UnitTempletBase), false);
						}
					}
					for (int i = 0; i < 8; i++)
					{
						if (!(this.m_lstNKCDeckViewUnitSlot[i] == null) && bestLineUp.m_dicUnit.Count > i)
						{
							NKMUnitData unitFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(bestLineUp.m_dicUnit[i]);
							this.m_lstNKCDeckViewUnitSlot[i].SetData(unitFromUID, false);
						}
					}
				}
				NKCUtil.SetGameobjectActive(this.m_FIERCE_BATTLE_MY_BEST_DECK_NODATA_TEXT, !flag);
				NKCUtil.SetGameobjectActive(this.m_DECK_List, flag);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_FIERCE_BATTLE_MY_BEST_DECK_NODATA_TEXT, false);
			NKCUtil.SetGameobjectActive(this.m_DECK_List, false);
		}

		// Token: 0x06008AF8 RID: 35576 RVA: 0x002F43F8 File Offset: 0x002F25F8
		private void UpdateBossUI()
		{
			this.UpdateBossInfo();
			NKCUtil.SetLabelText(this.m_Disc, this.m_FierceDataMgr.GetCurBossDesc());
			NKMDungeonTemplet dungeonTemplet = NKMDungeonManager.GetDungeonTemplet(this.m_FierceDataMgr.GetTargetDungeonID());
			if (dungeonTemplet != null)
			{
				this.m_FIERCE_BATTLE_BOSS_IllustView.SetCharacterIllust(dungeonTemplet.m_BossUnitStrID, true, true, false, 0);
			}
			NKMFierceBossGroupTemplet bossGroupTemplet = this.m_FierceDataMgr.GetBossGroupTemplet();
			if (bossGroupTemplet != null && this.m_rtFierceBossIllust != null)
			{
				this.m_rtFierceBossIllust.localScale = new Vector2((float)bossGroupTemplet.UI_BossPrefabScale, (float)bossGroupTemplet.UI_BossPrefabScale);
				if (bossGroupTemplet.UI_BossPrefabFlip)
				{
					this.m_rtFierceBossIllust.localRotation = Quaternion.Euler(0f, -180f, 0f);
				}
				else
				{
					this.m_rtFierceBossIllust.localRotation = Quaternion.Euler(0f, 0f, 0f);
				}
				if (this.m_FIERCE_BATTLE_BOSS_IllustView.m_rectIllustRoot != null)
				{
					this.m_FIERCE_BATTLE_BOSS_IllustView.m_rectIllustRoot.localPosition = new Vector2(bossGroupTemplet.UI_BossPrefabPos.x, bossGroupTemplet.UI_BossPrefabPos.y);
				}
			}
		}

		// Token: 0x06008AF9 RID: 35577 RVA: 0x002F451C File Offset: 0x002F271C
		private void UpdateBossInfo()
		{
			int curSelectedBossLv = this.m_FierceDataMgr.GetCurSelectedBossLv(this.m_icurFierceBossGroupID);
			NKCUtil.SetLabelText(this.m_BossLv, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, curSelectedBossLv));
			NKCUtil.SetLabelText(this.m_BossName, this.m_FierceDataMgr.GetCurBossName());
		}

		// Token: 0x06008AFA RID: 35578 RVA: 0x002F456C File Offset: 0x002F276C
		private void Update()
		{
			if (this.m_bFierceActivateTimeCnt && this.m_curFierceTemplet != null)
			{
				if (NKCSynchronizedTime.IsFinished(NKMTime.LocalToUTC(this.m_curFierceTemplet.FierceGameEnd, 0)))
				{
					NKCUtil.SetLabelText(this.m_FIERCE_BATTLE_TIME_TEXT, NKCUtilString.GET_FIERCE_ACTIVATE_SEASON_END);
					this.m_bFierceActivateTimeCnt = false;
					return;
				}
				NKCUtil.SetLabelText(this.m_FIERCE_BATTLE_TIME_TEXT, this.m_FierceDataMgr.GetLeftTimeString());
				if (this.m_curFierceTemplet.DailyEnterLimit > 0)
				{
					string msg = string.Format(NKCUtilString.GET_FIERCE_ENTER_LIMIT, this.m_curFierceTemplet.DailyEnterLimit - NKCScenManager.CurrentUserData().GetStatePlayCnt(NKMFierceConst.StageId, true, false, false), this.m_curFierceTemplet.DailyEnterLimit);
					NKCUtil.SetLabelText(this.m_EnterLimit_TEXT, msg);
					if (this.m_ObjDailyRewardRedDot.activeSelf && NKCScenManager.CurrentUserData().GetStatePlayCnt(NKMFierceConst.StageId, true, false, false) == 0)
					{
						NKCUtil.SetGameobjectActive(this.m_ObjDailyRewardRedDot, false);
					}
				}
			}
		}

		// Token: 0x06008AFB RID: 35579 RVA: 0x002F465C File Offset: 0x002F285C
		private void OnClickPointReward()
		{
			NKCUIPopupFierceBattleScoreReward.Instance.Open();
		}

		// Token: 0x06008AFC RID: 35580 RVA: 0x002F4668 File Offset: 0x002F2868
		private void OnClickRankReward()
		{
			NKCUIPopupFierceBattleRewardInfo.Instance.Open();
		}

		// Token: 0x06008AFD RID: 35581 RVA: 0x002F4674 File Offset: 0x002F2874
		private void OnClickPopUpLeaderBoard()
		{
			NKMLeaderBoardTemplet nkmleaderBoardTemplet = NKMLeaderBoardTemplet.Find(LeaderBoardType.BT_FIERCE, 0);
			if (nkmleaderBoardTemplet != null)
			{
				NKCPopupLeaderBoardSingle.Instance.OpenSingle(nkmleaderBoardTemplet);
			}
		}

		// Token: 0x06008AFE RID: 35582 RVA: 0x002F4697 File Offset: 0x002F2897
		private void OnClickOpenRankingPopup()
		{
			if (this.m_FierceDataMgr.GetStatus() == NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_ACTIVATE)
			{
				NKCPacketSender.Send_NKMPacket_LEADERBOARD_FIERCE_BOSSGROUP_LIST_REQ(this.m_FierceDataMgr.CurBossGroupID, true);
			}
		}

		// Token: 0x06008AFF RID: 35583 RVA: 0x002F46B8 File Offset: 0x002F28B8
		private void OnShop()
		{
			NKCUIShop.ShopShortcut("TAB_SEASON_FIERCE_POINT", 0, 0);
		}

		// Token: 0x06008B00 RID: 35584 RVA: 0x002F46C8 File Offset: 0x002F28C8
		private void OnClickSelfPenalty()
		{
			if (this.m_FierceDataMgr.GetCurSelectedBossLv() == 3)
			{
				NKCPopupFierceBattleSelfPenalty.Instance.Open(this.m_curFierceTemplet.FierceID, new UnityAction(this.UpdatePenaltyUI));
				return;
			}
			NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_FIERCE_POPUP_SELF_PENALTY_BLOCK_TEXT, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
		}

		// Token: 0x06008B01 RID: 35585 RVA: 0x002F4718 File Offset: 0x002F2918
		public void RefreshLeaderBoard()
		{
			NKCUtil.SetGameobjectActive(this.m_FIERCE_BATTLE_BUTTON_RANK_SHORTCUT_REDDOT, false);
			if (NKCPopupLeaderBoardSingle.IsInstanceOpen)
			{
				NKCPopupLeaderBoardSingle.Instance.RefreshUI(false);
			}
		}

		// Token: 0x06008B02 RID: 35586 RVA: 0x002F4738 File Offset: 0x002F2938
		private void OnClickReady()
		{
			NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
			if (nkcfierceBattleSupportDataMgr != null && nkcfierceBattleSupportDataMgr.GetStatus() == NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_ACTIVATE)
			{
				this.UpdateUIAni(NKCUIFierceBattleSupport.UI_STATUS.US_READY, false);
			}
		}

		// Token: 0x06008B03 RID: 35587 RVA: 0x002F4764 File Offset: 0x002F2964
		private void OnClickPrepare()
		{
			if (this.m_FierceDataMgr.IsCanStart())
			{
				this.m_FierceDataMgr.SendPenaltyReq();
				int targetDungeonID = this.m_FierceDataMgr.GetTargetDungeonID();
				if (targetDungeonID == 0)
				{
					Debug.Log("던전 정보를 확인 할 수 없습니다. NKCUIFierceBattleSupport::OnClickPrepare()");
					return;
				}
				NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY().SetDungeonInfo(NKMDungeonManager.GetDungeonTempletBase(targetDungeonID), DeckContents.FIERCE_BATTLE_SUPPORT);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_DUNGEON_ATK_READY, true);
			}
		}

		// Token: 0x06008B04 RID: 35588 RVA: 0x002F47C8 File Offset: 0x002F29C8
		private void UpdateFierceRanking()
		{
			foreach (int bossGroupID in this.m_curFierceTemplet.FierceBossGroupIdList)
			{
				NKCPacketSender.Send_NKMPacket_LEADERBOARD_FIERCE_BOSSGROUP_LIST_REQ(bossGroupID, false);
			}
		}

		// Token: 0x06008B05 RID: 35589 RVA: 0x002F4820 File Offset: 0x002F2A20
		public void UpdatePointRewardRedDot()
		{
			NKCUtil.SetGameobjectActive(this.m_FIERCE_BATTLE_BUTTON_SCORE_REWARD_REDDOT, this.m_FierceDataMgr.IsCanReceivePointReward());
		}

		// Token: 0x06008B06 RID: 35590 RVA: 0x002F4838 File Offset: 0x002F2A38
		public void UpdateDailyRewardRedDot()
		{
			bool bValue = NKCScenManager.CurrentUserData().GetStatePlayCnt(NKMFierceConst.StageId, true, false, false) > 0 && !this.m_FierceDataMgr.m_fierceDailyRewardReceived;
			NKCUtil.SetGameobjectActive(this.m_ObjDailyRewardRedDot, bValue);
		}

		// Token: 0x06008B07 RID: 35591 RVA: 0x002F4878 File Offset: 0x002F2A78
		private RectTransform GetObject(int index)
		{
			NKCUIFierceBattleBossPersonalRankSlot nkcuifierceBattleBossPersonalRankSlot;
			if (this.m_stk.Count > 0)
			{
				nkcuifierceBattleBossPersonalRankSlot = this.m_stk.Pop();
			}
			else
			{
				nkcuifierceBattleBossPersonalRankSlot = NKCUIFierceBattleBossPersonalRankSlot.GetNewInstance(this.m_FIERCE_BATTLE_BOSS_RANK_FINAL_ScrollRect.content.transform);
			}
			this.m_lstVisible.Add(nkcuifierceBattleBossPersonalRankSlot);
			if (nkcuifierceBattleBossPersonalRankSlot == null)
			{
				return null;
			}
			return nkcuifierceBattleBossPersonalRankSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06008B08 RID: 35592 RVA: 0x002F48D0 File Offset: 0x002F2AD0
		private void ReturnObject(Transform tr)
		{
			NKCUIFierceBattleBossPersonalRankSlot component = tr.GetComponent<NKCUIFierceBattleBossPersonalRankSlot>();
			this.m_lstVisible.Remove(component);
			this.m_stk.Push(component);
			NKCUtil.SetGameobjectActive(component, false);
			tr.SetParent(base.transform);
		}

		// Token: 0x06008B09 RID: 35593 RVA: 0x002F4910 File Offset: 0x002F2B10
		private void ProvideData(Transform tr, int idx)
		{
			NKCUIFierceBattleBossPersonalRankSlot component = tr.GetComponent<NKCUIFierceBattleBossPersonalRankSlot>();
			int rank = idx + 1;
			NKMFierceData fierceRankingData = this.m_FierceDataMgr.GetFierceRankingData(idx);
			if (fierceRankingData == null)
			{
				NKCUtil.SetGameobjectActive(component, false);
				return;
			}
			component.SetData(fierceRankingData, rank);
		}

		// Token: 0x06008B0A RID: 35594 RVA: 0x002F4948 File Offset: 0x002F2B48
		private void OnClickDailyReward()
		{
			if (NKCScenManager.CurrentUserData().GetStatePlayCnt(NKMFierceConst.StageId, true, false, false) <= 0)
			{
				NKCPopupMessageManager.AddPopupMessage(NKM_ERROR_CODE.NEC_FAIL_FIERCE_GOT_DAILY_REWARD, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			if (this.m_FierceDataMgr.m_fierceDailyRewardReceived)
			{
				NKCPopupMessageManager.AddPopupMessage(NKM_ERROR_CODE.NEC_FAIL_FIERCE_ALREADY_GOT_REWARD, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			NKCPacketSender.Send_NKMPacket_FIERCE_DAILY_REWARD_REQ();
		}

		// Token: 0x06008B0B RID: 35595 RVA: 0x002F49A4 File Offset: 0x002F2BA4
		private void OnClickDifficultTab(NKCUIFierceBattleSupport.DIFFICULT_LEVEL DifficultLv)
		{
			if (this.m_lstDifficultToggle.Count - 1 < (int)DifficultLv)
			{
				return;
			}
			int num = 0;
			foreach (NKMFierceBossGroupTemplet nkmfierceBossGroupTemplet in NKMFierceBossGroupTemplet.Groups[this.m_icurFierceBossGroupID])
			{
				if (num == (int)DifficultLv)
				{
					this.m_FierceDataMgr.SetCurBossID(nkmfierceBossGroupTemplet.FierceBossID);
					Debug.Log(string.Format("<color=red>OnClickDifficultTab : difLV : {0}, groupID : {1}, bossID : {2}</color>", (int)DifficultLv, nkmfierceBossGroupTemplet.FierceBossGroupID, nkmfierceBossGroupTemplet.FierceBossID));
					break;
				}
				num++;
			}
			this.m_lstDifficultToggle[(int)DifficultLv].Select(true, true, false);
			Sprite sp = null;
			switch (DifficultLv)
			{
			case NKCUIFierceBattleSupport.DIFFICULT_LEVEL.EASY:
				sp = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_fierce_battle_sprite", "NKM_UI_FIERCE_BATTLE_DIFFICULTY_01_EASY", false);
				break;
			case NKCUIFierceBattleSupport.DIFFICULT_LEVEL.NORMAL:
				sp = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_fierce_battle_sprite", "NKM_UI_FIERCE_BATTLE_DIFFICULTY_02_NORMAL", false);
				break;
			case NKCUIFierceBattleSupport.DIFFICULT_LEVEL.CHALLENGE:
				sp = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_fierce_battle_sprite", "NKM_UI_FIERCE_BATTLE_DIFFICULTY_03_CHALLENGE", false);
				break;
			}
			NKCUtil.SetImageSprite(this.m_imgSelectedDifficult, sp, false);
			this.UpdateNightMareMode();
			this.UpdateBossInfo();
			this.UpdateBattleCond();
		}

		// Token: 0x06008B0C RID: 35596 RVA: 0x002F4ADC File Offset: 0x002F2CDC
		private void UpdateNightMareMode()
		{
			int curSelectedBossLv = this.m_FierceDataMgr.GetCurSelectedBossLv(this.m_icurFierceBossGroupID);
			this.m_bNightMareMode = (curSelectedBossLv == 3 && this.m_FierceDataMgr.GetSelfPenalty().Count > 0);
		}

		// Token: 0x040077A0 RID: 30624
		public const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_fierce_battle";

		// Token: 0x040077A1 RID: 30625
		public const string UI_ASSET_NAME = "NKM_UI_FIERCE_BATTLE";

		// Token: 0x040077A2 RID: 30626
		[Header("boss")]
		public NKCUICharacterView m_FIERCE_BATTLE_BOSS_IllustView;

		// Token: 0x040077A3 RID: 30627
		public Text m_BossLv;

		// Token: 0x040077A4 RID: 30628
		public Text m_BossName;

		// Token: 0x040077A5 RID: 30629
		public RectTransform m_FIERCE_BATTLE_BOSS_SLOT_LIST;

		// Token: 0x040077A6 RID: 30630
		public Image m_Background;

		// Token: 0x040077A7 RID: 30631
		[Header("ani")]
		public Animator m_AB_UI_NKM_UI_FIERCE_BATTLE_CONTENT;

		// Token: 0x040077A8 RID: 30632
		[Space]
		public GameObject m_FIERCE_BATTLE_BOSS_INFO_TYPE_01;

		// Token: 0x040077A9 RID: 30633
		public GameObject m_FIERCE_BATTLE_BOSS_INFO_TYPE_02;

		// Token: 0x040077AA RID: 30634
		public GameObject m_FIERCE_BATTLE_BOSS_INFO_TYPE_03;

		// Token: 0x040077AB RID: 30635
		public GameObject m_BOTTOM_Type_01;

		// Token: 0x040077AC RID: 30636
		public GameObject m_BOTTOM_Type_02;

		// Token: 0x040077AD RID: 30637
		[Header("info")]
		public Text m_FIERCE_BATTLE_TOTAL_SCORE_TEXT_1;

		// Token: 0x040077AE RID: 30638
		public Text m_FIERCE_BATTLE_TOTAL_SCORE_TEXT_2;

		// Token: 0x040077AF RID: 30639
		public Text m_FIERCE_BATTLE_TIME_TEXT;

		// Token: 0x040077B0 RID: 30640
		[Header("wait ui")]
		public Text m_Disc;

		// Token: 0x040077B1 RID: 30641
		[Header("ready ui")]
		public Text m_FIERCE_BATTLE_NOW_BOSS_SCORE_TEXT;

		// Token: 0x040077B2 RID: 30642
		public Text m_FIERCE_BATTLE_NOW_BOSS_SCORE_TEXT_2;

		// Token: 0x040077B3 RID: 30643
		public NKCUIComStateButton m_FIERCE_BATTLE_NOW_BOSS_SCORE_BUTTON_RESET;

		// Token: 0x040077B4 RID: 30644
		[Header("battle condition")]
		public RectTransform m_rtBCSlotParents;

		// Token: 0x040077B5 RID: 30645
		public NKCUIFierceBattleCondition m_pfbBCSlots;

		// Token: 0x040077B6 RID: 30646
		private List<NKCUIFierceBattleCondition> m_lstBattleCond = new List<NKCUIFierceBattleCondition>();

		// Token: 0x040077B7 RID: 30647
		[Header("result ui")]
		public Text m_FIERCE_BATTLE_NOW_BOSS_SCORE_TEXT_3;

		// Token: 0x040077B8 RID: 30648
		public Text m_FIERCE_BATTLE_NOW_BOSS_SCORE_TEXT_4;

		// Token: 0x040077B9 RID: 30649
		public NKCUIComStateButton m_FIERCE_BATTLE_NOW_BOSS_SCORE_BUTTON_RESET_2;

		// Token: 0x040077BA RID: 30650
		public LoopScrollRect m_FIERCE_BATTLE_BOSS_RANK_FINAL_ScrollRect;

		// Token: 0x040077BB RID: 30651
		[Header("button ui")]
		public NKCUIComStateButton m_FIERCE_BATTLE_BUTTON_SCORE_REWARD;

		// Token: 0x040077BC RID: 30652
		public GameObject m_FIERCE_BATTLE_BUTTON_SCORE_REWARD_REDDOT;

		// Token: 0x040077BD RID: 30653
		public NKCUIComStateButton m_FIERCE_BATTLE_BUTTON_RANK_INFO;

		// Token: 0x040077BE RID: 30654
		public NKCUIComStateButton m_FIERCE_BATTLE_BUTTON_RANK_SHORTCUT;

		// Token: 0x040077BF RID: 30655
		public GameObject m_FIERCE_BATTLE_BUTTON_RANK_SHORTCUT_REDDOT;

		// Token: 0x040077C0 RID: 30656
		public NKCUIComStateButton m_FIERCE_BATTLE_BUTTON_BOSS_PERSONAL_RANK;

		// Token: 0x040077C1 RID: 30657
		public NKCUIComStateButton m_csbtnShop;

		// Token: 0x040077C2 RID: 30658
		[Space]
		public GameObject m_FIERCE_BATTLE_TOP3_LIST_ScrollRect;

		// Token: 0x040077C3 RID: 30659
		public GameObject m_FIERCE_BATTLE_TOP3_LIST_NODATA;

		// Token: 0x040077C4 RID: 30660
		public List<NKCUIFierceBattleBossPersonalRankSlot> m_lstRankSlot;

		// Token: 0x040077C5 RID: 30661
		[Header("출격 버튼")]
		public GameObject m_BUTTON_Root;

		// Token: 0x040077C6 RID: 30662
		public GameObject m_FIERCE_BATTLE_BUTTON_EnterLimit;

		// Token: 0x040077C7 RID: 30663
		public Text m_EnterLimit_TEXT;

		// Token: 0x040077C8 RID: 30664
		public NKCUIComStateButton m_FIERCE_BATTLE_BUTTON_READY;

		// Token: 0x040077C9 RID: 30665
		public NKCUIComStateButton m_FIERCE_BATTLE_BUTTON_START;

		// Token: 0x040077CA RID: 30666
		public GameObject m_FIERCE_BATTLE_BUTTON_READY_Disable;

		// Token: 0x040077CB RID: 30667
		public GameObject m_FIERCE_BATTLE_BUTTON_READY_Normal;

		// Token: 0x040077CC RID: 30668
		public List<NKCUIComToggle> m_lstDifficultToggle = new List<NKCUIComToggle>();

		// Token: 0x040077CD RID: 30669
		public Image m_imgSelectedDifficult;

		// Token: 0x040077CE RID: 30670
		public GameObject m_objDifficultSelect;

		// Token: 0x040077CF RID: 30671
		[Header("작전 상태")]
		public GameObject m_FIERCE_BATTLE_NOTICE;

		// Token: 0x040077D0 RID: 30672
		public GameObject m_FIERCE_BATTLE_NOTICE_1;

		// Token: 0x040077D1 RID: 30673
		public GameObject m_FIERCE_BATTLE_NOTICE_2;

		// Token: 0x040077D2 RID: 30674
		[Header("BestLineUp")]
		public NKCUIComToggle m_MY_BEST_DECK_Toggle;

		// Token: 0x040077D3 RID: 30675
		public GameObject m_DECK_List;

		// Token: 0x040077D4 RID: 30676
		public GameObject m_FIERCE_BATTLE_MY_BEST_DECK_NODATA_TEXT;

		// Token: 0x040077D5 RID: 30677
		public Image m_ANIM_SHIP_IMG;

		// Token: 0x040077D6 RID: 30678
		public List<NKCDeckViewUnitSlot> m_lstNKCDeckViewUnitSlot;

		// Token: 0x040077D7 RID: 30679
		[Header("nightmare")]
		public GameObject m_FIERCE_BATTLE_NIGHTMARE;

		// Token: 0x040077D8 RID: 30680
		public GameObject m_NightmareMode;

		// Token: 0x040077D9 RID: 30681
		public GameObject m_NightmareModeBGOFF;

		// Token: 0x040077DA RID: 30682
		public GameObject m_NightmareModeBGON;

		// Token: 0x040077DB RID: 30683
		public Animator m_aniNightMare;

		// Token: 0x040077DC RID: 30684
		[Header("Self Penalty UI")]
		public NKCUIComStateButton m_csbtnSelfPenalty;

		// Token: 0x040077DD RID: 30685
		public GameObject m_objSelfPenaltyBtnNornal;

		// Token: 0x040077DE RID: 30686
		public GameObject m_objSelfPenaltyBtnDisable;

		// Token: 0x040077DF RID: 30687
		public GameObject m_objSelfPenaltyDesc;

		// Token: 0x040077E0 RID: 30688
		public Text m_lbSelfPenaltyDesc;

		// Token: 0x040077E1 RID: 30689
		[Header("일일보상")]
		public GameObject m_objDailyReward;

		// Token: 0x040077E2 RID: 30690
		public NKCUIComStateButton m_csbtnDailyReward;

		// Token: 0x040077E3 RID: 30691
		public GameObject m_ObjDailyRewardRedDot;

		// Token: 0x040077E4 RID: 30692
		private RectTransform m_rtFierceBossIllust;

		// Token: 0x040077E5 RID: 30693
		private NKCFierceBattleSupportDataMgr m_FierceDataMgr;

		// Token: 0x040077E6 RID: 30694
		private NKMFierceTemplet m_curFierceTemplet;

		// Token: 0x040077E7 RID: 30695
		private int m_icurFierceBossGroupID;

		// Token: 0x040077E8 RID: 30696
		private const string ANI_INTRO = "INTRO";

		// Token: 0x040077E9 RID: 30697
		private const string ANI_READY = "READY";

		// Token: 0x040077EA RID: 30698
		private const string ANI_RESULT = "RESULT";

		// Token: 0x040077EB RID: 30699
		private const string ANI_BACK = "BACK";

		// Token: 0x040077EC RID: 30700
		private const string ANI_INTRO_IDLE = "INTRO_IDLE";

		// Token: 0x040077ED RID: 30701
		private const string ANI_READY_IDLE = "READY_IDLE";

		// Token: 0x040077EE RID: 30702
		private const string ANI_RESULT_IDLE = "RESULT_IDLE";

		// Token: 0x040077EF RID: 30703
		private NKCUIFierceBattleSupport.UI_STATUS m_curUIStatus;

		// Token: 0x040077F0 RID: 30704
		private bool m_bNightMareMode;

		// Token: 0x040077F1 RID: 30705
		private bool m_bFierceActivateTimeCnt;

		// Token: 0x040077F2 RID: 30706
		private Stack<NKCUIFierceBattleBossPersonalRankSlot> m_stk = new Stack<NKCUIFierceBattleBossPersonalRankSlot>();

		// Token: 0x040077F3 RID: 30707
		private List<NKCUIFierceBattleBossPersonalRankSlot> m_lstVisible = new List<NKCUIFierceBattleBossPersonalRankSlot>();

		// Token: 0x02001989 RID: 6537
		private enum DIFFICULT_LEVEL
		{
			// Token: 0x0400AC47 RID: 44103
			EASY,
			// Token: 0x0400AC48 RID: 44104
			NORMAL,
			// Token: 0x0400AC49 RID: 44105
			CHALLENGE
		}

		// Token: 0x0200198A RID: 6538
		private enum UI_STATUS
		{
			// Token: 0x0400AC4B RID: 44107
			US_NONE,
			// Token: 0x0400AC4C RID: 44108
			US_INTRO,
			// Token: 0x0400AC4D RID: 44109
			US_READY,
			// Token: 0x0400AC4E RID: 44110
			US_BACK,
			// Token: 0x0400AC4F RID: 44111
			US_RESULT,
			// Token: 0x0400AC50 RID: 44112
			US_INTRO_IDLE,
			// Token: 0x0400AC51 RID: 44113
			US_READY_IDLE,
			// Token: 0x0400AC52 RID: 44114
			US_RESULT_IDLE
		}
	}
}
