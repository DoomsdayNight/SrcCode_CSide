using System;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.Common;
using ClientPacket.Mode;
using Cs.Core.Util;
using Cs.Logging;
using NKC;
using NKM.Shop;
using NKM.Templet;

namespace NKM
{
	// Token: 0x020003BB RID: 955
	public static class NKMContentUnlockManager
	{
		// Token: 0x06001903 RID: 6403 RVA: 0x0006686B File Offset: 0x00064A6B
		public static bool IsValidMissionUnlockType(UnlockInfo unlockInfo)
		{
			return NKMContentUnlockManager.IsValidMissionUnlockType(unlockInfo.eReqType, unlockInfo.reqValue, unlockInfo.reqValueStr, unlockInfo.reqDateTime);
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0006688C File Offset: 0x00064A8C
		private static bool IsValidMissionUnlockType(STAGE_UNLOCK_REQ_TYPE reqType, int reqValue, string reqValueStr, DateTime reqDateTime)
		{
			switch (reqType)
			{
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_WARFARE:
				if (NKMWarfareTemplet.Find(reqValue) == null)
				{
					Log.Error(string.Format("[ContentUnlock] 전역 클리어 대상이 존재하지 않음 m_StageUnlockReqType : {0}, m_MissionUnlockReqValue : {1}", reqType, reqValue), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMContentUnlockManager.cs", 224);
					return false;
				}
				break;
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_GET:
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_LEVEL_20:
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_LEVEL_25:
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_LEVEL_50:
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_LEVEL_80:
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_LEVEL_100:
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_LIMIT_GARDE_1:
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_LIMIT_GARDE_2:
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_LIMIT_GARDE_3:
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_DEVOTION:
				if (NKMUnitManager.GetUnitTempletBase(reqValue) == null)
				{
					Log.Error(string.Format("[ContentUnlock] 유닛 획득 대상이 존재하지 않음 m_StageUnlockReqType : {0}, m_MissionUnlockReqValue : {1}", reqType, reqValue), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMContentUnlockManager.cs", 242);
					return false;
				}
				break;
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DUNGEON:
				if (NKMDungeonManager.GetDungeonTempletBase(reqValue) == null)
				{
					Log.Error(string.Format("[ContentUnlock] 던전 클리어 대상이 존재하지 않음 m_StageUnlockReqType : {0}, m_MissionUnlockReqValue : {1}", reqType, reqValue), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMContentUnlockManager.cs", 252);
					return false;
				}
				break;
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DIVE:
				if (NKMDiveTemplet.Find(reqValue) == null)
				{
					Log.Error(string.Format("[ContentUnlock] 컨텐츠 해제조건(다이브 클리어) 대상이 존재하지 않음 m_StageUnlockReqType : {0}, m_MissionUnlockReqValue : {1}", reqType, reqValue), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMContentUnlockManager.cs", 270);
					return false;
				}
				break;
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_PHASE:
				if (NKMPhaseTemplet.Find(reqValue) == null)
				{
					Log.Error(string.Format("[ContentUnlock] 페이즈 클리어 대상이 존재하지 않음 m_StageUnlockReqType : {0}, m_MissionUnlockReqValue : {1}", reqType, reqValue), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMContentUnlockManager.cs", 261);
					return false;
				}
				break;
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_TRIM:
			{
				NKMTrimTemplet nkmtrimTemplet = NKMTrimTemplet.Find(reqValue);
				if (nkmtrimTemplet == null)
				{
					Log.Error(string.Format("[ContentUnlock] 컨텐츠 해제조건 SURT_CLEAR_TRIM : 트리밍 목표 던전 {0}의 템플릿이 존재하지 않음", reqValue), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMContentUnlockManager.cs", 357);
					return false;
				}
				int num;
				if (!int.TryParse(reqValueStr, out num))
				{
					Log.Error("[ContentUnlock] 컨텐츠 해제조건 SURT_CLEAR_TRIM : 트리밍 목표 레벨 " + reqValueStr + "의 파싱 실패. 반드시 숫자 형식이어야 함", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMContentUnlockManager.cs", 363);
					return false;
				}
				if (nkmtrimTemplet.MaxTrimLevel < num)
				{
					Log.Error(string.Format("[ContentUnlock] 컨텐츠 해제조건 SURT_CLEAR_TRIM : 트리밍 목표 레벨 {0}이 해당 트리밍 던전의 최대 레벨 {1}보다 큼", reqValueStr, nkmtrimTemplet.MaxTrimLevel), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMContentUnlockManager.cs", 369);
					return false;
				}
				break;
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_START_DATETIME:
				if (reqDateTime <= DateTime.MinValue)
				{
					Log.Error(string.Format("[ContentUnlock] 컨텐츠 해제조건(시작시간 설정) 시간이 존재하지 않거나 잘못입력됨 m_StageUnlockReqType : {0}, m_MissionUnlockReqValueStr : {1}", reqType, reqValueStr), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMContentUnlockManager.cs", 280);
					return false;
				}
				break;
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_WARFARE_START_DATETIME:
				return NKMContentUnlockManager.IsValidMissionUnlockType(STAGE_UNLOCK_REQ_TYPE.SURT_START_DATETIME, reqValue, reqValueStr, reqDateTime) && NKMContentUnlockManager.IsValidMissionUnlockType(STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_WARFARE, reqValue, reqValueStr, reqDateTime);
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DUNGEON_START_DATETIME:
				return NKMContentUnlockManager.IsValidMissionUnlockType(STAGE_UNLOCK_REQ_TYPE.SURT_START_DATETIME, reqValue, reqValueStr, reqDateTime) && NKMContentUnlockManager.IsValidMissionUnlockType(STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DUNGEON, reqValue, reqValueStr, reqDateTime);
			case STAGE_UNLOCK_REQ_TYPE.SURT_RETURN_USER:
			{
				ReturningUserType returningUserType;
				if (!string.IsNullOrEmpty(reqValueStr) && !Enum.TryParse<ReturningUserType>(reqValueStr, out returningUserType))
				{
					Log.Error(string.Format("[ContentUnlock] 컨텐츠 해제조건(리턴유저) ReturningUserType 타입이 존재하지 않거나 잘못입력됨 m_StageUnlockReqType : {0}, m_MissionUnlockReqValueStr : {1}", reqType, reqValueStr), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMContentUnlockManager.cs", 310);
					return false;
				}
				break;
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_SHOP_BUY_ITEM_ALL:
			{
				ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(reqValue);
				if (shopItemTemplet == null)
				{
					Log.Error(string.Format("[ContentUnlock] 컨텐츠 해제조건 SURT_SHOP_BUY_ITEM_ALL : 상품이 존재하지 않음. m_StageUnlockReqType : {0}, m_MissionUnlockReqValue : {1}", reqType, reqValue), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMContentUnlockManager.cs", 329);
					return false;
				}
				if (shopItemTemplet.resetType == SHOP_RESET_TYPE.Unlimited)
				{
					Log.Error(string.Format("[ContentUnlock] 컨텐츠 해제조건 SURT_SHOP_BUY_ITEM_ALL : 목표 상품이 무한 구매 가능함. m_StageUnlockReqType : {0}, m_MissionUnlockReqValue : {1}", reqType, reqValue), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMContentUnlockManager.cs", 335);
					return false;
				}
				break;
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_LAST_STAGE:
				return NKMStageTempletV2.Find(reqValue) != null;
			case STAGE_UNLOCK_REQ_TYPE.SURT_INTERVAL:
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_WARFARE_INTERVAL:
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DUNGEON_INTERVAL:
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_PHASE_INTERVAL:
				if (NKMIntervalTemplet.Find(reqValueStr) == null)
				{
					Log.Error(string.Format("[ContentUnlock] 인터벌 템플릿이 존재하지 않음. reqType : {0}, reqValueStr : {1}", reqType, reqValueStr), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMContentUnlockManager.cs", 347);
					return false;
				}
				break;
			case STAGE_UNLOCK_REQ_TYPE.SURT_DIVE_HISTORY_CLEARED:
				if (NKMDiveTemplet.Find(reqValue) == null)
				{
					Log.Error(string.Format("[ContentUnlock] 컨텐츠 해제조건(다이브 클리어 기록) 대상이 존재하지 않음 m_StageUnlockReqType : {0}, m_MissionUnlockReqValue : {1}", reqType, reqValue), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMContentUnlockManager.cs", 378);
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x00066C4B File Offset: 0x00064E4B
		public static bool IsContentUnlocked(NKMUserData cNKMUserData, in UnlockInfo unlockInfo, out bool bAdmin)
		{
			if (NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, unlockInfo, true))
			{
				bAdmin = false;
				return true;
			}
			if (cNKMUserData.IsSuperUser())
			{
				bAdmin = true;
				return true;
			}
			bAdmin = false;
			return false;
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x00066C70 File Offset: 0x00064E70
		public static bool IsContentUnlocked(NKMUserData cNKMUserData, in List<UnlockInfo> lstUnlockInfo, out bool bAdmin)
		{
			foreach (UnlockInfo unlockInfo in lstUnlockInfo)
			{
				bool flag;
				if (!NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, unlockInfo, out flag))
				{
					bAdmin = false;
					return false;
				}
				if (flag)
				{
					bAdmin = true;
					return true;
				}
			}
			bAdmin = false;
			return true;
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x00066CDC File Offset: 0x00064EDC
		public static bool IsContentUnlocked(NKMUserData cNKMUserData, in List<UnlockInfo> lstUnlockInfo, bool ignoreSuperUser = false)
		{
			foreach (UnlockInfo unlockInfo in lstUnlockInfo)
			{
				if (!NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, unlockInfo, ignoreSuperUser))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x00066D38 File Offset: 0x00064F38
		public static bool IsContentUnlocked(NKMUserData cNKMUserData, in UnlockInfo unlockInfo, bool ignoreSuperUser = false)
		{
			if (cNKMUserData == null)
			{
				return false;
			}
			if (cNKMUserData.IsSuperUser() && !ignoreSuperUser)
			{
				return true;
			}
			switch (unlockInfo.eReqType)
			{
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_WARFARE:
				return cNKMUserData.CheckWarfareClear(unlockInfo.reqValue);
			case STAGE_UNLOCK_REQ_TYPE.SURT_CITY_COUNT:
				return cNKMUserData.m_WorldmapData.GetUnlockedCityCount() >= unlockInfo.reqValue;
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_GET:
				return cNKMUserData.m_ArmyData.IsCollectedUnit(unlockInfo.reqValue);
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_LEVEL_20:
				return cNKMUserData.m_ArmyData.SearchUnitByID(NKM_UNIT_TYPE.NUT_NORMAL, unlockInfo.reqValue, NKMArmyData.UNIT_SEARCH_OPTION.Level, 20);
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_LEVEL_25:
				return cNKMUserData.m_ArmyData.SearchUnitByID(NKM_UNIT_TYPE.NUT_NORMAL, unlockInfo.reqValue, NKMArmyData.UNIT_SEARCH_OPTION.Level, 25);
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_LEVEL_50:
				return cNKMUserData.m_ArmyData.SearchUnitByID(NKM_UNIT_TYPE.NUT_NORMAL, unlockInfo.reqValue, NKMArmyData.UNIT_SEARCH_OPTION.Level, 50);
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_LEVEL_80:
				return cNKMUserData.m_ArmyData.SearchUnitByID(NKM_UNIT_TYPE.NUT_NORMAL, unlockInfo.reqValue, NKMArmyData.UNIT_SEARCH_OPTION.Level, 80);
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_LEVEL_100:
				return cNKMUserData.m_ArmyData.SearchUnitByID(NKM_UNIT_TYPE.NUT_NORMAL, unlockInfo.reqValue, NKMArmyData.UNIT_SEARCH_OPTION.Level, 100);
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_LIMIT_GARDE_1:
				return cNKMUserData.m_ArmyData.SearchUnitByID(NKM_UNIT_TYPE.NUT_NORMAL, unlockInfo.reqValue, NKMArmyData.UNIT_SEARCH_OPTION.LimitLevel, 1);
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_LIMIT_GARDE_2:
				return cNKMUserData.m_ArmyData.SearchUnitByID(NKM_UNIT_TYPE.NUT_NORMAL, unlockInfo.reqValue, NKMArmyData.UNIT_SEARCH_OPTION.LimitLevel, 2);
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_LIMIT_GARDE_3:
				return cNKMUserData.m_ArmyData.SearchUnitByID(NKM_UNIT_TYPE.NUT_NORMAL, unlockInfo.reqValue, NKMArmyData.UNIT_SEARCH_OPTION.LimitLevel, 3);
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_DEVOTION:
				return cNKMUserData.m_ArmyData.SearchUnitByID(NKM_UNIT_TYPE.NUT_NORMAL, unlockInfo.reqValue, NKMArmyData.UNIT_SEARCH_OPTION.Devotion, 0);
			case STAGE_UNLOCK_REQ_TYPE.SURT_PLAYER_LEVEL:
				return cNKMUserData.m_UserLevel >= unlockInfo.reqValue;
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DUNGEON:
				return cNKMUserData.CheckDungeonClear(unlockInfo.reqValue);
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DIVE:
				return cNKMUserData.CheckDiveClear(unlockInfo.reqValue);
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_PHASE:
				return NKCPhaseManager.CheckPhaseClear(unlockInfo.reqValue);
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_TRIM:
			{
				int trimLevel;
				return int.TryParse(unlockInfo.reqValueStr, out trimLevel) && cNKMUserData.TrimData.GetTrimClearData(unlockInfo.reqValue, trimLevel) != null;
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_START_DATETIME:
				return unlockInfo.reqDateTime < ServiceTime.Recent;
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_WARFARE_START_DATETIME:
				return cNKMUserData.CheckWarfareClear(unlockInfo.reqValue) && unlockInfo.reqDateTime < ServiceTime.Recent;
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DUNGEON_START_DATETIME:
				return cNKMUserData.CheckDungeonClear(unlockInfo.reqValue) && unlockInfo.reqDateTime < ServiceTime.Recent;
			case STAGE_UNLOCK_REQ_TYPE.SURT_ALWAYS_LOCKED:
			case STAGE_UNLOCK_REQ_TYPE.SURT_ALWAYS_HIDDEN:
				return false;
			case STAGE_UNLOCK_REQ_TYPE.SURT_ALWAYS_UNLOCKED:
				return true;
			case STAGE_UNLOCK_REQ_TYPE.SURT_REGISTER_DATE:
				return ServiceTime.FromUtcTime(cNKMUserData.m_NKMUserDateData.m_RegisterTime) >= unlockInfo.reqDateTime;
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_PALACE:
			{
				int palaceId = unlockInfo.reqValue;
				List<NKMShadowBattleTemplet> battleTemplets = NKMShadowPalaceManager.GetBattleTemplets(palaceId);
				if (battleTemplets == null)
				{
					return false;
				}
				NKMPalaceData nkmpalaceData = cNKMUserData.m_ShadowPalace.palaceDataList.Find((NKMPalaceData e) => e.palaceId == palaceId);
				if (nkmpalaceData == null)
				{
					return false;
				}
				return battleTemplets.Count == (from e in nkmpalaceData.dungeonDataList
				where e.bestTime != 0
				select e).Count<NKMPalaceDungeonData>();
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_GUILD_LEVEL:
				return NKCGuildManager.MyGuildData != null && NKCGuildManager.MyGuildData.guildLevel >= unlockInfo.reqValue;
			case STAGE_UNLOCK_REQ_TYPE.SURT_NEWBIE_USER:
			{
				int num;
				if (int.TryParse(unlockInfo.reqValueStr, out num))
				{
					DateTime registerTime = cNKMUserData.m_NKMUserDateData.m_RegisterTime;
					return registerTime.AddDays((double)num) < ServiceTime.ToUtcTime(ServiceTime.Recent) && cNKMUserData.m_NKMUserDateData.m_RegisterTime.AddDays((double)unlockInfo.reqValue) > ServiceTime.ToUtcTime(ServiceTime.Recent);
				}
				return cNKMUserData.m_NKMUserDateData.m_RegisterTime.AddDays((double)unlockInfo.reqValue) > ServiceTime.ToUtcTime(ServiceTime.Recent);
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_MISSION_CLEAR:
				return cNKMUserData.m_MissionData.GetCompletedMissionData(unlockInfo.reqValue) != null;
			case STAGE_UNLOCK_REQ_TYPE.SURT_MISSION_TAB_ALL_CLEAR:
				if (cNKMUserData.m_MissionData.IsTabComplete(unlockInfo.reqValue))
				{
					return true;
				}
				using (List<NKMMissionData>.Enumerator enumerator = cNKMUserData.m_MissionData.GetAllMissionList(unlockInfo.reqValue).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.isComplete)
						{
							return false;
						}
					}
				}
				cNKMUserData.m_MissionData.SetTabComplete(unlockInfo.reqValue);
				return true;
			case STAGE_UNLOCK_REQ_TYPE.SURT_CONTENT_TAG:
				return NKMContentsVersionManager.HasTag(unlockInfo.reqValueStr);
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_COLLECTION_RARITY_COUNT:
			{
				NKM_UNIT_GRADE nkm_UNIT_GRADE;
				if (!Enum.TryParse<NKM_UNIT_GRADE>(unlockInfo.reqValueStr, out nkm_UNIT_GRADE))
				{
					return false;
				}
				int num2 = 0;
				using (HashSet<int>.Enumerator enumerator2 = cNKMUserData.m_ArmyData.m_illustrateUnit.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (NKMUnitManager.GetUnitTempletBase(enumerator2.Current).m_NKM_UNIT_GRADE == nkm_UNIT_GRADE)
						{
							num2++;
							if (num2 >= unlockInfo.reqValue)
							{
								return true;
							}
						}
					}
				}
				return false;
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_PVP_RANK_SCORE:
				return cNKMUserData.m_PvpData.Score >= unlockInfo.reqValue;
			case STAGE_UNLOCK_REQ_TYPE.SURT_PVP_RANK_SCORE_RECORD:
				return cNKMUserData.m_PvpData.MaxScore >= unlockInfo.reqValue;
			case STAGE_UNLOCK_REQ_TYPE.SURT_PVP_ASYNC_SCORE:
				return cNKMUserData.m_AsyncData.Score >= unlockInfo.reqValue;
			case STAGE_UNLOCK_REQ_TYPE.SURT_PVP_ASYNC_SCORE_RECORD:
				return cNKMUserData.m_AsyncData.MaxScore >= unlockInfo.reqValue;
			case STAGE_UNLOCK_REQ_TYPE.SURT_SHOP_BUY_ITEM_ALL:
				return NKCShopManager.GetBuyCountLeft(unlockInfo.reqValue) == 0;
			case STAGE_UNLOCK_REQ_TYPE.SURT_UNLOCK_STAGE:
			{
				NKMStageTempletV2 nkmstageTempletV = NKMStageTempletV2.Find(unlockInfo.reqValue);
				return nkmstageTempletV != null && NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), nkmstageTempletV.m_UnlockInfo, false);
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_LAST_STAGE:
			{
				NKMStageTempletV2 nkmstageTempletV2 = NKMStageTempletV2.Find(unlockInfo.reqValue);
				if (nkmstageTempletV2 == null)
				{
					return false;
				}
				switch (nkmstageTempletV2.m_STAGE_TYPE)
				{
				case STAGE_TYPE.ST_WARFARE:
					if (nkmstageTempletV2.WarfareTemplet != null)
					{
						UnlockInfo unlockInfo2 = new UnlockInfo(STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_WARFARE, nkmstageTempletV2.WarfareTemplet.m_WarfareID);
						return NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, unlockInfo2, false);
					}
					break;
				case STAGE_TYPE.ST_DUNGEON:
					if (nkmstageTempletV2.DungeonTempletBase != null)
					{
						UnlockInfo unlockInfo2 = new UnlockInfo(STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DUNGEON, nkmstageTempletV2.DungeonTempletBase.m_DungeonID);
						return NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, unlockInfo2, false);
					}
					break;
				case STAGE_TYPE.ST_PHASE:
					if (nkmstageTempletV2.PhaseTemplet != null)
					{
						UnlockInfo unlockInfo2 = new UnlockInfo(STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_PHASE, nkmstageTempletV2.PhaseTemplet.Id);
						return NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, unlockInfo2, false);
					}
					break;
				}
				return false;
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_MISSION_TAB_UNLOCKED:
				return NKMMissionManager.CheckMissionTabUnlocked(unlockInfo.reqValue, cNKMUserData);
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_STAGE:
			{
				NKMStageTempletV2 nkmstageTempletV3 = NKMStageTempletV2.Find(unlockInfo.reqValue);
				if (nkmstageTempletV3 == null)
				{
					Log.Error(string.Format("NKMStageTemplet is null : stageId : {0}", unlockInfo.reqValue), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMContentUnlockManagerEx.cs", 184);
					return false;
				}
				switch (nkmstageTempletV3.m_STAGE_TYPE)
				{
				case STAGE_TYPE.ST_WARFARE:
					return cNKMUserData.CheckWarfareClear(nkmstageTempletV3.m_StageBattleStrID);
				case STAGE_TYPE.ST_DUNGEON:
					return cNKMUserData.CheckDungeonClear(nkmstageTempletV3.m_StageBattleStrID);
				case STAGE_TYPE.ST_PHASE:
					return NKCPhaseManager.CheckPhaseStageClear(nkmstageTempletV3);
				default:
					return false;
				}
				break;
			}
			case STAGE_UNLOCK_REQ_TYPE.SURT_INTERVAL:
				return NKCSynchronizedTime.IsEventTime(unlockInfo.reqValueStr);
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_WARFARE_INTERVAL:
				return NKCSynchronizedTime.IsEventTime(unlockInfo.reqValueStr) && cNKMUserData.CheckWarfareClear(unlockInfo.reqValue);
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DUNGEON_INTERVAL:
				return NKCSynchronizedTime.IsEventTime(unlockInfo.reqValueStr) && cNKMUserData.CheckDungeonClear(unlockInfo.reqValue);
			case STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_PHASE_INTERVAL:
				return NKCSynchronizedTime.IsEventTime(unlockInfo.reqValueStr) && NKCPhaseManager.CheckPhaseClear(unlockInfo.reqValue);
			case STAGE_UNLOCK_REQ_TYPE.SURT_DIVE_HISTORY_CLEARED:
				return cNKMUserData.CheckDiveHistory(unlockInfo.reqValue);
			case STAGE_UNLOCK_REQ_TYPE.SURT_OPEN_SECTION:
				return cNKMUserData.OfficeData.IsOpenedSection(unlockInfo.reqValue);
			case STAGE_UNLOCK_REQ_TYPE.SURT_OPEN_ROOM:
				return cNKMUserData.OfficeData.IsOpenedRoom(unlockInfo.reqValue);
			case STAGE_UNLOCK_REQ_TYPE.SURT_SKIN_GET:
				return unlockInfo.reqValue > 0 && cNKMUserData.m_InventoryData.HasItemSkin(unlockInfo.reqValue);
			}
			Log.Debug("NKMContentUnlockManager::IsContentUnlocked() Undefined Type:" + unlockInfo.eReqType.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMContentUnlockManagerEx.cs", 413);
			return false;
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x000674E0 File Offset: 0x000656E0
		public static ContentUnlockStatus CheckMissionUnlocked(NKMUserData userData, STAGE_BASIC_UNLOCK_TYPE basicUnlockType, UnlockInfo unlockInfo)
		{
			if (NKMContentUnlockManager.IsContentUnlocked(userData, unlockInfo, false))
			{
				return ContentUnlockStatus.Unlocked;
			}
			if (basicUnlockType == STAGE_BASIC_UNLOCK_TYPE.SBUT_LOCK)
			{
				return ContentUnlockStatus.Locked_Visible;
			}
			return ContentUnlockStatus.Locked_Invisible;
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x000674F6 File Offset: 0x000656F6
		public static bool IsTimeLimitCondition(UnlockInfo unlockInfo)
		{
			return NKMContentUnlockManager.GetConditionTimeLimit(unlockInfo) < DateTime.MaxValue;
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x00067508 File Offset: 0x00065708
		public static bool IsStarted(UnlockInfo unlockInfo)
		{
			return NKMContentUnlockManager.GetConditionStartTime(unlockInfo) <= NKCSynchronizedTime.GetServerUTCTime(0.0);
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x00067524 File Offset: 0x00065724
		public static bool IsStarted(List<UnlockInfo> unlockInfoList)
		{
			bool flag = true;
			foreach (UnlockInfo unlockInfo in unlockInfoList)
			{
				DateTime conditionStartTime = NKMContentUnlockManager.GetConditionStartTime(unlockInfo);
				flag &= (conditionStartTime <= NKCSynchronizedTime.GetServerUTCTime(0.0));
			}
			return flag;
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x0006758C File Offset: 0x0006578C
		public static DateTime GetConditionStartTime(List<UnlockInfo> lstUnlockInfo)
		{
			DateTime dateTime = DateTime.MinValue;
			foreach (UnlockInfo unlockInfo in lstUnlockInfo)
			{
				DateTime conditionStartTime = NKMContentUnlockManager.GetConditionStartTime(unlockInfo);
				if (conditionStartTime > dateTime)
				{
					dateTime = conditionStartTime;
				}
			}
			return dateTime;
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x000675EC File Offset: 0x000657EC
		public static DateTime GetConditionStartTime(UnlockInfo unlockInfo)
		{
			STAGE_UNLOCK_REQ_TYPE eReqType = unlockInfo.eReqType;
			if (eReqType <= STAGE_UNLOCK_REQ_TYPE.SURT_NEWBIE_USER)
			{
				if (eReqType - STAGE_UNLOCK_REQ_TYPE.SURT_START_DATETIME <= 2)
				{
					return ServiceTime.ToUtcTime(unlockInfo.reqDateTime);
				}
				if (eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_NEWBIE_USER)
				{
					int num;
					if (int.TryParse(unlockInfo.reqValueStr, out num))
					{
						DateTime registerTime = NKCScenManager.CurrentUserData().m_NKMUserDateData.m_RegisterTime;
						return registerTime.AddDays((double)num);
					}
					return NKCScenManager.CurrentUserData().m_NKMUserDateData.m_RegisterTime;
				}
			}
			else if (eReqType != STAGE_UNLOCK_REQ_TYPE.SURT_RETURN_USER)
			{
				if (eReqType - STAGE_UNLOCK_REQ_TYPE.SURT_INTERVAL <= 3)
				{
					NKMIntervalTemplet nkmintervalTemplet = NKMIntervalTemplet.Find(unlockInfo.reqValueStr);
					if (nkmintervalTemplet != null)
					{
						return nkmintervalTemplet.GetStartDateUtc();
					}
				}
			}
			else
			{
				if (string.IsNullOrEmpty(unlockInfo.reqValueStr))
				{
					DateTime dateTime = DateTime.MaxValue;
					foreach (object obj in Enum.GetValues(typeof(ReturningUserType)))
					{
						ReturningUserType state = (ReturningUserType)obj;
						DateTime returnStartDate = NKCScenManager.CurrentUserData().GetReturnStartDate(state);
						if (dateTime > returnStartDate)
						{
							dateTime = returnStartDate;
						}
					}
					return dateTime;
				}
				ReturningUserType state2;
				if (Enum.TryParse<ReturningUserType>(unlockInfo.reqValueStr, out state2))
				{
					return NKCScenManager.CurrentUserData().GetReturnStartDate(state2);
				}
				return DateTime.MaxValue;
			}
			return DateTime.MinValue;
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x0006773C File Offset: 0x0006593C
		public static DateTime GetConditionTimeLimit(UnlockInfo unlockInfo)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			STAGE_UNLOCK_REQ_TYPE eReqType = unlockInfo.eReqType;
			if (eReqType != STAGE_UNLOCK_REQ_TYPE.SURT_NEWBIE_USER)
			{
				if (eReqType != STAGE_UNLOCK_REQ_TYPE.SURT_RETURN_USER)
				{
					if (eReqType - STAGE_UNLOCK_REQ_TYPE.SURT_INTERVAL <= 3)
					{
						NKMIntervalTemplet nkmintervalTemplet = NKMIntervalTemplet.Find(unlockInfo.reqValueStr);
						if (nkmintervalTemplet != null)
						{
							return nkmintervalTemplet.GetEndDateUtc();
						}
					}
				}
				else
				{
					if (string.IsNullOrEmpty(unlockInfo.reqValueStr))
					{
						DateTime dateTime = DateTime.MinValue;
						foreach (object obj in Enum.GetValues(typeof(ReturningUserType)))
						{
							ReturningUserType state = (ReturningUserType)obj;
							DateTime returnEndDate = nkmuserData.GetReturnEndDate(state);
							if (dateTime < returnEndDate)
							{
								dateTime = returnEndDate;
							}
						}
						return dateTime;
					}
					ReturningUserType state2;
					if (Enum.TryParse<ReturningUserType>(unlockInfo.reqValueStr, out state2))
					{
						return nkmuserData.GetReturnEndDate(state2);
					}
				}
				return DateTime.MaxValue;
			}
			return nkmuserData.m_NKMUserDateData.m_RegisterTime.AddDays((double)unlockInfo.reqValue);
		}
	}
}
