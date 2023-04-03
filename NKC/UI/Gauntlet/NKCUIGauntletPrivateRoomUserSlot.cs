using System;
using ClientPacket.Common;
using ClientPacket.Pvp;
using NKC.UI.Guild;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B84 RID: 2948
	public class NKCUIGauntletPrivateRoomUserSlot : MonoBehaviour
	{
		// Token: 0x060087FB RID: 34811 RVA: 0x002E00AC File Offset: 0x002DE2AC
		public static NKCUIGauntletPrivateRoomUserSlot GetNewInstance(Transform parent, NKCUIGauntletPrivateRoomUserSlot.OnDragBegin onDragBegin)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_PRIVATE_ROOM_USER_SLOT", false, null);
			NKCUIGauntletPrivateRoomUserSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIGauntletPrivateRoomUserSlot>();
			if (component == null)
			{
				Debug.LogError("NKCUIGauntletPrivateRoomUserSlot Prefab null!");
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

		// Token: 0x060087FC RID: 34812 RVA: 0x002E0170 File Offset: 0x002DE370
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x060087FD RID: 34813 RVA: 0x002E018F File Offset: 0x002DE38F
		private void OnDragBeginImpl()
		{
			if (this.m_dOnDragBegin != null)
			{
				this.m_dOnDragBegin();
			}
		}

		// Token: 0x060087FE RID: 34814 RVA: 0x002E01A4 File Offset: 0x002DE3A4
		private void OnClick()
		{
			if (this.m_UserUID <= 0L)
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_USER_PROFILE_INFO_REQ(this.m_UserUID, NKM_DECK_TYPE.NDT_PVP);
		}

		// Token: 0x060087FF RID: 34815 RVA: 0x002E01C0 File Offset: 0x002DE3C0
		public void Init()
		{
			NKCUtil.SetGameobjectActive(this.m_objEmptySlot, true);
			NKCUtil.SetGameobjectActive(this.m_objUserSlot, false);
			NKCUIComStateButton csbtnInvite = this.m_csbtnInvite;
			if (csbtnInvite != null)
			{
				csbtnInvite.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton csbtnInvite2 = this.m_csbtnInvite;
			if (csbtnInvite2 != null)
			{
				csbtnInvite2.PointerClick.AddListener(new UnityAction(this.OnClickInvite));
			}
			NKCUIComStateButton csbtnChangeRole = this.m_csbtnChangeRole;
			if (csbtnChangeRole != null)
			{
				csbtnChangeRole.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton csbtnChangeRole2 = this.m_csbtnChangeRole;
			if (csbtnChangeRole2 == null)
			{
				return;
			}
			csbtnChangeRole2.PointerClick.AddListener(new UnityAction(this.OnClickChangeRole));
		}

		// Token: 0x06008800 RID: 34816 RVA: 0x002E0254 File Offset: 0x002DE454
		public void OnClickInvite()
		{
			NKCPrivatePVPRoomMgr.ShowInvitePopup();
		}

		// Token: 0x06008801 RID: 34817 RVA: 0x002E025B File Offset: 0x002DE45B
		public void OnClickChangeRole()
		{
		}

		// Token: 0x06008802 RID: 34818 RVA: 0x002E0260 File Offset: 0x002DE460
		public void SetEmptyUI()
		{
			this.m_csbtnSimpleUserInfoSlot.PointerClick.RemoveAllListeners();
			NKCUtil.SetGameobjectActive(this.m_objEmptySlot, true);
			NKCUtil.SetGameobjectActive(this.m_objUserSlot, false);
			if (NKCPrivatePVPRoomMgr.IsHost(NKCPrivatePVPRoomMgr.GetMyPvpGameLobbyUserState()))
			{
				NKCUtil.SetGameobjectActive(this.m_csbtnInvite, true);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_csbtnInvite, false);
		}

		// Token: 0x06008803 RID: 34819 RVA: 0x002E02BA File Offset: 0x002DE4BA
		public void SetUI(NKMPvpGameLobbyUserState userState)
		{
			if (userState == null)
			{
				return;
			}
			this.SetUI(userState.profileData, userState.isHost);
		}

		// Token: 0x06008804 RID: 34820 RVA: 0x002E02D4 File Offset: 0x002DE4D4
		public void SetUI(NKMUserProfileData userProfileData, bool bHost)
		{
			if (userProfileData == null)
			{
				return;
			}
			this.m_CommonProfile = userProfileData.commonProfile;
			this.m_GuildSimpleData = userProfileData.guildData;
			this.m_csbtnSimpleUserInfoSlot.PointerClick.RemoveAllListeners();
			this.m_csbtnSimpleUserInfoSlot.PointerClick.AddListener(new UnityAction(this.OnClick));
			this.SetUIData(bHost);
		}

		// Token: 0x06008805 RID: 34821 RVA: 0x002E0330 File Offset: 0x002DE530
		private void SetUIData(bool bHost)
		{
			NKCUtil.SetGameobjectActive(this.m_objEmptySlot, false);
			NKCUtil.SetGameobjectActive(this.m_objUserSlot, true);
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

		// Token: 0x06008806 RID: 34822 RVA: 0x002E0424 File Offset: 0x002DE624
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

		// Token: 0x04007451 RID: 29777
		[Header("빈 슬롯")]
		public GameObject m_objEmptySlot;

		// Token: 0x04007452 RID: 29778
		public NKCUIComStateButton m_csbtnInvite;

		// Token: 0x04007453 RID: 29779
		public NKCUIComStateButton m_csbtnChangeRole;

		// Token: 0x04007454 RID: 29780
		[Header("유저 슬롯")]
		public GameObject m_objUserSlot;

		// Token: 0x04007455 RID: 29781
		public NKCUISlotProfile m_NKCUISlot;

		// Token: 0x04007456 RID: 29782
		public Text m_lbLevel;

		// Token: 0x04007457 RID: 29783
		public Text m_lbName;

		// Token: 0x04007458 RID: 29784
		public Text m_lbUID;

		// Token: 0x04007459 RID: 29785
		public NKCUIComStateButton m_csbtnSimpleUserInfoSlot;

		// Token: 0x0400745A RID: 29786
		public GameObject m_objMySlot;

		// Token: 0x0400745B RID: 29787
		public GameObject m_objGuild;

		// Token: 0x0400745C RID: 29788
		public NKCUIGuildBadge m_GuildBadgeUI;

		// Token: 0x0400745D RID: 29789
		public Text m_lbGuildName;

		// Token: 0x0400745E RID: 29790
		private long m_UserUID;

		// Token: 0x0400745F RID: 29791
		private NKCUIGauntletPrivateRoomUserSlot.OnDragBegin m_dOnDragBegin;

		// Token: 0x04007460 RID: 29792
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x04007461 RID: 29793
		private NKMCommonProfile m_CommonProfile;

		// Token: 0x04007462 RID: 29794
		private NKMGuildSimpleData m_GuildSimpleData;

		// Token: 0x02001929 RID: 6441
		// (Invoke) Token: 0x0600B7CD RID: 47053
		public delegate void OnDragBegin();
	}
}
