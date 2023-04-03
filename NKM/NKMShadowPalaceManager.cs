using System;
using System.Collections.Generic;
using System.Linq;
using NKC;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;

namespace NKM
{
	// Token: 0x02000462 RID: 1122
	public class NKMShadowPalaceManager
	{
		// Token: 0x06001E5F RID: 7775 RVA: 0x000903A2 File Offset: 0x0008E5A2
		public static List<int> GetAllShadowTempletId()
		{
			return NKMTempletContainer<NKMShadowPalaceTemplet>.Keys.ToList<int>();
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x000903B0 File Offset: 0x0008E5B0
		public static bool LoadFromLua()
		{
			NKMTempletContainer<NKMShadowPalaceTemplet>.Load("AB_SCRIPT", "LUA_SHADOW_PALACE_TEMPLET", "SHADOW_PALACE_TEMPLET", new Func<NKMLua, NKMShadowPalaceTemplet>(NKMShadowPalaceTemplet.LoadFromLUA));
			NKMShadowPalaceManager.dicShadowBattleTemplet = NKMTempletLoader<NKMShadowBattleTemplet>.LoadGroup("AB_SCRIPT", "LUA_SHADOW_BATTLE_TEMPLET", "SHADOW_BATTLE_TEMPLET", new Func<NKMLua, NKMShadowBattleTemplet>(NKMShadowBattleTemplet.LoadFromLUA));
			return NKMShadowPalaceManager.dicShadowBattleTemplet != null;
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x0009040C File Offset: 0x0008E60C
		public static List<NKMShadowBattleTemplet> GetBattleTemplets(int palaceID)
		{
			NKMShadowPalaceTemplet nkmshadowPalaceTemplet = NKMTempletContainer<NKMShadowPalaceTemplet>.Find(palaceID);
			List<NKMShadowBattleTemplet> result;
			if (nkmshadowPalaceTemplet != null && NKMShadowPalaceManager.dicShadowBattleTemplet.TryGetValue(nkmshadowPalaceTemplet.BATTLE_GROUP_ID, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06001E62 RID: 7778 RVA: 0x0009043A File Offset: 0x0008E63A
		public static NKMShadowPalaceTemplet GetPalaceTemplet(int palaceID)
		{
			return NKMTempletContainer<NKMShadowPalaceTemplet>.Find(palaceID);
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x00090442 File Offset: 0x0008E642
		private static string GetLastClearedPalaceKey(NKMUserData userData)
		{
			return "LAST_CLEARED_PALACE_" + userData.m_UserUID.ToString();
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x00090459 File Offset: 0x0008E659
		public static void SaveLastClearedPalace(int palaceID)
		{
			PlayerPrefs.SetInt(NKMShadowPalaceManager.GetLastClearedPalaceKey(NKCScenManager.CurrentUserData()), palaceID);
			PlayerPrefs.Save();
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x00090470 File Offset: 0x0008E670
		public static int GetLastClearedPalace()
		{
			return PlayerPrefs.GetInt(NKMShadowPalaceManager.GetLastClearedPalaceKey(NKCScenManager.CurrentUserData()), 0);
		}

		// Token: 0x04001F05 RID: 7941
		private static Dictionary<int, List<NKMShadowBattleTemplet>> dicShadowBattleTemplet = new Dictionary<int, List<NKMShadowBattleTemplet>>();
	}
}
