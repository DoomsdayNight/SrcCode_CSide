using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Collection
{
	// Token: 0x02000C28 RID: 3112
	public class NKCUICollectionStory : MonoBehaviour
	{
		// Token: 0x06009042 RID: 36930 RVA: 0x003110EC File Offset: 0x0030F2EC
		public void Init(NKCUICollection.OnSyncCollectingData callBack, NKCUICollection.OnStoryCutscen cutscenCallBack)
		{
			if (callBack != null)
			{
				this.dOnSyncCollectingData = callBack;
			}
			if (cutscenCallBack != null)
			{
				this.dOnStoryCutscen = cutscenCallBack;
			}
			if (null != this.m_LoopScrollRect)
			{
				this.m_LoopScrollRect.dOnGetObject += this.GetSlot;
				this.m_LoopScrollRect.dOnReturnObject += this.ReturnSlot;
				this.m_LoopScrollRect.dOnProvideData += this.ProvideSlotData;
				this.m_LoopScrollRect.PrepareCells(0);
			}
			NKCUtil.SetScrollHotKey(this.m_srStory, null);
			this.InitEpisodeData();
		}

		// Token: 0x06009043 RID: 36931 RVA: 0x0031117E File Offset: 0x0030F37E
		public static bool IsVaildCollectionStory(NKMEpisodeTempletV2 epTemplet)
		{
			return epTemplet != null && !epTemplet.m_bNoCollectionCutscene && epTemplet.CollectionEnableByTag && (epTemplet.m_EPCategory == EPISODE_CATEGORY.EC_MAINSTREAM || epTemplet.m_EPCategory == EPISODE_CATEGORY.EC_SIDESTORY || epTemplet.m_EPCategory == EPISODE_CATEGORY.EC_EVENT);
		}

		// Token: 0x06009044 RID: 36932 RVA: 0x003111B6 File Offset: 0x0030F3B6
		public static bool IsValidCollectionStory(NKMDiveTemplet diveTemplet)
		{
			return diveTemplet != null && (!string.IsNullOrEmpty(diveTemplet.CutsceneDiveStart) || !string.IsNullOrEmpty(diveTemplet.CutsceneDiveEnter) || !string.IsNullOrEmpty(diveTemplet.CutsceneDiveBossBefore) || !string.IsNullOrEmpty(diveTemplet.CutsceneDiveBossAfter));
		}

		// Token: 0x06009045 RID: 36933 RVA: 0x003111F4 File Offset: 0x0030F3F4
		private void InitEpisodeData()
		{
			Dictionary<int, storyUnlockData> storyData = NKCCollectionManager.GetStoryData();
			Dictionary<int, List<int>> epiSodeStageIdData = NKCCollectionManager.GetEpiSodeStageIdData();
			using (IEnumerator<NKMEpisodeTempletV2> enumerator = NKMEpisodeTempletV2.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					NKMEpisodeTempletV2 epTemplet = enumerator.Current;
					if (NKCUICollectionStory.IsVaildCollectionStory(epTemplet) && epiSodeStageIdData.ContainsKey(epTemplet.m_EpisodeID))
					{
						if (!storyData.ContainsKey(epiSodeStageIdData[epTemplet.m_EpisodeID][0]))
						{
							Debug.LogError(string.Format("CutScene Templet does'nt contain stage ID: {0}", epiSodeStageIdData[epTemplet.m_EpisodeID][0]));
						}
						else
						{
							EPISODE_CATEGORY episodeCategory = storyData[epiSodeStageIdData[epTemplet.m_EpisodeID][0]].m_EpisodeCategory;
							if (!this.m_dicCollectionEpisodeData.ContainsKey(NKCCollectionManager.GetCollectionStoryCategory(episodeCategory)) || this.m_dicCollectionEpisodeData[NKCCollectionManager.GetCollectionStoryCategory(episodeCategory)].Find((NKCUICollectionStory.EpData x) => x.m_EpisodeID == epTemplet.m_EpisodeID) == null)
							{
								int sortIndex = (epTemplet != null) ? epTemplet.m_SortIndex : 0;
								NKCUICollectionStory.EpData epData = new NKCUICollectionStory.EpData(episodeCategory, epTemplet.m_EpisodeID, epTemplet.m_EpisodeTitle, epTemplet.m_EpisodeName, sortIndex);
								int count = epiSodeStageIdData[epTemplet.m_EpisodeID].Count;
								for (int i = 0; i < count; i++)
								{
									int key = epiSodeStageIdData[epTemplet.m_EpisodeID][i];
									NKMStageTempletV2 nkmstageTempletV = NKMStageTempletV2.Find(key);
									if (nkmstageTempletV != null)
									{
										string idBefore = "";
										string idAfter = "";
										if (nkmstageTempletV.m_STAGE_TYPE == STAGE_TYPE.ST_PHASE)
										{
											NKMPhaseTemplet nkmphaseTemplet = NKMPhaseTemplet.Find(nkmstageTempletV.m_StageBattleStrID);
											if (nkmphaseTemplet != null)
											{
												idBefore = nkmphaseTemplet.m_CutScenStrIDBefore;
												idAfter = nkmphaseTemplet.m_CutScenStrIDAfter;
											}
										}
										else if (nkmstageTempletV.DungeonTempletBase != null)
										{
											idBefore = nkmstageTempletV.DungeonTempletBase.m_CutScenStrIDBefore;
											idAfter = nkmstageTempletV.DungeonTempletBase.m_CutScenStrIDAfter;
										}
										else if (nkmstageTempletV.WarfareTemplet != null)
										{
											idBefore = nkmstageTempletV.WarfareTemplet.m_CutScenStrIDBefore;
											idAfter = nkmstageTempletV.WarfareTemplet.m_CutScenStrIDAfter;
										}
										epData.m_lstEpisodeStages.Add(new NKCUICollectionStory.EpSlotData(storyData[nkmstageTempletV.Key].m_UnlockReqList, storyData[key].m_ActID, nkmstageTempletV.m_StageIndex, idBefore, idAfter));
									}
								}
								if (this.m_dicCollectionEpisodeData.ContainsKey(NKCCollectionManager.GetCollectionStoryCategory(episodeCategory)))
								{
									this.m_dicCollectionEpisodeData[NKCCollectionManager.GetCollectionStoryCategory(episodeCategory)].Add(epData);
								}
								else
								{
									List<NKCUICollectionStory.EpData> list = new List<NKCUICollectionStory.EpData>();
									list.Add(epData);
									this.m_dicCollectionEpisodeData.Add(NKCCollectionManager.GetCollectionStoryCategory(episodeCategory), list);
								}
							}
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
				List<NKCUICollectionStory.EpData> list2 = new List<NKCUICollectionStory.EpData>();
				list2.Add(epData2);
				if (!this.m_dicCollectionEpisodeData.ContainsKey(NKCCollectionManager.COLLECTION_STORY_CATEGORY.WORLDMAP))
				{
					this.m_dicCollectionEpisodeData.Add(NKCCollectionManager.COLLECTION_STORY_CATEGORY.WORLDMAP, list2);
				}
				else
				{
					this.m_dicCollectionEpisodeData[NKCCollectionManager.COLLECTION_STORY_CATEGORY.WORLDMAP].Add(epData2);
				}
			}
			if (storyData.ContainsKey(4001))
			{
				int num2 = 0;
				if (storyData[4001].m_UnlockReqList.Count > 0)
				{
					UnlockInfo item = new UnlockInfo(storyData[4001].m_UnlockReqList[0].eReqType, storyData[4001].m_UnlockReqList[0].reqValue);
					List<NKCUICollectionStory.EpSlotData> list3 = new List<NKCUICollectionStory.EpSlotData>();
					string episodeNameKey = "";
					foreach (NKMTrimTemplet nkmtrimTemplet in NKMTrimTemplet.Values)
					{
						foreach (List<NKMTrimDungeonTemplet> list4 in nkmtrimTemplet.TrimDungeonTemplets.Values)
						{
							foreach (NKMTrimDungeonTemplet nkmtrimDungeonTemplet in list4)
							{
								if (nkmtrimDungeonTemplet.ShowCutScene && nkmtrimDungeonTemplet.DungeonTempletBase != null)
								{
									episodeNameKey = nkmtrimTemplet.TirmGroupName;
									string cutScenStrIDBefore = nkmtrimDungeonTemplet.DungeonTempletBase.m_CutScenStrIDBefore;
									string cutScenStrIDAfter = nkmtrimDungeonTemplet.DungeonTempletBase.m_CutScenStrIDAfter;
									list3.Add(new NKCUICollectionStory.EpSlotData(new List<UnlockInfo>
									{
										item
									}, 0, ++num2, cutScenStrIDBefore, cutScenStrIDAfter));
								}
							}
						}
					}
					if (list3.Count > 0)
					{
						NKCUICollectionStory.EpData epData3 = new NKCUICollectionStory.EpData("SI_EPISODE_EPISODE_TRIM", episodeNameKey);
						epData3.m_lstEpisodeStages = list3;
						if (this.m_dicCollectionEpisodeData.ContainsKey(NKCCollectionManager.COLLECTION_STORY_CATEGORY.ETC))
						{
							this.m_dicCollectionEpisodeData[NKCCollectionManager.COLLECTION_STORY_CATEGORY.ETC].Add(epData3);
							return;
						}
						this.m_dicCollectionEpisodeData.Add(NKCCollectionManager.COLLECTION_STORY_CATEGORY.ETC, new List<NKCUICollectionStory.EpData>
						{
							epData3
						});
					}
				}
			}
		}

		// Token: 0x06009046 RID: 36934 RVA: 0x003118D0 File Offset: 0x0030FAD0
		public void Open()
		{
			if (this.m_dicStory.Count <= 0)
			{
				this.m_iCollected = 0;
				this.m_iTotalCollected = 0;
				foreach (object obj in Enum.GetValues(typeof(NKCCollectionManager.COLLECTION_STORY_CATEGORY)))
				{
					NKCCollectionManager.COLLECTION_STORY_CATEGORY type = (NKCCollectionManager.COLLECTION_STORY_CATEGORY)obj;
					int iReservePageIdx = NKCUICollectionStory.UpdateEpisodeCategory(ref this.m_iCollected, ref this.m_iTotalCollected, type, this.m_dicCollectionEpisodeData, true, ref this.m_dicStory);
					if (this.m_iReservePageIdx == 0)
					{
						this.m_iReservePageIdx = iReservePageIdx;
					}
				}
				this.CreatSideMenu();
				this.OnClicked(this.m_iReservePageIdx.ToString());
				foreach (NKCSideMenuSlot nkcsideMenuSlot in this.m_lstSideMenuSlot)
				{
					if (nkcsideMenuSlot.HasChild(this.m_iReservePageIdx.ToString()))
					{
						nkcsideMenuSlot.ForceSelect(true);
						nkcsideMenuSlot.OnValueChange(true);
					}
				}
			}
			this.SyncCollectionData();
		}

		// Token: 0x06009047 RID: 36935 RVA: 0x003119F8 File Offset: 0x0030FBF8
		public void Clear()
		{
			this.m_iCollected = 0;
			this.m_iTotalCollected = 0;
			this.m_lstStorySlot.Clear();
			this.m_iSelectEpsodeIdx = 0;
			this.m_dicStory.Clear();
		}

		// Token: 0x06009048 RID: 36936 RVA: 0x00311A25 File Offset: 0x0030FC25
		private void CreatSideMenu()
		{
			this.m_lstSideMenuSlot.Clear();
			this.CreateSideMenu(NKCCollectionManager.COLLECTION_STORY_CATEGORY.MAINSTREAM);
			this.CreateSideMenu(NKCCollectionManager.COLLECTION_STORY_CATEGORY.SIDESTORY);
			this.CreateSideMenu(NKCCollectionManager.COLLECTION_STORY_CATEGORY.EVENT);
			this.CreateSideMenu(NKCCollectionManager.COLLECTION_STORY_CATEGORY.WORLDMAP);
			this.CreateSideMenu(NKCCollectionManager.COLLECTION_STORY_CATEGORY.ETC);
			this.CreateExceptionSideMenu();
		}

		// Token: 0x06009049 RID: 36937 RVA: 0x00311A5C File Offset: 0x0030FC5C
		private void CreateSideMenu(NKCCollectionManager.COLLECTION_STORY_CATEGORY category)
		{
			if (!this.m_dicCollectionEpisodeData.ContainsKey(category))
			{
				return;
			}
			NKCSideMenuSlot nkcsideMenuSlot = this.CreatSideMenuSlot(category);
			if (nkcsideMenuSlot == null)
			{
				return;
			}
			List<NKCUICollectionStory.EpData> list = this.m_dicCollectionEpisodeData[category];
			if (category == NKCCollectionManager.COLLECTION_STORY_CATEGORY.SIDESTORY && list.Count > 0 && list[0].m_SortIndex > 0)
			{
				list.Sort(delegate(NKCUICollectionStory.EpData e1, NKCUICollectionStory.EpData e2)
				{
					if (e1.m_SortIndex > e2.m_SortIndex)
					{
						return 1;
					}
					if (e1.m_SortIndex < e2.m_SortIndex)
					{
						return -1;
					}
					return 0;
				});
			}
			int num = 0;
			for (int i = 0; i < list.Count; i++)
			{
				if (!this.m_dicStory.ContainsKey(list[i].m_EpisodeID))
				{
					Debug.Log(string.Format("fail - can not found episode id : {0}", list[i].m_EpisodeID));
				}
				else
				{
					NKCSideMenuSlotChild nkcsideMenuSlotChild = this.CreateSideMenuSlotchild(this.m_dicStory[list[i].m_EpisodeID].m_EpisodeTitle, list[i].m_EpisodeID.ToString());
					if (nkcsideMenuSlotChild == null)
					{
						Debug.LogWarning("fail - create side menu child : " + this.m_dicStory[list[i].m_EpisodeID].m_EpisodeTitle);
					}
					else
					{
						if (this.m_dicStory[list[i].m_EpisodeID].m_iEpisodeClearCnt <= 0)
						{
							nkcsideMenuSlotChild.m_Toggle.Lock(false);
						}
						else
						{
							nkcsideMenuSlotChild.m_Toggle.UnLock(false);
						}
						if (category == NKCCollectionManager.COLLECTION_STORY_CATEGORY.EVENT)
						{
							num += this.m_dicStory[list[i].m_EpisodeID].m_iEpisodeClearCnt;
						}
						nkcsideMenuSlot.AddSubSlot(nkcsideMenuSlotChild);
					}
				}
			}
			switch (category)
			{
			case NKCCollectionManager.COLLECTION_STORY_CATEGORY.SIDESTORY:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.SIDESTORY, 0, 0))
				{
					nkcsideMenuSlot.Lock();
				}
				break;
			case NKCCollectionManager.COLLECTION_STORY_CATEGORY.EVENT:
				NKMEpisodeMgr.GetListNKMEpisodeTempletByCategory(this.GetEpisodeCategory(category), true, EPISODE_DIFFICULTY.NORMAL);
				if (num > 0)
				{
					nkcsideMenuSlot.UnLock();
				}
				else
				{
					nkcsideMenuSlot.Lock();
				}
				break;
			case NKCCollectionManager.COLLECTION_STORY_CATEGORY.WORLDMAP:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.DIVE, 0, 0))
				{
					nkcsideMenuSlot.Lock();
				}
				break;
			case NKCCollectionManager.COLLECTION_STORY_CATEGORY.ETC:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.DIMENSION_TRIM, 0, 0))
				{
					nkcsideMenuSlot.Lock();
				}
				break;
			}
			this.m_lstSideMenuSlot.Add(nkcsideMenuSlot);
		}

		// Token: 0x0600904A RID: 36938 RVA: 0x00311C81 File Offset: 0x0030FE81
		private EPISODE_CATEGORY GetEpisodeCategory(NKCCollectionManager.COLLECTION_STORY_CATEGORY category)
		{
			switch (category)
			{
			case NKCCollectionManager.COLLECTION_STORY_CATEGORY.MAINSTREAM:
				return EPISODE_CATEGORY.EC_MAINSTREAM;
			case NKCCollectionManager.COLLECTION_STORY_CATEGORY.SIDESTORY:
				return EPISODE_CATEGORY.EC_SIDESTORY;
			}
			return EPISODE_CATEGORY.EC_EVENT;
		}

		// Token: 0x0600904B RID: 36939 RVA: 0x00311C9C File Offset: 0x0030FE9C
		private NKCSideMenuSlot CreatSideMenuSlot(NKCCollectionManager.COLLECTION_STORY_CATEGORY category)
		{
			return this.CreatSideMenuSlot(NKCUtilString.GetCollectionStoryCategory(category));
		}

		// Token: 0x0600904C RID: 36940 RVA: 0x00311CAA File Offset: 0x0030FEAA
		private NKCSideMenuSlot CreatSideMenuSlot(string title)
		{
			NKCSideMenuSlot nkcsideMenuSlot = UnityEngine.Object.Instantiate<NKCSideMenuSlot>(this.m_pfbSideSlot);
			NKCUtil.SetGameobjectActive(nkcsideMenuSlot, true);
			nkcsideMenuSlot.Init(title, this.m_SideMenuToggleGroup, this.m_rtNKM_UI_COLLECTION_STORY_LEFTMENU_contents);
			nkcsideMenuSlot.transform.localScale = Vector3.one;
			return nkcsideMenuSlot;
		}

		// Token: 0x0600904D RID: 36941 RVA: 0x00311CE1 File Offset: 0x0030FEE1
		private NKCSideMenuSlotChild CreateSideMenuSlotchild(string title, string key)
		{
			NKCSideMenuSlotChild nkcsideMenuSlotChild = UnityEngine.Object.Instantiate<NKCSideMenuSlotChild>(this.m_pfbSideSlotChild);
			NKCUtil.SetGameobjectActive(nkcsideMenuSlotChild, true);
			nkcsideMenuSlotChild.Init(title, key, this.m_rtNKM_UI_COLLECTION_STORY_LEFTMENU_contents, new NKCSideMenuSlotChild.OnClicked(this.OnClicked));
			nkcsideMenuSlotChild.transform.localScale = Vector3.one;
			return nkcsideMenuSlotChild;
		}

		// Token: 0x0600904E RID: 36942 RVA: 0x00311D20 File Offset: 0x0030FF20
		private void OnSkinClick(int skinID)
		{
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_SKIN_STORY_REPLAY_CONFIRM, delegate()
			{
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(skinID);
				if (skinTemplet != null && !string.IsNullOrEmpty(skinTemplet.m_CutscenePurchase))
				{
					this.PlayCutScene(skinTemplet.m_CutscenePurchase, 0);
				}
			}, null, false);
		}

		// Token: 0x0600904F RID: 36943 RVA: 0x00311D60 File Offset: 0x0030FF60
		public void OnClicked(string key)
		{
			int num = int.Parse(key);
			if (!this.m_dicStory.ContainsKey(num))
			{
				Debug.Log("식별할 수 없는 에피소드 id : " + key);
				return;
			}
			if (this.m_iSelectEpsodeIdx == num)
			{
				return;
			}
			this.m_iSelectEpsodeIdx = num;
			foreach (NKCSideMenuSlot nkcsideMenuSlot in this.m_lstSideMenuSlot)
			{
				nkcsideMenuSlot.NotifySelectID(this.m_iSelectEpsodeIdx.ToString());
			}
			this.UpdateEpisodeSlot();
		}

		// Token: 0x06009050 RID: 36944 RVA: 0x00311DF8 File Offset: 0x0030FFF8
		public static int UpdateEpisodeCategory(ref int collectedCount, ref int totalCollectedCount, NKCCollectionManager.COLLECTION_STORY_CATEGORY type, Dictionary<NKCCollectionManager.COLLECTION_STORY_CATEGORY, List<NKCUICollectionStory.EpData>> dicCollectionEpisodeData, bool getStoryData, ref Dictionary<int, NKCUICollectionStory.StorySlotData> dicStory)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return 0;
			}
			if (dicCollectionEpisodeData == null)
			{
				return 0;
			}
			if (!dicCollectionEpisodeData.ContainsKey(type))
			{
				return 0;
			}
			int num = 0;
			List<NKCUICollectionStory.EpData> list = dicCollectionEpisodeData[type];
			bool flag = false;
			if (nkmuserData.IsSuperUser())
			{
				flag = true;
			}
			if (list != null)
			{
				using (List<NKCUICollectionStory.EpData>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						NKCUICollectionStory.EpData epData = enumerator.Current;
						NKCUICollectionStory.StorySlotData storySlotData = null;
						if (getStoryData)
						{
							storySlotData = new NKCUICollectionStory.StorySlotData(NKCStringTable.GetString(epData.m_EpisodeTitle, false), NKCStringTable.GetString(epData.m_EpisodeName, false), type);
						}
						List<NKCUICollectionStory.EpSlotData> lstEpisodeStages = epData.m_lstEpisodeStages;
						for (int i = 0; i < lstEpisodeStages.Count; i++)
						{
							bool flag2 = false;
							int count = lstEpisodeStages[i].m_UnlockReqList.Count;
							for (int j = 0; j < count; j++)
							{
								bool flag3 = flag2;
								NKMUserData cNKMUserData = nkmuserData;
								UnlockInfo unlockInfo = lstEpisodeStages[i].m_UnlockReqList[j];
								flag2 = (flag3 | NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, unlockInfo, false));
							}
							if (flag)
							{
								flag2 = true;
							}
							bool flag4 = false;
							bool flag5 = false;
							for (int k = 0; k < count; k++)
							{
								if (lstEpisodeStages[i].m_UnlockReqList[k].eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_STAGE)
								{
									NKMStageTempletV2 nkmstageTempletV = NKMStageTempletV2.Find(lstEpisodeStages[i].m_UnlockReqList[k].reqValue);
									if (nkmstageTempletV != null)
									{
										flag4 |= (nkmstageTempletV.GetStageBeforeCutscen() != null);
										flag5 |= (nkmstageTempletV.GetStageAfterCutscen() != null);
									}
								}
								else if (lstEpisodeStages[i].m_UnlockReqList[k].eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DUNGEON)
								{
									NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(lstEpisodeStages[i].m_UnlockReqList[k].reqValue);
									if (dungeonTempletBase != null)
									{
										flag4 |= !string.IsNullOrEmpty(dungeonTempletBase.m_CutScenStrIDBefore);
										flag5 |= !string.IsNullOrEmpty(dungeonTempletBase.m_CutScenStrIDAfter);
									}
								}
								else if (lstEpisodeStages[i].m_UnlockReqList[k].eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_PHASE)
								{
									NKMPhaseTemplet nkmphaseTemplet = NKMPhaseTemplet.Find(lstEpisodeStages[i].m_UnlockReqList[k].reqValue);
									if (nkmphaseTemplet != null)
									{
										flag4 |= !string.IsNullOrEmpty(nkmphaseTemplet.m_CutScenStrIDBefore);
										flag5 |= !string.IsNullOrEmpty(nkmphaseTemplet.m_CutScenStrIDAfter);
									}
								}
								else if (lstEpisodeStages[i].m_UnlockReqList[k].eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_WARFARE)
								{
									NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(lstEpisodeStages[i].m_UnlockReqList[k].reqValue);
									if (nkmwarfareTemplet != null)
									{
										flag4 |= !string.IsNullOrEmpty(nkmwarfareTemplet.m_CutScenStrIDBefore);
										flag5 |= !string.IsNullOrEmpty(nkmwarfareTemplet.m_CutScenStrIDAfter);
									}
								}
								else if (lstEpisodeStages[i].m_UnlockReqList[k].eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_DIVE_HISTORY_CLEARED)
								{
									NKMDiveTemplet nkmdiveTemplet = NKMDiveTemplet.Find(lstEpisodeStages[i].m_UnlockReqList[k].reqValue);
									flag4 |= (nkmdiveTemplet != null);
								}
								else if (lstEpisodeStages[i].m_UnlockReqList[k].eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_TRIM)
								{
									flag4 |= !string.IsNullOrEmpty(lstEpisodeStages[i].m_CutSceneStrIDBefore);
									flag5 |= !string.IsNullOrEmpty(lstEpisodeStages[i].m_CutSceneStrIDAfter);
								}
								else
								{
									Debug.LogError(string.Format("NKCUICollectionStory::UpdateEpisodeCategory - Can not define reqType - {0}", lstEpisodeStages[i].m_UnlockReqList[k].eReqType));
								}
							}
							if (flag2)
							{
								if (num == 0)
								{
									num = epData.m_EpisodeID;
								}
								if (flag4)
								{
									collectedCount++;
								}
								if (flag5)
								{
									collectedCount++;
								}
								if (getStoryData)
								{
									NKCUICollectionStory.StoryData item = new NKCUICollectionStory.StoryData(lstEpisodeStages[i].m_ActID, lstEpisodeStages[i].m_MissionIndex, lstEpisodeStages[i].m_UnlockReqList[0], flag2, lstEpisodeStages[i].m_CutSceneStrIDBefore, lstEpisodeStages[i].m_CutSceneStrIDAfter);
									storySlotData.m_iEpisodeClearCnt++;
									storySlotData.m_lstStoryData.Add(item);
								}
							}
							if (flag4)
							{
								totalCollectedCount++;
							}
							if (flag5)
							{
								totalCollectedCount++;
							}
						}
						if (getStoryData)
						{
							dicStory.Add(epData.m_EpisodeID, storySlotData);
						}
					}
					return num;
				}
			}
			Debug.LogError("Can not Found Story Data - " + type.ToString());
			return num;
		}

		// Token: 0x06009051 RID: 36945 RVA: 0x00312314 File Offset: 0x00310514
		private void UpdateEpisodeSlot()
		{
			if (!this.m_dicStory.ContainsKey(this.m_iSelectEpsodeIdx) || this.m_dicStory[this.m_iSelectEpsodeIdx].m_iEpisodeClearCnt <= 0)
			{
				Debug.LogWarning("[ERROR] Collection story data is wrong : " + this.m_iSelectEpsodeIdx.ToString());
				return;
			}
			this.OnActiveSkinStory(false);
			this.ClearExceptionSlot();
			this.ClearEpisodeSlot();
			List<RectTransform> uislot = this.GetUISlot(this.m_dicStory[this.m_iSelectEpsodeIdx].m_iEpisodeClearCnt);
			HashSet<int> hashSet = new HashSet<int>();
			foreach (NKCUICollectionStory.StoryData storyData in this.m_dicStory[this.m_iSelectEpsodeIdx].m_lstStoryData)
			{
				hashSet.Add(storyData.m_ActID);
			}
			List<RectTransform> titleUISlot = this.GetTitleUISlot(hashSet.Count);
			this.m_StoryContent.SetData(this.m_dicStory[this.m_iSelectEpsodeIdx], uislot, titleUISlot);
		}

		// Token: 0x06009052 RID: 36946 RVA: 0x00312428 File Offset: 0x00310628
		private void ClearEpisodeSlot()
		{
			List<RectTransform> rentalList = this.m_StoryContent.GetRentalList();
			for (int i = 0; i < rentalList.Count; i++)
			{
				rentalList[i].SetParent(this.m_rtStorySlotPool);
				NKCUtil.SetGameobjectActive(rentalList[i].gameObject, false);
				this.m_stkStorySlotPool.Push(rentalList[i]);
			}
			List<RectTransform> subRentalList = this.m_StoryContent.GetSubRentalList();
			for (int j = 0; j < subRentalList.Count; j++)
			{
				subRentalList[j].SetParent(this.m_rtSubTitlePool);
				NKCUtil.SetGameobjectActive(subRentalList[j].gameObject, false);
				this.m_stkStorySubTitlePool.Push(subRentalList[j]);
			}
			this.m_StoryContent.ClearRentalList();
		}

		// Token: 0x170016D5 RID: 5845
		// (get) Token: 0x06009053 RID: 36947 RVA: 0x003124E8 File Offset: 0x003106E8
		private NKCUICollectionStoryContent m_StoryContent
		{
			get
			{
				if (this.StroyContent == null)
				{
					NKCUICollectionStoryContent nkcuicollectionStoryContent = UnityEngine.Object.Instantiate<NKCUICollectionStoryContent>(this.m_pfStoryContent);
					nkcuicollectionStoryContent.Init();
					nkcuicollectionStoryContent.transform.localPosition = Vector3.zero;
					nkcuicollectionStoryContent.transform.localScale = Vector3.one;
					nkcuicollectionStoryContent.GetComponent<RectTransform>().SetParent(this.m_rtNKM_UI_COLLECTION_STORY_LIST_Contents, false);
					this.StroyContent = nkcuicollectionStoryContent;
				}
				return this.StroyContent;
			}
		}

		// Token: 0x06009054 RID: 36948 RVA: 0x00312554 File Offset: 0x00310754
		public void PlayCutScene(string name, int stageID)
		{
			NKCUICutScenPlayer.Instance.LoadAndPlay(name, stageID, new NKCUICutScenPlayer.CutScenCallBack(this.EndCutScene), true);
			this.dOnStoryCutscen(true);
		}

		// Token: 0x06009055 RID: 36949 RVA: 0x0031257B File Offset: 0x0031077B
		public void EndCutScene()
		{
			NKCSoundManager.StopAllSound();
			NKCSoundManager.PlayScenMusic(NKCScenManager.GetScenManager().GetNowScenID(), false);
			this.dOnStoryCutscen(false);
		}

		// Token: 0x06009056 RID: 36950 RVA: 0x003125A0 File Offset: 0x003107A0
		private List<RectTransform> GetUISlot(int iCnt)
		{
			List<RectTransform> list = new List<RectTransform>();
			for (int i = 0; i < iCnt; i++)
			{
				if (this.m_stkStorySlotPool.Count > 0)
				{
					RectTransform item = this.m_stkStorySlotPool.Pop();
					list.Add(item);
				}
				else
				{
					NKCUICollectionStorySlot nkcuicollectionStorySlot = UnityEngine.Object.Instantiate<NKCUICollectionStorySlot>(this.m_pfStorySlot);
					nkcuicollectionStorySlot.Init(new NKCUICollectionStory.OnPlayCutScene(this.PlayCutScene));
					nkcuicollectionStorySlot.transform.localPosition = Vector3.zero;
					nkcuicollectionStorySlot.transform.localScale = Vector3.one;
					RectTransform component = nkcuicollectionStorySlot.GetComponent<RectTransform>();
					list.Add(component);
				}
			}
			return list;
		}

		// Token: 0x06009057 RID: 36951 RVA: 0x00312630 File Offset: 0x00310830
		private List<RectTransform> GetTitleUISlot(int iCnt)
		{
			List<RectTransform> list = new List<RectTransform>();
			for (int i = 0; i < iCnt; i++)
			{
				if (this.m_stkStorySubTitlePool.Count > 0)
				{
					RectTransform item = this.m_stkStorySubTitlePool.Pop();
					list.Add(item);
				}
				else
				{
					NKCUICollectionStorySubTitle nkcuicollectionStorySubTitle = UnityEngine.Object.Instantiate<NKCUICollectionStorySubTitle>(this.m_pfSubTitle);
					nkcuicollectionStorySubTitle.Init();
					nkcuicollectionStorySubTitle.transform.localPosition = Vector3.zero;
					nkcuicollectionStorySubTitle.transform.localScale = Vector3.one;
					RectTransform component = nkcuicollectionStorySubTitle.GetComponent<RectTransform>();
					list.Add(component);
				}
			}
			return list;
		}

		// Token: 0x06009058 RID: 36952 RVA: 0x003126B1 File Offset: 0x003108B1
		public void PlayCutscen(bool bPlay)
		{
			if (this.dOnStoryCutscen != null)
			{
				this.dOnStoryCutscen(bPlay);
			}
		}

		// Token: 0x06009059 RID: 36953 RVA: 0x003126C7 File Offset: 0x003108C7
		private void SyncCollectionData()
		{
			if (this.dOnSyncCollectingData != null)
			{
				this.dOnSyncCollectingData(NKCUICollection.CollectionType.CT_STORY, this.m_iCollected, this.m_iTotalCollected);
			}
		}

		// Token: 0x0600905A RID: 36954 RVA: 0x003126EC File Offset: 0x003108EC
		private void CreateExceptionSideMenu()
		{
			if (NKCCollectionManager.GetSkinStoryData().Count <= 0)
			{
				return;
			}
			NKCSideMenuSlot nkcsideMenuSlot = this.CreatSideMenuSlot(NKCUtilString.GET_STRING_COLLECTION_SKIN_SLOT_NAME);
			if (nkcsideMenuSlot == null)
			{
				return;
			}
			nkcsideMenuSlot.SetCallBackFunction(new UnityAction(this.UpdateSkinCutscenSlot));
			this.m_lstSideMenuSlot.Add(nkcsideMenuSlot);
		}

		// Token: 0x0600905B RID: 36955 RVA: 0x0031273B File Offset: 0x0031093B
		private void OnActiveSkinStory(bool bActive)
		{
			NKCUtil.SetGameobjectActive(this.m_rtNKM_UI_COLLECTION_STORY_LIST_Contents.gameObject, !bActive);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_SKIN_STORY_LIST, bActive);
		}

		// Token: 0x0600905C RID: 36956 RVA: 0x00312760 File Offset: 0x00310960
		private void UpdateSkinCutscenSlot()
		{
			this.m_iSelectEpsodeIdx = 0;
			foreach (NKCSideMenuSlot nkcsideMenuSlot in this.m_lstSideMenuSlot)
			{
				nkcsideMenuSlot.NotifySelectID(this.m_iSelectEpsodeIdx.ToString());
			}
			this.OnActiveSkinStory(true);
			this.m_lstSkinStoryList = NKCCollectionManager.GetSkinStoryData();
			if (this.m_lstSkinStoryList != null && this.m_lstSkinStoryList.Count > 0)
			{
				this.m_LoopScrollRect.TotalCount = this.m_lstSkinStoryList.Count;
				this.m_LoopScrollRect.RefreshCells(false);
			}
		}

		// Token: 0x0600905D RID: 36957 RVA: 0x0031280C File Offset: 0x00310A0C
		private void ClearExceptionSlot()
		{
			foreach (NKCUISkinSlot nkcuiskinSlot in this.m_lstSkinSlot)
			{
				nkcuiskinSlot.GetComponent<RectTransform>().SetParent(this.m_rtSkinSlotPool);
			}
		}

		// Token: 0x0600905E RID: 36958 RVA: 0x00312868 File Offset: 0x00310A68
		private RectTransform GetSlot(int index)
		{
			if (this.m_stkSkinSlotPool.Count > 0)
			{
				return this.m_stkSkinSlotPool.Pop().GetComponent<RectTransform>();
			}
			NKCUISkinSlot nkcuiskinSlot = UnityEngine.Object.Instantiate<NKCUISkinSlot>(this.m_pbfNKM_UI_COLLECTION_UNIT_INFO_UNIT_SKIN_STORY_SLOT);
			NKCUtil.SetGameobjectActive(nkcuiskinSlot, true);
			nkcuiskinSlot.transform.localScale = Vector3.one;
			nkcuiskinSlot.transform.SetParent(this.m_rtNKM_UI_COLLECTION_SKIN_STORY_LIST_Contents);
			return nkcuiskinSlot.GetComponent<RectTransform>();
		}

		// Token: 0x0600905F RID: 36959 RVA: 0x003128CC File Offset: 0x00310ACC
		private void ReturnSlot(Transform go)
		{
			go.SetParent(this.m_rtSkinSlotPool);
			NKCUISkinSlot component = go.GetComponent<NKCUISkinSlot>();
			if (component != null)
			{
				this.m_stkSkinSlotPool.Push(component);
			}
		}

		// Token: 0x06009060 RID: 36960 RVA: 0x00312904 File Offset: 0x00310B04
		private void ProvideSlotData(Transform tr, int idx)
		{
			if (this.m_lstSkinStoryList == null)
			{
				return;
			}
			if (this.m_lstSkinStoryList.Count < idx)
			{
				return;
			}
			storyUnlockData storyUnlockData = this.m_lstSkinStoryList[idx];
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(storyUnlockData.m_UnlockReqList[0].reqValue);
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (skinTemplet != null)
			{
				bool flag = false;
				if (nkmuserData != null)
				{
					flag = nkmuserData.m_InventoryData.HasItemSkin(storyUnlockData.m_UnlockReqList[0].reqValue);
				}
				NKCUISkinSlot component = tr.GetComponent<NKCUISkinSlot>();
				if (component != null)
				{
					if (flag)
					{
						component.Init(new NKCUISkinSlot.OnClick(this.OnSkinClick));
					}
					component.SetData(skinTemplet, flag, false, !flag);
					this.m_lstSkinSlot.Add(component);
				}
			}
		}

		// Token: 0x04007D5A RID: 32090
		[Header("탭 버튼")]
		public NKCUIComStateButton m_NKM_UI_COLLECTION_STORY_TOP_MENU_MAINSTREAM;

		// Token: 0x04007D5B RID: 32091
		public NKCUIComStateButton m_NKM_UI_COLLECTION_STORY_TOP_MENU_SIDESTORY;

		// Token: 0x04007D5C RID: 32092
		[Header("왼쪽(메인스트림,외전) 메뉴")]
		public RectTransform m_rtNKM_UI_COLLECTION_STORY_LEFTMENU_contents;

		// Token: 0x04007D5D RID: 32093
		public NKCUIComToggleGroup m_SideMenuToggleGroup;

		// Token: 0x04007D5E RID: 32094
		[Header("슬롯")]
		public RectTransform m_rtNKM_UI_COLLECTION_STORY_LIST_Contents;

		// Token: 0x04007D5F RID: 32095
		public NKCSideMenuSlot m_pfbSideSlot;

		// Token: 0x04007D60 RID: 32096
		public NKCSideMenuSlotChild m_pfbSideSlotChild;

		// Token: 0x04007D61 RID: 32097
		public NKCUISkinSlot m_pbfNKM_UI_COLLECTION_UNIT_INFO_UNIT_SKIN_STORY_SLOT;

		// Token: 0x04007D62 RID: 32098
		public LoopScrollRect m_LoopScrollRect;

		// Token: 0x04007D63 RID: 32099
		public RectTransform m_rtSkinSlotPool;

		// Token: 0x04007D64 RID: 32100
		[Space]
		public GameObject m_NKM_UI_COLLECTION_SKIN_STORY_LIST;

		// Token: 0x04007D65 RID: 32101
		public RectTransform m_rtNKM_UI_COLLECTION_SKIN_STORY_LIST_Contents;

		// Token: 0x04007D66 RID: 32102
		public ScrollRect m_srStory;

		// Token: 0x04007D67 RID: 32103
		public NKCUICollectionStorySlot m_pfStorySlot;

		// Token: 0x04007D68 RID: 32104
		public RectTransform m_rtStorySlotPool;

		// Token: 0x04007D69 RID: 32105
		private Stack<RectTransform> m_stkStorySlotPool = new Stack<RectTransform>();

		// Token: 0x04007D6A RID: 32106
		public NKCUICollectionStorySubTitle m_pfSubTitle;

		// Token: 0x04007D6B RID: 32107
		public RectTransform m_rtSubTitlePool;

		// Token: 0x04007D6C RID: 32108
		private Stack<RectTransform> m_stkStorySubTitlePool = new Stack<RectTransform>();

		// Token: 0x04007D6D RID: 32109
		public NKCUICollectionStoryContent m_pfStoryContent;

		// Token: 0x04007D6E RID: 32110
		public RectTransform m_rtStoryContentPool;

		// Token: 0x04007D6F RID: 32111
		private Stack<RectTransform> m_stkStoryContentPool = new Stack<RectTransform>();

		// Token: 0x04007D70 RID: 32112
		private NKCUICollection.OnSyncCollectingData dOnSyncCollectingData;

		// Token: 0x04007D71 RID: 32113
		private NKCUICollection.OnStoryCutscen dOnStoryCutscen;

		// Token: 0x04007D72 RID: 32114
		private Dictionary<NKCCollectionManager.COLLECTION_STORY_CATEGORY, List<NKCUICollectionStory.EpData>> m_dicCollectionEpisodeData = new Dictionary<NKCCollectionManager.COLLECTION_STORY_CATEGORY, List<NKCUICollectionStory.EpData>>();

		// Token: 0x04007D73 RID: 32115
		private Dictionary<EPISODE_CATEGORY, NKCUICollectionStory.EpData> m_dicEpData = new Dictionary<EPISODE_CATEGORY, NKCUICollectionStory.EpData>();

		// Token: 0x04007D74 RID: 32116
		private Dictionary<int, NKCUICollectionStory.StorySlotData> m_dicStory = new Dictionary<int, NKCUICollectionStory.StorySlotData>();

		// Token: 0x04007D75 RID: 32117
		public List<NKCUICollectionStory.StorySlotData> m_lstStorySlotData = new List<NKCUICollectionStory.StorySlotData>();

		// Token: 0x04007D76 RID: 32118
		private int m_iReservePageIdx;

		// Token: 0x04007D77 RID: 32119
		private int m_iSelectEpsodeIdx;

		// Token: 0x04007D78 RID: 32120
		private List<NKCSideMenuSlot> m_lstSideMenuSlot = new List<NKCSideMenuSlot>();

		// Token: 0x04007D79 RID: 32121
		private NKCUICollectionStoryContent StroyContent;

		// Token: 0x04007D7A RID: 32122
		private List<NKCUICollectionStoryContent> m_lstStorySlot = new List<NKCUICollectionStoryContent>();

		// Token: 0x04007D7B RID: 32123
		private int m_iCollected;

		// Token: 0x04007D7C RID: 32124
		private int m_iTotalCollected;

		// Token: 0x04007D7D RID: 32125
		private List<NKCUISkinSlot> m_lstSkinSlot = new List<NKCUISkinSlot>();

		// Token: 0x04007D7E RID: 32126
		private List<storyUnlockData> m_lstSkinStoryList = new List<storyUnlockData>();

		// Token: 0x04007D7F RID: 32127
		private Stack<NKCUISkinSlot> m_stkSkinSlotPool = new Stack<NKCUISkinSlot>();

		// Token: 0x020019E7 RID: 6631
		public class EpData
		{
			// Token: 0x0600BA7F RID: 47743 RVA: 0x0036DFD4 File Offset: 0x0036C1D4
			public EpData(EPISODE_CATEGORY category, int epID, string epTitle, string epName, int sortIndex)
			{
				this.m_Category = NKCCollectionManager.GetCollectionStoryCategory(category);
				this.m_EpisodeID = epID;
				this.m_EpisodeTitle = epTitle;
				this.m_EpisodeName = epName;
				this.m_SortIndex = sortIndex;
			}

			// Token: 0x0600BA80 RID: 47744 RVA: 0x0036E011 File Offset: 0x0036C211
			public EpData(NKMDiveTemplet templet)
			{
				this.m_Category = NKCCollectionManager.COLLECTION_STORY_CATEGORY.WORLDMAP;
				this.m_EpisodeID = -1;
				this.m_EpisodeTitle = "SI_DP_DIVE";
				this.m_EpisodeName = "SI_DP_DIVE";
				this.m_SortIndex = 0;
			}

			// Token: 0x0600BA81 RID: 47745 RVA: 0x0036E04F File Offset: 0x0036C24F
			public EpData(string EpisodeTitleKey, string EpisodeNameKey)
			{
				this.m_Category = NKCCollectionManager.COLLECTION_STORY_CATEGORY.ETC;
				this.m_EpisodeID = -2;
				this.m_EpisodeTitle = EpisodeTitleKey;
				this.m_EpisodeName = EpisodeNameKey;
				this.m_SortIndex = 0;
			}

			// Token: 0x0400AD35 RID: 44341
			public NKCCollectionManager.COLLECTION_STORY_CATEGORY m_Category;

			// Token: 0x0400AD36 RID: 44342
			public int m_EpisodeID;

			// Token: 0x0400AD37 RID: 44343
			public int m_SortIndex;

			// Token: 0x0400AD38 RID: 44344
			public string m_EpisodeTitle;

			// Token: 0x0400AD39 RID: 44345
			public string m_EpisodeName;

			// Token: 0x0400AD3A RID: 44346
			public List<NKCUICollectionStory.EpSlotData> m_lstEpisodeStages = new List<NKCUICollectionStory.EpSlotData>();
		}

		// Token: 0x020019E8 RID: 6632
		public struct EpSlotData
		{
			// Token: 0x0600BA82 RID: 47746 RVA: 0x0036E086 File Offset: 0x0036C286
			public EpSlotData(List<UnlockInfo> unlockReqList, int actID, int missionID, string idBefore, string idAfter)
			{
				this.m_UnlockReqList = unlockReqList;
				this.m_MissionIndex = missionID;
				this.m_ActID = actID;
				this.m_CutSceneStrIDBefore = idBefore;
				this.m_CutSceneStrIDAfter = idAfter;
			}

			// Token: 0x170019E8 RID: 6632
			// (get) Token: 0x0600BA83 RID: 47747 RVA: 0x0036E0AD File Offset: 0x0036C2AD
			// (set) Token: 0x0600BA84 RID: 47748 RVA: 0x0036E0B5 File Offset: 0x0036C2B5
			public List<UnlockInfo> m_UnlockReqList { readonly get; private set; }

			// Token: 0x170019E9 RID: 6633
			// (get) Token: 0x0600BA85 RID: 47749 RVA: 0x0036E0BE File Offset: 0x0036C2BE
			// (set) Token: 0x0600BA86 RID: 47750 RVA: 0x0036E0C6 File Offset: 0x0036C2C6
			public int m_MissionIndex { readonly get; private set; }

			// Token: 0x170019EA RID: 6634
			// (get) Token: 0x0600BA87 RID: 47751 RVA: 0x0036E0CF File Offset: 0x0036C2CF
			// (set) Token: 0x0600BA88 RID: 47752 RVA: 0x0036E0D7 File Offset: 0x0036C2D7
			public int m_ActID { readonly get; private set; }

			// Token: 0x170019EB RID: 6635
			// (get) Token: 0x0600BA89 RID: 47753 RVA: 0x0036E0E0 File Offset: 0x0036C2E0
			// (set) Token: 0x0600BA8A RID: 47754 RVA: 0x0036E0E8 File Offset: 0x0036C2E8
			public string m_CutSceneStrIDBefore { readonly get; private set; }

			// Token: 0x170019EC RID: 6636
			// (get) Token: 0x0600BA8B RID: 47755 RVA: 0x0036E0F1 File Offset: 0x0036C2F1
			// (set) Token: 0x0600BA8C RID: 47756 RVA: 0x0036E0F9 File Offset: 0x0036C2F9
			public string m_CutSceneStrIDAfter { readonly get; private set; }
		}

		// Token: 0x020019E9 RID: 6633
		public class StoryData
		{
			// Token: 0x0600BA8D RID: 47757 RVA: 0x0036E102 File Offset: 0x0036C302
			public StoryData(int actID, int MissionIdx, UnlockInfo unlockInfo, bool bClear, string strBeforeCutScene = "", string strAfterCutScene = "")
			{
				this.m_ActID = actID;
				this.m_MissionIdx = MissionIdx;
				this.m_UnlockInfo = unlockInfo;
				this.m_bClear = bClear;
				this.m_strBeforeCutScene = strBeforeCutScene;
				this.m_strAfterCutScene = strAfterCutScene;
			}

			// Token: 0x0400AD40 RID: 44352
			public int m_ActID;

			// Token: 0x0400AD41 RID: 44353
			public int m_MissionIdx;

			// Token: 0x0400AD42 RID: 44354
			public bool m_bClear;

			// Token: 0x0400AD43 RID: 44355
			public UnlockInfo m_UnlockInfo;

			// Token: 0x0400AD44 RID: 44356
			public string m_strBeforeCutScene;

			// Token: 0x0400AD45 RID: 44357
			public string m_strAfterCutScene;
		}

		// Token: 0x020019EA RID: 6634
		public class StorySlotData
		{
			// Token: 0x0600BA8E RID: 47758 RVA: 0x0036E137 File Offset: 0x0036C337
			public StorySlotData(string Title, string Name, NKCCollectionManager.COLLECTION_STORY_CATEGORY type)
			{
				this.m_EpisodeTitle = Title;
				this.m_EpisodeName = Name;
				this.m_eCategory = type;
			}

			// Token: 0x0400AD46 RID: 44358
			public string m_EpisodeTitle;

			// Token: 0x0400AD47 RID: 44359
			public string m_EpisodeName;

			// Token: 0x0400AD48 RID: 44360
			public int m_iEpisodeClearCnt;

			// Token: 0x0400AD49 RID: 44361
			public NKCCollectionManager.COLLECTION_STORY_CATEGORY m_eCategory;

			// Token: 0x0400AD4A RID: 44362
			public List<NKCUICollectionStory.StoryData> m_lstStoryData = new List<NKCUICollectionStory.StoryData>();
		}

		// Token: 0x020019EB RID: 6635
		// (Invoke) Token: 0x0600BA90 RID: 47760
		public delegate void OnPlayCutScene(string name, int stageID);
	}
}
