using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Community;
using ClientPacket.Guild;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Friend
{
	// Token: 0x02000B13 RID: 2835
	public class NKCUIFriendMentoring : MonoBehaviour
	{
		// Token: 0x060080B5 RID: 32949 RVA: 0x002B55FB File Offset: 0x002B37FB
		public MentoringIdentity GetCurMentoringIdentity()
		{
			return this.m_eMentorType;
		}

		// Token: 0x060080B6 RID: 32950 RVA: 0x002B5603 File Offset: 0x002B3803
		public NKMMentoringTemplet GetCurMentoringTemplet()
		{
			return this.m_curMetoringTemplet;
		}

		// Token: 0x060080B7 RID: 32951 RVA: 0x002B560C File Offset: 0x002B380C
		public void Init()
		{
			this.m_eMentorType = MentoringIdentity.Mentee;
			this.m_curMetoringTemplet = null;
			if (this.m_MenteeMissionAllClearSlot != null)
			{
				this.m_MenteeMissionAllClearSlot.Init();
			}
			if (this.m_NKM_UI_FRIEND_MENTORING_INFO_ADD_BUTTON != null)
			{
				this.m_NKM_UI_FRIEND_MENTORING_INFO_ADD_BUTTON.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_FRIEND_MENTORING_INFO_ADD_BUTTON.PointerClick.AddListener(delegate()
				{
					NKCPacketSender.Send_kNKMPacket_MENTORING_RECEIVE_LIST_REQ(MentoringIdentity.Mentee, false);
				});
			}
			if (this.m_NKM_UI_FRIEND_MENTORING_SLOT_ADD_BUTTON != null)
			{
				this.m_NKM_UI_FRIEND_MENTORING_SLOT_ADD_BUTTON.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_FRIEND_MENTORING_SLOT_ADD_BUTTON.PointerClick.AddListener(delegate()
				{
					NKCPacketSender.Send_kNKMPacket_MENTORING_RECEIVE_LIST_REQ(MentoringIdentity.Mentee, false);
				});
			}
			if (this.m_MISSION_LIST_ScrollRect != null)
			{
				this.m_MISSION_LIST_ScrollRect.dOnGetObject += this.GetMissionSlot;
				this.m_MISSION_LIST_ScrollRect.dOnReturnObject += this.ReturnMissionSlot;
				this.m_MISSION_LIST_ScrollRect.dOnProvideData += this.ProvideData;
				this.m_MISSION_LIST_ScrollRect.PrepareCells(0);
				NKCUtil.SetScrollHotKey(this.m_MISSION_LIST_ScrollRect, null);
			}
			if (this.m_NKM_UI_FRIEND_MENTORING_INVITE_REWARD_BUTTON != null)
			{
				this.m_NKM_UI_FRIEND_MENTORING_INVITE_REWARD_BUTTON.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_FRIEND_MENTORING_INVITE_REWARD_BUTTON.PointerClick.AddListener(new UnityAction(this.OnClickMentorReward));
			}
			if (this.m_NKM_UI_FRIEND_LIST_ScrollView != null)
			{
				this.m_NKM_UI_FRIEND_LIST_ScrollView.dOnGetObject += this.GetFriendSlot;
				this.m_NKM_UI_FRIEND_LIST_ScrollView.dOnReturnObject += this.ReturnFriendSlot;
				this.m_NKM_UI_FRIEND_LIST_ScrollView.dOnProvideData += this.ProvideFriendSlotToMenteeData;
				this.m_NKM_UI_FRIEND_LIST_ScrollView.PrepareCells(0);
				NKCUtil.SetScrollHotKey(this.m_NKM_UI_FRIEND_LIST_ScrollView, null);
			}
			if (this.m_NKM_UI_FRIEND_TOP_SEARCH_BUTTON != null)
			{
				this.m_NKM_UI_FRIEND_TOP_SEARCH_BUTTON.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_FRIEND_TOP_SEARCH_BUTTON.PointerClick.AddListener(new UnityAction(this.OnClickMenteeSearch));
			}
			if (this.m_NKM_UI_FRIEND_TOP_SUBMENU_REFRESH != null)
			{
				this.m_NKM_UI_FRIEND_TOP_SUBMENU_REFRESH.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_FRIEND_TOP_SUBMENU_REFRESH.PointerClick.AddListener(new UnityAction(this.OnClickMenteeSearchRefresh));
			}
			if (this.m_MENTORING_INVITE_LIST_ScrollRect != null)
			{
				this.m_MENTORING_INVITE_LIST_ScrollRect.dOnGetObject += this.GetMentoringSlot;
				this.m_MENTORING_INVITE_LIST_ScrollRect.dOnReturnObject += this.ReturnMentoringSlot;
				this.m_MENTORING_INVITE_LIST_ScrollRect.dOnProvideData += this.ProvideMentoringData;
				this.m_MENTORING_INVITE_LIST_ScrollRect.PrepareCells(0);
				NKCUtil.SetScrollHotKey(this.m_MENTORING_INVITE_LIST_ScrollRect, null);
			}
			if (this.m_NKM_UI_FRIEND_MENTORING_REFRESH_BUTTON != null)
			{
				this.m_NKM_UI_FRIEND_MENTORING_REFRESH_BUTTON.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_FRIEND_MENTORING_REFRESH_BUTTON.PointerClick.AddListener(new UnityAction(this.OnClickMatchListRefresh));
			}
			if (this.m_NKM_UI_FRIEND_TOP_SEARCH_INPUT_TEXT != null)
			{
				this.m_NKM_UI_FRIEND_TOP_SEARCH_INPUT_TEXT.onEndEdit.RemoveAllListeners();
				this.m_NKM_UI_FRIEND_TOP_SEARCH_INPUT_TEXT.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditSearch));
			}
		}

		// Token: 0x060080B8 RID: 32952 RVA: 0x002B593D File Offset: 0x002B3B3D
		public void UpdateData(UnityAction callBack = null)
		{
			this.m_curMetoringTemplet = NKCMentoringUtil.GetCurrentTempet();
			if (!this.bCheckHasMentor)
			{
				NKCPacketSender.Send_NKMPacket_MENTORING_DATA_REQ();
				this.m_CallBack = callBack;
				return;
			}
			callBack();
			this.m_CallBack = null;
		}

		// Token: 0x060080B9 RID: 32953 RVA: 0x002B596C File Offset: 0x002B3B6C
		public void Open()
		{
			this.UpdateUI();
			this.lstAlreadyUID.Clear();
			NKCUtil.SetGameobjectActive(base.gameObject, true);
		}

		// Token: 0x060080BA RID: 32954 RVA: 0x002B598B File Offset: 0x002B3B8B
		public void Close()
		{
			this.bCheckHasMentor = false;
			this.m_curMetoringTemplet = null;
			this.m_NKM_UI_FRIEND_MENTORING_INVITE_ILLUST_VIEW.CloseImmediatelyIllust();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060080BB RID: 32955 RVA: 0x002B59B2 File Offset: 0x002B3BB2
		public void UpdateMyMetor()
		{
			this.bCheckHasMentor = true;
			if (this.m_CallBack != null)
			{
				this.m_CallBack();
			}
		}

		// Token: 0x060080BC RID: 32956 RVA: 0x002B59CE File Offset: 0x002B3BCE
		public int GetMenteeCnt()
		{
			return this.m_iMyMenteeCnt;
		}

		// Token: 0x060080BD RID: 32957 RVA: 0x002B59D8 File Offset: 0x002B3BD8
		public void UpdateMyMenteeList()
		{
			this.m_lstSearchMentee.Clear();
			this.m_lstMyMentee = NKCScenManager.CurrentUserData().MentoringData.lstMenteeMatch;
			this.m_iMyMenteeCnt = this.m_lstMyMentee.Count;
			if (this.m_CallBack != null)
			{
				this.m_CallBack();
			}
		}

		// Token: 0x060080BE RID: 32958 RVA: 0x002B5A2C File Offset: 0x002B3C2C
		public void UpdateMyMentoringIdentity(bool bForceUpdate = false)
		{
			this.m_eMentorType = NKCMentoringUtil.GetMentoringIdentity(NKCScenManager.CurrentUserData());
			if (this.m_eMentorType == MentoringIdentity.Mentor)
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				bool flag = false;
				if (nkmuserData != null && nkmuserData.MentoringData.lstMenteeMatch != null && nkmuserData.MentoringData.bMentoringNotify)
				{
					flag = true;
				}
				NKCPacketSender.Send_NKMPacket_MENTORING_MATCH_LIST_REQ(flag || bForceUpdate);
				return;
			}
			this.UpdateMyMetor();
		}

		// Token: 0x060080BF RID: 32959 RVA: 0x002B5A88 File Offset: 0x002B3C88
		public void UpdateMenteeList()
		{
			this.m_lstSearchMentee.Clear();
			List<FriendListData> lstRecommend = NKCScenManager.CurrentUserData().MentoringData.lstRecommend;
			List<FriendListData> lstInvited = NKCScenManager.CurrentUserData().MentoringData.lstInvited;
			if (this.m_eMentorType == MentoringIdentity.Mentor)
			{
				NKMUserProfileData userProfileData = NKCScenManager.CurrentUserData().UserProfileData;
				if (userProfileData != null)
				{
					NKMUnitData nkmunitData = new NKMUnitData();
					nkmunitData.m_UnitID = userProfileData.commonProfile.mainUnitId;
					nkmunitData.m_SkinID = userProfileData.commonProfile.mainUnitSkinId;
					this.m_NKM_UI_FRIEND_MENTORING_INVITE_ILLUST_VIEW.SetCharacterIllust(nkmunitData, false, true, true, 0);
				}
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_MENTORING_INVITE_ILLUST_VIEW, userProfileData != null);
			}
			if (lstRecommend != null && lstRecommend.Count > 0)
			{
				this.m_lstSearchMentee.AddRange(lstRecommend);
				if (lstInvited != null)
				{
					this.m_lstSearchMentee.AddRange(lstInvited);
				}
			}
			if (this.lstAlreadyUID.Count > 0)
			{
				int iCnt;
				Predicate<long> <>9__0;
				int iCnt2;
				for (iCnt = 0; iCnt < this.m_lstSearchMentee.Count; iCnt = iCnt2)
				{
					List<long> list = this.lstAlreadyUID;
					Predicate<long> match;
					if ((match = <>9__0) == null)
					{
						match = (<>9__0 = ((long e) => e == this.m_lstSearchMentee[iCnt].commonProfile.userUid));
					}
					if (list.Find(match) > 0L)
					{
						this.m_lstSearchMentee.RemoveAt(iCnt);
						iCnt2 = iCnt - 1;
						iCnt = iCnt2;
					}
					iCnt2 = iCnt + 1;
				}
			}
			this.m_MENTORING_INVITE_LIST_ScrollRect.TotalCount = this.m_lstSearchMentee.Count;
			this.m_MENTORING_INVITE_LIST_ScrollRect.SetIndexPosition(0);
			this.m_MENTORING_INVITE_LIST_ScrollRect.RefreshCells(true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_MENTORING_INVITE, true);
		}

		// Token: 0x060080C0 RID: 32960 RVA: 0x002B5C24 File Offset: 0x002B3E24
		private void UpdateUI()
		{
			MentoringIdentity eMentorType = this.m_eMentorType;
			if (eMentorType != MentoringIdentity.Mentor)
			{
				if (eMentorType == MentoringIdentity.Mentee)
				{
					this.UpdateMenteeUI();
					return;
				}
			}
			else
			{
				this.UpdateMentorUI();
			}
		}

		// Token: 0x060080C1 RID: 32961 RVA: 0x002B5C4C File Offset: 0x002B3E4C
		private void UpdateMenteeUI()
		{
			if (this.m_curMetoringTemplet == null)
			{
				return;
			}
			this.m_lstMenteeMission.Clear();
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			NKMCommonProfile nkmcommonProfile = null;
			if (NKCScenManager.CurrentUserData().UserProfileData != null)
			{
				nkmcommonProfile = NKCScenManager.CurrentUserData().UserProfileData.commonProfile;
			}
			if (nkmcommonProfile != null)
			{
				nkmcommonProfile.nickname = myUserData.m_UserNickName;
				this.m_MenteeSlot.SetData(MentoringIdentity.Mentee, nkmcommonProfile, (!NKCGuildManager.HasGuild()) ? null : NKCGuildManager.MyGuildData);
				this.m_MenteeSlotProfile.SetProfiledata(nkmcommonProfile, null);
			}
			FriendListData myMentor = NKCScenManager.CurrentUserData().MentoringData.MyMentor;
			if (myMentor != null)
			{
				this.m_MentorSlot.SetData(MentoringIdentity.Mentor, myMentor.commonProfile, myMentor.guildData);
				this.m_MentorSlotProfile.SetProfiledata(myMentor.commonProfile, null);
			}
			else
			{
				this.m_MentorSlot.SetData(MentoringIdentity.Mentor, null, new NKMGuildData());
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_MENTORING_SLOT_ADD_BUTTON, myMentor == null);
			NKCUtil.SetGameobjectActive(this.m_Center_ON, myMentor != null);
			NKCUtil.SetGameobjectActive(this.m_Center_OFF, myMentor == null);
			NKCUtil.SetGameobjectActive(this.m_Slot_Area, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_MENTORING_INVITE, false);
			bool flag = false;
			bool bMenteeGraduate = NKCScenManager.CurrentUserData().MentoringData.bMenteeGraduate;
			int num = 0;
			NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(this.m_curMetoringTemplet.AllClearMissionId);
			if (missionTemplet != null && myMentor != null && !bMenteeGraduate)
			{
				flag = true;
				foreach (int mission_id in missionTemplet.m_MissionCond.value1)
				{
					NKMMissionTemplet missionTemplet2 = NKMMissionManager.GetMissionTemplet(mission_id);
					if (missionTemplet2 != null)
					{
						NKMMissionData missionData = myUserData.m_MissionData.GetMissionData(missionTemplet2);
						if (missionData != null && missionData.isComplete)
						{
							num++;
						}
						this.m_lstMenteeMission.Add(missionTemplet2);
					}
				}
				this.m_lstMenteeMission.Sort(new Comparison<NKMMissionTemplet>(NKMMissionManager.Comparer));
				this.m_MISSION_LIST_ScrollRect.TotalCount = this.m_lstMenteeMission.Count;
				this.m_MISSION_LIST_ScrollRect.SetIndexPosition(0);
				this.m_MISSION_LIST_ScrollRect.RefreshCells(true);
				if (missionTemplet.m_MissionReward != null && missionTemplet.m_MissionReward.Count > 0)
				{
					bool flag2 = this.m_lstMenteeMission.Count <= num;
					NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeRewardTypeData(missionTemplet.m_MissionReward[0].reward_type, missionTemplet.m_MissionReward[0].reward_id, missionTemplet.m_MissionReward[0].reward_value, 0);
					this.m_MenteeMissionAllClearSlot.SetData(data, false, new NKCUISlot.OnClick(this.OnClickRewardAllClear));
					if (flag2)
					{
						NKMMissionData missionData2 = myUserData.m_MissionData.GetMissionData(missionTemplet);
						if (missionData2 != null && missionData2.isComplete)
						{
							this.m_MenteeMissionAllClearSlot.SetDisable(true, "");
							this.m_MenteeMissionAllClearSlot.SetEventGet(true);
							this.m_MenteeMissionAllClearSlot.SetRewardFx(false);
							NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_MENTORING_MISSION_LIST_COUNT_TEXT, false);
							NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_MENTORING_MISSION_LIST_COMPLETE_TEXT, true);
						}
						else
						{
							this.m_MenteeMissionAllClearSlot.SetDisable(false, "");
							this.m_MenteeMissionAllClearSlot.SetEventGet(false);
							this.m_MenteeMissionAllClearSlot.SetRewardFx(true);
							NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_MENTORING_MISSION_LIST_COUNT_TEXT, true);
							NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_MENTORING_MISSION_LIST_COMPLETE_TEXT, false);
						}
					}
					NKCUtil.SetLabelText(this.m_NKM_UI_FRIEND_MENTORING_MISSION_LIST_COUNT_TEXT, string.Format("{0}/{1}", num, this.m_lstMenteeMission.Count));
					if (this.m_MENTORING_MISSION_LIST_PROGRESS_SLIDER != null)
					{
						this.m_MENTORING_MISSION_LIST_PROGRESS_SLIDER.value = ((num == 0) ? 0f : ((float)num / (float)this.m_lstMenteeMission.Count));
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_MenteeMissionAllClearSlot.gameObject, false);
				}
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_MENTORING_MISSION_COMPLETE, bMenteeGraduate);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_MENTORING_MISSION_LIST, flag);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_MENTORING_INFO, !flag);
		}

		// Token: 0x060080C2 RID: 32962 RVA: 0x002B6034 File Offset: 0x002B4234
		private void OnClickRewardAllClear(NKCUISlot.SlotData slotData, bool bLocked)
		{
			if (NKCScenManager.CurrentUserData().MentoringData.MyMentor == null)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_FRIEND_MENTORING_MENTEE_MISSION_NOT_COMPLETE, null, "");
				return;
			}
			NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(this.m_curMetoringTemplet.AllClearMissionId);
			if (missionTemplet == null)
			{
				return;
			}
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(this.m_curMetoringTemplet.MissionTabId);
			if (missionTabTemplet == null)
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null && NKMMissionManager.IsMissionTabExpired(missionTabTemplet, nkmuserData))
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_MISSION_EXPIRED, null, "");
				return;
			}
			NKCPacketSender.Send_NKMPacket_MISSION_COMPLETE_REQ(this.m_curMetoringTemplet.MissionTabId, missionTemplet.m_GroupId, missionTemplet.m_MissionID);
		}

		// Token: 0x060080C3 RID: 32963 RVA: 0x002B60D8 File Offset: 0x002B42D8
		private void UpdateMentorUI()
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_MENTORING_INFO, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_MENTORING_MISSION_LIST, false);
			NKCUtil.SetGameobjectActive(this.m_Slot_Area, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_MENTORING_INVITE, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FRIEND_MENTORING_MISSION_COMPLETE, false);
			int totalCount = 0;
			if (this.m_lstMyMentee != null && this.m_lstMyMentee.Count > 0)
			{
				totalCount = this.m_lstMyMentee.Count;
			}
			this.m_NKM_UI_FRIEND_LIST_ScrollView.PrepareCells(0);
			this.m_NKM_UI_FRIEND_LIST_ScrollView.TotalCount = totalCount;
			this.m_NKM_UI_FRIEND_LIST_ScrollView.SetIndexPosition(0);
			this.m_NKM_UI_FRIEND_LIST_ScrollView.RefreshCells(true);
		}

		// Token: 0x060080C4 RID: 32964 RVA: 0x002B6175 File Offset: 0x002B4375
		public void ResetUI()
		{
			this.UpdateUI();
		}

		// Token: 0x060080C5 RID: 32965 RVA: 0x002B6180 File Offset: 0x002B4380
		public RectTransform GetMissionSlot(int index)
		{
			NKCUIMissionAchieveSlot newInstance = NKCUIMissionAchieveSlot.GetNewInstance(null, "AB_UI_NKM_UI_FRIEND", "NKM_UI_FRIEND_MENTORING_MISSION_SLOT");
			if (newInstance != null)
			{
				return newInstance.GetComponent<RectTransform>();
			}
			return null;
		}

		// Token: 0x060080C6 RID: 32966 RVA: 0x002B61B0 File Offset: 0x002B43B0
		public void ReturnMissionSlot(Transform tr)
		{
			NKCUIMissionAchieveSlot component = tr.GetComponent<NKCUIMissionAchieveSlot>();
			tr.SetParent(base.transform);
			if (component != null)
			{
				component.DestoryInstance();
				return;
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x060080C7 RID: 32967 RVA: 0x002B61EC File Offset: 0x002B43EC
		public void ProvideData(Transform tr, int index)
		{
			NKCUIMissionAchieveSlot component = tr.GetComponent<NKCUIMissionAchieveSlot>();
			if (component != null)
			{
				NKMMissionTemplet cNKMMissionTemplet = this.m_lstMenteeMission[index];
				component.SetData(cNKMMissionTemplet, new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickMove), new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickComplete), null, null);
			}
		}

		// Token: 0x060080C8 RID: 32968 RVA: 0x002B6238 File Offset: 0x002B4438
		public void OnClickMove(NKCUIMissionAchieveSlot cNKCUIMissionAchieveSlot)
		{
			if (cNKCUIMissionAchieveSlot == null)
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			NKMMissionTemplet nkmmissionTemplet = cNKCUIMissionAchieveSlot.GetNKMMissionTemplet();
			if (nkmmissionTemplet == null)
			{
				return;
			}
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(nkmmissionTemplet.m_MissionTabId);
			if (missionTabTemplet == null)
			{
				return;
			}
			if (NKMMissionManager.IsMissionTabExpired(missionTabTemplet, nkmuserData))
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_MISSION_EXPIRED, null, "");
				return;
			}
			NKCContentManager.MoveToShortCut(nkmmissionTemplet.m_ShortCutType, nkmmissionTemplet.m_ShortCut, false);
		}

		// Token: 0x060080C9 RID: 32969 RVA: 0x002B62A8 File Offset: 0x002B44A8
		public void OnClickComplete(NKCUIMissionAchieveSlot cNKCUIMissionAchieveSlot)
		{
			if (cNKCUIMissionAchieveSlot == null)
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			if (NKCScenManager.CurrentUserData().MentoringData.MyMentor == null)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_FRIEND_MENTORING_MENTEE_MISSION_NOT_COMPLETE, null, "");
				return;
			}
			NKMMissionTemplet nkmmissionTemplet = cNKCUIMissionAchieveSlot.GetNKMMissionTemplet();
			if (nkmmissionTemplet == null)
			{
				return;
			}
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(nkmmissionTemplet.m_MissionTabId);
			if (missionTabTemplet == null)
			{
				return;
			}
			if (NKMMissionManager.IsMissionTabExpired(missionTabTemplet, nkmuserData))
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_MISSION_EXPIRED, null, "");
				return;
			}
			NKCPacketSender.Send_NKMPacket_MISSION_COMPLETE_REQ(cNKCUIMissionAchieveSlot.GetNKMMissionTemplet().m_MissionTabId, cNKCUIMissionAchieveSlot.GetNKMMissionTemplet().m_GroupId, cNKCUIMissionAchieveSlot.GetNKMMissionTemplet().m_MissionID);
		}

		// Token: 0x060080CA RID: 32970 RVA: 0x002B6350 File Offset: 0x002B4550
		private void OnClickMentorReward()
		{
			if (this.m_eMentorType == MentoringIdentity.Mentor)
			{
				NKCPacketSender.Send_NKMPacket_MENTORING_INVITE_REWARD_LIST_REQ();
			}
		}

		// Token: 0x060080CB RID: 32971 RVA: 0x002B6360 File Offset: 0x002B4560
		private RectTransform GetFriendSlot(int index)
		{
			if (this.m_slotPool.Count > 0)
			{
				RectTransform rectTransform = this.m_slotPool.Pop();
				NKCUtil.SetGameobjectActive(rectTransform, true);
				rectTransform.SetParent(this.m_NKM_UI_FRIEND_LIST_ScrollView.content);
				return rectTransform;
			}
			NKCUIFriendSlot newInstance = NKCUIFriendSlot.GetNewInstance(this.m_NKM_UI_FRIEND_LIST_ScrollView.content);
			if (newInstance == null)
			{
				return null;
			}
			newInstance.transform.localScale = Vector3.one;
			this.m_slotList.Add(newInstance);
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x060080CC RID: 32972 RVA: 0x002B63DD File Offset: 0x002B45DD
		public void ReturnFriendSlot(Transform tr)
		{
			NKCUtil.SetGameobjectActive(tr, false);
			tr.SetParent(this.m_trMentoringHideParent);
			this.m_slotPool.Push(tr.GetComponent<RectTransform>());
		}

		// Token: 0x060080CD RID: 32973 RVA: 0x002B6404 File Offset: 0x002B4604
		public void ProvideFriendSlotToMenteeData(Transform tr, int index)
		{
			if (this.m_lstMyMentee != null && this.m_lstMyMentee.Count > 0 && this.m_lstMyMentee.Count > index)
			{
				MenteeInfo menteeInfo = this.m_lstMyMentee[index];
				tr.GetComponent<NKCUIFriendSlot>() != null;
				return;
			}
		}

		// Token: 0x060080CE RID: 32974 RVA: 0x002B6450 File Offset: 0x002B4650
		private RectTransform GetMentoringSlot(int index)
		{
			if (this.m_mentoringSlotPool.Count > 0)
			{
				RectTransform rectTransform = this.m_mentoringSlotPool.Pop();
				NKCUtil.SetGameobjectActive(rectTransform, true);
				return rectTransform;
			}
			NKCUIMentoringSlot newInstance = NKCUIMentoringSlot.GetNewInstance(this.m_rtMentoringSlotParent);
			if (newInstance == null)
			{
				return null;
			}
			newInstance.transform.localScale = Vector3.one;
			this.m_lstMentoring.Add(newInstance);
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x060080CF RID: 32975 RVA: 0x002B64B7 File Offset: 0x002B46B7
		public void ReturnMentoringSlot(Transform tr)
		{
			NKCUtil.SetGameobjectActive(tr, false);
			tr.SetParent(base.transform);
			this.m_mentoringSlotPool.Push(tr.GetComponent<RectTransform>());
		}

		// Token: 0x060080D0 RID: 32976 RVA: 0x002B64E0 File Offset: 0x002B46E0
		public void ProvideMentoringData(Transform tr, int index)
		{
			if (this.m_lstSearchMentee != null && this.m_lstSearchMentee.Count > 0 && this.m_lstSearchMentee.Count > index)
			{
				FriendListData friendListData = this.m_lstSearchMentee[index];
				NKCUIMentoringSlot component = tr.GetComponent<NKCUIMentoringSlot>();
				if (component != null)
				{
					component.SetDataForSearch(friendListData.commonProfile, friendListData.lastLoginDate.Ticks, new NKCUIMentoringSlot.callBackFunc(this.AlreadySendMenteeUID));
					return;
				}
			}
		}

		// Token: 0x060080D1 RID: 32977 RVA: 0x002B6552 File Offset: 0x002B4752
		private void AlreadySendMenteeUID(long sendReqUID)
		{
			this.lstAlreadyUID.Add(sendReqUID);
		}

		// Token: 0x060080D2 RID: 32978 RVA: 0x002B6560 File Offset: 0x002B4760
		private void OnEndEditSearch(string input)
		{
			if (NKCInputManager.IsChatSubmitEnter())
			{
				if (!this.m_NKM_UI_FRIEND_TOP_SEARCH_BUTTON.m_bLock)
				{
					this.OnClickMenteeSearch();
				}
				EventSystem.current.SetSelectedGameObject(null);
			}
		}

		// Token: 0x060080D3 RID: 32979 RVA: 0x002B6587 File Offset: 0x002B4787
		private void OnClickMenteeSearch()
		{
			if (this.m_NKM_UI_FRIEND_TOP_SEARCH_INPUT_TEXT != null)
			{
				NKCPacketSender.Send_NKMPacket_MENTORING_SEARCH_LIST_REQ(MentoringIdentity.Mentor, this.m_NKM_UI_FRIEND_TOP_SEARCH_INPUT_TEXT.text);
			}
		}

		// Token: 0x060080D4 RID: 32980 RVA: 0x002B65A8 File Offset: 0x002B47A8
		private void OnClickMenteeSearchRefresh()
		{
			NKCPacketSender.Send_kNKMPacket_MENTORING_RECEIVE_LIST_REQ(MentoringIdentity.Mentor, true);
		}

		// Token: 0x060080D5 RID: 32981 RVA: 0x002B65B1 File Offset: 0x002B47B1
		private void OnClickMatchListRefresh()
		{
			if (this.m_eMentorType == MentoringIdentity.Mentor)
			{
				NKCPacketSender.Send_NKMPacket_MENTORING_MATCH_LIST_REQ(true);
			}
		}

		// Token: 0x04006CBE RID: 27838
		[Header("멘토 등록")]
		public GameObject m_Slot_Area;

		// Token: 0x04006CBF RID: 27839
		public GameObject m_Center_OFF;

		// Token: 0x04006CC0 RID: 27840
		public GameObject m_Center_ON;

		// Token: 0x04006CC1 RID: 27841
		public GameObject m_NKM_UI_FRIEND_MENTORING_INFO;

		// Token: 0x04006CC2 RID: 27842
		public GameObject m_NKM_UI_FRIEND_MENTORING_MISSION_LIST;

		// Token: 0x04006CC3 RID: 27843
		public NKCUIFriendMentoringSlot m_MenteeSlot;

		// Token: 0x04006CC4 RID: 27844
		public NKCUIFriendMentoringSlot m_MentorSlot;

		// Token: 0x04006CC5 RID: 27845
		public NKCUISlotProfile m_MenteeSlotProfile;

		// Token: 0x04006CC6 RID: 27846
		public NKCUISlotProfile m_MentorSlotProfile;

		// Token: 0x04006CC7 RID: 27847
		public NKCUIComStateButton m_NKM_UI_FRIEND_MENTORING_INFO_ADD_BUTTON;

		// Token: 0x04006CC8 RID: 27848
		[Header("멘티 미션")]
		public NKCUISlot m_MenteeMissionAllClearSlot;

		// Token: 0x04006CC9 RID: 27849
		public Text m_NKM_UI_FRIEND_MENTORING_MISSION_LIST_TEXT_02;

		// Token: 0x04006CCA RID: 27850
		public Text m_NKM_UI_FRIEND_MENTORING_MISSION_LIST_COUNT_TEXT;

		// Token: 0x04006CCB RID: 27851
		public Text m_NKM_UI_FRIEND_MENTORING_MISSION_LIST_COMPLETE_TEXT;

		// Token: 0x04006CCC RID: 27852
		public Slider m_MENTORING_MISSION_LIST_PROGRESS_SLIDER;

		// Token: 0x04006CCD RID: 27853
		public LoopScrollRect m_MISSION_LIST_ScrollRect;

		// Token: 0x04006CCE RID: 27854
		public NKCUIComStateButton m_NKM_UI_FRIEND_MENTORING_SLOT_ADD_BUTTON;

		// Token: 0x04006CCF RID: 27855
		[Header("멘토 목록")]
		public GameObject m_NKM_UI_FRIEND_MENTORING_MISSION_COMPLETE;

		// Token: 0x04006CD0 RID: 27856
		[Header("멘티 리스트")]
		public GameObject m_NKM_UI_FRIEND_MENTORING_INVITE;

		// Token: 0x04006CD1 RID: 27857
		public RectTransform m_ILLUST_Root;

		// Token: 0x04006CD2 RID: 27858
		public LoopScrollRect m_MENTORING_INVITE_LIST_ScrollRect;

		// Token: 0x04006CD3 RID: 27859
		public InputField m_NKM_UI_FRIEND_TOP_SEARCH_INPUT_TEXT;

		// Token: 0x04006CD4 RID: 27860
		public NKCUIComButton m_NKM_UI_FRIEND_TOP_SEARCH_BUTTON;

		// Token: 0x04006CD5 RID: 27861
		public NKCUIComButton m_NKM_UI_FRIEND_TOP_SUBMENU_REFRESH;

		// Token: 0x04006CD6 RID: 27862
		[Space]
		public NKCUIComButton m_NKM_UI_FRIEND_MENTORING_REFRESH_BUTTON;

		// Token: 0x04006CD7 RID: 27863
		public NKCUIComButton m_NKM_UI_FRIEND_MENTORING_INVITE_REWARD_BUTTON;

		// Token: 0x04006CD8 RID: 27864
		public LoopScrollRect m_NKM_UI_FRIEND_LIST_ScrollView;

		// Token: 0x04006CD9 RID: 27865
		public NKCUICharacterView m_NKM_UI_FRIEND_MENTORING_INVITE_ILLUST_VIEW;

		// Token: 0x04006CDA RID: 27866
		private MentoringIdentity m_eMentorType;

		// Token: 0x04006CDB RID: 27867
		private NKMMentoringTemplet m_curMetoringTemplet;

		// Token: 0x04006CDC RID: 27868
		private UnityAction m_CallBack;

		// Token: 0x04006CDD RID: 27869
		private bool bCheckHasMentor;

		// Token: 0x04006CDE RID: 27870
		private int m_iMyMenteeCnt;

		// Token: 0x04006CDF RID: 27871
		private List<MenteeInfo> m_lstMyMentee = new List<MenteeInfo>();

		// Token: 0x04006CE0 RID: 27872
		private List<FriendListData> m_lstSearchMentee = new List<FriendListData>();

		// Token: 0x04006CE1 RID: 27873
		private List<NKMMissionTemplet> m_lstMenteeMission = new List<NKMMissionTemplet>();

		// Token: 0x04006CE2 RID: 27874
		public Transform m_rtMentoringSlotParent;

		// Token: 0x04006CE3 RID: 27875
		private List<NKCUIFriendSlot> m_slotList = new List<NKCUIFriendSlot>();

		// Token: 0x04006CE4 RID: 27876
		private Stack<RectTransform> m_slotPool = new Stack<RectTransform>();

		// Token: 0x04006CE5 RID: 27877
		public Transform m_trMentoringHideParent;

		// Token: 0x04006CE6 RID: 27878
		private List<NKCUIMentoringSlot> m_lstMentoring = new List<NKCUIMentoringSlot>();

		// Token: 0x04006CE7 RID: 27879
		private Stack<RectTransform> m_mentoringSlotPool = new Stack<RectTransform>();

		// Token: 0x04006CE8 RID: 27880
		private List<long> lstAlreadyUID = new List<long>();
	}
}
