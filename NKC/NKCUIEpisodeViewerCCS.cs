using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000989 RID: 2441
	public class NKCUIEpisodeViewerCCS : NKCUIBase
	{
		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x0600644E RID: 25678 RVA: 0x001FCF9E File Offset: 0x001FB19E
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_MENU_NAME_CCS;
			}
		}

		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x0600644F RID: 25679 RVA: 0x001FCFA5 File Offset: 0x001FB1A5
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x06006450 RID: 25680 RVA: 0x001FCFA8 File Offset: 0x001FB1A8
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Normal;
			}
		}

		// Token: 0x06006451 RID: 25681 RVA: 0x001FCFAB File Offset: 0x001FB1AB
		public void SetReservedResetBook(bool bSet)
		{
			this.m_bReservedResetBook = bSet;
		}

		// Token: 0x06006452 RID: 25682 RVA: 0x001FCFB4 File Offset: 0x001FB1B4
		public void InitOutComponents(GameObject cNUM_EPISODE_PREFAB)
		{
			this.m_NKM_EPISODE_CCS = cNUM_EPISODE_PREFAB.transform.Find("NKM_EPISODE_CCS").gameObject;
			this.m_rectListContent = cNUM_EPISODE_PREFAB.transform.Find("NKM_EPISODE_CCS/NKM_UI_OPERATION_EP_LIST/NKM_UI_OPERATION_EP_LIST_ScrollView/NKM_UI_OPERATION_EP_LIST_Viewport/NKM_UI_OPERATION_EP_LIST_Content").gameObject.GetComponent<RectTransform>();
			this.m_rectViewPort = cNUM_EPISODE_PREFAB.transform.Find("NKM_EPISODE_CCS/NKM_UI_OPERATION_EP_LIST/NKM_UI_OPERATION_EP_LIST_ScrollView/NKM_UI_OPERATION_EP_LIST_Viewport").gameObject.GetComponent<RectTransform>();
			this.m_SREPList = cNUM_EPISODE_PREFAB.transform.Find("NKM_EPISODE_CCS/NKM_UI_OPERATION_EP_LIST/NKM_UI_OPERATION_EP_LIST_ScrollView").gameObject.GetComponent<ScrollRect>();
		}

		// Token: 0x06006453 RID: 25683 RVA: 0x001FD03C File Offset: 0x001FB23C
		public static NKCUIEpisodeViewerCCS InitUI()
		{
			return NKCUIManager.OpenUI<NKCUIEpisodeViewerCCS>("NKM_EPISODE_CCS_Panel");
		}

		// Token: 0x06006454 RID: 25684 RVA: 0x001FD048 File Offset: 0x001FB248
		public void InitUI2()
		{
			NKCUIManager.OpenUI(this.m_NKM_EPISODE_CCS);
			NKCUtil.SetGameobjectActive(this.m_NKM_EPISODE_CCS, false);
			this.m_ViewPortCenterX = this.m_rectViewPort.anchoredPosition.x + this.m_rectViewPort.sizeDelta.x / 2f + -300f;
			this.m_fRectListContentOrgX = this.m_rectListContent.anchoredPosition.x;
			NKCUtil.SetGameobjectActive(this.m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO2, false);
			NKCUtil.SetGameobjectActive(this.m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_DOT, false);
			NKCUtil.SetGameobjectActive(this.m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO, false);
			if (this.m_NKCUISlot != null)
			{
				this.m_NKCUISlot.Init();
			}
			if (this.m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO2_START_BUTTON != null)
			{
				this.m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO2_START_BUTTON.PointerClick.RemoveAllListeners();
				this.m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO2_START_BUTTON.PointerClick.AddListener(new UnityAction(this.OnStartButton));
			}
			if (this.m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO_BOOK1 != null)
			{
				this.m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO_BOOK1.OnValueChanged.RemoveAllListeners();
				this.m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO_BOOK1.OnValueChanged.AddListener(new UnityAction<bool>(this.OnMissionSlotSelect0));
			}
			if (this.m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO_BOOK2 != null)
			{
				this.m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO_BOOK2.OnValueChanged.RemoveAllListeners();
				this.m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO_BOOK2.OnValueChanged.AddListener(new UnityAction<bool>(this.OnMissionSlotSelect1));
			}
			if (this.m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO_BOOK3 != null)
			{
				this.m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO_BOOK3.OnValueChanged.RemoveAllListeners();
				this.m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO_BOOK3.OnValueChanged.AddListener(new UnityAction<bool>(this.OnMissionSlotSelect2));
			}
			if (this.m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO_BOOK4 != null)
			{
				this.m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO_BOOK4.OnValueChanged.RemoveAllListeners();
				this.m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO_BOOK4.OnValueChanged.AddListener(new UnityAction<bool>(this.OnMissionSlotSelect3));
			}
			if (base.gameObject)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x06006455 RID: 25685 RVA: 0x001FD22E File Offset: 0x001FB42E
		public void SetEpisodeID(int episodeID)
		{
			if (episodeID <= 0)
			{
				return;
			}
			NKCUIEpisodeViewerCCS.m_EpisodeID = episodeID;
		}

		// Token: 0x06006456 RID: 25686 RVA: 0x001FD23B File Offset: 0x001FB43B
		public override void Hide()
		{
			base.Hide();
			NKCUtil.SetGameobjectActive(this.m_NKM_EPISODE_CCS, false);
		}

		// Token: 0x06006457 RID: 25687 RVA: 0x001FD24F File Offset: 0x001FB44F
		public override void UnHide()
		{
			base.UnHide();
			NKCUtil.SetGameobjectActive(this.m_NKM_EPISODE_CCS, true);
		}

		// Token: 0x06006458 RID: 25688 RVA: 0x001FD264 File Offset: 0x001FB464
		private void OnSelectedActSlot(int actID)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(NKCUIEpisodeViewerCCS.m_EpisodeID, EPISODE_DIFFICULTY.NORMAL);
			if (nkmepisodeTempletV == null)
			{
				return;
			}
			if (!NKMEpisodeMgr.CheckLockCounterCase(myUserData, nkmepisodeTempletV, actID))
			{
				this.OnSlotSelected(actID);
			}
		}

		// Token: 0x06006459 RID: 25689 RVA: 0x001FD2A1 File Offset: 0x001FB4A1
		public void OnSlotPointerUp()
		{
			this.m_bReserveSnapToGrid = true;
			this.m_fElapsedTimeReserveSnapToGrid = 0f;
		}

		// Token: 0x0600645A RID: 25690 RVA: 0x001FD2B8 File Offset: 0x001FB4B8
		private void ScrollForCenter(NKCUICCSecretSlot cNKCUICCSecretSlot, float fTime = 0.6f)
		{
			if (cNKCUICCSecretSlot == null)
			{
				return;
			}
			float targetVal = cNKCUICCSecretSlot.GetHalfOfWidth() + this.m_rectListContent.anchoredPosition.x + (this.m_ViewPortCenterX - (this.m_rectListContent.anchoredPosition.x + cNKCUICCSecretSlot.GetCenterX()));
			this.m_NKMTrackingFloat.SetNowValue(this.m_rectListContent.anchoredPosition.x);
			this.m_NKMTrackingFloat.SetTracking(targetVal, fTime, TRACKING_DATA_TYPE.TDT_SLOWER);
		}

		// Token: 0x0600645B RID: 25691 RVA: 0x001FD330 File Offset: 0x001FB530
		private void SnapToGrid()
		{
			float num = float.MaxValue;
			NKCUICCSecretSlot nkcuiccsecretSlot = null;
			for (int i = 0; i < this.m_listNKCUICCSecretSlot.Count; i++)
			{
				NKCUICCSecretSlot nkcuiccsecretSlot2 = this.m_listNKCUICCSecretSlot[i];
				if (nkcuiccsecretSlot2.IsActive())
				{
					float num2 = Mathf.Abs(this.m_ViewPortCenterX - (nkcuiccsecretSlot2.GetCenterX() - nkcuiccsecretSlot2.GetHalfOfWidth() + (this.m_rectListContent.anchoredPosition.x - this.m_fRectListContentOrgX)));
					if (num2 < num)
					{
						num = num2;
						nkcuiccsecretSlot = nkcuiccsecretSlot2;
					}
				}
			}
			if (nkcuiccsecretSlot != null)
			{
				this.ScrollForCenter(nkcuiccsecretSlot, 0.6f);
			}
		}

		// Token: 0x0600645C RID: 25692 RVA: 0x001FD3C4 File Offset: 0x001FB5C4
		public void ResetBook()
		{
			for (int i = 0; i < this.m_listNKCUICCSecretSlot.Count; i++)
			{
				NKCUICCSecretSlot nkcuiccsecretSlot = this.m_listNKCUICCSecretSlot[i];
				if (nkcuiccsecretSlot != null && nkcuiccsecretSlot.IsActive() && nkcuiccsecretSlot.IsBookOpen())
				{
					nkcuiccsecretSlot.SetBookOpen(false);
					nkcuiccsecretSlot.m_Animator.Play("AB_UI_NKM_UI_COUNTER_CASE_SECRET_SLOT_CLOSE");
				}
			}
			if (this.m_bBookOpen)
			{
				this.m_bBookOpen = false;
				this.m_Animator.Play("AB_UI_NKM_UI_COUNTER_CASE_SECRET_CLOSE");
			}
			NKCUIEpisodeViewerCCS.m_LastStageIndex = -1;
			NKCUIEpisodeViewerCCS.m_ActID = -1;
		}

		// Token: 0x0600645D RID: 25693 RVA: 0x001FD44F File Offset: 0x001FB64F
		public void OnSlotDragStart()
		{
			this.m_NKMTrackingFloat.StopTracking();
			this.ResetBook();
		}

		// Token: 0x0600645E RID: 25694 RVA: 0x001FD462 File Offset: 0x001FB662
		public void OnStartButton()
		{
			NKMEpisodeTempletV2.Find(NKCUIEpisodeViewerCCS.m_EpisodeID, EPISODE_DIFFICULTY.NORMAL);
		}

		// Token: 0x0600645F RID: 25695 RVA: 0x001FD470 File Offset: 0x001FB670
		private void SendUnLockPacket()
		{
			NKMEpisodeTempletV2.Find(NKCUIEpisodeViewerCCS.m_EpisodeID, EPISODE_DIFFICULTY.NORMAL);
		}

		// Token: 0x06006460 RID: 25696 RVA: 0x001FD47E File Offset: 0x001FB67E
		public void SetMissionUI()
		{
			if (NKCUIEpisodeViewerCCS.m_LastStageIndex == -1)
			{
				return;
			}
			this.SetMissionUI(NKCUIEpisodeViewerCCS.m_LastStageIndex);
		}

		// Token: 0x06006461 RID: 25697 RVA: 0x001FD494 File Offset: 0x001FB694
		private void SetMissionUI(int stageIndex)
		{
			NKCUIEpisodeViewerCCS.m_LastStageIndex = stageIndex;
			NKMEpisodeTempletV2.Find(NKCUIEpisodeViewerCCS.m_EpisodeID, EPISODE_DIFFICULTY.NORMAL);
		}

		// Token: 0x06006462 RID: 25698 RVA: 0x001FD4A8 File Offset: 0x001FB6A8
		public void OnMissionSlotSelect0(bool bSet)
		{
			if (!bSet)
			{
				return;
			}
			this.SetMissionUI(0);
		}

		// Token: 0x06006463 RID: 25699 RVA: 0x001FD4B5 File Offset: 0x001FB6B5
		public void OnMissionSlotSelect1(bool bSet)
		{
			if (!bSet)
			{
				return;
			}
			this.SetMissionUI(1);
		}

		// Token: 0x06006464 RID: 25700 RVA: 0x001FD4C2 File Offset: 0x001FB6C2
		public void OnMissionSlotSelect2(bool bSet)
		{
			if (!bSet)
			{
				return;
			}
			this.SetMissionUI(2);
		}

		// Token: 0x06006465 RID: 25701 RVA: 0x001FD4CF File Offset: 0x001FB6CF
		public void OnMissionSlotSelect3(bool bSet)
		{
			if (!bSet)
			{
				return;
			}
			this.SetMissionUI(3);
		}

		// Token: 0x06006466 RID: 25702 RVA: 0x001FD4DC File Offset: 0x001FB6DC
		private void Update()
		{
			if (this.m_bOpen)
			{
				if (this.m_bReserveSnapToGrid)
				{
					this.m_fElapsedTimeReserveSnapToGrid += Time.deltaTime;
					if (this.m_fElapsedTimeReserveSnapToGrid > 0.1f && Mathf.Abs(this.m_SREPList.velocity.x) <= this.MIN_VELOCITY_TO_SNAP_TO_GRID)
					{
						this.m_SREPList.velocity = Vector2.zero;
						this.SnapToGrid();
						this.m_bReserveSnapToGrid = false;
					}
				}
				this.m_NKMTrackingFloat.Update(Time.deltaTime);
				if (this.m_NKMTrackingFloat.IsTracking())
				{
					this.m_rectListContent.anchoredPosition = new Vector2(this.m_NKMTrackingFloat.GetNowValue(), this.m_rectListContent.anchoredPosition.y);
				}
				float num = float.MaxValue;
				NKCUICCSecretSlot nkcuiccsecretSlot = null;
				for (int i = 0; i < this.m_listNKCUICCSecretSlot.Count; i++)
				{
					NKCUICCSecretSlot nkcuiccsecretSlot2 = this.m_listNKCUICCSecretSlot[i];
					if (nkcuiccsecretSlot2.IsActive())
					{
						float num2 = Mathf.Abs(this.m_ViewPortCenterX - (nkcuiccsecretSlot2.GetCenterX() - nkcuiccsecretSlot2.GetHalfOfWidth() + (this.m_rectListContent.anchoredPosition.x - this.m_fRectListContentOrgX)));
						if (num2 < num)
						{
							num = num2;
							nkcuiccsecretSlot = nkcuiccsecretSlot2;
						}
						if (num2 > nkcuiccsecretSlot2.GetHalfOfWidth())
						{
							nkcuiccsecretSlot2.m_RTButton.localScale = new Vector3(0.9f, 0.9f, 1f);
						}
						else
						{
							nkcuiccsecretSlot2.m_RTButton.localScale = new Vector3(0.9f + 0.1f * ((nkcuiccsecretSlot2.GetHalfOfWidth() - num2) / nkcuiccsecretSlot2.GetHalfOfWidth()), 0.9f + 0.1f * ((nkcuiccsecretSlot2.GetHalfOfWidth() - num2) / nkcuiccsecretSlot2.GetHalfOfWidth()), 1f);
						}
					}
				}
				if (nkcuiccsecretSlot != null && this.m_CandidateSlotForCenter != nkcuiccsecretSlot)
				{
					nkcuiccsecretSlot.transform.SetSiblingIndex(this.m_listNKCUICCSecretSlot.Count - 1);
					nkcuiccsecretSlot.ResetPos();
					this.m_CandidateSlotForCenter = nkcuiccsecretSlot;
				}
			}
		}

		// Token: 0x06006467 RID: 25703 RVA: 0x001FD6CA File Offset: 0x001FB8CA
		private void SetRightInfo()
		{
			if (NKCUIEpisodeViewerCCS.m_ActID != -1)
			{
				this.SetRightInfo(NKCUIEpisodeViewerCCS.m_ActID, false);
			}
		}

		// Token: 0x06006468 RID: 25704 RVA: 0x001FD6E0 File Offset: 0x001FB8E0
		private void SetRightInfo(int actID, bool bRefreshComboButtonsState = true)
		{
			this.m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO_CONTENT_TEXT.text = "";
			this.m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO_SUBTITLE_TEXT.text = "";
			NKMEpisodeTempletV2.Find(NKCUIEpisodeViewerCCS.m_EpisodeID, EPISODE_DIFFICULTY.NORMAL);
		}

		// Token: 0x06006469 RID: 25705 RVA: 0x001FD710 File Offset: 0x001FB910
		private void OnSlotSelected(int actID)
		{
			if (this.m_CandidateSlotForCenter != null && this.m_CandidateSlotForCenter.GetActID() == actID && !this.m_CandidateSlotForCenter.IsBookOpen())
			{
				this.m_Animator.Play("AB_UI_NKM_UI_COUNTER_CASE_SECRET");
				this.m_CandidateSlotForCenter.m_Animator.Play("AB_UI_NKM_UI_COUNTER_CASE_SECRET_SLOT_OPEN");
				this.m_CandidateSlotForCenter.SetBookOpen(true);
				this.m_bBookOpen = true;
				NKCUIEpisodeViewerCCS.m_ActID = actID;
				NKCUIEpisodeViewerCCS.m_LastStageIndex = -1;
				this.SetRightInfo(actID, true);
			}
			for (int i = 0; i < this.m_listNKCUICCSecretSlot.Count; i++)
			{
				NKCUICCSecretSlot nkcuiccsecretSlot = this.m_listNKCUICCSecretSlot[i];
				if (!(nkcuiccsecretSlot == this.m_CandidateSlotForCenter) && nkcuiccsecretSlot.IsActive() && nkcuiccsecretSlot.GetActID() == actID)
				{
					this.ResetBook();
					this.ScrollForCenter(nkcuiccsecretSlot, 0.6f);
					return;
				}
			}
		}

		// Token: 0x0600646A RID: 25706 RVA: 0x001FD7E8 File Offset: 0x001FB9E8
		public void Open()
		{
			NKCUIFadeInOut.FadeIn(0.1f, null, false);
			if (NKMEpisodeTempletV2.Find(NKCUIEpisodeViewerCCS.m_EpisodeID, EPISODE_DIFFICULTY.NORMAL) == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_EPISODE_CCS, true);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			if (this.m_bReservedResetBook)
			{
				this.ResetBook();
			}
			this.m_bReservedResetBook = false;
			this.SetRightInfo();
			this.SetMissionUI();
			base.UIOpened(true);
		}

		// Token: 0x0600646B RID: 25707 RVA: 0x001FD84F File Offset: 0x001FBA4F
		public override void CloseInternal()
		{
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_EPISODE_CCS, false);
		}

		// Token: 0x0600646C RID: 25708 RVA: 0x001FD876 File Offset: 0x001FBA76
		public override void OnBackButton()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OPERATION, true);
		}

		// Token: 0x04004FE4 RID: 20452
		private GameObject m_NKM_EPISODE_CCS;

		// Token: 0x04004FE5 RID: 20453
		private static int m_EpisodeID = 0;

		// Token: 0x04004FE6 RID: 20454
		private static int m_ActID = -1;

		// Token: 0x04004FE7 RID: 20455
		private RectTransform m_rectListContent;

		// Token: 0x04004FE8 RID: 20456
		private RectTransform m_rectViewPort;

		// Token: 0x04004FE9 RID: 20457
		private ScrollRect m_SREPList;

		// Token: 0x04004FEA RID: 20458
		public Animator m_Animator;

		// Token: 0x04004FEB RID: 20459
		public Text m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO_TITLE_TEXT;

		// Token: 0x04004FEC RID: 20460
		public Text m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO_SUBTITLE_TEXT;

		// Token: 0x04004FED RID: 20461
		public Text m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO_CONTENT_TEXT;

		// Token: 0x04004FEE RID: 20462
		public GameObject m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO2;

		// Token: 0x04004FEF RID: 20463
		public GameObject m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_DOT;

		// Token: 0x04004FF0 RID: 20464
		public GameObject m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO;

		// Token: 0x04004FF1 RID: 20465
		public List<GameObject> m_lst_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO_BOOK_DISABLE;

		// Token: 0x04004FF2 RID: 20466
		public List<GameObject> m_lst_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO_BOOK_CLEAR;

		// Token: 0x04004FF3 RID: 20467
		public List<NKCUIComToggle> m_lst_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO_BOOK;

		// Token: 0x04004FF4 RID: 20468
		public GameObject m_AB_ICON_SLOT;

		// Token: 0x04004FF5 RID: 20469
		public NKCUISlot m_NKCUISlot;

		// Token: 0x04004FF6 RID: 20470
		public Text m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO2_START_BUTTON_TEXT;

		// Token: 0x04004FF7 RID: 20471
		public GameObject m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO2_START_BUTTON_COST;

		// Token: 0x04004FF8 RID: 20472
		public Text m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO2_START_BUTTON_COST_TEXT;

		// Token: 0x04004FF9 RID: 20473
		public NKCUIComButton m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO2_START_BUTTON;

		// Token: 0x04004FFA RID: 20474
		public NKCUIComToggle m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO_BOOK1;

		// Token: 0x04004FFB RID: 20475
		public NKCUIComToggle m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO_BOOK2;

		// Token: 0x04004FFC RID: 20476
		public NKCUIComToggle m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO_BOOK3;

		// Token: 0x04004FFD RID: 20477
		public NKCUIComToggle m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_INFO_BOOK4;

		// Token: 0x04004FFE RID: 20478
		private float m_ViewPortCenterX;

		// Token: 0x04004FFF RID: 20479
		private float m_fRectListContentOrgX;

		// Token: 0x04005000 RID: 20480
		private bool m_bReserveSnapToGrid;

		// Token: 0x04005001 RID: 20481
		private float m_fElapsedTimeReserveSnapToGrid;

		// Token: 0x04005002 RID: 20482
		private float MIN_VELOCITY_TO_SNAP_TO_GRID = 170f;

		// Token: 0x04005003 RID: 20483
		private List<NKCUICCSecretSlot> m_listNKCUICCSecretSlot = new List<NKCUICCSecretSlot>();

		// Token: 0x04005004 RID: 20484
		private const int DEFAULT_SLOT_COUNT = 10;

		// Token: 0x04005005 RID: 20485
		private NKCUICCSecretSlot m_CandidateSlotForCenter;

		// Token: 0x04005006 RID: 20486
		private NKMTrackingFloat m_NKMTrackingFloat = new NKMTrackingFloat();

		// Token: 0x04005007 RID: 20487
		private bool m_bBookOpen;

		// Token: 0x04005008 RID: 20488
		private static int m_LastStageIndex = -1;

		// Token: 0x04005009 RID: 20489
		private bool m_bReservedResetBook;
	}
}
