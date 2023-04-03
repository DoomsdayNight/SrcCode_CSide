using System;
using System.Collections.Generic;
using ClientPacket.Common;
using NKC;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;

namespace NKM
{
	// Token: 0x0200036D RID: 877
	public static class NKCOperatorUtil
	{
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600159D RID: 5533 RVA: 0x00057EC7 File Offset: 0x000560C7
		public static Color BAN_COLOR_RED
		{
			get
			{
				return NKCUtil.GetColor("#EC2020");
			}
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x00057ED3 File Offset: 0x000560D3
		public static bool IsHide()
		{
			return !NKMContentsVersionManager.HasTag("OPERATOR");
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x00057EE2 File Offset: 0x000560E2
		public static bool IsActive()
		{
			return NKCContentManager.IsContentsUnlocked(ContentsType.OPERATOR, 0, 0);
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x00057EED File Offset: 0x000560ED
		public static bool IsActiveCastingBan()
		{
			return NKMOpenTagManager.IsOpened("PVP_OPR_BAN");
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x00057EFC File Offset: 0x000560FC
		public static bool IsOperatorUnit(int operatorID)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(operatorID);
			return unitTempletBase != null && unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_OPERATOR;
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x00057F20 File Offset: 0x00056120
		public static bool IsContractFromUnit(long operatorUID)
		{
			NKMOperator operatorData = NKCOperatorUtil.GetOperatorData(operatorUID);
			return operatorData != null && operatorData.fromContract;
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x00057F40 File Offset: 0x00056140
		public static NKMOperator GetOperatorData(long operatorUID)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null && myUserData.m_ArmyData != null)
			{
				return myUserData.m_ArmyData.GetOperatorFromUId(operatorUID);
			}
			return null;
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x00057F74 File Offset: 0x00056174
		public static bool IsMyOperator(long OperatorUID)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			return nkmuserData != null && nkmuserData.m_ArmyData != null && nkmuserData.m_ArmyData.m_dicMyOperator != null && nkmuserData.m_ArmyData.m_dicMyOperator.ContainsKey(OperatorUID);
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x00057FB6 File Offset: 0x000561B6
		public static bool IsSameOperatorGroup(long operatorA, long operatorB)
		{
			return NKCOperatorUtil.GetPassiveGroupID(operatorA) == NKCOperatorUtil.GetPassiveGroupID(operatorB);
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x00057FC8 File Offset: 0x000561C8
		public static void UpdateLockState(long operatorUID, bool bLock)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			if (!nkmuserData.m_ArmyData.m_dicMyOperator.ContainsKey(operatorUID))
			{
				return;
			}
			nkmuserData.m_ArmyData.m_dicMyOperator[operatorUID].bLock = bLock;
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x0005800C File Offset: 0x0005620C
		public static bool IsLock(long operatorUID)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			return nkmuserData != null && nkmuserData.m_ArmyData.m_dicMyOperator.ContainsKey(operatorUID) && nkmuserData.m_ArmyData.m_dicMyOperator[operatorUID].bLock;
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x00058050 File Offset: 0x00056250
		public static NKMOperatorSkillTemplet GetMainSkill(int operatorID)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(operatorID);
			if (unitTempletBase == null)
			{
				return null;
			}
			if (unitTempletBase.m_NKM_UNIT_TYPE != NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				return null;
			}
			if (unitTempletBase.m_lstSkillStrID != null && unitTempletBase.m_lstSkillStrID.Count > 0)
			{
				return NKCOperatorUtil.GetSkillTemplet(unitTempletBase.m_lstSkillStrID[0]);
			}
			return null;
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x000580A0 File Offset: 0x000562A0
		public static NKMOperatorSkillTemplet GetSkillTemplet(string operSkillStrID)
		{
			foreach (NKMOperatorSkillTemplet nkmoperatorSkillTemplet in NKMTempletContainer<NKMOperatorSkillTemplet>.Values)
			{
				if (string.Equals(nkmoperatorSkillTemplet.m_OperSkillStrID, operSkillStrID))
				{
					return nkmoperatorSkillTemplet;
				}
			}
			return null;
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x000580FC File Offset: 0x000562FC
		public static NKMOperatorSkillTemplet GetSkillTemplet(int skillID)
		{
			return NKMTempletContainer<NKMOperatorSkillTemplet>.Find(skillID);
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x00058104 File Offset: 0x00056304
		public static int GetPassiveGroupID(long operatorUID)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null && nkmuserData.m_ArmyData != null && nkmuserData.m_ArmyData.m_dicMyOperator != null && nkmuserData.m_ArmyData.m_dicMyOperator.ContainsKey(operatorUID))
			{
				return NKCOperatorUtil.GetPassiveGroupID(nkmuserData.m_ArmyData.m_dicMyOperator[operatorUID].id);
			}
			return -1;
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x00058160 File Offset: 0x00056360
		public static int GetPassiveGroupID(int operatorID)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(operatorID);
			if (unitTempletBase != null)
			{
				return unitTempletBase.m_OprPassiveGroupID;
			}
			return -1;
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x00058180 File Offset: 0x00056380
		public static bool IsMaximumSkillLevel(int skillID, int skillLevel)
		{
			NKMOperatorSkillTemplet skillTemplet = NKCOperatorUtil.GetSkillTemplet(skillID);
			return skillTemplet != null && skillTemplet.m_MaxSkillLevel <= skillLevel;
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x000581A8 File Offset: 0x000563A8
		public static bool IsCanEnhanceMainSkill(NKMOperator mainOp, NKMOperator targetOp)
		{
			return mainOp != null && targetOp != null && mainOp.mainSkill.id == targetOp.mainSkill.id && !NKCOperatorUtil.IsMaximumSkillLevel(mainOp.mainSkill.id, (int)mainOp.mainSkill.level);
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x000581F8 File Offset: 0x000563F8
		public static bool IsCanEnhanceSubSkill(NKMOperator mainOp, NKMOperator targetOp)
		{
			return mainOp != null && targetOp != null && !NKCOperatorUtil.IsMaximumSkillLevel(mainOp.subSkill.id, (int)mainOp.subSkill.level) && mainOp.subSkill.id == targetOp.subSkill.id;
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x00058244 File Offset: 0x00056444
		public static int GetEnhanceSuccessfulRate(NKMOperator targetOp)
		{
			if (targetOp == null)
			{
				return 0;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(targetOp.id);
			if (unitTempletBase != null)
			{
				NKMOperatorConstTemplet.MaterialUnit materialUnit = NKMCommonConst.OperatorConstTemplet.materialUntis.Find((NKMOperatorConstTemplet.MaterialUnit e) => e.m_NKM_UNIT_GRADE == unitTempletBase.m_NKM_UNIT_GRADE);
				if (materialUnit != null)
				{
					return materialUnit.levelUpSuccessRatePercent;
				}
			}
			return 0;
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x0005829C File Offset: 0x0005649C
		public static int GetTransferSuccessfulRate(NKMOperator targetOp)
		{
			if (targetOp == null)
			{
				return 0;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(targetOp.id);
			if (unitTempletBase != null)
			{
				NKMOperatorConstTemplet.MaterialUnit materialUnit = NKMCommonConst.OperatorConstTemplet.materialUntis.Find((NKMOperatorConstTemplet.MaterialUnit e) => e.m_NKM_UNIT_GRADE == unitTempletBase.m_NKM_UNIT_GRADE);
				if (materialUnit != null)
				{
					return materialUnit.transportSuccessRatePercent;
				}
			}
			return 0;
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x000582F4 File Offset: 0x000564F4
		public static bool IsMaximumLevel(int operatorLv)
		{
			return NKMCommonConst.OperatorConstTemplet.unitMaximumLevel <= operatorLv;
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x00058308 File Offset: 0x00056508
		public static int CalcNegotiationTotalExp(List<MiscItemData> itemList)
		{
			int num = 0;
			foreach (MiscItemData miscItemData in itemList)
			{
				NKMOperatorConstTemplet.Negotiation operatorConstTempletNeogitiation = NKCOperatorUtil.GetOperatorConstTempletNeogitiation(miscItemData.itemId);
				if (operatorConstTempletNeogitiation != null)
				{
					num += operatorConstTempletNeogitiation.exp * miscItemData.count;
				}
			}
			return num;
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x00058374 File Offset: 0x00056574
		public static int CalcNegotiationCostCredit(List<MiscItemData> itemList)
		{
			int num = 0;
			foreach (MiscItemData miscItemData in itemList)
			{
				NKMOperatorConstTemplet.Negotiation operatorConstTempletNeogitiation = NKCOperatorUtil.GetOperatorConstTempletNeogitiation(miscItemData.itemId);
				if (operatorConstTempletNeogitiation != null)
				{
					num += operatorConstTempletNeogitiation.credit * miscItemData.count;
				}
			}
			return num;
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x000583E0 File Offset: 0x000565E0
		private static NKMOperatorConstTemplet.Negotiation GetOperatorConstTempletNeogitiation(int itemID)
		{
			return Array.Find<NKMOperatorConstTemplet.Negotiation>(NKMCommonConst.OperatorConstTemplet.list, (NKMOperatorConstTemplet.Negotiation e) => e.itemId == itemID);
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x00058418 File Offset: 0x00056618
		public static int GetOperatorLevelByTotalExp(int unitId, int totalExp)
		{
			int num = 0;
			int unitMaximumLevel = NKMCommonConst.OperatorConstTemplet.unitMaximumLevel;
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitId);
			if (unitTempletBase == null)
			{
				return -1;
			}
			foreach (NKMOperatorExpData nkmoperatorExpData in NKMOperatorExpTemplet.Find(unitTempletBase.m_NKM_UNIT_GRADE).values)
			{
				if (nkmoperatorExpData.m_iExpCumulatedOpr > totalExp)
				{
					break;
				}
				if (num >= unitMaximumLevel)
				{
					break;
				}
				num = nkmoperatorExpData.m_iLevel;
			}
			return num;
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x000584A4 File Offset: 0x000566A4
		public static int GetOperatorLevelExp(int unitId, int level, int totalExp)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitId);
			if (unitTempletBase == null)
			{
				return -1;
			}
			NKMOperatorExpData nkmoperatorExpData = NKMOperatorExpTemplet.Find(unitTempletBase.m_NKM_UNIT_GRADE).values.Find((NKMOperatorExpData e) => e.m_iLevel == level);
			if (nkmoperatorExpData == null)
			{
				return 0;
			}
			return totalExp - nkmoperatorExpData.m_iExpCumulatedOpr;
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x000584FC File Offset: 0x000566FC
		public static int GetOperatorTotalExp(int unitId, int level, int exp)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitId);
			if (unitTempletBase == null)
			{
				return -1;
			}
			return NKMOperatorExpTemplet.Find(unitTempletBase.m_NKM_UNIT_GRADE).values.Find((NKMOperatorExpData e) => e.m_iLevel == level).m_iExpCumulatedOpr + exp;
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x0005854C File Offset: 0x0005674C
		public static int GetRequiredExp(NKMOperator operatorData)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(operatorData.id);
			if (unitTempletBase != null)
			{
				return NKCOperatorUtil.GetRequiredExp(unitTempletBase.m_NKM_UNIT_GRADE, operatorData.level);
			}
			return 0;
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x0005857B File Offset: 0x0005677B
		public static int GetRequiredExp(int unitID, int level)
		{
			return NKCOperatorUtil.GetRequiredExp(NKMUnitManager.GetUnitTempletBase(unitID).m_NKM_UNIT_GRADE, level);
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x00058590 File Offset: 0x00056790
		public static int GetRequiredExp(NKM_UNIT_GRADE grade, int level)
		{
			NKMOperatorExpTemplet nkmoperatorExpTemplet = NKMOperatorExpTemplet.Find(grade);
			if (nkmoperatorExpTemplet != null)
			{
				foreach (NKMOperatorExpData nkmoperatorExpData in nkmoperatorExpTemplet.values)
				{
					if (nkmoperatorExpData.m_iLevel == level)
					{
						return nkmoperatorExpData.m_iExpRequiredOpr;
					}
				}
				return 0;
			}
			return 0;
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x000585FC File Offset: 0x000567FC
		public static void CalculateFutureOperatorExpAndLevel(NKMOperator operatorData, int expGain, out int Level, out int Exp)
		{
			if (operatorData == null)
			{
				Level = 0;
				Exp = 0;
				return;
			}
			Level = operatorData.level;
			Exp = operatorData.exp + expGain;
			int unitMaximumLevel = NKMCommonConst.OperatorConstTemplet.unitMaximumLevel;
			if (Level >= unitMaximumLevel)
			{
				Exp = 0;
				return;
			}
			int i = NKCOperatorUtil.GetRequiredExp(operatorData);
			if (i == 0)
			{
				return;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(operatorData.id);
			if (unitTempletBase == null)
			{
				return;
			}
			while (i <= Exp)
			{
				Level++;
				if (Level >= unitMaximumLevel)
				{
					Exp = 0;
					return;
				}
				Exp -= i;
				i = NKCOperatorUtil.GetRequiredUnitExp(unitTempletBase.m_NKM_UNIT_GRADE, Level);
				if (i == 0)
				{
					break;
				}
			}
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x0005867F File Offset: 0x0005687F
		public static int CalculateNeedExpForUnitMaxLevel(NKMOperator operatorData, NKM_UNIT_GRADE grade)
		{
			if (operatorData == null)
			{
				return 0;
			}
			return NKCOperatorUtil.CalculateNeedExpForUnitMaxLevel(operatorData.level, operatorData.exp, grade);
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x00058698 File Offset: 0x00056898
		private static int CalculateNeedExpForUnitMaxLevel(int Level, int Exp, NKM_UNIT_GRADE grade)
		{
			int unitMaximumLevel = NKMCommonConst.OperatorConstTemplet.unitMaximumLevel;
			if (Level == unitMaximumLevel)
			{
				return 0;
			}
			int num = NKCOperatorUtil.GetRequiredUnitExp(grade, Level);
			if (num == 0)
			{
				return 0;
			}
			while (Level < unitMaximumLevel)
			{
				Level++;
				if (Level == unitMaximumLevel)
				{
					break;
				}
				num += NKCOperatorUtil.GetRequiredUnitExp(grade, Level);
			}
			return num - Exp;
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x000586E0 File Offset: 0x000568E0
		public static int GetRequiredUnitExp(NKM_UNIT_GRADE grade, int level)
		{
			NKMOperatorExpTemplet nkmoperatorExpTemplet = NKMOperatorExpTemplet.Find(grade);
			if (nkmoperatorExpTemplet != null)
			{
				foreach (NKMOperatorExpData nkmoperatorExpData in nkmoperatorExpTemplet.values)
				{
					if (nkmoperatorExpData.m_iLevel == level)
					{
						return nkmoperatorExpData.m_iExpRequiredOpr;
					}
				}
				return 0;
			}
			return 0;
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x0005874C File Offset: 0x0005694C
		private static bool IsVailedStat(NKM_STAT_TYPE type)
		{
			return type <= NKM_STAT_TYPE.NST_DEF || type == NKM_STAT_TYPE.NST_SKILL_COOL_TIME_REDUCE_RATE;
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x0005875C File Offset: 0x0005695C
		public static float GetStateValue(NKMOperator operatorData, NKM_STAT_TYPE type)
		{
			if (!NKCOperatorUtil.IsVailedStat(type))
			{
				return 0f;
			}
			NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(operatorData.id);
			if (unitStatTemplet == null)
			{
				return 0f;
			}
			return unitStatTemplet.m_StatData.GetStatBase(type) + unitStatTemplet.m_StatData.GetStatPerLevel(type) * (float)(operatorData.level - 1);
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x000587AF File Offset: 0x000569AF
		public static string GetStatPercentageString(NKMOperator operatorData, NKM_STAT_TYPE type)
		{
			if (operatorData == null)
			{
				return "0%";
			}
			return NKCOperatorUtil.GetStatPercentageString(operatorData.id, operatorData.level, type);
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x000587CC File Offset: 0x000569CC
		public static string GetStatPercentageString(int unitID, int level, NKM_STAT_TYPE type)
		{
			if (!NKCOperatorUtil.IsVailedStat(type))
			{
				return "0%";
			}
			NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(unitID);
			if (unitStatTemplet == null)
			{
				return "0%";
			}
			return NKCOperatorUtil.GetStatPercentageString(unitStatTemplet.m_StatData.GetStatBase(type) + unitStatTemplet.m_StatData.GetStatPerLevel(type) * (float)(level - 1));
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x0005881C File Offset: 0x00056A1C
		public static string GetStatPercentageString(float value)
		{
			return (value * 0.01f).ToString("N2") + "%";
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x00058847 File Offset: 0x00056A47
		public static bool IsPercentageStat(NKCUnitSortSystem.eSortOption sortOption)
		{
			return sortOption - NKCUnitSortSystem.eSortOption.Attack_Low <= 5 || sortOption - NKCUnitSortSystem.eSortOption.Unit_ReduceSkillCool_Low <= 1;
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x0005885A File Offset: 0x00056A5A
		public static bool IsPercentageStat(NKCOperatorSortSystem.eSortOption sortOption)
		{
			return sortOption - NKCOperatorSortSystem.eSortOption.Attack_Low <= 7;
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x00058866 File Offset: 0x00056A66
		public static Sprite GetSpriteEmptySlot()
		{
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_operator_deck_sprite", "NKM_UI_OPERATOR_DECK_SLOT_RARE_EMPTY", false);
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x00058878 File Offset: 0x00056A78
		public static Sprite GetSpriteLockSlot()
		{
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_operator_deck_sprite", "NKM_UI_OPERATOR_DECK_SLOT_RARE_LOCK", false);
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x0005888C File Offset: 0x00056A8C
		public static string MakeOperatorSkillDesc(NKMOperatorSkillTemplet templet, int level)
		{
			if (templet == null)
			{
				return "";
			}
			string result = "";
			OperatorSkillType operSkillType = templet.m_OperSkillType;
			if (operSkillType != OperatorSkillType.m_Tactical)
			{
				if (operSkillType == OperatorSkillType.m_Passive)
				{
					using (HashSet<string>.Enumerator enumerator = NKMBattleConditionManager.GetTempletByStrID(templet.m_OperSkillTarget).AllyBuffStrIDList.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							NKMBuffTemplet buffTempletByStrID = NKMBuffManager.GetBuffTempletByStrID(enumerator.Current);
							result = NKCUtilString.ApplyBuffValueToString(NKCStringTable.GetString(templet.m_OperSkillDescStrID, false), buffTempletByStrID, level, level);
						}
					}
				}
			}
			else
			{
				result = NKCUtilString.ApplyBuffValueToString(NKMTacticalCommandManager.GetTacticalCommandTempletByStrID(templet.m_OperSkillTarget), level);
			}
			return result;
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x00058934 File Offset: 0x00056B34
		public static void PlayVoice(NKMDeckIndex deckIdx, VOICE_TYPE type, bool bStopCurrentVoice = true)
		{
			NKMDeckData deckData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetDeckData(deckIdx);
			if (deckData != null && deckData.m_OperatorUID != 0L)
			{
				NKMOperator operatorData = NKCOperatorUtil.GetOperatorData(deckData.m_OperatorUID);
				if (operatorData != null)
				{
					NKCUIVoiceManager.PlayVoice(type, operatorData, false, bStopCurrentVoice);
				}
			}
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x0005897B File Offset: 0x00056B7B
		public static NKMOperator GetDummyOperator(int unitID, bool bSetMaximum = false)
		{
			return NKCOperatorUtil.GetDummyOperator(NKMUnitManager.GetUnitTempletBase(unitID), bSetMaximum);
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x0005898C File Offset: 0x00056B8C
		public static NKMOperator GetDummyOperator(NKMUnitTempletBase _templet, bool bSetMaximum = false)
		{
			if (_templet == null)
			{
				return null;
			}
			NKMOperator nkmoperator = new NKMOperator();
			nkmoperator.id = _templet.m_UnitID;
			nkmoperator.uid = (long)_templet.m_UnitID;
			if (bSetMaximum)
			{
				nkmoperator.level = NKMCommonConst.OperatorConstTemplet.unitMaximumLevel;
			}
			else
			{
				nkmoperator.level = 1;
			}
			if (bSetMaximum)
			{
				nkmoperator.exp = NKCOperatorUtil.GetRequiredExp(_templet.m_NKM_UNIT_GRADE, NKMCommonConst.OperatorConstTemplet.unitMaximumLevel);
			}
			else
			{
				nkmoperator.exp = 0;
			}
			nkmoperator.bLock = false;
			nkmoperator.fromContract = false;
			nkmoperator.mainSkill = new NKMOperatorSkill();
			nkmoperator.subSkill = new NKMOperatorSkill();
			NKMOperatorSkillTemplet skillTemplet = NKCOperatorUtil.GetSkillTemplet(_templet.m_lstSkillStrID[0]);
			if (skillTemplet != null)
			{
				nkmoperator.mainSkill.id = skillTemplet.m_OperSkillID;
				nkmoperator.mainSkill.level = (byte)(bSetMaximum ? skillTemplet.m_MaxSkillLevel : 1);
			}
			nkmoperator.subSkill.id = 0;
			nkmoperator.subSkill.level = 0;
			return nkmoperator;
		}
	}
}
