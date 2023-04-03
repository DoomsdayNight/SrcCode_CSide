using System;
using System.Collections.Generic;
using ClientPacket.Mode;
using NKC.Templet;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A0F RID: 2575
	public class NKCUIOperationSubSummarySlot : MonoBehaviour
	{
		// Token: 0x06007065 RID: 28773 RVA: 0x00253CF8 File Offset: 0x00251EF8
		public bool SetData(NKCEpisodeSummaryTemplet summaryTemplet)
		{
			this.m_fDeltaTime = 0f;
			this.m_btn.PointerClick.RemoveAllListeners();
			this.m_btn.PointerClick.AddListener(new UnityAction(this.OnClickBtn));
			if (summaryTemplet == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return false;
			}
			this.m_SummaryTemplet = summaryTemplet;
			this.m_ShortcutType = summaryTemplet.m_ShortcutType;
			this.m_ShortcutParam = summaryTemplet.m_ShortcutParam;
			if (this.m_imgSlotBG != null && !string.IsNullOrEmpty(summaryTemplet.m_SubResourceID))
			{
				NKCUtil.SetImageSprite(this.m_imgSlotBG, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_OPERATION_Thumbnail", summaryTemplet.m_SubResourceID, false), false);
			}
			if (this.m_imgCategory != null)
			{
				NKMEpisodeGroupTemplet nkmepisodeGroupTemplet = NKMEpisodeGroupTemplet.Find(this.m_SummaryTemplet.m_EPCategory);
				if (nkmepisodeGroupTemplet != null)
				{
					NKCUtil.SetImageSprite(this.m_imgCategory, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_OPERATION_Thumbnail", nkmepisodeGroupTemplet.m_EPGroupIcon, false), false);
				}
			}
			if (this.m_lbStageNum != null)
			{
				NKCUtil.SetLabelText(this.m_lbStageNum, "");
			}
			NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(summaryTemplet.m_EpisodeID, EPISODE_DIFFICULTY.NORMAL);
			if (nkmepisodeTempletV != null)
			{
				NKCUtil.SetLabelText(this.m_lbTitle, nkmepisodeTempletV.GetEpisodeName());
				if (this.m_lbCategory != null)
				{
					NKCUtil.SetLabelText(this.m_lbCategory, string.Format(NKCUtilString.GET_STRING_EPISODE_PROGRESS, NKCUtilString.GetEpisodeCategory(nkmepisodeTempletV.m_EPCategory)));
				}
				NKCUtil.SetGameobjectActive(this.m_ObjEventDrop, nkmepisodeTempletV.HaveEventDrop);
				if (this.m_SummaryTemplet.HasDateLimit())
				{
					this.m_bUseRemainTime = true;
					NKCUtil.SetGameobjectActive(this.m_objRemainTime, true);
					this.m_EndDateUTC = this.m_SummaryTemplet.IntervalTemplet.GetEndDateUtc();
					this.SetRemainTime();
				}
				else
				{
					this.m_bUseRemainTime = false;
					NKCUtil.SetGameobjectActive(this.m_objRemainTime, false);
				}
				NKCUtil.SetGameobjectActive(this.m_objFierceReward, false);
			}
			else
			{
				if (this.m_SummaryTemplet.m_EPCategory == EPISODE_CATEGORY.EC_FIERCE)
				{
					NKCUtil.SetLabelText(this.m_lbTitle, NKCUtilString.GET_STRING_FIERCE);
					NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
					NKCFierceBattleSupportDataMgr.FIERCE_STATUS status = nkcfierceBattleSupportDataMgr.GetStatus();
					if (nkcfierceBattleSupportDataMgr.FierceTemplet == null || !nkcfierceBattleSupportDataMgr.IsCanAccessFierce())
					{
						NKCUtil.SetGameobjectActive(base.gameObject, false);
						return false;
					}
					this.m_ShortcutType = NKM_SHORTCUT_TYPE.SHORTCUT_FIERCE;
					switch (status)
					{
					case NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_WAIT:
						this.m_EndDateUTC = NKMTime.LocalToUTC(nkcfierceBattleSupportDataMgr.FierceTemplet.FierceGameStart, 0);
						break;
					case NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_ACTIVATE:
						this.m_EndDateUTC = NKMTime.LocalToUTC(nkcfierceBattleSupportDataMgr.FierceTemplet.FierceGameEnd, 0);
						break;
					case NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_REWARD:
					case NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_COMPLETE:
						this.m_EndDateUTC = NKMTime.LocalToUTC(nkcfierceBattleSupportDataMgr.FierceTemplet.FierceRewardPeriodEnd, 0);
						break;
					}
					NKCUtil.SetGameobjectActive(this.m_objFierceReward, status == NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_REWARD || status == NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_COMPLETE);
					this.m_bUseRemainTime = (this.m_EndDateUTC != DateTime.MinValue);
					NKCUtil.SetGameobjectActive(this.m_objRemainTime, this.m_bUseRemainTime);
					if (this.m_bUseRemainTime)
					{
						this.SetRemainTime();
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objFierceReward, false);
				}
				NKCUtil.SetGameobjectActive(this.m_ObjEventDrop, false);
			}
			return true;
		}

		// Token: 0x06007066 RID: 28774 RVA: 0x00253FF0 File Offset: 0x002521F0
		public bool SetLastPlayInfo(NKMShortCutInfo lastPlayData)
		{
			this.m_btn.PointerClick.RemoveAllListeners();
			this.m_btn.PointerClick.AddListener(new UnityAction(this.OnClickBtn));
			if (lastPlayData != null || lastPlayData.gameType == 0)
			{
				NKM_GAME_TYPE nkm_GAME_TYPE = (NKM_GAME_TYPE)lastPlayData.gameType;
				if (nkm_GAME_TYPE - NKM_GAME_TYPE.NGT_PRACTICE > 1)
				{
					switch (nkm_GAME_TYPE)
					{
					case NKM_GAME_TYPE.NGT_TUTORIAL:
					case NKM_GAME_TYPE.NGT_CUTSCENE:
					case NKM_GAME_TYPE.NGT_PHASE:
						break;
					case NKM_GAME_TYPE.NGT_RAID:
					case NKM_GAME_TYPE.NGT_WORLDMAP:
					case NKM_GAME_TYPE.NGT_ASYNC_PVP:
					case NKM_GAME_TYPE.NGT_RAID_SOLO:
						goto IL_362;
					case NKM_GAME_TYPE.NGT_SHADOW_PALACE:
					{
						NKCEpisodeSummaryTemplet nkcepisodeSummaryTemplet = NKCEpisodeSummaryTemplet.Find(EPISODE_CATEGORY.EC_SHADOW, 0);
						if (nkcepisodeSummaryTemplet != null)
						{
							this.m_SummaryTemplet = nkcepisodeSummaryTemplet;
							this.m_ShortcutType = nkcepisodeSummaryTemplet.m_ShortcutType;
							this.m_ShortcutParam = nkcepisodeSummaryTemplet.m_ShortcutParam;
							if (this.m_imgCategory != null)
							{
								NKMEpisodeGroupTemplet nkmepisodeGroupTemplet = NKMEpisodeGroupTemplet.Find(this.m_SummaryTemplet.m_EPCategory);
								if (nkmepisodeGroupTemplet != null)
								{
									NKCUtil.SetImageSprite(this.m_imgCategory, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_OPERATION_Thumbnail", nkmepisodeGroupTemplet.m_EPGroupIcon, false), false);
								}
							}
							if (this.m_lbStageNum != null)
							{
								NKCUtil.SetLabelText(this.m_lbStageNum, "");
							}
							NKCUtil.SetLabelText(this.m_lbTitle, NKCUtilString.GetEpisodeCategory(EPISODE_CATEGORY.EC_SHADOW));
							this.m_bUseRemainTime = false;
							NKCUtil.SetGameobjectActive(this.m_objRemainTime, false);
							NKCUtil.SetGameobjectActive(this.m_ObjEventDrop, false);
							NKCUtil.SetGameobjectActive(this.m_objFierceReward, false);
							return true;
						}
						goto IL_362;
					}
					case NKM_GAME_TYPE.NGT_FIERCE:
					{
						NKCEpisodeSummaryTemplet nkcepisodeSummaryTemplet = NKCEpisodeSummaryTemplet.Find(EPISODE_CATEGORY.EC_FIERCE, 0);
						if (nkcepisodeSummaryTemplet != null)
						{
							return this.SetData(nkcepisodeSummaryTemplet);
						}
						goto IL_362;
					}
					default:
					{
						if (nkm_GAME_TYPE != NKM_GAME_TYPE.NGT_TRIM)
						{
							goto IL_362;
						}
						NKCEpisodeSummaryTemplet nkcepisodeSummaryTemplet = NKCEpisodeSummaryTemplet.Find(EPISODE_CATEGORY.EC_TRIM, 0);
						if (nkcepisodeSummaryTemplet != null)
						{
							this.m_SummaryTemplet = nkcepisodeSummaryTemplet;
							this.m_ShortcutType = nkcepisodeSummaryTemplet.m_ShortcutType;
							this.m_ShortcutParam = nkcepisodeSummaryTemplet.m_ShortcutParam;
							if (this.m_imgCategory != null)
							{
								NKMEpisodeGroupTemplet nkmepisodeGroupTemplet2 = NKMEpisodeGroupTemplet.Find(this.m_SummaryTemplet.m_EPCategory);
								if (nkmepisodeGroupTemplet2 != null)
								{
									NKCUtil.SetImageSprite(this.m_imgCategory, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_OPERATION_Thumbnail", nkmepisodeGroupTemplet2.m_EPGroupIcon, false), false);
								}
							}
							if (this.m_lbStageNum != null)
							{
								NKCUtil.SetLabelText(this.m_lbStageNum, "");
							}
							NKCUtil.SetLabelText(this.m_lbTitle, NKCUtilString.GetEpisodeCategory(EPISODE_CATEGORY.EC_TRIM));
							this.m_bUseRemainTime = false;
							NKCUtil.SetGameobjectActive(this.m_objRemainTime, false);
							NKCUtil.SetGameobjectActive(this.m_ObjEventDrop, false);
							NKCUtil.SetGameobjectActive(this.m_objFierceReward, false);
							return true;
						}
						goto IL_362;
					}
					}
				}
				NKMStageTempletV2 nkmstageTempletV = NKMStageTempletV2.Find(lastPlayData.stageId);
				if (nkmstageTempletV != null)
				{
					NKCUtil.SetLabelText(this.m_lbTitle, nkmstageTempletV.EpisodeTemplet.GetEpisodeName());
					NKMDungeonTempletBase dungeonTempletBase = nkmstageTempletV.DungeonTempletBase;
					if (dungeonTempletBase != null && dungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_CUTSCENE)
					{
						NKCUtil.SetLabelText(this.m_lbStageNum, string.Format(NKCUtilString.GET_STRING_EP_CUTSCEN_NUMBER, nkmstageTempletV.m_StageUINum));
					}
					else
					{
						NKCUtil.SetLabelText(this.m_lbStageNum, string.Format("{0}-{1}", nkmstageTempletV.ActId, nkmstageTempletV.m_StageUINum));
					}
					NKMEpisodeGroupTemplet nkmepisodeGroupTemplet3 = NKMEpisodeGroupTemplet.Find(nkmstageTempletV.EpisodeCategory);
					if (nkmepisodeGroupTemplet3 != null)
					{
						NKCUtil.SetImageSprite(this.m_imgCategory, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_OPERATION_Thumbnail", nkmepisodeGroupTemplet3.m_EPGroupIcon, false), false);
					}
					this.m_bUseRemainTime = nkmstageTempletV.EpisodeTemplet.HasEventTimeLimit;
					NKCUtil.SetGameobjectActive(this.m_objRemainTime, this.m_bUseRemainTime);
					if (this.m_bUseRemainTime)
					{
						this.m_EndDateUTC = nkmstageTempletV.EpisodeTemplet.EpisodeDateEndUtc;
					}
					this.m_ShortcutType = NKM_SHORTCUT_TYPE.SHORTCUT_DUNGEON;
					this.m_ShortcutParam = nkmstageTempletV.Key.ToString();
					NKCUtil.SetGameobjectActive(this.m_ObjEventDrop, nkmstageTempletV.HaveEventDrop);
					NKCUtil.SetGameobjectActive(this.m_objFierceReward, false);
					return true;
				}
			}
			IL_362:
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			return false;
		}

		// Token: 0x06007067 RID: 28775 RVA: 0x0025436C File Offset: 0x0025256C
		public bool SetMainStreamProgress()
		{
			this.m_btn.PointerClick.RemoveAllListeners();
			this.m_btn.PointerClick.AddListener(new UnityAction(this.OnClickBtn));
			NKMStageTempletV2 nkmstageTempletV = this.FindPlaybleStageTemplet();
			if (nkmstageTempletV == null)
			{
				return false;
			}
			this.m_ShortcutType = NKM_SHORTCUT_TYPE.SHORTCUT_DUNGEON;
			this.m_ShortcutParam = nkmstageTempletV.Key.ToString();
			NKCUtil.SetLabelText(this.m_lbTitle, nkmstageTempletV.EpisodeTemplet.GetEpisodeName());
			NKMDungeonTempletBase dungeonTempletBase = nkmstageTempletV.DungeonTempletBase;
			if (dungeonTempletBase != null && dungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_CUTSCENE)
			{
				NKCUtil.SetLabelText(this.m_lbStageNum, string.Format(NKCUtilString.GET_STRING_EP_CUTSCEN_NUMBER, nkmstageTempletV.m_StageUINum));
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbStageNum, string.Format("{0}-{1}", nkmstageTempletV.ActId, nkmstageTempletV.m_StageUINum));
			}
			NKMEpisodeGroupTemplet nkmepisodeGroupTemplet = NKMEpisodeGroupTemplet.Find(nkmstageTempletV.EpisodeCategory);
			if (nkmepisodeGroupTemplet != null)
			{
				NKCUtil.SetImageSprite(this.m_imgCategory, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_OPERATION_Thumbnail", nkmepisodeGroupTemplet.m_EPGroupIcon, false), false);
			}
			NKCEpisodeSummaryTemplet nkcepisodeSummaryTemplet = NKCEpisodeSummaryTemplet.Find(nkmstageTempletV.EpisodeCategory, nkmstageTempletV.EpisodeId);
			if (nkcepisodeSummaryTemplet != null && this.m_imgSlotBG != null && !string.IsNullOrEmpty(nkcepisodeSummaryTemplet.m_SubResourceID))
			{
				NKCUtil.SetImageSprite(this.m_imgSlotBG, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_OPERATION_Thumbnail", nkcepisodeSummaryTemplet.m_SubResourceID, false), false);
			}
			this.m_bUseRemainTime = false;
			NKCUtil.SetGameobjectActive(this.m_objRemainTime, false);
			NKCUtil.SetGameobjectActive(this.m_ObjEventDrop, nkmstageTempletV.EpisodeTemplet.HaveEventDrop);
			NKCUtil.SetGameobjectActive(this.m_objFierceReward, false);
			return true;
		}

		// Token: 0x06007068 RID: 28776 RVA: 0x002544F8 File Offset: 0x002526F8
		private NKMStageTempletV2 FindPlaybleStageTemplet()
		{
			NKMUserData cNKMUserData = NKCScenManager.CurrentUserData();
			List<NKMEpisodeTempletV2> listNKMEpisodeTempletByCategory = NKMEpisodeMgr.GetListNKMEpisodeTempletByCategory(EPISODE_CATEGORY.EC_MAINSTREAM, true, EPISODE_DIFFICULTY.NORMAL);
			NKMEpisodeTempletV2 nkmepisodeTempletV = null;
			for (int i = listNKMEpisodeTempletByCategory.Count - 1; i >= 0; i--)
			{
				if (NKMEpisodeMgr.IsPossibleEpisode(cNKMUserData, listNKMEpisodeTempletByCategory[i].m_EpisodeID, EPISODE_DIFFICULTY.NORMAL))
				{
					nkmepisodeTempletV = listNKMEpisodeTempletByCategory[i];
					break;
				}
			}
			NKMStageTempletV2 result = null;
			bool flag = false;
			foreach (KeyValuePair<int, List<NKMStageTempletV2>> keyValuePair in nkmepisodeTempletV.m_DicStage)
			{
				if (flag)
				{
					break;
				}
				int num = 0;
				while (num < keyValuePair.Value.Count && !flag)
				{
					if (NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, keyValuePair.Value[num].m_UnlockInfo, false))
					{
						result = keyValuePair.Value[num];
					}
					else
					{
						flag = true;
					}
					num++;
				}
			}
			return result;
		}

		// Token: 0x06007069 RID: 28777 RVA: 0x002545EC File Offset: 0x002527EC
		private void SetRemainTime()
		{
			NKCUtil.SetLabelText(this.m_lbRemainTime, NKCUtilString.GetRemainTimeString(this.m_EndDateUTC, 1));
			if (NKCSynchronizedTime.IsFinished(this.m_EndDateUTC))
			{
				this.m_bUseRemainTime = false;
			}
		}

		// Token: 0x0600706A RID: 28778 RVA: 0x0025461C File Offset: 0x0025281C
		private void Update()
		{
			if (this.m_bUseRemainTime && this.m_objRemainTime != null && this.m_objRemainTime.activeSelf)
			{
				this.m_fDeltaTime += Time.deltaTime;
				if (this.m_fDeltaTime > 1f)
				{
					this.m_fDeltaTime -= 1f;
					this.SetRemainTime();
				}
			}
		}

		// Token: 0x0600706B RID: 28779 RVA: 0x00254684 File Offset: 0x00252884
		private void OnClickBtn()
		{
			if (this.m_ShortcutType == NKM_SHORTCUT_TYPE.SHORTCUT_FIERCE)
			{
				NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
				nkcfierceBattleSupportDataMgr.GetStatus();
				if (nkcfierceBattleSupportDataMgr.FierceTemplet == null || !nkcfierceBattleSupportDataMgr.IsCanAccessFierce())
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OPERATION, true);
					return;
				}
			}
			NKCContentManager.MoveToShortCut(this.m_ShortcutType, this.m_ShortcutParam, true);
		}

		// Token: 0x04005C1D RID: 23581
		[Header("공용")]
		public NKCUIComStateButton m_btn;

		// Token: 0x04005C1E RID: 23582
		public GameObject m_objRemainTime;

		// Token: 0x04005C1F RID: 23583
		public Text m_lbRemainTime;

		// Token: 0x04005C20 RID: 23584
		public GameObject m_ObjEventDrop;

		// Token: 0x04005C21 RID: 23585
		[Header("에피소드 타이틀")]
		public Text m_lbTitle;

		// Token: 0x04005C22 RID: 23586
		[Header("Big 전용")]
		public Text m_lbCategory;

		// Token: 0x04005C23 RID: 23587
		[Header("Small 전용")]
		public Image m_imgSlotBG;

		// Token: 0x04005C24 RID: 23588
		public Image m_imgCategory;

		// Token: 0x04005C25 RID: 23589
		[Header("격전지원 결산중 표시")]
		public GameObject m_objFierceReward;

		// Token: 0x04005C26 RID: 23590
		[Header("스테이지 진행도")]
		public Text m_lbStageNum;

		// Token: 0x04005C27 RID: 23591
		private NKCEpisodeSummaryTemplet m_SummaryTemplet;

		// Token: 0x04005C28 RID: 23592
		private bool m_bUseRemainTime;

		// Token: 0x04005C29 RID: 23593
		private DateTime m_EndDateUTC = DateTime.MinValue;

		// Token: 0x04005C2A RID: 23594
		private NKM_SHORTCUT_TYPE m_ShortcutType;

		// Token: 0x04005C2B RID: 23595
		private string m_ShortcutParam;

		// Token: 0x04005C2C RID: 23596
		private float m_fDeltaTime;
	}
}
