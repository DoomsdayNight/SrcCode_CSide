using System;
using ClientPacket.Community;
using NKC.UI;
using NKC.UI.Friend;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200070E RID: 1806
	public class NKC_SCEN_FRIEND : NKC_SCEN_BASIC
	{
		// Token: 0x06004707 RID: 18183 RVA: 0x00158933 File Offset: 0x00156B33
		public NKC_SCEN_FRIEND()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_FRIEND;
		}

		// Token: 0x06004708 RID: 18184 RVA: 0x00158943 File Offset: 0x00156B43
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			if (!NKCUIManager.IsValid(NKC_SCEN_FRIEND.FriendUIData))
			{
				NKC_SCEN_FRIEND.FriendUIData = NKCUIFriend.OpenNewInstanceAsync();
			}
		}

		// Token: 0x06004709 RID: 18185 RVA: 0x00158964 File Offset: 0x00156B64
		public override void ScenLoadComplete()
		{
			base.ScenLoadComplete();
			if (!(this.m_NKCUIFriend == null))
			{
				return;
			}
			if (NKC_SCEN_FRIEND.FriendUIData != null && NKC_SCEN_FRIEND.FriendUIData.CheckLoadAndGetInstance<NKCUIFriend>(out this.m_NKCUIFriend))
			{
				this.m_NKCUIFriend.InitUI();
				this.SetAddReceivedNew(NKCFriendManager.ReceivedREQList.Count > 0);
				return;
			}
			Debug.LogError("Error - NKC_SCEN_FRIEND.ScenLoadComplete() : UIFriendLoadResourceData is null");
		}

		// Token: 0x0600470A RID: 18186 RVA: 0x001589C8 File Offset: 0x00156BC8
		public override void ScenStart()
		{
			base.ScenStart();
			this.m_NKCUIFriend.Open(this.m_eReservedOpenRegisterTab);
			this.m_eReservedOpenRegisterTab = NKCUIFriendTopMenu.FRIEND_TOP_MENU_TYPE.FTMT_MANAGE;
		}

		// Token: 0x0600470B RID: 18187 RVA: 0x001589E8 File Offset: 0x00156BE8
		public override void ScenEnd()
		{
			base.ScenEnd();
			NKCPopupImageChange.CheckInstanceAndClose();
			if (this.m_NKCUIFriend != null)
			{
				this.m_NKCUIFriend.Close();
				this.m_NKCUIFriend = null;
			}
			NKCUIManager.LoadedUIData friendUIData = NKC_SCEN_FRIEND.FriendUIData;
			if (friendUIData != null)
			{
				friendUIData.CloseInstance();
			}
			NKC_SCEN_FRIEND.FriendUIData = null;
		}

		// Token: 0x0600470C RID: 18188 RVA: 0x00158A36 File Offset: 0x00156C36
		public void OpenImageChangeForUnit(NKCPopupImageChange.OnClickOK _OnClickOK)
		{
			NKCPopupImageChange.Instance.OpenForUnit(_OnClickOK);
		}

		// Token: 0x0600470D RID: 18189 RVA: 0x00158A43 File Offset: 0x00156C43
		public void CloseImageChange()
		{
			NKCPopupImageChange.CheckInstanceAndClose();
		}

		// Token: 0x0600470E RID: 18190 RVA: 0x00158A4A File Offset: 0x00156C4A
		public void SetAddReceivedNew(bool bSet)
		{
			if (this.m_NKCUIFriend != null)
			{
				this.m_NKCUIFriend.SetAddReceivedNew(bSet);
			}
		}

		// Token: 0x0600470F RID: 18191 RVA: 0x00158A66 File Offset: 0x00156C66
		public void SetReservedTab(NKCUIFriendTopMenu.FRIEND_TOP_MENU_TYPE reservedTab)
		{
			this.m_eReservedOpenRegisterTab = reservedTab;
		}

		// Token: 0x06004710 RID: 18192 RVA: 0x00158A6F File Offset: 0x00156C6F
		public NKCUIFriendSlot.FRIEND_SLOT_TYPE GetCurrentSlotType()
		{
			if (this.m_NKCUIFriend != null)
			{
				return this.m_NKCUIFriend.GetCurrentSlotType();
			}
			return NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_FRIEND_LIST;
		}

		// Token: 0x06004711 RID: 18193 RVA: 0x00158A8C File Offset: 0x00156C8C
		public void OnRecv(NKMPacket_FRIEND_LIST_ACK cNKMPacket_FRIEND_LIST_ACK)
		{
			if (this.m_NKCUIFriend != null)
			{
				this.m_NKCUIFriend.OnRecv(cNKMPacket_FRIEND_LIST_ACK);
			}
		}

		// Token: 0x06004712 RID: 18194 RVA: 0x00158AA8 File Offset: 0x00156CA8
		public void OnRecv(NKMPacket_FRIEND_ACCEPT_NOT cNKMPacket_FRIEND_ACCEPT_NOT)
		{
			if (this.m_NKCUIFriend != null)
			{
				this.m_NKCUIFriend.OnRecv(cNKMPacket_FRIEND_ACCEPT_NOT);
			}
		}

		// Token: 0x06004713 RID: 18195 RVA: 0x00158AC4 File Offset: 0x00156CC4
		public void OnRecv(NKMPacket_FRIEND_DELETE_NOT not)
		{
			if (this.m_NKCUIFriend != null)
			{
				this.m_NKCUIFriend.OnRecv(not);
			}
		}

		// Token: 0x06004714 RID: 18196 RVA: 0x00158AE0 File Offset: 0x00156CE0
		public void OnRecv(NKMPacket_FRIEND_REQUEST_NOT not)
		{
			if (this.m_NKCUIFriend != null)
			{
				this.m_NKCUIFriend.OnRecv(not);
			}
		}

		// Token: 0x06004715 RID: 18197 RVA: 0x00158AFC File Offset: 0x00156CFC
		public void OnRecv(NKMPacket_FRIEND_CANCEL_REQUEST_NOT not)
		{
			if (this.m_NKCUIFriend != null)
			{
				this.m_NKCUIFriend.OnRecv(not);
			}
		}

		// Token: 0x06004716 RID: 18198 RVA: 0x00158B18 File Offset: 0x00156D18
		public void OnRecv(NKMPacket_FRIEND_PROFILE_MODIFY_INTRO_ACK cNKMPacket_FRIEND_PROFILE_MODIFY_INTRO_ACK)
		{
			if (this.m_NKCUIFriend != null)
			{
				this.m_NKCUIFriend.OnRecv(cNKMPacket_FRIEND_PROFILE_MODIFY_INTRO_ACK);
			}
		}

		// Token: 0x06004717 RID: 18199 RVA: 0x00158B34 File Offset: 0x00156D34
		public void OnRecv(NKMPacket_FRIEND_PROFILE_MODIFY_DECK_ACK cNKMPacket_FRIEND_PROFILE_MODIFY_DECK_ACK)
		{
			if (this.m_NKCUIFriend != null)
			{
				this.m_NKCUIFriend.OnRecv(cNKMPacket_FRIEND_PROFILE_MODIFY_DECK_ACK);
			}
		}

		// Token: 0x06004718 RID: 18200 RVA: 0x00158B50 File Offset: 0x00156D50
		public void OnRecv(NKMPacket_FRIEND_PROFILE_MODIFY_MAIN_CHAR_ACK cNKMPacket_FRIEND_PROFILE_MODIFY_MAIN_CHAR_ACK)
		{
			NKCPopupImageChange.CheckInstanceAndClose();
			if (this.m_NKCUIFriend != null)
			{
				this.m_NKCUIFriend.UpdateMyMainCharUI();
			}
		}

		// Token: 0x06004719 RID: 18201 RVA: 0x00158B70 File Offset: 0x00156D70
		public void OnRecv(NKMPacket_FRIEND_DELETE_ACK ack)
		{
			if (this.m_NKCUIFriend != null)
			{
				this.m_NKCUIFriend.OnRecv(ack);
			}
		}

		// Token: 0x0600471A RID: 18202 RVA: 0x00158B8C File Offset: 0x00156D8C
		public void OnRecv(NKMPacket_FRIEND_SEARCH_ACK cNKMPacket_FRIEND_SEARCH_ACK)
		{
			if (this.m_NKCUIFriend != null)
			{
				this.m_NKCUIFriend.OnRecv(cNKMPacket_FRIEND_SEARCH_ACK);
			}
		}

		// Token: 0x0600471B RID: 18203 RVA: 0x00158BA8 File Offset: 0x00156DA8
		public void OnRecv(NKMPacket_FRIEND_RECOMMEND_ACK cNKMPacket_FRIEND_RECOMMEND_ACK)
		{
			if (this.m_NKCUIFriend != null)
			{
				this.m_NKCUIFriend.OnRecv(cNKMPacket_FRIEND_RECOMMEND_ACK);
			}
		}

		// Token: 0x0600471C RID: 18204 RVA: 0x00158BC4 File Offset: 0x00156DC4
		public void OnRecv(NKMPacket_FRIEND_REQUEST_ACK ack)
		{
			if (this.m_NKCUIFriend != null)
			{
				this.m_NKCUIFriend.OnRecv(ack);
			}
		}

		// Token: 0x0600471D RID: 18205 RVA: 0x00158BE0 File Offset: 0x00156DE0
		public void OnRecv(NKMPacket_FRIEND_ACCEPT_ACK cNKMPacket_FRIEND_ACCEPT_ACK)
		{
			if (this.m_NKCUIFriend != null)
			{
				this.m_NKCUIFriend.OnRecv(cNKMPacket_FRIEND_ACCEPT_ACK);
			}
		}

		// Token: 0x0600471E RID: 18206 RVA: 0x00158BFC File Offset: 0x00156DFC
		public void OnRecv(NKMPacket_FRIEND_CANCEL_REQUEST_ACK ack)
		{
			if (this.m_NKCUIFriend != null)
			{
				this.m_NKCUIFriend.OnRecv(ack);
			}
		}

		// Token: 0x0600471F RID: 18207 RVA: 0x00158C18 File Offset: 0x00156E18
		public void OnRecv(NKMPacket_FRIEND_BLOCK_ACK cNKMPacket_FRIEND_BLOCK_ACK)
		{
			if (this.m_NKCUIFriend != null)
			{
				this.m_NKCUIFriend.OnRecv(cNKMPacket_FRIEND_BLOCK_ACK);
			}
		}

		// Token: 0x06004720 RID: 18208 RVA: 0x00158C34 File Offset: 0x00156E34
		public void OnRecv(NKMPacket_SET_EMBLEM_ACK cNKMPacket_SET_EMBLEM_ACK)
		{
			if (this.m_NKCUIFriend != null)
			{
				this.m_NKCUIFriend.OnRecv(cNKMPacket_SET_EMBLEM_ACK);
			}
		}

		// Token: 0x06004721 RID: 18209 RVA: 0x00158C50 File Offset: 0x00156E50
		public void OnRecv(NKMPacket_USER_PROFILE_CHANGE_FRAME_ACK sPacket)
		{
			if (this.m_NKCUIFriend != null)
			{
				this.m_NKCUIFriend.UpdateMyMainCharUI();
			}
		}

		// Token: 0x06004722 RID: 18210 RVA: 0x00158C6B File Offset: 0x00156E6B
		public void RefreshNickname()
		{
			if (this.m_NKCUIFriend != null)
			{
				this.m_NKCUIFriend.RefreshNickname();
			}
		}

		// Token: 0x040037C5 RID: 14277
		private static NKCUIManager.LoadedUIData FriendUIData;

		// Token: 0x040037C6 RID: 14278
		private NKCUIFriend m_NKCUIFriend;

		// Token: 0x040037C7 RID: 14279
		private NKCUIFriendTopMenu.FRIEND_TOP_MENU_TYPE m_eReservedOpenRegisterTab;
	}
}
