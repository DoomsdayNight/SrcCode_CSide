using System;
using Cs.Logging;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000916 RID: 2326
	public class NKCPopupAccountCodeInput : NKCUIBase
	{
		// Token: 0x170010FE RID: 4350
		// (get) Token: 0x06005D12 RID: 23826 RVA: 0x001CB820 File Offset: 0x001C9A20
		public static NKCPopupAccountCodeInput Instance
		{
			get
			{
				if (NKCPopupAccountCodeInput.m_Instance == null)
				{
					NKCPopupAccountCodeInput.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupAccountCodeInput>("AB_UI_NKM_UI_ACCOUNT_LINK", "NKM_UI_POPUP_ACCOUNT_CODE_INPUT", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupAccountCodeInput.CleanupInstance)).GetInstance<NKCPopupAccountCodeInput>();
					NKCPopupAccountCodeInput.m_Instance.InitUI();
				}
				return NKCPopupAccountCodeInput.m_Instance;
			}
		}

		// Token: 0x06005D13 RID: 23827 RVA: 0x001CB86F File Offset: 0x001C9A6F
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupAccountCodeInput.m_Instance != null && NKCPopupAccountCodeInput.m_Instance.IsOpen)
			{
				NKCPopupAccountCodeInput.m_Instance.Close();
			}
		}

		// Token: 0x06005D14 RID: 23828 RVA: 0x001CB894 File Offset: 0x001C9A94
		private static void CleanupInstance()
		{
			NKCPopupAccountCodeInput.m_Instance = null;
		}

		// Token: 0x170010FF RID: 4351
		// (get) Token: 0x06005D15 RID: 23829 RVA: 0x001CB89C File Offset: 0x001C9A9C
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001100 RID: 4352
		// (get) Token: 0x06005D16 RID: 23830 RVA: 0x001CB89F File Offset: 0x001C9A9F
		public override string MenuName
		{
			get
			{
				return "AccountLink";
			}
		}

		// Token: 0x06005D17 RID: 23831 RVA: 0x001CB8A6 File Offset: 0x001C9AA6
		public override void OnBackButton()
		{
			if (this.m_isStartingProcess)
			{
				base.Close();
				return;
			}
			this.OnClickClose();
		}

		// Token: 0x06005D18 RID: 23832 RVA: 0x001CB8BD File Offset: 0x001C9ABD
		public void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06005D19 RID: 23833 RVA: 0x001CB8DC File Offset: 0x001C9ADC
		public void Open(bool isStartingProcess)
		{
			Log.Debug("[SteamLink][CodeInput] Open", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Steam/NKCPopupAccountCodeInput.cs", 76);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			NKCUIComStateButton cancel = this.m_cancel;
			if (cancel != null)
			{
				cancel.PointerClick.RemoveAllListeners();
			}
			if (this.m_inputField != null)
			{
				this.m_inputField.text = "";
				this.m_inputField.inputType = InputField.InputType.AutoCorrect;
				if (isStartingProcess)
				{
					this.m_inputField.keyboardType = TouchScreenKeyboardType.NumberPad;
					this.m_inputField.contentType = InputField.ContentType.IntegerNumber;
				}
				else
				{
					this.m_inputField.keyboardType = TouchScreenKeyboardType.Default;
					this.m_inputField.contentType = InputField.ContentType.Alphanumeric;
				}
			}
			NKCUIComStateButton ok = this.m_ok;
			if (ok != null)
			{
				ok.PointerClick.RemoveAllListeners();
			}
			if (isStartingProcess)
			{
				NKCUIComStateButton ok2 = this.m_ok;
				if (ok2 != null)
				{
					ok2.PointerClick.AddListener(new UnityAction(this.TrySendPublisherCode));
				}
				NKCUIComStateButton cancel2 = this.m_cancel;
				if (cancel2 != null)
				{
					cancel2.PointerClick.AddListener(new UnityAction(base.Close));
				}
				NKCUtil.SetLabelText(this.m_titleText, NKCStringTable.GetString("SI_PF_STEAMLINK_MEMBERSHIP_TITLE", false));
				NKCUtil.SetLabelText(this.m_descriptionText, NKCStringTable.GetString("SI_PF_STEAMLINK_MEMBERSHIP_DESC", false));
			}
			else
			{
				NKCUIComStateButton ok3 = this.m_ok;
				if (ok3 != null)
				{
					ok3.PointerClick.AddListener(new UnityAction(this.TrySendPrivateLinkCode));
				}
				NKCUIComStateButton cancel3 = this.m_cancel;
				if (cancel3 != null)
				{
					cancel3.PointerClick.AddListener(new UnityAction(this.OnClickClose));
				}
				NKCUtil.SetLabelText(this.m_titleText, NKCStringTable.GetString("SI_PF_STEAMLINK_CODE_ENTER_TITLE", false));
				NKCUtil.SetLabelText(this.m_descriptionText, NKCStringTable.GetString("SI_PF_STEAMLINK_CODE_ENTER_DESC", false));
			}
			base.UIOpened(true);
		}

		// Token: 0x06005D1A RID: 23834 RVA: 0x001CBA88 File Offset: 0x001C9C88
		private void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x06005D1B RID: 23835 RVA: 0x001CBA9D File Offset: 0x001C9C9D
		public override void CloseInternal()
		{
			Log.Debug("[SteamLink][CodeInput] CloseInternal", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Steam/NKCPopupAccountCodeInput.cs", 132);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06005D1C RID: 23836 RVA: 0x001CBABF File Offset: 0x001C9CBF
		public void OnClickClose()
		{
			Log.Debug("[SteamLink][CodeInput] OnClickClose", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Steam/NKCPopupAccountCodeInput.cs", 139);
			NKCAccountLinkMgr.CheckForCancelProcess();
		}

		// Token: 0x06005D1D RID: 23837 RVA: 0x001CBADC File Offset: 0x001C9CDC
		public void TrySendPublisherCode()
		{
			string text = "";
			if (this.m_inputField != null)
			{
				text = this.m_inputField.text;
			}
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			NKCAccountLinkMgr.Send_NKMPacket_BSIDE_ACCOUNT_LINK_REQ(text);
		}

		// Token: 0x06005D1E RID: 23838 RVA: 0x001CBB18 File Offset: 0x001C9D18
		public void TrySendPrivateLinkCode()
		{
			string text = "";
			if (this.m_inputField != null)
			{
				text = this.m_inputField.text;
			}
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			NKCAccountLinkMgr.Send_NKMPacket_BSIDE_ACCOUNT_LINK_CODE_REQ(text);
		}

		// Token: 0x0400492B RID: 18731
		private const string DEBUG_HEADER = "[SteamLink][CodeInput]";

		// Token: 0x0400492C RID: 18732
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_ACCOUNT_LINK";

		// Token: 0x0400492D RID: 18733
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_ACCOUNT_CODE_INPUT";

		// Token: 0x0400492E RID: 18734
		private static NKCPopupAccountCodeInput m_Instance;

		// Token: 0x0400492F RID: 18735
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x04004930 RID: 18736
		public NKCUIComStateButton m_ok;

		// Token: 0x04004931 RID: 18737
		public NKCUIComStateButton m_cancel;

		// Token: 0x04004932 RID: 18738
		public Text m_titleText;

		// Token: 0x04004933 RID: 18739
		public Text m_descriptionText;

		// Token: 0x04004934 RID: 18740
		public InputField m_inputField;

		// Token: 0x04004935 RID: 18741
		private bool m_isStartingProcess = true;
	}
}
