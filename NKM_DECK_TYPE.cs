using System;
using Cs.GameLog.CountryDescription;

namespace NKM
{
	// Token: 0x020003D7 RID: 983
	public enum NKM_DECK_TYPE : byte
	{
		// Token: 0x040012F6 RID: 4854
		[CountryDescription("소대타입 없음", CountryCode.KOR)]
		NDT_NONE,
		// Token: 0x040012F7 RID: 4855
		[CountryDescription("전역 소대", CountryCode.KOR)]
		NDT_NORMAL,
		// Token: 0x040012F8 RID: 4856
		[CountryDescription("경쟁전 소대", CountryCode.KOR)]
		NDT_PVP,
		// Token: 0x040012F9 RID: 4857
		[CountryDescription("전투 소대", CountryCode.KOR)]
		NDT_DAILY,
		// Token: 0x040012FA RID: 4858
		[CountryDescription("레이드 소대", CountryCode.KOR)]
		NDT_RAID,
		// Token: 0x040012FB RID: 4859
		[CountryDescription("친구 소대", CountryCode.KOR)]
		NDT_FRIEND,
		// Token: 0x040012FC RID: 4860
		[CountryDescription("전략전 방어소대", CountryCode.KOR)]
		NDT_PVP_DEFENCE,
		// Token: 0x040012FD RID: 4861
		[CountryDescription("트리밍 던전 소대", CountryCode.KOR)]
		NDT_TRIM,
		// Token: 0x040012FE RID: 4862
		[CountryDescription("다이브 소대", CountryCode.KOR)]
		NDT_DIVE
	}
}
