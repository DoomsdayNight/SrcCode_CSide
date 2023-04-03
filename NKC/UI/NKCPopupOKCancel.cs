using System;
using NKC.PacketHandler;
using NKC.Publisher;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A72 RID: 2674
	public class NKCPopupOKCancel : NKCUIBase
	{
		// Token: 0x170013AC RID: 5036
		// (get) Token: 0x06007602 RID: 30210 RVA: 0x00273B0D File Offset: 0x00271D0D
		public override string MenuName
		{
			get
			{
				return "확인/취소 팝업";
			}
		}

		// Token: 0x170013AD RID: 5037
		// (get) Token: 0x06007603 RID: 30211 RVA: 0x00273B14 File Offset: 0x00271D14
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x06007604 RID: 30212 RVA: 0x00273B17 File Offset: 0x00271D17
		public static bool isOpen()
		{
			return NKCPopupOKCancel.m_Popup != null && NKCPopupOKCancel.m_Popup.IsOpen;
		}

		// Token: 0x06007605 RID: 30213 RVA: 0x00273B34 File Offset: 0x00271D34
		public override void OnBackButton()
		{
			NKCPopupOKCancel.eOpenType type = this.m_Type;
			if (type == NKCPopupOKCancel.eOpenType.OK)
			{
				this.OnOK();
				return;
			}
			if (type != NKCPopupOKCancel.eOpenType.OKCancel)
			{
				return;
			}
			this.OnCancel();
		}

		// Token: 0x06007606 RID: 30214 RVA: 0x00273B60 File Offset: 0x00271D60
		public static void InitUI()
		{
			NKCPopupOKCancel nkcpopupOKCancel = NKCUIManager.OpenUI<NKCPopupOKCancel>("NKM_UI_POPUP_BOX");
			NKCPopupOKCancel.m_Popup = nkcpopupOKCancel;
			nkcpopupOKCancel.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(nkcpopupOKCancel.gameObject);
			if (nkcpopupOKCancel.m_cbtnOK_OK != null)
			{
				nkcpopupOKCancel.m_cbtnOK_OK.PointerClick.RemoveAllListeners();
				nkcpopupOKCancel.m_cbtnOK_OK.PointerClick.AddListener(new UnityAction(nkcpopupOKCancel.OnOK));
				NKCUtil.SetHotkey(nkcpopupOKCancel.m_cbtnOK_OK, HotkeyEventType.Confirm);
			}
			if (nkcpopupOKCancel.m_cbtnOKCancel_OK != null)
			{
				nkcpopupOKCancel.m_cbtnOKCancel_OK.PointerClick.RemoveAllListeners();
				nkcpopupOKCancel.m_cbtnOKCancel_OK.PointerClick.AddListener(new UnityAction(nkcpopupOKCancel.OnOK));
				NKCUtil.SetHotkey(nkcpopupOKCancel.m_cbtnOKCancel_OK, HotkeyEventType.Confirm);
			}
			if (nkcpopupOKCancel.m_cbtnOKCancel_Cancel != null)
			{
				nkcpopupOKCancel.m_cbtnOKCancel_Cancel.PointerClick.RemoveAllListeners();
				nkcpopupOKCancel.m_cbtnOKCancel_Cancel.PointerClick.AddListener(new UnityAction(nkcpopupOKCancel.OnCancel));
			}
			if (nkcpopupOKCancel.m_cbtnOKCancel_Red != null)
			{
				nkcpopupOKCancel.m_cbtnOKCancel_Red.PointerClick.RemoveAllListeners();
				nkcpopupOKCancel.m_cbtnOKCancel_Red.PointerClick.AddListener(new UnityAction(nkcpopupOKCancel.OnOK));
				NKCUtil.SetHotkey(nkcpopupOKCancel.m_cbtnOKCancel_Red, HotkeyEventType.Confirm);
			}
			nkcpopupOKCancel.gameObject.SetActive(false);
			nkcpopupOKCancel.m_endTimer = 0f;
			if (nkcpopupOKCancel.m_lbTimerText != null)
			{
				NKCUtil.SetGameobjectActive(nkcpopupOKCancel.m_lbTimerText.gameObject, false);
			}
			nkcpopupOKCancel.m_bInitComplete = true;
		}

		// Token: 0x06007607 RID: 30215 RVA: 0x00273CD8 File Offset: 0x00271ED8
		public static void OpenOKBoxByStringID(string TitleID, string ContentID, NKCPopupOKCancel.OnButton onOkButton = null)
		{
			NKCPopupOKCancel.OpenOKBox(NKCStringTable.GetString(TitleID, false), NKCStringTable.GetString(ContentID, false), onOkButton, "");
		}

		// Token: 0x06007608 RID: 30216 RVA: 0x00273CF3 File Offset: 0x00271EF3
		public static void OpenOKBox(string Title, string Content, NKCPopupOKCancel.OnButton onOkButton = null, string OKButtonStr = "")
		{
			NKCPopupOKCancel.m_Popup.OpenOK(Title, Content, onOkButton, OKButtonStr, true);
		}

		// Token: 0x06007609 RID: 30217 RVA: 0x00273D04 File Offset: 0x00271F04
		public static void OpenOKBox(NKM_ERROR_CODE errorCode, NKCPopupOKCancel.OnButton onOkButton = null, string OKButtonStr = "")
		{
			NKCPopupOKCancel.m_Popup.OpenOK(NKCUtilString.GET_STRING_ERROR, NKCPacketHandlers.GetErrorMessage(errorCode), onOkButton, OKButtonStr, true);
		}

		// Token: 0x0600760A RID: 30218 RVA: 0x00273D1E File Offset: 0x00271F1E
		public static void OpenOKBox(NKC_PUBLISHER_RESULT_CODE errorCode, string additionalError, NKCPopupOKCancel.OnButton onOkButton = null, string OKButtonStr = "")
		{
			NKCPopupOKCancel.m_Popup.OpenOK(NKCUtilString.GET_STRING_ERROR, NKCPublisherModule.GetErrorMessage(errorCode, additionalError), onOkButton, OKButtonStr, true);
		}

		// Token: 0x0600760B RID: 30219 RVA: 0x00273D3C File Offset: 0x00271F3C
		public void OpenOK(string Title, string Content, NKCPopupOKCancel.OnButton onOkButton = null, string OKButtonStr = "", bool bShowNpcIllust = true)
		{
			if (!this.m_bInitComplete)
			{
				NKCPopupOKCancel.InitUI();
			}
			if (this.m_bOpen)
			{
				base.Close();
			}
			if (this.m_NKCUIOpenAnimator != null)
			{
				this.m_NKCUIOpenAnimator.PlayOpenAni();
			}
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(delegate(BaseEventData data)
			{
				this.OnOK();
			});
			if (this.m_etBG != null)
			{
				this.m_etBG.triggers.Clear();
				this.m_etBG.triggers.Add(entry);
			}
			this.dOnOKButton = onOkButton;
			this.m_lbTitle.text = Title;
			this.m_lbContent.text = Content;
			NKCUtil.SetLabelText(this.m_lbTimerText, "");
			if (Content != null)
			{
				Debug.Log("OK Popup Content : " + Content);
			}
			this.m_objRootOkBox.SetActive(true);
			this.m_objRootOkCancelBox.SetActive(false);
			NKCUtil.SetGameobjectActive(this.m_objNpcIllust, bShowNpcIllust);
			base.gameObject.SetActive(true);
			if (string.IsNullOrEmpty(OKButtonStr))
			{
				this.m_lbBtnOK_OK.text = NKCUtilString.GET_STRING_CONFIRM_BY_ALL_SEARCH();
			}
			else
			{
				this.m_lbBtnOK_OK.text = OKButtonStr;
			}
			this.m_Type = NKCPopupOKCancel.eOpenType.OK;
			base.UIOpened(true);
		}

		// Token: 0x0600760C RID: 30220 RVA: 0x00273E74 File Offset: 0x00272074
		public void Update()
		{
			if (base.IsOpen)
			{
				if (this.m_NKCUIOpenAnimator != null)
				{
					this.m_NKCUIOpenAnimator.Update();
				}
				if (this.m_endTimer > 0f && this.m_lbTimerText != null)
				{
					int num = (int)this.m_endTimer;
					this.m_endTimer -= Time.deltaTime;
					if (this.m_endTimer <= 0f)
					{
						if (this.dOnCancelButton != null)
						{
							this.dOnCancelButton();
						}
						this.m_endTimer = 0f;
						base.Close();
					}
					if (num != (int)this.m_endTimer)
					{
						if (!string.IsNullOrEmpty(this.m_endTimerTextStringID))
						{
							this.m_lbTimerText.text = string.Format(NKCStringTable.GetString(this.m_endTimerTextStringID, false), 1 + (int)this.m_endTimer);
						}
						else
						{
							this.m_lbTimerText.text = string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_SECONDS", false), 1 + (int)this.m_endTimer);
						}
						if (!this.m_lbTimerText.gameObject.activeInHierarchy)
						{
							NKCUtil.SetGameobjectActive(this.m_lbTimerText.gameObject, true);
						}
					}
				}
			}
		}

		// Token: 0x0600760D RID: 30221 RVA: 0x00273F9C File Offset: 0x0027219C
		public static void OpenOKCancelBoxbyStringID(string TitleID, string ContentID, NKCPopupOKCancel.OnButton onOkButton, NKCPopupOKCancel.OnButton onCancelButton = null)
		{
			NKCPopupOKCancel.m_Popup.OpenOKCancel(NKCStringTable.GetString(TitleID, false), NKCStringTable.GetString(ContentID, false), onOkButton, onCancelButton, "", "", false, true);
		}

		// Token: 0x0600760E RID: 30222 RVA: 0x00273FCF File Offset: 0x002721CF
		public static void OpenOKTimerBox(string title, string content, NKCPopupOKCancel.OnButton onOkButton, float endTime, string OKButtonStr, string endTimerStringID)
		{
			NKCPopupOKCancel.m_Popup.OpenOK(title, content, onOkButton, OKButtonStr, true);
			NKCPopupOKCancel.m_Popup.m_endTimer = endTime;
			NKCPopupOKCancel.m_Popup.m_endTimerTextStringID = endTimerStringID;
		}

		// Token: 0x0600760F RID: 30223 RVA: 0x00273FF8 File Offset: 0x002721F8
		public static void OpenOKCancelTimerBox(string Title, string Content, NKCPopupOKCancel.OnButton onOkButton, NKCPopupOKCancel.OnButton onCancelButton, float endTime, string endTimerStringID, string OKButtonStr, string CancelButtonStr)
		{
			NKCPopupOKCancel.m_Popup.OpenOKCancel(Title, Content, onOkButton, onCancelButton, OKButtonStr, CancelButtonStr, false, true);
			NKCPopupOKCancel.m_Popup.m_endTimer = endTime;
			NKCPopupOKCancel.m_Popup.m_endTimerTextStringID = endTimerStringID;
		}

		// Token: 0x06007610 RID: 30224 RVA: 0x00274034 File Offset: 0x00272234
		public static void OpenOKCancelBox(string Title, string Content, NKCPopupOKCancel.OnButton onOkButton, NKCPopupOKCancel.OnButton onCancelButton = null, bool bDev = false)
		{
			NKCPopupOKCancel.m_Popup.OpenOKCancel(Title, Content, onOkButton, onCancelButton, "", "", false, true);
			if (bDev)
			{
				Transform component = NKCPopupOKCancel.m_Popup.GetComponent<Transform>();
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

		// Token: 0x06007611 RID: 30225 RVA: 0x002740A8 File Offset: 0x002722A8
		public static void OpenOKCancelBox(string Title, string Content, NKCPopupOKCancel.OnButton onOkButton, NKCPopupOKCancel.OnButton onCancelButton, string OKButtonStr, string CancelButtonStr = "", bool bUseRed = false)
		{
			NKCPopupOKCancel.m_Popup.OpenOKCancel(Title, Content, onOkButton, onCancelButton, OKButtonStr, CancelButtonStr, bUseRed, true);
		}

		// Token: 0x06007612 RID: 30226 RVA: 0x002740CC File Offset: 0x002722CC
		public void OpenOKCancel(string Title, string Content, NKCPopupOKCancel.OnButton onOkButton, NKCPopupOKCancel.OnButton onCancelButton = null, string OKButtonStr = "", string CancelButtonStr = "", bool bUseRedButton = false, bool bShowNpcIllust = true)
		{
			if (!this.m_bInitComplete)
			{
				NKCPopupOKCancel.InitUI();
			}
			if (this.m_bOpen)
			{
				base.Close();
			}
			Debug.Log("### OpenOKCancel");
			if (this.m_NKCUIOpenAnimator != null)
			{
				this.m_NKCUIOpenAnimator.PlayOpenAni();
			}
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
			NKCUtil.SetLabelText(this.m_lbTimerText, "");
			this.m_objRootOkBox.SetActive(false);
			this.m_objRootOkCancelBox.SetActive(true);
			NKCUtil.SetGameobjectActive(this.m_cbtnOKCancel_OK, !bUseRedButton);
			NKCUtil.SetGameobjectActive(this.m_cbtnOKCancel_Red, bUseRedButton);
			NKCUtil.SetGameobjectActive(this.m_objNpcIllust, bShowNpcIllust);
			base.gameObject.SetActive(true);
			if (string.IsNullOrEmpty(OKButtonStr))
			{
				if (bUseRedButton)
				{
					this.m_lbBtnOKCancel_Red.text = NKCUtilString.GET_STRING_CONFIRM_BY_ALL_SEARCH();
				}
				else
				{
					this.m_lbBtnOKCancel_OK.text = NKCUtilString.GET_STRING_CONFIRM_BY_ALL_SEARCH();
				}
			}
			else if (bUseRedButton)
			{
				this.m_lbBtnOKCancel_Red.text = OKButtonStr;
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
				this.m_lbBtnOKCancel_Cancel.text = CancelButtonStr;
			}
			this.m_Type = NKCPopupOKCancel.eOpenType.OKCancel;
			base.UIOpened(true);
		}

		// Token: 0x06007613 RID: 30227 RVA: 0x00274270 File Offset: 0x00272470
		public static void ClosePopupBox()
		{
			Debug.Log("### ClosePopupBox");
			NKCPopupOKCancel.m_Popup.Close();
		}

		// Token: 0x06007614 RID: 30228 RVA: 0x00274286 File Offset: 0x00272486
		public void OnOK()
		{
			base.Close();
			if (this.dOnOKButton != null)
			{
				this.dOnOKButton();
			}
		}

		// Token: 0x06007615 RID: 30229 RVA: 0x002742A1 File Offset: 0x002724A1
		public void OnCancel()
		{
			base.Close();
			if (this.dOnCancelButton != null)
			{
				this.dOnCancelButton();
			}
		}

		// Token: 0x06007616 RID: 30230 RVA: 0x002742BC File Offset: 0x002724BC
		public override void CloseInternal()
		{
			Debug.Log("### CloseInternal");
			this.m_endTimer = 0f;
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007617 RID: 30231 RVA: 0x002742E0 File Offset: 0x002724E0
		public static void SetOnTop(bool bOverDevConsle = false)
		{
			if (NKCPopupOKCancel.m_Popup != null)
			{
				GameObject gameObject = GameObject.Find("NKM_SCEN_UI_FRONT");
				Transform transform = (gameObject != null) ? gameObject.transform : null;
				if (transform != null)
				{
					NKCPopupOKCancel.m_Popup.transform.SetParent(transform);
					if (bOverDevConsle)
					{
						NKCPopupOKCancel.m_Popup.transform.SetAsLastSibling();
						return;
					}
					NKCPopupOKCancel.m_Popup.transform.SetSiblingIndex(transform.childCount - 2);
				}
			}
		}

		// Token: 0x04006268 RID: 25192
		private NKCPopupOKCancel.eOpenType m_Type;

		// Token: 0x04006269 RID: 25193
		public Text m_lbTitle;

		// Token: 0x0400626A RID: 25194
		public Text m_lbContent;

		// Token: 0x0400626B RID: 25195
		public Text m_lbTimerText;

		// Token: 0x0400626C RID: 25196
		private static NKCPopupOKCancel m_Popup;

		// Token: 0x0400626D RID: 25197
		[Header("BG")]
		public EventTrigger m_etBG;

		// Token: 0x0400626E RID: 25198
		public GameObject m_objNpcIllust;

		// Token: 0x0400626F RID: 25199
		[Header("OK Box")]
		public GameObject m_objRootOkBox;

		// Token: 0x04006270 RID: 25200
		public NKCUIComButton m_cbtnOK_OK;

		// Token: 0x04006271 RID: 25201
		public Text m_lbBtnOK_OK;

		// Token: 0x04006272 RID: 25202
		[Header("OK/Cancel Box")]
		public GameObject m_objRootOkCancelBox;

		// Token: 0x04006273 RID: 25203
		public NKCUIComButton m_cbtnOKCancel_OK;

		// Token: 0x04006274 RID: 25204
		public NKCUIComButton m_cbtnOKCancel_Cancel;

		// Token: 0x04006275 RID: 25205
		public NKCUIComButton m_cbtnOKCancel_Red;

		// Token: 0x04006276 RID: 25206
		public Text m_lbBtnOKCancel_OK;

		// Token: 0x04006277 RID: 25207
		public Text m_lbBtnOKCancel_Cancel;

		// Token: 0x04006278 RID: 25208
		public Text m_lbBtnOKCancel_Red;

		// Token: 0x04006279 RID: 25209
		private NKCPopupOKCancel.OnButton dOnOKButton;

		// Token: 0x0400627A RID: 25210
		private NKCPopupOKCancel.OnButton dOnCancelButton;

		// Token: 0x0400627B RID: 25211
		private float m_endTimer;

		// Token: 0x0400627C RID: 25212
		private string m_endTimerTextStringID = "";

		// Token: 0x0400627D RID: 25213
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x0400627E RID: 25214
		private bool m_bInitComplete;

		// Token: 0x020017D3 RID: 6099
		private enum eOpenType
		{
			// Token: 0x0400A794 RID: 42900
			OK,
			// Token: 0x0400A795 RID: 42901
			OKCancel
		}

		// Token: 0x020017D4 RID: 6100
		// (Invoke) Token: 0x0600B450 RID: 46160
		public delegate void OnButton();
	}
}
