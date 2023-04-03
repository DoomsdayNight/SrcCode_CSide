using System;
using System.Collections.Generic;
using System.Linq;

namespace NKM
{
	// Token: 0x020004A2 RID: 1186
	public class NKMEventCondition
	{
		// Token: 0x06002113 RID: 8467 RVA: 0x000A85C0 File Offset: 0x000A67C0
		public void DeepCopyFromSource(NKMEventCondition source)
		{
			this.m_Phase.DeepCopyFromSource(source.m_Phase);
			this.m_bLeaderUnit = source.m_bLeaderUnit;
			this.m_SkillStrID = source.m_SkillStrID;
			this.m_SkillLevel.DeepCopyFromSource(source.m_SkillLevel);
			this.m_MasterSkillStrID = source.m_MasterSkillStrID;
			this.m_MasterSkillLevel.DeepCopyFromSource(source.m_MasterSkillLevel);
			this.m_NeedBuffStrID = source.m_NeedBuffStrID;
			this.m_NeedBuffID = source.m_NeedBuffID;
			this.m_NeedBuffLevel.DeepCopyFromSource(source.m_NeedBuffLevel);
			this.m_NeedBuffOverlapCount.DeepCopyFromSource(source.m_NeedBuffOverlapCount);
			this.m_IgnoreBuffStrID = source.m_IgnoreBuffStrID;
			this.m_IgnoreBuffID = source.m_IgnoreBuffID;
			this.m_IgnoreBuffLevel.DeepCopyFromSource(source.m_IgnoreBuffLevel);
			this.m_IgnoreBuffOverlapCount.DeepCopyFromSource(source.m_IgnoreBuffOverlapCount);
			this.m_MapPositon.DeepCopyFromSource(source.m_MapPositon);
			this.m_fHPRate.DeepCopyFromSource(source.m_fHPRate);
			this.m_LevelRange.DeepCopyFromSource(source.m_LevelRange);
			this.m_bUsePVE = source.m_bUsePVE;
			this.m_bUsePVP = source.m_bUsePVP;
			this.m_IgnoreStatusEffect = source.m_IgnoreStatusEffect;
			this.m_NeedStatusEffect = source.m_NeedStatusEffect;
			this.m_ReqUnitCond = source.m_ReqUnitCond;
			this.m_bReqUnitEnemy = source.m_bReqUnitEnemy;
			if (source.m_listReqUnit != null)
			{
				this.m_listReqUnit = new List<int>();
				this.m_listReqUnit.AddRange(source.m_listReqUnit);
				return;
			}
			this.m_listReqUnit = null;
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x000A8740 File Offset: 0x000A6940
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			this.m_Phase.LoadFromLua(cNKMLua, "m_Phase");
			cNKMLua.GetData("m_bLeaderUnit", ref this.m_bLeaderUnit);
			cNKMLua.GetData("m_SkillStrID", ref this.m_SkillStrID);
			this.m_SkillLevel.LoadFromLua(cNKMLua, "m_SkillLevel");
			cNKMLua.GetData("m_MasterSkillStrID", ref this.m_MasterSkillStrID);
			this.m_MasterSkillLevel.LoadFromLua(cNKMLua, "m_MasterSkillLevel");
			cNKMLua.GetData("m_NeedBuffStrID", ref this.m_NeedBuffStrID);
			this.m_NeedBuffLevel.LoadFromLua(cNKMLua, "m_NeedBuffLevel");
			this.m_NeedBuffOverlapCount.LoadFromLua(cNKMLua, "m_NeedBuffOverlapCount");
			cNKMLua.GetData("m_IgnoreBuffStrID", ref this.m_IgnoreBuffStrID);
			this.m_IgnoreBuffLevel.LoadFromLua(cNKMLua, "m_IgnoreBuffLevel");
			this.m_IgnoreBuffOverlapCount.LoadFromLua(cNKMLua, "m_IgnoreBuffOverlapCount");
			this.m_MapPositon.LoadFromLua(cNKMLua, "m_MapPositon");
			this.m_fHPRate.LoadFromLua(cNKMLua, "m_fHPRate");
			this.m_LevelRange.LoadFromLua(cNKMLua, "m_LevelRange");
			cNKMLua.GetData("m_bUsePVE", ref this.m_bUsePVE);
			cNKMLua.GetData("m_bUsePVP", ref this.m_bUsePVP);
			cNKMLua.GetData<NKM_UNIT_STATUS_EFFECT>("m_IgnoreStatusEffect", ref this.m_IgnoreStatusEffect);
			cNKMLua.GetData<NKM_UNIT_STATUS_EFFECT>("m_NeedStatusEffect", ref this.m_NeedStatusEffect);
			cNKMLua.GetData<NKMEventCondition.UnitCountCond>("m_ReqUnitCond", ref this.m_ReqUnitCond);
			cNKMLua.GetData("m_bReqUnitEnemy", ref this.m_bReqUnitEnemy);
			if (!cNKMLua.GetDataList("m_listReqUnit", out this.m_listReqUnit))
			{
				this.m_listReqUnit = null;
			}
			return true;
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x000A88E4 File Offset: 0x000A6AE4
		public void CheckSkillID()
		{
			if (this.m_SkillID == -1 && this.m_SkillStrID.Length > 1)
			{
				this.m_SkillID = NKMUnitSkillManager.GetSkillID(this.m_SkillStrID);
				if (this.m_SkillID == -1)
				{
					this.m_SkillID = NKMShipSkillManager.GetSkillID(this.m_SkillStrID);
					this.m_bShipSkill = true;
				}
				else
				{
					this.m_bShipSkill = false;
				}
			}
			if (this.m_MasterSkillID == -1 && this.m_MasterSkillStrID.Length > 1)
			{
				this.m_MasterSkillID = NKMUnitSkillManager.GetSkillID(this.m_MasterSkillStrID);
				if (this.m_MasterSkillID == -1)
				{
					this.m_MasterSkillID = NKMShipSkillManager.GetSkillID(this.m_MasterSkillStrID);
					this.m_bMasterShipSkill = true;
					return;
				}
				this.m_bMasterShipSkill = false;
			}
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x000A8994 File Offset: 0x000A6B94
		public bool CanUseSkill(int skillLevel)
		{
			if (this.m_SkillID != -1 && skillLevel != -1)
			{
				if (this.m_bShipSkill && skillLevel == 0)
				{
					return false;
				}
				if (this.m_SkillLevel.m_Min != -1 && this.m_SkillLevel.m_Min > skillLevel)
				{
					return false;
				}
				if (this.m_SkillLevel.m_Max != -1 && this.m_SkillLevel.m_Max < skillLevel)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x000A89F8 File Offset: 0x000A6BF8
		public bool CanUseMasterSkill(int masterSkillLevel)
		{
			if (this.m_MasterSkillID != -1 && masterSkillLevel != -1)
			{
				if (this.m_bMasterShipSkill && masterSkillLevel == 0)
				{
					return false;
				}
				if (this.m_MasterSkillLevel.m_Min != -1 && this.m_MasterSkillLevel.m_Min > masterSkillLevel)
				{
					return false;
				}
				if (this.m_MasterSkillLevel.m_Max != -1 && this.m_MasterSkillLevel.m_Max < masterSkillLevel)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x000A8A5C File Offset: 0x000A6C5C
		public bool CanUsePhase(int phaseNow)
		{
			return (this.m_Phase.m_Min == -1 || this.m_Phase.m_Min <= phaseNow) && (this.m_Phase.m_Max == -1 || this.m_Phase.m_Max >= phaseNow);
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x000A8A9C File Offset: 0x000A6C9C
		public bool CheckHPRate(NKMUnit masterUnit)
		{
			return this.m_fHPRate.m_Min < 0f || this.m_fHPRate.m_Max < 0f || (masterUnit != null && masterUnit.GetHPRate() >= this.m_fHPRate.m_Min && masterUnit.GetHPRate() <= this.m_fHPRate.m_Max);
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x000A8B00 File Offset: 0x000A6D00
		public bool CanUseBuff(Dictionary<short, NKMBuffSyncData> dicBuffData)
		{
			if (this.m_IgnoreBuffStrID.Length > 1 && this.m_IgnoreBuffID <= 0)
			{
				NKMBuffTemplet buffTempletByStrID = NKMBuffManager.GetBuffTempletByStrID(this.m_IgnoreBuffStrID);
				if (buffTempletByStrID != null)
				{
					this.m_IgnoreBuffID = buffTempletByStrID.m_BuffID;
				}
			}
			if (this.m_NeedBuffStrID.Length > 1 && this.m_NeedBuffID <= 0)
			{
				NKMBuffTemplet buffTempletByStrID2 = NKMBuffManager.GetBuffTempletByStrID(this.m_NeedBuffStrID);
				if (buffTempletByStrID2 != null)
				{
					this.m_NeedBuffID = buffTempletByStrID2.m_BuffID;
				}
			}
			if (this.m_IgnoreBuffID > 0)
			{
				foreach (KeyValuePair<short, NKMBuffSyncData> keyValuePair in dicBuffData)
				{
					NKMBuffSyncData value = keyValuePair.Value;
					if (Math.Abs(value.m_BuffID) == this.m_IgnoreBuffID)
					{
						if (this.m_IgnoreBuffLevel.m_Min != -1 && this.m_IgnoreBuffLevel.m_Min > (int)value.m_BuffStatLevel)
						{
							return true;
						}
						if (this.m_IgnoreBuffLevel.m_Max != -1 && this.m_IgnoreBuffLevel.m_Max < (int)value.m_BuffStatLevel)
						{
							return true;
						}
						if (this.m_IgnoreBuffOverlapCount.m_Min != -1 && this.m_IgnoreBuffOverlapCount.m_Min > (int)value.m_OverlapCount)
						{
							return true;
						}
						if (this.m_IgnoreBuffOverlapCount.m_Max != -1 && this.m_IgnoreBuffOverlapCount.m_Max < (int)value.m_OverlapCount)
						{
							return true;
						}
						return false;
					}
				}
			}
			if (this.m_NeedBuffID > 0)
			{
				foreach (KeyValuePair<short, NKMBuffSyncData> keyValuePair2 in dicBuffData)
				{
					NKMBuffSyncData value2 = keyValuePair2.Value;
					if (Math.Abs(value2.m_BuffID) == this.m_NeedBuffID)
					{
						if (this.m_NeedBuffLevel.m_Min != -1 && this.m_NeedBuffLevel.m_Min > (int)value2.m_BuffStatLevel)
						{
							return false;
						}
						if (this.m_NeedBuffLevel.m_Max != -1 && this.m_NeedBuffLevel.m_Max < (int)value2.m_BuffStatLevel)
						{
							return false;
						}
						if (this.m_NeedBuffOverlapCount.m_Min != -1 && this.m_NeedBuffOverlapCount.m_Min > (int)value2.m_OverlapCount)
						{
							return false;
						}
						if (this.m_NeedBuffOverlapCount.m_Max != -1 && this.m_NeedBuffOverlapCount.m_Max < (int)value2.m_OverlapCount)
						{
							return false;
						}
						return true;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x000A8DB4 File Offset: 0x000A6FB4
		public bool CanUseStatus(NKMUnit unit)
		{
			return (this.m_NeedStatusEffect == NKM_UNIT_STATUS_EFFECT.NUSE_NONE || unit.HasStatus(this.m_NeedStatusEffect)) && (this.m_IgnoreStatusEffect == NKM_UNIT_STATUS_EFFECT.NUSE_NONE || !unit.HasStatus(this.m_IgnoreStatusEffect));
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x000A8DE8 File Offset: 0x000A6FE8
		public bool CanUseMapPosition(float currentPosition)
		{
			return (this.m_MapPositon.m_Min == -1f || currentPosition >= this.m_MapPositon.m_Min) && (this.m_MapPositon.m_Max == -1f || currentPosition <= this.m_MapPositon.m_Max);
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x000A8E3C File Offset: 0x000A703C
		public bool CanUseLevelRange(NKMUnit unit)
		{
			if (this.m_LevelRange.m_Min < 0 && this.m_LevelRange.m_Max < 0)
			{
				return true;
			}
			if (unit == null)
			{
				return false;
			}
			if (unit.GetUnitData() == null)
			{
				return false;
			}
			int unitLevel = unit.GetUnitData().m_UnitLevel;
			return (this.m_LevelRange.m_Min < 0 || unitLevel >= this.m_LevelRange.m_Min) && (this.m_LevelRange.m_Max < 0 || unitLevel <= this.m_LevelRange.m_Max);
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x000A8EBF File Offset: 0x000A70BF
		public bool CanUseUnitExist(NKMGame game, NKM_TEAM_TYPE myTeam)
		{
			return this.CanUseUnitExist(game, this.m_listReqUnit, this.m_bReqUnitEnemy, myTeam, this.m_ReqUnitCond);
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x000A8EDC File Offset: 0x000A70DC
		private bool CanUseUnitExist(NKMGame game, List<int> lstUnitID, bool bEnemy, NKM_TEAM_TYPE myTeam, NKMEventCondition.UnitCountCond cond)
		{
			NKMEventCondition.<>c__DisplayClass42_0 CS$<>8__locals1 = new NKMEventCondition.<>c__DisplayClass42_0();
			CS$<>8__locals1.bEnemy = bEnemy;
			CS$<>8__locals1.game = game;
			CS$<>8__locals1.myTeam = myTeam;
			if (lstUnitID == null)
			{
				return true;
			}
			switch (cond)
			{
			default:
				return true;
			case NKMEventCondition.UnitCountCond.ALL:
				return lstUnitID.All(new Func<int, bool>(CS$<>8__locals1.<CanUseUnitExist>g__UnitExist|0));
			case NKMEventCondition.UnitCountCond.ANY:
				return lstUnitID.Exists(new Predicate<int>(CS$<>8__locals1.<CanUseUnitExist>g__UnitExist|0));
			case NKMEventCondition.UnitCountCond.NOT_ANY:
				return lstUnitID.Exists(new Predicate<int>(CS$<>8__locals1.<CanUseUnitExist>g__UnitNotExist|1));
			case NKMEventCondition.UnitCountCond.NOT_ALL:
				return lstUnitID.All(new Func<int, bool>(CS$<>8__locals1.<CanUseUnitExist>g__UnitNotExist|1));
			}
		}

		// Token: 0x040021AC RID: 8620
		public NKMMinMaxInt m_Phase = new NKMMinMaxInt(-1, -1);

		// Token: 0x040021AD RID: 8621
		public bool m_bShipSkill;

		// Token: 0x040021AE RID: 8622
		public string m_SkillStrID = "";

		// Token: 0x040021AF RID: 8623
		public int m_SkillID = -1;

		// Token: 0x040021B0 RID: 8624
		public NKMMinMaxInt m_SkillLevel = new NKMMinMaxInt(-1, -1);

		// Token: 0x040021B1 RID: 8625
		public bool m_bMasterShipSkill;

		// Token: 0x040021B2 RID: 8626
		public string m_MasterSkillStrID = "";

		// Token: 0x040021B3 RID: 8627
		public int m_MasterSkillID = -1;

		// Token: 0x040021B4 RID: 8628
		public NKMMinMaxInt m_MasterSkillLevel = new NKMMinMaxInt(-1, -1);

		// Token: 0x040021B5 RID: 8629
		public bool m_bLeaderUnit;

		// Token: 0x040021B6 RID: 8630
		public string m_NeedBuffStrID = "";

		// Token: 0x040021B7 RID: 8631
		public short m_NeedBuffID;

		// Token: 0x040021B8 RID: 8632
		public NKMMinMaxInt m_NeedBuffLevel = new NKMMinMaxInt(-1, -1);

		// Token: 0x040021B9 RID: 8633
		public NKMMinMaxInt m_NeedBuffOverlapCount = new NKMMinMaxInt(-1, -1);

		// Token: 0x040021BA RID: 8634
		public string m_IgnoreBuffStrID = "";

		// Token: 0x040021BB RID: 8635
		public short m_IgnoreBuffID;

		// Token: 0x040021BC RID: 8636
		public NKMMinMaxInt m_IgnoreBuffLevel = new NKMMinMaxInt(-1, -1);

		// Token: 0x040021BD RID: 8637
		public NKMMinMaxInt m_IgnoreBuffOverlapCount = new NKMMinMaxInt(-1, -1);

		// Token: 0x040021BE RID: 8638
		public NKMMinMaxFloat m_MapPositon = new NKMMinMaxFloat(-1f, -1f);

		// Token: 0x040021BF RID: 8639
		public NKMMinMaxFloat m_fHPRate = new NKMMinMaxFloat(-1f, -1f);

		// Token: 0x040021C0 RID: 8640
		public NKMMinMaxInt m_LevelRange = new NKMMinMaxInt(-1, -1);

		// Token: 0x040021C1 RID: 8641
		public bool m_bUsePVE = true;

		// Token: 0x040021C2 RID: 8642
		public bool m_bUsePVP = true;

		// Token: 0x040021C3 RID: 8643
		public NKM_UNIT_STATUS_EFFECT m_IgnoreStatusEffect;

		// Token: 0x040021C4 RID: 8644
		public NKM_UNIT_STATUS_EFFECT m_NeedStatusEffect;

		// Token: 0x040021C5 RID: 8645
		public List<int> m_listReqUnit;

		// Token: 0x040021C6 RID: 8646
		public NKMEventCondition.UnitCountCond m_ReqUnitCond;

		// Token: 0x040021C7 RID: 8647
		public bool m_bReqUnitEnemy;

		// Token: 0x02001215 RID: 4629
		public enum UnitCountCond
		{
			// Token: 0x04009471 RID: 38001
			INVALID,
			// Token: 0x04009472 RID: 38002
			ALL,
			// Token: 0x04009473 RID: 38003
			ANY,
			// Token: 0x04009474 RID: 38004
			NOT_ANY,
			// Token: 0x04009475 RID: 38005
			NOT_ALL
		}
	}
}
