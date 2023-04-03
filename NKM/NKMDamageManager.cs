using System;
using System.Collections.Generic;
using System.Linq;
using Cs.Logging;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x020003D4 RID: 980
	public class NKMDamageManager
	{
		// Token: 0x060019D6 RID: 6614 RVA: 0x0006EF28 File Offset: 0x0006D128
		public static void ValidateTempletServerOnly()
		{
			if (NKMDamageManager.m_dicDamageTempletID != null)
			{
				foreach (NKMDamageTemplet nkmdamageTemplet in NKMDamageManager.m_dicDamageTempletID.Values)
				{
					nkmdamageTemplet.Validate();
				}
			}
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x0006EF84 File Offset: 0x0006D184
		public static void LoadFromLUA(IEnumerable<string> fileNames, IEnumerable<string> listFileName)
		{
			NKMDamageManager.m_dicDamageTempletID.Clear();
			NKMDamageManager.m_dicDamageTempletStrID.Clear();
			foreach (string basefileName in fileNames)
			{
				NKMDamageManager.LoadFromLuaBase(basefileName);
			}
			foreach (string fileName in listFileName)
			{
				NKMDamageManager.LoadFromLua(fileName);
			}
			foreach (KeyValuePair<string, NKMDamageTemplet> keyValuePair in from e in NKMDamageManager.m_dicDamageTempletStrID
			where !e.Value.luaDataloaded
			select e)
			{
				NKMTempletError.Add("[DamageTemplet:" + keyValuePair.Key + "] 데미지템플릿베이스는 있으나 데미지템플릿이 없습니다.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDamageManager.cs", 833);
			}
			foreach (KeyValuePair<string, NKMDamageTemplet> keyValuePair2 in NKMDamageManager.m_dicDamageTempletStrID)
			{
				keyValuePair2.Value.JoinExtraHitDT();
			}
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x0006F0D8 File Offset: 0x0006D2D8
		private static void LoadFromLuaBase(string basefileName)
		{
			NKMLua nkmlua = new NKMLua();
			if (!nkmlua.LoadCommonPath("AB_SCRIPT", basefileName, true) || !nkmlua.OpenTable("m_dicDamageTempletStrID"))
			{
				Log.ErrorAndExit("lua file loading failed. filaName:" + basefileName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDamageManager.cs", 848);
				return;
			}
			int num = 1;
			int count = NKMDamageManager.m_dicDamageTempletID.Count;
			while (nkmlua.OpenTable(num++))
			{
				NKMDamageTempletBase nkmdamageTempletBase = new NKMDamageTempletBase();
				nkmdamageTempletBase.LoadFromLUA(nkmlua);
				nkmdamageTempletBase.m_DamageTempletIndex = num + count;
				NKMDamageTemplet nkmdamageTemplet;
				if (!NKMDamageManager.m_dicDamageTempletStrID.TryGetValue(nkmdamageTempletBase.m_DamageTempletName, out nkmdamageTemplet))
				{
					nkmdamageTemplet = new NKMDamageTemplet
					{
						m_DamageTempletBase = nkmdamageTempletBase
					};
					NKMDamageManager.m_dicDamageTempletStrID.Add(nkmdamageTempletBase.m_DamageTempletName, nkmdamageTemplet);
				}
				else
				{
					nkmdamageTemplet.m_DamageTempletBase = nkmdamageTempletBase;
				}
				if (!NKMDamageManager.m_dicDamageTempletID.ContainsKey(nkmdamageTemplet.m_DamageTempletBase.m_DamageTempletIndex))
				{
					NKMDamageManager.m_dicDamageTempletID.Add(nkmdamageTemplet.m_DamageTempletBase.m_DamageTempletIndex, nkmdamageTemplet);
				}
				nkmlua.CloseTable();
			}
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x0006F1D0 File Offset: 0x0006D3D0
		private static void LoadFromLua(string fileName)
		{
			NKMLua nkmlua = new NKMLua();
			if (!nkmlua.LoadCommonPath("AB_SCRIPT", fileName, true) || !nkmlua.OpenTable("m_dicDamageTempletStrID"))
			{
				Log.ErrorAndExit("lua file loading failed. filaName:" + fileName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDamageManager.cs", 885);
				return;
			}
			int num = 1;
			while (nkmlua.OpenTable(num++))
			{
				string @string = nkmlua.GetString("m_DamageTempletName");
				NKMDamageTemplet nkmdamageTemplet;
				if (!NKMDamageManager.m_dicDamageTempletStrID.TryGetValue(@string, out nkmdamageTemplet))
				{
					NKMTempletError.Add("[DamageTemplet:" + @string + "] 데미지템플릿은 있으나 데미지템플릿베이스가 없습니다.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDamageManager.cs", 895);
				}
				else
				{
					nkmdamageTemplet.LoadFromLUA(nkmlua);
				}
				nkmlua.CloseTable();
			}
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x0006F276 File Offset: 0x0006D476
		public static NKMDamageTemplet GetTempletByID(int damageID)
		{
			if (NKMDamageManager.m_dicDamageTempletID.ContainsKey(damageID))
			{
				return NKMDamageManager.m_dicDamageTempletID[damageID];
			}
			Log.Error("GetTempletByID null: " + damageID.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDamageManager.cs", 914);
			return null;
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x0006F2B2 File Offset: 0x0006D4B2
		public static NKMDamageTemplet GetTempletByStrID(string damageStrID)
		{
			if (NKMDamageManager.m_dicDamageTempletStrID.ContainsKey(damageStrID))
			{
				return NKMDamageManager.m_dicDamageTempletStrID[damageStrID];
			}
			Log.Error("GetTempletByStrID null: " + damageStrID, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDamageManager.cs", 927);
			return null;
		}

		// Token: 0x040012E8 RID: 4840
		public static Dictionary<int, NKMDamageTemplet> m_dicDamageTempletID = new Dictionary<int, NKMDamageTemplet>();

		// Token: 0x040012E9 RID: 4841
		public static Dictionary<string, NKMDamageTemplet> m_dicDamageTempletStrID = new Dictionary<string, NKMDamageTemplet>();
	}
}
