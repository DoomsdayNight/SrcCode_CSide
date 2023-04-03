using System;
using Cs.GameLog.CountryDescription;

namespace NKM
{
	// Token: 0x020003E2 RID: 994
	public enum NKM_DUNGEON_END_TYPE
	{
		// Token: 0x04001334 RID: 4916
		[CountryDescription("정상종료", CountryCode.KOR)]
		NORMAL,
		// Token: 0x04001335 RID: 4917
		[CountryDescription("포기", CountryCode.KOR)]
		GIVE_UP,
		// Token: 0x04001336 RID: 4918
		[CountryDescription("타임아웃", CountryCode.KOR)]
		TIME_OUT,
		// Token: 0x04001337 RID: 4919
		[CountryDescription("연결종료", CountryCode.KOR)]
		DISCONNECT
	}
}
