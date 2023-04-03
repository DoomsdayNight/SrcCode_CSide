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
	// Token: 0x02000B82 RID: 2946
	public class NKCUIGauntletPrivateRoomDeckSelect : NKCUIBase
	{
		// Token: 0x060087D4 RID: 34772 RVA: 0x002DF2C1 File Offset: 0x002DD4C1
		public static NKCUIManager.LoadedUIData OpenNewInstanceAsync()
		{
			if (!NKCUIManager.IsValid(NKCUIGauntletPrivateRoomDeckSelect.s_LoadedUIData))
			{
				NKCUIGauntletPrivateRoomDeckSelect.s_LoadedUIData = NKCUIManager.OpenNewInstanceAsync<NKCUIGauntletPrivateRoomDeckSelect>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_PRIVATE_ROOM_DECK_SELECT", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIGauntletPrivateRoomDeckSelect.CleanupInstance));
			}
			return NKCUIGauntletPrivateRoomDeckSelect.s_LoadedUIData;
		}

		// Token: 0x170015DB RID: 5595
		// (get) Token: 0x060087D5 RID: 34773 RVA: 0x002DF2F5 File Offset: 0x002DD4F5
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIGauntletPrivateRoomDeckSelect.s_LoadedUIData != null && NKCUIGauntletPrivateRoomDeckSelect.s_LoadedUIData.IsUIOpen;
			}
		}

		// Token: 0x170015DC RID: 5596
		// (get) Token: 0x060087D6 RID: 34774 RVA: 0x002DF30A File Offset: 0x002DD50A
		public static bool IsInstanceLoaded
		{
			get
			{
				return NKCUIGauntletPrivateRoomDeckSelect.s_LoadedUIData != null && NKCUIGauntletPrivateRoomDeckSelect.s_LoadedUIData.IsLoadComplete;
			}
		}

		// Token: 0x060087D7 RID: 34775 RVA: 0x002DF31F File Offset: 0x002DD51F
		public static NKCUIGauntletPrivateRoomDeckSelect GetInstance()
		{
			if (NKCUIGauntletPrivateRoomDeckSelect.s_LoadedUIData != null && NKCUIGauntletPrivateRoomDeckSelect.s_LoadedUIData.IsLoadComplete)
			{
				return NKCUIGauntletPrivateRoomDeckSelect.s_LoadedUIData.GetInstance<NKCUIGauntletPrivateRoomDeckSelect>();
			}
			return null;
		}

		// Token: 0x060087D8 RID: 34776 RVA: 0x002DF340 File Offset: 0x002DD540
		public static void CleanupInstance()
		{
			NKCUIGauntletPrivateRoomDeckSelect.s_LoadedUIData = null;
		}

		// Token: 0x170015DD RID: 5597
		// (get) Token: 0x060087D9 RID: 34777 RVA: 0x002DF348 File Offset: 0x002DD548
		public override string GuideTempletID
		{
			get
			{
				return "ARTICLE_PVP_PRIVATE";
			}
		}

		// Token: 0x170015DE RID: 5598
		// (get) Token: 0x060087DA RID: 34778 RVA: 0x002DF34F File Offset: 0x002DD54F
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170015DF RID: 5599
		// (get) Token: 0x060087DB RID: 34779 RVA: 0x002DF352 File Offset: 0x002DD552
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x170015E0 RID: 5600
		// (get) Token: 0x060087DC RID: 34780 RVA: 0x002DF355 File Offset: 0x002DD555
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_GAUNTLET;
			}
		}

		// Token: 0x170015E1 RID: 5601
		// (get) Token: 0x060087DD RID: 34781 RVA: 0x002DF35C File Offset: 0x002DD55C
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return new List<int>();
			}
		}

		// Token: 0x060087DE RID: 34782 RVA: 0x002DF364 File Offset: 0x002DD564
		public void Init()
		{
			NKMPvpGameLobbyUserState myPvpGameLobbyUserState = NKCPrivatePVPRoomMgr.GetMyPvpGameLobbyUserState();
			for (int i = 0; i < this.m_leftDeckInfo.Unit.Length; i++)
			{
				NKCDeckViewUnitSlot nkcdeckViewUnitSlot = this.m_leftDeckInfo.Unit[i];
				nkcdeckViewUnitSlot.Init(0, false);
				nkcdeckViewUnitSlot.m_NKCUIComButton.PointerClick.RemoveAllListeners();
				if (NKCPrivatePVPRoomMgr.IsPlayer(myPvpGameLobbyUserState))
				{
					nkcdeckViewUnitSlot.m_NKCUIComButton.PointerClick.AddListener(new UnityAction(this.OnTouchDeckEdit));
				}
			}
			NKCUIComButton component = this.m_leftDeckInfo.Ship.GetComponent<NKCUIComButton>();
			if (component != null)
			{
				component.PointerClick.RemoveAllListeners();
				component.PointerClick.AddListener(new UnityAction(this.OnTouchDeckEdit));
			}
			for (int j = 0; j < this.m_leftDeckInfo.Unit.Length; j++)
			{
				this.m_leftDeckInfo.Unit[j].Init(0, false);
				this.m_leftDeckInfo.Unit[j].SetData(null, true);
			}
			NKCUtil.SetGameobjectActive(this.m_leftDeckInfo.Ship, false);
			this.m_leftDeckInfo.Ship.sprite = null;
			this.m_leftDeckInfo.ShipInfo.SetShipData(null, null, false);
			this.m_leftDeckInfo.OperatorInfo.SetEmpty();
			for (int k = 0; k < this.m_rightDeckInfo.Unit.Length; k++)
			{
				this.m_rightDeckInfo.Unit[k].Init(0, false);
				this.m_rightDeckInfo.Unit[k].SetData(null, true);
			}
			NKCUtil.SetGameobjectActive(this.m_rightDeckInfo.Ship, false);
			this.m_rightDeckInfo.Ship.sprite = null;
			this.m_rightDeckInfo.ShipInfo.SetShipData(null, null, false);
			this.m_rightDeckInfo.OperatorInfo.SetEmpty();
			this.m_btnDeckEdit.PointerClick.RemoveAllListeners();
			this.m_btnBattle.PointerClick.RemoveAllListeners();
			this.m_btnCancelMatch.PointerClick.RemoveAllListeners();
			if (NKCPrivatePVPRoomMgr.IsPlayer(myPvpGameLobbyUserState))
			{
				this.m_btnDeckEdit.PointerClick.AddListener(new UnityAction(this.OnTouchDeckEdit));
				this.m_btnBattle.PointerClick.AddListener(new UnityAction(this.OnTouchBattle));
			}
			this.m_btnCancelMatch.PointerClick.AddListener(new UnityAction(this.OnTouchCancelMatch));
			NKCUtil.SetGameobjectActive(this.m_btnDeckEdit, NKCPrivatePVPRoomMgr.IsPlayer(myPvpGameLobbyUserState));
			NKCUtil.SetGameobjectActive(this.m_btnBattle, NKCPrivatePVPRoomMgr.IsPlayer(myPvpGameLobbyUserState));
			NKCUtil.SetGameobjectActive(this.m_leftDeckReadyEffect, false);
			NKCUtil.SetGameobjectActive(this.m_rightDeckReadyEffect, false);
		}

		// Token: 0x060087DF RID: 34783 RVA: 0x002DF5F1 File Offset: 0x002DD7F1
		public void Open()
		{
			this.RefreshUI();
		}

		// Token: 0x060087E0 RID: 34784 RVA: 0x002DF5FC File Offset: 0x002DD7FC
		public void RefreshUI()
		{
			NKMPvpGameLobbyUserState myPvpGameLobbyUserState = NKCPrivatePVPRoomMgr.GetMyPvpGameLobbyUserState();
			if (NKCScenManager.CurrentUserData().m_ArmyData.GetDeckData(myPvpGameLobbyUserState.deckIndex) == null)
			{
				this.m_curDeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_PVP, 0);
			}
			else
			{
				this.m_curDeckIndex = myPvpGameLobbyUserState.deckIndex;
			}
			this.SetUserInfo(NKCPrivatePVPRoomMgr.GetLeftPvpGameLobbyUserState(), this.m_leftUserInfo);
			this.SetUserInfo(NKCPrivatePVPRoomMgr.GetRightPvpGameLobbyUserState(), this.m_rightUserInfo);
			this.SetDeckInfo(NKCPrivatePVPRoomMgr.GetLeftPvpGameLobbyUserState(), this.m_leftDeckInfo);
			this.SetDeckInfo(NKCPrivatePVPRoomMgr.GetRightPvpGameLobbyUserState(), this.m_rightDeckInfo);
			this.OnMatchReady();
			if (!base.IsOpen)
			{
				base.UIOpened(true);
			}
		}

		// Token: 0x060087E1 RID: 34785 RVA: 0x002DF69C File Offset: 0x002DD89C
		private void SetUserInfo(NKMPvpGameLobbyUserState targetLobbyUserState, NKCUIGauntletPrivateRoomDeckSelect.UserInfoUI userInfo)
		{
			NKMUserProfileData profileData = targetLobbyUserState.profileData;
			userInfo.Profile.SetProfiledata(profileData.commonProfile, null);
			NKCUtil.SetLabelText(userInfo.Level, NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, new object[]
			{
				profileData.commonProfile.level
			});
			NKCUtil.SetLabelText(userInfo.Name, NKCStringTable.GetString(profileData.commonProfile.nickname, true));
			NKCUtil.SetGameobjectActive(userInfo.FriendCode, profileData.commonProfile.friendCode > 0L);
			NKCUtil.SetLabelText(userInfo.FriendCode, NKCUtilString.GetFriendCode(profileData.commonProfile.friendCode, true));
			NKCUtil.SetGameobjectActive(userInfo.Guild, profileData.guildData != null && profileData.guildData.guildUid > 0L);
			if (userInfo.Guild != null && userInfo.Guild.activeSelf)
			{
				userInfo.GuildBadge.SetData(profileData.guildData.badgeId, true);
				NKCUtil.SetLabelText(userInfo.GuildName, NKCUtilString.GetUserGuildName(profileData.guildData.guildName, true));
			}
		}

		// Token: 0x060087E2 RID: 34786 RVA: 0x002DF7B0 File Offset: 0x002DD9B0
		private void SetDeckInfo(NKMPvpGameLobbyUserState targetLobbyUserState, NKCUIGauntletPrivateRoomDeckSelect.DeckInfoUI deckInfo)
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
				NKCUtil.SetGameobjectActive(deckInfo.Ship, true);
				deckInfo.Ship.sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, unitTempletBase);
				NKMUnitData shipData = NKMDungeonManager.MakeUnitDataFromID(ship.UnitId, -1L, ship.UnitLevel, (int)ship.LimitBreakLevel, ship.SkinId, 0);
				deckInfo.ShipInfo.SetShipData(shipData, unitTempletBase, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(deckInfo.Ship, false);
				deckInfo.Ship.sprite = null;
				deckInfo.ShipInfo.SetShipData(null, null, false);
			}
			if (!NKCOperatorUtil.IsHide())
			{
				if (deckData.operatorUnit != null && deckData.operatorUnit.UnitId > 0)
				{
					NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(deckData.operatorUnit.UnitId);
					deckInfo.OperatorInfo.SetData(unitTempletBase2, deckData.operatorUnit.UnitLevel);
				}
				else
				{
					deckInfo.OperatorInfo.SetEmpty();
				}
			}
			NKCUtil.SetGameobjectActive(deckInfo.OperatorInfo, !NKCOperatorUtil.IsHide());
			for (int i = 0; i < deckInfo.Unit.Length; i++)
			{
				NKCDeckViewUnitSlot nkcdeckViewUnitSlot = deckInfo.Unit[i];
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
			NKCUtil.SetLabelText(deckInfo.Power, deckData.CalculateOperationPowerForPrivatePvp().ToString());
			NKCUtil.SetLabelText(deckInfo.AvgCost, string.Format("{0:0.00}", deckData.CalculateAvgSummonCost(flag ? NKCBanManager.GetBanData(NKCBanManager.BAN_DATA_TYPE.FINAL) : null, flag2 ? NKCBanManager.m_dicNKMUpData : null)));
		}

		// Token: 0x060087E3 RID: 34787 RVA: 0x002DFA07 File Offset: 0x002DDC07
		public override void CloseInternal()
		{
		}

		// Token: 0x060087E4 RID: 34788 RVA: 0x002DFA0C File Offset: 0x002DDC0C
		private void OnTouchDeckEdit()
		{
			if (!NKCPrivatePVPRoomMgr.CanEditDeck())
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

		// Token: 0x060087E5 RID: 34789 RVA: 0x002DFAEA File Offset: 0x002DDCEA
		private void OnChangeDeckIndex(NKMDeckIndex deckIndex)
		{
			this.m_curDeckIndex = deckIndex;
			NKCPrivatePVPRoomMgr.Send_NKMPacket_PRIVATE_PVP_SYNC_DECK_INDEX_REQ(deckIndex);
		}

		// Token: 0x060087E6 RID: 34790 RVA: 0x002DFAFC File Offset: 0x002DDCFC
		private void OnTouchBattle()
		{
			NKM_ERROR_CODE nkm_ERROR_CODE = this.CheckSelectDeck();
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupOKCancel.OpenOKBox(nkm_ERROR_CODE, null, "");
				return;
			}
			NKCPrivatePVPRoomMgr.Send_NKMPacket_PRIVATE_PVP_READY_REQ(this.m_curDeckIndex);
		}

		// Token: 0x060087E7 RID: 34791 RVA: 0x002DFB2B File Offset: 0x002DDD2B
		private void OnTouchCancelMatch()
		{
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_PRIVATE_PVP_READY_CANCEL_TITLE, NKCUtilString.GET_STRING_PRIVATE_PVP_READY_CANCEL, new NKCPopupOKCancel.OnButton(NKCPrivatePVPRoomMgr.Send_NKMPacket_PRIVATE_PVP_EXIT_REQ), null, false);
		}

		// Token: 0x060087E8 RID: 34792 RVA: 0x002DFB4A File Offset: 0x002DDD4A
		private NKM_ERROR_CODE CheckSelectDeck()
		{
			return NKMMain.IsValidDeck(NKCScenManager.CurrentUserData().m_ArmyData, this.m_curDeckIndex.m_eDeckType, this.m_curDeckIndex.m_iIndex, NKM_GAME_TYPE.NGT_PVP_PRIVATE);
		}

		// Token: 0x060087E9 RID: 34793 RVA: 0x002DFB73 File Offset: 0x002DDD73
		private void OnTouchDeckSelect(NKMDeckIndex deckIndex)
		{
			if (this.m_curDeckIndex != deckIndex)
			{
				NKCPrivatePVPRoomMgr.Send_NKMPacket_PRIVATE_PVP_SYNC_DECK_INDEX_REQ(deckIndex);
			}
			this.m_curDeckIndex = deckIndex;
			this.SetDeckInfo(NKCPrivatePVPRoomMgr.GetLeftPvpGameLobbyUserState(), this.m_leftDeckInfo);
			NKCUIDeckViewer.Instance.Close();
		}

		// Token: 0x060087EA RID: 34794 RVA: 0x002DFBAB File Offset: 0x002DDDAB
		private void OnTouchDeckSelect()
		{
			this.OnTouchDeckSelect(this.m_curDeckIndex);
		}

		// Token: 0x060087EB RID: 34795 RVA: 0x002DFBB9 File Offset: 0x002DDDB9
		public override void OnBackButton()
		{
			NKCPrivatePVPRoomMgr.Send_NKMPacket_PVP_ROOM_CANCEL_INVITE_REQ();
		}

		// Token: 0x060087EC RID: 34796 RVA: 0x002DFBC0 File Offset: 0x002DDDC0
		public void ProcessBackButton()
		{
			base.OnBackButton();
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetReservedLobbyTab(NKC_GAUNTLET_LOBBY_TAB.NGLT_PRIVATE);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_LOBBY, true);
		}

		// Token: 0x060087ED RID: 34797 RVA: 0x002DFBE8 File Offset: 0x002DDDE8
		public void OnMatchReady()
		{
			NKMPvpGameLobbyUserState myPvpGameLobbyUserState = NKCPrivatePVPRoomMgr.GetMyPvpGameLobbyUserState();
			NKCUtil.SetGameobjectActive(this.m_btnBattle, NKCPrivatePVPRoomMgr.IsPlayer(myPvpGameLobbyUserState) && !myPvpGameLobbyUserState.isReady);
			NKCUtil.SetGameobjectActive(this.m_btnDeckEdit, NKCPrivatePVPRoomMgr.IsPlayer(myPvpGameLobbyUserState) && !myPvpGameLobbyUserState.isReady);
			NKCUtil.SetGameobjectActive(this.m_leftDeckReadyEffect, NKCPrivatePVPRoomMgr.GetLeftPvpGameLobbyUserState().isReady);
			NKCUtil.SetGameobjectActive(this.m_rightDeckReadyEffect, NKCPrivatePVPRoomMgr.GetRightPvpGameLobbyUserState().isReady);
		}

		// Token: 0x0400742E RID: 29742
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_GAUNTLET";

		// Token: 0x0400742F RID: 29743
		private const string UI_ASSET_NAME = "NKM_UI_GAUNTLET_PRIVATE_ROOM_DECK_SELECT";

		// Token: 0x04007430 RID: 29744
		private static NKCUIManager.LoadedUIData s_LoadedUIData;

		// Token: 0x04007431 RID: 29745
		public NKCUIGauntletPrivateRoomDeckSelect.UserInfoUI m_leftUserInfo;

		// Token: 0x04007432 RID: 29746
		public NKCUIGauntletPrivateRoomDeckSelect.DeckInfoUI m_leftDeckInfo;

		// Token: 0x04007433 RID: 29747
		public NKCUIGauntletPrivateRoomDeckSelect.UserInfoUI m_rightUserInfo;

		// Token: 0x04007434 RID: 29748
		public NKCUIGauntletPrivateRoomDeckSelect.DeckInfoUI m_rightDeckInfo;

		// Token: 0x04007435 RID: 29749
		public NKCUIComStateButton m_btnDeckEdit;

		// Token: 0x04007436 RID: 29750
		public NKCUIComStateButton m_btnBattle;

		// Token: 0x04007437 RID: 29751
		public NKCUIComButton m_btnCancelMatch;

		// Token: 0x04007438 RID: 29752
		public GameObject m_objRightShipPrivate;

		// Token: 0x04007439 RID: 29753
		public GameObject m_leftDeckReadyEffect;

		// Token: 0x0400743A RID: 29754
		public GameObject m_rightDeckReadyEffect;

		// Token: 0x0400743B RID: 29755
		[Header("Sprite")]
		public Sprite SpriteUnitPrivate;

		// Token: 0x0400743C RID: 29756
		private NKMDeckIndex m_curDeckIndex;

		// Token: 0x02001926 RID: 6438
		[Serializable]
		public class UserInfoUI
		{
			// Token: 0x0400AAC6 RID: 43718
			public NKCUISlotProfile Profile;

			// Token: 0x0400AAC7 RID: 43719
			public Text Level;

			// Token: 0x0400AAC8 RID: 43720
			public Text Name;

			// Token: 0x0400AAC9 RID: 43721
			public Text FriendCode;

			// Token: 0x0400AACA RID: 43722
			public GameObject Guild;

			// Token: 0x0400AACB RID: 43723
			public NKCUIGuildBadge GuildBadge;

			// Token: 0x0400AACC RID: 43724
			public Text GuildName;
		}

		// Token: 0x02001927 RID: 6439
		[Serializable]
		public class DeckInfoUI
		{
			// Token: 0x0400AACD RID: 43725
			public NKCUIOperatorDeckSlot OperatorInfo;

			// Token: 0x0400AACE RID: 43726
			public Image Ship;

			// Token: 0x0400AACF RID: 43727
			public NKCUIShipInfoSummary ShipInfo;

			// Token: 0x0400AAD0 RID: 43728
			public NKCDeckViewUnitSlot[] Unit = new NKCDeckViewUnitSlot[8];

			// Token: 0x0400AAD1 RID: 43729
			public Text Power;

			// Token: 0x0400AAD2 RID: 43730
			public Text AvgCost;
		}
	}
}
