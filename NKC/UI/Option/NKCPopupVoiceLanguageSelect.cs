using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Option
{
	// Token: 0x02000B97 RID: 2967
	public class NKCPopupVoiceLanguageSelect : NKCUIBase
	{
		// Token: 0x170015FE RID: 5630
		// (get) Token: 0x060088F9 RID: 35065 RVA: 0x002E54D0 File Offset: 0x002E36D0
		public static NKCPopupVoiceLanguageSelect Instance
		{
			get
			{
				if (NKCPopupVoiceLanguageSelect.m_Instance == null)
				{
					NKCPopupVoiceLanguageSelect.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupVoiceLanguageSelect>("ab_ui_login_select", "AB_UI_LOGIN_SELECT_VOICE", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupVoiceLanguageSelect.CleanupInstance)).GetInstance<NKCPopupVoiceLanguageSelect>();
					NKCPopupVoiceLanguageSelect.m_Instance.Init(delegate
					{
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString("SI_DP_OPTION_GAME_VOICE_CHANGE_NOTICE", false), null, "");
						PlayerPrefs.SetInt("PatchIntegrityCheck", 1);
					}, false);
				}
				return NKCPopupVoiceLanguageSelect.m_Instance;
			}
		}

		// Token: 0x060088FA RID: 35066 RVA: 0x002E553F File Offset: 0x002E373F
		private static void CleanupInstance()
		{
			NKCPopupVoiceLanguageSelect.m_Instance = null;
		}

		// Token: 0x170015FF RID: 5631
		// (get) Token: 0x060088FB RID: 35067 RVA: 0x002E5547 File Offset: 0x002E3747
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupVoiceLanguageSelect.m_Instance != null && NKCPopupVoiceLanguageSelect.m_Instance.IsOpen;
			}
		}

		// Token: 0x060088FC RID: 35068 RVA: 0x002E5562 File Offset: 0x002E3762
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupVoiceLanguageSelect.m_Instance != null && NKCPopupVoiceLanguageSelect.m_Instance.IsOpen)
			{
				NKCPopupVoiceLanguageSelect.m_Instance.Close();
			}
		}

		// Token: 0x060088FD RID: 35069 RVA: 0x002E5587 File Offset: 0x002E3787
		private void OnDestroy()
		{
			NKCPopupVoiceLanguageSelect.m_Instance = null;
		}

		// Token: 0x060088FE RID: 35070 RVA: 0x002E558F File Offset: 0x002E378F
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x17001600 RID: 5632
		// (get) Token: 0x060088FF RID: 35071 RVA: 0x002E559D File Offset: 0x002E379D
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001601 RID: 5633
		// (get) Token: 0x06008900 RID: 35072 RVA: 0x002E55A0 File Offset: 0x002E37A0
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x06008901 RID: 35073 RVA: 0x002E55A8 File Offset: 0x002E37A8
		public void Init(Action onComplete, bool bPatcher = false)
		{
			NKCUtil.SetGameobjectActive(this.m_csbtnClose, !bPatcher);
			NKCUtil.SetGameobjectActive(this.m_lbDesc, bPatcher);
			foreach (NKCPopupVoiceLanguageSelect.VoiceButton voiceButton in this.m_lstButtons)
			{
				if (voiceButton != null && voiceButton.m_tglButton != null)
				{
					voiceButton.m_tglButton.OnValueChangedWithData = new NKCUIComToggle.ValueChangedWithData(this.OnTglButtonSelect);
					voiceButton.m_tglButton.m_DataInt = (int)voiceButton.m_eVoiceCode;
				}
			}
			NKCUtil.SetButtonClickDelegate(this.m_csbtnClose, new UnityAction(base.Close));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnOK, delegate()
			{
				this.OnOK(onComplete);
			});
		}

		// Token: 0x06008902 RID: 35074 RVA: 0x002E568C File Offset: 0x002E388C
		public void Open()
		{
			HashSet<NKC_VOICE_CODE> hashSet = new HashSet<NKC_VOICE_CODE>(NKCUIVoiceManager.GetAvailableVoiceCode());
			foreach (NKCPopupVoiceLanguageSelect.VoiceButton voiceButton in this.m_lstButtons)
			{
				if (voiceButton != null && !(voiceButton.m_tglButton == null))
				{
					NKCUtil.SetGameobjectActive(voiceButton.m_tglButton, hashSet.Contains(voiceButton.m_eVoiceCode));
					if (NKCUIVoiceManager.CurrentVoiceCode == voiceButton.m_eVoiceCode)
					{
						voiceButton.m_tglButton.Select(true, true, false);
					}
				}
			}
			base.UIOpened(true);
		}

		// Token: 0x06008903 RID: 35075 RVA: 0x002E5730 File Offset: 0x002E3930
		private void OnOK(Action onComplete)
		{
			NKCUIVoiceManager.SetVoiceCode(this.m_eSelectedVoiceCode);
			base.Close();
			if (onComplete != null)
			{
				onComplete();
			}
		}

		// Token: 0x06008904 RID: 35076 RVA: 0x002E574C File Offset: 0x002E394C
		private void OnTglButtonSelect(bool value, int data)
		{
			if (value)
			{
				this.m_eSelectedVoiceCode = (NKC_VOICE_CODE)data;
			}
		}

		// Token: 0x04007575 RID: 30069
		private const string ASSET_BUNDLE_NAME = "ab_ui_login_select";

		// Token: 0x04007576 RID: 30070
		private const string UI_ASSET_NAME = "AB_UI_LOGIN_SELECT_VOICE";

		// Token: 0x04007577 RID: 30071
		private static NKCPopupVoiceLanguageSelect m_Instance;

		// Token: 0x04007578 RID: 30072
		public List<NKCPopupVoiceLanguageSelect.VoiceButton> m_lstButtons;

		// Token: 0x04007579 RID: 30073
		public NKCUIComStateButton m_csbtnOK;

		// Token: 0x0400757A RID: 30074
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x0400757B RID: 30075
		public Text m_lbDesc;

		// Token: 0x0400757C RID: 30076
		private NKC_VOICE_CODE m_eSelectedVoiceCode;

		// Token: 0x0200193E RID: 6462
		[Serializable]
		public class VoiceButton
		{
			// Token: 0x0400AB05 RID: 43781
			public NKC_VOICE_CODE m_eVoiceCode;

			// Token: 0x0400AB06 RID: 43782
			public NKCUIComToggle m_tglButton;
		}
	}
}
