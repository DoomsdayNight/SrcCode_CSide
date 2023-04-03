using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Math;
using NKC.Templet;
using NKC.UI.Component;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A1C RID: 2588
	public class NKCUIOperatorInfo : NKCUIBase
	{
		// Token: 0x170012D5 RID: 4821
		// (get) Token: 0x060070FB RID: 28923 RVA: 0x00257DF0 File Offset: 0x00255FF0
		public static NKCUIOperatorInfo Instance
		{
			get
			{
				if (NKCUIOperatorInfo.m_Instance == null)
				{
					NKCUIOperatorInfo.m_Instance = NKCUIManager.OpenNewInstance<NKCUIOperatorInfo>("ab_ui_nkm_ui_operator_info", "NKM_UI_OPERATOR_INFO", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIOperatorInfo.CleanupInstance)).GetInstance<NKCUIOperatorInfo>();
					NKCUIOperatorInfo.m_Instance.Init();
				}
				return NKCUIOperatorInfo.m_Instance;
			}
		}

		// Token: 0x170012D6 RID: 4822
		// (get) Token: 0x060070FC RID: 28924 RVA: 0x00257E3F File Offset: 0x0025603F
		public static bool HasInstance
		{
			get
			{
				return NKCUIOperatorInfo.m_Instance != null;
			}
		}

		// Token: 0x170012D7 RID: 4823
		// (get) Token: 0x060070FD RID: 28925 RVA: 0x00257E4C File Offset: 0x0025604C
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIOperatorInfo.m_Instance != null && NKCUIOperatorInfo.m_Instance.IsOpen;
			}
		}

		// Token: 0x060070FE RID: 28926 RVA: 0x00257E67 File Offset: 0x00256067
		public static void CheckInstanceAndClose()
		{
			if (NKCUIOperatorInfo.m_Instance != null && NKCUIOperatorInfo.m_Instance.IsOpen)
			{
				NKCUIOperatorInfo.m_Instance.Close();
			}
		}

		// Token: 0x060070FF RID: 28927 RVA: 0x00257E8C File Offset: 0x0025608C
		public override void CloseInternal()
		{
			this.BannerCleanUp();
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
			this.m_State = NKCUIOperatorInfo.TAB_STATE.NONE;
		}

		// Token: 0x06007100 RID: 28928 RVA: 0x00257EB4 File Offset: 0x002560B4
		private static void CleanupInstance()
		{
			NKCUIOperatorInfo.m_Instance = null;
		}

		// Token: 0x170012D8 RID: 4824
		// (get) Token: 0x06007101 RID: 28929 RVA: 0x00257EBC File Offset: 0x002560BC
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_OPERATOR_INFO_MENU_NAME;
			}
		}

		// Token: 0x170012D9 RID: 4825
		// (get) Token: 0x06007102 RID: 28930 RVA: 0x00257EC3 File Offset: 0x002560C3
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170012DA RID: 4826
		// (get) Token: 0x06007103 RID: 28931 RVA: 0x00257EC6 File Offset: 0x002560C6
		public override string GuideTempletID
		{
			get
			{
				return "ARTICLE_OPERATOR_INFO";
			}
		}

		// Token: 0x06007104 RID: 28932 RVA: 0x00257ED0 File Offset: 0x002560D0
		private void Init()
		{
			if (this.m_NKM_UI_OPERATOR_INFO_POPUP_LEVELUP != null)
			{
				this.m_NKM_UI_OPERATOR_INFO_POPUP_LEVELUP.Init(new NKCUIOperatorInfoPopupLevelUp.OnStart(this.OnStart));
			}
			NKCUtil.SetBindFunction(this.m_BUTTON_SKILLUP, new UnityAction(this.OnClickSkillUp));
			NKCUtil.SetBindFunction(this.m_BUTTON_LEVELUP, new UnityAction(this.OnClickLevelUp));
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
			if (this.m_NKM_UI_OPERATOR_INFO_UNIT_LOCK != null)
			{
				this.m_NKM_UI_OPERATOR_INFO_UNIT_LOCK.OnValueChanged.RemoveAllListeners();
				this.m_NKM_UI_OPERATOR_INFO_UNIT_LOCK.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickLock));
			}
			if (null != this.m_NKM_UI_UNIT_INFO_CONTROL_VOICE_BG)
			{
				this.m_NKM_UI_UNIT_INFO_CONTROL_VOICE_BG.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_UNIT_INFO_CONTROL_VOICE_BG.PointerClick.AddListener(new UnityAction(this.OnClickSpeech));
			}
			if (null != this.m_NKM_UI_UNIT_INFO_CONTROL_APPRAISAL_BG)
			{
				this.m_NKM_UI_UNIT_INFO_CONTROL_APPRAISAL_BG.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_UNIT_INFO_CONTROL_APPRAISAL_BG.PointerClick.AddListener(new UnityAction(this.OnClickAppraisal));
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_INFO_CONTROL_APPRAISAL_BG, NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.UNIT_REVIEW_SYSTEM));
			if (null != this.NKM_UI_OPERATOR_INFO_ILLUST_CHANGE)
			{
				this.NKM_UI_OPERATOR_INFO_ILLUST_CHANGE.PointerClick.RemoveAllListeners();
				this.NKM_UI_OPERATOR_INFO_ILLUST_CHANGE.PointerClick.AddListener(new UnityAction(this.OnClickChangeIllust));
			}
			NKCUtil.SetBindFunction(this.m_MainSkillBtn, new UnityAction(this.OnClickSkillInfo));
			NKCUtil.SetBindFunction(this.m_SubSkillBtn, new UnityAction(this.OnClickSkillInfo));
		}

		// Token: 0x06007105 RID: 28933 RVA: 0x0025812C File Offset: 0x0025632C
		public void OnStart(List<MiscItemData> lstMaterials)
		{
			if (this.m_OperatorData == null)
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_OPERATOR_LEVELUP_REQ(this.m_OperatorData.uid, lstMaterials);
		}

		// Token: 0x06007106 RID: 28934 RVA: 0x00258148 File Offset: 0x00256348
		public override void OnBackButton()
		{
			if (this.m_State == NKCUIOperatorInfo.TAB_STATE.LEVEL_UP)
			{
				this.UpdateState(NKCUIOperatorInfo.TAB_STATE.INFO);
				return;
			}
			if (this.m_bViewMode)
			{
				this.OnClickChangeIllust();
				return;
			}
			base.OnBackButton();
		}

		// Token: 0x06007107 RID: 28935 RVA: 0x00258170 File Offset: 0x00256370
		public override void UnHide()
		{
			base.UnHide();
			if (this.m_State == NKCUIOperatorInfo.TAB_STATE.LEVEL_UP)
			{
				this.UpdateMenuAni(NKCUIOperatorInfo.TAB_STATE.LEVEL_UP, true);
				return;
			}
			if (this.m_State == NKCUIOperatorInfo.TAB_STATE.INFO)
			{
				this.m_bAppraisal = false;
				this.UpdateMenuAni(NKCUIOperatorInfo.TAB_STATE.INFO, true);
			}
		}

		// Token: 0x06007108 RID: 28936 RVA: 0x002581A4 File Offset: 0x002563A4
		public void Open(NKMOperator data, NKCUIOperatorInfo.OpenOption option = null)
		{
			this.m_OperatorData = data;
			bool bValue = false;
			if (option == null)
			{
				if (this.m_OpenOption == null)
				{
					this.m_OpenOption = new NKCUIOperatorInfo.OpenOption(new List<long>
					{
						this.m_OperatorData.uid
					}, 0);
				}
				this.m_OpenOption.m_lstOperatorData.Add(data);
				if (this.m_OpenOption != null)
				{
					this.m_OpenOption.m_lstOperatorData.Add(data);
				}
			}
			else
			{
				this.m_OpenOption = option;
				bValue = (this.m_OpenOption.m_lstOperatorData.Count > 1);
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATOR_INFO_ARROW, bValue);
			this.InitBanner();
			this.bHasVoice = false;
			this.UpdateState(NKCUIOperatorInfo.TAB_STATE.INFO);
			this.UpdateUnitInfo();
			this.CheckTutorial();
			base.UIOpened(true);
		}

		// Token: 0x06007109 RID: 28937 RVA: 0x00258264 File Offset: 0x00256464
		private void InitBanner()
		{
			if (this.m_OperatorData == null)
			{
				return;
			}
			if (this.m_DragCharacterView != null && this.m_OpenOption != null)
			{
				if (this.m_OpenOption.m_lstOperatorData.Count <= 0)
				{
					if (this.m_OpenOption.m_lstOperatorUID.Count <= 0)
					{
						this.m_OpenOption.m_lstOperatorUID.Add(this.m_OperatorData.uid);
					}
					NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
					if (nkmuserData != null)
					{
						NKMArmyData armyData = nkmuserData.m_ArmyData;
						if (armyData != null)
						{
							for (int i = 0; i < this.m_OpenOption.m_lstOperatorUID.Count; i++)
							{
								NKMOperator operatorFromUId = armyData.GetOperatorFromUId(this.m_OpenOption.m_lstOperatorUID[i]);
								if (operatorFromUId != null)
								{
									this.m_OpenOption.m_lstOperatorData.Add(operatorFromUId);
								}
							}
						}
					}
				}
				for (int j = 0; j < this.m_OpenOption.m_lstOperatorData.Count; j++)
				{
					if (this.m_OpenOption.m_lstOperatorData[j].uid == this.m_OperatorData.uid)
					{
						this.m_DragCharacterView.TotalCount = this.m_OpenOption.m_lstOperatorData.Count;
						this.m_DragCharacterView.SetIndex(j);
						return;
					}
				}
			}
		}

		// Token: 0x0600710A RID: 28938 RVA: 0x002583A0 File Offset: 0x002565A0
		private void UpdateUnitInfo()
		{
			if (this.m_OperatorData == null)
			{
				return;
			}
			this.m_NKM_UI_OPERATOR_INFO_SUMMARY.SetData(this.m_OperatorData);
			NKCUIOperatorInfoPopupLevelUp nkm_UI_OPERATOR_INFO_POPUP_LEVELUP = this.m_NKM_UI_OPERATOR_INFO_POPUP_LEVELUP;
			if (nkm_UI_OPERATOR_INFO_POPUP_LEVELUP != null)
			{
				nkm_UI_OPERATOR_INFO_POPUP_LEVELUP.SetData(this.m_OperatorData, false);
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_OperatorData.id);
			this.bHasVoice = NKCUIVoiceManager.UnitHasVoice(unitTempletBase);
			this.m_cgVoice.alpha = (this.bHasVoice ? 1f : 0.4f);
			this.UpdateUnitStat();
			this.UpdateSkillInfo();
			this.UpdateLockState();
			if (unitTempletBase != null)
			{
				NKCUtil.SetLabelText(this.m_lbVoiceActor, NKCVoiceActorNameTemplet.FindActorName(unitTempletBase));
			}
		}

		// Token: 0x0600710B RID: 28939 RVA: 0x00258441 File Offset: 0x00256641
		private void UpdateUnitLevelUp(bool bShowLevelUpFX)
		{
			if (this.m_OperatorData == null)
			{
				return;
			}
			this.m_NKM_UI_OPERATOR_INFO_SUMMARY.SetData(this.m_OperatorData);
			NKCUIOperatorInfoPopupLevelUp nkm_UI_OPERATOR_INFO_POPUP_LEVELUP = this.m_NKM_UI_OPERATOR_INFO_POPUP_LEVELUP;
			if (nkm_UI_OPERATOR_INFO_POPUP_LEVELUP != null)
			{
				nkm_UI_OPERATOR_INFO_POPUP_LEVELUP.SetData(this.m_OperatorData, bShowLevelUpFX);
			}
			this.UpdateUnitStat();
		}

		// Token: 0x0600710C RID: 28940 RVA: 0x0025847C File Offset: 0x0025667C
		private void UpdateUnitStat()
		{
			NKCUtil.SetLabelText(this.m_NKM_UI_OPERATOR_INFO_SUMMARY_ATTACK_TEXT, this.m_OperatorData.CalculateOperatorOperationPower().ToString());
			NKCUtil.SetLabelText(this.m_STAT_NAME_ATK, NKCOperatorUtil.GetStatPercentageString(this.m_OperatorData, NKM_STAT_TYPE.NST_ATK));
			NKCUtil.SetLabelText(this.m_STAT_NAME_DEF, NKCOperatorUtil.GetStatPercentageString(this.m_OperatorData, NKM_STAT_TYPE.NST_DEF));
			NKCUtil.SetLabelText(this.m_STAT_NAME_HP, NKCOperatorUtil.GetStatPercentageString(this.m_OperatorData, NKM_STAT_TYPE.NST_HP));
			NKCUtil.SetLabelText(this.m_STAT_NAME_SKILL_COOL, NKCOperatorUtil.GetStatPercentageString(this.m_OperatorData, NKM_STAT_TYPE.NST_SKILL_COOL_TIME_REDUCE_RATE));
		}

		// Token: 0x0600710D RID: 28941 RVA: 0x00258504 File Offset: 0x00256704
		private void UpdateSkillInfo()
		{
			if (this.m_OperatorData != null)
			{
				this.m_MainSkill.SetData(this.m_OperatorData.mainSkill.id, (int)this.m_OperatorData.mainSkill.level, false);
				this.m_MainSkillCombo.SetData(this.m_OperatorData.id);
				this.m_SubSkill.SetData(this.m_OperatorData.subSkill.id, (int)this.m_OperatorData.subSkill.level, false);
				NKMTacticalCommandTemplet tacticalCommandTempletByID = NKMTacticalCommandManager.GetTacticalCommandTempletByID(this.m_OperatorData.mainSkill.id);
				if (tacticalCommandTempletByID != null)
				{
					NKCUtil.SetLabelText(this.m_lbSkillCool, string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_SECONDS", false), (int)tacticalCommandTempletByID.m_fCoolTime));
				}
			}
		}

		// Token: 0x0600710E RID: 28942 RVA: 0x002585CC File Offset: 0x002567CC
		private void UpdateState(NKCUIOperatorInfo.TAB_STATE newState)
		{
			if (this.m_State == newState)
			{
				return;
			}
			if (this.m_State == NKCUIOperatorInfo.TAB_STATE.LEVEL_UP)
			{
				NKCUIOperatorInfoPopupLevelUp nkm_UI_OPERATOR_INFO_POPUP_LEVELUP = this.m_NKM_UI_OPERATOR_INFO_POPUP_LEVELUP;
				if (nkm_UI_OPERATOR_INFO_POPUP_LEVELUP != null)
				{
					nkm_UI_OPERATOR_INFO_POPUP_LEVELUP.Refresh();
				}
			}
			this.UpdateMenuAni(newState, false);
			if (this.m_State == NKCUIOperatorInfo.TAB_STATE.LEVEL_UP)
			{
				NKCUIOperatorInfoPopupLevelUp nkm_UI_OPERATOR_INFO_POPUP_LEVELUP2 = this.m_NKM_UI_OPERATOR_INFO_POPUP_LEVELUP;
				if (nkm_UI_OPERATOR_INFO_POPUP_LEVELUP2 != null)
				{
					nkm_UI_OPERATOR_INFO_POPUP_LEVELUP2.ResetResourceIcon();
				}
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATOR_INFO_DESC_STAT, this.m_State == NKCUIOperatorInfo.TAB_STATE.INFO);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATOR_INFO_DESC_SKILL_01, this.m_State == NKCUIOperatorInfo.TAB_STATE.INFO);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATOR_INFO_DESC_SKILL_02, this.m_State == NKCUIOperatorInfo.TAB_STATE.INFO);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATOR_INFO_DESC_BUTTON, this.m_State == NKCUIOperatorInfo.TAB_STATE.INFO);
			NKCUtil.SetGameobjectActive(this.m_LEVELUP, this.m_State == NKCUIOperatorInfo.TAB_STATE.LEVEL_UP);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATOR_INFO_CONTROL, this.m_State == NKCUIOperatorInfo.TAB_STATE.INFO);
		}

		// Token: 0x0600710F RID: 28943 RVA: 0x00258698 File Offset: 0x00256898
		private void UpdateMenuAni(NKCUIOperatorInfo.TAB_STATE newState, bool bForce = false)
		{
			if (newState != NKCUIOperatorInfo.TAB_STATE.INFO)
			{
				if (newState == NKCUIOperatorInfo.TAB_STATE.LEVEL_UP)
				{
					if (bForce)
					{
						this.m_NKM_UI_OPERATOR_INFO_ANI.SetTrigger(this.Ani_LvUp);
					}
					else
					{
						this.m_NKM_UI_OPERATOR_INFO_ANI.SetTrigger(this.Ani_LvUpIn);
					}
				}
			}
			else if (bForce)
			{
				this.m_NKM_UI_OPERATOR_INFO_ANI.SetTrigger(this.Ani_Base);
			}
			else if (this.m_State == NKCUIOperatorInfo.TAB_STATE.NONE)
			{
				this.m_NKM_UI_OPERATOR_INFO_ANI.SetTrigger(this.Ani_Base);
			}
			else
			{
				this.m_NKM_UI_OPERATOR_INFO_ANI.SetTrigger(this.Ani_LvUpOut);
			}
			this.m_State = newState;
		}

		// Token: 0x06007110 RID: 28944 RVA: 0x00258721 File Offset: 0x00256921
		private void OnClickSkillUp()
		{
			NKCUIOperatorInfoPopupSkill.Instance.Open(this.m_OperatorData);
		}

		// Token: 0x06007111 RID: 28945 RVA: 0x00258733 File Offset: 0x00256933
		private void OnClickLevelUp()
		{
			this.UpdateState(NKCUIOperatorInfo.TAB_STATE.LEVEL_UP);
		}

		// Token: 0x06007112 RID: 28946 RVA: 0x0025873C File Offset: 0x0025693C
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

		// Token: 0x06007113 RID: 28947 RVA: 0x002587C8 File Offset: 0x002569C8
		private void ProvideMainBannerListSlotData(Transform tr, int idx)
		{
			if (this.m_OpenOption != null && this.m_OpenOption.m_lstOperatorData != null && this.m_OpenOption.m_lstOperatorData.Count > idx)
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

		// Token: 0x06007114 RID: 28948 RVA: 0x00258855 File Offset: 0x00256A55
		private void ReturnMainBannerListSlot(Transform go)
		{
			NKCUtil.SetGameobjectActive(go, false);
			UnityEngine.Object.Destroy(go.gameObject);
		}

		// Token: 0x06007115 RID: 28949 RVA: 0x0025886C File Offset: 0x00256A6C
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

		// Token: 0x06007116 RID: 28950 RVA: 0x002588AE File Offset: 0x00256AAE
		private void Focus(RectTransform rect, bool bFocus)
		{
			NKCUtil.SetGameobjectActive(rect.gameObject, bFocus);
		}

		// Token: 0x06007117 RID: 28951 RVA: 0x002588BC File Offset: 0x00256ABC
		private void FocusColor(RectTransform rect, Color ApplyColor)
		{
			NKCUICharacterView componentInChildren = rect.gameObject.GetComponentInChildren<NKCUICharacterView>();
			if (componentInChildren != null)
			{
				componentInChildren.SetColor(ApplyColor, false);
			}
		}

		// Token: 0x06007118 RID: 28952 RVA: 0x002588E8 File Offset: 0x00256AE8
		public void SelectCharacter(int idx)
		{
			if (this.m_OpenOption.m_lstOperatorData.Count < idx || idx < 0)
			{
				Debug.LogWarning(string.Format("Error - Count : {0}, Index : {1}", this.m_OpenOption.m_lstOperatorData.Count, idx));
				return;
			}
			NKMOperator nkmoperator = this.m_OpenOption.m_lstOperatorData[idx];
			if (nkmoperator != null)
			{
				this.ChangeOperator(nkmoperator);
			}
		}

		// Token: 0x06007119 RID: 28953 RVA: 0x00258954 File Offset: 0x00256B54
		private void BannerCleanUp()
		{
			if (this.m_DragCharacterView != null)
			{
				NKCUICharacterView[] componentsInChildren = this.m_DragCharacterView.gameObject.GetComponentsInChildren<NKCUICharacterView>();
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

		// Token: 0x0600711A RID: 28954 RVA: 0x0025899C File Offset: 0x00256B9C
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

		// Token: 0x0600711B RID: 28955 RVA: 0x00258A0C File Offset: 0x00256C0C
		private void ChangeOperator(NKMOperator operatorData)
		{
			this.m_OperatorData = operatorData;
			this.UpdateUnitInfo();
		}

		// Token: 0x0600711C RID: 28956 RVA: 0x00258A1C File Offset: 0x00256C1C
		public override void OnOperatorUpdate(NKMUserData.eChangeNotifyType eEventType, long uid, NKMOperator operatorData)
		{
			if (eEventType == NKMUserData.eChangeNotifyType.Update)
			{
				if (this.m_OperatorData != null && this.m_OperatorData.uid == uid)
				{
					this.m_OperatorData = operatorData;
				}
				if (this.m_State == NKCUIOperatorInfo.TAB_STATE.LEVEL_UP)
				{
					if (this.m_OpenOption != null && this.m_OpenOption.m_lstOperatorData != null)
					{
						List<NKMOperator> lstOperatorData = this.m_OpenOption.m_lstOperatorData;
						for (int i = 0; i < lstOperatorData.Count; i++)
						{
							if (uid == lstOperatorData[i].uid)
							{
								this.m_OpenOption = new NKCUIOperatorInfo.OpenOption(lstOperatorData, this.m_OpenOption.m_SelectSlotIndex);
								break;
							}
						}
					}
				}
				else
				{
					this.UpdateSkillInfo();
				}
				this.UpdateUnitLevelUp(this.m_State == NKCUIOperatorInfo.TAB_STATE.LEVEL_UP);
				return;
			}
			if (eEventType == NKMUserData.eChangeNotifyType.Remove)
			{
				int index = 0;
				for (int j = 0; j < this.m_OpenOption.m_lstOperatorData.Count; j++)
				{
					if (this.m_OpenOption.m_lstOperatorData[j].uid == uid)
					{
						this.m_OpenOption.m_lstOperatorData.RemoveAt(j);
						j--;
					}
					else if (this.m_OpenOption.m_lstOperatorData[j].uid == this.m_OperatorData.uid)
					{
						index = j;
					}
				}
				this.m_DragCharacterView.TotalCount = this.m_OpenOption.m_lstOperatorData.Count;
				this.m_DragCharacterView.SetIndex(index);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATOR_INFO_ARROW, this.m_OpenOption.m_lstOperatorData.Count > 1);
			}
		}

		// Token: 0x0600711D RID: 28957 RVA: 0x00258B88 File Offset: 0x00256D88
		public void UpdateLockState(long operatorUID)
		{
			if (this.m_OperatorData == null)
			{
				return;
			}
			if (this.m_OpenOption != null && this.m_OpenOption.m_lstOperatorData != null)
			{
				foreach (NKMOperator nkmoperator in this.m_OpenOption.m_lstOperatorData)
				{
					if (nkmoperator.uid == operatorUID)
					{
						nkmoperator.bLock = this.m_OperatorData.bLock;
						break;
					}
				}
			}
			if (this.m_OperatorData.uid != operatorUID)
			{
				return;
			}
			this.UpdateLockState();
		}

		// Token: 0x0600711E RID: 28958 RVA: 0x00258C28 File Offset: 0x00256E28
		private void UpdateLockState()
		{
			if (this.m_OperatorData == null)
			{
				return;
			}
			bool bSelect = NKCOperatorUtil.IsLock(this.m_OperatorData.uid);
			this.m_NKM_UI_OPERATOR_INFO_UNIT_LOCK.Select(bSelect, true, false);
		}

		// Token: 0x0600711F RID: 28959 RVA: 0x00258C5E File Offset: 0x00256E5E
		private void OnClickLock(bool bSet)
		{
			NKCPacketSender.Send_NKMPacket_OPERATOR_LOCK_REQ(this.m_OperatorData.uid, bSet);
		}

		// Token: 0x06007120 RID: 28960 RVA: 0x00258C71 File Offset: 0x00256E71
		private void OnClickSkillInfo()
		{
			NKCUIOperatorPopUpSkill.Instance.Open(this.m_OperatorData.uid);
		}

		// Token: 0x06007121 RID: 28961 RVA: 0x00258C88 File Offset: 0x00256E88
		private void OnClickSpeech()
		{
			if (!this.bHasVoice)
			{
				return;
			}
			NKCUIPopupVoice.Instance.Open(this.m_OperatorData.id, 0, false);
		}

		// Token: 0x06007122 RID: 28962 RVA: 0x00258CAA File Offset: 0x00256EAA
		private void OnClickAppraisal()
		{
			if (this.m_bViewMode)
			{
				return;
			}
			NKCUIUnitReview.Instance.OpenUI(this.m_OperatorData.id);
			this.m_bAppraisal = true;
		}

		// Token: 0x06007123 RID: 28963 RVA: 0x00258CD1 File Offset: 0x00256ED1
		private void OnClickChangeIllust()
		{
			if (this.m_bAppraisal)
			{
				return;
			}
			NKCUIPopupIllustView.Instance.Open(this.m_OperatorData);
		}

		// Token: 0x06007124 RID: 28964 RVA: 0x00258CEC File Offset: 0x00256EEC
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			this.m_NKM_UI_OPERATOR_INFO_POPUP_LEVELUP.OnInventoryChange(itemData);
		}

		// Token: 0x06007125 RID: 28965 RVA: 0x00258CFA File Offset: 0x00256EFA
		public void CheckTutorial()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.OperatorEnhance, true);
		}

		// Token: 0x04005CBF RID: 23743
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_operator_info";

		// Token: 0x04005CC0 RID: 23744
		private const string UI_ASSET_NAME = "NKM_UI_OPERATOR_INFO";

		// Token: 0x04005CC1 RID: 23745
		private static NKCUIOperatorInfo m_Instance;

		// Token: 0x04005CC2 RID: 23746
		public RectTransform m_NKM_UI_UNIT_ILLUST_ROOT;

		// Token: 0x04005CC3 RID: 23747
		[Header("유닛 일러스트")]
		public NKCUIComDragSelectablePanel m_DragCharacterView;

		// Token: 0x04005CC4 RID: 23748
		public EventTrigger m_evtPanel;

		// Token: 0x04005CC5 RID: 23749
		public GameObject m_NKM_UI_OPERATOR_INFO_ARROW;

		// Token: 0x04005CC6 RID: 23750
		public NKCUIComStateButton m_Right;

		// Token: 0x04005CC7 RID: 23751
		public NKCUIComStateButton m_Left;

		// Token: 0x04005CC8 RID: 23752
		public GameObject m_NKM_UI_OPERATOR_INFO_CONTROL;

		// Token: 0x04005CC9 RID: 23753
		public CanvasGroup m_cgVoice;

		// Token: 0x04005CCA RID: 23754
		public NKCUIComButton m_NKM_UI_UNIT_INFO_CONTROL_VOICE_BG;

		// Token: 0x04005CCB RID: 23755
		public NKCUIComButton m_NKM_UI_UNIT_INFO_CONTROL_PRACTICE_BG;

		// Token: 0x04005CCC RID: 23756
		public NKCUIComButton m_NKM_UI_UNIT_INFO_CONTROL_APPRAISAL_BG;

		// Token: 0x04005CCD RID: 23757
		public NKCUIComButton m_NKM_UI_UNIT_INFO_CONTROL_SKIN_VIEW_BG;

		// Token: 0x04005CCE RID: 23758
		[Header("우측 메뉴")]
		public GameObject m_NKM_UI_OPERATOR_INFO_DESC_STAT;

		// Token: 0x04005CCF RID: 23759
		public GameObject m_NKM_UI_OPERATOR_INFO_DESC_SKILL_01;

		// Token: 0x04005CD0 RID: 23760
		public GameObject m_NKM_UI_OPERATOR_INFO_DESC_SKILL_02;

		// Token: 0x04005CD1 RID: 23761
		public GameObject m_NKM_UI_OPERATOR_INFO_DESC_BUTTON;

		// Token: 0x04005CD2 RID: 23762
		public GameObject m_LEVELUP;

		// Token: 0x04005CD3 RID: 23763
		public NKCUIOperatorSummary m_NKM_UI_OPERATOR_INFO_SUMMARY;

		// Token: 0x04005CD4 RID: 23764
		public NKCUIOperatorInfoPopupLevelUp m_NKM_UI_OPERATOR_INFO_POPUP_LEVELUP;

		// Token: 0x04005CD5 RID: 23765
		public GameObject m_NKM_UI_OPERATOR_INFO_CONTROL_BOTTOM;

		// Token: 0x04005CD6 RID: 23766
		public NKCUIComButton NKM_UI_OPERATOR_INFO_ILLUST_CHANGE;

		// Token: 0x04005CD7 RID: 23767
		public NKCUIComToggle m_NKM_UI_OPERATOR_INFO_UNIT_LOCK;

		// Token: 0x04005CD8 RID: 23768
		[Header("능력치")]
		public Text m_NKM_UI_OPERATOR_INFO_SUMMARY_ATTACK_TEXT;

		// Token: 0x04005CD9 RID: 23769
		public Text m_STAT_NAME_HP;

		// Token: 0x04005CDA RID: 23770
		public Text m_STAT_NAME_ATK;

		// Token: 0x04005CDB RID: 23771
		public Text m_STAT_NAME_DEF;

		// Token: 0x04005CDC RID: 23772
		public Text m_STAT_NAME_SKILL_COOL;

		// Token: 0x04005CDD RID: 23773
		[Header("스킬 패널")]
		public NKCUIOperatorSkill m_MainSkill;

		// Token: 0x04005CDE RID: 23774
		public NKCUIOperatorSkill m_SubSkill;

		// Token: 0x04005CDF RID: 23775
		public NKCUIOperatorTacticalSkillCombo m_MainSkillCombo;

		// Token: 0x04005CE0 RID: 23776
		public NKCUIComStateButton m_MainSkillBtn;

		// Token: 0x04005CE1 RID: 23777
		public NKCUIComStateButton m_SubSkillBtn;

		// Token: 0x04005CE2 RID: 23778
		public Text m_lbSkillCool;

		// Token: 0x04005CE3 RID: 23779
		[Header("버튼")]
		public NKCUIComStateButton m_BUTTON_SKILLUP;

		// Token: 0x04005CE4 RID: 23780
		public NKCUIComStateButton m_BUTTON_LEVELUP;

		// Token: 0x04005CE5 RID: 23781
		public NKCUIComStateButton m_BUTTON_SKILLUP_LOCK;

		// Token: 0x04005CE6 RID: 23782
		public NKCUIComStateButton m_BUTTON_LEVELUP_LOCK;

		// Token: 0x04005CE7 RID: 23783
		[Header("애니메이션")]
		public Animator m_NKM_UI_OPERATOR_INFO_ANI;

		// Token: 0x04005CE8 RID: 23784
		[Header("성우")]
		public Text m_lbVoiceActor;

		// Token: 0x04005CE9 RID: 23785
		private NKCUIOperatorInfo.TAB_STATE m_State;

		// Token: 0x04005CEA RID: 23786
		private NKMOperator m_OperatorData;

		// Token: 0x04005CEB RID: 23787
		private string Ani_LvUp = "LvUp";

		// Token: 0x04005CEC RID: 23788
		private string Ani_LvUpIn = "LvUpIn";

		// Token: 0x04005CED RID: 23789
		private string Ani_LvUpOut = "LvUpOut";

		// Token: 0x04005CEE RID: 23790
		private string Ani_Base = "Base";

		// Token: 0x04005CEF RID: 23791
		private NKCUIOperatorInfo.OpenOption m_OpenOption;

		// Token: 0x04005CF0 RID: 23792
		private bool bHasVoice;

		// Token: 0x04005CF1 RID: 23793
		private bool m_bAppraisal;

		// Token: 0x04005CF2 RID: 23794
		private bool m_bViewMode;

		// Token: 0x0200175A RID: 5978
		private enum TAB_STATE
		{
			// Token: 0x0400A690 RID: 42640
			NONE,
			// Token: 0x0400A691 RID: 42641
			INFO,
			// Token: 0x0400A692 RID: 42642
			LEVEL_UP
		}

		// Token: 0x0200175B RID: 5979
		public class OpenOption
		{
			// Token: 0x0600B2FF RID: 45823 RVA: 0x00362A91 File Offset: 0x00360C91
			public OpenOption(List<long> UnitUIDList, int SlotIdx = 0)
			{
				if (UnitUIDList == null || UnitUIDList.Count <= 1)
				{
					return;
				}
				this.m_lstOperatorUID = UnitUIDList;
				this.m_SelectSlotIndex = SlotIdx;
			}

			// Token: 0x0600B300 RID: 45824 RVA: 0x00362ACA File Offset: 0x00360CCA
			public OpenOption(List<NKMOperator> lstOperatorData, int SlotIdx = 0)
			{
				if (lstOperatorData == null || lstOperatorData.Count <= 1)
				{
					return;
				}
				this.m_lstOperatorData = lstOperatorData;
				this.m_SelectSlotIndex = SlotIdx;
			}

			// Token: 0x0400A693 RID: 42643
			public readonly List<long> m_lstOperatorUID = new List<long>();

			// Token: 0x0400A694 RID: 42644
			public readonly List<NKMOperator> m_lstOperatorData = new List<NKMOperator>();

			// Token: 0x0400A695 RID: 42645
			public int m_SelectSlotIndex;

			// Token: 0x0400A696 RID: 42646
			public bool m_bShowFierceInfo;
		}
	}
}
