using System;
using System.Collections.Generic;
using ClientPacket.Unit;
using NKM;
using NKM.Contract2;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009E4 RID: 2532
	public class NKCUIShipInfoRepair : MonoBehaviour
	{
		// Token: 0x06006CDE RID: 27870 RVA: 0x00239CB8 File Offset: 0x00237EB8
		public void Init(UnityAction callback = null)
		{
			this.m_ShipLevelUp.Init(new UnityAction(this.ClickLevelMinBtn), new UnityAction(this.ClickLevelPervBtn), new UnityAction(this.ClickLevelNextBtn), new UnityAction(this.ClickLevelMaxBtn));
			this.m_ShipUpgrade.Init();
			NKCUIHangarShipyardLimitBreak shipLimitBreak = this.m_ShipLimitBreak;
			if (shipLimitBreak != null)
			{
				shipLimitBreak.Init();
			}
			NKCUtil.SetBindFunction(this.m_cbtn_NKM_UI_HANGAR_SHIPYARD_BUTTON_LevelUp, new UnityAction(this.OnClickLevelUp));
			this.m_cbtn_NKM_UI_HANGAR_SHIPYARD_BUTTON_LevelUp.m_bGetCallbackWhileLocked = true;
			NKCUtil.SetBindFunction(this.m_cbtn_NKM_UI_HANGAR_SHIPYARD_BUTTON_Upgrade, new UnityAction(this.OnClickUpgrade));
			NKCUtil.SetBindFunction(this.m_btnLimitBreak, new UnityAction(this.OnClickLimitBreak));
			this.m_btnLimitBreak.m_bGetCallbackWhileLocked = true;
			if (this.m_ToolTipHP != null)
			{
				this.m_ToolTipHP.SetType(NKM_STAT_TYPE.NST_HP, false);
			}
			if (this.m_ToolTipATK != null)
			{
				this.m_ToolTipATK.SetType(NKM_STAT_TYPE.NST_ATK, false);
			}
			if (this.m_ToolTipDEF != null)
			{
				this.m_ToolTipDEF.SetType(NKM_STAT_TYPE.NST_DEF, false);
			}
			if (this.m_ToolTipCritical != null)
			{
				this.m_ToolTipCritical.SetType(NKM_STAT_TYPE.NST_CRITICAL, false);
			}
			if (this.m_ToolTipHit != null)
			{
				this.m_ToolTipHit.SetType(NKM_STAT_TYPE.NST_HIT, false);
			}
			if (this.m_ToolTipEvade != null)
			{
				this.m_ToolTipEvade.SetType(NKM_STAT_TYPE.NST_EVADE, false);
			}
			NKCUIShipSkillSlot[] slot_NKM_UI_SHIP_INFO_SKILL_SLOT = this.m_slot_NKM_UI_SHIP_INFO_SKILL_SLOT;
			for (int i = 0; i < slot_NKM_UI_SHIP_INFO_SKILL_SLOT.Length; i++)
			{
				slot_NKM_UI_SHIP_INFO_SKILL_SLOT[i].Init(callback, false);
			}
			if (this.m_CostShipSlot != null)
			{
				this.m_CostShipSlot.Init();
				this.m_CostShipSlot.SetOnClick(new NKCUISlot.OnClick(this.OnClickCostShipSlot));
				this.m_CostShipSlot.SetEmpty(new NKCUISlot.OnClick(this.OnClickCostShipSlot));
			}
			NKCUIComStateButton btnEmptyShipSlot = this.m_btnEmptyShipSlot;
			if (btnEmptyShipSlot != null)
			{
				btnEmptyShipSlot.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton btnEmptyShipSlot2 = this.m_btnEmptyShipSlot;
			if (btnEmptyShipSlot2 != null)
			{
				btnEmptyShipSlot2.PointerClick.AddListener(delegate()
				{
					this.OnClickCostShipSlot(null, false);
				});
			}
			this.m_lstCostShipIDList = new List<int>();
		}

		// Token: 0x17001290 RID: 4752
		// (get) Token: 0x06006CDF RID: 27871 RVA: 0x00239EC4 File Offset: 0x002380C4
		public NKCUIShipInfoRepair.RepairState Status
		{
			get
			{
				if (this.m_curShipRepairInfo != null)
				{
					return this.m_curShipRepairInfo.eRepairState;
				}
				return NKCUIShipInfoRepair.RepairState.None;
			}
		}

		// Token: 0x06006CE0 RID: 27872 RVA: 0x00239EDB File Offset: 0x002380DB
		public void SetData(NKMUnitData targetShipData)
		{
			this.m_SelectedCostShipUID = 0L;
			this.m_CostShipSlot.SetEmpty(new NKCUISlot.OnClick(this.OnClickCostShipSlot));
			this.UpdateShipRepairInfo(targetShipData);
			this.UpdateUI();
		}

		// Token: 0x06006CE1 RID: 27873 RVA: 0x00239F0C File Offset: 0x0023810C
		private void UpdateSkillUI()
		{
			if (NKCUtil.IsNullObject<NKMUnitData>(this.m_curShipRepairInfo.ShipData, ""))
			{
				return;
			}
			NKMShipBuildTemplet shipBuildTemplet = NKMShipManager.GetShipBuildTemplet(this.m_curShipRepairInfo.ShipData.m_UnitID);
			if (NKCUtil.IsNullObject<NKMShipBuildTemplet>(shipBuildTemplet, string.Format("buildTemplet is null - target Unit ID{0}", this.m_curShipRepairInfo.ShipData.m_UnitID)))
			{
				return;
			}
			NKMUnitTempletBase nkmunitTempletBase = null;
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_curShipRepairInfo.ShipData.m_UnitID);
			if (NKCUtil.IsNullObject<NKMUnitTempletBase>(unitTempletBase, string.Format("ShipTemplet is null - target Unit ID{0}", this.m_curShipRepairInfo.ShipData.m_UnitID)))
			{
				return;
			}
			if (this.m_curShipRepairInfo.eRepairState == NKCUIShipInfoRepair.RepairState.Upgrade)
			{
				nkmunitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_curShipRepairInfo.ShipData.m_UnitID);
				if (NKCUtil.IsNullObject<NKMUnitTempletBase>(nkmunitTempletBase, string.Format("prevShipTemplet is null - target Unit ID{0}", this.m_curShipRepairInfo.ShipData.m_UnitID)))
				{
					return;
				}
				shipBuildTemplet = NKMShipManager.GetShipBuildTemplet(shipBuildTemplet.ShipUpgradeTarget1);
				if (NKCUtil.IsNullObject<NKMShipBuildTemplet>(shipBuildTemplet, "buildTemplet is null"))
				{
					return;
				}
				unitTempletBase = NKMUnitManager.GetUnitTempletBase(shipBuildTemplet.ShipID);
				if (NKCUtil.IsNullObject<NKMUnitTempletBase>(unitTempletBase, string.Format("ShipTemplet is null - target Unit ID{0}", this.m_curShipRepairInfo.ShipData.m_UnitID)))
				{
					return;
				}
			}
			for (int i = 0; i < this.m_slot_NKM_UI_SHIP_INFO_SKILL_SLOT.Length; i++)
			{
				NKCUIShipSkillSlot nkcuishipSkillSlot = this.m_slot_NKM_UI_SHIP_INFO_SKILL_SLOT[i];
				NKMShipSkillTemplet shipSkillTempletByIndex = NKMShipSkillManager.GetShipSkillTempletByIndex(unitTempletBase, i);
				if (NKCUtil.IsNullObject<NKMShipSkillTemplet>(shipSkillTempletByIndex, string.Format("UpdateSkill- Can not found skill templet : shipID{0}, target idx : {1}", unitTempletBase.m_UnitID, i)))
				{
					NKCUtil.SetGameobjectActive(nkcuishipSkillSlot, false);
					nkcuishipSkillSlot.Cleanup();
				}
				else
				{
					NKCUtil.SetGameobjectActive(nkcuishipSkillSlot, true);
					nkcuishipSkillSlot.SetData(shipSkillTempletByIndex, NKCUIShipSkillSlot.eShipSkillSlotStatus.NONE);
					if (this.m_curShipRepairInfo.eRepairState == NKCUIShipInfoRepair.RepairState.Upgrade && nkmunitTempletBase != null)
					{
						bool flag = false;
						if (i < nkmunitTempletBase.m_lstSkillStrID.Count && i < unitTempletBase.m_lstSkillStrID.Count)
						{
							if (!string.Equals(nkmunitTempletBase.m_lstSkillStrID[i], unitTempletBase.m_lstSkillStrID[i]))
							{
								if (this.m_slot_NKM_UI_SHIP_INFO_SKILL_SLOT[i] != null)
								{
									this.m_slot_NKM_UI_SHIP_INFO_SKILL_SLOT[i].SetText(NKCUtilString.GET_STRING_HANGAR_SHIPYARD_SKILL_UPGRADE);
								}
								flag = true;
							}
							if (this.m_slot_NKM_UI_SHIP_INFO_SKILL_SLOT[i] != null)
							{
								this.m_slot_NKM_UI_SHIP_INFO_SKILL_SLOT[i].SetStatus(NKCUIShipSkillSlot.eShipSkillSlotStatus.NONE);
							}
						}
						else if (i < unitTempletBase.m_lstSkillStrID.Count)
						{
							if (this.m_slot_NKM_UI_SHIP_INFO_SKILL_SLOT[i] != null)
							{
								this.m_slot_NKM_UI_SHIP_INFO_SKILL_SLOT[i].SetText(NKCUtilString.GET_STRING_HANGAR_SHIPYARD_SKILL_NEW);
								this.m_slot_NKM_UI_SHIP_INFO_SKILL_SLOT[i].SetStatus(NKCUIShipSkillSlot.eShipSkillSlotStatus.NEW_GET_POPUP);
							}
							flag = true;
						}
						if (this.m_slot_NKM_UI_SHIP_INFO_SKILL_SLOT[i] != null)
						{
							if (flag)
							{
								this.m_slot_NKM_UI_SHIP_INFO_SKILL_SLOT[i].SetStatus(NKCUIShipSkillSlot.eShipSkillSlotStatus.NEW_GET_POPUP);
							}
							else
							{
								this.m_slot_NKM_UI_SHIP_INFO_SKILL_SLOT[i].SetStatus(NKCUIShipSkillSlot.eShipSkillSlotStatus.NONE);
							}
						}
					}
					else
					{
						this.m_slot_NKM_UI_SHIP_INFO_SKILL_SLOT[i].SetStatus(NKCUIShipSkillSlot.eShipSkillSlotStatus.NONE);
					}
				}
			}
		}

		// Token: 0x06006CE2 RID: 27874 RVA: 0x0023A1E0 File Offset: 0x002383E0
		private void UpdateSubUI()
		{
			if (this.m_curShipRepairInfo.eRepairState == NKCUIShipInfoRepair.RepairState.LevelUp)
			{
				this.m_ShipLevelUp.UpdateShipData(this.m_curShipRepairInfo.ShipData.m_UnitID, this.m_curShipRepairInfo.ShipData.GetStarGrade(), this.m_curShipRepairInfo.iCurShipLevel, this.m_curShipRepairInfo.iMinimumLevel, this.m_curShipRepairInfo.iMaximumLevel, this.m_curShipRepairInfo.iTargetLevel, this.m_curShipRepairInfo.bCanTryLevelUp, (int)this.m_curShipRepairInfo.ShipData.m_LimitBreakLevel);
				return;
			}
			if (this.m_curShipRepairInfo.eRepairState == NKCUIShipInfoRepair.RepairState.Upgrade)
			{
				this.m_ShipUpgrade.UpdateShipData(this.m_curShipRepairInfo);
				return;
			}
			if (this.m_curShipRepairInfo.eRepairState == NKCUIShipInfoRepair.RepairState.LimitBreak)
			{
				NKCUIHangarShipyardLimitBreak shipLimitBreak = this.m_ShipLimitBreak;
				if (shipLimitBreak == null)
				{
					return;
				}
				shipLimitBreak.UpdateShipData(this.m_curShipRepairInfo);
			}
		}

		// Token: 0x06006CE3 RID: 27875 RVA: 0x0023A2B4 File Offset: 0x002384B4
		private void UpdateStatUI()
		{
			NKCUtil.SetGameobjectActive(this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_PLUS_ATTACK_TEXT.gameObject, this.m_curShipRepairInfo.eRepairState == NKCUIShipInfoRepair.RepairState.LevelUp);
			NKCUtil.SetGameobjectActive(this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_PLUS_HP_TEXT.gameObject, this.m_curShipRepairInfo.eRepairState == NKCUIShipInfoRepair.RepairState.LevelUp);
			NKCUtil.SetGameobjectActive(this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_PLUS_DEFENCE_TEXT.gameObject, this.m_curShipRepairInfo.eRepairState == NKCUIShipInfoRepair.RepairState.LevelUp);
			NKCUtil.SetGameobjectActive(this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_PLUS_CRITICAL_TEXT.gameObject, this.m_curShipRepairInfo.eRepairState == NKCUIShipInfoRepair.RepairState.LevelUp);
			NKCUtil.SetGameobjectActive(this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_PLUS_HIT_TEXT.gameObject, this.m_curShipRepairInfo.eRepairState == NKCUIShipInfoRepair.RepairState.LevelUp);
			NKCUtil.SetGameobjectActive(this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_PLUS_EVADE_TEXT.gameObject, this.m_curShipRepairInfo.eRepairState == NKCUIShipInfoRepair.RepairState.LevelUp);
			if (this.m_curShipRepairInfo.eRepairState == NKCUIShipInfoRepair.RepairState.LevelUp)
			{
				this.m_txt_NKM_UI_HANGAR_SHIPYARD_STAT_POWER_TEXT.text = this.m_curShipRepairInfo.ShipData.CalculateOperationPower(NKCScenManager.CurrentUserData().m_InventoryData, this.m_curShipRepairInfo.iTargetLevel, null, null).ToString();
				this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_ATTACK_TEXT.text = string.Format("{0}", Mathf.RoundToInt(this.m_curShipRepairInfo.fNextShipAtk));
				this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_PLUS_ATTACK_TEXT.text = string.Format("(+{0})", Mathf.RoundToInt(this.m_curShipRepairInfo.fNextShipAtk) - Mathf.RoundToInt(this.m_curShipRepairInfo.fCurShipAtk));
				this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_HP_TEXT.text = string.Format("{0}", Mathf.RoundToInt(this.m_curShipRepairInfo.fNextShipHP));
				this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_PLUS_HP_TEXT.text = string.Format("(+{0})", Mathf.RoundToInt(this.m_curShipRepairInfo.fNextShipHP) - Mathf.RoundToInt(this.m_curShipRepairInfo.fCurShipHP));
				this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_DEFENCE_TEXT.text = string.Format("{0}", Mathf.RoundToInt(this.m_curShipRepairInfo.fNextShipDef));
				this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_PLUS_DEFENCE_TEXT.text = string.Format("(+{0})", Mathf.RoundToInt(this.m_curShipRepairInfo.fNextShipDef) - Mathf.RoundToInt(this.m_curShipRepairInfo.fCurShipDef));
				this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_CRITICAL_TEXT.text = string.Format("{0}", Mathf.RoundToInt(this.m_curShipRepairInfo.fNextShipCritical));
				this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_PLUS_CRITICAL_TEXT.text = string.Format("(+{0})", Mathf.RoundToInt(this.m_curShipRepairInfo.fNextShipCritical) - Mathf.RoundToInt(this.m_curShipRepairInfo.fCurShipCritical));
				this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_HIT_TEXT.text = string.Format("{0}", Mathf.RoundToInt(this.m_curShipRepairInfo.fNextShipHit));
				this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_PLUS_HIT_TEXT.text = string.Format("(+{0})", Mathf.RoundToInt(this.m_curShipRepairInfo.fNextShipHit) - Mathf.RoundToInt(this.m_curShipRepairInfo.fCurShipHit));
				this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_EVADE_TEXT.text = string.Format("{0}", Mathf.RoundToInt(this.m_curShipRepairInfo.fNextShipEvade));
				this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_PLUS_EVADE_TEXT.text = string.Format("(+{0})", Mathf.RoundToInt(this.m_curShipRepairInfo.fNextShipEvade) - Mathf.RoundToInt(this.m_curShipRepairInfo.fCurShipEvade));
				return;
			}
			this.m_txt_NKM_UI_HANGAR_SHIPYARD_STAT_POWER_TEXT.text = this.m_curShipRepairInfo.ShipData.CalculateOperationPower(NKCScenManager.CurrentUserData().m_InventoryData, this.m_curShipRepairInfo.iCurShipLevel, null, null).ToString();
			this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_HP_TEXT.text = string.Format("{0}", Mathf.RoundToInt(this.m_curShipRepairInfo.fCurShipHP));
			this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_ATTACK_TEXT.text = string.Format("{0}", Mathf.RoundToInt(this.m_curShipRepairInfo.fCurShipAtk));
			this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_DEFENCE_TEXT.text = string.Format("{0}", Mathf.RoundToInt(this.m_curShipRepairInfo.fCurShipDef));
			this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_CRITICAL_TEXT.text = string.Format("{0}", Mathf.RoundToInt(this.m_curShipRepairInfo.fCurShipCritical));
			this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_HIT_TEXT.text = string.Format("{0}", Mathf.RoundToInt(this.m_curShipRepairInfo.fCurShipHit));
			this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_EVADE_TEXT.text = string.Format("{0}", Mathf.RoundToInt(this.m_curShipRepairInfo.fCurShipEvade));
			this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_PLUS_ATTACK_TEXT.text = "";
			this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_PLUS_HP_TEXT.text = "";
			this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_PLUS_DEFENCE_TEXT.text = "";
			this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_PLUS_CRITICAL_TEXT.text = "";
			this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_PLUS_HIT_TEXT.text = "";
			this.m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_PLUS_EVADE_TEXT.text = "";
		}

		// Token: 0x06006CE4 RID: 27876 RVA: 0x0023A7B8 File Offset: 0x002389B8
		private void CheckCanRepair()
		{
			if (NKCUtil.IsNullObject<NKMUnitData>(this.m_curShipRepairInfo.ShipData, ""))
			{
				return;
			}
			this.m_curShipRepairInfo.bPossibleLevelUp = (this.m_curShipRepairInfo.eRepairState == NKCUIShipInfoRepair.RepairState.LevelUp && this.CanShipLevelUp(this.m_curShipRepairInfo) == NKM_ERROR_CODE.NEC_OK);
			this.m_curShipRepairInfo.bPossibleUpgrade = (this.m_curShipRepairInfo.eRepairState == NKCUIShipInfoRepair.RepairState.Upgrade && this.CanShipUpgrade(this.m_curShipRepairInfo) == NKM_ERROR_CODE.NEC_OK);
			this.m_curShipRepairInfo.bPossibleLimitBreak = (this.m_curShipRepairInfo.eRepairState == NKCUIShipInfoRepair.RepairState.LimitBreak && this.CanShipLimitBreak(this.m_curShipRepairInfo) == NKM_ERROR_CODE.NEC_OK);
		}

		// Token: 0x06006CE5 RID: 27877 RVA: 0x0023A860 File Offset: 0x00238A60
		private void UpdateButtonUI()
		{
			if (this.m_curShipRepairInfo.eRepairState == NKCUIShipInfoRepair.RepairState.LevelUp)
			{
				if (this.m_curShipRepairInfo.bPossibleLevelUp)
				{
					NKCUtil.SetGameobjectActive(this.m_cbtn_NKM_UI_HANGAR_SHIPYARD_BUTTON_Upgrade.gameObject, false);
					NKCUtil.SetGameobjectActive(this.m_cbtn_NKM_UI_HANGAR_SHIPYARD_BUTTON_LevelUp.gameObject, true);
					NKCUtil.SetGameobjectActive(this.m_btnLimitBreak, false);
					this.m_NKM_UI_HANGAR_UNIT_INFO_BUTTON_LevelUp_BG_DISABLE.SetActive(false);
					this.m_NKM_UI_HANGAR_UNIT_INFO_BUTTON_LevelUp_BG_LIGHT.SetActive(true);
					this.m_txt_NKM_UI_HANGAR_UNIT_INFO_BUTTON_LevelUp_BG_TEXT.color = NKCUtil.GetButtonUIColor(true);
					this.m_img_NKM_UI_HANGAR_UNIT_INFO_BUTTON_LevelUp_BG_ICON.color = NKCUtil.GetButtonUIColor(true);
					this.m_cbtn_NKM_UI_HANGAR_SHIPYARD_BUTTON_LevelUp.UnLock(false);
					return;
				}
				this.m_cbtn_NKM_UI_HANGAR_SHIPYARD_BUTTON_LevelUp.Lock(false);
				NKCUtil.SetGameobjectActive(this.m_cbtn_NKM_UI_HANGAR_SHIPYARD_BUTTON_Upgrade.gameObject, false);
				NKCUtil.SetGameobjectActive(this.m_cbtn_NKM_UI_HANGAR_SHIPYARD_BUTTON_LevelUp.gameObject, true);
				NKCUtil.SetGameobjectActive(this.m_btnLimitBreak, false);
				this.m_NKM_UI_HANGAR_UNIT_INFO_BUTTON_LevelUp_BG_DISABLE.SetActive(false);
				this.m_NKM_UI_HANGAR_UNIT_INFO_BUTTON_LevelUp_BG_LIGHT.SetActive(false);
				this.m_txt_NKM_UI_HANGAR_UNIT_INFO_BUTTON_LevelUp_BG_TEXT.color = NKCUtil.GetButtonUIColor(false);
				this.m_img_NKM_UI_HANGAR_UNIT_INFO_BUTTON_LevelUp_BG_ICON.color = NKCUtil.GetButtonUIColor(false);
				return;
			}
			else
			{
				if (this.m_curShipRepairInfo.eRepairState != NKCUIShipInfoRepair.RepairState.Upgrade)
				{
					if (this.m_curShipRepairInfo.eRepairState == NKCUIShipInfoRepair.RepairState.LimitBreak)
					{
						if (this.m_curShipRepairInfo.bPossibleLimitBreak && !this.m_CostShipSlot.IsEmpty() && NKMShipManager.CanUseForShipLimitBreakMaterial(this.m_curShipRepairInfo.ShipData, this.m_CostShipSlot.GetSlotData().ID))
						{
							NKCUtil.SetGameobjectActive(this.m_cbtn_NKM_UI_HANGAR_SHIPYARD_BUTTON_Upgrade.gameObject, false);
							NKCUtil.SetGameobjectActive(this.m_cbtn_NKM_UI_HANGAR_SHIPYARD_BUTTON_LevelUp.gameObject, false);
							NKCUtil.SetGameobjectActive(this.m_btnLimitBreak, true);
							this.m_btnLimitBreak.UnLock(false);
							return;
						}
						NKCUtil.SetGameobjectActive(this.m_cbtn_NKM_UI_HANGAR_SHIPYARD_BUTTON_Upgrade.gameObject, false);
						NKCUtil.SetGameobjectActive(this.m_cbtn_NKM_UI_HANGAR_SHIPYARD_BUTTON_LevelUp.gameObject, false);
						NKCUtil.SetGameobjectActive(this.m_btnLimitBreak, true);
						this.m_btnLimitBreak.Lock(false);
					}
					return;
				}
				if (this.m_curShipRepairInfo.bPossibleUpgrade)
				{
					NKCUtil.SetGameobjectActive(this.m_cbtn_NKM_UI_HANGAR_SHIPYARD_BUTTON_Upgrade.gameObject, true);
					NKCUtil.SetGameobjectActive(this.m_cbtn_NKM_UI_HANGAR_SHIPYARD_BUTTON_LevelUp.gameObject, false);
					NKCUtil.SetGameobjectActive(this.m_btnLimitBreak, false);
					this.m_NKM_UI_HANGAR_SHIPYARD_BUTTON_Upgrade_BG_DISABLE.SetActive(false);
					this.m_NKM_UI_HANGAR_SHIPYARD_BUTTON_Upgrade_BG_LIGHT.SetActive(true);
					this.m_NKM_UI_HANGAR_SHIPYARD_BUTTON_Upgrade_BG_TEXT.color = NKCUtil.GetButtonUIColor(true);
					this.m_NKM_UI_HANGAR_SHIPYARD_BUTTON_Upgrade_BG_ICON.color = NKCUtil.GetButtonUIColor(true);
					return;
				}
				NKCUtil.SetGameobjectActive(this.m_cbtn_NKM_UI_HANGAR_SHIPYARD_BUTTON_Upgrade.gameObject, true);
				NKCUtil.SetGameobjectActive(this.m_cbtn_NKM_UI_HANGAR_SHIPYARD_BUTTON_LevelUp.gameObject, false);
				NKCUtil.SetGameobjectActive(this.m_btnLimitBreak, false);
				this.m_NKM_UI_HANGAR_SHIPYARD_BUTTON_Upgrade_BG_LIGHT.SetActive(false);
				this.m_NKM_UI_HANGAR_SHIPYARD_BUTTON_Upgrade_BG_DISABLE.SetActive(true);
				this.m_NKM_UI_HANGAR_SHIPYARD_BUTTON_Upgrade_BG_TEXT.color = NKCUtil.GetButtonUIColor(false);
				this.m_NKM_UI_HANGAR_SHIPYARD_BUTTON_Upgrade_BG_ICON.color = NKCUtil.GetButtonUIColor(false);
				return;
			}
		}

		// Token: 0x06006CE6 RID: 27878 RVA: 0x0023AB28 File Offset: 0x00238D28
		private void UpdateMaterialSlotUI()
		{
			NKCUtil.SetGameobjectActive(this.m_CostSlot[0], this.m_curShipRepairInfo.iNeedCredit > 0);
			if (this.m_curShipRepairInfo.iNeedCredit > 0)
			{
				this.m_CostSlot[0].SetData(1, this.m_curShipRepairInfo.iNeedCredit, NKCScenManager.CurrentUserData().GetCredit(), true, true, false);
			}
			if (NKMOpenTagManager.IsOpened("SHIP_LIMITBREAK"))
			{
				bool flag = false;
				NKMShipLimitBreakTemplet shipLimitBreakTemplet = NKMShipManager.GetShipLimitBreakTemplet(this.m_curShipRepairInfo.ShipData.m_UnitID, (int)(this.m_curShipRepairInfo.ShipData.m_LimitBreakLevel + 1));
				if (this.m_curShipRepairInfo.iCurShipLevel == this.m_curShipRepairInfo.iCurShipMaxLevel && this.m_curShipRepairInfo.iCurShipLevel >= 100 && shipLimitBreakTemplet != null)
				{
					flag = true;
				}
				if (flag)
				{
					NKCUtil.SetLabelText(this.m_lbCostName, NKCUtilString.GET_STRING_HANGAR_UPGRADE_COST_2);
					NKCUtil.SetGameobjectActive(this.m_objCostShip, true);
					if (this.m_CostShipSlot != null)
					{
						if (this.m_SelectedCostShipUID > 0L)
						{
							NKCUtil.SetGameobjectActive(this.m_CostShipSlot, true);
							if (this.m_CostShipSlot.GetSlotData().UID != this.m_SelectedCostShipUID)
							{
								NKCUISlot.SlotData slotData = new NKCUISlot.SlotData();
								slotData.UID = this.m_SelectedCostShipUID;
								slotData.ID = NKCScenManager.CurrentUserData().m_ArmyData.GetShipFromUID(this.m_SelectedCostShipUID).m_UnitID;
								slotData.Count = 1L;
								slotData.eType = NKCUISlot.eSlotMode.Unit;
							}
						}
						else
						{
							NKCUtil.SetGameobjectActive(this.m_CostShipSlot, false);
						}
					}
					this.m_lstCostShipIDList = shipLimitBreakTemplet.ListMaterialShipId;
				}
				else
				{
					NKCUtil.SetLabelText(this.m_lbCostName, NKCUtilString.GET_STRING_HANGAR_UPGRADE_COST);
					NKCUtil.SetGameobjectActive(this.m_objCostShip, false);
					NKCUtil.SetGameobjectActive(this.m_CostShipSlot, false);
				}
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbCostName, NKCUtilString.GET_STRING_HANGAR_UPGRADE_COST);
				NKCUtil.SetGameobjectActive(this.m_objCostShip, false);
				NKCUtil.SetGameobjectActive(this.m_CostShipSlot, false);
			}
			Dictionary<int, int>.Enumerator enumerator = this.m_curShipRepairInfo.dicMaterialList.GetEnumerator();
			for (int i = 1; i < this.m_CostSlot.Length; i++)
			{
				NKCUIItemCostSlot nkcuiitemCostSlot = this.m_CostSlot[i];
				bool flag2 = enumerator.MoveNext();
				NKCUtil.SetGameobjectActive(nkcuiitemCostSlot, flag2);
				if (flag2)
				{
					NKMInventoryData inventoryData = NKCScenManager.CurrentUserData().m_InventoryData;
					KeyValuePair<int, int> keyValuePair = enumerator.Current;
					long countMiscItem = inventoryData.GetCountMiscItem(keyValuePair.Key);
					NKCUIItemCostSlot nkcuiitemCostSlot2 = nkcuiitemCostSlot;
					keyValuePair = enumerator.Current;
					int key = keyValuePair.Key;
					keyValuePair = enumerator.Current;
					nkcuiitemCostSlot2.SetData(key, keyValuePair.Value, countMiscItem, true, true, false);
				}
				else
				{
					nkcuiitemCostSlot.SetData(0, 0, 0L, true, true, false);
				}
			}
		}

		// Token: 0x06006CE7 RID: 27879 RVA: 0x0023ADA0 File Offset: 0x00238FA0
		private NKM_ERROR_CODE CanShipLevelUp(NKCUIShipInfoRepair.ShipRepairInfo repairData)
		{
			if (repairData.ShipData.m_UnitLevel == repairData.iCurShipMaxLevel)
			{
				return NKM_ERROR_CODE.NEC_FAIL_SHIP_MAX_LEVEL;
			}
			if (NKCUtil.IsNullObject<NKMUnitData>(repairData.ShipData, ""))
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_NOT_EXIST;
			}
			if (repairData.ShipData.IsSeized)
			{
				return NKM_ERROR_CODE.NEC_FAIL_SHIP_IS_SEIZED;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKMDeckData deckDataByShipUID = nkmuserData.m_ArmyData.GetDeckDataByShipUID(repairData.ShipData.m_UnitUID);
			if (!NKCUtil.IsNullObject<NKMDeckData>(deckDataByShipUID, ""))
			{
				NKM_DECK_STATE state = deckDataByShipUID.GetState();
				if (state == NKM_DECK_STATE.DECK_STATE_WARFARE)
				{
					return NKM_ERROR_CODE.NEC_FAIL_WARFARE_DOING;
				}
				if (state == NKM_DECK_STATE.DECK_STATE_DIVE)
				{
					return NKM_ERROR_CODE.NEC_FAIL_DIVE_DOING;
				}
			}
			return NKMShipManager.CanShipLevelup(nkmuserData, repairData.ShipData, repairData.iTargetLevel);
		}

		// Token: 0x06006CE8 RID: 27880 RVA: 0x0023AE4C File Offset: 0x0023904C
		private NKM_ERROR_CODE CanShipUpgrade(NKCUIShipInfoRepair.ShipRepairInfo repairData)
		{
			if (NKCUtil.IsNullObject<NKMUnitData>(repairData.ShipData, ""))
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_NOT_EXIST;
			}
			if (repairData.ShipData.IsSeized)
			{
				return NKM_ERROR_CODE.NEC_FAIL_SHIP_IS_SEIZED;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKMDeckData deckDataByShipUID = nkmuserData.m_ArmyData.GetDeckDataByShipUID(repairData.ShipData.m_UnitUID);
			if (!NKCUtil.IsNullObject<NKMDeckData>(deckDataByShipUID, ""))
			{
				NKM_DECK_STATE state = deckDataByShipUID.GetState();
				if (state == NKM_DECK_STATE.DECK_STATE_WARFARE)
				{
					return NKM_ERROR_CODE.NEC_FAIL_WARFARE_DOING;
				}
				if (state == NKM_DECK_STATE.DECK_STATE_DIVE)
				{
					return NKM_ERROR_CODE.NEC_FAIL_DIVE_DOING;
				}
			}
			return NKMShipManager.CanShipUpgrade(nkmuserData, repairData.ShipData, repairData.iNextShipID);
		}

		// Token: 0x06006CE9 RID: 27881 RVA: 0x0023AEE0 File Offset: 0x002390E0
		private NKM_ERROR_CODE CanShipLimitBreak(NKCUIShipInfoRepair.ShipRepairInfo repairData)
		{
			if (NKCUtil.IsNullObject<NKMUnitData>(repairData.ShipData, ""))
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_NOT_EXIST;
			}
			if (repairData.ShipData.IsSeized)
			{
				return NKM_ERROR_CODE.NEC_FAIL_SHIP_IS_SEIZED;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKMDeckData deckDataByShipUID = nkmuserData.m_ArmyData.GetDeckDataByShipUID(repairData.ShipData.m_UnitUID);
			if (!NKCUtil.IsNullObject<NKMDeckData>(deckDataByShipUID, ""))
			{
				NKM_DECK_STATE state = deckDataByShipUID.GetState();
				if (state == NKM_DECK_STATE.DECK_STATE_WARFARE)
				{
					return NKM_ERROR_CODE.NEC_FAIL_WARFARE_DOING;
				}
				if (state == NKM_DECK_STATE.DECK_STATE_DIVE)
				{
					return NKM_ERROR_CODE.NEC_FAIL_DIVE_DOING;
				}
			}
			return NKMShipManager.CanShipLimitBreak(nkmuserData, repairData.ShipData, this.m_SelectedCostShipUID);
		}

		// Token: 0x06006CEA RID: 27882 RVA: 0x0023AF74 File Offset: 0x00239174
		private void UpdateUI()
		{
			if (this.m_curShipRepairInfo != null)
			{
				this.m_curShipRepairInfo.UpdateShipData();
			}
			this.UpdateSkillUI();
			this.UpdateStatUI();
			this.UpdateSubUI();
			this.UpdateButtonUI();
			this.UpdateMaterialSlotUI();
			NKCUtil.SetGameobjectActive(this.m_ShipLevelUp, this.m_curShipRepairInfo.eRepairState == NKCUIShipInfoRepair.RepairState.LevelUp);
			NKCUtil.SetGameobjectActive(this.m_ShipUpgrade, this.m_curShipRepairInfo.eRepairState == NKCUIShipInfoRepair.RepairState.Upgrade);
			NKCUtil.SetGameobjectActive(this.m_ShipLimitBreak, this.m_curShipRepairInfo.eRepairState == NKCUIShipInfoRepair.RepairState.LimitBreak);
		}

		// Token: 0x06006CEB RID: 27883 RVA: 0x0023AFFD File Offset: 0x002391FD
		private void UpdateShipRepairInfo(NKMUnitData targetShip)
		{
			if (targetShip == null)
			{
				return;
			}
			this.m_curShipRepairInfo = new NKCUIShipInfoRepair.ShipRepairInfo(targetShip.m_UnitUID);
			this.CheckCanRepair();
		}

		// Token: 0x06006CEC RID: 27884 RVA: 0x0023B01A File Offset: 0x0023921A
		private void ClickLevelMinBtn()
		{
			if (!this.IsMinimumLevel())
			{
				this.m_curShipRepairInfo.iTargetLevel = this.m_curShipRepairInfo.iMinimumLevel;
				this.UpdateUI();
			}
		}

		// Token: 0x06006CED RID: 27885 RVA: 0x0023B040 File Offset: 0x00239240
		private void ClickLevelPervBtn()
		{
			if (!this.IsMinimumLevel())
			{
				this.m_curShipRepairInfo.iTargetLevel--;
				this.UpdateUI();
			}
		}

		// Token: 0x06006CEE RID: 27886 RVA: 0x0023B063 File Offset: 0x00239263
		private void ClickLevelNextBtn()
		{
			if (!this.IsMaximumLevel())
			{
				this.m_curShipRepairInfo.iTargetLevel++;
				this.UpdateUI();
			}
		}

		// Token: 0x06006CEF RID: 27887 RVA: 0x0023B086 File Offset: 0x00239286
		private void ClickLevelMaxBtn()
		{
			if (!this.IsMaximumLevel())
			{
				this.m_curShipRepairInfo.iTargetLevel = this.m_curShipRepairInfo.iMaximumLevel;
				this.UpdateUI();
				this.m_ShipLevelUp.OnClickMaximumButton();
			}
		}

		// Token: 0x06006CF0 RID: 27888 RVA: 0x0023B0B7 File Offset: 0x002392B7
		private bool IsMinimumLevel()
		{
			return this.m_curShipRepairInfo.iMinimumLevel >= this.m_curShipRepairInfo.iTargetLevel;
		}

		// Token: 0x06006CF1 RID: 27889 RVA: 0x0023B0D4 File Offset: 0x002392D4
		private bool IsMaximumLevel()
		{
			return this.m_curShipRepairInfo.iTargetLevel == this.m_curShipRepairInfo.iMaximumLevel || !this.m_curShipRepairInfo.bCanTryLevelUp || this.m_curShipRepairInfo.iTargetLevel > this.m_curShipRepairInfo.GetMaximumLevel();
		}

		// Token: 0x06006CF2 RID: 27890 RVA: 0x0023B124 File Offset: 0x00239324
		private void OnClickLevelUp()
		{
			if (this.m_curShipRepairInfo.ShipData == null)
			{
				return;
			}
			if (this.m_curShipRepairInfo.bPossibleLevelUp)
			{
				NKCUIHangarShipyardPopup.Instance.Open(this.m_curShipRepairInfo, new UnityAction(this.tryShipLevelup), new UnityAction(this.tryShipUpgrade), new UnityAction(this.tryShipLimitBreak), null);
				return;
			}
			NKMUnitData shipFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetShipFromUID(this.m_curShipRepairInfo.ShipData.m_UnitUID);
			if (shipFromUID != null)
			{
				if (NKM_ERROR_CODE.NEC_FAIL_INVALID_ITEM_ID == NKMShipManager.CanShipLevelup(NKCScenManager.CurrentUserData(), shipFromUID, this.m_curShipRepairInfo.iTargetLevel))
				{
					foreach (KeyValuePair<int, int> keyValuePair in NKMShipManager.GetMaterialListInLevelup(shipFromUID.m_UnitID, shipFromUID.m_UnitLevel, this.m_curShipRepairInfo.iTargetLevel, 0))
					{
						if (NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(keyValuePair.Key) < (long)keyValuePair.Value)
						{
							NKCShopManager.OpenItemLackPopup(keyValuePair.Key, keyValuePair.Value);
							return;
						}
					}
				}
				NKM_ERROR_CODE nkm_ERROR_CODE = this.CanShipLevelUp(this.m_curShipRepairInfo);
				if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
				{
					NKCPopupMessageManager.AddPopupMessage(nkm_ERROR_CODE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
			}
		}

		// Token: 0x06006CF3 RID: 27891 RVA: 0x0023B270 File Offset: 0x00239470
		private void OnClickUpgrade()
		{
			if (this.m_curShipRepairInfo.ShipData == null)
			{
				return;
			}
			if (this.m_curShipRepairInfo.bPossibleUpgrade)
			{
				NKCUIHangarShipyardPopup.Instance.Open(this.m_curShipRepairInfo, new UnityAction(this.tryShipLevelup), new UnityAction(this.tryShipUpgrade), new UnityAction(this.tryShipLimitBreak), null);
				return;
			}
			NKMUnitData shipFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetShipFromUID(this.m_curShipRepairInfo.ShipData.m_UnitUID);
			if (shipFromUID == null)
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			if (NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_ITEM == NKMShipManager.CanShipUpgrade(nkmuserData, shipFromUID, this.m_curShipRepairInfo.iNextShipID))
			{
				NKMShipBuildTemplet shipBuildTemplet = NKMShipManager.GetShipBuildTemplet(this.m_curShipRepairInfo.iNextShipID);
				NKMInventoryData inventoryData = NKCScenManager.CurrentUserData().m_InventoryData;
				foreach (UpgradeMaterial upgradeMaterial in shipBuildTemplet.UpgradeMaterialList)
				{
					if (inventoryData.GetCountMiscItem(upgradeMaterial.m_ShipUpgradeMaterial) < (long)upgradeMaterial.m_ShipUpgradeMaterialCount)
					{
						NKCShopManager.OpenItemLackPopup(upgradeMaterial.m_ShipUpgradeMaterial, upgradeMaterial.m_ShipUpgradeMaterialCount);
						return;
					}
				}
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = this.CanShipUpgrade(this.m_curShipRepairInfo);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupMessageManager.AddPopupMessage(nkm_ERROR_CODE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
		}

		// Token: 0x06006CF4 RID: 27892 RVA: 0x0023B3BC File Offset: 0x002395BC
		private void OnClickLimitBreak()
		{
			if (this.m_curShipRepairInfo.ShipData == null)
			{
				return;
			}
			if (this.m_curShipRepairInfo.bPossibleLimitBreak)
			{
				NKCUIHangarShipyardPopup.Instance.Open(this.m_curShipRepairInfo, new UnityAction(this.tryShipLevelup), new UnityAction(this.tryShipUpgrade), new UnityAction(this.tryShipLimitBreak), this.m_CostShipSlot.GetSlotData());
				return;
			}
			if (this.m_CostShipSlot.IsEmpty())
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_SHIP_LIMITBREAK_NOT_CHOICE_SHIP, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			NKMUnitData shipFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetShipFromUID(this.m_curShipRepairInfo.ShipData.m_UnitUID);
			if (shipFromUID == null)
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			if (NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_ITEM == NKMShipManager.CanShipLimitBreak(nkmuserData, shipFromUID, this.m_SelectedCostShipUID))
			{
				NKMShipLimitBreakTemplet shipLimitBreakTemplet = NKMShipManager.GetShipLimitBreakTemplet(this.m_curShipRepairInfo.ShipData.m_UnitID, (int)(this.m_curShipRepairInfo.ShipData.m_LimitBreakLevel + 1));
				if (shipLimitBreakTemplet != null)
				{
					NKMInventoryData inventoryData = NKCScenManager.CurrentUserData().m_InventoryData;
					foreach (MiscItemUnit miscItemUnit in shipLimitBreakTemplet.ShipLimitBreakItems)
					{
						if (inventoryData.GetCountMiscItem(miscItemUnit.ItemId) < miscItemUnit.Count)
						{
							NKCShopManager.OpenItemLackPopup(miscItemUnit.ItemId, miscItemUnit.Count32);
							return;
						}
					}
				}
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = this.CanShipLimitBreak(this.m_curShipRepairInfo);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupMessageManager.AddPopupMessage(nkm_ERROR_CODE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
		}

		// Token: 0x06006CF5 RID: 27893 RVA: 0x0023B54C File Offset: 0x0023974C
		private void tryShipLevelup()
		{
			NKCPacketSender.Send_NKMPacket_SHIP_LEVELUP_REQ(this.m_curShipRepairInfo.ShipData.m_UnitUID, this.m_curShipRepairInfo.iTargetLevel);
		}

		// Token: 0x06006CF6 RID: 27894 RVA: 0x0023B56E File Offset: 0x0023976E
		private void tryShipUpgrade()
		{
			NKCUIHangarShipyardPopup.CheckInstanceAndClose();
			NKCPacketSender.Send_NKMPacket_SHIP_UPGRADE_REQ(this.m_curShipRepairInfo.ShipData.m_UnitUID, this.m_curShipRepairInfo.iNextShipID);
		}

		// Token: 0x06006CF7 RID: 27895 RVA: 0x0023B595 File Offset: 0x00239795
		private void tryShipLimitBreak()
		{
			NKCUIHangarShipyardPopup.CheckInstanceAndClose();
			NKCPacketSender.Send_NKMPacket_LIMIT_BREAK_SHIP_REQ(this.m_curShipRepairInfo.ShipData.m_UnitUID, this.m_SelectedCostShipUID);
		}

		// Token: 0x06006CF8 RID: 27896 RVA: 0x0023B5B8 File Offset: 0x002397B8
		private void OnClickCostShipSlot(NKCUISlot.SlotData slotData, bool bLocked)
		{
			NKCUIUnitSelectList.UnitSelectListOptions options = new NKCUIUnitSelectList.UnitSelectListOptions(NKM_UNIT_TYPE.NUT_SHIP, false, NKM_DECK_TYPE.NDT_NORMAL, NKCUIUnitSelectList.eUnitSelectListMode.Normal, true);
			options.setShipFilterCategory = NKCUnitSortSystem.setDefaultShipFilterCategory;
			options.setShipSortCategory = NKCUnitSortSystem.setDefaultShipSortCategory;
			options.m_SortOptions.AdditionalExcludeFilterFunc = new NKCUnitSortSystem.UnitListOptions.CustomFilterFunc(this.CheckUnitListFilter);
			options.setExcludeUnitUID = new HashSet<long>
			{
				this.m_curShipRepairInfo.ShipData.m_UnitUID
			};
			options.bCanSelectUnitInMission = false;
			options.bPushBackUnselectable = true;
			options.m_SortOptions.bUseDeckedState = true;
			options.m_SortOptions.bUseLockedState = true;
			options.m_bShowShipBuildShortcut = true;
			options.strEmptyMessage = NKCUtilString.GET_STRING_NO_EXIST_SELECT_SHIP;
			this.UnitSelectList.Open(options, new NKCUIUnitSelectList.OnUnitSelectCommand(this.OnChangedCostShip), null, null, null, null);
		}

		// Token: 0x06006CF9 RID: 27897 RVA: 0x0023B67E File Offset: 0x0023987E
		private bool CheckUnitListFilter(NKMUnitData unitData)
		{
			return this.m_lstCostShipIDList.Contains(unitData.m_UnitID);
		}

		// Token: 0x06006CFA RID: 27898 RVA: 0x0023B694 File Offset: 0x00239894
		private void OnChangedCostShip(List<long> listUnitUID)
		{
			if (listUnitUID.Count != 1)
			{
				Debug.LogError("Fatal Error : UnitSelectList returned wrong list");
				return;
			}
			long num = listUnitUID[0];
			NKMUnitData selectedShipData = NKCScenManager.CurrentUserData().m_ArmyData.GetShipFromUID(num);
			if (selectedShipData != null && selectedShipData.m_UnitLevel > 1)
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_SHIP_LIMITBREAK_WARNING, delegate()
				{
					this.SetCostShip(selectedShipData.m_UnitUID);
				}, null, false);
				return;
			}
			this.SetCostShip(num);
		}

		// Token: 0x06006CFB RID: 27899 RVA: 0x0023B71C File Offset: 0x0023991C
		private void SetCostShip(long unitUID)
		{
			this.UnitSelectList.Close();
			this.m_SelectedCostShipUID = unitUID;
			NKCUISlot.SlotData slotData = new NKCUISlot.SlotData();
			slotData.UID = unitUID;
			slotData.ID = NKCScenManager.CurrentUserData().m_ArmyData.GetShipFromUID(unitUID).m_UnitID;
			slotData.Count = 1L;
			slotData.eType = NKCUISlot.eSlotMode.Unit;
			NKCUtil.SetGameobjectActive(this.m_CostShipSlot, true);
			this.m_CostShipSlot.SetData(slotData, false, false, false, new NKCUISlot.OnClick(this.OnClickCostShipSlot));
			this.CheckCanRepair();
			this.m_btnLimitBreak.UnLock(false);
		}

		// Token: 0x06006CFC RID: 27900 RVA: 0x0023B7AC File Offset: 0x002399AC
		public void OnRecv(NKMPacket_SHIP_UPGRADE_ACK sPacket)
		{
			if (this.m_curShipRepairInfo.ShipData.m_UnitUID == sPacket.shipUnitData.m_UnitUID)
			{
				NKCUIGameResultGetUnit.ShowShipTranscendence(NKCScenManager.CurrentUserData().m_ArmyData.GetShipFromUID(sPacket.shipUnitData.m_UnitUID), this.m_curShipRepairInfo.iCurSkillCnt, this.m_curShipRepairInfo.iCurShipMaxLevel, this.m_curShipRepairInfo.iNextShipMaxLevel);
			}
		}

		// Token: 0x06006CFD RID: 27901 RVA: 0x0023B818 File Offset: 0x00239A18
		private void FindComponent<T>(string name, ref T target) where T : Component
		{
			GameObject gameObject = GameObject.Find(name);
			if (gameObject == null)
			{
				Debug.LogErrorFormat("NKCUIHangarShipyard::FindGameObject - FAILE NAME : {0}", new object[]
				{
					name
				});
				return;
			}
			target = gameObject.GetComponent<T>();
			if (target == null)
			{
				Debug.LogErrorFormat("NKCUIHangarShipyard::FindGameObject - FAILE NAME : {0}", new object[]
				{
					name
				});
			}
		}

		// Token: 0x06006CFE RID: 27902 RVA: 0x0023B880 File Offset: 0x00239A80
		private void FindGameObject(string name, ref GameObject target)
		{
			GameObject gameObject = GameObject.Find(name);
			if (gameObject == null)
			{
				Debug.LogErrorFormat("NKCUIHangarShipyard::FindGameObject - FAILE NAME : {0}", new object[]
				{
					name
				});
			}
			target = gameObject;
		}

		// Token: 0x17001291 RID: 4753
		// (get) Token: 0x06006CFF RID: 27903 RVA: 0x0023B8B4 File Offset: 0x00239AB4
		private NKCUIUnitSelectList UnitSelectList
		{
			get
			{
				if (this.m_UIUnitSelectList == null)
				{
					this.m_UIUnitSelectList = NKCUIUnitSelectList.OpenNewInstance(true);
				}
				return this.m_UIUnitSelectList;
			}
		}

		// Token: 0x040058B2 RID: 22706
		public GameObject m_objCommonRoot;

		// Token: 0x040058B3 RID: 22707
		public NKCUIHangarShipyardLevelup m_ShipLevelUp;

		// Token: 0x040058B4 RID: 22708
		public NKCUIHangarShipyardUpgrade m_ShipUpgrade;

		// Token: 0x040058B5 RID: 22709
		public NKCUIHangarShipyardLimitBreak m_ShipLimitBreak;

		// Token: 0x040058B6 RID: 22710
		[Header("작전능력")]
		public Text m_txt_NKM_UI_HANGAR_SHIPYARD_STAT_POWER_TEXT;

		// Token: 0x040058B7 RID: 22711
		public Text m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_ATTACK_TEXT;

		// Token: 0x040058B8 RID: 22712
		public Text m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_PLUS_ATTACK_TEXT;

		// Token: 0x040058B9 RID: 22713
		public Text m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_HP_TEXT;

		// Token: 0x040058BA RID: 22714
		public Text m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_PLUS_HP_TEXT;

		// Token: 0x040058BB RID: 22715
		public Text m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_DEFENCE_TEXT;

		// Token: 0x040058BC RID: 22716
		public Text m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_PLUS_DEFENCE_TEXT;

		// Token: 0x040058BD RID: 22717
		public Text m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_CRITICAL_TEXT;

		// Token: 0x040058BE RID: 22718
		public Text m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_PLUS_CRITICAL_TEXT;

		// Token: 0x040058BF RID: 22719
		public Text m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_HIT_TEXT;

		// Token: 0x040058C0 RID: 22720
		public Text m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_PLUS_HIT_TEXT;

		// Token: 0x040058C1 RID: 22721
		public Text m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_EVADE_TEXT;

		// Token: 0x040058C2 RID: 22722
		public Text m_txt_NKM_UI_HANGAR_UNIT_INFO_STAT_PLUS_EVADE_TEXT;

		// Token: 0x040058C3 RID: 22723
		[Header("함선 스킬")]
		public NKCUIShipSkillSlot[] m_slot_NKM_UI_SHIP_INFO_SKILL_SLOT;

		// Token: 0x040058C4 RID: 22724
		[Header("이식 재료 함선")]
		public GameObject m_objCostShip;

		// Token: 0x040058C5 RID: 22725
		public NKCUISlot m_CostShipSlot;

		// Token: 0x040058C6 RID: 22726
		public NKCUIComStateButton m_btnEmptyShipSlot;

		// Token: 0x040058C7 RID: 22727
		[Header("개조 재료")]
		public Text m_lbCostName;

		// Token: 0x040058C8 RID: 22728
		public NKCUIItemCostSlot[] m_CostSlot;

		// Token: 0x040058C9 RID: 22729
		[Header("실행 버튼")]
		public NKCUIComStateButton m_cbtn_NKM_UI_HANGAR_SHIPYARD_BUTTON_LevelUp;

		// Token: 0x040058CA RID: 22730
		public GameObject m_NKM_UI_HANGAR_UNIT_INFO_BUTTON_LevelUp_BG_DISABLE;

		// Token: 0x040058CB RID: 22731
		public GameObject m_NKM_UI_HANGAR_UNIT_INFO_BUTTON_LevelUp_BG_LIGHT;

		// Token: 0x040058CC RID: 22732
		public Text m_txt_NKM_UI_HANGAR_UNIT_INFO_BUTTON_LevelUp_BG_TEXT;

		// Token: 0x040058CD RID: 22733
		public Image m_img_NKM_UI_HANGAR_UNIT_INFO_BUTTON_LevelUp_BG_ICON;

		// Token: 0x040058CE RID: 22734
		public NKCUIComStateButton m_cbtn_NKM_UI_HANGAR_SHIPYARD_BUTTON_Upgrade;

		// Token: 0x040058CF RID: 22735
		public GameObject m_NKM_UI_HANGAR_SHIPYARD_BUTTON_Upgrade_BG_DISABLE;

		// Token: 0x040058D0 RID: 22736
		public GameObject m_NKM_UI_HANGAR_SHIPYARD_BUTTON_Upgrade_BG_LIGHT;

		// Token: 0x040058D1 RID: 22737
		public Text m_NKM_UI_HANGAR_SHIPYARD_BUTTON_Upgrade_BG_TEXT;

		// Token: 0x040058D2 RID: 22738
		public Image m_NKM_UI_HANGAR_SHIPYARD_BUTTON_Upgrade_BG_ICON;

		// Token: 0x040058D3 RID: 22739
		public NKCUIComStateButton m_btnLimitBreak;

		// Token: 0x040058D4 RID: 22740
		[Header("함선 능력치 툴팁")]
		public NKCComStatInfoToolTip m_ToolTipHP;

		// Token: 0x040058D5 RID: 22741
		public NKCComStatInfoToolTip m_ToolTipATK;

		// Token: 0x040058D6 RID: 22742
		public NKCComStatInfoToolTip m_ToolTipDEF;

		// Token: 0x040058D7 RID: 22743
		public NKCComStatInfoToolTip m_ToolTipCritical;

		// Token: 0x040058D8 RID: 22744
		public NKCComStatInfoToolTip m_ToolTipHit;

		// Token: 0x040058D9 RID: 22745
		public NKCComStatInfoToolTip m_ToolTipEvade;

		// Token: 0x040058DA RID: 22746
		private List<int> m_lstCostShipIDList = new List<int>();

		// Token: 0x040058DB RID: 22747
		private long m_SelectedCostShipUID;

		// Token: 0x040058DC RID: 22748
		private NKCUIShipInfoRepair.ShipRepairInfo m_curShipRepairInfo;

		// Token: 0x040058DD RID: 22749
		private NKCUIUnitSelectList m_UIUnitSelectList;

		// Token: 0x020016F0 RID: 5872
		public enum RepairState
		{
			// Token: 0x0400A576 RID: 42358
			None,
			// Token: 0x0400A577 RID: 42359
			LevelUp,
			// Token: 0x0400A578 RID: 42360
			Upgrade,
			// Token: 0x0400A579 RID: 42361
			LimitBreak
		}

		// Token: 0x020016F1 RID: 5873
		public class ShipRepairInfo
		{
			// Token: 0x17001926 RID: 6438
			// (get) Token: 0x0600B1BE RID: 45502 RVA: 0x00360A72 File Offset: 0x0035EC72
			public int iCurShipLevel
			{
				get
				{
					return this.ShipData.m_UnitLevel;
				}
			}

			// Token: 0x0600B1BF RID: 45503 RVA: 0x00360A80 File Offset: 0x0035EC80
			public ShipRepairInfo(long shipUID)
			{
				this.ShipData = NKCScenManager.CurrentUserData().m_ArmyData.GetShipFromUID(shipUID);
				if (this.ShipData == null)
				{
					return;
				}
				int num = NKCExpManager.GetUnitMaxLevel(this.ShipData);
				if (this.ShipData.m_UnitLevel < num)
				{
					this.eRepairState = NKCUIShipInfoRepair.RepairState.LevelUp;
				}
				else
				{
					NKMShipLimitBreakTemplet shipLimitBreakTemplet = NKMShipManager.GetShipLimitBreakTemplet(this.ShipData.m_UnitID, (int)(this.ShipData.m_LimitBreakLevel + 1));
					if (this.ShipData.m_UnitLevel == num && this.ShipData.m_UnitLevel >= 100)
					{
						if (shipLimitBreakTemplet != null)
						{
							this.eRepairState = NKCUIShipInfoRepair.RepairState.LimitBreak;
							num = shipLimitBreakTemplet.ShipLimitBreakMaxLevel;
						}
						else
						{
							this.eRepairState = NKCUIShipInfoRepair.RepairState.LevelUp;
							num = this.ShipData.m_UnitLevel;
						}
					}
					else
					{
						this.eRepairState = NKCUIShipInfoRepair.RepairState.Upgrade;
					}
				}
				int num2 = (num == this.iCurShipLevel) ? num : (this.iCurShipLevel + 1);
				this.iTargetLevel = num2;
				this.iMinimumLevel = num2;
				this.iMaximumLevel = num;
				this.dicMaterialList = new Dictionary<int, int>();
				this.UpdateShipData();
			}

			// Token: 0x0600B1C0 RID: 45504 RVA: 0x00360B7A File Offset: 0x0035ED7A
			public void UpdateShipData()
			{
				if (this.ShipData == null)
				{
					return;
				}
				this.UpdateMaterial();
				this.CheckCanTryLevelUp();
				this.UpdateShipAbility();
				this.UpdateUpgradeInfo();
				this.UpdateShipCredit();
			}

			// Token: 0x0600B1C1 RID: 45505 RVA: 0x00360BA4 File Offset: 0x0035EDA4
			public int GetMaximumLevel()
			{
				int i = this.iCurShipLevel;
				Dictionary<int, int> dictionary = new Dictionary<int, int>();
				while (i < this.iCurShipMaxLevel)
				{
					List<LevelupMaterial> shipLevelupMaterialList = NKMShipManager.GetShipLevelupTempletByLevel(this.iCurShipLevel, this.ShipData.GetUnitGrade(), (int)this.ShipData.m_LimitBreakLevel).ShipLevelupMaterialList;
					for (int j = 0; j < shipLevelupMaterialList.Count; j++)
					{
						LevelupMaterial levelupMaterial = shipLevelupMaterialList[j];
						int levelupMaterialItemID = levelupMaterial.m_LevelupMaterialItemID;
						if (dictionary.ContainsKey(levelupMaterialItemID))
						{
							dictionary[levelupMaterialItemID] += levelupMaterial.m_LevelupMaterialCount;
						}
						else
						{
							dictionary.Add(levelupMaterialItemID, levelupMaterial.m_LevelupMaterialCount);
						}
					}
					NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
					foreach (KeyValuePair<int, int> keyValuePair in dictionary)
					{
						if (nkmuserData.m_InventoryData.GetCountMiscItem(keyValuePair.Key) < (long)keyValuePair.Value)
						{
							return i;
						}
					}
					i++;
				}
				return i;
			}

			// Token: 0x0600B1C2 RID: 45506 RVA: 0x00360CBC File Offset: 0x0035EEBC
			private void UpdateMaterial()
			{
				this.dicMaterialList.Clear();
				if (this.eRepairState == NKCUIShipInfoRepair.RepairState.LevelUp)
				{
					int i = this.iCurShipLevel;
					int num = this.iTargetLevel - 1;
					while (i <= num)
					{
						List<LevelupMaterial> shipLevelupMaterialList = NKMShipManager.GetShipLevelupTempletByLevel(i, this.ShipData.GetUnitGrade(), (int)this.ShipData.m_LimitBreakLevel).ShipLevelupMaterialList;
						for (int j = 0; j < shipLevelupMaterialList.Count; j++)
						{
							LevelupMaterial levelupMaterial = shipLevelupMaterialList[j];
							int levelupMaterialItemID = levelupMaterial.m_LevelupMaterialItemID;
							if (this.dicMaterialList.ContainsKey(levelupMaterialItemID))
							{
								this.dicMaterialList[levelupMaterialItemID] = this.dicMaterialList[levelupMaterialItemID] + levelupMaterial.m_LevelupMaterialCount;
							}
							else
							{
								this.dicMaterialList.Add(levelupMaterialItemID, levelupMaterial.m_LevelupMaterialCount);
							}
						}
						i++;
					}
					return;
				}
				if (this.eRepairState == NKCUIShipInfoRepair.RepairState.Upgrade)
				{
					if (NKMUnitManager.GetUnitTempletBase(this.ShipData.m_UnitID) == null)
					{
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_HANGAR_SHIPYARD_CANNOT_CHANGE_SHIP, null, "");
						return;
					}
					NKMShipBuildTemplet shipBuildTemplet = NKMShipManager.GetShipBuildTemplet(this.ShipData.m_UnitID);
					if (shipBuildTemplet == null)
					{
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_HANGAR_SHIPYARD_CANNOT_FIND_INFORMATION, null, "");
						return;
					}
					shipBuildTemplet = NKMShipManager.GetShipBuildTemplet(shipBuildTemplet.ShipUpgradeTarget1);
					if (shipBuildTemplet == null)
					{
						return;
					}
					for (int k = 0; k < shipBuildTemplet.UpgradeMaterialList.Count; k++)
					{
						UpgradeMaterial upgradeMaterial = shipBuildTemplet.UpgradeMaterialList[k];
						int shipUpgradeMaterial = upgradeMaterial.m_ShipUpgradeMaterial;
						if (this.dicMaterialList.ContainsKey(shipUpgradeMaterial))
						{
							this.dicMaterialList[shipUpgradeMaterial] = this.dicMaterialList[shipUpgradeMaterial] + upgradeMaterial.m_ShipUpgradeMaterialCount;
						}
						else
						{
							this.dicMaterialList.Add(shipUpgradeMaterial, upgradeMaterial.m_ShipUpgradeMaterialCount);
						}
					}
					return;
				}
				else
				{
					if (this.eRepairState != NKCUIShipInfoRepair.RepairState.LimitBreak)
					{
						Debug.LogError("something was warong - NKCUIShipInfoRepair::UpdateMaterial >> unkonw eRepairState");
						return;
					}
					NKMShipLimitBreakTemplet shipLimitBreakTemplet = NKMShipManager.GetShipLimitBreakTemplet(this.ShipData.m_UnitID, (int)(this.ShipData.m_LimitBreakLevel + 1));
					if (shipLimitBreakTemplet == null)
					{
						return;
					}
					for (int l = 0; l < shipLimitBreakTemplet.ShipLimitBreakItems.Count; l++)
					{
						int itemId = shipLimitBreakTemplet.ShipLimitBreakItems[l].ItemId;
						if (itemId != 1)
						{
							int count = shipLimitBreakTemplet.ShipLimitBreakItems[l].Count32;
							if (this.dicMaterialList.ContainsKey(itemId))
							{
								Dictionary<int, int> dictionary = this.dicMaterialList;
								int key = itemId;
								dictionary[key] += count;
							}
							else
							{
								this.dicMaterialList.Add(itemId, count);
							}
						}
					}
					return;
				}
			}

			// Token: 0x0600B1C3 RID: 45507 RVA: 0x00360F48 File Offset: 0x0035F148
			private void CheckCanTryLevelUp()
			{
				this.bCanTryLevelUp = false;
				if (this.eRepairState == NKCUIShipInfoRepair.RepairState.LevelUp)
				{
					this.iCurShipMaxLevel = NKMShipManager.GetShipMaxLevel(this.ShipData);
					int nextLevel = (this.iTargetLevel >= this.iCurShipMaxLevel) ? (this.iCurShipMaxLevel - 1) : this.iTargetLevel;
					this.bCanTryLevelUp = (NKMShipManager.CanShipLevelup(NKCScenManager.CurrentUserData(), this.ShipData, nextLevel) == NKM_ERROR_CODE.NEC_OK);
				}
			}

			// Token: 0x0600B1C4 RID: 45508 RVA: 0x00360FB0 File Offset: 0x0035F1B0
			private void UpdateShipAbility()
			{
				NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(this.ShipData.m_UnitID);
				if (unitStatTemplet != null)
				{
					this.fCurShipHP = NKMUnitStatManager.CalculateStat(NKM_STAT_TYPE.NST_HP, unitStatTemplet.m_StatData, this.ShipData.m_listStatEXP, this.ShipData.m_UnitLevel, (int)this.ShipData.m_LimitBreakLevel, this.ShipData.GetMultiplierByPermanentContract(), null, null, 0, NKM_UNIT_TYPE.NUT_SHIP);
					this.fCurShipAtk = NKMUnitStatManager.CalculateStat(NKM_STAT_TYPE.NST_ATK, unitStatTemplet.m_StatData, this.ShipData.m_listStatEXP, this.ShipData.m_UnitLevel, (int)this.ShipData.m_LimitBreakLevel, this.ShipData.GetMultiplierByPermanentContract(), null, null, 0, NKM_UNIT_TYPE.NUT_SHIP);
					this.fCurShipDef = NKMUnitStatManager.CalculateStat(NKM_STAT_TYPE.NST_DEF, unitStatTemplet.m_StatData, this.ShipData.m_listStatEXP, this.ShipData.m_UnitLevel, (int)this.ShipData.m_LimitBreakLevel, this.ShipData.GetMultiplierByPermanentContract(), null, null, 0, NKM_UNIT_TYPE.NUT_SHIP);
					this.fCurShipCritical = NKMUnitStatManager.CalculateStat(NKM_STAT_TYPE.NST_CRITICAL, unitStatTemplet.m_StatData, this.ShipData.m_listStatEXP, this.ShipData.m_UnitLevel, (int)this.ShipData.m_LimitBreakLevel, this.ShipData.GetMultiplierByPermanentContract(), null, null, 0, NKM_UNIT_TYPE.NUT_SHIP);
					this.fCurShipHit = NKMUnitStatManager.CalculateStat(NKM_STAT_TYPE.NST_HIT, unitStatTemplet.m_StatData, this.ShipData.m_listStatEXP, this.ShipData.m_UnitLevel, (int)this.ShipData.m_LimitBreakLevel, this.ShipData.GetMultiplierByPermanentContract(), null, null, 0, NKM_UNIT_TYPE.NUT_SHIP);
					this.fCurShipEvade = NKMUnitStatManager.CalculateStat(NKM_STAT_TYPE.NST_EVADE, unitStatTemplet.m_StatData, this.ShipData.m_listStatEXP, this.ShipData.m_UnitLevel, (int)this.ShipData.m_LimitBreakLevel, this.ShipData.GetMultiplierByPermanentContract(), null, null, 0, NKM_UNIT_TYPE.NUT_SHIP);
					if (this.eRepairState == NKCUIShipInfoRepair.RepairState.LevelUp)
					{
						this.fNextShipHP = NKMUnitStatManager.CalculateStat(NKM_STAT_TYPE.NST_HP, unitStatTemplet.m_StatData, this.ShipData.m_listStatEXP, this.iTargetLevel, (int)this.ShipData.m_LimitBreakLevel, this.ShipData.GetMultiplierByPermanentContract(), null, null, 0, NKM_UNIT_TYPE.NUT_SHIP);
						this.fNextShipAtk = NKMUnitStatManager.CalculateStat(NKM_STAT_TYPE.NST_ATK, unitStatTemplet.m_StatData, this.ShipData.m_listStatEXP, this.iTargetLevel, (int)this.ShipData.m_LimitBreakLevel, this.ShipData.GetMultiplierByPermanentContract(), null, null, 0, NKM_UNIT_TYPE.NUT_SHIP);
						this.fNextShipDef = NKMUnitStatManager.CalculateStat(NKM_STAT_TYPE.NST_DEF, unitStatTemplet.m_StatData, this.ShipData.m_listStatEXP, this.iTargetLevel, (int)this.ShipData.m_LimitBreakLevel, this.ShipData.GetMultiplierByPermanentContract(), null, null, 0, NKM_UNIT_TYPE.NUT_SHIP);
						this.fNextShipCritical = NKMUnitStatManager.CalculateStat(NKM_STAT_TYPE.NST_CRITICAL, unitStatTemplet.m_StatData, this.ShipData.m_listStatEXP, this.iTargetLevel, (int)this.ShipData.m_LimitBreakLevel, this.ShipData.GetMultiplierByPermanentContract(), null, null, 0, NKM_UNIT_TYPE.NUT_SHIP);
						this.fNextShipHit = NKMUnitStatManager.CalculateStat(NKM_STAT_TYPE.NST_HIT, unitStatTemplet.m_StatData, this.ShipData.m_listStatEXP, this.iTargetLevel, (int)this.ShipData.m_LimitBreakLevel, this.ShipData.GetMultiplierByPermanentContract(), null, null, 0, NKM_UNIT_TYPE.NUT_SHIP);
						this.fNextShipEvade = NKMUnitStatManager.CalculateStat(NKM_STAT_TYPE.NST_EVADE, unitStatTemplet.m_StatData, this.ShipData.m_listStatEXP, this.iTargetLevel, (int)this.ShipData.m_LimitBreakLevel, this.ShipData.GetMultiplierByPermanentContract(), null, null, 0, NKM_UNIT_TYPE.NUT_SHIP);
					}
				}
			}

			// Token: 0x0600B1C5 RID: 45509 RVA: 0x003612DC File Offset: 0x0035F4DC
			private void UpdateShipCredit()
			{
				if (this.eRepairState == NKCUIShipInfoRepair.RepairState.LevelUp)
				{
					int levelUpCredit = NKMShipLevelUpTemplet.GetLevelUpCredit(this.ShipData.GetStarGrade(), this.ShipData.GetUnitGrade(), (int)this.ShipData.m_LimitBreakLevel);
					this.iNeedCredit = levelUpCredit * (this.iTargetLevel - this.iCurShipLevel);
					return;
				}
				if (this.eRepairState == NKCUIShipInfoRepair.RepairState.Upgrade)
				{
					NKMShipBuildTemplet shipBuildTemplet = NKMShipManager.GetShipBuildTemplet(this.iNextShipID);
					if (shipBuildTemplet != null)
					{
						this.iNeedCredit = shipBuildTemplet.ShipUpgradeCredit;
						return;
					}
				}
				else if (this.eRepairState == NKCUIShipInfoRepair.RepairState.LimitBreak && this.ShipData != null && this.ShipData.m_UnitLevel >= 100)
				{
					NKMShipLimitBreakTemplet shipLimitBreakTemplet = NKMShipManager.GetShipLimitBreakTemplet(this.ShipData.m_UnitID, (int)(this.ShipData.m_LimitBreakLevel + 1));
					if (shipLimitBreakTemplet != null)
					{
						this.iNeedCredit = shipLimitBreakTemplet.ShipLimitBreakItems.Find((MiscItemUnit x) => x.ItemId == 1).Count32;
					}
				}
			}

			// Token: 0x0600B1C6 RID: 45510 RVA: 0x003613D0 File Offset: 0x0035F5D0
			private void UpdateUpgradeInfo()
			{
				if (this.eRepairState == NKCUIShipInfoRepair.RepairState.Upgrade)
				{
					NKMShipBuildTemplet shipBuildTemplet = NKMShipManager.GetShipBuildTemplet(this.ShipData.m_UnitID);
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(shipBuildTemplet.ShipUpgradeTarget1);
					NKM_UNIT_GRADE grade = NKM_UNIT_GRADE.NUG_N;
					NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(this.ShipData.m_UnitID);
					if (unitTempletBase2 != null)
					{
						grade = unitTempletBase2.m_NKM_UNIT_GRADE;
					}
					else
					{
						Debug.LogError(string.Format("유닛 정보(unit ID : {0})를 찾을 수 없습니다.", this.ShipData.m_UnitID));
					}
					this.iNextShipID = shipBuildTemplet.ShipUpgradeTarget1;
					this.iCurShipMaxLevel = NKMShipLevelUpTemplet.GetMaxLevel(this.ShipData.GetStarGrade(), grade, (int)this.ShipData.m_LimitBreakLevel);
					this.iCurStar = this.ShipData.GetStarGrade();
					if (unitTempletBase != null)
					{
						this.iNextShipMaxLevel = NKMShipLevelUpTemplet.GetMaxLevel(unitTempletBase.m_StarGradeMax, grade, (int)this.ShipData.m_LimitBreakLevel);
						this.iNextStar = unitTempletBase.m_StarGradeMax;
					}
					else
					{
						this.iNextShipMaxLevel = this.iCurShipMaxLevel;
						this.iNextStar = this.iCurStar;
					}
				}
				else if (this.eRepairState == NKCUIShipInfoRepair.RepairState.LimitBreak)
				{
					NKM_UNIT_GRADE grade2 = NKM_UNIT_GRADE.NUG_N;
					NKMUnitTempletBase unitTempletBase3 = NKMUnitManager.GetUnitTempletBase(this.ShipData.m_UnitID);
					if (unitTempletBase3 != null)
					{
						grade2 = unitTempletBase3.m_NKM_UNIT_GRADE;
					}
					else
					{
						Debug.LogError(string.Format("유닛 정보(unit ID : {0})를 찾을 수 없습니다.", this.ShipData.m_UnitID));
					}
					NKMShipLimitBreakTemplet shipLimitBreakTemplet = NKMShipManager.GetShipLimitBreakTemplet(this.ShipData.m_UnitID, (int)this.ShipData.m_LimitBreakLevel);
					NKMShipLimitBreakTemplet shipLimitBreakTemplet2 = NKMShipManager.GetShipLimitBreakTemplet(this.ShipData.m_UnitID, (int)(this.ShipData.m_LimitBreakLevel + 1));
					if (shipLimitBreakTemplet != null)
					{
						this.iCurShipMaxLevel = shipLimitBreakTemplet.ShipLimitBreakMaxLevel;
					}
					else
					{
						this.iCurShipMaxLevel = NKMShipLevelUpTemplet.GetMaxLevel(this.ShipData.GetStarGrade(), grade2, 0);
					}
					if (shipLimitBreakTemplet2 != null)
					{
						this.iNextShipMaxLevel = shipLimitBreakTemplet2.ShipLimitBreakMaxLevel;
					}
					else
					{
						this.iNextShipMaxLevel = NKMShipLevelUpTemplet.GetMaxLevel(this.ShipData.GetStarGrade(), grade2, 0);
					}
					this.iNextShipID = this.ShipData.m_UnitID;
					this.iCurStar = this.ShipData.GetStarGrade();
					this.iNextStar = this.ShipData.GetStarGrade();
				}
				NKMUnitTempletBase unitTempletBase4 = NKMUnitManager.GetUnitTempletBase(this.ShipData.m_UnitID);
				this.iCurSkillCnt = unitTempletBase4.GetSkillCount();
			}

			// Token: 0x0400A57A RID: 42362
			public readonly NKCUIShipInfoRepair.RepairState eRepairState;

			// Token: 0x0400A57B RID: 42363
			public readonly NKMUnitData ShipData;

			// Token: 0x0400A57C RID: 42364
			public readonly int iMaximumLevel;

			// Token: 0x0400A57D RID: 42365
			public readonly int iMinimumLevel;

			// Token: 0x0400A57E RID: 42366
			public int iTargetLevel;

			// Token: 0x0400A57F RID: 42367
			public int iNextShipID;

			// Token: 0x0400A580 RID: 42368
			public int iCurStar;

			// Token: 0x0400A581 RID: 42369
			public int iNextStar;

			// Token: 0x0400A582 RID: 42370
			public int iCurShipMaxLevel;

			// Token: 0x0400A583 RID: 42371
			public int iNextShipMaxLevel;

			// Token: 0x0400A584 RID: 42372
			public float fCurShipAtk;

			// Token: 0x0400A585 RID: 42373
			public float fNextShipAtk;

			// Token: 0x0400A586 RID: 42374
			public float fCurShipHP;

			// Token: 0x0400A587 RID: 42375
			public float fNextShipHP;

			// Token: 0x0400A588 RID: 42376
			public float fCurShipDef;

			// Token: 0x0400A589 RID: 42377
			public float fNextShipDef;

			// Token: 0x0400A58A RID: 42378
			public float fCurShipCritical;

			// Token: 0x0400A58B RID: 42379
			public float fNextShipCritical;

			// Token: 0x0400A58C RID: 42380
			public float fCurShipHit;

			// Token: 0x0400A58D RID: 42381
			public float fNextShipHit;

			// Token: 0x0400A58E RID: 42382
			public float fCurShipEvade;

			// Token: 0x0400A58F RID: 42383
			public float fNextShipEvade;

			// Token: 0x0400A590 RID: 42384
			public int iCurSkillCnt;

			// Token: 0x0400A591 RID: 42385
			public int iNeedCredit;

			// Token: 0x0400A592 RID: 42386
			public bool bCanTryLevelUp;

			// Token: 0x0400A593 RID: 42387
			public bool bPossibleLevelUp;

			// Token: 0x0400A594 RID: 42388
			public bool bPossibleUpgrade;

			// Token: 0x0400A595 RID: 42389
			public bool bPossibleLimitBreak;

			// Token: 0x0400A596 RID: 42390
			public Dictionary<int, int> dicMaterialList;
		}
	}
}
