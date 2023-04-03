using System;
using NKC.UI.Lobby;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI
{
	// Token: 0x02000A60 RID: 2656
	public class NKCPopupHamburgerMenuSimpleButton : NKCUILobbyMenuButtonBase
	{
		// Token: 0x060074CF RID: 29903 RVA: 0x0026D55C File Offset: 0x0026B75C
		public void Init(NKCPopupHamburgerMenuSimpleButton.DotEnableConditionFunction dotConditionFunc, NKCPopupHamburgerMenuSimpleButton.OnButton onButton, ContentsType contentsType = ContentsType.None)
		{
			this.dDotEnableConditionFunction = dotConditionFunc;
			this.dOnButton = onButton;
			this.m_ContentsType = contentsType;
			this.m_csbtnButton.PointerClick.RemoveAllListeners();
			this.m_csbtnButton.PointerClick.AddListener(new UnityAction(this.OnBtn));
			NKCUtil.SetGameobjectActive(this.m_objEvent, false);
		}

		// Token: 0x060074D0 RID: 29904 RVA: 0x0026D5B8 File Offset: 0x0026B7B8
		protected override void ContentsUpdate(NKMUserData userData)
		{
			bool flag = this.dDotEnableConditionFunction != null && this.dDotEnableConditionFunction(userData);
			this.SetNotify(flag);
			NKCUtil.SetGameobjectActive(this.m_objReddot, flag);
		}

		// Token: 0x060074D1 RID: 29905 RVA: 0x0026D5F0 File Offset: 0x0026B7F0
		protected override void SetNotify(bool value)
		{
			base.SetNotify(value);
			NKCUtil.SetGameobjectActive(this.m_objReddot, value);
		}

		// Token: 0x060074D2 RID: 29906 RVA: 0x0026D608 File Offset: 0x0026B808
		private void OnBtn()
		{
			if (this.m_bLocked)
			{
				NKCContentManager.ShowLockedMessagePopup(this.m_ContentsType, 0);
				return;
			}
			if (this.m_ContentsType != ContentsType.BASE_FACTORY && NKCUIForge.IsInstanceOpen && NKCScenManager.GetScenManager().GetMyUserData().hasReservedEquipTuningData())
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_FORGE_TUNING_EXIT_CONFIRM, delegate()
				{
					NKCPacketSender.Send_NKMPacket_Equip_Tuning_Cancel_REQ();
					NKCPopupHamburgerMenuSimpleButton.OnButton onButton2 = this.dOnButton;
					if (onButton2 == null)
					{
						return;
					}
					onButton2();
				}, null, false);
				return;
			}
			if (this.m_ContentsType != ContentsType.BASE_FACTORY && NKCUIForge.IsInstanceOpen && NKCScenManager.GetScenManager().GetMyUserData().hasReservedSetOptionData())
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_FORGE_SET_OPTION_TUNING_EXIT_CONFIRM, delegate()
				{
					NKCPacketSender.Send_NKMPacket_Equip_Tuning_Cancel_REQ();
					NKCPopupHamburgerMenuSimpleButton.OnButton onButton2 = this.dOnButton;
					if (onButton2 == null)
					{
						return;
					}
					onButton2();
				}, null, false);
				return;
			}
			if (NKCPopupShipCommandModule.IsInstanceOpen && NKCScenManager.GetScenManager().GetMyUserData().GetShipCandidateData().shipUid > 0L)
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_SHIP_COMMANDMODULE_EXIT_CONFIRM, delegate()
				{
					NKCPacketSender.Send_NKMPacket_SHIP_SLOT_OPTION_CANCEL_REQ();
					NKCPopupHamburgerMenuSimpleButton.OnButton onButton2 = this.dOnButton;
					if (onButton2 == null)
					{
						return;
					}
					onButton2();
				}, null, false);
				return;
			}
			NKCPopupHamburgerMenuSimpleButton.OnButton onButton = this.dOnButton;
			if (onButton == null)
			{
				return;
			}
			onButton();
		}

		// Token: 0x060074D3 RID: 29907 RVA: 0x0026D6F8 File Offset: 0x0026B8F8
		public void SetEnableEvent(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objEvent, bValue);
		}

		// Token: 0x04006124 RID: 24868
		public GameObject m_objReddot;

		// Token: 0x04006125 RID: 24869
		public NKCUIComStateButton m_csbtnButton;

		// Token: 0x04006126 RID: 24870
		public GameObject m_objEvent;

		// Token: 0x04006127 RID: 24871
		private NKCPopupHamburgerMenuSimpleButton.DotEnableConditionFunction dDotEnableConditionFunction;

		// Token: 0x04006128 RID: 24872
		private NKCPopupHamburgerMenuSimpleButton.OnButton dOnButton;

		// Token: 0x04006129 RID: 24873
		private NKCPopupHamburgerMenuSimpleButton.OnLocked dOnLocked;

		// Token: 0x020017B7 RID: 6071
		// (Invoke) Token: 0x0600B408 RID: 46088
		public delegate bool DotEnableConditionFunction(NKMUserData userData);

		// Token: 0x020017B8 RID: 6072
		// (Invoke) Token: 0x0600B40C RID: 46092
		public delegate void OnButton();

		// Token: 0x020017B9 RID: 6073
		// (Invoke) Token: 0x0600B410 RID: 46096
		public delegate void OnLocked();
	}
}
