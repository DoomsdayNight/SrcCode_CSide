using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009D8 RID: 2520
	public class NKCUISelectionOperator : NKCUIBase
	{
		// Token: 0x17001275 RID: 4725
		// (get) Token: 0x06006C0F RID: 27663 RVA: 0x00234968 File Offset: 0x00232B68
		public static NKCUISelectionOperator Instance
		{
			get
			{
				if (NKCUISelectionOperator.m_Instance == null)
				{
					NKCUISelectionOperator.m_Instance = NKCUIManager.OpenNewInstance<NKCUISelectionOperator>("AB_UI_NKM_UI_UNIT_SELECTION", "NKM_UI_OPERATOR_SELECTION", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUISelectionOperator.CleanupInstance)).GetInstance<NKCUISelectionOperator>();
					NKCUISelectionOperator.m_Instance.InitUI();
				}
				return NKCUISelectionOperator.m_Instance;
			}
		}

		// Token: 0x17001276 RID: 4726
		// (get) Token: 0x06006C10 RID: 27664 RVA: 0x002349B7 File Offset: 0x00232BB7
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUISelectionOperator.m_Instance != null && NKCUISelectionOperator.m_Instance.IsOpen;
			}
		}

		// Token: 0x06006C11 RID: 27665 RVA: 0x002349D2 File Offset: 0x00232BD2
		public static void CheckInstanceAndClose()
		{
			if (NKCUISelectionOperator.m_Instance != null && NKCUISelectionOperator.m_Instance.IsOpen)
			{
				NKCUISelectionOperator.m_Instance.Close();
			}
		}

		// Token: 0x06006C12 RID: 27666 RVA: 0x002349F7 File Offset: 0x00232BF7
		private static void CleanupInstance()
		{
			NKCUISelectionOperator.m_Instance = null;
		}

		// Token: 0x17001277 RID: 4727
		// (get) Token: 0x06006C13 RID: 27667 RVA: 0x002349FF File Offset: 0x00232BFF
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001278 RID: 4728
		// (get) Token: 0x06006C14 RID: 27668 RVA: 0x00234A02 File Offset: 0x00232C02
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_USE_CHOICE;
			}
		}

		// Token: 0x06006C15 RID: 27669 RVA: 0x00234A0C File Offset: 0x00232C0C
		private void InitUI()
		{
			this.m_loopScrollRect.dOnGetObject += this.GetObject;
			this.m_loopScrollRect.dOnReturnObject += this.ReturnObject;
			this.m_loopScrollRect.dOnProvideData += this.ProvideData;
			this.m_loopScrollRect.dOnRepopulate += this.CalculateContentRectSize;
			NKCUtil.SetScrollHotKey(this.m_loopScrollRect, null);
		}

		// Token: 0x06006C16 RID: 27670 RVA: 0x00234A81 File Offset: 0x00232C81
		public override void CloseInternal()
		{
			this.m_lstRewardId = new List<int>();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06006C17 RID: 27671 RVA: 0x00234A9A File Offset: 0x00232C9A
		public override void OnCloseInstance()
		{
			this.m_NKMItemMiscTemplet = null;
		}

		// Token: 0x06006C18 RID: 27672 RVA: 0x00234AA4 File Offset: 0x00232CA4
		public void Open(NKMItemMiscTemplet itemMiscTemplet)
		{
			if (itemMiscTemplet == null)
			{
				return;
			}
			this.m_NKMItemMiscTemplet = itemMiscTemplet;
			List<NKMRandomBoxItemTemplet> randomBoxItemTempletList = NKCRandomBoxManager.GetRandomBoxItemTempletList(this.m_NKMItemMiscTemplet.m_RewardGroupID);
			if (randomBoxItemTempletList == null)
			{
				return;
			}
			for (int i = 0; i < randomBoxItemTempletList.Count; i++)
			{
				this.m_lstRewardId.Add(randomBoxItemTempletList[i].m_RewardID);
			}
			NKCScenManager.CurrentUserData();
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.CalculateContentRectSize();
			this.SetChoiceList();
			NKCUtil.SetImageSprite(this.m_imgBanner, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_UNIT_SELECTION_TEXTURE", itemMiscTemplet.m_BannerImage, false), false);
			base.UIOpened(true);
		}

		// Token: 0x06006C19 RID: 27673 RVA: 0x00234B3C File Offset: 0x00232D3C
		private void CalculateContentRectSize()
		{
			NKCUIComSafeArea safeArea = this.m_SafeArea;
			if (safeArea != null)
			{
				safeArea.SetSafeAreaBase();
			}
			int minColumn = 4;
			Vector2 cellSize = this.m_trContentParent.GetComponent<GridLayoutGroup>().cellSize;
			Vector2 spacing = this.m_trContentParent.GetComponent<GridLayoutGroup>().spacing;
			NKCUtil.CalculateContentRectSize(this.m_loopScrollRect, this.m_trContentParent.GetComponent<GridLayoutGroup>(), minColumn, cellSize, spacing, false);
		}

		// Token: 0x06006C1A RID: 27674 RVA: 0x00234B99 File Offset: 0x00232D99
		private int CompOrderList(NKMRandomBoxItemTemplet lItem, NKMRandomBoxItemTemplet rItem)
		{
			if (lItem.m_OrderList == rItem.m_OrderList)
			{
				return lItem.m_RewardID.CompareTo(rItem.m_RewardID);
			}
			return lItem.m_OrderList.CompareTo(rItem.m_OrderList);
		}

		// Token: 0x06006C1B RID: 27675 RVA: 0x00234BCC File Offset: 0x00232DCC
		private RectTransform GetObject(int index)
		{
			NKCUIOperatorSelectListSlot nkcuioperatorSelectListSlot;
			if (this.m_stkSlotPool.Count > 0)
			{
				nkcuioperatorSelectListSlot = this.m_stkSlotPool.Pop();
			}
			else
			{
				nkcuioperatorSelectListSlot = UnityEngine.Object.Instantiate<NKCUIOperatorSelectListSlot>(this.m_pfbSlot);
				nkcuioperatorSelectListSlot.Init(false);
			}
			NKCUtil.SetGameobjectActive(nkcuioperatorSelectListSlot, true);
			this.m_lstVisibleSlot.Add(nkcuioperatorSelectListSlot);
			return nkcuioperatorSelectListSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06006C1C RID: 27676 RVA: 0x00234C24 File Offset: 0x00232E24
		private void ReturnObject(Transform go)
		{
			NKCUIOperatorSelectListSlot component = go.GetComponent<NKCUIOperatorSelectListSlot>();
			NKCUtil.SetGameobjectActive(component, false);
			go.SetParent(base.transform);
			if (component != null)
			{
				this.m_lstVisibleSlot.Remove(component);
				this.m_stkSlotPool.Push(component);
			}
		}

		// Token: 0x06006C1D RID: 27677 RVA: 0x00234C70 File Offset: 0x00232E70
		private void ProvideData(Transform tr, int idx)
		{
			if (idx < 0 || idx >= this.m_lstRewardId.Count)
			{
				Debug.LogError("out of index");
				NKCUtil.SetGameobjectActive(tr, false);
				return;
			}
			NKCUIOperatorSelectListSlot component = tr.GetComponent<NKCUIOperatorSelectListSlot>();
			int unitID = this.m_lstRewardId[idx];
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
			int operatorCountByID = NKCScenManager.CurrentUserData().m_ArmyData.GetOperatorCountByID(unitID);
			component.SetOperatorData(unitTempletBase, 1, true, new NKCUIUnitSelectListSlotBase.OnSelectThisOperatorSlot(this.OnSelectSlot));
			component.SetHaveCount(operatorCountByID, false);
			NKCUtil.SetGameobjectActive(component.gameObject, true);
		}

		// Token: 0x06006C1E RID: 27678 RVA: 0x00234CF4 File Offset: 0x00232EF4
		private void SetChoiceList()
		{
			this.m_loopScrollRect.PrepareCells(0);
			this.m_loopScrollRect.TotalCount = this.m_lstRewardId.Count;
			this.m_loopScrollRect.RefreshCells(true);
		}

		// Token: 0x06006C1F RID: 27679 RVA: 0x00234D24 File Offset: 0x00232F24
		public void OnSelectSlot(NKMOperator operatorData, NKMUnitTempletBase unitTempletBase, NKMDeckIndex deckIndex, NKCUnitSortSystem.eUnitState slotState, NKCUIUnitSelectList.eUnitSlotSelectState unitSlotSelectState)
		{
			if (unitTempletBase != null)
			{
				NKCPopupSelectionConfirm.Instance.Open(this.m_NKMItemMiscTemplet, unitTempletBase.m_UnitID, 1L, 0);
			}
		}

		// Token: 0x040057D8 RID: 22488
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_UNIT_SELECTION";

		// Token: 0x040057D9 RID: 22489
		private const string UI_ASSET_NAME = "NKM_UI_OPERATOR_SELECTION";

		// Token: 0x040057DA RID: 22490
		private static NKCUISelectionOperator m_Instance;

		// Token: 0x040057DB RID: 22491
		public NKCUIComSafeArea m_SafeArea;

		// Token: 0x040057DC RID: 22492
		[Header("오퍼레이터")]
		public LoopScrollRect m_loopScrollRect;

		// Token: 0x040057DD RID: 22493
		public Transform m_trContentParent;

		// Token: 0x040057DE RID: 22494
		public Image m_imgBanner;

		// Token: 0x040057DF RID: 22495
		[Header("프리팹")]
		public NKCUIOperatorSelectListSlot m_pfbSlot;

		// Token: 0x040057E0 RID: 22496
		private List<int> m_lstRewardId = new List<int>();

		// Token: 0x040057E1 RID: 22497
		private List<NKCUIOperatorSelectListSlot> m_lstVisibleSlot = new List<NKCUIOperatorSelectListSlot>();

		// Token: 0x040057E2 RID: 22498
		private Stack<NKCUIOperatorSelectListSlot> m_stkSlotPool = new Stack<NKCUIOperatorSelectListSlot>();

		// Token: 0x040057E3 RID: 22499
		private NKMItemMiscTemplet m_NKMItemMiscTemplet;
	}
}
