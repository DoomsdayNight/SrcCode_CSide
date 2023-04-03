using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Community;
using ClientPacket.Guild;
using NKC.UI.Gauntlet;
using NKC.UI.Guild;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Friend
{
	// Token: 0x02000B18 RID: 2840
	public class NKCPopupFriendInfo : NKCUIBase
	{
		// Token: 0x17001515 RID: 5397
		// (get) Token: 0x0600813B RID: 33083 RVA: 0x002B8934 File Offset: 0x002B6B34
		public static NKCPopupFriendInfo Instance
		{
			get
			{
				if (NKCPopupFriendInfo.m_Instance == null)
				{
					NKCPopupFriendInfo.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupFriendInfo>("AB_UI_NKM_UI_FRIEND", "NKM_UI_FRIEND_INFO_POPUP", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupFriendInfo.CleanupInstance)).GetInstance<NKCPopupFriendInfo>();
					NKCPopupFriendInfo.m_Instance.InitUI();
				}
				return NKCPopupFriendInfo.m_Instance;
			}
		}

		// Token: 0x17001516 RID: 5398
		// (get) Token: 0x0600813C RID: 33084 RVA: 0x002B8983 File Offset: 0x002B6B83
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupFriendInfo.m_Instance != null && NKCPopupFriendInfo.m_Instance.IsOpen;
			}
		}

		// Token: 0x0600813D RID: 33085 RVA: 0x002B899E File Offset: 0x002B6B9E
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupFriendInfo.m_Instance != null && NKCPopupFriendInfo.m_Instance.IsOpen)
			{
				NKCPopupFriendInfo.m_Instance.Close();
			}
		}

		// Token: 0x0600813E RID: 33086 RVA: 0x002B89C3 File Offset: 0x002B6BC3
		private static void CleanupInstance()
		{
			NKCPopupFriendInfo.m_Instance = null;
		}

		// Token: 0x0600813F RID: 33087 RVA: 0x002B89CB File Offset: 0x002B6BCB
		public static bool IsHasInstance()
		{
			return NKCPopupFriendInfo.m_Instance != null;
		}

		// Token: 0x17001517 RID: 5399
		// (get) Token: 0x06008140 RID: 33088 RVA: 0x002B89D8 File Offset: 0x002B6BD8
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001518 RID: 5400
		// (get) Token: 0x06008141 RID: 33089 RVA: 0x002B89DB File Offset: 0x002B6BDB
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_FRIEND_INFO;
			}
		}

		// Token: 0x06008142 RID: 33090 RVA: 0x002B89E4 File Offset: 0x002B6BE4
		public void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			for (int i = 0; i < this.m_lstNKCDeckViewUnitSlot.Count; i++)
			{
				NKCDeckViewUnitSlot nkcdeckViewUnitSlot = this.m_lstNKCDeckViewUnitSlot[i];
				if (nkcdeckViewUnitSlot != null)
				{
					nkcdeckViewUnitSlot.Init(i, true);
				}
			}
			this.m_cbtnChat.PointerClick.RemoveAllListeners();
			this.m_cbtnChat.PointerClick.AddListener(new UnityAction(this.OnClickChat));
			this.m_cbtnChat.m_bGetCallbackWhileLocked = true;
			this.m_cbtnInvite.PointerClick.RemoveAllListeners();
			this.m_cbtnInvite.PointerClick.AddListener(new UnityAction(this.OnClickInvite));
			this.m_cbtnBlock.PointerClick.RemoveAllListeners();
			this.m_cbtnBlock.PointerClick.AddListener(new UnityAction(this.OnClickBlock));
			this.m_cbtnBlockCancel.PointerClick.RemoveAllListeners();
			this.m_cbtnBlockCancel.PointerClick.AddListener(new UnityAction(this.OnClickCancelBlockFriend));
			this.m_csbtnDormitory.PointerClick.RemoveAllListeners();
			this.m_csbtnDormitory.PointerClick.AddListener(new UnityAction(this.OnClickDormitory));
			this.m_tgMain.OnValueChanged.RemoveAllListeners();
			this.m_tgMain.OnValueChanged.AddListener(new UnityAction<bool>(this.OnToggleChangedMain));
			this.m_tgRank.OnValueChanged.RemoveAllListeners();
			this.m_tgRank.OnValueChanged.AddListener(new UnityAction<bool>(this.OnToggleChangedRank));
			this.m_tgAsync.OnValueChanged.RemoveAllListeners();
			this.m_tgAsync.OnValueChanged.AddListener(new UnityAction<bool>(this.OnToggleChangedAsync));
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_FRIENDLY_MODE) && this.m_csbtnInviteToCustomMatch != null)
			{
				this.m_csbtnInviteToCustomMatch.PointerClick.RemoveAllListeners();
				this.m_csbtnInviteToCustomMatch.PointerClick.AddListener(new UnityAction(this.OnClickInviteCustomMatch));
			}
			for (int j = 0; j < this.m_lstEmblem.Count; j++)
			{
				this.m_lstEmblem[j].Init();
			}
			NKCPopupGuildUserInfo guildUserInfo = this.m_GuildUserInfo;
			if (guildUserInfo == null)
			{
				return;
			}
			guildUserInfo.InitUI();
		}

		// Token: 0x06008143 RID: 33091 RVA: 0x002B8C27 File Offset: 0x002B6E27
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06008144 RID: 33092 RVA: 0x002B8C38 File Offset: 0x002B6E38
		public void Open(NKMUserProfileData cNKMUserProfileData, bool bRegisterUI = true)
		{
			NKCUIFriendSlot.FRIEND_SLOT_TYPE friend_SLOT_TYPE = NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_NONE;
			NKM_SCEN_ID nowScenID = NKCScenManager.GetScenManager().GetNowScenID();
			if (nowScenID <= NKM_SCEN_ID.NSI_FRIEND)
			{
				if (nowScenID != NKM_SCEN_ID.NSI_HOME)
				{
					if (nowScenID != NKM_SCEN_ID.NSI_WARFARE_GAME)
					{
						if (nowScenID == NKM_SCEN_ID.NSI_FRIEND)
						{
							friend_SLOT_TYPE = NKCScenManager.GetScenManager().Get_NKC_SCEN_FRIEND().GetCurrentSlotType();
							this.m_deckTab = NKCPopupFriendInfo.DeckTab.Main;
						}
					}
					else
					{
						friend_SLOT_TYPE = NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_FRIEND_SEARCH;
						this.m_deckTab = NKCPopupFriendInfo.DeckTab.Main;
					}
				}
				else if (NKCPopupPrivateChatLobby.IsInstanceOpen)
				{
					friend_SLOT_TYPE = NKCPopupPrivateChatLobby.Instance.GetFriendSlotType();
				}
			}
			else if (nowScenID <= NKM_SCEN_ID.NSI_GAUNTLET_MATCH_READY)
			{
				if (nowScenID != NKM_SCEN_ID.NSI_GAUNTLET_LOBBY)
				{
					if (nowScenID == NKM_SCEN_ID.NSI_GAUNTLET_MATCH_READY)
					{
						friend_SLOT_TYPE = NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_GAUNTLET_LIST;
						this.m_deckTab = NKCPopupFriendInfo.DeckTab.Rank;
					}
				}
				else
				{
					switch (NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().GetCurrentLobbyTab())
					{
					case NKC_GAUNTLET_LOBBY_TAB.NGLT_RANK:
						friend_SLOT_TYPE = NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_GAUNTLET_LIST;
						this.m_deckTab = NKCPopupFriendInfo.DeckTab.Rank;
						break;
					case NKC_GAUNTLET_LOBBY_TAB.NGLT_ASYNC:
						friend_SLOT_TYPE = NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_GAUNTLET_LIST;
						this.m_deckTab = NKCPopupFriendInfo.DeckTab.Async;
						break;
					case NKC_GAUNTLET_LOBBY_TAB.NGLT_PRIVATE:
						friend_SLOT_TYPE = NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_GAUNTLET_CUSTOM_LIST;
						this.m_deckTab = NKCPopupFriendInfo.DeckTab.Main;
						break;
					}
				}
			}
			else if (nowScenID != NKM_SCEN_ID.NSI_GUILD_LOBBY)
			{
				if (nowScenID == NKM_SCEN_ID.NSI_OFFICE)
				{
					friend_SLOT_TYPE = NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_OFFICE;
				}
			}
			else
			{
				friend_SLOT_TYPE = NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_GUILD_LIST;
				this.m_deckTab = NKCPopupFriendInfo.DeckTab.Main;
			}
			NKCUtil.SetGameobjectActive(this.m_GuildUserInfo, friend_SLOT_TYPE == NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_GUILD_LIST);
			if (this.m_GuildUserInfo != null && this.m_GuildUserInfo.gameObject.activeSelf)
			{
				this.m_GuildUserInfo.SetData(cNKMUserProfileData);
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_PROFILE_UNIT_POWER_ALL, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_INFO_POPUP_TITLE, false);
			this.m_tgMain.Select(this.m_deckTab == NKCPopupFriendInfo.DeckTab.Main, true, false);
			this.m_tgRank.Select(this.m_deckTab == NKCPopupFriendInfo.DeckTab.Rank, true, false);
			this.m_tgAsync.Select(this.m_deckTab == NKCPopupFriendInfo.DeckTab.Async, true, false);
			this.SetData(friend_SLOT_TYPE, cNKMUserProfileData);
			this.SetGuildData();
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			if (bRegisterUI)
			{
				base.UIOpened(true);
			}
		}

		// Token: 0x06008145 RID: 33093 RVA: 0x002B8E08 File Offset: 0x002B7008
		public void Open(NKMFierceProfileData fierceData)
		{
			this.m_tgMain.Select(false, false, false);
			this.m_tgRank.Select(false, false, false);
			this.m_tgAsync.Select(false, false, false);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.SetData(fierceData);
			this.SetGuildData();
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			base.UIOpened(true);
		}

		// Token: 0x06008146 RID: 33094 RVA: 0x002B8E70 File Offset: 0x002B7070
		private void OnClickChat()
		{
			if (NKMOpenTagManager.IsOpened("CHAT_PRIVATE"))
			{
				if (this.m_cbtnChat.m_bLock)
				{
					NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_CHAT_BLOCKED, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
					return;
				}
				bool flag;
				NKCContentManager.eContentStatus eContentStatus = NKCContentManager.CheckContentStatus(ContentsType.FRIENDS, out flag, 0, 0);
				if (eContentStatus == NKCContentManager.eContentStatus.Open)
				{
					if (NKCScenManager.GetScenManager().GetGameOptionData().UseChatContent)
					{
						base.Close();
						NKCPacketSender.Send_NKMPacket_PRIVATE_CHAT_LIST_REQ(this.m_cNKMUserProfileData.commonProfile.userUid);
						return;
					}
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_OPTION_GAME_CHAT_NOTICE, null, "");
					return;
				}
				else if (eContentStatus == NKCContentManager.eContentStatus.Lock)
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.FRIENDS, 0);
					return;
				}
			}
			else
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_COMING_SOON_SYSTEM, null, "");
			}
		}

		// Token: 0x06008147 RID: 33095 RVA: 0x002B8F2C File Offset: 0x002B712C
		private void OnClickInvite()
		{
			if (NKCGuildManager.MyGuildData.inviteList.Find((FriendListData x) => x.commonProfile.userUid == this.m_cNKMUserProfileData.commonProfile.userUid) != null)
			{
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_GUILD_ALREADY_INVITED, null, "");
				return;
			}
			if (NKCGuildManager.MyGuildData.inviteList.Count == NKMCommonConst.Guild.MaxInviteCount)
			{
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_GUILD_MAX_INVITE_COUNT, null, "");
				return;
			}
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_CONSORTIUM_POPUP_INVITE_TITLE, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_INVITE_SEND_POPUP_BODY_DESC, this.m_cNKMUserProfileData.commonProfile.nickname), delegate()
			{
				NKCPacketSender.Send_NKMPacket_GUILD_INVITE_REQ(NKCGuildManager.MyData.guildUid, this.m_cNKMUserProfileData.commonProfile.userUid);
			}, null, NKCUtilString.GET_STRING_CONSORTIUM_INVITE, "", false);
		}

		// Token: 0x06008148 RID: 33096 RVA: 0x002B8FCF File Offset: 0x002B71CF
		private void OnClickBlock()
		{
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_WARNING, string.Format(NKCUtilString.GET_STRING_FRIEND_BLOCK_REQ_ONE_PARAM, this.m_cNKMUserProfileData.commonProfile.nickname), new NKCPopupOKCancel.OnButton(this.OnClickBlock_), null, false);
		}

		// Token: 0x06008149 RID: 33097 RVA: 0x002B9004 File Offset: 0x002B7204
		private void OnClickBlock_()
		{
			NKMPacket_FRIEND_BLOCK_REQ nkmpacket_FRIEND_BLOCK_REQ = new NKMPacket_FRIEND_BLOCK_REQ();
			nkmpacket_FRIEND_BLOCK_REQ.friendCode = this.m_cNKMUserProfileData.commonProfile.friendCode;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_FRIEND_BLOCK_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600814A RID: 33098 RVA: 0x002B9040 File Offset: 0x002B7240
		private void OnClickCancelBlockFriend()
		{
			NKMPacket_FRIEND_BLOCK_REQ nkmpacket_FRIEND_BLOCK_REQ = new NKMPacket_FRIEND_BLOCK_REQ();
			nkmpacket_FRIEND_BLOCK_REQ.friendCode = this.m_cNKMUserProfileData.commonProfile.friendCode;
			nkmpacket_FRIEND_BLOCK_REQ.isCancel = true;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_FRIEND_BLOCK_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600814B RID: 33099 RVA: 0x002B9084 File Offset: 0x002B7284
		public void SetData(NKCUIFriendSlot.FRIEND_SLOT_TYPE _FRIEND_SLOT_TYPE, NKMUserProfileData cNKMUserProfileData)
		{
			if (cNKMUserProfileData == null)
			{
				return;
			}
			this.m_cNKMUserProfileData = cNKMUserProfileData;
			this.m_GuildSimpleData = cNKMUserProfileData.guildData;
			this.m_NKM_UI_FRIEND_PROFILE_INFO_NAME.text = cNKMUserProfileData.commonProfile.nickname;
			this.m_NKM_UI_FRIEND_PROFILE_INFO_LEVEL.text = string.Format(NKCUtilString.GET_STRING_FRIEND_INFO_LEVEL_ONE_PARAM, cNKMUserProfileData.commonProfile.level);
			this.m_UID_TEXT.text = NKCUtilString.GetFriendCode(cNKMUserProfileData.commonProfile.friendCode);
			this.m_NKCUISlot.SetProfiledata(cNKMUserProfileData, null);
			this.m_NKM_UI_FRIEND_PROFILE_COMMENT_TEXT.text = NKCFilterManager.CheckBadChat(cNKMUserProfileData.friendIntro);
			this.SetDeck(cNKMUserProfileData, this.m_deckTab);
			this.SetPvP(cNKMUserProfileData, this.m_deckTab);
			bool bValue = NKCFriendManager.IsFriend(cNKMUserProfileData.commonProfile.friendCode) || NKCGuildManager.IsGuildMemberByUID(cNKMUserProfileData.commonProfile.userUid);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_INFO_POPUP_SQUAD_TOGGLE, true);
			if (_FRIEND_SLOT_TYPE == NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_GAUNTLET_LIST)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_OK_ROOT, false);
			}
			else if (_FRIEND_SLOT_TYPE == NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_GAUNTLET_CUSTOM_LIST)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_OK_ROOT, true);
				NKCUtil.SetGameobjectActive(this.m_objFriendMenu, false);
				NKCUtil.SetGameobjectActive(this.m_cbtnChat, bValue);
				NKCUtil.SetGameobjectActive(this.m_cbtnBlock, false);
				NKCUtil.SetGameobjectActive(this.m_objPVPMenu, false);
				NKCUtil.SetGameobjectActive(this.m_csbtnDormitory, false);
			}
			else if (_FRIEND_SLOT_TYPE == NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_NONE)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_OK_ROOT, false);
			}
			else if (_FRIEND_SLOT_TYPE == NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_GUILD_LIST)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_OK_ROOT, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_OK_ROOT, true);
				NKCUtil.SetGameobjectActive(this.m_objPVPMenu, false);
				NKCUtil.SetGameobjectActive(this.m_objFriendMenu, true);
				NKCUtil.SetGameobjectActive(this.m_cbtnChat, bValue);
				NKCUtil.SetGameobjectActive(this.m_cbtnBlock, _FRIEND_SLOT_TYPE == NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_FRIEND_LIST || _FRIEND_SLOT_TYPE == NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_FRIEND_SEARCH || _FRIEND_SLOT_TYPE == NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_FRIEND_SEARCH_RECOMMEND || _FRIEND_SLOT_TYPE == NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_RECEIVE_REQ || _FRIEND_SLOT_TYPE == NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_SENT_REQ);
				NKCUtil.SetGameobjectActive(this.m_cbtnBlockCancel, _FRIEND_SLOT_TYPE == NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_BLOCK_LIST);
				NKCUtil.SetGameobjectActive(this.m_csbtnDormitory, _FRIEND_SLOT_TYPE == NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_FRIEND_LIST || _FRIEND_SLOT_TYPE == NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_OFFICE);
				NKCUtil.SetGameobjectActive(this.m_objDomitoryLocked, !this.m_cNKMUserProfileData.hasOffice);
			}
			if (NKCFriendManager.IsBlockedUser(this.m_cNKMUserProfileData.commonProfile.friendCode))
			{
				this.m_cbtnChat.Lock(false);
			}
			else
			{
				this.m_cbtnChat.UnLock(false);
			}
			if (cNKMUserProfileData.guildData != null && cNKMUserProfileData.guildData.guildUid > 0L)
			{
				NKCUtil.SetGameobjectActive(this.m_cbtnInvite, false);
			}
			else
			{
				MonoBehaviour cbtnInvite = this.m_cbtnInvite;
				bool bValue2;
				if (cNKMUserProfileData.commonProfile.userUid != NKCScenManager.CurrentUserData().m_UserUID && NKCGuildManager.HasGuild())
				{
					bValue2 = (NKCGuildManager.MyGuildData.members.Find((NKMGuildMemberData x) => x.commonProfile.userUid == NKCScenManager.CurrentUserData().m_UserUID).grade != GuildMemberGrade.Member);
				}
				else
				{
					bValue2 = false;
				}
				NKCUtil.SetGameobjectActive(cbtnInvite, bValue2);
			}
			for (int i = 0; i < this.m_lstEmblem.Count; i++)
			{
				NKCUISlot nkcuislot = this.m_lstEmblem[i];
				if (i < cNKMUserProfileData.emblems.Count && cNKMUserProfileData.emblems[i] != null && cNKMUserProfileData.emblems[i].id > 0 && NKMItemManager.GetItemMiscTempletByID(cNKMUserProfileData.emblems[i].id) != null)
				{
					if (i <= 3)
					{
						nkcuislot.SetMiscItemData(cNKMUserProfileData.emblems[i].id, cNKMUserProfileData.emblems[i].count, false, true, true, null);
						nkcuislot.SetOnClickAction(new NKCUISlot.SlotClickType[]
						{
							NKCUISlot.SlotClickType.ItemBox
						});
					}
					else
					{
						nkcuislot.SetEmpty(null);
					}
				}
				else
				{
					nkcuislot.SetEmpty(null);
				}
			}
		}

		// Token: 0x0600814C RID: 33100 RVA: 0x002B9410 File Offset: 0x002B7610
		private void SetGuildData()
		{
			if (this.m_objGuild == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objGuild, this.m_GuildSimpleData != null && this.m_GuildSimpleData.guildUid > 0L);
			if (this.m_objGuild.activeSelf && this.m_GuildSimpleData != null)
			{
				this.m_BadgeUI.SetData(this.m_GuildSimpleData.badgeId);
				NKCUtil.SetLabelText(this.m_lbGuildName, this.m_GuildSimpleData.guildName);
			}
		}

		// Token: 0x0600814D RID: 33101 RVA: 0x002B9492 File Offset: 0x002B7692
		public bool IsSameProfile(int bossGroupID, long userUID)
		{
			return bossGroupID == this.m_iFierceBossGroupID && userUID == this.m_lFierceUserUID;
		}

		// Token: 0x0600814E RID: 33102 RVA: 0x002B94A8 File Offset: 0x002B76A8
		public void SetData(NKMFierceProfileData fierceProfile)
		{
			if (fierceProfile == null)
			{
				return;
			}
			this.m_iFierceBossGroupID = fierceProfile.fierceBossGroupId;
			this.SetDeck(fierceProfile.profileDeck, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_OK_ROOT, true);
			NKCUtil.SetGameobjectActive(this.m_objPVPMenu, false);
			NKCUtil.SetGameobjectActive(this.m_objFriendMenu, true);
			NKCUtil.SetGameobjectActive(this.m_cbtnChat, false);
			NKCUtil.SetGameobjectActive(this.m_cbtnBlock, true);
			NKCUtil.SetGameobjectActive(this.m_cbtnBlockCancel, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_INFO_POPUP_SQUAD_TOGGLE, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_INFO_POPUP_TITLE, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_PROFILE_UNIT_POWER_ALL, true);
			int num = 0;
			int num2 = 0;
			foreach (NKMDummyUnitData nkmdummyUnitData in fierceProfile.profileDeck.List)
			{
				if (nkmdummyUnitData != null)
				{
					NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(nkmdummyUnitData.UnitId);
					if (unitStatTemplet != null)
					{
						num += unitStatTemplet.GetRespawnCost(num2 == 0, null, null);
						num2++;
					}
				}
			}
			NKCUtil.SetLabelText(this.m_ArmyOperationPower, fierceProfile.operationPower.ToString());
			NKCUtil.SetLabelText(this.m_ArmyAvgCost, string.Format("{0:0.00}", num / num2));
		}

		// Token: 0x0600814F RID: 33103 RVA: 0x002B95BE File Offset: 0x002B77BE
		private void SetDeck(NKMUserProfileData userData, NKCPopupFriendInfo.DeckTab deckTab)
		{
			switch (deckTab)
			{
			case NKCPopupFriendInfo.DeckTab.Rank:
				this.SetDeck(userData.leagueDeck, false);
				return;
			case NKCPopupFriendInfo.DeckTab.Async:
				this.SetDeck(userData.defenceDeck);
				return;
			}
			this.SetDeck(userData.profileDeck, false);
		}

		// Token: 0x06008150 RID: 33104 RVA: 0x002B95FC File Offset: 0x002B77FC
		private void SetDeck(NKMDummyDeckData deckData, bool bSetFirstDeckLeader = false)
		{
			if (deckData == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objTroopsPanel, false);
				this.m_lbEmptyMessage.text = NKCUtilString.GET_STRING_FIREND_NO_EXIST_PVP_LOG;
				NKCUtil.SetGameobjectActive(this.m_objEmpty, true);
				NKCUtil.SetGameobjectActive(this.m_OperatorSlot, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objTroopsPanel, true);
			NKCUtil.SetGameobjectActive(this.m_objEmpty, false);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(deckData.Ship.UnitId);
			if (unitTempletBase != null)
			{
				this.m_ANIM_SHIP_IMG.sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, unitTempletBase);
			}
			else
			{
				this.m_ANIM_SHIP_IMG.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_DECK_VIEW_SPRITE", "NKM_DECK_VIEW_SHIP_UNKNOWN", false);
			}
			for (int i = 0; i < this.m_lstNKCDeckViewUnitSlot.Count; i++)
			{
				if (i < 8)
				{
					NKMDummyUnitData nkmdummyUnitData = deckData.List[i];
					NKMUnitData cNKMUnitData = null;
					if (nkmdummyUnitData != null && nkmdummyUnitData.UnitId > 0)
					{
						cNKMUnitData = nkmdummyUnitData.ToUnitData(-1L);
					}
					this.m_lstNKCDeckViewUnitSlot[i].SetData(cNKMUnitData, false);
					if (bSetFirstDeckLeader && i == 0)
					{
						this.m_lstNKCDeckViewUnitSlot[i].SetLeader(true, false);
					}
				}
			}
			if (NKCOperatorUtil.IsHide())
			{
				NKCUtil.SetGameobjectActive(this.m_OperatorSlot, false);
				return;
			}
			if (deckData.operatorUnit == null)
			{
				this.m_OperatorSlot.SetEmpty();
				return;
			}
			NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(deckData.operatorUnit.UnitId);
			if (unitTempletBase2 != null)
			{
				this.m_OperatorSlot.SetData(unitTempletBase2, deckData.operatorUnit.UnitLevel);
				return;
			}
			this.m_OperatorSlot.SetEmpty();
		}

		// Token: 0x06008151 RID: 33105 RVA: 0x002B9768 File Offset: 0x002B7968
		private void SetDeck(NKMAsyncDeckData cNKMAsyncDeckData)
		{
			if (cNKMAsyncDeckData == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objTroopsPanel, false);
				this.m_lbEmptyMessage.text = NKCUtilString.GET_STRING_FIREND_NO_EXIST_ASYNC_LOG;
				NKCUtil.SetGameobjectActive(this.m_objEmpty, true);
				NKCUtil.SetGameobjectActive(this.m_OperatorSlot, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objTroopsPanel, true);
			NKCUtil.SetGameobjectActive(this.m_objEmpty, false);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(cNKMAsyncDeckData.ship.unitId);
			if (unitTempletBase != null)
			{
				this.m_ANIM_SHIP_IMG.sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, unitTempletBase);
			}
			else
			{
				this.m_ANIM_SHIP_IMG.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_DECK_VIEW_SPRITE", "NKM_DECK_VIEW_SHIP_UNKNOWN", false);
			}
			for (int i = 0; i < this.m_lstNKCDeckViewUnitSlot.Count; i++)
			{
				if (i < 8)
				{
					NKMAsyncUnitData nkmasyncUnitData = cNKMAsyncDeckData.units[i];
					NKCDeckViewUnitSlot nkcdeckViewUnitSlot = this.m_lstNKCDeckViewUnitSlot[i];
					if (nkmasyncUnitData.unitId > 0)
					{
						NKMUnitData cNKMUnitData = NKMDungeonManager.MakeUnitDataFromID(nkmasyncUnitData.unitId, -1L, nkmasyncUnitData.unitLevel, nkmasyncUnitData.limitBreakLevel, nkmasyncUnitData.skinId, nkmasyncUnitData.tacticLevel);
						nkcdeckViewUnitSlot.SetData(cNKMUnitData, false);
						nkcdeckViewUnitSlot.SetLeader(false, false);
					}
					else
					{
						nkcdeckViewUnitSlot.SetPrivate();
					}
				}
			}
			if (NKCOperatorUtil.IsHide())
			{
				NKCUtil.SetGameobjectActive(this.m_OperatorSlot, false);
				return;
			}
			if (cNKMAsyncDeckData.operatorUnit == null)
			{
				this.m_OperatorSlot.SetEmpty();
				return;
			}
			NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(cNKMAsyncDeckData.operatorUnit.id);
			if (unitTempletBase2 != null)
			{
				this.m_OperatorSlot.SetData(unitTempletBase2, cNKMAsyncDeckData.operatorUnit.level);
				return;
			}
			this.m_OperatorSlot.SetEmpty();
		}

		// Token: 0x06008152 RID: 33106 RVA: 0x002B98EC File Offset: 0x002B7AEC
		private void SetPvP(NKMUserProfileData userData, NKCPopupFriendInfo.DeckTab deckTab)
		{
			switch (deckTab)
			{
			case NKCPopupFriendInfo.DeckTab.Rank:
				this.SetPvP(NKM_GAME_TYPE.NGT_PVP_RANK, userData.rankPvpData.seasonId, userData.rankPvpData.leagueTierId, userData.rankPvpData.score);
				this.m_NKM_UI_FRIEND_PROFILE_TIER_BG.sprite = this.SpriteTierBG_Rank;
				return;
			case NKCPopupFriendInfo.DeckTab.Async:
				this.SetPvP(NKM_GAME_TYPE.NGT_ASYNC_PVP, userData.asyncPvpData.seasonId, userData.asyncPvpData.leagueTierId, userData.asyncPvpData.score);
				this.m_NKM_UI_FRIEND_PROFILE_TIER_BG.sprite = this.SpriteTierBG_Async;
				return;
			}
			if (userData.rankPvpData.score < userData.asyncPvpData.score)
			{
				this.SetPvP(userData, NKCPopupFriendInfo.DeckTab.Async);
				return;
			}
			this.SetPvP(userData, NKCPopupFriendInfo.DeckTab.Rank);
		}

		// Token: 0x06008153 RID: 33107 RVA: 0x002B99AC File Offset: 0x002B7BAC
		private void SetPvP(NKM_GAME_TYPE gameType, int userSeasonID, int userTierID, int userScore)
		{
			int num = NKCPVPManager.FindPvPSeasonID(gameType, NKCSynchronizedTime.GetServerUTCTime(0.0));
			if (num != userSeasonID)
			{
				userScore = NKCPVPManager.GetResetScore(num, userScore, gameType);
				NKMPvpRankTemplet rankTempletByScore = NKCPVPManager.GetRankTempletByScore(gameType, num, userScore);
				if (rankTempletByScore != null)
				{
					this.m_NKCUILeagueTier.SetUI(rankTempletByScore);
					this.m_NKM_UI_FRIEND_PROFILE_TIER_TIER.text = rankTempletByScore.GetLeagueName();
				}
				else
				{
					this.m_NKM_UI_FRIEND_PROFILE_TIER_TIER.text = "";
				}
			}
			else
			{
				NKMPvpRankTemplet rankTempletByTier = NKCPVPManager.GetRankTempletByTier(gameType, num, userTierID);
				if (rankTempletByTier != null)
				{
					this.m_NKCUILeagueTier.SetUI(rankTempletByTier);
					this.m_NKM_UI_FRIEND_PROFILE_TIER_TIER.text = rankTempletByTier.GetLeagueName();
				}
				else
				{
					this.m_NKM_UI_FRIEND_PROFILE_TIER_TIER.text = "";
				}
			}
			this.m_NKM_UI_FRIEND_PROFILE_TIER_RANK.text = "";
			this.m_NKM_UI_FRIEND_PROFILE_TIER_SCORE.text = userScore.ToString();
		}

		// Token: 0x06008154 RID: 33108 RVA: 0x002B9A78 File Offset: 0x002B7C78
		private void OnToggleChangedMain(bool set)
		{
			if (set)
			{
				this.m_deckTab = NKCPopupFriendInfo.DeckTab.Main;
				this.SetDeck(this.m_cNKMUserProfileData, this.m_deckTab);
				this.SetPvP(this.m_cNKMUserProfileData, this.m_deckTab);
			}
		}

		// Token: 0x06008155 RID: 33109 RVA: 0x002B9AA8 File Offset: 0x002B7CA8
		private void OnToggleChangedRank(bool set)
		{
			if (set)
			{
				this.m_deckTab = NKCPopupFriendInfo.DeckTab.Rank;
				this.SetDeck(this.m_cNKMUserProfileData, this.m_deckTab);
				this.SetPvP(this.m_cNKMUserProfileData, this.m_deckTab);
			}
		}

		// Token: 0x06008156 RID: 33110 RVA: 0x002B9AD8 File Offset: 0x002B7CD8
		private void OnToggleChangedAsync(bool set)
		{
			if (set)
			{
				this.m_deckTab = NKCPopupFriendInfo.DeckTab.Async;
				this.SetDeck(this.m_cNKMUserProfileData, this.m_deckTab);
				this.SetPvP(this.m_cNKMUserProfileData, this.m_deckTab);
			}
		}

		// Token: 0x06008157 RID: 33111 RVA: 0x002B9B08 File Offset: 0x002B7D08
		private void OnClickInviteCustomMatch()
		{
		}

		// Token: 0x06008158 RID: 33112 RVA: 0x002B9B0A File Offset: 0x002B7D0A
		private void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x06008159 RID: 33113 RVA: 0x002B9B1F File Offset: 0x002B7D1F
		public override void OnGuildDataChanged()
		{
			NKCPopupGuildUserInfo guildUserInfo = this.m_GuildUserInfo;
			if (guildUserInfo == null)
			{
				return;
			}
			guildUserInfo.SetData(this.m_cNKMUserProfileData);
		}

		// Token: 0x0600815A RID: 33114 RVA: 0x002B9B38 File Offset: 0x002B7D38
		public void OnClickDormitory()
		{
			if (this.m_cNKMUserProfileData == null)
			{
				return;
			}
			if (this.m_cNKMUserProfileData.hasOffice)
			{
				NKCPacketSender.Send_NKMPacket_OFFICE_STATE_REQ(this.m_cNKMUserProfileData.commonProfile.userUid);
				return;
			}
			NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_OFFICE_FRIEND_CANNOT_VISIT, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
		}

		// Token: 0x04006D5B RID: 27995
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_FRIEND";

		// Token: 0x04006D5C RID: 27996
		private const string UI_ASSET_NAME = "NKM_UI_FRIEND_INFO_POPUP";

		// Token: 0x04006D5D RID: 27997
		private static NKCPopupFriendInfo m_Instance;

		// Token: 0x04006D5E RID: 27998
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x04006D5F RID: 27999
		[Header("소대 토글")]
		public GameObject m_NKM_UI_FRIEND_INFO_POPUP_SQUAD_TOGGLE;

		// Token: 0x04006D60 RID: 28000
		public NKCUIComToggle m_tgMain;

		// Token: 0x04006D61 RID: 28001
		public NKCUIComToggle m_tgRank;

		// Token: 0x04006D62 RID: 28002
		public NKCUIComToggle m_tgAsync;

		// Token: 0x04006D63 RID: 28003
		[Header("정보")]
		public Text m_NKM_UI_FRIEND_PROFILE_INFO_NAME;

		// Token: 0x04006D64 RID: 28004
		public Text m_NKM_UI_FRIEND_PROFILE_INFO_LEVEL;

		// Token: 0x04006D65 RID: 28005
		public Text m_UID_TEXT;

		// Token: 0x04006D66 RID: 28006
		public GameObject m_objGuild;

		// Token: 0x04006D67 RID: 28007
		public NKCUIGuildBadge m_BadgeUI;

		// Token: 0x04006D68 RID: 28008
		public Text m_lbGuildName;

		// Token: 0x04006D69 RID: 28009
		public NKCUISlotProfile m_NKCUISlot;

		// Token: 0x04006D6A RID: 28010
		public Text m_NKM_UI_FRIEND_PROFILE_COMMENT_TEXT;

		// Token: 0x04006D6B RID: 28011
		public Image m_ANIM_SHIP_IMG;

		// Token: 0x04006D6C RID: 28012
		public NKCUIOperatorDeckSlot m_OperatorSlot;

		// Token: 0x04006D6D RID: 28013
		public List<NKCDeckViewUnitSlot> m_lstNKCDeckViewUnitSlot;

		// Token: 0x04006D6E RID: 28014
		public NKCUILeagueTier m_NKCUILeagueTier;

		// Token: 0x04006D6F RID: 28015
		public Text m_NKM_UI_FRIEND_PROFILE_TIER_RANK;

		// Token: 0x04006D70 RID: 28016
		public Text m_NKM_UI_FRIEND_PROFILE_TIER_TIER;

		// Token: 0x04006D71 RID: 28017
		public Text m_NKM_UI_FRIEND_PROFILE_TIER_SCORE;

		// Token: 0x04006D72 RID: 28018
		public Image m_NKM_UI_FRIEND_PROFILE_TIER_BG;

		// Token: 0x04006D73 RID: 28019
		public List<NKCUISlot> m_lstEmblem;

		// Token: 0x04006D74 RID: 28020
		[Header("버튼")]
		public NKCUIComStateButton m_cbtnChat;

		// Token: 0x04006D75 RID: 28021
		public NKCUIComStateButton m_cbtnInvite;

		// Token: 0x04006D76 RID: 28022
		public NKCUIComButton m_cbtnBlock;

		// Token: 0x04006D77 RID: 28023
		public NKCUIComButton m_cbtnBlockCancel;

		// Token: 0x04006D78 RID: 28024
		public NKCUIComStateButton m_csbtnDormitory;

		// Token: 0x04006D79 RID: 28025
		[Space]
		public GameObject m_NKM_UI_POPUP_OK_ROOT;

		// Token: 0x04006D7A RID: 28026
		public GameObject m_objFriendMenu;

		// Token: 0x04006D7B RID: 28027
		public GameObject m_objTroopsPanel;

		// Token: 0x04006D7C RID: 28028
		public GameObject m_objEmpty;

		// Token: 0x04006D7D RID: 28029
		public GameObject m_objDomitoryLocked;

		// Token: 0x04006D7E RID: 28030
		public Text m_lbEmptyMessage;

		// Token: 0x04006D7F RID: 28031
		[Header("Sprite")]
		public Sprite SpriteTierBG_Rank;

		// Token: 0x04006D80 RID: 28032
		public Sprite SpriteTierBG_Async;

		// Token: 0x04006D81 RID: 28033
		[Header("격전지원")]
		public GameObject m_NKM_UI_FRIEND_INFO_POPUP_TITLE;

		// Token: 0x04006D82 RID: 28034
		public Text m_TITLE_TEXT;

		// Token: 0x04006D83 RID: 28035
		[Header("소대 정보")]
		public GameObject m_NKM_UI_FRIEND_PROFILE_UNIT_POWER_ALL;

		// Token: 0x04006D84 RID: 28036
		public Text m_ArmyOperationPower;

		// Token: 0x04006D85 RID: 28037
		public Text m_ArmyAvgCost;

		// Token: 0x04006D86 RID: 28038
		[Header("커스텀 매치 초대")]
		public GameObject m_objPVPMenu;

		// Token: 0x04006D87 RID: 28039
		public NKCUIComStateButton m_csbtnInviteToCustomMatch;

		// Token: 0x04006D88 RID: 28040
		public NKCPopupGuildUserInfo m_GuildUserInfo;

		// Token: 0x04006D89 RID: 28041
		private NKMUserProfileData m_cNKMUserProfileData;

		// Token: 0x04006D8A RID: 28042
		private NKCPopupFriendInfo.DeckTab m_deckTab;

		// Token: 0x04006D8B RID: 28043
		private NKMGuildSimpleData m_GuildSimpleData;

		// Token: 0x04006D8C RID: 28044
		private int m_iFierceBossGroupID;

		// Token: 0x04006D8D RID: 28045
		private long m_lFierceUserUID;

		// Token: 0x020018B1 RID: 6321
		public enum DeckTab
		{
			// Token: 0x0400A999 RID: 43417
			Main,
			// Token: 0x0400A99A RID: 43418
			Rank,
			// Token: 0x0400A99B RID: 43419
			Async
		}
	}
}
