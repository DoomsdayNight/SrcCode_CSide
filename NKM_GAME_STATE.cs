using System;
using Cs.GameLog.CountryDescription;

namespace NKM
{
	// Token: 0x020003FE RID: 1022
	public enum NKM_GAME_STATE : byte
	{
		// Token: 0x04001A97 RID: 6807
		NGS_INVALID,
		// Token: 0x04001A98 RID: 6808
		[CountryDescription("던전 미참여", CountryCode.KOR)]
		NGS_STOP,
		// Token: 0x04001A99 RID: 6809
		[CountryDescription("던전 플레이 시작", CountryCode.KOR)]
		NGS_START,
		// Token: 0x04001A9A RID: 6810
		[CountryDescription("던전 플레이 중", CountryCode.KOR)]
		NGS_PLAY,
		// Token: 0x04001A9B RID: 6811
		[CountryDescription("던전 플레이 종료", CountryCode.KOR)]
		NGS_FINISH,
		// Token: 0x04001A9C RID: 6812
		[CountryDescription("던전 종료", CountryCode.KOR)]
		NGS_END,
		// Token: 0x04001A9D RID: 6813
		[CountryDescription("방생성-파티매칭", CountryCode.KOR)]
		NGS_LOBBY_MATCHING,
		// Token: 0x04001A9E RID: 6814
		[CountryDescription("방생성-게임편성", CountryCode.KOR)]
		NGS_LOBBY_GAME_SETTING
	}
}
