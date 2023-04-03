using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Community;
using ClientPacket.Contract;
using ClientPacket.Event;
using ClientPacket.Item;
using ClientPacket.Mode;
using ClientPacket.Shop;
using ClientPacket.User;
using ClientPacket.WorldMap;
using Cs.Logging;
using Cs.Protocol;
using NKC;
using NKC.Office;
using NKC.Publisher;
using NKC.Trim;
using NKC.UI;
using NKC.UI.Warfare;
using NKM.Guild;
using NKM.Shop;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;

namespace NKM
{
	// Token: 0x02000500 RID: 1280
	public class NKMUserData : ISerializable
	{
		// Token: 0x06002420 RID: 9248 RVA: 0x000BB988 File Offset: 0x000B9B88
		public long GetCash()
		{
			NKMItemMiscData itemMisc = this.m_InventoryData.GetItemMisc(101);
			if (itemMisc != null)
			{
				return itemMisc.TotalCount;
			}
			return 0L;
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x000BB9B0 File Offset: 0x000B9BB0
		public long GetCashPaid()
		{
			NKMItemMiscData itemMisc = this.m_InventoryData.GetItemMisc(101);
			if (itemMisc != null)
			{
				return itemMisc.CountPaid;
			}
			return 0L;
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x000BB9D8 File Offset: 0x000B9BD8
		public long GetCashBonus()
		{
			NKMItemMiscData itemMisc = this.m_InventoryData.GetItemMisc(101);
			if (itemMisc != null)
			{
				return itemMisc.CountFree;
			}
			return 0L;
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x000BBA00 File Offset: 0x000B9C00
		public long GetCredit()
		{
			NKMItemMiscData itemMisc = this.m_InventoryData.GetItemMisc(1);
			if (itemMisc != null)
			{
				return itemMisc.TotalCount;
			}
			return 0L;
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x000BBA28 File Offset: 0x000B9C28
		public long GetEternium()
		{
			NKMItemMiscData itemMisc = this.m_InventoryData.GetItemMisc(2);
			if (itemMisc != null)
			{
				return itemMisc.TotalCount;
			}
			return 0L;
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x000BBA50 File Offset: 0x000B9C50
		public long GetInformation()
		{
			NKMItemMiscData itemMisc = this.m_InventoryData.GetItemMisc(3);
			if (itemMisc != null)
			{
				return itemMisc.TotalCount;
			}
			return 0L;
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x000BBA76 File Offset: 0x000B9C76
		public long GetMissionAchievePoint()
		{
			return this.m_MissionData.GetAchiecePoint();
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x000BBA84 File Offset: 0x000B9C84
		public long GetDailyTicket()
		{
			NKMItemMiscData itemMisc = this.m_InventoryData.GetItemMisc(4);
			if (itemMisc != null)
			{
				return itemMisc.CountPaid;
			}
			return 0L;
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x000BBAAC File Offset: 0x000B9CAC
		public long GetDailyTicket_A()
		{
			NKMItemMiscData itemMisc = this.m_InventoryData.GetItemMisc(15);
			if (itemMisc != null)
			{
				return itemMisc.CountPaid;
			}
			return 0L;
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x000BBAD4 File Offset: 0x000B9CD4
		public long GetDailyTicket_B()
		{
			NKMItemMiscData itemMisc = this.m_InventoryData.GetItemMisc(16);
			if (itemMisc != null)
			{
				return itemMisc.CountPaid;
			}
			return 0L;
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x000BBAFC File Offset: 0x000B9CFC
		public long GetDailyTicket_C()
		{
			NKMItemMiscData itemMisc = this.m_InventoryData.GetItemMisc(17);
			if (itemMisc != null)
			{
				return itemMisc.CountPaid;
			}
			return 0L;
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x000BBB24 File Offset: 0x000B9D24
		public long GetDailyTicketBonus()
		{
			NKMItemMiscData itemMisc = this.m_InventoryData.GetItemMisc(4);
			if (itemMisc != null)
			{
				return itemMisc.CountFree;
			}
			return 0L;
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x000BBB4C File Offset: 0x000B9D4C
		public long GetDailyTicketA_Bonus()
		{
			NKMItemMiscData itemMisc = this.m_InventoryData.GetItemMisc(15);
			if (itemMisc != null)
			{
				return itemMisc.CountFree;
			}
			return 0L;
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x000BBB74 File Offset: 0x000B9D74
		public long GetDailyTicketB_Bonus()
		{
			NKMItemMiscData itemMisc = this.m_InventoryData.GetItemMisc(16);
			if (itemMisc != null)
			{
				return itemMisc.CountFree;
			}
			return 0L;
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x000BBB9C File Offset: 0x000B9D9C
		public long GetDailyTicketC_Bonus()
		{
			NKMItemMiscData itemMisc = this.m_InventoryData.GetItemMisc(17);
			if (itemMisc != null)
			{
				return itemMisc.CountFree;
			}
			return 0L;
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x000BBBC3 File Offset: 0x000B9DC3
		public void SetCash(long cash)
		{
			this.m_InventoryData.UpdateItemInfo(101, cash, NKM_ITEM_PAYMENT_TYPE.NIPT_PAID);
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x000BBBD4 File Offset: 0x000B9DD4
		public void SetCashBonus(long cash_bonus)
		{
			this.m_InventoryData.UpdateItemInfo(101, cash_bonus, NKM_ITEM_PAYMENT_TYPE.NIPT_FREE);
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x000BBBE5 File Offset: 0x000B9DE5
		public void SetCredit(long credit)
		{
			this.m_InventoryData.UpdateItemInfo(1, credit, NKM_ITEM_PAYMENT_TYPE.NIPT_FREE);
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x000BBBF5 File Offset: 0x000B9DF5
		public void SetEternium(long eternium)
		{
			this.m_InventoryData.UpdateItemInfo(2, eternium, NKM_ITEM_PAYMENT_TYPE.NIPT_FREE);
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x000BBC05 File Offset: 0x000B9E05
		public void SetInformation(long information)
		{
			this.m_InventoryData.UpdateItemInfo(3, information, NKM_ITEM_PAYMENT_TYPE.NIPT_FREE);
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x000BBC15 File Offset: 0x000B9E15
		public void SetMissionAchievePoint(long achieve_point)
		{
			this.m_MissionData.SetAchievePoint(achieve_point);
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x000BBC23 File Offset: 0x000B9E23
		public void SetDailyTicketBonus(long count)
		{
			this.m_InventoryData.UpdateItemInfo(4, count, NKM_ITEM_PAYMENT_TYPE.NIPT_FREE);
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x000BBC34 File Offset: 0x000B9E34
		public NKMUserData()
		{
			this.m_ArmyData.SetOwner(this);
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x000BBE10 File Offset: 0x000BA010
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_UserUID);
			stream.PutOrGet(ref this.m_FriendCode);
			stream.PutOrGet(ref this.m_UserNickName);
			stream.PutOrGet(ref this.m_UserLevel);
			stream.PutOrGet(ref this.m_lUserLevelEXP);
			stream.PutOrGetEnum<NKM_USER_AUTH_LEVEL>(ref this.m_eAuthLevel);
			stream.PutOrGet<NKMUserDateData>(ref this.m_NKMUserDateData);
			stream.PutOrGet<NKMInventoryData>(ref this.m_InventoryData);
			stream.PutOrGet<NKMArmyData>(ref this.m_ArmyData);
			stream.PutOrGet<NKMUserOption>(ref this.m_UserOption);
			stream.PutOrGet<NKMDungeonClearData>(ref this.m_dicNKMDungeonClearData);
			stream.PutOrGet<NKMWorldMapData>(ref this.m_WorldmapData);
			stream.PutOrGet<NKMWarfareClearData>(ref this.m_dicNKMWarfareClearData);
			stream.PutOrGet<NKMShopData>(ref this.m_ShopData);
			stream.PutOrGet<NKMUserMissionData>(ref this.m_MissionData);
			stream.PutOrGet<NKMCounterCaseData>(ref this.m_dicNKMCounterCaseData);
			stream.PutOrGet<NKMCraftData>(ref this.m_CraftData);
			stream.PutOrGet<NKMEpisodeCompleteData>(ref this.m_dicEpisodeCompleteData);
			stream.PutOrGet<PvpState>(ref this.m_PvpData);
			stream.PutOrGet<PvpHistoryList>(ref this.m_SyncPvpHistory);
			stream.PutOrGet<PvpHistoryList>(ref this.m_AsyncPvpHistory);
			stream.PutOrGet<NKMDiveGameData>(ref this.m_DiveGameData);
			stream.PutOrGet(ref this.m_DiveClearData);
			stream.PutOrGet(ref this.m_DiveHistoryData);
			stream.PutOrGet<NKMAttendanceData>(ref this.m_AttendanceData);
			stream.PutOrGetEnum<UserState>(ref this.m_UserState);
			stream.PutOrGet<NKMCompanyBuffData>(ref this.m_companyBuffDataList);
			stream.PutOrGet<NKMShadowPalace>(ref this.m_ShadowPalace);
			stream.PutOrGet<NKMBackgroundInfo>(ref this.backGroundInfo);
			stream.PutOrGet<RecallHistoryInfo>(ref this.m_RecallHistoryData);
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x000BBF85 File Offset: 0x000BA185
		public bool IsAdminUser()
		{
			return this.m_eAuthLevel == NKM_USER_AUTH_LEVEL.NORMAL_ADMIN || this.m_eAuthLevel == NKM_USER_AUTH_LEVEL.SUPER_ADMIN;
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x000BBF9C File Offset: 0x000BA19C
		public bool IsSuperUser()
		{
			return this.m_eAuthLevel == NKM_USER_AUTH_LEVEL.SUPER_USER || this.m_eAuthLevel == NKM_USER_AUTH_LEVEL.SUPER_ADMIN;
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x000BBFB3 File Offset: 0x000BA1B3
		public bool CheckDungeonClear(int dungeonID)
		{
			return dungeonID == 0 || this.m_dicNKMDungeonClearData.ContainsKey(dungeonID);
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x000BBFCC File Offset: 0x000BA1CC
		public bool CheckDungeonClear(string dungeonStrID)
		{
			if (string.IsNullOrEmpty(dungeonStrID))
			{
				return true;
			}
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(dungeonStrID);
			return dungeonTempletBase != null && this.CheckDungeonClear(dungeonTempletBase.m_DungeonID);
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x000BBFFC File Offset: 0x000BA1FC
		public bool CheckWarfareClear(string warfareStrID)
		{
			if (string.IsNullOrEmpty(warfareStrID))
			{
				return true;
			}
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(warfareStrID);
			return nkmwarfareTemplet != null && this.CheckWarfareClear(nkmwarfareTemplet.m_WarfareID);
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x000BC02B File Offset: 0x000BA22B
		public bool CheckWarfareClear(int warfareID)
		{
			return warfareID == 0 || this.m_dicNKMWarfareClearData.ContainsKey(warfareID);
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x000BC043 File Offset: 0x000BA243
		public bool CheckDiveClear(int stageID)
		{
			return this.m_DiveClearData.Contains(stageID);
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x000BC054 File Offset: 0x000BA254
		public NKMDungeonClearData GetDungeonClearData(string dungeonStrID)
		{
			if (string.IsNullOrEmpty(dungeonStrID))
			{
				return null;
			}
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(dungeonStrID);
			if (dungeonTempletBase == null)
			{
				return null;
			}
			return this.GetDungeonClearData(dungeonTempletBase.m_DungeonID);
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x000BC083 File Offset: 0x000BA283
		public NKMDungeonClearData GetDungeonClearData(int dungeonID)
		{
			if (this.m_dicNKMDungeonClearData.ContainsKey(dungeonID))
			{
				return this.m_dicNKMDungeonClearData[dungeonID];
			}
			return null;
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x000BC0A4 File Offset: 0x000BA2A4
		public void SetDungeonClearData(NKMDungeonClearData cNKMDungeonClearData)
		{
			if (cNKMDungeonClearData == null)
			{
				return;
			}
			if (cNKMDungeonClearData.dungeonId > 0)
			{
				if (this.m_dicNKMDungeonClearData.ContainsKey(cNKMDungeonClearData.dungeonId))
				{
					this.m_dicNKMDungeonClearData[cNKMDungeonClearData.dungeonId] = cNKMDungeonClearData;
					return;
				}
				this.m_dicNKMDungeonClearData.Add(cNKMDungeonClearData.dungeonId, cNKMDungeonClearData);
			}
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x000BC0F8 File Offset: 0x000BA2F8
		public NKMWarfareClearData GetWarfareClearData(string warfareStrID)
		{
			if (string.IsNullOrEmpty(warfareStrID))
			{
				return null;
			}
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(warfareStrID);
			if (nkmwarfareTemplet == null)
			{
				return null;
			}
			return this.GetWarfareClearData(nkmwarfareTemplet.m_WarfareID);
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x000BC127 File Offset: 0x000BA327
		public NKMWarfareClearData GetWarfareClearData(int warfareID)
		{
			if (this.m_dicNKMWarfareClearData.ContainsKey(warfareID))
			{
				return this.m_dicNKMWarfareClearData[warfareID];
			}
			return null;
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x000BC145 File Offset: 0x000BA345
		public bool CheckPrice(int price, int itemID)
		{
			return price >= 0 && (itemID == 0 || (long)price <= this.m_InventoryData.GetCountMiscItem(itemID));
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x000BC168 File Offset: 0x000BA368
		public void AddCounterCaseData(int dungeonID, bool unlocked)
		{
			if (!this.m_dicNKMCounterCaseData.ContainsKey(dungeonID))
			{
				NKMCounterCaseData value = new NKMCounterCaseData(dungeonID, unlocked);
				this.m_dicNKMCounterCaseData.Add(dungeonID, value);
			}
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x000BC198 File Offset: 0x000BA398
		public bool CheckUnlockedCounterCase(string dungeonStrID)
		{
			if (string.IsNullOrEmpty(dungeonStrID))
			{
				return true;
			}
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(dungeonStrID);
			return dungeonTempletBase != null && this.CheckUnlockedCounterCase(dungeonTempletBase.m_DungeonID);
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x000BC1C8 File Offset: 0x000BA3C8
		public bool CheckUnlockedCounterCase(int dungeonID)
		{
			NKMCounterCaseData nkmcounterCaseData = null;
			return this.m_dicNKMCounterCaseData.TryGetValue(dungeonID, out nkmcounterCaseData) && nkmcounterCaseData.m_Unlocked;
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x000BC1F0 File Offset: 0x000BA3F0
		public void UpdateEpisodeCompleteData(NKMEpisodeCompleteData episodeCompleteData)
		{
			if (episodeCompleteData == null)
			{
				return;
			}
			EpisodeCompleteKey episodeCompleteKey = new EpisodeCompleteKey(episodeCompleteData.m_EpisodeID, (int)episodeCompleteData.m_EpisodeDifficulty);
			NKMEpisodeCompleteData nkmepisodeCompleteData;
			if (this.m_dicEpisodeCompleteData.TryGetValue(episodeCompleteKey.m_EpisodeKey, out nkmepisodeCompleteData))
			{
				nkmepisodeCompleteData.DeepCopyFromSource(episodeCompleteData);
				return;
			}
			this.m_dicEpisodeCompleteData.Add(episodeCompleteKey.m_EpisodeKey, episodeCompleteData);
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x000BC244 File Offset: 0x000BA444
		public NKMEpisodeCompleteData GetEpisodeCompleteData(int episodeID, EPISODE_DIFFICULTY episodeDifficulty)
		{
			EpisodeCompleteKey episodeCompleteKey = new EpisodeCompleteKey(episodeID, (int)episodeDifficulty);
			NKMEpisodeCompleteData result;
			this.m_dicEpisodeCompleteData.TryGetValue(episodeCompleteKey.m_EpisodeKey, out result);
			return result;
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x000BC270 File Offset: 0x000BA470
		public int GetCounterCaseClearCount(int unitId)
		{
			List<NKMStageTempletV2> list;
			if (unitId == 0)
			{
				list = NKMEpisodeMgr.GetAllCounterCaseTemplets();
			}
			else
			{
				list = NKMEpisodeMgr.GetCounterCaseTemplets(unitId);
			}
			if (list == null || list.Count <= 0)
			{
				return 0;
			}
			int num = 0;
			foreach (NKMStageTempletV2 nkmstageTempletV in list)
			{
				switch (nkmstageTempletV.m_STAGE_TYPE)
				{
				case STAGE_TYPE.ST_WARFARE:
					if (nkmstageTempletV.WarfareTemplet != null && this.CheckWarfareClear(nkmstageTempletV.WarfareTemplet.m_WarfareID))
					{
						num++;
					}
					break;
				case STAGE_TYPE.ST_DUNGEON:
					if (nkmstageTempletV.DungeonTempletBase != null && this.CheckDungeonClear(nkmstageTempletV.DungeonTempletBase.m_DungeonID))
					{
						num++;
					}
					break;
				case STAGE_TYPE.ST_PHASE:
					Log.Error("CounterCase Can't have phase!", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUserData.cs", 1042);
					break;
				}
			}
			return num;
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x000BC354 File Offset: 0x000BA554
		public bool CanDelete(NKMUnitData unitData)
		{
			if (this.m_ArmyData.GetShipFromUID(unitData.m_UnitUID) != null)
			{
				if (this.m_ArmyData.IsShipInAnyDeck(unitData.m_UnitUID))
				{
					return false;
				}
			}
			else
			{
				if (this.m_ArmyData.GetUnitFromUID(unitData.m_UnitUID) == null)
				{
					return false;
				}
				if (this.backGroundInfo.unitInfoList.Find((NKMBackgroundUnitInfo e) => e.unitUid == unitData.m_UnitUID) != null)
				{
					return false;
				}
				if (this.m_ArmyData.IsUnitInAnyDeck(unitData.m_UnitUID))
				{
					return false;
				}
				if (unitData.GetEquipItemAccessoryUid() != 0L || unitData.GetEquipItemDefenceUid() != 0L || unitData.GetEquipItemWeaponUid() != 0L || unitData.GetEquipItemAccessory2Uid() != 0L)
				{
					return false;
				}
				using (Dictionary<int, NKMWorldMapCityData>.ValueCollection.Enumerator enumerator = this.m_WorldmapData.worldMapCityDataMap.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.leaderUnitUID == unitData.m_UnitUID)
						{
							return false;
						}
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x0600244C RID: 9292 RVA: 0x000BC494 File Offset: 0x000BA694
		public string CountryCode { get; }

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x0600244D RID: 9293 RVA: 0x000BC49C File Offset: 0x000BA69C
		public string MarketId { get; }

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x0600244E RID: 9294 RVA: 0x000BC4A4 File Offset: 0x000BA6A4
		public NKMUserProfileData UserProfileData
		{
			get
			{
				return this.m_UserProfileData;
			}
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x000BC4AC File Offset: 0x000BA6AC
		public void SetMyUserProfileInfo(NKMUserProfileData cData)
		{
			this.m_UserProfileData = cData;
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06002450 RID: 9296 RVA: 0x000BC4B5 File Offset: 0x000BA6B5
		// (set) Token: 0x06002451 RID: 9297 RVA: 0x000BC4BD File Offset: 0x000BA6BD
		public int UserLevel
		{
			get
			{
				return this.m_UserLevel;
			}
			set
			{
				this.m_UserLevel = value;
				NKMUserData.OnUserLevelUpdate onUserLevelUpdate = this.dOnUserLevelUpdate;
				if (onUserLevelUpdate == null)
				{
					return;
				}
				onUserLevelUpdate(this);
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06002452 RID: 9298 RVA: 0x000BC4D7 File Offset: 0x000BA6D7
		// (set) Token: 0x06002453 RID: 9299 RVA: 0x000BC4DF File Offset: 0x000BA6DF
		public int UserLevelEXP
		{
			get
			{
				return this.m_lUserLevelEXP;
			}
			set
			{
				this.m_lUserLevelEXP = value;
				NKMUserData.OnUserLevelUpdate onUserLevelUpdate = this.dOnUserLevelUpdate;
				if (onUserLevelUpdate == null)
				{
					return;
				}
				onUserLevelUpdate(this);
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06002454 RID: 9300 RVA: 0x000BC4F9 File Offset: 0x000BA6F9
		// (set) Token: 0x06002455 RID: 9301 RVA: 0x000BC50F File Offset: 0x000BA70F
		public NKMEventCollectionInfo EventCollectionInfo
		{
			get
			{
				if (this.m_eventCollectionInfo == null)
				{
					return new NKMEventCollectionInfo();
				}
				return this.m_eventCollectionInfo;
			}
			set
			{
				this.m_eventCollectionInfo = value;
			}
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x000BC518 File Offset: 0x000BA718
		public void SetLastPlayInfo(NKM_GAME_TYPE gameType, int stageID)
		{
			if (gameType - NKM_GAME_TYPE.NGT_PRACTICE > 1)
			{
				switch (gameType)
				{
				case NKM_GAME_TYPE.NGT_TUTORIAL:
				case NKM_GAME_TYPE.NGT_CUTSCENE:
				case NKM_GAME_TYPE.NGT_SHADOW_PALACE:
				case NKM_GAME_TYPE.NGT_FIERCE:
				case NKM_GAME_TYPE.NGT_PHASE:
					goto IL_38;
				case NKM_GAME_TYPE.NGT_RAID:
				case NKM_GAME_TYPE.NGT_WORLDMAP:
				case NKM_GAME_TYPE.NGT_ASYNC_PVP:
				case NKM_GAME_TYPE.NGT_RAID_SOLO:
					break;
				default:
					if (gameType == NKM_GAME_TYPE.NGT_TRIM)
					{
						goto IL_38;
					}
					break;
				}
				return;
			}
			IL_38:
			this.m_LastPlayInfo.gameType = (int)gameType;
			this.m_LastPlayInfo.stageId = stageID;
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06002457 RID: 9303 RVA: 0x000BC575 File Offset: 0x000BA775
		public int BackgroundID
		{
			get
			{
				if (this.backGroundInfo == null || this.backGroundInfo.backgroundItemId == 0)
				{
					return 9001;
				}
				return this.backGroundInfo.backgroundItemId;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06002458 RID: 9304 RVA: 0x000BC59D File Offset: 0x000BA79D
		public int BackgroundBGMID
		{
			get
			{
				if (this.backGroundInfo == null || this.backGroundInfo.backgroundBgmId == 0)
				{
					return 9001;
				}
				return this.backGroundInfo.backgroundBgmId;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06002459 RID: 9305 RVA: 0x000BC5C5 File Offset: 0x000BA7C5
		// (set) Token: 0x0600245A RID: 9306 RVA: 0x000BC5E5 File Offset: 0x000BA7E5
		public bool BackgroundBGMContinue
		{
			get
			{
				return PlayerPrefs.HasKey("BGM_CONTINUE_KEY") && PlayerPrefs.GetInt("BGM_CONTINUE_KEY") == 1;
			}
			set
			{
				PlayerPrefs.SetInt("BGM_CONTINUE_KEY", value ? 1 : 0);
			}
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x000BC5F8 File Offset: 0x000BA7F8
		public NKMBackgroundUnitInfo GetBackgroundUnitInfo(int index)
		{
			if (this.backGroundInfo == null)
			{
				return null;
			}
			if (this.backGroundInfo.unitInfoList == null)
			{
				return null;
			}
			if (index < this.backGroundInfo.unitInfoList.Count)
			{
				return this.backGroundInfo.unitInfoList[index];
			}
			return null;
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x000BC644 File Offset: 0x000BA844
		public int GetBackgroundUnitIndex(long uid)
		{
			if (this.backGroundInfo == null)
			{
				return -1;
			}
			if (this.backGroundInfo.unitInfoList == null)
			{
				return -1;
			}
			for (int i = 0; i < this.backGroundInfo.unitInfoList.Count; i++)
			{
				if (this.backGroundInfo.unitInfoList[i].unitUid == uid)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x000BC6A4 File Offset: 0x000BA8A4
		public bool PredictUseCash(int CashCost, out long newCash, out long newBonusCash)
		{
			long cashPaid = this.GetCashPaid();
			long cashBonus = this.GetCashBonus();
			bool flag = (long)CashCost <= cashPaid + cashBonus;
			if (!flag)
			{
				newCash = cashPaid;
				newBonusCash = cashBonus;
				return flag;
			}
			if (cashBonus >= (long)CashCost)
			{
				newBonusCash = cashBonus - (long)CashCost;
				newCash = cashPaid;
				return flag;
			}
			newCash = cashPaid + cashBonus - (long)CashCost;
			newBonusCash = 0L;
			return flag;
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x000BC6F0 File Offset: 0x000BA8F0
		public bool CheckDungeonOneTimeReward(int warfareID, int index)
		{
			NKMDungeonClearData nkmdungeonClearData;
			return warfareID == 0 || (index >= 0 && this.m_dicNKMDungeonClearData.TryGetValue(warfareID, out nkmdungeonClearData) && nkmdungeonClearData.onetimeRewardResults != null && nkmdungeonClearData.onetimeRewardResults.Count > index && nkmdungeonClearData.onetimeRewardResults[index]);
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x000BC740 File Offset: 0x000BA940
		public bool CheckWarfareOneTimeReward(int warfareID, int index)
		{
			NKMWarfareClearData nkmwarfareClearData;
			return warfareID == 0 || (index >= 0 && this.m_dicNKMWarfareClearData.TryGetValue(warfareID, out nkmwarfareClearData) && nkmwarfareClearData.m_OnetimeRewardResults != null && nkmwarfareClearData.m_OnetimeRewardResults.Count > index && nkmwarfareClearData.m_OnetimeRewardResults[index]);
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x000BC78E File Offset: 0x000BA98E
		public void GetReward(NKMRewardData rewardData, NKMAdditionalReward additionalRewardData)
		{
			this.GetReward(rewardData);
			this.GetReward(additionalRewardData);
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x000BC7A0 File Offset: 0x000BA9A0
		public void GetReward(NKMRewardData rewardData)
		{
			if (rewardData == null)
			{
				return;
			}
			int futureUserLevel = NKCExpManager.GetFutureUserLevel(this, rewardData.UserExp);
			if (futureUserLevel > this.UserLevel)
			{
				NKCContentManager.SetLevelChanged(true);
				NKCPublisherModule.Statistics.OnUserLevelUp(futureUserLevel);
			}
			this.UserLevelEXP = NKCExpManager.GetFutureUserRemainEXP(this, rewardData.UserExp);
			this.UserLevel = futureUserLevel;
			if (rewardData.UnitDataList != null)
			{
				foreach (NKMUnitData nkmunitData in rewardData.UnitDataList)
				{
					switch (NKMUnitManager.GetUnitTempletBase(nkmunitData.m_UnitID).m_NKM_UNIT_TYPE)
					{
					case NKM_UNIT_TYPE.NUT_NORMAL:
						this.m_ArmyData.AddNewUnit(nkmunitData);
						continue;
					case NKM_UNIT_TYPE.NUT_SHIP:
						this.m_ArmyData.AddNewShip(nkmunitData);
						continue;
					}
					Log.Error("Undefined Unittype, unitID : " + nkmunitData.m_UnitID.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMUserDataEx.cs", 487);
				}
			}
			if (rewardData.ContractList != null)
			{
				foreach (MiscContractResult miscContractResult in rewardData.contractList)
				{
					if (miscContractResult != null)
					{
						foreach (NKMUnitData nkmunitData2 in miscContractResult.units)
						{
							switch (NKMUnitManager.GetUnitTempletBase(nkmunitData2.m_UnitID).m_NKM_UNIT_TYPE)
							{
							case NKM_UNIT_TYPE.NUT_NORMAL:
								this.m_ArmyData.AddNewUnit(nkmunitData2);
								continue;
							case NKM_UNIT_TYPE.NUT_SHIP:
								this.m_ArmyData.AddNewShip(nkmunitData2);
								continue;
							}
							Log.Error("Undefined Unittype, unitID : " + nkmunitData2.m_UnitID.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMUserDataEx.cs", 514);
						}
					}
				}
			}
			if (rewardData.OperatorList != null)
			{
				foreach (NKMOperator newOperator in rewardData.OperatorList)
				{
					this.m_ArmyData.AddNewOperator(newOperator);
				}
			}
			if (rewardData.MiscItemDataList != null)
			{
				foreach (NKMItemMiscData miscItemData in rewardData.MiscItemDataList)
				{
					this.m_InventoryData.AddItemMisc(miscItemData);
				}
			}
			if (rewardData.EquipItemDataList != null)
			{
				foreach (NKMEquipItemData equip_item_data in rewardData.EquipItemDataList)
				{
					this.m_InventoryData.AddItemEquip(equip_item_data);
				}
			}
			if (rewardData.UnitExpDataList != null)
			{
				foreach (NKMRewardUnitExpData nkmrewardUnitExpData in rewardData.UnitExpDataList)
				{
					NKMUnitData unitFromUID = this.m_ArmyData.GetUnitFromUID(nkmrewardUnitExpData.m_UnitUid);
					if (unitFromUID != null)
					{
						NKCExpManager.CalculateFutureUnitExpAndLevel(unitFromUID, nkmrewardUnitExpData.m_Exp + nkmrewardUnitExpData.m_BonusExp, out unitFromUID.m_UnitLevel, out unitFromUID.m_iUnitLevelEXP);
					}
				}
			}
			if (rewardData.SkinIdList != null)
			{
				foreach (int skinID in rewardData.SkinIdList)
				{
					this.m_InventoryData.AddItemSkin(skinID);
				}
			}
			if (rewardData.MoldItemDataList != null)
			{
				foreach (NKMMoldItemData moldItemData in rewardData.MoldItemDataList)
				{
					this.m_CraftData.AddMoldItem(moldItemData);
				}
			}
			if (rewardData.CompanyBuffDataList != null)
			{
				foreach (NKMCompanyBuffData data in rewardData.CompanyBuffDataList)
				{
					NKCCompanyBuff.UpsertCompanyBuffData(this.m_companyBuffDataList, data);
				}
			}
			if (rewardData.EmoticonList != null && NKCEmoticonManager.m_bReceivedEmoticonData)
			{
				foreach (int item in rewardData.EmoticonList)
				{
					NKCEmoticonManager.m_hsEmoticonCollection.Add(item);
				}
			}
			if (rewardData.BingoTileList != null)
			{
				foreach (NKMBingoTile nkmbingoTile in rewardData.BingoTileList)
				{
					EventBingo bingoData = NKMEventManager.GetBingoData(nkmbingoTile.eventId);
					if (bingoData != null)
					{
						bingoData.MarkToLine(nkmbingoTile.tileIndex);
					}
				}
			}
			if (rewardData.Interiors != null)
			{
				this.OfficeData.AddInteriorData(rewardData.Interiors);
			}
			if (rewardData.AchievePoint > 0L)
			{
				this.m_MissionData.AddAchievePoint(rewardData.AchievePoint);
			}
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x000BCD04 File Offset: 0x000BAF04
		public void GetReward(NKMAdditionalReward rewardData)
		{
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x000BCD06 File Offset: 0x000BAF06
		public bool CheckDiveHistory(int stageID)
		{
			return this.m_DiveHistoryData.Contains(stageID);
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x000BCD14 File Offset: 0x000BAF14
		public bool IsCurrentDiveGameIsWorldmapEventDive()
		{
			return this.m_DiveGameData != null && this.m_WorldmapData.GetCityIDByEventData(NKM_WORLDMAP_EVENT_TYPE.WET_DIVE, this.m_DiveGameData.DiveUid) >= 0;
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x000BCD40 File Offset: 0x000BAF40
		public void SetDungeonClearDataOnlyTrue(NKMDungeonClearData cNKMDungeonClearData)
		{
			if (cNKMDungeonClearData == null)
			{
				return;
			}
			if (cNKMDungeonClearData.dungeonId > 0)
			{
				if (this.m_dicNKMDungeonClearData.ContainsKey(cNKMDungeonClearData.dungeonId))
				{
					NKMDungeonClearData nkmdungeonClearData = this.m_dicNKMDungeonClearData[cNKMDungeonClearData.dungeonId];
					if (cNKMDungeonClearData.missionResult1)
					{
						nkmdungeonClearData.missionResult1 = true;
					}
					if (cNKMDungeonClearData.missionResult2)
					{
						nkmdungeonClearData.missionResult2 = true;
					}
					if (cNKMDungeonClearData.onetimeRewardResults != null)
					{
						for (int i = 0; i < cNKMDungeonClearData.onetimeRewardResults.Count; i++)
						{
							if (cNKMDungeonClearData.onetimeRewardResults[i] && i < nkmdungeonClearData.onetimeRewardResults.Count)
							{
								nkmdungeonClearData.onetimeRewardResults[i] = true;
							}
						}
						return;
					}
				}
				else
				{
					this.m_dicNKMDungeonClearData.Add(cNKMDungeonClearData.dungeonId, cNKMDungeonClearData);
					NKCContentManager.SetUnlockedContent(STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DUNGEON, cNKMDungeonClearData.dungeonId);
				}
			}
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x000BCE08 File Offset: 0x000BB008
		public void SetWarfareClearDataOnlyTrue(NKMWarfareClearData cNKMWarfareClearData)
		{
			if (cNKMWarfareClearData == null)
			{
				return;
			}
			if (cNKMWarfareClearData.m_WarfareID > 0)
			{
				if (this.m_dicNKMWarfareClearData.ContainsKey(cNKMWarfareClearData.m_WarfareID))
				{
					NKMWarfareClearData nkmwarfareClearData = this.m_dicNKMWarfareClearData[cNKMWarfareClearData.m_WarfareID];
					if (cNKMWarfareClearData.m_mission_result_1)
					{
						nkmwarfareClearData.m_mission_result_1 = true;
					}
					if (cNKMWarfareClearData.m_mission_result_2)
					{
						nkmwarfareClearData.m_mission_result_2 = true;
					}
					if (cNKMWarfareClearData.m_OnetimeRewardResults != null)
					{
						for (int i = 0; i < cNKMWarfareClearData.m_OnetimeRewardResults.Count; i++)
						{
							if (cNKMWarfareClearData.m_OnetimeRewardResults[i] && i < nkmwarfareClearData.m_OnetimeRewardResults.Count)
							{
								nkmwarfareClearData.m_OnetimeRewardResults[i] = true;
							}
						}
						return;
					}
				}
				else
				{
					this.m_dicNKMWarfareClearData.Add(cNKMWarfareClearData.m_WarfareID, cNKMWarfareClearData);
				}
			}
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x000BCEC2 File Offset: 0x000BB0C2
		public void ClearDiveGameData()
		{
			this.m_DiveGameData = null;
			this.m_ArmyData.ResetDeckStateOf(NKM_DECK_STATE.DECK_STATE_DIVE);
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x000BCED7 File Offset: 0x000BB0D7
		public void SetEquipTuningData(NKMEquipTuningCandidate tuningData)
		{
			this.m_EquipTuningCandidate = tuningData;
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x000BCEE0 File Offset: 0x000BB0E0
		public bool IsPossibleTuning(long itemUID, int idx = 0)
		{
			if (itemUID != this.m_EquipTuningCandidate.equipUid)
			{
				return false;
			}
			if (idx == 0)
			{
				return this.m_EquipTuningCandidate.option1 != NKM_STAT_TYPE.NST_RANDOM;
			}
			return idx == 1 && this.m_EquipTuningCandidate.option2 != NKM_STAT_TYPE.NST_RANDOM;
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x000BCF1E File Offset: 0x000BB11E
		public NKMEquipTuningCandidate GetTuiningData()
		{
			return this.m_EquipTuningCandidate;
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x000BCF28 File Offset: 0x000BB128
		public bool hasReservedEquipCandidate()
		{
			if (this.m_EquipTuningCandidate != null)
			{
				if (!this.m_InventoryData.ItemEquipData.ContainsKey(this.m_EquipTuningCandidate.equipUid))
				{
					return false;
				}
				if (this.m_EquipTuningCandidate.option1 != NKM_STAT_TYPE.NST_RANDOM || this.m_EquipTuningCandidate.option2 != NKM_STAT_TYPE.NST_RANDOM)
				{
					return true;
				}
				if (this.m_EquipTuningCandidate.setOptionId != 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x000BCF8A File Offset: 0x000BB18A
		public bool hasReservedEquipTuningData()
		{
			return (this.m_EquipTuningCandidate != null && this.m_EquipTuningCandidate.option1 != NKM_STAT_TYPE.NST_RANDOM) || this.m_EquipTuningCandidate.option2 != NKM_STAT_TYPE.NST_RANDOM;
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x000BCFB3 File Offset: 0x000BB1B3
		public void SetShipCandidateData(NKMShipModuleCandidate candidateData)
		{
			this.m_ShipCmdModuleCandidate = candidateData;
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x000BCFBC File Offset: 0x000BB1BC
		public NKMShipModuleCandidate GetShipCandidateData()
		{
			return this.m_ShipCmdModuleCandidate;
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x000BCFC4 File Offset: 0x000BB1C4
		public bool HaveEnoughResourceToBuy(ShopItemTemplet productTemplet, int ProductCount)
		{
			if (productTemplet == null)
			{
				return false;
			}
			int realPrice = this.m_ShopData.GetRealPrice(productTemplet, ProductCount, false);
			return this.CheckPrice(realPrice, productTemplet.m_PriceItemID);
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x000BCFF4 File Offset: 0x000BB1F4
		public bool HasBuff(int buffId)
		{
			if (this.m_companyBuffDataList != null)
			{
				for (int i = 0; i < this.m_companyBuffDataList.Count; i++)
				{
					if (this.m_companyBuffDataList[i].Id == buffId)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x000BD038 File Offset: 0x000BB238
		public bool HasBuffGroup(int groupId)
		{
			if (this.m_companyBuffDataList != null)
			{
				int i;
				Func<GuildWelfareTemplet, bool> <>9__0;
				int j;
				for (i = 0; i < this.m_companyBuffDataList.Count; i = j + 1)
				{
					Func<GuildWelfareTemplet, bool> predicate;
					if ((predicate = <>9__0) == null)
					{
						predicate = (<>9__0 = ((GuildWelfareTemplet x) => x.CompanyBuffID == this.m_companyBuffDataList[i].Id));
					}
					GuildWelfareTemplet guildWelfareTemplet = NKMTempletContainer<GuildWelfareTemplet>.Find(predicate);
					if (guildWelfareTemplet != null && guildWelfareTemplet.CompanyBuffGroupID == groupId)
					{
						return true;
					}
					j = i;
				}
			}
			return false;
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x000BD0C0 File Offset: 0x000BB2C0
		public DateTime GetBuffExpireTimeByGroupId(int buffGroupId)
		{
			if (this.HasBuffGroup(buffGroupId))
			{
				int i;
				Func<GuildWelfareTemplet, bool> <>9__0;
				int j;
				for (i = 0; i < this.m_companyBuffDataList.Count; i = j + 1)
				{
					Func<GuildWelfareTemplet, bool> predicate;
					if ((predicate = <>9__0) == null)
					{
						predicate = (<>9__0 = ((GuildWelfareTemplet x) => x.CompanyBuffID == this.m_companyBuffDataList[i].Id));
					}
					GuildWelfareTemplet guildWelfareTemplet = NKMTempletContainer<GuildWelfareTemplet>.Find(predicate);
					if (guildWelfareTemplet != null && guildWelfareTemplet.CompanyBuffGroupID == buffGroupId)
					{
						return this.m_companyBuffDataList[i].ExpireDate;
					}
					j = i;
				}
			}
			return default(DateTime);
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x000BD164 File Offset: 0x000BB364
		public long GetEterniumCap()
		{
			return NKCExpManager.GetUserExpTemplet(this).m_EterniumCap;
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x000BD174 File Offset: 0x000BB374
		public float GetEterniumCapProgress()
		{
			float num = (float)this.GetEternium();
			float num2 = (float)this.GetEterniumCap();
			if (num >= num2 || num2 == 0f)
			{
				return 1f;
			}
			return num / num2;
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06002475 RID: 9333 RVA: 0x000BD1A6 File Offset: 0x000BB3A6
		public DateTime lastAsyncTicketUpdateDate
		{
			get
			{
				return this.m_lastUpdateAsyncTicket;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06002476 RID: 9334 RVA: 0x000BD1AE File Offset: 0x000BB3AE
		public DateTime lastEterniumUpdateDate
		{
			get
			{
				return this.m_lastUpdateEterniumCap;
			}
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x000BD1B6 File Offset: 0x000BB3B6
		public void SetUpdateDate(NKMPacket_CHARGE_ITEM_NOT sPacket)
		{
			if (sPacket.itemData.ItemID == 2)
			{
				this.m_lastUpdateEterniumCap = sPacket.lastUpdateDate;
				return;
			}
			if (sPacket.itemData.ItemID == 13)
			{
				this.m_lastUpdateAsyncTicket = sPacket.lastUpdateDate;
			}
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x000BD1F0 File Offset: 0x000BB3F0
		public void SetReturningUserStates(List<NKMReturningUserState> lstStates)
		{
			this.m_dicReturningUserState.Clear();
			for (int i = 0; i < lstStates.Count; i++)
			{
				if (!this.m_dicReturningUserState.ContainsKey(lstStates[i].type))
				{
					this.m_dicReturningUserState.Add(lstStates[i].type, lstStates[i]);
				}
				else
				{
					Log.Error(string.Format("{0} is already exist!!", lstStates[i].type), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMUserDataEx.cs", 894);
				}
			}
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x000BD27C File Offset: 0x000BB47C
		public DateTime GetReturnStartDate(ReturningUserType state)
		{
			if (state == ReturningUserType.None)
			{
				return default(DateTime);
			}
			if (!this.m_dicReturningUserState.ContainsKey(state))
			{
				return default(DateTime);
			}
			return this.m_dicReturningUserState[state].startDate;
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x000BD2C0 File Offset: 0x000BB4C0
		public DateTime GetReturnEndDate(ReturningUserType state)
		{
			if (state == ReturningUserType.None)
			{
				return default(DateTime);
			}
			if (!this.m_dicReturningUserState.ContainsKey(state))
			{
				return default(DateTime);
			}
			return this.m_dicReturningUserState[state].endDate;
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x000BD304 File Offset: 0x000BB504
		public bool IsReturnUser()
		{
			foreach (NKMReturningUserState nkmreturningUserState in this.m_dicReturningUserState.Values)
			{
				if (NKCSynchronizedTime.IsEventTime(nkmreturningUserState.startDate, nkmreturningUserState.endDate))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x000BD370 File Offset: 0x000BB570
		public bool IsReturnUser(ReturningUserType type)
		{
			NKMReturningUserState nkmreturningUserState;
			return this.m_dicReturningUserState.TryGetValue(type, out nkmreturningUserState) && NKCSynchronizedTime.IsEventTime(nkmreturningUserState.startDate, nkmreturningUserState.endDate);
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x000BD3A3 File Offset: 0x000BB5A3
		public bool IsNewbieUser(int newbieDay)
		{
			return NKCSynchronizedTime.GetServerUTCTime(0.0) <= this.GetNewbieEndDate(newbieDay);
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x000BD3BF File Offset: 0x000BB5BF
		public DateTime GetNewbieEndDate(int newbieDay)
		{
			return this.m_NKMUserDateData.m_RegisterTime.AddHours((double)(newbieDay * 24));
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x000BD3D8 File Offset: 0x000BB5D8
		public void SetStagePlayData(List<NKMStagePlayData> lstStagePlayData)
		{
			if (lstStagePlayData == null)
			{
				return;
			}
			this.m_dicStagePlayData.Clear();
			foreach (NKMStagePlayData nkmstagePlayData in lstStagePlayData)
			{
				this.m_dicStagePlayData.Add(nkmstagePlayData.stageId, nkmstagePlayData);
			}
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x000BD440 File Offset: 0x000BB640
		public void UpdateStagePlayData(NKMStagePlayData stagePlayData)
		{
			if (stagePlayData == null)
			{
				return;
			}
			this.CheckFierceDailyRewardReset(stagePlayData);
			this.m_dicStagePlayData[stagePlayData.stageId] = stagePlayData;
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WARFARE_GAME)
			{
				NKC_SCEN_WARFARE_GAME nkc_SCEN_WARFARE_GAME = NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME();
				if (nkc_SCEN_WARFARE_GAME != null)
				{
					NKCWarfareGame warfareGame = nkc_SCEN_WARFARE_GAME.GetWarfareGame();
					if (warfareGame != null)
					{
						warfareGame.m_NKCWarfareGameHUD.UpdateStagePlayState();
						warfareGame.m_NKCWarfareGameHUD.SelfUpdateAttackCost();
					}
				}
			}
			if (NKCUIPrepareEventDeck.IsInstanceOpen)
			{
				NKCUIPrepareEventDeck.Instance.UpdateEnterLimitUI();
			}
			if (NKCUIDeckViewer.IsInstanceOpen)
			{
				NKCUIDeckViewer.Instance.UpdateEnterLimitUI();
			}
			if (NKCPopupFavorite.isOpen())
			{
				NKCPopupFavorite.Instance.RefreshData();
			}
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x000BD4DF File Offset: 0x000BB6DF
		public bool IsHaveStatePlayData(int stageID)
		{
			return this.m_dicStagePlayData.ContainsKey(stageID);
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x000BD4F0 File Offset: 0x000BB6F0
		public int GetStatePlayCnt(int stageID, bool IsServiceTime = false, bool bSkipNextResetData = false, bool bTotalCnt = false)
		{
			NKMStagePlayData nkmstagePlayData;
			if (!this.m_dicStagePlayData.TryGetValue(stageID, out nkmstagePlayData))
			{
				return 0;
			}
			DateTime nextResetDate = nkmstagePlayData.nextResetDate;
			if (!bSkipNextResetData)
			{
				DateTime finishTime = nkmstagePlayData.nextResetDate;
				if (IsServiceTime)
				{
					finishTime = NKMTime.LocalToUTC(nkmstagePlayData.nextResetDate, 0);
				}
				if (NKCSynchronizedTime.GetTimeLeft(finishTime).Ticks <= 0L)
				{
					return 0;
				}
			}
			if (!bTotalCnt)
			{
				return (int)nkmstagePlayData.playCount;
			}
			return (int)nkmstagePlayData.totalPlayCount;
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x000BD558 File Offset: 0x000BB758
		private void CheckFierceDailyRewardReset(NKMStagePlayData stagePlayData)
		{
			if (stagePlayData == null)
			{
				return;
			}
			if (stagePlayData.stageId == NKMFierceConst.StageId && this.m_dicStagePlayData.ContainsKey(NKMFierceConst.StageId) && this.m_dicStagePlayData[stagePlayData.stageId].nextResetDate != stagePlayData.nextResetDate)
			{
				NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr().SetDailyRewardReceived(false);
			}
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x000BD5BC File Offset: 0x000BB7BC
		public long GetStageKillCountBest(int stageID)
		{
			NKMStagePlayData nkmstagePlayData;
			if (this.m_dicStagePlayData.TryGetValue(stageID, out nkmstagePlayData))
			{
				return nkmstagePlayData.bestKillCount;
			}
			return 0L;
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x000BD5E4 File Offset: 0x000BB7E4
		public int GetStageRestoreCnt(int stageID)
		{
			NKMStagePlayData nkmstagePlayData;
			if (!this.m_dicStagePlayData.TryGetValue(stageID, out nkmstagePlayData) || nkmstagePlayData == null)
			{
				return 0;
			}
			DateTime nextResetDate = nkmstagePlayData.nextResetDate;
			if (NKCSynchronizedTime.GetTimeLeft(nkmstagePlayData.nextResetDate).Ticks <= 0L)
			{
				return 0;
			}
			return (int)nkmstagePlayData.restoreCount;
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x000BD630 File Offset: 0x000BB830
		public int GetStageBestClearSec(int stageID)
		{
			NKMStagePlayData nkmstagePlayData;
			if (!this.m_dicStagePlayData.TryGetValue(stageID, out nkmstagePlayData) || nkmstagePlayData == null)
			{
				return 0;
			}
			DateTime nextResetDate = nkmstagePlayData.nextResetDate;
			if (nkmstagePlayData.nextResetDate.Ticks > 0L && NKCSynchronizedTime.GetTimeLeft(nkmstagePlayData.nextResetDate).Ticks != 0L)
			{
				return 0;
			}
			return nkmstagePlayData.bestClearTimeSec;
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x000BD688 File Offset: 0x000BB888
		public bool CheckStageCleared(int stageID)
		{
			NKMStageTempletV2 stageTemplet = NKMStageTempletV2.Find(stageID);
			return this.CheckStageCleared(stageTemplet);
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x000BD6A4 File Offset: 0x000BB8A4
		public bool CheckStageCleared(NKMGameData gameData)
		{
			if (gameData == null)
			{
				return false;
			}
			NKM_GAME_TYPE gameType = gameData.GetGameType();
			if (gameType != NKM_GAME_TYPE.NGT_DUNGEON)
			{
				if (gameType == NKM_GAME_TYPE.NGT_WARFARE)
				{
					return this.CheckWarfareClear(gameData.m_WarfareID);
				}
				if (gameType == NKM_GAME_TYPE.NGT_PHASE)
				{
					return NKCPhaseManager.IsCurrentPhaseDungeon(gameData.m_DungeonID) && NKCPhaseManager.CheckPhaseStageClear(NKCPhaseManager.GetStageTemplet());
				}
			}
			return this.CheckDungeonClear(gameData.m_DungeonID);
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x000BD700 File Offset: 0x000BB900
		public bool CheckStageCleared(NKMStageTempletV2 stageTemplet)
		{
			if (stageTemplet == null)
			{
				return false;
			}
			switch (stageTemplet.m_STAGE_TYPE)
			{
			case STAGE_TYPE.ST_WARFARE:
				return this.CheckWarfareClear(stageTemplet.WarfareTemplet.m_WarfareID);
			case STAGE_TYPE.ST_DUNGEON:
				return this.CheckDungeonClear(stageTemplet.DungeonTempletBase.m_DungeonID);
			case STAGE_TYPE.ST_PHASE:
				return NKCPhaseManager.CheckPhaseStageClear(stageTemplet);
			default:
				return false;
			}
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x000BD75C File Offset: 0x000BB95C
		public bool CheckPhaseClear(int phaseID)
		{
			if (phaseID == 0)
			{
				return true;
			}
			NKMStageTempletV2 nkmstageTempletV = NKMStageTempletV2.Find(phaseID);
			return nkmstageTempletV != null && nkmstageTempletV.m_STAGE_TYPE == STAGE_TYPE.ST_PHASE && NKCPhaseManager.CheckPhaseStageClear(nkmstageTempletV);
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x000BD78B File Offset: 0x000BB98B
		public bool hasReservedSetOptionData()
		{
			return this.m_EquipTuningCandidate != null && this.m_EquipTuningCandidate.setOptionId != 0;
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x000BD7A5 File Offset: 0x000BB9A5
		public int GetReservedSetOption(long ItemUID)
		{
			if (this.m_EquipTuningCandidate != null && this.m_EquipTuningCandidate.equipUid == ItemUID)
			{
				return this.m_EquipTuningCandidate.setOptionId;
			}
			return 0;
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x0600248D RID: 9357 RVA: 0x000BD7CA File Offset: 0x000BB9CA
		public NKMUserData.strMentoringData MentoringData
		{
			get
			{
				return this.m_MyMentoringData;
			}
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x000BD7D2 File Offset: 0x000BB9D2
		public void UpdateMyMenteeMatchList(List<MenteeInfo> lstMatch)
		{
			this.m_MyMentoringData.lstMenteeMatch = lstMatch;
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x000BD7E0 File Offset: 0x000BB9E0
		public void UpdateReceiveList(List<FriendListData> recommend, List<FriendListData> invited = null)
		{
			this.m_MyMentoringData.lstRecommend = recommend;
			this.m_MyMentoringData.lstInvited = invited;
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x000BD7FA File Offset: 0x000BB9FA
		public void UpdateMyMentor(FriendListData myMentor, bool bGraduate = false)
		{
			this.m_MyMentoringData.MyMentor = myMentor;
			this.m_MyMentoringData.bMenteeGraduate = bGraduate;
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x000BD814 File Offset: 0x000BBA14
		public void UpdateMentoringSeasonID(int seasonID)
		{
			this.m_MyMentoringData.SeasonId = seasonID;
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x000BD824 File Offset: 0x000BBA24
		public void DeleteMentee(long deleteMenteeUID)
		{
			foreach (MenteeInfo menteeInfo in this.m_MyMentoringData.lstMenteeMatch)
			{
				if (menteeInfo.data.commonProfile.userUid == deleteMenteeUID)
				{
					this.m_MyMentoringData.lstMenteeMatch.Remove(menteeInfo);
					break;
				}
			}
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x000BD89C File Offset: 0x000BBA9C
		public void DeleteRecommandMentee(FriendListData menteeInfo)
		{
			if (this.m_MyMentoringData.lstRecommend != null)
			{
				foreach (FriendListData friendListData in this.m_MyMentoringData.lstRecommend)
				{
					if (friendListData.commonProfile.userUid == menteeInfo.commonProfile.userUid)
					{
						this.m_MyMentoringData.lstRecommend.Remove(friendListData);
						break;
					}
				}
			}
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x000BD928 File Offset: 0x000BBB28
		public int GetMenteeMissionCompletCnt()
		{
			int num = 0;
			using (List<MenteeInfo>.Enumerator enumerator = this.m_MyMentoringData.lstMenteeMatch.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.state == MentoringState.Graduated)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06002495 RID: 9365 RVA: 0x000BD988 File Offset: 0x000BBB88
		public void DeleteInvitedMentor(long deleteMentorUID)
		{
			if (this.m_MyMentoringData.lstInvited != null)
			{
				foreach (FriendListData friendListData in this.m_MyMentoringData.lstInvited)
				{
					if (friendListData.commonProfile.userUid == deleteMentorUID)
					{
						this.m_MyMentoringData.lstInvited.Remove(friendListData);
						break;
					}
				}
			}
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x000BDA08 File Offset: 0x000BBC08
		public string GetMenteeName(long menteeUID)
		{
			string result = "";
			foreach (MenteeInfo menteeInfo in this.m_MyMentoringData.lstMenteeMatch)
			{
				if (menteeInfo.data.commonProfile.userUid == menteeUID)
				{
					result = menteeInfo.data.commonProfile.nickname;
					break;
				}
			}
			return result;
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x000BDA88 File Offset: 0x000BBC88
		public string GetMentorName(long _mentorUID)
		{
			string text = "";
			if (this.m_MyMentoringData.lstInvited != null)
			{
				foreach (FriendListData friendListData in this.m_MyMentoringData.lstInvited)
				{
					if (friendListData.commonProfile.userUid == _mentorUID)
					{
						text = friendListData.commonProfile.nickname;
						break;
					}
				}
			}
			if (string.IsNullOrEmpty(text) && this.m_MyMentoringData.lstRecommend != null)
			{
				foreach (FriendListData friendListData2 in this.m_MyMentoringData.lstRecommend)
				{
					if (friendListData2.commonProfile.userUid == _mentorUID)
					{
						text = friendListData2.commonProfile.nickname;
						break;
					}
				}
			}
			return text;
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x000BDB7C File Offset: 0x000BBD7C
		public void SetMentoringNotify(bool bSet)
		{
			this.m_MyMentoringData.bMentoringNotify = bSet;
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x000BDB8A File Offset: 0x000BBD8A
		public void UpdateConsumerPackageData(NKMConsumerPackageData packageData)
		{
			if (packageData == null)
			{
				return;
			}
			this.m_dicConsumerPackageData[packageData.productId] = packageData;
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x000BDBA4 File Offset: 0x000BBDA4
		public void UpdateConsumerPackageData(IEnumerable<NKMConsumerPackageData> lstPackageData)
		{
			if (lstPackageData == null)
			{
				return;
			}
			foreach (NKMConsumerPackageData nkmconsumerPackageData in lstPackageData)
			{
				this.m_dicConsumerPackageData[nkmconsumerPackageData.productId] = nkmconsumerPackageData;
			}
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x000BDBFC File Offset: 0x000BBDFC
		public bool GetConsumerPackageData(int productID, out NKMConsumerPackageData data)
		{
			return this.m_dicConsumerPackageData.TryGetValue(productID, out data);
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x000BDC0B File Offset: 0x000BBE0B
		public void RemoveConsumerPackageData(int productID)
		{
			this.m_dicConsumerPackageData.Remove(productID);
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x000BDC1C File Offset: 0x000BBE1C
		public void RemoveConsumerPackageData(IEnumerable<int> lstProductID)
		{
			if (lstProductID == null)
			{
				return;
			}
			foreach (int key in lstProductID)
			{
				this.m_dicConsumerPackageData.Remove(key);
			}
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x000BDC70 File Offset: 0x000BBE70
		public bool IsKakaoMissionOngoing()
		{
			return this.kakaoMissionData != null && this.kakaoMissionData.state != KakaoMissionState.OutOfDate;
		}

		// Token: 0x040025DE RID: 9694
		public List<NKMCompanyBuffData> m_companyBuffDataList = new List<NKMCompanyBuffData>();

		// Token: 0x040025DF RID: 9695
		public long m_UserUID;

		// Token: 0x040025E0 RID: 9696
		public long m_FriendCode;

		// Token: 0x040025E1 RID: 9697
		public string m_UserNickName = "";

		// Token: 0x040025E2 RID: 9698
		public NKM_PUBLISHER_TYPE m_NKM_PUBLISHER_TYPE;

		// Token: 0x040025E3 RID: 9699
		public UserState m_UserState;

		// Token: 0x040025E4 RID: 9700
		public NKM_USER_AUTH_LEVEL m_eAuthLevel = NKM_USER_AUTH_LEVEL.NORMAL_USER;

		// Token: 0x040025E5 RID: 9701
		public NKMUserDateData m_NKMUserDateData = new NKMUserDateData();

		// Token: 0x040025E6 RID: 9702
		public NKMInventoryData m_InventoryData = new NKMInventoryData();

		// Token: 0x040025E7 RID: 9703
		public NKMArmyData m_ArmyData = new NKMArmyData();

		// Token: 0x040025E8 RID: 9704
		public NKMUserOption m_UserOption = new NKMUserOption();

		// Token: 0x040025E9 RID: 9705
		public Dictionary<int, NKMDungeonClearData> m_dicNKMDungeonClearData = new Dictionary<int, NKMDungeonClearData>();

		// Token: 0x040025EA RID: 9706
		public NKMWorldMapData m_WorldmapData = new NKMWorldMapData();

		// Token: 0x040025EB RID: 9707
		public Dictionary<int, NKMWarfareClearData> m_dicNKMWarfareClearData = new Dictionary<int, NKMWarfareClearData>();

		// Token: 0x040025EC RID: 9708
		public NKMShopData m_ShopData = new NKMShopData();

		// Token: 0x040025ED RID: 9709
		public NKMUserMissionData m_MissionData = new NKMUserMissionData();

		// Token: 0x040025EE RID: 9710
		public Dictionary<int, NKMCounterCaseData> m_dicNKMCounterCaseData = new Dictionary<int, NKMCounterCaseData>();

		// Token: 0x040025EF RID: 9711
		public NKMCraftData m_CraftData = new NKMCraftData();

		// Token: 0x040025F0 RID: 9712
		public NKMEquipTuningCandidate m_EquipTuningCandidate = new NKMEquipTuningCandidate();

		// Token: 0x040025F1 RID: 9713
		public NKMShipModuleCandidate m_ShipCmdModuleCandidate = new NKMShipModuleCandidate();

		// Token: 0x040025F2 RID: 9714
		public Dictionary<long, NKMEpisodeCompleteData> m_dicEpisodeCompleteData = new Dictionary<long, NKMEpisodeCompleteData>();

		// Token: 0x040025F3 RID: 9715
		public PvpState m_PvpData = new PvpState();

		// Token: 0x040025F4 RID: 9716
		public PvpHistoryList m_SyncPvpHistory = new PvpHistoryList();

		// Token: 0x040025F5 RID: 9717
		public PvpHistoryList m_AsyncPvpHistory = new PvpHistoryList();

		// Token: 0x040025F6 RID: 9718
		public NKMDiveGameData m_DiveGameData;

		// Token: 0x040025F7 RID: 9719
		public HashSet<int> m_DiveClearData = new HashSet<int>();

		// Token: 0x040025F8 RID: 9720
		public HashSet<int> m_DiveHistoryData = new HashSet<int>();

		// Token: 0x040025F9 RID: 9721
		public NKMAttendanceData m_AttendanceData = new NKMAttendanceData();

		// Token: 0x040025FA RID: 9722
		public NKMShadowPalace m_ShadowPalace = new NKMShadowPalace();

		// Token: 0x040025FB RID: 9723
		public NKMBackgroundInfo backGroundInfo = new NKMBackgroundInfo();

		// Token: 0x040025FC RID: 9724
		public Dictionary<int, RecallHistoryInfo> m_RecallHistoryData = new Dictionary<int, RecallHistoryInfo>();

		// Token: 0x040025FD RID: 9725
		public int m_UserLevel = 1;

		// Token: 0x040025FE RID: 9726
		public int m_lUserLevelEXP;

		// Token: 0x040025FF RID: 9727
		public NKMUserData.OnUserLevelUpdate dOnUserLevelUpdate;

		// Token: 0x04002600 RID: 9728
		public PvpState m_AsyncData = new PvpState();

		// Token: 0x04002601 RID: 9729
		public PvpState m_LeagueData = new PvpState();

		// Token: 0x04002602 RID: 9730
		public NpcPvpData m_NpcData = new NpcPvpData();

		// Token: 0x04002603 RID: 9731
		public bool m_enableAccountLink;

		// Token: 0x04002604 RID: 9732
		public bool m_RankOpen;

		// Token: 0x04002605 RID: 9733
		public bool m_LeagueOpen;

		// Token: 0x04002606 RID: 9734
		public DateTime LastPvpPointChargeTimeUTC;

		// Token: 0x04002607 RID: 9735
		public NKMUserData.OnCompanyBuffUpdate dOnCompanyBuffUpdate;

		// Token: 0x04002608 RID: 9736
		public DateTime m_GuildJoinDisableTime;

		// Token: 0x04002609 RID: 9737
		public PvpHistoryList m_LeaguePvpHistory = new PvpHistoryList();

		// Token: 0x0400260A RID: 9738
		public PvpHistoryList m_PrivatePvpHistory = new PvpHistoryList();

		// Token: 0x0400260B RID: 9739
		public HashSet<int> m_LastDiveHistoryData = new HashSet<int>();

		// Token: 0x0400260C RID: 9740
		public NKCOfficeData OfficeData = new NKCOfficeData();

		// Token: 0x0400260D RID: 9741
		public NKCTrimData TrimData = new NKCTrimData();

		// Token: 0x0400260E RID: 9742
		private Dictionary<int, NKMConsumerPackageData> m_dicConsumerPackageData = new Dictionary<int, NKMConsumerPackageData>();

		// Token: 0x0400260F RID: 9743
		private NKMEventCollectionInfo m_eventCollectionInfo;

		// Token: 0x04002610 RID: 9744
		public NKMShortCutInfo m_LastPlayInfo = new NKMShortCutInfo();

		// Token: 0x04002613 RID: 9747
		private NKMUserProfileData m_UserProfileData;

		// Token: 0x04002614 RID: 9748
		public int m_unitTacticReturnCount;

		// Token: 0x04002615 RID: 9749
		private const string BGM_CONTINUE_KEY = "BGM_CONTINUE_KEY";

		// Token: 0x04002616 RID: 9750
		private DateTime m_lastUpdateAsyncTicket = DateTime.MinValue;

		// Token: 0x04002617 RID: 9751
		private DateTime m_lastUpdateEterniumCap = DateTime.MinValue;

		// Token: 0x04002618 RID: 9752
		public Dictionary<ReturningUserType, NKMReturningUserState> m_dicReturningUserState = new Dictionary<ReturningUserType, NKMReturningUserState>();

		// Token: 0x04002619 RID: 9753
		private Dictionary<int, NKMStagePlayData> m_dicStagePlayData = new Dictionary<int, NKMStagePlayData>();

		// Token: 0x0400261A RID: 9754
		private NKMUserData.strMentoringData m_MyMentoringData;

		// Token: 0x0400261B RID: 9755
		public KakaoMissionData kakaoMissionData;

		// Token: 0x02001231 RID: 4657
		public enum eChangeNotifyType
		{
			// Token: 0x04009532 RID: 38194
			Add,
			// Token: 0x04009533 RID: 38195
			Update,
			// Token: 0x04009534 RID: 38196
			Remove
		}

		// Token: 0x02001232 RID: 4658
		// (Invoke) Token: 0x0600A26D RID: 41581
		public delegate void OnUserLevelUpdate(NKMUserData userData);

		// Token: 0x02001233 RID: 4659
		// (Invoke) Token: 0x0600A271 RID: 41585
		public delegate void OnCompanyBuffUpdate(NKMUserData userData);

		// Token: 0x02001234 RID: 4660
		public struct strMentoringData
		{
			// Token: 0x0600A274 RID: 41588 RVA: 0x003411E1 File Offset: 0x0033F3E1
			public strMentoringData(bool bInit = false)
			{
				this.SeasonId = 0;
				this.lstMenteeMatch = null;
				this.lstRecommend = null;
				this.lstInvited = null;
				this.MyMentor = null;
				this.bMenteeGraduate = false;
				this.bMentoringNotify = false;
			}

			// Token: 0x04009535 RID: 38197
			public int SeasonId;

			// Token: 0x04009536 RID: 38198
			public List<MenteeInfo> lstMenteeMatch;

			// Token: 0x04009537 RID: 38199
			public List<FriendListData> lstRecommend;

			// Token: 0x04009538 RID: 38200
			public List<FriendListData> lstInvited;

			// Token: 0x04009539 RID: 38201
			public FriendListData MyMentor;

			// Token: 0x0400953A RID: 38202
			public bool bMenteeGraduate;

			// Token: 0x0400953B RID: 38203
			public bool bMentoringNotify;
		}
	}
}
