using System;
using System.Collections.Generic;
using NKM.Templet;
using UnityEngine.Events;

namespace NKC.UI
{
	// Token: 0x02000A59 RID: 2649
	public class NKCPopupFilterUnit : NKCUIBase
	{
		// Token: 0x1700135E RID: 4958
		// (get) Token: 0x06007436 RID: 29750 RVA: 0x0026A760 File Offset: 0x00268960
		public static NKCPopupFilterUnit Instance
		{
			get
			{
				if (NKCPopupFilterUnit.m_Instance == null)
				{
					NKCPopupFilterUnit.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupFilterUnit>("ab_ui_nkm_ui_popup_selection", "NKM_UI_POPUP_FILTER_UNIT", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupFilterUnit.CleanupInstance)).GetInstance<NKCPopupFilterUnit>();
					NKCPopupFilterUnit.m_Instance.Init();
				}
				return NKCPopupFilterUnit.m_Instance;
			}
		}

		// Token: 0x06007437 RID: 29751 RVA: 0x0026A7AF File Offset: 0x002689AF
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupFilterUnit.m_Instance != null && NKCPopupFilterUnit.m_Instance.IsOpen)
			{
				NKCPopupFilterUnit.m_Instance.Close();
			}
		}

		// Token: 0x06007438 RID: 29752 RVA: 0x0026A7D4 File Offset: 0x002689D4
		private static void CleanupInstance()
		{
			NKCPopupFilterUnit.m_Instance = null;
		}

		// Token: 0x1700135F RID: 4959
		// (get) Token: 0x06007439 RID: 29753 RVA: 0x0026A7DC File Offset: 0x002689DC
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001360 RID: 4960
		// (get) Token: 0x0600743A RID: 29754 RVA: 0x0026A7DF File Offset: 0x002689DF
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Invalid;
			}
		}

		// Token: 0x17001361 RID: 4961
		// (get) Token: 0x0600743B RID: 29755 RVA: 0x0026A7E2 File Offset: 0x002689E2
		public override string MenuName
		{
			get
			{
				return "필 터";
			}
		}

		// Token: 0x0600743C RID: 29756 RVA: 0x0026A7E9 File Offset: 0x002689E9
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x0600743D RID: 29757 RVA: 0x0026A7F8 File Offset: 0x002689F8
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

		// Token: 0x0600743E RID: 29758 RVA: 0x0026A8C8 File Offset: 0x00268AC8
		public void Open(HashSet<NKCUnitSortSystem.eFilterOption> setFilterOption, NKCPopupFilterUnit.OnFilterSetChange onFilterSetChange, NKCPopupFilterUnit.FILTER_TYPE targetType, NKCPopupFilterUnit.FILTER_OPEN_TYPE filterOpenType)
		{
			switch (targetType)
			{
			case NKCPopupFilterUnit.FILTER_TYPE.UNIT:
				this.Open(NKCPopupFilterUnit.MakeDefaultFilterOption(NKM_UNIT_TYPE.NUT_NORMAL, filterOpenType), setFilterOption, onFilterSetChange, targetType);
				return;
			case NKCPopupFilterUnit.FILTER_TYPE.OPERATOR:
				this.Open(NKCPopupFilterUnit.MakeDefaultFilterOption(NKM_UNIT_TYPE.NUT_OPERATOR, filterOpenType), setFilterOption, onFilterSetChange, targetType);
				return;
			case NKCPopupFilterUnit.FILTER_TYPE.SHIP:
				this.Open(NKCPopupFilterUnit.MakeDefaultFilterOption(NKM_UNIT_TYPE.NUT_SHIP, filterOpenType), setFilterOption, onFilterSetChange, targetType);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600743F RID: 29759 RVA: 0x0026A920 File Offset: 0x00268B20
		public void Open(HashSet<NKCUnitSortSystem.eFilterCategory> setFilterCategory, HashSet<NKCUnitSortSystem.eFilterOption> setCurrentFilterOption, NKCPopupFilterUnit.OnFilterSetChange onFilterSetChange, NKCPopupFilterUnit.FILTER_TYPE targetType)
		{
			if (!this.m_bInitComplete)
			{
				this.Init();
			}
			this.dOnFilterSetChange = onFilterSetChange;
			if (setCurrentFilterOption != null)
			{
				this.m_setFilterOption = setCurrentFilterOption;
			}
			else
			{
				this.m_setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
			}
			this.m_cNKCPopupFilterSubUIUnit.OpenFilterPopup(this.m_setFilterOption, setFilterCategory, new NKCPopupFilterSubUIUnit.OnFilterOptionChange(this.OnSelectFilterOption));
			base.UIOpened(true);
		}

		// Token: 0x06007440 RID: 29760 RVA: 0x0026A980 File Offset: 0x00268B80
		public void OnSelectFilterOption(NKCUnitSortSystem.eFilterOption selectOption)
		{
			if (this.m_setFilterOption == null)
			{
				this.m_setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
			}
			if (this.m_setFilterOption.Contains(selectOption))
			{
				this.m_setFilterOption.Remove(selectOption);
			}
			else
			{
				this.m_setFilterOption.Add(selectOption);
			}
			NKCPopupFilterUnit.OnFilterSetChange onFilterSetChange = this.dOnFilterSetChange;
			if (onFilterSetChange == null)
			{
				return;
			}
			onFilterSetChange(this.m_setFilterOption);
		}

		// Token: 0x06007441 RID: 29761 RVA: 0x0026A9E0 File Offset: 0x00268BE0
		public void OnClickOk()
		{
			base.Close();
		}

		// Token: 0x06007442 RID: 29762 RVA: 0x0026A9E8 File Offset: 0x00268BE8
		public void OnClickReset()
		{
			this.m_cNKCPopupFilterSubUIUnit.ResetFilter();
			bool flag = this.m_setFilterOption.Contains(NKCUnitSortSystem.eFilterOption.Collection_HasAchieve);
			this.m_setFilterOption.Clear();
			if (flag)
			{
				this.m_setFilterOption.Add(NKCUnitSortSystem.eFilterOption.Collection_HasAchieve);
			}
			NKCPopupFilterUnit.OnFilterSetChange onFilterSetChange = this.dOnFilterSetChange;
			if (onFilterSetChange == null)
			{
				return;
			}
			onFilterSetChange(this.m_setFilterOption);
		}

		// Token: 0x06007443 RID: 29763 RVA: 0x0026AA3E File Offset: 0x00268C3E
		public static HashSet<NKCUnitSortSystem.eFilterCategory> MakeGlobalBanSortOption()
		{
			return new HashSet<NKCUnitSortSystem.eFilterCategory>
			{
				NKCUnitSortSystem.eFilterCategory.UnitType,
				NKCUnitSortSystem.eFilterCategory.UnitRole,
				NKCUnitSortSystem.eFilterCategory.UnitTargetType,
				NKCUnitSortSystem.eFilterCategory.Cost,
				NKCUnitSortSystem.eFilterCategory.Rarity
			};
		}

		// Token: 0x06007444 RID: 29764 RVA: 0x0026AA70 File Offset: 0x00268C70
		public static HashSet<NKCUnitSortSystem.eFilterCategory> MakeDefaultFilterOption(NKM_UNIT_TYPE unitType, NKCPopupFilterUnit.FILTER_OPEN_TYPE filterOpenType)
		{
			HashSet<NKCUnitSortSystem.eFilterCategory> hashSet = new HashSet<NKCUnitSortSystem.eFilterCategory>();
			if (filterOpenType == NKCPopupFilterUnit.FILTER_OPEN_TYPE.ALLUNIT_DEV)
			{
				hashSet.Add(NKCUnitSortSystem.eFilterCategory.MonsterType);
				filterOpenType = NKCPopupFilterUnit.FILTER_OPEN_TYPE.NORMAL;
			}
			switch (unitType)
			{
			case NKM_UNIT_TYPE.NUT_NORMAL:
				hashSet.Add(NKCUnitSortSystem.eFilterCategory.UnitType);
				hashSet.Add(NKCUnitSortSystem.eFilterCategory.UnitRole);
				hashSet.Add(NKCUnitSortSystem.eFilterCategory.UnitMoveType);
				hashSet.Add(NKCUnitSortSystem.eFilterCategory.UnitTargetType);
				hashSet.Add(NKCUnitSortSystem.eFilterCategory.Cost);
				hashSet.Add(NKCUnitSortSystem.eFilterCategory.Rarity);
				hashSet.Add(NKCUnitSortSystem.eFilterCategory.SpecialType);
				break;
			case NKM_UNIT_TYPE.NUT_SHIP:
				hashSet.Add(NKCUnitSortSystem.eFilterCategory.ShipType);
				hashSet.Add(NKCUnitSortSystem.eFilterCategory.Rarity);
				break;
			case NKM_UNIT_TYPE.NUT_OPERATOR:
				hashSet.Add(NKCUnitSortSystem.eFilterCategory.Rarity);
				break;
			}
			switch (filterOpenType)
			{
			case NKCPopupFilterUnit.FILTER_OPEN_TYPE.NORMAL:
				hashSet.Add(NKCUnitSortSystem.eFilterCategory.Level);
				hashSet.Add(NKCUnitSortSystem.eFilterCategory.Decked);
				hashSet.Add(NKCUnitSortSystem.eFilterCategory.Locked);
				break;
			case NKCPopupFilterUnit.FILTER_OPEN_TYPE.SELECTION:
				hashSet.Add(NKCUnitSortSystem.eFilterCategory.Have);
				break;
			}
			return hashSet;
		}

		// Token: 0x0400609B RID: 24731
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_selection";

		// Token: 0x0400609C RID: 24732
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_FILTER_UNIT";

		// Token: 0x0400609D RID: 24733
		private static NKCPopupFilterUnit m_Instance;

		// Token: 0x0400609E RID: 24734
		public NKCUIComStateButton m_btnBackground;

		// Token: 0x0400609F RID: 24735
		public NKCUIComStateButton m_btnOk;

		// Token: 0x040060A0 RID: 24736
		public NKCUIComStateButton m_btnReset;

		// Token: 0x040060A1 RID: 24737
		public NKCPopupFilterSubUIUnit m_cNKCPopupFilterSubUIUnit;

		// Token: 0x040060A2 RID: 24738
		private NKCPopupFilterUnit.OnFilterSetChange dOnFilterSetChange;

		// Token: 0x040060A3 RID: 24739
		private HashSet<NKCUnitSortSystem.eFilterOption> m_setFilterOption;

		// Token: 0x040060A4 RID: 24740
		private bool m_bInitComplete;

		// Token: 0x020017AE RID: 6062
		public enum FILTER_OPEN_TYPE
		{
			// Token: 0x0400A743 RID: 42819
			NONE,
			// Token: 0x0400A744 RID: 42820
			NORMAL,
			// Token: 0x0400A745 RID: 42821
			COLLECTION,
			// Token: 0x0400A746 RID: 42822
			SELECTION,
			// Token: 0x0400A747 RID: 42823
			ALLUNIT_DEV
		}

		// Token: 0x020017AF RID: 6063
		// (Invoke) Token: 0x0600B3F9 RID: 46073
		public delegate void OnFilterSetChange(HashSet<NKCUnitSortSystem.eFilterOption> setFilterOption);

		// Token: 0x020017B0 RID: 6064
		public enum FILTER_TYPE
		{
			// Token: 0x0400A749 RID: 42825
			NONE,
			// Token: 0x0400A74A RID: 42826
			UNIT,
			// Token: 0x0400A74B RID: 42827
			OPERATOR,
			// Token: 0x0400A74C RID: 42828
			SHIP
		}
	}
}
