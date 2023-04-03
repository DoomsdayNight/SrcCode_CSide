using System;
using System.Collections.Generic;
using ClientPacket.Item;
using DG.Tweening;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200098B RID: 2443
	public class NKCUIEquipPresetSlot : MonoBehaviour
	{
		// Token: 0x170011A3 RID: 4515
		// (set) Token: 0x0600648B RID: 25739 RVA: 0x001FE362 File Offset: 0x001FC562
		public static int UpdatingIndex
		{
			set
			{
				NKCUIEquipPresetSlot.m_iUpdatingIndex = value;
			}
		}

		// Token: 0x170011A4 RID: 4516
		// (set) Token: 0x0600648C RID: 25740 RVA: 0x001FE36A File Offset: 0x001FC56A
		public static bool SavedPreset
		{
			set
			{
				NKCUIEquipPresetSlot.m_bSavedPreset = value;
			}
		}

		// Token: 0x170011A5 RID: 4517
		// (set) Token: 0x0600648D RID: 25741 RVA: 0x001FE372 File Offset: 0x001FC572
		public static bool ShowSetItemFx
		{
			set
			{
				NKCUIEquipPresetSlot.m_bShowSetItemFx = value;
			}
		}

		// Token: 0x0600648E RID: 25742 RVA: 0x001FE37C File Offset: 0x001FC57C
		public void Init()
		{
			this.m_slotEquipWeapon.Init();
			this.m_slotEquipDefense.Init();
			this.m_slotEquipAcc.Init();
			this.m_slotEquipAcc_2.Init();
			NKCUtil.SetButtonClickDelegate(this.m_csbtnPresetAdd, new UnityAction(this.OnClickPresetAdd));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnChangePresetName, new UnityAction(this.OnClickNameChange));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnSaveUnitEquipSet, new UnityAction(this.OnClickSaveUnitEquip));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnApplyPreset, new UnityAction(this.OnClickApplyPreset));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnApplyDisabled, new UnityAction(this.OnClickApplyPreset));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnUp, new UnityAction(this.OnClickSlotUp));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnDown, new UnityAction(this.OnClickSlotDown));
			int num = 4;
			for (int i = 0; i < num; i++)
			{
				this.m_listEquipUId.Add(0L);
			}
			this.m_inputFieldPresetName.onValueChanged.RemoveAllListeners();
			this.m_inputFieldPresetName.onValueChanged.AddListener(new UnityAction<string>(this.OnInputNameChanged));
			this.m_inputFieldPresetName.onValidateInput = new InputField.OnValidateInput(NKCFilterManager.FilterEmojiInput);
			this.m_inputFieldPresetName.onEndEdit.RemoveAllListeners();
			this.m_inputFieldPresetName.onEndEdit.AddListener(new UnityAction<string>(this.OnInputPresetName));
		}

		// Token: 0x0600648F RID: 25743 RVA: 0x001FE4E0 File Offset: 0x001FC6E0
		public static NKCUIEquipPresetSlot GetNewInstance(Transform parent, bool bMentoringSlot = false)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("ab_ui_nkm_ui_equip_preset", "NKM_UI_POPUP_PRESET_LIST_SLOT", false, null);
			NKCUIEquipPresetSlot nkcuiequipPresetSlot = (nkcassetInstanceData != null) ? nkcassetInstanceData.m_Instant.GetComponent<NKCUIEquipPresetSlot>() : null;
			if (nkcuiequipPresetSlot == null)
			{
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				Debug.LogError("NKCUIEquipPresetSlot Prefab null!");
				return null;
			}
			nkcuiequipPresetSlot.m_InstanceData = nkcassetInstanceData;
			nkcuiequipPresetSlot.Init();
			if (parent != null)
			{
				nkcuiequipPresetSlot.transform.SetParent(parent);
			}
			nkcuiequipPresetSlot.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
			nkcuiequipPresetSlot.gameObject.SetActive(false);
			return nkcuiequipPresetSlot;
		}

		// Token: 0x06006490 RID: 25744 RVA: 0x001FE57A File Offset: 0x001FC77A
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06006491 RID: 25745 RVA: 0x001FE59C File Offset: 0x001FC79C
		public void SetData(int index, NKMEquipPresetData presetData, NKCUIEquipPreset cNKCUIEquipPreset, int newPresetIndexFrom, bool showFierceInfo, bool slotMoveState, LoopScrollRect parentScrollRect, NKCUIEquipPresetSlot.OnClickAdd onClickAdd, NKCUIEquipPresetSlot.OnClickUp onClickUp = null, NKCUIEquipPresetSlot.OnClickDown onClickDown = null)
		{
			this.m_presetData = presetData;
			this.m_bShowFierceInfo = showFierceInfo;
			this.m_cNKCUIEquipPreset = cNKCUIEquipPreset;
			this.m_comDragScrollInputField.ScrollRect = parentScrollRect;
			this.m_dOnClickAdd = onClickAdd;
			this.m_dOnClickUp = onClickUp;
			this.m_dOnClickDown = onClickDown;
			this.m_weaponSlotFx.SetSlotEquipEffectState(false);
			this.m_defenceSlotFx.SetSlotEquipEffectState(false);
			this.m_accSlotFx.SetSlotEquipEffectState(false);
			this.m_acc2SlotFx.SetSlotEquipEffectState(false);
			NKCUtil.SetGameobjectActive(this.m_objPresetSlotFx, false);
			if (presetData != null)
			{
				this.m_iPresetIndex = presetData.presetIndex;
				this.m_ePresetType = presetData.presetType;
				this.m_strPresetName = presetData.presetName;
				((Text)this.m_inputFieldPresetName.placeholder).text = this.GetDefaultSlotName();
				this.m_inputFieldPresetName.text = presetData.presetName;
				if (NKCUIEquipPresetSlot.m_iUpdatingIndex != this.m_iPresetIndex)
				{
					this.m_listUpdatedEquipPosition.Clear();
				}
				int count = presetData.equipUids.Count;
				for (int i = 0; i < count; i++)
				{
					if (presetData.equipUids[i] > 0L && this.m_listEquipUId.Count > i && this.m_listUpdatedEquipPosition.Contains((ITEM_EQUIP_POSITION)i) && !NKCUIEquipPresetSlot.m_bSavedPreset && presetData.equipUids[i] == this.m_listEquipUId[i])
					{
						this.m_listUpdatedEquipPosition.Remove((ITEM_EQUIP_POSITION)i);
					}
				}
				this.ActivateSlotEffect(true);
				this.m_listEquipUId.Clear();
				this.m_listEquipUId.AddRange(presetData.equipUids);
				int num = 4;
				for (int j = this.m_listEquipUId.Count; j < num; j++)
				{
					this.m_listEquipUId.Add(0L);
				}
				this.UpdateEquipItemSlot(this.m_listEquipUId[0], this.m_slotEquipWeapon, new NKCUISlot.OnClick(this.OnEmptyEquipWeaponSlotClick), this.m_objEquipSetFXWeapon, this.m_aniEquipSetFXWeapon, this.m_setFxLoopWeapon, ITEM_EQUIP_POSITION.IEP_WEAPON);
				this.UpdateEquipItemSlot(this.m_listEquipUId[1], this.m_slotEquipDefense, new NKCUISlot.OnClick(this.OnEmptyEquipDefSlotClick), this.m_objEquipSetFXDefence, this.m_aniEquipSetFXDefence, this.m_setFxLoopDefence, ITEM_EQUIP_POSITION.IEP_DEFENCE);
				this.UpdateEquipItemSlot(this.m_listEquipUId[2], this.m_slotEquipAcc, new NKCUISlot.OnClick(this.OnEmptyEquipAccSlotClick), this.m_objEquipSetFXAcc, this.m_aniEquipSetFXAcc, this.m_setFxLoopAcc, ITEM_EQUIP_POSITION.IEP_ACC);
				this.UpdateEquipItemSlot(this.m_listEquipUId[3], this.m_slotEquipAcc_2, new NKCUISlot.OnClick(this.OnEmptyEquipAcc2SlotClick), this.m_objEquipSetFXAcc2, this.m_aniEquipSetFXAcc2, this.m_setFxLoopAcc2, ITEM_EQUIP_POSITION.IEP_ACC2);
				if (this.m_iPresetIndex >= newPresetIndexFrom)
				{
					NKCUtil.SetGameobjectActive(this.m_objPresetSlotFx, true);
				}
				NKCUtil.SetGameobjectActive(this.m_objBasicSlot, true);
				NKCUtil.SetGameobjectActive(this.m_objAddSlot, false);
				if (!slotMoveState)
				{
					bool flag = this.IsAppliableSet(false);
					NKCUtil.SetGameobjectActive(this.m_csbtnApplyPreset, flag);
					NKCUtil.SetGameobjectActive(this.m_csbtnApplyDisabled, !flag);
					NKCUtil.SetGameobjectActive(this.m_csbtnSaveUnitEquipSet, true);
					NKCUtil.SetGameobjectActive(this.m_csbtnUp, false);
					NKCUtil.SetGameobjectActive(this.m_csbtnDown, false);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_csbtnApplyPreset, false);
					NKCUtil.SetGameobjectActive(this.m_csbtnApplyDisabled, false);
					NKCUtil.SetGameobjectActive(this.m_csbtnSaveUnitEquipSet, false);
					NKCUtil.SetGameobjectActive(this.m_csbtnUp, true);
					NKCUtil.SetGameobjectActive(this.m_csbtnDown, true);
					if (cNKCUIEquipPreset != null)
					{
						int tempPresetDataIndex = NKCEquipPresetDataManager.GetTempPresetDataIndex(this.m_presetData);
						NKCUIComStateButton csbtnUp = this.m_csbtnUp;
						if (csbtnUp != null)
						{
							csbtnUp.SetLock(tempPresetDataIndex == 0, false);
						}
						NKCUIComStateButton csbtnDown = this.m_csbtnDown;
						if (csbtnDown != null)
						{
							csbtnDown.SetLock(tempPresetDataIndex == NKCEquipPresetDataManager.ListEquipPresetData.Count - 1, false);
						}
						if (!cNKCUIEquipPreset.m_enableInterPageSlotMove)
						{
							int num2 = index % cNKCUIEquipPreset.m_maxSlotCountPerPage;
							NKCUIComStateButton csbtnUp2 = this.m_csbtnUp;
							if (csbtnUp2 != null)
							{
								csbtnUp2.SetLock(num2 == 0, false);
							}
							if (num2 == cNKCUIEquipPreset.m_maxSlotCountPerPage - 1)
							{
								NKCUIComStateButton csbtnDown2 = this.m_csbtnDown;
								if (csbtnDown2 != null)
								{
									csbtnDown2.SetLock(true, false);
								}
							}
						}
					}
					else
					{
						NKCUIComStateButton csbtnUp3 = this.m_csbtnUp;
						if (csbtnUp3 != null)
						{
							csbtnUp3.SetLock(false, false);
						}
						NKCUIComStateButton csbtnDown3 = this.m_csbtnDown;
						if (csbtnDown3 != null)
						{
							csbtnDown3.SetLock(false, false);
						}
					}
				}
				if (NKCUIEquipPresetSlot.m_iUpdatingIndex == this.m_iPresetIndex)
				{
					NKCUIEquipPresetSlot.ResetUpdateIndex();
					return;
				}
			}
			else
			{
				this.m_iPresetIndex = index;
				this.m_ePresetType = NKM_EQUIP_PRESET_TYPE.NEPT_INVLID;
				this.m_inputFieldPresetName.text = "";
				this.m_listEquipUId.Clear();
				NKCUtil.SetGameobjectActive(this.m_objBasicSlot, false);
				NKCUtil.SetGameobjectActive(this.m_objAddSlot, true);
			}
		}

		// Token: 0x06006492 RID: 25746 RVA: 0x001FE9F9 File Offset: 0x001FCBF9
		public static void ResetUpdateIndex()
		{
			NKCUIEquipPresetSlot.m_iUpdatingIndex = -1;
			NKCUIEquipPresetSlot.m_bSavedPreset = false;
			NKCUIEquipPresetSlot.m_bShowSetItemFx = false;
		}

		// Token: 0x06006493 RID: 25747 RVA: 0x001FEA10 File Offset: 0x001FCC10
		private void ActivateSlotEffect(bool activate)
		{
			if (activate)
			{
				for (int i = 0; i < this.m_listUpdatedEquipPosition.Count; i++)
				{
					switch (this.m_listUpdatedEquipPosition[i])
					{
					case ITEM_EQUIP_POSITION.IEP_WEAPON:
						this.m_weaponSlotFx.SetSlotEquipEffectState(true);
						break;
					case ITEM_EQUIP_POSITION.IEP_DEFENCE:
						this.m_defenceSlotFx.SetSlotEquipEffectState(true);
						break;
					case ITEM_EQUIP_POSITION.IEP_ACC:
						this.m_accSlotFx.SetSlotEquipEffectState(true);
						break;
					case ITEM_EQUIP_POSITION.IEP_ACC2:
						this.m_acc2SlotFx.SetSlotEquipEffectState(true);
						break;
					}
				}
			}
			this.m_listUpdatedEquipPosition.Clear();
		}

		// Token: 0x06006494 RID: 25748 RVA: 0x001FEA9C File Offset: 0x001FCC9C
		private void DeactivateAllSetItemEffect()
		{
			NKCUtil.SetGameobjectActive(this.m_objEquipSetFXWeapon, false);
			NKCUtil.SetGameobjectActive(this.m_objEquipSetFXDefence, false);
			NKCUtil.SetGameobjectActive(this.m_objEquipSetFXAcc, false);
			NKCUtil.SetGameobjectActive(this.m_objEquipSetFXAcc2, false);
		}

		// Token: 0x06006495 RID: 25749 RVA: 0x001FEAD0 File Offset: 0x001FCCD0
		private void UpdateEquipItemSlot(long equipItemUID, NKCUISlotEquipPreset slot, NKCUISlot.OnClick func, GameObject effObj, Animator effAni, Image setLoopFx, ITEM_EQUIP_POSITION equipPosition)
		{
			bool flag = false;
			bool flag2 = false;
			bool disable = false;
			NKCUtil.SetGameobjectActive(effObj, true);
			NKCUtil.SetGameobjectActive(setLoopFx.gameObject, false);
			if (equipItemUID > 0L)
			{
				NKMEquipItemData itemEquip = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(equipItemUID);
				if (itemEquip != null)
				{
					slot.Slot.SetData(NKCUISlot.SlotData.MakeEquipData(itemEquip), false, true, false, null);
					slot.Slot.SetOnClick(delegate(NKCUISlot.SlotData slotData, bool bLocked)
					{
						NKMUnitData unitData = this.m_cNKCUIEquipPreset.UnitData;
						if (unitData == null)
						{
							return;
						}
						this.DeactivateAllSetItemEffect();
						NKCUIEquipPresetSlot.m_iUpdatingIndex = this.m_iPresetIndex;
						this.m_listUpdatedEquipPosition.Clear();
						this.m_listUpdatedEquipPosition.Add(equipPosition);
						NKCPopupItemEquipBox.OpenForPresetChange(unitData.m_UnitUID, equipItemUID, equipPosition, this.m_iPresetIndex, this.m_listEquipUId, this.m_bShowFierceInfo);
					});
					flag = true;
					if (this.IsSetOptionActivated(itemEquip, equipPosition) && effAni != null)
					{
						NKMItemEquipSetOptionTemplet equipSetOptionTemplet = NKMItemManager.GetEquipSetOptionTemplet(itemEquip.m_SetOptionId);
						if (equipSetOptionTemplet != null)
						{
							flag2 = true;
							effAni.SetTrigger(equipSetOptionTemplet.m_EquipSetIconEffect);
						}
					}
					NKMUnitTempletBase cNKMUnitTempletBase = null;
					if (itemEquip.m_OwnerUnitUID > 0L)
					{
						NKMUnitData unitFromUID = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetUnitFromUID(itemEquip.m_OwnerUnitUID);
						if (unitFromUID != null)
						{
							cNKMUnitTempletBase = NKMUnitManager.GetUnitTempletBase(unitFromUID.m_UnitID);
						}
					}
					slot.SetEquipUnitSprite(NKCResourceUtility.GetOrLoadMinimapFaceIcon(cNKMUnitTempletBase, false));
				}
			}
			if (!flag2 || !NKCUIEquipPresetSlot.m_bShowSetItemFx || NKCUIEquipPresetSlot.m_iUpdatingIndex != this.m_iPresetIndex)
			{
				NKCUtil.SetGameobjectActive(effObj, false);
			}
			if (!flag)
			{
				if (this.m_listEquipUId[(int)equipPosition] > 0L)
				{
					Debug.LogWarning(string.Format("{0} EquipUid Data of Preset Index {1} not exist", equipPosition, this.m_iPresetIndex));
					this.m_listEquipUId[(int)equipPosition] = 0L;
					this.m_ePresetType = this.GetEquipPresetType();
				}
				Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_UNIT_INFO_SPRITE", "NKM_UI_UNIT_INFO_ITEM_EQUIP_SLOT_ADD", false);
				slot.Slot.SetCustomizedEmptySP(orLoadAssetResource);
				slot.SetEmpty(func);
			}
			slot.Slot.SetDisable(disable, NKCStringTable.GetString("SI_PF_FIERCE", false));
		}

		// Token: 0x06006496 RID: 25750 RVA: 0x001FECB2 File Offset: 0x001FCEB2
		private void OnEmptyEquipWeaponSlotClick(NKCUISlot.SlotData slotData, bool bLocked)
		{
			this.OnEmptyEquipSlotClick(ITEM_EQUIP_POSITION.IEP_WEAPON);
		}

		// Token: 0x06006497 RID: 25751 RVA: 0x001FECBB File Offset: 0x001FCEBB
		private void OnEmptyEquipDefSlotClick(NKCUISlot.SlotData slotData, bool bLocked)
		{
			this.OnEmptyEquipSlotClick(ITEM_EQUIP_POSITION.IEP_DEFENCE);
		}

		// Token: 0x06006498 RID: 25752 RVA: 0x001FECC4 File Offset: 0x001FCEC4
		private void OnEmptyEquipAccSlotClick(NKCUISlot.SlotData slotData, bool bLocked)
		{
			this.OnEmptyEquipSlotClick(ITEM_EQUIP_POSITION.IEP_ACC);
		}

		// Token: 0x06006499 RID: 25753 RVA: 0x001FECCD File Offset: 0x001FCECD
		private void OnEmptyEquipAcc2SlotClick(NKCUISlot.SlotData slotData, bool bLocked)
		{
			this.OnEmptyEquipSlotClick(ITEM_EQUIP_POSITION.IEP_ACC2);
		}

		// Token: 0x0600649A RID: 25754 RVA: 0x001FECD8 File Offset: 0x001FCED8
		private void OnEmptyEquipSlotClick(ITEM_EQUIP_POSITION equipPos)
		{
			NKMUnitData unitData = this.m_cNKCUIEquipPreset.UnitData;
			if (unitData == null)
			{
				return;
			}
			if (this.IsSeizedUnit(unitData))
			{
				return;
			}
			this.DeactivateAllSetItemEffect();
			NKM_UNIT_STYLE_TYPE unitStyleType = this.GetUnitStyleType(this.m_ePresetType);
			NKCUIEquipPresetSlot.m_iUpdatingIndex = this.m_iPresetIndex;
			this.m_listUpdatedEquipPosition.Clear();
			this.m_listUpdatedEquipPosition.Add(equipPos);
			NKCUtil.ChangePresetEquip(unitData.m_UnitUID, this.m_iPresetIndex, this.m_listEquipUId[(int)equipPos], this.m_listEquipUId, equipPos, unitStyleType, this.m_bShowFierceInfo, null);
		}

		// Token: 0x0600649B RID: 25755 RVA: 0x001FED60 File Offset: 0x001FCF60
		private bool IsSetOptionActivated(NKMEquipItemData cNKMEquipItemData, ITEM_EQUIP_POSITION equipPosition)
		{
			if (cNKMEquipItemData == null)
			{
				return false;
			}
			NKMItemEquipSetOptionTemplet equipSetOptionTemplet = NKMItemManager.GetEquipSetOptionTemplet(cNKMEquipItemData.m_SetOptionId);
			if (equipSetOptionTemplet == null)
			{
				return false;
			}
			if (equipSetOptionTemplet.m_EquipSetPart == 1)
			{
				return true;
			}
			List<int> list = new List<int>();
			int count = this.m_listEquipUId.Count;
			for (int i = 0; i < count; i++)
			{
				NKMEquipItemData itemEquip = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(this.m_listEquipUId[i]);
				if (itemEquip != null && itemEquip.m_SetOptionId == cNKMEquipItemData.m_SetOptionId)
				{
					list.Add(i);
				}
			}
			if (equipSetOptionTemplet.m_EquipSetPart <= list.Count)
			{
				int num = 0;
				int count2 = list.Count;
				for (int j = 0; j < count2; j++)
				{
					if (++num == equipSetOptionTemplet.m_EquipSetPart)
					{
						if (list[j] >= (int)equipPosition)
						{
							return true;
						}
						num = 0;
					}
				}
			}
			return false;
		}

		// Token: 0x0600649C RID: 25756 RVA: 0x001FEE3A File Offset: 0x001FD03A
		private NKM_UNIT_STYLE_TYPE GetUnitStyleType(NKM_EQUIP_PRESET_TYPE presetType)
		{
			switch (presetType)
			{
			case NKM_EQUIP_PRESET_TYPE.NEPT_COUNTER:
				return NKM_UNIT_STYLE_TYPE.NUST_COUNTER;
			case NKM_EQUIP_PRESET_TYPE.NEPT_SOLDIER:
				return NKM_UNIT_STYLE_TYPE.NUST_SOLDIER;
			case NKM_EQUIP_PRESET_TYPE.NEPT_MECHANIC:
				return NKM_UNIT_STYLE_TYPE.NUST_MECHANIC;
			default:
				return NKM_UNIT_STYLE_TYPE.NUST_INVALID;
			}
		}

		// Token: 0x0600649D RID: 25757 RVA: 0x001FEE59 File Offset: 0x001FD059
		private string GetDefaultSlotName()
		{
			return string.Format(NKCUtilString.GET_STRING_EQUIP_PRESET_NAME, this.m_iPresetIndex + 1);
		}

		// Token: 0x0600649E RID: 25758 RVA: 0x001FEE74 File Offset: 0x001FD074
		private NKM_EQUIP_PRESET_TYPE GetEquipPresetType()
		{
			NKM_EQUIP_PRESET_TYPE nkm_EQUIP_PRESET_TYPE = NKM_EQUIP_PRESET_TYPE.NEPT_NONE;
			int count = this.m_listEquipUId.Count;
			for (int i = 0; i < count; i++)
			{
				NKMEquipTemplet equipTempletFromEquipUId = this.GetEquipTempletFromEquipUId(this.m_listEquipUId[i]);
				if (equipTempletFromEquipUId != null)
				{
					switch (equipTempletFromEquipUId.m_EquipUnitStyleType)
					{
					case NKM_UNIT_STYLE_TYPE.NUST_COUNTER:
						nkm_EQUIP_PRESET_TYPE = NKM_EQUIP_PRESET_TYPE.NEPT_COUNTER;
						break;
					case NKM_UNIT_STYLE_TYPE.NUST_SOLDIER:
						nkm_EQUIP_PRESET_TYPE = NKM_EQUIP_PRESET_TYPE.NEPT_SOLDIER;
						break;
					case NKM_UNIT_STYLE_TYPE.NUST_MECHANIC:
						nkm_EQUIP_PRESET_TYPE = NKM_EQUIP_PRESET_TYPE.NEPT_MECHANIC;
						break;
					}
					if (nkm_EQUIP_PRESET_TYPE != NKM_EQUIP_PRESET_TYPE.NEPT_NONE)
					{
						break;
					}
				}
			}
			return nkm_EQUIP_PRESET_TYPE;
		}

		// Token: 0x0600649F RID: 25759 RVA: 0x001FEEE0 File Offset: 0x001FD0E0
		private NKMEquipTemplet GetEquipTempletFromEquipUId(long equipUId)
		{
			NKMEquipItemData itemEquip = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(equipUId);
			if (itemEquip == null)
			{
				return null;
			}
			return NKMItemManager.GetEquipTemplet(itemEquip.m_ItemEquipID);
		}

		// Token: 0x060064A0 RID: 25760 RVA: 0x001FEF13 File Offset: 0x001FD113
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

		// Token: 0x060064A1 RID: 25761 RVA: 0x001FEF38 File Offset: 0x001FD138
		private bool IsEmptyAllEquipSlot()
		{
			bool result = true;
			int count = this.m_listEquipUId.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.m_listEquipUId[i] > 0L)
				{
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x060064A2 RID: 25762 RVA: 0x001FEF74 File Offset: 0x001FD174
		private bool IsAppliableSet(bool showMsg)
		{
			NKM_UNIT_STYLE_TYPE unitStyleType = this.GetUnitStyleType(this.m_ePresetType);
			if (unitStyleType == NKM_UNIT_STYLE_TYPE.NUST_INVALID || this.IsEmptyAllEquipSlot())
			{
				if (showMsg)
				{
					NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_EQUIP_PRESET_NONE, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				}
				return false;
			}
			NKMUnitData unitData = this.m_cNKCUIEquipPreset.UnitData;
			if (unitData == null)
			{
				return false;
			}
			if (this.IsSeizedUnit(unitData))
			{
				return false;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID);
			if (unitTempletBase == null)
			{
				return false;
			}
			if (unitTempletBase.m_NKM_UNIT_STYLE_TYPE != unitStyleType)
			{
				if (showMsg)
				{
					NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_EQUIP_PRESET_DIFFERENT_TYPE, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				}
				return false;
			}
			return true;
		}

		// Token: 0x060064A3 RID: 25763 RVA: 0x001FF014 File Offset: 0x001FD214
		private void OnClickPresetAdd()
		{
			if (this.m_dOnClickAdd != null)
			{
				this.m_dOnClickAdd();
			}
		}

		// Token: 0x060064A4 RID: 25764 RVA: 0x001FF02C File Offset: 0x001FD22C
		private void OnInputNameChanged(string _string)
		{
			this.m_inputFieldPresetName.text = NKCFilterManager.CheckBadChat(this.m_inputFieldPresetName.text);
			if (this.m_inputFieldPresetName.text.Length >= NKMCommonConst.EQUIP_PRESET_NAME_MAX_LENGTH)
			{
				this.m_inputFieldPresetName.text = this.m_inputFieldPresetName.text.Substring(0, NKMCommonConst.EQUIP_PRESET_NAME_MAX_LENGTH);
			}
		}

		// Token: 0x060064A5 RID: 25765 RVA: 0x001FF08C File Offset: 0x001FD28C
		private void OnInputPresetName(string _string)
		{
			this.m_comDragScrollInputField.ActiveInput = false;
			this.m_inputFieldPresetName.enabled = false;
			this.m_inputFieldPresetName.text = NKCFilterManager.CheckBadChat(this.m_inputFieldPresetName.text);
			if (this.m_inputFieldPresetName.text == this.m_strPresetName)
			{
				if (NKCUIManager.FrontCanvas != null)
				{
					this.m_bNameChangeButtonClicked = RectTransformUtility.RectangleContainsScreenPoint(this.m_csbtnChangePresetName.GetComponent<RectTransform>(), Input.mousePosition, NKCUIManager.FrontCanvas.worldCamera);
				}
				return;
			}
			if (this.m_inputFieldPresetName.text.Length >= NKMCommonConst.EQUIP_PRESET_NAME_MAX_LENGTH)
			{
				string presetName = this.m_inputFieldPresetName.text.Substring(0, NKMCommonConst.EQUIP_PRESET_NAME_MAX_LENGTH);
				NKCPacketSender.Send_NKMPacket_EQUIP_PRESET_NAME_CHANGE_REQ(this.m_iPresetIndex, presetName);
				return;
			}
			NKCPacketSender.Send_NKMPacket_EQUIP_PRESET_NAME_CHANGE_REQ(this.m_iPresetIndex, this.m_inputFieldPresetName.text);
		}

		// Token: 0x060064A6 RID: 25766 RVA: 0x001FF16D File Offset: 0x001FD36D
		private void OnClickNameChange()
		{
			if (this.m_bNameChangeButtonClicked)
			{
				this.m_bNameChangeButtonClicked = false;
				return;
			}
			this.m_comDragScrollInputField.ActiveInput = true;
			this.m_inputFieldPresetName.enabled = true;
			this.m_inputFieldPresetName.Select();
			this.m_inputFieldPresetName.ActivateInputField();
		}

		// Token: 0x060064A7 RID: 25767 RVA: 0x001FF1B0 File Offset: 0x001FD3B0
		private void OnClickSaveUnitEquip()
		{
			NKMUnitData unitData = this.m_cNKCUIEquipPreset.UnitData;
			if (unitData == null)
			{
				return;
			}
			if (this.IsSeizedUnit(unitData))
			{
				return;
			}
			if (unitData.GetEquipItemWeaponUid() <= 0L && unitData.GetEquipItemDefenceUid() <= 0L && unitData.GetEquipItemAccessoryUid() <= 0L && unitData.GetEquipItemAccessory2Uid() <= 0L)
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_EQUIP_PRESET_UNIT_EQUIP_EMPTY, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return;
			}
			NKCPopupResourceConfirmBox.Instance.OpenForConfirm(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_EQUIP_PRESET_SAVE_CONTENT, delegate
			{
				bool flag = true;
				this.m_listUpdatedEquipPosition.Clear();
				int num = 4;
				for (int i = 0; i < num; i++)
				{
					ITEM_EQUIP_POSITION item_EQUIP_POSITION = (ITEM_EQUIP_POSITION)i;
					long equipUid = unitData.GetEquipUid(item_EQUIP_POSITION);
					if (this.m_listEquipUId.Count <= i || equipUid != this.m_listEquipUId[i])
					{
						flag = false;
					}
					if (equipUid > 0L)
					{
						this.m_listUpdatedEquipPosition.Add(item_EQUIP_POSITION);
					}
				}
				NKCUIEquipPresetSlot.m_iUpdatingIndex = this.m_iPresetIndex;
				if (flag)
				{
					NKCUIEquipPresetSlot.m_bShowSetItemFx = true;
					this.UpdateEquipItemSlot(this.m_listEquipUId[0], this.m_slotEquipWeapon, new NKCUISlot.OnClick(this.OnEmptyEquipWeaponSlotClick), this.m_objEquipSetFXWeapon, this.m_aniEquipSetFXWeapon, this.m_setFxLoopWeapon, ITEM_EQUIP_POSITION.IEP_WEAPON);
					this.UpdateEquipItemSlot(this.m_listEquipUId[1], this.m_slotEquipDefense, new NKCUISlot.OnClick(this.OnEmptyEquipDefSlotClick), this.m_objEquipSetFXDefence, this.m_aniEquipSetFXDefence, this.m_setFxLoopDefence, ITEM_EQUIP_POSITION.IEP_DEFENCE);
					this.UpdateEquipItemSlot(this.m_listEquipUId[2], this.m_slotEquipAcc, new NKCUISlot.OnClick(this.OnEmptyEquipAccSlotClick), this.m_objEquipSetFXAcc, this.m_aniEquipSetFXAcc, this.m_setFxLoopAcc, ITEM_EQUIP_POSITION.IEP_ACC);
					this.UpdateEquipItemSlot(this.m_listEquipUId[3], this.m_slotEquipAcc_2, new NKCUISlot.OnClick(this.OnEmptyEquipAcc2SlotClick), this.m_objEquipSetFXAcc2, this.m_aniEquipSetFXAcc2, this.m_setFxLoopAcc2, ITEM_EQUIP_POSITION.IEP_ACC2);
					this.ActivateSlotEffect(true);
					NKCUIEquipPresetSlot.m_bShowSetItemFx = false;
					NKCUIEquipPresetSlot.m_iUpdatingIndex = -1;
					return;
				}
				NKCPacketSender.Send_NKMPacket_EQUIP_PRESET_REGISTER_ALL_REQ(this.m_iPresetIndex, unitData.m_UnitUID);
			}, null, false);
		}

		// Token: 0x060064A8 RID: 25768 RVA: 0x001FF270 File Offset: 0x001FD470
		private void OnClickApplyPreset()
		{
			if (!this.IsAppliableSet(true))
			{
				return;
			}
			NKMUnitData unitData = this.m_cNKCUIEquipPreset.UnitData;
			if (unitData == null)
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = NKMUnitManager.IsUnitBusy(myUserData, unitData, true);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString(nkm_ERROR_CODE.ToString(), false), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			bool flag = false;
			bool flag2 = unitData.IsUnlockAccessory2();
			bool flag3 = true;
			int num = 3;
			List<long> list = new List<long>();
			list.AddRange(unitData.EquipItemUids);
			int count = this.m_listEquipUId.Count;
			for (int i = 0; i < count; i++)
			{
				long num2 = this.m_listEquipUId[i];
				if (num2 > 0L)
				{
					NKMEquipItemData itemEquip = myUserData.m_InventoryData.GetItemEquip(num2);
					if (itemEquip != null)
					{
						if (itemEquip.m_OwnerUnitUID > 0L)
						{
							NKMUnitData unitFromUID = myUserData.m_ArmyData.GetUnitFromUID(itemEquip.m_OwnerUnitUID);
							if (unitFromUID != null)
							{
								nkm_ERROR_CODE = NKMUnitManager.IsUnitBusy(myUserData, unitFromUID, true);
								if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
								{
									NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString(nkm_ERROR_CODE.ToString(), false), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
									return;
								}
							}
						}
						NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(itemEquip.m_ItemEquipID);
						if (equipTemplet != null)
						{
							if (equipTemplet.IsPrivateEquip() && !equipTemplet.IsPrivateEquipForUnit(unitData.m_UnitID))
							{
								flag = true;
							}
							else
							{
								if (list.Count > i)
								{
									list[i] = num2;
								}
								long equipUid = unitData.GetEquipUid((ITEM_EQUIP_POSITION)i);
								if ((i != num || flag2) && num2 != equipUid)
								{
									flag3 = false;
								}
							}
						}
					}
				}
			}
			if (flag)
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_EQUIP_PRESET_PRIVATE_EQUIP, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
			}
			if (!flag2 && this.m_listEquipUId.Count > num && this.m_listEquipUId[num] > 0L)
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_EQUIP_PRESET_APPLY_SLOT_LOCKED, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
			}
			if (flag3)
			{
				return;
			}
			if (NKCUtil.IsPrivateEquipAlreadyEquipped(list))
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_EQUIP_PRIVATE.ToString(), false), null, "");
				return;
			}
			NKCPacketSender.Send_NKMPacket_EQUIP_PRESET_APPLY_REQ(this.m_iPresetIndex, unitData.m_UnitUID);
		}

		// Token: 0x060064A9 RID: 25769 RVA: 0x001FF4AD File Offset: 0x001FD6AD
		private void OnClickSlotUp()
		{
			if (this.m_dOnClickUp != null)
			{
				this.m_dOnClickUp(this.m_presetData);
			}
		}

		// Token: 0x060064AA RID: 25770 RVA: 0x001FF4C8 File Offset: 0x001FD6C8
		private void OnClickSlotDown()
		{
			if (this.m_dOnClickDown != null)
			{
				this.m_dOnClickDown(this.m_presetData);
			}
		}

		// Token: 0x060064AB RID: 25771 RVA: 0x001FF4E3 File Offset: 0x001FD6E3
		private void OnDestroy()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_presetData = null;
			this.m_cNKCUIEquipPreset = null;
			this.m_listEquipUId = null;
			this.m_strPresetName = null;
			this.m_listUpdatedEquipPosition = null;
		}

		// Token: 0x0400501F RID: 20511
		public NKCUIComStateButton m_csbtnChangePresetName;

		// Token: 0x04005020 RID: 20512
		public NKCUIComStateButton m_csbtnSaveUnitEquipSet;

		// Token: 0x04005021 RID: 20513
		public NKCUIComStateButton m_csbtnApplyPreset;

		// Token: 0x04005022 RID: 20514
		public NKCUIComStateButton m_csbtnApplyDisabled;

		// Token: 0x04005023 RID: 20515
		public NKCUIComStateButton m_csbtnPresetAdd;

		// Token: 0x04005024 RID: 20516
		public InputField m_inputFieldPresetName;

		// Token: 0x04005025 RID: 20517
		public NKCUIComDragScrollInputField m_comDragScrollInputField;

		// Token: 0x04005026 RID: 20518
		public GameObject m_objBasicSlot;

		// Token: 0x04005027 RID: 20519
		public GameObject m_objAddSlot;

		// Token: 0x04005028 RID: 20520
		[Header("장비 슬롯")]
		public NKCUISlotEquipPreset m_slotEquipWeapon;

		// Token: 0x04005029 RID: 20521
		public NKCUISlotEquipPreset m_slotEquipDefense;

		// Token: 0x0400502A RID: 20522
		public NKCUISlotEquipPreset m_slotEquipAcc;

		// Token: 0x0400502B RID: 20523
		public NKCUISlotEquipPreset m_slotEquipAcc_2;

		// Token: 0x0400502C RID: 20524
		[Header("프리셋 이동 화살표 버튼")]
		public NKCUIComStateButton m_csbtnUp;

		// Token: 0x0400502D RID: 20525
		public NKCUIComStateButton m_csbtnDown;

		// Token: 0x0400502E RID: 20526
		[Header("세트아이템 이펙트")]
		public GameObject m_objEquipSetFXWeapon;

		// Token: 0x0400502F RID: 20527
		public Animator m_aniEquipSetFXWeapon;

		// Token: 0x04005030 RID: 20528
		public GameObject m_objEquipSetFXDefence;

		// Token: 0x04005031 RID: 20529
		public Animator m_aniEquipSetFXDefence;

		// Token: 0x04005032 RID: 20530
		public GameObject m_objEquipSetFXAcc;

		// Token: 0x04005033 RID: 20531
		public Animator m_aniEquipSetFXAcc;

		// Token: 0x04005034 RID: 20532
		public GameObject m_objEquipSetFXAcc2;

		// Token: 0x04005035 RID: 20533
		public Animator m_aniEquipSetFXAcc2;

		// Token: 0x04005036 RID: 20534
		[Header("아이템 장착 이펙트")]
		public NKCUIEquipPresetSlot.SlotEquipFx m_weaponSlotFx;

		// Token: 0x04005037 RID: 20535
		public NKCUIEquipPresetSlot.SlotEquipFx m_defenceSlotFx;

		// Token: 0x04005038 RID: 20536
		public NKCUIEquipPresetSlot.SlotEquipFx m_accSlotFx;

		// Token: 0x04005039 RID: 20537
		public NKCUIEquipPresetSlot.SlotEquipFx m_acc2SlotFx;

		// Token: 0x0400503A RID: 20538
		[Header("프리셋 확장 이펙트")]
		public GameObject m_objPresetSlotFx;

		// Token: 0x0400503B RID: 20539
		[Header("세트 아이템 상시 이펙트")]
		public Image m_setFxLoopWeapon;

		// Token: 0x0400503C RID: 20540
		public Image m_setFxLoopDefence;

		// Token: 0x0400503D RID: 20541
		public Image m_setFxLoopAcc;

		// Token: 0x0400503E RID: 20542
		public Image m_setFxLoopAcc2;

		// Token: 0x0400503F RID: 20543
		public Color m_setFxRed;

		// Token: 0x04005040 RID: 20544
		public Color m_setFxBlue;

		// Token: 0x04005041 RID: 20545
		public Color m_setFxYellow;

		// Token: 0x04005042 RID: 20546
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x04005043 RID: 20547
		private NKCUIEquipPreset m_cNKCUIEquipPreset;

		// Token: 0x04005044 RID: 20548
		private int m_iPresetIndex;

		// Token: 0x04005045 RID: 20549
		private NKM_EQUIP_PRESET_TYPE m_ePresetType;

		// Token: 0x04005046 RID: 20550
		private List<long> m_listEquipUId = new List<long>();

		// Token: 0x04005047 RID: 20551
		private string m_strPresetName;

		// Token: 0x04005048 RID: 20552
		private static int m_iUpdatingIndex = -1;

		// Token: 0x04005049 RID: 20553
		private static bool m_bSavedPreset = false;

		// Token: 0x0400504A RID: 20554
		private static bool m_bShowSetItemFx = false;

		// Token: 0x0400504B RID: 20555
		private bool m_bShowFierceInfo;

		// Token: 0x0400504C RID: 20556
		private bool m_bNameChangeButtonClicked;

		// Token: 0x0400504D RID: 20557
		private List<ITEM_EQUIP_POSITION> m_listUpdatedEquipPosition = new List<ITEM_EQUIP_POSITION>();

		// Token: 0x0400504E RID: 20558
		private NKMEquipPresetData m_presetData;

		// Token: 0x0400504F RID: 20559
		private NKCUIEquipPresetSlot.OnClickAdd m_dOnClickAdd;

		// Token: 0x04005050 RID: 20560
		private NKCUIEquipPresetSlot.OnClickUp m_dOnClickUp;

		// Token: 0x04005051 RID: 20561
		private NKCUIEquipPresetSlot.OnClickDown m_dOnClickDown;

		// Token: 0x02001643 RID: 5699
		[Serializable]
		public struct SlotEquipFx
		{
			// Token: 0x0600AFC9 RID: 45001 RVA: 0x0035E14F File Offset: 0x0035C34F
			public void SetSlotEquipEffectState(bool activate)
			{
				NKCUtil.SetGameobjectActive(this.m_objEquipEffect, activate);
				if (activate)
				{
					DOTweenAnimation tweenEquipFx = this.m_tweenEquipFx1;
					if (tweenEquipFx != null)
					{
						tweenEquipFx.DORestart();
					}
					DOTweenAnimation tweenEquipFx2 = this.m_tweenEquipFx2;
					if (tweenEquipFx2 == null)
					{
						return;
					}
					tweenEquipFx2.DORestart();
				}
			}

			// Token: 0x0400A3F2 RID: 41970
			public GameObject m_objEquipEffect;

			// Token: 0x0400A3F3 RID: 41971
			public DOTweenAnimation m_tweenEquipFx1;

			// Token: 0x0400A3F4 RID: 41972
			public DOTweenAnimation m_tweenEquipFx2;
		}

		// Token: 0x02001644 RID: 5700
		// (Invoke) Token: 0x0600AFCB RID: 45003
		public delegate void OnClickAdd();

		// Token: 0x02001645 RID: 5701
		// (Invoke) Token: 0x0600AFCF RID: 45007
		public delegate void OnClickUp(NKMEquipPresetData presetData);

		// Token: 0x02001646 RID: 5702
		// (Invoke) Token: 0x0600AFD3 RID: 45011
		public delegate void OnClickDown(NKMEquipPresetData presetData);
	}
}
