using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009D9 RID: 2521
	public class NKCUISelectionSkin : NKCUIBase
	{
		// Token: 0x17001279 RID: 4729
		// (get) Token: 0x06006C21 RID: 27681 RVA: 0x00234D6C File Offset: 0x00232F6C
		public static NKCUISelectionSkin Instance
		{
			get
			{
				if (NKCUISelectionSkin.m_Instance == null)
				{
					NKCUISelectionSkin.m_Instance = NKCUIManager.OpenNewInstance<NKCUISelectionSkin>("AB_UI_NKM_UI_UNIT_SELECTION", "NKM_UI_SKIN_SELECTION", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUISelectionSkin.CleanupInstance)).GetInstance<NKCUISelectionSkin>();
					NKCUISelectionSkin.m_Instance.InitUI();
				}
				return NKCUISelectionSkin.m_Instance;
			}
		}

		// Token: 0x1700127A RID: 4730
		// (get) Token: 0x06006C22 RID: 27682 RVA: 0x00234DBB File Offset: 0x00232FBB
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUISelectionSkin.m_Instance != null && NKCUISelectionSkin.m_Instance.IsOpen;
			}
		}

		// Token: 0x06006C23 RID: 27683 RVA: 0x00234DD6 File Offset: 0x00232FD6
		public static void CheckInstanceAndClose()
		{
			if (NKCUISelectionSkin.m_Instance != null && NKCUISelectionSkin.m_Instance.IsOpen)
			{
				NKCUISelectionSkin.m_Instance.Close();
			}
		}

		// Token: 0x06006C24 RID: 27684 RVA: 0x00234DFB File Offset: 0x00232FFB
		private static void CleanupInstance()
		{
			NKCUISelectionSkin.m_Instance = null;
		}

		// Token: 0x1700127B RID: 4731
		// (get) Token: 0x06006C25 RID: 27685 RVA: 0x00234E03 File Offset: 0x00233003
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x1700127C RID: 4732
		// (get) Token: 0x06006C26 RID: 27686 RVA: 0x00234E06 File Offset: 0x00233006
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_USE_CHOICE;
			}
		}

		// Token: 0x06006C27 RID: 27687 RVA: 0x00234E10 File Offset: 0x00233010
		private void InitUI()
		{
			this.m_loopScrollRect.dOnGetObject += this.GetObject;
			this.m_loopScrollRect.dOnReturnObject += this.ReturnObject;
			this.m_loopScrollRect.dOnProvideData += this.ProvideData;
			this.m_loopScrollRect.dOnRepopulate += this.CalculateContentRectSize;
			NKCUtil.SetScrollHotKey(this.m_loopScrollRect, null);
		}

		// Token: 0x06006C28 RID: 27688 RVA: 0x00234E85 File Offset: 0x00233085
		public override void CloseInternal()
		{
			this.m_lstRewardId.Clear();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06006C29 RID: 27689 RVA: 0x00234E9E File Offset: 0x0023309E
		public override void OnCloseInstance()
		{
			this.m_NKMItemMiscTemplet = null;
		}

		// Token: 0x06006C2A RID: 27690 RVA: 0x00234EA8 File Offset: 0x002330A8
		public void Open(NKMItemMiscTemplet itemMiscTemplet)
		{
			this.m_NKMItemMiscTemplet = itemMiscTemplet;
			this.m_lstRewardId.Clear();
			List<NKMRandomBoxItemTemplet> randomBoxItemTempletList = NKCRandomBoxManager.GetRandomBoxItemTempletList(this.m_NKMItemMiscTemplet.m_RewardGroupID);
			if (randomBoxItemTempletList == null)
			{
				return;
			}
			for (int i = 0; i < randomBoxItemTempletList.Count; i++)
			{
				this.m_lstRewardId.Add(randomBoxItemTempletList[i].m_RewardID);
			}
			base.gameObject.SetActive(true);
			this.CalculateContentRectSize();
			if (!string.IsNullOrEmpty(itemMiscTemplet.m_BannerImage))
			{
				NKCUtil.SetImageSprite(this.m_imgBannerMisc, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_UNIT_SELECTION_TEXTURE", itemMiscTemplet.m_BannerImage, false), false);
			}
			this.m_loopScrollRect.PrepareCells(0);
			this.m_loopScrollRect.TotalCount = this.m_lstRewardId.Count;
			this.m_loopScrollRect.RefreshCells(true);
			base.UIOpened(true);
		}

		// Token: 0x06006C2B RID: 27691 RVA: 0x00234F78 File Offset: 0x00233178
		private void CalculateContentRectSize()
		{
			NKCUIComSafeArea safeArea = this.m_SafeArea;
			if (safeArea != null)
			{
				safeArea.SetSafeAreaBase();
			}
			GridLayoutGroup component = this.m_trContentParent.GetComponent<GridLayoutGroup>();
			int constraintCount = component.constraintCount;
			Vector2 cellSize = component.cellSize;
			Vector2 spacing = component.spacing;
			NKCUtil.CalculateContentRectSizeHorizontal(this.m_loopScrollRect, this.m_trContentParent.GetComponent<GridLayoutGroup>(), constraintCount, cellSize, spacing, false);
		}

		// Token: 0x06006C2C RID: 27692 RVA: 0x00234FD0 File Offset: 0x002331D0
		public void OnSelectSkinSlot(int skinId)
		{
			NKCPopupSelectionConfirm.Instance.Open(this.m_NKMItemMiscTemplet, skinId, 1L, 0);
		}

		// Token: 0x06006C2D RID: 27693 RVA: 0x00234FE8 File Offset: 0x002331E8
		private RectTransform GetObject(int index)
		{
			NKCUISkinSelectionSlot nkcuiskinSelectionSlot;
			if (this.m_stkSlotPool.Count > 0)
			{
				nkcuiskinSelectionSlot = this.m_stkSlotPool.Pop();
			}
			else
			{
				nkcuiskinSelectionSlot = UnityEngine.Object.Instantiate<NKCUISkinSelectionSlot>(this.m_pfbSlot);
				nkcuiskinSelectionSlot.Init();
			}
			NKCUtil.SetGameobjectActive(nkcuiskinSelectionSlot, true);
			this.m_lstVisibleSlot.Add(nkcuiskinSelectionSlot);
			return nkcuiskinSelectionSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06006C2E RID: 27694 RVA: 0x00235040 File Offset: 0x00233240
		private void ReturnObject(Transform go)
		{
			NKCUISkinSelectionSlot component = go.GetComponent<NKCUISkinSelectionSlot>();
			NKCUtil.SetGameobjectActive(component, false);
			go.SetParent(base.transform);
			if (component != null)
			{
				this.m_lstVisibleSlot.Remove(component);
				this.m_stkSlotPool.Push(component);
			}
		}

		// Token: 0x06006C2F RID: 27695 RVA: 0x0023508C File Offset: 0x0023328C
		private void ProvideData(Transform tr, int idx)
		{
			if (idx < 0 || idx >= this.m_lstRewardId.Count)
			{
				Debug.LogError("out of index");
				NKCUtil.SetGameobjectActive(tr, false);
				return;
			}
			NKCUISkinSelectionSlot component = tr.GetComponent<NKCUISkinSelectionSlot>();
			int skinID = this.m_lstRewardId[idx];
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(skinID);
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			bool haveSkin = nkmuserData != null && nkmuserData.m_InventoryData.HasItemSkin(skinID);
			component.SetData(skinTemplet, haveSkin, new NKCUISkinSelectionSlot.OnClick(this.OnSelectSkinSlot));
			NKCUtil.SetGameobjectActive(component.gameObject, true);
		}

		// Token: 0x040057E4 RID: 22500
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_UNIT_SELECTION";

		// Token: 0x040057E5 RID: 22501
		private const string UI_ASSET_NAME = "NKM_UI_SKIN_SELECTION";

		// Token: 0x040057E6 RID: 22502
		private static NKCUISelectionSkin m_Instance;

		// Token: 0x040057E7 RID: 22503
		public NKCUIComSafeArea m_SafeArea;

		// Token: 0x040057E8 RID: 22504
		public LoopScrollRect m_loopScrollRect;

		// Token: 0x040057E9 RID: 22505
		public Transform m_trContentParent;

		// Token: 0x040057EA RID: 22506
		public Image m_imgBannerMisc;

		// Token: 0x040057EB RID: 22507
		[Header("������")]
		public NKCUISkinSelectionSlot m_pfbSlot;

		// Token: 0x040057EC RID: 22508
		private List<int> m_lstRewardId = new List<int>();

		// Token: 0x040057ED RID: 22509
		private List<NKCUISkinSelectionSlot> m_lstVisibleSlot = new List<NKCUISkinSelectionSlot>();

		// Token: 0x040057EE RID: 22510
		private Stack<NKCUISkinSelectionSlot> m_stkSlotPool = new Stack<NKCUISkinSelectionSlot>();

		// Token: 0x040057EF RID: 22511
		private NKMItemMiscTemplet m_NKMItemMiscTemplet;
	}
}
