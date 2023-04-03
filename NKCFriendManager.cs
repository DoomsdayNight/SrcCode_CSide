using System;
using System.Collections.Generic;
using ClientPacket.Common;
using NKC;
using NKM;
using NKM.Templet;

// Token: 0x02000006 RID: 6
public static class NKCFriendManager
{
	// Token: 0x17000003 RID: 3
	// (get) Token: 0x0600000D RID: 13 RVA: 0x000022F4 File Offset: 0x000004F4
	public static List<long> FriendList
	{
		get
		{
			return NKCFriendManager.m_friendList;
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x0600000E RID: 14 RVA: 0x000022FB File Offset: 0x000004FB
	public static List<long> ReceivedREQList
	{
		get
		{
			return NKCFriendManager.m_receivdREQList;
		}
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x0600000F RID: 15 RVA: 0x00002302 File Offset: 0x00000502
	public static List<long> BlockList
	{
		get
		{
			return NKCFriendManager.m_blockList;
		}
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x06000010 RID: 16 RVA: 0x00002309 File Offset: 0x00000509
	public static List<FriendListData> FriendListData
	{
		get
		{
			return NKCFriendManager.m_friendListData;
		}
	}

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x06000011 RID: 17 RVA: 0x00002310 File Offset: 0x00000510
	public static bool IsDirty
	{
		get
		{
			return NKCFriendManager.m_bFriendChanged || NKCFriendManager.m_bReceivedREQChanged || NKCFriendManager.m_bBlockChanged;
		}
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00002327 File Offset: 0x00000527
	public static void Initialize()
	{
		NKCFriendManager.m_bFriendChanged = false;
		NKCFriendManager.m_bReceivedREQChanged = false;
		NKCFriendManager.m_bBlockChanged = false;
		NKCFriendManager.m_friendList.Clear();
		NKCFriendManager.m_friendListData.Clear();
		NKCFriendManager.m_receivdREQList.Clear();
		NKCFriendManager.m_blockList.Clear();
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00002364 File Offset: 0x00000564
	public static void SetFriendList(List<FriendListData> list)
	{
		NKCFriendManager.m_friendListData = list;
		NKCFriendManager.m_friendList.Clear();
		for (int i = 0; i < list.Count; i++)
		{
			NKCFriendManager.m_friendList.Add(list[i].commonProfile.friendCode);
		}
		NKCFriendManager.m_bFriendChanged = false;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x000023B3 File Offset: 0x000005B3
	public static void AddFriend(FriendListData friendListData)
	{
		NKCFriendManager.m_friendListData.Add(friendListData);
		NKCFriendManager.m_friendList.Add(friendListData.commonProfile.friendCode);
		NKCFriendManager.m_bFriendChanged = true;
	}

	// Token: 0x06000015 RID: 21 RVA: 0x000023DB File Offset: 0x000005DB
	public static void AddFriend(long friendCode)
	{
		if (!NKCFriendManager.m_friendList.Contains(friendCode))
		{
			NKCFriendManager.m_friendList.Add(friendCode);
			NKCFriendManager.m_bFriendChanged = true;
		}
	}

	// Token: 0x06000016 RID: 22 RVA: 0x000023FC File Offset: 0x000005FC
	public static void DeleteFriend(long friendCode)
	{
		int num = NKCFriendManager.m_friendListData.FindIndex((FriendListData x) => x.commonProfile.friendCode == friendCode);
		if (num >= 0)
		{
			NKCFriendManager.m_friendListData.RemoveAt(num);
			NKCFriendManager.m_bFriendChanged = true;
		}
		if (NKCFriendManager.m_friendList.Contains(friendCode))
		{
			NKCFriendManager.m_friendList.Remove(friendCode);
			NKCFriendManager.m_bFriendChanged = true;
		}
	}

	// Token: 0x06000017 RID: 23 RVA: 0x0000246B File Offset: 0x0000066B
	public static bool IsFriend(long friendCode)
	{
		return NKCFriendManager.m_friendList.Contains(friendCode);
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00002478 File Offset: 0x00000678
	public static int GetFriendCount()
	{
		return NKCFriendManager.m_friendListData.Count;
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002484 File Offset: 0x00000684
	public static void SetReceivedREQList(List<FriendListData> list)
	{
		NKCFriendManager.m_receivdREQList.Clear();
		for (int i = 0; i < list.Count; i++)
		{
			if (!NKCFriendManager.m_receivdREQList.Contains(list[i].commonProfile.friendCode))
			{
				NKCFriendManager.m_receivdREQList.Add(list[i].commonProfile.friendCode);
			}
		}
		NKCFriendManager.m_bReceivedREQChanged = false;
		if (list.Count > 0)
		{
			NKCScenManager.GetScenManager().Get_NKC_SCEN_FRIEND().SetAddReceivedNew(true);
			NKCScenManager.GetScenManager().Get_SCEN_HOME().SetFriendNewIcon(true);
			return;
		}
		NKCScenManager.GetScenManager().Get_NKC_SCEN_FRIEND().SetAddReceivedNew(false);
		NKCScenManager.GetScenManager().Get_SCEN_HOME().SetFriendNewIcon(false);
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00002534 File Offset: 0x00000734
	public static void AddReceivedREQ(long friendCode)
	{
		if (!NKCFriendManager.m_receivdREQList.Contains(friendCode))
		{
			NKCFriendManager.m_receivdREQList.Add(friendCode);
			NKCFriendManager.m_bReceivedREQChanged = true;
		}
		if (NKCFriendManager.m_receivdREQList.Count > 0)
		{
			NKCScenManager.GetScenManager().Get_SCEN_HOME().SetFriendNewIcon(true);
		}
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00002574 File Offset: 0x00000774
	public static void RemoveReceivedREQ(long friendCode)
	{
		if (NKCFriendManager.m_receivdREQList.Contains(friendCode))
		{
			NKCFriendManager.m_receivdREQList.Remove(friendCode);
			NKCFriendManager.m_bReceivedREQChanged = true;
		}
		if (NKCFriendManager.m_receivdREQList.Count > 0)
		{
			NKCScenManager.GetScenManager().Get_NKC_SCEN_FRIEND().SetAddReceivedNew(true);
			NKCScenManager.GetScenManager().Get_SCEN_HOME().SetFriendNewIcon(true);
			return;
		}
		NKCScenManager.GetScenManager().Get_NKC_SCEN_FRIEND().SetAddReceivedNew(false);
		NKCScenManager.GetScenManager().Get_SCEN_HOME().SetFriendNewIcon(false);
	}

	// Token: 0x0600001C RID: 28 RVA: 0x000025F0 File Offset: 0x000007F0
	public static void SetBlockList(List<FriendListData> list)
	{
		NKCFriendManager.m_blockList.Clear();
		for (int i = 0; i < list.Count; i++)
		{
			if (!NKCFriendManager.m_blockList.Contains(list[i].commonProfile.friendCode))
			{
				NKCFriendManager.m_blockList.Add(list[i].commonProfile.friendCode);
			}
		}
		NKCFriendManager.m_bBlockChanged = false;
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00002656 File Offset: 0x00000856
	public static void AddBlockUser(long friendCode)
	{
		if (!NKCFriendManager.m_blockList.Contains(friendCode))
		{
			NKCFriendManager.m_blockList.Add(friendCode);
			NKCFriendManager.m_bBlockChanged = true;
		}
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00002676 File Offset: 0x00000876
	public static void RemoveBlockUser(long friendCode)
	{
		if (NKCFriendManager.m_blockList.Contains(friendCode))
		{
			NKCFriendManager.m_blockList.Remove(friendCode);
			NKCFriendManager.m_bBlockChanged = true;
		}
	}

	// Token: 0x0600001F RID: 31 RVA: 0x00002697 File Offset: 0x00000897
	public static bool IsBlockedUser(long friendCode)
	{
		return NKCFriendManager.m_blockList.Contains(friendCode);
	}

	// Token: 0x06000020 RID: 32 RVA: 0x000026A4 File Offset: 0x000008A4
	public static bool RefreshFriendData()
	{
		bool flag;
		if (NKCContentManager.CheckContentStatus(ContentsType.FRIENDS, out flag, 0, 0) != NKCContentManager.eContentStatus.Open)
		{
			return false;
		}
		bool result = false;
		if (NKCFriendManager.m_bFriendChanged)
		{
			NKCPacketSender.Send_NKMPacket_FRIEND_LIST_REQ(NKM_FRIEND_LIST_TYPE.FRIEND);
			result = true;
		}
		if (NKCFriendManager.m_bReceivedREQChanged)
		{
			NKCPacketSender.Send_NKMPacket_FRIEND_LIST_REQ(NKM_FRIEND_LIST_TYPE.RECEIVE_REQUEST);
			result = true;
		}
		if (NKCFriendManager.m_bBlockChanged)
		{
			NKCPacketSender.Send_NKMPacket_FRIEND_LIST_REQ(NKM_FRIEND_LIST_TYPE.BLOCKER);
			result = true;
		}
		return result;
	}

	// Token: 0x04000011 RID: 17
	private static List<long> m_friendList = new List<long>();

	// Token: 0x04000012 RID: 18
	private static List<long> m_receivdREQList = new List<long>();

	// Token: 0x04000013 RID: 19
	private static List<long> m_blockList = new List<long>();

	// Token: 0x04000014 RID: 20
	private static bool m_bFriendChanged = false;

	// Token: 0x04000015 RID: 21
	private static bool m_bReceivedREQChanged = false;

	// Token: 0x04000016 RID: 22
	private static bool m_bBlockChanged = false;

	// Token: 0x04000017 RID: 23
	private static List<FriendListData> m_friendListData = new List<FriendListData>();
}
