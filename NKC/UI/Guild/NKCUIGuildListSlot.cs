using System;
using System.Collections.Generic;
using ClientPacket.Guild;
using NKM.Guild;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B50 RID: 2896
	public class NKCUIGuildListSlot : MonoBehaviour
	{
		// Token: 0x060083F3 RID: 33779 RVA: 0x002C7226 File Offset: 0x002C5426
		private void OnDestroy()
		{
			NKCAssetResourceManager.CloseInstance(this.m_instance);
		}

		// Token: 0x060083F4 RID: 33780 RVA: 0x002C7234 File Offset: 0x002C5434
		public static NKCUIGuildListSlot GetNewInstance(Transform parent, NKCUIGuildListSlot.OnSelectedSlot selectedSlot = null)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_CONSORTIUM", "NKM_UI_CONSORTIUM_LIST_SLOT", false, null);
			NKCUIGuildListSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIGuildListSlot>();
			if (component == null)
			{
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				Debug.LogError("NKCUIGuildListSlot Prefab null!");
				return null;
			}
			component.m_instance = nkcassetInstanceData;
			component.SetOnSelectedSlot(selectedSlot);
			component.m_btnSlot.PointerClick.RemoveAllListeners();
			component.m_btnSlot.PointerClick.AddListener(new UnityAction(component.OnClickSlot));
			component.m_btnJoin.PointerClick.RemoveAllListeners();
			component.m_btnJoin.PointerClick.AddListener(new UnityAction(component.OnClickJoinBtn));
			component.m_btnCancel.PointerClick.RemoveAllListeners();
			component.m_btnCancel.PointerClick.AddListener(new UnityAction(component.OnClickCancel));
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x060083F5 RID: 33781 RVA: 0x002C732E File Offset: 0x002C552E
		private void SetOnSelectedSlot(NKCUIGuildListSlot.OnSelectedSlot selectedSlot)
		{
			this.m_dOnSelectedSlot = selectedSlot;
		}

		// Token: 0x060083F6 RID: 33782 RVA: 0x002C7338 File Offset: 0x002C5538
		public void SetData(GuildListData guildData, NKCUIGuildJoin.GuildJoinUIType guildJoinUIType)
		{
			this.m_GuildData = guildData;
			if (guildData == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			this.m_GuildJoinUIType = guildJoinUIType;
			for (int i = 0; i < this.m_lstInviteOnly.Count; i++)
			{
				NKCUtil.SetGameobjectActive(this.m_lstInviteOnly[i], false);
			}
			this.m_Badge.SetData(guildData.badgeId);
			NKCUtil.SetLabelText(this.m_lbGuildLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, guildData.guildLevel));
			NKCUtil.SetLabelText(this.m_lbGuildName, guildData.name);
			NKCUtil.SetLabelText(this.m_lbGuildMasterName, guildData.masterNickname);
			NKCUtil.SetLabelText(this.m_lbGuildMemberCount, string.Format("{0}/{1}", guildData.memberCount, NKMTempletContainer<GuildExpTemplet>.Find(guildData.guildLevel).MaxMemberCount));
			if (this.m_GuildJoinUIType == NKCUIGuildJoin.GuildJoinUIType.Search)
			{
				switch (guildData.guildJoinType)
				{
				case GuildJoinType.DirectJoin:
					NKCUtil.SetLabelText(this.m_lbBtnText, NKCUtilString.GET_STRING_CONSORTIUM_CREATE_JOIN_METHOD_RIGHTOFF_DESC);
					return;
				case GuildJoinType.NeedApproval:
					NKCUtil.SetLabelText(this.m_lbBtnText, NKCUtilString.GET_STRING_CONSORTIUM_CREATE_JOIN_METHOD_CONFIRM_DESC);
					return;
				case GuildJoinType.Closed:
					NKCUtil.SetGameobjectActive(base.gameObject, false);
					return;
				default:
					return;
				}
			}
			else
			{
				if (this.m_GuildJoinUIType == NKCUIGuildJoin.GuildJoinUIType.Requested)
				{
					NKCUtil.SetLabelText(this.m_lbBtnText, NKCUtilString.GET_STRING_FRIEND_REQ_CANCEL);
					return;
				}
				if (this.m_GuildJoinUIType == NKCUIGuildJoin.GuildJoinUIType.Invited)
				{
					NKCUtil.SetLabelText(this.m_lbBtnText, NKCUtilString.GET_STRING_CONSORTIUM_CREATE_JOIN_METHOD_RIGHTOFF_DESC);
					NKCUtil.SetGameobjectActive(this.m_btnCancel, true);
				}
				return;
			}
		}

		// Token: 0x060083F7 RID: 33783 RVA: 0x002C74A4 File Offset: 0x002C56A4
		public void OnClickSlot()
		{
			NKCUIGuildListSlot.OnSelectedSlot dOnSelectedSlot = this.m_dOnSelectedSlot;
			if (dOnSelectedSlot == null)
			{
				return;
			}
			dOnSelectedSlot(this.m_GuildData);
		}

		// Token: 0x060083F8 RID: 33784 RVA: 0x002C74BC File Offset: 0x002C56BC
		private void OnClickJoinBtn()
		{
			switch (this.m_GuildJoinUIType)
			{
			case NKCUIGuildJoin.GuildJoinUIType.Search:
			{
				string okbuttonStr = string.Empty;
				GuildJoinType guildJoinType = this.m_GuildData.guildJoinType;
				if (guildJoinType != GuildJoinType.DirectJoin)
				{
					if (guildJoinType == GuildJoinType.NeedApproval)
					{
						okbuttonStr = NKCUtilString.GET_CONSORTIUM_JOIN_CONFIRM_JOIN_POPUP_APPROVE_BTN_DESC;
					}
				}
				else
				{
					okbuttonStr = NKCUtilString.GET_CONSORTIUM_JOIN_RIGHTOFF_JOIN_POPUP_APPROVE_BTN_DESC;
				}
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_CONSORTIUM_JOIN_CONFIRM_JOIN_POPUP_TITLE_DESC, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_JOIN_RIGHTOFF_JOIN_POPUP_BODY_DESC, this.m_GuildData.name), delegate()
				{
					NKCGuildManager.Send_GUILD_JOIN_REQ(this.m_GuildData.guildUid, this.m_GuildData.name, this.m_GuildData.guildJoinType);
				}, null, okbuttonStr, "", false);
				return;
			}
			case NKCUIGuildJoin.GuildJoinUIType.Requested:
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_CONSORTIUM_JOIN_CONFIRM_JOIN_CANCEL_POPUP_TITLE_DESC, string.Format(NKCUtilString.GET_CONSORTIUM_JOIN_CONFIRM_JOIN_CANCEL_POPUP_BODY_DESC, this.m_GuildData.name), delegate()
				{
					NKCPacketSender.Send_NKMPacket_GUILD_CANCEL_JOIN_REQ(this.m_GuildData.guildUid);
				}, null, false);
				return;
			case NKCUIGuildJoin.GuildJoinUIType.Invited:
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_CONSORTIUM_JOIN_INVITE_JOIN_AGREE_POPUP_TITLE_DESC, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_JOIN_RIGHTOFF_JOIN_POPUP_BODY_DESC, this.m_GuildData.name), delegate()
				{
					NKCPacketSender.Send_NKMPacket_GUILD_ACCEPT_INVITE_REQ(this.m_GuildData.guildUid, true);
				}, null, false);
				return;
			default:
				return;
			}
		}

		// Token: 0x060083F9 RID: 33785 RVA: 0x002C759D File Offset: 0x002C579D
		private void OnClickCancel()
		{
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_CONSORTIUM_INVITE_JOIN_REJECT_POPUP_TITLE_DESC, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_INVITE_JOIN_REJECT_POPUP_BODY_DESC, this.m_GuildData.name), delegate()
			{
				NKCPacketSender.Send_NKMPacket_GUILD_ACCEPT_INVITE_REQ(this.m_GuildData.guildUid, false);
			}, null, false);
		}

		// Token: 0x0400700E RID: 28686
		public NKCUIComStateButton m_btnSlot;

		// Token: 0x0400700F RID: 28687
		public NKCUIGuildBadge m_Badge;

		// Token: 0x04007010 RID: 28688
		public Text m_lbGuildLevel;

		// Token: 0x04007011 RID: 28689
		public Text m_lbGuildName;

		// Token: 0x04007012 RID: 28690
		public Text m_lbGuildMasterName;

		// Token: 0x04007013 RID: 28691
		public Text m_lbGuildMemberCount;

		// Token: 0x04007014 RID: 28692
		public Text m_lbGuildDesc;

		// Token: 0x04007015 RID: 28693
		public NKCUIComStateButton m_btnJoin;

		// Token: 0x04007016 RID: 28694
		public Text m_lbBtnText;

		// Token: 0x04007017 RID: 28695
		public NKCUIComStateButton m_btnCancel;

		// Token: 0x04007018 RID: 28696
		public List<GameObject> m_lstInviteOnly = new List<GameObject>();

		// Token: 0x04007019 RID: 28697
		private GuildListData m_GuildData;

		// Token: 0x0400701A RID: 28698
		private NKCUIGuildListSlot.OnSelectedSlot m_dOnSelectedSlot;

		// Token: 0x0400701B RID: 28699
		private NKCAssetInstanceData m_instance;

		// Token: 0x0400701C RID: 28700
		private NKCUIGuildJoin.GuildJoinUIType m_GuildJoinUIType;

		// Token: 0x020018E6 RID: 6374
		// (Invoke) Token: 0x0600B719 RID: 46873
		public delegate void OnSelectedSlot(GuildListData guildData);
	}
}
