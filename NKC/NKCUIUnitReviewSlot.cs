using System;
using ClientPacket.Community;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009F7 RID: 2551
	public class NKCUIUnitReviewSlot : MonoBehaviour
	{
		// Token: 0x06006E95 RID: 28309 RVA: 0x00245DA8 File Offset: 0x00243FA8
		private void InitUI()
		{
			this.m_btnDelete.PointerClick.RemoveAllListeners();
			this.m_btnDelete.PointerClick.AddListener(new UnityAction(this.OnClickDelete));
			this.m_tglUserLike.OnValueChanged.RemoveAllListeners();
			this.m_tglUserLike.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickVote));
			this.bInitComplete = true;
		}

		// Token: 0x06006E96 RID: 28310 RVA: 0x00245E14 File Offset: 0x00244014
		public void SetData(NKMUnitReviewCommentData commentData, int unitID, bool bBest)
		{
			if (!this.bInitComplete)
			{
				this.InitUI();
			}
			if (commentData == null || string.IsNullOrEmpty(commentData.content))
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			this.m_nUnitID = unitID;
			this.m_CommentUID = commentData.commentUID;
			this.m_UserUid = commentData.userUID;
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			DateTime dateTime = NKMTime.UTCtoLocal(new DateTime(commentData.regDate), 0);
			this.m_bIsMyReview = (commentData.userUID == nkmuserData.m_UserUID);
			this.m_bIsBannedUser = NKCUnitReviewManager.IsBannedUser(this.m_UserUid);
			NKCUtil.SetLabelText(this.m_lbUserLevel, NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, new object[]
			{
				commentData.level
			});
			NKCUtil.SetLabelText(this.m_lbUserName, commentData.nickName ?? "");
			NKCUtil.SetGameobjectActive(this.m_objUserBest, bBest);
			NKCUtil.SetGameobjectActive(this.m_objMyReviewTag, this.m_bIsMyReview);
			NKCUtil.SetLabelTextColor(this.m_lbUserLevel, this.m_bIsMyReview ? this.COLOR_MY_REVIEW_TEXT : this.COLOR_NORMAL_REVIEW_TEXT);
			NKCUtil.SetLabelTextColor(this.m_lbUserName, this.m_bIsMyReview ? this.COLOR_MY_REVIEW_TEXT : this.COLOR_NORMAL_REVIEW_TEXT);
			NKCUtil.SetLabelText(this.m_lbUserDate, string.Format("{0}.{1}.{2}", dateTime.Year, dateTime.Month, dateTime.Day));
			this.m_tglUserLike.Select(commentData.isVoted && !this.m_bIsMyReview, true, true);
			this.m_tglUserLike.enabled = !this.m_bIsMyReview;
			NKCUtil.SetLabelText(this.m_lbUserLikeCountOn, (commentData.votedCount > 9999) ? string.Format("{0}+", commentData.votedCount) : commentData.votedCount.ToString());
			NKCUtil.SetLabelText(this.m_lbUserLikeCountOff, (commentData.votedCount > 9999) ? string.Format("{0}+", commentData.votedCount) : commentData.votedCount.ToString());
			NKCUtil.SetLabelText(this.m_lbDelete, this.GetDeleteDesc());
			NKCUtil.SetLabelText(this.m_lbDesc, this.m_bIsBannedUser ? NKCUtilString.GET_STRING_REVIEW_BANNED_CONTENT : commentData.content);
		}

		// Token: 0x06006E97 RID: 28311 RVA: 0x0024606F File Offset: 0x0024426F
		private string GetDeleteDesc()
		{
			if (this.m_bIsMyReview)
			{
				return NKCUtilString.GET_STRING_POPUP_UNIT_REVIEW_DELETE;
			}
			if (!this.m_bIsBannedUser)
			{
				return NKCUtilString.GET_STRING_REVIEW_BAN;
			}
			return NKCUtilString.GET_STRING_REVIEW_UNBAN;
		}

		// Token: 0x06006E98 RID: 28312 RVA: 0x00246094 File Offset: 0x00244294
		public void ChangeVotedCount(int changedVotedCount, bool bVote)
		{
			this.m_tglUserLike.Select(bVote, true, true);
			NKCUtil.SetLabelText(this.m_lbUserLikeCountOn, (changedVotedCount > 9999) ? string.Format("{0}+", changedVotedCount) : changedVotedCount.ToString());
			NKCUtil.SetLabelText(this.m_lbUserLikeCountOff, (changedVotedCount > 9999) ? string.Format("{0}+", changedVotedCount) : changedVotedCount.ToString());
		}

		// Token: 0x06006E99 RID: 28313 RVA: 0x00246108 File Offset: 0x00244308
		public long GetCommentUID()
		{
			return this.m_CommentUID;
		}

		// Token: 0x06006E9A RID: 28314 RVA: 0x00246110 File Offset: 0x00244310
		private void OnClickDelete()
		{
			if (this.m_bIsMyReview)
			{
				NKCPopupUnitReviewDelete.Instance.OpenUI(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_REVIEW_DELETE, new NKCPopupUnitReviewDelete.OnButton(this.OnDelete));
				return;
			}
			if (this.m_bIsBannedUser)
			{
				NKCPopupUnitReviewDelete.Instance.OpenUI(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_REVIEW_BAN_CANCEL_DESC, new NKCPopupUnitReviewDelete.OnButton(this.OnUnBan));
				return;
			}
			NKCPopupUnitReviewDelete.Instance.OpenUI(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_REVIEW_BAN_DESC, new NKCPopupUnitReviewDelete.OnButton(this.OnBan));
		}

		// Token: 0x06006E9B RID: 28315 RVA: 0x0024618F File Offset: 0x0024438F
		private void OnDelete()
		{
			NKCPacketSender.Send_NKMPacket_UNIT_REVIEW_COMMENT_DELETE_REQ(this.m_nUnitID);
		}

		// Token: 0x06006E9C RID: 28316 RVA: 0x0024619C File Offset: 0x0024439C
		private void OnBan()
		{
			NKCPacketSender.Send_NKMPacket_UNIT_REVIEW_USER_BAN_REQ(this.m_UserUid);
		}

		// Token: 0x06006E9D RID: 28317 RVA: 0x002461A9 File Offset: 0x002443A9
		private void OnUnBan()
		{
			NKCPacketSender.Send_NKMPacket_UNIT_REVIEW_USER_BAN_CANCEL_REQ(this.m_UserUid);
		}

		// Token: 0x06006E9E RID: 28318 RVA: 0x002461B6 File Offset: 0x002443B6
		private void OnClickVote(bool bSelect)
		{
			if (bSelect)
			{
				NKCPacketSender.Send_NKMPacket_UNIT_REVIEW_COMMENT_VOTE_REQ(this.m_nUnitID, this.m_CommentUID);
				return;
			}
			NKCPacketSender.Send_NKMPacket_UNIT_REVIEW_COMMENT_VOTE_CANCEL_REQ(this.m_nUnitID, this.m_CommentUID);
		}

		// Token: 0x040059F1 RID: 23025
		private const int MAX_REVIEW_COUNT = 9999;

		// Token: 0x040059F2 RID: 23026
		public GameObject m_objUserReview;

		// Token: 0x040059F3 RID: 23027
		public Text m_lbUserLevel;

		// Token: 0x040059F4 RID: 23028
		public Text m_lbUserName;

		// Token: 0x040059F5 RID: 23029
		public GameObject m_objUserBest;

		// Token: 0x040059F6 RID: 23030
		public GameObject m_objMyReviewTag;

		// Token: 0x040059F7 RID: 23031
		public Text m_lbUserDate;

		// Token: 0x040059F8 RID: 23032
		public NKCUIComStateButton m_btnDelete;

		// Token: 0x040059F9 RID: 23033
		public Text m_lbDelete;

		// Token: 0x040059FA RID: 23034
		public NKCUIComToggle m_tglUserLike;

		// Token: 0x040059FB RID: 23035
		public Text m_lbUserLikeCountOn;

		// Token: 0x040059FC RID: 23036
		public Text m_lbUserLikeCountOff;

		// Token: 0x040059FD RID: 23037
		public Text m_lbDesc;

		// Token: 0x040059FE RID: 23038
		private bool bInitComplete;

		// Token: 0x040059FF RID: 23039
		private int m_nUnitID;

		// Token: 0x04005A00 RID: 23040
		private long m_CommentUID;

		// Token: 0x04005A01 RID: 23041
		private long m_UserUid;

		// Token: 0x04005A02 RID: 23042
		private bool m_bIsMyReview;

		// Token: 0x04005A03 RID: 23043
		private bool m_bIsBannedUser;

		// Token: 0x04005A04 RID: 23044
		private Color COLOR_MY_REVIEW_TEXT = new Color(1f, 0.8745098f, 0.3647059f);

		// Token: 0x04005A05 RID: 23045
		private Color COLOR_NORMAL_REVIEW_TEXT = new Color(0.30588236f, 0.7607843f, 0.9529412f);
	}
}
