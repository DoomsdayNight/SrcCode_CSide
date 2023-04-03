using System;
using System.Collections.Generic;
using NKM.Templet;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004CA RID: 1226
	public class NKMEventAttack : IEventConditionOwner, INKMUnitStateEvent
	{
		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06002273 RID: 8819 RVA: 0x000B21A1 File Offset: 0x000B03A1
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06002274 RID: 8820 RVA: 0x000B21A9 File Offset: 0x000B03A9
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTimeMin;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06002275 RID: 8821 RVA: 0x000B21B1 File Offset: 0x000B03B1
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Prohibited;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06002276 RID: 8822 RVA: 0x000B21B4 File Offset: 0x000B03B4
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06002277 RID: 8823 RVA: 0x000B21BC File Offset: 0x000B03BC
		public bool bStateEnd
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x000B22B0 File Offset: 0x000B04B0
		public void DeepCopyFromSource(NKMEventAttack source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_ConditionTarget.DeepCopyFromSource(source.m_ConditionTarget);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTimeMin = source.m_fEventTimeMin;
			this.m_fEventTimeMax = source.m_fEventTimeMax;
			this.m_fRangeMin = source.m_fRangeMin;
			this.m_fRangeMax = source.m_fRangeMax;
			this.m_bHitLand = source.m_bHitLand;
			this.m_bHitAir = source.m_bHitAir;
			this.m_bHitSummonOnly = source.m_bHitSummonOnly;
			this.m_bHitAwakenUnit = source.m_bHitAwakenUnit;
			this.m_bHitNormalUnit = source.m_bHitNormalUnit;
			this.m_bHitBossUnit = source.m_bHitBossUnit;
			this.m_bForceHit = source.m_bForceHit;
			this.m_bTrueDamage = source.m_bTrueDamage;
			this.m_bCleanHit = source.m_bCleanHit;
			this.m_NKM_DAMAGE_TARGET_TYPE = source.m_NKM_DAMAGE_TARGET_TYPE;
			this.m_listAllowStyle.Clear();
			this.m_listAllowStyle.UnionWith(source.m_listAllowStyle);
			this.m_listIgnoreStyle.Clear();
			this.m_listIgnoreStyle.UnionWith(source.m_listIgnoreStyle);
			this.m_listAllowRole.Clear();
			this.m_listAllowRole.UnionWith(source.m_listAllowRole);
			this.m_listIgnoreRole.Clear();
			this.m_listIgnoreRole.UnionWith(source.m_listIgnoreRole);
			this.m_TargetCostLess = source.m_TargetCostLess;
			this.m_TargetCostOver = source.m_TargetCostOver;
			this.m_AttackTargetUnit = source.m_AttackTargetUnit;
			this.m_AttackUnitCount = source.m_AttackUnitCount;
			this.m_AttackUnitCountOnly = source.m_AttackUnitCountOnly;
			this.m_bDamageSpeedDependRight = source.m_bDamageSpeedDependRight;
			this.m_DamageTempletName = (string)source.m_DamageTempletName.Clone();
			this.m_bForceCritical = source.m_bForceCritical;
			this.m_bNoCritical = source.m_bNoCritical;
			this.m_fGetAgroTime = source.m_fGetAgroTime;
			this.m_SoundName = source.m_SoundName;
			this.m_fLocalVol = source.m_fLocalVol;
			this.m_EffectName = (string)source.m_EffectName.Clone();
			this.m_HitStateChange = (string)source.m_HitStateChange.Clone();
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x000B24CC File Offset: 0x000B06CC
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			if (cNKMLua.OpenTable("m_Condition"))
			{
				this.m_Condition.LoadFromLUA(cNKMLua);
				cNKMLua.CloseTable();
			}
			if (cNKMLua.OpenTable("m_ConditionTarget"))
			{
				this.m_ConditionTarget.LoadFromLUA(cNKMLua);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_bAnimTime", ref this.m_bAnimTime);
			cNKMLua.GetData("m_fEventTimeMin", ref this.m_fEventTimeMin);
			cNKMLua.GetData("m_fEventTimeMax", ref this.m_fEventTimeMax);
			cNKMLua.GetData("m_fRangeMin", ref this.m_fRangeMin);
			cNKMLua.GetData("m_fRangeMax", ref this.m_fRangeMax);
			cNKMLua.GetData("m_bHitLand", ref this.m_bHitLand);
			cNKMLua.GetData("m_bHitAir", ref this.m_bHitAir);
			cNKMLua.GetData("m_bHitSummonOnly", ref this.m_bHitSummonOnly);
			cNKMLua.GetData("m_bHitAwakenUnit", ref this.m_bHitAwakenUnit);
			cNKMLua.GetData("m_bHitNormalUnit", ref this.m_bHitNormalUnit);
			cNKMLua.GetData("m_bHitBossUnit", ref this.m_bHitBossUnit);
			cNKMLua.GetData("m_bForceHit", ref this.m_bForceHit);
			cNKMLua.GetData("m_bTrueDamage", ref this.m_bTrueDamage);
			cNKMLua.GetData("m_bCleanHit", ref this.m_bCleanHit);
			cNKMLua.GetData<NKM_DAMAGE_TARGET_TYPE>("m_NKM_DAMAGE_TARGET_TYPE", ref this.m_NKM_DAMAGE_TARGET_TYPE);
			this.m_listAllowStyle.Clear();
			cNKMLua.GetDataListEnum<NKM_UNIT_STYLE_TYPE>("m_listAllowStyle", this.m_listAllowStyle, true);
			this.m_listIgnoreStyle.Clear();
			cNKMLua.GetDataListEnum<NKM_UNIT_STYLE_TYPE>("m_listIgnoreStyle", this.m_listIgnoreStyle, true);
			this.m_listAllowRole.Clear();
			cNKMLua.GetDataListEnum<NKM_UNIT_ROLE_TYPE>("m_listAllowRole", this.m_listAllowRole, true);
			this.m_listIgnoreRole.Clear();
			cNKMLua.GetDataListEnum<NKM_UNIT_ROLE_TYPE>("m_listIgnoreRole", this.m_listIgnoreRole, true);
			HashSet<NKM_UNIT_STYLE_TYPE> hashSet = new HashSet<NKM_UNIT_STYLE_TYPE>();
			if (cNKMLua.GetDataListEnum<NKM_UNIT_STYLE_TYPE>("m_listNKM_UNIT_STYLE_TYPE", hashSet, true))
			{
				this.m_listAllowStyle.UnionWith(hashSet);
			}
			HashSet<NKM_UNIT_ROLE_TYPE> hashSet2 = new HashSet<NKM_UNIT_ROLE_TYPE>();
			if (cNKMLua.GetDataListEnum<NKM_UNIT_ROLE_TYPE>("m_listNKM_UNIT_ROLE_TYPE", hashSet2, true))
			{
				this.m_listAllowRole.UnionWith(hashSet2);
			}
			cNKMLua.GetData("m_TargetCostLess", ref this.m_TargetCostLess);
			cNKMLua.GetData("m_TargetCostOver", ref this.m_TargetCostOver);
			cNKMLua.GetData("m_AttackTargetUnit", ref this.m_AttackTargetUnit);
			cNKMLua.GetData("m_AttackUnitCount", ref this.m_AttackUnitCount);
			cNKMLua.GetData("m_AttackUnitCountOnly", ref this.m_AttackUnitCountOnly);
			cNKMLua.GetData("m_bDamageSpeedDependRight", ref this.m_bDamageSpeedDependRight);
			cNKMLua.GetData("m_DamageTempletName", ref this.m_DamageTempletName);
			cNKMLua.GetData("m_bForceCritical", ref this.m_bForceCritical);
			cNKMLua.GetData("m_bNoCritical", ref this.m_bNoCritical);
			cNKMLua.GetData("m_fGetAgroTime", ref this.m_fGetAgroTime);
			cNKMLua.GetData("m_SoundName", ref this.m_SoundName);
			cNKMLua.GetData("m_fLocalVol", ref this.m_fLocalVol);
			cNKMLua.GetData("m_EffectName", ref this.m_EffectName);
			cNKMLua.GetData("m_HitStateChange", ref this.m_HitStateChange);
			return true;
		}

		// Token: 0x040023BC RID: 9148
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x040023BD RID: 9149
		public NKMEventCondition m_ConditionTarget = new NKMEventCondition();

		// Token: 0x040023BE RID: 9150
		public bool m_bAnimTime = true;

		// Token: 0x040023BF RID: 9151
		public float m_fEventTimeMin;

		// Token: 0x040023C0 RID: 9152
		public float m_fEventTimeMax;

		// Token: 0x040023C1 RID: 9153
		public float m_fRangeMin = 100f;

		// Token: 0x040023C2 RID: 9154
		public float m_fRangeMax = 100f;

		// Token: 0x040023C3 RID: 9155
		public bool m_bHitLand = true;

		// Token: 0x040023C4 RID: 9156
		public bool m_bHitAir = true;

		// Token: 0x040023C5 RID: 9157
		public bool m_bHitSummonOnly;

		// Token: 0x040023C6 RID: 9158
		public bool m_bHitAwakenUnit = true;

		// Token: 0x040023C7 RID: 9159
		public bool m_bHitNormalUnit = true;

		// Token: 0x040023C8 RID: 9160
		public bool m_bHitBossUnit = true;

		// Token: 0x040023C9 RID: 9161
		public bool m_bForceHit;

		// Token: 0x040023CA RID: 9162
		public bool m_bTrueDamage;

		// Token: 0x040023CB RID: 9163
		public bool m_bCleanHit;

		// Token: 0x040023CC RID: 9164
		public NKM_DAMAGE_TARGET_TYPE m_NKM_DAMAGE_TARGET_TYPE = NKM_DAMAGE_TARGET_TYPE.NDTT_ENEMY;

		// Token: 0x040023CD RID: 9165
		public HashSet<NKM_UNIT_STYLE_TYPE> m_listAllowStyle = new HashSet<NKM_UNIT_STYLE_TYPE>();

		// Token: 0x040023CE RID: 9166
		public HashSet<NKM_UNIT_STYLE_TYPE> m_listIgnoreStyle = new HashSet<NKM_UNIT_STYLE_TYPE>();

		// Token: 0x040023CF RID: 9167
		public HashSet<NKM_UNIT_ROLE_TYPE> m_listAllowRole = new HashSet<NKM_UNIT_ROLE_TYPE>();

		// Token: 0x040023D0 RID: 9168
		public HashSet<NKM_UNIT_ROLE_TYPE> m_listIgnoreRole = new HashSet<NKM_UNIT_ROLE_TYPE>();

		// Token: 0x040023D1 RID: 9169
		public int m_TargetCostLess = -1;

		// Token: 0x040023D2 RID: 9170
		public int m_TargetCostOver = -1;

		// Token: 0x040023D3 RID: 9171
		public bool m_AttackTargetUnit = true;

		// Token: 0x040023D4 RID: 9172
		public int m_AttackUnitCount = 1;

		// Token: 0x040023D5 RID: 9173
		public bool m_AttackUnitCountOnly;

		// Token: 0x040023D6 RID: 9174
		public bool m_bDamageSpeedDependRight;

		// Token: 0x040023D7 RID: 9175
		public string m_DamageTempletName = "";

		// Token: 0x040023D8 RID: 9176
		public bool m_bForceCritical;

		// Token: 0x040023D9 RID: 9177
		public bool m_bNoCritical;

		// Token: 0x040023DA RID: 9178
		public float m_fGetAgroTime;

		// Token: 0x040023DB RID: 9179
		public string m_SoundName = "";

		// Token: 0x040023DC RID: 9180
		public float m_fLocalVol = 1f;

		// Token: 0x040023DD RID: 9181
		public string m_EffectName = "";

		// Token: 0x040023DE RID: 9182
		public string m_HitStateChange = "";
	}
}
