using System;
using NKC.UI;
using NKM;
using NKM.Templet;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000800 RID: 2048
	public class NKCUIPopupRearmamentConfirmBox : NKCUIBase
	{
		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x0600511C RID: 20764 RVA: 0x00189B28 File Offset: 0x00187D28
		public static NKCUIPopupRearmamentConfirmBox Instance
		{
			get
			{
				if (NKCUIPopupRearmamentConfirmBox.m_Instance == null)
				{
					NKCUIPopupRearmamentConfirmBox.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPopupRearmamentConfirmBox>("ab_ui_rearm", "AB_UI_POPUP_REARM_FINAL_CONFIRM", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPopupRearmamentConfirmBox.CleanupInstance)).GetInstance<NKCUIPopupRearmamentConfirmBox>();
					NKCUIPopupRearmamentConfirmBox.m_Instance.InitUI();
				}
				return NKCUIPopupRearmamentConfirmBox.m_Instance;
			}
		}

		// Token: 0x0600511D RID: 20765 RVA: 0x00189B77 File Offset: 0x00187D77
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPopupRearmamentConfirmBox.m_Instance != null && NKCUIPopupRearmamentConfirmBox.m_Instance.IsOpen)
			{
				NKCUIPopupRearmamentConfirmBox.m_Instance.Close();
			}
		}

		// Token: 0x0600511E RID: 20766 RVA: 0x00189B9C File Offset: 0x00187D9C
		private static void CleanupInstance()
		{
			NKCUIPopupRearmamentConfirmBox.m_Instance = null;
		}

		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x0600511F RID: 20767 RVA: 0x00189BA4 File Offset: 0x00187DA4
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIPopupRearmamentConfirmBox.m_Instance != null && NKCUIPopupRearmamentConfirmBox.m_Instance.IsOpen;
			}
		}

		// Token: 0x06005120 RID: 20768 RVA: 0x00189BBF File Offset: 0x00187DBF
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x06005121 RID: 20769 RVA: 0x00189BCD File Offset: 0x00187DCD
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x06005122 RID: 20770 RVA: 0x00189BD0 File Offset: 0x00187DD0
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_REARM_CONFIRM_POPIP_BOX_TITLE;
			}
		}

		// Token: 0x06005123 RID: 20771 RVA: 0x00189BD8 File Offset: 0x00187DD8
		private void InitUI()
		{
			NKCUtil.SetButtonClickDelegate(this.m_btnOK, new UnityAction(this.OnClickOK));
			NKCUtil.SetHotkey(this.m_btnOK, HotkeyEventType.Confirm, null, false);
			NKCUtil.SetButtonClickDelegate(this.m_btnCancel, new UnityAction(base.Close));
			NKCUIUnitSelectListSlot baseUnitSlot = this.m_BaseUnitSlot;
			if (baseUnitSlot != null)
			{
				baseUnitSlot.Init(true);
			}
			NKCUIUnitSelectListSlot targetUnitSlot = this.m_TargetUnitSlot;
			if (targetUnitSlot == null)
			{
				return;
			}
			targetUnitSlot.Init(true);
		}

		// Token: 0x06005124 RID: 20772 RVA: 0x00189C44 File Offset: 0x00187E44
		public void Open(long resourceUnitUID, int TargetUnitID)
		{
			NKMUnitData unitFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(resourceUnitUID);
			if (unitFromUID == null)
			{
				return;
			}
			NKMUnitData nkmunitData = new NKMUnitData();
			nkmunitData.m_UnitID = TargetUnitID;
			nkmunitData.m_SkinID = 0;
			nkmunitData.m_UnitLevel = 1;
			nkmunitData.tacticLevel = unitFromUID.tacticLevel;
			this.m_BaseUnitSlot.SetDataForRearm(unitFromUID, new NKMDeckIndex(NKM_DECK_TYPE.NDT_NONE), false, null, false, true, false);
			this.m_TargetUnitSlot.SetDataForRearm(nkmunitData, new NKMDeckIndex(NKM_DECK_TYPE.NDT_NONE), false, null, false, true, false);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(TargetUnitID);
			string msg = string.Format(NKCUtilString.GET_STRING_REARM_CONFIRM_POPUP_FINAL_BOX_DESC, unitTempletBase.GetUnitName());
			NKCUtil.SetLabelText(this.m_lbDesc, msg);
			this.m_lResourceRearmUID = resourceUnitUID;
			this.m_iTargetRearmID = TargetUnitID;
			base.UIOpened(true);
		}

		// Token: 0x06005125 RID: 20773 RVA: 0x00189CF4 File Offset: 0x00187EF4
		private void OnClickOK()
		{
			NKCPacketSender.Send_NKMPacket_REARMAMENT_UNIT_REQ(this.m_lResourceRearmUID, this.m_iTargetRearmID);
		}

		// Token: 0x0400417A RID: 16762
		private const string ASSET_BUNDLE_NAME = "ab_ui_rearm";

		// Token: 0x0400417B RID: 16763
		private const string UI_ASSET_NAME = "AB_UI_POPUP_REARM_FINAL_CONFIRM";

		// Token: 0x0400417C RID: 16764
		private static NKCUIPopupRearmamentConfirmBox m_Instance;

		// Token: 0x0400417D RID: 16765
		public NKCUIUnitSelectListSlot m_BaseUnitSlot;

		// Token: 0x0400417E RID: 16766
		public NKCUIUnitSelectListSlot m_TargetUnitSlot;

		// Token: 0x0400417F RID: 16767
		public Text m_lbDesc;

		// Token: 0x04004180 RID: 16768
		public NKCUIComStateButton m_btnOK;

		// Token: 0x04004181 RID: 16769
		public NKCUIComStateButton m_btnCancel;

		// Token: 0x04004182 RID: 16770
		private long m_lResourceRearmUID;

		// Token: 0x04004183 RID: 16771
		private int m_iTargetRearmID;
	}
}
