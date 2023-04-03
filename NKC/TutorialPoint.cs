using System;

namespace NKC
{
	// Token: 0x020006D7 RID: 1751
	public enum TutorialPoint
	{
		// Token: 0x04003632 RID: 13874
		None,
		// Token: 0x04003633 RID: 13875
		Lobby,
		// Token: 0x04003634 RID: 13876
		DailyEpisode,
		// Token: 0x04003635 RID: 13877
		EpisodeResult,
		// Token: 0x04003636 RID: 13878
		EventDeck,
		// Token: 0x04003637 RID: 13879
		CounterCase,
		// Token: 0x04003638 RID: 13880
		Contract,
		// Token: 0x04003639 RID: 13881
		ContractSelection,
		// Token: 0x0400363A RID: 13882
		Base,
		// Token: 0x0400363B RID: 13883
		Negotiate,
		// Token: 0x0400363C RID: 13884
		LabEnhance,
		// Token: 0x0400363D RID: 13885
		LabLimitBreak,
		// Token: 0x0400363E RID: 13886
		LabLimitBreakUnit,
		// Token: 0x0400363F RID: 13887
		HangerBuild,
		// Token: 0x04003640 RID: 13888
		HangerShipyard,
		// Token: 0x04003641 RID: 13889
		FactoryCraft,
		// Token: 0x04003642 RID: 13890
		FactoryEnchant,
		// Token: 0x04003643 RID: 13891
		FactoryTuning,
		// Token: 0x04003644 RID: 13892
		FactoryUpgrade,
		// Token: 0x04003645 RID: 13893
		FactoryHiddenOption,
		// Token: 0x04003646 RID: 13894
		UnitInfo,
		// Token: 0x04003647 RID: 13895
		UnitNegotiate,
		// Token: 0x04003648 RID: 13896
		UnitLimitBreak,
		// Token: 0x04003649 RID: 13897
		UnitSkillTraining,
		// Token: 0x0400364A RID: 13898
		ShipInfo,
		// Token: 0x0400364B RID: 13899
		ShipInfoMaxLevel,
		// Token: 0x0400364C RID: 13900
		ShipOverhaul,
		// Token: 0x0400364D RID: 13901
		ShipLimitBreak,
		// Token: 0x0400364E RID: 13902
		ShipModule,
		// Token: 0x0400364F RID: 13903
		TeamSetting,
		// Token: 0x04003650 RID: 13904
		Warfare,
		// Token: 0x04003651 RID: 13905
		WarfareResult,
		// Token: 0x04003652 RID: 13906
		WarfareStart,
		// Token: 0x04003653 RID: 13907
		WarfareBattle,
		// Token: 0x04003654 RID: 13908
		WarfareSync,
		// Token: 0x04003655 RID: 13909
		Gauntlet,
		// Token: 0x04003656 RID: 13910
		GauntletLobby,
		// Token: 0x04003657 RID: 13911
		GauntletLobbyRank,
		// Token: 0x04003658 RID: 13912
		GauntletLobbyLeague,
		// Token: 0x04003659 RID: 13913
		Team,
		// Token: 0x0400365A RID: 13914
		Achieventment,
		// Token: 0x0400365B RID: 13915
		WorldMap,
		// Token: 0x0400365C RID: 13916
		DiveReady,
		// Token: 0x0400365D RID: 13917
		DiveStart,
		// Token: 0x0400365E RID: 13918
		RaidReady,
		// Token: 0x0400365F RID: 13919
		RaidStart,
		// Token: 0x04003660 RID: 13920
		RaidWarning,
		// Token: 0x04003661 RID: 13921
		DiveWarning,
		// Token: 0x04003662 RID: 13922
		ShadowPalace,
		// Token: 0x04003663 RID: 13923
		ShadowBattle,
		// Token: 0x04003664 RID: 13924
		FierceLobby,
		// Token: 0x04003665 RID: 13925
		SupplyGuide,
		// Token: 0x04003666 RID: 13926
		OperatorEnhance,
		// Token: 0x04003667 RID: 13927
		OperatorList,
		// Token: 0x04003668 RID: 13928
		Shop,
		// Token: 0x04003669 RID: 13929
		ConsortiumMain,
		// Token: 0x0400366A RID: 13930
		ConsortiumDungeon,
		// Token: 0x0400366B RID: 13931
		Scout,
		// Token: 0x0400366C RID: 13932
		Lifetime,
		// Token: 0x0400366D RID: 13933
		OfficeRoom,
		// Token: 0x0400366E RID: 13934
		OfficeLab,
		// Token: 0x0400366F RID: 13935
		OfficeFactory,
		// Token: 0x04003670 RID: 13936
		OfficeHangar,
		// Token: 0x04003671 RID: 13937
		OfficeCEO,
		// Token: 0x04003672 RID: 13938
		OfficeTerrabrain,
		// Token: 0x04003673 RID: 13939
		OfficeBaseMiniMap,
		// Token: 0x04003674 RID: 13940
		OfficeMiniMap,
		// Token: 0x04003675 RID: 13941
		Rearm,
		// Token: 0x04003676 RID: 13942
		Extract,
		// Token: 0x04003677 RID: 13943
		TrimEntry,
		// Token: 0x04003678 RID: 13944
		Operation,
		// Token: 0x04003679 RID: 13945
		Episode,
		// Token: 0x0400367A RID: 13946
		CounterCaseList,
		// Token: 0x0400367B RID: 13947
		Operation_Summary,
		// Token: 0x0400367C RID: 13948
		Operation_MainStream,
		// Token: 0x0400367D RID: 13949
		Operation_SubStream,
		// Token: 0x0400367E RID: 13950
		Operation_SubStream_Popup,
		// Token: 0x0400367F RID: 13951
		Operation_Growth,
		// Token: 0x04003680 RID: 13952
		Operation_Growth_Supply,
		// Token: 0x04003681 RID: 13953
		Operation_Growth_Challenge,
		// Token: 0x04003682 RID: 13954
		Operation_Challenge,
		// Token: 0x04003683 RID: 13955
		Operation_Favorite
	}
}
