using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A65 RID: 2661
	public class NKCPopupInputText : NKCUIBase
	{
		// Token: 0x17001383 RID: 4995
		// (get) Token: 0x0600750E RID: 29966 RVA: 0x0026EC04 File Offset: 0x0026CE04
		public static NKCPopupInputText Instance
		{
			get
			{
				if (NKCPopupInputText.m_Instance == null)
				{
					NKCPopupInputText.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupInputText>("ab_ui_nkm_ui_popup_ok_cancel_box", "NKM_UI_POPUP_INPUT_TEXT", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupInputText.CleanupInstance)).GetInstance<NKCPopupInputText>();
					NKCPopupInputText.m_Instance.InitUI();
				}
				return NKCPopupInputText.m_Instance;
			}
		}

		// Token: 0x0600750F RID: 29967 RVA: 0x0026EC53 File Offset: 0x0026CE53
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupInputText.m_Instance != null && NKCPopupInputText.m_Instance.IsOpen)
			{
				NKCPopupInputText.m_Instance.Close();
			}
		}

		// Token: 0x06007510 RID: 29968 RVA: 0x0026EC78 File Offset: 0x0026CE78
		public static bool isOpen()
		{
			return NKCPopupInputText.m_Instance != null && NKCPopupInputText.m_Instance.IsOpen;
		}

		// Token: 0x06007511 RID: 29969 RVA: 0x0026EC93 File Offset: 0x0026CE93
		private static void CleanupInstance()
		{
			NKCPopupInputText.m_Instance = null;
		}

		// Token: 0x17001384 RID: 4996
		// (get) Token: 0x06007512 RID: 29970 RVA: 0x0026EC9B File Offset: 0x0026CE9B
		public override string MenuName
		{
			get
			{
				return "확인/취소 팝업";
			}
		}

		// Token: 0x17001385 RID: 4997
		// (get) Token: 0x06007513 RID: 29971 RVA: 0x0026ECA2 File Offset: 0x0026CEA2
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x06007514 RID: 29972 RVA: 0x0026ECA5 File Offset: 0x0026CEA5
		public override void OnBackButton()
		{
			this.OnCancel();
		}

		// Token: 0x06007515 RID: 29973 RVA: 0x0026ECB0 File Offset: 0x0026CEB0
		public void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			if (this.m_cbtnOKCancel_OK != null)
			{
				this.m_cbtnOKCancel_OK.PointerClick.RemoveAllListeners();
				this.m_cbtnOKCancel_OK.PointerClick.AddListener(new UnityAction(this.OnOK));
			}
			if (this.m_cbtnOKCancel_Cancel != null)
			{
				this.m_cbtnOKCancel_Cancel.PointerClick.RemoveAllListeners();
				this.m_cbtnOKCancel_Cancel.PointerClick.AddListener(new UnityAction(this.OnCancel));
			}
			if (this.m_IFText != null)
			{
				this.m_IFText.onEndEdit.RemoveAllListeners();
				this.m_IFText.onEndEdit.AddListener(new UnityAction<string>(this.OnTextChanged));
			}
			NKCUtil.SetHotkey(this.m_cbtnOKCancel_OK, HotkeyEventType.Confirm, null, false);
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007516 RID: 29974 RVA: 0x0026ED96 File Offset: 0x0026CF96
		public void Update()
		{
			if (base.IsOpen && this.m_NKCUIOpenAnimator != null)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x06007517 RID: 29975 RVA: 0x0026EDB4 File Offset: 0x0026CFB4
		public void OpenOKCancelBoxbyStringID(string TitleID, string ContentID, string GuideID, NKCPopupInputText.OnButton onOkButton, NKCPopupInputText.OnButton onCancelButton = null, int MaxCharCount = 0)
		{
			NKCPopupInputText.m_Instance.OpenOKCancel(NKCStringTable.GetString(TitleID, false), NKCStringTable.GetString(ContentID, false), NKCStringTable.GetString(GuideID, false), onOkButton, onCancelButton, "", "", MaxCharCount);
		}

		// Token: 0x06007518 RID: 29976 RVA: 0x0026EDF0 File Offset: 0x0026CFF0
		public void OpenOKCancelBox(string Title, string Content, string Guide, NKCPopupInputText.OnButton onOkButton, NKCPopupInputText.OnButton onCancelButton = null, bool bDev = false, int MaxCharCount = 0)
		{
			NKCPopupInputText.m_Instance.OpenOKCancel(Title, Content, Guide, onOkButton, onCancelButton, "", "", MaxCharCount);
			if (bDev)
			{
				Transform component = NKCPopupInputText.m_Instance.GetComponent<Transform>();
				if (component)
				{
					GameObject gameObject = GameObject.Find("NKM_SCEN_UI_FRONT");
					Transform parent = (gameObject != null) ? gameObject.transform : null;
					component.SetParent(parent, false);
					component.localPosition = Vector3.zero;
					component.localScale = Vector3.one;
				}
			}
		}

		// Token: 0x06007519 RID: 29977 RVA: 0x0026EE68 File Offset: 0x0026D068
		public void OpenOKCancelBox(string Title, string Content, string Guide, NKCPopupInputText.OnButton onOkButton, NKCPopupInputText.OnButton onCancelButton, string OKButtonStr, string CancelButtonStr = "", int MaxCharCount = 0)
		{
			NKCPopupInputText.m_Instance.OpenOKCancel(Title, Content, Guide, onOkButton, onCancelButton, OKButtonStr, CancelButtonStr, MaxCharCount);
		}

		// Token: 0x0600751A RID: 29978 RVA: 0x0026EE8C File Offset: 0x0026D08C
		public void OpenOKCancel(string Title, string Content, string Guide, NKCPopupInputText.OnButton onOkButton, NKCPopupInputText.OnButton onCancelButton = null, string OKButtonStr = "", string CancelButtonStr = "", int MaxCharCount = 0)
		{
			if (this.m_NKCUIOpenAnimator != null)
			{
				this.m_NKCUIOpenAnimator.PlayOpenAni();
			}
			this.m_IFText.text = string.Empty;
			this.m_IFText.placeholder.GetComponent<Text>().text = Guide;
			this.m_IFText.characterLimit = MaxCharCount;
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(delegate(BaseEventData data)
			{
				this.OnCancel();
			});
			if (this.m_etBG != null)
			{
				this.m_etBG.triggers.Clear();
				this.m_etBG.triggers.Add(entry);
			}
			this.dOnOKButton = onOkButton;
			this.dOnCancelButton = onCancelButton;
			this.m_lbTitle.text = Title;
			this.m_lbContent.text = Content;
			base.gameObject.SetActive(true);
			if (string.IsNullOrEmpty(OKButtonStr))
			{
				this.m_lbBtnOKCancel_OK.text = NKCUtilString.GET_STRING_CONFIRM_BY_ALL_SEARCH();
			}
			else
			{
				this.m_lbBtnOKCancel_OK.text = OKButtonStr;
			}
			if (string.IsNullOrEmpty(CancelButtonStr))
			{
				this.m_lbBtnOKCancel_Cancel.text = NKCUtilString.GET_STRING_CANCEL_BY_ALL_SEARCH();
			}
			else
			{
				this.m_lbBtnOKCancel_Cancel.text = OKButtonStr;
			}
			base.UIOpened(true);
		}

		// Token: 0x0600751B RID: 29979 RVA: 0x0026EFBD File Offset: 0x0026D1BD
		public void ClosePopupBox()
		{
			NKCPopupInputText.m_Instance.Close();
		}

		// Token: 0x0600751C RID: 29980 RVA: 0x0026EFCC File Offset: 0x0026D1CC
		private void OnTextChanged(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				this.m_IFText.text = string.Empty;
			}
			else
			{
				this.m_IFText.text = NKCFilterManager.CheckBadChat(str);
			}
			if (NKCInputManager.IsChatSubmitEnter())
			{
				if (!this.m_cbtnOKCancel_OK.m_bLock)
				{
					this.OnOK();
				}
				EventSystem.current.SetSelectedGameObject(null);
			}
		}

		// Token: 0x0600751D RID: 29981 RVA: 0x0026F029 File Offset: 0x0026D229
		public void OnOK()
		{
			base.Close();
			if (!string.IsNullOrEmpty(this.m_IFText.text))
			{
				NKCPopupInputText.OnButton onButton = this.dOnOKButton;
				if (onButton == null)
				{
					return;
				}
				onButton(this.m_IFText.text);
			}
		}

		// Token: 0x0600751E RID: 29982 RVA: 0x0026F05E File Offset: 0x0026D25E
		public void OnCancel()
		{
			base.Close();
			NKCPopupInputText.OnButton onButton = this.dOnCancelButton;
			if (onButton == null)
			{
				return;
			}
			onButton(this.m_IFText.text);
		}

		// Token: 0x0600751F RID: 29983 RVA: 0x0026F081 File Offset: 0x0026D281
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007520 RID: 29984 RVA: 0x0026F090 File Offset: 0x0026D290
		public static void SetOnTop(bool bOverDevConsle = false)
		{
			if (NKCPopupInputText.m_Instance != null)
			{
				GameObject gameObject = GameObject.Find("NKM_SCEN_UI_FRONT");
				Transform transform = (gameObject != null) ? gameObject.transform : null;
				if (transform != null)
				{
					NKCPopupInputText.m_Instance.transform.SetParent(transform);
					if (bOverDevConsle)
					{
						NKCPopupInputText.m_Instance.transform.SetAsLastSibling();
						return;
					}
					NKCPopupInputText.m_Instance.transform.SetSiblingIndex(transform.childCount - 2);
				}
			}
		}

		// Token: 0x04006159 RID: 24921
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_ok_cancel_box";

		// Token: 0x0400615A RID: 24922
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_INPUT_TEXT";

		// Token: 0x0400615B RID: 24923
		private static NKCPopupInputText m_Instance;

		// Token: 0x0400615C RID: 24924
		public Text m_lbTitle;

		// Token: 0x0400615D RID: 24925
		public Text m_lbContent;

		// Token: 0x0400615E RID: 24926
		[Header("BG")]
		public EventTrigger m_etBG;

		// Token: 0x0400615F RID: 24927
		public InputField m_IFText;

		// Token: 0x04006160 RID: 24928
		[Header("OK/Cancel Box")]
		public NKCUIComStateButton m_cbtnOKCancel_OK;

		// Token: 0x04006161 RID: 24929
		public NKCUIComStateButton m_cbtnOKCancel_Cancel;

		// Token: 0x04006162 RID: 24930
		public Text m_lbBtnOKCancel_OK;

		// Token: 0x04006163 RID: 24931
		public Text m_lbBtnOKCancel_Cancel;

		// Token: 0x04006164 RID: 24932
		private NKCPopupInputText.OnButton dOnOKButton;

		// Token: 0x04006165 RID: 24933
		private NKCPopupInputText.OnButton dOnCancelButton;

		// Token: 0x04006166 RID: 24934
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x020017C3 RID: 6083
		// (Invoke) Token: 0x0600B426 RID: 46118
		public delegate void OnButton(string str);
	}
}
