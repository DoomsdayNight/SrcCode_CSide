using System;
using System.Collections.Generic;
using System.Linq;
using Cs.Logging;
using NKC;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x02000523 RID: 1315
	public class NKMDungeonTempletBase : INKMTemplet
	{
		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06002584 RID: 9604 RVA: 0x000C12CB File Offset: 0x000BF4CB
		public int Key
		{
			get
			{
				return this.m_DungeonID;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06002585 RID: 9605 RVA: 0x000C12D3 File Offset: 0x000BF4D3
		public string DebugName
		{
			get
			{
				return string.Format("[{0}] {1}", this.m_DungeonID, this.m_DungeonStrID);
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06002586 RID: 9606 RVA: 0x000C12F0 File Offset: 0x000BF4F0
		// (set) Token: 0x06002587 RID: 9607 RVA: 0x000C12F8 File Offset: 0x000BF4F8
		public NKMStageTempletV2 StageTemplet { get; internal set; }

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06002588 RID: 9608 RVA: 0x000C1301 File Offset: 0x000BF501
		// (set) Token: 0x06002589 RID: 9609 RVA: 0x000C1309 File Offset: 0x000BF509
		public NKMBattleConditionTemplet BattleCondition { get; private set; }

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x0600258A RID: 9610 RVA: 0x000C1312 File Offset: 0x000BF512
		public string DungeonName
		{
			get
			{
				return this.m_DungeonName;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x0600258B RID: 9611 RVA: 0x000C131A File Offset: 0x000BF51A
		public bool BonusResult
		{
			get
			{
				return this.m_bBonus_Resource;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x0600258C RID: 9612 RVA: 0x000C1322 File Offset: 0x000BF522
		public bool IsPhaseDungeon
		{
			get
			{
				return this.phaseReferences.Any<NKMPhaseGroupTemplet>();
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x0600258D RID: 9613 RVA: 0x000C132F File Offset: 0x000BF52F
		// (set) Token: 0x0600258E RID: 9614 RVA: 0x000C1337 File Offset: 0x000BF537
		public NKMDungeonEventDeckTemplet EventDeckTemplet { get; private set; }

		// Token: 0x0600258F RID: 9615 RVA: 0x000C1340 File Offset: 0x000BF540
		public bool HasCutscen()
		{
			return this.m_CutScenStrIDBefore.Length > 0 || this.m_CutScenStrIDAfter.Length > 0;
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x000C1360 File Offset: 0x000BF560
		public bool IsUsingEventDeck()
		{
			return this.m_UseEventDeck != 0;
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x000C136C File Offset: 0x000BF56C
		public static NKMDungeonTempletBase LoadFromLUA(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/Stage/NKMDungeonTempletBase.cs", 101))
			{
				return null;
			}
			NKMDungeonTempletBase nkmdungeonTempletBase = new NKMDungeonTempletBase();
			cNKMLua.GetData("m_DungeonID", ref nkmdungeonTempletBase.m_DungeonID);
			cNKMLua.GetData("m_DungeonStrID", ref nkmdungeonTempletBase.m_DungeonStrID);
			cNKMLua.GetData("m_DungeonTempletFileName", ref nkmdungeonTempletBase.m_DungeonTempletFileName);
			cNKMLua.GetData("m_DungeonMapStrID", ref nkmdungeonTempletBase.m_DungeonMapStrID);
			cNKMLua.GetData("m_BattleConditionStrID", ref nkmdungeonTempletBase.m_BattleConditionStrID);
			cNKMLua.GetData("m_MusicAssetName", ref nkmdungeonTempletBase.m_MusicName);
			cNKMLua.GetData("m_fGameTime", ref nkmdungeonTempletBase.m_fGameTime);
			cNKMLua.GetData("m_fDoubleCostTime", ref nkmdungeonTempletBase.m_fDoubleCostTime);
			cNKMLua.GetData<NKM_DUNGEON_TYPE>("m_DungeonType", ref nkmdungeonTempletBase.m_DungeonType);
			cNKMLua.GetData("m_bNoShuffleDeck", ref nkmdungeonTempletBase.m_bNoShuffleDeck);
			cNKMLua.GetData("m_bDeckReuse", ref nkmdungeonTempletBase.m_bDeckReuse);
			cNKMLua.GetData("m_RespawnCountMaxSameTime", ref nkmdungeonTempletBase.m_RespawnCountMaxSameTime);
			cNKMLua.GetData("m_UseEventDeck", ref nkmdungeonTempletBase.m_UseEventDeck);
			cNKMLua.GetData("m_bBonus_Resource", ref nkmdungeonTempletBase.m_bBonus_Resource);
			cNKMLua.GetData("m_DungeonName", ref nkmdungeonTempletBase.m_DungeonName);
			cNKMLua.GetData("m_DungeonDesc", ref nkmdungeonTempletBase.m_DungeonDesc);
			cNKMLua.GetData("m_DungeonIcon", ref nkmdungeonTempletBase.m_DungeonIcon);
			cNKMLua.GetData("m_DungeonLevel", ref nkmdungeonTempletBase.m_DungeonLevel);
			cNKMLua.GetData("m_DGRecommendFightPower", ref nkmdungeonTempletBase.m_DGRecommendFightPower);
			cNKMLua.GetData("m_DGLimitUserLevel", ref nkmdungeonTempletBase.m_DGLimitUserLevel);
			cNKMLua.GetData("m_CutScenStrIDBefore", ref nkmdungeonTempletBase.m_CutScenStrIDBefore);
			cNKMLua.GetData("m_CutScenStrIDAfter", ref nkmdungeonTempletBase.m_CutScenStrIDAfter);
			cNKMLua.GetData<DUNGEON_GAME_MISSION_TYPE>("m_DGMissionType_1", ref nkmdungeonTempletBase.m_DGMissionType_1);
			cNKMLua.GetData("m_DGMissionValue_1", ref nkmdungeonTempletBase.m_DGMissionValue_1);
			cNKMLua.GetData<DUNGEON_GAME_MISSION_TYPE>("m_DGMissionType_2", ref nkmdungeonTempletBase.m_DGMissionType_2);
			cNKMLua.GetData("m_DGMissionValue_2", ref nkmdungeonTempletBase.m_DGMissionValue_2);
			cNKMLua.GetData("m_RewardUserEXP", ref nkmdungeonTempletBase.m_RewardUserEXP);
			cNKMLua.GetData("m_RewardUnitEXP", ref nkmdungeonTempletBase.m_RewardUnitEXP);
			cNKMLua.GetData("m_RewardCredit_Min", ref nkmdungeonTempletBase.m_RewardCredit_Min);
			cNKMLua.GetData("m_RewardCredit_Max", ref nkmdungeonTempletBase.m_RewardCredit_Max);
			cNKMLua.GetData("m_RewardEternium_Min", ref nkmdungeonTempletBase.m_RewardEternium_Min);
			cNKMLua.GetData("m_RewardEternium_Max", ref nkmdungeonTempletBase.m_RewardEternium_Max);
			cNKMLua.GetData("m_RewardInformation_Min", ref nkmdungeonTempletBase.m_RewardInformation_Min);
			cNKMLua.GetData("m_RewardInformation_Max", ref nkmdungeonTempletBase.m_RewardInformation_Max);
			cNKMLua.GetData("m_RewardUnitExp1Tier", ref nkmdungeonTempletBase.m_RewardUnitExp1Tier);
			cNKMLua.GetData("m_RewardUnitExp2Tier", ref nkmdungeonTempletBase.m_RewardUnitExp2Tier);
			cNKMLua.GetData("m_RewardUnitExp3Tier", ref nkmdungeonTempletBase.m_RewardUnitExp3Tier);
			cNKMLua.GetData<NKMDungeonTempletBase.NKM_WARFARE_GAME_UNIT_DIE_TYPE>("m_NKM_WARFARE_GAME_UNIT_DIE_TYPE", ref nkmdungeonTempletBase.m_NKM_WARFARE_GAME_UNIT_DIE_TYPE);
			cNKMLua.GetData("m_RewardMultiplyMax", ref nkmdungeonTempletBase.m_RewardMultiplyMax);
			cNKMLua.GetData("m_Intro", ref nkmdungeonTempletBase.m_Intro);
			cNKMLua.GetData("m_Outro", ref nkmdungeonTempletBase.m_Outro);
			nkmdungeonTempletBase.m_listDungeonReward.Clear();
			for (int i = 0; i < DungeonReward.MAX_REWARD_COUNT; i++)
			{
				DungeonReward dungeonReward = new DungeonReward();
				if (!cNKMLua.GetData(string.Format("m_RewardGroupID_{0}", i + 1), ref dungeonReward.m_RewardGroupID))
				{
					break;
				}
				cNKMLua.GetData(string.Format("m_fRewardRate_{0}", i + 1), ref dungeonReward.m_RewardRate);
				nkmdungeonTempletBase.m_listDungeonReward.Add(dungeonReward);
			}
			nkmdungeonTempletBase.CheckValidation();
			return nkmdungeonTempletBase;
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x000C16E5 File Offset: 0x000BF8E5
		public FirstRewardData GetFirstRewardData()
		{
			NKMStageTempletV2 stageTemplet = this.StageTemplet;
			return ((stageTemplet != null) ? stageTemplet.GetFirstRewardData() : null) ?? FirstRewardData.Empty;
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x000C1704 File Offset: 0x000BF904
		public void Join()
		{
			if (!string.IsNullOrEmpty(this.m_BattleConditionStrID))
			{
				this.BattleCondition = NKMBattleConditionManager.GetTempletByStrID(this.m_BattleConditionStrID);
				if (this.BattleCondition == null)
				{
					Log.ErrorAndExit(string.Format("[{0}]{1} BattleCondition 값이 올바르지 않음: {2}", this.m_DungeonID, this.m_DungeonStrID, this.m_BattleConditionStrID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/Stage/NKMDungeonTempletBase.cs", 200);
				}
			}
			if (this.m_UseEventDeck > 0)
			{
				this.EventDeckTemplet = NKMDungeonManager.GetEventDeckTemplet(this.m_UseEventDeck);
				if (this.EventDeckTemplet == null)
				{
					NKMTempletError.Add(string.Format("{0} invalid eventDeckId:{1}", this.DebugName, this.m_UseEventDeck), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/Stage/NKMDungeonTempletBase.cs", 209);
				}
			}
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x000C17B4 File Offset: 0x000BF9B4
		public void Validate()
		{
			if (this.m_RewardCredit_Min < 0 || this.m_RewardCredit_Max < 0 || this.m_RewardCredit_Min > this.m_RewardCredit_Max)
			{
				NKMTempletError.Add(string.Format("[DungeonTempletBase] invalid reward credit range. [{0},{1}]", this.m_RewardCredit_Min, this.m_RewardCredit_Max), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/Stage/NKMDungeonTempletBase.cs", 219);
			}
			if (this.m_RewardEternium_Min < 0 || this.m_RewardEternium_Max < 0 || this.m_RewardEternium_Min > this.m_RewardEternium_Max)
			{
				NKMTempletError.Add(string.Format("[DungeonTempletBase] invalid reward eternium range. [{0}, {1}]", this.m_RewardEternium_Min, this.m_RewardEternium_Max), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/Stage/NKMDungeonTempletBase.cs", 225);
			}
			if (this.m_RewardInformation_Min < 0 || this.m_RewardInformation_Max < 0 || this.m_RewardInformation_Min > this.m_RewardInformation_Max)
			{
				NKMTempletError.Add(string.Format("[DungeonTempletBase] invalid reward infomation range. [{0}, {1}", this.m_RewardInformation_Min, this.m_RewardInformation_Max), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/Stage/NKMDungeonTempletBase.cs", 231);
			}
			if (this.m_DungeonType == NKM_DUNGEON_TYPE.NDT_CUTSCENE && this.BonusResult)
			{
				NKMTempletError.Add(string.Format("[DungeonTempletBase] bonusResult[{0}] is invalid, CutScene dungeon must be bonusResult = false", this.BonusResult), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/Stage/NKMDungeonTempletBase.cs", 236);
			}
			if (this.m_RewardMultiplyMax <= 0)
			{
				NKMTempletError.Add(string.Format("[{0}] {1} 보상 배수의 맥스치가 0이하 입니다. value: {2}", this.Key, this.m_DungeonStrID, this.m_RewardMultiplyMax), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/Stage/NKMDungeonTempletBase.cs", 241);
			}
			if (this.IsPhaseDungeon)
			{
				if (this.m_RewardCredit_Min > 0 || this.m_RewardCredit_Max > 0)
				{
					NKMTempletError.Add(string.Format("{0} 페이즈 던전에 크레딧 보상이 설정되어 있음: {1} ~ {2}", this.DebugName, this.m_RewardCredit_Min, this.m_RewardCredit_Max), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/Stage/NKMDungeonTempletBase.cs", 248);
				}
				if (this.m_RewardEternium_Min > 0 || this.m_RewardEternium_Max > 0)
				{
					NKMTempletError.Add(string.Format("{0} 페이즈 던전에 이터니움 보상이 설정되어 있음: {1} ~ {2}", this.DebugName, this.m_RewardEternium_Min, this.m_RewardEternium_Max), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/Stage/NKMDungeonTempletBase.cs", 253);
				}
				if (this.m_RewardInformation_Min > 0 || this.m_RewardInformation_Max > 0)
				{
					NKMTempletError.Add(string.Format("{0} 페이즈 던전에 정보 보상이 설정되어 있음: {1} ~ {2}", this.DebugName, this.m_RewardInformation_Min, this.m_RewardInformation_Max), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/Stage/NKMDungeonTempletBase.cs", 258);
				}
				if (this.BonusResult)
				{
					NKMTempletError.Add(this.DebugName + " 페이즈 던전에 메달 보너스 비율이 설정되어 있음", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/Stage/NKMDungeonTempletBase.cs", 263);
				}
				if (this.m_RewardUserEXP > 0)
				{
					NKMTempletError.Add(string.Format("{0} 페이즈 던전에 유저 경험치가 설정되어 있음:{1}", this.DebugName, this.m_RewardUserEXP), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/Stage/NKMDungeonTempletBase.cs", 268);
				}
				if (this.m_RewardUnitEXP > 0)
				{
					NKMTempletError.Add(string.Format("{0} 페이즈 던전에 유닛 경험치가 설정되어 있음:{1}", this.DebugName, this.m_RewardUserEXP), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/Stage/NKMDungeonTempletBase.cs", 273);
				}
				if (this.m_RewardMultiplyMax > 1)
				{
					NKMTempletError.Add(string.Format("{0} 페이즈 던전에 중첩작전 배율이 설정되어 있음:{1}", this.DebugName, this.m_RewardMultiplyMax), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/Stage/NKMDungeonTempletBase.cs", 278);
				}
				if (this.m_DGMissionType_1 != DUNGEON_GAME_MISSION_TYPE.DGMT_NONE || this.m_DGMissionType_2 != DUNGEON_GAME_MISSION_TYPE.DGMT_NONE)
				{
					NKMTempletError.Add(string.Format("{0} 페이즈 던전에 중첩작전 미션이 설정되어 있음:{1} {2}", this.DebugName, this.m_DGMissionType_1, this.m_DGMissionType_2), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/Stage/NKMDungeonTempletBase.cs", 284);
				}
			}
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x000C1B06 File Offset: 0x000BFD06
		public void AddReference(NKMPhaseGroupTemplet phaseGroupTemplet)
		{
			this.phaseReferences.Add(phaseGroupTemplet);
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x000C1B15 File Offset: 0x000BFD15
		public int DecideRewardCredit()
		{
			return PerThreadRandom.Instance.Next(this.m_RewardCredit_Min, this.m_RewardCredit_Max + 1);
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x000C1B2F File Offset: 0x000BFD2F
		public int DecideRewardEternium()
		{
			return PerThreadRandom.Instance.Next(this.m_RewardEternium_Min, this.m_RewardEternium_Max + 1);
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x000C1B49 File Offset: 0x000BFD49
		public int DecideRewardInfomation()
		{
			return PerThreadRandom.Instance.Next(this.m_RewardInformation_Min, this.m_RewardInformation_Max + 1);
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x000C1B63 File Offset: 0x000BFD63
		public string GetDungeonName()
		{
			if (!string.IsNullOrWhiteSpace(this.m_DungeonName))
			{
				return NKCStringTable.GetString(this.m_DungeonName, false);
			}
			return "";
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x000C1B84 File Offset: 0x000BFD84
		public string GetDungeonDesc()
		{
			return NKCStringTable.GetString(this.m_DungeonDesc, false);
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x000C1B94 File Offset: 0x000BFD94
		private void CheckValidation()
		{
			if (this.m_UseEventDeck > 0 && NKMDungeonManager.GetEventDeckTemplet(this.m_UseEventDeck) == null)
			{
				Log.ErrorAndExit(string.Format("[DungeonTempletBase] 이벤트 덱 정보가 존재하지 않음 m_DungeonID : {0}, useEventDeck : {1}", this.m_DungeonID, this.m_UseEventDeck), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/Templet/NKMDungeonTempletBase.cs", 29);
			}
			if (this.m_DungeonType == NKM_DUNGEON_TYPE.NDT_CUTSCENE && this.m_CutScenStrIDAfter == "" && this.m_CutScenStrIDBefore == "")
			{
				Log.ErrorAndExit(string.Format("[DungeonTempletBase] 컷씬 던전인데 컷씬 정보가 입력되어 있지 않음 m_DungeonID : {0}", this.m_DungeonID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/Templet/NKMDungeonTempletBase.cs", 37);
			}
			if (this.m_DungeonMapStrID != "" && NKMMapManager.GetMapTempletByStrID(this.m_DungeonMapStrID) == null)
			{
				Log.ErrorAndExit(string.Format("[DungeonTempletBase] 맵 정보가 존재하지 않음 m_DungeonID : {0}, m_DungeonMapStrID : {1}", this.m_DungeonID, this.m_DungeonMapStrID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/Templet/NKMDungeonTempletBase.cs", 46);
			}
		}

		// Token: 0x040026E6 RID: 9958
		private readonly HashSet<NKMPhaseGroupTemplet> phaseReferences = new HashSet<NKMPhaseGroupTemplet>();

		// Token: 0x040026E7 RID: 9959
		public int m_DungeonID;

		// Token: 0x040026E8 RID: 9960
		public string m_DungeonStrID = "";

		// Token: 0x040026E9 RID: 9961
		public string m_DungeonTempletFileName = "";

		// Token: 0x040026EA RID: 9962
		public string m_DungeonMapStrID = "";

		// Token: 0x040026EB RID: 9963
		private string m_BattleConditionStrID = "";

		// Token: 0x040026EC RID: 9964
		public string m_MusicName = "";

		// Token: 0x040026ED RID: 9965
		public float m_fGameTime = 180f;

		// Token: 0x040026EE RID: 9966
		public float m_fDoubleCostTime = 60f;

		// Token: 0x040026EF RID: 9967
		public NKM_DUNGEON_TYPE m_DungeonType = NKM_DUNGEON_TYPE.NDT_BOSS_KILL;

		// Token: 0x040026F0 RID: 9968
		public bool m_bNoShuffleDeck;

		// Token: 0x040026F1 RID: 9969
		public bool m_bDeckReuse = true;

		// Token: 0x040026F2 RID: 9970
		public int m_RespawnCountMaxSameTime;

		// Token: 0x040026F3 RID: 9971
		public int m_UseEventDeck;

		// Token: 0x040026F4 RID: 9972
		private bool m_bBonus_Resource;

		// Token: 0x040026F5 RID: 9973
		private string m_DungeonName = "";

		// Token: 0x040026F6 RID: 9974
		private string m_DungeonDesc = "";

		// Token: 0x040026F7 RID: 9975
		public string m_DungeonIcon = "";

		// Token: 0x040026F8 RID: 9976
		public int m_DungeonLevel = 1;

		// Token: 0x040026F9 RID: 9977
		public int m_DGRecommendFightPower;

		// Token: 0x040026FA RID: 9978
		public int m_DGLimitUserLevel;

		// Token: 0x040026FB RID: 9979
		public string m_CutScenStrIDBefore = "";

		// Token: 0x040026FC RID: 9980
		public string m_CutScenStrIDAfter = "";

		// Token: 0x040026FD RID: 9981
		public DUNGEON_GAME_MISSION_TYPE m_DGMissionType_1;

		// Token: 0x040026FE RID: 9982
		public int m_DGMissionValue_1;

		// Token: 0x040026FF RID: 9983
		public DUNGEON_GAME_MISSION_TYPE m_DGMissionType_2;

		// Token: 0x04002700 RID: 9984
		public int m_DGMissionValue_2;

		// Token: 0x04002701 RID: 9985
		public int m_RewardUserEXP;

		// Token: 0x04002702 RID: 9986
		public int m_RewardUnitEXP;

		// Token: 0x04002703 RID: 9987
		public int m_RewardCredit_Min;

		// Token: 0x04002704 RID: 9988
		public int m_RewardCredit_Max;

		// Token: 0x04002705 RID: 9989
		public int m_RewardEternium_Min;

		// Token: 0x04002706 RID: 9990
		public int m_RewardEternium_Max;

		// Token: 0x04002707 RID: 9991
		public int m_RewardInformation_Min;

		// Token: 0x04002708 RID: 9992
		public int m_RewardInformation_Max;

		// Token: 0x04002709 RID: 9993
		public int m_RewardUnitExp1Tier;

		// Token: 0x0400270A RID: 9994
		public int m_RewardUnitExp2Tier;

		// Token: 0x0400270B RID: 9995
		public int m_RewardUnitExp3Tier;

		// Token: 0x0400270C RID: 9996
		public int m_RewardMultiplyMax = 1;

		// Token: 0x0400270D RID: 9997
		public string m_Intro = "";

		// Token: 0x0400270E RID: 9998
		public string m_Outro = "";

		// Token: 0x0400270F RID: 9999
		public List<DungeonReward> m_listDungeonReward = new List<DungeonReward>();

		// Token: 0x04002710 RID: 10000
		public NKMDungeonTempletBase.NKM_WARFARE_GAME_UNIT_DIE_TYPE m_NKM_WARFARE_GAME_UNIT_DIE_TYPE;

		// Token: 0x02001247 RID: 4679
		public enum NKM_WARFARE_GAME_UNIT_DIE_TYPE
		{
			// Token: 0x0400956F RID: 38255
			NWGUDT_EXPLODE,
			// Token: 0x04009570 RID: 38256
			NWGUDT_RUNAWAY
		}
	}
}
