using System;
using ClientPacket.Chat;
using ClientPacket.Common;
using NKC.UI.Guild;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000922 RID: 2338
	public class NKCPopupPrivateChatLobbySlot : MonoBehaviour
	{
		// Token: 0x06005DA3 RID: 23971 RVA: 0x001CE4AC File Offset: 0x001CC6AC
		public void Init()
		{
			NKCUISlotProfile slot = this.m_Slot;
			if (slot != null)
			{
				slot.Init();
			}
			this.m_btnChat.PointerClick.RemoveAllListeners();
			this.m_btnChat.PointerClick.AddListener(new UnityAction(this.OnClickSlot));
			if (this.m_btnInfo != null)
			{
				this.m_btnInfo.PointerClick.RemoveAllListeners();
				this.m_btnInfo.PointerClick.AddListener(new UnityAction(this.OnClickInfo));
			}
		}

		// Token: 0x06005DA4 RID: 23972 RVA: 0x001CE530 File Offset: 0x001CC730
		public void OnRefreshUI()
		{
			this.m_deltaTime = 3f;
		}

		// Token: 0x06005DA5 RID: 23973 RVA: 0x001CE540 File Offset: 0x001CC740
		public void SetData(NKCPopupPrivateChatLobby.CHAT_LOBBY_TAB_TYPE lobbyTabType, PrivateChatListData privateChatData, NKCPopupPrivateChatLobbySlot.OnClickChat dOnClickChat, bool bEnableRedDot = false)
		{
			this.m_prevChatTimeUpdate = DateTime.MinValue;
			NKCUtil.SetLabelText(this.m_lbLastChatTime, "-");
			this.m_dOnClickChat = dOnClickChat;
			this.m_commonProfile = privateChatData.commonProfile;
			this.m_Slot.SetProfiledata(this.m_commonProfile, delegate(NKCUISlotProfile slot, int framId)
			{
				this.OnClickSlot();
			});
			NKCUtil.SetLabelText(this.m_lbLevel, NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, new object[]
			{
				this.m_commonProfile.level
			});
			NKCUtil.SetLabelText(this.m_lbName, this.m_commonProfile.nickname);
			NKCUtil.SetLabelText(this.m_lbFriendCode, string.Format("#{0}", this.m_commonProfile.friendCode));
			if (privateChatData.lastMessage != null)
			{
				this.m_LastChatTime = privateChatData.lastMessage.createdAt;
			}
			else
			{
				this.m_LastChatTime = DateTime.MinValue;
			}
			this.SetLastChatTime();
			if (lobbyTabType == NKCPopupPrivateChatLobby.CHAT_LOBBY_TAB_TYPE.FRIEND)
			{
				FriendListData friendListData = NKCFriendManager.FriendListData.Find((FriendListData x) => x.commonProfile.userUid == privateChatData.commonProfile.userUid);
				if (friendListData != null)
				{
					if (friendListData.guildData != null)
					{
						NKCUtil.SetGameobjectActive(this.m_objGuild, friendListData.guildData.guildUid > 0L);
						if (friendListData.guildData.guildUid > 0L)
						{
							this.m_GuildBadge.SetData(friendListData.guildData.badgeId);
							NKCUtil.SetLabelText(this.m_lbGuildName, friendListData.guildData.guildName);
						}
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_objGuild, false);
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(base.gameObject, false);
				}
			}
			else if (lobbyTabType == NKCPopupPrivateChatLobby.CHAT_LOBBY_TAB_TYPE.GUILD_MEMBER)
			{
				NKCUtil.SetGameobjectActive(this.m_objGuild, NKCGuildManager.MyData.guildUid > 0L && NKCGuildManager.MyGuildData != null);
				if (NKCGuildManager.MyData.guildUid > 0L)
				{
					this.m_GuildBadge.SetData(NKCGuildManager.MyGuildData.badgeId);
					NKCUtil.SetLabelText(this.m_lbGuildName, NKCGuildManager.MyGuildData.name);
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objRedDot, bEnableRedDot);
		}

		// Token: 0x06005DA6 RID: 23974 RVA: 0x001CE758 File Offset: 0x001CC958
		private void SetCommonData()
		{
			this.m_Slot.SetProfiledata(this.m_commonProfile, delegate(NKCUISlotProfile slot, int framId)
			{
				this.OnClickSlot();
			});
			NKCUtil.SetLabelText(this.m_lbLevel, NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, new object[]
			{
				this.m_commonProfile.level
			});
			NKCUtil.SetLabelText(this.m_lbName, this.m_commonProfile.nickname);
			NKCUtil.SetLabelText(this.m_lbFriendCode, string.Format("#{0}", this.m_commonProfile.friendCode));
			this.m_LastChatTime = NKCChatManager.GetLastChatTime(this.m_commonProfile.userUid);
			this.SetLastChatTime();
		}

		// Token: 0x06005DA7 RID: 23975 RVA: 0x001CE804 File Offset: 0x001CCA04
		private void SetLastChatTime()
		{
			this.m_LastChatTime = NKCChatManager.GetLastChatTime(this.m_commonProfile.userUid);
			if (this.m_prevChatTimeUpdate != this.m_LastChatTime && this.m_LastChatTime > DateTime.MinValue)
			{
				this.m_prevChatTimeUpdate = this.m_LastChatTime;
				NKCUtil.SetLabelText(this.m_lbLastChatTime, NKCUtilString.GetLastTimeString(NKMTime.LocalToUTC(this.m_LastChatTime, 0)));
			}
		}

		// Token: 0x06005DA8 RID: 23976 RVA: 0x001CE874 File Offset: 0x001CCA74
		private void OnClickInfo()
		{
			NKCPacketSender.Send_NKMPacket_USER_PROFILE_INFO_REQ(this.m_commonProfile.userUid, NKM_DECK_TYPE.NDT_NORMAL);
		}

		// Token: 0x06005DA9 RID: 23977 RVA: 0x001CE887 File Offset: 0x001CCA87
		private void OnClickSlot()
		{
			NKCPopupPrivateChatLobbySlot.OnClickChat dOnClickChat = this.m_dOnClickChat;
			if (dOnClickChat == null)
			{
				return;
			}
			dOnClickChat(this.m_commonProfile);
		}

		// Token: 0x06005DAA RID: 23978 RVA: 0x001CE89F File Offset: 0x001CCA9F
		private void Update()
		{
			if (this.m_commonProfile == null)
			{
				return;
			}
			this.m_deltaTime += Time.deltaTime;
			if (this.m_deltaTime >= 3f)
			{
				this.m_deltaTime = 0f;
				this.SetLastChatTime();
			}
		}

		// Token: 0x040049D2 RID: 18898
		[Header("프로필 슬롯")]
		public NKCUISlotProfile m_Slot;

		// Token: 0x040049D3 RID: 18899
		[Header("기본정보")]
		public Text m_lbLevel;

		// Token: 0x040049D4 RID: 18900
		public Text m_lbName;

		// Token: 0x040049D5 RID: 18901
		[Header("유저 상세정보")]
		public NKCUIComStateButton m_btnInfo;

		// Token: 0x040049D6 RID: 18902
		[Header("컨소시움")]
		public GameObject m_objGuild;

		// Token: 0x040049D7 RID: 18903
		public NKCUIGuildBadge m_GuildBadge;

		// Token: 0x040049D8 RID: 18904
		public Text m_lbGuildName;

		// Token: 0x040049D9 RID: 18905
		[Header("사업자 등록 번호")]
		public Text m_lbFriendCode;

		// Token: 0x040049DA RID: 18906
		[Header("시간")]
		public Text m_lbLastChatTime;

		// Token: 0x040049DB RID: 18907
		[Header("우측 버튼")]
		public NKCUIComStateButton m_btnChat;

		// Token: 0x040049DC RID: 18908
		public GameObject m_objRedDot;

		// Token: 0x040049DD RID: 18909
		private NKCPopupPrivateChatLobbySlot.OnClickChat m_dOnClickChat;

		// Token: 0x040049DE RID: 18910
		private NKMCommonProfile m_commonProfile;

		// Token: 0x040049DF RID: 18911
		private DateTime m_LastChatTime;

		// Token: 0x040049E0 RID: 18912
		private DateTime m_prevChatTimeUpdate;

		// Token: 0x040049E1 RID: 18913
		private const float LASTCHAT_UPDATE_INTERVAL = 3f;

		// Token: 0x040049E2 RID: 18914
		private float m_deltaTime;

		// Token: 0x020015B0 RID: 5552
		// (Invoke) Token: 0x0600ADE7 RID: 44519
		public delegate void OnClickChat(NKMCommonProfile commonProfile);
	}
}
