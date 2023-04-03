using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Community;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Friend
{
	// Token: 0x02000B17 RID: 2839
	public class NKCUIFriendTopMenu : MonoBehaviour
	{
		// Token: 0x06008112 RID: 33042 RVA: 0x002B7C8C File Offset: 0x002B5E8C
		public void Init()
		{
			this.m_NKCUIFriendSlotMgr.Init(this.m_ParentOfFriendSlots);
			this.m_NKCUIFriendSlotMgr.SetAscend(true);
			this.m_NKCUIFriendSlotMgr.SetFriendSortType(NKCUIFriendTopMenu.FRIEND_SORT_TYPE.FST_LAST_LOGIN);
			if (this.m_cbtnNKM_UI_FRIEND_TOP_SORTING != null)
			{
				this.m_cbtnNKM_UI_FRIEND_TOP_SORTING.PointerClick.RemoveAllListeners();
				this.m_cbtnNKM_UI_FRIEND_TOP_SORTING.PointerClick.AddListener(new UnityAction(this.OnClickOpenSortMenu));
			}
			if (this.m_cbtlNKM_UI_FRIEND_TOP_ARRAY != null)
			{
				this.m_cbtlNKM_UI_FRIEND_TOP_ARRAY.OnValueChanged.RemoveAllListeners();
				this.m_cbtlNKM_UI_FRIEND_TOP_ARRAY.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickAscendToggle));
			}
			if (this.m_cbtnNKM_UI_FRIEND_TOP_SEARCH_BUTTON != null)
			{
				this.m_cbtnNKM_UI_FRIEND_TOP_SEARCH_BUTTON.PointerClick.RemoveAllListeners();
				this.m_cbtnNKM_UI_FRIEND_TOP_SEARCH_BUTTON.PointerClick.AddListener(new UnityAction(this.OnClickSearch));
			}
			if (this.m_cbtnNKM_UI_FRIEND_TOP_SUBMENU_REFRESH != null)
			{
				this.m_cbtnNKM_UI_FRIEND_TOP_SUBMENU_REFRESH.PointerClick.RemoveAllListeners();
				this.m_cbtnNKM_UI_FRIEND_TOP_SUBMENU_REFRESH.PointerClick.AddListener(delegate()
				{
					this.OnClickRefresh();
				});
			}
			if (this.m_cbtnNKM_UI_FRIEND_TOP_SORTING_MENU_01 != null)
			{
				this.m_cbtnNKM_UI_FRIEND_TOP_SORTING_MENU_01.PointerClick.RemoveAllListeners();
				this.m_cbtnNKM_UI_FRIEND_TOP_SORTING_MENU_01.PointerClick.AddListener(new UnityAction(this.OnClickLastLoginSort));
			}
			if (this.m_cbtnNKM_UI_FRIEND_TOP_SORTING_MENU_02 != null)
			{
				this.m_cbtnNKM_UI_FRIEND_TOP_SORTING_MENU_02.PointerClick.RemoveAllListeners();
				this.m_cbtnNKM_UI_FRIEND_TOP_SORTING_MENU_02.PointerClick.AddListener(new UnityAction(this.OnClickUserLevelSort));
			}
			if (this.m_IFNicknameOrUID != null)
			{
				this.m_IFNicknameOrUID.onEndEdit.RemoveAllListeners();
				this.m_IFNicknameOrUID.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditSearch));
			}
		}

		// Token: 0x06008113 RID: 33043 RVA: 0x002B7E58 File Offset: 0x002B6058
		private void OnClickRefresh()
		{
			this.OnClickFriendSearch(true);
			this.m_fNextPossibleRefreshTime = Time.time + 3f;
		}

		// Token: 0x06008114 RID: 33044 RVA: 0x002B7E72 File Offset: 0x002B6072
		private void Update()
		{
			NKCUtil.SetGameobjectActive(this.m_cbtnNKM_UI_FRIEND_TOP_SUBMENU_REFRESH, this.m_fNextPossibleRefreshTime < Time.time);
			NKCUtil.SetGameobjectActive(this.m_cbtnNKM_UI_FRIEND_TOP_SUBMENU_REFRESH_Disable, this.m_fNextPossibleRefreshTime >= Time.time);
		}

		// Token: 0x06008115 RID: 33045 RVA: 0x002B7EA7 File Offset: 0x002B60A7
		public void SetAddReceiveNew(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_TOP_SUBMENU_ADD_RECEIVE_NEW, bSet);
		}

		// Token: 0x06008116 RID: 33046 RVA: 0x002B7EB8 File Offset: 0x002B60B8
		public void OnClickSearch()
		{
			this.m_NKCUIFriendSlotMgr.Clear();
			if (string.IsNullOrWhiteSpace(this.m_IFNicknameOrUID.text))
			{
				NKMPacket_FRIEND_RECOMMEND_REQ packet = new NKMPacket_FRIEND_RECOMMEND_REQ();
				NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
				return;
			}
			NKMPacket_FRIEND_SEARCH_REQ nkmpacket_FRIEND_SEARCH_REQ = new NKMPacket_FRIEND_SEARCH_REQ();
			nkmpacket_FRIEND_SEARCH_REQ.searchKeyword = this.m_IFNicknameOrUID.text;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_FRIEND_SEARCH_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06008117 RID: 33047 RVA: 0x002B7F26 File Offset: 0x002B6126
		private void OnEndEditSearch(string input)
		{
			if (NKCInputManager.IsChatSubmitEnter())
			{
				if (!this.m_cbtnNKM_UI_FRIEND_TOP_SEARCH_BUTTON.m_bLock)
				{
					this.OnClickSearch();
				}
				EventSystem.current.SetSelectedGameObject(null);
			}
		}

		// Token: 0x06008118 RID: 33048 RVA: 0x002B7F4D File Offset: 0x002B614D
		public void OnClickAscendToggle(bool bCheck)
		{
			this.m_NKCUIFriendSlotMgr.SetAscend(bCheck);
		}

		// Token: 0x06008119 RID: 33049 RVA: 0x002B7F5B File Offset: 0x002B615B
		public void OnRecv(NKMPacket_FRIEND_BLOCK_ACK cNKMPacket_FRIEND_BLOCK_ACK)
		{
			if (this.m_NKCUIFriendSlotMgr.InvalidSlot(cNKMPacket_FRIEND_BLOCK_ACK.friendCode))
			{
				this.SetCountUI();
			}
		}

		// Token: 0x0600811A RID: 33050 RVA: 0x002B7F78 File Offset: 0x002B6178
		public void OnRecv(NKMPacket_FRIEND_ACCEPT_NOT cNKMPacket_FRIEND_ACCEPT_NOT)
		{
			if (cNKMPacket_FRIEND_ACCEPT_NOT.isAllow && this.IsFriendListShowing())
			{
				this.m_NKCUIFriendSlotMgr.Add(cNKMPacket_FRIEND_ACCEPT_NOT.friendProfileData, NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_FRIEND_LIST);
				this.SetCountUI();
			}
			if (this.IsSentREQListShowing())
			{
				this.m_NKCUIFriendSlotMgr.InvalidSlot(cNKMPacket_FRIEND_ACCEPT_NOT.friendProfileData.commonProfile.friendCode);
				this.SetCountUI();
			}
		}

		// Token: 0x0600811B RID: 33051 RVA: 0x002B7FD7 File Offset: 0x002B61D7
		public void OnRecv(NKMPacket_FRIEND_DELETE_NOT cNKMPacket_FRIEND_DEL_NOT)
		{
			if (this.IsFriendListShowing() && this.m_NKCUIFriendSlotMgr.InvalidSlot(cNKMPacket_FRIEND_DEL_NOT.friendCode))
			{
				this.SetCountUI();
			}
		}

		// Token: 0x0600811C RID: 33052 RVA: 0x002B7FFA File Offset: 0x002B61FA
		public void OnRecv(NKMPacket_FRIEND_REQUEST_NOT cNKMPacket_FRIEND_ADD_NOT)
		{
			if (this.IsReceivedREQListShowing())
			{
				this.m_NKCUIFriendSlotMgr.Add(cNKMPacket_FRIEND_ADD_NOT.friendProfileData, NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_RECEIVE_REQ);
				this.SetCountUI();
			}
		}

		// Token: 0x0600811D RID: 33053 RVA: 0x002B801C File Offset: 0x002B621C
		public void OnRecv(NKMPacket_FRIEND_CANCEL_REQUEST_NOT cNKMPacket_FRIEND_ADD_CANCEL_NOT)
		{
			if (this.IsReceivedREQListShowing() && this.m_NKCUIFriendSlotMgr.InvalidSlot(cNKMPacket_FRIEND_ADD_CANCEL_NOT.friendCode))
			{
				this.SetCountUI();
			}
		}

		// Token: 0x0600811E RID: 33054 RVA: 0x002B803F File Offset: 0x002B623F
		public void OnRecv(NKMPacket_FRIEND_DELETE_ACK cNKMPacket_FRIEND_DEL_ACK)
		{
			if (this.IsFriendListShowing() && this.m_NKCUIFriendSlotMgr.InvalidSlot(cNKMPacket_FRIEND_DEL_ACK.friendCode))
			{
				this.SetCountUI();
			}
		}

		// Token: 0x0600811F RID: 33055 RVA: 0x002B8062 File Offset: 0x002B6262
		public void OnRecv(NKMPacket_FRIEND_SEARCH_ACK cNKMPacket_FRIEND_SEARCH_ACK)
		{
			if (this.IsSearchShowing())
			{
				this.m_NKCUIFriendSlotMgr.SetForSearch(false, cNKMPacket_FRIEND_SEARCH_ACK.list);
			}
		}

		// Token: 0x06008120 RID: 33056 RVA: 0x002B807E File Offset: 0x002B627E
		public void OnRecv(NKMPacket_FRIEND_RECOMMEND_ACK cNKMPacket_FRIEND_RECOMMEND_ACK)
		{
			if (this.IsSearchShowing())
			{
				this.m_NKCUIFriendSlotMgr.SetForSearch(true, cNKMPacket_FRIEND_RECOMMEND_ACK.list);
			}
		}

		// Token: 0x06008121 RID: 33057 RVA: 0x002B809A File Offset: 0x002B629A
		public void OnRecv(NKMPacket_FRIEND_REQUEST_ACK cNKMPacket_FRIEND_ADD_ACK)
		{
			if (this.IsSearchShowing())
			{
				this.m_NKCUIFriendSlotMgr.InvalidSlot(cNKMPacket_FRIEND_ADD_ACK.friendCode);
			}
		}

		// Token: 0x06008122 RID: 33058 RVA: 0x002B80B6 File Offset: 0x002B62B6
		public void OnRecv(NKMPacket_FRIEND_ACCEPT_ACK cNKMPacket_FRIEND_ACCEPT_ACK)
		{
			if (this.IsReceivedREQListShowing() && this.m_NKCUIFriendSlotMgr.InvalidSlot(cNKMPacket_FRIEND_ACCEPT_ACK.friendCode))
			{
				this.SetCountUI();
			}
		}

		// Token: 0x06008123 RID: 33059 RVA: 0x002B80D9 File Offset: 0x002B62D9
		public void OnRecv(NKMPacket_FRIEND_CANCEL_REQUEST_ACK cNKMPacket_FRIEND_ADD_CANCEL_ACK)
		{
			if (this.IsSentREQListShowing() && this.m_NKCUIFriendSlotMgr.InvalidSlot(cNKMPacket_FRIEND_ADD_CANCEL_ACK.friendCode))
			{
				this.SetCountUI();
			}
		}

		// Token: 0x06008124 RID: 33060 RVA: 0x002B80FC File Offset: 0x002B62FC
		public NKCUIFriendSlot.FRIEND_SLOT_TYPE GetCurrentSlotType()
		{
			if (this.IsFriendListShowing())
			{
				return NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_FRIEND_LIST;
			}
			if (this.IsBlockListShowing())
			{
				return NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_BLOCK_LIST;
			}
			if (this.IsReceivedREQListShowing())
			{
				return NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_RECEIVE_REQ;
			}
			if (this.IsSentREQListShowing())
			{
				return NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_SENT_REQ;
			}
			if (this.IsSearchShowing())
			{
				return NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_FRIEND_SEARCH;
			}
			return NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_FRIEND_LIST;
		}

		// Token: 0x06008125 RID: 33061 RVA: 0x002B8134 File Offset: 0x002B6334
		public void OnRecv(NKMPacket_FRIEND_LIST_ACK cNKMPacket_FRIEND_LIST_ACK)
		{
			if (cNKMPacket_FRIEND_LIST_ACK.friendListType == NKM_FRIEND_LIST_TYPE.FRIEND)
			{
				if (this.IsFriendListShowing())
				{
					this.m_NKCUIFriendSlotMgr.Set(cNKMPacket_FRIEND_LIST_ACK.friendListType, cNKMPacket_FRIEND_LIST_ACK.list);
					this.m_lbEmptyMessage.text = NKCUtilString.GET_STRING_FRIEND_LIST_IS_EMPTY;
					this.SetCountUI();
				}
			}
			else if (cNKMPacket_FRIEND_LIST_ACK.friendListType == NKM_FRIEND_LIST_TYPE.BLOCKER)
			{
				if (this.IsBlockListShowing())
				{
					this.m_NKCUIFriendSlotMgr.Set(cNKMPacket_FRIEND_LIST_ACK.friendListType, cNKMPacket_FRIEND_LIST_ACK.list);
					this.m_lbEmptyMessage.text = NKCUtilString.GET_STRING_FRIEND_LIST_BLOCK_IS_EMPTY;
					this.SetCountUI();
				}
			}
			else if (cNKMPacket_FRIEND_LIST_ACK.friendListType == NKM_FRIEND_LIST_TYPE.RECEIVE_REQUEST)
			{
				if (this.IsReceivedREQListShowing())
				{
					this.m_NKCUIFriendSlotMgr.Set(cNKMPacket_FRIEND_LIST_ACK.friendListType, cNKMPacket_FRIEND_LIST_ACK.list);
					this.m_lbEmptyMessage.text = NKCUtilString.GET_STRING_FRIEND_LIST_RECV_IS_EMPTY;
					this.SetCountUI();
				}
			}
			else if (cNKMPacket_FRIEND_LIST_ACK.friendListType == NKM_FRIEND_LIST_TYPE.SEND_REQUEST && this.IsSentREQListShowing())
			{
				this.m_NKCUIFriendSlotMgr.Set(cNKMPacket_FRIEND_LIST_ACK.friendListType, cNKMPacket_FRIEND_LIST_ACK.list);
				this.m_lbEmptyMessage.text = NKCUtilString.GET_STRING_FRIEND_LIST_REQ_IS_EMPTY;
				this.SetCountUI();
			}
			NKCUtil.SetGameobjectActive(this.m_objEmpty, cNKMPacket_FRIEND_LIST_ACK.list.Count == 0);
		}

		// Token: 0x06008126 RID: 33062 RVA: 0x002B8260 File Offset: 0x002B6460
		public void SetCountUI()
		{
			NKCUtil.SetGameobjectActive(this.m_objEmpty, this.m_NKCUIFriendSlotMgr.GetActiveSlotCount() == 0);
			if (this.IsFriendListShowing())
			{
				this.m_NKM_UI_FRIEND_TOP_COUNT_TEXT.text = string.Format(NKCUtilString.GET_STRING_FRIEND_COUNT_TWO_PARAM, this.m_NKCUIFriendSlotMgr.GetActiveSlotCount(), 60);
				return;
			}
			if (this.IsBlockListShowing())
			{
				this.m_NKM_UI_FRIEND_TOP_COUNT_TEXT.text = string.Format(NKCUtilString.GET_STRING_FRIEND_BLOCK_COUNT_TWO_PARAM, this.m_NKCUIFriendSlotMgr.GetActiveSlotCount(), 30);
				return;
			}
			if (this.IsReceivedREQListShowing())
			{
				this.m_NKM_UI_FRIEND_TOP_COUNT_TEXT.text = string.Format(NKCUtilString.GET_STRING_FRIEND_RECV_COUNT_TWO_PARAM, this.m_NKCUIFriendSlotMgr.GetActiveSlotCount(), 50);
				return;
			}
			if (this.IsSentREQListShowing())
			{
				this.m_NKM_UI_FRIEND_TOP_COUNT_TEXT.text = string.Format(NKCUtilString.GET_STRING_FRIEND_REQ_COUNT_TWO_PARAM, this.m_NKCUIFriendSlotMgr.GetActiveSlotCount(), 50);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objEmpty, false);
		}

		// Token: 0x06008127 RID: 33063 RVA: 0x002B8366 File Offset: 0x002B6566
		public void OnClickOpenSortMenu()
		{
			if (this.m_NKM_UI_FRIEND_TOP_SORTING_MENU.activeSelf)
			{
				this.CloseSortMenu(true);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_TOP_SORTING_MENU_01, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_TOP_SORTING_MENU_02, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_TOP_SORTING_MENU, true);
		}

		// Token: 0x06008128 RID: 33064 RVA: 0x002B83A1 File Offset: 0x002B65A1
		public void CloseSortMenu(bool bAnimate = true)
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_TOP_SORTING_MENU_01, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_TOP_SORTING_MENU_02, false);
			this.OnFinishSortMenuCloseAni();
		}

		// Token: 0x06008129 RID: 33065 RVA: 0x002B83C1 File Offset: 0x002B65C1
		private void OnFinishSortMenuCloseAni()
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_TOP_SORTING_MENU, false);
		}

		// Token: 0x0600812A RID: 33066 RVA: 0x002B83CF File Offset: 0x002B65CF
		public void OnClickLastLoginSort()
		{
			this.CloseSortMenu(true);
			this.m_NKM_UI_FRIEND_TOP_SORTING_TEXT.text = NKCUtilString.GET_STRING_FRIEND_LAST_CONNECT;
			this.m_NKCUIFriendSlotMgr.SetFriendSortType(NKCUIFriendTopMenu.FRIEND_SORT_TYPE.FST_LAST_LOGIN);
		}

		// Token: 0x0600812B RID: 33067 RVA: 0x002B83F4 File Offset: 0x002B65F4
		public void OnClickUserLevelSort()
		{
			this.CloseSortMenu(true);
			this.m_NKM_UI_FRIEND_TOP_SORTING_TEXT.text = NKCUtilString.GET_STRING_FRIEND_LEVEL;
			this.m_NKCUIFriendSlotMgr.SetFriendSortType(NKCUIFriendTopMenu.FRIEND_SORT_TYPE.FST_LEVEL);
		}

		// Token: 0x0600812C RID: 33068 RVA: 0x002B8419 File Offset: 0x002B6619
		private bool IsSearchShowing()
		{
			return this.m_NKM_UI_FRIEND_TOP_SUBMENU_ADD_SEARCH.m_bChecked && this.m_NKM_UI_FRIEND_TOP_SUBMENU_ADD.activeSelf;
		}

		// Token: 0x0600812D RID: 33069 RVA: 0x002B8438 File Offset: 0x002B6638
		private bool IsFriendListShowing()
		{
			return this.m_NKM_UI_FRIEND_TOP_SUBMENU_MANAGEMENT_LIST.m_bChecked && this.m_NKM_UI_FRIEND_TOP_SUBMENU_MANAGEMENT.activeSelf;
		}

		// Token: 0x0600812E RID: 33070 RVA: 0x002B8457 File Offset: 0x002B6657
		private bool IsBlockListShowing()
		{
			return this.m_NKM_UI_FRIEND_TOP_SUBMENU_MANAGEMENT_BLOCK.m_bChecked && this.m_NKM_UI_FRIEND_TOP_SUBMENU_MANAGEMENT.activeSelf;
		}

		// Token: 0x0600812F RID: 33071 RVA: 0x002B8476 File Offset: 0x002B6676
		private bool IsReceivedREQListShowing()
		{
			return this.m_NKM_UI_FRIEND_TOP_SUBMENU_ADD_RECEIVE.m_bChecked && this.m_NKM_UI_FRIEND_TOP_SUBMENU_ADD.activeSelf;
		}

		// Token: 0x06008130 RID: 33072 RVA: 0x002B8495 File Offset: 0x002B6695
		private bool IsSentREQListShowing()
		{
			return this.m_NKM_UI_FRIEND_TOP_SUBMENU_ADD_SEND.m_bChecked && this.m_NKM_UI_FRIEND_TOP_SUBMENU_ADD.activeSelf;
		}

		// Token: 0x06008131 RID: 33073 RVA: 0x002B84B4 File Offset: 0x002B66B4
		public void Open(NKCUIFriendTopMenu.FRIEND_TOP_MENU_TYPE menuType)
		{
			this.m_FRIEND_TOP_MENU_TYPE = menuType;
			this.CloseSortMenu(true);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_LIST, true);
			if (this.m_FRIEND_TOP_MENU_TYPE == NKCUIFriendTopMenu.FRIEND_TOP_MENU_TYPE.FTMT_MANAGE)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_TOP_COUNT, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_TOP_SEARCH, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_TOP_SUBMENU_MANAGEMENT, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_TOP_SUBMENU_ADD, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_TOP_SUBMENU_REFRESH, false);
				NKCUtil.SetGameobjectActive(this.m_MENTORING_INVITE_REWARD_TIME, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_TOP_SUBMENU_MENTORING, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_MENTORING_LIST_INFO, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_LIST_ScrollView, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_MENTORIG_LIST_ScrollView, false);
				NKCUtil.SetGameobjectActive(this.m_MENTORING_COUNT, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_TOP_SORT, true);
				NKCUtil.SetGameobjectActive(this.m_m_NKM_UI_FRIEND_MENTORING_INVITE_ILLUST_VIEW, false);
				if (this.m_NKM_UI_FRIEND_TOP_SUBMENU_MANAGEMENT_LIST.m_bChecked)
				{
					this.OnClickFriendList(true);
					return;
				}
				if (this.m_NKM_UI_FRIEND_TOP_SUBMENU_MANAGEMENT_BLOCK.m_bChecked)
				{
					this.OnClickBlockList(true);
					return;
				}
			}
			else if (this.m_FRIEND_TOP_MENU_TYPE == NKCUIFriendTopMenu.FRIEND_TOP_MENU_TYPE.FTMT_REGISTER)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_TOP_SUBMENU_MANAGEMENT, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_TOP_SUBMENU_ADD, true);
				NKCUtil.SetGameobjectActive(this.m_MENTORING_INVITE_REWARD_TIME, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_TOP_SUBMENU_MENTORING, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_MENTORING_LIST_INFO, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_LIST_ScrollView, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_MENTORIG_LIST_ScrollView, false);
				NKCUtil.SetGameobjectActive(this.m_MENTORING_COUNT, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_TOP_SORT, true);
				NKCUtil.SetGameobjectActive(this.m_m_NKM_UI_FRIEND_MENTORING_INVITE_ILLUST_VIEW, false);
				if (this.m_NKM_UI_FRIEND_TOP_SUBMENU_ADD_RECEIVE_NEW.activeSelf)
				{
					this.m_NKM_UI_FRIEND_TOP_SUBMENU_ADD_RECEIVE.Select(false, false, false);
					this.m_NKM_UI_FRIEND_TOP_SUBMENU_ADD_RECEIVE.Select(true, false, false);
					return;
				}
				this.m_NKM_UI_FRIEND_TOP_SUBMENU_ADD_SEARCH.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickFriendSearch));
				this.m_NKM_UI_FRIEND_TOP_SUBMENU_ADD_SEND.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickSentReq));
				this.m_NKM_UI_FRIEND_TOP_SUBMENU_ADD_RECEIVE.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickReceivedReq));
				if (this.m_NKM_UI_FRIEND_TOP_SUBMENU_ADD_SEARCH.m_bChecked)
				{
					this.OnClickFriendSearch(true);
					return;
				}
				if (this.m_NKM_UI_FRIEND_TOP_SUBMENU_ADD_RECEIVE.m_bChecked)
				{
					this.OnClickReceivedReq(true);
					return;
				}
				if (this.m_NKM_UI_FRIEND_TOP_SUBMENU_ADD_SEND.m_bChecked)
				{
					this.OnClickSentReq(true);
				}
			}
		}

		// Token: 0x06008132 RID: 33074 RVA: 0x002B86FD File Offset: 0x002B68FD
		public void Close()
		{
			NKCUtil.SetGameobjectActive(this.m_objEmpty, false);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_LIST, false);
		}

		// Token: 0x06008133 RID: 33075 RVA: 0x002B8723 File Offset: 0x002B6923
		public void CloseInstance()
		{
			NKCUIFriendTopMenu.NKCUIFriendSlotMgr nkcuifriendSlotMgr = this.m_NKCUIFriendSlotMgr;
			if (nkcuifriendSlotMgr == null)
			{
				return;
			}
			nkcuifriendSlotMgr.CloseSlotInstance();
		}

		// Token: 0x06008134 RID: 33076 RVA: 0x002B8738 File Offset: 0x002B6938
		public void OnClickFriendList(bool bCheck = true)
		{
			if (!bCheck)
			{
				return;
			}
			this.m_NKM_UI_FRIEND_TOP_COUNT_TEXT.text = "";
			this.m_NKCUIFriendSlotMgr.Clear();
			NKMPacket_FRIEND_LIST_REQ nkmpacket_FRIEND_LIST_REQ = new NKMPacket_FRIEND_LIST_REQ();
			nkmpacket_FRIEND_LIST_REQ.friendListType = NKM_FRIEND_LIST_TYPE.FRIEND;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_FRIEND_LIST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06008135 RID: 33077 RVA: 0x002B8784 File Offset: 0x002B6984
		public void OnClickBlockList(bool bCheck = true)
		{
			if (!bCheck)
			{
				return;
			}
			this.m_NKM_UI_FRIEND_TOP_COUNT_TEXT.text = "";
			this.m_NKCUIFriendSlotMgr.Clear();
			NKMPacket_FRIEND_LIST_REQ nkmpacket_FRIEND_LIST_REQ = new NKMPacket_FRIEND_LIST_REQ();
			nkmpacket_FRIEND_LIST_REQ.friendListType = NKM_FRIEND_LIST_TYPE.BLOCKER;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_FRIEND_LIST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06008136 RID: 33078 RVA: 0x002B87D0 File Offset: 0x002B69D0
		public void OnClickFriendSearch(bool bCheck = true)
		{
			if (!bCheck)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objEmpty, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_TOP_COUNT, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_TOP_SEARCH, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_TOP_SUBMENU_REFRESH, true);
			this.m_NKCUIFriendSlotMgr.Clear();
			NKMPacket_FRIEND_RECOMMEND_REQ packet = new NKMPacket_FRIEND_RECOMMEND_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06008137 RID: 33079 RVA: 0x002B8838 File Offset: 0x002B6A38
		public void OnClickReceivedReq(bool bCheck = true)
		{
			if (!bCheck)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_TOP_COUNT, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_TOP_SEARCH, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_TOP_SUBMENU_REFRESH, false);
			this.m_NKM_UI_FRIEND_TOP_COUNT_TEXT.text = "";
			this.m_NKCUIFriendSlotMgr.Clear();
			NKMPacket_FRIEND_LIST_REQ nkmpacket_FRIEND_LIST_REQ = new NKMPacket_FRIEND_LIST_REQ();
			nkmpacket_FRIEND_LIST_REQ.friendListType = NKM_FRIEND_LIST_TYPE.RECEIVE_REQUEST;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_FRIEND_LIST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06008138 RID: 33080 RVA: 0x002B88A8 File Offset: 0x002B6AA8
		public void OnClickSentReq(bool bCheck = true)
		{
			if (!bCheck)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_TOP_COUNT, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_TOP_SEARCH, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_TOP_SUBMENU_REFRESH, false);
			this.m_NKM_UI_FRIEND_TOP_COUNT_TEXT.text = "";
			this.m_NKCUIFriendSlotMgr.Clear();
			NKMPacket_FRIEND_LIST_REQ nkmpacket_FRIEND_LIST_REQ = new NKMPacket_FRIEND_LIST_REQ();
			nkmpacket_FRIEND_LIST_REQ.friendListType = NKM_FRIEND_LIST_TYPE.SEND_REQUEST;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_FRIEND_LIST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x04006D2B RID: 27947
		public GameObject m_NKM_UI_FRIEND_TOP_COUNT;

		// Token: 0x04006D2C RID: 27948
		public GameObject m_NKM_UI_FRIEND_TOP_SEARCH;

		// Token: 0x04006D2D RID: 27949
		public GameObject m_NKM_UI_FRIEND_TOP_SUBMENU_MANAGEMENT;

		// Token: 0x04006D2E RID: 27950
		public GameObject m_NKM_UI_FRIEND_TOP_SUBMENU_ADD;

		// Token: 0x04006D2F RID: 27951
		public GameObject m_NKM_UI_FRIEND_TOP_SUBMENU_REFRESH;

		// Token: 0x04006D30 RID: 27952
		public Text m_NKM_UI_FRIEND_TOP_COUNT_TEXT;

		// Token: 0x04006D31 RID: 27953
		public NKCUIComToggle m_NKM_UI_FRIEND_TOP_SUBMENU_MANAGEMENT_LIST;

		// Token: 0x04006D32 RID: 27954
		public NKCUIComToggle m_NKM_UI_FRIEND_TOP_SUBMENU_MANAGEMENT_BLOCK;

		// Token: 0x04006D33 RID: 27955
		public NKCUIComToggle m_NKM_UI_FRIEND_TOP_SUBMENU_ADD_SEARCH;

		// Token: 0x04006D34 RID: 27956
		public NKCUIComToggle m_NKM_UI_FRIEND_TOP_SUBMENU_ADD_RECEIVE;

		// Token: 0x04006D35 RID: 27957
		public NKCUIComToggle m_NKM_UI_FRIEND_TOP_SUBMENU_ADD_SEND;

		// Token: 0x04006D36 RID: 27958
		public GameObject m_NKM_UI_FRIEND_TOP_SUBMENU_ADD_RECEIVE_NEW;

		// Token: 0x04006D37 RID: 27959
		public Text m_NKM_UI_FRIEND_TOP_SORTING_TEXT;

		// Token: 0x04006D38 RID: 27960
		public GameObject m_NKM_UI_FRIEND_TOP_SORTING_MENU;

		// Token: 0x04006D39 RID: 27961
		public GameObject m_NKM_UI_FRIEND_TOP_SORTING_MENU_01;

		// Token: 0x04006D3A RID: 27962
		public GameObject m_NKM_UI_FRIEND_TOP_SORTING_MENU_02;

		// Token: 0x04006D3B RID: 27963
		public GameObject m_NKM_UI_FRIEND_LIST;

		// Token: 0x04006D3C RID: 27964
		public Transform m_ParentOfFriendSlots;

		// Token: 0x04006D3D RID: 27965
		public InputField m_IFNicknameOrUID;

		// Token: 0x04006D3E RID: 27966
		public GameObject m_objEmpty;

		// Token: 0x04006D3F RID: 27967
		public Text m_lbEmptyMessage;

		// Token: 0x04006D40 RID: 27968
		public GameObject m_NKM_UI_FRIEND_TOP_SORT;

		// Token: 0x04006D41 RID: 27969
		public NKCUIComButton m_cbtnNKM_UI_FRIEND_TOP_SORTING;

		// Token: 0x04006D42 RID: 27970
		public NKCUIComToggle m_cbtlNKM_UI_FRIEND_TOP_ARRAY;

		// Token: 0x04006D43 RID: 27971
		public NKCUIComButton m_cbtnNKM_UI_FRIEND_TOP_SEARCH_BUTTON;

		// Token: 0x04006D44 RID: 27972
		public NKCUIComButton m_cbtnNKM_UI_FRIEND_TOP_SUBMENU_REFRESH;

		// Token: 0x04006D45 RID: 27973
		public NKCUIComButton m_cbtnNKM_UI_FRIEND_TOP_SUBMENU_REFRESH_Disable;

		// Token: 0x04006D46 RID: 27974
		private float m_fNextPossibleRefreshTime;

		// Token: 0x04006D47 RID: 27975
		private const float REFRESH_INTERVAL_TIME = 3f;

		// Token: 0x04006D48 RID: 27976
		private NKCUIFriendTopMenu.FRIEND_TOP_MENU_TYPE m_FRIEND_TOP_MENU_TYPE;

		// Token: 0x04006D49 RID: 27977
		public NKCUIComButton m_cbtnNKM_UI_FRIEND_TOP_SORTING_MENU_01;

		// Token: 0x04006D4A RID: 27978
		public NKCUIComButton m_cbtnNKM_UI_FRIEND_TOP_SORTING_MENU_02;

		// Token: 0x04006D4B RID: 27979
		private NKCUIFriendTopMenu.NKCUIFriendSlotMgr m_NKCUIFriendSlotMgr = new NKCUIFriendTopMenu.NKCUIFriendSlotMgr();

		// Token: 0x04006D4C RID: 27980
		[Header("멘토")]
		public GameObject m_NKM_UI_FRIEND_MENTORING_LIST_INFO;

		// Token: 0x04006D4D RID: 27981
		public GameObject m_NKM_UI_FRIEND_LIST_ScrollView;

		// Token: 0x04006D4E RID: 27982
		public GameObject m_NKM_UI_FRIEND_MENTORIG_LIST_ScrollView;

		// Token: 0x04006D4F RID: 27983
		public GameObject m_MENTORING_INVITE_REWARD_TIME;

		// Token: 0x04006D50 RID: 27984
		public GameObject m_NKM_UI_FRIEND_TOP_SUBMENU_MENTORING;

		// Token: 0x04006D51 RID: 27985
		public Text m_MENTORIG_REWARD_TIME;

		// Token: 0x04006D52 RID: 27986
		public GameObject m_m_NKM_UI_FRIEND_MENTORING_INVITE_ILLUST_VIEW;

		// Token: 0x04006D53 RID: 27987
		public NKCUIComToggle m_NKM_UI_FRIEND_TOP_SUBMENU_MENTORING_LIST;

		// Token: 0x04006D54 RID: 27988
		public NKCUIComToggle m_NKM_UI_FRIEND_TOP_SUBMENU_MENTORING_ADD;

		// Token: 0x04006D55 RID: 27989
		public NKCUIComToggle m_NKM_UI_FRIEND_TOP_SUBMENU_MENTORING_INVITE;

		// Token: 0x04006D56 RID: 27990
		public NKCUIComStateButton m_sbtnNKM_UI_FRIEND_TOP_SUBMENU_MENTORING_INVITE;

		// Token: 0x04006D57 RID: 27991
		public GameObject m_SUBMENU_MENTORING_INVITE_ICON_OFF;

		// Token: 0x04006D58 RID: 27992
		public GameObject m_SUBMENU_MENTORING_INVITE_ICON_OFF_LOCK;

		// Token: 0x04006D59 RID: 27993
		public GameObject m_MENTORING_COUNT;

		// Token: 0x04006D5A RID: 27994
		public Text m_MENTORING_COUNT_TEXT;

		// Token: 0x020018AE RID: 6318
		public enum FRIEND_SORT_TYPE
		{
			// Token: 0x0400A98D RID: 43405
			FST_LAST_LOGIN,
			// Token: 0x0400A98E RID: 43406
			FST_LEVEL
		}

		// Token: 0x020018AF RID: 6319
		public class NKCUIFriendSlotMgr
		{
			// Token: 0x0600B66B RID: 46699 RVA: 0x00366790 File Offset: 0x00364990
			public void Init(Transform parentTransform)
			{
				this.m_ParentTransform = parentTransform;
				for (int i = 0; i < 20; i++)
				{
					NKCUIFriendSlot newInstance = NKCUIFriendSlot.GetNewInstance(this.m_ParentTransform);
					if (newInstance != null)
					{
						this.m_lstNKCUIFriendSlot.Add(newInstance);
						newInstance.SetActive(false);
					}
				}
			}

			// Token: 0x0600B66C RID: 46700 RVA: 0x003667D9 File Offset: 0x003649D9
			public void SetAscend(bool bAscend)
			{
				if (this.m_bAscend == bAscend)
				{
					return;
				}
				this.m_bAscend = bAscend;
				this.Sort();
			}

			// Token: 0x0600B66D RID: 46701 RVA: 0x003667F2 File Offset: 0x003649F2
			public void SetFriendSortType(NKCUIFriendTopMenu.FRIEND_SORT_TYPE _FRIEND_SORT_TYPE)
			{
				if (this.m_FRIEND_SORT_TYPE == _FRIEND_SORT_TYPE)
				{
					return;
				}
				this.m_FRIEND_SORT_TYPE = _FRIEND_SORT_TYPE;
				this.Sort();
			}

			// Token: 0x0600B66E RID: 46702 RVA: 0x0036680C File Offset: 0x00364A0C
			private void Sort()
			{
				if (this.m_bAscend)
				{
					if (this.m_FRIEND_SORT_TYPE == NKCUIFriendTopMenu.FRIEND_SORT_TYPE.FST_LAST_LOGIN)
					{
						NKCUIFriendTopMenu.NKCUIFriendSlotMgr.CompLoginTimeAscending comparer = new NKCUIFriendTopMenu.NKCUIFriendSlotMgr.CompLoginTimeAscending();
						this.m_lstNKCUIFriendSlot.Sort(comparer);
					}
					else if (this.m_FRIEND_SORT_TYPE == NKCUIFriendTopMenu.FRIEND_SORT_TYPE.FST_LEVEL)
					{
						NKCUIFriendTopMenu.NKCUIFriendSlotMgr.CompLevelAscending comparer2 = new NKCUIFriendTopMenu.NKCUIFriendSlotMgr.CompLevelAscending();
						this.m_lstNKCUIFriendSlot.Sort(comparer2);
					}
				}
				else if (this.m_FRIEND_SORT_TYPE == NKCUIFriendTopMenu.FRIEND_SORT_TYPE.FST_LAST_LOGIN)
				{
					NKCUIFriendTopMenu.NKCUIFriendSlotMgr.CompLoginTimeDescending comparer3 = new NKCUIFriendTopMenu.NKCUIFriendSlotMgr.CompLoginTimeDescending();
					this.m_lstNKCUIFriendSlot.Sort(comparer3);
				}
				else if (this.m_FRIEND_SORT_TYPE == NKCUIFriendTopMenu.FRIEND_SORT_TYPE.FST_LEVEL)
				{
					NKCUIFriendTopMenu.NKCUIFriendSlotMgr.CompLevelDescending comparer4 = new NKCUIFriendTopMenu.NKCUIFriendSlotMgr.CompLevelDescending();
					this.m_lstNKCUIFriendSlot.Sort(comparer4);
				}
				for (int i = 0; i < this.m_lstNKCUIFriendSlot.Count; i++)
				{
					NKCUIFriendSlot nkcuifriendSlot = this.m_lstNKCUIFriendSlot[i];
					if (nkcuifriendSlot.IsActive())
					{
						nkcuifriendSlot.transform.SetSiblingIndex(i);
					}
				}
			}

			// Token: 0x0600B66F RID: 46703 RVA: 0x003668CF File Offset: 0x00364ACF
			public NKCUIFriendSlot GetSlot(int index)
			{
				if (index < 0 || index >= this.m_lstNKCUIFriendSlot.Count)
				{
					return null;
				}
				return this.m_lstNKCUIFriendSlot[index];
			}

			// Token: 0x0600B670 RID: 46704 RVA: 0x003668F4 File Offset: 0x00364AF4
			public int GetActiveSlotCount()
			{
				int num = 0;
				for (int i = 0; i < this.m_lstNKCUIFriendSlot.Count; i++)
				{
					NKCUIFriendSlot nkcuifriendSlot = this.m_lstNKCUIFriendSlot[i];
					if (nkcuifriendSlot != null && nkcuifriendSlot.IsActive() && nkcuifriendSlot.GetFriendListData() != null)
					{
						num++;
					}
				}
				return num;
			}

			// Token: 0x0600B671 RID: 46705 RVA: 0x00366944 File Offset: 0x00364B44
			public bool InvalidSlot(long friendCode)
			{
				for (int i = 0; i < this.m_lstNKCUIFriendSlot.Count; i++)
				{
					NKCUIFriendSlot nkcuifriendSlot = this.m_lstNKCUIFriendSlot[i];
					if (nkcuifriendSlot != null && nkcuifriendSlot.IsActive() && nkcuifriendSlot.GetFriendListData() != null && nkcuifriendSlot.GetFriendListData().commonProfile.friendCode == friendCode)
					{
						nkcuifriendSlot.SetActive(false);
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600B672 RID: 46706 RVA: 0x003669AC File Offset: 0x00364BAC
			public void Clear()
			{
				for (int i = 0; i < this.m_lstNKCUIFriendSlot.Count; i++)
				{
					NKCUIFriendSlot nkcuifriendSlot = this.m_lstNKCUIFriendSlot[i];
					if (nkcuifriendSlot != null)
					{
						nkcuifriendSlot.SetActive(false);
					}
				}
			}

			// Token: 0x0600B673 RID: 46707 RVA: 0x003669EC File Offset: 0x00364BEC
			public void CloseSlotInstance()
			{
				foreach (NKCUIFriendSlot nkcuifriendSlot in this.m_lstNKCUIFriendSlot)
				{
					if (nkcuifriendSlot != null)
					{
						nkcuifriendSlot.Clear();
					}
				}
			}

			// Token: 0x0600B674 RID: 46708 RVA: 0x00366A44 File Offset: 0x00364C44
			public void Add(FriendListData friend, NKCUIFriendSlot.FRIEND_SLOT_TYPE slotType)
			{
				if (this.GetActiveSlotCount() <= this.m_lstNKCUIFriendSlot.Count)
				{
					NKCUIFriendSlot newInstance = NKCUIFriendSlot.GetNewInstance(this.m_ParentTransform);
					if (newInstance != null)
					{
						this.m_lstNKCUIFriendSlot.Add(newInstance);
						newInstance.SetActive(false);
					}
				}
				if (friend != null)
				{
					this.m_lstNKCUIFriendSlot[this.GetActiveSlotCount()].SetData(slotType, friend);
				}
				for (int i = this.GetActiveSlotCount(); i < this.m_lstNKCUIFriendSlot.Count; i++)
				{
					this.m_lstNKCUIFriendSlot[i].SetActive(false);
				}
				this.Sort();
			}

			// Token: 0x0600B675 RID: 46709 RVA: 0x00366ADC File Offset: 0x00364CDC
			public void SetForSearch(bool bRecommend, List<FriendListData> friend_list)
			{
				if (friend_list.Count > this.m_lstNKCUIFriendSlot.Count)
				{
					int count = this.m_lstNKCUIFriendSlot.Count;
					for (int i = 0; i < friend_list.Count - count; i++)
					{
						NKCUIFriendSlot newInstance = NKCUIFriendSlot.GetNewInstance(this.m_ParentTransform);
						if (newInstance != null)
						{
							this.m_lstNKCUIFriendSlot.Add(newInstance);
							newInstance.SetActive(false);
						}
					}
				}
				int j;
				for (j = 0; j < friend_list.Count; j++)
				{
					FriendListData friendListData = friend_list[j];
					if (friendListData != null)
					{
						if (bRecommend)
						{
							this.m_lstNKCUIFriendSlot[j].SetData(NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_FRIEND_SEARCH_RECOMMEND, friendListData);
						}
						else
						{
							this.m_lstNKCUIFriendSlot[j].SetData(NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_FRIEND_SEARCH, friendListData);
						}
					}
				}
				for (int k = j; k < this.m_lstNKCUIFriendSlot.Count; k++)
				{
					this.m_lstNKCUIFriendSlot[k].SetActive(false);
				}
				this.Sort();
			}

			// Token: 0x0600B676 RID: 46710 RVA: 0x00366BC4 File Offset: 0x00364DC4
			public void Set(NKM_FRIEND_LIST_TYPE list_type, List<FriendListData> friend_list)
			{
				if (friend_list.Count > this.m_lstNKCUIFriendSlot.Count)
				{
					int count = this.m_lstNKCUIFriendSlot.Count;
					for (int i = 0; i < friend_list.Count - count; i++)
					{
						NKCUIFriendSlot newInstance = NKCUIFriendSlot.GetNewInstance(this.m_ParentTransform);
						if (newInstance != null)
						{
							this.m_lstNKCUIFriendSlot.Add(newInstance);
							newInstance.SetActive(false);
						}
					}
				}
				int j;
				for (j = 0; j < friend_list.Count; j++)
				{
					FriendListData friendListData = friend_list[j];
					if (friendListData != null)
					{
						NKCUIFriendSlot.FRIEND_SLOT_TYPE slotType = NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_FRIEND_LIST;
						if (list_type == NKM_FRIEND_LIST_TYPE.FRIEND)
						{
							slotType = NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_FRIEND_LIST;
						}
						else if (list_type == NKM_FRIEND_LIST_TYPE.BLOCKER)
						{
							slotType = NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_BLOCK_LIST;
						}
						else if (list_type == NKM_FRIEND_LIST_TYPE.RECEIVE_REQUEST)
						{
							slotType = NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_RECEIVE_REQ;
						}
						else if (list_type == NKM_FRIEND_LIST_TYPE.SEND_REQUEST)
						{
							slotType = NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_SENT_REQ;
						}
						this.m_lstNKCUIFriendSlot[j].SetData(slotType, friendListData);
					}
				}
				for (int k = j; k < this.m_lstNKCUIFriendSlot.Count; k++)
				{
					this.m_lstNKCUIFriendSlot[k].SetActive(false);
				}
				this.Sort();
			}

			// Token: 0x0400A98F RID: 43407
			private const int DEFAULT_SIZE = 20;

			// Token: 0x0400A990 RID: 43408
			private List<NKCUIFriendSlot> m_lstNKCUIFriendSlot = new List<NKCUIFriendSlot>();

			// Token: 0x0400A991 RID: 43409
			private Transform m_ParentTransform;

			// Token: 0x0400A992 RID: 43410
			private bool m_bAscend;

			// Token: 0x0400A993 RID: 43411
			private NKCUIFriendTopMenu.FRIEND_SORT_TYPE m_FRIEND_SORT_TYPE;

			// Token: 0x02001A90 RID: 6800
			public class CompLevelDescending : IComparer<NKCUIFriendSlot>
			{
				// Token: 0x0600BC59 RID: 48217 RVA: 0x0036FCC2 File Offset: 0x0036DEC2
				public int Compare(NKCUIFriendSlot x, NKCUIFriendSlot y)
				{
					if (!x.IsActive())
					{
						return 1;
					}
					if (!y.IsActive())
					{
						return -1;
					}
					if (y.GetFriendListData().commonProfile.level >= x.GetFriendListData().commonProfile.level)
					{
						return 1;
					}
					return -1;
				}
			}

			// Token: 0x02001A91 RID: 6801
			public class CompLevelAscending : IComparer<NKCUIFriendSlot>
			{
				// Token: 0x0600BC5B RID: 48219 RVA: 0x0036FD05 File Offset: 0x0036DF05
				public int Compare(NKCUIFriendSlot x, NKCUIFriendSlot y)
				{
					if (!x.IsActive())
					{
						return 1;
					}
					if (!y.IsActive())
					{
						return -1;
					}
					if (y.GetFriendListData().commonProfile.level <= x.GetFriendListData().commonProfile.level)
					{
						return 1;
					}
					return -1;
				}
			}

			// Token: 0x02001A92 RID: 6802
			public class CompLoginTimeDescending : IComparer<NKCUIFriendSlot>
			{
				// Token: 0x0600BC5D RID: 48221 RVA: 0x0036FD48 File Offset: 0x0036DF48
				public int Compare(NKCUIFriendSlot x, NKCUIFriendSlot y)
				{
					if (!x.IsActive())
					{
						return 1;
					}
					if (!y.IsActive())
					{
						return -1;
					}
					if (y.GetFriendListData().lastLoginDate <= x.GetFriendListData().lastLoginDate)
					{
						return 1;
					}
					return -1;
				}
			}

			// Token: 0x02001A93 RID: 6803
			public class CompLoginTimeAscending : IComparer<NKCUIFriendSlot>
			{
				// Token: 0x0600BC5F RID: 48223 RVA: 0x0036FD86 File Offset: 0x0036DF86
				public int Compare(NKCUIFriendSlot x, NKCUIFriendSlot y)
				{
					if (!x.IsActive())
					{
						return 1;
					}
					if (!y.IsActive())
					{
						return -1;
					}
					if (y.GetFriendListData().lastLoginDate >= x.GetFriendListData().lastLoginDate)
					{
						return 1;
					}
					return -1;
				}
			}
		}

		// Token: 0x020018B0 RID: 6320
		public enum FRIEND_TOP_MENU_TYPE
		{
			// Token: 0x0400A995 RID: 43413
			FTMT_MANAGE,
			// Token: 0x0400A996 RID: 43414
			FTMT_REGISTER,
			// Token: 0x0400A997 RID: 43415
			FTMT_MY_PROFILE
		}
	}
}
