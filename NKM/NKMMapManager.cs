using System;
using System.Collections.Generic;
using System.Linq;
using Cs.Logging;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200042C RID: 1068
	public class NKMMapManager
	{
		// Token: 0x06001D16 RID: 7446 RVA: 0x00087840 File Offset: 0x00085A40
		public static bool LoadFromLUA(string fileName, bool bReload = false)
		{
			if (bReload)
			{
				Dictionary<int, NKMMapTemplet> dicNKMMapTempletID = NKMMapManager.m_dicNKMMapTempletID;
				if (dicNKMMapTempletID != null)
				{
					dicNKMMapTempletID.Clear();
				}
				Dictionary<string, NKMMapTemplet> dicNKMMapTempletStrID = NKMMapManager.m_dicNKMMapTempletStrID;
				if (dicNKMMapTempletStrID != null)
				{
					dicNKMMapTempletStrID.Clear();
				}
			}
			NKMMapManager.m_dicNKMMapTempletID = NKMTempletLoader.LoadDictionary<NKMMapTemplet>("AB_SCRIPT", fileName, "m_listNKMMapTemplet", new Func<NKMLua, NKMMapTemplet>(NKMMapTemplet.LoadFromLUA));
			if (NKMMapManager.m_dicNKMMapTempletID != null)
			{
				NKMMapManager.m_dicNKMMapTempletStrID = NKMMapManager.m_dicNKMMapTempletID.ToDictionary((KeyValuePair<int, NKMMapTemplet> e) => e.Value.m_MapStrID, (KeyValuePair<int, NKMMapTemplet> e) => e.Value);
				foreach (KeyValuePair<int, NKMMapTemplet> keyValuePair in NKMMapManager.m_dicNKMMapTempletID)
				{
					if (keyValuePair.Value.m_bUsePVP)
					{
						NKMMapManager.m_listPVPMap.Add(keyValuePair.Value.m_MapID);
					}
				}
			}
			if (NKMMapManager.m_listPVPMap.Count == 0)
			{
				Log.ErrorAndExit("Invalid PvpMapTemplet.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMMapManager.cs", 387);
				return false;
			}
			return NKMMapManager.m_dicNKMMapTempletID != null;
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x00087974 File Offset: 0x00085B74
		public static NKMMapTemplet GetMapTempletByID(int mapID)
		{
			if (NKMMapManager.m_dicNKMMapTempletID.ContainsKey(mapID))
			{
				return NKMMapManager.m_dicNKMMapTempletID[mapID];
			}
			return null;
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x00087990 File Offset: 0x00085B90
		public static NKMMapTemplet GetMapTempletByStrID(string mapStrID)
		{
			if (NKMMapManager.m_dicNKMMapTempletStrID.ContainsKey(mapStrID))
			{
				return NKMMapManager.m_dicNKMMapTempletStrID[mapStrID];
			}
			return null;
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x000879AC File Offset: 0x00085BAC
		public static List<string> GetTotalMapStrID()
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<int, NKMMapTemplet> keyValuePair in NKMMapManager.m_dicNKMMapTempletID)
			{
				NKMMapTemplet value = keyValuePair.Value;
				list.Add(value.m_MapStrID);
			}
			return list;
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x000879F4 File Offset: 0x00085BF4
		public static int GetPVPRandomMap()
		{
			int index = NKMRandom.Range(0, NKMMapManager.m_listPVPMap.Count);
			return NKMMapManager.m_listPVPMap[index];
		}

		// Token: 0x04001C82 RID: 7298
		public static Dictionary<int, NKMMapTemplet> m_dicNKMMapTempletID = null;

		// Token: 0x04001C83 RID: 7299
		public static Dictionary<string, NKMMapTemplet> m_dicNKMMapTempletStrID = null;

		// Token: 0x04001C84 RID: 7300
		public static List<int> m_listPVPMap = new List<int>();
	}
}
