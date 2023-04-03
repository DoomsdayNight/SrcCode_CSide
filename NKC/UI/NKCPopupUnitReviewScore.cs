using System;
using ClientPacket.Community;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A8E RID: 2702
	public class NKCPopupUnitReviewScore : NKCUIBase
	{
		// Token: 0x170013FD RID: 5117
		// (get) Token: 0x0600778F RID: 30607 RVA: 0x0027C118 File Offset: 0x0027A318
		public static NKCPopupUnitReviewScore Instance
		{
			get
			{
				if (NKCPopupUnitReviewScore.m_Instance == null)
				{
					NKCPopupUnitReviewScore.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupUnitReviewScore>("ab_ui_nkm_ui_unit_review", "NKM_UI_UNIT_REVIEW_POPUP_AVG_SCORE_UPLOAD", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupUnitReviewScore.CleanupInstance)).GetInstance<NKCPopupUnitReviewScore>();
					NKCPopupUnitReviewScore.m_Instance.InitUI();
				}
				return NKCPopupUnitReviewScore.m_Instance;
			}
		}

		// Token: 0x170013FE RID: 5118
		// (get) Token: 0x06007790 RID: 30608 RVA: 0x0027C167 File Offset: 0x0027A367
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupUnitReviewScore.m_Instance != null && NKCPopupUnitReviewScore.m_Instance.IsOpen;
			}
		}

		// Token: 0x06007791 RID: 30609 RVA: 0x0027C182 File Offset: 0x0027A382
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupUnitReviewScore.m_Instance != null && NKCPopupUnitReviewScore.m_Instance.IsOpen)
			{
				NKCPopupUnitReviewScore.m_Instance.Close();
			}
		}

		// Token: 0x06007792 RID: 30610 RVA: 0x0027C1A7 File Offset: 0x0027A3A7
		private static void CleanupInstance()
		{
			NKCPopupUnitReviewScore.m_Instance = null;
		}

		// Token: 0x170013FF RID: 5119
		// (get) Token: 0x06007793 RID: 30611 RVA: 0x0027C1AF File Offset: 0x0027A3AF
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001400 RID: 5120
		// (get) Token: 0x06007794 RID: 30612 RVA: 0x0027C1B2 File Offset: 0x0027A3B2
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_POPUP_UNIT_REVIEW_SCORE;
			}
		}

		// Token: 0x06007795 RID: 30613 RVA: 0x0027C1B9 File Offset: 0x0027A3B9
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06007796 RID: 30614 RVA: 0x0027C1C8 File Offset: 0x0027A3C8
		private void InitUI()
		{
			this.m_tglScore_1.OnValueChanged.RemoveAllListeners();
			this.m_tglScore_1.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickScore_1));
			this.m_tglScore_2.OnValueChanged.RemoveAllListeners();
			this.m_tglScore_2.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickScore_2));
			this.m_tglScore_3.OnValueChanged.RemoveAllListeners();
			this.m_tglScore_3.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickScore_3));
			this.m_tglScore_4.OnValueChanged.RemoveAllListeners();
			this.m_tglScore_4.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickScore_4));
			this.m_tglScore_5.OnValueChanged.RemoveAllListeners();
			this.m_tglScore_5.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickScore_5));
			this.m_btnOk.PointerClick.RemoveAllListeners();
			this.m_btnOk.PointerClick.AddListener(new UnityAction(this.OnClickOk));
			NKCUtil.SetHotkey(this.m_btnOk, HotkeyEventType.Confirm, null, false);
			this.m_btnCancel.PointerClick.RemoveAllListeners();
			this.m_btnCancel.PointerClick.AddListener(new UnityAction(this.OnClickClose));
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(this.OnClickClose));
			this.m_openAni = new NKCUIOpenAnimator(base.gameObject);
			this.m_bInitComplete = true;
		}

		// Token: 0x06007797 RID: 30615 RVA: 0x0027C35C File Offset: 0x0027A55C
		public void OpenUI(NKMUnitReviewScoreData scoreData, NKCPopupUnitReviewScore.OnVoteScore onVoteScore)
		{
			if (!this.m_bInitComplete)
			{
				this.InitUI();
			}
			if (this.m_openAni != null)
			{
				this.m_openAni.PlayOpenAni();
			}
			if (scoreData == null)
			{
				scoreData = new NKMUnitReviewScoreData();
			}
			if (onVoteScore != null)
			{
				this.dOnVoteScore = onVoteScore;
			}
			this.m_nLastVotedScore = (int)scoreData.myScore;
			NKCUtil.SetLabelText(this.m_lbTitle, this.MenuName);
			string msg = (scoreData.votedCount > 9999) ? string.Format(NKCUtilString.GET_STRING_UNIT_REVIEW_SCORE_VOTE_PLUS_ONE_PARAM, scoreData.votedCount) : string.Format(NKCUtilString.GET_STRING_UNIT_REVIEW_SCORE_VOTE_ONE_PARAM, scoreData.votedCount);
			NKCUtil.SetLabelText(this.m_lbCount, msg);
			switch (this.m_nLastVotedScore)
			{
			default:
				this.OnClickScore_1(true);
				break;
			case 2:
				this.OnClickScore_2(true);
				break;
			case 3:
				this.OnClickScore_3(true);
				break;
			case 4:
				this.OnClickScore_4(true);
				break;
			case 5:
				this.OnClickScore_5(true);
				break;
			}
			base.UIOpened(true);
		}

		// Token: 0x06007798 RID: 30616 RVA: 0x0027C456 File Offset: 0x0027A656
		private void OnClickScore_1(bool bSelect)
		{
			if (bSelect)
			{
				this.m_selectedScore = 1;
			}
			this.m_tglScore_1.Select(bSelect, true, true);
		}

		// Token: 0x06007799 RID: 30617 RVA: 0x0027C471 File Offset: 0x0027A671
		private void OnClickScore_2(bool bSelect)
		{
			if (bSelect)
			{
				this.m_selectedScore = 2;
			}
			this.m_tglScore_2.Select(bSelect, true, true);
		}

		// Token: 0x0600779A RID: 30618 RVA: 0x0027C48C File Offset: 0x0027A68C
		private void OnClickScore_3(bool bSelect)
		{
			if (bSelect)
			{
				this.m_selectedScore = 3;
			}
			this.m_tglScore_3.Select(bSelect, true, true);
		}

		// Token: 0x0600779B RID: 30619 RVA: 0x0027C4A7 File Offset: 0x0027A6A7
		private void OnClickScore_4(bool bSelect)
		{
			if (bSelect)
			{
				this.m_selectedScore = 4;
			}
			this.m_tglScore_4.Select(bSelect, true, true);
		}

		// Token: 0x0600779C RID: 30620 RVA: 0x0027C4C2 File Offset: 0x0027A6C2
		private void OnClickScore_5(bool bSelect)
		{
			if (bSelect)
			{
				this.m_selectedScore = 5;
			}
			this.m_tglScore_5.Select(bSelect, true, true);
		}

		// Token: 0x0600779D RID: 30621 RVA: 0x0027C4DD File Offset: 0x0027A6DD
		private void OnClickOk()
		{
			if (this.m_nLastVotedScore == this.m_selectedScore)
			{
				base.Close();
				return;
			}
			if (this.dOnVoteScore != null)
			{
				this.dOnVoteScore(this.m_selectedScore);
			}
			base.Close();
		}

		// Token: 0x0600779E RID: 30622 RVA: 0x0027C513 File Offset: 0x0027A713
		private void OnClickClose()
		{
			base.Close();
		}

		// Token: 0x0600779F RID: 30623 RVA: 0x0027C51B File Offset: 0x0027A71B
		private void Update()
		{
			if (base.IsOpen && this.m_openAni != null)
			{
				this.m_openAni.Update();
			}
		}

		// Token: 0x04006421 RID: 25633
		private const int MAX_VOTED_COUNT = 9999;

		// Token: 0x04006422 RID: 25634
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_unit_review";

		// Token: 0x04006423 RID: 25635
		private const string UI_ASSET_NAME = "NKM_UI_UNIT_REVIEW_POPUP_AVG_SCORE_UPLOAD";

		// Token: 0x04006424 RID: 25636
		private static NKCPopupUnitReviewScore m_Instance;

		// Token: 0x04006425 RID: 25637
		public Text m_lbTitle;

		// Token: 0x04006426 RID: 25638
		public NKCUIComToggle m_tglScore_1;

		// Token: 0x04006427 RID: 25639
		public NKCUIComToggle m_tglScore_2;

		// Token: 0x04006428 RID: 25640
		public NKCUIComToggle m_tglScore_3;

		// Token: 0x04006429 RID: 25641
		public NKCUIComToggle m_tglScore_4;

		// Token: 0x0400642A RID: 25642
		public NKCUIComToggle m_tglScore_5;

		// Token: 0x0400642B RID: 25643
		public Text m_lbCount;

		// Token: 0x0400642C RID: 25644
		public NKCUIComStateButton m_btnOk;

		// Token: 0x0400642D RID: 25645
		public NKCUIComStateButton m_btnCancel;

		// Token: 0x0400642E RID: 25646
		public NKCUIComStateButton m_btnClose;

		// Token: 0x0400642F RID: 25647
		private NKCPopupUnitReviewScore.OnVoteScore dOnVoteScore;

		// Token: 0x04006430 RID: 25648
		private bool m_bInitComplete;

		// Token: 0x04006431 RID: 25649
		private int m_unitID;

		// Token: 0x04006432 RID: 25650
		private int m_nLastVotedScore;

		// Token: 0x04006433 RID: 25651
		private int m_selectedScore;

		// Token: 0x04006434 RID: 25652
		private NKCUIOpenAnimator m_openAni;

		// Token: 0x020017EB RID: 6123
		// (Invoke) Token: 0x0600B496 RID: 46230
		public delegate void OnVoteScore(int votedScore);
	}
}
