using System;
using Cs.GameLog.CountryDescription;

namespace NKM
{
	// Token: 0x02000502 RID: 1282
	public enum NKM_SCEN_ID
	{
		// Token: 0x04002622 RID: 9762
		[CountryDescription("알수없음", CountryCode.KOR)]
		NSI_INVALID,
		// Token: 0x04002623 RID: 9763
		[CountryDescription("로그인", CountryCode.KOR)]
		NSI_LOGIN,
		// Token: 0x04002624 RID: 9764
		[CountryDescription("로비", CountryCode.KOR)]
		NSI_HOME,
		// Token: 0x04002625 RID: 9765
		[CountryDescription("게임", CountryCode.KOR)]
		NSI_GAME,
		// Token: 0x04002626 RID: 9766
		[CountryDescription("편성", CountryCode.KOR)]
		NSI_TEAM,
		// Token: 0x04002627 RID: 9767
		[CountryDescription("본부", CountryCode.KOR)]
		NSI_BASE,
		// Token: 0x04002628 RID: 9768
		[CountryDescription("채용", CountryCode.KOR)]
		NSI_CONTRACT,
		// Token: 0x04002629 RID: 9769
		[CountryDescription("관리부", CountryCode.KOR)]
		NSI_INVENTORY,
		// Token: 0x0400262A RID: 9770
		[CountryDescription("컷신 시뮬레이터", CountryCode.KOR)]
		NSI_CUTSCENE_SIM,
		// Token: 0x0400262B RID: 9771
		[CountryDescription("작전", CountryCode.KOR)]
		NSI_OPERATION,
		// Token: 0x0400262C RID: 9772
		[CountryDescription("에피소드", CountryCode.KOR)]
		NSI_EPISODE,
		// Token: 0x0400262D RID: 9773
		[CountryDescription("전투대기", CountryCode.KOR)]
		NSI_DUNGEON_ATK_READY,
		// Token: 0x0400262E RID: 9774
		[CountryDescription("유닛(함선) 리스트", CountryCode.KOR)]
		NSI_UNIT_LIST,
		// Token: 0x0400262F RID: 9775
		[CountryDescription("도감", CountryCode.KOR)]
		NSI_COLLECTION,
		// Token: 0x04002630 RID: 9776
		[CountryDescription("전역전투", CountryCode.KOR)]
		NSI_WARFARE_GAME,
		// Token: 0x04002631 RID: 9777
		[CountryDescription("상점", CountryCode.KOR)]
		NSI_SHOP,
		// Token: 0x04002632 RID: 9778
		[CountryDescription("친구", CountryCode.KOR)]
		NSI_FRIEND,
		// Token: 0x04002633 RID: 9779
		[CountryDescription("월드맵", CountryCode.KOR)]
		NSI_WORLDMAP,
		// Token: 0x04002634 RID: 9780
		[CountryDescription("컷신던전", CountryCode.KOR)]
		NSI_CUTSCENE_DUNGEON,
		// Token: 0x04002635 RID: 9781
		[CountryDescription("게임결과", CountryCode.KOR)]
		NSI_GAME_RESULT,
		// Token: 0x04002636 RID: 9782
		[CountryDescription("다이브준비", CountryCode.KOR)]
		NSI_DIVE_READY,
		// Token: 0x04002637 RID: 9783
		[CountryDescription("다이브", CountryCode.KOR)]
		NSI_DIVE,
		// Token: 0x04002638 RID: 9784
		[CountryDescription("다이브결과", CountryCode.KOR)]
		NSI_DIVE_RESULT,
		// Token: 0x04002639 RID: 9785
		[CountryDescription("PVP 인트로", CountryCode.KOR)]
		NSI_GAUNTLET_INTRO,
		// Token: 0x0400263A RID: 9786
		[CountryDescription("PVP 로비", CountryCode.KOR)]
		NSI_GAUNTLET_LOBBY,
		// Token: 0x0400263B RID: 9787
		[CountryDescription("PVP 매치대기", CountryCode.KOR)]
		NSI_GAUNTLET_MATCH_READY,
		// Token: 0x0400263C RID: 9788
		[CountryDescription("PVP 매치", CountryCode.KOR)]
		NSI_GAUNTLET_MATCH,
		// Token: 0x0400263D RID: 9789
		[CountryDescription("PVP 전략전 준비", CountryCode.KOR)]
		NSI_GAUNTLET_ASYNC_READY,
		// Token: 0x0400263E RID: 9790
		[CountryDescription("레이드", CountryCode.KOR)]
		NSI_RAID,
		// Token: 0x0400263F RID: 9791
		[CountryDescription("레이드 준비", CountryCode.KOR)]
		NSI_RAID_READY,
		// Token: 0x04002640 RID: 9792
		[CountryDescription("보이스리스트", CountryCode.KOR)]
		NSI_VOICE_LIST,
		// Token: 0x04002641 RID: 9793
		[CountryDescription("그림자 전당", CountryCode.KOR)]
		NSI_SHADOW_PALACE,
		// Token: 0x04002642 RID: 9794
		[CountryDescription("그림자 전당 미션", CountryCode.KOR)]
		NSI_SHADOW_BATTLE,
		// Token: 0x04002643 RID: 9795
		[CountryDescription("그림자 전당 결과", CountryCode.KOR)]
		NSI_SHADOW_RESULT,
		// Token: 0x04002644 RID: 9796
		[CountryDescription("컨소시움 인트로", CountryCode.KOR)]
		NSI_GUILD_INTRO,
		// Token: 0x04002645 RID: 9797
		[CountryDescription("컨소시움 로비", CountryCode.KOR)]
		NSI_GUILD_LOBBY,
		// Token: 0x04002646 RID: 9798
		[CountryDescription("격전 지원", CountryCode.KOR)]
		NSI_FIERCE_BATTLE_SUPPORT,
		// Token: 0x04002647 RID: 9799
		[CountryDescription("길드 협력전", CountryCode.KOR)]
		NSI_GUILD_COOP,
		// Token: 0x04002648 RID: 9800
		[CountryDescription("PVP 친선전 준비", CountryCode.KOR)]
		NSI_GAUNTLET_PRIVATE_READY,
		// Token: 0x04002649 RID: 9801
		[CountryDescription("사옥", CountryCode.KOR)]
		NSI_OFFICE,
		// Token: 0x0400264A RID: 9802
		[CountryDescription("PVP 리그전 룸", CountryCode.KOR)]
		NSI_GAUNTLET_LEAGUE_ROOM,
		// Token: 0x0400264B RID: 9803
		[CountryDescription("PVP 친선전 룸", CountryCode.KOR)]
		NSI_GAUNTLET_PRIVATE_ROOM,
		// Token: 0x0400264C RID: 9804
		[CountryDescription("PVP 친선전 룸 덱 구성", CountryCode.KOR)]
		NSI_GAUNTLET_PRIVATE_ROOM_DECK_SELECT,
		// Token: 0x0400264D RID: 9805
		[CountryDescription("디멘션 트리밍", CountryCode.KOR)]
		NSI_TRIM,
		// Token: 0x0400264E RID: 9806
		[CountryDescription("디멘션 트리밍 결과", CountryCode.KOR)]
		NSI_TRIM_RESULT,
		// Token: 0x0400264F RID: 9807
		[CountryDescription("던전 결과", CountryCode.KOR)]
		NSI_DUNGEON_RESULT
	}
}
