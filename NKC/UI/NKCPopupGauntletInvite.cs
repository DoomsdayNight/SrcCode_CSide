using System;
using ClientPacket.Common;
using ClientPacket.Pvp;
using NKC.UI.Gauntlet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A5D RID: 2653
	public class NKCPopupGauntletInvite : NKCUIBase
	{
		// Token: 0x1700136E RID: 4974
		// (get) Token: 0x06007480 RID: 29824 RVA: 0x0026BA86 File Offset: 0x00269C86
		public override string MenuName
		{
			get
			{
				return "확인/취소 팝업";
			}
		}

		// Token: 0x1700136F RID: 4975
		// (get) Token: 0x06007481 RID: 29825 RVA: 0x0026BA8D File Offset: 0x00269C8D
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x06007482 RID: 29826 RVA: 0x0026BA90 File Offset: 0x00269C90
		public static bool isOpen()
		{
			return NKCPopupGauntletInvite.m_Popup != null && NKCPopupGauntletInvite.m_Popup.IsOpen;
		}

		// Token: 0x17001370 RID: 4976
		// (get) Token: 0x06007483 RID: 29827 RVA: 0x0026BAAC File Offset: 0x00269CAC
		private static NKCPopupGauntletInvite m_Popup
		{
			get
			{
				if (NKCPopupGauntletInvite.m_Instance == null)
				{
					NKCPopupGauntletInvite.m_loadedUIData = NKCUIManager.OpenNewInstance<NKCPopupGauntletInvite>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_POPUP_GAUNTLET_INVITE", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupGauntletInvite.CleanupInstance));
					if (NKCPopupGauntletInvite.m_loadedUIData != null)
					{
						NKCPopupGauntletInvite.m_Instance = NKCPopupGauntletInvite.m_loadedUIData.GetInstance<NKCPopupGauntletInvite>();
					}
					NKCPopupGauntletInvite.InitUI();
				}
				return NKCPopupGauntletInvite.m_Instance;
			}
		}

		// Token: 0x06007484 RID: 29828 RVA: 0x0026BB07 File Offset: 0x00269D07
		public static void CheckInstanceAndClose()
		{
			NKCUIManager.LoadedUIData loadedUIData = NKCPopupGauntletInvite.m_loadedUIData;
			if (loadedUIData != null)
			{
				loadedUIData.CloseInstance();
			}
			NKCPopupGauntletInvite.m_loadedUIData = null;
		}

		// Token: 0x06007485 RID: 29829 RVA: 0x0026BB1F File Offset: 0x00269D1F
		private static void CleanupInstance()
		{
			NKCPopupGauntletInvite.m_Instance = null;
		}

		// Token: 0x06007486 RID: 29830 RVA: 0x0026BB28 File Offset: 0x00269D28
		public override void OnBackButton()
		{
			NKCPopupGauntletInvite.eOpenType type = this.m_Type;
			if (type == NKCPopupGauntletInvite.eOpenType.OK)
			{
				this.OnOK();
				return;
			}
			if (type != NKCPopupGauntletInvite.eOpenType.OKCancel)
			{
				return;
			}
			this.OnCancel();
		}

		// Token: 0x06007487 RID: 29831 RVA: 0x0026BB54 File Offset: 0x00269D54
		public static void InitUI()
		{
			NKCPopupGauntletInvite.m_Instance.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(NKCPopupGauntletInvite.m_Instance.gameObject);
			if (NKCPopupGauntletInvite.m_Instance.m_cbtnOK_OK != null)
			{
				NKCPopupGauntletInvite.m_Instance.m_cbtnOK_OK.PointerClick.RemoveAllListeners();
				NKCPopupGauntletInvite.m_Instance.m_cbtnOK_OK.PointerClick.AddListener(new UnityAction(NKCPopupGauntletInvite.m_Instance.OnOK));
				NKCUtil.SetHotkey(NKCPopupGauntletInvite.m_Instance.m_cbtnOK_OK, HotkeyEventType.Confirm);
			}
			if (NKCPopupGauntletInvite.m_Instance.m_cbtnOKCancel_OK != null)
			{
				NKCPopupGauntletInvite.m_Instance.m_cbtnOKCancel_OK.PointerClick.RemoveAllListeners();
				NKCPopupGauntletInvite.m_Instance.m_cbtnOKCancel_OK.PointerClick.AddListener(new UnityAction(NKCPopupGauntletInvite.m_Instance.OnOK));
				NKCUtil.SetHotkey(NKCPopupGauntletInvite.m_Instance.m_cbtnOKCancel_OK, HotkeyEventType.Confirm);
			}
			if (NKCPopupGauntletInvite.m_Instance.m_cbtnOKCancel_Cancel != null)
			{
				NKCPopupGauntletInvite.m_Instance.m_cbtnOKCancel_Cancel.PointerClick.RemoveAllListeners();
				NKCPopupGauntletInvite.m_Instance.m_cbtnOKCancel_Cancel.PointerClick.AddListener(new UnityAction(NKCPopupGauntletInvite.m_Instance.OnCancel));
			}
			if (NKCPopupGauntletInvite.m_Instance.m_cbtnOKCancel_Red != null)
			{
				NKCPopupGauntletInvite.m_Instance.m_cbtnOKCancel_Red.PointerClick.RemoveAllListeners();
				NKCPopupGauntletInvite.m_Instance.m_cbtnOKCancel_Red.PointerClick.AddListener(new UnityAction(NKCPopupGauntletInvite.m_Instance.OnOK));
				NKCUtil.SetHotkey(NKCPopupGauntletInvite.m_Instance.m_cbtnOKCancel_Red, HotkeyEventType.Confirm);
			}
			NKCPopupGauntletInvite.m_Instance.gameObject.SetActive(false);
			NKCPopupGauntletInvite.m_Instance.m_endTimer = 0f;
			if (NKCPopupGauntletInvite.m_Instance.m_lbTimerText != null)
			{
				NKCUtil.SetGameobjectActive(NKCPopupGauntletInvite.m_Instance.m_lbTimerText.gameObject, false);
			}
			NKCUIGauntletCustomOption customOption = NKCPopupGauntletInvite.m_Instance.m_customOption;
			if (customOption != null)
			{
				customOption.Init();
			}
			NKCPopupGauntletInvite.m_Instance.m_bInitComplete = true;
		}

		// Token: 0x06007488 RID: 29832 RVA: 0x0026BD38 File Offset: 0x00269F38
		private void OpenOK(string Title, string Content, NKCPopupGauntletInvite.OnButton onOkButton = null, string OKButtonStr = "")
		{
			if (this.m_bOpen)
			{
				base.Close();
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
			base.gameObject.SetActive(true);
			if (string.IsNullOrEmpty(OKButtonStr))
			{
				this.m_lbBtnOK_OK.text = NKCUtilString.GET_STRING_CONFIRM_BY_ALL_SEARCH();
			}
			else
			{
				this.m_lbBtnOK_OK.text = OKButtonStr;
			}
			this.m_Type = NKCPopupGauntletInvite.eOpenType.OK;
			base.UIOpened(true);
		}

		// Token: 0x06007489 RID: 29833 RVA: 0x0026BE44 File Offset: 0x0026A044
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

		// Token: 0x0600748A RID: 29834 RVA: 0x0026BF6C File Offset: 0x0026A16C
		public static void OpenOKTimerBox(string title, string content, NKCPopupGauntletInvite.OnButton onOkButton, float endTime, string OKButtonStr, string endTimerStringID, FriendListData friendListData, NKMPrivateGameConfig privateGameConfig)
		{
			NKCPopupGauntletInvite.m_Popup.OpenOK(title, content, onOkButton, OKButtonStr);
			NKCPopupGauntletInvite.m_Popup.m_endTimer = endTime;
			NKCPopupGauntletInvite.m_Popup.m_endTimerTextStringID = endTimerStringID;
			NKCPopupGauntletInvite.m_Popup.m_customOption.SetOption(privateGameConfig);
			NKCPopupGauntletInvite.m_Popup.m_gauntletFriendSlot.SetUI(friendListData, false);
		}

		// Token: 0x0600748B RID: 29835 RVA: 0x0026BFC2 File Offset: 0x0026A1C2
		public static void OpenOKTimerBox(string title, string content, NKCPopupGauntletInvite.OnButton onOkButton, float endTime, string OKButtonStr, string endTimerStringID, NKMUserProfileData userProfileData)
		{
			NKCPopupGauntletInvite.m_Popup.OpenOK(title, content, onOkButton, OKButtonStr);
			NKCPopupGauntletInvite.m_Popup.m_endTimer = endTime;
			NKCPopupGauntletInvite.m_Popup.m_endTimerTextStringID = endTimerStringID;
			NKCPopupGauntletInvite.m_Popup.m_gauntletFriendSlot.SetUI(userProfileData);
		}

		// Token: 0x0600748C RID: 29836 RVA: 0x0026BFFC File Offset: 0x0026A1FC
		public static void OpenOKCancelTimerBox(string Title, string Content, NKCPopupGauntletInvite.OnButton onOkButton, NKCPopupGauntletInvite.OnButton onCancelButton, float endTime, string endTimerStringID, string OKButtonStr, string CancelButtonStr, NKMUserProfileData userProfileData, NKMPrivateGameConfig privateGameConfig)
		{
			NKCPopupGauntletInvite.m_Popup.OpenOKCancel(Title, Content, onOkButton, onCancelButton, OKButtonStr, CancelButtonStr, false);
			NKCPopupGauntletInvite.m_Popup.m_endTimer = endTime;
			NKCPopupGauntletInvite.m_Popup.m_endTimerTextStringID = endTimerStringID;
			NKCPopupGauntletInvite.m_Popup.m_customOption.SetOption(privateGameConfig);
			NKCPopupGauntletInvite.m_Popup.m_gauntletFriendSlot.SetUI(userProfileData);
		}

		// Token: 0x0600748D RID: 29837 RVA: 0x0026C058 File Offset: 0x0026A258
		private void OpenOKCancel(string Title, string Content, NKCPopupGauntletInvite.OnButton onOkButton, NKCPopupGauntletInvite.OnButton onCancelButton = null, string OKButtonStr = "", string CancelButtonStr = "", bool bUseRedButton = false)
		{
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
			this.m_Type = NKCPopupGauntletInvite.eOpenType.OKCancel;
			base.UIOpened(true);
		}

		// Token: 0x0600748E RID: 29838 RVA: 0x0026C1D8 File Offset: 0x0026A3D8
		public static void ClosePopupBox()
		{
			if (NKCPopupGauntletInvite.m_Instance == null)
			{
				return;
			}
			if (NKCPopupGauntletInvite.isOpen())
			{
				NKCPopupGauntletInvite.m_Popup.Close();
			}
			NKCPopupGauntletInvite.CheckInstanceAndClose();
		}

		// Token: 0x0600748F RID: 29839 RVA: 0x0026C1FE File Offset: 0x0026A3FE
		public void OnOK()
		{
			base.Close();
			if (this.dOnOKButton != null)
			{
				this.dOnOKButton();
			}
		}

		// Token: 0x06007490 RID: 29840 RVA: 0x0026C219 File Offset: 0x0026A419
		public void OnCancel()
		{
			base.Close();
			if (this.dOnCancelButton != null)
			{
				this.dOnCancelButton();
			}
		}

		// Token: 0x06007491 RID: 29841 RVA: 0x0026C234 File Offset: 0x0026A434
		public override void CloseInternal()
		{
			this.m_endTimer = 0f;
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007492 RID: 29842 RVA: 0x0026C250 File Offset: 0x0026A450
		public static void SetOnTop(bool bOverDevConsle = false)
		{
			if (NKCPopupGauntletInvite.m_Popup != null)
			{
				GameObject gameObject = GameObject.Find("NKM_SCEN_UI_FRONT");
				Transform transform = (gameObject != null) ? gameObject.transform : null;
				if (transform != null)
				{
					NKCPopupGauntletInvite.m_Popup.transform.SetParent(transform);
					if (bOverDevConsle)
					{
						NKCPopupGauntletInvite.m_Popup.transform.SetAsLastSibling();
						return;
					}
					NKCPopupGauntletInvite.m_Popup.transform.SetSiblingIndex(transform.childCount - 2);
				}
			}
		}

		// Token: 0x040060DA RID: 24794
		private NKCPopupGauntletInvite.eOpenType m_Type;

		// Token: 0x040060DB RID: 24795
		public Text m_lbTitle;

		// Token: 0x040060DC RID: 24796
		public Text m_lbContent;

		// Token: 0x040060DD RID: 24797
		public Text m_lbTimerText;

		// Token: 0x040060DE RID: 24798
		public NKCUIGauntletFriendSlot m_gauntletFriendSlot;

		// Token: 0x040060DF RID: 24799
		public NKCUIGauntletCustomOption m_customOption;

		// Token: 0x040060E0 RID: 24800
		private static NKCUIManager.LoadedUIData m_loadedUIData;

		// Token: 0x040060E1 RID: 24801
		private static NKCPopupGauntletInvite m_Instance;

		// Token: 0x040060E2 RID: 24802
		[Header("BG")]
		public EventTrigger m_etBG;

		// Token: 0x040060E3 RID: 24803
		[Header("OK Box")]
		public GameObject m_objRootOkBox;

		// Token: 0x040060E4 RID: 24804
		public NKCUIComButton m_cbtnOK_OK;

		// Token: 0x040060E5 RID: 24805
		public Text m_lbBtnOK_OK;

		// Token: 0x040060E6 RID: 24806
		[Header("OK/Cancel Box")]
		public GameObject m_objRootOkCancelBox;

		// Token: 0x040060E7 RID: 24807
		public NKCUIComButton m_cbtnOKCancel_OK;

		// Token: 0x040060E8 RID: 24808
		public NKCUIComButton m_cbtnOKCancel_Cancel;

		// Token: 0x040060E9 RID: 24809
		public NKCUIComButton m_cbtnOKCancel_Red;

		// Token: 0x040060EA RID: 24810
		public Text m_lbBtnOKCancel_OK;

		// Token: 0x040060EB RID: 24811
		public Text m_lbBtnOKCancel_Cancel;

		// Token: 0x040060EC RID: 24812
		public Text m_lbBtnOKCancel_Red;

		// Token: 0x040060ED RID: 24813
		private NKCPopupGauntletInvite.OnButton dOnOKButton;

		// Token: 0x040060EE RID: 24814
		private NKCPopupGauntletInvite.OnButton dOnCancelButton;

		// Token: 0x040060EF RID: 24815
		private float m_endTimer;

		// Token: 0x040060F0 RID: 24816
		private string m_endTimerTextStringID = "";

		// Token: 0x040060F1 RID: 24817
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x040060F2 RID: 24818
		private bool m_bInitComplete;

		// Token: 0x020017B4 RID: 6068
		private enum eOpenType
		{
			// Token: 0x0400A754 RID: 42836
			OK,
			// Token: 0x0400A755 RID: 42837
			OKCancel
		}

		// Token: 0x020017B5 RID: 6069
		// (Invoke) Token: 0x0600B404 RID: 46084
		public delegate void OnButton();
	}
}
