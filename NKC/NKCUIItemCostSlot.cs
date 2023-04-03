using System;
using System.Collections.Generic;
using NKC.UI.Result;
using NKC.UI.Tooltip;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009AC RID: 2476
	public class NKCUIItemCostSlot : MonoBehaviour
	{
		// Token: 0x170011F6 RID: 4598
		// (get) Token: 0x06006773 RID: 26483 RVA: 0x0021557B File Offset: 0x0021377B
		// (set) Token: 0x06006774 RID: 26484 RVA: 0x00215583 File Offset: 0x00213783
		public int ItemID { get; private set; }

		// Token: 0x06006775 RID: 26485 RVA: 0x0021558C File Offset: 0x0021378C
		public void Set_EQUIP_BOX_BOTTOM_MENU_TYPE(NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE type)
		{
			this.m_EQUIP_BOX_BOTTOM_MENU_TYPE = type;
		}

		// Token: 0x06006776 RID: 26486 RVA: 0x00215598 File Offset: 0x00213798
		public void SetData(int itemID, int ReqCnt, long CurCnt, bool bShowTooltip = true, bool bShowBG = true, bool bShowEvent = false)
		{
			this.ItemID = itemID;
			NKCUtil.SetGameobjectActive(this.m_ICON.gameObject, itemID != 0);
			NKCUtil.SetGameobjectActive(this.m_COUNT.gameObject, itemID != 0 && (ReqCnt > 0 || CurCnt > 0L));
			NKCUtil.SetGameobjectActive(this.m_REQUIRED, itemID != 0 && (long)ReqCnt > CurCnt);
			NKCUtil.SetGameobjectActive(this.m_BG, bShowBG);
			NKCUtil.SetGameobjectActive(this.m_objEvent, bShowEvent);
			if (itemID == 0)
			{
				if (bShowBG)
				{
					Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_inven_icon_common", "AB_INVEN_ICON_COSTBG_EMPTY", false);
					if (orLoadAssetResource != null)
					{
						NKCUtil.SetImageSprite(this.m_BG, orLoadAssetResource, false);
					}
				}
				this.m_AB_ICON_COST_SLOT.PointerClick.RemoveAllListeners();
				this.slotData = null;
				return;
			}
			if (bShowBG)
			{
				Sprite orLoadAssetResource2 = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_inven_icon_common", "AB_INVEN_ICON_COSTBG", false);
				if (orLoadAssetResource2 != null)
				{
					NKCUtil.SetImageSprite(this.m_BG, orLoadAssetResource2, false);
				}
			}
			Sprite orLoadMiscItemIcon = NKCResourceUtility.GetOrLoadMiscItemIcon(NKMItemManager.GetItemMiscTempletByID(itemID));
			NKCUtil.SetImageSprite(this.m_ICON, orLoadMiscItemIcon, false);
			this.SetCount(ReqCnt, CurCnt);
			this.slotData = NKCUISlot.SlotData.MakeMiscItemData(itemID, (long)ReqCnt, 0);
			if (bShowTooltip)
			{
				if (!this.m_ShowPopup)
				{
					this.m_AB_ICON_COST_SLOT.PointerDown.RemoveAllListeners();
					this.m_AB_ICON_COST_SLOT.PointerDown.AddListener(new UnityAction<PointerEventData>(this.OnClicked));
					return;
				}
				this.m_AB_ICON_COST_SLOT.PointerClick.RemoveAllListeners();
				this.m_AB_ICON_COST_SLOT.PointerClick.AddListener(new UnityAction(this.OnClickedPopUpSlot));
			}
		}

		// Token: 0x06006777 RID: 26487 RVA: 0x00215718 File Offset: 0x00213918
		public void SetCount(int ReqCnt, long CurCnt)
		{
			int itemID = this.ItemID;
			string msg;
			if (itemID - 1 <= 2 || itemID == 101)
			{
				if ((long)ReqCnt > CurCnt)
				{
					msg = string.Format("<color=#ff0000ff>{0}</color>", ReqCnt);
				}
				else
				{
					msg = string.Format("{0}", ReqCnt);
				}
			}
			else if ((long)ReqCnt > CurCnt)
			{
				msg = string.Format("<color=#ff0000ff>{0}</color>/{1}", CurCnt, ReqCnt);
			}
			else if ((int)CurCnt > 100000)
			{
				msg = string.Format("*/{0}", ReqCnt);
			}
			else
			{
				msg = string.Format("{0}/{1}", CurCnt, ReqCnt);
			}
			NKCUtil.SetLabelText(this.m_COUNT, msg);
		}

		// Token: 0x06006778 RID: 26488 RVA: 0x002157C0 File Offset: 0x002139C0
		private void OpenItemBox()
		{
			if (this.slotData == null)
			{
				return;
			}
			switch (this.slotData.eType)
			{
			case NKCUISlot.eSlotMode.Unit:
			case NKCUISlot.eSlotMode.UnitCount:
			case NKCUISlot.eSlotMode.Emoticon:
				NKCPopupItemBox.Instance.Open(NKCPopupItemBox.eMode.Normal, this.slotData, null, false, false, true);
				return;
			default:
				NKCPopupItemBox.Instance.Open(NKCPopupItemBox.eMode.Normal, this.slotData, null, false, false, true);
				return;
			case NKCUISlot.eSlotMode.Equip:
				this.OpenEquipBox(this.slotData);
				return;
			case NKCUISlot.eSlotMode.Skin:
				Debug.LogWarning("Skin Popup under construction");
				return;
			case NKCUISlot.eSlotMode.Mold:
			{
				NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(this.slotData.ID);
				if (itemMoldTempletByID != null && itemMoldTempletByID.IsEquipMold && NKMItemManager.m_dicRandomMoldBox.ContainsKey(itemMoldTempletByID.m_RewardGroupID))
				{
					List<int> list = NKMItemManager.m_dicRandomMoldBox[itemMoldTempletByID.m_RewardGroupID];
					if (list != null && list.Count > 0)
					{
						NKCUISlotListViewer.GetNewInstance().OpenRewardList(list, NKM_REWARD_TYPE.RT_EQUIP, itemMoldTempletByID.GetItemName(), NKCUtilString.GET_STRING_FORGE_CRAFT_MOLD_DESC);
					}
				}
				return;
			}
			}
		}

		// Token: 0x06006779 RID: 26489 RVA: 0x002158B8 File Offset: 0x00213AB8
		private void OpenEquipBox(NKCUISlot.SlotData slotData)
		{
			NKMEquipItemData nkmequipItemData = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(slotData.UID);
			if (nkmequipItemData == null)
			{
				nkmequipItemData = NKCEquipSortSystem.MakeTempEquipData(slotData.ID, slotData.GroupID, false);
				NKCPopupItemEquipBox.Open(nkmequipItemData, NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_NONE, null);
				return;
			}
			if (NKCUIWarfareResult.IsInstanceOpen)
			{
				NKCPopupItemEquipBox.Open(nkmequipItemData, NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_NONE, null);
				return;
			}
			if (nkmequipItemData.m_OwnerUnitUID <= 0L)
			{
				NKCPopupItemEquipBox.Open(nkmequipItemData, this.m_EQUIP_BOX_BOTTOM_MENU_TYPE, null);
				return;
			}
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(nkmequipItemData.m_ItemEquipID);
			if (equipTemplet == null)
			{
				return;
			}
			NKMUnitData unitFromUID = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetUnitFromUID(nkmequipItemData.m_OwnerUnitUID);
			if (unitFromUID == null)
			{
				return;
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = equipTemplet.CanUnEquipByUnit(NKCScenManager.GetScenManager().GetMyUserData(), unitFromUID);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupMessageManager.AddPopupMessage(nkm_ERROR_CODE.ToString(), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			NKCPopupItemEquipBox.Open(nkmequipItemData, this.m_EQUIP_BOX_BOTTOM_MENU_TYPE, null);
		}

		// Token: 0x0600677A RID: 26490 RVA: 0x00215994 File Offset: 0x00213B94
		public void OnClicked(PointerEventData eventData)
		{
			if (this.slotData != null)
			{
				NKCUITooltip.Instance.Open(this.slotData, new Vector2?(eventData.position));
			}
		}

		// Token: 0x0600677B RID: 26491 RVA: 0x002159B9 File Offset: 0x00213BB9
		public void OnClickedPopUpSlot()
		{
			if (this.slotData != null)
			{
				this.OpenItemBox();
			}
		}

		// Token: 0x040053A2 RID: 21410
		public Image m_BG;

		// Token: 0x040053A3 RID: 21411
		public Image m_ICON;

		// Token: 0x040053A4 RID: 21412
		public Text m_COUNT;

		// Token: 0x040053A5 RID: 21413
		public GameObject m_REQUIRED;

		// Token: 0x040053A6 RID: 21414
		public NKCUIComStateButton m_AB_ICON_COST_SLOT;

		// Token: 0x040053A7 RID: 21415
		public GameObject m_objEvent;

		// Token: 0x040053A8 RID: 21416
		public bool m_ShowPopup;

		// Token: 0x040053AA RID: 21418
		private NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE m_EQUIP_BOX_BOTTOM_MENU_TYPE = NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_ENFORCE_AND_EQUIP;

		// Token: 0x040053AB RID: 21419
		private const int DISPLAY_LIMIT_VALUE = 100000;

		// Token: 0x040053AC RID: 21420
		private NKCUISlot.SlotData slotData;
	}
}
