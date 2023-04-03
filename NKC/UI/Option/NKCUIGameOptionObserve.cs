using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Option
{
	// Token: 0x02000B92 RID: 2962
	public class NKCUIGameOptionObserve : NKCUIGameOptionContentBase
	{
		// Token: 0x170015FA RID: 5626
		// (get) Token: 0x060088CF RID: 35023 RVA: 0x002E4A22 File Offset: 0x002E2C22
		private string OBSERVE_LEAVE_POPUP_TITLE
		{
			get
			{
				return "관전 LEAVE TITLE";
			}
		}

		// Token: 0x170015FB RID: 5627
		// (get) Token: 0x060088D0 RID: 35024 RVA: 0x002E4A29 File Offset: 0x002E2C29
		private string OBSERVE_LEAVE_POPUP_DESC
		{
			get
			{
				return "관전 LEAVE DESC";
			}
		}

		// Token: 0x060088D1 RID: 35025 RVA: 0x002E4A30 File Offset: 0x002E2C30
		public override void Init()
		{
			this.m_csbtnExit.PointerClick.RemoveAllListeners();
			this.m_csbtnExit.PointerClick.AddListener(new UnityAction(this.LeaveObserve));
		}

		// Token: 0x060088D2 RID: 35026 RVA: 0x002E4A5E File Offset: 0x002E2C5E
		public override void SetContent()
		{
			NKCUtil.SetLabelText(this.m_NKM_UI_GAME_OPTION_OBSERVE_TEXT_TITLE, NKCUtilString.GET_STRING_GAUNTLET.ToUpper());
			NKCUtil.SetLabelText(this.m_NKM_UI_GAME_OPTION_OBSERVE_TEXT_SUB_TITLE, "");
			NKCUtil.SetLabelText(this.m_NKM_UI_GAME_OPTION_OBSERVE_TEXT_DESC, "");
		}

		// Token: 0x060088D3 RID: 35027 RVA: 0x002E4A95 File Offset: 0x002E2C95
		public void LeaveObserve()
		{
			NKCPopupOKCancel.OpenOKCancelBox(this.OBSERVE_LEAVE_POPUP_TITLE, this.OBSERVE_LEAVE_POPUP_DESC, new NKCPopupOKCancel.OnButton(NKCUIGameOptionObserve.OnClickLeaveObserveOkButton), null, false);
		}

		// Token: 0x060088D4 RID: 35028 RVA: 0x002E4AB6 File Offset: 0x002E2CB6
		private static void OnClickLeaveObserveOkButton()
		{
			NKCPrivatePVPRoomMgr.Send_NKMPacket_PRIVATE_PVP_EXIT_REQ();
		}

		// Token: 0x0400754F RID: 30031
		public Text m_NKM_UI_GAME_OPTION_OBSERVE_TEXT_TITLE;

		// Token: 0x04007550 RID: 30032
		public Text m_NKM_UI_GAME_OPTION_OBSERVE_TEXT_SUB_TITLE;

		// Token: 0x04007551 RID: 30033
		public Text m_NKM_UI_GAME_OPTION_OBSERVE_TEXT_DESC;

		// Token: 0x04007552 RID: 30034
		public NKCUIComStateButton m_csbtnExit;
	}
}
