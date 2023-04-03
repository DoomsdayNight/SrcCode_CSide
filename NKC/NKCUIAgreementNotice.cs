using System;
using Cs.Logging;
using NKC.Publisher;
using NKC.UI;
using UnityEngine;
using UnityEngine.Events;

namespace NKC
{
	// Token: 0x0200074C RID: 1868
	public class NKCUIAgreementNotice : MonoBehaviour
	{
		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x06004A8C RID: 19084 RVA: 0x001658F6 File Offset: 0x00163AF6
		public static string PopupMessage
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_TOAST_MESSAGE_CHECK_SERVICE_AGREEMENT", false);
			}
		}

		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x06004A8D RID: 19085 RVA: 0x00165903 File Offset: 0x00163B03
		public static string AgeLimitMessage
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_POPUP_MESSAGE_AGE_LIMIT_NOTICE", false);
			}
		}

		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x06004A8E RID: 19086 RVA: 0x00165910 File Offset: 0x00163B10
		public static string ResetPrivacyPolicyMessage
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_POPUP_MESSAGE_RESET_PRIVACY_POLICY", false);
			}
		}

		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x06004A8F RID: 19087 RVA: 0x0016591D File Offset: 0x00163B1D
		public static string ResetPrivacyPolicySuccess
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_POPUP_MESSAGE_RESET_PRIVACY_POLICY_SUCCESS", false);
			}
		}

		// Token: 0x06004A90 RID: 19088 RVA: 0x0016592A File Offset: 0x00163B2A
		private void Start()
		{
			Log.Debug("[Agreement] InitUI", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Components/NKCUIAgreementNotice.cs", 37);
			this.InitUI();
		}

		// Token: 0x06004A91 RID: 19089 RVA: 0x00165944 File Offset: 0x00163B44
		public void InitUI()
		{
			if (this.m_checkAgreeAll != null)
			{
				this.m_checkAgreeAll.OnValueChanged.RemoveAllListeners();
				this.m_checkAgreeAll.OnValueChanged.AddListener(new UnityAction<bool>(this.OnToggleValueChanged));
			}
			if (this.m_openAgreement != null)
			{
				this.m_openAgreement.PointerClick.RemoveAllListeners();
				this.m_openAgreement.PointerClick.AddListener(new UnityAction(NKCUIAgreementNotice.OnClickAgreement));
			}
			if (this.m_openPrivacyPolicy != null)
			{
				this.m_openPrivacyPolicy.PointerClick.RemoveAllListeners();
				this.m_openPrivacyPolicy.PointerClick.AddListener(new UnityAction(NKCUIAgreementNotice.OnClickPrivacy));
			}
			if (this.m_AgeLimitButton != null)
			{
				this.m_AgeLimitButton.PointerClick.RemoveAllListeners();
				this.m_AgeLimitButton.PointerClick.AddListener(new UnityAction(this.OnClickAgeLimit));
			}
			if (this.m_ageLimitPopupClose != null)
			{
				this.m_ageLimitPopupClose.PointerClick.RemoveAllListeners();
				this.m_ageLimitPopupClose.PointerClick.AddListener(new UnityAction(this.OnClickAgeLimitClose));
			}
			this.OnToggleValueChanged(NKCUIAgreementNotice.IsAgreementChecked());
		}

		// Token: 0x06004A92 RID: 19090 RVA: 0x00165A7E File Offset: 0x00163C7E
		public static bool IsAgreementChecked()
		{
			if (PlayerPrefs.HasKey(NKCUIAgreementNotice.LocalSaveKey) && PlayerPrefs.GetInt(NKCUIAgreementNotice.LocalSaveKey) == 1)
			{
				NKCUIAgreementNotice.m_currentValue = true;
			}
			else
			{
				NKCUIAgreementNotice.m_currentValue = false;
			}
			return NKCUIAgreementNotice.m_currentValue;
		}

		// Token: 0x06004A93 RID: 19091 RVA: 0x00165AAC File Offset: 0x00163CAC
		public void OnToggleValueChanged(bool value)
		{
			Log.Debug(string.Format("[Agreement] Toggle[{0}]", value), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Components/NKCUIAgreementNotice.cs", 93);
			NKCUtil.SetGameobjectActive(this.m_toggleOn, value);
			NKCUtil.SetGameobjectActive(this.m_toggleOff, !value);
			NKCUIAgreementNotice.SetAgreementLocalValue(value);
			NKCUIAgreementNotice.m_toggleValue = value;
			this.m_checkAgreeAll.Select(value, true, true);
		}

		// Token: 0x06004A94 RID: 19092 RVA: 0x00165B0B File Offset: 0x00163D0B
		public static void SetAgreementLocalValue(bool value)
		{
			NKCUIAgreementNotice.m_currentValue = value;
			if (value)
			{
				PlayerPrefs.SetInt(NKCUIAgreementNotice.LocalSaveKey, 1);
				return;
			}
			PlayerPrefs.DeleteKey(NKCUIAgreementNotice.LocalSaveKey);
		}

		// Token: 0x06004A95 RID: 19093 RVA: 0x00165B2C File Offset: 0x00163D2C
		public static void OnClickAgreement()
		{
			NKCPublisherModule.Notice.OpenAgreement(new NKCPublisherModule.OnComplete(NKCUIAgreementNotice.OnCompleteAgreement));
		}

		// Token: 0x06004A96 RID: 19094 RVA: 0x00165B44 File Offset: 0x00163D44
		public static void OnClickPrivacy()
		{
			NKCPublisherModule.Notice.OpenPrivacyPolicy(new NKCPublisherModule.OnComplete(NKCUIAgreementNotice.OnCompletePrivacy));
		}

		// Token: 0x06004A97 RID: 19095 RVA: 0x00165B5C File Offset: 0x00163D5C
		public void OnClickAgeLimit()
		{
			NKCUtil.SetGameobjectActive(this.m_ageLimitPopup, true);
		}

		// Token: 0x06004A98 RID: 19096 RVA: 0x00165B6A File Offset: 0x00163D6A
		public void OnClickAgeLimitClose()
		{
			NKCUtil.SetGameobjectActive(this.m_ageLimitPopup, false);
		}

		// Token: 0x06004A99 RID: 19097 RVA: 0x00165B78 File Offset: 0x00163D78
		private void Update()
		{
			if (NKCUIAgreementNotice.m_toggleValue != NKCUIAgreementNotice.m_currentValue)
			{
				this.OnToggleValueChanged(NKCUIAgreementNotice.m_currentValue);
			}
		}

		// Token: 0x06004A9A RID: 19098 RVA: 0x00165B91 File Offset: 0x00163D91
		private static void OnCompleteAgreement(NKC_PUBLISHER_RESULT_CODE eNKC_PUBLISHER_RESULT_CODE, string additionalError)
		{
			NKCPublisherModule.CheckError(eNKC_PUBLISHER_RESULT_CODE, additionalError, false, null, false);
		}

		// Token: 0x06004A9B RID: 19099 RVA: 0x00165B9E File Offset: 0x00163D9E
		private static void OnCompletePrivacy(NKC_PUBLISHER_RESULT_CODE eNKC_PUBLISHER_RESULT_CODE, string additionalError)
		{
			NKCPublisherModule.CheckError(eNKC_PUBLISHER_RESULT_CODE, additionalError, false, null, false);
		}

		// Token: 0x06004A9C RID: 19100 RVA: 0x00165BAC File Offset: 0x00163DAC
		public static void OnResetAgreement(bool bApplicationQuit)
		{
			Log.Debug("[ResetAgreement] OnResetAgreement", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Components/NKCUIAgreementNotice.cs", 157);
			NKCUIAgreementNotice.SetAgreementLocalValue(false);
			if (bApplicationQuit)
			{
				NKCPublisherModule.Statistics.OnResetAgreement();
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUIAgreementNotice.ResetPrivacyPolicySuccess, delegate()
				{
					Application.Quit();
				}, "");
			}
		}

		// Token: 0x04003967 RID: 14695
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x04003968 RID: 14696
		public NKCUIComToggle m_checkAgreeAll;

		// Token: 0x04003969 RID: 14697
		public NKCUIComButton m_openAgreement;

		// Token: 0x0400396A RID: 14698
		public NKCUIComButton m_openPrivacyPolicy;

		// Token: 0x0400396B RID: 14699
		public GameObject m_toggleOn;

		// Token: 0x0400396C RID: 14700
		public GameObject m_toggleOff;

		// Token: 0x0400396D RID: 14701
		public NKCUIComButton m_AgeLimitButton;

		// Token: 0x0400396E RID: 14702
		public GameObject m_ageLimitPopup;

		// Token: 0x0400396F RID: 14703
		public NKCUIComStateButton m_ageLimitPopupClose;

		// Token: 0x04003970 RID: 14704
		private static bool m_toggleValue = false;

		// Token: 0x04003971 RID: 14705
		private static bool m_currentValue = false;

		// Token: 0x04003972 RID: 14706
		private static string LocalSaveKey = "CHECK_SERVICE_AGREEMENT";
	}
}
