using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Pvp;
using NKC.UI.Guild;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B7F RID: 2943
	public class NKCUIGauntletPrivateReady : NKCUIBase
	{
		// Token: 0x06008797 RID: 34711 RVA: 0x002DE12E File Offset: 0x002DC32E
		public static NKCUIManager.LoadedUIData OpenNewInstanceAsync()
		{
			if (!NKCUIManager.IsValid(NKCUIGauntletPrivateReady.s_LoadedUIData))
			{
				NKCUIGauntletPrivateReady.s_LoadedUIData = NKCUIManager.OpenNewInstanceAsync<NKCUIGauntletPrivateReady>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_PRIVATE_READY", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIGauntletPrivateReady.CleanupInstance));
			}
			return NKCUIGauntletPrivateReady.s_LoadedUIData;
		}

		// Token: 0x170015CE RID: 5582
		// (get) Token: 0x06008798 RID: 34712 RVA: 0x002DE162 File Offset: 0x002DC362
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIGauntletPrivateReady.s_LoadedUIData != null && NKCUIGauntletPrivateReady.s_LoadedUIData.IsUIOpen;
			}
		}

		// Token: 0x170015CF RID: 5583
		// (get) Token: 0x06008799 RID: 34713 RVA: 0x002DE177 File Offset: 0x002DC377
		public static bool IsInstanceLoaded
		{
			get
			{
				return NKCUIGauntletPrivateReady.s_LoadedUIData != null && NKCUIGauntletPrivateReady.s_LoadedUIData.IsLoadComplete;
			}
		}

		// Token: 0x0600879A RID: 34714 RVA: 0x002DE18C File Offset: 0x002DC38C
		public static NKCUIGauntletPrivateReady GetInstance()
		{
			if (NKCUIGauntletPrivateReady.s_LoadedUIData != null && NKCUIGauntletPrivateReady.s_LoadedUIData.IsLoadComplete)
			{
				return NKCUIGauntletPrivateReady.s_LoadedUIData.GetInstance<NKCUIGauntletPrivateReady>();
			}
			return null;
		}

		// Token: 0x0600879B RID: 34715 RVA: 0x002DE1AD File Offset: 0x002DC3AD
		public static void CleanupInstance()
		{
			NKCUIGauntletPrivateReady.s_LoadedUIData = null;
		}

		// Token: 0x170015D0 RID: 5584
		// (get) Token: 0x0600879C RID: 34716 RVA: 0x002DE1B5 File Offset: 0x002DC3B5
		public override string GuideTempletID
		{
			get
			{
				return "ARTICLE_PVP_PRIVATE";
			}
		}

		// Token: 0x170015D1 RID: 5585
		// (get) Token: 0x0600879D RID: 34717 RVA: 0x002DE1BC File Offset: 0x002DC3BC
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170015D2 RID: 5586
		// (get) Token: 0x0600879E RID: 34718 RVA: 0x002DE1BF File Offset: 0x002DC3BF
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x170015D3 RID: 5587
		// (get) Token: 0x0600879F RID: 34719 RVA: 0x002DE1C2 File Offset: 0x002DC3C2
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_GAUNTLET;
			}
		}

		// Token: 0x170015D4 RID: 5588
		// (get) Token: 0x060087A0 RID: 34720 RVA: 0x002DE1C9 File Offset: 0x002DC3C9
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return new List<int>();
			}
		}

		// Token: 0x060087A1 RID: 34721 RVA: 0x002DE1D0 File Offset: 0x002DC3D0
		public void Init()
		{
			for (int i = 0; i < this.m_myDeckInfo.Unit.Length; i++)
			{
				NKCDeckViewUnitSlot nkcdeckViewUnitSlot = this.m_myDeckInfo.Unit[i];
				nkcdeckViewUnitSlot.Init(0, false);
				nkcdeckViewUnitSlot.m_NKCUIComButton.PointerClick.RemoveAllListeners();
				nkcdeckViewUnitSlot.m_NKCUIComButton.PointerClick.AddListener(new UnityAction(this.OnTouchDeckEdit));
			}
			NKCUIComButton component = this.m_myDeckInfo.Ship.GetComponent<NKCUIComButton>();
			if (component != null)
			{
				component.PointerClick.RemoveAllListeners();
				component.PointerClick.AddListener(new UnityAction(this.OnTouchDeckEdit));
			}
			for (int j = 0; j < this.m_enemyDeckInfo.Unit.Length; j++)
			{
				this.m_enemyDeckInfo.Unit[j].Init(0, false);
				this.m_enemyDeckInfo.Unit[j].SetData(null, true);
			}
			NKCUtil.SetGameobjectActive(this.m_enemyDeckInfo.Ship, false);
			this.m_enemyDeckInfo.Ship.sprite = null;
			this.m_enemyDeckInfo.ShipInfo.SetShipData(null, null, false);
			this.m_enemyDeckInfo.OperatorInfo.SetEmpty();
			this.m_btnDeckEdit.PointerClick.RemoveAllListeners();
			this.m_btnDeckEdit.PointerClick.AddListener(new UnityAction(this.OnTouchDeckEdit));
			this.m_btnBattle.PointerClick.RemoveAllListeners();
			this.m_btnBattle.PointerClick.AddListener(new UnityAction(this.OnTouchBattle));
			this.m_btnCancelMatch.PointerClick.RemoveAllListeners();
			this.m_btnCancelMatch.PointerClick.AddListener(new UnityAction(this.OnTouchCancelMatch));
			NKCUtil.SetGameobjectActive(this.m_myDeckReadyEffect, false);
			NKCUtil.SetGameobjectActive(this.m_enemyDeckReadyEffect, false);
		}

		// Token: 0x060087A2 RID: 34722 RVA: 0x002DE392 File Offset: 0x002DC592
		public void Open()
		{
			this.RefreshUI();
		}

		// Token: 0x060087A3 RID: 34723 RVA: 0x002DE39C File Offset: 0x002DC59C
		public void RefreshUI()
		{
			NKMPvpGameLobbyUserState myPvpGameLobbyUserState = NKCPrivatePVPMgr.GetMyPvpGameLobbyUserState();
			if (NKCScenManager.CurrentUserData().m_ArmyData.GetDeckData(myPvpGameLobbyUserState.deckIndex) == null)
			{
				this.m_curDeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_PVP, 0);
			}
			else
			{
				this.m_curDeckIndex = myPvpGameLobbyUserState.deckIndex;
			}
			this.SetMyUserInfo();
			this.SetMyDeckInfo(this.m_curDeckIndex);
			NKMPvpGameLobbyUserState targetPvpGameLobbyUserState = NKCPrivatePVPMgr.GetTargetPvpGameLobbyUserState();
			this.SetEnemyUserInfo(targetPvpGameLobbyUserState);
			this.SetEnemyDeckInfo(targetPvpGameLobbyUserState);
			if (myPvpGameLobbyUserState.isReady)
			{
				this.OnMatchReady(true);
			}
			NKMPvpGameLobbyUserState targetPvpGameLobbyUserState2 = NKCPrivatePVPMgr.GetTargetPvpGameLobbyUserState();
			if (targetPvpGameLobbyUserState2 != null && targetPvpGameLobbyUserState2.isReady)
			{
				this.OnMatchReady(false);
			}
			if (!base.IsOpen)
			{
				base.UIOpened(true);
			}
		}

		// Token: 0x060087A4 RID: 34724 RVA: 0x002DE43D File Offset: 0x002DC63D
		public NKMDeckIndex GetLastDeckIndex()
		{
			return this.m_curDeckIndex;
		}

		// Token: 0x060087A5 RID: 34725 RVA: 0x002DE448 File Offset: 0x002DC648
		private void SetMyUserInfo()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			NKMUserProfileData userProfileData = NKCScenManager.CurrentUserData().UserProfileData;
			if (userProfileData != null)
			{
				this.m_myUserInfo.Profile.SetProfiledata(userProfileData.commonProfile, null);
			}
			NKCUtil.SetLabelText(this.m_myUserInfo.Level, NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, new object[]
			{
				nkmuserData.UserLevel
			});
			NKCUtil.SetLabelText(this.m_myUserInfo.Name, NKCUtilString.GetUserNickname(nkmuserData.m_UserNickName, false));
			NKCUtil.SetLabelText(this.m_myUserInfo.FriendCode, NKCUtilString.GetFriendCode(nkmuserData.m_FriendCode, false));
			NKCUtil.SetGameobjectActive(this.m_myUserInfo.Guild, NKCGuildManager.HasGuild());
			if (this.m_myUserInfo.Guild != null && this.m_myUserInfo.Guild.activeSelf)
			{
				this.m_myUserInfo.GuildBadge.SetData(NKCGuildManager.MyGuildData.badgeId);
				NKCUtil.SetLabelText(this.m_myUserInfo.GuildName, NKCGuildManager.MyGuildData.name);
			}
		}

		// Token: 0x060087A6 RID: 34726 RVA: 0x002DE554 File Offset: 0x002DC754
		private void SetEnemyUserInfo(NKMPvpGameLobbyUserState targetLobbyUserState)
		{
			NKMUserProfileData profileData = targetLobbyUserState.profileData;
			this.m_enemyUserInfo.Profile.SetProfiledata(profileData.commonProfile, null);
			NKCUtil.SetLabelText(this.m_enemyUserInfo.Level, NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, new object[]
			{
				profileData.commonProfile.level
			});
			NKCUtil.SetLabelText(this.m_enemyUserInfo.Name, NKCStringTable.GetString(profileData.commonProfile.nickname, true));
			NKCUtil.SetGameobjectActive(this.m_enemyUserInfo.FriendCode, profileData.commonProfile.friendCode > 0L);
			NKCUtil.SetLabelText(this.m_enemyUserInfo.FriendCode, NKCUtilString.GetFriendCode(profileData.commonProfile.friendCode, true));
			NKCUtil.SetGameobjectActive(this.m_enemyUserInfo.Guild, profileData.guildData != null && profileData.guildData.guildUid > 0L);
			if (this.m_enemyUserInfo.Guild != null && this.m_enemyUserInfo.Guild.activeSelf)
			{
				this.m_enemyUserInfo.GuildBadge.SetData(profileData.guildData.badgeId, true);
				NKCUtil.SetLabelText(this.m_enemyUserInfo.GuildName, NKCUtilString.GetUserGuildName(profileData.guildData.guildName, true));
			}
		}

		// Token: 0x060087A7 RID: 34727 RVA: 0x002DE69C File Offset: 0x002DC89C
		private void SetMyDeckInfo(NKMDeckIndex myDeckIndex)
		{
			bool flag = NKCUtil.CheckPossibleShowBan(NKCUIDeckViewer.DeckViewerMode.PrivatePvPReady);
			bool flag2 = NKCUtil.CheckPossibleShowUpUnit(NKCUIDeckViewer.DeckViewerMode.PrivatePvPReady);
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			NKMDeckData deckData = armyData.GetDeckData(myDeckIndex);
			if (deckData == null)
			{
				return;
			}
			NKMUnitData shipFromUID = armyData.GetShipFromUID(deckData.m_ShipUID);
			NKMUnitTempletBase shipTempletBase = null;
			if (shipFromUID != null)
			{
				this.m_myDeckInfo.Ship.enabled = true;
				this.m_myDeckInfo.Ship.sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, shipFromUID);
				shipTempletBase = NKMUnitManager.GetUnitTempletBase(shipFromUID.m_UnitID);
			}
			else
			{
				this.m_myDeckInfo.Ship.enabled = false;
				this.m_myDeckInfo.Ship.sprite = null;
			}
			this.m_myDeckInfo.ShipInfo.SetShipData(shipFromUID, shipTempletBase, myDeckIndex, false);
			for (int i = 0; i < this.m_myDeckInfo.Unit.Length; i++)
			{
				NKMUnitData unitFromUID = armyData.GetUnitFromUID(deckData.m_listDeckUnitUID[i]);
				NKCDeckViewUnitSlot nkcdeckViewUnitSlot = this.m_myDeckInfo.Unit[i];
				nkcdeckViewUnitSlot.SetEnableShowBan(flag);
				nkcdeckViewUnitSlot.SetEnableShowUpUnit(flag2);
				if (unitFromUID != null)
				{
					nkcdeckViewUnitSlot.SetData(unitFromUID, true);
					nkcdeckViewUnitSlot.SetLeader(i == (int)deckData.m_LeaderIndex, false);
				}
				else
				{
					nkcdeckViewUnitSlot.SetData(null, true);
					nkcdeckViewUnitSlot.SetLeader(false, false);
				}
			}
			if (!NKCOperatorUtil.IsHide())
			{
				NKMOperator operatorData = NKCOperatorUtil.GetOperatorData(deckData.m_OperatorUID);
				if (operatorData != null)
				{
					this.m_myDeckInfo.OperatorInfo.SetData(operatorData, false);
				}
				else
				{
					this.m_myDeckInfo.OperatorInfo.SetEmpty();
				}
			}
			NKCUtil.SetGameobjectActive(this.m_myDeckInfo.OperatorInfo, !NKCOperatorUtil.IsHide());
			NKCUtil.SetLabelText(this.m_myDeckInfo.Power, armyData.GetArmyAvarageOperationPower(myDeckIndex, false, null, null).ToString());
			NKCUtil.SetLabelText(this.m_myDeckInfo.AvgCost, string.Format("{0:0.00}", armyData.CalculateDeckAvgSummonCost(myDeckIndex, flag ? NKCBanManager.GetBanData(NKCBanManager.BAN_DATA_TYPE.FINAL) : null, flag2 ? NKCBanManager.m_dicNKMUpData : null).ToString()));
		}

		// Token: 0x060087A8 RID: 34728 RVA: 0x002DE894 File Offset: 0x002DCA94
		private void SetEnemyDeckInfo(NKMPvpGameLobbyUserState targetLobbyUserState)
		{
			if (targetLobbyUserState == null)
			{
				return;
			}
			bool flag = NKCUtil.CheckPossibleShowBan(NKCUIDeckViewer.DeckViewerMode.PrivatePvPReady);
			bool flag2 = NKCUtil.CheckPossibleShowUpUnit(NKCUIDeckViewer.DeckViewerMode.PrivatePvPReady);
			NKMDummyDeckData deckData = targetLobbyUserState.deckData;
			if (deckData == null)
			{
				return;
			}
			NKMDummyUnitData ship = deckData.Ship;
			if (ship.UnitId > 0)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(ship.UnitId);
				NKCUtil.SetGameobjectActive(this.m_enemyDeckInfo.Ship, true);
				this.m_enemyDeckInfo.Ship.sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, unitTempletBase);
				NKMUnitData shipData = NKMDungeonManager.MakeUnitDataFromID(ship.UnitId, -1L, ship.UnitLevel, (int)ship.LimitBreakLevel, ship.SkinId, 0);
				this.m_enemyDeckInfo.ShipInfo.SetShipData(shipData, unitTempletBase, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_enemyDeckInfo.Ship, false);
				this.m_enemyDeckInfo.Ship.sprite = null;
				this.m_enemyDeckInfo.ShipInfo.SetShipData(null, null, false);
			}
			if (!NKCOperatorUtil.IsHide())
			{
				if (deckData.operatorUnit != null && deckData.operatorUnit.UnitId > 0)
				{
					NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(deckData.operatorUnit.UnitId);
					this.m_enemyDeckInfo.OperatorInfo.SetData(unitTempletBase2, deckData.operatorUnit.UnitLevel);
				}
				else
				{
					this.m_enemyDeckInfo.OperatorInfo.SetEmpty();
				}
			}
			NKCUtil.SetGameobjectActive(this.m_enemyDeckInfo.OperatorInfo, !NKCOperatorUtil.IsHide());
			for (int i = 0; i < this.m_enemyDeckInfo.Unit.Length; i++)
			{
				NKCDeckViewUnitSlot nkcdeckViewUnitSlot = this.m_enemyDeckInfo.Unit[i];
				nkcdeckViewUnitSlot.SetEnableShowBan(flag);
				nkcdeckViewUnitSlot.SetEnableShowUpUnit(flag2);
				if (i >= deckData.List.Length)
				{
					nkcdeckViewUnitSlot.SetData(null, false);
					nkcdeckViewUnitSlot.SetLeader(false, false);
				}
				else
				{
					NKMDummyUnitData nkmdummyUnitData = deckData.List[i];
					if (nkmdummyUnitData != null && nkmdummyUnitData.UnitId > 0)
					{
						NKMUnitData nkmunitData = NKMDungeonManager.MakeUnitDataFromID(nkmdummyUnitData.UnitId, -1L, nkmdummyUnitData.UnitLevel, (int)nkmdummyUnitData.LimitBreakLevel, nkmdummyUnitData.SkinId, nkmdummyUnitData.TacticLevel);
						if (nkmunitData != null)
						{
							nkcdeckViewUnitSlot.SetData(nkmunitData, true);
							nkcdeckViewUnitSlot.SetLeader(i == (int)deckData.LeaderIndex, false);
						}
						else
						{
							nkcdeckViewUnitSlot.SetData(null, true);
							nkcdeckViewUnitSlot.SetLeader(false, false);
						}
					}
					else
					{
						nkcdeckViewUnitSlot.SetPrivate();
					}
				}
			}
			NKCUtil.SetLabelText(this.m_enemyDeckInfo.Power, deckData.CalculateOperationPowerForPrivatePvp().ToString());
			NKCUtil.SetLabelText(this.m_enemyDeckInfo.AvgCost, string.Format("{0:0.00}", deckData.CalculateAvgSummonCost(flag ? NKCBanManager.GetBanData(NKCBanManager.BAN_DATA_TYPE.FINAL) : null, flag2 ? NKCBanManager.m_dicNKMUpData : null)));
		}

		// Token: 0x060087A9 RID: 34729 RVA: 0x002DEB2C File Offset: 0x002DCD2C
		public override void CloseInternal()
		{
		}

		// Token: 0x060087AA RID: 34730 RVA: 0x002DEB30 File Offset: 0x002DCD30
		private void OnTouchDeckEdit()
		{
			if (!NKCPrivatePVPMgr.CanEditDeck())
			{
				return;
			}
			NKCUIDeckViewer.DeckViewerOption options = default(NKCUIDeckViewer.DeckViewerOption);
			options.MenuName = NKCUtilString.GET_STRING_GAUNTLET;
			options.eDeckviewerMode = NKCUIDeckViewer.DeckViewerMode.PrivatePvPReady;
			options.dOnSideMenuButtonConfirm = new NKCUIDeckViewer.DeckViewerOption.OnDeckSideButtonConfirm(this.OnTouchDeckSelect);
			options.dOnBackButton = new NKCUIDeckViewer.DeckViewerOption.OnBackButton(this.OnTouchDeckSelect);
			options.dOnChangeDeckIndex = new NKCUIDeckViewer.DeckViewerOption.OnChangeDeckIndex(this.OnChangeDeckIndex);
			if (NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetDeckData(this.m_curDeckIndex) == null)
			{
				this.m_curDeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_PVP, 0);
			}
			options.DeckIndex = this.m_curDeckIndex;
			options.SelectLeaderUnitOnOpen = true;
			options.bEnableDefaultBackground = false;
			options.bUpsideMenuHomeButton = false;
			options.upsideMenuShowResourceList = this.UpsideMenuShowResourceList;
			options.StageBattleStrID = string.Empty;
			NKCUIDeckViewer.Instance.Open(options, true);
		}

		// Token: 0x060087AB RID: 34731 RVA: 0x002DEC0E File Offset: 0x002DCE0E
		private void OnChangeDeckIndex(NKMDeckIndex deckIndex)
		{
			this.m_curDeckIndex = deckIndex;
			NKCPrivatePVPMgr.Send_NKMPacket_PRIVATE_PVP_SYNC_DECK_INDEX_REQ(deckIndex);
		}

		// Token: 0x060087AC RID: 34732 RVA: 0x002DEC20 File Offset: 0x002DCE20
		private void OnTouchBattle()
		{
			NKM_ERROR_CODE nkm_ERROR_CODE = this.CheckSelectDeck();
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupOKCancel.OpenOKBox(nkm_ERROR_CODE, null, "");
				return;
			}
			NKCPrivatePVPMgr.SendDeckSelectComplete(this.m_curDeckIndex);
		}

		// Token: 0x060087AD RID: 34733 RVA: 0x002DEC4F File Offset: 0x002DCE4F
		private void OnTouchCancelMatch()
		{
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_PRIVATE_PVP_READY_CANCEL_TITLE, NKCUtilString.GET_STRING_PRIVATE_PVP_READY_CANCEL, new NKCPopupOKCancel.OnButton(NKCPrivatePVPMgr.SendReadyCancel), null, false);
		}

		// Token: 0x060087AE RID: 34734 RVA: 0x002DEC6E File Offset: 0x002DCE6E
		private NKM_ERROR_CODE CheckSelectDeck()
		{
			return NKMMain.IsValidDeck(NKCScenManager.CurrentUserData().m_ArmyData, this.m_curDeckIndex.m_eDeckType, this.m_curDeckIndex.m_iIndex, NKM_GAME_TYPE.NGT_PVP_PRIVATE);
		}

		// Token: 0x060087AF RID: 34735 RVA: 0x002DEC97 File Offset: 0x002DCE97
		private void OnTouchDeckSelect(NKMDeckIndex deckIndex)
		{
			if (this.m_curDeckIndex != deckIndex)
			{
				NKCPrivatePVPMgr.Send_NKMPacket_PRIVATE_PVP_SYNC_DECK_INDEX_REQ(deckIndex);
			}
			this.m_curDeckIndex = deckIndex;
			this.SetMyDeckInfo(deckIndex);
			NKCUIDeckViewer.Instance.Close();
		}

		// Token: 0x060087B0 RID: 34736 RVA: 0x002DECC5 File Offset: 0x002DCEC5
		private void OnTouchDeckSelect()
		{
			this.OnTouchDeckSelect(this.m_curDeckIndex);
		}

		// Token: 0x060087B1 RID: 34737 RVA: 0x002DECD3 File Offset: 0x002DCED3
		public override void OnBackButton()
		{
			NKCPrivatePVPMgr.SendInviteCancel();
		}

		// Token: 0x060087B2 RID: 34738 RVA: 0x002DECDA File Offset: 0x002DCEDA
		public void ProcessBackButton()
		{
			base.OnBackButton();
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetReservedLobbyTab(NKC_GAUNTLET_LOBBY_TAB.NGLT_PRIVATE);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_LOBBY, true);
		}

		// Token: 0x060087B3 RID: 34739 RVA: 0x002DED00 File Offset: 0x002DCF00
		private float CalculateDeckAvgSummonCost(NKMAsyncDeckData asyncDeckData)
		{
			int num = 0;
			int num2 = 0;
			if (asyncDeckData == null)
			{
				return 0f;
			}
			for (int i = 0; i < 8; i++)
			{
				if (i < asyncDeckData.units.Count)
				{
					NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(asyncDeckData.units[i].unitId);
					if (unitStatTemplet != null)
					{
						num2++;
						num += unitStatTemplet.GetRespawnCost(false, null, null);
					}
				}
			}
			if (num2 == 0)
			{
				return 0f;
			}
			return ((float)num - 1f) / (float)num2;
		}

		// Token: 0x060087B4 RID: 34740 RVA: 0x002DED72 File Offset: 0x002DCF72
		public void OnMatchReady(bool myDeckReady)
		{
			if (myDeckReady)
			{
				NKCUtil.SetGameobjectActive(this.m_btnBattle, false);
				NKCUtil.SetGameobjectActive(this.m_btnDeckEdit, false);
				NKCUtil.SetGameobjectActive(this.m_myDeckReadyEffect, true);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_enemyDeckReadyEffect, true);
		}

		// Token: 0x0400740B RID: 29707
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_GAUNTLET";

		// Token: 0x0400740C RID: 29708
		private const string UI_ASSET_NAME = "NKM_UI_GAUNTLET_PRIVATE_READY";

		// Token: 0x0400740D RID: 29709
		private static NKCUIManager.LoadedUIData s_LoadedUIData;

		// Token: 0x0400740E RID: 29710
		public NKCUIGauntletPrivateReady.UserInfoUI m_myUserInfo;

		// Token: 0x0400740F RID: 29711
		public NKCUIGauntletPrivateReady.DeckInfoUI m_myDeckInfo;

		// Token: 0x04007410 RID: 29712
		public NKCUIGauntletPrivateReady.UserInfoUI m_enemyUserInfo;

		// Token: 0x04007411 RID: 29713
		public NKCUIGauntletPrivateReady.DeckInfoUI m_enemyDeckInfo;

		// Token: 0x04007412 RID: 29714
		public NKCUIComStateButton m_btnDeckEdit;

		// Token: 0x04007413 RID: 29715
		public NKCUIComStateButton m_btnBattle;

		// Token: 0x04007414 RID: 29716
		public NKCUIComButton m_btnCancelMatch;

		// Token: 0x04007415 RID: 29717
		public GameObject m_objRightShipPrivate;

		// Token: 0x04007416 RID: 29718
		public GameObject m_myDeckReadyEffect;

		// Token: 0x04007417 RID: 29719
		public GameObject m_enemyDeckReadyEffect;

		// Token: 0x04007418 RID: 29720
		[Header("Sprite")]
		public Sprite SpriteUnitPrivate;

		// Token: 0x04007419 RID: 29721
		private NKMDeckIndex m_curDeckIndex;

		// Token: 0x02001924 RID: 6436
		[Serializable]
		public class UserInfoUI
		{
			// Token: 0x0400AAB9 RID: 43705
			public NKCUISlotProfile Profile;

			// Token: 0x0400AABA RID: 43706
			public Text Level;

			// Token: 0x0400AABB RID: 43707
			public Text Name;

			// Token: 0x0400AABC RID: 43708
			public Text FriendCode;

			// Token: 0x0400AABD RID: 43709
			public GameObject Guild;

			// Token: 0x0400AABE RID: 43710
			public NKCUIGuildBadge GuildBadge;

			// Token: 0x0400AABF RID: 43711
			public Text GuildName;
		}

		// Token: 0x02001925 RID: 6437
		[Serializable]
		public class DeckInfoUI
		{
			// Token: 0x0400AAC0 RID: 43712
			public NKCUIOperatorDeckSlot OperatorInfo;

			// Token: 0x0400AAC1 RID: 43713
			public Image Ship;

			// Token: 0x0400AAC2 RID: 43714
			public NKCUIShipInfoSummary ShipInfo;

			// Token: 0x0400AAC3 RID: 43715
			public NKCDeckViewUnitSlot[] Unit = new NKCDeckViewUnitSlot[8];

			// Token: 0x0400AAC4 RID: 43716
			public Text Power;

			// Token: 0x0400AAC5 RID: 43717
			public Text AvgCost;
		}
	}
}
