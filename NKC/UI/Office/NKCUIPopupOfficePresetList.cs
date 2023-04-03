using System;
using ClientPacket.Office;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Office
{
	// Token: 0x02000AFD RID: 2813
	public class NKCUIPopupOfficePresetList : NKCUIBase
	{
		// Token: 0x170014F9 RID: 5369
		// (get) Token: 0x06007F36 RID: 32566 RVA: 0x002AABAC File Offset: 0x002A8DAC
		public static NKCUIPopupOfficePresetList Instance
		{
			get
			{
				if (NKCUIPopupOfficePresetList.m_Instance == null)
				{
					NKCUIPopupOfficePresetList.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPopupOfficePresetList>("ab_ui_office", "AB_UI_POPUP_OFFICE_PRESET_LIST", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPopupOfficePresetList.CleanupInstance)).GetInstance<NKCUIPopupOfficePresetList>();
					NKCUIPopupOfficePresetList.m_Instance.InitUI();
				}
				return NKCUIPopupOfficePresetList.m_Instance;
			}
		}

		// Token: 0x06007F37 RID: 32567 RVA: 0x002AABFB File Offset: 0x002A8DFB
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPopupOfficePresetList.m_Instance != null && NKCUIPopupOfficePresetList.m_Instance.IsOpen)
			{
				NKCUIPopupOfficePresetList.m_Instance.Close();
			}
		}

		// Token: 0x06007F38 RID: 32568 RVA: 0x002AAC20 File Offset: 0x002A8E20
		private static void CleanupInstance()
		{
			NKCUIPopupOfficePresetList.m_Instance = null;
		}

		// Token: 0x170014FA RID: 5370
		// (get) Token: 0x06007F39 RID: 32569 RVA: 0x002AAC28 File Offset: 0x002A8E28
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIPopupOfficePresetList.m_Instance != null && NKCUIPopupOfficePresetList.m_Instance.IsOpen;
			}
		}

		// Token: 0x170014FB RID: 5371
		// (get) Token: 0x06007F3A RID: 32570 RVA: 0x002AAC43 File Offset: 0x002A8E43
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170014FC RID: 5372
		// (get) Token: 0x06007F3B RID: 32571 RVA: 0x002AAC46 File Offset: 0x002A8E46
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x06007F3C RID: 32572 RVA: 0x002AAC4D File Offset: 0x002A8E4D
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007F3D RID: 32573 RVA: 0x002AAC5C File Offset: 0x002A8E5C
		private void InitUI()
		{
			if (this.m_ScrollRect != null)
			{
				this.m_ScrollRect.dOnGetObject += this.GetSlot;
				this.m_ScrollRect.dOnReturnObject += this.ReturnSlot;
				this.m_ScrollRect.dOnProvideData += this.ProvideSlotData;
				this.m_ScrollRect.SetAutoResize(2, false);
				NKCUtil.SetScrollHotKey(this.m_ScrollRect, null);
				this.m_ScrollRect.PrepareCells(0);
			}
			NKCUtil.SetButtonClickDelegate(this.m_csbtnClose, new UnityAction(base.Close));
		}

		// Token: 0x06007F3E RID: 32574 RVA: 0x002AACF8 File Offset: 0x002A8EF8
		public void Open(int currentRoomID, NKCUIPopupOfficePresetList.OnAction onAction)
		{
			base.gameObject.SetActive(true);
			this.dOnAction = onAction;
			this.m_currentRoomID = currentRoomID;
			if (this.m_ScrollRect != null)
			{
				this.m_ScrollRect.TotalCount = Mathf.Min(NKCScenManager.CurrentUserData().OfficeData.GetPresetCount() + 1, NKMCommonConst.Office.PresetConst.MaxCount);
				this.m_ScrollRect.SetIndexPosition(0);
			}
			this.SetSlotCountText();
			base.UIOpened(true);
		}

		// Token: 0x06007F3F RID: 32575 RVA: 0x002AAD76 File Offset: 0x002A8F76
		private RectTransform GetSlot(int index)
		{
			NKCUIPopupOfficePresetSlot nkcuipopupOfficePresetSlot = UnityEngine.Object.Instantiate<NKCUIPopupOfficePresetSlot>(this.m_SlotPrefab);
			nkcuipopupOfficePresetSlot.Init();
			nkcuipopupOfficePresetSlot.SetLoopScroll(this.m_ScrollRect);
			return nkcuipopupOfficePresetSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06007F40 RID: 32576 RVA: 0x002AAD9A File Offset: 0x002A8F9A
		private void ReturnSlot(Transform go)
		{
			UnityEngine.Object.Destroy(go.gameObject);
		}

		// Token: 0x06007F41 RID: 32577 RVA: 0x002AADA8 File Offset: 0x002A8FA8
		private void ProvideSlotData(Transform tr, int idx)
		{
			NKCUIPopupOfficePresetSlot component = tr.GetComponent<NKCUIPopupOfficePresetSlot>();
			int presetCount = NKCScenManager.CurrentUserData().OfficeData.GetPresetCount();
			if (idx < presetCount)
			{
				NKMOfficePreset preset = NKCScenManager.CurrentUserData().OfficeData.GetPreset(idx);
				component.SetData(this.m_currentRoomID, preset, this.dOnAction);
				return;
			}
			component.SetPlus(this.dOnAction);
		}

		// Token: 0x06007F42 RID: 32578 RVA: 0x002AAE04 File Offset: 0x002A9004
		public void Refresh(int index = -1)
		{
			if (index < 0)
			{
				this.m_ScrollRect.TotalCount = Mathf.Min(NKCScenManager.CurrentUserData().OfficeData.GetPresetCount() + 1, NKMCommonConst.Office.PresetConst.MaxCount);
				this.m_ScrollRect.RefreshCells(false);
			}
			else
			{
				Transform child = this.m_ScrollRect.GetChild(index);
				if (child != null)
				{
					this.ProvideSlotData(child, index);
				}
			}
			this.SetSlotCountText();
		}

		// Token: 0x06007F43 RID: 32579 RVA: 0x002AAE78 File Offset: 0x002A9078
		public void PlayUnlockEffect(int unlockCount)
		{
			for (int i = 0; i < unlockCount; i++)
			{
				int index = NKCScenManager.CurrentUserData().OfficeData.GetPresetCount() - 1 - i;
				Transform child = this.m_ScrollRect.GetChild(index);
				if (child != null)
				{
					NKCUIPopupOfficePresetSlot component = child.GetComponent<NKCUIPopupOfficePresetSlot>();
					if (component != null)
					{
						component.PlayUnlockEffect();
					}
				}
			}
		}

		// Token: 0x06007F44 RID: 32580 RVA: 0x002AAED4 File Offset: 0x002A90D4
		private void SetSlotCountText()
		{
			int presetCount = NKCScenManager.CurrentUserData().OfficeData.GetPresetCount();
			NKCUtil.SetLabelText(this.m_lbPresetCount, string.Format(this.m_strCountFormat, presetCount, NKMCommonConst.Office.PresetConst.MaxCount));
		}

		// Token: 0x04006BC6 RID: 27590
		private const string ASSET_BUNDLE_NAME = "ab_ui_office";

		// Token: 0x04006BC7 RID: 27591
		private const string UI_ASSET_NAME = "AB_UI_POPUP_OFFICE_PRESET_LIST";

		// Token: 0x04006BC8 RID: 27592
		private static NKCUIPopupOfficePresetList m_Instance;

		// Token: 0x04006BC9 RID: 27593
		private NKCUIPopupOfficePresetList.OnAction dOnAction;

		// Token: 0x04006BCA RID: 27594
		public NKCUIPopupOfficePresetSlot m_SlotPrefab;

		// Token: 0x04006BCB RID: 27595
		public LoopScrollRect m_ScrollRect;

		// Token: 0x04006BCC RID: 27596
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x04006BCD RID: 27597
		public Text m_lbPresetCount;

		// Token: 0x04006BCE RID: 27598
		public string m_strCountFormat = "{0}/{1}";

		// Token: 0x04006BCF RID: 27599
		private int m_currentRoomID;

		// Token: 0x02001889 RID: 6281
		public enum ActionType
		{
			// Token: 0x0400A92B RID: 43307
			Save,
			// Token: 0x0400A92C RID: 43308
			Load,
			// Token: 0x0400A92D RID: 43309
			Clear,
			// Token: 0x0400A92E RID: 43310
			Rename,
			// Token: 0x0400A92F RID: 43311
			Add
		}

		// Token: 0x0200188A RID: 6282
		// (Invoke) Token: 0x0600B62E RID: 46638
		public delegate void OnAction(NKCUIPopupOfficePresetList.ActionType type, int id, string name);
	}
}
