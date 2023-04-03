using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A61 RID: 2657
	public class NKCPopupHaveInfo : NKCUIBase
	{
		// Token: 0x17001376 RID: 4982
		// (get) Token: 0x060074D8 RID: 29912 RVA: 0x0026D754 File Offset: 0x0026B954
		public static NKCPopupHaveInfo Instance
		{
			get
			{
				if (NKCPopupHaveInfo.m_Instance == null)
				{
					NKCPopupHaveInfo.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupHaveInfo>("ab_ui_nkm_ui_popup_ok_cancel_box", "NKM_UI_POPUP_UNIT_HAVE", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupHaveInfo.CleanupInstance)).GetInstance<NKCPopupHaveInfo>();
					NKCPopupHaveInfo.m_Instance.InitUI();
				}
				return NKCPopupHaveInfo.m_Instance;
			}
		}

		// Token: 0x060074D9 RID: 29913 RVA: 0x0026D7A3 File Offset: 0x0026B9A3
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupHaveInfo.m_Instance != null && NKCPopupHaveInfo.m_Instance.IsOpen)
			{
				NKCPopupHaveInfo.m_Instance.Close();
			}
		}

		// Token: 0x060074DA RID: 29914 RVA: 0x0026D7C8 File Offset: 0x0026B9C8
		private static void CleanupInstance()
		{
			NKCPopupHaveInfo.m_Instance = null;
		}

		// Token: 0x17001377 RID: 4983
		// (get) Token: 0x060074DB RID: 29915 RVA: 0x0026D7D0 File Offset: 0x0026B9D0
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001378 RID: 4984
		// (get) Token: 0x060074DC RID: 29916 RVA: 0x0026D7D3 File Offset: 0x0026B9D3
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x060074DD RID: 29917 RVA: 0x0026D7DA File Offset: 0x0026B9DA
		public override void CloseInternal()
		{
			this.m_lstUnitData = null;
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060074DE RID: 29918 RVA: 0x0026D7F0 File Offset: 0x0026B9F0
		public void InitUI()
		{
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_btnOk.PointerClick.RemoveAllListeners();
			this.m_btnOk.PointerClick.AddListener(new UnityAction(base.Close));
			NKCUtil.SetHotkey(this.m_btnOk, HotkeyEventType.Confirm, null, false);
			this.m_loopScrollRect.dOnGetObject += this.GetObject;
			this.m_loopScrollRect.dOnReturnObject += this.ReturnObject;
			this.m_loopScrollRect.dOnProvideData += this.ProvideData;
			NKCUtil.SetScrollHotKey(this.m_loopScrollRect, null);
		}

		// Token: 0x060074DF RID: 29919 RVA: 0x0026D8B4 File Offset: 0x0026BAB4
		public void Open(int unitID)
		{
			this.m_lstUnitData = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitListByUnitID(unitID);
			if (this.m_lstUnitData.Count == 0)
			{
				base.Close();
				Debug.LogWarning("보유중인 유닛이 없을 경우 여기 들어오면 안됨");
				return;
			}
			this.m_lstUnitData.Sort(new Comparison<NKMUnitData>(this.CompBreakLevel));
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_loopScrollRect.TotalCount = this.m_lstUnitData.Count;
			if (this.m_loopScrollRect.content.childCount == 0)
			{
				this.m_loopScrollRect.PrepareCells(0);
			}
			this.m_loopScrollRect.RefreshCells(false);
			this.m_loopScrollRect.SetIndexPosition(0);
			base.UIOpened(true);
		}

		// Token: 0x060074E0 RID: 29920 RVA: 0x0026D96B File Offset: 0x0026BB6B
		public int CompBreakLevel(NKMUnitData lUnitData, NKMUnitData rUnitData)
		{
			if (lUnitData.m_LimitBreakLevel == rUnitData.m_LimitBreakLevel)
			{
				return rUnitData.m_UnitLevel.CompareTo(lUnitData.m_UnitLevel);
			}
			return rUnitData.m_LimitBreakLevel.CompareTo(lUnitData.m_LimitBreakLevel);
		}

		// Token: 0x060074E1 RID: 29921 RVA: 0x0026D9A0 File Offset: 0x0026BBA0
		public RectTransform GetObject(int index)
		{
			if (this.m_stkUnitSlotPool.Count > 0)
			{
				NKCDeckViewUnitSlot nkcdeckViewUnitSlot = this.m_stkUnitSlotPool.Pop();
				NKCUtil.SetGameobjectActive(nkcdeckViewUnitSlot, true);
				nkcdeckViewUnitSlot.transform.localScale = Vector3.one;
				this.m_lstVisibleSlot.Add(nkcdeckViewUnitSlot);
				return nkcdeckViewUnitSlot.GetComponent<RectTransform>();
			}
			NKCDeckViewUnitSlot nkcdeckViewUnitSlot2 = UnityEngine.Object.Instantiate<NKCDeckViewUnitSlot>(this.m_pfbUnitSlot);
			nkcdeckViewUnitSlot2.Init(0, false);
			NKCUtil.SetGameobjectActive(nkcdeckViewUnitSlot2, true);
			nkcdeckViewUnitSlot2.transform.localScale = Vector3.one;
			this.m_lstVisibleSlot.Add(nkcdeckViewUnitSlot2);
			return nkcdeckViewUnitSlot2.GetComponent<RectTransform>();
		}

		// Token: 0x060074E2 RID: 29922 RVA: 0x0026DA30 File Offset: 0x0026BC30
		public void ReturnObject(Transform tr)
		{
			NKCDeckViewUnitSlot component = tr.GetComponent<NKCDeckViewUnitSlot>();
			NKCUtil.SetGameobjectActive(component, false);
			tr.SetParent(base.transform);
			if (component != null)
			{
				this.m_lstVisibleSlot.Remove(component);
				this.m_stkUnitSlotPool.Push(component);
			}
		}

		// Token: 0x060074E3 RID: 29923 RVA: 0x0026DA7C File Offset: 0x0026BC7C
		public void ProvideData(Transform tr, int idx)
		{
			NKCDeckViewUnitSlot component = tr.GetComponent<NKCDeckViewUnitSlot>();
			if (component == null || this.m_lstUnitData.Count <= idx)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(component, true);
			component.SetData(this.m_lstUnitData[idx], false);
		}

		// Token: 0x0400612A RID: 24874
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_ok_cancel_box";

		// Token: 0x0400612B RID: 24875
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_UNIT_HAVE";

		// Token: 0x0400612C RID: 24876
		private static NKCPopupHaveInfo m_Instance;

		// Token: 0x0400612D RID: 24877
		public LoopScrollRect m_loopScrollRect;

		// Token: 0x0400612E RID: 24878
		public NKCUIComStateButton m_btnClose;

		// Token: 0x0400612F RID: 24879
		public NKCUIComStateButton m_btnOk;

		// Token: 0x04006130 RID: 24880
		public NKCDeckViewUnitSlot m_pfbUnitSlot;

		// Token: 0x04006131 RID: 24881
		private List<NKMUnitData> m_lstUnitData = new List<NKMUnitData>();

		// Token: 0x04006132 RID: 24882
		private List<NKCDeckViewUnitSlot> m_lstVisibleSlot = new List<NKCDeckViewUnitSlot>();

		// Token: 0x04006133 RID: 24883
		private Stack<NKCDeckViewUnitSlot> m_stkUnitSlotPool = new Stack<NKCDeckViewUnitSlot>();
	}
}
