using System;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A6E RID: 2670
	public class NKCPopupMiscUseCount : NKCUIBase
	{
		// Token: 0x170013A0 RID: 5024
		// (get) Token: 0x060075C6 RID: 30150 RVA: 0x00272BE4 File Offset: 0x00270DE4
		public static NKCPopupMiscUseCount Instance
		{
			get
			{
				if (NKCPopupMiscUseCount.m_Instance == null)
				{
					NKCPopupMiscUseCount.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupMiscUseCount>("ab_ui_nkm_ui_popup_ok_cancel_box", "NKM_UI_POPUP_MISC_USE_COUNT", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupMiscUseCount.CleanupInstance)).GetInstance<NKCPopupMiscUseCount>();
					NKCPopupMiscUseCount.m_Instance.InitUI();
				}
				return NKCPopupMiscUseCount.m_Instance;
			}
		}

		// Token: 0x060075C7 RID: 30151 RVA: 0x00272C33 File Offset: 0x00270E33
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupMiscUseCount.m_Instance != null && NKCPopupMiscUseCount.m_Instance.IsOpen)
			{
				NKCPopupMiscUseCount.m_Instance.Close();
			}
		}

		// Token: 0x060075C8 RID: 30152 RVA: 0x00272C58 File Offset: 0x00270E58
		private static void CleanupInstance()
		{
			NKCPopupMiscUseCount.m_Instance = null;
		}

		// Token: 0x170013A1 RID: 5025
		// (get) Token: 0x060075C9 RID: 30153 RVA: 0x00272C60 File Offset: 0x00270E60
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x170013A2 RID: 5026
		// (get) Token: 0x060075CA RID: 30154 RVA: 0x00272C67 File Offset: 0x00270E67
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x060075CB RID: 30155 RVA: 0x00272C6A File Offset: 0x00270E6A
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060075CC RID: 30156 RVA: 0x00272C78 File Offset: 0x00270E78
		private void InitUI()
		{
			this.m_btnCancel.PointerClick.RemoveAllListeners();
			this.m_btnCancel.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_btnAction.PointerClick.RemoveAllListeners();
			this.m_btnAction.PointerClick.AddListener(new UnityAction(this.OnAction));
			NKCUtil.SetHotkey(this.m_btnAction, HotkeyEventType.Confirm, null, false);
			NKCPopupMiscUseCountContents contents = this.m_contents;
			if (contents == null)
			{
				return;
			}
			contents.Init();
		}

		// Token: 0x060075CD RID: 30157 RVA: 0x00272CFC File Offset: 0x00270EFC
		public void Open(NKCPopupMiscUseCount.USE_ITEM_TYPE useItemType, int useItemId, NKCUISlot.SlotData slotData, NKCPopupMiscUseCount.OnButton onButton = null)
		{
			if (slotData == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			if (useItemType != NKCPopupMiscUseCount.USE_ITEM_TYPE.Common)
			{
				if (useItemType == NKCPopupMiscUseCount.USE_ITEM_TYPE.DailyTicket)
				{
					string itemName = NKMItemManager.GetItemMiscTempletByID(slotData.ID).GetItemName();
					NKCUtil.SetLabelText(this.m_lbTitle, string.Format(NKCStringTable.GetString("SI_DP_POPUP_USE_COUNT_TOP_TEXT_MISC_DAILY_TICKET", false), itemName));
				}
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbTitle, NKCStringTable.GetString("SI_DP_POPUP_USE_COUNT_TOP_TEXT_MISC", false));
			}
			this.m_slotData = slotData;
			this.m_contents.SetData(useItemType, useItemId, this.m_slotData);
			this.dOnActionButton = onButton;
			base.UIOpened(true);
		}

		// Token: 0x060075CE RID: 30158 RVA: 0x00272D90 File Offset: 0x00270F90
		private void OnAction()
		{
			base.Close();
			NKCPopupMiscUseCount.OnButton onButton = this.dOnActionButton;
			if (onButton == null)
			{
				return;
			}
			onButton(this.m_slotData.ID, (int)this.m_contents.m_useCount);
		}

		// Token: 0x0400622B RID: 25131
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_ok_cancel_box";

		// Token: 0x0400622C RID: 25132
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_MISC_USE_COUNT";

		// Token: 0x0400622D RID: 25133
		private static NKCPopupMiscUseCount m_Instance;

		// Token: 0x0400622E RID: 25134
		public Text m_lbTitle;

		// Token: 0x0400622F RID: 25135
		public NKCPopupMiscUseCountContents m_contents;

		// Token: 0x04006230 RID: 25136
		[Header("하단 버튼")]
		public NKCUIComStateButton m_btnCancel;

		// Token: 0x04006231 RID: 25137
		public NKCUIComStateButton m_btnAction;

		// Token: 0x04006232 RID: 25138
		public Text m_txtAction;

		// Token: 0x04006233 RID: 25139
		private NKCPopupMiscUseCount.OnButton dOnActionButton;

		// Token: 0x04006234 RID: 25140
		private NKCUISlot.SlotData m_slotData;

		// Token: 0x020017D0 RID: 6096
		public enum USE_ITEM_TYPE
		{
			// Token: 0x0400A791 RID: 42897
			Common,
			// Token: 0x0400A792 RID: 42898
			DailyTicket
		}

		// Token: 0x020017D1 RID: 6097
		// (Invoke) Token: 0x0600B448 RID: 46152
		public delegate void OnButton(int useItemId, int useItemCount);
	}
}
