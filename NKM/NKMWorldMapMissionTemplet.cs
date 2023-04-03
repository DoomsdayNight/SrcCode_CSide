using System;
using Cs.GameLog.CountryDescription;
using Cs.Logging;
using NKC;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x02000522 RID: 1314
	public class NKMWorldMapMissionTemplet : INKMTemplet
	{
		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x0600257E RID: 9598 RVA: 0x000C1002 File Offset: 0x000BF202
		public int Key
		{
			get
			{
				return this.m_ID;
			}
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x000C100C File Offset: 0x000BF20C
		public static NKMWorldMapMissionTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/NKMWorldMapMissionTemplet.cs", 72))
			{
				return null;
			}
			NKMWorldMapMissionTemplet nkmworldMapMissionTemplet = new NKMWorldMapMissionTemplet();
			bool flag = true & cNKMLua.GetData("m_WorldmapMissionID", ref nkmworldMapMissionTemplet.m_ID) & cNKMLua.GetData("m_WorldmapMissionName", ref nkmworldMapMissionTemplet.m_MissionName) & cNKMLua.GetData("m_WorldmapMissionPoolID", ref nkmworldMapMissionTemplet.m_WorldmapMissionPoolID) & cNKMLua.GetData("m_WorldMapMissionLevel", ref nkmworldMapMissionTemplet.m_MissionLevel) & cNKMLua.GetData("m_WorldmapMissionRatio", ref nkmworldMapMissionTemplet.m_WorldmapMissionRatio) & cNKMLua.GetData<NKMWorldMapMissionTemplet.WorldMapMissionRank>("m_WorldMapMissionRank", ref nkmworldMapMissionTemplet.m_eMissionRank) & cNKMLua.GetData("m_ReqManagerLevel", ref nkmworldMapMissionTemplet.m_ReqManagerLevel) & cNKMLua.GetData("m_MissionTime", ref nkmworldMapMissionTemplet.m_MissionTimeInMinutes) & cNKMLua.GetData("m_RewardCityEXP", ref nkmworldMapMissionTemplet.m_RewardCityEXP) & cNKMLua.GetData("m_RewardUnitEXP", ref nkmworldMapMissionTemplet.m_RewardUnitExp) & cNKMLua.GetData("m_RewardEternium", ref nkmworldMapMissionTemplet.m_RewardEternium) & cNKMLua.GetData("m_RewardCredit", ref nkmworldMapMissionTemplet.m_RewardCredit) & cNKMLua.GetData("m_RewardInformation", ref nkmworldMapMissionTemplet.m_RewardInformation);
			cNKMLua.GetData<NKM_REWARD_TYPE>("m_CompleteReward_Type", ref nkmworldMapMissionTemplet.m_CompleteRewardType);
			cNKMLua.GetData("m_CompleteReward_ID", ref nkmworldMapMissionTemplet.m_CompleteRewardID);
			cNKMLua.GetData("m_CompleteReward_StrID", ref nkmworldMapMissionTemplet.m_CompleteRewardStrID);
			cNKMLua.GetData("m_CompleteRewardQuantity", ref nkmworldMapMissionTemplet.m_CompleteRewardQuantity);
			cNKMLua.GetData("m_WorldMapMissionThumbnailFile", ref nkmworldMapMissionTemplet.m_WorldMapMissionThumbnailFile);
			if (!(flag & cNKMLua.GetData<NKMWorldMapMissionTemplet.WorldMapMissionType>("m_WorldmapMission_Type", ref nkmworldMapMissionTemplet.m_eMissionType) & cNKMLua.GetData("m_WorldmapEventRatio", ref nkmworldMapMissionTemplet.m_WorldmapEventRatio) & cNKMLua.GetData("m_WorldmapEventGroup", ref nkmworldMapMissionTemplet.m_WorldmapEventGroup) & cNKMLua.GetData("m_bEnableMission", ref nkmworldMapMissionTemplet.m_bEnableMission)))
			{
				return null;
			}
			return nkmworldMapMissionTemplet;
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x000C11C2 File Offset: 0x000BF3C2
		public void Join()
		{
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x000C11C4 File Offset: 0x000BF3C4
		public void Validate()
		{
			if (this.m_eMissionType == NKMWorldMapMissionTemplet.WorldMapMissionType.WMT_INVALID)
			{
				Log.ErrorAndExit(string.Format("[WorldMapMissionTemplet] 월드맵 미션 타입이 존재하지 않음 m_ID : {0}, m_eMissionType : {1}", this.m_ID, this.m_eMissionType), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/NKMWorldMapMissionTemplet.cs", 119);
			}
			if (this.m_CompleteRewardType != NKM_REWARD_TYPE.RT_NONE && this.m_CompleteRewardID > 0 && (!NKMRewardTemplet.IsValidReward(this.m_CompleteRewardType, this.m_CompleteRewardID) || this.m_CompleteRewardQuantity <= 0))
			{
				Log.ErrorAndExit(string.Format("[WorldMapMissionTemplet] 월드맵 미션 완료 보상 정보가 존재하지 않음 m_ID : {0}, m_CompleteRewardType : {1}, m_CompleteRewardID : {2}, m_CompleteRewardQuantity : {3}", new object[]
				{
					this.m_ID,
					this.m_CompleteRewardType,
					this.m_CompleteRewardID,
					this.m_CompleteRewardQuantity
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/NKMWorldMapMissionTemplet.cs", 126);
			}
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x000C1286 File Offset: 0x000BF486
		public string GetMissionName()
		{
			return NKCStringTable.GetString(this.m_MissionName, false);
		}

		// Token: 0x040026D0 RID: 9936
		public int m_ID = -1;

		// Token: 0x040026D1 RID: 9937
		public string m_MissionName = "";

		// Token: 0x040026D2 RID: 9938
		public int m_MissionLevel;

		// Token: 0x040026D3 RID: 9939
		public int m_WorldmapMissionPoolID;

		// Token: 0x040026D4 RID: 9940
		public int m_WorldmapMissionRatio;

		// Token: 0x040026D5 RID: 9941
		public NKMWorldMapMissionTemplet.WorldMapMissionRank m_eMissionRank;

		// Token: 0x040026D6 RID: 9942
		public int m_ReqManagerLevel;

		// Token: 0x040026D7 RID: 9943
		public int m_MissionTimeInMinutes = 1;

		// Token: 0x040026D8 RID: 9944
		public int m_RewardCityEXP;

		// Token: 0x040026D9 RID: 9945
		public int m_RewardUnitExp;

		// Token: 0x040026DA RID: 9946
		public int m_RewardCredit;

		// Token: 0x040026DB RID: 9947
		public int m_RewardEternium;

		// Token: 0x040026DC RID: 9948
		public int m_RewardInformation;

		// Token: 0x040026DD RID: 9949
		public NKM_REWARD_TYPE m_CompleteRewardType;

		// Token: 0x040026DE RID: 9950
		public int m_CompleteRewardID;

		// Token: 0x040026DF RID: 9951
		public string m_CompleteRewardStrID = "";

		// Token: 0x040026E0 RID: 9952
		public int m_CompleteRewardQuantity;

		// Token: 0x040026E1 RID: 9953
		public NKMWorldMapMissionTemplet.WorldMapMissionType m_eMissionType;

		// Token: 0x040026E2 RID: 9954
		public string m_WorldMapMissionThumbnailFile = "";

		// Token: 0x040026E3 RID: 9955
		public int m_WorldmapEventRatio;

		// Token: 0x040026E4 RID: 9956
		public int m_WorldmapEventGroup;

		// Token: 0x040026E5 RID: 9957
		public bool m_bEnableMission;

		// Token: 0x02001245 RID: 4677
		public enum WorldMapMissionType
		{
			// Token: 0x04009564 RID: 38244
			[CountryDescription("알수없음", CountryCode.KOR)]
			WMT_INVALID,
			// Token: 0x04009565 RID: 38245
			[CountryDescription("파견", CountryCode.KOR)]
			WMT_EXPLORE,
			// Token: 0x04009566 RID: 38246
			[CountryDescription("방위", CountryCode.KOR)]
			WMT_DEFENCE,
			// Token: 0x04009567 RID: 38247
			[CountryDescription("채굴", CountryCode.KOR)]
			WMT_MINING,
			// Token: 0x04009568 RID: 38248
			[CountryDescription("사무", CountryCode.KOR)]
			WMT_OFFICE
		}

		// Token: 0x02001246 RID: 4678
		public enum WorldMapMissionRank
		{
			// Token: 0x0400956A RID: 38250
			WMMR_S,
			// Token: 0x0400956B RID: 38251
			WMMR_A,
			// Token: 0x0400956C RID: 38252
			WMMR_B,
			// Token: 0x0400956D RID: 38253
			WMMR_C
		}
	}
}
