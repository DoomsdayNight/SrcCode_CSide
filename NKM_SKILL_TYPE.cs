using System;
using Cs.GameLog.CountryDescription;

namespace NKM
{
	// Token: 0x020004B3 RID: 1203
	public enum NKM_SKILL_TYPE : short
	{
		// Token: 0x04002246 RID: 8774
		[CountryDescription("알수없음", CountryCode.KOR)]
		NST_INVALID,
		// Token: 0x04002247 RID: 8775
		[CountryDescription("패시브", CountryCode.KOR)]
		NST_PASSIVE,
		// Token: 0x04002248 RID: 8776
		[CountryDescription("공격스킬", CountryCode.KOR)]
		NST_ATTACK,
		// Token: 0x04002249 RID: 8777
		[CountryDescription("액티브", CountryCode.KOR)]
		NST_SKILL,
		// Token: 0x0400224A RID: 8778
		[CountryDescription("궁극기", CountryCode.KOR)]
		NST_HYPER,
		// Token: 0x0400224B RID: 8779
		[CountryDescription("함선스킬", CountryCode.KOR)]
		NST_SHIP_ACTIVE,
		// Token: 0x0400224C RID: 8780
		[CountryDescription("리더스킬", CountryCode.KOR)]
		NST_LEADER
	}
}
