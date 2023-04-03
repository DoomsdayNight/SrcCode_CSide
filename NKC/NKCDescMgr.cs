using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM;
using NKM.Templet;

namespace NKC
{
	// Token: 0x0200066B RID: 1643
	public class NKCDescMgr
	{
		// Token: 0x060033DA RID: 13274 RVA: 0x001045CD File Offset: 0x001027CD
		public static NKCDescTemplet GetDescTemplet(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				return null;
			}
			return NKCDescMgr.GetDescTemplet(unitData.m_UnitID, unitData.m_SkinID);
		}

		// Token: 0x060033DB RID: 13275 RVA: 0x001045E8 File Offset: 0x001027E8
		public static NKCDescTemplet GetDescTemplet(int unitID, int skinID)
		{
			if (NKCDescMgr.m_dicNKCDescTemplet.ContainsKey(unitID))
			{
				NKCDescTemplet result;
				if (NKCDescMgr.m_dicNKCDescTemplet[unitID].TryGetValue(skinID, out result))
				{
					return result;
				}
				NKCDescTemplet result2;
				if (NKCDescMgr.m_dicNKCDescTemplet[unitID].TryGetValue(0, out result2))
				{
					return result2;
				}
			}
			NKMUnitTempletBase nkmunitTempletBase = NKMUnitTempletBase.Find(unitID);
			if (nkmunitTempletBase.m_BaseUnitID != 0 && nkmunitTempletBase.m_BaseUnitID != unitID)
			{
				NKCDescTemplet descTemplet = NKCDescMgr.GetDescTemplet(nkmunitTempletBase.m_BaseUnitID, skinID);
				if (descTemplet != null)
				{
					return descTemplet;
				}
			}
			Log.Error("m_dicNKCDescTemplet Cannot find Key: " + unitID.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCDescMgr.cs", 117);
			return null;
		}

		// Token: 0x060033DC RID: 13276 RVA: 0x00104678 File Offset: 0x00102878
		public static void Init()
		{
			foreach (KeyValuePair<int, Dictionary<int, NKCDescTemplet>> keyValuePair in NKCDescMgr.m_dicNKCDescTemplet)
			{
				keyValuePair.Value.Clear();
			}
			NKCDescMgr.m_dicNKCDescTemplet.Clear();
			NKCDescMgr.LoadFromLUA_Desc("LUA_DESC_TEMPLET");
		}

		// Token: 0x060033DD RID: 13277 RVA: 0x001046E4 File Offset: 0x001028E4
		public static bool LoadFromLUA_Desc(string fileName)
		{
			NKMLua nkmlua = new NKMLua();
			if (nkmlua.LoadCommonPath("AB_SCRIPT", fileName, true) && nkmlua.OpenTable("m_dicNKCDescTempletByID"))
			{
				int num = 1;
				while (nkmlua.OpenTable(num))
				{
					NKCDescTemplet nkcdescTemplet = new NKCDescTemplet();
					if (nkcdescTemplet.LoadFromLUA(nkmlua))
					{
						if (!NKCDescMgr.m_dicNKCDescTemplet.ContainsKey(nkcdescTemplet.m_UnitID))
						{
							NKCDescMgr.m_dicNKCDescTemplet.Add(nkcdescTemplet.m_UnitID, new Dictionary<int, NKCDescTemplet>());
						}
						if (!NKCDescMgr.m_dicNKCDescTemplet[nkcdescTemplet.m_UnitID].ContainsKey(nkcdescTemplet.m_SkinID))
						{
							NKCDescMgr.m_dicNKCDescTemplet[nkcdescTemplet.m_UnitID].Add(nkcdescTemplet.m_SkinID, nkcdescTemplet);
						}
						else
						{
							Log.Error(string.Format("m_dicNKCDescTemplet Duplicate unitID:{0}, skinID:{1}", nkcdescTemplet.m_UnitID, nkcdescTemplet.m_SkinID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCDescMgr.cs", 156);
						}
					}
					num++;
					nkmlua.CloseTable();
				}
				nkmlua.CloseTable();
			}
			nkmlua.LuaClose();
			return true;
		}

		// Token: 0x0400324F RID: 12879
		private static Dictionary<int, Dictionary<int, NKCDescTemplet>> m_dicNKCDescTemplet = new Dictionary<int, Dictionary<int, NKCDescTemplet>>();
	}
}
