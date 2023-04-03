using System;
using Cs.Logging;
using NKM.Templet;
using NKM.Unit;

namespace NKM
{
	// Token: 0x02000363 RID: 867
	public static class NKCExpManager
	{
		// Token: 0x060014AF RID: 5295 RVA: 0x0004DE21 File Offset: 0x0004C021
		public static NKMUnitExpTemplet GetUnitExpTemplet(NKMUnitData cUnitData)
		{
			if (cUnitData == null)
			{
				Log.Error("NKMExpManager : UnitData Null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCExpManager.cs", 15);
				return null;
			}
			return cUnitData.GetExpTemplet();
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x0004DE3F File Offset: 0x0004C03F
		public static NKMUserExpTemplet GetUserExpTemplet(NKMUserData cUserData)
		{
			if (cUserData == null)
			{
				Log.Error("NKMExpManager : UserData Null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCExpManager.cs", 26);
				return null;
			}
			return NKMUserExpTemplet.Find(cUserData.UserLevel);
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x0004DE64 File Offset: 0x0004C064
		public static float GetUnitNextLevelExpProgress(NKMUnitData cUnitData)
		{
			NKMUnitExpTemplet unitExpTemplet = NKCExpManager.GetUnitExpTemplet(cUnitData);
			if (unitExpTemplet == null)
			{
				return 0f;
			}
			if (unitExpTemplet.m_iExpRequired == 0)
			{
				return 1f;
			}
			return (float)((long)cUnitData.m_iUnitLevelEXP) / (float)unitExpTemplet.m_iExpRequired;
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x0004DE9F File Offset: 0x0004C09F
		public static float GetOperatorNextLevelExpProgress(NKMOperator operatorData)
		{
			if (operatorData == null)
			{
				return 0f;
			}
			return (float)operatorData.exp / (float)NKCOperatorUtil.GetRequiredExp(operatorData);
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x0004DEBC File Offset: 0x0004C0BC
		public static float GetUnitNextLevelExpProgress(int unitID, int level, int currentExp)
		{
			NKMUnitExpTemplet nkmunitExpTemplet = NKMUnitExpTemplet.FindByUnitId(unitID, level);
			if (nkmunitExpTemplet == null)
			{
				return 0f;
			}
			if (nkmunitExpTemplet.m_iExpRequired == 0)
			{
				return 1f;
			}
			return (float)currentExp / (float)nkmunitExpTemplet.m_iExpRequired;
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0004DEF2 File Offset: 0x0004C0F2
		public static long GetCurrentExp(NKMUnitData cUnitData)
		{
			if (NKCExpManager.GetUnitExpTemplet(cUnitData) == null)
			{
				return 0L;
			}
			return (long)cUnitData.m_iUnitLevelEXP;
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x0004DF08 File Offset: 0x0004C108
		public static int GetRequiredExp(NKMUnitData cUnitData)
		{
			NKMUnitExpTemplet unitExpTemplet = NKCExpManager.GetUnitExpTemplet(cUnitData);
			if (unitExpTemplet == null)
			{
				return 0;
			}
			return unitExpTemplet.m_iExpRequired;
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x0004DF28 File Offset: 0x0004C128
		public static int GetRequiredUnitExp(int unitID, int lv)
		{
			NKMUnitExpTemplet nkmunitExpTemplet = NKMUnitExpTemplet.FindByUnitId(unitID, lv);
			if (nkmunitExpTemplet == null)
			{
				return 0;
			}
			return nkmunitExpTemplet.m_iExpRequired;
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x0004DF48 File Offset: 0x0004C148
		public static float GetNextLevelExpProgress(NKMUserData cUserData)
		{
			NKMUserExpTemplet userExpTemplet = NKCExpManager.GetUserExpTemplet(cUserData);
			if (userExpTemplet == null)
			{
				return 0f;
			}
			if (userExpTemplet.m_lExpRequired == 0)
			{
				return 1f;
			}
			return (float)((long)cUserData.UserLevelEXP) / (float)userExpTemplet.m_lExpRequired;
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x0004DF83 File Offset: 0x0004C183
		public static long GetCurrentExp(NKMUserData cUserData)
		{
			if (NKCExpManager.GetUserExpTemplet(cUserData) == null)
			{
				return 0L;
			}
			return (long)cUserData.UserLevelEXP;
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x0004DF98 File Offset: 0x0004C198
		public static int GetRequiredExp(NKMUserData cUserData)
		{
			NKMUserExpTemplet userExpTemplet = NKCExpManager.GetUserExpTemplet(cUserData);
			if (userExpTemplet == null)
			{
				return 0;
			}
			return userExpTemplet.m_lExpRequired;
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x0004DFB8 File Offset: 0x0004C1B8
		public static int GetRequiredUserExp(int level)
		{
			NKMUserExpTemplet nkmuserExpTemplet = NKMUserExpTemplet.Find(level);
			if (nkmuserExpTemplet == null)
			{
				return 0;
			}
			return nkmuserExpTemplet.m_lExpRequired;
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x0004DFD8 File Offset: 0x0004C1D8
		public static int GetFutureUserLevel(NKMUserData cUserData, int plusEXP)
		{
			if (cUserData == null)
			{
				return 0;
			}
			int num = cUserData.UserLevelEXP;
			int num2 = cUserData.UserLevel;
			int num3 = plusEXP;
			int i = NKCExpManager.GetRequiredExp(cUserData);
			if (i == 0)
			{
				return cUserData.UserLevel;
			}
			while (i <= num + num3)
			{
				num3 -= i - num;
				num2++;
				num = 0;
				i = NKCExpManager.GetRequiredUserExp(num2);
				if (i == 0)
				{
					return num2;
				}
			}
			return num2;
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x0004E02C File Offset: 0x0004C22C
		public static int GetFutureUserRemainEXP(NKMUserData cUserData, int plusEXP)
		{
			if (cUserData == null)
			{
				return 0;
			}
			int num = cUserData.UserLevelEXP;
			int num2 = cUserData.UserLevel;
			int num3 = plusEXP;
			int i = NKCExpManager.GetRequiredExp(cUserData);
			if (i == 0)
			{
				return 0;
			}
			bool flag = false;
			while (i <= num + num3)
			{
				num3 -= i - num;
				num2++;
				num = 0;
				i = NKCExpManager.GetRequiredUserExp(num2);
				flag = true;
				if (i == 0)
				{
					return i;
				}
			}
			if (!flag)
			{
				num3 += num;
			}
			return num3;
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0004E08C File Offset: 0x0004C28C
		public static void CalculateFutureUnitExpAndLevel(NKMUnitData cUnitData, int expGain, out int Level, out int Exp)
		{
			if (cUnitData == null)
			{
				Level = 0;
				Exp = 0;
				return;
			}
			Level = cUnitData.m_UnitLevel;
			Exp = cUnitData.m_iUnitLevelEXP + expGain;
			int unitMaxLevel = NKCExpManager.GetUnitMaxLevel(cUnitData);
			if (Level >= unitMaxLevel)
			{
				Exp = 0;
				return;
			}
			int i = NKCExpManager.GetRequiredUnitExp(cUnitData.m_UnitID, Level);
			if (i == 0)
			{
				return;
			}
			while (i <= Exp)
			{
				Level++;
				if (Level >= unitMaxLevel)
				{
					Exp = 0;
					return;
				}
				Exp -= i;
				i = NKCExpManager.GetRequiredUnitExp(cUnitData.m_UnitID, Level);
				if (i == 0)
				{
					break;
				}
			}
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x0004E104 File Offset: 0x0004C304
		public static int CalculateNeedExpForUnitMaxLevel(NKMUnitData cUnitData)
		{
			if (cUnitData == null)
			{
				return 0;
			}
			int i = cUnitData.m_UnitLevel;
			int iUnitLevelEXP = cUnitData.m_iUnitLevelEXP;
			int unitMaxLevel = NKCExpManager.GetUnitMaxLevel(cUnitData);
			if (i == unitMaxLevel)
			{
				return 0;
			}
			int num = NKCExpManager.GetRequiredUnitExp(cUnitData.m_UnitID, i);
			if (num == 0)
			{
				return 0;
			}
			while (i < unitMaxLevel)
			{
				i++;
				if (i == unitMaxLevel)
				{
					break;
				}
				num += NKCExpManager.GetRequiredUnitExp(cUnitData.m_UnitID, i);
			}
			return num - iUnitLevelEXP;
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x0004E164 File Offset: 0x0004C364
		public static int CalculateUnitExpGain(int unitID, int levelBefore, int expBefore, int levelAfter, int expAfter)
		{
			if (levelBefore > levelAfter)
			{
				return 0;
			}
			int num = 0;
			for (int i = levelBefore; i < levelAfter; i++)
			{
				NKMUnitExpTemplet nkmunitExpTemplet = NKMUnitExpTemplet.FindByUnitId(unitID, i);
				num += nkmunitExpTemplet.m_iExpRequired;
			}
			num += expAfter;
			return num - expBefore;
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0004E1A0 File Offset: 0x0004C3A0
		public static int CalculateUserExpGain(int levelBefore, int expBefore, int levelAfter, int expAfter)
		{
			if (levelBefore > levelAfter)
			{
				return 0;
			}
			if (levelBefore == levelAfter && expBefore > expAfter)
			{
				return 0;
			}
			int num = 0;
			for (int i = levelBefore; i < levelAfter; i++)
			{
				NKMUserExpTemplet nkmuserExpTemplet = NKMUserExpTemplet.Find(i);
				num += nkmuserExpTemplet.m_lExpRequired;
			}
			num += expAfter;
			return num - expBefore;
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0004E1E4 File Offset: 0x0004C3E4
		public static int GetUnitMaxLevel(NKMUnitData cUnitData)
		{
			if (cUnitData == null)
			{
				return 110;
			}
			return NKCExpManager.GetUnitMaxLevel(NKMUnitManager.GetUnitTempletBase(cUnitData.m_UnitID), (int)cUnitData.m_LimitBreakLevel);
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0004E202 File Offset: 0x0004C402
		public static bool IsUnitMaxLevel(NKMUnitData unitData)
		{
			return NKCExpManager.GetUnitMaxLevel(unitData) == unitData.m_UnitLevel;
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x0004E214 File Offset: 0x0004C414
		public static int GetUnitMaxLevel(NKMUnitTempletBase templetBase, int limitBreakLevel)
		{
			NKM_UNIT_TYPE nkm_UNIT_TYPE = templetBase.m_NKM_UNIT_TYPE;
			if (nkm_UNIT_TYPE != NKM_UNIT_TYPE.NUT_NORMAL)
			{
				if (nkm_UNIT_TYPE != NKM_UNIT_TYPE.NUT_SHIP)
				{
					return 110;
				}
				return NKMShipLevelUpTemplet.GetMaxLevel(templetBase.m_StarGradeMax, templetBase.m_NKM_UNIT_GRADE, limitBreakLevel);
			}
			else
			{
				NKMLimitBreakTemplet lbinfo = NKMUnitLimitBreakManager.GetLBInfo(limitBreakLevel);
				if (lbinfo == null)
				{
					Log.Error(string.Format("LimitBreakInfo not found! unit id : {0}, lbLevel {1}", templetBase.m_UnitID, limitBreakLevel), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCExpManager.cs", 382);
					return 110;
				}
				return lbinfo.m_iMaxLevel;
			}
		}
	}
}
