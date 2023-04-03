using System;
using ClientPacket.Community;
using NKM;

namespace NKC.UI.Friend
{
	// Token: 0x02000B10 RID: 2832
	public class NKCUIFriend : NKCUIBase
	{
		// Token: 0x0600806F RID: 32879 RVA: 0x002B4DD3 File Offset: 0x002B2FD3
		public static NKCUIManager.LoadedUIData OpenNewInstanceAsync()
		{
			if (!NKCUIManager.IsValid(NKCUIFriend.s_LoadedUIData))
			{
				NKCUIFriend.s_LoadedUIData = NKCUIManager.OpenNewInstanceAsync<NKCUIFriend>("ab_ui_nkm_ui_friend", "NKM_UI_FRIEND", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIFriend.CleanupInstance));
			}
			return NKCUIFriend.s_LoadedUIData;
		}

		// Token: 0x1700150E RID: 5390
		// (get) Token: 0x06008070 RID: 32880 RVA: 0x002B4E07 File Offset: 0x002B3007
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIFriend.s_LoadedUIData != null && NKCUIFriend.s_LoadedUIData.IsUIOpen;
			}
		}

		// Token: 0x1700150F RID: 5391
		// (get) Token: 0x06008071 RID: 32881 RVA: 0x002B4E1C File Offset: 0x002B301C
		public static bool IsInstanceLoaded
		{
			get
			{
				return NKCUIFriend.s_LoadedUIData != null && NKCUIFriend.s_LoadedUIData.IsLoadComplete;
			}
		}

		// Token: 0x06008072 RID: 32882 RVA: 0x002B4E31 File Offset: 0x002B3031
		public static NKCUIFriend GetInstance()
		{
			if (NKCUIFriend.s_LoadedUIData != null && NKCUIFriend.s_LoadedUIData.IsLoadComplete)
			{
				return NKCUIFriend.s_LoadedUIData.GetInstance<NKCUIFriend>();
			}
			return null;
		}

		// Token: 0x06008073 RID: 32883 RVA: 0x002B4E52 File Offset: 0x002B3052
		public static void CleanupInstance()
		{
			NKCUIFriend.s_LoadedUIData = null;
		}

		// Token: 0x17001510 RID: 5392
		// (get) Token: 0x06008074 RID: 32884 RVA: 0x002B4E5A File Offset: 0x002B305A
		public override string GuideTempletID
		{
			get
			{
				return "ARTICLE_SYSTEM_FRIEND";
			}
		}

		// Token: 0x17001511 RID: 5393
		// (get) Token: 0x06008075 RID: 32885 RVA: 0x002B4E61 File Offset: 0x002B3061
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_FRIEND;
			}
		}

		// Token: 0x17001512 RID: 5394
		// (get) Token: 0x06008076 RID: 32886 RVA: 0x002B4E68 File Offset: 0x002B3068
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x06008077 RID: 32887 RVA: 0x002B4E6B File Offset: 0x002B306B
		public void InitUI()
		{
			this.m_NKCUIFriendLeftMenu.Init();
		}

		// Token: 0x06008078 RID: 32888 RVA: 0x002B4E78 File Offset: 0x002B3078
		public void Open(NKCUIFriendTopMenu.FRIEND_TOP_MENU_TYPE openRegisterTab)
		{
			if (base.IsOpen)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			if (openRegisterTab != NKCUIFriendTopMenu.FRIEND_TOP_MENU_TYPE.FTMT_REGISTER)
			{
				if (openRegisterTab != NKCUIFriendTopMenu.FRIEND_TOP_MENU_TYPE.FTMT_MY_PROFILE)
				{
					this.m_NKCUIFriendLeftMenu.Reset();
				}
				else
				{
					this.m_NKCUIFriendLeftMenu.OnClickMyProfile(true);
				}
			}
			else
			{
				this.m_NKCUIFriendLeftMenu.ForceClickRegisterBtn();
			}
			base.UIOpened(true);
		}

		// Token: 0x06008079 RID: 32889 RVA: 0x002B4ED1 File Offset: 0x002B30D1
		public override void CloseInternal()
		{
			if (!base.gameObject.activeSelf)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_NKCUIFriendLeftMenu.CloseSortMenu();
			NKCUIFriendLeftMenu nkcuifriendLeftMenu = this.m_NKCUIFriendLeftMenu;
			if (nkcuifriendLeftMenu == null)
			{
				return;
			}
			nkcuifriendLeftMenu.Close();
		}

		// Token: 0x0600807A RID: 32890 RVA: 0x002B4F08 File Offset: 0x002B3108
		public NKCUIFriendSlot.FRIEND_SLOT_TYPE GetCurrentSlotType()
		{
			return this.m_NKCUIFriendLeftMenu.GetCurrentSlotType();
		}

		// Token: 0x0600807B RID: 32891 RVA: 0x002B4F15 File Offset: 0x002B3115
		public void SetAddReceivedNew(bool bSet)
		{
			this.m_NKCUIFriendLeftMenu.SetAddReceiveNew(bSet);
		}

		// Token: 0x0600807C RID: 32892 RVA: 0x002B4F23 File Offset: 0x002B3123
		public override void OnBackButton()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
		}

		// Token: 0x0600807D RID: 32893 RVA: 0x002B4F31 File Offset: 0x002B3131
		public void UpdateMyMainCharUI()
		{
			NKCUIFriendLeftMenu nkcuifriendLeftMenu = this.m_NKCUIFriendLeftMenu;
			if (nkcuifriendLeftMenu == null)
			{
				return;
			}
			nkcuifriendLeftMenu.UpdateMainCharUI();
		}

		// Token: 0x0600807E RID: 32894 RVA: 0x002B4F43 File Offset: 0x002B3143
		public void OnRecv(NKMPacket_FRIEND_LIST_ACK cNKMPacket_FRIEND_LIST_ACK)
		{
			this.m_NKCUIFriendLeftMenu.OnRecv(cNKMPacket_FRIEND_LIST_ACK);
		}

		// Token: 0x0600807F RID: 32895 RVA: 0x002B4F51 File Offset: 0x002B3151
		public void OnRecv(NKMPacket_SET_EMBLEM_ACK cNKMPacket_SET_EMBLEM_ACK)
		{
			this.m_NKCUIFriendLeftMenu.OnRecv(cNKMPacket_SET_EMBLEM_ACK);
		}

		// Token: 0x06008080 RID: 32896 RVA: 0x002B4F5F File Offset: 0x002B315F
		public void OnRecv(NKMPacket_FRIEND_RECOMMEND_ACK cNKMPacket_FRIEND_RECOMMEND_ACK)
		{
			this.m_NKCUIFriendLeftMenu.OnRecv(cNKMPacket_FRIEND_RECOMMEND_ACK);
		}

		// Token: 0x06008081 RID: 32897 RVA: 0x002B4F6D File Offset: 0x002B316D
		public void OnRecv(NKMPacket_FRIEND_ACCEPT_NOT cNKMPacket_FRIEND_ACCEPT_NOT)
		{
			this.m_NKCUIFriendLeftMenu.OnRecv(cNKMPacket_FRIEND_ACCEPT_NOT);
		}

		// Token: 0x06008082 RID: 32898 RVA: 0x002B4F7B File Offset: 0x002B317B
		public void OnRecv(NKMPacket_FRIEND_DELETE_NOT cNKMPacket_FRIEND_DEL_NOT)
		{
			this.m_NKCUIFriendLeftMenu.OnRecv(cNKMPacket_FRIEND_DEL_NOT);
		}

		// Token: 0x06008083 RID: 32899 RVA: 0x002B4F89 File Offset: 0x002B3189
		public void OnRecv(NKMPacket_FRIEND_REQUEST_NOT cNKMPacket_FRIEND_ADD_NOT)
		{
			this.m_NKCUIFriendLeftMenu.OnRecv(cNKMPacket_FRIEND_ADD_NOT);
		}

		// Token: 0x06008084 RID: 32900 RVA: 0x002B4F97 File Offset: 0x002B3197
		public void OnRecv(NKMPacket_FRIEND_CANCEL_REQUEST_NOT cNKMPacket_FRIEND_ADD_CANCEL_NOT)
		{
			this.m_NKCUIFriendLeftMenu.OnRecv(cNKMPacket_FRIEND_ADD_CANCEL_NOT);
		}

		// Token: 0x06008085 RID: 32901 RVA: 0x002B4FA5 File Offset: 0x002B31A5
		public void OnRecv(NKMPacket_FRIEND_PROFILE_MODIFY_INTRO_ACK cNKMPacket_FRIEND_PROFILE_MODIFY_INTRO_ACK)
		{
			this.m_NKCUIFriendLeftMenu.OnRecv(cNKMPacket_FRIEND_PROFILE_MODIFY_INTRO_ACK);
		}

		// Token: 0x06008086 RID: 32902 RVA: 0x002B4FB3 File Offset: 0x002B31B3
		public void OnRecv(NKMPacket_FRIEND_PROFILE_MODIFY_DECK_ACK cNKMPacket_FRIEND_PROFILE_MODIFY_DECK_ACK)
		{
			this.m_NKCUIFriendLeftMenu.OnRecv(cNKMPacket_FRIEND_PROFILE_MODIFY_DECK_ACK);
		}

		// Token: 0x06008087 RID: 32903 RVA: 0x002B4FC1 File Offset: 0x002B31C1
		public void OnRecv(NKMPacket_FRIEND_DELETE_ACK cNKMPacket_FRIEND_DEL_ACK)
		{
			this.m_NKCUIFriendLeftMenu.OnRecv(cNKMPacket_FRIEND_DEL_ACK);
		}

		// Token: 0x06008088 RID: 32904 RVA: 0x002B4FCF File Offset: 0x002B31CF
		public void OnRecv(NKMPacket_FRIEND_SEARCH_ACK cNKMPacket_FRIEND_SEARCH_ACK)
		{
			this.m_NKCUIFriendLeftMenu.OnRecv(cNKMPacket_FRIEND_SEARCH_ACK);
		}

		// Token: 0x06008089 RID: 32905 RVA: 0x002B4FDD File Offset: 0x002B31DD
		public void OnRecv(NKMPacket_FRIEND_REQUEST_ACK cNKMPacket_FRIEND_ADD_ACK)
		{
			this.m_NKCUIFriendLeftMenu.OnRecv(cNKMPacket_FRIEND_ADD_ACK);
		}

		// Token: 0x0600808A RID: 32906 RVA: 0x002B4FEB File Offset: 0x002B31EB
		public void OnRecv(NKMPacket_FRIEND_ACCEPT_ACK cNKMPacket_FRIEND_ACCEPT_ACK)
		{
			this.m_NKCUIFriendLeftMenu.OnRecv(cNKMPacket_FRIEND_ACCEPT_ACK);
		}

		// Token: 0x0600808B RID: 32907 RVA: 0x002B4FF9 File Offset: 0x002B31F9
		public void OnRecv(NKMPacket_FRIEND_CANCEL_REQUEST_ACK cNKMPacket_FRIEND_ADD_CANCEL_ACK)
		{
			this.m_NKCUIFriendLeftMenu.OnRecv(cNKMPacket_FRIEND_ADD_CANCEL_ACK);
		}

		// Token: 0x0600808C RID: 32908 RVA: 0x002B5007 File Offset: 0x002B3207
		public void OnRecv(NKMPacket_FRIEND_BLOCK_ACK cNKMPacket_FRIEND_BLOCK_ACK)
		{
			this.m_NKCUIFriendLeftMenu.OnRecv(cNKMPacket_FRIEND_BLOCK_ACK);
		}

		// Token: 0x0600808D RID: 32909 RVA: 0x002B5015 File Offset: 0x002B3215
		public void RefreshNickname()
		{
			this.m_NKCUIFriendLeftMenu.RefreshNickname();
		}

		// Token: 0x0600808E RID: 32910 RVA: 0x002B5022 File Offset: 0x002B3222
		public override void OnGuildDataChanged()
		{
			NKCUIFriendLeftMenu nkcuifriendLeftMenu = this.m_NKCUIFriendLeftMenu;
			if (nkcuifriendLeftMenu == null)
			{
				return;
			}
			nkcuifriendLeftMenu.OnGuildDataChanged();
		}

		// Token: 0x04006CB1 RID: 27825
		public const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_friend";

		// Token: 0x04006CB2 RID: 27826
		public const string UI_ASSET_NAME = "NKM_UI_FRIEND";

		// Token: 0x04006CB3 RID: 27827
		private static NKCUIManager.LoadedUIData s_LoadedUIData;

		// Token: 0x04006CB4 RID: 27828
		public NKCUIFriendLeftMenu m_NKCUIFriendLeftMenu;
	}
}
