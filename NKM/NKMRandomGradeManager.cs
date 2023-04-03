using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM.Templet;

namespace NKM
{
	// Token: 0x02000461 RID: 1121
	public class NKMRandomGradeManager
	{
		// Token: 0x06001E57 RID: 7767 RVA: 0x0009013E File Offset: 0x0008E33E
		public static Dictionary<string, NKMRandomGradeTemplet> Get_dicRandomGradeByStrID()
		{
			return NKMRandomGradeManager.m_dicRandomGradeByStrID;
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x00090150 File Offset: 0x0008E350
		public static bool LoadFromLUA(string fileName)
		{
			NKMLua nkmlua = new NKMLua();
			if (nkmlua.LoadCommonPath("AB_SCRIPT", fileName, true) && nkmlua.OpenTable("RANDOM_GRADE"))
			{
				int num = 1;
				while (nkmlua.OpenTable(num))
				{
					NKMRandomGradeTemplet nkmrandomGradeTemplet = new NKMRandomGradeTemplet();
					nkmrandomGradeTemplet.LoadFromLUA(nkmlua);
					if (NKMRandomGradeManager.m_dicRandomGradeByID.ContainsKey(nkmrandomGradeTemplet.m_RandomGradeID))
					{
						NKMRandomGradeManager.m_dicRandomGradeByID[nkmrandomGradeTemplet.m_RandomGradeID].MergeData(nkmrandomGradeTemplet.m_iMaxSalaryLevel, nkmrandomGradeTemplet.GetLastData());
					}
					else
					{
						NKMRandomGradeManager.m_dicRandomGradeByID.Add(nkmrandomGradeTemplet.m_RandomGradeID, nkmrandomGradeTemplet);
					}
					if (NKMRandomGradeManager.m_dicRandomGradeByStrID.ContainsKey(nkmrandomGradeTemplet.m_RandomGradeStrID))
					{
						NKMRandomGradeManager.m_dicRandomGradeByStrID[nkmrandomGradeTemplet.m_RandomGradeStrID].MergeData(nkmrandomGradeTemplet.m_iMaxSalaryLevel, nkmrandomGradeTemplet.GetLastData());
					}
					else
					{
						NKMRandomGradeManager.m_dicRandomGradeByStrID.Add(nkmrandomGradeTemplet.m_RandomGradeStrID, nkmrandomGradeTemplet);
					}
					num++;
					nkmlua.CloseTable();
				}
				nkmlua.CloseTable();
			}
			nkmlua.LuaClose();
			return true;
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x0009024C File Offset: 0x0008E44C
		public static NKMRandomGradeTemplet GetRandomGradeTemplet(int randomGradeID)
		{
			if (!NKMRandomGradeManager.m_dicRandomGradeByID.ContainsKey(randomGradeID))
			{
				Log.Error(string.Format("GetRandomGrade m_dicRandomGradeByID InvalidID! randomGradeID: [{0}]", randomGradeID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMRandomGradeManager.cs", 159);
				return null;
			}
			NKMRandomGradeTemplet nkmrandomGradeTemplet = NKMRandomGradeManager.m_dicRandomGradeByID[randomGradeID];
			if (nkmrandomGradeTemplet == null)
			{
				Log.Error(string.Format("GetRandomGrade m_dicRandomGradeByID InvalidID! randomGradeID: [{0}]", randomGradeID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMRandomGradeManager.cs", 166);
				return null;
			}
			return nkmrandomGradeTemplet;
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x000902B8 File Offset: 0x0008E4B8
		public static NKMRandomGradeTemplet GetRandomGradeTemplet(string randomGradeStrID)
		{
			if (!NKMRandomGradeManager.m_dicRandomGradeByStrID.ContainsKey(randomGradeStrID))
			{
				Log.Error("GetRandomGrade m_dicRandomGradeByStrID InvalidID! randomGradeStrID: [" + randomGradeStrID + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMRandomGradeManager.cs", 177);
				return null;
			}
			NKMRandomGradeTemplet nkmrandomGradeTemplet = NKMRandomGradeManager.m_dicRandomGradeByStrID[randomGradeStrID];
			if (nkmrandomGradeTemplet == null)
			{
				Log.Error("GetRandomGrade m_dicRandomStarGradeByStrID InvalidID! randomGradeStrID: [" + randomGradeStrID + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMRandomGradeManager.cs", 184);
				return null;
			}
			return nkmrandomGradeTemplet;
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x00090324 File Offset: 0x0008E524
		public static NKM_UNIT_GRADE GetRandomGrade(int randomGradeID)
		{
			NKMRandomGradeTemplet randomGradeTemplet = NKMRandomGradeManager.GetRandomGradeTemplet(randomGradeID);
			if (randomGradeTemplet == null)
			{
				return NKM_UNIT_GRADE.NUG_N;
			}
			return randomGradeTemplet.GetRandomGrade();
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x00090344 File Offset: 0x0008E544
		public static NKM_UNIT_GRADE GetRandomGrade(string randomGradeStrID)
		{
			NKMRandomGradeTemplet randomGradeTemplet = NKMRandomGradeManager.GetRandomGradeTemplet(randomGradeStrID);
			if (randomGradeTemplet == null)
			{
				Log.Error("GetRandomGrade m_dicRandomGradeByStrID InvalidID! randomGradeStrID: [" + randomGradeStrID + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMRandomGradeManager.cs", 207);
				return NKM_UNIT_GRADE.NUG_N;
			}
			return randomGradeTemplet.GetRandomGrade();
		}

		// Token: 0x04001F02 RID: 7938
		public static Dictionary<int, Dictionary<int, RatioData>> m_dicRandomGrade = new Dictionary<int, Dictionary<int, RatioData>>();

		// Token: 0x04001F03 RID: 7939
		public static Dictionary<int, NKMRandomGradeTemplet> m_dicRandomGradeByID = new Dictionary<int, NKMRandomGradeTemplet>();

		// Token: 0x04001F04 RID: 7940
		public static Dictionary<string, NKMRandomGradeTemplet> m_dicRandomGradeByStrID = new Dictionary<string, NKMRandomGradeTemplet>();
	}
}
