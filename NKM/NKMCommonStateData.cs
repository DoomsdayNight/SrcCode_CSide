using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004A6 RID: 1190
	public class NKMCommonStateData : IEventConditionOwner
	{
		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06002129 RID: 8489 RVA: 0x000A91CC File Offset: 0x000A73CC
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x000A9208 File Offset: 0x000A7408
		public bool CanUseState(float fHPRate, int phaseNow)
		{
			return this.m_StateName.Length > 1 && (this.m_fUseHPRateOver <= 0f || this.m_fUseHPRateOver <= fHPRate) && (this.m_fUseHPRateUnder <= 0f || this.m_fUseHPRateUnder >= fHPRate) && (this.m_PhaseOver <= -1 || this.m_PhaseOver <= phaseNow) && (this.m_PhaseLess <= -1 || this.m_PhaseLess > phaseNow);
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x000A9280 File Offset: 0x000A7480
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			if (cNKMLua.OpenTable("m_Condition"))
			{
				this.m_Condition.LoadFromLUA(cNKMLua);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_StateName", ref this.m_StateName);
			cNKMLua.GetData("m_fUseHPRateOver", ref this.m_fUseHPRateOver);
			cNKMLua.GetData("m_fUseHPRateUnder", ref this.m_fUseHPRateUnder);
			cNKMLua.GetData("m_PhaseOver", ref this.m_PhaseOver);
			cNKMLua.GetData("m_PhaseLess", ref this.m_PhaseLess);
			cNKMLua.GetData("m_Ratio", ref this.m_Ratio);
			return true;
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x000A931C File Offset: 0x000A751C
		public void DeepCopyFromSource(NKMCommonStateData source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_StateName = source.m_StateName;
			this.m_fUseHPRateOver = source.m_fUseHPRateOver;
			this.m_fUseHPRateUnder = source.m_fUseHPRateUnder;
			this.m_PhaseOver = source.m_PhaseOver;
			this.m_PhaseLess = source.m_PhaseLess;
			this.m_Ratio = source.m_Ratio;
		}

		// Token: 0x040021EC RID: 8684
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x040021ED RID: 8685
		public string m_StateName = "";

		// Token: 0x040021EE RID: 8686
		public float m_fUseHPRateOver;

		// Token: 0x040021EF RID: 8687
		public float m_fUseHPRateUnder;

		// Token: 0x040021F0 RID: 8688
		public int m_PhaseOver = -1;

		// Token: 0x040021F1 RID: 8689
		public int m_PhaseLess = -1;

		// Token: 0x040021F2 RID: 8690
		public int m_Ratio = 1;
	}
}
