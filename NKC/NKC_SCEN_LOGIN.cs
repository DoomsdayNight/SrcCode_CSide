using System;
using System.Collections;
using AssetBundles;
using ClientPacket.Account;
using ClientPacket.Community;
using NKC.Advertise;
using NKC.Publisher;
using NKC.Templet;
using NKC.UI;
using NKC.UI.Option;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000722 RID: 1826
	public class NKC_SCEN_LOGIN : NKC_SCEN_BASIC
	{
		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x06004878 RID: 18552 RVA: 0x0015DE55 File Offset: 0x0015C055
		public NKCUILoginBaseMenu LoginBaseMenu
		{
			get
			{
				return this.m_LoginBaseMenu;
			}
		}

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x06004879 RID: 18553 RVA: 0x0015DE5D File Offset: 0x0015C05D
		public NKCUILoginDevMenu LoginDevMenu
		{
			get
			{
				return this.m_LoginDevMenu;
			}
		}

		// Token: 0x0600487A RID: 18554 RVA: 0x0015DE65 File Offset: 0x0015C065
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			if (!NKCUIManager.IsValid(this.m_LoginUIData))
			{
				this.m_LoginUIData = NKCUILoginBaseMenu.OpenNewInstanceAsync();
			}
		}

		// Token: 0x0600487B RID: 18555 RVA: 0x0015DE85 File Offset: 0x0015C085
		public void SetShutdownPopup()
		{
			this.m_bShutdownPopup = true;
		}

		// Token: 0x0600487C RID: 18556 RVA: 0x0015DE8E File Offset: 0x0015C08E
		public void SetErrorCodeForNGS(NKM_ERROR_CODE eNKM_ERROR_CODE)
		{
			this.m_errorCodeForNGS = eNKM_ERROR_CODE;
		}

		// Token: 0x0600487D RID: 18557 RVA: 0x0015DE97 File Offset: 0x0015C097
		public void SetDuplicateConnectPopup()
		{
			this.m_bDuplicateConnect = true;
		}

		// Token: 0x0600487E RID: 18558 RVA: 0x0015DEA0 File Offset: 0x0015C0A0
		public NKC_SCEN_LOGIN()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_LOGIN;
		}

		// Token: 0x0600487F RID: 18559 RVA: 0x0015DEC4 File Offset: 0x0015C0C4
		public override void ScenLoadUIComplete()
		{
			base.ScenLoadUIComplete();
			if (!(this.LoginBaseMenu == null))
			{
				return;
			}
			if (this.m_LoginUIData != null && this.m_LoginUIData.CheckLoadAndGetInstance<NKCUILoginBaseMenu>(out this.m_LoginBaseMenu))
			{
				this.LoginBaseMenu.InitUI();
				this.m_LoginDevMenu = this.LoginBaseMenu.GetLoginDevMenu();
				return;
			}
			Debug.LogError("Error - NKC_SCEN_LOGIN.ScenLoadUIComplete() : UI Load Failed!");
		}

		// Token: 0x06004880 RID: 18560 RVA: 0x0015DF28 File Offset: 0x0015C128
		public override void ScenStart()
		{
			NKCUIManager.SetScreenInputBlock(false);
			if (!NKCDefineManager.DEFINE_ZLONG())
			{
				this.PlayTitleCall();
			}
			base.ScenStart();
			NKCCamera.EnableBloom(true);
			NKCCamera.GetCamera().orthographic = false;
			NKCCamera.GetTrackingPos().SetNowValue(0f, 0f, this.GetCameraDistance());
			NKCCamera.StopTrackingCamera();
			switch (NKCPublisherModule.InitState)
			{
			case NKCPublisherModule.ePublisherInitState.NotInitialized:
				this.TryInitPublisher();
				break;
			case NKCPublisherModule.ePublisherInitState.Maintanance:
				NKCPublisherModule.Notice.NotifyMainenance(delegate(NKC_PUBLISHER_RESULT_CODE x, string add)
				{
					Application.Quit();
				});
				break;
			case NKCPublisherModule.ePublisherInitState.Initialized:
				NKCPublisherModule.DoAfterLogout();
				this.TryLoginToPublisher();
				break;
			}
			NKCAdBase.InitInstance();
			this.LoginBaseMenu.Open();
			NKCScenManager.GetScenManager().DoAfterLogout();
			this.ProcessPopupOnStartScen();
		}

		// Token: 0x06004881 RID: 18561 RVA: 0x0015DFF8 File Offset: 0x0015C1F8
		private void ProcessPopupOnStartScen()
		{
			if (this.m_bShutdownPopup)
			{
				this.m_bShutdownPopup = false;
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_SHUTDOWN_ALARM, delegate()
				{
					NKCMain.QuitGame();
				}, "");
				return;
			}
			if (this.m_errorCodeForNGS != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupOKCancel.OpenOKBox(this.m_errorCodeForNGS, delegate()
				{
					NKCMain.QuitGame();
				}, "");
				this.m_errorCodeForNGS = NKM_ERROR_CODE.NEC_OK;
				return;
			}
			if (this.m_bDuplicateConnect)
			{
				this.m_bDuplicateConnect = false;
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_ERROR_MULTIPLE_CONNECT, delegate()
				{
					NKCMain.QuitGame();
				}, "");
			}
		}

		// Token: 0x06004882 RID: 18562 RVA: 0x0015E0CC File Offset: 0x0015C2CC
		private void PlayTitleCall()
		{
			this.m_fBgmDelay = 0f;
			SoundData playingVoiceData = NKCSoundManager.GetPlayingVoiceData(NKCUIVoiceManager.PlayRandomVoiceInBundle("ab_titlecall_default"));
			if (playingVoiceData != null)
			{
				this.m_fBgmDelay = playingVoiceData.m_AudioSource.clip.length + 0.5f;
			}
			Debug.Log(string.Format("Play Title Call. BgmDelay : {0}", this.m_fBgmDelay));
		}

		// Token: 0x06004883 RID: 18563 RVA: 0x0015E130 File Offset: 0x0015C330
		public override void PlayScenMusic()
		{
			NKCLoginBackgroundTemplet currentBackgroundTemplet = NKCLoginBackgroundTemplet.GetCurrentBackgroundTemplet();
			if (currentBackgroundTemplet == null)
			{
				base.PlayScenMusic();
				return;
			}
			if (AssetBundleManager.IsBundleExists("ab_music/" + currentBackgroundTemplet.m_MusicName.ToLower()))
			{
				NKCSoundManager.PlayMusic(currentBackgroundTemplet.m_MusicName, true, 1f, true, currentBackgroundTemplet.m_MusicStartTime, this.m_fBgmDelay);
				return;
			}
			Debug.LogWarning("playing default login music!");
			NKCSoundManager.PlayMusic("cutscene_login", true, 1f, false, 0f, 0f);
		}

		// Token: 0x06004884 RID: 18564 RVA: 0x0015E1AD File Offset: 0x0015C3AD
		public float GetCameraDistance()
		{
			return -1777.7778f * ((float)Screen.height / (float)Screen.width);
		}

		// Token: 0x06004885 RID: 18565 RVA: 0x0015E1C2 File Offset: 0x0015C3C2
		public override void ScenEnd()
		{
			base.ScenEnd();
			NKCSoundManager.StopAllSound();
			if (this.LoginBaseMenu != null)
			{
				this.LoginBaseMenu.Close();
			}
			this.UnloadUI();
		}

		// Token: 0x06004886 RID: 18566 RVA: 0x0015E1EE File Offset: 0x0015C3EE
		public override void UnloadUI()
		{
			base.UnloadUI();
			NKCUIManager.LoadedUIData loginUIData = this.m_LoginUIData;
			if (loginUIData != null)
			{
				loginUIData.CloseInstance();
			}
			this.m_LoginUIData = null;
			this.m_LoginBaseMenu = null;
			this.m_LoginDevMenu = null;
		}

		// Token: 0x06004887 RID: 18567 RVA: 0x0015E21C File Offset: 0x0015C41C
		public override void ScenUpdate()
		{
			base.ScenUpdate();
			this.m_BloomIntensity.Update(Time.deltaTime);
			if (!this.m_BloomIntensity.IsTracking())
			{
				this.m_BloomIntensity.SetTracking(NKMRandom.Range(1f, 2f), 4f, TRACKING_DATA_TYPE.TDT_SLOWER);
			}
			NKCCamera.SetBloomIntensity(this.m_BloomIntensity.GetNowValue());
			if (this.LoginBaseMenu != null)
			{
				this.LoginBaseMenu.Update();
			}
		}

		// Token: 0x06004888 RID: 18568 RVA: 0x0015E295 File Offset: 0x0015C495
		public override bool ScenMsgProc(NKCMessageData cNKCMessageData)
		{
			return false;
		}

		// Token: 0x06004889 RID: 18569 RVA: 0x0015E298 File Offset: 0x0015C498
		public void OnLoginSuccess(NKMPacket_JOIN_LOBBY_ACK res)
		{
			if (this.LoginDevMenu != null)
			{
				this.LoginDevMenu.SaveIDPass();
			}
			NKCScenManager.GetScenManager().Get_SCEN_EPISODE().SetFirstOpen();
			NKCScenManager.GetScenManager().Get_NKC_SCEN_DIVE().OnLoginSuccess();
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().OnLoginSuccess();
			NKCSynchronizedTime.ForceUpdateTime();
			NKCMailManager.Cleanup();
			NKCMailManager.RefreshMailList();
			NKCShopManager.InvalidateShopItemList();
			NKCEquipPresetDataManager.RequestPresetData(false);
			if (NKCUIGameOption.HasInstance)
			{
				NKCUIGameOption.Instance.RemoveCloseCallBack();
			}
			this.OnLoginSuccessForFriend();
			this.OnLoginSuccessForMission();
			this.OnLoginSuccessForAttendance();
			this.OnLoginSuccessForDive();
			this.OnLoginSuccessForOptions();
			this.OnLoginSuccessForChat();
			this.OnLoginSuccessForOperationFavorite();
			NKCPublisherModule.Statistics.OnLoginSuccessToCS(res);
		}

		// Token: 0x0600488A RID: 18570 RVA: 0x0015E34C File Offset: 0x0015C54C
		private void OnLoginSuccessForDive()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null && nkmuserData.m_DiveGameData != null)
			{
				nkmuserData.m_DiveGameData.Floor.OnPacketRead();
			}
		}

		// Token: 0x0600488B RID: 18571 RVA: 0x0015E37C File Offset: 0x0015C57C
		private void OnLoginSuccessForMission()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null && myUserData.m_MissionData != null)
			{
				NKMMissionManager.SetHaveClearedMission(myUserData.m_MissionData.CheckCompletableMission(myUserData, true), true);
				NKMMissionManager.SetHaveClearedMissionGuide(myUserData.m_MissionData.CheckCompletableGuideMission(myUserData, true), true);
			}
		}

		// Token: 0x0600488C RID: 18572 RVA: 0x0015E3C8 File Offset: 0x0015C5C8
		private void OnLoginSuccessForFriend()
		{
			NKCFriendManager.Initialize();
			NKC_SCEN_HOME scen_HOME = NKCScenManager.GetScenManager().Get_SCEN_HOME();
			if (scen_HOME != null)
			{
				scen_HOME.SetFriendNewIcon(false);
			}
			NKCPacketSender.Send_NKMPacket_FRIEND_LIST_REQ(NKM_FRIEND_LIST_TYPE.RECEIVE_REQUEST);
			NKCPacketSender.Send_NKMPacket_FRIEND_LIST_REQ(NKM_FRIEND_LIST_TYPE.FRIEND);
			NKCPacketSender.Send_NKMPacket_FRIEND_LIST_REQ(NKM_FRIEND_LIST_TYPE.BLOCKER);
			NKMPacket_GREETING_MESSAGE_REQ packet = new NKMPacket_GREETING_MESSAGE_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600488D RID: 18573 RVA: 0x0015E41C File Offset: 0x0015C61C
		private void OnLoginSuccessForAttendance()
		{
			NKC_SCEN_HOME scen_HOME = NKCScenManager.GetScenManager().Get_SCEN_HOME();
			if (scen_HOME != null)
			{
				NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
				if (myUserData != null && myUserData.m_MissionData != null)
				{
					scen_HOME.SetAttendanceRequired(NKMAttendanceManager.CheckNeedAttendance(myUserData.m_AttendanceData, NKCSynchronizedTime.GetServerUTCTime(0.0), 0));
				}
			}
		}

		// Token: 0x0600488E RID: 18574 RVA: 0x0015E46D File Offset: 0x0015C66D
		private void OnLoginSuccessForOptions()
		{
			NKCPublisherModule.Push.UpdateAllLocalPush();
		}

		// Token: 0x0600488F RID: 18575 RVA: 0x0015E479 File Offset: 0x0015C679
		private void OnLoginSuccessForChat()
		{
			if (NKCGuildManager.MyData != null && NKCGuildManager.MyData.guildUid > 0L)
			{
				NKCPacketSender.Send_NKMPacket_GUILD_CHAT_LIST_REQ(NKCGuildManager.MyData.guildUid);
			}
		}

		// Token: 0x06004890 RID: 18576 RVA: 0x0015E49F File Offset: 0x0015C69F
		private void OnLoginSuccessForOperationFavorite()
		{
			NKCPacketSender.Send_NKMPacket_FAVORITES_STAGE_REQ();
		}

		// Token: 0x06004891 RID: 18577 RVA: 0x0015E4A6 File Offset: 0x0015C6A6
		public void UpdateLoginMsgUI()
		{
			if (this.LoginBaseMenu != null)
			{
				this.LoginBaseMenu.UpdateLoginMsgUI();
			}
		}

		// Token: 0x06004892 RID: 18578 RVA: 0x0015E4C4 File Offset: 0x0015C6C4
		public void TryLogin()
		{
			if (NKCPublisherModule.Auth.LoginToPublisherCompleted)
			{
				Debug.Log(string.Format("TryLogin : {0}:{1}", this.LoginBaseMenu.GetCurrentServerAddress(), this.LoginBaseMenu.GetCurrentServerPort()));
				NKCConnectLogin connectLogin = NKCScenManager.GetScenManager().GetConnectLogin();
				connectLogin.SetRemoteAddress(this.LoginBaseMenu.GetCurrentServerAddress(), this.LoginBaseMenu.GetCurrentServerPort());
				connectLogin.ResetConnection();
				NKMPopUpBox.OpenWaitBox(0f, "");
				NKCScenManager.GetScenManager().VersionCheck(delegate
				{
					NKCPublisherModule.Auth.PrepareCSLogin(new NKCPublisherModule.OnComplete(this.OnLoginReady));
				});
				return;
			}
			this.TryLoginToPublisher();
		}

		// Token: 0x06004893 RID: 18579 RVA: 0x0015E55E File Offset: 0x0015C75E
		private void OnLoginReady(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
		{
			if (!NKCPublisherModule.CheckError(resultCode, additionalError, true, null, false))
			{
				return;
			}
			NKCPublisherModule.Statistics.LogClientAction(NKCPublisherModule.NKCPMStatistics.eClientAction.TryLoginToGameServer, 0, null);
			NKCScenManager.GetScenManager().OnLoginReady();
		}

		// Token: 0x06004894 RID: 18580 RVA: 0x0015E584 File Offset: 0x0015C784
		public NKM_USER_AUTH_LEVEL GetAuthLevel()
		{
			if (this.LoginDevMenu != null)
			{
				return this.LoginDevMenu.AuthLevel;
			}
			return NKM_USER_AUTH_LEVEL.NORMAL_USER;
		}

		// Token: 0x06004895 RID: 18581 RVA: 0x0015E5A1 File Offset: 0x0015C7A1
		private void TryInitPublisher()
		{
			NKMPopUpBox.OpenWaitBox(0f, "");
			NKCPublisherModule.InitInstance(new NKCPublisherModule.OnComplete(this.OnInitComplete));
		}

		// Token: 0x06004896 RID: 18582 RVA: 0x0015E5C4 File Offset: 0x0015C7C4
		private void OnInitComplete(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
		{
			NKMPopUpBox.CloseWaitBox();
			if (resultCode == NKC_PUBLISHER_RESULT_CODE.NPRC_OK)
			{
				this.TryLoginToPublisher();
				return;
			}
			if (resultCode == NKC_PUBLISHER_RESULT_CODE.NPRC_MAINTENANCE)
			{
				NKCPublisherModule.Notice.NotifyMainenance(delegate(NKC_PUBLISHER_RESULT_CODE x, string add)
				{
					Application.Quit();
				});
				return;
			}
			if (resultCode != NKC_PUBLISHER_RESULT_CODE.NPRC_STEAM_INITIALIZE_FAIL)
			{
				NKCPopupOKCancel.OpenOKBox(resultCode, additionalError, new NKCPopupOKCancel.OnButton(this.TryInitPublisher), NKCStringTable.GetString("SI_DP_TOY_RE_TRY_TITLE", false));
				return;
			}
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_STEAM_INITIALIZE), delegate()
			{
				NKCMain.QuitGame();
			}, "");
		}

		// Token: 0x06004897 RID: 18583 RVA: 0x0015E66F File Offset: 0x0015C86F
		private void TryLoginToPublisher()
		{
			Debug.Log("TryLoginToPublisher");
			NKMPopUpBox.OpenWaitBox(0f, "");
			NKCPublisherModule.Auth.LoginToPublisher(new NKCPublisherModule.OnComplete(this.OnLoginToPublisherComplete));
		}

		// Token: 0x06004898 RID: 18584 RVA: 0x0015E6A0 File Offset: 0x0015C8A0
		private IEnumerator DelayedOpenNotice()
		{
			NKMPopUpBox.OpenWaitBox(0f, "");
			this.m_fTimeToOpenNotice = Time.time + 0.5f;
			while (this.m_fTimeToOpenNotice > Time.time)
			{
				yield return null;
			}
			NKCPublisherModule.Statistics.LogClientAction(NKCPublisherModule.NKCPMStatistics.eClientAction.Login_ShowNotice, 0, null);
			NKCPublisherModule.Notice.OpenNotice(delegate(NKC_PUBLISHER_RESULT_CODE eNKC_PUBLISHER_RESULT_CODE, string additionalError)
			{
				NKMPopUpBox.CloseWaitBox();
				NKCPublisherModule.Auth.OpenCertification();
			});
			yield break;
		}

		// Token: 0x06004899 RID: 18585 RVA: 0x0015E6B0 File Offset: 0x0015C8B0
		private void OnLoginToPublisherComplete(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
		{
			Debug.Log("OnLoginToPublisherComplete");
			NKMPopUpBox.CloseWaitBox();
			switch (resultCode)
			{
			case NKC_PUBLISHER_RESULT_CODE.NPRC_OK:
				NKCPublisherModule.Permission.RequestAppTrackingPermission();
				goto IL_D7;
			case NKC_PUBLISHER_RESULT_CODE.NPRC_BUSY:
				break;
			case NKC_PUBLISHER_RESULT_CODE.NPRC_MAINTENANCE:
				NKCPublisherModule.Notice.NotifyMainenance(delegate(NKC_PUBLISHER_RESULT_CODE x, string add)
				{
					Application.Quit();
				});
				return;
			case NKC_PUBLISHER_RESULT_CODE.NPRC_TIMEOUT:
				Debug.Log("Login TimeOut. Can be normal state : skip error popup");
				return;
			default:
				if (resultCode == NKC_PUBLISHER_RESULT_CODE.NPRC_AUTH_LOGIN_QUIT_USER)
				{
					NKMPopUpBox.OpenWaitBox(0f, "");
					NKCPublisherModule.Auth.TryRestoreQuitUser(new NKCPublisherModule.OnComplete(this.OnQuitUserRestore));
					return;
				}
				if (resultCode == NKC_PUBLISHER_RESULT_CODE.NPRC_AUTH_LOGIN_USER_RESOLVE_REQUIRED)
				{
					NKMPopUpBox.OpenWaitBox(0f, "");
					NKCPublisherModule.Auth.TryResolveUser(new NKCPublisherModule.OnComplete(this.OnSyncResolve));
					return;
				}
				break;
			}
			NKCPopupOKCancel.OpenOKBox(resultCode, additionalError, null, "");
			IL_D7:
			if (this.LoginBaseMenu != null)
			{
				this.LoginBaseMenu.AfterLoginToPublisherCompleted(resultCode, additionalError);
			}
			if (this.m_bFirstSuccessLoginToPublisher)
			{
				this.m_bFirstSuccessLoginToPublisher = false;
				if (NKCPublisherModule.Notice.CheckOpenNoticeWhenFirstLoginSuccess() && this.LoginBaseMenu != null)
				{
					NKCUtil.SetGameobjectActive(this.LoginBaseMenu, true);
					this.LoginBaseMenu.StartCoroutine(this.DelayedOpenNotice());
				}
			}
		}

		// Token: 0x0600489A RID: 18586 RVA: 0x0015E7F6 File Offset: 0x0015C9F6
		private void OnSyncResolve(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
		{
			if (!NKCPublisherModule.CheckError(resultCode, additionalError, true, new NKCPopupOKCancel.OnButton(this.ReturnLogin), false))
			{
				return;
			}
			this.TryLoginToPublisher();
		}

		// Token: 0x0600489B RID: 18587 RVA: 0x0015E816 File Offset: 0x0015CA16
		private void OnQuitUserRestore(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
		{
			if (!NKCPublisherModule.CheckError(resultCode, additionalError, true, new NKCPopupOKCancel.OnButton(this.ReturnLogin), false))
			{
				return;
			}
			this.TryLoginToPublisher();
		}

		// Token: 0x0600489C RID: 18588 RVA: 0x0015E836 File Offset: 0x0015CA36
		private void ReturnLogin()
		{
			NKMPopUpBox.CloseWaitBox();
			NKCScenManager.GetScenManager().GetConnectLogin().ResetConnection();
			NKCScenManager.GetScenManager().GetConnectGame().ResetConnection();
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_LOGIN, true);
		}

		// Token: 0x04003842 RID: 14402
		private NKCUIManager.LoadedUIData m_LoginUIData;

		// Token: 0x04003843 RID: 14403
		private NKCUILoginBaseMenu m_LoginBaseMenu;

		// Token: 0x04003844 RID: 14404
		private NKCUILoginDevMenu m_LoginDevMenu;

		// Token: 0x04003845 RID: 14405
		private NKMTrackingFloat m_BloomIntensity = new NKMTrackingFloat();

		// Token: 0x04003846 RID: 14406
		private const float CONST_CAMERA_DISTANCE = -1777.7778f;

		// Token: 0x04003847 RID: 14407
		private bool m_bFirstSuccessLoginToPublisher = true;

		// Token: 0x04003848 RID: 14408
		private bool m_bShutdownPopup;

		// Token: 0x04003849 RID: 14409
		private NKM_ERROR_CODE m_errorCodeForNGS;

		// Token: 0x0400384A RID: 14410
		private bool m_bDuplicateConnect;

		// Token: 0x0400384B RID: 14411
		private const float DEFAULT_DELAY_TITLECALL_END = 0.5f;

		// Token: 0x0400384C RID: 14412
		private float m_fBgmDelay;

		// Token: 0x0400384D RID: 14413
		private float m_fTimeToOpenNotice;
	}
}
