using System;
using System.Collections.Generic;
using ClientPacket.Account;
using Cs.Logging;
using Cs.Protocol;
using NKC.UI;
using NKM;
using NKM.Shop;
using UnityEngine;

namespace NKC.Publisher
{
	// Token: 0x02000864 RID: 2148
	public class NKCPMNone : NKCPublisherModule
	{
		// Token: 0x0600553E RID: 21822 RVA: 0x0019E8F0 File Offset: 0x0019CAF0
		protected override NKCPublisherModule.NKCPMAuthentication MakeAuthInstance()
		{
			return new NKCPMNone.AuthNone();
		}

		// Token: 0x0600553F RID: 21823 RVA: 0x0019E8F7 File Offset: 0x0019CAF7
		protected override NKCPublisherModule.NKCPMInAppPurchase MakeInappInstance()
		{
			return new NKCPMNone.InAppNone();
		}

		// Token: 0x06005540 RID: 21824 RVA: 0x0019E8FE File Offset: 0x0019CAFE
		protected override NKCPublisherModule.NKCPMNotice MakeNoticeInstance()
		{
			return new NKCPMNone.NoticeNone();
		}

		// Token: 0x06005541 RID: 21825 RVA: 0x0019E905 File Offset: 0x0019CB05
		protected override NKCPublisherModule.NKCPMStatistics MakeStatisticsInstance()
		{
			return new NKCPMNone.StatisticsNone();
		}

		// Token: 0x17001042 RID: 4162
		// (get) Token: 0x06005542 RID: 21826 RVA: 0x0019E90C File Offset: 0x0019CB0C
		protected override NKCPublisherModule.ePublisherType _PublisherType
		{
			get
			{
				return NKCPublisherModule.ePublisherType.None;
			}
		}

		// Token: 0x17001043 RID: 4163
		// (get) Token: 0x06005543 RID: 21827 RVA: 0x0019E90F File Offset: 0x0019CB0F
		protected override bool _Busy
		{
			get
			{
				return NKCPMNone.s_bWait;
			}
		}

		// Token: 0x06005544 RID: 21828 RVA: 0x0019E916 File Offset: 0x0019CB16
		protected override void OnTimeOut()
		{
			NKCPMNone.s_bWait = false;
		}

		// Token: 0x06005545 RID: 21829 RVA: 0x0019E91E File Offset: 0x0019CB1E
		protected override void _Init(NKCPublisherModule.OnComplete dOnComplete)
		{
			NKCPublisherModule.InitState = NKCPublisherModule.ePublisherInitState.Initialized;
			NKCPMNone.RunFakeProcess(dOnComplete, "Init", false);
		}

		// Token: 0x06005546 RID: 21830 RVA: 0x0019E932 File Offset: 0x0019CB32
		private static void RunFakeProcess(NKCPublisherModule.OnComplete dOnComplete, string fakeMessage, bool showPopup)
		{
			Debug.Log(fakeMessage);
			if (dOnComplete != null)
			{
				dOnComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
			}
		}

		// Token: 0x04004413 RID: 17427
		private static bool s_bWait;

		// Token: 0x020014FD RID: 5373
		public class AuthNone : NKCPublisherModule.NKCPMAuthentication
		{
			// Token: 0x0600AA8E RID: 43662 RVA: 0x0034F458 File Offset: 0x0034D658
			public override string GetPublisherAccountCode()
			{
				if (NKCScenManager.CurrentUserData() != null)
				{
					return NKCScenManager.CurrentUserData().m_UserUID.ToString();
				}
				return "";
			}

			// Token: 0x0600AA8F RID: 43663 RVA: 0x0034F476 File Offset: 0x0034D676
			public override void LoginToPublisher(NKCPublisherModule.OnComplete dOnComplete)
			{
				NKCPMNone.RunFakeProcess(dOnComplete, "SyncAccount", false);
			}

