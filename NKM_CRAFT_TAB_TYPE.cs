using System;
using Cs.GameLog.CountryDescription;

namespace NKM
{
	// Token: 0x02000383 RID: 899
	public enum NKM_CRAFT_TAB_TYPE
	{
		// Token: 0x04000F55 RID: 3925
		[CountryDescription("장비", CountryCode.KOR)]
		MT_EQUIP,
		// Token: 0x04000F56 RID: 3926
		[CountryDescription("초월 재료", CountryCode.KOR)]
		MT_LIMITBREAK,
		// Token: 0x04000F57 RID: 3927
		[CountryDescription("이벤트", CountryCode.KOR)]
		MT_MISC_EVENT_1,
		// Token: 0x04000F58 RID: 3928
		[CountryDescription("이벤트", CountryCode.KOR)]
		MT_MISC_EVENT_2,
		// Token: 0x04000F59 RID: 3929
		[CountryDescription("이벤트", CountryCode.KOR)]
		MT_MISC_EVENT_3,
		// Token: 0x04000F5A RID: 3930
		[CountryDescription("비품", CountryCode.KOR)]
		MT_MISC_1,
		// Token: 0x04000F5B RID: 3931
		[CountryDescription("비품", CountryCode.KOR)]
		MT_MISC_2,
		// Token: 0x04000F5C RID: 3932
		[CountryDescription("비품", CountryCode.KOR)]
		MT_MISC_3,
		// Token: 0x04000F5D RID: 3933
		[CountryDescription("전용장비", CountryCode.KOR)]
		MT_EQUIP_PRIVATE,
		// Token: 0x04000F5E RID: 3934
		[CountryDescription("렐릭장비", CountryCode.KOR)]
		MT_EQUIP_RELIC,
		// Token: 0x04000F5F RID: 3935
		[CountryDescription("레이드장비", CountryCode.KOR)]
		MT_EQUIP_RAID,
		// Token: 0x04000F60 RID: 3936
		[CountryDescription("함선", CountryCode.KOR)]
		MT_SHIP
	}
}
