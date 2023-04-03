using System;
using System.Collections.Generic;
using System.Linq;
using Cs.Logging;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x020003B3 RID: 947
	public class NKMCommonUnitEvent
	{
		// Token: 0x060018EA RID: 6378 RVA: 0x00066334 File Offset: 0x00064534
		public static bool LoadFromLUA(string fileName, bool bReload)
		{
			if (bReload)
			{
				Dictionary<string, NKMEventHeal> dicEventHeal = NKMCommonUnitEvent.m_dicEventHeal;
				if (dicEventHeal != null)
				{
					dicEventHeal.Clear();
				}
			}
			IEnumerable<NKMEventHeal> enumerable = NKMTempletLoader.LoadCommonPath<NKMEventHeal>("AB_SCRIPT", fileName, "m_dicEventHeal", new Func<NKMLua, NKMEventHeal>(NKMEventHeal.LoadFromLUAStatic));
			if (enumerable != null)
			{
				NKMCommonUnitEvent.m_dicEventHeal = enumerable.ToDictionary((NKMEventHeal e) => e.m_EventStrID, (NKMEventHeal e) => e);
			}
			return NKMCommonUnitEvent.m_dicEventHeal != null;
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x000663C5 File Offset: 0x000645C5
		public static NKMEventHeal GetNKMEventHeal(string eventID)
		{
			if (NKMCommonUnitEvent.m_dicEventHeal.ContainsKey(eventID))
			{
				return NKMCommonUnitEvent.m_dicEventHeal[eventID];
			}
			Log.Error("NKMCommonUnitEvent GetNKMEventHeal no m_EventStrID " + eventID, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMCommonUnitEvent.cs", 38);
			return null;
		}

		// Token: 0x0400114D RID: 4429
		public static Dictionary<string, NKMEventHeal> m_dicEventHeal;
	}
}
