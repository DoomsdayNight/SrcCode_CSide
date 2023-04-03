using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using ClientPacket.Account;
using Cs.Logging;
using Cs.Protocol;
using NKC.UI;
using NKM;
using NKM.Shop;
using UnityEngine;

namespace NKC.Publisher
{
	// Token: 0x02000862 RID: 2146
	public class NKCPMJPPC : NKCPublisherModule
	{
		// Token: 0x17001040 RID: 4160
		// (get) Token: 0x0600552B RID: 21803 RVA: 0x0019E638 File Offset: 0x0019C838
		protected override NKCPublisherModule.ePublisherType _PublisherType
		{
			get
			{
				return NKCPublisherModule.ePublisherType.JPPC;
			}
		}

		// Token: 0x17001041 RID: 4161
		// (get) Token: 0x0600552C RID: 21804 RVA: 0x0019E63B File Offset: 0x0019C83B
		protected override bool _Busy
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600552D RID: 21805 RVA: 0x0019E63E File Offset: 0x0019C83E
		protected override void OnTimeOut()
		{
		}

		// Token: 0x0600552E RID: 21806 RVA: 0x0019E640 File Offset: 0x0019C840
		private void OnDestroy()
		{
		}

		// Token: 0x0600552F RID: 21807 RVA: 0x0019E644 File Offset: 0x0019C844
		private static void NgsSendCallback(IntPtr bytes, uint size, uint sessionID)
		{
			byte[] array = new byte[size];
			Marshal.Copy(bytes, array, 0, (int)size);
			Log.Debug(string.Format("[NgsSendCallback] SessionID[{0}]", sessionID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMJPPC.cs", 73);
			NKCPacketSender.Send_NKMPacket_NEXON_NGS_DATA_NOT(array);
		}

		// Token: 0x06005530 RID: 21808 RVA: 0x0019E683 File Offset: 0x0019C883
		protected override NKCPublisherModule.NKCPMAuthentication MakeAuthInstance()
		{
			return new NKCPMJPPC.AuthJPPC();
		}

		// Token: 0x06005531 RID: 21809 RVA: 0x0019E68A File Offset: 0x0019C88A
		protected override NKCPublisherModule.NKCPMInAppPurchase MakeInappInstance()
		{
			return new NKCPMJPPC.InAppJPPC();
		}

		// Token: 0x06005532 RID: 21810 RVA: 0x0019E691 File Offset: 0x0019C891
		protected override NKCPublisherModule.NKCPMNotice MakeNoticeInstance()
		{
			return new NKCPMJPPC.NoticeJPPC();
		}

		// Token: 0x06005533 RID: 21811 RVA: 0x0019E698 File Offset: 0x0019C898
		protected override NKCPublisherModule.NKCPMServerInfo MakeServerInfoInstance()
		{
			return new NKCPMJPPC.ServerInfoJPPC();
		}

		// Token: 0x06005534 RID: 21812 RVA: 0x0019E69F File Offset: 0x0019C89F
		protected override NKCPublisherModule.NKCPMStatistics MakeStatisticsInstance()
		{
			return new NKCPMJPPC.StatisticsNone();
		}

		// Token: 0x06005535 RID: 21813 RVA: 0x0019E6A8 File Offset: 0x0019C8A8
		private static void OnAuthClosedCallback(uint nType)
		{
			if (nType == 1U)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, "인증 서비스가 중지 되었습니다.", delegate()
				{
					Application.Quit();
				}, "");
				return;
			}
			if (nType != 2U)
			{
				switch (nType)
				{
				case 20014U:
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, "IP가 변경 되었습니다.", delegate()
					{
						Application.Quit();
					}, "");
					return;
				case 20015U:
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, "넥슨 패스포트가 유효하지 않습니다.", delegate()
					{
						Application.Quit();
					}, "");
					return;
				case 20018U:
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, "다른 세션에 의해 로그아웃 되었습니다.", delegate()
					{
						Application.Quit();
					}, "");
					return;
				}
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, "인증 서버 접속이 끊겼습니다.", delegate()
				{
					Application.Quit();
				}, "");
				return;
			}
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, "네트워크 연결에 실패했습니다.", delegate()
			{
				Application.Quit();
			}, "");
		}

		// Token: 0x06005536 RID: 21814 RVA: 0x0019E81C File Offset: 0x0019CA1C
		protected override void _Init(NKCPublisherModule.OnComplete dOnComplete)
		{
			if (!NKCDefineManager.DEFINE_NX_PC_TEST() && !NKCDefineManager.DEFINE_NX_PC_STAGE() && !NKCDefineManager.DEFINE_NX_PC_LIVE())
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, "Stage Build인지 Live Build인지 알 수 없습니다.", delegate()
				{
					Application.Quit();
				}, "");
				return;
			}
			NKCPublisherModule.InitState = NKCPublisherModule.ePublisherInitState.Initialized;
			if (dOnComplete != null)
			{
				dOnComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
			}
		}

		// Token: 0x06005537 RID: 21815 RVA: 0x0019E883 File Offset: 0x0019CA83
		private static void RunFakeProcess(NKCPublisherModule.OnComplete dOnComplete, string fakeMessage, bool showPopup)
		{
			Debug.Log(fakeMessage);
			if (dOnComplete != null)
			{
				dOnComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
			}
		}

		// Token: 0x04004408 RID: 17416
		public const uint GAMECODE_TEST = 16818287U;

		// Token: 0x04004409 RID: 17417
		public const uint GAMECODE_STAGE = 16818288U;

		// Token: 0x0400440A RID: 17418
		public const uint GAMECODE_LIVE = 16818286U;

		// Token: 0x0400440B RID: 17419
		public const string URL_PAYMENT_LAW = "https://m.nexon.com/terms/625";

		// Token: 0x0400440C RID: 17420
		public const string URL_POLICY = "http://m.nexon.com/terms/60";

		// Token: 0x0400440D RID: 17421
		public const string URL_WEB_BANNER = "https://counterside.nexon.co.jp/news";

		// Token: 0x0400440E RID: 17422
		public const string URL_COMMERCIAL_LAW_PC = "https://m.nexon.com/terms/629";

		// Token: 0x0400440F RID: 17423
		public const string GAME_ID = "STAGE";

		// Token: 0x04004410 RID: 17424
		public const string SERVICE_ID = "MjY5OQ";

		// Token: 0x04004411 RID: 17425
		public const string URL_CUSTOMER_CENTER = "https://m-page.nexon.com/cc/jppc/auth/sso?clientId={0}&npp={1}";

		// Token: 0x020014F7 RID: 5367
		public class AuthJPPC : NKCPublisherModule.NKCPMAuthentication
		{
			// Token: 0x0600AA5F RID: 43615 RVA: 0x0034ED64 File Offset: 0x0034CF64
			public override bool Init()
			{
				this.m_strNexonPassport = "";
				this.m_strNxHWID = "";
				this.m_bLoginSuccessFromPubAuth = false;
				return true;
			}

			// Token: 0x0600AA60 RID: 43616 RVA: 0x0034ED84 File Offset: 0x0034CF84
			public override bool OnLoginSuccessToCS()
			{
				if (NKCPMJPPC.AuthJPPC.s_bNGS_Start_Success)
				{
					return true;
				}
				if (NKCPMJPPC.AuthJPPC.s_bNGS_Start)
				{
					return false;
				}
				NKCPMJPPC.AuthJPPC.s_bNGS_Start = true;
				int num = 0;
				if (num == 0)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, "NGS Start Failed", delegate()
					{
						Application.Quit();
					}, "");
					return num != 0;
				}
				NKCPMJPPC.AuthJPPC.s_bNGS_Start_Success = true;
				return num != 0;
			}

			// Token: 0x0600AA61 RID: 43617 RVA: 0x0034EDE7 File Offset: 0x0034CFE7
			public override string GetPublisherAccountCode()
			{
				return NKCPMNexonNGS.GetNpaCode();
			}

			// Token: 0x0600AA62 RID: 43618 RVA: 0x0034EDF0 File Offset: 0x0034CFF0
			public override void LoginToPublisher(NKCPublisherModule.OnComplete dOnComplete)
			{
				string[] commandLineArgs = Environment.GetCommandLineArgs();
				string text = "";
				if (commandLineArgs != null && commandLineArgs.Length > 1)
				{
					text = commandLineArgs[1];
				}
				Log.Debug("passPort : " + text, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMJPPC.cs", 250);
				text = text.Replace("/passport:", "");
				Log.Debug("/passport: 제거 후, passPort : " + text, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMJPPC.cs", 254);
				int num = 0;
				if (num != 0)
				{
					this.m_bLoginSuccessFromPubAuth = false;
					string additionalError = "";
					if (num <= 20018)
					{
						if (num != 20000)
						{
							if (num != 20002)
							{
								switch (num)
								{
								case 20013:
									additionalError = "プレイヤー認証情報がありません。";
									break;
								case 20014:
									additionalError = "ログインIPが不一致です。";
									break;
								case 20015:
									additionalError = "Nexon Passport整合性にエラーが発生しています。";
									break;
								case 20018:
									additionalError = "他のIPで接続されています。";
									break;
								}
							}
							else
							{
								additionalError = "メンテナンス中です。";
							}
						}
						else
						{
							additionalError = "SSOサーバーアクセス失敗、またはSSOサーバー内部エラーです。";
						}
					}
					else if (num != 20048)
					{
						if (num != 20049)
						{
							if (num == 20056)
							{
								additionalError = "誤ったチャネリング情報です。";
							}
						}
						else
						{
							additionalError = "誤ったゲーム情報です。";
						}
					}
					else
					{
						additionalError = "2次認証に失敗しました。";
					}
					if (dOnComplete != null)
					{
						dOnComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_AUTH_LOGIN_FAIL, additionalError);
					}
				}
			}

			// Token: 0x0600AA63 RID: 43619 RVA: 0x0034EF1D File Offset: 0x0034D11D
			public override void PrepareCSLogin(NKCPublisherModule.OnComplete dOnComplete)
			{
				if (this.m_bLoginSuccessFromPubAuth)
				{
					if (dOnComplete != null)
					{
						dOnComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
						return;
					}
				}
				else if (dOnComplete != null)
				{
					dOnComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_AUTH_LOGIN_FAIL_NOT_READY, null);
				}
			}

			// Token: 0x17001860 RID: 6240
			// (get) Token: 0x0600AA64 RID: 43620 RVA: 0x0034EF42 File Offset: 0x0034D142
			public override bool LoginToPublisherCompleted
			{
				get
				{
					return this.m_bLoginSuccessFromPubAuth;
				}
			}

			// Token: 0x0600AA65 RID: 43621 RVA: 0x0034EF4A File Offset: 0x0034D14A
			public override void ChangeAccount(NKCPublisherModule.OnComplete onComplete, bool syncAccount)
			{
				if (onComplete != null)
				{
					onComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_FAIL_NO_SUPPORT, null);
				}
			}

			// Token: 0x0600AA66 RID: 43622 RVA: 0x0034EF58 File Offset: 0x0034D158
			public override ISerializable MakeLoginServerLoginReqPacket()
			{
				if (string.IsNullOrWhiteSpace(NKCScenManager.GetScenManager().GetConnectGame().GetReconnectKey()))
				{
					NKMPacket_NXPC_LOGIN_REQ nkmpacket_NXPC_LOGIN_REQ = new NKMPacket_NXPC_LOGIN_REQ();
					nkmpacket_NXPC_LOGIN_REQ.deviceUid = this.m_strNxHWID;
					Debug.Log("nexonPassport : " + this.m_strNexonPassport);
					nkmpacket_NXPC_LOGIN_REQ.nexonPassport = this.m_strNexonPassport;
					nkmpacket_NXPC_LOGIN_REQ.protocolVersion = 845;
					nkmpacket_NXPC_LOGIN_REQ.ssoLoginDate = this.m_UserInfoAcquisitionTime;
					nkmpacket_NXPC_LOGIN_REQ.userMobileData = new NKMUserMobileData
					{
						m_MarketId = "0",
						m_Country = "JP",
						m_Language = "JA_JP",
						m_AuthPlatform = "SSO",
						m_Platform = Application.platform.ToString(),
						m_OsVersion = SystemInfo.operatingSystem.ToString(),
						m_AdId = "",
						m_ClientVersion = Application.version
					};
					return nkmpacket_NXPC_LOGIN_REQ;
				}
				return new NKMPacket_RECONNECT_REQ
				{
					reconnectKey = NKCScenManager.GetScenManager().GetConnectGame().GetReconnectKey()
				};
			}

			// Token: 0x0600AA67 RID: 43623 RVA: 0x0034F05A File Offset: 0x0034D25A
			public override ISerializable MakeGameServerLoginReqPacket(string accessToken)
			{
				return new NKMPacket_JOIN_LOBBY_REQ
				{
					protocolVersion = 845,
					accessToken = accessToken
				};
			}

			// Token: 0x0600AA68 RID: 43624 RVA: 0x0034F073 File Offset: 0x0034D273
			public override void Logout(NKCPublisherModule.OnComplete dOnComplete)
			{
				NKCPMJPPC.RunFakeProcess(dOnComplete, "Logout", false);
			}

			// Token: 0x04009F81 RID: 40833
			private bool m_bLoginSuccessFromPubAuth;

			// Token: 0x04009F82 RID: 40834
			private string m_strNexonPassport = "";

			// Token: 0x04009F83 RID: 40835
			private string m_strNxHWID = "";

			// Token: 0x04009F84 RID: 40836
			private DateTime m_UserInfoAcquisitionTime;

			// Token: 0x04009F85 RID: 40837
			public static bool s_bNGS_Start;

			// Token: 0x04009F86 RID: 40838
			public static bool s_bNGS_Start_Success;
		}

		// Token: 0x020014F8 RID: 5368
		public class InAppJPPC : NKCPublisherModule.NKCPMInAppPurchase
		{
			// Token: 0x0600AA6B RID: 43627 RVA: 0x0034F0A1 File Offset: 0x0034D2A1
			public override void RequestBillingProductList(NKCPublisherModule.OnComplete dOnComplete)
			{
				if (dOnComplete != null)
				{
					dOnComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
				}
			}

			// Token: 0x17001861 RID: 6241
			// (get) Token: 0x0600AA6C RID: 43628 RVA: 0x0034F0AE File Offset: 0x0034D2AE
			public override bool CheckReceivedBillingProductList
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600AA6D RID: 43629 RVA: 0x0034F0B1 File Offset: 0x0034D2B1
			public override void BillingRestore(NKCPublisherModule.OnComplete dOnComplete)
			{
				NKCPMJPPC.RunFakeProcess(dOnComplete, "Billing Restored", false);
			}

			// Token: 0x0600AA6E RID: 43630 RVA: 0x0034F0BF File Offset: 0x0034D2BF
			public override void InappPurchase(ShopItemTemplet shopTemplet, NKCPublisherModule.OnComplete dOnComplete, string metadata = "", List<int> lstSelection = null)
			{
				if (dOnComplete != null)
				{
					dOnComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_INAPP_FAIL_NOT_SUPPORTED, null);
				}
			}

			// Token: 0x0600AA6F RID: 43631 RVA: 0x0034F0D0 File Offset: 0x0034D2D0
			public override bool IsRegisteredProduct(string marketID, int productID)
			{
				return false;
			}

			// Token: 0x0600AA70 RID: 43632 RVA: 0x0034F0D4 File Offset: 0x0034D2D4
			public override decimal GetLocalPrice(string marketID, int productID)
			{
				ShopItemTemplet shopTempletByMarketID = NKCShopManager.GetShopTempletByMarketID(marketID);
				return NKCScenManager.CurrentUserData().m_ShopData.GetRealPrice(shopTempletByMarketID, 1, false);
			}

			// Token: 0x0600AA71 RID: 43633 RVA: 0x0034F100 File Offset: 0x0034D300
			public override string GetLocalPriceString(string marketID, int productID)
			{
				ShopItemTemplet shopTempletByMarketID = NKCShopManager.GetShopTempletByMarketID(marketID);
				return NKCUtilString.GetInAppPurchasePriceString(NKCScenManager.CurrentUserData().m_ShopData.GetRealPrice(shopTempletByMarketID, 1, false), productID);
			}

			// Token: 0x0600AA72 RID: 43634 RVA: 0x0034F131 File Offset: 0x0034D331
			public override List<int> GetInappProductIDs()
			{
				return new List<int>(NKCShopManager.GetMarketProductList().Keys);
			}

			// Token: 0x0600AA73 RID: 43635 RVA: 0x0034F142 File Offset: 0x0034D342
			public override void OpenPolicy(NKCPublisherModule.OnComplete dOnClose)
			{
				NKCPublisherModule.Notice.OpenURL("http://m.nexon.com/terms/60", dOnClose);
			}

			// Token: 0x0600AA74 RID: 43636 RVA: 0x0034F154 File Offset: 0x0034D354
			public override string GetCurrencyMark(int productID)
			{
				return "円";
			}

			// Token: 0x0600AA75 RID: 43637 RVA: 0x0034F15B File Offset: 0x0034D35B
			public override void OpenPaymentLaw(NKCPublisherModule.OnComplete dOnClose)
			{
				NKCPublisherModule.Notice.OpenURL("https://m.nexon.com/terms/625", dOnClose);
			}

			// Token: 0x0600AA76 RID: 43638 RVA: 0x0034F16D File Offset: 0x0034D36D
			public override void OpenCommercialLaw(NKCPublisherModule.OnComplete dOnClose)
			{
				NKCPublisherModule.Notice.OpenURL("https://m.nexon.com/terms/629", dOnClose);
			}
		}

		// Token: 0x020014F9 RID: 5369
		public class NoticeJPPC : NKCPublisherModule.NKCPMNotice
		{
			// Token: 0x0600AA78 RID: 43640 RVA: 0x0034F188 File Offset: 0x0034D388
			public override void OpenCustomerCenter(NKCPublisherModule.OnComplete dOnComplete)
			{
				Log.Debug("[OpenCustomerCenter] START", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMJPPC.cs", 464);
				string str = "";
				string text = "https://m-page.nexon.com/cc/jppc/auth/sso?clientId=MjY5OQ&npp=" + str;
				Log.Debug("[OpenCustomerCenter] " + text, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMJPPC.cs", 471);
				string text2 = string.Concat(new string[]
				{
					"{GameID:STAGE,CharacterName:",
					NKCScenManager.CurrentUserData().m_UserNickName,
					",ClientVersion:",
					Application.version,
					",UID:",
					NKCScenManager.CurrentUserData().m_UserUID.ToString(),
					",NPA_Code:",
					NKCPublisherModule.Auth.GetPublisherAccountCode(),
					",BusinessLicenseCode:",
					NKCScenManager.CurrentUserData().m_FriendCode.ToString(),
					"}"
				});
				Log.Debug("[OpenCustomerCenter] RequestHeader [" + text2 + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMJPPC.cs", 483);
				if (false)
				{
					HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(text.Trim());
					httpWebRequest.Method = "GET";
					httpWebRequest.Headers.Add("x-toy-locale", "ja-JP");
					httpWebRequest.Headers.Add("x-toy-game-meta", text2);
					try
					{
						using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
						{
							if (httpWebResponse.StatusCode == HttpStatusCode.OK)
							{
								Log.Debug("[OpenCustomerCenter] WebResponse - OK", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMJPPC.cs", 499);
							}
							else
							{
								Log.Debug("[OpenCustomerCenter] response StatusCode[" + httpWebResponse.StatusDescription + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMJPPC.cs", 503);
							}
						}
					}
					catch (Exception ex)
					{
						Log.Error("HTTPWebRequest Exception : " + ex.Message, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMJPPC.cs", 509);
					}
				}
				NKCPublisherModule.Notice.OpenURL(text.Trim(), dOnComplete);
			}

			// Token: 0x0600AA79 RID: 43641 RVA: 0x0034F378 File Offset: 0x0034D578
			public override bool IsActiveCustomerCenter()
			{
				return true;
			}

			// Token: 0x0600AA7A RID: 43642 RVA: 0x0034F37B File Offset: 0x0034D57B
			public override void OpenQnA(NKCPublisherModule.OnComplete dOnComplete)
			{
			}

			// Token: 0x0600AA7B RID: 43643 RVA: 0x0034F37D File Offset: 0x0034D57D
			public override bool CheckOpenNoticeWhenFirstLobbyVisit()
			{
				return false;
			}

			// Token: 0x0600AA7C RID: 43644 RVA: 0x0034F380 File Offset: 0x0034D580
			public override void OpenNotice(NKCPublisherModule.OnComplete onComplete)
			{
				NKCPublisherModule.Notice.OpenURL("https://counterside.nexon.co.jp/news", onComplete);
			}

			// Token: 0x0600AA7D RID: 43645 RVA: 0x0034F392 File Offset: 0x0034D592
			public override void OpenPromotionalBanner(NKCPublisherModule.NKCPMNotice.eOptionalBannerPlaces placeType, NKCPublisherModule.OnComplete dOnComplete)
			{
				NKCPMJPPC.RunFakeProcess(dOnComplete, "OptionalBanner : " + placeType.ToString(), true);
			}

			// Token: 0x0600AA7E RID: 43646 RVA: 0x0034F3B2 File Offset: 0x0034D5B2
			public override void NotifyMainenance(NKCPublisherModule.OnComplete dOnComplete)
			{
				NKCPMJPPC.RunFakeProcess(dOnComplete, "Maintanance", true);
			}
		}

		// Token: 0x020014FA RID: 5370
		public class ServerInfoJPPC : NKCPublisherModule.NKCPMServerInfo
		{
			// Token: 0x0600AA80 RID: 43648 RVA: 0x0034F3C8 File Offset: 0x0034D5C8
			public override bool GetUseLocalSaveLastServerInfoToGetTags()
			{
				return false;
			}
		}

		// Token: 0x020014FB RID: 5371
		public class StatisticsNone : NKCPublisherModule.NKCPMStatistics
		{
			// Token: 0x0600AA82 RID: 43650 RVA: 0x0034F3D3 File Offset: 0x0034D5D3
			public override void LogClientActionForPublisher(NKCPublisherModule.NKCPMStatistics.eClientAction funnelPosition, int key = 0, string data = null)
			{
				NKCPMJPPC.RunFakeProcess(null, string.Format("SendFunnel : {0} {1}", funnelPosition, key), false);
			}

			// Token: 0x0600AA83 RID: 43651 RVA: 0x0034F3F2 File Offset: 0x0034D5F2
			public override void TrackPurchase(int itemID)
			{
				NKCPMJPPC.RunFakeProcess(null, string.Format("TrackPurchase : {0}", itemID), false);
			}
		}
	}
}
