using System;
using System.Collections.Generic;
using ClientPacket.Office;
using NKC.UI.Component.Office;
using NKM;
using NKM.Templet.Base;
using NKM.Templet.Office;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Office
{
	// Token: 0x02000AF9 RID: 2809
	public class NKCUIPopupOfficeInteriorSelect : NKCUIBase
	{
		// Token: 0x170014E7 RID: 5351
		// (get) Token: 0x06007ECB RID: 32459 RVA: 0x002A8404 File Offset: 0x002A6604
		public static NKCUIPopupOfficeInteriorSelect Instance
		{
			get
			{
				if (NKCUIPopupOfficeInteriorSelect.m_Instance == null)
				{
					NKCUIPopupOfficeInteriorSelect.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPopupOfficeInteriorSelect>("ab_ui_office", "AB_UI_POPUP_OFFICE_FNC_LIST", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPopupOfficeInteriorSelect.CleanupInstance)).GetInstance<NKCUIPopupOfficeInteriorSelect>();
					NKCUIPopupOfficeInteriorSelect.m_Instance.InitUI();
				}
				return NKCUIPopupOfficeInteriorSelect.m_Instance;
			}
		}

		// Token: 0x06007ECC RID: 32460 RVA: 0x002A8453 File Offset: 0x002A6653
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPopupOfficeInteriorSelect.m_Instance != null && NKCUIPopupOfficeInteriorSelect.m_Instance.IsOpen)
			{
				NKCUIPopupOfficeInteriorSelect.m_Instance.Close();
			}
		}

		// Token: 0x06007ECD RID: 32461 RVA: 0x002A8478 File Offset: 0x002A6678
		private static void CleanupInstance()
		{
			NKCUIPopupOfficeInteriorSelect.m_Instance = null;
		}

		// Token: 0x170014E8 RID: 5352
		// (get) Token: 0x06007ECE RID: 32462 RVA: 0x002A8480 File Offset: 0x002A6680
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIPopupOfficeInteriorSelect.m_Instance != null && NKCUIPopupOfficeInteriorSelect.m_Instance.IsOpen;
			}
		}

		// Token: 0x170014E9 RID: 5353
		// (get) Token: 0x06007ECF RID: 32463 RVA: 0x002A849B File Offset: 0x002A669B
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170014EA RID: 5354
		// (get) Token: 0x06007ED0 RID: 32464 RVA: 0x002A849E File Offset: 0x002A669E
		public override string MenuName
		{
			get
			{
				return "창고";
			}
		}

		// Token: 0x06007ED1 RID: 32465 RVA: 0x002A84A5 File Offset: 0x002A66A5
		public override void CloseInternal()
		{
			if (this.m_eMode == NKCUIPopupOfficeInteriorSelect.Mode.Listview)
			{
				this.m_ssActive = null;
			}
			this.m_lstThemePreset = null;
			this.m_dicFurnitureListView = null;
			this.m_comFurniturePreview.Clear();
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007ED2 RID: 32466 RVA: 0x002A84DC File Offset: 0x002A66DC
		public override void OnCloseInstance()
		{
			if (this.m_lstVisibleSlot != null)
			{
				for (int i = 0; i < this.m_lstVisibleSlot.Count; i++)
				{
					UnityEngine.Object.Destroy(this.m_lstVisibleSlot[i].gameObject);
				}
				this.m_lstVisibleSlot.Clear();
			}
		}

		// Token: 0x06007ED3 RID: 32467 RVA: 0x002A8528 File Offset: 0x002A6728
		private void InitUI()
		{
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglTabFuniture, new UnityAction<bool>(this.OnTglTabFuniture));
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglTabBackground, new UnityAction<bool>(this.OnTglTabBackground));
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglTabThemePreset, new UnityAction<bool>(this.OnTglTabThemePreset));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnInstall, new UnityAction(this.OnInstallFuniture));
			NKCUtil.SetHotkey(this.m_csbtnInstall, HotkeyEventType.Confirm, null, false);
			NKCUtil.SetButtonClickDelegate(this.m_csbtnClose, new UnityAction(base.Close));
			this.m_srFuniture.dOnGetObject += this.GetObject;
			this.m_srFuniture.dOnReturnObject += this.ReturnObject;
			this.m_srFuniture.dOnProvideData += this.ProvideData;
			this.m_srFuniture.SetAutoResize(3, false);
			this.m_srFuniture.PrepareCells(0);
			if (this.m_sortOptions != null)
			{
				this.m_sortOptions.Init(new NKCUIComMiscSortOptions.OnSorted(this.OnSorted));
			}
			NKCUtil.SetScrollHotKey(this.m_srFuniture, null);
		}

		// Token: 0x06007ED4 RID: 32468 RVA: 0x002A8648 File Offset: 0x002A6848
		public void Open(NKCUIPopupOfficeInteriorSelect.OnSelectInterior onSelectInterior, NKCUIPopupOfficeInteriorSelect.OnSelectPreset onSelectPreset)
		{
			NKCUtil.SetLabelText(this.m_lbTitle, NKCStringTable.GetString("SI_PF_OFFICE_ROOM_UI_FRONT_WAREHOUSE", false));
			NKCUtil.SetLabelText(this.m_lbNoFurnitureSelect, NKCStringTable.GetString("SI_PF_POPUP_OFFICE_FNC_LIST_SELECT_NONE", false));
			NKCUtil.SetLabelText(this.m_lbNoFuniture, NKCStringTable.GetString("SI_PF_POPUP_OFFICE_FNC_LIST_NONE", false));
			this.dOnSelectInterior = onSelectInterior;
			this.dOnSelectPreset = onSelectPreset;
			this.m_eMode = NKCUIPopupOfficeInteriorSelect.Mode.FurnitureEdit;
			this.BuildFunitureList();
			NKCUtil.SetGameobjectActive(this.m_tglTabThemePreset, true);
			NKCUtil.SetGameobjectActive(this.m_csbtnInstall, true);
			base.UIOpened(true);
			this.SelectTab(this.m_eCurrentCategory);
		}

		// Token: 0x06007ED5 RID: 32469 RVA: 0x002A86E0 File Offset: 0x002A68E0
		public void OpenForListView(Dictionary<int, int> dicFurniture)
		{
			NKCUtil.SetLabelText(this.m_lbTitle, NKCStringTable.GetString("SI_PF_SHOP_BANNER_INTERIOR_VIEW_TEXT", false));
			NKCUtil.SetLabelText(this.m_lbNoFurnitureSelect, NKCStringTable.GetString("SI_PF_SHOP_INTERIOR_02", false));
			NKCUtil.SetLabelText(this.m_lbNoFuniture, NKCStringTable.GetString("SI_PF_SHOP_INTERIOR_01", false));
			this.m_dicFurnitureListView = dicFurniture;
			this.m_eMode = NKCUIPopupOfficeInteriorSelect.Mode.Listview;
			this.m_ssActive = null;
			NKCUtil.SetGameobjectActive(this.m_tglTabThemePreset, false);
			NKCUtil.SetGameobjectActive(this.m_csbtnInstall, false);
			this.BuildFunitureList();
			base.UIOpened(true);
			this.SelectTab(NKCUIPopupOfficeInteriorSelect.Category.FURNITURE);
		}

		// Token: 0x06007ED6 RID: 32470 RVA: 0x002A8770 File Offset: 0x002A6970
		private void BuildFunitureList()
		{
			this.m_lstDecoration.Clear();
			this.m_lstFuniture.Clear();
			NKCUIPopupOfficeInteriorSelect.Mode eMode = this.m_eMode;
			if (eMode != NKCUIPopupOfficeInteriorSelect.Mode.FurnitureEdit)
			{
				if (eMode != NKCUIPopupOfficeInteriorSelect.Mode.Listview)
				{
					return;
				}
			}
			else
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				using (IEnumerator<NKMInteriorData> enumerator = nkmuserData.OfficeData.GetAllInteriorData().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						NKMInteriorData nkminteriorData = enumerator.Current;
						NKMOfficeInteriorTemplet nkmofficeInteriorTemplet = NKMOfficeInteriorTemplet.Find(nkminteriorData.itemId);
						if (nkmuserData.OfficeData.GetInteriorCount(nkminteriorData.itemId) != 0L && nkmofficeInteriorTemplet != null)
						{
							InteriorCategory interiorCategory = nkmofficeInteriorTemplet.InteriorCategory;
							if (interiorCategory != InteriorCategory.DECO)
							{
								if (interiorCategory == InteriorCategory.FURNITURE)
								{
									this.m_lstFuniture.Add(nkmofficeInteriorTemplet);
								}
							}
							else
							{
								this.m_lstDecoration.Add(nkmofficeInteriorTemplet);
							}
						}
					}
					return;
				}
			}
			foreach (KeyValuePair<int, int> keyValuePair in this.m_dicFurnitureListView)
			{
				NKMOfficeInteriorTemplet nkmofficeInteriorTemplet2 = NKMOfficeInteriorTemplet.Find(keyValuePair.Key);
				if (keyValuePair.Value != 0 && nkmofficeInteriorTemplet2 != null)
				{
					InteriorCategory interiorCategory = nkmofficeInteriorTemplet2.InteriorCategory;
					if (interiorCategory != InteriorCategory.DECO)
					{
						if (interiorCategory == InteriorCategory.FURNITURE)
						{
							this.m_lstFuniture.Add(nkmofficeInteriorTemplet2);
						}
					}
					else
					{
						this.m_lstDecoration.Add(nkmofficeInteriorTemplet2);
					}
				}
			}
		}

		// Token: 0x06007ED7 RID: 32471 RVA: 0x002A88C8 File Offset: 0x002A6AC8
		private void SelectTab(NKCUIPopupOfficeInteriorSelect.Category category)
		{
			this.m_eCurrentCategory = category;
			if (category == NKCUIPopupOfficeInteriorSelect.Category.THEME)
			{
				this.m_ssActive = null;
				NKCUtil.SetGameobjectActive(this.m_sortOptions, false);
				this.m_tglTabThemePreset.Select(true, true, false);
				if (this.m_lstThemePreset == null)
				{
					this.MakeThemePresetList();
				}
				this.m_srFuniture.TotalCount = this.m_lstThemePreset.Count;
				this.m_srFuniture.SetIndexPosition(0);
				NKCUtil.SetGameobjectActive(this.m_objNoFuniture, this.m_lstThemePreset.Count == 0);
				this.OnSelectTheme(-1, true);
				return;
			}
			if (category != NKCUIPopupOfficeInteriorSelect.Category.DECO)
			{
				if (category == NKCUIPopupOfficeInteriorSelect.Category.FURNITURE)
				{
					this.m_tglTabFuniture.Select(true, true, false);
					this.m_ssActive = new NKCMiscSortSystem(NKCScenManager.CurrentUserData(), this.m_lstFuniture, this.MakeSortOption());
				}
			}
			else
			{
				this.m_tglTabBackground.Select(true, true, false);
				this.m_ssActive = new NKCMiscSortSystem(NKCScenManager.CurrentUserData(), this.m_lstDecoration, this.MakeSortOption());
			}
			NKCUtil.SetGameobjectActive(this.m_sortOptions, true);
			this.m_sortOptions.RegisterCategories(NKCMiscSortSystem.GetDefaultInteriorFilterCategory(), NKCMiscSortSystem.GetDefaultInteriorSortCategory());
			this.m_sortOptions.RegisterMiscSort(this.m_ssActive);
			this.m_sortOptions.ResetUI();
			this.m_ssActive.FilterList(this.m_ssActive.FilterSet);
			this.OnSelectFuniture(-1, true);
			this.OnSorted(true);
		}

		// Token: 0x06007ED8 RID: 32472 RVA: 0x002A8A18 File Offset: 0x002A6C18
		private NKCMiscSortSystem.MiscListOptions MakeSortOption()
		{
			if (this.m_ssActive != null)
			{
				return this.m_ssActive.Options;
			}
			return new NKCMiscSortSystem.MiscListOptions
			{
				lstSortOption = NKCMiscSortSystem.GetDefaultInteriorSortList(),
				setFilterOption = new HashSet<NKCMiscSortSystem.eFilterOption>()
			};
		}

		// Token: 0x06007ED9 RID: 32473 RVA: 0x002A8A5A File Offset: 0x002A6C5A
		private void OnTglTabFuniture(bool value)
		{
			if (value)
			{
				this.SelectTab(NKCUIPopupOfficeInteriorSelect.Category.FURNITURE);
			}
		}

		// Token: 0x06007EDA RID: 32474 RVA: 0x002A8A66 File Offset: 0x002A6C66
		private void OnTglTabBackground(bool value)
		{
			if (value)
			{
				this.SelectTab(NKCUIPopupOfficeInteriorSelect.Category.DECO);
			}
		}

		// Token: 0x06007EDB RID: 32475 RVA: 0x002A8A72 File Offset: 0x002A6C72
		private void OnTglTabThemePreset(bool value)
		{
			if (value)
			{
				this.SelectTab(NKCUIPopupOfficeInteriorSelect.Category.THEME);
			}
		}

		// Token: 0x06007EDC RID: 32476 RVA: 0x002A8A80 File Offset: 0x002A6C80
		private void OnInstallFuniture()
		{
			if (this.m_eCurrentCategory == NKCUIPopupOfficeInteriorSelect.Category.THEME)
			{
				base.Close();
				NKCUIPopupOfficeInteriorSelect.OnSelectPreset onSelectPreset = this.dOnSelectPreset;
				if (onSelectPreset == null)
				{
					return;
				}
				onSelectPreset(this.m_SelectedThemeID);
				return;
			}
			else
			{
				if (NKCScenManager.CurrentUserData().OfficeData.GetFreeInteriorCount(this.m_SelectedFunitureID) <= 0L)
				{
					NKCPopupMessageManager.AddPopupMessage(NKM_ERROR_CODE.NEC_FAIL_OFFICE_FURNITURE_NOT_REMAINS, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
				base.Close();
				NKCUIPopupOfficeInteriorSelect.OnSelectInterior onSelectInterior = this.dOnSelectInterior;
				if (onSelectInterior == null)
				{
					return;
				}
				onSelectInterior(this.m_SelectedFunitureID);
				return;
			}
		}

		// Token: 0x06007EDD RID: 32477 RVA: 0x002A8AFC File Offset: 0x002A6CFC
		private RectTransform GetObject(int idx)
		{
			NKCUISlot nkcuislot = UnityEngine.Object.Instantiate<NKCUISlot>(this.m_pfbSlot);
			nkcuislot.Init();
			RectTransform component = nkcuislot.GetComponent<RectTransform>();
			if (component == null)
			{
				UnityEngine.Object.Destroy(nkcuislot.gameObject);
			}
			this.m_lstVisibleSlot.Add(nkcuislot);
			return component;
		}

		// Token: 0x06007EDE RID: 32478 RVA: 0x002A8B44 File Offset: 0x002A6D44
		private void ReturnObject(Transform tr)
		{
			tr.SetParent(base.transform);
			tr.gameObject.SetActive(false);
			NKCUISlot component = tr.GetComponent<NKCUISlot>();
			if (component != null)
			{
				this.m_lstVisibleSlot.Remove(component);
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x06007EDF RID: 32479 RVA: 0x002A8B94 File Offset: 0x002A6D94
		private void ProvideData(Transform tr, int idx)
		{
			NKCUISlot component = tr.GetComponent<NKCUISlot>();
			if (component == null)
			{
				NKCUtil.SetGameobjectActive(tr, false);
				return;
			}
			if (this.m_eCurrentCategory == NKCUIPopupOfficeInteriorSelect.Category.THEME)
			{
				if (idx < 0 || idx >= this.m_lstThemePreset.Count)
				{
					NKCUtil.SetGameobjectActive(tr, false);
					return;
				}
				NKMOfficeThemePresetTemplet nkmofficeThemePresetTemplet = this.m_lstThemePreset[idx];
				NKCUISlot.SlotData slotData = new NKCUISlot.SlotData();
				slotData.eType = NKCUISlot.eSlotMode.Etc;
				slotData.ID = nkmofficeThemePresetTemplet.Key;
				Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>(NKMAssetName.ParseBundleName("AB_INVEN_ICON_FNC_THEME", nkmofficeThemePresetTemplet.ThemaPresetIMG));
				component.SetEtcData(slotData, orLoadAssetResource, "", "", new NKCUISlot.OnClick(this.OnSelectSlot));
				component.SetSelected(this.m_SelectedThemeID == nkmofficeThemePresetTemplet.Key);
				return;
			}
			else
			{
				if (idx < 0 || idx >= this.m_ssActive.SortedMiscList.Count)
				{
					NKCUtil.SetGameobjectActive(tr, false);
					return;
				}
				NKMOfficeInteriorTemplet nkmofficeInteriorTemplet = this.m_ssActive.SortedMiscList[idx] as NKMOfficeInteriorTemplet;
				NKCUIPopupOfficeInteriorSelect.Mode eMode = this.m_eMode;
				if (eMode != NKCUIPopupOfficeInteriorSelect.Mode.FurnitureEdit)
				{
					if (eMode == NKCUIPopupOfficeInteriorSelect.Mode.Listview)
					{
						int num;
						if (!this.m_dicFurnitureListView.TryGetValue(nkmofficeInteriorTemplet.m_ItemMiscID, out num))
						{
							num = 0;
						}
						NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeMiscItemData(nkmofficeInteriorTemplet.m_ItemMiscID, (long)num, 0);
						component.SetData(data, false, this.m_eCurrentCategory == NKCUIPopupOfficeInteriorSelect.Category.FURNITURE, true, new NKCUISlot.OnClick(this.OnSelectSlot));
					}
				}
				else
				{
					long freeInteriorCount = NKCScenManager.CurrentUserData().OfficeData.GetFreeInteriorCount(nkmofficeInteriorTemplet.m_ItemMiscID);
					NKCUISlot.SlotData data2 = NKCUISlot.SlotData.MakeMiscItemData(nkmofficeInteriorTemplet.m_ItemMiscID, freeInteriorCount, 0);
					NKCUIPopupOfficeInteriorSelect.Category eCurrentCategory = this.m_eCurrentCategory;
					if (eCurrentCategory != NKCUIPopupOfficeInteriorSelect.Category.DECO)
					{
						if (eCurrentCategory == NKCUIPopupOfficeInteriorSelect.Category.FURNITURE)
						{
							long interiorCount = NKCScenManager.CurrentUserData().OfficeData.GetInteriorCount(nkmofficeInteriorTemplet.m_ItemMiscID);
							component.SetData(data2, false, true, true, new NKCUISlot.OnClick(this.OnSelectSlot));
							if (freeInteriorCount == 0L)
							{
								component.SetSlotItemCountString(true, string.Format("<color=#ff0000>{0}</color>/{1}", freeInteriorCount, interiorCount));
							}
							else
							{
								component.SetSlotItemCountString(true, string.Format("{0}/{1}", freeInteriorCount, interiorCount));
							}
						}
					}
					else
					{
						component.SetData(data2, false, false, true, new NKCUISlot.OnClick(this.OnSelectSlot));
					}
				}
				component.SetSelected(this.m_SelectedFunitureID == nkmofficeInteriorTemplet.Id);
				if (!NKCDefineManager.DEFINE_SERVICE() && NKMItemMiscTemplet.Find(nkmofficeInteriorTemplet.m_ItemMiscID) == null)
				{
					component.OverrideName(nkmofficeInteriorTemplet.PrefabName, false, false);
				}
				return;
			}
		}

		// Token: 0x06007EE0 RID: 32480 RVA: 0x002A8DF3 File Offset: 0x002A6FF3
		private void OnSelectSlot(NKCUISlot.SlotData slotData, bool bLocked)
		{
			if (bLocked)
			{
				return;
			}
			if (this.m_eCurrentCategory == NKCUIPopupOfficeInteriorSelect.Category.THEME)
			{
				this.OnSelectTheme(slotData.ID, false);
				return;
			}
			this.OnSelectFuniture(slotData.ID, false);
		}

		// Token: 0x06007EE1 RID: 32481 RVA: 0x002A8E20 File Offset: 0x002A7020
		private void OnSorted(bool bResetScroll)
		{
			this.m_srFuniture.TotalCount = this.m_ssActive.SortedMiscList.Count;
			if (bResetScroll)
			{
				this.m_srFuniture.SetIndexPosition(0);
			}
			else
			{
				this.m_srFuniture.RefreshCells(false);
			}
			NKCUtil.SetGameobjectActive(this.m_objNoFuniture, this.m_ssActive.SortedMiscList.Count == 0);
		}

		// Token: 0x06007EE2 RID: 32482 RVA: 0x002A8E84 File Offset: 0x002A7084
		private void OnSelectFuniture(int id, bool bForce = false)
		{
			if (!bForce && this.m_SelectedFunitureID == id)
			{
				return;
			}
			this.m_SelectedFunitureID = id;
			NKMOfficeInteriorTemplet nkmofficeInteriorTemplet = NKMItemMiscTemplet.FindInterior(id);
			NKCUtil.SetGameobjectActive(this.m_objNoFunitureSelect, nkmofficeInteriorTemplet == null);
			NKCUtil.SetGameobjectActive(this.m_comFurniturePreview, nkmofficeInteriorTemplet != null);
			if (this.m_ssActive != null && this.m_srFuniture != null)
			{
				foreach (object obj in this.m_srFuniture.content)
				{
					((Transform)obj).GetComponent<NKCUISlot>().SetSelected(false);
				}
				int index = this.m_ssActive.SortedMiscList.FindIndex((NKMItemMiscTemplet x) => x.m_ItemMiscID == id);
				Transform child = this.m_srFuniture.GetChild(index);
				if (child != null)
				{
					NKCUISlot component = child.GetComponent<NKCUISlot>();
					if (component != null)
					{
						component.SetSelected(true);
					}
				}
			}
			if (this.m_comInteriorDetail != null)
			{
				this.m_comInteriorDetail.SetData(nkmofficeInteriorTemplet);
			}
			if (this.m_comInteriorInteractionBubble != null)
			{
				this.m_comInteriorInteractionBubble.SetData(nkmofficeInteriorTemplet);
			}
			if (nkmofficeInteriorTemplet != null)
			{
				NKCUIComOfficeFurniturePreview comFurniturePreview = this.m_comFurniturePreview;
				if (comFurniturePreview != null)
				{
					comFurniturePreview.SetData(nkmofficeInteriorTemplet);
				}
			}
			this.m_csbtnInstall.SetLock(nkmofficeInteriorTemplet == null, false);
		}

		// Token: 0x06007EE3 RID: 32483 RVA: 0x002A9004 File Offset: 0x002A7204
		internal RectTransform GetTutorialItemSlot(int itemID)
		{
			int num = this.m_ssActive.SortedMiscList.FindIndex((NKMItemMiscTemplet x) => x.m_ItemMiscID == itemID);
			if (num < 0)
			{
				return null;
			}
			this.m_srFuniture.SetIndexPosition(num);
			NKCUISlot nkcuislot = this.m_lstVisibleSlot.Find((NKCUISlot x) => x.GetSlotData().ID == itemID);
			if (nkcuislot == null)
			{
				return null;
			}
			return nkcuislot.GetComponent<RectTransform>();
		}

		// Token: 0x06007EE4 RID: 32484 RVA: 0x002A9076 File Offset: 0x002A7276
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			if (NKMOfficeInteriorTemplet.Find(itemData.ItemID) != null)
			{
				this.BuildFunitureList();
				this.SelectTab(this.m_eCurrentCategory);
			}
		}

		// Token: 0x06007EE5 RID: 32485 RVA: 0x002A9097 File Offset: 0x002A7297
		public override void OnInteriorInventoryUpdate(NKMInteriorData interiorData, bool bAdded)
		{
			if (bAdded)
			{
				this.BuildFunitureList();
				this.SelectTab(this.m_eCurrentCategory);
				return;
			}
			this.m_srFuniture.RefreshCells(false);
		}

		// Token: 0x06007EE6 RID: 32486 RVA: 0x002A90BC File Offset: 0x002A72BC
		private void MakeThemePresetList()
		{
			this.m_lstThemePreset = new List<NKMOfficeThemePresetTemplet>();
			foreach (NKMOfficeThemePresetTemplet nkmofficeThemePresetTemplet in NKMTempletContainer<NKMOfficeThemePresetTemplet>.Values)
			{
				if (nkmofficeThemePresetTemplet.EnableByTag)
				{
					if (nkmofficeThemePresetTemplet.AlwaysAppearOnList)
					{
						this.m_lstThemePreset.Add(nkmofficeThemePresetTemplet);
					}
					else
					{
						NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
						foreach (NKMOfficeInteriorTemplet templet in nkmofficeThemePresetTemplet.AllInteriors)
						{
							if (nkmuserData.OfficeData.GetInteriorCount(templet) > 0L)
							{
								this.m_lstThemePreset.Add(nkmofficeThemePresetTemplet);
								break;
							}
						}
					}
				}
			}
		}

		// Token: 0x06007EE7 RID: 32487 RVA: 0x002A9188 File Offset: 0x002A7388
		private void OnSelectTheme(int id, bool bForce = false)
		{
			this.m_SelectedThemeID = id;
			NKMOfficeThemePresetTemplet nkmofficeThemePresetTemplet = NKMOfficeThemePresetTemplet.Find(id);
			NKCUtil.SetGameobjectActive(this.m_objNoFunitureSelect, nkmofficeThemePresetTemplet == null);
			NKCUtil.SetGameobjectActive(this.m_comFurniturePreview, nkmofficeThemePresetTemplet != null);
			NKCUtil.SetGameobjectActive(this.m_comInteriorDetail, false);
			NKCUtil.SetGameobjectActive(this.m_comInteriorInteractionBubble, false);
			if (this.m_comFurniturePreview != null)
			{
				this.m_comFurniturePreview.SetData(nkmofficeThemePresetTemplet);
			}
			if (this.m_lstThemePreset != null && this.m_srFuniture != null)
			{
				foreach (object obj in this.m_srFuniture.content)
				{
					((Transform)obj).GetComponent<NKCUISlot>().SetSelected(false);
				}
				int index = this.m_lstThemePreset.FindIndex((NKMOfficeThemePresetTemplet x) => x.Key == id);
				Transform child = this.m_srFuniture.GetChild(index);
				if (child != null)
				{
					NKCUISlot component = child.GetComponent<NKCUISlot>();
					if (component != null)
					{
						component.SetSelected(true);
					}
				}
			}
			this.m_csbtnInstall.SetLock(nkmofficeThemePresetTemplet == null, false);
		}

		// Token: 0x04006B62 RID: 27490
		private const string ASSET_BUNDLE_NAME = "ab_ui_office";

		// Token: 0x04006B63 RID: 27491
		private const string UI_ASSET_NAME = "AB_UI_POPUP_OFFICE_FNC_LIST";

		// Token: 0x04006B64 RID: 27492
		private static NKCUIPopupOfficeInteriorSelect m_Instance;

		// Token: 0x04006B65 RID: 27493
		public Text m_lbTitle;

		// Token: 0x04006B66 RID: 27494
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x04006B67 RID: 27495
		[Header("왼쪽")]
		public NKCUIComToggle m_tglTabFuniture;

		// Token: 0x04006B68 RID: 27496
		public NKCUIComToggle m_tglTabBackground;

		// Token: 0x04006B69 RID: 27497
		public NKCUIComToggle m_tglTabThemePreset;

		// Token: 0x04006B6A RID: 27498
		[Header("가구 프리뷰")]
		public NKCUIComOfficeFurniturePreview m_comFurniturePreview;

		// Token: 0x04006B6B RID: 27499
		public GameObject m_objNoFunitureSelect;

		// Token: 0x04006B6C RID: 27500
		public Text m_lbNoFurnitureSelect;

		// Token: 0x04006B6D RID: 27501
		public NKCUIComStateButton m_csbtnInstall;

		// Token: 0x04006B6E RID: 27502
		[Header("가구목록")]
		public NKCUIComMiscSortOptions m_sortOptions;

		// Token: 0x04006B6F RID: 27503
		public LoopScrollRect m_srFuniture;

		// Token: 0x04006B70 RID: 27504
		public NKCUISlot m_pfbSlot;

		// Token: 0x04006B71 RID: 27505
		public GridLayoutGroup m_GridLayoutGroup;

		// Token: 0x04006B72 RID: 27506
		public GameObject m_objNoFuniture;

		// Token: 0x04006B73 RID: 27507
		public Text m_lbNoFuniture;

		// Token: 0x04006B74 RID: 27508
		private const int MIN_COLUMN_COUNT = 3;

		// Token: 0x04006B75 RID: 27509
		[Header("상호작용 관련")]
		public NKCUIComOfficeInteriorDetail m_comInteriorDetail;

		// Token: 0x04006B76 RID: 27510
		public NKCUIComOfficeInteriorInteractionBubble m_comInteriorInteractionBubble;

		// Token: 0x04006B77 RID: 27511
		private int m_SelectedFunitureID = -1;

		// Token: 0x04006B78 RID: 27512
		private int m_SelectedThemeID = -1;

		// Token: 0x04006B79 RID: 27513
		private List<NKMOfficeInteriorTemplet> m_lstFuniture = new List<NKMOfficeInteriorTemplet>();

		// Token: 0x04006B7A RID: 27514
		private List<NKMOfficeInteriorTemplet> m_lstDecoration = new List<NKMOfficeInteriorTemplet>();

		// Token: 0x04006B7B RID: 27515
		private NKCUIPopupOfficeInteriorSelect.OnSelectInterior dOnSelectInterior;

		// Token: 0x04006B7C RID: 27516
		private NKCUIPopupOfficeInteriorSelect.OnSelectPreset dOnSelectPreset;

		// Token: 0x04006B7D RID: 27517
		private NKCUIPopupOfficeInteriorSelect.Category m_eCurrentCategory = NKCUIPopupOfficeInteriorSelect.Category.FURNITURE;

		// Token: 0x04006B7E RID: 27518
		private NKCMiscSortSystem m_ssActive;

		// Token: 0x04006B7F RID: 27519
		private List<NKMOfficeThemePresetTemplet> m_lstThemePreset;

		// Token: 0x04006B80 RID: 27520
		private Dictionary<int, int> m_dicFurnitureListView;

		// Token: 0x04006B81 RID: 27521
		private NKCUIPopupOfficeInteriorSelect.Mode m_eMode;

		// Token: 0x04006B82 RID: 27522
		private List<NKCUISlot> m_lstVisibleSlot = new List<NKCUISlot>();

		// Token: 0x0200187A RID: 6266
		public enum Category
		{
			// Token: 0x0400A911 RID: 43281
			DECO,
			// Token: 0x0400A912 RID: 43282
			FURNITURE,
			// Token: 0x0400A913 RID: 43283
			THEME
		}

		// Token: 0x0200187B RID: 6267
		public enum Mode
		{
			// Token: 0x0400A915 RID: 43285
			FurnitureEdit,
			// Token: 0x0400A916 RID: 43286
			Listview
		}

		// Token: 0x0200187C RID: 6268
		// (Invoke) Token: 0x0600B602 RID: 46594
		public delegate void OnSelectInterior(int id);

		// Token: 0x0200187D RID: 6269
		// (Invoke) Token: 0x0600B606 RID: 46598
		public delegate void OnSelectPreset(int id);
	}
}
