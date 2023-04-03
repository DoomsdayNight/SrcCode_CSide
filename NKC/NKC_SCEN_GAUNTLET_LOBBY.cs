using System;
using System.Collections.Generic;
using ClientPacket.Pvp;
using NKC.UI;
using NKC.UI.Gauntlet;
using NKM;
using UnityEngine.Video;

namespace NKC
{
	// Token: 0x02000715 RID: 1813
	public class NKC_SCEN_GAUNTLET_LOBBY : NKC_SCEN_BASIC
	{
		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x06004796 RID: 18326 RVA: 0x0015B1CF File Offset: 0x001593CF
		public NKCUIGauntletLobby GauntletLobby
		{
			get
			{
				return this.m_NKCUIGauntletLobby;
			}
		}

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x06004797 RID: 18327 RVA: 0x0015B1D7 File Offset: 0x001593D7
		public NKCUIGauntletLobbyCustom GauntletLobbyCustom
		{
			get
			{
				return this.m_NKCUIGauntletLobby.m_NKCUIGauntletLobbyCustom;
			}
		}

		// Token: 0x06004798 RID: 18328 RVA: 0x0015B1E4 File Offset: 0x001593E4
		public void SetReserved_NKM_ERROR_CODE(NKM_ERROR_CODE eNKM_ERROR_CODE)
		{
			this.m_Reserved_NKM_ERROR_CODE = eNKM_ERROR_CODE;
		}

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x06004799 RID: 18329 RVA: 0x0015B1F0 File Offset: 0x001593F0
		public NKCPopupGauntletOutgameReward NKCPopupGauntletOutgameReward
		{
			get
			{
				if (this.m_NKCPopupGauntletOutgameReward == null)
				{
					NKCUIManager.LoadedUIData loadedUIData = NKCUIManager.OpenNewInstance<NKCPopupGauntletOutgameReward>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_RANK_REWARD_POPUP", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), delegate()
					{
						this.m_NKCPopupGauntletOutgameReward = null;
					});
					this.m_NKCPopupGauntletOutgameReward = loadedUIData.GetInstance<NKCPopupGauntletOutgameReward>();
					NKCPopupGauntletOutgameReward nkcpopupGauntletOutgameReward = this.m_NKCPopupGauntletOutgameReward;
					if (nkcpopupGauntletOutgameReward != null)
					{
						nkcpopupGauntletOutgameReward.InitUI();
					}
				}
				return this.m_NKCPopupGauntletOutgameReward;
			}
		}

