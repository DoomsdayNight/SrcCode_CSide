using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009BF RID: 2495
	public class NKCUINews : NKCUIBase
	{
		// Token: 0x17001239 RID: 4665
		// (get) Token: 0x06006A18 RID: 27160 RVA: 0x002269D8 File Offset: 0x00224BD8
		public static NKCUINews Instance
		{
			get
			{
				if (NKCUINews.m_Instance == null)
				{
					NKCUINews.m_Instance = NKCUIManager.OpenNewInstance<NKCUINews>("ab_ui_nkm_ui_news", "NKM_UI_NEWS", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUINews.CleanupInstance)).GetInstance<NKCUINews>();
					NKCUINews.m_Instance.InitUI();
				}
				return NKCUINews.m_Instance;
			}
		}

		// Token: 0x06006A19 RID: 27161 RVA: 0x00226A27 File Offset: 0x00224C27
		private static void CleanupInstance()
		{
			NKCUINews.m_Instance = null;
		}

		// Token: 0x1700123A RID: 4666
		// (get) Token: 0x06006A1A RID: 27162 RVA: 0x00226A2F File Offset: 0x00224C2F
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUINews.m_Instance != null && NKCUINews.m_Instance.IsOpen;
			}
		}

		// Token: 0x1700123B RID: 4667
		// (get) Token: 0x06006A1B RID: 27163 RVA: 0x00226A4A File Offset: 0x00224C4A
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700123C RID: 4668
		// (get) Token: 0x06006A1C RID: 27164 RVA: 0x00226A4D File Offset: 0x00224C4D
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_NEWS;
			}
		}

		// Token: 0x06006A1D RID: 27165 RVA: 0x00226A54 File Offset: 0x00224C54
		private void InitUI()
		{
			Transform transform = base.transform.Find("AB_UI_NKM_UI_NEWS_BG2/AB_UI_NKM_UI_NEWS_CONTENT");
			this.m_tglTopMenuNews = transform.Find("AB_UI_NKM_UI_NEWS_TOGGLE_TOP_MENU/AB_UI_NKM_UI_NEWS_TAP_TOP_MENU_NEWS").GetComponent<NKCUIComToggle>();
			this.m_tglTopMenuNews.OnValueChanged.RemoveAllListeners();
			this.m_tglTopMenuNews.OnValueChanged.AddListener(new UnityAction<bool>(this.OnTglNews));
			this.m_tglTopMenuNotice = transform.Find("AB_UI_NKM_UI_NEWS_TOGGLE_TOP_MENU/AB_UI_NKM_UI_NEWS_TAP_TOP_MENU_NOTICE").GetComponent<NKCUIComToggle>();
			this.m_tglTopMenuNotice.OnValueChanged.RemoveAllListeners();
			this.m_tglTopMenuNotice.OnValueChanged.AddListener(new UnityAction<bool>(this.OnTglNotice));
			this.m_btnClose = transform.Find("AB_UI_NKM_UI_NEWS_BUTTON_CLOSE").GetComponent<NKCUIComStateButton>();
			this.m_loopScrollRectSlot = transform.Find("AB_UI_NKM_UI_NEWS_CONTENT_SCROLL_VIEW_BANNER").GetComponent<LoopVerticalScrollRect>();
			this.m_trSlotParent = transform.Find("AB_UI_NKM_UI_NEWS_CONTENT_SCROLL_VIEW_BANNER/AB_UI_NKM_UI_NEWS_CONTENT_VIEWPORT_BANNER");
			this.m_btnTodayClose = transform.Find("AB_UI_NKM_UI_NEWS_BUTTON_TODAYCLOSE").GetComponent<NKCUIComStateButton>();
			this.m_btnShortCut = transform.Find("AB_UI_NKM_UI_NEWS_BUTTON_SHORTCUT").GetComponent<NKCUIComStateButton>();
			this.m_btnShortCut.PointerClick.RemoveAllListeners();
			this.m_btnShortCut.PointerClick.AddListener(new UnityAction(this.OnClickShortCut));
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_btnTodayClose.PointerClick.RemoveAllListeners();
			this.m_btnTodayClose.PointerClick.AddListener(new UnityAction(this.OnClickTodayClose));
			this.m_eCurrentFilterType = eNewsFilterType.NONE;
			this.m_loopScrollRectSlot.dOnGetObject += this.GetObject;
			this.m_loopScrollRectSlot.dOnReturnObject += this.ReturnObject;
			this.m_loopScrollRectSlot.dOnProvideData += this.ProvideData;
			this.m_loopScrollRectSlot.ContentConstraintCount = 1;
			this.m_loopScrollRectSlot.PrepareCells(0);
			NKCUtil.SetScrollHotKey(this.m_loopScrollRectSlot, null);
			this.m_btnBG.PointerClick.RemoveAllListeners();
			this.m_btnBG.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_bInitComplete = true;
		}

		// Token: 0x06006A1E RID: 27166 RVA: 0x00226C88 File Offset: 0x00224E88
		public void SetDataAndOpen(bool bForceRefresh = false, eNewsFilterType reservedFilterType = eNewsFilterType.NOTICE, int reservedSlotKey = -1)
		{
			if (!this.m_bInitComplete)
			{
				this.InitUI();
			}
			this.m_reservedSlotKey = reservedSlotKey;
			if (bForceRefresh || this.m_lstNewsData.Count == 0)
			{
				this.m_eCurrentFilterType = eNewsFilterType.NONE;
				NKCNewsManager.SortByFilterType(NKCSynchronizedTime.GetServerUTCTime(0.0), out this.m_lstNewsData, out this.m_lstNoticeData);
				if (this.m_lstNewsData.Count == 0 && this.m_lstNoticeData.Count > 0)
				{
					reservedFilterType = eNewsFilterType.NOTICE;
				}
				else if (this.m_lstNewsData.Count > 0 && this.m_lstNoticeData.Count == 0)
				{
					reservedFilterType = eNewsFilterType.NEWS;
				}
			}
			base.UIOpened(true);
			this.OnClickTopMenu(reservedFilterType);
		}

		// Token: 0x06006A1F RID: 27167 RVA: 0x00226D2C File Offset: 0x00224F2C
		public override void CloseInternal()
		{
			this.m_eCurrentFilterType = eNewsFilterType.NONE;
			this.m_currentSelectSlotKey = -1;
			this.m_reservedSlotKey = -1;
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			if (this.m_CloseCallback != null)
			{
				this.m_CloseCallback();
				this.m_CloseCallback = null;
			}
		}

		// Token: 0x06006A20 RID: 27168 RVA: 0x00226D6C File Offset: 0x00224F6C
		private void ShowSubUI(int slotKey)
		{
			NKCNewsTemplet newsTemplet = NKCNewsManager.GetNewsTemplet(slotKey);
			this.m_subUI.SetData(this.m_eCurrentFilterType, newsTemplet);
			if (newsTemplet != null)
			{
				NKCUtil.SetGameobjectActive(this.m_btnShortCut.gameObject, newsTemplet.m_ShortCutType > NKM_SHORTCUT_TYPE.SHORTCUT_NONE);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_btnShortCut.gameObject, false);
		}

		// Token: 0x06006A21 RID: 27169 RVA: 0x00226DC0 File Offset: 0x00224FC0
		private int GetFirstSlotKey()
		{
			if (this.m_reservedSlotKey >= 0)
			{
				return this.m_reservedSlotKey;
			}
			if (this.m_eCurrentFilterType == eNewsFilterType.NEWS && this.m_lstNewsData.Count > 0)
			{
				return this.m_lstNewsData[0].Idx;
			}
			if (this.m_eCurrentFilterType == eNewsFilterType.NOTICE && this.m_lstNoticeData.Count > 0)
			{
				return this.m_lstNoticeData[0].Idx;
			}
			return -1;
		}

		// Token: 0x06006A22 RID: 27170 RVA: 0x00226E30 File Offset: 0x00225030
		public void SetCloseCallback(Action closeCallback)
		{
			this.m_CloseCallback = closeCallback;
		}

		// Token: 0x06006A23 RID: 27171 RVA: 0x00226E3C File Offset: 0x0022503C
		private void OnClickTopMenu(eNewsFilterType filterType)
		{
			if (this.m_eCurrentFilterType == filterType)
			{
				return;
			}
			this.m_eCurrentFilterType = filterType;
			if (this.m_eCurrentFilterType == eNewsFilterType.NEWS)
			{
				this.m_loopScrollRectSlot.TotalCount = this.m_lstNewsData.Count;
				this.m_tglTopMenuNews.Select(true, true, true);
			}
			else if (this.m_eCurrentFilterType == eNewsFilterType.NOTICE)
			{
				this.m_loopScrollRectSlot.TotalCount = this.m_lstNoticeData.Count;
				this.m_tglTopMenuNotice.Select(true, true, true);
			}
			this.m_loopScrollRectSlot.RefreshCells(false);
			if (this.m_loopScrollRectSlot.TotalCount > 0)
			{
				int firstSlotKey = this.GetFirstSlotKey();
				this.m_reservedSlotKey = -1;
				this.OnClickSlot(firstSlotKey);
				return;
			}
			this.OnClickSlot(-1);
		}

		// Token: 0x06006A24 RID: 27172 RVA: 0x00226EEF File Offset: 0x002250EF
		private void OnTglNews(bool bSelect)
		{
			this.OnClickTopMenu(eNewsFilterType.NEWS);
		}

		// Token: 0x06006A25 RID: 27173 RVA: 0x00226EF8 File Offset: 0x002250F8
		private void OnTglNotice(bool bSelect)
		{
			this.OnClickTopMenu(eNewsFilterType.NOTICE);
		}

		// Token: 0x06006A26 RID: 27174 RVA: 0x00226F04 File Offset: 0x00225104
		private void OnClickSlot(int key)
		{
			if (this.m_currentSelectSlotKey == key)
			{
				return;
			}
			for (int i = 0; i < this.m_lstNewsSlot.Count; i++)
			{
				this.m_lstNewsSlot[i].Select(this.m_lstNewsSlot[i].GetSlotKey() == key);
			}
			this.m_currentSelectSlotKey = key;
			this.ShowSubUI(this.m_currentSelectSlotKey);
		}

		// Token: 0x06006A27 RID: 27175 RVA: 0x00226F6C File Offset: 0x0022516C
		private void OnClickTodayClose()
		{
			PlayerPrefs.SetString(NKCNewsManager.GetPreferenceString(NKCNewsManager.NKM_LOCAL_SAVE_NEXT_NEWS_POPUP_SHOW_TIME), NKMTime.GetNextResetTime(NKCSynchronizedTime.GetServerUTCTime(0.0), NKMTime.TimePeriod.Day).Ticks.ToString());
			base.Close();
		}

		// Token: 0x06006A28 RID: 27176 RVA: 0x00226FB4 File Offset: 0x002251B4
		private void OnClickShortCut()
		{
			NKCNewsTemplet newsTemplet = NKCNewsManager.GetNewsTemplet(this.m_currentSelectSlotKey);
			if (newsTemplet == null)
			{
				return;
			}
			NKCContentManager.MoveToShortCut(newsTemplet.m_ShortCutType, newsTemplet.m_ShortCut, false);
			base.Close();
		}

		// Token: 0x06006A29 RID: 27177 RVA: 0x00226FEC File Offset: 0x002251EC
		private RectTransform GetObject(int index)
		{
			if (this.m_stkIdleNewsSlot.Count > 0)
			{
				NKCUINewsSlot nkcuinewsSlot = this.m_stkIdleNewsSlot.Pop();
				NKCUtil.SetGameobjectActive(nkcuinewsSlot, true);
				this.m_lstNewsSlot.Add(nkcuinewsSlot);
				return nkcuinewsSlot.GetComponent<RectTransform>();
			}
			NKCUINewsSlot nkcuinewsSlot2 = UnityEngine.Object.Instantiate<NKCUINewsSlot>(this.m_pfbNewsSlot);
			nkcuinewsSlot2.InitUI();
			nkcuinewsSlot2.transform.localScale = Vector3.one;
			nkcuinewsSlot2.transform.localPosition = Vector3.zero;
			nkcuinewsSlot2.gameObject.AddComponent<CanvasGroup>();
			NKCUtil.SetGameobjectActive(nkcuinewsSlot2, true);
			this.m_lstNewsSlot.Add(nkcuinewsSlot2);
			return nkcuinewsSlot2.GetComponent<RectTransform>();
		}

		// Token: 0x06006A2A RID: 27178 RVA: 0x00227084 File Offset: 0x00225284
		private void ReturnObject(Transform go)
		{
			NKCUtil.SetGameobjectActive(go, false);
			go.SetParent(base.transform);
			NKCUINewsSlot component = go.GetComponent<NKCUINewsSlot>();
			if (this.m_lstNewsSlot.Contains(component))
			{
				this.m_lstNewsSlot.Remove(component);
			}
			if (!this.m_stkIdleNewsSlot.Contains(component))
			{
				this.m_stkIdleNewsSlot.Push(component);
			}
		}

		// Token: 0x06006A2B RID: 27179 RVA: 0x002270E0 File Offset: 0x002252E0
		private void ProvideData(Transform transform, int idx)
		{
			NKCUINewsSlot component = transform.GetComponent<NKCUINewsSlot>();
			if (this.m_eCurrentFilterType != eNewsFilterType.NEWS)
			{
				if (this.m_eCurrentFilterType == eNewsFilterType.NOTICE)
				{
					if (this.m_lstNoticeData.Count > idx)
					{
						component.SetData(this.m_lstNoticeData[idx], new NKCUINewsSlot.OnSlot(this.OnClickSlot), new NKCUINewsSlot.OnTimeOut(this.SetDataAndOpen));
						return;
					}
					NKCUtil.SetGameobjectActive(component, false);
				}
				return;
			}
			if (this.m_lstNewsData.Count > idx)
			{
				component.SetData(this.m_lstNewsData[idx], new NKCUINewsSlot.OnSlot(this.OnClickSlot), new NKCUINewsSlot.OnTimeOut(this.SetDataAndOpen));
				return;
			}
			NKCUtil.SetGameobjectActive(component, false);
		}

		// Token: 0x040055CE RID: 21966
		private const string UI_ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_news";

		// Token: 0x040055CF RID: 21967
		private const string UI_ASSET_NAME = "NKM_UI_NEWS";

		// Token: 0x040055D0 RID: 21968
		private static NKCUINews m_Instance;

		// Token: 0x040055D1 RID: 21969
		[Header("탑 메뉴")]
		public NKCUIComToggle m_tglTopMenuNews;

		// Token: 0x040055D2 RID: 21970
		public NKCUIComToggle m_tglTopMenuNotice;

		// Token: 0x040055D3 RID: 21971
		public NKCUIComStateButton m_btnClose;

		// Token: 0x040055D4 RID: 21972
		[Header("좌측 슬롯")]
		public LoopVerticalScrollRect m_loopScrollRectSlot;

		// Token: 0x040055D5 RID: 21973
		public Transform m_trSlotParent;

		// Token: 0x040055D6 RID: 21974
		public NKCUINewsSlot m_pfbNewsSlot;

		// Token: 0x040055D7 RID: 21975
		[Header("우측 화면")]
		public NKCUINewsSubUI m_subUI;

		// Token: 0x040055D8 RID: 21976
		[Header("하단 바")]
		public NKCUIComStateButton m_btnTodayClose;

		// Token: 0x040055D9 RID: 21977
		public NKCUIComStateButton m_btnShortCut;

		// Token: 0x040055DA RID: 21978
		[Header("배경 버튼")]
		public NKCUIComStateButton m_btnBG;

		// Token: 0x040055DB RID: 21979
		private eNewsFilterType m_eCurrentFilterType;

		// Token: 0x040055DC RID: 21980
		private List<NKCUINewsSlot> m_lstNewsSlot = new List<NKCUINewsSlot>();

		// Token: 0x040055DD RID: 21981
		private Stack<NKCUINewsSlot> m_stkIdleNewsSlot = new Stack<NKCUINewsSlot>();

		// Token: 0x040055DE RID: 21982
		private List<NKCNewsTemplet> m_lstNewsData = new List<NKCNewsTemplet>();

		// Token: 0x040055DF RID: 21983
		private List<NKCNewsTemplet> m_lstNoticeData = new List<NKCNewsTemplet>();

		// Token: 0x040055E0 RID: 21984
		private int m_currentSelectSlotKey;

		// Token: 0x040055E1 RID: 21985
		private int m_reservedSlotKey = -1;

		// Token: 0x040055E2 RID: 21986
		private bool m_bInitComplete;

		// Token: 0x040055E3 RID: 21987
		private Action m_CloseCallback;
	}
}
