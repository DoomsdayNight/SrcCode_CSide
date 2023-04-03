using System;
using ClientPacket.Office;
using NKC.FX;
using NKC.Office;
using NKM;
using NKM.Templet;
using NKM.Templet.Office;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Office
{
	// Token: 0x02000AEE RID: 2798
	public class NKCUIComOfficeMapTileRoom : MonoBehaviour
	{
		// Token: 0x170014CB RID: 5323
		// (get) Token: 0x06007D91 RID: 32145 RVA: 0x002A1677 File Offset: 0x0029F877
		// (set) Token: 0x06007D92 RID: 32146 RVA: 0x002A167F File Offset: 0x0029F87F
		public int m_iSectionId { get; private set; }

		// Token: 0x170014CC RID: 5324
		// (get) Token: 0x06007D93 RID: 32147 RVA: 0x002A1688 File Offset: 0x0029F888
		public NKMOfficeRoomTemplet.RoomType RoomType
		{
			get
			{
				return this.m_eRoomType;
			}
		}

		// Token: 0x170014CD RID: 5325
		// (get) Token: 0x06007D94 RID: 32148 RVA: 0x002A1690 File Offset: 0x0029F890
		public RectTransform RectTransformTileShape
		{
			get
			{
				return this.m_rtBgShape;
			}
		}

		// Token: 0x06007D95 RID: 32149 RVA: 0x002A1698 File Offset: 0x0029F898
		public void Init()
		{
			this.m_eRoomType = NKMOfficeRoomTemplet.RoomType.Dorm;
			NKMOfficeRoomTemplet nkmofficeRoomTemplet = NKMOfficeRoomTemplet.Find(this.m_iRoomId);
			if (nkmofficeRoomTemplet != null)
			{
				this.m_eRoomType = nkmofficeRoomTemplet.Type;
			}
			if (this.m_animator == null)
			{
				this.m_animator = base.GetComponent<Animator>();
			}
			Animator animator = this.m_animator;
			if (animator != null)
			{
				animator.SetInteger("State", 0);
			}
			NKC_FX_PTC_OFFICE_MAP_TILE[] componentsInChildren = base.GetComponentsInChildren<NKC_FX_PTC_OFFICE_MAP_TILE>(true);
			if (componentsInChildren != null)
			{
				RectTransform component = base.GetComponent<RectTransform>();
				int num = componentsInChildren.Length;
				for (int i = 0; i < num; i++)
				{
					componentsInChildren[i].Reference = component;
				}
			}
			this.m_unitBlackSprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_office_sprite", "AB_UI_OFFICE_MEMBER_NONE", false);
			NKCUtil.SetButtonClickDelegate(this.m_csbtnTileButton, new UnityAction(this.OnBtnTile));
			NKCUtil.SetGameobjectActive(this.m_objRedDot, false);
		}

		// Token: 0x06007D96 RID: 32150 RVA: 0x002A1764 File Offset: 0x0029F964
		public void UpdateRoomInfo(NKMOfficeRoom officeRoom)
		{
			NKMOfficeRoomTemplet roomTemplet = NKMOfficeRoomTemplet.Find(this.m_iRoomId);
			this.SetRoomName(officeRoom, roomTemplet);
			this.SetUnitAssignInfo(officeRoom, roomTemplet);
		}

		// Token: 0x06007D97 RID: 32151 RVA: 0x002A1790 File Offset: 0x0029F990
		public void LockRoom()
		{
			if (this.m_eRoomState == NKCUIComOfficeMapTileRoom.RoomState.LOCK)
			{
				return;
			}
			this.m_eRoomState = NKCUIComOfficeMapTileRoom.RoomState.LOCK;
			NKCUIOfficeMapFront instance = NKCUIOfficeMapFront.GetInstance();
			NKMOfficeRoom officeRoom = (instance != null) ? instance.OfficeData.GetOfficeRoom(this.m_iRoomId) : null;
			this.SetRoomInfo(this.m_eRoomState, officeRoom);
		}

		// Token: 0x06007D98 RID: 32152 RVA: 0x002A17D8 File Offset: 0x0029F9D8
		public void UpdateFxState()
		{
			NKCUIOfficeMapFront instance = NKCUIOfficeMapFront.GetInstance();
			if (this.IsAccessableRoom() && instance.CurrentMinimapState == NKCUIOfficeMapFront.MinimapState.Edit)
			{
				Animator animator = this.m_animator;
				if (animator == null)
				{
					return;
				}
				animator.SetInteger("State", 1);
				return;
			}
			else
			{
				Animator animator2 = this.m_animator;
				if (animator2 == null)
				{
					return;
				}
				animator2.SetInteger("State", 0);
				return;
			}
		}

		// Token: 0x06007D99 RID: 32153 RVA: 0x002A182C File Offset: 0x0029FA2C
		public void UpdateRoomState()
		{
			NKMOfficeRoomTemplet nkmofficeRoomTemplet = NKMOfficeRoomTemplet.Find(this.m_iRoomId);
			NKCUIOfficeMapFront instance = NKCUIOfficeMapFront.GetInstance();
			if (instance == null)
			{
				return;
			}
			if (nkmofficeRoomTemplet != null)
			{
				this.m_iSectionId = nkmofficeRoomTemplet.SectionId;
			}
			bool visiting = NKCUIOfficeMapFront.GetInstance().Visiting;
			this.m_eRoomState = NKCUIComOfficeMapTileRoom.RoomState.NORMAL;
			if (!visiting && !NKCContentManager.IsContentsUnlocked(ContentsType.OFFICE, 0, 0))
			{
				this.m_eRoomState = NKCUIComOfficeMapTileRoom.RoomState.LOCK;
			}
			if (nkmofficeRoomTemplet == null)
			{
				this.m_eRoomState = NKCUIComOfficeMapTileRoom.RoomState.LOCK;
			}
			if (this.m_eRoomState == NKCUIComOfficeMapTileRoom.RoomState.NORMAL && !instance.OfficeData.IsOpenedSection(this.m_iSectionId))
			{
				this.m_eRoomState = NKCUIComOfficeMapTileRoom.RoomState.LOCK;
			}
			if (this.m_eRoomState == NKCUIComOfficeMapTileRoom.RoomState.NORMAL && !instance.OfficeData.IsOpenedRoom(this.m_iRoomId))
			{
				if (!visiting)
				{
					this.m_eRoomState = NKCUIComOfficeMapTileRoom.RoomState.NEED_PURCHASE;
					if (nkmofficeRoomTemplet.HasUnlockType)
					{
						UnlockInfo unlockInfo = new UnlockInfo(nkmofficeRoomTemplet.UnlockReqType, nkmofficeRoomTemplet.UnlockReqValue);
						if (!NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), unlockInfo, this.IgnoreSuperUser))
						{
							this.m_eRoomState = NKCUIComOfficeMapTileRoom.RoomState.CANNOT_PURCHASE;
						}
					}
				}
				else
				{
					this.m_eRoomState = NKCUIComOfficeMapTileRoom.RoomState.LOCK;
				}
			}
			if (this.m_eRoomState == NKCUIComOfficeMapTileRoom.RoomState.LOCK && visiting)
			{
				this.m_eRoomState = NKCUIComOfficeMapTileRoom.RoomState.NO_SIGNAL;
			}
			if (this.m_eRoomState == NKCUIComOfficeMapTileRoom.RoomState.NORMAL && this.m_bIsLobby)
			{
				this.m_eRoomState = NKCUIComOfficeMapTileRoom.RoomState.LOBBY;
			}
			NKCUIOfficeMapFront instance2 = NKCUIOfficeMapFront.GetInstance();
			NKMOfficeRoom officeRoom = (instance2 != null) ? instance2.OfficeData.GetOfficeRoom(this.m_iRoomId) : null;
			this.SetRoomInfo(this.m_eRoomState, officeRoom);
			this.UpdateFxState();
		}

		// Token: 0x06007D9A RID: 32154 RVA: 0x002A1978 File Offset: 0x0029FB78
		public void UpdateRedDot()
		{
			if (!this.IsAccessableRoom())
			{
				NKCUtil.SetGameobjectActive(this.m_objRedDot, false);
				return;
			}
			if (this.m_bIsLobby)
			{
				bool flag = false;
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				if (nkmuserData != null)
				{
					flag = NKCAlarmManager.CheckOfficeCommunityNotify(nkmuserData);
				}
				NKCUtil.SetGameobjectActive(this.m_objRedDot, this.LoyaltyFullUnitExist() || flag);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objRedDot, this.LoyaltyFullUnitExist());
		}

		// Token: 0x06007D9B RID: 32155 RVA: 0x002A19DC File Offset: 0x0029FBDC
		private bool LoyaltyFullUnitExist()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return false;
			}
			NKMOfficeRoom officeRoom = nkmuserData.OfficeData.GetOfficeRoom(this.m_iRoomId);
			if (officeRoom == null)
			{
				return false;
			}
			foreach (long unitUid in officeRoom.unitUids)
			{
				NKMUnitData unitFromUID = nkmuserData.m_ArmyData.GetUnitFromUID(unitUid);
				if (unitFromUID != null && unitFromUID.CheckOfficeRoomHeartFull())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06007D9C RID: 32156 RVA: 0x002A1A70 File Offset: 0x0029FC70
		private bool IsAccessableRoom()
		{
			return this.m_eRoomState == NKCUIComOfficeMapTileRoom.RoomState.LOBBY || this.m_eRoomState == NKCUIComOfficeMapTileRoom.RoomState.NORMAL;
		}

		// Token: 0x06007D9D RID: 32157 RVA: 0x002A1A88 File Offset: 0x0029FC88
		private void HideRoomObjectRootAll()
		{
			NKCUtil.SetGameobjectActive(this.m_roomInfo.m_objRoot, false);
			NKCUtil.SetGameobjectActive(this.m_lobbyInfo.m_objRoot, false);
			NKCUtil.SetGameobjectActive(this.m_roomLock.m_objRoot, false);
			NKCUtil.SetGameobjectActive(this.m_noSignal.m_objRoot, false);
		}

		// Token: 0x06007D9E RID: 32158 RVA: 0x002A1ADC File Offset: 0x0029FCDC
		private void SetRoomInfo(NKCUIComOfficeMapTileRoom.RoomState roomState, NKMOfficeRoom officeRoom)
		{
			this.HideRoomObjectRootAll();
			this.m_currentRoomNameText = null;
			this.m_currentUnitArray = null;
			this.m_currentUnitCountText = null;
			NKMOfficeRoomTemplet nkmofficeRoomTemplet = NKMOfficeRoomTemplet.Find(this.m_iRoomId);
			switch (roomState)
			{
			case NKCUIComOfficeMapTileRoom.RoomState.NORMAL:
				this.m_currentRoomNameText = this.m_roomInfo.m_lbRoomName;
				this.m_currentUnitArray = this.m_imgUnitDormArray;
				this.m_currentUnitCountText = this.m_lbUnitDormCount;
				this.SetTileColor(this.m_roomInfo);
				NKCUtil.SetGameobjectActive(this.m_roomInfo.m_objRoot, true);
				break;
			case NKCUIComOfficeMapTileRoom.RoomState.LOBBY:
				this.m_currentRoomNameText = this.m_lobbyInfo.m_lbRoomName;
				this.m_currentUnitArray = this.m_imgUnitLobbyArray;
				this.m_currentUnitCountText = this.m_lbUnitLobbyCount;
				this.SetTileColor(this.m_lobbyInfo);
				NKCUtil.SetGameobjectActive(this.m_lobbyInfo.m_objRoot, true);
				break;
			case NKCUIComOfficeMapTileRoom.RoomState.LOCK:
			case NKCUIComOfficeMapTileRoom.RoomState.CANNOT_PURCHASE:
			case NKCUIComOfficeMapTileRoom.RoomState.NEED_PURCHASE:
				this.m_currentRoomNameText = this.m_roomLock.m_lbRoomName;
				this.SetTileColor(this.m_roomLock);
				NKCUtil.SetGameobjectActive(this.m_roomLock.m_objRoot, true);
				if (nkmofficeRoomTemplet != null && nkmofficeRoomTemplet.PriceItem != null)
				{
					NKCUtil.SetImageSprite(this.m_imgUnlockItemIcon, NKCResourceUtility.GetOrLoadMiscItemIcon(nkmofficeRoomTemplet.PriceItem.m_ItemMiscID), false);
					NKCUtil.SetLabelText(this.m_lbUnlockPriceCount, string.Format("{0:#,##0}", nkmofficeRoomTemplet.Price));
				}
				else
				{
					NKCUtil.SetLabelText(this.m_lbUnlockPriceCount, string.Format("{0:#,##0}", 0));
				}
				break;
			case NKCUIComOfficeMapTileRoom.RoomState.NO_SIGNAL:
				this.SetTileColor(this.m_noSignal);
				NKCUtil.SetGameobjectActive(this.m_noSignal.m_objRoot, true);
				break;
			}
			this.SetRoomName(officeRoom, nkmofficeRoomTemplet);
			this.SetUnitAssignInfo(officeRoom, nkmofficeRoomTemplet);
		}

		// Token: 0x06007D9F RID: 32159 RVA: 0x002A1C8A File Offset: 0x0029FE8A
		private void SetTileColor(NKCUIComOfficeMapTileRoom.RoomObject roomObject)
		{
			NKCUtil.SetImageColor(this.m_imgBg, roomObject.m_colBg);
			NKCUtil.SetImageColor(this.m_imgGlow, roomObject.m_colGlow);
			NKCUtil.SetImageColor(this.m_imgStroke, roomObject.m_colStroke);
		}

		// Token: 0x06007DA0 RID: 32160 RVA: 0x002A1CBF File Offset: 0x0029FEBF
		private void SetRoomName(NKMOfficeRoom officeRoom, NKMOfficeRoomTemplet roomTemplet)
		{
			if (officeRoom != null && !string.IsNullOrEmpty(officeRoom.name))
			{
				NKCUtil.SetLabelText(this.m_currentRoomNameText, officeRoom.name);
				return;
			}
			NKCUtil.SetLabelText(this.m_currentRoomNameText, NKCUIOfficeMapFront.GetDefaultRoomName(this.m_iRoomId));
		}

		// Token: 0x06007DA1 RID: 32161 RVA: 0x002A1CFC File Offset: 0x0029FEFC
		private void SetUnitAssignInfo(NKMOfficeRoom officeRoom, NKMOfficeRoomTemplet roomTemplet)
		{
			if (this.m_currentUnitArray == null || officeRoom == null || roomTemplet == null)
			{
				return;
			}
			if (!NKCUIOfficeMapFront.GetInstance().Visiting)
			{
				int num = this.m_currentUnitArray.Length;
				for (int i = 0; i < num; i++)
				{
					bool flag = i < roomTemplet.UnitLimitCount;
					if (this.m_currentUnitArray[i] != null)
					{
						NKCUtil.SetGameobjectActive(this.m_currentUnitArray[i].gameObject, flag);
					}
					if (flag)
					{
						if (i < officeRoom.unitUids.Count)
						{
							NKCUtil.SetImageSprite(this.m_currentUnitArray[i], this.GetUnitFaceSprite(officeRoom.unitUids[i]), false);
						}
						else
						{
							NKCUtil.SetImageSprite(this.m_currentUnitArray[i], this.m_unitBlackSprite, false);
						}
					}
				}
			}
			else
			{
				int num2 = this.m_currentUnitArray.Length;
				for (int j = 0; j < num2; j++)
				{
					bool flag2 = j < roomTemplet.UnitLimitCount;
					if (this.m_currentUnitArray[j] != null)
					{
						NKCUtil.SetGameobjectActive(this.m_currentUnitArray[j].gameObject, flag2);
					}
					if (flag2)
					{
						if (j < officeRoom.unitUids.Count)
						{
							NKCUtil.SetImageSprite(this.m_currentUnitArray[j], this.GetFriendUnitFaceSprite(officeRoom.unitUids[j]), false);
						}
						else
						{
							NKCUtil.SetImageSprite(this.m_currentUnitArray[j], this.m_unitBlackSprite, false);
						}
					}
				}
			}
			NKCUtil.SetLabelText(this.m_currentUnitCountText, string.Format("{0}/{1}", officeRoom.unitUids.Count, roomTemplet.UnitLimitCount));
		}

		// Token: 0x06007DA2 RID: 32162 RVA: 0x002A1E84 File Offset: 0x002A0084
		private Sprite GetUnitFaceSprite(long unitUId)
		{
			NKCScenManager scenManager = NKCScenManager.GetScenManager();
			NKMUnitData nkmunitData;
			if (scenManager == null)
			{
				nkmunitData = null;
			}
			else
			{
				NKMUserData myUserData = scenManager.GetMyUserData();
				nkmunitData = ((myUserData != null) ? myUserData.m_ArmyData.GetUnitOrTrophyFromUID(unitUId) : null);
			}
			NKMUnitData nkmunitData2 = nkmunitData;
			if (nkmunitData2 != null)
			{
				Sprite orLoadMinimapFaceIcon = NKCResourceUtility.GetOrLoadMinimapFaceIcon(NKMUnitManager.GetUnitTempletBase(nkmunitData2.m_UnitID), false);
				if (orLoadMinimapFaceIcon != null)
				{
					return orLoadMinimapFaceIcon;
				}
			}
			return this.m_unitBlackSprite;
		}

		// Token: 0x06007DA3 RID: 32163 RVA: 0x002A1EDC File Offset: 0x002A00DC
		private Sprite GetFriendUnitFaceSprite(long unitUId)
		{
			NKCScenManager scenManager = NKCScenManager.GetScenManager();
			NKCOfficeData nkcofficeData;
			if (scenManager == null)
			{
				nkcofficeData = null;
			}
			else
			{
				NKMUserData myUserData = scenManager.GetMyUserData();
				nkcofficeData = ((myUserData != null) ? myUserData.OfficeData : null);
			}
			NKCOfficeData nkcofficeData2 = nkcofficeData;
			if (nkcofficeData2 != null)
			{
				Sprite orLoadMinimapFaceIcon = NKCResourceUtility.GetOrLoadMinimapFaceIcon(NKMUnitManager.GetUnitTempletBase(nkcofficeData2.GetFriendUnitId(unitUId)), false);
				if (orLoadMinimapFaceIcon != null)
				{
					return orLoadMinimapFaceIcon;
				}
			}
			return this.m_unitBlackSprite;
		}

		// Token: 0x06007DA4 RID: 32164 RVA: 0x002A1F2E File Offset: 0x002A012E
		private void OnCloseMemberEdit()
		{
			Animator selectedAnimator = NKCUIComOfficeMapTileRoom.m_selectedAnimator;
			if (selectedAnimator != null)
			{
				selectedAnimator.SetInteger("State", 1);
			}
			NKCUIComOfficeMapTileRoom.m_selectedAnimator = null;
			NKCUIOfficeMapFront instance = NKCUIOfficeMapFront.GetInstance();
			if (instance == null)
			{
				return;
			}
			instance.RevertScrollRectRange();
		}

		// Token: 0x06007DA5 RID: 32165 RVA: 0x002A1F5C File Offset: 0x002A015C
		private void OnBtnTile()
		{
			NKMOfficeRoomTemplet nkmofficeRoomTemplet = NKMOfficeRoomTemplet.Find(this.m_iRoomId);
			if (nkmofficeRoomTemplet == null)
			{
				return;
			}
			if (NKCUIOfficeMapFront.GetInstance().IgnoreRoomLockState)
			{
				this.m_eRoomState = (this.m_bIsLobby ? NKCUIComOfficeMapTileRoom.RoomState.LOBBY : NKCUIComOfficeMapTileRoom.RoomState.NORMAL);
			}
			if (this.m_eRoomState == NKCUIComOfficeMapTileRoom.RoomState.LOCK || this.m_eRoomState == NKCUIComOfficeMapTileRoom.RoomState.NO_SIGNAL)
			{
				string get_STRING_OFFICE_ROOM_IN_LOCKED_SECTION = NKCUtilString.GET_STRING_OFFICE_ROOM_IN_LOCKED_SECTION;
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(get_STRING_OFFICE_ROOM_IN_LOCKED_SECTION, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return;
			}
			if (this.m_eRoomState == NKCUIComOfficeMapTileRoom.RoomState.CANNOT_PURCHASE)
			{
				string message = "";
				if (nkmofficeRoomTemplet.HasUnlockType)
				{
					UnlockInfo unlockInfo = new UnlockInfo(nkmofficeRoomTemplet.UnlockReqType, nkmofficeRoomTemplet.UnlockReqValue);
					message = NKCContentManager.MakeUnlockConditionString(unlockInfo, false);
				}
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(message, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return;
			}
			if (this.m_eRoomState == NKCUIComOfficeMapTileRoom.RoomState.NEED_PURCHASE)
			{
				if (nkmofficeRoomTemplet.PriceItem == null)
				{
					string content = string.Format(NKCUtilString.GET_STRING_OFFICE_PURCHASE_ROOM, this.m_currentRoomNameText.text);
					NKCPopupResourceConfirmBox.Instance.OpenForConfirm(NKCUtilString.GET_STRING_UNLOCK, content, delegate
					{
						NKCPacketSender.Send_NKMPacket_OFFICE_OPEN_ROOM_REQ(this.m_iRoomId);
					}, null, false);
					return;
				}
				string content2 = string.Format(NKCUtilString.GET_STRING_OFFICE_PURCHASE_ROOM, this.m_currentRoomNameText.text);
				NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_UNLOCK, content2, nkmofficeRoomTemplet.PriceItem.m_ItemMiscID, nkmofficeRoomTemplet.Price, delegate()
				{
					NKCPacketSender.Send_NKMPacket_OFFICE_OPEN_ROOM_REQ(this.m_iRoomId);
				}, null, false);
				return;
			}
			else
			{
				NKCUIOfficeMapFront instance = NKCUIOfficeMapFront.GetInstance();
				if (instance == null)
				{
					return;
				}
				switch (instance.CurrentMinimapState)
				{
				case NKCUIOfficeMapFront.MinimapState.Main:
					if (this.IsAccessableRoom())
					{
						NKCUIOffice instance2 = NKCUIOffice.GetInstance();
						if (instance2 == null)
						{
							return;
						}
						instance2.Open(this.m_iRoomId);
						return;
					}
					break;
				case NKCUIOfficeMapFront.MinimapState.Edit:
				{
					NKCUIPopupOfficeMemberEdit.Instance.Open(this.m_iRoomId, new NKCUIPopupOfficeMemberEdit.OnCloseMemberEdit(this.OnCloseMemberEdit));
					if (NKCUIComOfficeMapTileRoom.m_selectedAnimator != null)
					{
						NKCUIComOfficeMapTileRoom.m_selectedAnimator.SetInteger("State", 1);
					}
					NKCUIComOfficeMapTileRoom.m_selectedAnimator = this.m_animator;
					Animator animator = this.m_animator;
					if (animator != null)
					{
						animator.SetInteger("State", 2);
					}
					float popupWidth = NKCUIPopupOfficeMemberEdit.Instance.PopupWidth;
					if (popupWidth > 0f)
					{
						float x = ((float)Screen.width - popupWidth) * 0.5f;
						Vector3 vector = NKCCamera.GetSubUICamera().ScreenToWorldPoint(new Vector3(x, (float)Screen.height * 0.5f, 0f));
						Vector3 position = base.transform.position;
						position.x -= vector.x;
						NKCUIOfficeMapFront instance3 = NKCUIOfficeMapFront.GetInstance();
						if (instance3 == null)
						{
							return;
						}
						instance3.MoveMiniMap(position, false);
						return;
					}
					break;
				}
				case NKCUIOfficeMapFront.MinimapState.Visit:
				{
					long currentFriendUid = instance.OfficeData.CurrentFriendUid;
					NKCUIOffice instance4 = NKCUIOffice.GetInstance();
					if (instance4 == null)
					{
						return;
					}
					instance4.Open(currentFriendUid, this.m_iRoomId);
					break;
				}
				default:
					return;
				}
				return;
			}
		}

		// Token: 0x06007DA6 RID: 32166 RVA: 0x002A21F5 File Offset: 0x002A03F5
		private void OnDestroy()
		{
			this.m_currentRoomNameText = null;
			this.m_currentUnitArray = null;
			this.m_currentUnitCountText = null;
			this.m_unitBlackSprite = null;
			NKCUIComOfficeMapTileRoom.m_selectedAnimator = null;
		}

		// Token: 0x04006A8F RID: 27279
		public int m_iRoomId;

		// Token: 0x04006A90 RID: 27280
		public bool m_bIsLobby;

		// Token: 0x04006A92 RID: 27282
		[Header("룸 오브젝트")]
		public NKCUIComOfficeMapTileRoom.RoomObject m_roomInfo;

		// Token: 0x04006A93 RID: 27283
		public NKCUIComOfficeMapTileRoom.RoomObject m_lobbyInfo;

		// Token: 0x04006A94 RID: 27284
		public NKCUIComOfficeMapTileRoom.RoomObject m_roomLock;

		// Token: 0x04006A95 RID: 27285
		public NKCUIComOfficeMapTileRoom.RoomObject m_noSignal;

		// Token: 0x04006A96 RID: 27286
		[Header("룸 색상 이미지")]
		public Image m_imgBg;

		// Token: 0x04006A97 RID: 27287
		public Image m_imgGlow;

		// Token: 0x04006A98 RID: 27288
		public Image m_imgStroke;

		// Token: 0x04006A99 RID: 27289
		[Header("로비 유닛")]
		public Image[] m_imgUnitLobbyArray;

		// Token: 0x04006A9A RID: 27290
		public Text m_lbUnitLobbyCount;

		// Token: 0x04006A9B RID: 27291
		[Header("기숙사 유닛")]
		public Image[] m_imgUnitDormArray;

		// Token: 0x04006A9C RID: 27292
		public Text m_lbUnitDormCount;

		// Token: 0x04006A9D RID: 27293
		[Header("언락 필요 자원")]
		public GameObject m_objPriceRoot;

		// Token: 0x04006A9E RID: 27294
		public Image m_imgUnlockItemIcon;

		// Token: 0x04006A9F RID: 27295
		public Text m_lbUnlockPriceCount;

		// Token: 0x04006AA0 RID: 27296
		[Space]
		public Animator m_animator;

		// Token: 0x04006AA1 RID: 27297
		public RectTransform m_rtBgShape;

		// Token: 0x04006AA2 RID: 27298
		public NKCUIComStateButton m_csbtnTileButton;

		// Token: 0x04006AA3 RID: 27299
		public GameObject m_objRedDot;

		// Token: 0x04006AA4 RID: 27300
		private NKCUIComOfficeMapTileRoom.RoomState m_eRoomState;

		// Token: 0x04006AA5 RID: 27301
		private NKMOfficeRoomTemplet.RoomType m_eRoomType;

		// Token: 0x04006AA6 RID: 27302
		private Image[] m_currentUnitArray;

		// Token: 0x04006AA7 RID: 27303
		private Text m_currentUnitCountText;

		// Token: 0x04006AA8 RID: 27304
		private Text m_currentRoomNameText;

		// Token: 0x04006AA9 RID: 27305
		private Sprite m_unitBlackSprite;

		// Token: 0x04006AAA RID: 27306
		private bool IgnoreSuperUser = true;

		// Token: 0x04006AAB RID: 27307
		private static Animator m_selectedAnimator;

		// Token: 0x02001862 RID: 6242
		[Serializable]
		public struct RoomObject
		{
			// Token: 0x0400A8BF RID: 43199
			public GameObject m_objRoot;

			// Token: 0x0400A8C0 RID: 43200
			public Text m_lbRoomName;

			// Token: 0x0400A8C1 RID: 43201
			public Color m_colBg;

			// Token: 0x0400A8C2 RID: 43202
			public Color m_colGlow;

			// Token: 0x0400A8C3 RID: 43203
			public Color m_colStroke;
		}

		// Token: 0x02001863 RID: 6243
		private enum RoomState
		{
			// Token: 0x0400A8C5 RID: 43205
			NORMAL,
			// Token: 0x0400A8C6 RID: 43206
			LOBBY,
			// Token: 0x0400A8C7 RID: 43207
			LOCK,
			// Token: 0x0400A8C8 RID: 43208
			CANNOT_PURCHASE,
			// Token: 0x0400A8C9 RID: 43209
			NEED_PURCHASE,
			// Token: 0x0400A8CA RID: 43210
			NO_SIGNAL
		}
	}
}
