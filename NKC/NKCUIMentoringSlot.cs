using System;
using ClientPacket.Common;
using ClientPacket.Community;
using NKC.UI;
using NKC.UI.Guild;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x0200079D RID: 1949
	public class NKCUIMentoringSlot : MonoBehaviour
	{
		// Token: 0x06004C76 RID: 19574 RVA: 0x0016E314 File Offset: 0x0016C514
		public static NKCUIMentoringSlot GetNewInstance(Transform parent)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_FRIEND", "NKM_UI_FRIEND_LIST_SLOT_MENTORING", false, null);
			NKCUIMentoringSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIMentoringSlot>();
			if (component == null)
			{
				Debug.LogError("NKCUIMentoringSlot Prefab null!");
				return null;
			}
			if (parent != null)
			{
				component.transform.SetParent(parent);
				component.m_Instance = nkcassetInstanceData;
				component.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
			}
			component.gameObject.SetActive(false);
			component.m_cbtnConfirm.PointerClick.RemoveAllListeners();
			component.m_cbtnConfirm.PointerClick.AddListener(new UnityAction(component.OnClickConfirm));
			component.m_cbtnCancel.PointerClick.RemoveAllListeners();
			component.m_cbtnCancel.PointerClick.AddListener(new UnityAction(component.OnClickCancel));
			component.m_cbtnMentoring_Add.PointerClick.RemoveAllListeners();
			component.m_cbtnMentoring_Add.PointerClick.AddListener(new UnityAction(component.OnClickAddMentor));
			component.m_cbtnMentoring_Request.PointerClick.RemoveAllListeners();
			component.m_cbtnMentoring_Request.PointerClick.AddListener(new UnityAction(component.OnClickRequestMentee));
			component.m_BGButton.PointerClick.RemoveAllListeners();
			component.m_BGButton.PointerClick.AddListener(new UnityAction(component.OnClickBG));
			return component;
		}

		// Token: 0x06004C77 RID: 19575 RVA: 0x0016E478 File Offset: 0x0016C678
		public void Clear()
		{
			if (this.m_Instance != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_Instance);
			}
		}

		// Token: 0x06004C78 RID: 19576 RVA: 0x0016E490 File Offset: 0x0016C690
		private void OnClickConfirm()
		{
			if (this.m_MentoringTargetUID != 0L)
			{
				string mentorName = NKCScenManager.CurrentUserData().GetMentorName(this.m_MentoringTargetUID);
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_FRIEND_MENTORING_REGISTER_MENTOR_ACCEPT_TITLE, string.Format(NKCUtilString.GET_FRIEND_MENTORING_REGISTER_MENTOR_ACCEPT_DESC_01, mentorName), delegate()
				{
					NKCPacketSender.Send_NKMPacket_MENTORING_ACCEPT_MENTOR_REQ(this.m_MentoringTargetUID);
				}, null, false);
			}
		}

		// Token: 0x06004C79 RID: 19577 RVA: 0x0016E4DC File Offset: 0x0016C6DC
		private void OnClickCancel()
		{
			if (this.m_MentoringTargetUID != 0L)
			{
				string mentorName = NKCScenManager.CurrentUserData().GetMentorName(this.m_MentoringTargetUID);
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_FRIEND_MENTORING_REGISTER_MENTOR_DISACCEPT_TITLE, string.Format(NKCUtilString.GET_FRIEND_MENTORING_REGISTER_MENTOR_DISACCEPT_DESC_01, mentorName), delegate()
				{
					NKCPacketSender.Send_NKMPacket_MENTORING_DISACCEPT_MENTOR_REQ(this.m_MentoringTargetUID);
				}, null, false);
			}
		}

		// Token: 0x06004C7A RID: 19578 RVA: 0x0016E528 File Offset: 0x0016C728
		private void SetGuildData()
		{
			NKMGuildSimpleData guildData = this.m_friendListData.guildData;
			if (this.m_objGuild != null)
			{
				NKCUtil.SetGameobjectActive(this.m_objGuild, guildData != null && guildData.guildUid > 0L);
				if (guildData != null && this.m_objGuild.activeSelf)
				{
					this.m_BadgeUI.SetData(guildData.badgeId);
					NKCUtil.SetLabelText(this.m_lbGuildName, guildData.guildName);
				}
			}
		}

		// Token: 0x06004C7B RID: 19579 RVA: 0x0016E59C File Offset: 0x0016C79C
		public void SetData(FriendListData _NKMUserProfileData)
		{
			this.m_friendListData = _NKMUserProfileData;
			this.SetGuildData();
			this.SetData(_NKMUserProfileData.commonProfile, _NKMUserProfileData.lastLoginDate.Ticks);
		}

		// Token: 0x06004C7C RID: 19580 RVA: 0x0016E5C4 File Offset: 0x0016C7C4
		private void SetData(NKMCommonProfile profile, long lastLoginTime)
		{
			this.m_NKM_UI_FRIEND_LIST_SLOT_INFO_NAME.text = profile.nickname;
			this.m_NKM_UI_FRIEND_LIST_SLOT_INFO_LV.text = string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, profile.level);
			this.m_NKM_UI_FRIEND_LIST_SLOT_INFO_UID.text = NKCUtilString.GetFriendCode(profile.friendCode);
			this.m_NKCUISlot.SetProfiledata(profile, null);
			DateTime lastTime = new DateTime(lastLoginTime);
			this.m_NKM_UI_FRIEND_LIST_SLOT_INFO_TIME_TEXT_2.text = NKCUtilString.GetLastTimeString(lastTime);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_LIST_SLOT_MENTORING_TEXT, true);
			if (this.m_NKM_UI_FRIEND_LIST_SLOT_BUTTON_DELETE != null)
			{
				this.m_NKM_UI_FRIEND_LIST_SLOT_BUTTON_DELETE.PointerClick.RemoveAllListeners();
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_LIST_SLOT_MENTORING_DELETE, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_LIST_SLOT_BUTTON_MENTORING_ADD, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_LIST_SLOT_BUTTON_MENTORING_REQUEST, false);
			NKCUtil.SetGameobjectActive(this.m_cbtnUserInfo, false);
			NKCUtil.SetGameobjectActive(this.m_SUB_BUTTON_layout_gruop, true);
			this.m_MentoringTargetUID = 0L;
			base.gameObject.SetActive(true);
		}

		// Token: 0x06004C7D RID: 19581 RVA: 0x0016E6B8 File Offset: 0x0016C8B8
		public void SetData(NKMCommonProfile profile, long lastLoginTime, bool Invited)
		{
			this.SetData(profile, lastLoginTime);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_LIST_SLOT_MENTORING_DELETE, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_LIST_SLOT_MENTORING_TEXT, false);
			NKCUtil.SetGameobjectActive(this.m_RECOMMEND_BADGE, !Invited);
			NKCUtil.SetGameobjectActive(this.m_INVITE_BADGE, Invited);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_LIST_SLOT_BUTTON_MENTORING_ADD, !Invited);
			NKCUtil.SetGameobjectActive(this.m_SUB_BUTTON_layout_gruop, Invited);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_LIST_SLOT_MENTORING_DELETE, false);
			NKCUtil.SetGameobjectActive(this.m_MENTORING_PROCEEDING, false);
			NKCUtil.SetGameobjectActive(this.m_MENTORING_COMPLETE, false);
			this.m_MentoringTargetUID = profile.userUid;
			this.m_lFriendCode = profile.friendCode;
		}

		// Token: 0x06004C7E RID: 19582 RVA: 0x0016E758 File Offset: 0x0016C958
		public void SetDataForSearch(NKMCommonProfile profile, long lastLoginTime, NKCUIMentoringSlot.callBackFunc callBack = null)
		{
			this.SetData(profile, lastLoginTime);
			NKCUtil.SetGameobjectActive(this.m_RECOMMEND_BADGE, false);
			NKCUtil.SetGameobjectActive(this.m_INVITE_BADGE, false);
			NKCUtil.SetGameobjectActive(this.m_SUB_BUTTON_layout_gruop, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_LIST_SLOT_MENTORING_DELETE, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_LIST_SLOT_MENTORING_TEXT, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_LIST_SLOT_BUTTON_MENTORING_ADD, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_LIST_SLOT_BUTTON_MENTORING_REQUEST, true);
			this.m_MentoringTargetUID = profile.userUid;
			this.m_lFriendCode = profile.friendCode;
			this.m_callBack = callBack;
		}

		// Token: 0x06004C7F RID: 19583 RVA: 0x0016E7E0 File Offset: 0x0016C9E0
		private void OnClickAddMentor()
		{
			if (this.m_MentoringTargetUID != 0L)
			{
				string mentorName = NKCScenManager.CurrentUserData().GetMentorName(this.m_MentoringTargetUID);
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_FRIEND_MENTORING_REGISTER_MENTOR_ACCEPT_TITLE, string.Format(NKCUtilString.GET_FRIEND_MENTORING_REGISTER_MENTOR_ACCEPT_DESC_01, mentorName), delegate()
				{
					NKCPacketSender.Send_NKMPacket_MENTORING_ADD_REQ(MentoringIdentity.Mentor, this.m_MentoringTargetUID);
				}, null, false);
			}
		}

		// Token: 0x06004C80 RID: 19584 RVA: 0x0016E82C File Offset: 0x0016CA2C
		private void OnClickRequestMentee()
		{
			if (this.m_MentoringTargetUID != 0L)
			{
				NKMUserData.strMentoringData mentoringData = NKCScenManager.CurrentUserData().MentoringData;
				if (mentoringData.lstMenteeMatch != null && mentoringData.lstMenteeMatch.Count > 0 && mentoringData.lstMenteeMatch.Count >= NKMMentoringConst.MenteeLimitBelongCount)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_FRIEND_MENTORING_LIMIT_COUNT_DESC, null, "");
					return;
				}
				NKCPacketSender.Send_NKMPacket_MENTORING_ADD_REQ(MentoringIdentity.Mentee, this.m_MentoringTargetUID);
				NKCUIMentoringSlot.callBackFunc callBack = this.m_callBack;
				if (callBack == null)
				{
					return;
				}
				callBack(this.m_MentoringTargetUID);
			}
		}

		// Token: 0x06004C81 RID: 19585 RVA: 0x0016E8AC File Offset: 0x0016CAAC
		public void OnClickBG()
		{
			if (this.m_lFriendCode != 0L)
			{
				NKMPacket_USER_PROFILE_BY_FRIEND_CODE_REQ nkmpacket_USER_PROFILE_BY_FRIEND_CODE_REQ = new NKMPacket_USER_PROFILE_BY_FRIEND_CODE_REQ();
				nkmpacket_USER_PROFILE_BY_FRIEND_CODE_REQ.friendCode = this.m_lFriendCode;
				NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_USER_PROFILE_BY_FRIEND_CODE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
			}
		}

		// Token: 0x04003C2F RID: 15407
		public Text m_NKM_UI_FRIEND_LIST_SLOT_INFO_NAME;

		// Token: 0x04003C30 RID: 15408
		public Text m_NKM_UI_FRIEND_LIST_SLOT_INFO_LV;

		// Token: 0x04003C31 RID: 15409
		public Text m_NKM_UI_FRIEND_LIST_SLOT_INFO_UID;

		// Token: 0x04003C32 RID: 15410
		public Text m_NKM_UI_FRIEND_LIST_SLOT_INFO_TIME_TEXT_2;

		// Token: 0x04003C33 RID: 15411
		public GameObject m_objGuild;

		// Token: 0x04003C34 RID: 15412
		public NKCUIGuildBadge m_BadgeUI;

		// Token: 0x04003C35 RID: 15413
		public Text m_lbGuildName;

		// Token: 0x04003C36 RID: 15414
		public NKCUIComButton m_BGButton;

		// Token: 0x04003C37 RID: 15415
		public NKCUIComButton m_cbtnUserInfo;

		// Token: 0x04003C38 RID: 15416
		public NKCUIComButton m_cbtnDelete;

		// Token: 0x04003C39 RID: 15417
		public NKCUIComButton m_cbtnAdd;

		// Token: 0x04003C3A RID: 15418
		public NKCUIComButton m_cbtnConfirm;

		// Token: 0x04003C3B RID: 15419
		public NKCUIComButton m_cbtnCancel;

		// Token: 0x04003C3C RID: 15420
		public NKCUISlotProfile m_NKCUISlot;

		// Token: 0x04003C3D RID: 15421
		public GameObject m_RECOMMEND_BADGE;

		// Token: 0x04003C3E RID: 15422
		public GameObject m_INVITE_BADGE;

		// Token: 0x04003C3F RID: 15423
		private FriendListData m_friendListData;

		// Token: 0x04003C40 RID: 15424
		[Header("멘토링")]
		public GameObject m_SUB_BUTTON_layout_gruop;

		// Token: 0x04003C41 RID: 15425
		public GameObject m_NKM_UI_FRIEND_LIST_SLOT_BUTTON_MENTORING_ADD;

		// Token: 0x04003C42 RID: 15426
		public NKCUIComButton m_cbtnMentoring_Add;

		// Token: 0x04003C43 RID: 15427
		public GameObject m_NKM_UI_FRIEND_LIST_SLOT_BUTTON_MENTORING_REQUEST;

		// Token: 0x04003C44 RID: 15428
		public NKCUIComButton m_cbtnMentoring_Request;

		// Token: 0x04003C45 RID: 15429
		public GameObject m_NKM_UI_FRIEND_LIST_SLOT_MENTORING_TEXT;

		// Token: 0x04003C46 RID: 15430
		public GameObject m_NKM_UI_FRIEND_LIST_SLOT_MENTORING_DELETE;

		// Token: 0x04003C47 RID: 15431
		public NKCUIComButton m_NKM_UI_FRIEND_LIST_SLOT_BUTTON_DELETE;

		// Token: 0x04003C48 RID: 15432
		public Text m_MENTORING_PROCEEDING;

		// Token: 0x04003C49 RID: 15433
		public Text m_MENTORING_COMPLETE;

		// Token: 0x04003C4A RID: 15434
		private NKCAssetInstanceData m_Instance;

		// Token: 0x04003C4B RID: 15435
		private long m_MentoringTargetUID;

		// Token: 0x04003C4C RID: 15436
		private NKCUIMentoringSlot.callBackFunc m_callBack;

		// Token: 0x04003C4D RID: 15437
		private long m_lFriendCode;

		// Token: 0x0200145E RID: 5214
		// (Invoke) Token: 0x0600A882 RID: 43138
		public delegate void callBackFunc(long uid);
	}
}
