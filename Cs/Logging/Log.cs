using System;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Cs.Logging
{
	// Token: 0x020010D2 RID: 4306
	public static class Log
	{
		// Token: 0x06009E16 RID: 40470 RVA: 0x0033A368 File Offset: 0x00338568
		public static void Info(string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			UnityEngine.Debug.Log(string.Concat(new string[]
			{
				message,
				" (",
				Path.GetFileName(file),
				":",
				line.ToString(),
				")"
			}));
		}

		// Token: 0x06009E17 RID: 40471 RVA: 0x0033A3B4 File Offset: 0x003385B4
		public static void Info(string message, UnityEngine.Object context, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			UnityEngine.Debug.Log(string.Concat(new string[]
			{
				message,
				" (",
				Path.GetFileName(file),
				":",
				line.ToString(),
				")"
			}), context);
		}

		// Token: 0x06009E18 RID: 40472 RVA: 0x0033A404 File Offset: 0x00338604
		public static void Debug(string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			UnityEngine.Debug.Log(string.Concat(new string[]
			{
				message,
				" (",
				Path.GetFileName(file),
				":",
				line.ToString(),
				")"
			}));
		}

		// Token: 0x06009E19 RID: 40473 RVA: 0x0033A450 File Offset: 0x00338650
		public static void Debug(string message, UnityEngine.Object context, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			UnityEngine.Debug.Log(string.Concat(new string[]
			{
				message,
				" (",
				Path.GetFileName(file),
				":",
				line.ToString(),
				")"
			}), context);
		}

		// Token: 0x06009E1A RID: 40474 RVA: 0x0033A49D File Offset: 0x0033869D
		public static void DebugBold(string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			Log.Debug(message, file, line);
		}

		// Token: 0x06009E1B RID: 40475 RVA: 0x0033A4A8 File Offset: 0x003386A8
		public static void Warn(string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			UnityEngine.Debug.LogWarning(string.Concat(new string[]
			{
				message,
				" (",
				Path.GetFileName(file),
				":",
				line.ToString(),
				")"
			}));
		}

		// Token: 0x06009E1C RID: 40476 RVA: 0x0033A4F4 File Offset: 0x003386F4
		public static void Warn(string message, UnityEngine.Object context, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			UnityEngine.Debug.LogWarning(string.Concat(new string[]
			{
				message,
				" (",
				Path.GetFileName(file),
				":",
				line.ToString(),
				")"
			}), context);
		}

		// Token: 0x06009E1D RID: 40477 RVA: 0x0033A544 File Offset: 0x00338744
		public static void Error(string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			UnityEngine.Debug.LogError(string.Concat(new string[]
			{
				message,
				" (",
				Path.GetFileName(file),
				":",
				line.ToString(),
				")"
			}));
		}

		// Token: 0x06009E1E RID: 40478 RVA: 0x0033A590 File Offset: 0x00338790
		public static void Error(string message, UnityEngine.Object context, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			UnityEngine.Debug.LogError(string.Concat(new string[]
			{
				message,
				" (",
				Path.GetFileName(file),
				":",
				line.ToString(),
				")"
			}), context);
		}

		// Token: 0x06009E1F RID: 40479 RVA: 0x0033A5DD File Offset: 0x003387DD
		public static void ErrorFormat(string message, params object[] args)
		{
			UnityEngine.Debug.LogErrorFormat(message ?? "", args);
		}

		// Token: 0x06009E20 RID: 40480 RVA: 0x0033A5F0 File Offset: 0x003387F0
		public static void ErrorAndExit(string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			UnityEngine.Debug.LogError(string.Concat(new string[]
			{
				message,
				" (",
				Path.GetFileName(file),
				":",
				line.ToString(),
				")"
			}));
		}
	}
}
