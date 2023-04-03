using System;
using Cs.GameLog.CountryDescription;

namespace NKM
{
	// Token: 0x02000381 RID: 897
	public enum NKM_ITEM_PAYMENT_TYPE : byte
	{
		// Token: 0x04000F4D RID: 3917
		[CountryDescription("무료", CountryCode.KOR)]
		NIPT_FREE,
		// Token: 0x04000F4E RID: 3918
		[CountryDescription("유료", CountryCode.KOR)]
		NIPT_PAID,
		// Token: 0x04000F4F RID: 3919
		[CountryDescription("무료,유료", CountryCode.KOR)]
		NIPT_BOTH
	}
}
