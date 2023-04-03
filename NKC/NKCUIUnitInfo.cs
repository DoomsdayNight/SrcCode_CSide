using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Item;
using ClientPacket.Unit;
using ClientPacket.User;
using ClientPacket.WorldMap;
using Cs.Math;
using NKC.Templet;
using NKC.UI.Component;
using NKC.UI.Shop;
using NKM;
using NKM.Templet;
using NKM.Templet.Recall;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009F2 RID: 2546
	public class NKCUIUnitInfo : NKCUIBase
	{
		// Token: 0x1700129C RID: 4764
		// (get) Token: 0x06006E02 RID: 28162 RVA: 0x00241894 File Offset: 0x0023FA94
		public static NKCUIUnitInfo Instance
		{
			get
			{
				if (NKCUIUnitInfo.m_Instance == null)
				{
					NKCUIUnitInfo.m_Instance = NKCUIManager.OpenNewInstance<NKCUIUnitInfo>("ab_ui_nkm_ui_unit_info", "NKM_UI_UNIT_INFO", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIUnitInfo.CleanupInstance)).GetInstance<NKCUIUnitInfo>();
					NKCUIUnitInfo.m_Instance.InitUI();
				}
				return NKCUIUnitInfo.m_Instance;
			}
		}

		// Token: 0x1700129D RID: 4765
		// (get) Token: 0x06006E03 RID: 28163 RVA: 0x002418E3 File Offset: 0x0023FAE3
		public static bool HasInstance
		{
			get
			{
				return NKCUIUnitInfo.m_Instance != null;
			}
		}

		// Token: 0x1700129E RID: 4766
		// (get) Token: 0x06006E04 RID: 28164 RVA: 0x002418F0 File Offset: 0x0023FAF0
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIUnitInfo.m_Instance != null && NKCUIUnitInfo.m_Instance.IsOpen;
			}
		}

		// Token: 0x06006E05 RID: 28165 RVA: 0x0024190C File Offset: 0x0023FB0C
		public static NKCUIUnitInfo OpenNewInstance()
		{
			NKCUIUnitInfo instance = NKCUIManager.OpenNewInstance<NKCUIUnitInfo>("ab_ui_nkm_ui_unit_info", "NKM_UI_UNIT_INFO", NKCUIManager.eUIBaseRect.UIFrontCommon, null).GetInstance<NKCUIUnitInfo>();
			if (instance != null)
			{
				instance.InitUI();
			}
			return instance;
		}

		// Token: 0x06006E06 RID: 28166 RVA: 0x00241940 File Offset: 0x0023FB40
		public static void CheckInstanceAndClose()
		{
			if (NKCUIUnitInfo.m_Instance != null && NKCUIUnitInfo.m_Instance.IsOpen)
			{
				NKCUIUnitInfo.m_Instance.Close();
			}
		}

		// Token: 0x06006E07 RID: 28167 RVA: 0x00241965 File Offset: 0x0023FB65
		private static void CleanupInstance()
		{
			NKCUIUnitInfo.m_Instance = null;
		}

		// Token: 0x1700129F RID: 4767
		// (get) Token: 0x06006E08 RID: 28168 RVA: 0x00241970 File Offset: 0x0023FB70
		public override string MenuName
		{
			get
			{
				switch (this.m_curUIState)
				{
				case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.NEGOTIATION:
					return NKCUtilString.GET_STRING_NEGOTIATE;
				case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.LIMIT_BREAK:
					return NKCStringTable.GetString("SI_DP_LAB_MENU_NAME_LDS_UNIT_LIMITBREAK", false);
				case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.SKILL_TRAIN:
					return NKCStringTable.GetString("SI_DP_LAB_MENU_NAME_LDS_UNIT_SKILL_TRAIN", false);
				default:
					return NKCUtilString.GET_STRING_UNIT_INFO;
				}
			}
		}

		// Token: 0x170012A0 RID: 4768
		// (get) Token: 0x06006E09 RID: 28169 RVA: 0x002419BD File Offset: 0x0023FBBD
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				if (this.m_curUIState == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.SKILL_TRAIN)
				{
					return new List<int>
					{
						3,
						1,
						2,
						101
					};
				}
				return base.UpsideMenuShowResourceList;
			}
		}

		// Token: 0x170012A1 RID: 4769
		// (get) Token: 0x06006E0A RID: 28170 RVA: 0x002419F1 File Offset: 0x0023FBF1
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170012A2 RID: 4770
		// (get) Token: 0x06006E0B RID: 28171 RVA: 0x002419F4 File Offset: 0x0023FBF4
		public override string GuideTempletID
		{
			get
			{
				switch (this.m_curUIState)
				{
				case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.NEGOTIATION:
					return "ARTICLE_UNIT_NEGOTIATION";
				case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.LIMIT_BREAK:
					return "ARTICLE_UNIT_LIMITBREAK";
				case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.SKILL_TRAIN:
					return "ARTICLE_UNIT_TRAINING";
				default:
					return "ARTICLE_UNIT_INFO";
				}
			}
		}

		// Token: 0x06006E0C RID: 28172 RVA: 0x00241A38 File Offset: 0x0023FC38
		public override void CloseInternal()
		{
			this.BannerCleanUp();
			this.m_lReserveUID = 0L;
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
			if (NKCUIInventory.IsInstanceLoaded)
			{
				NKCUIInventory.Instance.ClearCachingData();
			}
			NKCPopupUnitInfoDetail.CheckInstanceAndClose();
			NKCUIPopupIllustView.CheckInstanceAndClose();
			if (this.m_UIUnitSelectList != null && NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_UNIT_LIST)
			{
				this.UnitSelectList.Close();
			}
			this.m_UIUnitSelectList = null;
			this.m_NKCUIUnitInfoInfo.Clear();
			this.m_NKCUIUnitInfoLimitBreak.Clear();
			this.m_NKCUIUnitInfoSkillTrain.Clear();
			this.m_NKCUIUnitInfoNegotiation.Clear();
			this.m_curUIState = NKCUIUnitInfo.UNIT_INFO_TAB_STATE.NONE;
		}

		// Token: 0x06006E0D RID: 28173 RVA: 0x00241AE8 File Offset: 0x0023FCE8
		public override void OnBackButton()
		{
			base.OnBackButton();
		}

		// Token: 0x06006E0E RID: 28174 RVA: 0x00241AF0 File Offset: 0x0023FCF0
		public override void UnHide()
		{
			base.UnHide();
			if (this.m_curUIState == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.BASE)
			{
				this.m_NKCUIUnitInfoInfo.UnActiveEffect();
			}
			this.CheckTabLock();
			this.TutorialCheck();
		}

		// Token: 0x06006E0F RID: 28175 RVA: 0x00241B18 File Offset: 0x0023FD18
		public override void OnUnitUpdate(NKMUserData.eChangeNotifyType eEventType, NKM_UNIT_TYPE eUnitType, long uid, NKMUnitData unitData)
		{
			if (eEventType == NKMUserData.eChangeNotifyType.Update && uid == this.m_NKMUnitData.m_UnitUID)
			{
				this.m_NKMUnitData = unitData;
				this.UpdateUnitData();
				this.OnSkinEquip(uid, this.m_NKMUnitData.m_SkinID);
				this.m_NKCUICharInfoSummary.SetData(unitData);
				this.m_NKMUnitData = unitData;
				int num = this.m_UnitSortList.FindIndex((NKMUnitData v) => v.m_UnitUID == uid);
				if (num >= 0)
				{
					this.m_UnitSortList[num] = unitData;
				}
				if (this.m_DragCharacterView != null)
				{
					RectTransform currentItem = this.m_DragCharacterView.GetCurrentItem();
					if (currentItem != null)
					{
						NKCUICharacterView componentInChildren = currentItem.gameObject.GetComponentInChildren<NKCUICharacterView>();
						if (componentInChildren != null)
						{
							componentInChildren.SetCharacterIllust(unitData, false, true, true, 0);
						}
					}
				}
				switch (this.m_curUIState)
				{
				case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.NEGOTIATION:
					this.m_NKCUIUnitInfoNegotiation.OnUnitUpdate(uid, unitData);
					break;
				case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.LIMIT_BREAK:
					this.m_NKCUIUnitInfoLimitBreak.OnUnitUpdate(uid, unitData);
					break;
				case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.SKILL_TRAIN:
					this.m_NKCUIUnitInfoSkillTrain.OnUnitUpdate(uid, unitData);
					break;
				}
			}
			if (eEventType == NKMUserData.eChangeNotifyType.Remove)
			{
				int num2 = this.m_UnitSortList.FindIndex((NKMUnitData v) => v.m_UnitUID == uid);
				if (num2 >= 0)
				{
					this.m_UnitSortList.RemoveAt(num2);
					int index = this.m_UnitSortList.FindIndex((NKMUnitData v) => v.m_UnitUID == this.m_NKMUnitData.m_UnitUID);
					this.m_DragCharacterView.TotalCount = this.m_UnitSortList.Count;
					this.m_DragCharacterView.SetIndex(index);
				}
			}
		}

		// Token: 0x06006E10 RID: 28176 RVA: 0x00241CC8 File Offset: 0x0023FEC8
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			switch (this.m_curUIState)
			{
			case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.NEGOTIATION:
				this.m_NKCUIUnitInfoNegotiation.OnInventoryChange(itemData);
				return;
			case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.LIMIT_BREAK:
				this.m_NKCUIUnitInfoLimitBreak.OnInventoryChange(itemData);
				return;
			case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.SKILL_TRAIN:
				this.m_NKCUIUnitInfoSkillTrain.OnInventoryChange(itemData);
				return;
			default:
				return;
			}
		}

		// Token: 0x06006E11 RID: 28177 RVA: 0x00241D18 File Offset: 0x0023FF18
		public override void OnEquipChange(NKMUserData.eChangeNotifyType eType, long equipUID, NKMEquipItemData equipItem)
		{
			if (eType == NKMUserData.eChangeNotifyType.Update && equipItem.m_OwnerUnitUID == this.m_NKMUnitData.m_UnitUID && this.m_curUIState == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.BASE)
			{
				this.UpdateUnitData();
			}
			if ((eType == NKMUserData.eChangeNotifyType.Update || eType == NKMUserData.eChangeNotifyType.Remove) && this.EquipPresetOpened())
			{
				NKCUIEquipPreset equipPreset = this.m_NKCUIUnitInfoInfo.EquipPreset;
				if (equipPreset == null)
				{
					return;
				}
				equipPreset.UpdatePresetData(null, false, 0, true);
			}
		}

		// Token: 0x06006E12 RID: 28178 RVA: 0x00241D74 File Offset: 0x0023FF74
		public override void OnCompanyBuffUpdate(NKMUserData userData)
		{
			this.m_NKCUIUnitInfoNegotiation.OnCompanyBuffUpdate();
		}

		// Token: 0x06006E13 RID: 28179 RVA: 0x00241D84 File Offset: 0x0023FF84
		public void InitUI()
		{
			this.m_NKCUIUnitInfoInfo.Init();
			this.m_NKCUIUnitInfoLimitBreak.Init();
			this.m_NKCUIUnitInfoNegotiation.Init();
			this.m_NKCUIUnitInfoSkillTrain.Init();
			if (this.m_cbtnChangeIllust != null)
			{
				this.m_cbtnChangeIllust.PointerClick.RemoveAllListeners();
				this.m_cbtnChangeIllust.PointerClick.AddListener(new UnityAction(this.OnClickChangeIllust));
			}
			if (this.m_ctglLock != null)
			{
				this.m_ctglLock.OnValueChanged.RemoveAllListeners();
				this.m_ctglLock.OnValueChanged.AddListener(new UnityAction<bool>(this.OnLockToggle));
			}
			NKCUtil.SetToggleValueChangedDelegate(this.m_ctglFavorite, new UnityAction<bool>(this.OnFavoriteToggle));
			if (this.m_btnPractice != null)
			{
				this.m_btnPractice.PointerClick.RemoveAllListeners();
				this.m_btnPractice.PointerClick.AddListener(new UnityAction(this.OnUnitTestButton));
			}
			if (this.m_cbtnReview != null)
			{
				this.m_cbtnReview.PointerClick.RemoveAllListeners();
				this.m_cbtnReview.PointerClick.AddListener(new UnityAction(this.OnReviewButton));
			}
			NKCUtil.SetGameobjectActive(this.m_cbtnReview, NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.UNIT_REVIEW_SYSTEM));
			if (this.m_cbtnSkinMode != null)
			{
				this.m_cbtnSkinMode.PointerClick.RemoveAllListeners();
				this.m_cbtnSkinMode.PointerClick.AddListener(new UnityAction(this.OnSkinButton));
			}
			if (this.m_cbtnCollection != null)
			{
				this.m_cbtnCollection.PointerClick.RemoveAllListeners();
				this.m_cbtnCollection.PointerClick.AddListener(new UnityAction(this.OnCollectionButton));
			}
			if (this.m_cbtnVoicePopup != null)
			{
				this.m_cbtnVoicePopup.PointerClick.RemoveAllListeners();
				this.m_cbtnVoicePopup.PointerClick.AddListener(new UnityAction(this.OnVoiceButton));
			}
			if (this.m_DragCharacterView != null)
			{
				this.m_DragCharacterView.Init(true, true);
				this.m_DragCharacterView.dOnGetObject += this.MakeMainBannerListSlot;
				this.m_DragCharacterView.dOnReturnObject += new NKCUIComDragSelectablePanel.OnReturnObject(this.ReturnMainBannerListSlot);
				this.m_DragCharacterView.dOnProvideData += new NKCUIComDragSelectablePanel.OnProvideData(this.ProvideMainBannerListSlotData);
				this.m_DragCharacterView.dOnIndexChangeListener += this.SelectCharacter;
				this.m_DragCharacterView.dOnFocus += this.Focus;
			}
			if (this.m_evtPanel != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(new UnityAction<BaseEventData>(this.OnEventPanelClick));
				this.m_evtPanel.triggers.Add(entry);
			}
			NKCUtil.SetBindFunction(this.m_btnLoyalty, new UnityAction(this.OnTouchLoyalty));
			NKCUtil.SetBindFunction(this.m_ChangeBtn, new UnityAction(this.OnSelectUnit));
			if (null != this.m_tglInfo)
			{
				this.m_tglInfo.OnValueChanged.RemoveAllListeners();
				this.m_tglInfo.OnValueChanged.AddListener(delegate(bool bSet)
				{
					if (bSet)
					{
						this.ChangeState(NKCUIUnitInfo.UNIT_INFO_TAB_STATE.BASE);
					}
				});
			}
			if (null != this.m_tglNegotiation)
			{
				this.m_tglNegotiation.OnValueChanged.RemoveAllListeners();
				this.m_tglNegotiation.OnValueChanged.AddListener(delegate(bool bSet)
				{
					if (bSet)
					{
						this.ChangeState(NKCUIUnitInfo.UNIT_INFO_TAB_STATE.NEGOTIATION);
					}
				});
			}
			if (null != this.m_tglLimitBreak)
			{
				this.m_tglLimitBreak.OnValueChanged.RemoveAllListeners();
				this.m_tglLimitBreak.OnValueChanged.AddListener(delegate(bool bSet)
				{
					if (bSet)
					{
						this.ChangeState(NKCUIUnitInfo.UNIT_INFO_TAB_STATE.LIMIT_BREAK);
					}
				});
			}
			if (null != this.m_tglSkillTrain)
			{
				this.m_tglSkillTrain.OnValueChanged.RemoveAllListeners();
				this.m_tglSkillTrain.OnValueChanged.AddListener(delegate(bool bSet)
				{
					if (bSet)
					{
						this.ChangeState(NKCUIUnitInfo.UNIT_INFO_TAB_STATE.SKILL_TRAIN);
					}
				});
			}
			if (this.m_btnRecall != null)
			{
				this.m_btnRecall.PointerClick.RemoveAllListeners();
				this.m_btnRecall.PointerClick.AddListener(new UnityAction(this.OnClickRecall));
			}
			this.m_NKCUICharInfoSummary.SetUnitClassRootActive(false);
			this.m_NKCUICharInfoSummary.Init(true);
			base.gameObject.SetActive(false);
			this.m_lReserveUID = 0L;
		}

		// Token: 0x06006E14 RID: 28180 RVA: 0x002421CC File Offset: 0x002403CC
		public NKMUnitData GetNKMUnitData()
		{
			return this.m_NKMUnitData;
		}

		// Token: 0x06006E15 RID: 28181 RVA: 0x002421D4 File Offset: 0x002403D4
		public void Open(NKMUnitData cNKMUnitData, NKCUIUnitInfo.OnRemoveFromDeck onRemoveFromDeck, NKCUIUnitInfo.OpenOption openOption = null, NKC_SCEN_UNIT_LIST.eUIOpenReserve ReserveUI = NKC_SCEN_UNIT_LIST.eUIOpenReserve.Nothing)
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCPopupUnitInfoDetail.CheckInstanceAndClose();
			this.dOnRemoveFromDeck = onRemoveFromDeck;
			this.m_NKMUnitData = cNKMUnitData;
			this.UpdateUnitList(openOption, cNKMUnitData.m_UnitUID);
			this.m_bShowFierceInfo = openOption.m_bShowFierceInfo;
			switch (ReserveUI)
			{
			case NKC_SCEN_UNIT_LIST.eUIOpenReserve.UnitSkillTraining:
				this.m_tglSkillTrain.Select(true, true, true);
				this.ChangeState(NKCUIUnitInfo.UNIT_INFO_TAB_STATE.SKILL_TRAIN);
				break;
			case NKC_SCEN_UNIT_LIST.eUIOpenReserve.UnitLimitbreak:
				this.m_tglLimitBreak.Select(true, true, true);
				this.ChangeState(NKCUIUnitInfo.UNIT_INFO_TAB_STATE.LIMIT_BREAK);
				break;
			case NKC_SCEN_UNIT_LIST.eUIOpenReserve.UnitNegotiate:
				this.m_tglNegotiation.Select(true, true, true);
				this.ChangeState(NKCUIUnitInfo.UNIT_INFO_TAB_STATE.NEGOTIATION);
				break;
			default:
				this.m_tglInfo.Select(true, true, true);
				this.ChangeState(NKCUIUnitInfo.UNIT_INFO_TAB_STATE.BASE);
				break;
			}
			this.m_fDeltaTime = 0f;
			NKCUtil.SetGameobjectActive(this.m_objRecall, NKCRecallManager.IsRecallTargetUnit(this.m_NKMUnitData, NKCSynchronizedTime.GetServerUTCTime(0.0)));
			if (this.m_objRecall.activeSelf)
			{
				this.SetRecallRemainTime();
			}
			this.m_NKCUICharInfoSummary.SetData(this.m_NKMUnitData);
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			this.m_lReserveUID = this.m_NKMUnitData.m_UnitUID;
			this.CheckTabLock();
			base.UIOpened(true);
		}

		// Token: 0x06006E16 RID: 28182 RVA: 0x00242318 File Offset: 0x00240518
		private void CheckTabLock()
		{
			NKCUtil.SetGameobjectActive(this.m_objLimitBreakLock, !NKCContentManager.IsContentsUnlocked(ContentsType.LAB_LIMITBREAK, 0, 0));
			NKCUtil.SetGameobjectActive(this.m_objNegotiationLock, !NKCContentManager.IsContentsUnlocked(ContentsType.PERSONNAL_NEGO, 0, 0));
			NKCUtil.SetGameobjectActive(this.m_objSkillTrainLock, !NKCContentManager.IsContentsUnlocked(ContentsType.LAB_TRAINING, 0, 0));
		}

		// Token: 0x06006E17 RID: 28183 RVA: 0x0024236C File Offset: 0x0024056C
		private void UpdateUnitList(NKCUIUnitInfo.OpenOption openOption, long selectedUnitUID)
		{
			this.m_UnitSortList.Clear();
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				NKMArmyData armyData = nkmuserData.m_ArmyData;
				if (armyData != null)
				{
					foreach (long uid in openOption.m_UnitUIDList)
					{
						NKMUnitData unitOrTrophyFromUID = armyData.GetUnitOrTrophyFromUID(uid);
						if (unitOrTrophyFromUID != null)
						{
							this.m_UnitSortList.Add(unitOrTrophyFromUID);
						}
					}
					using (List<NKMUnitData>.Enumerator enumerator2 = openOption.m_lstUnitData.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							NKMUnitData unitData = enumerator2.Current;
							if (this.m_UnitSortList.Find((NKMUnitData x) => x.m_UnitUID == unitData.m_UnitUID) == null && armyData.GetUnitOrTrophyFromUID(unitData.m_UnitUID) != null)
							{
								this.m_UnitSortList.Add(unitData);
							}
						}
					}
					if (this.m_UnitSortList.Find((NKMUnitData x) => x.m_UnitUID == selectedUnitUID) == null)
					{
						this.m_UnitSortList.Add(armyData.GetUnitOrTrophyFromUID(selectedUnitUID));
					}
				}
			}
			int index = 0;
			for (int i = 0; i < this.m_UnitSortList.Count; i++)
			{
				if (this.m_UnitSortList[i].m_UnitUID == selectedUnitUID)
				{
					index = i;
					break;
				}
			}
			this.m_DragCharacterView.TotalCount = this.m_UnitSortList.Count;
			this.m_DragCharacterView.SetIndex(index);
		}

		// Token: 0x06006E18 RID: 28184 RVA: 0x0024251C File Offset: 0x0024071C
		private void UpdateUnitInfoUI()
		{
			if (this.m_NKMUnitData != null)
			{
				NKMWorldMapManager.WorldMapLeaderState unitWorldMapLeaderState = NKMWorldMapManager.GetUnitWorldMapLeaderState(NKCScenManager.CurrentUserData(), this.m_NKMUnitData.m_UnitUID, -1);
				NKCUtil.SetGameobjectActive(this.m_objCityLeader, unitWorldMapLeaderState > NKMWorldMapManager.WorldMapLeaderState.None);
				if (this.m_ctglLock != null)
				{
					this.m_ctglLock.Select(this.m_NKMUnitData.m_bLock, true, true);
				}
				if (this.m_ctglFavorite != null)
				{
					this.m_ctglFavorite.Select(this.m_NKMUnitData.isFavorite, true, true);
				}
				NKCUtil.SetGameobjectActive(this.m_objSeized, this.m_NKMUnitData.IsSeized);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objCityLeader, false);
			}
			if (this.m_Loyalty != null)
			{
				this.m_Loyalty.SetLoyalty(this.m_NKMUnitData);
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_NKMUnitData.m_UnitID);
			if (unitTempletBase != null && unitTempletBase.IsTrophy)
			{
				this.m_btnPractice.enabled = false;
				this.m_cgPractice.alpha = 0.4f;
				NKCUtil.SetGameobjectActive(this.m_UnitInfoBlock, true);
				NKCUtil.SetGameobjectActive(this.m_Loyalty, false);
			}
			else if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_PRIVATE_READY)
			{
				this.m_cbtnSkinMode.enabled = false;
				this.m_cgSkinMode.alpha = 0.4f;
				this.m_btnPractice.enabled = false;
				this.m_cgPractice.alpha = 0.4f;
				NKCUtil.SetGameobjectActive(this.m_UnitInfoBlock, false);
				NKCUtil.SetGameobjectActive(this.m_Loyalty, true);
			}
			else
			{
				this.m_btnPractice.enabled = true;
				this.m_cgPractice.alpha = 1f;
				NKCUtil.SetGameobjectActive(this.m_UnitInfoBlock, false);
				NKCUtil.SetGameobjectActive(this.m_Loyalty, true);
			}
			bool flag = NKCUIVoiceManager.UnitHasVoice(unitTempletBase);
			this.m_cgVoice.alpha = (flag ? 1f : 0.4f);
			this.m_cbtnVoicePopup.enabled = flag;
			if (NKCPopupUnitInfoDetail.IsInstanceOpen)
			{
				NKCPopupUnitInfoDetail.Instance.SetData(this.m_NKMUnitData, null);
			}
			NKCUtil.SetLabelText(this.m_lbVoiceActor, NKCVoiceActorNameTemplet.FindActorName(this.m_NKMUnitData));
			bool bValue = false;
			if (this.m_cbtnCollection != null && NKCCollectionManager.GetUnitTemplet(this.m_NKMUnitData.m_UnitID) != null)
			{
				bValue = true;
			}
			NKCUtil.SetGameobjectActive(this.m_cbtnCollection, bValue);
			bool bValue2 = false;
			if (this.m_cbtnCollection != null && this.m_cbtnCollection.gameObject.activeSelf && this.m_NKMUnitData != null)
			{
				bValue2 = NKCUnitMissionManager.HasRewardEnableMission(this.m_NKMUnitData.m_UnitID);
			}
			NKCUtil.SetGameobjectActive(this.m_objCollectionReward, bValue2);
		}

		// Token: 0x06006E19 RID: 28185 RVA: 0x002427A8 File Offset: 0x002409A8
		private void OnUnitTestButton()
		{
			if (this.IsSeizedUnit(this.m_NKMUnitData))
			{
				return;
			}
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_MOVE_TO_TEST_MODE, delegate()
			{
				NKCScenManager.GetScenManager().Get_SCEN_GAME().PlayPracticeGame(this.m_NKMUnitData, NKM_SHORTCUT_TYPE.SHORTCUT_NONE, "");
			}, null, false);
		}

		// Token: 0x06006E1A RID: 28186 RVA: 0x002427D6 File Offset: 0x002409D6
		private void OnReviewButton()
		{
			NKCUIUnitReview.Instance.OpenUI(this.m_NKMUnitData.m_UnitID);
		}

		// Token: 0x06006E1B RID: 28187 RVA: 0x002427ED File Offset: 0x002409ED
		private void OnSkinButton()
		{
			if (this.IsSeizedUnit(this.m_NKMUnitData))
			{
				return;
			}
			NKCUIShopSkinPopup.Instance.OpenForUnitInfo(this.m_NKMUnitData, true);
		}

		// Token: 0x06006E1C RID: 28188 RVA: 0x00242810 File Offset: 0x00240A10
		private void OnCollectionButton()
		{
			if (this.m_NKMUnitData != null && this.m_NKMUnitData.GetUnitTemplet() != null && this.m_NKMUnitData.GetUnitTemplet().m_UnitTempletBase != null)
			{
				string unitStrID = this.m_NKMUnitData.GetUnitTemplet().m_UnitTempletBase.m_UnitStrID;
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_MOVE_TO_COLLECTION, delegate()
				{
					NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_COLLECTION_UNIT, unitStrID, false);
				}, null, false);
			}
		}

		// Token: 0x06006E1D RID: 28189 RVA: 0x00242882 File Offset: 0x00240A82
		private void OnVoiceButton()
		{
			NKCUIPopupVoice.Instance.Open(this.m_NKMUnitData);
		}

		// Token: 0x06006E1E RID: 28190 RVA: 0x00242894 File Offset: 0x00240A94
		private void OnClickChangeIllust()
		{
			NKCUIPopupIllustView.Instance.Open(this.m_NKMUnitData);
		}

		// Token: 0x06006E1F RID: 28191 RVA: 0x002428A6 File Offset: 0x00240AA6
		private bool IsEquipPresetOpend()
		{
			return this.m_curUIState == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.BASE && this.m_NKCUIUnitInfoInfo.IsPresetOpend();
		}

		// Token: 0x06006E20 RID: 28192 RVA: 0x002428BE File Offset: 0x00240ABE
		private void OnLockToggle(bool bValue)
		{
			if (bValue != this.m_NKMUnitData.m_bLock)
			{
				NKCPacketSender.Send_NKMPacket_LOCK_UNIT_REQ(this.m_NKMUnitData.m_UnitUID, !this.m_NKMUnitData.m_bLock);
			}
		}

		// Token: 0x06006E21 RID: 28193 RVA: 0x002428EC File Offset: 0x00240AEC
		private void OnFavoriteToggle(bool bValue)
		{
			if (bValue != this.m_NKMUnitData.isFavorite)
			{
				NKCPacketSender.Send_NKMPacket_FAVORITE_UNIT_REQ(this.m_NKMUnitData.m_UnitUID, !this.m_NKMUnitData.isFavorite);
			}
		}

		// Token: 0x06006E22 RID: 28194 RVA: 0x0024291A File Offset: 0x00240B1A
		public void UpdateLock(long UnitUID, bool bLock)
		{
			if (this.m_NKMUnitData != null && this.m_NKMUnitData.m_UnitUID == UnitUID)
			{
				this.m_ctglLock.Select(bLock, true, false);
			}
		}

		// Token: 0x06006E23 RID: 28195 RVA: 0x00242941 File Offset: 0x00240B41
		public void UpdateFavorite(long UnitUID, bool bFavorite)
		{
			if (this.m_NKMUnitData != null && this.m_NKMUnitData.m_UnitUID == UnitUID)
			{
				this.m_ctglFavorite.Select(bFavorite, true, false);
			}
		}

		// Token: 0x06006E24 RID: 28196 RVA: 0x00242968 File Offset: 0x00240B68
		private void OnSkinEquip(long unitUID, int equippedSkinID)
		{
			foreach (NKMUnitData nkmunitData in this.m_UnitSortList)
			{
				if (nkmunitData.m_UnitUID == unitUID)
				{
					if (equippedSkinID != 0)
					{
						NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(equippedSkinID);
						if (!NKMSkinManager.IsSkinForCharacter(nkmunitData.m_UnitID, skinTemplet))
						{
							continue;
						}
					}
					nkmunitData.m_SkinID = equippedSkinID;
					if (this.m_DragCharacterView != null)
					{
						foreach (NKCUICharacterView nkcuicharacterView in this.m_DragCharacterView.GetComponentsInChildren<NKCUICharacterView>())
						{
							if (nkcuicharacterView.GetCurrentUnitData().m_UnitUID == unitUID)
							{
								nkcuicharacterView.SetCharacterIllust(nkmunitData, nkmunitData.m_SkinID, false, 0);
								break;
							}
						}
						break;
					}
					break;
				}
			}
		}

		// Token: 0x06006E25 RID: 28197 RVA: 0x00242A38 File Offset: 0x00240C38
		private void OnTouchLoyalty()
		{
			if (this.m_NKMUnitData.IsPermanentContract)
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_LIFETIME_REPLAY, delegate()
				{
					NKCUILifetime.Instance.Open(this.m_NKMUnitData, true);
				}, null, false);
				return;
			}
			if (this.m_NKMUnitData.loyalty < 10000)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_INFORMATION, NKCUtilString.GET_STRING_LIFETIME_LOYALTY_INFO, null, "");
				return;
			}
			if (this.IsSeizedUnit(this.m_NKMUnitData))
			{
				return;
			}
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_LIFETIME_CONTRACT_POPUP, delegate()
			{
				NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_UNIT_LIFETIME, this.m_NKMUnitData.m_UnitUID.ToString(), false);
			}, null, false);
		}

		// Token: 0x06006E26 RID: 28198 RVA: 0x00242AC4 File Offset: 0x00240CC4
		private bool IsSeizedUnit(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				return true;
			}
			if (unitData.IsSeized)
			{
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_UNIT_IS_SEIZED, null, "");
				return true;
			}
			return false;
		}

		// Token: 0x06006E27 RID: 28199 RVA: 0x00242AE8 File Offset: 0x00240CE8
		private void OnClickRecall()
		{
			if (this.m_NKMUnitData != null)
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				DateTime serverUTCTime = NKCSynchronizedTime.GetServerUTCTime(0.0);
				NKMRecallTemplet nkmrecallTemplet = NKMRecallTemplet.Find(this.m_NKMUnitData.m_UnitID, NKMTime.UTCtoLocal(serverUTCTime, 0));
				if (nkmrecallTemplet != null)
				{
					if (nkmuserData.m_RecallHistoryData.ContainsKey(this.m_NKMUnitData.m_UnitID))
					{
						RecallHistoryInfo recallHistoryInfo = nkmuserData.m_RecallHistoryData[this.m_NKMUnitData.m_UnitID];
						if (NKCRecallManager.IsValidTime(nkmrecallTemplet, recallHistoryInfo.lastUpdateDate))
						{
							NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_RECALL_ALREADY_USED, null, "");
							return;
						}
					}
					if (!NKCRecallManager.IsValidTime(nkmrecallTemplet, serverUTCTime))
					{
						NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_RECALL_PERIOD_EXPIRED, null, "");
						return;
					}
					if (!NKCRecallManager.IsValidRegTime(nkmrecallTemplet, this.m_NKMUnitData.m_regDate))
					{
						NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_RECALL_INVALID_ACCQUIRE_TIME, null, "");
						return;
					}
					if (this.m_NKMUnitData.m_bLock || nkmuserData.m_ArmyData.IsUnitInAnyDeck(this.m_NKMUnitData.m_UnitUID))
					{
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_RECALL_ERROR_ALT_USING_UNIT, null, "");
						return;
					}
					if (nkmuserData.backGroundInfo.unitInfoList.Find((NKMBackgroundUnitInfo e) => e.unitUid == this.m_NKMUnitData.m_UnitUID) != null)
					{
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_RECALL_ERROR_ALT_USING_UNIT, null, "");
						return;
					}
					using (Dictionary<int, NKMWorldMapCityData>.ValueCollection.Enumerator enumerator = nkmuserData.m_WorldmapData.worldMapCityDataMap.Values.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.leaderUnitUID == this.m_NKMUnitData.m_UnitUID)
							{
								NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_RECALL_ERROR_ALT_USING_UNIT, null, "");
								return;
							}
						}
					}
					if (this.m_NKMUnitData.OfficeRoomId > 0)
					{
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_RECALL_ERROR_ALT_USING_UNIT, null, "");
						return;
					}
					NKCPopupRecall.Instance.Open(this.m_NKMUnitData);
				}
			}
		}

		// Token: 0x06006E28 RID: 28200 RVA: 0x00242CD4 File Offset: 0x00240ED4
		private RectTransform MakeMainBannerListSlot()
		{
			GameObject gameObject = new GameObject("Banner", new Type[]
			{
				typeof(RectTransform),
				typeof(LayoutElement)
			});
			LayoutElement component = gameObject.GetComponent<LayoutElement>();
			component.ignoreLayout = false;
			component.preferredWidth = this.m_DragCharacterView.m_rtContentRect.GetWidth();
			component.preferredHeight = this.m_DragCharacterView.m_rtContentRect.GetHeight();
			component.flexibleWidth = 2f;
			component.flexibleHeight = 2f;
			return gameObject.GetComponent<RectTransform>();
		}

		// Token: 0x06006E29 RID: 28201 RVA: 0x00242D60 File Offset: 0x00240F60
		private void ProvideMainBannerListSlotData(Transform tr, int idx)
		{
			if (this.m_UnitSortList != null && this.m_UnitSortList.Count > idx)
			{
				NKMUnitData nkmunitData = this.m_UnitSortList[idx];
				if (nkmunitData != null)
				{
					NKCUICharacterView component = tr.GetComponent<NKCUICharacterView>();
					if (component != null)
					{
						component.CloseImmediatelyIllust();
						component.SetCharacterIllust(nkmunitData, false, true, true, 0);
						return;
					}
					NKCUICharacterView nkcuicharacterView = tr.gameObject.AddComponent<NKCUICharacterView>();
					nkcuicharacterView.m_rectIllustRoot = tr.GetComponent<RectTransform>();
					nkcuicharacterView.SetCharacterIllust(nkmunitData, false, true, true, 0);
				}
			}
		}

		// Token: 0x06006E2A RID: 28202 RVA: 0x00242DD6 File Offset: 0x00240FD6
		private void ReturnMainBannerListSlot(Transform go)
		{
			NKCUtil.SetGameobjectActive(go, false);
			UnityEngine.Object.Destroy(go.gameObject);
		}

		// Token: 0x06006E2B RID: 28203 RVA: 0x00242DEC File Offset: 0x00240FEC
		public void TouchCharacter(RectTransform rt, PointerEventData eventData)
		{
			if (this.m_DragCharacterView.GetDragOffset().IsNearlyZero(1E-05f))
			{
				NKCUICharacterView componentInChildren = rt.GetComponentInChildren<NKCUICharacterView>();
				if (componentInChildren != null)
				{
					componentInChildren.OnPointerDown(eventData);
					componentInChildren.OnPointerUp(eventData);
				}
			}
		}

		// Token: 0x06006E2C RID: 28204 RVA: 0x00242E2E File Offset: 0x0024102E
		private void Focus(RectTransform rect, bool bFocus)
		{
			NKCUtil.SetGameobjectActive(rect.gameObject, bFocus);
		}

		// Token: 0x06006E2D RID: 28205 RVA: 0x00242E3C File Offset: 0x0024103C
		public void SelectCharacter(int idx)
		{
			if (this.m_UnitSortList.Count < idx || idx < 0)
			{
				Debug.LogWarning(string.Format("Error - Count : {0}, Index : {1}", this.m_UnitSortList.Count, idx));
				return;
			}
			NKMUnitData nkmunitData = this.m_UnitSortList[idx];
			if (nkmunitData != null)
			{
				this.ChangeUnit(nkmunitData);
			}
		}

		// Token: 0x06006E2E RID: 28206 RVA: 0x00242E98 File Offset: 0x00241098
		private void BannerCleanUp()
		{
			if (this.m_DragCharacterView != null)
			{
				NKCUICharacterView[] componentsInChildren = this.m_DragCharacterView.gameObject.GetComponentsInChildren<NKCUICharacterView>(true);
				if (componentsInChildren != null)
				{
					NKCUICharacterView[] array = componentsInChildren;
					for (int i = 0; i < array.Length; i++)
					{
						array[i].CloseImmediatelyIllust();
					}
				}
				return;
			}
		}

		// Token: 0x06006E2F RID: 28207 RVA: 0x00242EE4 File Offset: 0x002410E4
		private void OnEventPanelClick(BaseEventData e)
		{
			if (this.m_DragCharacterView != null && this.m_DragCharacterView.GetDragOffset().IsNearlyZero(1E-05f))
			{
				RectTransform currentItem = this.m_DragCharacterView.GetCurrentItem();
				if (currentItem != null)
				{
					NKCUICharacterView componentInChildren = currentItem.GetComponentInChildren<NKCUICharacterView>();
					if (componentInChildren != null)
					{
						PointerEventData eventData = new PointerEventData(EventSystem.current);
						componentInChildren.OnPointerDown(eventData);
						componentInChildren.OnPointerUp(eventData);
					}
				}
			}
		}

		// Token: 0x06006E30 RID: 28208 RVA: 0x00242F54 File Offset: 0x00241154
		private void ChangeUnit(NKMUnitData cNKMUnitData)
		{
			if (this.m_NKMUnitData.m_UnitUID == cNKMUnitData.m_UnitUID)
			{
				return;
			}
			this.m_NKCUICharInfoSummary.SetData(cNKMUnitData);
			this.m_NKMUnitData = cNKMUnitData;
			this.UpdateUnitData();
			this.m_lReserveUID = cNKMUnitData.m_UnitUID;
			this.m_fDeltaTime = 0f;
			NKCUtil.SetGameobjectActive(this.m_objRecall, NKCRecallManager.IsRecallTargetUnit(this.m_NKMUnitData, NKCSynchronizedTime.GetServerUTCTime(0.0)));
			if (this.m_objRecall.activeSelf)
			{
				this.SetRecallRemainTime();
			}
		}

		// Token: 0x06006E31 RID: 28209 RVA: 0x00242FDC File Offset: 0x002411DC
		private void UpdateUnitData()
		{
			switch (this.m_curUIState)
			{
			case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.BASE:
				this.m_NKCUIUnitInfoInfo.SetData(this.m_NKMUnitData, this.m_bShowFierceInfo);
				break;
			case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.NEGOTIATION:
				this.m_NKCUIUnitInfoNegotiation.SetData(this.m_NKMUnitData);
				break;
			case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.LIMIT_BREAK:
				this.m_NKCUIUnitInfoLimitBreak.SetData(this.m_NKMUnitData);
				break;
			case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.SKILL_TRAIN:
				this.m_NKCUIUnitInfoSkillTrain.SetData(this.m_NKMUnitData, this.m_NKCUIUnitInfoSkillTrain.SelectedSkillID, false);
				break;
			}
			bool flag = NKMSkinManager.IsCharacterHasSkin(this.m_NKMUnitData.m_UnitID);
			this.m_cgSkinMode.alpha = (flag ? 1f : 0.4f);
			this.m_cbtnSkinMode.enabled = flag;
			this.UpdateUnitInfoUI();
		}

		// Token: 0x06006E32 RID: 28210 RVA: 0x002430A8 File Offset: 0x002412A8
		private void ChangeState(NKCUIUnitInfo.UNIT_INFO_TAB_STATE newState)
		{
			ContentsType contentsType = ContentsType.None;
			switch (newState)
			{
			case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.NEGOTIATION:
				contentsType = ContentsType.PERSONNAL_NEGO;
				break;
			case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.LIMIT_BREAK:
				contentsType = ContentsType.LAB_LIMITBREAK;
				break;
			case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.SKILL_TRAIN:
				contentsType = ContentsType.LAB_TRAINING;
				break;
			}
			if (contentsType != ContentsType.None && !NKCContentManager.IsContentsUnlocked(contentsType, 0, 0))
			{
				switch (this.m_curUIState)
				{
				case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.NEGOTIATION:
					this.m_tglNegotiation.Select(true, true, true);
					break;
				case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.LIMIT_BREAK:
					this.m_tglLimitBreak.Select(true, true, true);
					break;
				case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.SKILL_TRAIN:
					this.m_tglSkillTrain.Select(true, true, true);
					break;
				default:
					this.m_tglInfo.Select(true, true, true);
					break;
				}
				NKCContentManager.ShowLockedMessagePopup(contentsType, 0);
				return;
			}
			if (newState == this.m_curUIState)
			{
				return;
			}
			if (NKCPopupUnitInfoDetail.IsInstanceOpen && newState != NKCUIUnitInfo.UNIT_INFO_TAB_STATE.BASE)
			{
				NKCPopupUnitInfoDetail.CheckInstanceAndClose();
			}
			this.m_curUIState = newState;
			this.TutorialCheck();
			this.UpdateStateUI();
			this.UpdateUnitData();
			NKCUIManager.UpdateUpsideMenu();
		}

		// Token: 0x06006E33 RID: 28211 RVA: 0x00243188 File Offset: 0x00241388
		private void UpdateStateUI()
		{
			NKCUtil.SetGameobjectActive(this.m_UnitInfoControlBtn, this.m_curUIState == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.BASE);
			NKCUtil.SetGameobjectActive(this.m_UnitInfoBottomBtn, this.m_curUIState == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.BASE);
			NKCUtil.SetGameobjectActive(this.m_NKCUIUnitInfoInfo, this.m_curUIState == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.BASE);
			if (this.m_curUIState != NKCUIUnitInfo.UNIT_INFO_TAB_STATE.BASE)
			{
				this.m_NKCUIUnitInfoInfo.SetEnableEquipInfo(false);
			}
			NKCUtil.SetGameobjectActive(this.m_NKCUIUnitInfoNegotiation, this.m_curUIState == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.NEGOTIATION);
			NKCUtil.SetGameobjectActive(this.m_NKCUIUnitInfoLimitBreak, this.m_curUIState == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.LIMIT_BREAK);
			NKCUtil.SetGameobjectActive(this.m_NKCUIUnitInfoSkillTrain, this.m_curUIState == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.SKILL_TRAIN);
			NKCUtil.SetGameobjectActive(this.m_ChangeBtn.gameObject, this.m_curUIState == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.NEGOTIATION || this.m_curUIState == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.LIMIT_BREAK || this.m_curUIState == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.SKILL_TRAIN);
		}

		// Token: 0x06006E34 RID: 28212 RVA: 0x00243250 File Offset: 0x00241450
		public void OnUnitSortOption(NKCUnitSortSystem.UnitListOptions unitOption)
		{
			this.m_preUnitListOption = unitOption;
		}

		// Token: 0x06006E35 RID: 28213 RVA: 0x0024325C File Offset: 0x0024145C
		private void OnSelectUnit()
		{
			if (this.m_curUIState == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.LIMIT_BREAK && this.m_curUIState == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.SKILL_TRAIN)
			{
				this.UnitSelectList.Open(this.GetUnitSelectListOption(NKCUIUnitInfo.UNIT_INFO_TAB_STATE.NONE), new NKCUIUnitSelectList.OnUnitSelectCommand(this.OnUnitSelected), new NKCUIUnitSelectList.OnUnitSortList(this.OnUnitSortList), null, new NKCUIUnitSelectList.OnUnitSortOption(this.OnUnitSortOption), null);
				return;
			}
			this.UnitSelectList.Open(this.GetUnitSelectListOption(NKCUIUnitInfo.UNIT_INFO_TAB_STATE.NONE), new NKCUIUnitSelectList.OnUnitSelectCommand(this.OnUnitSelected), new NKCUIUnitSelectList.OnUnitSortList(this.OnUnitSortList), null, null, null);
		}

		// Token: 0x06006E36 RID: 28214 RVA: 0x002432E4 File Offset: 0x002414E4
		public NKCUIUnitSelectList.UnitSelectListOptions GetUnitSelectListOption(NKCUIUnitInfo.UNIT_INFO_TAB_STATE newTabState = NKCUIUnitInfo.UNIT_INFO_TAB_STATE.NONE)
		{
			if (newTabState == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.NONE)
			{
				newTabState = this.m_curUIState;
			}
			NKCUIUnitSelectList.UnitSelectListOptions options = new NKCUIUnitSelectList.UnitSelectListOptions(NKM_UNIT_TYPE.NUT_NORMAL, false, NKM_DECK_TYPE.NDT_NORMAL, NKCUIUnitSelectList.eUnitSelectListMode.Normal, true);
			options.lstSortOption = NKCUnitSortSystem.GetDefaultSortOptions(NKM_UNIT_TYPE.NUT_NORMAL, false, false);
			if (newTabState == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.SKILL_TRAIN || newTabState == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.LIMIT_BREAK)
			{
				options.bShowRemoveSlot = false;
				options.bCanSelectUnitInMission = true;
				options.eDeckType = NKM_DECK_TYPE.NDT_NORMAL;
				if (this.m_NKMUnitData != null)
				{
					options.m_IncludeUnitUID = this.m_NKMUnitData.m_UnitUID;
				}
			}
			options.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
			options.setExcludeUnitUID = new HashSet<long>();
			options.bDescending = true;
			options.bExcludeLockedUnit = false;
			options.bExcludeDeckedUnit = false;
			options.bHideDeckedUnit = false;
			string menuName = NKCUtilString.GetMenuName(newTabState);
			string emptyMessage = NKCUtilString.GetEmptyMessage(newTabState);
			options.strUpsideMenuName = menuName;
			options.strEmptyMessage = emptyMessage;
			options.bPushBackUnselectable = false;
			options.m_SortOptions.bIgnoreCityState = true;
			options.m_SortOptions.bIgnoreWorldMapLeader = true;
			options.bShowHideDeckedUnitMenu = false;
			options.setUnitFilterCategory = NKCUnitSortSystem.setDefaultUnitFilterCategory;
			options.setUnitSortCategory = NKCUnitSortSystem.setDefaultUnitSortCategory;
			options.m_bUseFavorite = true;
			if (newTabState == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.NEGOTIATION)
			{
				options.bEnableLockUnitSystem = false;
				options.m_SortOptions.AdditionalExcludeFilterFunc = new NKCUnitSortSystem.UnitListOptions.CustomFilterFunc(this.CheckUnitCanNegotiate);
			}
			if (newTabState == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.SKILL_TRAIN || newTabState == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.LIMIT_BREAK)
			{
				NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
				if (newTabState != NKCUIUnitInfo.UNIT_INFO_TAB_STATE.LIMIT_BREAK)
				{
					if (newTabState == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.SKILL_TRAIN)
					{
						foreach (KeyValuePair<long, NKMUnitData> keyValuePair in armyData.m_dicMyUnit)
						{
							if (keyValuePair.Value.m_UnitUID == options.m_IncludeUnitUID)
							{
								options.setExcludeUnitUID.Add(keyValuePair.Key);
							}
							else if (!NKMUnitSkillManager.CheckHaveUpgradableSkill(keyValuePair.Value))
							{
								options.setExcludeUnitUID.Add(keyValuePair.Key);
							}
						}
						options.m_SortOptions.AdditionalExcludeFilterFunc = new NKCUnitSortSystem.UnitListOptions.CustomFilterFunc(this.CheckUnitCanSkillTrain);
					}
				}
				else
				{
					this.dicUnitLimitBreak.Clear();
					NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
					if (nkmuserData != null)
					{
						foreach (KeyValuePair<long, NKMUnitData> keyValuePair2 in armyData.m_dicMyUnit)
						{
							int num = NKMUnitLimitBreakManager.CanThisUnitLimitBreakNow(keyValuePair2.Value, nkmuserData);
							if (keyValuePair2.Value.m_UnitUID == options.m_IncludeUnitUID && num < 0)
							{
								num = 0;
							}
							this.dicUnitLimitBreak[keyValuePair2.Key] = num;
						}
					}
					options.m_SortOptions.AdditionalExcludeFilterFunc = new NKCUnitSortSystem.UnitListOptions.CustomFilterFunc(this.CheckUnitCanLimitBreak);
					options.setUnitSortCategory = new HashSet<NKCUnitSortSystem.eSortCategory>();
					options.setUnitSortCategory.Add(NKCUnitSortSystem.eSortCategory.Custom1);
					foreach (NKCUnitSortSystem.eSortCategory item in NKCUnitSortSystem.setDefaultUnitSortCategory)
					{
						options.setUnitSortCategory.Add(item);
					}
					string @string = NKCStringTable.GetString("SI_GUIDE_MANUAL_TEMPLET_ARTICLE_UNIT_LIMITBREAK", false);
					options.m_SortOptions.lstCustomSortFunc.Add(NKCUnitSortSystem.eSortCategory.Custom1, new KeyValuePair<string, NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc>(@string, new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.SortByUnitCanLimitBreakNow)));
					options.lstSortOption = new List<NKCUnitSortSystem.eSortOption>();
					options.lstSortOption.Add(NKCUnitSortSystem.eSortOption.CustomDescend1);
					NKCUnitSortSystem.GetDefaultSortOptions(NKM_UNIT_TYPE.NUT_NORMAL, false, false).ForEach(delegate(NKCUnitSortSystem.eSortOption e)
					{
						options.lstSortOption.Add(e);
					});
					options.dOnSlotSetData = new NKCUIUnitSelectList.UnitSelectListOptions.OnSlotSetData(this.OnLimitBreakSlotSetData);
				}
				if (this.m_NKMUnitData != null && !options.setExcludeUnitUID.Contains(this.m_NKMUnitData.m_UnitUID))
				{
					options.setExcludeUnitUID.Add(this.m_NKMUnitData.m_UnitUID);
				}
			}
			return options;
		}

		// Token: 0x06006E37 RID: 28215 RVA: 0x00243754 File Offset: 0x00241954
		private bool CheckUnitCanNegotiate(NKMUnitData unitData)
		{
			return NKCNegotiateManager.CanTargetNegotiate(NKCScenManager.CurrentUserData(), unitData) == NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06006E38 RID: 28216 RVA: 0x00243764 File Offset: 0x00241964
		private bool CheckUnitCanSkillTrain(NKMUnitData unitData)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID);
			return unitTempletBase != null && unitTempletBase.m_NKM_UNIT_STYLE_TYPE != NKM_UNIT_STYLE_TYPE.NUST_TRAINER;
		}

		// Token: 0x06006E39 RID: 28217 RVA: 0x0024378C File Offset: 0x0024198C
		private bool CheckUnitCanLimitBreak(NKMUnitData unitData)
		{
			return NKMUnitLimitBreakManager.CanThisUnitLimitBreak(unitData);
		}

		// Token: 0x06006E3A RID: 28218 RVA: 0x00243794 File Offset: 0x00241994
		private int SortByUnitCanLimitBreakNow(NKMUnitData lhs, NKMUnitData rhs)
		{
			int value;
			if (!this.dicUnitLimitBreak.TryGetValue(lhs.m_UnitUID, out value))
			{
				value = NKMUnitLimitBreakManager.CanThisUnitLimitBreakNow(lhs, NKCScenManager.CurrentUserData());
				this.dicUnitLimitBreak[lhs.m_UnitUID] = value;
			}
			int value2;
			if (!this.dicUnitLimitBreak.TryGetValue(rhs.m_UnitUID, out value2))
			{
				value2 = NKMUnitLimitBreakManager.CanThisUnitLimitBreakNow(rhs, NKCScenManager.CurrentUserData());
				this.dicUnitLimitBreak[rhs.m_UnitUID] = value2;
			}
			return value2.CompareTo(value);
		}

		// Token: 0x06006E3B RID: 28219 RVA: 0x00243810 File Offset: 0x00241A10
		private void OnLimitBreakSlotSetData(NKCUIUnitSelectListSlotBase cUnitSlot, NKMUnitData cNKMUnitData, NKMDeckIndex deckIndex)
		{
			if (cNKMUnitData == null)
			{
				return;
			}
			int num;
			if (this.dicUnitLimitBreak.TryGetValue(cNKMUnitData.m_UnitUID, out num))
			{
				NKCUIUnitSelectListSlot nkcuiunitSelectListSlot = cUnitSlot as NKCUIUnitSelectListSlot;
				if (nkcuiunitSelectListSlot != null)
				{
					nkcuiunitSelectListSlot.SetLimitPossibleMark(num >= 0, NKMUnitLimitBreakManager.IsMaxLimitBreak(cNKMUnitData, false));
				}
			}
		}

		// Token: 0x06006E3C RID: 28220 RVA: 0x0024385C File Offset: 0x00241A5C
		private NKM_ERROR_CODE CanSelectUnit(NKMUnitData unitData, NKMArmyData armyData)
		{
			if (unitData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_NOT_EXIST;
			}
			NKMDeckData deckDataByUnitUID = armyData.GetDeckDataByUnitUID(unitData.m_UnitUID);
			if (deckDataByUnitUID != null)
			{
				NKM_DECK_STATE state = deckDataByUnitUID.GetState();
				if (state == NKM_DECK_STATE.DECK_STATE_WARFARE)
				{
					return NKM_ERROR_CODE.NEC_FAIL_WARFARE_DOING;
				}
				if (state == NKM_DECK_STATE.DECK_STATE_DIVE)
				{
					return NKM_ERROR_CODE.NEC_FAIL_DIVE_DOING;
				}
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06006E3D RID: 28221 RVA: 0x002438A0 File Offset: 0x00241AA0
		private void OnUnitSelected(List<long> lstUnitUID)
		{
			if (lstUnitUID == null || lstUnitUID.Count != 1)
			{
				Debug.LogError("NKCUIUnitInfo.OpenUnitEnhance, Fatal Error : UnitSelectList returned wrong list");
				return;
			}
			long uid = lstUnitUID[0];
			NKMUnitData unitOrTrophyFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitOrTrophyFromUID(uid);
			if (unitOrTrophyFromUID == null)
			{
				Debug.Log("NKCUIUnitInfo.OpenUnitEnhance, Fatal Error : wrong uid, newUnitData is null");
				return;
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = this.CanSelectUnit(unitOrTrophyFromUID);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupMessageManager.AddPopupMessage(nkm_ERROR_CODE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			if (this.m_curUIState == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.LIMIT_BREAK && NKMUnitLimitBreakManager.IsMaxLimitBreak(unitOrTrophyFromUID, true))
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_ALREADY_LIMITBREAK_MAX, null, "");
				return;
			}
			this.UnitSelectList.Close();
			if (this.m_curUIState == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.NEGOTIATION)
			{
				NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_NEGOTITATE_READY, unitOrTrophyFromUID, false, false);
			}
			this.m_NKMUnitData = unitOrTrophyFromUID;
		}

		// Token: 0x06006E3E RID: 28222 RVA: 0x00243954 File Offset: 0x00241B54
		private NKM_ERROR_CODE CanSelectUnit(NKMUnitData unitData)
		{
			NKMDeckData deckDataByUnitUID = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetDeckDataByUnitUID(unitData.m_UnitUID);
			NKM_ERROR_CODE result = NKM_ERROR_CODE.NEC_OK;
			if (deckDataByUnitUID != null)
			{
				NKM_DECK_STATE state = deckDataByUnitUID.GetState();
				if (state != NKM_DECK_STATE.DECK_STATE_WARFARE)
				{
					if (state == NKM_DECK_STATE.DECK_STATE_DIVE)
					{
						result = NKM_ERROR_CODE.NEC_FAIL_DIVE_DOING;
					}
				}
				else
				{
					result = NKM_ERROR_CODE.NEC_FAIL_WARFARE_DOING;
				}
			}
			return result;
		}

		// Token: 0x06006E3F RID: 28223 RVA: 0x002439A4 File Offset: 0x00241BA4
		private void OnUnitSortList(long UID, List<NKMUnitData> unitUIDList)
		{
			if (unitUIDList.Count > 0)
			{
				NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
				if (armyData == null)
				{
					return;
				}
				if (this.CanSelectUnit(armyData.GetUnitOrTrophyFromUID(UID), armyData) != NKM_ERROR_CODE.NEC_OK)
				{
					return;
				}
				for (int i = 0; i < unitUIDList.Count; i++)
				{
					if (this.CanSelectUnit(unitUIDList[i], armyData) != NKM_ERROR_CODE.NEC_OK)
					{
						unitUIDList.RemoveAt(i);
						i--;
					}
				}
			}
			this.BannerCleanUp();
			this.m_UnitSortList = unitUIDList;
			this.m_DragCharacterView.TotalCount = this.m_UnitSortList.Count;
			if (this.m_UnitSortList.Count > 0)
			{
				for (int j = 0; j < this.m_UnitSortList.Count; j++)
				{
					if (this.m_UnitSortList[j].m_UnitUID == UID)
					{
						this.m_DragCharacterView.SetIndex(j);
						return;
					}
				}
			}
		}

		// Token: 0x06006E40 RID: 28224 RVA: 0x00243A74 File Offset: 0x00241C74
		public bool IsBlockedUnit()
		{
			if (this.m_NKMUnitData != null)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_NKMUnitData.m_UnitID);
				if (unitTempletBase != null && unitTempletBase.m_NKM_UNIT_STYLE_TYPE == NKM_UNIT_STYLE_TYPE.NUST_TRAINER)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x170012A3 RID: 4771
		// (get) Token: 0x06006E41 RID: 28225 RVA: 0x00243AA9 File Offset: 0x00241CA9
		private NKCUIUnitSelectList UnitSelectList
		{
			get
			{
				if (this.m_UIUnitSelectList == null)
				{
					this.m_UIUnitSelectList = NKCUIUnitSelectList.OpenNewInstance(true);
				}
				return this.m_UIUnitSelectList;
			}
		}

		// Token: 0x06006E42 RID: 28226 RVA: 0x00243ACC File Offset: 0x00241CCC
		public void OnRecv(NKMPacket_UNIT_SKILL_UPGRADE_ACK sPacket)
		{
			if (this.m_curUIState == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.SKILL_TRAIN)
			{
				this.m_NKCUIUnitInfoSkillTrain.OnSkillLevelUp(sPacket.skillID);
				if (this.m_PlayVoiceSoundID == 0 || !NKCSoundManager.IsPlayingVoice(this.m_PlayVoiceSoundID))
				{
					this.m_PlayVoiceSoundID = NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_GROWTH_SKILL, NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetUnitFromUID(sPacket.unitUID), false, false);
				}
			}
		}

		// Token: 0x06006E43 RID: 28227 RVA: 0x00243B31 File Offset: 0x00241D31
		public void OnRecv(NKMPacket_EQUIP_PRESET_LIST_ACK cPacket)
		{
			NKCUIUnitInfoInfo nkcuiunitInfoInfo = this.m_NKCUIUnitInfoInfo;
			if (nkcuiunitInfoInfo == null)
			{
				return;
			}
			nkcuiunitInfoInfo.OpenEquipPreset(cPacket.presetDatas);
		}

		// Token: 0x06006E44 RID: 28228 RVA: 0x00243B49 File Offset: 0x00241D49
		public void OnRecv(NKMPacket_LIMIT_BREAK_UNIT_ACK sPacket)
		{
			if (this.m_curUIState == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.LIMIT_BREAK)
			{
				NKCUIGameResultGetUnit.ShowUnitTranscendence(sPacket.unitData, delegate
				{
					if (NKCGameEventManager.IsEventPlaying())
					{
						NKCGameEventManager.WaitFinished();
					}
				});
			}
		}

		// Token: 0x06006E45 RID: 28229 RVA: 0x00243B7E File Offset: 0x00241D7E
		public void ReserveLevelUpFx(NKCNegotiateManager.NegotiateResultUIData uiData)
		{
			this.m_NKCUIUnitInfoNegotiation.ReserveLevelUpFx(uiData);
		}

		// Token: 0x06006E46 RID: 28230 RVA: 0x00243B8C File Offset: 0x00241D8C
		private void TutorialCheck()
		{
			switch (this.m_curUIState)
			{
			case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.BASE:
				NKCTutorialManager.TutorialRequired(TutorialPoint.UnitInfo, true);
				return;
			case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.NEGOTIATION:
				NKCTutorialManager.TutorialRequired(TutorialPoint.UnitNegotiate, true);
				return;
			case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.LIMIT_BREAK:
				NKCTutorialManager.TutorialRequired(TutorialPoint.UnitLimitBreak, true);
				return;
			case NKCUIUnitInfo.UNIT_INFO_TAB_STATE.SKILL_TRAIN:
				NKCTutorialManager.TutorialRequired(TutorialPoint.UnitSkillTraining, true);
				return;
			default:
				return;
			}
		}

		// Token: 0x06006E47 RID: 28231 RVA: 0x00243BE0 File Offset: 0x00241DE0
		public RectTransform GetSkillLevelSlotRect(int index)
		{
			if (this.m_NKCUIUnitInfoSkillTrain == null || this.m_NKCUIUnitInfoSkillTrain.m_lstSkillSlot == null)
			{
				return null;
			}
			if (index >= this.m_NKCUIUnitInfoSkillTrain.m_lstSkillSlot.Count)
			{
				return null;
			}
			NKCUIComButton cbtnSlot = this.m_NKCUIUnitInfoSkillTrain.m_lstSkillSlot[index].m_slot.m_cbtnSlot;
			if (cbtnSlot == null)
			{
				return null;
			}
			return cbtnSlot.GetComponent<RectTransform>();
		}

		// Token: 0x170012A4 RID: 4772
		// (get) Token: 0x06006E48 RID: 28232 RVA: 0x00243C45 File Offset: 0x00241E45
		public NKCUIEquipPreset EquipPreset
		{
			get
			{
				return this.m_NKCUIUnitInfoInfo.EquipPreset;
			}
		}

		// Token: 0x06006E49 RID: 28233 RVA: 0x00243C52 File Offset: 0x00241E52
		public void UpdateEquipSlots()
		{
			this.m_NKCUIUnitInfoInfo.UpdateEquipSlots();
		}

		// Token: 0x06006E4A RID: 28234 RVA: 0x00243C5F File Offset: 0x00241E5F
		public bool EquipPresetOpened()
		{
			return !(this.m_NKCUIUnitInfoInfo == null) && this.m_NKCUIUnitInfoInfo.EquipPresetOpened();
		}

		// Token: 0x06006E4B RID: 28235 RVA: 0x00243C7C File Offset: 0x00241E7C
		public void RefreshUIForReconnect()
		{
			if (this.m_curUIState == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.NEGOTIATION)
			{
				this.m_NKCUIUnitInfoNegotiation.RefreshUIForReconnect();
			}
		}

		// Token: 0x06006E4C RID: 28236 RVA: 0x00243C94 File Offset: 0x00241E94
		private Sprite GetBackgroundSprite(NKCUIUnitInfo.UNIT_INFO_TAB_STATE type)
		{
			string text = (type == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.LIMIT_BREAK || type == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.NEGOTIATION || type == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.SKILL_TRAIN) ? "AB_UI_NUF_BASE_BG" : "AB_UI_BG_SPRITE";
			string text2;
			if (type != NKCUIUnitInfo.UNIT_INFO_TAB_STATE.NEGOTIATION)
			{
				if (type - NKCUIUnitInfo.UNIT_INFO_TAB_STATE.LIMIT_BREAK > 1)
				{
					text2 = "BG";
				}
				else
				{
					text2 = "NKM_UI_BASE_LAB_BG";
				}
			}
			else
			{
				text2 = "NKM_UI_BASE_PERSONNEL_BG";
			}
			Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>(text, text2, false);
			if (orLoadAssetResource == null)
			{
				Debug.LogError("Error - NKCUIUnitInfo::GetBackgroundSprite - path:" + text + ", name:" + text2);
			}
			return orLoadAssetResource;
		}

		// Token: 0x06006E4D RID: 28237 RVA: 0x00243D0C File Offset: 0x00241F0C
		private void SetRecallRemainTime()
		{
			NKMRecallTemplet nkmrecallTemplet = NKMRecallTemplet.Find(this.m_NKMUnitData.m_UnitID, NKMTime.UTCtoLocal(NKCSynchronizedTime.GetServerUTCTime(0.0), 0));
			if (nkmrecallTemplet != null)
			{
				NKCUtil.SetLabelText(this.m_lbRecallTime, string.Format(NKCUtilString.GET_STRING_RECALL_DESC_END_DATE, NKCUtilString.GetRemainTimeStringEx(nkmrecallTemplet.IntervalTemplet.GetEndDateUtc())));
			}
		}

		// Token: 0x06006E4E RID: 28238 RVA: 0x00243D68 File Offset: 0x00241F68
		public void SetUnitAnimation(NKCASUIUnitIllust.eAnimation animation = NKCASUIUnitIllust.eAnimation.UNIT_IDLE)
		{
			if (this.m_DragCharacterView != null)
			{
				RectTransform currentItem = this.m_DragCharacterView.GetCurrentItem();
				if (currentItem != null)
				{
					NKCUICharacterView componentInChildren = currentItem.gameObject.GetComponentInChildren<NKCUICharacterView>();
					if (componentInChildren != null)
					{
						componentInChildren.SetAnimation(animation, false);
					}
				}
			}
			if (animation == NKCASUIUnitIllust.eAnimation.UNIT_IDLE)
			{
				NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_NEGOTITATE_SUCCESS, this.m_NKMUnitData, false, false);
				return;
			}
			if (animation != NKCASUIUnitIllust.eAnimation.UNIT_LAUGH)
			{
				return;
			}
			NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_NEGOTITATE_SUCCESS_GREAT, this.m_NKMUnitData, false, false);
		}

		// Token: 0x06006E4F RID: 28239 RVA: 0x00243DE0 File Offset: 0x00241FE0
		private void Update()
		{
			if (this.m_curUIState == NKCUIUnitInfo.UNIT_INFO_TAB_STATE.NEGOTIATION)
			{
				this.m_NKCUIUnitInfoNegotiation.OnUpdateButtonHold();
			}
			if (this.m_objRecall.activeSelf)
			{
				this.m_fDeltaTime += Time.deltaTime;
				if (this.m_fDeltaTime > 1f)
				{
					this.m_fDeltaTime -= 1f;
					this.SetRecallRemainTime();
				}
			}
		}

		// Token: 0x04005976 RID: 22902
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_unit_info";

		// Token: 0x04005977 RID: 22903
		private const string UI_ASSET_NAME = "NKM_UI_UNIT_INFO";

		// Token: 0x04005978 RID: 22904
		private static NKCUIUnitInfo m_Instance;

		// Token: 0x04005979 RID: 22905
		private NKMUnitData m_NKMUnitData;

		// Token: 0x0400597A RID: 22906
		[Header("요약정보창")]
		public NKCUICharInfoSummary m_NKCUICharInfoSummary;

		// Token: 0x0400597B RID: 22907
		[Header("지부장 표시")]
		public GameObject m_objCityLeader;

		// Token: 0x0400597C RID: 22908
		[Header("애사심")]
		public NKCUIComStateButton m_btnLoyalty;

		// Token: 0x0400597D RID: 22909
		public NKCUIComUnitLoyalty m_Loyalty;

		// Token: 0x0400597E RID: 22910
		[Header("우측 상단 토글")]
		public NKCUIComToggle m_tglInfo;

		// Token: 0x0400597F RID: 22911
		public NKCUIComToggle m_tglNegotiation;

		// Token: 0x04005980 RID: 22912
		public NKCUIComToggle m_tglLimitBreak;

		// Token: 0x04005981 RID: 22913
		public NKCUIComToggle m_tglSkillTrain;

		// Token: 0x04005982 RID: 22914
		public GameObject m_objNegotiationLock;

		// Token: 0x04005983 RID: 22915
		public GameObject m_objLimitBreakLock;

		// Token: 0x04005984 RID: 22916
		public GameObject m_objSkillTrainLock;

		// Token: 0x04005985 RID: 22917
		[Header("우측 서브UI")]
		public NKCUIUnitInfoInfo m_NKCUIUnitInfoInfo;

		// Token: 0x04005986 RID: 22918
		public NKCUIUnitInfoLimitBreak m_NKCUIUnitInfoLimitBreak;

		// Token: 0x04005987 RID: 22919
		public NKCUIUnitInfoNegotiation m_NKCUIUnitInfoNegotiation;

		// Token: 0x04005988 RID: 22920
		public NKCUIUnitInfoSkillTrain m_NKCUIUnitInfoSkillTrain;

		// Token: 0x04005989 RID: 22921
		public GameObject m_UnitInfoBlock;

		// Token: 0x0400598A RID: 22922
		[Header("유닛 일러스트")]
		public NKCUIComDragSelectablePanel m_DragCharacterView;

		// Token: 0x0400598B RID: 22923
		public EventTrigger m_evtPanel;

		// Token: 0x0400598C RID: 22924
		[Header("기타 버튼")]
		public CanvasGroup m_cgPractice;

		// Token: 0x0400598D RID: 22925
		public NKCUIComButton m_btnPractice;

		// Token: 0x0400598E RID: 22926
		public NKCUIComButton m_cbtnChangeIllust;

		// Token: 0x0400598F RID: 22927
		public NKCUIComToggle m_ctglLock;

		// Token: 0x04005990 RID: 22928
		public NKCUIComToggle m_ctglFavorite;

		// Token: 0x04005991 RID: 22929
		public NKCUIComButton m_cbtnReview;

		// Token: 0x04005992 RID: 22930
		public CanvasGroup m_cgSkinMode;

		// Token: 0x04005993 RID: 22931
		public NKCUIComButton m_cbtnSkinMode;

		// Token: 0x04005994 RID: 22932
		public NKCUIComButton m_cbtnCollection;

		// Token: 0x04005995 RID: 22933
		public GameObject m_objCollectionReward;

		// Token: 0x04005996 RID: 22934
		public CanvasGroup m_cgVoice;

		// Token: 0x04005997 RID: 22935
		public NKCUIComButton m_cbtnVoicePopup;

		// Token: 0x04005998 RID: 22936
		[Space]
		public GameObject m_UnitInfoControlBtn;

		// Token: 0x04005999 RID: 22937
		public GameObject m_UnitInfoBottomBtn;

		// Token: 0x0400599A RID: 22938
		[Space]
		public GameObject m_objSeized;

		// Token: 0x0400599B RID: 22939
		public NKCUIComStateButton m_ChangeBtn;

		// Token: 0x0400599C RID: 22940
		[Header("리콜")]
		public GameObject m_objRecall;

		// Token: 0x0400599D RID: 22941
		public NKCUIComStateButton m_btnRecall;

		// Token: 0x0400599E RID: 22942
		public Text m_lbRecallTime;

		// Token: 0x0400599F RID: 22943
		[Header("성우")]
		public Text m_lbVoiceActor;

		// Token: 0x040059A0 RID: 22944
		[Header("기타")]
		public Image m_BG;

		// Token: 0x040059A1 RID: 22945
		private NKCUIUnitInfo.OnRemoveFromDeck dOnRemoveFromDeck;

		// Token: 0x040059A2 RID: 22946
		private long m_lReserveUID;

		// Token: 0x040059A3 RID: 22947
		private bool m_bShowFierceInfo;

		// Token: 0x040059A4 RID: 22948
		private NKCUIUnitInfo.UNIT_INFO_TAB_STATE m_curUIState;

		// Token: 0x040059A5 RID: 22949
		private NKCUnitSortSystem.UnitListOptions m_preUnitListOption;

		// Token: 0x040059A6 RID: 22950
		private Dictionary<long, int> dicUnitLimitBreak = new Dictionary<long, int>();

		// Token: 0x040059A7 RID: 22951
		private List<NKMUnitData> m_UnitSortList = new List<NKMUnitData>();

		// Token: 0x040059A8 RID: 22952
		private NKCUIUnitSelectList m_UIUnitSelectList;

		// Token: 0x040059A9 RID: 22953
		private int m_PlayVoiceSoundID;

		// Token: 0x040059AA RID: 22954
		private float m_fDeltaTime;

		// Token: 0x02001701 RID: 5889
		// (Invoke) Token: 0x0600B202 RID: 45570
		public delegate void OnRemoveFromDeck(NKMUnitData unitData);

		// Token: 0x02001702 RID: 5890
		public class OpenOption
		{
			// Token: 0x0600B205 RID: 45573 RVA: 0x00361C61 File Offset: 0x0035FE61
			public OpenOption(List<long> UnitUIDList, int SlotIdx = 0)
			{
				if (UnitUIDList == null || UnitUIDList.Count <= 1)
				{
					return;
				}
				this.m_UnitUIDList = UnitUIDList;
				this.m_SelectSlotIndex = SlotIdx;
			}

			// Token: 0x0600B206 RID: 45574 RVA: 0x00361C9A File Offset: 0x0035FE9A
			public OpenOption(List<NKMUnitData> lstUnitData, int SlotIdx = 0)
			{
				if (lstUnitData == null || lstUnitData.Count <= 1)
				{
					return;
				}
				this.m_lstUnitData = lstUnitData;
				this.m_SelectSlotIndex = SlotIdx;
			}

			// Token: 0x0400A5C2 RID: 42434
			public readonly List<long> m_UnitUIDList = new List<long>();

			// Token: 0x0400A5C3 RID: 42435
			public readonly List<NKMUnitData> m_lstUnitData = new List<NKMUnitData>();

			// Token: 0x0400A5C4 RID: 42436
			public int m_SelectSlotIndex;

			// Token: 0x0400A5C5 RID: 42437
			public bool m_bShowFierceInfo;
		}

		// Token: 0x02001703 RID: 5891
		public enum UNIT_INFO_TAB_STATE
		{
			// Token: 0x0400A5C7 RID: 42439
			NONE,
			// Token: 0x0400A5C8 RID: 42440
			BASE,
			// Token: 0x0400A5C9 RID: 42441
			NEGOTIATION,
			// Token: 0x0400A5CA RID: 42442
			LIMIT_BREAK,
			// Token: 0x0400A5CB RID: 42443
			SKILL_TRAIN
		}
	}
}
