using System;
using System.Collections.Generic;
using NKM.Templet;

namespace NKM
{
	// Token: 0x0200052C RID: 1324
	public static class NKMRewardManager
	{
		// Token: 0x060025BD RID: 9661 RVA: 0x000C25E4 File Offset: 0x000C07E4
		public static NKMRewardGroupTemplet GetRewardGroup(int groupID)
		{
			NKMRewardGroupTemplet result;
			NKMRewardManager.m_RewardData.TryGetValue(groupID, out result);
			return result;
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x000C2600 File Offset: 0x000C0800
		public static bool ContainsKey(int groupId)
		{
			return NKMRewardManager.m_RewardData.ContainsKey(groupId);
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x000C2610 File Offset: 0x000C0810
		public static bool LoadFromLUA(string fileName)
		{
			using (NKMLua nkmlua = new NKMLua())
			{
				if (!nkmlua.LoadCommonPath("AB_SCRIPT", fileName, true) || !nkmlua.OpenTable("REWARD_TEMPLET"))
				{
					return false;
				}
				int num = 1;
				while (nkmlua.OpenTable(num))
				{
					if (NKMContentsVersionManager.CheckContentsVersion(nkmlua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMRewardManager.cs", 35))
					{
						int num2 = 0;
						if (!nkmlua.GetData("m_RewardGroupID", ref num2) || num2 <= 0)
						{
							num++;
							nkmlua.CloseTable();
							continue;
						}
						if (!NKMRewardManager.m_RewardData.ContainsKey(num2))
						{
							NKMRewardGroupTemplet value = new NKMRewardGroupTemplet(num2);
							NKMRewardManager.m_RewardData.Add(num2, value);
						}
						NKMRewardTemplet nkmrewardTemplet = new NKMRewardTemplet();
						nkmrewardTemplet.LoadFromLUA(nkmlua);
						nkmrewardTemplet.m_Ratio = 1;
						nkmrewardTemplet.Validate();
						NKMRewardManager.m_RewardData[num2].Add(nkmrewardTemplet);
					}
					num++;
					nkmlua.CloseTable();
				}
				nkmlua.CloseTable();
			}
			return true;
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x000C2710 File Offset: 0x000C0910
		public static void Clear()
		{
			NKMRewardManager.m_RewardData.Clear();
		}

		// Token: 0x04002746 RID: 10054
		private static Dictionary<int, NKMRewardGroupTemplet> m_RewardData = new Dictionary<int, NKMRewardGroupTemplet>();
	}
}