			// Token: 0x0600AA90 RID: 43664 RVA: 0x0034F484 File Offset: 0x0034D684
			public override void PrepareCSLogin(NKCPublisherModule.OnComplete dOnComplete)
			{
				NKCPMNone.RunFakeProcess(dOnComplete, "Login", false);
			}

			// Token: 0x17001862 RID: 6242
			// (get) Token: 0x0600AA91 RID: 43665 RVA: 0x0034F492 File Offset: 0x0034D692
			public override bool LoginToPublisherCompleted
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600AA92 RID: 43666 RVA: 0x0034F495 File Offset: 0x0034D695
			public override void ChangeAccount(NKCPublisherModule.OnComplete onComplete, bool syncAccount)
			{
				if (onComplete != null)
				{
					onComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_FAIL_NO_SUPPORT, null);
				}
			}

			// Token: 0x0600AA93 RID: 43667 RVA: 0x0034F4A4 File Offset: 0x0034D6A4
			public override ISerializable MakeLoginServerLoginReqPacket()
			{
				if (string.IsNullOrWhiteSpace(NKCScenManager.GetScenManager().GetConnectGame().GetReconnectKey()))
				{
					NKMPacket_LOGIN_REQ nkmpacket_LOGIN_REQ = new NKMPacket_LOGIN_REQ();
					nkmpacket_LOGIN_REQ.protocolVersion = 845L;
					nkmpacket_LOGIN_REQ.accountID = PlayerPrefs.GetString("NKM_LOCAL_SAVE_KEY_LOGIN_ID_STRING");
					nkmpacket_LOGIN_REQ.password = PlayerPrefs.GetString("NKM_LOCAL_SAVE_KEY_LOGIN_PASSWORD_STRING");
					Log.Debug(string.Format("[Login] LoginReq ProtocolVersion[{0}] ID[{1}]", nkmpacket_LOGIN_REQ.protocolVersion, nkmpacket_LOGIN_REQ.accountID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMNone.cs", 161);
					NKM_USER_AUTH_LEVEL userAuthLevel = NKM_USER_AUTH_LEVEL.NORMAL_USER;
					nkmpacket_LOGIN_REQ.userAuthLevel = userAuthLevel;
					nkmpacket_LOGIN_REQ.deviceUid = SystemInfo.deviceUniqueIdentifier;
					return nkmpacket_LOGIN_REQ;
				}
				return new NKMPacket_RECONNECT_REQ
				{
					reconnectKey = NKCScenManager.GetScenManager().GetConnectGame().GetReconnectKey()
				};
			}

			// Token: 0x0600AA94 RID: 43668 RVA: 0x0034F553 File Offset: 0x0034D753
			public override ISerializable MakeGameServerLoginReqPacket(string accessToken)
			{
				return new NKMPacket_JOIN_LOBBY_REQ
				{
					protocolVersion = 845,
					accessToken = accessToken
				};
			}

			// Token: 0x0600AA95 RID: 43669 RVA: 0x0034F56C File Offset: 0x0034D76C
			public override void Logout(NKCPublisherModule.OnComplete dOnComplete)
			{
				NKCPMNone.RunFakeProcess(dOnComplete, "Logout", false);
			}

			// Token: 0x0600AA96 RID: 43670 RVA: 0x0034F57A File Offset: 0x0034D77A
			public override bool IsTryAuthWhenSessionExpired()
			{
				return true;
			}
		}

		// Token: 0x020014FE RID: 5374
		public class InAppNone : NKCPublisherModule.NKCPMInAppPurchase
		{
			// Token: 0x0600AA98 RID: 43672 RVA: 0x0034F585 File Offset: 0x0034D785
			public override void RequestBillingProductList(NKCPublisherModule.OnComplete dOnComplete)
			{
				if (dOnComplete != null)
				{
					dOnComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
				}
			}

			// Token: 0x17001863 RID: 6243
			// (get) Token: 0x0600AA99 RID: 43673 RVA: 0x0034F592 File Offset: 0x0034D792
			public override bool CheckReceivedBillingProductList
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600AA9A RID: 43674 RVA: 0x0034F595 File Offset: 0x0034D795
			public override void BillingRestore(NKCPublisherModule.OnComplete dOnComplete)
			{
				NKCPMNone.RunFakeProcess(dOnComplete, "Billing Restored", true);
			}

			// Token: 0x0600AA9B RID: 43675 RVA: 0x0034F5A3 File Offset: 0x0034D7A3
			public override void InappPurchase(ShopItemTemplet shopTemplet, NKCPublisherModule.OnComplete dOnComplete, string metadata = "", List<int> lstSelection = null)
			{
				if (dOnComplete != null)
				{
					dOnComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_INAPP_FAIL_NOT_SUPPORTED, null);
				}
			}

			// Token: 0x0600AA9C RID: 43676 RVA: 0x0034F5B4 File Offset: 0x0034D7B4
			public override bool IsRegisteredProduct(string marketID, int productID)
			{
				return NKCShopManager.GetShopTempletByMarketID(marketID) != null;
			}

			// Token: 0x0600AA9D RID: 43677 RVA: 0x0034F5C0 File Offset: 0x0034D7C0
			public override decimal GetLocalPrice(string marketID, int productID)
			{
				ShopItemTemplet shopTempletByMarketID = NKCShopManager.GetShopTempletByMarketID(marketID);
				return NKCScenManager.CurrentUserData().m_ShopData.GetRealPrice(shopTempletByMarketID, 1, false);
			}

			// Token: 0x0600AA9E RID: 43678 RVA: 0x0034F5EC File Offset: 0x0034D7EC
			public override string GetLocalPriceString(string marketID, int productID)
			{
				ShopItemTemplet shopTempletByMarketID = NKCShopManager.GetShopTempletByMarketID(marketID);
				return NKCUtilString.GetInAppPurchasePriceString(NKCScenManager.CurrentUserData().m_ShopData.GetRealPrice(shopTempletByMarketID, 1, false), productID);
			}

			// Token: 0x0600AA9F RID: 43679 RVA: 0x0034F61D File Offset: 0x0034D81D
			public override List<int> GetInappProductIDs()
			{
				return new List<int>(NKCShopManager.GetMarketProductList().Keys);
			}

			// Token: 0x0600AAA0 RID: 43680 RVA: 0x0034F62E File Offset: 0x0034D82E
			public override void OpenPolicy(NKCPublisherModule.OnComplete dOnClose)
			{
				if (dOnClose != null)
				{
					dOnClose(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
				}
			}
		}

		// Token: 0x020014FF RID: 5375
		public class NoticeNone : NKCPublisherModule.NKCPMNotice
		{
			// Token: 0x0600AAA2 RID: 43682 RVA: 0x0034F643 File Offset: 0x0034D843
			public override void OpenCustomerCenter(NKCPublisherModule.OnComplete dOnComplete)
			{
				Application.OpenURL("https://forum.nexon.com/counterside");
				if (dOnComplete != null)
				{
					dOnComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
				}
			}

			// Token: 0x0600AAA3 RID: 43683 RVA: 0x0034F65A File Offset: 0x0034D85A
			public override bool IsActiveCustomerCenter()
			{
				return true;
			}

			// Token: 0x0600AAA4 RID: 43684 RVA: 0x0034F65D File Offset: 0x0034D85D
			public override void OpenQnA(NKCPublisherModule.OnComplete dOnComplete)
			{
			}

			// Token: 0x0600AAA5 RID: 43685 RVA: 0x0034F660 File Offset: 0x0034D860
			public override void OpenNotice(NKCPublisherModule.OnComplete onComplete)
			{
				if (NKCDefineManager.DEFINE_WEBVIEW_TEST())
				{
					NKMPopUpBox.CloseWaitBox();
					NKCPopupNoticeWeb.Instance.Open("https://counterside.nexon.com/Banner/Index/", onComplete, false);
					return;
				}
				if (NKCNewsManager.CheckNeedNewsPopup(NKCSynchronizedTime.GetServerUTCTime(0.0)))
				{
					NKCUINews.Instance.SetDataAndOpen(true, eNewsFilterType.NOTICE, -1);
					NKCUINews.Instance.SetCloseCallback(delegate
					{
						NKCPublisherModule.OnComplete onComplete3 = onComplete;
						if (onComplete3 == null)
						{
							return;
						}
						onComplete3(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
					});
					return;
				}
				NKCPublisherModule.OnComplete onComplete2 = onComplete;
				if (onComplete2 == null)
				{
					return;
				}
				onComplete2(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
			}

			// Token: 0x0600AAA6 RID: 43686 RVA: 0x0034F6E8 File Offset: 0x0034D8E8
			public override void OpenPromotionalBanner(NKCPublisherModule.NKCPMNotice.eOptionalBannerPlaces placeType, NKCPublisherModule.OnComplete dOnComplete)
			{
				NKCPMNone.RunFakeProcess(dOnComplete, "OptionalBanner : " + placeType.ToString(), true);
			}

			// Token: 0x0600AAA7 RID: 43687 RVA: 0x0034F708 File Offset: 0x0034D908
			public override void NotifyMainenance(NKCPublisherModule.OnComplete dOnComplete)
			{
				NKCPMNone.RunFakeProcess(dOnComplete, "Maintanance", true);
			}
		}

		// Token: 0x02001500 RID: 5376
		public class StatisticsNone : NKCPublisherModule.NKCPMStatistics
		{
			// Token: 0x0600AAA9 RID: 43689 RVA: 0x0034F71E File Offset: 0x0034D91E
			public override void LogClientActionForPublisher(NKCPublisherModule.NKCPMStatistics.eClientAction funnelPosition, int key = 0, string data = null)
			{
				NKCPMNone.RunFakeProcess(null, string.Format("SendFunnel : {0} {1}", funnelPosition, key), false);
			}

			// Token: 0x0600AAAA RID: 43690 RVA: 0x0034F73D File Offset: 0x0034D93D
			public override void TrackPurchase(int itemID)
			{
				NKCPMNone.RunFakeProcess(null, string.Format("TrackPurchase : {0}", itemID), false);
			}
		}

		// Token: 0x02001501 RID: 5377
		public class PushNone : NKCPublisherModule.NKCPMPush
		{
			// Token: 0x0600AAAC RID: 43692 RVA: 0x0034F75E File Offset: 0x0034D95E
			public override void Init()
			{
			}

			// Token: 0x0600AAAD RID: 43693 RVA: 0x0034F760 File Offset: 0x0034D960
			protected override void CancelLocalPush(NKC_GAME_OPTION_ALARM_GROUP evtType)
			{
			}

			// Token: 0x0600AAAE RID: 43694 RVA: 0x0034F762 File Offset: 0x0034D962
			protected override void ClearAllLocalPush()
			{
				base.ClearAllLocalPush();
			}

			// Token: 0x0600AAAF RID: 43695 RVA: 0x0034F76C File Offset: 0x0034D96C
			protected override bool ReserveLocalPush(DateTime newUtcTime, NKC_GAME_OPTION_ALARM_GROUP evtType)
			{
				this.CancelLocalPush(evtType);
				TimeSpan timeLeft = NKCSynchronizedTime.GetTimeLeft(newUtcTime.Ticks);
				Debug.Log(string.Format("로컬푸시 등록 - 타입 : {0}, 등록 시간 : {1} : 남은 시간 : {2}", evtType, newUtcTime, timeLeft));
				return true;
			}
		}

		// Token: 0x02001502 RID: 5378
		public class PermissionNone : NKCPublisherModule.NKCPMPermission
		{
			// Token: 0x0600AAB1 RID: 43697 RVA: 0x0034F7B7 File Offset: 0x0034D9B7
			public override void RequestCameraPermission(NKCPublisherModule.OnComplete dOnComplete)
			{
				if (dOnComplete != null)
				{
					dOnComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
				}
			}
		}

		// Token: 0x02001503 RID: 5379
		public class ServerInfoDefault : NKCPublisherModule.NKCPMServerInfo
		{
			// Token: 0x0600AAB3 RID: 43699 RVA: 0x0034F7CC File Offset: 0x0034D9CC
			public override bool GetUseLocalSaveLastServerInfoToGetTags()
			{
				return !NKCDefineManager.DEFINE_SELECT_SERVER() && !NKCDefineManager.DEFINE_DOWNLOAD_CONFIG();
			}

			// Token: 0x0600AAB4 RID: 43700 RVA: 0x0034F7E4 File Offset: 0x0034D9E4
			public override string GetServerConfigPath()
			{
				string text = UnityEngine.Random.Range(1000000, 8000000).ToString();
				text += UnityEngine.Random.Range(1000000, 8000000).ToString();
				string str = "?p=" + text;
				string str2 = "http://FileServer.bside.com/ConnectionInfo/";
				string str3 = "ConnectionInfo.json";
				if (NKCDefineManager.DEFINE_SELECT_SERVER())
				{
					str2 = "http://FileServer.bside.com/server_config/Dev/";
					string customServerInfoAddress = NKCConnectionInfo.CustomServerInfoAddress;
					if (!string.IsNullOrEmpty(customServerInfoAddress))
					{
						str2 = customServerInfoAddress;
					}
					str3 = NKCConnectionInfo.ServerInfoFileName;
				}
				return str2 + str3 + str;
			}
		}

		// Token: 0x02001504 RID: 5380
		public class LocalizationNone : NKCPublisherModule.NKCPMLocalization
		{
			// Token: 0x0600AAB6 RID: 43702 RVA: 0x0034F877 File Offset: 0x0034DA77
			public override NKM_NATIONAL_CODE GetDefaultLanguage()
			{
				return NKM_NATIONAL_CODE.NNC_KOREA;
			}
		}

		// Token: 0x02001505 RID: 5381
		public class MarketingNone : NKCPublisherModule.NKCPMMarketing
		{
			// Token: 0x0600AAB8 RID: 43704 RVA: 0x0034F882 File Offset: 0x0034DA82
			public override bool SnsShareEnabled(NKMUnitData unitData)
			{
				return false;
			}

			// Token: 0x0600AAB9 RID: 43705 RVA: 0x0034F888 File Offset: 0x0034DA88
			public override void TrySnsShare(NKCPublisherModule.SNS_SHARE_TYPE sst, string capturePath, string thumbnailPath, NKCPublisherModule.OnComplete onComplete)
			{
				Debug.Log("TrySnsShare : " + capturePath + " / " + thumbnailPath);
				this.dOnSnsShareComplete = onComplete;
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, "이걸로 공유함?", new NKCPopupOKCancel.OnButton(this.OnShareOK), new NKCPopupOKCancel.OnButton(this.OnShareFinish), false);
			}

			// Token: 0x0600AABA RID: 43706 RVA: 0x0034F8DB File Offset: 0x0034DADB
			private void OnShareFinish()
			{
				NKCPublisherModule.OnComplete onComplete = this.dOnSnsShareComplete;
				if (onComplete == null)
				{
					return;
				}
				onComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
			}

			// Token: 0x0600AABB RID: 43707 RVA: 0x0034F8EF File Offset: 0x0034DAEF
			private void OnShareOK()
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, "공유했소", new NKCPopupOKCancel.OnButton(this.OnShareFinish), "");
			}

			// Token: 0x04009F8F RID: 40847
			private NKCPublisherModule.OnComplete dOnSnsShareComplete;
		}
	}
}
