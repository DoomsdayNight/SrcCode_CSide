using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Office;
using NKC.UI.Guide;
using NKM;
using NKM.Templet.Base;
using NKM.Templet.Office;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Office
{
	// Token: 0x02000AF7 RID: 2807
	public class NKCUIOfficeUpsideMenu : MonoBehaviour
	{
		// Token: 0x06007EA1 RID: 32417 RVA: 0x002A77F8 File Offset: 0x002A59F8
		public void Init()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnBack, new UnityAction(this.OnBtnBack));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnHome, new UnityAction(this.OnBtnHome));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnHelp, new UnityAction(this.OnBtnHelp));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnMenu, new UnityAction(this.OnBtnMenu));
			NKCUtil.SetHotkey(this.m_csbtnMenu, HotkeyEventType.HamburgerMenu, null, false);
			NKCUtil.SetButtonClickDelegate(this.m_csbtnRightMove, new UnityAction(this.OnBtnRightMove));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnLeftMove, new UnityAction(this.OnBtnLeftMove));
			this.m_iDormMaxCount = 0;
			using (IEnumerator<NKMOfficeRoomTemplet> enumerator = NKMTempletContainer<NKMOfficeRoomTemplet>.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Type == NKMOfficeRoomTemplet.RoomType.Dorm)
					{
						this.m_iDormMaxCount++;
					}
				}
			}
		}

		// Token: 0x06007EA2 RID: 32418 RVA: 0x002A78F4 File Offset: 0x002A5AF4
		public void SetState(NKCUIOfficeUpsideMenu.MenuState state, NKMOfficeRoomTemplet roomTemplet = null)
		{
			this.m_eMenuState = state;
			switch (state)
			{
			case NKCUIOfficeUpsideMenu.MenuState.MinimapRoom:
				NKCUtil.SetGameobjectActive(this.m_objRoomUnlocked, true);
				NKCUtil.SetGameobjectActive(this.m_objRoomMove, false);
				this.SetTitleText(NKCUtilString.GET_STRING_OFFICE_MINIMAP);
				this.UpdateMinimapRoomInfo();
				break;
			case NKCUIOfficeUpsideMenu.MenuState.MinimapFacility:
				NKCUtil.SetGameobjectActive(this.m_objRoomUnlocked, false);
				NKCUtil.SetGameobjectActive(this.m_objRoomMove, false);
				this.SetTitleText(NKCUtilString.GET_STRING_OFFICE_MINIMAP);
				break;
			case NKCUIOfficeUpsideMenu.MenuState.Room:
				NKCUtil.SetGameobjectActive(this.m_objRoomUnlocked, false);
				NKCUtil.SetGameobjectActive(this.m_objRoomMove, true);
				this.SetTitleText(NKCUtilString.GET_STRING_OFFICE_DORMITORY);
				this.UpdateRoomIndex(roomTemplet);
				break;
			case NKCUIOfficeUpsideMenu.MenuState.Facility:
				NKCUtil.SetGameobjectActive(this.m_objRoomUnlocked, false);
				NKCUtil.SetGameobjectActive(this.m_objRoomMove, false);
				NKCUtil.SetGameobjectActive(this.m_objProfileRoot, false);
				break;
			case NKCUIOfficeUpsideMenu.MenuState.Decoration:
				NKCUtil.SetGameobjectActive(this.m_objRoomUnlocked, false);
				NKCUtil.SetGameobjectActive(this.m_objRoomMove, true);
				NKCUtil.SetLabelText(this.m_lbTitleName, NKCUtilString.GET_STRING_OFFICE_DECORATE);
				break;
			}
			NKCUtil.SetGameobjectActive(this.m_csbtnMenu.gameObject, state != NKCUIOfficeUpsideMenu.MenuState.Decoration);
			if (roomTemplet != null)
			{
				this.UpdateRoomInfo(roomTemplet);
			}
		}

		// Token: 0x06007EA3 RID: 32419 RVA: 0x002A7A18 File Offset: 0x002A5C18
		public void UpdateMinimapRoomInfo()
		{
			NKCUIOfficeMapFront instance = NKCUIOfficeMapFront.GetInstance();
			if (instance == null)
			{
				return;
			}
			int openedDormsCount = instance.OfficeData.GetOpenedDormsCount();
			NKCUtil.SetLabelText(this.m_lbRoomUnlockCount, string.Format(NKCUtilString.GET_STRING_OFFICE_OPENED_DORMS_COUNT, openedDormsCount, this.m_iDormMaxCount));
		}

		// Token: 0x06007EA4 RID: 32420 RVA: 0x002A7A68 File Offset: 0x002A5C68
		public void UpdateRoomInfo(NKMOfficeRoomTemplet roomTemplet)
		{
			if (roomTemplet == null)
			{
				NKCUtil.SetLabelText(this.m_lbTitleName, "");
				NKCUtil.SetLabelText(this.m_lbRoomName, "");
				return;
			}
			if (roomTemplet.IsFacility)
			{
				NKCUtil.SetLabelText(this.m_lbTitleName, NKCStringTable.GetString(roomTemplet.Name, false));
				return;
			}
			NKCUIOfficeMapFront instance = NKCUIOfficeMapFront.GetInstance();
			NKMOfficeRoom nkmofficeRoom = (instance != null) ? instance.OfficeData.GetOfficeRoom(roomTemplet.ID) : null;
			if (nkmofficeRoom != null && !string.IsNullOrEmpty(nkmofficeRoom.name))
			{
				NKCUtil.SetLabelText(this.m_lbRoomName, nkmofficeRoom.name);
				return;
			}
			NKCUtil.SetLabelText(this.m_lbRoomName, NKCStringTable.GetString(roomTemplet.Name, false));
		}

		// Token: 0x06007EA5 RID: 32421 RVA: 0x002A7B10 File Offset: 0x002A5D10
		public void SetRedDotNotify()
		{
			bool bValue = NKCAlarmManager.CheckAllNotify(NKCScenManager.CurrentUserData());
			NKCUtil.SetGameobjectActive(this.m_objMenuRedDot, bValue);
		}

		// Token: 0x06007EA6 RID: 32422 RVA: 0x002A7B34 File Offset: 0x002A5D34
		public void SetActive(bool value)
		{
			base.gameObject.SetActive(value);
			if (value)
			{
				this.SetRedDotNotify();
			}
		}

		// Token: 0x06007EA7 RID: 32423 RVA: 0x002A7B4C File Offset: 0x002A5D4C
		public void SetTitleText(string title)
		{
			NKCUIOfficeMapFront instance = NKCUIOfficeMapFront.GetInstance();
			if (instance == null)
			{
				return;
			}
			if (!instance.Visiting)
			{
				NKCUtil.SetGameobjectActive(this.m_csbtnHelp.gameObject, true);
				NKCUtil.SetGameobjectActive(this.m_objProfileRoot, false);
				NKCUtil.SetLabelText(this.m_lbTitleName, title);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_csbtnHelp.gameObject, false);
			NKCUtil.SetGameobjectActive(this.m_objProfileRoot, true);
			NKMCommonProfile friendProfile = instance.OfficeData.GetFriendProfile();
			if (friendProfile != null)
			{
				NKCUISlotProfile profileSlot = this.m_profileSlot;
				if (profileSlot != null)
				{
					profileSlot.SetProfiledata(friendProfile, null);
				}
				NKCUtil.SetLabelText(this.m_lbTitleName, string.Format(NKCUtilString.GET_STRING_OFFICE_FRIEND_NICKNAME, friendProfile.nickname));
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objProfileRoot, false);
			NKCUtil.SetLabelText(this.m_lbTitleName, string.Format(NKCUtilString.GET_STRING_OFFICE_FRIEND_NICKNAME, ""));
		}

		// Token: 0x06007EA8 RID: 32424 RVA: 0x002A7C20 File Offset: 0x002A5E20
		private void UpdateRoomIndex(NKMOfficeRoomTemplet roomTemplet)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				this.m_dormIdList = nkmuserData.OfficeData.GetOpenedRoomIdList();
			}
			if (this.m_dormIdList != null && roomTemplet != null)
			{
				int num = this.m_dormIdList.IndexOf(roomTemplet.ID);
				NKCUtil.SetLabelText(this.m_lbRoomCount, string.Format("<color={0}>{1}</color>/{2}", this.m_strRoomCountColor, num + 1, this.m_dormIdList.Count));
				return;
			}
			NKCUtil.SetLabelText(this.m_lbRoomCount, "<color=" + this.m_strRoomCountColor + ">0</color>/0");
		}

		// Token: 0x06007EA9 RID: 32425 RVA: 0x002A7CB8 File Offset: 0x002A5EB8
		private void OnBtnHome()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
		}

		// Token: 0x06007EAA RID: 32426 RVA: 0x002A7CC6 File Offset: 0x002A5EC6
		private void OnBtnBack()
		{
			NKCUIManager.OnBackButton();
		}

		// Token: 0x06007EAB RID: 32427 RVA: 0x002A7CCD File Offset: 0x002A5ECD
		private void OnBtnMenu()
		{
			NKCPopupHamburgerMenu.instance.OpenUI();
		}

		// Token: 0x06007EAC RID: 32428 RVA: 0x002A7CDC File Offset: 0x002A5EDC
		private void OnBtnHelp()
		{
			switch (this.m_eMenuState)
			{
			case NKCUIOfficeUpsideMenu.MenuState.MinimapRoom:
			case NKCUIOfficeUpsideMenu.MenuState.MinimapFacility:
				NKCUIPopUpGuide.Instance.Open("ARTICLE_OFFICE_INFO", 0);
				return;
			case NKCUIOfficeUpsideMenu.MenuState.Room:
			case NKCUIOfficeUpsideMenu.MenuState.Decoration:
				NKCUIPopUpGuide.Instance.Open("ARTICLE_OFFICE_DORMI", 0);
				return;
			}
			NKCUIPopUpGuide.Instance.Open("ARTICLE_OFFICE_FACILITY", 0);
		}

		// Token: 0x06007EAD RID: 32429 RVA: 0x002A7D40 File Offset: 0x002A5F40
		public void OnBtnLeftMove()
		{
			if (this.m_dormIdList == null)
			{
				return;
			}
			int num = this.m_dormIdList.IndexOf(NKCUIOffice.GetInstance().RoomID);
			if (num < 0 || num >= this.m_dormIdList.Count)
			{
				return;
			}
			if (num == 0)
			{
				num = this.m_dormIdList.Count - 1;
			}
			else
			{
				num--;
			}
			NKCUIOffice.GetInstance().MoveToRoom(this.m_dormIdList[num]);
		}

		// Token: 0x06007EAE RID: 32430 RVA: 0x002A7DAC File Offset: 0x002A5FAC
		public void OnBtnRightMove()
		{
			if (this.m_dormIdList == null)
			{
				return;
			}
			int num = this.m_dormIdList.IndexOf(NKCUIOffice.GetInstance().RoomID);
			if (num < 0 || num >= this.m_dormIdList.Count)
			{
				return;
			}
			if (num == this.m_dormIdList.Count - 1)
			{
				num = 0;
			}
			else
			{
				num++;
			}
			NKCUIOffice.GetInstance().MoveToRoom(this.m_dormIdList[num]);
		}

		// Token: 0x06007EAF RID: 32431 RVA: 0x002A7E19 File Offset: 0x002A6019
		private void OnDestroy()
		{
			List<int> dormIdList = this.m_dormIdList;
			if (dormIdList != null)
			{
				dormIdList.Clear();
			}
			this.m_dormIdList = null;
		}

		// Token: 0x04006B3B RID: 27451
		public Text m_lbTitleName;

		// Token: 0x04006B3C RID: 27452
		public NKCUIComStateButton m_csbtnBack;

		// Token: 0x04006B3D RID: 27453
		public NKCUIComStateButton m_csbtnHome;

		// Token: 0x04006B3E RID: 27454
		public NKCUIComStateButton m_csbtnHelp;

		// Token: 0x04006B3F RID: 27455
		[Header("햄버거 메뉴")]
		public NKCUIComStateButton m_csbtnMenu;

		// Token: 0x04006B40 RID: 27456
		public GameObject m_objMenuRedDot;

		// Token: 0x04006B41 RID: 27457
		[Header("미니맵 룸 언락 카운트")]
		public GameObject m_objRoomUnlocked;

		// Token: 0x04006B42 RID: 27458
		public Text m_lbRoomUnlockCount;

		// Token: 0x04006B43 RID: 27459
		[Header("방 정보")]
		public GameObject m_objRoomMove;

		// Token: 0x04006B44 RID: 27460
		public Transform m_RoomInfoBg;

		// Token: 0x04006B45 RID: 27461
		public NKCUIComStateButton m_csbtnLeftMove;

		// Token: 0x04006B46 RID: 27462
		public NKCUIComStateButton m_csbtnRightMove;

		// Token: 0x04006B47 RID: 27463
		public Text m_lbRoomName;

		// Token: 0x04006B48 RID: 27464
		public Text m_lbRoomCount;

		// Token: 0x04006B49 RID: 27465
		public string m_strRoomCountColor;

		// Token: 0x04006B4A RID: 27466
		[Header("친구 프로필 아이콘")]
		public GameObject m_objProfileRoot;

		// Token: 0x04006B4B RID: 27467
		public NKCUISlotProfile m_profileSlot;

		// Token: 0x04006B4C RID: 27468
		private int m_iDormMaxCount;

		// Token: 0x04006B4D RID: 27469
		private NKCUIOfficeUpsideMenu.MenuState m_eMenuState;

		// Token: 0x04006B4E RID: 27470
		private List<int> m_dormIdList;

		// Token: 0x02001879 RID: 6265
		public enum MenuState
		{
			// Token: 0x0400A90B RID: 43275
			MinimapRoom,
			// Token: 0x0400A90C RID: 43276
			MinimapFacility,
			// Token: 0x0400A90D RID: 43277
			Room,
			// Token: 0x0400A90E RID: 43278
			Facility,
			// Token: 0x0400A90F RID: 43279
			Decoration
		}
	}
}
