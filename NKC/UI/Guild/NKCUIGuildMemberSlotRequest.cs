using System;
using ClientPacket.Common;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B5C RID: 2908
	public class NKCUIGuildMemberSlotRequest : MonoBehaviour
	{
		// Token: 0x06008495 RID: 33941 RVA: 0x002CB960 File Offset: 0x002C9B60
		public void InitUI()
		{
			this.m_slot.Init();
			this.m_btnInvite.PointerClick.RemoveAllListeners();
			this.m_btnInvite.PointerClick.AddListener(new UnityAction(this.OnClickInvite));
			this.m_btnAccept.PointerClick.RemoveAllListeners();
			this.m_btnAccept.PointerClick.AddListener(new UnityAction(this.OnClickAccept));
			this.m_btnDenied.PointerClick.RemoveAllListeners();
			this.m_btnDenied.PointerClick.AddListener(new UnityAction(this.OnClickDenied));
		}

		// Token: 0x06008496 RID: 33942 RVA: 0x002CB9FC File Offset: 0x002C9BFC
		public void SetData(FriendListData userData, NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE lobbyUIType)
		{
			this.m_UserUid = userData.commonProfile.userUid;
			this.m_UserName = userData.commonProfile.nickname;
			this.m_slot.SetUnitData(userData.commonProfile.mainUnitId, 1, userData.commonProfile.mainUnitSkinId, false, false, false, null);
			NKCUtil.SetLabelText(this.m_lbLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, userData.commonProfile.level));
			NKCUtil.SetLabelText(this.m_lbName, userData.commonProfile.nickname);
			NKCUtil.SetLabelText(this.m_lbFriendCode, string.Format("#{0}", userData.commonProfile.friendCode));
			NKCUtil.SetLabelText(this.m_lbLastLoginTime, NKCUtilString.GetLastTimeString(userData.lastLoginDate));
			NKCUtil.SetGameobjectActive(this.m_btnInvite, lobbyUIType == NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Invite);
			NKCUtil.SetGameobjectActive(this.m_btnAccept, lobbyUIType == NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Member);
			NKCUtil.SetGameobjectActive(this.m_btnDenied, lobbyUIType == NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Member);
			if (lobbyUIType == NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Invite)
			{
				NKCUtil.SetLabelText(this.m_lbInviteBtn, NKCUtilString.GET_STRING_CONSORTIUM_INVITE);
				if (NKCGuildManager.MyGuildData != null && NKCGuildManager.MyGuildData.inviteList.Find((FriendListData x) => x.commonProfile.userUid == userData.commonProfile.userUid) != null)
				{
					this.m_btnInvite.Lock(false);
					return;
				}
				this.m_btnInvite.UnLock(false);
			}
		}

		// Token: 0x06008497 RID: 33943 RVA: 0x002CBB7C File Offset: 0x002C9D7C
		private void OnClickInvite()
		{
			if (NKCGuildManager.MyGuildData.inviteList.Find((FriendListData x) => x.commonProfile.userUid == this.m_UserUid) != null)
			{
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_GUILD_ALREADY_INVITED, null, "");
				return;
			}
			if (NKCGuildManager.MyGuildData.inviteList.Count == NKMCommonConst.Guild.MaxInviteCount)
			{
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_GUILD_MAX_INVITE_COUNT, null, "");
				return;
			}
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_CONSORTIUM_POPUP_INVITE_TITLE, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_INVITE_SEND_POPUP_BODY_DESC, this.m_UserName), delegate()
			{
				NKCPacketSender.Send_NKMPacket_GUILD_INVITE_REQ(NKCGuildManager.MyData.guildUid, this.m_UserUid);
			}, null, NKCUtilString.GET_STRING_CONSORTIUM_INVITE, "", false);
		}

		// Token: 0x06008498 RID: 33944 RVA: 0x002CBC15 File Offset: 0x002C9E15
		private void OnClickAccept()
		{
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_CONFIRM_JOIN_CONFIRM_POPUP_TITLE_DESC, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_CONFIRM_JOIN_CONFIRM_POPUP_BODY_DESC, this.m_UserName), delegate()
			{
				NKCPacketSender.Send_NKMPacket_GUILD_ACCEPT_JOIN_REQ(NKCGuildManager.MyData.guildUid, this.m_UserUid, true);
			}, null, NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_CONFIRM_JOIN_CONFIRM_POPUP_TITLE_DESC, "", false);
		}

		// Token: 0x06008499 RID: 33945 RVA: 0x002CBC49 File Offset: 0x002C9E49
		private void OnClickDenied()
		{
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_CONFIRM_JOIN_REFUSE_POPUP_TITLE_DESC, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_CONFIRM_JOIN_REFUSE_POPUP_BODY_DESC, this.m_UserName), delegate()
			{
				NKCPacketSender.Send_NKMPacket_GUILD_ACCEPT_JOIN_REQ(NKCGuildManager.MyData.guildUid, this.m_UserUid, false);
			}, null, NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_CONFIRM_JOIN_REFUSE_POPUP_TITLE_DESC, "", true);
		}

		// Token: 0x040070D3 RID: 28883
		public NKCUISlot m_slot;

		// Token: 0x040070D4 RID: 28884
		public Text m_lbLevel;

		// Token: 0x040070D5 RID: 28885
		public Text m_lbName;

		// Token: 0x040070D6 RID: 28886
		public Text m_lbFriendCode;

		// Token: 0x040070D7 RID: 28887
		public Text m_lbLastLoginTime;

		// Token: 0x040070D8 RID: 28888
		public NKCUIComStateButton m_btnInvite;

		// Token: 0x040070D9 RID: 28889
		public Text m_lbInviteBtn;

		// Token: 0x040070DA RID: 28890
		public NKCUIComStateButton m_btnAccept;

		// Token: 0x040070DB RID: 28891
		public NKCUIComStateButton m_btnDenied;

		// Token: 0x040070DC RID: 28892
		private long m_UserUid;

		// Token: 0x040070DD RID: 28893
		private string m_UserName;
	}
}
