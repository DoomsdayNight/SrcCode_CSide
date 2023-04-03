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
	// Token: 0x02000B6B RID: 2923
	public class NKCUIGauntletFriendSlot : MonoBehaviour
	{
		// Token: 0x0600859F RID: 34207 RVA: 0x002D3928 File Offset: 0x002D1B28
		public static NKCUIGauntletFriendSlot GetNewInstance(Transform parent, NKCUIGauntletFriendSlot.OnDragBegin onDragBegin)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_FRIEND_SLOT", false, null);
			NKCUIGauntletFriendSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIGauntletFriendSlot>();
			if (component == null)
			{
				Debug.LogError("NKCUIGauntletFriendSlot Prefab null!");
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

		// Token: 0x060085A0 RID: 34208 RVA: 0x002D39EC File Offset: 0x002D1BEC
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x060085A1 RID: 34209 RVA: 0x002D3A0B File Offset: 0x002D1C0B
		private void OnDragBeginImpl()
		{
			if (this.m_dOnDragBegin != null)
			{
				this.m_dOnDragBegin();
			}
		}

		// Token: 0x060085A2 RID: 34210 RVA: 0x002D3A20 File Offset: 0x002D1C20
		private void OnClick()
		{
			if (this.m_UserUID <= 0L)
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_USER_PROFILE_INFO_REQ(this.m_UserUID, NKM_DECK_TYPE.NDT_PVP);
		}

		// Token: 0x060085A3 RID: 34211 RVA: 0x002D3A39 File Offset: 0x002D1C39
		private void OnClickOffline()
		{
			long userUID = this.m_UserUID;
		}

		// Token: 0x060085A4 RID: 34212 RVA: 0x002D3A45 File Offset: 0x002D1C45
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

		// Token: 0x060085A5 RID: 34213 RVA: 0x002D3A6C File Offset: 0x002D1C6C
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

		// Token: 0x060085A6 RID: 34214 RVA: 0x002D3BF8 File Offset: 0x002D1DF8
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

		// Token: 0x060085A7 RID: 34215 RVA: 0x002D3CD4 File Offset: 0x002D1ED4
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

		// Token: 0x060085A8 RID: 34216 RVA: 0x002D3D49 File Offset: 0x002D1F49
		private void OnClickInviteReq()
		{
			NKCPrivatePVPMgr.SendInvite(this.m_FriendListData);
		}

		// Token: 0x04007225 RID: 29221
		public NKCUISlotProfile m_NKCUISlot;

		// Token: 0x04007226 RID: 29222
		public GameObject m_obj1Min_BG;

		// Token: 0x04007227 RID: 29223
		public GameObject m_obj2Min_BG;

		// Token: 0x04007228 RID: 29224
		public GameObject m_obj3Min_BG;

		// Token: 0x04007229 RID: 29225
		public Text m_lbLevel;

		// Token: 0x0400722A RID: 29226
		public Text m_lbName;

		// Token: 0x0400722B RID: 29227
		public Text m_lbUID;

		// Token: 0x0400722C RID: 29228
		public Text m_lbLastOnlineTime;

		// Token: 0x0400722D RID: 29229
		public NKCUIComStateButton m_csbtnSimpleUserInfoSlot;

		// Token: 0x0400722E RID: 29230
		public GameObject m_objMySlot;

		// Token: 0x0400722F RID: 29231
		public GameObject m_objGuild;

		// Token: 0x04007230 RID: 29232
		public NKCUIGuildBadge m_GuildBadgeUI;

		// Token: 0x04007231 RID: 29233
		public Text m_lbGuildName;

		// Token: 0x04007232 RID: 29234
		[Header("커스텀 매치 초대")]
		public NKCUIComStateButton m_csbtnInviteToCustomMatch;

		// Token: 0x04007233 RID: 29235
		private long m_UserUID;

		// Token: 0x04007234 RID: 29236
		private NKCUIGauntletFriendSlot.OnDragBegin m_dOnDragBegin;

		// Token: 0x04007235 RID: 29237
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x04007236 RID: 29238
		private FriendListData m_FriendListData;

		// Token: 0x04007237 RID: 29239
		private NKMCommonProfile m_CommonProfile;

		// Token: 0x04007238 RID: 29240
		private NKMGuildSimpleData m_GuildSimpleData;

		// Token: 0x0200190E RID: 6414
		// (Invoke) Token: 0x0600B784 RID: 46980
		public delegate void OnDragBegin();
	}
}
