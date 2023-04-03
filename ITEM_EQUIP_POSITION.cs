using System;
using Cs.GameLog.CountryDescription;

namespace NKM
{
	// Token: 0x02000384 RID: 900
	public enum ITEM_EQUIP_POSITION : short
	{
		// Token: 0x04000F62 RID: 3938
		[CountryDescription("무기", CountryCode.KOR)]
		IEP_WEAPON,
		// Token: 0x04000F63 RID: 3939
		[CountryDescription("방어구", CountryCode.KOR)]
		IEP_DEFENCE,
		// Token: 0x04000F64 RID: 3940
		[CountryDescription("보조 장비", CountryCode.KOR)]
		IEP_ACC,
		// Token: 0x04000F65 RID: 3941
		[CountryDescription("보조 장비2", CountryCode.KOR)]
		IEP_ACC2,
		// Token: 0x04000F66 RID: 3942
		IEP_MAX,
		// Token: 0x04000F67 RID: 3943
		[CountryDescription("강화 모듈", CountryCode.KOR)]
		IEP_ENCHANT,
		// Token: 0x04000F68 RID: 3944
		[CountryDescription("알수없음", CountryCode.KOR)]
		IEP_NONE
	}
}
