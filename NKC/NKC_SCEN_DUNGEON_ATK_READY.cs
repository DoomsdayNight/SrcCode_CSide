using System;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.Guild;
using NKC.PacketHandler;
using NKC.UI;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000709 RID: 1801
	public class NKC_SCEN_DUNGEON_ATK_READY : NKC_SCEN_BASIC
	{
		// Token: 0x0600469F RID: 18079 RVA: 0x00156B0D File Offset: 0x00154D0D
		public NKMDeckIndex GetLastDeckIndex()
		{
			return this.m_LastDeckIndex;
		}

		// Token: 0x060046A0 RID: 18080 RVA: 0x00156B15 File Offset: 0x00154D15
		public NKMEventDeckData GetLastEventDeck()
		{
			return this.m_SelectedEventDeck;
		}

		// Token: 0x060046A1 RID: 18081 RVA: 0x00156B1D File Offset: 0x00154D1D
		public int GetLastMultiplyRewardCount()
		{
			return this.m_LastMultiplyRewardCount;
		}

		// Token: 0x060046A2 RID: 18082 RVA: 0x00156B28 File Offset: 0x00154D28
		public NKC_SCEN_DUNGEON_ATK_READY()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_DUNGEON_ATK_READY;
		}

		// Token: 0x060046A3 RID: 18083 RVA: 0x00156B84 File Offset: 0x00154D84
		public void DoAfterLogout()
		{
			this.m_LastDeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_NONE, 0);
			this.m_SelectedDeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_NONE, 0);
			this.m_SelectedEventDeck = null;
			this.m_LastMultiplyRewardCount = 1;
			this.m_bOperationSkip = false;
			this.m_StageTemplet = null;
			this.m_eventDeckIndex = 0;
			this.m_deckContents = DeckContents.NORMAL;
			this.m_BGMName = "";
		}

		// Token: 0x060046A4 RID: 18084 RVA: 0x00156BE0 File Offset: 0x00154DE0
		public int GetEpisodeID()
		{
			if (this.m_StageTemplet == null)
			{
				return 0;
			}
			return this.m_StageTemplet.EpisodeId;
		}

		// Token: 0x060046A5 RID: 18085 RVA: 0x00156BF7 File Offset: 0x00154DF7
		public EPISODE_DIFFICULTY GetEpisodeDifficulty()
		{
			if (this.m_StageTemplet == null)
			{
				return EPISODE_DIFFICULTY.NORMAL;
			}
			return this.m_StageTemplet.m_Difficulty;
		}

		// Token: 0x060046A6 RID: 18086 RVA: 0x00156C0E File Offset: 0x00154E0E
		public int GetActID()
		{
			if (this.m_StageTemplet == null)
			{
				return 0;
			}
			return this.m_StageTemplet.ActId;
		}

		// Token: 0x060046A7 RID: 18087 RVA: 0x00156C25 File Offset: 0x00154E25
		public int GetStageIndex()
		{
			if (this.m_StageTemplet == null)
			{
				return 0;
			}
			return this.m_StageTemplet.m_StageIndex;
		}

		// Token: 0x060046A8 RID: 18088 RVA: 0x00156C3C File Offset: 0x00154E3C
		public int GetStageUIIndex()
		{
			if (this.m_StageTemplet == null)
			{
				return 0;
			}
			return this.m_StageTemplet.m_StageUINum;
		}

		// Token: 0x060046A9 RID: 18089 RVA: 0x00156C53 File Offset: 0x00154E53
		public NKMStageTempletV2 GetStageTemplet()
		{
			return this.m_StageTemplet;
		}

		// Token: 0x060046AA RID: 18090 RVA: 0x00156C5B File Offset: 0x00154E5B
		public NKMDungeonTempletBase GetDungeonTempletBase()
		{
			return this.m_DungeonTempletBase;
		}

		// Token: 0x060046AB RID: 18091 RVA: 0x00156C63 File Offset: 0x00154E63
		public void SetDungeonInfo(NKMDungeonTempletBase dungeonTempletBase, DeckContents eventDeckContents = DeckContents.NORMAL)
		{
			this.m_DungeonTempletBase = dungeonTempletBase;
			this.m_StageTemplet = dungeonTempletBase.StageTemplet;
			this.m_deckContents = eventDeckContents;
			this.m_BGMName = "";
			this.m_UsedDeckType = NKM_DECK_TYPE.NDT_DAILY;
			this.m_eventDeckIndex = dungeonTempletBase.m_UseEventDeck;
		}

		// Token: 0x060046AC RID: 18092 RVA: 0x00156C9D File Offset: 0x00154E9D
		public void SetDungeonInfo(NKMStageTempletV2 stageTemplet, DeckContents eventDeckContents = DeckContents.NORMAL)
		{
			this.m_StageTemplet = stageTemplet;
			this.m_deckContents = eventDeckContents;
			this.m_BGMName = "";
			this.m_DungeonTempletBase = stageTemplet.DungeonTempletBase;
			this.m_UsedDeckType = NKM_DECK_TYPE.NDT_DAILY;
			this.m_eventDeckIndex = stageTemplet.GetEventDeckID();
		}

		// Token: 0x060046AD RID: 18093 RVA: 0x00156CD7 File Offset: 0x00154ED7
		public void SetReservedBGM(string bgmName)
		{
			this.m_BGMName = bgmName;
		}

		// Token: 0x060046AE RID: 18094 RVA: 0x00156CE0 File Offset: 0x00154EE0
		private void OnClickStartCommomProcess(bool bEventDeck, NKMStageTempletV2 stageTemplet)
		{
			if (stageTemplet == null)
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (!NKMEpisodeMgr.HasEnoughResource(stageTemplet, this.m_LastMultiplyRewardCount))
			{
				return;
			}
			if (this.m_LastMultiplyRewardCount > 1 || this.m_bOperationSkip)
			{
				if (!this.m_bOperationSkip)
				{
					NKMRewardMultiplyTemplet.RewardMultiplyItem costItem = NKMRewardMultiplyTemplet.GetCostItem(NKMRewardMultiplyTemplet.ScopeType.General);
					if (!myUserData.CheckPrice(costItem.MiscItemCount * (this.m_LastMultiplyRewardCount - 1), costItem.MiscItemId))
					{
						NKCShopManager.OpenItemLackPopup(costItem.MiscItemId, costItem.MiscItemCount * (this.m_LastMultiplyRewardCount - 1));
						return;
					}
				}
				else if (!myUserData.CheckPrice(NKMCommonConst.SkipCostMiscItemCount * this.m_LastMultiplyRewardCount, NKMCommonConst.SkipCostMiscItemId))
				{
					NKCShopManager.OpenItemLackPopup(NKMCommonConst.SkipCostMiscItemId, NKMCommonConst.SkipCostMiscItemCount * this.m_LastMultiplyRewardCount);
					return;
				}
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = NKCUtil.CheckCommonStartCond(myUserData);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCUtil.OnExpandInventoryPopup(nkm_ERROR_CODE);
				return;
			}
			if (!this.m_bOperationSkip || stageTemplet.DungeonTempletBase == null)
			{
				int fierceBossID = 0;
				if (this.m_deckContents == DeckContents.FIERCE_BATTLE_SUPPORT)
				{
					NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
					if (nkcfierceBattleSupportDataMgr != null)
					{
						fierceBossID = nkcfierceBattleSupportDataMgr.CurBossID;
					}
				}
				STAGE_TYPE stage_TYPE = stageTemplet.m_STAGE_TYPE;
				NKCUICutScenPlayer.CutScenCallBack cutScenCallBack;
				if (stage_TYPE != STAGE_TYPE.ST_DUNGEON && stage_TYPE == STAGE_TYPE.ST_PHASE)
				{
					NKMPhaseTemplet phaseTemplet = stageTemplet.PhaseTemplet;
					if (bEventDeck)
					{
						cutScenCallBack = delegate()
						{
							NKCPacketSender.Send_NKMPacket_PHASE_START_REQ(stageTemplet.Key, this.m_SelectedEventDeck);
						};
					}
					else
					{
						cutScenCallBack = delegate()
						{
							NKCPacketSender.Send_NKMPacket_PHASE_START_REQ(stageTemplet.Key, this.m_SelectedDeckIndex);
						};
					}
				}
				else if (bEventDeck)
				{
					cutScenCallBack = delegate()
					{
						NKCPacketSender.Send_NKMPacket_GAME_LOAD_REQ(this.m_SelectedEventDeck, stageTemplet.Key, 0, stageTemplet.DungeonTempletBase.m_DungeonID, 0, false, this.m_LastMultiplyRewardCount, fierceBossID);
					};
				}
				else
				{
					cutScenCallBack = delegate()
					{
						NKCPacketSender.Send_NKMPacket_GAME_LOAD_REQ(this.m_SelectedDeckIndex.m_iIndex, stageTemplet.Key, 0, stageTemplet.DungeonTempletBase.m_DungeonStrID, 0, false, this.m_LastMultiplyRewardCount, fierceBossID);
					};
				}
				bool flag = true;
				if (NKCScenManager.CurrentUserData() != null)
				{
					flag = NKCScenManager.CurrentUserData().m_UserOption.m_bPlayCutscene;
				}
				bool flag2 = false;
				bool isOnGoing = NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing();
				if (!myUserData.CheckStageCleared(stageTemplet) || (flag && !isOnGoing))
				{
					NKCCutScenTemplet stageBeforeCutscen = stageTemplet.GetStageBeforeCutscen();
					if (stageBeforeCutscen != null)
					{
						NKCScenManager.GetScenManager().Get_NKC_SCEN_CUTSCEN_DUNGEON().SetReservedOneCutscenType(stageBeforeCutscen.m_CutScenStrID, cutScenCallBack, stageTemplet.Key);
						NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_CUTSCENE_DUNGEON, true);
						flag2 = true;
					}
				}
				if (!flag2)
				{
					cutScenCallBack();
				}
				return;
			}
			if (!myUserData.CheckDungeonClear(stageTemplet.DungeonTempletBase.m_DungeonID))
			{
				NKCPopupOKCancel.OpenOKBox(NKCStringTable.GetString("SI_DP_NOTICE", false), NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_CLEAR_STAGE", false), null, "");
				return;
			}
			List<long> lstUnits = new List<long>();
			if (NKCUIPrepareEventDeck.IsInstanceOpen)
			{
				lstUnits = this.m_SelectedEventDeck.m_dicUnit.Values.ToList<long>();
			}
			else
			{
				myUserData.m_ArmyData.GetDeckList(this.m_SelectedDeckIndex.m_eDeckType, (int)this.m_SelectedDeckIndex.m_iIndex, ref lstUnits);
			}
			NKCPacketSender.Send_NKMPacket_DUNGEON_SKIP_REQ(stageTemplet.DungeonTempletBase.m_DungeonID, lstUnits, this.m_LastMultiplyRewardCount);
		}

		// Token: 0x060046AF RID: 18095 RVA: 0x00156FA8 File Offset: 0x001551A8
		private void OnClickStartShadowPalace(bool bEventDeck, NKMDungeonTempletBase dungeonTempletBase)
		{
			if (dungeonTempletBase == null)
			{
				return;
			}
			NKMUserData cNKMUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKM_ERROR_CODE nkm_ERROR_CODE = NKCUtil.CheckCommonStartCond(cNKMUserData);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCUtil.OnExpandInventoryPopup(nkm_ERROR_CODE);
				return;
			}
			NKCUICutScenPlayer.CutScenCallBack cutScenCallBack;
			if (bEventDeck)
			{
				cutScenCallBack = delegate()
				{
					NKCPacketSender.Send_NKMPacket_GAME_LOAD_REQ(this.m_SelectedEventDeck, 0, 0, dungeonTempletBase.m_DungeonID, cNKMUserData.m_ShadowPalace.currentPalaceId, false, this.m_LastMultiplyRewardCount, 0);
				};
			}
			else
			{
				cutScenCallBack = delegate()
				{
					NKCPacketSender.Send_NKMPacket_GAME_LOAD_REQ(this.m_SelectedDeckIndex.m_iIndex, 0, 0, dungeonTempletBase.m_DungeonStrID, cNKMUserData.m_ShadowPalace.currentPalaceId, false, 1, 0);
				};
			}
			bool bPlayCutscene = cNKMUserData.m_UserOption.m_bPlayCutscene;
			bool flag = false;
			bool isOnGoing = NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing();
			if (dungeonTempletBase.m_DungeonID > 0 && (!cNKMUserData.CheckDungeonClear(dungeonTempletBase.m_DungeonID) || (bPlayCutscene && !isOnGoing)))
			{
				NKCCutScenTemplet cutScenTemple = NKCCutScenManager.GetCutScenTemple(dungeonTempletBase.m_CutScenStrIDBefore);
				if (cutScenTemple != null)
				{
					NKCScenManager.GetScenManager().Get_NKC_SCEN_CUTSCEN_DUNGEON().SetReservedOneCutscenType(cutScenTemple.m_CutScenStrID, cutScenCallBack, dungeonTempletBase.m_DungeonStrID);
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_CUTSCENE_DUNGEON, true);
					flag = true;
				}
			}
			if (!flag)
			{
				cutScenCallBack();
			}
		}

		// Token: 0x060046B0 RID: 18096 RVA: 0x001570C0 File Offset: 0x001552C0
		private void OnClickStartFierceBattleSupport(bool bEventDeck, NKMDungeonTempletBase dungeonTempletBase)
		{
			if (dungeonTempletBase == null)
			{
				return;
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = NKCUtil.CheckCommonStartCond(NKCScenManager.GetScenManager().GetMyUserData());
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCUtil.OnExpandInventoryPopup(nkm_ERROR_CODE);
				return;
			}
			NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
			if (nkcfierceBattleSupportDataMgr == null)
			{
				return;
			}
			int curBossID = nkcfierceBattleSupportDataMgr.CurBossID;
			if (bEventDeck)
			{
				NKCPacketSender.Send_NKMPacket_GAME_LOAD_REQ(this.m_SelectedEventDeck, 0, 0, dungeonTempletBase.m_DungeonID, 0, false, 0, curBossID);
				return;
			}
			NKCPacketSender.Send_NKMPacket_GAME_LOAD_REQ(this.m_SelectedDeckIndex.m_iIndex, 0, 0, dungeonTempletBase.m_DungeonStrID, 0, false, 0, curBossID);
		}

		// Token: 0x060046B1 RID: 18097 RVA: 0x00157138 File Offset: 0x00155338
		public void OnClickGuildCoop(bool bEventDeck, NKMDungeonTempletBase dungeonTempletBase)
		{
			if (dungeonTempletBase == null)
			{
				return;
			}
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(NKCGuildCoopManager.CanStartArena(dungeonTempletBase), true, null, -2147483648))
			{
				return;
			}
			NKMUserData cNKMUserData = NKCScenManager.GetScenManager().GetMyUserData();
			GuildDungeonMemberInfo guildDungeonMemberInfo = NKCGuildCoopManager.GetGuildMemberInfo().Find((GuildDungeonMemberInfo x) => x.profile.userUid == cNKMUserData.m_UserUID);
			if (guildDungeonMemberInfo == null)
			{
				return;
			}
			if (NKCGuildCoopManager.m_ArenaPlayableCount <= 0 && NKCGuildCoopManager.m_ArenaTicketBuyCount >= NKMCommonConst.GuildDungeonConstTemplet.ArenaTicketBuyCount)
			{
				return;
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = NKCUtil.CheckCommonStartCond(cNKMUserData);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCUtil.OnExpandInventoryPopup(nkm_ERROR_CODE);
				return;
			}
			if (NKMCommonConst.GuildDungeonConstTemplet.ArenaTicketBuyCount - NKCGuildCoopManager.m_ArenaTicketBuyCount > 0 && NKCGuildCoopManager.m_ArenaPlayableCount <= 0 && guildDungeonMemberInfo.arenaList.Count >= NKMCommonConst.GuildDungeonConstTemplet.ArenaPlayCountBasic)
			{
				NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_CONSORTIUM_DUNGEON_PLAY_COUNT_BUY_TEXT, 101, NKMCommonConst.GuildDungeonConstTemplet.TicketCost, delegate()
				{
					NKCPacketSender.Send_NKMPacket_GUILD_DUNGEON_TICKET_BUY_REQ();
				}, null, false);
				return;
			}
			NKCUICutScenPlayer.CutScenCallBack cutScenCallBack;
			if (bEventDeck)
			{
				cutScenCallBack = delegate()
				{
					NKCPacketSender.Send_NKMPacket_GAME_LOAD_REQ(this.m_SelectedEventDeck, 0, 0, dungeonTempletBase.m_DungeonID, 0, false, 0, 0);
				};
			}
			else
			{
				cutScenCallBack = delegate()
				{
					NKCPacketSender.Send_NKMPacket_GAME_LOAD_REQ(this.m_SelectedDeckIndex.m_iIndex, 0, 0, dungeonTempletBase.m_DungeonStrID, 0, false, 1, 0);
				};
			}
			cutScenCallBack();
		}

		// Token: 0x060046B2 RID: 18098 RVA: 0x00157272 File Offset: 0x00155472
		public void StartByRepeatOperation()
		{
			if (this.m_eventDeckIndex == 0)
			{
				this.UI_DUNGEON_START_CLICK(NKCUIDeckViewer.Instance.GetSelectDeckIndex());
				return;
			}
			if (NKCUIPrepareEventDeck.Instance.CheckStartPossible())
			{
				NKCUIPrepareEventDeck.Instance.StartGame();
			}
		}

		// Token: 0x060046B3 RID: 18099 RVA: 0x001572A4 File Offset: 0x001554A4
		private bool CheckPlayCount()
		{
			if (this.m_StageTemplet == null)
			{
				return true;
			}
			if (this.m_StageTemplet.EnterLimit <= 0)
			{
				return true;
			}
			int statePlayCnt = NKCScenManager.CurrentUserData().GetStatePlayCnt(this.m_StageTemplet.Key, false, false, false);
			if (this.m_StageTemplet.EnterLimit - statePlayCnt <= 0)
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				int num = 0;
				if (nkmuserData != null)
				{
					num = nkmuserData.GetStageRestoreCnt(this.m_StageTemplet.Key);
				}
				if (!this.m_StageTemplet.Restorable)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_ENTER_LIMIT_OVER, null, "");
				}
				else if (num >= this.m_StageTemplet.RestoreLimit)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_WARFARE_GAEM_HUD_RESTORE_LIMIT_OVER_DESC, null, "");
				}
				else
				{
					NKCPopupResourceWithdraw.Instance.OpenForRestoreEnterLimit(this.m_StageTemplet, delegate
					{
						NKCPacketSender.Send_NKMPacket_RESET_STAGE_PLAY_COUNT_REQ(this.m_StageTemplet.Key);
					}, num);
				}
				return false;
			}
			return true;
		}

		// Token: 0x060046B4 RID: 18100 RVA: 0x00157380 File Offset: 0x00155580
		public void UI_DUNGEON_START_CLICK(NKMDeckIndex selectedDeckIndex)
		{
			this.m_SelectedDeckIndex = selectedDeckIndex;
			if (!NKCUtil.ProcessDeckErrorMsg(NKMMain.IsValidDeck(NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData, selectedDeckIndex)))
			{
				return;
			}
			if (this.m_StageTemplet == null)
			{
				return;
			}
			this.m_LastMultiplyRewardCount = NKCUIDeckViewer.Instance.GetCurrMultiplyRewardCount();
			this.m_bOperationSkip = NKCUIDeckViewer.Instance.GetOperationSkipState();
			if (!this.CheckPlayCount())
			{
				return;
			}
			this.OnClickStartCommomProcess(false, this.m_StageTemplet);
		}

		// Token: 0x060046B5 RID: 18101 RVA: 0x001573F0 File Offset: 0x001555F0
		public void OnEventDeckConfirm(NKMStageTempletV2 stageTemplet, NKMDungeonTempletBase dungeonTempletBase, NKMEventDeckData eventDeckData)
		{
			if (eventDeckData == null)
			{
				return;
			}
			if (stageTemplet == null && dungeonTempletBase == null)
			{
				return;
			}
			if (this.m_SelectedEventDeck == null)
			{
				this.m_SelectedEventDeck = new NKMEventDeckData();
			}
			this.m_SelectedEventDeck.DeepCopy(eventDeckData);
			this.m_LastMultiplyRewardCount = NKCUIPrepareEventDeck.Instance.GetCurrMultiplyRewardCount();
			this.m_bOperationSkip = NKCUIPrepareEventDeck.Instance.GetOperationSkipState();
			switch (this.m_deckContents)
			{
			case DeckContents.SHADOW_PALACE:
				this.OnClickStartShadowPalace(true, dungeonTempletBase);
				return;
			case DeckContents.FIERCE_BATTLE_SUPPORT:
				if (NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr() != null)
				{
					this.OnClickStartFierceBattleSupport(true, dungeonTempletBase);
					return;
				}
				return;
			case DeckContents.GUILD_COOP:
				this.OnClickGuildCoop(true, dungeonTempletBase);
				return;
			}
			this.OnClickStartCommomProcess(true, stageTemplet);
		}

		// Token: 0x060046B6 RID: 18102 RVA: 0x00157499 File Offset: 0x00155699
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
		}

		// Token: 0x060046B7 RID: 18103 RVA: 0x001574A1 File Offset: 0x001556A1
		public override void ScenLoadComplete()
		{
			base.ScenLoadComplete();
			if (this.m_eventDeckIndex == 0)
			{
				NKCUIDeckViewer.Instance.LoadComplete();
			}
		}

		// Token: 0x060046B8 RID: 18104 RVA: 0x001574BC File Offset: 0x001556BC
		public override void ScenStart()
		{
			base.ScenStart();
			if (this.m_LastDeckIndex.m_eDeckType == NKM_DECK_TYPE.NDT_NONE)
			{
				this.m_LastDeckIndex.m_eDeckType = NKM_DECK_TYPE.NDT_DAILY;
			}
			if (this.m_SelectedEventDeck == null)
			{
				this.m_SelectedEventDeck = new NKMEventDeckData();
			}
			if (this.m_DungeonTempletBase != null && NKCTutorialManager.IsTutorialDungeon(this.m_DungeonTempletBase.m_DungeonID))
			{
				NKCUICutScenPlayer.CutScenCallBack cutScenCallBack = null;
				switch (this.m_DungeonTempletBase.m_DungeonID)
				{
				case 1004:
					cutScenCallBack = delegate()
					{
						NKCPacketSender.Send_NKMPacket_GAME_LOAD_REQ(new NKMEventDeckData(), 11211, 0, this.m_DungeonTempletBase.m_DungeonID, 0, false, 1, 0);
					};
					break;
				case 1005:
					cutScenCallBack = delegate()
					{
						NKCPacketSender.Send_NKMPacket_GAME_LOAD_REQ(new NKMEventDeckData(), 11212, 0, this.m_DungeonTempletBase.m_DungeonID, 0, false, 1, 0);
					};
					break;
				case 1006:
					cutScenCallBack = delegate()
					{
						NKCPacketSender.Send_NKMPacket_GAME_LOAD_REQ(new NKMEventDeckData(), 11213, 0, this.m_DungeonTempletBase.m_DungeonID, 0, false, 1, 0);
					};
					break;
				case 1007:
					cutScenCallBack = delegate()
					{
						NKCPacketSender.Send_NKMPacket_GAME_LOAD_REQ(new NKMEventDeckData(), 11214, 0, this.m_DungeonTempletBase.m_DungeonID, 0, false, 1, 0);
					};
					break;
				}
				bool flag = true;
				if (NKCScenManager.CurrentUserData() != null)
				{
					flag = NKCScenManager.CurrentUserData().m_UserOption.m_bPlayCutscene;
				}
				NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
				if (cutScenCallBack != null && (!myUserData.CheckStageCleared(this.m_StageTemplet) || flag))
				{
					NKCCutScenTemplet stageBeforeCutscen = this.m_StageTemplet.GetStageBeforeCutscen();
					if (stageBeforeCutscen != null)
					{
						NKCScenManager.GetScenManager().Get_NKC_SCEN_CUTSCEN_DUNGEON().SetReservedOneCutscenType(stageBeforeCutscen.m_CutScenStrID, cutScenCallBack, this.m_StageTemplet.Key);
						NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_CUTSCENE_DUNGEON, true);
						return;
					}
				}
				if (cutScenCallBack != null)
				{
					cutScenCallBack();
					return;
				}
			}
			if (this.m_deckContents == DeckContents.PHASE && NKCPhaseManager.ShouldPlayNextPhase())
			{
				NKCPhaseManager.PlayNextPhase();
				return;
			}
			NKCCamera.EnableBloom(false);
			NKCCamera.GetCamera().orthographic = false;
			NKCCamera.GetTrackingPos().SetNowValue(0f, 0f, -1000f);
			if (this.m_objNUM_OPERATION_BG == null)
			{
				this.m_objNUM_OPERATION_BG = NKCUIManager.OpenUI("NUM_OPERATION_BG");
			}
			if (this.m_objNUM_OPERATION_BG != null)
			{
				NKCCamera.RescaleRectToCameraFrustrum(this.m_objNUM_OPERATION_BG.GetComponent<RectTransform>(), NKCCamera.GetCamera(), new Vector2(200f, 200f), -1000f, NKCCamera.FitMode.FitAuto, NKCCamera.ScaleMode.Scale);
			}
			if (this.m_eventDeckIndex == 0)
			{
				NKCUIDeckViewer.DeckViewerOption deckViewerOption = default(NKCUIDeckViewer.DeckViewerOption);
				deckViewerOption.MenuName = NKCUtilString.GET_STRING_ATTACK_READY;
				if (this.m_StageTemplet == null)
				{
					return;
				}
				NKMEpisodeTempletV2 episodeTemplet = this.m_StageTemplet.EpisodeTemplet;
				if (episodeTemplet == null)
				{
					return;
				}
				if (episodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_DAILY)
				{
					deckViewerOption.eDeckviewerMode = NKCUIDeckViewer.DeckViewerMode.PrepareDungeonBattle_Daily;
				}
				else if (episodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_COUNTERCASE)
				{
					deckViewerOption.eDeckviewerMode = NKCUIDeckViewer.DeckViewerMode.PrepareDungeonBattleWithoutCost;
				}
				else
				{
					deckViewerOption.eDeckviewerMode = NKCUIDeckViewer.DeckViewerMode.PrepareDungeonBattle;
				}
				deckViewerOption.upsideMenuShowResourceList = new List<int>
				{
					1,
					this.m_StageTemplet.m_StageReqItemID,
					101
				};
				deckViewerOption.dOnSideMenuButtonConfirm = new NKCUIDeckViewer.DeckViewerOption.OnDeckSideButtonConfirm(this.UI_DUNGEON_START_CLICK);
				if (this.m_LastDeckIndex.m_eDeckType == this.m_UsedDeckType)
				{
					if (NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetDeckData(this.m_LastDeckIndex) == null)
					{
						this.m_LastDeckIndex = new NKMDeckIndex(this.m_UsedDeckType, 0);
					}
					deckViewerOption.DeckIndex = this.m_LastDeckIndex;
				}
				else
				{
					deckViewerOption.DeckIndex = new NKMDeckIndex(this.m_UsedDeckType, 0);
				}
				deckViewerOption.dOnBackButton = new NKCUIDeckViewer.DeckViewerOption.OnBackButton(this.OnBackButton);
				deckViewerOption.SelectLeaderUnitOnOpen = true;
				deckViewerOption.bEnableDefaultBackground = true;
				deckViewerOption.bUpsideMenuHomeButton = false;
				if (this.m_StageTemplet != null)
				{
					if (this.m_StageTemplet.m_StageReqItemID == 0)
					{
						deckViewerOption.CostItemID = 2;
					}
					else
					{
						deckViewerOption.CostItemID = this.m_StageTemplet.m_StageReqItemID;
					}
					deckViewerOption.CostItemCount = this.m_StageTemplet.m_StageReqItemCount;
					if (this.m_StageTemplet.m_StageReqItemID == 2)
					{
						NKCCompanyBuff.SetDiscountOfEterniumInEnteringDungeon(NKCScenManager.CurrentUserData().m_companyBuffDataList, ref deckViewerOption.CostItemCount);
					}
					deckViewerOption.StageBattleStrID = this.m_StageTemplet.m_StageBattleStrID;
					bool flag2 = true;
					bool flag4;
					bool flag3 = NKCContentManager.CheckContentStatus(ContentsType.OPERATION_MULTIPLY, out flag4, 0, 0) == NKCContentManager.eContentStatus.Open;
					if (flag3)
					{
						int num = 1;
						if (this.m_DungeonTempletBase != null)
						{
							num = this.m_DungeonTempletBase.m_RewardMultiplyMax;
							flag2 = (this.m_DungeonTempletBase.m_RewardMultiplyMax > 1);
						}
						if (this.m_StageTemplet.m_bActiveBattleSkip)
						{
							NKMUserData myUserData2 = NKCScenManager.GetScenManager().GetMyUserData();
							if (myUserData2 != null)
							{
								num = (int)myUserData2.m_InventoryData.GetCountMiscItem(deckViewerOption.CostItemID) / deckViewerOption.CostItemCount;
							}
						}
						if (this.m_StageTemplet.EnterLimit > 0)
						{
							NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
							int num2 = 0;
							if (nkmuserData != null)
							{
								num2 = nkmuserData.GetStatePlayCnt(this.m_StageTemplet.Key, false, false, false);
							}
							if (this.m_StageTemplet.EnterLimit - num2 > 0)
							{
								num = Mathf.Min(num, this.m_StageTemplet.EnterLimit - num2);
							}
						}
						if (this.m_StageTemplet.m_STAGE_TYPE == STAGE_TYPE.ST_PHASE)
						{
							num = 1;
							flag2 = false;
						}
						deckViewerOption.OperationMultiplyMax = num;
					}
					deckViewerOption.bUsableOperationSkip = (flag3 && flag2 && this.m_StageTemplet.m_bActiveBattleSkip);
				}
				NKCUIDeckViewer.Instance.Open(deckViewerOption, true);
				return;
			}
			else
			{
				bool flag5 = this.m_DungeonTempletBase != null && NKCTutorialManager.IsTutorialDungeon(this.m_DungeonTempletBase.m_DungeonID);
				NKMDungeonEventDeckTemplet eventDeckTemplet = NKMDungeonManager.GetEventDeckTemplet(this.m_eventDeckIndex);
				if (eventDeckTemplet != null && NKMDungeonManager.IsEventDeckSelectRequired(eventDeckTemplet, NKMContentsVersionManager.HasTag("OPERATOR")) && !flag5)
				{
					NKCUIPrepareEventDeck.Instance.Open(this.m_StageTemplet, this.m_DungeonTempletBase, new NKCUIPrepareEventDeck.OnEventDeckConfirm(this.OnEventDeckConfirm), new NKCUIPrepareEventDeck.OnBackButtonEvent(this.OnBackButton), this.m_deckContents);
					return;
				}
				this.OnEventDeckConfirm(this.m_StageTemplet, this.m_DungeonTempletBase, new NKMEventDeckData());
				return;
			}
		}

		// Token: 0x060046B9 RID: 18105 RVA: 0x00157A04 File Offset: 0x00155C04
		public override void PlayScenMusic()
		{
			switch (this.m_deckContents)
			{
			case DeckContents.SHADOW_PALACE:
				if (string.IsNullOrEmpty(this.m_BGMName))
				{
					NKCSoundManager.PlayScenMusic(this.m_NKM_SCEN_ID, false);
					return;
				}
				NKCSoundManager.PlayMusic(this.m_BGMName, false, 1f, false, 0f, 0f);
				return;
			case DeckContents.PHASE:
				if (!NKCPhaseManager.IsPhaseOnGoing())
				{
					NKCSoundManager.PlayScenMusic(this.m_NKM_SCEN_ID, false);
					return;
				}
				return;
			}
			NKCSoundManager.PlayScenMusic(this.m_NKM_SCEN_ID, false);
		}

		// Token: 0x060046BA RID: 18106 RVA: 0x00157A8C File Offset: 0x00155C8C
		private void OnBackButton()
		{
			switch (this.m_deckContents)
			{
			case DeckContents.SHADOW_PALACE:
				this.BackToShadowBattleScene();
				return;
			case DeckContents.FIERCE_BATTLE_SUPPORT:
				this.BackToFierceBattleSupportScene();
				return;
			case DeckContents.GUILD_COOP:
				this.BackToGuildCoopScene();
				return;
			}
			this.BackToOperationScene();
		}

		// Token: 0x060046BB RID: 18107 RVA: 0x00157AD4 File Offset: 0x00155CD4
		private void BackToOperationScene()
		{
			if (this.m_StageTemplet != null && this.m_StageTemplet.EpisodeTemplet != null && this.m_StageTemplet.EpisodeCategory == EPISODE_CATEGORY.EC_COUNTERCASE && this.m_StageTemplet.EpisodeId == 50)
			{
				NKCScenManager.GetScenManager().Get_SCEN_EPISODE().SetCounterCaseNormalActID(this.m_StageTemplet.ActId);
			}
			if (!NKCScenManager.GetScenManager().Get_SCEN_OPERATION().PlayByFavorite)
			{
				NKCScenManager.GetScenManager().Get_SCEN_OPERATION().SetReservedStage(this.m_StageTemplet);
			}
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OPERATION, true);
		}

		// Token: 0x060046BC RID: 18108 RVA: 0x00157B60 File Offset: 0x00155D60
		private void BackToShadowBattleScene()
		{
			NKCScenManager.GetScenManager().Get_NKC_SCEN_SHADOW_BATTLE().SetShadowPalaceID(NKCScenManager.CurrentUserData().m_ShadowPalace.currentPalaceId);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_SHADOW_BATTLE, true);
		}

		// Token: 0x060046BD RID: 18109 RVA: 0x00157B8D File Offset: 0x00155D8D
		private void BackToFierceBattleSupportScene()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_FIERCE_BATTLE_SUPPORT, true);
		}

		// Token: 0x060046BE RID: 18110 RVA: 0x00157B9C File Offset: 0x00155D9C
		private void BackToGuildCoopScene()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GUILD_COOP, true);
		}

		// Token: 0x060046BF RID: 18111 RVA: 0x00157BAB File Offset: 0x00155DAB
		public override void ScenEnd()
		{
			base.ScenEnd();
			NKCUIPrepareEventDeck.CheckInstanceAndClose();
			if (NKCUIDeckViewer.CheckInstance())
			{
				this.m_LastDeckIndex = NKCUIDeckViewer.Instance.GetSelectDeckIndex();
			}
			NKCUIDeckViewer.CheckInstanceAndClose();
		}

		// Token: 0x060046C0 RID: 18112 RVA: 0x00157BD4 File Offset: 0x00155DD4
		public override void ScenUpdate()
		{
			base.ScenUpdate();
			if (!NKCCamera.IsTrackingCameraPos())
			{
				NKCCamera.TrackingPos(10f, NKMRandom.Range(-50f, 50f), NKMRandom.Range(-50f, 50f), NKMRandom.Range(-1000f, -900f));
			}
			this.m_BloomIntensity.Update(Time.deltaTime);
			if (!this.m_BloomIntensity.IsTracking())
			{
				this.m_BloomIntensity.SetTracking(NKMRandom.Range(1f, 2f), 4f, TRACKING_DATA_TYPE.TDT_SLOWER);
			}
			NKCCamera.SetBloomIntensity(this.m_BloomIntensity.GetNowValue());
		}

		// Token: 0x060046C1 RID: 18113 RVA: 0x00157C72 File Offset: 0x00155E72
		public override bool ScenMsgProc(NKCMessageData cNKCMessageData)
		{
			return false;
		}

		// Token: 0x0400379E RID: 14238
		private NKMStageTempletV2 m_StageTemplet;

		// Token: 0x0400379F RID: 14239
		private NKMDungeonTempletBase m_DungeonTempletBase;

		// Token: 0x040037A0 RID: 14240
		private int m_eventDeckIndex;

		// Token: 0x040037A1 RID: 14241
		private DeckContents m_deckContents;

		// Token: 0x040037A2 RID: 14242
		private string m_BGMName = "";

		// Token: 0x040037A3 RID: 14243
		private GameObject m_NUM_OPERATION;

		// Token: 0x040037A4 RID: 14244
		private GameObject m_objNUM_OPERATION_BG;

		// Token: 0x040037A5 RID: 14245
		private NKMTrackingFloat m_BloomIntensity = new NKMTrackingFloat();

		// Token: 0x040037A6 RID: 14246
		private NKMDeckIndex m_LastDeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_NONE, 0);

		// Token: 0x040037A7 RID: 14247
		private NKM_DECK_TYPE m_UsedDeckType = NKM_DECK_TYPE.NDT_DAILY;

		// Token: 0x040037A8 RID: 14248
		private NKMDeckIndex m_SelectedDeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_NONE, 0);

		// Token: 0x040037A9 RID: 14249
		private NKMEventDeckData m_SelectedEventDeck;

		// Token: 0x040037AA RID: 14250
		private int m_LastMultiplyRewardCount = 1;

		// Token: 0x040037AB RID: 14251
		private bool m_bOperationSkip;
	}
}
