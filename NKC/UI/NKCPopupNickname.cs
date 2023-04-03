using System;
using Cs.Logging;
using NKC.PacketHandler;
using NKM;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A70 RID: 2672
	public class NKCPopupNickname : NKCUIBase
	{
		// Token: 0x170013A5 RID: 5029
		// (get) Token: 0x060075E4 RID: 30180 RVA: 0x002734D8 File Offset: 0x002716D8
		public static NKCPopupNickname Instance
		{
			get
			{
				if (NKCPopupNickname.m_Instance == null)
				{
					NKCPopupNickname.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupNickname>("ab_ui_nkm_ui_popup_ok_cancel_box", "NKM_UI_POPUP_NICKNAME_BOX", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupNickname.CleanupInstance)).GetInstance<NKCPopupNickname>();
					NKCPopupNickname.m_Instance.InitUI();
				}
				return NKCPopupNickname.m_Instance;
			}
		}

		// Token: 0x060075E5 RID: 30181 RVA: 0x00273527 File Offset: 0x00271727
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupNickname.m_Instance != null && NKCPopupNickname.m_Instance.IsOpen)
			{
				NKCPopupNickname.m_Instance.Close();
			}
		}

		// Token: 0x060075E6 RID: 30182 RVA: 0x0027354C File Offset: 0x0027174C
		private static void CleanupInstance()
		{
			NKCPopupNickname.m_Instance = null;
		}

		// Token: 0x170013A6 RID: 5030
		// (get) Token: 0x060075E7 RID: 30183 RVA: 0x00273554 File Offset: 0x00271754
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170013A7 RID: 5031
		// (get) Token: 0x060075E8 RID: 30184 RVA: 0x00273557 File Offset: 0x00271757
		public override string MenuName
		{
			get
			{
				return "Nickname";
			}
		}

		// Token: 0x170013A8 RID: 5032
		// (get) Token: 0x060075E9 RID: 30185 RVA: 0x0027355E File Offset: 0x0027175E
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x060075EA RID: 30186 RVA: 0x00273564 File Offset: 0x00271764
		public void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			NKCUIComStateButton btnOK = this.m_btnOK;
			if (btnOK != null)
			{
				btnOK.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton btnOK2 = this.m_btnOK;
			if (btnOK2 != null)
			{
				btnOK2.PointerClick.AddListener(new UnityAction(this.OnOK));
			}
			NKCUIComStateButton btnCancel = this.m_btnCancel;
			if (btnCancel != null)
			{
				btnCancel.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton btnCancel2 = this.m_btnCancel;
			if (btnCancel2 != null)
			{
				btnCancel2.PointerClick.AddListener(new UnityAction(base.Close));
			}
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.NICKNAME_LIMIT_ENG))
			{
				this.m_InputField.contentType = InputField.ContentType.Alphanumeric;
				NKCUtil.SetLabelText(this.m_textDesc1, NKCStringTable.GetString("SI_PF_NICKNAME_CHANGE_DESC_1_GLOBAL", false));
			}
			this.m_InputField.onValueChanged.AddListener(new UnityAction<string>(this.OnNickNameValueChanged));
			this.m_InputField.onEndEdit.RemoveAllListeners();
			this.m_InputField.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditNickName));
			NKCUtil.SetHotkey(this.m_btnOK, HotkeyEventType.Confirm, null, false);
		}

		// Token: 0x060075EB RID: 30187 RVA: 0x00273673 File Offset: 0x00271873
		public void Open(NKCPopupNickname.OnButton onClose = null)
		{
			base.gameObject.SetActive(true);
			this.m_InputField.text = "";
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			this.m_dOnClose = onClose;
			base.UIOpened(true);
		}

		// Token: 0x060075EC RID: 30188 RVA: 0x002736AA File Offset: 0x002718AA
		public void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x060075ED RID: 30189 RVA: 0x002736C0 File Offset: 0x002718C0
		public void OnOK()
		{
			if (!NKM_USER_COMMON.CheckNickName(this.m_InputField.text))
			{
				if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.NICKNAME_LIMIT_ENG))
				{
					NKCPacketHandlers.Check_NKM_ERROR_CODE(NKM_ERROR_CODE.NEC_FAIL_ACCOUNT_INVALID_NICKNAME_LENGTH_GLOBAL, true, null, int.MinValue);
					return;
				}
				NKCPacketHandlers.Check_NKM_ERROR_CODE(NKM_ERROR_CODE.NEC_FAIL_ACCOUNT_INVALID_NICKNAME_LENGTH, true, null, int.MinValue);
				return;
			}
			else
			{
				if (!NKCFilterManager.CheckNickNameFilter(this.m_InputField.text))
				{
					NKCPacketHandlers.Check_NKM_ERROR_CODE(NKM_ERROR_CODE.NEC_FAIL_ACCOUNT_INVALID_NICKNAME_FILTER, true, null, int.MinValue);
					return;
				}
				for (int i = 0; i < this.m_InputField.text.Length; i++)
				{
					if (char.IsWhiteSpace(this.m_InputField.text[i]))
					{
						NKCPacketHandlers.Check_NKM_ERROR_CODE(NKM_ERROR_CODE.NEC_FAIL_ACCOUNT_INVALID_NICKNAME_FILTER, true, null, int.MinValue);
						return;
					}
				}
				if (string.Equals(NKCScenManager.CurrentUserData().m_UserNickName, this.m_InputField.text))
				{
					NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCPacketHandlers.GetErrorMessage(NKM_ERROR_CODE.NEC_FAIL_ACCOUNT_INVALID_NICKNAME_SAME), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
					return;
				}
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				if (nkmuserData != null)
				{
					long countMiscItem = nkmuserData.m_InventoryData.GetCountMiscItem(510);
					string.Format("1/{0}", countMiscItem);
					string itemName = NKMItemManager.GetItemMiscTempletByID(510).GetItemName();
					NKCPopupResourceConfirmBox.Instance.Open(this.m_InputField.text, string.Format(NKCUtilString.GET_STRING_NICKNAME_CHANGE_RECHECK_ONE_PARAM, itemName), 510, 1, delegate()
					{
						this.OnChangeNickname(this.m_InputField.text);
					}, null, false);
				}
				return;
			}
		}

		// Token: 0x060075EE RID: 30190 RVA: 0x0027381E File Offset: 0x00271A1E
		private void OnChangeNickname(string nickname)
		{
			NKMPopUpBox.OpenWaitBox(0f, "");
			NKCPacketSender.Send_NKMPacket_CHANGE_NICKNAME_REQ(nickname);
		}

		// Token: 0x060075EF RID: 30191 RVA: 0x00273835 File Offset: 0x00271A35
		private void OnNickNameValueChanged(string input)
		{
			Log.Debug("NickName : " + input, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Popup/NKCPopupNickname.cs", 167);
		}

		// Token: 0x060075F0 RID: 30192 RVA: 0x00273851 File Offset: 0x00271A51
		private void OnEndEditNickName(string input)
		{
			if (NKCInputManager.IsChatSubmitEnter())
			{
				if (!this.m_btnOK.m_bLock)
				{
					this.OnOK();
				}
				EventSystem.current.SetSelectedGameObject(null);
			}
		}

		// Token: 0x060075F1 RID: 30193 RVA: 0x00273878 File Offset: 0x00271A78
		public void OnClose()
		{
			base.Close();
		}

		// Token: 0x060075F2 RID: 30194 RVA: 0x00273880 File Offset: 0x00271A80
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
			NKCPopupNickname.OnButton dOnClose = this.m_dOnClose;
			if (dOnClose == null)
			{
				return;
			}
			dOnClose();
		}

		// Token: 0x04006253 RID: 25171
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_ok_cancel_box";

		// Token: 0x04006254 RID: 25172
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_NICKNAME_BOX";

		// Token: 0x04006255 RID: 25173
		private static NKCPopupNickname m_Instance;

		// Token: 0x04006256 RID: 25174
		public InputField m_InputField;

		// Token: 0x04006257 RID: 25175
		public NKCUIComStateButton m_btnOK;

		// Token: 0x04006258 RID: 25176
		public NKCUIComStateButton m_btnCancel;

		// Token: 0x04006259 RID: 25177
		public Text m_textDesc1;

		// Token: 0x0400625A RID: 25178
		public Text m_textDesc2;

		// Token: 0x0400625B RID: 25179
		private NKCPopupNickname.OnButton m_dOnClose;

		// Token: 0x0400625C RID: 25180
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x020017D2 RID: 6098
		// (Invoke) Token: 0x0600B44C RID: 46156
		public delegate void OnButton();
	}
}
