using System;
using System.Collections.Generic;
using ClientPacket.Common;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A95 RID: 2709
	public class NKCUIPopupProfileFrameChange : NKCUIBase
	{
		// Token: 0x17001419 RID: 5145
		// (get) Token: 0x060077F2 RID: 30706 RVA: 0x0027D910 File Offset: 0x0027BB10
		public static NKCUIPopupProfileFrameChange Instance
		{
			get
			{
				if (NKCUIPopupProfileFrameChange.m_Instance == null)
				{
					NKCUIPopupProfileFrameChange.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPopupProfileFrameChange>("ab_ui_nkm_ui_popup_ok_cancel_box", "NKM_UI_POPUP_BORDER_CHANGE", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPopupProfileFrameChange.CleanupInstance)).GetInstance<NKCUIPopupProfileFrameChange>();
					NKCUIPopupProfileFrameChange.m_Instance.Init();
				}
				return NKCUIPopupProfileFrameChange.m_Instance;
			}
		}

		// Token: 0x060077F3 RID: 30707 RVA: 0x0027D95F File Offset: 0x0027BB5F
		private static void CleanupInstance()
		{
			NKCUIPopupProfileFrameChange.m_Instance = null;
		}

		// Token: 0x1700141A RID: 5146
		// (get) Token: 0x060077F4 RID: 30708 RVA: 0x0027D967 File Offset: 0x0027BB67
		public override NKCUIManager.eUIUnloadFlag UnloadFlag
		{
			get
			{
				return NKCUIManager.eUIUnloadFlag.DEFAULT;
			}
		}

		// Token: 0x1700141B RID: 5147
		// (get) Token: 0x060077F5 RID: 30709 RVA: 0x0027D96A File Offset: 0x0027BB6A
		public static bool HasInstance
		{
			get
			{
				return NKCUIPopupProfileFrameChange.m_Instance != null;
			}
		}

		// Token: 0x1700141C RID: 5148
		// (get) Token: 0x060077F6 RID: 30710 RVA: 0x0027D977 File Offset: 0x0027BB77
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIPopupProfileFrameChange.m_Instance != null && NKCUIPopupProfileFrameChange.m_Instance.IsOpen;
			}
		}

		// Token: 0x060077F7 RID: 30711 RVA: 0x0027D992 File Offset: 0x0027BB92
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPopupProfileFrameChange.m_Instance != null && NKCUIPopupProfileFrameChange.m_Instance.IsOpen)
			{
				NKCUIPopupProfileFrameChange.m_Instance.Close();
			}
		}

		// Token: 0x1700141D RID: 5149
		// (get) Token: 0x060077F8 RID: 30712 RVA: 0x0027D9B7 File Offset: 0x0027BBB7
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700141E RID: 5150
		// (get) Token: 0x060077F9 RID: 30713 RVA: 0x0027D9BA File Offset: 0x0027BBBA
		public override string MenuName
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_PROFILE_BORDER_CHANGE", false);
			}
		}

		// Token: 0x060077FA RID: 30714 RVA: 0x0027D9C7 File Offset: 0x0027BBC7
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x060077FB RID: 30715 RVA: 0x0027D9D8 File Offset: 0x0027BBD8
		private void Init()
		{
			NKCUtil.SetBindFunction(this.m_csbtnCancel, new UnityAction(base.Close));
			NKCUtil.SetBindFunction(this.m_csbtnOK, new UnityAction(this.OnBtnOK));
			NKCUtil.SetHotkey(this.m_csbtnOK, HotkeyEventType.Confirm, null, false);
			this.m_LoopScrollRect.dOnGetObject += this.GetSlot;
			this.m_LoopScrollRect.dOnReturnObject += this.ReturnSlot;
			this.m_LoopScrollRect.dOnProvideData += this.ProvideSlotData;
			this.m_LoopScrollRect.PrepareCells(0);
			NKCUtil.SetScrollHotKey(this.m_LoopScrollRect, null);
		}

		// Token: 0x060077FC RID: 30716 RVA: 0x0027DA7E File Offset: 0x0027BC7E
		public override void OnCloseInstance()
		{
			base.OnCloseInstance();
			List<int> lstFrameItemID = this.m_lstFrameItemID;
			if (lstFrameItemID != null)
			{
				lstFrameItemID.Clear();
			}
			this.m_lstFrameItemID = null;
		}

		// Token: 0x060077FD RID: 30717 RVA: 0x0027DA9E File Offset: 0x0027BC9E
		public void Open(NKMUserProfileData profileData, NKCUIPopupProfileFrameChange.OnSelectFrame onSelectFrame)
		{
			this.Open(profileData.commonProfile.mainUnitId, profileData.commonProfile.mainUnitSkinId, profileData.commonProfile.frameId, onSelectFrame);
		}

		// Token: 0x060077FE RID: 30718 RVA: 0x0027DAC8 File Offset: 0x0027BCC8
		public void Open(NKMCommonProfile commonProfile, NKCUIPopupProfileFrameChange.OnSelectFrame onSelectFrame)
		{
			this.Open(commonProfile.mainUnitId, commonProfile.mainUnitSkinId, commonProfile.frameId, onSelectFrame);
		}

		// Token: 0x060077FF RID: 30719 RVA: 0x0027DAE4 File Offset: 0x0027BCE4
		public void Open(int profileUnitID, int profileSkinID, int currentFrameID, NKCUIPopupProfileFrameChange.OnSelectFrame onSelectFrame)
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.PROFILE_FRAME, 0, 0))
			{
				return;
			}
			this.m_ProfileUnitID = profileUnitID;
			this.m_ProfileSkinID = profileSkinID;
			this.m_selectedFrame = currentFrameID;
			this.dOnSelectFrame = onSelectFrame;
			base.UIOpened(true);
			this.m_lstFrameItemID = this.GetFrameIDList();
			this.m_LoopScrollRect.TotalCount = this.m_lstFrameItemID.Count;
			this.m_LoopScrollRect.SetIndexPosition(0);
		}

		// Token: 0x06007800 RID: 30720 RVA: 0x0027DB4F File Offset: 0x0027BD4F
		private void OnSelectSlot(NKCUISlotProfile selectedSlot, int selectedFrame)
		{
			NKCUISlotProfile slotSelected = this.m_slotSelected;
			if (slotSelected != null)
			{
				slotSelected.SetSelected(false);
			}
			this.m_slotSelected = selectedSlot;
			if (selectedSlot != null)
			{
				selectedSlot.SetSelected(true);
			}
			this.m_selectedFrame = selectedFrame;
		}

		// Token: 0x06007801 RID: 30721 RVA: 0x0027DB7C File Offset: 0x0027BD7C
		private List<int> GetFrameIDList()
		{
			List<int> list = new List<int>();
			list.Add(0);
			NKMInventoryData inventoryData = NKCScenManager.CurrentUserData().m_InventoryData;
			foreach (NKMItemMiscTemplet nkmitemMiscTemplet in NKMItemMiscTemplet.Values)
			{
				if (nkmitemMiscTemplet.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_SELFIE_FRAME && inventoryData.GetCountMiscItem(nkmitemMiscTemplet.Key) > 0L)
				{
					list.Add(nkmitemMiscTemplet.Key);
				}
			}
			return list;
		}

		// Token: 0x06007802 RID: 30722 RVA: 0x0027DC00 File Offset: 0x0027BE00
		private void OnBtnOK()
		{
			base.Close();
			NKCUIPopupProfileFrameChange.OnSelectFrame onSelectFrame = this.dOnSelectFrame;
			if (onSelectFrame == null)
			{
				return;
			}
			onSelectFrame(this.m_selectedFrame);
		}

		// Token: 0x06007803 RID: 30723 RVA: 0x0027DC20 File Offset: 0x0027BE20
		private RectTransform GetSlot(int index)
		{
			if (this.m_stkSlot.Count > 0)
			{
				return this.m_stkSlot.Pop().GetComponent<RectTransform>();
			}
			NKCUISlotProfile nkcuislotProfile = UnityEngine.Object.Instantiate<NKCUISlotProfile>(this.m_pfbSlot);
			nkcuislotProfile.Init();
			NKCUtil.SetGameobjectActive(nkcuislotProfile, true);
			nkcuislotProfile.transform.localScale = Vector3.one;
			nkcuislotProfile.transform.SetParent(this.m_LoopScrollRect.content);
			return nkcuislotProfile.GetComponent<RectTransform>();
		}

		// Token: 0x06007804 RID: 30724 RVA: 0x0027DC90 File Offset: 0x0027BE90
		private void ReturnSlot(Transform go)
		{
			go.SetParent(this.m_rtSlotPool);
			NKCUISlotProfile component = go.GetComponent<NKCUISlotProfile>();
			if (component != null)
			{
				this.m_stkSlot.Push(component);
			}
		}

		// Token: 0x06007805 RID: 30725 RVA: 0x0027DCC8 File Offset: 0x0027BEC8
		private void ProvideSlotData(Transform tr, int idx)
		{
			if (this.m_lstFrameItemID == null)
			{
				tr.gameObject.SetActive(false);
				return;
			}
			if (this.m_lstFrameItemID.Count < idx || idx < 0)
			{
				tr.gameObject.SetActive(false);
				return;
			}
			int num = this.m_lstFrameItemID[idx];
			NKCUISlotProfile component = tr.GetComponent<NKCUISlotProfile>();
			if (component != null)
			{
				component.SetProfiledata(this.m_ProfileUnitID, this.m_ProfileSkinID, num, new NKCUISlotProfile.OnClick(this.OnSelectSlot));
				bool flag = num == this.m_selectedFrame;
				component.SetSelected(flag);
				if (flag)
				{
					this.m_slotSelected = component;
				}
			}
		}

		// Token: 0x04006489 RID: 25737
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_ok_cancel_box";

		// Token: 0x0400648A RID: 25738
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_BORDER_CHANGE";

		// Token: 0x0400648B RID: 25739
		private static NKCUIPopupProfileFrameChange m_Instance;

		// Token: 0x0400648C RID: 25740
		public NKCUISlotProfile m_pfbSlot;

		// Token: 0x0400648D RID: 25741
		public LoopScrollRect m_LoopScrollRect;

		// Token: 0x0400648E RID: 25742
		public NKCUIComStateButton m_csbtnCancel;

		// Token: 0x0400648F RID: 25743
		public NKCUIComStateButton m_csbtnOK;

		// Token: 0x04006490 RID: 25744
		private NKCUIPopupProfileFrameChange.OnSelectFrame dOnSelectFrame;

		// Token: 0x04006491 RID: 25745
		private int m_selectedFrame;

		// Token: 0x04006492 RID: 25746
		private int m_ProfileUnitID;

		// Token: 0x04006493 RID: 25747
		private int m_ProfileSkinID;

		// Token: 0x04006494 RID: 25748
		private Stack<NKCUISlotProfile> m_stkSlot = new Stack<NKCUISlotProfile>();

		// Token: 0x04006495 RID: 25749
		private NKCUISlotProfile m_slotSelected;

		// Token: 0x04006496 RID: 25750
		public Transform m_rtSlotPool;

		// Token: 0x04006497 RID: 25751
		private List<int> m_lstFrameItemID;

		// Token: 0x020017F1 RID: 6129
		// (Invoke) Token: 0x0600B4B0 RID: 46256
		public delegate void OnSelectFrame(int frameID);
	}
}
