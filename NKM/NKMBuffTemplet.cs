using System;
using System.Collections.Generic;
using Cs.Logging;
using NKC;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x020003AF RID: 943
	public class NKMBuffTemplet : INKMTemplet
	{
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060018B9 RID: 6329 RVA: 0x0006382B File Offset: 0x00061A2B
		public int Key
		{
			get
			{
				return (int)this.m_BuffID;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060018BA RID: 6330 RVA: 0x00063833 File Offset: 0x00061A33
		public bool IsBarrierBuff
		{
			get
			{
				return this.m_fBarrierHP > 0f;
			}
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x00063842 File Offset: 0x00061A42
		public static NKMBuffTemplet Find(string key)
		{
			return NKMTempletContainer<NKMBuffTemplet>.Find(key);
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x0006384C File Offset: 0x00061A4C
		public static NKMBuffTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			NKMBuffTemplet nkmbuffTemplet = new NKMBuffTemplet();
			cNKMLua.GetData("m_BuffID", ref nkmbuffTemplet.m_BuffID);
			cNKMLua.GetData("m_BuffStrID", ref nkmbuffTemplet.m_BuffStrID);
			cNKMLua.GetData("m_bDebuff", ref nkmbuffTemplet.m_bDebuff);
			nkmbuffTemplet.m_bDebuffSon = nkmbuffTemplet.m_bDebuff;
			cNKMLua.GetData("m_bDebuffSon", ref nkmbuffTemplet.m_bDebuffSon);
			cNKMLua.GetData("m_bInfinity", ref nkmbuffTemplet.m_bInfinity);
			cNKMLua.GetData("m_fLifeTime", ref nkmbuffTemplet.m_fLifeTime);
			cNKMLua.GetData("m_fLifeTimePerLevel", ref nkmbuffTemplet.m_fLifeTimePerLevel);
			cNKMLua.GetData("m_Range", ref nkmbuffTemplet.m_Range);
			cNKMLua.GetData("m_RangeSonCount", ref nkmbuffTemplet.m_RangeSonCount);
			cNKMLua.GetData("m_RangeOverlap", ref nkmbuffTemplet.m_RangeOverlap);
			cNKMLua.GetData("m_bNoRefresh", ref nkmbuffTemplet.m_bNoRefresh);
			cNKMLua.GetData("m_MaxOverlapCount", ref nkmbuffTemplet.m_MaxOverlapCount);
			cNKMLua.GetData("m_MaxOverlapBuffStrID", ref nkmbuffTemplet.m_MaxOverlapBuffStrID);
			cNKMLua.GetData("m_bShipSkillPos", ref nkmbuffTemplet.m_bShipSkillPos);
			cNKMLua.GetData("m_fOffsetX", ref nkmbuffTemplet.m_fOffsetX);
			cNKMLua.GetData("m_bShowBuffIcon", ref nkmbuffTemplet.m_bShowBuffIcon);
			cNKMLua.GetData("m_bShowBuffText", ref nkmbuffTemplet.m_bShowBuffText);
			cNKMLua.GetData("m_IconName", ref nkmbuffTemplet.m_IconName);
			cNKMLua.GetData("m_RangeEffectName", ref nkmbuffTemplet.m_RangeEffectName);
			cNKMLua.GetData("m_MasterEffectName", ref nkmbuffTemplet.m_MasterEffectName);
			cNKMLua.GetData("m_MasterEffectNameSkinDic", ref nkmbuffTemplet.m_MasterEffectNameSkinDic);
			cNKMLua.GetData("m_MasterEffectBoneName", ref nkmbuffTemplet.m_MasterEffectBoneName);
			cNKMLua.GetData("m_SlaveEffectName", ref nkmbuffTemplet.m_SlaveEffectName);
			cNKMLua.GetData("m_SlaveEffectNameSkinDic", ref nkmbuffTemplet.m_SlaveEffectNameSkinDic);
			cNKMLua.GetData("m_SlaveEffectBoneName", ref nkmbuffTemplet.m_SlaveEffectBoneName);
			cNKMLua.GetData("m_bIgnoreUnitScaleFactor", ref nkmbuffTemplet.m_bIgnoreUnitScaleFactor);
			cNKMLua.GetData("m_MasterColorR", ref nkmbuffTemplet.m_MasterColorR);
			cNKMLua.GetData("m_MasterColorG", ref nkmbuffTemplet.m_MasterColorG);
			cNKMLua.GetData("m_MasterColorB", ref nkmbuffTemplet.m_MasterColorB);
			cNKMLua.GetData("m_ColorR", ref nkmbuffTemplet.m_ColorR);
			cNKMLua.GetData("m_ColorG", ref nkmbuffTemplet.m_ColorG);
			cNKMLua.GetData("m_ColorB", ref nkmbuffTemplet.m_ColorB);
			cNKMLua.GetData("m_bNoReuse", ref nkmbuffTemplet.m_bNoReuse);
			cNKMLua.GetData("m_bAllowAirUnit", ref nkmbuffTemplet.m_bAllowAirUnit);
			cNKMLua.GetData("m_bAllowLandUnit", ref nkmbuffTemplet.m_bAllowLandUnit);
			cNKMLua.GetData("m_bAllowAwaken", ref nkmbuffTemplet.m_bAllowAwaken);
			cNKMLua.GetData("m_bAllowNormal", ref nkmbuffTemplet.m_bAllowNormal);
			cNKMLua.GetData("m_bRangeSonAllowAirUnit", ref nkmbuffTemplet.m_bRangeSonAllowAirUnit);
			cNKMLua.GetData("m_bRangeSonAllowLandUnit", ref nkmbuffTemplet.m_bRangeSonAllowLandUnit);
			cNKMLua.GetData("m_bRangeSonAllowAwaken", ref nkmbuffTemplet.m_bRangeSonAllowAwaken);
			cNKMLua.GetData("m_bRangeSonAllowNormal", ref nkmbuffTemplet.m_bRangeSonAllowNormal);
			cNKMLua.GetData("m_bRangeSonOnlyTarget", ref nkmbuffTemplet.m_bRangeSonOnlyTarget);
			cNKMLua.GetData("m_bRangeSonOnlySubTarget", ref nkmbuffTemplet.m_bRangeSonOnlySubTarget);
			cNKMLua.GetData("m_bAllowBoss", ref nkmbuffTemplet.m_bAllowBoss);
			nkmbuffTemplet.m_listAllowUnitID.Clear();
			if (cNKMLua.OpenTable("m_listAllowUnitID"))
			{
				int num = 1;
				int item = 0;
				while (cNKMLua.GetData(num, ref item))
				{
					nkmbuffTemplet.m_listAllowUnitID.Add(item);
					num++;
				}
				cNKMLua.CloseTable();
			}
			nkmbuffTemplet.m_listIgnoreUnitID.Clear();
			if (cNKMLua.OpenTable("m_listIgnoreUnitID"))
			{
				int num2 = 1;
				int item2 = 0;
				while (cNKMLua.GetData(num2, ref item2))
				{
					nkmbuffTemplet.m_listIgnoreUnitID.Add(item2);
					num2++;
				}
				cNKMLua.CloseTable();
			}
			nkmbuffTemplet.m_listAllowStyleType.Clear();
			if (cNKMLua.OpenTable("m_listAllowStyleType"))
			{
				int num3 = 1;
				NKM_UNIT_STYLE_TYPE item3 = NKM_UNIT_STYLE_TYPE.NUST_INVALID;
				while (cNKMLua.GetData<NKM_UNIT_STYLE_TYPE>(num3, ref item3))
				{
					nkmbuffTemplet.m_listAllowStyleType.Add(item3);
					num3++;
				}
				cNKMLua.CloseTable();
			}
			nkmbuffTemplet.m_listAllowRoleType.Clear();
			if (cNKMLua.OpenTable("m_listAllowRoleType"))
			{
				int num4 = 1;
				NKM_UNIT_ROLE_TYPE item4 = NKM_UNIT_ROLE_TYPE.NURT_INVALID;
				while (cNKMLua.GetData<NKM_UNIT_ROLE_TYPE>(num4, ref item4))
				{
					nkmbuffTemplet.m_listAllowRoleType.Add(item4);
					num4++;
				}
				cNKMLua.CloseTable();
			}
			nkmbuffTemplet.m_listIgnoreStyleType.Clear();
			if (cNKMLua.OpenTable("m_listIgnoreStyleType"))
			{
				int num5 = 1;
				NKM_UNIT_STYLE_TYPE item5 = NKM_UNIT_STYLE_TYPE.NUST_INVALID;
				while (cNKMLua.GetData<NKM_UNIT_STYLE_TYPE>(num5, ref item5))
				{
					nkmbuffTemplet.m_listIgnoreStyleType.Add(item5);
					num5++;
				}
				cNKMLua.CloseTable();
			}
			nkmbuffTemplet.m_listIgnoreRoleType.Clear();
			if (cNKMLua.OpenTable("m_listIgnoreRoleType"))
			{
				int num6 = 1;
				NKM_UNIT_ROLE_TYPE item6 = NKM_UNIT_ROLE_TYPE.NURT_INVALID;
				while (cNKMLua.GetData<NKM_UNIT_ROLE_TYPE>(num6, ref item6))
				{
					nkmbuffTemplet.m_listIgnoreRoleType.Add(item6);
					num6++;
				}
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_bRangeSonAllowBoss", ref nkmbuffTemplet.m_bRangeSonAllowBoss);
			nkmbuffTemplet.m_listRangeSonAllowUnitID.Clear();
			if (cNKMLua.OpenTable("m_listRangeSonAllowUnitID"))
			{
				int num7 = 1;
				int item7 = 0;
				while (cNKMLua.GetData(num7, ref item7))
				{
					nkmbuffTemplet.m_listRangeSonAllowUnitID.Add(item7);
					num7++;
				}
				cNKMLua.CloseTable();
			}
			nkmbuffTemplet.m_listRangeSonIgnoreUnitID.Clear();
			if (cNKMLua.OpenTable("m_listRangeSonIgnoreUnitID"))
			{
				int num8 = 1;
				int item8 = 0;
				while (cNKMLua.GetData(num8, ref item8))
				{
					nkmbuffTemplet.m_listRangeSonIgnoreUnitID.Add(item8);
					num8++;
				}
				cNKMLua.CloseTable();
			}
			nkmbuffTemplet.m_listRangeSonAllowStyleType.Clear();
			if (cNKMLua.OpenTable("m_listRangeSonAllowStyleType"))
			{
				int num9 = 1;
				NKM_UNIT_STYLE_TYPE item9 = NKM_UNIT_STYLE_TYPE.NUST_INVALID;
				while (cNKMLua.GetData<NKM_UNIT_STYLE_TYPE>(num9, ref item9))
				{
					nkmbuffTemplet.m_listRangeSonAllowStyleType.Add(item9);
					num9++;
				}
				cNKMLua.CloseTable();
			}
			nkmbuffTemplet.m_listRangeSonAllowRoleType.Clear();
			if (cNKMLua.OpenTable("m_listRangeSonAllowRoleType"))
			{
				int num10 = 1;
				NKM_UNIT_ROLE_TYPE item10 = NKM_UNIT_ROLE_TYPE.NURT_INVALID;
				while (cNKMLua.GetData<NKM_UNIT_ROLE_TYPE>(num10, ref item10))
				{
					nkmbuffTemplet.m_listRangeSonAllowRoleType.Add(item10);
					num10++;
				}
				cNKMLua.CloseTable();
			}
			nkmbuffTemplet.m_listRangeSonIgnoreStyleType.Clear();
			if (cNKMLua.OpenTable("m_listRangeSonIgnoreStyleType"))
			{
				int num11 = 1;
				NKM_UNIT_STYLE_TYPE item11 = NKM_UNIT_STYLE_TYPE.NUST_INVALID;
				while (cNKMLua.GetData<NKM_UNIT_STYLE_TYPE>(num11, ref item11))
				{
					nkmbuffTemplet.m_listRangeSonIgnoreStyleType.Add(item11);
					num11++;
				}
				cNKMLua.CloseTable();
			}
			nkmbuffTemplet.m_listRangeSonIgnoreRoleType.Clear();
			if (cNKMLua.OpenTable("m_listRangeSonIgnoreRoleType"))
			{
				int num12 = 1;
				NKM_UNIT_ROLE_TYPE item12 = NKM_UNIT_ROLE_TYPE.NURT_INVALID;
				while (cNKMLua.GetData<NKM_UNIT_ROLE_TYPE>(num12, ref item12))
				{
					nkmbuffTemplet.m_listRangeSonIgnoreRoleType.Add(item12);
					num12++;
				}
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_AffectMe", ref nkmbuffTemplet.m_AffectMe);
			cNKMLua.GetData("m_AffectMasterTeam", ref nkmbuffTemplet.m_AffectMasterTeam);
			cNKMLua.GetData("m_AffectMasterEnemyTeam", ref nkmbuffTemplet.m_AffectMasterEnemyTeam);
			cNKMLua.GetData("m_AffectMultiRespawnMinCount", ref nkmbuffTemplet.m_AffectMultiRespawnMinCount);
			cNKMLua.GetData("m_AffectSonMultiRespawnMinCount", ref nkmbuffTemplet.m_AffectSonMultiRespawnMinCount);
			nkmbuffTemplet.m_AffectCostRange.LoadFromLua(cNKMLua, "m_AffectCostRange");
			cNKMLua.GetDataEnum<NKMBuffTemplet.AffectSummonType>("m_eAffectSummonType", out nkmbuffTemplet.m_eAffectSummonType);
			nkmbuffTemplet.m_ApplyStatus.Clear();
			cNKMLua.GetDataListEnum<NKM_UNIT_STATUS_EFFECT>("m_ApplyStatus", nkmbuffTemplet.m_ApplyStatus, true);
			nkmbuffTemplet.m_ImmuneStatus.Clear();
			cNKMLua.GetDataListEnum<NKM_UNIT_STATUS_EFFECT>("m_ImmuneStatus", nkmbuffTemplet.m_ImmuneStatus, true);
			NKMBuffTemplet.LoadStatusFromBool(cNKMLua, NKM_UNIT_STATUS_EFFECT.NUSE_DETECTER, "m_bDetect", ref nkmbuffTemplet.m_ApplyStatus);
			NKMBuffTemplet.LoadStatusFromBool(cNKMLua, NKM_UNIT_STATUS_EFFECT.NUSE_CLOCKING, "m_bNoTargeted", ref nkmbuffTemplet.m_ApplyStatus);
			NKMBuffTemplet.LoadStatusFromBool(cNKMLua, NKM_UNIT_STATUS_EFFECT.NUSE_SLEEP, "m_bSleep", ref nkmbuffTemplet.m_ApplyStatus);
			NKMBuffTemplet.LoadStatusFromBool(cNKMLua, NKM_UNIT_STATUS_EFFECT.NUSE_SLEEP, "m_bImmuneSleep", ref nkmbuffTemplet.m_ImmuneStatus);
			NKMBuffTemplet.LoadStatusFromBool(cNKMLua, NKM_UNIT_STATUS_EFFECT.NUSE_SILENCE, "m_bSilence", ref nkmbuffTemplet.m_ApplyStatus);
			NKMBuffTemplet.LoadStatusFromBool(cNKMLua, NKM_UNIT_STATUS_EFFECT.NUSE_SILENCE, "m_bImmuneSilence", ref nkmbuffTemplet.m_ImmuneStatus);
			NKMBuffTemplet.LoadStatusFromBool(cNKMLua, NKM_UNIT_STATUS_EFFECT.NUSE_INVINCIBLE, "m_bInvincible", ref nkmbuffTemplet.m_ApplyStatus);
			NKMBuffTemplet.LoadStatusFromBool(cNKMLua, NKM_UNIT_STATUS_EFFECT.NUSE_FORCE_MISS, "m_bForceMiss", ref nkmbuffTemplet.m_ApplyStatus);
			NKMBuffTemplet.LoadStatusFromBool(cNKMLua, NKM_UNIT_STATUS_EFFECT.NUSE_FORCE_MISS, "m_bImmuneForceMiss", ref nkmbuffTemplet.m_ImmuneStatus);
			NKMBuffTemplet.LoadStatusFromBool(cNKMLua, NKM_UNIT_STATUS_EFFECT.NUSE_FORCE_EVADE, "m_bForceEvade", ref nkmbuffTemplet.m_ApplyStatus);
			NKMBuffTemplet.LoadStatusFromBool(cNKMLua, NKM_UNIT_STATUS_EFFECT.NUSE_FORCE_HIT, "m_bForceHit", ref nkmbuffTemplet.m_ApplyStatus);
			NKMBuffTemplet.LoadStatusFromBool(cNKMLua, NKM_UNIT_STATUS_EFFECT.NUSE_CONFUSE, "m_bConfuse", ref nkmbuffTemplet.m_ApplyStatus);
			NKMBuffTemplet.LoadStatusFromBool(cNKMLua, NKM_UNIT_STATUS_EFFECT.NUSE_CONFUSE, "m_bImmuneConfuse", ref nkmbuffTemplet.m_ImmuneStatus);
			NKMBuffTemplet.LoadStatusFromBool(cNKMLua, NKM_UNIT_STATUS_EFFECT.NUSE_IMMORTAL, "m_bImmortal", ref nkmbuffTemplet.m_ApplyStatus);
			NKMBuffTemplet.LoadStatusFromBool(cNKMLua, NKM_UNIT_STATUS_EFFECT.NUSE_BUFF_DAMAGE_IMMUNE, "m_bImmuneBuffDamage", ref nkmbuffTemplet.m_ApplyStatus);
			NKMBuffTemplet.LoadStatusFromBool(cNKMLua, NKM_UNIT_STATUS_EFFECT.NUSE_NULLIFY_BARRIER, "m_bCrashBarrier", ref nkmbuffTemplet.m_ApplyStatus);
			NKMBuffTemplet.LoadStatusFromBool(cNKMLua, NKM_UNIT_STATUS_EFFECT.NUSE_NULLIFY_BARRIER, "m_bImmuneBarrier", ref nkmbuffTemplet.m_ApplyStatus);
			NKMBuffTemplet.LoadStatusFromBool(cNKMLua, NKM_UNIT_STATUS_EFFECT.NUSE_IMMUNE_MOVE_SLOW, "m_bImmuneMoveSlow", ref nkmbuffTemplet.m_ApplyStatus);
			NKMBuffTemplet.LoadStatusFromBool(cNKMLua, NKM_UNIT_STATUS_EFFECT.NUSE_IMMUNE_ATTACK_SLOW, "m_bImmuneAttackSlow", ref nkmbuffTemplet.m_ApplyStatus);
			NKMBuffTemplet.LoadStatusFromBool(cNKMLua, NKM_UNIT_STATUS_EFFECT.NUSE_IMMUNE_AIRBORNE, "m_bImmuneAirborne", ref nkmbuffTemplet.m_ApplyStatus);
			NKMBuffTemplet.LoadStatusFromBool(cNKMLua, NKM_UNIT_STATUS_EFFECT.NUSE_IMMUNE_SKILL_COOLTIME_DAMAGE, "m_bImmuneSkillCoolTime", ref nkmbuffTemplet.m_ApplyStatus);
			NKMBuffTemplet.LoadStatusFromBool(cNKMLua, NKM_UNIT_STATUS_EFFECT.NUSE_STUN, "m_bStun", ref nkmbuffTemplet.m_ApplyStatus);
			NKMBuffTemplet.LoadStatusFromBool(cNKMLua, NKM_UNIT_STATUS_EFFECT.NUSE_STUN, "m_bImmuneStun", ref nkmbuffTemplet.m_ImmuneStatus);
			cNKMLua.GetData("m_AddAttackUnitCount", ref nkmbuffTemplet.m_AddAttackUnitCount);
			cNKMLua.GetData("m_fAddAttackRange", ref nkmbuffTemplet.m_fAddAttackRange);
			cNKMLua.GetData<NKM_SUPER_ARMOR_LEVEL>("m_SuperArmorLevel", ref nkmbuffTemplet.m_SuperArmorLevel);
			cNKMLua.GetData("m_bNotDispel", ref nkmbuffTemplet.m_bNotDispel);
			cNKMLua.GetData("m_bRangeSonDispelBuff", ref nkmbuffTemplet.m_bRangeSonDispelBuff);
			cNKMLua.GetData("m_bRangeSonDispelDebuff", ref nkmbuffTemplet.m_bRangeSonDispelDebuff);
			cNKMLua.GetData("m_bDispelBuff", ref nkmbuffTemplet.m_bDispelBuff);
			cNKMLua.GetData("m_bDispelDebuff", ref nkmbuffTemplet.m_bDispelDebuff);
			cNKMLua.GetData("m_bNotCastSummon", ref nkmbuffTemplet.m_bNotCastSummon);
			cNKMLua.GetData("m_bIgnoreBlock", ref nkmbuffTemplet.m_bIgnoreBlock);
			cNKMLua.GetData("m_fDamageTransfer", ref nkmbuffTemplet.m_fDamageTransfer);
			cNKMLua.GetData("m_fDamageReflection", ref nkmbuffTemplet.m_fDamageReflection);
			cNKMLua.GetData("m_fHealFeedback", ref nkmbuffTemplet.m_fHealFeedback);
			cNKMLua.GetData("m_fHealFeedbackPerLevel", ref nkmbuffTemplet.m_fHealFeedbackPerLevel);
			cNKMLua.GetData("m_bGuard", ref nkmbuffTemplet.m_bGuard);
			cNKMLua.GetData("m_bBarrierHPRate", ref nkmbuffTemplet.m_bBarrierHPRate);
			cNKMLua.GetData("m_fBarrierHP", ref nkmbuffTemplet.m_fBarrierHP);
			cNKMLua.GetData("m_fBarrierHPPerLevel", ref nkmbuffTemplet.m_fBarrierHPPerLevel);
			cNKMLua.GetData("m_BarrierDamageEffectName", ref nkmbuffTemplet.m_BarrierDamageEffectName);
			cNKMLua.GetData("m_DamageTempletStrID", ref nkmbuffTemplet.m_DamageTempletStrID);
			if (nkmbuffTemplet.m_DamageTempletStrID.Length > 1)
			{
				nkmbuffTemplet.m_NKMDamageTemplet = NKMDamageManager.GetTempletByStrID(nkmbuffTemplet.m_DamageTempletStrID);
			}
			else
			{
				nkmbuffTemplet.m_NKMDamageTemplet = null;
			}
			cNKMLua.GetData("m_StartDTStrID", ref nkmbuffTemplet.m_StartDTStrID);
			if (nkmbuffTemplet.m_StartDTStrID.Length > 1)
			{
				nkmbuffTemplet.m_DTStart = NKMDamageManager.GetTempletByStrID(nkmbuffTemplet.m_StartDTStrID);
			}
			else
			{
				nkmbuffTemplet.m_DTStart = null;
			}
			cNKMLua.GetData("m_EndDTStrID", ref nkmbuffTemplet.m_EndDTStrID);
			if (nkmbuffTemplet.m_EndDTStrID.Length > 1)
			{
				nkmbuffTemplet.m_DTEnd = NKMDamageManager.GetTempletByStrID(nkmbuffTemplet.m_EndDTStrID);
			}
			else
			{
				nkmbuffTemplet.m_DTEnd = null;
			}
			cNKMLua.GetData("m_DispelDTStrID", ref nkmbuffTemplet.m_DispelDTStrID);
			if (nkmbuffTemplet.m_DispelDTStrID.Length > 1)
			{
				nkmbuffTemplet.m_DTDispel = NKMDamageManager.GetTempletByStrID(nkmbuffTemplet.m_DispelDTStrID);
			}
			else
			{
				nkmbuffTemplet.m_DTDispel = null;
			}
			cNKMLua.GetData("m_fOneTimeHPDamageRate", ref nkmbuffTemplet.m_fOneTimeHPDamageRate);
			cNKMLua.GetData("m_EventHealStrID", ref nkmbuffTemplet.m_EventHealStrID);
			if (nkmbuffTemplet.m_EventHealStrID.Length > 1)
			{
				nkmbuffTemplet.m_NKMEventHeal = NKMCommonUnitEvent.GetNKMEventHeal(nkmbuffTemplet.m_EventHealStrID);
			}
			else
			{
				nkmbuffTemplet.m_NKMEventHeal = null;
			}
			cNKMLua.GetData("m_bUnitDieEvent", ref nkmbuffTemplet.m_bUnitDieEvent);
			cNKMLua.GetData("m_UnitLevel", ref nkmbuffTemplet.m_UnitLevel);
			cNKMLua.GetData("m_FinalUnitStateChange", ref nkmbuffTemplet.m_FinalUnitStateChange);
			cNKMLua.GetData("m_FinalBuffStrID", ref nkmbuffTemplet.m_FinalBuffStrID);
			cNKMLua.GetData<NKM_STAT_TYPE>("m_StatType1", ref nkmbuffTemplet.m_StatType1);
			cNKMLua.GetData("m_StatValue1", ref nkmbuffTemplet.m_StatValue1);
			cNKMLua.GetData("m_StatFactor1", ref nkmbuffTemplet.m_StatFactor1);
			cNKMLua.GetData("m_StatAddPerLevel1", ref nkmbuffTemplet.m_StatAddPerLevel1);
			cNKMLua.GetData<NKM_STAT_TYPE>("m_StatType2", ref nkmbuffTemplet.m_StatType2);
			cNKMLua.GetData("m_StatValue2", ref nkmbuffTemplet.m_StatValue2);
			cNKMLua.GetData("m_StatFactor2", ref nkmbuffTemplet.m_StatFactor2);
			cNKMLua.GetData("m_StatAddPerLevel2", ref nkmbuffTemplet.m_StatAddPerLevel2);
			cNKMLua.GetData<NKM_STAT_TYPE>("m_StatType3", ref nkmbuffTemplet.m_StatType3);
			cNKMLua.GetData("m_StatValue3", ref nkmbuffTemplet.m_StatValue3);
			cNKMLua.GetData("m_StatFactor3", ref nkmbuffTemplet.m_StatFactor3);
			cNKMLua.GetData("m_StatAddPerLevel3", ref nkmbuffTemplet.m_StatAddPerLevel3);
			return nkmbuffTemplet;
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x000644E4 File Offset: 0x000626E4
		private static void LoadStatusFromBool(NKMLua cNKMLua, NKM_UNIT_STATUS_EFFECT status, string oldName, ref HashSet<NKM_UNIT_STATUS_EFFECT> targetHashSet)
		{
			bool flag = false;
			cNKMLua.GetData(oldName, ref flag);
			if (flag)
			{
				targetHashSet.Add(status);
			}
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x00064509 File Offset: 0x00062709
		public bool IsFixedPosBuff()
		{
			return this.m_bShipSkillPos || this.m_fOffsetX != 0f;
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x00064525 File Offset: 0x00062725
		public void Join()
		{
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x00064528 File Offset: 0x00062728
		public void Validate()
		{
			if (!string.IsNullOrEmpty(this.m_FinalBuffStrID) && NKMBuffManager.GetBuffTempletByStrID(this.m_FinalBuffStrID) == null)
			{
				Log.ErrorAndExit(string.Concat(new string[]
				{
					"[NKMBuffTemplet] m_FinalBuffStrID is invalid. m_BuffStrID [",
					this.m_BuffStrID,
					"], m_FinalBuffStrID [",
					this.m_FinalBuffStrID,
					"]"
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMBuffManager.cs", 689);
			}
			if (this.m_RangeOverlap)
			{
				if (this.m_MaxOverlapCount > 1)
				{
					Log.ErrorAndExit("[NKMBuffTemplet] m_MaxOverlapCount > 1 is not allowed when m_RangeOverlap is true.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMBuffManager.cs", 697);
				}
				if (this.m_RangeSonCount < 1)
				{
					Log.ErrorAndExit("[NKMBuffTemplet] m_RangeSonCount must 1 or higher for m_RangeOverlap buff", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMBuffManager.cs", 702);
				}
			}
			if (!string.IsNullOrEmpty(this.m_MaxOverlapBuffStrID) && this.m_MaxOverlapCount <= 1)
			{
				Log.ErrorAndExit("[NKMBuffTemplet] if buff has m_MaxOverlapBuffStrID, m_MaxOverlapCount must 2 or higher!", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMBuffManager.cs", 710);
			}
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x00064600 File Offset: 0x00062800
		public float GetBarrierHPMax(float fHPMax, float fReinforceBarrier, int buffLevel)
		{
			if (this.m_fBarrierHP < 0f)
			{
				return -1f;
			}
			if (!this.m_bBarrierHPRate)
			{
				return (this.m_fBarrierHP + this.m_fBarrierHPPerLevel * (float)(buffLevel - 1)) * (1f + fReinforceBarrier);
			}
			float num = fHPMax * (this.m_fBarrierHP + this.m_fBarrierHPPerLevel * (float)(buffLevel - 1));
			return num + num * fReinforceBarrier;
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x0006465C File Offset: 0x0006285C
		public float GetLifeTimeMax(int timeLevel)
		{
			float num = this.m_fLifeTime;
			if (this.m_fLifeTime > 0f && timeLevel > 0)
			{
				num += this.m_fLifeTimePerLevel * (float)(timeLevel - 1);
			}
			return num;
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x00064690 File Offset: 0x00062890
		public bool HasStatus(NKM_UNIT_STATUS_EFFECT status)
		{
			return this.m_ApplyStatus.Contains(status);
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x0006469E File Offset: 0x0006289E
		public bool HasImmuneStatus(NKM_UNIT_STATUS_EFFECT status)
		{
			return this.m_ImmuneStatus.Contains(status);
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x000646AC File Offset: 0x000628AC
		public string GetMasterEffectName(int skinID)
		{
			if (skinID == 0 || this.m_dicMasterEffect == null)
			{
				return this.m_MasterEffectName;
			}
			string result;
			if (this.m_dicMasterEffect.TryGetValue(skinID, out result))
			{
				return result;
			}
			return this.m_MasterEffectBoneName;
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x000646E4 File Offset: 0x000628E4
		public string GetSlaveEffectName(int skinID)
		{
			if (skinID == 0 || this.m_dicSlaveEffect == null)
			{
				return this.m_SlaveEffectName;
			}
			string result;
			if (this.m_dicSlaveEffect.TryGetValue(skinID, out result))
			{
				return result;
			}
			return this.m_SlaveEffectBoneName;
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x0006471C File Offset: 0x0006291C
		public void ParseSkinDic()
		{
			if (!string.IsNullOrEmpty(this.m_MasterEffectNameSkinDic))
			{
				this.m_dicMasterEffect = NKCUtil.ParseIntKeyTable(this.m_MasterEffectNameSkinDic);
			}
			else
			{
				this.m_dicMasterEffect = null;
			}
			if (!string.IsNullOrEmpty(this.m_SlaveEffectNameSkinDic))
			{
				this.m_dicSlaveEffect = NKCUtil.ParseIntKeyTable(this.m_SlaveEffectNameSkinDic);
				return;
			}
			this.m_dicSlaveEffect = null;
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x00064778 File Offset: 0x00062978
		public static void ParseAllSkinDic()
		{
			foreach (NKMBuffTemplet nkmbuffTemplet in NKMTempletContainer<NKMBuffTemplet>.Values)
			{
				nkmbuffTemplet.ParseSkinDic();
			}
		}

		// Token: 0x0400107C RID: 4220
		public short m_BuffID;

		// Token: 0x0400107D RID: 4221
		public string m_BuffStrID = "";

		// Token: 0x0400107E RID: 4222
		public bool m_bDebuff;

		// Token: 0x0400107F RID: 4223
		public bool m_bDebuffSon;

		// Token: 0x04001080 RID: 4224
		public bool m_bInfinity;

		// Token: 0x04001081 RID: 4225
		public float m_fLifeTime = -1f;

		// Token: 0x04001082 RID: 4226
		public float m_fLifeTimePerLevel;

		// Token: 0x04001083 RID: 4227
		public float m_Range;

		// Token: 0x04001084 RID: 4228
		public int m_RangeSonCount = 99999;

		// Token: 0x04001085 RID: 4229
		public bool m_RangeOverlap;

		// Token: 0x04001086 RID: 4230
		public bool m_bNoRefresh;

		// Token: 0x04001087 RID: 4231
		public byte m_MaxOverlapCount = 1;

		// Token: 0x04001088 RID: 4232
		public string m_MaxOverlapBuffStrID = "";

		// Token: 0x04001089 RID: 4233
		public bool m_bShipSkillPos;

		// Token: 0x0400108A RID: 4234
		public float m_fOffsetX;

		// Token: 0x0400108B RID: 4235
		public bool m_bShowBuffIcon = true;

		// Token: 0x0400108C RID: 4236
		public bool m_bShowBuffText = true;

		// Token: 0x0400108D RID: 4237
		public string m_IconName = "";

		// Token: 0x0400108E RID: 4238
		public string m_RangeEffectName = "";

		// Token: 0x0400108F RID: 4239
		public string m_MasterEffectName = "";

		// Token: 0x04001090 RID: 4240
		public string m_MasterEffectNameSkinDic = "";

		// Token: 0x04001091 RID: 4241
		public string m_MasterEffectBoneName = "";

		// Token: 0x04001092 RID: 4242
		public string m_SlaveEffectName = "";

		// Token: 0x04001093 RID: 4243
		public string m_SlaveEffectNameSkinDic = "";

		// Token: 0x04001094 RID: 4244
		public string m_SlaveEffectBoneName = "";

		// Token: 0x04001095 RID: 4245
		public bool m_bIgnoreUnitScaleFactor;

		// Token: 0x04001096 RID: 4246
		public float m_MasterColorR = -1f;

		// Token: 0x04001097 RID: 4247
		public float m_MasterColorG = -1f;

		// Token: 0x04001098 RID: 4248
		public float m_MasterColorB = -1f;

		// Token: 0x04001099 RID: 4249
		public float m_ColorR = -1f;

		// Token: 0x0400109A RID: 4250
		public float m_ColorG = -1f;

		// Token: 0x0400109B RID: 4251
		public float m_ColorB = -1f;

		// Token: 0x0400109C RID: 4252
		public bool m_bNoReuse;

		// Token: 0x0400109D RID: 4253
		public bool m_bAllowBoss = true;

		// Token: 0x0400109E RID: 4254
		public List<int> m_listAllowUnitID = new List<int>();

		// Token: 0x0400109F RID: 4255
		public List<int> m_listIgnoreUnitID = new List<int>();

		// Token: 0x040010A0 RID: 4256
		public List<NKM_UNIT_STYLE_TYPE> m_listAllowStyleType = new List<NKM_UNIT_STYLE_TYPE>();

		// Token: 0x040010A1 RID: 4257
		public List<NKM_UNIT_ROLE_TYPE> m_listAllowRoleType = new List<NKM_UNIT_ROLE_TYPE>();

		// Token: 0x040010A2 RID: 4258
		public List<NKM_UNIT_STYLE_TYPE> m_listIgnoreStyleType = new List<NKM_UNIT_STYLE_TYPE>();

		// Token: 0x040010A3 RID: 4259
		public List<NKM_UNIT_ROLE_TYPE> m_listIgnoreRoleType = new List<NKM_UNIT_ROLE_TYPE>();

		// Token: 0x040010A4 RID: 4260
		public bool m_bAllowAirUnit = true;

		// Token: 0x040010A5 RID: 4261
		public bool m_bAllowLandUnit = true;

		// Token: 0x040010A6 RID: 4262
		public bool m_bAllowAwaken = true;

		// Token: 0x040010A7 RID: 4263
		public bool m_bAllowNormal = true;

		// Token: 0x040010A8 RID: 4264
		public bool m_bRangeSonAllowBoss = true;

		// Token: 0x040010A9 RID: 4265
		public List<int> m_listRangeSonAllowUnitID = new List<int>();

		// Token: 0x040010AA RID: 4266
		public List<int> m_listRangeSonIgnoreUnitID = new List<int>();

		// Token: 0x040010AB RID: 4267
		public List<NKM_UNIT_STYLE_TYPE> m_listRangeSonAllowStyleType = new List<NKM_UNIT_STYLE_TYPE>();

		// Token: 0x040010AC RID: 4268
		public List<NKM_UNIT_ROLE_TYPE> m_listRangeSonAllowRoleType = new List<NKM_UNIT_ROLE_TYPE>();

		// Token: 0x040010AD RID: 4269
		public List<NKM_UNIT_STYLE_TYPE> m_listRangeSonIgnoreStyleType = new List<NKM_UNIT_STYLE_TYPE>();

		// Token: 0x040010AE RID: 4270
		public List<NKM_UNIT_ROLE_TYPE> m_listRangeSonIgnoreRoleType = new List<NKM_UNIT_ROLE_TYPE>();

		// Token: 0x040010AF RID: 4271
		public bool m_bRangeSonAllowAirUnit = true;

		// Token: 0x040010B0 RID: 4272
		public bool m_bRangeSonAllowLandUnit = true;

		// Token: 0x040010B1 RID: 4273
		public bool m_bRangeSonAllowAwaken = true;

		// Token: 0x040010B2 RID: 4274
		public bool m_bRangeSonAllowNormal = true;

		// Token: 0x040010B3 RID: 4275
		public bool m_bRangeSonOnlyTarget;

		// Token: 0x040010B4 RID: 4276
		public bool m_bRangeSonOnlySubTarget;

		// Token: 0x040010B5 RID: 4277
		public bool m_AffectMe = true;

		// Token: 0x040010B6 RID: 4278
		public bool m_AffectMasterTeam;

		// Token: 0x040010B7 RID: 4279
		public bool m_AffectMasterEnemyTeam;

		// Token: 0x040010B8 RID: 4280
		public int m_AffectMultiRespawnMinCount;

		// Token: 0x040010B9 RID: 4281
		public int m_AffectSonMultiRespawnMinCount;

		// Token: 0x040010BA RID: 4282
		public NKMMinMaxInt m_AffectCostRange = new NKMMinMaxInt(-1, -1);

		// Token: 0x040010BB RID: 4283
		public NKMBuffTemplet.AffectSummonType m_eAffectSummonType;

		// Token: 0x040010BC RID: 4284
		public byte m_AddAttackUnitCount;

		// Token: 0x040010BD RID: 4285
		public float m_fAddAttackRange;

		// Token: 0x040010BE RID: 4286
		public NKM_SUPER_ARMOR_LEVEL m_SuperArmorLevel = NKM_SUPER_ARMOR_LEVEL.NSAL_NO;

		// Token: 0x040010BF RID: 4287
		public HashSet<NKM_UNIT_STATUS_EFFECT> m_ApplyStatus = new HashSet<NKM_UNIT_STATUS_EFFECT>();

		// Token: 0x040010C0 RID: 4288
		public HashSet<NKM_UNIT_STATUS_EFFECT> m_ImmuneStatus = new HashSet<NKM_UNIT_STATUS_EFFECT>();

		// Token: 0x040010C1 RID: 4289
		public bool m_bNotDispel;

		// Token: 0x040010C2 RID: 4290
		public bool m_bRangeSonDispelBuff;

		// Token: 0x040010C3 RID: 4291
		public bool m_bRangeSonDispelDebuff;

		// Token: 0x040010C4 RID: 4292
		public bool m_bDispelBuff;

		// Token: 0x040010C5 RID: 4293
		public bool m_bDispelDebuff;

		// Token: 0x040010C6 RID: 4294
		public bool m_bNotCastSummon;

		// Token: 0x040010C7 RID: 4295
		public bool m_bIgnoreBlock;

		// Token: 0x040010C8 RID: 4296
		public float m_fDamageTransfer;

		// Token: 0x040010C9 RID: 4297
		public float m_fDamageReflection;

		// Token: 0x040010CA RID: 4298
		public float m_fHealFeedback;

		// Token: 0x040010CB RID: 4299
		public float m_fHealFeedbackPerLevel;

		// Token: 0x040010CC RID: 4300
		public bool m_bGuard;

		// Token: 0x040010CD RID: 4301
		public bool m_bBarrierHPRate;

		// Token: 0x040010CE RID: 4302
		public float m_fBarrierHP = -1f;

		// Token: 0x040010CF RID: 4303
		public float m_fBarrierHPPerLevel;

		// Token: 0x040010D0 RID: 4304
		public string m_BarrierDamageEffectName = "";

		// Token: 0x040010D1 RID: 4305
		public string m_DamageTempletStrID = "";

		// Token: 0x040010D2 RID: 4306
		public NKMDamageTemplet m_NKMDamageTemplet;

		// Token: 0x040010D3 RID: 4307
		public float m_fOneTimeHPDamageRate;

		// Token: 0x040010D4 RID: 4308
		public string m_StartDTStrID = "";

		// Token: 0x040010D5 RID: 4309
		public NKMDamageTemplet m_DTStart;

		// Token: 0x040010D6 RID: 4310
		public string m_EndDTStrID = "";

		// Token: 0x040010D7 RID: 4311
		public NKMDamageTemplet m_DTEnd;

		// Token: 0x040010D8 RID: 4312
		public string m_DispelDTStrID = "";

		// Token: 0x040010D9 RID: 4313
		public NKMDamageTemplet m_DTDispel;

		// Token: 0x040010DA RID: 4314
		public string m_EventHealStrID = "";

		// Token: 0x040010DB RID: 4315
		public NKMEventHeal m_NKMEventHeal;

		// Token: 0x040010DC RID: 4316
		public bool m_bUnitDieEvent;

		// Token: 0x040010DD RID: 4317
		public int m_UnitLevel;

		// Token: 0x040010DE RID: 4318
		public string m_FinalUnitStateChange = "";

		// Token: 0x040010DF RID: 4319
		public string m_FinalBuffStrID = "";

		// Token: 0x040010E0 RID: 4320
		public NKM_STAT_TYPE m_StatType1 = NKM_STAT_TYPE.NST_END;

		// Token: 0x040010E1 RID: 4321
		public int m_StatValue1;

		// Token: 0x040010E2 RID: 4322
		public int m_StatFactor1;

		// Token: 0x040010E3 RID: 4323
		public int m_StatAddPerLevel1;

		// Token: 0x040010E4 RID: 4324
		public NKM_STAT_TYPE m_StatType2 = NKM_STAT_TYPE.NST_END;

		// Token: 0x040010E5 RID: 4325
		public int m_StatValue2;

		// Token: 0x040010E6 RID: 4326
		public int m_StatFactor2;

		// Token: 0x040010E7 RID: 4327
		public int m_StatAddPerLevel2;

		// Token: 0x040010E8 RID: 4328
		public NKM_STAT_TYPE m_StatType3 = NKM_STAT_TYPE.NST_END;

		// Token: 0x040010E9 RID: 4329
		public int m_StatValue3;

		// Token: 0x040010EA RID: 4330
		public int m_StatFactor3;

		// Token: 0x040010EB RID: 4331
		public int m_StatAddPerLevel3;

		// Token: 0x040010EC RID: 4332
		private Dictionary<int, string> m_dicMasterEffect;

		// Token: 0x040010ED RID: 4333
		private Dictionary<int, string> m_dicSlaveEffect;

		// Token: 0x020011AC RID: 4524
		public enum BuffEndDTType
		{
			// Token: 0x040092DE RID: 37598
			NoUse,
			// Token: 0x040092DF RID: 37599
			End,
			// Token: 0x040092E0 RID: 37600
			Dispel
		}

		// Token: 0x020011AD RID: 4525
		public enum AffectSummonType : byte
		{
			// Token: 0x040092E2 RID: 37602
			All,
			// Token: 0x040092E3 RID: 37603
			SummonOnly,
			// Token: 0x040092E4 RID: 37604
			SummonNo
		}
	}
}
