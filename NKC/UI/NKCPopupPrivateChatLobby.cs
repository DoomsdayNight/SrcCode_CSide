using System;
using System.Collections.Generic;
using ClientPacket.Chat;
using ClientPacket.Common;
using ClientPacket.Guild;
using NKC.UI.Friend;
using NKC.UI.Guild;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000921 RID: 2337
	public class NKCPopupPrivateChatLobby : NKCUIBase
	{
		// Token: 0x17001115 RID: 4373
		// (get) Token: 0x06005D85 RID: 23941 RVA: 0x001CD7BC File Offset: 0x001CB9BC
		public static NKCPopupPrivateChatLobby Instance
		{
			get
			{
				if (NKCPopupPrivateChatLobby.m_Instance == null)
				{
					NKCPopupPrivateChatLobby.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupPrivateChatLobby>("AB_UI_CHAT", "AB_UI_CHAT", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupPrivateChatLobby.CleanupInstance)).GetInstance<NKCPopupPrivateChatLobby>();
					NKCPopupPrivateChatLobby.m_Instance.InitUI();
				}
				return NKCPopupPrivateChatLobby.m_Instance;
			}
		}

		// Token: 0x06005D86 RID: 23942 RVA: 0x001CD80B File Offset: 0x001CBA0B
		private static void CleanupInstance()
		{
			NKCPopupPrivateChatLobby.m_Instance = null;
		}

		// Token: 0x17001116 RID: 4374
		// (get) Token: 0x06005D87 RID: 23943 RVA: 0x001CD813 File Offset: 0x001CBA13
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupPrivateChatLobby.m_Instance != null && NKCPopupPrivateChatLobby.m_Instance.IsOpen;
			}
		}

		// Token: 0x06005D88 RID: 23944 RVA: 0x001CD82E File Offset: 0x001CBA2E
		private void OnDestroy()
		{
			NKCPopupPrivateChatLobby.m_Instance = null;
		}

		// Token: 0x17001117 RID: 4375
		// (get) Token: 0x06005D89 RID: 23945 RVA: 0x001CD836 File Offset: 0x001CBA36
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001118 RID: 4376
		// (get) Token: 0x06005D8A RID: 23946 RVA: 0x001CD839 File Offset: 0x001CBA39
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x06005D8B RID: 23947 RVA: 0x001CD840 File Offset: 0x001CBA40
		public override void CloseInternal()
		{
			if (this.m_SoundID >= 0)
			{
				NKCSoundManager.StopSound(this.m_SoundID);
				this.m_SoundID = -1;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06005D8C RID: 23948 RVA: 0x001CD86C File Offset: 0x001CBA6C
		private void InitUI()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_objLobby, true);
			this.m_Chat.InitUI(new NKCUIComChat.OnSendMessage(this.OnSendMessage), new NKCUIComChat.OnClose(this.OnCloseChat), true);
			if (this.m_btnClose != null)
			{
				this.m_btnClose.PointerClick.RemoveAllListeners();
				this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
			}
			if (this.m_loop != null)
			{
				NKCUtil.SetGameobjectActive(this.m_loop, true);
				this.m_loop.dOnGetObject += this.GetObject;
				this.m_loop.dOnReturnObject += this.ReturnObject;
				this.m_loop.dOnProvideData += this.ProvideData;
				this.m_loop.PrepareCells(0);
			}
			if (this.m_tglFriend != null)
			{
				this.m_tglFriend.OnValueChanged.RemoveAllListeners();
				this.m_tglFriend.OnValueChanged.AddListener(new UnityAction<bool>(this.OnChangedFriend));
			}
			if (this.m_tglGuildMember != null)
			{
				this.m_tglGuildMember.OnValueChanged.RemoveAllListeners();
				this.m_tglGuildMember.OnValueChanged.AddListener(new UnityAction<bool>(this.OnChangedGuildMember));
				this.m_tglGuildMember.m_bGetCallbackWhileLocked = true;
			}
			if (this.m_btnGuildChat != null)
			{
				this.m_btnGuildChat.PointerClick.RemoveAllListeners();
				this.m_btnGuildChat.PointerClick.AddListener(new UnityAction(this.OnClickGuildChat));
				this.m_btnGuildChat.m_bGetCallbackWhileLocked = true;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06005D8D RID: 23949 RVA: 0x001CDA2C File Offset: 0x001CBC2C
		public override void OnBackButton()
		{
			if (this.m_Chat.gameObject.activeSelf)
			{
				this.m_Chat.Close();
				return;
			}
			base.Close();
		}

		// Token: 0x06005D8E RID: 23950 RVA: 0x001CDA52 File Offset: 0x001CBC52
		private void OnSendMessage(long channelUid, ChatMessageType messageType, string message, int emotionId)
		{
			NKCChatManager.SetCurrentChatRoomUid(channelUid);
			NKCPacketSender.Send_NKMPacket_PRIVATE_CHAT_REQ(channelUid, message, emotionId);
		}

		// Token: 0x06005D8F RID: 23951 RVA: 0x001CDA63 File Offset: 0x001CBC63
		private void OnCloseChat()
		{
			NKCUtil.SetGameobjectActive(this.m_objLobby, true);
			this.RefreshUI(false);
		}

		// Token: 0x06005D90 RID: 23952 RVA: 0x001CDA78 File Offset: 0x001CBC78
		private RectTransform GetObject(int idx)
		{
			NKCPopupPrivateChatLobbySlot nkcpopupPrivateChatLobbySlot;
			if (this.m_stkSlot.Count > 0)
			{
				nkcpopupPrivateChatLobbySlot = this.m_stkSlot.Pop();
			}
			else
			{
				nkcpopupPrivateChatLobbySlot = UnityEngine.Object.Instantiate<NKCPopupPrivateChatLobbySlot>(this.m_pfbSlot, this.m_trContent);
				nkcpopupPrivateChatLobbySlot.Init();
			}
			return nkcpopupPrivateChatLobbySlot.GetComponent<RectTransform>();
		}

		// Token: 0x06005D91 RID: 23953 RVA: 0x001CDAC4 File Offset: 0x001CBCC4
		private void ReturnObject(Transform tr)
		{
			NKCPopupPrivateChatLobbySlot component = tr.GetComponent<NKCPopupPrivateChatLobbySlot>();
			NKCUtil.SetGameobjectActive(component, false);
			this.m_stkSlot.Push(component);
		}

		// Token: 0x06005D92 RID: 23954 RVA: 0x001CDAEC File Offset: 0x001CBCEC
		private void ProvideData(Transform tr, int idx)
		{
			NKCPopupPrivateChatLobbySlot component = tr.GetComponent<NKCPopupPrivateChatLobbySlot>();
			NKCUtil.SetGameobjectActive(component, true);
			if (this.m_curTabType == NKCPopupPrivateChatLobby.CHAT_LOBBY_TAB_TYPE.FRIEND)
			{
				if (this.m_lstFriends.Count > idx)
				{
					component.SetData(this.m_curTabType, this.m_lstFriends[idx], new NKCPopupPrivateChatLobbySlot.OnClickChat(this.OnClickChatSlot), NKCChatManager.CheckPrivateChatNotify(NKCScenManager.CurrentUserData(), this.m_lstFriends[idx].commonProfile.userUid));
					return;
				}
				NKCUtil.SetGameobjectActive(component, false);
				return;
			}
			else
			{
				if (this.m_lstGuilds.Count > idx)
				{
					component.SetData(this.m_curTabType, this.m_lstGuilds[idx], new NKCPopupPrivateChatLobbySlot.OnClickChat(this.OnClickChatSlot), NKCChatManager.CheckPrivateChatNotify(NKCScenManager.CurrentUserData(), this.m_lstGuilds[idx].commonProfile.userUid));
					return;
				}
				NKCUtil.SetGameobjectActive(component, false);
				return;
			}
		}

		// Token: 0x06005D93 RID: 23955 RVA: 0x001CDBC4 File Offset: 0x001CBDC4
		public void Open(long reservedChatRoomUid = 0L)
		{
			this.m_SoundID = NKCSoundManager.PlaySound("FX_UI_CHAT_INTRO", 1f, 0f, 0f, false, 0f, false, 0f);
			NKCUtil.SetGameobjectActive(this.m_Chat, false);
			NKCUtil.SetGameobjectActive(this.m_objLobby, true);
			this.m_LastAlarmTime = DateTime.MinValue;
			if (NKCGuildManager.HasGuild())
			{
				this.m_GuildBadge.SetData(NKCGuildManager.MyGuildData.badgeId);
				NKCUtil.SetLabelText(this.m_lbGuildName, NKCGuildManager.MyGuildData.name);
				NKCUtil.SetGameobjectActive(this.m_objRedDotGuildChat, NKCChatManager.CheckGuildChatNotify());
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_curTabType = NKCPopupPrivateChatLobby.CHAT_LOBBY_TAB_TYPE.FRIEND;
			this.m_tglFriend.Select(true, true, true);
			this.m_tglGuildMember.Select(false, true, true);
			if (NKCGuildManager.HasGuild())
			{
				this.m_btnGuildChat.UnLock(false);
				this.m_tglGuildMember.UnLock(false);
			}
			else
			{
				this.m_btnGuildChat.Lock(false);
				this.m_tglGuildMember.Lock(false);
			}
			this.OnChangedFriend(true);
			if (reservedChatRoomUid > 0L)
			{
				this.ShowPrivateChat(reservedChatRoomUid);
			}
			base.UIOpened(true);
			if (!NKCChatManager.bAllListRequested)
			{
				NKCChatManager.bAllListRequested = true;
				NKCPacketSender.Send_NKMPacket_PRIVATE_CHAT_ALL_LIST_REQ();
			}
		}

		// Token: 0x06005D94 RID: 23956 RVA: 0x001CDCF6 File Offset: 0x001CBEF6
		public void ShowPrivateChat(long userUid)
		{
			NKCUtil.SetGameobjectActive(this.m_objLobby, false);
			this.m_Chat.SetData(userUid, false, this.FindNicknameByUserUid(userUid) ?? "");
		}

		// Token: 0x06005D95 RID: 23957 RVA: 0x001CDD24 File Offset: 0x001CBF24
		private string FindNicknameByUserUid(long userUid)
		{
			string result = "";
			if (NKCGuildManager.IsGuildMemberByUID(userUid))
			{
				NKMGuildMemberData nkmguildMemberData = NKCGuildManager.MyGuildData.members.Find((NKMGuildMemberData x) => x.commonProfile.userUid == userUid);
				if (nkmguildMemberData != null)
				{
					return nkmguildMemberData.commonProfile.nickname;
				}
			}
			else
			{
				FriendListData friendListData = NKCFriendManager.FriendListData.Find((FriendListData x) => x.commonProfile.userUid == userUid);
				if (friendListData != null)
				{
					return friendListData.commonProfile.nickname;
				}
			}
			return result;
		}

		// Token: 0x06005D96 RID: 23958 RVA: 0x001CDDA3 File Offset: 0x001CBFA3
		private void OnClickChatSlot(NKMCommonProfile commonProfile)
		{
			NKCPacketSender.Send_NKMPacket_PRIVATE_CHAT_LIST_REQ(commonProfile.userUid);
		}

		// Token: 0x06005D97 RID: 23959 RVA: 0x001CDDB0 File Offset: 0x001CBFB0
		private void OnChangedFriend(bool bValue)
		{
			if (bValue)
			{
				this.m_curTabType = NKCPopupPrivateChatLobby.CHAT_LOBBY_TAB_TYPE.FRIEND;
				this.RefreshUI(true);
			}
		}

		// Token: 0x06005D98 RID: 23960 RVA: 0x001CDDC3 File Offset: 0x001CBFC3
		private void OnChangedGuildMember(bool bValue)
		{
			if (this.m_tglGuildMember.m_bLock)
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_CHAT_CONSORTIUM_JOIN_REQ_DESC, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return;
			}
			if (bValue)
			{
				this.m_curTabType = NKCPopupPrivateChatLobby.CHAT_LOBBY_TAB_TYPE.GUILD_MEMBER;
				this.RefreshUI(true);
			}
		}

		// Token: 0x06005D99 RID: 23961 RVA: 0x001CDE04 File Offset: 0x001CC004
		private int SortByMessageTime(PrivateChatListData lData, PrivateChatListData rData)
		{
			if (rData == null || rData.lastMessage == null)
			{
				return -1;
			}
			if (lData == null || lData.lastMessage == null)
			{
				return 1;
			}
			if (rData.lastMessage.createdAt == lData.lastMessage.createdAt)
			{
				return rData.commonProfile.nickname.CompareTo(lData.commonProfile.nickname);
			}
			return rData.lastMessage.createdAt.CompareTo(lData.lastMessage.createdAt);
		}

		// Token: 0x06005D9A RID: 23962 RVA: 0x001CDE80 File Offset: 0x001CC080
		public void RefreshUI(bool bResetScroll = false)
		{
			if (this.m_curTabType == NKCPopupPrivateChatLobby.CHAT_LOBBY_TAB_TYPE.FRIEND)
			{
				this.m_lstFriends = NKCChatManager.FriendChatList;
				for (int i = this.m_lstFriends.Count - 1; i >= 0; i--)
				{
					if (NKCFriendManager.IsBlockedUser(this.m_lstFriends[i].commonProfile.friendCode))
					{
						this.m_lstFriends.RemoveAt(i);
					}
				}
				this.m_lstFriends.Sort(new Comparison<PrivateChatListData>(this.SortByMessageTime));
				this.m_loop.TotalCount = this.m_lstFriends.Count;
				NKCUtil.SetLabelText(this.m_lbCountDesc, NKCUtilString.GET_STRING_CHAT_FRIEND_COUNT_TEXT);
				NKCUtil.SetLabelText(this.m_lbCount, string.Format("{0}/{1}", NKCFriendManager.FriendListData.Count, 60));
			}
			else if (this.m_curTabType == NKCPopupPrivateChatLobby.CHAT_LOBBY_TAB_TYPE.GUILD_MEMBER)
			{
				this.m_lstGuilds = NKCChatManager.GuildChatList;
				for (int j = this.m_lstGuilds.Count - 1; j >= 0; j--)
				{
					if (NKCFriendManager.IsBlockedUser(this.m_lstGuilds[j].commonProfile.friendCode))
					{
						this.m_lstGuilds.RemoveAt(j);
					}
				}
				this.m_lstGuilds.Sort(new Comparison<PrivateChatListData>(this.SortByMessageTime));
				this.m_loop.TotalCount = this.m_lstGuilds.Count;
				NKCUtil.SetLabelText(this.m_lbCountDesc, NKCUtilString.GET_STRING_CHAT_CONSORTIUM_MEMBER_COUNT_TEXT);
				NKCUtil.SetLabelText(this.m_lbCount, string.Format("{0}/{1}", NKCGuildManager.MyGuildData.members.Count, NKCGuildManager.GetMaxGuildMemberCount(NKCGuildManager.MyGuildData.guildLevel)));
			}
			NKCUtil.SetGameobjectActive(this.m_loop, this.m_loop.TotalCount > 0);
			NKCUtil.SetGameobjectActive(this.m_objNone, this.m_loop.TotalCount == 0);
			if (bResetScroll && this.m_loop.TotalCount > 0)
			{
				this.m_loop.SetIndexPosition(0);
			}
			else
			{
				this.m_loop.RefreshCells(false);
			}
			NKCUtil.SetGameobjectActive(this.m_objFriendRedDot, NKCChatManager.CheckPrivateChatNotifyByTabType(NKCPopupPrivateChatLobby.CHAT_LOBBY_TAB_TYPE.FRIEND, 0L));
			NKCUtil.SetGameobjectActive(this.m_objGuildMemberRedDot, NKCChatManager.CheckPrivateChatNotifyByTabType(NKCPopupPrivateChatLobby.CHAT_LOBBY_TAB_TYPE.GUILD_MEMBER, 0L));
			if (NKCGuildManager.HasGuild())
			{
				this.m_btnGuildChat.UnLock(false);
				this.m_tglGuildMember.UnLock(false);
				NKCUtil.SetGameobjectActive(this.m_objRedDotGuildChat, NKCChatManager.CheckGuildChatNotify());
			}
			else
			{
				this.m_btnGuildChat.Lock(false);
				this.m_tglGuildMember.Lock(false);
				NKCUtil.SetGameobjectActive(this.m_objRedDotGuildChat, false);
			}
			foreach (NKCPopupPrivateChatLobbySlot nkcpopupPrivateChatLobbySlot in this.m_stkSlot)
			{
				nkcpopupPrivateChatLobbySlot.OnRefreshUI();
			}
		}

		// Token: 0x06005D9B RID: 23963 RVA: 0x001CE140 File Offset: 0x001CC340
		private void OnClickGuildChat()
		{
			if (this.m_btnGuildChat.m_bLock)
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_CHAT_CONSORTIUM_JOIN_REQ_DESC, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objRedDotGuildChat, false);
			NKCPopupGuildChat.Instance.Open(NKCGuildManager.MyGuildData.guildUid);
		}

		// Token: 0x06005D9C RID: 23964 RVA: 0x001CE198 File Offset: 0x001CC398
		public void AddMessage(NKMChatMessageData data, bool bIsMyMessage)
		{
			this.CheckAlarmSound(data.commonProfile.userUid, bIsMyMessage);
			if (this.m_Chat.gameObject.activeSelf)
			{
				this.m_Chat.AddMessage(data, bIsMyMessage, true);
				return;
			}
			if (NKCFriendManager.IsFriend(data.commonProfile.friendCode))
			{
				NKCUtil.SetGameobjectActive(this.m_objFriendRedDot, true);
			}
			if (NKCGuildManager.IsGuildMember(data.commonProfile.friendCode))
			{
				NKCUtil.SetGameobjectActive(this.m_objGuildMemberRedDot, true);
			}
			this.RefreshUI(false);
		}

		// Token: 0x06005D9D RID: 23965 RVA: 0x001CE21C File Offset: 0x001CC41C
		private bool CheckAlarmSound(long userUid, bool bIsMyMessage)
		{
			if (!NKCScenManager.GetScenManager().GetGameOptionData().UseChatNotifySound)
			{
				return false;
			}
			if (bIsMyMessage)
			{
				if ((NKCSynchronizedTime.ServiceTime - this.m_MyMessageAlarmTime).TotalSeconds >= (double)this.m_AlarmCooltimeSecond)
				{
					this.m_MyMessageAlarmTime = NKCSynchronizedTime.ServiceTime;
					NKCSoundManager.PlaySound("FX_UI_CHAT_SEND", 1f, 0f, 0f, false, 0f, false, 0f);
					return true;
				}
			}
			else if ((NKCSynchronizedTime.ServiceTime - this.m_LastAlarmTime).TotalSeconds >= (double)this.m_AlarmCooltimeSecond)
			{
				if (!this.m_Chat.gameObject.activeInHierarchy)
				{
					this.m_LastAlarmTime = NKCSynchronizedTime.ServiceTime;
					NKCSoundManager.PlaySound("FX_UI_CHAT_MAIN_ALARM", 1f, 0f, 0f, false, 0f, false, 0f);
					return true;
				}
				if (this.m_Chat.GetChannelUid() == userUid)
				{
					this.m_LastAlarmTime = NKCSynchronizedTime.ServiceTime;
					NKCSoundManager.PlaySound("FX_UI_CHAT_RECEIVE", 1f, 0f, 0f, false, 0f, false, 0f);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005D9E RID: 23966 RVA: 0x001CE341 File Offset: 0x001CC541
		public override void OnGuildDataChanged()
		{
			if (this.m_Chat.gameObject.activeInHierarchy)
			{
				this.m_Chat.OnGuildDataChanged();
				return;
			}
			this.RefreshUI(false);
		}

		// Token: 0x06005D9F RID: 23967 RVA: 0x001CE368 File Offset: 0x001CC568
		public NKCUIFriendSlot.FRIEND_SLOT_TYPE GetFriendSlotType()
		{
			NKCPopupPrivateChatLobby.CHAT_LOBBY_TAB_TYPE curTabType = this.m_curTabType;
			if (curTabType == NKCPopupPrivateChatLobby.CHAT_LOBBY_TAB_TYPE.FRIEND)
			{
				return NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_FRIEND_LIST;
			}
			if (curTabType != NKCPopupPrivateChatLobby.CHAT_LOBBY_TAB_TYPE.GUILD_MEMBER)
			{
				return NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_NONE;
			}
			return NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_GUILD_LIST;
		}

		// Token: 0x06005DA0 RID: 23968 RVA: 0x001CE38C File Offset: 0x001CC58C
		public void OnRecvFriendBlock(long friendCode)
		{
			if (this.m_Chat.gameObject.activeSelf)
			{
				PrivateChatListData privateChatListData = this.m_lstFriends.Find((PrivateChatListData x) => x.commonProfile.friendCode == friendCode);
				if (privateChatListData != null && this.m_Chat.GetChannelUid() == privateChatListData.commonProfile.userUid)
				{
					this.m_Chat.Close();
					return;
				}
				PrivateChatListData privateChatListData2 = this.m_lstGuilds.Find((PrivateChatListData x) => x.commonProfile.friendCode == friendCode);
				if (privateChatListData2 != null && this.m_Chat.GetChannelUid() == privateChatListData2.commonProfile.userUid)
				{
					this.m_Chat.Close();
					return;
				}
			}
			this.RefreshUI(false);
		}

		// Token: 0x06005DA1 RID: 23969 RVA: 0x001CE43D File Offset: 0x001CC63D
		public override void OnScreenResolutionChanged()
		{
			base.OnScreenResolutionChanged();
			this.m_Chat.OnScreenResolutionChanged();
		}

		// Token: 0x040049B5 RID: 18869
		private const string ASSET_BUNDLE_NAME = "AB_UI_CHAT";

		// Token: 0x040049B6 RID: 18870
		private const string UI_ASSET_NAME = "AB_UI_CHAT";

		// Token: 0x040049B7 RID: 18871
		private static NKCPopupPrivateChatLobby m_Instance;

		// Token: 0x040049B8 RID: 18872
		public GameObject m_objLobby;

		// Token: 0x040049B9 RID: 18873
		public NKCUIComChat m_Chat;

		// Token: 0x040049BA RID: 18874
		public NKCPopupPrivateChatLobbySlot m_pfbSlot;

		// Token: 0x040049BB RID: 18875
		public NKCUIComStateButton m_btnClose;

		// Token: 0x040049BC RID: 18876
		public NKCUIComToggle m_tglFriend;

		// Token: 0x040049BD RID: 18877
		public GameObject m_objFriendRedDot;

		// Token: 0x040049BE RID: 18878
		public NKCUIComToggle m_tglGuildMember;

		// Token: 0x040049BF RID: 18879
		public GameObject m_objGuildMemberRedDot;

		// Token: 0x040049C0 RID: 18880
		public LoopScrollRect m_loop;

		// Token: 0x040049C1 RID: 18881
		public Transform m_trContent;

		// Token: 0x040049C2 RID: 18882
		public Text m_lbCountDesc;

		// Token: 0x040049C3 RID: 18883
		public Text m_lbCount;

		// Token: 0x040049C4 RID: 18884
		public GameObject m_objGuildChatShortcut;

		// Token: 0x040049C5 RID: 18885
		public NKCUIComStateButton m_btnGuildChat;

		// Token: 0x040049C6 RID: 18886
		public NKCUIGuildBadge m_GuildBadge;

		// Token: 0x040049C7 RID: 18887
		public Text m_lbGuildName;

		// Token: 0x040049C8 RID: 18888
		public GameObject m_objRedDotGuildChat;

		// Token: 0x040049C9 RID: 18889
		public GameObject m_objNone;

		// Token: 0x040049CA RID: 18890
		public float m_AlarmCooltimeSecond = 30f;

		// Token: 0x040049CB RID: 18891
		private Stack<NKCPopupPrivateChatLobbySlot> m_stkSlot = new Stack<NKCPopupPrivateChatLobbySlot>();

		// Token: 0x040049CC RID: 18892
		private NKCPopupPrivateChatLobby.CHAT_LOBBY_TAB_TYPE m_curTabType;

		// Token: 0x040049CD RID: 18893
		private List<PrivateChatListData> m_lstFriends = new List<PrivateChatListData>();

		// Token: 0x040049CE RID: 18894
		private List<PrivateChatListData> m_lstGuilds = new List<PrivateChatListData>();

		// Token: 0x040049CF RID: 18895
		private DateTime m_LastAlarmTime = DateTime.MinValue;

		// Token: 0x040049D0 RID: 18896
		private DateTime m_MyMessageAlarmTime = DateTime.MinValue;

		// Token: 0x040049D1 RID: 18897
		private int m_SoundID = -1;

		// Token: 0x020015AD RID: 5549
		public enum CHAT_LOBBY_TAB_TYPE
		{
			// Token: 0x0400A24F RID: 41551
			FRIEND,
			// Token: 0x0400A250 RID: 41552
			GUILD_MEMBER
		}
	}
}
