using System;
using System.Collections.Generic;
using ClientPacket.User;
using Cs.Logging;
using NKM;
using NKM.Templet;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A14 RID: 2580
	public class NKCUIStageInfoSubStage : NKCUIStageInfoSubBase
	{
		// Token: 0x060070A3 RID: 28835 RVA: 0x00255410 File Offset: 0x00253610
		public override void InitUI(NKCUIStageInfoSubBase.OnButton onButton)
		{
			base.InitUI(onButton);
			this.m_tglStageInfo.OnValueChanged.RemoveAllListeners();
			this.m_tglStageInfo.OnValueChanged.AddListener(new UnityAction<bool>(this.OnStageInfo));
			this.m_tglIngameInfo.OnValueChanged.RemoveAllListeners();
			this.m_tglIngameInfo.OnValueChanged.AddListener(new UnityAction<bool>(this.OnIngameInfo));
			this.m_btnEnemyLevel.PointerClick.RemoveAllListeners();
			this.m_btnEnemyLevel.PointerClick.AddListener(new UnityAction(this.OnClickEnemyLevel));
			this.m_tglStoryReplay.OnValueChanged.RemoveAllListeners();
			this.m_tglStoryReplay.OnValueChanged.AddListener(new UnityAction<bool>(this.OnStoryReplay));
			this.m_btnReady.PointerClick.RemoveAllListeners();
			this.m_btnReady.PointerClick.AddListener(new UnityAction(base.OnOK));
			this.m_btnReady.m_bGetCallbackWhileLocked = true;
			this.m_btnReady.SetHotkey(HotkeyEventType.Confirm, null, false);
			this.m_tglFavorite.OnValueChanged.RemoveAllListeners();
			this.m_tglFavorite.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickFavorite));
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglSkip, new UnityAction<bool>(this.OnTglSkip));
			if (this.m_NKCUIOperationSkip != null)
			{
				this.m_NKCUIOperationSkip.Init(new NKCUIOperationSkip.OnCountUpdated(this.OnOperationSkipUpdated), new UnityAction(this.OnClickOperationSkipClose));
			}
			NKCUtil.SetGameobjectActive(this.m_NKCUIOperationSkip, false);
			this.m_SkipCount = 1;
			this.m_bOperationSkip = false;
		}

		// Token: 0x060070A4 RID: 28836 RVA: 0x002555A8 File Offset: 0x002537A8
		public void Close()
		{
			this.ClearTeamUPData();
		}

		// Token: 0x060070A5 RID: 28837 RVA: 0x002555B0 File Offset: 0x002537B0
		public override void SetData(NKMStageTempletV2 stageTemplet, bool bFirstOpen = true)
		{
			base.SetData(stageTemplet, bFirstOpen);
			this.SetStageInfo();
			if (bFirstOpen)
			{
				this.m_tglStageInfo.Select(true, false, false);
			}
			this.m_tglFavorite.Select(NKMEpisodeMgr.GetFavoriteStageList().ContainsValue(stageTemplet), true, false);
			NKCUtil.SetGameobjectActive(this.m_NKCUIOperationSkip, false);
			this.m_SkipCount = 1;
			this.m_bOperationSkip = false;
			if (this.m_tglSkip != null)
			{
				this.m_tglSkip.Select(false, true, false);
			}
			NKCUtil.SetGameobjectActive(this.m_tglSkip, stageTemplet.m_bActiveBattleSkip);
			this.UpdateStageRequiredItem();
		}

		// Token: 0x060070A6 RID: 28838 RVA: 0x00255644 File Offset: 0x00253844
		private void UpdateStageRequiredItem()
		{
			this.SetStageRequiredItem(this.m_objReadyResourceParent, this.m_imgReadyResource, this.m_lbReadyResource, this.m_StageTemplet);
			this.SetStageRequiredItem(this.m_objReadyResourceParent, this.m_imgReadyResourceLocked, this.m_lbReadyResourceLocked, this.m_StageTemplet);
		}

		// Token: 0x060070A7 RID: 28839 RVA: 0x00255684 File Offset: 0x00253884
		private void SetStageInfo()
		{
			STAGE_TYPE stage_TYPE = this.m_StageTemplet.m_STAGE_TYPE;
			if (stage_TYPE != STAGE_TYPE.ST_DUNGEON)
			{
				if (stage_TYPE != STAGE_TYPE.ST_PHASE)
				{
					Log.Warn(string.Format("던전 정보창에 등록되지 않은 타입 - StageID : {0}, StageType : {1}", this.m_StageTemplet.Key, this.m_StageTemplet.m_STAGE_TYPE), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Operation/NKCUIStageInfoSubStage.cs", 192);
				}
				else
				{
					NKMPhaseTemplet phaseTemplet = this.m_StageTemplet.PhaseTemplet;
					if (phaseTemplet != null)
					{
						NKCUIComMedal mission = this.m_Mission;
						if (mission != null)
						{
							mission.SetData(phaseTemplet, false);
						}
						NKCUtil.SetLabelText(this.m_lbEnemyLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, phaseTemplet.PhaseLevel));
						this.SetBattleConditionUI(phaseTemplet);
						NKCUtil.SetGameobjectActive(this.m_ObjStoryReplay, NKCUIStageInfoSubStage.HasCutScen(phaseTemplet));
						NKCUtil.SetLabelText(this.m_lbLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, phaseTemplet.PhaseLevel));
						return;
					}
				}
			}
			else
			{
				NKMDungeonTempletBase dungeonTempletBase = this.m_StageTemplet.DungeonTempletBase;
				if (dungeonTempletBase != null)
				{
					NKCUIComMedal mission2 = this.m_Mission;
					if (mission2 != null)
					{
						mission2.SetData(dungeonTempletBase, false);
					}
					NKCUtil.SetLabelText(this.m_lbEnemyLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, dungeonTempletBase.m_DungeonLevel));
					this.SetBattleConditionUI(dungeonTempletBase);
					NKCUtil.SetGameobjectActive(this.m_ObjStoryReplay, dungeonTempletBase.HasCutscen());
					NKCUtil.SetLabelText(this.m_lbLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, dungeonTempletBase.m_DungeonLevel));
					return;
				}
			}
		}

		// Token: 0x060070A8 RID: 28840 RVA: 0x002557E4 File Offset: 0x002539E4
		private void SetBattleConditionUI(NKMDungeonTempletBase dungeonTempletBase)
		{
			List<NKMBattleConditionTemplet> list = new List<NKMBattleConditionTemplet>();
			if (dungeonTempletBase != null && dungeonTempletBase.BattleCondition != null)
			{
				list.Add(dungeonTempletBase.BattleCondition);
			}
			this.UpdateBattleConditionUI(list);
		}

		// Token: 0x060070A9 RID: 28841 RVA: 0x00255818 File Offset: 0x00253A18
		private void SetBattleConditionUI(NKMPhaseTemplet phaseTemplet)
		{
			List<NKMBattleConditionTemplet> list = new List<NKMBattleConditionTemplet>();
			if (phaseTemplet != null)
			{
				using (IEnumerator<NKMPhaseOrderTemplet> enumerator = phaseTemplet.PhaseList.List.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						NKMPhaseOrderTemplet phase = enumerator.Current;
						if (phase.Dungeon != null && phase.Dungeon.BattleCondition != null && list.FindIndex((NKMBattleConditionTemplet e) => e == phase.Dungeon.BattleCondition) == -1)
						{
							list.Add(phase.Dungeon.BattleCondition);
						}
					}
				}
			}
			this.UpdateBattleConditionUI(list);
		}

		// Token: 0x060070AA RID: 28842 RVA: 0x002558CC File Offset: 0x00253ACC
		private void UpdateBattleConditionUI(List<NKMBattleConditionTemplet> listBattleConditionTemplet)
		{
			if (listBattleConditionTemplet == null || listBattleConditionTemplet.Count == 0)
			{
				NKCUtil.SetGameobjectActive(this.m_objBattleCond, false);
				NKCUtil.SetGameobjectActive(this.m_objTeamUP, false);
				return;
			}
			NKMBattleConditionTemplet nkmbattleConditionTemplet = listBattleConditionTemplet[0];
			if (nkmbattleConditionTemplet == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objBattleCond, false);
				NKCUtil.SetGameobjectActive(this.m_objTeamUP, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objBattleCond, true);
			if (nkmbattleConditionTemplet != null)
			{
				Sprite spriteBattleConditionICon = NKCUtil.GetSpriteBattleConditionICon(nkmbattleConditionTemplet);
				if (spriteBattleConditionICon != null)
				{
					NKCUtil.SetImageSprite(this.m_imgBattleCond, spriteBattleConditionICon, false);
				}
				NKCUtil.SetLabelText(this.m_lbBattleCondTitle, nkmbattleConditionTemplet.BattleCondName_Translated);
				NKCUtil.SetLabelText(this.m_lbBattleCondDesc, nkmbattleConditionTemplet.BattleCondDesc_Translated);
			}
			this.UpdateBattleConditionTeamUpUI(listBattleConditionTemplet);
		}

		// Token: 0x060070AB RID: 28843 RVA: 0x00255978 File Offset: 0x00253B78
		private void UpdateBattleConditionTeamUpUI(List<NKMBattleConditionTemplet> listBattleConditionTemplet)
		{
			this.ClearTeamUPData();
			List<int> list = new List<int>();
			if (listBattleConditionTemplet != null && listBattleConditionTemplet.Count > 0)
			{
				List<string> list2 = new List<string>();
				foreach (NKMBattleConditionTemplet nkmbattleConditionTemplet in listBattleConditionTemplet)
				{
					if (nkmbattleConditionTemplet != null)
					{
						foreach (string item in nkmbattleConditionTemplet.AffectTeamUpID)
						{
							if (!list2.Contains(item))
							{
								list2.Add(item);
							}
						}
						foreach (int item2 in nkmbattleConditionTemplet.hashAffectUnitID)
						{
							if (!list.Contains(item2))
							{
								list.Add(item2);
							}
						}
					}
				}
				List<NKMUnitTempletBase> list3 = new List<NKMUnitTempletBase>();
				if (list2.Count > 0)
				{
					foreach (string teamUp in list2)
					{
						foreach (NKMUnitTempletBase item3 in NKMUnitManager.GetListTeamUPUnitTempletBase(teamUp))
						{
							if (!list3.Contains(item3))
							{
								list3.Add(item3);
							}
						}
					}
				}
				if (list3.Count == 0 && list.Count > 0)
				{
					foreach (int unitID in list)
					{
						NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
						if (unitTempletBase != null && !list3.Contains(unitTempletBase))
						{
							list3.Add(unitTempletBase);
						}
					}
				}
				foreach (NKMUnitTempletBase nkmunitTempletBase in list3)
				{
					if (nkmunitTempletBase.PickupEnableByTag && nkmunitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL && nkmunitTempletBase.m_UnitID >= this.m_iMinDisplayUnitID && nkmunitTempletBase.m_UnitID <= this.m_iMaxDisplayUnitID && (nkmunitTempletBase.m_ShipGroupID == 0 || nkmunitTempletBase.m_ShipGroupID == nkmunitTempletBase.m_UnitID))
					{
						NKCUtil.SetGameobjectActive(this.m_objTeamUP, true);
						NKCUISlot newInstance = NKCUISlot.GetNewInstance(this.m_rtTeamUpParent.transform);
						if (null != newInstance)
						{
							newInstance.transform.localPosition = Vector3.zero;
							newInstance.transform.localScale = Vector3.one;
							NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeRewardTypeData(NKM_REWARD_TYPE.RT_UNIT, nkmunitTempletBase.m_UnitID, 1, 0);
							NKCUtil.SetGameobjectActive(newInstance.gameObject, true);
							newInstance.SetData(data, true, null);
							this.m_lstTeamUpUnits.Add(newInstance);
						}
					}
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objTeamUP, this.m_lstTeamUpUnits.Count > 0);
		}

		// Token: 0x060070AC RID: 28844 RVA: 0x00255CB8 File Offset: 0x00253EB8
		private void SetIngameInfo()
		{
		}

		// Token: 0x060070AD RID: 28845 RVA: 0x00255CBC File Offset: 0x00253EBC
		private void ClearTeamUPData()
		{
			for (int i = 0; i < this.m_lstTeamUpUnits.Count; i++)
			{
				UnityEngine.Object.Destroy(this.m_lstTeamUpUnits[i]);
				this.m_lstTeamUpUnits[i] = null;
			}
			this.m_lstTeamUpUnits.Clear();
		}

		// Token: 0x060070AE RID: 28846 RVA: 0x00255D08 File Offset: 0x00253F08
		private void SetStageRequiredItem(GameObject itemObject, Image itemIcon, Text itemCount, NKMStageTempletV2 stageTemplet)
		{
			if (stageTemplet == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(itemObject, stageTemplet.m_StageReqItemID != 0 && stageTemplet.m_StageReqItemCount > 0);
			int num = stageTemplet.m_StageReqItemCount * this.m_SkipCount;
			if (stageTemplet.m_StageReqItemID == 2)
			{
				if (stageTemplet.WarfareTemplet != null)
				{
					NKCCompanyBuff.SetDiscountOfEterniumInEnteringWarfare(NKCScenManager.CurrentUserData().m_companyBuffDataList, ref num);
				}
				else if (stageTemplet.DungeonTempletBase != null)
				{
					NKCCompanyBuff.SetDiscountOfEterniumInEnteringDungeon(NKCScenManager.CurrentUserData().m_companyBuffDataList, ref num);
				}
				else if (stageTemplet.PhaseTemplet != null)
				{
					NKCCompanyBuff.SetDiscountOfEterniumInEnteringDungeon(NKCScenManager.CurrentUserData().m_companyBuffDataList, ref num);
				}
			}
			NKCUtil.SetLabelText(itemCount, num.ToString());
			if (stageTemplet.m_StageReqItemID > 0)
			{
				if ((long)num > NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(stageTemplet.m_StageReqItemID))
				{
					NKCUtil.SetLabelTextColor(itemCount, Color.red);
				}
				else
				{
					NKCUtil.SetLabelTextColor(itemCount, Color.white);
				}
			}
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(stageTemplet.m_StageReqItemID);
			if (itemMiscTempletByID != null)
			{
				Sprite orLoadMiscItemSmallIcon = NKCResourceUtility.GetOrLoadMiscItemSmallIcon(itemMiscTempletByID);
				NKCUtil.SetImageSprite(itemIcon, orLoadMiscItemSmallIcon, false);
			}
		}

		// Token: 0x060070AF RID: 28847 RVA: 0x00255E09 File Offset: 0x00254009
		public void RefreshFavoriteState()
		{
			this.m_tglFavorite.Select(NKMEpisodeMgr.GetFavoriteStageList().ContainsValue(this.m_StageTemplet), true, false);
		}

		// Token: 0x060070B0 RID: 28848 RVA: 0x00255E29 File Offset: 0x00254029
		private void OnStageInfo(bool bValue)
		{
			if (bValue)
			{
				NKCUtil.SetGameobjectActive(this.m_objStageInfo, true);
				NKCUtil.SetGameobjectActive(this.m_objIngameInfo, false);
			}
		}

		// Token: 0x060070B1 RID: 28849 RVA: 0x00255E46 File Offset: 0x00254046
		private void OnIngameInfo(bool bValue)
		{
			if (bValue)
			{
				NKCUtil.SetGameobjectActive(this.m_objStageInfo, false);
				NKCUtil.SetGameobjectActive(this.m_objIngameInfo, true);
			}
		}

		// Token: 0x060070B2 RID: 28850 RVA: 0x00255E63 File Offset: 0x00254063
		private void OnClickEnemyLevel()
		{
			NKCPopupEnemyList.Instance.Open(this.m_StageTemplet);
		}

		// Token: 0x060070B3 RID: 28851 RVA: 0x00255E78 File Offset: 0x00254078
		private void OnStoryReplay(bool bValue)
		{
			NKMPacket_GAME_OPTION_PLAY_CUTSCENE_REQ nkmpacket_GAME_OPTION_PLAY_CUTSCENE_REQ = new NKMPacket_GAME_OPTION_PLAY_CUTSCENE_REQ();
			nkmpacket_GAME_OPTION_PLAY_CUTSCENE_REQ.isPlayCutscene = this.m_tglStoryReplay.m_bChecked;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GAME_OPTION_PLAY_CUTSCENE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060070B4 RID: 28852 RVA: 0x00255EB0 File Offset: 0x002540B0
		private void OnClickFavorite(bool bValue)
		{
			if (!bValue)
			{
				NKCPacketSender.Send_NKMPacket_FAVORITES_STAGE_DELETE_REQ(this.m_StageTemplet.Key);
				return;
			}
			if (NKMEpisodeMgr.GetFavoriteStageList().ContainsValue(this.m_StageTemplet))
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_FAVORITES_STAGE_ID_DUPLICATE), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				this.m_tglFavorite.Select(false, true, false);
				return;
			}
			if (NKMEpisodeMgr.GetFavoriteStageList().Count >= NKMCommonConst.MaxStageFavoriteCount)
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_FAVORITES_STAGE_COUNT_MAX), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				this.m_tglFavorite.Select(false, true, false);
				return;
			}
			NKCPacketSender.Send_NKMPacket_FAVORITES_STAGE_ADD_REQ(this.m_StageTemplet.Key);
		}

		// Token: 0x060070B5 RID: 28853 RVA: 0x00255F6C File Offset: 0x0025416C
		private static bool HasCutScen(NKMPhaseTemplet templet)
		{
			if (templet.m_CutScenStrIDAfter.Length > 0 || templet.m_CutScenStrIDBefore.Length > 0)
			{
				return true;
			}
			foreach (NKMPhaseOrderTemplet nkmphaseOrderTemplet in templet.PhaseList.List)
			{
				if (nkmphaseOrderTemplet.Dungeon != null && nkmphaseOrderTemplet.Dungeon.HasCutscen())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060070B6 RID: 28854 RVA: 0x00255FF4 File Offset: 0x002541F4
		private void OnTglSkip(bool bSet)
		{
			if (bSet)
			{
				if (!this.m_StageTemplet.m_bActiveBattleSkip)
				{
					this.m_tglSkip.Select(false, false, false);
					return;
				}
				if (!this.CheckCanSkip())
				{
					this.m_tglSkip.Select(false, false, false);
					return;
				}
				this.m_bOperationSkip = true;
				this.UpdateStageRequiredItem();
				this.SetSkipCountUIData();
			}
			NKCUtil.SetGameobjectActive(this.m_NKCUIOperationSkip, bSet);
			if (!bSet)
			{
				this.m_SkipCount = 1;
				this.m_bOperationSkip = false;
				this.UpdateStageRequiredItem();
				this.SetSkipCountUIData();
			}
		}

		// Token: 0x060070B7 RID: 28855 RVA: 0x00256078 File Offset: 0x00254278
		private bool CheckCanSkip()
		{
			if (!NKCScenManager.CurrentUserData().CheckStageCleared(this.m_StageTemplet))
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCStringTable.GetString("SI_DP_UNLOCK_CONDITION_REQUIRE_DESC_SURT_CLEAR_STAGE", false), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return false;
			}
			if (!NKMEpisodeMgr.GetMedalAllClear(NKCScenManager.CurrentUserData(), this.m_StageTemplet))
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_INVALID_SKIP_CONDITION), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return false;
			}
			if (this.m_StageTemplet.EnterLimit > 0)
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				int statePlayCnt = nkmuserData.GetStatePlayCnt(this.m_StageTemplet.Key, false, false, false);
				if (this.m_StageTemplet.EnterLimit - statePlayCnt <= 0)
				{
					int num = 0;
					if (nkmuserData != null)
					{
						num = nkmuserData.GetStageRestoreCnt(this.m_StageTemplet.Key);
					}
					if (!this.m_StageTemplet.Restorable)
					{
						NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_ENTER_LIMIT_OVER, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
					}
					else if (num >= this.m_StageTemplet.RestoreLimit)
					{
						NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_WARFARE_GAEM_HUD_RESTORE_LIMIT_OVER_DESC, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
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
			}
			return NKMEpisodeMgr.HasEnoughResource(this.m_StageTemplet, 1);
		}

		// Token: 0x060070B8 RID: 28856 RVA: 0x002561D1 File Offset: 0x002543D1
		private void SetSkipCountUIData()
		{
			if (this.m_NKCUIOperationSkip != null)
			{
				this.m_NKCUIOperationSkip.SetData(this.m_StageTemplet, this.m_SkipCount);
			}
		}

		// Token: 0x060070B9 RID: 28857 RVA: 0x002561F8 File Offset: 0x002543F8
		private void OnOperationSkipUpdated(int newCount)
		{
			this.m_SkipCount = newCount;
			this.UpdateStageRequiredItem();
		}

		// Token: 0x060070BA RID: 28858 RVA: 0x00256207 File Offset: 0x00254407
		private void OnClickOperationSkipClose()
		{
			this.m_tglSkip.Select(false, false, false);
		}

		// Token: 0x04005C62 RID: 23650
		[Header("스테이지")]
		[Header("상단")]
		public NKCUIComToggle m_tglFavorite;

		// Token: 0x04005C63 RID: 23651
		public TMP_Text m_lbLevel;

		// Token: 0x04005C64 RID: 23652
		[Space]
		[Header("중단")]
		[Space]
		[Header("스테이지정보")]
		public GameObject m_objStageInfo;

		// Token: 0x04005C65 RID: 23653
		public NKCUIComToggle m_tglStageInfo;

		// Token: 0x04005C66 RID: 23654
		public NKCUIComToggle m_tglIngameInfo;

		// Token: 0x04005C67 RID: 23655
		[Space]
		public GameObject m_objMedalParent;

		// Token: 0x04005C68 RID: 23656
		public NKCUIComMedal m_Mission;

		// Token: 0x04005C69 RID: 23657
		[Space]
		[Header("인게임 정보")]
		public GameObject m_objIngameInfo;

		// Token: 0x04005C6A RID: 23658
		public NKCUIComStateButton m_btnEnemyLevel;

		// Token: 0x04005C6B RID: 23659
		public Text m_lbEnemyLevel;

		// Token: 0x04005C6C RID: 23660
		[Header("전투환경")]
		public GameObject m_objBattleCond;

		// Token: 0x04005C6D RID: 23661
		public Image m_imgBattleCond;

		// Token: 0x04005C6E RID: 23662
		public Text m_lbBattleCondTitle;

		// Token: 0x04005C6F RID: 23663
		public Text m_lbBattleCondDesc;

		// Token: 0x04005C70 RID: 23664
		[Header("팀업")]
		public GameObject m_objTeamUP;

		// Token: 0x04005C71 RID: 23665
		public RectTransform m_rtTeamUpParent;

		// Token: 0x04005C72 RID: 23666
		[Space]
		[Header("하단")]
		public GameObject m_ObjStoryReplay;

		// Token: 0x04005C73 RID: 23667
		public NKCUIComToggle m_tglStoryReplay;

		// Token: 0x04005C74 RID: 23668
		public NKCUIComStateButton m_btnReady;

		// Token: 0x04005C75 RID: 23669
		public GameObject m_objReadyResourceParent;

		// Token: 0x04005C76 RID: 23670
		public Text m_lbReadyResource;

		// Token: 0x04005C77 RID: 23671
		public Image m_imgReadyResource;

		// Token: 0x04005C78 RID: 23672
		public Text m_lbReadyResourceLocked;

		// Token: 0x04005C79 RID: 23673
		public Image m_imgReadyResourceLocked;

		// Token: 0x04005C7A RID: 23674
		[Header("스킵")]
		public NKCUIComToggle m_tglSkip;

		// Token: 0x04005C7B RID: 23675
		public NKCUIOperationSkip m_NKCUIOperationSkip;

		// Token: 0x04005C7C RID: 23676
		[Header("버프 적용 유닛 노출 ID 범위")]
		public int m_iMinDisplayUnitID = 1001;

		// Token: 0x04005C7D RID: 23677
		public int m_iMaxDisplayUnitID = 10000;

		// Token: 0x04005C7E RID: 23678
		private List<NKCUIStageInfoSubStage.BattleCondition> m_listBattleConditionSlot = new List<NKCUIStageInfoSubStage.BattleCondition>();

		// Token: 0x04005C7F RID: 23679
		private List<NKCUISlot> m_lstTeamUpUnits = new List<NKCUISlot>();

		// Token: 0x02001751 RID: 5969
		private struct BattleCondition
		{
			// Token: 0x0600B2F1 RID: 45809 RVA: 0x003629B0 File Offset: 0x00360BB0
			public BattleCondition(Image _img, NKCUIComStateButton _btn)
			{
				this.Img = _img;
				this.Btn = _btn;
			}

			// Token: 0x0400A67F RID: 42623
			public Image Img;

			// Token: 0x0400A680 RID: 42624
			public NKCUIComStateButton Btn;
		}
	}
}
