using System;
using System.Collections.Generic;
using System.Linq;
using NKC.UI;
using NKC.UI.Collection;
using NKC.UI.Component;
using NKM;
using NKM.Contract2;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000806 RID: 2054
	public class NKCUIRearmamentProcess : MonoBehaviour
	{
		// Token: 0x0600515B RID: 20827 RVA: 0x0018B270 File Offset: 0x00189470
		public void Init(NKCUIRearmamentProcess.ChangeState func = null)
		{
			NKCUtil.SetBindFunction(this.m_RearmUnitInfo, new UnityAction(this.OnClickUnitInfo));
			NKCUtil.SetBindFunction(this.m_UnitSelectBtn, new UnityAction(this.OnClickTargetSelectBtn));
			NKCUtil.SetBindFunction(this.m_RearmBtn, new UnityAction(this.OnClickRearmBtn));
			if (this.m_LoopScroll != null)
			{
				this.m_LoopScroll.dOnGetObject += this.GetUnitSelectSlot;
				this.m_LoopScroll.dOnReturnObject += this.ReturnUnitSelectSlot;
				this.m_LoopScroll.dOnProvideData += this.ProvideUnitSelectSlot;
				this.m_LoopScroll.PrepareCells(0);
				NKCUtil.SetScrollHotKey(this.m_LoopScroll, null);
			}
			if (this.m_SortUI != null)
			{
				this.m_SortUI.Init(new NKCUIComUnitSortOptions.OnSorted(this.OnSortChanged), true);
				this.m_SortUI.RegisterCategories(this.eRearmFilterCategories, this.eRearmSortCategories, false);
			}
			this.InitRearmData();
			this.InitProcessUI();
			this.m_CharInfoSummary.Init(false);
			this.dOk = func;
		}

		// Token: 0x0600515C RID: 20828 RVA: 0x0018B38A File Offset: 0x0018958A
		public void SetReserveRearmData(int iTargetRearmTypeUnitID, long iResourceRearmUnitUID)
		{
			this.m_iCurSelectedUnitID = iTargetRearmTypeUnitID;
			this.m_iCurRearmResourceUID = iResourceRearmUnitUID;
		}

		// Token: 0x0600515D RID: 20829 RVA: 0x0018B39A File Offset: 0x0018959A
		public void Clear()
		{
			this.m_iCurSelectedUnitID = 0;
			this.m_iCurRearmResourceUID = 0L;
			NKCUIUnitSelectList uiunitSelectList = this.m_UIUnitSelectList;
			if (uiunitSelectList != null)
			{
				uiunitSelectList.Close();
			}
			this.m_UIUnitSelectList = null;
		}

		// Token: 0x0600515E RID: 20830 RVA: 0x0018B3C3 File Offset: 0x001895C3
		private void OnClickUnitInfo()
		{
			if (this.m_iCurSelectedUnitID == 0)
			{
				return;
			}
			NKCUICollectionUnitInfo.CheckInstanceAndOpen(NKCUtil.MakeDummyUnit(this.m_iCurSelectedUnitID, true), null, null, NKCUICollectionUnitInfo.eCollectionState.CS_PROFILE, false, NKCUIUpsideMenu.eMode.Normal);
		}

		// Token: 0x0600515F RID: 20831 RVA: 0x0018B3E4 File Offset: 0x001895E4
		private void OnClickTargetSelectBtn()
		{
			if (this.m_iCurSelectedUnitID == 0)
			{
				return;
			}
			this.m_iCurRearmResourceUID = 0L;
			NKCUIRearmamentProcess.ChangeState changeState = this.dOk;
			if (changeState == null)
			{
				return;
			}
			changeState(NKCUIRearmament.REARM_TYPE.RT_PROCESS);
		}

		// Token: 0x06005160 RID: 20832 RVA: 0x0018B408 File Offset: 0x00189608
		public void Open(NKCUIRearmament.REARM_TYPE type = NKCUIRearmament.REARM_TYPE.RT_LIST)
		{
			if (type == NKCUIRearmament.REARM_TYPE.RT_LIST)
			{
				this.InitRearmListSortSystem();
				if (this.m_iCurSelectedUnitID == 0)
				{
					this.m_iCurSelectedUnitID = this.m_SortSystem.SortedUnitList[0].m_UnitID;
				}
				this.ChangeSlotSelectedState(this.m_iCurSelectedUnitID);
				this.OnSortChanged(true);
			}
			else
			{
				int fromUnitID = NKCRearmamentUtil.GetFromUnitID(this.m_iCurSelectedUnitID);
				if (fromUnitID != 0)
				{
					this.m_RearmSelectSubUI.SetData(fromUnitID, this.m_iCurSelectedUnitID, this.m_iCurRearmResourceUID);
				}
				else
				{
					this.m_RearmSelectSubUI.SetData(this.m_lstRearm[0].FromUnitTemplet.m_UnitID, 0, 0L);
				}
			}
			this.ChangeUI(type);
			this.UpdateUI();
		}

		// Token: 0x06005161 RID: 20833 RVA: 0x0018B4B4 File Offset: 0x001896B4
		private void ChangeUI(NKCUIRearmament.REARM_TYPE type)
		{
			NKCUtil.SetGameobjectActive(this.m_objRearmProcess, type == NKCUIRearmament.REARM_TYPE.RT_PROCESS);
			NKCUtil.SetGameobjectActive(this.m_objRearmList, type == NKCUIRearmament.REARM_TYPE.RT_LIST);
			NKCUtil.SetGameobjectActive(this.m_objRearmProcess, type == NKCUIRearmament.REARM_TYPE.RT_PROCESS);
			NKCUtil.SetGameobjectActive(this.m_objRearmList, type == NKCUIRearmament.REARM_TYPE.RT_LIST);
			NKCUtil.SetGameobjectActive(this.m_LoopScroll.content.gameObject, type == NKCUIRearmament.REARM_TYPE.RT_LIST);
			this.m_curUIType = type;
		}

		// Token: 0x06005162 RID: 20834 RVA: 0x0018B520 File Offset: 0x00189720
		private void InitRearmListSortSystem()
		{
			this.m_SortSystem = null;
			NKCUIUnitSelectList.UnitSelectListOptions currentOption = new NKCUIUnitSelectList.UnitSelectListOptions(NKM_UNIT_TYPE.NUT_NORMAL, true, NKM_DECK_TYPE.NDT_NORMAL, NKCUIUnitSelectList.eUnitSelectListMode.Normal, true);
			currentOption.setUnitFilterCategory = this.eRearmFilterCategories;
			currentOption.lstSortOption = NKCUIRearmamentProcess.eRearmSortLists;
			currentOption.bShowHideDeckedUnitMenu = true;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_currentOption = currentOption;
			List<NKMUnitData> list = new List<NKMUnitData>();
			foreach (NKMUnitRearmamentTemplet nkmunitRearmamentTemplet in this.m_lstRearm)
			{
				list.Add(new NKMUnitData
				{
					m_UnitID = nkmunitRearmamentTemplet.Key,
					m_UnitUID = (long)nkmunitRearmamentTemplet.Key
				});
			}
			this.m_SortSystem = new NKCGenericUnitSort(null, this.m_currentOption.m_SortOptions, list);
			this.m_SortUI.RegisterUnitSort(this.m_SortSystem);
			this.m_SortUI.ResetUI(false);
		}

		// Token: 0x06005163 RID: 20835 RVA: 0x0018B618 File Offset: 0x00189818
		private void OnSortChanged(bool bResetScroll)
		{
			if (this.m_SortSystem != null)
			{
				this.m_LoopScroll.TotalCount = this.m_SortSystem.SortedUnitList.Count;
				if (bResetScroll)
				{
					this.m_LoopScroll.SetIndexPosition(0);
					this.m_LoopScroll.RefreshCells(true);
				}
				else
				{
					this.m_LoopScroll.RefreshCells(false);
				}
				this.ChangeSlotSelectedState(this.m_iCurSelectedUnitID);
			}
		}

		// Token: 0x06005164 RID: 20836 RVA: 0x0018B680 File Offset: 0x00189880
		public RectTransform GetRearmSlotRectTransform(int iRearmUnitID)
		{
			foreach (NKCUIUnitSelectListSlot nkcuiunitSelectListSlot in this.m_lstVisbleSlots)
			{
				if (nkcuiunitSelectListSlot.NKMUnitData.m_UnitID == iRearmUnitID)
				{
					return nkcuiunitSelectListSlot.GetComponent<RectTransform>();
				}
			}
			return null;
		}

		// Token: 0x06005165 RID: 20837 RVA: 0x0018B6E8 File Offset: 0x001898E8
		private void InitRearmData()
		{
			this.m_lstRearm.Clear();
			foreach (NKMUnitRearmamentTemplet nkmunitRearmamentTemplet in NKMTempletContainer<NKMUnitRearmamentTemplet>.Values)
			{
				if (nkmunitRearmamentTemplet.EnableByTag)
				{
					this.m_lstRearm.Add(nkmunitRearmamentTemplet);
				}
			}
			if (this.m_lstRearm.Count <= 0)
			{
				Debug.Log("<color=red>Unit Rearm Data is null</color>");
				return;
			}
			from x in this.m_lstRearm
			orderby x.Key
			select x;
		}

		// Token: 0x06005166 RID: 20838 RVA: 0x0018B790 File Offset: 0x00189990
		private RectTransform GetUnitSelectSlot(int index)
		{
			NKCUIUnitSelectListSlot nkcuiunitSelectListSlot = UnityEngine.Object.Instantiate<NKCUIUnitSelectListSlot>(this.m_pfbUnitSelectSlot);
			nkcuiunitSelectListSlot.Init(false);
			nkcuiunitSelectListSlot.transform.localScale = Vector3.one;
			this.m_lstVisbleSlots.Add(nkcuiunitSelectListSlot);
			return nkcuiunitSelectListSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06005167 RID: 20839 RVA: 0x0018B7D4 File Offset: 0x001899D4
		private void ReturnUnitSelectSlot(Transform go)
		{
			NKCUIUnitSelectListSlot slot = go.GetComponent<NKCUIUnitSelectListSlot>();
			if (slot != null)
			{
				NKCUIUnitSelectListSlot nkcuiunitSelectListSlot = this.m_lstVisbleSlots.Find((NKCUIUnitSelectListSlot x) => x.NKMUnitData.m_UnitID == slot.NKMUnitData.m_UnitID);
				if (nkcuiunitSelectListSlot != null)
				{
					this.m_lstVisbleSlots.Remove(nkcuiunitSelectListSlot);
				}
			}
			NKCUtil.SetGameobjectActive(go, false);
			UnityEngine.Object.Destroy(go.gameObject);
		}

		// Token: 0x06005168 RID: 20840 RVA: 0x0018B844 File Offset: 0x00189A44
		private void ProvideUnitSelectSlot(Transform tr, int idx)
		{
			if (this.m_SortSystem == null)
			{
				Debug.LogError("Slot Sort System Null!!");
				return;
			}
			NKCUIUnitSelectListSlot component = tr.GetComponent<NKCUIUnitSelectListSlot>();
			if (component != null)
			{
				if (this.m_SortSystem.SortedUnitList.Count <= idx || idx < 0)
				{
					Debug.LogError(string.Format("m_SortSystem.SortedUnitList - 잘못된 인덱스 입니다, {0}", idx));
					return;
				}
				tr.SetParent(this.m_LoopScroll.content);
				NKMUnitData nkmunitData = new NKMUnitData();
				nkmunitData.m_UnitID = this.m_SortSystem.SortedUnitList[idx].m_UnitID;
				nkmunitData.m_SkinID = 0;
				nkmunitData.m_UnitLevel = 1;
				component.Init(true);
				component.SetDataForRearm(nkmunitData, new NKMDeckIndex(NKM_DECK_TYPE.NDT_NONE), false, new NKCUIUnitSelectListSlotBase.OnSelectThisSlot(this.OnSlotClicked), false, false, false);
				if (nkmunitData.m_UnitID == this.m_iCurSelectedUnitID)
				{
					component.SetSlotSelectState(NKCUIUnitSelectList.eUnitSlotSelectState.SELECTED);
				}
				else
				{
					component.SetSlotSelectState(NKCUIUnitSelectList.eUnitSlotSelectState.DISABLE);
				}
				this.m_lstVisbleSlots.Add(component);
			}
		}

		// Token: 0x06005169 RID: 20841 RVA: 0x0018B933 File Offset: 0x00189B33
		private void UpdateUI()
		{
			this.UpdateCommonUI();
			if (this.m_curUIType == NKCUIRearmament.REARM_TYPE.RT_LIST)
			{
				this.UpdateRearmListUI();
				return;
			}
			this.UpdateRearmProcessUI();
		}

		// Token: 0x0600516A RID: 20842 RVA: 0x0018B951 File Offset: 0x00189B51
		private void UpdateRearmListUI()
		{
			this.ChangeSlotSelectedState(this.m_iCurSelectedUnitID);
			this.m_RearmSummaryInfo.SetData(this.m_iCurSelectedUnitID, false);
		}

		// Token: 0x0600516B RID: 20843 RVA: 0x0018B974 File Offset: 0x00189B74
		private void UpdateCommonUI()
		{
			NKMUnitData nkmunitData = NKCUtil.MakeDummyUnit(this.m_iCurSelectedUnitID, true);
			this.m_CharInfoSummary.SetData(nkmunitData);
			this.m_CharInfoSummary.SetEnableClassStar(false);
			this.m_charView.CloseCharacterIllust();
			this.m_charView.SetCharacterIllust(nkmunitData, false, true, true, 0);
		}

		// Token: 0x0600516C RID: 20844 RVA: 0x0018B9C1 File Offset: 0x00189BC1
		public void OnSlotClicked(NKMUnitData unitData, NKMUnitTempletBase unitTempletBase, NKMDeckIndex deckIndex, NKCUnitSortSystem.eUnitState slotState, NKCUIUnitSelectList.eUnitSlotSelectState unitSlotSelectState)
		{
			this.m_iCurSelectedUnitID = unitData.m_UnitID;
			this.UpdateUI();
		}

		// Token: 0x0600516D RID: 20845 RVA: 0x0018B9D8 File Offset: 0x00189BD8
		private void ChangeSlotSelectedState(int unitID)
		{
			foreach (NKCUIUnitSelectListSlot nkcuiunitSelectListSlot in this.m_lstVisbleSlots)
			{
				if (nkcuiunitSelectListSlot.NKMUnitData != null && nkcuiunitSelectListSlot.NKMUnitData.m_UnitID == unitID)
				{
					nkcuiunitSelectListSlot.SetSlotSelectState(NKCUIUnitSelectList.eUnitSlotSelectState.SELECTED);
				}
				else
				{
					nkcuiunitSelectListSlot.SetSlotSelectState(NKCUIUnitSelectList.eUnitSlotSelectState.DISABLE);
				}
			}
		}

		// Token: 0x0600516E RID: 20846 RVA: 0x0018BA4C File Offset: 0x00189C4C
		private void InitProcessUI()
		{
			NKCUIUnitSelectListSlot rearmTargetUnitSlot = this.m_RearmTargetUnitSlot;
			if (rearmTargetUnitSlot != null)
			{
				rearmTargetUnitSlot.Init(true);
			}
			NKCUtil.SetBindFunction(this.m_csbtnSelectResourceUnit, delegate()
			{
				this.OnClickSelectResourceUnit(0L);
			});
			this.m_RearmSelectSubUI.Init(new NKCUIRearmamentSubUISelectList.OnSelectedUnitID(this.SelectTargetRearmUnit));
		}

		// Token: 0x0600516F RID: 20847 RVA: 0x0018BA99 File Offset: 0x00189C99
		private void SelectTargetRearmUnit(int targetUnitID)
		{
			this.m_iCurSelectedUnitID = targetUnitID;
			this.UpdateUI();
		}

		// Token: 0x06005170 RID: 20848 RVA: 0x0018BAA8 File Offset: 0x00189CA8
		private void UpdateRearmProcessUI()
		{
			this.LeftRearmTargetUI();
			this.UpdateRearmSubUIUnitSlotUI();
			this.UpdateCostSlotUI();
			this.UpdateButtonUI();
			this.m_RearmSummarySkill.SetData(this.m_iCurSelectedUnitID, true);
		}

		// Token: 0x06005171 RID: 20849 RVA: 0x0018BAD4 File Offset: 0x00189CD4
		private void UpdateRearmSubUIUnitSlotUI()
		{
			this.m_RearmSelectSubUI.UpdateReamUnitSlotData(this.m_iCurRearmResourceUID);
		}

		// Token: 0x06005172 RID: 20850 RVA: 0x0018BAE8 File Offset: 0x00189CE8
		private void UpdateCostSlotUI()
		{
			int num = 1;
			NKMUnitData unitFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(this.m_iCurRearmResourceUID);
			if (unitFromUID != null)
			{
				num = unitFromUID.m_UnitLevel;
			}
			NKCUIItemCostSlot requestLvSlot = this.m_RequestLvSlot;
			if (requestLvSlot != null)
			{
				requestLvSlot.SetData(910, 110, (long)num, true, true, false);
			}
			NKMUnitRearmamentTemplet rearmamentTemplet = NKCRearmamentUtil.GetRearmamentTemplet(this.m_iCurSelectedUnitID);
			if (rearmamentTemplet != null)
			{
				for (int i = 0; i < this.m_lstRequestItemSlot.Count; i++)
				{
					if (rearmamentTemplet.RearmamentUseItems.Count > i)
					{
						MiscItemUnit miscItemUnit = rearmamentTemplet.RearmamentUseItems[i];
						this.m_lstRequestItemSlot[i].SetData(miscItemUnit.ItemId, (int)miscItemUnit.Count, NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(miscItemUnit.ItemId), true, true, false);
					}
				}
			}
		}

		// Token: 0x06005173 RID: 20851 RVA: 0x0018BBB0 File Offset: 0x00189DB0
		private void LeftRearmTargetUI()
		{
			NKMUnitData unitFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(this.m_iCurRearmResourceUID);
			if (unitFromUID != null)
			{
				this.m_RearmTargetUnitSlot.SetDataForRearm(unitFromUID, new NKMDeckIndex(NKM_DECK_TYPE.NDT_NONE), false, new NKCUIUnitSelectListSlotBase.OnSelectThisSlot(this.OnResourceSlotClicked), false, false, false);
			}
			bool flag = unitFromUID != null;
			NKCUtil.SetGameobjectActive(this.m_TargetEmptyObj, !flag);
			NKCUtil.SetGameobjectActive(this.m_RearmTargetUnitSlot.gameObject, flag);
			NKCUtil.SetGameobjectActive(this.m_TargetEmptyText.gameObject, !flag);
			NKCUtil.SetGameobjectActive(this.m_TargetSelectedText.gameObject, flag);
		}

		// Token: 0x06005174 RID: 20852 RVA: 0x0018BC44 File Offset: 0x00189E44
		private void UpdateButtonUI()
		{
			bool flag = false;
			NKMUnitData unitFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(this.m_iCurRearmResourceUID);
			if (unitFromUID != null && unitFromUID.m_UnitLevel >= 110)
			{
				bool flag2 = true;
				NKMUnitRearmamentTemplet rearmamentTemplet = NKCRearmamentUtil.GetRearmamentTemplet(this.m_iCurSelectedUnitID);
				if (rearmamentTemplet != null)
				{
					for (int i = 0; i < this.m_lstRequestItemSlot.Count; i++)
					{
						if (rearmamentTemplet.RearmamentUseItems.Count > i)
						{
							MiscItemUnit miscItemUnit = rearmamentTemplet.RearmamentUseItems[i];
							if ((long)((int)miscItemUnit.Count) > NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(miscItemUnit.ItemId))
							{
								flag2 = false;
								break;
							}
						}
					}
					flag = flag2;
				}
			}
			if (flag)
			{
				NKCUtil.SetImageSprite(this.m_imgRearmBtn, NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_YELLOW), false);
			}
			else
			{
				NKCUtil.SetImageSprite(this.m_imgRearmBtn, NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_GRAY), false);
			}
			NKCUtil.SetLabelTextColor(this.m_lbRearmBtn, NKCUtil.GetButtonUIColor(flag));
		}

		// Token: 0x06005175 RID: 20853 RVA: 0x0018BD20 File Offset: 0x00189F20
		private void OnClickRearmBtn()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKMUnitData unitFromUID = nkmuserData.m_ArmyData.GetUnitFromUID(this.m_iCurRearmResourceUID);
			if (unitFromUID == null)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_REARM_PROCESS_BLOCK_MESSAGE_EMPTY_TARGET_UNIT, null, "");
				return;
			}
			if (unitFromUID.m_UnitLevel < 110)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_REARM_PROCESS_BLOCK_MESSAGE_LACK_COND, null, "");
				return;
			}
			NKMEquipmentSet equipmentSet = unitFromUID.GetEquipmentSet(nkmuserData.m_InventoryData);
			if (equipmentSet.Weapon != null || equipmentSet.Defence != null || equipmentSet.Accessory != null || equipmentSet.Accessory2 != null)
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_REARM_PROCESS_BLOCK_MESSAGE_EQUIPED, new NKCPopupOKCancel.OnButton(this.UnEquipAllItems), null, false);
				return;
			}
			NKMUnitRearmamentTemplet rearmamentTemplet = NKCRearmamentUtil.GetRearmamentTemplet(this.m_iCurSelectedUnitID);
			if (rearmamentTemplet != null)
			{
				for (int i = 0; i < this.m_lstRequestItemSlot.Count; i++)
				{
					if (rearmamentTemplet.RearmamentUseItems.Count > i)
					{
						MiscItemUnit miscItemUnit = rearmamentTemplet.RearmamentUseItems[i];
						if ((long)((int)miscItemUnit.Count) > NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(miscItemUnit.ItemId))
						{
							NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_REARM_PROCESS_BLOCK_MESSAGE_LACK_COND, null, "");
							return;
						}
					}
				}
			}
			NKCUIPopupRearmamentConfirm.Instance.Open(this.m_iCurSelectedUnitID, this.m_iCurRearmResourceUID);
		}

		// Token: 0x06005176 RID: 20854 RVA: 0x0018BE60 File Offset: 0x0018A060
		private void UnEquipAllItems()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKMUnitData unitFromUID = nkmuserData.m_ArmyData.GetUnitFromUID(this.m_iCurRearmResourceUID);
			if (unitFromUID == null)
			{
				return;
			}
			NKMEquipmentSet equipmentSet = unitFromUID.GetEquipmentSet(nkmuserData.m_InventoryData);
			this.SendUnEquipItemREQ(this.m_iCurRearmResourceUID, equipmentSet.Weapon, ITEM_EQUIP_POSITION.IEP_WEAPON);
			this.SendUnEquipItemREQ(this.m_iCurRearmResourceUID, equipmentSet.Defence, ITEM_EQUIP_POSITION.IEP_DEFENCE);
			this.SendUnEquipItemREQ(this.m_iCurRearmResourceUID, equipmentSet.Accessory, ITEM_EQUIP_POSITION.IEP_ACC);
			this.SendUnEquipItemREQ(this.m_iCurRearmResourceUID, equipmentSet.Accessory2, ITEM_EQUIP_POSITION.IEP_ACC2);
		}

		// Token: 0x06005177 RID: 20855 RVA: 0x0018BEE2 File Offset: 0x0018A0E2
		private void SendUnEquipItemREQ(long unitUID, NKMEquipItemData equipData, ITEM_EQUIP_POSITION position)
		{
			if (equipData == null)
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_EQUIP_ITEM_EQUIP_REQ(false, unitUID, equipData.m_ItemUid, position);
		}

		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x06005178 RID: 20856 RVA: 0x0018BEF6 File Offset: 0x0018A0F6
		private NKCUIUnitSelectList UnitSelectList
		{
			get
			{
				if (this.m_UIUnitSelectList == null)
				{
					this.m_UIUnitSelectList = NKCUIUnitSelectList.OpenNewInstance(true);
				}
				return this.m_UIUnitSelectList;
			}
		}

		// Token: 0x06005179 RID: 20857 RVA: 0x0018BF18 File Offset: 0x0018A118
		public void OnResourceSlotClicked(NKMUnitData unitData, NKMUnitTempletBase unitTempletBase, NKMDeckIndex deckIndex, NKCUnitSortSystem.eUnitState slotStatem, NKCUIUnitSelectList.eUnitSlotSelectState slotSelectState)
		{
			this.OnClickSelectResourceUnit(unitData.m_UnitUID);
		}

		// Token: 0x0600517A RID: 20858 RVA: 0x0018BF28 File Offset: 0x0018A128
		private void OnClickSelectResourceUnit(long lSelectedUnitUID = 0L)
		{
			NKMUnitRearmamentTemplet rearmamentTemplet = NKCRearmamentUtil.GetRearmamentTemplet(this.m_iCurSelectedUnitID);
			if (rearmamentTemplet == null)
			{
				return;
			}
			NKCUIUnitSelectList.UnitSelectListOptions unitSelectListOptions = new NKCUIUnitSelectList.UnitSelectListOptions(NKM_UNIT_TYPE.NUT_NORMAL, true, NKM_DECK_TYPE.NDT_NORMAL, NKCUIUnitSelectList.eUnitSelectListMode.Normal, true);
			unitSelectListOptions.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
			unitSelectListOptions.lstSortOption = NKCUnitSortSystem.GetDefaultSortOptions(NKM_UNIT_TYPE.NUT_NORMAL, false, false);
			unitSelectListOptions.bDescending = false;
			unitSelectListOptions.bShowRemoveSlot = false;
			unitSelectListOptions.bMultipleSelect = false;
			unitSelectListOptions.bExcludeLockedUnit = false;
			unitSelectListOptions.bExcludeDeckedUnit = false;
			unitSelectListOptions.m_SortOptions.bUseDeckedState = false;
			unitSelectListOptions.m_SortOptions.bUseLockedState = false;
			unitSelectListOptions.m_SortOptions.bUseDormInState = false;
			unitSelectListOptions.dOnSelectedUnitWarning = null;
			unitSelectListOptions.m_SortOptions.bIgnoreCityState = true;
			unitSelectListOptions.m_SortOptions.bIgnoreWorldMapLeader = true;
			unitSelectListOptions.bShowHideDeckedUnitMenu = false;
			unitSelectListOptions.bHideDeckedUnit = false;
			unitSelectListOptions.setOnlyIncludeUnitID = new HashSet<int>();
			unitSelectListOptions.setOnlyIncludeUnitID.Add(rearmamentTemplet.FromUnitTemplet.m_BaseUnitID);
			unitSelectListOptions.setSelectedUnitUID = new HashSet<long>();
			if (lSelectedUnitUID != 0L)
			{
				unitSelectListOptions.setSelectedUnitUID.Add(lSelectedUnitUID);
			}
			unitSelectListOptions.setExcludeUnitID = NKCUnitSortSystem.GetDefaultExcludeUnitIDs();
			unitSelectListOptions.strEmptyMessage = NKCUtilString.GET_STRING_REARM_PROCESS_UNIT_SELECT_LIST_TITLE;
			unitSelectListOptions.dOnSlotSetData = null;
			unitSelectListOptions.setUnitFilterCategory = new HashSet<NKCUnitSortSystem.eFilterCategory>
			{
				NKCUnitSortSystem.eFilterCategory.Level,
				NKCUnitSortSystem.eFilterCategory.Locked,
				NKCUnitSortSystem.eFilterCategory.Decked
			};
			unitSelectListOptions.setUnitSortCategory = new HashSet<NKCUnitSortSystem.eSortCategory>
			{
				NKCUnitSortSystem.eSortCategory.Level
			};
			unitSelectListOptions.m_bHideUnitCount = true;
			unitSelectListOptions.m_bUseFavorite = true;
			this.UnitSelectList.Open(unitSelectListOptions, new NKCUIUnitSelectList.OnUnitSelectCommand(this.OnConsumeUnitSelected), null, null, null, null);
		}

		// Token: 0x0600517B RID: 20859 RVA: 0x0018C0B2 File Offset: 0x0018A2B2
		public void OnConsumeUnitSelected(List<long> selectedList)
		{
			if (this.UnitSelectList.IsOpen)
			{
				this.UnitSelectList.Close();
			}
			if (selectedList.Count <= 0)
			{
				return;
			}
			this.m_iCurRearmResourceUID = selectedList[0];
			this.UpdateUI();
		}

		// Token: 0x0600517C RID: 20860 RVA: 0x0018C0E9 File Offset: 0x0018A2E9
		public void OnInventoryChange()
		{
			this.UpdateCostSlotUI();
			this.UpdateButtonUI();
		}

		// Token: 0x040041D4 RID: 16852
		public NKCUICharInfoSummary m_CharInfoSummary;

		// Token: 0x040041D5 RID: 16853
		public NKCUICharacterView m_charView;

		// Token: 0x040041D6 RID: 16854
		[Header("재무장 유닛 선택 리스트")]
		public NKCUIRearmamentProcessInfoSummary m_RearmSummaryInfo;

		// Token: 0x040041D7 RID: 16855
		public GameObject m_objRearmList;

		// Token: 0x040041D8 RID: 16856
		public LoopVerticalScrollRect m_LoopScroll;

		// Token: 0x040041D9 RID: 16857
		public NKCUIComStateButton m_UnitSelectBtn;

		// Token: 0x040041DA RID: 16858
		public NKCUIComMiscSortOptions m_SortOptions;

		// Token: 0x040041DB RID: 16859
		[Header("정렬/필터 통합UI")]
		public NKCUIComUnitSortOptions m_SortUI;

		// Token: 0x040041DC RID: 16860
		[Space]
		[Space]
		[Header("재무장 유닛 진행")]
		public NKCUIRearmamentProcessInfoSummary m_RearmSummarySkill;

		// Token: 0x040041DD RID: 16861
		public GameObject m_objRearmProcess;

		// Token: 0x040041DE RID: 16862
		public NKCUIComStateButton m_RearmUnitInfo;

		// Token: 0x040041DF RID: 16863
		public NKCUIComStateButton m_RearmBtn;

		// Token: 0x040041E0 RID: 16864
		public Image m_imgRearmBtn;

		// Token: 0x040041E1 RID: 16865
		public Text m_lbRearmBtn;

		// Token: 0x040041E2 RID: 16866
		[Space]
		public NKCUIItemCostSlot m_RequestLvSlot;

		// Token: 0x040041E3 RID: 16867
		public List<NKCUIItemCostSlot> m_lstRequestItemSlot;

		// Token: 0x040041E4 RID: 16868
		[Space]
		public NKCUIComStateButton m_csbtnSelectResourceUnit;

		// Token: 0x040041E5 RID: 16869
		public GameObject m_TargetEmptyObj;

		// Token: 0x040041E6 RID: 16870
		public NKCUIUnitSelectListSlot m_RearmTargetUnitSlot;

		// Token: 0x040041E7 RID: 16871
		public GameObject m_TargetEmptyText;

		// Token: 0x040041E8 RID: 16872
		public GameObject m_TargetSelectedText;

		// Token: 0x040041E9 RID: 16873
		private NKCUIRearmamentProcess.ChangeState dOk;

		// Token: 0x040041EA RID: 16874
		private int m_iCurSelectedUnitID;

		// Token: 0x040041EB RID: 16875
		private long m_iCurRearmResourceUID;

		// Token: 0x040041EC RID: 16876
		private NKCUIRearmament.REARM_TYPE m_curUIType;

		// Token: 0x040041ED RID: 16877
		private readonly HashSet<NKCUnitSortSystem.eFilterCategory> eRearmFilterCategories = new HashSet<NKCUnitSortSystem.eFilterCategory>
		{
			NKCUnitSortSystem.eFilterCategory.UnitType,
			NKCUnitSortSystem.eFilterCategory.UnitRole,
			NKCUnitSortSystem.eFilterCategory.UnitMoveType,
			NKCUnitSortSystem.eFilterCategory.UnitTargetType,
			NKCUnitSortSystem.eFilterCategory.Rarity,
			NKCUnitSortSystem.eFilterCategory.Cost
		};

		// Token: 0x040041EE RID: 16878
		private readonly HashSet<NKCUnitSortSystem.eSortCategory> eRearmSortCategories = new HashSet<NKCUnitSortSystem.eSortCategory>
		{
			NKCUnitSortSystem.eSortCategory.IDX,
			NKCUnitSortSystem.eSortCategory.Rarity,
			NKCUnitSortSystem.eSortCategory.UnitSummonCost
		};

		// Token: 0x040041EF RID: 16879
		private static readonly List<NKCUnitSortSystem.eSortOption> eRearmSortLists = new List<NKCUnitSortSystem.eSortOption>
		{
			NKCUnitSortSystem.eSortOption.IDX_First,
			NKCUnitSortSystem.eSortOption.Rarity_High,
			NKCUnitSortSystem.eSortOption.Unit_SummonCost_High
		};

		// Token: 0x040041F0 RID: 16880
		private NKCUIUnitSelectList.UnitSelectListOptions m_currentOption;

		// Token: 0x040041F1 RID: 16881
		private NKCUnitSortSystem m_SortSystem;

		// Token: 0x040041F2 RID: 16882
		public NKCUIUnitSelectListSlot m_pfbUnitSelectSlot;

		// Token: 0x040041F3 RID: 16883
		private List<NKMUnitRearmamentTemplet> m_lstRearm = new List<NKMUnitRearmamentTemplet>();

		// Token: 0x040041F4 RID: 16884
		private List<NKCUIUnitSelectListSlot> m_lstVisbleSlots = new List<NKCUIUnitSelectListSlot>();

		// Token: 0x040041F5 RID: 16885
		public NKCUIRearmamentSubUISelectList m_RearmSelectSubUI;

		// Token: 0x040041F6 RID: 16886
		private NKCUIUnitSelectList m_UIUnitSelectList;

		// Token: 0x020014C1 RID: 5313
		// (Invoke) Token: 0x0600A9CE RID: 43470
		public delegate void ChangeState(NKCUIRearmament.REARM_TYPE newType);
	}
}
