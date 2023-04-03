using System;
using System.Collections.Generic;
using ClientPacket.Unit;
using Cs.Math;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKC.UI.Component;
using NKC.UI.NPC;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009AE RID: 2478
	public class NKCUILab : NKCUIBase
	{
		// Token: 0x060067BE RID: 26558 RVA: 0x00217540 File Offset: 0x00215740
		public static NKCAssetResourceData OpenInstanceAsync()
		{
			return NKCUIBase.OpenInstanceAsync<NKCUILab>("ab_ui_nkm_ui_lab", "NKM_UI_LAB");
		}

		// Token: 0x060067BF RID: 26559 RVA: 0x00217551 File Offset: 0x00215751
		public static bool CheckInstanceLoaded(NKCAssetResourceData loadResourceData, out NKCUILab retVal)
		{
			return NKCUIBase.CheckInstanceLoaded<NKCUILab>(loadResourceData, NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontCommon), out retVal);
		}

		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x060067C0 RID: 26560 RVA: 0x00217560 File Offset: 0x00215760
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GetLabMenuName(this.m_LAB_DETAIL_STATE);
			}
		}

		// Token: 0x170011FF RID: 4607
		// (get) Token: 0x060067C1 RID: 26561 RVA: 0x0021756D File Offset: 0x0021576D
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001200 RID: 4608
		// (get) Token: 0x060067C2 RID: 26562 RVA: 0x00217570 File Offset: 0x00215770
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				if (this.m_LAB_DETAIL_STATE == NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_SKILL_TRAIN)
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

		// Token: 0x17001201 RID: 4609
		// (get) Token: 0x060067C3 RID: 26563 RVA: 0x002175A4 File Offset: 0x002157A4
		public override string GuideTempletID
		{
			get
			{
				switch (this.m_LAB_DETAIL_STATE)
				{
				case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_ENHANCE:
					return "ARTICLE_UNIT_ENCHANT";
				case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_LIMITBREAK:
					return "ARTICLE_UNIT_LIMITBREAK";
				case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_SKILL_TRAIN:
					return "ARTICLE_UNIT_TRAINING";
				default:
					return "";
				}
			}
		}

		// Token: 0x060067C4 RID: 26564 RVA: 0x002175E8 File Offset: 0x002157E8
		public void InitUI(NKCUILabLimitBreak.OnTryLimitBreak onTryLimitBreak, NKCUILabSkillTrain.OnTrySkillTrain onTrySkillTrain, NKCUINPCProfessorOlivia npcLabProfessorOlivia)
		{
			this.m_NKCUILabUnitEnhance.Init(npcLabProfessorOlivia, new NKCUILabUnitEnhance.GetUnitList(this.onGetUnitList));
			this.m_NKCUILabLimitBreak.Init(onTryLimitBreak);
			this.m_NKCUILabSkillTrain.Init(onTrySkillTrain);
			this.InitButton();
			this.m_NKCUILabCharacterInfo.Init(new NKCUILabCharacterInfo.OnClickChangeCharacterButton(this.OnClickCharacterChange));
			this.m_NKCUIUnitSelect.Init(new UnityAction(this.OnClickCharacterChange));
			this.m_rtLimitBreak = this.m_NKCUILabLimitBreak.GetComponent<RectTransform>();
			this.m_rtUnitEnhance = this.m_NKCUILabUnitEnhance.GetComponent<RectTransform>();
			this.m_rtSkillTrain = this.m_NKCUILabSkillTrain.GetComponent<RectTransform>();
			if (this.m_DragCharacterView != null)
			{
				this.m_DragCharacterView.Init(true, true);
				this.m_DragCharacterView.dOnGetObject += this.MakeMainBannerListSlot;
				this.m_DragCharacterView.dOnReturnObject += new NKCUIComDragSelectablePanel.OnReturnObject(this.ReturnMainBannerListSlot);
				this.m_DragCharacterView.dOnProvideData += new NKCUIComDragSelectablePanel.OnProvideData(this.ProvideMainBannerListSlotData);
				this.m_DragCharacterView.dOnFocus += this.Focus;
			}
			if (this.m_evtPanel != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(new UnityAction<BaseEventData>(this.OnEventPanelClick));
				this.m_evtPanel.triggers.Add(entry);
			}
			if (npcLabProfessorOlivia != null)
			{
				this.dOnSelectUnitPlayVoice = new NKCUILab.OnSelectUnitPlayVoice(npcLabProfessorOlivia.PlayVoice);
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060067C5 RID: 26565 RVA: 0x00217770 File Offset: 0x00215970
		private void InitButton()
		{
			this.m_cbtnShortcutEnhance.PointerClick.RemoveAllListeners();
			this.m_cbtnShortcutEnhance.PointerClick.AddListener(delegate()
			{
				this.SetState(NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_ENHANCE);
			});
			this.m_cbtnShortcutLimitBreak.PointerClick.RemoveAllListeners();
			this.m_cbtnShortcutLimitBreak.PointerClick.AddListener(delegate()
			{
				this.SetState(NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_LIMITBREAK);
			});
			this.m_cbtnShortcutTraining.PointerClick.RemoveAllListeners();
			this.m_cbtnShortcutTraining.PointerClick.AddListener(delegate()
			{
				this.SetState(NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_SKILL_TRAIN);
			});
			this.m_objNKM_UI_LAB_UNIT_CHANGE = base.transform.Find("NKM_UI_LAB_UNIT_CHANGE").gameObject;
			this.m_NKM_UI_LAB_UNIT_CHANGE_btn = this.m_objNKM_UI_LAB_UNIT_CHANGE.GetComponent<NKCUIComButton>();
			this.m_NKM_UI_LAB_UNIT_CHANGE_btn.PointerClick.RemoveAllListeners();
			this.m_NKM_UI_LAB_UNIT_CHANGE_btn.PointerClick.AddListener(new UnityAction(this.OnClickCharacterChange));
			NKCUtil.SetGameobjectActive(this.m_objNKM_UI_LAB_UNIT_CHANGE, false);
		}

		// Token: 0x060067C6 RID: 26566 RVA: 0x00217865 File Offset: 0x00215A65
		public void TouchIllust()
		{
			this.m_NKCASUISpineIllust.SetAnimation(NKCASUIUnitIllust.eAnimation.UNIT_TOUCH, false, 0, true, 0f, true);
		}

		// Token: 0x060067C7 RID: 26567 RVA: 0x0021787C File Offset: 0x00215A7C
		public override void Hide()
		{
			base.Hide();
			NKCUtil.SetGameobjectActive(this.m_rtCharacterIllust.gameObject, true);
			this.m_NKCUIUnitSelect.Close();
		}

		// Token: 0x060067C8 RID: 26568 RVA: 0x002178A0 File Offset: 0x00215AA0
		public override void UnHide()
		{
			this.m_bHide = false;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_rtCharacterIllust.gameObject, true);
			if (this.m_CurrentUnitData == null)
			{
				this.OpenUnitSelect();
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objNKM_UI_LAB_UNIT_CHANGE, true);
			}
			this.TutorialCheck();
		}

		// Token: 0x060067C9 RID: 26569 RVA: 0x002178F4 File Offset: 0x00215AF4
		public void Open(NKCUILab.LAB_DETAIL_STATE _LAB_DETAIL_STATE, long lReserveUID = 0L)
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_cbtnShortcutTraining.gameObject, NKCContentManager.IsContentsUnlocked(ContentsType.LAB_TRAINING, 0, 0));
			NKCUtil.SetGameobjectActive(this.m_cbtnShortcutLimitBreak.gameObject, NKCContentManager.IsContentsUnlocked(ContentsType.LAB_LIMITBREAK, 0, 0));
			this.CurrentUnitDataCheck();
			this.SetState(_LAB_DETAIL_STATE);
			base.UIOpened(true);
			this.TutorialCheck();
			if (lReserveUID != 0L)
			{
				NKMUnitData unitFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(lReserveUID);
				if (unitFromUID != null)
				{
					List<NKMUnitData> list = new List<NKMUnitData>();
					list.Add(unitFromUID);
					this.OnUnitSortList(lReserveUID, list);
				}
				else
				{
					Debug.LogError("유닛 정보를 찾을 수 없습니다. lReserveUID : " + lReserveUID.ToString());
				}
				this.m_bForceReserveOpen = true;
				this.OnSelectUnit(new List<long>
				{
					lReserveUID
				});
			}
			else if (null != this.m_DragCharacterView)
			{
				this.m_DragCharacterView.SetArrow(false);
			}
			this.m_bForceReserveOpen = false;
		}

		// Token: 0x060067CA RID: 26570 RVA: 0x002179DC File Offset: 0x00215BDC
		private void CurrentUnitDataCheck()
		{
			bool flag = false;
			if (this.m_CurrentUnitData != null)
			{
				flag = NKCScenManager.CurrentUserData().m_ArmyData.IsHaveUnitFromUID(this.m_CurrentUnitData.m_UnitUID);
			}
			if (!flag)
			{
				this.m_CurrentUnitData = null;
				this.OpenUnitSelect();
				this.SwitchCharacterInfo(false);
				return;
			}
			this.SwitchCharacterInfo(true);
		}

		// Token: 0x060067CB RID: 26571 RVA: 0x00217A30 File Offset: 0x00215C30
		private void SetState(NKCUILab.LAB_DETAIL_STATE state)
		{
			this.m_targetState = state;
			NKCUILab.MenuTransitionType currentTransition;
			if (!base.IsOpen)
			{
				currentTransition = NKCUILab.MenuTransitionType.MenuDirectOpen;
			}
			else if (this.m_LAB_DETAIL_STATE == NKCUILab.LAB_DETAIL_STATE.LDS_INVALID)
			{
				currentTransition = NKCUILab.MenuTransitionType.Start;
			}
			else if (this.m_LAB_DETAIL_STATE == NKCUILab.LAB_DETAIL_STATE.LDS_MENU)
			{
				currentTransition = NKCUILab.MenuTransitionType.MenuToDetail;
			}
			else if (state == NKCUILab.LAB_DETAIL_STATE.LDS_MENU)
			{
				currentTransition = NKCUILab.MenuTransitionType.DetailToMenu;
			}
			else if (this.m_LAB_DETAIL_STATE == state)
			{
				currentTransition = NKCUILab.MenuTransitionType.CharacterChange;
			}
			else
			{
				currentTransition = NKCUILab.MenuTransitionType.DetailToDetail;
			}
			this.m_cbtnShortcutEnhance.Select(state == NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_ENHANCE, false, false);
			this.m_cbtnShortcutLimitBreak.Select(state == NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_LIMITBREAK, false, false);
			this.m_cbtnShortcutTraining.Select(state == NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_SKILL_TRAIN, false, false);
			this.ProcessChangeState(currentTransition, this.m_LAB_DETAIL_STATE, state);
		}

		// Token: 0x060067CC RID: 26572 RVA: 0x00217AC4 File Offset: 0x00215CC4
		public void ProcessChangeState(NKCUILab.MenuTransitionType currentTransition, NKCUILab.LAB_DETAIL_STATE oldState, NKCUILab.LAB_DETAIL_STATE newState)
		{
			this.m_LAB_DETAIL_STATE = newState;
			base.UpdateUpsideMenu();
			switch (currentTransition)
			{
			case NKCUILab.MenuTransitionType.Start:
				NKCUtil.SetGameobjectActive(this.m_NKCUILabCharacterInfo, false);
				NKCUtil.SetGameobjectActive(this.m_objNKM_UI_LAB_UNIT_CHANGE, false);
				NKCUtil.SetGameobjectActive(this.m_objRootShortcutMenu, false);
				NKCUtil.SetGameobjectActive(this.m_NKCUILabLimitBreak, false);
				NKCUtil.SetGameobjectActive(this.m_NKCUILabUnitEnhance, false);
				NKCUtil.SetGameobjectActive(this.m_NKCUILabSkillTrain, false);
				break;
			case NKCUILab.MenuTransitionType.MenuToDetail:
			case NKCUILab.MenuTransitionType.CharacterChange:
				if (this.m_CurrentUnitData != null)
				{
					this.SwitchCharacterInfo(true);
					if (oldState != newState)
					{
						this.SetLeftUIIn(ref this.m_rtCharacterInfo);
					}
				}
				NKCUtil.SetGameobjectActive(this.m_objRootShortcutMenu, true);
				this.ChangeFunctionByState(newState);
				this.ChangeEmptyPanel(newState);
				this.SetDetailData(newState);
				if (oldState != newState)
				{
					this.PlaySubUIAnimation(newState, NKCUILab.UI_ANIM_TYPE.UAT_IN);
				}
				break;
			case NKCUILab.MenuTransitionType.DetailToMenu:
				if (oldState != newState)
				{
					this.PlaySubUIAnimation(oldState, NKCUILab.UI_ANIM_TYPE.UAT_OUT);
				}
				break;
			case NKCUILab.MenuTransitionType.DetailToDetail:
				this.SetDetailData(newState);
				this.ChangeEmptyPanel(newState);
				if (oldState != newState)
				{
					this.PlaySubUIAnimation(oldState, NKCUILab.UI_ANIM_TYPE.UAT_OUT);
					this.PlaySubUIAnimation(newState, NKCUILab.UI_ANIM_TYPE.UAT_IN);
				}
				if (this.m_CurrentUnitData == null)
				{
					this.OpenUnitSelect();
				}
				break;
			case NKCUILab.MenuTransitionType.MenuDirectOpen:
				if (this.m_CurrentUnitData != null)
				{
					this.SwitchCharacterInfo(true);
					this.SetLeftUIIn(ref this.m_rtCharacterInfo);
					this.SetLeftUIIn(ref this.m_rtShortcutMenu);
				}
				NKCUtil.SetGameobjectActive(this.m_objRootShortcutMenu, true);
				this.ChangeFunctionByState(newState);
				this.ChangeEmptyPanel(newState);
				this.SetDetailData(newState);
				if (oldState != newState)
				{
					this.PlaySubUIAnimation(newState, NKCUILab.UI_ANIM_TYPE.UAT_IN);
				}
				this.m_NKCUILabCharacterInfo.SetData(this.m_CurrentUnitData);
				break;
			}
			if ((currentTransition == NKCUILab.MenuTransitionType.MenuDirectOpen || currentTransition == NKCUILab.MenuTransitionType.DetailToDetail) && (oldState != newState || oldState == NKCUILab.LAB_DETAIL_STATE.LDS_INVALID))
			{
				this.MakeDummyList(newState);
			}
		}

		// Token: 0x060067CD RID: 26573 RVA: 0x00217C5D File Offset: 0x00215E5D
		private void SwitchCharacterInfo(bool bActive)
		{
			NKCUtil.SetGameobjectActive(this.m_NKCUILabCharacterInfo, bActive);
			NKCUtil.SetGameobjectActive(this.m_objNKM_UI_LAB_UNIT_CHANGE, bActive);
		}

		// Token: 0x060067CE RID: 26574 RVA: 0x00217C77 File Offset: 0x00215E77
		private void ChangeFunctionByState(NKCUILab.LAB_DETAIL_STATE newState)
		{
			NKCUtil.SetGameobjectActive(this.m_NKCUILabLimitBreak, newState == NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_LIMITBREAK);
			NKCUtil.SetGameobjectActive(this.m_NKCUILabUnitEnhance, newState == NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_ENHANCE);
			NKCUtil.SetGameobjectActive(this.m_NKCUILabSkillTrain, newState == NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_SKILL_TRAIN);
		}

		// Token: 0x060067CF RID: 26575 RVA: 0x00217CA6 File Offset: 0x00215EA6
		private void ChangeEmptyPanel(NKCUILab.LAB_DETAIL_STATE newState)
		{
			NKCUtil.SetGameobjectActive(this.m_objNKM_UI_LAB_LIMITBREAK_EMPTY_ROOT, this.m_CurrentUnitData == null && newState == NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_LIMITBREAK);
			NKCUtil.SetGameobjectActive(this.m_objNKM_UI_LAB_TRAINING_EMPTY, this.m_CurrentUnitData == null && newState == NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_SKILL_TRAIN);
		}

		// Token: 0x060067D0 RID: 26576 RVA: 0x00217CDC File Offset: 0x00215EDC
		private void PlaySubUIAnimation(NKCUILab.LAB_DETAIL_STATE state, NKCUILab.UI_ANIM_TYPE ani)
		{
			RectTransform rectTransform;
			this.GetUIRectTransform(state, out rectTransform);
			if (null == rectTransform)
			{
				return;
			}
			if (ani != NKCUILab.UI_ANIM_TYPE.UAT_IN)
			{
				if (ani == NKCUILab.UI_ANIM_TYPE.UAT_OUT)
				{
					this.SetSubUIOut(rectTransform);
					return;
				}
			}
			else
			{
				this.SetSubUIIn(ref rectTransform);
			}
		}

		// Token: 0x060067D1 RID: 26577 RVA: 0x00217D13 File Offset: 0x00215F13
		private void GetUIRectTransform(NKCUILab.LAB_DETAIL_STATE state, out RectTransform rt)
		{
			rt = null;
			switch (state)
			{
			case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_ENHANCE:
				rt = this.m_rtUnitEnhance;
				return;
			case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_LIMITBREAK:
				rt = this.m_rtLimitBreak;
				return;
			case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_SKILL_TRAIN:
				rt = this.m_rtSkillTrain;
				return;
			default:
				return;
			}
		}

		// Token: 0x060067D2 RID: 26578 RVA: 0x00217D48 File Offset: 0x00215F48
		private void SetSubUIIn(ref RectTransform rect)
		{
			rect.DOKill(false);
			rect.anchorMin = new Vector2(0.5f, 0f);
			rect.anchorMax = new Vector2(1.5f, 1f);
			NKCUtil.SetGameobjectActive(rect, true);
			rect.DOAnchorMin(Vector2.zero, 0.4f, false).SetEase(Ease.OutCubic);
			rect.DOAnchorMax(Vector2.one, 0.4f, false).SetEase(Ease.OutCubic);
		}

		// Token: 0x060067D3 RID: 26579 RVA: 0x00217DC8 File Offset: 0x00215FC8
		private void SetSubUIOut(RectTransform rect)
		{
			rect.DOKill(false);
			rect.DOAnchorMin(new Vector2(0.5f, 0f), 0.4f, false).SetEase(Ease.OutCubic).OnComplete(delegate
			{
				NKCUtil.SetGameobjectActive(rect, false);
			});
			rect.DOAnchorMax(new Vector2(1.5f, 1f), 0.4f, false).SetEase(Ease.OutCubic);
		}

		// Token: 0x060067D4 RID: 26580 RVA: 0x00217E50 File Offset: 0x00216050
		private void SetLeftUIIn(ref RectTransform rect)
		{
			rect.anchorMin = Vector2.left;
			rect.anchorMax = Vector2.up;
			rect.DOAnchorPosX(-1000f, 0f, false);
			rect.DOKill(false);
			rect.DOAnchorMin(Vector2.zero, 0.4f, false).SetEase(Ease.OutCubic);
			rect.DOAnchorMax(Vector2.one, 0.4f, false).SetEase(Ease.OutCubic);
		}

		// Token: 0x060067D5 RID: 26581 RVA: 0x00217EC8 File Offset: 0x002160C8
		private void SetDetailData(NKCUILab.LAB_DETAIL_STATE state)
		{
			switch (state)
			{
			case NKCUILab.LAB_DETAIL_STATE.LDS_MENU:
				break;
			case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_ENHANCE:
				this.m_NKCUILabUnitEnhance.SetData(this.m_CurrentUnitData);
				return;
			case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_LIMITBREAK:
				this.m_NKCUILabLimitBreak.SetData(this.m_CurrentUnitData, NKCScenManager.GetScenManager().GetMyUserData());
				return;
			case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_SKILL_TRAIN:
				this.m_NKCUILabSkillTrain.SetData(NKCScenManager.GetScenManager().GetMyUserData(), this.m_CurrentUnitData, -1, false);
				break;
			default:
				return;
			}
		}

		// Token: 0x060067D6 RID: 26582 RVA: 0x00217F39 File Offset: 0x00216139
		private void OpenUnitSelect()
		{
			if (this.m_LAB_DETAIL_STATE == NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_SKILL_TRAIN)
			{
				this.m_NKCUILabSkillTrain.SwitchSkillBack(false);
			}
			NKCUIUnitSelect nkcuiunitSelect = this.m_NKCUIUnitSelect;
			if (nkcuiunitSelect == null)
			{
				return;
			}
			nkcuiunitSelect.Open();
		}

		// Token: 0x060067D7 RID: 26583 RVA: 0x00217F60 File Offset: 0x00216160
		private NKCUIUnitSelectList.UnitSelectListOptions GetLabUnitSelectOption(NKCUILab.LAB_DETAIL_STATE targetState, bool bIncludeCurrnetUnit = false)
		{
			NKCUIUnitSelectList.UnitSelectListOptions options = new NKCUIUnitSelectList.UnitSelectListOptions(NKM_UNIT_TYPE.NUT_NORMAL, false, NKM_DECK_TYPE.NDT_NORMAL, NKCUIUnitSelectList.eUnitSelectListMode.Normal, true);
			options.bDescending = true;
			options.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
			options.lstSortOption = NKCUnitSortSystem.GetDefaultSortOptions(NKM_UNIT_TYPE.NUT_NORMAL, false, false);
			options.bShowRemoveSlot = false;
			options.bShowHideDeckedUnitMenu = false;
			options.bHideDeckedUnit = false;
			options.bCanSelectUnitInMission = true;
			options.eDeckType = NKM_DECK_TYPE.NDT_NORMAL;
			options.setExcludeUnitUID = new HashSet<long>();
			options.strUpsideMenuName = NKCUtilString.GetLabSelectUnitMenuName(targetState);
			options.strEmptyMessage = NKCUtilString.GetLabSelectUnitEmptyMsg(targetState);
			options.bPushBackUnselectable = false;
			options.m_SortOptions.bIgnoreCityState = true;
			options.m_SortOptions.bIgnoreWorldMapLeader = true;
			options.m_bUseFavorite = true;
			options.setUnitFilterCategory = NKCUnitSortSystem.setDefaultUnitFilterCategory;
			options.setUnitSortCategory = NKCUnitSortSystem.setDefaultUnitSortCategory;
			options.setShipFilterCategory = NKCUnitSortSystem.setDefaultShipFilterCategory;
			options.setShipSortCategory = NKCUnitSortSystem.setDefaultShipSortCategory;
			if (this.m_CurrentUnitData != null)
			{
				options.m_IncludeUnitUID = this.m_CurrentUnitData.m_UnitUID;
			}
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			switch (targetState)
			{
			case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_ENHANCE:
			{
				List<NKMUnitData> list = new List<NKMUnitData>(armyData.m_dicMyUnit.Values);
				for (int i = 0; i < list.Count; i++)
				{
					if (options.m_IncludeUnitUID == list[i].m_UnitUID)
					{
						options.setExcludeUnitUID.Add(list[i].m_UnitUID);
					}
					else if (NKMEnhanceManager.CheckUnitFullEnhance(list[i]))
					{
						options.setExcludeUnitUID.Add(list[i].m_UnitUID);
					}
				}
				options.lstSortOption = new List<NKCUnitSortSystem.eSortOption>
				{
					NKCUnitSortSystem.eSortOption.Level_High,
					NKCUnitSortSystem.eSortOption.Rarity_High,
					NKCUnitSortSystem.eSortOption.Unit_SummonCost_High,
					NKCUnitSortSystem.eSortOption.ID_First,
					NKCUnitSortSystem.eSortOption.UID_Last
				};
				options.m_SortOptions.AdditionalExcludeFilterFunc = new NKCUnitSortSystem.UnitListOptions.CustomFilterFunc(this.CheckUnitCanEnhance);
				break;
			}
			case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_LIMITBREAK:
			{
				this.dicUnitLimitBreak.Clear();
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				if (nkmuserData != null)
				{
					foreach (KeyValuePair<long, NKMUnitData> keyValuePair in armyData.m_dicMyUnit)
					{
						int num = NKMUnitLimitBreakManager.CanThisUnitLimitBreakNow(keyValuePair.Value, nkmuserData);
						if (keyValuePair.Value.m_UnitUID == options.m_IncludeUnitUID && num < 0)
						{
							num = 0;
						}
						this.dicUnitLimitBreak[keyValuePair.Key] = num;
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
				break;
			}
			case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_SKILL_TRAIN:
				foreach (KeyValuePair<long, NKMUnitData> keyValuePair2 in armyData.m_dicMyUnit)
				{
					if (keyValuePair2.Value.m_UnitUID == options.m_IncludeUnitUID)
					{
						options.setExcludeUnitUID.Add(keyValuePair2.Key);
					}
					else if (!NKMUnitSkillManager.CheckHaveUpgradableSkill(keyValuePair2.Value))
					{
						options.setExcludeUnitUID.Add(keyValuePair2.Key);
					}
				}
				options.m_SortOptions.AdditionalExcludeFilterFunc = new NKCUnitSortSystem.UnitListOptions.CustomFilterFunc(this.CheckUnitCanSkillTrain);
				break;
			}
			if (!bIncludeCurrnetUnit && this.m_CurrentUnitData != null && !options.setExcludeUnitUID.Contains(this.m_CurrentUnitData.m_UnitUID))
			{
				options.setExcludeUnitUID.Add(this.m_CurrentUnitData.m_UnitUID);
			}
			return options;
		}

		// Token: 0x060067D8 RID: 26584 RVA: 0x00218470 File Offset: 0x00216670
		private void OpenUnitSelect(NKCUILab.LAB_DETAIL_STATE targetState)
		{
			NKCUIUnitSelectList.Instance.Open(this.GetLabUnitSelectOption(targetState, false), new NKCUIUnitSelectList.OnUnitSelectCommand(this.OnSelectUnit), new NKCUIUnitSelectList.OnUnitSortList(this.OnUnitSortList), null, new NKCUIUnitSelectList.OnUnitSortOption(this.OnUnitSortOption), null);
		}

		// Token: 0x060067D9 RID: 26585 RVA: 0x002184AC File Offset: 0x002166AC
		private bool CheckUnitCanEnhance(NKMUnitData unitData)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID);
			return unitTempletBase != null && unitTempletBase.m_NKM_UNIT_STYLE_TYPE != NKM_UNIT_STYLE_TYPE.NUST_TRAINER;
		}

		// Token: 0x060067DA RID: 26586 RVA: 0x002184D4 File Offset: 0x002166D4
		private bool CheckUnitCanLimitBreak(NKMUnitData unitData)
		{
			return NKMUnitLimitBreakManager.CanThisUnitLimitBreak(unitData);
		}

		// Token: 0x060067DB RID: 26587 RVA: 0x002184DC File Offset: 0x002166DC
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

		// Token: 0x060067DC RID: 26588 RVA: 0x00218558 File Offset: 0x00216758
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

		// Token: 0x060067DD RID: 26589 RVA: 0x002185A4 File Offset: 0x002167A4
		private bool CheckUnitCanSkillTrain(NKMUnitData unitData)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID);
			return unitTempletBase != null && unitTempletBase.m_NKM_UNIT_STYLE_TYPE != NKM_UNIT_STYLE_TYPE.NUST_TRAINER;
		}

		// Token: 0x060067DE RID: 26590 RVA: 0x002185CC File Offset: 0x002167CC
		private void OnSelectUnit(List<long> unitUID)
		{
			if (unitUID.Count != 1)
			{
				Debug.LogError("NKCUILab.OpenUnitEnhance, Fatal Error : UnitSelectList returned wrong list");
				this.OpenUnitSelect();
				return;
			}
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			NKMUnitData unitFromUID = armyData.GetUnitFromUID(unitUID[0]);
			if (unitFromUID == null)
			{
				Debug.Log("NKCUILab.OpenUnitEnhance, Fatal Error : wrong uid, newUnitData is null");
				return;
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = this.CanSelectUnit(unitFromUID, armyData);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupMessageManager.AddPopupMessage(nkm_ERROR_CODE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			NKCUILab.LAB_DETAIL_STATE targetState = this.m_targetState;
			if (targetState != NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_ENHANCE)
			{
				if (targetState == NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_LIMITBREAK)
				{
					if (NKMUnitLimitBreakManager.IsMaxLimitBreak(unitFromUID, true) && !this.m_bForceReserveOpen)
					{
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_ALREADY_LIMITBREAK_MAX, null, "");
						this.OpenUnitSelect();
						return;
					}
				}
			}
			else if (NKMEnhanceManager.CheckUnitFullEnhance(unitFromUID) && !this.m_bForceReserveOpen)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_ALREADY_ENHANCE_MAX, null, "");
				this.OpenUnitSelect();
				return;
			}
			NKCUIUnitSelectList.CheckInstanceAndClose();
			if (this.m_CurrentUnitData == null || this.m_CurrentUnitData.m_UnitUID != unitFromUID.m_UnitUID)
			{
				this.m_NKCUILabCharacterInfo.SetData(unitFromUID);
			}
			this.m_CurrentUnitData = unitFromUID;
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitFromUID.m_UnitID);
			if (this.dOnSelectUnitPlayVoice != null)
			{
				this.dOnSelectUnitPlayVoice(unitTempletBase.m_NKM_UNIT_STYLE_TYPE, this.m_LAB_DETAIL_STATE);
			}
			this.SetState(this.m_targetState);
			this.m_NKCUIUnitSelect.Close();
		}

		// Token: 0x060067DF RID: 26591 RVA: 0x00218720 File Offset: 0x00216920
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

		// Token: 0x060067E0 RID: 26592 RVA: 0x00218764 File Offset: 0x00216964
		public void OnUnitSortOption(NKCUnitSortSystem.UnitListOptions unitOption)
		{
			this.m_preUnitListOption = unitOption;
		}

		// Token: 0x060067E1 RID: 26593 RVA: 0x00218770 File Offset: 0x00216970
		private void MakeDummyList(NKCUILab.LAB_DETAIL_STATE targetState)
		{
			if (this.m_DragCharacterView != null)
			{
				this.BannerCleanUp();
				if (this.m_CurrentUnitData != null)
				{
					NKCUIUnitSelectList.UnitSelectListOptions labUnitSelectOption = this.GetLabUnitSelectOption(targetState, false);
					labUnitSelectOption.lstSortOption = this.m_preUnitListOption.lstSortOption;
					labUnitSelectOption.setFilterOption = this.m_preUnitListOption.setFilterOption;
					NKCUnitSortSystem unitDummySortSystem = NKCUIUnitSelectList.GetUnitDummySortSystem(labUnitSelectOption);
					if (unitDummySortSystem.SortedUnitList.Count > 0)
					{
						bool flag = false;
						using (List<NKMUnitData>.Enumerator enumerator = unitDummySortSystem.SortedUnitList.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (enumerator.Current.m_UnitUID == this.m_CurrentUnitData.m_UnitUID)
								{
									flag = true;
									break;
								}
							}
						}
						if (!flag)
						{
							unitDummySortSystem.SortedUnitList.Add(this.m_CurrentUnitData);
						}
					}
					else
					{
						unitDummySortSystem.SortedUnitList.Add(this.m_CurrentUnitData);
					}
					this.OnUnitSortList(this.m_CurrentUnitData.m_UnitUID, unitDummySortSystem.SortedUnitList);
				}
			}
		}

		// Token: 0x060067E2 RID: 26594 RVA: 0x00218878 File Offset: 0x00216A78
		private void OnUnitSortList(long UID, List<NKMUnitData> unitUIDList)
		{
			if (unitUIDList.Count > 0)
			{
				NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
				if (armyData == null)
				{
					return;
				}
				if (this.CanSelectUnit(armyData.GetUnitFromUID(UID), armyData) != NKM_ERROR_CODE.NEC_OK)
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

		// Token: 0x060067E3 RID: 26595 RVA: 0x00218947 File Offset: 0x00216B47
		private void ClearCheckCount()
		{
			this.m_iCheckUnitCnt = 0;
			this.m_iMaxUnitCnt = this.m_UnitSortList.Count;
		}

		// Token: 0x060067E4 RID: 26596 RVA: 0x00218964 File Offset: 0x00216B64
		private void ChangeCharacter(bool bIsNext = false)
		{
			if (this.m_UnitSortList.Count <= 1)
			{
				return;
			}
			this.m_SelectUnitIndex = (bIsNext ? (this.m_SelectUnitIndex + 1) : (this.m_SelectUnitIndex - 1));
			if (this.m_SelectUnitIndex >= this.m_UnitSortList.Count)
			{
				this.m_SelectUnitIndex = 0;
			}
			if (this.m_SelectUnitIndex < 0)
			{
				this.m_SelectUnitIndex = this.m_UnitSortList.Count - 1;
			}
			this.bNextClick = bIsNext;
			this.OpenSelectUnit(this.m_SelectUnitIndex, bIsNext);
		}

		// Token: 0x060067E5 RID: 26597 RVA: 0x002189E8 File Offset: 0x00216BE8
		private void OpenSelectUnit(int idx = 0, bool bNext = false)
		{
			if (this.m_iCheckUnitCnt >= this.m_iMaxUnitCnt)
			{
				return;
			}
			NKMUnitData nkmunitData = this.m_UnitSortList[idx];
			bool flag = false;
			NKCUILab.LAB_DETAIL_STATE targetState = this.m_targetState;
			if (targetState != NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_ENHANCE)
			{
				if (targetState == NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_LIMITBREAK)
				{
					if (NKMUnitLimitBreakManager.IsMaxLimitBreak(nkmunitData, true))
					{
						flag = true;
					}
				}
			}
			else if (NKMEnhanceManager.CheckUnitFullEnhance(nkmunitData))
			{
				flag = true;
			}
			if (flag)
			{
				this.m_iCheckUnitCnt++;
				this.ChangeCharacter(bNext);
				return;
			}
			if (this.m_CurrentUnitData == null || this.m_CurrentUnitData.m_UnitUID != nkmunitData.m_UnitUID)
			{
				this.m_NKCUILabCharacterInfo.SetData(nkmunitData);
			}
			this.m_CurrentUnitData = nkmunitData;
			this.SetState(this.m_targetState);
			this.TutorialCheckUnit();
		}

		// Token: 0x060067E6 RID: 26598 RVA: 0x00218A94 File Offset: 0x00216C94
		public List<long> onGetUnitList(ref int curIdx)
		{
			curIdx = this.m_SelectUnitIndex;
			List<long> list = new List<long>();
			for (int i = 0; i < this.m_UnitSortList.Count; i++)
			{
				list.Add(this.m_UnitSortList[i].m_UnitUID);
			}
			return list;
		}

		// Token: 0x060067E7 RID: 26599 RVA: 0x00218AE0 File Offset: 0x00216CE0
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			switch (this.m_LAB_DETAIL_STATE)
			{
			case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_ENHANCE:
				if (itemData.ItemID == 1)
				{
					this.m_NKCUILabUnitEnhance.UpdateRequiredCredit();
					return;
				}
				break;
			case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_LIMITBREAK:
				this.m_NKCUILabLimitBreak.OnInventoryChange(itemData);
				return;
			case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_SKILL_TRAIN:
				this.m_NKCUILabSkillTrain.OnInventoryChange(itemData);
				break;
			default:
				return;
			}
		}

		// Token: 0x060067E8 RID: 26600 RVA: 0x00218B38 File Offset: 0x00216D38
		public override void OnUnitUpdate(NKMUserData.eChangeNotifyType eEventType, NKM_UNIT_TYPE eUnitType, long uid, NKMUnitData unitData)
		{
			if (eEventType == NKMUserData.eChangeNotifyType.Update && this.m_CurrentUnitData != null && this.m_CurrentUnitData.m_UnitUID == uid)
			{
				this.m_NKCUILabCharacterInfo.SetData(unitData);
				this.m_CurrentUnitData = unitData;
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
							componentInChildren.SetCharacterIllust(unitData, false, unitData.m_SkinID == 0, true, 0);
						}
					}
				}
			}
			switch (this.m_LAB_DETAIL_STATE)
			{
			case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_ENHANCE:
				this.m_NKCUILabUnitEnhance.UnitUpdated(uid, unitData);
				return;
			case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_LIMITBREAK:
				this.m_NKCUILabLimitBreak.UnitUpdated(uid, unitData);
				return;
			case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_SKILL_TRAIN:
				this.m_NKCUILabSkillTrain.UnitUpdated(uid, unitData);
				return;
			default:
				return;
			}
		}

		// Token: 0x060067E9 RID: 26601 RVA: 0x00218C5F File Offset: 0x00216E5F
		public override void OnBackButton()
		{
			base.OnBackButton();
			this.m_NKCUIUnitSelect.Close();
		}

		// Token: 0x060067EA RID: 26602 RVA: 0x00218C74 File Offset: 0x00216E74
		public void OnRecv(NKMPacket_ENHANCE_UNIT_ACK sPacket)
		{
			if (this.m_LAB_DETAIL_STATE == NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_ENHANCE && sPacket.errorCode == NKM_ERROR_CODE.NEC_OK)
			{
				NKCSoundManager.PlaySound("FX_UI_UNIT_ENCHANT_START", 1f, 0f, 0f, false, 0f, false, 0f);
				this.m_animEffect.SetTrigger("ENHANCE");
				this.m_rtEffect.position = this.m_rtCharacterIllust.position;
				NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_GROWTH_STATUS, NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetUnitFromUID(sPacket.unitUID), false, false);
			}
		}

		// Token: 0x060067EB RID: 26603 RVA: 0x00218D04 File Offset: 0x00216F04
		public void OnRecv(NKMPacket_UNIT_SKILL_UPGRADE_ACK sPacket)
		{
			if (this.m_LAB_DETAIL_STATE == NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_SKILL_TRAIN)
			{
				if (sPacket.errorCode != NKM_ERROR_CODE.NEC_OK)
				{
					return;
				}
				this.m_NKCUILabSkillTrain.OnSkillLevelUp(sPacket.skillID);
				NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_GROWTH_SKILL, NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetUnitFromUID(sPacket.unitUID), false, false);
			}
		}

		// Token: 0x060067EC RID: 26604 RVA: 0x00218D58 File Offset: 0x00216F58
		public void OnClickCharacterChange()
		{
			this.bNextClick = true;
			this.m_NKCUIUnitSelect.Outro();
			this.OpenUnitSelect(this.m_LAB_DETAIL_STATE);
		}

		// Token: 0x060067ED RID: 26605 RVA: 0x00218D78 File Offset: 0x00216F78
		public override void CloseInternal()
		{
			this.m_LAB_DETAIL_STATE = NKCUILab.LAB_DETAIL_STATE.LDS_INVALID;
			this.m_NKCUILabLimitBreak.Cleanup();
			this.m_NKCUILabSkillTrain.Cleanup();
			this.m_NKCUILabUnitEnhance.Cleanup();
			if (this.m_NKCASUISpineIllust != null)
			{
				NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_NKCASUISpineIllust);
				this.m_NKCASUISpineIllust = null;
			}
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
			this.m_NKCUIUnitSelect.Close();
			this.BannerCleanUp();
		}

		// Token: 0x060067EE RID: 26606 RVA: 0x00218DFB File Offset: 0x00216FFB
		public void ClearAllFeedUnitSlots()
		{
			this.m_NKCUILabUnitEnhance.ClearAllFeedUnitSlots();
		}

		// Token: 0x060067EF RID: 26607 RVA: 0x00218E08 File Offset: 0x00217008
		private void ClearFeedUnitSlot(int index)
		{
			this.m_NKCUILabUnitEnhance.ClearFeedUnitSlot(index);
		}

		// Token: 0x060067F0 RID: 26608 RVA: 0x00218E18 File Offset: 0x00217018
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

		// Token: 0x060067F1 RID: 26609 RVA: 0x00218E88 File Offset: 0x00217088
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

		// Token: 0x060067F2 RID: 26610 RVA: 0x00218F12 File Offset: 0x00217112
		private void ReturnMainBannerListSlot(Transform go)
		{
			NKCUtil.SetGameobjectActive(go, false);
			UnityEngine.Object.Destroy(go.gameObject);
		}

		// Token: 0x060067F3 RID: 26611 RVA: 0x00218F28 File Offset: 0x00217128
		private void ProvideMainBannerListSlotData(Transform tr, int idx)
		{
			NKMUnitData nkmunitData = this.m_UnitSortList[idx];
			if (nkmunitData != null)
			{
				NKCUICharacterView component = tr.GetComponent<NKCUICharacterView>();
				if (component != null)
				{
					component.CloseImmediatelyIllust();
					component.SetCharacterIllust(nkmunitData, false, nkmunitData.m_SkinID == 0, true, 0);
					return;
				}
				NKCUICharacterView nkcuicharacterView = tr.gameObject.AddComponent<NKCUICharacterView>();
				nkcuicharacterView.m_rectIllustRoot = tr.GetComponent<RectTransform>();
				nkcuicharacterView.SetCharacterIllust(nkmunitData, false, nkmunitData.m_SkinID == 0, true, 0);
			}
		}

		// Token: 0x060067F4 RID: 26612 RVA: 0x00218F98 File Offset: 0x00217198
		private void FocusColor(RectTransform rect, Color ApplyColor)
		{
			NKCUICharacterView componentInChildren = rect.gameObject.GetComponentInChildren<NKCUICharacterView>();
			if (componentInChildren != null)
			{
				componentInChildren.SetColor(ApplyColor, false);
			}
		}

		// Token: 0x060067F5 RID: 26613 RVA: 0x00218FC4 File Offset: 0x002171C4
		private void Focus(RectTransform rect, bool bFocus)
		{
			NKCUtil.SetGameobjectActive(rect.gameObject, bFocus);
			if (bFocus)
			{
				NKCUICharacterView componentInChildren = rect.gameObject.GetComponentInChildren<NKCUICharacterView>();
				if (componentInChildren != null)
				{
					this.SetCharacterInfo(componentInChildren.GetCurrentUnitData());
				}
			}
		}

		// Token: 0x060067F6 RID: 26614 RVA: 0x00219004 File Offset: 0x00217204
		private void BannerCleanUp()
		{
			NKCUICharacterView[] componentsInChildren = this.m_rtCharacterIllust.gameObject.GetComponentsInChildren<NKCUICharacterView>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].CloseImmediatelyIllust();
			}
		}

		// Token: 0x060067F7 RID: 26615 RVA: 0x00219038 File Offset: 0x00217238
		private void SetCharacterInfo(NKMUnitData newUnitData)
		{
			if (newUnitData != null)
			{
				if (this.m_CurrentUnitData == null || this.m_CurrentUnitData.m_UnitUID != newUnitData.m_UnitUID)
				{
					this.m_NKCUILabCharacterInfo.SetData(newUnitData);
				}
				this.m_CurrentUnitData = newUnitData;
				this.SetState(this.m_targetState);
				this.TutorialCheckUnit();
			}
		}

		// Token: 0x060067F8 RID: 26616 RVA: 0x00219088 File Offset: 0x00217288
		public void SelectCharacter(int idx)
		{
			if (this.m_UnitSortList.Count < idx || idx < 0)
			{
				Debug.LogWarning(string.Format("잘못된 인덱스 총 갯수 : {0}, 목표 인덱스 : {1}", this.m_UnitSortList.Count, idx));
				return;
			}
			NKMUnitData nkmunitData = this.m_UnitSortList[idx];
			if (this.m_CurrentUnitData == null || this.m_CurrentUnitData.m_UnitUID != nkmunitData.m_UnitUID)
			{
				this.m_NKCUILabCharacterInfo.SetData(nkmunitData);
			}
			this.m_CurrentUnitData = nkmunitData;
			this.SetState(this.m_targetState);
			this.TutorialCheckUnit();
		}

		// Token: 0x060067F9 RID: 26617 RVA: 0x0021911C File Offset: 0x0021731C
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

		// Token: 0x060067FA RID: 26618 RVA: 0x00219160 File Offset: 0x00217360
		private void TutorialCheck()
		{
			NKCUILab.LAB_DETAIL_STATE lab_DETAIL_STATE = this.m_LAB_DETAIL_STATE;
			if (lab_DETAIL_STATE == NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_ENHANCE)
			{
				NKCTutorialManager.TutorialRequired(TutorialPoint.LabEnhance, true);
				return;
			}
			if (lab_DETAIL_STATE != NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_LIMITBREAK)
			{
				return;
			}
			NKCTutorialManager.TutorialRequired(TutorialPoint.LabLimitBreak, true);
		}

		// Token: 0x060067FB RID: 26619 RVA: 0x00219190 File Offset: 0x00217390
		public void TutorialCheckUnit()
		{
			switch (this.m_LAB_DETAIL_STATE)
			{
			case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_ENHANCE:
			case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_SKILL_TRAIN:
				break;
			case NKCUILab.LAB_DETAIL_STATE.LDS_UNIT_LIMITBREAK:
			{
				if (this.m_CurrentUnitData == null)
				{
					return;
				}
				NKMUnitLimitBreakManager.UnitLimitBreakStatus unitLimitbreakStatus = NKMUnitLimitBreakManager.GetUnitLimitbreakStatus(this.m_CurrentUnitData);
				Debug.LogWarning("TutorialCheckUnit - " + unitLimitbreakStatus.ToString());
				if (unitLimitbreakStatus == NKMUnitLimitBreakManager.UnitLimitBreakStatus.TranscendenceLevelNotEnough || unitLimitbreakStatus == NKMUnitLimitBreakManager.UnitLimitBreakStatus.CanTranscendence)
				{
					NKCTutorialManager.TutorialRequired(TutorialPoint.LabLimitBreakUnit, true);
				}
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x060067FC RID: 26620 RVA: 0x002191FB File Offset: 0x002173FB
		public RectTransform GetEnhanceItemSlotRect(int index)
		{
			if (index >= this.m_NKCUILabUnitEnhance.m_lstObjUnitSlot.Count)
			{
				return null;
			}
			NKCUISlot slot = this.m_NKCUILabUnitEnhance.m_lstObjUnitSlot[index].m_Slot;
			if (slot == null)
			{
				return null;
			}
			return slot.GetComponent<RectTransform>();
		}

		// Token: 0x060067FD RID: 26621 RVA: 0x00219233 File Offset: 0x00217433
		public RectTransform GetSkillLevelSlotRect(int index)
		{
			if (index >= this.m_NKCUILabSkillTrain.m_lstSkillSlot.Count)
			{
				return null;
			}
			NKCUIComButton cbtnSlot = this.m_NKCUILabSkillTrain.m_lstSkillSlot[index].m_slot.m_cbtnSlot;
			if (cbtnSlot == null)
			{
				return null;
			}
			return cbtnSlot.GetComponent<RectTransform>();
		}

		// Token: 0x040053D9 RID: 21465
		public const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_lab";

		// Token: 0x040053DA RID: 21466
		public const string UI_ASSET_NAME = "NKM_UI_LAB";

		// Token: 0x040053DB RID: 21467
		[Header("왼쪽 캐릭터 정보")]
		public RectTransform m_rtCharacterInfo;

		// Token: 0x040053DC RID: 21468
		public NKCUILabCharacterInfo m_NKCUILabCharacterInfo;

		// Token: 0x040053DD RID: 21469
		private NKCUIComButton m_NKM_UI_LAB_UNIT_CHANGE_btn;

		// Token: 0x040053DE RID: 21470
		private GameObject m_objNKM_UI_LAB_UNIT_CHANGE;

		// Token: 0x040053DF RID: 21471
		[Header("숏컷 메뉴")]
		public GameObject m_objRootShortcutMenu;

		// Token: 0x040053E0 RID: 21472
		public RectTransform m_rtShortcutMenu;

		// Token: 0x040053E1 RID: 21473
		public NKCUIComButton m_cbtnShortcutEnhance;

		// Token: 0x040053E2 RID: 21474
		public NKCUIComButton m_cbtnShortcutLimitBreak;

		// Token: 0x040053E3 RID: 21475
		public NKCUIComButton m_cbtnShortcutTraining;

		// Token: 0x040053E4 RID: 21476
		[Header("캐릭터 스파인 일러스트")]
		public RectTransform m_rtCharacterIllust;

		// Token: 0x040053E5 RID: 21477
		[Header("초월")]
		public NKCUILabLimitBreak m_NKCUILabLimitBreak;

		// Token: 0x040053E6 RID: 21478
		private RectTransform m_rtLimitBreak;

		// Token: 0x040053E7 RID: 21479
		[Header("강화")]
		public NKCUILabUnitEnhance m_NKCUILabUnitEnhance;

		// Token: 0x040053E8 RID: 21480
		private RectTransform m_rtUnitEnhance;

		// Token: 0x040053E9 RID: 21481
		[Header("훈련")]
		public NKCUILabSkillTrain m_NKCUILabSkillTrain;

		// Token: 0x040053EA RID: 21482
		private RectTransform m_rtSkillTrain;

		// Token: 0x040053EB RID: 21483
		[Header("이펙트 관련")]
		public Animator m_animEffect;

		// Token: 0x040053EC RID: 21484
		public RectTransform m_rtEffect;

		// Token: 0x040053ED RID: 21485
		public GameObject m_objNKM_UI_LAB_TRAINING_EMPTY;

		// Token: 0x040053EE RID: 21486
		public GameObject m_objNKM_UI_LAB_LIMITBREAK_EMPTY_ROOT;

		// Token: 0x040053EF RID: 21487
		public NKCUIUnitSelect m_NKCUIUnitSelect;

		// Token: 0x040053F0 RID: 21488
		[Header("캐릭터 판넬")]
		public NKCUIComDragSelectablePanel m_DragCharacterView;

		// Token: 0x040053F1 RID: 21489
		public EventTrigger m_evtPanel;

		// Token: 0x040053F2 RID: 21490
		private NKCUILab.OnSelectUnitPlayVoice dOnSelectUnitPlayVoice;

		// Token: 0x040053F3 RID: 21491
		private NKCUILab.LAB_DETAIL_STATE m_LAB_DETAIL_STATE;

		// Token: 0x040053F4 RID: 21492
		private NKCUILab.LAB_DETAIL_STATE m_targetState;

		// Token: 0x040053F5 RID: 21493
		private NKMUnitData m_CurrentUnitData;

		// Token: 0x040053F6 RID: 21494
		private NKCASUIUnitIllust m_NKCASUISpineIllust;

		// Token: 0x040053F7 RID: 21495
		private bool m_bForceReserveOpen;

		// Token: 0x040053F8 RID: 21496
		private const float UI_TRANSITION_TIME = 0.4f;

		// Token: 0x040053F9 RID: 21497
		private Dictionary<long, int> dicUnitLimitBreak = new Dictionary<long, int>();

		// Token: 0x040053FA RID: 21498
		private NKCUnitSortSystem.UnitListOptions m_preUnitListOption;

		// Token: 0x040053FB RID: 21499
		private NKCUnitSortSystem.eFilterOption m_preFilterOption;

		// Token: 0x040053FC RID: 21500
		private int m_SelectUnitIndex;

		// Token: 0x040053FD RID: 21501
		private List<NKMUnitData> m_UnitSortList = new List<NKMUnitData>();

		// Token: 0x040053FE RID: 21502
		private int m_iCheckUnitCnt;

		// Token: 0x040053FF RID: 21503
		private int m_iMaxUnitCnt;

		// Token: 0x04005400 RID: 21504
		private bool bNextClick;

		// Token: 0x04005401 RID: 21505
		protected NKCUILab.ContractBannerInfo m_BannerSet = new NKCUILab.ContractBannerInfo();

		// Token: 0x0200169C RID: 5788
		public enum LAB_DETAIL_STATE
		{
			// Token: 0x0400A4DF RID: 42207
			LDS_INVALID,
			// Token: 0x0400A4E0 RID: 42208
			LDS_MENU,
			// Token: 0x0400A4E1 RID: 42209
			LDS_UNIT_ENHANCE,
			// Token: 0x0400A4E2 RID: 42210
			LDS_UNIT_LIMITBREAK,
			// Token: 0x0400A4E3 RID: 42211
			LDS_UNIT_SKILL_TRAIN
		}

		// Token: 0x0200169D RID: 5789
		private enum UI_ANIM_TYPE
		{
			// Token: 0x0400A4E5 RID: 42213
			UAT_INVALID = -1,
			// Token: 0x0400A4E6 RID: 42214
			UAT_IN,
			// Token: 0x0400A4E7 RID: 42215
			UAT_OUT
		}

		// Token: 0x0200169E RID: 5790
		// (Invoke) Token: 0x0600B0C5 RID: 45253
		public delegate void OnSelectUnitPlayVoice(NKM_UNIT_STYLE_TYPE unitStyleType, NKCUILab.LAB_DETAIL_STATE labState);

		// Token: 0x0200169F RID: 5791
		public enum MenuTransitionType
		{
			// Token: 0x0400A4E9 RID: 42217
			Start,
			// Token: 0x0400A4EA RID: 42218
			MenuToDetail,
			// Token: 0x0400A4EB RID: 42219
			DetailToMenu,
			// Token: 0x0400A4EC RID: 42220
			CharacterChange,
			// Token: 0x0400A4ED RID: 42221
			DetailToDetail,
			// Token: 0x0400A4EE RID: 42222
			MenuDirectOpen
		}

		// Token: 0x020016A0 RID: 5792
		public struct strSpineInfo
		{
			// Token: 0x0600B0C8 RID: 45256 RVA: 0x0035F954 File Offset: 0x0035DB54
			public strSpineInfo(int idx, GameObject obj)
			{
				this.index = idx;
				this.objSpine = obj;
			}

			// Token: 0x0400A4EF RID: 42223
			public int index;

			// Token: 0x0400A4F0 RID: 42224
			public GameObject objSpine;
		}

		// Token: 0x020016A1 RID: 5793
		public class ContractBannerInfo
		{
			// Token: 0x0400A4F1 RID: 42225
			public Dictionary<int, NKCUILab.strSpineInfo> m_LstBanner = new Dictionary<int, NKCUILab.strSpineInfo>();
		}
	}
}
