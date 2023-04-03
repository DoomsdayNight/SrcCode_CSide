using System;
using System.Collections.Generic;
using ClientPacket.Office;
using NKM;
using NKM.Templet.Office;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Office
{
	// Token: 0x02000AFB RID: 2811
	public class NKCUIPopupOfficePartyConfirm : NKCUIBase
	{
		// Token: 0x170014F1 RID: 5361
		// (get) Token: 0x06007F18 RID: 32536 RVA: 0x002AA480 File Offset: 0x002A8680
		public static NKCUIPopupOfficePartyConfirm Instance
		{
			get
			{
				if (NKCUIPopupOfficePartyConfirm.m_Instance == null)
				{
					NKCUIPopupOfficePartyConfirm.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPopupOfficePartyConfirm>("ab_ui_office", "AB_UI_POPUP_OFFICE_PARTY_READY", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPopupOfficePartyConfirm.CleanupInstance)).GetInstance<NKCUIPopupOfficePartyConfirm>();
					NKCUIPopupOfficePartyConfirm.m_Instance.InitUI();
				}
				return NKCUIPopupOfficePartyConfirm.m_Instance;
			}
		}

		// Token: 0x06007F19 RID: 32537 RVA: 0x002AA4CF File Offset: 0x002A86CF
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPopupOfficePartyConfirm.m_Instance != null && NKCUIPopupOfficePartyConfirm.m_Instance.IsOpen)
			{
				NKCUIPopupOfficePartyConfirm.m_Instance.Close();
			}
		}

		// Token: 0x06007F1A RID: 32538 RVA: 0x002AA4F4 File Offset: 0x002A86F4
		private static void CleanupInstance()
		{
			NKCUIPopupOfficePartyConfirm.m_Instance = null;
		}

		// Token: 0x170014F2 RID: 5362
		// (get) Token: 0x06007F1B RID: 32539 RVA: 0x002AA4FC File Offset: 0x002A86FC
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIPopupOfficePartyConfirm.m_Instance != null && NKCUIPopupOfficePartyConfirm.m_Instance.IsOpen;
			}
		}

		// Token: 0x170014F3 RID: 5363
		// (get) Token: 0x06007F1C RID: 32540 RVA: 0x002AA517 File Offset: 0x002A8717
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170014F4 RID: 5364
		// (get) Token: 0x06007F1D RID: 32541 RVA: 0x002AA51A File Offset: 0x002A871A
		public override string MenuName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06007F1E RID: 32542 RVA: 0x002AA521 File Offset: 0x002A8721
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007F1F RID: 32543 RVA: 0x002AA530 File Offset: 0x002A8730
		private void InitUI()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnClose, new UnityAction(base.Close));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnCancel, new UnityAction(base.Close));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnConfirm, new UnityAction(this.OnBtnComfirm));
			NKCUtil.SetHotkey(this.m_csbtnConfirm, HotkeyEventType.Confirm, null, false);
			foreach (NKCUISlot nkcuislot in this.m_lstSlotReward)
			{
				nkcuislot.Init();
			}
		}

		// Token: 0x06007F20 RID: 32544 RVA: 0x002AA5D4 File Offset: 0x002A87D4
		public void Open(int roomID, NKCUIPopupOfficePartyConfirm.OnComfirm onComfirm)
		{
			NKMOfficeRoom officeRoom = NKCScenManager.CurrentUserData().OfficeData.GetOfficeRoom(roomID);
			if (officeRoom == null)
			{
				Debug.LogError("OfficeRoomData null!");
				return;
			}
			this.m_roomID = roomID;
			this.dOnComfirm = onComfirm;
			this.SetRequiredResource();
			this.SetReward(officeRoom);
			base.UIOpened(true);
		}

		// Token: 0x06007F21 RID: 32545 RVA: 0x002AA624 File Offset: 0x002A8824
		private void SetReward(NKMOfficeRoom room)
		{
			NKMOfficeGradeTemplet nkmofficeGradeTemplet = NKMOfficeGradeTemplet.Find(room.grade);
			if (nkmofficeGradeTemplet == null)
			{
				Debug.LogError("GradeTemplet null!");
				NKCUtil.SetLabelText(this.m_lbDesc, "");
				for (int i = 0; i < this.m_lstSlotReward.Count; i++)
				{
					NKCUtil.SetGameobjectActive(this.m_lstSlotReward[i], false);
				}
				return;
			}
			string text = (!string.IsNullOrEmpty(room.name)) ? room.name : NKCUIOfficeMapFront.GetDefaultRoomName(room.id);
			NKCUtil.SetLabelText(this.m_lbDesc, NKCStringTable.GetString("SI_PF_OFFICE_PARTY_0", new object[]
			{
				text,
				nkmofficeGradeTemplet.PartyRewardLoyalty / 100
			}));
			NKCUISlot.SlotData item = NKCUISlot.SlotData.MakeRewardTypeData(nkmofficeGradeTemplet.PartyRewardType, nkmofficeGradeTemplet.PartyRewardId, nkmofficeGradeTemplet.PartyRewardValueMin, 0);
			List<NKCUISlot.SlotData> list = new List<NKCUISlot.SlotData>();
			list.Add(item);
			NKCUISlot.SetSlotListData(this.m_lstSlotReward, list, false, false, true, null, new NKCUISlot.SlotClickType[1]);
		}

		// Token: 0x06007F22 RID: 32546 RVA: 0x002AA718 File Offset: 0x002A8918
		private void SetRequiredResource()
		{
			NKMOfficeConst office = NKMCommonConst.Office;
			if (((office != null) ? office.PartyUseItem : null) != null)
			{
				this.m_csbtnConfirm.OnShow(true);
				this.m_csbtnConfirm.SetData(NKMCommonConst.Office.PartyUseItem.m_ItemMiscID, 1);
				NKCUtil.SetGameobjectActive(this.m_objCount, true);
				NKCUtil.SetLabelText(this.m_lbCount, NKCUtilString.GET_STRING_TOOLTIP_QUANTITY_ONE_PARAM, new object[]
				{
					NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(NKMCommonConst.Office.PartyUseItem.m_ItemMiscID)
				});
				return;
			}
			Debug.LogError("Party required item not set!");
			this.m_csbtnConfirm.OnShow(false);
		}

		// Token: 0x06007F23 RID: 32547 RVA: 0x002AA7BE File Offset: 0x002A89BE
		private void OnBtnComfirm()
		{
			base.Close();
			NKCUIPopupOfficePartyConfirm.OnComfirm onComfirm = this.dOnComfirm;
			if (onComfirm == null)
			{
				return;
			}
			onComfirm(this.m_roomID);
		}

		// Token: 0x04006BA2 RID: 27554
		private const string ASSET_BUNDLE_NAME = "ab_ui_office";

		// Token: 0x04006BA3 RID: 27555
		private const string UI_ASSET_NAME = "AB_UI_POPUP_OFFICE_PARTY_READY";

		// Token: 0x04006BA4 RID: 27556
		private static NKCUIPopupOfficePartyConfirm m_Instance;

		// Token: 0x04006BA5 RID: 27557
		private NKCUIPopupOfficePartyConfirm.OnComfirm dOnComfirm;

		// Token: 0x04006BA6 RID: 27558
		private int m_roomID;

		// Token: 0x04006BA7 RID: 27559
		public Text m_lbDesc;

		// Token: 0x04006BA8 RID: 27560
		public GameObject m_objCount;

		// Token: 0x04006BA9 RID: 27561
		public Text m_lbCount;

		// Token: 0x04006BAA RID: 27562
		public NKCUIComResourceButton m_csbtnConfirm;

		// Token: 0x04006BAB RID: 27563
		public NKCUIComStateButton m_csbtnCancel;

		// Token: 0x04006BAC RID: 27564
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x04006BAD RID: 27565
		public List<NKCUISlot> m_lstSlotReward;

		// Token: 0x02001885 RID: 6277
		// (Invoke) Token: 0x0600B61A RID: 46618
		public delegate void OnComfirm(int roomID);
	}
}
