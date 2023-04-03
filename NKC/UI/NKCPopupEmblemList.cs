using System;
using System.Collections.Generic;
using ClientPacket.Common;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A40 RID: 2624
	public class NKCPopupEmblemList : NKCUIBase
	{
		// Token: 0x1700132B RID: 4907
		// (get) Token: 0x06007301 RID: 29441 RVA: 0x00263A8F File Offset: 0x00261C8F
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700132C RID: 4908
		// (get) Token: 0x06007302 RID: 29442 RVA: 0x00263A92 File Offset: 0x00261C92
		public override string MenuName
		{
			get
			{
				return "PopupEmblemList";
			}
		}

		// Token: 0x1700132D RID: 4909
		// (get) Token: 0x06007303 RID: 29443 RVA: 0x00263A9C File Offset: 0x00261C9C
		private NKCPopupEmblemChangeConfirm NKCPopupEmblemChangeConfirm
		{
			get
			{
				if (this.m_NKCPopupEmblemChangeConfirm == null)
				{
					NKCUIManager.LoadedUIData loadedUIData = NKCUIManager.OpenNewInstance<NKCPopupEmblemChangeConfirm>("AB_UI_NKM_UI_FRIEND", "NKM_UI_POPUP_EMBLEM_CONFIRM", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), null);
					this.m_NKCPopupEmblemChangeConfirm = loadedUIData.GetInstance<NKCPopupEmblemChangeConfirm>();
					this.m_NKCPopupEmblemChangeConfirm.InitUI();
				}
				return this.m_NKCPopupEmblemChangeConfirm;
			}
		}

		// Token: 0x06007304 RID: 29444 RVA: 0x00263AEB File Offset: 0x00261CEB
		public void CheckNKCPopupEmblemChangeConfirmAndClose()
		{
			if (this.m_NKCPopupEmblemChangeConfirm != null && this.m_NKCPopupEmblemChangeConfirm.IsOpen)
			{
				this.m_NKCPopupEmblemChangeConfirm.Close();
			}
		}

		// Token: 0x06007305 RID: 29445 RVA: 0x00263B14 File Offset: 0x00261D14
		public void InitUI()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_lsrEmblem.dOnGetObject += this.GetEmblemSlot;
			this.m_lsrEmblem.dOnReturnObject += this.ReturnEmblemSlot;
			this.m_lsrEmblem.dOnProvideData += this.ProvideEmblemSlot;
			NKCUtil.SetScrollHotKey(this.m_lsrEmblem, null);
			this.m_lsrEmblem.ContentConstraintCount = 5;
			NKCUtil.SetBindFunction(this.m_csbtnOK, new UnityAction(this.OnClickOK));
			NKCUtil.SetHotkey(this.m_csbtnOK, HotkeyEventType.Confirm, null, false);
			NKCUtil.SetBindFunction(this.m_csbtnCancel, new UnityAction(this.OnCloseBtn));
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(delegate(BaseEventData e)
			{
				this.OnCloseBtn();
			});
			this.m_etBG.triggers.Add(entry);
		}

		// Token: 0x06007306 RID: 29446 RVA: 0x00263BFB File Offset: 0x00261DFB
		private void OnClickOK()
		{
			if (this.m_SelectedID != -1)
			{
				this.NKCPopupEmblemChangeConfirm.Open(this.m_EquippedEmblemID, this.m_SelectedID, delegate(int e)
				{
					this.m_dOnClickOK(e);
				});
				return;
			}
			base.Close();
		}

		// Token: 0x06007307 RID: 29447 RVA: 0x00263C30 File Offset: 0x00261E30
		public RectTransform GetEmblemSlot(int index)
		{
			return NKCUISlot.GetNewInstance(null).GetComponent<RectTransform>();
		}

		// Token: 0x06007308 RID: 29448 RVA: 0x00263C3D File Offset: 0x00261E3D
		public void ReturnEmblemSlot(Transform tr)
		{
			tr.SetParent(base.transform);
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x06007309 RID: 29449 RVA: 0x00263C58 File Offset: 0x00261E58
		public void ProvideEmblemSlot(Transform tr, int index)
		{
			NKCUISlot component = tr.GetComponent<NKCUISlot>();
			if (component != null && this.m_lstItemMiscDataAndTemplet.Count > index)
			{
				if (this.m_lstItemMiscDataAndTemplet[index].m_NKMItemMiscData != null)
				{
					component.SetData(NKCUISlot.SlotData.MakeMiscItemData(this.m_lstItemMiscDataAndTemplet[index].m_NKMItemMiscData, 0), true, new NKCUISlot.OnClick(this.OnClickSlot));
					component.SetSelected(this.m_SelectedID == this.m_lstItemMiscDataAndTemplet[index].m_NKMItemMiscData.ItemID);
					return;
				}
				component.SetEmpty(new NKCUISlot.OnClick(this.OnClickSlot));
				component.SetSelected(this.m_SelectedID == 0);
			}
		}

		// Token: 0x0600730A RID: 29450 RVA: 0x00263D0C File Offset: 0x00261F0C
		private void OnClickSlot(NKCUISlot.SlotData slotData, bool bLocked)
		{
			if (slotData != null)
			{
				this.m_SelectedID = slotData.ID;
				this.m_NKCPopupEmblemBigSlot.SetData(this.m_SelectedID);
			}
			else
			{
				this.m_SelectedID = 0;
				this.m_NKCPopupEmblemBigSlot.SetEmpty(NKCUtilString.GET_STRING_EMBLEM_EQUIPPED_EMBLEM_UNEQUIP);
			}
			this.m_lsrEmblem.RefreshCells(false);
		}

		// Token: 0x0600730B RID: 29451 RVA: 0x00263D60 File Offset: 0x00261F60
		public void Open(int equippedEmblemID, bool bUseEmpty, NKCPopupEmblemList.dOnClickOK _dOnClickOK = null)
		{
			NKMUserProfileData userProfileData = NKCScenManager.CurrentUserData().UserProfileData;
			if (userProfileData == null)
			{
				return;
			}
			this.m_dOnClickOK = _dOnClickOK;
			this.m_EquippedEmblemID = equippedEmblemID;
			this.m_SelectedID = -1;
			if (bUseEmpty)
			{
				this.m_NKCPopupEmblemBigSlot.SetData(equippedEmblemID);
			}
			else
			{
				this.m_NKCPopupEmblemBigSlot.SetEmpty("");
			}
			List<NKMItemMiscData> emblemData = NKCScenManager.CurrentUserData().m_InventoryData.GetEmblemData();
			for (int i = 0; i < userProfileData.emblems.Count; i++)
			{
				if (userProfileData.emblems[i] != null)
				{
					for (int j = 0; j < emblemData.Count; j++)
					{
						if (emblemData[j].ItemID == userProfileData.emblems[i].id)
						{
							emblemData.RemoveAt(j);
							break;
						}
					}
				}
			}
			this.m_lstItemMiscDataAndTemplet = null;
			this.m_lstItemMiscDataAndTemplet = new List<NKCPopupEmblemList.ItemMiscDataAndTemplet>();
			for (int k = 0; k < emblemData.Count; k++)
			{
				NKCPopupEmblemList.ItemMiscDataAndTemplet itemMiscDataAndTemplet = new NKCPopupEmblemList.ItemMiscDataAndTemplet();
				itemMiscDataAndTemplet.m_NKMItemMiscData = emblemData[k];
				itemMiscDataAndTemplet.m_NKMItemMiscTemplet = NKMItemManager.GetItemMiscTempletByID(emblemData[k].ItemID);
				this.m_lstItemMiscDataAndTemplet.Add(itemMiscDataAndTemplet);
			}
			this.m_lstItemMiscDataAndTemplet.Sort(new NKCPopupEmblemList.CompGradeAndID());
			if (bUseEmpty)
			{
				NKCPopupEmblemList.ItemMiscDataAndTemplet itemMiscDataAndTemplet2 = new NKCPopupEmblemList.ItemMiscDataAndTemplet();
				itemMiscDataAndTemplet2.m_NKMItemMiscData = null;
				itemMiscDataAndTemplet2.m_NKMItemMiscTemplet = null;
				this.m_lstItemMiscDataAndTemplet.Insert(0, itemMiscDataAndTemplet2);
			}
			if (this.m_lstItemMiscDataAndTemplet == null || this.m_lstItemMiscDataAndTemplet.Count <= 0)
			{
				NKCUtil.SetGameobjectActive(this.m_objNoSlotList, true);
				NKCUtil.SetGameobjectActive(this.m_objSlotList, false);
				NKCUtil.SetGameobjectActive(this.m_NKCPopupEmblemBigSlot, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objNoSlotList, false);
				NKCUtil.SetGameobjectActive(this.m_objSlotList, true);
				NKCUtil.SetGameobjectActive(this.m_NKCPopupEmblemBigSlot, true);
			}
			base.UIOpened(true);
			if (this.m_bFirstOpen)
			{
				this.m_lsrEmblem.PrepareCells(0);
				this.m_bFirstOpen = false;
			}
			this.m_lsrEmblem.TotalCount = this.m_lstItemMiscDataAndTemplet.Count;
			this.m_lsrEmblem.SetIndexPosition(0);
			this.m_lsrEmblem.velocity = new Vector2(0f, 0f);
		}

		// Token: 0x0600730C RID: 29452 RVA: 0x00263F78 File Offset: 0x00262178
		public void CloseEmblemListPopup()
		{
			base.Close();
		}

		// Token: 0x0600730D RID: 29453 RVA: 0x00263F80 File Offset: 0x00262180
		public void OnCloseBtn()
		{
			base.Close();
		}

		// Token: 0x0600730E RID: 29454 RVA: 0x00263F88 File Offset: 0x00262188
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.CheckNKCPopupEmblemChangeConfirmAndClose();
		}

		// Token: 0x0600730F RID: 29455 RVA: 0x00263F9C File Offset: 0x0026219C
		private void OnDestroy()
		{
		}

		// Token: 0x04005EFB RID: 24315
		public const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_FRIEND";

		// Token: 0x04005EFC RID: 24316
		public const string UI_ASSET_NAME = "NKM_UI_POPUP_EMBLEM";

		// Token: 0x04005EFD RID: 24317
		public NKCUIComStateButton m_csbtnOK;

		// Token: 0x04005EFE RID: 24318
		public NKCUIComStateButton m_csbtnCancel;

		// Token: 0x04005EFF RID: 24319
		public EventTrigger m_etBG;

		// Token: 0x04005F00 RID: 24320
		public NKCPopupEmblemBigSlot m_NKCPopupEmblemBigSlot;

		// Token: 0x04005F01 RID: 24321
		public GameObject m_objSlotList;

		// Token: 0x04005F02 RID: 24322
		public GameObject m_objNoSlotList;

		// Token: 0x04005F03 RID: 24323
		public LoopScrollRect m_lsrEmblem;

		// Token: 0x04005F04 RID: 24324
		public GridLayoutGroup m_GridLayoutGroup;

		// Token: 0x04005F05 RID: 24325
		private List<NKCPopupEmblemList.ItemMiscDataAndTemplet> m_lstItemMiscDataAndTemplet = new List<NKCPopupEmblemList.ItemMiscDataAndTemplet>();

		// Token: 0x04005F06 RID: 24326
		private bool m_bFirstOpen = true;

		// Token: 0x04005F07 RID: 24327
		private int m_SelectedID = -1;

		// Token: 0x04005F08 RID: 24328
		private NKCPopupEmblemList.dOnClickOK m_dOnClickOK;

		// Token: 0x04005F09 RID: 24329
		private int m_EquippedEmblemID = -1;

		// Token: 0x04005F0A RID: 24330
		private NKCPopupEmblemChangeConfirm m_NKCPopupEmblemChangeConfirm;

		// Token: 0x02001784 RID: 6020
		private class ItemMiscDataAndTemplet
		{
			// Token: 0x0400A702 RID: 42754
			public NKMItemMiscData m_NKMItemMiscData;

			// Token: 0x0400A703 RID: 42755
			public NKMItemMiscTemplet m_NKMItemMiscTemplet;
		}

		// Token: 0x02001785 RID: 6021
		// (Invoke) Token: 0x0600B38D RID: 45965
		public delegate void dOnClickOK(int id);

		// Token: 0x02001786 RID: 6022
		private class CompGradeAndID : IComparer<NKCPopupEmblemList.ItemMiscDataAndTemplet>
		{
			// Token: 0x0600B390 RID: 45968 RVA: 0x0036392C File Offset: 0x00361B2C
			public int Compare(NKCPopupEmblemList.ItemMiscDataAndTemplet x, NKCPopupEmblemList.ItemMiscDataAndTemplet y)
			{
				if (x == null || x.m_NKMItemMiscTemplet == null || x.m_NKMItemMiscData == null)
				{
					return 1;
				}
				if (y == null || y.m_NKMItemMiscTemplet == null || y.m_NKMItemMiscData == null)
				{
					return -1;
				}
				if (x.m_NKMItemMiscTemplet.m_NKM_ITEM_GRADE > y.m_NKMItemMiscTemplet.m_NKM_ITEM_GRADE)
				{
					return -1;
				}
				if (x.m_NKMItemMiscTemplet.m_NKM_ITEM_GRADE < y.m_NKMItemMiscTemplet.m_NKM_ITEM_GRADE)
				{
					return 1;
				}
				return x.m_NKMItemMiscTemplet.m_ItemMiscID.CompareTo(y.m_NKMItemMiscTemplet.m_ItemMiscID);
			}
		}
	}
}
