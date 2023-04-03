using System;

namespace NKM
{
	// Token: 0x020003A4 RID: 932
	public static class AuthLevelExt
	{
		// Token: 0x0600186F RID: 6255 RVA: 0x00062A7D File Offset: 0x00060C7D
		public static bool IsNormalUser(this NKM_USER_AUTH_LEVEL value)
		{
			return value == NKM_USER_AUTH_LEVEL.NORMAL_USER;
		}
	}
}
