using System;
using System.Collections.Generic;
using ClientPacket.Community;
using Cs.Protocol;
using NKC.Templet;
using NKC.UI.Component;
using NKC.UI.Guide;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Collection
{
	// Token: 0x02000C2F RID: 3119
	public class NKCUICollectionUnitInfo : NKCUIBase
	{
		// Token: 0x170016D6 RID: 5846
		// (get) Token: 0x06009093 RID: 37011 RVA: 0x00313DAC File Offset: 0x00311FAC
		public static NKCUICollectionUnitInfo Instance
		{
			get
			{
				if (NKCUICollectionUnitInfo.m_Instance == null)
				{
					NKCUICollectionUnitInfo.m_loadedUIData = NKCUIManager.OpenNewInstance<NKCUICollectionUnitInfo>("ab_ui_nkm_ui_collection", NKCUICollectionUnitInfo.m_isGauntlet ? "NKM_UI_COLLECTION_UNIT_INFO_OTHER" : "NKM_UI_COLLECTION_UNIT_INFO", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUICollectionUnitInfo.CleanupInstance));
					NKCUICollectionUnitInfo.m_Instance = NKCUICollectionUnitInfo.m_loadedUIData.GetInstance<NKCUICollectionUnitInfo>();
					NKCUICollectionUnitInfo.m_Instance.Init();
				}
				return NKCUICollectionUnitInfo.m_Instance;
			}
		}

		// Token: 0x06009094 RID: 37012 RVA: 0x00313E13 File Offset: 0x00312013
		private static void CleanupInstance()
		{
			NKCUICollectionUnitInfo.m_isGauntlet = false;
			NKCUICollectionUnitInfo.m_Instance = null;
		}

		// Token: 0x170016D7 RID: 5847
		// (get) Token: 0x06009095 RID: 37013 RVA: 0x00313E21 File Offset: 0x00312021
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUICollectionUnitInfo.m_Instance != null && NKCUICollectionUnitInfo.m_Instance.IsOpen;
			}
		}

		// Token: 0x06009096 RID: 37014 RVA: 0x00313E3C File Offset: 0x0031203C
		public static void CheckInstanceAndClose()
		{
			if (NKCUICollectionUnitInfo.m_loadedUIData != null)
			{
				NKCUICollectionUnitInfo.m_loadedUIData.CloseInstance();
				NKCUICollectionUnitInfo.m_loadedUIData = null;
			}
		}

		// Token: 0x06009097 RID: 37015 RVA: 0x00313E55 File Offset: 0x00312055
		private void OnDestroy()
		{
			NKCUICollectionUnitInfo.CheckInstanceAndClose();
		}

		// Token: 0x170016D8 RID: 5848
		// (get) Token: 0x06009098 RID: 37016 RVA: 0x00313E5C File Offset: 0x0031205C
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_UNIT_INFO;
			}
		}

		// Token: 0x170016D9 RID: 5849
		// (get) Token: 0x06009099 RID: 37017 RVA: 0x00313E63 File Offset: 0x00312063
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170016DA RID: 5850
		// (get) Token: 0x0600909A RID: 37018 RVA: 0x00313E66 File Offset: 0x00312066
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return this.m_UpsideMenuMode;
			}
		}

		// Token: 0x0600909B RID: 37019 RVA: 0x00313E6E File Offset: 0x0031206E
		public void Init()
		{
			this.InitUI();
			this.InitTag();
		}

		// Token: 0x0600909C RID: 37020 RVA: 0x00313E7C File Offset: 0x0031207C
		public void InitUI()
		{
			if (this.m_NKM_UI_COLLECTION_UNIT_INFO_BOTTOM_BUTTON_ILLUST_CHANGE != null)
			{
				this.m_NKM_UI_COLLECTION_UNIT_INFO_BOTTOM_BUTTON_ILLUST_CHANGE.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_COLLECTION_UNIT_INFO_BOTTOM_BUTTON_ILLUST_CHANGE.PointerClick.AddListener(new UnityAction(this.OnClickChangeIllust));
			}
			if (this.m_NKM_UI_COLLECTION_UNIT_INFO_BOTTOM_BUTTON_PRACTICE != null)
			{
				this.m_NKM_UI_COLLECTION_UNIT_INFO_BOTTOM_BUTTON_PRACTICE.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_COLLECTION_UNIT_INFO_BOTTOM_BUTTON_PRACTICE.PointerClick.AddListener(new UnityAction(this.OnUnitTestButton));
			}
			if (this.m_NKM_UI_COLLECTION_UNIT_INFO_BOTTOM_BUTTON_APPRAISAL != null)
			{
				this.m_NKM_UI_COLLECTION_UNIT_INFO_BOTTOM_BUTTON_APPRAISAL.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_COLLECTION_UNIT_INFO_BOTTOM_BUTTON_APPRAISAL.PointerClick.AddListener(new UnityAction(this.OnUnitAppraisal));
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_INFO_BOTTOM_BUTTON_APPRAISAL, NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.UNIT_REVIEW_SYSTEM));
			if (this.m_NKM_UI_COLLECTION_UNIT_PROFILE_TAG_VIEW_ALL_BUTTON != null)
			{
				this.m_NKM_UI_COLLECTION_UNIT_PROFILE_TAG_VIEW_ALL_BUTTON.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_COLLECTION_UNIT_PROFILE_TAG_VIEW_ALL_BUTTON.PointerClick.AddListener(delegate()
				{
					this.ShowTag();
				});
			}
			if (this.m_NKM_UI_COLLECTION_UNIT_PROFILE_VOICE_BUTTON != null)
			{
				this.m_NKM_UI_COLLECTION_UNIT_PROFILE_VOICE_BUTTON.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_COLLECTION_UNIT_PROFILE_VOICE_BUTTON.PointerClick.AddListener(new UnityAction(this.OnUnitVoice));
			}
			if (this.m_NKM_UI_COLLECTION_UNIT_INFO_LOYALTY != null)
			{
				this.m_NKM_UI_COLLECTION_UNIT_INFO_LOYALTY.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_COLLECTION_UNIT_INFO_LOYALTY.PointerClick.AddListener(new UnityAction(this.OnReplayLifetime));
			}
			if (this.m_cbtnUnitInfoDetailPopup != null)
			{
				NKCUtil.SetGameobjectActive(this.m_cbtnUnitInfoDetailPopup, false);
				this.m_cbtnUnitInfoDetailPopup.PointerClick.RemoveAllListeners();
				this.m_cbtnUnitInfoDetailPopup.PointerClick.AddListener(new UnityAction(this.OpenUnitInfoDetailPopup));
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
			if (this.m_UISkillPanel != null)
			{
				this.m_UISkillPanel.Init();
				this.m_UISkillPanel.SetOpenPopupWhenSelected();
			}
			if (this.m_UISkinPanel != null)
			{
				this.m_UISkinPanel.Init(new NKCUIUnitInfoSkinPanel.ChangeSkin(this.ChangeSkin));
			}
			this.m_slotEquipWeapon.Init();
			this.m_slotEquipWeapon.Set_EQUIP_BOX_BOTTOM_MENU_TYPE(NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_NONE);
			this.m_slotEquipDefense.Init();
			this.m_slotEquipDefense.Set_EQUIP_BOX_BOTTOM_MENU_TYPE(NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_NONE);
			this.m_slotEquipAcc.Init();
			this.m_slotEquipAcc.Set_EQUIP_BOX_BOTTOM_MENU_TYPE(NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_NONE);
			this.m_slotEquipAcc_2.Init();
			this.m_slotEquipAcc_2.Set_EQUIP_BOX_BOTTOM_MENU_TYPE(NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_NONE);
			if (null != this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_PROFILE)
			{
				this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_PROFILE.PointerClick.RemoveAllListeners();
				this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_PROFILE.PointerClick.AddListener(delegate()
				{
					this.ChangeState(NKCUICollectionUnitInfo.eCollectionState.CS_PROFILE);
				});
			}
			if (null != this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_STAT)
			{
				this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_STAT.PointerClick.RemoveAllListeners();
				this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_STAT.PointerClick.AddListener(delegate()
				{
					this.ChangeState(NKCUICollectionUnitInfo.eCollectionState.CS_STATUS);
				});
			}
			if (null != this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_TAG)
			{
				this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_TAG.PointerClick.RemoveAllListeners();
				this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_TAG.PointerClick.AddListener(delegate()
				{
					this.ChangeState(NKCUICollectionUnitInfo.eCollectionState.CS_TAG);
				});
			}
			if (null != this.m_cbtn_NKM_UI_COLLECTION_UNIT_INFO_USER_TAG_BUTTON_CLOSE)
			{
				this.m_cbtn_NKM_UI_COLLECTION_UNIT_INFO_USER_TAG_BUTTON_CLOSE.PointerClick.RemoveAllListeners();
				this.m_cbtn_NKM_UI_COLLECTION_UNIT_INFO_USER_TAG_BUTTON_CLOSE.PointerClick.AddListener(delegate()
				{
					this.OnCloseTagList();
				});
			}
			if (null != this.m_GuideBtn)
			{
				this.m_GuideBtn.PointerClick.RemoveAllListeners();
				this.m_GuideBtn.PointerClick.AddListener(delegate()
				{
					NKCUIPopUpGuide.Instance.Open(this.m_GuideStrID, 0);
				});
			}
			bool openTagCollectionMission = NKCUnitMissionManager.GetOpenTagCollectionMission();
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_ACHIEVEMENT_BUTTON, openTagCollectionMission);
			if (openTagCollectionMission)
			{
				NKCUtil.SetButtonClickDelegate(this.m_NKM_UI_COLLECTION_UNIT_ACHIEVEMENT_BUTTON, new UnityAction(this.OnClickUnitAchievement));
			}
			this.m_NKCUICharInfoSummary.SetUnitClassRootActive(false);
			this.m_NKCUICharInfoSummary.Init(false);
			base.gameObject.SetActive(false);
		}

		// Token: 0x0600909D RID: 37021 RVA: 0x00314328 File Offset: 0x00312528
		private void OnEventPanelClick(BaseEventData e)
		{
			if (this.m_DragCharacterView != null && this.m_DragCharacterView.GetDragOffset() == 0f)
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

		// Token: 0x0600909E RID: 37022 RVA: 0x00314394 File Offset: 0x00312594
		private void ChangeUnit(NKMUnitData cNKMUnitData)
		{
			this.m_eOldTagSortType = this.m_eCurrentTagSortType;
			this.m_eCurrentTagSortType = NKCUICollectionUnitInfo.eTagSortOption.TS_TAG_NORMAL;
			this.UpdateTagInfo(cNKMUnitData.m_UnitID);
			this.m_NKCUICharInfoSummary.SetData(cNKMUnitData);
			this.SetData(cNKMUnitData);
			this.UpdateMyVoteCount(cNKMUnitData.m_UnitID);
			if (NKCPopupUnitInfoDetail.IsInstanceOpen)
			{
				NKCPopupUnitInfoDetail.InstanceOpen(this.m_NKMUnitData, NKCPopupUnitInfoDetail.UnitInfoDetailType.gauntlet, this.m_listNKMEquipItemData);
			}
		}

		// Token: 0x0600909F RID: 37023 RVA: 0x003143F8 File Offset: 0x003125F8
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

		// Token: 0x060090A0 RID: 37024 RVA: 0x00314484 File Offset: 0x00312684
		private void ProvideMainBannerListSlotData(Transform tr, int idx)
		{
			if (this.m_OpenOption != null && this.m_OpenOption.m_lstUnitData != null)
			{
				NKMUnitData nkmunitData = this.m_OpenOption.m_lstUnitData[idx];
				Debug.Log(string.Format("<color=yellow>target : {0}, idx : {1}, </color>", tr.name, idx));
				if (nkmunitData != null)
				{
					this.m_SkinID = 0;
					NKCUICharacterView component = tr.GetComponent<NKCUICharacterView>();
					if (component != null)
					{
						component.CloseImmediatelyIllust();
						component.SetCharacterIllust(nkmunitData.m_UnitID, 0, false, true, 0);
						return;
					}
					NKCUICharacterView nkcuicharacterView = tr.gameObject.AddComponent<NKCUICharacterView>();
					nkcuicharacterView.m_rectIllustRoot = tr.GetComponent<RectTransform>();
					nkcuicharacterView.SetCharacterIllust(nkmunitData.m_UnitID, 0, false, true, 0);
				}
			}
		}

		// Token: 0x060090A1 RID: 37025 RVA: 0x00314530 File Offset: 0x00312730
		private void ReturnMainBannerListSlot(Transform go)
		{
			NKCUtil.SetGameobjectActive(go, false);
			UnityEngine.Object.Destroy(go.gameObject);
		}

		// Token: 0x060090A2 RID: 37026 RVA: 0x00314544 File Offset: 0x00312744
		public void TouchCharacter(RectTransform rt, PointerEventData eventData)
		{
			if (this.m_DragCharacterView.GetDragOffset() == 0f)
			{
				NKCUICharacterView componentInChildren = rt.GetComponentInChildren<NKCUICharacterView>();
				if (componentInChildren != null)
				{
					componentInChildren.OnPointerDown(eventData);
					componentInChildren.OnPointerUp(eventData);
				}
			}
		}

		// Token: 0x060090A3 RID: 37027 RVA: 0x00314581 File Offset: 0x00312781
		private void Focus(RectTransform rect, bool bFocus)
		{
			NKCUtil.SetGameobjectActive(rect.gameObject, bFocus);
		}

		// Token: 0x060090A4 RID: 37028 RVA: 0x00314590 File Offset: 0x00312790
		private void FocusColor(RectTransform rect, Color ApplyColor)
		{
			NKCUICharacterView componentInChildren = rect.gameObject.GetComponentInChildren<NKCUICharacterView>();
			if (componentInChildren != null)
			{
				componentInChildren.SetColor(ApplyColor, false);
			}
		}

		// Token: 0x060090A5 RID: 37029 RVA: 0x003145BC File Offset: 0x003127BC
		public void SelectCharacter(int idx)
		{
			if (this.m_OpenOption.m_lstUnitData.Count < idx || idx < 0)
			{
				return;
			}
			NKMUnitData nkmunitData = this.m_OpenOption.m_lstUnitData[idx];
			if (nkmunitData != null)
			{
				this.ChangeUnit(nkmunitData);
			}
		}

		// Token: 0x060090A6 RID: 37030 RVA: 0x00314600 File Offset: 0x00312800
		private void BannerCleanUp()
		{
			if (this.m_DragCharacterView != null)
			{
				NKCUICharacterView[] componentsInChildren = this.m_DragCharacterView.gameObject.GetComponentsInChildren<NKCUICharacterView>(true);
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].CloseImmediatelyIllust();
				}
				return;
			}
		}

		// Token: 0x060090A7 RID: 37031 RVA: 0x00314644 File Offset: 0x00312844
		public static void CheckInstanceAndOpen(NKMUnitData cNKMUnitData, NKCUIUnitInfo.OpenOption openOption, List<NKMEquipItemData> listNKMEquipItemData = null, NKCUICollectionUnitInfo.eCollectionState eStartingState = NKCUICollectionUnitInfo.eCollectionState.CS_PROFILE, bool isGauntlet = false, NKCUIUpsideMenu.eMode upsideMenuMode = NKCUIUpsideMenu.eMode.Normal)
		{
			if (NKCUICollectionUnitInfo.m_Instance != null && NKCUICollectionUnitInfo.m_isGauntlet != isGauntlet)
			{
				NKCUICollectionUnitInfo.m_loadedUIData.CloseInstance();
			}
			NKCUICollectionUnitInfo.m_isGauntlet = isGauntlet;
			NKCUICollectionUnitInfo.Instance.Open(cNKMUnitData, openOption, listNKMEquipItemData, eStartingState, upsideMenuMode);
		}

		// Token: 0x060090A8 RID: 37032 RVA: 0x00314680 File Offset: 0x00312880
		private void Open(NKMUnitData cNKMUnitData, NKCUIUnitInfo.OpenOption openOption, List<NKMEquipItemData> listNKMEquipItemData = null, NKCUICollectionUnitInfo.eCollectionState eStartingState = NKCUICollectionUnitInfo.eCollectionState.CS_PROFILE, NKCUIUpsideMenu.eMode upsideMenuMode = NKCUIUpsideMenu.eMode.Normal)
		{
			this.m_eCurrentState = NKCUICollectionUnitInfo.eCollectionState.CS_NONE;
			this.m_eCurrentTagSortType = NKCUICollectionUnitInfo.eTagSortOption.TS_TAG_NORMAL;
			this.m_UpsideMenuMode = upsideMenuMode;
			this.m_listNKMEquipItemData = listNKMEquipItemData;
			this.m_OpenOption = openOption;
			if (this.m_OpenOption == null)
			{
				this.m_OpenOption = new NKCUIUnitInfo.OpenOption(new List<long>(), 0);
				this.m_OpenOption.m_lstUnitData.Add(cNKMUnitData);
			}
			if (this.m_DragCharacterView != null)
			{
				if (this.m_OpenOption.m_lstUnitData.Count == 0)
				{
					this.m_OpenOption.m_lstUnitData.Add(cNKMUnitData);
				}
				this.m_DragCharacterView.TotalCount = this.m_OpenOption.m_lstUnitData.Count;
				for (int i = 0; i < this.m_OpenOption.m_lstUnitData.Count; i++)
				{
					if (this.m_OpenOption.m_lstUnitData[i].m_UnitUID == cNKMUnitData.m_UnitUID)
					{
						this.m_DragCharacterView.SetIndex(i);
						break;
					}
				}
			}
			this.SetData(cNKMUnitData);
			this.ChangeState(eStartingState);
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			base.UIOpened(true);
		}

		// Token: 0x060090A9 RID: 37033 RVA: 0x003147A0 File Offset: 0x003129A0
		private void ChangeState(NKCUICollectionUnitInfo.eCollectionState newStat)
		{
			if (this.m_eCurrentState == newStat || this.m_bViewMode)
			{
				return;
			}
			this.m_eCurrentState = newStat;
			this.UpdateUI();
			if (NKCUICollectionUnitInfo.eCollectionState.CS_TAG == this.m_eCurrentState)
			{
				this.UpdateTagInfo(this.m_UnitID);
			}
		}

		// Token: 0x060090AA RID: 37034 RVA: 0x003147D6 File Offset: 0x003129D6
		private void ShowTag()
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_INFO_CONTENT_USER_TAG, true);
			this.UpdateTagInfo(this.m_UnitID);
		}

		// Token: 0x060090AB RID: 37035 RVA: 0x003147F0 File Offset: 0x003129F0
		private void UpdateUI()
		{
			switch (this.m_eCurrentState)
			{
			case NKCUICollectionUnitInfo.eCollectionState.CS_PROFILE:
				NKCUtil.SetGameobjectActive(this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_PROFILE.gameObject, true);
				NKCUtil.SetGameobjectActive(this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_STAT.gameObject, true);
				NKCUtil.SetGameobjectActive(this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_TAG.gameObject, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_PROFILE, true);
				this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_PROFILE.Select(true, false, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_STAT, false);
				this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_STAT.Select(false, false, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_TAG, false);
				this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_TAG.Select(false, false, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_INFO_CONTENT_USER_TAG, false);
				return;
			case NKCUICollectionUnitInfo.eCollectionState.CS_STATUS:
				NKCUtil.SetGameobjectActive(this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_PROFILE.gameObject, true);
				NKCUtil.SetGameobjectActive(this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_STAT.gameObject, true);
				NKCUtil.SetGameobjectActive(this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_TAG.gameObject, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_PROFILE, false);
				this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_PROFILE.Select(false, false, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_STAT, true);
				this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_STAT.Select(true, false, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_TAG, false);
				this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_TAG.Select(false, false, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_INFO_CONTENT_USER_TAG, false);
				return;
			case NKCUICollectionUnitInfo.eCollectionState.CS_TAG:
				NKCUtil.SetGameobjectActive(this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_PROFILE.gameObject, true);
				NKCUtil.SetGameobjectActive(this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_STAT.gameObject, true);
				NKCUtil.SetGameobjectActive(this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_TAG.gameObject, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_PROFILE, false);
				this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_PROFILE.Select(false, false, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_STAT, false);
				this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_STAT.Select(false, false, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_TAG, true);
				this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_TAG.Select(true, false, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_INFO_CONTENT_USER_TAG, false);
				return;
			default:
				return;
			}
		}

		// Token: 0x060090AC RID: 37036 RVA: 0x003149CC File Offset: 0x00312BCC
		private void SetData(NKMUnitData unitData)
		{
			this.OpenSDIllust(unitData, 0);
			this.m_UISkillPanel.SetData(unitData, true);
			if (this.m_NKMUnitData != null && this.m_UnitID == unitData.m_UnitID)
			{
				return;
			}
			if (this.m_NKMUnitData == null)
			{
				this.m_NKMUnitData = new NKMUnitData();
			}
			this.m_NKMUnitData.DeepCopyFrom(unitData);
			this.m_UnitID = unitData.m_UnitID;
			this.m_SkinID = 0;
			NKCUtil.SetGameobjectActive(this.m_UISkinPanel, true);
			this.m_UISkinPanel.SetData(this.m_NKMUnitData, true);
			this.SetUnitDiscription(this.m_UnitID);
			this.SetDetailedStat(this.m_NKMUnitData);
			this.m_NKCUICharInfoSummary.SetData(this.m_NKMUnitData);
			this.CheckHasUnit(this.m_UnitID);
			this.SetVoiceButtonUI();
			this.SetLifetimeButtonUI(this.m_UnitID);
			if (this.m_listNKMEquipItemData != null || NKCUICollectionUnitInfo.m_isGauntlet)
			{
				NKCUtil.SetGameobjectActive(this.m_objEquipSlotParent, true);
				this.UpdateEquipSlots();
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_INFO_UNIT_SD, false);
				NKCUtil.SetGameobjectActive(this.m_UISkinPanel, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_INFO_BOTTOM_BUTTON_PRACTICE, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_INFO_BOTTOM_BUTTON_APPRAISAL, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_PROFILE_TAG_VIEW_ALL_BUTTON, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_PROFILE_VOICE_BUTTON, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_PROFILE_VOICE_BUTTON_DISABLE, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_INFO_LOYALTY, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_INFO_NOTICE_NOTGET, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_ACHIEVEMENT_BUTTON, false);
				NKCUtil.SetGameobjectActive(this.m_objVoiceActor, false);
				NKCUtil.SetGameobjectActive(this.m_cbtnUnitInfoDetailPopup, true);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objEquipSlotParent, false);
			}
			this.UpdateUnitMissionRedDot();
			if (NKCUIPopupCollectionAchievement.IsInstanceOpen)
			{
				NKCUIPopupCollectionAchievement.Instance.Open(this.m_UnitID);
			}
			NKCUtil.SetLabelText(this.m_lbVoiceActorName, NKCVoiceActorNameTemplet.FindActorName(unitData));
		}

		// Token: 0x060090AD RID: 37037 RVA: 0x00314B8C File Offset: 0x00312D8C
		private void CheckHasUnit(int iUnitID)
		{
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_INFO_NOTICE_NOTGET, armyData.IsFirstGetUnit(iUnitID));
		}

		// Token: 0x060090AE RID: 37038 RVA: 0x00314BB8 File Offset: 0x00312DB8
		private void SetUnitDiscription(int unitID)
		{
			NKCCollectionUnitTemplet unitTemplet = NKCCollectionManager.GetUnitTemplet(unitID);
			if (unitTemplet != null)
			{
				if (this.m_srUnitIntroduce != null)
				{
					this.m_srUnitIntroduce.verticalNormalizedPosition = 1f;
				}
				NKCUtil.SetLabelText(this.m_NKM_UI_COLLECTION_UNIT_PROFILE_UNIT_INTRODUCE_TEXT, unitTemplet.GetUnitIntro());
			}
		}

		// Token: 0x060090AF RID: 37039 RVA: 0x00314C00 File Offset: 0x00312E00
		private void SetDetailedStat(NKMUnitData unitData)
		{
			NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(unitData.m_UnitID);
			bool flag = false;
			NKMStatData nkmstatData = new NKMStatData();
			nkmstatData.Init();
			nkmstatData.MakeBaseStat(null, flag, unitData, unitStatTemplet.m_StatData, false, 0, null);
			if (this.m_listNKMEquipItemData != null)
			{
				NKMInventoryData nkminventoryData = new NKMInventoryData();
				nkminventoryData.AddItemEquip(this.m_listNKMEquipItemData);
				nkmstatData.MakeBaseBonusFactor(unitData, nkminventoryData.EquipItems, null, null, flag);
				NKCUtil.SetLabelText(this.m_NKM_UI_COLLECTION_UNIT_STAT_SUMMARY_POWER_TEXT, unitData.CalculateOperationPower(nkminventoryData, 0, null, null).ToString());
			}
			else
			{
				nkmstatData.MakeBaseBonusFactor(unitData, null, null, null, flag);
				NKCUtil.SetLabelText(this.m_NKM_UI_COLLECTION_UNIT_STAT_SUMMARY_POWER_TEXT, unitData.CalculateOperationPower(NKCScenManager.CurrentUserData().m_InventoryData, 0, null, null).ToString());
			}
			this.m_slotHP.SetStat(NKM_STAT_TYPE.NST_HP, nkmstatData, unitData);
			this.m_slotAttack.SetStat(NKM_STAT_TYPE.NST_ATK, nkmstatData, unitData);
			this.m_slotDefense.SetStat(NKM_STAT_TYPE.NST_DEF, nkmstatData, unitData);
			this.m_slotHitRate.SetStat(NKM_STAT_TYPE.NST_HIT, nkmstatData, unitData);
			this.m_slotCritHitRate.SetStat(NKM_STAT_TYPE.NST_CRITICAL, nkmstatData, unitData);
			this.m_slotEvade.SetStat(NKM_STAT_TYPE.NST_EVADE, nkmstatData, unitData);
		}

		// Token: 0x060090B0 RID: 37040 RVA: 0x00314D09 File Offset: 0x00312F09
		public override void OnBackButton()
		{
			if (this.m_bViewMode)
			{
				this.OnClickChangeIllust();
				return;
			}
			base.OnBackButton();
		}

		// Token: 0x060090B1 RID: 37041 RVA: 0x00314D20 File Offset: 0x00312F20
		public override void UnHide()
		{
			this.m_bAppraisal = false;
			base.UnHide();
		}

		// Token: 0x060090B2 RID: 37042 RVA: 0x00314D30 File Offset: 0x00312F30
		public override void CloseInternal()
		{
			this.BannerCleanUp();
			this.m_UISkinPanel.CleanUp();
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
			this.m_eCurrentTagSortType = NKCUICollectionUnitInfo.eTagSortOption.TS_NONE;
			if (this.m_spineSD != null)
			{
				this.m_spineSD.Unload();
				this.m_spineSD = null;
			}
			NKCPopupUnitInfoDetail.CheckInstanceAndClose();
			NKCUIPopupIllustView.CheckInstanceAndClose();
			NKCUIPopupCollectionAchievement.CheckInstanceAndClose();
			this.m_NKMUnitData = null;
			this.m_UnitID = 0;
			this.m_SkinID = 0;
		}

		// Token: 0x060090B3 RID: 37043 RVA: 0x00314DAC File Offset: 0x00312FAC
		private void OnUnitTestButton()
		{
			if (this.m_bViewMode)
			{
				return;
			}
			NKM_SHORTCUT_TYPE shortcutType;
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_COLLECTION)
			{
				shortcutType = NKCScenManager.GetScenManager().Get_NKC_SCEN_COLLECTION().GetCurrentShortcutType();
			}
			else
			{
				shortcutType = NKM_SHORTCUT_TYPE.SHORTCUT_NONE;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_UnitID);
			string shortcutParam = (unitTempletBase != null) ? unitTempletBase.m_UnitStrID : "";
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_COLLECTION_TRAINING_MODE_CHANGE_REQ, delegate()
			{
				NKCScenManager.GetScenManager().Get_SCEN_GAME().PlayPracticeGame(this.m_NKMUnitData, shortcutType, shortcutParam);
			}, null, false);
		}

		// Token: 0x060090B4 RID: 37044 RVA: 0x00314E3A File Offset: 0x0031303A
		private void OnUnitAppraisal()
		{
			if (this.m_bViewMode)
			{
				return;
			}
			NKCUIUnitReview.Instance.OpenUI(this.m_UnitID);
			this.m_bAppraisal = true;
		}

		// Token: 0x060090B5 RID: 37045 RVA: 0x00314E5C File Offset: 0x0031305C
		private void OnClickChangeIllust()
		{
			if (this.m_bAppraisal)
			{
				return;
			}
			NKCUIPopupIllustView.Instance.Open(this.m_NKMUnitData);
		}

		// Token: 0x060090B6 RID: 37046 RVA: 0x00314E78 File Offset: 0x00313078
		public void ChangeSkin(int skinID)
		{
			if (this.m_OpenOption != null && this.m_DragCharacterView != null && this.m_DragCharacterView.CurrentIndex >= 0 && this.m_OpenOption.m_lstUnitData.Count > this.m_DragCharacterView.CurrentIndex)
			{
				NKMUnitData nkmunitData = this.m_OpenOption.m_lstUnitData[this.m_DragCharacterView.CurrentIndex];
				if (nkmunitData != null)
				{
					if (skinID != 0)
					{
						NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(skinID);
						if (!NKMSkinManager.IsSkinForCharacter(nkmunitData.m_UnitID, skinTemplet))
						{
							return;
						}
					}
					if (this.m_DragCharacterView.GetCurrentItem() != null)
					{
						NKCUICharacterView componentInChildren = this.m_DragCharacterView.GetCurrentItem().GetComponentInChildren<NKCUICharacterView>();
						if (componentInChildren != null)
						{
							componentInChildren.CloseImmediatelyIllust();
							this.m_SkinID = skinID;
							this.m_NKMUnitData.m_SkinID = skinID;
							componentInChildren.SetCharacterIllust(nkmunitData.m_UnitID, skinID, false, true, 0);
							this.OpenSDIllust(nkmunitData, skinID);
						}
					}
					NKCUtil.SetLabelText(this.m_lbVoiceActorName, NKCVoiceActorNameTemplet.FindActorName(this.m_NKMUnitData));
				}
			}
		}

		// Token: 0x060090B7 RID: 37047 RVA: 0x00314F84 File Offset: 0x00313184
		private void OpenSDIllust(NKMUnitData unitData, int skinID)
		{
			if (unitData == null)
			{
				NKCUtil.SetGameobjectActive(this.m_rtSDRoot, false);
				return;
			}
			if (this.m_spineSD != null)
			{
				this.m_spineSD.Unload();
				this.m_spineSD = null;
			}
			this.m_spineSD = NKCResourceUtility.OpenSpineSD(unitData.m_UnitID, skinID, false);
			if (this.m_spineSD != null)
			{
				this.m_spineSD.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, true, false, 0f);
				this.m_spineSD.SetAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, true, 0, true, 0f, true);
				this.m_spineSD.SetParent(this.m_rtSDRoot, false);
				RectTransform rectTransform = this.m_spineSD.GetRectTransform();
				if (rectTransform != null)
				{
					rectTransform.localPosition = Vector3.zero;
					rectTransform.localScale = Vector3.one * this.m_fSDScale;
				}
				NKCUtil.SetGameobjectActive(this.m_rtSDRoot, true);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_rtSDRoot, false);
		}

		// Token: 0x060090B8 RID: 37048 RVA: 0x0031506C File Offset: 0x0031326C
		private void InitTag()
		{
			if (null != this.m_cbtn_NKM_UI_COLLECTION_UNIT_INFO_USER_TAG_BG)
			{
				this.m_cbtn_NKM_UI_COLLECTION_UNIT_INFO_USER_TAG_BG.OnValueChanged.RemoveAllListeners();
				this.m_cbtn_NKM_UI_COLLECTION_UNIT_INFO_USER_TAG_BG.OnValueChanged.AddListener(new UnityAction<bool>(this.OnSortMenuOpen));
			}
			if (null != this.m_cbtnSortTypeNormal)
			{
				this.m_cbtnSortTypeNormal.PointerClick.RemoveAllListeners();
				this.m_cbtnSortTypeNormal.PointerClick.AddListener(delegate()
				{
					this.SetSort(NKCUICollectionUnitInfo.eTagSortOption.TS_TAG_NORMAL);
				});
			}
			if (null != this.m_cbtnSortTypeVote)
			{
				this.m_cbtnSortTypeVote.PointerClick.RemoveAllListeners();
				this.m_cbtnSortTypeVote.PointerClick.AddListener(delegate()
				{
					this.SetSort(NKCUICollectionUnitInfo.eTagSortOption.TS_TAG_VOTE);
				});
			}
			if (null != this.m_LoopScrollRect)
			{
				this.m_LoopScrollRect.dOnGetObject += this.GetSlot;
				this.m_LoopScrollRect.dOnReturnObject += this.ReturnSlot;
				this.m_LoopScrollRect.dOnProvideData += this.ProvideSlotData;
				this.m_LoopScrollRect.dOnRepopulate += this.CalculateContentRectSize;
			}
		}

		// Token: 0x060090B9 RID: 37049 RVA: 0x00315191 File Offset: 0x00313391
		private void CalculateContentRectSize()
		{
			NKCUtil.CalculateContentRectSize(this.m_LoopScrollRect, this.m_GridLayoutGroup, this.minColumnTag, this.m_vTagSlotSize, this.m_vTagSlotSpacing, false);
		}

		// Token: 0x060090BA RID: 37050 RVA: 0x003151B8 File Offset: 0x003133B8
		private RectTransform GetSlot(int index)
		{
			Stack<NKCUICollectionTagSlot> stkTagSlotPool = this.m_stkTagSlotPool;
			NKCUICollectionTagSlot pfbTagSlot = this.m_pfbTagSlot;
			if (stkTagSlotPool.Count > 0)
			{
				NKCUICollectionTagSlot nkcuicollectionTagSlot = stkTagSlotPool.Pop();
				NKCUtil.SetGameobjectActive(nkcuicollectionTagSlot, true);
				nkcuicollectionTagSlot.transform.localScale = Vector3.one;
				this.m_lstVisibleSlot.Add(nkcuicollectionTagSlot);
				return nkcuicollectionTagSlot.GetComponent<RectTransform>();
			}
			NKCUICollectionTagSlot nkcuicollectionTagSlot2 = UnityEngine.Object.Instantiate<NKCUICollectionTagSlot>(pfbTagSlot);
			NKCUtil.SetGameobjectActive(nkcuicollectionTagSlot2, true);
			nkcuicollectionTagSlot2.transform.localScale = Vector3.one;
			this.m_lstVisibleSlot.Add(nkcuicollectionTagSlot2);
			return nkcuicollectionTagSlot2.GetComponent<RectTransform>();
		}

		// Token: 0x060090BB RID: 37051 RVA: 0x00315240 File Offset: 0x00313440
		private void ReturnSlot(Transform go)
		{
			NKCUICollectionTagSlot component = go.GetComponent<NKCUICollectionTagSlot>();
			if (component == null)
			{
				return;
			}
			this.m_lstVisibleSlot.Remove(component);
			go.SetParent(this.m_rectSlotPoolRect);
			this.m_stkTagSlotPool.Push(component);
		}

		// Token: 0x060090BC RID: 37052 RVA: 0x00315284 File Offset: 0x00313484
		private void ProvideSlotData(Transform tr, int idx)
		{
			NKCUICollectionTagSlot component = tr.GetComponent<NKCUICollectionTagSlot>();
			if (component == null)
			{
				return;
			}
			NKCUnitTagData nkcunitTagData = this.m_lst_CurTagData[idx];
			if (nkcunitTagData != null)
			{
				component.SetData(new NKCUICollectionTagSlot.OnSelected(this.tagClick), nkcunitTagData.TagType, idx, nkcunitTagData.Voted, NKCCollectionManager.GetTagTitle(nkcunitTagData.TagType), nkcunitTagData.VoteCount, nkcunitTagData.IsTop);
			}
		}

		// Token: 0x060090BD RID: 37053 RVA: 0x003152E8 File Offset: 0x003134E8
		private string GetSortText(NKCUICollectionUnitInfo.eTagSortOption sortType)
		{
			if (sortType == NKCUICollectionUnitInfo.eTagSortOption.TS_TAG_NORMAL)
			{
				return NKCUtilString.GET_STRING_NORMAL;
			}
			if (sortType != NKCUICollectionUnitInfo.eTagSortOption.TS_TAG_VOTE)
			{
				return "";
			}
			return NKCUtilString.GET_STRING_VOTE;
		}

		// Token: 0x060090BE RID: 37054 RVA: 0x00315305 File Offset: 0x00313505
		private void SetSort(NKCUICollectionUnitInfo.eTagSortOption newSort)
		{
			this.OnSortMenuOpen(false);
			if (this.SortTagContent(newSort))
			{
				this.UpdateTagInfo(this.m_UnitID);
			}
		}

		// Token: 0x060090BF RID: 37055 RVA: 0x00315324 File Offset: 0x00313524
		private bool SortTagContent(NKCUICollectionUnitInfo.eTagSortOption newSort)
		{
			if (this.m_eCurrentTagSortType == newSort && this.m_CurUnitID == this.m_NKMUnitData.m_UnitID)
			{
				return false;
			}
			List<NKCUnitTagData> unitTagData = NKCCollectionManager.GetUnitTagData(this.m_CurUnitID);
			if (unitTagData == null)
			{
				return false;
			}
			if (newSort == NKCUICollectionUnitInfo.eTagSortOption.TS_TAG_NORMAL)
			{
				unitTagData.Sort(new Comparison<NKCUnitTagData>(this.TagCompareAscending));
			}
			else if (newSort == NKCUICollectionUnitInfo.eTagSortOption.TS_TAG_VOTE)
			{
				unitTagData.Sort(new Comparison<NKCUnitTagData>(this.VoteCompareDescending));
			}
			this.m_eCurrentTagSortType = newSort;
			NKCUtil.SetLabelText(this.m_NKM_UI_COLLECTION_UNIT_INFO_USER_TAGS_SORT_TYPE_TEXT, this.GetSortText(this.m_eCurrentTagSortType));
			return true;
		}

		// Token: 0x060090C0 RID: 37056 RVA: 0x003153B0 File Offset: 0x003135B0
		private int TagCompareAscending(NKCUnitTagData a, NKCUnitTagData b)
		{
			return a.TagType.CompareTo(b.TagType);
		}

		// Token: 0x060090C1 RID: 37057 RVA: 0x003153D1 File Offset: 0x003135D1
		private int VoteCompareDescending(NKCUnitTagData a, NKCUnitTagData b)
		{
			return b.VoteCount.CompareTo(a.VoteCount);
		}

		// Token: 0x060090C2 RID: 37058 RVA: 0x003153E4 File Offset: 0x003135E4
		private void OnSortMenuOpen(bool bOpen)
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_INFO_USER_TAG_OPEN, bOpen);
		}

		// Token: 0x060090C3 RID: 37059 RVA: 0x003153F2 File Offset: 0x003135F2
		private void PrepareTagList()
		{
			if (!this.m_bCellPrepared)
			{
				this.m_bCellPrepared = true;
				this.CalculateContentRectSize();
				this.m_LoopScrollRect.PrepareCells(0);
			}
		}

		// Token: 0x060090C4 RID: 37060 RVA: 0x00315415 File Offset: 0x00313615
		private void UpdateTagInfo(int unitID)
		{
			this.m_CurUnitID = unitID;
			if (NKCCollectionManager.GetUnitTagData(this.m_CurUnitID) == null)
			{
				NKCPacketSender.Send_NKMPacket_UNIT_REVIEW_TAG_LIST_REQ(this.m_CurUnitID);
				return;
			}
			this.UpdateTagData(this.m_CurUnitID, true);
		}

		// Token: 0x060090C5 RID: 37061 RVA: 0x00315444 File Offset: 0x00313644
		private void UpdateTagData(int unitID, bool bResetScroll = true)
		{
			this.m_lst_CurTagData = NKCCollectionManager.GetUnitTagData(unitID);
			if (this.m_lst_CurTagData == null)
			{
				return;
			}
			if (this.m_eCurrentState == NKCUICollectionUnitInfo.eCollectionState.CS_TAG)
			{
				this.m_iCurVotedCount = 0;
				this.m_eOldTagSortType = this.m_eCurrentTagSortType;
				this.SortTagContent(NKCUICollectionUnitInfo.eTagSortOption.TS_TAG_VOTE);
				int num = 6;
				for (int i = 0; i < this.m_lst_CurTagData.Count; i++)
				{
					if (this.m_lst_CurTagData[i].VoteCount > 0 && num > i)
					{
						this.m_lst_CurTagData[i].IsTop = true;
					}
					else
					{
						this.m_lst_CurTagData[i].IsTop = false;
					}
				}
				for (int j = 0; j < this.m_lst_CurTagData.Count; j++)
				{
					if (j < this.tagRank.Count)
					{
						if (this.m_lst_CurTagData[j].VoteCount > 0)
						{
							NKCUtil.SetLabelText(this.tagRank[j].tagTigle, NKCCollectionManager.GetTagTitle(this.m_lst_CurTagData[j].TagType));
							NKCUtil.SetLabelText(this.tagRank[j].tagCount, this.m_lst_CurTagData[j].VoteCount.ToString());
							NKCUtil.SetGameobjectActive(this.tagRank[j].m_tagObj, true);
						}
						else
						{
							NKCUtil.SetGameobjectActive(this.tagRank[j].m_tagObj, false);
						}
					}
					if (this.m_lst_CurTagData[j].Voted)
					{
						this.m_iCurVotedCount++;
					}
				}
				if (this.m_lst_CurTagData[0] != null)
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_PROFILE_USER_TAG, this.m_lst_CurTagData[0].VoteCount > 0);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_PROFILE_USER_TAG_CAUTION, this.m_lst_CurTagData[0].VoteCount <= 0);
				}
				NKCUtil.SetLabelText(this.m_USER_TAG_MY_OPINION_COUNT_TEXT, string.Format("{0}/{1}", this.m_iCurVotedCount, 7));
				this.SortTagContent(this.m_eOldTagSortType);
				this.PrepareTagList();
				this.m_LoopScrollRect.TotalCount = this.m_lst_CurTagData.Count;
				if (bResetScroll)
				{
					this.m_LoopScrollRect.SetIndexPosition(0);
					return;
				}
				this.m_LoopScrollRect.RefreshCells(false);
			}
		}

		// Token: 0x060090C6 RID: 37062 RVA: 0x00315687 File Offset: 0x00313887
		private void OnCloseTagList()
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_INFO_CONTENT_USER_TAG, false);
			this.ChangeState(NKCUICollectionUnitInfo.eCollectionState.CS_TAG);
		}

		// Token: 0x060090C7 RID: 37063 RVA: 0x0031569C File Offset: 0x0031389C
		public void tagClick(short tagType, int slotIdx)
		{
			if (NKCCollectionManager.IsVoted(this.m_CurUnitID, tagType))
			{
				NKCPacketSender.Send_NKMPacket_UNIT_REVIEW_TAG_VOTE_CANCEL_REQ(this.m_CurUnitID, tagType);
			}
			else
			{
				if (this.m_iCurVotedCount >= 7)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_COLLECTION_ALLOWED_RANGE_VOTE_COMPLETE, null, "");
				}
				NKCPacketSender.Send_NKMPacket_UNIT_REVIEW_TAG_VOTE_REQ(this.m_CurUnitID, tagType);
			}
			this.m_iClickSlotIdx = slotIdx;
		}

		// Token: 0x060090C8 RID: 37064 RVA: 0x003156F6 File Offset: 0x003138F6
		public void OnRecvReviewTagVoteCancelAck(NKMPacket_UNIT_REVIEW_TAG_VOTE_CANCEL_ACK sPacket)
		{
			this.UpdateMyVoteCount(sPacket.unitID);
			this.UpdateTagData(sPacket.unitID, true);
		}

		// Token: 0x060090C9 RID: 37065 RVA: 0x00315711 File Offset: 0x00313911
		public void OnRecvReviewTagVoteAck(NKMPacket_UNIT_REVIEW_TAG_VOTE_ACK sPacket)
		{
			this.UpdateMyVoteCount(sPacket.unitID);
			this.UpdateTagData(sPacket.unitID, false);
		}

		// Token: 0x060090CA RID: 37066 RVA: 0x0031572C File Offset: 0x0031392C
		public void OnRecvReviewTagListAck(NKMPacket_UNIT_REVIEW_TAG_LIST_ACK sPacket)
		{
			List<NKMUnitReviewTagData> unitReviewTagDataList = sPacket.unitReviewTagDataList;
			List<NKCUnitTagData> list = new List<NKCUnitTagData>();
			for (int i = 0; i < unitReviewTagDataList.Count; i++)
			{
				NKCUnitTagData item = new NKCUnitTagData(unitReviewTagDataList[i].tagType, unitReviewTagDataList[i].isVoted, unitReviewTagDataList[i].votedCount, false);
				if (unitReviewTagDataList[i].isVoted)
				{
					this.m_iCurVotedCount++;
				}
				list.Add(item);
			}
			NKCUtil.SetLabelText(this.m_USER_TAG_MY_OPINION_COUNT_TEXT, string.Format("{0}/{1}", this.m_iCurVotedCount, 7));
			NKCCollectionManager.SetUnitTagData(this.m_CurUnitID, list);
			this.SortTagContent(this.m_eOldTagSortType);
			this.UpdateTagInfo(this.m_CurUnitID);
		}

		// Token: 0x060090CB RID: 37067 RVA: 0x003157F0 File Offset: 0x003139F0
		private void UpdateMyVoteCount(int unitID)
		{
			this.m_iCurVotedCount = 0;
			List<NKCUnitTagData> unitTagData = NKCCollectionManager.GetUnitTagData(unitID);
			if (unitTagData != null)
			{
				for (int i = 0; i < unitTagData.Count; i++)
				{
					if (unitTagData[i].Voted)
					{
						this.m_iCurVotedCount++;
					}
				}
			}
			NKCUtil.SetLabelText(this.m_USER_TAG_MY_OPINION_COUNT_TEXT, string.Format("{0}/{1}", this.m_iCurVotedCount, 7));
		}

		// Token: 0x060090CC RID: 37068 RVA: 0x00315864 File Offset: 0x00313A64
		private void SetVoiceButtonUI()
		{
			bool flag = false;
			if (this.m_NKMUnitData != null && NKCUIVoiceManager.UnitHasVoice(NKMUnitManager.GetUnitTempletBase(this.m_UnitID)))
			{
				flag = true;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_PROFILE_VOICE_BUTTON, flag);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_PROFILE_VOICE_BUTTON_DISABLE, !flag);
		}

		// Token: 0x060090CD RID: 37069 RVA: 0x003158AC File Offset: 0x00313AAC
		private void OnUnitVoice()
		{
			if (this.m_NKMUnitData == null)
			{
				return;
			}
			bool bLifetime = false;
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			if (armyData.IsCollectedUnit(this.m_UnitID))
			{
				bLifetime = armyData.SearchUnitByID(NKM_UNIT_TYPE.NUT_NORMAL, this.m_CurUnitID, NKMArmyData.UNIT_SEARCH_OPTION.Devotion, 0);
			}
			if (this.m_SkinID > 0)
			{
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(this.m_SkinID);
				NKCUIPopupVoice.Instance.Open(skinTemplet, bLifetime);
				return;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_UnitID);
			NKCUIPopupVoice.Instance.Open(unitTempletBase, bLifetime);
		}

		// Token: 0x060090CE RID: 37070 RVA: 0x00315928 File Offset: 0x00313B28
		private void SetLifetimeButtonUI(int unitID)
		{
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			bool bValue = false;
			if (armyData.IsCollectedUnit(unitID))
			{
				bValue = armyData.SearchUnitByID(NKM_UNIT_TYPE.NUT_NORMAL, unitID, NKMArmyData.UNIT_SEARCH_OPTION.Devotion, 0);
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_INFO_LOYALTY, bValue);
		}

		// Token: 0x060090CF RID: 37071 RVA: 0x00315962 File Offset: 0x00313B62
		private void OnReplayLifetime()
		{
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_LIFETIME_REPLAY, delegate()
			{
				NKCUILifetime.Instance.Open(this.m_NKMUnitData, true);
			}, null, false);
		}

		// Token: 0x060090D0 RID: 37072 RVA: 0x00315981 File Offset: 0x00313B81
		public void OpenUnitInfoDetailPopup()
		{
			if (NKCPopupUnitInfoDetail.IsInstanceOpen)
			{
				NKCPopupUnitInfoDetail.CheckInstanceAndClose();
				return;
			}
			NKCPopupUnitInfoDetail.InstanceOpen(this.m_NKMUnitData, NKCPopupUnitInfoDetail.UnitInfoDetailType.gauntlet, this.m_listNKMEquipItemData);
		}

		// Token: 0x060090D1 RID: 37073 RVA: 0x003159A4 File Offset: 0x00313BA4
		public void UpdateEquipSlots()
		{
			if (this.m_NKMUnitData != null)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_UnitID);
				if (unitTempletBase != null && unitTempletBase.m_NKM_UNIT_STYLE_TYPE == NKM_UNIT_STYLE_TYPE.NUST_TRAINER)
				{
					NKCUtil.SetGameobjectActive(this.m_objEquipSlotParent, false);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objEquipSlotParent, true);
					this.UpdateEquipItemSlot(this.m_NKMUnitData.GetEquipItemWeaponUid(), ref this.m_slotEquipWeapon, null, this.m_NKM_UI_UNIT_INFO_EQUIP_SET_FX_WEAPON, this.m_ani_NKM_UI_UNIT_INFO_EQUIP_SET_FX_WEAPON);
					this.UpdateEquipItemSlot(this.m_NKMUnitData.GetEquipItemDefenceUid(), ref this.m_slotEquipDefense, null, this.m_NKM_UI_UNIT_INFO_EQUIP_SET_FX_DEFENCE, this.m_ani_NKM_UI_UNIT_INFO_EQUIP_SET_FX_DEFENCE);
					this.UpdateEquipItemSlot(this.m_NKMUnitData.GetEquipItemAccessoryUid(), ref this.m_slotEquipAcc, null, this.m_NKM_UI_UNIT_INFO_EQUIP_SET_FX_ACC, this.m_ani_NKM_UI_UNIT_INFO_EQUIP_SET_FX_ACC);
					this.UpdateEquipItemSlot(this.m_NKMUnitData.GetEquipItemAccessory2Uid(), ref this.m_slotEquipAcc_2, null, this.m_NKM_UI_UNIT_INFO_EQUIP_SET_FX_ACC_02, this.m_ani_NKM_UI_UNIT_INFO_EQUIP_SET_FX_ACC_02);
				}
				if (!this.m_NKMUnitData.IsUnlockAccessory2())
				{
					this.m_slotEquipAcc_2.SetLock(new NKCUISlot.OnClick(this.OnSetLockMessage));
					this.m_slotEquipAcc_2.Set_EQUIP_BOX_BOTTOM_MENU_TYPE(NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_NONE);
				}
				this.SetDetailedStat(this.m_NKMUnitData);
				return;
			}
			this.m_slotEquipAcc_2.SetLock(new NKCUISlot.OnClick(this.OnSetLockMessage));
			this.m_slotEquipAcc_2.Set_EQUIP_BOX_BOTTOM_MENU_TYPE(NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_NONE);
		}

		// Token: 0x060090D2 RID: 37074 RVA: 0x00315AE1 File Offset: 0x00313CE1
		private void OnSetLockMessage(NKCUISlot.SlotData slotData, bool bLocked)
		{
			NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_EQUIP_ACC_2_LOCKED_DESC, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
		}

		// Token: 0x060090D3 RID: 37075 RVA: 0x00315AF8 File Offset: 0x00313CF8
		private NKMEquipItemData GetItemEquip(long itemUID)
		{
			if (this.m_listNKMEquipItemData != null)
			{
				return this.m_listNKMEquipItemData.Find((NKMEquipItemData x) => x.m_ItemUid == itemUID);
			}
			return null;
		}

		// Token: 0x060090D4 RID: 37076 RVA: 0x00315B34 File Offset: 0x00313D34
		private void UpdateEquipItemSlot(long equipItemUID, ref NKCUISlot slot, NKCUISlot.OnClick func, GameObject effObj, Animator effAni)
		{
			bool flag = false;
			NKCUtil.SetGameobjectActive(effObj, true);
			if (equipItemUID > 0L)
			{
				NKMEquipItemData itemEquip = this.GetItemEquip(equipItemUID);
				if (itemEquip != null)
				{
					slot.SetData(NKCUISlot.SlotData.MakeEquipData(itemEquip), false, true, false, new NKCUISlot.OnClick(this.OpenEquipBoxForInspection));
					flag = true;
					if (NKMItemManager.IsActiveSetOptionItem(itemEquip) && effAni != null)
					{
						NKMItemEquipSetOptionTemplet equipSetOptionTemplet = NKMItemManager.GetEquipSetOptionTemplet(itemEquip.m_SetOptionId);
						if (equipSetOptionTemplet != null)
						{
							effAni.SetTrigger(equipSetOptionTemplet.m_EquipSetIconEffect);
						}
					}
				}
			}
			NKCUtil.SetGameobjectActive(effObj, false);
			if (!flag)
			{
				slot.SetCustomizedEmptySP(this.GetCustomizedEquipEmptySP());
				slot.SetEmpty(func);
			}
			slot.SetUsedMark(false);
		}

		// Token: 0x060090D5 RID: 37077 RVA: 0x00315BD0 File Offset: 0x00313DD0
		private Sprite GetCustomizedEquipEmptySP()
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_NKMUnitData.m_UnitID);
			if (unitTempletBase == null)
			{
				return null;
			}
			NKM_UNIT_STYLE_TYPE nkm_UNIT_STYLE_TYPE = unitTempletBase.m_NKM_UNIT_STYLE_TYPE;
			if (nkm_UNIT_STYLE_TYPE - NKM_UNIT_STYLE_TYPE.NUST_COUNTER <= 2)
			{
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_UNIT_INFO_SPRITE", "NKM_UI_UNIT_INFO_ITEM_EQUIP_SLOT_ADD", false);
			}
			return null;
		}

		// Token: 0x060090D6 RID: 37078 RVA: 0x00315C14 File Offset: 0x00313E14
		private void OpenEquipBoxForInspection(NKCUISlot.SlotData slotData, bool bLocked)
		{
			NKMEquipItemData itemEquip = this.GetItemEquip(slotData.UID);
			if (itemEquip != null)
			{
				NKCPopupItemEquipBox.Open(itemEquip, NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_NONE, null);
			}
		}

		// Token: 0x060090D7 RID: 37079 RVA: 0x00315C39 File Offset: 0x00313E39
		public void UpdateUnitMissionRedDot()
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_ACHIEVEMENT_BUTTON_NEW, NKCUnitMissionManager.HasRewardEnableMission(this.m_UnitID));
		}

		// Token: 0x060090D8 RID: 37080 RVA: 0x00315C51 File Offset: 0x00313E51
		private void OnClickUnitAchievement()
		{
			NKCUIPopupCollectionAchievement.Instance.Open(this.m_UnitID);
		}

		// Token: 0x04007DBE RID: 32190
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_collection";

		// Token: 0x04007DBF RID: 32191
		private const string UI_ASSET_NAME = "NKM_UI_COLLECTION_UNIT_INFO";

		// Token: 0x04007DC0 RID: 32192
		private const string UI_ASSET_NAME_OTHER = "NKM_UI_COLLECTION_UNIT_INFO_OTHER";

		// Token: 0x04007DC1 RID: 32193
		private static bool m_isGauntlet;

		// Token: 0x04007DC2 RID: 32194
		private static NKCUICollectionUnitInfo m_Instance;

		// Token: 0x04007DC3 RID: 32195
		private static NKCUIManager.LoadedUIData m_loadedUIData;

		// Token: 0x04007DC4 RID: 32196
		private NKCUIUpsideMenu.eMode m_UpsideMenuMode = NKCUIUpsideMenu.eMode.Normal;

		// Token: 0x04007DC5 RID: 32197
		private int m_UnitID;

		// Token: 0x04007DC6 RID: 32198
		private int m_SkinID;

		// Token: 0x04007DC7 RID: 32199
		private NKMUnitData m_NKMUnitData;

		// Token: 0x04007DC8 RID: 32200
		private List<NKMEquipItemData> m_listNKMEquipItemData;

		// Token: 0x04007DC9 RID: 32201
		[Header("요약정보창")]
		public NKCUICharInfoSummary m_NKCUICharInfoSummary;

		// Token: 0x04007DCA RID: 32202
		[Header("장착정보창")]
		public GameObject m_objEquipSlotParent;

		// Token: 0x04007DCB RID: 32203
		public NKCUISlot m_slotEquipWeapon;

		// Token: 0x04007DCC RID: 32204
		public NKCUISlot m_slotEquipDefense;

		// Token: 0x04007DCD RID: 32205
		public NKCUISlot m_slotEquipAcc;

		// Token: 0x04007DCE RID: 32206
		public NKCUISlot m_slotEquipAcc_2;

		// Token: 0x04007DCF RID: 32207
		[Header("세트아이템 이펙트")]
		public GameObject m_NKM_UI_UNIT_INFO_EQUIP_SET_FX_WEAPON;

		// Token: 0x04007DD0 RID: 32208
		public Animator m_ani_NKM_UI_UNIT_INFO_EQUIP_SET_FX_WEAPON;

		// Token: 0x04007DD1 RID: 32209
		public GameObject m_NKM_UI_UNIT_INFO_EQUIP_SET_FX_DEFENCE;

		// Token: 0x04007DD2 RID: 32210
		public Animator m_ani_NKM_UI_UNIT_INFO_EQUIP_SET_FX_DEFENCE;

		// Token: 0x04007DD3 RID: 32211
		public GameObject m_NKM_UI_UNIT_INFO_EQUIP_SET_FX_ACC;

		// Token: 0x04007DD4 RID: 32212
		public Animator m_ani_NKM_UI_UNIT_INFO_EQUIP_SET_FX_ACC;

		// Token: 0x04007DD5 RID: 32213
		public GameObject m_NKM_UI_UNIT_INFO_EQUIP_SET_FX_ACC_02;

		// Token: 0x04007DD6 RID: 32214
		public Animator m_ani_NKM_UI_UNIT_INFO_EQUIP_SET_FX_ACC_02;

		// Token: 0x04007DD7 RID: 32215
		[Header("상세정보창")]
		public Text m_NKM_UI_COLLECTION_UNIT_STAT_SUMMARY_POWER_TEXT;

		// Token: 0x04007DD8 RID: 32216
		public ScrollRect m_srUnitIntroduce;

		// Token: 0x04007DD9 RID: 32217
		public Text m_NKM_UI_COLLECTION_UNIT_PROFILE_UNIT_INTRODUCE_TEXT;

		// Token: 0x04007DDA RID: 32218
		public NKCUIComStateButton m_GuideBtn;

		// Token: 0x04007DDB RID: 32219
		public string m_GuideStrID;

		// Token: 0x04007DDC RID: 32220
		public NKCUIUnitStatSlot m_slotHP;

		// Token: 0x04007DDD RID: 32221
		public NKCUIUnitStatSlot m_slotAttack;

		// Token: 0x04007DDE RID: 32222
		public NKCUIUnitStatSlot m_slotDefense;

		// Token: 0x04007DDF RID: 32223
		public NKCUIUnitStatSlot m_slotHitRate;

		// Token: 0x04007DE0 RID: 32224
		public NKCUIUnitStatSlot m_slotCritHitRate;

		// Token: 0x04007DE1 RID: 32225
		public NKCUIUnitStatSlot m_slotEvade;

		// Token: 0x04007DE2 RID: 32226
		[Header("탭")]
		public NKCUIComStateButton m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_PROFILE;

		// Token: 0x04007DE3 RID: 32227
		public GameObject m_NKM_UI_COLLECTION_UNIT_PROFILE;

		// Token: 0x04007DE4 RID: 32228
		public NKCUIComStateButton m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_STAT;

		// Token: 0x04007DE5 RID: 32229
		public GameObject m_NKM_UI_COLLECTION_UNIT_STAT;

		// Token: 0x04007DE6 RID: 32230
		public NKCUIComStateButton m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_TAG;

		// Token: 0x04007DE7 RID: 32231
		public GameObject m_NKM_UI_COLLECTION_UNIT_TAG;

		// Token: 0x04007DE8 RID: 32232
		[Header("스킨")]
		public NKCUIUnitInfoSkinPanel m_UISkinPanel;

		// Token: 0x04007DE9 RID: 32233
		public GameObject m_NKM_UI_COLLECTION_UNIT_INFO_UNIT_SD;

		// Token: 0x04007DEA RID: 32234
		[Header("유닛 일러스트")]
		public NKCUICharacterView m_CharacterView;

		// Token: 0x04007DEB RID: 32235
		[Header("일러스트 보기 모드에서 움직이는 Rect들. Base/ViewMode 두 이름으로 지정")]
		public Animator m_ani_NKM_UI_COLLECTION_UNIT_INFO_CONTENT;

		// Token: 0x04007DEC RID: 32236
		[Header("기타 버튼")]
		public NKCUIComStateButton m_NKM_UI_COLLECTION_UNIT_INFO_BOTTOM_BUTTON_PRACTICE;

		// Token: 0x04007DED RID: 32237
		public NKCUIComStateButton m_NKM_UI_COLLECTION_UNIT_INFO_BOTTOM_BUTTON_APPRAISAL;

		// Token: 0x04007DEE RID: 32238
		public NKCUIComStateButton m_NKM_UI_COLLECTION_UNIT_INFO_BOTTOM_BUTTON_ILLUST_CHANGE;

		// Token: 0x04007DEF RID: 32239
		public NKCUIComStateButton m_NKM_UI_COLLECTION_UNIT_PROFILE_TAG_VIEW_ALL_BUTTON;

		// Token: 0x04007DF0 RID: 32240
		public NKCUIComStateButton m_NKM_UI_COLLECTION_UNIT_PROFILE_VOICE_BUTTON;

		// Token: 0x04007DF1 RID: 32241
		public NKCUIComStateButton m_NKM_UI_COLLECTION_UNIT_INFO_LOYALTY;

		// Token: 0x04007DF2 RID: 32242
		public NKCUIComStateButton m_NKM_UI_COLLECTION_UNIT_ACHIEVEMENT_BUTTON;

		// Token: 0x04007DF3 RID: 32243
		public NKCUIComButton m_cbtnUnitInfoDetailPopup;

		// Token: 0x04007DF4 RID: 32244
		[Space]
		public GameObject m_NKM_UI_COLLECTION_UNIT_INFO_NOTICE_NOTGET;

		// Token: 0x04007DF5 RID: 32245
		public GameObject m_NKM_UI_COLLECTION_UNIT_PROFILE_VOICE_BUTTON_DISABLE;

		// Token: 0x04007DF6 RID: 32246
		public GameObject m_NKM_UI_COLLECTION_UNIT_ACHIEVEMENT_BUTTON_NEW;

		// Token: 0x04007DF7 RID: 32247
		[Header("스킬 패널")]
		public NKCUIUnitInfoSkillPanel m_UISkillPanel;

		// Token: 0x04007DF8 RID: 32248
		[Header("태그 Top6 노출")]
		public GameObject m_NKM_UI_COLLECTION_UNIT_PROFILE_USER_TAG;

		// Token: 0x04007DF9 RID: 32249
		public GameObject m_NKM_UI_COLLECTION_UNIT_PROFILE_USER_TAG_CAUTION;

		// Token: 0x04007DFA RID: 32250
		[Header("성우")]
		public GameObject m_objVoiceActor;

		// Token: 0x04007DFB RID: 32251
		public Text m_lbVoiceActorName;

		// Token: 0x04007DFC RID: 32252
		private NKCUICollectionUnitInfo.eCollectionState m_eCurrentState;

		// Token: 0x04007DFD RID: 32253
		[Header("캐릭터 판넬")]
		public NKCUIComDragSelectablePanel m_DragCharacterView;

		// Token: 0x04007DFE RID: 32254
		public EventTrigger m_evtPanel;

		// Token: 0x04007DFF RID: 32255
		private NKCUIUnitInfo.OpenOption m_OpenOption;

		// Token: 0x04007E00 RID: 32256
		private bool m_bAppraisal;

		// Token: 0x04007E01 RID: 32257
		private bool m_bViewMode;

		// Token: 0x04007E02 RID: 32258
		[Header("SD캐릭터 관련 설정")]
		public RectTransform m_rtSDRoot;

		// Token: 0x04007E03 RID: 32259
		private NKCASUIUnitIllust m_spineSD;

		// Token: 0x04007E04 RID: 32260
		public float m_fSDScale = 1.2f;

		// Token: 0x04007E05 RID: 32261
		private int m_CurUnitID = -1;

		// Token: 0x04007E06 RID: 32262
		[Space]
		[Header("태그")]
		public GameObject m_NKM_UI_COLLECTION_UNIT_INFO_CONTENT_USER_TAG;

		// Token: 0x04007E07 RID: 32263
		public NKCUIComStateButton m_cbtn_NKM_UI_COLLECTION_UNIT_INFO_USER_TAG_BUTTON_CLOSE;

		// Token: 0x04007E08 RID: 32264
		public Text m_USER_TAG_MY_OPINION_COUNT_TEXT;

		// Token: 0x04007E09 RID: 32265
		[Header("태그 슬롯 프리팹 & 사이즈 설정")]
		public NKCUICollectionTagSlot m_pfbTagSlot;

		// Token: 0x04007E0A RID: 32266
		public Vector2 m_vTagSlotSize;

		// Token: 0x04007E0B RID: 32267
		public Vector2 m_vTagSlotSpacing;

		// Token: 0x04007E0C RID: 32268
		[Header("태그 UI Component")]
		public RectTransform m_rectContentRect;

		// Token: 0x04007E0D RID: 32269
		public RectTransform m_rectSlotPoolRect;

		// Token: 0x04007E0E RID: 32270
		public LoopScrollRect m_LoopScrollRect;

		// Token: 0x04007E0F RID: 32271
		public GridLayoutGroup m_GridLayoutGroup;

		// Token: 0x04007E10 RID: 32272
		private List<NKCUICollectionTagSlot> m_lstVisibleSlot = new List<NKCUICollectionTagSlot>();

		// Token: 0x04007E11 RID: 32273
		private Stack<NKCUICollectionTagSlot> m_stkTagSlotPool = new Stack<NKCUICollectionTagSlot>();

		// Token: 0x04007E12 RID: 32274
		[Header("정렬 방식 선택")]
		public NKCUIComToggle m_cbtn_NKM_UI_COLLECTION_UNIT_INFO_USER_TAG_BG;

		// Token: 0x04007E13 RID: 32275
		public GameObject m_NKM_UI_COLLECTION_UNIT_INFO_USER_TAG_OPEN;

		// Token: 0x04007E14 RID: 32276
		public NKCUIComButton m_cbtnSortTypeNormal;

		// Token: 0x04007E15 RID: 32277
		public NKCUIComButton m_cbtnSortTypeVote;

		// Token: 0x04007E16 RID: 32278
		public Text m_NKM_UI_COLLECTION_UNIT_INFO_USER_TAGS_SORT_TYPE_TEXT;

		// Token: 0x04007E17 RID: 32279
		private int minColumnTag = 3;

		// Token: 0x04007E18 RID: 32280
		private NKCUICollectionUnitInfo.eTagSortOption m_eCurrentTagSortType;

		// Token: 0x04007E19 RID: 32281
		private NKCUICollectionUnitInfo.eTagSortOption m_eOldTagSortType;

		// Token: 0x04007E1A RID: 32282
		private bool m_bCellPrepared;

		// Token: 0x04007E1B RID: 32283
		[Header("유저 태그 순위 노출")]
		public List<NKCUICollectionUnitInfo.tagDiscription> tagRank;

		// Token: 0x04007E1C RID: 32284
		private List<NKCUnitTagData> m_lst_CurTagData = new List<NKCUnitTagData>();

		// Token: 0x04007E1D RID: 32285
		private const int m_iMaxTagVotdeCount = 7;

		// Token: 0x04007E1E RID: 32286
		private int m_iCurVotedCount;

		// Token: 0x04007E1F RID: 32287
		private int m_iClickSlotIdx;

		// Token: 0x020019F6 RID: 6646
		public enum eCollectionState
		{
			// Token: 0x0400AD64 RID: 44388
			CS_NONE,
			// Token: 0x0400AD65 RID: 44389
			CS_PROFILE,
			// Token: 0x0400AD66 RID: 44390
			CS_STATUS,
			// Token: 0x0400AD67 RID: 44391
			CS_TAG
		}

		// Token: 0x020019F7 RID: 6647
		private enum eTagSortOption
		{
			// Token: 0x0400AD69 RID: 44393
			TS_NONE,
			// Token: 0x0400AD6A RID: 44394
			TS_TAG_NORMAL,
			// Token: 0x0400AD6B RID: 44395
			TS_TAG_VOTE
		}

		// Token: 0x020019F8 RID: 6648
		[Serializable]
		public struct tagDiscription
		{
			// Token: 0x0400AD6C RID: 44396
			public GameObject m_tagObj;

			// Token: 0x0400AD6D RID: 44397
			public Text tagTigle;

			// Token: 0x0400AD6E RID: 44398
			public Text tagCount;
		}
	}
}
