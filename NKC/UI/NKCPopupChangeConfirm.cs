using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A35 RID: 2613
	public class NKCPopupChangeConfirm : NKCUIBase
	{
		// Token: 0x17001310 RID: 4880
		// (get) Token: 0x0600726C RID: 29292 RVA: 0x00260570 File Offset: 0x0025E770
		public static NKCPopupChangeConfirm Instance
		{
			get
			{
				if (NKCPopupChangeConfirm.m_Instance == null)
				{
					NKCPopupChangeConfirm.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupChangeConfirm>("AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX", "NKM_UI_POPUP_CHANGE_CONFIRM", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupChangeConfirm.CleanupInstance)).GetInstance<NKCPopupChangeConfirm>();
					if (NKCPopupChangeConfirm.m_Instance != null)
					{
						NKCPopupChangeConfirm.m_Instance.InitUI();
					}
				}
				return NKCPopupChangeConfirm.m_Instance;
			}
		}

		// Token: 0x0600726D RID: 29293 RVA: 0x002605D1 File Offset: 0x0025E7D1
		private static void CleanupInstance()
		{
			NKCPopupChangeConfirm.m_Instance = null;
		}

		// Token: 0x17001311 RID: 4881
		// (get) Token: 0x0600726E RID: 29294 RVA: 0x002605D9 File Offset: 0x0025E7D9
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001312 RID: 4882
		// (get) Token: 0x0600726F RID: 29295 RVA: 0x002605DC File Offset: 0x0025E7DC
		public override string MenuName
		{
			get
			{
				return "변경 확인 팝업";
			}
		}

		// Token: 0x06007270 RID: 29296 RVA: 0x002605E3 File Offset: 0x0025E7E3
		public void InitUI()
		{
		}

		// Token: 0x06007271 RID: 29297 RVA: 0x002605E5 File Offset: 0x0025E7E5
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x17001313 RID: 4883
		// (get) Token: 0x06007272 RID: 29298 RVA: 0x002605F3 File Offset: 0x0025E7F3
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupChangeConfirm.m_Instance != null && NKCPopupChangeConfirm.m_Instance.IsOpen;
			}
		}

		// Token: 0x06007273 RID: 29299 RVA: 0x0026060E File Offset: 0x0025E80E
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupChangeConfirm.m_Instance != null && NKCPopupChangeConfirm.m_Instance.IsOpen)
			{
				NKCPopupChangeConfirm.m_Instance.Close();
			}
		}

		// Token: 0x06007274 RID: 29300 RVA: 0x00260634 File Offset: 0x0025E834
		public void Open(string title, string before, string after, string desc, UnityAction ok, UnityAction cancel = null)
		{
			NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_CHANGE_CONFIRM_TOP_TEXT, title);
			NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_CHANGE_CONFIRM_TEXT_BEFORE, before);
			NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_CHANGE_CONFIRM_TEXT_AFTER, after);
			NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_CHANGE_CONFIRM_TEXT, desc);
			this.m_NKM_UI_POPUP_OK_CANCEL_BOX_OK.PointerClick.RemoveAllListeners();
			if (ok != null)
			{
				this.m_NKM_UI_POPUP_OK_CANCEL_BOX_OK.PointerClick.AddListener(ok);
			}
			this.m_NKM_UI_POPUP_OK_CANCEL_BOX_OK.PointerClick.AddListener(new UnityAction(NKCPopupChangeConfirm.CheckInstanceAndClose));
			NKCUtil.SetHotkey(this.m_NKM_UI_POPUP_OK_CANCEL_BOX_OK, HotkeyEventType.Confirm, null, false);
			this.m_NKM_UI_POPUP_OK_CANCEL_BOX_CANCEL.PointerClick.RemoveAllListeners();
			if (cancel != null)
			{
				this.m_NKM_UI_POPUP_OK_CANCEL_BOX_CANCEL.PointerClick.AddListener(cancel);
			}
			this.m_NKM_UI_POPUP_OK_CANCEL_BOX_CANCEL.PointerClick.AddListener(new UnityAction(NKCPopupChangeConfirm.CheckInstanceAndClose));
			if (!base.gameObject.activeSelf)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, true);
			}
			base.UIOpened(true);
		}

		// Token: 0x04005E4F RID: 24143
		public const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX";

		// Token: 0x04005E50 RID: 24144
		public const string UI_ASSET_NAME = "NKM_UI_POPUP_CHANGE_CONFIRM";

		// Token: 0x04005E51 RID: 24145
		private static NKCPopupChangeConfirm m_Instance;

		// Token: 0x04005E52 RID: 24146
		public Text m_NKM_UI_POPUP_CHANGE_CONFIRM_TOP_TEXT;

		// Token: 0x04005E53 RID: 24147
		public Text m_NKM_UI_POPUP_CHANGE_CONFIRM_TEXT_BEFORE;

		// Token: 0x04005E54 RID: 24148
		public Text m_NKM_UI_POPUP_CHANGE_CONFIRM_TEXT_AFTER;

		// Token: 0x04005E55 RID: 24149
		public Text m_NKM_UI_POPUP_CHANGE_CONFIRM_TEXT;

		// Token: 0x04005E56 RID: 24150
		public NKCUIComStateButton m_NKM_UI_POPUP_OK_CANCEL_BOX_OK;

		// Token: 0x04005E57 RID: 24151
		public NKCUIComStateButton m_NKM_UI_POPUP_OK_CANCEL_BOX_CANCEL;
	}
}
