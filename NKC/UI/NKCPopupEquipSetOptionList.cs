using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A47 RID: 2631
	public class NKCPopupEquipSetOptionList : NKCUIBase
	{
		// Token: 0x1700133A RID: 4922
		// (get) Token: 0x06007371 RID: 29553 RVA: 0x00266813 File Offset: 0x00264A13
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700133B RID: 4923
		// (get) Token: 0x06007372 RID: 29554 RVA: 0x00266816 File Offset: 0x00264A16
		public override string MenuName
		{
			get
			{
				return "세트 옵션 목록";
			}
		}

		// Token: 0x06007373 RID: 29555 RVA: 0x0026681D File Offset: 0x00264A1D
		public void InitUI()
		{
			NKCUtil.SetBindFunction(this.m_NKM_UI_POPUP_OK_BOX_OK, new UnityAction(base.Close));
			NKCUtil.SetHotkey(this.m_NKM_UI_POPUP_OK_BOX_OK, HotkeyEventType.Confirm, null, false);
			NKCUtil.SetBindFunction(this.NKM_UI_FACTORY_EQUIP_SET_LIST_POPUP_CANCEL_BUTTON, new UnityAction(base.Close));
		}

		// Token: 0x06007374 RID: 29556 RVA: 0x0026685C File Offset: 0x00264A5C
		public void Open(long equipUID, string desc)
		{
			NKMEquipItemData itemEquip = NKCScenManager.CurrentUserData().m_InventoryData.GetItemEquip(equipUID);
			if (equipUID == 0L || itemEquip == null)
			{
				return;
			}
			this.Open(itemEquip, desc);
		}

		// Token: 0x06007375 RID: 29557 RVA: 0x0026688C File Offset: 0x00264A8C
		public void Open(NKMEquipItemData equipData, string desc)
		{
			NKCUtil.SetGameobjectActive(this.m_lbDesc, !string.IsNullOrEmpty(desc));
			NKCUtil.SetLabelText(this.m_lbDesc, desc);
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(equipData.m_ItemEquipID);
			if (equipTemplet != null && equipTemplet.SetGroupList != null && equipTemplet.SetGroupList.Count > 0)
			{
				this.m_NKM_UI_FACTORY_EQUIP_SET_LIST_POPUP_Content.anchoredPosition = new Vector2(this.m_NKM_UI_FACTORY_EQUIP_SET_LIST_POPUP_Content.anchoredPosition.x, 0f);
				List<int> list = new List<int>();
				list.AddRange(equipTemplet.SetGroupList);
				list.Sort();
				this.SetData(list);
				base.UIOpened(true);
			}
		}

		// Token: 0x06007376 RID: 29558 RVA: 0x0026692C File Offset: 0x00264B2C
		private void SetData(IReadOnlyList<int> lstSetOption)
		{
			foreach (int data in lstSetOption)
			{
				NKCPopupEquipSetOptionListSlot nkcpopupEquipSetOptionListSlot = UnityEngine.Object.Instantiate<NKCPopupEquipSetOptionListSlot>(this.m_pbfNKCPopupEquipSetOptionListSlot);
				if (nkcpopupEquipSetOptionListSlot != null)
				{
					nkcpopupEquipSetOptionListSlot.SetData(data);
					RectTransform component = nkcpopupEquipSetOptionListSlot.GetComponent<RectTransform>();
					if (component != null)
					{
						component.localScale = Vector2.one;
						component.SetParent(this.m_NKM_UI_FACTORY_EQUIP_SET_LIST_POPUP_Content);
					}
					this.m_lstSlots.Add(nkcpopupEquipSetOptionListSlot.gameObject);
				}
			}
		}

		// Token: 0x06007377 RID: 29559 RVA: 0x002669C8 File Offset: 0x00264BC8
		public override void CloseInternal()
		{
			foreach (GameObject obj in this.m_lstSlots)
			{
				UnityEngine.Object.Destroy(obj);
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x04005F6F RID: 24431
		public const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_FACTORY";

		// Token: 0x04005F70 RID: 24432
		public const string UI_ASSET_NAME = "NKM_UI_FACTORY_EQUIP_SET_LIST_POPUP";

		// Token: 0x04005F71 RID: 24433
		public Text m_NKM_UI_FACTORY_EQUIP_SET_LIST_POPUP_TOP_TEXT;

		// Token: 0x04005F72 RID: 24434
		public Text m_lbDesc;

		// Token: 0x04005F73 RID: 24435
		public RectTransform m_NKM_UI_FACTORY_EQUIP_SET_LIST_POPUP_Content;

		// Token: 0x04005F74 RID: 24436
		[Header("버튼들")]
		public NKCUIComStateButton m_NKM_UI_POPUP_OK_BOX_OK;

		// Token: 0x04005F75 RID: 24437
		public NKCUIComStateButton NKM_UI_FACTORY_EQUIP_SET_LIST_POPUP_CANCEL_BUTTON;

		// Token: 0x04005F76 RID: 24438
		[Header("슬롯")]
		public NKCPopupEquipSetOptionListSlot m_pbfNKCPopupEquipSetOptionListSlot;

		// Token: 0x04005F77 RID: 24439
		private List<GameObject> m_lstSlots = new List<GameObject>();
	}
}
