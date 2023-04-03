using System;
using System.Collections.Generic;
using System.Linq;
using NKC.UI.Tooltip;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200099D RID: 2461
	public class NKCUIForgeUpgradeStatSlot : MonoBehaviour
	{
		// Token: 0x06006667 RID: 26215 RVA: 0x0020B228 File Offset: 0x00209428
		public void InitUI()
		{
			this.m_btnInfo.PointerDown.RemoveAllListeners();
			this.m_btnInfo.PointerDown.AddListener(new UnityAction<PointerEventData>(this.OnPressInfoTooltip));
			this.m_btnInfo.PointerUp.RemoveAllListeners();
			this.m_btnInfo.PointerUp.AddListener(delegate()
			{
				NKCUITooltip.Instance.Close();
			});
			this.m_btnWarning.PointerDown.RemoveAllListeners();
			this.m_btnWarning.PointerDown.AddListener(new UnityAction<PointerEventData>(this.OnPressWarningTooltip));
			this.m_btnWarning.PointerUp.RemoveAllListeners();
			this.m_btnWarning.PointerUp.AddListener(delegate()
			{
				NKCUITooltip.Instance.Close();
			});
			this.m_btnDetail.PointerClick.RemoveAllListeners();
			this.m_btnDetail.PointerClick.AddListener(new UnityAction(this.OnClickDetail));
		}

		// Token: 0x06006668 RID: 26216 RVA: 0x0020B338 File Offset: 0x00209538
		public void SetData(int idx, NKMEquipItemData prevEquipData, NKMEquipItemData nextEquipData)
		{
			this.m_PrevEquipData = prevEquipData;
			this.m_IDX = idx;
			NKCUtil.SetGameobjectActive(this.m_btnWarning, idx == 0);
			NKCUtil.SetGameobjectActive(this.m_btnInfo, idx != 0 && idx < prevEquipData.m_Stat.Count && prevEquipData.m_Stat[idx].type != NKM_STAT_TYPE.NST_RANDOM);
			NKCUtil.SetGameobjectActive(this.m_btnDetail, idx >= prevEquipData.m_Stat.Count || prevEquipData.m_Stat[idx].type == NKM_STAT_TYPE.NST_RANDOM);
			NKCUtil.SetLabelText(this.m_lbOptionType, this.GetOptionType(idx));
			if (idx == prevEquipData.m_Stat.Count)
			{
				NKMItemEquipSetOptionTemplet equipSetOptionTemplet = NKMItemManager.GetEquipSetOptionTemplet(prevEquipData.m_SetOptionId);
				if (equipSetOptionTemplet != null)
				{
					NKCUtil.SetLabelText(this.m_lbOptionName, NKCStringTable.GetString(equipSetOptionTemplet.m_EquipSetName, false));
				}
				else
				{
					NKCUtil.SetLabelText(this.m_lbOptionName, NKCUtilString.GET_STRING_FACTORY_UPGRADE_OPTION_SUCCESSION);
				}
				NKCUtil.SetLabelText(this.m_lbPrevStat, "");
				NKCUtil.SetGameobjectActive(this.m_objArrow, false);
				NKCUtil.SetLabelText(this.m_lbNextStat, "");
				return;
			}
			if (prevEquipData.m_Stat[idx].type == NKM_STAT_TYPE.NST_RANDOM)
			{
				NKCUtil.SetLabelText(this.m_lbOptionName, NKCUtilString.GET_STRING_FACTORY_UPGRADE_OPTION_SUCCESSION);
				NKCUtil.SetLabelText(this.m_lbPrevStat, "");
				NKCUtil.SetGameobjectActive(this.m_objArrow, false);
				NKCUtil.SetLabelText(this.m_lbNextStat, "");
				return;
			}
			bool flag = prevEquipData.m_Stat[idx].stat_value < 0f || prevEquipData.m_Stat[idx].stat_factor < 0f;
			bool flag2 = NKCUtilString.IsNameReversedIfNegative(prevEquipData.m_Stat[idx].type) && flag;
			NKCUtil.SetLabelText(this.m_lbOptionName, NKCUtilString.GetStatShortName(prevEquipData.m_Stat[idx].type, flag));
			NKCUtil.SetGameobjectActive(this.m_objArrow, true);
			if (prevEquipData.m_Stat[idx].stat_value != 0f || prevEquipData.m_Stat[idx].stat_factor != 0f)
			{
				NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(nextEquipData.m_ItemEquipID);
				IReadOnlyList<NKMEquipRandomStatTemplet> readOnlyList = null;
				if (idx == 1)
				{
					readOnlyList = NKMEquipTuningManager.GetEquipRandomStatGroupList(equipTemplet.m_StatGroupID);
				}
				else if (idx == 2)
				{
					readOnlyList = NKMEquipTuningManager.GetEquipRandomStatGroupList(equipTemplet.m_StatGroupID_2);
				}
				NKMEquipRandomStatTemplet nkmequipRandomStatTemplet = null;
				if (readOnlyList != null && readOnlyList.Count > 0)
				{
					nkmequipRandomStatTemplet = readOnlyList.ToList<NKMEquipRandomStatTemplet>().Find((NKMEquipRandomStatTemplet x) => x.m_StatType == prevEquipData.m_Stat[idx].type);
				}
				if ((nkmequipRandomStatTemplet != null && NKCUIForgeTuning.IsPercentStat(nkmequipRandomStatTemplet)) || NKMUnitStatManager.IsPercentStat(prevEquipData.m_Stat[idx].type))
				{
					float num = (prevEquipData.m_Stat[idx].stat_factor != 0f) ? prevEquipData.m_Stat[idx].stat_factor : prevEquipData.m_Stat[idx].stat_value;
					if (flag2)
					{
						num = Mathf.Abs(num);
					}
					decimal num2 = new decimal(num + (float)prevEquipData.m_EnchantLevel * prevEquipData.m_Stat[idx].stat_level_value);
					num2 = Math.Round(num2 * 1000m) / 1000m;
					NKCUtil.SetLabelText(this.m_lbPrevStat, string.Format("{0:P1}", num2));
					if (readOnlyList == null)
					{
						float num3 = (nextEquipData.m_Stat[idx].stat_factor != 0f) ? nextEquipData.m_Stat[idx].stat_factor : nextEquipData.m_Stat[idx].stat_value;
						if (flag2)
						{
							num3 = Mathf.Abs(num3);
						}
						NKCUtil.SetLabelText(this.m_lbNextStat, string.Format("{0:P1}", Math.Round((double)(num3 * 1000f)) / 1000.0));
						return;
					}
					using (IEnumerator<NKMEquipRandomStatTemplet> enumerator = readOnlyList.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							NKMEquipRandomStatTemplet nkmequipRandomStatTemplet2 = enumerator.Current;
							if (nkmequipRandomStatTemplet2.m_StatType == prevEquipData.m_Stat[idx].type)
							{
								decimal d = new decimal((nkmequipRandomStatTemplet2.m_MaxStatValue > 0f) ? nkmequipRandomStatTemplet2.m_MaxStatValue : nkmequipRandomStatTemplet2.m_MaxStatRate);
								NKCUtil.SetLabelText(this.m_lbNextStat, string.Format("{0:P1}", Math.Round(d * 1000m) / 1000m));
								break;
							}
						}
						return;
					}
				}
				float num4 = prevEquipData.m_Stat[idx].stat_value + (float)prevEquipData.m_EnchantLevel * prevEquipData.m_Stat[idx].stat_level_value;
				if (flag2)
				{
					num4 = Mathf.Abs(num4);
				}
				NKCUtil.SetLabelText(this.m_lbPrevStat, string.Format("{0:+#;-#;''}", num4));
				if (readOnlyList == null)
				{
					float num5 = flag2 ? Mathf.Abs(nextEquipData.m_Stat[idx].stat_value) : nextEquipData.m_Stat[idx].stat_value;
					NKCUtil.SetLabelText(this.m_lbNextStat, string.Format("{0:+#;-#;''}", num5));
					return;
				}
				using (IEnumerator<NKMEquipRandomStatTemplet> enumerator = readOnlyList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						NKMEquipRandomStatTemplet nkmequipRandomStatTemplet3 = enumerator.Current;
						if (nkmequipRandomStatTemplet3.m_StatType == prevEquipData.m_Stat[idx].type)
						{
							float num6 = (nkmequipRandomStatTemplet3.m_MaxStatRate > 0f) ? nkmequipRandomStatTemplet3.m_MaxStatRate : nkmequipRandomStatTemplet3.m_MaxStatValue;
							if (flag2)
							{
								num6 = Mathf.Abs(num6);
							}
							NKCUtil.SetLabelText(this.m_lbNextStat, string.Format("{0:+#;-#;''}", num6));
							break;
						}
					}
					return;
				}
			}
			NKCUtil.SetLabelText(this.m_lbPrevStat, "");
			NKCUtil.SetGameobjectActive(this.m_objArrow, false);
			NKCUtil.SetLabelText(this.m_lbNextStat, "");
		}

		// Token: 0x06006669 RID: 26217 RVA: 0x0020BA6C File Offset: 0x00209C6C
		private string GetOptionType(int idx)
		{
			switch (idx)
			{
			case 0:
				return NKCUtilString.GET_STRING_EQUIP_OPTION_MAIN;
			case 1:
				return NKCUtilString.GET_STRING_EQUIP_OPTION_1;
			case 2:
				return NKCUtilString.GET_STRING_EQUIP_OPTION_2;
			case 3:
				return NKCUtilString.GET_STRING_EQUIP_OPTION_SET;
			default:
				return "";
			}
		}

		// Token: 0x0600666A RID: 26218 RVA: 0x0020BAA3 File Offset: 0x00209CA3
		private void OnPressInfoTooltip(PointerEventData e)
		{
			NKCUITooltip.Instance.Open(NKCUISlot.eSlotMode.Etc, "", NKCUtilString.GET_STRING_FACTORY_UPGRADE_MAIN_SUB_TOOLTIP, new Vector2?(e.position));
		}

		// Token: 0x0600666B RID: 26219 RVA: 0x0020BAC6 File Offset: 0x00209CC6
		private void OnPressWarningTooltip(PointerEventData e)
		{
			NKCUITooltip.Instance.Open(NKCUISlot.eSlotMode.Etc, "", NKCUtilString.GET_STRING_FACTORY_UPGRADE_MAIN_OPTION_TOOLTIP, new Vector2?(e.position));
		}

		// Token: 0x0600666C RID: 26220 RVA: 0x0020BAEC File Offset: 0x00209CEC
		private void OnClickDetail()
		{
			if (this.m_IDX < 3)
			{
				if (this.NKCPopupEquipOption != null)
				{
					this.NKCPopupEquipOption.Open(this.m_PrevEquipData, this.m_IDX, string.Empty);
					return;
				}
			}
			else if (this.NKCPopupEquipSetOption != null)
			{
				this.NKCPopupEquipSetOption.Open(this.m_PrevEquipData, string.Empty);
			}
		}

		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x0600666D RID: 26221 RVA: 0x0020BB54 File Offset: 0x00209D54
		private NKCPopupEquipOptionList NKCPopupEquipOption
		{
			get
			{
				if (this.m_NKCPopupEquipOption == null)
				{
					NKCUIManager.LoadedUIData loadedUIData = NKCUIManager.OpenNewInstance<NKCPopupEquipOptionList>("AB_UI_NKM_UI_FACTORY", "NKM_UI_FACTORY_EQUIP_OPTION_POPUP", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), null);
					this.m_NKCPopupEquipOption = loadedUIData.GetInstance<NKCPopupEquipOptionList>();
					if (this.m_NKCPopupEquipOption != null)
					{
						this.m_NKCPopupEquipOption.InitUI();
					}
				}
				return this.m_NKCPopupEquipOption;
			}
		}

		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x0600666E RID: 26222 RVA: 0x0020BBB4 File Offset: 0x00209DB4
		private NKCPopupEquipSetOptionList NKCPopupEquipSetOption
		{
			get
			{
				if (this.m_NKCPopupEquipSetOption == null)
				{
					NKCUIManager.LoadedUIData loadedUIData = NKCUIManager.OpenNewInstance<NKCPopupEquipSetOptionList>("AB_UI_NKM_UI_FACTORY", "NKM_UI_FACTORY_EQUIP_SET_LIST_POPUP", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), null);
					this.m_NKCPopupEquipSetOption = loadedUIData.GetInstance<NKCPopupEquipSetOptionList>();
					if (this.m_NKCPopupEquipSetOption != null)
					{
						this.m_NKCPopupEquipSetOption.InitUI();
					}
				}
				return this.m_NKCPopupEquipSetOption;
			}
		}

		// Token: 0x0400521B RID: 21019
		public Text m_lbOptionType;

		// Token: 0x0400521C RID: 21020
		public Text m_lbOptionName;

		// Token: 0x0400521D RID: 21021
		public Text m_lbPrevStat;

		// Token: 0x0400521E RID: 21022
		public GameObject m_objArrow;

		// Token: 0x0400521F RID: 21023
		public Text m_lbNextStat;

		// Token: 0x04005220 RID: 21024
		public NKCUIComStateButton m_btnInfo;

		// Token: 0x04005221 RID: 21025
		public NKCUIComStateButton m_btnWarning;

		// Token: 0x04005222 RID: 21026
		public NKCUIComStateButton m_btnDetail;

		// Token: 0x04005223 RID: 21027
		private NKMEquipItemData m_PrevEquipData;

		// Token: 0x04005224 RID: 21028
		private int m_IDX;

		// Token: 0x04005225 RID: 21029
		private NKCPopupEquipOptionList m_NKCPopupEquipOption;

		// Token: 0x04005226 RID: 21030
		private NKCPopupEquipSetOptionList m_NKCPopupEquipSetOption;

		// Token: 0x02001673 RID: 5747
		private enum STAT_SLOT_TYPE
		{
			// Token: 0x0400A458 RID: 42072
			MAIN,
			// Token: 0x0400A459 RID: 42073
			SUB_01,
			// Token: 0x0400A45A RID: 42074
			SUB_02,
			// Token: 0x0400A45B RID: 42075
			SET
		}
	}
}
