using System;
using Cs.Logging;
using NKC.PacketHandler;
using NKC.Publisher;
using NKC.UI.Guide;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Option
{
	// Token: 0x02000B89 RID: 2953
	public class NKCUIGameOptionAccount : NKCUIGameOptionContentBase
	{
		// Token: 0x170015ED RID: 5613
		// (get) Token: 0x06008847 RID: 34887 RVA: 0x002E18BD File Offset: 0x002DFABD
		private string CONNECTED_STRING
		{
			get
			{
				return NKCUtilString.GET_STRING_OPTION_CONNECTED;
			}
		}

		// Token: 0x170015EE RID: 5614
		// (get) Token: 0x06008848 RID: 34888 RVA: 0x002E18C4 File Offset: 0x002DFAC4
		private string DISCONNECTED_STRING
		{
			get
			{
				return NKCUtilString.GET_STRING_OPTION_DISCONNECTED;
			}
		}

		// Token: 0x170015EF RID: 5615
		// (get) Token: 0x06008849 RID: 34889 RVA: 0x002E18CB File Offset: 0x002DFACB
		private string LOGOUT_WARNING_TITLE_STRING
		{
			get
			{
				return NKCUtilString.GET_STRING_WARNING;
			}
		}

		// Token: 0x170015F0 RID: 5616
		// (get) Token: 0x0600884A RID: 34890 RVA: 0x002E18D2 File Offset: 0x002DFAD2
		private string LOGOUT_USABLE_CONTENT_STRING
		{
			get
			{
				return NKCUtilString.GET_STRING_OPTION_LOGOUT_REQ;
			}
		}

		// Token: 0x170015F1 RID: 5617
		// (get) Token: 0x0600884B RID: 34891 RVA: 0x002E18D9 File Offset: 0x002DFAD9
		private string LOGOUT_UNUSABLE_CONTENT_STRING
		{
			get
			{
				return NKCUtilString.GET_STRING_OPTION_CANNOT_LOG_OUT_WHEN_IN_GAME_BATTLE;
			}
		}

		// Token: 0x0600884C RID: 34892 RVA: 0x002E18E0 File Offset: 0x002DFAE0
		public override void Init()
		{
			NKCUtil.SetGameobjectActive(this.m_FACEBOOK, false);
			NKCUtil.SetGameobjectActive(this.m_GOOGLE, false);
			NKCUtil.SetGameobjectActive(this.m_NEXON, false);
			NKCUtil.SetGameobjectActive(this.m_APPLE, false);
			NKCUtil.SetGameobjectActive(this.m_NAVER, false);
			NKCUtil.SetGameobjectActive(this.m_TWITTER, false);
			NKCUtil.SetGameobjectActive(this.m_LINE, false);
			NKCUtil.SetGameobjectActive(this.m_STEAM, false);
			NKCUtil.SetBindFunction(this.m_NKM_UI_GAME_OPTION_GAME_GRADE_CHECK, new UnityAction(this.OnClickGameGradeCheck));
			NKCUtil.SetBindFunction(this.m_NKM_UI_GAME_OPTION_COPY, new UnityAction(this.OnClickAccountCodeCopy));
			NKCUtil.SetBindFunction(this.m_NKM_UI_GAME_OPTION_OFFICIAL_ANNOUNCEMENT, new UnityAction(this.OnClickAnnouncement));
			NKCUtil.SetBindFunction(this.m_NKM_UI_GAME_OPTION_CUSTOMER_SERVICE_CENTER, new UnityAction(this.OnClickServiceCenter));
			NKCUtil.SetBindFunction(this.m_NKM_UI_GAME_OPTION_PURCHASE_RECOVERY, new UnityAction(this.OnClickBillingRestre));
			NKCUtil.SetBindFunction(this.m_NKM_UI_GAME_OPTION_LOGOUT, new UnityAction(this.OnClickLogoutButton));
			NKCUtil.SetBindFunction(this.m_NKM_UI_GAME_OPTION_ACCOUNT_CONNECTION, new UnityAction(this.OnClickSyncAccount));
			NKCUtil.SetBindFunction(this.m_NKM_UI_GAME_OPTION_GUIDE, new UnityAction(this.OnClickGuide));
			NKCUtil.SetBindFunction(this.m_NKM_UI_GAME_OPTION_COMMUNITY, new UnityAction(this.OnClickCommunity));
			if (NKCDefineManager.DEFINE_NXTOY())
			{
				NKCUtil.SetBindFunction(this.m_NKM_UI_GAME_OPTION_MEMBER_WITHDRAWAL, new UnityAction(this.OnClickWithdrawalNexon));
				NKCUtil.SetBindFunction(this.m_NKM_UI_GAME_OPTION_ACCOUNT_RESET, new UnityAction(this.OnClickWithdrawal));
			}
			else
			{
				if (!NKCDefineManager.DEFINE_SELECT_SERVER())
				{
					NKCUtil.SetBindFunction(this.m_NKM_UI_GAME_OPTION_MEMBER_WITHDRAWAL, new UnityAction(this.OnClickWithdrawal));
				}
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_ACCOUNT_RESET, false);
			}
			if (NKCDefineManager.DEFINE_SELECT_SERVER())
			{
				NKCUtil.SetBindFunction(this.m_NKM_UI_GAME_OPTION_SERVER_INITIALIZATION, new UnityAction(this.OnClickInitServer));
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_SERVER_INITIALIZATION, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_MEMBER_WITHDRAWAL, false);
				if (NKCConnectionInfo.GetLoginServerCount() > 1)
				{
					NKCUtil.SetBindFunction(this.m_NKM_UI_GAME_OPTION_SELECT_SERVER, new UnityAction(this.OnClickSelectServer));
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_SELECT_SERVER, true);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_SELECT_SERVER, false);
				}
			}
			if (NKCPublisherModule.Marketing.IsCouponEnabled())
			{
				NKCUtil.SetGameobjectActive(this.m_objNKM_UI_GAME_OPTION_ACCOUNT_COUPON, true);
				if (NKCPublisherModule.Marketing.IsUseSelfCouponPopup())
				{
					NKCUtil.SetBindFunction(this.m_NKM_UI_GAME_OPTION_ACCOUNT_COUPON, new UnityAction(NKCPublisherModule.Marketing.OpenCoupon));
				}
				else
				{
					NKCUtil.SetBindFunction(this.m_NKM_UI_GAME_OPTION_ACCOUNT_COUPON, new UnityAction(this.OnClickCouponPopup));
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objNKM_UI_GAME_OPTION_ACCOUNT_COUPON, false);
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_CUSTOMER_SERVICE_CENTER, NKCPublisherModule.Notice.IsActiveCustomerCenter());
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_GAME_GRADE_CHECK, NKCPublisherModule.ShowGameOptionGradeCheck());
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_ACCOUNT_LINK, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_ACCOUNT_LINK_INPUT, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_ACCOUNT_LINK_REWARD, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_IMPORTANT_NOTICE, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_IMPORTANT_NOTICE_REFUSE, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_BUG_REPORT, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_RESOURCE_DIVISION, false);
			if (NKCPublisherModule.InAppPurchase.ShowCashResourceState())
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_RESOURCE_DIVISION, true);
				NKCUtil.SetBindFunction(this.m_NKM_UI_GAME_OPTION_RESOURCE_DIVISION, new UnityAction(this.OnClickResourceDivision));
			}
			if (NKMContentsVersionManager.HasCountryTag(CountryTagType.TWN) || NKMContentsVersionManager.HasCountryTag(CountryTagType.CHN) || NKMContentsVersionManager.HasCountryTag(CountryTagType.SEA))
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_MEMBER_WITHDRAWAL, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_ACCOUNT_CONNECTION, false);
				NKCUtil.SetBindFunction(this.m_NKM_UI_GAME_OPTION_NOTICE, new UnityAction(this.OnClickCommunity));
				NKCUtil.SetBindFunction(this.m_NKM_UI_GAME_OPTION_QnA, new UnityAction(this.OnClickQnA));
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_COMMUNITY, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_NOTICE, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_QnA, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID_CONNECTED.gameObject, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_COPY.gameObject, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID, false);
				NKCUtil.SetGameobjectActive(this.m_objNKM_UI_GAME_OPTION_GUEST_ACCOUNT_TRANSFER, false);
				return;
			}
			if (NKCPublisherModule.IsNexonPCBuild())
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_MEMBER_WITHDRAWAL, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_ACCOUNT_CONNECTION, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_NOTICE, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_QnA, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID_CONNECTED.gameObject, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_COPY.gameObject, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID, true);
				NKCUtil.SetGameobjectActive(this.m_objNKM_UI_GAME_OPTION_GUEST_ACCOUNT_TRANSFER, false);
				NKCUtil.SetGameobjectActive(this.m_objNKM_UI_GAME_OPTION_LOGOUT, false);
				NKCUtil.SetGameobjectActive(this.m_objNKM_UI_GAME_OPTION_LOGOUT_dummy, false);
				return;
			}
			if (NKCPublisherModule.IsSteamPC())
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_MEMBER_WITHDRAWAL, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_ACCOUNT_CONNECTION, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_NOTICE, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_QnA, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID_CONNECTED.gameObject, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_COPY.gameObject, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID, true);
				NKCUtil.SetGameobjectActive(this.m_objNKM_UI_GAME_OPTION_GUEST_ACCOUNT_TRANSFER, false);
				NKCUtil.SetGameobjectActive(this.m_objNKM_UI_GAME_OPTION_LOGOUT, false);
				NKCUtil.SetGameobjectActive(this.m_objNKM_UI_GAME_OPTION_LOGOUT_dummy, false);
				return;
			}
			if (NKCPublisherModule.IsGamebasePublished())
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_NOTICE, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_QnA, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID_CONNECTED.gameObject, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_COPY.gameObject, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID, true);
				NKCUtil.SetGameobjectActive(this.m_objNKM_UI_GAME_OPTION_GUEST_ACCOUNT_TRANSFER, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_NOTICE, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_QnA, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID_CONNECTED.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_COPY.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID, true);
			NKCUtil.SetGameobjectActive(this.m_objNKM_UI_GAME_OPTION_GUEST_ACCOUNT_TRANSFER, true);
			if (NKCDefineManager.DEFINE_NXTOY_JP())
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_MEMBER_WITHDRAWAL, false);
			}
		}

		// Token: 0x0600884D RID: 34893 RVA: 0x002E1E94 File Offset: 0x002E0094
		private void OnClickCouponPopup()
		{
			NKCPopupCoupon.Instance.Open(new NKCPopupCoupon.OnClickOK(this.OnClickCouponPopupOK));
		}

		// Token: 0x0600884E RID: 34894 RVA: 0x002E1EAC File Offset: 0x002E00AC
		private void OnClickCouponPopupOK(string code)
		{
			NKCPublisherModule.Marketing.SendUseCouponReqToCSServer(code);
		}

		// Token: 0x0600884F RID: 34895 RVA: 0x002E1EB9 File Offset: 0x002E00B9
		private void SetContentForNxToy(NKMUserData userData)
		{
		}

		// Token: 0x06008850 RID: 34896 RVA: 0x002E1EBC File Offset: 0x002E00BC
		private void SetContentForNexonPC(NKMUserData userData)
		{
			NKCUtil.SetLabelText(this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID_INFO, NKCPublisherModule.Auth.GetPublisherAccountCode());
			if (string.IsNullOrWhiteSpace(NKCPublisherModule.Auth.GetPublisherAccountCode()))
			{
				this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID_CONNECTED.text = this.DISCONNECTED_STRING;
				this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID_CONNECTED.color = this.cDisonnectedColor;
			}
			else
			{
				this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID_CONNECTED.text = this.CONNECTED_STRING;
				this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID_CONNECTED.color = Color.yellow;
			}
			this.m_NKM_UI_GAME_OPTION_MEMBER_WITHDRAWAL.enabled = true;
			this.m_NKM_UI_GAME_OPTION_MEMBER_WITHDRAWAL.UnLock(false);
			this.m_NKM_UI_GAME_OPTION_ACCOUNT_CONNECTION.Lock(false);
		}

		// Token: 0x06008851 RID: 34897 RVA: 0x002E1F58 File Offset: 0x002E0158
		private void SetContentForSteamPC(NKMUserData userData)
		{
			NKCUtil.SetLabelText(this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID_INFO, NKCPublisherModule.Auth.GetPublisherAccountCode());
			if (string.IsNullOrWhiteSpace(NKCPublisherModule.Auth.GetPublisherAccountCode()))
			{
				this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID_CONNECTED.text = this.DISCONNECTED_STRING;
				this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID_CONNECTED.color = this.cDisonnectedColor;
			}
			else
			{
				this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID_CONNECTED.text = this.CONNECTED_STRING;
				this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID_CONNECTED.color = Color.yellow;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_MEMBER_WITHDRAWAL, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_ACCOUNT_CONNECTION, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_GUEST_ACCOUNT_TRANSFER, false);
			this.m_NKM_UI_GAME_OPTION_ACCOUNT_CONNECTION.Lock(false);
			NKCUtil.SetGameobjectActive(this.m_STEAM, true);
		}

		// Token: 0x06008852 RID: 34898 RVA: 0x002E200C File Offset: 0x002E020C
		private void SetContentForGamebase(NKMUserData userData)
		{
			if (NKCPublisherModule.Auth.IsGuest())
			{
				this.m_NKM_UI_GAME_OPTION_ACCOUNT_CONNECTION.UnLock(false);
				this.m_NKM_UI_GAME_OPTION_MEMBER_WITHDRAWAL.enabled = true;
				this.m_NKM_UI_GAME_OPTION_MEMBER_WITHDRAWAL.UnLock(false);
				NKCUtil.SetLabelText(this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID_CONNECTED, NKCUtilString.GET_STRING_TOY_LOGGED_IN_GUEST_KOR);
				NKCUtil.SetLabelTextColor(this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID_CONNECTED, Color.white);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NEXON, false);
				NKCUtil.SetGameobjectActive(this.m_NAVER, false);
				NKCUtil.SetGameobjectActive(this.m_FACEBOOK, NKCPublisherModule.Auth.GetLoginIdpType() == NKCPublisherModule.NKCPMAuthentication.LOGIN_IDP_TYPE.facebook);
				NKCUtil.SetGameobjectActive(this.m_GOOGLE, NKCPublisherModule.Auth.GetLoginIdpType() == NKCPublisherModule.NKCPMAuthentication.LOGIN_IDP_TYPE.google);
				NKCUtil.SetGameobjectActive(this.m_APPLE, NKCPublisherModule.Auth.GetLoginIdpType() == NKCPublisherModule.NKCPMAuthentication.LOGIN_IDP_TYPE.appleid);
				NKCUtil.SetGameobjectActive(this.m_TWITTER, NKCPublisherModule.Auth.GetLoginIdpType() == NKCPublisherModule.NKCPMAuthentication.LOGIN_IDP_TYPE.twitter);
				this.m_NKM_UI_GAME_OPTION_ACCOUNT_CONNECTION.Lock(false);
				this.m_NKM_UI_GAME_OPTION_MEMBER_WITHDRAWAL.UnLock(false);
			}
			if (string.IsNullOrWhiteSpace(NKCPublisherModule.Auth.GetPublisherAccountCode()))
			{
				this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID_CONNECTED.text = this.DISCONNECTED_STRING;
				this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID_CONNECTED.color = this.cDisonnectedColor;
				return;
			}
			this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID_INFO.text = NKCPublisherModule.Auth.GetPublisherAccountCode();
			this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID_CONNECTED.text = this.CONNECTED_STRING;
			this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID_CONNECTED.color = Color.yellow;
		}

		// Token: 0x06008853 RID: 34899 RVA: 0x002E2168 File Offset: 0x002E0368
		public override void SetContent()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				this.m_NKM_UI_GAME_OPTION_ACCOUNT_ID_INFO.text = myUserData.m_FriendCode.ToString();
				this.m_NKM_UI_GAME_OPTION_ACCOUNT_NICKNAME_INFO.text = myUserData.m_UserNickName.ToString();
				this.m_NKM_UI_GAME_OPTION_MEMBER_WITHDRAWAL.enabled = true;
				this.m_NKM_UI_GAME_OPTION_MEMBER_WITHDRAWAL.UnLock(false);
				this.m_NKM_UI_GAME_OPTION_ACCOUNT_CONNECTION.Lock(false);
				this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID_INFO.text = "";
				this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID_CONNECTED.text = this.DISCONNECTED_STRING;
				this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID_CONNECTED.color = this.cDisonnectedColor;
				if (NKCDefineManager.DEFINE_NXTOY())
				{
					this.SetContentForNxToy(myUserData);
				}
				else
				{
					if (NKCPublisherModule.IsNexonPCBuild())
					{
						this.SetContentForNexonPC(myUserData);
					}
					if (NKCPublisherModule.IsGamebasePublished())
					{
						this.SetContentForGamebase(myUserData);
					}
					if (NKCPublisherModule.IsSteamPC())
					{
						this.SetContentForSteamPC(myUserData);
					}
				}
			}
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.STEAM_ACCOUNT_LINK))
			{
				bool flag = NKCAccountLinkMgr.m_requestUserProfile != null;
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_ACCOUNT_LINK, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_ACCOUNT_LINK_INPUT, true);
				NKCUtil.SetBindFunction(this.m_NKM_UI_GAME_OPTION_ACCOUNT_LINK_INPUT, new UnityAction(this.OnClickAccountLinkCodeInput));
				if (NKCPublisherModule.Auth.IsGuest())
				{
					NKCUtil.SetBindFunction(this.m_NKM_UI_GAME_OPTION_ACCOUNT_LINK, delegate()
					{
						NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_STEAM_LINK_GUEST_ACCOUNT, null, "");
					});
				}
				else
				{
					NKCUtil.SetBindFunction(this.m_NKM_UI_GAME_OPTION_ACCOUNT_LINK, new UnityAction(this.OnClickAccountLink));
				}
				Log.Debug("[SteamLink][Option] HasLinkRequest[" + flag.ToString() + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/GameOption/NKCUIGameOptionAccount.cs", 471);
				if (this.m_NKM_UI_GAME_OPTION_ACCOUNT_LINK != null && this.m_NKM_UI_GAME_OPTION_ACCOUNT_LINK_INPUT != null)
				{
					if (flag)
					{
						this.m_NKM_UI_GAME_OPTION_ACCOUNT_LINK.Lock(false);
						this.m_NKM_UI_GAME_OPTION_ACCOUNT_LINK_INPUT.UnLock(false);
					}
					else
					{
						this.m_NKM_UI_GAME_OPTION_ACCOUNT_LINK.UnLock(false);
						this.m_NKM_UI_GAME_OPTION_ACCOUNT_LINK_INPUT.Lock(false);
					}
					NKCUtil.SetLabelText(this.m_NKM_UI_GAME_OPTION_ACCOUNT_LINK_TEXT, NKCPublisherModule.Auth.GetAccountLinkText());
					NKCUtil.SetLabelText(this.m_NKM_UI_GAME_OPTION_ACCOUNT_LINK_TEXT_LOCK, NKCPublisherModule.Auth.GetAccountLinkText());
					NKCUtil.SetLabelText(this.m_NKM_UI_GAME_OPTION_ACCOUNT_LINK_INPUT_TEXT, NKCStringTable.GetString("SI_PF_STEAMLINK_OPEN_CODE_INPUT", false));
					NKCUtil.SetLabelText(this.m_NKM_UI_GAME_OPTION_ACCOUNT_LINK_INPUT_TEXT_LOCK, NKCStringTable.GetString("SI_PF_STEAMLINK_OPEN_CODE_INPUT", false));
				}
				Log.Debug(string.Format("[SteamLink][Option] UserData - enableAccountLink[{0}]", myUserData.m_enableAccountLink), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/GameOption/NKCUIGameOptionAccount.cs", 491);
				if (myUserData.m_enableAccountLink)
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_ACCOUNT_LINK_REWARD, false);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_ACCOUNT_LINK, false);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_ACCOUNT_LINK_INPUT, false);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_ACCOUNT_LINK_REWARD, false);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_ACCOUNT_LINK_REWARD, true);
				}
			}
			if (NKCReportManager.IsReportOpened())
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_BUG_REPORT, true);
				NKCUtil.SetBindFunction(this.m_NKM_UI_GAME_OPTION_BUG_REPORT, new UnityAction(this.OnClickBugReport));
			}
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.SERVICE_TRANSFER_REGIST))
			{
				NKCUtil.SetLabelText(this.m_NKM_UI_GAME_OPTION_IMPORTANT_NOTICE_TEXT, NKCStringTable.GetString("SI_PF_SERVICE_TRANSFER_REGIST_NOTICE_TITLE", false));
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_IMPORTANT_NOTICE, true);
				NKCUtil.SetBindFunction(this.m_NKM_UI_GAME_OPTION_IMPORTANT_NOTICE, new UnityAction(this.OnClickServiceTransferRegist));
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_IMPORTANT_NOTICE_REFUSE, true);
				NKCUtil.SetBindFunction(this.m_NKM_UI_GAME_OPTION_IMPORTANT_NOTICE_REFUSE, new UnityAction(this.OnClickServiceCenter));
				return;
			}
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.SERVICE_TRANSFER))
			{
				NKCUtil.SetLabelText(this.m_NKM_UI_GAME_OPTION_IMPORTANT_NOTICE_TEXT, NKCStringTable.GetString("SI_PF_SERVICE_TRANSFER", false));
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAME_OPTION_IMPORTANT_NOTICE, true);
				NKCUtil.SetBindFunction(this.m_NKM_UI_GAME_OPTION_IMPORTANT_NOTICE, new UnityAction(this.OnClickServiceTransfer));
			}
		}

		// Token: 0x06008854 RID: 34900 RVA: 0x002E24E4 File Offset: 0x002E06E4
		private void OnClickLogoutButton()
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME)
			{
				NKCPopupOKCancel.OpenOKBox(this.LOGOUT_WARNING_TITLE_STRING, this.LOGOUT_UNUSABLE_CONTENT_STRING, null, "");
				return;
			}
			NKCPopupOKCancel.OpenOKCancelBox(this.LOGOUT_WARNING_TITLE_STRING, this.LOGOUT_USABLE_CONTENT_STRING, new NKCPopupOKCancel.OnButton(this.OnClickLogoutOKButton), null, false);
		}

		// Token: 0x06008855 RID: 34901 RVA: 0x002E2535 File Offset: 0x002E0735
		private void OnClickGuestAccountTransferButton()
		{
		}

		// Token: 0x06008856 RID: 34902 RVA: 0x002E2537 File Offset: 0x002E0737
		private void OnClickAccountCodeCopy()
		{
			if (this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID_INFO != null)
			{
				GUIUtility.systemCopyBuffer = this.m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID_INFO.text;
			}
		}

		// Token: 0x06008857 RID: 34903 RVA: 0x002E2557 File Offset: 0x002E0757
		private void OnClickAnnouncement()
		{
			NKCUIGameOption.Instance.Close();
			NKCUINews.Instance.SetDataAndOpen(true, eNewsFilterType.NOTICE, -1);
		}

		// Token: 0x06008858 RID: 34904 RVA: 0x002E2570 File Offset: 0x002E0770
		private void OnClickBillingRestre()
		{
			NKCPublisherModule.InAppPurchase.BillingRestore(new NKCPublisherModule.OnComplete(NKCShopManager.OnBillingRestore));
		}

		// Token: 0x06008859 RID: 34905 RVA: 0x002E2588 File Offset: 0x002E0788
		private void OnClickServiceCenter()
		{
			NKCPublisherModule.Notice.OpenCustomerCenter(new NKCPublisherModule.OnComplete(this.OnCustomerCenter));
		}

		// Token: 0x0600885A RID: 34906 RVA: 0x002E25A0 File Offset: 0x002E07A0
		private void OnCustomerCenter(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
		{
			if (resultCode == NKC_PUBLISHER_RESULT_CODE.NPRC_OK)
			{
				return;
			}
			switch (resultCode)
			{
			case NKC_PUBLISHER_RESULT_CODE.NPRC_ACCOUNT_DATA_RESTORE_SUCCESS:
			case NKC_PUBLISHER_RESULT_CODE.NPRC_ACCOUNT_USER_QUIT_SUCCESS:
				NKCPacketHandlersLobby.MoveToLogin();
				return;
			case NKC_PUBLISHER_RESULT_CODE.NPRC_ACCOUNT_DATA_RESTORE_FAIL:
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_TOY_GUEST_ACCOUT_RESTORE_FAIL + "\n" + NKCPublisherModule.LastError, null, "");
				return;
			default:
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString(resultCode.ToString(), false), null, "");
				return;
			}
		}

		// Token: 0x0600885B RID: 34907 RVA: 0x002E2619 File Offset: 0x002E0819
		private void OnClickQnA()
		{
			NKCPublisherModule.Notice.OpenQnA(null);
		}

		// Token: 0x0600885C RID: 34908 RVA: 0x002E2628 File Offset: 0x002E0828
		private void OnClickWithdrawal()
		{
			if (!NKCDefineManager.DEFINE_SB_GB())
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_OPTION_DROPOUT_WARNING, delegate()
				{
					NKCUIAccountWithdrawCheckPopup.Instance.OpenUI(false);
				}, delegate()
				{
				}, false);
				return;
			}
			if (NKCPublisherModule.Auth.IsGuest())
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_OPTION_DROPOUT_WARNING_INSTANT, delegate()
				{
					NKCUIAccountWithdrawCheckPopup.Instance.OpenUI(false);
				}, null, false);
				return;
			}
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_OPTION_DROPOUT_WARNING, delegate()
			{
				NKCUIAccountWithdrawCheckPopup.Instance.OpenUI(false);
			}, null, false);
		}

		// Token: 0x0600885D RID: 34909 RVA: 0x002E26F8 File Offset: 0x002E08F8
		private void OnClickWithdrawalNexon()
		{
		}

		// Token: 0x0600885E RID: 34910 RVA: 0x002E26FA File Offset: 0x002E08FA
		private void OnClickSyncAccount()
		{
			NKCPublisherModule.Auth.ChangeAccount(new NKCPublisherModule.OnComplete(this.AfterChangeAccount), true);
		}

		// Token: 0x0600885F RID: 34911 RVA: 0x002E2714 File Offset: 0x002E0914
		private void AfterChangeAccount(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
		{
			if (resultCode != NKC_PUBLISHER_RESULT_CODE.NPRC_OK)
			{
				switch (resultCode)
				{
				case NKC_PUBLISHER_RESULT_CODE.NPRC_AUTH_CHANGEACCOUNT_SUCCESS_ACCOUNT_CHANGED:
					NKCPacketHandlersLobby.MoveToLogin();
					return;
				case NKC_PUBLISHER_RESULT_CODE.NPRC_AUTH_CHANGEACCOUNT_SUCCESS_GUEST_SYNC:
					goto IL_34;
				case NKC_PUBLISHER_RESULT_CODE.NPRC_AUTH_CHANGEACCOUNT_FAIL_GUEST_ALREADY_MAPPED:
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_CHANGEACCOUNT_FAIL_GUEST_ALREADY_MAPPED, null, "");
					return;
				case NKC_PUBLISHER_RESULT_CODE.NPRC_AUTH_CHANGEACCOUNT_SUCCESS_QUIT:
				case NKC_PUBLISHER_RESULT_CODE.NPRC_AUTH_CHANGEACCOUNT_FAIL_USER_CANCEL:
				case NKC_PUBLISHER_RESULT_CODE.NPRC_AUTH_CHANGEACCOUNT_FAIL_QUIT:
				case NKC_PUBLISHER_RESULT_CODE.NPRC_AUTH_CHANGEACCOUNT_FAIL:
					break;
				case NKC_PUBLISHER_RESULT_CODE.NPRC_AUTH_CHANGEACCOUNT_FAIL_NO_CHANGEABLE:
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_TOY_SYNC_ACCOUNT_FAIL, null, "");
					return;
				case NKC_PUBLISHER_RESULT_CODE.NPRC_AUTH_CHANGEACCOUNT_SUCCESS_GUEST_SYNC_RELOGIN:
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString("SI_DP_CHANGEACCOUNT_SUCCESS_GUEST_SYNC_LOGOUT", false), delegate()
					{
						this.OnClickLogoutOKButton();
					}, "");
					break;
				default:
					return;
				}
				return;
			}
			IL_34:
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_CHANGEACCOUNT_SUCCESS_GUEST_SYNC, null, "");
		}

		// Token: 0x06008860 RID: 34912 RVA: 0x002E27C3 File Offset: 0x002E09C3
		private void OnClickLogoutOKButton()
		{
			NKMPopUpBox.OpenWaitBox(0f, "");
			NKCPublisherModule.Auth.Logout(new NKCPublisherModule.OnComplete(this.OnLogoutComplete));
		}

		// Token: 0x06008861 RID: 34913 RVA: 0x002E27EA File Offset: 0x002E09EA
		private void OnLogoutComplete(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
		{
			if (!NKCPublisherModule.CheckError(resultCode, additionalError, true, null, false))
			{
				return;
			}
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_TOY_LOGOUT_SUCCESS, new NKCPopupOKCancel.OnButton(NKCPacketHandlersLobby.MoveToLogin), "");
		}

		// Token: 0x06008862 RID: 34914 RVA: 0x002E2819 File Offset: 0x002E0A19
		private void OnClickGuide()
		{
			NKCUIPopUpGuide.Instance.Open("", 0);
		}

		// Token: 0x06008863 RID: 34915 RVA: 0x002E282B File Offset: 0x002E0A2B
		private void OnClickGameGradeCheck()
		{
			NKCPopupGameGradeCheck.Instance.Open();
		}

		// Token: 0x06008864 RID: 34916 RVA: 0x002E2837 File Offset: 0x002E0A37
		private void OnClickCommunity()
		{
			NKCPublisherModule.Notice.OpenCommunity(null);
		}

		// Token: 0x06008865 RID: 34917 RVA: 0x002E2844 File Offset: 0x002E0A44
		private void OnClickResourceDivision()
		{
			NKCPopupResourceDivision.Instance.Open();
		}

		// Token: 0x06008866 RID: 34918 RVA: 0x002E2850 File Offset: 0x002E0A50
		private void OnClickAccountLink()
		{
			NKCAccountLinkMgr.StartLinkProcess();
		}

		// Token: 0x06008867 RID: 34919 RVA: 0x002E2857 File Offset: 0x002E0A57
		private void OnClickAccountLinkCodeInput()
		{
			NKCAccountLinkMgr.OpenPrivateLinkCodeInput();
		}

		// Token: 0x06008868 RID: 34920 RVA: 0x002E285E File Offset: 0x002E0A5E
		private void OnClickBugReport()
		{
			if (!NKCReportManager.IsReportOpened())
			{
				return;
			}
			NKCPopupBugReport.Instance.Open();
		}

		// Token: 0x06008869 RID: 34921 RVA: 0x002E2872 File Offset: 0x002E0A72
		private void OnClickServiceTransfer()
		{
			NKCServiceTransferMgr.StartServiceTransferProcess();
		}

		// Token: 0x0600886A RID: 34922 RVA: 0x002E2879 File Offset: 0x002E0A79
		private void OnClickServiceTransferRegist()
		{
			NKCServiceTransferMgr.StartServiceTransferRegistProcess();
		}

		// Token: 0x0600886B RID: 34923 RVA: 0x002E2880 File Offset: 0x002E0A80
		private void OnClickInitServer()
		{
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_WARNING, NKCStringTable.GetString("SI_PF_POPUP_SERVER_INITIALIZATION_WARNING", false), delegate()
			{
				NKCUIAccountWithdrawCheckPopup.Instance.OpenUI(true);
			}, null, false);
		}

		// Token: 0x0600886C RID: 34924 RVA: 0x002E28B8 File Offset: 0x002E0AB8
		private void OnClickSelectServer()
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_WARNING, NKCStringTable.GetString("SI_DP_OPTION_CANNOT_CHANGE_SERVER_WHEN_IN_GAME_BATTLE", false), null, "");
				return;
			}
			NKCPopupSelectServer.Instance.Open(true, true, null);
		}

		// Token: 0x040074B8 RID: 29880
		private readonly Color cDisonnectedColor = Color.red;

		// Token: 0x040074B9 RID: 29881
		public Text m_NKM_UI_GAME_OPTION_ACCOUNT_ID_INFO;

		// Token: 0x040074BA RID: 29882
		public Text m_NKM_UI_GAME_OPTION_ACCOUNT_NICKNAME_INFO;

		// Token: 0x040074BB RID: 29883
		public Text m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID_INFO;

		// Token: 0x040074BC RID: 29884
		public Text m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID_CONNECTED;

		// Token: 0x040074BD RID: 29885
		public GameObject m_NKM_UI_GAME_OPTION_ACCOUNT_ACCOUNTID;

		// Token: 0x040074BE RID: 29886
		public NKCUIComStateButton m_NKM_UI_GAME_OPTION_COPY;

		// Token: 0x040074BF RID: 29887
		public NKCUIComStateButton m_NKM_UI_GAME_OPTION_OFFICIAL_ANNOUNCEMENT;

		// Token: 0x040074C0 RID: 29888
		public NKCUIComStateButton m_NKM_UI_GAME_OPTION_CUSTOMER_SERVICE_CENTER;

		// Token: 0x040074C1 RID: 29889
		public NKCUIComStateButton m_NKM_UI_GAME_OPTION_PURCHASE_RECOVERY;

		// Token: 0x040074C2 RID: 29890
		public NKCUIComStateButton m_NKM_UI_GAME_OPTION_LOGOUT;

		// Token: 0x040074C3 RID: 29891
		public NKCUIComStateButton m_NKM_UI_GAME_OPTION_GUEST_ACCOUNT_TRANSFER;

		// Token: 0x040074C4 RID: 29892
		public NKCUIComStateButton m_NKM_UI_GAME_OPTION_ACCOUNT_RESET;

		// Token: 0x040074C5 RID: 29893
		public NKCUIComStateButton m_NKM_UI_GAME_OPTION_ACCOUNT_CONNECTION;

		// Token: 0x040074C6 RID: 29894
		public NKCUIComStateButton m_NKM_UI_GAME_OPTION_MEMBER_WITHDRAWAL;

		// Token: 0x040074C7 RID: 29895
		public NKCUIComStateButton m_NKM_UI_GAME_OPTION_SERVER_INITIALIZATION;

		// Token: 0x040074C8 RID: 29896
		public NKCUIComStateButton m_NKM_UI_GAME_OPTION_GUIDE;

		// Token: 0x040074C9 RID: 29897
		public NKCUIComStateButton m_NKM_UI_GAME_OPTION_COMMUNITY;

		// Token: 0x040074CA RID: 29898
		public NKCUIComStateButton m_NKM_UI_GAME_OPTION_NOTICE;

		// Token: 0x040074CB RID: 29899
		public NKCUIComStateButton m_NKM_UI_GAME_OPTION_QnA;

		// Token: 0x040074CC RID: 29900
		public NKCUIComStateButton m_NKM_UI_GAME_OPTION_GAME_GRADE_CHECK;

		// Token: 0x040074CD RID: 29901
		public NKCUIComStateButton m_NKM_UI_GAME_OPTION_RESOURCE_DIVISION;

		// Token: 0x040074CE RID: 29902
		public NKCUIComStateButton m_NKM_UI_GAME_OPTION_ACCOUNT_LINK;

		// Token: 0x040074CF RID: 29903
		public NKCUIComStateButton m_NKM_UI_GAME_OPTION_ACCOUNT_LINK_INPUT;

		// Token: 0x040074D0 RID: 29904
		public Text m_NKM_UI_GAME_OPTION_ACCOUNT_LINK_TEXT;

		// Token: 0x040074D1 RID: 29905
		public Text m_NKM_UI_GAME_OPTION_ACCOUNT_LINK_TEXT_LOCK;

		// Token: 0x040074D2 RID: 29906
		public Text m_NKM_UI_GAME_OPTION_ACCOUNT_LINK_INPUT_TEXT;

		// Token: 0x040074D3 RID: 29907
		public Text m_NKM_UI_GAME_OPTION_ACCOUNT_LINK_INPUT_TEXT_LOCK;

		// Token: 0x040074D4 RID: 29908
		public GameObject m_NKM_UI_GAME_OPTION_ACCOUNT_LINK_REWARD;

		// Token: 0x040074D5 RID: 29909
		public NKCUIComStateButton m_NKM_UI_GAME_OPTION_IMPORTANT_NOTICE;

		// Token: 0x040074D6 RID: 29910
		public NKCUIComStateButton m_NKM_UI_GAME_OPTION_IMPORTANT_NOTICE_REFUSE;

		// Token: 0x040074D7 RID: 29911
		public Text m_NKM_UI_GAME_OPTION_IMPORTANT_NOTICE_TEXT;

		// Token: 0x040074D8 RID: 29912
		public NKCUIComStateButton m_NKM_UI_GAME_OPTION_SELECT_SERVER;

		// Token: 0x040074D9 RID: 29913
		public NKCUIComStateButton m_NKM_UI_GAME_OPTION_BUG_REPORT;

		// Token: 0x040074DA RID: 29914
		public GameObject m_objNKM_UI_GAME_OPTION_GUEST_ACCOUNT_TRANSFER;

		// Token: 0x040074DB RID: 29915
		public GameObject m_objNKM_UI_GAME_OPTION_GUEST_ACCOUNT_TRANSFER_dummy;

		// Token: 0x040074DC RID: 29916
		public GameObject m_objNKM_UI_GAME_OPTION_LOGOUT;

		// Token: 0x040074DD RID: 29917
		public GameObject m_objNKM_UI_GAME_OPTION_LOGOUT_dummy;

		// Token: 0x040074DE RID: 29918
		[Header("쿠폰 입력 버튼")]
		public NKCUIComStateButton m_NKM_UI_GAME_OPTION_ACCOUNT_COUPON;

		// Token: 0x040074DF RID: 29919
		public GameObject m_objNKM_UI_GAME_OPTION_ACCOUNT_COUPON;

		// Token: 0x040074E0 RID: 29920
		[Header("계정 연동 버튼")]
		public GameObject m_FACEBOOK;

		// Token: 0x040074E1 RID: 29921
		public GameObject m_GOOGLE;

		// Token: 0x040074E2 RID: 29922
		public GameObject m_NEXON;

		// Token: 0x040074E3 RID: 29923
		public GameObject m_APPLE;

		// Token: 0x040074E4 RID: 29924
		public GameObject m_NAVER;

		// Token: 0x040074E5 RID: 29925
		public GameObject m_TWITTER;

		// Token: 0x040074E6 RID: 29926
		public GameObject m_LINE;

		// Token: 0x040074E7 RID: 29927
		public GameObject m_STEAM;
	}
}
