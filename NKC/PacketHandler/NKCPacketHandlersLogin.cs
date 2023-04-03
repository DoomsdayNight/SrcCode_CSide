using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ClientPacket.Account;
using Cs.Engine.Util;
using Cs.Logging;
using NKC.Publisher;
using NKC.UI;
using NKM;
using UnityEngine;

namespace NKC.PacketHandler
{
	// Token: 0x0200089F RID: 2207
	public static class NKCPacketHandlersLogin
	{
		// Token: 0x060059E2 RID: 23010 RVA: 0x001B497E File Offset: 0x001B2B7E
		public static void OnRecv(NKMPacket_LOGIN_ACK lPacket)
		{
			NKMPopUpBox.CloseWaitBox();
			NKCPacketHandlersLogin.OnLogin(lPacket.errorCode, lPacket.accessToken, lPacket.gameServerIP, lPacket.gameServerPort, lPacket.contentsVersion, lPacket.contentsTag, lPacket.openTag, int.MinValue);
		}

		// Token: 0x060059E3 RID: 23011 RVA: 0x001B49B9 File Offset: 0x001B2BB9
		public static void OnRecv(NKMPacket_ZLONG_LOGIN_ACK lPacket)
		{
			NKMPopUpBox.CloseWaitBox();
			NKCPacketHandlersLogin.OnLogin(lPacket.errorCode, lPacket.accessToken, lPacket.gameServerIP, lPacket.gameServerPort, lPacket.contentsVersion, lPacket.contentsTag, lPacket.openTag, lPacket.status);
		}

		// Token: 0x060059E4 RID: 23012 RVA: 0x001B49F8 File Offset: 0x001B2BF8
		public static void OnRecv(NKMPacket_GAMEBASE_LOGIN_ACK lPacket)
		{
			NKMPopUpBox.CloseWaitBox();
			if (lPacket.errorCode == NKM_ERROR_CODE.NEC_FAIL_UNDER_MAINTENANCE)
			{
				NKC_SCEN_LOGIN scen_LOGIN = NKCScenManager.GetScenManager().Get_SCEN_LOGIN();
				if (scen_LOGIN != null)
				{
					scen_LOGIN.LoginBaseMenu.SetLoginTouchDelay();
				}
			}
			NKCPacketHandlersLogin.OnLogin(lPacket.errorCode, lPacket.accessToken, lPacket.gameServerIP, lPacket.gameServerPort, lPacket.contentsVersion, lPacket.contentsTag, lPacket.openTag, lPacket.resultCode);
		}

		// Token: 0x060059E5 RID: 23013 RVA: 0x001B4A68 File Offset: 0x001B2C68
		public static void OnRecv(NKMPacket_RECONNECT_ACK ack)
		{
			NKMPopUpBox.CloseWaitBox();
			if (ack.errorCode != NKM_ERROR_CODE.NEC_OK)
			{
				NKCScenManager.GetScenManager().GetConnectGame().SetReconnectKey("");
			}
			if (ack.errorCode != NKM_ERROR_CODE.NEC_FAIL_RECONNECT_PRESENCE_NOT_FOUND || !NKCPublisherModule.Auth.IsTryAuthWhenSessionExpired())
			{
				NKCPacketHandlersLogin.OnReconnect(ack.errorCode, ack.accessToken, ack.gameServerIp, ack.gameServerPort, ack.contentsVersion, ack.contentsTag, ack.openTag);
				return;
			}
			if (NKCDefineManager.DEFINE_SB_GB())
			{
				NKCPacketHandlersLogin.OnLogoutComplete(NKC_PUBLISHER_RESULT_CODE.NPRC_OK, string.Empty);
				return;
			}
			NKCPublisherModule.Auth.Logout(new NKCPublisherModule.OnComplete(NKCPacketHandlersLogin.OnLogoutComplete));
		}

		// Token: 0x060059E6 RID: 23014 RVA: 0x001B4B08 File Offset: 0x001B2D08
		private static void OnLogoutByGamebase(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
		{
			if (!string.IsNullOrEmpty(additionalError))
			{
				Debug.Log("### additionalError : " + additionalError);
			}
			if (resultCode == NKC_PUBLISHER_RESULT_CODE.NPRC_OK)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_TOY_LOGOUT_SUCCESS, new NKCPopupOKCancel.OnButton(NKCPacketHandlersLogin.<OnLogoutByGamebase>g__ResetConnection|4_0), "");
				return;
			}
			Debug.LogWarning("OnLogoutByGamebase => Logout fail");
		}

