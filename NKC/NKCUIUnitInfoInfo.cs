using System;
using System.Collections.Generic;
using ClientPacket.Item;
using NKC.UI.Guide;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000AA6 RID: 2726
	public class NKCUIUnitInfoInfo : MonoBehaviour
	{
		// Token: 0x1700144C RID: 5196
		// (get) Token: 0x060078E0 RID: 30944 RVA: 0x00282308 File Offset: 0x00280508
		public NKCUIEquipPreset EquipPreset
		{
			get
			{
				return this.m_NKCUIEquipPreset;
			}
		}

		// Token: 0x060078E1 RID: 30945 RVA: 0x00282310 File Offset: 0x00280510
		public void Init()
		{
			if (this.m_cbtnUnitInfoDetailPopup != null)
			{
				this.m_cbtnUnitInfoDetailPopup.PointerClick.RemoveAllListeners();
				this.m_cbtnUnitInfoDetailPopup.PointerClick.AddListener(new UnityAction(this.OpenUnitInfoDetailPopup));
			}
			if (this.m_UISkillPanel != null)
			{
				this.m_UISkillPanel.Init();
				this.m_UISkillPanel.SetOpenPopupWhenSelected();
			}
			if (this.m_GuideBtn != null)
			{
				this.m_GuideBtn.PointerClick.RemoveAllListeners();
				this.m_GuideBtn.PointerClick.AddListener(delegate()
				{
					NKCUIPopUpGuide.Instance.Open(this.m_GuideStrID, 0);
				});
			}
			this.m_slotEquipWeapon.Init();
			this.m_slotEquipWeapon.SetEmpty(new NKCUISlot.OnClick(this.OnEmptyEquipWeaponSlotClick));
			this.m_slotEquipWeapon.Set_EQUIP_BOX_BOTTOM_MENU_TYPE(NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_CHANGE);
			this.m_slotEquipDefense.Init();
			this.m_slotEquipDefense.SetEmpty(new NKCUISlot.OnClick(this.OnEmptyEquipDefSlotClick));
			this.m_slotEquipDefense.Set_EQUIP_BOX_BOTTOM_MENU_TYPE(NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_CHANGE);
			this.m_slotEquipAcc.Init();
			this.m_slotEquipAcc.SetEmpty(new NKCUISlot.OnClick(this.OnEmptyEquipAccSlotClick));
			this.m_slotEquipAcc.Set_EQUIP_BOX_BOTTOM_MENU_TYPE(NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_CHANGE);
			this.m_slotEquipAcc_2.Init();
			this.m_slotEquipAcc_2.SetEmpty(new NKCUISlot.OnClick(this.OnEmptyEquipAcc2SlotClick));
			this.m_slotEquipAcc_2.Set_EQUIP_BOX_BOTTOM_MENU_TYPE(NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_CHANGE);
			NKCUtil.SetBindFunction(this.m_NKM_UI_UNIT_INFO_DESC_EQUIP_AUTO, new UnityAction(this.AddAllEquipItem));
			NKCUtil.SetBindFunction(this.m_NKM_UI_UNIT_INFO_DESC_EQUIP_RESET, new UnityAction(this.ClearAllEquipItem));
			NKCUIEquipPreset nkcuiequipPreset = this.m_NKCUIEquipPreset;
			if (nkcuiequipPreset != null)
			{
				nkcuiequipPreset.Init();
			}
			NKCUtil.SetButtonClickDelegate(this.m_csbtnEquipPreset, new UnityAction(this.OnClickEquipPreset));
			NKCUIComStateButton btnUnEquip = this.m_btnUnEquip;
			if (btnUnEquip != null)
			{
				btnUnEquip.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton btnUnEquip2 = this.m_btnUnEquip;
			if (btnUnEquip2 != null)
			{
				btnUnEquip2.PointerClick.AddListener(new UnityAction(this.OnClickUnEquip));
			}
			NKCUIComStateButton btnReinforce = this.m_btnReinforce;
			if (btnReinforce != null)
			{
				btnReinforce.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton btnReinforce2 = this.m_btnReinforce;
			if (btnReinforce2 != null)
			{
				btnReinforce2.PointerClick.AddListener(new UnityAction(this.OnClickReinforce));
			}
			NKCUIComStateButton btnChange = this.m_btnChange;
			if (btnChange != null)
			{
				btnChange.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton btnChange2 = this.m_btnChange;
			if (btnChange2 == null)
			{
				return;
			}
			btnChange2.PointerClick.AddListener(new UnityAction(this.OnClickChange));
		}

		// Token: 0x060078E2 RID: 30946 RVA: 0x0028256A File Offset: 0x0028076A
		public void Clear()
		{
			NKCUIEquipPreset nkcuiequipPreset = this.m_NKCUIEquipPreset;
			if (nkcuiequipPreset != null)
			{
				nkcuiequipPreset.CloseUI();
			}
			this.SetEnableEquipInfo(false);
		}

		// Token: 0x060078E3 RID: 30947 RVA: 0x00282584 File Offset: 0x00280784
		public void SetData(NKMUnitData unitData, bool bFierceInfo = false)
		{
			if (unitData != null)
			{
				this.m_lbPowerSummary.text = unitData.CalculateOperationPower(NKCScenManager.CurrentUserData().m_InventoryData, 0, null, null).ToString();
			}
			this.m_UISkillPanel.SetData(unitData, false);
			NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(unitData.m_UnitID);
			if (unitStatTemplet == null)
			{
				return;
			}
			bool flag = false;
			NKMStatData nkmstatData = new NKMStatData();
			nkmstatData.Init();
			nkmstatData.MakeBaseStat(null, flag, unitData, unitStatTemplet.m_StatData, false, 0, null);
			nkmstatData.MakeBaseBonusFactor(unitData, NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.EquipItems, null, null, flag);
			this.m_slotHP.SetStat(NKM_STAT_TYPE.NST_HP, nkmstatData, unitData);
			this.m_slotAttack.SetStat(NKM_STAT_TYPE.NST_ATK, nkmstatData, unitData);
			this.m_slotDefense.SetStat(NKM_STAT_TYPE.NST_DEF, nkmstatData, unitData);
			this.m_slotHitRate.SetStat(NKM_STAT_TYPE.NST_HIT, nkmstatData, unitData);
			this.m_slotCritHitRate.SetStat(NKM_STAT_TYPE.NST_CRITICAL, nkmstatData, unitData);
			this.m_slotEvade.SetStat(NKM_STAT_TYPE.NST_EVADE, nkmstatData, unitData);
			this.m_UnitData = unitData;
			this.UpdateEquipSlots();
			this.m_bShowFierceInfo = bFierceInfo;
			NKCUIEquipPreset nkcuiequipPreset = this.m_NKCUIEquipPreset;
			if (nkcuiequipPreset != null)
			{
				nkcuiequipPreset.ChangeUnitData(this.m_UnitData);
			}
			this.SetEnableEquipInfo(false);
		}

		// Token: 0x060078E4 RID: 30948 RVA: 0x0028269B File Offset: 0x0028089B
		private void OpenUnitInfoDetailPopup()
		{
			this.SetEnableEquipInfo(false);
			if (NKCPopupUnitInfoDetail.IsInstanceOpen)
			{
				NKCPopupUnitInfoDetail.CheckInstanceAndClose();
				return;
			}
			NKCPopupUnitInfoDetail.InstanceOpen(this.m_UnitData, NKCPopupUnitInfoDetail.UnitInfoDetailType.normal, null);
		}

		// Token: 0x060078E5 RID: 30949 RVA: 0x002826C0 File Offset: 0x002808C0
		public void UpdateEquipSlots()
		{
			this.SetEnableEquipInfo(false);
			if (this.m_UnitData != null)
			{
				if (NKMUnitManager.GetUnitTempletBase(this.m_UnitData.m_UnitID).m_NKM_UNIT_STYLE_TYPE == NKM_UNIT_STYLE_TYPE.NUST_TRAINER)
				{
					NKCUtil.SetGameobjectActive(this.m_objEquipSlotParent, false);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objEquipSlotParent, true);
					this.UpdateEquipItemSlot(this.m_UnitData.GetEquipItemWeaponUid(), ref this.m_slotEquipWeapon, new NKCUISlot.OnClick(this.OnEmptyEquipWeaponSlotClick), this.m_NKM_UI_UNIT_INFO_EQUIP_SET_FX_WEAPON, this.m_ani_NKM_UI_UNIT_INFO_EQUIP_SET_FX_WEAPON);
					this.UpdateEquipItemSlot(this.m_UnitData.GetEquipItemDefenceUid(), ref this.m_slotEquipDefense, new NKCUISlot.OnClick(this.OnEmptyEquipDefSlotClick), this.m_NKM_UI_UNIT_INFO_EQUIP_SET_FX_DEFENCE, this.m_ani_NKM_UI_UNIT_INFO_EQUIP_SET_FX_DEFENCE);
					this.UpdateEquipItemSlot(this.m_UnitData.GetEquipItemAccessoryUid(), ref this.m_slotEquipAcc, new NKCUISlot.OnClick(this.OnEmptyEquipAccSlotClick), this.m_NKM_UI_UNIT_INFO_EQUIP_SET_FX_ACC, this.m_ani_NKM_UI_UNIT_INFO_EQUIP_SET_FX_ACC);
					this.UpdateEquipItemSlot(this.m_UnitData.GetEquipItemAccessory2Uid(), ref this.m_slotEquipAcc_2, new NKCUISlot.OnClick(this.OnEmptyEquipAcc2SlotClick), this.m_NKM_UI_UNIT_INFO_EQUIP_SET_FX_ACC_02, this.m_ani_NKM_UI_UNIT_INFO_EQUIP_SET_FX_ACC_02);
				}
				if (!this.m_UnitData.IsUnlockAccessory2())
				{
					this.m_slotEquipAcc_2.SetLock(new NKCUISlot.OnClick(this.OnSetLockMessage));
					this.m_slotEquipAcc_2.Set_EQUIP_BOX_BOTTOM_MENU_TYPE(NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_NONE);
					return;
				}
			}
			else
			{
				this.m_slotEquipAcc_2.SetLock(new NKCUISlot.OnClick(this.OnSetLockMessage));
				this.m_slotEquipAcc_2.Set_EQUIP_BOX_BOTTOM_MENU_TYPE(NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_NONE);
			}
		}

		// Token: 0x060078E6 RID: 30950 RVA: 0x00282824 File Offset: 0x00280A24
		private void OnSetLockMessage(NKCUISlot.SlotData slotData, bool bLocked)
		{
			NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_EQUIP_ACC_2_LOCKED_DESC, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
		}

		// Token: 0x060078E7 RID: 30951 RVA: 0x0028283C File Offset: 0x00280A3C
		private void UpdateEquipItemSlot(long equipItemUID, ref NKCUISlot slot, NKCUISlot.OnClick func, GameObject effObj, Animator effAni)
		{
			bool flag = false;
			bool flag2 = false;
			NKCUtil.SetGameobjectActive(effObj, true);
			if (equipItemUID > 0L)
			{
				NKMEquipItemData itemEquip = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(equipItemUID);
				if (itemEquip != null)
				{
					slot.SetData(NKCUISlot.SlotData.MakeEquipData(itemEquip), false, true, false, null);
					slot.SetOnClick(new NKCUISlot.OnClick(this.OpenEquipBoxForChange));
					flag = true;
					if (NKMItemManager.IsActiveSetOptionItem(itemEquip) && effAni != null)
					{
						NKMItemEquipSetOptionTemplet equipSetOptionTemplet = NKMItemManager.GetEquipSetOptionTemplet(itemEquip.m_SetOptionId);
						if (equipSetOptionTemplet != null)
						{
							flag2 = true;
							effAni.SetTrigger(equipSetOptionTemplet.m_EquipSetIconEffect);
						}
					}
				}
			}
			if (!flag2)
			{
				NKCUtil.SetGameobjectActive(effObj, false);
			}
			if (!flag)
			{
				slot.SetCustomizedEmptySP(this.GetCustomizedEquipEmptySP());
				slot.SetEmpty(func);
			}
			slot.SetUsedMark(false);
		}

		// Token: 0x060078E8 RID: 30952 RVA: 0x002828F4 File Offset: 0x00280AF4
		private void OnEmptyEquipSlotClick(ITEM_EQUIP_POSITION equipPos)
		{
			this.SetEnableEquipInfo(false);
			if (this.m_UnitData == null)
			{
				return;
			}
			if (this.IsSeizedUnit(this.m_UnitData))
			{
				return;
			}
			NKCUtil.ChangeEquip(this.m_UnitData.m_UnitUID, equipPos, null, 0L, this.m_bShowFierceInfo);
		}

		// Token: 0x060078E9 RID: 30953 RVA: 0x0028292F File Offset: 0x00280B2F
		private void OnEmptyEquipWeaponSlotClick(NKCUISlot.SlotData slotData, bool bLocked)
		{
			this.OnEmptyEquipSlotClick(ITEM_EQUIP_POSITION.IEP_WEAPON);
		}

		// Token: 0x060078EA RID: 30954 RVA: 0x00282938 File Offset: 0x00280B38
		private void OnEmptyEquipDefSlotClick(NKCUISlot.SlotData slotData, bool bLocked)
		{
			this.OnEmptyEquipSlotClick(ITEM_EQUIP_POSITION.IEP_DEFENCE);
		}

		// Token: 0x060078EB RID: 30955 RVA: 0x00282941 File Offset: 0x00280B41
		private void OnEmptyEquipAccSlotClick(NKCUISlot.SlotData slotData, bool bLocked)
		{
			this.OnEmptyEquipSlotClick(ITEM_EQUIP_POSITION.IEP_ACC);
		}

		// Token: 0x060078EC RID: 30956 RVA: 0x0028294A File Offset: 0x00280B4A
		private void OnEmptyEquipAcc2SlotClick(NKCUISlot.SlotData slotData, bool bLocked)
		{
			this.OnEmptyEquipSlotClick(ITEM_EQUIP_POSITION.IEP_ACC2);
		}

		// Token: 0x060078ED RID: 30957 RVA: 0x00282954 File Offset: 0x00280B54
		private Sprite GetCustomizedEquipEmptySP()
		{
			if (this.m_UnitData == null)
			{
				return null;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_UnitData.m_UnitID);
			if (unitTempletBase == null)
			{
				return null;
			}
			NKM_UNIT_STYLE_TYPE nkm_UNIT_STYLE_TYPE = unitTempletBase.m_NKM_UNIT_STYLE_TYPE;
			if (nkm_UNIT_STYLE_TYPE - NKM_UNIT_STYLE_TYPE.NUST_COUNTER <= 2)
			{
				return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_UNIT_INFO_SPRITE", "NKM_UI_UNIT_INFO_ITEM_EQUIP_SLOT_ADD", false);
			}
			return null;
		}

		// Token: 0x060078EE RID: 30958 RVA: 0x002829A0 File Offset: 0x00280BA0
		public void OpenEquipBoxForChange(NKCUISlot.SlotData slotData, bool bLocked)
		{
			if (this.m_UnitData == null)
			{
				return;
			}
			NKMEquipItemData itemEquip = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(slotData.UID);
			if (itemEquip != null && itemEquip.m_OwnerUnitUID > 0L)
			{
				if (this.m_LatestSelectedEquipUID == itemEquip.m_ItemUid && this.m_objEquipInfo.activeSelf)
				{
					this.SetEnableEquipInfo(false);
					return;
				}
				this.SetOffAllSlot();
				this.m_LatestSelectedEquipUID = itemEquip.m_ItemUid;
				this.SetEnableEquipInfo(true);
				this.SetSlotSelected(itemEquip.m_ItemUid, true);
				this.m_slotEquip.SetData(itemEquip, this.m_bShowFierceInfo, false);
			}
		}

		// Token: 0x060078EF RID: 30959 RVA: 0x00282A3A File Offset: 0x00280C3A
		private void SetOffAllSlot()
		{
			this.m_slotEquipWeapon.SetSelected(false);
			this.m_slotEquipDefense.SetSelected(false);
			this.m_slotEquipAcc.SetSelected(false);
			this.m_slotEquipAcc_2.SetSelected(false);
		}

		// Token: 0x060078F0 RID: 30960 RVA: 0x00282A6C File Offset: 0x00280C6C
		private void SetSlotSelected(long equipUid, bool bSelect)
		{
			if (this.m_LatestSelectedEquipUID <= 0L)
			{
				return;
			}
			if (this.m_UnitData == null)
			{
				return;
			}
			if (this.m_UnitData.GetEquipItemWeaponUid() == this.m_LatestSelectedEquipUID)
			{
				this.m_slotEquipWeapon.SetSelected(bSelect);
			}
			if (this.m_UnitData.GetEquipItemDefenceUid() == this.m_LatestSelectedEquipUID)
			{
				this.m_slotEquipDefense.SetSelected(bSelect);
			}
			if (this.m_UnitData.GetEquipItemAccessoryUid() == this.m_LatestSelectedEquipUID)
			{
				this.m_slotEquipAcc.SetSelected(bSelect);
			}
			if (this.m_UnitData.GetEquipItemAccessory2Uid() == this.m_LatestSelectedEquipUID)
			{
				this.m_slotEquipAcc_2.SetSelected(bSelect);
			}
		}

		// Token: 0x060078F1 RID: 30961 RVA: 0x00282B09 File Offset: 0x00280D09
		public void UnActiveEffect()
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_INFO_EQUIP_SET_FX_WEAPON, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_INFO_EQUIP_SET_FX_DEFENCE, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_INFO_EQUIP_SET_FX_ACC, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_INFO_EQUIP_SET_FX_ACC_02, false);
		}

		// Token: 0x060078F2 RID: 30962 RVA: 0x00282B3B File Offset: 0x00280D3B
		private bool IsSeizedUnit(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				return true;
			}
			if (unitData.IsSeized)
			{
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_UNIT_IS_SEIZED, null, "");
				return true;
			}
			return false;
		}

		// Token: 0x060078F3 RID: 30963 RVA: 0x00282B5D File Offset: 0x00280D5D
		public bool IsPresetOpend()
		{
			return this.m_NKCUIEquipPreset != null && this.m_NKCUIEquipPreset.IsOpened();
		}

		// Token: 0x060078F4 RID: 30964 RVA: 0x00282B7C File Offset: 0x00280D7C
		private void OnClickEquipPreset()
		{
			this.SetEnableEquipInfo(false);
			if (this.m_NKCUIEquipPreset == null)
			{
				return;
			}
			if (this.m_NKCUIEquipPreset.IsOpened())
			{
				this.m_NKCUIEquipPreset.CloseUI();
				return;
			}
			if (NKCScenManager.GetScenManager().GetMyUserData() == null)
			{
				return;
			}
			if (NKCEquipPresetDataManager.HasData())
			{
				this.m_NKCUIEquipPreset.Open(this.m_UnitData, null, this.m_bShowFierceInfo);
				return;
			}
			NKCEquipPresetDataManager.RequestPresetData(true);
		}

		// Token: 0x060078F5 RID: 30965 RVA: 0x00282BEB File Offset: 0x00280DEB
		public void OpenEquipPreset(List<NKMEquipPresetData> presetDatas)
		{
			this.SetEnableEquipInfo(false);
			NKCUIEquipPreset nkcuiequipPreset = this.m_NKCUIEquipPreset;
			if (nkcuiequipPreset == null)
			{
				return;
			}
			nkcuiequipPreset.Open(this.m_UnitData, presetDatas, this.m_bShowFierceInfo);
		}

		// Token: 0x060078F6 RID: 30966 RVA: 0x00282C11 File Offset: 0x00280E11
		public bool EquipPresetOpened()
		{
			return this.m_NKCUIEquipPreset != null && this.m_NKCUIEquipPreset.IsOpened();
		}

		// Token: 0x060078F7 RID: 30967 RVA: 0x00282C31 File Offset: 0x00280E31
		public void SetEnableEquipInfo(bool bEnable)
		{
			NKCUtil.SetGameobjectActive(this.m_objEquipInfo, bEnable);
			this.SetOffAllSlot();
		}

		// Token: 0x060078F8 RID: 30968 RVA: 0x00282C45 File Offset: 0x00280E45
		private void OnClickUnEquip()
		{
			NKMItemManager.UnEquip(this.m_LatestSelectedEquipUID);
		}

		// Token: 0x060078F9 RID: 30969 RVA: 0x00282C52 File Offset: 0x00280E52
		public void OnClickUnEquip(NKCUISlotEquip slot)
		{
			NKMItemManager.UnEquip(slot.GetEquipItemUID());
		}

		// Token: 0x060078FA RID: 30970 RVA: 0x00282C60 File Offset: 0x00280E60
		public void OnClickReinforce()
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.BASE_FACTORY, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.BASE_FACTORY, 0);
				return;
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_ENCHANT, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.FACTORY_ENCHANT, 0);
				return;
			}
			if (this.m_LatestSelectedEquipUID > 0L)
			{
				NKM_ERROR_CODE nkm_ERROR_CODE = NKMItemManager.CanEnchantItem(NKCScenManager.GetScenManager().GetMyUserData(), this.m_LatestSelectedEquipUID);
				if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
				{
					NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString(nkm_ERROR_CODE.ToString(), false), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
				this.SetEnableEquipInfo(false);
				NKCUIForge.Instance.Open(NKCUIForge.NKC_FORGE_TAB.NFT_ENCHANT, this.m_LatestSelectedEquipUID, null);
			}
		}

		// Token: 0x060078FB RID: 30971 RVA: 0x00282CF5 File Offset: 0x00280EF5
		public void OnClickChange()
		{
			NKCUtil.ChangeEquip(this.m_UnitData.m_UnitUID, NKMItemManager.GetItemEquipPosition(this.m_LatestSelectedEquipUID), delegate(NKCUISlotEquip slot, NKMEquipItemData data)
			{
				this.OnClickUnEquip();
			}, this.m_LatestSelectedEquipUID, this.m_bShowFierceInfo);
		}

		// Token: 0x060078FC RID: 30972 RVA: 0x00282D2C File Offset: 0x00280F2C
		private void AddAllEquipItem()
		{
			if (this.m_UnitData == null)
			{
				return;
			}
			if (this.m_UnitData.GetEquipItemWeaponUid() > 0L && this.m_UnitData.GetEquipItemDefenceUid() > 0L && this.m_UnitData.GetEquipItemAccessoryUid() > 0L && this.m_UnitData.GetEquipItemAccessory2Uid() > 0L)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_ALREADY_FULL_EQUIPMENT, null, "");
				return;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_UnitData.m_UnitID);
			if (unitTempletBase == null)
			{
				return;
			}
			if (!this.IsPossibleUnitType(unitTempletBase.m_NKM_UNIT_STYLE_TYPE))
			{
				return;
			}
			this.m_lstAlreadySelectedEquipItem.Clear();
			this.m_bRecordPrivateAccItemEquipTry = false;
			if (this.m_UnitData.GetEquipItemWeaponUid() <= 0L)
			{
				this.UpdateEquipItem(unitTempletBase, ITEM_EQUIP_POSITION.IEP_WEAPON);
			}
			if (this.m_UnitData.GetEquipItemDefenceUid() <= 0L)
			{
				this.UpdateEquipItem(unitTempletBase, ITEM_EQUIP_POSITION.IEP_DEFENCE);
			}
			if (this.m_UnitData.GetEquipItemAccessoryUid() <= 0L)
			{
				this.UpdateEquipItem(unitTempletBase, ITEM_EQUIP_POSITION.IEP_ACC);
			}
			if (this.m_UnitData.GetEquipItemAccessory2Uid() <= 0L && this.m_UnitData.IsUnlockAccessory2())
			{
				this.UpdateEquipItem(unitTempletBase, ITEM_EQUIP_POSITION.IEP_ACC2);
			}
		}

		// Token: 0x060078FD RID: 30973 RVA: 0x00282E33 File Offset: 0x00281033
		private bool IsPossibleUnitType(NKM_UNIT_STYLE_TYPE targetType)
		{
			if (targetType - NKM_UNIT_STYLE_TYPE.NUST_COUNTER <= 2)
			{
				return true;
			}
			Debug.LogError("현재 허용되지 않는 타입입니다. 추가 직업이 있을 경우, 추가 해주세요. : " + targetType.ToString());
			return false;
		}

		// Token: 0x060078FE RID: 30974 RVA: 0x00282E5C File Offset: 0x0028105C
		private void UpdateEquipItem(NKMUnitTempletBase unitTempletBase, ITEM_EQUIP_POSITION targetEquipPosition)
		{
			if (unitTempletBase == null)
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			NKMInventoryData inventoryData = myUserData.m_InventoryData;
			if (inventoryData == null)
			{
				return;
			}
			NKCEquipSortSystem.eFilterOption item;
			switch (unitTempletBase.m_NKM_UNIT_STYLE_TYPE)
			{
			case NKM_UNIT_STYLE_TYPE.NUST_COUNTER:
				item = NKCEquipSortSystem.eFilterOption.Equip_Counter;
				break;
			case NKM_UNIT_STYLE_TYPE.NUST_SOLDIER:
				item = NKCEquipSortSystem.eFilterOption.Equip_Soldier;
				break;
			case NKM_UNIT_STYLE_TYPE.NUST_MECHANIC:
				item = NKCEquipSortSystem.eFilterOption.Equip_Mechanic;
				break;
			default:
				Debug.LogError(string.Format("지정되지 않은 타입입니다.{0}", unitTempletBase.m_NKM_UNIT_STYLE_TYPE));
				return;
			}
			bool flag = false;
			NKCEquipSortSystem.eFilterOption eFilterOption;
			switch (targetEquipPosition)
			{
			case ITEM_EQUIP_POSITION.IEP_WEAPON:
				eFilterOption = NKCEquipSortSystem.eFilterOption.Equip_Weapon;
				break;
			case ITEM_EQUIP_POSITION.IEP_DEFENCE:
				eFilterOption = NKCEquipSortSystem.eFilterOption.Equip_Armor;
				break;
			case ITEM_EQUIP_POSITION.IEP_ACC:
			case ITEM_EQUIP_POSITION.IEP_ACC2:
				eFilterOption = NKCEquipSortSystem.eFilterOption.Equip_Acc;
				if (this.m_UnitData != null)
				{
					NKMEquipItemData itemEquip = inventoryData.GetItemEquip(this.m_UnitData.GetEquipItemAccessoryUid());
					if (itemEquip != null)
					{
						flag = NKMItemManager.GetEquipTemplet(itemEquip.m_ItemEquipID).IsPrivateEquipForUnit(this.m_UnitData.m_UnitID);
					}
					if (!flag)
					{
						NKMEquipItemData itemEquip2 = inventoryData.GetItemEquip(this.m_UnitData.GetEquipItemAccessory2Uid());
						if (itemEquip2 != null)
						{
							flag = NKMItemManager.GetEquipTemplet(itemEquip2.m_ItemEquipID).IsPrivateEquipForUnit(this.m_UnitData.m_UnitID);
						}
					}
				}
				break;
			default:
				Debug.LogError(string.Format("지정되지 않은 장착 타입입니다.{0}", targetEquipPosition));
				return;
			}
			NKCEquipSortSystem.EquipListOptions equipListOptions = default(NKCEquipSortSystem.EquipListOptions);
			equipListOptions.setOnlyIncludeEquipID = null;
			equipListOptions.setExcludeEquipID = null;
			equipListOptions.setExcludeEquipUID = null;
			equipListOptions.setExcludeFilterOption = null;
			equipListOptions.setFilterOption = new HashSet<NKCEquipSortSystem.eFilterOption>();
			equipListOptions.setFilterOption.Add(NKCEquipSortSystem.eFilterOption.Equip_Unused);
			equipListOptions.OwnerUnitID = unitTempletBase.m_UnitID;
			equipListOptions.setFilterOption.Add(item);
			if (eFilterOption != NKCEquipSortSystem.eFilterOption.Nothing)
			{
				equipListOptions.setFilterOption.Add(eFilterOption);
			}
			equipListOptions.lstSortOption = NKCEquipSortSystem.GetDefaultSortOption(NKCPopupEquipSort.SORT_OPEN_TYPE.OPTION_WEIGHT);
			equipListOptions.PreemptiveSortFunc = null;
			equipListOptions.AdditionalExcludeFilterFunc = null;
			equipListOptions.bHideEquippedItem = false;
			equipListOptions.bPushBackUnselectable = true;
			equipListOptions.bHideLockItem = false;
			equipListOptions.bHideMaxLvItem = false;
			equipListOptions.bLockMaxItem = false;
			equipListOptions.bHideNotPossibleSetOptionItem = false;
			NKCEquipSortSystem nkcequipSortSystem = new NKCEquipSortSystem(myUserData, equipListOptions);
			if (nkcequipSortSystem.SortedEquipList.Count <= 0)
			{
				return;
			}
			for (int i = 0; i < nkcequipSortSystem.SortedEquipList.Count; i++)
			{
				NKMEquipItemData nkmequipItemData = nkcequipSortSystem.SortedEquipList[i];
				NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(nkmequipItemData.m_ItemEquipID);
				if (equipTemplet != null)
				{
					if (!flag && !this.m_bRecordPrivateAccItemEquipTry && equipTemplet.IsPrivateEquipForUnit(this.m_UnitData.m_UnitID) && !this.m_lstAlreadySelectedEquipItem.Contains(nkmequipItemData.m_ItemUid))
					{
						this.m_lstAlreadySelectedEquipItem.Add(nkmequipItemData.m_ItemUid);
						this.SendEquipItem(true, nkmequipItemData.m_ItemUid, targetEquipPosition);
						this.m_bRecordPrivateAccItemEquipTry = (targetEquipPosition == ITEM_EQUIP_POSITION.IEP_ACC || targetEquipPosition == ITEM_EQUIP_POSITION.IEP_ACC2);
						return;
					}
					if (equipTemplet.IsPrivateEquip())
					{
						nkcequipSortSystem.SortedEquipList.RemoveAt(i);
						i--;
					}
				}
			}
			if (nkcequipSortSystem.SortedEquipList.Count <= 0)
			{
				return;
			}
			foreach (NKMEquipItemData nkmequipItemData2 in nkcequipSortSystem.SortedEquipList)
			{
				if (!this.m_lstAlreadySelectedEquipItem.Contains(nkmequipItemData2.m_ItemUid))
				{
					this.SendEquipItem(true, nkmequipItemData2.m_ItemUid, targetEquipPosition);
					this.m_lstAlreadySelectedEquipItem.Add(nkmequipItemData2.m_ItemUid);
					break;
				}
			}
		}

		// Token: 0x060078FF RID: 30975 RVA: 0x002831BC File Offset: 0x002813BC
		private void ClearAllEquipItem()
		{
			if (this.m_UnitData == null)
			{
				return;
			}
			if (!this.m_objEquipSlotParent.activeSelf)
			{
				return;
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = NKMUnitManager.IsUnitBusy(NKCScenManager.GetScenManager().GetMyUserData(), this.m_UnitData, true);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString(nkm_ERROR_CODE.ToString(), false), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			this.SendEquipItem(false, this.m_UnitData.GetEquipItemWeaponUid(), ITEM_EQUIP_POSITION.IEP_WEAPON);
			this.SendEquipItem(false, this.m_UnitData.GetEquipItemDefenceUid(), ITEM_EQUIP_POSITION.IEP_DEFENCE);
			this.SendEquipItem(false, this.m_UnitData.GetEquipItemAccessoryUid(), ITEM_EQUIP_POSITION.IEP_ACC);
			this.SendEquipItem(false, this.m_UnitData.GetEquipItemAccessory2Uid(), ITEM_EQUIP_POSITION.IEP_ACC2);
		}

		// Token: 0x06007900 RID: 30976 RVA: 0x00283268 File Offset: 0x00281468
		private void SendEquipItem(bool bEquip, long targetEquipUId, ITEM_EQUIP_POSITION equipPosition)
		{
			if (this.m_UnitData == null || targetEquipUId <= 0L)
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_EQUIP_ITEM_EQUIP_REQ(bEquip, this.m_UnitData.m_UnitUID, targetEquipUId, equipPosition);
		}

		// Token: 0x04006561 RID: 25953
		public Text m_lbPowerSummary;

		// Token: 0x04006562 RID: 25954
		[Space]
		public NKCUIComStateButton m_GuideBtn;

		// Token: 0x04006563 RID: 25955
		public string m_GuideStrID;

		// Token: 0x04006564 RID: 25956
		public NKCUIUnitStatSlot m_slotHP;

		// Token: 0x04006565 RID: 25957
		public NKCUIUnitStatSlot m_slotAttack;

		// Token: 0x04006566 RID: 25958
		public NKCUIUnitStatSlot m_slotDefense;

		// Token: 0x04006567 RID: 25959
		public NKCUIUnitStatSlot m_slotHitRate;

		// Token: 0x04006568 RID: 25960
		public NKCUIUnitStatSlot m_slotCritHitRate;

		// Token: 0x04006569 RID: 25961
		public NKCUIUnitStatSlot m_slotEvade;

		// Token: 0x0400656A RID: 25962
		public NKCUIComButton m_cbtnUnitInfoDetailPopup;

		// Token: 0x0400656B RID: 25963
		public NKCUIUnitInfoSkillPanel m_UISkillPanel;

		// Token: 0x0400656C RID: 25964
		[Header("아이템")]
		public GameObject m_objEquipSlotParent;

		// Token: 0x0400656D RID: 25965
		public NKCUISlot m_slotEquipWeapon;

		// Token: 0x0400656E RID: 25966
		public NKCUISlot m_slotEquipDefense;

		// Token: 0x0400656F RID: 25967
		public NKCUISlot m_slotEquipAcc;

		// Token: 0x04006570 RID: 25968
		public NKCUISlot m_slotEquipAcc_2;

		// Token: 0x04006571 RID: 25969
		[Header("세트아이템 이펙트")]
		public GameObject m_NKM_UI_UNIT_INFO_EQUIP_SET_FX_WEAPON;

		// Token: 0x04006572 RID: 25970
		public Animator m_ani_NKM_UI_UNIT_INFO_EQUIP_SET_FX_WEAPON;

		// Token: 0x04006573 RID: 25971
		public GameObject m_NKM_UI_UNIT_INFO_EQUIP_SET_FX_DEFENCE;

		// Token: 0x04006574 RID: 25972
		public Animator m_ani_NKM_UI_UNIT_INFO_EQUIP_SET_FX_DEFENCE;

		// Token: 0x04006575 RID: 25973
		public GameObject m_NKM_UI_UNIT_INFO_EQUIP_SET_FX_ACC;

		// Token: 0x04006576 RID: 25974
		public Animator m_ani_NKM_UI_UNIT_INFO_EQUIP_SET_FX_ACC;

		// Token: 0x04006577 RID: 25975
		public GameObject m_NKM_UI_UNIT_INFO_EQUIP_SET_FX_ACC_02;

		// Token: 0x04006578 RID: 25976
		public Animator m_ani_NKM_UI_UNIT_INFO_EQUIP_SET_FX_ACC_02;

		// Token: 0x04006579 RID: 25977
		[Header("장비 프리셋")]
		public NKCUIComStateButton m_csbtnEquipPreset;

		// Token: 0x0400657A RID: 25978
		public NKCUIEquipPreset m_NKCUIEquipPreset;

		// Token: 0x0400657B RID: 25979
		[Header("장비 팝업")]
		public GameObject m_objEquipInfo;

		// Token: 0x0400657C RID: 25980
		public NKCUIInvenEquipSlot m_slotEquip;

		// Token: 0x0400657D RID: 25981
		public NKCUIComStateButton m_btnUnEquip;

		// Token: 0x0400657E RID: 25982
		public NKCUIComStateButton m_btnReinforce;

		// Token: 0x0400657F RID: 25983
		public NKCUIComStateButton m_btnChange;

		// Token: 0x04006580 RID: 25984
		private long m_LatestSelectedEquipUID = -1L;

		// Token: 0x04006581 RID: 25985
		private NKMUnitData m_UnitData;

		// Token: 0x04006582 RID: 25986
		private bool m_bShowFierceInfo;

		// Token: 0x04006583 RID: 25987
		[Header("자동 장착&해제")]
		public NKCUIComStateButton m_NKM_UI_UNIT_INFO_DESC_EQUIP_AUTO;

		// Token: 0x04006584 RID: 25988
		public NKCUIComStateButton m_NKM_UI_UNIT_INFO_DESC_EQUIP_RESET;

		// Token: 0x04006585 RID: 25989
		private List<long> m_lstAlreadySelectedEquipItem = new List<long>();

		// Token: 0x04006586 RID: 25990
		private bool m_bRecordPrivateAccItemEquipTry;
	}
}
