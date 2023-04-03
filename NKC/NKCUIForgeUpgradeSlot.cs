using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200099C RID: 2460
	public class NKCUIForgeUpgradeSlot : MonoBehaviour
	{
		// Token: 0x06006661 RID: 26209 RVA: 0x0020B0D4 File Offset: 0x002092D4
		public void SetData(NKMItemEquipUpgradeTemplet upgradeTemplet, NKCUIForgeUpgradeSlot.OnClickUpgradeSlot onClickUpgardeSlot)
		{
			this.m_UpgradeTemplet = upgradeTemplet;
			NKCUtil.SetGameobjectActive(this.m_objSelected, false);
			NKMEquipTemplet upgradeEquipTemplet = upgradeTemplet.UpgradeEquipTemplet;
			if (upgradeEquipTemplet == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			NKCUtil.SetLabelText(this.m_lbEquipName, upgradeEquipTemplet.GetItemName());
			if (NKMUnitManager.GetUnitTempletBase(upgradeEquipTemplet.GetPrivateUnitID()) != null)
			{
				NKCUtil.SetLabelText(this.m_lbEquipType, NKCUtilString.GetEquipPositionStringByUnitStyle(upgradeEquipTemplet, false));
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbEquipType, NKCUtilString.GetEquipPositionStringByUnitStyle(upgradeEquipTemplet, true));
			}
			this.m_slotEquip.SetData(NKCEquipSortSystem.MakeTempEquipData(upgradeEquipTemplet.m_ItemEquipID, 0, false), null, false, false, false, false);
			List<NKMEquipItemData> list = new List<NKMEquipItemData>();
			this.m_EquipUpgradeState = NKMItemManager.GetSetUpgradeSlotState(upgradeTemplet, ref list);
			this.SetUpgradeSlotState(this.m_EquipUpgradeState);
			this.m_btn.PointerClick.RemoveAllListeners();
			this.m_btn.PointerClick.AddListener(delegate()
			{
				onClickUpgardeSlot(this, this.m_EquipUpgradeState);
			});
		}

		// Token: 0x06006662 RID: 26210 RVA: 0x0020B1CC File Offset: 0x002093CC
		public void SetSelected(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objSelected, bValue);
		}

		// Token: 0x06006663 RID: 26211 RVA: 0x0020B1DA File Offset: 0x002093DA
		public void SetUpgradeSlotState(NKC_EQUIP_UPGRADE_STATE state)
		{
			NKCUtil.SetGameobjectActive(this.m_objUpgradable, state == NKC_EQUIP_UPGRADE_STATE.UPGRADABLE);
			NKCUtil.SetGameobjectActive(this.m_objNeedEnhance, state == NKC_EQUIP_UPGRADE_STATE.NEED_ENHANCE || state == NKC_EQUIP_UPGRADE_STATE.NEED_PRECISION);
			NKCUtil.SetGameobjectActive(this.m_objNotHave, state == NKC_EQUIP_UPGRADE_STATE.NOT_HAVE);
		}

		// Token: 0x06006664 RID: 26212 RVA: 0x0020B210 File Offset: 0x00209410
		public NKMItemEquipUpgradeTemplet GetUpgradeTemplet()
		{
			return this.m_UpgradeTemplet;
		}

		// Token: 0x06006665 RID: 26213 RVA: 0x0020B218 File Offset: 0x00209418
		public NKC_EQUIP_UPGRADE_STATE GetUpgradeState()
		{
			return this.m_EquipUpgradeState;
		}

		// Token: 0x04005211 RID: 21009
		public NKCUIComStateButton m_btn;

		// Token: 0x04005212 RID: 21010
		public NKCUISlotEquip m_slotEquip;

		// Token: 0x04005213 RID: 21011
		public Text m_lbEquipName;

		// Token: 0x04005214 RID: 21012
		public Text m_lbEquipType;

		// Token: 0x04005215 RID: 21013
		public GameObject m_objUpgradable;

		// Token: 0x04005216 RID: 21014
		public GameObject m_objNeedEnhance;

		// Token: 0x04005217 RID: 21015
		public GameObject m_objNotHave;

		// Token: 0x04005218 RID: 21016
		public GameObject m_objSelected;

		// Token: 0x04005219 RID: 21017
		private NKC_EQUIP_UPGRADE_STATE m_EquipUpgradeState;

		// Token: 0x0400521A RID: 21018
		private NKMItemEquipUpgradeTemplet m_UpgradeTemplet;

		// Token: 0x02001671 RID: 5745
		// (Invoke) Token: 0x0600B047 RID: 45127
		public delegate void OnClickUpgradeSlot(NKCUIForgeUpgradeSlot slot, NKC_EQUIP_UPGRADE_STATE state);
	}
}
