using System;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200080B RID: 2059
	public static class PlayerPrefsContainer
	{
		// Token: 0x060051B3 RID: 20915 RVA: 0x0018CC61 File Offset: 0x0018AE61
		public static void Set(string key, int value)
		{
			PlayerPrefs.SetInt(key, value);
			PlayerPrefs.Save();
		}

		// Token: 0x060051B4 RID: 20916 RVA: 0x0018CC6F File Offset: 0x0018AE6F
		public static void Set(string key, float value)
		{
			PlayerPrefs.SetFloat(key, value);
			PlayerPrefs.Save();
		}

		// Token: 0x060051B5 RID: 20917 RVA: 0x0018CC7D File Offset: 0x0018AE7D
		public static void Set(string key, string value)
		{
			PlayerPrefs.SetString(key, value);
			PlayerPrefs.Save();
		}

		// Token: 0x060051B6 RID: 20918 RVA: 0x0018CC8B File Offset: 0x0018AE8B
		public static void Set(string key, bool value)
		{
			PlayerPrefs.SetInt(key, value ? 1 : 0);
			PlayerPrefs.Save();
		}

		// Token: 0x060051B7 RID: 20919 RVA: 0x0018CC9F File Offset: 0x0018AE9F
		public static int GetInt(string key)
		{
			return PlayerPrefs.GetInt(key);
		}

		// Token: 0x060051B8 RID: 20920 RVA: 0x0018CCA7 File Offset: 0x0018AEA7
		public static string GetString(string key)
		{
			return PlayerPrefs.GetString(key);
		}

		// Token: 0x060051B9 RID: 20921 RVA: 0x0018CCAF File Offset: 0x0018AEAF
		public static bool GetBoolean(string key)
		{
			return PlayerPrefs.GetInt(key) > 0;
		}
	}
}
