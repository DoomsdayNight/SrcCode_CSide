using System;
using Cs.Math;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004A7 RID: 1191
	public class NKMAttackStateData : IEventConditionOwner
	{
		// Token: 0x1700035F RID: 863
		// (get) Token: 0x0600212E RID: 8494 RVA: 0x000A9382 File Offset: 0x000A7582
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x000A93C8 File Offset: 0x000A75C8
		public bool CanUseAttack(NKM_ATTACK_STATE_DATA_TYPE eNKM_ATTACK_STATE_DATA_TYPE, float fHPRate, int phaseNow, float fRangeFactor)
		{
			return this.m_bNoTarget && this.CanUseAttack(eNKM_ATTACK_STATE_DATA_TYPE, fHPRate, false, 0f, phaseNow, fRangeFactor);
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x000A93E8 File Offset: 0x000A75E8
		public bool CanUseAttack(NKM_ATTACK_STATE_DATA_TYPE eNKM_ATTACK_STATE_DATA_TYPE, float fHPRate, bool bAirUnit, float fDistToTarget, int phaseNow, float fRangeFactor)
		{
			if (this.m_StateName.Length <= 1)
			{
				return false;
			}
			if (this.m_NKM_ATTACK_STATE_DATA_TYPE != eNKM_ATTACK_STATE_DATA_TYPE)
			{
				return false;
			}
			if (!this.m_bNoTarget)
			{
				float num = this.m_fRangeMin * fRangeFactor;
				float num2 = this.m_fRangeMax * fRangeFactor;
				if (this.m_bTargetLandOnly && bAirUnit)
				{
					return false;
				}
				if (this.m_bTargetAirOnly && !bAirUnit)
				{
					return false;
				}
				if (num > 0f && num > fDistToTarget)
				{
					return false;
				}
				if (num2 < fDistToTarget)
				{
					return false;
				}
			}
			return (this.m_fUseHPRateOver <= 0f || this.m_fUseHPRateOver <= fHPRate) && (this.m_fUseHPRateUnder <= 0f || this.m_fUseHPRateUnder >= fHPRate) && (this.m_PhaseOver <= -1 || this.m_PhaseOver <= phaseNow) && (this.m_PhaseLess <= -1 || this.m_PhaseLess > phaseNow);
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x000A94B8 File Offset: 0x000A76B8
		public bool LoadFromLUA(NKMLua cNKMLua, float fTargetNearRange)
		{
			if (cNKMLua.OpenTable("m_Condition"))
			{
				this.m_Condition.LoadFromLUA(cNKMLua);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData<NKM_ATTACK_STATE_DATA_TYPE>("m_NKM_ATTACK_STATE_DATA_TYPE", ref this.m_NKM_ATTACK_STATE_DATA_TYPE);
			cNKMLua.GetData("m_StateName", ref this.m_StateName);
			cNKMLua.GetData("m_fStartCool", ref this.m_fStartCool);
			cNKMLua.GetData("m_fStartCoolPVP", ref this.m_fStartCoolPVP);
			cNKMLua.GetData("m_bNoTarget", ref this.m_bNoTarget);
			cNKMLua.GetData("m_bTargetLandOnly", ref this.m_bTargetLandOnly);
			cNKMLua.GetData("m_bTargetAirOnly", ref this.m_bTargetAirOnly);
			cNKMLua.GetData("m_fRangeMin", ref this.m_fRangeMin);
			cNKMLua.GetData("m_fRangeMax", ref this.m_fRangeMax);
			cNKMLua.GetData("m_fUseHPRateOver", ref this.m_fUseHPRateOver);
			cNKMLua.GetData("m_fUseHPRateUnder", ref this.m_fUseHPRateUnder);
			cNKMLua.GetData("m_PhaseOver", ref this.m_PhaseOver);
			cNKMLua.GetData("m_PhaseLess", ref this.m_PhaseLess);
			cNKMLua.GetData("m_Ratio", ref this.m_Ratio);
			if (this.m_fRangeMax.IsNearlyZero(1E-05f))
			{
				this.m_fRangeMax = fTargetNearRange;
			}
			return true;
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x000A95FC File Offset: 0x000A77FC
		public void DeepCopyFromSource(NKMAttackStateData source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_NKM_ATTACK_STATE_DATA_TYPE = source.m_NKM_ATTACK_STATE_DATA_TYPE;
			this.m_StateName = source.m_StateName;
			this.m_fStartCool = source.m_fStartCool;
			this.m_fStartCoolPVP = source.m_fStartCoolPVP;
			this.m_bNoTarget = source.m_bNoTarget;
			this.m_bTargetLandOnly = source.m_bTargetLandOnly;
			this.m_bTargetAirOnly = source.m_bTargetAirOnly;
			this.m_fRangeMin = source.m_fRangeMin;
			this.m_fRangeMax = source.m_fRangeMax;
			this.m_fUseHPRateOver = source.m_fUseHPRateOver;
			this.m_fUseHPRateUnder = source.m_fUseHPRateUnder;
			this.m_PhaseOver = source.m_PhaseOver;
			this.m_PhaseLess = source.m_PhaseLess;
			this.m_Ratio = source.m_Ratio;
		}

		// Token: 0x040021F3 RID: 8691
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x040021F4 RID: 8692
		public NKM_ATTACK_STATE_DATA_TYPE m_NKM_ATTACK_STATE_DATA_TYPE;

		// Token: 0x040021F5 RID: 8693
		public string m_StateName = "";

		// Token: 0x040021F6 RID: 8694
		public float m_fStartCool;

		// Token: 0x040021F7 RID: 8695
		public float m_fStartCoolPVP = 1f;

		// Token: 0x040021F8 RID: 8696
		public bool m_bNoTarget;

		// Token: 0x040021F9 RID: 8697
		public bool m_bTargetLandOnly;

		// Token: 0x040021FA RID: 8698
		public bool m_bTargetAirOnly;

		// Token: 0x040021FB RID: 8699
		public float m_fRangeMin;

		// Token: 0x040021FC RID: 8700
		public float m_fRangeMax;

		// Token: 0x040021FD RID: 8701
		public float m_fUseHPRateOver;

		// Token: 0x040021FE RID: 8702
		public float m_fUseHPRateUnder;

		// Token: 0x040021FF RID: 8703
		public int m_PhaseOver = -1;

		// Token: 0x04002200 RID: 8704
		public int m_PhaseLess = -1;

		// Token: 0x04002201 RID: 8705
		public int m_Ratio = 1;
	}
}
