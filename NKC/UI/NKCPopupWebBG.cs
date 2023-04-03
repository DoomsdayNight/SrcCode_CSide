using System;
using Cs.Logging;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI
{
	// Token: 0x02000A92 RID: 2706
	public class NKCPopupWebBG : NKCUIBase
	{
		// Token: 0x1700140B RID: 5131
		// (get) Token: 0x060077C8 RID: 30664 RVA: 0x0027CE00 File Offset: 0x0027B000
		public static NKCPopupWebBG Instance
		{
			get
			{
				if (NKCPopupWebBG.m_Instance == null)
				{
					NKCPopupWebBG.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupWebBG>("AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX", "NKM_UI_POPUP_WEB_BG", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupWebBG.CleanupInstance)).GetInstance<NKCPopupWebBG>();
					NKCPopupWebBG.m_Instance.InitUI();
				}
				return NKCPopupWebBG.m_Instance;
			}
		}

		// Token: 0x060077C9 RID: 30665 RVA: 0x0027CE4F File Offset: 0x0027B04F
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupWebBG.m_Instance != null && NKCPopupWebBG.m_Instance.IsOpen)
			{
				NKCPopupWebBG.m_Instance.Close();
			}
		}

		// Token: 0x060077CA RID: 30666 RVA: 0x0027CE74 File Offset: 0x0027B074
		private static void CleanupInstance()
		{
			NKCPopupWebBG.m_Instance = null;
		}

		// Token: 0x1700140C RID: 5132
		// (get) Token: 0x060077CB RID: 30667 RVA: 0x0027CE7C File Offset: 0x0027B07C
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700140D RID: 5133
		// (get) Token: 0x060077CC RID: 30668 RVA: 0x0027CE7F File Offset: 0x0027B07F
		public override string MenuName
		{
			get
			{
				return "PopupWebBG";
			}
		}

		// Token: 0x060077CD RID: 30669 RVA: 0x0027CE88 File Offset: 0x0027B088
		public void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			this.m_BtnClose.PointerClick.RemoveAllListeners();
			this.m_BtnClose.PointerClick.AddListener(new UnityAction(this.OnClickClose));
			this.m_BtnBG.PointerClick.RemoveAllListeners();
			this.m_BtnBG.PointerClick.AddListener(new UnityAction(this.OnClickClose));
			NKCUtil.SetGameobjectActive(this.m_WebViewAreaObject, false);
			if (NKCDefineManager.DEFINE_UNITY_EDITOR())
			{
				NKCUtil.SetGameobjectActive(this.m_WebViewAreaObject, true);
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060077CE RID: 30670 RVA: 0x0027CF29 File Offset: 0x0027B129
		public void OnClickClose()
		{
			Log.Debug("PopupWebBG - OnClickClose", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Popup/NKCPopupWebBG.cs", 75);
			base.Close();
		}

		// Token: 0x060077CF RID: 30671 RVA: 0x0027CF44 File Offset: 0x0027B144
		public void Open(int marginX, int marginYTop, int marginYBottom, int nxpWebwidth, int nxpWebheight, NKCPopupWebBG.OnCloseWeb dOnCloseWeb)
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			this.m_dOnCloseWeb = dOnCloseWeb;
			RectTransform component = base.gameObject.GetComponent<RectTransform>();
			Log.Debug(string.Format("PopupWebBG - screenSize[{0}, {1}]", Screen.width, Screen.height), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Popup/NKCPopupWebBG.cs", 90);
			Log.Debug(string.Format("PopupWebBG - safescreenSize[{0}, {1}]", Screen.safeArea.width, Screen.safeArea.height), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Popup/NKCPopupWebBG.cs", 91);
			Log.Debug(string.Format("PopupWebBG - stretchedscreenSize[{0}, {1}]", component.GetWidth(), component.GetHeight()), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Popup/NKCPopupWebBG.cs", 92);
			Log.Debug(string.Format("PopupWebBG - WebSize[{0}, {1}]", nxpWebwidth, nxpWebheight), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Popup/NKCPopupWebBG.cs", 93);
			Log.Debug(string.Format("PopupWebBG - marginX[{0}], marginYTop[{1}], marginYBottom[{2}]", marginX, marginYTop, marginYBottom), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Popup/NKCPopupWebBG.cs", 94);
			float num = component.GetHeight() / (float)Screen.height;
			if (this.m_WebViewAreaObject != null)
			{
				RectTransform component2 = this.m_WebViewAreaObject.GetComponent<RectTransform>();
				component2.offsetMin = new Vector2((float)marginX, (float)marginYBottom);
				component2.offsetMax = new Vector2((float)(-(float)marginX), (float)(-(float)marginYTop));
			}
			if (this.m_TopBarObject != null)
			{
				RectTransform component3 = this.m_TopBarObject.GetComponent<RectTransform>();
				component3.offsetMin = new Vector2((float)marginX, component3.offsetMin.y);
				component3.offsetMax = new Vector2((float)(-(float)marginX), component3.offsetMax.y);
				component3.SetHeight((float)NKCPopupWebBG.m_topBarHeight);
				component3.anchoredPosition = new Vector2(component3.anchoredPosition.x, -((float)marginYTop * num));
			}
			base.UIOpened(true);
		}

		// Token: 0x060077D0 RID: 30672 RVA: 0x0027D118 File Offset: 0x0027B318
		public static void CalculateMarginSizeForNXPWeb(out int marginX, out int marginYTop, out int marginYBottom, out int webWidth, out int webHeight)
		{
			int height = Screen.height;
			int num = 177;
			int num2 = 10;
			webHeight = height - NKCPopupWebBG.m_topBarHeight - num2 * 2;
			int num3 = webHeight * num / 100;
			if (Screen.width < num3)
			{
				webHeight = Screen.width / num * 100;
				num3 = Screen.width;
			}
			webWidth = num3;
			int num4 = (Screen.height - NKCPopupWebBG.m_topBarHeight - webHeight) / 2;
			marginX = (Screen.width - webWidth) / 2;
			marginYTop = NKCPopupWebBG.m_topBarHeight + num4;
			marginYBottom = num4;
		}

		// Token: 0x060077D1 RID: 30673 RVA: 0x0027D196 File Offset: 0x0027B396
		private void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x060077D2 RID: 30674 RVA: 0x0027D1AB File Offset: 0x0027B3AB
		public override void CloseInternal()
		{
			Log.Debug("PopupWebBG - CloseInternal", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Popup/NKCPopupWebBG.cs", 164);
			NKCPopupWebBG.OnCloseWeb dOnCloseWeb = this.m_dOnCloseWeb;
			if (dOnCloseWeb != null)
			{
				dOnCloseWeb();
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x04006466 RID: 25702
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX";

		// Token: 0x04006467 RID: 25703
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_WEB_BG";

		// Token: 0x04006468 RID: 25704
		private static NKCPopupWebBG m_Instance;

		// Token: 0x04006469 RID: 25705
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x0400646A RID: 25706
		public static int m_topBarHeight = 100;

		// Token: 0x0400646B RID: 25707
		public NKCUIComStateButton m_BtnClose;

		// Token: 0x0400646C RID: 25708
		public NKCUIComStateButton m_BtnBG;

		// Token: 0x0400646D RID: 25709
		public GameObject m_TopBarObject;

		// Token: 0x0400646E RID: 25710
		public GameObject m_WebViewAreaObject;

		// Token: 0x0400646F RID: 25711
		private NKCPopupWebBG.OnCloseWeb m_dOnCloseWeb;

		// Token: 0x020017EE RID: 6126
		// (Invoke) Token: 0x0600B4A4 RID: 46244
		public delegate void OnCloseWeb();
	}
}
