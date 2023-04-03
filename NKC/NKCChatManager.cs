using System;
using System.Collections.Generic;
using ClientPacket.Chat;
using ClientPacket.Common;
using ClientPacket.Guild;
using NKC.Publisher;
using NKC.UI;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000648 RID: 1608
	public static class NKCChatManager
	{
		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06003255 RID: 12885 RVA: 0x000FA5E0 File Offset: 0x000F87E0
		public static List<PrivateChatListData> FriendChatList
		{
			get
			{
				if (NKCFriendManager.FriendListData.Count > NKCChatManager.m_friends.Count)
				{
					using (List<FriendListData>.Enumerator enumerator = NKCFriendManager.FriendListData.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							FriendListData friendListData = enumerator.Current;
							if (NKCChatManager.m_friends.Find((PrivateChatListData x) => x.commonProfile.friendCode == friendListData.commonProfile.friendCode) == null)
							{
								PrivateChatListData privateChatListData = new PrivateChatListData();
								privateChatListData.commonProfile = friendListData.commonProfile;
								NKCChatManager.m_friends.Add(privateChatListData);
								NKCChatManager.AddPrivateChatListData(privateChatListData);
							}
						}
					}
				}
				return NKCChatManager.m_friends;
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06003256 RID: 12886 RVA: 0x000FA694 File Offset: 0x000F8894
		public static List<PrivateChatListData> GuildChatList
		{
			get
			{
				if (NKCGuildManager.MyGuildMemberDataList.Count > NKCChatManager.m_guilds.Count)
				{
					using (List<NKMGuildMemberData>.Enumerator enumerator = NKCGuildManager.MyGuildMemberDataList.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							NKMGuildMemberData guildMemberData = enumerator.Current;
							if (NKCChatManager.m_guilds.Find((PrivateChatListData x) => x.commonProfile.friendCode == guildMemberData.commonProfile.friendCode) == null)
							{
								PrivateChatListData privateChatListData = new PrivateChatListData();
								privateChatListData.commonProfile = guildMemberData.commonProfile;
								NKCChatManager.m_guilds.Add(privateChatListData);
								NKCChatManager.AddPrivateChatListData(privateChatListData);
							}
						}
					}
				}
				return NKCChatManager.m_guilds;
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06003257 RID: 12887 RVA: 0x000FA748 File Offset: 0x000F8948
		// (set) Token: 0x06003258 RID: 12888 RVA: 0x000FA74F File Offset: 0x000F894F
		public static DateTime m_MuteEndDate { get; private set; }

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06003259 RID: 12889 RVA: 0x000FA757 File Offset: 0x000F8957
		// (set) Token: 0x0600325A RID: 12890 RVA: 0x000FA75E File Offset: 0x000F895E
		public static bool HasAnyNewMessage { get; private set; }

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x0600325B RID: 12891 RVA: 0x000FA766 File Offset: 0x000F8966
		// (set) Token: 0x0600325C RID: 12892 RVA: 0x000FA76D File Offset: 0x000F896D
		public static bool bAllListRequested
		{
			get
			{
				return NKCChatManager.m_bAllListRequested;
			}
			set
			{
				NKCChatManager.m_bAllListRequested = value;
			}
		}

		// Token: 0x0600325D RID: 12893 RVA: 0x000FA778 File Offset: 0x000F8978
		public static void Initialize()
		{
			NKCChatManager.m_dicChatList.Clear();
			NKCChatManager.m_dicTranslatedMessage.Clear();
			NKCChatManager.m_dicLastCheckedMessageUid.Clear();
			NKCChatManager.m_friends.Clear();
			NKCChatManager.m_guilds.Clear();
			NKCChatManager.m_LastSendChatRoomUid = 0L;
			NKCChatManager.m_MuteEndDate = default(DateTime);
			NKCChatManager.HasAnyNewMessage = false;
			NKCChatManager.m_bAllListRequested = false;
		}

		// Token: 0x0600325E RID: 12894 RVA: 0x000FA7D8 File Offset: 0x000F89D8
		public static bool IsContentsUnlocked()
		{
			bool flag;
			return NKCContentManager.CheckContentStatus(ContentsType.FRIENDS, out flag, 0, 0) == NKCContentManager.eContentStatus.Open && NKMOpenTagManager.IsOpened("CHAT_PRIVATE");
		}

		// Token: 0x0600325F RID: 12895 RVA: 0x000FA7FE File Offset: 0x000F89FE
		private static string GetLastCheckedChatUidKey(long channelId)
		{
			return string.Format("{0}_{1}_LAST_CHECKED_CHAT_UID", NKCScenManager.CurrentUserData().m_UserUID, channelId);
		}

		// Token: 0x06003260 RID: 12896 RVA: 0x000FA81F File Offset: 0x000F8A1F
		public static long GetLastCheckedMessageUid(long channelUid)
		{
			if (NKCChatManager.m_dicLastCheckedMessageUid.ContainsKey(channelUid))
			{
				return NKCChatManager.m_dicLastCheckedMessageUid[channelUid];
			}
			return 0L;
		}

		// Token: 0x06003261 RID: 12897 RVA: 0x000FA83C File Offset: 0x000F8A3C
		public static DateTime GetLastChatTime(long userUid)
		{
			List<NKMChatMessageData> list;
			if (NKCChatManager.m_dicChatList.TryGetValue(userUid, out list) && list.Count > 0)
			{
				return list[list.Count - 1].createdAt;
			}
			return DateTime.MinValue;
		}

		// Token: 0x06003262 RID: 12898 RVA: 0x000FA87A File Offset: 0x000F8A7A
		public static List<NKMChatMessageData> GetChatList(long channelUid, out bool bWaitForData)
		{
			if (NKCChatManager.m_dicChatList.ContainsKey(channelUid))
			{
				bWaitForData = false;
				return NKCChatManager.m_dicChatList[channelUid];
			}
			NKCChatManager.m_dicChatList.Add(channelUid, new List<NKMChatMessageData>());
			bWaitForData = true;
			NKCPacketSender.Send_NKMPacket_GUILD_CHAT_LIST_REQ(channelUid);
			return null;
		}

		// Token: 0x06003263 RID: 12899 RVA: 0x000FA8B2 File Offset: 0x000F8AB2
		public static string GetTranslatedMessage(long chatUid)
		{
			if (NKCChatManager.m_dicTranslatedMessage.ContainsKey(chatUid))
			{
				return NKCChatManager.m_dicTranslatedMessage[chatUid];
			}
			return string.Empty;
		}

		// Token: 0x06003264 RID: 12900 RVA: 0x000FA8D4 File Offset: 0x000F8AD4
		public static void OnRecvGuildChatList(long channelUid, List<NKMChatMessageData> lstchatData, bool bRefreshUI)
		{
			if (lstchatData == null)
			{
				lstchatData = new List<NKMChatMessageData>();
			}
			if (!NKCChatManager.m_dicChatList.ContainsKey(channelUid))
			{
				NKCChatManager.m_dicChatList.Add(channelUid, lstchatData);
			}
			else
			{
				NKCChatManager.m_dicChatList[channelUid] = lstchatData;
			}
			long num = 0L;
			if (NKCPopupGuildChat.IsInstanceOpen)
			{
				if (lstchatData.Count > 0)
				{
					num = lstchatData[lstchatData.Count - 1].messageUid;
				}
			}
			else if (PlayerPrefs.HasKey(NKCChatManager.GetLastCheckedChatUidKey(NKCGuildManager.MyData.guildUid)))
			{
				num = long.Parse(PlayerPrefs.GetString(NKCChatManager.GetLastCheckedChatUidKey(NKCGuildManager.MyData.guildUid)));
			}
			if (num > 0L)
			{
				if (NKCChatManager.m_dicLastCheckedMessageUid.ContainsKey(NKCGuildManager.MyData.guildUid))
				{
					NKCChatManager.m_dicLastCheckedMessageUid[NKCGuildManager.MyData.guildUid] = num;
				}
				else
				{
					NKCChatManager.m_dicLastCheckedMessageUid.Add(NKCGuildManager.MyData.guildUid, num);
				}
			}
			if (NKCPopupGuildChat.IsInstanceOpen)
			{
				NKCPopupGuildChat.Instance.OnChatDataReceived(channelUid, NKCChatManager.m_dicChatList[channelUid], bRefreshUI);
				return;
			}
			Debug.LogWarning("안읽은 메세지 : " + NKCChatManager.GetUncheckedMessageCount(channelUid).ToString());
		}

		// Token: 0x06003265 RID: 12901 RVA: 0x000FA9F0 File Offset: 0x000F8BF0
		public static void OnRecv(NKMPacket_GUILD_CHAT_NOT cNKMPacket_GUILD_CHAT_NOT)
		{
			if (!NKCChatManager.m_dicChatList.ContainsKey(NKCGuildManager.MyData.guildUid))
			{
				NKCChatManager.m_dicChatList.Add(NKCGuildManager.MyData.guildUid, new List<NKMChatMessageData>
				{
					cNKMPacket_GUILD_CHAT_NOT.message
				});
			}
			else
			{
				if (NKCChatManager.m_dicChatList[NKCGuildManager.MyData.guildUid].Find((NKMChatMessageData x) => x.messageUid == cNKMPacket_GUILD_CHAT_NOT.message.messageUid && x.message != null) != null)
				{
					return;
				}
				NKCChatManager.m_dicChatList[NKCGuildManager.MyData.guildUid].Add(cNKMPacket_GUILD_CHAT_NOT.message);
				if (NKCChatManager.m_dicChatList[NKCGuildManager.MyData.guildUid].Count > 100)
				{
					int num = NKCChatManager.m_dicChatList[NKCGuildManager.MyData.guildUid].Count - 100;
					for (int i = 0; i < num; i++)
					{
						NKCChatManager.m_dicChatList[NKCGuildManager.MyData.guildUid].RemoveAt(0);
					}
				}
			}
			if (NKCPopupGuildChat.IsInstanceOpen)
			{
				NKCPopupGuildChat.Instance.AddMessage(cNKMPacket_GUILD_CHAT_NOT.message);
				return;
			}
			if (NKCPopupPrivateChatLobby.IsInstanceOpen)
			{
				NKCPopupPrivateChatLobby.Instance.RefreshUI(false);
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GUILD_LOBBY)
			{
				NKCChatManager.HasAnyNewMessage = true;
			}
		}

		// Token: 0x06003266 RID: 12902 RVA: 0x000FAB40 File Offset: 0x000F8D40
		public static bool CheckGuildChatNotify()
		{
			if (!NKCGuildManager.HasGuild())
			{
				return false;
			}
			long guildUid = NKCGuildManager.MyGuildData.guildUid;
			if (NKCChatManager.m_dicChatList.ContainsKey(guildUid))
			{
				List<NKMChatMessageData> list = NKCChatManager.m_dicChatList[guildUid];
				if (list.Count > 0)
				{
					NKMChatMessageData nkmchatMessageData = list[list.Count - 1];
					if (nkmchatMessageData.createdAt > NKMTime.UTCtoLocal(NKCScenManager.CurrentUserData().m_NKMUserDateData.m_LastLogOutTime, 0))
					{
						if (!NKCChatManager.m_dicLastCheckedMessageUid.ContainsKey(guildUid))
						{
							return true;
						}
						if (nkmchatMessageData.messageUid != NKCChatManager.m_dicLastCheckedMessageUid[guildUid])
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06003267 RID: 12903 RVA: 0x000FABD9 File Offset: 0x000F8DD9
		public static void SetCurrentChatRoomUid(long chatRoomUid)
		{
			NKCChatManager.m_LastSendChatRoomUid = chatRoomUid;
		}

		// Token: 0x06003268 RID: 12904 RVA: 0x000FABE1 File Offset: 0x000F8DE1
		public static void Init()
		{
		}

		// Token: 0x06003269 RID: 12905 RVA: 0x000FABE4 File Offset: 0x000F8DE4
		public static void AddPrivateChatListData(PrivateChatListData privateChatListData)
		{
			if (privateChatListData == null)
			{
				return;
			}
			if (privateChatListData.lastMessage == null)
			{
				return;
			}
			if (NKCChatManager.m_dicChatList.ContainsKey(privateChatListData.commonProfile.userUid))
			{
				NKCChatManager.m_dicChatList[privateChatListData.commonProfile.userUid] = new List<NKMChatMessageData>
				{
					privateChatListData.lastMessage
				};
				return;
			}
			NKCChatManager.m_dicChatList.Add(privateChatListData.commonProfile.userUid, new List<NKMChatMessageData>
			{
				privateChatListData.lastMessage
			});
		}

		// Token: 0x0600326A RID: 12906 RVA: 0x000FAC64 File Offset: 0x000F8E64
		public static void OnRecvAllChat(NKMPacket_PRIVATE_CHAT_ALL_LIST_ACK sPacket)
		{
			NKCChatManager.m_friends = sPacket.friends;
			NKCChatManager.m_guilds = sPacket.guildMembers;
			foreach (PrivateChatListData privateChatListData in sPacket.friends)
			{
				NKCChatManager.AddPrivateChatListData(privateChatListData);
			}
			foreach (PrivateChatListData privateChatListData2 in sPacket.guildMembers)
			{
				NKCChatManager.AddPrivateChatListData(privateChatListData2);
			}
			if (NKCPopupPrivateChatLobby.IsInstanceOpen)
			{
				NKCPopupPrivateChatLobby.Instance.RefreshUI(false);
			}
		}

		// Token: 0x0600326B RID: 12907 RVA: 0x000FAD1C File Offset: 0x000F8F1C
		public static void OnRecv(NKMPacket_PRIVATE_CHAT_NOT sPacket)
		{
			bool flag = false;
			bool flag2 = sPacket.message.commonProfile.userUid == NKCScenManager.CurrentUserData().m_UserUID;
			long targetRoomUid = 0L;
			if (flag2)
			{
				targetRoomUid = NKCChatManager.m_LastSendChatRoomUid;
			}
			else
			{
				targetRoomUid = sPacket.message.commonProfile.userUid;
			}
			if (targetRoomUid == 0L)
			{
				return;
			}
			if (!NKCChatManager.m_dicChatList.ContainsKey(targetRoomUid))
			{
				NKCChatManager.m_dicChatList.Add(targetRoomUid, new List<NKMChatMessageData>
				{
					sPacket.message
				});
			}
			else
			{
				if (NKCChatManager.m_dicChatList[targetRoomUid].Find((NKMChatMessageData x) => x.messageUid == sPacket.message.messageUid && x.message != null) != null)
				{
					return;
				}
				NKCChatManager.m_dicChatList[targetRoomUid].Add(sPacket.message);
				if (NKCChatManager.m_dicChatList[targetRoomUid].Count > 100)
				{
					int num = NKCChatManager.m_dicChatList[targetRoomUid].Count - 100;
					for (int i = 0; i < num; i++)
					{
						NKCChatManager.m_dicChatList[targetRoomUid].RemoveAt(0);
					}
				}
			}
			bool flag3;
			if (NKCContentManager.CheckContentStatus(ContentsType.FRIENDS, out flag3, 0, 0) == NKCContentManager.eContentStatus.Open)
			{
				FriendListData friendListData = NKCFriendManager.FriendListData.Find((FriendListData x) => x.commonProfile.userUid == targetRoomUid);
				if (friendListData != null)
				{
					PrivateChatListData privateChatListData = NKCChatManager.m_friends.Find((PrivateChatListData x) => x.commonProfile.userUid == targetRoomUid);
					if (privateChatListData != null)
					{
						privateChatListData.lastMessage = sPacket.message;
					}
					else
					{
						PrivateChatListData privateChatListData2 = new PrivateChatListData();
						privateChatListData2.commonProfile = friendListData.commonProfile;
						privateChatListData2.lastMessage = sPacket.message;
						NKCChatManager.m_friends.Add(privateChatListData2);
					}
					flag = true;
				}
			}
			if (NKCGuildManager.HasGuild())
			{
				NKMGuildMemberData nkmguildMemberData = NKCGuildManager.MyGuildMemberDataList.Find((NKMGuildMemberData x) => x.commonProfile.userUid == targetRoomUid);
				if (nkmguildMemberData != null)
				{
					PrivateChatListData privateChatListData3 = NKCChatManager.m_guilds.Find((PrivateChatListData x) => x.commonProfile.userUid == targetRoomUid);
					if (privateChatListData3 != null)
					{
						privateChatListData3.lastMessage = sPacket.message;
					}
					else
					{
						PrivateChatListData privateChatListData4 = new PrivateChatListData();
						privateChatListData4.commonProfile = nkmguildMemberData.commonProfile;
						privateChatListData4.lastMessage = sPacket.message;
						NKCChatManager.m_guilds.Add(privateChatListData4);
					}
					flag = true;
				}
			}
			if (NKCPopupPrivateChatLobby.IsInstanceOpen)
			{
				NKCPopupPrivateChatLobby.Instance.AddMessage(sPacket.message, flag2);
				return;
			}
			if (flag && NKCScenManager.GetScenManager().GetGameOptionData().UseChatContent)
			{
				if (NKCPopupHamburgerMenu.IsInstanceOpen)
				{
					NKCPopupHamburgerMenu.instance.Refresh();
					return;
				}
				NKCUIManager.NKCUIUpsideMenu.SetHamburgerNotify(true);
			}
		}

		// Token: 0x0600326C RID: 12908 RVA: 0x000FAFDC File Offset: 0x000F91DC
		public static void OnRecv(NKMPacket_PRIVATE_CHAT_LIST_ACK sPacket)
		{
			if (sPacket.messages == null)
			{
				sPacket.messages = new List<NKMChatMessageData>();
			}
			if (NKCChatManager.m_dicChatList.ContainsKey(sPacket.userUid))
			{
				NKCChatManager.m_dicChatList[sPacket.userUid] = sPacket.messages;
			}
			else
			{
				NKCChatManager.m_dicChatList.Add(sPacket.userUid, sPacket.messages);
			}
			if (sPacket.messages.Count > 0)
			{
				FriendListData friendListData = NKCFriendManager.FriendListData.Find((FriendListData x) => x.commonProfile.userUid == sPacket.userUid);
				if (friendListData != null)
				{
					PrivateChatListData privateChatListData = NKCChatManager.m_friends.Find((PrivateChatListData x) => x.commonProfile.userUid == sPacket.userUid);
					if (privateChatListData != null)
					{
						privateChatListData.lastMessage = sPacket.messages[sPacket.messages.Count - 1];
					}
					else
					{
						PrivateChatListData privateChatListData2 = new PrivateChatListData();
						privateChatListData2.commonProfile = friendListData.commonProfile;
						privateChatListData2.lastMessage = sPacket.messages[sPacket.messages.Count - 1];
						NKCChatManager.m_friends.Add(privateChatListData2);
					}
				}
				if (NKCGuildManager.HasGuild())
				{
					NKMGuildMemberData nkmguildMemberData = NKCGuildManager.MyGuildMemberDataList.Find((NKMGuildMemberData x) => x.commonProfile.userUid == sPacket.userUid);
					if (nkmguildMemberData != null)
					{
						PrivateChatListData privateChatListData3 = NKCChatManager.m_guilds.Find((PrivateChatListData x) => x.commonProfile.userUid == sPacket.userUid);
						if (privateChatListData3 != null)
						{
							privateChatListData3.lastMessage = sPacket.messages[sPacket.messages.Count - 1];
						}
						else
						{
							PrivateChatListData privateChatListData4 = new PrivateChatListData();
							privateChatListData4.commonProfile = nkmguildMemberData.commonProfile;
							privateChatListData4.lastMessage = sPacket.messages[sPacket.messages.Count - 1];
							NKCChatManager.m_guilds.Add(privateChatListData4);
						}
					}
				}
			}
			if (NKCPopupPrivateChatLobby.IsInstanceOpen)
			{
				NKCPopupPrivateChatLobby.Instance.ShowPrivateChat(sPacket.userUid);
				return;
			}
			NKCPopupPrivateChatLobby.Instance.Open(sPacket.userUid);
		}

		// Token: 0x0600326D RID: 12909 RVA: 0x000FB214 File Offset: 0x000F9414
		public static void AddFriend(NKMCommonProfile commonProfile)
		{
			if (NKCChatManager.m_friends.Find((PrivateChatListData x) => x.commonProfile.userUid == commonProfile.userUid) == null)
			{
				PrivateChatListData privateChatListData = new PrivateChatListData();
				privateChatListData.commonProfile = commonProfile;
				privateChatListData.lastMessage = new NKMChatMessageData();
				privateChatListData.lastMessage.commonProfile = commonProfile;
				NKCChatManager.m_friends.Add(privateChatListData);
			}
		}

		// Token: 0x0600326E RID: 12910 RVA: 0x000FB280 File Offset: 0x000F9480
		public static void RemoveFriendByFriendCode(long friendCode)
		{
			PrivateChatListData privateChatListData = NKCChatManager.m_friends.Find((PrivateChatListData x) => x.commonProfile.friendCode == friendCode);
			if (privateChatListData != null)
			{
				NKCChatManager.m_friends.Remove(privateChatListData);
			}
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x000FB2C0 File Offset: 0x000F94C0
		public static bool CheckPrivateChatNotify(NKMUserData userData, long userUid = 0L)
		{
			return NKCChatManager.CheckPrivateChatNotifyByTabType(NKCPopupPrivateChatLobby.CHAT_LOBBY_TAB_TYPE.FRIEND, userUid) || NKCChatManager.CheckPrivateChatNotifyByTabType(NKCPopupPrivateChatLobby.CHAT_LOBBY_TAB_TYPE.GUILD_MEMBER, userUid);
		}

		// Token: 0x06003270 RID: 12912 RVA: 0x000FB2D4 File Offset: 0x000F94D4
		public static bool CheckPrivateChatNotifyByTabType(NKCPopupPrivateChatLobby.CHAT_LOBBY_TAB_TYPE tabType, long userUid = 0L)
		{
			if (!NKCScenManager.GetScenManager().GetGameOptionData().UseChatContent)
			{
				return false;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (tabType != NKCPopupPrivateChatLobby.CHAT_LOBBY_TAB_TYPE.FRIEND)
			{
				if (tabType == NKCPopupPrivateChatLobby.CHAT_LOBBY_TAB_TYPE.GUILD_MEMBER)
				{
					for (int i = 0; i < NKCChatManager.m_guilds.Count; i++)
					{
						if (NKCChatManager.m_guilds[i].lastMessage != null && NKCChatManager.m_guilds[i].lastMessage.message != null && (userUid <= 0L || NKCChatManager.m_guilds[i].commonProfile.userUid == userUid) && NKCChatManager.m_guilds[i].lastMessage.createdAt > NKMTime.UTCtoLocal(nkmuserData.m_NKMUserDateData.m_LastLogOutTime, 0))
						{
							if (!NKCChatManager.m_dicLastCheckedMessageUid.ContainsKey(NKCChatManager.m_guilds[i].commonProfile.userUid))
							{
								return true;
							}
							if (NKCChatManager.m_dicLastCheckedMessageUid[NKCChatManager.m_guilds[i].commonProfile.userUid] != NKCChatManager.m_guilds[i].lastMessage.messageUid)
							{
								return true;
							}
						}
					}
				}
			}
			else
			{
				for (int j = 0; j < NKCChatManager.m_friends.Count; j++)
				{
					if (NKCChatManager.m_friends[j].lastMessage != null && NKCChatManager.m_friends[j].lastMessage.message != null && (userUid <= 0L || NKCChatManager.m_friends[j].commonProfile.userUid == userUid) && NKCChatManager.m_friends[j].lastMessage.createdAt > NKMTime.UTCtoLocal(nkmuserData.m_NKMUserDateData.m_LastLogOutTime, 0))
					{
						if (!NKCChatManager.m_dicLastCheckedMessageUid.ContainsKey(NKCChatManager.m_friends[j].commonProfile.userUid))
						{
							return true;
						}
						if (NKCChatManager.m_dicLastCheckedMessageUid[NKCChatManager.m_friends[j].commonProfile.userUid] != NKCChatManager.m_friends[j].lastMessage.messageUid)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06003271 RID: 12913 RVA: 0x000FB4F4 File Offset: 0x000F96F4
		private static void OnRecv(long uid, NKMChatMessageData message)
		{
			if (NKCChatManager.m_dicChatList.ContainsKey(uid))
			{
				NKCChatManager.m_dicChatList.Add(uid, new List<NKMChatMessageData>
				{
					message
				});
				return;
			}
			if (NKCChatManager.m_dicChatList[uid].Find((NKMChatMessageData x) => x.messageUid == message.messageUid) != null)
			{
				return;
			}
			NKCChatManager.m_dicChatList[uid].Add(message);
			if (NKCChatManager.m_dicChatList[uid].Count > 100)
			{
				int num = NKCChatManager.m_dicChatList[uid].Count - 100;
				for (int i = 0; i < num; i++)
				{
					NKCChatManager.m_dicChatList[uid].RemoveAt(0);
				}
			}
		}

		// Token: 0x06003272 RID: 12914 RVA: 0x000FB5B2 File Offset: 0x000F97B2
		public static void OnRecv(NKC_PUBLISHER_RESULT_CODE resultCode, string translatedString, long chatUID, string additionalError)
		{
			if (!NKCPublisherModule.CheckError(resultCode, additionalError, true, null, false))
			{
				return;
			}
			if (!NKCChatManager.m_dicTranslatedMessage.ContainsKey(chatUID))
			{
				NKCChatManager.m_dicTranslatedMessage.Add(chatUID, translatedString);
			}
			NKCPopupGuildChat.Instance.RefreshList(false);
		}

		// Token: 0x06003273 RID: 12915 RVA: 0x000FB5E8 File Offset: 0x000F97E8
		public static int GetUncheckedMessageCount(long channelUid)
		{
			if (!NKCChatManager.m_dicChatList.ContainsKey(channelUid))
			{
				return 0;
			}
			long messageUid = 0L;
			if (NKCChatManager.m_dicLastCheckedMessageUid.ContainsKey(channelUid))
			{
				messageUid = NKCChatManager.m_dicLastCheckedMessageUid[channelUid];
			}
			int num = NKCChatManager.m_dicChatList[channelUid].FindIndex((NKMChatMessageData x) => x.messageUid == messageUid);
			return NKCChatManager.m_dicChatList[channelUid].Count - (num + 1);
		}

		// Token: 0x06003274 RID: 12916 RVA: 0x000FB664 File Offset: 0x000F9864
		public static void SetLastCheckedMeesageUid(long channelUid, long messageUid)
		{
			if (channelUid == NKCGuildManager.MyData.guildUid)
			{
				PlayerPrefs.SetString(NKCChatManager.GetLastCheckedChatUidKey(NKCGuildManager.MyData.guildUid), messageUid.ToString());
			}
			if (NKCChatManager.m_dicLastCheckedMessageUid.ContainsKey(channelUid))
			{
				NKCChatManager.m_dicLastCheckedMessageUid[channelUid] = messageUid;
				return;
			}
			NKCChatManager.m_dicLastCheckedMessageUid.Add(channelUid, messageUid);
		}

		// Token: 0x06003275 RID: 12917 RVA: 0x000FB6BF File Offset: 0x000F98BF
		public static void SetMuteEndDate(DateTime endTime)
		{
			NKCChatManager.m_MuteEndDate = endTime;
			if (NKCPopupGuildChat.IsInstanceOpen)
			{
				NKCPopupGuildChat.Instance.CheckMute();
			}
		}

		// Token: 0x06003276 RID: 12918 RVA: 0x000FB6D8 File Offset: 0x000F98D8
		public static List<int> GetEmoticons()
		{
			List<int> list = new List<int>();
			foreach (int num in NKCEmoticonManager.m_hsEmoticonCollection)
			{
				NKMEmoticonTemplet nkmemoticonTemplet = NKMEmoticonTemplet.Find(num);
				if (nkmemoticonTemplet != null && nkmemoticonTemplet.m_EmoticonType == NKM_EMOTICON_TYPE.NET_ANI)
				{
					list.Add(num);
				}
			}
			list.Sort();
			return list;
		}

		// Token: 0x06003277 RID: 12919 RVA: 0x000FB74C File Offset: 0x000F994C
		public static void ResetGuildMemberChatList()
		{
			NKCChatManager.m_guilds.Clear();
		}

		// Token: 0x04003142 RID: 12610
		public const int MAX_CHAT_COUNT = 100;

		// Token: 0x04003143 RID: 12611
		private static Dictionary<long, List<NKMChatMessageData>> m_dicChatList = new Dictionary<long, List<NKMChatMessageData>>();

		// Token: 0x04003144 RID: 12612
		private static Dictionary<long, string> m_dicTranslatedMessage = new Dictionary<long, string>();

		// Token: 0x04003145 RID: 12613
		private static Dictionary<long, long> m_dicLastCheckedMessageUid = new Dictionary<long, long>();

		// Token: 0x04003146 RID: 12614
		private static List<PrivateChatListData> m_friends = new List<PrivateChatListData>();

		// Token: 0x04003147 RID: 12615
		private static List<PrivateChatListData> m_guilds = new List<PrivateChatListData>();

		// Token: 0x0400314A RID: 12618
		private static long m_LastSendChatRoomUid = 0L;

		// Token: 0x0400314B RID: 12619
		private static bool m_bAllListRequested = false;
	}
}