		// Token: 0x060059E7 RID: 23015 RVA: 0x001B4B5B File Offset: 0x001B2D5B
		private static void OnLogoutComplete(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
		{
			if (!NKCPacketHandlersLogin.Process_TryAuthFailedWhenSessionExpired(resultCode, additionalError))
			{
				return;
			}
			NKCPublisherModule.Auth.LoginToPublisher(new NKCPublisherModule.OnComplete(NKCPacketHandlersLogin.OnSyncAccountComplete));
		}

		// Token: 0x060059E8 RID: 23016 RVA: 0x001B4B7D File Offset: 0x001B2D7D
		private static void OnSyncAccountComplete(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
		{
			if (!NKCPacketHandlersLogin.Process_TryAuthFailedWhenSessionExpired(resultCode, additionalError))
			{
				return;
			}
			NKCPublisherModule.Auth.PrepareCSLogin(new NKCPublisherModule.OnComplete(NKCPacketHandlersLogin.OnLoginReady));
		}

		// Token: 0x060059E9 RID: 23017 RVA: 0x001B4B9F File Offset: 0x001B2D9F
		private static void OnLoginReady(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
		{
			if (!NKCPacketHandlersLogin.Process_TryAuthFailedWhenSessionExpired(resultCode, additionalError))
			{
				return;
			}
			NKCScenManager.GetScenManager().OnLoginReady();
		}

		// Token: 0x060059EA RID: 23018 RVA: 0x001B4BB8 File Offset: 0x001B2DB8
		private static bool Process_TryAuthFailedWhenSessionExpired(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
		{
			if (resultCode == NKC_PUBLISHER_RESULT_CODE.NPRC_OK)
			{
				return true;
			}
			Debug.LogWarningFormat("ProcessReconnectFail. result:{0}", new object[]
			{
				resultCode
			});
			NKCScenManager.GetScenManager().GetConnectLogin().ResetConnection();
			NKCScenManager.GetScenManager().GetConnectGame().ResetConnection();
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_LOGIN)
			{
				NKCPopupOKCancel.OpenOKBox(resultCode, additionalError, null, "");
			}
			else
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_DECONNECT_AND_GO_TITLE, delegate()
				{
					NKCPublisherModule.Auth.ResetConnection();
					NKCScenManager.GetScenManager().GetConnectLogin().ResetConnection();
					NKCScenManager.GetScenManager().GetConnectGame().ResetConnection();
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_LOGIN, true);
				}, "");
			}
			return false;
		}

