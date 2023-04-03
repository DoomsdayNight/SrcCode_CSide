using System;
using System.Collections;
using ClientPacket.Common;
using ClientPacket.Item;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000998 RID: 2456
	public class NKCUIForgeTuning : MonoBehaviour
	{
		// Token: 0x060065F4 RID: 26100 RVA: 0x002078F8 File Offset: 0x00205AF8
		public void InitUI(NKCUIForgeTuning.OnSelectSlot selectSlotIdx = null)
		{
			NKCUIComButton btnNKM_UI_FACTORY_TUNING_BUTTON_PRECISION = this.m_btnNKM_UI_FACTORY_TUNING_BUTTON_PRECISION;
			if (btnNKM_UI_FACTORY_TUNING_BUTTON_PRECISION != null)
			{
				btnNKM_UI_FACTORY_TUNING_BUTTON_PRECISION.PointerClick.RemoveAllListeners();
			}
			NKCUIComButton btnNKM_UI_FACTORY_TUNING_BUTTON_PRECISION2 = this.m_btnNKM_UI_FACTORY_TUNING_BUTTON_PRECISION;
			if (btnNKM_UI_FACTORY_TUNING_BUTTON_PRECISION2 != null)
			{
				btnNKM_UI_FACTORY_TUNING_BUTTON_PRECISION2.PointerClick.AddListener(new UnityAction(this.OnClickRefine));
			}
			NKCUIComButton btnNKM_UI_FACTORY_TUNING_BUTTON_CHANGE = this.m_btnNKM_UI_FACTORY_TUNING_BUTTON_CHANGE;
			if (btnNKM_UI_FACTORY_TUNING_BUTTON_CHANGE != null)
			{
				btnNKM_UI_FACTORY_TUNING_BUTTON_CHANGE.PointerClick.RemoveAllListeners();
			}
			NKCUIComButton btnNKM_UI_FACTORY_TUNING_BUTTON_CHANGE2 = this.m_btnNKM_UI_FACTORY_TUNING_BUTTON_CHANGE;
			if (btnNKM_UI_FACTORY_TUNING_BUTTON_CHANGE2 != null)
			{
				btnNKM_UI_FACTORY_TUNING_BUTTON_CHANGE2.PointerClick.AddListener(new UnityAction(this.OnClickOptionChange));
			}
			NKCUIComButton btnNKM_UI_FACTORY_TUNING_BUTTON_OK = this.m_btnNKM_UI_FACTORY_TUNING_BUTTON_OK;
			if (btnNKM_UI_FACTORY_TUNING_BUTTON_OK != null)
			{
				btnNKM_UI_FACTORY_TUNING_BUTTON_OK.PointerClick.RemoveAllListeners();
			}
			NKCUIComButton btnNKM_UI_FACTORY_TUNING_BUTTON_OK2 = this.m_btnNKM_UI_FACTORY_TUNING_BUTTON_OK;
			if (btnNKM_UI_FACTORY_TUNING_BUTTON_OK2 != null)
			{
				btnNKM_UI_FACTORY_TUNING_BUTTON_OK2.PointerClick.AddListener(new UnityAction(this.OnClickOptionConfirm));
			}
			NKCUIComToggle refineToggleBtn = this.m_RefineToggleBtn;
			if (refineToggleBtn != null)
			{
				refineToggleBtn.OnValueChanged.RemoveAllListeners();
			}
			NKCUIComToggle refineToggleBtn2 = this.m_RefineToggleBtn;
			if (refineToggleBtn2 != null)
			{
				refineToggleBtn2.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickPrecision));
			}
			NKCUIComToggle changeToggleBtn = this.m_ChangeToggleBtn;
			if (changeToggleBtn != null)
			{
				changeToggleBtn.OnValueChanged.RemoveAllListeners();
			}
			NKCUIComToggle changeToggleBtn2 = this.m_ChangeToggleBtn;
			if (changeToggleBtn2 != null)
			{
				changeToggleBtn2.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickOptionChange));
			}
			NKCUIComToggle setOptionChangeToggleBtn = this.m_SetOptionChangeToggleBtn;
			if (setOptionChangeToggleBtn != null)
			{
				setOptionChangeToggleBtn.OnValueChanged.RemoveAllListeners();
			}
			NKCUIComToggle setOptionChangeToggleBtn2 = this.m_SetOptionChangeToggleBtn;
			if (setOptionChangeToggleBtn2 != null)
			{
				setOptionChangeToggleBtn2.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickSetOptionChange));
			}
			NKCUIComToggle firstOptionBtn = this.m_firstOptionBtn;
			if (firstOptionBtn != null)
			{
				firstOptionBtn.OnValueChanged.RemoveAllListeners();
			}
			NKCUIComToggle firstOptionBtn2 = this.m_firstOptionBtn;
			if (firstOptionBtn2 != null)
			{
				firstOptionBtn2.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickFirstOption));
			}
			NKCUIComToggle secondOptionBtn = this.m_secondOptionBtn;
			if (secondOptionBtn != null)
			{
				secondOptionBtn.OnValueChanged.RemoveAllListeners();
			}
			NKCUIComToggle secondOptionBtn2 = this.m_secondOptionBtn;
			if (secondOptionBtn2 != null)
			{
				secondOptionBtn2.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickSecondOption));
			}
			NKCUtil.SetBindFunction(this.m_NKM_UI_FACTORY_TUNING_OPTION_CHANGE_LIST_BUTTON, new UnityAction(this.OpenOptionList));
			NKCUtil.SetBindFunction(this.m_NKM_UI_FACTORY_TUNING_SET_OPTION_CHANGE_BUTTON, new UnityAction(this.OpenSetOptionList));
			NKCUtil.SetBindFunction(this.m_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_FREE, delegate()
			{
				NKCPacketSender.Send_NKMPacket_EQUIP_ITEM_FIRST_SET_OPTION_REQ(this.m_LeftEquipUID);
			});
			NKCUIItemCostSlot materialSlot = this.m_MaterialSlot1;
			if (materialSlot != null)
			{
				materialSlot.SetData(0, 0, 0L, true, true, false);
			}
			NKCUIItemCostSlot materialSlot2 = this.m_MaterialSlot2;
			if (materialSlot2 != null)
			{
				materialSlot2.SetData(0, 0, 0L, true, true, false);
			}
			this.SetActiveEffect(false);
			this.dOnSelectSlot = selectSlotIdx;
		}

		// Token: 0x060065F5 RID: 26101 RVA: 0x00207B48 File Offset: 0x00205D48
		public void SetActiveEffect(bool bActive)
		{
			NKCUtil.SetGameobjectActive(this.m_AB_FX_UI_FACTORY_EQUIP_SLOT.gameObject, bActive);
			NKCUtil.SetGameobjectActive(this.m_AB_FX_UI_FACTORY_SMALL_SLOT_OPTION_BEFORE.gameObject, bActive);
			NKCUtil.SetGameobjectActive(this.m_AB_FX_UI_FACTORY_SMALL_SLOT_OPTION_AFTER.gameObject, bActive);
			NKCUtil.SetGameobjectActive(this.m_AB_FX_UI_FACTORY_SMALL_SLOT_AFTER.gameObject, bActive);
		}

		// Token: 0x060065F6 RID: 26102 RVA: 0x00207B99 File Offset: 0x00205D99
		public void SetLeftEquipUID(long uid)
		{
			this.m_LeftEquipUID = uid;
			this.EnableUI(this.m_LeftEquipUID != 0L);
			this.SetTuningRequirementResourceCount();
		}

		// Token: 0x060065F7 RID: 26103 RVA: 0x00207BB8 File Offset: 0x00205DB8
		private void SetTuningRequirementResourceCount()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMEquipItemData itemEquip = myUserData.m_InventoryData.GetItemEquip(this.m_LeftEquipUID);
			if (itemEquip == null)
			{
				return;
			}
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(itemEquip.m_ItemEquipID);
			if (equipTemplet == null)
			{
				return;
			}
			this.m_PrecisionReqCredit = equipTemplet.m_PrecisionReqResource;
			this.m_PrecisionReqMaterial = equipTemplet.m_PrecisionReqItem;
			this.m_RandomStatReqCredit = equipTemplet.m_RandomStatReqResource;
			this.m_RandomStatReqMaterial = equipTemplet.m_RandomStatReqItem;
			if (NKCCompanyBuff.NeedShowEventMark(myUserData.m_companyBuffDataList, NKMConst.Buff.BuffType.BASE_FACTORY_ENCHANT_TUNING_CREDIT_DISCOUNT))
			{
				NKCCompanyBuff.SetDiscountOfCreditInEnchantTuning(myUserData.m_companyBuffDataList, ref this.m_PrecisionReqCredit);
				NKCCompanyBuff.SetDiscountOfCreditInEnchantTuning(myUserData.m_companyBuffDataList, ref this.m_RandomStatReqCredit);
			}
		}

		// Token: 0x060065F8 RID: 26104 RVA: 0x00207C57 File Offset: 0x00205E57
		public void SetOut()
		{
			this.m_rmTuningRoot.Set("Out");
			this.SetActiveEffect(false);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060065F9 RID: 26105 RVA: 0x00207C7C File Offset: 0x00205E7C
		public void AnimateOutToIn()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_rmTuningRoot.Set("Out");
			this.m_rmTuningRoot.Transit("In", null);
		}

		// Token: 0x060065FA RID: 26106 RVA: 0x00207CAC File Offset: 0x00205EAC
		public void ResetUI(bool bForce = true, bool bMoveToTabBeingTuned = false)
		{
			if (bMoveToTabBeingTuned)
			{
				NKMEquipTuningCandidate tuiningData = NKCScenManager.CurrentUserData().GetTuiningData();
				if (tuiningData != null)
				{
					if (tuiningData.option1 != NKM_STAT_TYPE.NST_RANDOM || tuiningData.option2 != NKM_STAT_TYPE.NST_RANDOM)
					{
						this.m_ChangeToggleBtn.Select(true, false, false);
						this.m_NKC_TUNING_TAB = NKCUIForgeTuning.NKC_TUNING_TAB.NTT_OPTION_CHANGE;
						this.m_iSelectOptionIdx = ((tuiningData.option1 != NKM_STAT_TYPE.NST_RANDOM) ? 1 : 2);
					}
					else if (tuiningData.setOptionId != 0)
					{
						this.m_NKC_TUNING_TAB = NKCUIForgeTuning.NKC_TUNING_TAB.NTT_SET_OPTION_CHANGE;
						this.m_SetOptionChangeToggleBtn.Select(true, false, false);
					}
				}
			}
			this.SetActiveEffect(false);
			this.SetTab(this.m_NKC_TUNING_TAB, bForce);
			this.m_firstOptionBtn.Select(this.m_iSelectOptionIdx == 1, false, false);
			this.m_secondOptionBtn.Select(this.m_iSelectOptionIdx == 2, false, false);
			this.dOnSelectSlot(this.m_iSelectOptionIdx);
		}

		// Token: 0x060065FB RID: 26107 RVA: 0x00207D78 File Offset: 0x00205F78
		public int GetSelectOption()
		{
			return this.m_iSelectOptionIdx;
		}

		// Token: 0x060065FC RID: 26108 RVA: 0x00207D80 File Offset: 0x00205F80
		public void OnClickPrecision(bool bSet)
		{
			if (bSet)
			{
				this.SetTab(NKCUIForgeTuning.NKC_TUNING_TAB.NTT_PRECISION, true);
			}
		}

		// Token: 0x060065FD RID: 26109 RVA: 0x00207D90 File Offset: 0x00205F90
		public void OnClickOptionChange(bool bSet)
		{
			if (bSet)
			{
				if (NKCScenManager.GetScenManager().GetMyUserData().hasReservedSetOptionData())
				{
					NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_FORGE_SET_OPTION_TUNING_EXIT_CONFIRM, delegate()
					{
						NKCPacketSender.Send_NKMPacket_Equip_Tuning_Cancel_REQ();
						this.SetTab(NKCUIForgeTuning.NKC_TUNING_TAB.NTT_OPTION_CHANGE, true);
					}, delegate()
					{
						this.SetTab(this.m_NKC_TUNING_TAB, true);
					}, false);
					return;
				}
				this.SetTab(NKCUIForgeTuning.NKC_TUNING_TAB.NTT_OPTION_CHANGE, true);
			}
		}

		// Token: 0x060065FE RID: 26110 RVA: 0x00207DE4 File Offset: 0x00205FE4
		public void OnClickSetOptionChange(bool bSet)
		{
			if (bSet)
			{
				if (NKCScenManager.GetScenManager().GetMyUserData().hasReservedEquipTuningData())
				{
					NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_FORGE_TUNING_EXIT_CONFIRM, delegate()
					{
						NKCPacketSender.Send_NKMPacket_Equip_Tuning_Cancel_REQ();
						this.SetTab(NKCUIForgeTuning.NKC_TUNING_TAB.NTT_SET_OPTION_CHANGE, true);
					}, delegate()
					{
						this.SetTab(this.m_NKC_TUNING_TAB, true);
					}, false);
					return;
				}
				this.SetTab(NKCUIForgeTuning.NKC_TUNING_TAB.NTT_SET_OPTION_CHANGE, true);
			}
		}

		// Token: 0x060065FF RID: 26111 RVA: 0x00207E36 File Offset: 0x00206036
		public void OnClickFirstOption(bool bSet)
		{
			if (bSet)
			{
				this.SelectTuningOption(1);
			}
		}

		// Token: 0x06006600 RID: 26112 RVA: 0x00207E42 File Offset: 0x00206042
		public void OnClickSecondOption(bool bSet)
		{
			if (bSet)
			{
				this.SelectTuningOption(2);
			}
		}

		// Token: 0x06006601 RID: 26113 RVA: 0x00207E50 File Offset: 0x00206050
		private void SelectTuningOption(int idx)
		{
			if (this.m_LeftEquipUID == 0L)
			{
				this.m_NKM_UI_FACTORY_TUNING_OPTION_SLOT_toggle.SetAllToggleUnselected();
				return;
			}
			if (this.m_iSelectOptionIdx == idx)
			{
				return;
			}
			this.m_iSelectOptionIdx = idx;
			this.dOnSelectSlot(this.m_iSelectOptionIdx);
			this.SetTab(this.m_NKC_TUNING_TAB, false);
		}

		// Token: 0x06006602 RID: 26114 RVA: 0x00207EA0 File Offset: 0x002060A0
		public void OnClickRefine()
		{
			int itemID;
			int itemCnt;
			if (!this.CanTuning(this.m_PrecisionReqCredit, this.m_PrecisionReqMaterial, out itemID, out itemCnt))
			{
				NKCShopManager.OpenItemLackPopup(itemID, itemCnt);
				return;
			}
			NKMPacket_EQUIP_TUNING_REFINE_REQ nkmpacket_EQUIP_TUNING_REFINE_REQ = new NKMPacket_EQUIP_TUNING_REFINE_REQ();
			nkmpacket_EQUIP_TUNING_REFINE_REQ.equipOptionID = this.m_iSelectOptionIdx;
			nkmpacket_EQUIP_TUNING_REFINE_REQ.equipUID = this.m_LeftEquipUID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EQUIP_TUNING_REFINE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06006603 RID: 26115 RVA: 0x00207F00 File Offset: 0x00206100
		public void OnClickOptionChange()
		{
			int itemID;
			int itemCnt;
			if (!this.CanTuning(this.m_RandomStatReqCredit, this.m_RandomStatReqMaterial, out itemID, out itemCnt))
			{
				NKCShopManager.OpenItemLackPopup(itemID, itemCnt);
				return;
			}
			NKMEquipItemData itemEquip = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(this.m_LeftEquipUID);
			if (itemEquip != null && NKCScenManager.CurrentUserData().hasReservedEquipCandidate())
			{
				NKM_ERROR_CODE nkm_ERROR_CODE = NKMItemManager.CanEnchantItem(NKCScenManager.CurrentUserData(), itemEquip);
				if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
				{
					this.GoToHomeAfterImpossibleTuningPopupMsgBox(nkm_ERROR_CODE);
					return;
				}
			}
			NKMPacket_EQUIP_TUNING_STAT_CHANGE_REQ nkmpacket_EQUIP_TUNING_STAT_CHANGE_REQ = new NKMPacket_EQUIP_TUNING_STAT_CHANGE_REQ();
			nkmpacket_EQUIP_TUNING_STAT_CHANGE_REQ.equipOptionID = this.m_iSelectOptionIdx;
			nkmpacket_EQUIP_TUNING_STAT_CHANGE_REQ.equipUID = this.m_LeftEquipUID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EQUIP_TUNING_STAT_CHANGE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06006604 RID: 26116 RVA: 0x00207FA4 File Offset: 0x002061A4
		private void GoToHomeAfterImpossibleTuningPopupMsgBox(NKM_ERROR_CODE error)
		{
			string content;
			if (error == NKM_ERROR_CODE.NEC_FAIL_WARFARE_DOING)
			{
				content = NKCUtilString.GET_STRING_IMPOSSIBLE_TUNING_BY_WARFARE;
			}
			else
			{
				if (error != NKM_ERROR_CODE.NEC_FAIL_DIVE_DOING)
				{
					return;
				}
				content = NKCUtilString.GET_STRING_IMPOSSIBLE_TUNING_BY_DIVE;
			}
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, content, delegate()
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
			}, "");
		}

		// Token: 0x06006605 RID: 26117 RVA: 0x00208008 File Offset: 0x00206208
		private bool CanTuning(int creditCost, int itemCost, out int needItemID, out int needCount)
		{
			needItemID = 0;
			needCount = 0;
			long num = 0L;
			NKMItemMiscData itemMisc = NKCScenManager.CurrentUserData().m_InventoryData.GetItemMisc(1013);
			if (itemMisc != null)
			{
				num = itemMisc.TotalCount;
			}
			if (num < (long)itemCost)
			{
				needItemID = 1013;
				needCount = itemCost;
				return false;
			}
			if (NKCScenManager.CurrentUserData().GetCredit() < (long)creditCost)
			{
				needItemID = 1;
				needCount = creditCost;
				return false;
			}
			return true;
		}

		// Token: 0x06006606 RID: 26118 RVA: 0x00208068 File Offset: 0x00206268
		private void SendOptionConfirmPacket()
		{
			NKMEquipItemData itemEquip = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(this.m_LeftEquipUID);
			if (itemEquip != null && NKCScenManager.CurrentUserData().hasReservedEquipCandidate())
			{
				NKM_ERROR_CODE nkm_ERROR_CODE = NKMItemManager.CanEnchantItem(NKCScenManager.CurrentUserData(), itemEquip);
				if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
				{
					this.GoToHomeAfterImpossibleTuningPopupMsgBox(nkm_ERROR_CODE);
					return;
				}
			}
			NKMPacket_EQUIP_TUNING_STAT_CHANGE_CONFIRM_REQ nkmpacket_EQUIP_TUNING_STAT_CHANGE_CONFIRM_REQ = new NKMPacket_EQUIP_TUNING_STAT_CHANGE_CONFIRM_REQ();
			nkmpacket_EQUIP_TUNING_STAT_CHANGE_CONFIRM_REQ.equipOptionID = this.m_iSelectOptionIdx;
			nkmpacket_EQUIP_TUNING_STAT_CHANGE_CONFIRM_REQ.equipUID = this.m_LeftEquipUID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EQUIP_TUNING_STAT_CHANGE_CONFIRM_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06006607 RID: 26119 RVA: 0x002080E8 File Offset: 0x002062E8
		public void OnClickOptionConfirm()
		{
			if (!this.IsOptionChangePossible())
			{
				return;
			}
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_FORGE_TUNING_CONFIRM_TITLE, string.Format(NKCUtilString.GET_STRING_FORGE_TUNING_CONFIRM_DESC_TWO_PARAM, this.GetTuningOptionText(this.m_iSelectOptionIdx - 1, true), this.GetTuningOptionText(this.m_iSelectOptionIdx - 1, false)), new NKCPopupOKCancel.OnButton(this.SendOptionConfirmPacket), null, false);
		}

		// Token: 0x06006608 RID: 26120 RVA: 0x0020813E File Offset: 0x0020633E
		private bool IsOptionChangePossible()
		{
			return NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(this.m_LeftEquipUID) != null && NKCScenManager.GetScenManager().GetMyUserData().IsPossibleTuning(this.m_LeftEquipUID, this.m_iSelectOptionIdx - 1);
		}

		// Token: 0x06006609 RID: 26121 RVA: 0x0020817B File Offset: 0x0020637B
		private string GetTuningOptionText(int idx, bool Before = false)
		{
			if (this.m_NKM_UI_FACTORY_TUNING_OPTION_SLOT.Length > idx)
			{
				return this.m_NKM_UI_FACTORY_TUNING_OPTION_SLOT[idx].GetStatText(Before);
			}
			return "";
		}

		// Token: 0x0600660A RID: 26122 RVA: 0x0020819C File Offset: 0x0020639C
		public NKCUIForgeTuning.NKC_TUNING_TAB GetCurTuningTab()
		{
			return this.m_NKC_TUNING_TAB;
		}

		// Token: 0x0600660B RID: 26123 RVA: 0x002081A4 File Offset: 0x002063A4
		public void SetTab(NKCUIForgeTuning.NKC_TUNING_TAB eNKC_TUNING_TAB, bool bForce = false)
		{
			if (bForce)
			{
				this.m_RefineToggleBtn.Select(eNKC_TUNING_TAB == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_PRECISION, true, false);
				this.m_ChangeToggleBtn.Select(eNKC_TUNING_TAB == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_OPTION_CHANGE, true, false);
				this.m_SetOptionChangeToggleBtn.Select(eNKC_TUNING_TAB == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_SET_OPTION_CHANGE, true, false);
			}
			NKMEquipItemData itemEquip = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(this.m_LeftEquipUID);
			this.m_NKC_TUNING_TAB = eNKC_TUNING_TAB;
			NKCUIManager.UpdateUpsideMenu();
			this.UpdateSubUI();
			if (itemEquip != null && this.m_NKC_TUNING_TAB != NKCUIForgeTuning.NKC_TUNING_TAB.NTT_SET_OPTION_CHANGE)
			{
				for (int i = 0; i < this.m_NKM_UI_FACTORY_TUNING_OPTION_SLOT.Length; i++)
				{
					NKCUIForgeTuningOptionSlot nkcuiforgeTuningOptionSlot = this.m_NKM_UI_FACTORY_TUNING_OPTION_SLOT[i];
					if (nkcuiforgeTuningOptionSlot != null)
					{
						nkcuiforgeTuningOptionSlot.SetData(this.m_NKC_TUNING_TAB, i, itemEquip);
					}
				}
				this.UpdateOptionSlotStat(bForce);
			}
			this.m_MaterialSlot1.SetData(0, 0, 0L, true, true, false);
			this.m_MaterialSlot2.SetData(0, 0, 0L, true, true, false);
			this.UpdateRequireItemUI();
		}

		// Token: 0x0600660C RID: 26124 RVA: 0x00208284 File Offset: 0x00206484
		private void UpdateSubUI()
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_TUNING_OPTION_SLOT_toggle.gameObject, this.m_NKC_TUNING_TAB != NKCUIForgeTuning.NKC_TUNING_TAB.NTT_SET_OPTION_CHANGE);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_TUNING_PRECISION_panel, this.m_NKC_TUNING_TAB == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_PRECISION);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_TUNING_OPTION_CHANGE_panel, this.m_NKC_TUNING_TAB == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_OPTION_CHANGE);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_TUNING_SET_CHANGE_panel, this.m_NKC_TUNING_TAB == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_SET_OPTION_CHANGE);
			if (this.m_LeftEquipUID == 0L)
			{
				this.ClearAllUI();
				return;
			}
			if (this.m_NKC_TUNING_TAB == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_OPTION_CHANGE)
			{
				NKCUtil.SetBindFunction(this.m_NKM_UI_FACTORY_TUNING_OPTION_CHANGE_LIST_BUTTON, new UnityAction(this.OpenOptionList));
				return;
			}
			if (this.m_NKC_TUNING_TAB == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_SET_OPTION_CHANGE)
			{
				bool flag = false;
				NKMEquipItemData itemEquip = NKCScenManager.CurrentUserData().m_InventoryData.GetItemEquip(this.m_LeftEquipUID);
				if (itemEquip != null)
				{
					NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(itemEquip.m_ItemEquipID);
					if (equipTemplet != null && equipTemplet.SetGroupList != null && equipTemplet.SetGroupList.Count > 1)
					{
						flag = true;
					}
				}
				if (flag)
				{
					NKCUtil.SetBindFunction(this.m_NKM_UI_FACTORY_TUNING_SET_OPTION_CHANGE_BUTTON, new UnityAction(this.OpenSetOptionList));
					NKCUtil.SetBindFunction(this.m_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_OK, new UnityAction(this.OnSetOptionConfirm));
					NKCUtil.SetBindFunction(this.m_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_CHANGE, new UnityAction(this.OnSetOptionChange));
					NKCUtil.SetImageSprite(this.m_img_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_CHANGE, NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_BLUE), false);
					NKCUtil.SetLabelTextColor(this.m_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_CHANGE_TEXT, Color.white);
					return;
				}
				NKCUtil.SetBindFunction(this.m_NKM_UI_FACTORY_TUNING_SET_OPTION_CHANGE_BUTTON, null);
				NKCUtil.SetBindFunction(this.m_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_OK, null);
				NKCUtil.SetBindFunction(this.m_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_CHANGE, null);
				NKCUtil.SetImageSprite(this.m_img_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_CHANGE, NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY), false);
				NKCUtil.SetLabelTextColor(this.m_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_CHANGE_TEXT, NKCUtil.GetUITextColor(false));
			}
		}

		// Token: 0x0600660D RID: 26125 RVA: 0x0020841C File Offset: 0x0020661C
		private void OnSetOptionChange()
		{
			bool flag = true;
			int num = 0;
			int num2 = 0;
			NKMInventoryData inventoryData = NKCScenManager.CurrentUserData().m_InventoryData;
			NKMEquipItemData itemEquip = inventoryData.GetItemEquip(this.m_LeftEquipUID);
			if (itemEquip != null)
			{
				if (NKCScenManager.CurrentUserData().hasReservedEquipCandidate())
				{
					NKM_ERROR_CODE nkm_ERROR_CODE = NKMItemManager.CanEnchantItem(NKCScenManager.CurrentUserData(), itemEquip);
					if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
					{
						this.GoToHomeAfterImpossibleTuningPopupMsgBox(nkm_ERROR_CODE);
						return;
					}
				}
				NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(itemEquip.m_ItemEquipID);
				if (equipTemplet != null)
				{
					if ((long)equipTemplet.m_RandomSetReqItemValue > inventoryData.GetCountMiscItem(equipTemplet.m_RandomSetReqItemID))
					{
						num = equipTemplet.m_RandomSetReqItemID;
						num2 = equipTemplet.m_RandomSetReqItemValue;
						flag = false;
					}
					else if ((long)equipTemplet.m_RandomSetReqResource > inventoryData.GetCountMiscItem(1))
					{
						int randomSetReqResource = equipTemplet.m_RandomSetReqResource;
						if (NKCCompanyBuff.NeedShowEventMark(NKCScenManager.CurrentUserData().m_companyBuffDataList, NKMConst.Buff.BuffType.BASE_FACTORY_ENCHANT_TUNING_CREDIT_DISCOUNT))
						{
							NKCCompanyBuff.SetDiscountOfCreditInEnchantTuning(NKCScenManager.CurrentUserData().m_companyBuffDataList, ref randomSetReqResource);
						}
						if ((long)randomSetReqResource > inventoryData.GetCountMiscItem(1))
						{
							num = 1;
							num2 = randomSetReqResource;
							flag = false;
						}
					}
				}
			}
			if (!flag && num != 0 && num2 != 0)
			{
				NKCShopManager.OpenItemLackPopup(num, num2);
				return;
			}
			NKCPacketSender.Send_NKMPacket_EQUIP_ITEM_CHANGE_SET_OPTION_REQ(this.m_LeftEquipUID);
		}

		// Token: 0x0600660E RID: 26126 RVA: 0x00208520 File Offset: 0x00206720
		private void OnSetOptionConfirm()
		{
			NKMEquipItemData itemEquip = NKCScenManager.CurrentUserData().m_InventoryData.GetItemEquip(this.m_LeftEquipUID);
			if (itemEquip != null)
			{
				if (NKCScenManager.CurrentUserData().hasReservedEquipCandidate())
				{
					NKM_ERROR_CODE nkm_ERROR_CODE = NKMItemManager.CanEnchantItem(NKCScenManager.CurrentUserData(), itemEquip);
					if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
					{
						this.GoToHomeAfterImpossibleTuningPopupMsgBox(nkm_ERROR_CODE);
						return;
					}
				}
				NKCPacketSender.Send_NKMPacket_EQUIP_ITEM_CONFIRM_SET_OPTION_REQ(this.m_LeftEquipUID);
			}
		}

		// Token: 0x0600660F RID: 26127 RVA: 0x00208574 File Offset: 0x00206774
		private void UpdateUIPrecision()
		{
			if (this.m_LeftEquipUID == 0L)
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMEquipItemData itemEquip = myUserData.m_InventoryData.GetItemEquip(this.m_LeftEquipUID);
			if (itemEquip == null)
			{
				return;
			}
			bool flag = true;
			int num = (this.m_iSelectOptionIdx == 1) ? itemEquip.m_Precision : itemEquip.m_Precision2;
			if (num >= 100)
			{
				this.m_NKM_UI_FACTORY_TUNING_BUTTON_PRECISION.material = NKCResourceUtility.GetOrLoadAssetResource<Material>("AB_UI_NKM_UI_OPERATION_EP_THUMBNAIL", "EP_THUMBNAIL_BLACK_AND_WHITE", false);
			}
			else
			{
				if (myUserData.m_InventoryData.GetCountMiscItem(1013) < (long)this.m_PrecisionReqMaterial || myUserData.m_InventoryData.GetCountMiscItem(1) < (long)this.m_PrecisionReqCredit)
				{
					flag = false;
				}
				this.SetSlotMaterialData(ref this.m_MaterialSlot1, 1013, this.m_PrecisionReqMaterial);
				this.SetSlotMaterialData(ref this.m_MaterialSlot2, 1, this.m_PrecisionReqCredit);
				this.m_NKM_UI_FACTORY_TUNING_BUTTON_PRECISION.material = null;
			}
			if (num >= 100 || !flag)
			{
				this.EnableUI(false);
				return;
			}
			this.EnableUI(true);
		}

		// Token: 0x06006610 RID: 26128 RVA: 0x00208664 File Offset: 0x00206864
		public void UpdateRequireItemUI()
		{
			switch (this.m_NKC_TUNING_TAB)
			{
			case NKCUIForgeTuning.NKC_TUNING_TAB.NTT_PRECISION:
				this.UpdateUIPrecision();
				return;
			case NKCUIForgeTuning.NKC_TUNING_TAB.NTT_OPTION_CHANGE:
				this.UpdateUIOptionChange();
				return;
			case NKCUIForgeTuning.NKC_TUNING_TAB.NTT_SET_OPTION_CHANGE:
				this.UpdateUISetOptionChange();
				return;
			default:
				Debug.LogError("설정되지 않은 옵션(" + this.m_NKC_TUNING_TAB.ToString() + ")입니다.");
				return;
			}
		}

		// Token: 0x06006611 RID: 26129 RVA: 0x002086C8 File Offset: 0x002068C8
		private void UpdateUIOptionChange()
		{
			if (this.m_LeftEquipUID == 0L)
			{
				return;
			}
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
			int optionState = this.GetOptionState();
			int statGroupID = (optionState == 1 || optionState == 3) ? equipTemplet.m_StatGroupID : equipTemplet.m_StatGroupID_2;
			if (NKMEquipTuningManager.GetEquipRandomStatGroupList(statGroupID) == null || NKMEquipTuningManager.GetEquipRandomStatGroupList(statGroupID).Count == 1)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_TUNING_OPTION_CHANGE_WARNING, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_TUNING_OPTION_CHANGE_WARNING, true);
				this.SetSlotMaterialData(ref this.m_MaterialSlot1, 1013, this.m_RandomStatReqMaterial);
				this.SetSlotMaterialData(ref this.m_MaterialSlot2, 1, this.m_RandomStatReqCredit);
				if (NKCScenManager.GetScenManager().GetMyUserData().IsPossibleTuning(this.m_LeftEquipUID, this.m_iSelectOptionIdx - 1))
				{
					this.m_NKM_UI_FACTORY_TUNING_BUTTON_OK.material = null;
				}
			}
			this.EnableUI(true);
		}

		// Token: 0x06006612 RID: 26130 RVA: 0x002087B8 File Offset: 0x002069B8
		private void UpdateUISetOptionChange()
		{
			NKMEquipItemData itemEquip = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(this.m_LeftEquipUID);
			NKMEquipTemplet nkmequipTemplet = null;
			this.m_curSetOptionState = NKCUIForgeTuning.SET_OPTION_UI_STATE.NOT_SELECTED;
			if (this.m_LeftEquipUID != 0L && itemEquip != null)
			{
				nkmequipTemplet = NKMItemManager.GetEquipTemplet(itemEquip.m_ItemEquipID);
				NKMItemEquipSetOptionTemplet nowSetOptionTemplet = NKMItemManager.GetEquipSetOptionTemplet(itemEquip.m_SetOptionId);
				if (nkmequipTemplet != null)
				{
					if (nowSetOptionTemplet != null)
					{
						if (nkmequipTemplet.SetGroupList != null)
						{
							if (nkmequipTemplet.SetGroupList.Count > 1)
							{
								this.m_curSetOptionState = NKCUIForgeTuning.SET_OPTION_UI_STATE.CAN_POSSIBLE_CHANGE;
							}
							else if (nkmequipTemplet.SetGroupList.Count == 1)
							{
								this.m_curSetOptionState = NKCUIForgeTuning.SET_OPTION_UI_STATE.FIXED_SET_OPTION;
							}
							else
							{
								this.m_curSetOptionState = NKCUIForgeTuning.SET_OPTION_UI_STATE.DONT_HAVE_SET_OPTION_DATA;
							}
						}
						else
						{
							this.m_curSetOptionState = NKCUIForgeTuning.SET_OPTION_UI_STATE.FIXED_SET_OPTION;
						}
					}
					else if (nkmequipTemplet.SetGroupList != null && nkmequipTemplet.SetGroupList.Count > 0)
					{
						this.m_curSetOptionState = NKCUIForgeTuning.SET_OPTION_UI_STATE.FIRST_FREE_CHANGE;
					}
					else
					{
						this.m_curSetOptionState = NKCUIForgeTuning.SET_OPTION_UI_STATE.DONT_HAVE_SET_OPTION_DATA;
					}
				}
				if (nowSetOptionTemplet != null)
				{
					NKCUtil.SetImageSprite(this.m_NowOption_SET_ICON, NKCUtil.GetSpriteEquipSetOptionIcon(nowSetOptionTemplet), false);
					int num = 0;
					if (itemEquip.m_OwnerUnitUID > 0L)
					{
						num = NKMItemManager.GetMatchingSetOptionItem(itemEquip);
					}
					NKCUtil.SetLabelText(this.m_NowOptionSET_NAME, string.Format("{0} ({1}/{2})", NKCStringTable.GetString(nowSetOptionTemplet.m_EquipSetName, false), num, nowSetOptionTemplet.m_EquipSetPart));
					NKCUtil.SetGameobjectActive(this.m_NowOption_SET_None_Text, false);
					NKCUtil.SetGameobjectActive(this.m_NowOptionOption1, nowSetOptionTemplet.m_StatType_1 != NKM_STAT_TYPE.NST_RANDOM);
					NKCUtil.SetGameobjectActive(this.m_NowOptionOption2, nowSetOptionTemplet.m_StatType_2 != NKM_STAT_TYPE.NST_RANDOM);
					string setOptionDescription = NKMItemManager.GetSetOptionDescription(nowSetOptionTemplet.m_StatType_1, nowSetOptionTemplet.m_StatRate_1, nowSetOptionTemplet.m_StatValue_1);
					NKCUtil.SetLabelText(this.m_NowOption_STAT_TEXT_1, setOptionDescription);
					if (nowSetOptionTemplet.m_StatType_2 != NKM_STAT_TYPE.NST_RANDOM)
					{
						string setOptionDescription2 = NKMItemManager.GetSetOptionDescription(nowSetOptionTemplet.m_StatType_2, nowSetOptionTemplet.m_StatRate_2, nowSetOptionTemplet.m_StatValue_2);
						NKCUtil.SetLabelText(this.m_NowOption_STAT_TEXT_2, setOptionDescription2);
					}
				}
				NKCUtil.SetGameobjectActive(this.m_NowOption_SET_None_Text, nowSetOptionTemplet == null);
				NKCUtil.SetGameobjectActive(this.m_NowOption_SET_ICON.gameObject, nowSetOptionTemplet != null);
				NKCUtil.SetGameobjectActive(this.m_NowOptionSET_NAME.gameObject, nowSetOptionTemplet != null);
				int reservedSetOption = NKCScenManager.CurrentUserData().GetReservedSetOption(this.m_LeftEquipUID);
				NKMItemEquipSetOptionTemplet newSetOptionTemplet = NKMItemManager.GetEquipSetOptionTemplet(reservedSetOption);
				if (newSetOptionTemplet != null)
				{
					NKCUtil.SetImageSprite(this.m_NewOption_SET_ICON, NKCUtil.GetSpriteEquipSetOptionIcon(newSetOptionTemplet), false);
					int num2 = 0;
					if (itemEquip.m_OwnerUnitUID > 0L)
					{
						num2 = NKMItemManager.GetExpactSetOptionMatchingCnt(itemEquip, reservedSetOption);
					}
					NKCUtil.SetLabelText(this.m_NewOption_SET_NAME, string.Format("{0} ({1}/{2})", NKCStringTable.GetString(newSetOptionTemplet.m_EquipSetName, false), num2, newSetOptionTemplet.m_EquipSetPart));
					NKCUtil.SetGameobjectActive(this.m_NewOptionOption1, newSetOptionTemplet.m_StatType_1 != NKM_STAT_TYPE.NST_RANDOM);
					NKCUtil.SetGameobjectActive(this.m_NewOptionOption2, newSetOptionTemplet.m_StatType_2 != NKM_STAT_TYPE.NST_RANDOM);
					string setOptionDescription3 = NKMItemManager.GetSetOptionDescription(newSetOptionTemplet.m_StatType_1, newSetOptionTemplet.m_StatRate_1, newSetOptionTemplet.m_StatValue_1);
					NKCUtil.SetLabelText(this.m_NewOption_STAT_TEXT_1, setOptionDescription3);
					if (newSetOptionTemplet.m_StatType_2 != NKM_STAT_TYPE.NST_RANDOM)
					{
						string setOptionDescription4 = NKMItemManager.GetSetOptionDescription(newSetOptionTemplet.m_StatType_2, newSetOptionTemplet.m_StatRate_2, newSetOptionTemplet.m_StatValue_2);
						NKCUtil.SetLabelText(this.m_NewOption_STAT_TEXT_2, setOptionDescription4);
					}
					if (nowSetOptionTemplet != null)
					{
						NKCUtil.SetBindFunction(this.m_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_OK, delegate()
						{
							NKCPopupChangeConfirm.Instance.Open(NKCUtilString.GET_STRING_FORGE_SET_OPTION_CHANGE_POPUP_CONFIRM_TITLE, NKCStringTable.GetString(nowSetOptionTemplet.m_EquipSetName, false), NKCStringTable.GetString(newSetOptionTemplet.m_EquipSetName, false), NKCUtilString.GET_STRING_FORGE_SET_OPTION_CHANGE_POPUP_CONFIRM_DESC, new UnityAction(this.OnSetOptionConfirm), null);
						});
					}
					else
					{
						NKCUtil.SetBindFunction(this.m_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_OK, new UnityAction(this.OnSetOptionConfirm));
					}
				}
			}
			this.UpdateSetOptionUI(nkmequipTemplet);
			this.EnableUI(true);
		}

		// Token: 0x06006613 RID: 26131 RVA: 0x00208B9C File Offset: 0x00206D9C
		private void UpdateSetOptionUI(NKMEquipTemplet equipTemplet)
		{
			switch (this.m_curSetOptionState)
			{
			case NKCUIForgeTuning.SET_OPTION_UI_STATE.NOT_SELECTED:
				NKCUtil.SetGameobjectActive(this.m_NowOption_SET_None_Text, true);
				NKCUtil.SetLabelText(this.m_txtNowOption_SET_None_Text, NKCUtilString.GET_STRING_FORGE_TUNING_SET_NO_OPTION);
				NKCUtil.SetGameobjectActive(this.m_NewOption_BEFORE, true);
				NKCUtil.SetLabelText(this.m_NewOption_BEFORE_SET_None_Text, "-");
				NKCUtil.SetGameobjectActive(this.m_NewOption_AFTER, false);
				NKCUtil.SetGameobjectActive(this.m_NowOption_SET_ICON.gameObject, false);
				NKCUtil.SetGameobjectActive(this.m_NowOptionSET_NAME.gameObject, false);
				NKCUtil.SetGameobjectActive(this.m_NowOptionOption1, false);
				NKCUtil.SetGameobjectActive(this.m_NowOptionOption2, false);
				NKCUtil.SetImageColor(this.m_img_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_OK_ICON, NKCUtil.GetUITextColor(false));
				NKCUtil.SetImageSprite(this.m_img_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_OK, NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY), false);
				NKCUtil.SetLabelTextColor(this.m_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_OK_TEXT, NKCUtil.GetUITextColor(false));
				NKCUtil.SetBindFunction(this.m_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_OK, null);
				NKCUtil.SetImageSprite(this.m_img_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_CHANGE, NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY), false);
				NKCUtil.SetLabelTextColor(this.m_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_CHANGE_TEXT, NKCUtil.GetUITextColor(false));
				NKCUtil.SetBindFunction(this.m_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_CHANGE, null);
				break;
			case NKCUIForgeTuning.SET_OPTION_UI_STATE.FIRST_FREE_CHANGE:
				NKCUtil.SetGameobjectActive(this.m_NowOptionOption1, false);
				NKCUtil.SetGameobjectActive(this.m_NowOptionOption2, false);
				NKCUtil.SetLabelText(this.m_txtNowOption_SET_None_Text, NKCUtilString.GET_STRING_FORGE_TUNING_SET_NO_OPTION);
				if (equipTemplet != null)
				{
					this.SetSlotMaterialData(ref this.m_MaterialSlot1, equipTemplet.m_RandomSetReqItemID, 0);
					this.SetSlotMaterialData(ref this.m_MaterialSlot2, 1, 0);
				}
				break;
			case NKCUIForgeTuning.SET_OPTION_UI_STATE.CAN_POSSIBLE_CHANGE:
				if (equipTemplet != null)
				{
					this.SetSlotMaterialData(ref this.m_MaterialSlot1, equipTemplet.m_RandomSetReqItemID, equipTemplet.m_RandomSetReqItemValue);
					NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
					int randomSetReqResource = equipTemplet.m_RandomSetReqResource;
					if (NKCCompanyBuff.NeedShowEventMark((nkmuserData != null) ? nkmuserData.m_companyBuffDataList : null, NKMConst.Buff.BuffType.BASE_FACTORY_ENCHANT_TUNING_CREDIT_DISCOUNT))
					{
						NKCCompanyBuff.SetDiscountOfCreditInEnchantTuning((nkmuserData != null) ? nkmuserData.m_companyBuffDataList : null, ref randomSetReqResource);
					}
					this.SetSlotMaterialData(ref this.m_MaterialSlot2, 1, randomSetReqResource);
				}
				break;
			case NKCUIForgeTuning.SET_OPTION_UI_STATE.DONT_HAVE_SET_OPTION_DATA:
				NKCUtil.SetGameobjectActive(this.m_NowOption_SET_ICON.gameObject, false);
				NKCUtil.SetGameobjectActive(this.m_NowOptionSET_NAME.gameObject, false);
				NKCUtil.SetLabelText(this.m_txtNowOption_SET_None_Text, NKCUtilString.GET_STRING_FORGE_TUNING_SET_OPTION_CANNOT_CHANGE);
				NKCUtil.SetGameobjectActive(this.m_NowOptionOption1, false);
				NKCUtil.SetGameobjectActive(this.m_NowOptionOption2, false);
				break;
			}
			if (this.m_curSetOptionState == NKCUIForgeTuning.SET_OPTION_UI_STATE.FIXED_SET_OPTION)
			{
				NKCUtil.SetLabelText(this.m_NewOption_BEFORE_SET_None_Text, NKCUtilString.GET_STRING_FORGE_TUNING_SET_OPTION_CANNOT_CHANGE);
			}
			else
			{
				NKCUtil.SetLabelText(this.m_NewOption_BEFORE_SET_None_Text, "-");
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_FREE.gameObject, NKCUIForgeTuning.SET_OPTION_UI_STATE.FIRST_FREE_CHANGE == this.m_curSetOptionState);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_OK.gameObject, NKCUIForgeTuning.SET_OPTION_UI_STATE.FIRST_FREE_CHANGE != this.m_curSetOptionState);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_CHANGE.gameObject, NKCUIForgeTuning.SET_OPTION_UI_STATE.FIRST_FREE_CHANGE != this.m_curSetOptionState);
			NKMItemEquipSetOptionTemplet equipSetOptionTemplet = NKMItemManager.GetEquipSetOptionTemplet(NKCScenManager.CurrentUserData().GetReservedSetOption(this.m_LeftEquipUID));
			if (equipSetOptionTemplet == null)
			{
				NKCUtil.SetGameobjectActive(this.m_NewOptionOption1, false);
				NKCUtil.SetGameobjectActive(this.m_NewOptionOption2, false);
				NKCUtil.SetImageColor(this.m_img_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_OK_ICON, NKCUtil.GetUITextColor(false));
				NKCUtil.SetImageSprite(this.m_img_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_OK, NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY), false);
				NKCUtil.SetLabelTextColor(this.m_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_OK_TEXT, NKCUtil.GetUITextColor(false));
				NKCUtil.SetBindFunction(this.m_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_OK, null);
			}
			else
			{
				NKCUtil.SetImageColor(this.m_img_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_OK_ICON, NKCUtil.GetUITextColor(true));
				NKCUtil.SetImageSprite(this.m_img_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_OK, NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_YELLOW), false);
				NKCUtil.SetLabelTextColor(this.m_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_OK_TEXT, NKCUtil.GetUITextColor(true));
			}
			NKCUtil.SetGameobjectActive(this.m_NewOption_BEFORE, equipSetOptionTemplet == null);
			NKCUtil.SetGameobjectActive(this.m_NewOption_AFTER, equipSetOptionTemplet != null);
			NKCUtil.SetGameobjectActive(this.m_NewOption_SET_ICON.gameObject, equipSetOptionTemplet != null);
			NKCUtil.SetGameobjectActive(this.m_NewOption_SET_NAME.gameObject, equipSetOptionTemplet != null);
		}

		// Token: 0x06006614 RID: 26132 RVA: 0x00208F28 File Offset: 0x00207128
		private void SetSlotMaterialData(ref NKCUIItemCostSlot materialSlot, int itemID, int ReqCnt)
		{
			if (materialSlot == null)
			{
				return;
			}
			long countMiscItem = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetCountMiscItem(itemID);
			bool bShowEvent = itemID == 1 && NKCCompanyBuff.NeedShowEventMark(NKCScenManager.CurrentUserData().m_companyBuffDataList, NKMConst.Buff.BuffType.BASE_FACTORY_ENCHANT_TUNING_CREDIT_DISCOUNT);
			materialSlot.SetData(itemID, ReqCnt, countMiscItem, true, true, bShowEvent);
		}

		// Token: 0x06006615 RID: 26133 RVA: 0x00208F7C File Offset: 0x0020717C
		private void UpdateOptionSlotStat(bool bForce)
		{
			int num = (this.m_NKC_TUNING_TAB == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_PRECISION) ? this.GetSlotState() : this.GetOptionState();
			if (num == 0)
			{
				this.m_firstOptionBtn.m_bLock = true;
				this.m_secondOptionBtn.m_bLock = true;
				this.m_NKM_UI_FACTORY_TUNING_OPTION_SLOT[0].ClearUI(this.m_NKC_TUNING_TAB == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_PRECISION);
				this.m_NKM_UI_FACTORY_TUNING_OPTION_SLOT[1].ClearUI(this.m_NKC_TUNING_TAB == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_PRECISION);
			}
			else if (num == 1)
			{
				this.m_firstOptionBtn.m_bLock = false;
				this.m_secondOptionBtn.m_bLock = true;
				this.m_NKM_UI_FACTORY_TUNING_OPTION_SLOT[1].ClearUI(this.m_NKC_TUNING_TAB == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_PRECISION);
			}
			else if (num == 2)
			{
				this.m_firstOptionBtn.m_bLock = true;
				this.m_secondOptionBtn.m_bLock = false;
				this.m_NKM_UI_FACTORY_TUNING_OPTION_SLOT[0].ClearUI(this.m_NKC_TUNING_TAB == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_PRECISION);
			}
			else if (num == 3)
			{
				this.m_firstOptionBtn.m_bLock = false;
				this.m_secondOptionBtn.m_bLock = false;
			}
			if (bForce)
			{
				int num2 = (num == 1 || num == 3) ? 1 : 2;
				this.m_iSelectOptionIdx = num2;
				this.dOnSelectSlot(num2);
				this.m_firstOptionBtn.Select(num == 1 || num == 3, bForce, false);
				this.m_secondOptionBtn.Select(num == 2, bForce, false);
			}
		}

		// Token: 0x06006616 RID: 26134 RVA: 0x002090C0 File Offset: 0x002072C0
		public void DoAfterRefine(NKMEquipItemData orgData, int changedSlotNum)
		{
			if (orgData == null)
			{
				return;
			}
			NKMEquipItemData itemEquip = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(orgData.m_ItemUid);
			if (itemEquip == null || itemEquip.m_Precision >= 100 || itemEquip.m_Precision2 >= 100)
			{
				this.SetEffect(0, changedSlotNum);
				return;
			}
			if (this.m_NKC_TUNING_TAB == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_PRECISION)
			{
				int num = this.m_iSelectOptionIdx - 1;
				if (this.m_NKM_UI_FACTORY_TUNING_OPTION_SLOT.Length > num)
				{
					this.m_NKM_UI_FACTORY_TUNING_OPTION_SLOT[num].SetPrecisionRate(orgData, num);
				}
			}
		}

		// Token: 0x06006617 RID: 26135 RVA: 0x00209137 File Offset: 0x00207337
		public void Close()
		{
			this.m_LeftEquipUID = 0L;
			this.ClearAllUI();
			this.m_RefineToggleBtn.Select(true, false, false);
		}

		// Token: 0x06006618 RID: 26136 RVA: 0x00209158 File Offset: 0x00207358
		public void ClearAllUI()
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_TUNING_OPTION_CHANGE_WARNING, false);
			NKCUtil.SetBindFunction(this.m_NKM_UI_FACTORY_TUNING_OPTION_CHANGE_LIST_BUTTON, null);
			NKCUtil.SetBindFunction(this.m_NKM_UI_FACTORY_TUNING_SET_OPTION_CHANGE_BUTTON, null);
			NKCUtil.SetBindFunction(this.m_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_CHANGE, null);
			NKCUtil.SetBindFunction(this.m_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_OK, null);
			this.m_MaterialSlot1.SetData(0, 0, 0L, true, true, false);
			this.m_MaterialSlot2.SetData(0, 0, 0L, true, true, false);
			for (int i = 0; i < this.m_NKM_UI_FACTORY_TUNING_OPTION_SLOT.Length; i++)
			{
				this.m_NKM_UI_FACTORY_TUNING_OPTION_SLOT[i].ClearPrecisionRate();
				NKCUIForgeTuningOptionSlot nkcuiforgeTuningOptionSlot = this.m_NKM_UI_FACTORY_TUNING_OPTION_SLOT[i];
				if (nkcuiforgeTuningOptionSlot != null)
				{
					nkcuiforgeTuningOptionSlot.ClearUI(true);
				}
			}
			this.EnableUI(false);
		}

		// Token: 0x06006619 RID: 26137 RVA: 0x00209200 File Offset: 0x00207400
		private void EnableUI(bool bActive)
		{
			if (!bActive)
			{
				this.m_NKM_UI_FACTORY_TUNING_BUTTON_PRECISION.sprite = NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY);
			}
			else
			{
				this.m_NKM_UI_FACTORY_TUNING_BUTTON_PRECISION.sprite = NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_YELLOW);
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_TUNING_BUTTON_PRECISION_LIGHT, bActive);
			NKCUtil.SetImageColor(this.m_img_NKM_UI_FACTORY_TUNING_PRECISION_ICON, NKCUtil.GetUITextColor(bActive));
			NKCUtil.SetLabelTextColor(this.m_txt_NKM_UI_FACTORY_TUNING_PRECISION_TEXT, NKCUtil.GetUITextColor(bActive));
			bool flag = bActive && this.m_NKC_TUNING_TAB == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_OPTION_CHANGE && this.IsOptionChangePossible();
			if (flag)
			{
				this.m_NKM_UI_FACTORY_TUNING_BUTTON_OK.sprite = NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_YELLOW);
			}
			else
			{
				this.m_NKM_UI_FACTORY_TUNING_BUTTON_OK.sprite = NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY);
			}
			NKCUtil.SetImageColor(this.m_img_NKM_UI_FACTORY_TUNING_OK_ICON, NKCUtil.GetUITextColor(flag));
			NKCUtil.SetLabelTextColor(this.m_txt_NKM_UI_FACTORY_TUNING_OK_TEXT, NKCUtil.GetUITextColor(flag));
			if (!bActive)
			{
				NKCUtil.SetImageSprite(this.m_img_NKM_UI_FACTORY_TUNING_BUTTON_CHANGE, NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY), false);
				this.m_txt_NKM_UI_FACTORY_TUNING_BUTTON_CHANGE.color = NKCUtil.GetUITextColor(false);
				return;
			}
			NKCUtil.SetImageSprite(this.m_img_NKM_UI_FACTORY_TUNING_BUTTON_CHANGE, NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_BLUE), false);
			this.m_txt_NKM_UI_FACTORY_TUNING_BUTTON_CHANGE.color = Color.white;
		}

		// Token: 0x170011CC RID: 4556
		// (get) Token: 0x0600661A RID: 26138 RVA: 0x0020930C File Offset: 0x0020750C
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

		// Token: 0x0600661B RID: 26139 RVA: 0x00209369 File Offset: 0x00207569
		public void OpenOptionList()
		{
			if (this.NKCPopupEquipOption != null)
			{
				this.NKCPopupEquipOption.Open(this.m_LeftEquipUID, this.GetOptionState(), NKCUtilString.GET_STRING_SETOPTION_CHANGE_NOTICE);
			}
		}

		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x0600661C RID: 26140 RVA: 0x00209398 File Offset: 0x00207598
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

		// Token: 0x0600661D RID: 26141 RVA: 0x002093F5 File Offset: 0x002075F5
		public void OpenSetOptionList()
		{
			if (this.NKCPopupEquipSetOption != null)
			{
				this.NKCPopupEquipSetOption.Open(this.m_LeftEquipUID, NKCUtilString.GET_STRING_SETOPTION_CHANGE_NOTICE);
			}
		}

		// Token: 0x0600661E RID: 26142 RVA: 0x0020941C File Offset: 0x0020761C
		private int GetOptionState()
		{
			int num = 0;
			if (this.m_LeftEquipUID == 0L)
			{
				return num;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return num;
			}
			NKMEquipItemData itemEquip = myUserData.m_InventoryData.GetItemEquip(this.m_LeftEquipUID);
			if (itemEquip == null)
			{
				return num;
			}
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(itemEquip.m_ItemEquipID);
			if (equipTemplet == null)
			{
				return num;
			}
			if (NKMEquipTuningManager.IsChangeableStatGroup(equipTemplet.m_StatGroupID))
			{
				num++;
			}
			if (NKMEquipTuningManager.IsChangeableStatGroup(equipTemplet.m_StatGroupID_2))
			{
				num += 2;
			}
			return num;
		}

		// Token: 0x0600661F RID: 26143 RVA: 0x00209490 File Offset: 0x00207690
		private int GetSlotState()
		{
			int num = 0;
			if (this.m_LeftEquipUID == 0L)
			{
				return num;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return num;
			}
			NKMEquipItemData itemEquip = myUserData.m_InventoryData.GetItemEquip(this.m_LeftEquipUID);
			if (itemEquip == null)
			{
				return num;
			}
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(itemEquip.m_ItemEquipID);
			if (equipTemplet == null)
			{
				return num;
			}
			if (equipTemplet.m_StatGroupID != 0)
			{
				num++;
			}
			if (equipTemplet.m_StatGroupID_2 != 0)
			{
				num += 2;
			}
			return num;
		}

		// Token: 0x06006620 RID: 26144 RVA: 0x002094FC File Offset: 0x002076FC
		public void SetEffect(int effectID, int slotID = 0)
		{
			switch (effectID)
			{
			case 0:
				if (slotID == 1)
				{
					this.SetAutoUnActiveEffectObject(ref this.m_AB_FX_UI_FACTORY_SMALL_SLOT_OPTION_BEFORE, "ACTIVE_OPTION");
				}
				else if (slotID == 2)
				{
					this.SetAutoUnActiveEffectObject(ref this.m_AB_FX_UI_FACTORY_SMALL_SLOT_OPTION_AFTER, "ACTIVE_OPTION");
				}
				if (slotID == 1 || slotID == 2)
				{
					NKCSoundManager.PlaySound("FX_UI_UNIT_GET_STAR", 1f, 0f, 0f, false, 0f, false, 0f);
					return;
				}
				break;
			case 1:
				if (slotID == 1)
				{
					this.SetAutoUnActiveEffectObject(ref this.m_AB_FX_UI_FACTORY_SMALL_SLOT_OPTION_BEFORE, "ACTIVE_OPTION");
				}
				else if (slotID == 2)
				{
					this.SetAutoUnActiveEffectObject(ref this.m_AB_FX_UI_FACTORY_SMALL_SLOT_OPTION_AFTER, "ACTIVE_OPTION");
				}
				NKCSoundManager.PlaySound("FX_UI_UNIT_GET_MAIN", 1f, 0f, 0f, false, 0f, false, 0f);
				return;
			case 2:
				this.SetAutoUnActiveEffectObject(ref this.m_AB_FX_UI_FACTORY_SMALL_SLOT_AFTER, "ACTIVE_SET_OPTION");
				NKCSoundManager.PlaySound("FX_UI_UNIT_GET_MAIN", 1f, 0f, 0f, false, 0f, false, 0f);
				return;
			case 3:
				this.SetAutoUnActiveEffectObject(ref this.m_AB_FX_UI_FACTORY_EQUIP_SLOT, "ACTIVE_OPTION");
				NKCSoundManager.PlaySound("FX_UI_UNIT_GET_STAR", 1f, 0f, 0f, false, 0f, false, 0f);
				return;
			case 4:
				this.SetAutoUnActiveEffectObject(ref this.m_AB_FX_UI_FACTORY_EQUIP_SLOT, "ACTIVE_SET_OPTION");
				NKCSoundManager.PlaySound("FX_UI_UNIT_GET_STAR", 1f, 0f, 0f, false, 0f, false, 0f);
				break;
			default:
				return;
			}
		}

		// Token: 0x06006621 RID: 26145 RVA: 0x0020967C File Offset: 0x0020787C
		private void SetAutoUnActiveEffectObject(ref Animator ani, string key)
		{
			if (ani != null)
			{
				NKCUtil.SetGameobjectActive(ani.gameObject, true);
				ani.SetTrigger(key);
				RuntimeAnimatorController runtimeAnimatorController = ani.runtimeAnimatorController;
				if (runtimeAnimatorController != null)
				{
					this.UnActiveReservedGameObject = null;
					int num = -1;
					if (string.Equals(key, "ACTIVE_OPTION"))
					{
						num = 0;
					}
					else if (string.Equals(key, "ACTIVE_SET_OPTION"))
					{
						num = 1;
					}
					if (num > -1 && runtimeAnimatorController.animationClips.Length >= num)
					{
						float length = runtimeAnimatorController.animationClips[num].length;
						base.StartCoroutine(this.UnActiveEffect(length));
						this.UnActiveReservedGameObject = ani.gameObject;
						Debug.Log(string.Format("test : {0}", length));
					}
				}
			}
		}

		// Token: 0x06006622 RID: 26146 RVA: 0x00209732 File Offset: 0x00207932
		private IEnumerator UnActiveEffect(float closedTime)
		{
			yield return new WaitForSeconds(closedTime);
			NKCUtil.SetGameobjectActive(this.UnActiveReservedGameObject, false);
			yield break;
		}

		// Token: 0x06006623 RID: 26147 RVA: 0x00209748 File Offset: 0x00207948
		public static bool IsPercentStat(NKMEquipRandomStatTemplet statTemplet)
		{
			if (statTemplet != null)
			{
				if (NKMUnitStatManager.IsPercentStat(statTemplet.m_StatType))
				{
					return true;
				}
				if (statTemplet.ApplyType == StatApplyType.Multipliable && statTemplet.m_MinStatRate > 0f)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006624 RID: 26148 RVA: 0x00209778 File Offset: 0x00207978
		public static string GetTuningOptionStatString(NKMEquipItemData equipData, int StatGroupIdx = -1)
		{
			string result = "";
			bool bPercentStat = false;
			if (equipData != null)
			{
				EQUIP_ITEM_STAT equip_ITEM_STAT = null;
				if (equipData.m_Stat.Count > 0 && equipData.m_Stat.Count > StatGroupIdx)
				{
					equip_ITEM_STAT = equipData.m_Stat[StatGroupIdx];
				}
				if (equip_ITEM_STAT != null)
				{
					NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(equipData.m_ItemEquipID);
					if (equipTemplet != null)
					{
						foreach (NKMEquipRandomStatTemplet nkmequipRandomStatTemplet in NKMEquipTuningManager.GetEquipRandomStatGroupList((StatGroupIdx == 1) ? equipTemplet.m_StatGroupID : equipTemplet.m_StatGroupID_2))
						{
							if (nkmequipRandomStatTemplet.m_StatType == equip_ITEM_STAT.type)
							{
								bPercentStat = NKCUIForgeTuning.IsPercentStat(nkmequipRandomStatTemplet);
							}
						}
					}
					return NKCUIForgeTuning.GetTuningOptionStatString(equip_ITEM_STAT, equipData, bPercentStat);
				}
			}
			return result;
		}

		// Token: 0x06006625 RID: 26149 RVA: 0x00209844 File Offset: 0x00207A44
		public static string GetTuningOptionStatString(EQUIP_ITEM_STAT statData, NKMEquipItemData equipData, bool bPercentStat)
		{
			if (bPercentStat)
			{
				decimal num = new decimal((statData.stat_factor > 0f) ? statData.stat_factor : (statData.stat_value + (float)equipData.m_EnchantLevel * statData.stat_level_value));
				num = Math.Round(num * 1000m) / 1000m;
				return NKCUtilString.GetStatShortString("{0} {1:P1}", statData.type, num);
			}
			return NKCUtilString.GetStatShortString("{0} {1:+#;-#;''}", statData.type, statData.stat_value + (float)equipData.m_EnchantLevel * statData.stat_level_value);
		}

		// Token: 0x04005184 RID: 20868
		private NKCUIForgeTuning.NKC_TUNING_TAB m_NKC_TUNING_TAB;

		// Token: 0x04005185 RID: 20869
		private long m_LeftEquipUID;

		// Token: 0x04005186 RID: 20870
		public NKCUIRectMove m_rmTuningRoot;

		// Token: 0x04005187 RID: 20871
		[Header("panel")]
		public GameObject m_NKM_UI_FACTORY_TUNING_PRECISION_panel;

		// Token: 0x04005188 RID: 20872
		public GameObject m_NKM_UI_FACTORY_TUNING_OPTION_CHANGE_panel;

		// Token: 0x04005189 RID: 20873
		public GameObject m_NKM_UI_FACTORY_TUNING_SET_CHANGE_panel;

		// Token: 0x0400518A RID: 20874
		[Header("정밀화 버튼")]
		public Image m_NKM_UI_FACTORY_TUNING_BUTTON_PRECISION;

		// Token: 0x0400518B RID: 20875
		public GameObject m_NKM_UI_FACTORY_TUNING_BUTTON_PRECISION_LIGHT;

		// Token: 0x0400518C RID: 20876
		public NKCUIComButton m_btnNKM_UI_FACTORY_TUNING_BUTTON_PRECISION;

		// Token: 0x0400518D RID: 20877
		[Header("옵션 변경 경고 메시지")]
		public GameObject m_NKM_UI_FACTORY_TUNING_OPTION_CHANGE_WARNING;

		// Token: 0x0400518E RID: 20878
		[Header("탭 토글")]
		public NKCUIComToggle m_RefineToggleBtn;

		// Token: 0x0400518F RID: 20879
		public NKCUIComToggle m_ChangeToggleBtn;

		// Token: 0x04005190 RID: 20880
		public NKCUIComToggle m_SetOptionChangeToggleBtn;

		// Token: 0x04005191 RID: 20881
		[Header("튜닝 버튼")]
		public Image m_NKM_UI_FACTORY_TUNING_BUTTON_OK;

		// Token: 0x04005192 RID: 20882
		public NKCUIComButton m_btnNKM_UI_FACTORY_TUNING_BUTTON_OK;

		// Token: 0x04005193 RID: 20883
		public NKCUIComButton m_btnNKM_UI_FACTORY_TUNING_BUTTON_CHANGE;

		// Token: 0x04005194 RID: 20884
		public Image m_img_NKM_UI_FACTORY_TUNING_PRECISION_ICON;

		// Token: 0x04005195 RID: 20885
		public Text m_txt_NKM_UI_FACTORY_TUNING_PRECISION_TEXT;

		// Token: 0x04005196 RID: 20886
		public Image m_img_NKM_UI_FACTORY_TUNING_OK_ICON;

		// Token: 0x04005197 RID: 20887
		public Text m_txt_NKM_UI_FACTORY_TUNING_OK_TEXT;

		// Token: 0x04005198 RID: 20888
		public Image m_img_NKM_UI_FACTORY_TUNING_BUTTON_CHANGE;

		// Token: 0x04005199 RID: 20889
		public Text m_txt_NKM_UI_FACTORY_TUNING_BUTTON_CHANGE;

		// Token: 0x0400519A RID: 20890
		public Image m_img_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_CHANGE;

		// Token: 0x0400519B RID: 20891
		public NKCUIComStateButton m_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_CHANGE;

		// Token: 0x0400519C RID: 20892
		public Text m_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_CHANGE_TEXT;

		// Token: 0x0400519D RID: 20893
		public Image m_img_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_OK;

		// Token: 0x0400519E RID: 20894
		public Image m_img_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_OK_ICON;

		// Token: 0x0400519F RID: 20895
		public NKCUIComStateButton m_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_OK;

		// Token: 0x040051A0 RID: 20896
		public Text m_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_OK_TEXT;

		// Token: 0x040051A1 RID: 20897
		public NKCUIComStateButton m_NKM_UI_FACTORY_TUNING_SET_OPTION_BUTTON_FREE;

		// Token: 0x040051A2 RID: 20898
		public NKCUIComToggle m_firstOptionBtn;

		// Token: 0x040051A3 RID: 20899
		public NKCUIComToggle m_secondOptionBtn;

		// Token: 0x040051A4 RID: 20900
		private int m_iSelectOptionIdx = 1;

		// Token: 0x040051A5 RID: 20901
		[Header("소모 재화 슬롯")]
		public NKCUIItemCostSlot m_MaterialSlot1;

		// Token: 0x040051A6 RID: 20902
		public NKCUIItemCostSlot m_MaterialSlot2;

		// Token: 0x040051A7 RID: 20903
		[Header("리뉴얼 옵션(최대2개)")]
		public NKCUIForgeTuningOptionSlot[] m_NKM_UI_FACTORY_TUNING_OPTION_SLOT;

		// Token: 0x040051A8 RID: 20904
		public NKCUIComToggleGroup m_NKM_UI_FACTORY_TUNING_OPTION_SLOT_toggle;

		// Token: 0x040051A9 RID: 20905
		[Header("옵션 목록 보기")]
		public NKCUIComStateButton m_NKM_UI_FACTORY_TUNING_OPTION_CHANGE_LIST_BUTTON;

		// Token: 0x040051AA RID: 20906
		public NKCUIComStateButton m_NKM_UI_FACTORY_TUNING_SET_OPTION_CHANGE_BUTTON;

		// Token: 0x040051AB RID: 20907
		[Header("세트 옵션 변경")]
		public GameObject m_NowOption_SET_None_Text;

		// Token: 0x040051AC RID: 20908
		public Text m_txtNowOption_SET_None_Text;

		// Token: 0x040051AD RID: 20909
		public Image m_NowOption_SET_ICON;

		// Token: 0x040051AE RID: 20910
		public Text m_NowOptionSET_NAME;

		// Token: 0x040051AF RID: 20911
		public GameObject m_NowOptionOption1;

		// Token: 0x040051B0 RID: 20912
		public Text m_NowOption_STAT_TEXT_1;

		// Token: 0x040051B1 RID: 20913
		public GameObject m_NowOptionOption2;

		// Token: 0x040051B2 RID: 20914
		public Text m_NowOption_STAT_TEXT_2;

		// Token: 0x040051B3 RID: 20915
		[Space]
		public GameObject m_NewOption_SET_None_Text;

		// Token: 0x040051B4 RID: 20916
		public GameObject m_NewOption_BEFORE;

		// Token: 0x040051B5 RID: 20917
		public GameObject m_NewOption_AFTER;

		// Token: 0x040051B6 RID: 20918
		public Text m_NewOption_BEFORE_SET_None_Text;

		// Token: 0x040051B7 RID: 20919
		public Image m_NewOption_SET_ICON;

		// Token: 0x040051B8 RID: 20920
		public Text m_NewOption_SET_NAME;

		// Token: 0x040051B9 RID: 20921
		public GameObject m_NewOptionOption1;

		// Token: 0x040051BA RID: 20922
		public Text m_NewOption_STAT_TEXT_1;

		// Token: 0x040051BB RID: 20923
		public GameObject m_NewOptionOption2;

		// Token: 0x040051BC RID: 20924
		public Text m_NewOption_STAT_TEXT_2;

		// Token: 0x040051BD RID: 20925
		[Header("이펙트")]
		public Animator m_AB_FX_UI_FACTORY_EQUIP_SLOT;

		// Token: 0x040051BE RID: 20926
		public Animator m_AB_FX_UI_FACTORY_SMALL_SLOT_OPTION_BEFORE;

		// Token: 0x040051BF RID: 20927
		public Animator m_AB_FX_UI_FACTORY_SMALL_SLOT_OPTION_AFTER;

		// Token: 0x040051C0 RID: 20928
		public Animator m_AB_FX_UI_FACTORY_SMALL_SLOT_AFTER;

		// Token: 0x040051C1 RID: 20929
		private NKCUIForgeTuning.OnSelectSlot dOnSelectSlot;

		// Token: 0x040051C2 RID: 20930
		private int m_PrecisionReqCredit = 150;

		// Token: 0x040051C3 RID: 20931
		private int m_PrecisionReqMaterial = 1;

		// Token: 0x040051C4 RID: 20932
		private int m_RandomStatReqCredit = 150;

		// Token: 0x040051C5 RID: 20933
		private int m_RandomStatReqMaterial = 1;

		// Token: 0x040051C6 RID: 20934
		private NKCUIForgeTuning.SET_OPTION_UI_STATE m_curSetOptionState;

		// Token: 0x040051C7 RID: 20935
		private NKCPopupEquipOptionList m_NKCPopupEquipOption;

		// Token: 0x040051C8 RID: 20936
		private NKCPopupEquipSetOptionList m_NKCPopupEquipSetOption;

		// Token: 0x040051C9 RID: 20937
		private const string ACTIVE_OPTION = "ACTIVE_OPTION";

		// Token: 0x040051CA RID: 20938
		private const string ACTIVE_SET_OPTION = "ACTIVE_SET_OPTION";

		// Token: 0x040051CB RID: 20939
		private GameObject UnActiveReservedGameObject;

		// Token: 0x02001667 RID: 5735
		public enum NKC_TUNING_TAB
		{
			// Token: 0x0400A43B RID: 42043
			NTT_PRECISION,
			// Token: 0x0400A43C RID: 42044
			NTT_OPTION_CHANGE,
			// Token: 0x0400A43D RID: 42045
			NTT_SET_OPTION_CHANGE
		}

		// Token: 0x02001668 RID: 5736
		// (Invoke) Token: 0x0600B02B RID: 45099
		public delegate void OnSelectSlot(int idx);

		// Token: 0x02001669 RID: 5737
		private enum SET_OPTION_UI_STATE
		{
			// Token: 0x0400A43F RID: 42047
			NOT_SELECTED,
			// Token: 0x0400A440 RID: 42048
			FIRST_FREE_CHANGE,
			// Token: 0x0400A441 RID: 42049
			CAN_POSSIBLE_CHANGE,
			// Token: 0x0400A442 RID: 42050
			FIXED_SET_OPTION,
			// Token: 0x0400A443 RID: 42051
			DONT_HAVE_SET_OPTION_DATA
		}
	}
}
