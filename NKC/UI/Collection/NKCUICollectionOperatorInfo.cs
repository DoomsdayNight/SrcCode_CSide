using System;
using System.Collections.Generic;
using NKC.UI.Component;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Collection
{
	// Token: 0x02000C24 RID: 3108
	public class NKCUICollectionOperatorInfo : NKCUIBase
	{
		// Token: 0x170016CA RID: 5834
		// (get) Token: 0x06008FE2 RID: 36834 RVA: 0x0030EA70 File Offset: 0x0030CC70
		public static NKCUICollectionOperatorInfo Instance
		{
			get
			{
				if (NKCUICollectionOperatorInfo.m_Instance == null)
				{
					NKCUICollectionOperatorInfo.m_loadedUIData = NKCUIManager.OpenNewInstance<NKCUICollectionOperatorInfo>("ab_ui_nkm_ui_collection", "NKM_UI_COLLECTION_OPERATOR_INFO", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUICollectionOperatorInfo.CleanupInstance));
					NKCUICollectionOperatorInfo.m_Instance = NKCUICollectionOperatorInfo.m_loadedUIData.GetInstance<NKCUICollectionOperatorInfo>();
					NKCUICollectionOperatorInfo.m_Instance.Init();
				}
				return NKCUICollectionOperatorInfo.m_Instance;
			}
		}

		// Token: 0x06008FE3 RID: 36835 RVA: 0x0030EAC9 File Offset: 0x0030CCC9
		private static void CleanupInstance()
		{
			NKCUICollectionOperatorInfo.m_Instance = null;
		}

		// Token: 0x170016CB RID: 5835
		// (get) Token: 0x06008FE4 RID: 36836 RVA: 0x0030EAD1 File Offset: 0x0030CCD1
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUICollectionOperatorInfo.m_Instance != null && NKCUICollectionOperatorInfo.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008FE5 RID: 36837 RVA: 0x0030EAEC File Offset: 0x0030CCEC
		public static void CheckInstanceAndClose()
		{
			if (NKCUICollectionOperatorInfo.m_loadedUIData != null)
			{
				NKCUICollectionOperatorInfo.m_loadedUIData.CloseInstance();
			}
		}

		// Token: 0x06008FE6 RID: 36838 RVA: 0x0030EAFF File Offset: 0x0030CCFF
		private void OnDestroy()
		{
		}

		// Token: 0x170016CC RID: 5836
		// (get) Token: 0x06008FE7 RID: 36839 RVA: 0x0030EB01 File Offset: 0x0030CD01
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_UNIT_INFO;
			}
		}

		// Token: 0x170016CD RID: 5837
		// (get) Token: 0x06008FE8 RID: 36840 RVA: 0x0030EB08 File Offset: 0x0030CD08
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170016CE RID: 5838
		// (get) Token: 0x06008FE9 RID: 36841 RVA: 0x0030EB0B File Offset: 0x0030CD0B
		public override bool WillCloseUnderPopupOnOpen
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170016CF RID: 5839
		// (get) Token: 0x06008FEA RID: 36842 RVA: 0x0030EB0E File Offset: 0x0030CD0E
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return this.m_UpsideMenuMode;
			}
		}

		// Token: 0x06008FEB RID: 36843 RVA: 0x0030EB16 File Offset: 0x0030CD16
		public void Init()
		{
			this.InitUI();
		}

		// Token: 0x06008FEC RID: 36844 RVA: 0x0030EB20 File Offset: 0x0030CD20
		public void InitUI()
		{
			NKCUtil.SetBindFunction(this.m_NKM_UI_COLLECTION_UNIT_INFO_BOTTOM_BUTTON_ILLUST_CHANGE, new UnityAction(this.OnClickChangeIllust));
			NKCUtil.SetBindFunction(this.m_NKM_UI_COLLECTION_UNIT_INFO_BOTTOM_BUTTON_APPRAISAL, new UnityAction(this.OnUnitAppraisal));
			NKCUtil.SetBindFunction(this.m_NKM_UI_COLLECTION_UNIT_PROFILE_VOICE_BUTTON, new UnityAction(this.OnUnitVoice));
			NKCUtil.SetBindFunction(this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_PROFILE, delegate()
			{
				this.ChangeState(NKCUICollectionOperatorInfo.eCollectionState.CS_PROFILE);
			});
			NKCUtil.SetBindFunction(this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_STAT, delegate()
			{
				this.ChangeState(NKCUICollectionOperatorInfo.eCollectionState.CS_STATUS);
			});
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
			NKCUtil.SetBindFunction(this.m_SkillBtn, new UnityAction(this.OnClickSkillInfo));
			base.gameObject.SetActive(false);
		}

		// Token: 0x06008FED RID: 36845 RVA: 0x0030EC98 File Offset: 0x0030CE98
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

		// Token: 0x06008FEE RID: 36846 RVA: 0x0030ED03 File Offset: 0x0030CF03
		private void ChangeUnit(NKMOperator operatorData)
		{
			this.m_NKCUIOperatorSummary.SetData(operatorData);
			this.SetData(operatorData, false, false);
		}

		// Token: 0x06008FEF RID: 36847 RVA: 0x0030ED1C File Offset: 0x0030CF1C
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

		// Token: 0x06008FF0 RID: 36848 RVA: 0x0030EDA8 File Offset: 0x0030CFA8
		private void ProvideMainBannerListSlotData(Transform tr, int idx)
		{
			if (this.m_OpenOption != null && this.m_OpenOption.m_lstOperatorData != null)
			{
				NKMOperator nkmoperator = this.m_OpenOption.m_lstOperatorData[idx];
				if (nkmoperator != null)
				{
					NKCUICharacterView component = tr.GetComponent<NKCUICharacterView>();
					if (component != null)
					{
						component.CloseImmediatelyIllust();
						component.SetCharacterIllust(nkmoperator, false, true, true, 0);
						return;
					}
					NKCUICharacterView nkcuicharacterView = tr.gameObject.AddComponent<NKCUICharacterView>();
					nkcuicharacterView.m_rectIllustRoot = tr.GetComponent<RectTransform>();
					nkcuicharacterView.SetCharacterIllust(nkmoperator, false, true, true, 0);
				}
			}
		}

		// Token: 0x06008FF1 RID: 36849 RVA: 0x0030EE22 File Offset: 0x0030D022
		private void ReturnMainBannerListSlot(Transform go)
		{
			NKCUtil.SetGameobjectActive(go, false);
			UnityEngine.Object.Destroy(go.gameObject);
		}

		// Token: 0x06008FF2 RID: 36850 RVA: 0x0030EE38 File Offset: 0x0030D038
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

		// Token: 0x06008FF3 RID: 36851 RVA: 0x0030EE75 File Offset: 0x0030D075
		private void Focus(RectTransform rect, bool bFocus)
		{
			NKCUtil.SetGameobjectActive(rect.gameObject, bFocus);
		}

		// Token: 0x06008FF4 RID: 36852 RVA: 0x0030EE84 File Offset: 0x0030D084
		private void FocusColor(RectTransform rect, Color ApplyColor)
		{
			NKCUICharacterView componentInChildren = rect.gameObject.GetComponentInChildren<NKCUICharacterView>();
			if (componentInChildren != null)
			{
				componentInChildren.SetColor(ApplyColor, false);
			}
		}

		// Token: 0x06008FF5 RID: 36853 RVA: 0x0030EEB0 File Offset: 0x0030D0B0
		public void SelectCharacter(int idx)
		{
			if (this.m_OpenOption.m_lstOperatorData.Count < idx || idx < 0)
			{
				return;
			}
			NKMOperator nkmoperator = this.m_OpenOption.m_lstOperatorData[idx];
			if (nkmoperator != null)
			{
				this.ChangeUnit(nkmoperator);
			}
		}

		// Token: 0x06008FF6 RID: 36854 RVA: 0x0030EEF4 File Offset: 0x0030D0F4
		private void BannerCleanUp()
		{
			if (this.m_DragCharacterView != null)
			{
				NKCUICharacterView[] componentsInChildren = this.m_DragCharacterView.gameObject.GetComponentsInChildren<NKCUICharacterView>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].CloseImmediatelyIllust();
				}
				return;
			}
		}

		// Token: 0x06008FF7 RID: 36855 RVA: 0x0030EF38 File Offset: 0x0030D138
		public void Open(NKMOperator operatorData, NKCUIOperatorInfo.OpenOption openOption = null, NKCUICollectionOperatorInfo.eCollectionState startingState = NKCUICollectionOperatorInfo.eCollectionState.CS_PROFILE, NKCUIUpsideMenu.eMode upsideMenuMode = NKCUIUpsideMenu.eMode.Normal, bool isGauntlet = false, bool bRecord = false)
		{
			bool bForceUpdate = false;
			this.m_isGauntlet = isGauntlet;
			if (this.m_isGauntlet)
			{
				bForceUpdate = true;
			}
			this.m_eCurrentState = NKCUICollectionOperatorInfo.eCollectionState.CS_NONE;
			this.m_UpsideMenuMode = upsideMenuMode;
			this.SetData(operatorData, bForceUpdate, false);
			this.SetData(operatorData, bRecord, false);
			this.ChangeState(startingState);
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			if (openOption == null)
			{
				openOption = new NKCUIOperatorInfo.OpenOption(new List<long>(), 0);
				openOption.m_lstOperatorData.Add(operatorData);
			}
			this.m_OpenOption = openOption;
			if (this.m_DragCharacterView != null)
			{
				if (this.m_OpenOption.m_lstOperatorData.Count == 0)
				{
					this.m_OpenOption.m_lstOperatorData.Add(operatorData);
				}
				this.m_DragCharacterView.TotalCount = this.m_OpenOption.m_lstOperatorData.Count;
				for (int i = 0; i < this.m_OpenOption.m_lstOperatorData.Count; i++)
				{
					if (this.m_OpenOption.m_lstOperatorData[i].uid == operatorData.uid)
					{
						this.m_DragCharacterView.SetIndex(i);
						break;
					}
				}
			}
			base.UIOpened(true);
		}

		// Token: 0x06008FF8 RID: 36856 RVA: 0x0030F05B File Offset: 0x0030D25B
		private void ChangeState(NKCUICollectionOperatorInfo.eCollectionState newStat)
		{
			if (this.m_eCurrentState == newStat || this.m_bViewMode)
			{
				return;
			}
			this.m_eCurrentState = newStat;
			this.UpdateUI();
		}

		// Token: 0x06008FF9 RID: 36857 RVA: 0x0030F07C File Offset: 0x0030D27C
		private void UpdateUI()
		{
			NKCUICollectionOperatorInfo.eCollectionState eCurrentState = this.m_eCurrentState;
			if (eCurrentState == NKCUICollectionOperatorInfo.eCollectionState.CS_PROFILE)
			{
				NKCUtil.SetGameobjectActive(this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_PROFILE.gameObject, true);
				NKCUtil.SetGameobjectActive(this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_STAT.gameObject, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_PROFILE, true);
				this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_PROFILE.Select(true, false, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_STAT, false);
				this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_STAT.Select(false, false, false);
				return;
			}
			if (eCurrentState != NKCUICollectionOperatorInfo.eCollectionState.CS_STATUS)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_PROFILE.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_STAT.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_PROFILE, false);
			this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_PROFILE.Select(false, false, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_STAT, true);
			this.m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_STAT.Select(true, false, false);
		}

		// Token: 0x06008FFA RID: 36858 RVA: 0x0030F14C File Offset: 0x0030D34C
		private void SetData(NKMOperator cNKMOperator, bool bForceUpdate = false, bool bRecord = false)
		{
			if (cNKMOperator != null && this.m_NKMOperator != null && cNKMOperator.uid == this.m_NKMOperator.uid && !bForceUpdate)
			{
				return;
			}
			this.m_NKMOperator = cNKMOperator;
			this.UpdateSkillInfo();
			this.SetUnitDiscription(this.m_NKMOperator.id);
			this.SetDetailedStat(this.m_NKMOperator);
			this.m_NKCUIOperatorSummary.SetData(this.m_NKMOperator);
			if (this.m_isGauntlet)
			{
				this.m_NKCUIOperatorSummary.ShowLevelExpGauge(false);
			}
			this.CheckHasUnit(this.m_NKMOperator.id);
			this.SetVoiceButtonUI();
			if (bRecord)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_PROFILE_VOICE_BUTTON, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_PROFILE_VOICE_BUTTON_DISABLE, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_INFO_BOTTOM_BUTTON_APPRAISAL, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_INFO_NOTICE_NOTGET, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_INFO_BOTTOM_BUTTON_APPRAISAL, NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.UNIT_REVIEW_SYSTEM));
			}
			NKCUtil.SetLabelText(this.m_lbVoiceActorName, NKCCollectionManager.GetVoiceActorName(this.m_NKMOperator.id));
		}

		// Token: 0x06008FFB RID: 36859 RVA: 0x0030F244 File Offset: 0x0030D444
		private void CheckHasUnit(int iUnitID)
		{
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_INFO_NOTICE_NOTGET, armyData.IsFirstGetUnit(iUnitID));
		}

		// Token: 0x06008FFC RID: 36860 RVA: 0x0030F270 File Offset: 0x0030D470
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

		// Token: 0x06008FFD RID: 36861 RVA: 0x0030F2B8 File Offset: 0x0030D4B8
		private void SetDetailedStat(NKMOperator operatorData)
		{
			if (operatorData == null)
			{
				return;
			}
			if (this.m_NKMOperator == null)
			{
				NKMOperator nkmoperator = new NKMOperator();
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(operatorData.id);
				if (unitTempletBase != null)
				{
					nkmoperator.id = operatorData.id;
					NKMOperatorSkillTemplet skillTemplet = NKCOperatorUtil.GetSkillTemplet(unitTempletBase.m_lstSkillStrID[0]);
					if (skillTemplet != null)
					{
						nkmoperator.mainSkill = new NKMOperatorSkill
						{
							id = skillTemplet.m_OperSkillID,
							level = (byte)skillTemplet.m_MaxSkillLevel
						};
					}
					NKMOperatorRandomPassiveGroupTemplet nkmoperatorRandomPassiveGroupTemplet = NKMOperatorRandomPassiveGroupTemplet.Find(NKCOperatorUtil.GetPassiveGroupID(operatorData.id));
					if (nkmoperatorRandomPassiveGroupTemplet != null && nkmoperatorRandomPassiveGroupTemplet.Groups.Count > 0)
					{
						NKMOperatorSkillTemplet skillTemplet2 = NKCOperatorUtil.GetSkillTemplet(nkmoperatorRandomPassiveGroupTemplet.Groups[0].operSkillId);
						if (skillTemplet2 != null)
						{
							nkmoperator.subSkill = new NKMOperatorSkill
							{
								id = skillTemplet2.m_OperSkillID,
								level = (byte)skillTemplet2.m_MaxSkillLevel
							};
						}
					}
				}
				NKCUtil.SetLabelText(this.m_lbPower, this.CalculateOperatorOperationPower(operatorData).ToString());
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbPower, this.CalculateOperatorOperationPower(operatorData).ToString());
			}
			NKCUtil.SetLabelText(this.m_STAT_NUMBER_ATK, NKCOperatorUtil.GetStatPercentageString(operatorData, NKM_STAT_TYPE.NST_ATK) ?? "");
			NKCUtil.SetLabelText(this.m_STAT_NUMBER_DEF, NKCOperatorUtil.GetStatPercentageString(operatorData, NKM_STAT_TYPE.NST_DEF) ?? "");
			NKCUtil.SetLabelText(this.m_STAT_NUMBER_HP, NKCOperatorUtil.GetStatPercentageString(operatorData, NKM_STAT_TYPE.NST_HP) ?? "");
			NKCUtil.SetLabelText(this.m_STAT_NUMBER_SKILL_COOL, NKCOperatorUtil.GetStatPercentageString(operatorData, NKM_STAT_TYPE.NST_SKILL_COOL_TIME_REDUCE_RATE) ?? "");
		}

		// Token: 0x06008FFE RID: 36862 RVA: 0x0030F448 File Offset: 0x0030D648
		private int CalculateOperatorOperationPower(NKMOperator operatorData)
		{
			int result = 0;
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(operatorData.id);
			if (unitTempletBase != null && operatorData != null)
			{
				NKMOperatorSkillTemplet skillTemplet = NKCOperatorUtil.GetSkillTemplet(operatorData.mainSkill.id);
				NKMOperatorSkillTemplet skillTemplet2 = NKCOperatorUtil.GetSkillTemplet(operatorData.subSkill.id);
				float num = (skillTemplet != null) ? ((float)operatorData.mainSkill.level / (float)skillTemplet.m_MaxSkillLevel * 3000f) : 0f;
				float num2 = (skillTemplet2 != null) ? ((float)operatorData.subSkill.level / (float)skillTemplet2.m_MaxSkillLevel * 3000f) : 0f;
				float num3 = (float)operatorData.level / (float)NKMCommonConst.OperatorConstTemplet.unitMaximumLevel * 3000f;
				float num4 = 3000f;
				switch (unitTempletBase.m_NKM_UNIT_GRADE)
				{
				case NKM_UNIT_GRADE.NUG_N:
					num4 *= 0.1f;
					break;
				case NKM_UNIT_GRADE.NUG_R:
					num4 *= 0.3f;
					break;
				case NKM_UNIT_GRADE.NUG_SR:
					num4 *= 0.6f;
					break;
				}
				result = (int)(num + num2 + num3 + num4 + 0.5f);
			}
			return result;
		}

		// Token: 0x06008FFF RID: 36863 RVA: 0x0030F557 File Offset: 0x0030D757
		public override void OnBackButton()
		{
			if (this.m_bViewMode)
			{
				this.OnClickChangeIllust();
				return;
			}
			base.OnBackButton();
		}

		// Token: 0x06009000 RID: 36864 RVA: 0x0030F56E File Offset: 0x0030D76E
		public override void UnHide()
		{
			this.m_bAppraisal = false;
			base.UnHide();
		}

		// Token: 0x06009001 RID: 36865 RVA: 0x0030F57D File Offset: 0x0030D77D
		public override void CloseInternal()
		{
			this.BannerCleanUp();
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
			NKCPopupUnitInfoDetail.CheckInstanceAndClose();
			NKCUIPopupIllustView.CheckInstanceAndClose();
			this.m_NKMOperator = null;
		}

		// Token: 0x06009002 RID: 36866 RVA: 0x0030F5AF File Offset: 0x0030D7AF
		private void OnUnitAppraisal()
		{
			if (this.m_bViewMode)
			{
				return;
			}
			NKCUIUnitReview.Instance.OpenUI(this.m_NKMOperator.id);
			this.m_bAppraisal = true;
		}

		// Token: 0x06009003 RID: 36867 RVA: 0x0030F5D6 File Offset: 0x0030D7D6
		private void OnClickChangeIllust()
		{
			if (this.m_bAppraisal)
			{
				return;
			}
			NKCUIPopupIllustView.Instance.Open(this.m_NKMOperator);
		}

		// Token: 0x06009004 RID: 36868 RVA: 0x0030F5F4 File Offset: 0x0030D7F4
		private void SetVoiceButtonUI()
		{
			bool flag = false;
			if (this.m_NKMOperator != null && NKCUIVoiceManager.UnitHasVoice(NKMUnitManager.GetUnitTempletBase(this.m_NKMOperator.id)))
			{
				flag = true;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_PROFILE_VOICE_BUTTON, flag);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_UNIT_PROFILE_VOICE_BUTTON_DISABLE, !flag);
		}

		// Token: 0x06009005 RID: 36869 RVA: 0x0030F63F File Offset: 0x0030D83F
		private void OnUnitVoice()
		{
			if (this.m_NKMOperator == null)
			{
				return;
			}
			NKCUIPopupVoice.Instance.Open(this.m_NKMOperator.id, 0, false);
		}

		// Token: 0x06009006 RID: 36870 RVA: 0x0030F664 File Offset: 0x0030D864
		private void UpdateSkillInfo()
		{
			this.m_MainSkillCombo.SetData(this.m_NKMOperator.id);
			NKCUtil.SetGameobjectActive(this.m_SubSkill, this.m_NKMOperator.subSkill.id != 0);
			if (this.m_NKMOperator != null)
			{
				this.m_MainSkill.SetData(this.m_NKMOperator.mainSkill.id, (int)this.m_NKMOperator.mainSkill.level, false);
				this.m_SubSkill.SetData(this.m_NKMOperator.subSkill.id, (int)this.m_NKMOperator.subSkill.level, false);
			}
			else
			{
				this.m_MainSkill.SetDataForCollection(this.m_NKMOperator.id, -1);
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_NKMOperator.id);
			if (unitTempletBase != null)
			{
				NKMOperatorSkillTemplet skillTemplet = NKCOperatorUtil.GetSkillTemplet(unitTempletBase.m_lstSkillStrID[0]);
				if (skillTemplet != null)
				{
					NKMTacticalCommandTemplet tacticalCommandTempletByStrID = NKMTacticalCommandManager.GetTacticalCommandTempletByStrID(skillTemplet.m_OperSkillTarget);
					if (tacticalCommandTempletByStrID != null)
					{
						NKCUtil.SetLabelText(this.m_lbSkillCoolTime, string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_SECONDS", false), (int)tacticalCommandTempletByStrID.m_fCoolTime));
					}
				}
			}
		}

		// Token: 0x06009007 RID: 36871 RVA: 0x0030F77D File Offset: 0x0030D97D
		private void OnClickSkillInfo()
		{
			if (this.m_NKMOperator == null)
			{
				return;
			}
			NKCUIOperatorPopUpSkill.Instance.OpenForCollection(this.m_NKMOperator.id);
		}

		// Token: 0x04007CDB RID: 31963
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_collection";

		// Token: 0x04007CDC RID: 31964
		private const string UI_ASSET_NAME = "NKM_UI_COLLECTION_OPERATOR_INFO";

		// Token: 0x04007CDD RID: 31965
		private static NKCUICollectionOperatorInfo m_Instance;

		// Token: 0x04007CDE RID: 31966
		private static NKCUIManager.LoadedUIData m_loadedUIData;

		// Token: 0x04007CDF RID: 31967
		private NKCUIUpsideMenu.eMode m_UpsideMenuMode = NKCUIUpsideMenu.eMode.Normal;

		// Token: 0x04007CE0 RID: 31968
		private NKMOperator m_NKMOperator;

		// Token: 0x04007CE1 RID: 31969
		private NKMUnitTempletBase m_NKMUnitTempletBase;

		// Token: 0x04007CE2 RID: 31970
		public NKCUIOperatorSummary m_NKCUIOperatorSummary;

		// Token: 0x04007CE3 RID: 31971
		[Header("상세정보창")]
		public Text m_lbPower;

		// Token: 0x04007CE4 RID: 31972
		public Text m_STAT_NUMBER_HP;

		// Token: 0x04007CE5 RID: 31973
		public Text m_STAT_NUMBER_ATK;

		// Token: 0x04007CE6 RID: 31974
		public Text m_STAT_NUMBER_DEF;

		// Token: 0x04007CE7 RID: 31975
		public Text m_STAT_NUMBER_SKILL_COOL;

		// Token: 0x04007CE8 RID: 31976
		public ScrollRect m_srUnitIntroduce;

		// Token: 0x04007CE9 RID: 31977
		public Text m_NKM_UI_COLLECTION_UNIT_PROFILE_UNIT_INTRODUCE_TEXT;

		// Token: 0x04007CEA RID: 31978
		[Header("탭")]
		public NKCUIComStateButton m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_PROFILE;

		// Token: 0x04007CEB RID: 31979
		public GameObject m_NKM_UI_COLLECTION_UNIT_PROFILE;

		// Token: 0x04007CEC RID: 31980
		public NKCUIComStateButton m_csbtn_NKM_UI_COLLECTION_UNIT_INFO_STAT;

		// Token: 0x04007CED RID: 31981
		public GameObject m_NKM_UI_COLLECTION_UNIT_STAT;

		// Token: 0x04007CEE RID: 31982
		[Header("유닛 일러스트")]
		public NKCUICharacterView m_CharacterView;

		// Token: 0x04007CEF RID: 31983
		[Header("일러스트 보기 모드에서 움직이는 Rect들. Base/ViewMode 두 이름으로 지정")]
		public Animator m_ani_NKM_UI_COLLECTION_UNIT_INFO_CONTENT;

		// Token: 0x04007CF0 RID: 31984
		[Header("기타 버튼")]
		public NKCUIComStateButton m_NKM_UI_COLLECTION_UNIT_INFO_BOTTOM_BUTTON_APPRAISAL;

		// Token: 0x04007CF1 RID: 31985
		public NKCUIComStateButton m_NKM_UI_COLLECTION_UNIT_INFO_BOTTOM_BUTTON_ILLUST_CHANGE;

		// Token: 0x04007CF2 RID: 31986
		public NKCUIComStateButton m_NKM_UI_COLLECTION_UNIT_PROFILE_VOICE_BUTTON;

		// Token: 0x04007CF3 RID: 31987
		[Space]
		public GameObject m_NKM_UI_COLLECTION_UNIT_INFO_NOTICE_NOTGET;

		// Token: 0x04007CF4 RID: 31988
		public GameObject m_NKM_UI_COLLECTION_UNIT_PROFILE_VOICE_BUTTON_DISABLE;

		// Token: 0x04007CF5 RID: 31989
		[Header("스킬 패널")]
		public NKCUIOperatorSkill m_MainSkill;

		// Token: 0x04007CF6 RID: 31990
		public NKCUIOperatorSkill m_SubSkill;

		// Token: 0x04007CF7 RID: 31991
		public NKCUIOperatorTacticalSkillCombo m_MainSkillCombo;

		// Token: 0x04007CF8 RID: 31992
		public NKCUIComStateButton m_SkillBtn;

		// Token: 0x04007CF9 RID: 31993
		public Text m_lbSkillCoolTime;

		// Token: 0x04007CFA RID: 31994
		private NKCUICollectionOperatorInfo.eCollectionState m_eCurrentState;

		// Token: 0x04007CFB RID: 31995
		[Header("캐릭터 판넬")]
		public NKCUIComDragSelectablePanel m_DragCharacterView;

		// Token: 0x04007CFC RID: 31996
		public EventTrigger m_evtPanel;

		// Token: 0x04007CFD RID: 31997
		[Header("성우")]
		public Text m_lbVoiceActorName;

		// Token: 0x04007CFE RID: 31998
		private bool m_isGauntlet;

		// Token: 0x04007CFF RID: 31999
		private NKCUIOperatorInfo.OpenOption m_OpenOption;

		// Token: 0x04007D00 RID: 32000
		private bool m_bAppraisal;

		// Token: 0x04007D01 RID: 32001
		private bool m_bViewMode;

		// Token: 0x020019E5 RID: 6629
		public enum eCollectionState
		{
			// Token: 0x0400AD2F RID: 44335
			CS_NONE,
			// Token: 0x0400AD30 RID: 44336
			CS_PROFILE,
			// Token: 0x0400AD31 RID: 44337
			CS_STATUS
		}
	}
}
