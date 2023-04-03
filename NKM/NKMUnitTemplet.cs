using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cs.Logging;
using NKM.Templet;
using NKM.Templet.Base;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004EF RID: 1263
	public sealed class NKMUnitTemplet
	{
		// Token: 0x060023A7 RID: 9127 RVA: 0x000B7D38 File Offset: 0x000B5F38
		public NKM_UNIT_ATTACK_STATE_TYPE GetAttackStateType(int stateID)
		{
			NKM_UNIT_ATTACK_STATE_TYPE result;
			if (this.m_dicAttackStateType.TryGetValue(stateID, out result))
			{
				return result;
			}
			return NKM_UNIT_ATTACK_STATE_TYPE.INVALID;
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x000B7FCC File Offset: 0x000B61CC
		public void Validate()
		{
			if (this.m_listStaticBuffData != null)
			{
				foreach (NKMStaticBuffData nkmstaticBuffData in this.m_listStaticBuffData)
				{
					if (!nkmstaticBuffData.Validate())
					{
						Log.ErrorAndExit(string.Concat(new string[]
						{
							"[UnitTemplet] m_listStaticBuffData is invalid. UnitStrID [",
							this.m_UnitTempletBase.m_UnitStrID,
							"], StaticBuffDataStrID [",
							nkmstaticBuffData.m_BuffStrID,
							"]"
						}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitTemplet.cs", 164);
					}
				}
			}
			if (this.m_listReflectionBuffData != null)
			{
				foreach (NKMStaticBuffData nkmstaticBuffData2 in this.m_listReflectionBuffData)
				{
					if (!nkmstaticBuffData2.Validate())
					{
						Log.ErrorAndExit(string.Concat(new string[]
						{
							"[UnitTemplet] m_listReflectionBuffData is invalid. UnitStrID [",
							this.m_UnitTempletBase.m_UnitStrID,
							"], ReflectionBuffDataStrID [",
							nkmstaticBuffData2.m_BuffStrID,
							"]"
						}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitTemplet.cs", 175);
					}
				}
			}
			if (this.m_dicNKMUnitState != null)
			{
				foreach (NKMUnitState nkmunitState in this.m_dicNKMUnitState.Values)
				{
					nkmunitState.Validate(this.m_UnitTempletBase.m_UnitStrID);
				}
			}
			if (this.m_UnitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
			{
				foreach (string text in this.m_UnitTempletBase.m_lstSkillStrID)
				{
					NKMShipSkillTemplet shipSkillTempletByStrID = NKMShipSkillManager.GetShipSkillTempletByStrID(text);
					if (shipSkillTempletByStrID == null)
					{
						Log.ErrorAndExit("[UnitTemplet] 전함 스킬 아이디가 올바르지 않음. UnitStrID:" + this.m_UnitTempletBase.m_UnitStrID + " skillStrID:" + text, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitTemplet.cs", 211);
					}
					else if (!shipSkillTempletByStrID.m_NKM_SKILL_TYPE.IsStatHoldType() && this.GetUnitState(shipSkillTempletByStrID.m_UnitStateName, true) == null)
					{
						Log.ErrorAndExit(string.Concat(new string[]
						{
							"[UnitTemplet] 전함 스킬에 따른 유닛 상태 정보가 존재하지 않음 UnitStrID:",
							this.m_UnitTempletBase.m_UnitStrID,
							" skillStrID:",
							text,
							" skillUnitStateName:",
							shipSkillTempletByStrID.m_UnitStateName
						}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitTemplet.cs", 216);
					}
				}
			}
			IEventConditionOwner[] array = this.m_listHitCriticalFeedBack.Cast<IEventConditionOwner>().Union(this.m_listPhaseChangeData.Cast<IEventConditionOwner>()).Union(this.m_listReflectionBuffData.Cast<IEventConditionOwner>()).Union(this.m_listStaticBuffData.Cast<IEventConditionOwner>()).Union(this.m_listHitFeedBack.Cast<IEventConditionOwner>()).Union(this.m_listAccumStateChangePack.Cast<IEventConditionOwner>()).Union(this.m_listBuffUnitDieEvent.Cast<IEventConditionOwner>()).Union(this.m_listStartStateData.Cast<IEventConditionOwner>()).Union(this.m_listStandStateData.Cast<IEventConditionOwner>()).Union(this.m_listRunStateData.Cast<IEventConditionOwner>()).Union(this.m_listAttackStateData.Cast<IEventConditionOwner>()).Union(this.m_listAirAttackStateData.Cast<IEventConditionOwner>()).Union(this.m_listSkillStateData.Cast<IEventConditionOwner>()).Union(this.m_listAirSkillStateData.Cast<IEventConditionOwner>()).Union(this.m_listHyperSkillStateData.Cast<IEventConditionOwner>()).Union(this.m_listAirHyperSkillStateData.Cast<IEventConditionOwner>()).Union(this.m_listPassiveAttackStateData.Cast<IEventConditionOwner>()).Union(this.m_listAirPassiveAttackStateData.Cast<IEventConditionOwner>()).Union(this.m_listHitEvadeFeedBack.Cast<IEventConditionOwner>()).Union(this.m_listKillFeedBack.Cast<IEventConditionOwner>()).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventDirSpeed.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventSpeed.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventSpeedX.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventSpeedY.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventMove.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventAttack.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventStopTime.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventInvincible.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventInvincibleGlobal.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventSuperArmor.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventSound.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventColor.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventCameraCrash.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventCameraMove.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventFadeWorld.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventDissolve.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventMotionBlur.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventEffect.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventHyperSkillCutIn.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventDamageEffect.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventDEStateChange.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventGameSpeed.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventBuff.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventStatus.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventRespawn.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventDie.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventChangeState.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventAgro.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventHeal.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventStun.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventCost.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventDefence.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventDispel.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventChangeCooltime.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventCatchEnd.Cast<IEventConditionOwner>())).Union(this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventFindTarget.Cast<IEventConditionOwner>())).Union((from e in this.m_dicNKMUnitState.Values
			select e.m_NKMEventUnitChange into e
			where e != null
			select e).Cast<IEventConditionOwner>()).ToArray<IEventConditionOwner>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].ValidateSkillId(this.m_UnitTempletBase);
			}
			foreach (NKMEventRespawn nkmeventRespawn in this.m_dicNKMUnitState.Values.SelectMany((NKMUnitState e) => e.m_listNKMEventRespawn))
			{
				nkmeventRespawn.ValidateSummon(this.m_UnitTempletBase);
			}
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x000B8BE4 File Offset: 0x000B6DE4
		public bool LoadFromLUA(NKMLua cNKMLua, NKMUnitTempletBase cNKMUnitTempletBase)
		{
			NKMUnitTemplet.<>c__DisplayClass95_0 CS$<>8__locals1;
			CS$<>8__locals1.cNKMLua = cNKMLua;
			CS$<>8__locals1.<>4__this = this;
			bool result;
			try
			{
				this.m_UnitTempletBase = cNKMUnitTempletBase;
				this.m_StatTemplet = NKMUnitManager.GetUnitStatTemplet(this.m_UnitTempletBase.m_UnitID);
				CS$<>8__locals1.cNKMLua.GetData("m_SpriteScale", ref this.m_SpriteScale);
				CS$<>8__locals1.cNKMLua.GetData("m_SpriteOffsetX", ref this.m_SpriteOffsetX);
				CS$<>8__locals1.cNKMLua.GetData("m_SpriteOffsetY", ref this.m_SpriteOffsetY);
				CS$<>8__locals1.cNKMLua.GetData("m_fForceRespawnXpos", ref this.m_fForceRespawnXpos);
				CS$<>8__locals1.cNKMLua.GetData("m_fForceRespawnZposMin", ref this.m_fForceRespawnZposMin);
				CS$<>8__locals1.cNKMLua.GetData("m_fForceRespawnZposMax", ref this.m_fForceRespawnZposMax);
				CS$<>8__locals1.cNKMLua.GetData("m_UnitSizeX", ref this.m_UnitSizeX);
				CS$<>8__locals1.cNKMLua.GetData("m_UnitSizeY", ref this.m_UnitSizeY);
				CS$<>8__locals1.cNKMLua.GetData<NKC_TEAM_COLOR_TYPE>("m_NKC_TEAM_COLOR_TYPE", ref this.m_NKC_TEAM_COLOR_TYPE);
				CS$<>8__locals1.cNKMLua.GetData("m_fShadowOffsetX", ref this.m_fShadowOffsetX);
				CS$<>8__locals1.cNKMLua.GetData("m_fShadowOffsetY", ref this.m_fShadowOffsetY);
				CS$<>8__locals1.cNKMLua.GetData("m_fShadowScaleX", ref this.m_fShadowScaleX);
				CS$<>8__locals1.cNKMLua.GetData("m_fShadowScaleY", ref this.m_fShadowScaleY);
				CS$<>8__locals1.cNKMLua.GetData("m_fShadowRotateX", ref this.m_fShadowRotateX);
				CS$<>8__locals1.cNKMLua.GetData("m_fShadowRotateZ", ref this.m_fShadowRotateZ);
				CS$<>8__locals1.cNKMLua.GetData("m_fBuffEffectScaleFactor", ref this.m_fBuffEffectScaleFactor);
				CS$<>8__locals1.cNKMLua.GetData("m_bShowGage", ref this.m_bShowGage);
				CS$<>8__locals1.cNKMLua.GetData("m_bGageSmall", ref this.m_bGageSmall);
				CS$<>8__locals1.cNKMLua.GetData("m_fGageOffsetX", ref this.m_fGageOffsetX);
				CS$<>8__locals1.cNKMLua.GetData("m_fGageOffsetY", ref this.m_fGageOffsetY);
				CS$<>8__locals1.cNKMLua.GetData("m_fRespawnCoolTime", ref this.m_fRespawnCoolTime);
				CS$<>8__locals1.cNKMLua.GetData("m_fDieCompleteTime", ref this.m_fDieCompleteTime);
				CS$<>8__locals1.cNKMLua.GetData("m_bDieDeActive", ref this.m_bDieDeActive);
				CS$<>8__locals1.cNKMLua.GetData("m_bUseMotionBlur", ref this.m_bUseMotionBlur);
				CS$<>8__locals1.cNKMLua.GetData("m_bNoDamageState", ref this.m_bNoDamageState);
				CS$<>8__locals1.cNKMLua.GetData("m_bNoDamageDownState", ref this.m_bNoDamageDownState);
				CS$<>8__locals1.cNKMLua.GetData("m_bNoDamageStopTime", ref this.m_bNoDamageStopTime);
				CS$<>8__locals1.cNKMLua.GetData("m_Invincible", ref this.m_Invincible);
				CS$<>8__locals1.cNKMLua.GetData<NKM_SUPER_ARMOR_LEVEL>("m_SuperArmorLevel", ref this.m_SuperArmorLevel);
				CS$<>8__locals1.cNKMLua.GetData("m_ColorR", ref this.m_ColorR);
				CS$<>8__locals1.cNKMLua.GetData("m_ColorG", ref this.m_ColorG);
				CS$<>8__locals1.cNKMLua.GetData("m_ColorB", ref this.m_ColorB);
				CS$<>8__locals1.cNKMLua.GetData("m_fAirHigh", ref this.m_fAirHigh);
				CS$<>8__locals1.cNKMLua.GetData("m_bNoMove", ref this.m_bNoMove);
				CS$<>8__locals1.cNKMLua.GetData("m_bNoRun", ref this.m_bNoRun);
				CS$<>8__locals1.cNKMLua.GetData("m_SpeedRun", ref this.m_SpeedRun);
				CS$<>8__locals1.cNKMLua.GetData("m_SpeedJump", ref this.m_SpeedJump);
				CS$<>8__locals1.cNKMLua.GetData("m_fReloadAccel", ref this.m_fReloadAccel);
				CS$<>8__locals1.cNKMLua.GetData("m_fGAccel", ref this.m_fGAccel);
				CS$<>8__locals1.cNKMLua.GetData("m_fMaxGSpeed", ref this.m_fMaxGSpeed);
				CS$<>8__locals1.cNKMLua.GetData("m_fDamageBackFactor", ref this.m_fDamageBackFactor);
				CS$<>8__locals1.cNKMLua.GetData("m_fDamageUpFactor", ref this.m_fDamageUpFactor);
				CS$<>8__locals1.cNKMLua.GetData("m_bNoMapLimit", ref this.m_bNoMapLimit);
				CS$<>8__locals1.cNKMLua.GetData("m_SeeTarget", ref this.m_SeeTarget);
				CS$<>8__locals1.cNKMLua.GetData("m_bSeeMoreEnemy", ref this.m_bSeeMoreEnemy);
				CS$<>8__locals1.cNKMLua.GetData("m_SeeRange", ref this.m_SeeRange);
				CS$<>8__locals1.cNKMLua.GetData("m_SeeRangeMax", ref this.m_SeeRangeMax);
				CS$<>8__locals1.cNKMLua.GetData("m_FindTargetTime", ref this.m_FindTargetTime);
				CS$<>8__locals1.cNKMLua.GetData("m_bTargetNoChange", ref this.m_bTargetNoChange);
				CS$<>8__locals1.cNKMLua.GetData("m_bNoBackTarget", ref this.m_bNoBackTarget);
				CS$<>8__locals1.cNKMLua.GetData("m_AttackedTargetTime", ref this.m_AttackedTargetTime);
				CS$<>8__locals1.cNKMLua.GetData("m_bAttackedTarget", ref this.m_bAttackedTarget);
				CS$<>8__locals1.cNKMLua.GetData("m_TargetNearRange", ref this.m_TargetNearRange);
				if (this.m_TargetNearRange <= 0f)
				{
					Log.Error(this.m_UnitTempletBase.m_UnitStrID + " m_TargetNearRange must over zero", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitTemplet.cs", 373);
				}
				CS$<>8__locals1.cNKMLua.GetData("m_fPatrolRange", ref this.m_fPatrolRange);
				CS$<>8__locals1.cNKMLua.GetData("m_PenetrateDefence", ref this.m_PenetrateDefence);
				CS$<>8__locals1.cNKMLua.GetData("m_StateChangeSilence", ref this.m_StateChangeSilence);
				CS$<>8__locals1.cNKMLua.GetData("m_StateChangeStun", ref this.m_StateChangeStun);
				CS$<>8__locals1.cNKMLua.GetData("m_StateChangeSleep", ref this.m_StateChangeSleep);
				CS$<>8__locals1.cNKMLua.GetData("m_StateChangeConfuse", ref this.m_StateChangeConfuse);
				CS$<>8__locals1.cNKMLua.GetData("m_MapPosOverStatePos", ref this.m_MapPosOverStatePos);
				CS$<>8__locals1.cNKMLua.GetData("m_MapPosOverState", ref this.m_MapPosOverState);
				CS$<>8__locals1.cNKMLua.GetDataListEnum<NKM_UNIT_STATUS_EFFECT>("m_listFixedStatusEffect", this.m_listFixedStatusEffect, false);
				CS$<>8__locals1.cNKMLua.GetDataListEnum<NKM_UNIT_STATUS_EFFECT>("m_listFixedStatusImmune", this.m_listFixedStatusImmune, false);
				HashSet<NKM_UNIT_STATUS_EFFECT> hashSet = new HashSet<NKM_UNIT_STATUS_EFFECT>();
				HashSet<NKM_UNIT_STATUS_EFFECT> hashSet2 = new HashSet<NKM_UNIT_STATUS_EFFECT>();
				CS$<>8__locals1.cNKMLua.GetDataListEnum<NKM_UNIT_STATUS_EFFECT>("m_listFixedStatusEffectRemove", hashSet, true);
				CS$<>8__locals1.cNKMLua.GetDataListEnum<NKM_UNIT_STATUS_EFFECT>("m_listFixedStatusImmuneRemove", hashSet2, true);
				this.m_listFixedStatusEffect.ExceptWith(hashSet);
				this.m_listFixedStatusImmune.ExceptWith(hashSet2);
				this.<LoadFromLUA>g__LowerCompability|95_0("m_bNoHeal", NKM_UNIT_STATUS_EFFECT.NUSE_NOHEAL, this.m_listFixedStatusEffect, ref CS$<>8__locals1);
				this.<LoadFromLUA>g__LowerCompability|95_0("m_bImmuneSleep", NKM_UNIT_STATUS_EFFECT.NUSE_SLEEP, this.m_listFixedStatusImmune, ref CS$<>8__locals1);
				this.<LoadFromLUA>g__LowerCompability|95_0("m_bImmuneStun", NKM_UNIT_STATUS_EFFECT.NUSE_STUN, this.m_listFixedStatusImmune, ref CS$<>8__locals1);
				this.<LoadFromLUA>g__LowerCompability|95_0("m_bMetalSlime", NKM_UNIT_STATUS_EFFECT.NUSE_IRONWALL, this.m_listFixedStatusEffect, ref CS$<>8__locals1);
				this.<LoadFromLUA>g__LowerCompability|95_0("m_bNoDamageBackSpeed", NKM_UNIT_STATUS_EFFECT.NUSE_NO_DAMAGE_BACK_SPEED, this.m_listFixedStatusEffect, ref CS$<>8__locals1);
				if (CS$<>8__locals1.cNKMLua.OpenTable("m_listAccumStateChangePack"))
				{
					int num = 1;
					while (CS$<>8__locals1.cNKMLua.OpenTable(num))
					{
						NKMAccumStateChangePack nkmaccumStateChangePack;
						if (this.m_listAccumStateChangePack.Count < num)
						{
							nkmaccumStateChangePack = new NKMAccumStateChangePack();
							this.m_listAccumStateChangePack.Add(nkmaccumStateChangePack);
						}
						else
						{
							nkmaccumStateChangePack = this.m_listAccumStateChangePack[num - 1];
						}
						nkmaccumStateChangePack.LoadFromLUA(CS$<>8__locals1.cNKMLua);
						num++;
						CS$<>8__locals1.cNKMLua.CloseTable();
					}
					CS$<>8__locals1.cNKMLua.CloseTable();
				}
				if (CS$<>8__locals1.cNKMLua.OpenTable("m_listHitFeedBack"))
				{
					int num2 = 1;
					while (CS$<>8__locals1.cNKMLua.OpenTable(num2))
					{
						NKMHitFeedBack nkmhitFeedBack;
						if (this.m_listHitFeedBack.Count < num2)
						{
							nkmhitFeedBack = new NKMHitFeedBack();
							this.m_listHitFeedBack.Add(nkmhitFeedBack);
						}
						else
						{
							nkmhitFeedBack = this.m_listHitFeedBack[num2 - 1];
						}
						nkmhitFeedBack.LoadFromLUA(CS$<>8__locals1.cNKMLua);
						num2++;
						CS$<>8__locals1.cNKMLua.CloseTable();
					}
					CS$<>8__locals1.cNKMLua.CloseTable();
				}
				if (CS$<>8__locals1.cNKMLua.OpenTable("m_listHitCriticalFeedBack"))
				{
					int num3 = 1;
					while (CS$<>8__locals1.cNKMLua.OpenTable(num3))
					{
						NKMHitCriticalFeedBack nkmhitCriticalFeedBack;
						if (this.m_listHitCriticalFeedBack.Count < num3)
						{
							nkmhitCriticalFeedBack = new NKMHitCriticalFeedBack();
							this.m_listHitCriticalFeedBack.Add(nkmhitCriticalFeedBack);
						}
						else
						{
							nkmhitCriticalFeedBack = this.m_listHitCriticalFeedBack[num3 - 1];
						}
						nkmhitCriticalFeedBack.LoadFromLUA(CS$<>8__locals1.cNKMLua);
						num3++;
						CS$<>8__locals1.cNKMLua.CloseTable();
					}
					CS$<>8__locals1.cNKMLua.CloseTable();
				}
				if (CS$<>8__locals1.cNKMLua.OpenTable("m_listHitEvadeFeedBack"))
				{
					int num4 = 1;
					while (CS$<>8__locals1.cNKMLua.OpenTable(num4))
					{
						NKMHitEvadeFeedBack nkmhitEvadeFeedBack;
						if (this.m_listHitEvadeFeedBack.Count < num4)
						{
							nkmhitEvadeFeedBack = new NKMHitEvadeFeedBack();
							this.m_listHitEvadeFeedBack.Add(nkmhitEvadeFeedBack);
						}
						else
						{
							nkmhitEvadeFeedBack = this.m_listHitEvadeFeedBack[num4 - 1];
						}
						nkmhitEvadeFeedBack.LoadFromLUA(CS$<>8__locals1.cNKMLua);
						num4++;
						CS$<>8__locals1.cNKMLua.CloseTable();
					}
					CS$<>8__locals1.cNKMLua.CloseTable();
				}
				if (CS$<>8__locals1.cNKMLua.OpenTable("m_listKillFeedBack"))
				{
					int num5 = 1;
					while (CS$<>8__locals1.cNKMLua.OpenTable(num5))
					{
						NKMKillFeedBack nkmkillFeedBack;
						if (this.m_listKillFeedBack.Count < num5)
						{
							nkmkillFeedBack = new NKMKillFeedBack();
							this.m_listKillFeedBack.Add(nkmkillFeedBack);
						}
						else
						{
							nkmkillFeedBack = this.m_listKillFeedBack[num5 - 1];
						}
						nkmkillFeedBack.LoadFromLUA(CS$<>8__locals1.cNKMLua);
						num5++;
						CS$<>8__locals1.cNKMLua.CloseTable();
					}
					CS$<>8__locals1.cNKMLua.CloseTable();
				}
				if (CS$<>8__locals1.cNKMLua.OpenTable("m_listBuffUnitDieEvent"))
				{
					int num6 = 1;
					while (CS$<>8__locals1.cNKMLua.OpenTable(num6))
					{
						NKMBuffUnitDieEvent nkmbuffUnitDieEvent;
						if (this.m_listBuffUnitDieEvent.Count < num6)
						{
							nkmbuffUnitDieEvent = new NKMBuffUnitDieEvent();
							this.m_listBuffUnitDieEvent.Add(nkmbuffUnitDieEvent);
						}
						else
						{
							nkmbuffUnitDieEvent = this.m_listBuffUnitDieEvent[num6 - 1];
						}
						nkmbuffUnitDieEvent.LoadFromLUA(CS$<>8__locals1.cNKMLua);
						num6++;
						CS$<>8__locals1.cNKMLua.CloseTable();
					}
					CS$<>8__locals1.cNKMLua.CloseTable();
				}
				if (CS$<>8__locals1.cNKMLua.OpenTable("m_listReflectionBuffData"))
				{
					int num7 = 1;
					while (CS$<>8__locals1.cNKMLua.OpenTable(num7))
					{
						NKMStaticBuffData nkmstaticBuffData;
						if (this.m_listReflectionBuffData.Count < num7)
						{
							nkmstaticBuffData = new NKMStaticBuffData();
							this.m_listReflectionBuffData.Add(nkmstaticBuffData);
						}
						else
						{
							nkmstaticBuffData = this.m_listReflectionBuffData[num7 - 1];
						}
						nkmstaticBuffData.LoadFromLUA(CS$<>8__locals1.cNKMLua);
						num7++;
						CS$<>8__locals1.cNKMLua.CloseTable();
					}
					CS$<>8__locals1.cNKMLua.CloseTable();
				}
				if (CS$<>8__locals1.cNKMLua.OpenTable("m_listStaticBuffData"))
				{
					int num8 = 1;
					while (CS$<>8__locals1.cNKMLua.OpenTable(num8))
					{
						NKMStaticBuffData nkmstaticBuffData2;
						if (this.m_listStaticBuffData.Count < num8)
						{
							nkmstaticBuffData2 = new NKMStaticBuffData();
							this.m_listStaticBuffData.Add(nkmstaticBuffData2);
						}
						else
						{
							nkmstaticBuffData2 = this.m_listStaticBuffData[num8 - 1];
						}
						nkmstaticBuffData2.LoadFromLUA(CS$<>8__locals1.cNKMLua);
						num8++;
						CS$<>8__locals1.cNKMLua.CloseTable();
					}
					CS$<>8__locals1.cNKMLua.CloseTable();
				}
				if (CS$<>8__locals1.cNKMLua.OpenTable("m_listPhaseChangeData"))
				{
					int num9 = 1;
					while (CS$<>8__locals1.cNKMLua.OpenTable(num9))
					{
						NKMPhaseChangeData nkmphaseChangeData;
						if (this.m_listPhaseChangeData.Count < num9)
						{
							nkmphaseChangeData = new NKMPhaseChangeData();
							this.m_listPhaseChangeData.Add(nkmphaseChangeData);
						}
						else
						{
							nkmphaseChangeData = this.m_listPhaseChangeData[num9 - 1];
						}
						nkmphaseChangeData.LoadFromLUA(CS$<>8__locals1.cNKMLua);
						num9++;
						CS$<>8__locals1.cNKMLua.CloseTable();
					}
					CS$<>8__locals1.cNKMLua.CloseTable();
				}
				this.<LoadFromLUA>g__OpenCommonStateData|95_1("m_listStartStateData", ref this.m_listStartStateData, ref CS$<>8__locals1);
				this.<LoadFromLUA>g__OpenCommonStateData|95_1("m_listStandStateData", ref this.m_listStandStateData, ref CS$<>8__locals1);
				this.<LoadFromLUA>g__OpenCommonStateData|95_1("m_listRunStateData", ref this.m_listRunStateData, ref CS$<>8__locals1);
				NKMUnitTemplet.<>c__DisplayClass95_1 CS$<>8__locals2;
				CS$<>8__locals2.dicAttackStateType = new Dictionary<string, NKM_UNIT_ATTACK_STATE_TYPE>();
				this.<LoadFromLUA>g__OpenAttackStateData|95_2(NKM_UNIT_ATTACK_STATE_TYPE.ATTACK, "m_listAttackStateData", ref this.m_listAttackStateData, ref CS$<>8__locals1, ref CS$<>8__locals2);
				this.<LoadFromLUA>g__OpenAttackStateData|95_2(NKM_UNIT_ATTACK_STATE_TYPE.AIR_ATTACK, "m_listAirAttackStateData", ref this.m_listAirAttackStateData, ref CS$<>8__locals1, ref CS$<>8__locals2);
				this.<LoadFromLUA>g__OpenAttackStateData|95_2(NKM_UNIT_ATTACK_STATE_TYPE.SKILL, "m_listSkillStateData", ref this.m_listSkillStateData, ref CS$<>8__locals1, ref CS$<>8__locals2);
				this.<LoadFromLUA>g__OpenAttackStateData|95_2(NKM_UNIT_ATTACK_STATE_TYPE.AIR_SKILL, "m_listAirSkillStateData", ref this.m_listAirSkillStateData, ref CS$<>8__locals1, ref CS$<>8__locals2);
				this.<LoadFromLUA>g__OpenAttackStateData|95_2(NKM_UNIT_ATTACK_STATE_TYPE.HYPER, "m_listHyperSkillStateData", ref this.m_listHyperSkillStateData, ref CS$<>8__locals1, ref CS$<>8__locals2);
				this.<LoadFromLUA>g__OpenAttackStateData|95_2(NKM_UNIT_ATTACK_STATE_TYPE.AIR_HYPER, "m_listAirHyperSkillStateData", ref this.m_listAirHyperSkillStateData, ref CS$<>8__locals1, ref CS$<>8__locals2);
				this.<LoadFromLUA>g__OpenAttackStateData|95_2(NKM_UNIT_ATTACK_STATE_TYPE.PASSIVE, "m_listPassiveAttackStateData", ref this.m_listPassiveAttackStateData, ref CS$<>8__locals1, ref CS$<>8__locals2);
				this.<LoadFromLUA>g__OpenAttackStateData|95_2(NKM_UNIT_ATTACK_STATE_TYPE.AIR_PASSIVE, "m_listAirPassiveAttackStateData", ref this.m_listAirPassiveAttackStateData, ref CS$<>8__locals1, ref CS$<>8__locals2);
				this.<LoadFromLUA>g__AddAttackState|95_3(NKM_UNIT_ATTACK_STATE_TYPE.ATTACK, "m_AttackStateData", ref this.m_listAttackStateData, ref CS$<>8__locals1, ref CS$<>8__locals2);
				this.<LoadFromLUA>g__AddAttackState|95_3(NKM_UNIT_ATTACK_STATE_TYPE.AIR_ATTACK, "m_AirAttackStateData", ref this.m_listAirAttackStateData, ref CS$<>8__locals1, ref CS$<>8__locals2);
				this.<LoadFromLUA>g__AddAttackState|95_3(NKM_UNIT_ATTACK_STATE_TYPE.SKILL, "m_SkillStateData", ref this.m_listSkillStateData, ref CS$<>8__locals1, ref CS$<>8__locals2);
				this.<LoadFromLUA>g__AddAttackState|95_3(NKM_UNIT_ATTACK_STATE_TYPE.AIR_SKILL, "m_AirSkillStateData", ref this.m_listAirSkillStateData, ref CS$<>8__locals1, ref CS$<>8__locals2);
				this.<LoadFromLUA>g__AddAttackState|95_3(NKM_UNIT_ATTACK_STATE_TYPE.HYPER, "m_HyperSkillStateData", ref this.m_listHyperSkillStateData, ref CS$<>8__locals1, ref CS$<>8__locals2);
				this.<LoadFromLUA>g__AddAttackState|95_3(NKM_UNIT_ATTACK_STATE_TYPE.AIR_HYPER, "m_AirHyperSkillStateData", ref this.m_listAirHyperSkillStateData, ref CS$<>8__locals1, ref CS$<>8__locals2);
				this.<LoadFromLUA>g__AddAttackState|95_3(NKM_UNIT_ATTACK_STATE_TYPE.PASSIVE, "m_PassiveAttackStateData", ref this.m_listPassiveAttackStateData, ref CS$<>8__locals1, ref CS$<>8__locals2);
				this.<LoadFromLUA>g__AddAttackState|95_3(NKM_UNIT_ATTACK_STATE_TYPE.AIR_PASSIVE, "m_AirPassiveAttackStateData", ref this.m_listAirPassiveAttackStateData, ref CS$<>8__locals1, ref CS$<>8__locals2);
				CS$<>8__locals1.cNKMLua.GetData("m_DyingState", ref this.m_DyingState);
				if (CS$<>8__locals1.cNKMLua.OpenTable("m_dicNKMUnitState"))
				{
					int num10 = 1;
					while (CS$<>8__locals1.cNKMLua.OpenTable(num10))
					{
						string key = "";
						CS$<>8__locals1.cNKMLua.GetData("m_StateName", ref key);
						NKMUnitState nkmunitState;
						if (this.m_dicNKMUnitState.ContainsKey(key))
						{
							nkmunitState = this.m_dicNKMUnitState[key];
						}
						else
						{
							nkmunitState = new NKMUnitState();
						}
						nkmunitState.LoadFromLUA(CS$<>8__locals1.cNKMLua, cNKMUnitTempletBase);
						if (!this.m_dicNKMUnitState.ContainsKey(nkmunitState.m_StateName))
						{
							this.m_dicNKMUnitState.Add(nkmunitState.m_StateName, nkmunitState);
						}
						num10++;
						CS$<>8__locals1.cNKMLua.CloseTable();
					}
					CS$<>8__locals1.cNKMLua.CloseTable();
					num10 = 1;
					int num11 = this.m_dicNKMUnitStateID.Count + 10;
					this.m_dicNKMUnitStateID.Clear();
					this.m_dicAttackStateType.Clear();
					foreach (KeyValuePair<string, NKMUnitState> keyValuePair in this.m_dicNKMUnitState)
					{
						NKMUnitState value = keyValuePair.Value;
						if (value.m_StateID == 0)
						{
							value.m_StateID = (byte)(num11 + num10);
						}
						if (!this.m_dicNKMUnitStateID.ContainsKey((short)value.m_StateID))
						{
							this.m_dicNKMUnitStateID.Add((short)value.m_StateID, value);
						}
						else
						{
							Log.Error("m_dicNKMUnitStateID Duplicate: " + this.m_UnitTempletBase.m_UnitStrID + " : " + value.m_StateName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitTemplet.cs", 761);
						}
						if (CS$<>8__locals2.dicAttackStateType.ContainsKey(value.m_StateName))
						{
							this.m_dicAttackStateType.Add((int)value.m_StateID, CS$<>8__locals2.dicAttackStateType[value.m_StateName]);
						}
						num10++;
					}
				}
				NKMFindTargetData.LoadFromLUA(CS$<>8__locals1.cNKMLua, "m_SubTargetFindData", out this.m_SubTargetFindData);
				if (NKMCommonConst.USE_ROLLBACK && !this.m_UnitTempletBase.m_bMonster && !this.m_UnitTempletBase.IsShip())
				{
					NKMUnitState unitState = this.GetUnitState("USN_START", true);
					if (unitState != null)
					{
						if (unitState.m_bRun)
						{
							Log.Error("유닛 " + this.m_UnitTempletBase.m_UnitStrID + " 스타트 스테이트 USN_START : m_bRun = true", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitTemplet.cs", 783);
						}
						this.m_fMaxRollbackTime = unitState.CalcaluateMaxRollbackTime(this.m_UnitTempletBase.m_UnitStrID);
					}
					else
					{
						this.m_fMaxRollbackTime = 0f;
					}
				}
				result = true;
			}
			catch (Exception ex)
			{
				NKMTempletError.Add("[UnitTemplet] skill state 로딩 오류. exception:" + ex.Message, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitTemplet.cs", 798);
				result = false;
			}
			return result;
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x000B9C08 File Offset: 0x000B7E08
		public void DeepCopyFromSource(NKMUnitTemplet source)
		{
			this.m_SpriteScale = source.m_SpriteScale;
			this.m_SpriteOffsetX = source.m_SpriteOffsetX;
			this.m_SpriteOffsetY = source.m_SpriteOffsetY;
			this.m_fForceRespawnXpos = source.m_fForceRespawnXpos;
			this.m_fForceRespawnZposMin = source.m_fForceRespawnZposMin;
			this.m_fForceRespawnZposMax = source.m_fForceRespawnZposMax;
			this.m_UnitSizeX = source.m_UnitSizeX;
			this.m_UnitSizeY = source.m_UnitSizeY;
			this.m_NKC_TEAM_COLOR_TYPE = source.m_NKC_TEAM_COLOR_TYPE;
			this.m_fShadowOffsetX = source.m_fShadowOffsetX;
			this.m_fShadowOffsetY = source.m_fShadowOffsetY;
			this.m_fShadowScaleX = source.m_fShadowScaleX;
			this.m_fShadowScaleY = source.m_fShadowScaleY;
			this.m_fShadowRotateX = source.m_fShadowRotateX;
			this.m_fShadowRotateZ = source.m_fShadowRotateZ;
			this.m_fBuffEffectScaleFactor = source.m_fBuffEffectScaleFactor;
			this.m_bShowGage = source.m_bShowGage;
			this.m_bGageSmall = source.m_bGageSmall;
			this.m_fGageOffsetX = source.m_fGageOffsetX;
			this.m_fGageOffsetY = source.m_fGageOffsetY;
			this.m_fRespawnCoolTime = source.m_fRespawnCoolTime;
			this.m_fDieCompleteTime = source.m_fDieCompleteTime;
			this.m_bDieDeActive = source.m_bDieDeActive;
			this.m_bUseMotionBlur = source.m_bUseMotionBlur;
			this.m_bNoDamageState = source.m_bNoDamageState;
			this.m_bNoDamageDownState = source.m_bNoDamageDownState;
			this.m_bNoDamageStopTime = source.m_bNoDamageStopTime;
			this.m_Invincible = source.m_Invincible;
			this.m_SuperArmorLevel = source.m_SuperArmorLevel;
			this.m_ColorR = source.m_ColorR;
			this.m_ColorG = source.m_ColorG;
			this.m_ColorB = source.m_ColorB;
			this.m_fAirHigh = source.m_fAirHigh;
			this.m_bNoMove = source.m_bNoMove;
			this.m_bNoRun = source.m_bNoRun;
			this.m_SpeedRun = source.m_SpeedRun;
			this.m_SpeedJump = source.m_SpeedJump;
			this.m_fReloadAccel = source.m_fReloadAccel;
			this.m_fGAccel = source.m_fGAccel;
			this.m_fMaxGSpeed = source.m_fMaxGSpeed;
			this.m_fDamageBackFactor = source.m_fDamageBackFactor;
			this.m_fDamageUpFactor = source.m_fDamageUpFactor;
			this.m_bNoMapLimit = source.m_bNoMapLimit;
			this.m_SeeTarget = source.m_SeeTarget;
			this.m_bSeeMoreEnemy = source.m_bSeeMoreEnemy;
			this.m_SeeRange = source.m_SeeRange;
			this.m_SeeRangeMax = source.m_SeeRangeMax;
			this.m_FindTargetTime = source.m_FindTargetTime;
			this.m_bTargetNoChange = source.m_bTargetNoChange;
			this.m_bNoBackTarget = source.m_bNoBackTarget;
			this.m_AttackedTargetTime = source.m_AttackedTargetTime;
			this.m_bAttackedTarget = source.m_bAttackedTarget;
			this.m_TargetNearRange = source.m_TargetNearRange;
			this.m_fPatrolRange = source.m_fPatrolRange;
			this.m_PenetrateDefence = source.m_PenetrateDefence;
			this.m_StateChangeSilence = source.m_StateChangeSilence;
			this.m_StateChangeStun = source.m_StateChangeStun;
			this.m_StateChangeSleep = source.m_StateChangeSleep;
			this.m_StateChangeConfuse = source.m_StateChangeConfuse;
			this.m_MapPosOverStatePos = source.m_MapPosOverStatePos;
			this.m_MapPosOverState = source.m_MapPosOverState;
			this.m_listFixedStatusEffect.Clear();
			this.m_listFixedStatusEffect.UnionWith(source.m_listFixedStatusEffect);
			this.m_listFixedStatusImmune.Clear();
			this.m_listFixedStatusImmune.UnionWith(source.m_listFixedStatusImmune);
			this.m_listAccumStateChangePack.Clear();
			for (int i = 0; i < source.m_listAccumStateChangePack.Count; i++)
			{
				NKMAccumStateChangePack nkmaccumStateChangePack = new NKMAccumStateChangePack();
				nkmaccumStateChangePack.DeepCopyFromSource(source.m_listAccumStateChangePack[i]);
				this.m_listAccumStateChangePack.Add(nkmaccumStateChangePack);
			}
			this.m_listHitFeedBack.Clear();
			for (int j = 0; j < source.m_listHitFeedBack.Count; j++)
			{
				NKMHitFeedBack nkmhitFeedBack = new NKMHitFeedBack();
				nkmhitFeedBack.DeepCopyFromSource(source.m_listHitFeedBack[j]);
				this.m_listHitFeedBack.Add(nkmhitFeedBack);
			}
			this.m_listHitCriticalFeedBack.Clear();
			for (int k = 0; k < source.m_listHitCriticalFeedBack.Count; k++)
			{
				NKMHitCriticalFeedBack nkmhitCriticalFeedBack = new NKMHitCriticalFeedBack();
				nkmhitCriticalFeedBack.DeepCopyFromSource(source.m_listHitCriticalFeedBack[k]);
				this.m_listHitCriticalFeedBack.Add(nkmhitCriticalFeedBack);
			}
			this.m_listHitEvadeFeedBack.Clear();
			for (int l = 0; l < source.m_listHitEvadeFeedBack.Count; l++)
			{
				NKMHitEvadeFeedBack nkmhitEvadeFeedBack = new NKMHitEvadeFeedBack();
				nkmhitEvadeFeedBack.DeepCopyFromSource(source.m_listHitEvadeFeedBack[l]);
				this.m_listHitEvadeFeedBack.Add(nkmhitEvadeFeedBack);
			}
			this.m_listKillFeedBack.Clear();
			for (int m = 0; m < source.m_listKillFeedBack.Count; m++)
			{
				NKMKillFeedBack nkmkillFeedBack = new NKMKillFeedBack();
				nkmkillFeedBack.DeepCopyFromSource(source.m_listKillFeedBack[m]);
				this.m_listKillFeedBack.Add(nkmkillFeedBack);
			}
			this.m_listBuffUnitDieEvent.Clear();
			for (int n = 0; n < source.m_listBuffUnitDieEvent.Count; n++)
			{
				NKMBuffUnitDieEvent nkmbuffUnitDieEvent = new NKMBuffUnitDieEvent();
				nkmbuffUnitDieEvent.DeepCopyFromSource(source.m_listBuffUnitDieEvent[n]);
				this.m_listBuffUnitDieEvent.Add(nkmbuffUnitDieEvent);
			}
			this.m_listReflectionBuffData.Clear();
			for (int num = 0; num < source.m_listReflectionBuffData.Count; num++)
			{
				NKMStaticBuffData nkmstaticBuffData = new NKMStaticBuffData();
				nkmstaticBuffData.DeepCopyFromSource(source.m_listReflectionBuffData[num]);
				this.m_listReflectionBuffData.Add(nkmstaticBuffData);
			}
			this.m_listStaticBuffData.Clear();
			for (int num2 = 0; num2 < source.m_listStaticBuffData.Count; num2++)
			{
				NKMStaticBuffData nkmstaticBuffData2 = new NKMStaticBuffData();
				nkmstaticBuffData2.DeepCopyFromSource(source.m_listStaticBuffData[num2]);
				this.m_listStaticBuffData.Add(nkmstaticBuffData2);
			}
			this.m_listPhaseChangeData.Clear();
			for (int num3 = 0; num3 < source.m_listPhaseChangeData.Count; num3++)
			{
				NKMPhaseChangeData nkmphaseChangeData = new NKMPhaseChangeData();
				nkmphaseChangeData.DeepCopyFromSource(source.m_listPhaseChangeData[num3]);
				this.m_listPhaseChangeData.Add(nkmphaseChangeData);
			}
			NKMUnitTemplet.<DeepCopyFromSource>g__DeepCopyCommonState|96_0(ref this.m_listStartStateData, source.m_listStartStateData);
			NKMUnitTemplet.<DeepCopyFromSource>g__DeepCopyCommonState|96_0(ref this.m_listStandStateData, source.m_listStandStateData);
			NKMUnitTemplet.<DeepCopyFromSource>g__DeepCopyCommonState|96_0(ref this.m_listRunStateData, source.m_listRunStateData);
			NKMUnitTemplet.<DeepCopyFromSource>g__DeepCopyAttackState|96_1(ref this.m_listAttackStateData, source.m_listAttackStateData);
			NKMUnitTemplet.<DeepCopyFromSource>g__DeepCopyAttackState|96_1(ref this.m_listAirAttackStateData, source.m_listAirAttackStateData);
			NKMUnitTemplet.<DeepCopyFromSource>g__DeepCopyAttackState|96_1(ref this.m_listSkillStateData, source.m_listSkillStateData);
			NKMUnitTemplet.<DeepCopyFromSource>g__DeepCopyAttackState|96_1(ref this.m_listAirSkillStateData, source.m_listAirSkillStateData);
			NKMUnitTemplet.<DeepCopyFromSource>g__DeepCopyAttackState|96_1(ref this.m_listHyperSkillStateData, source.m_listHyperSkillStateData);
			NKMUnitTemplet.<DeepCopyFromSource>g__DeepCopyAttackState|96_1(ref this.m_listAirHyperSkillStateData, source.m_listAirHyperSkillStateData);
			NKMUnitTemplet.<DeepCopyFromSource>g__DeepCopyAttackState|96_1(ref this.m_listPassiveAttackStateData, source.m_listPassiveAttackStateData);
			NKMUnitTemplet.<DeepCopyFromSource>g__DeepCopyAttackState|96_1(ref this.m_listAirPassiveAttackStateData, source.m_listAirPassiveAttackStateData);
			this.m_dicAttackStateType.Clear();
			foreach (KeyValuePair<int, NKM_UNIT_ATTACK_STATE_TYPE> keyValuePair in source.m_dicAttackStateType)
			{
				this.m_dicAttackStateType.Add(keyValuePair.Key, keyValuePair.Value);
			}
			this.m_DyingState = source.m_DyingState;
			foreach (KeyValuePair<string, NKMUnitState> keyValuePair2 in source.m_dicNKMUnitState)
			{
				NKMUnitState value = keyValuePair2.Value;
				if (this.m_dicNKMUnitStateID.ContainsKey((short)value.m_StateID))
				{
					this.m_dicNKMUnitStateID[(short)value.m_StateID].DeepCopyFromSource(value);
				}
				else
				{
					NKMUnitState nkmunitState = new NKMUnitState();
					nkmunitState.DeepCopyFromSource(value);
					this.m_dicNKMUnitState.Add(nkmunitState.m_StateName, nkmunitState);
					this.m_dicNKMUnitStateID.Add((short)nkmunitState.m_StateID, nkmunitState);
				}
			}
			this.m_fMaxRollbackTime = source.m_fMaxRollbackTime;
			NKMFindTargetData.DeepCopyFrom(source.m_SubTargetFindData, out this.m_SubTargetFindData);
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x000BA3B0 File Offset: 0x000B85B0
		public NKMUnitState GetUnitState(string stateName, bool bLog = true)
		{
			if (this.m_dicNKMUnitState.ContainsKey(stateName))
			{
				return this.m_dicNKMUnitState[stateName];
			}
			if (bLog)
			{
				NKMUnitTemplet.stateErrorCallback(this, stateName);
			}
			return null;
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x000BA3DD File Offset: 0x000B85DD
		public NKMUnitState GetUnitState(short stateID)
		{
			if (this.m_dicNKMUnitStateID.ContainsKey(stateID))
			{
				return this.m_dicNKMUnitStateID[stateID];
			}
			Log.Error("No Exist StateID: " + stateID.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitTemplet.cs", 1054);
			return null;
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x000BA41C File Offset: 0x000B861C
		public void SetCoolTimeLink()
		{
			this.SetCoolTimeLink(this.m_listAttackStateData, this.m_listAttackStateData, true);
			this.SetCoolTimeLink(this.m_listAttackStateData, this.m_listAirAttackStateData, false);
			this.SetCoolTimeLink(this.m_listAirAttackStateData, this.m_listAirAttackStateData, true);
			this.SetCoolTimeLink(this.m_listAirAttackStateData, this.m_listAttackStateData, false);
			this.SetCoolTimeLink(this.m_listSkillStateData, this.m_listSkillStateData, true);
			this.SetCoolTimeLink(this.m_listSkillStateData, this.m_listAirSkillStateData, false);
			this.SetCoolTimeLink(this.m_listAirSkillStateData, this.m_listAirSkillStateData, true);
			this.SetCoolTimeLink(this.m_listAirSkillStateData, this.m_listSkillStateData, false);
			this.SetCoolTimeLink(this.m_listHyperSkillStateData, this.m_listHyperSkillStateData, true);
			this.SetCoolTimeLink(this.m_listHyperSkillStateData, this.m_listAirHyperSkillStateData, false);
			this.SetCoolTimeLink(this.m_listAirHyperSkillStateData, this.m_listAirHyperSkillStateData, true);
			this.SetCoolTimeLink(this.m_listAirHyperSkillStateData, this.m_listHyperSkillStateData, false);
			this.SetCoolTimeLink(this.m_listPassiveAttackStateData, this.m_listPassiveAttackStateData, true);
			this.SetCoolTimeLink(this.m_listPassiveAttackStateData, this.m_listAirPassiveAttackStateData, false);
			this.SetCoolTimeLink(this.m_listAirPassiveAttackStateData, this.m_listAirPassiveAttackStateData, true);
			this.SetCoolTimeLink(this.m_listAirPassiveAttackStateData, this.m_listPassiveAttackStateData, false);
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x000BA55C File Offset: 0x000B875C
		public void SetCoolTimeLink(List<NKMAttackStateData> listAttackStateDataDest, List<NKMAttackStateData> listAttackStateDataSrc, bool bClear = true)
		{
			for (int i = 0; i < listAttackStateDataDest.Count; i++)
			{
				NKMAttackStateData nkmattackStateData = listAttackStateDataDest[i];
				NKMUnitState unitState = this.GetUnitState(nkmattackStateData.m_StateName, true);
				if (unitState != null)
				{
					if (bClear)
					{
						unitState.m_listCoolTimeLink.Clear();
					}
					for (int j = 0; j < listAttackStateDataSrc.Count; j++)
					{
						NKMAttackStateData nkmattackStateData2 = listAttackStateDataSrc[j];
						NKMUnitState unitState2 = this.GetUnitState(nkmattackStateData2.m_StateName, true);
						if (unitState2 != null && unitState.m_StateID != unitState2.m_StateID)
						{
							unitState.m_listCoolTimeLink.Add(unitState2.m_StateName);
							unitState2.m_StateCoolTime = unitState.m_StateCoolTime;
						}
					}
				}
			}
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x000BA603 File Offset: 0x000B8803
		public float GetNearTargetRandomRange()
		{
			return this.m_TargetNearRange;
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x000BA60B File Offset: 0x000B880B
		public static void HookStateErrorHandler(Action<NKMUnitTemplet, string> handler)
		{
			NKMUnitTemplet.stateErrorCallback = handler;
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x000BA613 File Offset: 0x000B8813
		private static void DefaultStateErrorHandler(NKMUnitTemplet templet, string stateName)
		{
			Log.Error("unit state not found. unitName:" + templet.m_UnitTempletBase.m_UnitStrID + " stateName:" + stateName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitTemplet.cs", 1129);
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x000BA654 File Offset: 0x000B8854
		[CompilerGenerated]
		private void <LoadFromLUA>g__LowerCompability|95_0(string name, NKM_UNIT_STATUS_EFFECT type, HashSet<NKM_UNIT_STATUS_EFFECT> hsTarget, ref NKMUnitTemplet.<>c__DisplayClass95_0 A_4)
		{
			bool flag = false;
			if (A_4.cNKMLua.GetData(name, ref flag))
			{
				if (flag)
				{
					hsTarget.Add(type);
					return;
				}
				hsTarget.Remove(type);
			}
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x000BA688 File Offset: 0x000B8888
		[CompilerGenerated]
		private void <LoadFromLUA>g__OpenCommonStateData|95_1(string tableName, ref List<NKMCommonStateData> lstCommonStateData, ref NKMUnitTemplet.<>c__DisplayClass95_0 A_3)
		{
			if (A_3.cNKMLua.OpenTable(tableName))
			{
				int num = 1;
				while (A_3.cNKMLua.OpenTable(num))
				{
					NKMCommonStateData nkmcommonStateData;
					if (lstCommonStateData.Count < num)
					{
						nkmcommonStateData = new NKMCommonStateData();
						lstCommonStateData.Add(nkmcommonStateData);
					}
					else
					{
						nkmcommonStateData = lstCommonStateData[num - 1];
					}
					nkmcommonStateData.LoadFromLUA(A_3.cNKMLua);
					num++;
					A_3.cNKMLua.CloseTable();
				}
				A_3.cNKMLua.CloseTable();
			}
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x000BA708 File Offset: 0x000B8908
		[CompilerGenerated]
		private void <LoadFromLUA>g__OpenAttackStateData|95_2(NKM_UNIT_ATTACK_STATE_TYPE stateType, string tableName, ref List<NKMAttackStateData> lstAttackStateData, ref NKMUnitTemplet.<>c__DisplayClass95_0 A_4, ref NKMUnitTemplet.<>c__DisplayClass95_1 A_5)
		{
			if (A_4.cNKMLua.OpenTable(tableName))
			{
				int num = 1;
				while (A_4.cNKMLua.OpenTable(num))
				{
					NKMAttackStateData nkmattackStateData;
					if (lstAttackStateData.Count < num)
					{
						nkmattackStateData = new NKMAttackStateData();
						lstAttackStateData.Add(nkmattackStateData);
					}
					else
					{
						nkmattackStateData = lstAttackStateData[num - 1];
					}
					nkmattackStateData.LoadFromLUA(A_4.cNKMLua, this.m_TargetNearRange);
					A_5.dicAttackStateType.Add(nkmattackStateData.m_StateName, stateType);
					num++;
					A_4.cNKMLua.CloseTable();
				}
				A_4.cNKMLua.CloseTable();
			}
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x000BA7A4 File Offset: 0x000B89A4
		[CompilerGenerated]
		private void <LoadFromLUA>g__AddAttackState|95_3(NKM_UNIT_ATTACK_STATE_TYPE stateType, string tableName, ref List<NKMAttackStateData> lstAttackStateData, ref NKMUnitTemplet.<>c__DisplayClass95_0 A_4, ref NKMUnitTemplet.<>c__DisplayClass95_1 A_5)
		{
			if (A_4.cNKMLua.OpenTable(tableName))
			{
				NKMAttackStateData nkmattackStateData = new NKMAttackStateData();
				nkmattackStateData.LoadFromLUA(A_4.cNKMLua, this.m_TargetNearRange);
				A_5.dicAttackStateType.Add(nkmattackStateData.m_StateName, stateType);
				lstAttackStateData.Add(nkmattackStateData);
				A_4.cNKMLua.CloseTable();
			}
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x000BA804 File Offset: 0x000B8A04
		[CompilerGenerated]
		internal static void <DeepCopyFromSource>g__DeepCopyCommonState|96_0(ref List<NKMCommonStateData> target, List<NKMCommonStateData> source)
		{
			target.Clear();
			for (int i = 0; i < source.Count; i++)
			{
				NKMCommonStateData nkmcommonStateData = new NKMCommonStateData();
				nkmcommonStateData.DeepCopyFromSource(source[i]);
				target.Add(nkmcommonStateData);
			}
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x000BA844 File Offset: 0x000B8A44
		[CompilerGenerated]
		internal static void <DeepCopyFromSource>g__DeepCopyAttackState|96_1(ref List<NKMAttackStateData> target, List<NKMAttackStateData> source)
		{
			target.Clear();
			for (int i = 0; i < source.Count; i++)
			{
				NKMAttackStateData nkmattackStateData = new NKMAttackStateData();
				nkmattackStateData.DeepCopyFromSource(source[i]);
				target.Add(nkmattackStateData);
			}
		}

		// Token: 0x0400253B RID: 9531
		public NKMUnitTempletBase m_UnitTempletBase;

		// Token: 0x0400253C RID: 9532
		public NKMUnitStatTemplet m_StatTemplet;

		// Token: 0x0400253D RID: 9533
		public float m_SpriteScale = 1f;

		// Token: 0x0400253E RID: 9534
		public float m_SpriteOffsetX;

		// Token: 0x0400253F RID: 9535
		public float m_SpriteOffsetY;

		// Token: 0x04002540 RID: 9536
		public float m_fForceRespawnXpos = -1f;

		// Token: 0x04002541 RID: 9537
		public float m_fForceRespawnZposMin = -1f;

		// Token: 0x04002542 RID: 9538
		public float m_fForceRespawnZposMax = -1f;

		// Token: 0x04002543 RID: 9539
		public float m_UnitSizeX;

		// Token: 0x04002544 RID: 9540
		public float m_UnitSizeY;

		// Token: 0x04002545 RID: 9541
		public NKC_TEAM_COLOR_TYPE m_NKC_TEAM_COLOR_TYPE = NKC_TEAM_COLOR_TYPE.NTCT_FULL;

		// Token: 0x04002546 RID: 9542
		public float m_fShadowOffsetX;

		// Token: 0x04002547 RID: 9543
		public float m_fShadowOffsetY;

		// Token: 0x04002548 RID: 9544
		public float m_fShadowScaleX = 1f;

		// Token: 0x04002549 RID: 9545
		public float m_fShadowScaleY = 1f;

		// Token: 0x0400254A RID: 9546
		public float m_fShadowRotateX;

		// Token: 0x0400254B RID: 9547
		public float m_fShadowRotateZ;

		// Token: 0x0400254C RID: 9548
		public float m_fBuffEffectScaleFactor = 1f;

		// Token: 0x0400254D RID: 9549
		public bool m_bShowGage = true;

		// Token: 0x0400254E RID: 9550
		public bool m_bGageSmall = true;

		// Token: 0x0400254F RID: 9551
		public float m_fGageOffsetX;

		// Token: 0x04002550 RID: 9552
		public float m_fGageOffsetY = 290f;

		// Token: 0x04002551 RID: 9553
		public float m_fRespawnCoolTime = 1f;

		// Token: 0x04002552 RID: 9554
		public float m_fDieCompleteTime = 2f;

		// Token: 0x04002553 RID: 9555
		public bool m_bDieDeActive = true;

		// Token: 0x04002554 RID: 9556
		public bool m_bUseMotionBlur;

		// Token: 0x04002555 RID: 9557
		public bool m_bNoDamageState;

		// Token: 0x04002556 RID: 9558
		public bool m_bNoDamageDownState;

		// Token: 0x04002557 RID: 9559
		public bool m_bNoDamageStopTime;

		// Token: 0x04002558 RID: 9560
		public bool m_Invincible;

		// Token: 0x04002559 RID: 9561
		public NKM_SUPER_ARMOR_LEVEL m_SuperArmorLevel;

		// Token: 0x0400255A RID: 9562
		public float m_ColorR = 1f;

		// Token: 0x0400255B RID: 9563
		public float m_ColorG = 1f;

		// Token: 0x0400255C RID: 9564
		public float m_ColorB = 1f;

		// Token: 0x0400255D RID: 9565
		public float m_fAirHigh;

		// Token: 0x0400255E RID: 9566
		public bool m_bNoMove;

		// Token: 0x0400255F RID: 9567
		public bool m_bNoRun;

		// Token: 0x04002560 RID: 9568
		public float m_SpeedRun;

		// Token: 0x04002561 RID: 9569
		public float m_SpeedJump;

		// Token: 0x04002562 RID: 9570
		public float m_fReloadAccel = 3000f;

		// Token: 0x04002563 RID: 9571
		public float m_fGAccel = 2000f;

		// Token: 0x04002564 RID: 9572
		public float m_fMaxGSpeed = -3000f;

		// Token: 0x04002565 RID: 9573
		public float m_fDamageBackFactor = 1f;

		// Token: 0x04002566 RID: 9574
		public float m_fDamageUpFactor = 1f;

		// Token: 0x04002567 RID: 9575
		public bool m_bNoMapLimit;

		// Token: 0x04002568 RID: 9576
		public bool m_SeeTarget = true;

		// Token: 0x04002569 RID: 9577
		public bool m_bSeeMoreEnemy;

		// Token: 0x0400256A RID: 9578
		public float m_SeeRange;

		// Token: 0x0400256B RID: 9579
		public float m_SeeRangeMax;

		// Token: 0x0400256C RID: 9580
		public float m_FindTargetTime = 0.1f;

		// Token: 0x0400256D RID: 9581
		public bool m_bTargetNoChange;

		// Token: 0x0400256E RID: 9582
		public bool m_bNoBackTarget;

		// Token: 0x0400256F RID: 9583
		public NKMFindTargetData m_SubTargetFindData;

		// Token: 0x04002570 RID: 9584
		public float m_AttackedTargetTime = 1f;

		// Token: 0x04002571 RID: 9585
		public bool m_bAttackedTarget;

		// Token: 0x04002572 RID: 9586
		public float m_TargetNearRange = 1f;

		// Token: 0x04002573 RID: 9587
		public float m_fPatrolRange;

		// Token: 0x04002574 RID: 9588
		public int m_PenetrateDefence;

		// Token: 0x04002575 RID: 9589
		public string m_StateChangeSilence = "";

		// Token: 0x04002576 RID: 9590
		public string m_StateChangeStun = "";

		// Token: 0x04002577 RID: 9591
		public string m_StateChangeSleep = "";

		// Token: 0x04002578 RID: 9592
		public string m_StateChangeConfuse = "";

		// Token: 0x04002579 RID: 9593
		public float m_MapPosOverStatePos;

		// Token: 0x0400257A RID: 9594
		public string m_MapPosOverState = "";

		// Token: 0x0400257B RID: 9595
		public HashSet<NKM_UNIT_STATUS_EFFECT> m_listFixedStatusEffect = new HashSet<NKM_UNIT_STATUS_EFFECT>();

		// Token: 0x0400257C RID: 9596
		public HashSet<NKM_UNIT_STATUS_EFFECT> m_listFixedStatusImmune = new HashSet<NKM_UNIT_STATUS_EFFECT>();

		// Token: 0x0400257D RID: 9597
		public List<NKMAccumStateChangePack> m_listAccumStateChangePack = new List<NKMAccumStateChangePack>();

		// Token: 0x0400257E RID: 9598
		public List<NKMHitFeedBack> m_listHitFeedBack = new List<NKMHitFeedBack>();

		// Token: 0x0400257F RID: 9599
		public List<NKMHitEvadeFeedBack> m_listHitEvadeFeedBack = new List<NKMHitEvadeFeedBack>();

		// Token: 0x04002580 RID: 9600
		public List<NKMHitCriticalFeedBack> m_listHitCriticalFeedBack = new List<NKMHitCriticalFeedBack>();

		// Token: 0x04002581 RID: 9601
		public List<NKMKillFeedBack> m_listKillFeedBack = new List<NKMKillFeedBack>();

		// Token: 0x04002582 RID: 9602
		public List<NKMBuffUnitDieEvent> m_listBuffUnitDieEvent = new List<NKMBuffUnitDieEvent>();

		// Token: 0x04002583 RID: 9603
		public List<NKMStaticBuffData> m_listReflectionBuffData = new List<NKMStaticBuffData>();

		// Token: 0x04002584 RID: 9604
		public List<NKMStaticBuffData> m_listStaticBuffData = new List<NKMStaticBuffData>();

		// Token: 0x04002585 RID: 9605
		public List<NKMPhaseChangeData> m_listPhaseChangeData = new List<NKMPhaseChangeData>();

		// Token: 0x04002586 RID: 9606
		public List<NKMCommonStateData> m_listStartStateData = new List<NKMCommonStateData>();

		// Token: 0x04002587 RID: 9607
		public List<NKMCommonStateData> m_listStandStateData = new List<NKMCommonStateData>();

		// Token: 0x04002588 RID: 9608
		public List<NKMCommonStateData> m_listRunStateData = new List<NKMCommonStateData>();

		// Token: 0x04002589 RID: 9609
		public List<NKMAttackStateData> m_listAttackStateData = new List<NKMAttackStateData>();

		// Token: 0x0400258A RID: 9610
		public List<NKMAttackStateData> m_listAirAttackStateData = new List<NKMAttackStateData>();

		// Token: 0x0400258B RID: 9611
		public List<NKMAttackStateData> m_listSkillStateData = new List<NKMAttackStateData>();

		// Token: 0x0400258C RID: 9612
		public List<NKMAttackStateData> m_listAirSkillStateData = new List<NKMAttackStateData>();

		// Token: 0x0400258D RID: 9613
		public List<NKMAttackStateData> m_listHyperSkillStateData = new List<NKMAttackStateData>();

		// Token: 0x0400258E RID: 9614
		public List<NKMAttackStateData> m_listAirHyperSkillStateData = new List<NKMAttackStateData>();

		// Token: 0x0400258F RID: 9615
		public List<NKMAttackStateData> m_listPassiveAttackStateData = new List<NKMAttackStateData>();

		// Token: 0x04002590 RID: 9616
		public List<NKMAttackStateData> m_listAirPassiveAttackStateData = new List<NKMAttackStateData>();

		// Token: 0x04002591 RID: 9617
		public Dictionary<int, NKM_UNIT_ATTACK_STATE_TYPE> m_dicAttackStateType = new Dictionary<int, NKM_UNIT_ATTACK_STATE_TYPE>();

		// Token: 0x04002592 RID: 9618
		public string m_DyingState = "USN_DAMAGE_DOWN";

		// Token: 0x04002593 RID: 9619
		public Dictionary<string, NKMUnitState> m_dicNKMUnitState = new Dictionary<string, NKMUnitState>();

		// Token: 0x04002594 RID: 9620
		public Dictionary<short, NKMUnitState> m_dicNKMUnitStateID = new Dictionary<short, NKMUnitState>();

		// Token: 0x04002595 RID: 9621
		private static Action<NKMUnitTemplet, string> stateErrorCallback = new Action<NKMUnitTemplet, string>(NKMUnitTemplet.DefaultStateErrorHandler);

		// Token: 0x04002596 RID: 9622
		public float m_fMaxRollbackTime;
	}
}
