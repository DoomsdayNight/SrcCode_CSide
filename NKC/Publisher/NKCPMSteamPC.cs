using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using ClientPacket.Account;
using ClientPacket.Shop;
using Cs.Logging;
using Cs.Protocol;
using NKC.Localization;
using NKC.PacketHandler;
using NKC.UI;
using NKM;
using NKM.Shop;
using Steamworks;
using UnityEngine;

namespace NKC.Publisher
{
	// Token: 0x02000866 RID: 2150
	public class NKCPMSteamPC : NKCPublisherModule
	{
		// Token: 0x17001054 RID: 4180
		// (get) Token: 0x06005586 RID: 21894 RVA: 0x0019EEAC File Offset: 0x0019D0AC
		protected override NKCPublisherModule.ePublisherType _PublisherType
		{
			get
			{
				return NKCPublisherModule.ePublisherType.STEAM;
			}
		}

		// Token: 0x17001055 RID: 4181
		// (get) Token: 0x06005587 RID: 21895 RVA: 0x0019EEAF File Offset: 0x0019D0AF
		protected override bool _Busy
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005588 RID: 21896 RVA: 0x0019EEB2 File Offset: 0x0019D0B2
		protected override void OnTimeOut()
		{
		}

		// Token: 0x06005589 RID: 21897 RVA: 0x0019EEB4 File Offset: 0x0019D0B4
		private void OnDestroy()
		{
		}

		// Token: 0x0600558A RID: 21898 RVA: 0x0019EEB6 File Offset: 0x0019D0B6
		protected override NKCPublisherModule.NKCPMAuthentication MakeAuthInstance()
		{
			return new NKCPMSteamPC.AuthSteam();
		}

		// Token: 0x0600558B RID: 21899 RVA: 0x0019EEBD File Offset: 0x0019D0BD
		protected override NKCPublisherModule.NKCPMInAppPurchase MakeInappInstance()
		{
			return new NKCPMSteamPC.InAppSteam();
		}

		// Token: 0x0600558C RID: 21900 RVA: 0x0019EEC4 File Offset: 0x0019D0C4
		protected override NKCPublisherModule.NKCPMNotice MakeNoticeInstance()
		{
			return new NKCPMSteamPC.NoticeSteam();
		}

		// Token: 0x0600558D RID: 21901 RVA: 0x0019EECB File Offset: 0x0019D0CB
		protected override NKCPublisherModule.NKCPMServerInfo MakeServerInfoInstance()
		{
			return new NKCPMSteamPC.ServerInfoSteam();
		}

		// Token: 0x0600558E RID: 21902 RVA: 0x0019EED2 File Offset: 0x0019D0D2
		protected override NKCPublisherModule.NKCPMStatistics MakeStatisticsInstance()
		{
			return new NKCPMSteamPC.StatisticsNone();
		}

		// Token: 0x0600558F RID: 21903 RVA: 0x0019EED9 File Offset: 0x0019D0D9
		protected override NKCPublisherModule.NKCPMLocalization MakeLocalizationInstance()
		{
			return new NKCPMSteamPC.LocalizationSteam();
		}

		// Token: 0x06005590 RID: 21904 RVA: 0x0019EEE0 File Offset: 0x0019D0E0
		private void Start()
		{
			if (null == null)
			{
				Log.Debug("[SteamLogin] NKCPMSteamPC - Add SteamManager", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMSteamPC.cs", 81);
				new GameObject("SteamManager").AddComponent<SteamManager>();
			}
		}

		// Token: 0x06005591 RID: 21905 RVA: 0x0019EF0C File Offset: 0x0019D10C
		protected override void _Init(NKCPublisherModule.OnComplete dOnComplete)
		{
			NKCPublisherModule.InitState = NKCPublisherModule.ePublisherInitState.Initialized;
			Log.Debug(string.Format("[SteamPC][_Init] SteamManager[{0}]", SteamManager.Initialized), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMSteamPC.cs", 95);
			if (!SteamManager.Initialized)
			{
				if (dOnComplete != null)
				{
					dOnComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_STEAM_INITIALIZE_FAIL, null);
				}
				return;
			}
			if (dOnComplete != null)
			{
				dOnComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
			}
		}

		// Token: 0x06005592 RID: 21906 RVA: 0x0019EF61 File Offset: 0x0019D161
		private static void RunFakeProcess(NKCPublisherModule.OnComplete dOnComplete, string fakeMessage, bool showPopup)
		{
			Debug.Log(fakeMessage);
			if (dOnComplete != null)
			{
				dOnComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
			}
		}

		// Token: 0x02001517 RID: 5399
		public class AuthSteam : NKCPublisherModule.NKCPMAuthentication
		{
			// Token: 0x0600AB6A RID: 43882 RVA: 0x003509C4 File Offset: 0x0034EBC4
			public override bool Init()
			{
				this.m_bLoginSuccessFromPubAuth = false;
				this.m_pcbTicket = 0U;
				this.m_strTicket = string.Empty;
				Log.Debug(string.Format("[SteamLogin] AuthSteam - Init[{0}]", SteamManager.Initialized), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMSteamPC.cs", 130);
				if (!SteamManager.Initialized)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_STEAM_INITIALIZE), delegate()
					{
						NKCMain.QuitGame();
					}, "");
					return false;
				}
				this.m_AuthSessionTicket = HAuthTicket.Invalid;
				if (this.m_AuthSessionTicketResponse == null)
				{
					this.m_AuthSessionTicketResponse = Callback<GetAuthSessionTicketResponse_t>.Create(new Callback<GetAuthSessionTicketResponse_t>.DispatchDelegate(this.OnAuthSessionTicketResponse));
				}
				return true;
			}

			// Token: 0x0600AB6B RID: 43883 RVA: 0x00350A79 File Offset: 0x0034EC79
			public override string GetPublisherAccountCode()
			{
				return this.m_strUserID;
			}

