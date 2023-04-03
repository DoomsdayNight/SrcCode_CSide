using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009D7 RID: 2519
	public class NKCUISelectionMisc : NKCUIBase
	{
		// Token: 0x17001271 RID: 4721
		// (get) Token: 0x06006BFF RID: 27647 RVA: 0x002344D0 File Offset: 0x002326D0
		public static NKCUISelectionMisc Instance
		{
			get
			{
				if (NKCUISelectionMisc.m_Instance == null)
				{
					NKCUISelectionMisc.m_Instance = NKCUIManager.OpenNewInstance<NKCUISelectionMisc>("AB_UI_NKM_UI_UNIT_SELECTION", "NKM_UI_MISC_SELECTION", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUISelectionMisc.CleanupInstance)).GetInstance<NKCUISelectionMisc>();
					NKCUISelectionMisc.m_Instance.InitUI();
				}
				return NKCUISelectionMisc.m_Instance;
			}
		}

		// Token: 0x17001272 RID: 4722
		// (get) Token: 0x06006C00 RID: 27648 RVA: 0x0023451F File Offset: 0x0023271F
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUISelectionMisc.m_Instance != null && NKCUISelectionMisc.m_Instance.IsOpen;
			}
		}

		// Token: 0x06006C01 RID: 27649 RVA: 0x0023453A File Offset: 0x0023273A
		public static void CheckInstanceAndClose()
		{
			if (NKCUISelectionMisc.m_Instance != null && NKCUISelectionMisc.m_Instance.IsOpen)
			{
				NKCUISelectionMisc.m_Instance.Close();
			}
		}

		// Token: 0x06006C02 RID: 27650 RVA: 0x0023455F File Offset: 0x0023275F
		private static void CleanupInstance()
		{
			NKCUISelectionMisc.m_Instance = null;
		}

		// Token: 0x17001273 RID: 4723
		// (get) Token: 0x06006C03 RID: 27651 RVA: 0x00234567 File Offset: 0x00232767
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001274 RID: 4724
		// (get) Token: 0x06006C04 RID: 27652 RVA: 0x0023456A File Offset: 0x0023276A
		public override string MenuName
		{
			get
			{
				if (this.m_NKM_ITEM_MISC_TYPE == NKM_ITEM_MISC_TYPE.IMT_CHOICE_MISC)
				{
					return NKCUtilString.GET_STRING_CHOICE_MISC;
				}
				return NKCUtilString.GET_STRING_USE_CHOICE;
			}
		}

		// Token: 0x06006C05 RID: 27653 RVA: 0x00234584 File Offset: 0x00232784
		private void InitUI()
		{
			this.m_loopScrollRectMisc.dOnGetObject += this.GetObject;
			this.m_loopScrollRectMisc.dOnReturnObject += this.ReturnObject;
			this.m_loopScrollRectMisc.dOnProvideData += this.ProvideData;
			this.m_loopScrollRectMisc.dOnRepopulate += this.CalculateContentRectSize;
			NKCUtil.SetScrollHotKey(this.m_loopScrollRectMisc, null);
		}

		// Token: 0x06006C06 RID: 27654 RVA: 0x002345F9 File Offset: 0x002327F9
		public override void CloseInternal()
		{
			this.m_lstRewardId = new List<int>();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06006C07 RID: 27655 RVA: 0x00234614 File Offset: 0x00232814
		public void Open(NKMItemMiscTemplet itemMiscTemplet)
		{
			if (itemMiscTemplet == null)
			{
				return;
			}
			this.m_NKMItemMiscTemplet = itemMiscTemplet;
			List<NKMRandomBoxItemTemplet> randomBoxItemTempletList = NKCRandomBoxManager.GetRandomBoxItemTempletList(itemMiscTemplet.m_RewardGroupID);
			if (randomBoxItemTempletList == null)
			{
				return;
			}
			for (int i = 0; i < randomBoxItemTempletList.Count; i++)
			{
				this.m_lstRewardId.Add(randomBoxItemTempletList[i].m_RewardID);
			}
			this.m_NKM_ITEM_MISC_TYPE = itemMiscTemplet.m_ItemMiscType;
			NKCUtil.SetGameobjectActive(this.m_objMiscChoice, this.m_NKM_ITEM_MISC_TYPE == NKM_ITEM_MISC_TYPE.IMT_CHOICE_MISC);
			NKCScenManager.CurrentUserData();
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.CalculateContentRectSize();
			this.m_lstRandomBoxItemTemplet = randomBoxItemTempletList;
			this.m_lstRandomBoxItemTemplet.Sort(new Comparison<NKMRandomBoxItemTemplet>(this.CompOrderList));
			NKCUtil.SetImageSprite(this.m_imgBannerMisc, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_UNIT_SELECTION_TEXTURE", itemMiscTemplet.m_BannerImage, false), false);
			this.m_loopScrollRectMisc.PrepareCells(0);
			this.m_loopScrollRectMisc.TotalCount = this.m_lstRandomBoxItemTemplet.Count;
			this.m_loopScrollRectMisc.RefreshCells(true);
			base.UIOpened(true);
		}

		// Token: 0x06006C08 RID: 27656 RVA: 0x00234710 File Offset: 0x00232910
		private void CalculateContentRectSize()
		{
			NKCUIComSafeArea safeArea = this.m_SafeArea;
			if (safeArea != null)
			{
				safeArea.SetSafeAreaBase();
			}
			Vector2 cellSize = this.m_trContentParentMisc.GetComponent<GridLayoutGroup>().cellSize;
			Vector2 spacing = this.m_trContentParentMisc.GetComponent<GridLayoutGroup>().spacing;
			NKCUtil.CalculateContentRectSize(this.m_loopScrollRectMisc, this.m_trContentParentMisc.GetComponent<GridLayoutGroup>(), 5, cellSize, spacing, false);
		}

		// Token: 0x06006C09 RID: 27657 RVA: 0x0023476B File Offset: 0x0023296B
		private int CompOrderList(NKMRandomBoxItemTemplet lItem, NKMRandomBoxItemTemplet rItem)
		{
			if (lItem.m_OrderList == rItem.m_OrderList)
			{
				return lItem.m_RewardID.CompareTo(rItem.m_RewardID);
			}
			return lItem.m_OrderList.CompareTo(rItem.m_OrderList);
		}

		// Token: 0x06006C0A RID: 27658 RVA: 0x002347A0 File Offset: 0x002329A0
		private RectTransform GetObject(int index)
		{
			NKCUISlot nkcuislot;
			if (this.m_stkMiscSlotPool.Count > 0)
			{
				nkcuislot = this.m_stkMiscSlotPool.Pop();
			}
			else
			{
				nkcuislot = UnityEngine.Object.Instantiate<NKCUISlot>(this.m_pfbMiscSlot);
			}
			NKCUtil.SetGameobjectActive(nkcuislot, true);
			this.m_lstVisibleMiscSlot.Add(nkcuislot);
			return nkcuislot.GetComponent<RectTransform>();
		}

		// Token: 0x06006C0B RID: 27659 RVA: 0x002347F0 File Offset: 0x002329F0
		private void ReturnObject(Transform go)
		{
			NKCUISlot component = go.GetComponent<NKCUISlot>();
			NKCUtil.SetGameobjectActive(go, false);
			go.SetParent(base.transform);
			if (component != null)
			{
				this.m_lstVisibleMiscSlot.Remove(component);
				this.m_stkMiscSlotPool.Push(component);
			}
		}

		// Token: 0x06006C0C RID: 27660 RVA: 0x0023483C File Offset: 0x00232A3C
		private void ProvideData(Transform tr, int idx)
		{
			if (this.m_lstRandomBoxItemTemplet.Count == 0)
			{
				return;
			}
			if (NKMItemManager.GetItemMiscTempletByID(this.m_lstRandomBoxItemTemplet[idx].m_RewardID) == null)
			{
				return;
			}
			NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeMiscItemData(this.m_lstRandomBoxItemTemplet[idx].m_RewardID, (long)this.m_lstRandomBoxItemTemplet[idx].TotalQuantity_Max, 0);
			NKCUISlot component = tr.GetComponent<NKCUISlot>();
			component.Init();
			component.SetData(data, true, true, true, new NKCUISlot.OnClick(this.OnSelectMiscSlot));
			component.SetCountRange((long)this.m_lstRandomBoxItemTemplet[idx].TotalQuantity_Min, (long)this.m_lstRandomBoxItemTemplet[idx].TotalQuantity_Max);
			component.SetHaveCount(NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(this.m_lstRandomBoxItemTemplet[idx].m_RewardID), false);
		}

		// Token: 0x06006C0D RID: 27661 RVA: 0x0023490C File Offset: 0x00232B0C
		public void OnSelectMiscSlot(NKCUISlot.SlotData slotData, bool bLocked)
		{
			NKCPopupSelectionConfirm.Instance.Open(this.m_NKMItemMiscTemplet, slotData.ID, slotData.Count, 0);
		}

		// Token: 0x040057C9 RID: 22473
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_UNIT_SELECTION";

		// Token: 0x040057CA RID: 22474
		private const string UI_ASSET_NAME = "NKM_UI_MISC_SELECTION";

		// Token: 0x040057CB RID: 22475
		private static NKCUISelectionMisc m_Instance;

		// Token: 0x040057CC RID: 22476
		public NKCUIComSafeArea m_SafeArea;

		// Token: 0x040057CD RID: 22477
		[Header("Misc")]
		public GameObject m_objMiscChoice;

		// Token: 0x040057CE RID: 22478
		public LoopScrollRect m_loopScrollRectMisc;

		// Token: 0x040057CF RID: 22479
		public Transform m_trContentParentMisc;

		// Token: 0x040057D0 RID: 22480
		public Image m_imgBannerMisc;

		// Token: 0x040057D1 RID: 22481
		[Header("프리팹")]
		public NKCUISlot m_pfbMiscSlot;

		// Token: 0x040057D2 RID: 22482
		private NKM_ITEM_MISC_TYPE m_NKM_ITEM_MISC_TYPE = NKM_ITEM_MISC_TYPE.IMT_CHOICE_UNIT;

		// Token: 0x040057D3 RID: 22483
		private List<int> m_lstRewardId = new List<int>();

		// Token: 0x040057D4 RID: 22484
		private List<NKMRandomBoxItemTemplet> m_lstRandomBoxItemTemplet = new List<NKMRandomBoxItemTemplet>();

		// Token: 0x040057D5 RID: 22485
		private List<NKCUISlot> m_lstVisibleMiscSlot = new List<NKCUISlot>();

		// Token: 0x040057D6 RID: 22486
		private Stack<NKCUISlot> m_stkMiscSlotPool = new Stack<NKCUISlot>();

		// Token: 0x040057D7 RID: 22487
		private NKMItemMiscTemplet m_NKMItemMiscTemplet;
	}
}
