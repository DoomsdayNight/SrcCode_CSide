using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A5A RID: 2650
	public class NKCPopupFirstRunOptionSetup : NKCUIBase
	{
		// Token: 0x17001362 RID: 4962
		// (get) Token: 0x06007446 RID: 29766 RVA: 0x0026AB44 File Offset: 0x00268D44
		public static NKCPopupFirstRunOptionSetup Instance
		{
			get
			{
				if (NKCPopupFirstRunOptionSetup.m_Instance == null)
				{
					NKCPopupFirstRunOptionSetup.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupFirstRunOptionSetup>("ab_ui_nkm_ui_game_option", "NKM_UI_GAME_OPTION_SETTING", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupFirstRunOptionSetup.CleanupInstance)).GetInstance<NKCPopupFirstRunOptionSetup>();
					NKCPopupFirstRunOptionSetup.m_Instance.InitUI();
				}
				return NKCPopupFirstRunOptionSetup.m_Instance;
			}
		}

		// Token: 0x17001363 RID: 4963
		// (get) Token: 0x06007447 RID: 29767 RVA: 0x0026AB93 File Offset: 0x00268D93
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupFirstRunOptionSetup.m_Instance != null && NKCPopupFirstRunOptionSetup.m_Instance.IsOpen;
			}
		}

		// Token: 0x06007448 RID: 29768 RVA: 0x0026ABAE File Offset: 0x00268DAE
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupFirstRunOptionSetup.m_Instance != null && NKCPopupFirstRunOptionSetup.m_Instance.IsOpen)
			{
				NKCPopupFirstRunOptionSetup.m_Instance.Close();
			}
		}

		// Token: 0x06007449 RID: 29769 RVA: 0x0026ABD3 File Offset: 0x00268DD3
		private static void CleanupInstance()
		{
			NKCPopupFirstRunOptionSetup.m_Instance = null;
		}

		// Token: 0x0600744A RID: 29770 RVA: 0x0026ABDB File Offset: 0x00268DDB
		public static bool IsOptionSetupRequired()
		{
			if (NKCUtil.IsUsingSuperUserFunction() && Input.GetKey(KeyCode.Space))
			{
				return true;
			}
			if (!NKCPopupFirstRunOptionSetup.m_sOptionSetupRequired)
			{
				return false;
			}
			if (PlayerPrefs.HasKey("FIRST_OPTION_SETUP_KEY"))
			{
				NKCPopupFirstRunOptionSetup.m_sOptionSetupRequired = false;
				return false;
			}
			return true;
		}

		// Token: 0x0600744B RID: 29771 RVA: 0x0026AC0D File Offset: 0x00268E0D
		public static void ResetSetupReq()
		{
			NKCPopupFirstRunOptionSetup.m_sOptionSetupRequired = true;
			PlayerPrefs.DeleteKey("FIRST_OPTION_SETUP_KEY");
		}

		// Token: 0x0600744C RID: 29772 RVA: 0x0026AC20 File Offset: 0x00268E20
		private void InitUI()
		{
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglGameSimple, new UnityAction<bool>(this.OnGameSimple));
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglGameFullInfo, new UnityAction<bool>(this.OnGameFullInfo));
			NKCUtil.SetButtonClickDelegate(this.m_btnOK, new UnityAction(this.OnBtnOK));
			NKCUtil.SetHotkey(this.m_btnOK, HotkeyEventType.Confirm);
			NKCUtil.SetSliderValueChangedDelegate(this.m_sldrEffectTransparency, new UnityAction<float>(this.OnEffectTransparencyChanged));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnEffectPlus, new UnityAction(this.OnEffectPlus));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnEffectMinus, new UnityAction(this.OnEffectMinus));
		}

		// Token: 0x17001364 RID: 4964
		// (get) Token: 0x0600744D RID: 29773 RVA: 0x0026ACC3 File Offset: 0x00268EC3
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001365 RID: 4965
		// (get) Token: 0x0600744E RID: 29774 RVA: 0x0026ACC6 File Offset: 0x00268EC6
		public override string MenuName
		{
			get
			{
				return NKCStringTable.GetString("SI_DP_POPUP_FIRST_OPTION", false);
			}
		}

		// Token: 0x0600744F RID: 29775 RVA: 0x0026ACD3 File Offset: 0x00268ED3
		public override void OnBackButton()
		{
			this.OnBtnOK();
		}

		// Token: 0x06007450 RID: 29776 RVA: 0x0026ACDB File Offset: 0x00268EDB
		public override void CloseInternal()
		{
			this.SetOptionFromSelection();
			base.gameObject.SetActive(false);
			NKCPopupFirstRunOptionSetup.OnClose dOnClose = this.m_dOnClose;
			if (dOnClose == null)
			{
				return;
			}
			dOnClose();
		}

		// Token: 0x06007451 RID: 29777 RVA: 0x0026AD00 File Offset: 0x00268F00
		public void Open(NKCPopupFirstRunOptionSetup.OnClose onClose = null)
		{
			this.SetState(NKCPopupFirstRunOptionSetup.eUIState.GraphicOption);
			if (this.m_tglGameSimple != null)
			{
				this.m_tglGameSimple.Select(true, false, false);
			}
			if (this.m_tglGraphicMid != null)
			{
				this.m_tglGraphicMid.Select(true, false, false);
			}
			if (this.m_sldrEffectTransparency != null)
			{
				this.m_sldrEffectTransparency.value = this.m_sldrEffectTransparency.maxValue;
			}
			this.m_dOnClose = onClose;
			base.UIOpened(true);
		}

		// Token: 0x06007452 RID: 29778 RVA: 0x0026AD80 File Offset: 0x00268F80
		private void SetState(NKCPopupFirstRunOptionSetup.eUIState state)
		{
			this.m_eCurrentState = state;
			NKCUtil.SetGameobjectActive(this.m_objGraphicRoot, state == NKCPopupFirstRunOptionSetup.eUIState.GraphicOption);
			NKCUtil.SetGameobjectActive(this.m_objGameRoot, state == NKCPopupFirstRunOptionSetup.eUIState.GameOption);
			NKCUtil.SetGameobjectActive(this.m_objEffectRoot, state == NKCPopupFirstRunOptionSetup.eUIState.EffectTransparency);
			switch (state)
			{
			case NKCPopupFirstRunOptionSetup.eUIState.GraphicOption:
				NKCUtil.SetLabelText(this.m_lbTitle, NKCStringTable.GetString("SI_DP_POPUP_FIRST_OPTION_GRAPHIC_TITLE", false));
				NKCUtil.SetLabelText(this.m_lbSubTitle, NKCStringTable.GetString("SI_DP_POPUP_FIRST_OPTION_GRAPHIC_SUBTITLE", false));
				return;
			case NKCPopupFirstRunOptionSetup.eUIState.GameOption:
				NKCUtil.SetLabelText(this.m_lbTitle, NKCStringTable.GetString("SI_DP_POPUP_FIRST_OPTION_GAME_TITLE", false));
				NKCUtil.SetLabelText(this.m_lbSubTitle, NKCStringTable.GetString("SI_DP_POPUP_FIRST_OPTION_GAME_SUBTITLE", false));
				return;
			case NKCPopupFirstRunOptionSetup.eUIState.EffectTransparency:
				NKCUtil.SetLabelText(this.m_lbTitle, NKCStringTable.GetString("SI_DP_POPUP_FIRST_OPTION_EFFECT_TITLE", false));
				NKCUtil.SetLabelText(this.m_lbSubTitle, NKCStringTable.GetString("SI_DP_POPUP_FIRST_OPTION_EFFECT_SUBTITLE", false));
				return;
			default:
				return;
			}
		}

		// Token: 0x06007453 RID: 29779 RVA: 0x0026AE5C File Offset: 0x0026905C
		private void OnBtnOK()
		{
			switch (this.m_eCurrentState)
			{
			case NKCPopupFirstRunOptionSetup.eUIState.GraphicOption:
				this.SetState(NKCPopupFirstRunOptionSetup.eUIState.GameOption);
				return;
			case NKCPopupFirstRunOptionSetup.eUIState.GameOption:
				this.SetState(NKCPopupFirstRunOptionSetup.eUIState.EffectTransparency);
				return;
			case NKCPopupFirstRunOptionSetup.eUIState.EffectTransparency:
				base.Close();
				return;
			default:
				return;
			}
		}

		// Token: 0x06007454 RID: 29780 RVA: 0x0026AE9C File Offset: 0x0026909C
		private void SetOptionFromSelection()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (this.m_tglGraphicHigh.m_bSelect)
			{
				Debug.Log("Graphic option : High");
				gameOptionData.SetGameOptionDataByGrahpicQuality(NKC_GAME_OPTION_GRAPHIC_QUALITY.VERY_HIGH);
			}
			else if (this.m_tglGraphicMid.m_bSelect)
			{
				Debug.Log("Graphic option : Mid");
				gameOptionData.SetGameOptionDataByGrahpicQuality(NKC_GAME_OPTION_GRAPHIC_QUALITY.NORMAL);
			}
			else
			{
				Debug.Log("Graphic option : Low");
				gameOptionData.SetGameOptionDataByGrahpicQuality(NKC_GAME_OPTION_GRAPHIC_QUALITY.VERY_LOW);
			}
			if (this.m_tglGameSimple != null && this.m_tglGameSimple.m_bSelect)
			{
				Debug.Log("Game Option : Simple");
				gameOptionData.UseDamageAndBuffNumberFx = NKCGameOptionDataSt.GameOptionDamageNumber.Limited;
				gameOptionData.UseClassGuide = true;
			}
			else
			{
				Debug.Log("Game Option : Full");
				gameOptionData.UseDamageAndBuffNumberFx = NKCGameOptionDataSt.GameOptionDamageNumber.On;
				gameOptionData.UseClassGuide = true;
			}
			if (this.m_sldrEffectTransparency != null)
			{
				int effectOpacity = (int)this.m_sldrEffectTransparency.value;
				Debug.Log("Effect Transparency : " + effectOpacity.ToString());
				gameOptionData.EffectOpacity = effectOpacity;
			}
			gameOptionData.Save();
			gameOptionData.ApplyToGame();
			PlayerPrefs.SetInt("FIRST_OPTION_SETUP_KEY", 0);
			PlayerPrefs.Save();
		}

		// Token: 0x06007455 RID: 29781 RVA: 0x0026AFA6 File Offset: 0x002691A6
		private void OnChangeGameOption(bool bSimple)
		{
			NKCUtil.SetGameobjectActive(this.m_objGameSimple, bSimple);
			NKCUtil.SetGameobjectActive(this.m_objGameFullInfo, !bSimple);
		}

		// Token: 0x06007456 RID: 29782 RVA: 0x0026AFC3 File Offset: 0x002691C3
		private void OnGameSimple(bool bValue)
		{
			if (bValue)
			{
				this.OnChangeGameOption(true);
			}
		}

		// Token: 0x06007457 RID: 29783 RVA: 0x0026AFCF File Offset: 0x002691CF
		private void OnGameFullInfo(bool bValue)
		{
			if (bValue)
			{
				this.OnChangeGameOption(false);
			}
		}

		// Token: 0x06007458 RID: 29784 RVA: 0x0026AFDB File Offset: 0x002691DB
		private void OnEffectPlus()
		{
			if (this.m_sldrEffectTransparency != null)
			{
				this.SetEffectTransparencyValue(this.m_sldrEffectTransparency.value + 1f);
			}
		}

		// Token: 0x06007459 RID: 29785 RVA: 0x0026B002 File Offset: 0x00269202
		private void OnEffectMinus()
		{
			if (this.m_sldrEffectTransparency != null)
			{
				this.SetEffectTransparencyValue(this.m_sldrEffectTransparency.value - 1f);
			}
		}

		// Token: 0x0600745A RID: 29786 RVA: 0x0026B029 File Offset: 0x00269229
		private void OnEffectTransparencyChanged(float value)
		{
			this.SetEffectTransparencyValue(value);
		}

		// Token: 0x0600745B RID: 29787 RVA: 0x0026B034 File Offset: 0x00269234
		private void SetEffectTransparencyValue(float value)
		{
			if (this.m_sldrEffectTransparency != null)
			{
				this.m_sldrEffectTransparency.SetValueWithoutNotify(value);
			}
			if (this.m_imgEffectTransparency != null)
			{
				float num = value / 100f;
				this.m_imgEffectTransparency.color = new Color(1f, 1f, 1f, num * num);
			}
		}

		// Token: 0x040060A5 RID: 24741
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_game_option";

		// Token: 0x040060A6 RID: 24742
		private const string UI_ASSET_NAME = "NKM_UI_GAME_OPTION_SETTING";

		// Token: 0x040060A7 RID: 24743
		private static NKCPopupFirstRunOptionSetup m_Instance;

		// Token: 0x040060A8 RID: 24744
		[Header("상단")]
		public Text m_lbTitle;

		// Token: 0x040060A9 RID: 24745
		public Text m_lbSubTitle;

		// Token: 0x040060AA RID: 24746
		[Header("그래픽 옵션 설정")]
		public GameObject m_objGraphicRoot;

		// Token: 0x040060AB RID: 24747
		public NKCUIComToggle m_tglGraphicLow;

		// Token: 0x040060AC RID: 24748
		public NKCUIComToggle m_tglGraphicMid;

		// Token: 0x040060AD RID: 24749
		public NKCUIComToggle m_tglGraphicHigh;

		// Token: 0x040060AE RID: 24750
		[Header("게임 옵션 설정")]
		public GameObject m_objGameRoot;

		// Token: 0x040060AF RID: 24751
		public GameObject m_objGameSimple;

		// Token: 0x040060B0 RID: 24752
		public GameObject m_objGameFullInfo;

		// Token: 0x040060B1 RID: 24753
		public NKCUIComToggle m_tglGameSimple;

		// Token: 0x040060B2 RID: 24754
		public NKCUIComToggle m_tglGameFullInfo;

		// Token: 0x040060B3 RID: 24755
		[Header("이펙트 투명도 설정")]
		public GameObject m_objEffectRoot;

		// Token: 0x040060B4 RID: 24756
		public Image m_imgEffectTransparency;

		// Token: 0x040060B5 RID: 24757
		public Slider m_sldrEffectTransparency;

		// Token: 0x040060B6 RID: 24758
		public NKCUIComStateButton m_csbtnEffectMinus;

		// Token: 0x040060B7 RID: 24759
		public NKCUIComStateButton m_csbtnEffectPlus;

		// Token: 0x040060B8 RID: 24760
		public NKCUIComButton m_btnOK;

		// Token: 0x040060B9 RID: 24761
		private NKCPopupFirstRunOptionSetup.eUIState m_eCurrentState;

		// Token: 0x040060BA RID: 24762
		private static bool m_sOptionSetupRequired = true;

		// Token: 0x040060BB RID: 24763
		private const string FIRST_OPTION_SETUP_KEY = "FIRST_OPTION_SETUP_KEY";

		// Token: 0x040060BC RID: 24764
		private NKCPopupFirstRunOptionSetup.OnClose m_dOnClose;

		// Token: 0x020017B1 RID: 6065
		private enum eUIState
		{
			// Token: 0x0400A74E RID: 42830
			GraphicOption,
			// Token: 0x0400A74F RID: 42831
			GameOption,
			// Token: 0x0400A750 RID: 42832
			EffectTransparency
		}

		// Token: 0x020017B2 RID: 6066
		// (Invoke) Token: 0x0600B3FD RID: 46077
		public delegate void OnClose();
	}
}
