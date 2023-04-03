using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Game;
using DG.Tweening;
using NKC.UI.Result;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A73 RID: 2675
	public class NKCPopupOpSkipProcess : NKCUIBase
	{
		// Token: 0x170013AE RID: 5038
		// (get) Token: 0x0600761B RID: 30235 RVA: 0x00274378 File Offset: 0x00272578
		public static NKCPopupOpSkipProcess Instance
		{
			get
			{
				if (NKCPopupOpSkipProcess.m_Instance == null)
				{
					NKCPopupOpSkipProcess.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupOpSkipProcess>("AB_UI_NKM_UI_OPERATION", "NKM_UI_POPUP_OPERATION SKIP", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupOpSkipProcess.CleanupInstance)).GetInstance<NKCPopupOpSkipProcess>();
					NKCPopupOpSkipProcess instance = NKCPopupOpSkipProcess.m_Instance;
					if (instance != null)
					{
						instance.Init();
					}
				}
				return NKCPopupOpSkipProcess.m_Instance;
			}
		}

		// Token: 0x170013AF RID: 5039
		// (get) Token: 0x0600761C RID: 30236 RVA: 0x002743CD File Offset: 0x002725CD
		public static bool HasInstance
		{
			get
			{
				return NKCPopupOpSkipProcess.m_Instance != null;
			}
		}

		// Token: 0x170013B0 RID: 5040
		// (get) Token: 0x0600761D RID: 30237 RVA: 0x002743DA File Offset: 0x002725DA
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupOpSkipProcess.m_Instance != null && NKCPopupOpSkipProcess.m_Instance.IsOpen;
			}
		}

		// Token: 0x0600761E RID: 30238 RVA: 0x002743F5 File Offset: 0x002725F5
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupOpSkipProcess.m_Instance != null && NKCPopupOpSkipProcess.m_Instance.IsOpen)
			{
				NKCPopupOpSkipProcess.m_Instance.Close();
			}
		}

		// Token: 0x0600761F RID: 30239 RVA: 0x0027441A File Offset: 0x0027261A
		private static void CleanupInstance()
		{
			NKCPopupOpSkipProcess.m_Instance = null;
		}

		// Token: 0x170013B1 RID: 5041
		// (get) Token: 0x06007620 RID: 30240 RVA: 0x00274422 File Offset: 0x00272622
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170013B2 RID: 5042
		// (get) Token: 0x06007621 RID: 30241 RVA: 0x00274425 File Offset: 0x00272625
		public override string MenuName
		{
			get
			{
				return "Operaion Skip";
			}
		}

		// Token: 0x06007622 RID: 30242 RVA: 0x0027442C File Offset: 0x0027262C
		public void Init()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			NKCUtil.SetButtonClickDelegate(this.m_csbtnConfirm, new UnityAction(this.OnClickConfirm));
			NKCUtil.SetGameobjectActive(this.m_imgBgDummy.gameObject, false);
			NKCUtil.SetHotkey(this.m_csbtnConfirm, HotkeyEventType.Confirm, null, false);
			this.m_loopScrollRect.dOnGetObject += this.GetRewardSlot;
			this.m_loopScrollRect.dOnReturnObject += this.ReturnRewardSlot;
			this.m_loopScrollRect.dOnProvideData += this.ProvideRewardData;
			this.m_loopScrollRect.dOnRepopulate += this.RepopulateScrollRect;
			this.m_loopScrollRect.ContentConstraintCount = this.m_gridLayoutGroup.constraintCount;
			NKCUtil.SetScrollHotKey(this.m_loopScrollRect, null);
			if (this.m_eventTriggerBg != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(delegate(BaseEventData eventData)
				{
					this.OnClickConfirm();
				});
				this.m_eventTriggerBg.triggers.Add(entry);
			}
			if (this.m_eventTriggerRewardUI != null)
			{
				EventTrigger.Entry entry2 = new EventTrigger.Entry();
				entry2.eventID = EventTriggerType.PointerClick;
				entry2.callback.AddListener(delegate(BaseEventData eventData)
				{
					this.OnClickRewardUI();
				});
				this.m_eventTriggerRewardUI.triggers.Add(entry2);
			}
			if (this.m_eventTriggerPanel != null)
			{
				EventTrigger.Entry entry3 = new EventTrigger.Entry();
				entry3.eventID = EventTriggerType.PointerClick;
				entry3.callback.AddListener(delegate(BaseEventData eventData)
				{
					this.OnClickRewardUI();
				});
				this.m_eventTriggerPanel.triggers.Add(entry3);
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007623 RID: 30243 RVA: 0x002745D4 File Offset: 0x002727D4
		public override void CloseInternal()
		{
			this.m_lstResultDatas.Clear();
			this.m_dicRewardSlotData.Clear();
			this.m_lstRewardSortedSlotData.Clear();
			this.m_hsShakeSlotID.Clear();
			int count = this.m_lstTweenSlot.Count;
			for (int i = 0; i < count; i++)
			{
				this.m_lstTweenSlot[i].Kill(false);
			}
			this.m_lstTweenSlot.Clear();
			base.gameObject.SetActive(false);
			if (NKCContentManager.CheckLevelChanged())
			{
				NKCPopupUserLevelUp.instance.Open(NKCScenManager.CurrentUserData(), delegate()
				{
					NKCContentManager.SetLevelChanged(false);
					NKCUIManager.OnBackButton();
				});
				return;
			}
			NKCUIManager.OnBackButton();
		}

		// Token: 0x06007624 RID: 30244 RVA: 0x00274689 File Offset: 0x00272889
		public override void OnBackButton()
		{
		}

		// Token: 0x06007625 RID: 30245 RVA: 0x0027468B File Offset: 0x0027288B
		public void Open(List<NKMDungeonRewardSet> rewardSetList, List<UnitLoyaltyUpdateData> lstUnitLoyaltyData, string title = null)
		{
			if (rewardSetList == null)
			{
				return;
			}
			this.SetRewardData(rewardSetList, lstUnitLoyaltyData);
			base.gameObject.SetActive(true);
			this.OpenProcessCommon(rewardSetList.Count, title);
			NKCUIOpenAnimator nkcuiopenAnimator = this.m_NKCUIOpenAnimator;
			if (nkcuiopenAnimator != null)
			{
				nkcuiopenAnimator.PlayOpenAni();
			}
			base.UIOpened(true);
		}

		// Token: 0x06007626 RID: 30246 RVA: 0x002746CA File Offset: 0x002728CA
		public void Open(List<NKMRewardData> lstRewardData, List<UnitLoyaltyUpdateData> lstUnitLoyaltyData, string title = null)
		{
			if (lstRewardData == null)
			{
				return;
			}
			this.SetRewardData(lstRewardData, lstUnitLoyaltyData);
			base.gameObject.SetActive(true);
			this.OpenProcessCommon(lstRewardData.Count, title);
			NKCUIOpenAnimator nkcuiopenAnimator = this.m_NKCUIOpenAnimator;
			if (nkcuiopenAnimator != null)
			{
				nkcuiopenAnimator.PlayOpenAni();
			}
			base.UIOpened(true);
		}

		// Token: 0x06007627 RID: 30247 RVA: 0x0027470C File Offset: 0x0027290C
		private void OpenProcessCommon(int rewardResultCount, string title)
		{
			this.m_fTimer = this.m_fIdleTime;
			this.m_fPressSkipTimer = 0f;
			this.m_iFightCount = 0;
			this.m_iFullCount = rewardResultCount;
			this.m_eAniState = NKCPopupOpSkipProcess.AniState.BASE;
			this.m_aniShip.Play(this.m_baseAniName);
			this.m_fPrevXoffset = 0f;
			this.m_iAniRepeat = 0;
			this.m_bOffsetCalc = false;
			this.m_bUpdateCount = true;
			this.RepopulateScrollRect();
			this.m_loopScrollRect.PrepareCells(0);
			this.m_loopScrollRect.TotalCount = 0;
			this.m_loopScrollRect.StopMovement();
			this.m_loopScrollRect.SetIndexPosition(0);
			NKCUtil.SetGameobjectActive(this.m_objCount, true);
			this.SetCountText(this.m_iFightCount, this.m_iFullCount);
			if (string.IsNullOrEmpty(title))
			{
				NKCUtil.SetLabelText(this.m_lbTitle, NKCStringTable.GetString("SI_PF_OPERATION_SKIP_RESULT", false));
				return;
			}
			NKCUtil.SetLabelText(this.m_lbTitle, title);
		}

		// Token: 0x06007628 RID: 30248 RVA: 0x002747F4 File Offset: 0x002729F4
		private void Update()
		{
			if (!base.IsOpen)
			{
				return;
			}
			NKCUIOpenAnimator nkcuiopenAnimator = this.m_NKCUIOpenAnimator;
			if (nkcuiopenAnimator != null)
			{
				nkcuiopenAnimator.Update();
			}
			if (Input.GetKey(KeyCode.LeftControl))
			{
				this.m_fPressSkipTimer += Time.deltaTime;
				if (this.m_fPressSkipTimer >= this.PressSkipTime)
				{
					this.OnClickConfirm();
					this.m_fPressSkipTimer = 0f;
				}
			}
			else
			{
				this.m_fPressSkipTimer = 0f;
			}
			if (this.m_eAniState == NKCPopupOpSkipProcess.AniState.STOP)
			{
				return;
			}
			switch (this.m_eAniState)
			{
			case NKCPopupOpSkipProcess.AniState.BASE:
				this.AnimateBgUV(this.m_fBgScrollSpeed * Time.deltaTime);
				if (this.m_fTimer <= 0f)
				{
					if (this.m_iFullCount > 1)
					{
						this.m_eAniState = NKCPopupOpSkipProcess.AniState.FIGHT;
						this.m_aniShip.Play(this.m_fightAniName);
					}
					else
					{
						this.m_eAniState = NKCPopupOpSkipProcess.AniState.END;
						this.m_aniShip.Play(this.m_endAniName);
					}
					this.m_iAniRepeat = 0;
					return;
				}
				this.m_fTimer -= Time.deltaTime;
				return;
			case NKCPopupOpSkipProcess.AniState.FIGHT:
			{
				int num = (int)this.m_aniShip.GetCurrentAnimatorStateInfo(0).normalizedTime;
				if (this.m_objExplosion.activeInHierarchy)
				{
					if (this.m_bUpdateCount)
					{
						if (this.m_bgTweener != null)
						{
							DOTween.Kill(this.m_bgTweener, false);
						}
						this.m_bgTweener = DOTween.Shake(() => this.m_imgBackground.uvRect.position, delegate(Vector3 v)
						{
							Vector2 position = this.m_imgBackground.uvRect.position;
							Vector2 size = this.m_imgBackground.uvRect.size;
							this.m_imgBackground.uvRect = new Rect(position.x, v.y, size.x, size.y);
						}, this.m_fShakeDuration, this.m_fShakeStrength, this.m_iShakeVibration, 90f, true, true, ShakeRandomnessMode.Full).target;
						this.RefreshRewardData(this.m_iFightCount, true);
						int num2 = this.m_iFightCount + 1;
						this.m_iFightCount = num2;
						this.SetCountText(num2, this.m_iFullCount);
						this.m_bUpdateCount = false;
					}
				}
				else
				{
					this.m_bUpdateCount = true;
				}
				if (this.m_iAniRepeat != num)
				{
					this.m_iAniRepeat = num;
					if (this.m_iFightCount >= this.m_iFullCount - 1)
					{
						this.m_eAniState = NKCPopupOpSkipProcess.AniState.END;
						this.m_aniShip.Play(this.m_endAniName);
						this.m_iAniRepeat = 0;
						this.m_bOffsetCalc = false;
						this.m_bUpdateCount = true;
						return;
					}
				}
				this.AnimateBgUV(this.m_fBgScrollSpeed * Time.deltaTime);
				return;
			}
			case NKCPopupOpSkipProcess.AniState.END:
				if (this.m_bOffsetCalc)
				{
					this.m_fPrevXoffset = this.m_imgBgDummy.uvRect.position.x - this.m_vBgDummyUV.x;
				}
				else
				{
					this.m_bOffsetCalc = true;
				}
				this.m_vBgDummyUV = this.m_imgBgDummy.uvRect.position;
				this.AnimateBgUV(this.m_fPrevXoffset);
				if (this.m_objComplete.activeInHierarchy && this.m_bUpdateCount)
				{
					this.RefreshRewardData(this.m_iFightCount, true);
					int num2 = this.m_iFightCount + 1;
					this.m_iFightCount = num2;
					this.SetCountText(num2, this.m_iFullCount);
					this.m_bUpdateCount = false;
				}
				if (this.m_aniShip.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
				{
					this.m_eAniState = NKCPopupOpSkipProcess.AniState.STOP;
				}
				return;
			default:
				return;
			}
		}

		// Token: 0x06007629 RID: 30249 RVA: 0x00274AEC File Offset: 0x00272CEC
		private void SetRewardData(List<NKMDungeonRewardSet> rewardSetList, List<UnitLoyaltyUpdateData> lstUnitLoyaltyData)
		{
			this.m_lstResultDatas.Clear();
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			int count = rewardSetList.Count;
			for (int i = 0; i < count; i++)
			{
				NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(rewardSetList[i].dungeonClearData.dungeonId);
				int stageID = 0;
				if (dungeonTempletBase.StageTemplet != null)
				{
					stageID = dungeonTempletBase.StageTemplet.Key;
				}
				NKCUIResult.BattleResultData item = NKCUIResult.MakeMissionResultData(myUserData.m_ArmyData, rewardSetList[i].dungeonClearData.dungeonId, stageID, true, rewardSetList[i].dungeonClearData, NKMDeckIndex.None, null, lstUnitLoyaltyData, 1);
				this.m_lstResultDatas.Add(item);
			}
		}

		// Token: 0x0600762A RID: 30250 RVA: 0x00274B98 File Offset: 0x00272D98
		private void SetRewardData(List<NKMRewardData> lstRewardData, List<UnitLoyaltyUpdateData> lstUnitLoyaltyData)
		{
			this.m_lstResultDatas.Clear();
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			int count = lstRewardData.Count;
			for (int i = 0; i < count; i++)
			{
				NKCUIResult.BattleResultData battleResultData = new NKCUIResult.BattleResultData();
				battleResultData.m_lstUnitLevelupData = NKCUIResult.MakeUnitLevelupExpData(myUserData.m_ArmyData, lstRewardData[i].UnitExpDataList, NKMDeckIndex.None, lstUnitLoyaltyData);
				battleResultData.m_RewardData = lstRewardData[i];
				this.m_lstResultDatas.Add(battleResultData);
			}
		}

		// Token: 0x0600762B RID: 30251 RVA: 0x00274C14 File Offset: 0x00272E14
		private void SetCountText(int currentCount, int fullCount)
		{
			if (currentCount == fullCount)
			{
				NKCUtil.SetGameobjectActive(this.m_objCount, false);
				return;
			}
			NKCUtil.SetLabelText(this.m_lbCount, string.Format(NKCStringTable.GetString("SI_DP_OPERATION_SKIP_COUNT", false), currentCount, fullCount));
		}

		// Token: 0x0600762C RID: 30252 RVA: 0x00274C50 File Offset: 0x00272E50
		private void AnimateBgUV(float xOffset)
		{
			Vector2 position = this.m_imgBackground.uvRect.position;
			Vector2 size = this.m_imgBackground.uvRect.size;
			float x = Mathf.Repeat(position.x + xOffset, 1f);
			this.m_imgBackground.uvRect = new Rect(x, position.y, size.x, size.y);
		}

		// Token: 0x0600762D RID: 30253 RVA: 0x00274CBC File Offset: 0x00272EBC
		private void SetShipImage()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null || myUserData.m_ArmyData == null || !NKCUIDeckViewer.IsInstanceOpen)
			{
				return;
			}
			NKMDeckIndex selectDeckIndex = NKCUIDeckViewer.Instance.GetSelectDeckIndex();
			NKMDeckData deckData = myUserData.m_ArmyData.GetDeckData(selectDeckIndex);
			if (deckData == null)
			{
				return;
			}
			NKMUnitData shipFromUID = myUserData.m_ArmyData.GetShipFromUID(deckData.m_ShipUID);
			if (shipFromUID == null)
			{
				return;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(shipFromUID.m_UnitID);
			Sprite sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, unitTempletBase);
			if (sprite == null)
			{
				return;
			}
			NKCUtil.SetImageSprite(this.m_imgShip, sprite, false);
		}

		// Token: 0x0600762E RID: 30254 RVA: 0x00274D4C File Offset: 0x00272F4C
		private void AddToRewardSlotDataList(NKCUISlot.SlotData slotData)
		{
			if (slotData.ID <= 0)
			{
				return;
			}
			if (!this.m_dicRewardSlotData.ContainsKey(slotData.ID))
			{
				this.m_dicRewardSlotData.Add(slotData.ID, new List<NKCUISlot.SlotData>());
			}
			NKCUISlot.eSlotMode eType = slotData.eType;
			if (eType != NKCUISlot.eSlotMode.ItemMisc)
			{
				if (eType != NKCUISlot.eSlotMode.Mold)
				{
					if (eType != NKCUISlot.eSlotMode.UnitCount)
					{
						this.m_dicRewardSlotData[slotData.ID].Add(slotData);
						return;
					}
				}
				else
				{
					NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(slotData.ID);
					if (itemMoldTempletByID == null || itemMoldTempletByID.m_bPermanent)
					{
						this.m_dicRewardSlotData[slotData.ID].Add(slotData);
						return;
					}
					if (this.m_dicRewardSlotData[slotData.ID].Count > 0)
					{
						this.m_dicRewardSlotData[slotData.ID][0].Count += slotData.Count;
						this.m_hsShakeSlotID.Add(slotData.ID);
						return;
					}
					this.m_dicRewardSlotData[slotData.ID].Add(slotData);
					return;
				}
			}
			if (this.m_dicRewardSlotData[slotData.ID].Count > 0)
			{
				this.m_dicRewardSlotData[slotData.ID][0].Count += slotData.Count;
				this.m_hsShakeSlotID.Add(slotData.ID);
				return;
			}
			this.m_dicRewardSlotData[slotData.ID].Add(slotData);
		}

		// Token: 0x0600762F RID: 30255 RVA: 0x00274EC4 File Offset: 0x002730C4
		private RectTransform GetRewardSlot(int index)
		{
			RectTransform result = null;
			NKCUISlot newInstance = NKCUISlot.GetNewInstance(null);
			if (newInstance != null)
			{
				newInstance.Init();
				result = newInstance.GetComponent<RectTransform>();
			}
			return result;
		}

		// Token: 0x06007630 RID: 30256 RVA: 0x00274EF4 File Offset: 0x002730F4
		private void ReturnRewardSlot(Transform tr)
		{
			NKCUISlot component = tr.GetComponent<NKCUISlot>();
			tr.SetParent(null);
			if (component != null)
			{
				component.CleanUp();
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x06007631 RID: 30257 RVA: 0x00274F2C File Offset: 0x0027312C
		private void ProvideRewardData(Transform tr, int index)
		{
			NKCUISlot component = tr.GetComponent<NKCUISlot>();
			if (component == null)
			{
				return;
			}
			if (index >= this.m_lstRewardSortedSlotData.Count)
			{
				NKCUtil.SetGameobjectActive(component, false);
				return;
			}
			NKCUtil.SetGameobjectActive(component, true);
			if (this.m_lstRewardSortedSlotData[index].eType == NKCUISlot.eSlotMode.ItemMisc)
			{
				component.SetData(this.m_lstRewardSortedSlotData[index], true, new NKCUISlot.OnClick(this.OnClickMiscItemIcon));
				return;
			}
			component.SetData(this.m_lstRewardSortedSlotData[index], true, null);
		}

		// Token: 0x06007632 RID: 30258 RVA: 0x00274FAF File Offset: 0x002731AF
		private void RepopulateScrollRect()
		{
			NKCUtil.CalculateContentRectSize(this.m_loopScrollRect, this.m_gridLayoutGroup, this.m_iColumnMinCount, this.m_gridLayoutGroup.cellSize, this.m_gridLayoutGroup.spacing, false);
		}

		// Token: 0x06007633 RID: 30259 RVA: 0x00274FE0 File Offset: 0x002731E0
		private void OnClickMiscItemIcon(NKCUISlot.SlotData slotData, bool bLocked)
		{
			NKCPopupItemBox.Instance.Open(NKCPopupItemBox.eMode.Normal, slotData, null, false, false, false);
		}

		// Token: 0x06007634 RID: 30260 RVA: 0x00274FF4 File Offset: 0x002731F4
		private void RefreshRewardData(int resultDataIndex, bool refreshScrollRect = true)
		{
			if (resultDataIndex == 0)
			{
				this.m_dicRewardSlotData.Clear();
			}
			this.m_hsShakeSlotID.Clear();
			this.m_lstTweenSlot.Clear();
			if (resultDataIndex >= this.m_lstResultDatas.Count)
			{
				return;
			}
			NKCUIResult.BattleResultData battleResultData = this.m_lstResultDatas[resultDataIndex];
			if (battleResultData.m_lstUnitLevelupData != null)
			{
				int count = battleResultData.m_lstUnitLevelupData.Count;
				for (int i = 0; i < count; i++)
				{
					int iTotalExpGain = battleResultData.m_lstUnitLevelupData[i].m_iTotalExpGain;
					if (iTotalExpGain > 0)
					{
						NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeMiscItemData(502, (long)iTotalExpGain, battleResultData.m_iUnitExpBonusRate);
						this.AddToRewardSlotDataList(slotData);
						break;
					}
				}
			}
			if (battleResultData.m_firstRewardData != null)
			{
				NKCUISlot.MakeSlotDataListFromReward(battleResultData.m_firstRewardData, false, false).ForEach(new Action<NKCUISlot.SlotData>(this.AddToRewardSlotDataList));
			}
			if (battleResultData.m_firstAllClearData != null)
			{
				NKCUISlot.MakeSlotDataListFromReward(battleResultData.m_firstAllClearData, false, false).ForEach(new Action<NKCUISlot.SlotData>(this.AddToRewardSlotDataList));
			}
			if (battleResultData.m_OnetimeRewardData != null)
			{
				NKCUISlot.MakeSlotDataListFromReward(battleResultData.m_OnetimeRewardData, false, false).ForEach(new Action<NKCUISlot.SlotData>(this.AddToRewardSlotDataList));
			}
			if (battleResultData.m_RewardData != null)
			{
				NKCUISlot.MakeSlotDataListFromReward(battleResultData.m_RewardData, false, false).ForEach(new Action<NKCUISlot.SlotData>(this.AddToRewardSlotDataList));
			}
			if (battleResultData.m_additionalReward != null)
			{
				NKCUISlot.MakeSlotDataListFromReward(battleResultData.m_additionalReward).ForEach(new Action<NKCUISlot.SlotData>(this.AddToRewardSlotDataList));
			}
			if (!refreshScrollRect)
			{
				return;
			}
			this.m_lstRewardSortedSlotData.Clear();
			foreach (KeyValuePair<int, List<NKCUISlot.SlotData>> keyValuePair in this.m_dicRewardSlotData)
			{
				List<NKCUISlot.SlotData> value = keyValuePair.Value;
				int count2 = value.Count;
				for (int j = 0; j < count2; j++)
				{
					this.m_lstRewardSortedSlotData.Add(value[j]);
				}
			}
			this.m_lstRewardSortedSlotData.Sort(delegate(NKCUISlot.SlotData e1, NKCUISlot.SlotData e2)
			{
				if (e1.eType > e2.eType)
				{
					return 1;
				}
				if (e1.eType < e2.eType)
				{
					return -1;
				}
				return 0;
			});
			this.m_loopScrollRect.TotalCount = this.m_lstRewardSortedSlotData.Count;
			this.m_loopScrollRect.StopMovement();
			this.m_loopScrollRect.RefreshCells(false);
			int childCount = this.m_loopScrollRect.content.childCount;
			for (int k = 0; k < childCount; k++)
			{
				Transform child = this.m_loopScrollRect.content.GetChild(k);
				if (child.gameObject.activeSelf)
				{
					NKCUISlot component = child.GetComponent<NKCUISlot>();
					if (!(component == null))
					{
						int id = component.GetSlotData().ID;
						if (this.m_hsShakeSlotID.Contains(id))
						{
							component.transform.DOKill(true);
							this.m_lstTweenSlot.Add(component.transform.DOShakePosition(this.m_fShakeItemDuration, new Vector3(this.m_fShakeItemStrength, this.m_fShakeItemStrength, 0f), this.m_iShakeItemVibration, 90f, false, true, ShakeRandomnessMode.Full));
							this.m_lstTweenSlot.Add(component.transform.DOPunchScale(new Vector3(this.m_fPunchItemScale, this.m_fPunchItemScale, 0f), this.m_fPunchItemDuration, 10, 1f));
						}
					}
				}
			}
			this.m_hsShakeSlotID.Clear();
		}

		// Token: 0x06007635 RID: 30261 RVA: 0x00275348 File Offset: 0x00273548
		private void OnClickConfirm()
		{
			if (this.m_iFightCount < this.m_iFullCount)
			{
				this.m_eAniState = NKCPopupOpSkipProcess.AniState.STOP;
				this.m_aniShip.Play(this.m_endAniName, -1, 1f);
				for (int i = this.m_iFightCount; i < this.m_iFullCount; i++)
				{
					this.RefreshRewardData(i, i == this.m_iFullCount - 1);
				}
				this.m_iFightCount = this.m_iFullCount;
				this.SetCountText(this.m_iFullCount, this.m_iFullCount);
				int count = this.m_lstTweenSlot.Count;
				for (int j = 0; j < count; j++)
				{
					if (this.m_lstTweenSlot[j] != null && this.m_lstTweenSlot[j].IsPlaying())
					{
						this.m_lstTweenSlot[j].Complete();
					}
				}
				return;
			}
			bool flag = true;
			int count2 = this.m_lstTweenSlot.Count;
			for (int k = 0; k < count2; k++)
			{
				if (this.m_lstTweenSlot[k] != null && this.m_lstTweenSlot[k].IsPlaying())
				{
					this.m_lstTweenSlot[k].Complete();
					flag = false;
				}
			}
			if (!flag)
			{
				return;
			}
			base.Close();
		}

		// Token: 0x06007636 RID: 30262 RVA: 0x0027547C File Offset: 0x0027367C
		private void OnClickRewardUI()
		{
			if (this.m_eAniState == NKCPopupOpSkipProcess.AniState.FIGHT)
			{
				int num = (int)this.m_aniShip.GetCurrentAnimatorStateInfo(0).normalizedTime;
				if (this.m_aniShip.GetCurrentAnimatorStateInfo(0).normalizedTime - (float)num < this.m_fStepSkipAniTime)
				{
					this.m_aniShip.Play(this.m_fightAniName, 0, (float)num + this.m_fStepSkipAniTime);
				}
			}
		}

		// Token: 0x06007637 RID: 30263 RVA: 0x002754E4 File Offset: 0x002736E4
		private void OnDestroy()
		{
			this.m_NKCUIOpenAnimator = null;
			List<NKCUIResult.BattleResultData> lstResultDatas = this.m_lstResultDatas;
			if (lstResultDatas != null)
			{
				lstResultDatas.Clear();
			}
			this.m_lstResultDatas = null;
			Dictionary<int, List<NKCUISlot.SlotData>> dicRewardSlotData = this.m_dicRewardSlotData;
			if (dicRewardSlotData != null)
			{
				dicRewardSlotData.Clear();
			}
			this.m_dicRewardSlotData = null;
			List<NKCUISlot.SlotData> lstRewardSortedSlotData = this.m_lstRewardSortedSlotData;
			if (lstRewardSortedSlotData != null)
			{
				lstRewardSortedSlotData.Clear();
			}
			this.m_lstRewardSortedSlotData = null;
			HashSet<int> hsShakeSlotID = this.m_hsShakeSlotID;
			if (hsShakeSlotID != null)
			{
				hsShakeSlotID.Clear();
			}
			this.m_hsShakeSlotID = null;
			List<Tween> lstTweenSlot = this.m_lstTweenSlot;
			if (lstTweenSlot != null)
			{
				lstTweenSlot.Clear();
			}
			this.m_lstTweenSlot = null;
		}

		// Token: 0x0400627F RID: 25215
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_OPERATION";

		// Token: 0x04006280 RID: 25216
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_OPERATION SKIP";

		// Token: 0x04006281 RID: 25217
		private static NKCPopupOpSkipProcess m_Instance;

		// Token: 0x04006282 RID: 25218
		public LoopScrollRect m_loopScrollRect;

		// Token: 0x04006283 RID: 25219
		public GridLayoutGroup m_gridLayoutGroup;

		// Token: 0x04006284 RID: 25220
		public GameObject m_objCount;

		// Token: 0x04006285 RID: 25221
		public Text m_lbCount;

		// Token: 0x04006286 RID: 25222
		public Text m_lbTitle;

		// Token: 0x04006287 RID: 25223
		public Animator m_aniShip;

		// Token: 0x04006288 RID: 25224
		public GameObject m_objExplosion;

		// Token: 0x04006289 RID: 25225
		public GameObject m_objComplete;

		// Token: 0x0400628A RID: 25226
		public RawImage m_imgBackground;

		// Token: 0x0400628B RID: 25227
		public RawImage m_imgBgDummy;

		// Token: 0x0400628C RID: 25228
		public Image m_imgShip;

		// Token: 0x0400628D RID: 25229
		public EventTrigger m_eventTriggerBg;

		// Token: 0x0400628E RID: 25230
		public EventTrigger m_eventTriggerRewardUI;

		// Token: 0x0400628F RID: 25231
		public EventTrigger m_eventTriggerPanel;

		// Token: 0x04006290 RID: 25232
		public NKCUIComStateButton m_csbtnConfirm;

		// Token: 0x04006291 RID: 25233
		public float m_fIdleTime;

		// Token: 0x04006292 RID: 25234
		public float m_fBgScrollSpeed;

		// Token: 0x04006293 RID: 25235
		public int m_iColumnMinCount;

		// Token: 0x04006294 RID: 25236
		[Header("애니메이션 이름")]
		public string m_baseAniName;

		// Token: 0x04006295 RID: 25237
		public string m_fightAniName;

		// Token: 0x04006296 RID: 25238
		public string m_endAniName;

		// Token: 0x04006297 RID: 25239
		[Header("배경 셰이크 효과")]
		public float m_fShakeDuration;

		// Token: 0x04006298 RID: 25240
		public float m_fShakeStrength;

		// Token: 0x04006299 RID: 25241
		public int m_iShakeVibration;

		// Token: 0x0400629A RID: 25242
		[Header("아이템 슬롯 셰이크 효과")]
		public float m_fShakeItemDuration;

		// Token: 0x0400629B RID: 25243
		public float m_fShakeItemStrength;

		// Token: 0x0400629C RID: 25244
		public int m_iShakeItemVibration;

		// Token: 0x0400629D RID: 25245
		[Header("아이템 슬롯 펀칭 효과")]
		public float m_fPunchItemDuration;

		// Token: 0x0400629E RID: 25246
		public float m_fPunchItemScale;

		// Token: 0x0400629F RID: 25247
		[Header("아이템 슬롯 스케일 효과")]
		public float m_fScaleItemDuration;

		// Token: 0x040062A0 RID: 25248
		public float m_fScaleItemInterval;

		// Token: 0x040062A1 RID: 25249
		[Header("단계별 스킵 애니메이션 타임 기준")]
		public float m_fStepSkipAniTime;

		// Token: 0x040062A2 RID: 25250
		[Header("애니메이션 스킵 키입력 유지 시간")]
		public float PressSkipTime;

		// Token: 0x040062A3 RID: 25251
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x040062A4 RID: 25252
		private List<NKCUIResult.BattleResultData> m_lstResultDatas = new List<NKCUIResult.BattleResultData>();

		// Token: 0x040062A5 RID: 25253
		private Dictionary<int, List<NKCUISlot.SlotData>> m_dicRewardSlotData = new Dictionary<int, List<NKCUISlot.SlotData>>();

		// Token: 0x040062A6 RID: 25254
		private List<NKCUISlot.SlotData> m_lstRewardSortedSlotData = new List<NKCUISlot.SlotData>();

		// Token: 0x040062A7 RID: 25255
		private HashSet<int> m_hsShakeSlotID = new HashSet<int>();

		// Token: 0x040062A8 RID: 25256
		private List<Tween> m_lstTweenSlot = new List<Tween>();

		// Token: 0x040062A9 RID: 25257
		private NKCPopupOpSkipProcess.AniState m_eAniState;

		// Token: 0x040062AA RID: 25258
		private Vector2 m_vBgDummyUV;

		// Token: 0x040062AB RID: 25259
		private float m_fPrevXoffset;

		// Token: 0x040062AC RID: 25260
		private float m_fTimer;

		// Token: 0x040062AD RID: 25261
		private float m_fPressSkipTimer;

		// Token: 0x040062AE RID: 25262
		private int m_iAniRepeat;

		// Token: 0x040062AF RID: 25263
		private int m_iFightCount;

		// Token: 0x040062B0 RID: 25264
		private int m_iFullCount;

		// Token: 0x040062B1 RID: 25265
		private bool m_bOffsetCalc;

		// Token: 0x040062B2 RID: 25266
		private bool m_bUpdateCount;

		// Token: 0x040062B3 RID: 25267
		private object m_bgTweener;

		// Token: 0x020017D5 RID: 6101
		private enum AniState
		{
			// Token: 0x0400A797 RID: 42903
			BASE,
			// Token: 0x0400A798 RID: 42904
			FIGHT,
			// Token: 0x0400A799 RID: 42905
			END,
			// Token: 0x0400A79A RID: 42906
			STOP
		}
	}
}
