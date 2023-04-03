using System;
using System.Collections.Generic;
using ClientPacket.Common;
using NKC.UI.Gauntlet;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000957 RID: 2391
	public class NKCUIGauntletPrivateRoomInvite : NKCUIBase
	{
		// Token: 0x17001124 RID: 4388
		// (get) Token: 0x06005F54 RID: 24404 RVA: 0x001D9F03 File Offset: 0x001D8103
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001125 RID: 4389
		// (get) Token: 0x06005F55 RID: 24405 RVA: 0x001D9F06 File Offset: 0x001D8106
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_GAUNTLET;
			}
		}

		// Token: 0x06005F56 RID: 24406 RVA: 0x001D9F0D File Offset: 0x001D810D
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06005F57 RID: 24407 RVA: 0x001D9F1B File Offset: 0x001D811B
		public bool IsGuildOpened()
		{
			if (NKCGuildManager.MyGuildData == null)
			{
				Debug.Log("[PrivatePVP] NKCGuildManager.MyGuildData is null");
				return false;
			}
			if (NKCGuildManager.MyGuildMemberDataList == null)
			{
				Debug.Log("[PrivatePVP] NKCGuildManager.MyGuildMemberDataList is null");
				return false;
			}
			return NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_GUILD);
		}

		// Token: 0x06005F58 RID: 24408 RVA: 0x001D9F4A File Offset: 0x001D814A
		public void Open()
		{
			base.gameObject.SetActive(true);
			this.SetUI();
			base.UIOpened(true);
		}

		// Token: 0x06005F59 RID: 24409 RVA: 0x001D9F68 File Offset: 0x001D8168
		public void Init()
		{
			NKCPrivatePVPRoomMgr.ResetSearchData();
			if (this.m_cbtnClose != null)
			{
				this.m_cbtnClose.PointerClick.RemoveAllListeners();
				this.m_cbtnClose.PointerClick.AddListener(new UnityAction(base.Close));
			}
			if (this.m_evtBG != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(new UnityAction<BaseEventData>(this.OnClickBg));
				this.m_evtBG.triggers.Add(entry);
			}
			this.m_ctglRankFriendTab.OnValueChanged.RemoveAllListeners();
			this.m_ctglRankFriendTab.OnValueChanged.AddListener(new UnityAction<bool>(this.OnTabChangedToFriend));
			this.m_lvsrRankFriend.dOnGetObject += this.GetSlotFriend;
			this.m_lvsrRankFriend.dOnReturnObject += this.ReturnSlot;
			this.m_lvsrRankFriend.dOnProvideData += this.ProvideSlotData;
			this.m_lvsrRankFriend.ContentConstraintCount = 1;
			NKCUtil.SetScrollHotKey(this.m_lvsrRankFriend, null);
			if (this.m_ctglTabSearchID != null)
			{
				this.m_ctglTabSearchID.OnValueChanged.RemoveAllListeners();
				this.m_ctglTabSearchID.OnValueChanged.AddListener(new UnityAction<bool>(this.OnTabChangedToSearchID));
				this.m_scrollSearchID.dOnGetObject += this.GetSlotSearch;
				this.m_scrollSearchID.dOnReturnObject += this.ReturnSlot;
				this.m_scrollSearchID.dOnProvideData += this.ProvideSlotData;
				this.m_scrollSearchID.ContentConstraintCount = 1;
				NKCUtil.SetScrollHotKey(this.m_scrollSearchID, null);
			}
			NKCUtil.SetGameobjectActive(this.m_ctglTabSearchID, NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_SEARCH));
			if (this.m_ctglTabGuild != null)
			{
				this.m_ctglTabGuild.OnValueChanged.RemoveAllListeners();
				this.m_ctglTabGuild.OnValueChanged.AddListener(new UnityAction<bool>(this.OnTabChangedToGuild));
				this.m_scrollGuild.dOnGetObject += this.GetSlotGuild;
				this.m_scrollGuild.dOnReturnObject += this.ReturnSlot;
				this.m_scrollGuild.dOnProvideData += this.ProvideSlotDataGuild;
				this.m_scrollGuild.ContentConstraintCount = 1;
				NKCUtil.SetScrollHotKey(this.m_scrollGuild, null);
			}
			NKCUtil.SetGameobjectActive(this.m_ctglTabGuild, NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_GUILD));
			if (this.m_IFNicknameOrUID != null)
			{
				this.m_IFNicknameOrUID.onEndEdit.RemoveAllListeners();
				this.m_IFNicknameOrUID.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditSearch));
			}
			if (this.m_cbtnNKM_UI_FRIEND_TOP_SEARCH_BUTTON != null)
			{
				this.m_cbtnNKM_UI_FRIEND_TOP_SEARCH_BUTTON.PointerClick.RemoveAllListeners();
				this.m_cbtnNKM_UI_FRIEND_TOP_SEARCH_BUTTON.PointerClick.AddListener(new UnityAction(this.OnClickSearch));
			}
			if (this.m_cbtnNKM_UI_FRIEND_TOP_SUBMENU_REFRESH != null)
			{
				NKCUtil.SetGameobjectActive(this.m_cbtnNKM_UI_FRIEND_TOP_SUBMENU_REFRESH, false);
				this.m_cbtnNKM_UI_FRIEND_TOP_SUBMENU_REFRESH.PointerClick.RemoveAllListeners();
				this.m_cbtnNKM_UI_FRIEND_TOP_SUBMENU_REFRESH.PointerClick.AddListener(new UnityAction(this.OnClickRefresh));
			}
			NKCUIComButton cbtnNKM_UI_FRIEND_TOP_SEARCH_BUTTON_DISABLE = this.m_cbtnNKM_UI_FRIEND_TOP_SEARCH_BUTTON_DISABLE;
			if (cbtnNKM_UI_FRIEND_TOP_SEARCH_BUTTON_DISABLE != null)
			{
				cbtnNKM_UI_FRIEND_TOP_SEARCH_BUTTON_DISABLE.Lock();
			}
			NKCUIComButton cbtnNKM_UI_FRIEND_TOP_SUBMENU_REFRESH_Disable = this.m_cbtnNKM_UI_FRIEND_TOP_SUBMENU_REFRESH_Disable;
			if (cbtnNKM_UI_FRIEND_TOP_SUBMENU_REFRESH_Disable == null)
			{
				return;
			}
			cbtnNKM_UI_FRIEND_TOP_SUBMENU_REFRESH_Disable.Lock();
		}

		// Token: 0x06005F5A RID: 24410 RVA: 0x001DA2B4 File Offset: 0x001D84B4
		private void RefreshTabUI()
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			Debug.Log("[PrivatePVP] RefreshTabUI [" + this.m_currentTabType.ToString() + "]");
			NKCUtil.SetGameobjectActive(this.m_objRankFriend, this.m_currentTabType == NKCUIGauntletPrivateRoomInvite.PRIVATE_PVP_TAB_TYPE.FRIEND);
			NKCUtil.SetGameobjectActive(this.m_objSearchID, this.m_currentTabType == NKCUIGauntletPrivateRoomInvite.PRIVATE_PVP_TAB_TYPE.SEARCH);
			NKCUtil.SetGameobjectActive(this.m_objGuild, this.m_currentTabType == NKCUIGauntletPrivateRoomInvite.PRIVATE_PVP_TAB_TYPE.GUILD);
			this.RefreshTabCells();
			if (this.m_bPlayIntro)
			{
				this.m_bPlayIntro = false;
			}
		}

		// Token: 0x06005F5B RID: 24411 RVA: 0x001DA345 File Offset: 0x001D8545
		private void OnTabChangedToFriend(bool bSet)
		{
			if (bSet)
			{
				this.m_currentTabType = NKCUIGauntletPrivateRoomInvite.PRIVATE_PVP_TAB_TYPE.FRIEND;
				this.UpdateFriendList();
			}
			this.RefreshTabUI();
		}

		// Token: 0x06005F5C RID: 24412 RVA: 0x001DA35D File Offset: 0x001D855D
		private void OnTabChangedToSearchID(bool bSet)
		{
			if (bSet)
			{
				this.m_currentTabType = NKCUIGauntletPrivateRoomInvite.PRIVATE_PVP_TAB_TYPE.SEARCH;
				this.UpdateSearchList();
			}
			this.RefreshTabUI();
			if (NKCPrivatePVPRoomMgr.SearchResult.Count == 0)
			{
				this.OnClickSearch();
			}
		}

		// Token: 0x06005F5D RID: 24413 RVA: 0x001DA387 File Offset: 0x001D8587
		private void OnTabChangedToGuild(bool bSet)
		{
			Debug.Log(string.Format("[PrivatePVP] OnTabChangedToGuild [{0}]", bSet));
			if (bSet)
			{
				this.m_currentTabType = NKCUIGauntletPrivateRoomInvite.PRIVATE_PVP_TAB_TYPE.GUILD;
				this.UpdateGuildList();
			}
			this.RefreshTabUI();
		}

		// Token: 0x06005F5E RID: 24414 RVA: 0x001DA3B4 File Offset: 0x001D85B4
		private void UpdateReadyButtonUI()
		{
		}

		// Token: 0x06005F5F RID: 24415 RVA: 0x001DA3B8 File Offset: 0x001D85B8
		private void Update()
		{
			if (this.m_minimumSearchDelay > 0f)
			{
				this.m_minimumSearchDelay -= Time.deltaTime;
				if (this.m_minimumSearchDelay <= 0f)
				{
					this.m_minimumSearchDelay = 0f;
					NKCUtil.SetGameobjectActive(this.m_cbtnNKM_UI_FRIEND_TOP_SEARCH_BUTTON, true);
					NKCUtil.SetGameobjectActive(this.m_cbtnNKM_UI_FRIEND_TOP_SEARCH_BUTTON_DISABLE, false);
				}
			}
			if (this.m_minimumRefreshDelay > 0f)
			{
				this.m_minimumRefreshDelay -= Time.deltaTime;
				if (this.m_minimumRefreshDelay <= 0f)
				{
					this.m_minimumRefreshDelay = 0f;
					NKCUtil.SetGameobjectActive(this.m_cbtnNKM_UI_FRIEND_TOP_SUBMENU_REFRESH, true);
					NKCUtil.SetGameobjectActive(this.m_cbtnNKM_UI_FRIEND_TOP_SUBMENU_REFRESH_Disable, false);
				}
			}
			if (this.m_fPrevUpdateTime + 1f < Time.time)
			{
				this.m_fPrevUpdateTime = Time.time;
				this.UpdateReadyButtonUI();
			}
		}

		// Token: 0x06005F60 RID: 24416 RVA: 0x001DA487 File Offset: 0x001D8687
		private void OnEventPanelBeginDragFriend()
		{
		}

		// Token: 0x06005F61 RID: 24417 RVA: 0x001DA48C File Offset: 0x001D868C
		public void ReturnSlot(Transform tr)
		{
			NKCUIGauntletPrivateRoomFriendSlot component = tr.GetComponent<NKCUIGauntletPrivateRoomFriendSlot>();
			tr.SetParent(base.transform);
			if (component != null)
			{
				component.DestoryInstance();
				return;
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x06005F62 RID: 24418 RVA: 0x001DA4C7 File Offset: 0x001D86C7
		public RectTransform GetSlotFriend(int index)
		{
			NKCUIGauntletPrivateRoomFriendSlot newInstance = NKCUIGauntletPrivateRoomFriendSlot.GetNewInstance(this.m_trRankFriend, new NKCUIGauntletPrivateRoomFriendSlot.OnDragBegin(this.OnEventPanelBeginDragFriend));
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x06005F63 RID: 24419 RVA: 0x001DA4EC File Offset: 0x001D86EC
		public void ProvideSlotData(Transform tr, int index)
		{
			NKCUIGauntletPrivateRoomFriendSlot component = tr.GetComponent<NKCUIGauntletPrivateRoomFriendSlot>();
			if (component != null)
			{
				component.SetUI(this.GetUserSimpleData(index), true);
			}
		}

		// Token: 0x06005F64 RID: 24420 RVA: 0x001DA518 File Offset: 0x001D8718
		public void ProvideSlotDataGuild(Transform tr, int index)
		{
			NKCUIGauntletPrivateRoomFriendSlot component = tr.GetComponent<NKCUIGauntletPrivateRoomFriendSlot>();
			if (component != null)
			{
				component.SetUI(this.GetUserSimpleDataGuild(index), true);
			}
		}

		// Token: 0x06005F65 RID: 24421 RVA: 0x001DA543 File Offset: 0x001D8743
		public RectTransform GetSlotGuild(int index)
		{
			NKCUIGauntletPrivateRoomFriendSlot newInstance = NKCUIGauntletPrivateRoomFriendSlot.GetNewInstance(this.m_scrollContentGuild, new NKCUIGauntletPrivateRoomFriendSlot.OnDragBegin(this.OnEventPanelBeginDragFriend));
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x06005F66 RID: 24422 RVA: 0x001DA567 File Offset: 0x001D8767
		public RectTransform GetSlotSearch(int index)
		{
			NKCUIGauntletPrivateRoomFriendSlot newInstance = NKCUIGauntletPrivateRoomFriendSlot.GetNewInstance(this.m_scrollContentSearchID, new NKCUIGauntletPrivateRoomFriendSlot.OnDragBegin(this.OnEventPanelBeginDragFriend));
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x06005F67 RID: 24423 RVA: 0x001DA58C File Offset: 0x001D878C
		private void RefreshTabCells()
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			switch (this.m_currentTabType)
			{
			case NKCUIGauntletPrivateRoomInvite.PRIVATE_PVP_TAB_TYPE.FRIEND:
				this.RefreshScrollRectFriend();
				return;
			case NKCUIGauntletPrivateRoomInvite.PRIVATE_PVP_TAB_TYPE.GUILD:
				this.RefreshScrollRectGuild();
				return;
			case NKCUIGauntletPrivateRoomInvite.PRIVATE_PVP_TAB_TYPE.SEARCH:
				this.RefreshScrollRectSearch();
				return;
			default:
				return;
			}
		}

		// Token: 0x06005F68 RID: 24424 RVA: 0x001DA5D8 File Offset: 0x001D87D8
		private void RefreshScrollRectFriend()
		{
			this.m_lvsrRankFriend.TotalCount = this.m_friendSlotDataList.Count;
			if (!this.m_bFriendTabOpened)
			{
				this.m_bFriendTabOpened = true;
				this.m_lvsrRankFriend.velocity = Vector2.zero;
				this.m_lvsrRankFriend.SetIndexPosition(0);
			}
			else
			{
				this.m_lvsrRankFriend.RefreshCells(false);
			}
			NKCUtil.SetLabelText(this.m_lbEmptyMessage, NKCUtilString.GET_STRING_FRIEND_LIST_IS_EMPTY);
			NKCUtil.SetGameobjectActive(this.m_objEmptyList, this.m_lvsrRankFriend.TotalCount == 0);
		}

		// Token: 0x06005F69 RID: 24425 RVA: 0x001DA660 File Offset: 0x001D8860
		private void RefreshScrollRectSearch()
		{
			this.m_scrollSearchID.TotalCount = this.m_friendSlotDataList.Count;
			this.m_scrollSearchID.SetIndexPosition(0);
			if (!this.m_bSearchTabOpened)
			{
				this.m_bSearchTabOpened = true;
				this.m_scrollSearchID.velocity = Vector2.zero;
				return;
			}
			this.m_scrollSearchID.RefreshCells(false);
		}

		// Token: 0x06005F6A RID: 24426 RVA: 0x001DA6BC File Offset: 0x001D88BC
		private void RefreshScrollRectGuild()
		{
			this.m_scrollGuild.TotalCount = 0;
			if (this.IsGuildOpened())
			{
				this.m_scrollGuild.TotalCount = NKCGuildManager.MyGuildMemberDataList.Count;
			}
			if (!this.m_bGuildTabOpened)
			{
				this.m_bGuildTabOpened = true;
				this.m_scrollGuild.velocity = Vector2.zero;
				this.m_scrollGuild.SetIndexPosition(0);
			}
			else
			{
				this.m_scrollGuild.RefreshCells(false);
			}
			NKCUtil.SetLabelText(this.m_lbEmptyMessageGuild, NKCUtilString.GET_STRING_PRIVATE_PVP_GUILD_MEMBER_EMPTY);
			NKCUtil.SetGameobjectActive(this.m_objEmptyListGuild, this.m_scrollGuild.TotalCount == 0);
		}

		// Token: 0x06005F6B RID: 24427 RVA: 0x001DA754 File Offset: 0x001D8954
		public void SetUI()
		{
			this.m_bPlayIntro = true;
			if (!this.m_bPrepareLoopScrollCells)
			{
				NKCUtil.SetGameobjectActive(this.m_objRankFriend, true);
				NKCUtil.SetGameobjectActive(this.m_objSearchID, NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_SEARCH));
				NKCUtil.SetGameobjectActive(this.m_objGuild, NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_GUILD));
				LoopVerticalScrollRect lvsrRankFriend = this.m_lvsrRankFriend;
				if (lvsrRankFriend != null)
				{
					lvsrRankFriend.PrepareCells(0);
				}
				LoopVerticalScrollRect scrollSearchID = this.m_scrollSearchID;
				if (scrollSearchID != null)
				{
					scrollSearchID.PrepareCells(0);
				}
				LoopVerticalScrollRect scrollGuild = this.m_scrollGuild;
				if (scrollGuild != null)
				{
					scrollGuild.PrepareCells(0);
				}
				this.m_bPrepareLoopScrollCells = true;
			}
			if (this.m_bFirstOpen)
			{
				this.m_bFriendTabOpened = false;
				this.m_bSearchTabOpened = false;
				this.m_bGuildTabOpened = false;
				this.m_bFirstOpen = false;
			}
			switch (this.m_currentTabType)
			{
			case NKCUIGauntletPrivateRoomInvite.PRIVATE_PVP_TAB_TYPE.FRIEND:
				this.m_ctglRankFriendTab.Select(false, true, false);
				this.m_ctglRankFriendTab.Select(true, false, false);
				NKCUtil.SetGameobjectActive(this.m_objRankFriend, true);
				this.UpdateFriendList();
				break;
			case NKCUIGauntletPrivateRoomInvite.PRIVATE_PVP_TAB_TYPE.GUILD:
				this.m_ctglTabGuild.Select(false, true, false);
				this.m_ctglTabGuild.Select(true, false, false);
				NKCUtil.SetGameobjectActive(this.m_objGuild, true);
				this.UpdateGuildList();
				break;
			case NKCUIGauntletPrivateRoomInvite.PRIVATE_PVP_TAB_TYPE.SEARCH:
				this.m_ctglTabSearchID.Select(false, true, false);
				this.m_ctglTabSearchID.Select(true, false, false);
				NKCUtil.SetGameobjectActive(this.m_objSearchID, true);
				this.UpdateSearchList();
				break;
			}
			this.UpdateReadyButtonUI();
			this.RefreshTabCells();
			if (this.m_bPlayIntro)
			{
				this.m_bPlayIntro = false;
			}
		}

		// Token: 0x06005F6C RID: 24428 RVA: 0x001DA8CE File Offset: 0x001D8ACE
		public void ClearCacheData()
		{
			this.m_lvsrRankFriend.ClearCells();
			this.m_scrollSearchID.ClearCells();
		}

		// Token: 0x06005F6D RID: 24429 RVA: 0x001DA8E6 File Offset: 0x001D8AE6
		private FriendListData GetUserSimpleData(int index)
		{
			if (this.m_friendSlotDataList.Count > index)
			{
				return this.m_friendSlotDataList[index];
			}
			return null;
		}

		// Token: 0x06005F6E RID: 24430 RVA: 0x001DA904 File Offset: 0x001D8B04
		private FriendListData GetUserSimpleDataGuild(int index)
		{
			if (!this.IsGuildOpened())
			{
				return null;
			}
			Debug.Log(string.Format("[PrivatePVP] GetUserSimpleDataGuild index[{0}] / total[{1}]", index, NKCGuildManager.MyGuildMemberDataList.Count));
			if (NKCGuildManager.MyGuildMemberDataList.Count <= index)
			{
				return null;
			}
			FriendListData friendListData = new FriendListData();
			friendListData.commonProfile = NKCGuildManager.MyGuildMemberDataList[index].commonProfile;
			friendListData.lastLoginDate = NKCGuildManager.MyGuildMemberDataList[index].lastOnlineTime;
			if (friendListData.commonProfile == null)
			{
				Debug.Log("[PrivatePVP] commonProfile is null");
				return null;
			}
			DateTime lastLoginDate = friendListData.lastLoginDate;
			return friendListData;
		}

		// Token: 0x06005F6F RID: 24431 RVA: 0x001DA99C File Offset: 0x001D8B9C
		public void UpdateFriendList()
		{
			this.m_friendSlotDataList.Clear();
			this.m_friendSlotDataList.AddRange(NKCFriendManager.FriendListData);
			this.m_friendSlotDataList.Sort((FriendListData a, FriendListData b) => b.lastLoginDate.CompareTo(a.lastLoginDate));
		}

		// Token: 0x06005F70 RID: 24432 RVA: 0x001DA9F0 File Offset: 0x001D8BF0
		public void UpdateSearchList()
		{
			this.m_friendSlotDataList.Clear();
			this.m_friendSlotDataList.AddRange(NKCPrivatePVPRoomMgr.SearchResult);
			this.m_friendSlotDataList.Sort((FriendListData a, FriendListData b) => b.lastLoginDate.CompareTo(a.lastLoginDate));
		}

		// Token: 0x06005F71 RID: 24433 RVA: 0x001DAA44 File Offset: 0x001D8C44
		public void UpdateGuildList()
		{
			Debug.Log("[PrivatePVP] UpdateGuildList");
			this.m_friendSlotDataList.Clear();
			if (!this.IsGuildOpened())
			{
				return;
			}
			for (int i = 0; i < NKCGuildManager.MyGuildMemberDataList.Count; i++)
			{
				FriendListData userSimpleDataGuild = this.GetUserSimpleDataGuild(i);
				if (userSimpleDataGuild != null)
				{
					this.m_friendSlotDataList.Add(userSimpleDataGuild);
				}
			}
			this.m_friendSlotDataList.Sort((FriendListData a, FriendListData b) => b.lastLoginDate.CompareTo(a.lastLoginDate));
		}

		// Token: 0x06005F72 RID: 24434 RVA: 0x001DAAC5 File Offset: 0x001D8CC5
		public void UpdateRoomList()
		{
		}

		// Token: 0x06005F73 RID: 24435 RVA: 0x001DAAC8 File Offset: 0x001D8CC8
		public void OnClickSearch()
		{
			if (this.m_minimumSearchDelay > 0f)
			{
				return;
			}
			this.UpdateSearchList();
			NKCPrivatePVPRoomMgr.Send_NKMPacket_PRIVATE_PVP_SEARCH_USER_REQ(this.m_IFNicknameOrUID.text);
			this.m_minimumSearchDelay = 9f;
			NKCUtil.SetGameobjectActive(this.m_cbtnNKM_UI_FRIEND_TOP_SEARCH_BUTTON, false);
			NKCUtil.SetGameobjectActive(this.m_cbtnNKM_UI_FRIEND_TOP_SEARCH_BUTTON_DISABLE, true);
		}

		// Token: 0x06005F74 RID: 24436 RVA: 0x001DAB1C File Offset: 0x001D8D1C
		public void OnClickRefresh()
		{
			if (this.m_minimumRefreshDelay > 0f)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_cbtnNKM_UI_FRIEND_TOP_SUBMENU_REFRESH, false);
			NKCUtil.SetGameobjectActive(this.m_cbtnNKM_UI_FRIEND_TOP_SUBMENU_REFRESH_Disable, true);
			NKCPrivatePVPRoomMgr.Send_NKMPacket_PRIVATE_PVP_SEARCH_USER_REQ("");
			this.m_minimumRefreshDelay = 9f;
		}

		// Token: 0x06005F75 RID: 24437 RVA: 0x001DAB59 File Offset: 0x001D8D59
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

		// Token: 0x06005F76 RID: 24438 RVA: 0x001DAB80 File Offset: 0x001D8D80
		private void OnClickBg(BaseEventData cBaseEventData)
		{
			base.Close();
		}

		// Token: 0x04004B67 RID: 19303
		public const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_GAUNTLET";

		// Token: 0x04004B68 RID: 19304
		public const string UI_ASSET_NAME = "NKM_UI_GAUNTLET_PRIVATE_ROOM_INVITE_POPUP";

		// Token: 0x04004B69 RID: 19305
		[Header("친구")]
		public NKCUIComToggle m_ctglRankFriendTab;

		// Token: 0x04004B6A RID: 19306
		public GameObject m_objRankFriend;

		// Token: 0x04004B6B RID: 19307
		public LoopVerticalScrollRect m_lvsrRankFriend;

		// Token: 0x04004B6C RID: 19308
		public Transform m_trRankFriend;

		// Token: 0x04004B6D RID: 19309
		public GameObject m_objEmptyList;

		// Token: 0x04004B6E RID: 19310
		public Text m_lbEmptyMessage;

		// Token: 0x04004B6F RID: 19311
		[Header("검색")]
		public NKCUIComToggle m_ctglTabSearchID;

		// Token: 0x04004B70 RID: 19312
		public GameObject m_objSearchID;

		// Token: 0x04004B71 RID: 19313
		public LoopVerticalScrollRect m_scrollSearchID;

		// Token: 0x04004B72 RID: 19314
		public Transform m_scrollContentSearchID;

		// Token: 0x04004B73 RID: 19315
		public NKCUIComButton m_cbtnNKM_UI_FRIEND_TOP_SEARCH_BUTTON;

		// Token: 0x04004B74 RID: 19316
		public NKCUIComButton m_cbtnNKM_UI_FRIEND_TOP_SEARCH_BUTTON_DISABLE;

		// Token: 0x04004B75 RID: 19317
		public NKCUIComButton m_cbtnNKM_UI_FRIEND_TOP_SUBMENU_REFRESH;

		// Token: 0x04004B76 RID: 19318
		public NKCUIComButton m_cbtnNKM_UI_FRIEND_TOP_SUBMENU_REFRESH_Disable;

		// Token: 0x04004B77 RID: 19319
		public InputField m_IFNicknameOrUID;

		// Token: 0x04004B78 RID: 19320
		private float m_minimumSearchDelay;

		// Token: 0x04004B79 RID: 19321
		private float m_minimumRefreshDelay;

		// Token: 0x04004B7A RID: 19322
		private const float MIN_SEARCH_DELAY = 9f;

		// Token: 0x04004B7B RID: 19323
		private const float MIN_REFRESH_DELAY = 9f;

		// Token: 0x04004B7C RID: 19324
		[Header("연합원")]
		public NKCUIComToggle m_ctglTabGuild;

		// Token: 0x04004B7D RID: 19325
		public GameObject m_objGuild;

		// Token: 0x04004B7E RID: 19326
		public LoopVerticalScrollRect m_scrollGuild;

		// Token: 0x04004B7F RID: 19327
		public Transform m_scrollContentGuild;

		// Token: 0x04004B80 RID: 19328
		public GameObject m_objEmptyListGuild;

		// Token: 0x04004B81 RID: 19329
		public Text m_lbEmptyMessageGuild;

		// Token: 0x04004B82 RID: 19330
		private NKCUIGauntletPrivateRoomInvite.PRIVATE_PVP_TAB_TYPE m_currentTabType;

		// Token: 0x04004B83 RID: 19331
		private bool m_bFirstOpen = true;

		// Token: 0x04004B84 RID: 19332
		private bool m_bPrepareLoopScrollCells;

		// Token: 0x04004B85 RID: 19333
		private bool m_bFriendTabOpened;

		// Token: 0x04004B86 RID: 19334
		private bool m_bSearchTabOpened;

		// Token: 0x04004B87 RID: 19335
		private bool m_bGuildTabOpened;

		// Token: 0x04004B88 RID: 19336
		private float m_fPrevUpdateTime;

		// Token: 0x04004B89 RID: 19337
		[Header("공통")]
		public NKCUIComStateButton m_cbtnClose;

		// Token: 0x04004B8A RID: 19338
		public EventTrigger m_evtBG;

		// Token: 0x04004B8B RID: 19339
		private bool m_bPlayIntro = true;

		// Token: 0x04004B8C RID: 19340
		private List<FriendListData> m_friendSlotDataList = new List<FriendListData>();

		// Token: 0x020015D9 RID: 5593
		private enum PRIVATE_PVP_TAB_TYPE
		{
			// Token: 0x0400A2AB RID: 41643
			FRIEND,
			// Token: 0x0400A2AC RID: 41644
			GUILD,
			// Token: 0x0400A2AD RID: 41645
			SEARCH
		}
	}
}
