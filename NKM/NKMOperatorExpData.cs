using System;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200051C RID: 1308
	public class NKMOperatorExpData : INKMTemplet
	{
		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x0600255E RID: 9566 RVA: 0x000C0BAC File Offset: 0x000BEDAC
		public int Key
		{
			get
			{
				return (int)this.m_NKM_UNIT_GRADE;
			}
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x000C0BB4 File Offset: 0x000BEDB4
		public static NKMOperatorExpData LoadFromLua(NKMLua lua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(lua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Templet/NKMOperatorExpTemplet.cs", 54))
			{
				return null;
			}
			NKMOperatorExpData nkmoperatorExpData = new NKMOperatorExpData();
			if (!(true & lua.GetData("m_iLevel", ref nkmoperatorExpData.m_iLevel) & lua.GetData<NKM_UNIT_GRADE>("m_NKM_UNIT_GRADE", ref nkmoperatorExpData.m_NKM_UNIT_GRADE) & lua.GetData("m_iExpRequiredOpr", ref nkmoperatorExpData.m_iExpRequiredOpr) & lua.GetData("m_iExpCumulatedOpr", ref nkmoperatorExpData.m_iExpCumulatedOpr)))
			{
				return null;
			}
			return nkmoperatorExpData;
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x000C0C26 File Offset: 0x000BEE26
		public void Join()
		{
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x000C0C28 File Offset: 0x000BEE28
		public void Validate()
		{
		}

		// Token: 0x040026B4 RID: 9908
		public int m_iLevel;

		// Token: 0x040026B5 RID: 9909
		public NKM_UNIT_GRADE m_NKM_UNIT_GRADE;

		// Token: 0x040026B6 RID: 9910
		public int m_iExpRequiredOpr;

		// Token: 0x040026B7 RID: 9911
		public int m_iExpCumulatedOpr;
	}
}
