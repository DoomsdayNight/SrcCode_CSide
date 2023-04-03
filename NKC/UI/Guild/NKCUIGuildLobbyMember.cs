using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Guild;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B54 RID: 2900
	public class NKCUIGuildLobbyMember : MonoBehaviour
	{
		// Token: 0x0600843F RID: 33855 RVA: 0x002C92AC File Offset: 0x002C74AC
		public void InitUI()
		{
			this.m_tglMember.OnValueChanged.RemoveAllListeners();
			this.m_tglMember.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickMember));
			this.m_tglJoinWaiting.OnValueChanged.RemoveAllListeners();
			this.m_tglJoinWaiting.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickJoinWaiting));
			this.m_loopScroll.dOnGetObject += this.GetObject;
			this.m_loopScroll.dOnReturnObject += this.ReturnObject;
			this.m_loopScroll.dOnProvideData += this.ProvideData;
			this.m_loopScroll.PrepareCells(0);
			NKCUtil.SetScrollHotKey(this.m_loopScroll, null);
			this.m_btnInvite.PointerClick.RemoveAllListeners();
			this.m_btnInvite.PointerClick.AddListener(new UnityAction(this.OnClickInvite));
			this.m_btnEditMyComment.PointerClick.RemoveAllListeners();
			this.m_btnEditMyComment.PointerClick.AddListener(new UnityAction(this.OnClickEditMyComment));
			if (this.m_ctgDescending != null)
			{
				this.m_ctgDescending.OnValueChanged.RemoveAllListeners();
				this.m_ctgDescending.OnValueChanged.AddListener(new UnityAction<bool>(this.OnCheckAscend));
			}
			this.m_tgSortTypeMenu.OnValueChanged.RemoveAllListeners();
			this.m_tgSortTypeMenu.OnValueChanged.AddListener(new UnityAction<bool>(this.OnSortMenuOpen));
			this.m_MemberSlotType = NKCUIGuildLobbyMember.GUILD_MEMBER_SLOT_TYPE.Normal;
			this.m_tglMember.Select(true, true, true);
		}

		// Token: 0x06008440 RID: 33856 RVA: 0x002C9444 File Offset: 0x002C7644
		private RectTransform GetObject(int index)
		{
			NKCUIGuildMemberSlot nkcuiguildMemberSlot;
			if (this.m_stkMember.Count > 0)
			{
				nkcuiguildMemberSlot = this.m_stkMember.Pop();
			}
			else
			{
				nkcuiguildMemberSlot = NKCUIGuildMemberSlot.GetNewInstance(this.m_trContentParent, new NKCUIGuildMemberSlot.OnSelectedSlot(this.OnSelectedSlot));
			}
			this.m_lstVisibleSlot.Add(nkcuiguildMemberSlot);
			NKCUtil.SetGameobjectActive(nkcuiguildMemberSlot, false);
			if (nkcuiguildMemberSlot == null)
			{
				return null;
			}
			return nkcuiguildMemberSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06008441 RID: 33857 RVA: 0x002C94A8 File Offset: 0x002C76A8
		private void ReturnObject(Transform tr)
		{
			NKCUIGuildMemberSlot component = tr.GetComponent<NKCUIGuildMemberSlot>();
			this.m_lstVisibleSlot.Remove(component);
			this.m_stkMember.Push(component);
			NKCUtil.SetGameobjectActive(component, false);
			tr.SetParent(base.transform);
		}

		// Token: 0x06008442 RID: 33858 RVA: 0x002C94E8 File Offset: 0x002C76E8
		private void ProvideData(Transform tr, int idx)
		{
			NKCUIGuildMemberSlot component = tr.GetComponent<NKCUIGuildMemberSlot>();
			if (component == null)
			{
				NKCUtil.SetGameobjectActive(component, false);
				return;
			}
			NKCUIGuildLobbyMember.GUILD_MEMBER_SLOT_TYPE memberSlotType = this.m_MemberSlotType;
			if (memberSlotType != NKCUIGuildLobbyMember.GUILD_MEMBER_SLOT_TYPE.Normal)
			{
				if (memberSlotType != NKCUIGuildLobbyMember.GUILD_MEMBER_SLOT_TYPE.JoinWaiting)
				{
					return;
				}
				if (this.m_GuildData.joinWaitingList.Count < idx)
				{
					NKCUtil.SetGameobjectActive(component, false);
					return;
				}
				component.SetData(this.m_lstJoinWaiting[idx], NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Member);
				return;
			}
			else
			{
				if (this.m_GuildData.members.Count < idx)
				{
					NKCUtil.SetGameobjectActive(component, false);
					return;
				}
				component.SetData(this.m_lstMember[idx], true);
				return;
			}
		}

		// Token: 0x06008443 RID: 33859 RVA: 0x002C9579 File Offset: 0x002C7779
		public void SetData(NKMGuildData guildData)
		{
			this.m_GuildData = guildData;
			this.RefreshUI(false);
		}

		// Token: 0x06008444 RID: 33860 RVA: 0x002C958C File Offset: 0x002C778C
		public void RefreshUI(bool bResetScroll = false)
		{
			NKMGuildMemberData nkmguildMemberData = this.m_GuildData.members.Find((NKMGuildMemberData x) => x.commonProfile.userUid == NKCScenManager.CurrentUserData().m_UserUID);
			if (nkmguildMemberData != null)
			{
				NKCUtil.SetGameobjectActive(this.m_tglJoinWaiting, nkmguildMemberData.grade != GuildMemberGrade.Member);
				NKCUtil.SetGameobjectActive(this.m_btnInvite, nkmguildMemberData.grade != GuildMemberGrade.Member);
			}
			NKCUIGuildLobbyMember.GUILD_MEMBER_SLOT_TYPE memberSlotType = this.m_MemberSlotType;
			if (memberSlotType != NKCUIGuildLobbyMember.GUILD_MEMBER_SLOT_TYPE.Normal)
			{
				if (memberSlotType == NKCUIGuildLobbyMember.GUILD_MEMBER_SLOT_TYPE.JoinWaiting)
				{
					this.m_lstJoinWaiting = this.m_GuildData.joinWaitingList;
					this.m_loopScroll.TotalCount = this.m_GuildData.joinWaitingList.Count;
					NKCUtil.SetGameobjectActive(this.m_objEmpty, this.m_loopScroll.TotalCount == 0);
				}
			}
			else
			{
				this.m_lstMember = this.m_GuildData.members;
				this.m_lstMember.Sort(new Comparison<NKMGuildMemberData>(this.Compare));
				this.m_loopScroll.TotalCount = this.m_GuildData.members.Count;
				NKCUtil.SetGameobjectActive(this.m_objEmpty, false);
			}
			this.m_loopScroll.RefreshCells(false);
			NKCUtil.SetLabelText(this.m_txtSortType, NKCUnitSortSystem.GetSortName(this.m_currentSortOption));
			NKCUtil.SetLabelText(this.m_txtSelectedSortType, NKCUnitSortSystem.GetSortName(this.m_currentSortOption));
			NKCUtil.SetGameobjectActive(this.m_objSortSelect, NKCUnitSortSystem.GetSortCategoryFromOption(this.m_currentSortOption) != this.DEFATUL_SORT_CATEGORY);
			if (bResetScroll)
			{
				this.m_loopScroll.SetIndexPosition(0);
			}
		}

		// Token: 0x06008445 RID: 33861 RVA: 0x002C970B File Offset: 0x002C790B
		private void OnSelectedSlot(long userUid)
		{
			NKCPacketSender.Send_NKMPacket_USER_PROFILE_INFO_REQ(userUid, NKM_DECK_TYPE.NDT_NORMAL);
		}

		// Token: 0x06008446 RID: 33862 RVA: 0x002C9714 File Offset: 0x002C7914
		private void OnClickInvite()
		{
			NKCPopupGuildInvite.Instance.Open();
		}

		// Token: 0x06008447 RID: 33863 RVA: 0x002C9720 File Offset: 0x002C7920
		private void OnClickMember(bool bValue)
		{
			if (bValue)
			{
				if (this.m_MemberSlotType == NKCUIGuildLobbyMember.GUILD_MEMBER_SLOT_TYPE.Normal)
				{
					return;
				}
				this.m_MemberSlotType = NKCUIGuildLobbyMember.GUILD_MEMBER_SLOT_TYPE.Normal;
				this.RefreshUI(true);
			}
		}

		// Token: 0x06008448 RID: 33864 RVA: 0x002C973C File Offset: 0x002C793C
		private void OnClickJoinWaiting(bool bValue)
		{
			if (bValue)
			{
				if (this.m_MemberSlotType == NKCUIGuildLobbyMember.GUILD_MEMBER_SLOT_TYPE.JoinWaiting)
				{
					return;
				}
				this.m_MemberSlotType = NKCUIGuildLobbyMember.GUILD_MEMBER_SLOT_TYPE.JoinWaiting;
				this.RefreshUI(true);
			}
		}

		// Token: 0x06008449 RID: 33865 RVA: 0x002C975C File Offset: 0x002C795C
		private void OnClickEditMyComment()
		{
			string text = this.m_GuildData.members.Find((NKMGuildMemberData x) => x.commonProfile.userUid == NKCScenManager.CurrentUserData().m_UserUID).greeting;
			if (string.IsNullOrEmpty(text))
			{
				text = NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_INTRODUCE_WRITE_POPUP_GUIDE_DESC;
			}
			NKCPopupInputText.Instance.OpenOKCancelBox(NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_INTRODUCE_WRITE_POPUP_TITLE_DESC, NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_INTRODUCE_WRITE_POPUP_BODY_DESC, text, new NKCPopupInputText.OnButton(this.OnEditMyComment), null, false, 13);
		}

		// Token: 0x0600844A RID: 33866 RVA: 0x002C97D1 File Offset: 0x002C79D1
		private void OnEditMyComment(string greeting)
		{
			NKCPacketSender.Send_NKMPacket_GUILD_UPDATE_MEMBER_GREETING_REQ(NKCGuildManager.MyData.guildUid, greeting);
		}

		// Token: 0x0600844B RID: 33867 RVA: 0x002C97E3 File Offset: 0x002C79E3
		private void OnCheckAscend(bool bValue)
		{
			if (this.m_lstMember.Count <= 1)
			{
				return;
			}
			this.m_currentSortOption = NKCUnitSortSystem.GetInvertedAscendOption(this.m_currentSortOption);
			this.RefreshUI(false);
		}

		// Token: 0x0600844C RID: 33868 RVA: 0x002C980C File Offset: 0x002C7A0C
		private void OnSortMenuOpen(bool bValue)
		{
			this.m_NKCPopupSort.OpenGuildMemberSortMenu(this.m_hsSort, this.m_currentSortOption, new NKCPopupSort.OnSortOption(this.OnSort), bValue);
			this.SetOpenSortingMenu(bValue, true);
		}

		// Token: 0x0600844D RID: 33869 RVA: 0x002C983A File Offset: 0x002C7A3A
		private void OnSort(List<NKCUnitSortSystem.eSortOption> sortOptionList)
		{
			this.m_currentSortOption = sortOptionList[0];
			this.SetOpenSortingMenu(false, true);
			this.RefreshUI(false);
		}

		// Token: 0x0600844E RID: 33870 RVA: 0x002C9858 File Offset: 0x002C7A58
		public void SetOpenSortingMenu(bool bOpen, bool bAnimate = true)
		{
			NKCUtil.SetLabelText(this.m_txtSelectedSortType, NKCUnitSortSystem.GetSortName(this.m_currentSortOption));
			this.m_tgSortTypeMenu.Select(bOpen, true, false);
			this.m_NKCPopupSort.StartRectMove(bOpen, bAnimate);
		}

		// Token: 0x0600844F RID: 33871 RVA: 0x002C988C File Offset: 0x002C7A8C
		private int Compare(NKMGuildMemberData member_a, NKMGuildMemberData member_b)
		{
			NKCUnitSortSystem.eSortOption currentSortOption = this.m_currentSortOption;
			if (currentSortOption == NKCUnitSortSystem.eSortOption.LoginTime_Latly)
			{
				return member_b.lastOnlineTime.CompareTo(member_a.lastOnlineTime);
			}
			if (currentSortOption != NKCUnitSortSystem.eSortOption.LoginTime_Old)
			{
				switch (currentSortOption)
				{
				case NKCUnitSortSystem.eSortOption.Guild_Grade_High:
					if (member_a.grade != member_b.grade)
					{
						return member_a.grade.CompareTo(member_b.grade);
					}
					if (member_a.totalContributionPoint != member_b.totalContributionPoint)
					{
						return member_b.totalContributionPoint.CompareTo(member_a.totalContributionPoint);
					}
					break;
				case NKCUnitSortSystem.eSortOption.Guild_Grade_Low:
					if (member_a.grade != member_b.grade)
					{
						return member_b.grade.CompareTo(member_a.grade);
					}
					if (member_a.totalContributionPoint != member_b.totalContributionPoint)
					{
						return member_a.totalContributionPoint.CompareTo(member_b.totalContributionPoint);
					}
					break;
				case NKCUnitSortSystem.eSortOption.Guild_WeeklyPoint_High:
					if (member_a.weeklyContributionPoint != member_b.weeklyContributionPoint)
					{
						return member_b.weeklyContributionPoint.CompareTo(member_a.weeklyContributionPoint);
					}
					break;
				case NKCUnitSortSystem.eSortOption.Guild_WeeklyPoint_Low:
					if (member_a.weeklyContributionPoint != member_b.weeklyContributionPoint)
					{
						return member_a.weeklyContributionPoint.CompareTo(member_b.weeklyContributionPoint);
					}
					break;
				case NKCUnitSortSystem.eSortOption.Guild_TotalPoint_High:
					if (member_a.totalContributionPoint != member_b.totalContributionPoint)
					{
						return member_b.totalContributionPoint.CompareTo(member_a.totalContributionPoint);
					}
					break;
				case NKCUnitSortSystem.eSortOption.Guild_TotalPoint_Low:
					if (member_a.totalContributionPoint != member_b.totalContributionPoint)
					{
						return member_a.totalContributionPoint.CompareTo(member_b.totalContributionPoint);
					}
					break;
				}
				return member_a.grade.CompareTo(member_b.grade);
			}
			return member_a.lastOnlineTime.CompareTo(member_b.lastOnlineTime);
		}

		// Token: 0x04007064 RID: 28772
		public NKCUIComToggle m_tglMember;

		// Token: 0x04007065 RID: 28773
		public NKCUIComToggle m_tglJoinWaiting;

		// Token: 0x04007066 RID: 28774
		public LoopScrollRect m_loopScroll;

		// Token: 0x04007067 RID: 28775
		public Transform m_trContentParent;

		// Token: 0x04007068 RID: 28776
		public NKCUIComStateButton m_btnInvite;

		// Token: 0x04007069 RID: 28777
		public NKCUIComStateButton m_btnEditMyComment;

		// Token: 0x0400706A RID: 28778
		[Header("내림차순/오름차순 토글")]
		public NKCUIComToggle m_ctgDescending;

		// Token: 0x0400706B RID: 28779
		[Header("정렬 옵션")]
		public NKCUIComToggle m_tgSortTypeMenu;

		// Token: 0x0400706C RID: 28780
		public GameObject m_objSortSelect;

		// Token: 0x0400706D RID: 28781
		public NKCPopupSort m_NKCPopupSort;

		// Token: 0x0400706E RID: 28782
		public Text m_txtSortType;

		// Token: 0x0400706F RID: 28783
		public Text m_txtSelectedSortType;

		// Token: 0x04007070 RID: 28784
		[Header("리스트 비었을 때 표시")]
		public GameObject m_objEmpty;

		// Token: 0x04007071 RID: 28785
		private Stack<NKCUIGuildMemberSlot> m_stkMember = new Stack<NKCUIGuildMemberSlot>();

		// Token: 0x04007072 RID: 28786
		private List<NKCUIGuildMemberSlot> m_lstVisibleSlot = new List<NKCUIGuildMemberSlot>();

		// Token: 0x04007073 RID: 28787
		private List<NKMGuildMemberData> m_lstMember = new List<NKMGuildMemberData>();

		// Token: 0x04007074 RID: 28788
		private List<FriendListData> m_lstJoinWaiting = new List<FriendListData>();

		// Token: 0x04007075 RID: 28789
		private NKMGuildData m_GuildData;

		// Token: 0x04007076 RID: 28790
		private NKCUIGuildLobbyMember.GUILD_MEMBER_SLOT_TYPE m_MemberSlotType;

		// Token: 0x04007077 RID: 28791
		private NKCUnitSortSystem.eSortCategory DEFATUL_SORT_CATEGORY = NKCUnitSortSystem.eSortCategory.GuildGrade;

		// Token: 0x04007078 RID: 28792
		private NKCUnitSortSystem.eSortOption m_currentSortOption = NKCUnitSortSystem.eSortOption.Guild_Grade_High;

		// Token: 0x04007079 RID: 28793
		private HashSet<NKCUnitSortSystem.eSortCategory> m_hsSort = new HashSet<NKCUnitSortSystem.eSortCategory>
		{
			NKCUnitSortSystem.eSortCategory.GuildGrade,
			NKCUnitSortSystem.eSortCategory.GuildWeeklyPoint,
			NKCUnitSortSystem.eSortCategory.GuildTotalPoint,
			NKCUnitSortSystem.eSortCategory.LoginTime
		};

		// Token: 0x020018EC RID: 6380
		public enum GUILD_MEMBER_SLOT_TYPE
		{
			// Token: 0x0400AA2B RID: 43563
			Normal,
			// Token: 0x0400AA2C RID: 43564
			JoinWaiting
		}
	}
}
