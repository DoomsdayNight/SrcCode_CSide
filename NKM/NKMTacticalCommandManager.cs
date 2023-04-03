using System;
using System.Collections.Generic;
using System.Linq;
using Cs.Logging;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200047B RID: 1147
	public class NKMTacticalCommandManager
	{
		// Token: 0x06001F34 RID: 7988 RVA: 0x0009467C File Offset: 0x0009287C
		public static void LoadFromLUA(string fileName)
		{
			Dictionary<int, NKMTacticalCommandTemplet> dicTacticalCommandTempletByID = NKMTacticalCommandManager.m_dicTacticalCommandTempletByID;
			if (dicTacticalCommandTempletByID != null)
			{
				dicTacticalCommandTempletByID.Clear();
			}
			NKMTacticalCommandManager.m_dicTacticalCommandTempletByID = NKMTempletLoader.LoadDictionary<NKMTacticalCommandTemplet>("AB_SCRIPT", fileName, "m_dicTacticalCommandTempletByID", new Func<NKMLua, NKMTacticalCommandTemplet>(NKMTacticalCommandTemplet.LoadFromLUA));
			if (NKMTacticalCommandManager.m_dicTacticalCommandTempletByID == null)
			{
				Log.ErrorAndExit("[NKMTacticalCommandManager] LoadFromLUA failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMTacticalCommand.cs", 469);
				return;
			}
			NKMTacticalCommandManager.m_dicTacticalCommandTempletByStrID = NKMTacticalCommandManager.m_dicTacticalCommandTempletByID.Values.ToDictionary((NKMTacticalCommandTemplet e) => e.m_TCStrID);
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x00094709 File Offset: 0x00092909
		public static NKMTacticalCommandTemplet GetTacticalCommandTempletByID(int TCID)
		{
			if (NKMTacticalCommandManager.m_dicTacticalCommandTempletByID.ContainsKey(TCID))
			{
				return NKMTacticalCommandManager.m_dicTacticalCommandTempletByID[TCID];
			}
			return null;
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x00094725 File Offset: 0x00092925
		public static NKMTacticalCommandTemplet GetTacticalCommandTempletByStrID(string TCStrID)
		{
			if (NKMTacticalCommandManager.m_dicTacticalCommandTempletByStrID.ContainsKey(TCStrID))
			{
				return NKMTacticalCommandManager.m_dicTacticalCommandTempletByStrID[TCStrID];
			}
			return null;
		}

		// Token: 0x04001FBE RID: 8126
		public static Dictionary<int, NKMTacticalCommandTemplet> m_dicTacticalCommandTempletByID;

		// Token: 0x04001FBF RID: 8127
		public static Dictionary<string, NKMTacticalCommandTemplet> m_dicTacticalCommandTempletByStrID;
	}
}
