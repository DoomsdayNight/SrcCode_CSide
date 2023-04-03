using System;
using Cs.Logging;
using NKC.FX;
using NKM;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000639 RID: 1593
	public class NKCAnimSpine
	{
		// Token: 0x06003187 RID: 12679 RVA: 0x000F5BF0 File Offset: 0x000F3DF0
		public string GetAnimName()
		{
			return this.m_AnimName;
		}

		// Token: 0x06003189 RID: 12681 RVA: 0x000F5C20 File Offset: 0x000F3E20
		public void Init()
		{
			this.m_SpineObject = null;
			this.m_SPINE_ANIM_TYPE = SPINE_ANIM_TYPE.SAT_INVALID;
			this.m_SkeletonAnimation = null;
			this.m_SkeletonGraphic = null;
			this.m_TrackEntry = null;
			this.m_bObjectActive = false;
			this.m_AnimName = "";
			this.m_bLoop = false;
			this.m_fPlaySpeed = 1f;
			this.m_bAnimationEnd = false;
			this.m_bAnimStartThisFrame = false;
			this.m_AnimTimeNow = 0f;
			this.m_AnimTimeBefore = 0f;
			this.m_bShow = true;
			this.m_bUpdateThisFrame = false;
			this.m_fUpdateDeltaTime = 0f;
			this.m_Animator = null;
			this.m_Animators = null;
			this.m_NKC_FXM_PLAYERs = null;
			this.m_NKC_FX_DELAY_EXECUTERs = null;
			this.m_ParticleSystems = null;
			this.m_ParticleSystem_SimulationSpeedOrg = null;
			this.m_NKM_GAME_SPEED_TYPE = NKM_GAME_SPEED_TYPE.NGST_1;
		}

		// Token: 0x0600318A RID: 12682 RVA: 0x000F5CE4 File Offset: 0x000F3EE4
		public void SetAnimObj(GameObject spineObject, GameObject animatorObject = null, bool bPreload = false)
		{
			this.Init();
			this.m_SpineObject = spineObject;
			this.m_SkeletonAnimation = this.m_SpineObject.GetComponentInChildren<SkeletonAnimation>(true);
			if (this.m_SkeletonAnimation != null)
			{
				this.m_SPINE_ANIM_TYPE = SPINE_ANIM_TYPE.SAT_SPINE;
				if (bPreload)
				{
					MeshRenderer componentInChildren = this.m_SkeletonAnimation.GetComponentInChildren<MeshRenderer>(true);
					NKCScenManager.GetScenManager().ForceRender(componentInChildren);
				}
			}
			this.m_SkeletonGraphic = this.m_SpineObject.GetComponentInChildren<SkeletonGraphic>(true);
			if (this.m_SkeletonGraphic != null)
			{
				this.m_SPINE_ANIM_TYPE = SPINE_ANIM_TYPE.SAT_SPINE_UI;
				if (bPreload)
				{
					NKCScenManager.GetScenManager().ForceRender(this.m_SkeletonGraphic.mainTexture);
				}
			}
			if (this.m_SPINE_ANIM_TYPE == SPINE_ANIM_TYPE.SAT_INVALID)
			{
				this.Init();
				return;
			}
			this.ComponentEnable(false);
			if (animatorObject == null)
			{
				animatorObject = spineObject;
			}
			this.m_Animator = animatorObject.GetComponent<Animator>();
			if (this.m_Animator != null && this.m_Animator.enabled)
			{
				this.m_Animator.enabled = false;
			}
			this.m_Animators = animatorObject.GetComponentsInChildren<Animator>(true);
			for (int i = 0; i < this.m_Animators.Length; i++)
			{
				if (this.m_Animators[i].enabled)
				{
					this.m_Animators[i].enabled = false;
				}
			}
			this.m_NKC_FXM_PLAYERs = animatorObject.GetComponentsInChildren<NKC_FXM_PLAYER>(true);
			for (int j = 0; j < this.m_NKC_FXM_PLAYERs.Length; j++)
			{
				this.m_NKC_FXM_PLAYERs[j].SetUseGameUpdate(true);
			}
			this.m_NKC_FX_DELAY_EXECUTERs = animatorObject.GetComponentsInChildren<NKC_FX_DELAY_EXECUTER>(true);
			for (int k = 0; k < this.m_NKC_FX_DELAY_EXECUTERs.Length; k++)
			{
				this.m_NKC_FX_DELAY_EXECUTERs[k].SetUseGameUpdate(true);
			}
			this.m_ParticleSystems = animatorObject.GetComponentsInChildren<ParticleSystem>(true);
			this.m_ParticleSystem_SimulationSpeedOrg = new float[this.m_ParticleSystems.Length];
			for (int l = 0; l < this.m_ParticleSystems.Length; l++)
			{
				ParticleSystem particleSystem = this.m_ParticleSystems[l];
				if (!(particleSystem == null))
				{
					ParticleSystem.MainModule main = particleSystem.main;
					this.m_ParticleSystem_SimulationSpeedOrg[l] = main.simulationSpeed;
					if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME && NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_STATE() == NKC_SCEN_STATE.NSS_START)
					{
						main.simulationSpeed = this.m_ParticleSystem_SimulationSpeedOrg[l] * 1.1f;
					}
					else
					{
						main.simulationSpeed = this.m_ParticleSystem_SimulationSpeedOrg[l];
					}
				}
			}
			this.SetSpriteActive();
		}

		// Token: 0x0600318B RID: 12683 RVA: 0x000F5F24 File Offset: 0x000F4124
		public void ResetParticleSimulSpeedOrg()
		{
			if (this.m_ParticleSystems == null)
			{
				return;
			}
			if (this.m_ParticleSystem_SimulationSpeedOrg == null)
			{
				return;
			}
			for (int i = 0; i < this.m_ParticleSystems.Length; i++)
			{
				ParticleSystem particleSystem = this.m_ParticleSystems[i];
				if (!(particleSystem == null))
				{
					particleSystem.main.simulationSpeed = this.m_ParticleSystem_SimulationSpeedOrg[i];
				}
			}
			this.m_NKM_GAME_SPEED_TYPE = NKM_GAME_SPEED_TYPE.NGST_1;
		}

		// Token: 0x0600318C RID: 12684 RVA: 0x000F5F85 File Offset: 0x000F4185
		public void ComponentEnable(bool bEnable)
		{
			if (this.m_SPINE_ANIM_TYPE == SPINE_ANIM_TYPE.SAT_SPINE && this.m_SkeletonAnimation.enabled == !bEnable)
			{
				this.m_SkeletonAnimation.enabled = bEnable;
			}
		}

		// Token: 0x0600318D RID: 12685 RVA: 0x000F5FB0 File Offset: 0x000F41B0
		public Spine.Animation GetAnimByName(string animName)
		{
			SPINE_ANIM_TYPE spine_ANIM_TYPE = this.m_SPINE_ANIM_TYPE;
			if (spine_ANIM_TYPE == SPINE_ANIM_TYPE.SAT_SPINE)
			{
				return this.m_SkeletonAnimation.state.Data.SkeletonData.FindAnimation(animName);
			}
			if (spine_ANIM_TYPE != SPINE_ANIM_TYPE.SAT_SPINE_UI)
			{
				return null;
			}
			return this.m_SkeletonGraphic.AnimationState.Data.SkeletonData.FindAnimation(animName);
		}

		// Token: 0x0600318E RID: 12686 RVA: 0x000F6008 File Offset: 0x000F4208
		private void SetSpriteActive()
		{
			SPINE_ANIM_TYPE spine_ANIM_TYPE = this.m_SPINE_ANIM_TYPE;
			if (spine_ANIM_TYPE != SPINE_ANIM_TYPE.SAT_SPINE)
			{
				if (spine_ANIM_TYPE != SPINE_ANIM_TYPE.SAT_SPINE_UI)
				{
					return;
				}
				if (!this.m_SkeletonGraphic.gameObject.activeSelf || !this.m_SkeletonGraphic.gameObject.activeInHierarchy)
				{
					this.m_bObjectActive = false;
					return;
				}
				this.m_bObjectActive = true;
				return;
			}
			else
			{
				if (!this.m_SkeletonAnimation.gameObject.activeSelf || !this.m_SkeletonAnimation.gameObject.activeInHierarchy)
				{
					this.m_bObjectActive = false;
					return;
				}
				this.m_bObjectActive = true;
				return;
			}
		}

		// Token: 0x0600318F RID: 12687 RVA: 0x000F608C File Offset: 0x000F428C
		public void Update(float deltaTime)
		{
			if (this.m_SpineObject == null)
			{
				return;
			}
			SPINE_ANIM_TYPE spine_ANIM_TYPE = this.m_SPINE_ANIM_TYPE;
			if (spine_ANIM_TYPE != SPINE_ANIM_TYPE.SAT_SPINE)
			{
				if (spine_ANIM_TYPE != SPINE_ANIM_TYPE.SAT_SPINE_UI)
				{
					return;
				}
				if (this.m_SkeletonGraphic == null)
				{
					return;
				}
			}
			else if (this.m_SkeletonAnimation == null)
			{
				return;
			}
			if (!this.m_bObjectActive)
			{
				this.SetSpriteActive();
				if (this.m_bObjectActive)
				{
					this.Play(this.m_AnimName, this.m_bLoop, 0f);
				}
			}
			else
			{
				this.SetSpriteActive();
			}
			if (!this.m_bObjectActive)
			{
				return;
			}
			this.m_bAnimStartThisFrame = false;
			this.m_AnimTimeBefore = this.m_AnimTimeNow;
			this.m_AnimTimeNow = this.GetAnimTimeNow();
			spine_ANIM_TYPE = this.m_SPINE_ANIM_TYPE;
			if (spine_ANIM_TYPE != SPINE_ANIM_TYPE.SAT_SPINE)
			{
				if (spine_ANIM_TYPE == SPINE_ANIM_TYPE.SAT_SPINE_UI)
				{
					this.m_TrackEntry = this.m_SkeletonGraphic.AnimationState.GetCurrent(0);
				}
			}
			else
			{
				this.m_TrackEntry = this.m_SkeletonAnimation.AnimationState.GetCurrent(0);
			}
			if (this.IsAnimationEnd())
			{
				if (this.m_bLoop && this.m_bAnimationEnd)
				{
					this.Play(this.m_AnimName, this.m_bLoop, 0f);
				}
				this.m_bAnimationEnd = true;
			}
			else
			{
				NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
				if (gameOptionData != null)
				{
					if (gameOptionData.AnimationQuality == NKCGameOptionDataSt.GraphicOptionAnimationQuality.Normal)
					{
						this.m_bHalfUpdate = true;
					}
					else
					{
						this.m_bHalfUpdate = false;
					}
					if (gameOptionData.GameFrameLimit == NKCGameOptionDataSt.GraphicOptionGameFrameLimit.Thirty)
					{
						this.m_bHalfUpdate = false;
					}
				}
				if (this.m_bHalfUpdate)
				{
					this.m_fUpdateDeltaTime += deltaTime;
					this.m_bUpdateThisFrame = !this.m_bUpdateThisFrame;
					if (this.m_bUpdateThisFrame)
					{
						this.UpdateSpine(this.m_fUpdateDeltaTime);
						this.m_fUpdateDeltaTime = 0f;
					}
				}
				else
				{
					this.UpdateSpine(deltaTime);
				}
			}
			this.UpdateEffect(deltaTime);
		}

		// Token: 0x06003190 RID: 12688 RVA: 0x000F6238 File Offset: 0x000F4438
		public void UpdateSpine(float deltaTime)
		{
			if (this.m_SPINE_ANIM_TYPE == SPINE_ANIM_TYPE.SAT_SPINE)
			{
				this.m_SkeletonAnimation.Update(deltaTime);
				this.m_SkeletonAnimation.LateUpdate();
			}
			for (int i = 0; i < this.m_Animators.Length; i++)
			{
				Animator animator = this.m_Animators[i];
				if (animator != null && animator.gameObject.activeInHierarchy)
				{
					animator.Update(deltaTime);
				}
			}
		}

		// Token: 0x06003191 RID: 12689 RVA: 0x000F62A0 File Offset: 0x000F44A0
		public void UpdateEffect(float deltaTime)
		{
			try
			{
				for (int i = 0; i < this.m_NKC_FXM_PLAYERs.Length; i++)
				{
					NKC_FXM_PLAYER nkc_FXM_PLAYER = this.m_NKC_FXM_PLAYERs[i];
					if (nkc_FXM_PLAYER != null && nkc_FXM_PLAYER.gameObject.activeInHierarchy)
					{
						nkc_FXM_PLAYER.UpdateInternal(deltaTime);
					}
				}
				for (int j = 0; j < this.m_NKC_FX_DELAY_EXECUTERs.Length; j++)
				{
					NKC_FX_DELAY_EXECUTER nkc_FX_DELAY_EXECUTER = this.m_NKC_FX_DELAY_EXECUTERs[j];
					if (nkc_FX_DELAY_EXECUTER != null && nkc_FX_DELAY_EXECUTER.gameObject.activeInHierarchy)
					{
						nkc_FX_DELAY_EXECUTER.UpdateInternal(deltaTime);
					}
				}
			}
			catch (Exception exception)
			{
				Debug.LogError("Effect update failed. m_SpineObject: " + this.m_SpineObject.name);
				Debug.LogException(exception);
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME && NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_STATE() == NKC_SCEN_STATE.NSS_START && this.m_NKM_GAME_SPEED_TYPE != NKCScenManager.GetScenManager().GetGameClient().GetGameRuntimeData().m_NKM_GAME_SPEED_TYPE)
			{
				this.m_NKM_GAME_SPEED_TYPE = NKCScenManager.GetScenManager().GetGameClient().GetGameRuntimeData().m_NKM_GAME_SPEED_TYPE;
				for (int k = 0; k < this.m_ParticleSystems.Length; k++)
				{
					ParticleSystem particleSystem = this.m_ParticleSystems[k];
					if (!(particleSystem == null))
					{
						ParticleSystem.MainModule main = particleSystem.main;
						switch (this.m_NKM_GAME_SPEED_TYPE)
						{
						case NKM_GAME_SPEED_TYPE.NGST_1:
						case NKM_GAME_SPEED_TYPE.NGST_3:
						case NKM_GAME_SPEED_TYPE.NGST_10:
							main.simulationSpeed = this.m_ParticleSystem_SimulationSpeedOrg[k] * 1.1f;
							break;
						case NKM_GAME_SPEED_TYPE.NGST_2:
							main.simulationSpeed = this.m_ParticleSystem_SimulationSpeedOrg[k] * 1.5f;
							break;
						case NKM_GAME_SPEED_TYPE.NGST_05:
							main.simulationSpeed = this.m_ParticleSystem_SimulationSpeedOrg[k] * 0.6f;
							break;
						}
					}
				}
			}
		}

		// Token: 0x06003192 RID: 12690 RVA: 0x000F6458 File Offset: 0x000F4658
		public void PlayAnimator()
		{
			if (this.m_Animator == null)
			{
				return;
			}
			if (this.m_TrackEntry.TrackTime > 0f)
			{
				this.m_Animator.Play(this.m_AnimName, -1, this.m_TrackEntry.AnimationTime / this.m_TrackEntry.AnimationEnd);
			}
			else
			{
				this.m_Animator.Play(this.m_AnimName, -1, 0f);
			}
			this.m_Animator.Update(0.001f);
		}

		// Token: 0x06003193 RID: 12691 RVA: 0x000F64D8 File Offset: 0x000F46D8
		public void Play(string animName, bool bLoop, float fStartTime = 0f)
		{
			SPINE_ANIM_TYPE spine_ANIM_TYPE = this.m_SPINE_ANIM_TYPE;
			if (spine_ANIM_TYPE != SPINE_ANIM_TYPE.SAT_SPINE)
			{
				if (spine_ANIM_TYPE != SPINE_ANIM_TYPE.SAT_SPINE_UI)
				{
					return;
				}
				if (this.m_SkeletonGraphic == null)
				{
					return;
				}
			}
			else if (this.m_SkeletonAnimation == null)
			{
				return;
			}
			if (!this.m_bObjectActive)
			{
				this.SetSpriteActive();
			}
			if (!this.m_bObjectActive)
			{
				return;
			}
			Spine.Animation spineAnim = this.GetSpineAnim(animName);
			if (spineAnim == null)
			{
				return;
			}
			this.m_AnimName = animName;
			this.m_bLoop = bLoop;
			this.m_bAnimationEnd = false;
			this.m_bAnimStartThisFrame = true;
			this.m_AnimTimeNow = 0f;
			this.m_AnimTimeBefore = 0f;
			if (fStartTime > 0f)
			{
				this.m_AnimTimeNow = fStartTime;
				this.m_AnimTimeBefore = fStartTime;
			}
			spine_ANIM_TYPE = this.m_SPINE_ANIM_TYPE;
			if (spine_ANIM_TYPE != SPINE_ANIM_TYPE.SAT_SPINE)
			{
				if (spine_ANIM_TYPE == SPINE_ANIM_TYPE.SAT_SPINE_UI)
				{
					this.m_SkeletonGraphic.AnimationState.TimeScale = this.m_fPlaySpeed;
					if (this.m_SkeletonGraphic.Skeleton != null)
					{
						this.m_SkeletonGraphic.Skeleton.SetToSetupPose();
					}
					this.m_SkeletonGraphic.AnimationState.SetAnimation(0, spineAnim, false);
					this.m_TrackEntry = this.m_SkeletonGraphic.AnimationState.GetCurrent(0);
					if (fStartTime > 0f)
					{
						this.m_TrackEntry.AnimationLast = fStartTime;
					}
					else
					{
						this.m_TrackEntry.AnimationLast = -1f;
					}
					this.m_TrackEntry.TrackTime = fStartTime;
					this.m_SkeletonGraphic._Update(0.001f);
					this.m_SkeletonGraphic.LateUpdate();
				}
			}
			else
			{
				this.m_SkeletonAnimation.AnimationState.TimeScale = this.m_fPlaySpeed;
				if (this.m_SkeletonAnimation.Skeleton != null)
				{
					this.m_SkeletonAnimation.Skeleton.SetToSetupPose();
				}
				this.m_SkeletonAnimation.AnimationState.SetAnimation(0, spineAnim, false);
				this.m_TrackEntry = this.m_SkeletonAnimation.AnimationState.GetCurrent(0);
				if (fStartTime > 0f)
				{
					this.m_TrackEntry.AnimationLast = fStartTime;
				}
				else
				{
					this.m_TrackEntry.AnimationLast = -1f;
				}
				this.m_TrackEntry.TrackTime = fStartTime;
				this.m_SkeletonAnimation.Update(0.001f);
				this.m_SkeletonAnimation.LateUpdate();
			}
			this.PlayAnimator();
		}

		// Token: 0x06003194 RID: 12692 RVA: 0x000F66F8 File Offset: 0x000F48F8
		private Spine.Animation GetSpineAnim(string animName)
		{
			Spine.Animation animation = null;
			SPINE_ANIM_TYPE spine_ANIM_TYPE = this.m_SPINE_ANIM_TYPE;
			if (spine_ANIM_TYPE != SPINE_ANIM_TYPE.SAT_SPINE)
			{
				if (spine_ANIM_TYPE == SPINE_ANIM_TYPE.SAT_SPINE_UI)
				{
					if (this.m_SkeletonGraphic != null && this.m_SkeletonGraphic.AnimationState != null)
					{
						animation = this.m_SkeletonGraphic.AnimationState.Data.SkeletonData.FindAnimation(animName);
						if (animation == null && this.m_SkeletonGraphic.SkeletonDataAsset != null)
						{
							Debug.LogErrorFormat("NKCAnimSpine NoExistAnim {0}, {1}", new object[]
							{
								this.m_SkeletonGraphic.SkeletonDataAsset.name,
								animName
							});
						}
					}
					else
					{
						Debug.LogErrorFormat("NKCAnimSpine NoExistAnim {0}", new object[]
						{
							animName
						});
					}
				}
			}
			else if (this.m_SkeletonAnimation != null && this.m_SkeletonAnimation.AnimationState != null)
			{
				animation = this.m_SkeletonAnimation.AnimationState.Data.SkeletonData.FindAnimation(animName);
				if (animation == null && this.m_SkeletonAnimation.SkeletonDataAsset != null)
				{
					Debug.LogErrorFormat("NKCAnimSpine NoExistAnim {0}, {1}", new object[]
					{
						this.m_SkeletonAnimation.SkeletonDataAsset.name,
						animName
					});
				}
			}
			else
			{
				Debug.LogErrorFormat("NKCAnimSpine NoExistAnim {0}", new object[]
				{
					animName
				});
			}
			return animation;
		}

		// Token: 0x06003195 RID: 12693 RVA: 0x000F6841 File Offset: 0x000F4A41
		public float GetAnimTimeNow()
		{
			if (this.m_TrackEntry == null)
			{
				return 0f;
			}
			return this.m_TrackEntry.AnimationTime;
		}

		// Token: 0x06003196 RID: 12694 RVA: 0x000F685C File Offset: 0x000F4A5C
		public bool IsAnimationEnd()
		{
			return this.m_TrackEntry != null && this.m_TrackEntry.TrackTime >= this.m_TrackEntry.AnimationEnd;
		}

		// Token: 0x06003197 RID: 12695 RVA: 0x000F6881 File Offset: 0x000F4A81
		public void SetShow(bool bShow)
		{
			if (this.m_bShow == bShow)
			{
				return;
			}
			if (this.m_SpineObject != null)
			{
				this.m_SpineObject.SetActive(bShow);
			}
			this.m_bShow = bShow;
		}

		// Token: 0x06003198 RID: 12696 RVA: 0x000F68B0 File Offset: 0x000F4AB0
		public void SetPlaySpeed(float fSpeed)
		{
			this.m_fPlaySpeed = fSpeed;
			SPINE_ANIM_TYPE spine_ANIM_TYPE = this.m_SPINE_ANIM_TYPE;
			if (spine_ANIM_TYPE != SPINE_ANIM_TYPE.SAT_SPINE)
			{
				if (spine_ANIM_TYPE == SPINE_ANIM_TYPE.SAT_SPINE_UI)
				{
					if (this.m_SkeletonGraphic != null && this.m_SkeletonGraphic.AnimationState != null)
					{
						this.m_SkeletonGraphic.AnimationState.TimeScale = this.m_fPlaySpeed;
					}
					else
					{
						Log.Error("m_SkeletonGraphic null, m_SpineObject: " + this.m_SpineObject.name, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCAnimSpine.cs", 636);
					}
				}
			}
			else if (this.m_SkeletonAnimation != null && this.m_SkeletonAnimation.AnimationState != null)
			{
				this.m_SkeletonAnimation.AnimationState.TimeScale = this.m_fPlaySpeed;
			}
			else
			{
				Log.Error("m_SkeletonAnimation null, m_SpineObject: " + this.m_SpineObject.name, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCAnimSpine.cs", 628);
			}
			if (this.m_Animator != null)
			{
				this.m_Animator.speed = this.m_fPlaySpeed;
			}
		}

		// Token: 0x06003199 RID: 12697 RVA: 0x000F69A7 File Offset: 0x000F4BA7
		public bool EventTimer(float fTime)
		{
			return (fTime == 0f && this.m_bAnimStartThisFrame) || (fTime > this.m_AnimTimeBefore && fTime <= this.m_AnimTimeNow);
		}

		// Token: 0x040030B8 RID: 12472
		private GameObject m_SpineObject;

		// Token: 0x040030B9 RID: 12473
		private SPINE_ANIM_TYPE m_SPINE_ANIM_TYPE;

		// Token: 0x040030BA RID: 12474
		private SkeletonAnimation m_SkeletonAnimation;

		// Token: 0x040030BB RID: 12475
		private SkeletonGraphic m_SkeletonGraphic;

		// Token: 0x040030BC RID: 12476
		private TrackEntry m_TrackEntry;

		// Token: 0x040030BD RID: 12477
		private bool m_bObjectActive;

		// Token: 0x040030BE RID: 12478
		private string m_AnimName = "";

		// Token: 0x040030BF RID: 12479
		private bool m_bLoop;

		// Token: 0x040030C0 RID: 12480
		private float m_fPlaySpeed = 1f;

		// Token: 0x040030C1 RID: 12481
		private bool m_bAnimationEnd;

		// Token: 0x040030C2 RID: 12482
		private bool m_bAnimStartThisFrame;

		// Token: 0x040030C3 RID: 12483
		private float m_AnimTimeNow;

		// Token: 0x040030C4 RID: 12484
		private float m_AnimTimeBefore;

		// Token: 0x040030C5 RID: 12485
		private bool m_bShow = true;

		// Token: 0x040030C6 RID: 12486
		private bool m_bHalfUpdate;

		// Token: 0x040030C7 RID: 12487
		private bool m_bUpdateThisFrame;

		// Token: 0x040030C8 RID: 12488
		private float m_fUpdateDeltaTime;

		// Token: 0x040030C9 RID: 12489
		private Animator m_Animator;

		// Token: 0x040030CA RID: 12490
		private Animator[] m_Animators;

		// Token: 0x040030CB RID: 12491
		private NKC_FXM_PLAYER[] m_NKC_FXM_PLAYERs;

		// Token: 0x040030CC RID: 12492
		private NKC_FX_DELAY_EXECUTER[] m_NKC_FX_DELAY_EXECUTERs;

		// Token: 0x040030CD RID: 12493
		private ParticleSystem[] m_ParticleSystems;

		// Token: 0x040030CE RID: 12494
		private float[] m_ParticleSystem_SimulationSpeedOrg;

		// Token: 0x040030CF RID: 12495
		private NKM_GAME_SPEED_TYPE m_NKM_GAME_SPEED_TYPE;
	}
}
