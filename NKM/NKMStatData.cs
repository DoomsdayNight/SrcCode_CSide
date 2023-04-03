using System;
using System.Collections.Generic;
using System.Linq;
using Cs.Logging;
using Cs.Math;
using NKM.Templet;

namespace NKM
{
	// Token: 0x020004BA RID: 1210
	[Serializable]
	public class NKMStatData
	{
		// Token: 0x060021AA RID: 8618 RVA: 0x000AC1EE File Offset: 0x000AA3EE
		private bool IsSecondaryBuffStat(NKM_STAT_TYPE statType)
		{
			return NKMStatData.s_hsSecondaryBuffStat.Contains(statType);
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x000AC1FC File Offset: 0x000AA3FC
		private float GetStat(Dictionary<NKM_STAT_TYPE, float> dicStat, NKM_STAT_TYPE statType)
		{
			float result;
			if (dicStat.TryGetValue(statType, out result))
			{
				return result;
			}
			return 0f;
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x000AC21B File Offset: 0x000AA41B
		private float GetStat(Dictionary<NKM_STAT_TYPE, float> dicStat, int statType)
		{
			return this.GetStat(dicStat, (NKM_STAT_TYPE)statType);
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x000AC226 File Offset: 0x000AA426
		private bool SetStat(Dictionary<NKM_STAT_TYPE, float> dicStat, NKM_STAT_TYPE statType, float fStat)
		{
			if (fStat == 0f)
			{
				dicStat.Remove(statType);
			}
			else
			{
				dicStat[statType] = fStat;
			}
			return true;
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x000AC243 File Offset: 0x000AA443
		private bool SetStat(Dictionary<NKM_STAT_TYPE, float> dicStat, int statType, float fStat)
		{
			return this.SetStat(dicStat, (NKM_STAT_TYPE)statType, fStat);
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x000AC24F File Offset: 0x000AA44F
		private float GetStatBuffFinalFactor(int statType)
		{
			return this.GetStat(this.m_StatBuffFinalFactor, statType);
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x000AC25E File Offset: 0x000AA45E
		private float GetStatBuffFinalFactor(NKM_STAT_TYPE statType)
		{
			return this.GetStat(this.m_StatBuffFinalFactor, statType);
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x000AC26D File Offset: 0x000AA46D
		private bool SetStatBuffFinalFactor(int statType, float fStat)
		{
			return this.SetStat(this.m_StatBuffFinalFactor, statType, fStat);
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x000AC27D File Offset: 0x000AA47D
		private bool SetStatBuffFinalFactor(NKM_STAT_TYPE statType, float fStat)
		{
			return this.SetStat(this.m_StatBuffFinalFactor, statType, fStat);
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x000AC28D File Offset: 0x000AA48D
		private float GetStatBuffFinalValue(int statType)
		{
			return this.GetStat(this.m_StatBuffFinalValue, statType);
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x000AC29C File Offset: 0x000AA49C
		private float GetStatBuffFinalValue(NKM_STAT_TYPE statType)
		{
			return this.GetStat(this.m_StatBuffFinalValue, statType);
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x000AC2AB File Offset: 0x000AA4AB
		private bool SetStatBuffFinalValue(int statType, float fStat)
		{
			return this.SetStat(this.m_StatBuffFinalValue, statType, fStat);
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x000AC2BB File Offset: 0x000AA4BB
		private bool SetStatBuffFinalValue(NKM_STAT_TYPE statType, float fStat)
		{
			return this.SetStat(this.m_StatBuffFinalValue, statType, fStat);
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x000AC2CB File Offset: 0x000AA4CB
		private float GetStatDebuffFinalFactor(int statType)
		{
			return this.GetStat(this.m_StatDebuffFinalFactor, statType);
		}

		// Token: 0x060021B8 RID: 8632 RVA: 0x000AC2DA File Offset: 0x000AA4DA
		private float GetStatDebuffFinalFactor(NKM_STAT_TYPE statType)
		{
			return this.GetStat(this.m_StatDebuffFinalFactor, statType);
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x000AC2E9 File Offset: 0x000AA4E9
		private bool SetStatDebuffFinalFactor(int statType, float fStat)
		{
			return this.SetStat(this.m_StatDebuffFinalFactor, statType, fStat);
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x000AC2F9 File Offset: 0x000AA4F9
		private bool SetStatDebuffFinalFactor(NKM_STAT_TYPE statType, float fStat)
		{
			return this.SetStat(this.m_StatDebuffFinalFactor, statType, fStat);
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x000AC309 File Offset: 0x000AA509
		private float GetStatDebuffFinalValue(int statType)
		{
			return this.GetStat(this.m_StatDebuffFinalValue, statType);
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x000AC318 File Offset: 0x000AA518
		private float GetStatDebuffFinalValue(NKM_STAT_TYPE statType)
		{
			return this.GetStat(this.m_StatDebuffFinalValue, statType);
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x000AC327 File Offset: 0x000AA527
		private bool SetStatDebuffFinalValue(int statType, float fStat)
		{
			return this.SetStat(this.m_StatDebuffFinalValue, statType, fStat);
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x000AC337 File Offset: 0x000AA537
		private bool SetStatDebuffFinalValue(NKM_STAT_TYPE statType, float fStat)
		{
			return this.SetStat(this.m_StatDebuffFinalValue, statType, fStat);
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x000AC347 File Offset: 0x000AA547
		private float GetStatBonusBaseFactor(int statType)
		{
			return this.GetStat(this.m_StatBonusBaseFactor, statType);
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x000AC356 File Offset: 0x000AA556
		private float GetStatBonusBaseFactor(NKM_STAT_TYPE statType)
		{
			return this.GetStat(this.m_StatBonusBaseFactor, statType);
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x000AC365 File Offset: 0x000AA565
		private bool SetStatBonusBaseFactor(int statType, float fStat)
		{
			return this.SetStat(this.m_StatBonusBaseFactor, statType, fStat);
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x000AC375 File Offset: 0x000AA575
		private bool SetStatBonusBaseFactor(NKM_STAT_TYPE statType, float fStat)
		{
			return this.SetStat(this.m_StatBonusBaseFactor, statType, fStat);
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x000AC385 File Offset: 0x000AA585
		private float GetStatBonusBaseValue(int statType)
		{
			return this.GetStat(this.m_StatBonusBaseValue, statType);
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x000AC394 File Offset: 0x000AA594
		private float GetStatBonusBaseValue(NKM_STAT_TYPE statType)
		{
			return this.GetStat(this.m_StatBonusBaseValue, statType);
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x000AC3A3 File Offset: 0x000AA5A3
		private bool SetStatBonusBaseValue(int statType, float fStat)
		{
			return this.SetStat(this.m_StatBonusBaseValue, statType, fStat);
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x000AC3B3 File Offset: 0x000AA5B3
		private bool SetStatBonusBaseValue(NKM_STAT_TYPE statType, float fStat)
		{
			return this.SetStat(this.m_StatBonusBaseValue, statType, fStat);
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x000AC3C3 File Offset: 0x000AA5C3
		public float GetStatPerLevel(int statType)
		{
			return this.GetStat(this.m_StatPerLevel, statType);
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x000AC3D2 File Offset: 0x000AA5D2
		public float GetStatPerLevel(NKM_STAT_TYPE statType)
		{
			return this.GetStat(this.m_StatPerLevel, statType);
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x000AC3E1 File Offset: 0x000AA5E1
		private bool SetStatPerLevel(int statType, float fStat)
		{
			return this.SetStat(this.m_StatPerLevel, statType, fStat);
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x000AC3F1 File Offset: 0x000AA5F1
		private bool SetStatPerLevel(NKM_STAT_TYPE statType, float fStat)
		{
			return this.SetStat(this.m_StatPerLevel, statType, fStat);
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x000AC401 File Offset: 0x000AA601
		public float GetStatMaxPerLevel(int statType)
		{
			return this.GetStat(this.m_StatMaxPerLevel, statType);
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x000AC410 File Offset: 0x000AA610
		public float GetStatMaxPerLevel(NKM_STAT_TYPE statType)
		{
			return this.GetStat(this.m_StatMaxPerLevel, statType);
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x000AC41F File Offset: 0x000AA61F
		private bool SetStatMaxPerLevel(int statType, float fStat)
		{
			return this.SetStat(this.m_StatMaxPerLevel, statType, fStat);
		}

		// Token: 0x060021CE RID: 8654 RVA: 0x000AC42F File Offset: 0x000AA62F
		private bool SetStatMaxPerLevel(NKM_STAT_TYPE statType, float fStat)
		{
			return this.SetStat(this.m_StatMaxPerLevel, statType, fStat);
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x000AC43F File Offset: 0x000AA63F
		public float GetStatEXP(int statType)
		{
			return 0f;
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x000AC446 File Offset: 0x000AA646
		public float GetStatEXP(NKM_STAT_TYPE statType)
		{
			return 0f;
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x000AC44D File Offset: 0x000AA64D
		private bool SetStatEXP(int statType, float stat)
		{
			return true;
		}

		// Token: 0x060021D2 RID: 8658 RVA: 0x000AC450 File Offset: 0x000AA650
		private bool SetStatEXP(NKM_STAT_TYPE statType, float stat)
		{
			return true;
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x000AC453 File Offset: 0x000AA653
		public float GetStatEnhanceFeedEXP(int statType)
		{
			return 0f;
		}

		// Token: 0x060021D4 RID: 8660 RVA: 0x000AC45A File Offset: 0x000AA65A
		public float GetStatEnhanceFeedEXP(NKM_STAT_TYPE statType)
		{
			return 0f;
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x000AC461 File Offset: 0x000AA661
		private bool SetStatEnhanceFeedEXP(int statType, float fStat)
		{
			return true;
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x000AC464 File Offset: 0x000AA664
		private bool SetStatEnhanceFeedEXP(NKM_STAT_TYPE statType, float fStat)
		{
			return true;
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x000AC467 File Offset: 0x000AA667
		public float GetStatBase(int statType)
		{
			return this.GetStat(this.m_StatBase, statType);
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x000AC476 File Offset: 0x000AA676
		public float GetStatBase(NKM_STAT_TYPE statType)
		{
			return this.GetStat(this.m_StatBase, statType);
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x000AC485 File Offset: 0x000AA685
		private bool SetStatBase(int statType, float fStat)
		{
			return this.SetStat(this.m_StatBase, statType, fStat);
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x000AC495 File Offset: 0x000AA695
		private bool SetStatBase(NKM_STAT_TYPE statType, float fStat)
		{
			return this.SetStat(this.m_StatBase, statType, fStat);
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x000AC4A5 File Offset: 0x000AA6A5
		public float GetStatFinal(int statType)
		{
			return this.GetStat(this.m_StatFinal, statType);
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x000AC4B4 File Offset: 0x000AA6B4
		public float GetStatFinal(NKM_STAT_TYPE statType)
		{
			return this.GetStat(this.m_StatFinal, statType);
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x000AC4C3 File Offset: 0x000AA6C3
		private bool SetStatFinal(int statType, float fStat)
		{
			return this.SetStat(this.m_StatFinal, statType, fStat);
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x000AC4D3 File Offset: 0x000AA6D3
		public bool SetStatFinal(NKM_STAT_TYPE statType, float fStat)
		{
			return this.SetStat(this.m_StatFinal, statType, fStat);
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060021DF RID: 8671 RVA: 0x000AC4E3 File Offset: 0x000AA6E3
		public bool HasEnchantFeedExp
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x000AC4E8 File Offset: 0x000AA6E8
		public NKMStatData()
		{
			this.Init();
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x000AC57C File Offset: 0x000AA77C
		public void Init()
		{
			this.m_StatFinal.Clear();
			this.m_StatBase.Clear();
			this.m_StatPerLevel.Clear();
			this.m_StatMaxPerLevel.Clear();
			this.m_StatBonusBaseValue.Clear();
			this.m_StatBonusBaseFactor.Clear();
			this.m_StatBuffFinalValue.Clear();
			this.m_StatBuffFinalFactor.Clear();
			this.m_StatDebuffFinalValue.Clear();
			this.m_StatDebuffFinalFactor.Clear();
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x000AC5F8 File Offset: 0x000AA7F8
		public bool LoadFromLUA(NKMLua cNKMLua, bool bDungeonRespawn = false)
		{
			this.LoadStatFromLUA(cNKMLua, "m_Stat", ref this.m_StatBase);
			this.LoadStatFromLUA(cNKMLua, "m_StatPerLevel", ref this.m_StatPerLevel);
			this.LoadStatFromLUA(cNKMLua, "m_StatMaxPerLevel", ref this.m_StatMaxPerLevel);
			if (bDungeonRespawn)
			{
				this.LoadStatFromLUA(cNKMLua, "m_StatValue", ref this.m_StatBase);
				this.LoadStatFromLUA(cNKMLua, "m_StatFactor", ref this.m_StatPerLevel);
			}
			return true;
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x000AC664 File Offset: 0x000AA864
		private void LoadStatFromLUA(NKMLua cNKMLua, string tableKey, ref Dictionary<NKM_STAT_TYPE, float> refStat)
		{
			refStat.Clear();
			if (cNKMLua.OpenTable(tableKey))
			{
				for (int i = 0; i < 82; i++)
				{
					NKM_STAT_TYPE statType = (NKM_STAT_TYPE)i;
					float stat = this.GetStat(refStat, i);
					if (cNKMLua.GetData(statType.ToString(), ref stat))
					{
						this.SetStat(refStat, statType, stat);
					}
				}
				cNKMLua.CloseTable();
			}
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x000AC6C4 File Offset: 0x000AA8C4
		private void CopyStatDic(Dictionary<NKM_STAT_TYPE, float> target, Dictionary<NKM_STAT_TYPE, float> source)
		{
			target.Clear();
			foreach (KeyValuePair<NKM_STAT_TYPE, float> keyValuePair in source)
			{
				target.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x000AC728 File Offset: 0x000AA928
		public void DeepCopyFromSource(NKMStatData source)
		{
			this.CopyStatDic(this.m_StatFinal, source.m_StatFinal);
			this.CopyStatDic(this.m_StatBase, source.m_StatBase);
			this.CopyStatDic(this.m_StatPerLevel, source.m_StatPerLevel);
			this.CopyStatDic(this.m_StatMaxPerLevel, source.m_StatMaxPerLevel);
			this.CopyStatDic(this.m_StatBonusBaseValue, source.m_StatBonusBaseValue);
			this.CopyStatDic(this.m_StatBonusBaseFactor, source.m_StatBonusBaseFactor);
			this.CopyStatDic(this.m_StatBuffFinalValue, source.m_StatBuffFinalValue);
			this.CopyStatDic(this.m_StatBuffFinalFactor, source.m_StatBuffFinalFactor);
			this.CopyStatDic(this.m_StatDebuffFinalValue, source.m_StatDebuffFinalValue);
			this.CopyStatDic(this.m_StatDebuffFinalFactor, source.m_StatDebuffFinalFactor);
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x000AC7EC File Offset: 0x000AA9EC
		public void MakeOperatorBaseStat(NKMOperator operatorData, NKMStatData unitStatData)
		{
			this.DeepCopyFromSource(unitStatData);
			this.SetStatBase(NKM_STAT_TYPE.NST_ATK, this.CalculateOperatorStat(NKM_STAT_TYPE.NST_ATK, unitStatData, operatorData.level));
			this.SetStatBase(NKM_STAT_TYPE.NST_DEF, this.CalculateOperatorStat(NKM_STAT_TYPE.NST_DEF, unitStatData, operatorData.level));
			this.SetStatBase(NKM_STAT_TYPE.NST_HP, this.CalculateOperatorStat(NKM_STAT_TYPE.NST_HP, unitStatData, operatorData.level));
			this.SetStatBase(NKM_STAT_TYPE.NST_SKILL_COOL_TIME_REDUCE_RATE, this.CalculateOperatorStat(NKM_STAT_TYPE.NST_SKILL_COOL_TIME_REDUCE_RATE, unitStatData, operatorData.level));
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x000AC85A File Offset: 0x000AAA5A
		public float CalculateOperatorStat(NKM_STAT_TYPE type, NKMStatData unitStatData, int level)
		{
			if (unitStatData == null)
			{
				return 0f;
			}
			return unitStatData.GetStatBase(type) + unitStatData.GetStatPerLevel(type) * (float)(level - 1);
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x000AC87C File Offset: 0x000AAA7C
		public float GetDungeonRespawnAddStat(NKMDungeonRespawnUnitTemplet cDungeonRespawnUnitTemplet, float fTargetStat, NKM_STAT_TYPE statType)
		{
			if (cDungeonRespawnUnitTemplet != null)
			{
				if (cDungeonRespawnUnitTemplet.m_AddStatData.GetStatBase(statType) != 0f)
				{
					return fTargetStat + cDungeonRespawnUnitTemplet.m_AddStatData.GetStatBase(statType);
				}
				if (cDungeonRespawnUnitTemplet.m_AddStatData.GetStatPerLevel(statType) != 0f)
				{
					return fTargetStat * cDungeonRespawnUnitTemplet.m_AddStatData.GetStatPerLevel(statType);
				}
			}
			return fTargetStat;
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x000AC8D4 File Offset: 0x000AAAD4
		public void MakeBaseStat(NKMGameData cNKMGameData, bool bPvP, NKMUnitData unitData, NKMStatData unitStatData, bool bPure = false, int buffUnitLevel = 0, NKMOperator cNKMOperator = null)
		{
			this.DeepCopyFromSource(unitStatData);
			int num = unitData.m_UnitLevel + buffUnitLevel;
			if (num < 1)
			{
				num = 1;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData);
			int operatorBanLevel = 0;
			if (bPvP && cNKMGameData != null && cNKMOperator != null && unitTempletBase.IsShip() && cNKMGameData.IsBanOperator(cNKMOperator.id))
			{
				operatorBanLevel = cNKMGameData.GetBanOperatorLevel(cNKMOperator.id);
			}
			for (int i = 0; i < 82; i++)
			{
				NKM_STAT_TYPE nkm_STAT_TYPE = (NKM_STAT_TYPE)i;
				if (NKMUnitStatManager.IsMainStat(nkm_STAT_TYPE))
				{
					NKMGameStatRate cGameStatRate = (cNKMGameData != null) ? cNKMGameData.GameStatRate : null;
					if (bPure)
					{
						float num2 = NKMUnitStatManager.CalculateStat(nkm_STAT_TYPE, unitStatData, unitData.m_listStatEXP, num, 0, 0f, cGameStatRate, null, 0, unitTempletBase.m_NKM_UNIT_TYPE);
						num2 = this.GetDungeonRespawnAddStat(unitData.m_DungeonRespawnUnitTemplet, num2, nkm_STAT_TYPE);
						this.SetStatBase(nkm_STAT_TYPE, num2);
					}
					else
					{
						float num3 = NKMUnitStatManager.CalculateStat(nkm_STAT_TYPE, unitStatData, unitData.m_listStatEXP, num, (int)unitData.m_LimitBreakLevel, unitData.GetMultiplierByPermanentContract(), cGameStatRate, cNKMOperator, operatorBanLevel, unitTempletBase.m_NKM_UNIT_TYPE);
						num3 = this.GetDungeonRespawnAddStat(unitData.m_DungeonRespawnUnitTemplet, num3, nkm_STAT_TYPE);
						this.SetStatBase(nkm_STAT_TYPE, num3);
					}
				}
				else
				{
					float dungeonRespawnAddStat = this.GetDungeonRespawnAddStat(unitData.m_DungeonRespawnUnitTemplet, unitStatData.GetStatBase(nkm_STAT_TYPE), nkm_STAT_TYPE);
					this.SetStatBase(nkm_STAT_TYPE, dungeonRespawnAddStat);
				}
			}
			NKM_TEAM_TYPE teamType = NKM_TEAM_TYPE.NTT_INVALID;
			if (cNKMGameData != null)
			{
				teamType = cNKMGameData.GetTeamType(unitData.m_UserUID);
			}
			if (bPvP && cNKMGameData != null && cNKMGameData.IsUpUnit(unitTempletBase.m_UnitID) && NKMGame.ApplyUpBanByGameType(cNKMGameData, teamType))
			{
				int upUnitLevel = cNKMGameData.GetUpUnitLevel(unitTempletBase.m_UnitID);
				float num4 = this.GetStatBase(NKM_STAT_TYPE.NST_SKILL_COOL_TIME_REDUCE_RATE) + NKMUnitStatManager.m_fPercentPerUpLevel * (float)upUnitLevel;
				if (num4 > this.GetStatBase(NKM_STAT_TYPE.NST_SKILL_COOL_TIME_REDUCE_RATE) + NKMUnitStatManager.m_fMaxPercentPerUpLevel * (float)upUnitLevel)
				{
					num4 = this.GetStatBase(NKM_STAT_TYPE.NST_SKILL_COOL_TIME_REDUCE_RATE) + NKMUnitStatManager.m_fMaxPercentPerUpLevel * (float)upUnitLevel;
				}
				this.SetStatBase(NKM_STAT_TYPE.NST_SKILL_COOL_TIME_REDUCE_RATE, num4);
				for (int j = 0; j <= 5; j++)
				{
					NKM_STAT_TYPE statType = (NKM_STAT_TYPE)j;
					num4 = this.GetStatBase(statType) * (1f + NKMUnitStatManager.m_fPercentPerUpLevel * (float)upUnitLevel);
					if (num4 > this.GetStatBase(statType) * (1f + NKMUnitStatManager.m_fMaxPercentPerUpLevel))
					{
						num4 = this.GetStatBase(statType) * (1f + NKMUnitStatManager.m_fMaxPercentPerUpLevel);
					}
					this.SetStatBase(statType, num4);
				}
			}
			if (bPvP && cNKMGameData != null && unitTempletBase.IsShip() && NKMGame.ApplyUpBanByGameType(cNKMGameData, teamType))
			{
				if (cNKMGameData.IsBanShip(unitTempletBase.m_ShipGroupID))
				{
					int banShipLevel = cNKMGameData.GetBanShipLevel(unitTempletBase.m_ShipGroupID);
					float num5 = this.GetStatBase(NKM_STAT_TYPE.NST_ATK) * (1f - NKMUnitStatManager.m_fPercentPerBanLevel * (float)banShipLevel);
					if (num5 < this.GetStatBase(NKM_STAT_TYPE.NST_ATK) * (1f - NKMUnitStatManager.m_fMaxPercentPerBanLevel))
					{
						num5 = this.GetStatBase(NKM_STAT_TYPE.NST_ATK) * (1f - NKMUnitStatManager.m_fMaxPercentPerBanLevel);
					}
					this.SetStatBase(NKM_STAT_TYPE.NST_ATK, num5);
					num5 = this.GetStatBase(NKM_STAT_TYPE.NST_SKILL_COOL_TIME_REDUCE_RATE) - NKMUnitStatManager.m_fPercentPerBanLevel * (float)banShipLevel;
					if (num5 < this.GetStatBase(NKM_STAT_TYPE.NST_SKILL_COOL_TIME_REDUCE_RATE) - NKMUnitStatManager.m_fMaxPercentPerBanLevel)
					{
						num5 = this.GetStatBase(NKM_STAT_TYPE.NST_SKILL_COOL_TIME_REDUCE_RATE) - NKMUnitStatManager.m_fMaxPercentPerBanLevel;
					}
					this.SetStatBase(NKM_STAT_TYPE.NST_SKILL_COOL_TIME_REDUCE_RATE, num5);
				}
				if (cNKMGameData.GetGameType() == NKM_GAME_TYPE.NGT_PVP_LEAGUE)
				{
					NKMPvpCommonConst.LeaguePvpConst leaguePvp = NKMPvpCommonConst.Instance.LeaguePvp;
					float statBase = this.GetStatBase(NKM_STAT_TYPE.NST_HP);
					float num6 = statBase * leaguePvp.ShipHpMultiply;
					Log.Debug(string.Format("[UnitStat] leaguePvp ship HP:{0}->{1} unitName:{2}", statBase, num6, unitTempletBase.Name), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitStatManager.cs", 691);
					this.SetStatBase(NKM_STAT_TYPE.NST_HP, num6);
					statBase = this.GetStatBase(NKM_STAT_TYPE.NST_ATK);
					num6 = statBase * leaguePvp.ShipAttackPowerMultiply;
					Log.Debug(string.Format("[UnitStat] leaguePvp ship Atk:{0}->{1} unitName:{2}", statBase, num6, unitTempletBase.Name), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitStatManager.cs", 696);
					this.SetStatBase(NKM_STAT_TYPE.NST_ATK, num6);
				}
			}
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x000ACC98 File Offset: 0x000AAE98
		public void MakeBaseBonusFactor(NKMUnitData unitData, IReadOnlyDictionary<long, NKMEquipItemData> dicEquipItemData, List<NKMShipCmdModule> ShipCommandModule, NKMGameStatRate cGameStatRate, bool isPvp)
		{
			if (unitData == null)
			{
				Log.Error("UnitData is null.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitStatManager.cs", 728);
				return;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID);
			if (unitTempletBase == null)
			{
				Log.Error(string.Format("Invalid UnitTemplet. UnitId : {0}", unitData.m_UnitID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitStatManager.cs", 736);
				return;
			}
			bool flag = true;
			this.m_StatBonusBaseValue.Clear();
			this.m_StatBonusBaseFactor.Clear();
			if (unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL)
			{
				int unitSkillCount = unitData.GetUnitSkillCount();
				for (int i = 0; i < unitSkillCount; i++)
				{
					NKMUnitSkillTemplet unitSkillTempletByIndex = unitData.GetUnitSkillTempletByIndex(i);
					if (unitSkillTempletByIndex != null && unitSkillTempletByIndex.m_UnlockReqUpgrade <= (int)unitData.m_LimitBreakLevel)
					{
						foreach (SkillStatData skillStatData in unitSkillTempletByIndex.m_lstSkillStatData)
						{
							if (skillStatData.m_NKM_STAT_TYPE < NKM_STAT_TYPE.NST_END)
							{
								flag &= this.SetStatBonusBaseValue(skillStatData.m_NKM_STAT_TYPE, this.GetStatBonusBaseValue(skillStatData.m_NKM_STAT_TYPE) + skillStatData.m_fStatValue);
								flag &= this.SetStatBonusBaseFactor(skillStatData.m_NKM_STAT_TYPE, this.GetStatBonusBaseFactor(skillStatData.m_NKM_STAT_TYPE) + skillStatData.m_fStatRate);
							}
						}
					}
				}
			}
			if (dicEquipItemData != null)
			{
				float num = (cGameStatRate != null) ? cGameStatRate.GetEquipStatRate() : 1f;
				for (int j = 0; j < 4; j++)
				{
					long equipUid = unitData.GetEquipUid((ITEM_EQUIP_POSITION)j);
					NKMEquipItemData nkmequipItemData;
					if (equipUid > 0L && dicEquipItemData.TryGetValue(equipUid, out nkmequipItemData))
					{
						bool flag2 = unitData.m_UnitID == nkmequipItemData.m_ImprintUnitId;
						NKMCommonConst.ImprintMainOptEffect imprintMainOptEffect = flag2 ? NKMCommonConst.GetEquipIimprintMainOptEffect((ITEM_EQUIP_POSITION)j) : new NKMCommonConst.ImprintMainOptEffect(1f, 1f);
						foreach (ValueTuple<EQUIP_ITEM_STAT, int> valueTuple in nkmequipItemData.m_Stat.Select((EQUIP_ITEM_STAT stat, int index) => new ValueTuple<EQUIP_ITEM_STAT, int>(stat, index)))
						{
							EQUIP_ITEM_STAT item = valueTuple.Item1;
							int item2 = valueTuple.Item2;
							float num2 = (item.stat_value + item.stat_level_value * (float)nkmequipItemData.m_EnchantLevel) * num;
							if (flag2 && item2 == 0)
							{
								bool isMainStat = NKMUnitStatManager.IsMainStat(item.type);
								num2 *= imprintMainOptEffect.GetMultiplyValue(isMainStat);
							}
							flag &= this.SetStatBonusBaseValue(item.type, this.GetStatBonusBaseValue(item.type) + num2);
							float num3 = item.stat_factor * num;
							flag &= this.SetStatBonusBaseFactor(item.type, this.GetStatBonusBaseFactor(item.type) + num3);
						}
						if (nkmequipItemData.potentialOption != null)
						{
							NKMPotentialOption potentialOption = nkmequipItemData.potentialOption;
							float num4 = 0f;
							float num5 = 0f;
							foreach (NKMPotentialOption.SocketData socketData in potentialOption.sockets)
							{
								if (socketData == null)
								{
									break;
								}
								num4 += socketData.statValue;
								num5 += socketData.statFactor;
							}
							flag &= this.SetStatBonusBaseValue(potentialOption.statType, this.GetStatBonusBaseValue(potentialOption.statType) + num4 * num);
							flag &= this.SetStatBonusBaseFactor(potentialOption.statType, this.GetStatBonusBaseFactor(potentialOption.statType) + num5 * num);
						}
					}
				}
				NKMEquipItemData weapon;
				dicEquipItemData.TryGetValue(unitData.GetEquipItemWeaponUid(), out weapon);
				NKMEquipItemData defence;
				dicEquipItemData.TryGetValue(unitData.GetEquipItemDefenceUid(), out defence);
				NKMEquipItemData accessory;
				dicEquipItemData.TryGetValue(unitData.GetEquipItemAccessoryUid(), out accessory);
				NKMEquipItemData accessory2;
				dicEquipItemData.TryGetValue(unitData.GetEquipItemAccessory2Uid(), out accessory2);
				List<NKMItemEquipSetOptionTemplet> activatedSetItem = NKMItemManager.GetActivatedSetItem(new NKMEquipmentSet(weapon, defence, accessory, accessory2));
				for (int l = 0; l < activatedSetItem.Count; l++)
				{
					NKMItemEquipSetOptionTemplet nkmitemEquipSetOptionTemplet = activatedSetItem[l];
					if (nkmitemEquipSetOptionTemplet.m_StatType_1 != NKM_STAT_TYPE.NST_RANDOM && nkmitemEquipSetOptionTemplet.m_StatType_1 != NKM_STAT_TYPE.NST_END)
					{
						flag &= this.SetStatBonusBaseValue(nkmitemEquipSetOptionTemplet.m_StatType_1, this.GetStatBonusBaseValue(nkmitemEquipSetOptionTemplet.m_StatType_1) + nkmitemEquipSetOptionTemplet.m_StatValue_1 * num);
						flag &= this.SetStatBonusBaseFactor(nkmitemEquipSetOptionTemplet.m_StatType_1, this.GetStatBonusBaseFactor(nkmitemEquipSetOptionTemplet.m_StatType_1) + nkmitemEquipSetOptionTemplet.m_StatRate_1 * num);
					}
					if (nkmitemEquipSetOptionTemplet.m_StatType_2 != NKM_STAT_TYPE.NST_RANDOM && nkmitemEquipSetOptionTemplet.m_StatType_2 != NKM_STAT_TYPE.NST_END)
					{
						flag &= this.SetStatBonusBaseValue(nkmitemEquipSetOptionTemplet.m_StatType_2, this.GetStatBonusBaseValue(nkmitemEquipSetOptionTemplet.m_StatType_2) + nkmitemEquipSetOptionTemplet.m_StatValue_2 * num);
						flag &= this.SetStatBonusBaseFactor(nkmitemEquipSetOptionTemplet.m_StatType_2, this.GetStatBonusBaseFactor(nkmitemEquipSetOptionTemplet.m_StatType_2) + nkmitemEquipSetOptionTemplet.m_StatRate_2 * num);
					}
				}
			}
			if (ShipCommandModule != null)
			{
				for (int m = 0; m < ShipCommandModule.Count; m++)
				{
					if (ShipCommandModule[m] != null && ShipCommandModule[m].slots != null)
					{
						for (int n = 0; n < ShipCommandModule[m].slots.Length; n++)
						{
							NKMShipCmdSlot nkmshipCmdSlot = ShipCommandModule[m].slots[n];
							if (nkmshipCmdSlot != null && nkmshipCmdSlot.CanApply(unitData))
							{
								flag &= this.SetStatBonusBaseValue(nkmshipCmdSlot.statType, this.GetStatBonusBaseValue(nkmshipCmdSlot.statType) + nkmshipCmdSlot.statValue);
								flag &= this.SetStatBonusBaseFactor(nkmshipCmdSlot.statType, this.GetStatBonusBaseFactor(nkmshipCmdSlot.statType) + nkmshipCmdSlot.statFactor);
							}
						}
					}
				}
			}
			for (int num6 = 1; num6 <= unitData.tacticLevel; num6++)
			{
				NKMTacticUpdateTemplet nkmtacticUpdateTemplet = NKMTacticUpdateTemplet.Find(num6);
				if (nkmtacticUpdateTemplet == null)
				{
					Log.Error(string.Format("NKMTacticUpdateTemplet level {0} not found!!", num6), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitStatManager.cs", 885);
					flag = false;
				}
				else
				{
					flag &= this.SetStatBonusBaseValue(nkmtacticUpdateTemplet.m_StatType, this.GetStatBonusBaseValue(nkmtacticUpdateTemplet.m_StatType) + nkmtacticUpdateTemplet.m_StatValue * 0.0001f);
				}
			}
			if (!flag)
			{
				Log.Error(string.Format("[UnitStat] stat setting failed. userUid:{0} unitUid:{1}", unitData.m_UserUID, unitData.m_UnitUID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitStatManager.cs", 894);
			}
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x000AD2C4 File Offset: 0x000AB4C4
		public static float GetBuffStatVal(NKM_STAT_TYPE statType, int statVal, int statFactor, int statPerLevel, byte buffLevel, byte buffOverlap)
		{
			if (statVal == 0)
			{
				return 0f;
			}
			return (float)((statVal + statPerLevel * (int)(buffLevel - 1)) * (int)buffOverlap) * 0.0001f;
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x000AD2E1 File Offset: 0x000AB4E1
		public static float GetBuffStatFactor(NKM_STAT_TYPE statType, int statVal, int statFactor, int statPerLevel, byte buffLevel, byte buffOverlap)
		{
			if (statFactor == 0)
			{
				return 0f;
			}
			return (float)((statFactor + statPerLevel * (int)(buffLevel - 1)) * (int)buffOverlap) * 0.0001f;
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x000AD300 File Offset: 0x000AB500
		public void AddBuffStat(NKM_STAT_TYPE statType, int statVal, int statFactor, int statPerLevel, byte buffLevel, byte buffOverlap, bool bDebuff, short casterUID)
		{
			if (this.IsSecondaryBuffStat(statType))
			{
				NKMStatData.SecondaryBuffStatInfo item = default(NKMStatData.SecondaryBuffStatInfo);
				item.statType = statType;
				item.value = ((statVal != 0) ? ((float)((statVal + statPerLevel * (int)(buffLevel - 1)) * (int)buffOverlap) * 0.0001f) : 0f);
				item.factor = ((statFactor != 0) ? ((float)((statFactor + statPerLevel * (int)(buffLevel - 1)) * (int)buffOverlap) * 0.0001f) : 0f);
				item.isDebuff = bDebuff;
				item.casterUID = casterUID;
				this.m_lstSecondaryBuff.Add(item);
				return;
			}
			if (statVal != 0)
			{
				float num = (float)((statVal + statPerLevel * (int)(buffLevel - 1)) * (int)buffOverlap) * 0.0001f;
				if (bDebuff)
				{
					this.SetStatDebuffFinalValue(statType, this.GetStatDebuffFinalValue(statType) + num);
				}
				else
				{
					this.SetStatBuffFinalValue(statType, this.GetStatBuffFinalValue(statType) + num);
				}
			}
			if (statFactor != 0)
			{
				float num2 = (float)((statFactor + statPerLevel * (int)(buffLevel - 1)) * (int)buffOverlap) * 0.0001f;
				if (bDebuff)
				{
					this.SetStatDebuffFinalFactor(statType, this.GetStatDebuffFinalFactor(statType) + num2);
					return;
				}
				this.SetStatBuffFinalFactor(statType, this.GetStatBuffFinalFactor(statType) + num2);
			}
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x000AD40C File Offset: 0x000AB60C
		private void AddSecondaryBuffStat(NKMStatData.SecondaryBuffStatInfo statInfo, NKMGame cNKMGame)
		{
			NKMUnit unit = cNKMGame.GetUnit(statInfo.casterUID, true, false);
			if (statInfo.value != 0f)
			{
				float num = this.ProcessSecondaryBuffStat(unit, statInfo.statType, statInfo.value);
				if (statInfo.isDebuff)
				{
					this.SetStatDebuffFinalValue(statInfo.statType, this.GetStatDebuffFinalValue(statInfo.statType) + num);
				}
				else
				{
					this.SetStatBuffFinalValue(statInfo.statType, this.GetStatBuffFinalValue(statInfo.statType) + num);
				}
			}
			if (statInfo.factor != 0f)
			{
				float factor = statInfo.factor;
				if (statInfo.isDebuff)
				{
					this.SetStatDebuffFinalFactor(statInfo.statType, this.GetStatDebuffFinalFactor(statInfo.statType) + factor);
					return;
				}
				this.SetStatBuffFinalFactor(statInfo.statType, this.GetStatBuffFinalFactor(statInfo.statType) + factor);
			}
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x000AD4DC File Offset: 0x000AB6DC
		private float ProcessSecondaryBuffStat(NKMUnit caster, NKM_STAT_TYPE statType, float value)
		{
			if (statType != NKM_STAT_TYPE.NST_HP_REGEN_RATE)
			{
				return value;
			}
			if (value <= 0f)
			{
				return value;
			}
			float num = 0f;
			if (caster != null)
			{
				num = caster.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HEAL_RATE);
			}
			float num2 = 1f + num - this.GetStatFinal(NKM_STAT_TYPE.NST_HEAL_REDUCE_RATE);
			if (num2 < 0f)
			{
				num2 = 0f;
			}
			return value * num2;
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x000AD538 File Offset: 0x000AB738
		private float GetSecondaryExtraFactor(NKM_STAT_TYPE statType)
		{
			if (statType == NKM_STAT_TYPE.NST_HP_REGEN_RATE)
			{
				float num = 1f - this.GetStatFinal(NKM_STAT_TYPE.NST_HEAL_REDUCE_RATE);
				if (num < 0f)
				{
					num = 0f;
				}
				return num;
			}
			return 1f;
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x000AD570 File Offset: 0x000AB770
		public void UpdateFinalStat(NKMGame cNKMGame, NKMGameStatRate cGameStatRate, NKMUnit cNKMUnit, bool bConserveHPRate = false)
		{
			if (cNKMUnit != null)
			{
				NKMUnitFrameData unitFrameData = cNKMUnit.GetUnitFrameData();
				NKMUnitSyncData unitSyncData = cNKMUnit.GetUnitSyncData();
				bool flag = false;
				float num = 1f;
				if (unitSyncData != null)
				{
					num = unitSyncData.GetHP() / this.GetStatFinal(NKM_STAT_TYPE.NST_HP);
					flag = (unitSyncData.GetHP() >= this.GetStatFinal(NKM_STAT_TYPE.NST_HP));
				}
				this.m_StatBuffFinalFactor.Clear();
				this.m_StatBuffFinalValue.Clear();
				this.m_StatDebuffFinalFactor.Clear();
				this.m_StatDebuffFinalValue.Clear();
				this.m_lstSecondaryBuff.Clear();
				foreach (KeyValuePair<short, NKMBuffData> keyValuePair in unitFrameData.m_dicBuffData)
				{
					NKMBuffData value = keyValuePair.Value;
					if (value.m_BuffSyncData.m_bAffect)
					{
						bool bDebuff = value.m_NKMBuffTemplet.m_bDebuff;
						if (value.m_BuffSyncData.m_bRangeSon)
						{
							bDebuff = value.m_NKMBuffTemplet.m_bDebuffSon;
						}
						if (value.m_NKMBuffTemplet.m_StatType1 != NKM_STAT_TYPE.NST_END)
						{
							this.AddBuffStat(value.m_NKMBuffTemplet.m_StatType1, value.m_NKMBuffTemplet.m_StatValue1, value.m_NKMBuffTemplet.m_StatFactor1, value.m_NKMBuffTemplet.m_StatAddPerLevel1, value.m_BuffSyncData.m_BuffStatLevel, value.m_BuffSyncData.m_OverlapCount, bDebuff, value.m_BuffSyncData.m_MasterGameUnitUID);
						}
						if (value.m_NKMBuffTemplet.m_StatType2 != NKM_STAT_TYPE.NST_END)
						{
							this.AddBuffStat(value.m_NKMBuffTemplet.m_StatType2, value.m_NKMBuffTemplet.m_StatValue2, value.m_NKMBuffTemplet.m_StatFactor2, value.m_NKMBuffTemplet.m_StatAddPerLevel2, value.m_BuffSyncData.m_BuffStatLevel, value.m_BuffSyncData.m_OverlapCount, bDebuff, value.m_BuffSyncData.m_MasterGameUnitUID);
						}
						if (value.m_NKMBuffTemplet.m_StatType3 != NKM_STAT_TYPE.NST_END)
						{
							this.AddBuffStat(value.m_NKMBuffTemplet.m_StatType3, value.m_NKMBuffTemplet.m_StatValue3, value.m_NKMBuffTemplet.m_StatFactor3, value.m_NKMBuffTemplet.m_StatAddPerLevel3, value.m_BuffSyncData.m_BuffStatLevel, value.m_BuffSyncData.m_OverlapCount, bDebuff, value.m_BuffSyncData.m_MasterGameUnitUID);
						}
					}
				}
				if (cNKMUnit.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_STABLE_HP))
				{
					this.SetStatBuffFinalFactor(NKM_STAT_TYPE.NST_HP, 0f);
					this.SetStatBuffFinalValue(NKM_STAT_TYPE.NST_HP, 0f);
					this.SetStatDebuffFinalFactor(NKM_STAT_TYPE.NST_HP, 0f);
					this.SetStatDebuffFinalValue(NKM_STAT_TYPE.NST_HP, 0f);
				}
				this.ApplyBuffStatToFinalStat(cGameStatRate, NKM_STAT_TYPE.NST_CC_RESIST_RATE, false, false);
				for (int i = 0; i < 82; i++)
				{
					NKM_STAT_TYPE nkm_STAT_TYPE = (NKM_STAT_TYPE)i;
					if (nkm_STAT_TYPE != NKM_STAT_TYPE.NST_CC_RESIST_RATE && !this.IsSecondaryBuffStat(nkm_STAT_TYPE))
					{
						this.ApplyBuffStatToFinalStat(cGameStatRate, nkm_STAT_TYPE, nkm_STAT_TYPE != NKM_STAT_TYPE.NST_HP_REGEN_RATE, false);
					}
				}
				foreach (NKMStatData.SecondaryBuffStatInfo statInfo in this.m_lstSecondaryBuff)
				{
					this.AddSecondaryBuffStat(statInfo, cNKMGame);
				}
				foreach (NKM_STAT_TYPE nkm_STAT_TYPE2 in NKMStatData.s_hsSecondaryBuffStat)
				{
					this.ApplyBuffStatToFinalStat(cGameStatRate, nkm_STAT_TYPE2, nkm_STAT_TYPE2 != NKM_STAT_TYPE.NST_HP_REGEN_RATE, true);
				}
				if (this.GetStatFinal(NKM_STAT_TYPE.NST_MAIN_STAT_RATE) != 0f)
				{
					float num2 = 1f + this.GetStatFinal(NKM_STAT_TYPE.NST_MAIN_STAT_RATE);
					if (!cNKMUnit.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_STABLE_HP))
					{
						float num3 = this.GetStatFinal(NKM_STAT_TYPE.NST_HP) * num2;
						if (num3 < 1f)
						{
							num3 = 1f;
						}
						this.SetStatFinal(NKM_STAT_TYPE.NST_HP, num3);
					}
					this.SetStatFinal(NKM_STAT_TYPE.NST_ATK, this.GetStatFinal(NKM_STAT_TYPE.NST_ATK) * num2);
					this.SetStatFinal(NKM_STAT_TYPE.NST_DEF, this.GetStatFinal(NKM_STAT_TYPE.NST_DEF) * num2);
					this.SetStatFinal(NKM_STAT_TYPE.NST_CRITICAL, this.GetStatFinal(NKM_STAT_TYPE.NST_CRITICAL) * num2);
					this.SetStatFinal(NKM_STAT_TYPE.NST_HIT, this.GetStatFinal(NKM_STAT_TYPE.NST_HIT) * num2);
					this.SetStatFinal(NKM_STAT_TYPE.NST_EVADE, this.GetStatFinal(NKM_STAT_TYPE.NST_EVADE) * num2);
				}
				if (unitSyncData != null)
				{
					if (bConserveHPRate)
					{
						unitSyncData.SetHP(this.GetStatFinal(NKM_STAT_TYPE.NST_HP) * num);
						return;
					}
					if (flag)
					{
						unitSyncData.SetHP(this.GetStatFinal(NKM_STAT_TYPE.NST_HP));
						return;
					}
					if (unitSyncData.GetHP() >= this.GetStatFinal(NKM_STAT_TYPE.NST_HP))
					{
						unitSyncData.SetHP(this.GetStatFinal(NKM_STAT_TYPE.NST_HP));
						return;
					}
				}
			}
			else
			{
				for (int j = 0; j < 82; j++)
				{
					this.SetStatFinal(j, this.GetStatBase(j));
				}
			}
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x000ADA10 File Offset: 0x000ABC10
		private void ApplyBuffStatToFinalStat(NKMGameStatRate cGameStatRate, NKM_STAT_TYPE stattype, bool bApplyResistDebuff, bool bSecondary)
		{
			float num = (cGameStatRate != null) ? cGameStatRate.GetStatValueRate(stattype) : 1f;
			float num2 = (cGameStatRate != null) ? cGameStatRate.GetStatFactorRate(stattype) : 1f;
			float num3 = 1f;
			if (bApplyResistDebuff)
			{
				num3 -= this.GetStatFinal(NKM_STAT_TYPE.NST_CC_RESIST_RATE);
			}
			float num4 = this.GetStatBonusBaseFactor(stattype) + this.GetStatBuffFinalFactor(stattype) + this.GetStatDebuffFinalFactor(stattype) * num3;
			float num5 = 1f;
			float num6;
			if (bSecondary)
			{
				num6 = this.ProcessSecondaryBuffStat(null, stattype, this.GetStatBonusBaseValue(stattype)) + this.GetStatBuffFinalValue(stattype) + this.GetStatDebuffFinalValue(stattype) * num3;
				num5 = this.GetSecondaryExtraFactor(stattype);
			}
			else
			{
				num6 = this.GetStatBonusBaseValue(stattype) + this.GetStatBuffFinalValue(stattype) + this.GetStatDebuffFinalValue(stattype) * num3;
			}
			float num7 = this.GetStatBase(stattype) * (1f + num4 * num2) * num5 + num6 * num;
			num7 = this.ApplyStatCap(stattype, num7);
			this.SetStatFinal(stattype, num7);
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x000ADAF4 File Offset: 0x000ABCF4
		private float ApplyStatCap(NKM_STAT_TYPE statType, float value)
		{
			switch (statType)
			{
			case NKM_STAT_TYPE.NST_HP:
				if (value < 1f)
				{
					return 1f;
				}
				return value;
			case NKM_STAT_TYPE.NST_HP_REGEN_RATE:
			case NKM_STAT_TYPE.NST_DAMAGE_REDUCE_RATE:
			case NKM_STAT_TYPE.NST_MOVE_SPEED_RATE:
			case NKM_STAT_TYPE.NST_ATTACK_SPEED_RATE:
			case NKM_STAT_TYPE.NST_ATTACK_DAMAGE_MODIFY_G2:
				if (value < -0.9f)
				{
					return -0.9f;
				}
				return value;
			case NKM_STAT_TYPE.NST_SKILL_COOL_TIME_REDUCE_RATE:
			case NKM_STAT_TYPE.NST_UNIT_TYPE_COUNTER_DAMAGE_RATE:
			case NKM_STAT_TYPE.NST_UNIT_TYPE_COUNTER_DAMAGE_REDUCE_RATE:
			case NKM_STAT_TYPE.NST_UNIT_TYPE_SOLDIER_DAMAGE_RATE:
			case NKM_STAT_TYPE.NST_UNIT_TYPE_SOLDIER_DAMAGE_REDUCE_RATE:
			case NKM_STAT_TYPE.NST_UNIT_TYPE_MECHANIC_DAMAGE_RATE:
			case NKM_STAT_TYPE.NST_UNIT_TYPE_MECHANIC_DAMAGE_REDUCE_RATE:
			case NKM_STAT_TYPE.NST_ROLE_TYPE_STRIKER_DAMAGE_RATE:
			case NKM_STAT_TYPE.NST_ROLE_TYPE_STRIKER_DAMAGE_REDUCE_RATE:
			case NKM_STAT_TYPE.NST_ROLE_TYPE_RANGER_DAMAGE_RATE:
			case NKM_STAT_TYPE.NST_ROLE_TYPE_RANGER_DAMAGE_REDUCE_RATE:
			case NKM_STAT_TYPE.NST_ROLE_TYPE_SNIPER_DAMAGE_RATE:
			case NKM_STAT_TYPE.NST_ROLE_TYPE_SNIPER_DAMAGE_REDUCE_RATE:
			case NKM_STAT_TYPE.NST_ROLE_TYPE_DEFFENDER_DAMAGE_RATE:
			case NKM_STAT_TYPE.NST_ROLE_TYPE_DEFFENDER_DAMAGE_REDUCE_RATE:
			case NKM_STAT_TYPE.NST_ROLE_TYPE_SUPPOERTER_DAMAGE_RATE:
			case NKM_STAT_TYPE.NST_ROLE_TYPE_SUPPOERTER_DAMAGE_REDUCE_RATE:
			case NKM_STAT_TYPE.NST_ROLE_TYPE_SIEGE_DAMAGE_RATE:
			case NKM_STAT_TYPE.NST_ROLE_TYPE_SIEGE_DAMAGE_REDUCE_RATE:
			case NKM_STAT_TYPE.NST_ROLE_TYPE_TOWER_DAMAGE_RATE:
			case NKM_STAT_TYPE.NST_ROLE_TYPE_TOWER_DAMAGE_REDUCE_RATE:
			case NKM_STAT_TYPE.NST_MOVE_TYPE_LAND_DAMAGE_RATE:
			case NKM_STAT_TYPE.NST_MOVE_TYPE_LAND_DAMAGE_REDUCE_RATE:
			case NKM_STAT_TYPE.NST_MOVE_TYPE_AIR_DAMAGE_RATE:
			case NKM_STAT_TYPE.NST_MOVE_TYPE_AIR_DAMAGE_REDUCE_RATE:
			case NKM_STAT_TYPE.NST_LONG_RANGE_DAMAGE_REDUCE_RATE:
			case NKM_STAT_TYPE.NST_LONG_RANGE_DAMAGE_RATE:
			case NKM_STAT_TYPE.NST_SHORT_RANGE_DAMAGE_REDUCE_RATE:
			case NKM_STAT_TYPE.NST_SHORT_RANGE_DAMAGE_RATE:
			case NKM_STAT_TYPE.NST_HEAL_REDUCE_RATE:
			case NKM_STAT_TYPE.NST_SKILL_DAMAGE_RATE:
			case NKM_STAT_TYPE.NST_HYPER_SKILL_DAMAGE_RATE:
			case NKM_STAT_TYPE.NST_UNIT_TYPE_CORRUPTED_DAMAGE_RATE:
			case NKM_STAT_TYPE.NST_UNIT_TYPE_CORRUPTED_DAMAGE_REDUCE_RATE:
			case NKM_STAT_TYPE.NST_UNIT_TYPE_REPLACER_DAMAGE_RATE:
			case NKM_STAT_TYPE.NST_UNIT_TYPE_REPLACER_DAMAGE_REDUCE_RATE:
			case NKM_STAT_TYPE.NST_ROLE_TYPE_DAMAGE_RATE:
			case NKM_STAT_TYPE.NST_ROLE_TYPE_DAMAGE_REDUCE_RATE:
			case NKM_STAT_TYPE.NST_HP_GROWN_ATK_RATE:
			case NKM_STAT_TYPE.NST_HP_GROWN_DEF_RATE:
			case NKM_STAT_TYPE.NST_HP_GROWN_EVADE_RATE:
			case NKM_STAT_TYPE.NST_NON_CRITICAL_DAMAGE_TAKE_RATE:
			case NKM_STAT_TYPE.NST_HEAL_RATE:
				if (value < -2f)
				{
					return -2f;
				}
				return value;
			case NKM_STAT_TYPE.NST_CC_RESIST_RATE:
				return value.Clamp(-1f, 0.8f);
			case NKM_STAT_TYPE.NST_DEF_PENETRATE_RATE:
				return value.Clamp(0f, 1f);
			case NKM_STAT_TYPE.NST_DAMAGE_BACK_RESIST:
				return value.Clamp(-1f, 1f);
			case NKM_STAT_TYPE.NST_MAIN_STAT_RATE:
				if (value < -0.99f)
				{
					return -0.99f;
				}
				return value;
			case NKM_STAT_TYPE.NST_EXTRA_ADJUST_DAMAGE_DEALT:
			case NKM_STAT_TYPE.NST_EXTRA_ADJUST_DAMAGE_RECEIVE:
				return value;
			case NKM_STAT_TYPE.NST_COST_RETURN_RATE:
				return value.Clamp(0f, 0.5f);
			}
			if (value < 0f)
			{
				value = 0f;
			}
			return value;
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x000ADCF0 File Offset: 0x000ABEF0
		public float GetBaseBonusStat(NKM_STAT_TYPE eNKM_STAT_TYPE)
		{
			if (eNKM_STAT_TYPE >= NKM_STAT_TYPE.NST_END)
			{
				return 0f;
			}
			return this.GetStatBase(eNKM_STAT_TYPE) * this.GetStatBonusBaseFactor(eNKM_STAT_TYPE) + this.GetStatBonusBaseValue(eNKM_STAT_TYPE);
		}

		// Token: 0x040022DD RID: 8925
		private Dictionary<NKM_STAT_TYPE, float> m_StatBase = new Dictionary<NKM_STAT_TYPE, float>();

		// Token: 0x040022DE RID: 8926
		private Dictionary<NKM_STAT_TYPE, float> m_StatBonusBaseValue = new Dictionary<NKM_STAT_TYPE, float>();

		// Token: 0x040022DF RID: 8927
		private Dictionary<NKM_STAT_TYPE, float> m_StatBonusBaseFactor = new Dictionary<NKM_STAT_TYPE, float>();

		// Token: 0x040022E0 RID: 8928
		private Dictionary<NKM_STAT_TYPE, float> m_StatBuffFinalValue = new Dictionary<NKM_STAT_TYPE, float>();

		// Token: 0x040022E1 RID: 8929
		private Dictionary<NKM_STAT_TYPE, float> m_StatBuffFinalFactor = new Dictionary<NKM_STAT_TYPE, float>();

		// Token: 0x040022E2 RID: 8930
		private Dictionary<NKM_STAT_TYPE, float> m_StatDebuffFinalValue = new Dictionary<NKM_STAT_TYPE, float>();

		// Token: 0x040022E3 RID: 8931
		private Dictionary<NKM_STAT_TYPE, float> m_StatDebuffFinalFactor = new Dictionary<NKM_STAT_TYPE, float>();

		// Token: 0x040022E4 RID: 8932
		private Dictionary<NKM_STAT_TYPE, float> m_StatFinal = new Dictionary<NKM_STAT_TYPE, float>();

		// Token: 0x040022E5 RID: 8933
		private List<NKMStatData.SecondaryBuffStatInfo> m_lstSecondaryBuff = new List<NKMStatData.SecondaryBuffStatInfo>();

		// Token: 0x040022E6 RID: 8934
		private static readonly HashSet<NKM_STAT_TYPE> s_hsSecondaryBuffStat = new HashSet<NKM_STAT_TYPE>
		{
			NKM_STAT_TYPE.NST_HP_REGEN_RATE
		};

		// Token: 0x040022E7 RID: 8935
		private Dictionary<NKM_STAT_TYPE, float> m_StatPerLevel = new Dictionary<NKM_STAT_TYPE, float>();

		// Token: 0x040022E8 RID: 8936
		private Dictionary<NKM_STAT_TYPE, float> m_StatMaxPerLevel = new Dictionary<NKM_STAT_TYPE, float>();

		// Token: 0x0200121D RID: 4637
		private struct SecondaryBuffStatInfo
		{
			// Token: 0x04009488 RID: 38024
			public NKM_STAT_TYPE statType;

			// Token: 0x04009489 RID: 38025
			public float factor;

			// Token: 0x0400948A RID: 38026
			public float value;

			// Token: 0x0400948B RID: 38027
			public bool isDebuff;

			// Token: 0x0400948C RID: 38028
			public short casterUID;
		}
	}
}
