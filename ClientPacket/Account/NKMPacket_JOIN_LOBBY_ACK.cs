using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Contract;
using ClientPacket.Event;
using ClientPacket.Mode;
using ClientPacket.Office;
using ClientPacket.Pvp;
using ClientPacket.Shop;
using ClientPacket.Unit;
using ClientPacket.User;
using ClientPacket.Warfare;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Account
{
	// Token: 0x0200106F RID: 4207
	[PacketId(ClientPacketId.kNKMPacket_JOIN_LOBBY_ACK)]
	public sealed class NKMPacket_JOIN_LOBBY_ACK : ISerializable
	{
		// Token: 0x06009B9B RID: 39835 RVA: 0x003335C8 File Offset: 0x003317C8
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.friendCode);
			stream.PutOrGet<NKMUserData>(ref this.userData);
			stream.PutOrGet<NKMGameData>(ref this.gameData);
			stream.PutOrGet<WarfareGameData>(ref this.warfareGameData);
			stream.PutOrGet(ref this.utcTime);
			stream.PutOrGet(ref this.utcOffset);
			stream.PutOrGet(ref this.lastCreditSupplyTakeTime);
			stream.PutOrGet(ref this.lastEterniumSupplyTakeTime);
			stream.PutOrGet(ref this.totalPaidAmount);
			stream.PutOrGet<ShopChainTabNextResetData>(ref this.shopChainTabNestResetList);
			stream.PutOrGet<NKMPvpBanResult>(ref this.pvpBanResult);
			stream.PutOrGet<PvpState>(ref this.asyncPvpState);
			stream.PutOrGet<PvpState>(ref this.leaguePvpState);
			stream.PutOrGet(ref this.pvpPointChargeTime);
			stream.PutOrGet(ref this.rankPvpOpen);
			stream.PutOrGet(ref this.leaguePvpOpen);
			stream.PutOrGet<NKMReturningUserState>(ref this.ReturningUserStates);
			stream.PutOrGet<NKMContractState>(ref this.contractState);
			stream.PutOrGet<NKMContractBonusState>(ref this.contractBonusState);
			stream.PutOrGet<NKMSelectableContractState>(ref this.selectableContractState);
			stream.PutOrGet<NKMStagePlayData>(ref this.stagePlayDataList);
			stream.PutOrGet<EventInfo>(ref this.eventInfo);
			stream.PutOrGet(ref this.reconnectKey);
			stream.PutOrGet<ZlongUserData>(ref this.zlongUserData);
			stream.PutOrGet<NKMBackgroundInfo>(ref this.backGroundInfo);
			stream.PutOrGet<PrivateGuildData>(ref this.privateGuildData);
			stream.PutOrGet(ref this.blockMuteEndDate);
			stream.PutOrGet(ref this.marketReviewCompletion);
			stream.PutOrGet(ref this.fierceDailyRewardReceived);
			stream.PutOrGet<GuildDungeonRewardInfo>(ref this.guildDungeonRewardInfo);
			stream.PutOrGet<NKMEquipTuningCandidate>(ref this.equipTuningCandidate);
			stream.PutOrGet<NKMPvpGameLobbyState>(ref this.pvpGameLobby);
			stream.PutOrGet<DraftPvpRoomData>(ref this.leaguePvpRoomData);
			stream.PutOrGet<PvpSingleHistory>(ref this.leaguePvpHistories);
			stream.PutOrGet<PvpSingleHistory>(ref this.privatePvpHistories);
			stream.PutOrGet<NKMMyOfficeState>(ref this.officeState);
			stream.PutOrGet<KakaoMissionData>(ref this.kakaoMissionData);
			stream.PutOrGet(ref this.unlockedStageIds);
			stream.PutOrGet<NKMPhaseClearData>(ref this.phaseClearDataList);
			stream.PutOrGet<PhaseModeState>(ref this.phaseModeState);
			stream.PutOrGet<NKMServerKillCountData>(ref this.serverKillCountDataList);
			stream.PutOrGet<NKMKillCountData>(ref this.killCountDataList);
			stream.PutOrGet<NKMUnitMissionData>(ref this.completedUnitMissions);
			stream.PutOrGet<NKMUnitMissionData>(ref this.rewardEnableUnitMissions);
			stream.PutOrGet<PvpCastingVoteData>(ref this.pvpCastingVoteData);
			stream.PutOrGet<NKMIntervalData>(ref this.intervalData);
			stream.PutOrGet<NKMConsumerPackageData>(ref this.consumerPackages);
			stream.PutOrGet<NpcPvpData>(ref this.npcPvpData);
			stream.PutOrGet<NKMTrimIntervalData>(ref this.trimIntervalData);
			stream.PutOrGet<NKMTrimClearData>(ref this.trimClearList);
			stream.PutOrGet<NKMShipModuleCandidate>(ref this.shipSlotCandidate);
			stream.PutOrGet<TrimModeState>(ref this.trimModeState);
			stream.PutOrGet(ref this.enableAccountLink);
			stream.PutOrGet<NKMEventCollectionInfo>(ref this.eventCollectionInfo);
			stream.PutOrGet<NKMUserProfileData>(ref this.userProfileData);
			stream.PutOrGet<NKMShortCutInfo>(ref this.lastPlayInfo);
			stream.PutOrGet(ref this.unitTacticReturnCount);
		}

		// Token: 0x04008FA1 RID: 36769
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008FA2 RID: 36770
		public long friendCode;

		// Token: 0x04008FA3 RID: 36771
		public NKMUserData userData;

		// Token: 0x04008FA4 RID: 36772
		public NKMGameData gameData;

		// Token: 0x04008FA5 RID: 36773
		public WarfareGameData warfareGameData = new WarfareGameData();

		// Token: 0x04008FA6 RID: 36774
		public DateTime utcTime;

		// Token: 0x04008FA7 RID: 36775
		public TimeSpan utcOffset;

		// Token: 0x04008FA8 RID: 36776
		public DateTime lastCreditSupplyTakeTime;

		// Token: 0x04008FA9 RID: 36777
		public DateTime lastEterniumSupplyTakeTime;

		// Token: 0x04008FAA RID: 36778
		public double totalPaidAmount;

		// Token: 0x04008FAB RID: 36779
		public List<ShopChainTabNextResetData> shopChainTabNestResetList = new List<ShopChainTabNextResetData>();

		// Token: 0x04008FAC RID: 36780
		public NKMPvpBanResult pvpBanResult = new NKMPvpBanResult();

		// Token: 0x04008FAD RID: 36781
		public PvpState asyncPvpState;

		// Token: 0x04008FAE RID: 36782
		public PvpState leaguePvpState;

		// Token: 0x04008FAF RID: 36783
		public DateTime pvpPointChargeTime;

		// Token: 0x04008FB0 RID: 36784
		public bool rankPvpOpen;

		// Token: 0x04008FB1 RID: 36785
		public bool leaguePvpOpen;

		// Token: 0x04008FB2 RID: 36786
		public List<NKMReturningUserState> ReturningUserStates = new List<NKMReturningUserState>();

		// Token: 0x04008FB3 RID: 36787
		public List<NKMContractState> contractState = new List<NKMContractState>();

		// Token: 0x04008FB4 RID: 36788
		public List<NKMContractBonusState> contractBonusState = new List<NKMContractBonusState>();

		// Token: 0x04008FB5 RID: 36789
		public NKMSelectableContractState selectableContractState = new NKMSelectableContractState();

		// Token: 0x04008FB6 RID: 36790
		public List<NKMStagePlayData> stagePlayDataList = new List<NKMStagePlayData>();

		// Token: 0x04008FB7 RID: 36791
		public EventInfo eventInfo = new EventInfo();

		// Token: 0x04008FB8 RID: 36792
		public string reconnectKey;

		// Token: 0x04008FB9 RID: 36793
		public ZlongUserData zlongUserData = new ZlongUserData();

		// Token: 0x04008FBA RID: 36794
		public NKMBackgroundInfo backGroundInfo = new NKMBackgroundInfo();

		// Token: 0x04008FBB RID: 36795
		public PrivateGuildData privateGuildData = new PrivateGuildData();

		// Token: 0x04008FBC RID: 36796
		public DateTime blockMuteEndDate;

		// Token: 0x04008FBD RID: 36797
		public bool marketReviewCompletion;

		// Token: 0x04008FBE RID: 36798
		public bool fierceDailyRewardReceived;

		// Token: 0x04008FBF RID: 36799
		public GuildDungeonRewardInfo guildDungeonRewardInfo = new GuildDungeonRewardInfo();

		// Token: 0x04008FC0 RID: 36800
		public NKMEquipTuningCandidate equipTuningCandidate = new NKMEquipTuningCandidate();

		// Token: 0x04008FC1 RID: 36801
		public NKMPvpGameLobbyState pvpGameLobby = new NKMPvpGameLobbyState();

		// Token: 0x04008FC2 RID: 36802
		public DraftPvpRoomData leaguePvpRoomData;

		// Token: 0x04008FC3 RID: 36803
		public List<PvpSingleHistory> leaguePvpHistories = new List<PvpSingleHistory>();

		// Token: 0x04008FC4 RID: 36804
		public List<PvpSingleHistory> privatePvpHistories = new List<PvpSingleHistory>();

		// Token: 0x04008FC5 RID: 36805
		public NKMMyOfficeState officeState;

		// Token: 0x04008FC6 RID: 36806
		public KakaoMissionData kakaoMissionData;

		// Token: 0x04008FC7 RID: 36807
		public List<int> unlockedStageIds = new List<int>();

		// Token: 0x04008FC8 RID: 36808
		public List<NKMPhaseClearData> phaseClearDataList = new List<NKMPhaseClearData>();

		// Token: 0x04008FC9 RID: 36809
		public PhaseModeState phaseModeState;

		// Token: 0x04008FCA RID: 36810
		public List<NKMServerKillCountData> serverKillCountDataList = new List<NKMServerKillCountData>();

		// Token: 0x04008FCB RID: 36811
		public List<NKMKillCountData> killCountDataList = new List<NKMKillCountData>();

		// Token: 0x04008FCC RID: 36812
		public List<NKMUnitMissionData> completedUnitMissions = new List<NKMUnitMissionData>();

		// Token: 0x04008FCD RID: 36813
		public List<NKMUnitMissionData> rewardEnableUnitMissions = new List<NKMUnitMissionData>();

		// Token: 0x04008FCE RID: 36814
		public PvpCastingVoteData pvpCastingVoteData = new PvpCastingVoteData();

		// Token: 0x04008FCF RID: 36815
		public List<NKMIntervalData> intervalData = new List<NKMIntervalData>();

		// Token: 0x04008FD0 RID: 36816
		public List<NKMConsumerPackageData> consumerPackages = new List<NKMConsumerPackageData>();

		// Token: 0x04008FD1 RID: 36817
		public NpcPvpData npcPvpData;

		// Token: 0x04008FD2 RID: 36818
		public NKMTrimIntervalData trimIntervalData = new NKMTrimIntervalData();

		// Token: 0x04008FD3 RID: 36819
		public List<NKMTrimClearData> trimClearList = new List<NKMTrimClearData>();

		// Token: 0x04008FD4 RID: 36820
		public NKMShipModuleCandidate shipSlotCandidate = new NKMShipModuleCandidate();

		// Token: 0x04008FD5 RID: 36821
		public TrimModeState trimModeState;

		// Token: 0x04008FD6 RID: 36822
		public bool enableAccountLink;

		// Token: 0x04008FD7 RID: 36823
		public NKMEventCollectionInfo eventCollectionInfo = new NKMEventCollectionInfo();

		// Token: 0x04008FD8 RID: 36824
		public NKMUserProfileData userProfileData = new NKMUserProfileData();

		// Token: 0x04008FD9 RID: 36825
		public NKMShortCutInfo lastPlayInfo = new NKMShortCutInfo();

		// Token: 0x04008FDA RID: 36826
		public int unitTacticReturnCount;
	}
}
