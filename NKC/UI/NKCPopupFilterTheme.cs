using System;
using System.Collections.Generic;
using NKC.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A57 RID: 2647
	public class NKCPopupFilterTheme : NKCUIBase
	{
		// Token: 0x1700135B RID: 4955
		// (get) Token: 0x06007423 RID: 29731 RVA: 0x0026A38C File Offset: 0x0026858C
		public static NKCPopupFilterTheme Instance
		{
			get
			{
				if (NKCPopupFilterTheme.m_Instance == null)
				{
					NKCPopupFilterTheme.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupFilterTheme>("ab_ui_nkm_ui_popup_selection", "NKM_UI_POPUP_FILTER_FNC", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupFilterTheme.CleanupInstance)).GetInstance<NKCPopupFilterTheme>();
					NKCPopupFilterTheme.m_Instance.Init();
				}
				return NKCPopupFilterTheme.m_Instance;
			}
		}

		// Token: 0x06007424 RID: 29732 RVA: 0x0026A3DB File Offset: 0x002685DB
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupFilterTheme.m_Instance != null && NKCPopupFilterTheme.m_Instance.IsOpen)
			{
				NKCPopupFilterTheme.m_Instance.Close();
			}
		}

		// Token: 0x06007425 RID: 29733 RVA: 0x0026A400 File Offset: 0x00268600
		private static void CleanupInstance()
		{
			NKCPopupFilterTheme.m_Instance = null;
		}

		// Token: 0x1700135C RID: 4956
		// (get) Token: 0x06007426 RID: 29734 RVA: 0x0026A408 File Offset: 0x00268608
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700135D RID: 4957
		// (get) Token: 0x06007427 RID: 29735 RVA: 0x0026A40B File Offset: 0x0026860B
		public override string MenuName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06007428 RID: 29736 RVA: 0x0026A412 File Offset: 0x00268612
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007429 RID: 29737 RVA: 0x0026A420 File Offset: 0x00268620
		public void Init()
		{
			if (this.m_LoopScrollRect != null)
			{
				this.m_LoopScrollRect.dOnGetObject += this.GetObject;
				this.m_LoopScrollRect.dOnReturnObject += this.ReturnObject;
				this.m_LoopScrollRect.dOnProvideData += this.ProvideData;
				this.m_LoopScrollRect.SetAutoResize(this.MinColumnCount, false);
				this.m_LoopScrollRect.PrepareCells(0);
			}
			NKCUtil.SetButtonClickDelegate(this.m_csbtnReset, new UnityAction(this.OnBtnReset));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnOK, new UnityAction(base.Close));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnBackground, new UnityAction(base.Close));
		}

		// Token: 0x0600742A RID: 29738 RVA: 0x0026A4E4 File Offset: 0x002686E4
		public void Open(NKCPopupFilterTheme.OnSelectTheme onSelectTheme, int currentSelectedThemeID)
		{
			NKCThemeGroupTemplet.Load();
			this.m_SelectedID = currentSelectedThemeID;
			this.m_lstThemeTemplet.Clear();
			foreach (NKCThemeGroupTemplet nkcthemeGroupTemplet in NKMTempletContainer<NKCThemeGroupTemplet>.Values)
			{
				if (nkcthemeGroupTemplet.EnableByTag)
				{
					this.m_lstThemeTemplet.Add(nkcthemeGroupTemplet);
				}
			}
			base.gameObject.SetActive(true);
			this.m_LoopScrollRect.TotalCount = this.m_lstThemeTemplet.Count;
			this.m_LoopScrollRect.SetIndexPosition(0);
			this.dOnSelectTheme = onSelectTheme;
			base.UIOpened(true);
		}

		// Token: 0x0600742B RID: 29739 RVA: 0x0026A590 File Offset: 0x00268790
		private RectTransform GetObject(int idx)
		{
			NKCPopupFilterThemeSlot nkcpopupFilterThemeSlot = UnityEngine.Object.Instantiate<NKCPopupFilterThemeSlot>(this.m_pfbSlot);
			RectTransform component = nkcpopupFilterThemeSlot.GetComponent<RectTransform>();
			nkcpopupFilterThemeSlot.Init(this.m_ToggleGroup);
			return component;
		}

		// Token: 0x0600742C RID: 29740 RVA: 0x0026A5BB File Offset: 0x002687BB
		private void ReturnObject(Transform tr)
		{
			tr.SetParent(base.transform);
			tr.gameObject.SetActive(false);
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x0600742D RID: 29741 RVA: 0x0026A5E0 File Offset: 0x002687E0
		private void ProvideData(Transform tr, int idx)
		{
			NKCPopupFilterThemeSlot component = tr.GetComponent<NKCPopupFilterThemeSlot>();
			if (component == null)
			{
				return;
			}
			component.SetData(this.m_lstThemeTemplet[idx], new NKCPopupFilterThemeSlot.OnClick(this.OnSelectSlot));
			component.SetSelected(this.m_lstThemeTemplet[idx].Key == this.m_SelectedID);
		}

		// Token: 0x0600742E RID: 29742 RVA: 0x0026A63B File Offset: 0x0026883B
		private void OnSelectSlot(int themeID)
		{
			this.m_SelectedID = themeID;
			base.Close();
			NKCPopupFilterTheme.OnSelectTheme onSelectTheme = this.dOnSelectTheme;
			if (onSelectTheme == null)
			{
				return;
			}
			onSelectTheme(themeID);
		}

		// Token: 0x0600742F RID: 29743 RVA: 0x0026A65B File Offset: 0x0026885B
		private void OnBtnReset()
		{
			this.m_SelectedID = 0;
			this.m_LoopScrollRect.RefreshCells(false);
			NKCPopupFilterTheme.OnSelectTheme onSelectTheme = this.dOnSelectTheme;
			if (onSelectTheme == null)
			{
				return;
			}
			onSelectTheme(0);
		}

		// Token: 0x04006089 RID: 24713
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_selection";

		// Token: 0x0400608A RID: 24714
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_FILTER_FNC";

		// Token: 0x0400608B RID: 24715
		private static NKCPopupFilterTheme m_Instance;

		// Token: 0x0400608C RID: 24716
		public LoopScrollRect m_LoopScrollRect;

		// Token: 0x0400608D RID: 24717
		public NKCPopupFilterThemeSlot m_pfbSlot;

		// Token: 0x0400608E RID: 24718
		public int MinColumnCount = 4;

		// Token: 0x0400608F RID: 24719
		public NKCUIComToggleGroup m_ToggleGroup;

		// Token: 0x04006090 RID: 24720
		public NKCUIComStateButton m_csbtnReset;

		// Token: 0x04006091 RID: 24721
		public NKCUIComStateButton m_csbtnOK;

		// Token: 0x04006092 RID: 24722
		public NKCUIComStateButton m_csbtnBackground;

		// Token: 0x04006093 RID: 24723
		private List<NKCThemeGroupTemplet> m_lstThemeTemplet = new List<NKCThemeGroupTemplet>();

		// Token: 0x04006094 RID: 24724
		private NKCPopupFilterTheme.OnSelectTheme dOnSelectTheme;

		// Token: 0x04006095 RID: 24725
		private int m_SelectedID;

		// Token: 0x020017AC RID: 6060
		// (Invoke) Token: 0x0600B3F1 RID: 46065
		public delegate void OnSelectTheme(int themeID);
	}
}
