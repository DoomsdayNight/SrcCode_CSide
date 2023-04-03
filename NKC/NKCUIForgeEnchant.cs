using System;
using System.Collections.Generic;
using ClientPacket.Item;
using NKC.PacketHandler;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000995 RID: 2453
	public class NKCUIForgeEnchant : MonoBehaviour
	{
		// Token: 0x060065C6 RID: 26054 RVA: 0x00206060 File Offset: 0x00204260
		public void InitUI()
		{
			if (this.m_listNKCUIEnhanceSlot != null)
			{
				for (int i = 0; i < this.m_listNKCUIEnhanceSlot.Count; i++)
				{
					if (this.m_listNKCUIEnhanceSlot[i] != null)
					{
						this.m_listNKCUIEnhanceSlot[i].Init();
					}
				}
			}
			this.m_btn_NKM_UI_FACTORY_ENCHANT_AUTOSELECT.PointerClick.RemoveAllListeners();
			this.m_btn_NKM_UI_FACTORY_ENCHANT_AUTOSELECT.PointerClick.AddListener(new UnityAction(this.AutoSelect));
			this.m_btn_NKM_UI_FACTORY_ENCHANT_BUTTON_RESET.PointerClick.RemoveAllListeners();
			this.m_btn_NKM_UI_FACTORY_ENCHANT_BUTTON_RESET.PointerClick.AddListener(new UnityAction(this.ResetMaterialEquipSlotsToEnhance));
			this.m_btnNKM_UI_FACTORY_ENCHANT_BUTTON_ENCHANT.PointerClick.RemoveAllListeners();
			this.m_btnNKM_UI_FACTORY_ENCHANT_BUTTON_ENCHANT.PointerClick.AddListener(new UnityAction(this.TrySendEquipEnchant));
		}

		// Token: 0x060065C7 RID: 26055 RVA: 0x00206134 File Offset: 0x00204334
		public void SetLeftEquipUID(long uid)
		{
			this.m_LeftEquipUID = uid;
			this.EnableUI(this.m_LeftEquipUID != 0L);
		}

		// Token: 0x060065C8 RID: 26056 RVA: 0x0020614D File Offset: 0x0020434D
		public void SetOut()
		{
			this.m_rmEnchantRoot.Set("Out");
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060065C9 RID: 26057 RVA: 0x0020616B File Offset: 0x0020436B
		public void AnimateOutToIn()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_rmEnchantRoot.Set("Out");
			this.m_rmEnchantRoot.Transit("In", null);
			this.ResetMaterialEquipSlotsToEnhanceUI();
		}

		// Token: 0x060065CA RID: 26058 RVA: 0x002061A0 File Offset: 0x002043A0
		public void PlayEnhanceEffect()
		{
			for (int i = 0; i < this.m_listNKCUIEnhanceSlot.Count; i++)
			{
				if (!this.m_listNKCUIEnhanceSlot[i].IsEmpty())
				{
					NKCUtil.SetGameobjectActive(this.m_listEffect[i], true);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_listEffect[i], false);
				}
			}
		}

		// Token: 0x060065CB RID: 26059 RVA: 0x002061FC File Offset: 0x002043FC
		public void ClearEnhanceEffect()
		{
			for (int i = 0; i < this.m_listNKCUIEnhanceSlot.Count; i++)
			{
				NKCUtil.SetGameobjectActive(this.m_listEffect[i], false);
			}
		}

		// Token: 0x060065CC RID: 26060 RVA: 0x00206231 File Offset: 0x00204431
		public void ClearCurrentMaterialEquipsToEnhance()
		{
			this.m_listCurrentMaterialEquipsToEnhance.Clear();
		}

		// Token: 0x060065CD RID: 26061 RVA: 0x0020623E File Offset: 0x0020443E
		public void RemoveCurrentMaterialEquipToEnhance(long equipUID)
		{
			this.m_listCurrentMaterialEquipsToEnhance.Remove(equipUID);
		}

		// Token: 0x060065CE RID: 26062 RVA: 0x0020624D File Offset: 0x0020444D
		public void OnFinishMultiSelectionToEnhance(List<long> listEquipSlot)
		{
			if (this.m_UIInventory != null && this.m_UIInventory.IsOpen)
			{
				this.m_UIInventory.Close();
			}
			this.SetMaterials(listEquipSlot);
		}

		// Token: 0x060065CF RID: 26063 RVA: 0x0020627C File Offset: 0x0020447C
		private void SetMaterials(List<long> listEquipSlot)
		{
			this.m_listCurrentMaterialEquipsToEnhance.Clear();
			this.m_listCurrentMaterialEquipsToEnhance.AddRange(listEquipSlot);
			this.ResetMaterialEquipSlotsToEnhanceUI();
			this.ResetEnhanceStatUI();
		}

		// Token: 0x060065D0 RID: 26064 RVA: 0x002062A1 File Offset: 0x002044A1
		public void ResetMaterialEquipSlotsToEnhance()
		{
			this.ClearCurrentMaterialEquipsToEnhance();
			this.ResetMaterialEquipSlotsToEnhanceUI();
			this.ResetEnhanceStatUI();
		}

		// Token: 0x060065D1 RID: 26065 RVA: 0x002062B8 File Offset: 0x002044B8
		public void AutoSelect()
		{
			if (this.m_LeftEquipUID == 0L)
			{
				return;
			}
			NKMInventoryData inventoryData = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData;
			NKMEquipItemData itemEquip = inventoryData.GetItemEquip(this.m_LeftEquipUID);
			if (itemEquip == null)
			{
				return;
			}
			if (NKMItemManager.GetEquipTemplet(itemEquip.m_ItemEquipID) == null)
			{
				return;
			}
			List<NKMEquipItemData> list = new List<NKMEquipItemData>(inventoryData.EquipItems.Values);
			int i;
			for (i = 0; i < list.Count; i++)
			{
				bool flag = false;
				NKMEquipItemData nkmequipItemData = list[i];
				if (nkmequipItemData != null)
				{
					if (nkmequipItemData.m_OwnerUnitUID > 0L || nkmequipItemData.m_EnchantLevel > 0 || nkmequipItemData.m_bLock || nkmequipItemData.m_ItemUid == this.m_LeftEquipUID)
					{
						flag = true;
					}
					else
					{
						NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(nkmequipItemData.m_ItemEquipID);
						if (equipTemplet == null)
						{
							flag = true;
						}
						else if (equipTemplet.m_NKM_ITEM_GRADE > NKM_ITEM_GRADE.NIG_R && equipTemplet.m_EquipUnitStyleType != NKM_UNIT_STYLE_TYPE.NUST_ENCHANT)
						{
							flag = true;
						}
					}
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					list.RemoveAt(i);
					i--;
				}
			}
			List<NKCUIForgeEnchant.CandidateMaterial> list2 = new List<NKCUIForgeEnchant.CandidateMaterial>();
			for (i = 0; i < list.Count; i++)
			{
				NKMEquipItemData nkmequipItemData2 = list[i];
				NKMEquipTemplet equipTemplet2 = NKMItemManager.GetEquipTemplet(nkmequipItemData2.m_ItemEquipID);
				if (equipTemplet2 != null)
				{
					list2.Add(new NKCUIForgeEnchant.CandidateMaterial
					{
						m_NKMEquipItemData = nkmequipItemData2,
						m_NKMEquipTemplet = equipTemplet2
					});
				}
			}
			list2.Sort(new NKCUIForgeEnchant.CompForAutoSelect());
			int needExpToMaxLevel = NKMItemManager.GetNeedExpToMaxLevel(itemEquip);
			int num = 0;
			List<long> list3 = new List<long>();
			i = 0;
			while (i < list2.Count && i < this.m_listNKCUIEnhanceSlot.Count && needExpToMaxLevel > 0)
			{
				list3.Add(list2[i].m_NKMEquipItemData.m_ItemUid);
				num += NKMItemManager.GetEquipEnchantFeedExp(list2[i].m_NKMEquipItemData);
				if (num > needExpToMaxLevel)
				{
					break;
				}
				i++;
			}
			this.SetMaterials(list3);
		}

		// Token: 0x060065D2 RID: 26066 RVA: 0x00206484 File Offset: 0x00204684
		public void ResetEnhanceStatUI()
		{
			this.m_NKM_UI_FACTORY_ENCHANT_EXP_SLIDER_NEW_Slider.value = 0f;
			this.m_NKM_UI_FACTORY_ENCHANT_EXP_SLIDER_Slider.value = 0f;
			NKMEquipItemData itemEquip = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(this.m_LeftEquipUID);
			if (itemEquip == null)
			{
				return;
			}
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(itemEquip.m_ItemEquipID);
			if (equipTemplet == null)
			{
				return;
			}
			this.m_needCredit = 0;
			NKCUtil.SetLabelText(this.m_ENCHANT_BEFORE_TEXT, string.Format(NKCUtilString.GET_STRING_FORGE_ENCHANT_LEVEL_ONE_PARAM, itemEquip.m_EnchantLevel));
			int num = itemEquip.m_EnchantLevel;
			int num2 = itemEquip.m_EnchantExp;
			int num3 = 0;
			for (int i = 0; i < this.m_listCurrentMaterialEquipsToEnhance.Count; i++)
			{
				NKMEquipItemData itemEquip2 = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(this.m_listCurrentMaterialEquipsToEnhance[i]);
				if (itemEquip2 != null && NKMItemManager.GetEquipTemplet(itemEquip2.m_ItemEquipID) != null)
				{
					int equipEnchantFeedExp = NKMItemManager.GetEquipEnchantFeedExp(itemEquip2);
					if (equipEnchantFeedExp != -1)
					{
						num3 += equipEnchantFeedExp;
					}
				}
			}
			num2 += num3;
			this.m_needCredit = num3 * 8;
			NKCCompanyBuff.SetDiscountOfCreditInEnchantTuning(NKCScenManager.CurrentUserData().m_companyBuffDataList, ref this.m_needCredit);
			this.m_creditSlot.SetData(1, this.m_needCredit, NKCScenManager.CurrentUserData().GetCredit(), true, true, NKCCompanyBuff.NeedShowEventMark(NKCScenManager.CurrentUserData().m_companyBuffDataList, NKMConst.Buff.BuffType.BASE_FACTORY_ENCHANT_TUNING_CREDIT_DISCOUNT));
			int enchantRequireExp = NKMItemManager.GetEnchantRequireExp(itemEquip);
			int num4 = num2;
			while (enchantRequireExp <= num4 && num < NKMItemManager.GetMaxEquipEnchantLevel(equipTemplet.m_NKM_ITEM_TIER))
			{
				num++;
				num4 -= enchantRequireExp;
				enchantRequireExp = NKMItemManager.GetEnchantRequireExp(equipTemplet.m_NKM_ITEM_TIER, num, equipTemplet.m_NKM_ITEM_GRADE);
			}
			NKCUtil.SetGameobjectActive(this.m_ENCHANT_ARROW, num > itemEquip.m_EnchantLevel);
			NKCUtil.SetLabelText(this.m_ENCHANT_AFTER_TEXT, "");
			bool flag = false;
			if (num >= NKMItemManager.GetMaxEquipEnchantLevel(equipTemplet.m_NKM_ITEM_TIER))
			{
				this.m_NKM_UI_FACTORY_ENCHANT_EXP_SLIDER_NEW_Slider.value = 0f;
				this.m_NKM_UI_FACTORY_ENCHANT_EXP_SLIDER_Slider.value = 0f;
				if (num2 > itemEquip.m_EnchantExp)
				{
					NKCUtil.SetLabelText(this.m_ENCHANT_AFTER_TEXT, string.Format("+{0}", num));
				}
				else
				{
					flag = true;
				}
			}
			else if (num > itemEquip.m_EnchantLevel)
			{
				NKCUtil.SetLabelText(this.m_ENCHANT_AFTER_TEXT, string.Format("+{0}", num));
				this.m_NKM_UI_FACTORY_ENCHANT_EXP_SLIDER_Slider.value = 0f;
				this.m_NKM_UI_FACTORY_ENCHANT_EXP_SLIDER_NEW_Slider.value = (float)num4 / (float)enchantRequireExp;
			}
			else
			{
				this.m_NKM_UI_FACTORY_ENCHANT_EXP_SLIDER_Slider.value = (float)itemEquip.m_EnchantExp / (float)enchantRequireExp;
				this.m_NKM_UI_FACTORY_ENCHANT_EXP_SLIDER_NEW_Slider.value = (float)(num2 - itemEquip.m_EnchantExp) / (float)(enchantRequireExp - itemEquip.m_EnchantExp);
			}
			if (flag)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_ENCHANT_EXP_BG_TEXT, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_ENCHANT_EXP_BG_TEXT, true);
				if (num2 > itemEquip.m_EnchantExp)
				{
					NKCUtil.SetLabelText(this.m_NKM_UI_FACTORY_ENCHANT_EXP_BG_TEXT, string.Format("<color=#FFDB00>{0}</color>/{1}", num2, NKMItemManager.GetEnchantRequireExp(itemEquip)));
				}
				else
				{
					NKCUtil.SetLabelText(this.m_NKM_UI_FACTORY_ENCHANT_EXP_BG_TEXT, string.Format("{0}/{1}", num2, NKMItemManager.GetEnchantRequireExp(itemEquip)));
				}
			}
			float x = this.m_NKM_UI_FACTORY_ENCHANT_EXP_SLIDER.GetComponent<RectTransform>().anchoredPosition.x;
			float width = this.m_NKM_UI_FACTORY_ENCHANT_EXP_SLIDER.GetComponent<RectTransform>().GetWidth();
			float value = this.m_NKM_UI_FACTORY_ENCHANT_EXP_SLIDER_Slider.value;
			float x2 = x + width * value;
			float newSize = this.m_NKM_UI_FACTORY_ENCHANT_EXP_SLIDER.GetComponent<RectTransform>().GetWidth() * (1f - this.m_NKM_UI_FACTORY_ENCHANT_EXP_SLIDER_Slider.value);
			Vector2 anchoredPosition = this.m_NKM_UI_FACTORY_ENCHANT_EXP_SLIDER_NEW.GetComponent<RectTransform>().anchoredPosition;
			anchoredPosition.x = x2;
			this.m_NKM_UI_FACTORY_ENCHANT_EXP_SLIDER_NEW.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
			this.m_NKM_UI_FACTORY_ENCHANT_EXP_SLIDER_NEW.GetComponent<RectTransform>().SetWidth(newSize);
			NKCUtil.SetLabelText(this.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_01_Before, "");
			NKCUtil.SetLabelText(this.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_02_Before, "");
			NKCUtil.SetLabelText(this.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_01_After, "");
			NKCUtil.SetLabelText(this.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_02_After, "");
			for (int j = 0; j < itemEquip.m_Stat.Count; j++)
			{
				EQUIP_ITEM_STAT equip_ITEM_STAT = itemEquip.m_Stat[j];
				if (equip_ITEM_STAT != null && j == 0)
				{
					this.SetBeforeStatTextAndSprite(this.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_01_Before, equip_ITEM_STAT, itemEquip);
					this.SetAfterStatTextAndSprite(this.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_01_After, num, equip_ITEM_STAT, itemEquip);
				}
			}
			this.EnableUI(true);
		}

		// Token: 0x060065D3 RID: 26067 RVA: 0x002068BC File Offset: 0x00204ABC
		private void SetBeforeStatTextAndSprite(Text beforeText, EQUIP_ITEM_STAT cEQUIP_ITEM_STAT, NKMEquipItemData cNKMEquipItemDataTarget)
		{
			string statShortString = NKCUtilString.GetStatShortString(cEQUIP_ITEM_STAT.type, cEQUIP_ITEM_STAT.stat_value + cEQUIP_ITEM_STAT.stat_level_value * (float)cNKMEquipItemDataTarget.m_EnchantLevel, NKMUnitStatManager.IsPercentStat(cEQUIP_ITEM_STAT.type));
			NKCUtil.SetLabelText(beforeText, statShortString);
		}

		// Token: 0x060065D4 RID: 26068 RVA: 0x002068FC File Offset: 0x00204AFC
		private void SetAfterStatTextAndSprite(Text afterText, int preCalcEnchantLV, EQUIP_ITEM_STAT cEQUIP_ITEM_STAT, NKMEquipItemData cNKMEquipItemDataTarget)
		{
			string statShortString = NKCUtilString.GetStatShortString(cEQUIP_ITEM_STAT.type, cEQUIP_ITEM_STAT.stat_value + cEQUIP_ITEM_STAT.stat_level_value * (float)preCalcEnchantLV, NKMUnitStatManager.IsPercentStat(cEQUIP_ITEM_STAT.type));
			NKCUtil.SetLabelText(afterText, statShortString);
			if (preCalcEnchantLV > cNKMEquipItemDataTarget.m_EnchantLevel)
			{
				string text = afterText.text;
				if (NKMUnitStatManager.IsPercentStat(cEQUIP_ITEM_STAT.type))
				{
					NKCUtil.SetLabelText(afterText, string.Format("{0} <color=#FFDB00>({1:+#.0%;-#.0%;0%})</color>", text, cEQUIP_ITEM_STAT.stat_level_value * (float)preCalcEnchantLV - cEQUIP_ITEM_STAT.stat_level_value * (float)cNKMEquipItemDataTarget.m_EnchantLevel));
					return;
				}
				NKCUtil.SetLabelText(afterText, string.Format("{0} <color=#FFDB00>({1:+#;-#;''})</color>", text, cEQUIP_ITEM_STAT.stat_level_value * (float)preCalcEnchantLV - cEQUIP_ITEM_STAT.stat_level_value * (float)cNKMEquipItemDataTarget.m_EnchantLevel));
			}
		}

		// Token: 0x060065D5 RID: 26069 RVA: 0x002069B4 File Offset: 0x00204BB4
		public void TrySendEquipEnchant()
		{
			if (this.m_LeftEquipUID <= 0L)
			{
				return;
			}
			NKMEquipItemData itemEquip = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(this.m_LeftEquipUID);
			if (itemEquip == null)
			{
				return;
			}
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(itemEquip.m_ItemEquipID);
			if (equipTemplet != null && itemEquip.m_EnchantLevel >= NKMItemManager.GetMaxEquipEnchantLevel(equipTemplet.m_NKM_ITEM_TIER))
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_FORGE_ENCHANT_ALREADY_MAX, null, "");
				return;
			}
			if (this.m_listCurrentMaterialEquipsToEnhance.Count <= 0)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_FORGE_ENCHANT_NEED_CONSUME, null, "");
				return;
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = NKMItemManager.CanEnchantItem(NKCScenManager.GetScenManager().GetMyUserData(), itemEquip, this.m_listCurrentMaterialEquipsToEnhance);
			if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_OK)
			{
				NKMPacket_EQUIP_ITEM_ENCHANT_REQ nkmpacket_EQUIP_ITEM_ENCHANT_REQ = new NKMPacket_EQUIP_ITEM_ENCHANT_REQ();
				nkmpacket_EQUIP_ITEM_ENCHANT_REQ.equipItemUID = this.m_LeftEquipUID;
				nkmpacket_EQUIP_ITEM_ENCHANT_REQ.consumeEquipItemUIDList = new List<long>(this.m_listCurrentMaterialEquipsToEnhance);
				NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EQUIP_ITEM_ENCHANT_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
				return;
			}
			if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_CREDIT)
			{
				NKCShopManager.OpenItemLackPopup(1, this.m_needCredit);
				return;
			}
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCPacketHandlers.GetErrorMessage(nkm_ERROR_CODE), null, "");
		}

		// Token: 0x060065D6 RID: 26070 RVA: 0x00206AC0 File Offset: 0x00204CC0
		public void ResetMaterialEquipSlotsToEnhanceUI()
		{
			if (this.m_listNKCUIEnhanceSlot != null)
			{
				for (int i = 0; i < this.m_listNKCUIEnhanceSlot.Count; i++)
				{
					if (this.m_listNKCUIEnhanceSlot[i] != null)
					{
						if (i < this.m_listCurrentMaterialEquipsToEnhance.Count)
						{
							NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeEquipData(NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(this.m_listCurrentMaterialEquipsToEnhance[i]));
							this.m_listNKCUIEnhanceSlot[i].SetData(data, false, true, true, new NKCUISlot.OnClick(this.OnClickMaterialEquipToEnhance));
						}
						else
						{
							this.m_listNKCUIEnhanceSlot[i].SetEmptyMaterial(new NKCUISlot.OnClick(this.OnClickEmptyMaterialEquipToEnhance));
						}
					}
				}
			}
		}

		// Token: 0x060065D7 RID: 26071 RVA: 0x00206B7C File Offset: 0x00204D7C
		public void OnClickMaterialEquipToEnhance(NKCUISlot.SlotData slotData, bool bLocked)
		{
			this.RemoveCurrentMaterialEquipToEnhance(slotData.UID);
			this.ResetMaterialEquipSlotsToEnhanceUI();
			this.ResetEnhanceStatUI();
		}

		// Token: 0x060065D8 RID: 26072 RVA: 0x00206B98 File Offset: 0x00204D98
		private void OnClickEmptyMaterialEquipToEnhance(NKCUISlot.SlotData slotData, bool bLocked)
		{
			if (this.m_LeftEquipUID <= 0L)
			{
				return;
			}
			NKMEquipItemData itemEquip = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(this.m_LeftEquipUID);
			if (itemEquip == null)
			{
				return;
			}
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(itemEquip.m_ItemEquipID);
			if (equipTemplet != null && itemEquip.m_EnchantLevel >= NKMItemManager.GetMaxEquipEnchantLevel(equipTemplet.m_NKM_ITEM_TIER))
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_FORGE_ENCHANT_ALREADY_MAX, null, "");
				return;
			}
			NKCUIInventory.EquipSelectListOptions options = new NKCUIInventory.EquipSelectListOptions(NKC_INVENTORY_OPEN_TYPE.NIOT_NORMAL, false, true);
			options.m_NKC_INVENTORY_OPEN_TYPE = NKC_INVENTORY_OPEN_TYPE.NIOT_EQUIP_SELECT;
			options.m_dOnSelectedEquipSlot = null;
			options.lstSortOption = NKCEquipSortSystem.FORGE_MATERIAL_SORT_LIST;
			HashSet<long> equipsBeingUsed = NKCUtil.GetEquipsBeingUsed(NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.m_dicMyUnit, NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData);
			if (!equipsBeingUsed.Contains(this.m_LeftEquipUID))
			{
				equipsBeingUsed.Add(this.m_LeftEquipUID);
			}
			options.m_hsSelectedEquipUIDToShow = this.GetCurrentMaterialEquipsHashSetToEnhance();
			options.setExcludeEquipUID = equipsBeingUsed;
			options.bHideLockItem = true;
			options.bHideMaxLvItem = false;
			options.bLockMaxItem = false;
			options.bMultipleSelect = true;
			options.iMaxMultipleSelect = 10;
			options.m_dOnFinishMultiSelection = new NKCUIInventory.OnFinishMultiSelection(this.OnFinishMultiSelectionToEnhance);
			options.bSkipItemEquipBox = true;
			if (this.m_UIInventory == null)
			{
				this.m_UIInventory = NKCUIInventory.OpenNewInstance();
			}
			options.strEmptyMessage = NKCUtilString.GET_STRING_FORGE_ENCHANT_NO_EXIST_CONSUME;
			options.m_ButtonMenuType = NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_OK;
			NKCUIInventory uiinventory = this.m_UIInventory;
			if (uiinventory == null)
			{
				return;
			}
			uiinventory.Open(options, null, 0L, NKCUIInventory.NKC_INVENTORY_TAB.NIT_NONE);
		}

		// Token: 0x060065D9 RID: 26073 RVA: 0x00206D0C File Offset: 0x00204F0C
		private HashSet<long> GetCurrentMaterialEquipsHashSetToEnhance()
		{
			return new HashSet<long>(this.m_listCurrentMaterialEquipsToEnhance);
		}

		// Token: 0x060065DA RID: 26074 RVA: 0x00206D19 File Offset: 0x00204F19
		public void Close()
		{
			this.m_LeftEquipUID = 0L;
			this.ClearAllUI();
			this.OnCloseInstance();
		}

		// Token: 0x060065DB RID: 26075 RVA: 0x00206D2F File Offset: 0x00204F2F
		public void OnCloseInstance()
		{
			if (this.m_UIInventory != null && this.m_UIInventory.IsOpen)
			{
				this.m_UIInventory.Close();
			}
			this.m_UIInventory = null;
		}

		// Token: 0x060065DC RID: 26076 RVA: 0x00206D60 File Offset: 0x00204F60
		public void ClearAllUI()
		{
			this.m_NKM_UI_FACTORY_ENCHANT_EXP_SLIDER_NEW_Slider.value = 0f;
			this.m_NKM_UI_FACTORY_ENCHANT_EXP_SLIDER_Slider.value = 0f;
			NKCUtil.SetLabelText(this.m_ENCHANT_BEFORE_TEXT, string.Format(NKCUtilString.GET_STRING_FORGE_ENCHANT_LEVEL_ONE_PARAM, 0));
			NKCUtil.SetLabelText(this.m_NKM_UI_FACTORY_ENCHANT_EXP_BG_TEXT, "");
			this.m_ENCHANT_ARROW.SetActive(false);
			NKCUtil.SetLabelText(this.m_ENCHANT_AFTER_TEXT, "");
			this.m_creditSlot.SetData(0, 0, 0L, true, true, false);
			NKCUtil.SetLabelText(this.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_01_Before, "0");
			NKCUtil.SetLabelText(this.m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_01_After, "0");
			foreach (NKCUISlot nkcuislot in this.m_listNKCUIEnhanceSlot)
			{
				nkcuislot.SetEmptyMaterial(null);
			}
			this.EnableUI(false);
		}

		// Token: 0x060065DD RID: 26077 RVA: 0x00206E54 File Offset: 0x00205054
		private void EnableUI(bool bActive)
		{
			if (!bActive)
			{
				this.m_Img_NKM_UI_FACTORY_ENCHANT_BUTTON_RESET.sprite = NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY);
				this.m_Img_NKM_UI_FACTORY_ENCHANT_BUTTON_ENCHANT.sprite = NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY);
			}
			else
			{
				this.m_Img_NKM_UI_FACTORY_ENCHANT_BUTTON_RESET.sprite = NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_BLUE);
				this.m_Img_NKM_UI_FACTORY_ENCHANT_BUTTON_ENCHANT.sprite = NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_YELLOW);
			}
			Color col = bActive ? NKCUtil.GetColor("#FFFFFF") : NKCUtil.GetColor("#222222");
			NKCUtil.SetLabelTextColor(this.m_txt_NKM_UI_FACTORY_ENCHANT_RESET_TEXT, col);
			this.m_img_NKM_UI_FACTORY_ENCHANT_ENCHANT_ICON.color = NKCUtil.GetUITextColor(bActive);
			this.m_txt_NKM_UI_FACTORY_ENCHANT_ENCHANT_TEXT.color = NKCUtil.GetUITextColor(bActive);
			this.m_NKM_UI_FACTORY_ENCHANT_BUTTON_ENCHANT_LIGHT.SetActive(bActive);
			NKCUtil.SetGameobjectActive(this.m_btn_NKM_UI_FACTORY_ENCHANT_AUTOSELECT.gameObject, bActive);
		}

		// Token: 0x04005149 RID: 20809
		private long m_LeftEquipUID;

		// Token: 0x0400514A RID: 20810
		private int m_needCredit;

		// Token: 0x0400514B RID: 20811
		public NKCUIRectMove m_rmEnchantRoot;

		// Token: 0x0400514C RID: 20812
		public List<NKCUISlot> m_listNKCUIEnhanceSlot;

		// Token: 0x0400514D RID: 20813
		public List<GameObject> m_listEffect;

		// Token: 0x0400514E RID: 20814
		private List<long> m_listCurrentMaterialEquipsToEnhance = new List<long>();

		// Token: 0x0400514F RID: 20815
		public Text m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_01_Before;

		// Token: 0x04005150 RID: 20816
		public Text m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_02_Before;

		// Token: 0x04005151 RID: 20817
		public Text m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_01_After;

		// Token: 0x04005152 RID: 20818
		public Text m_NKM_UI_ITEM_EQUIP_SLOT_ITEM_STAT_TEXT_02_After;

		// Token: 0x04005153 RID: 20819
		public Text m_ENCHANT_BEFORE_TEXT;

		// Token: 0x04005154 RID: 20820
		public GameObject m_ENCHANT_ARROW;

		// Token: 0x04005155 RID: 20821
		public Text m_ENCHANT_AFTER_TEXT;

		// Token: 0x04005156 RID: 20822
		public Text m_NKM_UI_FACTORY_ENCHANT_EXP_BG_TEXT;

		// Token: 0x04005157 RID: 20823
		public GameObject m_NKM_UI_FACTORY_ENCHANT_EXP_SLIDER;

		// Token: 0x04005158 RID: 20824
		public GameObject m_NKM_UI_FACTORY_ENCHANT_EXP_SLIDER_NEW;

		// Token: 0x04005159 RID: 20825
		public Slider m_NKM_UI_FACTORY_ENCHANT_EXP_SLIDER_Slider;

		// Token: 0x0400515A RID: 20826
		public Slider m_NKM_UI_FACTORY_ENCHANT_EXP_SLIDER_NEW_Slider;

		// Token: 0x0400515B RID: 20827
		public Text m_txt_NKM_UI_FACTORY_ENCHANT_RESET_TEXT;

		// Token: 0x0400515C RID: 20828
		public Image m_img_NKM_UI_FACTORY_ENCHANT_ENCHANT_ICON;

		// Token: 0x0400515D RID: 20829
		public Text m_txt_NKM_UI_FACTORY_ENCHANT_ENCHANT_TEXT;

		// Token: 0x0400515E RID: 20830
		public NKCUIItemCostSlot m_creditSlot;

		// Token: 0x0400515F RID: 20831
		private NKCUIInventory m_UIInventory;

		// Token: 0x04005160 RID: 20832
		[Header("UI")]
		public Image m_Img_NKM_UI_FACTORY_ENCHANT_BUTTON_RESET;

		// Token: 0x04005161 RID: 20833
		public Image m_Img_NKM_UI_FACTORY_ENCHANT_BUTTON_ENCHANT;

		// Token: 0x04005162 RID: 20834
		public GameObject m_NKM_UI_FACTORY_ENCHANT_BUTTON_ENCHANT_LIGHT;

		// Token: 0x04005163 RID: 20835
		public NKCUIComStateButton m_btn_NKM_UI_FACTORY_ENCHANT_AUTOSELECT;

		// Token: 0x04005164 RID: 20836
		public NKCUIComButton m_btn_NKM_UI_FACTORY_ENCHANT_BUTTON_RESET;

		// Token: 0x04005165 RID: 20837
		public NKCUIComButton m_btnNKM_UI_FACTORY_ENCHANT_BUTTON_ENCHANT;

		// Token: 0x02001663 RID: 5731
		public class CandidateMaterial
		{
			// Token: 0x0400A434 RID: 42036
			public NKMEquipTemplet m_NKMEquipTemplet;

			// Token: 0x0400A435 RID: 42037
			public NKMEquipItemData m_NKMEquipItemData;
		}

		// Token: 0x02001664 RID: 5732
		public class CompForAutoSelect : IComparer<NKCUIForgeEnchant.CandidateMaterial>
		{
			// Token: 0x0600B01E RID: 45086 RVA: 0x0035E7C8 File Offset: 0x0035C9C8
			public int Compare(NKCUIForgeEnchant.CandidateMaterial x, NKCUIForgeEnchant.CandidateMaterial y)
			{
				if (x.m_NKMEquipTemplet.m_ItemEquipPosition == ITEM_EQUIP_POSITION.IEP_ENCHANT && y.m_NKMEquipTemplet.m_ItemEquipPosition != ITEM_EQUIP_POSITION.IEP_ENCHANT)
				{
					return -1;
				}
				if (x.m_NKMEquipTemplet.m_ItemEquipPosition != ITEM_EQUIP_POSITION.IEP_ENCHANT && y.m_NKMEquipTemplet.m_ItemEquipPosition == ITEM_EQUIP_POSITION.IEP_ENCHANT)
				{
					return 1;
				}
				if (x.m_NKMEquipTemplet.m_ItemEquipPosition == ITEM_EQUIP_POSITION.IEP_ENCHANT && y.m_NKMEquipTemplet.m_ItemEquipPosition == ITEM_EQUIP_POSITION.IEP_ENCHANT)
				{
					if (x.m_NKMEquipTemplet.m_NKM_ITEM_TIER < y.m_NKMEquipTemplet.m_NKM_ITEM_TIER)
					{
						return 1;
					}
					if (x.m_NKMEquipTemplet.m_NKM_ITEM_TIER > y.m_NKMEquipTemplet.m_NKM_ITEM_TIER)
					{
						return -1;
					}
					if (x.m_NKMEquipTemplet.m_NKM_ITEM_GRADE < y.m_NKMEquipTemplet.m_NKM_ITEM_GRADE)
					{
						return 1;
					}
					if (x.m_NKMEquipTemplet.m_NKM_ITEM_GRADE > y.m_NKMEquipTemplet.m_NKM_ITEM_GRADE)
					{
						return -1;
					}
					if (x.m_NKMEquipItemData.m_ItemUid < y.m_NKMEquipItemData.m_ItemUid)
					{
						return -1;
					}
					if (x.m_NKMEquipItemData.m_ItemUid > y.m_NKMEquipItemData.m_ItemUid)
					{
						return 1;
					}
					return 0;
				}
				else
				{
					if (x.m_NKMEquipTemplet.m_NKM_ITEM_TIER < y.m_NKMEquipTemplet.m_NKM_ITEM_TIER)
					{
						return -1;
					}
					if (x.m_NKMEquipTemplet.m_NKM_ITEM_TIER > y.m_NKMEquipTemplet.m_NKM_ITEM_TIER)
					{
						return 1;
					}
					if (x.m_NKMEquipTemplet.m_NKM_ITEM_GRADE < y.m_NKMEquipTemplet.m_NKM_ITEM_GRADE)
					{
						return -1;
					}
					if (x.m_NKMEquipTemplet.m_NKM_ITEM_GRADE > y.m_NKMEquipTemplet.m_NKM_ITEM_GRADE)
					{
						return 1;
					}
					if (x.m_NKMEquipItemData.m_ItemUid < y.m_NKMEquipItemData.m_ItemUid)
					{
						return -1;
					}
					if (x.m_NKMEquipItemData.m_ItemUid > y.m_NKMEquipItemData.m_ItemUid)
					{
						return 1;
					}
					return 0;
				}
			}
		}
	}
}
