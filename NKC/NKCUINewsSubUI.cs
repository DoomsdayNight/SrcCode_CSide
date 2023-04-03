using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007CC RID: 1996
	public class NKCUINewsSubUI : MonoBehaviour
	{
		// Token: 0x06004EDC RID: 20188 RVA: 0x0017CD44 File Offset: 0x0017AF44
		private void InitUI()
		{
			this.m_imgThumbnail = base.transform.Find("AB_UI_NKM_UI_NEWS_CONTENT_SCROLL_VIEW_MAIN2/AB_UI_NKM_UI_NEWS_CONTENT_VIEWPORT_MAIN/NEWS_BANNER_THUMBNAIL").GetComponent<Image>();
			this.m_objTimeCount = this.m_imgThumbnail.transform.Find("AB_UI_NKM_UI_NEWS_CONTENT_TIME_COUNT").gameObject;
			this.m_lbTimeCount = this.m_objTimeCount.transform.Find("AB_UI_NKM_UI_NEWS_BUTTON_TEXT1").GetComponent<Text>();
			this.m_lbDesc = base.transform.Find("AB_UI_NKM_UI_NEWS_CONTENT_SCROLL_VIEW_MAIN2/AB_UI_NKM_UI_NEWS_CONTENT_VIEWPORT_MAIN/NEWS_MAIN_TEXT").GetComponent<Text>();
		}

		// Token: 0x06004EDD RID: 20189 RVA: 0x0017CDC8 File Offset: 0x0017AFC8
		public void SetData(eNewsFilterType filterType, NKCNewsTemplet newsTemplet)
		{
			NKCUtil.SetGameobjectActive(this.m_objEmpty, newsTemplet == null);
			if (newsTemplet != null)
			{
				NKCUtil.SetGameobjectActive(this.m_imgThumbnail, true);
				NKCUtil.SetGameobjectActive(this.m_lbDesc, true);
				this.m_imgThumbnail.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_news_thumbnail", newsTemplet.m_BannerImage, false);
				this.m_lbDesc.text = newsTemplet.m_Contents;
				this.m_endDateTime = newsTemplet.m_DateEndUtc;
				this.m_bShowEndTime = newsTemplet.m_bDateAlert;
				if (this.m_bShowEndTime)
				{
					NKCUtil.SetGameobjectActive(this.m_objTimeCount, true);
					this.m_lbTimeCount.text = NKCUtilString.GetTimeString(newsTemplet.m_DateEndUtc, true);
					this.m_time = 0f;
					return;
				}
				NKCUtil.SetGameobjectActive(this.m_objTimeCount, false);
				return;
			}
			else
			{
				this.m_bShowEndTime = false;
				NKCUtil.SetGameobjectActive(this.m_imgThumbnail, false);
				NKCUtil.SetGameobjectActive(this.m_lbDesc, false);
				if (filterType == eNewsFilterType.NEWS)
				{
					this.m_lbEmpty.text = NKCUtilString.GET_STRING_NEWS_DOES_NOT_HAVE_NEWS;
					return;
				}
				this.m_lbEmpty.text = NKCUtilString.GET_STRING_NEWS_DOES_NOT_HAVE_NOTICE;
				return;
			}
		}

		// Token: 0x06004EDE RID: 20190 RVA: 0x0017CED0 File Offset: 0x0017B0D0
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
				this.m_lbTimeCount.text = NKCUtilString.GetTimeString(this.m_endDateTime, true);
			}
		}

		// Token: 0x04003EB6 RID: 16054
		private const string BANNER_ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_news_thumbnail";

		// Token: 0x04003EB7 RID: 16055
		private const float UPDATE_INTERVAL = 1f;

		// Token: 0x04003EB8 RID: 16056
		public Image m_imgThumbnail;

		// Token: 0x04003EB9 RID: 16057
		public GameObject m_objTimeCount;

		// Token: 0x04003EBA RID: 16058
		public Text m_lbTimeCount;

		// Token: 0x04003EBB RID: 16059
		public Text m_lbDesc;

		// Token: 0x04003EBC RID: 16060
		public GameObject m_objEmpty;

		// Token: 0x04003EBD RID: 16061
		public Text m_lbEmpty;

		// Token: 0x04003EBE RID: 16062
		private DateTime m_endDateTime;

		// Token: 0x04003EBF RID: 16063
		private bool m_bShowEndTime;

		// Token: 0x04003EC0 RID: 16064
		private float m_time;
	}
}