		// Token: 0x0600479A RID: 18330 RVA: 0x0015B250 File Offset: 0x00159450
		private void CheckNKCPopupGauntletOutgameRewardAndClose()
		{
			if (this.m_NKCPopupGauntletOutgameReward != null && this.m_NKCPopupGauntletOutgameReward.IsOpen)
			{
				this.m_NKCPopupGauntletOutgameReward.Close();
			}
		}

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x0600479B RID: 18331 RVA: 0x0015B278 File Offset: 0x00159478
		public NKCPopupGauntletNewSeasonAlarm NKCPopupGauntletNewSeasonAlarm
		{
			get
			{
				if (this.m_NKCPopupGauntletNewSeasonAlarm == null)
				{
					NKCUIManager.LoadedUIData loadedUIData = NKCUIManager.OpenNewInstance<NKCPopupGauntletNewSeasonAlarm>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_RANK_NEWSEASON_POPUP", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), delegate()
					{
						this.m_NKCPopupGauntletNewSeasonAlarm = null;
					});
					this.m_NKCPopupGauntletNewSeasonAlarm = loadedUIData.GetInstance<NKCPopupGauntletNewSeasonAlarm>();
					NKCPopupGauntletNewSeasonAlarm nkcpopupGauntletNewSeasonAlarm = this.m_NKCPopupGauntletNewSeasonAlarm;
					if (nkcpopupGauntletNewSeasonAlarm != null)
					{
						nkcpopupGauntletNewSeasonAlarm.InitUI();
					}
				}
				return this.m_NKCPopupGauntletNewSeasonAlarm;
			}
		}

		// Token: 0x0600479C RID: 18332 RVA: 0x0015B2D8 File Offset: 0x001594D8
		private void CheckNKCPopupGauntletNewSeasonAlarmAndClose()
		{
			if (this.m_NKCPopupGauntletNewSeasonAlarm != null && this.m_NKCPopupGauntletNewSeasonAlarm.IsOpen)
			{
				this.m_NKCPopupGauntletNewSeasonAlarm.Close();
			}
		}

		// Token: 0x0600479D RID: 18333 RVA: 0x0015B300 File Offset: 0x00159500
		public NKC_SCEN_GAUNTLET_LOBBY()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_GAUNTLET_LOBBY;
		}

		// Token: 0x0600479E RID: 18334 RVA: 0x0015B322 File Offset: 0x00159522
		public void SetLatestRANK_TYPE(RANK_TYPE eRANK_TYPE)
		{
			this.m_Latest_RANK_TYPE = eRANK_TYPE;
		}

		// Token: 0x0600479F RID: 18335 RVA: 0x0015B32B File Offset: 0x0015952B
		public void SetReservedLobbyTab(NKC_GAUNTLET_LOBBY_TAB eNKC_GAUNTLET_LOBBY_TAB)
		{
			this.m_NKC_GAUNTLET_LOBBY_TAB = eNKC_GAUNTLET_LOBBY_TAB;
		}

		// Token: 0x060047A0 RID: 18336 RVA: 0x0015B334 File Offset: 0x00159534
		public void SetReservedAsyncTab(NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE eAsyncTab = NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.MAX)
		{
			this.m_lastest_AsyncTab = eAsyncTab;
		}

		// Token: 0x060047A1 RID: 18337 RVA: 0x0015B33D File Offset: 0x0015953D
		public RANK_TYPE GetCurrRankType()
		{
			if (this.m_NKCUIGauntletLobby == null)
			{
				return RANK_TYPE.COUNT;
			}
			return this.m_NKCUIGauntletLobby.GetCurrRankType();
		}

		// Token: 0x060047A2 RID: 18338 RVA: 0x0015B35A File Offset: 0x0015955A
		public NKC_GAUNTLET_LOBBY_TAB GetCurrentLobbyTab()
		{
			if (this.m_NKCUIGauntletLobby == null)
			{
				return NKC_GAUNTLET_LOBBY_TAB.NGLT_RANK;
			}
			return this.m_NKCUIGauntletLobby.GetCurrentLobbyTab();
		}

		// Token: 0x060047A3 RID: 18339 RVA: 0x0015B377 File Offset: 0x00159577
		public void DoAfterLogout()
		{
			this.SetLatestRANK_TYPE(RANK_TYPE.MY_LEAGUE);
			this.m_Reserved_NKM_ERROR_CODE = NKM_ERROR_CODE.NEC_OK;
			this.m_bWaitForEmoticon = false;
		}

		// Token: 0x060047A4 RID: 18340 RVA: 0x0015B38E File Offset: 0x0015958E
		public bool IsWaitForEmoticon()
		{
			return this.m_bWaitForEmoticon;
		}

		// Token: 0x060047A5 RID: 18341 RVA: 0x0015B396 File Offset: 0x00159596
		public void SetWaitForEmoticon(bool bValue)
		{
			this.m_bWaitForEmoticon = bValue;
		}

		// Token: 0x060047A6 RID: 18342 RVA: 0x0015B3A0 File Offset: 0x001595A0
		public void OnLoginSuccess()
		{
			NKCUIGauntletLobbyRank.SetAlertDemotion(false);
			NKCUIGauntletLobbyLeague.SetAlertDemotion(false);
			if (this.asyncUserUID != NKCScenManager.CurrentUserData().m_UserUID)
			{
				NKC_SCEN_GAUNTLET_LOBBY.AsyncRefreshCooltime = 0f;
				this.AsyncTargetList.Clear();
				this.asyncUserUID = NKCScenManager.CurrentUserData().m_UserUID;
			}
		}

		// Token: 0x060047A7 RID: 18343 RVA: 0x0015B3F0 File Offset: 0x001595F0
		public void ClearCacheData()
		{
			if (this.m_NKCUIGauntletLobby != null)
			{
				this.m_NKCUIGauntletLobby.CloseInstance();
				this.m_NKCUIGauntletLobby = null;
			}
			if (this.m_NKCPopupGauntletBattleRecord != null)
			{
				this.m_NKCPopupGauntletBattleRecord.CloseInstance();
				this.m_NKCPopupGauntletBattleRecord = null;
			}
		}

		// Token: 0x060047A8 RID: 18344 RVA: 0x0015B43D File Offset: 0x0015963D
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			NKCCollectionManager.Init();
			if (this.m_NKCUIGauntletLobby == null)
			{
				this.m_UILoadResourceData = NKCUIGauntletLobby.OpenInstanceAsync();
				return;
			}
			this.m_UILoadResourceData = null;
		}

		// Token: 0x060047A9 RID: 18345 RVA: 0x0015B46C File Offset: 0x0015966C
		public override void ScenLoadUpdate()
		{
			if (!NKCAssetResourceManager.IsLoadEnd())
			{
				return;
			}
			NKCUIComVideoCamera subUICameraVideoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
			if (subUICameraVideoPlayer != null && subUICameraVideoPlayer.IsPreparing())
			{
				return;
			}
			if (this.m_NKCUIGauntletLobby == null && this.m_UILoadResourceData != null)
			{
				if (!NKCUIGauntletLobby.CheckInstanceLoaded(this.m_UILoadResourceData, out this.m_NKCUIGauntletLobby))
				{
					return;
				}
				this.m_UILoadResourceData = null;
			}
			this.ScenLoadLastStart();
		}

		// Token: 0x060047AA RID: 18346 RVA: 0x0015B4D0 File Offset: 0x001596D0
		public override void ScenLoadComplete()
		{
			base.ScenLoadComplete();
			if (this.m_NKCUIGauntletLobby != null)
			{
				this.m_NKCUIGauntletLobby.InitUI();
			}
			this.SetBG();
		}

		// Token: 0x060047AB RID: 18347 RVA: 0x0015B4F8 File Offset: 0x001596F8
		public override void ScenStart()
		{
			base.ScenStart();
			NKCCamera.EnableBloom(false);
			if (this.m_NKCUIGauntletLobby != null)
			{
				NKCUIGauntletLobbyAsyncV2 nkcuigauntletLobbyAsyncV = this.m_NKCUIGauntletLobby.m_NKCUIGauntletLobbyAsyncV2;
				if (nkcuigauntletLobbyAsyncV != null)
				{
					nkcuigauntletLobbyAsyncV.SetReserveOpenNpcBotTier(this.m_iNpcNewOpenTierSlot);
				}
				this.m_NKCUIGauntletLobby.Open(this.m_NKC_GAUNTLET_LOBBY_TAB, this.m_Latest_RANK_TYPE, this.m_lastest_AsyncTab);
				NKCContentManager.SetUnlockedContent(STAGE_UNLOCK_REQ_TYPE.SURT_ALWAYS_UNLOCKED, -1);
				NKCContentManager.ShowContentUnlockPopup(null, Array.Empty<STAGE_UNLOCK_REQ_TYPE>());
			}
			if (this.m_Reserved_NKM_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupOKCancel.OpenOKBox(this.m_Reserved_NKM_ERROR_CODE, null, "");
				this.m_Reserved_NKM_ERROR_CODE = NKM_ERROR_CODE.NEC_OK;
			}
		}

		// Token: 0x060047AC RID: 18348 RVA: 0x0015B58C File Offset: 0x0015978C
		private void SetBG()
		{
			NKCUIComVideoCamera subUICameraVideoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
			if (subUICameraVideoPlayer != null)
			{
				subUICameraVideoPlayer.renderMode = VideoRenderMode.CameraFarPlane;
				subUICameraVideoPlayer.m_fMoviePlaySpeed = 1f;
				subUICameraVideoPlayer.SetAlpha(0.6f);
				subUICameraVideoPlayer.Play("Gauntlet_BG.mp4", true, false, null, false);
			}
		}

		// Token: 0x060047AD RID: 18349 RVA: 0x0015B5D4 File Offset: 0x001597D4
		public override void ScenEnd()
		{
			base.ScenEnd();
			if (this.m_NKCUIGauntletLobby != null)
			{
				this.m_NKCUIGauntletLobby.Close();
			}
			NKCUIComVideoCamera subUICameraVideoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
			if (subUICameraVideoPlayer != null)
			{
				subUICameraVideoPlayer.CleanUp();
			}
			if (this.m_NKCPopupGauntletBattleRecord != null)
			{
				this.m_NKCPopupGauntletBattleRecord.CloseInstance();
				this.m_NKCPopupGauntletBattleRecord = null;
			}
			this.m_lastest_AsyncTab = NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.MAX;
			this.m_iNpcNewOpenTierSlot = 0;
			this.CheckNKCPopupGauntletOutgameRewardAndClose();
		}

		// Token: 0x060047AE RID: 18350 RVA: 0x0015B649 File Offset: 0x00159849
		public void OpenBattleRecord(NKM_GAME_TYPE pvpGameType)
		{
			if (this.m_NKCPopupGauntletBattleRecord == null)
			{
				this.m_NKCPopupGauntletBattleRecord = NKCPopupGauntletBattleRecord.OpenInstance();
			}
			NKCPopupGauntletBattleRecord nkcpopupGauntletBattleRecord = this.m_NKCPopupGauntletBattleRecord;
			if (nkcpopupGauntletBattleRecord == null)
			{
				return;
			}
			nkcpopupGauntletBattleRecord.Open(pvpGameType);
		}

		// Token: 0x060047AF RID: 18351 RVA: 0x0015B675 File Offset: 0x00159875
		public override void ScenUpdate()
		{
			base.ScenUpdate();
		}

		// Token: 0x060047B0 RID: 18352 RVA: 0x0015B67D File Offset: 0x0015987D
		public override bool ScenMsgProc(NKCMessageData cNKCMessageData)
		{
			return false;
		}

		// Token: 0x060047B1 RID: 18353 RVA: 0x0015B680 File Offset: 0x00159880
		public void ProcessPVPPointCharge(int itemID = 6)
		{
			if (itemID == 6)
			{
				if (!NKCSynchronizedTime.IsFinished(new DateTime(this.m_lastPVPChargePacketSendTime.Ticks + 600000000L)))
				{
					return;
				}
				long countMiscItem = NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(6);
				int charge_POINT_MAX_COUNT = NKMPvpCommonConst.Instance.CHARGE_POINT_MAX_COUNT;
				if (countMiscItem < (long)charge_POINT_MAX_COUNT)
				{
					DateTime dateTime = new DateTime(NKCPVPManager.GetLastUpdateChargePointTicks());
					if (NKCSynchronizedTime.IsFinished(new DateTime(dateTime.Ticks + NKMPvpCommonConst.Instance.CHARGE_POINT_REFRESH_INTERVAL_TICKS)) && !NKMPopUpBox.IsOpenedWaitBox())
					{
						this.m_lastPVPChargePacketSendTime = NKCSynchronizedTime.GetServerUTCTime(0.0);
						NKCPacketSender.Send_NKMPacket_PVP_CHARGE_POINT_REFRESH_REQ(itemID);
						return;
					}
				}
			}
			else if (itemID == 9)
			{
				if (!NKCSynchronizedTime.IsFinished(this.m_lastPVPPracticeChargePacketSendTime.Ticks + 600000000L))
				{
					return;
				}
				long countMiscItem2 = NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(9);
				int charge_POINT_MAX_COUNT_FOR_PRACTICE = NKMPvpCommonConst.Instance.CHARGE_POINT_MAX_COUNT_FOR_PRACTICE;
				if (countMiscItem2 < (long)charge_POINT_MAX_COUNT_FOR_PRACTICE && NKCSynchronizedTime.IsFinished(NKMTime.GetNextResetTime(new DateTime(NKCPVPManager.GetLastUpdateChargePointTicks()), NKMTime.TimePeriod.Day)) && !NKMPopUpBox.IsOpenedWaitBox())
				{
					this.m_lastPVPPracticeChargePacketSendTime = NKCSynchronizedTime.GetServerUTCTime(0.0);
					NKCPacketSender.Send_NKMPacket_PVP_CHARGE_POINT_REFRESH_REQ(itemID);
				}
			}
		}

		// Token: 0x060047B2 RID: 18354 RVA: 0x0015B79E File Offset: 0x0015999E
		public void OnRecv(NKMPacket_PVP_RANK_LIST_ACK cNKMPacket_PVP_RANK_LIST_ACK)
		{
			if (this.m_NKCUIGauntletLobby != null)
			{
				this.m_NKCUIGauntletLobby.OnRecv(cNKMPacket_PVP_RANK_LIST_ACK);
			}
		}

		// Token: 0x060047B3 RID: 18355 RVA: 0x0015B7BA File Offset: 0x001599BA
		public void OnRecv(NKMPacket_LEAGUE_PVP_RANK_LIST_ACK sPacket)
		{
			if (this.m_NKCUIGauntletLobby != null)
			{
				this.m_NKCUIGauntletLobby.OnRecv(sPacket);
			}
		}

		// Token: 0x060047B4 RID: 18356 RVA: 0x0015B7D6 File Offset: 0x001599D6
		public void OnRecv(NKMPacket_PVP_CHARGE_POINT_REFRESH_ACK cNKMPacket_PVP_CHARGE_POINT_REFRESH_ACK)
		{
			if (this.m_NKCUIGauntletLobby != null)
			{
				this.m_NKCUIGauntletLobby.OnRecv(cNKMPacket_PVP_CHARGE_POINT_REFRESH_ACK);
			}
		}

		// Token: 0x060047B5 RID: 18357 RVA: 0x0015B7F2 File Offset: 0x001599F2
		public void OnRecv(NKMPacket_PVP_RANK_WEEK_REWARD_ACK sPacket)
		{
			if (this.m_NKCUIGauntletLobby != null)
			{
				this.m_NKCUIGauntletLobby.OnRecv(sPacket);
			}
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().NKCPopupGauntletOutgameReward.Open(true, sPacket.rewardData, true, sPacket.isScoreChanged);
		}

		// Token: 0x060047B6 RID: 18358 RVA: 0x0015B830 File Offset: 0x00159A30
		public void OnRecv(NKMPacket_PVP_RANK_SEASON_REWARD_ACK cNKMPacket_PVP_RANK_SEASON_REWARD_ACK)
		{
			if (this.m_NKCUIGauntletLobby != null)
			{
				this.m_NKCUIGauntletLobby.OnRecv(cNKMPacket_PVP_RANK_SEASON_REWARD_ACK);
			}
			if (cNKMPacket_PVP_RANK_SEASON_REWARD_ACK.rewardData != null)
			{
				NKCPopupGauntletOutgameReward nkcpopupGauntletOutgameReward = this.NKCPopupGauntletOutgameReward;
				if (nkcpopupGauntletOutgameReward == null)
				{
					return;
				}
				nkcpopupGauntletOutgameReward.Open(false, cNKMPacket_PVP_RANK_SEASON_REWARD_ACK.rewardData, true, cNKMPacket_PVP_RANK_SEASON_REWARD_ACK.isScoreChanged);
				return;
			}
			else
			{
				NKCPopupGauntletNewSeasonAlarm nkcpopupGauntletNewSeasonAlarm = this.NKCPopupGauntletNewSeasonAlarm;
				if (nkcpopupGauntletNewSeasonAlarm == null)
				{
					return;
				}
				nkcpopupGauntletNewSeasonAlarm.Open(true);
				return;
			}
		}

		// Token: 0x060047B7 RID: 18359 RVA: 0x0015B890 File Offset: 0x00159A90
		public void OnRecv(NKMPacket_ASYNC_PVP_RANK_SEASON_REWARD_ACK packet)
		{
			if (this.m_NKCUIGauntletLobby != null)
			{
				this.m_NKCUIGauntletLobby.OnRecv(packet);
			}
			if (packet.rewardData != null)
			{
				NKCPopupGauntletOutgameReward nkcpopupGauntletOutgameReward = this.NKCPopupGauntletOutgameReward;
				if (nkcpopupGauntletOutgameReward == null)
				{
					return;
				}
				nkcpopupGauntletOutgameReward.Open(false, packet.rewardData, false, false);
				return;
			}
			else
			{
				NKCPopupGauntletNewSeasonAlarm nkcpopupGauntletNewSeasonAlarm = this.NKCPopupGauntletNewSeasonAlarm;
				if (nkcpopupGauntletNewSeasonAlarm == null)
				{
					return;
				}
				nkcpopupGauntletNewSeasonAlarm.Open(false);
				return;
			}
		}

		// Token: 0x060047B8 RID: 18360 RVA: 0x0015B8EA File Offset: 0x00159AEA
		public void OnRecv(NKMPacket_LEAGUE_PVP_WEEKLY_REWARD_ACK sPacket)
		{
			if (this.m_NKCUIGauntletLobby != null)
			{
				this.m_NKCUIGauntletLobby.OnRecv(sPacket);
			}
			if (sPacket.rewardData != null)
			{
				NKCPopupGauntletOutgameReward nkcpopupGauntletOutgameReward = this.NKCPopupGauntletOutgameReward;
				if (nkcpopupGauntletOutgameReward == null)
				{
					return;
				}
				nkcpopupGauntletOutgameReward.OpenForLeague(sPacket.rewardData);
			}
		}

		// Token: 0x060047B9 RID: 18361 RVA: 0x0015B924 File Offset: 0x00159B24
		public void OnRecv(NKMPacket_LEAGUE_PVP_SEASON_REWARD_ACK sPacket)
		{
			if (this.m_NKCUIGauntletLobby != null)
			{
				this.m_NKCUIGauntletLobby.OnRecv(sPacket);
			}
			if (sPacket.rewardData != null)
			{
				NKCPopupGauntletOutgameReward nkcpopupGauntletOutgameReward = this.NKCPopupGauntletOutgameReward;
				if (nkcpopupGauntletOutgameReward == null)
				{
					return;
				}
				nkcpopupGauntletOutgameReward.OpenForLeague(sPacket.rewardData);
				return;
			}
			else
			{
				NKCPopupGauntletNewSeasonAlarm nkcpopupGauntletNewSeasonAlarm = this.NKCPopupGauntletNewSeasonAlarm;
				if (nkcpopupGauntletNewSeasonAlarm == null)
				{
					return;
				}
				nkcpopupGauntletNewSeasonAlarm.OpenForLeague();
				return;
			}
		}

		// Token: 0x060047BA RID: 18362 RVA: 0x0015B97A File Offset: 0x00159B7A
		public void OnRecv(NKMPacket_ASYNC_PVP_TARGET_LIST_ACK packet)
		{
			if (this.m_NKCUIGauntletLobby != null)
			{
				this.m_NKCUIGauntletLobby.OnRecv(packet);
			}
		}

		// Token: 0x060047BB RID: 18363 RVA: 0x0015B996 File Offset: 0x00159B96
		public void OnRecv(NKMPacket_ASYNC_PVP_RANK_LIST_ACK packet)
		{
			if (this.m_NKCUIGauntletLobby != null)
			{
				this.m_NKCUIGauntletLobby.OnRecv(packet);
			}
		}

		// Token: 0x060047BC RID: 18364 RVA: 0x0015B9B2 File Offset: 0x00159BB2
		public void OnRecv(NKMPacket_REVENGE_PVP_TARGET_LIST_ACK sPacket)
		{
			if (this.m_NKCUIGauntletLobby != null)
			{
				this.m_NKCUIGauntletLobby.OnRecv(sPacket);
			}
		}

		// Token: 0x060047BD RID: 18365 RVA: 0x0015B9CE File Offset: 0x00159BCE
		public void OnRecv(NKMPacket_NPC_PVP_TARGET_LIST_ACK sPacket)
		{
			if (this.m_NKCUIGauntletLobby != null)
			{
				this.m_NKCUIGauntletLobby.OnRecv(sPacket);
			}
		}

		// Token: 0x060047BE RID: 18366 RVA: 0x0015B9EA File Offset: 0x00159BEA
		public void OnRecv(NKMPacket_UPDATE_DEFENCE_DECK_ACK packet)
		{
			if (this.m_NKCUIGauntletLobby != null)
			{
				this.m_NKCUIGauntletLobby.OnRecv(packet);
			}
		}

		// Token: 0x060047BF RID: 18367 RVA: 0x0015BA06 File Offset: 0x00159C06
		public void SetAsyncTargetList(List<AsyncPvpTarget> newlist)
		{
			this.AsyncTargetList.Clear();
			this.AsyncTargetList.AddRange(newlist.FindAll((AsyncPvpTarget v) => this.InvalidTarget(v)));
		}

		// Token: 0x060047C0 RID: 18368 RVA: 0x0015BA30 File Offset: 0x00159C30
		public void SetTargetData(AsyncPvpTarget refreshedTargetData)
		{
			for (int i = 0; i < this.AsyncTargetList.Count; i++)
			{
				if (this.AsyncTargetList[i].userFriendCode == refreshedTargetData.userFriendCode)
				{
					this.AsyncTargetList[i] = refreshedTargetData;
					return;
				}
			}
		}

		// Token: 0x060047C1 RID: 18369 RVA: 0x0015BA7C File Offset: 0x00159C7C
		private bool InvalidTarget(AsyncPvpTarget target)
		{
			return target != null && target.asyncDeck != null && target.asyncDeck.ship != null && target.asyncDeck.units != null && target.asyncDeck.units.Count == 8;
		}

		// Token: 0x060047C2 RID: 18370 RVA: 0x0015BACC File Offset: 0x00159CCC
		public void SetReserveOpenNpcBotTier(int iNewOpenTier)
		{
			this.m_iNpcNewOpenTierSlot = iNewOpenTier;
		}

		// Token: 0x060047C3 RID: 18371 RVA: 0x0015BAD5 File Offset: 0x00159CD5
		public void OnRecv(NKMPacket_PVP_CASTING_VOTE_UNIT_ACK sPacket)
		{
			if (this.m_NKCUIGauntletLobby != null)
			{
				this.m_NKCUIGauntletLobby.OnRecv(sPacket);
			}
		}

		// Token: 0x060047C4 RID: 18372 RVA: 0x0015BAF1 File Offset: 0x00159CF1
		public void OnRecv(NKMPacket_PVP_CASTING_VOTE_SHIP_ACK sPacket)
		{
			if (this.m_NKCUIGauntletLobby != null)
			{
				this.m_NKCUIGauntletLobby.OnRecv(sPacket);
			}
		}

		// Token: 0x060047C5 RID: 18373 RVA: 0x0015BB0D File Offset: 0x00159D0D
		public void OnRecv(NKMPacket_PVP_CASTING_VOTE_OPERATOR_ACK sPacket)
		{
			if (this.m_NKCUIGauntletLobby != null)
			{
				this.m_NKCUIGauntletLobby.OnRecv(sPacket);
			}
		}

		// Token: 0x040037F2 RID: 14322
		private NKCAssetResourceData m_UILoadResourceData;

		// Token: 0x040037F3 RID: 14323
		private NKCUIGauntletLobby m_NKCUIGauntletLobby;

		// Token: 0x040037F4 RID: 14324
		private NKC_GAUNTLET_LOBBY_TAB m_NKC_GAUNTLET_LOBBY_TAB;

		// Token: 0x040037F5 RID: 14325
		private NKCPopupGauntletBattleRecord m_NKCPopupGauntletBattleRecord;

		// Token: 0x040037F6 RID: 14326
		private RANK_TYPE m_Latest_RANK_TYPE;

		// Token: 0x040037F7 RID: 14327
		private NKM_ERROR_CODE m_Reserved_NKM_ERROR_CODE;

		// Token: 0x040037F8 RID: 14328
		private NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE m_lastest_AsyncTab = NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.MAX;

		// Token: 0x040037F9 RID: 14329
		public static float AsyncRefreshCooltime;

		// Token: 0x040037FA RID: 14330
		public List<AsyncPvpTarget> AsyncTargetList = new List<AsyncPvpTarget>();

		// Token: 0x040037FB RID: 14331
		private long asyncUserUID;

		// Token: 0x040037FC RID: 14332
		private bool m_bWaitForEmoticon;

		// Token: 0x040037FD RID: 14333
		private NKCPopupGauntletOutgameReward m_NKCPopupGauntletOutgameReward;

		// Token: 0x040037FE RID: 14334
		private NKCPopupGauntletNewSeasonAlarm m_NKCPopupGauntletNewSeasonAlarm;

		// Token: 0x040037FF RID: 14335
		private DateTime m_lastPVPChargePacketSendTime;

		// Token: 0x04003800 RID: 14336
		private DateTime m_lastPVPPracticeChargePacketSendTime;

		// Token: 0x04003801 RID: 14337
		private const long CHARGE_POINT_REFRESH_PACKET_INTERVAL_TICK = 600000000L;

		// Token: 0x04003802 RID: 14338
		private int m_iNpcNewOpenTierSlot;
	}
}
