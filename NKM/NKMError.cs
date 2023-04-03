using System;
using System.Runtime.CompilerServices;
using Cs.Logging;

namespace NKM
{
	// Token: 0x020003F9 RID: 1017
	public static class NKMError
	{
		// Token: 0x06001B04 RID: 6916 RVA: 0x00076C71 File Offset: 0x00074E71
		public static NKM_ERROR_CODE Build(NKM_ERROR_CODE code, string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			if (code == NKM_ERROR_CODE.NEC_OK)
			{
				return code;
			}
			Log.Error(string.Format("[{0}] {1}", code, message), file, line);
			return code;
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x00076C91 File Offset: 0x00074E91
		public static NKM_ERROR_CODE Build(NKM_ERROR_CODE code, long userUid, string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			if (code == NKM_ERROR_CODE.NEC_OK)
			{
				return code;
			}
			Log.Error(string.Format("[{0}] [userUid:{1}] {2}", code, userUid, message), file, line);
			return code;
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x00076CB8 File Offset: 0x00074EB8
		public static NKM_ERROR_CODE Build(NKM_ERROR_CODE code, long userUid, long gameUid, string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			if (code == NKM_ERROR_CODE.NEC_OK)
			{
				return code;
			}
			Log.Error(string.Format("[{0}] [userUid:{1}] [gameUid:{2} {3}", new object[]
			{
				code,
				userUid,
				gameUid,
				message
			}), file, line);
			return code;
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x00076CF8 File Offset: 0x00074EF8
		public static NKM_ERROR_CODE Build(NKM_ERROR_CODE code, NKMUserData userData, string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			if (code == NKM_ERROR_CODE.NEC_OK)
			{
				return code;
			}
			Log.Error(string.Format("[{0}] [userUid:{1}] {2}", code, (userData != null) ? new long?(userData.m_UserUID) : null, message), file, line);
			return code;
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x00076D44 File Offset: 0x00074F44
		public static NKM_ERROR_CODE Build(NKM_ERROR_CODE code, NKMUserData userData, NKMGameData gameData, string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			if (code == NKM_ERROR_CODE.NEC_OK)
			{
				return code;
			}
			Log.Error(string.Format("[{0}] [userUid:{1}] [gameUid:{2}] {3}", new object[]
			{
				code,
				(userData != null) ? new long?(userData.m_UserUID) : null,
				(gameData != null) ? new long?(gameData.m_GameUID) : null,
				message
			}), file, line);
			return code;
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x00076DBF File Offset: 0x00074FBF
		public static NKM_ERROR_CODE Warning(NKM_ERROR_CODE code, string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			if (code == NKM_ERROR_CODE.NEC_OK)
			{
				return code;
			}
			Log.Warn(string.Format("[{0}] {1}", code, message), file, line);
			return code;
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x00076DDF File Offset: 0x00074FDF
		public static NKM_ERROR_CODE Warning(NKM_ERROR_CODE code, long userUid, string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			if (code == NKM_ERROR_CODE.NEC_OK)
			{
				return code;
			}
			Log.Warn(string.Format("[{0}] [userUid:{1}] {2}", code, userUid, message), file, line);
			return code;
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x00076E06 File Offset: 0x00075006
		public static NKM_ERROR_CODE Warning(NKM_ERROR_CODE code, long userUid, long gameUid, string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			if (code == NKM_ERROR_CODE.NEC_OK)
			{
				return code;
			}
			Log.Warn(string.Format("[{0}] [userUid:{1}] [gameUid:{2} {3}", new object[]
			{
				code,
				userUid,
				gameUid,
				message
			}), file, line);
			return code;
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x00076E48 File Offset: 0x00075048
		public static NKM_ERROR_CODE Warning(NKM_ERROR_CODE code, NKMUserData userData, string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			if (code == NKM_ERROR_CODE.NEC_OK)
			{
				return code;
			}
			Log.Warn(string.Format("[{0}] [userUid:{1}] {2}", code, (userData != null) ? new long?(userData.m_UserUID) : null, message), file, line);
			return code;
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x00076E94 File Offset: 0x00075094
		public static NKM_ERROR_CODE Warning(NKM_ERROR_CODE code, NKMUserData userData, NKMGameData gameData, string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			if (code == NKM_ERROR_CODE.NEC_OK)
			{
				return code;
			}
			Log.Warn(string.Format("[{0}] [userUid:{1}] [gameUid:{2}] {3}", new object[]
			{
				code,
				(userData != null) ? new long?(userData.m_UserUID) : null,
				(gameData != null) ? new long?(gameData.m_GameUID) : null,
				message
			}), file, line);
			return code;
		}
	}
}
