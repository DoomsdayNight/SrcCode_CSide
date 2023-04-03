using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace NKC.UI
{
	// Token: 0x02000A4C RID: 2636
	public class NKCPopupFilterEquip : NKCUIBase
	{
		// Token: 0x17001348 RID: 4936
		// (get) Token: 0x060073B1 RID: 29617 RVA: 0x00267E74 File Offset: 0x00266074
		public static NKCPopupFilterEquip Instance
		{
			get
			{
				if (NKCPopupFilterEquip.m_Instance == null)
				{
					NKCPopupFilterEquip.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupFilterEquip>("ab_ui_nkm_ui_popup_selection", "NKM_UI_POPUP_FILTER_EQUIP", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupFilterEquip.CleanupInstance)).GetInstance<NKCPopupFilterEquip>();
					NKCPopupFilterEquip.m_Instance.Init();
				}
				return NKCPopupFilterEquip.m_Instance;
			}
		}

		// Token: 0x060073B2 RID: 29618 RVA: 0x00267EC3 File Offset: 0x002660C3
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupFilterEquip.m_Instance != null && NKCPopupFilterEquip.m_Instance.IsOpen)
			{
				NKCPopupFilterEquip.m_Instance.Close();
			}
		}

		// Token: 0x060073B3 RID: 29619 RVA: 0x00267EE8 File Offset: 0x002660E8
		private static void CleanupInstance()
		{
			NKCPopupFilterEquip.m_Instance = null;
		}

		// Token: 0x17001349 RID: 4937
		// (get) Token: 0x060073B4 RID: 29620 RVA: 0x00267EF0 File Offset: 0x002660F0
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700134A RID: 4938
		// (get) Token: 0x060073B5 RID: 29621 RVA: 0x00267EF3 File Offset: 0x002660F3
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Invalid;
			}
		}

		// Token: 0x1700134B RID: 4939
		// (get) Token: 0x060073B6 RID: 29622 RVA: 0x00267EF6 File Offset: 0x002660F6
		public override string MenuName
		{
			get
			{
				return "필 터";
			}
		}

		// Token: 0x060073B7 RID: 29623 RVA: 0x00267EFD File Offset: 0x002660FD
		public override void CloseInternal()
		{
			this.m_ssActive = null;
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060073B8 RID: 29624 RVA: 0x00267F14 File Offset: 0x00266114
		private void Init()
		{
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

		// Token: 0x060073B9 RID: 29625 RVA: 0x00267FE4 File Offset: 0x002661E4
		public void Open(HashSet<NKCEquipSortSystem.eFilterCategory> setFilterCategory, NKCEquipSortSystem ssActive, NKCPopupFilterEquip.OnEquipFilterSetChange onFilterSetChange, bool bEnableEnchantModuleFilter = false)
		{
			if (!this.m_bInitComplete)
			{
				this.Init();
			}
			this.dOnEquipFilterSetChange = onFilterSetChange;
			this.m_ssActive = ssActive;
			if (ssActive != null)
			{
				this.m_ssActive.FilterSet = ssActive.m_EquipListOptions.setFilterOption;
			}
			else
			{
				this.m_ssActive.FilterSet = new HashSet<NKCEquipSortSystem.eFilterOption>();
			}
			this.m_cNKCPopupFilterSubUIEquip.OpenFilterPopup(this.m_ssActive, setFilterCategory, new NKCPopupFilterSubUIEquip.OnFilterOptionChange(this.OnSelectFilterOption), bEnableEnchantModuleFilter);
			base.UIOpened(true);
		}

		// Token: 0x060073BA RID: 29626 RVA: 0x0026805F File Offset: 0x0026625F
		public void OnSelectFilterOption(NKCEquipSortSystem ssActive, NKCEquipSortSystem.eFilterOption selectOption)
		{
			if (ssActive != null)
			{
				this.m_ssActive = ssActive;
			}
			NKCPopupFilterEquip.OnEquipFilterSetChange onEquipFilterSetChange = this.dOnEquipFilterSetChange;
			if (onEquipFilterSetChange == null)
			{
				return;
			}
			onEquipFilterSetChange(this.m_ssActive);
		}

		// Token: 0x060073BB RID: 29627 RVA: 0x00268081 File Offset: 0x00266281
		public void OnClickOk()
		{
			base.Close();
		}

		// Token: 0x060073BC RID: 29628 RVA: 0x00268089 File Offset: 0x00266289
		public void OnClickReset()
		{
			this.m_cNKCPopupFilterSubUIEquip.ResetFilter();
			this.m_ssActive.FilterSet.Clear();
			NKCPopupFilterEquip.OnEquipFilterSetChange onEquipFilterSetChange = this.dOnEquipFilterSetChange;
			if (onEquipFilterSetChange == null)
			{
				return;
			}
			onEquipFilterSetChange(this.m_ssActive);
		}

		// Token: 0x04005FB0 RID: 24496
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_selection";

		// Token: 0x04005FB1 RID: 24497
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_FILTER_EQUIP";

		// Token: 0x04005FB2 RID: 24498
		private static NKCPopupFilterEquip m_Instance;

		// Token: 0x04005FB3 RID: 24499
		public NKCUIComStateButton m_btnBackground;

		// Token: 0x04005FB4 RID: 24500
		public NKCUIComStateButton m_btnOk;

		// Token: 0x04005FB5 RID: 24501
		public NKCUIComStateButton m_btnReset;

		// Token: 0x04005FB6 RID: 24502
		public NKCPopupFilterSubUIEquip m_cNKCPopupFilterSubUIEquip;

		// Token: 0x04005FB7 RID: 24503
		private NKCPopupFilterEquip.OnEquipFilterSetChange dOnEquipFilterSetChange;

		// Token: 0x04005FB8 RID: 24504
		private NKCEquipSortSystem m_ssActive;

		// Token: 0x04005FB9 RID: 24505
		private bool m_bInitComplete;

		// Token: 0x02001797 RID: 6039
		public enum FILTER_OPEN_TYPE
		{
			// Token: 0x0400A721 RID: 42785
			NORMAL,
			// Token: 0x0400A722 RID: 42786
			COLLECTION,
			// Token: 0x0400A723 RID: 42787
			SELECTION
		}

		// Token: 0x02001798 RID: 6040
		// (Invoke) Token: 0x0600B3BF RID: 46015
		public delegate void OnEquipFilterSetChange(NKCEquipSortSystem equipSortSystem);
	}
}
