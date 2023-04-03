using System;
using ClientPacket.Common;
using ClientPacket.Community;
using NKC.UI.Guild;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Friend
{
	// Token: 0x02000B16 RID: 2838
	public class NKCUIFriendSlot : MonoBehaviour
	{
		// Token: 0x060080FC RID: 33020 RVA: 0x002B763C File Offset: 0x002B583C
		public static NKCUIFriendSlot GetNewInstance(Transform parent)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_FRIEND", "NKM_UI_FRIEND_LIST_SLOT", false, null);
			NKCUIFriendSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIFriendSlot>();
			if (component == null)
			{
				Debug.LogError("NKCUIFriendSlot Prefab null!");
				return null;
			}
			if (parent != null)
			{
				component.transform.SetParent(parent);
				component.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
			}
			component.m_Instance = nkcassetInstanceData;
			component.gameObject.SetActive(false);
			component.m_BGButton.PointerClick.RemoveAllListeners();
			component.m_BGButton.PointerClick.AddListener(new UnityAction(component.OnClickSlotBGButton));
			component.m_cbtnUserInfo.PointerClick.RemoveAllListeners();
			component.m_cbtnUserInfo.PointerClick.AddListener(new UnityAction(component.OnClickSlotBGButton));
			component.m_cbtnDelete.PointerClick.RemoveAllListeners();
			component.m_cbtnDelete.PointerClick.AddListener(new UnityAction(component.OnClickDelete));
			component.m_cbtnAdd.PointerClick.RemoveAllListeners();
			component.m_cbtnAdd.PointerClick.AddListener(new UnityAction(component.OnClickAdd));
			component.m_cbtnConfirm.PointerClick.RemoveAllListeners();
			component.m_cbtnConfirm.PointerClick.AddListener(new UnityAction(component.OnClickConfirm));
			component.m_cbtnCancel.PointerClick.RemoveAllListeners();
			component.m_cbtnCancel.PointerClick.AddListener(new UnityAction(component.OnClickCancel));
			component.m_cbtnDomitory.PointerClick.RemoveAllListeners();
			component.m_cbtnDomitory.PointerClick.AddListener(new UnityAction(component.OnClickDomitory));
			return component;
		}

		// Token: 0x060080FD RID: 33021 RVA: 0x002B77F8 File Offset: 0x002B59F8
		private void OnClickDelete()
		{
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_FRIEND_DELETE_REQ, new NKCPopupOKCancel.OnButton(this.OnClickDelete_), null, false);
		}

		// Token: 0x060080FE RID: 33022 RVA: 0x002B7817 File Offset: 0x002B5A17
		private void OnClickAdd()
		{
			this.OnClickFriendREQ();
		}

		// Token: 0x060080FF RID: 33023 RVA: 0x002B781F File Offset: 0x002B5A1F
		private void OnClickConfirm()
		{
			this.OnClickAcceptFriend();
		}

		// Token: 0x06008100 RID: 33024 RVA: 0x002B7827 File Offset: 0x002B5A27
		private void OnClickCancel()
		{
			if (this.m_FRIEND_SLOT_TYPE == NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_BLOCK_LIST)
			{
				this.OnClickCancelBlockFriend();
				return;
			}
			if (this.m_FRIEND_SLOT_TYPE == NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_RECEIVE_REQ)
			{
				this.OnClickRejectFriend();
				return;
			}
			if (this.m_FRIEND_SLOT_TYPE == NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_SENT_REQ)
			{
				this.OnClickCancelAddFriend();
			}
		}

		// Token: 0x06008101 RID: 33025 RVA: 0x002B7858 File Offset: 0x002B5A58
		public FriendListData GetFriendListData()
		{
			return this.m_friendListData;
		}

		// Token: 0x06008102 RID: 33026 RVA: 0x002B7860 File Offset: 0x002B5A60
		public void SetActive(bool bSet)
		{
			NKCUtil.SetGameobjectActive(base.gameObject, bSet);
		}

		// Token: 0x06008103 RID: 33027 RVA: 0x002B786E File Offset: 0x002B5A6E
		public bool IsActive()
		{
			return base.gameObject.activeSelf;
		}

		// Token: 0x06008104 RID: 33028 RVA: 0x002B787B File Offset: 0x002B5A7B
		public void Clear()
		{
			if (this.m_Instance != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_Instance);
			}
		}

		// Token: 0x06008105 RID: 33029 RVA: 0x002B7890 File Offset: 0x002B5A90
		private void SetGuildData()
		{
			NKMGuildSimpleData guildData = this.m_friendListData.guildData;
			if (this.m_objGuild != null)
			{
				NKCUtil.SetGameobjectActive(this.m_objGuild, guildData != null && guildData.guildUid > 0L);
				if (this.m_objGuild.activeSelf && guildData != null)
				{
					this.m_BadgeUI.SetData(guildData.badgeId);
					NKCUtil.SetLabelText(this.m_lbGuildName, guildData.guildName);
				}
			}
		}

		// Token: 0x06008106 RID: 33030 RVA: 0x002B7904 File Offset: 0x002B5B04
		public NKCUIFriendSlot.FRIEND_SLOT_TYPE Get_FRIEND_SLOT_TYPE()
		{
			return this.m_FRIEND_SLOT_TYPE;
		}

		// Token: 0x06008107 RID: 33031 RVA: 0x002B790C File Offset: 0x002B5B0C
		public void SetData(NKCUIFriendSlot.FRIEND_SLOT_TYPE slotType, FriendListData _NKMUserProfileData)
		{
			this.m_friendListData = _NKMUserProfileData;
			this.SetGuildData();
			this.SetData(slotType, _NKMUserProfileData.commonProfile, _NKMUserProfileData.lastLoginDate.Ticks);
		}

		// Token: 0x06008108 RID: 33032 RVA: 0x002B7934 File Offset: 0x002B5B34
		private void SetData(NKCUIFriendSlot.FRIEND_SLOT_TYPE slotType, NKMCommonProfile profile, long lastLoginTime)
		{
			this.m_FRIEND_SLOT_TYPE = slotType;
			this.m_NKM_UI_FRIEND_LIST_SLOT_INFO_NAME.text = profile.nickname;
			this.m_NKM_UI_FRIEND_LIST_SLOT_INFO_LV.text = string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, profile.level);
			this.m_NKM_UI_FRIEND_LIST_SLOT_INFO_UID.text = NKCUtilString.GetFriendCode(profile.friendCode);
			this.m_NKCUISlot.SetProfiledata(profile, null);
			NKCUtil.SetGameobjectActive(this.m_RECOMMEND_BADGE, slotType == NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_FRIEND_SEARCH_RECOMMEND);
			DateTime lastTime = new DateTime(lastLoginTime);
			this.m_NKM_UI_FRIEND_LIST_SLOT_INFO_TIME_TEXT_2.text = NKCUtilString.GetLastTimeString(lastTime);
			NKCUtil.SetGameobjectActive(this.m_cbtnDelete, slotType == NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_FRIEND_LIST);
			NKCUtil.SetGameobjectActive(this.m_cbtnCancel, slotType == NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_BLOCK_LIST || slotType == NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_RECEIVE_REQ || slotType == NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_SENT_REQ);
			NKCUtil.SetGameobjectActive(this.m_cbtnAdd, slotType == NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_FRIEND_SEARCH || slotType == NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_FRIEND_SEARCH_RECOMMEND);
			NKCUtil.SetGameobjectActive(this.m_cbtnConfirm, slotType == NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_RECEIVE_REQ);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_LIST_SLOT_MENTORING_TEXT, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_LIST_SLOT_MENTORING_DELETE, false);
			NKCUtil.SetGameobjectActive(this.m_cbtnUserInfo, true);
			NKCUtil.SetGameobjectActive(this.m_SUB_BUTTON_layout_gruop, true);
			this.m_lFriendCoide = profile.friendCode;
			base.gameObject.SetActive(true);
		}

		// Token: 0x06008109 RID: 33033 RVA: 0x002B7A5C File Offset: 0x002B5C5C
		private void OnClickDelete_()
		{
			NKMPacket_FRIEND_DELETE_REQ nkmpacket_FRIEND_DELETE_REQ = new NKMPacket_FRIEND_DELETE_REQ();
			nkmpacket_FRIEND_DELETE_REQ.friendCode = this.GetFriendListData().commonProfile.friendCode;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_FRIEND_DELETE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600810A RID: 33034 RVA: 0x002B7A98 File Offset: 0x002B5C98
		private void OnClickSlotBGButton()
		{
			if (this.m_lFriendCoide == 0L)
			{
				return;
			}
			NKMPacket_USER_PROFILE_BY_FRIEND_CODE_REQ nkmpacket_USER_PROFILE_BY_FRIEND_CODE_REQ = new NKMPacket_USER_PROFILE_BY_FRIEND_CODE_REQ();
			nkmpacket_USER_PROFILE_BY_FRIEND_CODE_REQ.friendCode = this.GetFriendListData().commonProfile.friendCode;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_USER_PROFILE_BY_FRIEND_CODE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600810B RID: 33035 RVA: 0x002B7AE0 File Offset: 0x002B5CE0
		private void OnClickCancelBlockFriend()
		{
			NKMPacket_FRIEND_BLOCK_REQ nkmpacket_FRIEND_BLOCK_REQ = new NKMPacket_FRIEND_BLOCK_REQ();
			nkmpacket_FRIEND_BLOCK_REQ.friendCode = this.GetFriendListData().commonProfile.friendCode;
			nkmpacket_FRIEND_BLOCK_REQ.isCancel = true;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_FRIEND_BLOCK_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600810C RID: 33036 RVA: 0x002B7B24 File Offset: 0x002B5D24
		private void OnClickAcceptFriend()
		{
			NKMPacket_FRIEND_ACCEPT_REQ nkmpacket_FRIEND_ACCEPT_REQ = new NKMPacket_FRIEND_ACCEPT_REQ();
			nkmpacket_FRIEND_ACCEPT_REQ.friendCode = this.GetFriendListData().commonProfile.friendCode;
			nkmpacket_FRIEND_ACCEPT_REQ.isAllow = true;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_FRIEND_ACCEPT_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600810D RID: 33037 RVA: 0x002B7B68 File Offset: 0x002B5D68
		private void OnClickRejectFriend()
		{
			NKMPacket_FRIEND_ACCEPT_REQ nkmpacket_FRIEND_ACCEPT_REQ = new NKMPacket_FRIEND_ACCEPT_REQ();
			nkmpacket_FRIEND_ACCEPT_REQ.friendCode = this.GetFriendListData().commonProfile.friendCode;
			nkmpacket_FRIEND_ACCEPT_REQ.isAllow = false;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_FRIEND_ACCEPT_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600810E RID: 33038 RVA: 0x002B7BAC File Offset: 0x002B5DAC
		private void OnClickCancelAddFriend()
		{
			NKMPacket_FRIEND_CANCEL_REQUEST_REQ nkmpacket_FRIEND_CANCEL_REQUEST_REQ = new NKMPacket_FRIEND_CANCEL_REQUEST_REQ();
			nkmpacket_FRIEND_CANCEL_REQUEST_REQ.friendCode = this.GetFriendListData().commonProfile.friendCode;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_FRIEND_CANCEL_REQUEST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600810F RID: 33039 RVA: 0x002B7BE8 File Offset: 0x002B5DE8
		private void OnClickFriendREQ()
		{
			NKMPacket_FRIEND_REQUEST_REQ nkmpacket_FRIEND_REQUEST_REQ = new NKMPacket_FRIEND_REQUEST_REQ();
			nkmpacket_FRIEND_REQUEST_REQ.friendCode = this.GetFriendListData().commonProfile.friendCode;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_FRIEND_REQUEST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06008110 RID: 33040 RVA: 0x002B7C24 File Offset: 0x002B5E24
		private void OnClickDomitory()
		{
			if (this.m_friendListData == null)
			{
				return;
			}
			if (this.m_friendListData.hasOffice)
			{
				NKCPacketSender.Send_NKMPacket_OFFICE_STATE_REQ(this.m_friendListData.commonProfile.userUid);
				return;
			}
			NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_OFFICE_FRIEND_CANNOT_VISIT, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
		}

		// Token: 0x04006D11 RID: 27921
		public Text m_NKM_UI_FRIEND_LIST_SLOT_INFO_NAME;

		// Token: 0x04006D12 RID: 27922
		public Text m_NKM_UI_FRIEND_LIST_SLOT_INFO_LV;

		// Token: 0x04006D13 RID: 27923
		public Text m_NKM_UI_FRIEND_LIST_SLOT_INFO_UID;

		// Token: 0x04006D14 RID: 27924
		public Text m_NKM_UI_FRIEND_LIST_SLOT_INFO_TIME_TEXT_2;

		// Token: 0x04006D15 RID: 27925
		public GameObject m_objGuild;

		// Token: 0x04006D16 RID: 27926
		public NKCUIGuildBadge m_BadgeUI;

		// Token: 0x04006D17 RID: 27927
		public Text m_lbGuildName;

		// Token: 0x04006D18 RID: 27928
		public NKCUIComButton m_BGButton;

		// Token: 0x04006D19 RID: 27929
		public NKCUIComButton m_cbtnUserInfo;

		// Token: 0x04006D1A RID: 27930
		public NKCUIComButton m_cbtnDelete;

		// Token: 0x04006D1B RID: 27931
		public NKCUIComButton m_cbtnAdd;

		// Token: 0x04006D1C RID: 27932
		public NKCUIComButton m_cbtnConfirm;

		// Token: 0x04006D1D RID: 27933
		public NKCUIComButton m_cbtnCancel;

		// Token: 0x04006D1E RID: 27934
		public NKCUIComButton m_cbtnDomitory;

		// Token: 0x04006D1F RID: 27935
		public NKCUISlotProfile m_NKCUISlot;

		// Token: 0x04006D20 RID: 27936
		public GameObject m_RECOMMEND_BADGE;

		// Token: 0x04006D21 RID: 27937
		private NKCUIFriendSlot.FRIEND_SLOT_TYPE m_FRIEND_SLOT_TYPE = NKCUIFriendSlot.FRIEND_SLOT_TYPE.FST_FRIEND_LIST;

		// Token: 0x04006D22 RID: 27938
		private FriendListData m_friendListData;

		// Token: 0x04006D23 RID: 27939
		public GameObject m_SUB_BUTTON_layout_gruop;

		// Token: 0x04006D24 RID: 27940
		[Header("멘토링")]
		public GameObject m_NKM_UI_FRIEND_LIST_SLOT_MENTORING_TEXT;

		// Token: 0x04006D25 RID: 27941
		public GameObject m_NKM_UI_FRIEND_LIST_SLOT_MENTORING_DELETE;

		// Token: 0x04006D26 RID: 27942
		public NKCUIComButton m_NKM_UI_FRIEND_LIST_SLOT_BUTTON_DELETE;

		// Token: 0x04006D27 RID: 27943
		public Text m_MENTORING_PROCEEDING;

		// Token: 0x04006D28 RID: 27944
		public Text m_MENTORING_COMPLETE;

		// Token: 0x04006D29 RID: 27945
		private NKCAssetInstanceData m_Instance;

		// Token: 0x04006D2A RID: 27946
		private long m_lFriendCoide;

		// Token: 0x020018AD RID: 6317
		public enum FRIEND_SLOT_TYPE
		{
			// Token: 0x0400A981 RID: 43393
			FST_NONE,
			// Token: 0x0400A982 RID: 43394
			FST_FRIEND_LIST,
			// Token: 0x0400A983 RID: 43395
			FST_BLOCK_LIST,
			// Token: 0x0400A984 RID: 43396
			FST_FRIEND_SEARCH,
			// Token: 0x0400A985 RID: 43397
			FST_FRIEND_SEARCH_RECOMMEND,
			// Token: 0x0400A986 RID: 43398
			FST_RECEIVE_REQ,
			// Token: 0x0400A987 RID: 43399
			FST_SENT_REQ,
			// Token: 0x0400A988 RID: 43400
			FST_GAUNTLET_LIST,
			// Token: 0x0400A989 RID: 43401
			FST_GUILD_LIST,
			// Token: 0x0400A98A RID: 43402
			FST_GAUNTLET_CUSTOM_LIST,
			// Token: 0x0400A98B RID: 43403
			FST_OFFICE
		}
	}
}
