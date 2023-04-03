using System;
using ClientPacket.Account;
using Cs.Logging;
using NKM;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000918 RID: 2328
	public class NKCPopupAccountSelect : NKCUIBase
	{
		// Token: 0x17001104 RID: 4356
		// (get) Token: 0x06005D2D RID: 23853 RVA: 0x001CBDC4 File Offset: 0x001C9FC4
		public static NKCPopupAccountSelect Instance
		{
			get
			{
				if (NKCPopupAccountSelect.m_Instance == null)
				{
					NKCPopupAccountSelect.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupAccountSelect>("AB_UI_NKM_UI_ACCOUNT_LINK", "NKM_UI_POPUP_ACCOUNT_SELECT", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupAccountSelect.CleanupInstance)).GetInstance<NKCPopupAccountSelect>();
					NKCPopupAccountSelect.m_Instance.InitUI();
				}
				return NKCPopupAccountSelect.m_Instance;
			}
		}

		// Token: 0x06005D2E RID: 23854 RVA: 0x001CBE13 File Offset: 0x001CA013
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupAccountSelect.m_Instance != null && NKCPopupAccountSelect.m_Instance.IsOpen)
			{
				NKCPopupAccountSelect.m_Instance.Close();
			}
		}

		// Token: 0x06005D2F RID: 23855 RVA: 0x001CBE38 File Offset: 0x001CA038
		private static void CleanupInstance()
		{
			NKCPopupAccountSelect.m_Instance = null;
		}

		// Token: 0x17001105 RID: 4357
		// (get) Token: 0x06005D30 RID: 23856 RVA: 0x001CBE40 File Offset: 0x001CA040
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001106 RID: 4358
		// (get) Token: 0x06005D31 RID: 23857 RVA: 0x001CBE43 File Offset: 0x001CA043
		public override string MenuName
		{
			get
			{
				return "AccountLink";
			}
		}

		// Token: 0x06005D32 RID: 23858 RVA: 0x001CBE4C File Offset: 0x001CA04C
		public void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			NKCPopupAccountSelectSlot steamUserProfileSlot = this.m_steamUserProfileSlot;
			if (steamUserProfileSlot != null)
			{
				steamUserProfileSlot.InitData();
			}
			NKCPopupAccountSelectSlot mobileUserProfileSlot = this.m_mobileUserProfileSlot;
			if (mobileUserProfileSlot != null)
			{
				mobileUserProfileSlot.InitData();
			}
			NKCUIComStateButton ok = this.m_ok;
			if (ok != null)
			{
				ok.PointerClick.AddListener(new UnityAction(this.OnSelectConfirm));
			}
			NKCUIComStateButton cancel = this.m_cancel;
			if (cancel == null)
			{
				return;
			}
			cancel.PointerClick.AddListener(new UnityAction(this.OnClickClose));
		}

		// Token: 0x06005D33 RID: 23859 RVA: 0x001CBEDB File Offset: 0x001CA0DB
		public override void OnBackButton()
		{
			this.OnClickClose();
		}

		// Token: 0x06005D34 RID: 23860 RVA: 0x001CBEE3 File Offset: 0x001CA0E3
		public void OnClickClose()
		{
			Log.Debug("[SteamLink][Select] OnClickClose", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Steam/NKCPopupAccountSelect.cs", 78);
			NKCAccountLinkMgr.CheckForCancelProcess();
		}

		// Token: 0x06005D35 RID: 23861 RVA: 0x001CBEFC File Offset: 0x001CA0FC
		public void Open(NKMAccountLinkUserProfile requestUserProfile, NKMAccountLinkUserProfile targetUserProfile)
		{
			Log.Debug("[SteamLink][Select] Open", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Steam/NKCPopupAccountSelect.cs", 88);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			if (requestUserProfile.publisherType == NKM_PUBLISHER_TYPE.NPT_STEAM)
			{
				this.m_userProfileSteam = requestUserProfile;
				this.m_userProfileMobile = targetUserProfile;
			}
			else
			{
				this.m_userProfileSteam = targetUserProfile;
				this.m_userProfileMobile = requestUserProfile;
			}
			NKCPopupAccountSelectSlot steamUserProfileSlot = this.m_steamUserProfileSlot;
			if (steamUserProfileSlot != null)
			{
				steamUserProfileSlot.SetData(this.m_userProfileSteam, new UnityAction<bool>(this.OnSteamSelected));
			}
			NKCPopupAccountSelectSlot mobileUserProfileSlot = this.m_mobileUserProfileSlot;
			if (mobileUserProfileSlot != null)
			{
				mobileUserProfileSlot.SetData(this.m_userProfileMobile, new UnityAction<bool>(this.OnMobileSelected));
			}
			this.m_toggleGroup.SetAllToggleUnselected();
			if (NKCScenManager.GetScenManager().GetMyUserData().m_UserUID == requestUserProfile.commonProfile.userUid)
			{
				this.m_toggleGroup.enabled = true;
				this.m_titleText.text = NKCStringTable.GetString("SI_PF_STEAMLINK_CHOOSE_ACCOUNT_TITLE", false);
				this.m_DescText.text = NKCStringTable.GetString("SI_PF_STEAMLINK_CHOOSE_ACCOUNT_DESC", false);
				NKCUtil.SetGameobjectActive(this.m_ok, true);
				NKCUtil.SetGameobjectActive(this.m_cancel, true);
			}
			else
			{
				this.m_toggleGroup.enabled = false;
				this.m_titleText.text = NKCStringTable.GetString("SI_PF_STEAMLINK_CHOOSE_ACCOUNT_TITLE", false);
				this.m_DescText.text = NKCStringTable.GetString("SI_PF_STEAMLINK_CHOOSE_ACCOUNT_DESC_WAIT", false);
				NKCUtil.SetGameobjectActive(this.m_ok, false);
				NKCUtil.SetGameobjectActive(this.m_cancel, false);
			}
			base.UIOpened(true);
		}

		// Token: 0x06005D36 RID: 23862 RVA: 0x001CC06F File Offset: 0x001CA26F
		private void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x06005D37 RID: 23863 RVA: 0x001CC084 File Offset: 0x001CA284
		public override void CloseInternal()
		{
			Log.Debug("[SteamLink][Select] CloseInternal", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Steam/NKCPopupAccountSelect.cs", 140);
			this.m_userProfileSteam = null;
			this.m_userProfileMobile = null;
			this.m_selectedUserProfile = null;
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06005D38 RID: 23864 RVA: 0x001CC0BB File Offset: 0x001CA2BB
		public void OnSelectConfirm()
		{
			if (this.m_selectedUserProfile == null)
			{
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_STEAM_LINK_SELECT_USERPROFILE, null, "");
				return;
			}
			NKCPopupAccountSelectConfirm.Instance.Open(this.m_selectedUserProfile, new UnityAction(this.OnClickSendConfirm));
		}

		// Token: 0x06005D39 RID: 23865 RVA: 0x001CC0F2 File Offset: 0x001CA2F2
		public void OnClickSendConfirm()
		{
			NKCPopupAccountSelectConfirm.Instance.Close();
			NKCAccountLinkMgr.Send_NKMPacket_ACCOUNT_LINK_SELECT_USERDATA_REQ(this.m_selectedUserProfile.commonProfile.userUid);
		}

		// Token: 0x06005D3A RID: 23866 RVA: 0x001CC113 File Offset: 0x001CA313
		public void OnSteamSelected(bool bSelected)
		{
			this.m_selectedUserProfile = this.m_userProfileSteam;
		}

		// Token: 0x06005D3B RID: 23867 RVA: 0x001CC121 File Offset: 0x001CA321
		public void OnMobileSelected(bool bSelected)
		{
			this.m_selectedUserProfile = this.m_userProfileMobile;
		}

		// Token: 0x04004941 RID: 18753
		private const string DEBUG_HEADER = "[SteamLink][Select]";

		// Token: 0x04004942 RID: 18754
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_ACCOUNT_LINK";

		// Token: 0x04004943 RID: 18755
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_ACCOUNT_SELECT";

		// Token: 0x04004944 RID: 18756
		private static NKCPopupAccountSelect m_Instance;

		// Token: 0x04004945 RID: 18757
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x04004946 RID: 18758
		public NKCUIComStateButton m_ok;

		// Token: 0x04004947 RID: 18759
		public NKCUIComStateButton m_cancel;

		// Token: 0x04004948 RID: 18760
		public NKCUIComToggleGroup m_toggleGroup;

		// Token: 0x04004949 RID: 18761
		public Text m_titleText;

		// Token: 0x0400494A RID: 18762
		public Text m_DescText;

		// Token: 0x0400494B RID: 18763
		public NKCPopupAccountSelectSlot m_steamUserProfileSlot;

		// Token: 0x0400494C RID: 18764
		public NKCPopupAccountSelectSlot m_mobileUserProfileSlot;

		// Token: 0x0400494D RID: 18765
		private NKMAccountLinkUserProfile m_userProfileSteam;

		// Token: 0x0400494E RID: 18766
		private NKMAccountLinkUserProfile m_userProfileMobile;

		// Token: 0x0400494F RID: 18767
		private NKMAccountLinkUserProfile m_selectedUserProfile;
	}
}
