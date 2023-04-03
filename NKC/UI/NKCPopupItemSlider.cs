using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A6A RID: 2666
	public class NKCPopupItemSlider : NKCUIBase, IScrollHandler, IEventSystemHandler
	{
		// Token: 0x17001393 RID: 5011
		// (get) Token: 0x0600758E RID: 30094 RVA: 0x00271DF8 File Offset: 0x0026FFF8
		public static NKCPopupItemSlider Instance
		{
			get
			{
				if (NKCPopupItemSlider.m_Instance == null)
				{
					NKCPopupItemSlider.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupItemSlider>("ab_ui_nkm_ui_popup_ok_cancel_box", "NKM_UI_POPUP_ITEM_SLIDER", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupItemSlider.CleanupInstance)).GetInstance<NKCPopupItemSlider>();
					NKCPopupItemSlider.m_Instance.Init();
				}
				return NKCPopupItemSlider.m_Instance;
			}
		}

		// Token: 0x0600758F RID: 30095 RVA: 0x00271E47 File Offset: 0x00270047
		private static void CleanupInstance()
		{
			NKCPopupItemSlider.m_Instance = null;
		}

		// Token: 0x17001394 RID: 5012
		// (get) Token: 0x06007590 RID: 30096 RVA: 0x00271E4F File Offset: 0x0027004F
		public static bool HasInstance
		{
			get
			{
				return NKCPopupItemSlider.m_Instance != null;
			}
		}

		// Token: 0x17001395 RID: 5013
		// (get) Token: 0x06007591 RID: 30097 RVA: 0x00271E5C File Offset: 0x0027005C
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupItemSlider.m_Instance != null && NKCPopupItemSlider.m_Instance.IsOpen;
			}
		}

		// Token: 0x06007592 RID: 30098 RVA: 0x00271E77 File Offset: 0x00270077
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupItemSlider.m_Instance != null && NKCPopupItemSlider.m_Instance.IsOpen)
			{
				NKCPopupItemSlider.m_Instance.Close();
			}
		}

		// Token: 0x17001396 RID: 5014
		// (get) Token: 0x06007593 RID: 30099 RVA: 0x00271E9C File Offset: 0x0027009C
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001397 RID: 5015
		// (get) Token: 0x06007594 RID: 30100 RVA: 0x00271E9F File Offset: 0x0027009F
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x06007595 RID: 30101 RVA: 0x00271EA6 File Offset: 0x002700A6
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007596 RID: 30102 RVA: 0x00271EB4 File Offset: 0x002700B4
		public void Init()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnPlus, new UnityAction(this.OnPlus));
			NKCUtil.SetHotkey(this.m_csbtnPlus, HotkeyEventType.Plus, null, false);
			NKCUtil.SetButtonClickDelegate(this.m_csbtnMinus, new UnityAction(this.OnMinus));
			NKCUtil.SetHotkey(this.m_csbtnMinus, HotkeyEventType.Minus, null, false);
			NKCUtil.SetButtonClickDelegate(this.m_csbtnConfirm, new UnityAction(this.OnOK));
			NKCUtil.SetHotkey(this.m_csbtnConfirm, HotkeyEventType.Confirm, null, false);
			NKCUtil.SetButtonClickDelegate(this.m_csbtnCancel, new UnityAction(base.Close));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnClose, new UnityAction(base.Close));
			NKCUtil.SetSliderValueChangedDelegate(this.m_Slider, new UnityAction<float>(this.OnSliderChange));
		}

		// Token: 0x06007597 RID: 30103 RVA: 0x00271F78 File Offset: 0x00270178
		public void Open(string title, string desc, NKCUISlot.SlotData itemSlotData, int minValue, int maxValue, int destMax, bool bShowHaveCount, NKCPopupItemSlider.OnConfirm onConfirm, int currentValue = 1)
		{
			NKCUtil.SetLabelText(this.m_lbTitle, title);
			NKCUtil.SetLabelText(this.m_lbDescription, desc);
			this.m_minValue = minValue;
			this.m_maxValue = maxValue;
			this.m_destMax = destMax;
			this.m_bShowCount = (bShowHaveCount && itemSlotData.eType == NKCUISlot.eSlotMode.ItemMisc);
			this.dOnConfirm = onConfirm;
			NKCUtil.SetGameobjectActive(this.m_lbHaveCount, bShowHaveCount);
			if (bShowHaveCount)
			{
				long countMiscItem = NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(itemSlotData.ID);
				NKCUtil.SetLabelText(this.m_lbHaveCount, NKCUtilString.GET_STRING_TOOLTIP_QUANTITY_ONE_PARAM, new object[]
				{
					countMiscItem
				});
			}
			this.m_originalSlotData = itemSlotData;
			this.m_currentValue = Mathf.Clamp(currentValue, minValue, maxValue);
			if (this.m_Slider != null)
			{
				this.m_Slider.wholeNumbers = true;
				this.m_Slider.maxValue = (float)maxValue;
				this.m_Slider.minValue = (float)minValue;
			}
			NKCUtil.SetGameobjectActive(this.m_objGauge, maxValue > 1);
			this.UpdateValue(this.m_currentValue);
			base.UIOpened(true);
		}

		// Token: 0x06007598 RID: 30104 RVA: 0x0027208A File Offset: 0x0027028A
		private void OnOK()
		{
			base.Close();
			NKCPopupItemSlider.OnConfirm onConfirm = this.dOnConfirm;
			if (onConfirm == null)
			{
				return;
			}
			onConfirm(this.m_currentValue);
		}

		// Token: 0x06007599 RID: 30105 RVA: 0x002720A8 File Offset: 0x002702A8
		private void OnSliderChange(float value)
		{
			this.UpdateValue((int)value);
		}

		// Token: 0x0600759A RID: 30106 RVA: 0x002720B2 File Offset: 0x002702B2
		private void OnPlus()
		{
			this.UpdateValue(this.m_currentValue + 1);
		}

		// Token: 0x0600759B RID: 30107 RVA: 0x002720C2 File Offset: 0x002702C2
		private void OnMinus()
		{
			this.UpdateValue(this.m_currentValue - 1);
		}

		// Token: 0x0600759C RID: 30108 RVA: 0x002720D4 File Offset: 0x002702D4
		private void UpdateValue(int newValue)
		{
			this.m_currentValue = Mathf.Clamp(newValue, this.m_minValue, this.m_maxValue);
			if (this.m_Slot != null)
			{
				NKCUISlot.SlotData slotData = new NKCUISlot.SlotData(this.m_originalSlotData);
				slotData.Count = this.m_originalSlotData.Count * (long)this.m_currentValue;
				this.m_Slot.SetMiscItemData(slotData, false, this.m_bShowCount, false, null);
				this.m_Slot.SetOnClickAction(new NKCUISlot.SlotClickType[1]);
			}
			NKCUtil.SetLabelText(this.m_lbCurrentCount, string.Format("{0}/{1}", this.m_currentValue, this.m_destMax));
			if (this.m_Slider != null)
			{
				this.m_Slider.value = (float)this.m_currentValue;
			}
		}

		// Token: 0x0600759D RID: 30109 RVA: 0x002721A0 File Offset: 0x002703A0
		public void OnScroll(PointerEventData eventData)
		{
			if (eventData.scrollDelta.y < 0f)
			{
				this.UpdateValue(this.m_currentValue - 1);
				return;
			}
			if (eventData.scrollDelta.y > 0f)
			{
				this.UpdateValue(this.m_currentValue + 1);
			}
		}

		// Token: 0x040061F1 RID: 25073
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_ok_cancel_box";

		// Token: 0x040061F2 RID: 25074
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_ITEM_SLIDER";

		// Token: 0x040061F3 RID: 25075
		private static NKCPopupItemSlider m_Instance;

		// Token: 0x040061F4 RID: 25076
		public Text m_lbTitle;

		// Token: 0x040061F5 RID: 25077
		public NKCUISlot m_Slot;

		// Token: 0x040061F6 RID: 25078
		public Text m_lbDescription;

		// Token: 0x040061F7 RID: 25079
		public Text m_lbHaveCount;

		// Token: 0x040061F8 RID: 25080
		public Text m_lbCurrentCount;

		// Token: 0x040061F9 RID: 25081
		public Slider m_Slider;

		// Token: 0x040061FA RID: 25082
		public GameObject m_objGauge;

		// Token: 0x040061FB RID: 25083
		public NKCUIComStateButton m_csbtnPlus;

		// Token: 0x040061FC RID: 25084
		public NKCUIComStateButton m_csbtnMinus;

		// Token: 0x040061FD RID: 25085
		public NKCUIComStateButton m_csbtnConfirm;

		// Token: 0x040061FE RID: 25086
		public NKCUIComStateButton m_csbtnCancel;

		// Token: 0x040061FF RID: 25087
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x04006200 RID: 25088
		private int m_minValue;

		// Token: 0x04006201 RID: 25089
		private int m_maxValue = 1;

		// Token: 0x04006202 RID: 25090
		private int m_destMax;

		// Token: 0x04006203 RID: 25091
		private int m_currentValue;

		// Token: 0x04006204 RID: 25092
		private bool m_bShowCount;

		// Token: 0x04006205 RID: 25093
		private NKCPopupItemSlider.OnConfirm dOnConfirm;

		// Token: 0x04006206 RID: 25094
		private NKCUISlot.SlotData m_originalSlotData;

		// Token: 0x020017CC RID: 6092
		// (Invoke) Token: 0x0600B43C RID: 46140
		public delegate void OnConfirm(int count);
	}
}
