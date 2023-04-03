using System;
using System.Collections.Generic;
using Cs.Logging;
using NKC.Trim;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Trim
{
	// Token: 0x02000AAC RID: 2732
	public class NKCUIPopupTrimDungeon : NKCUIBase
	{
		// Token: 0x17001455 RID: 5205
		// (get) Token: 0x06007981 RID: 31105 RVA: 0x0028699C File Offset: 0x00284B9C
		public static NKCUIPopupTrimDungeon Instance
		{
			get
			{
				if (NKCUIPopupTrimDungeon.m_Instance == null)
				{
					NKCUIPopupTrimDungeon.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPopupTrimDungeon>("ab_ui_trim", "AB_UI_TRIM_DUNGEON", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPopupTrimDungeon.CleanUpInstance)).GetInstance<NKCUIPopupTrimDungeon>();
					NKCUIPopupTrimDungeon.m_Instance.InitUI();
				}
				return NKCUIPopupTrimDungeon.m_Instance;
			}
		}

		// Token: 0x17001456 RID: 5206
		// (get) Token: 0x06007982 RID: 31106 RVA: 0x002869EB File Offset: 0x00284BEB
		public static bool HasInstance
		{
			get
			{
				return NKCUIPopupTrimDungeon.m_Instance != null;
			}
		}

		// Token: 0x17001457 RID: 5207
		// (get) Token: 0x06007983 RID: 31107 RVA: 0x002869F8 File Offset: 0x00284BF8
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIPopupTrimDungeon.m_Instance != null && NKCUIPopupTrimDungeon.m_Instance.IsOpen;
			}
		}

		// Token: 0x06007984 RID: 31108 RVA: 0x00286A13 File Offset: 0x00284C13
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPopupTrimDungeon.m_Instance != null && NKCUIPopupTrimDungeon.m_Instance.IsOpen)
			{
				NKCUIPopupTrimDungeon.m_Instance.Close();
			}
		}

		// Token: 0x06007985 RID: 31109 RVA: 0x00286A38 File Offset: 0x00284C38
		private static void CleanUpInstance()
		{
			NKCUIPopupTrimDungeon.m_Instance = null;
		}

		// Token: 0x17001458 RID: 5208
		// (get) Token: 0x06007986 RID: 31110 RVA: 0x00286A40 File Offset: 0x00284C40
		public override string MenuName
		{
			get
			{
				return "Trim Dungeon";
			}
		}

		// Token: 0x17001459 RID: 5209
		// (get) Token: 0x06007987 RID: 31111 RVA: 0x00286A47 File Offset: 0x00284C47
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x1700145A RID: 5210
		// (get) Token: 0x06007988 RID: 31112 RVA: 0x00286A4A File Offset: 0x00284C4A
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Normal;
			}
		}

		// Token: 0x1700145B RID: 5211
		// (get) Token: 0x06007989 RID: 31113 RVA: 0x00286A4D File Offset: 0x00284C4D
		public int TrimId
		{
			get
			{
				return this.m_trimId;
			}
		}

		// Token: 0x1700145C RID: 5212
		// (get) Token: 0x0600798A RID: 31114 RVA: 0x00286A55 File Offset: 0x00284C55
		public int SelectedGroup
		{
			get
			{
				return this.m_selectedGroup;
			}
		}

		// Token: 0x1700145D RID: 5213
		// (get) Token: 0x0600798B RID: 31115 RVA: 0x00286A5D File Offset: 0x00284C5D
		public int SelectedLevel
		{
			get
			{
				return this.m_selectedLevel;
			}
		}

		// Token: 0x0600798C RID: 31116 RVA: 0x00286A68 File Offset: 0x00284C68
		private void InitUI()
		{
			if (this.m_squadSlot != null)
			{
				int num = this.m_squadSlot.Length;
				for (int i = 0; i < num; i++)
				{
					NKCUITrimSquadSlot nkcuitrimSquadSlot = this.m_squadSlot[i];
					if (nkcuitrimSquadSlot != null)
					{
						nkcuitrimSquadSlot.Init(i, new NKCUITrimSquadSlot.OnDeckConfirm(this.OnDeckConfirm));
					}
				}
			}
			if (this.m_trimLevelScrollRect != null)
			{
				this.m_trimLevelScrollRect.dOnGetObject += this.GetPresetSlot;
				this.m_trimLevelScrollRect.dOnReturnObject += this.ReturnPresetSlot;
				this.m_trimLevelScrollRect.dOnProvideData += this.ProvidePresetData;
				this.m_trimLevelScrollRect.ContentConstraintCount = 1;
				this.m_trimLevelScrollRect.TotalCount = 0;
				this.m_trimLevelScrollRect.PrepareCells(0);
			}
			NKCUITrimReward trimReward = this.m_trimReward;
			if (trimReward != null)
			{
				trimReward.Init();
			}
			NKCUtil.SetButtonClickDelegate(this.m_csbtnStart, new UnityAction(this.OnClickStart));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnStartResource, new UnityAction(this.OnClickStart));
			NKCUITrimUtility.InitBattleCondition(this.m_battleCondParent, true);
			NKCUIComResourceButton comResourceButton = this.m_comResourceButton;
			if (comResourceButton != null)
			{
				comResourceButton.Init();
			}
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglSkip, new UnityAction<bool>(this.OnClickSkip));
			NKCUIOperationSkip operationSkip = this.m_operationSkip;
			if (operationSkip == null)
			{
				return;
			}
			operationSkip.Init(new NKCUIOperationSkip.OnCountUpdated(this.OnOperationSkipUpdated), new UnityAction(this.OnClickOperationSkipClose));
		}

		// Token: 0x0600798D RID: 31117 RVA: 0x00286BC4 File Offset: 0x00284DC4
		public override void CloseInternal()
		{
			NKCLocalDeckDataManager.Clear();
			this.m_tglSkip.Select(false, false, false);
			base.gameObject.SetActive(false);
		}

		// Token: 0x0600798E RID: 31118 RVA: 0x00286BE8 File Offset: 0x00284DE8
		public void Open(int trimId)
		{
			this.m_trimId = trimId;
			this.m_dateUpdateTimerSec = 0f;
			this.m_dateUpdateTimerMin = 0f;
			NKMTrimTemplet nkmtrimTemplet = NKMTrimTemplet.Find(trimId);
			NKCLocalDeckDataManager.Clear();
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmtrimTemplet != null)
			{
				int num = nkmtrimTemplet.TrimDungeonIds.Length;
				long userUId = (nkmuserData != null) ? nkmuserData.m_UserUID : 0L;
				for (int i = 0; i < num; i++)
				{
					NKCLocalDeckDataManager.LoadLocalDeckData(NKCUITrimUtility.GetTrimDeckKey(trimId, nkmtrimTemplet.TrimDungeonIds[i], userUId), i, 8);
				}
				NKCLocalDeckDataManager.SetDataLoadedState(true);
				NKCUtil.SetGameobjectActive(this.m_objSkip, nkmtrimTemplet.m_bActiveBattleSkip);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objSkip, false);
			}
			this.RefreshUI(true);
			base.UIOpened(true);
		}

		// Token: 0x0600798F RID: 31119 RVA: 0x00286C9C File Offset: 0x00284E9C
		public void RefreshUI(bool resetLevelTab)
		{
			this.m_maxTrimLevel = 0;
			this.m_selectedGroup = 0;
			NKMTrimTemplet nkmtrimTemplet = NKMTrimTemplet.Find(this.m_trimId);
			string text = null;
			string text2 = null;
			Sprite sp = null;
			int stageReqItemId = 0;
			int stageReqItemCount = 0;
			if (nkmtrimTemplet != null)
			{
				text = NKCStringTable.GetString(nkmtrimTemplet.TirmGroupName, false);
				text2 = NKCStringTable.GetString(nkmtrimTemplet.TirmGroupDesc, false);
				this.m_maxTrimLevel = nkmtrimTemplet.MaxTrimLevel;
				this.m_selectedGroup = nkmtrimTemplet.TrimPointGroup;
				sp = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_TRIM_MAP_IMG", nkmtrimTemplet.TrimGroupBGPrefab, false);
				stageReqItemId = nkmtrimTemplet.m_StageReqItemID;
				stageReqItemCount = nkmtrimTemplet.m_StageReqItemCount;
			}
			NKMUserData userData = NKCScenManager.CurrentUserData();
			this.m_clearedLevel = NKCUITrimUtility.GetClearedTrimLevel(userData, this.m_trimId);
			this.m_selectedLevel = Mathf.Min(this.m_maxTrimLevel, this.m_clearedLevel + 1);
			base.gameObject.SetActive(true);
			if (this.m_trimLevelScrollRect != null)
			{
				int num = Mathf.Min(this.m_maxTrimLevel, this.m_selectedLevel + 1);
				this.m_trimLevelScrollRect.TotalCount = num;
				if (resetLevelTab)
				{
					int num2 = Mathf.Max(0, num - 10);
					int num3 = Mathf.Max(this.m_selectedLevel - 1, 0);
					float time = Mathf.Max(0.2f, (float)(num3 - num2) / 20f * this.m_scrollTime);
					this.m_trimLevelScrollRect.SetIndexPosition(num2);
					this.m_trimLevelScrollRect.ScrollToCell(num3, time, LoopScrollRect.ScrollTarget.Top, delegate
					{
						this.m_trimLevelScrollRect.RefreshCells(false);
					});
				}
				else
				{
					this.m_trimLevelScrollRect.RefreshCells(false);
				}
			}
			NKCUtil.SetLabelText(this.m_lbDungeonName, string.IsNullOrEmpty(text) ? " - " : text);
			NKCUtil.SetLabelText(this.m_lbDungeonDesc, string.IsNullOrEmpty(text2) ? " - " : text2);
			NKCUtil.SetLabelText(this.m_lbTrimLevel, this.m_selectedLevel.ToString());
			NKCUtil.SetImageSprite(this.m_imgMap, sp, true);
			NKMTrimIntervalTemplet trimInterval = NKMTrimIntervalTemplet.Find(NKCSynchronizedTime.ServiceTime);
			NKCUtil.SetGameobjectActive(this.m_objEnterLimitRoot, NKCUITrimUtility.IsEnterCountLimited(trimInterval));
			if (NKCUITrimUtility.IsEnterCountLimited(trimInterval))
			{
				string enterLimitMsg = NKCUITrimUtility.GetEnterLimitMsg(trimInterval);
				NKCUtil.SetLabelText(this.m_lbEnterLimit, enterLimitMsg);
			}
			this.m_bShowIntervalTime = nkmtrimTemplet.ShowInterval;
			NKCUtil.SetGameobjectActive(this.m_objRemainDate, this.m_bShowIntervalTime);
			if (this.m_bShowIntervalTime)
			{
				string remainTimeStringExWithoutEnd = NKCUtilString.GetRemainTimeStringExWithoutEnd(NKCUITrimUtility.GetRemainDateMsg());
				NKCUtil.SetLabelText(this.m_lbRemainDate, remainTimeStringExWithoutEnd);
			}
			NKCUITrimReward trimReward = this.m_trimReward;
			if (trimReward != null)
			{
				trimReward.SetData(this.m_trimId, this.m_selectedLevel);
			}
			NKCUITrimUtility.SetBattleCondition(this.m_battleCondParent, nkmtrimTemplet, this.m_selectedLevel, true);
			int trimLevelScore = NKCUITrimUtility.GetTrimLevelScore(this.m_trimId, this.m_selectedLevel);
			NKCUtil.SetLabelText(this.m_lbTrimLevelScore, trimLevelScore.ToString());
			int recommendedPower = NKCUITrimUtility.GetRecommendedPower(this.m_selectedGroup, this.m_selectedLevel);
			NKCUtil.SetLabelText(this.m_lbRecommendedPower, recommendedPower.ToString());
			this.UpdateSquadSlot(this.m_selectedGroup, this.m_selectedLevel);
			this.UpdateStartButtonState(userData, stageReqItemId, stageReqItemCount);
			NKCUtil.SetGameobjectActive(this.m_operationSkip, false);
		}

		// Token: 0x06007990 RID: 31120 RVA: 0x00286F88 File Offset: 0x00285188
		public override bool OnHotkey(HotkeyEventType hotkey)
		{
			if (this.m_trimLevelScrollRect.content.childCount <= 0)
			{
				return false;
			}
			if (hotkey == HotkeyEventType.Up)
			{
				int num = Mathf.Max(1, this.m_selectedLevel - 1);
				this.OnClickLevelSlot(num, false);
				int index = Mathf.Max(0, num - 1);
				Transform child = this.m_trimLevelScrollRect.GetChild(index);
				RectTransform component = this.m_trimLevelScrollRect.GetComponent<RectTransform>();
				if (!(child != null) || !(component != null) || !component.GetWorldRect().Contains(child.position))
				{
					VerticalLayoutGroup component2 = this.m_trimLevelScrollRect.content.GetComponent<VerticalLayoutGroup>();
					float num2 = (component2 != null) ? component2.spacing : 0f;
					RectTransform component3 = this.m_trimLevelScrollRect.content.GetChild(0).GetComponent<RectTransform>();
					float num3 = (component3 != null) ? component3.GetHeight() : 0f;
					this.m_trimLevelScrollRect.MovePosition(new Vector2(0f, -num3 - num2));
				}
				return true;
			}
			if (hotkey != HotkeyEventType.Down)
			{
				return false;
			}
			int num4 = Mathf.Min(this.m_maxTrimLevel, this.m_selectedLevel + 1);
			if (num4 <= Mathf.Min(this.m_maxTrimLevel, this.m_clearedLevel + 1))
			{
				this.OnClickLevelSlot(num4, false);
				int index2 = Mathf.Max(0, num4 - 1);
				Transform child2 = this.m_trimLevelScrollRect.GetChild(index2);
				RectTransform component4 = this.m_trimLevelScrollRect.GetComponent<RectTransform>();
				if (!(child2 != null) || !(component4 != null) || !component4.GetWorldRect().Contains(child2.position))
				{
					VerticalLayoutGroup component5 = this.m_trimLevelScrollRect.content.GetComponent<VerticalLayoutGroup>();
					float num5 = (component5 != null) ? component5.spacing : 0f;
					RectTransform component6 = this.m_trimLevelScrollRect.content.GetChild(0).GetComponent<RectTransform>();
					float num6 = (component6 != null) ? component6.GetHeight() : 0f;
					this.m_trimLevelScrollRect.MovePosition(new Vector2(0f, num6 + num5));
				}
			}
			return true;
		}

		// Token: 0x06007991 RID: 31121 RVA: 0x002871A8 File Offset: 0x002853A8
		private void Update()
		{
			if (!this.m_bShowIntervalTime)
			{
				return;
			}
			if (this.m_dateUpdateTimerSec > 1f)
			{
				DateTime remainDateMsg = NKCUITrimUtility.GetRemainDateMsg();
				if (NKCSynchronizedTime.GetTimeLeft(remainDateMsg).TotalMinutes >= 1.0 && this.m_dateUpdateTimerMin < 60f)
				{
					this.m_dateUpdateTimerSec = 0f;
					return;
				}
				string remainTimeStringExWithoutEnd = NKCUtilString.GetRemainTimeStringExWithoutEnd(remainDateMsg);
				NKCUtil.SetLabelText(this.m_lbRemainDate, remainTimeStringExWithoutEnd);
				this.m_dateUpdateTimerSec = 0f;
				this.m_dateUpdateTimerMin = 0f;
			}
			this.m_dateUpdateTimerSec += Time.deltaTime;
			this.m_dateUpdateTimerMin += Time.deltaTime;
		}

		// Token: 0x06007992 RID: 31122 RVA: 0x00287250 File Offset: 0x00285450
		private RectTransform GetPresetSlot(int index)
		{
			NKCUITrimLevelSlot newInstance = NKCUITrimLevelSlot.GetNewInstance(null);
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x06007993 RID: 31123 RVA: 0x00287264 File Offset: 0x00285464
		private void ReturnPresetSlot(Transform tr)
		{
			NKCUITrimLevelSlot component = tr.GetComponent<NKCUITrimLevelSlot>();
			tr.SetParent(null);
			if (component != null)
			{
				component.DestoryInstance();
				return;
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x06007994 RID: 31124 RVA: 0x0028729C File Offset: 0x0028549C
		private void ProvidePresetData(Transform tr, int index)
		{
			NKCUITrimLevelSlot component = tr.GetComponent<NKCUITrimLevelSlot>();
			if (component == null)
			{
				return;
			}
			component.SetData(this.m_trimId, index + 1, this.m_clearedLevel, new NKCUITrimLevelSlot.OnClickSlot(this.OnClickLevelSlot));
			component.SetSelectedState(this.m_selectedLevel);
			component.SetLock(index + 1 > Mathf.Min(this.m_maxTrimLevel, this.m_clearedLevel + 1));
		}

		// Token: 0x06007995 RID: 31125 RVA: 0x00287308 File Offset: 0x00285508
		private void UpdateSquadSlot(int trimGroup, int trimLevel)
		{
			if (this.m_squadSlot == null)
			{
				return;
			}
			int num = 3;
			NKMTrimTemplet nkmtrimTemplet = NKMTrimTemplet.Find(this.m_trimId);
			int num2 = this.m_squadSlot.Length;
			for (int i = 0; i < num2; i++)
			{
				if (!(this.m_squadSlot[i] == null))
				{
					if (i < num)
					{
						int trimDungeonId = (nkmtrimTemplet != null) ? nkmtrimTemplet.TrimDungeonIds[i] : 0;
						this.m_squadSlot[i].SetActive(true);
						this.m_squadSlot[i].SetData(this.m_trimId, trimDungeonId, trimGroup, trimLevel);
					}
					else
					{
						this.m_squadSlot[i].SetActive(false);
					}
				}
			}
		}

		// Token: 0x06007996 RID: 31126 RVA: 0x0028739C File Offset: 0x0028559C
		private void UpdateStartButtonState(NKMUserData userData, int stageReqItemId, int stageReqItemCount)
		{
			NKMTrimIntervalTemplet trimInterval = NKMTrimIntervalTemplet.Find(NKCSynchronizedTime.ServiceTime);
			int num = (!NKCUITrimUtility.IsEnterCountLimited(trimInterval) || NKCUITrimUtility.IsEnterCountRemaining(trimInterval)) ? 1 : 0;
			bool flag = !NKCUITrimUtility.IsRestoreEnterCountLimited(trimInterval) || NKCUITrimUtility.IsRestoreEnterCountEnable(trimInterval, userData);
			bool flag2 = num == 0 && flag;
			bool flag3 = stageReqItemId > 0 && stageReqItemCount > 0;
			NKCUtil.SetGameobjectActive(this.m_csbtnStart, !flag2 && !flag3);
			NKCUtil.SetGameobjectActive(this.m_csbtnStartResource, flag2 || flag3);
			if (!flag2)
			{
				if (flag3)
				{
					int itemCount = stageReqItemCount;
					if (stageReqItemId == 2)
					{
						NKCCompanyBuff.SetDiscountOfEterniumInEnteringDungeon(NKCScenManager.CurrentUserData().m_companyBuffDataList, ref itemCount);
					}
					NKCUIComResourceButton comResourceButton = this.m_comResourceButton;
					if (comResourceButton != null)
					{
						comResourceButton.SetData(stageReqItemId, itemCount);
					}
					NKCUIComResourceButton comResourceButton2 = this.m_comResourceButton;
					if (comResourceButton2 == null)
					{
						return;
					}
					comResourceButton2.SetTitleText(NKCStringTable.GetString("SI_PF_TRIM_DUNGEON_START_TEXT", false));
				}
				return;
			}
			int restoreItemReqId = NKCUITrimUtility.GetRestoreItemReqId(trimInterval);
			int restoreItemReqCount = NKCUITrimUtility.GetRestoreItemReqCount(trimInterval, userData);
			NKCUIComResourceButton comResourceButton3 = this.m_comResourceButton;
			if (comResourceButton3 != null)
			{
				comResourceButton3.SetData(restoreItemReqId, restoreItemReqCount);
			}
			NKCUIComResourceButton comResourceButton4 = this.m_comResourceButton;
			if (comResourceButton4 == null)
			{
				return;
			}
			comResourceButton4.SetTitleText(NKCStringTable.GetString("SI_PF_TRIM_DUNGEON_START_BUTTON_TEXT", false));
		}

		// Token: 0x06007997 RID: 31127 RVA: 0x0028749C File Offset: 0x0028569C
		private void StartBattle(List<NKMEventDeckData> deckDataList)
		{
			if (!this.m_isSkip)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_TRIM_RESULT().SetUnitUId(NKCLocalDeckDataManager.GetLocalLeaderUnitUId(0));
				NKCPacketSender.Send_NKMPacket_TRIM_START_REQ(this.m_trimId, this.m_selectedLevel, deckDataList);
				return;
			}
			if (this.m_selectedLevel > this.m_clearedLevel)
			{
				NKCPopupOKCancel.OpenOKBox(NKCStringTable.GetString("SI_DP_NOTICE", false), NKCUtilString.GET_STRING_CONTENTS_UNLOCK_CLEAR_STAGE, null, "");
				return;
			}
			NKCPacketSender.Send_NKMPacket_TRIM_DUNGEON_SKIP_REQ(this.m_trimId, this.m_selectedLevel, this.m_skipCount, deckDataList);
		}

		// Token: 0x06007998 RID: 31128 RVA: 0x0028751C File Offset: 0x0028571C
		private void SetSkipCountUIData()
		{
			int maxCount = 99;
			NKMTrimIntervalTemplet trimInterval = NKMTrimIntervalTemplet.Find(NKCSynchronizedTime.ServiceTime);
			NKMTrimTemplet nkmtrimTemplet = NKMTrimTemplet.Find(this.m_trimId);
			if (NKCUITrimUtility.IsEnterCountLimited(trimInterval))
			{
				maxCount = NKCUITrimUtility.GetRemainEnterCount(trimInterval);
			}
			int num = 0;
			int dungeonCostItemCount = 0;
			if (nkmtrimTemplet != null)
			{
				num = nkmtrimTemplet.m_StageReqItemID;
				dungeonCostItemCount = nkmtrimTemplet.m_StageReqItemCount;
				if (num == 2)
				{
					NKCCompanyBuff.SetDiscountOfEterniumInEnteringDungeon(NKCScenManager.CurrentUserData().m_companyBuffDataList, ref dungeonCostItemCount);
				}
			}
			NKCUIOperationSkip operationSkip = this.m_operationSkip;
			if (operationSkip == null)
			{
				return;
			}
			operationSkip.SetData(NKMCommonConst.SkipCostMiscItemId, NKMCommonConst.SkipCostMiscItemCount, num, dungeonCostItemCount, this.m_skipCount, 1, maxCount);
		}

		// Token: 0x06007999 RID: 31129 RVA: 0x002875A4 File Offset: 0x002857A4
		private void UpdateAttackCost(NKMTrimTemplet trimTemplet)
		{
			if (trimTemplet == null)
			{
				return;
			}
			int stageReqItemCount = trimTemplet.m_StageReqItemCount;
			if (trimTemplet.m_StageReqItemID == 2)
			{
				NKCCompanyBuff.SetDiscountOfEterniumInEnteringDungeon(NKCScenManager.CurrentUserData().m_companyBuffDataList, ref stageReqItemCount);
			}
			NKCUIComResourceButton comResourceButton = this.m_comResourceButton;
			if (comResourceButton == null)
			{
				return;
			}
			comResourceButton.SetData(trimTemplet.m_StageReqItemID, this.m_skipCount * stageReqItemCount);
		}

		// Token: 0x0600799A RID: 31130 RVA: 0x002875F4 File Offset: 0x002857F4
		private bool HaveEnoughResource()
		{
			NKMTrimTemplet nkmtrimTemplet = NKMTrimTemplet.Find(this.m_trimId);
			if (nkmtrimTemplet != null)
			{
				if (nkmtrimTemplet.m_StageReqItemID == 2)
				{
					if (!NKCUtil.IsCanStartEterniumStage(nkmtrimTemplet.m_StageReqItemID, nkmtrimTemplet.m_StageReqItemCount, true))
					{
						return false;
					}
				}
				else if (NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(nkmtrimTemplet.m_StageReqItemID) < (long)nkmtrimTemplet.m_StageReqItemCount)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_ATTACK_COST_IS_NOT_ENOUGH, null, "");
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600799B RID: 31131 RVA: 0x00287665 File Offset: 0x00285865
		private void OnDeckConfirm()
		{
			this.UpdateSquadSlot(this.m_selectedGroup, this.m_selectedLevel);
		}

		// Token: 0x0600799C RID: 31132 RVA: 0x0028767C File Offset: 0x0028587C
		private void OnClickLevelSlot(int trimLevel, bool isLocked)
		{
			if (isLocked)
			{
				return;
			}
			this.m_selectedLevel = trimLevel;
			LoopScrollRect trimLevelScrollRect = this.m_trimLevelScrollRect;
			if (trimLevelScrollRect != null)
			{
				trimLevelScrollRect.RefreshCells(false);
			}
			NKCUtil.SetLabelText(this.m_lbTrimLevel, trimLevel.ToString());
			NKCUITrimReward trimReward = this.m_trimReward;
			if (trimReward != null)
			{
				trimReward.SetData(this.m_trimId, this.m_selectedLevel);
			}
			NKMTrimTemplet trimTemplet = NKMTrimTemplet.Find(this.m_trimId);
			NKCUITrimUtility.SetBattleCondition(this.m_battleCondParent, trimTemplet, this.m_selectedLevel, true);
			int trimLevelScore = NKCUITrimUtility.GetTrimLevelScore(this.m_trimId, this.m_selectedLevel);
			NKCUtil.SetLabelText(this.m_lbTrimLevelScore, trimLevelScore.ToString());
			int recommendedPower = NKCUITrimUtility.GetRecommendedPower(this.m_selectedGroup, this.m_selectedLevel);
			NKCUtil.SetLabelText(this.m_lbRecommendedPower, recommendedPower.ToString());
			this.UpdateSquadSlot(this.m_selectedGroup, this.m_selectedLevel);
			this.m_tglSkip.Select(false, false, false);
		}

		// Token: 0x0600799D RID: 31133 RVA: 0x00287760 File Offset: 0x00285960
		private void OnClickStart()
		{
			if (NKCTrimManager.ProcessTrim())
			{
				return;
			}
			NKMTrimIntervalTemplet trimInterval = NKMTrimIntervalTemplet.Find(NKCSynchronizedTime.ServiceTime);
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (trimInterval == null)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_TRIM_INTERVAL_END, delegate()
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_WORLDMAP, true);
				}, "");
				return;
			}
			NKMTrimTemplet nkmtrimTemplet = NKMTrimTemplet.Find(this.m_trimId);
			if (nkmtrimTemplet != null && !NKMContentUnlockManager.IsContentUnlocked(nkmuserData, nkmtrimTemplet.m_UnlockInfo, false))
			{
				string message = NKCContentManager.MakeUnlockConditionString(nkmtrimTemplet.m_UnlockInfo, false);
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(message, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return;
			}
			if (NKCUITrimUtility.IsEnterCountLimited(trimInterval) && !NKCUITrimUtility.IsEnterCountRemaining(trimInterval))
			{
				if (NKCUITrimUtility.IsRestoreEnterCountEnable(trimInterval, nkmuserData))
				{
					int remainRestoreCount = NKCUITrimUtility.GetRemainRestoreCount(trimInterval, nkmuserData);
					int restoreLimitCount = NKCUITrimUtility.GetRestoreLimitCount(trimInterval);
					string content = string.Format(NKCUtilString.GET_STRING_TRIM_NOT_ENOUGH_TRY_COUNT_RESTORE, remainRestoreCount, restoreLimitCount);
					int restoreItemReqId = NKCUITrimUtility.GetRestoreItemReqId(trimInterval);
					int restoreItemReqCount = NKCUITrimUtility.GetRestoreItemReqCount(trimInterval, nkmuserData);
					NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_NOTICE, content, restoreItemReqId, restoreItemReqCount, delegate()
					{
						NKCPacketSender.Send_NKMPacket_TRIM_RESTORE_REQ(trimInterval.TrimIntervalID);
					}, null, false);
					return;
				}
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_TRIM_NOT_ENOUGH_TRY_COUNT, null, "");
				return;
			}
			else
			{
				Dictionary<int, NKMEventDeckData> allLocalDeckData = NKCLocalDeckDataManager.GetAllLocalDeckData();
				List<NKMEventDeckData> deckDataList = new List<NKMEventDeckData>();
				for (int i = 0; i < 3; i++)
				{
					if (allLocalDeckData.ContainsKey(i))
					{
						deckDataList.Add(allLocalDeckData[i]);
					}
				}
				if (deckDataList.Count < 3)
				{
					Log.Error("Not Enough Deck", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Trim/NKCUIPopupTrimDungeon.cs", 596);
					return;
				}
				for (int j = 0; j < 3; j++)
				{
					if (deckDataList[j].m_ShipUID <= 0L)
					{
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_DECK_NO_SHIP), null, "");
						return;
					}
				}
				if (!this.HaveEnoughResource())
				{
					return;
				}
				for (int k = 0; k < 3; k++)
				{
					bool flag = false;
					using (Dictionary<int, long>.ValueCollection.Enumerator enumerator = deckDataList[k].m_dicUnit.Values.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current > 0L)
							{
								flag = true;
							}
						}
					}
					if (!flag)
					{
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_TRIM_NOT_ENOUGH_SQUAD, null, "");
						return;
					}
				}
				if (!this.m_operationSkip.gameObject.activeSelf)
				{
					int recommendedPower = NKCUITrimUtility.GetRecommendedPower(this.m_selectedGroup, this.m_selectedLevel);
					NKCPopupOKCancel.OnButton <>9__2;
					for (int l = 0; l < 3; l++)
					{
						if (NKCLocalDeckDataManager.GetOperationPower(l, false, false, false) < recommendedPower)
						{
							string get_STRING_NOTICE = NKCUtilString.GET_STRING_NOTICE;
							string get_STRING_TRIM_NOT_ENOUGH_POWER = NKCUtilString.GET_STRING_TRIM_NOT_ENOUGH_POWER;
							NKCPopupOKCancel.OnButton onOkButton;
							if ((onOkButton = <>9__2) == null)
							{
								onOkButton = (<>9__2 = delegate()
								{
									this.StartBattle(deckDataList);
								});
							}
							NKCPopupOKCancel.OpenOKCancelBox(get_STRING_NOTICE, get_STRING_TRIM_NOT_ENOUGH_POWER, onOkButton, null, false);
							return;
						}
					}
				}
				this.StartBattle(deckDataList);
				return;
			}
		}

		// Token: 0x0600799E RID: 31134 RVA: 0x00287A84 File Offset: 0x00285C84
		private void OnClickSkip(bool bSet)
		{
			NKMTrimTemplet nkmtrimTemplet = NKMTrimTemplet.Find(this.m_trimId);
			if (nkmtrimTemplet == null)
			{
				this.m_tglSkip.Select(false, false, false);
				return;
			}
			this.m_skipCount = 1;
			if (bSet)
			{
				if (this.m_selectedLevel > this.m_clearedLevel)
				{
					NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_CONTENTS_UNLOCK_CLEAR_STAGE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					this.m_tglSkip.Select(false, false, false);
					return;
				}
				if (!this.HaveEnoughResource())
				{
					this.m_tglSkip.Select(false, false, false);
					return;
				}
				this.m_isSkip = true;
				this.UpdateAttackCost(nkmtrimTemplet);
				this.SetSkipCountUIData();
			}
			if (!bSet)
			{
				this.m_isSkip = false;
				this.UpdateAttackCost(nkmtrimTemplet);
				this.SetSkipCountUIData();
			}
			NKCUtil.SetGameobjectActive(this.m_operationSkip, bSet);
		}

		// Token: 0x0600799F RID: 31135 RVA: 0x00287B3C File Offset: 0x00285D3C
		private void OnOperationSkipUpdated(int newCount)
		{
			this.m_skipCount = newCount;
			NKMTrimTemplet trimTemplet = NKMTrimTemplet.Find(this.m_trimId);
			this.UpdateAttackCost(trimTemplet);
		}

		// Token: 0x060079A0 RID: 31136 RVA: 0x00287B63 File Offset: 0x00285D63
		private void OnClickOperationSkipClose()
		{
			this.m_tglSkip.Select(false, false, false);
		}

		// Token: 0x0400663A RID: 26170
		public const string UI_ASSET_BUNDLE_NAME = "ab_ui_trim";

		// Token: 0x0400663B RID: 26171
		public const string UI_ASSET_NAME = "AB_UI_TRIM_DUNGEON";

		// Token: 0x0400663C RID: 26172
		private static NKCUIPopupTrimDungeon m_Instance;

		// Token: 0x0400663D RID: 26173
		public Text m_lbDungeonName;

		// Token: 0x0400663E RID: 26174
		public Text m_lbDungeonDesc;

		// Token: 0x0400663F RID: 26175
		public Text m_lbEnterLimit;

		// Token: 0x04006640 RID: 26176
		public GameObject m_objRemainDate;

		// Token: 0x04006641 RID: 26177
		public Text m_lbRemainDate;

		// Token: 0x04006642 RID: 26178
		public Text m_lbTrimLevel;

		// Token: 0x04006643 RID: 26179
		public Text m_lbTrimLevelScore;

		// Token: 0x04006644 RID: 26180
		public Text m_lbRecommendedPower;

		// Token: 0x04006645 RID: 26181
		public Image m_imgMap;

		// Token: 0x04006646 RID: 26182
		public LoopScrollRect m_trimLevelScrollRect;

		// Token: 0x04006647 RID: 26183
		public NKCUITrimSquadSlot[] m_squadSlot;

		// Token: 0x04006648 RID: 26184
		public Transform m_battleCondParent;

		// Token: 0x04006649 RID: 26185
		public NKCUITrimReward m_trimReward;

		// Token: 0x0400664A RID: 26186
		public NKCUIComStateButton m_csbtnStart;

		// Token: 0x0400664B RID: 26187
		public NKCUIComStateButton m_csbtnStartResource;

		// Token: 0x0400664C RID: 26188
		public NKCUIComResourceButton m_comResourceButton;

		// Token: 0x0400664D RID: 26189
		public float m_scrollTime;

		// Token: 0x0400664E RID: 26190
		public GameObject m_objEnterLimitRoot;

		// Token: 0x0400664F RID: 26191
		[Header("��ŵ")]
		public GameObject m_objSkip;

		// Token: 0x04006650 RID: 26192
		public NKCUIOperationSkip m_operationSkip;

		// Token: 0x04006651 RID: 26193
		public NKCUIComToggle m_tglSkip;

		// Token: 0x04006652 RID: 26194
		private int m_trimId;

		// Token: 0x04006653 RID: 26195
		private int m_clearedLevel;

		// Token: 0x04006654 RID: 26196
		private int m_selectedGroup = 101;

		// Token: 0x04006655 RID: 26197
		private int m_selectedLevel;

		// Token: 0x04006656 RID: 26198
		private int m_maxTrimLevel;

		// Token: 0x04006657 RID: 26199
		private int m_skipCount;

		// Token: 0x04006658 RID: 26200
		private float m_dateUpdateTimerSec;

		// Token: 0x04006659 RID: 26201
		private float m_dateUpdateTimerMin;

		// Token: 0x0400665A RID: 26202
		private bool m_isSkip;

		// Token: 0x0400665B RID: 26203
		private bool m_bShowIntervalTime;
	}
}
