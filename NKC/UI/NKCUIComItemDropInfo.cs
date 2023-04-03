using System;
using System.Collections.Generic;
using System.Linq;
using NKC.Templet;
using NKC.UI.Trim;
using NKM;
using NKM.Shop;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000939 RID: 2361
	public class NKCUIComItemDropInfo : MonoBehaviour
	{
		// Token: 0x06005E5C RID: 24156 RVA: 0x001D34A8 File Offset: 0x001D16A8
		public void Init()
		{
			base.gameObject.SetActive(true);
			if (this.m_LoopScrollRect != null)
			{
				this.m_LoopScrollRect.dOnGetObject += this.GetSlot;
				this.m_LoopScrollRect.dOnReturnObject += this.ReturnMissionSlot;
				this.m_LoopScrollRect.dOnProvideData += this.ProvideMissionData;
				this.m_LoopScrollRect.ContentConstraintCount = 1;
				this.m_LoopScrollRect.PrepareCells(0);
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x06005E5D RID: 24157 RVA: 0x001D3538 File Offset: 0x001D1738
		public bool SetData(NKCUISlot.SlotData data, bool initScrollPosition = true)
		{
			if (data == null || data.eType != NKCUISlot.eSlotMode.ItemMisc)
			{
				base.gameObject.SetActive(false);
				return false;
			}
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(data.ID);
			NKCItemDropInfoTemplet nkcitemDropInfoTemplet = null;
			if (itemMiscTempletByID != null && itemMiscTempletByID.m_ItemDropInfo)
			{
				nkcitemDropInfoTemplet = NKCItemDropInfoTemplet.Find(data.ID);
			}
			this.m_ItemDropInfoList.Clear();
			if (nkcitemDropInfoTemplet != null)
			{
				HashSet<string> hashSet = new HashSet<string>();
				Dictionary<EPISODE_CATEGORY, List<ItemDropInfo>> dictionary = new Dictionary<EPISODE_CATEGORY, List<ItemDropInfo>>();
				Dictionary<DropContent, List<ItemDropInfo>> dictionary2 = new Dictionary<DropContent, List<ItemDropInfo>>();
				int count = nkcitemDropInfoTemplet.ItemDropInfoList.Count;
				for (int i = 0; i < count; i++)
				{
					ItemDropInfo itemDropInfo = nkcitemDropInfoTemplet.ItemDropInfoList[i];
					if (this.FilterOpenedDropInfo(itemDropInfo, ref hashSet))
					{
						if (itemDropInfo.ContentType == DropContent.Stage)
						{
							NKMStageTempletV2 nkmstageTempletV = NKMStageTempletV2.Find(itemDropInfo.ContentID);
							if (nkmstageTempletV.EpisodeTemplet != null)
							{
								if (!dictionary.ContainsKey(nkmstageTempletV.EpisodeCategory))
								{
									dictionary.Add(nkmstageTempletV.EpisodeCategory, new List<ItemDropInfo>());
								}
								dictionary[nkmstageTempletV.EpisodeCategory].Add(itemDropInfo);
							}
						}
						else
						{
							if (!dictionary2.ContainsKey(itemDropInfo.ContentType))
							{
								dictionary2.Add(itemDropInfo.ContentType, new List<ItemDropInfo>());
							}
							dictionary2[itemDropInfo.ContentType].Add(itemDropInfo);
						}
					}
				}
				foreach (KeyValuePair<EPISODE_CATEGORY, List<ItemDropInfo>> keyValuePair in dictionary)
				{
					int stageDropInfoCountLimit = this.GetStageDropInfoCountLimit(keyValuePair.Key);
					if (stageDropInfoCountLimit > 0)
					{
						if (keyValuePair.Value.Count < stageDropInfoCountLimit)
						{
							this.m_ItemDropInfoList.AddRange(keyValuePair.Value);
						}
						else
						{
							keyValuePair.Value.Sort(delegate(ItemDropInfo e1, ItemDropInfo e2)
							{
								if (e1.ContentID < e2.ContentID)
								{
									return 1;
								}
								if (e1.ContentID > e2.ContentID)
								{
									return -1;
								}
								return 0;
							});
							keyValuePair.Value[0].Summary = true;
							this.m_ItemDropInfoList.Add(keyValuePair.Value[0]);
						}
					}
				}
				foreach (KeyValuePair<DropContent, List<ItemDropInfo>> keyValuePair2 in dictionary2)
				{
					if (this.GetDropInfoCountLimit(keyValuePair2.Key) > 0)
					{
						if (keyValuePair2.Value.Count < this.GetDropInfoCountLimit(keyValuePair2.Key))
						{
							this.m_ItemDropInfoList.AddRange(keyValuePair2.Value);
						}
						else
						{
							keyValuePair2.Value.Sort(delegate(ItemDropInfo e1, ItemDropInfo e2)
							{
								if (e1.ContentID < e2.ContentID)
								{
									return -1;
								}
								if (e1.ContentID > e2.ContentID)
								{
									return 1;
								}
								return 0;
							});
							keyValuePair2.Value[0].Summary = true;
							this.m_ItemDropInfoList.Add(keyValuePair2.Value[0]);
						}
					}
				}
			}
			bool flag = this.m_ItemDropInfoList.Count > 0;
			base.gameObject.SetActive(flag);
			if (!flag)
			{
				return false;
			}
			this.m_ItemID = data.ID;
			if (this.m_LoopScrollRect != null)
			{
				this.m_LoopScrollRect.TotalCount = this.m_ItemDropInfoList.Count;
				if (initScrollPosition)
				{
					this.m_LoopScrollRect.SetIndexPosition(0);
				}
				else
				{
					this.m_LoopScrollRect.RefreshCells(false);
				}
				if (!this.m_LoopScrollRect.isActiveAndEnabled)
				{
					this.m_LoopScrollRect.RefreshCells(true);
				}
			}
			return true;
		}

		// Token: 0x06005E5E RID: 24158 RVA: 0x001D38AC File Offset: 0x001D1AAC
		private bool FilterOpenedDropInfo(ItemDropInfo itemDropInfo, ref HashSet<string> worldMapMissionNameSet)
		{
			switch (itemDropInfo.ContentType)
			{
			case DropContent.Stage:
			{
				NKMStageTempletV2 nkmstageTempletV = NKMStageTempletV2.Find(itemDropInfo.ContentID);
				if (nkmstageTempletV != null)
				{
					bool flag = NKMEpisodeMgr.CheckEpisodeMission(NKCScenManager.CurrentUserData(), nkmstageTempletV);
					bool flag2 = nkmstageTempletV.EpisodeTemplet == null || nkmstageTempletV.EpisodeTemplet.IsOpen;
					if (nkmstageTempletV.EpisodeTemplet != null)
					{
						ContentsType contentsType = NKCContentManager.GetContentsType(nkmstageTempletV.EpisodeTemplet.m_EPCategory);
						flag2 &= NKCContentManager.IsContentsUnlocked(contentsType, 0, 0);
					}
					bool flag3 = nkmstageTempletV.IsOpenedDayOfWeek();
					if (flag2 && flag && flag3)
					{
						return true;
					}
				}
				break;
			}
			case DropContent.Shop:
			case DropContent.SubStreamShop:
			{
				ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(itemDropInfo.ContentID);
				if (shopItemTemplet != null && (shopItemTemplet.TabTemplet == null || !shopItemTemplet.TabTemplet.HasDateLimit) && (shopItemTemplet.TabTemplet == null || shopItemTemplet.m_ChainIndex <= NKCShopManager.GetCurrentTargetChainIndex(shopItemTemplet.TabTemplet)))
				{
					bool flag4 = true;
					if (itemDropInfo.ContentType == DropContent.SubStreamShop)
					{
						flag4 = false;
						string shopShortCut = string.Format("{0}@{1}", shopItemTemplet.m_TabID, shopItemTemplet.m_TabSubIndex);
						NKMStageTempletV2 nkmstageTempletV2 = NKMStageTempletV2.Values.First((NKMStageTempletV2 e) => e.m_ShopShortcut == shopShortCut);
						if (nkmstageTempletV2 != null && nkmstageTempletV2.EpisodeTemplet != null)
						{
							NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(nkmstageTempletV2.EpisodeTemplet.m_EpisodeID, EPISODE_DIFFICULTY.NORMAL);
							bool flag5;
							if (nkmepisodeTempletV != null)
							{
								NKMUserData cNKMUserData = NKCScenManager.CurrentUserData();
								UnlockInfo unlockInfo = nkmepisodeTempletV.GetUnlockInfo();
								flag5 = NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, unlockInfo, false);
							}
							else
							{
								flag5 = false;
							}
							flag4 = flag5;
						}
					}
					if (NKCShopManager.CanExhibitItem(shopItemTemplet, false, false) && flag4)
					{
						return true;
					}
				}
				break;
			}
			case DropContent.WorldMapMission:
			{
				NKMWorldMapMissionTemplet nkmworldMapMissionTemplet = NKMTempletContainer<NKMWorldMapMissionTemplet>.Find(itemDropInfo.ContentID);
				if (nkmworldMapMissionTemplet != null && !string.IsNullOrEmpty(nkmworldMapMissionTemplet.m_MissionName) && !worldMapMissionNameSet.Contains(nkmworldMapMissionTemplet.m_MissionName) && NKCContentManager.IsContentsUnlocked(ContentsType.WORLDMAP, 0, 0) && nkmworldMapMissionTemplet.m_bEnableMission && nkmworldMapMissionTemplet.m_eMissionType != NKMWorldMapMissionTemplet.WorldMapMissionType.WMT_INVALID)
				{
					worldMapMissionNameSet.Add(nkmworldMapMissionTemplet.m_MissionName);
					return true;
				}
				break;
			}
			case DropContent.Raid:
			{
				object obj = NKMTempletContainer<NKMRaidTemplet>.Find(itemDropInfo.ContentID);
				bool flag6 = NKMTutorialManager.IsTutorialCompleted(TutorialStep.RaidEvent, NKCScenManager.CurrentUserData());
				if (obj != null && flag6)
				{
					return true;
				}
				break;
			}
			case DropContent.Shadow:
			{
				NKMShadowPalaceTemplet nkmshadowPalaceTemplet = NKMTempletContainer<NKMShadowPalaceTemplet>.Find(itemDropInfo.ContentID);
				if (nkmshadowPalaceTemplet != null)
				{
					UnlockInfo unlockInfo2 = new UnlockInfo(nkmshadowPalaceTemplet.STAGE_UNLOCK_REQ_TYPE, nkmshadowPalaceTemplet.STAGE_UNLOCK_REQ_VALUE);
					bool flag7 = NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), unlockInfo2, false);
					if (nkmshadowPalaceTemplet != null && flag7)
					{
						return true;
					}
				}
				break;
			}
			case DropContent.Dive:
			{
				NKMDiveTemplet nkmdiveTemplet = NKMTempletContainer<NKMDiveTemplet>.Find(itemDropInfo.ContentID);
				if (nkmdiveTemplet != null)
				{
					NKMUserData cNKMUserData2 = NKCScenManager.CurrentUserData();
					UnlockInfo unlockInfo = new UnlockInfo(nkmdiveTemplet.StageUnlockReqType, nkmdiveTemplet.StageUnlockReqValue);
					bool flag8 = NKMContentUnlockManager.IsContentUnlocked(cNKMUserData2, unlockInfo, false);
					NKMItemMiscTemplet nkmitemMiscTemplet = NKMItemMiscTemplet.Find(itemDropInfo.ItemID);
					bool flag9 = nkmitemMiscTemplet != null && nkmitemMiscTemplet.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_RESOURCE;
					bool flag10 = flag8 || flag9;
					if (!nkmdiveTemplet.IsEventDive && nkmdiveTemplet.EnableByTag && flag10)
					{
						return true;
					}
				}
				break;
			}
			case DropContent.Fierce:
			{
				NKMFiercePointRewardTemplet nkmfiercePointRewardTemplet = NKMTempletContainer<NKMFiercePointRewardTemplet>.Find(itemDropInfo.ContentID);
				NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
				int num = 0;
				bool flag11 = false;
				if (nkcfierceBattleSupportDataMgr != null)
				{
					if (nkcfierceBattleSupportDataMgr.FierceTemplet != null)
					{
						num = nkcfierceBattleSupportDataMgr.FierceTemplet.PointRewardGroupID;
					}
					NKCFierceBattleSupportDataMgr.FIERCE_STATUS status = nkcfierceBattleSupportDataMgr.GetStatus();
					flag11 = (status != NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_UNUSABLE && status != NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_LOCKED);
				}
				if (nkmfiercePointRewardTemplet != null && num == nkmfiercePointRewardTemplet.FiercePointRewardGroupID && flag11)
				{
					return true;
				}
				break;
			}
			case DropContent.RandomMoldBox:
				if (NKMItemMiscTemplet.Find(itemDropInfo.ItemID) != null && NKCContentManager.IsContentsUnlocked(ContentsType.BASE_FACTORY, 0, 0))
				{
					return true;
				}
				break;
			case DropContent.UnitDismiss:
				if (NKMItemMiscTemplet.Find(itemDropInfo.ItemID) != null)
				{
					return true;
				}
				break;
			case DropContent.UnitExtract:
				if (NKMItemMiscTemplet.Find(itemDropInfo.ItemID) != null && NKCContentManager.IsContentsUnlocked(ContentsType.EXTRACT, 0, 0))
				{
					return true;
				}
				break;
			case DropContent.Trim:
				if (NKMItemMiscTemplet.Find(itemDropInfo.ItemID) != null && NKCContentManager.IsContentsUnlocked(ContentsType.DIMENSION_TRIM, 0, 0) && NKCUITrimUtility.OpenTagEnabled)
				{
					return true;
				}
				break;
			}
			return false;
		}

		// Token: 0x06005E5F RID: 24159 RVA: 0x001D3C8C File Offset: 0x001D1E8C
		private int GetDropInfoCountLimit(DropContent dropContent)
		{
			switch (dropContent)
			{
			case DropContent.Shop:
				return NKMCommonConst.DropInfoShopLimit;
			case DropContent.WorldMapMission:
				return NKMCommonConst.DropInfoWorldMapMissionLimit;
			case DropContent.Raid:
				return NKMCommonConst.DropInfoRaidLimit;
			case DropContent.Shadow:
				return NKMCommonConst.DropInfoShadowPalace;
			case DropContent.Dive:
				return NKMCommonConst.DropInfoDiveLimit;
			case DropContent.Fierce:
				return NKMCommonConst.DropInfoFiercePointReward;
			case DropContent.RandomMoldBox:
				return NKMCommonConst.DropInfoRandomMoldBox;
			case DropContent.UnitDismiss:
				return NKMCommonConst.DropInfoUnitDismiss;
			case DropContent.UnitExtract:
				return NKMCommonConst.DropInfoUnitExtract;
			case DropContent.Trim:
				return NKMCommonConst.DropInfoTrimDungeon;
			case DropContent.SubStreamShop:
				return NKMCommonConst.DropInfoSubStreamShop;
			default:
				return 0;
			}
		}

		// Token: 0x06005E60 RID: 24160 RVA: 0x001D3D14 File Offset: 0x001D1F14
		private int GetStageDropInfoCountLimit(EPISODE_CATEGORY episodeCategory)
		{
			switch (episodeCategory)
			{
			case EPISODE_CATEGORY.EC_MAINSTREAM:
				return NKMCommonConst.DropInfoMainStreamLimit;
			case EPISODE_CATEGORY.EC_DAILY:
				return NKMCommonConst.DropInfoDailyLimit;
			case EPISODE_CATEGORY.EC_COUNTERCASE:
				return NKMCommonConst.DropInfoCounterCase;
			case EPISODE_CATEGORY.EC_SIDESTORY:
				return NKMCommonConst.DropInfoSideStoryLimit;
			case EPISODE_CATEGORY.EC_FIELD:
				return NKMCommonConst.DropInfoFieldLimit;
			case EPISODE_CATEGORY.EC_EVENT:
				return NKMCommonConst.DropInfoEventLimit;
			case EPISODE_CATEGORY.EC_SUPPLY:
				return NKMCommonConst.DropInfoSupplyLimit;
			case EPISODE_CATEGORY.EC_CHALLENGE:
				return NKMCommonConst.DropInfoChallengeLimit;
			default:
				return 0;
			}
		}

		// Token: 0x06005E61 RID: 24161 RVA: 0x001D3D7A File Offset: 0x001D1F7A
		private RectTransform GetSlot(int index)
		{
			NKCUIComItemDropInfoSlot newInstance = NKCUIComItemDropInfoSlot.GetNewInstance(null, false);
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x06005E62 RID: 24162 RVA: 0x001D3D90 File Offset: 0x001D1F90
		private void ReturnMissionSlot(Transform tr)
		{
			NKCUIComItemDropInfoSlot component = tr.GetComponent<NKCUIComItemDropInfoSlot>();
			tr.SetParent(null);
			if (component != null)
			{
				component.DestoryInstance();
				return;
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x06005E63 RID: 24163 RVA: 0x001D3DC8 File Offset: 0x001D1FC8
		private void ProvideMissionData(Transform tr, int index)
		{
			NKCUIComItemDropInfoSlot component = tr.GetComponent<NKCUIComItemDropInfoSlot>();
			if (component == null)
			{
				return;
			}
			if (this.m_ItemDropInfoList == null || this.m_ItemDropInfoList.Count <= index)
			{
				return;
			}
			component.SetData(this.m_ItemDropInfoList[index]);
		}

		// Token: 0x06005E64 RID: 24164 RVA: 0x001D3E0F File Offset: 0x001D200F
		private void OnDestroy()
		{
			List<ItemDropInfo> itemDropInfoList = this.m_ItemDropInfoList;
			if (itemDropInfoList != null)
			{
				itemDropInfoList.Clear();
			}
			this.m_ItemDropInfoList = null;
		}

		// Token: 0x04004A87 RID: 19079
		public LoopScrollRect m_LoopScrollRect;

		// Token: 0x04004A88 RID: 19080
		private int m_ItemID;

		// Token: 0x04004A89 RID: 19081
		private List<ItemDropInfo> m_ItemDropInfoList = new List<ItemDropInfo>();
	}
}
