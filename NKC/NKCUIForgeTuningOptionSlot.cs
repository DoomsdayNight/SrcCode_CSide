using System;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000999 RID: 2457
	public class NKCUIForgeTuningOptionSlot : MonoBehaviour
	{
		// Token: 0x0600662C RID: 26156 RVA: 0x00209960 File Offset: 0x00207B60
		public void SetData(NKCUIForgeTuning.NKC_TUNING_TAB newState, int idx, NKMEquipItemData equipData)
		{
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(equipData.m_ItemEquipID);
			if (equipTemplet == null)
			{
				return;
			}
			if (equipData.m_Stat.Count <= idx + 1)
			{
				return;
			}
			bool bValue = true;
			if (newState == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_PRECISION)
			{
				bValue = false;
			}
			bool flag = false;
			if (idx == 0)
			{
				flag = NKMEquipTuningManager.IsChangeableStatGroup(equipTemplet.m_StatGroupID);
			}
			else if (idx == 1)
			{
				flag = NKMEquipTuningManager.IsChangeableStatGroup(equipTemplet.m_StatGroupID_2);
			}
			string msg;
			if (equipTemplet.IsPrivateEquip() && !flag)
			{
				msg = NKCUtilString.GET_STRING_TUNING_OPTION_SLOT_EXCLUSIVE;
				if (newState == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_OPTION_CHANGE)
				{
					NKCUtil.SetLabelText(this.m_OPTION_None_Text_off, NKCUtilString.GET_STRING_TUNING_OPTIN_CAN_NOT_CHANGE);
					NKCUtil.SetGameobjectActive(this.m_objOption_None_text_off, true);
				}
			}
			else
			{
				msg = string.Format(NKCUtilString.GET_STRING_TUNING_OPTION_SLOT_OPTION + " " + (idx + 1).ToString(), Array.Empty<object>());
			}
			NKCUtil.SetLabelText(this.m_txtOption_Number_off, msg);
			NKCUtil.SetLabelText(this.m_txtOption_Number_on, msg);
			if (newState == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_PRECISION)
			{
				bool bValue2 = false;
				int num = 0;
				if (idx == 0)
				{
					num = equipData.m_Precision;
				}
				else if (idx == 1)
				{
					num = equipData.m_Precision2;
				}
				if (num >= 100)
				{
					bValue2 = true;
				}
				this.m_NKM_UI_FACTORY_ADJUSTMENT_RATE_SLIDER_off.value = (float)num / 100f;
				this.m_NKM_UI_FACTORY_ADJUSTMENT_RATE_SLIDER_on.value = (float)num / 100f;
				this.m_NKM_UI_FACTORY_ADJUSTMENT_RATE_SLIDER_NEW_off.value = 0f;
				this.m_NKM_UI_FACTORY_ADJUSTMENT_RATE_SLIDER_NEW_on.value = 0f;
				bool flag2 = false;
				if (idx == 0)
				{
					flag2 = (equipTemplet.m_StatGroupID != 0);
				}
				else if (idx == 1)
				{
					flag2 = (equipTemplet.m_StatGroupID_2 != 0);
				}
				if (!flag2)
				{
					NKCUtil.SetLabelText(this.m_OPTION_None_Text_off, NKCUtilString.GET_STRING_TUNING_OPTIN_NONE);
					NKCUtil.SetLabelText(this.m_OPTION_None_Text_on, NKCUtilString.GET_STRING_TUNING_OPTIN_NONE);
					NKCUtil.SetGameobjectActive(this.m_objOption_None_text_off, true);
					NKCUtil.SetGameobjectActive(this.m_objOption_None_text_on, true);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objOption_None_text_off, false);
					NKCUtil.SetGameobjectActive(this.m_objOption_None_text_on, false);
				}
				string tuningOptionStatString = NKCUIForgeTuning.GetTuningOptionStatString(equipData, idx + 1);
				NKCUtil.SetLabelText(this.m_txtOPTION_TEXT_off, tuningOptionStatString);
				NKCUtil.SetLabelText(this.m_txtOPTION_TEXT_on, tuningOptionStatString);
				NKCUtil.SetLabelText(this.m_txtOPTION_PRECISION_NUMBER_TEXT_off, string.Format("{0}%", num));
				NKCUtil.SetLabelText(this.m_txtOPTION_PRECISION_NUMBER_TEXT_on, string.Format("{0}%", num));
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_ADJUSTMENT_RATE_SLIDER_MAX_off, bValue2);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_ADJUSTMENT_RATE_SLIDER_MAX_on, bValue2);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_ADJUSTMENT_RATE_MAX_off, bValue2);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_ADJUSTMENT_RATE_MAX_on, bValue2);
			}
			else if (newState == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_OPTION_CHANGE)
			{
				if (!flag)
				{
					NKCUtil.SetLabelText(this.m_OPTION_None_Text_off, NKCUtilString.GET_STRING_TUNING_OPTIN_CAN_NOT_CHANGE);
					NKCUtil.SetLabelText(this.m_OPTION_None_Text_on, NKCUtilString.GET_STRING_TUNING_OPTIN_CAN_NOT_CHANGE);
					NKCUtil.SetGameobjectActive(this.m_objOption_None_text_off, true);
					NKCUtil.SetGameobjectActive(this.m_objOption_None_text_on, true);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objOption_None_text_off, false);
					NKCUtil.SetGameobjectActive(this.m_objOption_None_text_on, false);
				}
				NKCUIForgeTuningStatSlot statSlot_Option_Change_Before_off = this.m_statSlot_Option_Change_Before_off;
				if (statSlot_Option_Change_Before_off != null)
				{
					statSlot_Option_Change_Before_off.SetData(true, equipData, idx);
				}
				NKCUIForgeTuningStatSlot statSlot_Option_Change_Before_on = this.m_statSlot_Option_Change_Before_on;
				if (statSlot_Option_Change_Before_on != null)
				{
					statSlot_Option_Change_Before_on.SetData(true, equipData, idx);
				}
				NKCUIForgeTuningStatSlot statSlot_Option_Change_After_off = this.m_statSlot_Option_Change_After_off;
				if (statSlot_Option_Change_After_off != null)
				{
					statSlot_Option_Change_After_off.SetData(false, equipData, idx);
				}
				NKCUIForgeTuningStatSlot statSlot_Option_Change_After_on = this.m_statSlot_Option_Change_After_on;
				if (statSlot_Option_Change_After_on != null)
				{
					statSlot_Option_Change_After_on.SetData(false, equipData, idx);
				}
			}
			NKCUtil.SetGameobjectActive(this.m_txtOPTION_TEXT_off.gameObject, newState == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_PRECISION);
			NKCUtil.SetGameobjectActive(this.m_txtOPTION_TEXT_on.gameObject, newState == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_PRECISION);
			NKCUtil.SetGameobjectActive(this.m_objAdjustment_rate_off, newState == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_PRECISION);
			NKCUtil.SetGameobjectActive(this.m_objAdjustment_rate_on, newState == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_PRECISION);
			NKCUtil.SetGameobjectActive(this.m_objOption_Change_off, bValue);
			NKCUtil.SetGameobjectActive(this.m_objOption_Change_on, bValue);
		}

		// Token: 0x0600662D RID: 26157 RVA: 0x00209CB8 File Offset: 0x00207EB8
		public void SetPrecisionRate(NKMEquipItemData equipData, int idx)
		{
			if (equipData == null)
			{
				return;
			}
			NKMEquipItemData itemEquip = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(equipData.m_ItemUid);
			if (itemEquip == null)
			{
				return;
			}
			int num;
			int num2;
			if (idx == 0)
			{
				num = itemEquip.m_Precision;
				num2 = equipData.m_Precision;
			}
			else
			{
				num = itemEquip.m_Precision2;
				num2 = equipData.m_Precision2;
			}
			if (num >= 100)
			{
				return;
			}
			if (num >= num2)
			{
				this.m_NKM_UI_FACTORY_ADJUSTMENT_RATE_SLIDER_off.value = (float)num / 100f;
				this.m_NKM_UI_FACTORY_ADJUSTMENT_RATE_SLIDER_on.value = (float)num / 100f;
				this.m_NKM_UI_FACTORY_ADJUSTMENT_RATE_SLIDER_NEW_off.value = (float)num2 / 100f;
				this.m_NKM_UI_FACTORY_ADJUSTMENT_RATE_SLIDER_NEW_on.value = (float)num2 / 100f;
				return;
			}
			this.m_NKM_UI_FACTORY_ADJUSTMENT_RATE_SLIDER_off.value = (float)num / 100f;
			this.m_NKM_UI_FACTORY_ADJUSTMENT_RATE_SLIDER_on.value = (float)num / 100f;
			this.m_NKM_UI_FACTORY_ADJUSTMENT_RATE_SLIDER_NEW_off.value = 0f;
			this.m_NKM_UI_FACTORY_ADJUSTMENT_RATE_SLIDER_NEW_on.value = 0f;
		}

		// Token: 0x0600662E RID: 26158 RVA: 0x00209DAA File Offset: 0x00207FAA
		public string GetStatText(bool before)
		{
			if (before)
			{
				return this.m_statSlot_Option_Change_Before_on.m_STAT_TEXT.text;
			}
			return this.m_statSlot_Option_Change_After_on.m_STAT_TEXT.text;
		}

		// Token: 0x0600662F RID: 26159 RVA: 0x00209DD0 File Offset: 0x00207FD0
		public void ClearPrecisionRate()
		{
			this.m_NKM_UI_FACTORY_ADJUSTMENT_RATE_SLIDER_off.value = 0f;
			this.m_NKM_UI_FACTORY_ADJUSTMENT_RATE_SLIDER_on.value = 0f;
			this.m_NKM_UI_FACTORY_ADJUSTMENT_RATE_SLIDER_NEW_off.value = 0f;
			this.m_NKM_UI_FACTORY_ADJUSTMENT_RATE_SLIDER_NEW_on.value = 0f;
		}

		// Token: 0x06006630 RID: 26160 RVA: 0x00209E20 File Offset: 0x00208020
		public void ClearUI(bool bForce = false)
		{
			if (bForce)
			{
				NKCUtil.SetLabelText(this.m_txtOption_Number_off, "-");
				NKCUtil.SetLabelText(this.m_txtOption_Number_on, "-");
				NKCUtil.SetLabelText(this.m_OPTION_None_Text_off, NKCUtilString.GET_STRING_TUNING_OPTIN_NONE);
				NKCUtil.SetLabelText(this.m_OPTION_None_Text_on, NKCUtilString.GET_STRING_TUNING_OPTIN_NONE);
			}
			NKCUtil.SetGameobjectActive(this.m_txtOPTION_TEXT_off.gameObject, false);
			NKCUtil.SetGameobjectActive(this.m_txtOPTION_TEXT_on.gameObject, false);
			NKCUtil.SetGameobjectActive(this.m_objAdjustment_rate_off, false);
			NKCUtil.SetGameobjectActive(this.m_objAdjustment_rate_on, false);
			NKCUtil.SetGameobjectActive(this.m_objOption_Change_off, false);
			NKCUtil.SetGameobjectActive(this.m_objOption_Change_on, false);
			NKCUtil.SetGameobjectActive(this.m_objOption_None_text_off, true);
			NKCUtil.SetGameobjectActive(this.m_objOption_None_text_on, true);
			NKCUtil.SetGameobjectActive(this.m_objNKM_UI_FACTORY_TUNING_OPTION_SLOT_OFF, true);
			NKCUtil.SetGameobjectActive(this.m_objNKM_UI_FACTORY_TUNING_OPTION_SLOT_ON, false);
		}

		// Token: 0x040051CC RID: 20940
		[Header("옵션 on/off")]
		public GameObject m_objNKM_UI_FACTORY_TUNING_OPTION_SLOT_OFF;

		// Token: 0x040051CD RID: 20941
		public GameObject m_objNKM_UI_FACTORY_TUNING_OPTION_SLOT_ON;

		// Token: 0x040051CE RID: 20942
		[Header("정밀화 on/off")]
		public GameObject m_objAdjustment_rate_off;

		// Token: 0x040051CF RID: 20943
		public GameObject m_objAdjustment_rate_on;

		// Token: 0x040051D0 RID: 20944
		[Header("옵션 변경 on/off")]
		public GameObject m_objOption_Change_off;

		// Token: 0x040051D1 RID: 20945
		public GameObject m_objOption_Change_on;

		// Token: 0x040051D2 RID: 20946
		[Header("슬라이더 설정")]
		public Slider m_NKM_UI_FACTORY_ADJUSTMENT_RATE_SLIDER_off;

		// Token: 0x040051D3 RID: 20947
		public Slider m_NKM_UI_FACTORY_ADJUSTMENT_RATE_SLIDER_on;

		// Token: 0x040051D4 RID: 20948
		public Slider m_NKM_UI_FACTORY_ADJUSTMENT_RATE_SLIDER_NEW_off;

		// Token: 0x040051D5 RID: 20949
		public Slider m_NKM_UI_FACTORY_ADJUSTMENT_RATE_SLIDER_NEW_on;

		// Token: 0x040051D6 RID: 20950
		[Header("최대치 슬라이더")]
		public GameObject m_NKM_UI_FACTORY_ADJUSTMENT_RATE_SLIDER_MAX_off;

		// Token: 0x040051D7 RID: 20951
		public GameObject m_NKM_UI_FACTORY_ADJUSTMENT_RATE_SLIDER_MAX_on;

		// Token: 0x040051D8 RID: 20952
		[Header("최대치 MAX 표시")]
		public GameObject m_NKM_UI_FACTORY_ADJUSTMENT_RATE_MAX_off;

		// Token: 0x040051D9 RID: 20953
		public GameObject m_NKM_UI_FACTORY_ADJUSTMENT_RATE_MAX_on;

		// Token: 0x040051DA RID: 20954
		[Header("옵션 변경")]
		public NKCUIForgeTuningStatSlot m_statSlot_Option_Change_Before_off;

		// Token: 0x040051DB RID: 20955
		public NKCUIForgeTuningStatSlot m_statSlot_Option_Change_Before_on;

		// Token: 0x040051DC RID: 20956
		public NKCUIForgeTuningStatSlot m_statSlot_Option_Change_After_off;

		// Token: 0x040051DD RID: 20957
		public NKCUIForgeTuningStatSlot m_statSlot_Option_Change_After_on;

		// Token: 0x040051DE RID: 20958
		public Text m_txtOption_Number_off;

		// Token: 0x040051DF RID: 20959
		public Text m_txtOption_Number_on;

		// Token: 0x040051E0 RID: 20960
		[Header("옵션")]
		public Text m_txtOPTION_TEXT_off;

		// Token: 0x040051E1 RID: 20961
		public Text m_txtOPTION_TEXT_on;

		// Token: 0x040051E2 RID: 20962
		public Text m_txtOPTION_PRECISION_NUMBER_TEXT_off;

		// Token: 0x040051E3 RID: 20963
		public Text m_txtOPTION_PRECISION_NUMBER_TEXT_on;

		// Token: 0x040051E4 RID: 20964
		public GameObject m_objOption_None_text_off;

		// Token: 0x040051E5 RID: 20965
		public GameObject m_objOption_None_text_on;

		// Token: 0x040051E6 RID: 20966
		public Text m_OPTION_None_Text_off;

		// Token: 0x040051E7 RID: 20967
		public Text m_OPTION_None_Text_on;
	}
}
