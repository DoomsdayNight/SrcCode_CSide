using System;
using Cs.Logging;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x02000499 RID: 1177
	public class NKMLimitBreakTemplet : INKMTemplet
	{
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060020F1 RID: 8433 RVA: 0x000A7D5D File Offset: 0x000A5F5D
		public int Key
		{
			get
			{
				return this.m_iLBRank;
			}
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x000A7D68 File Offset: 0x000A5F68
		public static NKMLimitBreakTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitLimitBreakManager.cs", 27))
			{
				return null;
			}
			NKMLimitBreakTemplet nkmlimitBreakTemplet = new NKMLimitBreakTemplet();
			if (!(true & cNKMLua.GetData("m_iLBRank", ref nkmlimitBreakTemplet.m_iLBRank) & cNKMLua.GetData("m_iRequiredLevel", ref nkmlimitBreakTemplet.m_iRequiredLevel) & cNKMLua.GetData("m_iMaxLevel", ref nkmlimitBreakTemplet.m_iMaxLevel) & cNKMLua.GetData("m_iUnitRequirement", ref nkmlimitBreakTemplet.m_iUnitRequirement) & cNKMLua.GetData("m_bTranscendence", ref nkmlimitBreakTemplet.m_bTranscendence)))
			{
				Log.Error(string.Format("NKMLimitBreakTemplet Load - {0}", nkmlimitBreakTemplet.m_iLBRank), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitLimitBreakManager.cs", 40);
				return null;
			}
			return nkmlimitBreakTemplet;
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x000A7E0D File Offset: 0x000A600D
		public void Join()
		{
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x000A7E0F File Offset: 0x000A600F
		public void Validate()
		{
		}

		// Token: 0x04002176 RID: 8566
		public int m_iLBRank;

		// Token: 0x04002177 RID: 8567
		public int m_iRequiredLevel;

		// Token: 0x04002178 RID: 8568
		public int m_iMaxLevel;

		// Token: 0x04002179 RID: 8569
		public int m_iUnitRequirement;

		// Token: 0x0400217A RID: 8570
		public bool m_bTranscendence;
	}
}
