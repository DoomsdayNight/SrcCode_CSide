using System;
using System.Collections.Generic;
using NKM.Templet;

namespace NKM
{
	// Token: 0x0200052E RID: 1326
	public static class NKMWarfareMapContainer
	{
		// Token: 0x060025CF RID: 9679 RVA: 0x000C2BF4 File Offset: 0x000C0DF4
		public static NKMWarfareMapTemplet GetOrLoad(string strId)
		{
			if (!NKMWarfareMapContainer.mapTemplates.ContainsKey(strId))
			{
				NKMWarfareMapContainer.LoadFromLUA_LUA_WARFARE_MAP_TEMPLET(strId);
			}
			NKMWarfareMapTemplet result;
			NKMWarfareMapContainer.mapTemplates.TryGetValue(strId, out result);
			return result;
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x000C2C24 File Offset: 0x000C0E24
		public static NKMWarfareMapTemplet ForceLoad(string strId)
		{
			NKMWarfareMapContainer.LoadFromLUA_LUA_WARFARE_MAP_TEMPLET(strId);
			NKMWarfareMapTemplet result;
			NKMWarfareMapContainer.mapTemplates.TryGetValue(strId, out result);
			return result;
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x000C2C48 File Offset: 0x000C0E48
		private static bool LoadFromLUA_LUA_WARFARE_MAP_TEMPLET(string strID)
		{
			using (NKMLua nkmlua = new NKMLua())
			{
				if (!nkmlua.LoadCommonPath("AB_SCRIPT_WARFARE_MAP_TEMPLET_ALL", strID, true))
				{
					return false;
				}
				NKMWarfareMapTemplet nkmwarfareMapTemplet = new NKMWarfareMapTemplet();
				if (!nkmlua.OpenTable("NKMWarfareMapTemplet"))
				{
					return false;
				}
				if (!nkmwarfareMapTemplet.LoadFromLUA(nkmlua, strID))
				{
					return false;
				}
				if (NKMWarfareMapContainer.mapTemplates.ContainsKey(strID))
				{
					NKMWarfareMapContainer.mapTemplates[strID] = nkmwarfareMapTemplet;
				}
				else
				{
					NKMWarfareMapContainer.mapTemplates.Add(nkmwarfareMapTemplet.m_WarfareMapStrID, nkmwarfareMapTemplet);
				}
			}
			return true;
		}

		// Token: 0x04002747 RID: 10055
		public static readonly Dictionary<string, NKMWarfareMapTemplet> mapTemplates = new Dictionary<string, NKMWarfareMapTemplet>();
	}
}
