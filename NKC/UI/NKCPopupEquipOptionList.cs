using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A46 RID: 2630
	public class NKCPopupEquipOptionList : NKCUIBase
	{
		// Token: 0x17001338 RID: 4920
		// (get) Token: 0x06007369 RID: 29545 RVA: 0x00266431 File Offset: 0x00264631
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001339 RID: 4921
		// (get) Token: 0x0600736A RID: 29546 RVA: 0x00266434 File Offset: 0x00264634
		public override string MenuName
		{
			get
			{
				return "옵션 목록";
			}
		}

		// Token: 0x0600736B RID: 29547 RVA: 0x0026643B File Offset: 0x0026463B
		public void InitUI()
		{
			NKCUtil.SetBindFunction(this.m_NKM_UI_POPUP_OK_BOX_OK, new UnityAction(base.Close));
			NKCUtil.SetHotkey(this.m_NKM_UI_POPUP_OK_BOX_OK, HotkeyEventType.Confirm, null, false);
			NKCUtil.SetBindFunction(this.m_NKM_UI_FACTORY_EQUIP_OPTION_POPUP_CANCEL_BUTTON, new UnityAction(base.Close));
		}

		// Token: 0x0600736C RID: 29548 RVA: 0x0026647C File Offset: 0x0026467C
		public void Open(NKMEquipItemData equipData, int ChangeableOptionCnt, string desc)
		{
			if (equipData == null || ChangeableOptionCnt == 0)
			{
				base.Close();
				return;
			}
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(equipData.m_ItemEquipID);
			if (equipTemplet == null)
			{
				base.Close();
				return;
			}
			this.SetData(0, NKMEquipTuningManager.GetEquipRandomStatGroupList(equipTemplet.m_StatGroupID));
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_EQUIP_OPTION_POPUP_LIST01, ChangeableOptionCnt != 2);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_EQUIP_OPTION_POPUP_LIST02, ChangeableOptionCnt >= 2);
			if (ChangeableOptionCnt >= 2)
			{
				this.SetData(1, NKMEquipTuningManager.GetEquipRandomStatGroupList(equipTemplet.m_StatGroupID_2));
			}
			NKCUtil.SetGameobjectActive(this.m_lbDesc, !string.IsNullOrEmpty(desc));
			NKCUtil.SetLabelText(this.m_lbDesc, desc);
			base.UIOpened(true);
		}

		// Token: 0x0600736D RID: 29549 RVA: 0x00266520 File Offset: 0x00264720
		public void Open(long equipUID, int ChangeableOptionCnt, string desc)
		{
			if (equipUID == 0L || ChangeableOptionCnt == 0)
			{
				return;
			}
			NKMEquipItemData itemEquip = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(equipUID);
			if (itemEquip == null)
			{
				return;
			}
			this.Open(itemEquip, ChangeableOptionCnt, desc);
		}

		// Token: 0x0600736E RID: 29550 RVA: 0x00266558 File Offset: 0x00264758
		private void SetData(int Idx, IReadOnlyList<NKMEquipRandomStatTemplet> lstStat)
		{
			foreach (NKMEquipRandomStatTemplet nkmequipRandomStatTemplet in lstStat)
			{
				NKCPopupEquipOptionListSlot nkcpopupEquipOptionListSlot = UnityEngine.Object.Instantiate<NKCPopupEquipOptionListSlot>(this.m_pbfNKCPopupEquipOptionListSlot);
				if (nkcpopupEquipOptionListSlot != null)
				{
					if (Idx == 0)
					{
						nkcpopupEquipOptionListSlot.transform.SetParent(this.m_trNKM_UI_UNIT_INFO_POPUP_LIST_Content_01, false);
					}
					else
					{
						nkcpopupEquipOptionListSlot.transform.SetParent(this.m_trNKM_UI_UNIT_INFO_POPUP_LIST_Content_02, false);
					}
					if (NKCUIForgeTuning.IsPercentStat(nkmequipRandomStatTemplet))
					{
						decimal num = new decimal(NKMUnitStatManager.IsPercentStat(nkmequipRandomStatTemplet.m_StatType) ? nkmequipRandomStatTemplet.m_MinStatValue : nkmequipRandomStatTemplet.m_MinStatRate);
						num = Math.Round(num * 1000m) / 1000m;
						decimal num2 = new decimal(NKMUnitStatManager.IsPercentStat(nkmequipRandomStatTemplet.m_StatType) ? nkmequipRandomStatTemplet.m_MaxStatValue : nkmequipRandomStatTemplet.m_MaxStatRate);
						num2 = Math.Round(num2 * 1000m) / 1000m;
						if (NKCUtilString.IsNameReversedIfNegative(nkmequipRandomStatTemplet.m_StatType) && num2 < 0m)
						{
							nkcpopupEquipOptionListSlot.SetData(NKCUtilString.GetStatShortName(nkmequipRandomStatTemplet.m_StatType, true), string.Format("{0:P1}~{1:P1}", -num2, -num));
						}
						else
						{
							nkcpopupEquipOptionListSlot.SetData(NKCUtilString.GetStatShortName(nkmequipRandomStatTemplet.m_StatType), string.Format("{0:P1}~{1:P1}", num, num2));
						}
					}
					else if (NKCUtilString.IsNameReversedIfNegative(nkmequipRandomStatTemplet.m_StatType) && nkmequipRandomStatTemplet.m_MaxStatValue < 0f)
					{
						nkcpopupEquipOptionListSlot.SetData(NKCUtilString.GetStatShortName(nkmequipRandomStatTemplet.m_StatType, true), string.Format("{0}~{1}", Mathf.Abs(nkmequipRandomStatTemplet.m_MaxStatValue), Mathf.Abs(nkmequipRandomStatTemplet.m_MinStatValue)));
					}
					else
					{
						nkcpopupEquipOptionListSlot.SetData(NKCUtilString.GetStatShortName(nkmequipRandomStatTemplet.m_StatType), string.Format("{0}~{1}", nkmequipRandomStatTemplet.m_MinStatValue, nkmequipRandomStatTemplet.m_MaxStatValue));
					}
					this.m_lstSlots.Add(nkcpopupEquipOptionListSlot.gameObject);
				}
			}
		}

		// Token: 0x0600736F RID: 29551 RVA: 0x002667A4 File Offset: 0x002649A4
		public override void CloseInternal()
		{
			foreach (GameObject obj in this.m_lstSlots)
			{
				UnityEngine.Object.Destroy(obj);
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x04005F64 RID: 24420
		public const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_FACTORY";

		// Token: 0x04005F65 RID: 24421
		public const string UI_ASSET_NAME = "NKM_UI_FACTORY_EQUIP_OPTION_POPUP";

		// Token: 0x04005F66 RID: 24422
		[Header("옵션 1,2")]
		public GameObject m_NKM_UI_FACTORY_EQUIP_OPTION_POPUP_LIST01;

		// Token: 0x04005F67 RID: 24423
		public GameObject m_NKM_UI_FACTORY_EQUIP_OPTION_POPUP_LIST02;

		// Token: 0x04005F68 RID: 24424
		[Space]
		public Transform m_trNKM_UI_UNIT_INFO_POPUP_LIST_Content_01;

		// Token: 0x04005F69 RID: 24425
		public Transform m_trNKM_UI_UNIT_INFO_POPUP_LIST_Content_02;

		// Token: 0x04005F6A RID: 24426
		[Header("팝업 설명")]
		public Text m_lbDesc;

		// Token: 0x04005F6B RID: 24427
		[Header("버튼들")]
		public NKCUIComStateButton m_NKM_UI_POPUP_OK_BOX_OK;

		// Token: 0x04005F6C RID: 24428
		public NKCUIComStateButton m_NKM_UI_FACTORY_EQUIP_OPTION_POPUP_CANCEL_BUTTON;

		// Token: 0x04005F6D RID: 24429
		[Header("슬롯")]
		public NKCPopupEquipOptionListSlot m_pbfNKCPopupEquipOptionListSlot;

		// Token: 0x04005F6E RID: 24430
		private List<GameObject> m_lstSlots = new List<GameObject>();
	}
}
