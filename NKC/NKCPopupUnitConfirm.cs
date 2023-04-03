using System;
using NKC.UI;
using NKM;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007FD RID: 2045
	public class NKCPopupUnitConfirm : NKCUIBase
	{
		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x060050FB RID: 20731 RVA: 0x001892D8 File Offset: 0x001874D8
		public static NKCPopupUnitConfirm Instance
		{
			get
			{
				if (NKCPopupUnitConfirm.m_Instance == null)
				{
					NKCPopupUnitConfirm.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupUnitConfirm>("AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX", "NKM_UI_POPUP_UNIT_CONFIRM", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupUnitConfirm.CleanupInstance)).GetInstance<NKCPopupUnitConfirm>();
					NKCPopupUnitConfirm.m_Instance.Initialize();
				}
				return NKCPopupUnitConfirm.m_Instance;
			}
		}

		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x060050FC RID: 20732 RVA: 0x00189327 File Offset: 0x00187527
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupUnitConfirm.m_Instance != null && NKCPopupUnitConfirm.m_Instance.IsOpen;
			}
		}

		// Token: 0x060050FD RID: 20733 RVA: 0x00189342 File Offset: 0x00187542
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupUnitConfirm.m_Instance != null && NKCPopupUnitConfirm.m_Instance.IsOpen)
			{
				NKCPopupUnitConfirm.m_Instance.Close();
			}
		}

		// Token: 0x060050FE RID: 20734 RVA: 0x00189367 File Offset: 0x00187567
		private static void CleanupInstance()
		{
			NKCPopupUnitConfirm.m_Instance = null;
		}

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x060050FF RID: 20735 RVA: 0x0018936F File Offset: 0x0018756F
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x06005100 RID: 20736 RVA: 0x00189372 File Offset: 0x00187572
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x06005101 RID: 20737 RVA: 0x00189379 File Offset: 0x00187579
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06005102 RID: 20738 RVA: 0x00189388 File Offset: 0x00187588
		public override void Initialize()
		{
			NKCUtil.SetBindFunction(this.m_csbtnOk, new UnityAction(this.OnClickOK));
			NKCUtil.SetBindFunction(this.m_csbtnCancel, new UnityAction(this.OnClickCancel));
			NKCUtil.SetHotkey(this.m_csbtnOk, HotkeyEventType.Confirm, null, false);
			this.m_UnitSlot.Init(false);
		}

		// Token: 0x06005103 RID: 20739 RVA: 0x001893E0 File Offset: 0x001875E0
		public void Open(long unitUID, string strTitle, string strDesc, string strDesc2, UnityAction onOK, UnityAction onCancel = null)
		{
			NKMUnitData unitFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(unitUID);
			this.Open(unitFromUID, strTitle, strDesc, strDesc2, onOK, onCancel);
		}

		// Token: 0x06005104 RID: 20740 RVA: 0x00189410 File Offset: 0x00187610
		public void Open(NKMUnitData targetUnit, string strTitle, string strDesc, string strDesc2, UnityAction onOK, UnityAction onCancel = null)
		{
			this.m_dOnOK = onOK;
			this.m_dOnCancel = onCancel;
			NKCUtil.SetLabelText(this.m_lbTitle, strTitle);
			NKCUtil.SetLabelText(this.m_lbDesc, strDesc);
			NKCUtil.SetLabelText(this.m_lbDesc2, strDesc2);
			NKCUtil.SetGameobjectActive(this.m_lbDesc2.gameObject, !string.IsNullOrEmpty(strDesc2));
			this.m_UnitSlot.SetData(targetUnit, NKMDeckIndex.None, true, null);
			NKCUtil.SetEventTriggerDelegate(this.m_eventTrigger, new UnityAction(base.Close));
			base.UIOpened(true);
		}

		// Token: 0x06005105 RID: 20741 RVA: 0x0018949D File Offset: 0x0018769D
		private void OnClickOK()
		{
			UnityAction dOnOK = this.m_dOnOK;
			if (dOnOK != null)
			{
				dOnOK();
			}
			base.Close();
		}

		// Token: 0x06005106 RID: 20742 RVA: 0x001894B6 File Offset: 0x001876B6
		private void OnClickCancel()
		{
			UnityAction dOnCancel = this.m_dOnCancel;
			if (dOnCancel != null)
			{
				dOnCancel();
			}
			base.Close();
		}

		// Token: 0x04004164 RID: 16740
		public const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX";

		// Token: 0x04004165 RID: 16741
		public const string UI_ASSET_NAME = "NKM_UI_POPUP_UNIT_CONFIRM";

		// Token: 0x04004166 RID: 16742
		private static NKCPopupUnitConfirm m_Instance;

		// Token: 0x04004167 RID: 16743
		public NKCUIUnitSelectListSlot m_UnitSlot;

		// Token: 0x04004168 RID: 16744
		public Text m_lbTitle;

		// Token: 0x04004169 RID: 16745
		public Text m_lbDesc;

		// Token: 0x0400416A RID: 16746
		public Text m_lbDesc2;

		// Token: 0x0400416B RID: 16747
		public NKCUIComStateButton m_csbtnOk;

		// Token: 0x0400416C RID: 16748
		public NKCUIComStateButton m_csbtnCancel;

		// Token: 0x0400416D RID: 16749
		public EventTrigger m_eventTrigger;

		// Token: 0x0400416E RID: 16750
		private UnityAction m_dOnOK;

		// Token: 0x0400416F RID: 16751
		private UnityAction m_dOnCancel;
	}
}
