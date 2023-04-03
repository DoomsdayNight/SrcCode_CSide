using System;
using System.Collections.Generic;
using Cs.Logging;
using Cs.Math;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x020004BD RID: 1213
	public class NKMUnitStatManager
	{
		// Token: 0x06002202 RID: 8706 RVA: 0x000ADF74 File Offset: 0x000AC174
		public static void LoadFromLua()
		{
			NKMTempletContainer<NKMGameStatRateTemplet>.Load("AB_SCRIPT", "LUA_GAME_STAT_RATE", "m_GameStatRate", new Func<NKMLua, NKMGameStatRateTemplet>(NKMGameStatRateTemplet.LoadFromLua), (NKMGameStatRateTemplet e) => e.m_strID);
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x000ADFC0 File Offset: 0x000AC1C0
		public static NKMStatData MakeFinalStat(NKMUnitData unitData, NKMInventoryData inventoryData, NKMOperator cNKMOperator)
		{
			NKMStatData nkmstatData = new NKMStatData();
			nkmstatData.Init();
			NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(unitData.m_UnitID);
			if (unitStatTemplet != null)
			{
				nkmstatData.MakeBaseStat(null, false, unitData, unitStatTemplet.m_StatData, false, 0, cNKMOperator);
			}
			nkmstatData.MakeBaseBonusFactor(unitData, (inventoryData != null) ? inventoryData.EquipItems : null, null, null, false);
			nkmstatData.UpdateFinalStat(null, null, null, false);
			return nkmstatData;
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x000AE01C File Offset: 0x000AC21C
		public static NKMStatData MakeFinalStat(NKMGame cNKMGame, NKMUnitData unitData, NKM_TEAM_TYPE teamType, NKMUnit cNKMUnit, NKMOperator cNKMOperator)
		{
			NKMStatData nkmstatData = new NKMStatData();
			nkmstatData.Init();
			NKMGameData nkmgameData = (cNKMGame != null) ? cNKMGame.GetGameData() : null;
			NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(unitData.m_UnitID);
			if (unitStatTemplet != null)
			{
				nkmstatData.MakeBaseStat(nkmgameData, nkmgameData.IsPVP(), unitData, unitStatTemplet.m_StatData, false, 0, cNKMOperator);
			}
			Dictionary<long, NKMEquipItemData> dictionary = teamType.IsAteam() ? nkmgameData.m_NKMGameTeamDataA.m_ItemEquipData : nkmgameData.m_NKMGameTeamDataB.m_ItemEquipData;
			NKMGameTeamData nkmgameTeamData = teamType.IsAteam() ? nkmgameData.m_NKMGameTeamDataA : nkmgameData.m_NKMGameTeamDataB;
			NKMStatData nkmstatData2 = nkmstatData;
			IReadOnlyDictionary<long, NKMEquipItemData> dicEquipItemData = dictionary;
			List<NKMShipCmdModule> shipCommandModule;
			if (nkmgameTeamData == null)
			{
				shipCommandModule = null;
			}
			else
			{
				NKMUnitData mainShip = nkmgameTeamData.m_MainShip;
				shipCommandModule = ((mainShip != null) ? mainShip.ShipCommandModule : null);
			}
			nkmstatData2.MakeBaseBonusFactor(unitData, dicEquipItemData, shipCommandModule, (nkmgameData != null) ? nkmgameData.GameStatRate : null, nkmgameData.IsPVP());
			nkmstatData.UpdateFinalStat(cNKMGame, nkmgameData.GameStatRate, cNKMUnit, false);
			return nkmstatData;
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x000AE0E8 File Offset: 0x000AC2E8
		public static float CalculateStat(NKM_STAT_TYPE eNKM_STAT_TYPE, NKMUnitData unitData, NKMGameStatRate cGameStatRate = null)
		{
			if (unitData == null)
			{
				return 0f;
			}
			NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(unitData.m_UnitID);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID);
			return NKMUnitStatManager.CalculateStat(eNKM_STAT_TYPE, unitStatTemplet.m_StatData, unitData.m_UnitLevel, (int)unitData.m_LimitBreakLevel, unitData.GetMultiplierByPermanentContract(), cGameStatRate, null, 0, unitTempletBase.m_NKM_UNIT_TYPE);
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x000AE140 File Offset: 0x000AC340
		public static float CalculateStat(NKM_STAT_TYPE eNKM_STAT_TYPE, NKMStatData unitBaseStatData, List<int> lstStatExp, int unitLevel, int limitBreakLevel, float permanentContractMultiplier, NKMGameStatRate cGameStatRate, NKMOperator cNKMOperator, int operatorBanLevel, NKM_UNIT_TYPE eNKM_UNIT_TYPE)
		{
			return NKMUnitStatManager.CalculateStat(eNKM_STAT_TYPE, unitBaseStatData, unitLevel, limitBreakLevel, permanentContractMultiplier, cGameStatRate, cNKMOperator, operatorBanLevel, eNKM_UNIT_TYPE);
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x000AE164 File Offset: 0x000AC364
		public static float CalculateStat(NKM_STAT_TYPE eNKM_STAT_TYPE, NKMStatData unitBaseStatData, int unitLevel, int limitBreakLevel, float permanentContractMultiplier, NKMGameStatRate cGameStatRate, NKMOperator cNKMOperator, int operatorBanLevel, NKM_UNIT_TYPE eNKM_UNIT_TYPE)
		{
			float num;
			if (eNKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
			{
				num = NKMUnitLimitBreakManager.GetLimitBreakStatMultiplierForShip(limitBreakLevel);
			}
			else
			{
				num = NKMUnitLimitBreakManager.GetLimitBreakStatMultiplier(limitBreakLevel);
			}
			float num2 = (cGameStatRate != null) ? cGameStatRate.GetStatValueRate(eNKM_STAT_TYPE) : 1f;
			if (eNKM_STAT_TYPE <= NKM_STAT_TYPE.NST_EVADE)
			{
				float num3 = unitBaseStatData.GetStatBase(eNKM_STAT_TYPE);
				num3 += unitBaseStatData.GetStatPerLevel(eNKM_STAT_TYPE) * (float)(unitLevel - 1) * num2;
				num3 = NKMUnitStatManager.GetFinalStatValueByOperator(num3, eNKM_STAT_TYPE, cNKMOperator, operatorBanLevel);
				num3 *= num;
				if (permanentContractMultiplier > 0f)
				{
					num3 += num3 * permanentContractMultiplier;
				}
				float maxStat = NKMUnitStatManager.GetMaxStat(eNKM_STAT_TYPE, unitBaseStatData, unitLevel, limitBreakLevel, permanentContractMultiplier, cGameStatRate, cNKMOperator, eNKM_UNIT_TYPE);
				if (num3 > maxStat)
				{
					num3 = maxStat;
				}
				return num3;
			}
			Log.Error("잘못된 스탯 계산 시도", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitStatManager.cs", 1640);
			return 0f;
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x000AE214 File Offset: 0x000AC414
		public static float GetMaxStat(NKM_STAT_TYPE eNKM_STAT_TYPE, NKMUnitData unitData, NKMGameStatRate cGameStatRate = null, NKMOperator cNKMOperator = null)
		{
			if (unitData == null)
			{
				return 0f;
			}
			NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(unitData.m_UnitID);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(cNKMOperator.id);
			return NKMUnitStatManager.GetMaxStat(eNKM_STAT_TYPE, unitStatTemplet.m_StatData, unitData.m_UnitLevel, (int)unitData.m_LimitBreakLevel, unitData.GetMultiplierByPermanentContract(), cGameStatRate, cNKMOperator, unitTempletBase.m_NKM_UNIT_TYPE);
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x000AE268 File Offset: 0x000AC468
		private static float GetFinalStatValueByOperator(float inputValue, NKM_STAT_TYPE eNKM_STAT_TYPE, NKMOperator cNKMOperator, int operatorBanLevel)
		{
			float num = inputValue;
			if (cNKMOperator != null)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(cNKMOperator.id);
				if (unitTempletBase != null && unitTempletBase.StatTemplet != null)
				{
					float num2 = unitTempletBase.StatTemplet.m_StatData.GetStatBase(eNKM_STAT_TYPE);
					num2 += unitTempletBase.StatTemplet.m_StatData.GetStatPerLevel(eNKM_STAT_TYPE) * (float)(cNKMOperator.level - 1);
					if (operatorBanLevel != 0 && (eNKM_STAT_TYPE == NKM_STAT_TYPE.NST_HP || eNKM_STAT_TYPE == NKM_STAT_TYPE.NST_ATK || eNKM_STAT_TYPE == NKM_STAT_TYPE.NST_DEF))
					{
						float num3 = num2 * NKMUnitStatManager.m_fPercentPerBanLevel * (float)operatorBanLevel;
						if (num3 < num2 * NKMUnitStatManager.m_fMaxPercentPerBanLevel)
						{
							num3 = num2 * NKMUnitStatManager.m_fMaxPercentPerBanLevel;
						}
						num2 -= num3;
					}
					num += num * (num2 / 10000f);
				}
			}
			return num;
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x000AE300 File Offset: 0x000AC500
		public static float GetMaxStat(NKM_STAT_TYPE eNKM_STAT_TYPE, NKMStatData unitBaseStatData, int unitLevel, int limitBreakLevel, float permanentContractMultiplier, NKMGameStatRate cGameStatRate = null, NKMOperator cNKMOperator = null, NKM_UNIT_TYPE eNKM_UNIT_TYPE = NKM_UNIT_TYPE.NUT_NORMAL)
		{
			float num;
			if (eNKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
			{
				num = NKMUnitLimitBreakManager.GetLimitBreakStatMultiplierForShip(limitBreakLevel);
			}
			else
			{
				num = NKMUnitLimitBreakManager.GetLimitBreakStatMultiplier(limitBreakLevel);
			}
			float num2 = (cGameStatRate != null) ? cGameStatRate.GetStatValueRate(eNKM_STAT_TYPE) : 1f;
			if (eNKM_STAT_TYPE <= NKM_STAT_TYPE.NST_EVADE)
			{
				float num3 = unitBaseStatData.GetStatBase(eNKM_STAT_TYPE);
				num3 += unitBaseStatData.GetStatPerLevel(eNKM_STAT_TYPE) * (float)(unitLevel - 1) * num2;
				num3 = NKMUnitStatManager.GetFinalStatValueByOperator(num3, eNKM_STAT_TYPE, cNKMOperator, 0);
				num3 *= num;
				num3 += unitBaseStatData.GetStatMaxPerLevel(eNKM_STAT_TYPE) * (float)(unitLevel - 1) * num2;
				if (permanentContractMultiplier > 0f)
				{
					float num4 = num3 * permanentContractMultiplier;
					num3 += num4;
				}
				return num3;
			}
			Log.Error("잘못된 스탯 계산 시도", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitStatManager.cs", 1747);
			return 0f;
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x000AE3A6 File Offset: 0x000AC5A6
		public static bool IsMainStat(NKM_STAT_TYPE eStat)
		{
			return eStat <= NKM_STAT_TYPE.NST_EVADE;
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x000AE3AF File Offset: 0x000AC5AF
		public static bool IsPercentStat(NKM_STAT_TYPE eNKM_STAT_TYPE)
		{
			return eNKM_STAT_TYPE > NKM_STAT_TYPE.NST_EVADE;
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x000AE3B8 File Offset: 0x000AC5B8
		private static float GetDamageAdjustFactor(NKMUnit unitAtk, NKMUnit unitDef, NKMStatData statAtk, NKMStatData statDef, NKMUnitTempletBase atkTempletBase, NKMUnitTempletBase defTempletBase, bool bLongRange)
		{
			if (statAtk == null || statDef == null)
			{
				return 1f;
			}
			if (atkTempletBase == null || defTempletBase == null)
			{
				return 1f;
			}
			bool bDefAir = defTempletBase.m_bAirUnit;
			if (unitDef != null)
			{
				bDefAir = unitDef.IsAirUnit();
			}
			float num = bLongRange ? statAtk.GetStatFinal(NKM_STAT_TYPE.NST_LONG_RANGE_DAMAGE_RATE) : statAtk.GetStatFinal(NKM_STAT_TYPE.NST_SHORT_RANGE_DAMAGE_RATE);
			float num2 = NKMUnitStatManager.GetAttackerBonusFromDefUnitStyle(statAtk, defTempletBase.m_NKM_UNIT_STYLE_TYPE) + NKMUnitStatManager.GetAttackerBonusFromDefUnitStyle(statAtk, defTempletBase.m_NKM_UNIT_STYLE_TYPE_SUB) + NKMUnitStatManager.GetAttackerBonusFromDefUnitRoleStat(statAtk, defTempletBase.m_NKM_UNIT_ROLE_TYPE) + NKMUnitStatManager.GetAttackerBonusFromDefUnitAir(statAtk, bDefAir) + num;
			num2 -= num2 * (statDef.GetStatFinal(NKM_STAT_TYPE.NST_DAMAGE_INCREASE_DEFENCE) + statAtk.GetStatFinal(NKM_STAT_TYPE.NST_DAMAGE_INCREASE_REDUCE));
			float num3 = 0f;
			if (unitAtk != null && unitDef != null && statDef.GetStatFinal(NKM_STAT_TYPE.NST_DAMAGE_RESIST) > 0f)
			{
				num3 = statDef.GetStatFinal(NKM_STAT_TYPE.NST_DAMAGE_RESIST) * (float)unitDef.GetDamageResistCount(unitAtk.GetUnitSyncData().m_GameUnitUID);
				if (num3 > 0.8f)
				{
					num3 = 0.8f;
				}
				unitDef.AddDamageResistCount(unitAtk.GetUnitSyncData().m_GameUnitUID);
			}
			bool bAtkAir = atkTempletBase.m_bAirUnit;
			if (unitAtk != null)
			{
				bAtkAir = unitAtk.IsAirUnit();
			}
			float num4 = bLongRange ? statDef.GetStatFinal(NKM_STAT_TYPE.NST_LONG_RANGE_DAMAGE_REDUCE_RATE) : statDef.GetStatFinal(NKM_STAT_TYPE.NST_SHORT_RANGE_DAMAGE_REDUCE_RATE);
			float num5 = NKMUnitStatManager.GetAttackReduceRateFromDefUnitStyle(statDef, atkTempletBase.m_NKM_UNIT_STYLE_TYPE) + NKMUnitStatManager.GetAttackReduceRateFromDefUnitStyle(statDef, atkTempletBase.m_NKM_UNIT_STYLE_TYPE_SUB) + NKMUnitStatManager.GetAttackReduceRateFromAtkUnitRoleStat(statDef, atkTempletBase.m_NKM_UNIT_ROLE_TYPE) + NKMUnitStatManager.GetAttackReduceFromAtkUnitAir(statDef, bAtkAir) + num4 + num3;
			num5 -= num5 * (statAtk.GetStatFinal(NKM_STAT_TYPE.NST_DAMAGE_REDUCE_PENETRATE) + statDef.GetStatFinal(NKM_STAT_TYPE.NST_DAMAGE_REDUCE_REDUCE));
			float num6 = 1f + num2 - num5;
			if (num6 < 0f)
			{
				num6 = 0f;
			}
			return num6;
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x000AE544 File Offset: 0x000AC744
		private static float Get2ndDamageAdjustFactor(NKMStatData statAtk, NKMStatData statDef, NKMUnitSkillTemplet AttackerSkillTemplet, bool bSplashHit)
		{
			if (statAtk == null || statDef == null)
			{
				return 1f;
			}
			float num = 1f;
			if (AttackerSkillTemplet != null)
			{
				if (AttackerSkillTemplet.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_SKILL)
				{
					num += statAtk.GetStatFinal(NKM_STAT_TYPE.NST_SKILL_DAMAGE_RATE);
					num -= statDef.GetStatFinal(NKM_STAT_TYPE.NST_SKILL_DAMAGE_REDUCE_RATE);
				}
				else if (AttackerSkillTemplet.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_HYPER)
				{
					num += statAtk.GetStatFinal(NKM_STAT_TYPE.NST_HYPER_SKILL_DAMAGE_RATE);
					num -= statDef.GetStatFinal(NKM_STAT_TYPE.NST_HYPER_SKILL_DAMAGE_REDUCE_RATE);
				}
			}
			if (bSplashHit)
			{
				num -= statDef.GetStatFinal(NKM_STAT_TYPE.NST_SPLASH_DAMAGE_REDUCE_RATE);
			}
			num -= statDef.GetStatFinal(NKM_STAT_TYPE.NST_DAMAGE_REDUCE_RATE);
			num += statAtk.GetStatFinal(NKM_STAT_TYPE.NST_ATTACK_DAMAGE_MODIFY_G2);
			if (num < 0.5f)
			{
				num = 0.5f;
			}
			return num;
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x000AE5DC File Offset: 0x000AC7DC
		private static float GetRoleDamageAdjustFactor(NKMStatData statAtk, NKMStatData statDef, NKMUnitTempletBase atkTempletBase, NKMUnitTempletBase defTempletBase, ref NKM_DAMAGE_RESULT_TYPE eNKM_DAMAGE_RESULT_TYPE)
		{
			if (statAtk == null || statDef == null)
			{
				return 1f;
			}
			if (atkTempletBase == null || defTempletBase == null)
			{
				return 1f;
			}
			float num = NKMUnitStatManager.GetAttackerBonusFromDefUnitRole(atkTempletBase.m_NKM_UNIT_ROLE_TYPE, defTempletBase.m_NKM_UNIT_ROLE_TYPE);
			float num2 = statAtk.GetStatFinal(NKM_STAT_TYPE.NST_ROLE_TYPE_DAMAGE_RATE) - statDef.GetStatFinal(NKM_STAT_TYPE.NST_ROLE_TYPE_DAMAGE_REDUCE_RATE);
			if (num2 < 0f)
			{
				num2 = 0f;
			}
			num *= 1f + num2;
			float attackReduceRateFromAtkUnitRole = NKMUnitStatManager.GetAttackReduceRateFromAtkUnitRole(atkTempletBase.m_NKM_UNIT_ROLE_TYPE, defTempletBase.m_NKM_UNIT_ROLE_TYPE);
			float num3 = 1f + num - attackReduceRateFromAtkUnitRole;
			if (num3 > 1f)
			{
				eNKM_DAMAGE_RESULT_TYPE = NKM_DAMAGE_RESULT_TYPE.NDRT_WEAK;
			}
			return num3;
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x000AE664 File Offset: 0x000AC864
		private static float GetExtraDamageAdjustFactor(NKMUnit unitAtk, NKMUnit unitDef, NKMStatData statAtk, NKMStatData statDef, bool bCritical)
		{
			float num = 1f;
			if (unitDef != null && unitDef.HasBarrierBuff() && statAtk != null && statAtk.GetStatFinal(NKM_STAT_TYPE.NST_DAMAGE_REDUCE_RATE_AGAINST_BARRIER) > 0f)
			{
				float num2 = statAtk.GetStatFinal(NKM_STAT_TYPE.NST_DAMAGE_REDUCE_RATE_AGAINST_BARRIER);
				num2 = num2.Clamp(0f, 0.99f);
				num -= num2;
			}
			if (!bCritical && statDef != null)
			{
				float num3 = statDef.GetStatFinal(NKM_STAT_TYPE.NST_NON_CRITICAL_DAMAGE_TAKE_RATE);
				if (num3 < -0.99f)
				{
					num3 = -0.99f;
				}
				num += num3;
			}
			if (statAtk != null)
			{
				num += statAtk.GetStatFinal(NKM_STAT_TYPE.NST_EXTRA_ADJUST_DAMAGE_DEALT);
			}
			if (statDef != null)
			{
				num += statDef.GetStatFinal(NKM_STAT_TYPE.NST_EXTRA_ADJUST_DAMAGE_RECEIVE);
			}
			if (num < 0.01f)
			{
				num = 0.01f;
			}
			return num;
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x000AE700 File Offset: 0x000AC900
		private static float GetAttackerBonusFromDefUnitStyle(NKMStatData statAtk, NKM_UNIT_STYLE_TYPE defStyle)
		{
			switch (defStyle)
			{
			case NKM_UNIT_STYLE_TYPE.NUST_COUNTER:
				return statAtk.GetStatFinal(NKM_STAT_TYPE.NST_UNIT_TYPE_COUNTER_DAMAGE_RATE);
			case NKM_UNIT_STYLE_TYPE.NUST_SOLDIER:
				return statAtk.GetStatFinal(NKM_STAT_TYPE.NST_UNIT_TYPE_SOLDIER_DAMAGE_RATE);
			case NKM_UNIT_STYLE_TYPE.NUST_MECHANIC:
				return statAtk.GetStatFinal(NKM_STAT_TYPE.NST_UNIT_TYPE_MECHANIC_DAMAGE_RATE);
			case NKM_UNIT_STYLE_TYPE.NUST_CORRUPTED:
				return statAtk.GetStatFinal(NKM_STAT_TYPE.NST_UNIT_TYPE_CORRUPTED_DAMAGE_RATE);
			case NKM_UNIT_STYLE_TYPE.NUST_REPLACER:
				return statAtk.GetStatFinal(NKM_STAT_TYPE.NST_UNIT_TYPE_REPLACER_DAMAGE_RATE);
			default:
				return 0f;
			}
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x000AE760 File Offset: 0x000AC960
		private static float GetAttackReduceRateFromDefUnitStyle(NKMStatData statDef, NKM_UNIT_STYLE_TYPE atkRole)
		{
			switch (atkRole)
			{
			case NKM_UNIT_STYLE_TYPE.NUST_COUNTER:
				return statDef.GetStatFinal(NKM_STAT_TYPE.NST_UNIT_TYPE_COUNTER_DAMAGE_REDUCE_RATE);
			case NKM_UNIT_STYLE_TYPE.NUST_SOLDIER:
				return statDef.GetStatFinal(NKM_STAT_TYPE.NST_UNIT_TYPE_SOLDIER_DAMAGE_REDUCE_RATE);
			case NKM_UNIT_STYLE_TYPE.NUST_MECHANIC:
				return statDef.GetStatFinal(NKM_STAT_TYPE.NST_UNIT_TYPE_MECHANIC_DAMAGE_REDUCE_RATE);
			case NKM_UNIT_STYLE_TYPE.NUST_CORRUPTED:
				return statDef.GetStatFinal(NKM_STAT_TYPE.NST_UNIT_TYPE_CORRUPTED_DAMAGE_REDUCE_RATE);
			case NKM_UNIT_STYLE_TYPE.NUST_REPLACER:
				return statDef.GetStatFinal(NKM_STAT_TYPE.NST_UNIT_TYPE_REPLACER_DAMAGE_REDUCE_RATE);
			default:
				return 0f;
			}
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x000AE7C0 File Offset: 0x000AC9C0
		private static float GetAttackerBonusFromDefUnitRole(NKM_UNIT_ROLE_TYPE atkRole, NKM_UNIT_ROLE_TYPE defRole)
		{
			switch (atkRole)
			{
			case NKM_UNIT_ROLE_TYPE.NURT_STRIKER:
				if (defRole != NKM_UNIT_ROLE_TYPE.NURT_RANGER)
				{
					return 0f;
				}
				return NKMUnitStatManager.ROLE_TYPE_BONUS_FACTOR;
			case NKM_UNIT_ROLE_TYPE.NURT_RANGER:
				if (defRole != NKM_UNIT_ROLE_TYPE.NURT_DEFENDER)
				{
					return 0f;
				}
				return NKMUnitStatManager.ROLE_TYPE_BONUS_FACTOR;
			case NKM_UNIT_ROLE_TYPE.NURT_DEFENDER:
				if (defRole != NKM_UNIT_ROLE_TYPE.NURT_SNIPER)
				{
					return 0f;
				}
				return NKMUnitStatManager.ROLE_TYPE_BONUS_FACTOR;
			case NKM_UNIT_ROLE_TYPE.NURT_SNIPER:
				if (defRole != NKM_UNIT_ROLE_TYPE.NURT_STRIKER)
				{
					return 0f;
				}
				return NKMUnitStatManager.ROLE_TYPE_BONUS_FACTOR;
			case NKM_UNIT_ROLE_TYPE.NURT_SUPPORTER:
			case NKM_UNIT_ROLE_TYPE.NURT_SIEGE:
			case NKM_UNIT_ROLE_TYPE.NURT_TOWER:
				return 0f;
			default:
				return 0f;
			}
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x000AE840 File Offset: 0x000ACA40
		private static float GetAttackReduceRateFromAtkUnitRole(NKM_UNIT_ROLE_TYPE atkRole, NKM_UNIT_ROLE_TYPE defRole)
		{
			switch (defRole)
			{
			case NKM_UNIT_ROLE_TYPE.NURT_STRIKER:
				if (atkRole != NKM_UNIT_ROLE_TYPE.NURT_RANGER)
				{
					return 0f;
				}
				return NKMUnitStatManager.ROLE_TYPE_REDUCE_FACTOR;
			case NKM_UNIT_ROLE_TYPE.NURT_RANGER:
				if (atkRole != NKM_UNIT_ROLE_TYPE.NURT_DEFENDER)
				{
					return 0f;
				}
				return NKMUnitStatManager.ROLE_TYPE_REDUCE_FACTOR;
			case NKM_UNIT_ROLE_TYPE.NURT_DEFENDER:
				if (atkRole != NKM_UNIT_ROLE_TYPE.NURT_SNIPER)
				{
					return 0f;
				}
				return NKMUnitStatManager.ROLE_TYPE_REDUCE_FACTOR;
			case NKM_UNIT_ROLE_TYPE.NURT_SNIPER:
				if (atkRole != NKM_UNIT_ROLE_TYPE.NURT_STRIKER)
				{
					return 0f;
				}
				return NKMUnitStatManager.ROLE_TYPE_REDUCE_FACTOR;
			case NKM_UNIT_ROLE_TYPE.NURT_SUPPORTER:
			case NKM_UNIT_ROLE_TYPE.NURT_SIEGE:
			case NKM_UNIT_ROLE_TYPE.NURT_TOWER:
				return 0f;
			default:
				return 0f;
			}
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x000AE8C0 File Offset: 0x000ACAC0
		private static float GetAttackerBonusFromDefUnitRoleStat(NKMStatData statAtk, NKM_UNIT_ROLE_TYPE defRole)
		{
			switch (defRole)
			{
			default:
				return 0f;
			case NKM_UNIT_ROLE_TYPE.NURT_STRIKER:
				return statAtk.GetStatFinal(NKM_STAT_TYPE.NST_ROLE_TYPE_STRIKER_DAMAGE_RATE);
			case NKM_UNIT_ROLE_TYPE.NURT_RANGER:
				return statAtk.GetStatFinal(NKM_STAT_TYPE.NST_ROLE_TYPE_RANGER_DAMAGE_RATE);
			case NKM_UNIT_ROLE_TYPE.NURT_DEFENDER:
				return statAtk.GetStatFinal(NKM_STAT_TYPE.NST_ROLE_TYPE_DEFFENDER_DAMAGE_RATE);
			case NKM_UNIT_ROLE_TYPE.NURT_SNIPER:
				return statAtk.GetStatFinal(NKM_STAT_TYPE.NST_ROLE_TYPE_SNIPER_DAMAGE_RATE);
			case NKM_UNIT_ROLE_TYPE.NURT_SUPPORTER:
				return statAtk.GetStatFinal(NKM_STAT_TYPE.NST_ROLE_TYPE_SUPPOERTER_DAMAGE_RATE);
			case NKM_UNIT_ROLE_TYPE.NURT_SIEGE:
				return statAtk.GetStatFinal(NKM_STAT_TYPE.NST_ROLE_TYPE_SIEGE_DAMAGE_RATE);
			case NKM_UNIT_ROLE_TYPE.NURT_TOWER:
				return statAtk.GetStatFinal(NKM_STAT_TYPE.NST_ROLE_TYPE_TOWER_DAMAGE_RATE);
			}
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x000AE938 File Offset: 0x000ACB38
		private static float GetAttackReduceRateFromAtkUnitRoleStat(NKMStatData statDef, NKM_UNIT_ROLE_TYPE atkRole)
		{
			switch (atkRole)
			{
			default:
				return 0f;
			case NKM_UNIT_ROLE_TYPE.NURT_STRIKER:
				return statDef.GetStatFinal(NKM_STAT_TYPE.NST_ROLE_TYPE_STRIKER_DAMAGE_REDUCE_RATE);
			case NKM_UNIT_ROLE_TYPE.NURT_RANGER:
				return statDef.GetStatFinal(NKM_STAT_TYPE.NST_ROLE_TYPE_RANGER_DAMAGE_REDUCE_RATE);
			case NKM_UNIT_ROLE_TYPE.NURT_DEFENDER:
				return statDef.GetStatFinal(NKM_STAT_TYPE.NST_ROLE_TYPE_DEFFENDER_DAMAGE_REDUCE_RATE);
			case NKM_UNIT_ROLE_TYPE.NURT_SNIPER:
				return statDef.GetStatFinal(NKM_STAT_TYPE.NST_ROLE_TYPE_SNIPER_DAMAGE_REDUCE_RATE);
			case NKM_UNIT_ROLE_TYPE.NURT_SUPPORTER:
				return statDef.GetStatFinal(NKM_STAT_TYPE.NST_ROLE_TYPE_SUPPOERTER_DAMAGE_REDUCE_RATE);
			case NKM_UNIT_ROLE_TYPE.NURT_SIEGE:
				return statDef.GetStatFinal(NKM_STAT_TYPE.NST_ROLE_TYPE_SIEGE_DAMAGE_REDUCE_RATE);
			case NKM_UNIT_ROLE_TYPE.NURT_TOWER:
				return statDef.GetStatFinal(NKM_STAT_TYPE.NST_ROLE_TYPE_TOWER_DAMAGE_REDUCE_RATE);
			}
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x000AE9AF File Offset: 0x000ACBAF
		private static float GetAttackerBonusFromDefUnitAir(NKMStatData statAtk, bool bDefAir)
		{
			if (bDefAir)
			{
				return statAtk.GetStatFinal(NKM_STAT_TYPE.NST_MOVE_TYPE_AIR_DAMAGE_RATE);
			}
			return statAtk.GetStatFinal(NKM_STAT_TYPE.NST_MOVE_TYPE_LAND_DAMAGE_RATE);
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x000AE9C5 File Offset: 0x000ACBC5
		private static float GetAttackReduceFromAtkUnitAir(NKMStatData statDef, bool bAtkAir)
		{
			if (bAtkAir)
			{
				return statDef.GetStatFinal(NKM_STAT_TYPE.NST_MOVE_TYPE_AIR_DAMAGE_REDUCE_RATE);
			}
			return statDef.GetStatFinal(NKM_STAT_TYPE.NST_MOVE_TYPE_LAND_DAMAGE_REDUCE_RATE);
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x000AE9DC File Offset: 0x000ACBDC
		public static bool GetEvade(NKMUnit atkUnit, NKMUnit defUnit, bool bBuffDamage, float fDefHPRate, NKMEventAttack cNKMEventAttack = null)
		{
			if (bBuffDamage)
			{
				return false;
			}
			bool flag = cNKMEventAttack.m_bCleanHit || atkUnit.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_FORCE_HIT) || defUnit.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_NO_EVADE);
			bool flag2 = atkUnit.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_FORCE_MISS) || defUnit.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_FORCE_EVADE);
			if (flag2 && !flag)
			{
				return true;
			}
			if (!flag2 && flag)
			{
				return false;
			}
			NKMStatData statData = atkUnit.GetUnitFrameData().m_StatData;
			NKMStatData statData2 = defUnit.GetUnitFrameData().m_StatData;
			float statFinal = statData2.GetStatFinal(NKM_STAT_TYPE.NST_EVADE);
			float num = statData2.GetStatFinal(NKM_STAT_TYPE.NST_HP_GROWN_EVADE_RATE);
			float num2 = Math.Max(-1f, num * (1f - fDefHPRate));
			num = statFinal + statFinal * num2;
			float num3 = num / (num + NKMUnitStatManager.m_fConstEvade);
			float num4 = statData.GetStatFinal(NKM_STAT_TYPE.NST_HIT) / (statData.GetStatFinal(NKM_STAT_TYPE.NST_HIT) + NKMUnitStatManager.m_fConstHit);
			if (num3 < num4)
			{
				return false;
			}
			int num5 = (int)(num3 * 10000f);
			return NKMRandom.Range(0, 10000) <= num5;
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x000AEAC4 File Offset: 0x000ACCC4
		public static float GetAttackFactorDamage(NKMDamageTempletBase damageTempletBase, NKMUnitSkillTemplet m_AttackerUnitSkillTemplet, bool bBoss)
		{
			float num = (m_AttackerUnitSkillTemplet != null) ? m_AttackerUnitSkillTemplet.m_fEmpowerFactor : 1f;
			float fAtkFactor = damageTempletBase.m_fAtkFactor;
			float num2 = num * fAtkFactor * 10000f;
			if (!bBoss)
			{
				float num3 = 100000f * damageTempletBase.m_fAtkMaxHPRateFactor;
				float num4 = 100000f * damageTempletBase.m_fAtkHPRateFactor;
				num2 = num2 + num3 + num4;
			}
			return num2;
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x000AEB18 File Offset: 0x000ACD18
		public static float GetFinalDamage(bool bPVP, NKMStatData statAtk, NKMStatData statDef, NKMUnitData unitdataAtk, NKMUnit unitAtk, NKMUnit unitDef, NKMDamageTemplet damageTemplet, NKMUnitSkillTemplet AttackerSkillTemplet, bool bAttackCountOver, bool bBuffDamage, bool bEvade, out NKM_DAMAGE_RESULT_TYPE eNKM_DAMAGE_RESULT_TYPE, float fDefenderDamageReduce, bool bLongRange, bool bBoss, float fAtkHPRate, bool bSplashHit, NKMDamageAttribute damageAttribute)
		{
			return NKMUnitStatManager.GetFinalDamage(bPVP, statAtk, statDef, unitdataAtk, unitAtk, unitDef, damageTemplet, AttackerSkillTemplet, bAttackCountOver, bBuffDamage, bEvade, out eNKM_DAMAGE_RESULT_TYPE, fDefenderDamageReduce, bLongRange, bBoss, fAtkHPRate, damageAttribute != null && damageAttribute.m_bTrueDamage, bSplashHit, damageAttribute != null && damageAttribute.m_bForceCritical, damageAttribute != null && damageAttribute.m_bNoCritical);
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x000AEB74 File Offset: 0x000ACD74
		public static float GetFinalDamage(bool bPVP, NKMStatData statAtk, NKMStatData statDef, NKMUnitData unitdataAtk, NKMUnit unitAtk, NKMUnit unitDef, NKMDamageTemplet damageTemplet, NKMUnitSkillTemplet AttackerSkillTemplet, bool bAttackCountOver, bool bBuffDamage, bool bEvade, out NKM_DAMAGE_RESULT_TYPE eNKM_DAMAGE_RESULT_TYPE, float fDefenderDamageReduce, bool bLongRange, bool bBoss, float fAtkHPRate, bool bTrueDamage, bool bSplashHit, bool bForceCritical, bool bNoCritical)
		{
			if (bPVP)
			{
				if (damageTemplet.m_DamageTempletBase.m_fAtkFactor.IsNearlyZero(1E-05f) && damageTemplet.m_DamageTempletBase.m_fAtkMaxHPRateFactor.IsNearlyZero(1E-05f) && damageTemplet.m_DamageTempletBase.m_fAtkHPRateFactor.IsNearlyZero(1E-05f) && damageTemplet.m_DamageTempletBase.m_fAtkFactorPVP.IsNearlyZero(1E-05f) && damageTemplet.m_DamageTempletBase.m_fAtkMaxHPRateFactorPVP.IsNearlyZero(1E-05f) && damageTemplet.m_DamageTempletBase.m_fAtkHPRateFactorPVP.IsNearlyZero(1E-05f) && damageTemplet.m_fInstantKillHPRate.IsNearlyZero(1E-05f))
				{
					eNKM_DAMAGE_RESULT_TYPE = NKM_DAMAGE_RESULT_TYPE.NDRT_NO_MARK;
					return 0f;
				}
			}
			else if (damageTemplet.m_DamageTempletBase.m_fAtkFactor.IsNearlyZero(1E-05f) && damageTemplet.m_DamageTempletBase.m_fAtkMaxHPRateFactor.IsNearlyZero(1E-05f) && damageTemplet.m_DamageTempletBase.m_fAtkHPRateFactor.IsNearlyZero(1E-05f) && damageTemplet.m_fInstantKillHPRate.IsNearlyZero(1E-05f))
			{
				eNKM_DAMAGE_RESULT_TYPE = NKM_DAMAGE_RESULT_TYPE.NDRT_NO_MARK;
				return 0f;
			}
			if (unitDef.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_IRONWALL))
			{
				if (statAtk == null)
				{
					bEvade = false;
				}
				if (bEvade)
				{
					eNKM_DAMAGE_RESULT_TYPE = NKM_DAMAGE_RESULT_TYPE.NDRT_MISS;
					return 0f;
				}
				eNKM_DAMAGE_RESULT_TYPE = NKM_DAMAGE_RESULT_TYPE.NDRT_NORMAL;
				return 1f;
			}
			else
			{
				eNKM_DAMAGE_RESULT_TYPE = NKM_DAMAGE_RESULT_TYPE.NDRT_NORMAL;
				float num = (AttackerSkillTemplet != null) ? AttackerSkillTemplet.m_fEmpowerFactor : 1f;
				float num2 = damageTemplet.m_DamageTempletBase.m_fAtkFactor;
				if (bPVP && !damageTemplet.m_DamageTempletBase.m_fAtkFactorPVP.IsNearlyZero(1E-05f))
				{
					num2 = damageTemplet.m_DamageTempletBase.m_fAtkFactorPVP;
				}
				float num3 = 1f;
				if (statAtk != null)
				{
					float statFinal = statAtk.GetStatFinal(NKM_STAT_TYPE.NST_ATK);
					float num4 = statAtk.GetStatFinal(NKM_STAT_TYPE.NST_HP_GROWN_ATK_RATE);
					float num5 = Math.Max(-1f, num4 * (1f - fAtkHPRate));
					num4 = statFinal + statFinal * num5;
					num3 = num4 * num2 * num;
				}
				if (!bBoss && !bAttackCountOver)
				{
					float num6 = statDef.GetStatFinal(NKM_STAT_TYPE.NST_HP) * damageTemplet.m_DamageTempletBase.m_fAtkMaxHPRateFactor;
					if (bPVP && !damageTemplet.m_DamageTempletBase.m_fAtkMaxHPRateFactorPVP.IsNearlyZero(1E-05f))
					{
						num6 = statDef.GetStatFinal(NKM_STAT_TYPE.NST_HP) * damageTemplet.m_DamageTempletBase.m_fAtkMaxHPRateFactorPVP;
					}
					float num7 = unitDef.GetUnitSyncData().GetHP() * damageTemplet.m_DamageTempletBase.m_fAtkHPRateFactor;
					if (bPVP && !damageTemplet.m_DamageTempletBase.m_fAtkHPRateFactorPVP.IsNearlyZero(1E-05f))
					{
						num7 = unitDef.GetUnitSyncData().GetHP() * damageTemplet.m_DamageTempletBase.m_fAtkHPRateFactorPVP;
					}
					num3 = num3 + num6 + num7;
				}
				float num8 = statDef.GetStatFinal(NKM_STAT_TYPE.NST_DEF);
				float num9 = statDef.GetStatFinal(NKM_STAT_TYPE.NST_HP_GROWN_DEF_RATE);
				float num10 = Math.Max(-1f, num9 * (1f - unitDef.GetHPRate()));
				num9 = num8 + num8 * num10;
				if (statAtk != null)
				{
					num8 = num9 * (1f - statAtk.GetStatFinal(NKM_STAT_TYPE.NST_DEF_PENETRATE_RATE));
				}
				else
				{
					num8 = num9;
				}
				float num11 = 1f - num8 / (num8 + NKMUnitStatManager.m_fConstDef);
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitdataAtk);
				NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(unitDef.GetUnitData());
				if (unitdataAtk != null && unitTempletBase == null)
				{
					Log.Error(string.Format("Can not found UnitTempletBase. UnitId:{0}", unitdataAtk.m_UnitID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitStatManager.cs", 2343);
					return 1f;
				}
				if (unitDef.GetUnitData() != null && unitTempletBase2 == null)
				{
					Log.Error(string.Format("Can not found UnitTempletBase. UnitId:{0}", unitDef.GetUnitData().m_UnitID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitStatManager.cs", 2349);
					return 1f;
				}
				float num12 = NKMUnitStatManager.GetDamageAdjustFactor(unitAtk, unitDef, statAtk, statDef, unitTempletBase, unitTempletBase2, bLongRange);
				if (bTrueDamage)
				{
					if (num11 < 1f)
					{
						num11 = 1f;
					}
					if (num12 < 1f)
					{
						num12 = 1f;
					}
				}
				float num13 = num11 * num12;
				if (num13 < 0.2f)
				{
					num13 = 0.2f;
				}
				float num14 = NKMUnitStatManager.Get2ndDamageAdjustFactor(statAtk, statDef, AttackerSkillTemplet, bSplashHit);
				float roleDamageAdjustFactor = NKMUnitStatManager.GetRoleDamageAdjustFactor(statAtk, statDef, unitTempletBase, unitTempletBase2, ref eNKM_DAMAGE_RESULT_TYPE);
				float num15 = num3 * num13 * num14 * roleDamageAdjustFactor;
				if (statAtk == null)
				{
					bEvade = false;
				}
				float num16 = 1f;
				if (bEvade && statAtk != null)
				{
					num16 = 1f - (NKMUnitStatManager.m_fConstEvadeDamage - statAtk.GetStatFinal(NKM_STAT_TYPE.NST_HIT) / (statAtk.GetStatFinal(NKM_STAT_TYPE.NST_HIT) + NKMUnitStatManager.m_fConstHit));
					eNKM_DAMAGE_RESULT_TYPE = NKM_DAMAGE_RESULT_TYPE.NDRT_MISS;
				}
				bool flag = false;
				float num17 = 0f;
				if (bForceCritical)
				{
					flag = true;
				}
				else if (!bEvade && !bAttackCountOver && statAtk != null && statDef != null)
				{
					int num18 = (int)(((statAtk.GetStatFinal(NKM_STAT_TYPE.NST_CRITICAL) - statDef.GetStatFinal(NKM_STAT_TYPE.NST_CRITICAL_RESIST)) / NKMUnitStatManager.m_fConstCritical).Clamp(0f, 0.85f) * 10000f);
					if (NKMRandom.Range(0, 10000) <= num18)
					{
						flag = true;
					}
				}
				if (bNoCritical)
				{
					flag = false;
				}
				if (bBuffDamage || statAtk == null)
				{
					flag = false;
				}
				if (flag && statAtk != null)
				{
					num17 = statAtk.GetStatFinal(NKM_STAT_TYPE.NST_CRITICAL_DAMAGE_RATE) - statDef.GetStatFinal(NKM_STAT_TYPE.NST_CRITICAL_DAMAGE_RESIST_RATE);
					num17 = num17.Clamp(0f, 5f);
					eNKM_DAMAGE_RESULT_TYPE = NKM_DAMAGE_RESULT_TYPE.NDRT_CRITICAL;
				}
				float num19 = num15 * num17;
				float num20 = num15 * num16 + num19;
				float min = num20 * 0.95f;
				float max = num20 * 1.05f;
				num20 = NKMRandom.Range(min, max);
				if (fDefenderDamageReduce > 0f && unitDef.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE != NKM_UNIT_ROLE_TYPE.NURT_DEFENDER)
				{
					num20 -= num20 * fDefenderDamageReduce;
					if (eNKM_DAMAGE_RESULT_TYPE == NKM_DAMAGE_RESULT_TYPE.NDRT_NORMAL || eNKM_DAMAGE_RESULT_TYPE == NKM_DAMAGE_RESULT_TYPE.NDRT_WEAK)
					{
						eNKM_DAMAGE_RESULT_TYPE = NKM_DAMAGE_RESULT_TYPE.NDRT_PROTECT;
					}
				}
				if (bAttackCountOver)
				{
					num20 *= 0.3f;
				}
				float extraDamageAdjustFactor = NKMUnitStatManager.GetExtraDamageAdjustFactor(unitAtk, unitDef, statAtk, statDef, flag);
				num20 *= extraDamageAdjustFactor;
				if (statDef.GetStatFinal(NKM_STAT_TYPE.NST_DAMAGE_LIMIT_RATE_BY_HP) > 0f)
				{
					float num21 = statDef.GetStatFinal(NKM_STAT_TYPE.NST_DAMAGE_LIMIT_RATE_BY_HP) * statDef.GetStatFinal(NKM_STAT_TYPE.NST_HP);
					if (num20 > num21)
					{
						num20 -= num21;
						num20 *= 0.04f;
						num20 += num21;
					}
				}
				if (num20 < 1f)
				{
					if (bEvade)
					{
						eNKM_DAMAGE_RESULT_TYPE = NKM_DAMAGE_RESULT_TYPE.NDRT_MISS;
						return 0f;
					}
					num20 = 1f;
				}
				return num20;
			}
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x000AF128 File Offset: 0x000AD328
		public static decimal GetFinalStatForUIOutput(NKM_STAT_TYPE eType, NKMStatData statData)
		{
			bool flag = NKMUnitStatManager.IsPercentStat(eType);
			float statBase = statData.GetStatBase(eType);
			float baseBonusStat = statData.GetBaseBonusStat(eType);
			decimal num = new decimal(baseBonusStat);
			if (flag)
			{
				num = Math.Round(num * 1000m);
				num /= 1000m;
			}
			if (flag)
			{
				return Math.Round(decimal.Add(new decimal(statBase), num) * 1000m) / 1000m;
			}
			return new decimal(statBase + baseBonusStat);
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x000AF1B8 File Offset: 0x000AD3B8
		public static int GetNerfPercentByShipBanLevel(int banLevel)
		{
			if (banLevel == 0)
			{
				return 0;
			}
			float num = (float)banLevel * NKMUnitStatManager.m_fPercentPerBanLevel;
			if (num <= 0f)
			{
				num = 0f;
			}
			if (num >= NKMUnitStatManager.m_fMaxPercentPerBanLevel)
			{
				num = NKMUnitStatManager.m_fMaxPercentPerBanLevel;
			}
			return (int)(num * 100f);
		}

		// Token: 0x040022F5 RID: 8949
		public static float m_fConstEvade = 1000f;

		// Token: 0x040022F6 RID: 8950
		public static float m_fConstHit = 1000f;

		// Token: 0x040022F7 RID: 8951
		public static float m_fConstCritical = 2000f;

		// Token: 0x040022F8 RID: 8952
		public static float m_fConstDef = 1000f;

		// Token: 0x040022F9 RID: 8953
		public static float m_fConstEvadeDamage = 0.75f;

		// Token: 0x040022FA RID: 8954
		public static float m_fLONG_RANGE = 800f;

		// Token: 0x040022FB RID: 8955
		public static float m_fPercentPerBanLevel = 0.2f;

		// Token: 0x040022FC RID: 8956
		public static float m_fMaxPercentPerBanLevel = 0.8f;

		// Token: 0x040022FD RID: 8957
		public static float m_fPercentPerUpLevel = 0.1f;

		// Token: 0x040022FE RID: 8958
		public static float m_fMaxPercentPerUpLevel = 0.2f;

		// Token: 0x040022FF RID: 8959
		public static float ROLE_TYPE_BONUS_FACTOR = 0.3f;

		// Token: 0x04002300 RID: 8960
		public static float ROLE_TYPE_REDUCE_FACTOR = 0.3f;

		// Token: 0x04002301 RID: 8961
		public static float m_fDEFENDER_PROTECT_RATE = 0.05f;

		// Token: 0x04002302 RID: 8962
		public static float m_fDEFENDER_PROTECT_RATE_MAX = 0.5f;

		// Token: 0x04002303 RID: 8963
		public static byte m_OperatorSkillLevelPerBanLevel = 2;

		// Token: 0x04002304 RID: 8964
		public static byte m_MinOperatorSkillLevelPerBanLevel = 1;

		// Token: 0x04002305 RID: 8965
		public static float m_OperatorTacticalCommandPerBanLevel = 3f;

		// Token: 0x04002306 RID: 8966
		public static float m_MaxOperatorTacticalCommandPerBanLevel = 12f;

		// Token: 0x04002307 RID: 8967
		public const float FIXED_DAMAGE_ATTACK_STAT = 10000f;

		// Token: 0x04002308 RID: 8968
		public const float FIXED_DAMAGE_DEFENDER_HP = 100000f;
	}
}
