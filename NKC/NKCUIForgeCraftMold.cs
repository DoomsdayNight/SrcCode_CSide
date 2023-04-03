using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cs.Core.Util;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000992 RID: 2450
	public class NKCUIForgeCraftMold : NKCUIBase
	{
		// Token: 0x170011C4 RID: 4548
		// (get) Token: 0x06006587 RID: 25991 RVA: 0x00204704 File Offset: 0x00202904
		public static NKCUIForgeCraftMold Instance
		{
			get
			{
				if (NKCUIForgeCraftMold.m_Instance == null)
				{
					NKCUIForgeCraftMold.m_Instance = NKCUIManager.OpenNewInstance<NKCUIForgeCraftMold>("ab_ui_nkm_ui_factory", "NKM_UI_FACTORY_CRAFT_MOLD", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIForgeCraftMold.CleanupInstance)).GetInstance<NKCUIForgeCraftMold>();
					NKCUIForgeCraftMold.m_Instance.InitUI();
				}
				return NKCUIForgeCraftMold.m_Instance;
			}
		}

		// Token: 0x06006588 RID: 25992 RVA: 0x00204753 File Offset: 0x00202953
		private static void CleanupInstance()
		{
			NKCUIForgeCraftMold.m_Instance = null;
		}

		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x06006589 RID: 25993 RVA: 0x0020475B File Offset: 0x0020295B
		public static bool HasInstance
		{
			get
			{
				return NKCUIForgeCraftMold.m_Instance != null;
			}
		}

		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x0600658A RID: 25994 RVA: 0x00204768 File Offset: 0x00202968
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIForgeCraftMold.m_Instance != null && NKCUIForgeCraftMold.m_Instance.IsOpen;
			}
		}

		// Token: 0x0600658B RID: 25995 RVA: 0x00204783 File Offset: 0x00202983
		public static void CheckInstanceAndClose()
		{
			if (NKCUIForgeCraftMold.m_Instance != null && NKCUIForgeCraftMold.m_Instance.IsOpen)
			{
				NKCUIForgeCraftMold.m_Instance.Close();
			}
		}

		// Token: 0x0600658C RID: 25996 RVA: 0x002047A8 File Offset: 0x002029A8
		private void OnDestroy()
		{
			NKCUIForgeCraftMold.m_Instance = null;
		}

		// Token: 0x170011C7 RID: 4551
		// (get) Token: 0x0600658D RID: 25997 RVA: 0x002047B0 File Offset: 0x002029B0
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_FORGE_CRAFT_MOLD;
			}
		}

		// Token: 0x170011C8 RID: 4552
		// (get) Token: 0x0600658E RID: 25998 RVA: 0x002047B7 File Offset: 0x002029B7
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170011C9 RID: 4553
		// (get) Token: 0x0600658F RID: 25999 RVA: 0x002047BA File Offset: 0x002029BA
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return new List<int>
				{
					1012,
					1,
					2,
					101
				};
			}
		}

		// Token: 0x06006590 RID: 26000 RVA: 0x002047E2 File Offset: 0x002029E2
		public void InitUI()
		{
			base.gameObject.SetActive(false);
			this.InitLoopScrollRect();
			this.InitFunctionButton();
		}

		// Token: 0x06006591 RID: 26001 RVA: 0x002047FC File Offset: 0x002029FC
		private void InitLoopScrollRect()
		{
			this.m_LoopScrollRect.dOnGetObject += this.GetSlot;
			this.m_LoopScrollRect.dOnReturnObject += this.ReturnSlot;
			this.m_LoopScrollRect.dOnProvideData += this.ProvideData;
			NKCUtil.SetScrollHotKey(this.m_LoopScrollRect, null);
			int minColumn = 2;
			Vector2 vSlotSize = this.m_vSlotSize;
			Vector2 vSlotSpacing = this.m_vSlotSpacing;
			if (this.m_SafeArea != null)
			{
				this.m_SafeArea.SetSafeAreaBase();
			}
			NKCUtil.CalculateContentRectSize(this.m_LoopScrollRect, this.m_GridLayoutGroup, minColumn, vSlotSize, vSlotSpacing, false);
		}

		// Token: 0x06006592 RID: 26002 RVA: 0x0020489C File Offset: 0x00202A9C
		public RectTransform GetSlot(int index)
		{
			NKCUIForgeCraftMoldSlot newInstance = NKCUIForgeCraftMoldSlot.GetNewInstance(this.m_ContentTransform, new NKCUIForgeCraftMoldSlot.OnClickMoldSlot(this.OnClickMoldSlot));
			if (newInstance != null)
			{
				return newInstance.GetComponent<RectTransform>();
			}
			return null;
		}

		// Token: 0x06006593 RID: 26003 RVA: 0x002048D4 File Offset: 0x00202AD4
		public void ReturnSlot(Transform tr)
		{
			NKCUIForgeCraftMoldSlot component = tr.GetComponent<NKCUIForgeCraftMoldSlot>();
			tr.SetParent(null);
			if (component != null)
			{
				component.DestoryInstance();
				return;
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x06006594 RID: 26004 RVA: 0x0020490C File Offset: 0x00202B0C
		public void ProvideData(Transform tr, int index)
		{
			NKCUIForgeCraftMoldSlot component = tr.GetComponent<NKCUIForgeCraftMoldSlot>();
			if (component != null)
			{
				component.SetData(index);
			}
		}

		// Token: 0x06006595 RID: 26005 RVA: 0x00204930 File Offset: 0x00202B30
		private void OnClickMoldSlot(NKMMoldItemData cNKMMoldItemData)
		{
			NKCPopupForgeCraft.Instance.Open(cNKMMoldItemData, this.m_Index);
		}

		// Token: 0x06006596 RID: 26006 RVA: 0x00204943 File Offset: 0x00202B43
		private void ProcessUIFromCurrentDisplayedSortData()
		{
			if (this.m_MoldSortSystem != null)
			{
				NKCUtil.SetLabelText(this.m_lbSortType, this.m_MoldSortSystem.GetSortName());
				NKCUtil.SetLabelText(this.m_lbSelectedSortType, this.m_MoldSortSystem.GetSortName());
			}
		}

		// Token: 0x06006597 RID: 26007 RVA: 0x0020497C File Offset: 0x00202B7C
		public NKMMoldItemData GetSortedMoldItemData(int index)
		{
			if ((this.m_MoldSortSystem != null && this.m_MoldSortSystem.lstSortedList != null && this.m_MoldSortSystem.lstSortedList.Count > index) || index >= 0)
			{
				return this.m_MoldSortSystem.lstSortedList[index];
			}
			return null;
		}

		// Token: 0x06006598 RID: 26008 RVA: 0x002049C8 File Offset: 0x00202BC8
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			if (NKCUIForgeCraftMold.IsInstanceOpen && this.m_MoldSortSystem != null)
			{
				this.m_MoldSortSystem.Update(this.m_eTapType);
				this.UpdateLoopScroll();
			}
		}

		// Token: 0x06006599 RID: 26009 RVA: 0x002049F0 File Offset: 0x00202BF0
		public override void OnCompanyBuffUpdate(NKMUserData userData)
		{
			if (NKCUIForgeCraftMold.IsInstanceOpen && this.m_MoldSortSystem != null)
			{
				this.m_MoldSortSystem.Update(this.m_eTapType);
				this.UpdateLoopScroll();
			}
		}

		// Token: 0x0600659A RID: 26010 RVA: 0x00204A18 File Offset: 0x00202C18
		public void Open(int index)
		{
			this.m_Index = index;
			if (this.m_bFirstOpen)
			{
				this.m_bFirstOpen = false;
				this.m_LoopScrollRect.PrepareCells(0);
				this.PrepareUI();
			}
			if (this.m_MoldSortSystem == null)
			{
				this.m_MoldSortSystem = new NKCMoldSortSystem(NKCMoldSortSystem.GetDefaultSortOption(this.m_defaultSortKey));
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_objSortSelected, false);
			this.OnSelectTab(this.m_eTapType);
			GameObject gameObject = NKCUIManager.OpenUI("NUM_FACTORY_BG");
			if (gameObject != null)
			{
				NKCCamera.RescaleRectToCameraFrustrum(gameObject.GetComponent<RectTransform>(), NKCCamera.GetCamera(), new Vector2(200f, 200f), -1000f, NKCCamera.FitMode.FitAuto, NKCCamera.ScaleMode.Scale);
			}
			this.m_setFilterOption.Clear();
			NKCUtil.SetGameobjectActive(this.m_objFilterSelected, this.m_setFilterOption.Count > 0);
			base.UIOpened(true);
		}

		// Token: 0x0600659B RID: 26011 RVA: 0x00204AF4 File Offset: 0x00202CF4
		private void PrepareUI()
		{
			GameObject orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<GameObject>("ab_ui_nkm_ui_factory", "NKM_UI_FACTORY_CRAFT_TAB", false);
			this.m_lstMoldTab.Clear();
			this.m_lstMoldTab = NKMItemManager.MoldTapTemplet.Values.ToList<NKCRandomMoldTabTemplet>();
			if (this.m_lstMoldTab.Count > 0 && orLoadAssetResource != null)
			{
				this.m_lstMoldTab.Sort((NKCRandomMoldTabTemplet x, NKCRandomMoldTabTemplet y) => x.m_TabOrder.CompareTo(y.m_TabOrder));
				this.m_eTapType = this.m_lstMoldTab[0].m_MoldTabID;
				this.m_defaultSortKey = this.m_lstMoldTab[0].m_MoldTab_Sort[0];
				for (int i = 0; i < this.m_lstMoldTab.Count; i++)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(orLoadAssetResource);
					if (NKCUtil.IsNullObject<GameObject>(gameObject, "PrepareUI - 탭 오브젝트를 생성할 수 없습니다."))
					{
						return;
					}
					gameObject.transform.SetParent(this.m_rtNKM_UI_FACTORY_CRAFT_LEFT, false);
					gameObject.GetComponent<RectTransform>();
					NKCUIComToggle component = gameObject.GetComponent<NKCUIComToggle>();
					if (!this.m_lstMoldTab[i].EnableByTag)
					{
						NKCUtil.SetGameobjectActive(component, false);
					}
					if (component != null && this.m_ctglGroup_FactoryCraftLeft != null)
					{
						component.SetToggleGroup(this.m_ctglGroup_FactoryCraftLeft);
						component.OnValueChanged.RemoveAllListeners();
						NKM_CRAFT_TAB_TYPE TabType = this.m_lstMoldTab[i].m_MoldTabID;
						component.OnValueChanged.AddListener(delegate(bool val)
						{
							if (val)
							{
								this.OnSelectTab(TabType);
							}
						});
						if (!this.m_dicTabToggle.ContainsKey(TabType))
						{
							this.m_dicTabToggle.Add(TabType, component);
						}
						component.Select(this.m_lstMoldTab[i].m_MoldTabID == this.m_eTapType, true, false);
					}
					this.SetCraftTabIcon(gameObject, "NKM_UI_CRAFT_MENU_BUTTON_ON/NKM_UI_CRAFT_MENU_ICON", this.m_lstMoldTab[i].m_MoldTabIconName);
					this.SetCraftTabIcon(gameObject, "NKM_UI_CRAFT_MENU_BUTTON_OFF/NKM_UI_CRAFT_MENU_ICON", this.m_lstMoldTab[i].m_MoldTabIconName);
					this.SetCraftTabName(gameObject, "NKM_UI_CRAFT_MENU_BUTTON_ON/NKM_UI_CRAFT_MENU_TEXT", this.m_lstMoldTab[i].m_MoldTabName);
					this.SetCraftTabName(gameObject, "NKM_UI_CRAFT_MENU_BUTTON_OFF/NKM_UI_CRAFT_MENU_TEXT", this.m_lstMoldTab[i].m_MoldTabName);
				}
			}
		}

		// Token: 0x0600659C RID: 26012 RVA: 0x00204D4C File Offset: 0x00202F4C
		private bool SetCraftTabIcon(GameObject rootObj, string targetPath, string iconName)
		{
			if (rootObj == null)
			{
				return false;
			}
			GameObject gameObject = rootObj.transform.Find(targetPath).gameObject;
			if (gameObject == null)
			{
				return false;
			}
			Image component = gameObject.GetComponent<Image>();
			if (component == null)
			{
				return false;
			}
			Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_factory_sprite", iconName, false);
			if (orLoadAssetResource == null)
			{
				return false;
			}
			NKCUtil.SetImageSprite(component, orLoadAssetResource, false);
			return true;
		}

		// Token: 0x0600659D RID: 26013 RVA: 0x00204DB4 File Offset: 0x00202FB4
		private bool SetCraftTabName(GameObject rootObj, string targetPath, string title)
		{
			if (rootObj == null)
			{
				return false;
			}
			GameObject gameObject = rootObj.transform.Find(targetPath).gameObject;
			if (gameObject == null)
			{
				return false;
			}
			Text component = gameObject.GetComponent<Text>();
			if (component == null)
			{
				return false;
			}
			NKCUtil.SetLabelText(component, NKCStringTable.GetString(title, false));
			return true;
		}

		// Token: 0x0600659E RID: 26014 RVA: 0x00204E09 File Offset: 0x00203009
		public void SelectCraftTab(NKM_CRAFT_TAB_TYPE tabType)
		{
			if (this.m_dicTabToggle.ContainsKey(tabType))
			{
				this.m_dicTabToggle[tabType].Select(true, false, false);
			}
		}

		// Token: 0x0600659F RID: 26015 RVA: 0x00204E2E File Offset: 0x0020302E
		public override void CloseInternal()
		{
			NKCPopupForgeCraft.CheckInstanceAndClose();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060065A0 RID: 26016 RVA: 0x00204E41 File Offset: 0x00203041
		public override void OnCloseInstance()
		{
			this.m_LoopScrollRect.ClearCells();
			this.m_eTapType = NKM_CRAFT_TAB_TYPE.MT_EQUIP;
			this.m_dicTabToggle.Clear();
		}

		// Token: 0x060065A1 RID: 26017 RVA: 0x00204E60 File Offset: 0x00203060
		public override void Hide()
		{
			base.Hide();
		}

		// Token: 0x060065A2 RID: 26018 RVA: 0x00204E68 File Offset: 0x00203068
		public override void UnHide()
		{
			base.UnHide();
		}

		// Token: 0x060065A3 RID: 26019 RVA: 0x00204E70 File Offset: 0x00203070
		private void OnSelectTab(NKM_CRAFT_TAB_TYPE newStatus)
		{
			bool flag = false;
			using (IEnumerator enumerator = Enum.GetValues(typeof(NKM_CRAFT_TAB_TYPE)).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if ((NKM_CRAFT_TAB_TYPE)enumerator.Current == newStatus)
					{
						flag = true;
					}
				}
			}
			if (!flag)
			{
				Debug.LogError(string.Format("허용되지 않는 enum 값, 추가 설정이 필요 : {0}", newStatus));
				return;
			}
			if (this.m_lstMoldTab.Count <= 0)
			{
				Debug.LogError("m_lstMoldTab 데이터를 확인 할 수 없습니다.");
				return;
			}
			if (this.m_eTapType != newStatus)
			{
				foreach (NKCRandomMoldTabTemplet nkcrandomMoldTabTemplet in this.m_lstMoldTab)
				{
					if (nkcrandomMoldTabTemplet.m_MoldTabID == newStatus)
					{
						if (nkcrandomMoldTabTemplet.m_MoldTab_Sort.Count <= 0)
						{
							Debug.LogError("m_MoldTab_Sort 설정을 확인 해주세요");
							return;
						}
						for (int i = 0; i < nkcrandomMoldTabTemplet.m_MoldTab_Sort.Count; i++)
						{
							if (i == 0)
							{
								this.m_MoldSortSystem = new NKCMoldSortSystem(NKCMoldSortSystem.GetDefaultSortOption(nkcrandomMoldTabTemplet.m_MoldTab_Sort[i]));
							}
						}
						break;
					}
				}
				this.SetOpenSortingMenu(false, true);
				NKCUtil.SetGameobjectActive(this.m_objSortSelected, false);
			}
			this.m_eTapType = newStatus;
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_CRAFT_MOLD_SLOT_TEXT, this.m_eTapType == NKM_CRAFT_TAB_TYPE.MT_EQUIP);
			this.m_MoldSortSystem.Update(this.m_eTapType);
			this.UpdateLoopScroll();
			this.ProcessUIFromCurrentDisplayedSortData();
		}

		// Token: 0x060065A4 RID: 26020 RVA: 0x00205000 File Offset: 0x00203200
		private void InitFunctionButton()
		{
			this.m_tgl_NKM_UI_FACTORY_CRAFT_MENU_VERTICAL.OnValueChanged.RemoveAllListeners();
			this.m_tgl_NKM_UI_FACTORY_CRAFT_MENU_VERTICAL.OnValueChanged.AddListener(new UnityAction<bool>(this.OnCheckAscend));
			this.m_sbtn_NKM_UI_FACTORY_CRAFT_MENU_FILTER.PointerClick.RemoveAllListeners();
			this.m_sbtn_NKM_UI_FACTORY_CRAFT_MENU_FILTER.PointerClick.AddListener(new UnityAction(this.OnClickFilterBtn));
			this.m_cbtnSortTypeMenu.OnValueChanged.RemoveAllListeners();
			this.m_cbtnSortTypeMenu.OnValueChanged.AddListener(new UnityAction<bool>(this.OnSortMenuOpen));
			if (this.m_rtNKM_UI_FACTORY_CRAFT_LEFT != null)
			{
				this.m_ctglGroup_FactoryCraftLeft = this.m_rtNKM_UI_FACTORY_CRAFT_LEFT.GetComponent<NKCUIComToggleGroup>();
			}
		}

		// Token: 0x060065A5 RID: 26021 RVA: 0x002050B0 File Offset: 0x002032B0
		public void OnSortMenuOpen(bool bValue)
		{
			if (this.m_MoldSortSystem == null)
			{
				return;
			}
			if (this.m_MoldSortSystem.lstSortOption.Count <= 1)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objSortSelected, this.m_MoldSortSystem.lstSortOption[1] != NKCMoldSortSystem.GetDefaultSortOption()[1]);
			foreach (NKCRandomMoldTabTemplet nkcrandomMoldTabTemplet in this.m_lstMoldTab)
			{
				if (nkcrandomMoldTabTemplet.m_MoldTabID == this.m_eTapType)
				{
					this.m_NKCMoldPopupSort.OpenMoldSortMenu(this.m_MoldSortSystem.GetCurActiveOption(), new NKCPopupMoldSort.OnSortOption(this.OnSort), NKCMoldSortSystem.GetDescendingBySorting(this.m_MoldSortSystem.lstSortOption), bValue, nkcrandomMoldTabTemplet.m_MoldTab_Sort);
					break;
				}
			}
			this.SetOpenSortingMenu(bValue, true);
		}

		// Token: 0x060065A6 RID: 26022 RVA: 0x00205198 File Offset: 0x00203398
		private void OnSort(NKCMoldSortSystem.eSortOption selectedSortOption)
		{
			NKCUtil.SetGameobjectActive(this.m_objSortSelected, selectedSortOption != NKCMoldSortSystem.GetDefaultSortOption()[1]);
			this.SetOpenSortingMenu(false, true);
			bool bChangedList = false;
			if (this.m_MoldSortSystem.GetCurActiveOption() != selectedSortOption)
			{
				this.m_MoldSortSystem.Sort(selectedSortOption);
				bChangedList = true;
			}
			this.UpdateMoldList(bChangedList);
		}

		// Token: 0x060065A7 RID: 26023 RVA: 0x002051EF File Offset: 0x002033EF
		public void OnCheckAscend(bool bValue)
		{
			this.m_MoldSortSystem.OnCheckAscend(bValue, new UnityAction(this.UpdateLoopScroll));
		}

		// Token: 0x060065A8 RID: 26024 RVA: 0x00205209 File Offset: 0x00203409
		public void SetOpenSortingMenu(bool bOpen, bool bAnimate = true)
		{
			this.m_cbtnSortTypeMenu.Select(bOpen, true, false);
			this.m_NKCMoldPopupSort.StartRectMove(bOpen, bAnimate);
		}

		// Token: 0x060065A9 RID: 26025 RVA: 0x00205228 File Offset: 0x00203428
		public void OnClickFilterBtn()
		{
			if (this.m_MoldSortSystem != null)
			{
				foreach (NKCRandomMoldTabTemplet nkcrandomMoldTabTemplet in this.m_lstMoldTab)
				{
					if (nkcrandomMoldTabTemplet.m_MoldTabID == this.m_eTapType)
					{
						NKCPopupFilterMold.Instance.Open(this.m_MoldSortSystem.FilterSet, new NKCPopupFilterMold.OnMoldFilterSetChange(this.OnSelectFilter), nkcrandomMoldTabTemplet.m_MoldTab_Filter);
						break;
					}
				}
			}
		}

		// Token: 0x060065AA RID: 26026 RVA: 0x002052B4 File Offset: 0x002034B4
		public void OnSelectFilter(HashSet<NKCMoldSortSystem.eFilterOption> setFilterOption)
		{
			if (this.m_MoldSortSystem != null)
			{
				this.m_MoldSortSystem.FilterList(setFilterOption, this.m_eTapType);
				this.UpdateLoopScroll();
			}
		}

		// Token: 0x060065AB RID: 26027 RVA: 0x002052D8 File Offset: 0x002034D8
		private void UpdateLoopScroll()
		{
			if (this.m_MoldSortSystem == null)
			{
				return;
			}
			for (int i = 0; i < this.m_MoldSortSystem.lstSortedList.Count; i++)
			{
				NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(this.m_MoldSortSystem.lstSortedList[i].m_MoldID);
				if (itemMoldTempletByID != null && itemMoldTempletByID.HasDateLimit && !itemMoldTempletByID.IntervalTemplet.IsValidTime(ServiceTime.Recent))
				{
					this.m_MoldSortSystem.lstSortedList.RemoveAt(i);
					i--;
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objNoneText, this.m_MoldSortSystem.lstSortedList.Count <= 0);
			this.m_LoopScrollRect.TotalCount = this.m_MoldSortSystem.lstSortedList.Count;
			this.m_LoopScrollRect.velocity = new Vector2(0f, 0f);
			this.m_LoopScrollRect.SetIndexPosition(0);
			this.m_LoopScrollRect.RefreshCells(true);
		}

		// Token: 0x060065AC RID: 26028 RVA: 0x002053C5 File Offset: 0x002035C5
		private void UpdateMoldList(bool bChangedList)
		{
			if (bChangedList)
			{
				this.UpdateLoopScroll();
			}
			this.ProcessUIFromCurrentDisplayedSortData();
		}

		// Token: 0x060065AD RID: 26029 RVA: 0x002053D8 File Offset: 0x002035D8
		public NKCUIForgeCraftMoldSlot GetMoldSlot(int moldID)
		{
			if (this.m_MoldSortSystem == null)
			{
				return null;
			}
			int num = this.m_MoldSortSystem.lstSortedList.FindIndex((NKMMoldItemData v) => v.m_MoldID == moldID);
			if (num < 0)
			{
				return null;
			}
			this.m_LoopScrollRect.SetIndexPosition(num);
			NKCUIForgeCraftMoldSlot[] componentsInChildren = this.m_LoopScrollRect.content.GetComponentsInChildren<NKCUIForgeCraftMoldSlot>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i].MoldID == moldID)
				{
					return componentsInChildren[i];
				}
			}
			return null;
		}

		// Token: 0x04005100 RID: 20736
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_factory";

		// Token: 0x04005101 RID: 20737
		private const string UI_ASSET_NAME = "NKM_UI_FACTORY_CRAFT_MOLD";

		// Token: 0x04005102 RID: 20738
		private static NKCUIForgeCraftMold m_Instance;

		// Token: 0x04005103 RID: 20739
		private GameObject m_NUM_FORGE;

		// Token: 0x04005104 RID: 20740
		public LoopScrollRect m_LoopScrollRect;

		// Token: 0x04005105 RID: 20741
		public Transform m_ContentTransform;

		// Token: 0x04005106 RID: 20742
		public GridLayoutGroup m_GridLayoutGroup;

		// Token: 0x04005107 RID: 20743
		public NKCUIComSafeArea m_SafeArea;

		// Token: 0x04005108 RID: 20744
		public Vector2 m_vSlotSize;

		// Token: 0x04005109 RID: 20745
		public Vector2 m_vSlotSpacing;

		// Token: 0x0400510A RID: 20746
		public NKCUIComToggle m_NKM_UI_FACTORY_CRAFT_MOLD_SLOT_ARRAY;

		// Token: 0x0400510B RID: 20747
		private bool m_bFirstOpen = true;

		// Token: 0x0400510C RID: 20748
		private int m_Index;

		// Token: 0x0400510D RID: 20749
		public GameObject m_NKM_UI_FACTORY_CRAFT_MOLD_SLOT_TEXT;

		// Token: 0x0400510E RID: 20750
		[Header("탭")]
		public NKCUIComToggle m_ctgl_FACTORY_CRAFT_EQUIP;

		// Token: 0x0400510F RID: 20751
		public NKCUIComToggle m_ctgl_FACTORY_CRAFT_LIMITBREAK;

		// Token: 0x04005110 RID: 20752
		private NKM_CRAFT_TAB_TYPE m_eTapType;

		// Token: 0x04005111 RID: 20753
		public RectTransform m_rtNKM_UI_FACTORY_CRAFT_LEFT;

		// Token: 0x04005112 RID: 20754
		private NKCUIComToggleGroup m_ctglGroup_FactoryCraftLeft;

		// Token: 0x04005113 RID: 20755
		private const float m_fCrateTabOffsetY = -126f;

		// Token: 0x04005114 RID: 20756
		private List<NKCRandomMoldTabTemplet> m_lstMoldTab = new List<NKCRandomMoldTabTemplet>();

		// Token: 0x04005115 RID: 20757
		private string m_defaultSortKey;

		// Token: 0x04005116 RID: 20758
		private Dictionary<NKM_CRAFT_TAB_TYPE, NKCUIComToggle> m_dicTabToggle = new Dictionary<NKM_CRAFT_TAB_TYPE, NKCUIComToggle>();

		// Token: 0x04005117 RID: 20759
		[Header("필터")]
		public NKCUIComToggle m_tgl_NKM_UI_FACTORY_CRAFT_MENU_VERTICAL;

		// Token: 0x04005118 RID: 20760
		public NKCUIComStateButton m_sbtn_NKM_UI_FACTORY_CRAFT_MENU_FILTER;

		// Token: 0x04005119 RID: 20761
		public GameObject m_objFilterSelected;

		// Token: 0x0400511A RID: 20762
		private HashSet<NKCMoldSortSystem.eFilterOption> m_setFilterOption = new HashSet<NKCMoldSortSystem.eFilterOption>();

		// Token: 0x0400511B RID: 20763
		[Header("정렬")]
		public NKCPopupMoldSort m_NKCMoldPopupSort;

		// Token: 0x0400511C RID: 20764
		public NKCUIComToggle m_cbtnSortTypeMenu;

		// Token: 0x0400511D RID: 20765
		public GameObject m_objSortSelected;

		// Token: 0x0400511E RID: 20766
		public Text m_lbSortType;

		// Token: 0x0400511F RID: 20767
		public Text m_lbSelectedSortType;

		// Token: 0x04005120 RID: 20768
		private NKCMoldSortSystem m_MoldSortSystem;

		// Token: 0x04005121 RID: 20769
		public GameObject m_objNoneText;
	}
}
