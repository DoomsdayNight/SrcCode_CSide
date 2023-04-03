using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Logging;
using NKC.UI.Collection;
using NKC.UI.Guild;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B60 RID: 2912
	public class NKCPopupGauntletBattleRecord : NKCUIBase
	{
		// Token: 0x060084DA RID: 34010 RVA: 0x002CD568 File Offset: 0x002CB768
		public static NKCPopupGauntletBattleRecord OpenInstance()
		{
			NKCUIManager.LoadedUIData loadedUIData = NKCUIManager.OpenNewInstance<NKCPopupGauntletBattleRecord>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_POPUP_BATTLE_RECORD", NKCUIManager.eUIBaseRect.UIFrontCommon, null);
			NKCPopupGauntletBattleRecord instance = loadedUIData.GetInstance<NKCPopupGauntletBattleRecord>();
			if (instance != null)
			{
				instance.InitUI(loadedUIData);
			}
			return instance;
		}

		// Token: 0x060084DB RID: 34011 RVA: 0x002CD59F File Offset: 0x002CB79F
		public override void OnCloseInstance()
		{
			if (this.m_LoopScrollRect != null)
			{
				this.m_LoopScrollRect.ClearCells();
			}
		}

		// Token: 0x060084DC RID: 34012 RVA: 0x002CD5BC File Offset: 0x002CB7BC
		public void CloseInstance()
		{
			if (this.m_LoopScrollRect != null)
			{
				this.m_LoopScrollRect.ClearCells();
			}
			NKCUICollectionShipInfo.CheckInstanceAndClose();
			NKCUICollectionUnitInfo.CheckInstanceAndClose();
			NKCUICollectionOperatorInfo.CheckInstanceAndClose();
			int num = NKCAssetResourceManager.CloseResource("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_POPUP_BATTLE_RECORD");
			Log.Debug(string.Format("gauntlet battle record close resource retval is {0}", num), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Gauntlet/NKCPopupGauntletBattleRecord.cs", 53);
			base.Close();
			if (this.m_loadedUIData != null)
			{
				this.m_loadedUIData.CloseInstance();
			}
		}

		// Token: 0x17001595 RID: 5525
		// (get) Token: 0x060084DD RID: 34013 RVA: 0x002CD636 File Offset: 0x002CB836
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001596 RID: 5526
		// (get) Token: 0x060084DE RID: 34014 RVA: 0x002CD639 File Offset: 0x002CB839
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Normal;
			}
		}

		// Token: 0x17001597 RID: 5527
		// (get) Token: 0x060084DF RID: 34015 RVA: 0x002CD63C File Offset: 0x002CB83C
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return new List<int>
				{
					5,
					101
				};
			}
		}

		// Token: 0x17001598 RID: 5528
		// (get) Token: 0x060084E0 RID: 34016 RVA: 0x002CD652 File Offset: 0x002CB852
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_GAUNTLET_BATTLE_RECORD;
			}
		}

		// Token: 0x060084E1 RID: 34017 RVA: 0x002CD65C File Offset: 0x002CB85C
		public void InitUI(NKCUIManager.LoadedUIData loadedUIData)
		{
			this.m_loadedUIData = loadedUIData;
			this.m_csbtnClose.PointerClick.RemoveAllListeners();
			this.m_csbtnClose.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_LoopScrollRect.dOnGetObject += this.GetSlot;
			this.m_LoopScrollRect.dOnReturnObject += this.ReturnSlot;
			this.m_LoopScrollRect.dOnProvideData += this.ProvideData;
			this.m_LoopScrollRect.ContentConstraintCount = 1;
			NKCUtil.SetScrollHotKey(this.m_LoopScrollRect, null);
			this.m_lstNKMUnitDataMy = new List<NKMUnitData>();
			for (int i = 0; i < this.m_lstNKCDeckViewUnitSlotMy.Count; i++)
			{
				NKCDeckViewUnitSlot nkcdeckViewUnitSlot = this.m_lstNKCDeckViewUnitSlotMy[i];
				if (nkcdeckViewUnitSlot != null)
				{
					nkcdeckViewUnitSlot.Init(i, true);
				}
			}
			if (this.m_slotMyLocalBan != null)
			{
				this.m_slotMyLocalBan.Init(0, true);
			}
			for (int i = 0; i < this.m_lstMyGlobalBan.Count; i++)
			{
				NKCDeckViewUnitSlot nkcdeckViewUnitSlot2 = this.m_lstMyGlobalBan[i];
				if (nkcdeckViewUnitSlot2 != null)
				{
					nkcdeckViewUnitSlot2.Init(i, true);
				}
			}
			this.m_lstNKMUnitDataEnemy = new List<NKMUnitData>();
			for (int i = 0; i < this.m_lstNKCDeckViewUnitSlotEnemy.Count; i++)
			{
				NKCDeckViewUnitSlot nkcdeckViewUnitSlot3 = this.m_lstNKCDeckViewUnitSlotEnemy[i];
				if (nkcdeckViewUnitSlot3 != null)
				{
					nkcdeckViewUnitSlot3.Init(i, true);
				}
			}
			if (this.m_slotEnemyLocalBan != null)
			{
				this.m_slotEnemyLocalBan.Init(0, true);
			}
			for (int i = 0; i < this.m_lstEnemyGlobalBan.Count; i++)
			{
				NKCDeckViewUnitSlot nkcdeckViewUnitSlot4 = this.m_lstEnemyGlobalBan[i];
				if (nkcdeckViewUnitSlot4 != null)
				{
					nkcdeckViewUnitSlot4.Init(i, true);
				}
			}
			if (this.m_EnemyShipButton != null)
			{
				this.m_EnemyShipButton.enabled = true;
				this.m_EnemyShipButton.PointerClick.RemoveAllListeners();
				this.m_EnemyShipButton.PointerClick.AddListener(new UnityAction(this.OnSelectEnemyShip));
			}
			if (this.m_MyShipButton != null)
			{
				this.m_MyShipButton.enabled = true;
				this.m_MyShipButton.PointerClick.RemoveAllListeners();
				this.m_MyShipButton.PointerClick.AddListener(new UnityAction(this.OnSelectMyShip));
			}
			if (this.m_playReplayDataButton != null)
			{
				this.m_playReplayDataButton.enabled = true;
				this.m_playReplayDataButton.PointerClick.RemoveAllListeners();
				this.m_playReplayDataButton.PointerClick.AddListener(new UnityAction(this.OnPlayReplayData));
			}
			NKCUtil.SetGameobjectActive(this.m_playReplayDataButton, false);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060084E2 RID: 34018 RVA: 0x002CD904 File Offset: 0x002CBB04
		public void Open(NKM_GAME_TYPE pvpGameType)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			if (myUserData.m_PvpData == null)
			{
				return;
			}
			this.m_arrangedPvpHistoryList = null;
			if (pvpGameType != NKM_GAME_TYPE.NGT_PVP_RANK)
			{
				if (pvpGameType != NKM_GAME_TYPE.NGT_ASYNC_PVP)
				{
					switch (pvpGameType)
					{
					case NKM_GAME_TYPE.NGT_PVP_PRIVATE:
						this.m_arrangedPvpHistoryList = myUserData.m_PrivatePvpHistory;
						goto IL_8B;
					case NKM_GAME_TYPE.NGT_PVP_LEAGUE:
						this.m_arrangedPvpHistoryList = myUserData.m_LeaguePvpHistory;
						goto IL_8B;
					case NKM_GAME_TYPE.NGT_PVP_STRATEGY:
					case NKM_GAME_TYPE.NGT_PVP_STRATEGY_REVENGE:
					case NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC:
						break;
					default:
						this.m_arrangedPvpHistoryList = myUserData.m_SyncPvpHistory;
						goto IL_8B;
					}
				}
				this.m_arrangedPvpHistoryList = myUserData.m_AsyncPvpHistory;
			}
			else
			{
				this.m_arrangedPvpHistoryList = myUserData.m_SyncPvpHistory;
			}
			IL_8B:
			bool ui = true;
			if (this.m_arrangedPvpHistoryList == null)
			{
				ui = false;
			}
			else if (this.m_arrangedPvpHistoryList.GetCount() <= 0)
			{
				ui = false;
			}
			base.UIOpened(true);
			this.SetUI(ui);
		}

		// Token: 0x060084E3 RID: 34019 RVA: 0x002CD9C8 File Offset: 0x002CBBC8
		public void OnClickSlot(int index)
		{
			if (this.m_arrangedPvpHistoryList == null)
			{
				return;
			}
			if (this.m_arrangedPvpHistoryList.GetCount() <= index)
			{
				return;
			}
			this.m_SelectedIndex = index;
			this.SetDetailRecord(this.m_arrangedPvpHistoryList.GetData(index));
		}

		// Token: 0x060084E4 RID: 34020 RVA: 0x002CD9FC File Offset: 0x002CBBFC
		public RectTransform GetSlot(int index)
		{
			NKCPopupGauntletBRSlot newInstance = NKCPopupGauntletBRSlot.GetNewInstance(this.m_trBattleRecordSlotRoot, new NKCPopupGauntletBRSlot.OnClick(this.OnClickSlot));
			if (newInstance == null)
			{
				return null;
			}
			if (newInstance.m_ctglBRSlot != null)
			{
				newInstance.m_ctglBRSlot.SetToggleGroup(this.m_ctglGroupBRSlot);
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x060084E5 RID: 34021 RVA: 0x002CDA54 File Offset: 0x002CBC54
		public void ReturnSlot(Transform tr)
		{
			NKCPopupGauntletBRSlot component = tr.GetComponent<NKCPopupGauntletBRSlot>();
			tr.SetParent(base.transform);
			if (component != null)
			{
				component.DestoryInstance();
				return;
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x060084E6 RID: 34022 RVA: 0x002CDA90 File Offset: 0x002CBC90
		public void ProvideData(Transform tr, int index)
		{
			NKCPopupGauntletBRSlot component = tr.GetComponent<NKCPopupGauntletBRSlot>();
			if (component != null)
			{
				if (this.m_arrangedPvpHistoryList == null)
				{
					return;
				}
				if (this.m_arrangedPvpHistoryList.GetCount() <= 0)
				{
					return;
				}
				if (this.m_arrangedPvpHistoryList.GetCount() <= index)
				{
					return;
				}
				PvpSingleHistory data = this.m_arrangedPvpHistoryList.GetData(index);
				if (data != null)
				{
					component.SetUI(data, index);
				}
				if (component.m_ctglBRSlot != null)
				{
					if (this.m_SelectedIndex == index)
					{
						component.m_ctglBRSlot.Select(true, true, false);
						return;
					}
					component.m_ctglBRSlot.Select(false, true, false);
				}
			}
		}

		// Token: 0x060084E7 RID: 34023 RVA: 0x002CDB22 File Offset: 0x002CBD22
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060084E8 RID: 34024 RVA: 0x002CDB30 File Offset: 0x002CBD30
		private void SetDetailRecord(PvpSingleHistory cPvpSingleHistory)
		{
			this.m_selectedPvpSingleHistory = null;
			if (cPvpSingleHistory == null)
			{
				return;
			}
			this.m_selectedPvpSingleHistory = cPvpSingleHistory;
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			this.m_MyShipUnitData = null;
			this.m_MyOperatorData = null;
			this.m_lstNKMUnitDataMy.Clear();
			this.m_EnemyShipUnitData = null;
			this.m_EnemyOperatorUnitData = null;
			this.m_lstNKMUnitDataEnemy.Clear();
			DateTime nowUTC = new DateTime(this.m_selectedPvpSingleHistory.RegdateTick);
			int seasonID = NKCPVPManager.FindPvPSeasonID(this.m_selectedPvpSingleHistory.GameType, nowUTC);
			this.m_amtContents.Play("NKM_UI_GAUNTLET_VSRECORD_INTRO");
			this.m_NKCUILeagueTierMy.SetUI(NKCPVPManager.GetTierIconByTier(this.m_selectedPvpSingleHistory.GameType, seasonID, this.m_selectedPvpSingleHistory.MyTier), NKCPVPManager.GetTierNumberByTier(this.m_selectedPvpSingleHistory.GameType, seasonID, this.m_selectedPvpSingleHistory.MyTier));
			this.m_lbMyLevel.text = NKCStringTable.GetString("SI_DP_LEVEL_ONE_PARAM", false, new object[]
			{
				this.m_selectedPvpSingleHistory.MyUserLevel
			});
			this.m_lbMyUserNickName.text = NKCUtilString.GetUserNickname(myUserData.m_UserNickName, false);
			this.m_lbMyUID.text = NKCUtilString.GetFriendCode(myUserData.m_FriendCode, false);
			this.m_lbMyScore.text = this.m_selectedPvpSingleHistory.MyScore.ToString();
			if (this.m_objMyGuild != null)
			{
				NKCUtil.SetGameobjectActive(this.m_objMyGuild, this.m_selectedPvpSingleHistory.SourceGuildUid > 0L);
				if (this.m_objMyGuild.activeSelf)
				{
					this.m_MyBadgeUI.SetData(this.m_selectedPvpSingleHistory.SourceGuildBadgeId, false);
					NKCUtil.SetLabelText(this.m_lbMyGuildName, NKCUtilString.GetUserGuildName(this.m_selectedPvpSingleHistory.SourceGuildName, false));
				}
			}
			this.m_lbMyAddScore.text = "";
			if (this.m_selectedPvpSingleHistory.Result == PVP_RESULT.WIN)
			{
				this.m_lbMyGameResult.text = NKCUtilString.GET_STRING_WIN;
				this.m_lbMyGameResult.color = NKCUtil.GetColor("#FFDF5D");
			}
			else if (this.m_selectedPvpSingleHistory.Result == PVP_RESULT.LOSE)
			{
				this.m_lbMyGameResult.text = NKCUtilString.GET_STRING_LOSE;
				this.m_lbMyGameResult.color = NKCUtil.GetColor("#FF4747");
			}
			else if (this.m_selectedPvpSingleHistory.Result == PVP_RESULT.DRAW)
			{
				this.m_lbMyGameResult.text = NKCUtilString.GET_STRING_DRAW;
				this.m_lbMyGameResult.color = NKCUtil.GetColor("#D4D4D4");
			}
			if (this.m_selectedPvpSingleHistory.MyDeckData.ship != null)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_selectedPvpSingleHistory.MyDeckData.ship.unitId);
				if (unitTempletBase != null)
				{
					this.m_imgMyShip.sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, unitTempletBase);
					this.m_MyShipUnitData = new NKMUnitData();
					this.m_MyShipUnitData.FillDataFromAsyncUnitData(this.m_selectedPvpSingleHistory.MyDeckData.ship);
					this.m_My_NKCUIShipInfoSummary.SetShipData(this.m_MyShipUnitData, unitTempletBase, false);
				}
			}
			if (NKCOperatorUtil.IsHide())
			{
				NKCUtil.SetGameobjectActive(this.m_MyOperator, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_MyOperator, true);
				if (!NKCOperatorUtil.IsActive())
				{
					this.m_MyOperator.SetLock();
				}
				else if (this.m_selectedPvpSingleHistory.MyDeckData.operatorUnit != null)
				{
					NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(this.m_selectedPvpSingleHistory.MyDeckData.operatorUnit.id);
					if (unitTempletBase2 != null)
					{
						this.m_MyOperatorData = new NKMOperator();
						this.m_MyOperatorData = this.m_selectedPvpSingleHistory.MyDeckData.operatorUnit;
						this.m_MyOperator.Init(new NKCUIOperatorDeckSlot.OnSelectOperator(this.OnSelectMyOperator));
						this.m_MyOperator.SetData(unitTempletBase2, this.m_selectedPvpSingleHistory.MyDeckData.operatorUnit.level);
					}
					else
					{
						this.m_MyOperator.SetEmpty();
					}
				}
				else
				{
					this.m_MyOperator.SetEmpty();
				}
			}
			for (int i = 0; i < this.m_lstNKCDeckViewUnitSlotMy.Count; i++)
			{
				if (i < 8)
				{
					NKMUnitData nkmunitData = null;
					NKMAsyncDeckData myDeckData = this.m_selectedPvpSingleHistory.MyDeckData;
					if (myDeckData.units[i] != null)
					{
						nkmunitData = new NKMUnitData();
						nkmunitData.FillDataFromAsyncUnitData(myDeckData.units[i]);
						nkmunitData.m_UnitUID = (long)i;
					}
					NKCDeckViewUnitSlot viewUnitSlot = this.m_lstNKCDeckViewUnitSlotMy[i];
					viewUnitSlot.SetData(nkmunitData, true);
					viewUnitSlot.SetUpBanData(nkmunitData, myDeckData.unitBanData, myDeckData.unitUpData, i == (int)myDeckData.leaderIndex);
					viewUnitSlot.SetUpBanData(nkmunitData, myDeckData.unitBanData, myDeckData.unitUpData, (int)this.m_selectedPvpSingleHistory.MyDeckData.leaderIndex == i);
					viewUnitSlot.m_NKCUIComButton.PointerClick.RemoveAllListeners();
					viewUnitSlot.m_NKCUIComButton.PointerClick.AddListener(delegate()
					{
						this.OnClickMyViewUnitSlot(viewUnitSlot.m_Index);
					});
					this.m_lstNKMUnitDataMy.Add(nkmunitData);
				}
			}
			bool flag = false;
			if (this.m_selectedPvpSingleHistory.GameType == NKM_GAME_TYPE.NGT_PVP_LEAGUE)
			{
				flag = true;
			}
			else if ((this.m_selectedPvpSingleHistory.myBanUnitIds.Count > 0 && this.m_selectedPvpSingleHistory.myBanUnitIds[0] != 0) || (this.m_selectedPvpSingleHistory.targetBanUnitIds.Count > 0 && this.m_selectedPvpSingleHistory.targetBanUnitIds[0] != 0))
			{
				flag = true;
			}
			NKCUtil.SetGameobjectActive(this.m_objMyLocalBan, flag);
			NKCUtil.SetGameobjectActive(this.m_objMyGlobalBan, flag);
			if (flag)
			{
				if (this.m_slotMyLocalBan != null)
				{
					NKMUnitData nkmunitData2 = null;
					if (this.m_selectedPvpSingleHistory.MyDeckData.banishedUnit != null)
					{
						nkmunitData2 = new NKMUnitData();
						nkmunitData2.FillDataFromAsyncUnitData(this.m_selectedPvpSingleHistory.MyDeckData.banishedUnit);
						nkmunitData2.m_UnitUID = (long)this.m_selectedPvpSingleHistory.MyDeckData.units.Count;
					}
					this.m_slotMyLocalBan.SetData(nkmunitData2, false);
					this.m_slotMyLocalBan.m_NKCUIComButton.PointerClick.RemoveAllListeners();
					this.m_slotMyLocalBan.SetLeagueBan(true);
					this.m_lstNKMUnitDataMy.Add(nkmunitData2);
				}
				for (int j = 0; j < this.m_lstMyGlobalBan.Count; j++)
				{
					if (j < this.m_selectedPvpSingleHistory.myBanUnitIds.Count)
					{
						NKMUnitData nkmunitData3 = null;
						if (this.m_selectedPvpSingleHistory.myBanUnitIds[j] != 0)
						{
							nkmunitData3 = new NKMUnitData(this.m_selectedPvpSingleHistory.myBanUnitIds[j], (long)j, false, false, false, false);
						}
						NKCDeckViewUnitSlot nkcdeckViewUnitSlot = this.m_lstMyGlobalBan[j];
						nkcdeckViewUnitSlot.SetData(nkmunitData3, false);
						nkcdeckViewUnitSlot.m_NKCUIComButton.PointerClick.RemoveAllListeners();
						nkcdeckViewUnitSlot.SetLeagueBan(true);
						nkcdeckViewUnitSlot.SetEnableShowLevelText(false);
						this.m_lstNKMUnitDataMy.Add(nkmunitData3);
					}
				}
			}
			this.m_NKCUILeagueTierEnemy.SetUI(NKCPVPManager.GetTierIconByTier(this.m_selectedPvpSingleHistory.GameType, seasonID, this.m_selectedPvpSingleHistory.TargetTier), NKCPVPManager.GetTierNumberByTier(this.m_selectedPvpSingleHistory.GameType, seasonID, this.m_selectedPvpSingleHistory.TargetTier));
			this.m_lbEnemyLevel.text = NKCStringTable.GetString("SI_DP_LEVEL_ONE_PARAM", new object[]
			{
				this.m_selectedPvpSingleHistory.TargetUserLevel
			});
			this.m_lbEnemyUserNickName.text = NKCUtilString.GetUserNickname(this.m_selectedPvpSingleHistory.TargetNickName, true);
			this.m_lbEnemyUID.text = NKCUtilString.GetFriendCode(this.m_selectedPvpSingleHistory.TargetFriendCode, true);
			this.m_lbEnemyScore.text = this.m_selectedPvpSingleHistory.TargetScore.ToString();
			if (this.m_objEnemyGuild != null)
			{
				NKCUtil.SetGameobjectActive(this.m_objEnemyGuild, this.m_selectedPvpSingleHistory.TargetGuildUid > 0L);
				if (this.m_objEnemyGuild.activeSelf)
				{
					this.m_EnemyBadgeUI.SetData(this.m_selectedPvpSingleHistory.TargetGuildBadgeId, true);
					NKCUtil.SetLabelText(this.m_lbEnemyGuildName, NKCUtilString.GetUserGuildName(this.m_selectedPvpSingleHistory.TargetGuildName, true));
				}
			}
			this.m_lbEnemyAddScore.text = "";
			if (this.m_selectedPvpSingleHistory.Result == PVP_RESULT.LOSE)
			{
				this.m_lbEnemyGameResult.text = NKCUtilString.GET_STRING_WIN;
				this.m_lbEnemyGameResult.color = NKCUtil.GetColor("#FFDF5D");
			}
			else if (this.m_selectedPvpSingleHistory.Result == PVP_RESULT.WIN)
			{
				this.m_lbEnemyGameResult.text = NKCUtilString.GET_STRING_LOSE;
				this.m_lbEnemyGameResult.color = NKCUtil.GetColor("#FF4747");
			}
			else if (this.m_selectedPvpSingleHistory.Result == PVP_RESULT.DRAW)
			{
				this.m_lbEnemyGameResult.text = NKCUtilString.GET_STRING_DRAW;
				this.m_lbEnemyGameResult.color = NKCUtil.GetColor("#D4D4D4");
			}
			NKM_GAME_TYPE gameType = this.m_selectedPvpSingleHistory.GameType;
			if (gameType != NKM_GAME_TYPE.NGT_PVP_RANK)
			{
				if (gameType != NKM_GAME_TYPE.NGT_ASYNC_PVP)
				{
					switch (gameType)
					{
					case NKM_GAME_TYPE.NGT_PVP_PRIVATE:
						this.m_imgModeBG.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_GAUNTLET_SPRITE", "AB_UI_NKM_UI_GAUNTLET_ELLIPSE_NORMAL", false);
						NKCUtil.SetLabelText(this.m_lbMode, NKCUtilString.GET_STRING_PRIVATE_PVP);
						NKCUtil.SetLabelTextColor(this.m_lbMode, NKCUtil.GetColor("#FFFFFFFF"));
						NKCUtil.SetGameobjectActive(this.m_lbMyScore, false);
						NKCUtil.SetGameobjectActive(this.m_lbEnemyScore, false);
						goto IL_A5B;
					case NKM_GAME_TYPE.NGT_PVP_LEAGUE:
						this.m_imgModeBG.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_GAUNTLET_SPRITE", "AB_UI_NKM_UI_GAUNTLET_ELLIPSE_LEAGUE", false);
						NKCUtil.SetLabelText(this.m_lbMode, NKCUtilString.GET_STRING_GAUNTLET_LEAGUE_GAME);
						NKCUtil.SetLabelTextColor(this.m_lbMode, NKCUtil.GetColor("#FFFFFFFF"));
						NKCUtil.SetGameobjectActive(this.m_lbMyScore, true);
						NKCUtil.SetGameobjectActive(this.m_lbEnemyScore, true);
						goto IL_A5B;
					case NKM_GAME_TYPE.NGT_PVP_STRATEGY:
					case NKM_GAME_TYPE.NGT_PVP_STRATEGY_REVENGE:
					case NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC:
						break;
					default:
						NKCUtil.SetGameobjectActive(this.m_lbMyScore, false);
						NKCUtil.SetGameobjectActive(this.m_lbEnemyScore, false);
						goto IL_A5B;
					}
				}
				this.m_imgModeBG.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_GAUNTLET_SPRITE", "AB_UI_NKM_UI_GAUNTLET_ELLIPSE_ASYNCMAYCH", false);
				NKCUtil.SetLabelText(this.m_lbMode, NKCUtilString.GET_STRING_GAUNTLET_ASYNC_GAME);
				NKCUtil.SetLabelTextColor(this.m_lbMode, NKCUtil.GetColor("#FFFFFFFF"));
				NKCUtil.SetGameobjectActive(this.m_lbMyScore, true);
				NKCUtil.SetGameobjectActive(this.m_lbEnemyScore, true);
			}
			else
			{
				this.m_imgModeBG.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_GAUNTLET_SPRITE", "AB_UI_NKM_UI_GAUNTLET_ELLIPSE_RANK", false);
				NKCUtil.SetLabelText(this.m_lbMode, NKCUtilString.GET_STRING_GAUNTLET_RANK_GAME);
				NKCUtil.SetLabelTextColor(this.m_lbMode, NKCUtil.GetColor("#FFFFFFFF"));
				NKCUtil.SetGameobjectActive(this.m_lbMyScore, true);
				NKCUtil.SetGameobjectActive(this.m_lbEnemyScore, true);
			}
			IL_A5B:
			if (this.m_selectedPvpSingleHistory.TargetDeckData.ship != null)
			{
				NKMUnitTempletBase unitTempletBase3 = NKMUnitManager.GetUnitTempletBase(this.m_selectedPvpSingleHistory.TargetDeckData.ship.unitId);
				if (unitTempletBase3 != null)
				{
					this.m_imgEnemyShip.sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, unitTempletBase3);
				}
				this.m_EnemyShipUnitData = new NKMUnitData();
				this.m_EnemyShipUnitData.FillDataFromAsyncUnitData(this.m_selectedPvpSingleHistory.TargetDeckData.ship);
				this.m_Enemy_NKCUIShipInfoSummary.SetShipData(this.m_EnemyShipUnitData, unitTempletBase3, false);
			}
			if (NKCOperatorUtil.IsHide())
			{
				NKCUtil.SetGameobjectActive(this.m_EnemyOperator, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_EnemyOperator, true);
				if (!NKCOperatorUtil.IsActive())
				{
					this.m_EnemyOperator.SetLock();
				}
				else if (this.m_selectedPvpSingleHistory.TargetDeckData.operatorUnit != null)
				{
					NKMUnitTempletBase unitTempletBase4 = NKMUnitManager.GetUnitTempletBase(this.m_selectedPvpSingleHistory.TargetDeckData.operatorUnit.id);
					if (unitTempletBase4 != null)
					{
						this.m_EnemyOperatorUnitData = new NKMOperator();
						this.m_EnemyOperatorUnitData = this.m_selectedPvpSingleHistory.TargetDeckData.operatorUnit;
						this.m_EnemyOperator.Init(new NKCUIOperatorDeckSlot.OnSelectOperator(this.OnSelectEnemyOperator));
						this.m_EnemyOperator.SetData(unitTempletBase4, this.m_selectedPvpSingleHistory.TargetDeckData.operatorUnit.level);
					}
					else
					{
						this.m_EnemyOperator.SetEmpty();
					}
				}
				else
				{
					this.m_EnemyOperator.SetEmpty();
				}
			}
			for (int k = 0; k < this.m_lstNKCDeckViewUnitSlotEnemy.Count; k++)
			{
				if (k < 8)
				{
					NKMUnitData nkmunitData4 = null;
					NKMAsyncDeckData targetDeckData = this.m_selectedPvpSingleHistory.TargetDeckData;
					if (targetDeckData.units.Count > k && targetDeckData.units[k] != null)
					{
						nkmunitData4 = new NKMUnitData();
						nkmunitData4.FillDataFromAsyncUnitData(targetDeckData.units[k]);
						nkmunitData4.m_UnitUID = (long)k;
					}
					NKCDeckViewUnitSlot viewUnitSlot = this.m_lstNKCDeckViewUnitSlotEnemy[k];
					viewUnitSlot.SetData(nkmunitData4, true);
					viewUnitSlot.SetUpBanData(nkmunitData4, targetDeckData.unitBanData, targetDeckData.unitUpData, (int)this.m_selectedPvpSingleHistory.TargetDeckData.leaderIndex == k);
					viewUnitSlot.m_NKCUIComButton.PointerClick.RemoveAllListeners();
					viewUnitSlot.m_NKCUIComButton.PointerClick.AddListener(delegate()
					{
						this.OnClickEnemyViewUnitSlot(viewUnitSlot.m_Index);
					});
					this.m_lstNKMUnitDataEnemy.Add(nkmunitData4);
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objEnemyLocalBan, flag);
			NKCUtil.SetGameobjectActive(this.m_objEnemyGlobalBan, flag);
			if (flag)
			{
				if (this.m_slotEnemyLocalBan != null)
				{
					NKMUnitData nkmunitData5 = null;
					if (this.m_selectedPvpSingleHistory.TargetDeckData.banishedUnit != null)
					{
						nkmunitData5 = new NKMUnitData();
						nkmunitData5.FillDataFromAsyncUnitData(this.m_selectedPvpSingleHistory.TargetDeckData.banishedUnit);
						nkmunitData5.m_UnitUID = (long)this.m_selectedPvpSingleHistory.TargetDeckData.units.Count;
					}
					this.m_slotEnemyLocalBan.SetData(nkmunitData5, false);
					this.m_slotEnemyLocalBan.m_NKCUIComButton.PointerClick.RemoveAllListeners();
					this.m_slotEnemyLocalBan.SetLeagueBan(true);
					this.m_lstNKMUnitDataEnemy.Add(nkmunitData5);
				}
				for (int l = 0; l < this.m_lstEnemyGlobalBan.Count; l++)
				{
					if (l < this.m_selectedPvpSingleHistory.targetBanUnitIds.Count)
					{
						NKMUnitData nkmunitData6 = null;
						if (this.m_selectedPvpSingleHistory.targetBanUnitIds[l] != 0)
						{
							nkmunitData6 = new NKMUnitData(this.m_selectedPvpSingleHistory.targetBanUnitIds[l], (long)l, false, false, false, false);
						}
						NKCDeckViewUnitSlot nkcdeckViewUnitSlot2 = this.m_lstEnemyGlobalBan[l];
						nkcdeckViewUnitSlot2.SetData(nkmunitData6, false);
						nkcdeckViewUnitSlot2.m_NKCUIComButton.PointerClick.RemoveAllListeners();
						nkcdeckViewUnitSlot2.SetLeagueBan(true);
						nkcdeckViewUnitSlot2.SetEnableShowLevelText(false);
						this.m_lstNKMUnitDataEnemy.Add(nkmunitData6);
					}
				}
			}
			NKCUtil.SetGameobjectActive(this.m_playReplayDataButton, this.CheckForReplayData());
		}

		// Token: 0x060084E9 RID: 34025 RVA: 0x002CE998 File Offset: 0x002CCB98
		private void SetUI(bool bExistHistory)
		{
			if (!bExistHistory)
			{
				NKCUtil.SetGameobjectActive(this.m_objNoHistory, true);
				NKCUtil.SetGameobjectActive(this.m_objContents, false);
				return;
			}
			if (this.m_bFirstInitLoopScroll)
			{
				this.m_LoopScrollRect.PrepareCells(0);
			}
			this.m_bFirstInitLoopScroll = false;
			NKCUtil.SetGameobjectActive(this.m_objNoHistory, false);
			NKCUtil.SetGameobjectActive(this.m_objContents, true);
			if (this.m_arrangedPvpHistoryList == null)
			{
				return;
			}
			if (this.m_arrangedPvpHistoryList.GetCount() <= 0)
			{
				return;
			}
			this.m_SelectedIndex = 0;
			this.m_arrangedPvpHistoryList.Sort();
			this.m_LoopScrollRect.TotalCount = this.m_arrangedPvpHistoryList.GetCount();
			this.m_LoopScrollRect.velocity = new Vector2(0f, 0f);
			this.m_LoopScrollRect.SetIndexPosition(0);
			this.SetDetailRecord(this.m_arrangedPvpHistoryList.GetData(this.m_SelectedIndex));
		}

		// Token: 0x060084EA RID: 34026 RVA: 0x002CEA74 File Offset: 0x002CCC74
		public void OnClickMyViewUnitSlot(int index)
		{
			this.SelectDeckViewUnit(index, this.m_lstNKCDeckViewUnitSlotMy, this.m_lstNKMUnitDataMy, this.m_selectedPvpSingleHistory.MyDeckData.equips);
		}

		// Token: 0x060084EB RID: 34027 RVA: 0x002CEA99 File Offset: 0x002CCC99
		public void OnClickEnemyViewUnitSlot(int index)
		{
			this.SelectDeckViewUnit(index, this.m_lstNKCDeckViewUnitSlotEnemy, this.m_lstNKMUnitDataEnemy, this.m_selectedPvpSingleHistory.TargetDeckData.equips);
		}

		// Token: 0x060084EC RID: 34028 RVA: 0x002CEAC0 File Offset: 0x002CCCC0
		public void SelectDeckViewUnit(int selectedIndex, List<NKCDeckViewUnitSlot> listNKCDeckViewUnitSlot, List<NKMUnitData> listNKMUnitData, List<NKMEquipItemData> listNKMEquipItemData)
		{
			for (int i = 0; i < listNKCDeckViewUnitSlot.Count; i++)
			{
				NKCDeckViewUnitSlot nkcdeckViewUnitSlot = listNKCDeckViewUnitSlot[i];
				if (i != selectedIndex)
				{
					nkcdeckViewUnitSlot.ButtonDeSelect(false, false);
				}
				else if (!this.IsDetailHistoryOpened())
				{
					nkcdeckViewUnitSlot.ButtonDeSelect(false, false);
				}
				else
				{
					nkcdeckViewUnitSlot.ButtonSelect();
					NKCUIUnitInfo.OpenOption openOption = new NKCUIUnitInfo.OpenOption(listNKMUnitData, i);
					NKCUICollectionUnitInfo.CheckInstanceAndOpen(nkcdeckViewUnitSlot.m_NKMUnitData, openOption, listNKMEquipItemData, NKCUICollectionUnitInfo.eCollectionState.CS_STATUS, true, NKCUIUpsideMenu.eMode.Normal);
				}
			}
		}

		// Token: 0x060084ED RID: 34029 RVA: 0x002CEB25 File Offset: 0x002CCD25
		public void OnSelectMyOperator(long operatorUID)
		{
			if (this.m_MyOperatorData == null)
			{
				return;
			}
			if (!this.IsDetailHistoryOpened())
			{
				return;
			}
			NKCUICollectionOperatorInfo.Instance.Open(this.m_MyOperatorData, null, NKCUICollectionOperatorInfo.eCollectionState.CS_STATUS, NKCUIUpsideMenu.eMode.Normal, true, false);
		}

		// Token: 0x060084EE RID: 34030 RVA: 0x002CEB4E File Offset: 0x002CCD4E
		public void OnSelectEnemyOperator(long operatorUID)
		{
			if (this.m_EnemyOperatorUnitData == null)
			{
				return;
			}
			if (!this.IsDetailHistoryOpened())
			{
				return;
			}
			NKCUICollectionOperatorInfo.Instance.Open(this.m_EnemyOperatorUnitData, null, NKCUICollectionOperatorInfo.eCollectionState.CS_STATUS, NKCUIUpsideMenu.eMode.Normal, true, false);
		}

		// Token: 0x060084EF RID: 34031 RVA: 0x002CEB77 File Offset: 0x002CCD77
		public void OnSelectMyShip()
		{
			if (this.m_MyShipUnitData == null)
			{
				return;
			}
			if (!this.IsDetailHistoryOpened())
			{
				return;
			}
			NKCUICollectionShipInfo.Instance.Open(this.m_MyShipUnitData, NKMDeckIndex.None, null, null, true);
		}

		// Token: 0x060084F0 RID: 34032 RVA: 0x002CEBA3 File Offset: 0x002CCDA3
		public void OnSelectEnemyShip()
		{
			if (this.m_EnemyShipUnitData == null)
			{
				return;
			}
			if (!this.IsDetailHistoryOpened())
			{
				return;
			}
			NKCUICollectionShipInfo.Instance.Open(this.m_EnemyShipUnitData, NKMDeckIndex.None, null, this.m_selectedPvpSingleHistory.TargetDeckData.equips, true);
		}

		// Token: 0x060084F1 RID: 34033 RVA: 0x002CEBDE File Offset: 0x002CCDDE
		public bool IsDetailHistoryOpened()
		{
			return NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_DETAIL_HISTORY);
		}

		// Token: 0x060084F2 RID: 34034 RVA: 0x002CEBE6 File Offset: 0x002CCDE6
		public void OnPlayReplayData()
		{
			if (this.m_selectedPvpSingleHistory == null)
			{
				return;
			}
			NKCReplayMgr.GetNKCReplaMgr().StartPlaying(this.m_selectedPvpSingleHistory.gameUid);
		}

		// Token: 0x060084F3 RID: 34035 RVA: 0x002CEC08 File Offset: 0x002CCE08
		public bool CheckForReplayData()
		{
			return this.m_selectedPvpSingleHistory != null && NKCReplayMgr.IsReplayOpened() && this.m_selectedPvpSingleHistory.GameType != NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC && this.m_selectedPvpSingleHistory.GameType != NKM_GAME_TYPE.NGT_PVP_STRATEGY_REVENGE && NKCReplayMgr.GetNKCReplaMgr().IsInReplayDataFileList(this.m_selectedPvpSingleHistory.gameUid);
		}

		// Token: 0x04007128 RID: 28968
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_GAUNTLET";

		// Token: 0x04007129 RID: 28969
		private const string UI_ASSET_NAME = "NKM_UI_GAUNTLET_POPUP_BATTLE_RECORD";

		// Token: 0x0400712A RID: 28970
		private NKCUIManager.LoadedUIData m_loadedUIData;

		// Token: 0x0400712B RID: 28971
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x0400712C RID: 28972
		public Animator m_amtContents;

		// Token: 0x0400712D RID: 28973
		public GameObject m_objContents;

		// Token: 0x0400712E RID: 28974
		public Transform m_trBattleRecordSlotRoot;

		// Token: 0x0400712F RID: 28975
		public NKCUIComToggleGroup m_ctglGroupBRSlot;

		// Token: 0x04007130 RID: 28976
		public LoopScrollRect m_LoopScrollRect;

		// Token: 0x04007131 RID: 28977
		public Image m_imgModeBG;

		// Token: 0x04007132 RID: 28978
		public Text m_lbMode;

		// Token: 0x04007133 RID: 28979
		[Header("내정보")]
		public NKCUILeagueTier m_NKCUILeagueTierMy;

		// Token: 0x04007134 RID: 28980
		public Text m_lbMyLevel;

		// Token: 0x04007135 RID: 28981
		public Text m_lbMyUserNickName;

		// Token: 0x04007136 RID: 28982
		public Text m_lbMyUID;

		// Token: 0x04007137 RID: 28983
		public Text m_lbMyGameResult;

		// Token: 0x04007138 RID: 28984
		public Text m_lbMyScore;

		// Token: 0x04007139 RID: 28985
		public Text m_lbMyAddScore;

		// Token: 0x0400713A RID: 28986
		public Image m_imgMyShip;

		// Token: 0x0400713B RID: 28987
		public NKCUIOperatorDeckSlot m_MyOperator;

		// Token: 0x0400713C RID: 28988
		public NKCUIComButton m_MyShipButton;

		// Token: 0x0400713D RID: 28989
		public NKCUIShipInfoSummary m_My_NKCUIShipInfoSummary;

		// Token: 0x0400713E RID: 28990
		public GameObject m_objMyUnitRoot;

		// Token: 0x0400713F RID: 28991
		public List<NKCDeckViewUnitSlot> m_lstNKCDeckViewUnitSlotMy;

		// Token: 0x04007140 RID: 28992
		public GameObject m_objMyGuild;

		// Token: 0x04007141 RID: 28993
		public NKCUIGuildBadge m_MyBadgeUI;

		// Token: 0x04007142 RID: 28994
		public Text m_lbMyGuildName;

		// Token: 0x04007143 RID: 28995
		public GameObject m_objMyLocalBan;

		// Token: 0x04007144 RID: 28996
		public NKCDeckViewUnitSlot m_slotMyLocalBan;

		// Token: 0x04007145 RID: 28997
		public GameObject m_objMyGlobalBan;

		// Token: 0x04007146 RID: 28998
		public List<NKCDeckViewUnitSlot> m_lstMyGlobalBan;

		// Token: 0x04007147 RID: 28999
		[Header("상대정보")]
		public NKCUILeagueTier m_NKCUILeagueTierEnemy;

		// Token: 0x04007148 RID: 29000
		public Text m_lbEnemyLevel;

		// Token: 0x04007149 RID: 29001
		public Text m_lbEnemyUserNickName;

		// Token: 0x0400714A RID: 29002
		public Text m_lbEnemyUID;

		// Token: 0x0400714B RID: 29003
		public Text m_lbEnemyGameResult;

		// Token: 0x0400714C RID: 29004
		public Text m_lbEnemyScore;

		// Token: 0x0400714D RID: 29005
		public Text m_lbEnemyAddScore;

		// Token: 0x0400714E RID: 29006
		public Image m_imgEnemyShip;

		// Token: 0x0400714F RID: 29007
		public NKCUIOperatorDeckSlot m_EnemyOperator;

		// Token: 0x04007150 RID: 29008
		public NKCUIComButton m_EnemyShipButton;

		// Token: 0x04007151 RID: 29009
		public NKCUIShipInfoSummary m_Enemy_NKCUIShipInfoSummary;

		// Token: 0x04007152 RID: 29010
		public GameObject m_objEnemyUnitRoot;

		// Token: 0x04007153 RID: 29011
		public List<NKCDeckViewUnitSlot> m_lstNKCDeckViewUnitSlotEnemy;

		// Token: 0x04007154 RID: 29012
		public GameObject m_objEnemyGuild;

		// Token: 0x04007155 RID: 29013
		public NKCUIGuildBadge m_EnemyBadgeUI;

		// Token: 0x04007156 RID: 29014
		public Text m_lbEnemyGuildName;

		// Token: 0x04007157 RID: 29015
		public GameObject m_objEnemyLocalBan;

		// Token: 0x04007158 RID: 29016
		public NKCDeckViewUnitSlot m_slotEnemyLocalBan;

		// Token: 0x04007159 RID: 29017
		public GameObject m_objEnemyGlobalBan;

		// Token: 0x0400715A RID: 29018
		public List<NKCDeckViewUnitSlot> m_lstEnemyGlobalBan;

		// Token: 0x0400715B RID: 29019
		[Header("")]
		public GameObject m_objNoHistory;

		// Token: 0x0400715C RID: 29020
		public PvpHistoryList m_arrangedPvpHistoryList;

		// Token: 0x0400715D RID: 29021
		public int m_SelectedIndex;

		// Token: 0x0400715E RID: 29022
		private PvpSingleHistory m_selectedPvpSingleHistory;

		// Token: 0x0400715F RID: 29023
		private bool m_bFirstInitLoopScroll = true;

		// Token: 0x04007160 RID: 29024
		private NKMUnitData m_MyShipUnitData;

		// Token: 0x04007161 RID: 29025
		private NKMOperator m_MyOperatorData;

		// Token: 0x04007162 RID: 29026
		private List<NKMUnitData> m_lstNKMUnitDataMy;

		// Token: 0x04007163 RID: 29027
		private NKMUnitData m_EnemyShipUnitData;

		// Token: 0x04007164 RID: 29028
		private NKMOperator m_EnemyOperatorUnitData;

		// Token: 0x04007165 RID: 29029
		private List<NKMUnitData> m_lstNKMUnitDataEnemy;

		// Token: 0x04007166 RID: 29030
		public NKCUIComStateButton m_playReplayDataButton;
	}
}
