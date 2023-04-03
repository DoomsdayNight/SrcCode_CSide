using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace NKC.UI
{
	// Token: 0x02000A4D RID: 2637
	public class NKCPopupFilterMisc : NKCUIBase
	{
		// Token: 0x1700134C RID: 4940
		// (get) Token: 0x060073BE RID: 29630 RVA: 0x002680C4 File Offset: 0x002662C4
		public static NKCPopupFilterMisc Instance
		{
			get
			{
				if (NKCPopupFilterMisc.m_Instance == null)
				{
					NKCPopupFilterMisc.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupFilterMisc>("AB_UI_FILTER", "AB_UI_FILTER_POPUP_MISC", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupFilterMisc.CleanupInstance)).GetInstance<NKCPopupFilterMisc>();
					NKCPopupFilterMisc.m_Instance.Init();
				}
				return NKCPopupFilterMisc.m_Instance;
			}
		}

		// Token: 0x060073BF RID: 29631 RVA: 0x00268113 File Offset: 0x00266313
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupFilterMisc.m_Instance != null && NKCPopupFilterMisc.m_Instance.IsOpen)
			{
				NKCPopupFilterMisc.m_Instance.Close();
			}
		}

		// Token: 0x060073C0 RID: 29632 RVA: 0x00268138 File Offset: 0x00266338
		private static void CleanupInstance()
		{
			NKCPopupFilterMisc.m_Instance = null;
		}

		// Token: 0x1700134D RID: 4941
		// (get) Token: 0x060073C1 RID: 29633 RVA: 0x00268140 File Offset: 0x00266340
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700134E RID: 4942
		// (get) Token: 0x060073C2 RID: 29634 RVA: 0x00268143 File Offset: 0x00266343
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x060073C3 RID: 29635 RVA: 0x0026814A File Offset: 0x0026634A
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060073C4 RID: 29636 RVA: 0x00268158 File Offset: 0x00266358
		private void Init()
		{
			if (this.m_bInitComplete)
			{
				return;
			}
			if (this.m_btnBackground != null)
			{
				this.m_btnBackground.PointerClick.RemoveAllListeners();
				this.m_btnBackground.PointerClick.AddListener(new UnityAction(this.OnClickOk));
			}
			if (this.m_btnOk != null)
			{
				this.m_btnOk.PointerClick.RemoveAllListeners();
				this.m_btnOk.PointerClick.AddListener(new UnityAction(this.OnClickOk));
				NKCUtil.SetHotkey(this.m_btnOk, HotkeyEventType.Confirm, null, false);
			}
			if (this.m_btnReset != null)
			{
				this.m_btnReset.PointerClick.RemoveAllListeners();
				this.m_btnReset.PointerClick.AddListener(new UnityAction(this.OnClickReset));
			}
			this.m_bInitComplete = true;
		}

		// Token: 0x060073C5 RID: 29637 RVA: 0x00268234 File Offset: 0x00266434
		public void Open(HashSet<NKCMiscSortSystem.eFilterCategory> setFilterCategory, HashSet<NKCMiscSortSystem.eFilterOption> setCurrentFilterOption, NKCPopupFilterMisc.OnMiscFilterSetChange onFilterSetChange, int currentThemeID)
		{
			if (!this.m_bInitComplete)
			{
				this.Init();
			}
			this.dOnMiscFilterSetChange = onFilterSetChange;
			if (setCurrentFilterOption != null)
			{
				this.m_setMiscFilterOption = setCurrentFilterOption;
			}
			else
			{
				this.m_setMiscFilterOption = new HashSet<NKCMiscSortSystem.eFilterOption>();
			}
			this.m_cNKCPopupFilterSubUIMisc.OpenFilterPopup(this.m_setMiscFilterOption, setFilterCategory, new NKCPopupFilterSubUIMisc.OnFilterOptionChange(this.OnSelectFilterOption), currentThemeID);
			base.UIOpened(true);
		}

		// Token: 0x060073C6 RID: 29638 RVA: 0x00268294 File Offset: 0x00266494
		public void OnSelectFilterOption(NKCMiscSortSystem.eFilterOption selectOption, int currentSelectedTheme)
		{
			if (this.m_setMiscFilterOption == null)
			{
				this.m_setMiscFilterOption = new HashSet<NKCMiscSortSystem.eFilterOption>();
			}
			if (this.m_setMiscFilterOption.Contains(selectOption))
			{
				this.m_setMiscFilterOption.Remove(selectOption);
			}
			else
			{
				this.m_setMiscFilterOption.Add(selectOption);
			}
			NKCPopupFilterMisc.OnMiscFilterSetChange onMiscFilterSetChange = this.dOnMiscFilterSetChange;
			if (onMiscFilterSetChange == null)
			{
				return;
			}
			onMiscFilterSetChange(this.m_setMiscFilterOption, currentSelectedTheme);
		}

		// Token: 0x060073C7 RID: 29639 RVA: 0x002682F5 File Offset: 0x002664F5
		public void OnClickOk()
		{
			base.Close();
		}

		// Token: 0x060073C8 RID: 29640 RVA: 0x002682FD File Offset: 0x002664FD
		public void OnClickReset()
		{
			this.m_cNKCPopupFilterSubUIMisc.ResetFilter(true);
			this.m_setMiscFilterOption.Clear();
			NKCPopupFilterMisc.OnMiscFilterSetChange onMiscFilterSetChange = this.dOnMiscFilterSetChange;
			if (onMiscFilterSetChange == null)
			{
				return;
			}
			onMiscFilterSetChange(this.m_setMiscFilterOption, 0);
		}

		// Token: 0x04005FBA RID: 24506
		private const string ASSET_BUNDLE_NAME = "AB_UI_FILTER";

		// Token: 0x04005FBB RID: 24507
		private const string UI_ASSET_NAME = "AB_UI_FILTER_POPUP_MISC";

		// Token: 0x04005FBC RID: 24508
		private static NKCPopupFilterMisc m_Instance;

		// Token: 0x04005FBD RID: 24509
		public NKCUIComStateButton m_btnBackground;

		// Token: 0x04005FBE RID: 24510
		public NKCUIComStateButton m_btnOk;

		// Token: 0x04005FBF RID: 24511
		public NKCUIComStateButton m_btnReset;

		// Token: 0x04005FC0 RID: 24512
		public NKCPopupFilterSubUIMisc m_cNKCPopupFilterSubUIMisc;

		// Token: 0x04005FC1 RID: 24513
		private NKCPopupFilterMisc.OnMiscFilterSetChange dOnMiscFilterSetChange;

		// Token: 0x04005FC2 RID: 24514
		private HashSet<NKCMiscSortSystem.eFilterOption> m_setMiscFilterOption;

		// Token: 0x04005FC3 RID: 24515
		private bool m_bInitComplete;

		// Token: 0x02001799 RID: 6041
		public enum FILTER_TYPE
		{
			// Token: 0x0400A725 RID: 42789
			NONE,
			// Token: 0x0400A726 RID: 42790
			NORMAL,
			// Token: 0x0400A727 RID: 42791
			INTERIOR
		}

		// Token: 0x0200179A RID: 6042
		// (Invoke) Token: 0x0600B3C3 RID: 46019
		public delegate void OnMiscFilterSetChange(HashSet<NKCMiscSortSystem.eFilterOption> setFilterOption, int selectedTheme);
	}
}
