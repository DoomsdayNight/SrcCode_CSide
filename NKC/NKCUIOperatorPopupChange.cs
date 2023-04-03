using System;
using NKM;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A21 RID: 2593
	public class NKCUIOperatorPopupChange : NKCUIBase
	{
		// Token: 0x170012E5 RID: 4837
		// (get) Token: 0x06007190 RID: 29072 RVA: 0x0025C038 File Offset: 0x0025A238
		public static NKCUIOperatorPopupChange Instance
		{
			get
			{
				if (NKCUIOperatorPopupChange.m_Instance == null)
				{
					NKCUIOperatorPopupChange.m_Instance = NKCUIManager.OpenNewInstance<NKCUIOperatorPopupChange>("ab_ui_nkm_ui_unit_change_popup", "NKM_UI_OPERATOR_CHANGE_POPUP", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIOperatorPopupChange.CleanupInstance)).GetInstance<NKCUIOperatorPopupChange>();
					NKCUIOperatorPopupChange.m_Instance.Init();
				}
				return NKCUIOperatorPopupChange.m_Instance;
			}
		}

		// Token: 0x06007191 RID: 29073 RVA: 0x0025C087 File Offset: 0x0025A287
		private static void CleanupInstance()
		{
			NKCUIOperatorPopupChange.m_Instance = null;
		}

		// Token: 0x170012E6 RID: 4838
		// (get) Token: 0x06007192 RID: 29074 RVA: 0x0025C08F File Offset: 0x0025A28F
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIOperatorPopupChange.m_Instance != null && NKCUIOperatorPopupChange.m_Instance.IsOpen;
			}
		}

		// Token: 0x06007193 RID: 29075 RVA: 0x0025C0AA File Offset: 0x0025A2AA
		public static void CheckInstanceAndClose()
		{
			if (NKCUIOperatorPopupChange.m_Instance != null && NKCUIOperatorPopupChange.m_Instance.IsOpen)
			{
				NKCUIOperatorPopupChange.m_Instance.Close();
			}
		}

		// Token: 0x06007194 RID: 29076 RVA: 0x0025C0CF File Offset: 0x0025A2CF
		private void OnDestroy()
		{
			NKCUIOperatorPopupChange.m_Instance = null;
		}

		// Token: 0x170012E7 RID: 4839
		// (get) Token: 0x06007195 RID: 29077 RVA: 0x0025C0D7 File Offset: 0x0025A2D7
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_OPERATOR_SKILL_POPUP;
			}
		}

		// Token: 0x170012E8 RID: 4840
		// (get) Token: 0x06007196 RID: 29078 RVA: 0x0025C0DE File Offset: 0x0025A2DE
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x06007197 RID: 29079 RVA: 0x0025C0E1 File Offset: 0x0025A2E1
		public override void CloseInternal()
		{
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x06007198 RID: 29080 RVA: 0x0025C0FC File Offset: 0x0025A2FC
		private void Init()
		{
			if (this.m_CloseEvent != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(new UnityAction<BaseEventData>(this.OnEventPanelClick));
				this.m_CloseEvent.triggers.Add(entry);
			}
			if (this.m_Cancel != null)
			{
				this.m_Cancel.PointerClick.RemoveAllListeners();
				this.m_Cancel.PointerClick.AddListener(new UnityAction(base.Close));
			}
			if (this.m_Ok != null)
			{
				this.m_Ok.PointerClick.RemoveAllListeners();
				this.m_Ok.PointerClick.AddListener(new UnityAction(this.OnConfirm));
				NKCUtil.SetHotkey(this.m_Ok, HotkeyEventType.Confirm);
			}
		}

		// Token: 0x06007199 RID: 29081 RVA: 0x0025C1CC File Offset: 0x0025A3CC
		private void OnEventPanelClick(BaseEventData e)
		{
			base.Close();
		}

		// Token: 0x0600719A RID: 29082 RVA: 0x0025C1D4 File Offset: 0x0025A3D4
		public void Open(NKMDeckIndex deckIdx, long BeforeOperatorUID, long AfterOperatorUID, UnityAction callBack = null)
		{
			this.m_curDeckIdx = deckIdx;
			this.m_BeforeOperatorData = NKCOperatorUtil.GetOperatorData(BeforeOperatorUID);
			if (this.m_BeforeOperatorData != null)
			{
				this.m_BeforeOperator.SetData(this.m_BeforeOperatorData, deckIdx, true, null);
			}
			this.m_AfterOperatorData = NKCOperatorUtil.GetOperatorData(AfterOperatorUID);
			if (this.m_AfterOperatorData != null)
			{
				this.m_AfterOperator.SetData(this.m_AfterOperatorData, NKMDeckIndex.None, true, null);
			}
			if (this.m_BeforeOperatorData != null && this.m_AfterOperatorData != null)
			{
				this.m_BeforeMainSkill.SetData(this.m_BeforeOperatorData.mainSkill.id, (int)this.m_BeforeOperatorData.mainSkill.level, false);
				this.m_BeforeSubSkill.SetData(this.m_BeforeOperatorData.subSkill.id, (int)this.m_BeforeOperatorData.subSkill.level, false);
				this.m_AfterMainSkill.SetData(this.m_AfterOperatorData.mainSkill.id, (int)this.m_AfterOperatorData.mainSkill.level, false);
				this.m_AfterSubSkill.SetData(this.m_AfterOperatorData.subSkill.id, (int)this.m_AfterOperatorData.subSkill.level, false);
				float stateValue = NKCOperatorUtil.GetStateValue(this.m_BeforeOperatorData, NKM_STAT_TYPE.NST_HP);
				float stateValue2 = NKCOperatorUtil.GetStateValue(this.m_BeforeOperatorData, NKM_STAT_TYPE.NST_ATK);
				float stateValue3 = NKCOperatorUtil.GetStateValue(this.m_BeforeOperatorData, NKM_STAT_TYPE.NST_DEF);
				float stateValue4 = NKCOperatorUtil.GetStateValue(this.m_BeforeOperatorData, NKM_STAT_TYPE.NST_SKILL_COOL_TIME_REDUCE_RATE);
				float stateValue5 = NKCOperatorUtil.GetStateValue(this.m_AfterOperatorData, NKM_STAT_TYPE.NST_HP);
				float stateValue6 = NKCOperatorUtil.GetStateValue(this.m_AfterOperatorData, NKM_STAT_TYPE.NST_ATK);
				float stateValue7 = NKCOperatorUtil.GetStateValue(this.m_AfterOperatorData, NKM_STAT_TYPE.NST_DEF);
				float stateValue8 = NKCOperatorUtil.GetStateValue(this.m_AfterOperatorData, NKM_STAT_TYPE.NST_SKILL_COOL_TIME_REDUCE_RATE);
				NKCUtil.SetLabelText(this.m_BeforeStatHP, NKCOperatorUtil.GetStatPercentageString(stateValue));
				NKCUtil.SetLabelText(this.m_BeforeStatATK, NKCOperatorUtil.GetStatPercentageString(stateValue2));
				NKCUtil.SetLabelText(this.m_BeforeStatDEF, NKCOperatorUtil.GetStatPercentageString(stateValue3));
				NKCUtil.SetLabelText(this.m_BeforeStatSKILL, NKCOperatorUtil.GetStatPercentageString(stateValue4));
				NKCUtil.SetLabelText(this.m_AfterStatHP, NKCOperatorUtil.GetStatPercentageString(stateValue5));
				NKCUtil.SetLabelText(this.m_AfterStatATK, NKCOperatorUtil.GetStatPercentageString(stateValue6));
				NKCUtil.SetLabelText(this.m_AfterStatDEF, NKCOperatorUtil.GetStatPercentageString(stateValue7));
				NKCUtil.SetLabelText(this.m_AfterStatSKILL, NKCOperatorUtil.GetStatPercentageString(stateValue8));
				this.SetChangeStatString(ref this.m_ChangeStatHP, stateValue, stateValue5);
				this.SetChangeStatString(ref this.m_ChangeStatATK, stateValue2, stateValue6);
				this.SetChangeStatString(ref this.m_ChangeStatDEF, stateValue3, stateValue7);
				this.SetChangeStatString(ref this.m_ChangeStatSKILL, stateValue4, stateValue8);
			}
			this.m_CallBack = callBack;
			base.UIOpened(true);
		}

		// Token: 0x0600719B RID: 29083 RVA: 0x0025C448 File Offset: 0x0025A648
		private void SetChangeStatString(ref Text str, float beforeValue, float AfterValue)
		{
			NKCUtil.SetGameobjectActive(str, beforeValue != AfterValue);
			if (beforeValue > AfterValue)
			{
				NKCUtil.SetLabelText(str, string.Format(NKCUtilString.GET_STRING_OPERATOR_POPUP_CHANGE_STAT_MINUS_DESC_01, NKCOperatorUtil.GetStatPercentageString(beforeValue - AfterValue)));
				NKCUtil.SetLabelTextColor(str, NKCUtil.GetColor("#FF3D40"));
				return;
			}
			NKCUtil.SetLabelText(str, string.Format(NKCUtilString.GET_STRING_OPERATOR_POPUP_CHANGE_STAT_PLUS_DESC_01, NKCOperatorUtil.GetStatPercentageString(AfterValue - beforeValue)));
			NKCUtil.SetLabelTextColor(str, NKCUtil.GetColor("#A3FF66"));
		}

		// Token: 0x0600719C RID: 29084 RVA: 0x0025C4BC File Offset: 0x0025A6BC
		private void OnConfirm()
		{
			UnityAction callBack = this.m_CallBack;
			if (callBack != null)
			{
				callBack();
			}
			base.Close();
		}

		// Token: 0x04005D72 RID: 23922
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_unit_change_popup";

		// Token: 0x04005D73 RID: 23923
		private const string UI_ASSET_NAME = "NKM_UI_OPERATOR_CHANGE_POPUP";

		// Token: 0x04005D74 RID: 23924
		private static NKCUIOperatorPopupChange m_Instance;

		// Token: 0x04005D75 RID: 23925
		public EventTrigger m_CloseEvent;

		// Token: 0x04005D76 RID: 23926
		public NKCUIComButton m_Ok;

		// Token: 0x04005D77 RID: 23927
		public NKCUIComButton m_Cancel;

		// Token: 0x04005D78 RID: 23928
		public NKCUIOperatorSelectListSlot m_BeforeOperator;

		// Token: 0x04005D79 RID: 23929
		public NKCUIOperatorSelectListSlot m_AfterOperator;

		// Token: 0x04005D7A RID: 23930
		public NKCUIOperatorSkill m_BeforeMainSkill;

		// Token: 0x04005D7B RID: 23931
		public NKCUIOperatorSkill m_BeforeSubSkill;

		// Token: 0x04005D7C RID: 23932
		public Text m_BeforeStatHP;

		// Token: 0x04005D7D RID: 23933
		public Text m_BeforeStatATK;

		// Token: 0x04005D7E RID: 23934
		public Text m_BeforeStatDEF;

		// Token: 0x04005D7F RID: 23935
		public Text m_BeforeStatSKILL;

		// Token: 0x04005D80 RID: 23936
		public NKCUIOperatorSkill m_AfterMainSkill;

		// Token: 0x04005D81 RID: 23937
		public NKCUIOperatorSkill m_AfterSubSkill;

		// Token: 0x04005D82 RID: 23938
		public Text m_AfterStatHP;

		// Token: 0x04005D83 RID: 23939
		public Text m_AfterStatATK;

		// Token: 0x04005D84 RID: 23940
		public Text m_AfterStatDEF;

		// Token: 0x04005D85 RID: 23941
		public Text m_AfterStatSKILL;

		// Token: 0x04005D86 RID: 23942
		public Text m_ChangeStatHP;

		// Token: 0x04005D87 RID: 23943
		public Text m_ChangeStatATK;

		// Token: 0x04005D88 RID: 23944
		public Text m_ChangeStatDEF;

		// Token: 0x04005D89 RID: 23945
		public Text m_ChangeStatSKILL;

		// Token: 0x04005D8A RID: 23946
		private NKMDeckIndex m_curDeckIdx;

		// Token: 0x04005D8B RID: 23947
		private NKMOperator m_BeforeOperatorData;

		// Token: 0x04005D8C RID: 23948
		private NKMOperator m_AfterOperatorData;

		// Token: 0x04005D8D RID: 23949
		private UnityAction m_CallBack;
	}
}
