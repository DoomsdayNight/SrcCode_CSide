using System;
using NKM;
using UnityEngine;

namespace NKC.UI.Result
{
	// Token: 0x02000B9D RID: 2973
	public class NKCUIResultRewardSlot : MonoBehaviour
	{
		// Token: 0x06008984 RID: 35204 RVA: 0x002EA688 File Offset: 0x002E8888
		public int GetIdx()
		{
			return this.idx;
		}

		// Token: 0x06008985 RID: 35205 RVA: 0x002EA690 File Offset: 0x002E8890
		public void Init()
		{
			this.m_NKCUISlot.Init();
		}

		// Token: 0x06008986 RID: 35206 RVA: 0x002EA69D File Offset: 0x002E889D
		public void SetData(NKCUISlot.SlotData data, int index)
		{
			this.idx = index;
			this.m_NKCUISlot.SetData(data, true, new NKCUISlot.OnClick(this.OnClickItem));
			this.m_NKCUISlot.SetBonusRate(data.BonusRate);
		}

		// Token: 0x06008987 RID: 35207 RVA: 0x002EA6D0 File Offset: 0x002E88D0
		public void SetEffectEnable(bool bEnable)
		{
			NKCUtil.SetGameobjectActive(this.m_objEffect, bEnable);
		}

		// Token: 0x06008988 RID: 35208 RVA: 0x002EA6E0 File Offset: 0x002E88E0
		public void OnClickItem(NKCUISlot.SlotData slotData, bool bLocked)
		{
			if (slotData == null)
			{
				return;
			}
			switch (slotData.eType)
			{
			default:
				NKCPopupItemBox.Instance.Open(NKCPopupItemBox.eMode.Normal, slotData, null, false, false, false);
				return;
			case NKCUISlot.eSlotMode.Equip:
			case NKCUISlot.eSlotMode.EquipCount:
			{
				NKMEquipItemData nkmequipItemData = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(slotData.UID);
				if (nkmequipItemData == null)
				{
					nkmequipItemData = NKCEquipSortSystem.MakeTempEquipData(slotData.ID, slotData.GroupID, false);
				}
				NKCPopupItemEquipBox.Open(nkmequipItemData, NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_NONE, null);
				return;
			}
			case NKCUISlot.eSlotMode.Skin:
				Debug.LogWarning("Skin Popup under construction");
				return;
			}
		}

		// Token: 0x06008989 RID: 35209 RVA: 0x002EA777 File Offset: 0x002E8977
		public void SetFirstRewardMark(bool bEnable)
		{
			this.m_NKCUISlot.SetFirstGetMark(bEnable);
		}

		// Token: 0x0600898A RID: 35210 RVA: 0x002EA785 File Offset: 0x002E8985
		public void SetOnetimeRewardMark(bool bEnable)
		{
			this.m_NKCUISlot.SetOnetimeMark(bEnable);
		}

		// Token: 0x0600898B RID: 35211 RVA: 0x002EA793 File Offset: 0x002E8993
		public void SetFirstAllClearRewardMark(bool bEnable)
		{
			this.m_NKCUISlot.SetFirstAllClearMark(bEnable);
		}

		// Token: 0x0600898C RID: 35212 RVA: 0x002EA7A1 File Offset: 0x002E89A1
		public void SetSelect(bool bSelect)
		{
			this.m_NKCUISlot.SetSelected(bSelect);
		}

		// Token: 0x0600898D RID: 35213 RVA: 0x002EA7AF File Offset: 0x002E89AF
		public void SetText(string text)
		{
			this.m_NKCUISlot.SetAdditionalText(text, TextAnchor.MiddleCenter);
		}

		// Token: 0x0600898E RID: 35214 RVA: 0x002EA7BE File Offset: 0x002E89BE
		private void OnDisable()
		{
			this.SetEffectEnable(false);
		}

		// Token: 0x040075FA RID: 30202
		public NKCUISlot m_NKCUISlot;

		// Token: 0x040075FB RID: 30203
		public GameObject m_objEffect;

		// Token: 0x040075FC RID: 30204
		private int idx;
	}
}
