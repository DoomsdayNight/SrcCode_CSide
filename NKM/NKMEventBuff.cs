using System;
using System.Collections.Generic;
using Cs.Logging;
using Cs.Math;
using NKM.Templet;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004CB RID: 1227
	public class NKMEventBuff : IEventConditionOwner, INKMUnitStateEvent
	{
		// Token: 0x1700039A RID: 922
		// (get) Token: 0x0600227B RID: 8827 RVA: 0x000B27E0 File Offset: 0x000B09E0
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x0600227C RID: 8828 RVA: 0x000B27E8 File Offset: 0x000B09E8
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTime;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x0600227D RID: 8829 RVA: 0x000B27F0 File Offset: 0x000B09F0
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Allowed;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x0600227E RID: 8830 RVA: 0x000B27F3 File Offset: 0x000B09F3
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x0600227F RID: 8831 RVA: 0x000B27FB File Offset: 0x000B09FB
		public bool bStateEnd
		{
			get
			{
				return this.m_bStateEndTime;
			}
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x000B283D File Offset: 0x000B0A3D
		public bool Validate()
		{
			return string.IsNullOrEmpty(this.m_BuffStrID) || NKMBuffManager.GetBuffTempletByStrID(this.m_BuffStrID) != null;
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x000B285C File Offset: 0x000B0A5C
		public void DeepCopyFromSource(NKMEventBuff source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTime = source.m_fEventTime;
			this.m_bStateEndTime = source.m_bStateEndTime;
			this.m_BuffStrID = source.m_BuffStrID;
			this.m_BuffStatLevel = source.m_BuffStatLevel;
			this.m_BuffRemove = source.m_BuffRemove;
			this.m_StateEndRemove = source.m_StateEndRemove;
			this.m_BuffStatLevelPerSkillLevel = source.m_BuffStatLevelPerSkillLevel;
			this.m_BuffTimeLevel = source.m_BuffTimeLevel;
			this.m_BuffTimeLevelPerSkillLevel = source.m_BuffTimeLevelPerSkillLevel;
			this.m_fRange = source.m_fRange;
			this.m_bMyTeam = source.m_bMyTeam;
			this.m_bEnemy = source.m_bEnemy;
			this.m_bReflection = source.m_bReflection;
			this.m_Overlap = source.m_Overlap;
			this.m_fMinTargetHP = source.m_fMinTargetHP;
			this.m_fMaxTargetHP = source.m_fMaxTargetHP;
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x000B2948 File Offset: 0x000B0B48
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			if (cNKMLua.OpenTable("m_Condition"))
			{
				this.m_Condition.LoadFromLUA(cNKMLua);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_bAnimTime", ref this.m_bAnimTime);
			cNKMLua.GetData("m_fEventTime", ref this.m_fEventTime);
			cNKMLua.GetData("m_bStateEndTime", ref this.m_bStateEndTime);
			cNKMLua.GetData("m_BuffStrID", ref this.m_BuffStrID);
			cNKMLua.GetData("m_BuffRemove", ref this.m_BuffRemove);
			cNKMLua.GetData("m_StateEndRemove", ref this.m_StateEndRemove);
			byte b = 0;
			if (cNKMLua.GetData("m_BuffLevel", ref b))
			{
				this.m_BuffStatLevel = b;
				this.m_BuffTimeLevel = b;
			}
			byte b2 = 0;
			if (cNKMLua.GetData("m_BuffLevelPerSkillLevel", ref b2))
			{
				this.m_BuffStatLevel = b2;
				this.m_BuffTimeLevel = b2;
			}
			cNKMLua.GetData("m_BuffStatLevel", ref this.m_BuffStatLevel);
			cNKMLua.GetData("m_BuffStatLevelPerSkillLevel", ref this.m_BuffStatLevelPerSkillLevel);
			cNKMLua.GetData("m_BuffTimeLevel", ref this.m_BuffTimeLevel);
			cNKMLua.GetData("m_BuffTimeLevelPerSkillLevel", ref this.m_BuffTimeLevelPerSkillLevel);
			cNKMLua.GetData("m_fRange", ref this.m_fRange);
			cNKMLua.GetData("m_bMyTeam", ref this.m_bMyTeam);
			cNKMLua.GetData("m_bEnemy", ref this.m_bEnemy);
			cNKMLua.GetData("m_bReflection", ref this.m_bReflection);
			int num = 0;
			if (cNKMLua.GetData("m_AddOverlap", ref num))
			{
				if (num >= 0)
				{
					this.m_Overlap = num + 1;
				}
				else
				{
					this.m_Overlap = num;
				}
			}
			cNKMLua.GetData("m_Overlap", ref this.m_Overlap);
			if (this.m_Overlap >= 255)
			{
				Log.ErrorAndExit(string.Format("[NKMEventBuff] Overlap is to big [{0}/{1}] BuffID[{2}]", this.m_Overlap, byte.MaxValue, this.m_BuffStrID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitStateEvent.cs", 1195);
				return false;
			}
			cNKMLua.GetData("m_fMinTargetHP", ref this.m_fMinTargetHP);
			cNKMLua.GetData("m_fMaxTargetHP", ref this.m_fMaxTargetHP);
			return true;
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x000B2B54 File Offset: 0x000B0D54
		public void Process(NKMGame cNKMGame, NKMUnit cNKMUnit)
		{
			int num = (int)this.m_BuffStatLevel;
			int num2 = (int)this.m_BuffTimeLevel;
			NKMUnitTemplet unitTemplet = cNKMUnit.GetUnitData().GetUnitTemplet();
			if (unitTemplet != null && unitTemplet.m_UnitTempletBase.m_NKM_UNIT_TYPE != NKM_UNIT_TYPE.NUT_SHIP)
			{
				NKMUnitSkillTemplet unitSkillTempletByType = cNKMUnit.GetUnitData().GetUnitSkillTempletByType(cNKMUnit.GetUnitStateNow().m_NKM_SKILL_TYPE);
				if (unitSkillTempletByType != null && unitSkillTempletByType.m_Level > 0)
				{
					if (this.m_BuffStatLevelPerSkillLevel > 0)
					{
						num += (unitSkillTempletByType.m_Level - 1) * (int)this.m_BuffStatLevelPerSkillLevel;
					}
					if (this.m_BuffTimeLevelPerSkillLevel > 0)
					{
						num2 = (unitSkillTempletByType.m_Level - 1) * (int)this.m_BuffTimeLevelPerSkillLevel;
					}
				}
			}
			if (!this.m_fRange.IsNearlyZero(1E-05f))
			{
				List<NKMUnit> sortUnitListByNearDist = cNKMUnit.GetSortUnitListByNearDist();
				this.ProcessRangeBuff(cNKMGame, cNKMUnit, (byte)num, (byte)num2, cNKMUnit.GetUnitSyncData().m_PosX, sortUnitListByNearDist);
				return;
			}
			if (this.m_fMinTargetHP > cNKMUnit.GetHPRate())
			{
				return;
			}
			if (this.m_fMaxTargetHP > 0f && this.m_fMaxTargetHP <= cNKMUnit.GetHPRate())
			{
				return;
			}
			if (this.m_BuffRemove)
			{
				cNKMUnit.DeleteBuff(this.m_BuffStrID, false);
				return;
			}
			cNKMUnit.AddBuffByStrID(this.m_BuffStrID, (byte)num, (byte)num2, cNKMUnit.GetUnitSyncData().m_GameUnitUID, true, false, this.m_StateEndRemove, (byte)this.m_Overlap);
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x000B2C84 File Offset: 0x000B0E84
		public void Process(NKMGame cNKMGame, NKMDamageEffect cNKMDamageEffect)
		{
			int num = (int)this.m_BuffStatLevel;
			int num2 = (int)this.m_BuffTimeLevel;
			NKMUnit masterUnit = cNKMDamageEffect.GetMasterUnit();
			NKMUnitTemplet unitTemplet = masterUnit.GetUnitData().GetUnitTemplet();
			if (unitTemplet != null && unitTemplet.m_UnitTempletBase.m_NKM_UNIT_TYPE != NKM_UNIT_TYPE.NUT_SHIP)
			{
				NKMUnitSkillTemplet unitSkillTempletByType = masterUnit.GetUnitData().GetUnitSkillTempletByType(masterUnit.GetUnitStateNow().m_NKM_SKILL_TYPE);
				if (unitSkillTempletByType != null && unitSkillTempletByType.m_Level > 0)
				{
					if (this.m_BuffStatLevelPerSkillLevel > 0)
					{
						num += (unitSkillTempletByType.m_Level - 1) * (int)this.m_BuffStatLevelPerSkillLevel;
					}
					if (this.m_BuffTimeLevelPerSkillLevel > 0)
					{
						num2 = (unitSkillTempletByType.m_Level - 1) * (int)this.m_BuffTimeLevelPerSkillLevel;
					}
				}
			}
			List<NKMUnit> sortUnitListByNearDist = cNKMDamageEffect.GetSortUnitListByNearDist();
			this.ProcessRangeBuff(cNKMGame, masterUnit, (byte)num, (byte)num2, cNKMDamageEffect.GetDEData().m_PosX, sortUnitListByNearDist);
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x000B2D40 File Offset: 0x000B0F40
		private void ProcessRangeBuff(NKMGame cNKMGame, NKMUnit cNKMUnit, byte buffLevel, byte buffTimeLevel, float posX, List<NKMUnit> cSortedUnitList)
		{
			for (int i = 0; i < cSortedUnitList.Count; i++)
			{
				NKMUnit nkmunit = cSortedUnitList[i];
				if (nkmunit.GetUnitSyncData().m_GameUnitUID != cNKMUnit.GetUnitSyncData().m_GameUnitUID && nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE != NKM_UNIT_STYLE_TYPE.NUST_ENV && nkmunit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DYING && nkmunit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DIE && (this.m_bMyTeam || cNKMGame.IsEnemy(cNKMUnit.GetUnitDataGame().m_NKM_TEAM_TYPE, nkmunit.GetUnitDataGame().m_NKM_TEAM_TYPE)) && (this.m_bEnemy || !cNKMGame.IsEnemy(cNKMUnit.GetUnitDataGame().m_NKM_TEAM_TYPE, nkmunit.GetUnitDataGame().m_NKM_TEAM_TYPE)))
				{
					if (this.m_fRange < Math.Abs(posX - nkmunit.GetUnitSyncData().m_PosX) || this.m_fMinTargetHP > nkmunit.GetHPRate() || (this.m_fMaxTargetHP > 0f && this.m_fMaxTargetHP <= nkmunit.GetHPRate()))
					{
						break;
					}
					if (this.m_BuffRemove)
					{
						bool bFromEnemy = cNKMGame.IsEnemy(cNKMUnit.GetUnitDataGame().m_NKM_TEAM_TYPE, nkmunit.GetUnitDataGame().m_NKM_TEAM_TYPE);
						nkmunit.DeleteBuff(this.m_BuffStrID, bFromEnemy);
					}
					else
					{
						nkmunit.AddBuffByStrID(this.m_BuffStrID, buffLevel, buffTimeLevel, cNKMUnit.GetUnitSyncData().m_GameUnitUID, true, false, this.m_StateEndRemove, (byte)this.m_Overlap);
					}
				}
			}
		}

		// Token: 0x040023DF RID: 9183
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x040023E0 RID: 9184
		public bool m_bAnimTime = true;

		// Token: 0x040023E1 RID: 9185
		public float m_fEventTime;

		// Token: 0x040023E2 RID: 9186
		public bool m_bStateEndTime;

		// Token: 0x040023E3 RID: 9187
		public string m_BuffStrID = "";

		// Token: 0x040023E4 RID: 9188
		public bool m_BuffRemove;

		// Token: 0x040023E5 RID: 9189
		public bool m_StateEndRemove;

		// Token: 0x040023E6 RID: 9190
		public byte m_BuffStatLevel = 1;

		// Token: 0x040023E7 RID: 9191
		public byte m_BuffStatLevelPerSkillLevel;

		// Token: 0x040023E8 RID: 9192
		public byte m_BuffTimeLevel = 1;

		// Token: 0x040023E9 RID: 9193
		public byte m_BuffTimeLevelPerSkillLevel;

		// Token: 0x040023EA RID: 9194
		public float m_fRange;

		// Token: 0x040023EB RID: 9195
		public bool m_bMyTeam;

		// Token: 0x040023EC RID: 9196
		public bool m_bEnemy;

		// Token: 0x040023ED RID: 9197
		public bool m_bReflection;

		// Token: 0x040023EE RID: 9198
		public int m_Overlap = 1;

		// Token: 0x040023EF RID: 9199
		public float m_fMinTargetHP;

		// Token: 0x040023F0 RID: 9200
		public float m_fMaxTargetHP;
	}
}
