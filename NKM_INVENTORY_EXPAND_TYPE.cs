using System;
using Cs.GameLog.CountryDescription;

namespace NKM
{
	// Token: 0x02000422 RID: 1058
	public enum NKM_INVENTORY_EXPAND_TYPE
	{
		// Token: 0x04001BCB RID: 7115
		[CountryDescription("일반인벤", CountryCode.KOR)]
		NIET_NONE,
		// Token: 0x04001BCC RID: 7116
		[CountryDescription("장비인벤", CountryCode.KOR)]
		NIET_EQUIP,
		// Token: 0x04001BCD RID: 7117
		[CountryDescription("유닛인벤", CountryCode.KOR)]
		NIET_UNIT,
		// Token: 0x04001BCE RID: 7118
		[CountryDescription("함선인벤", CountryCode.KOR)]
		NIET_SHIP,
		// Token: 0x04001BCF RID: 7119
		[CountryDescription("오퍼레이터인벤", CountryCode.KOR)]
		NIET_OPERATOR,
		// Token: 0x04001BD0 RID: 7120
		[CountryDescription("트로피인벤", CountryCode.KOR)]
		NIET_TROPHY,
		// Token: 0x04001BD1 RID: 7121
		[CountryDescription("알수없음", CountryCode.KOR)]
		NEIT_MAX
	}
}
