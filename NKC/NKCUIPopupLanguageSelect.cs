using System;
using System.Collections.Generic;
using System.Linq;
using NKC.Publisher;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A94 RID: 2708
	public class NKCUIPopupLanguageSelect : NKCUIBase
	{
		// Token: 0x17001413 RID: 5139
		// (get) Token: 0x060077E0 RID: 30688 RVA: 0x0027D42C File Offset: 0x0027B62C
		public static NKCUIPopupLanguageSelect Instance
		{
			get
			{
				if (NKCUIPopupLanguageSelect.m_Instance == null)
				{
					NKCUIPopupLanguageSelect.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPopupLanguageSelect>("ab_ui_nkm_ui_popup_ok_cancel_box", "NKM_UI_POPUP_LANGUAGE", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPopupLanguageSelect.CleanupInstance)).GetInstance<NKCUIPopupLanguageSelect>();
					NKCUIPopupLanguageSelect.m_Instance.Init();
				}
				return NKCUIPopupLanguageSelect.m_Instance;
			}
		}

		// Token: 0x060077E1 RID: 30689 RVA: 0x0027D47B File Offset: 0x0027B67B
		private static void CleanupInstance()
		{
			NKCUIPopupLanguageSelect.m_Instance = null;
		}

		// Token: 0x17001414 RID: 5140
		// (get) Token: 0x060077E2 RID: 30690 RVA: 0x0027D483 File Offset: 0x0027B683
		public override NKCUIManager.eUIUnloadFlag UnloadFlag
		{
			get
			{
				return NKCUIManager.eUIUnloadFlag.DEFAULT;
			}
		}

		// Token: 0x17001415 RID: 5141
		// (get) Token: 0x060077E3 RID: 30691 RVA: 0x0027D486 File Offset: 0x0027B686
		public static bool HasInstance
		{
			get
			{
				return NKCUIPopupLanguageSelect.m_Instance != null;
			}
		}

		// Token: 0x17001416 RID: 5142
		// (get) Token: 0x060077E4 RID: 30692 RVA: 0x0027D493 File Offset: 0x0027B693
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIPopupLanguageSelect.m_Instance != null && NKCUIPopupLanguageSelect.m_Instance.IsOpen;
			}
		}

		// Token: 0x17001417 RID: 5143
		// (get) Token: 0x060077E5 RID: 30693 RVA: 0x0027D4AE File Offset: 0x0027B6AE
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001418 RID: 5144
		// (get) Token: 0x060077E6 RID: 30694 RVA: 0x0027D4B1 File Offset: 0x0027B6B1
		public override string MenuName
		{
			get
			{
				return "Language Select";
			}
		}

		// Token: 0x060077E7 RID: 30695 RVA: 0x0027D4B8 File Offset: 0x0027B6B8
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPopupLanguageSelect.m_Instance != null && NKCUIPopupLanguageSelect.m_Instance.IsOpen)
			{
				NKCUIPopupLanguageSelect.m_Instance.Close();
			}
		}

		// Token: 0x060077E8 RID: 30696 RVA: 0x0027D4E0 File Offset: 0x0027B6E0
		public void Init()
		{
			if (this.m_csbtnOK != null)
			{
				this.m_csbtnOK.PointerClick.RemoveAllListeners();
				this.m_csbtnOK.PointerClick.AddListener(new UnityAction(base.Close));
			}
			if (this.m_closeButton != null)
			{
				this.m_closeButton.PointerClick.RemoveAllListeners();
				this.m_closeButton.PointerClick.AddListener(new UnityAction(this.CloseWithoutCallback));
			}
		}

		// Token: 0x060077E9 RID: 30697 RVA: 0x0027D564 File Offset: 0x0027B764
		private void MakeButtons(HashSet<NKM_NATIONAL_CODE> setLanguages)
		{
			foreach (NKM_NATIONAL_CODE nkm_NATIONAL_CODE in setLanguages)
			{
				if (!this.m_dicLanguageButton.ContainsKey(nkm_NATIONAL_CODE))
				{
					NKCUIComToggle nkcuicomToggle = UnityEngine.Object.Instantiate<NKCUIComToggle>(this.m_tglButtonOrg, this.m_tgrpLanguageButton.transform);
					nkcuicomToggle.SetToggleGroup(this.m_tgrpLanguageButton);
					nkcuicomToggle.m_DataInt = (int)nkm_NATIONAL_CODE;
					nkcuicomToggle.OnValueChangedWithData = new NKCUIComToggle.ValueChangedWithData(this.OnTglLanguage);
					if (nkcuicomToggle.m_ButtonBG_Normal != null)
					{
						NKCUtil.SetImageSprite(nkcuicomToggle.m_ButtonBG_Normal.GetComponent<Image>(), Resources.Load<Sprite>("LANGUAGE/Toggle_Off" + NKCStringTable.GetNationalPostfix(nkm_NATIONAL_CODE)), false);
					}
					if (nkcuicomToggle.m_ButtonBG_Selected != null)
					{
						NKCUtil.SetImageSprite(nkcuicomToggle.m_ButtonBG_Selected.GetComponent<Image>(), Resources.Load<Sprite>("LANGUAGE/Toggle_On" + NKCStringTable.GetNationalPostfix(nkm_NATIONAL_CODE)), false);
					}
					this.m_dicLanguageButton.Add(nkm_NATIONAL_CODE, nkcuicomToggle);
				}
			}
			foreach (KeyValuePair<NKM_NATIONAL_CODE, NKCUIComToggle> keyValuePair in this.m_dicLanguageButton)
			{
				NKCUtil.SetGameobjectActive(keyValuePair.Value, setLanguages.Contains(keyValuePair.Key));
			}
		}

		// Token: 0x060077EA RID: 30698 RVA: 0x0027D6C8 File Offset: 0x0027B8C8
		public void Open(HashSet<NKM_NATIONAL_CODE> setLanguages, NKCUIPopupLanguageSelect.OnClose onClose)
		{
			this.dOnClose = onClose;
			this.m_eSelectedLanguage = NKCStringTable.GetNationalCode();
			if (!setLanguages.Contains(this.m_eSelectedLanguage))
			{
				if (setLanguages.Count > 0)
				{
					this.m_eSelectedLanguage = setLanguages.First<NKM_NATIONAL_CODE>();
				}
				else
				{
					this.m_eSelectedLanguage = NKCPublisherModule.Localization.GetDefaultLanguage();
				}
			}
			this.MakeButtons(setLanguages);
			NKCUIComToggle nkcuicomToggle;
			if (this.m_dicLanguageButton.TryGetValue(this.m_eSelectedLanguage, out nkcuicomToggle))
			{
				nkcuicomToggle.Select(true, false, false);
			}
			this.SetUIByLanguage(this.m_eSelectedLanguage);
			base.UIOpened(true);
		}

		// Token: 0x060077EB RID: 30699 RVA: 0x0027D755 File Offset: 0x0027B955
		private void OnTglLanguage(bool value, int data)
		{
			if (value)
			{
				this.m_eSelectedLanguage = (NKM_NATIONAL_CODE)data;
				this.SetUIByLanguage(this.m_eSelectedLanguage);
			}
		}

		// Token: 0x060077EC RID: 30700 RVA: 0x0027D770 File Offset: 0x0027B970
		private void SetUIByLanguage(NKM_NATIONAL_CODE code)
		{
			NKCUtil.SetImageSprite(this.m_imageTitle, Resources.Load<Sprite>("LANGUAGE/Title" + NKCStringTable.GetNationalPostfix(code)), false);
			if (this.m_csbtnOK != null)
			{
				NKCUtil.SetImageSprite(this.m_imageOkBtn, Resources.Load<Sprite>("LANGUAGE/OK" + NKCStringTable.GetNationalPostfix(code)), false);
			}
		}

		// Token: 0x060077ED RID: 30701 RVA: 0x0027D7CD File Offset: 0x0027B9CD
		public void CloseWithoutCallback()
		{
			this.dOnClose = null;
			base.Close();
		}

		// Token: 0x060077EE RID: 30702 RVA: 0x0027D7DC File Offset: 0x0027B9DC
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
			Debug.Log("Language Select : " + this.m_eSelectedLanguage.ToString());
			NKCUIPopupLanguageSelect.OnClose onClose = this.dOnClose;
			if (onClose == null)
			{
				return;
			}
			onClose(this.m_eSelectedLanguage);
		}

		// Token: 0x060077EF RID: 30703 RVA: 0x0027D82C File Offset: 0x0027BA2C
		private string GetTitleText(NKM_NATIONAL_CODE code)
		{
			switch (code)
			{
			default:
				return "언어 선택";
			case NKM_NATIONAL_CODE.NNC_ENG:
				return "Select Language";
			case NKM_NATIONAL_CODE.NNC_JAPAN:
				return "言語";
			case NKM_NATIONAL_CODE.NNC_CENSORED_CHINESE:
			case NKM_NATIONAL_CODE.NNC_SIMPLIFIED_CHINESE:
				return "选择语言";
			case NKM_NATIONAL_CODE.NNC_TRADITIONAL_CHINESE:
				return "選擇語言";
			case NKM_NATIONAL_CODE.NNC_THAILAND:
				return "เลือกภาษา";
			case NKM_NATIONAL_CODE.NNC_VIETNAM:
				return "Chọn Ngôn ngữ";
			}
		}

		// Token: 0x060077F0 RID: 30704 RVA: 0x0027D894 File Offset: 0x0027BA94
		private string GetOKText(NKM_NATIONAL_CODE code)
		{
			switch (code)
			{
			default:
				return "확인";
			case NKM_NATIONAL_CODE.NNC_ENG:
				return "OK";
			case NKM_NATIONAL_CODE.NNC_JAPAN:
				return "確認";
			case NKM_NATIONAL_CODE.NNC_CENSORED_CHINESE:
			case NKM_NATIONAL_CODE.NNC_SIMPLIFIED_CHINESE:
				return "确认";
			case NKM_NATIONAL_CODE.NNC_TRADITIONAL_CHINESE:
				return "確認";
			case NKM_NATIONAL_CODE.NNC_THAILAND:
				return "ตกลง";
			case NKM_NATIONAL_CODE.NNC_VIETNAM:
				return "OK";
			}
		}

		// Token: 0x0400647D RID: 25725
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_ok_cancel_box";

		// Token: 0x0400647E RID: 25726
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_LANGUAGE";

		// Token: 0x0400647F RID: 25727
		private static NKCUIPopupLanguageSelect m_Instance;

		// Token: 0x04006480 RID: 25728
		private Dictionary<NKM_NATIONAL_CODE, NKCUIComToggle> m_dicLanguageButton = new Dictionary<NKM_NATIONAL_CODE, NKCUIComToggle>();

		// Token: 0x04006481 RID: 25729
		public NKCUIComButton m_closeButton;

		// Token: 0x04006482 RID: 25730
		public Image m_imageTitle;

		// Token: 0x04006483 RID: 25731
		public NKCUIComToggle m_tglButtonOrg;

		// Token: 0x04006484 RID: 25732
		public NKCUIComToggleGroup m_tgrpLanguageButton;

		// Token: 0x04006485 RID: 25733
		public NKCUIComStateButton m_csbtnOK;

		// Token: 0x04006486 RID: 25734
		public Image m_imageOkBtn;

		// Token: 0x04006487 RID: 25735
		private NKCUIPopupLanguageSelect.OnClose dOnClose;

		// Token: 0x04006488 RID: 25736
		private NKM_NATIONAL_CODE m_eSelectedLanguage;

		// Token: 0x020017F0 RID: 6128
		// (Invoke) Token: 0x0600B4AC RID: 46252
		public delegate void OnClose(NKM_NATIONAL_CODE selectedLanguage);
	}
}
