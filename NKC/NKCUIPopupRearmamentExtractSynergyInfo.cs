using System;
using System.Collections.Generic;
using System.Linq;
using NKM;
using NKM.Contract2;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A99 RID: 2713
	public class NKCUIPopupRearmamentExtractSynergyInfo : NKCUIBase
	{
		// Token: 0x1700142E RID: 5166
		// (get) Token: 0x0600783E RID: 30782 RVA: 0x0027EAD4 File Offset: 0x0027CCD4
		public static NKCUIPopupRearmamentExtractSynergyInfo Instance
		{
			get
			{
				if (NKCUIPopupRearmamentExtractSynergyInfo.m_Instance == null)
				{
					NKCUIPopupRearmamentExtractSynergyInfo.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPopupRearmamentExtractSynergyInfo>("ab_ui_rearm", "AB_UI_POPUP_REARM_RECORD_SYNERGY", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPopupRearmamentExtractSynergyInfo.CleanupInstance)).GetInstance<NKCUIPopupRearmamentExtractSynergyInfo>();
					NKCUIPopupRearmamentExtractSynergyInfo.m_Instance.InitUI();
				}
				return NKCUIPopupRearmamentExtractSynergyInfo.m_Instance;
			}
		}

		// Token: 0x0600783F RID: 30783 RVA: 0x0027EB23 File Offset: 0x0027CD23
		private static void CleanupInstance()
		{
			NKCUIPopupRearmamentExtractSynergyInfo.m_Instance = null;
		}

		// Token: 0x1700142F RID: 5167
		// (get) Token: 0x06007840 RID: 30784 RVA: 0x0027EB2B File Offset: 0x0027CD2B
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIPopupRearmamentExtractSynergyInfo.m_Instance != null && NKCUIPopupRearmamentExtractSynergyInfo.m_Instance.IsOpen;
			}
		}

		// Token: 0x06007841 RID: 30785 RVA: 0x0027EB46 File Offset: 0x0027CD46
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPopupRearmamentExtractSynergyInfo.m_Instance != null && NKCUIPopupRearmamentExtractSynergyInfo.m_Instance.IsOpen)
			{
				NKCUIPopupRearmamentExtractSynergyInfo.m_Instance.Close();
			}
		}

		// Token: 0x06007842 RID: 30786 RVA: 0x0027EB6B File Offset: 0x0027CD6B
		private void OnDestroy()
		{
			NKCUIPopupRearmamentExtractSynergyInfo.m_Instance = null;
		}

		// Token: 0x06007843 RID: 30787 RVA: 0x0027EB73 File Offset: 0x0027CD73
		public override void CloseInternal()
		{
			this.Clear();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x17001430 RID: 5168
		// (get) Token: 0x06007844 RID: 30788 RVA: 0x0027EB87 File Offset: 0x0027CD87
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001431 RID: 5169
		// (get) Token: 0x06007845 RID: 30789 RVA: 0x0027EB8A File Offset: 0x0027CD8A
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_REARM_CONFIRM_POPUP_TITLE;
			}
		}

		// Token: 0x17001432 RID: 5170
		// (get) Token: 0x06007846 RID: 30790 RVA: 0x0027EB91 File Offset: 0x0027CD91
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x06007847 RID: 30791 RVA: 0x0027EB94 File Offset: 0x0027CD94
		private void InitUI()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnOK, new UnityAction(base.Close));
			NKCUtil.SetHotkey(this.m_csbtnOK, HotkeyEventType.Confirm, null, false);
			if (this.m_evtPanel != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(new UnityAction<BaseEventData>(this.OnEventPanelClick));
				this.m_evtPanel.triggers.Add(entry);
			}
			if (this.m_LoopScroll != null)
			{
				this.m_LoopScroll.dOnGetObject += this.GetSlot;
				this.m_LoopScroll.dOnReturnObject += this.ReturnSlot;
				this.m_LoopScroll.dOnProvideData += this.ProvideSlot;
				this.m_LoopScroll.ContentConstraintCount = 1;
				this.m_LoopScroll.PrepareCells(0);
				NKCUtil.SetScrollHotKey(this.m_LoopScroll, null);
			}
		}

		// Token: 0x06007848 RID: 30792 RVA: 0x0027EC80 File Offset: 0x0027CE80
		private void Clear()
		{
			for (int i = 0; i < this.m_lstSynergySlots.Count; i++)
			{
				UnityEngine.Object.Destroy(this.m_lstSynergySlots[i].gameObject);
			}
			this.m_lstSynergySlots.Clear();
		}

		// Token: 0x06007849 RID: 30793 RVA: 0x0027ECC4 File Offset: 0x0027CEC4
		private void OnEventPanelClick(BaseEventData e)
		{
			base.Close();
		}

		// Token: 0x0600784A RID: 30794 RVA: 0x0027ECCC File Offset: 0x0027CECC
		public void Open()
		{
			NKCUtil.SetLabelText(this.m_lbAwakePercent, string.Format("{0}%", NKMCommonConst.ExtractBonusRatePercent_Awaken));
			NKCUtil.SetLabelText(this.m_lbSSRPercent, string.Format("{0}%", NKMCommonConst.ExtractBonusRatePercent_SSR));
			NKCUtil.SetLabelText(this.m_lbSRPercent, string.Format("{0}%", NKMCommonConst.ExtractBonusRatePercent_SR));
			this.m_lstSynergyItems = NKMUnitExtractBonuseTemplet.Instance.Datas.ToList<Tuple<int, MiscItemUnit>>();
			this.m_LoopScroll.TotalCount = this.m_lstSynergyItems.Count;
			this.m_LoopScroll.PrepareCells(0);
			this.m_LoopScroll.RefreshCells(true);
			base.UIOpened(true);
		}

		// Token: 0x0600784B RID: 30795 RVA: 0x0027ED80 File Offset: 0x0027CF80
		private RectTransform GetSlot(int index)
		{
			NKCUIRearmamentExtractSynergySlot nkcuirearmamentExtractSynergySlot = UnityEngine.Object.Instantiate<NKCUIRearmamentExtractSynergySlot>(this.m_pfbSynergySlot);
			nkcuirearmamentExtractSynergySlot.transform.localScale = Vector3.one;
			this.m_lstSynergySlots.Add(nkcuirearmamentExtractSynergySlot);
			return nkcuirearmamentExtractSynergySlot.GetComponent<RectTransform>();
		}

		// Token: 0x0600784C RID: 30796 RVA: 0x0027EDBB File Offset: 0x0027CFBB
		private void ReturnSlot(Transform go)
		{
			go.GetComponent<NKCUIRearmamentExtractSynergySlot>();
			NKCUtil.SetGameobjectActive(go, false);
		}

		// Token: 0x0600784D RID: 30797 RVA: 0x0027EDCC File Offset: 0x0027CFCC
		private void ProvideSlot(Transform tr, int idx)
		{
			NKCUIRearmamentExtractSynergySlot component = tr.GetComponent<NKCUIRearmamentExtractSynergySlot>();
			if (component != null)
			{
				if (this.m_lstSynergyItems.Count <= idx || idx < 0)
				{
					Debug.LogError(string.Format("m_lstUnitSlot - 잘못된 인덱스 입니다, {0}", idx));
					return;
				}
				tr.SetParent(this.m_LoopScroll.content);
				MiscItemUnit item = this.m_lstSynergyItems[idx].Item2;
				component.SetData(item.ItemId, (int)item.Count, this.m_lstSynergyItems[idx].Item1);
				NKCUtil.SetGameobjectActive(tr.gameObject, true);
			}
		}

		// Token: 0x040064C3 RID: 25795
		private const string ASSET_BUNDLE_NAME = "ab_ui_rearm";

		// Token: 0x040064C4 RID: 25796
		private const string UI_ASSET_NAME = "AB_UI_POPUP_REARM_RECORD_SYNERGY";

		// Token: 0x040064C5 RID: 25797
		private static NKCUIPopupRearmamentExtractSynergyInfo m_Instance;

		// Token: 0x040064C6 RID: 25798
		[Header("시너지 증가 확률")]
		public Text m_lbAwakePercent;

		// Token: 0x040064C7 RID: 25799
		public Text m_lbSSRPercent;

		// Token: 0x040064C8 RID: 25800
		public Text m_lbSRPercent;

		// Token: 0x040064C9 RID: 25801
		[Header("스크롤")]
		public LoopVerticalScrollRect m_LoopScroll;

		// Token: 0x040064CA RID: 25802
		[Header("슬롯")]
		public NKCUIRearmamentExtractSynergySlot m_pfbSynergySlot;

		// Token: 0x040064CB RID: 25803
		[Header("버튼")]
		public NKCUIComStateButton m_csbtnOK;

		// Token: 0x040064CC RID: 25804
		[Header("BG")]
		public EventTrigger m_evtPanel;

		// Token: 0x040064CD RID: 25805
		private List<Tuple<int, MiscItemUnit>> m_lstSynergyItems;

		// Token: 0x040064CE RID: 25806
		private List<NKCUIRearmamentExtractSynergySlot> m_lstSynergySlots = new List<NKCUIRearmamentExtractSynergySlot>();
	}
}