			// Token: 0x0600AB6C RID: 43884 RVA: 0x00350A84 File Offset: 0x0034EC84
			public override void LoginToPublisher(NKCPublisherModule.OnComplete dOnComplete)
			{
				try
				{
					Log.Debug("[SteamLogin] LoginBySteam - LoginToPublisher", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMSteamPC.cs", 157);
					CSteamID steamID = SteamUser.GetSteamID();
					this.m_SteamUserID = steamID.m_SteamID;
					this.m_strUserID = this.m_SteamUserID.ToString();
					this.m_appID = SteamUtils.GetAppID().m_AppId.ToString();
					Log.Debug(string.Concat(new string[]
					{
						"[SteamLogin] AppID[",
						this.m_appID,
						"] Language[",
						((NKCPMSteamPC.LocalizationSteam)NKCPublisherModule.Localization).m_strGameLanguage,
						"] IPCountry[",
						((NKCPMSteamPC.LocalizationSteam)NKCPublisherModule.Localization).m_strCountry,
						"]"
					}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMSteamPC.cs", 165);
					this.m_onLoginToPublisherComplete = dOnComplete;
					this.m_AuthSessionTicket = HAuthTicket.Invalid;
					if (this.m_AuthSessionTicketResponse == null)
					{
						this.m_AuthSessionTicketResponse = Callback<GetAuthSessionTicketResponse_t>.Create(new Callback<GetAuthSessionTicketResponse_t>.DispatchDelegate(this.OnAuthSessionTicketResponse));
					}
					Log.Debug("[SteamLogin] LoginBySteam - GetAuthSessionTicket", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMSteamPC.cs", 175);
					this.m_AuthSessionTicket = SteamUser.GetAuthSessionTicket(this.m_Ticket, this.m_Ticket.Length, out this.m_pcbTicket);
					if (this.m_AuthSessionTicket == HAuthTicket.Invalid || this.m_pcbTicket == 0U)
					{
						Log.Error("[SteamLogin] GetAuthSessionTicket Failed", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMSteamPC.cs", 180);
						if (dOnComplete != null)
						{
							dOnComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_AUTH_LOGIN_FAIL, "GetAuthSessionTicket");
						}
					}
				}
				catch (InvalidOperationException ex)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_STEAM_INITIALIZE), delegate()
					{
						NKCMain.QuitGame();
					}, "");
					Log.Error("[SteamLogin] Exception [" + ex.Message + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMSteamPC.cs", 189);
				}
			}

			// Token: 0x0600AB6D RID: 43885 RVA: 0x00350C6C File Offset: 0x0034EE6C
			private void OnAuthSessionTicketResponse(GetAuthSessionTicketResponse_t pCallback)
			{
				Log.Debug("[SteamLogin] OnAuthSessionTicketResponse", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMSteamPC.cs", 200);
				if (this.m_AuthSessionTicket == HAuthTicket.Invalid)
				{
					return;
				}
				EResult eResult = pCallback.m_eResult;
				if (eResult == EResult.k_EResultOK || eResult == EResult.k_EResultAdministratorOK)
				{
					Array.Resize<byte>(ref this.m_Ticket, (int)this.m_pcbTicket);
					StringBuilder stringBuilder = new StringBuilder();
					int num = 0;
					while ((long)num < (long)((ulong)this.m_pcbTicket))
					{
						stringBuilder.AppendFormat("{0:x2}", this.m_Ticket[num]);
						num++;
					}
					this.m_strTicket = stringBuilder.ToString();
					this.m_bLoginSuccessFromPubAuth = true;
					if (this.m_onLoginToPublisherComplete != null)
					{
						this.m_onLoginToPublisherComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
						return;
					}
				}
				else if (this.m_onLoginToPublisherComplete != null)
				{
					this.m_onLoginToPublisherComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_AUTH_LOGIN_FAIL, pCallback.m_eResult.ToString());
				}
			}

			// Token: 0x0600AB6E RID: 43886 RVA: 0x00350D46 File Offset: 0x0034EF46
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

			// Token: 0x1700186D RID: 6253
			// (get) Token: 0x0600AB6F RID: 43887 RVA: 0x00350D6B File Offset: 0x0034EF6B
			public override bool LoginToPublisherCompleted
			{
				get
				{
					return this.m_bLoginSuccessFromPubAuth;
				}
			}

			// Token: 0x0600AB70 RID: 43888 RVA: 0x00350D73 File Offset: 0x0034EF73
			public override void ChangeAccount(NKCPublisherModule.OnComplete onComplete, bool syncAccount)
			{
				if (onComplete != null)
				{
					onComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_FAIL_NO_SUPPORT, null);
				}
			}

			// Token: 0x0600AB71 RID: 43889 RVA: 0x00350D80 File Offset: 0x0034EF80
			public override ISerializable MakeLoginServerLoginReqPacket()
			{
				if (string.IsNullOrWhiteSpace(NKCScenManager.GetScenManager().GetConnectGame().GetReconnectKey()))
				{
					NKMPacket_STEAM_LOGIN_REQ nkmpacket_STEAM_LOGIN_REQ = new NKMPacket_STEAM_LOGIN_REQ();
					nkmpacket_STEAM_LOGIN_REQ.accessToken = this.m_strTicket;
					nkmpacket_STEAM_LOGIN_REQ.accountId = this.m_strUserID;
					nkmpacket_STEAM_LOGIN_REQ.protocolVersion = 845;
					nkmpacket_STEAM_LOGIN_REQ.deviceUid = SystemInfo.deviceUniqueIdentifier;
					NKMUserMobileData userMobileData = new NKMUserMobileData
					{
						m_AdId = "",
						m_MarketId = "Steam",
						m_AuthPlatform = "Steam",
						m_Country = "Unknown",
						m_Language = NKCStringTable.GetCurrLanguageCode(),
						m_Platform = Application.platform.ToString(),
						m_OsVersion = SystemInfo.operatingSystem.ToString(),
						m_ClientVersion = Application.version
					};
					nkmpacket_STEAM_LOGIN_REQ.userMobileData = userMobileData;
					return nkmpacket_STEAM_LOGIN_REQ;
				}
				return new NKMPacket_RECONNECT_REQ
				{
					reconnectKey = NKCScenManager.GetScenManager().GetConnectGame().GetReconnectKey()
				};
			}

			// Token: 0x0600AB72 RID: 43890 RVA: 0x00350E6C File Offset: 0x0034F06C
			public override ISerializable MakeGameServerLoginReqPacket(string accessToken)
			{
				return new NKMPacket_JOIN_LOBBY_REQ
				{
					protocolVersion = 845,
					accessToken = accessToken
				};
			}

			// Token: 0x0600AB73 RID: 43891 RVA: 0x00350E85 File Offset: 0x0034F085
			public override void Logout(NKCPublisherModule.OnComplete dOnComplete)
			{
				NKCPMSteamPC.RunFakeProcess(dOnComplete, "Logout", false);
			}

			// Token: 0x0600AB74 RID: 43892 RVA: 0x00350E93 File Offset: 0x0034F093
			public override string GetAccountLinkText()
			{
				return NKCStringTable.GetString("SI_PF_STEAMLINK_TO_MOBILE", false);
			}

			// Token: 0x04009FB6 RID: 40886
			private bool m_bLoginSuccessFromPubAuth;

			// Token: 0x04009FB7 RID: 40887
			protected Callback<GetAuthSessionTicketResponse_t> m_AuthSessionTicketResponse;

			// Token: 0x04009FB8 RID: 40888
			private HAuthTicket m_AuthSessionTicket;

			// Token: 0x04009FB9 RID: 40889
			private byte[] m_Ticket = new byte[1024];

			// Token: 0x04009FBA RID: 40890
			private uint m_pcbTicket;

			// Token: 0x04009FBB RID: 40891
			private string m_strTicket;

			// Token: 0x04009FBC RID: 40892
			private ulong m_SteamUserID;

			// Token: 0x04009FBD RID: 40893
			private string m_strUserID;

			// Token: 0x04009FBE RID: 40894
			public string m_appID;

			// Token: 0x04009FBF RID: 40895
			private NKCPublisherModule.OnComplete m_onLoginToPublisherComplete;
		}

