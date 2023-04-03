using System;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.WorldMap;
using Cs.Logging;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKC
{
	// Token: 0x0200066C RID: 1644
	public class NKCDiveManager
	{
		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x060033DF RID: 13279 RVA: 0x001047F4 File Offset: 0x001029F4
		// (set) Token: 0x060033E0 RID: 13280 RVA: 0x001047FB File Offset: 0x001029FB
		public static int BeginIndex { get; private set; }

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x060033E1 RID: 13281 RVA: 0x00104803 File Offset: 0x00102A03
		// (set) Token: 0x060033E2 RID: 13282 RVA: 0x0010480A File Offset: 0x00102A0A
		public static int EndIndex { get; private set; }

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x060033E3 RID: 13283 RVA: 0x00104812 File Offset: 0x00102A12
		// (set) Token: 0x060033E4 RID: 13284 RVA: 0x00104819 File Offset: 0x00102A19
		public static IReadOnlyList<NKMDiveTemplet> SortedTemplates { get; private set; }

		// Token: 0x060033E5 RID: 13285 RVA: 0x00104824 File Offset: 0x00102A24
		public static NKM_ERROR_CODE CanStart(int cityID, int stageID, List<int> deckIndexes, NKMUserData userData, DateTime curTimeUTC, bool bJump)
		{
			NKMDiveTemplet nkmdiveTemplet = NKMDiveTemplet.Find(stageID);
			if (nkmdiveTemplet == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_DIVE_INVALID_STAGE_ID;
			}
			if (!nkmdiveTemplet.IsEventDive && userData.CheckDiveClear(stageID))
			{
				return NKM_ERROR_CODE.NEC_FAIL_DIVE_ALREADY_CLEARED;
			}
			if (userData.m_DiveGameData != null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_DIVE_ALREADY_STARTED;
			}
			if (deckIndexes.Count <= 0)
			{
				return NKM_ERROR_CODE.NEC_FAIL_DIVE_NOT_ENOUGH_SQUAD_COUNT;
			}
			UnlockInfo unlockInfo = new UnlockInfo(nkmdiveTemplet.StageUnlockReqType, nkmdiveTemplet.StageUnlockReqValue);
			if (!NKMContentUnlockManager.IsContentUnlocked(userData, unlockInfo, false))
			{
				return NKM_ERROR_CODE.NEC_FAIL_DIVE_LOCKED_STAGE;
			}
			for (int i = 0; i < deckIndexes.Count; i++)
			{
				NKM_ERROR_CODE nkm_ERROR_CODE = NKMMain.IsValidDeck(userData.m_ArmyData, NKM_DECK_TYPE.NDT_DIVE, (byte)deckIndexes[i]);
				if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
				{
					return nkm_ERROR_CODE;
				}
			}
			NKMWorldMapCityData cityData = userData.m_WorldmapData.GetCityData(cityID);
			if (cityData != null)
			{
				if (cityData.worldMapEventGroup.worldmapEventID == 0)
				{
					return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_INVALID_EVENT_GROUP_ID;
				}
				if (cityData.worldMapEventGroup.eventGroupEndDate < curTimeUTC)
				{
					return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_EXPIRE_EVENT;
				}
				NKMWorldMapEventTemplet nkmworldMapEventTemplet = NKMWorldMapEventTemplet.Find(cityData.worldMapEventGroup.worldmapEventID);
				if (nkmworldMapEventTemplet == null)
				{
					Log.Error(string.Format("Invalid Templet City ID. CityID : {0}", cityData.cityID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCDiveManager.cs", 81);
					return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_INVALID_CITY_ID;
				}
				if (nkmworldMapEventTemplet.eventType != NKM_WORLDMAP_EVENT_TYPE.WET_DIVE || nkmworldMapEventTemplet.stageID != stageID)
				{
					return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_INVALID_EVENT_GROUP_ID;
				}
			}
			if (bJump && !nkmdiveTemplet.IsEventDive)
			{
				return NKM_ERROR_CODE.NEC_FAIL_DIVE_INVALID_STAGE_ID;
			}
			int diveCost = NKCDiveManager.GetDiveCost(nkmdiveTemplet, cityID, bJump);
			if (!userData.CheckPrice(diveCost, nkmdiveTemplet.StageReqItemId))
			{
				return NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_ITEM;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x060033E6 RID: 13286 RVA: 0x00104994 File Offset: 0x00102B94
		public static NKMDiveTemplet GetCurrNormalDiveTemplet(out int selectedIndex)
		{
			selectedIndex = -1;
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return null;
			}
			NKMDiveTemplet nkmdiveTemplet = null;
			List<NKMDiveTemplet> list = new List<NKMDiveTemplet>();
			foreach (NKMDiveTemplet nkmdiveTemplet2 in NKCDiveManager.SortedTemplates)
			{
				if (!nkmdiveTemplet2.IsEventDive)
				{
					list.Add(nkmdiveTemplet2);
				}
			}
			for (int i = 0; i < list.Count; i++)
			{
				NKMDiveTemplet nkmdiveTemplet3 = list[i];
				NKMUserData cNKMUserData = myUserData;
				UnlockInfo unlockInfo = new UnlockInfo(nkmdiveTemplet3.StageUnlockReqType, nkmdiveTemplet3.StageUnlockReqValue);
				bool flag = NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, unlockInfo, false);
				bool flag2 = false;
				if (myUserData.m_DiveClearData != null && myUserData.m_DiveClearData.Contains(nkmdiveTemplet3.StageID))
				{
					flag2 = true;
				}
				if (flag && !flag2)
				{
					selectedIndex = i;
					nkmdiveTemplet = nkmdiveTemplet3;
					break;
				}
			}
			if (nkmdiveTemplet == null && list.Count > 0)
			{
				selectedIndex = list.Count - 1;
				nkmdiveTemplet = list[selectedIndex];
			}
			return nkmdiveTemplet;
		}

		// Token: 0x060033E7 RID: 13287 RVA: 0x00104A94 File Offset: 0x00102C94
		public static bool IsGauntletSectorType(NKM_DIVE_SECTOR_TYPE type)
		{
			return type == NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_GAUNTLET || type == NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_GAUNTLET_HARD;
		}

		// Token: 0x060033E8 RID: 13288 RVA: 0x00104AA2 File Offset: 0x00102CA2
		public static bool IsBossSectorType(NKM_DIVE_SECTOR_TYPE type)
		{
			return type == NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_BOSS || type == NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_BOSS_HARD;
		}

		// Token: 0x060033E9 RID: 13289 RVA: 0x00104AAF File Offset: 0x00102CAF
		public static bool IsPoincareSectorType(NKM_DIVE_SECTOR_TYPE type)
		{
			return type == NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_POINCARE || type == NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_POINCARE_HARD;
		}

		// Token: 0x060033EA RID: 13290 RVA: 0x00104ABC File Offset: 0x00102CBC
		public static bool IsReimannSectorType(NKM_DIVE_SECTOR_TYPE type)
		{
			return type == NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_REIMANN || type == NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_REIMANN_HARD;
		}

		// Token: 0x060033EB RID: 13291 RVA: 0x00104AC9 File Offset: 0x00102CC9
		public static bool IsEuclidSectorType(NKM_DIVE_SECTOR_TYPE type)
		{
			return type == NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_EUCLID || type == NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_EUCLID_HARD;
		}

		// Token: 0x060033EC RID: 13292 RVA: 0x00104AD8 File Offset: 0x00102CD8
		public static bool IsSectorHardType(NKM_DIVE_SECTOR_TYPE type)
		{
			return type == NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_BOSS_HARD || type == NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_GAUNTLET_HARD || type == NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_POINCARE_HARD || type == NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_REIMANN_HARD || type == NKM_DIVE_SECTOR_TYPE.NDST_SECTOR_EUCLID_HARD;
		}

		// Token: 0x060033ED RID: 13293 RVA: 0x00104AF3 File Offset: 0x00102CF3
		public static bool IsItemEventType(NKM_DIVE_EVENT_TYPE type)
		{
			return type == NKM_DIVE_EVENT_TYPE.NDET_ITEM;
		}

		// Token: 0x060033EE RID: 13294 RVA: 0x00104AFC File Offset: 0x00102CFC
		public static bool IsLostContainerEventType(NKM_DIVE_EVENT_TYPE type)
		{
			return type == NKM_DIVE_EVENT_TYPE.NDET_SUPPLY;
		}

		// Token: 0x060033EF RID: 13295 RVA: 0x00104B05 File Offset: 0x00102D05
		public static bool IsRandomEventType(NKM_DIVE_EVENT_TYPE type)
		{
			return type == NKM_DIVE_EVENT_TYPE.NDET_BEACON_RANDOM || type == NKM_DIVE_EVENT_TYPE.NDET_BEACON_DUNGEON || type == NKM_DIVE_EVENT_TYPE.NDET_BEACON_BLANK || type == NKM_DIVE_EVENT_TYPE.NDET_BEACON_ITEM || type == NKM_DIVE_EVENT_TYPE.NDET_BEACON_UNIT || type == NKM_DIVE_EVENT_TYPE.NDET_BEACON_STORM;
		}

		// Token: 0x060033F0 RID: 13296 RVA: 0x00104B28 File Offset: 0x00102D28
		public static bool IsRescueSignalEventType(NKM_DIVE_EVENT_TYPE type)
		{
			return type == NKM_DIVE_EVENT_TYPE.NDET_UNIT;
		}

		// Token: 0x060033F1 RID: 13297 RVA: 0x00104B31 File Offset: 0x00102D31
		public static bool IsLostShipEventType(NKM_DIVE_EVENT_TYPE type)
		{
			return type == NKM_DIVE_EVENT_TYPE.NDET_LOSTSHIP_RANDOM || type == NKM_DIVE_EVENT_TYPE.NDET_LOSTSHIP_ITEM || type == NKM_DIVE_EVENT_TYPE.NDET_LOSTSHIP_UNIT || type == NKM_DIVE_EVENT_TYPE.NDET_LOSTSHIP_REPAIR || type == NKM_DIVE_EVENT_TYPE.NDET_LOSTSHIP_SUPPLY;
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x00104B4E File Offset: 0x00102D4E
		public static bool IsSafetyEventType(NKM_DIVE_EVENT_TYPE type)
		{
			return type == NKM_DIVE_EVENT_TYPE.NDET_BLANK;
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x00104B57 File Offset: 0x00102D57
		public static bool IsRepairKitEventType(NKM_DIVE_EVENT_TYPE type)
		{
			return type == NKM_DIVE_EVENT_TYPE.NDET_REPAIR;
		}

		// Token: 0x060033F4 RID: 13300 RVA: 0x00104B60 File Offset: 0x00102D60
		public static bool IsArtifactEventType(NKM_DIVE_EVENT_TYPE type)
		{
			return type == NKM_DIVE_EVENT_TYPE.NDET_ARTIFACT;
		}

		// Token: 0x060033F5 RID: 13301 RVA: 0x00104B6C File Offset: 0x00102D6C
		public static bool IsDiveJump()
		{
			NKMDiveGameData diveGameData = NKCScenManager.CurrentUserData().m_DiveGameData;
			return diveGameData != null && diveGameData.Floor.SlotSets.Count == 1;
		}

		// Token: 0x060033F6 RID: 13302 RVA: 0x00104B9C File Offset: 0x00102D9C
		public static NKMDiveTemplet GetTempletByUnlockData(STAGE_UNLOCK_REQ_TYPE type, int value)
		{
			return NKMTempletContainer<NKMDiveTemplet>.Find((NKMDiveTemplet e) => e.StageUnlockReqType == type && e.StageUnlockReqValue == value);
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x00104BC4 File Offset: 0x00102DC4
		public static int GetDiveCost(NKMDiveTemplet templet, int cityID, bool bJump)
		{
			if (templet == null)
			{
				return 0;
			}
			int num = templet.StageReqItemCount;
			if (bJump)
			{
				num += templet.GetDiveJumpPlusCost();
			}
			int diveDiscountCost = NKCDiveManager.GetDiveDiscountCost(cityID, num);
			return num - diveDiscountCost;
		}

		// Token: 0x060033F8 RID: 13304 RVA: 0x00104BF8 File Offset: 0x00102DF8
		public static int GetDiveDiscountCost(int cityID, int costCount)
		{
			if (cityID == 0)
			{
				return 0;
			}
			NKMWorldMapCityData cityData = NKCScenManager.CurrentUserData().m_WorldmapData.GetCityData(cityID);
			if (cityData != null)
			{
				float num = cityData.CalcBuildStat(NKM_CITY_BUILDING_STAT.CBS_DIVE_INFORMATION_REDUCE_RATE, (float)costCount);
				return Math.Min(costCount, (int)Math.Ceiling((double)num));
			}
			return 0;
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x00104C3C File Offset: 0x00102E3C
		public static void Initialize()
		{
			NKCDiveManager.BeginIndex = int.MaxValue;
			NKCDiveManager.EndIndex = int.MinValue;
			foreach (NKMDiveTemplet nkmdiveTemplet in NKMTempletContainer<NKMDiveTemplet>.Values)
			{
				if (!nkmdiveTemplet.IsEventDive)
				{
					if (NKCDiveManager.BeginIndex > nkmdiveTemplet.IndexID)
					{
						NKCDiveManager.BeginIndex = nkmdiveTemplet.IndexID;
					}
					if (NKCDiveManager.EndIndex < nkmdiveTemplet.IndexID)
					{
						NKCDiveManager.EndIndex = nkmdiveTemplet.IndexID;
					}
				}
			}
			NKCDiveManager.SortedTemplates = (from e in NKMTempletContainer<NKMDiveTemplet>.Values
			orderby e.IndexID
			select e).ToList<NKMDiveTemplet>();
		}

		// Token: 0x04003250 RID: 12880
		public const int MAX_SQUAD_COUNT = 5;

		// Token: 0x04003251 RID: 12881
		public const int NO_SUPPLY_DISADVANTAGE_RATE = 100;

		// Token: 0x04003252 RID: 12882
		public const int MAX_RESERVED_ARTIFACT_COUNT = 3;
	}
}
