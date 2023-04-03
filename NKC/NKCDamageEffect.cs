using System;
using System.Collections.Generic;
using NKC.FX;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000666 RID: 1638
	public class NKCDamageEffect : NKMDamageEffect
	{
		// Token: 0x06003376 RID: 13174 RVA: 0x0010254C File Offset: 0x0010074C
		public NKCDamageEffect()
		{
			this.m_NKM_OBJECT_POOL_TYPE = NKM_OBJECT_POOL_TYPE.NOPT_NKCDamageEffect;
			this.m_NKM_DAMAGE_EFFECT_CLASS_TYPE = NKM_DAMAGE_EFFECT_CLASS_TYPE.NDECT_NKC;
			this.InitShadow();
		}

		// Token: 0x06003377 RID: 13175 RVA: 0x001025C8 File Offset: 0x001007C8
		private void InitShadow()
		{
			this.m_NKCASUnitShadow = (NKCASUnitShadow)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASUnitShadow, "", "", false);
			if (this.m_NKCASUnitShadow == null || this.m_NKCASUnitShadow.m_ShadowSpriteInstant == null || this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant == null)
			{
				return;
			}
			this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.transform.SetParent(NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_GAME_BATTLE_UNIT_SHADOW().transform, false);
			NKCUtil.SetGameObjectLocalPos(this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant, 0f, 0f, 0f);
			if (this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.activeSelf)
			{
				this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.SetActive(false);
			}
		}

		// Token: 0x06003378 RID: 13176 RVA: 0x001026AF File Offset: 0x001008AF
		public override void Open()
		{
			base.Open();
			this.m_DissolveFactor.SetNowValue(0f);
		}

		// Token: 0x06003379 RID: 13177 RVA: 0x001026C8 File Offset: 0x001008C8
		public override void Close()
		{
			base.Close();
			if (this.m_NKCASEffect != null)
			{
				this.m_NKCGameClient.GetNKCEffectManager().DeleteEffect(this.m_NKCASEffect.m_EffectUID);
			}
			if (this.m_NKCASUnitShadow != null && this.m_NKCASUnitShadow.m_ShadowSpriteInstant != null && this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant != null && this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.activeSelf)
			{
				this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.SetActive(false);
			}
			for (int i = 0; i < this.m_listEffect.Count; i++)
			{
				NKCASEffect nkcaseffect = this.m_listEffect[i];
				nkcaseffect.Stop(false);
				nkcaseffect.m_bAutoDie = true;
			}
			this.m_listEffect.Clear();
		}

		// Token: 0x0600337A RID: 13178 RVA: 0x00102792 File Offset: 0x00100992
		public override void Unload()
		{
			this.StopSound();
			this.m_listEffect.Clear();
			this.m_NKCASEffect = null;
			this.m_NKCASUnitShadow.Unload();
			this.m_NKCASUnitShadow = null;
			this.m_NKCGameClient = null;
			base.Unload();
		}

		// Token: 0x0600337B RID: 13179 RVA: 0x001027CC File Offset: 0x001009CC
		public override bool SetDamageEffect(NKMGame cNKMGame, NKMDamageEffectManager cDEManager, NKMUnitSkillTemplet cUnitSkillTemplet, int masterUnitPhase, short deUID, string deTempletID, short masterUID, short targetUID, float fX, float fY, float fZ, bool bRight, float offsetX = 0f, float offsetY = 0f, float offsetZ = 0f, float fAddRotate = 0f, bool bUseZScale = true, float fSpeedFactorX = 0f, float fSpeedFactorY = 0f)
		{
			this.m_NKCGameClient = (NKCGameClient)cNKMGame;
			if (!base.SetDamageEffect(cNKMGame, cDEManager, cUnitSkillTemplet, masterUnitPhase, deUID, deTempletID, masterUID, targetUID, fX, fY, fZ, bRight, offsetX, offsetY, offsetZ, fAddRotate, bUseZScale, fSpeedFactorX, fSpeedFactorY))
			{
				return false;
			}
			NKMUnit unit = cNKMGame.GetUnit(masterUID, true, false);
			string mainEffectName;
			if (unit != null && unit.GetUnitData() != null)
			{
				mainEffectName = this.m_DETemplet.GetMainEffectName(unit.GetUnitData().m_SkinID);
			}
			else
			{
				mainEffectName = this.m_DETemplet.m_MainEffectName;
			}
			float num = 1f;
			if (this.m_DETemplet.m_bUseTargetBuffScaleFactor)
			{
				NKMUnit unit2 = cNKMGame.GetUnit(targetUID, true, false);
				if (unit2 != null)
				{
					num = unit2.GetUnitTemplet().m_fBuffEffectScaleFactor;
				}
			}
			this.m_NKCASEffect = this.m_NKCGameClient.GetNKCEffectManager().UseEffect(masterUID, mainEffectName, mainEffectName, NKM_EFFECT_PARENT_TYPE.NEPT_NUM_GAME_BATTLE_EFFECT, this.m_DEData.m_PosX, this.m_DEData.m_PosZ + this.m_DEData.m_JumpYPos, this.m_DEData.m_PosZ, bRight, this.m_DETemplet.m_fScaleFactor * num, 0f, 0f, 0f, false, this.m_DEData.m_fAddRotate, this.m_DEData.m_bUseZScale, "", false, false, "", 1f, false, false, 0f, -1f, true);
			if (this.m_NKCASEffect != null && this.m_DETemplet != null)
			{
				this.m_NKCASEffect.SetCanIgnoreStopTime(this.m_DETemplet.m_CanIgnoreStopTime);
				this.m_NKCASEffect.SetUseMasterAnimSpeed(this.m_DETemplet.m_UseMasterAnimSpeed);
			}
			this.SetShadow();
			this.SetSpecialProperty();
			return true;
		}

		// Token: 0x0600337C RID: 13180 RVA: 0x00102960 File Offset: 0x00100B60
		private void SetShadow()
		{
			if (this.m_NKCASUnitShadow == null || this.m_NKCASUnitShadow.m_ShadowSpriteInstant == null || this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant == null)
			{
				return;
			}
			if (!this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.activeSelf)
			{
				this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.SetActive(true);
			}
			this.m_Vector3Temp.Set(this.m_DETemplet.m_fShadowScaleX, this.m_DETemplet.m_fShadowScaleY, 1f);
			this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.transform.localScale = this.m_Vector3Temp;
			this.m_Vector3Temp.Set(this.m_DEData.m_PosX, this.m_DEData.m_PosZ, this.m_DEData.m_PosZ);
			this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.transform.localPosition = this.m_Vector3Temp;
			bool bTeamA = true;
			bool bRearm = false;
			if (this.m_DEData.m_MasterUnit != null)
			{
				bTeamA = !this.m_NKCGameClient.IsEnemy(this.m_NKCGameClient.m_MyTeam, this.m_DEData.m_MasterUnit.GetUnitDataGame().m_NKM_TEAM_TYPE_ORG);
				bRearm = this.m_DEData.m_MasterUnit.GetUnitTemplet().m_UnitTempletBase.IsRearmUnit;
			}
			this.m_NKCASUnitShadow.SetShadowType(this.m_DETemplet.m_NKC_TEAM_COLOR_TYPE, bTeamA, bRearm);
		}

		// Token: 0x0600337D RID: 13181 RVA: 0x00102AD4 File Offset: 0x00100CD4
		private void SetSpecialProperty()
		{
			IFxProperty[] componentsInChildren = this.m_NKCASEffect.m_EffectInstant.m_Instant.GetComponentsInChildren<IFxProperty>(true);
			if (componentsInChildren != null)
			{
				NKCUnitClient masterUnit = base.GetMasterUnit() as NKCUnitClient;
				NKCUnitClient targetUnit = base.GetTargetUnit() as NKCUnitClient;
				IFxProperty[] array = componentsInChildren;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].SetFxProperty(masterUnit, targetUnit, this.m_DEData);
				}
			}
		}

		// Token: 0x0600337E RID: 13182 RVA: 0x00102B38 File Offset: 0x00100D38
		protected void OnChangeTarget()
		{
			this.SetSpecialProperty();
		}

		// Token: 0x0600337F RID: 13183 RVA: 0x00102B40 File Offset: 0x00100D40
		protected override void StateStart()
		{
			if (this.m_EffectStateNow == null)
			{
				return;
			}
			this.StopSound();
			base.StateStart();
			if (this.m_EffectStateNow.m_AnimName.Length > 1)
			{
				NKCASEffect nkcaseffect = this.m_NKCASEffect;
				if (nkcaseffect == null)
				{
					return;
				}
				nkcaseffect.PlayAnim(this.m_EffectStateNow.m_AnimName, this.m_EffectStateNow.m_bAnimLoop, this.m_EffectStateNow.m_fAnimSpeed);
			}
		}

		// Token: 0x06003380 RID: 13184 RVA: 0x00102BA8 File Offset: 0x00100DA8
		protected void StopSound()
		{
			for (int i = 0; i < this.m_listSoundUID.Count; i++)
			{
				NKCSoundManager.StopSound(this.m_listSoundUID[i]);
			}
			this.m_listSoundUID.Clear();
		}

		// Token: 0x06003381 RID: 13185 RVA: 0x00102BE7 File Offset: 0x00100DE7
		protected override void StateUpdate()
		{
			if (this.m_EffectStateNow == null)
			{
				return;
			}
			base.StateUpdate();
			this.UpdateEffect();
		}

		// Token: 0x06003382 RID: 13186 RVA: 0x00102C00 File Offset: 0x00100E00
		protected float GetLookDir()
		{
			this.m_NKMVector3Temp1.x = this.m_DEData.m_DirVector.x;
			this.m_NKMVector3Temp1.y = this.m_DEData.m_DirVector.y;
			this.m_NKMVector3Temp1.z = this.m_DEData.m_DirVector.z;
			this.m_NKMVector3Temp1.Normalize();
			if (Math.Abs(this.m_NKMVector3Temp1.y) <= Math.Abs(this.m_NKMVector3Temp1.z))
			{
				this.m_NKMVector3Temp1.y = this.m_NKMVector3Temp1.z;
			}
			this.m_NKMVector3Temp1.z = 0f;
			this.m_NKMVector3Temp1.Normalize();
			return (float)Math.Atan2((double)this.m_NKMVector3Temp1.y, (double)this.m_NKMVector3Temp1.x) * 180f / 3.14159f;
		}

		// Token: 0x06003383 RID: 13187 RVA: 0x00102CE8 File Offset: 0x00100EE8
		private void UpdateEffect()
		{
			NKCASEffect nkcaseffect = this.m_NKCASEffect;
			if (nkcaseffect != null)
			{
				nkcaseffect.SetPos(this.m_DEData.m_PosX, this.m_DEData.m_PosZ + this.m_DEData.m_JumpYPos, this.m_DEData.m_PosZ);
			}
			if (this.m_DETemplet.m_bLookDir)
			{
				NKCASEffect nkcaseffect2 = this.m_NKCASEffect;
				if (nkcaseffect2 != null)
				{
					nkcaseffect2.SetLookDir(this.GetLookDir());
				}
			}
			else
			{
				NKCASEffect nkcaseffect3 = this.m_NKCASEffect;
				if (nkcaseffect3 != null)
				{
					nkcaseffect3.SetRight(this.m_DEData.m_bRight);
				}
			}
			this.UpdateShadow();
		}

		// Token: 0x06003384 RID: 13188 RVA: 0x00102D7C File Offset: 0x00100F7C
		private void UpdateShadow()
		{
			if (this.m_NKCASUnitShadow == null || this.m_NKCASUnitShadow.m_ShadowSpriteInstant == null || this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant == null)
			{
				return;
			}
			if (this.m_DEData.m_bRight)
			{
				this.m_Vector3Temp.Set(this.m_DETemplet.m_fShadowRotateX, 180f, this.m_DETemplet.m_fShadowRotateZ);
				this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.transform.localEulerAngles = this.m_Vector3Temp;
			}
			else
			{
				this.m_Vector3Temp.Set(this.m_DETemplet.m_fShadowRotateX, 0f, this.m_DETemplet.m_fShadowRotateZ);
				this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.transform.localEulerAngles = this.m_Vector3Temp;
			}
			this.m_Vector3Temp.Set(this.m_DEData.m_PosX, this.m_DEData.m_PosZ, this.m_DEData.m_PosZ);
			this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant.transform.localPosition = this.m_Vector3Temp;
			float num = 1f - 0.2f * this.m_DEData.m_JumpYPos * 0.01f;
			if (num < 0.3f)
			{
				num = 0.3f;
			}
			NKCUtil.SetGameObjectLocalScale(this.m_NKCASUnitShadow.m_ShadowSpriteInstant.m_Instant, this.m_DETemplet.m_fShadowScaleX * num, this.m_DETemplet.m_fShadowScaleY * num, 1f);
		}

		// Token: 0x06003385 RID: 13189 RVA: 0x00102EFE File Offset: 0x001010FE
		public override void SetDie(bool bForce = false, bool bDieEvent = true)
		{
			base.SetDie(bForce, bDieEvent);
		}

		// Token: 0x06003386 RID: 13190 RVA: 0x00102F08 File Offset: 0x00101108
		protected override void ProcessEventAttack()
		{
			if (this.m_EffectStateNow == null)
			{
				return;
			}
			if (this.m_DEData.m_DamageCountNow >= this.m_DETemplet.m_DamageCountMax)
			{
				return;
			}
			base.ProcessEventAttack();
		}

		// Token: 0x06003387 RID: 13191 RVA: 0x00102F34 File Offset: 0x00101134
		protected override void ProcessAttackHitEffect(NKMEventAttack cNKMEventAttack)
		{
			if (cNKMEventAttack == null)
			{
				return;
			}
			this.m_Vector3Temp.Set(this.m_DEData.m_PosX, this.m_DEData.m_PosZ + this.m_DEData.m_JumpYPos, this.m_DEData.m_PosZ);
			NKCASEffect nkcaseffect = this.m_NKCGameClient.GetNKCEffectManager().UseEffect(this.m_DEData.m_MasterGameUnitUID, cNKMEventAttack.m_EffectName, cNKMEventAttack.m_EffectName, NKM_EFFECT_PARENT_TYPE.NEPT_NUM_GAME_BATTLE_EFFECT, this.m_Vector3Temp.x, this.m_Vector3Temp.y, this.m_Vector3Temp.z, this.m_DEData.m_bRight, 1f, 0f, 0f, 0f, false, 0f, true, "", false, true, "", 1f, false, false, 0f, -1f, true);
			if (nkcaseffect != null && this.m_DETemplet != null)
			{
				nkcaseffect.SetCanIgnoreStopTime(this.m_DETemplet.m_CanIgnoreStopTime);
				nkcaseffect.SetUseMasterAnimSpeed(this.m_DETemplet.m_UseMasterAnimSpeed);
			}
		}

		// Token: 0x06003388 RID: 13192 RVA: 0x00103038 File Offset: 0x00101238
		protected override void ProcessEventEffect(bool bStateEnd = false)
		{
			if (this.m_EffectStateNow == null)
			{
				return;
			}
			if (bStateEnd)
			{
				for (int i = 0; i < this.m_listEffect.Count; i++)
				{
					NKCASEffect nkcaseffect = this.m_listEffect[i];
					if (nkcaseffect.m_bStateEndStop && this.m_NKCGameClient.GetNKCEffectManager().IsLiveEffect(nkcaseffect.m_EffectUID))
					{
						nkcaseffect.Stop(nkcaseffect.m_bStateEndStopForce);
					}
				}
			}
			else
			{
				for (int j = 0; j < this.m_listEffect.Count; j++)
				{
					NKCASEffect nkcaseffect2 = this.m_listEffect[j];
					if (!this.m_NKCGameClient.GetNKCEffectManager().IsLiveEffect(nkcaseffect2.m_EffectUID))
					{
						this.m_listEffect.RemoveAt(j);
						j--;
					}
					else if (this.m_NKCASEffect != null && nkcaseffect2.m_BoneName.Length > 1)
					{
						Transform transform = this.m_NKCASEffect.m_EffectInstant.GetTransform(nkcaseffect2.m_BoneName);
						nkcaseffect2.SetPos(transform.position.x, transform.position.y, transform.position.z);
						if (nkcaseffect2.m_bUseBoneRotate)
						{
							nkcaseffect2.m_EffectInstant.m_Instant.transform.eulerAngles = transform.eulerAngles;
						}
					}
					else
					{
						nkcaseffect2.SetRight(this.m_DEData.m_bRight);
						nkcaseffect2.SetPos(this.m_DEData.m_PosX, this.m_DEData.m_PosZ + this.m_DEData.m_JumpYPos, this.m_DEData.m_PosZ);
					}
				}
			}
			for (int k = 0; k < this.m_EffectStateNow.m_listNKMEventEffect.Count; k++)
			{
				NKMEventEffect nkmeventEffect = this.m_EffectStateNow.m_listNKMEventEffect[k];
				if (nkmeventEffect != null && base.CheckEventCondition(nkmeventEffect.m_Condition) && nkmeventEffect.IsRightSkin(this.m_DEData.m_MasterUnit.GetUnitData().m_SkinID))
				{
					bool flag = false;
					if (nkmeventEffect.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (base.EventTimer(nkmeventEffect.m_bAnimTime, nkmeventEffect.m_fEventTime, true) && !nkmeventEffect.m_bStateEndTime)
					{
						flag = true;
					}
					if (flag)
					{
						this.m_Vector3Temp2.Set(nkmeventEffect.m_OffsetX, nkmeventEffect.m_OffsetY, nkmeventEffect.m_OffsetZ);
						if (nkmeventEffect.m_bFixedPos)
						{
							this.m_Vector3Temp.Set(nkmeventEffect.m_OffsetX, nkmeventEffect.m_OffsetY, nkmeventEffect.m_OffsetZ);
							this.m_Vector3Temp2.Set(0f, 0f, 0f);
						}
						else if (this.m_NKCASEffect != null && nkmeventEffect.m_BoneName.Length > 1)
						{
							Transform transform2 = this.m_NKCASEffect.m_EffectInstant.GetTransform(nkmeventEffect.m_BoneName);
							this.m_Vector3Temp.Set(transform2.position.x, transform2.position.y, transform2.position.z);
						}
						else
						{
							this.m_Vector3Temp.Set(this.m_DEData.m_PosX, this.m_DEData.m_PosZ + this.m_DEData.m_JumpYPos, this.m_DEData.m_PosZ);
							if (nkmeventEffect.m_bLandConnect)
							{
								this.m_Vector3Temp.y = this.m_DEData.m_PosZ;
							}
						}
						bool bRight = this.m_DEData.m_bRight;
						if (nkmeventEffect.m_bForceRight)
						{
							bRight = true;
						}
						string effectName = nkmeventEffect.GetEffectName(this.m_DEData);
						NKCASEffect nkcaseffect3 = this.m_NKCGameClient.GetNKCEffectManager().UseEffect(this.m_DEData.m_MasterGameUnitUID, effectName, effectName, nkmeventEffect.m_ParentType, this.m_Vector3Temp.x, this.m_Vector3Temp.y, this.m_Vector3Temp.z, bRight, nkmeventEffect.m_fScaleFactor, this.m_Vector3Temp2.x, this.m_Vector3Temp2.y, this.m_Vector3Temp2.z, nkmeventEffect.m_bUseOffsetZtoY, nkmeventEffect.m_fAddRotate, nkmeventEffect.m_bUseZScale, nkmeventEffect.m_BoneName, nkmeventEffect.m_bUseBoneRotate, true, nkmeventEffect.m_AnimName, 1f, false, nkmeventEffect.m_bCutIn, nkmeventEffect.m_fReserveTime, -1f, true);
						if (nkcaseffect3 != null && nkmeventEffect != null)
						{
							if (nkmeventEffect.m_bHold || nkmeventEffect.m_bStateEndStop)
							{
								nkcaseffect3.m_bStateEndStop = nkmeventEffect.m_bStateEndStop;
								nkcaseffect3.m_bStateEndStopForce = nkmeventEffect.m_bStateEndStopForce;
								this.m_listEffect.Add(nkcaseffect3);
							}
							if (this.m_DETemplet != null)
							{
								nkcaseffect3.SetCanIgnoreStopTime(this.m_DETemplet.m_CanIgnoreStopTime);
								nkcaseffect3.SetUseMasterAnimSpeed(this.m_DETemplet.m_UseMasterAnimSpeed);
							}
							nkcaseffect3.SetApplyStopTime(nkmeventEffect.m_ApplyStopTime);
						}
					}
				}
			}
		}

		// Token: 0x06003389 RID: 13193 RVA: 0x001034EC File Offset: 0x001016EC
		protected override void ProcessEventSound(bool bStateEnd = false)
		{
			if (this.m_EffectStateNow == null)
			{
				return;
			}
			for (int i = 0; i < this.m_EffectStateNow.m_listNKMEventSound.Count; i++)
			{
				NKMEventSound nkmeventSound = this.m_EffectStateNow.m_listNKMEventSound[i];
				if (nkmeventSound != null && base.CheckEventCondition(nkmeventSound.m_Condition) && (this.m_DEData == null || nkmeventSound.IsRightSkin(this.m_DEData.GetMasterSkinID())))
				{
					bool bOneTime = true;
					if (this.m_EffectStateNow.m_bAnimLoop)
					{
						bOneTime = false;
					}
					bool flag = false;
					if (nkmeventSound.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (base.EventTimer(nkmeventSound.m_bAnimTime, nkmeventSound.m_fEventTime, bOneTime) && !nkmeventSound.m_bStateEndTime)
					{
						flag = true;
					}
					string audioClipName;
					if (flag && NKMRandom.Range(0f, 1f) <= nkmeventSound.m_PlayRate && nkmeventSound.GetRandomSound(this.m_DEData, out audioClipName))
					{
						int item = NKCSoundManager.PlaySound(audioClipName, nkmeventSound.m_fLocalVol, this.m_DEData.m_PosX, nkmeventSound.m_fFocusRange, nkmeventSound.m_bLoop, 0f, false, 0f);
						if (nkmeventSound.m_bStopSound)
						{
							this.m_listSoundUID.Add(item);
						}
					}
				}
			}
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x0010361C File Offset: 0x0010181C
		protected override void ProcessEventCameraCrash(bool bStateEnd = false)
		{
			if (this.m_EffectStateNow == null)
			{
				return;
			}
			for (int i = 0; i < this.m_EffectStateNow.m_listNKMEventCameraCrash.Count; i++)
			{
				NKMEventCameraCrash nkmeventCameraCrash = this.m_EffectStateNow.m_listNKMEventCameraCrash[i];
				if (nkmeventCameraCrash != null && base.CheckEventCondition(nkmeventCameraCrash.m_Condition))
				{
					bool flag = false;
					if (nkmeventCameraCrash.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (base.EventTimer(nkmeventCameraCrash.m_bAnimTime, nkmeventCameraCrash.m_fEventTime, true) && !nkmeventCameraCrash.m_bStateEndTime)
					{
						flag = true;
					}
					if (flag && (nkmeventCameraCrash.m_fCrashRadius <= 0f || NKCCamera.GetDist(this) <= nkmeventCameraCrash.m_fCrashRadius))
					{
						switch (nkmeventCameraCrash.m_CameraCrashType)
						{
						case NKM_CAMERA_CRASH_TYPE.NCCT_UP:
							NKCCamera.UpCrashCamera(nkmeventCameraCrash.m_fCameraCrashSpeed, nkmeventCameraCrash.m_fCameraCrashAccel);
							break;
						case NKM_CAMERA_CRASH_TYPE.NCCT_DOWN:
							NKCCamera.DownCrashCamera(nkmeventCameraCrash.m_fCameraCrashSpeed, nkmeventCameraCrash.m_fCameraCrashAccel);
							break;
						case NKM_CAMERA_CRASH_TYPE.NCCT_UP_DOWN:
							NKCCamera.UpDownCrashCamera(nkmeventCameraCrash.m_fCameraCrashGap, nkmeventCameraCrash.m_fCameraCrashTime, 0.05f);
							break;
						case NKM_CAMERA_CRASH_TYPE.NCCT_UP_DOWN_NO_RESET:
							NKCCamera.UpDownCrashCameraNoReset(nkmeventCameraCrash.m_fCameraCrashGap, nkmeventCameraCrash.m_fCameraCrashTime);
							break;
						}
					}
				}
			}
		}

		// Token: 0x0600338B RID: 13195 RVA: 0x0010373C File Offset: 0x0010193C
		protected override void ProcessEventDissolve(bool bStateEnd = false)
		{
			if (this.m_EffectStateNow == null)
			{
				return;
			}
			this.m_DissolveFactor.Update(this.m_fDeltaTime);
			if (this.m_NKCASEffect != null && this.m_NKCASEffect.m_bSpine)
			{
				if (this.m_DissolveFactor.IsTracking())
				{
					this.m_NKCASEffect.SetDissolveBlend(this.m_DissolveFactor.GetNowValue());
				}
				else if (this.m_DissolveFactor.GetNowValue() <= 0f && this.m_bDissolveEnable)
				{
					this.m_bDissolveEnable = false;
					this.m_NKCASEffect.SetDissolveBlend(0f);
					this.m_NKCASEffect.SetDissolveOn(this.m_bDissolveEnable);
				}
				for (int i = 0; i < this.m_EffectStateNow.m_listNKMEventDissolve.Count; i++)
				{
					NKMEventDissolve nkmeventDissolve = this.m_EffectStateNow.m_listNKMEventDissolve[i];
					if (nkmeventDissolve != null && base.CheckEventCondition(nkmeventDissolve.m_Condition))
					{
						bool flag = false;
						if (nkmeventDissolve.m_bStateEndTime && bStateEnd)
						{
							flag = true;
						}
						else if (base.EventTimer(nkmeventDissolve.m_bAnimTime, nkmeventDissolve.m_fEventTime, true) && !nkmeventDissolve.m_bStateEndTime)
						{
							flag = true;
						}
						if (flag)
						{
							if (!this.m_bDissolveEnable)
							{
								this.m_bDissolveEnable = true;
								this.m_NKCASEffect.SetDissolveOn(this.m_bDissolveEnable);
								this.m_ColorTemp.r = nkmeventDissolve.m_fColorR;
								this.m_ColorTemp.g = nkmeventDissolve.m_fColorG;
								this.m_ColorTemp.b = nkmeventDissolve.m_fColorB;
								this.m_ColorTemp.a = 1f;
								this.m_NKCASEffect.SetDissolveColor(this.m_ColorTemp);
							}
							this.m_DissolveFactor.SetTracking(nkmeventDissolve.m_fDissolve, nkmeventDissolve.m_fTrackTime, TRACKING_DATA_TYPE.TDT_NORMAL);
						}
					}
				}
			}
		}

		// Token: 0x0600338C RID: 13196 RVA: 0x001038F4 File Offset: 0x00101AF4
		protected override void ProcessDieEventEffect()
		{
			for (int i = 0; i < this.m_DETemplet.m_listNKMDieEventEffect.Count; i++)
			{
				NKMEventEffect nkmeventEffect = this.m_DETemplet.m_listNKMDieEventEffect[i];
				if (nkmeventEffect != null)
				{
					if (this.m_NKCASEffect != null && nkmeventEffect.m_BoneName.Length > 1)
					{
						Transform transform = this.m_NKCASEffect.m_EffectInstant.GetTransform(nkmeventEffect.m_BoneName);
						this.m_Vector3Temp.Set(transform.position.x, transform.position.y, transform.position.z);
					}
					else
					{
						this.m_Vector3Temp.Set(this.m_DEData.m_PosX, this.m_DEData.m_PosZ + this.m_DEData.m_JumpYPos, this.m_DEData.m_PosZ);
						if (nkmeventEffect.m_bLandConnect)
						{
							this.m_Vector3Temp.y = this.m_DEData.m_PosZ;
						}
					}
					string effectName = nkmeventEffect.GetEffectName(this.m_DEData);
					NKCASEffect nkcaseffect = this.m_NKCGameClient.GetNKCEffectManager().UseEffect(this.m_DEData.m_MasterGameUnitUID, effectName, effectName, nkmeventEffect.m_ParentType, this.m_Vector3Temp.x, this.m_Vector3Temp.y, this.m_Vector3Temp.z, this.m_DEData.m_bRight, nkmeventEffect.m_fScaleFactor, nkmeventEffect.m_OffsetX, nkmeventEffect.m_OffsetY, nkmeventEffect.m_OffsetZ, nkmeventEffect.m_bUseOffsetZtoY, nkmeventEffect.m_fAddRotate, nkmeventEffect.m_bUseZScale, nkmeventEffect.m_BoneName, nkmeventEffect.m_bUseBoneRotate, true, nkmeventEffect.m_AnimName, 1f, false, nkmeventEffect.m_bCutIn, nkmeventEffect.m_fReserveTime, -1f, true);
					if (nkcaseffect != null && this.m_DETemplet != null)
					{
						nkcaseffect.SetCanIgnoreStopTime(this.m_DETemplet.m_CanIgnoreStopTime);
						nkcaseffect.SetUseMasterAnimSpeed(this.m_DETemplet.m_UseMasterAnimSpeed);
					}
				}
			}
		}

		// Token: 0x0600338D RID: 13197 RVA: 0x00103AD0 File Offset: 0x00101CD0
		protected override void ProcessDieEventSound()
		{
			for (int i = 0; i < this.m_DETemplet.m_listNKMDieEventSound.Count; i++)
			{
				NKMEventSound nkmeventSound = this.m_DETemplet.m_listNKMDieEventSound[i];
				string audioClipName;
				if (nkmeventSound != null && base.CheckEventCondition(nkmeventSound.m_Condition) && (this.m_DEData == null || nkmeventSound.IsRightSkin(this.m_DEData.GetMasterSkinID())) && NKMRandom.Range(0f, 1f) <= nkmeventSound.m_PlayRate && nkmeventSound.GetRandomSound(this.m_DEData, out audioClipName))
				{
					int item;
					if (!nkmeventSound.m_bVoice)
					{
						item = NKCSoundManager.PlaySound(audioClipName, nkmeventSound.m_fLocalVol, this.m_DEData.m_PosX, nkmeventSound.m_fFocusRange, nkmeventSound.m_bLoop, 0f, false, 0f);
					}
					else
					{
						item = NKCSoundManager.PlayVoice(audioClipName, 0, true, true, nkmeventSound.m_fLocalVol, this.m_DEData.m_PosX, nkmeventSound.m_fFocusRange, nkmeventSound.m_bLoop, 0f, false);
					}
					if (nkmeventSound.m_bStopSound)
					{
						this.m_listSoundUID.Add(item);
					}
				}
			}
		}

		// Token: 0x0600338E RID: 13198 RVA: 0x00103BEB File Offset: 0x00101DEB
		protected override void ProcessEventBuff(bool bStateEnd = false)
		{
		}

		// Token: 0x0600338F RID: 13199 RVA: 0x00103BED File Offset: 0x00101DED
		protected override void ProcessEventStatus(bool bStateEnd = false)
		{
		}

		// Token: 0x06003390 RID: 13200 RVA: 0x00103BEF File Offset: 0x00101DEF
		public override float GetEventMovePosX(NKMEventMove cNKMEventMove, bool isATeam)
		{
			if (this.m_NKCGameClient.IsReversePosTeam(this.m_NKCGameClient.m_MyTeam))
			{
				isATeam = !isATeam;
			}
			return base.GetEventMovePosX(cNKMEventMove, isATeam);
		}

		// Token: 0x04003241 RID: 12865
		private NKCGameClient m_NKCGameClient;

		// Token: 0x04003242 RID: 12866
		private NKCASEffect m_NKCASEffect;

		// Token: 0x04003243 RID: 12867
		private NKCASUnitShadow m_NKCASUnitShadow;

		// Token: 0x04003244 RID: 12868
		private List<NKCASEffect> m_listEffect = new List<NKCASEffect>();

		// Token: 0x04003245 RID: 12869
		private List<int> m_listSoundUID = new List<int>();

		// Token: 0x04003246 RID: 12870
		private bool m_bDissolveEnable;

		// Token: 0x04003247 RID: 12871
		private NKMTrackingFloat m_DissolveFactor = new NKMTrackingFloat();

		// Token: 0x04003248 RID: 12872
		private Vector3 m_Vector3Temp = new Vector3(0f, 0f, 0f);

		// Token: 0x04003249 RID: 12873
		private Vector3 m_Vector3Temp2 = new Vector3(0f, 0f, 0f);

		// Token: 0x0400324A RID: 12874
		private Color m_ColorTemp;
	}
}
