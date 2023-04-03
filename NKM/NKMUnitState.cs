using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x020004BE RID: 1214
	public class NKMUnitState
	{
		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06002220 RID: 8736 RVA: 0x000AF2B1 File Offset: 0x000AD4B1
		public bool IsBadStatusState
		{
			get
			{
				return this.m_StatusEffectType > NKM_UNIT_STATUS_EFFECT.NUSE_NONE;
			}
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x000AF62C File Offset: 0x000AD82C
		public void Validate(string unitStrID)
		{
			if (this.m_listNKMEventBuff != null)
			{
				foreach (NKMEventBuff nkmeventBuff in this.m_listNKMEventBuff)
				{
					if (!nkmeventBuff.Validate())
					{
						Log.ErrorAndExit(string.Concat(new string[]
						{
							"[NKMUnitState] m_listNKMEventBuff is invalid. UnitStrID [",
							unitStrID,
							"], m_StateName [",
							this.m_StateName,
							"], m_BuffStrID [",
							nkmeventBuff.m_BuffStrID,
							"]"
						}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitState.cs", 178);
					}
				}
			}
			if (this.m_listNKMEventStatus != null)
			{
				foreach (NKMEventStatus nkmeventStatus in this.m_listNKMEventStatus)
				{
					if (!nkmeventStatus.Validate())
					{
						Log.ErrorAndExit(string.Format("[NKMUnitState] m_listNKMEventStatus is invalid. UnitStrID [{0}], m_StateName [{1}], m_StatusType [{2}]", unitStrID, this.m_StateName, nkmeventStatus.m_StatusType), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitState.cs", 189);
					}
				}
			}
			if (this.m_listNKMEventStopTime != null)
			{
				foreach (NKMEventStopTime nkmeventStopTime in this.m_listNKMEventStopTime)
				{
					if (nkmeventStopTime.m_StopTimeIndex >= NKM_STOP_TIME_INDEX.NSTI_MAX || nkmeventStopTime.m_StopTimeIndex < NKM_STOP_TIME_INDEX.NSTI_DAMAGE)
					{
						Log.ErrorAndExit(string.Format("[NKMUnitState] Invalid NKMEventStopTime/m_StopTimeIndex Data. unit:{0} InvalidData:{1}", unitStrID, nkmeventStopTime.m_StopTimeIndex), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitState.cs", 201);
					}
				}
			}
			HashSet<NKM_UNIT_STATUS_EFFECT> hashSet = new HashSet<NKM_UNIT_STATUS_EFFECT>();
			hashSet.UnionWith(this.m_listFixedStatusEffect);
			hashSet.IntersectWith(this.m_listFixedStatusImmune);
			if (hashSet.Count > 0)
			{
				Log.ErrorAndExit("[NKMUnitState] m_listFixedStatusEffect / m_listFixedStatusImmune has same NUSE element!", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitState.cs", 211);
			}
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x000AF80C File Offset: 0x000ADA0C
		public static void LoadAndMergeEventList<T>(NKMLua cNKMLua, string tableName, ref List<T> lstEvent, NKMUnitState.ValidateEvent validateEvent = null) where T : INKMUnitStateEvent, new()
		{
			if (cNKMLua.OpenTable(tableName))
			{
				int num = 1;
				while (cNKMLua.OpenTable(num))
				{
					T t;
					if (lstEvent.Count < num)
					{
						t = Activator.CreateInstance<T>();
						lstEvent.Add(t);
					}
					else
					{
						t = lstEvent[num - 1];
					}
					t.LoadFromLUA(cNKMLua);
					if (validateEvent != null)
					{
						validateEvent(t);
					}
					num++;
					cNKMLua.CloseTable();
				}
				cNKMLua.CloseTable();
			}
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x000AF888 File Offset: 0x000ADA88
		public static void LoadEventList<T>(NKMLua cNKMLua, string tableName, ref List<T> lstEvent, NKMUnitState.ValidateEvent validateEvent = null) where T : INKMUnitStateEvent, new()
		{
			if (cNKMLua.OpenTable(tableName))
			{
				lstEvent.Clear();
				int num = 1;
				while (cNKMLua.OpenTable(num))
				{
					T t = Activator.CreateInstance<T>();
					lstEvent.Add(t);
					t.LoadFromLUA(cNKMLua);
					if (validateEvent != null)
					{
						validateEvent(t);
					}
					num++;
					cNKMLua.CloseTable();
				}
				cNKMLua.CloseTable();
			}
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x000AF8F4 File Offset: 0x000ADAF4
		public static void DeepCopy<T>(List<T> sourceList, ref List<T> targetList, NKMUnitState.DeepCopyFactory<T> factory) where T : INKMUnitStateEvent, new()
		{
			if (sourceList == null || sourceList.Count == 0)
			{
				targetList.Clear();
				return;
			}
			int count = targetList.Count;
			for (int i = 0; i < sourceList.Count; i++)
			{
				T t;
				if (count - 1 >= i)
				{
					t = targetList[i];
				}
				else
				{
					t = Activator.CreateInstance<T>();
					targetList.Add(t);
				}
				factory(t, sourceList[i]);
			}
			if (targetList.Count > sourceList.Count)
			{
				targetList.RemoveRange(sourceList.Count, targetList.Count - sourceList.Count);
			}
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x000AF988 File Offset: 0x000ADB88
		public bool LoadFromLUA(NKMLua cNKMLua, NKMUnitTempletBase cNKMUnitTempletBase)
		{
			bool flag = cNKMLua.GetData<NKM_UNIT_STATE_TYPE>("m_NKM_UNIT_STATE_TYPE", ref this.m_NKM_UNIT_STATE_TYPE);
			cNKMLua.GetData("m_StateName", ref this.m_StateName);
			flag &= cNKMLua.GetData<NKM_SKILL_TYPE>("m_NKM_SKILL_TYPE", ref this.m_NKM_SKILL_TYPE);
			if (this.m_NKM_SKILL_TYPE.IsStatHoldType())
			{
				NKMTempletError.Add(string.Format("스테이트 [{0}]에 스킬 타입 [{1}]이 지정되어 있습니다. 오작동의 가능성이 있습니다", this.m_StateName, this.m_NKM_SKILL_TYPE), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitState.cs", 305);
				return false;
			}
			cNKMLua.GetData<NKM_UNIT_STATUS_EFFECT>("m_StatusEffectType", ref this.m_StatusEffectType);
			cNKMLua.GetData("m_AnimName", ref this.m_AnimName);
			cNKMLua.GetData("m_fAnimStartTime", ref this.m_fAnimStartTime);
			cNKMLua.GetData("m_fAnimSpeed", ref this.m_fAnimSpeed);
			cNKMLua.GetData("m_bAnimSpeedFix", ref this.m_bAnimSpeedFix);
			cNKMLua.GetData("m_bAnimLoop", ref this.m_bAnimLoop);
			cNKMLua.GetData("m_bSeeTarget", ref this.m_bSeeTarget);
			cNKMLua.GetData("m_bSeeMoreEnemy", ref this.m_bSeeMoreEnemy);
			cNKMLua.GetData("m_fAirHigh", ref this.m_fAirHigh);
			cNKMLua.GetData("m_bChangeIsAirUnit", ref this.m_bChangeIsAirUnit);
			cNKMLua.GetData("m_bNoAI", ref this.m_bNoAI);
			cNKMLua.GetData("m_bNoChangeRight", ref this.m_bNoChangeRight);
			cNKMLua.GetData("m_bNoMove", ref this.m_bNoMove);
			cNKMLua.GetData("m_bRun", ref this.m_bRun);
			cNKMLua.GetData("m_fRunSpeedRate", ref this.m_fRunSpeedRate);
			cNKMLua.GetData("m_bJump", ref this.m_bJump);
			cNKMLua.GetData("m_bForceNoTargeted", ref this.m_bForceNoTargeted);
			cNKMLua.GetData("m_bNoStateTypeEvent", ref this.m_bNoStateTypeEvent);
			cNKMLua.GetData("m_fGAccel", ref this.m_fGAccel);
			cNKMLua.GetData("m_bForceRightLeftDependTeam", ref this.m_bForceRightLeftDependTeam);
			cNKMLua.GetData("m_bForceRight", ref this.m_bForceRight);
			cNKMLua.GetData("m_bForceLeft", ref this.m_bForceLeft);
			cNKMLua.GetData("m_bShowGage", ref this.m_bShowGage);
			flag &= cNKMLua.GetData<NKM_SUPER_ARMOR_LEVEL>("m_SuperArmorLevel", ref this.m_SuperArmorLevel);
			cNKMLua.GetData("m_bNormalRevengeState", ref this.m_bNormalRevengeState);
			cNKMLua.GetData("m_bRevengeState", ref this.m_bRevengeState);
			cNKMLua.GetData("m_bSuperRevengeState", ref this.m_bSuperRevengeState);
			bool flag2 = false;
			cNKMLua.GetData("m_bSleepState", ref flag2);
			if (flag2)
			{
				this.m_StatusEffectType = NKM_UNIT_STATUS_EFFECT.NUSE_SLEEP;
			}
			bool flag3 = false;
			cNKMLua.GetData("m_bStunState", ref flag3);
			if (flag3)
			{
				this.m_StatusEffectType = NKM_UNIT_STATUS_EFFECT.NUSE_STUN;
			}
			bool flag4 = false;
			cNKMLua.GetData("m_bNoDamageBackSpeed", ref flag4);
			if (flag4)
			{
				this.m_listFixedStatusEffect.Add(NKM_UNIT_STATUS_EFFECT.NUSE_NO_DAMAGE_BACK_SPEED);
			}
			cNKMLua.GetData("m_RevengeChangeState", ref this.m_RevengeChangeState);
			cNKMLua.GetData("m_bInvincibleState", ref this.m_bInvincibleState);
			cNKMLua.GetData("m_bNotUseAttackSpeedStat", ref this.m_bNotUseAttackSpeedStat);
			cNKMLua.GetData("m_bSkillCutIn", ref this.m_bSkillCutIn);
			cNKMLua.GetData("m_bHyperSkillCutIn", ref this.m_bHyperSkillCutIn);
			cNKMLua.GetData("m_SkillCutInName", ref this.m_SkillCutInName);
			cNKMLua.GetData("m_bAutoCoolTime", ref this.m_bAutoCoolTime);
			this.m_StateCoolTime.LoadFromLua(cNKMLua, "m_StateCoolTime");
			if (cNKMLua.OpenTable("m_DangerCharge"))
			{
				this.m_DangerCharge.LoadFromLUA(cNKMLua);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_AnimTimeChangeStateTime", ref this.m_AnimTimeChangeStateTime);
			cNKMLua.GetData("m_AnimTimeChangeState", ref this.m_AnimTimeChangeState);
			cNKMLua.GetData("m_AnimTimeRateChangeStateTime", ref this.m_AnimTimeRateChangeStateTime);
			cNKMLua.GetData("m_AnimTimeRateChangeState", ref this.m_AnimTimeRateChangeState);
			cNKMLua.GetData("m_StateTimeChangeStateTime", ref this.m_StateTimeChangeStateTime);
			cNKMLua.GetData("m_StateTimeChangeState", ref this.m_StateTimeChangeState);
			cNKMLua.GetData("m_TargetDistOverChangeStateDist", ref this.m_TargetDistOverChangeStateDist);
			cNKMLua.GetData("m_TargetDistOverChangeState", ref this.m_TargetDistOverChangeState);
			cNKMLua.GetData("m_TargetDistLessChangeStateDist", ref this.m_TargetDistLessChangeStateDist);
			cNKMLua.GetData("m_TargetDistLessChangeState", ref this.m_TargetDistLessChangeState);
			cNKMLua.GetData("m_TargetLostOrDieStateDurationTime", ref this.m_TargetLostOrDieStateDurationTime);
			cNKMLua.GetData("m_TargetLostOrDieState", ref this.m_TargetLostOrDieState);
			cNKMLua.GetData("m_AnimEndChangeStatePlayCount", ref this.m_AnimEndChangeStatePlayCount);
			cNKMLua.GetData("m_AnimEndChangeState", ref this.m_AnimEndChangeState);
			cNKMLua.GetData("m_MapPosOverStatePos", ref this.m_MapPosOverStatePos);
			cNKMLua.GetData("m_MapPosOverState", ref this.m_MapPosOverState);
			cNKMLua.GetData("m_FootOnLandChangeState", ref this.m_FootOnLandChangeState);
			cNKMLua.GetData("m_FootOffLandChangeState", ref this.m_FootOffLandChangeState);
			cNKMLua.GetData("m_AnimEndFootOnLandChangeState", ref this.m_AnimEndFootOnLandChangeState);
			cNKMLua.GetData("m_AnimEndFootOffLandChangeState", ref this.m_AnimEndFootOffLandChangeState);
			cNKMLua.GetData("m_SpeedYPositiveChangeState", ref this.m_SpeedYPositiveChangeState);
			cNKMLua.GetData("m_SpeedY0NegativeChangeState", ref this.m_SpeedY0NegativeChangeState);
			cNKMLua.GetData("m_DamagedChangeState", ref this.m_DamagedChangeState);
			cNKMLua.GetData("m_AnimEndDyingState", ref this.m_AnimEndDyingState);
			cNKMLua.GetData("m_ChangeRightState", ref this.m_ChangeRightState);
			cNKMLua.GetData("m_ChangeRightTrueState", ref this.m_ChangeRightTrueState);
			cNKMLua.GetData("m_ChangeRightFalseState", ref this.m_ChangeRightFalseState);
			cNKMLua.GetData("m_AirTargetThisFrameChangeState", ref this.m_AirTargetThisFrameChangeState);
			cNKMLua.GetData("m_LandTargetThisFrameChangeState", ref this.m_LandTargetThisFrameChangeState);
			cNKMLua.GetData("m_fGageOffsetX", ref this.m_fGageOffsetX);
			cNKMLua.GetData("m_fGageOffsetY", ref this.m_fGageOffsetY);
			NKMUnitState.LoadAndMergeEventList<NKMEventText>(cNKMLua, "m_listNKMEventText", ref this.m_listNKMEventText, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventDirSpeed>(cNKMLua, "m_listNKMEventDirSpeed", ref this.m_listNKMEventDirSpeed, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventSpeed>(cNKMLua, "m_listNKMEventSpeed", ref this.m_listNKMEventSpeed, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventSpeedX>(cNKMLua, "m_listNKMEventSpeedX", ref this.m_listNKMEventSpeedX, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventSpeedY>(cNKMLua, "m_listNKMEventSpeedY", ref this.m_listNKMEventSpeedY, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventMove>(cNKMLua, "m_listNKMEventMove", ref this.m_listNKMEventMove, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventAttack>(cNKMLua, "m_listNKMEventAttack", ref this.m_listNKMEventAttack, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventStopTime>(cNKMLua, "m_listNKMEventStopTime", ref this.m_listNKMEventStopTime, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventInvincible>(cNKMLua, "m_listNKMEventInvincible", ref this.m_listNKMEventInvincible, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventInvincibleGlobal>(cNKMLua, "m_listNKMEventInvincibleGlobal", ref this.m_listNKMEventInvincibleGlobal, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventSuperArmor>(cNKMLua, "m_listNKMEventSuperArmor", ref this.m_listNKMEventSuperArmor, null);
			if (cNKMLua.OpenTable("m_listNKMEventSuperArmorGlobal"))
			{
				Log.Error("m_listNKMEventSuperArmorGlobal Obsolete!", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitState.cs", 436);
				cNKMLua.CloseTable();
			}
			NKMUnitState.LoadAndMergeEventList<NKMEventSound>(cNKMLua, "m_listNKMEventSound", ref this.m_listNKMEventSound, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventColor>(cNKMLua, "m_listNKMEventColor", ref this.m_listNKMEventColor, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventCameraCrash>(cNKMLua, "m_listNKMEventCameraCrash", ref this.m_listNKMEventCameraCrash, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventCameraMove>(cNKMLua, "m_listNKMEventCameraMove", ref this.m_listNKMEventCameraMove, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventFadeWorld>(cNKMLua, "m_listNKMEventFadeWorld", ref this.m_listNKMEventFadeWorld, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventDissolve>(cNKMLua, "m_listNKMEventDissolve", ref this.m_listNKMEventDissolve, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventMotionBlur>(cNKMLua, "m_listNKMEventMotionBlur", ref this.m_listNKMEventMotionBlur, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventEffect>(cNKMLua, "m_listNKMEventEffect", ref this.m_listNKMEventEffect, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventHyperSkillCutIn>(cNKMLua, "m_listNKMEventHyperSkillCutIn", ref this.m_listNKMEventHyperSkillCutIn, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventDamageEffect>(cNKMLua, "m_listNKMEventDamageEffect", ref this.m_listNKMEventDamageEffect, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventDEStateChange>(cNKMLua, "m_listNKMEventDEStateChange", ref this.m_listNKMEventDEStateChange, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventGameSpeed>(cNKMLua, "m_listNKMEventGameSpeed", ref this.m_listNKMEventGameSpeed, delegate(INKMUnitStateEvent cEvent)
			{
				if (!(cEvent is NKMEventGameSpeed))
				{
					return false;
				}
				NKMEventGameSpeed nkmeventGameSpeed = (NKMEventGameSpeed)cEvent;
				if (nkmeventGameSpeed.m_fGameSpeed < 1f && this.m_NKM_UNIT_STATE_TYPE != NKM_UNIT_STATE_TYPE.NUST_DAMAGE)
				{
					Log.Error(string.Format("EventSpeed is too low! : unit[{0}] state[{1}] speed[{2}]", cNKMUnitTempletBase.m_UnitStrID, this.m_StateName, nkmeventGameSpeed.m_fGameSpeed), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitState.cs", 459);
					return false;
				}
				return true;
			});
			NKMUnitState.LoadAndMergeEventList<NKMEventAnimSpeed>(cNKMLua, "m_listNKMEventAnimSpeed", ref this.m_listNKMEventAnimSpeed, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventBuff>(cNKMLua, "m_listNKMEventBuff", ref this.m_listNKMEventBuff, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventStatus>(cNKMLua, "m_listNKMEventStatus", ref this.m_listNKMEventStatus, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventRespawn>(cNKMLua, "m_listNKMEventRespawn", ref this.m_listNKMEventRespawn, null);
			if (cNKMLua.OpenTable("m_NKMEventUnitChange"))
			{
				if (this.m_NKMEventUnitChange == null)
				{
					this.m_NKMEventUnitChange = new NKMEventUnitChange();
				}
				this.m_NKMEventUnitChange.LoadFromLUA(cNKMLua);
				cNKMLua.CloseTable();
			}
			NKMUnitState.LoadAndMergeEventList<NKMEventDie>(cNKMLua, "m_listNKMEventDie", ref this.m_listNKMEventDie, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventChangeState>(cNKMLua, "m_listNKMEventChangeState", ref this.m_listNKMEventChangeState, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventAgro>(cNKMLua, "m_listNKMEventAgro", ref this.m_listNKMEventAgro, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventHeal>(cNKMLua, "m_listNKMEventHeal", ref this.m_listNKMEventHeal, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventStun>(cNKMLua, "m_listNKMEventStun", ref this.m_listNKMEventStun, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventCost>(cNKMLua, "m_listNKMEventCost", ref this.m_listNKMEventCost, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventDefence>(cNKMLua, "m_listNKMEventDefence", ref this.m_listNKMEventDefence, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventDispel>(cNKMLua, "m_listNKMEventDispel", ref this.m_listNKMEventDispel, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventChangeCooltime>(cNKMLua, "m_listNKMEventChangeCooltime", ref this.m_listNKMEventChangeCooltime, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventCatchEnd>(cNKMLua, "m_listNKMEventCatchEnd", ref this.m_listNKMEventCatchEnd, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventFindTarget>(cNKMLua, "m_listNKMEventFindTarget", ref this.m_listNKMEventFindTarget, null);
			cNKMLua.GetDataListEnum<NKM_UNIT_STATUS_EFFECT>("m_listFixedStatusEffect", this.m_listFixedStatusEffect, false);
			cNKMLua.GetDataListEnum<NKM_UNIT_STATUS_EFFECT>("m_listFixedStatusImmune", this.m_listFixedStatusImmune, false);
			return flag;
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x000B0274 File Offset: 0x000AE474
		public void DeepCopyFromSource(NKMUnitState source)
		{
			this.m_NKM_UNIT_STATE_TYPE = source.m_NKM_UNIT_STATE_TYPE;
			this.m_StateName = (string)source.m_StateName.Clone();
			this.m_StateID = source.m_StateID;
			this.m_StatusEffectType = source.m_StatusEffectType;
			this.m_NKM_SKILL_TYPE = source.m_NKM_SKILL_TYPE;
			this.m_listCoolTimeLink.Clear();
			for (int i = 0; i < source.m_listCoolTimeLink.Count; i++)
			{
				this.m_listCoolTimeLink.Add(source.m_listCoolTimeLink[i]);
			}
			this.m_AnimName = (string)source.m_AnimName.Clone();
			this.m_fAnimStartTime = source.m_fAnimStartTime;
			this.m_fAnimSpeed = source.m_fAnimSpeed;
			this.m_bAnimSpeedFix = source.m_bAnimSpeedFix;
			this.m_bAnimLoop = source.m_bAnimLoop;
			this.m_bSeeTarget = source.m_bSeeTarget;
			this.m_bSeeMoreEnemy = source.m_bSeeMoreEnemy;
			this.m_fAirHigh = source.m_fAirHigh;
			this.m_bChangeIsAirUnit = source.m_bChangeIsAirUnit;
			this.m_bNoAI = source.m_bNoAI;
			this.m_bNoMove = source.m_bNoMove;
			this.m_bNoChangeRight = source.m_bNoChangeRight;
			this.m_bRun = source.m_bRun;
			this.m_fRunSpeedRate = source.m_fRunSpeedRate;
			this.m_bJump = source.m_bJump;
			this.m_bForceNoTargeted = source.m_bForceNoTargeted;
			this.m_bNoStateTypeEvent = source.m_bNoStateTypeEvent;
			this.m_fGAccel = source.m_fGAccel;
			this.m_bForceRightLeftDependTeam = source.m_bForceRightLeftDependTeam;
			this.m_bForceRight = source.m_bForceRight;
			this.m_bForceLeft = source.m_bForceLeft;
			this.m_bShowGage = source.m_bShowGage;
			this.m_SuperArmorLevel = source.m_SuperArmorLevel;
			this.m_bNormalRevengeState = source.m_bNormalRevengeState;
			this.m_bRevengeState = source.m_bRevengeState;
			this.m_bSuperRevengeState = source.m_bSuperRevengeState;
			this.m_RevengeChangeState = source.m_RevengeChangeState;
			this.m_bInvincibleState = source.m_bInvincibleState;
			this.m_bNotUseAttackSpeedStat = source.m_bNotUseAttackSpeedStat;
			this.m_bSkillCutIn = source.m_bSkillCutIn;
			this.m_bHyperSkillCutIn = source.m_bHyperSkillCutIn;
			this.m_SkillCutInName = source.m_SkillCutInName;
			this.m_bAutoCoolTime = source.m_bAutoCoolTime;
			this.m_StateCoolTime.DeepCopyFromSource(source.m_StateCoolTime);
			this.m_DangerCharge.DeepCopyFromSource(source.m_DangerCharge);
			this.m_AnimTimeChangeStateTime = source.m_AnimTimeChangeStateTime;
			this.m_AnimTimeChangeState = (string)source.m_AnimTimeChangeState.Clone();
			this.m_AnimTimeRateChangeStateTime = source.m_AnimTimeRateChangeStateTime;
			this.m_AnimTimeRateChangeState = (string)source.m_AnimTimeRateChangeState.Clone();
			this.m_StateTimeChangeStateTime = source.m_StateTimeChangeStateTime;
			this.m_StateTimeChangeState = (string)source.m_StateTimeChangeState.Clone();
			this.m_TargetDistOverChangeStateDist = source.m_TargetDistOverChangeStateDist;
			this.m_TargetDistOverChangeState = (string)source.m_TargetDistOverChangeState.Clone();
			this.m_TargetDistLessChangeStateDist = source.m_TargetDistLessChangeStateDist;
			this.m_TargetDistLessChangeState = (string)source.m_TargetDistLessChangeState.Clone();
			this.m_TargetLostOrDieStateDurationTime = source.m_TargetLostOrDieStateDurationTime;
			this.m_TargetLostOrDieState = (string)source.m_TargetLostOrDieState.Clone();
			this.m_AnimEndChangeStatePlayCount = source.m_AnimEndChangeStatePlayCount;
			this.m_AnimEndChangeState = (string)source.m_AnimEndChangeState.Clone();
			this.m_MapPosOverStatePos = source.m_MapPosOverStatePos;
			this.m_MapPosOverState = (string)source.m_MapPosOverState.Clone();
			this.m_FootOffLandChangeState = source.m_FootOffLandChangeState;
			this.m_FootOnLandChangeState = (string)source.m_FootOnLandChangeState.Clone();
			this.m_AnimEndFootOnLandChangeState = (string)source.m_AnimEndFootOnLandChangeState.Clone();
			this.m_AnimEndFootOffLandChangeState = (string)source.m_AnimEndFootOffLandChangeState.Clone();
			this.m_SpeedYPositiveChangeState = source.m_SpeedYPositiveChangeState;
			this.m_SpeedY0NegativeChangeState = source.m_SpeedY0NegativeChangeState;
			this.m_DamagedChangeState = source.m_DamagedChangeState;
			this.m_AnimEndDyingState = source.m_AnimEndDyingState;
			this.m_ChangeRightState = source.m_ChangeRightState;
			this.m_ChangeRightTrueState = source.m_ChangeRightTrueState;
			this.m_ChangeRightFalseState = source.m_ChangeRightFalseState;
			this.m_AirTargetThisFrameChangeState = source.m_AirTargetThisFrameChangeState;
			this.m_LandTargetThisFrameChangeState = source.m_LandTargetThisFrameChangeState;
			this.m_fGageOffsetX = source.m_fGageOffsetX;
			this.m_fGageOffsetY = source.m_fGageOffsetY;
			if (source.m_NKMEventUnitChange == null)
			{
				this.m_NKMEventUnitChange = null;
			}
			else
			{
				this.m_NKMEventUnitChange = source.m_NKMEventUnitChange;
			}
			NKMUnitState.DeepCopy<NKMEventText>(source.m_listNKMEventText, ref this.m_listNKMEventText, delegate(NKMEventText t, NKMEventText s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventDirSpeed>(source.m_listNKMEventDirSpeed, ref this.m_listNKMEventDirSpeed, delegate(NKMEventDirSpeed t, NKMEventDirSpeed s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventSpeed>(source.m_listNKMEventSpeed, ref this.m_listNKMEventSpeed, delegate(NKMEventSpeed t, NKMEventSpeed s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventSpeedX>(source.m_listNKMEventSpeedX, ref this.m_listNKMEventSpeedX, delegate(NKMEventSpeedX t, NKMEventSpeedX s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventSpeedY>(source.m_listNKMEventSpeedY, ref this.m_listNKMEventSpeedY, delegate(NKMEventSpeedY t, NKMEventSpeedY s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventMove>(source.m_listNKMEventMove, ref this.m_listNKMEventMove, delegate(NKMEventMove t, NKMEventMove s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventAttack>(source.m_listNKMEventAttack, ref this.m_listNKMEventAttack, delegate(NKMEventAttack t, NKMEventAttack s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventStopTime>(source.m_listNKMEventStopTime, ref this.m_listNKMEventStopTime, delegate(NKMEventStopTime t, NKMEventStopTime s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventInvincible>(source.m_listNKMEventInvincible, ref this.m_listNKMEventInvincible, delegate(NKMEventInvincible t, NKMEventInvincible s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventInvincibleGlobal>(source.m_listNKMEventInvincibleGlobal, ref this.m_listNKMEventInvincibleGlobal, delegate(NKMEventInvincibleGlobal t, NKMEventInvincibleGlobal s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventSuperArmor>(source.m_listNKMEventSuperArmor, ref this.m_listNKMEventSuperArmor, delegate(NKMEventSuperArmor t, NKMEventSuperArmor s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventSound>(source.m_listNKMEventSound, ref this.m_listNKMEventSound, delegate(NKMEventSound t, NKMEventSound s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventColor>(source.m_listNKMEventColor, ref this.m_listNKMEventColor, delegate(NKMEventColor t, NKMEventColor s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventCameraCrash>(source.m_listNKMEventCameraCrash, ref this.m_listNKMEventCameraCrash, delegate(NKMEventCameraCrash t, NKMEventCameraCrash s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventCameraMove>(source.m_listNKMEventCameraMove, ref this.m_listNKMEventCameraMove, delegate(NKMEventCameraMove t, NKMEventCameraMove s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventFadeWorld>(source.m_listNKMEventFadeWorld, ref this.m_listNKMEventFadeWorld, delegate(NKMEventFadeWorld t, NKMEventFadeWorld s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventDissolve>(source.m_listNKMEventDissolve, ref this.m_listNKMEventDissolve, delegate(NKMEventDissolve t, NKMEventDissolve s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventMotionBlur>(source.m_listNKMEventMotionBlur, ref this.m_listNKMEventMotionBlur, delegate(NKMEventMotionBlur t, NKMEventMotionBlur s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventEffect>(source.m_listNKMEventEffect, ref this.m_listNKMEventEffect, delegate(NKMEventEffect t, NKMEventEffect s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventHyperSkillCutIn>(source.m_listNKMEventHyperSkillCutIn, ref this.m_listNKMEventHyperSkillCutIn, delegate(NKMEventHyperSkillCutIn t, NKMEventHyperSkillCutIn s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventDamageEffect>(source.m_listNKMEventDamageEffect, ref this.m_listNKMEventDamageEffect, delegate(NKMEventDamageEffect t, NKMEventDamageEffect s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventDEStateChange>(source.m_listNKMEventDEStateChange, ref this.m_listNKMEventDEStateChange, delegate(NKMEventDEStateChange t, NKMEventDEStateChange s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventGameSpeed>(source.m_listNKMEventGameSpeed, ref this.m_listNKMEventGameSpeed, delegate(NKMEventGameSpeed t, NKMEventGameSpeed s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventAnimSpeed>(source.m_listNKMEventAnimSpeed, ref this.m_listNKMEventAnimSpeed, delegate(NKMEventAnimSpeed t, NKMEventAnimSpeed s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventBuff>(source.m_listNKMEventBuff, ref this.m_listNKMEventBuff, delegate(NKMEventBuff t, NKMEventBuff s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventStatus>(source.m_listNKMEventStatus, ref this.m_listNKMEventStatus, delegate(NKMEventStatus t, NKMEventStatus s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventRespawn>(source.m_listNKMEventRespawn, ref this.m_listNKMEventRespawn, delegate(NKMEventRespawn t, NKMEventRespawn s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventDie>(source.m_listNKMEventDie, ref this.m_listNKMEventDie, delegate(NKMEventDie t, NKMEventDie s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventChangeState>(source.m_listNKMEventChangeState, ref this.m_listNKMEventChangeState, delegate(NKMEventChangeState t, NKMEventChangeState s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventAgro>(source.m_listNKMEventAgro, ref this.m_listNKMEventAgro, delegate(NKMEventAgro t, NKMEventAgro s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventHeal>(source.m_listNKMEventHeal, ref this.m_listNKMEventHeal, delegate(NKMEventHeal t, NKMEventHeal s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventStun>(source.m_listNKMEventStun, ref this.m_listNKMEventStun, delegate(NKMEventStun t, NKMEventStun s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventCost>(source.m_listNKMEventCost, ref this.m_listNKMEventCost, delegate(NKMEventCost t, NKMEventCost s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventDefence>(source.m_listNKMEventDefence, ref this.m_listNKMEventDefence, delegate(NKMEventDefence t, NKMEventDefence s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventDispel>(source.m_listNKMEventDispel, ref this.m_listNKMEventDispel, delegate(NKMEventDispel t, NKMEventDispel s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventChangeCooltime>(source.m_listNKMEventChangeCooltime, ref this.m_listNKMEventChangeCooltime, delegate(NKMEventChangeCooltime t, NKMEventChangeCooltime s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventCatchEnd>(source.m_listNKMEventCatchEnd, ref this.m_listNKMEventCatchEnd, delegate(NKMEventCatchEnd t, NKMEventCatchEnd s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventFindTarget>(source.m_listNKMEventFindTarget, ref this.m_listNKMEventFindTarget, delegate(NKMEventFindTarget t, NKMEventFindTarget s)
			{
				t.DeepCopyFromSource(s);
			});
			this.m_listFixedStatusEffect.Clear();
			this.m_listFixedStatusEffect.UnionWith(source.m_listFixedStatusEffect);
			this.m_listFixedStatusImmune.Clear();
			this.m_listFixedStatusImmune.UnionWith(source.m_listFixedStatusImmune);
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x000B0E0E File Offset: 0x000AF00E
		public IEnumerable<IReadOnlyList<INKMUnitStateEvent>> StateEventLists()
		{
			yield return this.m_listNKMEventText;
			yield return this.m_listNKMEventDirSpeed;
			yield return this.m_listNKMEventSpeed;
			yield return this.m_listNKMEventSpeedX;
			yield return this.m_listNKMEventSpeedY;
			yield return this.m_listNKMEventMove;
			yield return this.m_listNKMEventAttack;
			yield return this.m_listNKMEventStopTime;
			yield return this.m_listNKMEventInvincible;
			yield return this.m_listNKMEventInvincibleGlobal;
			yield return this.m_listNKMEventSuperArmor;
			yield return this.m_listNKMEventSound;
			yield return this.m_listNKMEventColor;
			yield return this.m_listNKMEventCameraCrash;
			yield return this.m_listNKMEventCameraMove;
			yield return this.m_listNKMEventFadeWorld;
			yield return this.m_listNKMEventDissolve;
			yield return this.m_listNKMEventMotionBlur;
			yield return this.m_listNKMEventEffect;
			yield return this.m_listNKMEventHyperSkillCutIn;
			yield return this.m_listNKMEventDamageEffect;
			yield return this.m_listNKMEventDEStateChange;
			yield return this.m_listNKMEventGameSpeed;
			yield return this.m_listNKMEventAnimSpeed;
			yield return this.m_listNKMEventBuff;
			yield return this.m_listNKMEventStatus;
			yield return this.m_listNKMEventRespawn;
			yield return this.m_listNKMEventDie;
			yield return this.m_listNKMEventChangeState;
			yield return this.m_listNKMEventAgro;
			yield return this.m_listNKMEventHeal;
			yield return this.m_listNKMEventStun;
			yield return this.m_listNKMEventCost;
			yield return this.m_listNKMEventDefence;
			yield return this.m_listNKMEventDispel;
			yield return this.m_listNKMEventChangeCooltime;
			yield return this.m_listNKMEventCatchEnd;
			yield return this.m_listNKMEventFindTarget;
			yield break;
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x000B0E20 File Offset: 0x000AF020
		public float CalcaluateMaxRollbackTime(string unitStrID)
		{
			float num = NKMCommonConst.SUMMON_UNIT_NOEVENT_TIME;
			foreach (IReadOnlyList<INKMUnitStateEvent> lstEvent in this.StateEventLists())
			{
				num = NKMMathf.Min(num, this.GetMaxRollbackTime<INKMUnitStateEvent>(lstEvent, unitStrID));
			}
			return num;
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x000B0E7C File Offset: 0x000AF07C
		private float GetMaxRollbackTime<T>(IReadOnlyList<T> lstEvent, string unitStrID) where T : INKMUnitStateEvent
		{
			float num = NKMCommonConst.SUMMON_UNIT_NOEVENT_TIME;
			foreach (T t in lstEvent)
			{
				switch (t.RollbackType)
				{
				case EventRollbackType.Prohibited:
				{
					float num2;
					if (t.bAnimTime)
					{
						num2 = this.m_fAnimStartTime + this.m_fAnimSpeed * num;
					}
					else
					{
						num2 = num;
					}
					if (t.EventStartTime < num)
					{
						num = t.EventStartTime;
					}
					if (t.EventStartTime < num2)
					{
						if (t.bAnimTime)
						{
							Log.Error(string.Format("{0}.{1} : 롤백 금지 이벤트 {2} (animTime {3}s) 감지", new object[]
							{
								unitStrID,
								this.m_StateName,
								t.ToString(),
								t.EventStartTime
							}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitState.cs", 807);
						}
						else
						{
							Log.Error(string.Format("{0}.{1} : 롤백 금지 이벤트 {2} (stateTime {3}s) 감지", new object[]
							{
								unitStrID,
								this.m_StateName,
								t.ToString(),
								t.EventStartTime
							}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitState.cs", 811);
						}
					}
					break;
				}
				case EventRollbackType.Warning:
				{
					float num3;
					if (t.bAnimTime)
					{
						num3 = this.m_fAnimStartTime + this.m_fAnimSpeed * num;
					}
					else
					{
						num3 = num;
					}
					if (t.EventStartTime < num3)
					{
						if (t.bAnimTime)
						{
							Log.Warn(string.Format("{0}.{1} : 롤백 위험 이벤트 {2} (animTime {3}s) 감지", new object[]
							{
								unitStrID,
								this.m_StateName,
								t.ToString(),
								t.EventStartTime
							}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitState.cs", 778);
						}
						else
						{
							Log.Warn(string.Format("{0}.{1} : 롤백 위험 이벤트 {2} (stateTime {3}s) 감지", new object[]
							{
								unitStrID,
								this.m_StateName,
								t.ToString(),
								t.EventStartTime
							}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitState.cs", 782);
						}
					}
					break;
				}
				}
			}
			return num;
		}

		// Token: 0x04002309 RID: 8969
		public NKM_UNIT_STATE_TYPE m_NKM_UNIT_STATE_TYPE;

		// Token: 0x0400230A RID: 8970
		public NKM_UNIT_STATUS_EFFECT m_StatusEffectType;

		// Token: 0x0400230B RID: 8971
		public string m_StateName = "";

		// Token: 0x0400230C RID: 8972
		public byte m_StateID;

		// Token: 0x0400230D RID: 8973
		public NKM_SKILL_TYPE m_NKM_SKILL_TYPE;

		// Token: 0x0400230E RID: 8974
		public List<string> m_listCoolTimeLink = new List<string>();

		// Token: 0x0400230F RID: 8975
		public string m_AnimName = "";

		// Token: 0x04002310 RID: 8976
		public float m_fAnimStartTime;

		// Token: 0x04002311 RID: 8977
		public float m_fAnimSpeed = 1f;

		// Token: 0x04002312 RID: 8978
		public bool m_bAnimSpeedFix;

		// Token: 0x04002313 RID: 8979
		public bool m_bAnimLoop;

		// Token: 0x04002314 RID: 8980
		public bool m_bSeeTarget;

		// Token: 0x04002315 RID: 8981
		public bool m_bSeeMoreEnemy;

		// Token: 0x04002316 RID: 8982
		public float m_fAirHigh = -1f;

		// Token: 0x04002317 RID: 8983
		public bool m_bChangeIsAirUnit;

		// Token: 0x04002318 RID: 8984
		public bool m_bNoAI;

		// Token: 0x04002319 RID: 8985
		public bool m_bNoChangeRight;

		// Token: 0x0400231A RID: 8986
		public bool m_bNoMove;

		// Token: 0x0400231B RID: 8987
		public bool m_bRun;

		// Token: 0x0400231C RID: 8988
		public float m_fRunSpeedRate = 1f;

		// Token: 0x0400231D RID: 8989
		public bool m_bJump;

		// Token: 0x0400231E RID: 8990
		public bool m_bForceNoTargeted;

		// Token: 0x0400231F RID: 8991
		public bool m_bNoStateTypeEvent;

		// Token: 0x04002320 RID: 8992
		public float m_fGAccel = -1f;

		// Token: 0x04002321 RID: 8993
		public bool m_bForceRightLeftDependTeam;

		// Token: 0x04002322 RID: 8994
		public bool m_bForceRight;

		// Token: 0x04002323 RID: 8995
		public bool m_bForceLeft;

		// Token: 0x04002324 RID: 8996
		public bool m_bShowGage = true;

		// Token: 0x04002325 RID: 8997
		public NKM_SUPER_ARMOR_LEVEL m_SuperArmorLevel;

		// Token: 0x04002326 RID: 8998
		public bool m_bNormalRevengeState;

		// Token: 0x04002327 RID: 8999
		public bool m_bRevengeState;

		// Token: 0x04002328 RID: 9000
		public bool m_bSuperRevengeState;

		// Token: 0x04002329 RID: 9001
		public string m_RevengeChangeState = "";

		// Token: 0x0400232A RID: 9002
		public bool m_bInvincibleState;

		// Token: 0x0400232B RID: 9003
		public bool m_bNotUseAttackSpeedStat;

		// Token: 0x0400232C RID: 9004
		public bool m_bSkillCutIn;

		// Token: 0x0400232D RID: 9005
		public bool m_bHyperSkillCutIn;

		// Token: 0x0400232E RID: 9006
		public string m_SkillCutInName = "";

		// Token: 0x0400232F RID: 9007
		public bool m_bAutoCoolTime;

		// Token: 0x04002330 RID: 9008
		public NKMMinMaxFloat m_StateCoolTime = new NKMMinMaxFloat(0f, 0f);

		// Token: 0x04002331 RID: 9009
		public NKMDangerCharge m_DangerCharge = new NKMDangerCharge();

		// Token: 0x04002332 RID: 9010
		public float m_AnimTimeChangeStateTime = -1f;

		// Token: 0x04002333 RID: 9011
		public string m_AnimTimeChangeState = "";

		// Token: 0x04002334 RID: 9012
		public float m_AnimTimeRateChangeStateTime = -1f;

		// Token: 0x04002335 RID: 9013
		public string m_AnimTimeRateChangeState = "";

		// Token: 0x04002336 RID: 9014
		public float m_StateTimeChangeStateTime = -1f;

		// Token: 0x04002337 RID: 9015
		public string m_StateTimeChangeState = "";

		// Token: 0x04002338 RID: 9016
		public float m_TargetLostOrDieStateDurationTime = -1f;

		// Token: 0x04002339 RID: 9017
		public string m_TargetLostOrDieState = "";

		// Token: 0x0400233A RID: 9018
		public int m_AnimEndChangeStatePlayCount = 1;

		// Token: 0x0400233B RID: 9019
		public string m_AnimEndChangeState = "";

		// Token: 0x0400233C RID: 9020
		public float m_TargetDistOverChangeStateDist;

		// Token: 0x0400233D RID: 9021
		public string m_TargetDistOverChangeState = "";

		// Token: 0x0400233E RID: 9022
		public float m_TargetDistLessChangeStateDist;

		// Token: 0x0400233F RID: 9023
		public string m_TargetDistLessChangeState = "";

		// Token: 0x04002340 RID: 9024
		public float m_MapPosOverStatePos;

		// Token: 0x04002341 RID: 9025
		public string m_MapPosOverState = "";

		// Token: 0x04002342 RID: 9026
		public string m_FootOnLandChangeState = "";

		// Token: 0x04002343 RID: 9027
		public string m_FootOffLandChangeState = "";

		// Token: 0x04002344 RID: 9028
		public string m_AnimEndFootOnLandChangeState = "";

		// Token: 0x04002345 RID: 9029
		public string m_AnimEndFootOffLandChangeState = "";

		// Token: 0x04002346 RID: 9030
		public string m_SpeedYPositiveChangeState = "";

		// Token: 0x04002347 RID: 9031
		public string m_SpeedY0NegativeChangeState = "";

		// Token: 0x04002348 RID: 9032
		public string m_DamagedChangeState = "";

		// Token: 0x04002349 RID: 9033
		public string m_AnimEndDyingState = "";

		// Token: 0x0400234A RID: 9034
		public string m_ChangeRightState = "";

		// Token: 0x0400234B RID: 9035
		public string m_ChangeRightTrueState = "";

		// Token: 0x0400234C RID: 9036
		public string m_ChangeRightFalseState = "";

		// Token: 0x0400234D RID: 9037
		public string m_AirTargetThisFrameChangeState = "";

		// Token: 0x0400234E RID: 9038
		public string m_LandTargetThisFrameChangeState = "";

		// Token: 0x0400234F RID: 9039
		public float m_fGageOffsetX;

		// Token: 0x04002350 RID: 9040
		public float m_fGageOffsetY;

		// Token: 0x04002351 RID: 9041
		public List<NKMEventText> m_listNKMEventText = new List<NKMEventText>();

		// Token: 0x04002352 RID: 9042
		public List<NKMEventDirSpeed> m_listNKMEventDirSpeed = new List<NKMEventDirSpeed>();

		// Token: 0x04002353 RID: 9043
		public List<NKMEventSpeed> m_listNKMEventSpeed = new List<NKMEventSpeed>();

		// Token: 0x04002354 RID: 9044
		public List<NKMEventSpeedX> m_listNKMEventSpeedX = new List<NKMEventSpeedX>();

		// Token: 0x04002355 RID: 9045
		public List<NKMEventSpeedY> m_listNKMEventSpeedY = new List<NKMEventSpeedY>();

		// Token: 0x04002356 RID: 9046
		public List<NKMEventMove> m_listNKMEventMove = new List<NKMEventMove>();

		// Token: 0x04002357 RID: 9047
		public List<NKMEventAttack> m_listNKMEventAttack = new List<NKMEventAttack>();

		// Token: 0x04002358 RID: 9048
		public List<NKMEventStopTime> m_listNKMEventStopTime = new List<NKMEventStopTime>();

		// Token: 0x04002359 RID: 9049
		public List<NKMEventInvincible> m_listNKMEventInvincible = new List<NKMEventInvincible>();

		// Token: 0x0400235A RID: 9050
		public List<NKMEventInvincibleGlobal> m_listNKMEventInvincibleGlobal = new List<NKMEventInvincibleGlobal>();

		// Token: 0x0400235B RID: 9051
		public List<NKMEventSuperArmor> m_listNKMEventSuperArmor = new List<NKMEventSuperArmor>();

		// Token: 0x0400235C RID: 9052
		public List<NKMEventSound> m_listNKMEventSound = new List<NKMEventSound>();

		// Token: 0x0400235D RID: 9053
		public List<NKMEventColor> m_listNKMEventColor = new List<NKMEventColor>();

		// Token: 0x0400235E RID: 9054
		public List<NKMEventCameraCrash> m_listNKMEventCameraCrash = new List<NKMEventCameraCrash>();

		// Token: 0x0400235F RID: 9055
		public List<NKMEventCameraMove> m_listNKMEventCameraMove = new List<NKMEventCameraMove>();

		// Token: 0x04002360 RID: 9056
		public List<NKMEventFadeWorld> m_listNKMEventFadeWorld = new List<NKMEventFadeWorld>();

		// Token: 0x04002361 RID: 9057
		public List<NKMEventDissolve> m_listNKMEventDissolve = new List<NKMEventDissolve>();

		// Token: 0x04002362 RID: 9058
		public List<NKMEventMotionBlur> m_listNKMEventMotionBlur = new List<NKMEventMotionBlur>();

		// Token: 0x04002363 RID: 9059
		public List<NKMEventEffect> m_listNKMEventEffect = new List<NKMEventEffect>();

		// Token: 0x04002364 RID: 9060
		public List<NKMEventHyperSkillCutIn> m_listNKMEventHyperSkillCutIn = new List<NKMEventHyperSkillCutIn>();

		// Token: 0x04002365 RID: 9061
		public List<NKMEventDamageEffect> m_listNKMEventDamageEffect = new List<NKMEventDamageEffect>();

		// Token: 0x04002366 RID: 9062
		public List<NKMEventDEStateChange> m_listNKMEventDEStateChange = new List<NKMEventDEStateChange>();

		// Token: 0x04002367 RID: 9063
		public List<NKMEventGameSpeed> m_listNKMEventGameSpeed = new List<NKMEventGameSpeed>();

		// Token: 0x04002368 RID: 9064
		public List<NKMEventAnimSpeed> m_listNKMEventAnimSpeed = new List<NKMEventAnimSpeed>();

		// Token: 0x04002369 RID: 9065
		public List<NKMEventBuff> m_listNKMEventBuff = new List<NKMEventBuff>();

		// Token: 0x0400236A RID: 9066
		public List<NKMEventStatus> m_listNKMEventStatus = new List<NKMEventStatus>();

		// Token: 0x0400236B RID: 9067
		public List<NKMEventRespawn> m_listNKMEventRespawn = new List<NKMEventRespawn>();

		// Token: 0x0400236C RID: 9068
		public List<NKMEventDie> m_listNKMEventDie = new List<NKMEventDie>();

		// Token: 0x0400236D RID: 9069
		public List<NKMEventChangeState> m_listNKMEventChangeState = new List<NKMEventChangeState>();

		// Token: 0x0400236E RID: 9070
		public List<NKMEventAgro> m_listNKMEventAgro = new List<NKMEventAgro>();

		// Token: 0x0400236F RID: 9071
		public List<NKMEventHeal> m_listNKMEventHeal = new List<NKMEventHeal>();

		// Token: 0x04002370 RID: 9072
		public List<NKMEventStun> m_listNKMEventStun = new List<NKMEventStun>();

		// Token: 0x04002371 RID: 9073
		public List<NKMEventCost> m_listNKMEventCost = new List<NKMEventCost>();

		// Token: 0x04002372 RID: 9074
		public List<NKMEventDefence> m_listNKMEventDefence = new List<NKMEventDefence>();

		// Token: 0x04002373 RID: 9075
		public List<NKMEventDispel> m_listNKMEventDispel = new List<NKMEventDispel>();

		// Token: 0x04002374 RID: 9076
		public List<NKMEventChangeCooltime> m_listNKMEventChangeCooltime = new List<NKMEventChangeCooltime>();

		// Token: 0x04002375 RID: 9077
		public List<NKMEventCatchEnd> m_listNKMEventCatchEnd = new List<NKMEventCatchEnd>();

		// Token: 0x04002376 RID: 9078
		public List<NKMEventFindTarget> m_listNKMEventFindTarget = new List<NKMEventFindTarget>();

		// Token: 0x04002377 RID: 9079
		public NKMEventUnitChange m_NKMEventUnitChange;

		// Token: 0x04002378 RID: 9080
		public HashSet<NKM_UNIT_STATUS_EFFECT> m_listFixedStatusEffect = new HashSet<NKM_UNIT_STATUS_EFFECT>();

		// Token: 0x04002379 RID: 9081
		public HashSet<NKM_UNIT_STATUS_EFFECT> m_listFixedStatusImmune = new HashSet<NKM_UNIT_STATUS_EFFECT>();

		// Token: 0x02001220 RID: 4640
		// (Invoke) Token: 0x0600A1CF RID: 41423
		public delegate bool ValidateEvent(INKMUnitStateEvent stateEvent);

		// Token: 0x02001221 RID: 4641
		// (Invoke) Token: 0x0600A1D3 RID: 41427
		public delegate void DeepCopyFactory<T>(T target, T source);
	}
}
