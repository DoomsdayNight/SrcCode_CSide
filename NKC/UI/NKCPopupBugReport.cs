using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A34 RID: 2612
	public class NKCPopupBugReport : NKCUIBase
	{
		// Token: 0x1700130D RID: 4877
		// (get) Token: 0x0600725F RID: 29279 RVA: 0x00260318 File Offset: 0x0025E518
		public static NKCPopupBugReport Instance
		{
			get
			{
				if (NKCPopupBugReport.m_Instance == null)
				{
					NKCPopupBugReport.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupBugReport>("AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX", "NKM_UI_POPUP_BUG_REPORT", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupBugReport.CleanupInstance)).GetInstance<NKCPopupBugReport>();
					NKCPopupBugReport.m_Instance.InitUI();
				}
				return NKCPopupBugReport.m_Instance;
			}
		}

		// Token: 0x06007260 RID: 29280 RVA: 0x00260367 File Offset: 0x0025E567
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupBugReport.m_Instance != null && NKCPopupBugReport.m_Instance.IsOpen)
			{
				NKCPopupBugReport.m_Instance.Close();
			}
		}

		// Token: 0x06007261 RID: 29281 RVA: 0x0026038C File Offset: 0x0025E58C
		private static void CleanupInstance()
		{
			NKCPopupBugReport.m_Instance = null;
		}

		// Token: 0x1700130E RID: 4878
		// (get) Token: 0x06007262 RID: 29282 RVA: 0x00260394 File Offset: 0x0025E594
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700130F RID: 4879
		// (get) Token: 0x06007263 RID: 29283 RVA: 0x00260397 File Offset: 0x0025E597
		public override string MenuName
		{
			get
			{
				return "BugReport";
			}
		}

		// Token: 0x06007264 RID: 29284 RVA: 0x0026039E File Offset: 0x0025E59E
		public override void OnBackButton()
		{
			base.OnBackButton();
		}

		// Token: 0x06007265 RID: 29285 RVA: 0x002603A6 File Offset: 0x0025E5A6
		public void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06007266 RID: 29286 RVA: 0x002603C8 File Offset: 0x0025E5C8
		public void Open()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			NKCUIComStateButton ok = this.m_ok;
			if (ok != null)
			{
				ok.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton ok2 = this.m_ok;
			if (ok2 != null)
			{
				ok2.PointerClick.AddListener(new UnityAction(this.RequestSendReport));
			}
			NKCUIComStateButton cancel = this.m_cancel;
			if (cancel != null)
			{
				cancel.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton cancel2 = this.m_cancel;
			if (cancel2 != null)
			{
				cancel2.PointerClick.AddListener(new UnityAction(base.Close));
			}
			if (this.m_inputField != null)
			{
				this.m_inputField.text = "";
				this.m_inputField.keyboardType = TouchScreenKeyboardType.Default;
				this.m_inputField.contentType = InputField.ContentType.Standard;
			}
			NKCUtil.SetLabelText(this.m_titleText, NKCStringTable.GetString("SI_PF_BUG_REPORT_POPUP_TITLE", false));
			NKCUtil.SetLabelText(this.m_descriptionText, NKCStringTable.GetString("SI_PF_BUG_REPORT_POPUP_DESC", false));
			base.UIOpened(true);
		}

		// Token: 0x06007267 RID: 29287 RVA: 0x002604C5 File Offset: 0x0025E6C5
		private void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x06007268 RID: 29288 RVA: 0x002604DA File Offset: 0x0025E6DA
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06007269 RID: 29289 RVA: 0x002604E8 File Offset: 0x0025E6E8
		private void RequestSendReport()
		{
			if (string.IsNullOrEmpty(this.m_inputField.text))
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString("SI_PF_BUG_REPORT_POPUP_NOTICE_INPUT", false), null, "");
				return;
			}
			NKCPopupOKCancel.OpenOKCancelBox(NKCStringTable.GetString("SI_PF_BUG_REPORT_POPUP_TITLE", false), NKCStringTable.GetString("SI_PF_BUG_REPORT_POPUP_OK_DESC", false), new NKCPopupOKCancel.OnButton(this.SendReport), null, false);
		}

		// Token: 0x0600726A RID: 29290 RVA: 0x0026054C File Offset: 0x0025E74C
		private void SendReport()
		{
			NKCReportManager.SendReport(this.m_inputField.text, true, false);
			base.Close();
		}

		// Token: 0x04005E46 RID: 24134
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX";

		// Token: 0x04005E47 RID: 24135
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_BUG_REPORT";

		// Token: 0x04005E48 RID: 24136
		private static NKCPopupBugReport m_Instance;

		// Token: 0x04005E49 RID: 24137
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x04005E4A RID: 24138
		public NKCUIComStateButton m_ok;

		// Token: 0x04005E4B RID: 24139
		public NKCUIComStateButton m_cancel;

		// Token: 0x04005E4C RID: 24140
		public Text m_titleText;

		// Token: 0x04005E4D RID: 24141
		public Text m_descriptionText;

		// Token: 0x04005E4E RID: 24142
		public InputField m_inputField;
	}
}
