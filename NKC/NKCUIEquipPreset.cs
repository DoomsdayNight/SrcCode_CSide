using System;
using System.Collections.Generic;
using ClientPacket.Item;
using DG.Tweening;
using DG.Tweening.Core;
using NKC.UI.Guide;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200098A RID: 2442
	public class NKCUIEquipPreset : MonoBehaviour
	{
		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x0600646F RID: 25711 RVA: 0x001FD8C2 File Offset: 0x001FBAC2
		public NKMUnitData UnitData
		{
			get
			{
				return this.m_cNKMUnitData;
			}
		}

		// Token: 0x06006470 RID: 25712 RVA: 0x001FD8CC File Offset: 0x001FBACC
		public static NKMEquipPresetData GetNewEmptyPresetData()
		{
			return new NKMEquipPresetData
			{
				presetIndex = 0,
				presetType = NKM_EQUIP_PRESET_TYPE.NEPT_NONE,
				presetName = "",
				equipUids = 
				{
					0L,
					0L,
					0L,
					0L
				}
			};
		}

		// Token: 0x06006471 RID: 25713 RVA: 0x001FD92C File Offset: 0x001FBB2C
		public void Init()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnInfomation, new UnityAction(this.OnClickInformation));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnPresetAdd, new UnityAction(this.OnClickPresetAdd));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnClose, new UnityAction(this.OnClickClose));
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglPresetFilter, new UnityAction<bool>(this.OnTogglePresetFilter));
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglOrderEdit, new UnityAction<bool>(this.OnToggleOrderEdit));
			if (this.m_tglPageArray != null)
			{
				int num = this.m_tglPageArray.Length;
				for (int i = 0; i < num; i++)
				{
					int page = i;
					NKCUtil.SetToggleValueChangedDelegate(this.m_tglPageArray[i], delegate(bool value)
					{
						this.OnClickPage(page);
					});
				}
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x06006472 RID: 25714 RVA: 0x001FDA04 File Offset: 0x001FBC04
		public void Open(NKMUnitData cNKMUnitData, List<NKMEquipPresetData> presetData, bool showFierceInfo)
		{
			if (cNKMUnitData == null)
			{
				return;
			}
			if (presetData != null && presetData.Count <= 0)
			{
				return;
			}
			if (presetData == null && NKCEquipPresetDataManager.ListEquipPresetData == null)
			{
				return;
			}
			if (NKCPopupUnitInfoDetail.IsInstanceOpen)
			{
				NKCPopupUnitInfoDetail.Instance.Close();
			}
			this.m_doTweenVisualManager.onEnableBehaviour = OnEnableBehaviour.Restart;
			base.gameObject.SetActive(true);
			this.m_doTweenVisualManager.onEnableBehaviour = OnEnableBehaviour.None;
			if (!this.m_bInitLoopScroll)
			{
				this.m_LoopScrollRect.dOnGetObject += this.GetPresetSlot;
				this.m_LoopScrollRect.dOnReturnObject += this.ReturnPresetSlot;
				this.m_LoopScrollRect.dOnProvideData += this.ProvidePresetData;
				this.m_LoopScrollRect.ContentConstraintCount = 1;
				this.m_LoopScrollRect.PrepareCells(0);
				NKCUtil.SetScrollHotKey(this.m_LoopScrollRect, null);
				this.m_bInitLoopScroll = true;
			}
			this.m_cNKMUnitData = cNKMUnitData;
			this.m_iNewPresetIndexFrom = ((presetData != null) ? presetData.Count : NKCEquipPresetDataManager.ListEquipPresetData.Count);
			this.m_bShowFierceInfo = showFierceInfo;
			this.m_bPresetFilterState = false;
			NKCUIComToggle tglPresetFilter = this.m_tglPresetFilter;
			if (tglPresetFilter != null)
			{
				tglPresetFilter.Select(this.m_bPresetFilterState, true, false);
			}
			NKCUIComToggle tglOrderEdit = this.m_tglOrderEdit;
			if (tglOrderEdit != null)
			{
				tglOrderEdit.SetLock(this.m_bPresetFilterState, false);
			}
			this.m_bMoveSlotState = false;
			NKCUIComToggle tglOrderEdit2 = this.m_tglOrderEdit;
			if (tglOrderEdit2 != null)
			{
				tglOrderEdit2.Select(this.m_bMoveSlotState, true, false);
			}
			NKCUIComToggle tglPresetFilter2 = this.m_tglPresetFilter;
			if (tglPresetFilter2 != null)
			{
				tglPresetFilter2.SetLock(this.m_bMoveSlotState, false);
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			long num = (nkmuserData != null) ? nkmuserData.m_UserUID : 0L;
			if (this.m_userUID != num && this.m_tglPageArray != null)
			{
				this.m_iCurrentPage = 0;
				this.m_userUID = num;
				if (this.m_tglPageArray.Length != 0 && this.m_tglPageArray[0] != null)
				{
					this.m_tglPageArray[0].Select(true, true, false);
				}
			}
			NKCEquipPresetDataManager.ListTempPresetData.Clear();
			this.UpdatePageLockState(this.m_iNewPresetIndexFrom);
			this.UpdatePresetData(presetData, presetData != null, 0, false);
		}

		// Token: 0x06006473 RID: 25715 RVA: 0x001FDBF5 File Offset: 0x001FBDF5
		public void ChangeUnitData(NKMUnitData unitData)
		{
			this.m_cNKMUnitData = unitData;
			this.RefreshScrollRect(false, 0, false);
			if (this.m_bPresetFilterState)
			{
				this.m_LoopScrollRect.Rebuild(CanvasUpdate.PostLayout);
			}
		}

		// Token: 0x06006474 RID: 25716 RVA: 0x001FDC1B File Offset: 0x001FBE1B
		public void UpdatePresetData(List<NKMEquipPresetData> listPresetData, bool setScrollPositon, int scrollIndex = 0, bool forceRefresh = false)
		{
			if (listPresetData != null)
			{
				NKCEquipPresetDataManager.ListEquipPresetData = listPresetData;
			}
			this.UpdatePresetNumber();
			this.RefreshScrollRect(setScrollPositon, scrollIndex, forceRefresh);
		}

		// Token: 0x06006475 RID: 25717 RVA: 0x001FDC38 File Offset: 0x001FBE38
		public void UpdatePresetSlot(NKMEquipPresetData presetData, bool registerAll = false)
		{
			if (NKCEquipPresetDataManager.ListEquipPresetData.Count > presetData.presetIndex)
			{
				NKCEquipPresetDataManager.ListEquipPresetData[presetData.presetIndex] = presetData;
			}
			else
			{
				Debug.LogWarning("Updating Preset Slot Index over index range");
			}
			if (this.m_bMoveSlotState)
			{
				NKCEquipPresetDataManager.UpdateTempPresetSlotData(presetData);
			}
			NKCUIEquipPresetSlot.ShowSetItemFx = true;
			NKCUIEquipPresetSlot.SavedPreset = registerAll;
			this.RefreshScrollRect(false, 0, false);
		}

		// Token: 0x06006476 RID: 25718 RVA: 0x001FDC98 File Offset: 0x001FBE98
		public void AddPresetSlot(int totalPresetCount)
		{
			if (NKCEquipPresetDataManager.ListEquipPresetData.Count >= totalPresetCount)
			{
				return;
			}
			int num = totalPresetCount - NKCEquipPresetDataManager.ListEquipPresetData.Count;
			this.m_iNewPresetIndexFrom = totalPresetCount - num;
			for (int i = 0; i < num; i++)
			{
				NKMEquipPresetData newEmptyPresetData = NKCUIEquipPreset.GetNewEmptyPresetData();
				newEmptyPresetData.presetIndex = this.m_iNewPresetIndexFrom + i;
				NKCEquipPresetDataManager.ListEquipPresetData.Add(newEmptyPresetData);
				if (this.m_bMoveSlotState)
				{
					NKCEquipPresetDataManager.ListTempPresetData.Add(newEmptyPresetData);
				}
			}
			this.UpdatePageLockState(NKCEquipPresetDataManager.ListEquipPresetData.Count);
			this.UpdatePresetData(null, false, 0, false);
		}

		// Token: 0x06006477 RID: 25719 RVA: 0x001FDD24 File Offset: 0x001FBF24
		public void UpdatePresetName(int presetIndex, string presetName)
		{
			NKMEquipPresetData nkmequipPresetData = NKCEquipPresetDataManager.ListEquipPresetData.Find((NKMEquipPresetData e) => e.presetIndex == presetIndex);
			if (nkmequipPresetData == null)
			{
				return;
			}
			nkmequipPresetData.presetName = presetName;
			this.RefreshScrollRect(false, 0, false);
		}

		// Token: 0x06006478 RID: 25720 RVA: 0x001FDD69 File Offset: 0x001FBF69
		public List<long> GetListEquipUid(int presetIndex)
		{
			if (NKCEquipPresetDataManager.ListEquipPresetData.Count <= presetIndex)
			{
				return null;
			}
			return NKCEquipPresetDataManager.ListEquipPresetData[presetIndex].equipUids;
		}

		// Token: 0x06006479 RID: 25721 RVA: 0x001FDD8A File Offset: 0x001FBF8A
		public bool IsOpened()
		{
			return base.gameObject.activeSelf;
		}

		// Token: 0x0600647A RID: 25722 RVA: 0x001FDD97 File Offset: 0x001FBF97
		public void CloseUI()
		{
			NKCEquipPresetDataManager.ListTempPresetData.Clear();
			NKCUIEquipPresetSlot.ResetUpdateIndex();
			base.gameObject.SetActive(false);
		}

		// Token: 0x0600647B RID: 25723 RVA: 0x001FDDB4 File Offset: 0x001FBFB4
		private void RefreshScrollRect(bool setScrollPosition, int scrollIndex = 0, bool forceRefresh = false)
		{
			if (NKCEquipPresetDataManager.ListEquipPresetData == null)
			{
				return;
			}
			if (!forceRefresh && !base.gameObject.activeSelf)
			{
				return;
			}
			this.m_listFilteredEquipPresetData.Clear();
			List<NKMEquipPresetData> presetDataListForPage = NKCEquipPresetDataManager.GetPresetDataListForPage(this.m_iCurrentPage, this.m_maxSlotCountPerPage, this.m_bMoveSlotState);
			if (this.m_bPresetFilterState)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_cNKMUnitData.m_UnitID);
				if (unitTempletBase == null)
				{
					return;
				}
				int count = presetDataListForPage.Count;
				for (int i = 0; i < count; i++)
				{
					NKM_EQUIP_PRESET_TYPE presetType = presetDataListForPage[i].presetType;
					bool flag = true;
					if (presetType != NKM_EQUIP_PRESET_TYPE.NEPT_NONE)
					{
						switch (presetType)
						{
						case NKM_EQUIP_PRESET_TYPE.NEPT_COUNTER:
							flag &= (unitTempletBase.m_NKM_UNIT_STYLE_TYPE == NKM_UNIT_STYLE_TYPE.NUST_COUNTER);
							break;
						case NKM_EQUIP_PRESET_TYPE.NEPT_SOLDIER:
							flag &= (unitTempletBase.m_NKM_UNIT_STYLE_TYPE == NKM_UNIT_STYLE_TYPE.NUST_SOLDIER);
							break;
						case NKM_EQUIP_PRESET_TYPE.NEPT_MECHANIC:
							flag &= (unitTempletBase.m_NKM_UNIT_STYLE_TYPE == NKM_UNIT_STYLE_TYPE.NUST_MECHANIC);
							break;
						}
					}
					if (flag)
					{
						this.m_listFilteredEquipPresetData.Add(presetDataListForPage[i]);
					}
				}
			}
			else
			{
				this.m_listFilteredEquipPresetData.AddRange(presetDataListForPage);
			}
			NKCEquipPresetDataManager.RefreshEquipUidHash();
			this.m_LoopScrollRect.TotalCount = ((presetDataListForPage.Count >= this.m_maxSlotCountPerPage) ? this.m_listFilteredEquipPresetData.Count : Mathf.Min(this.m_listFilteredEquipPresetData.Count + 1, this.m_maxSlotCountPerPage));
			this.m_LoopScrollRect.StopMovement();
			if (setScrollPosition)
			{
				this.m_LoopScrollRect.SetIndexPosition(scrollIndex);
				if (forceRefresh)
				{
					this.m_LoopScrollRect.RefreshCells(forceRefresh);
					return;
				}
			}
			else
			{
				this.m_LoopScrollRect.RefreshCellsForDynamicTotalCount(forceRefresh);
			}
		}

		// Token: 0x0600647C RID: 25724 RVA: 0x001FDF28 File Offset: 0x001FC128
		private RectTransform GetPresetSlot(int index)
		{
			NKCUIEquipPresetSlot newInstance = NKCUIEquipPresetSlot.GetNewInstance(null, false);
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x0600647D RID: 25725 RVA: 0x001FDF3C File Offset: 0x001FC13C
		private void UpdatePresetNumber()
		{
			if (base.gameObject.activeSelf)
			{
				NKCUtil.SetLabelText(this.m_lbPresetNumber, string.Format("{0}/{1}", NKCEquipPresetDataManager.ListEquipPresetData.Count, NKMCommonConst.EQUIP_PRESET_MAX_COUNT));
			}
		}

		// Token: 0x0600647E RID: 25726 RVA: 0x001FDF7C File Offset: 0x001FC17C
		private void UpdatePageLockState(int showSlotCount)
		{
			if (this.m_tglPageArray == null)
			{
				return;
			}
			int num = this.m_tglPageArray.Length;
			for (int i = 0; i < num; i++)
			{
				if (!(this.m_tglPageArray[i] == null))
				{
					this.m_tglPageArray[i].SetLock(showSlotCount < this.m_maxSlotCountPerPage * i, false);
				}
			}
		}

		// Token: 0x0600647F RID: 25727 RVA: 0x001FDFD0 File Offset: 0x001FC1D0
		private void ReturnPresetSlot(Transform tr)
		{
			NKCUIEquipPresetSlot component = tr.GetComponent<NKCUIEquipPresetSlot>();
			tr.SetParent(null);
			if (component != null)
			{
				component.DestoryInstance();
				return;
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x06006480 RID: 25728 RVA: 0x001FE008 File Offset: 0x001FC208
		private void ProvidePresetData(Transform tr, int index)
		{
			NKCUIEquipPresetSlot component = tr.GetComponent<NKCUIEquipPresetSlot>();
			if (component == null)
			{
				return;
			}
			if (this.m_listFilteredEquipPresetData.Count > index)
			{
				int count = this.m_listFilteredEquipPresetData[index].equipUids.Count;
				component.SetData(index, this.m_listFilteredEquipPresetData[index], this, this.m_iNewPresetIndexFrom, this.m_bShowFierceInfo, this.m_bMoveSlotState, this.m_LoopScrollRect, new NKCUIEquipPresetSlot.OnClickAdd(this.OnClickPresetAdd), new NKCUIEquipPresetSlot.OnClickUp(this.OnClickSlotUp), new NKCUIEquipPresetSlot.OnClickDown(this.OnClickSlotDown));
				if (index == this.m_listFilteredEquipPresetData.Count - 1 && NKCEquipPresetDataManager.IsLastPage(this.m_iCurrentPage, this.m_maxSlotCountPerPage))
				{
					this.m_iNewPresetIndexFrom = NKCEquipPresetDataManager.ListEquipPresetData.Count;
					return;
				}
			}
			else
			{
				component.SetData(NKCEquipPresetDataManager.ListEquipPresetData.Count, null, this, this.m_iNewPresetIndexFrom, this.m_bShowFierceInfo, this.m_bMoveSlotState, this.m_LoopScrollRect, new NKCUIEquipPresetSlot.OnClickAdd(this.OnClickPresetAdd), null, null);
			}
		}

		// Token: 0x06006481 RID: 25729 RVA: 0x001FE10C File Offset: 0x001FC30C
		private void OnClickPresetAdd()
		{
			if (NKCEquipPresetDataManager.ListEquipPresetData.Count >= NKMCommonConst.EQUIP_PRESET_MAX_COUNT)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_EQUIP_PRESET_SLOT_FULL, null, "");
				return;
			}
			NKCPopupInventoryAdd.SliderInfo sliderInfo = default(NKCPopupInventoryAdd.SliderInfo);
			sliderInfo.increaseCount = 1;
			sliderInfo.maxCount = NKMCommonConst.EQUIP_PRESET_MAX_COUNT;
			sliderInfo.currentCount = NKCEquipPresetDataManager.ListEquipPresetData.Count;
			sliderInfo.inventoryType = NKM_INVENTORY_EXPAND_TYPE.NIET_NONE;
			NKCPopupInventoryAdd.Instance.Open(NKCUtilString.GET_STRING_EQUIP_PRESET_ADD_TITLE, NKCUtilString.GET_STRING_EQUIP_PRESET_ADD_CONTENT, sliderInfo, NKMCommonConst.EQUIP_PRESET_EXPAND_COST_VALUE, NKMCommonConst.EQUIP_PRESET_EXPAND_COST_ITEM_ID, delegate(int value)
			{
				NKCPacketSender.Send_NKMPacket_EQUIP_PRESET_ADD_REQ(value);
			}, false);
		}

		// Token: 0x06006482 RID: 25730 RVA: 0x001FE1B4 File Offset: 0x001FC3B4
		private void OnClickInformation()
		{
			NKCUIPopUpGuide.Instance.Open(this.m_strGuideArticleID, 0);
		}

		// Token: 0x06006483 RID: 25731 RVA: 0x001FE1C7 File Offset: 0x001FC3C7
		private void OnTogglePresetFilter(bool value)
		{
			this.m_bPresetFilterState = value;
			NKCUIComToggle tglOrderEdit = this.m_tglOrderEdit;
			if (tglOrderEdit != null)
			{
				tglOrderEdit.SetLock(value, false);
			}
			this.RefreshScrollRect(value, 0, false);
		}

		// Token: 0x06006484 RID: 25732 RVA: 0x001FE1EC File Offset: 0x001FC3EC
		private void OnToggleOrderEdit(bool value)
		{
			NKCUIComToggle tglPresetFilter = this.m_tglPresetFilter;
			if (tglPresetFilter != null)
			{
				tglPresetFilter.SetLock(value, false);
			}
			this.m_bMoveSlotState = value;
			if (value)
			{
				NKCEquipPresetDataManager.ListTempPresetData.Clear();
				NKCEquipPresetDataManager.ListTempPresetData.AddRange(NKCEquipPresetDataManager.ListEquipPresetData);
				this.RefreshScrollRect(false, 0, false);
				return;
			}
			List<NKMPacket_EQUIP_PRESET_CHANGE_INDEX_REQ.PresetIndexData> movedSlotIndexList = NKCEquipPresetDataManager.GetMovedSlotIndexList();
			if (movedSlotIndexList.Count > 0)
			{
				NKCPacketSender.Send_NKMPacket_EQUIP_PRESET_CHANGE_INDEX_REQ(movedSlotIndexList);
			}
			else
			{
				this.RefreshScrollRect(false, 0, false);
			}
			NKCEquipPresetDataManager.ListTempPresetData.Clear();
		}

		// Token: 0x06006485 RID: 25733 RVA: 0x001FE263 File Offset: 0x001FC463
		private void OnClickPage(int page)
		{
			this.m_iCurrentPage = page;
			this.RefreshScrollRect(true, 0, false);
		}

		// Token: 0x06006486 RID: 25734 RVA: 0x001FE278 File Offset: 0x001FC478
		private void OnClickSlotUp(NKMEquipPresetData presetData)
		{
			int tempPresetDataIndex = NKCEquipPresetDataManager.GetTempPresetDataIndex(presetData);
			if (!this.m_enableInterPageSlotMove && tempPresetDataIndex % this.m_maxSlotCountPerPage == 0)
			{
				return;
			}
			NKCEquipPresetDataManager.SwapTempPresetData(tempPresetDataIndex - 1, tempPresetDataIndex);
			this.RefreshScrollRect(false, 0, false);
		}

		// Token: 0x06006487 RID: 25735 RVA: 0x001FE2B4 File Offset: 0x001FC4B4
		private void OnClickSlotDown(NKMEquipPresetData presetData)
		{
			int tempPresetDataIndex = NKCEquipPresetDataManager.GetTempPresetDataIndex(presetData);
			if (!this.m_enableInterPageSlotMove && tempPresetDataIndex % this.m_maxSlotCountPerPage == this.m_maxSlotCountPerPage - 1)
			{
				return;
			}
			NKCEquipPresetDataManager.SwapTempPresetData(tempPresetDataIndex, tempPresetDataIndex + 1);
			this.RefreshScrollRect(false, 0, false);
		}

		// Token: 0x06006488 RID: 25736 RVA: 0x001FE2F5 File Offset: 0x001FC4F5
		public void OnClickClose()
		{
			this.CloseUI();
		}

		// Token: 0x06006489 RID: 25737 RVA: 0x001FE300 File Offset: 0x001FC500
		private void OnDestroy()
		{
			this.m_csbtnPresetAdd = null;
			this.m_csbtnClose = null;
			this.m_tglPresetFilter = null;
			this.m_LoopScrollRect = null;
			this.m_lbPresetNumber = null;
			List<NKMEquipPresetData> listFilteredEquipPresetData = this.m_listFilteredEquipPresetData;
			if (listFilteredEquipPresetData != null)
			{
				listFilteredEquipPresetData.Clear();
			}
			this.m_listFilteredEquipPresetData = null;
			this.m_cNKMUnitData = null;
		}

		// Token: 0x0400500A RID: 20490
		public DOTweenVisualManager m_doTweenVisualManager;

		// Token: 0x0400500B RID: 20491
		public NKCUIComStateButton m_csbtnInfomation;

		// Token: 0x0400500C RID: 20492
		public NKCUIComStateButton m_csbtnPresetAdd;

		// Token: 0x0400500D RID: 20493
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x0400500E RID: 20494
		public NKCUIComToggle m_tglPresetFilter;

		// Token: 0x0400500F RID: 20495
		public NKCUIComToggle m_tglOrderEdit;

		// Token: 0x04005010 RID: 20496
		public LoopScrollRect m_LoopScrollRect;

		// Token: 0x04005011 RID: 20497
		public Text m_lbPresetNumber;

		// Token: 0x04005012 RID: 20498
		public string m_strGuideArticleID;

		// Token: 0x04005013 RID: 20499
		[Header("페이지 설정")]
		public int m_maxSlotCountPerPage;

		// Token: 0x04005014 RID: 20500
		public bool m_enableInterPageSlotMove;

		// Token: 0x04005015 RID: 20501
		public NKCUIComToggle[] m_tglPageArray;

		// Token: 0x04005016 RID: 20502
		private List<NKMEquipPresetData> m_listFilteredEquipPresetData = new List<NKMEquipPresetData>();

		// Token: 0x04005017 RID: 20503
		private NKMUnitData m_cNKMUnitData;

		// Token: 0x04005018 RID: 20504
		private long m_userUID;

		// Token: 0x04005019 RID: 20505
		private int m_iNewPresetIndexFrom;

		// Token: 0x0400501A RID: 20506
		private int m_iCurrentPage;

		// Token: 0x0400501B RID: 20507
		private bool m_bShowFierceInfo;

		// Token: 0x0400501C RID: 20508
		private bool m_bPresetFilterState;

		// Token: 0x0400501D RID: 20509
		private bool m_bInitLoopScroll;

		// Token: 0x0400501E RID: 20510
		private bool m_bMoveSlotState;
	}
}
