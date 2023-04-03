using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM.Templet;

namespace NKM
{
	// Token: 0x02000460 RID: 1120
	public class NKMRandomGradeTemplet
	{
		// Token: 0x06001E53 RID: 7763 RVA: 0x0008FF74 File Offset: 0x0008E174
		public void LoadFromLUA(NKMLua cNKMLua)
		{
			cNKMLua.GetData("m_RandomGradeID", ref this.m_RandomGradeID);
			cNKMLua.GetData("m_RandomGradeStrID", ref this.m_RandomGradeStrID);
			int num = 0;
			cNKMLua.GetData("Ratio_SSR", ref num);
			for (int i = 0; i < num; i++)
			{
				this.m_listGrade.Add(NKM_UNIT_GRADE.NUG_SSR);
			}
			int num2 = 0;
			cNKMLua.GetData("Ratio_SR", ref num2);
			for (int j = 0; j < num2; j++)
			{
				this.m_listGrade.Add(NKM_UNIT_GRADE.NUG_SR);
			}
			int num3 = 0;
			cNKMLua.GetData("Ratio_R", ref num3);
			for (int k = 0; k < num3; k++)
			{
				this.m_listGrade.Add(NKM_UNIT_GRADE.NUG_R);
			}
			while (this.m_listGrade.Count < 1000)
			{
				this.m_listGrade.Add(NKM_UNIT_GRADE.NUG_N);
			}
			int num4 = 10000 - (num + num2 + num3);
			cNKMLua.GetData("m_SalaryLevel", ref this.m_iMaxSalaryLevel);
			this.m_dicRatioList.Add(this.m_iMaxSalaryLevel, new RatioData((float)num * 0.01f, (float)num2 * 0.01f, (float)num3 * 0.01f, (float)num4 * 0.01f));
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x0009009F File Offset: 0x0008E29F
		public RatioData GetLastData()
		{
			return this.m_dicRatioList[this.m_iMaxSalaryLevel];
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x000900B2 File Offset: 0x0008E2B2
		public void MergeData(int salaryLv, RatioData data)
		{
			if (!this.m_dicRatioList.ContainsKey(salaryLv))
			{
				this.m_dicRatioList.Add(salaryLv, data);
				this.m_iMaxSalaryLevel = salaryLv;
			}
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x000900D8 File Offset: 0x0008E2D8
		public NKM_UNIT_GRADE GetRandomGrade()
		{
			if (this.m_listGrade.Count != 100)
			{
				Log.Error(string.Format("NKMRandomGrade m_listGrade count is not 100 m_RandomGradeStrID: {0}, count: {1}", this.m_RandomGradeStrID, this.m_listGrade.Count), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMRandomGradeManager.cs", 98);
				return NKM_UNIT_GRADE.NUG_N;
			}
			return this.m_listGrade[NKMRandom.Range(0, this.m_listGrade.Count)];
		}

		// Token: 0x04001EFD RID: 7933
		public int m_RandomGradeID;

		// Token: 0x04001EFE RID: 7934
		public string m_RandomGradeStrID = "";

		// Token: 0x04001EFF RID: 7935
		public List<NKM_UNIT_GRADE> m_listGrade = new List<NKM_UNIT_GRADE>();

		// Token: 0x04001F00 RID: 7936
		public int m_iMaxSalaryLevel;

		// Token: 0x04001F01 RID: 7937
		public Dictionary<int, RatioData> m_dicRatioList = new Dictionary<int, RatioData>();
	}
}
