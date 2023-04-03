using System;
using System.Collections.Generic;
using ClientPacket.Office;
using NKC.UI.Component.Office;
using NKM;
using NKM.Templet;
using NKM.Templet.Office;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Office
{
	// Token: 0x02000AFA RID: 2810
	public class NKCUIPopupOfficeMemberEdit : NKCUIBase
	{
		// Token: 0x170014EB RID: 5355
		// (get) Token: 0x06007EE9 RID: 32489 RVA: 0x002A931C File Offset: 0x002A751C
		public static NKCUIPopupOfficeMemberEdit Instance
		{
			get
			{
				if (NKCUIPopupOfficeMemberEdit.m_Instance == null)
				{
					NKCUIPopupOfficeMemberEdit.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPopupOfficeMemberEdit>("ab_ui_office", "AB_UI_POPUP_OFFICE_MEMBER_EDIT", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPopupOfficeMemberEdit.CleanupInstance)).GetInstance<NKCUIPopupOfficeMemberEdit>();
					NKCUIPopupOfficeMemberEdit.m_Instance.InitUI();
				}
				return NKCUIPopupOfficeMemberEdit.m_Instance;
			}
		}

		// Token: 0x170014EC RID: 5356
		// (get) Token: 0x06007EEA RID: 32490 RVA: 0x002A936B File Offset: 0x002A756B
		public static bool HasInstance
		{
			get
			{
				return NKCUIPopupOfficeMemberEdit.m_Instance != null;
			}
		}

		// Token: 0x170014ED RID: 5357
		// (get) Token: 0x06007EEB RID: 32491 RVA: 0x002A9378 File Offset: 0x002A7578
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIPopupOfficeMemberEdit.m_Instance != null && NKCUIPopupOfficeMemberEdit.m_Instance.IsOpen;
			}
		}

		// Token: 0x06007EEC RID: 32492 RVA: 0x002A9393 File Offset: 0x002A7593
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPopupOfficeMemberEdit.m_Instance != null && NKCUIPopupOfficeMemberEdit.m_Instance.IsOpen)
			{
				NKCUIPopupOfficeMemberEdit.m_Instance.Close();
			}
		}

		// Token: 0x06007EED RID: 32493 RVA: 0x002A93B8 File Offset: 0x002A75B8
		private static void CleanupInstance()
		{
			NKCUIPopupOfficeMemberEdit.m_Instance.Release();
			NKCUIPopupOfficeMemberEdit.m_Instance = null;
		}

		// Token: 0x170014EE RID: 5358
		// (get) Token: 0x06007EEE RID: 32494 RVA: 0x002A93CA File Offset: 0x002A75CA
		public override string MenuName
		{
			get
			{
				return "사원 배치";
			}
		}

		// Token: 0x170014EF RID: 5359
		// (get) Token: 0x06007EEF RID: 32495 RVA: 0x002A93D1 File Offset: 0x002A75D1
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170014F0 RID: 5360
		// (get) Token: 0x06007EF0 RID: 32496 RVA: 0x002A93D4 File Offset: 0x002A75D4
		public float PopupWidth
		{
			get
			{
				return this.m_fPopupWidth;
			}
		}

		// Token: 0x06007EF1 RID: 32497 RVA: 0x002A93DC File Offset: 0x002A75DC
		public void InitUI()
		{
			if (this.m_loopScrollRect != null)
			{
				this.m_loopScrollRect.dOnGetObject += this.GetMemberSlot;
				this.m_loopScrollRect.dOnReturnObject += this.ReturnMemberSlot;
				this.m_loopScrollRect.dOnProvideData += this.ProvideMemberData;
				GridLayoutGroup component = this.m_loopScrollRect.content.GetComponent<GridLayoutGroup>();
				if (component != null)
				{
					this.m_loopScrollRect.ContentConstraintCount = component.constraintCount;
				}
				this.m_loopScrollRect.PrepareCells(0);
			}
			this.m_iMaxRoomNameLength = NKMCommonConst.Office.OfficeNamingLimit;
			NKCUtil.SetButtonClickDelegate(this.m_csbtnConfirm, new UnityAction(this.OnBtnConfirm));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnRoomNameChange, new UnityAction(this.OnBtnNameChange));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnAutoSelect, new UnityAction(this.OnBtnAutoSelect));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnDeselectAll, new UnityAction(this.OnBtnDeselectAll));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnFilter, new UnityAction(this.OnBtnFilter));
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglFavorite, new UnityAction<bool>(this.OnTglFavorite));
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglTabUnit, new UnityAction<bool>(this.OnTglUnit));
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglTabTrophy, new UnityAction<bool>(this.OnTglTrophy));
			this.m_inputRoomNameInput.onValueChanged.RemoveAllListeners();
			this.m_inputRoomNameInput.onValueChanged.AddListener(new UnityAction<string>(this.OnInputNameChanged));
			this.m_inputRoomNameInput.onValidateInput = new InputField.OnValidateInput(NKCFilterManager.FilterEmojiInput);
			this.m_inputRoomNameInput.onEndEdit.RemoveAllListeners();
			this.m_inputRoomNameInput.onEndEdit.AddListener(new UnityAction<string>(this.OnInputRoomNameEnd));
			if (this.m_popupContent == null)
			{
				Transform transform = base.transform.Find("Contents");
				this.m_popupContent = ((transform != null) ? transform.GetComponent<RectTransform>() : null);
			}
			if (this.m_animator == null)
			{
				this.m_animator = base.GetComponent<Animator>();
			}
			this.m_fPopupWidth = -1f;
			if (this.m_popupContent != null && NKCUIManager.FrontCanvas != null)
			{
				this.m_fPopupWidth = this.m_popupContent.rect.width * NKCUIManager.FrontCanvas.scaleFactor;
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007EF2 RID: 32498 RVA: 0x002A964C File Offset: 0x002A784C
		public override void OnBackButton()
		{
			if (this.IsSameUnitAssined())
			{
				base.Close();
				return;
			}
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_OFFICE_ASSIGN_CANCEL, new NKCPopupOKCancel.OnButton(this.OnBtnConfirm), new NKCPopupOKCancel.OnButton(base.Close), NKCUtilString.GET_STRING_SAVE, NKCStringTable.GetString("SI_PF_COMMON_CLOSE", false), false);
		}

		// Token: 0x06007EF3 RID: 32499 RVA: 0x002A96A0 File Offset: 0x002A78A0
		public void Open(int roomId, NKCUIPopupOfficeMemberEdit.OnCloseMemberEdit onCloseMemberEdit = null)
		{
			this.m_iRoomId = roomId;
			this.m_strRoomName = "";
			NKCUIOfficeMapFront instance = NKCUIOfficeMapFront.GetInstance();
			NKMOfficeRoom nkmofficeRoom = (instance != null) ? instance.OfficeData.GetOfficeRoom(roomId) : null;
			if (nkmofficeRoom != null)
			{
				this.m_strRoomName = nkmofficeRoom.name;
			}
			this.m_inputRoomNameInput.text = this.m_strRoomName;
			this.m_dOnCloseMemberEdit = onCloseMemberEdit;
			NKMOfficeRoomTemplet nkmofficeRoomTemplet = NKMOfficeRoomTemplet.Find(roomId);
			if (nkmofficeRoomTemplet != null)
			{
				this.m_iMaxUnitCount = nkmofficeRoomTemplet.UnitLimitCount;
			}
			string sectionName = NKCUIOfficeMapFront.GetSectionName(nkmofficeRoomTemplet.SectionTemplet);
			NKCUtil.SetLabelText(this.m_lbFloorName, sectionName);
			NKCUtil.SetLabelText(this.m_inputRoomNameInput.placeholder.GetComponent<Text>(), NKCUIOfficeMapFront.GetDefaultRoomName(roomId));
			this.SetRoomMemberList(roomId);
			this.SetRoomMemberCount(this.m_unitAssignList);
			this.m_unitListOption.eDeckType = NKM_DECK_TYPE.NDT_NONE;
			this.m_unitListOption.bHideDeckedUnit = false;
			this.m_unitListOption.bDescending = true;
			this.m_unitListOption.bPushBackUnselectable = true;
			this.m_unitListOption.lstSortOption = new List<NKCUnitSortSystem.eSortOption>();
			this.m_unitListOption.lstSortOption = NKCUnitSortSystem.AddDefaultSortOptions(this.m_unitListOption.lstSortOption, NKM_UNIT_TYPE.NUT_NORMAL, false);
			this.m_unitListOption.lstSortOption.Insert(0, NKCUnitSortSystem.eSortOption.CustomDescend1);
			this.m_unitListOption.lstDefaultSortOption = null;
			this.m_unitListOption.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
			this.m_unitListOption.lstCustomSortFunc = new Dictionary<NKCUnitSortSystem.eSortCategory, KeyValuePair<string, NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc>>();
			this.m_unitListOption.lstCustomSortFunc.Add(NKCUnitSortSystem.eSortCategory.Custom1, new KeyValuePair<string, NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc>(NKCUtilString.GET_STRING_OFFICE_ROOM_IN, new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.SortUnitByRoomInOut)));
			this.m_unitListOption.setOnlyIncludeFilterOption = null;
			this.m_unitListOption.PreemptiveSortFunc = null;
			this.m_unitListOption.AdditionalExcludeFilterFunc = new NKCUnitSortSystem.UnitListOptions.CustomFilterFunc(this.IsUnitStyleType);
			this.m_unitListOption.bExcludeLockedUnit = false;
			this.m_unitListOption.bExcludeDeckedUnit = false;
			this.m_unitListOption.setExcludeUnitUID = null;
			this.m_unitListOption.setExcludeUnitID = null;
			this.m_unitListOption.setOnlyIncludeUnitID = null;
			this.m_unitListOption.setDuplicateUnitID = null;
			this.m_unitListOption.bIncludeUndeckableUnit = true;
			this.m_unitListOption.bIncludeSeizure = false;
			this.m_unitListOption.bUseDeckedState = false;
			this.m_unitListOption.bUseLockedState = false;
			this.m_unitListOption.bUseLobbyState = false;
			this.m_unitListOption.bIgnoreCityState = false;
			this.m_unitListOption.bIgnoreWorldMapLeader = false;
			this.m_unitListOption.AdditionalUnitStateFunc = null;
			this.m_unitListOption.bIgnoreMissionState = false;
			this.SelectUnitListType(NKCUIPopupOfficeMemberEdit.UnitListType.Normal);
			NKCUtil.SetGameobjectActive(this.m_tglTabTrophy, this.HasUsableTrophy());
			if (this.m_tglFavorite != null)
			{
				this.m_tglFavorite.Select(false, true, false);
			}
			base.gameObject.SetActive(true);
			this.RefreshScrollRect();
			if (base.IsOpen)
			{
				this.m_animator.Play("AB_UI_POPUP_OFFICE_MEMBER_EDIT_INTRO", 0, 0f);
				return;
			}
			NKCUIComOfficeEnvScore envScore = this.m_EnvScore;
			if (envScore != null)
			{
				envScore.UpdateEnvScore(nkmofficeRoom);
			}
			base.UIOpened(true);
		}

		// Token: 0x06007EF4 RID: 32500 RVA: 0x002A9980 File Offset: 0x002A7B80
		public void SortSpecifitUnitFirst(int unitId)
		{
			this.m_iTutorialUnitId = unitId;
			this.m_unitListOption.lstCustomSortFunc[NKCUnitSortSystem.eSortCategory.Custom1] = new KeyValuePair<string, NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc>(NKCUtilString.GET_STRING_OFFICE_ROOM_IN, new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.SortSpecificUnitIdFirst));
			this.m_unitSortSystem.SortList(this.m_unitSortSystem.lstSortOption, true);
			this.RefreshScrollRect();
		}

		// Token: 0x06007EF5 RID: 32501 RVA: 0x002A99D9 File Offset: 0x002A7BD9
		public override void CloseInternal()
		{
			this.m_unitAssignList = null;
			this.m_unitSortSystem = null;
			this.m_strRoomName = null;
			if (this.m_dOnCloseMemberEdit != null)
			{
				this.m_dOnCloseMemberEdit();
			}
			this.m_iTutorialUnitId = 0;
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007EF6 RID: 32502 RVA: 0x002A9A16 File Offset: 0x002A7C16
		public void Release()
		{
		}

		// Token: 0x06007EF7 RID: 32503 RVA: 0x002A9A18 File Offset: 0x002A7C18
		public void UpdateRoomName(string roomName)
		{
			this.m_strRoomName = roomName;
			this.m_inputRoomNameInput.text = roomName;
		}

		// Token: 0x06007EF8 RID: 32504 RVA: 0x002A9A30 File Offset: 0x002A7C30
		public RectTransform GetRectTransformUnitSlot(int unitId)
		{
			NKCUIOfficeMemberEditSlot[] componentsInChildren = this.m_loopScrollRect.content.GetComponentsInChildren<NKCUIOfficeMemberEditSlot>();
			int num = componentsInChildren.Length;
			for (int i = 0; i < num; i++)
			{
				if (componentsInChildren[i].UnitId == unitId)
				{
					return componentsInChildren[i].GetComponent<RectTransform>();
				}
			}
			return null;
		}

		// Token: 0x06007EF9 RID: 32505 RVA: 0x002A9A74 File Offset: 0x002A7C74
		private bool IsUnitStyleType(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				return false;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID);
			return unitTempletBase != null && unitTempletBase.IsUnitStyleType();
		}

		// Token: 0x06007EFA RID: 32506 RVA: 0x002A9A9D File Offset: 0x002A7C9D
		private bool IsUnitAssignedInOtherRoom(NKMUnitData unitData)
		{
			return unitData != null && (unitData.OfficeRoomId > 0 && this.m_iRoomId != unitData.OfficeRoomId);
		}

		// Token: 0x06007EFB RID: 32507 RVA: 0x002A9AC0 File Offset: 0x002A7CC0
		private int SortUnitByRoomInOut(NKMUnitData e1, NKMUnitData e2)
		{
			bool flag = e1.OfficeRoomId == this.m_iRoomId;
			bool flag2 = e2.OfficeRoomId == this.m_iRoomId;
			if (flag && !flag2)
			{
				return -1;
			}
			if (!flag && flag2)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06007EFC RID: 32508 RVA: 0x002A9B00 File Offset: 0x002A7D00
		private int SortSpecificUnitIdFirst(NKMUnitData e1, NKMUnitData e2)
		{
			bool flag = e1.m_UnitID == this.m_iTutorialUnitId;
			bool flag2 = e2.m_UnitID == this.m_iTutorialUnitId;
			if (flag && !flag2)
			{
				return -1;
			}
			if (!flag && flag2)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06007EFD RID: 32509 RVA: 0x002A9B40 File Offset: 0x002A7D40
		private void SetRoomMemberCount(List<long> unitAssignedList)
		{
			int num = 0;
			if (unitAssignedList != null)
			{
				num = unitAssignedList.Count;
			}
			NKCUtil.SetLabelText(this.m_lbMemberCount, string.Format("{0}/{1}", num, this.m_iMaxUnitCount));
		}

		// Token: 0x06007EFE RID: 32510 RVA: 0x002A9B80 File Offset: 0x002A7D80
		private void SetRoomMemberList(int roomId)
		{
			this.m_unitAssignList = new List<long>();
			NKCUIOfficeMapFront instance = NKCUIOfficeMapFront.GetInstance();
			NKMOfficeRoom nkmofficeRoom = (instance != null) ? instance.OfficeData.GetOfficeRoom(roomId) : null;
			if (nkmofficeRoom != null)
			{
				nkmofficeRoom.unitUids.ForEach(delegate(long e)
				{
					this.m_unitAssignList.Add(e);
				});
			}
		}

		// Token: 0x06007EFF RID: 32511 RVA: 0x002A9BCA File Offset: 0x002A7DCA
		private RectTransform GetMemberSlot(int index)
		{
			NKCUIOfficeMemberEditSlot newInstance = NKCUIOfficeMemberEditSlot.GetNewInstance(null, false);
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x06007F00 RID: 32512 RVA: 0x002A9BE0 File Offset: 0x002A7DE0
		private void ReturnMemberSlot(Transform tr)
		{
			NKCUIOfficeMemberEditSlot component = tr.GetComponent<NKCUIOfficeMemberEditSlot>();
			tr.SetParent(null);
			if (component != null)
			{
				component.DestoryInstance();
				return;
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x06007F01 RID: 32513 RVA: 0x002A9C18 File Offset: 0x002A7E18
		private void ProvideMemberData(Transform tr, int index)
		{
			NKCUIOfficeMemberEditSlot component = tr.GetComponent<NKCUIOfficeMemberEditSlot>();
			if (component == null)
			{
				return;
			}
			if (this.m_unitSortSystem == null)
			{
				return;
			}
			if (this.m_unitSortSystem.SortedUnitList.Count <= index)
			{
				return;
			}
			NKMUnitData unitData = this.m_unitSortSystem.SortedUnitList[index];
			component.SetData(this.m_unitAssignList, unitData, this.m_iRoomId, new NKCUIOfficeMemberEditSlot.SelectSlot(this.OnSelectUnit));
		}

		// Token: 0x06007F02 RID: 32514 RVA: 0x002A9C84 File Offset: 0x002A7E84
		private void RefreshScrollRect()
		{
			this.m_loopScrollRect.TotalCount = this.m_unitSortSystem.SortedUnitList.Count;
			this.m_loopScrollRect.StopMovement();
			this.m_loopScrollRect.SetIndexPosition(0);
		}

		// Token: 0x06007F03 RID: 32515 RVA: 0x002A9CB8 File Offset: 0x002A7EB8
		private void AssignUnit(long unitUId)
		{
			if (this.m_unitAssignList.Count < this.m_iMaxUnitCount)
			{
				this.m_unitAssignList.Add(unitUId);
				this.SetRoomMemberCount(this.m_unitAssignList);
				this.m_loopScrollRect.RefreshCells(false);
			}
		}

		// Token: 0x06007F04 RID: 32516 RVA: 0x002A9CF4 File Offset: 0x002A7EF4
		private bool IsSameUnitAssined()
		{
			if (this.m_unitAssignList == null)
			{
				return true;
			}
			NKCUIOfficeMapFront instance = NKCUIOfficeMapFront.GetInstance();
			NKMOfficeRoom nkmofficeRoom = (instance != null) ? instance.OfficeData.GetOfficeRoom(this.m_iRoomId) : null;
			if (nkmofficeRoom == null)
			{
				return true;
			}
			int count = this.m_unitAssignList.Count;
			bool result = true;
			if (count != nkmofficeRoom.unitUids.Count)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < count; i++)
				{
					if (!nkmofficeRoom.unitUids.Contains(this.m_unitAssignList[i]))
					{
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x06007F05 RID: 32517 RVA: 0x002A9D75 File Offset: 0x002A7F75
		private void SendUnitAssignPacket()
		{
			if (this.m_unitAssignList == null)
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_OFFICE_ROOM_SET_UNIT_REQ(this.m_iRoomId, this.m_unitAssignList);
		}

		// Token: 0x06007F06 RID: 32518 RVA: 0x002A9D94 File Offset: 0x002A7F94
		private void OnSelectUnit(long unitUId)
		{
			if (NKMOfficeRoomTemplet.Find(this.m_iRoomId) == null)
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKMUnitData nkmunitData = (nkmuserData != null) ? nkmuserData.m_ArmyData.GetUnitOrTrophyFromUID(unitUId) : null;
			if (nkmunitData == null)
			{
				return;
			}
			if (this.m_unitAssignList == null)
			{
				return;
			}
			if (this.m_unitAssignList.Contains(unitUId))
			{
				this.m_unitAssignList.Remove(unitUId);
				this.SetRoomMemberCount(this.m_unitAssignList);
				this.m_loopScrollRect.RefreshCells(false);
				return;
			}
			if (this.m_unitAssignList.Count >= this.m_iMaxUnitCount)
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_OFFICE_FULL_ASSIGNED, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return;
			}
			if (nkmunitData.OfficeRoomId > 0 && nkmunitData.OfficeRoomId != this.m_iRoomId)
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_OFFICE_ALREADY_ASSIGNED_UNIT, delegate()
				{
					this.AssignUnit(unitUId);
				}, null, false);
				return;
			}
			this.AssignUnit(unitUId);
		}

		// Token: 0x06007F07 RID: 32519 RVA: 0x002A9EA4 File Offset: 0x002A80A4
		private void OnInputNameChanged(string _string)
		{
			this.m_inputRoomNameInput.text = NKCFilterManager.CheckBadChat(this.m_inputRoomNameInput.text);
			if (this.m_inputRoomNameInput.text.Length >= this.m_iMaxRoomNameLength)
			{
				this.m_inputRoomNameInput.text = this.m_inputRoomNameInput.text.Substring(0, this.m_iMaxRoomNameLength);
			}
		}

		// Token: 0x06007F08 RID: 32520 RVA: 0x002A9F08 File Offset: 0x002A8108
		private void OnInputRoomNameEnd(string _string)
		{
			this.m_inputRoomNameInput.text = NKCFilterManager.CheckBadChat(this.m_inputRoomNameInput.text);
			if (this.m_inputRoomNameInput.text == this.m_strRoomName)
			{
				return;
			}
			if (this.m_inputRoomNameInput.text.Length >= this.m_iMaxRoomNameLength)
			{
				string roomName = this.m_inputRoomNameInput.text.Substring(0, this.m_iMaxRoomNameLength);
				NKCPacketSender.Send_NKMPacket_OFFICE_SET_ROOM_NAME_REQ(this.m_iRoomId, roomName);
				return;
			}
			NKCPacketSender.Send_NKMPacket_OFFICE_SET_ROOM_NAME_REQ(this.m_iRoomId, this.m_inputRoomNameInput.text);
		}

		// Token: 0x06007F09 RID: 32521 RVA: 0x002A9F9C File Offset: 0x002A819C
		private void OnBtnNameChange()
		{
			this.m_inputRoomNameInput.Select();
			this.m_inputRoomNameInput.ActivateInputField();
		}

		// Token: 0x06007F0A RID: 32522 RVA: 0x002A9FB4 File Offset: 0x002A81B4
		private void OnBtnDeselectAll()
		{
			this.m_unitAssignList.Clear();
			this.SetRoomMemberCount(this.m_unitAssignList);
			this.m_loopScrollRect.RefreshCells(false);
		}

		// Token: 0x06007F0B RID: 32523 RVA: 0x002A9FDC File Offset: 0x002A81DC
		private void OnBtnAutoSelect()
		{
			List<NKMUnitData> list = new List<NKMUnitData>();
			int count = this.m_unitSortSystem.SortedUnitList.Count;
			for (int i = 0; i < count; i++)
			{
				if (!this.IsUnitAssignedInOtherRoom(this.m_unitSortSystem.SortedUnitList[i]))
				{
					list.Add(this.m_unitSortSystem.SortedUnitList[i]);
				}
			}
			list.Sort(delegate(NKMUnitData e1, NKMUnitData e2)
			{
				if (e1.loyalty >= 10000 || e2.loyalty >= 10000)
				{
					if (e1.loyalty >= 10000 && e2.loyalty < 10000)
					{
						return 1;
					}
					if (e1.loyalty < 10000 && e2.loyalty >= 10000)
					{
						return -1;
					}
				}
				if (e1.loyalty != e2.loyalty)
				{
					if (e1.loyalty < e2.loyalty)
					{
						return 1;
					}
					return -1;
				}
				else if (e1.m_UnitLevel != e2.m_UnitLevel)
				{
					if (e1.m_UnitLevel < e2.m_UnitLevel)
					{
						return 1;
					}
					return -1;
				}
				else if (e1.m_iUnitLevelEXP != e2.m_iUnitLevelEXP)
				{
					if (e1.m_iUnitLevelEXP < e2.m_iUnitLevelEXP)
					{
						return 1;
					}
					return -1;
				}
				else
				{
					NKM_UNIT_GRADE unitGrade = e1.GetUnitGrade();
					NKM_UNIT_GRADE unitGrade2 = e2.GetUnitGrade();
					if (unitGrade != unitGrade2)
					{
						if (unitGrade < unitGrade2)
						{
							return 1;
						}
						return -1;
					}
					else
					{
						int starGrade = e1.GetStarGrade();
						int starGrade2 = e2.GetStarGrade();
						if (starGrade != starGrade2)
						{
							if (starGrade < starGrade2)
							{
								return 1;
							}
							return -1;
						}
						else
						{
							NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(e1.m_UnitID);
							NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(e2.m_UnitID);
							if (unitTempletBase != null && unitTempletBase2 != null)
							{
								NKMUnitStatTemplet statTemplet = unitTempletBase.StatTemplet;
								NKMUnitStatTemplet statTemplet2 = unitTempletBase2.StatTemplet;
								if (statTemplet != null && statTemplet2 != null)
								{
									int respawnCost = statTemplet.GetRespawnCost(false, null);
									int respawnCost2 = statTemplet2.GetRespawnCost(false, null);
									if (respawnCost != respawnCost2)
									{
										if (respawnCost < respawnCost2)
										{
											return 1;
										}
										return -1;
									}
								}
							}
							if (e1.m_UnitID != e2.m_UnitID)
							{
								if (e1.m_UnitID > e2.m_UnitID)
								{
									return 1;
								}
								return -1;
							}
							else
							{
								if (e1.m_UnitUID == e2.m_UnitUID)
								{
									return 0;
								}
								if (e1.m_UnitUID < e2.m_UnitUID)
								{
									return 1;
								}
								return -1;
							}
						}
					}
				}
			});
			this.m_unitAssignList.Clear();
			int count2 = this.m_unitAssignList.Count;
			int num = 0;
			while (num < this.m_iMaxUnitCount && list.Count > num)
			{
				this.m_unitAssignList.Add(list[num].m_UnitUID);
				num++;
			}
			this.m_loopScrollRect.RefreshCells(false);
			this.SetRoomMemberCount(this.m_unitAssignList);
		}

		// Token: 0x06007F0C RID: 32524 RVA: 0x002AA0C4 File Offset: 0x002A82C4
		private void OnSelectFilter(HashSet<NKCUnitSortSystem.eFilterOption> setFilterOption)
		{
			if (this.m_unitSortSystem != null)
			{
				if (this.m_tglFavorite != null)
				{
					if (this.m_tglFavorite.IsSelected)
					{
						this.m_unitSortSystem.FilterSet.Add(NKCUnitSortSystem.eFilterOption.Favorite);
					}
					else
					{
						this.m_unitSortSystem.FilterSet.Remove(NKCUnitSortSystem.eFilterOption.Favorite);
					}
				}
				this.m_unitSortSystem.FilterSet = setFilterOption;
				this.RefreshScrollRect();
			}
		}

		// Token: 0x06007F0D RID: 32525 RVA: 0x002AA12E File Offset: 0x002A832E
		private void OnTglFavorite(bool value)
		{
			this.OnSelectFilter(this.m_unitSortSystem.FilterSet);
		}

		// Token: 0x06007F0E RID: 32526 RVA: 0x002AA144 File Offset: 0x002A8344
		private void OnBtnFilter()
		{
			HashSet<NKCUnitSortSystem.eFilterCategory> hashSet = new HashSet<NKCUnitSortSystem.eFilterCategory>();
			hashSet.Add(NKCUnitSortSystem.eFilterCategory.InRoom);
			hashSet.Add(NKCUnitSortSystem.eFilterCategory.Rarity);
			hashSet.Add(NKCUnitSortSystem.eFilterCategory.Locked);
			hashSet.Add(NKCUnitSortSystem.eFilterCategory.UnitType);
			hashSet.Add(NKCUnitSortSystem.eFilterCategory.UnitRole);
			hashSet.Add(NKCUnitSortSystem.eFilterCategory.UnitMoveType);
			hashSet.Add(NKCUnitSortSystem.eFilterCategory.UnitTargetType);
			hashSet.Add(NKCUnitSortSystem.eFilterCategory.Rarity);
			hashSet.Add(NKCUnitSortSystem.eFilterCategory.Cost);
			hashSet.Add(NKCUnitSortSystem.eFilterCategory.Locked);
			hashSet.Add(NKCUnitSortSystem.eFilterCategory.SpecialType);
			if (this.m_eUnitListType == NKCUIPopupOfficeMemberEdit.UnitListType.Normal)
			{
				hashSet.Add(NKCUnitSortSystem.eFilterCategory.Loyalty);
				hashSet.Add(NKCUnitSortSystem.eFilterCategory.LifeContract);
				hashSet.Add(NKCUnitSortSystem.eFilterCategory.Level);
				hashSet.Add(NKCUnitSortSystem.eFilterCategory.Decked);
			}
			NKCPopupFilterUnit.Instance.Open(hashSet, this.m_unitSortSystem.FilterSet, new NKCPopupFilterUnit.OnFilterSetChange(this.OnSelectFilter), NKCPopupFilterUnit.FILTER_TYPE.UNIT);
		}

		// Token: 0x06007F0F RID: 32527 RVA: 0x002AA201 File Offset: 0x002A8401
		private void OnBtnConfirm()
		{
			if (this.m_unitAssignList == null)
			{
				return;
			}
			if (!this.IsSameUnitAssined())
			{
				this.SendUnitAssignPacket();
			}
			else
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_OFFICE_ASSIGN_COMPLETE, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
			}
			base.Close();
		}

		// Token: 0x06007F10 RID: 32528 RVA: 0x002AA240 File Offset: 0x002A8440
		private void UpdateEnvScore(int roomID)
		{
			NKMOfficeRoom officeRoom = NKCScenManager.CurrentUserData().OfficeData.GetOfficeRoom(roomID);
			if (officeRoom == null)
			{
				NKCUtil.SetLabelText(this.m_lbEnvScore, "-");
				NKCUtil.SetLabelText(this.m_lbEnvInformation, "");
				return;
			}
			NKCUtil.SetLabelText(this.m_lbEnvScore, officeRoom.interiorScore.ToString());
			NKMOfficeGradeTemplet nkmofficeGradeTemplet = NKMOfficeGradeTemplet.Find(officeRoom.grade);
			if (nkmofficeGradeTemplet != null)
			{
				string @string = NKCStringTable.GetString("SI_DP_OFFICE_LOYALTY_SPEED", new object[]
				{
					nkmofficeGradeTemplet.ChargingTimeHour
				});
				NKCUtil.SetLabelText(this.m_lbEnvInformation, @string);
				return;
			}
			NKCUtil.SetLabelText(this.m_lbEnvInformation, "");
		}

		// Token: 0x06007F11 RID: 32529 RVA: 0x002AA2E3 File Offset: 0x002A84E3
		private void OnTglUnit(bool value)
		{
			if (value)
			{
				this.SelectUnitListType(NKCUIPopupOfficeMemberEdit.UnitListType.Normal);
			}
		}

		// Token: 0x06007F12 RID: 32530 RVA: 0x002AA2EF File Offset: 0x002A84EF
		private void OnTglTrophy(bool value)
		{
			if (value)
			{
				this.SelectUnitListType(NKCUIPopupOfficeMemberEdit.UnitListType.Trophy);
			}
		}

		// Token: 0x06007F13 RID: 32531 RVA: 0x002AA2FC File Offset: 0x002A84FC
		private void SelectUnitListType(NKCUIPopupOfficeMemberEdit.UnitListType type)
		{
			if (type != NKCUIPopupOfficeMemberEdit.UnitListType.Normal)
			{
				if (type == NKCUIPopupOfficeMemberEdit.UnitListType.Trophy)
				{
					this.m_unitListOption.AdditionalExcludeFilterFunc = new NKCUnitSortSystem.UnitListOptions.CustomFilterFunc(this.FilterUnusableTrophy);
					this.m_unitSortSystem = new NKCGenericUnitSort(NKCScenManager.CurrentUserData(), this.m_unitListOption, NKCScenManager.CurrentUserData().m_ArmyData.m_dicMyTrophy.Values);
					this.m_unitSortSystem.lstSortOption = new List<NKCUnitSortSystem.eSortOption>
					{
						NKCUnitSortSystem.eSortOption.Rarity_High,
						NKCUnitSortSystem.eSortOption.UID_First
					};
					this.m_tglTabTrophy.Select(true, true, false);
				}
			}
			else
			{
				this.m_unitListOption.AdditionalExcludeFilterFunc = null;
				this.m_unitSortSystem = new NKCUnitSort(NKCScenManager.CurrentUserData(), this.m_unitListOption);
				this.m_tglTabUnit.Select(true, true, false);
			}
			this.m_eUnitListType = type;
			this.RefreshScrollRect();
		}

		// Token: 0x06007F14 RID: 32532 RVA: 0x002AA3C4 File Offset: 0x002A85C4
		private bool FilterUnusableTrophy(NKMUnitData unitData)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData);
			return unitTempletBase != null && !string.IsNullOrEmpty(unitTempletBase.m_SpineSDName) && unitTempletBase.m_bDorm;
		}

		// Token: 0x06007F15 RID: 32533 RVA: 0x002AA3F4 File Offset: 0x002A85F4
		private bool HasUsableTrophy()
		{
			foreach (NKMUnitData unitData in NKCScenManager.CurrentUserData().m_ArmyData.m_dicMyTrophy.Values)
			{
				if (this.FilterUnusableTrophy(unitData))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04006B83 RID: 27523
		private const string ASSET_BUNDLE_NAME = "ab_ui_office";

		// Token: 0x04006B84 RID: 27524
		private const string UI_ASSET_NAME = "AB_UI_POPUP_OFFICE_MEMBER_EDIT";

		// Token: 0x04006B85 RID: 27525
		private static NKCUIPopupOfficeMemberEdit m_Instance;

		// Token: 0x04006B86 RID: 27526
		public RectTransform m_popupContent;

		// Token: 0x04006B87 RID: 27527
		public LoopScrollRect m_loopScrollRect;

		// Token: 0x04006B88 RID: 27528
		public Text m_lbFloorName;

		// Token: 0x04006B89 RID: 27529
		public InputField m_inputRoomNameInput;

		// Token: 0x04006B8A RID: 27530
		public Text m_lbMemberCount;

		// Token: 0x04006B8B RID: 27531
		public Animator m_animator;

		// Token: 0x04006B8C RID: 27532
		public NKCUIComOfficeEnvScore m_EnvScore;

		// Token: 0x04006B8D RID: 27533
		[Space]
		public NKCUIComToggle m_tglTabUnit;

		// Token: 0x04006B8E RID: 27534
		public NKCUIComToggle m_tglTabTrophy;

		// Token: 0x04006B8F RID: 27535
		[Space]
		public NKCUIComStateButton m_csbtnRoomNameChange;

		// Token: 0x04006B90 RID: 27536
		public NKCUIComStateButton m_csbtnDeselectAll;

		// Token: 0x04006B91 RID: 27537
		public NKCUIComStateButton m_csbtnAutoSelect;

		// Token: 0x04006B92 RID: 27538
		public NKCUIComStateButton m_csbtnFilter;

		// Token: 0x04006B93 RID: 27539
		public NKCUIComToggle m_tglFavorite;

		// Token: 0x04006B94 RID: 27540
		public NKCUIComStateButton m_csbtnConfirm;

		// Token: 0x04006B95 RID: 27541
		[Header("환경점수")]
		public Text m_lbEnvScore;

		// Token: 0x04006B96 RID: 27542
		public Text m_lbEnvInformation;

		// Token: 0x04006B97 RID: 27543
		private NKCUnitSortSystem.UnitListOptions m_unitListOption;

		// Token: 0x04006B98 RID: 27544
		private NKCUnitSortSystem m_unitSortSystem;

		// Token: 0x04006B99 RID: 27545
		private NKCUIPopupOfficeMemberEdit.UnitListType m_eUnitListType;

		// Token: 0x04006B9A RID: 27546
		private int m_iMaxRoomNameLength = 8;

		// Token: 0x04006B9B RID: 27547
		private int m_iRoomId;

		// Token: 0x04006B9C RID: 27548
		private int m_iMaxUnitCount;

		// Token: 0x04006B9D RID: 27549
		private int m_iTutorialUnitId;

		// Token: 0x04006B9E RID: 27550
		private List<long> m_unitAssignList;

		// Token: 0x04006B9F RID: 27551
		private string m_strRoomName;

		// Token: 0x04006BA0 RID: 27552
		private float m_fPopupWidth;

		// Token: 0x04006BA1 RID: 27553
		private NKCUIPopupOfficeMemberEdit.OnCloseMemberEdit m_dOnCloseMemberEdit;

		// Token: 0x02001881 RID: 6273
		private enum UnitListType
		{
			// Token: 0x0400A91B RID: 43291
			Normal,
			// Token: 0x0400A91C RID: 43292
			Trophy
		}

		// Token: 0x02001882 RID: 6274
		// (Invoke) Token: 0x0600B611 RID: 46609
		public delegate void OnCloseMemberEdit();
	}
}