		// Token: 0x060059EB RID: 23019 RVA: 0x001B4C54 File Offset: 0x001B2E54
		private static void OnReconnectFailComplete(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
		{
			if (resultCode != NKC_PUBLISHER_RESULT_CODE.NPRC_OK)
			{
				NKCPopupOKCancel.OpenOKBox(resultCode, additionalError, null, "");
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_LOGIN)
			{
				NKCPopupOKCancel.OpenOKBox(NKCPacketHandlersLogin.s_LastReconnectFailErrorCode, null, "");
				return;
			}
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_DECONNECT_AND_GO_TITLE, delegate()
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_LOGIN, true);
			}, "");
		}

		// Token: 0x060059EC RID: 23020 RVA: 0x001B4CC4 File Offset: 0x001B2EC4
		private static void OnReconnect(NKM_ERROR_CODE errorCode, string accessToken, string gameServerIP, int gameServerPort, string contentsVersion, IReadOnlyList<string> contentsTagList, IReadOnlyList<string> openTagList)
		{
			if (errorCode != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPacketHandlersLogin.s_LastReconnectFailErrorCode = errorCode;
				Debug.LogWarningFormat("OnReconnect failed. result:{0}", new object[]
				{
					errorCode
				});
				NKCScenManager.GetScenManager().GetConnectLogin().ResetConnection();
				NKCScenManager.GetScenManager().GetConnectGame().ResetConnection();
				NKCPublisherModule.Auth.OnReconnectFail(new NKCPublisherModule.OnComplete(NKCPacketHandlersLogin.OnReconnectFailComplete));
				return;
			}
			NKCPacketHandlersLogin.OnLoginSuccess(accessToken, gameServerIP, gameServerPort, contentsVersion, contentsTagList, openTagList);
		}

		// Token: 0x060059ED RID: 23021 RVA: 0x001B4D38 File Offset: 0x001B2F38
		private static void OnLoginSuccess(string accessToken, string gameServerIP, int gameServerPort, string contentsVersion, IReadOnlyList<string> contentsTagList, IReadOnlyList<string> openTagList)
		{
			bool flag = NKCContentsVersionManager.CheckSameTagList(contentsTagList);
			Log.Info(string.Format("OnLoginSucces LocalCV[{0}] -> LoginCV[{1}], CheckSameTag[{2}]", NKMContentsVersionManager.CurrentVersion.Literal, contentsVersion, flag), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCPacketHandlersLogin.cs", 242);
			if (NKCDefineManager.DEFINE_CHECKVERSION() && NKCMain.m_ranAsSafeMode)
			{
				flag = false;
				Log.Info(string.Format("OnLoginSucces RanAsSafeMode VersionAckReceived[{0}]", ContentsVersionChecker.VersionAckReceived), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCPacketHandlersLogin.cs", 248);
			}
			if (NKMContentsVersionManager.CurrentVersion.Literal != contentsVersion || !flag)
			{
				PlayerPrefs.SetString("LOCAL_SAVE_CONTENTS_VERSION_KEY", contentsVersion);
				NKCContentsVersionManager.SetTagList(contentsTagList);
				NKCContentsVersionManager.SaveTagToLocal();
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_CONTENTS_VERSION_CHANGE, new NKCPopupOKCancel.OnButton(NKCScenManager.GetScenManager().MoveToPatchScene), "");
				return;
			}
			NKMOpenTagManager.SetTagList(openTagList);
			foreach (NKMPotentialOptionGroupTemplet nkmpotentialOptionGroupTemplet in NKMPotentialOptionGroupTemplet.Values)
			{
				nkmpotentialOptionGroupTemplet.Validate();
			}
			NKCScenManager.GetScenManager().SetAppEnableConnectCheckTime(-1f, true);
			NKCScenManager.GetScenManager().GetConnectLogin().LoginComplete();
			NKCAdjustManager.OnCustomEvent("06_login_complete");
			Debug.LogFormat("target server " + gameServerIP, Array.Empty<object>());
			NKCConnectGame connectGame = NKCScenManager.GetScenManager().GetConnectGame();
			connectGame.SetRemoteAddress(gameServerIP, gameServerPort);
			connectGame.SetAccessToken(accessToken);
			NKCScenManager.GetScenManager().GetConnectLogin().ResetConnection();
			connectGame.ResetConnection();
			connectGame.ConnectToLobbyServer();
		}

		// Token: 0x060059EE RID: 23022 RVA: 0x001B4EB0 File Offset: 0x001B30B0
		private static void OnLogin(NKM_ERROR_CODE errorCode, string accessToken, string gameServerIP, int gameServerPort, string contentsVersion, IReadOnlyList<string> contentsTagList, IReadOnlyList<string> openTagList, int status = -2147483648)
		{
			if (errorCode != NKM_ERROR_CODE.NEC_OK)
			{
				Debug.LogWarningFormat("Login failed. result:{0}", new object[]
				{
					errorCode
				});
				NKCScenManager.GetScenManager().GetConnectLogin().ResetConnection();
				NKCScenManager.GetScenManager().GetConnectGame().ResetConnection();
				NKCPublisherModule.Auth.ResetConnection();
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_LOGIN)
				{
					NKCPacketHandlersLogin.OpenLoginErrorPopup(errorCode, status);
					NKC_SCEN_LOGIN scen_LOGIN = NKCScenManager.GetScenManager().Get_SCEN_LOGIN();
					if (scen_LOGIN != null)
					{
						scen_LOGIN.UpdateLoginMsgUI();
						return;
					}
				}
				else
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_DECONNECT_AND_GO_TITLE, delegate()
					{
						NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_LOGIN, true);
					}, "");
				}
				return;
			}
			NKCPacketHandlersLogin.OnLoginSuccess(accessToken, gameServerIP, gameServerPort, contentsVersion, contentsTagList, openTagList);
		}

		// Token: 0x060059EF RID: 23023 RVA: 0x001B4F70 File Offset: 0x001B3170
		public static void OpenLoginErrorPopup(NKM_ERROR_CODE errorCode, int extraStatus)
		{
			if (NKCPublisherModule.PublisherType == NKCPublisherModule.ePublisherType.Zlong)
			{
				if (errorCode == NKM_ERROR_CODE.NEC_FAIL_ZLONG_LOGIN_INVALID_STATUS)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCStringTable.GetString(errorCode.ToString() + "_" + extraStatus.ToString(), false), null, "");
					return;
				}
				NKCPopupOKCancel.OpenOKBox(errorCode, null, "");
				return;
			}
			else
			{
				if (NKCPublisherModule.PublisherType == NKCPublisherModule.ePublisherType.SB_Gamebase || NKCPublisherModule.PublisherType == NKCPublisherModule.ePublisherType.STEAM)
				{
					NKCPopupOKCancel.OpenOKBox(errorCode, delegate()
					{
						NKCPublisherModule.Notice.OpenNotice(null);
					}, "");
					return;
				}
				NKCPopupOKCancel.OpenOKBox(errorCode, null, "");
				return;
			}
		}

		// Token: 0x060059F1 RID: 23025 RVA: 0x001B5018 File Offset: 0x001B3218
		[CompilerGenerated]
		internal static void <OnLogoutByGamebase>g__ResetConnection|4_0()
		{
			NKCScenManager.GetScenManager().GetConnectLogin().ResetConnection();
			NKCScenManager.GetScenManager().GetConnectGame().ResetConnection();
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_LOGIN, true);
		}

		// Token: 0x04004574 RID: 17780
		private static NKM_ERROR_CODE s_LastReconnectFailErrorCode;
	}
}
