using System;
using NKM;
using NKM.Templet;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A9F RID: 2719
	public class NKCUIPopupScoutConfirm : NKCUIBase
	{
		// Token: 0x17001438 RID: 5176
		// (get) Token: 0x06007879 RID: 30841 RVA: 0x00280014 File Offset: 0x0027E214
		public static NKCUIPopupScoutConfirm Instance
		{
			get
			{
				if (NKCUIPopupScoutConfirm.s_Instance == null)
				{
					NKCUIPopupScoutConfirm.s_Instance = NKCUIManager.OpenNewInstance<NKCUIPopupScoutConfirm>("ab_ui_nkm_ui_popup_ok_cancel_box", "NKM_UI_POPUP_UNIT_SCOUT", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPopupScoutConfirm.CleanupInstance)).GetInstance<NKCUIPopupScoutConfirm>();
					NKCUIPopupScoutConfirm.s_Instance.Init();
				}
				return NKCUIPopupScoutConfirm.s_Instance;
			}
		}

		// Token: 0x0600787A RID: 30842 RVA: 0x00280063 File Offset: 0x0027E263
		private static void CleanupInstance()
		{
			NKCUIPopupScoutConfirm.s_Instance = null;
		}

		// Token: 0x17001439 RID: 5177
		// (get) Token: 0x0600787B RID: 30843 RVA: 0x0028006B File Offset: 0x0027E26B
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIPopupScoutConfirm.s_Instance != null && NKCUIPopupScoutConfirm.s_Instance.IsOpen;
			}
		}

		// Token: 0x0600787C RID: 30844 RVA: 0x00280086 File Offset: 0x0027E286
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPopupScoutConfirm.s_Instance != null && NKCUIPopupScoutConfirm.s_Instance.IsOpen)
			{
				NKCUIPopupScoutConfirm.s_Instance.Close();
			}
		}

		// Token: 0x1700143A RID: 5178
		// (get) Token: 0x0600787D RID: 30845 RVA: 0x002800AB File Offset: 0x0027E2AB
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700143B RID: 5179
		// (get) Token: 0x0600787E RID: 30846 RVA: 0x002800AE File Offset: 0x0027E2AE
		public override string MenuName
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_PERSONNEL_SCOUT_TEXT", false);
			}
		}

		// Token: 0x0600787F RID: 30847 RVA: 0x002800BB File Offset: 0x0027E2BB
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007880 RID: 30848 RVA: 0x002800CC File Offset: 0x0027E2CC
		private void Init()
		{
			NKCUIUnitSelectListSlot unitSlot = this.m_UnitSlot;
			if (unitSlot != null)
			{
				unitSlot.Init(false);
			}
			NKCUIUnitSelectListSlot unitSlot2 = this.m_UnitSlot;
			if (unitSlot2 != null)
			{
				unitSlot2.SetSlotState(NKCUnitSortSystem.eUnitState.NONE);
			}
			NKCUIComStateButton csbtnOK = this.m_csbtnOK;
			if (csbtnOK != null)
			{
				csbtnOK.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton csbtnOK2 = this.m_csbtnOK;
			if (csbtnOK2 != null)
			{
				csbtnOK2.PointerClick.AddListener(new UnityAction(this.OnBtnConfirm));
			}
			NKCUtil.SetHotkey(this.m_csbtnOK, HotkeyEventType.Confirm, null, false);
			NKCUIComStateButton csbtnClose = this.m_csbtnClose;
			if (csbtnClose != null)
			{
				csbtnClose.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton csbtnClose2 = this.m_csbtnClose;
			if (csbtnClose2 != null)
			{
				csbtnClose2.PointerClick.AddListener(new UnityAction(base.Close));
			}
			NKCUIComStateButton csbtnCancel = this.m_csbtnCancel;
			if (csbtnCancel != null)
			{
				csbtnCancel.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton csbtnCancel2 = this.m_csbtnCancel;
			if (csbtnCancel2 != null)
			{
				csbtnCancel2.PointerClick.AddListener(new UnityAction(base.Close));
			}
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(delegate(BaseEventData eventData)
			{
				base.Close();
			});
			EventTrigger evtBackground = this.m_evtBackground;
			if (evtBackground == null)
			{
				return;
			}
			evtBackground.triggers.Add(entry);
		}

		// Token: 0x06007881 RID: 30849 RVA: 0x002801F0 File Offset: 0x0027E3F0
		public void Open(NKMPieceTemplet templet, NKCUIPopupScoutConfirm.OnConfirm onConfirm)
		{
			if (templet == null)
			{
				return;
			}
			this.dOnConfirm = onConfirm;
			base.gameObject.SetActive(true);
			NKCUIScoutUnitPiece unitPiece = this.m_UnitPiece;
			if (unitPiece != null)
			{
				unitPiece.SetData(templet);
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(templet.m_PieceGetUintId);
			NKCUIUnitSelectListSlot unitSlot = this.m_UnitSlot;
			if (unitSlot != null)
			{
				unitSlot.SetData(unitTempletBase, 1, true, null);
			}
			NKCUtil.SetLabelText(this.m_lbTargetUnit, NKCStringTable.GetString("SI_PF_PERSONNEL_SCOUT_CONFIRM_TEXT", false), new object[]
			{
				unitTempletBase.GetUnitName()
			});
			base.UIOpened(true);
		}

		// Token: 0x06007882 RID: 30850 RVA: 0x00280273 File Offset: 0x0027E473
		private void OnBtnConfirm()
		{
			base.Close();
			NKCUIPopupScoutConfirm.OnConfirm onConfirm = this.dOnConfirm;
			if (onConfirm == null)
			{
				return;
			}
			onConfirm();
		}

		// Token: 0x04006508 RID: 25864
		public const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_ok_cancel_box";

		// Token: 0x04006509 RID: 25865
		public const string UI_ASSET_NAME = "NKM_UI_POPUP_UNIT_SCOUT";

		// Token: 0x0400650A RID: 25866
		private static NKCUIPopupScoutConfirm s_Instance;

		// Token: 0x0400650B RID: 25867
		public Text m_lbTargetUnit;

		// Token: 0x0400650C RID: 25868
		public NKCUIScoutUnitPiece m_UnitPiece;

		// Token: 0x0400650D RID: 25869
		public NKCUIUnitSelectListSlot m_UnitSlot;

		// Token: 0x0400650E RID: 25870
		public NKCUIComStateButton m_csbtnCancel;

		// Token: 0x0400650F RID: 25871
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x04006510 RID: 25872
		public NKCUIComStateButton m_csbtnOK;

		// Token: 0x04006511 RID: 25873
		public EventTrigger m_evtBackground;

		// Token: 0x04006512 RID: 25874
		private NKCUIPopupScoutConfirm.OnConfirm dOnConfirm;

		// Token: 0x020017F7 RID: 6135
		// (Invoke) Token: 0x0600B4C8 RID: 46280
		public delegate void OnConfirm();
	}
}
