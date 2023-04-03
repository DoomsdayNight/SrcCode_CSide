using System;

namespace NKM
{
	// Token: 0x020003CD RID: 973
	public class NKMDamageTempletBase
	{
		// Token: 0x060019B9 RID: 6585 RVA: 0x0006DB18 File Offset: 0x0006BD18
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			cNKMLua.GetData("m_DamageTempletName", ref this.m_DamageTempletName);
			cNKMLua.GetData("m_fAtkFactor", ref this.m_fAtkFactor);
			cNKMLua.GetData("m_fAtkMaxHPRateFactor", ref this.m_fAtkMaxHPRateFactor);
			cNKMLua.GetData("m_fAtkHPRateFactor", ref this.m_fAtkHPRateFactor);
			cNKMLua.GetData("m_fAtkFactorPVP", ref this.m_fAtkFactorPVP);
			cNKMLua.GetData("m_fAtkMaxHPRateFactorPVP", ref this.m_fAtkMaxHPRateFactorPVP);
			cNKMLua.GetData("m_fAtkHPRateFactorPVP", ref this.m_fAtkHPRateFactorPVP);
			return true;
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x0006DBA4 File Offset: 0x0006BDA4
		public void DeepCopyFromSource(NKMDamageTempletBase source)
		{
			this.m_DamageTempletName = source.m_DamageTempletName;
			this.m_DamageTempletIndex = source.m_DamageTempletIndex;
			this.m_fAtkFactor = source.m_fAtkFactor;
			this.m_fAtkMaxHPRateFactor = source.m_fAtkMaxHPRateFactor;
			this.m_fAtkHPRateFactor = source.m_fAtkHPRateFactor;
			this.m_fAtkFactorPVP = source.m_fAtkFactorPVP;
			this.m_fAtkMaxHPRateFactorPVP = source.m_fAtkMaxHPRateFactorPVP;
			this.m_fAtkHPRateFactorPVP = source.m_fAtkHPRateFactorPVP;
		}

		// Token: 0x04001270 RID: 4720
		public string m_DamageTempletName = "";

		// Token: 0x04001271 RID: 4721
		public int m_DamageTempletIndex;

		// Token: 0x04001272 RID: 4722
		public float m_fAtkFactor;

		// Token: 0x04001273 RID: 4723
		public float m_fAtkMaxHPRateFactor;

		// Token: 0x04001274 RID: 4724
		public float m_fAtkHPRateFactor;

		// Token: 0x04001275 RID: 4725
		public float m_fAtkFactorPVP;

		// Token: 0x04001276 RID: 4726
		public float m_fAtkMaxHPRateFactorPVP;

		// Token: 0x04001277 RID: 4727
		public float m_fAtkHPRateFactorPVP;
	}
}
