using System;
using ClientPacket.Community;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Friend
{
	// Token: 0x02000B1B RID: 2843
	public class NKCPopupFriendRequest : NKCUIBase
	{
		// Token: 0x17001521 RID: 5409
		// (get) Token: 0x0600817C RID: 33148 RVA: 0x002BA740 File Offset: 0x002B8940
		public static NKCPopupFriendRequest Instance
		{
			get
			{
				if (NKCPopupFriendRequest.m_Instance == null)
				{
					NKCPopupFriendRequest.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupFriendRequest>("ab_ui_nkm_ui_popup_ok_cancel_box", "NKM_UI_POPUP_FRIEND_REQUEST", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupFriendRequest.CleanupInstance)).GetInstance<NKCPopupFriendRequest>();
					NKCPopupFriendRequest.m_Instance.InitUI();
				}
				return NKCPopupFriendRequest.m_Instance;
			}
		}

		// Token: 0x17001522 RID: 5410
		// (get) Token: 0x0600817D RID: 33149 RVA: 0x002BA78F File Offset: 0x002B898F
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupFriendRequest.m_Instance != null && NKCPopupFriendRequest.m_Instance.IsOpen;
			}
		}

		// Token: 0x0600817E RID: 33150 RVA: 0x002BA7AA File Offset: 0x002B89AA
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupFriendRequest.m_Instance != null && NKCPopupFriendRequest.m_Instance.IsOpen)
			{
				NKCPopupFriendRequest.m_Instance.Close();
			}
		}

		// Token: 0x0600817F RID: 33151 RVA: 0x002BA7CF File Offset: 0x002B89CF
		private static void CleanupInstance()
		{
			NKCPopupFriendRequest.m_Instance = null;
		}

		// Token: 0x17001523 RID: 5411
		// (get) Token: 0x06008180 RID: 33152 RVA: 0x002BA7D7 File Offset: 0x002B89D7
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001524 RID: 5412
		// (get) Token: 0x06008181 RID: 33153 RVA: 0x002BA7DA File Offset: 0x002B89DA
		public override string MenuName
		{
			get
			{
				return "친구 요청";
			}
		}

		// Token: 0x06008182 RID: 33154 RVA: 0x002BA7E4 File Offset: 0x002B89E4
		private void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			this.m_slot.Init();
			this.m_btnInfo.PointerClick.RemoveAllListeners();
			this.m_btnInfo.PointerClick.AddListener(new UnityAction(this.OnFriendInfo));
			this.m_btnOk.PointerClick.RemoveAllListeners();
			this.m_btnOk.PointerClick.AddListener(new UnityAction(this.OnOk));
			NKCUtil.SetHotkey(this.m_btnOk, HotkeyEventType.Confirm);
			this.m_btnCancel.PointerClick.RemoveAllListeners();
			this.m_btnCancel.PointerClick.AddListener(new UnityAction(this.OnCancel));
		}

		// Token: 0x06008183 RID: 33155 RVA: 0x002BA8A0 File Offset: 0x002B8AA0
		public void Open(WarfareSupporterListData data, UnityAction onClose)
		{
			if (data == null)
			{
				Debug.LogError("WarfareSupporterListData is null");
				return;
			}
			this.dOnClose = onClose;
			this.m_guestFriendCode = data.commonProfile.friendCode;
			this.m_slot.SetUnitData(data.commonProfile.mainUnitId, 0, data.commonProfile.mainUnitSkinId, false, false, false, null);
			NKCUtil.SetLabelText(this.m_txtLevel, NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, new object[]
			{
				data.commonProfile.level
			});
			NKCUtil.SetLabelText(this.m_txtName, data.commonProfile.nickname);
			NKCUtil.SetLabelText(this.m_txtFCode, NKCUtilString.GetFriendCode(data.commonProfile.friendCode));
			NKCUtil.SetLabelText(this.m_txtLastConnectTime, NKCUtilString.GetLastTimeString(data.lastLoginDate));
			NKCUtil.SetLabelText(this.m_txtDesc, data.message);
			NKCUtil.SetLabelText(this.m_txtPower, data.deckData.CalculateOperationPower().ToString());
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			base.UIOpened(true);
		}

		// Token: 0x06008184 RID: 33156 RVA: 0x002BA9AA File Offset: 0x002B8BAA
		private void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x06008185 RID: 33157 RVA: 0x002BA9BF File Offset: 0x002B8BBF
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			if (this.dOnClose != null)
			{
				this.dOnClose();
				this.dOnClose = null;
			}
		}

		// Token: 0x06008186 RID: 33158 RVA: 0x002BA9E7 File Offset: 0x002B8BE7
		private void OnFriendInfo()
		{
			NKCPacketSender.Send_NKMPacket_USER_PROFILE_BY_FRIEND_CODE_REQ(this.m_guestFriendCode);
		}

		// Token: 0x06008187 RID: 33159 RVA: 0x002BA9F4 File Offset: 0x002B8BF4
		private void OnOk()
		{
			NKCPacketSender.Send_NKMPacket_FRIEND_REQUEST_REQ(this.m_guestFriendCode);
			base.Close();
		}

		// Token: 0x06008188 RID: 33160 RVA: 0x002BAA07 File Offset: 0x002B8C07
		private void OnCancel()
		{
			base.Close();
		}

		// Token: 0x04006DAA RID: 28074
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_ok_cancel_box";

		// Token: 0x04006DAB RID: 28075
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_FRIEND_REQUEST";

		// Token: 0x04006DAC RID: 28076
		private static NKCPopupFriendRequest m_Instance;

		// Token: 0x04006DAD RID: 28077
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x04006DAE RID: 28078
		public NKCUISlot m_slot;

		// Token: 0x04006DAF RID: 28079
		public Text m_txtLevel;

		// Token: 0x04006DB0 RID: 28080
		public Text m_txtName;

		// Token: 0x04006DB1 RID: 28081
		public Text m_txtFCode;

		// Token: 0x04006DB2 RID: 28082
		public Text m_txtLastConnectTime;

		// Token: 0x04006DB3 RID: 28083
		public Text m_txtPower;

		// Token: 0x04006DB4 RID: 28084
		public Text m_txtDesc;

		// Token: 0x04006DB5 RID: 28085
		[Header("Button")]
		public NKCUIComStateButton m_btnInfo;

		// Token: 0x04006DB6 RID: 28086
		public NKCUIComButton m_btnOk;

		// Token: 0x04006DB7 RID: 28087
		public NKCUIComButton m_btnCancel;

		// Token: 0x04006DB8 RID: 28088
		private UnityAction dOnClose;

		// Token: 0x04006DB9 RID: 28089
		private long m_guestFriendCode;
	}
}
