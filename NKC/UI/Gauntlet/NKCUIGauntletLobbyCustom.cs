using System;
using System.Collections.Generic;
using ClientPacket.Common;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B75 RID: 2933
	public class NKCUIGauntletLobbyCustom : MonoBehaviour
	{
		// Token: 0x060086C4 RID: 34500 RVA: 0x002D9A1B File Offset: 0x002D7C1B
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

		// Token: 0x060086C5 RID: 34501 RVA: 0x002D9A4C File Offset: 0x002D7C4C
		public void Init()
		{
			NKCPrivatePVPMgr.ResetData();
			this.m_csbtnBattleHistory.PointerClick.RemoveAllListeners();
			this.m_csbtnBattleHistory.PointerClick.AddListener(new UnityAction(this.OnClickBattleRecord));
			this.m_csbtnRankMatchReady.PointerClick.RemoveAllListeners();
			this.m_csbtnRankMatchReadyDisable.PointerClick.RemoveAllListeners();
			NKCUtil.SetBindFunction(this.m_csbtnEmoticonSetting, new UnityAction(this.OnClickEmoticonSetting));
			NKCUtil.SetBindFunction(this.m_csbtnBanList, new UnityAction(this.OnClickBanList));
			NKCUtil.SetGameobjectActive(this.m_objDescGuild, NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_GUILD));
			NKCUtil.SetGameobjectActive(this.m_objDescSearch, NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_SEARCH));
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
			if (this.m_ctglTabRoom != null)
			{
				this.m_ctglTabRoom.OnValueChanged.RemoveAllListeners();
				this.m_ctglTabRoom.OnValueChanged.AddListener(new UnityAction<bool>(this.OnTabChangedToRoom));
			}
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM))
			{
				this.m_currentTabType = NKCUIGauntletLobbyCustom.PRIVATE_PVP_TAB_TYPE.ROOM;
				NKCUtil.SetGameobjectActive(this.m_ctglRankFriendTab, false);
				NKCUtil.SetGameobjectActive(this.m_ctglTabSearchID, false);
				NKCUtil.SetGameobjectActive(this.m_ctglTabGuild, false);
				NKCUtil.SetGameobjectActive(this.m_ctglTabRoom, true);
			}
			if (this.m_cbtnNKM_UI_ROOM_CREATE_BUTTON != null)
			{
				this.m_cbtnNKM_UI_ROOM_CREATE_BUTTON.PointerClick.RemoveAllListeners();
				this.m_cbtnNKM_UI_ROOM_CREATE_BUTTON.PointerClick.AddListener(new UnityAction(this.OnClickCreateRoom));
			}
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
			if (cbtnNKM_UI_FRIEND_TOP_SUBMENU_REFRESH_Disable != null)
			{
				cbtnNKM_UI_FRIEND_TOP_SUBMENU_REFRESH_Disable.Lock();
			}
			NKCUIGauntletCustomOption customOption = this.m_CustomOption;
			if (customOption == null)
			{
				return;
			}
			customOption.Init();
		}

		// Token: 0x060086C6 RID: 34502 RVA: 0x002D9E80 File Offset: 0x002D8080
		private void RefreshTabUI()
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			Debug.Log("[PrivatePVP] RefreshTabUI [" + this.m_currentTabType.ToString() + "]");
			NKCUtil.SetGameobjectActive(this.m_objRankFriend, this.m_currentTabType == NKCUIGauntletLobbyCustom.PRIVATE_PVP_TAB_TYPE.FRIEND);
			NKCUtil.SetGameobjectActive(this.m_objSearchID, this.m_currentTabType == NKCUIGauntletLobbyCustom.PRIVATE_PVP_TAB_TYPE.SEARCH);
			NKCUtil.SetGameobjectActive(this.m_objGuild, this.m_currentTabType == NKCUIGauntletLobbyCustom.PRIVATE_PVP_TAB_TYPE.GUILD);
			NKCUtil.SetGameobjectActive(this.m_objRoom, this.m_currentTabType == NKCUIGauntletLobbyCustom.PRIVATE_PVP_TAB_TYPE.ROOM);
			this.RefreshTabCells();
			if (!this.m_bPlayIntro)
			{
				this.m_amtorRankCenter.Play("NKM_UI_GAUNTLET_LOBBY_CONTENT_INTRO_CENTER_FADEIN");
				return;
			}
			this.m_bPlayIntro = false;
		}

		// Token: 0x060086C7 RID: 34503 RVA: 0x002D9F36 File Offset: 0x002D8136
		private void OnTabChangedToFriend(bool bSet)
		{
			if (bSet)
			{
				this.m_currentTabType = NKCUIGauntletLobbyCustom.PRIVATE_PVP_TAB_TYPE.FRIEND;
				this.UpdateFriendList();
			}
			this.RefreshTabUI();
		}

		// Token: 0x060086C8 RID: 34504 RVA: 0x002D9F4E File Offset: 0x002D814E
		private void OnTabChangedToSearchID(bool bSet)
		{
			if (bSet)
			{
				this.m_currentTabType = NKCUIGauntletLobbyCustom.PRIVATE_PVP_TAB_TYPE.SEARCH;
				this.UpdateSearchList();
			}
			this.RefreshTabUI();
			if (NKCPrivatePVPMgr.SearchResult.Count == 0)
			{
				this.OnClickSearch();
			}
		}

		// Token: 0x060086C9 RID: 34505 RVA: 0x002D9F78 File Offset: 0x002D8178
		private void OnTabChangedToGuild(bool bSet)
		{
			Debug.Log(string.Format("[PrivatePVP] OnTabChangedToGuild [{0}]", bSet));
			if (bSet)
			{
				this.m_currentTabType = NKCUIGauntletLobbyCustom.PRIVATE_PVP_TAB_TYPE.GUILD;
				this.UpdateGuildList();
			}
			this.RefreshTabUI();
		}

		// Token: 0x060086CA RID: 34506 RVA: 0x002D9FA5 File Offset: 0x002D81A5
		private void OnTabChangedToRoom(bool bSet)
		{
			if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM))
			{
				return;
			}
			Debug.Log(string.Format("[PrivatePVP] OnTabChangedToRoom [{0}]", bSet));
			if (bSet)
			{
				this.m_currentTabType = NKCUIGauntletLobbyCustom.PRIVATE_PVP_TAB_TYPE.ROOM;
			}
			this.RefreshTabUI();
		}

		// Token: 0x060086CB RID: 34507 RVA: 0x002D9FD6 File Offset: 0x002D81D6
		private void OnClickBanList()
		{
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_CASTING_BAN))
			{
				NKCPopupGauntletBanListV2.Instance.Open();
				return;
			}
			NKCPopupGauntletBanList.Instance.Open();
		}

		// Token: 0x060086CC RID: 34508 RVA: 0x002D9FF5 File Offset: 0x002D81F5
		private void OnClickEmoticonSetting()
		{
			if (NKCEmoticonManager.m_bReceivedEmoticonData)
			{
				NKCPopupEmoticonSetting.Instance.Open();
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_LOBBY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetWaitForEmoticon(true);
				NKCPacketSender.Send_NKMPacket_EMOTICON_DATA_REQ(NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL);
			}
		}

		// Token: 0x060086CD RID: 34509 RVA: 0x002DA02D File Offset: 0x002D822D
		private void UpdateReadyButtonUI()
		{
		}

		// Token: 0x060086CE RID: 34510 RVA: 0x002DA030 File Offset: 0x002D8230
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

		// Token: 0x060086CF RID: 34511 RVA: 0x002DA0FF File Offset: 0x002D82FF
		private void OnEventPanelBeginDragFriend()
		{
		}

		// Token: 0x060086D0 RID: 34512 RVA: 0x002DA104 File Offset: 0x002D8304
		public void ReturnSlot(Transform tr)
		{
			NKCUIGauntletFriendSlot component = tr.GetComponent<NKCUIGauntletFriendSlot>();
			tr.SetParent(base.transform);
			if (component != null)
			{
				component.DestoryInstance();
				return;
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x060086D1 RID: 34513 RVA: 0x002DA13F File Offset: 0x002D833F
		public RectTransform GetSlotFriend(int index)
		{
			NKCUIGauntletFriendSlot newInstance = NKCUIGauntletFriendSlot.GetNewInstance(this.m_trRankFriend, new NKCUIGauntletFriendSlot.OnDragBegin(this.OnEventPanelBeginDragFriend));
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x060086D2 RID: 34514 RVA: 0x002DA164 File Offset: 0x002D8364
		public void ProvideSlotData(Transform tr, int index)
		{
			NKCUIGauntletFriendSlot component = tr.GetComponent<NKCUIGauntletFriendSlot>();
			if (component != null)
			{
				component.SetUI(this.GetUserSimpleData(index), true);
			}
		}

		// Token: 0x060086D3 RID: 34515 RVA: 0x002DA190 File Offset: 0x002D8390
		public void ProvideSlotDataGuild(Transform tr, int index)
		{
			NKCUIGauntletFriendSlot component = tr.GetComponent<NKCUIGauntletFriendSlot>();
			if (component != null)
			{
				component.SetUI(this.GetUserSimpleDataGuild(index), true);
			}
		}

		// Token: 0x060086D4 RID: 34516 RVA: 0x002DA1BB File Offset: 0x002D83BB
		public RectTransform GetSlotGuild(int index)
		{
			NKCUIGauntletFriendSlot newInstance = NKCUIGauntletFriendSlot.GetNewInstance(this.m_scrollContentGuild, new NKCUIGauntletFriendSlot.OnDragBegin(this.OnEventPanelBeginDragFriend));
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x060086D5 RID: 34517 RVA: 0x002DA1DF File Offset: 0x002D83DF
		public RectTransform GetSlotSearch(int index)
		{
			NKCUIGauntletFriendSlot newInstance = NKCUIGauntletFriendSlot.GetNewInstance(this.m_scrollContentSearchID, new NKCUIGauntletFriendSlot.OnDragBegin(this.OnEventPanelBeginDragFriend));
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x060086D6 RID: 34518 RVA: 0x002DA204 File Offset: 0x002D8404
		private void RefreshTabCells()
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			switch (this.m_currentTabType)
			{
			case NKCUIGauntletLobbyCustom.PRIVATE_PVP_TAB_TYPE.FRIEND:
				this.RefreshScrollRectFriend();
				return;
			case NKCUIGauntletLobbyCustom.PRIVATE_PVP_TAB_TYPE.GUILD:
				this.RefreshScrollRectGuild();
				return;
			case NKCUIGauntletLobbyCustom.PRIVATE_PVP_TAB_TYPE.SEARCH:
				this.RefreshScrollRectSearch();
				return;
			case NKCUIGauntletLobbyCustom.PRIVATE_PVP_TAB_TYPE.ROOM:
				this.RefreshScrollRectRoom();
				return;
			default:
				return;
			}
		}

		// Token: 0x060086D7 RID: 34519 RVA: 0x002DA258 File Offset: 0x002D8458
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

		// Token: 0x060086D8 RID: 34520 RVA: 0x002DA2E0 File Offset: 0x002D84E0
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

		// Token: 0x060086D9 RID: 34521 RVA: 0x002DA33C File Offset: 0x002D853C
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

		// Token: 0x060086DA RID: 34522 RVA: 0x002DA3D4 File Offset: 0x002D85D4
		private void RefreshScrollRectRoom()
		{
		}

		// Token: 0x060086DB RID: 34523 RVA: 0x002DA3D8 File Offset: 0x002D85D8
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
			case NKCUIGauntletLobbyCustom.PRIVATE_PVP_TAB_TYPE.FRIEND:
				this.m_ctglRankFriendTab.Select(false, true, false);
				this.m_ctglRankFriendTab.Select(true, false, false);
				NKCUtil.SetGameobjectActive(this.m_objRankFriend, true);
				this.UpdateFriendList();
				break;
			case NKCUIGauntletLobbyCustom.PRIVATE_PVP_TAB_TYPE.GUILD:
				this.m_ctglTabGuild.Select(false, true, false);
				this.m_ctglTabGuild.Select(true, false, false);
				NKCUtil.SetGameobjectActive(this.m_objGuild, true);
				this.UpdateGuildList();
				break;
			case NKCUIGauntletLobbyCustom.PRIVATE_PVP_TAB_TYPE.SEARCH:
				this.m_ctglTabSearchID.Select(false, true, false);
				this.m_ctglTabSearchID.Select(true, false, false);
				NKCUtil.SetGameobjectActive(this.m_objSearchID, true);
				this.UpdateSearchList();
				break;
			case NKCUIGauntletLobbyCustom.PRIVATE_PVP_TAB_TYPE.ROOM:
				this.m_ctglTabRoom.Select(false, true, false);
				this.m_ctglTabRoom.Select(true, false, false);
				NKCUtil.SetGameobjectActive(this.m_objRoom, true);
				this.UpdateRoomList();
				break;
			}
			this.UpdateReadyButtonUI();
			this.RefreshTabCells();
			if (!this.m_bPlayIntro)
			{
				this.m_amtorRankCenter.Play("NKM_UI_GAUNTLET_LOBBY_CONTENT_INTRO_CENTER_FADEIN");
			}
			else
			{
				this.m_bPlayIntro = false;
			}
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM))
			{
				this.m_CustomOption.SetOption(NKCPrivatePVPRoomMgr.PrivateGameConfig);
				return;
			}
			this.m_CustomOption.SetOption(NKCPrivatePVPMgr.PrivateGameConfig);
		}

		// Token: 0x060086DC RID: 34524 RVA: 0x002DA5C8 File Offset: 0x002D87C8
		private void OnClickBattleRecord()
		{
			NKC_SCEN_GAUNTLET_LOBBY nkc_SCEN_GAUNTLET_LOBBY = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY();
			if (nkc_SCEN_GAUNTLET_LOBBY != null)
			{
				nkc_SCEN_GAUNTLET_LOBBY.OpenBattleRecord(NKM_GAME_TYPE.NGT_PVP_PRIVATE);
			}
		}

		// Token: 0x060086DD RID: 34525 RVA: 0x002DA5EB File Offset: 0x002D87EB
		public void ClearCacheData()
		{
			this.m_lvsrRankFriend.ClearCells();
			this.m_scrollSearchID.ClearCells();
			LoopVerticalScrollRect scrollGuild = this.m_scrollGuild;
			if (scrollGuild == null)
			{
				return;
			}
			scrollGuild.ClearCells();
		}

		// Token: 0x060086DE RID: 34526 RVA: 0x002DA613 File Offset: 0x002D8813
		public void Close()
		{
			this.m_bFirstOpen = true;
			NKCPopupGauntletBanList.CheckInstanceAndClose();
		}

		// Token: 0x060086DF RID: 34527 RVA: 0x002DA621 File Offset: 0x002D8821
		private FriendListData GetUserSimpleData(int index)
		{
			if (this.m_friendSlotDataList.Count > index)
			{
				return this.m_friendSlotDataList[index];
			}
			return null;
		}

		// Token: 0x060086E0 RID: 34528 RVA: 0x002DA640 File Offset: 0x002D8840
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

		// Token: 0x060086E1 RID: 34529 RVA: 0x002DA6D8 File Offset: 0x002D88D8
		public void UpdateFriendList()
		{
			this.m_friendSlotDataList.Clear();
			this.m_friendSlotDataList.AddRange(NKCFriendManager.FriendListData);
			this.m_friendSlotDataList.Sort((FriendListData a, FriendListData b) => b.lastLoginDate.CompareTo(a.lastLoginDate));
		}

		// Token: 0x060086E2 RID: 34530 RVA: 0x002DA72C File Offset: 0x002D892C
		public void UpdateSearchList()
		{
			this.m_friendSlotDataList.Clear();
			this.m_friendSlotDataList.AddRange(NKCPrivatePVPMgr.SearchResult);
			this.m_friendSlotDataList.Sort((FriendListData a, FriendListData b) => b.lastLoginDate.CompareTo(a.lastLoginDate));
		}

		// Token: 0x060086E3 RID: 34531 RVA: 0x002DA780 File Offset: 0x002D8980
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

		// Token: 0x060086E4 RID: 34532 RVA: 0x002DA801 File Offset: 0x002D8A01
		public void UpdateRoomList()
		{
		}

		// Token: 0x060086E5 RID: 34533 RVA: 0x002DA804 File Offset: 0x002D8A04
		public void OnClickSearch()
		{
			if (this.m_minimumSearchDelay > 0f)
			{
				return;
			}
			this.UpdateSearchList();
			NKCPrivatePVPMgr.Send_NKMPacket_PRIVATE_PVP_SEARCH_USER_REQ(this.m_IFNicknameOrUID.text);
			this.m_minimumSearchDelay = 9f;
			NKCUtil.SetGameobjectActive(this.m_cbtnNKM_UI_FRIEND_TOP_SEARCH_BUTTON, false);
			NKCUtil.SetGameobjectActive(this.m_cbtnNKM_UI_FRIEND_TOP_SEARCH_BUTTON_DISABLE, true);
		}

		// Token: 0x060086E6 RID: 34534 RVA: 0x002DA858 File Offset: 0x002D8A58
		public void OnClickRefresh()
		{
			if (this.m_minimumRefreshDelay > 0f)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_cbtnNKM_UI_FRIEND_TOP_SUBMENU_REFRESH, false);
			NKCUtil.SetGameobjectActive(this.m_cbtnNKM_UI_FRIEND_TOP_SUBMENU_REFRESH_Disable, true);
			NKCPrivatePVPMgr.Send_NKMPacket_PRIVATE_PVP_SEARCH_USER_REQ("");
			this.m_minimumRefreshDelay = 9f;
		}

		// Token: 0x060086E7 RID: 34535 RVA: 0x002DA895 File Offset: 0x002D8A95
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

		// Token: 0x060086E8 RID: 34536 RVA: 0x002DA8BC File Offset: 0x002D8ABC
		private void OnClickCreateRoom()
		{
			if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM))
			{
				return;
			}
			NKCPrivatePVPRoomMgr.Send_NKMPacket_PRIVATE_PVP_CREATE_ROOM_REQ();
		}

		// Token: 0x04007336 RID: 29494
		public Animator m_amtorRankCenter;

		// Token: 0x04007337 RID: 29495
		[Header("친구")]
		public NKCUIComToggle m_ctglRankFriendTab;

		// Token: 0x04007338 RID: 29496
		public GameObject m_objRankFriend;

		// Token: 0x04007339 RID: 29497
		public LoopVerticalScrollRect m_lvsrRankFriend;

		// Token: 0x0400733A RID: 29498
		public Transform m_trRankFriend;

		// Token: 0x0400733B RID: 29499
		public GameObject m_objEmptyList;

		// Token: 0x0400733C RID: 29500
		public Text m_lbEmptyMessage;

		// Token: 0x0400733D RID: 29501
		[Header("검색")]
		public NKCUIComToggle m_ctglTabSearchID;

		// Token: 0x0400733E RID: 29502
		public GameObject m_objSearchID;

		// Token: 0x0400733F RID: 29503
		public LoopVerticalScrollRect m_scrollSearchID;

		// Token: 0x04007340 RID: 29504
		public Transform m_scrollContentSearchID;

		// Token: 0x04007341 RID: 29505
		public NKCUIComButton m_cbtnNKM_UI_FRIEND_TOP_SEARCH_BUTTON;

		// Token: 0x04007342 RID: 29506
		public NKCUIComButton m_cbtnNKM_UI_FRIEND_TOP_SEARCH_BUTTON_DISABLE;

		// Token: 0x04007343 RID: 29507
		public NKCUIComButton m_cbtnNKM_UI_FRIEND_TOP_SUBMENU_REFRESH;

		// Token: 0x04007344 RID: 29508
		public NKCUIComButton m_cbtnNKM_UI_FRIEND_TOP_SUBMENU_REFRESH_Disable;

		// Token: 0x04007345 RID: 29509
		public InputField m_IFNicknameOrUID;

		// Token: 0x04007346 RID: 29510
		private float m_minimumSearchDelay;

		// Token: 0x04007347 RID: 29511
		private float m_minimumRefreshDelay;

		// Token: 0x04007348 RID: 29512
		private const float MIN_SEARCH_DELAY = 9f;

		// Token: 0x04007349 RID: 29513
		private const float MIN_REFRESH_DELAY = 9f;

		// Token: 0x0400734A RID: 29514
		[Header("연합원")]
		public NKCUIComToggle m_ctglTabGuild;

		// Token: 0x0400734B RID: 29515
		public GameObject m_objGuild;

		// Token: 0x0400734C RID: 29516
		public LoopVerticalScrollRect m_scrollGuild;

		// Token: 0x0400734D RID: 29517
		public Transform m_scrollContentGuild;

		// Token: 0x0400734E RID: 29518
		public GameObject m_objEmptyListGuild;

		// Token: 0x0400734F RID: 29519
		public Text m_lbEmptyMessageGuild;

		// Token: 0x04007350 RID: 29520
		[Header("방 생성")]
		public NKCUIComToggle m_ctglTabRoom;

		// Token: 0x04007351 RID: 29521
		public GameObject m_objRoom;

		// Token: 0x04007352 RID: 29522
		public NKCUIComButton m_cbtnNKM_UI_ROOM_CREATE_BUTTON;

		// Token: 0x04007353 RID: 29523
		[Header("정보")]
		public NKCUIComStateButton m_csbtnBattleHistory;

		// Token: 0x04007354 RID: 29524
		public NKCUIComStateButton m_csbtnRankMatchReady;

		// Token: 0x04007355 RID: 29525
		public NKCUIComStateButton m_csbtnRankMatchReadyDisable;

		// Token: 0x04007356 RID: 29526
		public NKCUIComStateButton m_csbtnEmoticonSetting;

		// Token: 0x04007357 RID: 29527
		public NKCUIComStateButton m_csbtnBanList;

		// Token: 0x04007358 RID: 29528
		public GameObject m_objMatchReady;

		// Token: 0x04007359 RID: 29529
		public GameObject m_objMatchReadyDisable;

		// Token: 0x0400735A RID: 29530
		[Header("우측정보")]
		public GameObject m_objDescGuild;

		// Token: 0x0400735B RID: 29531
		public GameObject m_objDescSearch;

		// Token: 0x0400735C RID: 29532
		public NKCUIGauntletCustomOption m_CustomOption;

		// Token: 0x0400735D RID: 29533
		private NKCUIGauntletLobbyCustom.PRIVATE_PVP_TAB_TYPE m_currentTabType;

		// Token: 0x0400735E RID: 29534
		private RANK_TYPE m_RANK_TYPE = RANK_TYPE.FRIEND;

		// Token: 0x0400735F RID: 29535
		private bool m_bFirstOpen = true;

		// Token: 0x04007360 RID: 29536
		private bool m_bPrepareLoopScrollCells;

		// Token: 0x04007361 RID: 29537
		private bool m_bFriendTabOpened;

		// Token: 0x04007362 RID: 29538
		private bool m_bSearchTabOpened;

		// Token: 0x04007363 RID: 29539
		private bool m_bGuildTabOpened;

		// Token: 0x04007364 RID: 29540
		private float m_fPrevUpdateTime;

		// Token: 0x04007365 RID: 29541
		private bool m_bPlayIntro = true;

		// Token: 0x04007366 RID: 29542
		private List<FriendListData> m_friendSlotDataList = new List<FriendListData>();

		// Token: 0x0200191D RID: 6429
		private enum PRIVATE_PVP_TAB_TYPE
		{
			// Token: 0x0400AAA5 RID: 43685
			FRIEND,
			// Token: 0x0400AAA6 RID: 43686
			GUILD,
			// Token: 0x0400AAA7 RID: 43687
			SEARCH,
			// Token: 0x0400AAA8 RID: 43688
			ROOM
		}
	}
}
