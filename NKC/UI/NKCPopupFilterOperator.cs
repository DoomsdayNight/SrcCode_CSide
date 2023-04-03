using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace NKC.UI
{
	// Token: 0x02000A4F RID: 2639
	public class NKCPopupFilterOperator : NKCUIBase
	{
		// Token: 0x17001353 RID: 4947
		// (get) Token: 0x060073D7 RID: 29655 RVA: 0x002685C4 File Offset: 0x002667C4
		public static NKCPopupFilterOperator Instance
		{
			get
			{
				if (NKCPopupFilterOperator.m_Instance == null)
				{
					NKCPopupFilterOperator.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupFilterOperator>("ab_ui_nkm_ui_popup_selection", "NKM_UI_POPUP_FILTER_OPERATOR", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupFilterOperator.CleanupInstance)).GetInstance<NKCPopupFilterOperator>();
					NKCPopupFilterOperator.m_Instance.Init();
				}
				return NKCPopupFilterOperator.m_Instance;
			}
		}

		// Token: 0x060073D8 RID: 29656 RVA: 0x00268613 File Offset: 0x00266813
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupFilterOperator.m_Instance != null && NKCPopupFilterOperator.m_Instance.IsOpen)
			{
				NKCPopupFilterOperator.m_Instance.Close();
			}
		}

		// Token: 0x060073D9 RID: 29657 RVA: 0x00268638 File Offset: 0x00266838
		private static void CleanupInstance()
		{
			NKCPopupFilterOperator.m_Instance = null;
		}

		// Token: 0x17001354 RID: 4948
		// (get) Token: 0x060073DA RID: 29658 RVA: 0x00268640 File Offset: 0x00266840
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001355 RID: 4949
		// (get) Token: 0x060073DB RID: 29659 RVA: 0x00268643 File Offset: 0x00266843
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Invalid;
			}
		}

		// Token: 0x17001356 RID: 4950
		// (get) Token: 0x060073DC RID: 29660 RVA: 0x00268646 File Offset: 0x00266846
		public override string MenuName
		{
			get
			{
				return "필 터";
			}
		}

		// Token: 0x060073DD RID: 29661 RVA: 0x0026864D File Offset: 0x0026684D
		public override void CloseInternal()
		{
			this.m_cNKCPopupFilterSubUIOperator.Close();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060073DE RID: 29662 RVA: 0x00268666 File Offset: 0x00266866
		public override void OnBackButton()
		{
			if (this.m_cNKCPopupFilterSubUIOperator.IsSubfilterOpened)
			{
				this.m_cNKCPopupFilterSubUIOperator.CloseSubFilter();
				return;
			}
			base.OnBackButton();
		}

		// Token: 0x060073DF RID: 29663 RVA: 0x00268688 File Offset: 0x00266888
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

		// Token: 0x060073E0 RID: 29664 RVA: 0x00268758 File Offset: 0x00266958
		public void Open(NKCOperatorSortSystem ssActive, HashSet<NKCOperatorSortSystem.eFilterCategory> setFilterCategory, NKCPopupFilterOperator.OnFilterSetChange onFilterSetChange)
		{
			if (!this.m_bInitComplete)
			{
				this.Init();
			}
			this.m_ssActive = ssActive;
			this.dOnFilterSetChange = onFilterSetChange;
			this.m_cNKCPopupFilterSubUIOperator.OpenFilterPopup(this.m_ssActive, setFilterCategory, new NKCPopupFilterSubUIOperator.OnFilterOptionChange(this.OnSelectFilterOption));
			base.UIOpened(true);
		}

		// Token: 0x060073E1 RID: 29665 RVA: 0x002687A8 File Offset: 0x002669A8
		public void OnSelectFilterOption(NKCOperatorSortSystem ssActive)
		{
			if (this.m_ssActive.FilterSet == null)
			{
				this.m_ssActive.FilterSet = new HashSet<NKCOperatorSortSystem.eFilterOption>();
			}
			this.m_ssActive.FilterSet = ssActive.FilterSet;
			NKCPopupFilterOperator.OnFilterSetChange onFilterSetChange = this.dOnFilterSetChange;
			if (onFilterSetChange == null)
			{
				return;
			}
			onFilterSetChange(this.m_ssActive);
		}

		// Token: 0x060073E2 RID: 29666 RVA: 0x002687F9 File Offset: 0x002669F9
		public void OnClickOk()
		{
			base.Close();
		}

		// Token: 0x060073E3 RID: 29667 RVA: 0x00268801 File Offset: 0x00266A01
		public void OnClickReset()
		{
			this.m_cNKCPopupFilterSubUIOperator.ResetFilterSlot();
			this.m_ssActive.FilterSet = new HashSet<NKCOperatorSortSystem.eFilterOption>();
			this.m_ssActive.m_PassiveSkillID = 0;
			NKCPopupFilterOperator.OnFilterSetChange onFilterSetChange = this.dOnFilterSetChange;
			if (onFilterSetChange == null)
			{
				return;
			}
			onFilterSetChange(this.m_ssActive);
		}

		// Token: 0x060073E4 RID: 29668 RVA: 0x00268840 File Offset: 0x00266A40
		public static HashSet<NKCOperatorSortSystem.eFilterCategory> MakeDefaultFilterCategory(NKCPopupFilterOperator.FILTER_OPEN_TYPE filterOpenType)
		{
			HashSet<NKCOperatorSortSystem.eFilterCategory> hashSet = new HashSet<NKCOperatorSortSystem.eFilterCategory>();
			hashSet.Add(NKCOperatorSortSystem.eFilterCategory.Rarity);
			switch (filterOpenType)
			{
			case NKCPopupFilterOperator.FILTER_OPEN_TYPE.NORMAL:
				hashSet.Add(NKCOperatorSortSystem.eFilterCategory.Level);
				hashSet.Add(NKCOperatorSortSystem.eFilterCategory.Decked);
				hashSet.Add(NKCOperatorSortSystem.eFilterCategory.Locked);
				hashSet.Add(NKCOperatorSortSystem.eFilterCategory.PassiveSkill);
				break;
			case NKCPopupFilterOperator.FILTER_OPEN_TYPE.COLLECTION:
				hashSet.Add(NKCOperatorSortSystem.eFilterCategory.Collected);
				break;
			case NKCPopupFilterOperator.FILTER_OPEN_TYPE.SELECTION:
				hashSet.Add(NKCOperatorSortSystem.eFilterCategory.Have);
				break;
			}
			return hashSet;
		}

		// Token: 0x04005FCE RID: 24526
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_selection";

		// Token: 0x04005FCF RID: 24527
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_FILTER_OPERATOR";

		// Token: 0x04005FD0 RID: 24528
		private static NKCPopupFilterOperator m_Instance;

		// Token: 0x04005FD1 RID: 24529
		public NKCUIComStateButton m_btnBackground;

		// Token: 0x04005FD2 RID: 24530
		public NKCUIComStateButton m_btnOk;

		// Token: 0x04005FD3 RID: 24531
		public NKCUIComStateButton m_btnReset;

		// Token: 0x04005FD4 RID: 24532
		public NKCPopupFilterSubUIOperator m_cNKCPopupFilterSubUIOperator;

		// Token: 0x04005FD5 RID: 24533
		private NKCPopupFilterOperator.OnFilterSetChange dOnFilterSetChange;

		// Token: 0x04005FD6 RID: 24534
		private NKCOperatorSortSystem m_ssActive;

		// Token: 0x04005FD7 RID: 24535
		private bool m_bInitComplete;

		// Token: 0x0200179D RID: 6045
		public enum FILTER_OPEN_TYPE
		{
			// Token: 0x0400A72D RID: 42797
			NONE,
			// Token: 0x0400A72E RID: 42798
			NORMAL,
			// Token: 0x0400A72F RID: 42799
			COLLECTION,
			// Token: 0x0400A730 RID: 42800
			SELECTION,
			// Token: 0x0400A731 RID: 42801
			ALLUNIT_DEV
		}

		// Token: 0x0200179E RID: 6046
		// (Invoke) Token: 0x0600B3CB RID: 46027
		public delegate void OnFilterSetChange(NKCOperatorSortSystem ssActive);
	}
}