		// Token: 0x02001518 RID: 5400
		public class LocalizationSteam : NKCPublisherModule.NKCPMLocalization
		{
			// Token: 0x1700186E RID: 6254
			// (get) Token: 0x0600AB76 RID: 43894 RVA: 0x00350EB8 File Offset: 0x0034F0B8
			public override bool UseDefaultLanguageOnFirstRun
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700186F RID: 6255
			// (get) Token: 0x0600AB77 RID: 43895 RVA: 0x00350EBB File Offset: 0x0034F0BB
			public string m_strCountry
			{
				get
				{
					if (this._steamCurrentCountry == null)
					{
						this._steamCurrentCountry = SteamUtils.GetIPCountry();
						Log.Debug("[LocalizationSteam] m_strCountry[" + this._steamCurrentCountry + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMSteamPC.cs", 328);
					}
					return this._steamCurrentCountry;
				}
			}

			// Token: 0x17001870 RID: 6256
			// (get) Token: 0x0600AB78 RID: 43896 RVA: 0x00350EFC File Offset: 0x0034F0FC
			public string m_strGameLanguage
			{
				get
				{
					if (this._steamCurrentGameLanguage == null)
					{
						Log.Debug("[LocalizationSteam] m_strGameLanguage is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMSteamPC.cs", 342);
						if (SteamManager.Initialized)
						{
							string steamUILanguage = SteamUtils.GetSteamUILanguage();
							this._steamCurrentGameLanguage = SteamApps.GetCurrentGameLanguage();
							Log.Debug(string.Concat(new string[]
							{
								"[LocalizationSteam] GetCurrentGameLanguage[",
								this._steamCurrentGameLanguage,
								"] GetSteamUILanguage[",
								steamUILanguage,
								"]"
							}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMSteamPC.cs", 348);
							string availableGameLanguages = SteamApps.GetAvailableGameLanguages();
							Log.Debug("[LocalizationSteam] GetAvailableGameLanguages[" + availableGameLanguages + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMSteamPC.cs", 351);
						}
						else
						{
							this._steamCurrentGameLanguage = "english";
						}
						this.CreateSteamLanguageCodeData();
					}
					return this._steamCurrentGameLanguage;
				}
			}

			// Token: 0x0600AB79 RID: 43897 RVA: 0x00350FC0 File Offset: 0x0034F1C0
			public void CreateSteamLanguageCodeData()
			{
				Log.Debug("[LocalizationSteam] CreateSteamLanguageCodeData", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMSteamPC.cs", 387);
				this.m_steamLanguageCodeData.Clear();
				this.m_steamLanguageCodeData.Add("arabic", new NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData("Arabic", "العربية", "ar", null));
				this.m_steamLanguageCodeData.Add("bulgarian", new NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData("Bulgarian", "български език", "bg", null));
				this.m_steamLanguageCodeData.Add("finnish", new NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData("Finnish", "Suomi", "fi", null));
				this.m_steamLanguageCodeData.Add("greek", new NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData("Greek", "Ελληνικά", "el", null));
				this.m_steamLanguageCodeData.Add("hungarian", new NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData("Hungarian", "Magyar", "hu", null));
				this.m_steamLanguageCodeData.Add("italian", new NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData("Italian", "Italiano", "it", null));
				this.m_steamLanguageCodeData.Add("norwegian", new NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData("Norwegian", "Norsk", "no", null));
				this.m_steamLanguageCodeData.Add("polish", new NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData("Polish", "Polski", "pl", null));
				this.m_steamLanguageCodeData.Add("portuguese", new NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData("Portuguese", "Português", "pt", null));
				this.m_steamLanguageCodeData.Add("brazilian", new NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData("Portuguese-Brazil", "Português-Brasil", "pt-BR", null));
				this.m_steamLanguageCodeData.Add("romanian", new NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData("Romanian", "Română", "ro", null));
				this.m_steamLanguageCodeData.Add("russian", new NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData("Russian", "Русский", "ru", null));
				this.m_steamLanguageCodeData.Add("spanish", new NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData("Spanish-Spain", "Español-España", "es", null));
				this.m_steamLanguageCodeData.Add("latam", new NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData("Spanish-Latin America", "Español-Latinoamérica", "es-419", null));
				this.m_steamLanguageCodeData.Add("swedish", new NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData("Swedish", "Svenska", "sv", null));
				this.m_steamLanguageCodeData.Add("turkish", new NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData("Turkish", "Türkçe", "tr", null));
				this.m_steamLanguageCodeData.Add("ukrainian", new NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData("Ukrainian", "Українська", "uk", null));
				this.m_steamLanguageCodeData.Add("schinese", new NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData("Chinese (Simplified)", "简体中文", "zh-CN", "zh-hans"));
				this.m_steamLanguageCodeData.Add("tchinese", new NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData("Chinese (Traditional)", "繁體中文", "zh-TW", "zh-hant"));
				this.m_steamLanguageCodeData.Add("czech", new NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData("Czech", "čeština", "cs", "de"));
				this.m_steamLanguageCodeData.Add("danish", new NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData("Danish", "Dansk", "da", "de"));
				this.m_steamLanguageCodeData.Add("dutch", new NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData("Dutch", "Nederlands", "nl", "de"));
				this.m_steamLanguageCodeData.Add("german", new NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData("German", "Deutsch", "de", "de"));
				this.m_steamLanguageCodeData.Add("english", new NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData("English", "English", "en", "en"));
				this.m_steamLanguageCodeData.Add("french", new NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData("French", "Français", "fr", "fr"));
				this.m_steamLanguageCodeData.Add("japanese", new NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData("Japanese", "日本語", "ja", "ja"));
				this.m_steamLanguageCodeData.Add("koreana", new NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData("Korean", "한국어", "ko", "ko"));
				this.m_steamLanguageCodeData.Add("thai", new NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData("Thai", "ไทย", "th", "th"));
				this.m_steamLanguageCodeData.Add("vietnamese", new NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData("Vietnamese", "Tiếng Việt", "vn", "vi"));
			}

			// Token: 0x0600AB7A RID: 43898 RVA: 0x00351450 File Offset: 0x0034F650
			public string GetWebApiLanguageCode(string apiLanguageCode)
			{
				if (this.m_steamLanguageCodeData == null)
				{
					return "";
				}
				NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData steamLanguageCodeData;
				if (this.m_steamLanguageCodeData.TryGetValue(apiLanguageCode, out steamLanguageCodeData))
				{
					return steamLanguageCodeData.webApiLanguageCode;
				}
				return "";
			}

			// Token: 0x0600AB7B RID: 43899 RVA: 0x00351487 File Offset: 0x0034F687
			public string GetCurrentWebApiLanguageCode()
			{
				return this.GetWebApiLanguageCode(this.m_strGameLanguage);
			}

			// Token: 0x0600AB7C RID: 43900 RVA: 0x00351498 File Offset: 0x0034F698
			public string GetCurrentCSLanguageCode()
			{
				if (this.m_steamLanguageCodeData == null)
				{
					return "";
				}
				NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData steamLanguageCodeData;
				if (this.m_steamLanguageCodeData.TryGetValue(this.m_strGameLanguage, out steamLanguageCodeData) && !string.IsNullOrEmpty(steamLanguageCodeData.csLanguageCode))
				{
					return steamLanguageCodeData.csLanguageCode;
				}
				return "";
			}

			// Token: 0x0600AB7D RID: 43901 RVA: 0x003514E4 File Offset: 0x0034F6E4
			public override NKM_NATIONAL_CODE GetDefaultLanguage()
			{
				Debug.Log("[LocalizationSteam] GetDefaultLanguage");
				NKM_NATIONAL_CODE result = NKM_NATIONAL_CODE.NNC_ENG;
				if (!SteamManager.Initialized)
				{
					NKM_NATIONAL_CODE nkm_NATIONAL_CODE = NKCGameOptionData.LoadLanguageCode(NKM_NATIONAL_CODE.NNC_END);
					if (nkm_NATIONAL_CODE != NKM_NATIONAL_CODE.NNC_END)
					{
						Debug.Log("LocalizationSteam - SteamManager not initialized. using saved lang code [" + nkm_NATIONAL_CODE.ToString() + "]");
						return nkm_NATIONAL_CODE;
					}
				}
				string currentCSLanguageCode = this.GetCurrentCSLanguageCode();
				Debug.Log("[LocalizationSteam] - SystemLanguageCode [" + currentCSLanguageCode + "]");
				if (string.IsNullOrWhiteSpace(currentCSLanguageCode))
				{
					Log.Debug("[LocalizationSteam] Can't Find GetDeviceLanguageCode : Set to Default Language [" + result.ToString() + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMSteamPC.cs", 484);
					return result;
				}
				string langTagByLangCode = NKCLocalization.GetLangTagByLangCode(currentCSLanguageCode);
				Debug.Log("[LocalizationSteam] - LanguageTag [" + langTagByLangCode + "]");
				if (string.IsNullOrWhiteSpace(langTagByLangCode))
				{
					Log.Debug(string.Concat(new string[]
					{
						"[LocalizationSteam] Can't Find LangCode By [",
						currentCSLanguageCode,
						"] : Set to Default Language [",
						result.ToString(),
						"]"
					}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMSteamPC.cs", 492);
					return result;
				}
				bool flag = false;
				TextAsset textAsset = Resources.Load<TextAsset>("LUA_DEFAULT_CONTENTS_TAG");
				if (textAsset != null)
				{
					Debug.Log("[LocalizationSteam] - patcherStringLua");
					string str = textAsset.ToString();
					using (NKMLua nkmlua = new NKMLua())
					{
						if (!nkmlua.DoString(str))
						{
							Log.Debug("[LocalizationSteam] Can't load DEFAULT_CONTENTS_TAG : Set to Default Language [" + result.ToString() + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMSteamPC.cs", 507);
							return result;
						}
						List<string> list = new List<string>();
						if (!nkmlua.GetData(CountryTagType.GLOBAL.ToString(), list))
						{
							Log.Debug("[LocalizationSteam] Can't load Global taglist : Set to Default Language [" + result.ToString() + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMSteamPC.cs", 514);
							return result;
						}
						using (List<string>.Enumerator enumerator = list.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (enumerator.Current == langTagByLangCode)
								{
									flag = true;
									break;
								}
							}
						}
					}
					if (!flag)
					{
						goto IL_258;
					}
					Debug.Log("[LocalizationSteam] - FoundLangTagInDefaultTags");
					NKM_NATIONAL_CODE result2;
					if (NKCLocalization.s_dicLanguageTag.TryGetValue(langTagByLangCode, out result2))
					{
						Log.Debug("[LocalizationSteam] Selected Default Language : Set Language [" + result2.ToString() + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMSteamPC.cs", 533);
						return result2;
					}
				}
				IL_258:
				Log.Debug("[LocalizationSteam] Couldn't set default language : Set to Default Language [" + result.ToString() + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMSteamPC.cs", 539);
				return result;
			}

			// Token: 0x04009FC0 RID: 40896
			private string _steamCurrentCountry;

			// Token: 0x04009FC1 RID: 40897
			private string _steamCurrentGameLanguage;

			// Token: 0x04009FC2 RID: 40898
			private Dictionary<string, NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData> m_steamLanguageCodeData = new Dictionary<string, NKCPMSteamPC.LocalizationSteam.SteamLanguageCodeData>();

			// Token: 0x02001A68 RID: 6760
			public class SteamLanguageCodeData
			{
				// Token: 0x0600BBD5 RID: 48085 RVA: 0x0036F9A2 File Offset: 0x0036DBA2
				public SteamLanguageCodeData(string _name, string _nativeName, string _webApiLanguageCode, string _csLanguageCode)
				{
					this.name = _name;
					this.nativeName = _nativeName;
					this.webApiLanguageCode = _webApiLanguageCode;
					this.csLanguageCode = _csLanguageCode;
				}

				// Token: 0x0400AE70 RID: 44656
				public string webApiLanguageCode;

				// Token: 0x0400AE71 RID: 44657
				public string name;

				// Token: 0x0400AE72 RID: 44658
				public string nativeName;

				// Token: 0x0400AE73 RID: 44659
				public string csLanguageCode;
			}
		}

		// Token: 0x02001519 RID: 5401
		public class InAppSteam : NKCPublisherModule.NKCPMInAppPurchase
		{
			// Token: 0x0600AB7F RID: 43903 RVA: 0x003517A8 File Offset: 0x0034F9A8
			public override void Init()
			{
				if (this.m_MicroTxnAuthorizationResponse == null)
				{
					this.m_MicroTxnAuthorizationResponse = Callback<MicroTxnAuthorizationResponse_t>.Create(new Callback<MicroTxnAuthorizationResponse_t>.DispatchDelegate(this.OnMicroTxnAuthorizationResponse));
				}
				if (this.m_GameOverlayActivated == null)
				{
					this.m_GameOverlayActivated = Callback<GameOverlayActivated_t>.Create(new Callback<GameOverlayActivated_t>.DispatchDelegate(this.OnGameOverlayActivated));
				}
				using (IEnumerator<CultureInfo> enumerator = this.GetCultureInfosByCurrencySymbol("USD").GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						CultureInfo cultureInfoForUSD = enumerator.Current;
						this.m_cultureInfoForUSD = cultureInfoForUSD;
					}
				}
				base.Init();
			}

			// Token: 0x0600AB80 RID: 43904 RVA: 0x00351840 File Offset: 0x0034FA40
			public override void RequestBillingProductList(NKCPublisherModule.OnComplete dOnComplete)
			{
				if (dOnComplete != null)
				{
					dOnComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
				}
			}

			// Token: 0x17001871 RID: 6257
			// (get) Token: 0x0600AB81 RID: 43905 RVA: 0x0035184D File Offset: 0x0034FA4D
			public override bool CheckReceivedBillingProductList
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600AB82 RID: 43906 RVA: 0x00351850 File Offset: 0x0034FA50
			public override void BillingRestore(NKCPublisherModule.OnComplete dOnComplete)
			{
				NKCPMSteamPC.RunFakeProcess(dOnComplete, "Billing Restored Fake", false);
			}

			// Token: 0x0600AB83 RID: 43907 RVA: 0x00351860 File Offset: 0x0034FA60
			public override void InappPurchase(ShopItemTemplet shopTemplet, NKCPublisherModule.OnComplete dOnComplete, string metadata = "", List<int> lstSelection = null)
			{
				this.m_requestedShopItemTemplet = null;
				this.m_onInappPurchaseComplete = dOnComplete;
				if (shopTemplet == null)
				{
					if (dOnComplete != null)
					{
						dOnComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_INAPP_FAIL_BAD_ITEM, null);
					}
					return;
				}
				this.m_requestedShopItemTemplet = shopTemplet;
				this.m_payload = null;
				if (lstSelection != null)
				{
					this.m_payload = NKCShopManager.EncodeCustomPackageSelectList(lstSelection);
				}
				NKMPopUpBox.OpenWaitBox(0f, "");
				this.Send_NKMPacket_STEAM_BUY_INIT_REQ(shopTemplet);
			}

			// Token: 0x0600AB84 RID: 43908 RVA: 0x003518C4 File Offset: 0x0034FAC4
			public void Send_NKMPacket_STEAM_BUY_INIT_REQ(ShopItemTemplet shopItemTemplet)
			{
				NKMPacket_STEAM_BUY_INIT_REQ nkmpacket_STEAM_BUY_INIT_REQ = new NKMPacket_STEAM_BUY_INIT_REQ();
				nkmpacket_STEAM_BUY_INIT_REQ.steamId = NKCPublisherModule.Auth.GetPublisherAccountCode();
				nkmpacket_STEAM_BUY_INIT_REQ.productId = shopItemTemplet.m_ProductID;
				nkmpacket_STEAM_BUY_INIT_REQ.itemShopDesc = shopItemTemplet.GetItemName();
				nkmpacket_STEAM_BUY_INIT_REQ.language = ((NKCPMSteamPC.LocalizationSteam)NKCPublisherModule.Localization).GetCurrentWebApiLanguageCode();
				nkmpacket_STEAM_BUY_INIT_REQ.country = ((NKCPMSteamPC.LocalizationSteam)NKCPublisherModule.Localization).m_strCountry;
				Log.Debug(string.Format("[Steam][Inapp] NKMPacket_STEAM_BUY_INIT_REQ steamID[{0}] productID[{1}] lang[{2}] country[{3}]", new object[]
				{
					nkmpacket_STEAM_BUY_INIT_REQ.steamId,
					nkmpacket_STEAM_BUY_INIT_REQ.productId,
					nkmpacket_STEAM_BUY_INIT_REQ.language,
					nkmpacket_STEAM_BUY_INIT_REQ.country
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMSteamPC.cs", 635);
				this.m_bOverlayActivated = false;
				NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_STEAM_BUY_INIT_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
			}

			// Token: 0x0600AB85 RID: 43909 RVA: 0x0035198C File Offset: 0x0034FB8C
			public void OnRecv(NKMPacket_STEAM_BUY_INIT_ACK sPacket)
			{
				if (NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
				{
					Debug.Log(string.Format("[Steam][Inapp] NKMPacket_STEAM_BUY_INIT_ACK product[{0}] orderID[{1}]", sPacket.productId, sPacket.orderId));
					NKMPopUpBox.OpenWaitBox(0f, "");
					this.m_initWaitCoroutine = NKCPublisherModule.Instance.StartCoroutine(this.WaitForOverlay(5f));
					return;
				}
				this.m_requestedShopItemTemplet = null;
				NKCPublisherModule.OnComplete onInappPurchaseComplete = this.m_onInappPurchaseComplete;
				if (onInappPurchaseComplete == null)
				{
					return;
				}
				onInappPurchaseComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_INAPP_FAIL, sPacket.errorCode.ToString());
			}

			// Token: 0x0600AB86 RID: 43910 RVA: 0x00351A25 File Offset: 0x0034FC25
			public IEnumerator WaitForOverlay(float fTime)
			{
				yield return new WaitForSeconds(fTime);
				NKMPopUpBox.CloseWaitBox();
				if (!this.m_bOverlayActivated)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_STEAM_ENABLE_OVERLAY), null, "");
				}
				yield break;
			}

			// Token: 0x0600AB87 RID: 43911 RVA: 0x00351A3C File Offset: 0x0034FC3C
			private void OnMicroTxnAuthorizationResponse(MicroTxnAuthorizationResponse_t pCallback)
			{
				Debug.Log("[Steam][Inapp][MicroTxn] OnMicroTxnAuthorizationResponse");
				NKMPopUpBox.CloseWaitBox();
				this.m_bOverlayActivated = true;
				if (pCallback.m_bAuthorized == 1)
				{
					Debug.Log(string.Format("[Steam][Inapp][MicroTxn] Authorized Payment - orderID[{0}]", pCallback.m_ulOrderID));
					this.Send_NKMPacket_STEAM_BUY_REQ(pCallback.m_ulOrderID);
					return;
				}
				Debug.Log(string.Format("[Steam][Inapp][MicroTxn] Failed to authorize payment - orderID[{0}]", pCallback.m_ulOrderID));
				NKCPublisherModule.OnComplete onInappPurchaseComplete = this.m_onInappPurchaseComplete;
				if (onInappPurchaseComplete == null)
				{
					return;
				}
				onInappPurchaseComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_INAPP_FAIL_USER_CANCEL, null);
			}

			// Token: 0x0600AB88 RID: 43912 RVA: 0x00351AC0 File Offset: 0x0034FCC0
			private void OnGameOverlayActivated(GameOverlayActivated_t pCallback)
			{
				if (this.m_initWaitCoroutine != null)
				{
					NKCPublisherModule.Instance.StopCoroutine(this.m_initWaitCoroutine);
				}
				NKMPopUpBox.CloseWaitBox();
				this.m_bOverlayActivated = true;
				if (pCallback.m_bActive != 0)
				{
					Log.Debug("[Steam][Overlay] Steam Overlay has been activated", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMSteamPC.cs", 706);
					return;
				}
				Log.Debug("[Steam][Overlay] Steam Overlay has been closed", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Publisher/NKCPMSteamPC.cs", 710);
			}

			// Token: 0x0600AB89 RID: 43913 RVA: 0x00351B24 File Offset: 0x0034FD24
			public void Send_NKMPacket_STEAM_BUY_REQ(ulong orderID)
			{
				Debug.Log(string.Format("[Steam][Inapp][MicroTxn] NKMPacket_STEAM_BUY_REQ - orderID[{0}]", orderID));
				List<int> selectIndices = NKCShopManager.DecodeCustomPackageSelectList(this.m_payload);
				NKMPacket_STEAM_BUY_REQ nkmpacket_STEAM_BUY_REQ = new NKMPacket_STEAM_BUY_REQ();
				nkmpacket_STEAM_BUY_REQ.steamId = NKCPublisherModule.Auth.GetPublisherAccountCode();
				nkmpacket_STEAM_BUY_REQ.orderId = orderID.ToString();
				nkmpacket_STEAM_BUY_REQ.productId = this.m_requestedShopItemTemplet.m_ProductID;
				nkmpacket_STEAM_BUY_REQ.country = ((NKCPMSteamPC.LocalizationSteam)NKCPublisherModule.Localization).m_strCountry;
				nkmpacket_STEAM_BUY_REQ.selectIndices = selectIndices;
				NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_STEAM_BUY_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
				NKCPublisherModule.OnComplete onInappPurchaseComplete = this.m_onInappPurchaseComplete;
				if (onInappPurchaseComplete == null)
				{
					return;
				}
				onInappPurchaseComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
			}

			// Token: 0x0600AB8A RID: 43914 RVA: 0x00351BC7 File Offset: 0x0034FDC7
			public override bool IsRegisteredProduct(string marketID, int productID)
			{
				return NKCShopManager.GetShopTempletByMarketID(marketID) != null;
			}

			// Token: 0x0600AB8B RID: 43915 RVA: 0x00351BD4 File Offset: 0x0034FDD4
			public override decimal GetLocalPrice(string marketID, int productID)
			{
				ShopItemTemplet shopTempletByMarketID = NKCShopManager.GetShopTempletByMarketID(marketID);
				if (shopTempletByMarketID != null)
				{
					return NKCScenManager.CurrentUserData().m_ShopData.GetRealPrice(shopTempletByMarketID, 1, true);
				}
				return 0m;
			}

			// Token: 0x0600AB8C RID: 43916 RVA: 0x00351C08 File Offset: 0x0034FE08
			private IEnumerable<CultureInfo> GetCultureInfosByCurrencySymbol(string currencySymbol)
			{
				if (currencySymbol == null)
				{
					throw new ArgumentNullException("currencySymbol");
				}
				return from x in CultureInfo.GetCultures(CultureTypes.SpecificCultures)
				where new RegionInfo(x.LCID).ISOCurrencySymbol == currencySymbol
				select x;
			}

			// Token: 0x0600AB8D RID: 43917 RVA: 0x00351C4C File Offset: 0x0034FE4C
			public override string GetLocalPriceString(string marketID, int productID)
			{
				ShopItemTemplet shopTempletByMarketID = NKCShopManager.GetShopTempletByMarketID(marketID);
				if (shopTempletByMarketID != null)
				{
					return ((float)NKCScenManager.CurrentUserData().m_ShopData.GetRealPrice(shopTempletByMarketID, 1, true) / 100f).ToString("c", this.m_cultureInfoForUSD);
				}
				return "-";
			}

			// Token: 0x0600AB8E RID: 43918 RVA: 0x00351C95 File Offset: 0x0034FE95
			public override List<int> GetInappProductIDs()
			{
				return new List<int>(NKCShopManager.GetMarketProductList().Keys);
			}

			// Token: 0x0600AB8F RID: 43919 RVA: 0x00351CA8 File Offset: 0x0034FEA8
			public override void OpenPolicy(NKCPublisherModule.OnComplete dOnClose)
			{
				string @string = NKCStringTable.GetString("SI_STEAM_URL_POLICY", true);
				if (string.IsNullOrEmpty(@string))
				{
					if (dOnClose != null)
					{
						dOnClose(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
					}
					return;
				}
				NKCPublisherModule.Notice.OpenURL(@string, dOnClose);
			}

			// Token: 0x0600AB90 RID: 43920 RVA: 0x00351CE1 File Offset: 0x0034FEE1
			public override string GetCurrencyMark(int productID)
			{
				return "$";
			}

			// Token: 0x0600AB91 RID: 43921 RVA: 0x00351CE8 File Offset: 0x0034FEE8
			public override void OpenPaymentLaw(NKCPublisherModule.OnComplete dOnClose)
			{
				string @string = NKCStringTable.GetString("SI_STEAM_URL_PAYMENT_LAW", true);
				if (string.IsNullOrEmpty(@string))
				{
					if (dOnClose != null)
					{
						dOnClose(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
					}
					return;
				}
				NKCPublisherModule.Notice.OpenURL(@string, dOnClose);
			}

			// Token: 0x0600AB92 RID: 43922 RVA: 0x00351D24 File Offset: 0x0034FF24
			public override void OpenCommercialLaw(NKCPublisherModule.OnComplete dOnClose)
			{
				string @string = NKCStringTable.GetString("SI_STEAM_URL_COMMERCIAL_LAW", true);
				if (string.IsNullOrEmpty(@string))
				{
					if (dOnClose != null)
					{
						dOnClose(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
					}
					return;
				}
				NKCPublisherModule.Notice.OpenURL(@string, dOnClose);
			}

			// Token: 0x0600AB93 RID: 43923 RVA: 0x00351D5D File Offset: 0x0034FF5D
			public override bool ShowPurchasePolicy()
			{
				return false;
			}

			// Token: 0x0600AB94 RID: 43924 RVA: 0x00351D60 File Offset: 0x0034FF60
			public override bool ShowPurchasePolicyBtn()
			{
				return false;
			}

			// Token: 0x0600AB95 RID: 43925 RVA: 0x00351D63 File Offset: 0x0034FF63
			public override bool ShowCashResourceState()
			{
				return NKCDefineManager.DEFINE_SELECT_SERVER() && NKCGameOptionData.LoadLanguageCode(NKM_NATIONAL_CODE.NNC_JAPAN) == NKM_NATIONAL_CODE.NNC_JAPAN;
			}

			// Token: 0x04009FC3 RID: 40899
			private CultureInfo m_cultureInfoForUSD;

			// Token: 0x04009FC4 RID: 40900
			protected Callback<MicroTxnAuthorizationResponse_t> m_MicroTxnAuthorizationResponse;

			// Token: 0x04009FC5 RID: 40901
			protected Callback<GameOverlayActivated_t> m_GameOverlayActivated;

			// Token: 0x04009FC6 RID: 40902
			private string m_payload = "";

			// Token: 0x04009FC7 RID: 40903
			private NKCPublisherModule.OnComplete m_onInappPurchaseComplete;

			// Token: 0x04009FC8 RID: 40904
			private ShopItemTemplet m_requestedShopItemTemplet;

			// Token: 0x04009FC9 RID: 40905
			private Coroutine m_initWaitCoroutine;

			// Token: 0x04009FCA RID: 40906
			private bool m_bOverlayActivated;

			// Token: 0x04009FCB RID: 40907
			private const float INAPP_INIT_TIME = 5f;
		}

		// Token: 0x0200151A RID: 5402
		public class NoticeSteam : NKCPublisherModule.NKCPMNotice
		{
			// Token: 0x0600AB97 RID: 43927 RVA: 0x00351D8A File Offset: 0x0034FF8A
			public override string NoticeUrl(bool bPatcher = false)
			{
				if (NKCDefineManager.DEFINE_SELECT_SERVER())
				{
					return NKCConnectionInfo.GetCurrentLoginServerString("SI_SYSTEM_WEB_NOTICE_URL", true);
				}
				return NKCStringTable.GetString("SI_SYSTEM_WEB_NOTICE_URL", true);
			}

			// Token: 0x0600AB98 RID: 43928 RVA: 0x00351DAA File Offset: 0x0034FFAA
			public override bool CheckOpenNoticeWhenFirstLoginSuccess()
			{
				return NKCMain.m_ranAsSafeMode;
			}

			// Token: 0x0600AB99 RID: 43929 RVA: 0x00351DB8 File Offset: 0x0034FFB8
			public override void OpenCommunity(NKCPublisherModule.OnComplete dOnComplete)
			{
				string text = "https://discord.gg/countersideglobal";
				if (NKCStringTable.CheckExistString("SI_STEAM_URL_COMMUNITY"))
				{
					text = NKCStringTable.GetString("SI_STEAM_URL_COMMUNITY", true);
				}
				Debug.Log("[STEAM] OpenCommunity : " + text);
				NKMPopUpBox.CloseWaitBox();
				Application.OpenURL(text);
				if (dOnComplete != null)
				{
					dOnComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
				}
			}

			// Token: 0x0600AB9A RID: 43930 RVA: 0x00351E0C File Offset: 0x0035000C
			public override void OpenCustomerCenter(NKCPublisherModule.OnComplete dOnComplete)
			{
				string text = "https://counterside.com/?page_id=1278";
				if (NKCStringTable.CheckExistString("SI_STEAM_URL_CUSTOMER_CENTER"))
				{
					text = NKCStringTable.GetString("SI_STEAM_URL_CUSTOMER_CENTER", true);
				}
				Debug.Log("[STEAM] OpenCustomerCenter : " + text);
				NKMPopUpBox.CloseWaitBox();
				Application.OpenURL(text);
				if (dOnComplete != null)
				{
					dOnComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
				}
			}

			// Token: 0x0600AB9B RID: 43931 RVA: 0x00351E5D File Offset: 0x0035005D
			public override bool IsActiveCustomerCenter()
			{
				return true;
			}

			// Token: 0x0600AB9C RID: 43932 RVA: 0x00351E60 File Offset: 0x00350060
			public override void OpenQnA(NKCPublisherModule.OnComplete dOnComplete)
			{
			}

			// Token: 0x0600AB9D RID: 43933 RVA: 0x00351E62 File Offset: 0x00350062
			public override bool CheckOpenNoticeWhenFirstLobbyVisit()
			{
				return true;
			}

			// Token: 0x0600AB9E RID: 43934 RVA: 0x00351E68 File Offset: 0x00350068
			public override void OpenNotice(NKCPublisherModule.OnComplete onComplete)
			{
				if (string.IsNullOrEmpty(this.NoticeUrl(false)) || this.NoticeUrl(false).Equals("SI_SYSTEM_WEB_NOTICE_URL"))
				{
					if (onComplete != null)
					{
						onComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
					}
					return;
				}
				NKCPublisherModule.Notice.OpenURL(this.NoticeUrl(false), onComplete);
			}

			// Token: 0x0600AB9F RID: 43935 RVA: 0x00351EB4 File Offset: 0x003500B4
			public override void OpenPromotionalBanner(NKCPublisherModule.NKCPMNotice.eOptionalBannerPlaces placeType, NKCPublisherModule.OnComplete dOnComplete)
			{
				NKCPMSteamPC.RunFakeProcess(dOnComplete, "OptionalBanner : " + placeType.ToString(), true);
			}

			// Token: 0x0600ABA0 RID: 43936 RVA: 0x00351ED4 File Offset: 0x003500D4
			public override void NotifyMainenance(NKCPublisherModule.OnComplete dOnComplete)
			{
				if (dOnComplete != null)
				{
					dOnComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, null);
				}
			}

			// Token: 0x0600ABA1 RID: 43937 RVA: 0x00351EE1 File Offset: 0x003500E1
			public override void OpenURL(string url, NKCPublisherModule.OnComplete dOnComplete)
			{
				Debug.Log("[STEAM] OpenURL : " + url);
				NKMPopUpBox.CloseWaitBox();
				NKCPopupNoticeWeb.Instance.Open(url, dOnComplete, false);
			}
		}

		// Token: 0x0200151B RID: 5403
		public class ServerInfoSteam : NKCPublisherModule.NKCPMServerInfo
		{
			// Token: 0x0600ABA3 RID: 43939 RVA: 0x00351F0D File Offset: 0x0035010D
			public override bool GetUseLocalSaveLastServerInfoToGetTags()
			{
				return false;
			}

			// Token: 0x0600ABA4 RID: 43940 RVA: 0x00351F10 File Offset: 0x00350110
			public override string GetServerConfigPath()
			{
				string text = UnityEngine.Random.Range(1000000, 8000000).ToString();
				text += UnityEngine.Random.Range(1000000, 8000000).ToString();
				string str = "?p=" + text;
				string str2 = "https://ctsglobal-cdndown.sbside.com/server_config/live/";
				if (NKCDefineManager.DEFINE_GLOBALQA())
				{
					str2 = "http://FileServer.bside.com/server_config/Dev/";
					string customServerInfoAddress = NKCConnectionInfo.CustomServerInfoAddress;
					if (!string.IsNullOrEmpty(customServerInfoAddress))
					{
						str2 = customServerInfoAddress;
					}
				}
				string serverInfoFileName = NKCConnectionInfo.ServerInfoFileName;
				return str2 + serverInfoFileName + str;
			}
		}

		// Token: 0x0200151C RID: 5404
		public class StatisticsNone : NKCPublisherModule.NKCPMStatistics
		{
			// Token: 0x0600ABA6 RID: 43942 RVA: 0x00351F9D File Offset: 0x0035019D
			public override void LogClientActionForPublisher(NKCPublisherModule.NKCPMStatistics.eClientAction funnelPosition, int key = 0, string data = null)
			{
				NKCPMSteamPC.RunFakeProcess(null, string.Format("SendFunnel : {0} {1}", funnelPosition, key), false);
			}

			// Token: 0x0600ABA7 RID: 43943 RVA: 0x00351FBC File Offset: 0x003501BC
			public override void TrackPurchase(int itemID)
			{
				NKCPMSteamPC.RunFakeProcess(null, string.Format("TrackPurchase : {0}", itemID), false);
			}
		}
	}
}
