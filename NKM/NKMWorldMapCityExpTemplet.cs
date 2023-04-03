using System;
using Cs.Logging;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x02000514 RID: 1300
	public class NKMWorldMapCityExpTemplet : INKMTemplet
	{
		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06002523 RID: 9507 RVA: 0x000BFB84 File Offset: 0x000BDD84
		public int Key
		{
			get
			{
				return this.m_Level;
			}
		}

		// Token: 0x06002524 RID: 9508 RVA: 0x000BFB8C File Offset: 0x000BDD8C
		public static NKMWorldMapCityExpTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			NKMWorldMapCityExpTemplet nkmworldMapCityExpTemplet = new NKMWorldMapCityExpTemplet();
			if (!(true & cNKMLua.GetData("m_iLevel", ref nkmworldMapCityExpTemplet.m_Level) & cNKMLua.GetData("m_iExpRequired", ref nkmworldMapCityExpTemplet.m_ExpRequired) & cNKMLua.GetData("m_iExpCumulated", ref nkmworldMapCityExpTemplet.m_ExpCumulated) & cNKMLua.GetData("m_LevelUpReqCredit", ref nkmworldMapCityExpTemplet.m_LevelUpReqCredit)))
			{
				Log.Error(string.Format("NKMWorldMapCityExpTemplet Load Fail - {0}", nkmworldMapCityExpTemplet.m_Level), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMWorldMapManager.cs", 155);
				return null;
			}
			return nkmworldMapCityExpTemplet;
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x000BFC11 File Offset: 0x000BDE11
		public void Join()
		{
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x000BFC13 File Offset: 0x000BDE13
		public void Validate()
		{
		}

		// Token: 0x04002684 RID: 9860
		public int m_Level;

		// Token: 0x04002685 RID: 9861
		public int m_ExpRequired;

		// Token: 0x04002686 RID: 9862
		public int m_ExpCumulated;

		// Token: 0x04002687 RID: 9863
		public int m_LevelUpReqCredit;
	}
}
