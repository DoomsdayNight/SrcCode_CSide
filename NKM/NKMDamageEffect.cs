using System;
using System.Collections.Generic;
using Cs.Logging;
using Cs.Math;
using NKM.Templet;

namespace NKM
{
	// Token: 0x020003C6 RID: 966
	public class NKMDamageEffect : NKMObjectPoolData
	{
		// Token: 0x0600195B RID: 6491 RVA: 0x0006882C File Offset: 0x00066A2C
		protected NKMDamageInst GetDamageInstAtk(int index)
		{
			NKMDamageInst result;
			if (this.m_dictDamageInstAtk.TryGetValue(index, out result))
			{
				return result;
			}
			NKMDamageInst nkmdamageInst = new NKMDamageInst();
			this.m_dictDamageInstAtk.Add(index, nkmdamageInst);
			return nkmdamageInst;
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x0006885F File Offset: 0x00066A5F
		public float GetTempSortDist()
		{
			return this.m_TempSortDist;
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x00068868 File Offset: 0x00066A68
		public NKMDamageEffect()
		{
			this.m_NKM_OBJECT_POOL_TYPE = NKM_OBJECT_POOL_TYPE.NOPT_NKMDamageEffect;
			this.m_NKM_DAMAGE_EFFECT_CLASS_TYPE = NKM_DAMAGE_EFFECT_CLASS_TYPE.NDECT_NKM;
			this.Init();
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x000688FC File Offset: 0x00066AFC
		public void Init()
		{
			this.m_DamageEffectUID = 0;
			this.m_DETemplet = null;
			this.m_DEData.Init();
			this.m_fDeltaTime = 0f;
			this.m_EffectStateNow = null;
			this.m_StateNameNow = "";
			this.m_StateNameNext = "";
			this.m_dictDamageInstAtk.Clear();
			this.m_UnitSkillTemplet = null;
			this.m_TargetUnit = null;
			foreach (KeyValuePair<float, NKMTimeStamp> keyValuePair in this.m_EventTimeStampAnim)
			{
				NKMTimeStamp value = keyValuePair.Value;
				this.m_NKMGame.GetObjectPool().CloseObj(value);
			}
			this.m_EventTimeStampAnim.Clear();
			foreach (KeyValuePair<float, NKMTimeStamp> keyValuePair in this.m_EventTimeStampState)
			{
				NKMTimeStamp value2 = keyValuePair.Value;
				this.m_NKMGame.GetObjectPool().CloseObj(value2);
			}
			this.m_EventTimeStampState.Clear();
			foreach (NKMDamageEffect nkmdamageEffect in this.m_linklistDamageEffect)
			{
				nkmdamageEffect.SetDie(false, false);
			}
			this.m_linklistDamageEffect.Clear();
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x00068A3C File Offset: 0x00066C3C
		public override void Close()
		{
			this.Init();
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x00068A44 File Offset: 0x00066C44
		public virtual bool SetDamageEffect(NKMGame cNKMGame, NKMDamageEffectManager cDEManager, NKMUnitSkillTemplet cSkillTemplet, int masterUnitPhase, short deUID, string deTempletID, short masterGameUnitUID, short targetGameUnitUID, float fX, float fY, float fZ, bool bRight, float offsetX = 0f, float offsetY = 0f, float offsetZ = 0f, float fAddRotate = 0f, bool bUseZScale = true, float fSpeedFactorX = 0f, float fSpeedFactorY = 0f)
		{
			this.m_NKMGame = cNKMGame;
			this.m_DEManager = cDEManager;
			this.m_DamageEffectUID = deUID;
			this.m_DETemplet = NKMDETempletManager.GetDETemplet(deTempletID);
			this.m_DEData.m_MasterGameUnitUID = masterGameUnitUID;
			this.m_DEData.m_TargetGameUnitUID = targetGameUnitUID;
			this.m_DEData.m_MasterUnit = null;
			this.m_DEData.m_MasterUnit = this.GetMasterUnit();
			if (this.m_DEData.m_MasterUnit == null)
			{
				return false;
			}
			this.m_DEData.m_NKM_TEAM_TYPE = this.m_DEData.m_MasterUnit.GetUnitDataGame().m_NKM_TEAM_TYPE;
			this.m_DEData.m_StatData = this.m_DEData.m_MasterUnit.GetUnitFrameData().m_StatData;
			this.m_DEData.m_UnitData = this.m_DEData.m_MasterUnit.GetUnitData();
			this.m_DEData.m_fOffsetX = offsetX;
			this.m_DEData.m_fOffsetY = offsetY;
			this.m_DEData.m_fOffsetZ = offsetZ;
			this.m_DEData.m_fAddRotate = fAddRotate;
			this.m_DEData.m_bUseZScale = bUseZScale;
			this.m_DEData.m_fSpeedFactorX = fSpeedFactorX;
			this.m_DEData.m_fSpeedFactorY = fSpeedFactorY;
			this.m_DEData.m_bRight = bRight;
			if (this.m_DEData.m_bRight)
			{
				this.m_DEData.m_DirVector.x = 1f;
			}
			else
			{
				this.m_DEData.m_DirVector.x = -1f;
			}
			this.m_DEData.m_DirVectorTrackX.SetNowValue(this.m_DEData.m_DirVector.x);
			this.m_UnitSkillTemplet = cSkillTemplet;
			this.m_MasterUnitPhase = masterUnitPhase;
			this.m_TargetUnit = this.GetTargetUnit();
			this.SetPos(fX, fY, fZ, true);
			NKMTrackingFloat targetDirSpeed = this.m_DEData.m_TargetDirSpeed;
			NKMDamageEffectTemplet detemplet = this.m_DETemplet;
			targetDirSpeed.SetNowValue((detemplet != null) ? detemplet.m_fTargetDirSpeed : 0f);
			this.MakeDirVec();
			this.SaveEventMoveXPosition(fX);
			this.StateChange("DES_BASE", true);
			return true;
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x00068C3F File Offset: 0x00066E3F
		public void SetHoldFollowData(TRACKING_DATA_TYPE followTrackingType, float followTime, float resetTime)
		{
			this.m_DEData.m_FollowTrackingDataType = followTrackingType;
			this.m_DEData.m_fFollowTime = followTime;
			this.m_DEData.m_fFollowResetTime = resetTime;
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x00068C65 File Offset: 0x00066E65
		public void SetStateEndDie(bool bStateEndStop)
		{
			this.m_DEData.m_bStateEndStop = bStateEndStop;
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x00068C73 File Offset: 0x00066E73
		public bool GetStateEndDie()
		{
			return this.m_DEData.m_bStateEndStop;
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x00068C80 File Offset: 0x00066E80
		public void SetFollowPos(float fX, float fY, float fZ, bool bUseOffset)
		{
			if (this.m_DEData.m_FollowTrackingDataType == TRACKING_DATA_TYPE.TDT_INVALID)
			{
				return;
			}
			if (this.m_DEData.m_fFollowTime == 0f || this.m_DEData.m_FollowTrackingDataType == TRACKING_DATA_TYPE.TDT_NORMAL)
			{
				this.SetPos(fX, fY, fZ, bUseOffset);
				return;
			}
			float num = fX;
			float num2 = fY;
			float num3 = fZ;
			if (bUseOffset)
			{
				if (this.m_DEData.m_bRight)
				{
					num += this.m_DEData.m_fOffsetX;
				}
				else
				{
					num -= this.m_DEData.m_fOffsetX;
				}
				num2 += this.m_DEData.m_fOffsetY;
				num3 += this.m_DEData.m_fOffsetZ;
			}
			this.m_DEData.m_fFollowUpdateTime -= this.m_fDeltaTime;
			if (this.m_DEData.m_fFollowUpdateTime > 0f)
			{
				return;
			}
			if (!this.m_EventMovePosX.IsTracking() || this.m_EventMovePosX.GetTargetValue() != num)
			{
				this.m_EventMovePosX.SetTracking(num, this.m_DEData.m_fFollowTime, this.m_DEData.m_FollowTrackingDataType);
			}
			if (!this.m_EventMovePosJumpY.IsTracking() || this.m_EventMovePosJumpY.GetTargetValue() != num2)
			{
				this.m_EventMovePosJumpY.SetTracking(num2, this.m_DEData.m_fFollowTime, this.m_DEData.m_FollowTrackingDataType);
			}
			if (!this.m_EventMovePosZ.IsTracking() || this.m_EventMovePosZ.GetTargetValue() != num3)
			{
				this.m_EventMovePosZ.SetTracking(num3, this.m_DEData.m_fFollowTime, this.m_DEData.m_FollowTrackingDataType);
			}
			this.m_DEData.m_fFollowUpdateTime = this.m_DEData.m_fFollowResetTime;
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x00068E10 File Offset: 0x00067010
		public void SetPos(float fX, float fY, float fZ, bool bUseOffset)
		{
			this.m_DEData.m_PosX = fX;
			this.m_DEData.m_PosZ = fZ;
			this.m_DEData.m_JumpYPos = fY;
			if (bUseOffset)
			{
				if (this.m_DEData.m_bRight)
				{
					this.m_DEData.m_PosX += this.m_DEData.m_fOffsetX;
				}
				else
				{
					this.m_DEData.m_PosX -= this.m_DEData.m_fOffsetX;
				}
				this.m_DEData.m_PosZ += this.m_DEData.m_fOffsetZ;
				this.m_DEData.m_JumpYPos += this.m_DEData.m_fOffsetY;
			}
			if (this.GetTemplet().m_bLandConnect)
			{
				this.m_DEData.m_JumpYPos = 0f;
			}
			this.m_DEData.m_PosXBefore = this.m_DEData.m_PosX;
			this.m_DEData.m_PosZBefore = this.m_DEData.m_PosZ;
			this.m_DEData.m_JumpYPosBefore = this.m_DEData.m_JumpYPos;
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x00068F2A File Offset: 0x0006712A
		public virtual void SetRight(bool bRight)
		{
			if (this.m_DEData.m_FollowTrackingDataType == TRACKING_DATA_TYPE.TDT_INVALID)
			{
				return;
			}
			this.m_DEData.m_bRight = bRight;
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x00068F48 File Offset: 0x00067148
		public virtual void Update(float deltaTime)
		{
			this.m_fDeltaTime = deltaTime;
			this.m_bSortUnitDirty = true;
			if (this.GetDEData().m_PosXBefore.IsNearlyEqual(-1f, 1E-05f))
			{
				this.GetDEData().m_PosXBefore = this.GetDEData().m_PosX;
			}
			if (this.GetDEData().m_PosZBefore.IsNearlyEqual(-1f, 1E-05f))
			{
				this.GetDEData().m_PosZBefore = this.GetDEData().m_PosZ;
			}
			if (this.GetDEData().m_JumpYPosBefore.IsNearlyEqual(-1f, 1E-05f))
			{
				this.GetDEData().m_JumpYPosBefore = this.GetDEData().m_JumpYPos;
			}
			this.m_TargetUnit = this.GetTargetUnit();
			if (!this.m_DEData.m_bStateFirstFrame)
			{
				this.StateTimeUpdate();
				this.AnimTimeUpdate();
			}
			this.DoStateEndStart();
			this.StateUpdate();
			this.StateEvent();
			this.DieCheck();
			this.m_DEData.m_bStateFirstFrame = false;
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x00069044 File Offset: 0x00067244
		public List<NKMUnit> GetSortUnitListByNearDist()
		{
			if (this.m_bSortUnitDirty)
			{
				this.m_listSortUnit.Clear();
				List<NKMUnit> unitChain = this.m_NKMGame.GetUnitChain();
				for (int i = 0; i < unitChain.Count; i++)
				{
					NKMUnit nkmunit = unitChain[i];
					if (nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE != NKM_UNIT_STYLE_TYPE.NUST_ENV)
					{
						nkmunit.CalcSortDist(this.GetDEData().m_PosX);
						this.m_listSortUnit.Add(nkmunit);
					}
				}
				this.m_listSortUnit.Sort((NKMUnit a, NKMUnit b) => a.GetTempSortDist().CompareTo(b.GetTempSortDist()));
				this.m_bSortUnitDirty = false;
			}
			return this.m_listSortUnit;
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x000690F4 File Offset: 0x000672F4
		protected void DieCheck()
		{
			if (this.m_EffectStateNow == null)
			{
				return;
			}
			if (this.m_EffectStateNow.m_NKM_LIFE_TIME_TYPE == NKM_LIFE_TIME_TYPE.NLTT_TIME && this.m_DEData.m_fLifeTimeMax <= this.m_DEData.m_fStateTime)
			{
				this.SetDie(false, true);
			}
			if (this.m_EffectStateNow.m_NKM_LIFE_TIME_TYPE == NKM_LIFE_TIME_TYPE.NLTT_ANIM_COUNT && this.m_DEData.m_AnimPlayCount >= this.m_EffectStateNow.m_LifeTimeAnimCount)
			{
				this.SetDie(false, true);
			}
			if (this.m_DETemplet.m_bDamageCountMaxDie && this.m_DEData.m_DamageCountNow >= this.m_DETemplet.m_DamageCountMax)
			{
				this.SetDie(false, true);
			}
			if (this.m_DETemplet.m_fTargetDistDie > 0f)
			{
				bool flag = false;
				if (this.m_TargetUnit == null || this.m_TargetUnit.IsDyingOrDie())
				{
					flag = true;
				}
				if (!this.m_DETemplet.m_bTargetDistDieOnlyTargetDie || flag)
				{
					bool flag2 = false;
					if (Math.Abs(this.m_DEData.m_fLastTargetPosX - this.m_DEData.m_PosX) <= this.m_DETemplet.m_fTargetDistDie)
					{
						flag2 = true;
					}
					if (!flag2 && !this.m_DEData.m_PosXBefore.IsNearlyEqual(-1f, 1E-05f))
					{
						if (this.m_DEData.m_PosXBefore >= this.m_DEData.m_fLastTargetPosX && this.m_DEData.m_PosX < this.m_DEData.m_fLastTargetPosX)
						{
							flag2 = true;
						}
						if (this.m_DEData.m_PosX >= this.m_DEData.m_fLastTargetPosX && this.m_DEData.m_PosXBefore < this.m_DEData.m_fLastTargetPosX)
						{
							flag2 = true;
						}
					}
					if (flag2)
					{
						this.SetDie(false, true);
					}
				}
			}
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x00069292 File Offset: 0x00067492
		protected void StateTimeUpdate()
		{
			this.m_DEData.m_fStateTimeBack = this.m_DEData.m_fStateTime;
			this.m_DEData.m_fStateTime += this.m_fDeltaTime;
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x000692C4 File Offset: 0x000674C4
		protected void AnimTimeUpdate()
		{
			if (this.m_EffectStateNow == null)
			{
				return;
			}
			if (this.m_DEData.m_fAnimTime >= this.m_DEData.m_fAnimTimeMax)
			{
				if (this.m_EffectStateNow.m_bAnimLoop)
				{
					this.m_DEData.m_fAnimTimeBack = 0f;
					this.m_DEData.m_fAnimTime = 0f;
					foreach (KeyValuePair<float, NKMTimeStamp> keyValuePair in this.m_EventTimeStampAnim)
					{
						NKMTimeStamp value = keyValuePair.Value;
						this.m_NKMGame.GetObjectPool().CloseObj(value);
					}
					this.m_EventTimeStampAnim.Clear();
				}
				this.m_DEData.m_AnimPlayCount += 1f;
			}
			this.m_DEData.m_fAnimTimeBack = this.m_DEData.m_fAnimTime;
			if (this.m_DETemplet.m_UseMasterAnimSpeed && this.GetMasterUnit() != null && this.GetMasterUnit().GetUnitFrameData() != null)
			{
				this.m_DEData.m_fAnimTime += this.m_fDeltaTime * this.GetMasterUnit().GetUnitFrameData().m_fAnimSpeed;
				return;
			}
			this.m_DEData.m_fAnimTime += this.m_fDeltaTime * this.m_EffectStateNow.m_fAnimSpeed;
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x00069404 File Offset: 0x00067604
		public void DoStateEndStart()
		{
			if (this.m_StateNameNext.Length > 1)
			{
				this.StateEnd();
				this.m_StateNameNow = this.m_StateNameNext;
				this.m_StateNameNext = "";
				if (this.m_DETemplet == null)
				{
					Log.Error("m_DETemplet is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDamageEffect.cs", 598);
					return;
				}
				this.m_EffectStateNow = this.m_DETemplet.GetState(this.m_StateNameNow);
				if (this.m_EffectStateNow == null)
				{
					Log.Error(this.m_DETemplet.m_MainEffectName + " m_EffectStateNow is null stateName: " + this.m_StateNameNow, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDamageEffect.cs", 605);
					return;
				}
				this.StateStart();
			}
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x000694AC File Offset: 0x000676AC
		protected virtual void StateEnd()
		{
			if (this.m_EffectStateNow == null)
			{
				return;
			}
			this.ProcessEventDirSpeed(true);
			this.ProcessEventSpeedX(true);
			this.ProcessEventSpeedY(true);
			this.ProcessEventMove(true);
			this.ProcessEventSound(true);
			this.ProcessEventCameraCrash(true);
			this.ProcessEventDissolve(true);
			this.ProcessEventEffect(true);
			this.ProcessEventDamageEffect(true);
			this.ProcessEventBuff(true);
			this.ProcessEventStatus(true);
			this.ProcessEventHeal(true);
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x00069518 File Offset: 0x00067718
		protected virtual void StateStart()
		{
			this.m_DEData.m_bStateFirstFrame = true;
			if (this.m_DETemplet == null)
			{
				Log.Error("m_DETemplet is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDamageEffect.cs", 636);
				return;
			}
			if (this.m_EffectStateNow == null)
			{
				Log.Error(this.m_DETemplet.m_MainEffectName + " m_EffectStateNow is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDamageEffect.cs", 641);
				return;
			}
			this.m_DEData.m_fAnimTimeBack = 0f;
			this.m_DEData.m_fAnimTime = 0f;
			this.m_DEData.m_fAnimTimeMax = NKMAnimDataManager.GetAnimTimeMax(this.m_DETemplet.m_MainEffectName, this.m_DETemplet.m_MainEffectName, this.m_EffectStateNow.m_AnimName);
			if (this.m_DEData.m_fAnimTimeMax.IsNearlyZero(1E-05f))
			{
				Log.Error("NKMDamageEffect NoExistAnim: " + this.m_DETemplet.m_MainEffectName + " : " + this.m_EffectStateNow.m_AnimName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDamageEffect.cs", 650);
				return;
			}
			this.m_DEData.m_AnimPlayCount = 0f;
			this.m_DEData.m_fStateTime = 0f;
			this.m_DEData.m_fStateTimeBack = 0f;
			foreach (KeyValuePair<float, NKMTimeStamp> keyValuePair in this.m_EventTimeStampAnim)
			{
				NKMTimeStamp value = keyValuePair.Value;
				this.m_NKMGame.GetObjectPool().CloseObj(value);
			}
			this.m_EventTimeStampAnim.Clear();
			foreach (KeyValuePair<float, NKMTimeStamp> keyValuePair in this.m_EventTimeStampState)
			{
				NKMTimeStamp value2 = keyValuePair.Value;
				this.m_NKMGame.GetObjectPool().CloseObj(value2);
			}
			this.m_EventTimeStampState.Clear();
			this.SetLifeTimeMax();
			this.SeeTarget();
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x000696DC File Offset: 0x000678DC
		protected virtual void StateUpdate()
		{
			if (this.m_EffectStateNow == null)
			{
				return;
			}
			this.m_DEData.m_fSeeTargetTimeNow -= this.m_fDeltaTime;
			if (this.m_DEData.m_fSeeTargetTimeNow <= 0f)
			{
				this.m_DEData.m_fSeeTargetTimeNow = this.m_DETemplet.m_fSeeTargetTime;
				this.SeeTarget();
			}
			this.ProcessTarget();
			this.ProcessEventTargetDirSpeed();
			this.ProcessEventDirSpeed(false);
			this.ProcessEventSpeedX(false);
			this.ProcessEventSpeedY(false);
			this.ProcessEventMove(false);
			this.ProcessEventAttack();
			this.ProcessEventSound(false);
			this.ProcessEventCameraCrash(false);
			this.ProcessEventDissolve(false);
			this.ProcessEventEffect(false);
			this.ProcessEventDamageEffect(false);
			this.ProcessEventBuff(false);
			this.ProcessEventStatus(false);
			this.ProcessEventHeal(false);
			this.PhysicProcess();
			this.MapEdgeProcess();
			foreach (KeyValuePair<float, NKMTimeStamp> keyValuePair in this.m_EventTimeStampAnim)
			{
				keyValuePair.Value.m_FramePass = true;
			}
			foreach (KeyValuePair<float, NKMTimeStamp> keyValuePair in this.m_EventTimeStampState)
			{
				keyValuePair.Value.m_FramePass = true;
			}
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x00069804 File Offset: 0x00067A04
		protected virtual void StateEvent()
		{
			if (this.m_EffectStateNow == null)
			{
				return;
			}
			if (this.m_EffectStateNow.m_StateTimeChangeStateTime >= 0f && this.m_DEData.m_fStateTime >= this.m_EffectStateNow.m_StateTimeChangeStateTime)
			{
				this.StateChange(this.m_EffectStateNow.m_StateTimeChangeState, true);
				return;
			}
			if (this.m_EffectStateNow.m_AnimEndChangeState.Length > 1 && this.m_DEData.m_fAnimTime >= this.m_DEData.m_fAnimTimeMax)
			{
				this.StateChange(this.m_EffectStateNow.m_AnimEndChangeState, true);
				return;
			}
			if (this.m_EffectStateNow.m_FootOnLandChangeState.Length > 1 && this.m_DEData.m_bFootOnLand)
			{
				this.StateChange(this.m_EffectStateNow.m_FootOnLandChangeState, true);
				return;
			}
			if (this.m_EffectStateNow.m_DamageCountChangeStateCount > 0 && this.m_DEData.m_DamageCountNow >= this.m_EffectStateNow.m_DamageCountChangeStateCount)
			{
				this.StateChange(this.m_EffectStateNow.m_DamageCountChangeState, true);
				return;
			}
			if (this.m_EffectStateNow.m_TargetDistFarChangeStateDist > 0f && !this.m_DEData.m_fLastTargetPosX.IsNearlyZero(1E-05f) && Math.Abs(this.m_DEData.m_fLastTargetPosX - this.m_DEData.m_PosX) >= this.m_EffectStateNow.m_TargetDistFarChangeStateDist)
			{
				this.StateChange(this.m_EffectStateNow.m_TargetDistFarChangeState, true);
				return;
			}
			if (this.m_EffectStateNow.m_TargetDistNearChangeStateDist > 0f && !this.m_DEData.m_fLastTargetPosX.IsNearlyZero(1E-05f))
			{
				bool flag = false;
				if (Math.Abs(this.m_DEData.m_fLastTargetPosX - this.m_DEData.m_PosX) <= this.m_EffectStateNow.m_TargetDistNearChangeStateDist)
				{
					flag = true;
				}
				if (!flag && !this.m_DEData.m_PosXBefore.IsNearlyEqual(-1f, 1E-05f))
				{
					if (this.m_DEData.m_PosXBefore >= this.m_DEData.m_fLastTargetPosX && this.m_DEData.m_PosX < this.m_DEData.m_fLastTargetPosX)
					{
						flag = true;
					}
					if (this.m_DEData.m_PosX >= this.m_DEData.m_fLastTargetPosX && this.m_DEData.m_PosXBefore < this.m_DEData.m_fLastTargetPosX)
					{
						flag = true;
					}
				}
				if (flag)
				{
					this.StateChange(this.m_EffectStateNow.m_TargetDistNearChangeState, true);
					return;
				}
			}
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x00069A5E File Offset: 0x00067C5E
		public void StateChangeByUnitState(string stateName, bool bForceChange = true)
		{
			this.StateChange(stateName, bForceChange);
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x00069A68 File Offset: 0x00067C68
		protected virtual void StateChange(string stateName, bool bForceChange = true)
		{
			if (!bForceChange && stateName.CompareTo(this.m_EffectStateNow.m_StateName) == 0)
			{
				return;
			}
			if (this.m_DETemplet == null)
			{
				Log.Error("StateChange m_DETemplet is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDamageEffect.cs", 815);
				return;
			}
			if (this.m_DETemplet.GetState(stateName) == null)
			{
				Log.Error("StateChange " + this.m_DETemplet.m_MainEffectName + " GetState is null stateName: " + stateName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDamageEffect.cs", 822);
				return;
			}
			this.m_StateNameNext = stateName;
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x00069AEC File Offset: 0x00067CEC
		protected void SetLifeTimeMax()
		{
			if (this.m_EffectStateNow == null)
			{
				return;
			}
			NKM_LIFE_TIME_TYPE nkm_LIFE_TIME_TYPE = this.m_EffectStateNow.m_NKM_LIFE_TIME_TYPE;
			if (nkm_LIFE_TIME_TYPE == NKM_LIFE_TIME_TYPE.NLTT_INFINITY)
			{
				this.m_DEData.m_fLifeTimeMax = 0f;
				return;
			}
			if (nkm_LIFE_TIME_TYPE - NKM_LIFE_TIME_TYPE.NLTT_TIME > 1)
			{
				return;
			}
			this.m_DEData.m_fLifeTimeMax = this.m_EffectStateNow.m_LifeTime;
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x00069B3F File Offset: 0x00067D3F
		public bool IsEnd()
		{
			return this.m_DEData.m_bDie;
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x00069B4C File Offset: 0x00067D4C
		public virtual void SetDie(bool bForce = false, bool bDieEvent = true)
		{
			if (this.m_DEData.m_bDie)
			{
				return;
			}
			if (bDieEvent)
			{
				this.ProcessDieEventAttack();
				this.ProcessDieEventEffect();
				this.ProcessDieEventDamageEffect();
				this.ProcessDieEventSound();
			}
			this.m_DEData.m_bDie = true;
			if (bForce)
			{
				this.m_DEManager.DeleteDE(this.m_DamageEffectUID);
			}
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x00069BA4 File Offset: 0x00067DA4
		protected void SeeTarget()
		{
			if (this.m_TargetUnit != null)
			{
				bool bRight = this.m_DEData.m_bRight;
				if (this.m_DETemplet.m_bSeeTarget)
				{
					if (this.m_DEData.m_PosX < this.m_TargetUnit.GetUnitSyncData().m_PosX)
					{
						this.m_DEData.m_bRight = true;
					}
					else
					{
						this.m_DEData.m_bRight = false;
					}
				}
				if (this.m_DETemplet.m_bSeeTargetSpeed && bRight != this.m_DEData.m_bRight)
				{
					this.m_DEData.m_SpeedX = -this.m_DEData.m_SpeedX;
				}
			}
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x00069C40 File Offset: 0x00067E40
		protected void MakeDirVec()
		{
			NKMDamageEffectTemplet detemplet = this.m_DETemplet;
			if (detemplet != null && detemplet.m_bUseTargetDir)
			{
				if (this.m_TargetUnit != null)
				{
					float num = this.m_TargetUnit.GetUnitSyncData().m_PosX - this.m_DEData.m_PosX;
					float num2 = this.m_TargetUnit.GetUnitSyncData().m_JumpYPos + this.m_TargetUnit.GetUnitTemplet().m_UnitSizeY * 0.5f - this.m_DEData.m_JumpYPos;
					if (this.m_DEData.m_TargetDirSpeed.GetNowValue() > 0f)
					{
						this.m_DEData.m_DirVectorTrackX.SetTracking(num, this.m_DEData.m_TargetDirSpeed.GetNowValue(), TRACKING_DATA_TYPE.TDT_NORMAL);
						this.m_DEData.m_DirVectorTrackY.SetTracking(num2, this.m_DEData.m_TargetDirSpeed.GetNowValue(), TRACKING_DATA_TYPE.TDT_NORMAL);
					}
					else
					{
						this.m_DEData.m_DirVectorTrackX.SetNowValue(num);
						this.m_DEData.m_DirVectorTrackY.SetNowValue(num2);
					}
					this.m_DEData.m_DirVectorTrackX.Update(this.m_fDeltaTime);
					this.m_DEData.m_DirVectorTrackY.Update(this.m_fDeltaTime);
					this.m_DEData.m_DirVector.x = this.m_DEData.m_DirVectorTrackX.GetNowValue();
					this.m_DEData.m_DirVector.y = this.m_DEData.m_DirVectorTrackY.GetNowValue();
					this.m_DEData.m_DirVector.Normalize();
				}
			}
			else
			{
				this.m_DEData.m_DirVector.x = this.m_DEData.m_SpeedX;
				this.m_DEData.m_DirVector.y = this.m_DEData.m_SpeedY;
				if (!this.m_DEData.m_fSpeedFactorX.IsNearlyZero(1E-05f))
				{
					NKMDamageEffectData dedata = this.m_DEData;
					dedata.m_DirVector.x = dedata.m_DirVector.x * this.m_DEData.m_fSpeedFactorX;
				}
				if (!this.m_DEData.m_fSpeedFactorY.IsNearlyZero(1E-05f))
				{
					NKMDamageEffectData dedata2 = this.m_DEData;
					dedata2.m_DirVector.y = dedata2.m_DirVector.y * this.m_DEData.m_fSpeedFactorY;
				}
				if (!this.m_DEData.m_fAddRotate.IsNearlyZero(1E-05f))
				{
					NKMMathf.RotateVector2(this.m_DEData.m_DirVector.x, this.m_DEData.m_DirVector.y, this.m_DEData.m_fAddRotate, out this.m_DEData.m_DirVector.x, out this.m_DEData.m_DirVector.y);
				}
				if (!this.m_DEData.m_bRight)
				{
					this.m_DEData.m_DirVector.x = -this.m_DEData.m_DirVector.x;
				}
			}
			if (this.m_DEData.m_DirVector.x.IsNearlyZero(1E-05f) && this.m_DEData.m_DirVector.y.IsNearlyZero(1E-05f) && this.m_DEData.m_DirVector.z.IsNearlyZero(1E-05f))
			{
				if (this.m_DEData.m_bRight)
				{
					this.m_DEData.m_DirVector.x = 1f;
				}
				else
				{
					this.m_DEData.m_DirVector.x = -1f;
				}
				this.m_DEData.m_DirVectorTrackX.SetNowValue(this.m_DEData.m_DirVector.x);
			}
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x00069FA8 File Offset: 0x000681A8
		protected virtual void PhysicProcess()
		{
			if (this.m_EffectStateNow == null)
			{
				return;
			}
			if (this.m_DETemplet == null)
			{
				return;
			}
			this.m_DEData.m_PosXBefore = this.m_DEData.m_PosX;
			this.m_DEData.m_PosZBefore = this.m_DEData.m_PosZ;
			this.m_DEData.m_JumpYPosBefore = this.m_DEData.m_JumpYPos;
			if (this.m_EffectStateNow.m_bNoMove || this.m_DETemplet.m_bNoMove)
			{
				this.m_DEData.m_SpeedX = 0f;
				this.m_DEData.m_SpeedY = 0f;
				this.m_DEData.m_SpeedZ = 0f;
			}
			if (this.m_DEData.m_bFootOnLand && this.m_DETemplet.m_bLandStruck)
			{
				this.m_DEData.m_SpeedX = 0f;
				this.m_DEData.m_SpeedY = 0f;
				this.m_DEData.m_SpeedZ = 0f;
			}
			if (this.GetTemplet().m_bLandConnect)
			{
				this.m_DEData.m_JumpYPos = 0f;
			}
			this.MakeDirVec();
			this.m_DEData.m_PosX += this.m_DEData.m_DirVector.x * this.m_fDeltaTime;
			this.m_DEData.m_JumpYPos += this.m_DEData.m_DirVector.y * this.m_fDeltaTime;
			this.m_DEData.m_PosZ += this.m_DEData.m_DirVector.z * this.m_fDeltaTime;
			bool flag = false;
			if (this.m_TargetUnit != null && !this.m_TargetUnit.IsDyingOrDie() && this.m_DETemplet.m_bUseTargetDir && Math.Abs(this.m_DEData.m_fLastTargetPosX - this.m_DEData.m_PosX) < 40f)
			{
				flag = true;
			}
			if (!this.m_DEData.m_fDirSpeed.IsNearlyZero(1E-05f) && !flag)
			{
				if (this.m_DEData.m_fSpeedFactorX.IsNearlyZero(1E-05f))
				{
					this.m_DEData.m_PosX += this.m_DEData.m_DirVector.x * this.m_DEData.m_fDirSpeed * this.m_fDeltaTime;
					this.m_DEData.m_PosZ += this.m_DEData.m_DirVector.z * this.m_DEData.m_fDirSpeed * this.m_fDeltaTime;
					this.m_DEData.m_JumpYPos += this.m_DEData.m_DirVector.y * this.m_DEData.m_fDirSpeed * this.m_fDeltaTime;
				}
				else
				{
					this.m_DEData.m_PosX += this.m_DEData.m_DirVector.x * this.m_DEData.m_fDirSpeed * this.m_fDeltaTime * this.m_DEData.m_fSpeedFactorX;
					this.m_DEData.m_PosZ += this.m_DEData.m_DirVector.z * this.m_DEData.m_fDirSpeed * this.m_fDeltaTime * this.m_DEData.m_fSpeedFactorX;
					this.m_DEData.m_JumpYPos += this.m_DEData.m_DirVector.y * this.m_DEData.m_fDirSpeed * this.m_fDeltaTime * this.m_DEData.m_fSpeedFactorX;
				}
			}
			if (this.m_DETemplet.m_bUseTargetDir)
			{
				if (this.m_DEData.m_fSpeedFactorY.IsNearlyZero(1E-05f))
				{
					this.m_DEData.m_JumpYPos += this.m_DEData.m_SpeedY * this.m_fDeltaTime;
				}
				else
				{
					this.m_DEData.m_JumpYPos += this.m_DEData.m_SpeedY * this.m_DEData.m_fSpeedFactorY * this.m_fDeltaTime;
				}
				float num = this.m_DEData.m_fLastTargetPosX - this.m_DEData.m_PosXBefore;
				float num2 = this.m_DEData.m_fLastTargetPosX - this.m_DEData.m_PosX;
				if (num * num2 <= 0f)
				{
					this.m_DEData.m_PosX = this.m_DEData.m_fLastTargetPosX;
				}
			}
			bool flag2 = this.m_DEData.m_SpeedX >= 0f;
			if (flag2)
			{
				this.m_DEData.m_SpeedX -= this.m_DETemplet.m_fReloadAccel * this.m_fDeltaTime;
				if (this.m_DEData.m_SpeedX <= 0f)
				{
					this.m_DEData.m_SpeedX = 0f;
				}
			}
			else
			{
				this.m_DEData.m_SpeedX += this.m_DETemplet.m_fReloadAccel * this.m_fDeltaTime;
				if (this.m_DEData.m_SpeedX > 0f)
				{
					this.m_DEData.m_SpeedX = 0f;
				}
			}
			bool flag3 = this.m_DEData.m_SpeedZ >= 0f;
			if (flag3)
			{
				this.m_DEData.m_SpeedZ -= this.m_DETemplet.m_fReloadAccel * this.m_fDeltaTime;
				if (this.m_DEData.m_SpeedZ <= 0f)
				{
					this.m_DEData.m_SpeedZ = 0f;
				}
			}
			else
			{
				this.m_DEData.m_SpeedZ += this.m_DETemplet.m_fReloadAccel * this.m_fDeltaTime;
				if (this.m_DEData.m_SpeedZ > 0f)
				{
					this.m_DEData.m_SpeedZ = 0f;
				}
			}
			this.m_DEData.m_SpeedY -= this.m_DETemplet.m_fGAccel * this.m_fDeltaTime;
			if (this.m_DEData.m_SpeedY <= this.m_DETemplet.m_fMaxGSpeed)
			{
				this.m_DEData.m_SpeedY = this.m_DETemplet.m_fMaxGSpeed;
			}
			if (this.m_EffectStateNow.m_bNoMove || this.m_DETemplet.m_bNoMove)
			{
				this.m_EventMovePosX.StopTracking();
				this.m_EventMovePosZ.StopTracking();
				this.m_EventMovePosJumpY.StopTracking();
			}
			this.m_EventMovePosX.Update(this.m_fDeltaTime);
			this.m_EventMovePosZ.Update(this.m_fDeltaTime);
			this.m_EventMovePosJumpY.Update(this.m_fDeltaTime);
			if (this.m_EventMovePosX.IsTracking())
			{
				this.m_DEData.m_SpeedX = 0f;
				this.m_DEData.m_PosX = this.m_EventMovePosX.GetNowValue();
			}
			if (this.m_EventMovePosZ.IsTracking())
			{
				this.m_DEData.m_SpeedZ = 0f;
				this.m_DEData.m_PosZ = this.m_EventMovePosZ.GetNowValue();
			}
			if (this.m_EventMovePosJumpY.IsTracking())
			{
				this.m_DEData.m_SpeedY = 0f;
				this.m_DEData.m_JumpYPos = this.m_EventMovePosJumpY.GetNowValue();
			}
			if (this.GetTemplet().m_bLandConnect)
			{
				this.m_DEData.m_JumpYPos = 0f;
			}
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x0006A6B8 File Offset: 0x000688B8
		protected void MapEdgeProcess()
		{
			if (this.m_DEData.m_JumpYPos <= 0f)
			{
				this.m_DEData.m_JumpYPos = 0f;
				if (this.m_DETemplet.m_bLandBind && this.m_DEData.m_SpeedY <= -10f)
				{
					this.m_DEData.m_SpeedY = -this.m_DEData.m_SpeedY * 0.5f;
				}
				else
				{
					this.m_DEData.m_SpeedY = 0f;
					this.m_DEData.m_bFootOnLand = true;
				}
			}
			else
			{
				this.m_DEData.m_bFootOnLand = false;
			}
			if (this.m_DETemplet.m_bLandEdge)
			{
				if (this.m_DEData.m_PosX > this.m_NKMGame.GetMapTemplet().m_fMaxX)
				{
					this.m_DEData.m_PosX = this.m_NKMGame.GetMapTemplet().m_fMaxX;
				}
				if (this.m_DEData.m_PosX < this.m_NKMGame.GetMapTemplet().m_fMinX)
				{
					this.m_DEData.m_PosX = this.m_NKMGame.GetMapTemplet().m_fMinX;
				}
			}
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x0006A7CC File Offset: 0x000689CC
		private void ProcessTarget()
		{
			if (this.m_DETemplet.m_fFindTargetTime <= 0f)
			{
				return;
			}
			this.m_DEData.m_fFindTargetTimeNow -= this.m_fDeltaTime;
			if (this.m_DEData.m_fFindTargetTimeNow > 0f)
			{
				return;
			}
			this.m_DEData.m_fFindTargetTimeNow = this.m_DETemplet.m_fFindTargetTime;
			if (this.m_DEData.m_TargetGameUnitUID != 0 && this.m_DETemplet.m_bTargetNoChange)
			{
				return;
			}
			this.m_TargetUnit = this.m_NKMGame.FindTarget(this.m_DEData.m_MasterUnit, this.GetSortUnitListByNearDist(), this.m_DETemplet.m_NKM_FIND_TARGET_TYPE, this.m_DEData.m_NKM_TEAM_TYPE, this.m_DEData.m_PosX, this.m_DETemplet.m_fSeeRange, this.m_DETemplet.m_bNoBackTarget, false, this.m_DEData.m_bRight, true, this.m_DETemplet.m_hsFindTargetRolePriority, false);
			if (this.m_TargetUnit != null)
			{
				this.m_DEData.m_TargetGameUnitUID = this.m_TargetUnit.GetUnitDataGame().m_GameUnitUID;
			}
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x0006A8E0 File Offset: 0x00068AE0
		public bool CheckEventCondition(NKMEventCondition cCondition)
		{
			if (this.m_DEData.m_MasterUnit == null)
			{
				return false;
			}
			if (this.m_NKMGame.GetGameData().IsPVP() && !cCondition.m_bUsePVP)
			{
				return false;
			}
			if (this.m_NKMGame.GetGameData().IsPVE() && !cCondition.m_bUsePVE)
			{
				return false;
			}
			if (this.m_DEData.m_MasterUnit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_TYPE != NKM_UNIT_TYPE.NUT_SHIP)
			{
				cCondition.CheckSkillID();
			}
			if (!cCondition.CanUsePhase(this.m_MasterUnitPhase))
			{
				return false;
			}
			if (!cCondition.CanUseBuff(this.m_DEData.m_MasterUnit.GetUnitSyncData().m_dicBuffData))
			{
				return false;
			}
			if (!cCondition.CanUseStatus(this.m_DEData.m_MasterUnit))
			{
				return false;
			}
			if (cCondition.m_bLeaderUnit)
			{
				NKMGameTeamData teamData = this.m_DEData.m_MasterUnit.GetTeamData();
				if (teamData == null)
				{
					return false;
				}
				if (teamData.GetLeaderUnitData() == null)
				{
					return false;
				}
				if (this.m_DEData.m_MasterUnit.IsSummonUnit())
				{
					NKMUnit masterUnit = this.m_DEData.m_MasterUnit.GetMasterUnit();
					if (masterUnit == null)
					{
						return false;
					}
					if (teamData.GetLeaderUnitData().m_UnitUID != masterUnit.GetUnitData().m_UnitUID)
					{
						return false;
					}
				}
				else if (teamData.GetLeaderUnitData().m_UnitUID != this.m_DEData.m_MasterUnit.GetUnitData().m_UnitUID)
				{
					return false;
				}
			}
			if (this.m_DEData.m_MasterUnit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_TYPE != NKM_UNIT_TYPE.NUT_SHIP)
			{
				if (cCondition.m_SkillID != -1)
				{
					int unitSkillLevel = this.m_DEData.m_MasterUnit.GetUnitData().GetUnitSkillLevel(cCondition.m_SkillID);
					if (!cCondition.CanUseSkill(unitSkillLevel))
					{
						return false;
					}
				}
				if (cCondition.m_MasterSkillID != -1)
				{
					int masterSkillLevel = -1;
					NKMUnit masterUnit2 = this.m_DEData.m_MasterUnit.GetMasterUnit();
					if (masterUnit2 != null)
					{
						masterSkillLevel = masterUnit2.GetUnitData().GetUnitSkillLevel(cCondition.m_MasterSkillID);
					}
					if (!cCondition.CanUseMasterSkill(masterSkillLevel))
					{
						return false;
					}
				}
			}
			return cCondition.CheckHPRate(this.m_DEData.m_MasterUnit) && cCondition.CanUseMapPosition(this.m_NKMGame.GetMapTemplet().GetMapFactor(this.m_DEData.m_MasterUnit.GetUnitSyncData().m_PosX, this.m_NKMGame.IsATeam(this.m_DEData.m_MasterUnit.GetUnitDataGame().m_NKM_TEAM_TYPE))) && cCondition.CanUseLevelRange(this.m_DEData.m_MasterUnit) && cCondition.CanUseUnitExist(this.m_NKMGame, this.m_DEData.m_NKM_TEAM_TYPE);
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x0006AB50 File Offset: 0x00068D50
		protected void ProcessEventTargetDirSpeed()
		{
			if (this.m_EffectStateNow == null)
			{
				return;
			}
			if (this.m_EffectStateNow.m_bNoMove || this.m_DETemplet.m_bNoMove)
			{
				return;
			}
			this.m_DEData.m_TargetDirSpeed.Update(this.m_fDeltaTime);
			for (int i = 0; i < this.m_EffectStateNow.m_listNKMEventTargetDirSpeed.Count; i++)
			{
				NKMEventTargetDirSpeed nkmeventTargetDirSpeed = this.m_EffectStateNow.m_listNKMEventTargetDirSpeed[i];
				if (nkmeventTargetDirSpeed != null)
				{
					bool flag = false;
					if (this.EventTimer(nkmeventTargetDirSpeed.m_bAnimTime, nkmeventTargetDirSpeed.m_fEventTime, true))
					{
						flag = true;
					}
					if (flag)
					{
						this.m_DEData.m_TargetDirSpeed.SetTracking(nkmeventTargetDirSpeed.m_fTargetDirSpeed, nkmeventTargetDirSpeed.m_fChangeTime, TRACKING_DATA_TYPE.TDT_NORMAL);
					}
				}
			}
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x0006AC00 File Offset: 0x00068E00
		protected void ProcessEventDirSpeed(bool bStateEnd = false)
		{
			if (this.m_EffectStateNow == null)
			{
				return;
			}
			if (this.m_EffectStateNow.m_bNoMove || this.m_DETemplet.m_bNoMove)
			{
				return;
			}
			for (int i = 0; i < this.m_EffectStateNow.m_listNKMEventDirSpeed.Count; i++)
			{
				NKMEventDirSpeed nkmeventDirSpeed = this.m_EffectStateNow.m_listNKMEventDirSpeed[i];
				if (nkmeventDirSpeed != null && this.CheckEventCondition(nkmeventDirSpeed.m_Condition))
				{
					bool flag = false;
					if (nkmeventDirSpeed.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (this.EventTimer(nkmeventDirSpeed.m_bAnimTime, nkmeventDirSpeed.m_fEventTimeMin, nkmeventDirSpeed.m_fEventTimeMax) && !nkmeventDirSpeed.m_bStateEndTime)
					{
						flag = true;
					}
					if (flag)
					{
						float speed;
						if (nkmeventDirSpeed.m_bAnimTime)
						{
							speed = nkmeventDirSpeed.GetSpeed(this.m_DEData.m_fAnimTime, this.m_DEData.m_fDirSpeed);
						}
						else
						{
							speed = nkmeventDirSpeed.GetSpeed(this.m_DEData.m_fStateTime, this.m_DEData.m_fDirSpeed);
						}
						this.m_DEData.m_fDirSpeed = speed;
					}
				}
			}
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x0006AD0C File Offset: 0x00068F0C
		protected void ProcessEventSpeed(bool bStateEnd = false)
		{
			if (this.m_EffectStateNow == null)
			{
				return;
			}
			if (this.m_EffectStateNow.m_bNoMove || this.m_DETemplet.m_bNoMove)
			{
				return;
			}
			for (int i = 0; i < this.m_EffectStateNow.m_listNKMEventSpeed.Count; i++)
			{
				NKMEventSpeed nkmeventSpeed = this.m_EffectStateNow.m_listNKMEventSpeed[i];
				if (nkmeventSpeed != null && this.CheckEventCondition(nkmeventSpeed.m_Condition))
				{
					bool flag = false;
					if (nkmeventSpeed.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (this.EventTimer(nkmeventSpeed.m_bAnimTime, nkmeventSpeed.m_fEventTimeMin, nkmeventSpeed.m_fEventTimeMax) && !nkmeventSpeed.m_bStateEndTime)
					{
						flag = true;
					}
					if (flag)
					{
						if (!nkmeventSpeed.m_SpeedX.IsNearlyEqual(-1f, 1E-05f))
						{
							float speedX;
							if (nkmeventSpeed.m_bAnimTime)
							{
								speedX = nkmeventSpeed.GetSpeedX(this.m_DEData.m_fAnimTime, this.m_DEData.m_SpeedX);
							}
							else
							{
								speedX = nkmeventSpeed.GetSpeedX(this.m_DEData.m_fStateTime, this.m_DEData.m_SpeedX);
							}
							if (nkmeventSpeed.m_bAdd)
							{
								this.m_DEData.m_SpeedX += speedX;
							}
							else if (nkmeventSpeed.m_bMultiply)
							{
								this.m_DEData.m_SpeedX *= speedX;
							}
							else
							{
								this.m_DEData.m_SpeedX = speedX;
							}
						}
						if (!nkmeventSpeed.m_SpeedY.IsNearlyEqual(-1f, 1E-05f))
						{
							float speedY;
							if (nkmeventSpeed.m_bAnimTime)
							{
								speedY = nkmeventSpeed.GetSpeedY(this.m_DEData.m_fAnimTime, this.m_DEData.m_SpeedY);
							}
							else
							{
								speedY = nkmeventSpeed.GetSpeedY(this.m_DEData.m_fStateTime, this.m_DEData.m_SpeedY);
							}
							if (nkmeventSpeed.m_bAdd)
							{
								this.m_DEData.m_SpeedY += speedY;
							}
							else if (nkmeventSpeed.m_bMultiply)
							{
								this.m_DEData.m_SpeedY *= speedY;
							}
							else
							{
								this.m_DEData.m_SpeedY = speedY;
							}
						}
					}
				}
			}
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x0006AF1C File Offset: 0x0006911C
		protected void ProcessEventSpeedX(bool bStateEnd = false)
		{
			if (this.m_EffectStateNow == null)
			{
				return;
			}
			if (this.m_EffectStateNow.m_bNoMove || this.m_DETemplet.m_bNoMove)
			{
				return;
			}
			for (int i = 0; i < this.m_EffectStateNow.m_listNKMEventSpeedX.Count; i++)
			{
				NKMEventSpeedX nkmeventSpeedX = this.m_EffectStateNow.m_listNKMEventSpeedX[i];
				if (nkmeventSpeedX != null && this.CheckEventCondition(nkmeventSpeedX.m_Condition))
				{
					bool flag = false;
					if (nkmeventSpeedX.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (this.EventTimer(nkmeventSpeedX.m_bAnimTime, nkmeventSpeedX.m_fEventTimeMin, nkmeventSpeedX.m_fEventTimeMax) && !nkmeventSpeedX.m_bStateEndTime)
					{
						flag = true;
					}
					if (flag)
					{
						float speed;
						if (nkmeventSpeedX.m_bAnimTime)
						{
							speed = nkmeventSpeedX.GetSpeed(this.m_DEData.m_fAnimTime, this.m_DEData.m_SpeedX);
						}
						else
						{
							speed = nkmeventSpeedX.GetSpeed(this.m_DEData.m_fStateTime, this.m_DEData.m_SpeedX);
						}
						if (nkmeventSpeedX.m_bAdd)
						{
							this.m_DEData.m_SpeedX += speed;
						}
						else if (nkmeventSpeedX.m_bMultiply)
						{
							this.m_DEData.m_SpeedX *= speed;
						}
						else
						{
							this.m_DEData.m_SpeedX = speed;
						}
					}
				}
			}
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x0006B064 File Offset: 0x00069264
		protected void ProcessEventSpeedY(bool bStateEnd = false)
		{
			if (this.m_EffectStateNow == null)
			{
				return;
			}
			if (this.m_EffectStateNow.m_bNoMove || this.m_DETemplet.m_bNoMove)
			{
				return;
			}
			for (int i = 0; i < this.m_EffectStateNow.m_listNKMEventSpeedY.Count; i++)
			{
				NKMEventSpeedY nkmeventSpeedY = this.m_EffectStateNow.m_listNKMEventSpeedY[i];
				if (nkmeventSpeedY != null && this.CheckEventCondition(nkmeventSpeedY.m_Condition))
				{
					bool flag = false;
					if (nkmeventSpeedY.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (this.EventTimer(nkmeventSpeedY.m_bAnimTime, nkmeventSpeedY.m_fEventTimeMin, nkmeventSpeedY.m_fEventTimeMax) && !nkmeventSpeedY.m_bStateEndTime)
					{
						flag = true;
					}
					if (flag)
					{
						float speed;
						if (nkmeventSpeedY.m_bAnimTime)
						{
							speed = nkmeventSpeedY.GetSpeed(this.m_DEData.m_fAnimTime, this.m_DEData.m_SpeedY);
						}
						else
						{
							speed = nkmeventSpeedY.GetSpeed(this.m_DEData.m_fStateTime, this.m_DEData.m_SpeedY);
						}
						if (nkmeventSpeedY.m_bAdd)
						{
							this.m_DEData.m_SpeedY += speed;
						}
						else if (nkmeventSpeedY.m_bMultiply)
						{
							this.m_DEData.m_SpeedY *= speed;
						}
						else
						{
							this.m_DEData.m_SpeedY = speed;
						}
					}
				}
			}
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x0006B1AC File Offset: 0x000693AC
		protected void ProcessEventMove(bool bStateEnd = false)
		{
			if (this.m_EffectStateNow == null)
			{
				return;
			}
			if (this.m_EffectStateNow.m_bNoMove || this.m_DETemplet.m_bNoMove)
			{
				return;
			}
			for (int i = 0; i < this.m_EffectStateNow.m_listNKMEventMove.Count; i++)
			{
				NKMEventMove cNKMEventMove = this.m_EffectStateNow.m_listNKMEventMove[i];
				this.ApplyEventMove(cNKMEventMove, true, bStateEnd);
			}
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0006B214 File Offset: 0x00069414
		public void ApplyEventMove(NKMEventMove cNKMEventMove, bool bUseEventTimer, bool bStateEnd = false)
		{
			if (cNKMEventMove == null)
			{
				return;
			}
			if (!this.CheckEventCondition(cNKMEventMove.m_Condition))
			{
				return;
			}
			bool flag = false;
			if (cNKMEventMove.m_bStateEndTime && bStateEnd)
			{
				flag = true;
			}
			else if (this.EventTimer(cNKMEventMove.m_bAnimTime, cNKMEventMove.m_fEventTime, true) && !cNKMEventMove.m_bStateEndTime)
			{
				flag = true;
			}
			if (!flag)
			{
				return;
			}
			if (cNKMEventMove.m_bSavePosition || cNKMEventMove.m_MoveBase == NKMEventMove.MoveBase.SAVE_ONLY)
			{
				this.SaveEventMoveXPosition(this.m_DEData.m_PosX);
				if (cNKMEventMove.m_MoveBase == NKMEventMove.MoveBase.SAVE_ONLY)
				{
					return;
				}
			}
			float eventMovePosX = this.GetEventMovePosX(cNKMEventMove, this.IsATeam());
			float eventMovePosY = this.GetEventMovePosY(cNKMEventMove);
			if (cNKMEventMove.m_fSpeed > 0f)
			{
				float fTime = Math.Abs(this.m_DEData.m_PosX - eventMovePosX) / cNKMEventMove.m_fSpeed;
				this.m_EventMovePosX.SetNowValue(this.m_DEData.m_PosX);
				this.m_EventMovePosX.SetTracking(eventMovePosX, fTime, cNKMEventMove.m_MoveTrackingType);
				if (!cNKMEventMove.m_bLandMove)
				{
					this.m_EventMovePosJumpY.SetNowValue(this.m_DEData.m_JumpYPos);
					this.m_EventMovePosJumpY.SetTracking(eventMovePosY, fTime, cNKMEventMove.m_MoveTrackingType);
					return;
				}
			}
			else if (cNKMEventMove.m_MoveTime > 0f)
			{
				this.m_EventMovePosX.SetNowValue(this.m_DEData.m_PosX);
				this.m_EventMovePosX.SetTracking(eventMovePosX, cNKMEventMove.m_MoveTime, cNKMEventMove.m_MoveTrackingType);
				if (!cNKMEventMove.m_bLandMove)
				{
					this.m_EventMovePosJumpY.SetNowValue(this.m_DEData.m_JumpYPos);
					this.m_EventMovePosJumpY.SetTracking(eventMovePosY, cNKMEventMove.m_MoveTime, cNKMEventMove.m_MoveTrackingType);
					return;
				}
			}
			else
			{
				this.m_DEData.m_PosX = eventMovePosX;
				if (!cNKMEventMove.m_bLandMove)
				{
					this.m_DEData.m_JumpYPos = eventMovePosY;
				}
			}
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x0006B3C4 File Offset: 0x000695C4
		public virtual float GetEventMovePosX(NKMEventMove cNKMEventMove, bool isATeam)
		{
			float eventMoveBasePosX = this.GetEventMoveBasePosX(cNKMEventMove, isATeam);
			if (this.GetEventMoveOffsetRight(cNKMEventMove, eventMoveBasePosX, isATeam))
			{
				return eventMoveBasePosX + cNKMEventMove.m_OffsetX;
			}
			return eventMoveBasePosX - cNKMEventMove.m_OffsetX;
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x0006B3F8 File Offset: 0x000695F8
		public float GetEventMoveBasePosX(NKMEventMove cNKMEventMove, bool isATeam)
		{
			NKMEventMove.MoveBase moveBase = cNKMEventMove.m_MoveBase;
			switch (moveBase)
			{
			case NKMEventMove.MoveBase.ME:
				return this.m_DEData.m_PosX;
			case NKMEventMove.MoveBase.TARGET_UNIT:
				return this.m_DEData.m_fLastTargetPosX;
			case NKMEventMove.MoveBase.SUB_TARGET_UNIT:
				break;
			case NKMEventMove.MoveBase.MASTER_UNIT:
				return this.GetMasterUnit().GetUnitSyncData().m_PosX;
			default:
				if (moveBase == NKMEventMove.MoveBase.SAVED_POS)
				{
					return this.m_fEventMoveSavedPositionX;
				}
				break;
			}
			return this.GetMasterUnit().GetEventMoveBasePosX(cNKMEventMove, isATeam);
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x0006B468 File Offset: 0x00069668
		public bool GetEventMoveOffsetRight(NKMEventMove cNKMEventMove, float basePos, bool isATeam)
		{
			NKMEventMove.MoveOffset moveOffset = cNKMEventMove.m_MoveOffset;
			switch (moveOffset)
			{
			case NKMEventMove.MoveOffset.ME:
				return this.m_DEData.m_PosX > basePos;
			case NKMEventMove.MoveOffset.ME_INV:
				return this.m_DEData.m_PosX <= basePos;
			case NKMEventMove.MoveOffset.MY_LOOK_DIR:
				return this.m_DEData.m_bRight;
			case NKMEventMove.MoveOffset.TARGET_UNIT:
				return basePos < this.m_DEData.m_fLastTargetPosX;
			case NKMEventMove.MoveOffset.TARGET_UNIT_INV:
				return basePos >= this.m_DEData.m_fLastTargetPosX;
			case NKMEventMove.MoveOffset.SUB_TARGET_UNIT:
				break;
			case NKMEventMove.MoveOffset.MASTER_UNIT:
				return this.GetMasterUnit().GetUnitSyncData().m_PosX > basePos;
			default:
				if (moveOffset == NKMEventMove.MoveOffset.SAVED_POS)
				{
					return this.m_fEventMoveSavedPositionX > basePos;
				}
				break;
			}
			return this.GetMasterUnit().GetEventMoveOffsetRight(cNKMEventMove, basePos, isATeam);
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x0006B530 File Offset: 0x00069730
		public float GetEventMovePosY(NKMEventMove cNKMEventMove)
		{
			switch (cNKMEventMove.m_MoveBase)
			{
			case NKMEventMove.MoveBase.TARGET_UNIT:
				return this.m_DEData.m_fLastTargetPosJumpY + cNKMEventMove.m_OffsetJumpYPos;
			case NKMEventMove.MoveBase.MASTER_UNIT:
				return this.GetMasterUnit().GetUnitSyncData().m_JumpYPos + cNKMEventMove.m_OffsetJumpYPos;
			}
			return this.m_DEData.m_JumpYPos + cNKMEventMove.m_OffsetJumpYPos;
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x0006B599 File Offset: 0x00069799
		private void SaveEventMoveXPosition(float posX)
		{
			this.m_fEventMoveSavedPositionX = posX;
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x0006B5A2 File Offset: 0x000697A2
		public bool IsATeam()
		{
			return this.m_NKMGame.IsATeam(this.GetDEData().m_NKM_TEAM_TYPE);
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x0006B5BC File Offset: 0x000697BC
		protected virtual void ProcessEventAttack()
		{
			if (this.m_EffectStateNow == null)
			{
				return;
			}
			if (this.m_DEData.m_DamageCountNow >= this.m_DETemplet.m_DamageCountMax)
			{
				return;
			}
			for (int i = 0; i < this.m_EffectStateNow.m_listNKMEventAttack.Count; i++)
			{
				NKMEventAttack nkmeventAttack = this.m_EffectStateNow.m_listNKMEventAttack[i];
				if (nkmeventAttack != null && this.CheckEventCondition(nkmeventAttack.m_Condition))
				{
					bool flag = false;
					if (this.EventTimer(nkmeventAttack.m_bAnimTime, nkmeventAttack.m_fEventTimeMin, true))
					{
						flag = true;
					}
					if (flag)
					{
						NKMDamageInst damageInstAtk = this.GetDamageInstAtk(i);
						if (damageInstAtk != null)
						{
							damageInstAtk.Init();
						}
					}
					flag = false;
					if (this.EventTimer(nkmeventAttack.m_bAnimTime, nkmeventAttack.m_fEventTimeMin, nkmeventAttack.m_fEventTimeMax))
					{
						flag = true;
					}
					if (nkmeventAttack.m_fEventTimeMin.IsNearlyEqual(nkmeventAttack.m_fEventTimeMax, 1E-05f) && this.EventTimer(nkmeventAttack.m_bAnimTime, nkmeventAttack.m_fEventTimeMin, true))
					{
						flag = true;
					}
					if (flag)
					{
						NKMDamageInst damageInstAtk2 = this.GetDamageInstAtk(i);
						if (damageInstAtk2.m_Templet == null)
						{
							damageInstAtk2.m_Templet = NKMDamageManager.GetTempletByStrID(nkmeventAttack.m_DamageTempletName);
							damageInstAtk2.m_AttackerType = NKM_REACTOR_TYPE.NRT_DAMAGE_EFFECT;
							damageInstAtk2.m_AttackerEffectUID = this.m_DamageEffectUID;
							damageInstAtk2.m_AttackerGameUnitUID = this.GetDEData().m_MasterGameUnitUID;
							damageInstAtk2.m_AttackerUnitSkillTemplet = this.m_UnitSkillTemplet;
							damageInstAtk2.m_AttackerTeamType = this.GetDEData().m_NKM_TEAM_TYPE;
						}
						if (this.m_NKMGame.DamageCheck(damageInstAtk2, nkmeventAttack, false) && nkmeventAttack.m_EffectName.Length > 1)
						{
							this.ProcessAttackHitEffect(nkmeventAttack);
						}
					}
				}
			}
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x0006B745 File Offset: 0x00069945
		protected virtual void ProcessEventSound(bool bStateEnd = false)
		{
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x0006B747 File Offset: 0x00069947
		protected virtual void ProcessEventCameraCrash(bool bStateEnd = false)
		{
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x0006B749 File Offset: 0x00069949
		protected virtual void ProcessEventEffect(bool bStateEnd = false)
		{
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x0006B74C File Offset: 0x0006994C
		protected void ProcessEventDamageEffect(bool bStateEnd = false)
		{
			if (this.m_EffectStateNow == null)
			{
				return;
			}
			if (bStateEnd)
			{
				for (LinkedListNode<NKMDamageEffect> linkedListNode = this.m_linklistDamageEffect.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					NKMDamageEffect value = linkedListNode.Value;
					if (value.GetStateEndDie())
					{
						value.SetDie(false, true);
					}
				}
			}
			else
			{
				LinkedListNode<NKMDamageEffect> linkedListNode2 = this.m_linklistDamageEffect.First;
				while (linkedListNode2 != null)
				{
					NKMDamageEffect value2 = linkedListNode2.Value;
					if (!this.m_NKMGame.GetDEManager().IsLiveEffect(value2.GetDEUID()))
					{
						LinkedListNode<NKMDamageEffect> next = linkedListNode2.Next;
						this.m_linklistDamageEffect.Remove(linkedListNode2);
						linkedListNode2 = next;
					}
					else
					{
						value2.SetRight(this.m_DEData.m_bRight);
						value2.SetFollowPos(this.m_DEData.m_PosX, this.m_DEData.m_JumpYPos, this.m_DEData.m_PosZ, true);
						linkedListNode2 = linkedListNode2.Next;
					}
				}
			}
			for (int i = 0; i < this.m_EffectStateNow.m_listNKMEventDamageEffect.Count; i++)
			{
				NKMEventDamageEffect nkmeventDamageEffect = this.m_EffectStateNow.m_listNKMEventDamageEffect[i];
				if (nkmeventDamageEffect != null && this.CheckEventCondition(nkmeventDamageEffect.m_Condition))
				{
					bool flag = false;
					if (nkmeventDamageEffect.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (this.EventTimer(nkmeventDamageEffect.m_bAnimTime, nkmeventDamageEffect.m_fEventTime, true) && !nkmeventDamageEffect.m_bStateEndTime)
					{
						flag = true;
					}
					if (nkmeventDamageEffect.m_bIgnoreNoTarget && this.m_TargetUnit == null)
					{
						flag = false;
					}
					if (flag)
					{
						this.m_NKMVector3Temp1.x = this.m_DEData.m_PosX;
						this.m_NKMVector3Temp1.y = this.m_DEData.m_JumpYPos;
						this.m_NKMVector3Temp1.z = this.m_DEData.m_PosZ;
						if (nkmeventDamageEffect.m_bTargetPos)
						{
							if (this.m_TargetUnit != null)
							{
								this.m_NKMVector3Temp1.x = this.m_TargetUnit.GetUnitSyncData().m_PosX;
								this.m_NKMVector3Temp1.y = this.m_TargetUnit.GetUnitSyncData().m_JumpYPos;
							}
							else
							{
								this.m_NKMVector3Temp1.x = this.GetDEData().m_fLastTargetPosX;
								this.m_NKMVector3Temp1.y = this.GetDEData().m_fLastTargetPosJumpY;
							}
						}
						if (nkmeventDamageEffect.m_bUseMapPos)
						{
							if (this.m_DEData.m_bRight)
							{
								this.m_NKMVector3Temp1.x = this.m_NKMGame.GetMapTemplet().GetMapRatePos(nkmeventDamageEffect.m_fMapPosRate, true);
							}
							else
							{
								this.m_NKMVector3Temp1.x = this.m_NKMGame.GetMapTemplet().GetMapRatePos(nkmeventDamageEffect.m_fMapPosRate, false);
							}
						}
						float zscaleFactor = this.m_NKMGame.GetZScaleFactor(this.m_DEData.m_PosZ);
						short targetGameUID = this.m_DEData.m_TargetGameUnitUID;
						if (nkmeventDamageEffect.m_bIgnoreTarget)
						{
							targetGameUID = 0;
						}
						string templetID = nkmeventDamageEffect.m_DEName;
						if (this.m_NKMGame.GetGameData().IsPVP() && nkmeventDamageEffect.m_DENamePVP.Length > 1)
						{
							templetID = nkmeventDamageEffect.m_DENamePVP;
						}
						NKMDamageEffect nkmdamageEffect = this.m_NKMGame.GetDEManager().UseDamageEffect(templetID, this.m_DEData.m_MasterGameUnitUID, targetGameUID, this.m_UnitSkillTemplet, this.m_MasterUnitPhase, this.m_NKMVector3Temp1.x, this.m_NKMVector3Temp1.y, this.m_NKMVector3Temp1.z, this.m_DEData.m_bRight, nkmeventDamageEffect.m_OffsetX * zscaleFactor, nkmeventDamageEffect.m_OffsetY * zscaleFactor, nkmeventDamageEffect.m_OffsetZ, nkmeventDamageEffect.m_fAddRotate, nkmeventDamageEffect.m_bUseZScale, nkmeventDamageEffect.m_fSpeedFactorX, nkmeventDamageEffect.m_fSpeedFactorY, nkmeventDamageEffect.m_fReserveTime, true);
						if (nkmdamageEffect != null && (nkmeventDamageEffect.m_bHold || nkmeventDamageEffect.m_bStateEndStop))
						{
							nkmdamageEffect.SetHoldFollowData(nkmeventDamageEffect.m_FollowType, nkmeventDamageEffect.m_FollowTime, nkmeventDamageEffect.m_FollowUpdateTime);
							nkmdamageEffect.SetStateEndDie(nkmeventDamageEffect.m_bStateEndStop);
							this.m_linklistDamageEffect.AddLast(nkmdamageEffect);
						}
					}
				}
			}
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x0006BB21 File Offset: 0x00069D21
		protected virtual void ProcessEventDissolve(bool bStateEnd = false)
		{
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x0006BB24 File Offset: 0x00069D24
		protected virtual void ProcessEventBuff(bool bStateEnd = false)
		{
			if (this.m_EffectStateNow == null)
			{
				return;
			}
			for (int i = 0; i < this.m_EffectStateNow.m_listNKMEventBuff.Count; i++)
			{
				NKMEventBuff nkmeventBuff = this.m_EffectStateNow.m_listNKMEventBuff[i];
				if (nkmeventBuff != null && !nkmeventBuff.m_bReflection && this.CheckEventCondition(nkmeventBuff.m_Condition))
				{
					bool flag = false;
					if (nkmeventBuff.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (this.EventTimer(nkmeventBuff.m_bAnimTime, nkmeventBuff.m_fEventTime, true) && !nkmeventBuff.m_bStateEndTime)
					{
						flag = true;
					}
					if (flag)
					{
						nkmeventBuff.Process(this.m_NKMGame, this);
					}
				}
			}
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x0006BBC4 File Offset: 0x00069DC4
		protected virtual void ProcessEventStatus(bool bStateEnd = false)
		{
			if (this.m_EffectStateNow == null)
			{
				return;
			}
			for (int i = 0; i < this.m_EffectStateNow.m_listNKMEventStatus.Count; i++)
			{
				NKMEventStatus nkmeventStatus = this.m_EffectStateNow.m_listNKMEventStatus[i];
				if (nkmeventStatus != null)
				{
					nkmeventStatus.ProcessEvent(this.m_NKMGame, this, bStateEnd);
				}
			}
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x0006BC18 File Offset: 0x00069E18
		protected virtual void ProcessEventHeal(bool bStateEnd = false)
		{
			if (this.GetMasterUnit().Get_NKM_UNIT_CLASS_TYPE() != NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
			{
				return;
			}
			if (this.m_EffectStateNow == null)
			{
				return;
			}
			if (this.GetMasterUnit() == null)
			{
				return;
			}
			for (int i = 0; i < this.m_EffectStateNow.m_listNKMEventHeal.Count; i++)
			{
				NKMEventHeal nkmeventHeal = this.m_EffectStateNow.m_listNKMEventHeal[i];
				if (nkmeventHeal != null && this.CheckEventCondition(nkmeventHeal.m_Condition) && (nkmeventHeal.m_bStateEndTime ? bStateEnd : this.EventTimer(nkmeventHeal.m_bAnimTime, nkmeventHeal.m_fEventTime, true)))
				{
					this.GetMasterUnit().SetEventHeal(nkmeventHeal, this.GetDEData().m_PosX);
				}
			}
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x0006BCBC File Offset: 0x00069EBC
		public bool EventTimer(bool bAnim, float fTime, bool bOneTime)
		{
			if (bAnim)
			{
				return this.EventTimer(fTime, bOneTime, this.m_DEData.m_fAnimTimeBack, this.m_DEData.m_fAnimTime, this.m_EventTimeStampAnim);
			}
			return this.EventTimer(fTime, bOneTime, this.m_DEData.m_fStateTimeBack, this.m_DEData.m_fStateTime, this.m_EventTimeStampState);
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x0006BD18 File Offset: 0x00069F18
		private bool EventTimer(float fTimeTarget, bool bOneTime, float fTimeBack, float fTimeNow, Dictionary<float, NKMTimeStamp> dicTimeStamp)
		{
			if ((fTimeTarget > fTimeBack && fTimeTarget <= fTimeNow) || (fTimeTarget.IsNearlyZero(1E-05f) && this.m_DEData.m_bStateFirstFrame))
			{
				if (!bOneTime)
				{
					return true;
				}
				if (!dicTimeStamp.ContainsKey(fTimeTarget))
				{
					NKMTimeStamp nkmtimeStamp = (NKMTimeStamp)this.m_NKMGame.GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKMTimeStamp, "", "", false);
					nkmtimeStamp.m_FramePass = false;
					dicTimeStamp.Add(fTimeTarget, nkmtimeStamp);
					return true;
				}
				if (!dicTimeStamp[fTimeTarget].m_FramePass)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x0006BDA0 File Offset: 0x00069FA0
		public bool EventTimer(bool bAnim, float fTimeMin, float fTimeMax)
		{
			bool flag = false;
			if (bAnim)
			{
				if (this.m_DEData.m_fAnimTime >= fTimeMin && this.m_DEData.m_fAnimTime <= fTimeMax)
				{
					flag = true;
				}
			}
			else if (this.m_DEData.m_fStateTime >= fTimeMin && this.m_DEData.m_fStateTime <= fTimeMax)
			{
				flag = true;
			}
			if (!flag && this.EventTimer(bAnim, fTimeMin, true))
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x0006BE04 File Offset: 0x0006A004
		public NKMUnit GetTargetUnit()
		{
			if (this.m_DEData.m_TargetGameUnitUID > 0)
			{
				NKMUnit unit = this.m_NKMGame.GetUnit(this.m_DEData.m_TargetGameUnitUID, true, false);
				if (unit != null)
				{
					this.m_DEData.m_fLastTargetPosX = unit.GetUnitSyncData().m_PosX;
					this.m_DEData.m_fLastTargetPosZ = unit.GetUnitSyncData().m_PosZ;
					this.m_DEData.m_fLastTargetPosJumpY = unit.GetUnitSyncData().m_JumpYPos;
					return unit;
				}
			}
			return null;
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x0006BE80 File Offset: 0x0006A080
		public float GetDist(NKMUnit unit)
		{
			return Math.Abs(this.m_DEData.m_PosX - unit.GetUnitSyncData().m_PosX);
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x0006BE9E File Offset: 0x0006A09E
		public short GetDEUID()
		{
			return this.m_DamageEffectUID;
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x0006BEA6 File Offset: 0x0006A0A6
		public NKMDamageEffectData GetDEData()
		{
			return this.m_DEData;
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x0006BEAE File Offset: 0x0006A0AE
		public NKMUnit GetMasterUnit()
		{
			if (this.m_DEData.m_MasterUnit == null)
			{
				return this.m_NKMGame.GetUnit(this.m_DEData.m_MasterGameUnitUID, true, true);
			}
			return this.m_DEData.m_MasterUnit;
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x0006BEE1 File Offset: 0x0006A0E1
		public NKMDamageEffectTemplet GetTemplet()
		{
			return this.m_DETemplet;
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x0006BEEC File Offset: 0x0006A0EC
		public virtual void AttackResult(NKMDamageInst cNKMDamageInst, NKMUnit pDefender)
		{
			if (cNKMDamageInst.m_ReActResult == NKM_REACT_TYPE.NRT_NO)
			{
				return;
			}
			if (this.m_NKM_DAMAGE_EFFECT_CLASS_TYPE == NKM_DAMAGE_EFFECT_CLASS_TYPE.NDECT_NKM && this.m_DEData.m_MasterUnit != null)
			{
				if (cNKMDamageInst.m_ReActResult == NKM_REACT_TYPE.NRT_REVENGE)
				{
					if (!this.m_DEData.m_MasterUnit.GetUnitTemplet().m_bNoDamageState)
					{
						if (!this.m_DEData.m_MasterUnit.IsAirUnit())
						{
							if (this.m_DEData.m_MasterUnit.GetUnitFrameData().m_bFootOnLand)
							{
								this.m_DEData.m_MasterUnit.StateChange("USN_DAMAGE_A", true, false);
							}
							else if (!this.m_DEData.m_MasterUnit.GetUnitTemplet().m_bNoDamageDownState)
							{
								this.m_DEData.m_MasterUnit.StateChange("USN_DAMAGE_AIR_DOWN", true, false);
							}
							else
							{
								this.m_DEData.m_MasterUnit.StateChange("USN_DAMAGE_A", true, false);
							}
						}
						else
						{
							this.m_DEData.m_MasterUnit.StateChange("USN_DAMAGE_A", true, false);
						}
					}
					else
					{
						this.m_DEData.m_MasterUnit.StateChangeToASTAND(true, false);
					}
					this.m_DEData.m_MasterUnit.SetStopTime(0.5f, NKM_STOP_TIME_INDEX.NSTI_DAMAGE);
					this.m_DEData.m_MasterUnit.SetStopReserveTime(0.1f);
					this.SetDie(false, false);
				}
				else if (cNKMDamageInst.m_Templet.m_AttackerStateChange.Length > 1)
				{
					this.m_DEData.m_MasterUnit.StateChange(cNKMDamageInst.m_Templet.m_AttackerStateChange, true, false);
				}
			}
			this.m_DEData.m_DamageCountNow++;
			if (pDefender != null && pDefender.GetUnitTemplet().m_PenetrateDefence > 0)
			{
				this.m_DEData.m_DamageCountNow += pDefender.GetUnitTemplet().m_PenetrateDefence;
			}
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x0006C0A0 File Offset: 0x0006A2A0
		protected virtual void ProcessAttackHitEffect(NKMEventAttack cNKMEventAttack)
		{
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x0006C0A4 File Offset: 0x0006A2A4
		protected virtual void ProcessDieEventAttack()
		{
			for (int i = 0; i < this.m_DETemplet.m_listNKMDieEventAttack.Count; i++)
			{
				NKMEventAttack nkmeventAttack = this.m_DETemplet.m_listNKMDieEventAttack[i];
				if (nkmeventAttack != null && this.CheckEventCondition(nkmeventAttack.m_Condition))
				{
					NKMDamageInst damageInstAtk = this.GetDamageInstAtk(-1);
					damageInstAtk.Init();
					if (damageInstAtk.m_Templet == null)
					{
						damageInstAtk.m_Templet = NKMDamageManager.GetTempletByStrID(nkmeventAttack.m_DamageTempletName);
						damageInstAtk.m_AttackerType = NKM_REACTOR_TYPE.NRT_DAMAGE_EFFECT;
						damageInstAtk.m_AttackerEffectUID = this.m_DamageEffectUID;
						damageInstAtk.m_AttackerGameUnitUID = this.GetDEData().m_MasterGameUnitUID;
						damageInstAtk.m_AttackerUnitSkillTemplet = this.m_UnitSkillTemplet;
						damageInstAtk.m_AttackerTeamType = this.GetDEData().m_NKM_TEAM_TYPE;
					}
					this.m_NKMGame.DamageCheck(damageInstAtk, nkmeventAttack, true);
				}
			}
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x0006C16F File Offset: 0x0006A36F
		protected virtual void ProcessDieEventEffect()
		{
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x0006C174 File Offset: 0x0006A374
		protected virtual void ProcessDieEventDamageEffect()
		{
			for (int i = 0; i < this.GetTemplet().m_listNKMDieEventDamageEffect.Count; i++)
			{
				NKMEventDamageEffect nkmeventDamageEffect = this.GetTemplet().m_listNKMDieEventDamageEffect[i];
				if (nkmeventDamageEffect != null && this.CheckEventCondition(nkmeventDamageEffect.m_Condition))
				{
					bool flag = true;
					if (nkmeventDamageEffect.m_bIgnoreNoTarget && this.m_TargetUnit == null)
					{
						flag = false;
					}
					if (flag)
					{
						this.m_NKMVector3Temp1.x = this.m_DEData.m_PosX;
						this.m_NKMVector3Temp1.y = this.m_DEData.m_JumpYPos;
						this.m_NKMVector3Temp1.z = this.m_DEData.m_PosZ;
						if (nkmeventDamageEffect.m_bTargetPos)
						{
							if (this.m_TargetUnit != null)
							{
								this.m_NKMVector3Temp1.x = this.m_TargetUnit.GetUnitSyncData().m_PosX;
								this.m_NKMVector3Temp1.y = this.m_TargetUnit.GetUnitSyncData().m_JumpYPos;
							}
							else
							{
								this.m_NKMVector3Temp1.x = this.GetDEData().m_fLastTargetPosX;
								this.m_NKMVector3Temp1.y = this.GetDEData().m_fLastTargetPosJumpY;
							}
						}
						if (nkmeventDamageEffect.m_bUseMapPos)
						{
							if (this.m_DEData.m_bRight)
							{
								this.m_NKMVector3Temp1.x = this.m_NKMGame.GetMapTemplet().GetMapRatePos(nkmeventDamageEffect.m_fMapPosRate, true);
							}
							else
							{
								this.m_NKMVector3Temp1.x = this.m_NKMGame.GetMapTemplet().GetMapRatePos(nkmeventDamageEffect.m_fMapPosRate, false);
							}
						}
						float zscaleFactor = this.m_NKMGame.GetZScaleFactor(this.m_DEData.m_PosZ);
						short targetGameUID = this.m_DEData.m_TargetGameUnitUID;
						if (nkmeventDamageEffect.m_bIgnoreTarget)
						{
							targetGameUID = 0;
						}
						string templetID = nkmeventDamageEffect.m_DEName;
						if (this.m_NKMGame.GetGameData().IsPVP() && nkmeventDamageEffect.m_DENamePVP.Length > 1)
						{
							templetID = nkmeventDamageEffect.m_DENamePVP;
						}
						this.m_NKMGame.GetDEManager().UseDamageEffect(templetID, this.m_DEData.m_MasterGameUnitUID, targetGameUID, this.m_UnitSkillTemplet, this.m_MasterUnitPhase, this.m_NKMVector3Temp1.x, this.m_NKMVector3Temp1.y, this.m_NKMVector3Temp1.z, this.m_DEData.m_bRight, nkmeventDamageEffect.m_OffsetX * zscaleFactor, nkmeventDamageEffect.m_OffsetY * zscaleFactor, nkmeventDamageEffect.m_OffsetZ, nkmeventDamageEffect.m_fAddRotate, nkmeventDamageEffect.m_bUseZScale, nkmeventDamageEffect.m_fSpeedFactorX, nkmeventDamageEffect.m_fSpeedFactorY, nkmeventDamageEffect.m_fReserveTime, true);
					}
				}
			}
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x0006C3E2 File Offset: 0x0006A5E2
		protected virtual void ProcessDieEventSound()
		{
		}

		// Token: 0x040011F0 RID: 4592
		protected NKM_DAMAGE_EFFECT_CLASS_TYPE m_NKM_DAMAGE_EFFECT_CLASS_TYPE;

		// Token: 0x040011F1 RID: 4593
		protected NKMGame m_NKMGame;

		// Token: 0x040011F2 RID: 4594
		protected NKMDamageEffectManager m_DEManager;

		// Token: 0x040011F3 RID: 4595
		protected short m_DamageEffectUID;

		// Token: 0x040011F4 RID: 4596
		protected NKMDamageEffectTemplet m_DETemplet;

		// Token: 0x040011F5 RID: 4597
		protected NKMDamageEffectData m_DEData = new NKMDamageEffectData();

		// Token: 0x040011F6 RID: 4598
		protected float m_fDeltaTime;

		// Token: 0x040011F7 RID: 4599
		protected NKMDamageEffectState m_EffectStateNow;

		// Token: 0x040011F8 RID: 4600
		protected string m_StateNameNow;

		// Token: 0x040011F9 RID: 4601
		protected string m_StateNameNext;

		// Token: 0x040011FA RID: 4602
		protected NKMTrackingFloat m_EventMovePosX = new NKMTrackingFloat();

		// Token: 0x040011FB RID: 4603
		protected NKMTrackingFloat m_EventMovePosZ = new NKMTrackingFloat();

		// Token: 0x040011FC RID: 4604
		protected NKMTrackingFloat m_EventMovePosJumpY = new NKMTrackingFloat();

		// Token: 0x040011FD RID: 4605
		protected float m_fEventMoveSavedPositionX;

		// Token: 0x040011FE RID: 4606
		protected Dictionary<int, NKMDamageInst> m_dictDamageInstAtk = new Dictionary<int, NKMDamageInst>();

		// Token: 0x040011FF RID: 4607
		protected NKMUnitSkillTemplet m_UnitSkillTemplet;

		// Token: 0x04001200 RID: 4608
		protected int m_MasterUnitPhase;

		// Token: 0x04001201 RID: 4609
		protected LinkedList<NKMDamageEffect> m_linklistDamageEffect = new LinkedList<NKMDamageEffect>();

		// Token: 0x04001202 RID: 4610
		protected Dictionary<float, NKMTimeStamp> m_EventTimeStampAnim = new Dictionary<float, NKMTimeStamp>();

		// Token: 0x04001203 RID: 4611
		protected Dictionary<float, NKMTimeStamp> m_EventTimeStampState = new Dictionary<float, NKMTimeStamp>();

		// Token: 0x04001204 RID: 4612
		protected NKMVector3 m_NKMVector3Temp1;

		// Token: 0x04001205 RID: 4613
		protected NKMVector3 m_NKMVector3Temp2;

		// Token: 0x04001206 RID: 4614
		protected NKMVector3 m_NKMVector3Temp3;

		// Token: 0x04001207 RID: 4615
		protected bool m_bSortUnitDirty = true;

		// Token: 0x04001208 RID: 4616
		protected float m_TempSortDist;

		// Token: 0x04001209 RID: 4617
		protected List<NKMUnit> m_listSortUnit = new List<NKMUnit>();

		// Token: 0x0400120A RID: 4618
		protected NKMUnit m_TargetUnit;
	}
}
