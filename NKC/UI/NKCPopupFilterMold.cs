using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace NKC.UI
{
	// Token: 0x02000A4E RID: 2638
	public class NKCPopupFilterMold : NKCUIBase
	{
		// Token: 0x1700134F RID: 4943
		// (get) Token: 0x060073CA RID: 29642 RVA: 0x00268338 File Offset: 0x00266538
		public static NKCPopupFilterMold Instance
		{
			get
			{
				if (NKCPopupFilterMold.m_Instance == null)
				{
					NKCPopupFilterMold.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupFilterMold>("ab_ui_nkm_ui_popup_selection", "NKM_UI_POPUP_FILTER_MOLD", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupFilterMold.CleanupInstance)).GetInstance<NKCPopupFilterMold>();
					NKCPopupFilterMold.m_Instance.Init();
				}
				return NKCPopupFilterMold.m_Instance;
			}
		}

		// Token: 0x060073CB RID: 29643 RVA: 0x00268387 File Offset: 0x00266587
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupFilterMold.m_Instance != null && NKCPopupFilterMold.m_Instance.IsOpen)
			{
				NKCPopupFilterMold.m_Instance.Close();
			}
		}

		// Token: 0x060073CC RID: 29644 RVA: 0x002683AC File Offset: 0x002665AC
		private static void CleanupInstance()
		{
			NKCPopupFilterMold.m_Instance = null;
		}

		// Token: 0x17001350 RID: 4944
		// (get) Token: 0x060073CD RID: 29645 RVA: 0x002683B4 File Offset: 0x002665B4
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001351 RID: 4945
		// (get) Token: 0x060073CE RID: 29646 RVA: 0x002683B7 File Offset: 0x002665B7
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Invalid;
			}
		}

		// Token: 0x17001352 RID: 4946
		// (get) Token: 0x060073CF RID: 29647 RVA: 0x002683BA File Offset: 0x002665BA
		public override string MenuName
		{
			get
			{
				return "필 터";
			}
		}

		// Token: 0x060073D0 RID: 29648 RVA: 0x002683C1 File Offset: 0x002665C1
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060073D1 RID: 29649 RVA: 0x002683D0 File Offset: 0x002665D0
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

		// Token: 0x060073D2 RID: 29650 RVA: 0x002684A0 File Offset: 0x002666A0
		public void Open(HashSet<NKCMoldSortSystem.eFilterOption> setFilterOption, NKCPopupFilterMold.OnMoldFilterSetChange onMoldFilterSetChange, List<string> lstFilter)
		{
			if (!this.m_bInitComplete)
			{
				this.Init();
			}
			this.dOnMoldFilterSetChange = onMoldFilterSetChange;
			this.m_setMoldFilterOption = setFilterOption;
			this.m_cNKCPopupFilterSubUIMold.OpenFilterPopup(this.m_setMoldFilterOption, new NKCPopupFilterSubUIMold.OnFilterOptionChange(this.OnSelectFilterOption), lstFilter, false);
			base.UIOpened(true);
		}

		// Token: 0x060073D3 RID: 29651 RVA: 0x002684F0 File Offset: 0x002666F0
		public void OnSelectFilterOption(NKCMoldSortSystem.eFilterOption selectOption)
		{
			if (this.m_setMoldFilterOption == null)
			{
				this.m_setMoldFilterOption = new HashSet<NKCMoldSortSystem.eFilterOption>();
			}
			if (this.m_setMoldFilterOption.Contains(selectOption))
			{
				this.m_setMoldFilterOption.Remove(selectOption);
			}
			else
			{
				if (selectOption == NKCMoldSortSystem.eFilterOption.Mold_Status_Enable)
				{
					this.m_setMoldFilterOption.Remove(NKCMoldSortSystem.eFilterOption.Mold_Status_Disable);
				}
				else if (selectOption == NKCMoldSortSystem.eFilterOption.Mold_Status_Disable)
				{
					this.m_setMoldFilterOption.Remove(NKCMoldSortSystem.eFilterOption.Mold_Status_Enable);
				}
				this.m_cNKCPopupFilterSubUIMold.ResetMoldPartFilter(selectOption);
				this.m_setMoldFilterOption.Add(selectOption);
			}
			NKCPopupFilterMold.OnMoldFilterSetChange onMoldFilterSetChange = this.dOnMoldFilterSetChange;
			if (onMoldFilterSetChange == null)
			{
				return;
			}
			onMoldFilterSetChange(this.m_setMoldFilterOption);
		}

		// Token: 0x060073D4 RID: 29652 RVA: 0x00268584 File Offset: 0x00266784
		public void OnClickOk()
		{
			base.Close();
		}

		// Token: 0x060073D5 RID: 29653 RVA: 0x0026858C File Offset: 0x0026678C
		public void OnClickReset()
		{
			this.m_cNKCPopupFilterSubUIMold.ResetFilter();
			this.m_setMoldFilterOption.Clear();
			NKCPopupFilterMold.OnMoldFilterSetChange onMoldFilterSetChange = this.dOnMoldFilterSetChange;
			if (onMoldFilterSetChange == null)
			{
				return;
			}
			onMoldFilterSetChange(this.m_setMoldFilterOption);
		}

		// Token: 0x04005FC4 RID: 24516
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_selection";

		// Token: 0x04005FC5 RID: 24517
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_FILTER_MOLD";

		// Token: 0x04005FC6 RID: 24518
		private static NKCPopupFilterMold m_Instance;

		// Token: 0x04005FC7 RID: 24519
		public NKCUIComStateButton m_btnBackground;

		// Token: 0x04005FC8 RID: 24520
		public NKCUIComStateButton m_btnOk;

		// Token: 0x04005FC9 RID: 24521
		public NKCUIComStateButton m_btnReset;

		// Token: 0x04005FCA RID: 24522
		public NKCPopupFilterSubUIMold m_cNKCPopupFilterSubUIMold;

		// Token: 0x04005FCB RID: 24523
		private NKCPopupFilterMold.OnMoldFilterSetChange dOnMoldFilterSetChange;

		// Token: 0x04005FCC RID: 24524
		private HashSet<NKCMoldSortSystem.eFilterOption> m_setMoldFilterOption;

		// Token: 0x04005FCD RID: 24525
		private bool m_bInitComplete;

		// Token: 0x0200179B RID: 6043
		public enum FILTER_OPEN_TYPE
		{
			// Token: 0x0400A729 RID: 42793
			NORMAL,
			// Token: 0x0400A72A RID: 42794
			COLLECTION,
			// Token: 0x0400A72B RID: 42795
			SELECTION
		}

		// Token: 0x0200179C RID: 6044
		// (Invoke) Token: 0x0600B3C7 RID: 46023
		public delegate void OnMoldFilterSetChange(HashSet<NKCMoldSortSystem.eFilterOption> setFilterOption);
	}
}
