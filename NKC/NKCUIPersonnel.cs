using System;
using System.Collections.Generic;
using Cs.Math;
using NKC.UI.Component;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009C7 RID: 2503
	public class NKCUIPersonnel : NKCUIBase
	{
		// Token: 0x1700124A RID: 4682
		// (get) Token: 0x06006AAF RID: 27311 RVA: 0x0022AA04 File Offset: 0x00228C04
		public static NKCUIPersonnel Instance
		{
			get
			{
				if (NKCUIPersonnel.m_Instance == null)
				{
					NKCUIPersonnel.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPersonnel>("ab_ui_nkm_ui_personnel", "NKM_UI_PERSONNEL_LIFETIME", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPersonnel.CleanupInstance)).GetInstance<NKCUIPersonnel>();
					NKCUIPersonnel.m_Instance.Init();
				}
				return NKCUIPersonnel.m_Instance;
			}
		}

		// Token: 0x1700124B RID: 4683
		// (get) Token: 0x06006AB0 RID: 27312 RVA: 0x0022AA53 File Offset: 0x00228C53
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIPersonnel.m_Instance != null && NKCUIPersonnel.m_Instance.IsOpen;
			}
		}

		// Token: 0x06006AB1 RID: 27313 RVA: 0x0022AA6E File Offset: 0x00228C6E
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPersonnel.m_Instance != null && NKCUIPersonnel.m_Instance.IsOpen)
			{
				NKCUIPersonnel.m_Instance.Close();
			}
		}

		// Token: 0x06006AB2 RID: 27314 RVA: 0x0022AA93 File Offset: 0x00228C93
		private static void CleanupInstance()
		{
			NKCUIPersonnel.m_Instance = null;
		}

		// Token: 0x1700124C RID: 4684
		// (get) Token: 0x06006AB3 RID: 27315 RVA: 0x0022AA9B File Offset: 0x00228C9B
		public override string GuideTempletID
		{
			get
			{
				return "ARTICLE_UNIT_WEDDING_CONTRACT";
			}
		}

		// Token: 0x1700124D RID: 4685
		// (get) Token: 0x06006AB4 RID: 27316 RVA: 0x0022AAA2 File Offset: 0x00228CA2
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_LIFETIME;
			}
		}

		// Token: 0x1700124E RID: 4686
		// (get) Token: 0x06006AB5 RID: 27317 RVA: 0x0022AAA9 File Offset: 0x00228CA9
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x06006AB6 RID: 27318 RVA: 0x0022AAAC File Offset: 0x00228CAC
		private void Init()
		{
			this.m_btnSelectUnit.PointerClick.RemoveAllListeners();
			this.m_btnSelectUnit.PointerClick.AddListener(new UnityAction(this.OnSelectUnit));
			this.m_unitInfoSummary.Init(true);
			this.m_btnLifetimeOK.PointerClick.RemoveAllListeners();
			this.m_btnLifetimeOK.PointerClick.AddListener(new UnityAction(this.OnTouchLifetime));
			this.m_btnLifetimeOK.m_ButtonBG_Locked = this.m_objLifetimeOK_off;
			this.m_btnLifetimeInfo.PointerClick.RemoveAllListeners();
			this.m_btnLifetimeInfo.PointerClick.AddListener(new UnityAction(this.OnTouchLifetimeInfo));
			this.m_btnLifetimeDefaultInfo.PointerClick.RemoveAllListeners();
			this.m_btnLifetimeDefaultInfo.PointerClick.AddListener(new UnityAction(this.OnTouchLifetimeInfo));
			this.m_btnLifetimeDefaultInfo2.PointerClick.RemoveAllListeners();
			this.m_btnLifetimeDefaultInfo2.PointerClick.AddListener(new UnityAction(this.OnTouchLifetimeInfo));
			if (this.m_UnitDragSelectView != null)
			{
				this.m_UnitDragSelectView.Init(true, true);
				this.m_UnitDragSelectView.dOnGetObject += this.MakeMainBannerListSlot;
				this.m_UnitDragSelectView.dOnReturnObject += new NKCUIComDragSelectablePanel.OnReturnObject(this.ReturnMainBannerListSlot);
				this.m_UnitDragSelectView.dOnProvideData += new NKCUIComDragSelectablePanel.OnProvideData(this.ProvideMainBannerListSlotData);
				this.m_UnitDragSelectView.dOnIndexChangeListener += this.SelectCharacter;
				this.m_UnitDragSelectView.dOnFocus += this.Focus;
			}
			if (this.m_evtPanel != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(new UnityAction<BaseEventData>(this.OnEventPanelClick));
				this.m_evtPanel.triggers.Add(entry);
			}
		}

		// Token: 0x06006AB7 RID: 27319 RVA: 0x0022AC86 File Offset: 0x00228E86
		public void Open()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.UpdateUI();
			base.UIOpened(true);
			NKCTutorialManager.TutorialRequired(TutorialPoint.Lifetime, true);
		}

		// Token: 0x06006AB8 RID: 27320 RVA: 0x0022ACAA File Offset: 0x00228EAA
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.BannerCleanUp();
			this.m_targetUnit = null;
			this.CloseSelectInstance();
		}

		// Token: 0x06006AB9 RID: 27321 RVA: 0x0022ACCB File Offset: 0x00228ECB
		public override void UnHide()
		{
			base.UnHide();
			this.UpdateUI();
		}

		// Token: 0x06006ABA RID: 27322 RVA: 0x0022ACDC File Offset: 0x00228EDC
		public void ReserveUnitData(NKMUnitData unitData)
		{
			List<NKMUnitData> unitUIDList = new List<NKMUnitData>
			{
				unitData
			};
			if (unitData != null)
			{
				this.OnUnitSortList(unitData.m_UnitUID, unitUIDList);
			}
			this.m_targetUnit = unitData;
			if (unitData != null)
			{
				NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_NEGOTITATE_READY, unitData, false, false);
			}
			this.UpdateUI();
		}

		// Token: 0x06006ABB RID: 27323 RVA: 0x0022AD21 File Offset: 0x00228F21
		private void SetData(NKMUnitData unitData)
		{
			this.m_targetUnit = unitData;
			if (unitData != null)
			{
				NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_NEGOTITATE_READY, unitData, false, false);
			}
			this.UpdateUI();
		}

		// Token: 0x06006ABC RID: 27324 RVA: 0x0022AD40 File Offset: 0x00228F40
		private void UpdateUI()
		{
			NKCUtil.SetGameobjectActive(this.m_btnSelectUnit, this.m_targetUnit != null);
			this.OpenUnitSelect();
			this.SetUnit(this.m_targetUnit);
			this.SetTalk(this.m_targetUnit);
			this.SetInfo(this.m_targetUnit);
			this.SetCost();
			this.SetButton(this.m_targetUnit);
			this.m_NKCUIPersonnelShortCutMenu.SetData(NKC_SCEN_BASE.eUIOpenReserve.Personnel_Lifetime);
		}

		// Token: 0x06006ABD RID: 27325 RVA: 0x0022ADAA File Offset: 0x00228FAA
		private void SetUnit(NKMUnitData unitData)
		{
			this.SetUICharInfo(unitData);
			if (this.m_UnitDragSelectView != null)
			{
				this.m_UnitDragSelectView.SetArrow(unitData == null);
			}
		}

		// Token: 0x06006ABE RID: 27326 RVA: 0x0022ADD0 File Offset: 0x00228FD0
		private void SetUICharInfo(NKMUnitData unitData)
		{
			NKCUtil.SetGameobjectActive(this.m_unitInfoSummary, unitData != null);
			if (unitData != null)
			{
				this.m_unitInfoSummary.SetData(unitData);
			}
		}

		// Token: 0x06006ABF RID: 27327 RVA: 0x0022ADF0 File Offset: 0x00228FF0
		private void SetTalk(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				NKCUtil.SetGameobjectActive(this.m_talkBox, false);
				return;
			}
			string speech = NKCNegotiateManager.GetSpeech(unitData, NKCNegotiateManager.SpeechType.Ready);
			if (string.IsNullOrEmpty(speech))
			{
				NKCUtil.SetGameobjectActive(this.m_talkBox, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_talkBox, true);
			this.m_talkBox.SetText(speech, 0f, 0f);
		}

		// Token: 0x06006AC0 RID: 27328 RVA: 0x0022AE4C File Offset: 0x0022904C
		private void SetInfo(NKMUnitData unitData)
		{
			bool flag = unitData != null;
			NKCUtil.SetGameobjectActive(this.m_objLifetimeDefault, !flag);
			NKCUtil.SetGameobjectActive(this.m_objLifetimeReady, flag);
			if (flag)
			{
				int loyalty = unitData.loyalty;
				int num = 10000;
				NKCUtil.SetLabelText(this.m_txtLoyaltyGauge, string.Format("{0}/{1}", loyalty / 100, num / 100));
				this.m_imgLoyaltyGauge.fillAmount = (float)loyalty / (float)num;
			}
		}

		// Token: 0x06006AC1 RID: 27329 RVA: 0x0022AEC0 File Offset: 0x002290C0
		private void SetCost()
		{
			long countMiscItem = NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(1024);
			this.m_costLifetime.SetData(1024, 1, countMiscItem, true, true, false);
		}

		// Token: 0x06006AC2 RID: 27330 RVA: 0x0022AEF8 File Offset: 0x002290F8
		private void SetButton(NKMUnitData unitData)
		{
			bool flag = this.CanLifetime(unitData) == NKM_ERROR_CODE.NEC_OK;
			if (flag)
			{
				this.m_btnLifetimeOK.UnLock(false);
			}
			else
			{
				this.m_btnLifetimeOK.Lock(false);
			}
			this.m_txtLifetimeOK.color = NKCUtil.GetButtonUIColor(flag);
			this.m_imgLifetimeOK.color = NKCUtil.GetButtonUIColor(flag);
		}

		// Token: 0x06006AC3 RID: 27331 RVA: 0x0022AF50 File Offset: 0x00229150
		private void OnTouchLifetime()
		{
			if (this.m_targetUnit == null)
			{
				return;
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = this.CanLifetime(this.m_targetUnit);
			if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_OK)
			{
				NKCUILifetime.Instance.Open(this.m_targetUnit, false);
				return;
			}
			if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_ITEM)
			{
				NKCShopManager.OpenItemLackPopup(1024, 1);
				return;
			}
			NKCPopupOKCancel.OpenOKBox(nkm_ERROR_CODE, null, "");
		}

		// Token: 0x06006AC4 RID: 27332 RVA: 0x0022AFA5 File Offset: 0x002291A5
		private void OnTouchLifetimeInfo()
		{
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_INFORMATION, NKCUtilString.GET_STRING_LIFETIME_REWARD_INFO, null, "");
		}

		// Token: 0x06006AC5 RID: 27333 RVA: 0x0022AFBC File Offset: 0x002291BC
		private NKM_ERROR_CODE CanLifetime(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_NOT_EXIST;
			}
			NKCCollectionUnitTemplet unitTemplet = NKCCollectionManager.GetUnitTemplet(unitData.m_UnitID);
			if (unitTemplet == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_NOT_EXIST;
			}
			if (string.IsNullOrEmpty(unitTemplet.m_CutsceneLifetime_Start))
			{
				return NKM_ERROR_CODE.NEC_FAIL_PERMANENT_CONTRACT_INVALID_CONDITION;
			}
			if (NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(1024) < 1L)
			{
				return NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_ITEM;
			}
			if (!unitData.IsPermanentContractEnable())
			{
				return NKM_ERROR_CODE.NEC_FAIL_PERMANENT_CONTRACT_INVALID_CONDITION;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06006AC6 RID: 27334 RVA: 0x0022B020 File Offset: 0x00229220
		private void OnSelectUnit()
		{
			NKCUIUnitSelectList.Instance.Open(this.MakeSelectUnitOption(), new NKCUIUnitSelectList.OnUnitSelectCommand(this.OnUnitSelected), new NKCUIUnitSelectList.OnUnitSortList(this.OnUnitSortList), null, null, null);
			if (this.m_NKCUIUnitSelect != null)
			{
				this.m_NKCUIUnitSelect.Close();
			}
		}

		// Token: 0x06006AC7 RID: 27335 RVA: 0x0022B074 File Offset: 0x00229274
		private NKCUIUnitSelectList.UnitSelectListOptions MakeSelectUnitOption()
		{
			NKCUIUnitSelectList.UnitSelectListOptions result = new NKCUIUnitSelectList.UnitSelectListOptions(NKM_UNIT_TYPE.NUT_NORMAL, false, NKM_DECK_TYPE.NDT_NORMAL, NKCUIUnitSelectList.eUnitSelectListMode.Normal, true);
			result.lstSortOption = NKCUnitSortSystem.GetDefaultSortOptions(NKM_UNIT_TYPE.NUT_NORMAL, false, false);
			result.bDescending = true;
			result.bExcludeLockedUnit = false;
			result.bExcludeDeckedUnit = false;
			result.bCanSelectUnitInMission = true;
			result.bHideDeckedUnit = false;
			result.strUpsideMenuName = NKCUtilString.GET_STRING_LIFETIME;
			result.strEmptyMessage = NKCUtilString.GET_STRING_LIFETIME_NO_EXIST_UNIT;
			result.bShowHideDeckedUnitMenu = false;
			result.bEnableLockUnitSystem = false;
			result.m_SortOptions.AdditionalExcludeFilterFunc = new NKCUnitSortSystem.UnitListOptions.CustomFilterFunc(this.CheckUnitCanLifetime);
			result.m_SortOptions.bIgnoreCityState = true;
			result.m_SortOptions.bIgnoreWorldMapLeader = true;
			result.bPushBackUnselectable = false;
			result.m_bUseFavorite = true;
			result.setUnitFilterCategory = NKCUnitSortSystem.setDefaultUnitFilterCategory;
			result.setUnitSortCategory = NKCUnitSortSystem.setDefaultUnitSortCategory;
			return result;
		}

		// Token: 0x06006AC8 RID: 27336 RVA: 0x0022B148 File Offset: 0x00229348
		private bool CheckUnitCanLifetime(NKMUnitData unitData)
		{
			NKCCollectionUnitTemplet unitTemplet = NKCCollectionManager.GetUnitTemplet(unitData.m_UnitID);
			return unitTemplet != null && !string.IsNullOrEmpty(unitTemplet.m_CutsceneLifetime_Start) && unitData.IsPermanentContractEnable();
		}

		// Token: 0x06006AC9 RID: 27337 RVA: 0x0022B17C File Offset: 0x0022937C
		private void OnUnitSelected(List<long> lstUnitUID)
		{
			if (lstUnitUID == null || lstUnitUID.Count < 1)
			{
				return;
			}
			NKCUIUnitSelectList.CheckInstanceAndClose();
			long unitUid = lstUnitUID[0];
			NKMUnitData unitFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(unitUid);
			this.SetData(unitFromUID);
		}

		// Token: 0x06006ACA RID: 27338 RVA: 0x0022B1BC File Offset: 0x002293BC
		private void OnEventPanelClick(BaseEventData e)
		{
			if (this.m_UnitDragSelectView != null && this.m_UnitDragSelectView.GetDragOffset().IsNearlyZero(1E-05f))
			{
				RectTransform currentItem = this.m_UnitDragSelectView.GetCurrentItem();
				if (currentItem != null)
				{
					NKCUICharacterView componentInChildren = currentItem.GetComponentInChildren<NKCUICharacterView>();
					if (componentInChildren != null && componentInChildren.HasCharacterIllust())
					{
						PointerEventData eventData = new PointerEventData(EventSystem.current);
						componentInChildren.OnPointerDown(eventData);
						componentInChildren.OnPointerUp(eventData);
					}
				}
			}
		}

		// Token: 0x06006ACB RID: 27339 RVA: 0x0022B234 File Offset: 0x00229434
		private RectTransform MakeMainBannerListSlot()
		{
			GameObject gameObject = new GameObject("Banner", new Type[]
			{
				typeof(RectTransform),
				typeof(LayoutElement)
			});
			LayoutElement component = gameObject.GetComponent<LayoutElement>();
			component.ignoreLayout = false;
			component.preferredWidth = this.m_UnitDragSelectView.m_rtContentRect.GetWidth();
			component.preferredHeight = this.m_UnitDragSelectView.m_rtContentRect.GetHeight();
			component.flexibleWidth = 2f;
			component.flexibleHeight = 2f;
			return gameObject.GetComponent<RectTransform>();
		}

		// Token: 0x06006ACC RID: 27340 RVA: 0x0022B2C0 File Offset: 0x002294C0
		private void ProvideMainBannerListSlotData(Transform tr, int idx)
		{
			if (this.m_UnitSortList != null)
			{
				NKMUnitData nkmunitData = this.m_UnitSortList[idx];
				Debug.Log(string.Format("<color=yellow>target : {0}, idx : {1}, </color>", tr.name, idx));
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
		}

		// Token: 0x06006ACD RID: 27341 RVA: 0x0022B353 File Offset: 0x00229553
		private void ReturnMainBannerListSlot(Transform go)
		{
			NKCUtil.SetGameobjectActive(go, false);
			UnityEngine.Object.Destroy(go.gameObject);
		}

		// Token: 0x06006ACE RID: 27342 RVA: 0x0022B368 File Offset: 0x00229568
		public void TouchCharacter(RectTransform rt, PointerEventData eventData)
		{
			if (this.m_UnitDragSelectView.GetDragOffset().IsNearlyZero(1E-05f))
			{
				NKCUICharacterView componentInChildren = rt.GetComponentInChildren<NKCUICharacterView>();
				if (componentInChildren != null)
				{
					componentInChildren.OnPointerDown(eventData);
					componentInChildren.OnPointerUp(eventData);
				}
			}
		}

		// Token: 0x06006ACF RID: 27343 RVA: 0x0022B3AA File Offset: 0x002295AA
		private void Focus(RectTransform rect, bool bFocus)
		{
			NKCUtil.SetGameobjectActive(rect.gameObject, bFocus);
		}

		// Token: 0x06006AD0 RID: 27344 RVA: 0x0022B3B8 File Offset: 0x002295B8
		public void SelectCharacter(int idx)
		{
			Debug.Log(string.Format("<color=yellow>SelectCharacter {0}</color>", idx));
			if (this.m_UnitSortList.Count < idx || idx < 0)
			{
				Debug.LogWarning(string.Format("무언가 잘못된 인덱스 인디? 총 갯수 : {0}, 목표 인덱스 : {1}", this.m_UnitSortList.Count, idx));
				return;
			}
			NKMUnitData nkmunitData = this.m_UnitSortList[idx];
			if (nkmunitData != null)
			{
				this.SetData(nkmunitData);
			}
		}

		// Token: 0x06006AD1 RID: 27345 RVA: 0x0022B42C File Offset: 0x0022962C
		private void BannerCleanUp()
		{
			if (this.m_UnitDragSelectView != null)
			{
				NKCUICharacterView[] componentsInChildren = this.m_UnitDragSelectView.gameObject.GetComponentsInChildren<NKCUICharacterView>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].CloseImmediatelyIllust();
				}
				return;
			}
		}

		// Token: 0x06006AD2 RID: 27346 RVA: 0x0022B470 File Offset: 0x00229670
		private void OnUnitSortList(long UID, List<NKMUnitData> unitUIDList)
		{
			this.m_UnitSortList = unitUIDList;
			if (this.m_UnitDragSelectView != null)
			{
				this.m_UnitDragSelectView.TotalCount = this.m_UnitSortList.Count;
				if (this.m_UnitSortList.Count > 0)
				{
					for (int i = 0; i < this.m_UnitSortList.Count; i++)
					{
						if (this.m_UnitSortList[i].m_UnitUID == UID)
						{
							this.m_UnitDragSelectView.SetIndex(i);
							return;
						}
					}
				}
			}
		}

		// Token: 0x06006AD3 RID: 27347 RVA: 0x0022B4F0 File Offset: 0x002296F0
		private void OpenSelectInstance()
		{
			NKCUIPersonnel.m_AssetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("ab_ui_nuf_base", "NKM_UI_BASE_UNIT_SELECT", false, null);
			if (NKCUIPersonnel.m_AssetInstanceData.m_Instant != null)
			{
				this.m_NKCUIUnitSelect = NKCUIPersonnel.m_AssetInstanceData.m_Instant.GetComponent<NKCUIUnitSelect>();
				this.m_NKCUIUnitSelect.Init(new UnityAction(this.OnClickCharacterChange));
				this.m_NKCUIUnitSelect.transform.SetParent(this.m_rtUnitSelectAnchor.transform, false);
				this.m_NKCUIUnitSelect.Open();
			}
		}

		// Token: 0x06006AD4 RID: 27348 RVA: 0x0022B578 File Offset: 0x00229778
		private void CloseSelectInstance()
		{
			if (NKCUIPersonnel.m_AssetInstanceData != null)
			{
				NKCUIPersonnel.m_AssetInstanceData.Unload();
			}
		}

		// Token: 0x06006AD5 RID: 27349 RVA: 0x0022B58B File Offset: 0x0022978B
		private void OnClickCharacterChange()
		{
			if (this.m_NKCUIUnitSelect != null)
			{
				this.m_NKCUIUnitSelect.Outro();
			}
			this.OnSelectUnit();
		}

		// Token: 0x06006AD6 RID: 27350 RVA: 0x0022B5AC File Offset: 0x002297AC
		private void OpenUnitSelect()
		{
			if (this.m_targetUnit != null)
			{
				if (this.m_NKCUIUnitSelect != null)
				{
					this.m_NKCUIUnitSelect.Close();
				}
				return;
			}
			if (this.m_NKCUIUnitSelect != null)
			{
				this.m_NKCUIUnitSelect.Open();
				return;
			}
			this.OpenSelectInstance();
		}

		// Token: 0x0400566F RID: 22127
		public const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_personnel";

		// Token: 0x04005670 RID: 22128
		public const string UI_ASSET_NAME = "NKM_UI_PERSONNEL_LIFETIME";

		// Token: 0x04005671 RID: 22129
		private static NKCUIPersonnel m_Instance;

		// Token: 0x04005672 RID: 22130
		public NKCUIPersonnelShortCutMenu m_NKCUIPersonnelShortCutMenu;

		// Token: 0x04005673 RID: 22131
		public NKCUICharInfoSummary m_unitInfoSummary;

		// Token: 0x04005674 RID: 22132
		public NKCUIComDragSelectablePanel m_UnitDragSelectView;

		// Token: 0x04005675 RID: 22133
		public EventTrigger m_evtPanel;

		// Token: 0x04005676 RID: 22134
		public NKCComUITalkBox m_talkBox;

		// Token: 0x04005677 RID: 22135
		public RectTransform m_rtUnitSelectAnchor;

		// Token: 0x04005678 RID: 22136
		public NKCUIComStateButton m_btnSelectUnit;

		// Token: 0x04005679 RID: 22137
		[Header("종신계약")]
		public NKCUIItemCostSlot m_costLifetime;

		// Token: 0x0400567A RID: 22138
		public NKCUIComStateButton m_btnLifetimeOK;

		// Token: 0x0400567B RID: 22139
		public GameObject m_objLifetimeOK_off;

		// Token: 0x0400567C RID: 22140
		public Text m_txtLifetimeOK;

		// Token: 0x0400567D RID: 22141
		public Image m_imgLifetimeOK;

		// Token: 0x0400567E RID: 22142
		public GameObject m_objLifetimeReady;

		// Token: 0x0400567F RID: 22143
		public NKCUIComStateButton m_btnLifetimeInfo;

		// Token: 0x04005680 RID: 22144
		public Text m_txtLoyaltyGauge;

		// Token: 0x04005681 RID: 22145
		public Image m_imgLoyaltyGauge;

		// Token: 0x04005682 RID: 22146
		public GameObject m_objLifetimeDefault;

		// Token: 0x04005683 RID: 22147
		public NKCUIComStateButton m_btnLifetimeDefaultInfo;

		// Token: 0x04005684 RID: 22148
		public NKCUIComStateButton m_btnLifetimeDefaultInfo2;

		// Token: 0x04005685 RID: 22149
		private const int LIFETIME_ITEM_REQUIRE_COUNT = 1;

		// Token: 0x04005686 RID: 22150
		private NKMUnitData m_targetUnit;

		// Token: 0x04005687 RID: 22151
		private List<NKMUnitData> m_UnitSortList = new List<NKMUnitData>();

		// Token: 0x04005688 RID: 22152
		private static NKCAssetInstanceData m_AssetInstanceData;

		// Token: 0x04005689 RID: 22153
		private const string ASSET_SELECT_BUNDLE_NAME = "ab_ui_nuf_base";

		// Token: 0x0400568A RID: 22154
		private const string UI_SELECT_ASSET_NAME = "NKM_UI_BASE_UNIT_SELECT";

		// Token: 0x0400568B RID: 22155
		private NKCUIUnitSelect m_NKCUIUnitSelect;
	}
}
