using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A22 RID: 2594
	public class NKCUIOperatorPopupConfirm : NKCUIBase
	{
		// Token: 0x170012E9 RID: 4841
		// (get) Token: 0x0600719E RID: 29086 RVA: 0x0025C4E0 File Offset: 0x0025A6E0
		public static NKCUIOperatorPopupConfirm Instance
		{
			get
			{
				if (NKCUIOperatorPopupConfirm.m_Instance == null)
				{
					NKCUIOperatorPopupConfirm.m_Instance = NKCUIManager.OpenNewInstance<NKCUIOperatorPopupConfirm>("ab_ui_nkm_ui_operator_info", "NKM_UI_POPUP_OPERATOR_CONFIRM", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIOperatorPopupConfirm.CleanupInstance)).GetInstance<NKCUIOperatorPopupConfirm>();
					NKCUIOperatorPopupConfirm.m_Instance.Init();
				}
				return NKCUIOperatorPopupConfirm.m_Instance;
			}
		}

		// Token: 0x170012EA RID: 4842
		// (get) Token: 0x0600719F RID: 29087 RVA: 0x0025C52F File Offset: 0x0025A72F
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIOperatorPopupConfirm.m_Instance != null && NKCUIOperatorPopupConfirm.m_Instance.IsOpen;
			}
		}

		// Token: 0x060071A0 RID: 29088 RVA: 0x0025C54A File Offset: 0x0025A74A
		public static void CheckInstanceAndClose()
		{
			if (NKCUIOperatorPopupConfirm.m_Instance != null && NKCUIOperatorPopupConfirm.m_Instance.IsOpen)
			{
				NKCUIOperatorPopupConfirm.m_Instance.Close();
			}
		}

		// Token: 0x060071A1 RID: 29089 RVA: 0x0025C56F File Offset: 0x0025A76F
		private static void CleanupInstance()
		{
			NKCUIOperatorPopupConfirm.m_Instance = null;
		}

		// Token: 0x170012EB RID: 4843
		// (get) Token: 0x060071A2 RID: 29090 RVA: 0x0025C577 File Offset: 0x0025A777
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170012EC RID: 4844
		// (get) Token: 0x060071A3 RID: 29091 RVA: 0x0025C57A File Offset: 0x0025A77A
		public override string MenuName
		{
			get
			{
				return "POPUP_OPERATOR_CONFIRM";
			}
		}

		// Token: 0x060071A4 RID: 29092 RVA: 0x0025C581 File Offset: 0x0025A781
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060071A5 RID: 29093 RVA: 0x0025C58F File Offset: 0x0025A78F
		private void Init()
		{
			NKCUtil.SetBindFunction(this.m_Confirm, new UnityAction(this.OnConfirm));
			NKCUtil.SetHotkey(this.m_Confirm, HotkeyEventType.Confirm, null, false);
			NKCUtil.SetBindFunction(this.m_Cancel, new UnityAction(this.OnCancel));
		}

		// Token: 0x060071A6 RID: 29094 RVA: 0x0025C5D0 File Offset: 0x0025A7D0
		public void Open(long targetUID, int BaseUnitID = 0, UnityAction callBack = null)
		{
			NKMOperator operatorData = NKCOperatorUtil.GetOperatorData(targetUID);
			if (operatorData == null)
			{
				return;
			}
			NKMOperator operatorData2 = NKCOperatorUtil.GetOperatorData(targetUID);
			if (operatorData2 != null)
			{
				this.m_OperatorSlot.SetData(operatorData2, NKMDeckIndex.None, true, null);
			}
			NKCUtil.SetLabelText(this.m_lbStatHP, NKCOperatorUtil.GetStatPercentageString(operatorData, NKM_STAT_TYPE.NST_HP));
			NKCUtil.SetLabelText(this.m_lbStatAtk, NKCOperatorUtil.GetStatPercentageString(operatorData, NKM_STAT_TYPE.NST_ATK));
			NKCUtil.SetLabelText(this.m_lbStatDef, NKCOperatorUtil.GetStatPercentageString(operatorData, NKM_STAT_TYPE.NST_DEF));
			NKCUtil.SetLabelText(this.m_lbSkillCollReduce, NKCOperatorUtil.GetStatPercentageString(operatorData, NKM_STAT_TYPE.NST_SKILL_COOL_TIME_REDUCE_RATE));
			this.m_MainSkill.SetData(operatorData.mainSkill.id, (int)operatorData.mainSkill.level, false);
			this.m_Subskill.SetData(operatorData.subSkill.id, (int)operatorData.subSkill.level, false);
			this.m_strFinalConfirmMsg = "";
			int num = 0;
			if (operatorData2.level > 1)
			{
				num++;
			}
			if (operatorData2.mainSkill.level > 1)
			{
				num++;
			}
			if (operatorData2.subSkill.level > 1)
			{
				num++;
			}
			if (num > 1)
			{
				this.m_strFinalConfirmMsg = NKCUtilString.GET_STRING_OPERATOR_CONFIRM_POPUP_CONFIRM_WARNING;
			}
			else
			{
				if (operatorData2.level > 1)
				{
					this.m_strFinalConfirmMsg = NKCUtilString.GET_STRING_OPERATOR_CONFIRM_POPUP_CONFIRM_WARNING_UNIT_LEVEL;
				}
				if (operatorData2.mainSkill.level > 1)
				{
					this.m_strFinalConfirmMsg = NKCUtilString.GET_STRING_OPERATOR_CONFIRM_POPUP_CONFIRM_WARNING_UNIT_ACTIVE_SKILL_LEVEL;
				}
				if (operatorData2.subSkill.level > 1)
				{
					this.m_strFinalConfirmMsg = NKCUtilString.GET_STRING_OPERATOR_CONFIRM_POPUP_CONFIRM_WARNING_UNIT_PASSIVE_SKILL_LEVEL;
				}
			}
			NKCUtil.SetLabelText(this.m_lbTitle, NKCUtilString.GET_STRING_OPERATOR_CONFIRM_POPUP_TITLE_TRANSFER);
			this.SetCostItem(BaseUnitID);
			base.UIOpened(true);
			this.m_OK = callBack;
		}

		// Token: 0x060071A7 RID: 29095 RVA: 0x0025C74C File Offset: 0x0025A94C
		public void Open(long targetUID, UnityAction callBack = null)
		{
			NKMOperator operatorData = NKCOperatorUtil.GetOperatorData(targetUID);
			if (operatorData == null)
			{
				return;
			}
			NKMOperator operatorData2 = NKCOperatorUtil.GetOperatorData(targetUID);
			if (operatorData2 != null)
			{
				this.m_OperatorSlot.SetData(operatorData2, NKMDeckIndex.None, true, null);
			}
			NKCUtil.SetLabelText(this.m_lbStatHP, NKCOperatorUtil.GetStatPercentageString(operatorData, NKM_STAT_TYPE.NST_HP));
			NKCUtil.SetLabelText(this.m_lbStatAtk, NKCOperatorUtil.GetStatPercentageString(operatorData, NKM_STAT_TYPE.NST_ATK));
			NKCUtil.SetLabelText(this.m_lbStatDef, NKCOperatorUtil.GetStatPercentageString(operatorData, NKM_STAT_TYPE.NST_DEF));
			NKCUtil.SetLabelText(this.m_lbSkillCollReduce, NKCOperatorUtil.GetStatPercentageString(operatorData, NKM_STAT_TYPE.NST_SKILL_COOL_TIME_REDUCE_RATE));
			this.m_MainSkill.SetData(operatorData.mainSkill.id, (int)operatorData.mainSkill.level, false);
			this.m_Subskill.SetData(operatorData.subSkill.id, (int)operatorData.subSkill.level, false);
			NKCUtil.SetLabelText(this.m_lbTitle, NKCUtilString.GET_STRING_OPERATOR_CONFIRM_POPUP_TITLE_SELECT);
			NKCUtil.SetGameobjectActive(this.m_CostItem, false);
			base.UIOpened(true);
			this.m_OK = callBack;
		}

		// Token: 0x060071A8 RID: 29096 RVA: 0x0025C838 File Offset: 0x0025AA38
		private void SetCostItem(int BaseUnitID)
		{
			bool bValue = false;
			if (BaseUnitID != 0)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(BaseUnitID);
				if (unitTempletBase != null)
				{
					NKMOperatorConstTemplet.HostUnit hostUnit = NKMCommonConst.OperatorConstTemplet.hostUnits.Find((NKMOperatorConstTemplet.HostUnit e) => e.m_NKM_UNIT_GRADE == unitTempletBase.m_NKM_UNIT_GRADE);
					if (hostUnit != null)
					{
						this.m_CostItemSlot.SetData(NKCUISlot.SlotData.MakeMiscItemData(hostUnit.itemId, (long)hostUnit.itemCount, 0), false, true, true, null);
						bValue = true;
					}
				}
			}
			NKCUtil.SetGameobjectActive(this.m_CostItem, bValue);
		}

		// Token: 0x060071A9 RID: 29097 RVA: 0x0025C8B3 File Offset: 0x0025AAB3
		private void OnCancel()
		{
			base.Close();
		}

		// Token: 0x060071AA RID: 29098 RVA: 0x0025C8BB File Offset: 0x0025AABB
		private void OnConfirm()
		{
			if (!string.IsNullOrEmpty(this.m_strFinalConfirmMsg))
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, this.m_strFinalConfirmMsg, new NKCPopupOKCancel.OnButton(this.OnConfirmOperatorMix), null, false);
				return;
			}
			this.OnConfirmOperatorMix();
		}

		// Token: 0x060071AB RID: 29099 RVA: 0x0025C8EF File Offset: 0x0025AAEF
		private void OnConfirmOperatorMix()
		{
			UnityAction ok = this.m_OK;
			if (ok != null)
			{
				ok();
			}
			base.Close();
		}

		// Token: 0x04005D8E RID: 23950
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_operator_info";

		// Token: 0x04005D8F RID: 23951
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_OPERATOR_CONFIRM";

		// Token: 0x04005D90 RID: 23952
		private static NKCUIOperatorPopupConfirm m_Instance;

		// Token: 0x04005D91 RID: 23953
		public Text m_lbTitle;

		// Token: 0x04005D92 RID: 23954
		public NKCUIOperatorSelectListSlot m_OperatorSlot;

		// Token: 0x04005D93 RID: 23955
		public Text m_lbStatHP;

		// Token: 0x04005D94 RID: 23956
		public Text m_lbStatAtk;

		// Token: 0x04005D95 RID: 23957
		public Text m_lbStatDef;

		// Token: 0x04005D96 RID: 23958
		public Text m_lbSkillCollReduce;

		// Token: 0x04005D97 RID: 23959
		public NKCUIOperatorSkill m_MainSkill;

		// Token: 0x04005D98 RID: 23960
		public NKCUIOperatorSkill m_Subskill;

		// Token: 0x04005D99 RID: 23961
		public GameObject m_CostItem;

		// Token: 0x04005D9A RID: 23962
		public NKCUISlot m_CostItemSlot;

		// Token: 0x04005D9B RID: 23963
		public NKCUIComStateButton m_Confirm;

		// Token: 0x04005D9C RID: 23964
		public NKCUIComStateButton m_Cancel;

		// Token: 0x04005D9D RID: 23965
		private UnityAction m_OK;

		// Token: 0x04005D9E RID: 23966
		private string m_strFinalConfirmMsg = "";
	}
}
