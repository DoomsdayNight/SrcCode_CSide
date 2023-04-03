using System;
using System.Collections.Generic;
using ClientPacket.Community;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009F6 RID: 2550
	public class NKCUIUnitReview : NKCUIBase
	{
		// Token: 0x170012A5 RID: 4773
		// (get) Token: 0x06006E76 RID: 28278 RVA: 0x00244D08 File Offset: 0x00242F08
		public static NKCUIUnitReview Instance
		{
			get
			{
				if (NKCUIUnitReview.m_Instance == null)
				{
					NKCUIUnitReview.m_Instance = NKCUIManager.OpenNewInstance<NKCUIUnitReview>("ab_ui_nkm_ui_unit_review", "NKM_UI_UNIT_REVIEW", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIUnitReview.CleanupInstance)).GetInstance<NKCUIUnitReview>();
					NKCUIUnitReview.m_Instance.InitUI();
				}
				return NKCUIUnitReview.m_Instance;
			}
		}

		// Token: 0x170012A6 RID: 4774
		// (get) Token: 0x06006E77 RID: 28279 RVA: 0x00244D57 File Offset: 0x00242F57
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIUnitReview.m_Instance != null && NKCUIUnitReview.m_Instance.IsOpen;
			}
		}

		// Token: 0x06006E78 RID: 28280 RVA: 0x00244D72 File Offset: 0x00242F72
		private static void CleanupInstance()
		{
			NKCUIUnitReview.m_Instance = null;
		}

		// Token: 0x170012A7 RID: 4775
		// (get) Token: 0x06006E79 RID: 28281 RVA: 0x00244D7A File Offset: 0x00242F7A
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170012A8 RID: 4776
		// (get) Token: 0x06006E7A RID: 28282 RVA: 0x00244D7D File Offset: 0x00242F7D
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_UNIT_REVIEW;
			}
		}

		// Token: 0x06006E7B RID: 28283 RVA: 0x00244D84 File Offset: 0x00242F84
		public override void CloseInternal()
		{
			this.m_cUnitTempletBase = null;
			this.m_lstCommentDataOrderByLike.Clear();
			this.m_lstCommentDataOrderByNew.Clear();
			this.m_lstBestCommentData.Clear();
			this.m_cScoreData = new NKMUnitReviewScoreData();
			NKCUtil.SetGameobjectActive(this.m_btnSortByVoted, false);
			NKCUtil.SetGameobjectActive(this.m_btnSortByDate, true);
			this.m_bSortByLike = false;
			this.m_bIsLastPageByNew = false;
			this.m_bIsLastPageByLike = false;
			this.m_bFirstOpen = true;
			this.m_nRequestedPageByNew = 0;
			this.m_nRequestedPageLike = 0;
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06006E7C RID: 28284 RVA: 0x00244E14 File Offset: 0x00243014
		private void NullReferenceCheck()
		{
			if (this.m_pfbReviewSlot == null)
			{
				Debug.LogError("m_pfbReviewSlot is null");
			}
			if (this.m_CharacterView == null)
			{
				Debug.LogError("m_CharacterView is null");
			}
			if (this.m_lbUnitClass == null)
			{
				Debug.LogError("m_lbUnitClass is null");
			}
			if (this.m_lbUnitTitle == null)
			{
				Debug.LogError("m_lbUnitTitle is null");
			}
			if (this.m_lbUnitName == null)
			{
				Debug.LogError("m_lbUnitName is null");
			}
			if (this.m_lbCurScore == null)
			{
				Debug.LogError("m_lbCurScore is null");
			}
			if (this.m_objMyScoreComplete == null)
			{
				Debug.LogError("m_objMyScoreComplete is null");
			}
			if (this.m_lbMyScore == null)
			{
				Debug.LogError("m_lbMyScore is null");
			}
			if (this.m_btnScore == null)
			{
				Debug.LogError("m_btnScore is null");
			}
			if (this.m_lbReviewCount == null)
			{
				Debug.LogError("m_lbReviewCount is null");
			}
			if (this.m_btnSortByDate == null)
			{
				Debug.LogError("m_btnSortByDate is null");
			}
			if (this.m_btnSortByVoted == null)
			{
				Debug.LogError("m_btnSortByVoted is null");
			}
			if (this.m_loopScrollRect == null)
			{
				Debug.LogError("m_loopScrollRect is null");
			}
			if (this.m_trSlotParent == null)
			{
				Debug.LogError("m_trSlotParent is null");
			}
			if (this.m_btnRegistReview == null)
			{
				Debug.LogError("m_btnRegistReview is null");
			}
			if (this.m_input == null)
			{
				Debug.LogError("m_input is null");
			}
			if (this.m_lbReviewText == null)
			{
				Debug.LogError("m_lbReviewText is null");
			}
			if (this.m_lbReviewInputCount == null)
			{
				Debug.LogError("m_lbReviewInputCount is null");
			}
		}

		// Token: 0x06006E7D RID: 28285 RVA: 0x00244FD4 File Offset: 0x002431D4
		private void InitUI()
		{
			this.NullReferenceCheck();
			this.m_CharacterView.Init(null, null);
			this.m_btnScore.PointerClick.RemoveAllListeners();
			this.m_btnScore.PointerClick.AddListener(new UnityAction(this.OnClickScore));
			this.m_btnSortByDate.PointerClick.RemoveAllListeners();
			this.m_btnSortByDate.PointerClick.AddListener(new UnityAction(this.OnClickSortByDateToVote));
			this.m_btnSortByVoted.PointerClick.RemoveAllListeners();
			this.m_btnSortByVoted.PointerClick.AddListener(new UnityAction(this.OnClickSortByVoteToDate));
			this.m_btnRegistReview.PointerClick.RemoveAllListeners();
			this.m_btnRegistReview.PointerClick.AddListener(new UnityAction(this.OnClickRegistReview));
			this.m_loopScrollRect.dOnGetObject += this.OnGetObject;
			this.m_loopScrollRect.dOnReturnObject += this.OnReturnObject;
			this.m_loopScrollRect.dOnProvideData += this.OnProvideData;
			this.m_loopScrollRect.ContentConstraintCount = 1;
			this.m_loopScrollRect.PrepareCells(0);
			NKCUtil.SetScrollHotKey(this.m_loopScrollRect, null);
			this.m_input.onValidateInput = new InputField.OnValidateInput(NKCFilterManager.FilterEmojiInput);
			this.m_input.onValueChanged.RemoveAllListeners();
			this.m_input.onValueChanged.AddListener(new UnityAction<string>(this.OnChangeInput));
			this.m_input.onEndEdit.RemoveAllListeners();
			this.m_input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditReview));
			this.m_bInitComplete = true;
		}

		// Token: 0x06006E7E RID: 28286 RVA: 0x00245184 File Offset: 0x00243384
		public void OpenUI(int unitID)
		{
			if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.UNIT_REVIEW_SYSTEM))
			{
				return;
			}
			if (!this.m_bInitComplete)
			{
				this.InitUI();
			}
			this.m_cUnitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
			if (this.m_cUnitTempletBase != null)
			{
				base.UIOpened(true);
				this.m_nRequestedPageByNew++;
				if (!NKCUnitReviewManager.m_bReceivedUnitReviewBanList)
				{
					NKCPacketSender.Send_NKMPacket_UNIT_REVIEW_USER_BAN_LIST_REQ();
				}
				else
				{
					NKCPacketSender.Send_NKMPacket_UNIT_REVIEW_COMMENT_AND_SCORE_REQ(unitID, 1, false);
				}
				this.SetUnitData(this.m_cUnitTempletBase);
			}
		}

		// Token: 0x06006E7F RID: 28287 RVA: 0x002451F4 File Offset: 0x002433F4
		private void SetUnitData(NKMUnitTempletBase unitTempletBase)
		{
			this.m_CharacterView.SetCharacterIllust(unitTempletBase, 0, false, true, 0);
			NKCUtil.SetLabelText(this.m_lbUnitClass, NKCUtilString.GetUnitStyleString(unitTempletBase));
			NKCUtil.SetLabelText(this.m_lbUnitTitle, unitTempletBase.GetUnitTitle());
			NKCUtil.SetLabelText(this.m_lbUnitName, unitTempletBase.GetUnitName());
		}

		// Token: 0x06006E80 RID: 28288 RVA: 0x00245244 File Offset: 0x00243444
		private void SetCommentList(List<NKMUnitReviewCommentData> commentDataList)
		{
			int num = (commentDataList == null) ? 0 : commentDataList.Count;
			int i;
			Predicate<NKMUnitReviewCommentData> <>9__1;
			int j;
			for (i = commentDataList.Count - 1; i >= 0; i = j - 1)
			{
				List<NKMUnitReviewCommentData> lstBestCommentData = this.m_lstBestCommentData;
				Predicate<NKMUnitReviewCommentData> match;
				if ((match = <>9__1) == null)
				{
					match = (<>9__1 = ((NKMUnitReviewCommentData x) => x.userUID == commentDataList[i].userUID));
				}
				if (lstBestCommentData.Find(match) != null)
				{
					commentDataList.RemoveAt(i);
				}
				j = i;
			}
			if (commentDataList != null)
			{
				if (this.m_bSortByLike)
				{
					if (num < 10)
					{
						this.m_bIsLastPageByLike = true;
					}
					this.m_lstCommentDataOrderByLike.AddRange(commentDataList);
					this.m_loopScrollRect.TotalCount = this.m_lstCommentDataOrderByLike.Count;
				}
				else
				{
					if (num < 10)
					{
						this.m_bIsLastPageByNew = true;
					}
					this.m_lstCommentDataOrderByNew.AddRange(commentDataList);
					this.m_loopScrollRect.TotalCount = this.m_lstCommentDataOrderByNew.Count;
				}
			}
			else if (this.m_bSortByLike)
			{
				this.m_loopScrollRect.TotalCount = this.m_lstCommentDataOrderByLike.Count;
				this.m_bIsLastPageByLike = true;
			}
			else
			{
				this.m_loopScrollRect.TotalCount = this.m_lstCommentDataOrderByNew.Count;
				this.m_bIsLastPageByNew = true;
			}
			this.m_loopScrollRect.RefreshCells(false);
			int num2 = this.m_bSortByLike ? this.m_lstCommentDataOrderByLike.Count : this.m_lstCommentDataOrderByNew.Count;
			if (this.m_bMyReviewExist)
			{
				if (this.m_lstBestCommentData.Find((NKMUnitReviewCommentData x) => x.userUID == NKCScenManager.CurrentUserData().m_UserUID) == null)
				{
					num2--;
				}
			}
			if (num2 > 9999)
			{
				NKCUtil.SetLabelText(this.m_lbReviewCount, string.Format("{0}+", num2));
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbReviewCount, num2.ToString());
			}
			if (this.m_bFirstOpen)
			{
				this.m_bFirstOpen = false;
				this.OnClickSortByVoteToDate();
			}
		}

		// Token: 0x06006E81 RID: 28289 RVA: 0x00245454 File Offset: 0x00243654
		private void SetScoreData(NKMUnitReviewScoreData scoreData)
		{
			this.m_cScoreData = scoreData;
			if (scoreData == null || scoreData.votedCount <= 0)
			{
				NKCUtil.SetLabelText(this.m_lbCurScore, "  -  ");
				NKCUtil.SetGameobjectActive(this.m_objMyScoreComplete, false);
				return;
			}
			NKCUtil.SetLabelText(this.m_lbCurScore, string.Format("{0:F1}", scoreData.avgScore));
			if (scoreData.myScore > 0)
			{
				NKCUtil.SetGameobjectActive(this.m_objMyScoreComplete, true);
				NKCUtil.SetLabelText(this.m_lbMyScore, scoreData.myScore.ToString());
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objMyScoreComplete, false);
		}

		// Token: 0x06006E82 RID: 28290 RVA: 0x002454EC File Offset: 0x002436EC
		public void RecvReviewData(NKMPacket_UNIT_REVIEW_COMMENT_AND_SCORE_ACK sPacket)
		{
			this.m_lstCommentDataOrderByLike.Clear();
			this.m_lstCommentDataOrderByNew.Clear();
			if (sPacket.myUnitReviewCommentData != null && !string.IsNullOrEmpty(sPacket.myUnitReviewCommentData.content))
			{
				this.m_bMyReviewExist = true;
				if (sPacket.bestUnitReviewCommentDataList != null && sPacket.bestUnitReviewCommentDataList.Find((NKMUnitReviewCommentData x) => x.userUID == sPacket.myUnitReviewCommentData.userUID) == null)
				{
					this.m_lstCommentDataOrderByNew.Add(sPacket.myUnitReviewCommentData);
					this.m_lstCommentDataOrderByLike.Add(sPacket.myUnitReviewCommentData);
				}
			}
			else
			{
				this.m_bMyReviewExist = false;
			}
			if (sPacket.bestUnitReviewCommentDataList != null)
			{
				this.m_lstBestCommentData = sPacket.bestUnitReviewCommentDataList;
				this.m_lstCommentDataOrderByLike.AddRange(sPacket.bestUnitReviewCommentDataList);
				this.m_lstCommentDataOrderByNew.AddRange(sPacket.bestUnitReviewCommentDataList);
			}
			this.m_input.text = "";
			this.m_lbReviewInputCount.text = "0/130";
			if (sPacket.unitReviewCommentDataList != null)
			{
				this.SetCommentList(sPacket.unitReviewCommentDataList);
			}
			this.SetScoreData(sPacket.unitReviewScoreData);
		}

		// Token: 0x06006E83 RID: 28291 RVA: 0x00245640 File Offset: 0x00243840
		public void RecvCommentList(List<NKMUnitReviewCommentData> commentDataList)
		{
			this.SetCommentList(commentDataList);
		}

		// Token: 0x06006E84 RID: 28292 RVA: 0x00245649 File Offset: 0x00243849
		public void RecvScoreVoteAck(int unitID, NKMUnitReviewScoreData scoreData)
		{
			if (unitID == this.m_cUnitTempletBase.m_UnitID)
			{
				this.SetScoreData(scoreData);
			}
		}

		// Token: 0x06006E85 RID: 28293 RVA: 0x00245660 File Offset: 0x00243860
		public void RecvMyCommentChanged(NKMUnitReviewCommentData myReview)
		{
			this.m_lstCommentDataOrderByNew.Clear();
			this.m_lstCommentDataOrderByLike.Clear();
			if (this.m_bSortByLike)
			{
				this.m_nRequestedPageByNew = 0;
				this.m_nRequestedPageLike = 1;
			}
			else
			{
				this.m_nRequestedPageByNew = 1;
				this.m_nRequestedPageLike = 0;
			}
			this.m_bIsLastPageByLike = false;
			this.m_bIsLastPageByNew = false;
			if (myReview != null)
			{
				if (this.m_lstBestCommentData.Find((NKMUnitReviewCommentData x) => x.userUID == myReview.userUID) == null)
				{
					this.m_lstCommentDataOrderByNew.Add(myReview);
					this.m_lstCommentDataOrderByLike.Add(myReview);
				}
				this.m_bMyReviewExist = true;
			}
			else
			{
				this.m_bMyReviewExist = false;
			}
			this.m_lstCommentDataOrderByNew.AddRange(this.m_lstBestCommentData);
			this.m_lstCommentDataOrderByLike.AddRange(this.m_lstBestCommentData);
			this.m_input.text = string.Empty;
			NKCPacketSender.Send_NKMPacket_UNIT_REVIEW_COMMENT_LIST_REQ(this.m_cUnitTempletBase.m_UnitID, 1, this.m_bSortByLike);
		}

		// Token: 0x06006E86 RID: 28294 RVA: 0x00245760 File Offset: 0x00243960
		public void RecvCommentVote(int unitID, NKMUnitReviewCommentData commentData, bool bVote)
		{
			if (unitID == this.m_cUnitTempletBase.m_UnitID)
			{
				List<NKMUnitReviewCommentData> list = this.m_lstCommentDataOrderByLike.FindAll((NKMUnitReviewCommentData x) => x.commentUID == commentData.commentUID);
				for (int i = 0; i < list.Count; i++)
				{
					list[i].votedCount = commentData.votedCount;
					list[i].isVoted = commentData.isVoted;
				}
				list = this.m_lstCommentDataOrderByNew.FindAll((NKMUnitReviewCommentData x) => x.commentUID == commentData.commentUID);
				for (int j = 0; j < list.Count; j++)
				{
					list[j].votedCount = commentData.votedCount;
					list[j].isVoted = commentData.isVoted;
				}
				List<NKCUIUnitReviewSlot> list2 = this.m_lstReviewSlot.FindAll((NKCUIUnitReviewSlot x) => x.GetCommentUID() == commentData.commentUID);
				for (int k = 0; k < list2.Count; k++)
				{
					list2[k].ChangeVotedCount(commentData.votedCount, bVote);
				}
			}
		}

		// Token: 0x06006E87 RID: 28295 RVA: 0x00245885 File Offset: 0x00243A85
		public void OnRecvBanList()
		{
			NKCPacketSender.Send_NKMPacket_UNIT_REVIEW_COMMENT_AND_SCORE_REQ(this.m_cUnitTempletBase.m_UnitID, 1, false);
		}

		// Token: 0x06006E88 RID: 28296 RVA: 0x00245899 File Offset: 0x00243A99
		public void RefreshUI()
		{
			this.m_loopScrollRect.RefreshCells(false);
		}

		// Token: 0x06006E89 RID: 28297 RVA: 0x002458A7 File Offset: 0x00243AA7
		private void OnClickScore()
		{
			NKCPopupUnitReviewScore.Instance.OpenUI(this.m_cScoreData, new NKCPopupUnitReviewScore.OnVoteScore(this.OnVoteScore));
		}

		// Token: 0x06006E8A RID: 28298 RVA: 0x002458C8 File Offset: 0x00243AC8
		private void OnClickSortByDateToVote()
		{
			if (this.m_bSortByLike)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_btnSortByVoted, true);
			NKCUtil.SetGameobjectActive(this.m_btnSortByDate, false);
			this.m_loopScrollRect.SetIndexPosition(0);
			this.m_bSortByLike = true;
			if (this.m_nRequestedPageLike > 0)
			{
				this.m_loopScrollRect.RefreshCells(false);
				return;
			}
			this.m_nRequestedPageLike = 1;
			NKCPacketSender.Send_NKMPacket_UNIT_REVIEW_COMMENT_LIST_REQ(this.m_cUnitTempletBase.m_UnitID, this.m_nRequestedPageLike, this.m_bSortByLike);
		}

		// Token: 0x06006E8B RID: 28299 RVA: 0x00245944 File Offset: 0x00243B44
		private void OnClickSortByVoteToDate()
		{
			if (!this.m_bSortByLike)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_btnSortByVoted, false);
			NKCUtil.SetGameobjectActive(this.m_btnSortByDate, true);
			this.m_bSortByLike = false;
			this.m_loopScrollRect.SetIndexPosition(0);
			if (this.m_nRequestedPageByNew > 0)
			{
				this.m_loopScrollRect.RefreshCells(false);
				return;
			}
			this.m_nRequestedPageByNew = 1;
			NKCPacketSender.Send_NKMPacket_UNIT_REVIEW_COMMENT_LIST_REQ(this.m_cUnitTempletBase.m_UnitID, this.m_nRequestedPageByNew, this.m_bSortByLike);
		}

		// Token: 0x06006E8C RID: 28300 RVA: 0x002459BE File Offset: 0x00243BBE
		private void OnChangeInput(string inputText)
		{
			this.m_lbReviewInputCount.text = string.Format("{0} / 130", inputText.Length);
		}

		// Token: 0x06006E8D RID: 28301 RVA: 0x002459E0 File Offset: 0x00243BE0
		private void OnEndEditReview(string inputText)
		{
			if (NKCInputManager.IsChatSubmitEnter())
			{
				if (!this.m_btnRegistReview.m_bLock)
				{
					this.OnClickRegistReview();
				}
				EventSystem.current.SetSelectedGameObject(null);
			}
		}

		// Token: 0x06006E8E RID: 28302 RVA: 0x00245A08 File Offset: 0x00243C08
		private void OnClickRegistReview()
		{
			if (string.IsNullOrWhiteSpace(this.m_input.text))
			{
				return;
			}
			if (this.m_bMyReviewExist)
			{
				NKCPopupUnitReviewDelete.Instance.OpenUI(NKCUtilString.GET_STRING_POPUP_UNIT_REVIEW_DELETE, NKCUtilString.GET_STRING_REVIEW_DELETE_AND_WRITE, new NKCPopupUnitReviewDelete.OnButton(this.OnRewrite));
				return;
			}
			NKCPacketSender.Send_NKMPacket_UNIT_REVIEW_COMMENT_WRITE_REQ(this.m_cUnitTempletBase.m_UnitID, this.m_input.text, false);
		}

		// Token: 0x06006E8F RID: 28303 RVA: 0x00245A6D File Offset: 0x00243C6D
		private void OnRewrite()
		{
			NKCPacketSender.Send_NKMPacket_UNIT_REVIEW_COMMENT_WRITE_REQ(this.m_cUnitTempletBase.m_UnitID, this.m_input.text, true);
		}

		// Token: 0x06006E90 RID: 28304 RVA: 0x00245A8B File Offset: 0x00243C8B
		private void OnVoteScore(int votedScore)
		{
			NKCPacketSender.Send_NKMPacket_UNIT_REVIEW_SCORE_VOTE_REQ(this.m_cUnitTempletBase.m_UnitID, votedScore);
		}

		// Token: 0x06006E91 RID: 28305 RVA: 0x00245AA0 File Offset: 0x00243CA0
		public RectTransform OnGetObject(int index)
		{
			if (this.m_stkReviewSlot.Count == 0)
			{
				NKCUIUnitReviewSlot nkcuiunitReviewSlot = UnityEngine.Object.Instantiate<NKCUIUnitReviewSlot>(this.m_pfbReviewSlot, this.m_trSlotParent);
				nkcuiunitReviewSlot.SetData(null, 0, false);
				nkcuiunitReviewSlot.transform.localScale = Vector3.one;
				this.m_lstReviewSlot.Add(nkcuiunitReviewSlot);
				NKCUtil.SetGameobjectActive(nkcuiunitReviewSlot.gameObject, true);
				return nkcuiunitReviewSlot.GetComponent<RectTransform>();
			}
			NKCUIUnitReviewSlot nkcuiunitReviewSlot2 = this.m_stkReviewSlot.Pop();
			this.m_lstReviewSlot.Add(nkcuiunitReviewSlot2);
			NKCUtil.SetGameobjectActive(nkcuiunitReviewSlot2.gameObject, true);
			return nkcuiunitReviewSlot2.GetComponent<RectTransform>();
		}

		// Token: 0x06006E92 RID: 28306 RVA: 0x00245B30 File Offset: 0x00243D30
		public void OnReturnObject(Transform go)
		{
			NKCUIUnitReviewSlot component = go.GetComponent<NKCUIUnitReviewSlot>();
			this.m_lstReviewSlot.Remove(component);
			this.m_stkReviewSlot.Push(component);
			NKCUtil.SetGameobjectActive(component.gameObject, false);
			go.SetParent(base.transform);
		}

		// Token: 0x06006E93 RID: 28307 RVA: 0x00245B78 File Offset: 0x00243D78
		public void OnProvideData(Transform transform, int idx)
		{
			NKCUIUnitReviewSlot component = transform.GetComponent<NKCUIUnitReviewSlot>();
			if (this.m_bSortByLike)
			{
				if (this.m_lstCommentDataOrderByLike.Count > idx)
				{
					bool bBest = this.m_lstBestCommentData.Find((NKMUnitReviewCommentData x) => x.commentUID == this.m_lstCommentDataOrderByLike[idx].commentUID) != null;
					component.SetData(this.m_lstCommentDataOrderByLike[idx], this.m_cUnitTempletBase.m_UnitID, bBest);
					if (!component.gameObject.activeInHierarchy)
					{
						NKCUtil.SetGameobjectActive(component.gameObject, true);
					}
				}
				else
				{
					Debug.LogWarning(string.Format("리뷰데이터가 없음 - idx : {0}", idx));
				}
				if (!this.m_bIsLastPageByLike && idx == this.m_lstCommentDataOrderByLike.Count - 1)
				{
					this.m_nRequestedPageLike++;
					NKCPacketSender.Send_NKMPacket_UNIT_REVIEW_COMMENT_LIST_REQ(this.m_cUnitTempletBase.m_UnitID, this.m_nRequestedPageLike, this.m_bSortByLike);
					return;
				}
			}
			else
			{
				if (this.m_lstCommentDataOrderByNew.Count > idx)
				{
					bool bBest2 = this.m_lstBestCommentData.Find((NKMUnitReviewCommentData x) => x.commentUID == this.m_lstCommentDataOrderByNew[idx].commentUID) != null;
					component.SetData(this.m_lstCommentDataOrderByNew[idx], this.m_cUnitTempletBase.m_UnitID, bBest2);
					if (!component.gameObject.activeInHierarchy)
					{
						NKCUtil.SetGameobjectActive(component.gameObject, true);
					}
				}
				else
				{
					Debug.LogWarning(string.Format("리뷰데이터가 없음 - idx : {0}", idx));
				}
				if (!this.m_bIsLastPageByNew && idx == this.m_lstCommentDataOrderByNew.Count - 1)
				{
					this.m_nRequestedPageByNew++;
					NKCPacketSender.Send_NKMPacket_UNIT_REVIEW_COMMENT_LIST_REQ(this.m_cUnitTempletBase.m_UnitID, this.m_nRequestedPageByNew, this.m_bSortByLike);
				}
			}
		}

		// Token: 0x040059CA RID: 22986
		private const int MAX_REVIEW_COUNT = 9999;

		// Token: 0x040059CB RID: 22987
		private const int MAX_MY_COMMENT_VIEW_LENGTH = 25;

		// Token: 0x040059CC RID: 22988
		private const int COMMENT_COUNT_PER_PAGE = 10;

		// Token: 0x040059CD RID: 22989
		private const string UI_ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_unit_review";

		// Token: 0x040059CE RID: 22990
		private const string UI_ASSET_NAME = "NKM_UI_UNIT_REVIEW";

		// Token: 0x040059CF RID: 22991
		private static NKCUIUnitReview m_Instance;

		// Token: 0x040059D0 RID: 22992
		public NKCUIUnitReviewSlot m_pfbReviewSlot;

		// Token: 0x040059D1 RID: 22993
		[Header("좌측 캐릭터 정보 / 평점")]
		public NKCUICharacterView m_CharacterView;

		// Token: 0x040059D2 RID: 22994
		public Text m_lbUnitClass;

		// Token: 0x040059D3 RID: 22995
		public Text m_lbUnitTitle;

		// Token: 0x040059D4 RID: 22996
		public Text m_lbUnitName;

		// Token: 0x040059D5 RID: 22997
		public Text m_lbCurScore;

		// Token: 0x040059D6 RID: 22998
		public GameObject m_objMyScoreComplete;

		// Token: 0x040059D7 RID: 22999
		public Text m_lbMyScore;

		// Token: 0x040059D8 RID: 23000
		public NKCUIComStateButton m_btnScore;

		// Token: 0x040059D9 RID: 23001
		[Header("상단 메뉴")]
		public Text m_lbReviewCount;

		// Token: 0x040059DA RID: 23002
		public NKCUIComStateButton m_btnSortByDate;

		// Token: 0x040059DB RID: 23003
		public NKCUIComStateButton m_btnSortByVoted;

		// Token: 0x040059DC RID: 23004
		[Header("우측 리뷰 목록")]
		public LoopScrollRect m_loopScrollRect;

		// Token: 0x040059DD RID: 23005
		public Transform m_trSlotParent;

		// Token: 0x040059DE RID: 23006
		public NKCUIComStateButton m_btnRegistReview;

		// Token: 0x040059DF RID: 23007
		public InputField m_input;

		// Token: 0x040059E0 RID: 23008
		public Text m_lbReviewText;

		// Token: 0x040059E1 RID: 23009
		public Text m_lbReviewInputCount;

		// Token: 0x040059E2 RID: 23010
		private List<NKCUIUnitReviewSlot> m_lstReviewSlot = new List<NKCUIUnitReviewSlot>();

		// Token: 0x040059E3 RID: 23011
		private Stack<NKCUIUnitReviewSlot> m_stkReviewSlot = new Stack<NKCUIUnitReviewSlot>();

		// Token: 0x040059E4 RID: 23012
		private List<NKMUnitReviewCommentData> m_lstCommentDataOrderByLike = new List<NKMUnitReviewCommentData>();

		// Token: 0x040059E5 RID: 23013
		private List<NKMUnitReviewCommentData> m_lstCommentDataOrderByNew = new List<NKMUnitReviewCommentData>();

		// Token: 0x040059E6 RID: 23014
		private List<NKMUnitReviewCommentData> m_lstBestCommentData = new List<NKMUnitReviewCommentData>();

		// Token: 0x040059E7 RID: 23015
		private NKMUnitReviewScoreData m_cScoreData = new NKMUnitReviewScoreData();

		// Token: 0x040059E8 RID: 23016
		private bool m_bInitComplete;

		// Token: 0x040059E9 RID: 23017
		private bool m_bSortByLike;

		// Token: 0x040059EA RID: 23018
		private int m_nRequestedPageByNew;

		// Token: 0x040059EB RID: 23019
		private int m_nRequestedPageLike;

		// Token: 0x040059EC RID: 23020
		private bool m_bIsLastPageByNew;

		// Token: 0x040059ED RID: 23021
		private bool m_bIsLastPageByLike;

		// Token: 0x040059EE RID: 23022
		private bool m_bMyReviewExist;

		// Token: 0x040059EF RID: 23023
		private NKMUnitTempletBase m_cUnitTempletBase;

		// Token: 0x040059F0 RID: 23024
		private bool m_bFirstOpen = true;
	}
}
