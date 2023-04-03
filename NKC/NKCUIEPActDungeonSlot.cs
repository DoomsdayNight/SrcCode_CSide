using System;
using ClientPacket.Common;
using ClientPacket.Warfare;
using NKC.UI.Tooltip;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000984 RID: 2436
	public class NKCUIEPActDungeonSlot : MonoBehaviour, IDungeonSlot
	{
		// Token: 0x060063DB RID: 25563 RVA: 0x001F94A0 File Offset: 0x001F76A0
		public int GetStageIndex()
		{
			return this.m_StageIndex;
		}

		// Token: 0x060063DC RID: 25564 RVA: 0x001F94A8 File Offset: 0x001F76A8
		public bool CheckLock()
		{
			return this.m_goLock.activeSelf;
		}

		// Token: 0x060063DD RID: 25565 RVA: 0x001F94B5 File Offset: 0x001F76B5
		private void OnDestroy()
		{
			NKCAssetResourceManager.CloseInstance(this.m_instance);
		}

		// Token: 0x060063DE RID: 25566 RVA: 0x001F94C4 File Offset: 0x001F76C4
		public static NKCUIEPActDungeonSlot GetNewInstance(Transform parent, IDungeonSlot.OnSelectedItemSlot selectedSlot = null)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_OPERATION", "NKM_UI_OPERATION_EPISODE_DUNGEON_SLOT", false, null);
			NKCUIEPActDungeonSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIEPActDungeonSlot>();
			if (component == null)
			{
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				Debug.LogError("NKCUIEpisodeActSlot Prefab null!");
				return null;
			}
			component.m_instance = nkcassetInstanceData;
			component.SetOnSelectedItemSlot(selectedSlot);
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.transform.localPosition = new Vector3(component.transform.localPosition.x, component.transform.localPosition.y, 0f);
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x060063DF RID: 25567 RVA: 0x001F956F File Offset: 0x001F776F
		public void SetOnSelectedItemSlot(IDungeonSlot.OnSelectedItemSlot selectedSlot)
		{
			if (selectedSlot != null)
			{
				this.m_Button.PointerClick.RemoveAllListeners();
				this.m_OnSelectedSlot = selectedSlot;
				this.m_Button.PointerClick.AddListener(new UnityAction(this.OnSelectedItemSlotImpl));
			}
		}

		// Token: 0x060063E0 RID: 25568 RVA: 0x001F95A8 File Offset: 0x001F77A8
		private void OnSelectedItemSlotImpl()
		{
			if (this.m_NKM_UI_OPERATION_EPISODE_LIST_MISSION_BLACK != null && this.m_NKM_UI_OPERATION_EPISODE_LIST_MISSION_BLACK.activeSelf)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_ENTER_LIMIT_OVER, null, "");
				return;
			}
			if (this.m_OnSelectedSlot != null)
			{
				bool isPlaying = false;
				if (this.m_objPlaying != null)
				{
					isPlaying = this.m_objPlaying.activeSelf;
				}
				this.m_OnSelectedSlot(this.m_StageIndex, this.m_StageBattleStrID, isPlaying);
			}
		}

		// Token: 0x060063E1 RID: 25569 RVA: 0x001F9624 File Offset: 0x001F7824
		private void Update()
		{
			if (base.gameObject.activeSelf && this.m_CGDGSlot != null && this.m_CGDGSlot.alpha < 1f)
			{
				this.m_fElapsedTime += Time.deltaTime;
				if ((float)this.m_IndexToAnimateAlpha * 0.08f < this.m_fElapsedTime)
				{
					this.m_CGDGSlot.alpha = Mathf.Min(1f, this.m_fElapsedTime * 2f - (float)this.m_IndexToAnimateAlpha * 0.08f);
				}
			}
		}

		// Token: 0x060063E2 RID: 25570 RVA: 0x001F96B4 File Offset: 0x001F78B4
		public void SetIndexToAnimateAlpha(int index)
		{
			this.m_IndexToAnimateAlpha = index;
		}

		// Token: 0x060063E3 RID: 25571 RVA: 0x001F96BD File Offset: 0x001F78BD
		public void SetData(NKMStageTempletV2 stageTemplet)
		{
			this.SetData(stageTemplet.ActId, stageTemplet.m_StageIndex, stageTemplet.m_StageBattleStrID, NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), stageTemplet.m_UnlockInfo, false), stageTemplet.m_Difficulty);
		}

		// Token: 0x060063E4 RID: 25572 RVA: 0x001F96F0 File Offset: 0x001F78F0
		public void SetData(int actID, int stageIndex, string stageBattleStrID, bool bLock = false, EPISODE_DIFFICULTY difficulty = EPISODE_DIFFICULTY.NORMAL)
		{
			this.m_fElapsedTime = 0f;
			if (this.m_CGDGSlot != null)
			{
				this.m_CGDGSlot.alpha = 0f;
			}
			this.m_ActID = actID;
			this.m_StageIndex = stageIndex;
			this.m_IndexToAnimateAlpha = stageIndex;
			this.m_StageBattleStrID = stageBattleStrID;
			NKCUtil.SetGameobjectActive(this.m_goLock, bLock);
			this.m_Button.enabled = !bLock;
			NKMStageTempletV2 nkmstageTempletV = NKMEpisodeMgr.FindStageTempletByBattleStrID(stageBattleStrID);
			if (bLock)
			{
				NKCUtil.SetLabelText(this.m_lbLockDesc, NKCUtilString.GetUnlockConditionRequireDesc(nkmstageTempletV, false));
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbLockDesc, "");
			}
			NKCUtil.SetGameobjectActive(this.m_objDungeonEPNumber, false);
			NKCUtil.SetGameobjectActive(this.m_objUnlockItem, false);
			NKCUtil.SetGameobjectActive(this.m_objClearTime, false);
			if (nkmstageTempletV != null)
			{
				bool bValue = true;
				bool flag = false;
				NKMEpisodeTempletV2 episodeTemplet = nkmstageTempletV.EpisodeTemplet;
				if (episodeTemplet != null)
				{
					int count = episodeTemplet.m_DicStage[this.m_ActID].Count;
					bool flag2 = false;
					if (nkmstageTempletV.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_PRACTICE)
					{
						bValue = false;
					}
					NKCUtil.SetGameobjectActive(this.m_lbRecommendFightPower, nkmstageTempletV.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_NORMAL || nkmstageTempletV.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_TUTORIAL || nkmstageTempletV.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_KILLCOUNT);
					NKCUtil.SetGameobjectActive(this.m_objTraining, nkmstageTempletV.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_PRACTICE);
					NKCUtil.SetGameobjectActive(this.m_imgBoss, nkmstageTempletV.m_STAGE_SUB_TYPE != STAGE_SUB_TYPE.SST_PRACTICE);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_lIST_SLOT_SHADOW_COVER_TITLE, nkmstageTempletV.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_NORMAL || nkmstageTempletV.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_TUTORIAL);
					if (nkmstageTempletV.m_STAGE_TYPE == STAGE_TYPE.ST_WARFARE)
					{
						NKCUtil.SetGameobjectActive(this.m_goMissionList, true);
						NKCUtil.SetGameobjectActive(this.m_goClearOff, false);
						NKCUtil.SetGameobjectActive(this.m_goClearOn, false);
						NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_StageBattleStrID);
						if (nkmwarfareTemplet != null)
						{
							if (episodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_MAINSTREAM)
							{
								NKCUtil.SetGameobjectActive(this.m_objDungeonEPNumber, true);
								NKCUtil.SetLabelText(this.m_lbDungeonEPNumber, episodeTemplet.GetEpisodeTitle());
								NKCUtil.SetLabelText(this.m_lbDungeonNumber, this.m_ActID.ToString() + "-" + nkmstageTempletV.m_StageUINum.ToString());
							}
							else if (episodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_DAILY)
							{
								NKCUtil.SetLabelText(this.m_lbDungeonNumber, "<size=40>" + NKCUtilString.GetDailyDungeonLVDesc(nkmstageTempletV.m_StageUINum) + "</size>");
							}
							else
							{
								NKCUtil.SetLabelText(this.m_lbDungeonNumber, this.m_ActID.ToString() + "-" + nkmstageTempletV.m_StageUINum.ToString());
							}
							NKCUtil.SetLabelText(this.m_lbDungeonName, nkmwarfareTemplet.GetWarfareName());
							NKCUtil.SetLabelText(this.m_lbRecommendFightPower, string.Format(NKCUtilString.GET_STRING_ACT_DUNGEON_SLOT_FIGHT_POWER_DESC, nkmwarfareTemplet.m_WarfareLevel.ToString()));
							if (this.m_imgBoss != null)
							{
								Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_INVEN_ICON_UNIT", "AB_INVEN_ICON_" + nkmwarfareTemplet.m_WarfareIcon, false);
								if (orLoadAssetResource != null)
								{
									NKCUtil.SetImageSprite(this.m_imgBoss, orLoadAssetResource, false);
								}
								else
								{
									NKCAssetResourceData assetResourceUnitInvenIconEmpty = NKCResourceUtility.GetAssetResourceUnitInvenIconEmpty();
									if (assetResourceUnitInvenIconEmpty != null)
									{
										NKCUtil.SetImageSprite(this.m_imgBoss, assetResourceUnitInvenIconEmpty.GetAsset<Sprite>(), false);
									}
								}
							}
							NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
							if (myUserData != null)
							{
								NKMWarfareClearData warfareClearData = myUserData.GetWarfareClearData(nkmwarfareTemplet.m_WarfareID);
								bool bValue2 = true;
								bool flag3 = nkmwarfareTemplet.m_WFMissionType_1 > WARFARE_GAME_MISSION_TYPE.WFMT_NONE;
								bool flag4 = nkmwarfareTemplet.m_WFMissionType_2 > WARFARE_GAME_MISSION_TYPE.WFMT_NONE;
								if (warfareClearData != null)
								{
									if (!this.m_goMission1.activeSelf)
									{
										this.m_goMission1.SetActive(true);
									}
									if (this.m_goMission2.activeSelf == !warfareClearData.m_mission_result_1)
									{
										this.m_goMission2.SetActive(warfareClearData.m_mission_result_1);
									}
									if (this.m_goMission3.activeSelf == !warfareClearData.m_mission_result_2)
									{
										this.m_goMission3.SetActive(warfareClearData.m_mission_result_2);
									}
									if (count == this.m_StageIndex)
									{
										flag2 = true;
									}
									NKCUtil.SetGameobjectActive(this.m_goMission1_BG, false);
									NKCUtil.SetGameobjectActive(this.m_goMission2_BG, flag3 && !warfareClearData.m_mission_result_1);
									NKCUtil.SetGameobjectActive(this.m_goMission3_BG, flag4 && !warfareClearData.m_mission_result_2);
								}
								else
								{
									NKCUtil.SetGameobjectActive(this.m_goMission1_BG, bValue2);
									NKCUtil.SetGameobjectActive(this.m_goMission2_BG, flag3);
									NKCUtil.SetGameobjectActive(this.m_goMission3_BG, flag4);
									if (this.m_goMission1.activeSelf)
									{
										this.m_goMission1.SetActive(false);
									}
									if (this.m_goMission2.activeSelf)
									{
										this.m_goMission2.SetActive(false);
									}
									if (this.m_goMission3.activeSelf)
									{
										this.m_goMission3.SetActive(false);
									}
								}
							}
						}
					}
					else if (nkmstageTempletV.m_STAGE_TYPE == STAGE_TYPE.ST_DUNGEON)
					{
						NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(this.m_StageBattleStrID);
						if (dungeonTempletBase != null)
						{
							if (episodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_MAINSTREAM)
							{
								NKCUtil.SetGameobjectActive(this.m_objDungeonEPNumber, true);
								NKCUtil.SetLabelText(this.m_lbDungeonEPNumber, episodeTemplet.GetEpisodeTitle());
							}
							if (nkmstageTempletV.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_PRACTICE)
							{
								NKCUtil.SetLabelText(this.m_lbDungeonNumber, string.Format(NKCUtilString.GET_STRING_EP_TRAINING_NUMBER, nkmstageTempletV.m_StageUINum));
							}
							else if (dungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_CUTSCENE)
							{
								NKCUtil.SetLabelText(this.m_lbDungeonNumber, string.Format(NKCUtilString.GET_STRING_EP_CUTSCEN_NUMBER, nkmstageTempletV.m_StageUINum));
							}
							else if (episodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_MAINSTREAM)
							{
								NKCUtil.SetLabelText(this.m_lbDungeonNumber, this.m_ActID.ToString() + "-" + nkmstageTempletV.m_StageUINum.ToString());
							}
							else if (episodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_DAILY)
							{
								NKCUtil.SetLabelText(this.m_lbDungeonNumber, "<size=40>" + NKCUtilString.GetDailyDungeonLVDesc(nkmstageTempletV.m_StageUINum) + "</size>");
							}
							else
							{
								NKCUtil.SetLabelText(this.m_lbDungeonNumber, this.m_ActID.ToString() + "-" + nkmstageTempletV.m_StageUINum.ToString());
							}
							NKCUtil.SetLabelText(this.m_lbDungeonName, dungeonTempletBase.GetDungeonName());
							if (dungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_CUTSCENE)
							{
								NKCUtil.SetLabelText(this.m_lbRecommendFightPower, "");
								bValue = false;
								flag = true;
							}
							else
							{
								NKCUtil.SetLabelText(this.m_lbRecommendFightPower, string.Format(NKCUtilString.GET_STRING_ACT_DUNGEON_SLOT_FIGHT_POWER_DESC, dungeonTempletBase.m_DungeonLevel.ToString()));
							}
							if (this.m_imgBoss != null)
							{
								Sprite orLoadAssetResource2 = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_INVEN_ICON_UNIT", "AB_INVEN_ICON_" + dungeonTempletBase.m_DungeonIcon, false);
								if (orLoadAssetResource2 != null)
								{
									NKCUtil.SetImageSprite(this.m_imgBoss, orLoadAssetResource2, false);
								}
								else
								{
									NKCAssetResourceData assetResourceUnitInvenIconEmpty2 = NKCResourceUtility.GetAssetResourceUnitInvenIconEmpty();
									if (assetResourceUnitInvenIconEmpty2 != null)
									{
										NKCUtil.SetImageSprite(this.m_imgBoss, assetResourceUnitInvenIconEmpty2.GetAsset<Sprite>(), false);
									}
								}
							}
							NKMUserData myUserData2 = NKCScenManager.GetScenManager().GetMyUserData();
							if (myUserData2 != null)
							{
								NKMDungeonClearData dungeonClearData = myUserData2.GetDungeonClearData(dungeonTempletBase.m_DungeonID);
								if (episodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_DAILY || dungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_CUTSCENE || nkmstageTempletV.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_PRACTICE || nkmstageTempletV.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_TUTORIAL)
								{
									NKCUtil.SetGameobjectActive(this.m_goMissionList, false);
									if (dungeonClearData != null)
									{
										if (count == this.m_StageIndex)
										{
											flag2 = true;
										}
										NKCUtil.SetGameobjectActive(this.m_goClearOff, false);
										NKCUtil.SetGameobjectActive(this.m_goClearOn, true);
									}
									else
									{
										NKCUtil.SetGameobjectActive(this.m_goClearOff, true);
										NKCUtil.SetGameobjectActive(this.m_goClearOn, false);
									}
								}
								else
								{
									NKCUtil.SetGameobjectActive(this.m_goMissionList, true);
									NKCUtil.SetGameobjectActive(this.m_goClearOff, false);
									NKCUtil.SetGameobjectActive(this.m_goClearOn, dungeonClearData != null && episodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_CHALLENGE);
									bool bValue3 = true;
									bool flag5 = dungeonTempletBase.m_DGMissionType_1 > DUNGEON_GAME_MISSION_TYPE.DGMT_NONE;
									bool flag6 = dungeonTempletBase.m_DGMissionType_2 > DUNGEON_GAME_MISSION_TYPE.DGMT_NONE;
									if (dungeonClearData != null)
									{
										if (!this.m_goMission1.activeSelf)
										{
											this.m_goMission1.SetActive(true);
										}
										if (this.m_goMission2.activeSelf == !dungeonClearData.missionResult1)
										{
											this.m_goMission2.SetActive(dungeonClearData.missionResult1);
										}
										if (this.m_goMission3.activeSelf == !dungeonClearData.missionResult2)
										{
											this.m_goMission3.SetActive(dungeonClearData.missionResult2);
										}
										if (count == this.m_StageIndex)
										{
											flag2 = true;
										}
										NKCUtil.SetGameobjectActive(this.m_goMission1_BG, false);
										NKCUtil.SetGameobjectActive(this.m_goMission2_BG, flag5 && !dungeonClearData.missionResult1);
										NKCUtil.SetGameobjectActive(this.m_goMission3_BG, flag6 && !dungeonClearData.missionResult2);
									}
									else
									{
										if (this.m_goMission1.activeSelf)
										{
											this.m_goMission1.SetActive(false);
										}
										if (this.m_goMission2.activeSelf)
										{
											this.m_goMission2.SetActive(false);
										}
										if (this.m_goMission3.activeSelf)
										{
											this.m_goMission3.SetActive(false);
										}
										NKCUtil.SetGameobjectActive(this.m_goMission1_BG, bValue3);
										NKCUtil.SetGameobjectActive(this.m_goMission2_BG, flag5);
										NKCUtil.SetGameobjectActive(this.m_goMission3_BG, flag6);
									}
								}
							}
						}
					}
					else if (nkmstageTempletV.m_STAGE_TYPE == STAGE_TYPE.ST_PHASE)
					{
						NKMPhaseTemplet nkmphaseTemplet = NKMPhaseTemplet.Find(this.m_StageBattleStrID);
						if (nkmphaseTemplet != null)
						{
							if (episodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_MAINSTREAM)
							{
								NKCUtil.SetGameobjectActive(this.m_objDungeonEPNumber, true);
								NKCUtil.SetLabelText(this.m_lbDungeonEPNumber, episodeTemplet.GetEpisodeTitle());
							}
							if (nkmstageTempletV.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_PRACTICE)
							{
								NKCUtil.SetLabelText(this.m_lbDungeonNumber, string.Format(NKCUtilString.GET_STRING_EP_TRAINING_NUMBER, nkmstageTempletV.m_StageUINum));
							}
							else if (episodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_MAINSTREAM)
							{
								NKCUtil.SetLabelText(this.m_lbDungeonNumber, this.m_ActID.ToString() + "-" + nkmstageTempletV.m_StageUINum.ToString());
							}
							else if (episodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_DAILY)
							{
								NKCUtil.SetLabelText(this.m_lbDungeonNumber, "<size=40>" + NKCUtilString.GetDailyDungeonLVDesc(nkmstageTempletV.m_StageUINum) + "</size>");
							}
							else
							{
								NKCUtil.SetLabelText(this.m_lbDungeonNumber, this.m_ActID.ToString() + "-" + nkmstageTempletV.m_StageUINum.ToString());
							}
							NKCUtil.SetLabelText(this.m_lbDungeonName, nkmphaseTemplet.GetName());
							NKCUtil.SetLabelText(this.m_lbRecommendFightPower, string.Format(NKCUtilString.GET_STRING_ACT_DUNGEON_SLOT_FIGHT_POWER_DESC, nkmphaseTemplet.PhaseLevel.ToString()));
							if (this.m_imgBoss != null)
							{
								Sprite orLoadAssetResource3 = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_INVEN_ICON_UNIT", "AB_INVEN_ICON_" + nkmphaseTemplet.Icon, false);
								if (orLoadAssetResource3 != null)
								{
									NKCUtil.SetImageSprite(this.m_imgBoss, orLoadAssetResource3, false);
								}
								else
								{
									NKCAssetResourceData assetResourceUnitInvenIconEmpty3 = NKCResourceUtility.GetAssetResourceUnitInvenIconEmpty();
									if (assetResourceUnitInvenIconEmpty3 != null)
									{
										NKCUtil.SetImageSprite(this.m_imgBoss, assetResourceUnitInvenIconEmpty3.GetAsset<Sprite>(), false);
									}
								}
							}
							if (NKCScenManager.GetScenManager().GetMyUserData() != null)
							{
								NKMPhaseClearData phaseClearData = NKCPhaseManager.GetPhaseClearData(nkmphaseTemplet);
								if (episodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_DAILY || nkmstageTempletV.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_PRACTICE || nkmstageTempletV.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_TUTORIAL)
								{
									NKCUtil.SetGameobjectActive(this.m_goMissionList, false);
									if (phaseClearData != null)
									{
										if (count == this.m_StageIndex)
										{
											flag2 = true;
										}
										NKCUtil.SetGameobjectActive(this.m_goClearOff, false);
										NKCUtil.SetGameobjectActive(this.m_goClearOn, true);
									}
									else
									{
										NKCUtil.SetGameobjectActive(this.m_goClearOff, true);
										NKCUtil.SetGameobjectActive(this.m_goClearOn, false);
									}
								}
								else
								{
									NKCUtil.SetGameobjectActive(this.m_goMissionList, true);
									NKCUtil.SetGameobjectActive(this.m_goClearOff, false);
									NKCUtil.SetGameobjectActive(this.m_goClearOn, phaseClearData != null && episodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_CHALLENGE);
									bool bValue4 = true;
									bool flag7 = nkmphaseTemplet.m_DGMissionType_1 > DUNGEON_GAME_MISSION_TYPE.DGMT_NONE;
									bool flag8 = nkmphaseTemplet.m_DGMissionType_2 > DUNGEON_GAME_MISSION_TYPE.DGMT_NONE;
									if (phaseClearData != null)
									{
										if (!this.m_goMission1.activeSelf)
										{
											this.m_goMission1.SetActive(true);
										}
										if (this.m_goMission2.activeSelf == !phaseClearData.missionResult1)
										{
											this.m_goMission2.SetActive(phaseClearData.missionResult1);
										}
										if (this.m_goMission3.activeSelf == !phaseClearData.missionResult2)
										{
											this.m_goMission3.SetActive(phaseClearData.missionResult2);
										}
										if (count == this.m_StageIndex)
										{
											flag2 = true;
										}
										NKCUtil.SetGameobjectActive(this.m_goMission1_BG, false);
										NKCUtil.SetGameobjectActive(this.m_goMission2_BG, flag7 && !phaseClearData.missionResult1);
										NKCUtil.SetGameobjectActive(this.m_goMission3_BG, flag8 && !phaseClearData.missionResult2);
									}
									else
									{
										if (this.m_goMission1.activeSelf)
										{
											this.m_goMission1.SetActive(false);
										}
										if (this.m_goMission2.activeSelf)
										{
											this.m_goMission2.SetActive(false);
										}
										if (this.m_goMission3.activeSelf)
										{
											this.m_goMission3.SetActive(false);
										}
										NKCUtil.SetGameobjectActive(this.m_goMission1_BG, bValue4);
										NKCUtil.SetGameobjectActive(this.m_goMission2_BG, flag7);
										NKCUtil.SetGameobjectActive(this.m_goMission3_BG, flag8);
									}
								}
							}
						}
					}
					this.UpdateSubUI(nkmstageTempletV);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_BONUS_REWARD_MAIN_REWARD, nkmstageTempletV.MainRewardData != null && nkmstageTempletV.MainRewardData.rewardType > NKM_REWARD_TYPE.RT_NONE);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_lIST_SLOT_LEVEL, bValue);
					NKCUtil.SetGameobjectActive(this.m_objCutscenBG, flag);
					NKCUtil.SetGameobjectActive(this.m_objCutscenDungeonBG, !flag2 && (flag || nkmstageTempletV.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_TUTORIAL));
					NKCUtil.SetGameobjectActive(this.m_objCutscenDungeonBGClear, flag2 && (flag || nkmstageTempletV.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_TUTORIAL));
					NKCUtil.SetGameobjectActive(this.m_objWarfareBG, !flag);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_lIST_SLOT_BG_SHADOW, !flag2 && nkmstageTempletV.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_NORMAL && !flag);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_lIST_SLOT_BG_SHADOW_CLEAR, flag2 && nkmstageTempletV.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_NORMAL && !flag);
					NKCUtil.SetGameobjectActive(this.m_objTrainingBG, !flag2 && nkmstageTempletV.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_PRACTICE);
					NKCUtil.SetGameobjectActive(this.m_objTrainingClearBG, flag2 && nkmstageTempletV.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_PRACTICE);
					Color color = this.m_colNormal;
					if (difficulty == EPISODE_DIFFICULTY.HARD)
					{
						color = this.m_colHard;
					}
					NKCUtil.SetImageColor(this.m_NKM_UI_OPERATION_EPISODE_lIST_SLOT_BG_SHADOW, color);
					if (nkmstageTempletV.NeedToUnlock && !NKMEpisodeMgr.IsUnlockedStage(nkmstageTempletV))
					{
						NKCUtil.SetGameobjectActive(this.m_goLock, true);
						if (nkmstageTempletV.EpisodeCategory == EPISODE_CATEGORY.EC_SIDESTORY)
						{
							NKCUtil.SetLabelText(this.m_lbLockDesc, NKCUtilString.GetSidestoryUnlockRequireDesc(nkmstageTempletV));
						}
						else
						{
							NKCUtil.SetLabelText(this.m_lbLockDesc, NKCUtilString.GetUnlockConditionRequireDesc(nkmstageTempletV, false));
						}
						NKCUtil.SetGameobjectActive(this.m_objUnlockItem, true);
						this.m_slotUnlockItem.SetData(NKCUISlot.SlotData.MakeMiscItemData(nkmstageTempletV.UnlockReqItem.ItemId, (long)nkmstageTempletV.UnlockReqItem.Count32, 0), true, null);
					}
					NKCUtil.SetGameobjectActive(this.m_objClearTime, nkmstageTempletV.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_TIMEATTACK);
					if (nkmstageTempletV.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_TIMEATTACK)
					{
						string msg = "-:--:--";
						NKCUtil.SetGameobjectActive(this.m_objClearTime, true);
						NKCUtil.SetLabelText(this.m_lbClearTime, msg);
						if (NKCScenManager.CurrentUserData().IsHaveStatePlayData(nkmstageTempletV.Key) && NKCScenManager.CurrentUserData().GetStageBestClearSec(nkmstageTempletV.Key) > 0)
						{
							msg = NKCUtilString.GetTimeStringFromSeconds(NKCScenManager.CurrentUserData().GetStageBestClearSec(nkmstageTempletV.Key));
							NKCUtil.SetLabelTextColor(this.m_lbClearTime, NKCUtil.GetColor("#ED173A"));
						}
						else
						{
							NKCUtil.SetLabelTextColor(this.m_lbClearTime, NKCUtil.GetColor("#FFFFFF"));
						}
						NKCUtil.SetLabelText(this.m_lbClearTime, msg);
					}
				}
				this.UpdateINGWarfareDirectGoUI(nkmstageTempletV);
				if (this.m_goLock != null && !this.m_goLock.activeSelf)
				{
					NKCUtil.SetGameobjectActive(this.m_goLock, !nkmstageTempletV.IsOpenedDayOfWeek());
					if (this.m_goLock.activeSelf)
					{
						NKCUtil.SetLabelText(this.m_lbLockDesc, NKCUtilString.GET_STRING_DAILY_CHECK_DAY);
						this.m_Button.enabled = false;
					}
				}
			}
			base.gameObject.SetActive(true);
		}

		// Token: 0x060063E5 RID: 25573 RVA: 0x001FA608 File Offset: 0x001F8808
		private void UpdateSubUI(NKMStageTempletV2 stageTemplet)
		{
			if (stageTemplet == null)
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			if (stageTemplet.EnterLimit <= 0)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_ENTER_LIMIT, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_LIST_MISSION_BLACK, false);
				NKCUtil.SetLabelTextColor(this.EnterLimit_COUNT_TEXT, Color.white);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_ENTER_LIMIT, true);
				if (stageTemplet.EnterLimit - nkmuserData.GetStatePlayCnt(stageTemplet.Key, false, false, false) <= 0)
				{
					NKCUtil.SetLabelTextColor(this.EnterLimit_COUNT_TEXT, Color.red);
				}
				else
				{
					NKCUtil.SetLabelTextColor(this.EnterLimit_COUNT_TEXT, Color.white);
				}
				NKCUtil.SetLabelText(this.EnterLimit_COUNT_TEXT, string.Format("({0}/{1})", stageTemplet.EnterLimit - nkmuserData.GetStatePlayCnt(stageTemplet.Key, false, false, false), stageTemplet.EnterLimit));
				if (nkmuserData.IsHaveStatePlayData(stageTemplet.Key) && nkmuserData.GetStatePlayCnt(stageTemplet.Key, false, false, false) >= stageTemplet.EnterLimit && nkmuserData.GetStageRestoreCnt(stageTemplet.Key) >= stageTemplet.RestoreLimit)
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_LIST_MISSION_BLACK, true);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_LIST_MISSION_BLACK, false);
				}
			}
			if (stageTemplet.m_BuffType.Equals(RewardTuningType.None))
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_BONUS_REWARD_BONUS_TYPE, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_BONUS_REWARD_BONUS_TYPE, true);
				NKCUtil.SetImageSprite(this.m_Img_NKM_UI_OPERATION_EPISODE_BONUS_REWARD_BONUS_TYPE, NKCUtil.GetBounsTypeIcon(stageTemplet.m_BuffType, true), false);
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_BONUS_REWARD_MAIN_REWARD_SLOT, false);
			bool flag = true;
			if (stageTemplet.MainRewardData != null)
			{
				flag = NKMRewardTemplet.IsOpenedReward(stageTemplet.MainRewardData.rewardType, stageTemplet.MainRewardData.ID, false);
			}
			if (this.m_NKM_UI_OPERATION_EPISODE_BONUS_REWARD_MAIN_REWARD_SLOT != null && stageTemplet.MainRewardData != null && stageTemplet.MainRewardData.rewardType != NKM_REWARD_TYPE.RT_NONE && NKCUtil.IsValidReward(stageTemplet.MainRewardData.rewardType, stageTemplet.MainRewardData.ID) && flag)
			{
				NKCUISlot.SlotData slotData = null;
				NKM_REWARD_TYPE rewardType = stageTemplet.MainRewardData.rewardType;
				switch (rewardType)
				{
				case NKM_REWARD_TYPE.RT_UNIT:
				case NKM_REWARD_TYPE.RT_SHIP:
					break;
				case NKM_REWARD_TYPE.RT_MISC:
					slotData = NKCUISlot.SlotData.MakeMiscItemData(stageTemplet.MainRewardData.ID, (long)stageTemplet.MainRewardData.MaxValue, 0);
					goto IL_2B3;
				case NKM_REWARD_TYPE.RT_USER_EXP:
					goto IL_2B3;
				case NKM_REWARD_TYPE.RT_EQUIP:
					slotData = NKCUISlot.SlotData.MakeEquipData(stageTemplet.MainRewardData.ID, stageTemplet.MainRewardData.MinValue, 0);
					goto IL_2B3;
				case NKM_REWARD_TYPE.RT_MOLD:
					slotData = NKCUISlot.SlotData.MakeMoldItemData(stageTemplet.MainRewardData.ID, (long)stageTemplet.MainRewardData.MaxValue);
					goto IL_2B3;
				default:
					if (rewardType != NKM_REWARD_TYPE.RT_OPERATOR)
					{
						goto IL_2B3;
					}
					break;
				}
				slotData = NKCUISlot.SlotData.MakeUnitData(stageTemplet.MainRewardData.ID, 1, 0, 0);
				IL_2B3:
				this.m_NKM_UI_OPERATION_EPISODE_BONUS_REWARD_MAIN_REWARD_SLOT.SetData(slotData, true, null);
				NKCUIComButton component = this.m_NKM_UI_OPERATION_EPISODE_BONUS_REWARD_MAIN_REWARD_SLOT.gameObject.GetComponent<NKCUIComButton>();
				if (component != null)
				{
					component.PointerDown.RemoveAllListeners();
					component.PointerDown.AddListener(delegate(PointerEventData x)
					{
						NKCUITooltip.Instance.Open(slotData, new Vector2?(x.position));
					});
				}
				this.m_NKM_UI_OPERATION_EPISODE_BONUS_REWARD_MAIN_REWARD_SLOT.DisableItemCount();
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_EPISODE_BONUS_REWARD_MAIN_REWARD_SLOT, true);
			}
			this.SetFirstReward(nkmuserData, stageTemplet);
			this.SetMedalClear(nkmuserData, stageTemplet);
		}

		// Token: 0x060063E6 RID: 25574 RVA: 0x001FA940 File Offset: 0x001F8B40
		private void SetFirstReward(NKMUserData userData, NKMStageTempletV2 stageTemplet)
		{
			if (this.m_objFirstReward != null && stageTemplet.GetFirstRewardData() != FirstRewardData.Empty)
			{
				NKCUtil.SetGameobjectActive(this.m_objFirstReward, true);
				FirstRewardData firstRewardData = stageTemplet.GetFirstRewardData();
				bool completeMark = NKMEpisodeMgr.CheckClear(userData, stageTemplet);
				if (this.m_slotFirstReward != null && firstRewardData != null && firstRewardData.Type != NKM_REWARD_TYPE.RT_NONE && firstRewardData.RewardId != 0)
				{
					NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeRewardTypeData(firstRewardData.Type, firstRewardData.RewardId, firstRewardData.RewardQuantity, 0);
					this.m_slotFirstReward.SetData(slotData, true, null);
					this.m_slotFirstReward.SetCompleteMark(completeMark);
					this.m_slotFirstReward.SetFirstGetMark(true);
					NKCUIComButton component = this.m_slotFirstReward.gameObject.GetComponent<NKCUIComButton>();
					if (component != null)
					{
						component.PointerDown.RemoveAllListeners();
						component.PointerDown.AddListener(delegate(PointerEventData x)
						{
							NKCUITooltip.Instance.Open(slotData, new Vector2?(x.position));
						});
						return;
					}
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objFirstReward, false);
			}
		}

		// Token: 0x060063E7 RID: 25575 RVA: 0x001FAA50 File Offset: 0x001F8C50
		private void SetMedalClear(NKMUserData userData, NKMStageTempletV2 stageTemplet)
		{
			if (this.m_objMedalClear != null && this.m_slotMedalClear != null && stageTemplet.MissionReward != null && stageTemplet.MissionReward.rewardType != NKM_REWARD_TYPE.RT_NONE && stageTemplet.MissionReward.ID != 0)
			{
				NKCUtil.SetGameobjectActive(this.m_objMedalClear, true);
				NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeRewardTypeData(stageTemplet.MissionReward.rewardType, stageTemplet.MissionReward.ID, stageTemplet.MissionReward.Count, 0);
				this.m_slotMedalClear.SetData(slotData, true, null);
				this.m_slotMedalClear.SetFirstAllClearMark(true);
				bool medalAllClear = NKMEpisodeMgr.GetMedalAllClear(userData, stageTemplet);
				this.m_slotMedalClear.SetCompleteMark(medalAllClear);
				NKCUIComButton component = this.m_slotMedalClear.gameObject.GetComponent<NKCUIComButton>();
				if (component != null)
				{
					component.PointerDown.RemoveAllListeners();
					component.PointerDown.AddListener(delegate(PointerEventData x)
					{
						NKCUITooltip.Instance.Open(slotData, new Vector2?(x.position));
					});
					return;
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objMedalClear, false);
			}
		}

		// Token: 0x060063E8 RID: 25576 RVA: 0x001FAB68 File Offset: 0x001F8D68
		public void UpdateINGWarfareDirectGoUI(NKMStageTempletV2 _stageTemplet)
		{
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			if (warfareGameData != null && warfareGameData.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_STOP)
			{
				NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(warfareGameData.warfareTempletID);
				if (nkmwarfareTemplet != null && NKMEpisodeMgr.FindStageTempletByBattleStrID(nkmwarfareTemplet.m_WarfareStrID).Key == _stageTemplet.Key)
				{
					NKCUtil.SetGameobjectActive(this.m_objPlaying, true);
					return;
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objPlaying, false);
		}

		// Token: 0x060063E9 RID: 25577 RVA: 0x001FABCB File Offset: 0x001F8DCB
		public void SetActive(bool bSet)
		{
			if (base.gameObject.activeSelf == !bSet)
			{
				base.gameObject.SetActive(bSet);
			}
		}

		// Token: 0x060063EA RID: 25578 RVA: 0x001FABEA File Offset: 0x001F8DEA
		public bool IsActive()
		{
			return base.gameObject.activeSelf;
		}

		// Token: 0x060063EB RID: 25579 RVA: 0x001FABF7 File Offset: 0x001F8DF7
		public void InvokeSelectSlot()
		{
			this.OnSelectedItemSlotImpl();
		}

		// Token: 0x060063EC RID: 25580 RVA: 0x001FABFF File Offset: 0x001F8DFF
		public void SetEnableNewMark(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objNew, bValue);
		}

		// Token: 0x060063ED RID: 25581 RVA: 0x001FAC0D File Offset: 0x001F8E0D
		public void SetSelectNode(bool bValue)
		{
		}

		// Token: 0x060063EE RID: 25582 RVA: 0x001FAC0F File Offset: 0x001F8E0F
		public void Close()
		{
			NKCAssetResourceManager.CloseInstance(this.m_instance);
			this.m_instance = null;
		}

		// Token: 0x04004F47 RID: 20295
		[Header("Dungeon")]
		public Image m_imgBoss;

		// Token: 0x04004F48 RID: 20296
		public GameObject m_objTraining;

		// Token: 0x04004F49 RID: 20297
		public GameObject m_objDungeonEPNumber;

		// Token: 0x04004F4A RID: 20298
		public Text m_lbDungeonEPNumber;

		// Token: 0x04004F4B RID: 20299
		public Text m_lbDungeonNumber;

		// Token: 0x04004F4C RID: 20300
		public Text m_lbDungeonName;

		// Token: 0x04004F4D RID: 20301
		public Text m_lbRecommendFightPower;

		// Token: 0x04004F4E RID: 20302
		public NKCUIComButton m_Button;

		// Token: 0x04004F4F RID: 20303
		public GameObject m_NKM_UI_OPERATION_EPISODE_lIST_SLOT_SHADOW_COVER_TITLE;

		// Token: 0x04004F50 RID: 20304
		public GameObject m_objWarfareBG;

		// Token: 0x04004F51 RID: 20305
		public Image m_NKM_UI_OPERATION_EPISODE_lIST_SLOT_BG_SHADOW;

		// Token: 0x04004F52 RID: 20306
		public Image m_NKM_UI_OPERATION_EPISODE_lIST_SLOT_BG_SHADOW_CLEAR;

		// Token: 0x04004F53 RID: 20307
		public GameObject m_objTrainingBG;

		// Token: 0x04004F54 RID: 20308
		public GameObject m_objTrainingClearBG;

		// Token: 0x04004F55 RID: 20309
		public GameObject m_objCutscenBG;

		// Token: 0x04004F56 RID: 20310
		public GameObject m_objCutscenDungeonBG;

		// Token: 0x04004F57 RID: 20311
		public GameObject m_objCutscenDungeonBGClear;

		// Token: 0x04004F58 RID: 20312
		public GameObject m_NKM_UI_OPERATION_EPISODE_lIST_SLOT_LEVEL;

		// Token: 0x04004F59 RID: 20313
		public GameObject m_objPlaying;

		// Token: 0x04004F5A RID: 20314
		public GameObject m_goMissionList;

		// Token: 0x04004F5B RID: 20315
		public GameObject m_goMission1_BG;

		// Token: 0x04004F5C RID: 20316
		public GameObject m_goMission2_BG;

		// Token: 0x04004F5D RID: 20317
		public GameObject m_goMission3_BG;

		// Token: 0x04004F5E RID: 20318
		public GameObject m_goMission1;

		// Token: 0x04004F5F RID: 20319
		public GameObject m_goMission2;

		// Token: 0x04004F60 RID: 20320
		public GameObject m_goMission3;

		// Token: 0x04004F61 RID: 20321
		public GameObject m_goClearOn;

		// Token: 0x04004F62 RID: 20322
		public GameObject m_goClearOff;

		// Token: 0x04004F63 RID: 20323
		public GameObject m_goLock;

		// Token: 0x04004F64 RID: 20324
		public Text m_lbLockDesc;

		// Token: 0x04004F65 RID: 20325
		public GameObject m_objNew;

		// Token: 0x04004F66 RID: 20326
		public GameObject m_objEventBadge;

		// Token: 0x04004F67 RID: 20327
		public CanvasGroup m_CGDGSlot;

		// Token: 0x04004F68 RID: 20328
		public Color m_colNormal;

		// Token: 0x04004F69 RID: 20329
		public Color m_colHard;

		// Token: 0x04004F6A RID: 20330
		[Header("보너스")]
		public GameObject m_NKM_UI_OPERATION_EPISODE_BONUS_REWARD_BONUS_TYPE;

		// Token: 0x04004F6B RID: 20331
		public Image m_Img_NKM_UI_OPERATION_EPISODE_BONUS_REWARD_BONUS_TYPE;

		// Token: 0x04004F6C RID: 20332
		public GameObject m_NKM_UI_OPERATION_EPISODE_BONUS_REWARD_MAIN_REWARD;

		// Token: 0x04004F6D RID: 20333
		public NKCUISlot m_NKM_UI_OPERATION_EPISODE_BONUS_REWARD_MAIN_REWARD_SLOT;

		// Token: 0x04004F6E RID: 20334
		[Header("최초 획득")]
		public GameObject m_objFirstReward;

		// Token: 0x04004F6F RID: 20335
		public NKCUISlot m_slotFirstReward;

		// Token: 0x04004F70 RID: 20336
		[Header("메달 보상")]
		public GameObject m_objMedalClear;

		// Token: 0x04004F71 RID: 20337
		public NKCUISlot m_slotMedalClear;

		// Token: 0x04004F72 RID: 20338
		[Header("일일 입장 제한")]
		public GameObject m_NKM_UI_OPERATION_EPISODE_ENTER_LIMIT;

		// Token: 0x04004F73 RID: 20339
		public Text EnterLimit_COUNT_TEXT;

		// Token: 0x04004F74 RID: 20340
		public GameObject m_NKM_UI_OPERATION_EPISODE_LIST_MISSION_BLACK;

		// Token: 0x04004F75 RID: 20341
		[Header("언락 재화")]
		public GameObject m_objUnlockItem;

		// Token: 0x04004F76 RID: 20342
		public NKCUISlot m_slotUnlockItem;

		// Token: 0x04004F77 RID: 20343
		[Header("클리어 시간")]
		public GameObject m_objClearTime;

		// Token: 0x04004F78 RID: 20344
		public Text m_lbClearTime;

		// Token: 0x04004F79 RID: 20345
		private float m_fElapsedTime;

		// Token: 0x04004F7A RID: 20346
		private IDungeonSlot.OnSelectedItemSlot m_OnSelectedSlot;

		// Token: 0x04004F7B RID: 20347
		private int m_ActID;

		// Token: 0x04004F7C RID: 20348
		private int m_StageIndex;

		// Token: 0x04004F7D RID: 20349
		private string m_StageBattleStrID = "";

		// Token: 0x04004F7E RID: 20350
		private int m_IndexToAnimateAlpha;

		// Token: 0x04004F7F RID: 20351
		private NKCAssetInstanceData m_instance;
	}
}
