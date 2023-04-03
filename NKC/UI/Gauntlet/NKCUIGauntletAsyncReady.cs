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
	// Token: 0x02000B67 RID: 2919
	public class NKCUIGauntletAsyncReady : NKCUIBase
	{
		// Token: 0x0600855D RID: 34141 RVA: 0x002D16C5 File Offset: 0x002CF8C5
		public static NKCUIManager.LoadedUIData OpenNewInstanceAsync()
		{
			if (!NKCUIManager.IsValid(NKCUIGauntletAsyncReady.s_LoadedUIData))
			{
				NKCUIGauntletAsyncReady.s_LoadedUIData = NKCUIManager.OpenNewInstanceAsync<NKCUIGauntletAsyncReady>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_ASYNC_READY", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIGauntletAsyncReady.CleanupInstance));
			}
			return NKCUIGauntletAsyncReady.s_LoadedUIData;
		}

		// Token: 0x170015AA RID: 5546
		// (get) Token: 0x0600855E RID: 34142 RVA: 0x002D16F9 File Offset: 0x002CF8F9
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIGauntletAsyncReady.s_LoadedUIData != null && NKCUIGauntletAsyncReady.s_LoadedUIData.IsUIOpen;
			}
		}

		// Token: 0x170015AB RID: 5547
		// (get) Token: 0x0600855F RID: 34143 RVA: 0x002D170E File Offset: 0x002CF90E
		public static bool IsInstanceLoaded
		{
			get
			{
				return NKCUIGauntletAsyncReady.s_LoadedUIData != null && NKCUIGauntletAsyncReady.s_LoadedUIData.IsLoadComplete;
			}
		}

		// Token: 0x06008560 RID: 34144 RVA: 0x002D1723 File Offset: 0x002CF923
		public static NKCUIGauntletAsyncReady GetInstance()
		{
			if (NKCUIGauntletAsyncReady.s_LoadedUIData != null && NKCUIGauntletAsyncReady.s_LoadedUIData.IsLoadComplete)
			{
				return NKCUIGauntletAsyncReady.s_LoadedUIData.GetInstance<NKCUIGauntletAsyncReady>();
			}
			return null;
		}

		// Token: 0x06008561 RID: 34145 RVA: 0x002D1744 File Offset: 0x002CF944
		public static void CleanupInstance()
		{
			NKCUIGauntletAsyncReady.s_LoadedUIData = null;
		}

		// Token: 0x170015AC RID: 5548
		// (get) Token: 0x06008562 RID: 34146 RVA: 0x002D174C File Offset: 0x002CF94C
		public override string GuideTempletID
		{
			get
			{
				return "ARTICLE_PVP_ASYNC";
			}
		}

		// Token: 0x170015AD RID: 5549
		// (get) Token: 0x06008563 RID: 34147 RVA: 0x002D1753 File Offset: 0x002CF953
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170015AE RID: 5550
		// (get) Token: 0x06008564 RID: 34148 RVA: 0x002D1756 File Offset: 0x002CF956
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_GAUNTLET;
			}
		}

		// Token: 0x170015AF RID: 5551
		// (get) Token: 0x06008565 RID: 34149 RVA: 0x002D175D File Offset: 0x002CF95D
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				if (this.m_gameType == NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC)
				{
					return new List<int>
					{
						5,
						101
					};
				}
				return new List<int>
				{
					13,
					5,
					101
				};
			}
		}

		// Token: 0x06008566 RID: 34150 RVA: 0x002D179C File Offset: 0x002CF99C
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
			}
			this.m_btnDeckEdit.PointerClick.RemoveAllListeners();
			this.m_btnDeckEdit.PointerClick.AddListener(new UnityAction(this.OnTouchDeckEdit));
			this.m_btnBattle.PointerClick.RemoveAllListeners();
			this.m_btnBattle.PointerClick.AddListener(new UnityAction(this.OnTouchBattle));
		}

		// Token: 0x06008567 RID: 34151 RVA: 0x002D18C4 File Offset: 0x002CFAC4
		public void Open(AsyncPvpTarget target, NKMDeckIndex lastDeckIndex, NKM_GAME_TYPE gameType)
		{
			if (target == null)
			{
				Debug.LogError("NKCUIGauntletAsyncReady target 없는데 왜 ?");
				return;
			}
			this.m_target = target;
			this.m_gameType = gameType;
			if (NKCScenManager.CurrentUserData().m_ArmyData.GetDeckData(lastDeckIndex) == null)
			{
				this.m_curDeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_PVP, 0);
			}
			else
			{
				this.m_curDeckIndex = lastDeckIndex;
			}
			this.SetMyUserInfo();
			this.SetMyDeckInfo(this.m_curDeckIndex);
			this.SetEnemyUserInfo(target);
			this.SetEnemyDeckInfo(target.asyncDeck);
			this.UpdateRankPVPPointUI();
			base.UIOpened(true);
		}

		// Token: 0x06008568 RID: 34152 RVA: 0x002D1948 File Offset: 0x002CFB48
		public void Open(NpcPvpTarget target, NKMDeckIndex lastDeckIndex, NKM_GAME_TYPE gameType)
		{
			if (target == null)
			{
				Debug.LogError("NKCUIGauntletAsyncReady.Open - NpcPvpTarget is null");
				return;
			}
			this.m_targetNpc = target;
			this.m_gameType = gameType;
			if (NKCScenManager.CurrentUserData().m_ArmyData.GetDeckData(lastDeckIndex) == null)
			{
				this.m_curDeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_PVP, 0);
			}
			else
			{
				this.m_curDeckIndex = lastDeckIndex;
			}
			this.SetMyUserInfo();
			this.SetMyDeckInfo(this.m_curDeckIndex);
			this.SetEnemyUserInfo(target);
			this.SetEnemyDeckInfo(target.asyncDeck);
			this.UpdateRankPVPPointUI();
			base.UIOpened(true);
		}

		// Token: 0x06008569 RID: 34153 RVA: 0x002D19CB File Offset: 0x002CFBCB
		public NKMDeckIndex GetLastDeckIndex()
		{
			return this.m_curDeckIndex;
		}

		// Token: 0x0600856A RID: 34154 RVA: 0x002D19D4 File Offset: 0x002CFBD4
		private void SetMyUserInfo()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
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
				this.m_myUserInfo.GuildBadge.SetData(NKCGuildManager.MyGuildData.badgeId, false);
				NKCUtil.SetLabelText(this.m_myUserInfo.GuildName, NKCUtilString.GetUserGuildName(NKCGuildManager.MyGuildData.name, false));
			}
			PvpState asyncData = nkmuserData.m_AsyncData;
			int num = NKCUtil.FindPVPSeasonIDForAsync(NKCSynchronizedTime.GetServerUTCTime(0.0));
			if (asyncData.SeasonID != num)
			{
				NKCUtil.SetLabelText(this.m_myUserInfo.Score, NKCPVPManager.GetResetScore(asyncData.SeasonID, asyncData.Score, NKM_GAME_TYPE.NGT_ASYNC_PVP).ToString());
				NKCUtil.SetLabelText(this.m_myUserInfo.Rank, "");
				NKMPvpRankTemplet asyncPvpRankTempletByScore = NKCPVPManager.GetAsyncPvpRankTempletByScore(num, NKCUtil.GetScoreBySeason(num, asyncData.SeasonID, asyncData.Score, NKM_GAME_TYPE.NGT_ASYNC_PVP));
				if (asyncPvpRankTempletByScore != null)
				{
					this.m_myUserInfo.Tier.SetUI(asyncPvpRankTempletByScore);
					return;
				}
			}
			else
			{
				NKMPvpRankTemplet asyncPvpRankTempletByTier = NKCPVPManager.GetAsyncPvpRankTempletByTier(num, asyncData.LeagueTierID);
				if (asyncPvpRankTempletByTier != null)
				{
					this.m_myUserInfo.Tier.SetUI(asyncPvpRankTempletByTier);
				}
				NKCUtil.SetLabelText(this.m_myUserInfo.Score, asyncData.Score.ToString());
				NKCUtil.SetLabelText(this.m_myUserInfo.Rank, string.Format(string.Format("{0}{1}", asyncData.Rank, NKCUtilString.GetRankNumber(asyncData.Rank, true)), Array.Empty<object>()));
			}
		}

		// Token: 0x0600856B RID: 34155 RVA: 0x002D1BD3 File Offset: 0x002CFDD3
		private void SetEnemyUserInfo(AsyncPvpTarget target)
		{
			this.SetEnemyUserInfo(target.userLevel, target.userNickName, target.userFriendCode, target.guildData, target.tier, target.rank, target.score);
		}

		// Token: 0x0600856C RID: 34156 RVA: 0x002D1C05 File Offset: 0x002CFE05
		private void SetEnemyUserInfo(NpcPvpTarget target)
		{
			this.SetEnemyUserInfo(target.userLevel, target.userNickName, target.userFriendCode, null, target.tier, 0, target.score);
		}

		// Token: 0x0600856D RID: 34157 RVA: 0x002D1C30 File Offset: 0x002CFE30
		private void SetEnemyUserInfo(int userLv, string nickName, long friendCode, NKMGuildSimpleData guildData, int tier, int rank, int score)
		{
			NKCUtil.SetLabelText(this.m_enemyUserInfo.Level, NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, new object[]
			{
				userLv
			});
			NKCUtil.SetLabelText(this.m_enemyUserInfo.Name, NKCUtilString.GetUserNickname(nickName, true));
			NKCUtil.SetGameobjectActive(this.m_enemyUserInfo.FriendCode, friendCode > 0L);
			NKCUtil.SetLabelText(this.m_enemyUserInfo.FriendCode, NKCUtilString.GetFriendCode(friendCode, true));
			NKCUtil.SetGameobjectActive(this.m_enemyUserInfo.Guild, guildData != null && guildData.guildUid > 0L);
			if (this.m_enemyUserInfo.Guild != null && this.m_enemyUserInfo.Guild.activeSelf)
			{
				this.m_enemyUserInfo.GuildBadge.SetData(guildData.badgeId, true);
				NKCUtil.SetLabelText(this.m_enemyUserInfo.GuildName, NKCUtilString.GetUserGuildName(guildData.guildName, true));
			}
			int seasonID = NKCUtil.FindPVPSeasonIDForAsync(NKCSynchronizedTime.GetServerUTCTime(0.0));
			NKMPvpRankTemplet asyncPvpRankTempletByTier = NKCPVPManager.GetAsyncPvpRankTempletByTier(seasonID, tier);
			if (asyncPvpRankTempletByTier != null)
			{
				this.m_enemyUserInfo.Tier.SetUI(asyncPvpRankTempletByTier);
			}
			if (rank == 0)
			{
				NKCUtil.SetLabelText(this.m_enemyUserInfo.Rank, "");
			}
			else
			{
				NKCUtil.SetLabelText(this.m_enemyUserInfo.Rank, string.Format(string.Format("{0}{1}", rank, NKCUtilString.GetRankNumber(rank, true)), Array.Empty<object>()));
			}
			NKCUtil.SetLabelText(this.m_enemyUserInfo.Score, score.ToString());
			if (this.m_gameType == NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC && this.m_targetNpc != null)
			{
				NKCUtil.SetLabelText(this.m_txtGetScore, string.Format("+{0}", score));
			}
			else
			{
				PvpState asyncData = NKCScenManager.CurrentUserData().m_AsyncData;
				if (asyncData == null)
				{
					return;
				}
				NKMPvpRankTemplet asyncPvpRankTempletByScore = NKCPVPManager.GetAsyncPvpRankTempletByScore(seasonID, asyncData.Score);
				if (asyncPvpRankTempletByScore != null)
				{
					int num = NKCUtil.CalcAddScore(asyncPvpRankTempletByScore.LeagueType, asyncData.Score, score);
					NKCUtil.SetLabelText(this.m_txtGetScore, string.Format("+{0}", num));
				}
				else
				{
					NKCUtil.SetLabelText(this.m_txtGetScore, "");
				}
			}
			NKCUtil.SetGameobjectActive(this.m_enemyUserInfo.Rank, this.m_gameType != NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC);
			NKCUtil.SetGameobjectActive(this.m_enemyUserInfo.Tier, this.m_gameType != NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC);
			NKCUtil.SetGameobjectActive(this.m_enemyUserInfo.Score, this.m_gameType != NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC);
			bool flag = this.m_gameType == NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC || this.m_gameType == NKM_GAME_TYPE.NGT_PVP_STRATEGY_REVENGE;
			NKCUtil.SetGameobjectActive(this.m_objGAUNTLET_ASYNC_READY_INFO_DESC, !flag);
		}

		// Token: 0x0600856E RID: 34158 RVA: 0x002D1ECC File Offset: 0x002D00CC
		private void SetMyDeckInfo(NKMDeckIndex myDeckIndex)
		{
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
			NKCUtil.SetLabelText(this.m_myDeckInfo.AvgCost, string.Format("{0:0.00}", armyData.CalculateDeckAvgSummonCost(myDeckIndex, null, null).ToString()));
		}

		// Token: 0x0600856F RID: 34159 RVA: 0x002D2088 File Offset: 0x002D0288
		private void SetEnemyDeckInfo(NKMAsyncDeckData asyncDeck)
		{
			if (NKCPVPManager.GetPvpAsyncSeasonTemplet(NKCUtil.FindPVPSeasonIDForAsync(NKCSynchronizedTime.GetServerUTCTime(0.0))) == null)
			{
				Debug.LogError("Gauntlet Async Ready - NKMPvpRankSeasonTemplet is null");
				return;
			}
			if (asyncDeck == null)
			{
				return;
			}
			NKMAsyncUnitData ship = asyncDeck.ship;
			if (ship.unitId > 0)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(ship.unitId);
				NKCUtil.SetGameobjectActive(this.m_enemyDeckInfo.Ship, true);
				this.m_enemyDeckInfo.Ship.sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, unitTempletBase);
				NKMUnitData shipData = NKMDungeonManager.MakeUnitDataFromID(ship.unitId, -1L, ship.unitLevel, ship.limitBreakLevel, ship.skinId, 0);
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
				if (asyncDeck.operatorUnit != null && asyncDeck.operatorUnit.id > 0)
				{
					NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(asyncDeck.operatorUnit.id);
					this.m_enemyDeckInfo.OperatorInfo.SetData(unitTempletBase2, asyncDeck.operatorUnit.level);
				}
				else
				{
					this.m_enemyDeckInfo.OperatorInfo.SetEmpty();
				}
			}
			NKCUtil.SetGameobjectActive(this.m_enemyDeckInfo.OperatorInfo, !NKCOperatorUtil.IsHide());
			bool enableShowBan = this.m_gameType == NKM_GAME_TYPE.NGT_PVP_STRATEGY || this.m_gameType == NKM_GAME_TYPE.NGT_PVP_STRATEGY_REVENGE;
			bool enableShowUpUnit = this.m_gameType == NKM_GAME_TYPE.NGT_PVP_STRATEGY || this.m_gameType == NKM_GAME_TYPE.NGT_PVP_STRATEGY_REVENGE;
			bool flag = false;
			for (int i = 0; i < this.m_enemyDeckInfo.Unit.Length; i++)
			{
				NKCDeckViewUnitSlot nkcdeckViewUnitSlot = this.m_enemyDeckInfo.Unit[i];
				if (i >= asyncDeck.units.Count)
				{
					nkcdeckViewUnitSlot.SetData(null, false);
					nkcdeckViewUnitSlot.SetLeader(false, false);
				}
				else
				{
					NKMAsyncUnitData nkmasyncUnitData = asyncDeck.units[i];
					if (nkmasyncUnitData.unitId > 0)
					{
						NKMUnitData cNKMUnitData = NKMDungeonManager.MakeUnitDataFromID(nkmasyncUnitData.unitId, -1L, nkmasyncUnitData.unitLevel, nkmasyncUnitData.limitBreakLevel, nkmasyncUnitData.skinId, nkmasyncUnitData.tacticLevel);
						nkcdeckViewUnitSlot.SetEnableShowBan(enableShowBan);
						nkcdeckViewUnitSlot.SetEnableShowUpUnit(enableShowUpUnit);
						nkcdeckViewUnitSlot.SetData(cNKMUnitData, false);
						nkcdeckViewUnitSlot.SetLeader(false, false);
					}
					else
					{
						nkcdeckViewUnitSlot.SetPrivate();
						flag = true;
					}
				}
			}
			if (asyncDeck.operationPower >= 0)
			{
				NKCUtil.SetLabelText(this.m_enemyDeckInfo.Power, asyncDeck.operationPower.ToString());
			}
			else
			{
				NKCUtil.SetLabelText(this.m_enemyDeckInfo.Power, "???");
			}
			if (flag)
			{
				NKCUtil.SetLabelText(this.m_enemyDeckInfo.AvgCost, "???");
				return;
			}
			NKCUtil.SetLabelText(this.m_enemyDeckInfo.AvgCost, string.Format("{0:0.00}", this.CalculateDeckAvgSummonCost(asyncDeck)));
		}

		// Token: 0x06008570 RID: 34160 RVA: 0x002D235C File Offset: 0x002D055C
		public void UpdateRankPVPPointUI()
		{
			NKMInventoryData inventoryData = NKCScenManager.CurrentUserData().m_InventoryData;
			long countMiscItem = inventoryData.GetCountMiscItem(301);
			int charge_POINT_MAX_COUNT = NKMPvpCommonConst.Instance.CHARGE_POINT_MAX_COUNT;
			long countMiscItem2 = inventoryData.GetCountMiscItem(6);
			int charge_POINT_ONE_STEP = NKMPvpCommonConst.Instance.CHARGE_POINT_ONE_STEP;
			int num = 0;
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				NKCCompanyBuff.IncreaseChargePointOfPvpWithBonusRatio(nkmuserData.m_companyBuffDataList, ref charge_POINT_ONE_STEP, out num);
			}
			if (countMiscItem > 0L)
			{
				NKCUtil.SetGameobjectActive(this.m_objPVPDoublePoint, true);
				NKCUtil.SetLabelText(this.m_lbPVPDoublePoint, countMiscItem.ToString());
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objPVPDoublePoint, false);
			}
			NKCUtil.SetLabelText(this.m_lbRemainPVPPoint, string.Format("{0}<color=#5d77a3>/{1}</color>", countMiscItem2, charge_POINT_MAX_COUNT));
			if (NKCScenManager.CurrentUserData().m_AsyncData == null)
			{
				return;
			}
			if (countMiscItem2 < (long)charge_POINT_MAX_COUNT)
			{
				NKCUtil.SetGameobjectActive(this.m_objRemainPVPPointPlusTime, true);
				if (num > 0)
				{
					NKCUtil.SetLabelText(this.m_lbPlusPVPPoint, string.Format("<color=#00baff>+{0}</color>", charge_POINT_ONE_STEP));
				}
				else
				{
					NKCUtil.SetLabelText(this.m_lbPlusPVPPoint, string.Format("+{0}", charge_POINT_ONE_STEP));
				}
				DateTime dateTime = new DateTime(NKCPVPManager.GetLastUpdateChargePointTicks());
				DateTime serverUTCTime = NKCSynchronizedTime.GetServerUTCTime(0.0);
				TimeSpan timeSpan = new DateTime(dateTime.Ticks + NKMPvpCommonConst.Instance.CHARGE_POINT_REFRESH_INTERVAL_TICKS) - serverUTCTime;
				if (timeSpan.TotalHours >= 1.0)
				{
					NKCUtil.SetLabelText(this.m_lbRemainPVPPointPlusTime, string.Format("{0}:{1:00}:{2:00}", (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds));
				}
				else
				{
					NKCUtil.SetLabelText(this.m_lbRemainPVPPointPlusTime, string.Format("{0}:{1:00}", timeSpan.Minutes, timeSpan.Seconds));
				}
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().ProcessPVPPointCharge(6);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objRemainPVPPointPlusTime, false);
			}
			NKCUtil.SetGameobjectActive(this.m_objBattleTicketIcon, this.m_gameType != NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC);
			NKCUtil.SetGameobjectActive(this.m_objBattleTicketCount, this.m_gameType != NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC);
		}

		// Token: 0x06008571 RID: 34161 RVA: 0x002D256F File Offset: 0x002D076F
		public override void CloseInternal()
		{
		}

		// Token: 0x06008572 RID: 34162 RVA: 0x002D2574 File Offset: 0x002D0774
		private void OnTouchDeckEdit()
		{
			NKCUIDeckViewer.DeckViewerOption options = default(NKCUIDeckViewer.DeckViewerOption);
			options.MenuName = NKCUtilString.GET_STRING_GAUNTLET;
			options.eDeckviewerMode = NKCUIDeckViewer.DeckViewerMode.AsyncPvPBattleStart;
			options.dOnSideMenuButtonConfirm = new NKCUIDeckViewer.DeckViewerOption.OnDeckSideButtonConfirm(this.OnTouchDeckSelect);
			options.dOnBackButton = new NKCUIDeckViewer.DeckViewerOption.OnBackButton(this.OnTouchDeckSelect);
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

		// Token: 0x06008573 RID: 34163 RVA: 0x002D2638 File Offset: 0x002D0838
		private void OnTouchBattle()
		{
			NKM_ERROR_CODE nkm_ERROR_CODE = this.CheckSelectDeck();
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupOKCancel.OpenOKBox(nkm_ERROR_CODE, null, "");
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (this.m_gameType != NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC && !nkmuserData.CheckPrice(1, 13))
			{
				NKCShopManager.OpenItemLackPopup(13, 1);
				return;
			}
			if (this.IsAllUnitsNotEquipedAllGears(this.m_curDeckIndex))
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_GAUNTLET_DECK_UNIT_NOT_ALL_EQUIPED_GEAR_DESC, delegate()
				{
					if (this.IsNotEnoughGauntletPoint())
					{
						NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_GAUNTLET_ASYNC_TICKET_USE_POPUP_TEXT, new NKCPopupOKCancel.OnButton(this.SendBattleStartReq), null, false);
						return;
					}
					this.SendBattleStartReq();
				}, null, false);
				return;
			}
			if (this.IsNotEnoughGauntletPoint())
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_GAUNTLET_ASYNC_TICKET_USE_POPUP_TEXT, new NKCPopupOKCancel.OnButton(this.SendBattleStartReq), null, false);
				return;
			}
			this.SendBattleStartReq();
		}

		// Token: 0x06008574 RID: 34164 RVA: 0x002D26D8 File Offset: 0x002D08D8
		private bool IsAllUnitsNotEquipedAllGears(NKMDeckIndex curDeck)
		{
			return this.m_gameType == NKM_GAME_TYPE.NGT_PVP_STRATEGY && !NKMArmyData.IsAllUnitsEquipedAllGears(curDeck);
		}

		// Token: 0x06008575 RID: 34165 RVA: 0x002D26F0 File Offset: 0x002D08F0
		private bool IsNotEnoughGauntletPoint()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			return this.m_gameType != NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC && nkmuserData.m_InventoryData.GetCountMiscItem(6) < (long)NKMPvpCommonConst.Instance.ASYNC_PVP_WIN_POINT;
		}

		// Token: 0x06008576 RID: 34166 RVA: 0x002D2728 File Offset: 0x002D0928
		private void SendBattleStartReq()
		{
			if (this.m_gameType == NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC)
			{
				NKCPacketSender.Send_NKMPacket_ASYNC_PVP_START_GAME_REQ(this.m_targetNpc.userFriendCode, this.m_curDeckIndex.m_iIndex, this.m_gameType);
				return;
			}
			NKCPacketSender.Send_NKMPacket_ASYNC_PVP_START_GAME_REQ(this.m_target.userFriendCode, this.m_curDeckIndex.m_iIndex, this.m_gameType);
		}

		// Token: 0x06008577 RID: 34167 RVA: 0x002D2784 File Offset: 0x002D0984
		public void OnRecv(NKMPacket_ASYNC_PVP_START_GAME_ACK packet)
		{
			if (packet.errorCode == NKM_ERROR_CODE.NEC_OK)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_MATCH().SetReservedGameType(this.m_gameType);
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_MATCH().SetDeckIndex(this.m_curDeckIndex.m_iIndex);
				if (this.m_gameType == NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC)
				{
					NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_MATCH().SetReservedAsyncTarget(this.m_targetNpc);
				}
				else
				{
					NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_MATCH().SetReservedAsyncTarget(this.m_target);
				}
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_MATCH, true);
				return;
			}
			if (packet.errorCode == NKM_ERROR_CODE.NEC_FAIL_ASYNC_PVP_CANNOT_FOUND_TARGET)
			{
				NKC_SCEN_GAUNTLET_LOBBY nkc_SCEN_GAUNTLET_LOBBY = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY();
				if (nkc_SCEN_GAUNTLET_LOBBY != null)
				{
					nkc_SCEN_GAUNTLET_LOBBY.SetReserved_NKM_ERROR_CODE(packet.errorCode);
				}
				this.OnBackButton();
				return;
			}
			NKCPopupOKCancel.OpenOKBox(packet.errorCode, delegate()
			{
				if (this.m_gameType == NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC)
				{
					this.SetEnemyUserInfo(this.m_targetNpc);
					this.SetEnemyDeckInfo(this.m_targetNpc.asyncDeck);
					return;
				}
				AsyncPvpTarget asyncPvpTarget = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().AsyncTargetList.Find((AsyncPvpTarget v) => v.userFriendCode == this.m_target.userFriendCode);
				if (asyncPvpTarget == null)
				{
					return;
				}
				this.SetEnemyUserInfo(asyncPvpTarget);
				this.SetEnemyDeckInfo(asyncPvpTarget.asyncDeck);
			}, "");
		}

		// Token: 0x06008578 RID: 34168 RVA: 0x002D2856 File Offset: 0x002D0A56
		private NKM_ERROR_CODE CheckSelectDeck()
		{
			return NKMMain.IsValidDeck(NKCScenManager.CurrentUserData().m_ArmyData, this.m_curDeckIndex.m_eDeckType, this.m_curDeckIndex.m_iIndex, NKM_GAME_TYPE.NGT_ASYNC_PVP);
		}

		// Token: 0x06008579 RID: 34169 RVA: 0x002D287F File Offset: 0x002D0A7F
		private void OnTouchDeckSelect(NKMDeckIndex deckIndex)
		{
			this.m_curDeckIndex = deckIndex;
			this.SetMyDeckInfo(deckIndex);
			NKCUIDeckViewer.Instance.Close();
		}

		// Token: 0x0600857A RID: 34170 RVA: 0x002D2899 File Offset: 0x002D0A99
		private void OnTouchDeckSelect()
		{
			this.OnTouchDeckSelect(this.m_curDeckIndex);
		}

		// Token: 0x0600857B RID: 34171 RVA: 0x002D28A7 File Offset: 0x002D0AA7
		public override void OnBackButton()
		{
			base.OnBackButton();
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetReservedLobbyTab(NKC_GAUNTLET_LOBBY_TAB.NGLT_ASYNC);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_LOBBY, true);
		}

		// Token: 0x0600857C RID: 34172 RVA: 0x002D28CC File Offset: 0x002D0ACC
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

		// Token: 0x0600857D RID: 34173 RVA: 0x002D293E File Offset: 0x002D0B3E
		private void Update()
		{
			if (this.m_fPrevUpdateTime + 1f < Time.time)
			{
				this.m_fPrevUpdateTime = Time.time;
				this.UpdateRankPVPPointUI();
			}
		}

		// Token: 0x040071CF RID: 29135
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_GAUNTLET";

		// Token: 0x040071D0 RID: 29136
		private const string UI_ASSET_NAME = "NKM_UI_GAUNTLET_ASYNC_READY";

		// Token: 0x040071D1 RID: 29137
		private static NKCUIManager.LoadedUIData s_LoadedUIData;

		// Token: 0x040071D2 RID: 29138
		public NKCUIGauntletAsyncReady.UserInfoUI m_myUserInfo;

		// Token: 0x040071D3 RID: 29139
		public NKCUIGauntletAsyncReady.DeckInfoUI m_myDeckInfo;

		// Token: 0x040071D4 RID: 29140
		public NKCUIGauntletAsyncReady.UserInfoUI m_enemyUserInfo;

		// Token: 0x040071D5 RID: 29141
		public NKCUIGauntletAsyncReady.DeckInfoUI m_enemyDeckInfo;

		// Token: 0x040071D6 RID: 29142
		public NKCUIComStateButton m_btnDeckEdit;

		// Token: 0x040071D7 RID: 29143
		public NKCUIComStateButton m_btnBattle;

		// Token: 0x040071D8 RID: 29144
		public GameObject m_objBattleTicketIcon;

		// Token: 0x040071D9 RID: 29145
		public GameObject m_objBattleTicketCount;

		// Token: 0x040071DA RID: 29146
		public Text m_txtGetScore;

		// Token: 0x040071DB RID: 29147
		public GameObject m_objRightShipPrivate;

		// Token: 0x040071DC RID: 29148
		[Header("포인트")]
		public GameObject m_objPVPDoublePoint;

		// Token: 0x040071DD RID: 29149
		public Text m_lbPVPDoublePoint;

		// Token: 0x040071DE RID: 29150
		public GameObject m_objPVPPoint;

		// Token: 0x040071DF RID: 29151
		public Text m_lbRemainPVPPoint;

		// Token: 0x040071E0 RID: 29152
		public GameObject m_objRemainPVPPointPlusTime;

		// Token: 0x040071E1 RID: 29153
		public Text m_lbPlusPVPPoint;

		// Token: 0x040071E2 RID: 29154
		public Text m_lbRemainPVPPointPlusTime;

		// Token: 0x040071E3 RID: 29155
		[Header("Sprite")]
		public Sprite SpriteUnitPrivate;

		// Token: 0x040071E4 RID: 29156
		[Header("etc")]
		public GameObject m_objGAUNTLET_ASYNC_READY_INFO_DESC;

		// Token: 0x040071E5 RID: 29157
		private NKMDeckIndex m_curDeckIndex;

		// Token: 0x040071E6 RID: 29158
		private AsyncPvpTarget m_target;

		// Token: 0x040071E7 RID: 29159
		private NpcPvpTarget m_targetNpc;

		// Token: 0x040071E8 RID: 29160
		private NKM_GAME_TYPE m_gameType;

		// Token: 0x040071E9 RID: 29161
		private float m_fPrevUpdateTime;

		// Token: 0x02001907 RID: 6407
		[Serializable]
		public class UserInfoUI
		{
			// Token: 0x0400AA5A RID: 43610
			public NKCUILeagueTier Tier;

			// Token: 0x0400AA5B RID: 43611
			public Text Rank;

			// Token: 0x0400AA5C RID: 43612
			public Text Score;

			// Token: 0x0400AA5D RID: 43613
			public Text Level;

			// Token: 0x0400AA5E RID: 43614
			public Text Name;

			// Token: 0x0400AA5F RID: 43615
			public Text FriendCode;

			// Token: 0x0400AA60 RID: 43616
			public GameObject Guild;

			// Token: 0x0400AA61 RID: 43617
			public NKCUIGuildBadge GuildBadge;

			// Token: 0x0400AA62 RID: 43618
			public Text GuildName;
		}

		// Token: 0x02001908 RID: 6408
		[Serializable]
		public class DeckInfoUI
		{
			// Token: 0x0400AA63 RID: 43619
			public NKCUIOperatorDeckSlot OperatorInfo;

			// Token: 0x0400AA64 RID: 43620
			public Image Ship;

			// Token: 0x0400AA65 RID: 43621
			public NKCUIShipInfoSummary ShipInfo;

			// Token: 0x0400AA66 RID: 43622
			public NKCDeckViewUnitSlot[] Unit = new NKCDeckViewUnitSlot[8];

			// Token: 0x0400AA67 RID: 43623
			public Text Power;

			// Token: 0x0400AA68 RID: 43624
			public Text AvgCost;
		}
	}
}
