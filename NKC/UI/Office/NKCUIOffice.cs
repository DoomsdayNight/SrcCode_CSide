using System;
using System.Collections;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Office;
using Cs.Protocol;
using NKC.Office;
using NKC.Office.Forge;
using NKC.Templet;
using NKC.UI.Component.Office;
using NKC.UI.NPC;
using NKM;
using NKM.Templet;
using NKM.Templet.Office;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Office
{
	// Token: 0x02000AEF RID: 2799
	public class NKCUIOffice : NKCUIBase
	{
		// Token: 0x06007DAA RID: 32170 RVA: 0x002A2242 File Offset: 0x002A0442
		public static NKCUIManager.LoadedUIData OpenNewInstanceAsync()
		{
			if (!NKCUIManager.IsValid(NKCUIOffice.s_LoadedUIData))
			{
				NKCUIOffice.s_LoadedUIData = NKCUIManager.OpenNewInstanceAsync<NKCUIOffice>("ab_ui_office", "AB_UI_OFFICE", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIOffice.CleanupInstance));
			}
			return NKCUIOffice.s_LoadedUIData;
		}

		// Token: 0x170014CE RID: 5326
		// (get) Token: 0x06007DAB RID: 32171 RVA: 0x002A2276 File Offset: 0x002A0476
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIOffice.s_LoadedUIData != null && NKCUIOffice.s_LoadedUIData.IsUIOpen;
			}
		}

		// Token: 0x170014CF RID: 5327
		// (get) Token: 0x06007DAC RID: 32172 RVA: 0x002A228B File Offset: 0x002A048B
		public static bool IsInstanceLoaded
		{
			get
			{
				return NKCUIOffice.s_LoadedUIData != null && NKCUIOffice.s_LoadedUIData.IsLoadComplete;
			}
		}

		// Token: 0x06007DAD RID: 32173 RVA: 0x002A22A0 File Offset: 0x002A04A0
		public static NKCUIOffice GetInstance()
		{
			if (NKCUIOffice.s_LoadedUIData != null && NKCUIOffice.s_LoadedUIData.IsLoadComplete)
			{
				return NKCUIOffice.s_LoadedUIData.GetInstance<NKCUIOffice>();
			}
			return null;
		}

		// Token: 0x06007DAE RID: 32174 RVA: 0x002A22C1 File Offset: 0x002A04C1
		public static void CleanupInstance()
		{
			NKCUIOffice.s_LoadedUIData = null;
		}

		// Token: 0x170014D0 RID: 5328
		// (get) Token: 0x06007DAF RID: 32175 RVA: 0x002A22C9 File Offset: 0x002A04C9
		public NKCUIOfficeFacilityButtons OfficeFacilityInterfaces
		{
			get
			{
				return this.m_UIOfficeFacilityButtons;
			}
		}

		// Token: 0x170014D1 RID: 5329
		// (get) Token: 0x06007DB0 RID: 32176 RVA: 0x002A22D1 File Offset: 0x002A04D1
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170014D2 RID: 5330
		// (get) Token: 0x06007DB1 RID: 32177 RVA: 0x002A22D4 File Offset: 0x002A04D4
		public override string MenuName
		{
			get
			{
				return "사옥";
			}
		}

		// Token: 0x170014D3 RID: 5331
		// (get) Token: 0x06007DB2 RID: 32178 RVA: 0x002A22DB File Offset: 0x002A04DB
		public int RoomID
		{
			get
			{
				if (this.m_currentRoom == null)
				{
					return 0;
				}
				return this.m_currentRoom.ID;
			}
		}

		// Token: 0x170014D4 RID: 5332
		// (get) Token: 0x06007DB3 RID: 32179 RVA: 0x002A22F2 File Offset: 0x002A04F2
		private long CurrentVisitUID
		{
			get
			{
				if (this.m_currentRoom == null)
				{
					return 0L;
				}
				return this.m_currentRoom.m_OwnerUID;
			}
		}

		// Token: 0x170014D5 RID: 5333
		// (get) Token: 0x06007DB4 RID: 32180 RVA: 0x002A230A File Offset: 0x002A050A
		private bool IsVisiting
		{
			get
			{
				return this.CurrentVisitUID > 0L;
			}
		}

		// Token: 0x170014D6 RID: 5334
		// (get) Token: 0x06007DB5 RID: 32181 RVA: 0x002A2316 File Offset: 0x002A0516
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x06007DB6 RID: 32182 RVA: 0x002A2319 File Offset: 0x002A0519
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
			NKCSoundManager.PlayScenMusic(NKCScenManager.GetScenManager().GetNowScenID(), false);
			this.CleanUp();
		}

		// Token: 0x06007DB7 RID: 32183 RVA: 0x002A233D File Offset: 0x002A053D
		public override void OnCloseInstance()
		{
			this.CleanUp();
		}

		// Token: 0x06007DB8 RID: 32184 RVA: 0x002A2348 File Offset: 0x002A0548
		private void CleanUp()
		{
			if (this.m_OfficeBuilding != null)
			{
				this.m_OfficeBuilding.transform.SetParent(base.transform);
				this.m_OfficeBuilding.CleanUp();
			}
			if (this.m_NKCUIOfficeFacility != null)
			{
				this.m_NKCUIOfficeFacility.transform.SetParent(base.transform);
				this.m_NKCUIOfficeFacility.CleanUp();
				UnityEngine.Object.Destroy(this.m_NKCUIOfficeFacility.gameObject);
				this.m_NKCUIOfficeFacility = null;
			}
		}

		// Token: 0x06007DB9 RID: 32185 RVA: 0x002A23CA File Offset: 0x002A05CA
		public override void UnHide()
		{
			base.UnHide();
			this.PlayRoomMusic();
		}

		// Token: 0x06007DBA RID: 32186 RVA: 0x002A23D8 File Offset: 0x002A05D8
		public override void OnBackButton()
		{
			switch (this.m_eMode)
			{
			case NKCUIOffice.Mode.Edit:
				this.ChangeMode(NKCUIOffice.Mode.Normal, -1, -1L);
				return;
			case NKCUIOffice.Mode.EditAdd:
			case NKCUIOffice.Mode.EditMove:
				this.ChangeMode(NKCUIOffice.Mode.Edit, -1, -1L);
				return;
			case NKCUIOffice.Mode.Hide:
				this.HideUI(false);
				return;
			case NKCUIOffice.Mode.Preview:
				this.CancelPreview();
				return;
			}
			base.Close();
		}

		// Token: 0x06007DBB RID: 32187 RVA: 0x002A2440 File Offset: 0x002A0640
		public override bool OnHotkey(HotkeyEventType hotkey)
		{
			if (hotkey != HotkeyEventType.PrevTab)
			{
				if (hotkey != HotkeyEventType.NextTab)
				{
					if (hotkey == HotkeyEventType.ShowHotkey && (this.m_eMode == NKCUIOffice.Mode.Normal || this.m_eMode == NKCUIOffice.Mode.Visit))
					{
						NKCUIComHotkeyDisplay.OpenInstance(this.m_uiOfficeUpsideMenu.m_RoomInfoBg, HotkeyEventType.NextTab);
					}
				}
				else if (this.m_eMode == NKCUIOffice.Mode.Normal || this.m_eMode == NKCUIOffice.Mode.Visit)
				{
					this.m_uiOfficeUpsideMenu.OnBtnRightMove();
				}
			}
			else if (this.m_eMode == NKCUIOffice.Mode.Normal || this.m_eMode == NKCUIOffice.Mode.Visit)
			{
				this.m_uiOfficeUpsideMenu.OnBtnLeftMove();
			}
			return false;
		}

		// Token: 0x06007DBC RID: 32188 RVA: 0x002A24BC File Offset: 0x002A06BC
		public void Init()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnMinimap, new UnityAction(base.Close));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnShop, new UnityAction(this.OnBtnShop));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnEditMode, new UnityAction(this.OnBtnEditMode));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnWarehouse, new UnityAction(this.OnBtnWarehouse));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnDeployUnit, new UnityAction(this.OnBtnDiployUnit));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnCommunity, new UnityAction(this.OnBtnCommunity));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnParty, new UnityAction(this.OnBtnParty));
			if (this.m_csbtnParty != null)
			{
				this.m_csbtnParty.m_bGetCallbackWhileLocked = true;
			}
			NKCUtil.SetGameobjectActive(this.m_csbtnParty, NKMOpenTagManager.IsOpened("OFFICE_PARTY"));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnToMyOffice, new UnityAction(this.OnBtnToMyOffice));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnRandomVisit, new UnityAction(this.OnBtnRandomVisit));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnSendBizCard, new UnityAction(this.OnBtnSendBizCard));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnEditInfo, new UnityAction(this.OnBtnEditInfo));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnEditInvert, new UnityAction(this.OnBtnEditInvert));
			NKCUtil.SetHotkey(this.m_csbtnEditInvert, HotkeyEventType.NextTab, null, false);
			NKCUtil.SetButtonClickDelegate(this.m_csbtnEditStore, new UnityAction(this.OnBtnEditStore));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnEditStoreAll, new UnityAction(this.OnBtnEditStoreAll));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnEditSave, new UnityAction(this.OnBtnEditSave));
			NKCUtil.SetHotkey(this.m_csbtnEditSave, HotkeyEventType.Confirm, null, false);
			NKCUtil.SetButtonClickDelegate(this.m_csbtnEditWarehouse, new UnityAction(this.OnBtnWarehouse));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnEditClose, new UnityAction(this.OnBtnEditClose));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnEditCopyPreset, new UnityAction(this.OnBtnEditCopyPreset));
			NKCUtil.SetGameobjectActive(this.m_csbtnEditCopyPreset, NKCScenManager.CurrentUserData().IsSuperUser());
			NKCUtil.SetButtonClickDelegate(this.m_csbtnPresetList, new UnityAction(this.OnBtnPresetList));
			NKCUtil.SetGameobjectActive(this.m_csbtnPresetList, true);
			NKCUtil.SetButtonClickDelegate(this.m_csbtnPreviewOK, new UnityAction(this.TryApplyPreset));
			NKCUtil.SetHotkey(this.m_csbtnPreviewOK, HotkeyEventType.Confirm, null, false);
			NKCUtil.SetButtonClickDelegate(this.m_csbtnPreviewCancel, new UnityAction(this.OnBtnPreviewCancel));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnPreviewInteriorList, new UnityAction(this.OnPresetInteriorList));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnHideMenu, new UnityAction(this.OnBtnHideMenu));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnUnhideMenu, new UnityAction(this.OnBtnUnhideMenu));
			if (this.m_slotLastFuniture != null)
			{
				this.m_slotLastFuniture.Init();
				this.m_slotLastFuniture.SetHotkey(HotkeyEventType.NextTab);
			}
			NKCUtil.SetGameobjectActive(this.m_goLastFuniture, false);
			this.m_OfficeBuilding.Init(new NKCOfficeFuniture.OnClickFuniture(this.OnSelectFuniture));
			this.m_UIOfficeFacilityButtons.Init(new UnityAction(base.Close));
			NKCUIOfficeUpsideMenu uiOfficeUpsideMenu = this.m_uiOfficeUpsideMenu;
			if (uiOfficeUpsideMenu == null)
			{
				return;
			}
			uiOfficeUpsideMenu.Init();
		}

		// Token: 0x06007DBD RID: 32189 RVA: 0x002A27DA File Offset: 0x002A09DA
		public void Preload()
		{
		}

		// Token: 0x06007DBE RID: 32190 RVA: 0x002A27DC File Offset: 0x002A09DC
		private void CleanupRooms()
		{
			if (this.m_OfficeBuilding != null)
			{
				this.m_OfficeBuilding.CleanUp();
				this.m_OfficeBuilding.gameObject.SetActive(false);
			}
			if (this.m_NKCUIOfficeFacility != null)
			{
				this.m_NKCUIOfficeFacility.CleanUp();
				UnityEngine.Object.Destroy(this.m_NKCUIOfficeFacility.gameObject);
			}
		}

		// Token: 0x06007DBF RID: 32191 RVA: 0x002A283C File Offset: 0x002A0A3C
		public void Open(long uid, int roomID)
		{
			if (NKCScenManager.CurrentUserData().OfficeData.GetFriendRoom(uid, roomID) == null)
			{
				Debug.LogError("Office room not unlocked");
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString("SI_DP_OFFICE_FRIEND_CANNOT_VISIT", false), delegate()
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				}, "");
				return;
			}
			this.CleanupRooms();
			this.OpenFriendRoom(uid, roomID);
			base.UIOpened(true);
		}

		// Token: 0x06007DC0 RID: 32192 RVA: 0x002A28B8 File Offset: 0x002A0AB8
		private void OpenFriendRoom(long uid, int roomID)
		{
			NKMOfficeRoom friendRoom = NKCScenManager.CurrentUserData().OfficeData.GetFriendRoom(uid, roomID);
			NKMOfficeRoomTemplet nkmofficeRoomTemplet = NKMOfficeRoomTemplet.Find(roomID);
			if (nkmofficeRoomTemplet == null)
			{
				return;
			}
			if (this.m_OfficeBuilding == null)
			{
				Debug.LogError("Office building object not set");
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_GAME_LOAD_FAILED, delegate()
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				}, "");
				return;
			}
			this.m_OfficeBuilding.CleanUp();
			this.m_OfficeBuilding.transform.SetParent(NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIMidCanvas));
			this.m_OfficeBuilding.transform.position = new Vector3(0f, 0f, 1000f);
			this.m_OfficeBuilding.transform.localScale = Vector3.one;
			NKCUtil.SetGameobjectActive(this.m_OfficeBuilding, true);
			this.m_currentRoom = new NKCOfficeRoomData(friendRoom, uid);
			this.m_currentRoomTemplet = nkmofficeRoomTemplet;
			this.m_lstVisitor.Clear();
			this.m_OfficeBuilding.SetRoomData(this.m_currentRoom, this.m_lstVisitor);
			this.m_OfficeBuilding.SetCameraOffset(0f);
			this.m_OfficeBuilding.SetCamera();
			this.UpdateEnvScore();
			this.ChangeMode(NKCUIOffice.Mode.Visit, -1, -1L);
		}

		// Token: 0x06007DC1 RID: 32193 RVA: 0x002A29F0 File Offset: 0x002A0BF0
		public void Open(int roomID)
		{
			NKMOfficeRoomTemplet nkmofficeRoomTemplet = NKMOfficeRoomTemplet.Find(roomID);
			if (nkmofficeRoomTemplet == null)
			{
				return;
			}
			this.CleanupRooms();
			if (nkmofficeRoomTemplet.IsFacility)
			{
				this.OpenFacility(nkmofficeRoomTemplet);
			}
			else
			{
				this.OpenRoom(nkmofficeRoomTemplet);
			}
			base.UIOpened(true);
		}

		// Token: 0x06007DC2 RID: 32194 RVA: 0x002A2A2D File Offset: 0x002A0C2D
		public void MoveToRoom(int roomID)
		{
			base.StartCoroutine(this.MoveRoomProcess(roomID));
		}

		// Token: 0x06007DC3 RID: 32195 RVA: 0x002A2A3D File Offset: 0x002A0C3D
		private IEnumerator MoveRoomProcess(int roomID)
		{
			if (this.RoomID == roomID)
			{
				yield break;
			}
			NKMOfficeRoomTemplet roomTemplet = NKMOfficeRoomTemplet.Find(roomID);
			if (roomTemplet == null)
			{
				yield break;
			}
			NKCUIFadeInOut.FadeOut(0.1f, null, false, 3f);
			while (!NKCUIFadeInOut.IsFinshed())
			{
				yield return null;
			}
			this.CleanupRooms();
			if (roomTemplet.IsFacility)
			{
				this.OpenFacility(roomTemplet);
			}
			else if (this.IsVisiting)
			{
				this.OpenFriendRoom(this.CurrentVisitUID, roomID);
			}
			else
			{
				this.OpenRoom(roomTemplet);
			}
			NKCUIFadeInOut.FadeIn(0.1f, null, false);
			yield break;
		}

		// Token: 0x06007DC4 RID: 32196 RVA: 0x002A2A54 File Offset: 0x002A0C54
		private void OpenRoom(NKMOfficeRoomTemplet roomTemplet)
		{
			if (this.m_OfficeBuilding == null)
			{
				Debug.LogError("Office building object not set");
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_GAME_LOAD_FAILED, delegate()
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				}, "");
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (((nkmuserData != null) ? nkmuserData.OfficeData : null) == null)
			{
				Debug.LogError("Office data not set");
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_GAME_LOAD_FAILED, delegate()
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				}, "");
				return;
			}
			NKMOfficeRoom officeRoom = NKCScenManager.CurrentUserData().OfficeData.GetOfficeRoom(roomTemplet.ID);
			if (officeRoom == null)
			{
				Debug.LogError("Office room not unlocked");
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_OFFICE_NOT_OPEND_ROOM, delegate()
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				}, "");
				return;
			}
			this.m_OfficeBuilding.CleanUp();
			this.LoadNPCSpineIllust(roomTemplet.Type, this.m_rtNPCRoot);
			this.m_OfficeBuilding.transform.SetParent(NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIMidCanvas));
			this.m_OfficeBuilding.transform.position = new Vector3(0f, 0f, 1000f);
			this.m_OfficeBuilding.transform.localScale = Vector3.one;
			NKCUtil.SetGameobjectActive(this.m_OfficeBuilding, true);
			this.m_currentRoom = new NKCOfficeRoomData(officeRoom, 0L);
			this.m_currentRoomTemplet = roomTemplet;
			if (roomTemplet.ID == 1 && !this.IsVisiting)
			{
				this.m_lstVisitor = NKCScenManager.CurrentUserData().OfficeData.GetRandomVisitor(4);
			}
			else
			{
				this.m_lstVisitor.Clear();
			}
			this.m_OfficeBuilding.SetRoomData(this.m_currentRoom, this.m_lstVisitor);
			this.m_OfficeBuilding.SetCameraOffset(0f);
			this.m_OfficeBuilding.SetCamera();
			if (roomTemplet.ID == 1 && !this.IsVisiting)
			{
				this.m_OfficeBuilding.AddNPC("ab_unit_office_sd@UNIT_OFFICE_SD_NPC_LOBBY", "AB_UNIT_SD_SPINE_NKM_UNIT_OFFICE_KIM_HANA", "OFFICE_BT_IDLE", new Vector3(700f, 0f));
			}
			this.UpdateEnvScore();
			this.ChangeMode(NKCUIOffice.Mode.Normal, -1, -1L);
			NKCTutorialManager.TutorialRequired(TutorialPoint.OfficeRoom, true);
		}

		// Token: 0x06007DC5 RID: 32197 RVA: 0x002A2C88 File Offset: 0x002A0E88
		private void OpenFacility(NKMOfficeRoomTemplet roomTemplet)
		{
			if (this.m_NKCUIOfficeFacility != null)
			{
				this.m_NKCUIOfficeFacility.CleanUp();
				UnityEngine.Object.Destroy(this.m_NKCUIOfficeFacility.gameObject);
			}
			this.m_currentRoomTemplet = roomTemplet;
			this.m_NKCUIOfficeFacility = NKCOfficeFacility.GetInstance(roomTemplet);
			if (this.m_NKCUIOfficeFacility == null)
			{
				Debug.LogError(string.Format("Facility load failed! ID : {0}, resource : {1}", roomTemplet.ID, roomTemplet.FacilityPrefab));
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_OFFICE_ROOM_NOT_FOUND, delegate()
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				}, "");
				return;
			}
			this.m_NKCUIOfficeFacility.transform.SetParent(NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIMidCanvas));
			this.m_NKCUIOfficeFacility.transform.position = new Vector3(0f, 0f, 1000f);
			this.m_NKCUIOfficeFacility.transform.localScale = Vector3.one;
			this.m_NKCUIOfficeFacility.CalculateRoomSize();
			this.LoadNPCSpineIllust(roomTemplet.Type, this.m_rtNPCRoot);
			if (this.m_lstNPCIllust.Count > 0)
			{
				this.m_NKCUIOfficeFacility.SetCameraOffset(this.m_rtNPCRoot.GetWidth());
			}
			else
			{
				this.m_NKCUIOfficeFacility.SetCameraOffset(0f);
			}
			this.m_NKCUIOfficeFacility.SetCamera();
			this.ChangeMode(NKCUIOffice.Mode.Facility, -1, -1L);
			switch (roomTemplet.Type)
			{
			case NKMOfficeRoomTemplet.RoomType.Lab:
				NKCTutorialManager.TutorialRequired(TutorialPoint.OfficeLab, true);
				return;
			case NKMOfficeRoomTemplet.RoomType.Forge:
				NKCTutorialManager.TutorialRequired(TutorialPoint.OfficeFactory, true);
				return;
			case NKMOfficeRoomTemplet.RoomType.Hangar:
				NKCTutorialManager.TutorialRequired(TutorialPoint.OfficeHangar, true);
				return;
			case NKMOfficeRoomTemplet.RoomType.CEO:
				NKCTutorialManager.TutorialRequired(TutorialPoint.OfficeCEO, true);
				return;
			case NKMOfficeRoomTemplet.RoomType.Terrabrain:
				NKCTutorialManager.TutorialRequired(TutorialPoint.OfficeTerrabrain, true);
				return;
			default:
				return;
			}
		}

		// Token: 0x06007DC6 RID: 32198 RVA: 0x002A2E3C File Offset: 0x002A103C
		private void OnFunitureAddSelected(int id)
		{
			NKMOfficeInteriorTemplet nkmofficeInteriorTemplet = NKMItemMiscTemplet.FindInterior(id);
			if (nkmofficeInteriorTemplet == null)
			{
				return;
			}
			InteriorCategory interiorCategory = nkmofficeInteriorTemplet.InteriorCategory;
			if (interiorCategory != InteriorCategory.DECO)
			{
				if (interiorCategory == InteriorCategory.FURNITURE)
				{
					this.ChangeMode(NKCUIOffice.Mode.EditAdd, id, -1L);
					return;
				}
			}
			else
			{
				this.TryApplyDecoration(nkmofficeInteriorTemplet);
			}
		}

		// Token: 0x06007DC7 RID: 32199 RVA: 0x002A2E74 File Offset: 0x002A1074
		private void ChangeMode(NKCUIOffice.Mode mode, int ID = -1, long uid = -1L)
		{
			NKCUIOffice.Mode eMode = this.m_eMode;
			if (eMode != NKCUIOffice.Mode.EditAdd)
			{
				if (eMode == NKCUIOffice.Mode.EditMove)
				{
					if (this.m_OfficeBuilding.HasSelection)
					{
						this.m_OfficeBuilding.CancelMoveFuniture();
					}
				}
			}
			else if (this.m_OfficeBuilding.HasSelection)
			{
				this.m_OfficeBuilding.ClearSelection();
			}
			if (mode != NKCUIOffice.Mode.EditAdd)
			{
				if (mode == NKCUIOffice.Mode.EditMove)
				{
					NKM_ERROR_CODE nkm_ERROR_CODE = this.m_OfficeBuilding.MoveFunitureMode(uid);
					if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
					{
						NKCPopupOKCancel.OpenOKBox(nkm_ERROR_CODE, null, "");
						mode = NKCUIOffice.Mode.Normal;
					}
				}
			}
			else
			{
				NKM_ERROR_CODE nkm_ERROR_CODE2 = this.m_OfficeBuilding.AddFunitureMode(ID);
				if (nkm_ERROR_CODE2 != NKM_ERROR_CODE.NEC_OK)
				{
					NKCPopupOKCancel.OpenOKBox(nkm_ERROR_CODE2, null, "");
					mode = NKCUIOffice.Mode.Normal;
				}
			}
			this.m_eMode = mode;
			if (mode == NKCUIOffice.Mode.Facility && this.m_currentRoomTemplet != null)
			{
				this.m_UIOfficeFacilityButtons.SetMode(this.m_currentRoomTemplet.Type);
			}
			bool flag = false;
			switch (mode)
			{
			case NKCUIOffice.Mode.Edit:
			case NKCUIOffice.Mode.EditAdd:
			case NKCUIOffice.Mode.EditMove:
			{
				flag = true;
				this.UpdateEditButtons();
				NKCUIOfficeUpsideMenu uiOfficeUpsideMenu = this.m_uiOfficeUpsideMenu;
				if (uiOfficeUpsideMenu == null)
				{
					goto IL_156;
				}
				uiOfficeUpsideMenu.SetState(NKCUIOfficeUpsideMenu.MenuState.Decoration, this.m_currentRoomTemplet);
				goto IL_156;
			}
			case NKCUIOffice.Mode.Facility:
			{
				NKCUIOfficeUpsideMenu uiOfficeUpsideMenu2 = this.m_uiOfficeUpsideMenu;
				if (uiOfficeUpsideMenu2 == null)
				{
					goto IL_156;
				}
				uiOfficeUpsideMenu2.SetState(NKCUIOfficeUpsideMenu.MenuState.Facility, this.m_currentRoomTemplet);
				goto IL_156;
			}
			case NKCUIOffice.Mode.Visit:
			{
				this.UpdatePostState();
				NKCUIOfficeUpsideMenu uiOfficeUpsideMenu3 = this.m_uiOfficeUpsideMenu;
				if (uiOfficeUpsideMenu3 == null)
				{
					goto IL_156;
				}
				uiOfficeUpsideMenu3.SetState(NKCUIOfficeUpsideMenu.MenuState.Room, this.m_currentRoomTemplet);
				goto IL_156;
			}
			}
			this.CheckCommunityReddot();
			NKCUIOfficeUpsideMenu uiOfficeUpsideMenu4 = this.m_uiOfficeUpsideMenu;
			if (uiOfficeUpsideMenu4 != null)
			{
				uiOfficeUpsideMenu4.SetState(NKCUIOfficeUpsideMenu.MenuState.Room, this.m_currentRoomTemplet);
			}
			IL_156:
			if (this.m_OfficeBuilding != null)
			{
				this.m_OfficeBuilding.SetEnableUnitTouch(!flag);
				this.m_OfficeBuilding.SetEnableUnitExtraUI(mode == NKCUIOffice.Mode.Normal);
			}
			NKCUtil.SetGameobjectActive(this.m_uiOfficeUpsideMenu, mode != NKCUIOffice.Mode.Hide && mode != NKCUIOffice.Mode.Preview);
			NKCUtil.SetGameobjectActive(this.m_objHideMenu, mode == NKCUIOffice.Mode.Hide);
			NKCUtil.SetGameobjectActive(this.m_objNormalMode, mode == NKCUIOffice.Mode.Normal || mode == NKCUIOffice.Mode.Visit);
			NKCUtil.SetGameobjectActive(this.m_objPreviewMode, mode == NKCUIOffice.Mode.Preview);
			NKCUtil.SetGameobjectActive(this.m_objNormalButtons, mode == NKCUIOffice.Mode.Normal);
			NKCUtil.SetGameobjectActive(this.m_objVisitButtons, mode == NKCUIOffice.Mode.Visit);
			NKCUtil.SetGameobjectActive(this.m_csbtnParty, mode == NKCUIOffice.Mode.Normal && NKMOpenTagManager.IsOpened("OFFICE_PARTY"));
			if (this.m_csbtnParty != null && this.m_currentRoom != null)
			{
				this.m_csbtnParty.SetLock(this.m_currentRoom.m_lstUnitUID.Count == 0, false);
			}
			NKCUtil.SetGameobjectActive(this.m_UIOfficeFacilityButtons, mode == NKCUIOffice.Mode.Facility);
			NKCUtil.SetGameobjectActive(this.m_objEditMode, flag);
			this.UpdateEnvScore();
			base.UpdateUpsideMenu();
			this.PlayRoomMusic();
		}

		// Token: 0x06007DC8 RID: 32200 RVA: 0x002A30EC File Offset: 0x002A12EC
		private void HideUI(bool value)
		{
			if (value)
			{
				this.ChangeMode(NKCUIOffice.Mode.Hide, -1, -1L);
				return;
			}
			if (this.IsVisiting)
			{
				this.ChangeMode(NKCUIOffice.Mode.Visit, -1, -1L);
				return;
			}
			if (this.m_currentRoomTemplet != null && this.m_currentRoomTemplet.IsFacility)
			{
				this.ChangeMode(NKCUIOffice.Mode.Facility, -1, -1L);
				return;
			}
			this.ChangeMode(NKCUIOffice.Mode.Normal, -1, -1L);
		}

		// Token: 0x06007DC9 RID: 32201 RVA: 0x002A3144 File Offset: 0x002A1344
		private void SetLastFurniture(int newItemID)
		{
			if (this.m_slotLastFuniture != null)
			{
				NKCUtil.SetGameobjectActive(this.m_goLastFuniture, true);
				long freeInteriorCount = NKCScenManager.CurrentUserData().OfficeData.GetFreeInteriorCount(newItemID);
				NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeMiscItemData(newItemID, freeInteriorCount, 0);
				this.m_slotLastFuniture.SetData(data, false, true, false, new NKCUISlot.OnClick(this.OnRepeatLastAdd));
				this.m_slotLastFuniture.SetDisable(freeInteriorCount <= 0L, "");
			}
		}

		// Token: 0x06007DCA RID: 32202 RVA: 0x002A31B8 File Offset: 0x002A13B8
		private void UpdateLastFurniture()
		{
			if (this.m_goLastFuniture == null || !this.m_goLastFuniture.activeInHierarchy)
			{
				return;
			}
			if (this.m_slotLastFuniture != null)
			{
				NKCUISlot.SlotData slotData = this.m_slotLastFuniture.GetSlotData();
				long freeInteriorCount = NKCScenManager.CurrentUserData().OfficeData.GetFreeInteriorCount(slotData.ID);
				slotData.Count = freeInteriorCount;
				this.m_slotLastFuniture.SetData(slotData, false, true, false, new NKCUISlot.OnClick(this.OnRepeatLastAdd));
				this.m_slotLastFuniture.SetDisable(freeInteriorCount <= 0L, "");
			}
		}

		// Token: 0x06007DCB RID: 32203 RVA: 0x002A324C File Offset: 0x002A144C
		private void TryFurnitureAdd(int id, BuildingFloor target, int x, int y, bool bInvert)
		{
			Debug.Log(string.Format("TryFurnitureAdd : {0} {1}({2},{3}) invert : {4}", new object[]
			{
				id,
				target,
				x,
				y,
				bInvert
			}));
			NKCOfficeFunitureData funitureData = new NKCOfficeFunitureData(-1L, id, target, x, y, bInvert);
			NKM_ERROR_CODE nkm_ERROR_CODE = this.m_currentRoom.CanAddFuniture(funitureData, false);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupMessageManager.AddPopupMessage(nkm_ERROR_CODE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			NKCPacketSender.Send_NKMPacket_OFFICE_ADD_FURNITURE_REQ(this.RoomID, id, (OfficePlaneType)target, x, y, bInvert);
		}

		// Token: 0x06007DCC RID: 32204 RVA: 0x002A32E0 File Offset: 0x002A14E0
		public void OnAddFurniture(int roomID, NKMOfficeFurniture nkmFurniture)
		{
			if (roomID != this.RoomID)
			{
				return;
			}
			NKCOfficeFunitureData funitureData = new NKCOfficeFunitureData(nkmFurniture);
			this.m_currentRoom.AddFuniture(funitureData);
			this.SetLastFurniture(nkmFurniture.itemId);
			this.m_OfficeBuilding.AddFuniture(funitureData, true);
			this.ChangeMode(NKCUIOffice.Mode.Edit, -1, -1L);
		}

		// Token: 0x06007DCD RID: 32205 RVA: 0x002A332D File Offset: 0x002A152D
		private void OnRepeatLastAdd(NKCUISlot.SlotData slotData, bool bLocked)
		{
			if (NKCScenManager.CurrentUserData().OfficeData.GetFreeInteriorCount(slotData.ID) <= 0L)
			{
				NKCPopupMessageManager.AddPopupMessage(NKM_ERROR_CODE.NEC_FAIL_OFFICE_FURNITURE_NOT_REMAINS, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			this.OnFunitureAddSelected(slotData.ID);
		}

		// Token: 0x06007DCE RID: 32206 RVA: 0x002A3368 File Offset: 0x002A1568
		private void OnSelectFuniture(int id, long uid)
		{
			switch (this.m_eMode)
			{
			case NKCUIOffice.Mode.Normal:
			case NKCUIOffice.Mode.Facility:
			case NKCUIOffice.Mode.Hide:
				this.m_OfficeBuilding.TouchFurniture(uid);
				break;
			case NKCUIOffice.Mode.Edit:
				this.OnFunitureMoveMode(id, uid);
				return;
			case NKCUIOffice.Mode.EditAdd:
			case NKCUIOffice.Mode.EditMove:
				break;
			default:
				return;
			}
		}

		// Token: 0x06007DCF RID: 32207 RVA: 0x002A33B0 File Offset: 0x002A15B0
		private void OnFunitureMoveMode(int id, long uid)
		{
			this.ChangeMode(NKCUIOffice.Mode.EditMove, id, uid);
		}

		// Token: 0x06007DD0 RID: 32208 RVA: 0x002A33BC File Offset: 0x002A15BC
		private void TryFurnitureMove(long uid, BuildingFloor target, int x, int y, bool bInvert)
		{
			Debug.Log(string.Format("TryFunitureMove : {0} {1}({2},{3}) invert : {4}", new object[]
			{
				uid,
				target,
				x,
				y,
				bInvert
			}));
			NKM_ERROR_CODE nkm_ERROR_CODE = this.m_currentRoom.CanMoveFuniture(uid, target, x, y, bInvert);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupMessageManager.AddPopupMessage(nkm_ERROR_CODE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			NKCPacketSender.Send_NKMPacket_OFFICE_UPDATE_FURNITURE_REQ(this.RoomID, uid, (OfficePlaneType)target, x, y, bInvert);
		}

		// Token: 0x06007DD1 RID: 32209 RVA: 0x002A3448 File Offset: 0x002A1648
		public void OnFurnitureMove(int roomID, NKMOfficeFurniture nkmOfficeFurniture)
		{
			if (roomID != this.RoomID)
			{
				return;
			}
			NKCOfficeFunitureData nkcofficeFunitureData = new NKCOfficeFunitureData(nkmOfficeFurniture);
			this.m_currentRoom.MoveFuniture(nkcofficeFunitureData.uid, nkcofficeFunitureData.eTarget, nkcofficeFunitureData.PosX, nkcofficeFunitureData.PosY, nkcofficeFunitureData.bInvert);
			this.m_OfficeBuilding.MoveFuniture(nkcofficeFunitureData);
			this.ChangeMode(NKCUIOffice.Mode.Edit, -1, -1L);
		}

		// Token: 0x06007DD2 RID: 32210 RVA: 0x002A34A8 File Offset: 0x002A16A8
		private void TryRemoveFurniture(long uid)
		{
			Debug.Log(string.Format("TryFurnitureRemove : {0}", uid));
			NKM_ERROR_CODE nkm_ERROR_CODE = this.m_currentRoom.CanRemoveFurniture(uid);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupOKCancel.OpenOKBox(nkm_ERROR_CODE, null, "");
			}
			NKCPacketSender.Send_NKMPacket_OFFICE_REMOVE_FURNITURE_REQ(this.RoomID, uid);
		}

		// Token: 0x06007DD3 RID: 32211 RVA: 0x002A34F2 File Offset: 0x002A16F2
		public void OnRemoveFurniture(int roomID, long uid)
		{
			if (roomID != this.RoomID)
			{
				return;
			}
			this.m_currentRoom.RemoveFuniture(uid);
			this.m_OfficeBuilding.RemoveFuniture(uid);
			this.UpdateLastFurniture();
			this.ChangeMode(NKCUIOffice.Mode.Edit, -1, -1L);
		}

		// Token: 0x06007DD4 RID: 32212 RVA: 0x002A3526 File Offset: 0x002A1726
		private void TryRemoveAllFurnitures()
		{
			NKCPacketSender.Send_NKMPacket_OFFICE_CLEAR_ALL_FURNITURE_REQ(this.RoomID);
		}

		// Token: 0x06007DD5 RID: 32213 RVA: 0x002A3533 File Offset: 0x002A1733
		public void OnRemoveAllFurnitures(int roomID)
		{
			if (roomID != this.RoomID)
			{
				return;
			}
			this.m_currentRoom.ClearAllFunitures();
			this.m_OfficeBuilding.ClearAllFunitures();
			this.UpdateEnvScore();
			this.UpdateLastFurniture();
			this.ChangeMode(NKCUIOffice.Mode.Edit, -1, -1L);
		}

		// Token: 0x06007DD6 RID: 32214 RVA: 0x002A356C File Offset: 0x002A176C
		private void UpdateSelectedFurnitureInfo(int furnitureID)
		{
			if (furnitureID <= 0)
			{
				NKCUtil.SetGameobjectActive(this.m_objEditFurnitureRoot, false);
				return;
			}
			NKMOfficeInteriorTemplet nkmofficeInteriorTemplet = NKMOfficeInteriorTemplet.Find(furnitureID);
			if (nkmofficeInteriorTemplet != null)
			{
				NKCUtil.SetGameobjectActive(this.m_objEditFurnitureRoot, true);
				NKCUtil.SetLabelText(this.m_lbEditFurnitureName, nkmofficeInteriorTemplet.GetItemName());
				NKCUtil.SetLabelText(this.m_lbEditFurnitureEnvScore, nkmofficeInteriorTemplet.InteriorScore.ToString());
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objEditFurnitureRoot, false);
		}

		// Token: 0x06007DD7 RID: 32215 RVA: 0x002A35D8 File Offset: 0x002A17D8
		private void TryApplyDecoration(NKMOfficeInteriorTemplet templet)
		{
			switch (templet.Target)
			{
			case InteriorTarget.Floor:
				NKCPacketSender.Send_NKMPacket_OFFICE_SET_ROOM_FLOOR_REQ(this.m_currentRoom.ID, templet.Key);
				return;
			case InteriorTarget.Tile:
				break;
			case InteriorTarget.Wall:
				NKCPacketSender.Send_NKMPacket_OFFICE_SET_ROOM_WALL_REQ(this.m_currentRoom.ID, templet.Key);
				break;
			case InteriorTarget.Background:
				NKCPacketSender.Send_NKMPacket_OFFICE_SET_ROOM_BACKGROUND_REQ(this.m_currentRoom.ID, templet.Key);
				return;
			default:
				return;
			}
		}

		// Token: 0x06007DD8 RID: 32216 RVA: 0x002A3648 File Offset: 0x002A1848
		public void OnApplyDecoration(int id)
		{
			NKMOfficeInteriorTemplet nkmofficeInteriorTemplet = NKMItemMiscTemplet.FindInterior(id);
			NKM_ERROR_CODE nkm_ERROR_CODE = this.m_currentRoom.SetDecoration(nkmofficeInteriorTemplet.Key, nkmofficeInteriorTemplet.Target);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				Debug.LogError(nkm_ERROR_CODE);
			}
			this.m_OfficeBuilding.SetDecoration(nkmofficeInteriorTemplet);
			this.UpdateEnvScore();
		}

		// Token: 0x06007DD9 RID: 32217 RVA: 0x002A3694 File Offset: 0x002A1894
		private void OnBtnShop()
		{
			NKCContentManager.MoveToShortCut(NKM_SHORTCUT_TYPE.SHORTCUT_SHOP, "TAB_EXCHANGE_OFFICE", false);
		}

		// Token: 0x06007DDA RID: 32218 RVA: 0x002A36A3 File Offset: 0x002A18A3
		private void OnBtnEditMode()
		{
			if (this.IsVisiting)
			{
				return;
			}
			this.ChangeMode(NKCUIOffice.Mode.Edit, -1, -1L);
		}

		// Token: 0x06007DDB RID: 32219 RVA: 0x002A36B8 File Offset: 0x002A18B8
		private void OnBtnWarehouse()
		{
			if (this.IsVisiting)
			{
				return;
			}
			NKCUIPopupOfficeInteriorSelect.Instance.Open(new NKCUIPopupOfficeInteriorSelect.OnSelectInterior(this.OnFunitureAddSelected), new NKCUIPopupOfficeInteriorSelect.OnSelectPreset(this.OnThemePresetSelected));
		}

		// Token: 0x06007DDC RID: 32220 RVA: 0x002A36E5 File Offset: 0x002A18E5
		private void OnBtnDiployUnit()
		{
			if (this.IsVisiting)
			{
				return;
			}
			NKCUIPopupOfficeMemberEdit.Instance.Open(this.RoomID, null);
		}

		// Token: 0x06007DDD RID: 32221 RVA: 0x002A3701 File Offset: 0x002A1901
		private void OnBtnCommunity()
		{
			if (this.IsVisiting)
			{
				return;
			}
			NKCUIPopupOfficeInteract.Instance.Open();
		}

		// Token: 0x06007DDE RID: 32222 RVA: 0x002A3718 File Offset: 0x002A1918
		private void OnBtnParty()
		{
			if (!NKMOpenTagManager.IsOpened("OFFICE_PARTY"))
			{
				return;
			}
			if (this.m_currentRoom.m_lstUnitUID.Count == 0)
			{
				NKCPopupMessageManager.AddPopupMessage(NKM_ERROR_CODE.NEC_FAIL_OFFICE_PARTY_NO_UNIT_EXIST, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			NKCUIPopupOfficePartyConfirm.Instance.Open(this.RoomID, new NKCUIPopupOfficePartyConfirm.OnComfirm(this.OnPartyConfirm));
		}

		// Token: 0x06007DDF RID: 32223 RVA: 0x002A3774 File Offset: 0x002A1974
		private void OnPartyConfirm(int roomID)
		{
			int itemMiscID = NKMCommonConst.Office.PartyUseItem.m_ItemMiscID;
			if (NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(itemMiscID) < 1L)
			{
				NKCShopManager.OpenItemLackPopup(itemMiscID, 1);
				return;
			}
			NKCPacketSender.Send_NKMPacket_OFFICE_PARTY_REQ(roomID);
		}

		// Token: 0x06007DE0 RID: 32224 RVA: 0x002A37B3 File Offset: 0x002A19B3
		public void OnPartyFinished(NKCOfficePartyTemplet partyTemplet)
		{
			if (this.m_OfficeBuilding != null)
			{
				this.m_OfficeBuilding.OnPartyFinished(partyTemplet);
			}
		}

		// Token: 0x06007DE1 RID: 32225 RVA: 0x002A37CF File Offset: 0x002A19CF
		private void OnBtnToMyOffice()
		{
			base.Close();
			NKCUIOfficeMapFront.GetInstance().SetMyOfficeData(null);
		}

		// Token: 0x06007DE2 RID: 32226 RVA: 0x002A37E2 File Offset: 0x002A19E2
		private void OnBtnRandomVisit()
		{
			NKCPacketSender.Send_NKMPacket_OFFICE_RANDOM_VISIT_REQ();
		}

		// Token: 0x06007DE3 RID: 32227 RVA: 0x002A37E9 File Offset: 0x002A19E9
		private void OnBtnSendBizCard()
		{
			if (this.GetPostSendCount() <= 0)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString("NEC_FAIL_OFFICE_POST_DAILY_LIMIT_FULL", false), null, "");
				return;
			}
			NKCPacketSender.Send_NKMPacket_OFFICE_POST_SEND_REQ(this.CurrentVisitUID);
		}

		// Token: 0x06007DE4 RID: 32228 RVA: 0x002A381C File Offset: 0x002A1A1C
		public void OnRoomUnitUpdated()
		{
			if (this.IsVisiting)
			{
				return;
			}
			NKMOfficeRoom officeRoom = NKCScenManager.CurrentUserData().OfficeData.GetOfficeRoom(this.RoomID);
			if (officeRoom == null)
			{
				return;
			}
			this.m_currentRoom.m_lstUnitUID = new List<long>(officeRoom.unitUids);
			this.m_OfficeBuilding.UpdateSDCharacters(officeRoom.unitUids, this.m_lstVisitor);
		}

		// Token: 0x06007DE5 RID: 32229 RVA: 0x002A387C File Offset: 0x002A1A7C
		private void UpdateEditButtons()
		{
			NKCUIOffice.Mode eMode = this.m_eMode;
			if (eMode <= NKCUIOffice.Mode.Edit)
			{
				this.m_csbtnEditInfo.Lock(false);
				this.m_csbtnEditInvert.Lock(false);
				this.m_csbtnEditStore.Lock(false);
				this.m_csbtnEditStoreAll.UnLock(false);
				this.m_csbtnEditSave.Lock(false);
				this.UpdateSelectedFurnitureInfo(0);
				return;
			}
			if (eMode - NKCUIOffice.Mode.EditAdd > 1)
			{
				return;
			}
			if (this.m_OfficeBuilding.m_SelectedFunitureData != null)
			{
				this.m_csbtnEditInfo.UnLock(false);
				this.m_csbtnEditInvert.SetLock(this.m_OfficeBuilding.m_SelectedFunitureData.Templet.Target == InteriorTarget.Wall, false);
				this.m_csbtnEditStore.UnLock(false);
				this.m_csbtnEditStoreAll.UnLock(false);
				this.m_csbtnEditSave.UnLock(false);
				this.UpdateSelectedFurnitureInfo(this.m_OfficeBuilding.m_SelectedFunitureData.itemID);
				return;
			}
			this.m_csbtnEditInfo.Lock(false);
			this.m_csbtnEditInvert.Lock(false);
			this.m_csbtnEditStore.Lock(false);
			this.m_csbtnEditStoreAll.UnLock(false);
			this.m_csbtnEditSave.Lock(false);
			this.UpdateSelectedFurnitureInfo(0);
		}

		// Token: 0x06007DE6 RID: 32230 RVA: 0x002A399C File Offset: 0x002A1B9C
		private void OnBtnEditSave()
		{
			NKCUIOffice.Mode eMode = this.m_eMode;
			if (eMode == NKCUIOffice.Mode.EditAdd)
			{
				this.TryFurnitureAdd(this.m_OfficeBuilding.m_SelectedFunitureData.itemID, this.m_OfficeBuilding.m_SelectedFunitureData.eTarget, this.m_OfficeBuilding.m_SelectedFunitureData.PosX, this.m_OfficeBuilding.m_SelectedFunitureData.PosY, this.m_OfficeBuilding.m_SelectedFunitureData.bInvert);
				return;
			}
			if (eMode != NKCUIOffice.Mode.EditMove)
			{
				return;
			}
			this.TryFurnitureMove(this.m_OfficeBuilding.m_SelectedFunitureData.uid, this.m_OfficeBuilding.m_SelectedFunitureData.eTarget, this.m_OfficeBuilding.m_SelectedFunitureData.PosX, this.m_OfficeBuilding.m_SelectedFunitureData.PosY, this.m_OfficeBuilding.m_SelectedFunitureData.bInvert);
		}

		// Token: 0x06007DE7 RID: 32231 RVA: 0x002A3A66 File Offset: 0x002A1C66
		private void OnBtnEditStoreAll()
		{
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString("SI_OFFICE_CONFIRM_STORE_ALL", false), new NKCPopupOKCancel.OnButton(this.TryRemoveAllFurnitures), null, false);
		}

		// Token: 0x06007DE8 RID: 32232 RVA: 0x002A3A8C File Offset: 0x002A1C8C
		private void OnBtnEditStore()
		{
			NKCUIOffice.Mode eMode = this.m_eMode;
			if (eMode == NKCUIOffice.Mode.EditAdd)
			{
				this.ChangeMode(NKCUIOffice.Mode.Edit, -1, -1L);
				return;
			}
			if (eMode != NKCUIOffice.Mode.EditMove)
			{
				return;
			}
			this.TryRemoveFurniture(this.m_OfficeBuilding.m_SelectedFunitureData.uid);
		}

		// Token: 0x06007DE9 RID: 32233 RVA: 0x002A3ACA File Offset: 0x002A1CCA
		private void OnBtnEditInvert()
		{
			if (this.m_OfficeBuilding.m_SelectedFunitureData.Templet.Target != InteriorTarget.Wall)
			{
				this.m_OfficeBuilding.InvertSelection();
			}
		}

		// Token: 0x06007DEA RID: 32234 RVA: 0x002A3AEF File Offset: 0x002A1CEF
		private void OnBtnEditInfo()
		{
			NKCPopupItemBox.Instance.OpenItemBox(this.m_OfficeBuilding.m_SelectedFunitureData.itemID, NKCPopupItemBox.eMode.Normal, null);
		}

		// Token: 0x06007DEB RID: 32235 RVA: 0x002A3B10 File Offset: 0x002A1D10
		private void OnBtnEditClose()
		{
			NKCUIOffice.Mode eMode = this.m_eMode;
			if (eMode != NKCUIOffice.Mode.Edit && eMode - NKCUIOffice.Mode.EditAdd <= 1)
			{
				this.ChangeMode(NKCUIOffice.Mode.Edit, -1, -1L);
				return;
			}
			this.ChangeMode(NKCUIOffice.Mode.Normal, -1, -1L);
		}

		// Token: 0x06007DEC RID: 32236 RVA: 0x002A3B43 File Offset: 0x002A1D43
		private void OnBtnEditCopyPreset()
		{
			if (NKCScenManager.CurrentUserData().IsSuperUser() && this.m_currentRoom != null)
			{
				GUIUtility.systemCopyBuffer = this.m_currentRoom.MakePresetFromRoom().ToBase64<NKMOfficePreset>();
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, "클립보드로 복사되었습니다", null, "");
			}
		}

		// Token: 0x06007DED RID: 32237 RVA: 0x002A3B84 File Offset: 0x002A1D84
		private void UpdateEnvScore()
		{
			NKMOfficeRoom room;
			if (this.IsVisiting)
			{
				room = NKCScenManager.CurrentUserData().OfficeData.GetFriendRoom(this.CurrentVisitUID, this.RoomID);
			}
			else
			{
				room = NKCScenManager.CurrentUserData().OfficeData.GetOfficeRoom(this.RoomID);
			}
			this.UpdateEnvScore(room);
		}

		// Token: 0x06007DEE RID: 32238 RVA: 0x002A3BD4 File Offset: 0x002A1DD4
		private void UpdateEnvScore(NKMOfficeRoom room)
		{
			if (this.m_comEnvScore != null)
			{
				this.m_comEnvScore.UpdateEnvScore(room);
			}
			if (this.m_comEditEnvScore != null)
			{
				this.m_comEditEnvScore.UpdateEnvScore(room);
			}
		}

		// Token: 0x06007DEF RID: 32239 RVA: 0x002A3C0C File Offset: 0x002A1E0C
		private void LoadNPCSpineIllust(NKMOfficeRoomTemplet.RoomType type, RectTransform parent)
		{
			foreach (NKCASUIUnitIllust nkcasuiunitIllust in this.m_lstNPCIllust)
			{
				nkcasuiunitIllust.Unload();
			}
			this.m_lstNPCIllust.Clear();
			foreach (NKCUIOffice.NPCInfo npcinfo in this.m_lstNPCInfo)
			{
				if (npcinfo.Type == type)
				{
					NKMAssetName nkmassetName = NKMAssetName.ParseBundleName(npcinfo.BundleName, npcinfo.BundleName);
					NKCASUIUnitIllust nkcasuiunitIllust2 = NKCResourceUtility.OpenSpineIllust(nkmassetName.m_BundleName, nkmassetName.m_AssetName, false);
					if (nkcasuiunitIllust2 != null)
					{
						NKCUINPCBase componentInChildren = nkcasuiunitIllust2.GetRectTransform().GetComponentInChildren<NKCUINPCBase>();
						if (componentInChildren != null)
						{
							componentInChildren.Init(true);
						}
					}
					nkcasuiunitIllust2.SetParent(parent, false);
					nkcasuiunitIllust2.GetRectTransform().anchoredPosition = npcinfo.Offset;
					this.m_lstNPCIllust.Add(nkcasuiunitIllust2);
				}
			}
		}

		// Token: 0x06007DF0 RID: 32240 RVA: 0x002A3D24 File Offset: 0x002A1F24
		private void PlayRoomMusic()
		{
			NKMOfficeInteriorTemplet nkmofficeInteriorTemplet = null;
			if (this.m_currentRoom != null)
			{
				foreach (NKCOfficeFunitureData nkcofficeFunitureData in this.m_currentRoom.m_dicFuniture.Values)
				{
					if (nkcofficeFunitureData.Templet.HasBGM)
					{
						nkmofficeInteriorTemplet = nkcofficeFunitureData.Templet;
					}
				}
			}
			if (nkmofficeInteriorTemplet != null)
			{
				Debug.Log("Playing BGM furniture music : from " + nkmofficeInteriorTemplet.GetItemName() + " : " + nkmofficeInteriorTemplet.PlayBGM);
				NKCSoundManager.PlayMusic(nkmofficeInteriorTemplet.PlayBGM, true, nkmofficeInteriorTemplet.GetBGMVolume, false, 0f, 0f);
				return;
			}
			if (this.m_currentRoomTemplet != null && !string.IsNullOrEmpty(this.m_currentRoomTemplet.DefaultBGM))
			{
				NKCSoundManager.PlayMusic(this.m_currentRoomTemplet.DefaultBGM, true, 1f, false, 0f, 0f);
			}
		}

		// Token: 0x06007DF1 RID: 32241 RVA: 0x002A3E14 File Offset: 0x002A2014
		private void SetPreview(NKCUIOffice.PresetMode mode, NKMOfficePreset preset)
		{
			if (preset == null)
			{
				return;
			}
			this.m_ePresetMode = mode;
			this.m_currentPresetId = preset.presetId;
			this.m_OfficeBuilding.SetTempFurniture(preset);
			this.ChangeMode(NKCUIOffice.Mode.Preview, -1, -1L);
			NKCUIManager.SetAsTopmost(this, true);
		}

		// Token: 0x06007DF2 RID: 32242 RVA: 0x002A3E4A File Offset: 0x002A204A
		private void CancelPreview()
		{
			this.m_OfficeBuilding.SetRoomData(this.m_currentRoom, this.m_lstVisitor);
			this.m_currentPresetId = -1;
			this.ChangeMode(NKCUIOffice.Mode.Edit, -1, -1L);
		}

		// Token: 0x06007DF3 RID: 32243 RVA: 0x002A3E78 File Offset: 0x002A2078
		public void OnApplyPreset(NKMOfficeRoom room)
		{
			if (room == null)
			{
				return;
			}
			if (room.id != this.m_currentRoom.ID)
			{
				return;
			}
			this.m_currentRoom = new NKCOfficeRoomData(room, 0L);
			this.m_OfficeBuilding.SetRoomData(this.m_currentRoom, this.m_lstVisitor);
			this.ChangeMode(NKCUIOffice.Mode.Edit, -1, -1L);
		}

		// Token: 0x06007DF4 RID: 32244 RVA: 0x002A3ECD File Offset: 0x002A20CD
		private void OnBtnPresetList()
		{
			NKCUIPopupOfficePresetList.Instance.Open(this.RoomID, new NKCUIPopupOfficePresetList.OnAction(this.OnSlotAction));
		}

		// Token: 0x06007DF5 RID: 32245 RVA: 0x002A3EEC File Offset: 0x002A20EC
		private void OnSlotAction(NKCUIPopupOfficePresetList.ActionType type, int id, string name)
		{
			switch (type)
			{
			case NKCUIPopupOfficePresetList.ActionType.Save:
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString("SI_PF_OFFICE_DECO_MODE_PRESET_SAVE_DESC", false), delegate()
				{
					NKCPacketSender.Send_NKMPacket_OFFICE_PRESET_REGISTER_REQ(this.RoomID, id);
				}, null, false);
				return;
			case NKCUIPopupOfficePresetList.ActionType.Load:
			{
				NKMOfficePreset preset = NKCScenManager.CurrentUserData().OfficeData.GetPreset(id);
				this.SetPreview(NKCUIOffice.PresetMode.MyPreset, preset);
				return;
			}
			case NKCUIPopupOfficePresetList.ActionType.Clear:
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString("SI_PF_OFFICE_DECO_MODE_PRESET_RESET_DESC", false), delegate()
				{
					NKCPacketSender.Send_NKMPacket_OFFICE_PRESET_RESET_REQ(id);
				}, null, false);
				return;
			case NKCUIPopupOfficePresetList.ActionType.Rename:
				NKCPacketSender.Send_NKMPacket_OFFICE_PRESET_CHANGE_NAME_REQ(id, name);
				return;
			case NKCUIPopupOfficePresetList.ActionType.Add:
			{
				NKCPopupInventoryAdd.SliderInfo sliderInfo = default(NKCPopupInventoryAdd.SliderInfo);
				sliderInfo.increaseCount = 1;
				sliderInfo.maxCount = NKMCommonConst.Office.PresetConst.MaxCount;
				sliderInfo.currentCount = NKCScenManager.CurrentUserData().OfficeData.GetPresetCount();
				sliderInfo.inventoryType = NKM_INVENTORY_EXPAND_TYPE.NIET_NONE;
				NKCPopupInventoryAdd.Instance.Open(NKCStringTable.GetString("SI_PF_OFFICE_DECO_MODE_PRESET_BUY_TOP_TEXT", false), NKCStringTable.GetString("SI_PF_OFFICE_DECO_MODE_PRESET_BUY_INFO_TEXT", false), sliderInfo, NKMCommonConst.Office.PresetConst.ExpandCostValue, NKMCommonConst.Office.PresetConst.ExpandCostItem.m_ItemMiscID, delegate(int count)
				{
					NKCPacketSender.Send_NKMPacket_OFFICE_PRESET_ADD_REQ(count);
				}, true);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06007DF6 RID: 32246 RVA: 0x002A4044 File Offset: 0x002A2244
		private void OnThemePresetSelected(int id)
		{
			NKMOfficeThemePresetTemplet nkmofficeThemePresetTemplet = NKMOfficeThemePresetTemplet.Find(id);
			this.SetPreview(NKCUIOffice.PresetMode.Theme, nkmofficeThemePresetTemplet.OfficePreset);
			this.m_currentPresetId = id;
		}

		// Token: 0x06007DF7 RID: 32247 RVA: 0x002A406C File Offset: 0x002A226C
		private void TryApplyPreset()
		{
			NKCUIOffice.PresetMode ePresetMode = this.m_ePresetMode;
			if (ePresetMode == NKCUIOffice.PresetMode.MyPreset)
			{
				NKCPacketSender.Send_NKMPacket_OFFICE_PRESET_APPLY_REQ(this.RoomID, this.m_currentPresetId);
				return;
			}
			if (ePresetMode != NKCUIOffice.PresetMode.Theme)
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_OFFICE_PRESET_APPLY_THEMA_REQ(this.RoomID, this.m_currentPresetId);
		}

		// Token: 0x06007DF8 RID: 32248 RVA: 0x002A40AB File Offset: 0x002A22AB
		private void OnBtnPreviewCancel()
		{
			this.CancelPreview();
		}

		// Token: 0x06007DF9 RID: 32249 RVA: 0x002A40B4 File Offset: 0x002A22B4
		private void OnPresetInteriorList()
		{
			NKMOfficePreset nkmofficePreset = null;
			NKCUIOffice.PresetMode ePresetMode = this.m_ePresetMode;
			if (ePresetMode != NKCUIOffice.PresetMode.MyPreset)
			{
				if (ePresetMode == NKCUIOffice.PresetMode.Theme)
				{
					NKMOfficeThemePresetTemplet nkmofficeThemePresetTemplet = NKMOfficeThemePresetTemplet.Find(this.m_currentPresetId);
					if (nkmofficeThemePresetTemplet != null)
					{
						nkmofficePreset = nkmofficeThemePresetTemplet.OfficePreset;
					}
				}
			}
			else
			{
				nkmofficePreset = NKCScenManager.CurrentUserData().OfficeData.GetPreset(this.m_currentPresetId);
			}
			if (nkmofficePreset != null)
			{
				Dictionary<int, int> dictionary = new Dictionary<int, int>();
				dictionary[nkmofficePreset.floorInteriorId] = 1;
				dictionary[nkmofficePreset.wallInteriorId] = 1;
				dictionary[nkmofficePreset.backgroundId] = 1;
				foreach (NKMOfficeFurniture nkmofficeFurniture in nkmofficePreset.furnitures)
				{
					if (nkmofficeFurniture != null)
					{
						if (dictionary.ContainsKey(nkmofficeFurniture.itemId))
						{
							Dictionary<int, int> dictionary2 = dictionary;
							int itemId = nkmofficeFurniture.itemId;
							dictionary2[itemId]++;
						}
						else
						{
							dictionary[nkmofficeFurniture.itemId] = 1;
						}
					}
				}
				NKCUIPopupOfficeInteriorSelect.Instance.OpenForListView(dictionary);
			}
		}

		// Token: 0x06007DFA RID: 32250 RVA: 0x002A41C4 File Offset: 0x002A23C4
		private void OnBtnHideMenu()
		{
			this.HideUI(true);
		}

		// Token: 0x06007DFB RID: 32251 RVA: 0x002A41CD File Offset: 0x002A23CD
		private void OnBtnUnhideMenu()
		{
			this.HideUI(false);
		}

		// Token: 0x06007DFC RID: 32252 RVA: 0x002A41D6 File Offset: 0x002A23D6
		public void OnUnitTakeHeart(NKMUnitData unitData)
		{
			if (this.m_currentRoom != null && this.m_currentRoom.m_lstUnitUID.Contains(unitData.m_UnitUID))
			{
				this.m_OfficeBuilding.OnUnitTakeHeart(unitData);
			}
		}

		// Token: 0x06007DFD RID: 32253 RVA: 0x002A4204 File Offset: 0x002A2404
		public void UpdatePostState()
		{
			int postSendCount = this.GetPostSendCount();
			NKCUtil.SetLabelText(this.m_lbSendBizCardCount, string.Format("{0}/{1}", postSendCount, 5));
			this.CheckCommunityReddot();
		}

		// Token: 0x06007DFE RID: 32254 RVA: 0x002A4240 File Offset: 0x002A2440
		private int GetPostSendCount()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return 0;
			}
			int result;
			if (NKCSynchronizedTime.IsFinished(NKCSynchronizedTime.ToUtcTime(nkmuserData.OfficeData.PostState.nextResetDate)))
			{
				result = 5;
			}
			else
			{
				result = 5 - nkmuserData.OfficeData.PostState.sendCount;
			}
			return result;
		}

		// Token: 0x06007DFF RID: 32255 RVA: 0x002A4290 File Offset: 0x002A2490
		public void UpdateForgeSlot(int index)
		{
			if (this.m_currentRoomTemplet != null && this.m_currentRoomTemplet.Type == NKMOfficeRoomTemplet.RoomType.Forge)
			{
				NKCOfficeFacilityForge nkcofficeFacilityForge = this.m_NKCUIOfficeFacility as NKCOfficeFacilityForge;
				if (nkcofficeFacilityForge != null)
				{
					nkcofficeFacilityForge.UpdateSlot(index);
				}
			}
		}

		// Token: 0x06007E00 RID: 32256 RVA: 0x002A42CF File Offset: 0x002A24CF
		public void UpdateAlarm()
		{
			this.CheckCommunityReddot();
			if (this.m_NKCUIOfficeFacility != null)
			{
				this.m_NKCUIOfficeFacility.UpdateAlarm();
			}
			if (this.m_UIOfficeFacilityButtons != null)
			{
				this.m_UIOfficeFacilityButtons.UpdateAlarm();
			}
		}

		// Token: 0x06007E01 RID: 32257 RVA: 0x002A4309 File Offset: 0x002A2509
		private void CheckCommunityReddot()
		{
			NKCUtil.SetGameobjectActive(this.m_objCommunityReddot, NKCAlarmManager.CheckOfficeCommunityNotify(NKCScenManager.CurrentUserData()));
		}

		// Token: 0x06007E02 RID: 32258 RVA: 0x002A4320 File Offset: 0x002A2520
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			this.UpdateAlarm();
		}

		// Token: 0x06007E03 RID: 32259 RVA: 0x002A4328 File Offset: 0x002A2528
		public override void OnUnitUpdate(NKMUserData.eChangeNotifyType eEventType, NKM_UNIT_TYPE eUnitType, long uid, NKMUnitData unitData)
		{
			this.UpdateAlarm();
			if (eEventType == NKMUserData.eChangeNotifyType.Update && this.m_OfficeBuilding != null)
			{
				this.m_OfficeBuilding.OnUnitUpdated(unitData);
			}
		}

		// Token: 0x06007E04 RID: 32260 RVA: 0x002A4350 File Offset: 0x002A2550
		public override void OnScreenResolutionChanged()
		{
			base.OnScreenResolutionChanged();
			if (this.m_OfficeBuilding != null)
			{
				this.m_OfficeBuilding.SetCameraOffset(0f);
				this.m_OfficeBuilding.SetCamera();
			}
			if (this.m_NKCUIOfficeFacility != null)
			{
				if (this.m_lstNPCIllust.Count > 0)
				{
					this.m_NKCUIOfficeFacility.SetCameraOffset(this.m_rtNPCRoot.GetWidth());
				}
				else
				{
					this.m_NKCUIOfficeFacility.SetCameraOffset(0f);
				}
				this.m_NKCUIOfficeFacility.SetCamera();
			}
		}

		// Token: 0x06007E05 RID: 32261 RVA: 0x002A43DC File Offset: 0x002A25DC
		public bool CanPlayInteraction()
		{
			NKCUIOffice.Mode eMode = this.m_eMode;
			return eMode - NKCUIOffice.Mode.Edit > 2;
		}

		// Token: 0x04006AAC RID: 27308
		private const string ASSET_BUNDLE_NAME = "ab_ui_office";

		// Token: 0x04006AAD RID: 27309
		private const string UI_ASSET_NAME = "AB_UI_OFFICE";

		// Token: 0x04006AAE RID: 27310
		private static NKCUIManager.LoadedUIData s_LoadedUIData;

		// Token: 0x04006AAF RID: 27311
		public NKCOfficeBuilding m_OfficeBuilding;

		// Token: 0x04006AB0 RID: 27312
		private NKCOfficeFacility m_NKCUIOfficeFacility;

		// Token: 0x04006AB1 RID: 27313
		[Header("상단 바")]
		public NKCUIOfficeUpsideMenu m_uiOfficeUpsideMenu;

		// Token: 0x04006AB2 RID: 27314
		[Header("미니맵")]
		public NKCUIComStateButton m_csbtnMinimap;

		// Token: 0x04006AB3 RID: 27315
		[Header("기본 UI")]
		public GameObject m_objNormalMode;

		// Token: 0x04006AB4 RID: 27316
		public GameObject m_objNormalButtons;

		// Token: 0x04006AB5 RID: 27317
		public NKCUIComStateButton m_csbtnShop;

		// Token: 0x04006AB6 RID: 27318
		public NKCUIComStateButton m_csbtnEditMode;

		// Token: 0x04006AB7 RID: 27319
		public NKCUIComStateButton m_csbtnWarehouse;

		// Token: 0x04006AB8 RID: 27320
		public NKCUIComStateButton m_csbtnDeployUnit;

		// Token: 0x04006AB9 RID: 27321
		public NKCUIComStateButton m_csbtnCommunity;

		// Token: 0x04006ABA RID: 27322
		public GameObject m_objCommunityReddot;

		// Token: 0x04006ABB RID: 27323
		public NKCUIComStateButton m_csbtnParty;

		// Token: 0x04006ABC RID: 27324
		[Header("기본 UI - 방문")]
		public GameObject m_objVisitButtons;

		// Token: 0x04006ABD RID: 27325
		public NKCUIComStateButton m_csbtnToMyOffice;

		// Token: 0x04006ABE RID: 27326
		public NKCUIComStateButton m_csbtnRandomVisit;

		// Token: 0x04006ABF RID: 27327
		public NKCUIComStateButton m_csbtnSendBizCard;

		// Token: 0x04006AC0 RID: 27328
		public Text m_lbSendBizCardCount;

		// Token: 0x04006AC1 RID: 27329
		[Header("환경점수")]
		public NKCUIComOfficeEnvScore m_comEnvScore;

		// Token: 0x04006AC2 RID: 27330
		[Header("시설 UI")]
		public RectTransform m_rtNPCRoot;

		// Token: 0x04006AC3 RID: 27331
		public NKCUIOfficeFacilityButtons m_UIOfficeFacilityButtons;

		// Token: 0x04006AC4 RID: 27332
		public List<NKCUIOffice.NPCInfo> m_lstNPCInfo;

		// Token: 0x04006AC5 RID: 27333
		private List<NKCASUIUnitIllust> m_lstNPCIllust = new List<NKCASUIUnitIllust>();

		// Token: 0x04006AC6 RID: 27334
		[Header("메뉴 숨기기 관련")]
		public NKCUIComStateButton m_csbtnHideMenu;

		// Token: 0x04006AC7 RID: 27335
		public NKCUIComStateButton m_csbtnUnhideMenu;

		// Token: 0x04006AC8 RID: 27336
		public GameObject m_objHideMenu;

		// Token: 0x04006AC9 RID: 27337
		[Header("편집 모드 관련")]
		public GameObject m_objEditMode;

		// Token: 0x04006ACA RID: 27338
		public NKCUIComStateButton m_csbtnEditInfo;

		// Token: 0x04006ACB RID: 27339
		public NKCUIComStateButton m_csbtnEditInvert;

		// Token: 0x04006ACC RID: 27340
		public NKCUIComStateButton m_csbtnEditStore;

		// Token: 0x04006ACD RID: 27341
		public NKCUIComStateButton m_csbtnEditStoreAll;

		// Token: 0x04006ACE RID: 27342
		public NKCUIComStateButton m_csbtnEditSave;

		// Token: 0x04006ACF RID: 27343
		public NKCUIComStateButton m_csbtnEditWarehouse;

		// Token: 0x04006AD0 RID: 27344
		public NKCUIComStateButton m_csbtnEditClose;

		// Token: 0x04006AD1 RID: 27345
		public NKCUIComStateButton m_csbtnEditCopyPreset;

		// Token: 0x04006AD2 RID: 27346
		public NKCUIComOfficeEnvScore m_comEditEnvScore;

		// Token: 0x04006AD3 RID: 27347
		public GameObject m_objEditFurnitureRoot;

		// Token: 0x04006AD4 RID: 27348
		public Text m_lbEditFurnitureEnvScore;

		// Token: 0x04006AD5 RID: 27349
		public Text m_lbEditFurnitureName;

		// Token: 0x04006AD6 RID: 27350
		public GameObject m_goLastFuniture;

		// Token: 0x04006AD7 RID: 27351
		public NKCUISlot m_slotLastFuniture;

		// Token: 0x04006AD8 RID: 27352
		[Header("프리셋/프리뷰 모드 관련")]
		public NKCUIComStateButton m_csbtnPresetList;

		// Token: 0x04006AD9 RID: 27353
		public GameObject m_objPreviewMode;

		// Token: 0x04006ADA RID: 27354
		public NKCUIComStateButton m_csbtnPreviewInteriorList;

		// Token: 0x04006ADB RID: 27355
		public NKCUIComStateButton m_csbtnPreviewOK;

		// Token: 0x04006ADC RID: 27356
		public NKCUIComStateButton m_csbtnPreviewCancel;

		// Token: 0x04006ADD RID: 27357
		private NKMOfficeRoomTemplet m_currentRoomTemplet;

		// Token: 0x04006ADE RID: 27358
		private NKCOfficeRoomData m_currentRoom;

		// Token: 0x04006ADF RID: 27359
		private NKCUIOffice.Mode m_eMode;

		// Token: 0x04006AE0 RID: 27360
		private List<NKMUserProfileData> m_lstVisitor = new List<NKMUserProfileData>();

		// Token: 0x04006AE1 RID: 27361
		private NKCUIOffice.PresetMode m_ePresetMode;

		// Token: 0x04006AE2 RID: 27362
		private int m_currentPresetId = -1;

		// Token: 0x02001864 RID: 6244
		[Serializable]
		public class NPCInfo
		{
			// Token: 0x0400A8CB RID: 43211
			public NKMOfficeRoomTemplet.RoomType Type;

			// Token: 0x0400A8CC RID: 43212
			public Vector2 Offset;

			// Token: 0x0400A8CD RID: 43213
			public string BundleName;
		}

		// Token: 0x02001865 RID: 6245
		private enum Mode
		{
			// Token: 0x0400A8CF RID: 43215
			Normal,
			// Token: 0x0400A8D0 RID: 43216
			Edit,
			// Token: 0x0400A8D1 RID: 43217
			EditAdd,
			// Token: 0x0400A8D2 RID: 43218
			EditMove,
			// Token: 0x0400A8D3 RID: 43219
			Facility,
			// Token: 0x0400A8D4 RID: 43220
			Hide,
			// Token: 0x0400A8D5 RID: 43221
			Visit,
			// Token: 0x0400A8D6 RID: 43222
			Preview
		}

		// Token: 0x02001866 RID: 6246
		private enum PresetMode
		{
			// Token: 0x0400A8D8 RID: 43224
			MyPreset,
			// Token: 0x0400A8D9 RID: 43225
			Theme
		}
	}
}
