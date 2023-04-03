using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKC.UI.Collection;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Lobby
{
	// Token: 0x02000C0D RID: 3085
	public class NKCUILobbyMenuCollection : NKCUILobbyMenuButtonBase
	{
		// Token: 0x170016B8 RID: 5816
		// (get) Token: 0x06008ED1 RID: 36561 RVA: 0x0030920C File Offset: 0x0030740C
		private string PercentString
		{
			get
			{
				return Mathf.FloorToInt(this.m_fPercent * 100f).ToString() + "%";
			}
		}

		// Token: 0x06008ED2 RID: 36562 RVA: 0x0030923C File Offset: 0x0030743C
		public void Init()
		{
			if (this.m_csbtnMenu != null)
			{
				this.m_csbtnMenu.PointerClick.RemoveAllListeners();
				this.m_csbtnMenu.PointerClick.AddListener(new UnityAction(this.OnButton));
			}
		}

		// Token: 0x06008ED3 RID: 36563 RVA: 0x00309278 File Offset: 0x00307478
		protected override void ContentsUpdate(NKMUserData userData)
		{
			NKCCollectionManager.Init();
			this.SetNotify(false);
			this.CheckCollectionTotalRate(userData);
		}

		// Token: 0x06008ED4 RID: 36564 RVA: 0x00309290 File Offset: 0x00307490
		private void CheckCollectionTotalRate(NKMUserData userData)
		{
			int num = 0;
			int num2 = 0;
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			if (armyData == null)
			{
				return;
			}
			bool flag = false;
			new List<NKCUICollectionTeamUp.TeamUpSlotData>();
			bool flag2;
			NKCUICollectionTeamUp.UpdateTeamUpList(ref num, ref num2, armyData, false, out flag2);
			if (flag2 && NKCUnitMissionManager.GetOpenTagCollectionTeamUp())
			{
				flag = true;
			}
			if (NKCUnitMissionManager.HasRewardEnableMission())
			{
				flag = true;
			}
			NKCUICollectionUnitList.UpdateCollectionUnitList(ref num, ref num2, NKM_UNIT_TYPE.NUT_NORMAL, false);
			NKCUICollectionUnitList.UpdateCollectionUnitList(ref num, ref num2, NKM_UNIT_TYPE.NUT_SHIP, false);
			if (!NKCOperatorUtil.IsHide() && NKCOperatorUtil.IsActive())
			{
				NKCUICollectionOperatorList.UpdateCollectionUnitList(ref num, ref num2, false);
			}
			this.AddCollectionStoryCount(userData, ref num, ref num2);
			this.m_fPercent = Mathf.Floor((float)num) / Mathf.Floor((float)num2);
			this.m_fPercent = Mathf.Clamp(this.m_fPercent, 0f, 1f);
			NKCUtil.SetLabelText(this.m_NKM_UI_LOBBY_RIGHT_MENU_2_ALBUM_COUNT, this.PercentString);
			NKCUtil.SetImageFillAmount(this.m_imgProgress, this.m_fPercent);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_LOBBY_RIGHT_MENU_2_ALBUM_Reddot, flag);
			base.SetNotify(flag);
		}

		// Token: 0x06008ED5 RID: 36565 RVA: 0x00309380 File Offset: 0x00307580
		private void AddCollectionUnitCount(ref int collected, ref int total, NKM_UNIT_TYPE type)
		{
			List<int> unitList = NKCCollectionManager.GetUnitList(type);
			for (int i = 0; i < unitList.Count; i++)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitList[i]);
				if (unitTempletBase != null)
				{
					NKCCollectionUnitTemplet unitTemplet = NKCCollectionManager.GetUnitTemplet(unitTempletBase.m_UnitID);
					if ((unitTemplet == null || !unitTemplet.m_bExclude) && unitTempletBase.CollectionEnableByTag)
					{
						if (NKCUICollectionUnitList.IsHasUnit(type, unitList[i]))
						{
							collected++;
						}
						total++;
					}
				}
			}
		}

		// Token: 0x06008ED6 RID: 36566 RVA: 0x003093F0 File Offset: 0x003075F0
		private void AddCollectionStoryCount(NKMUserData userData, ref int collected, ref int total)
		{
			if (userData == null)
			{
				return;
			}
			Dictionary<int, storyUnlockData> storyData = NKCCollectionManager.GetStoryData();
			Dictionary<int, List<int>> epiSodeStageIdData = NKCCollectionManager.GetEpiSodeStageIdData();
			if (storyData == null)
			{
				return;
			}
			Dictionary<NKCCollectionManager.COLLECTION_STORY_CATEGORY, List<NKCUICollectionStory.EpData>> dictionary = new Dictionary<NKCCollectionManager.COLLECTION_STORY_CATEGORY, List<NKCUICollectionStory.EpData>>();
			foreach (NKMEpisodeTempletV2 nkmepisodeTempletV in NKMEpisodeTempletV2.Values)
			{
				if (NKCUICollectionStory.IsVaildCollectionStory(nkmepisodeTempletV) && epiSodeStageIdData.ContainsKey(nkmepisodeTempletV.m_EpisodeID))
				{
					if (!storyData.ContainsKey(epiSodeStageIdData[nkmepisodeTempletV.m_EpisodeID][0]))
					{
						Debug.LogError(string.Format("CutScene Templet does'nt contain stage ID: {0}", epiSodeStageIdData[nkmepisodeTempletV.m_EpisodeID][0]));
					}
					else
					{
						EPISODE_CATEGORY episodeCategory = storyData[epiSodeStageIdData[nkmepisodeTempletV.m_EpisodeID][0]].m_EpisodeCategory;
						int sortIndex = (nkmepisodeTempletV != null) ? nkmepisodeTempletV.m_SortIndex : 0;
						NKCUICollectionStory.EpData epData = new NKCUICollectionStory.EpData(episodeCategory, nkmepisodeTempletV.m_EpisodeID, nkmepisodeTempletV.m_EpisodeTitle, nkmepisodeTempletV.m_EpisodeName, sortIndex);
						int count = epiSodeStageIdData[nkmepisodeTempletV.m_EpisodeID].Count;
						for (int i = 0; i < count; i++)
						{
							int key = epiSodeStageIdData[nkmepisodeTempletV.m_EpisodeID][i];
							NKMStageTempletV2 nkmstageTempletV = NKMStageTempletV2.Find(key);
							if (nkmstageTempletV != null)
							{
								epData.m_lstEpisodeStages.Add(new NKCUICollectionStory.EpSlotData(storyData[nkmstageTempletV.Key].m_UnlockReqList, storyData[key].m_ActID, nkmstageTempletV.m_StageIndex, "", ""));
							}
						}
						NKCCollectionManager.COLLECTION_STORY_CATEGORY collectionStoryCategory = NKCCollectionManager.GetCollectionStoryCategory(episodeCategory);
						if (dictionary.ContainsKey(collectionStoryCategory))
						{
							dictionary[collectionStoryCategory].Add(epData);
						}
						else
						{
							dictionary.Add(collectionStoryCategory, new List<NKCUICollectionStory.EpData>
							{
								epData
							});
						}
					}
				}
			}
			NKCUICollectionStory.EpData epData2 = new NKCUICollectionStory.EpData(new NKMDiveTemplet());
			int num = 0;
			foreach (NKMDiveTemplet nkmdiveTemplet in NKMTempletContainer<NKMDiveTemplet>.Values)
			{
				if (NKCUICollectionStory.IsValidCollectionStory(nkmdiveTemplet) && storyData.ContainsKey(nkmdiveTemplet.StageID))
				{
					storyUnlockData storyUnlockData = storyData[nkmdiveTemplet.StageID];
					if (!string.IsNullOrEmpty(nkmdiveTemplet.CutsceneDiveEnter))
					{
						List<UnlockInfo> unlockReqList = storyUnlockData.m_UnlockReqList;
						epData2.m_lstEpisodeStages.Add(new NKCUICollectionStory.EpSlotData(unlockReqList, num, 1, nkmdiveTemplet.CutsceneDiveEnter, ""));
					}
					if (!string.IsNullOrEmpty(nkmdiveTemplet.CutsceneDiveStart))
					{
						List<UnlockInfo> unlockReqList2 = storyUnlockData.m_UnlockReqList;
						epData2.m_lstEpisodeStages.Add(new NKCUICollectionStory.EpSlotData(unlockReqList2, num, 2, nkmdiveTemplet.CutsceneDiveStart, ""));
					}
					if (!string.IsNullOrEmpty(nkmdiveTemplet.CutsceneDiveBossBefore))
					{
						List<UnlockInfo> unlockReqList3 = storyUnlockData.m_UnlockReqList;
						epData2.m_lstEpisodeStages.Add(new NKCUICollectionStory.EpSlotData(unlockReqList3, num, 3, nkmdiveTemplet.CutsceneDiveBossBefore, ""));
					}
					if (!string.IsNullOrEmpty(nkmdiveTemplet.CutsceneDiveBossAfter))
					{
						List<UnlockInfo> unlockReqList4 = storyUnlockData.m_UnlockReqList;
						epData2.m_lstEpisodeStages.Add(new NKCUICollectionStory.EpSlotData(unlockReqList4, num, 4, nkmdiveTemplet.CutsceneDiveBossAfter, ""));
					}
					num++;
				}
			}
			if (epData2.m_lstEpisodeStages.Count > 0)
			{
				List<NKCUICollectionStory.EpData> list = new List<NKCUICollectionStory.EpData>();
				list.Add(epData2);
				if (!dictionary.ContainsKey(NKCCollectionManager.COLLECTION_STORY_CATEGORY.WORLDMAP))
				{
					dictionary.Add(NKCCollectionManager.COLLECTION_STORY_CATEGORY.WORLDMAP, list);
				}
				else
				{
					dictionary[NKCCollectionManager.COLLECTION_STORY_CATEGORY.WORLDMAP].Add(epData2);
				}
			}
			foreach (object obj in Enum.GetValues(typeof(NKCCollectionManager.COLLECTION_STORY_CATEGORY)))
			{
				NKCCollectionManager.COLLECTION_STORY_CATEGORY type = (NKCCollectionManager.COLLECTION_STORY_CATEGORY)obj;
				Dictionary<int, NKCUICollectionStory.StorySlotData> dictionary2 = new Dictionary<int, NKCUICollectionStory.StorySlotData>();
				NKCUICollectionStory.UpdateEpisodeCategory(ref collected, ref total, type, dictionary, false, ref dictionary2);
			}
		}

		// Token: 0x170016B9 RID: 5817
		// (get) Token: 0x06008ED7 RID: 36567 RVA: 0x0030980C File Offset: 0x00307A0C
		// (set) Token: 0x06008ED8 RID: 36568 RVA: 0x0030982D File Offset: 0x00307A2D
		public float Fillrate
		{
			get
			{
				if (!(this.m_imgProgress != null))
				{
					return 0f;
				}
				return this.m_imgProgress.fillAmount;
			}
			set
			{
				if (this.m_imgProgress != null)
				{
					this.m_imgProgress.fillAmount = value;
				}
			}
		}

		// Token: 0x06008ED9 RID: 36569 RVA: 0x0030984C File Offset: 0x00307A4C
		public override void PlayAnimation(bool bActive)
		{
			this.m_NKM_UI_LOBBY_RIGHT_MENU_2_ALBUM_COUNT.DOKill(false);
			this.m_imgProgress.DOKill(false);
			if (bActive)
			{
				this.animBuffer = Mathf.FloorToInt(this.m_fPercent * 100f).ToString();
				DOTween.To(() => this.animBuffer, delegate(string x)
				{
					this.animBuffer = x;
					this.m_NKM_UI_LOBBY_RIGHT_MENU_2_ALBUM_COUNT.text = x + "%";
				}, Mathf.FloorToInt(this.m_fPercent * 100f).ToString(), 0.6f).SetOptions(false, ScrambleMode.Numerals, null).SetTarget(this.m_NKM_UI_LOBBY_RIGHT_MENU_2_ALBUM_COUNT).SetEase(Ease.InCubic);
				this.Fillrate = 0f;
				this.m_imgProgress.DOFillAmount(this.m_fPercent, 0.6f).SetEase(Ease.InCubic);
				return;
			}
			NKCUtil.SetLabelText(this.m_NKM_UI_LOBBY_RIGHT_MENU_2_ALBUM_COUNT, this.PercentString);
			this.Fillrate = this.m_fPercent;
		}

		// Token: 0x06008EDA RID: 36570 RVA: 0x00309933 File Offset: 0x00307B33
		private void OnButton()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_COLLECTION, true);
		}

		// Token: 0x04007BDA RID: 31706
		public NKCUIComStateButton m_csbtnMenu;

		// Token: 0x04007BDB RID: 31707
		public Text m_NKM_UI_LOBBY_RIGHT_MENU_2_ALBUM_COUNT;

		// Token: 0x04007BDC RID: 31708
		public Image m_imgProgress;

		// Token: 0x04007BDD RID: 31709
		public GameObject m_NKM_UI_LOBBY_RIGHT_MENU_2_ALBUM_Reddot;

		// Token: 0x04007BDE RID: 31710
		private float m_fPercent;

		// Token: 0x04007BDF RID: 31711
		private const float m_fAnimTime = 0.6f;

		// Token: 0x04007BE0 RID: 31712
		private const Ease m_eAnimEase = Ease.InCubic;

		// Token: 0x04007BE1 RID: 31713
		private string animBuffer;
	}
}
