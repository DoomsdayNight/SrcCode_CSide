using System;
using Cs.GameLog.CountryDescription;

namespace NKM
{
	// Token: 0x020003FD RID: 1021
	public enum NKM_GAME_TYPE : byte
	{
		// Token: 0x04001A7E RID: 6782
		[CountryDescription("알수없음", CountryCode.KOR)]
		[CountryDescription("沒有內容", CountryCode.TWN)]
		NGT_INVALID,
		// Token: 0x04001A7F RID: 6783
		[CountryDescription("개발용", CountryCode.KOR)]
		[CountryDescription("研發用", CountryCode.TWN)]
		NGT_DEV,
		// Token: 0x04001A80 RID: 6784
		[CountryDescription("연습모드", CountryCode.KOR)]
		[CountryDescription("練習模式", CountryCode.TWN)]
		NGT_PRACTICE,
		// Token: 0x04001A81 RID: 6785
		[CountryDescription("일반던전", CountryCode.KOR)]
		[CountryDescription("一般副本", CountryCode.TWN)]
		NGT_DUNGEON,
		// Token: 0x04001A82 RID: 6786
		[CountryDescription("전역", CountryCode.KOR)]
		[CountryDescription("戰役", CountryCode.TWN)]
		NGT_WARFARE,
		// Token: 0x04001A83 RID: 6787
		[CountryDescription("다이브", CountryCode.KOR)]
		[CountryDescription("躍入", CountryCode.TWN)]
		NGT_DIVE,
		// Token: 0x04001A84 RID: 6788
		[CountryDescription("랭크전", CountryCode.KOR)]
		[CountryDescription("排位賽", CountryCode.TWN)]
		NGT_PVP_RANK,
		// Token: 0x04001A85 RID: 6789
		[CountryDescription("튜토리얼", CountryCode.KOR)]
		[CountryDescription("新手教學", CountryCode.TWN)]
		NGT_TUTORIAL,
		// Token: 0x04001A86 RID: 6790
		[CountryDescription("레이드", CountryCode.KOR)]
		[CountryDescription("團體副本", CountryCode.TWN)]
		NGT_RAID,
		// Token: 0x04001A87 RID: 6791
		[CountryDescription("컷신", CountryCode.KOR)]
		[CountryDescription("過場動畫", CountryCode.TWN)]
		NGT_CUTSCENE,
		// Token: 0x04001A88 RID: 6792
		[CountryDescription("월드맵", CountryCode.KOR)]
		[CountryDescription("世界地圖", CountryCode.TWN)]
		NGT_WORLDMAP,
		// Token: 0x04001A89 RID: 6793
		[CountryDescription("전략전", CountryCode.KOR)]
		[CountryDescription("戰略對抗戰", CountryCode.TWN)]
		NGT_ASYNC_PVP,
		// Token: 0x04001A8A RID: 6794
		[CountryDescription("솔로 레이드", CountryCode.KOR)]
		[CountryDescription("單人團體副本", CountryCode.TWN)]
		NGT_RAID_SOLO,
		// Token: 0x04001A8B RID: 6795
		[CountryDescription("그림자 전당", CountryCode.KOR)]
		[CountryDescription("暗影殿堂", CountryCode.TWN)]
		NGT_SHADOW_PALACE,
		// Token: 0x04001A8C RID: 6796
		[CountryDescription("격전 지원", CountryCode.KOR)]
		[CountryDescription("激戰支援", CountryCode.TWN)]
		NGT_FIERCE,
		// Token: 0x04001A8D RID: 6797
		[CountryDescription("페이즈", CountryCode.KOR)]
		[CountryDescription("階段", CountryCode.TWN)]
		NGT_PHASE,
		// Token: 0x04001A8E RID: 6798
		[CountryDescription("길드 협력전 아레나", CountryCode.KOR)]
		[CountryDescription("(TWN)길드 협력전 아레나", CountryCode.TWN)]
		NGT_GUILD_DUNGEON_ARENA,
		// Token: 0x04001A8F RID: 6799
		[CountryDescription("길드 협력전 보스", CountryCode.KOR)]
		[CountryDescription("(TWN)길드 협력전 보스", CountryCode.TWN)]
		NGT_GUILD_DUNGEON_BOSS,
		// Token: 0x04001A90 RID: 6800
		[CountryDescription("친선전", CountryCode.KOR)]
		[CountryDescription("(TWN)친선전", CountryCode.TWN)]
		NGT_PVP_PRIVATE,
		// Token: 0x04001A91 RID: 6801
		[CountryDescription("리그전", CountryCode.KOR)]
		[CountryDescription("(TWN)리그전", CountryCode.TWN)]
		NGT_PVP_LEAGUE,
		// Token: 0x04001A92 RID: 6802
		[CountryDescription("전략전 개편", CountryCode.KOR)]
		[CountryDescription("(TWN)전략전 개편", CountryCode.TWN)]
		NGT_PVP_STRATEGY,
		// Token: 0x04001A93 RID: 6803
		[CountryDescription("전략전 리벤지", CountryCode.KOR)]
		[CountryDescription("(TWN)전략전 리벤지", CountryCode.TWN)]
		NGT_PVP_STRATEGY_REVENGE,
		// Token: 0x04001A94 RID: 6804
		[CountryDescription("전략전 NPC", CountryCode.KOR)]
		[CountryDescription("(TWN)전략전 NPC", CountryCode.TWN)]
		NGT_PVP_STRATEGY_NPC,
		// Token: 0x04001A95 RID: 6805
		[CountryDescription("디멘션 트리밍", CountryCode.KOR)]
		[CountryDescription("(TWN)디멘션 트리밍", CountryCode.TWN)]
		NGT_TRIM
	}
}
