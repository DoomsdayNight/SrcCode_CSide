using System;
using ClientPacket.Common;
using NKC.UI.Guild;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B83 RID: 2947
	public class NKCUIGauntletPrivateRoomFriendSlot : MonoBehaviour
	{
		// Token: 0x060087EF RID: 34799 RVA: 0x002DFC6C File Offset: 0x002DDE6C
		public static NKCUIGauntletPrivateRoomFriendSlot GetNewInstance(Transform parent, NKCUIGauntletPrivateRoomFriendSlot.OnDragBegin onDragBegin)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_PRIVATE_ROOM_FRIEND_SLOT", false, null);
			NKCUIGauntletPrivateRoomFriendSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIGauntletPrivateRoomFriendSlot>();
			if (component == null)
			{
				Debug.LogError("NKCUIGauntletPrivateRoomFriendSlot Prefab null!");
				return null;
			}
			component.m_InstanceData = nkcassetInstanceData;
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.m_dOnDragBegin = onDragBegin;
			component.transform.localPosition = new Vector3(component.transform.localPosition.x, component.transform.localPosition.y, 0f);
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x060087F0 RID: 34800 RVA: 0x002DFD30 File Offset: 0x002DDF30
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x060087F1 RID: 34801 RVA: 0x002DFD4F File Offset: 0x002DDF4F
		private void OnDragBeginImpl()
		{
			if (this.m_dOnDragBegin != null)
			{
				this.m_dOnDragBegin();
			}
		}

		// Token: 0x060087F2 RID: 34802 RVA: 0x002DFD64 File Offset: 0x002DDF64
		private void OnClick()
		{
			if (this.m_UserUID <= 0L)
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_USER_PROFILE_INFO_REQ(this.m_UserUID, NKM_DECK_TYPE.NDT_PVP);
		}

		// Token: 0x060087F3 RID: 34803 RVA: 0x002DFD7D File Offset: 0x002DDF7D
		private void OnClickOffline()
		{
			long userUID = this.m_UserUID;
		}

		// Token: 0x060087F4 RID: 34804 RVA: 0x002DFD89 File Offset: 0x002DDF89
		public void SetUI(NKMUserProfileData userProfileData)
		{
			if (userProfileData == null)
			{
				return;
			}
			this.m_CommonProfile = userProfileData.commonProfile;
			this.m_GuildSimpleData = userProfileData.guildData;
			this.SetUIData();
		}

		// Token: 0x060087F5 RID: 34805 RVA: 0x002DFDB0 File Offset: 0x002DDFB0
		public void SetUI(FriendListData cFriendListData, bool showTimeAndButtons)
		{
			this.m_FriendListData = cFriendListData;
			if (this.m_FriendListData == null)
			{
				return;
			}
			this.m_CommonProfile = this.m_FriendListData.commonProfile;
			this.m_GuildSimpleData = this.m_FriendListData.guildData;
			this.SetUIData();
			if (showTimeAndButtons)
			{
				TimeSpan timeSpan = DateTime.Now - this.m_FriendListData.lastLoginDate;
				NKCUtil.SetGameobjectActive(this.m_obj1Min_BG, timeSpan.TotalMinutes <= 1.0);
				NKCUtil.SetGameobjectActive(this.m_obj2Min_BG, 1.0 < timeSpan.TotalMinutes && timeSpan.TotalMinutes <= 2.0);
				NKCUtil.SetGameobjectActive(this.m_obj3Min_BG, 2.0 < timeSpan.TotalMinutes && timeSpan.TotalMinutes <= 3.0);
				NKCUtil.SetLabelText(this.m_lbLastOnlineTime, NKCUtilString.GetLastTimeString(this.m_FriendListData.lastLoginDate));
				this.m_csbtnSimpleUserInfoSlot.PointerDown.RemoveAllListeners();
				this.m_csbtnSimpleUserInfoSlot.PointerDown.AddListener(delegate(PointerEventData eventData)
				{
					this.OnDragBeginImpl();
				});
				this.m_csbtnInviteToCustomMatch.PointerClick.RemoveAllListeners();
				this.m_csbtnInviteToCustomMatch.PointerClick.AddListener(new UnityAction(this.OnClickInviteReq));
				this.m_csbtnSimpleUserInfoSlot.PointerClick.RemoveAllListeners();
				this.m_csbtnSimpleUserInfoSlot.PointerClick.AddListener(new UnityAction(this.OnClick));
			}
		}

		// Token: 0x060087F6 RID: 34806 RVA: 0x002DFF3C File Offset: 0x002DE13C
		private void SetUIData()
		{
			this.m_UserUID = this.m_CommonProfile.userUid;
			this.m_NKCUISlot.SetProfiledata(this.m_CommonProfile, null);
			this.m_lbName.text = this.m_CommonProfile.nickname;
			this.m_lbUID.text = NKCUtilString.GetFriendCode(this.m_CommonProfile.friendCode, true);
			this.m_lbLevel.text = string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, this.m_CommonProfile.level);
			bool flag = false;
			if (this.m_UserUID == NKCScenManager.CurrentUserData().m_UserUID)
			{
				flag = true;
			}
			NKCUtil.SetGameobjectActive(this.m_objMySlot, flag);
			if (flag)
			{
				this.m_lbName.color = NKCUtil.GetColor("#FFDF5D");
			}
			else
			{
				this.m_lbName.color = Color.white;
			}
			this.SetGuildData();
		}

		// Token: 0x060087F7 RID: 34807 RVA: 0x002E0018 File Offset: 0x002DE218
		private void SetGuildData()
		{
			if (this.m_objGuild != null)
			{
				GameObject objGuild = this.m_objGuild;
				NKMGuildSimpleData guildSimpleData = this.m_GuildSimpleData;
				NKCUtil.SetGameobjectActive(objGuild, guildSimpleData != null && guildSimpleData.guildUid > 0L);
				if (this.m_objGuild.activeSelf)
				{
					this.m_GuildBadgeUI.SetData(this.m_GuildSimpleData.badgeId);
					NKCUtil.SetLabelText(this.m_lbGuildName, this.m_GuildSimpleData.guildName);
				}
			}
		}

		// Token: 0x060087F8 RID: 34808 RVA: 0x002E008D File Offset: 0x002DE28D
		private void OnClickInviteReq()
		{
			NKCPrivatePVPRoomMgr.Send_NKMPacket_PVP_ROOM_INVITE_REQ(this.m_FriendListData);
		}

		// Token: 0x0400743D RID: 29757
		public NKCUISlotProfile m_NKCUISlot;

		// Token: 0x0400743E RID: 29758
		public GameObject m_obj1Min_BG;

		// Token: 0x0400743F RID: 29759
		public GameObject m_obj2Min_BG;

		// Token: 0x04007440 RID: 29760
		public GameObject m_obj3Min_BG;

		// Token: 0x04007441 RID: 29761
		public Text m_lbLevel;

		// Token: 0x04007442 RID: 29762
		public Text m_lbName;

		// Token: 0x04007443 RID: 29763
		public Text m_lbUID;

		// Token: 0x04007444 RID: 29764
		public Text m_lbLastOnlineTime;

		// Token: 0x04007445 RID: 29765
		public NKCUIComStateButton m_csbtnSimpleUserInfoSlot;

		// Token: 0x04007446 RID: 29766
		public GameObject m_objMySlot;

		// Token: 0x04007447 RID: 29767
		public GameObject m_objGuild;

		// Token: 0x04007448 RID: 29768
		public NKCUIGuildBadge m_GuildBadgeUI;

		// Token: 0x04007449 RID: 29769
		public Text m_lbGuildName;

		// Token: 0x0400744A RID: 29770
		[Header("커스텀 매치 초대")]
		public NKCUIComStateButton m_csbtnInviteToCustomMatch;

		// Token: 0x0400744B RID: 29771
		private long m_UserUID;

		// Token: 0x0400744C RID: 29772
		private NKCUIGauntletPrivateRoomFriendSlot.OnDragBegin m_dOnDragBegin;

		// Token: 0x0400744D RID: 29773
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x0400744E RID: 29774
		private FriendListData m_FriendListData;

		// Token: 0x0400744F RID: 29775
		private NKMCommonProfile m_CommonProfile;

		// Token: 0x04007450 RID: 29776
		private NKMGuildSimpleData m_GuildSimpleData;

		// Token: 0x02001928 RID: 6440
		// (Invoke) Token: 0x0600B7C9 RID: 47049
		public delegate void OnDragBegin();
	}
}
