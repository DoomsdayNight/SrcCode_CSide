using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007CB RID: 1995
	public class NKCUINewsSlot : MonoBehaviour
	{
		// Token: 0x06004ED5 RID: 20181 RVA: 0x0017C8D4 File Offset: 0x0017AAD4
		public void InitUI()
		{
			this.m_btnNews = base.transform.Find("AB_UI_NKM_UI_NEWS").GetComponent<NKCUIComStateButton>();
			this.m_imgNewsBG = base.transform.Find("AB_UI_NKM_UI_NEWS/NEWS_IMAGE").GetComponent<Image>();
			this.m_objNewsDisable = base.transform.Find("AB_UI_NKM_UI_NEWS/NEWS_DISABLE").gameObject;
			this.m_objNewsTimeCount = base.transform.Find("AB_UI_NKM_UI_NEWS/AB_UI_NKM_UI_NEWS_TIME_COUNT").gameObject;
			this.m_lbNewsTimeCount = base.transform.Find("AB_UI_NKM_UI_NEWS/AB_UI_NKM_UI_NEWS_TIME_COUNT/NEWS_TIME_COUNT_TEXT1").GetComponent<Text>();
			this.m_objNewsSelect = base.transform.Find("AB_UI_NKM_UI_NEWS/AB_UI_NKM_UI_NEWS_SELECT").gameObject;
			this.m_btnNews.PointerClick.RemoveAllListeners();
			this.m_btnNews.PointerClick.AddListener(new UnityAction(this.OnClickSlot));
			this.m_btnNotice = base.transform.Find("AB_UI_NKM_UI_NOTICE").GetComponent<NKCUIComStateButton>();
			this.m_imgNoticeBG = base.transform.Find("AB_UI_NKM_UI_NOTICE/NEWS_IMAGE").GetComponent<Image>();
			this.m_lbNoticeTitle = base.transform.Find("AB_UI_NKM_UI_NOTICE/NEWS_NOTICE_TEXT").GetComponent<Text>();
			this.m_objNoticeDisable = base.transform.Find("AB_UI_NKM_UI_NOTICE/NEWS_DISABLE").gameObject;
			this.m_objNoticeTimeCount = base.transform.Find("AB_UI_NKM_UI_NOTICE/AB_UI_NKM_UI_NEWS_TIME_COUNT_NOTICE").gameObject;
			this.m_lbNoticeTimeCount = base.transform.Find("AB_UI_NKM_UI_NOTICE/AB_UI_NKM_UI_NEWS_TIME_COUNT_NOTICE/NEWS_TIME_COUNT_TEXT1").GetComponent<Text>();
			this.m_objNoticeSelect = base.transform.Find("AB_UI_NKM_UI_NOTICE/AB_UI_NKM_UI_NEWS_SELECT").gameObject;
			this.m_btnNotice.PointerClick.RemoveAllListeners();
			this.m_btnNotice.PointerClick.AddListener(new UnityAction(this.OnClickSlot));
		}

		// Token: 0x06004ED6 RID: 20182 RVA: 0x0017CA98 File Offset: 0x0017AC98
		public void SetData(NKCNewsTemplet newsTemplet, NKCUINewsSlot.OnSlot onClickSlot, NKCUINewsSlot.OnTimeOut onTimeOut)
		{
			this.m_slotIdx = newsTemplet.Idx;
			this.m_eSlotType = newsTemplet.m_FilterType;
			this.m_startDateTime = newsTemplet.m_DateStartUtc;
			this.m_endDateTime = newsTemplet.m_DateEndUtc;
			NKCUtil.SetGameobjectActive(this.m_btnNews.gameObject, newsTemplet.m_FilterType == eNewsFilterType.NEWS);
			NKCUtil.SetGameobjectActive(this.m_btnNotice.gameObject, newsTemplet.m_FilterType == eNewsFilterType.NOTICE);
			this.dOnClickSlot = onClickSlot;
			this.dOnTimeOut = onTimeOut;
			if (newsTemplet.m_FilterType == eNewsFilterType.NEWS)
			{
				this.m_imgNewsBG.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_news_thumbnail", newsTemplet.m_TabImage, false);
				NKCUtil.SetGameobjectActive(this.m_objNewsDisable, true);
				NKCUtil.SetGameobjectActive(this.m_objNewsTimeCount, newsTemplet.m_bDateAlert);
				if (newsTemplet.m_bDateAlert)
				{
					this.m_lbTargetText = this.m_lbNewsTimeCount;
					this.m_time = 0f;
				}
				NKCUtil.SetGameobjectActive(this.m_objNewsSelect, false);
			}
			else if (newsTemplet.m_FilterType == eNewsFilterType.NOTICE)
			{
				this.m_imgNoticeBG.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_news_thumbnail", newsTemplet.m_TabImage, false);
				this.m_lbNoticeTitle.text = newsTemplet.m_Title;
				NKCUtil.SetGameobjectActive(this.m_objNoticeDisable, true);
				NKCUtil.SetGameobjectActive(this.m_objNoticeTimeCount, newsTemplet.m_bDateAlert);
				if (newsTemplet.m_bDateAlert)
				{
					this.m_lbTargetText = this.m_lbNoticeTimeCount;
					this.m_time = 0f;
				}
				NKCUtil.SetGameobjectActive(this.m_objNoticeSelect, false);
			}
			this.m_bShowEndTime = newsTemplet.m_bDateAlert;
			if (this.m_bShowEndTime)
			{
				this.m_lbTargetText.text = NKCUtilString.GetTimeString(this.m_endDateTime, true);
			}
		}

		// Token: 0x06004ED7 RID: 20183 RVA: 0x0017CC30 File Offset: 0x0017AE30
		public void Select(bool bSelect)
		{
			if (this.m_eSlotType == eNewsFilterType.NEWS)
			{
				NKCUtil.SetGameobjectActive(this.m_objNewsDisable, !bSelect);
				NKCUtil.SetGameobjectActive(this.m_objNewsSelect, bSelect);
				return;
			}
			if (this.m_eSlotType == eNewsFilterType.NOTICE)
			{
				NKCUtil.SetGameobjectActive(this.m_objNoticeDisable, !bSelect);
				NKCUtil.SetGameobjectActive(this.m_objNoticeSelect, bSelect);
			}
		}

		// Token: 0x06004ED8 RID: 20184 RVA: 0x0017CC86 File Offset: 0x0017AE86
		public int GetSlotKey()
		{
			return this.m_slotIdx;
		}

		// Token: 0x06004ED9 RID: 20185 RVA: 0x0017CC8E File Offset: 0x0017AE8E
		public void OnClickSlot()
		{
			if (this.dOnClickSlot != null)
			{
				this.dOnClickSlot(this.m_slotIdx);
			}
		}

		// Token: 0x06004EDA RID: 20186 RVA: 0x0017CCAC File Offset: 0x0017AEAC
		private void Update()
		{
			if (!this.m_bShowEndTime)
			{
				return;
			}
			this.m_time += Time.deltaTime;
			if (1f < this.m_time)
			{
				this.m_time -= 1f;
				if (!NKCSynchronizedTime.IsFinished(this.m_endDateTime))
				{
					this.m_lbTargetText.text = NKCUtilString.GetTimeString(this.m_endDateTime, true);
					return;
				}
				this.m_bShowEndTime = false;
				NKCUINewsSlot.OnTimeOut onTimeOut = this.dOnTimeOut;
				if (onTimeOut == null)
				{
					return;
				}
				onTimeOut(true, this.m_eSlotType, this.m_slotIdx);
			}
		}

		// Token: 0x04003E9E RID: 16030
		private const string BG_ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_news_thumbnail";

		// Token: 0x04003E9F RID: 16031
		private const float UPDATE_INTERVAL = 1f;

		// Token: 0x04003EA0 RID: 16032
		[Header("News")]
		public NKCUIComStateButton m_btnNews;

		// Token: 0x04003EA1 RID: 16033
		public Image m_imgNewsBG;

		// Token: 0x04003EA2 RID: 16034
		public GameObject m_objNewsDisable;

		// Token: 0x04003EA3 RID: 16035
		public GameObject m_objNewsTimeCount;

		// Token: 0x04003EA4 RID: 16036
		public Text m_lbNewsTimeCount;

		// Token: 0x04003EA5 RID: 16037
		public GameObject m_objNewsSelect;

		// Token: 0x04003EA6 RID: 16038
		[Header("Notice")]
		public NKCUIComStateButton m_btnNotice;

		// Token: 0x04003EA7 RID: 16039
		public Image m_imgNoticeBG;

		// Token: 0x04003EA8 RID: 16040
		public Text m_lbNoticeTitle;

		// Token: 0x04003EA9 RID: 16041
		public GameObject m_objNoticeDisable;

		// Token: 0x04003EAA RID: 16042
		public GameObject m_objNoticeTimeCount;

		// Token: 0x04003EAB RID: 16043
		public Text m_lbNoticeTimeCount;

		// Token: 0x04003EAC RID: 16044
		public GameObject m_objNoticeSelect;

		// Token: 0x04003EAD RID: 16045
		private NKCUINewsSlot.OnSlot dOnClickSlot;

		// Token: 0x04003EAE RID: 16046
		private NKCUINewsSlot.OnTimeOut dOnTimeOut;

		// Token: 0x04003EAF RID: 16047
		private Text m_lbTargetText;

		// Token: 0x04003EB0 RID: 16048
		private eNewsFilterType m_eSlotType;

		// Token: 0x04003EB1 RID: 16049
		private int m_slotIdx;

		// Token: 0x04003EB2 RID: 16050
		private DateTime m_endDateTime;

		// Token: 0x04003EB3 RID: 16051
		private DateTime m_startDateTime;

		// Token: 0x04003EB4 RID: 16052
		private bool m_bShowEndTime;

		// Token: 0x04003EB5 RID: 16053
		private float m_time;

		// Token: 0x02001487 RID: 5255
		// (Invoke) Token: 0x0600A91E RID: 43294
		public delegate void OnSlot(int index);

		// Token: 0x02001488 RID: 5256
		// (Invoke) Token: 0x0600A922 RID: 43298
		public delegate void OnTimeOut(bool bTimeOut, eNewsFilterType filterType = eNewsFilterType.NEWS, int slotKey = 0);
	}
}
