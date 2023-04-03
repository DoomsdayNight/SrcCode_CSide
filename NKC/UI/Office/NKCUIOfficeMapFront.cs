using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKC.Office;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using NKM.Templet.Office;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Office
{
	// Token: 0x02000AF1 RID: 2801
	public class NKCUIOfficeMapFront : NKCUIBase
	{
		// Token: 0x06007E21 RID: 32289 RVA: 0x002A4D75 File Offset: 0x002A2F75
		public static NKCUIManager.LoadedUIData OpenNewInstanceAsync()
		{
			if (!NKCUIManager.IsValid(NKCUIOfficeMapFront.s_LoadedUIData))
			{
				NKCUIOfficeMapFront.s_LoadedUIData = NKCUIManager.OpenNewInstanceAsync<NKCUIOfficeMapFront>("ab_ui_office", "AB_UI_OFFICE_MAP_UI_FRONT", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIOfficeMapFront.CleanupInstance));
			}
			return NKCUIOfficeMapFront.s_LoadedUIData;
		}

		// Token: 0x170014D7 RID: 5335
		// (get) Token: 0x06007E22 RID: 32290 RVA: 0x002A4DA9 File Offset: 0x002A2FA9
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIOfficeMapFront.s_LoadedUIData != null && NKCUIOfficeMapFront.s_LoadedUIData.IsUIOpen;
			}
		}

		// Token: 0x170014D8 RID: 5336
		// (get) Token: 0x06007E23 RID: 32291 RVA: 0x002A4DBE File Offset: 0x002A2FBE
		public static bool IsInstanceLoaded
		{
			get
			{
				return NKCUIOfficeMapFront.s_LoadedUIData != null && NKCUIOfficeMapFront.s_LoadedUIData.IsLoadComplete;
			}
		}

		// Token: 0x06007E24 RID: 32292 RVA: 0x002A4DD3 File Offset: 0x002A2FD3
		public static NKCUIOfficeMapFront GetInstance()
		{
			if (NKCUIOfficeMapFront.s_LoadedUIData != null && NKCUIOfficeMapFront.s_LoadedUIData.IsLoadComplete)
			{
				return NKCUIOfficeMapFront.s_LoadedUIData.GetInstance<NKCUIOfficeMapFront>();
			}
			return null;
		}

		// Token: 0x06007E25 RID: 32293 RVA: 0x002A4DF4 File Offset: 0x002A2FF4
		public static void CleanupInstance()
		{
			NKCUIOfficeMapFront.s_LoadedUIData = null;
		}

		// Token: 0x170014D9 RID: 5337
		// (get) Token: 0x06007E26 RID: 32294 RVA: 0x002A4DFC File Offset: 0x002A2FFC
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170014DA RID: 5338
		// (get) Token: 0x06007E27 RID: 32295 RVA: 0x002A4DFF File Offset: 0x002A2FFF
		public override string MenuName
		{
			get
			{
				return "사옥 미니맵";
			}
		}

		// Token: 0x170014DB RID: 5339
		// (get) Token: 0x06007E28 RID: 32296 RVA: 0x002A4E06 File Offset: 0x002A3006
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x170014DC RID: 5340
		// (get) Token: 0x06007E29 RID: 32297 RVA: 0x002A4E09 File Offset: 0x002A3009
		public NKCUIOfficeUpsideMenu OfficeUpsideMenu
		{
			get
			{
				return this.m_officeUpsideMenu;
			}
		}

		// Token: 0x170014DD RID: 5341
		// (get) Token: 0x06007E2A RID: 32298 RVA: 0x002A4E11 File Offset: 0x002A3011
		public NKCOfficeData OfficeData
		{
			get
			{
				return this.m_NKCOfficeData;
			}
		}

		// Token: 0x170014DE RID: 5342
		// (get) Token: 0x06007E2B RID: 32299 RVA: 0x002A4E19 File Offset: 0x002A3019
		public NKCUIOfficeMapFront.MinimapState CurrentMinimapState
		{
			get
			{
				return this.m_eCurrentMinimapState;
			}
		}

		// Token: 0x170014DF RID: 5343
		// (get) Token: 0x06007E2C RID: 32300 RVA: 0x002A4E21 File Offset: 0x002A3021
		public NKCUIOfficeMapFront.SectionType CurrentSectionType
		{
			get
			{
				return this.m_eCurrentSectionType;
			}
		}

		// Token: 0x170014E0 RID: 5344
		// (get) Token: 0x06007E2D RID: 32301 RVA: 0x002A4E29 File Offset: 0x002A3029
		public bool Visiting
		{
			get
			{
				return NKCUIOfficeMapFront.m_bVisiting;
			}
		}

		// Token: 0x170014E1 RID: 5345
		// (set) Token: 0x06007E2E RID: 32302 RVA: 0x002A4E30 File Offset: 0x002A3030
		public static NKM_SCEN_ID ReserveScenID
		{
			set
			{
				NKCUIOfficeMapFront.m_eReserveScenId = value;
			}
		}

		// Token: 0x06007E2F RID: 32303 RVA: 0x002A4E38 File Offset: 0x002A3038
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
			this.m_NKCMinimap.Release();
			this.OfficeData.ResetFriendUId();
			UnityEngine.Object.Destroy(this.m_NKCMinimap.gameObject);
		}

		// Token: 0x06007E30 RID: 32304 RVA: 0x002A4E6C File Offset: 0x002A306C
		public override void OnCloseInstance()
		{
			base.OnCloseInstance();
			this.m_NKCOfficeData = null;
			this.m_NKCMinimap = null;
			this.m_NKCMinimapRoom = null;
			this.m_NKCMinimapFacility = null;
			this.m_currentMinimap = null;
			Dictionary<NKCUIOfficeMapFront.SectionType, List<int>> dicSectionId = this.m_dicSectionId;
			if (dicSectionId != null)
			{
				dicSectionId.Clear();
			}
			this.m_dicSectionId = null;
		}

		// Token: 0x06007E31 RID: 32305 RVA: 0x002A4EBC File Offset: 0x002A30BC
		public void Init()
		{
			GameObject orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<GameObject>("ab_ui_office", "AB_UI_OFFICE_MINIMAP", false);
			if (orLoadAssetResource != null)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(orLoadAssetResource, NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontCommonLow));
				NKCUIOfficeMinimap nkcuiofficeMinimap = gameObject.GetComponent<NKCUIOfficeMinimap>();
				if (nkcuiofficeMinimap == null)
				{
					nkcuiofficeMinimap = gameObject.AddComponent<NKCUIOfficeMinimap>();
				}
				if (nkcuiofficeMinimap != null)
				{
					nkcuiofficeMinimap.Init();
					this.m_NKCMinimap = nkcuiofficeMinimap;
					this.m_NKCMinimapRoom = nkcuiofficeMinimap.m_NKCUIMinimapRoom;
					this.m_NKCMinimapFacility = nkcuiofficeMinimap.m_NKCUIMinimapFacility;
				}
			}
			NKCUIOfficeUpsideMenu officeUpsideMenu = this.m_officeUpsideMenu;
			if (officeUpsideMenu != null)
			{
				officeUpsideMenu.Init();
			}
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglRoom, new UnityAction<bool>(this.OnToggleRoom));
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglFacility, new UnityAction<bool>(this.OnToggleFacility));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnMemberEdit, new UnityAction(this.OnBtnMemberEdit));
			this.m_tglRoom.SetbReverseSeqCallbackCall(true);
			this.m_dicSectionId = new Dictionary<NKCUIOfficeMapFront.SectionType, List<int>>();
			this.m_dicSectionId.Add(NKCUIOfficeMapFront.SectionType.Facility, new List<int>());
			this.m_dicSectionId.Add(NKCUIOfficeMapFront.SectionType.Room, new List<int>());
			foreach (NKMOfficeSectionTemplet nkmofficeSectionTemplet in NKMTempletContainer<NKMOfficeSectionTemplet>.Values)
			{
				this.m_dicSectionId[nkmofficeSectionTemplet.IsFacility ? NKCUIOfficeMapFront.SectionType.Facility : NKCUIOfficeMapFront.SectionType.Room].Add(nkmofficeSectionTemplet.SectionId);
			}
			this.m_dicSectionId[NKCUIOfficeMapFront.SectionType.Facility].Sort();
			this.m_dicSectionId[NKCUIOfficeMapFront.SectionType.Room].Sort();
			NKCUtil.SetGameobjectActive(this.m_objRoomRedDot, false);
			NKCUtil.SetGameobjectActive(this.m_objFacilityRedDot, false);
			NKCUtil.SetScrollHotKey(this.m_NKCMinimapFacility.GetScrollRect(), this);
			base.SetupScrollRects(this.m_NKCMinimapFacility.GetGameObject());
			NKCUtil.SetScrollHotKey(this.m_NKCMinimapRoom.GetScrollRect(), this);
			base.SetupScrollRects(this.m_NKCMinimapRoom.GetGameObject());
			NKCUtil.SetHotkey(this.m_tglFacility, HotkeyEventType.NextTab, null, false);
			NKCUtil.SetHotkey(this.m_tglRoom, HotkeyEventType.NextTab, null, false);
		}

		// Token: 0x06007E32 RID: 32306 RVA: 0x002A50BC File Offset: 0x002A32BC
		public void Open()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			this.m_NKCOfficeData = nkmuserData.OfficeData;
			NKCUIOfficeMapFront.m_bVisiting = this.m_NKCOfficeData.IsVisiting;
			if (!NKCUIOfficeMapFront.m_bVisiting)
			{
				this.SetMinimapState(NKCUIOfficeMapFront.MinimapState.Main);
				this.m_NKCMinimapFacility.UpdateRoomStateAll();
				this.m_NKCMinimapRoom.UpdateRoomStateAll();
				bool flag = NKCContentManager.IsContentsUnlocked(ContentsType.OFFICE, 0, 0);
				NKCUtil.SetGameobjectActive(this.m_objDormitoryLock, !flag);
				if (this.m_tglFacility != null)
				{
					this.m_tglFacility.Select(false, true, false);
					this.m_tglFacility.Select(true, false, false);
				}
				NKCUtil.SetGameobjectActive(this.m_objFacilityRedDot, this.m_NKCMinimapFacility.IsRedDotOn());
				NKCUtil.SetGameobjectActive(this.m_objRoomRedDot, this.m_NKCMinimapRoom.IsRedDotOn());
			}
			else
			{
				this.SetMinimapState(NKCUIOfficeMapFront.MinimapState.Visit);
				this.m_NKCMinimapRoom.UpdateRoomStateAll();
				if (this.m_tglRoom != null)
				{
					this.m_tglRoom.Select(false, true, false);
					this.m_tglRoom.Select(true, false, false);
				}
			}
			NKCUIOfficeUpsideMenu officeUpsideMenu = this.m_officeUpsideMenu;
			if (officeUpsideMenu != null)
			{
				officeUpsideMenu.SetRedDotNotify();
			}
			base.UIOpened(true);
		}

		// Token: 0x06007E33 RID: 32307 RVA: 0x002A51DF File Offset: 0x002A33DF
		public override void Hide()
		{
			base.Hide();
			this.m_NKCMinimap.SetAcive(false);
		}

		// Token: 0x06007E34 RID: 32308 RVA: 0x002A51F4 File Offset: 0x002A33F4
		public override void UnHide()
		{
			base.UnHide();
			this.m_NKCMinimap.SetAcive(true);
			this.m_currentMinimap.UpdateCameraPosition();
			NKCUtil.SetGameobjectActive(this.m_objFacilityRedDot, !NKCUIOfficeMapFront.m_bVisiting && this.m_NKCMinimapFacility.IsRedDotOn());
			NKCUtil.SetGameobjectActive(this.m_objRoomRedDot, !NKCUIOfficeMapFront.m_bVisiting && this.m_NKCMinimapRoom.IsRedDotOn());
			NKCUIOfficeUpsideMenu officeUpsideMenu = this.m_officeUpsideMenu;
			if (officeUpsideMenu != null)
			{
				officeUpsideMenu.SetRedDotNotify();
			}
			this.OnScreenResolutionChanged();
		}

		// Token: 0x06007E35 RID: 32309 RVA: 0x002A5278 File Offset: 0x002A3478
		public void SetMyOfficeData(NKMOfficeRoomTemplet reservationRoomTemplet = null)
		{
			this.OfficeData.ResetFriendUId();
			NKCUIOfficeMapFront.m_bVisiting = false;
			this.SetMinimapState(NKCUIOfficeMapFront.MinimapState.Main);
			this.m_NKCMinimapFacility.UpdateRoomStateAll();
			this.m_NKCMinimapRoom.UpdateRoomStateAll();
			bool flag = NKCContentManager.IsContentsUnlocked(ContentsType.OFFICE, 0, 0);
			NKCUtil.SetGameobjectActive(this.m_objDormitoryLock, !flag);
			bool flag2 = reservationRoomTemplet != null && reservationRoomTemplet.Type > NKMOfficeRoomTemplet.RoomType.Dorm;
			if (flag && !flag2)
			{
				if (this.m_tglRoom != null)
				{
					this.m_tglRoom.Select(false, true, false);
					this.m_tglRoom.Select(true, false, false);
				}
			}
			else if (this.m_tglFacility != null)
			{
				this.m_tglFacility.Select(false, true, false);
				this.m_tglFacility.Select(true, false, false);
			}
			NKCUtil.SetGameobjectActive(this.m_objFacilityRedDot, this.m_NKCMinimapFacility.IsRedDotOn());
			NKCUtil.SetGameobjectActive(this.m_objRoomRedDot, this.m_NKCMinimapRoom.IsRedDotOn());
			NKCUIOfficeUpsideMenu officeUpsideMenu = this.m_officeUpsideMenu;
			if (officeUpsideMenu == null)
			{
				return;
			}
			officeUpsideMenu.SetRedDotNotify();
		}

		// Token: 0x06007E36 RID: 32310 RVA: 0x002A5378 File Offset: 0x002A3578
		public void SetFriendOfficeData()
		{
			NKCUIOfficeMapFront.m_bVisiting = true;
			this.SetMinimapState(NKCUIOfficeMapFront.MinimapState.Visit);
			this.m_NKCMinimapRoom.UpdateRoomStateAll();
			if (this.m_tglRoom != null)
			{
				this.m_tglRoom.Select(false, true, false);
				this.m_tglRoom.Select(true, false, false);
			}
			NKCUIOfficeUpsideMenu officeUpsideMenu = this.m_officeUpsideMenu;
			if (officeUpsideMenu == null)
			{
				return;
			}
			officeUpsideMenu.SetRedDotNotify();
		}

		// Token: 0x06007E37 RID: 32311 RVA: 0x002A53DC File Offset: 0x002A35DC
		public override void OnBackButton()
		{
			if (this.m_eCurrentMinimapState == NKCUIOfficeMapFront.MinimapState.Edit)
			{
				this.SetMinimapState(NKCUIOfficeMapFront.MinimapState.Main);
				return;
			}
			base.OnBackButton();
			if (NKCUIOfficeMapFront.m_eReserveScenId == NKM_SCEN_ID.NSI_INVALID)
			{
				NKCScenManager scenManager = NKCScenManager.GetScenManager();
				if (scenManager != null)
				{
					scenManager.ScenChangeFade(NKM_SCEN_ID.NSI_HOME, false);
				}
			}
			else
			{
				NKCScenManager scenManager2 = NKCScenManager.GetScenManager();
				if (scenManager2 != null)
				{
					scenManager2.ScenChangeFade(NKCUIOfficeMapFront.m_eReserveScenId, false);
				}
			}
			NKCUIOfficeMapFront.m_eReserveScenId = NKM_SCEN_ID.NSI_INVALID;
		}

		// Token: 0x06007E38 RID: 32312 RVA: 0x002A5437 File Offset: 0x002A3637
		public IOfficeMinimap GetCurrentMinimap()
		{
			return this.m_currentMinimap;
		}

		// Token: 0x06007E39 RID: 32313 RVA: 0x002A543F File Offset: 0x002A363F
		public void UpdateSectionLockState(int sectionId)
		{
			this.SetSectionButtonInfo(this.m_eCurrentSectionType);
			this.MoveScrollToSection(sectionId);
		}

		// Token: 0x06007E3A RID: 32314 RVA: 0x002A5454 File Offset: 0x002A3654
		public static string GetDefaultRoomName(int roomId)
		{
			NKMOfficeRoomTemplet nkmofficeRoomTemplet = NKMOfficeRoomTemplet.Find(roomId);
			if (nkmofficeRoomTemplet != null)
			{
				return NKCStringTable.GetString(nkmofficeRoomTemplet.Name, false);
			}
			return "Room";
		}

		// Token: 0x06007E3B RID: 32315 RVA: 0x002A547D File Offset: 0x002A367D
		public static string GetSectionName(NKMOfficeSectionTemplet sectionTemplet)
		{
			if (sectionTemplet != null)
			{
				return NKCStringTable.GetString(sectionTemplet.SectionName, false);
			}
			return string.Format("Section {0}", sectionTemplet.SectionId);
		}

		// Token: 0x06007E3C RID: 32316 RVA: 0x002A54A4 File Offset: 0x002A36A4
		public void MoveMiniMap(Vector3 targetPosition, bool restrictScroll = true)
		{
			if (this.m_currentMinimap == null)
			{
				return;
			}
			ScrollRect scrollRect = this.m_currentMinimap.GetScrollRect();
			if (scrollRect == null)
			{
				return;
			}
			scrollRect.StopMovement();
			Vector3 position = scrollRect.content.position;
			Vector3 vector = new Vector3(position.x - targetPosition.x, position.y, position.z);
			if (restrictScroll)
			{
				Vector3 vector2 = NKCCamera.GetSubUICamera().WorldToScreenPoint(vector);
				float num = scrollRect.content.rect.width * NKCUIManager.FrontCanvas.scaleFactor;
				float num2 = vector2.x - scrollRect.content.pivot.x * num;
				float num3 = vector2.x + (1f - scrollRect.content.pivot.x) * num;
				if (num2 > 0f)
				{
					ref Vector3 ptr = NKCCamera.GetSubUICamera().ScreenToWorldPoint(new Vector3(num2, vector2.y, vector2.z));
					Vector3 vector3 = NKCCamera.GetSubUICamera().ScreenToWorldPoint(new Vector3(0f, vector2.y, vector2.z));
					float num4 = Mathf.Abs(ptr.x - vector3.x);
					vector.x -= num4;
				}
				else if (num3 < (float)Screen.width)
				{
					ref Vector3 ptr2 = NKCCamera.GetSubUICamera().ScreenToWorldPoint(new Vector3(num3, vector2.y, vector2.z));
					Vector3 vector4 = NKCCamera.GetSubUICamera().ScreenToWorldPoint(new Vector3((float)Screen.width, vector2.y, vector2.z));
					float num5 = Mathf.Abs(ptr2.x - vector4.x);
					vector.x += num5;
				}
			}
			else if (scrollRect.normalizedPosition.x > 0.5f)
			{
				this.m_currentMinimap.ExpandScrollRectRange();
			}
			scrollRect.content.DOKill(false);
			scrollRect.content.DOMoveX(vector.x, 0.5f, false).SetEase(Ease.OutQuint).SetDelay(0.1f);
		}

		// Token: 0x06007E3D RID: 32317 RVA: 0x002A56A4 File Offset: 0x002A38A4
		public void MoveMiniMap(float horizonralNormalizedValue, UnityAction onComplete)
		{
			if (this.m_currentMinimap == null)
			{
				return;
			}
			ScrollRect scrollRect = this.m_currentMinimap.GetScrollRect();
			if (scrollRect == null)
			{
				return;
			}
			scrollRect.StopMovement();
			scrollRect.DOKill(false);
			scrollRect.DOHorizontalNormalizedPos(horizonralNormalizedValue, 0.5f, false).SetEase(Ease.OutQuint).OnComplete(delegate
			{
				if (onComplete != null)
				{
					onComplete();
				}
			});
		}

		// Token: 0x06007E3E RID: 32318 RVA: 0x002A5711 File Offset: 0x002A3911
		public void RevertScrollRectRange()
		{
			if (this.m_currentMinimap != null)
			{
				this.m_currentMinimap.RevertScrollRectRange();
			}
		}

		// Token: 0x06007E3F RID: 32319 RVA: 0x002A5726 File Offset: 0x002A3926
		public void UpdateFactoryState()
		{
			if (this.m_eCurrentSectionType == NKCUIOfficeMapFront.SectionType.Facility)
			{
				this.m_currentMinimap.UpdateRoomState(NKMOfficeRoomTemplet.RoomType.Forge);
			}
			NKCUtil.SetGameobjectActive(this.m_objFacilityRedDot, this.m_NKCMinimapFacility.IsRedDotOn());
		}

		// Token: 0x06007E40 RID: 32320 RVA: 0x002A5754 File Offset: 0x002A3954
		public override bool OnHotkey(HotkeyEventType hotkey)
		{
			if (NKCUIOfficeMapFront.m_bVisiting)
			{
				return false;
			}
			if (hotkey - HotkeyEventType.PrevTab <= 1)
			{
				if (this.m_NKCMinimapFacility.GetGameObject().activeSelf)
				{
					this.m_tglRoom.Select(true, false, false);
				}
				else
				{
					this.m_tglFacility.Select(true, false, false);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06007E41 RID: 32321 RVA: 0x002A57A5 File Offset: 0x002A39A5
		public override void OnScreenResolutionChanged()
		{
			base.OnScreenResolutionChanged();
			this.m_NKCMinimap.CalcCamMoveBoundRect();
			this.m_currentMinimap.UpdateCameraPosition();
		}

		// Token: 0x06007E42 RID: 32322 RVA: 0x002A57C3 File Offset: 0x002A39C3
		public void SelectFacilityTab()
		{
			if (!NKCUIOfficeMapFront.m_bVisiting)
			{
				this.SetMinimapState(NKCUIOfficeMapFront.MinimapState.Main);
			}
			if (this.m_tglFacility == null)
			{
				return;
			}
			this.m_tglFacility.Select(false, true, false);
			this.m_tglFacility.Select(true, false, false);
		}

		// Token: 0x06007E43 RID: 32323 RVA: 0x002A5800 File Offset: 0x002A3A00
		public void SelectRoomTab()
		{
			if (!NKCUIOfficeMapFront.m_bVisiting)
			{
				this.SetMinimapState(NKCUIOfficeMapFront.MinimapState.Main);
			}
			if (this.m_tglRoom == null)
			{
				return;
			}
			this.m_tglRoom.Select(false, true, false);
			this.m_tglRoom.Select(true, false, false);
		}

		// Token: 0x06007E44 RID: 32324 RVA: 0x002A583D File Offset: 0x002A3A3D
		public static ContentsType GetFacilityContentType(NKMOfficeRoomTemplet.RoomType facilityType)
		{
			switch (facilityType)
			{
			case NKMOfficeRoomTemplet.RoomType.Lab:
				return ContentsType.BASE_LAB;
			case NKMOfficeRoomTemplet.RoomType.Forge:
				return ContentsType.BASE_FACTORY;
			case NKMOfficeRoomTemplet.RoomType.Hangar:
				return ContentsType.BASE_HANGAR;
			case NKMOfficeRoomTemplet.RoomType.CEO:
				return ContentsType.BASE_PERSONNAL;
			case NKMOfficeRoomTemplet.RoomType.Terrabrain:
				return ContentsType.TERRA_BRAIN;
			default:
				return ContentsType.None;
			}
		}

		// Token: 0x06007E45 RID: 32325 RVA: 0x002A5870 File Offset: 0x002A3A70
		private bool IsSectionUnlocked(NKMOfficeSectionTemplet sectionTemplet, ref ContentsType lockedContentType, ref bool purchaseEnable, ref bool isPurchased)
		{
			if (sectionTemplet == null)
			{
				return false;
			}
			bool flag = false;
			purchaseEnable = true;
			isPurchased = true;
			lockedContentType = ContentsType.None;
			bool result;
			if (sectionTemplet.IsFacility)
			{
				foreach (KeyValuePair<int, NKMOfficeRoomTemplet> keyValuePair in sectionTemplet.Rooms)
				{
					NKMOfficeRoomTemplet value = keyValuePair.Value;
					if (value != null)
					{
						ContentsType facilityContentType = NKCUIOfficeMapFront.GetFacilityContentType(value.Type);
						flag |= NKCContentManager.IsContentsUnlocked(facilityContentType, 0, 0);
						if (!flag && lockedContentType == ContentsType.None)
						{
							lockedContentType = facilityContentType;
						}
					}
				}
				result = flag;
			}
			else
			{
				flag = NKCContentManager.IsContentsUnlocked(ContentsType.OFFICE, 0, 0);
				if (!flag)
				{
					lockedContentType = ContentsType.OFFICE;
				}
				if (sectionTemplet.HasUnlockType)
				{
					NKMUserData cNKMUserData = NKCScenManager.CurrentUserData();
					UnlockInfo unlockInfo = new UnlockInfo(sectionTemplet.UnlockReqType, sectionTemplet.UnlockReqValue);
					purchaseEnable = NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, unlockInfo, true);
				}
				isPurchased = this.m_NKCOfficeData.IsOpenedSection(sectionTemplet.SectionId);
				result = (flag & purchaseEnable & isPurchased);
			}
			return result;
		}

		// Token: 0x06007E46 RID: 32326 RVA: 0x002A5964 File Offset: 0x002A3B64
		private bool IsSectionVisitUnlocked(NKMOfficeSectionTemplet sectionTemplet)
		{
			return sectionTemplet != null && this.m_NKCOfficeData.IsOpenedSection(sectionTemplet.SectionId);
		}

		// Token: 0x06007E47 RID: 32327 RVA: 0x002A597C File Offset: 0x002A3B7C
		private void SetMinimapState(NKCUIOfficeMapFront.MinimapState minimapState)
		{
			this.m_eCurrentMinimapState = minimapState;
			NKCUtil.SetGameobjectActive(this.m_objMainUI, false);
			NKCUtil.SetGameobjectActive(this.m_objMemberEditUI, false);
			NKCUtil.SetGameobjectActive(this.m_objVisitorUI, false);
			switch (minimapState)
			{
			case NKCUIOfficeMapFront.MinimapState.Main:
				NKCUtil.SetGameobjectActive(this.m_objMainUI, true);
				this.m_NKCMinimap.m_NKCUIMinimapRoom.UpdateRoomFxAll();
				return;
			case NKCUIOfficeMapFront.MinimapState.Edit:
				NKCUtil.SetGameobjectActive(this.m_objMemberEditUI, true);
				this.m_NKCMinimap.m_NKCUIMinimapRoom.UpdateRoomFxAll();
				return;
			case NKCUIOfficeMapFront.MinimapState.Visit:
				NKCUtil.SetGameobjectActive(this.m_objVisitorUI, true);
				return;
			default:
				return;
			}
		}

		// Token: 0x06007E48 RID: 32328 RVA: 0x002A5A10 File Offset: 0x002A3C10
		private void SetSectionButtonInfo(NKCUIOfficeMapFront.SectionType sectionType)
		{
			if (this.m_csbtnSectionArray == null)
			{
				return;
			}
			int num = this.m_csbtnSectionArray.Length;
			for (int i = 0; i < num; i++)
			{
				if (this.m_dicSectionId[sectionType].Count <= i)
				{
					NKCUtil.SetGameobjectActive(this.m_csbtnSectionArray[i].m_csbtnButton, false);
				}
				else
				{
					int num2 = this.m_dicSectionId[sectionType][i];
					NKCUtil.SetGameobjectActive(this.m_csbtnSectionArray[i].m_csbtnButton, true);
					NKMOfficeSectionTemplet sectionTemplet = NKMOfficeSectionTemplet.Find(num2);
					if (sectionTemplet == null)
					{
						NKCUtil.SetLabelText(this.m_csbtnSectionArray[i].m_lbLockText, "Error");
						this.m_csbtnSectionArray[i].SetLock(num2, true);
					}
					else
					{
						NKCUtil.SetLabelText(this.m_csbtnSectionArray[i].m_lbNormalText, NKCUIOfficeMapFront.GetSectionName(sectionTemplet));
						NKCUtil.SetLabelText(this.m_csbtnSectionArray[i].m_lbLockText, NKCUIOfficeMapFront.GetSectionName(sectionTemplet));
						bool flag = true;
						bool flag2 = true;
						ContentsType contentsType = ContentsType.None;
						bool flag3 = this.IsSectionUnlocked(sectionTemplet, ref contentsType, ref flag, ref flag2);
						NKCUtil.SetButtonClickDelegate(this.m_csbtnSectionArray[i].m_csbtnButton, delegate()
						{
							this.OnBtnArea(sectionTemplet.SectionId);
						});
						this.m_csbtnSectionArray[i].SetLock(num2, !flag3);
					}
				}
			}
		}

		// Token: 0x06007E49 RID: 32329 RVA: 0x002A5B68 File Offset: 0x002A3D68
		private void SetSectionButtonVisitInfo()
		{
			if (this.m_csbtnVisitorSectionArray == null)
			{
				return;
			}
			int num = this.m_csbtnVisitorSectionArray.Length;
			for (int i = 0; i < num; i++)
			{
				if (this.m_dicSectionId[NKCUIOfficeMapFront.SectionType.Room].Count <= i)
				{
					NKCUtil.SetGameobjectActive(this.m_csbtnVisitorSectionArray[i].m_csbtnButton, false);
				}
				else
				{
					int num2 = this.m_dicSectionId[NKCUIOfficeMapFront.SectionType.Room][i];
					NKCUtil.SetGameobjectActive(this.m_csbtnVisitorSectionArray[i].m_csbtnButton, true);
					NKMOfficeSectionTemplet sectionTemplet = NKMOfficeSectionTemplet.Find(num2);
					if (sectionTemplet == null)
					{
						NKCUtil.SetLabelText(this.m_csbtnVisitorSectionArray[i].m_lbLockText, "Error");
						this.m_csbtnVisitorSectionArray[i].SetLock(num2, true);
					}
					else
					{
						NKCUtil.SetLabelText(this.m_csbtnVisitorSectionArray[i].m_lbNormalText, NKCUIOfficeMapFront.GetSectionName(sectionTemplet));
						NKCUtil.SetLabelText(this.m_csbtnVisitorSectionArray[i].m_lbLockText, NKCUIOfficeMapFront.GetSectionName(sectionTemplet));
						bool flag = this.IsSectionVisitUnlocked(sectionTemplet);
						NKCUtil.SetButtonClickDelegate(this.m_csbtnVisitorSectionArray[i].m_csbtnButton, delegate()
						{
							this.OnBtnArea(sectionTemplet.SectionId);
						});
						this.m_csbtnVisitorSectionArray[i].SetLock(num2, !flag);
					}
				}
			}
		}

		// Token: 0x06007E4A RID: 32330 RVA: 0x002A5CB0 File Offset: 0x002A3EB0
		private bool IsDormSectionRedDotOn()
		{
			if (NKCUIOfficeMapFront.m_bVisiting)
			{
				return false;
			}
			bool result = false;
			int count = this.m_dicSectionId[NKCUIOfficeMapFront.SectionType.Room].Count;
			for (int i = 0; i < count; i++)
			{
				NKMOfficeSectionTemplet nkmofficeSectionTemplet = NKMOfficeSectionTemplet.Find(this.m_dicSectionId[NKCUIOfficeMapFront.SectionType.Room][i]);
				if (nkmofficeSectionTemplet != null)
				{
					bool flag = true;
					bool flag2 = true;
					ContentsType contentsType = ContentsType.None;
					this.IsSectionUnlocked(nkmofficeSectionTemplet, ref contentsType, ref flag, ref flag2);
					if (flag && !flag2)
					{
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06007E4B RID: 32331 RVA: 0x002A5D24 File Offset: 0x002A3F24
		private void LockRoomsInSection()
		{
			int num = this.m_csbtnSectionArray.Length;
			for (int i = 0; i < num; i++)
			{
				if (this.m_csbtnSectionArray[i].IsLocked())
				{
					this.m_currentMinimap.LockRoomsInSection(this.m_csbtnSectionArray[i].m_iSectionId);
				}
			}
		}

		// Token: 0x06007E4C RID: 32332 RVA: 0x002A5D70 File Offset: 0x002A3F70
		private void MoveScrollToSection(int sectionId)
		{
			IOfficeMinimap currentMinimap = this.m_currentMinimap;
			Transform transform = (currentMinimap != null) ? currentMinimap.GetScrollTargetTileTransform(sectionId) : null;
			if (transform == null)
			{
				return;
			}
			this.MoveMiniMap(transform.position, true);
		}

		// Token: 0x06007E4D RID: 32333 RVA: 0x002A5DA8 File Offset: 0x002A3FA8
		private void OnBtnArea(int sectionId)
		{
			NKMOfficeSectionTemplet nkmofficeSectionTemplet = NKMOfficeSectionTemplet.Find(sectionId);
			if (nkmofficeSectionTemplet == null)
			{
				return;
			}
			bool flag = true;
			bool flag2 = true;
			ContentsType contentsType = ContentsType.None;
			bool flag3;
			if (!NKCUIOfficeMapFront.m_bVisiting)
			{
				flag3 = this.IsSectionUnlocked(nkmofficeSectionTemplet, ref contentsType, ref flag, ref flag2);
			}
			else
			{
				flag3 = this.IsSectionVisitUnlocked(nkmofficeSectionTemplet);
			}
			if (!flag3)
			{
				if (!NKCUIOfficeMapFront.m_bVisiting)
				{
					if (contentsType != ContentsType.None)
					{
						NKCContentManager.ShowLockedMessagePopup(contentsType, 0);
						return;
					}
					if (!flag)
					{
						if (nkmofficeSectionTemplet.HasUnlockType)
						{
							UnlockInfo unlockInfo = new UnlockInfo(nkmofficeSectionTemplet.UnlockReqType, nkmofficeSectionTemplet.UnlockReqValue);
							string message = NKCContentManager.MakeUnlockConditionString(unlockInfo, false);
							NKCUIManager.NKCPopupMessage.Open(new PopupMessage(message, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
							return;
						}
					}
					else if (!flag2)
					{
						string content = string.Format(NKCUtilString.GET_STRING_OFFICE_PURCHASE_SECTION, NKCStringTable.GetString(nkmofficeSectionTemplet.SectionName, false));
						if (nkmofficeSectionTemplet.PriceItem == null)
						{
							NKCPopupResourceConfirmBox.Instance.OpenForConfirm(NKCUtilString.GET_STRING_UNLOCK, content, delegate
							{
								NKCPacketSender.Send_NKMPacket_OFFICE_OPEN_SECTION_REQ(sectionId);
							}, null, false);
							return;
						}
						NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_UNLOCK, content, nkmofficeSectionTemplet.PriceItem.m_ItemMiscID, nkmofficeSectionTemplet.Price, delegate()
						{
							NKCPacketSender.Send_NKMPacket_OFFICE_OPEN_SECTION_REQ(sectionId);
						}, null, false);
					}
				}
				return;
			}
			this.MoveScrollToSection(sectionId);
		}

		// Token: 0x06007E4E RID: 32334 RVA: 0x002A5EE4 File Offset: 0x002A40E4
		private void OnToggleRoom(bool value)
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.OFFICE, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.OFFICE, 0);
				this.m_tglFacility.Select(true, true, false);
				return;
			}
			this.m_NKCMinimap.SetActiveRoom(value);
			NKCUtil.SetGameobjectActive(this.m_csbtnMemberEdit, value);
			this.m_currentMinimap = this.m_NKCMinimapRoom;
			this.m_eCurrentSectionType = NKCUIOfficeMapFront.SectionType.Room;
			if (this.m_eCurrentMinimapState == NKCUIOfficeMapFront.MinimapState.Visit)
			{
				this.SetSectionButtonVisitInfo();
			}
			else
			{
				this.SetSectionButtonInfo(NKCUIOfficeMapFront.SectionType.Room);
			}
			NKCUIOfficeUpsideMenu officeUpsideMenu = this.m_officeUpsideMenu;
			if (officeUpsideMenu != null)
			{
				officeUpsideMenu.SetState(NKCUIOfficeUpsideMenu.MenuState.MinimapRoom, null);
			}
			NKCTutorialManager.TutorialRequired(TutorialPoint.OfficeMiniMap, true);
		}

		// Token: 0x06007E4F RID: 32335 RVA: 0x002A5F74 File Offset: 0x002A4174
		private void OnToggleFacility(bool value)
		{
			this.m_NKCMinimap.SetActiveFacility(value);
			NKCUtil.SetGameobjectActive(this.m_csbtnMemberEdit, !value);
			this.m_currentMinimap = this.m_NKCMinimapFacility;
			this.m_eCurrentSectionType = NKCUIOfficeMapFront.SectionType.Facility;
			this.SetSectionButtonInfo(NKCUIOfficeMapFront.SectionType.Facility);
			NKCUIOfficeUpsideMenu officeUpsideMenu = this.m_officeUpsideMenu;
			if (officeUpsideMenu != null)
			{
				officeUpsideMenu.SetState(NKCUIOfficeUpsideMenu.MenuState.MinimapFacility, null);
			}
			NKCTutorialManager.TutorialRequired(TutorialPoint.OfficeBaseMiniMap, true);
		}

		// Token: 0x06007E50 RID: 32336 RVA: 0x002A5FD2 File Offset: 0x002A41D2
		private void OnBtnMemberEdit()
		{
			this.SetMinimapState(NKCUIOfficeMapFront.MinimapState.Edit);
			this.m_NKCMinimap.m_NKCUIMinimapRoom.UpdateRoomFxAll();
		}

		// Token: 0x04006AFA RID: 27386
		private const string ASSET_BUNDLE_NAME = "ab_ui_office";

		// Token: 0x04006AFB RID: 27387
		private const string UI_ASSET_NAME = "AB_UI_OFFICE_MAP_UI_FRONT";

		// Token: 0x04006AFC RID: 27388
		private static NKCUIManager.LoadedUIData s_LoadedUIData;

		// Token: 0x04006AFD RID: 27389
		public bool IgnoreRoomLockState;

		// Token: 0x04006AFE RID: 27390
		[Header("UI 타입 Root")]
		public GameObject m_objMainUI;

		// Token: 0x04006AFF RID: 27391
		public GameObject m_objMemberEditUI;

		// Token: 0x04006B00 RID: 27392
		public GameObject m_objVisitorUI;

		// Token: 0x04006B01 RID: 27393
		[Space]
		public GameObject m_objRoomRedDot;

		// Token: 0x04006B02 RID: 27394
		public GameObject m_objFacilityRedDot;

		// Token: 0x04006B03 RID: 27395
		public GameObject m_objDormitoryLock;

		// Token: 0x04006B04 RID: 27396
		public NKCUIComToggle m_tglRoom;

		// Token: 0x04006B05 RID: 27397
		public NKCUIComToggle m_tglFacility;

		// Token: 0x04006B06 RID: 27398
		public NKCUIComStateButton m_csbtnMemberEdit;

		// Token: 0x04006B07 RID: 27399
		public NKCUIComMapSectionButton[] m_csbtnSectionArray;

		// Token: 0x04006B08 RID: 27400
		public NKCUIComMapSectionButton[] m_csbtnVisitorSectionArray;

		// Token: 0x04006B09 RID: 27401
		public NKCUIOfficeUpsideMenu m_officeUpsideMenu;

		// Token: 0x04006B0A RID: 27402
		private NKCUIOfficeMapFront.MinimapState m_eCurrentMinimapState;

		// Token: 0x04006B0B RID: 27403
		private NKCUIOfficeMapFront.SectionType m_eCurrentSectionType;

		// Token: 0x04006B0C RID: 27404
		private NKCOfficeData m_NKCOfficeData;

		// Token: 0x04006B0D RID: 27405
		private NKCUIOfficeMinimap m_NKCMinimap;

		// Token: 0x04006B0E RID: 27406
		private IOfficeMinimap m_NKCMinimapRoom;

		// Token: 0x04006B0F RID: 27407
		private IOfficeMinimap m_NKCMinimapFacility;

		// Token: 0x04006B10 RID: 27408
		private IOfficeMinimap m_currentMinimap;

		// Token: 0x04006B11 RID: 27409
		private Dictionary<NKCUIOfficeMapFront.SectionType, List<int>> m_dicSectionId;

		// Token: 0x04006B12 RID: 27410
		private static bool m_bVisiting;

		// Token: 0x04006B13 RID: 27411
		private const bool IgnoreSuperUser = true;

		// Token: 0x04006B14 RID: 27412
		private static NKM_SCEN_ID m_eReserveScenId;

		// Token: 0x0200186B RID: 6251
		public enum MinimapState
		{
			// Token: 0x0400A8EC RID: 43244
			Main,
			// Token: 0x0400A8ED RID: 43245
			Edit,
			// Token: 0x0400A8EE RID: 43246
			Visit
		}

		// Token: 0x0200186C RID: 6252
		public enum SectionType
		{
			// Token: 0x0400A8F0 RID: 43248
			Room,
			// Token: 0x0400A8F1 RID: 43249
			Facility
		}
	}
}
