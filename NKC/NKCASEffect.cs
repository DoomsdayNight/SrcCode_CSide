using System;
using Cs.Logging;
using NKC.FX;
using NKC.UI;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x0200066F RID: 1647
	public class NKCASEffect : NKMObjectPoolData
	{
		// Token: 0x060033FF RID: 13311 RVA: 0x00104D64 File Offset: 0x00102F64
		public NKMUnit GetMasterUnit()
		{
			if (this.m_MasterUnit != null)
			{
				return this.m_MasterUnit;
			}
			if (this.m_MasterUnitGameUID != 0 && NKCScenManager.GetScenManager().GetGameClient() != null)
			{
				this.m_MasterUnit = NKCScenManager.GetScenManager().GetGameClient().GetUnit(this.m_MasterUnitGameUID, true, false);
			}
			return this.m_MasterUnit;
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06003400 RID: 13312 RVA: 0x00104DB7 File Offset: 0x00102FB7
		public bool CanIgnoreStopTime
		{
			get
			{
				return this.m_canIgnoreStopTime;
			}
		}

		// Token: 0x06003401 RID: 13313 RVA: 0x00104DBF File Offset: 0x00102FBF
		public void SetCanIgnoreStopTime(bool bValue)
		{
			this.m_canIgnoreStopTime = bValue;
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06003402 RID: 13314 RVA: 0x00104DC8 File Offset: 0x00102FC8
		public bool ApplyStopTime
		{
			get
			{
				return this.m_applyStopTime;
			}
		}

		// Token: 0x06003403 RID: 13315 RVA: 0x00104DD0 File Offset: 0x00102FD0
		public void SetApplyStopTime(bool bValue)
		{
			this.m_applyStopTime = bValue;
		}

		// Token: 0x06003404 RID: 13316 RVA: 0x00104DD9 File Offset: 0x00102FD9
		public void SetUseMasterAnimSpeed(bool bValue)
		{
			this.m_UseMasterAnimSpeed = bValue;
		}

		// Token: 0x06003405 RID: 13317 RVA: 0x00104DE2 File Offset: 0x00102FE2
		public bool GetLoadFail()
		{
			return this.m_EffectInstant == null || this.m_EffectInstant.GetLoadFail();
		}

		// Token: 0x06003406 RID: 13318 RVA: 0x00104DFC File Offset: 0x00102FFC
		public NKCASEffect(string bundleName, string name, bool bAsync = false)
		{
			this.m_NKM_OBJECT_POOL_TYPE = NKM_OBJECT_POOL_TYPE.NOPT_NKCASEffect;
			this.m_ObjectPoolBundleName = bundleName;
			this.m_ObjectPoolName = name;
			this.m_bUnloadable = true;
			this.Load(bAsync);
		}

		// Token: 0x06003407 RID: 13319 RVA: 0x00104E49 File Offset: 0x00103049
		public override void Load(bool bAsync)
		{
			this.Init();
			this.m_EffectInstant = NKCAssetResourceManager.OpenInstance<GameObject>(this.m_ObjectPoolBundleName, this.m_ObjectPoolName, bAsync, null);
		}

		// Token: 0x06003408 RID: 13320 RVA: 0x00104E6C File Offset: 0x0010306C
		public override bool LoadComplete()
		{
			if (this.GetLoadFail())
			{
				return false;
			}
			this.m_RectTransform = this.m_EffectInstant.m_Instant.GetComponentInChildren<RectTransform>(true);
			this.m_SpriteRenderer = this.m_EffectInstant.m_Instant.GetComponentsInChildren<SpriteRenderer>(true);
			this.m_ParticleSystemRenderer = this.m_EffectInstant.m_Instant.GetComponentsInChildren<ParticleSystemRenderer>(true);
			this.m_Animator = this.m_EffectInstant.m_Instant.GetComponentInChildren<Animator>(true);
			this.m_ParticleSystems = this.m_EffectInstant.m_Instant.GetComponentsInChildren<ParticleSystem>(true);
			this.m_ParticleSystem_SimulationSpeedOrg = new float[this.m_ParticleSystems.Length];
			this.m_Cameras = this.m_EffectInstant.m_Instant.GetComponentsInChildren<Camera>(true);
			this.LoadComplete_Anim();
			if (!this.LoadComplete_Spine())
			{
				this.LoadComplete_SpineUI();
			}
			if (!this.m_bSpine)
			{
				this.m_NKC_FXM_PLAYERs = this.m_EffectInstant.m_Instant.GetComponentsInChildren<NKC_FXM_PLAYER>(true);
				this.m_NKC_FXM_PLAYER_TimeScaleOrg = new float[this.m_NKC_FXM_PLAYERs.Length];
				for (int i = 0; i < this.m_NKC_FXM_PLAYERs.Length; i++)
				{
					NKC_FXM_PLAYER nkc_FXM_PLAYER = this.m_NKC_FXM_PLAYERs[i];
					this.m_NKC_FXM_PLAYER_TimeScaleOrg[i] = nkc_FXM_PLAYER.TimeScale;
				}
				this.m_NKC_FX_DELAY_EXECUTERs = this.m_EffectInstant.m_Instant.GetComponentsInChildren<NKC_FX_DELAY_EXECUTER>(true);
				this.m_NKC_FX_DELAY_EXECUTER_TimeScaleOrg = new float[this.m_NKC_FX_DELAY_EXECUTERs.Length];
				for (int j = 0; j < this.m_NKC_FX_DELAY_EXECUTERs.Length; j++)
				{
					NKC_FX_DELAY_EXECUTER nkc_FX_DELAY_EXECUTER = this.m_NKC_FX_DELAY_EXECUTERs[j];
					this.m_NKC_FX_DELAY_EXECUTER_TimeScaleOrg[j] = nkc_FX_DELAY_EXECUTER.TimeScale;
				}
				for (int k = 0; k < this.m_ParticleSystems.Length; k++)
				{
					ParticleSystem particleSystem = this.m_ParticleSystems[k];
					if (!(particleSystem == null))
					{
						ParticleSystem.MainModule main = particleSystem.main;
						this.m_ParticleSystem_SimulationSpeedOrg[k] = main.simulationSpeed;
					}
				}
			}
			Vector3 localScale = this.m_EffectInstant.m_InstantOrg.GetAsset<GameObject>().transform.localScale;
			this.m_fScaleX = localScale.x;
			this.m_fScaleY = localScale.y;
			this.m_fScaleZ = localScale.z;
			this.m_RotateOrg = this.m_EffectInstant.m_InstantOrg.GetAsset<GameObject>().transform.localEulerAngles;
			return true;
		}

		// Token: 0x06003409 RID: 13321 RVA: 0x00105090 File Offset: 0x00103290
		public bool LoadComplete_Anim()
		{
			if (this.m_Animator == null)
			{
				return false;
			}
			this.m_Anim2D.SetAnimObj(this.m_EffectInstant.m_Instant);
			if (this.m_Anim2D.GetAnimClipByName(this.m_ObjectPoolName + "_END") != null)
			{
				this.m_bEndAnim = true;
			}
			if (this.m_Anim2D.GetAnimClipByName(this.m_ObjectPoolName + "_BASE_LOOP") != null)
			{
				this.m_Anim2D.SetAnimAutoChange("BASE", "BASE_LOOP", true, 1f);
			}
			return true;
		}

		// Token: 0x0600340A RID: 13322 RVA: 0x0010512C File Offset: 0x0010332C
		public bool LoadComplete_Spine()
		{
			this.m_SPINE_SkeletonAnimation = null;
			Transform transform = this.m_EffectInstant.m_Instant.transform.Find("SPINE_SkeletonAnimation");
			if (transform == null)
			{
				return false;
			}
			this.m_SPINE_SkeletonAnimation = transform.gameObject;
			if (this.m_SPINE_SkeletonAnimation == null)
			{
				return false;
			}
			this.m_bSpine = true;
			this.m_MeshRenderer = this.m_SPINE_SkeletonAnimation.GetComponentInChildren<MeshRenderer>(true);
			this.m_Material = this.m_MeshRenderer.sharedMaterial;
			this.m_AnimSpine.SetAnimObj(this.m_EffectInstant.m_Instant, null, true);
			if (this.m_AnimSpine.GetAnimByName("END") != null)
			{
				this.m_bEndAnim = true;
			}
			Transform transform2 = this.m_EffectInstant.m_InstantOrg.GetAsset<GameObject>().transform.Find("SPINE_SkeletonAnimation");
			if (transform2 == null)
			{
				Log.Error(this.m_ObjectPoolName + " has no sub prefab name SPINE_SkeletonAnimation", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCEffectManager.cs", 290);
				return false;
			}
			this.m_SPINE_SkeletonAnimationOrg = transform2.gameObject;
			this.m_MeshRendererOrg = this.m_SPINE_SkeletonAnimationOrg.GetComponentInChildren<MeshRenderer>(true);
			if (this.m_MeshRendererOrg.sharedMaterial != null)
			{
				if (this.m_Material == null)
				{
					this.m_Material = new Material(this.m_MeshRendererOrg.sharedMaterial);
					this.m_MeshRenderer.sharedMaterial = this.m_Material;
				}
				this.m_DissolveMaterial = new Material(this.m_MeshRendererOrg.sharedMaterial);
				this.m_DissolveMaterial.EnableKeyword("DISSOLVE_ON");
				if (!this.m_DissolveMaterial.HasProperty("_DissolveGlowColor"))
				{
					Log.Error("m_DissolveMaterial does not have _DissolveGlowColor prop, m_ObjectPoolName: " + this.m_ObjectPoolName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCEffectManager.cs", 309);
				}
				this.m_DissolveColorOrg = this.m_DissolveMaterial.GetColor("_DissolveGlowColor");
			}
			else
			{
				Log.Error("m_MeshRendererOrg.sharedMaterial is null, m_ObjectPoolName: " + this.m_ObjectPoolName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCEffectManager.cs", 315);
			}
			return true;
		}

		// Token: 0x0600340B RID: 13323 RVA: 0x00105320 File Offset: 0x00103520
		public bool LoadComplete_SpineUI()
		{
			this.m_SPINE_SkeletonGraphic = null;
			Transform transform = this.m_EffectInstant.m_Instant.transform.Find("SPINE_SkeletonGraphic");
			if (transform == null)
			{
				return false;
			}
			this.m_SPINE_SkeletonGraphic = transform.gameObject;
			if (this.m_SPINE_SkeletonGraphic == null)
			{
				return false;
			}
			this.m_bSpineUI = true;
			this.m_AnimSpine.SetAnimObj(this.m_EffectInstant.m_Instant, null, true);
			return true;
		}

		// Token: 0x0600340C RID: 13324 RVA: 0x00105398 File Offset: 0x00103598
		public void Init()
		{
			this.m_EffectUID = 0;
			this.m_NKM_EFFECT_PARENT_TYPE = NKM_EFFECT_PARENT_TYPE.NEPT_NUM_GAME_BATTLE_EFFECT;
			this.m_AnimName = "";
			this.m_fAnimSpeed = 1f;
			this.m_fReserveDieTime = -1f;
			this.m_bAutoDie = true;
			this.m_bRight = true;
			this.m_OffsetX = 0f;
			this.m_OffsetY = 0f;
			this.m_OffsetZ = 0f;
			this.m_fAddRotate = 0f;
			if (this.m_EffectInstant != null && this.m_EffectInstant.m_InstantOrg != null && !this.m_EffectInstant.GetLoadFail())
			{
				Vector3 localScale = this.m_EffectInstant.m_InstantOrg.GetAsset<GameObject>().transform.localScale;
				this.m_fScaleX = localScale.x;
				this.m_fScaleY = localScale.y;
				this.m_fScaleZ = localScale.z;
			}
			else
			{
				this.m_fScaleX = 1f;
				this.m_fScaleY = 1f;
				this.m_fScaleZ = 1f;
			}
			this.m_fScaleFactorX = 1f;
			this.m_fScaleFactorY = 1f;
			this.m_fScaleFactorZ = 1f;
			this.m_bUseZScale = false;
			this.m_bScaleChange = false;
			this.m_BoneName = "";
			this.m_bUseBoneRotate = false;
			this.m_bStateEndStop = false;
			this.m_bStateEndStopForce = false;
			this.m_bCutIn = false;
			this.m_UseMasterAnimSpeed = false;
			this.m_MasterUnitGameUID = 0;
			this.m_fAnimSpeedFinal = 1f;
			this.m_bDie = false;
			this.m_bDEEffect = false;
			this.m_NKM_GAME_SPEED_TYPE = NKM_GAME_SPEED_TYPE.NGST_1;
		}

		// Token: 0x0600340D RID: 13325 RVA: 0x00105518 File Offset: 0x00103718
		public override void Open()
		{
			this.m_bPlayed = false;
			if (!this.m_bSpine && this.m_NKC_FXM_PLAYERs != null)
			{
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME && NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_STATE() == NKC_SCEN_STATE.NSS_START)
				{
					this.m_NKM_GAME_SPEED_TYPE = NKM_GAME_SPEED_TYPE.NGST_1;
					for (int i = 0; i < this.m_NKC_FXM_PLAYERs.Length; i++)
					{
						this.m_NKC_FXM_PLAYERs[i].TimeScale = this.m_NKC_FXM_PLAYER_TimeScaleOrg[i] * 1.1f;
					}
					for (int j = 0; j < this.m_NKC_FX_DELAY_EXECUTERs.Length; j++)
					{
						this.m_NKC_FX_DELAY_EXECUTERs[j].TimeScale = this.m_NKC_FX_DELAY_EXECUTER_TimeScaleOrg[j] * 1.1f;
					}
				}
				else
				{
					this.m_NKM_GAME_SPEED_TYPE = NKM_GAME_SPEED_TYPE.NGST_1;
					for (int k = 0; k < this.m_NKC_FXM_PLAYERs.Length; k++)
					{
						this.m_NKC_FXM_PLAYERs[k].TimeScale = this.m_NKC_FXM_PLAYER_TimeScaleOrg[k];
					}
					for (int l = 0; l < this.m_NKC_FX_DELAY_EXECUTERs.Length; l++)
					{
						this.m_NKC_FX_DELAY_EXECUTERs[l].TimeScale = this.m_NKC_FX_DELAY_EXECUTER_TimeScaleOrg[l];
					}
				}
				for (int m = 0; m < this.m_ParticleSystems.Length; m++)
				{
					ParticleSystem particleSystem = this.m_ParticleSystems[m];
					if (!(particleSystem == null))
					{
						ParticleSystem.MainModule main = particleSystem.main;
						if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME && NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_STATE() == NKC_SCEN_STATE.NSS_START)
						{
							main.simulationSpeed = this.m_ParticleSystem_SimulationSpeedOrg[m] * 1.1f;
						}
						else
						{
							main.simulationSpeed = this.m_ParticleSystem_SimulationSpeedOrg[m];
						}
					}
				}
			}
		}

		// Token: 0x0600340E RID: 13326 RVA: 0x0010569C File Offset: 0x0010389C
		public override void Close()
		{
			if (this.m_AnimSpine != null)
			{
				this.m_AnimSpine.ResetParticleSimulSpeedOrg();
			}
			if (this.m_EffectInstant != null && this.m_EffectInstant.m_Instant != null && !this.m_EffectInstant.GetLoadFail() && this.m_EffectInstant.m_Instant.activeSelf)
			{
				this.m_EffectInstant.m_Instant.SetActive(false);
			}
			if (this.m_MeshRenderer != null && this.m_Material != null)
			{
				this.m_MeshRenderer.sharedMaterial = this.m_Material;
			}
			this.Init();
		}

		// Token: 0x0600340F RID: 13327 RVA: 0x0010573C File Offset: 0x0010393C
		public void ObjectParentWait()
		{
			if (this.m_EffectInstant == null || this.m_EffectInstant.m_Instant == null)
			{
				return;
			}
			if (this.m_EffectInstant.m_Instant.transform.parent != NKCUIManager.m_TR_NKM_WAIT_INSTANT)
			{
				this.m_EffectInstant.m_Instant.transform.SetParent(NKCUIManager.m_TR_NKM_WAIT_INSTANT, false);
			}
		}

		// Token: 0x06003410 RID: 13328 RVA: 0x001057A4 File Offset: 0x001039A4
		public void ObjectParentRestore()
		{
			if (this.m_EffectInstant == null || this.m_EffectInstant.m_Instant == null)
			{
				return;
			}
			switch (this.m_NKM_EFFECT_PARENT_TYPE)
			{
			case NKM_EFFECT_PARENT_TYPE.NEPT_NUM_GAME_BATTLE_EFFECT:
				if (NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUM_GAME_BATTLE_EFFECT() != null && this.m_EffectInstant.m_Instant.transform.parent.gameObject != NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUM_GAME_BATTLE_EFFECT())
				{
					this.m_EffectInstant.m_Instant.transform.SetParent(NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUM_GAME_BATTLE_EFFECT().transform, false);
					return;
				}
				break;
			case NKM_EFFECT_PARENT_TYPE.NEPT_NUF_BEFORE_HUD_EFFECT:
				if (NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUF_BEFORE_HUD_EFFECT() != null && this.m_EffectInstant.m_Instant.transform.parent.gameObject != NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUF_BEFORE_HUD_EFFECT())
				{
					this.m_EffectInstant.m_Instant.transform.SetParent(NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUF_BEFORE_HUD_EFFECT().transform, false);
					return;
				}
				break;
			case NKM_EFFECT_PARENT_TYPE.NEPT_NUF_BEFORE_HUD_CONTROL_EFFECT:
				if (NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUF_BEFORE_HUD_CONTROL_EFFECT() != null && this.m_EffectInstant.m_Instant.transform.parent.gameObject != NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUF_BEFORE_HUD_CONTROL_EFFECT())
				{
					this.m_EffectInstant.m_Instant.transform.SetParent(NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUF_BEFORE_HUD_CONTROL_EFFECT().transform, false);
					return;
				}
				break;
			case NKM_EFFECT_PARENT_TYPE.NEPT_NUF_AFTER_HUD_EFFECT:
				if (NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUF_AFTER_HUD_EFFECT() != null && this.m_EffectInstant.m_Instant.transform.parent.gameObject != NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUF_AFTER_HUD_EFFECT())
				{
					this.m_EffectInstant.m_Instant.transform.SetParent(NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_GAME_UI_DATA().Get_NUF_AFTER_HUD_EFFECT().transform, false);
					return;
				}
				break;
			case NKM_EFFECT_PARENT_TYPE.NEPT_NUF_AFTER_UI_EFFECT:
				if (NKCScenManager.GetScenManager().Get_NUF_AFTER_UI_EFFECT() != null && this.m_EffectInstant.m_Instant.transform.parent != NKCScenManager.GetScenManager().Get_NUF_AFTER_UI_EFFECT())
				{
					this.m_EffectInstant.m_Instant.transform.SetParent(NKCScenManager.GetScenManager().Get_NUF_AFTER_UI_EFFECT(), false);
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06003411 RID: 13329 RVA: 0x00105A64 File Offset: 0x00103C64
		public override void Unload()
		{
			if (this.m_Cameras != null)
			{
				for (int i = 0; i < this.m_Cameras.Length; i++)
				{
					this.m_Cameras[i].targetTexture = null;
				}
			}
			this.m_Cameras = null;
			NKCAssetResourceManager.CloseInstance(this.m_EffectInstant);
			this.m_EffectInstant = null;
			this.m_RectTransform = null;
			this.m_SpriteRenderer = null;
			this.m_ParticleSystemRenderer = null;
			this.m_Animator = null;
			this.m_ParticleSystems = null;
			this.m_Anim2D.Init();
			this.m_Anim2D = null;
			this.m_SPINE_SkeletonAnimationOrg = null;
			this.m_MeshRendererOrg = null;
			this.m_SPINE_SkeletonAnimation = null;
			this.m_SPINE_SkeletonGraphic = null;
			this.m_MeshRenderer = null;
			this.m_AnimSpine.Init();
			this.m_AnimSpine = null;
			this.m_Material = null;
			this.m_DissolveMaterial = null;
			this.m_BuffText = null;
			this.m_DamageText = null;
			this.m_DamageTextCritical = null;
			this.m_AB_FX_COST_COUNT_Text = null;
			this.m_NKM_UI_HUD_COOLTIME_COUNT_Text = null;
			this.m_AB_FX_SKILL_CUTIN_COMMON_DESC_UNIT_NAME = null;
			this.m_AB_FX_SKILL_CUTIN_COMMON_DESC_SKILL_NAME = null;
			this.m_AB_FX_SKILL_CUTIN_COMMON_DESC_UNIT_NAME_RectTransform = null;
			this.m_AB_FX_SKILL_CUTIN_COMMON_DESC_SKILL_NAME_RectTransform = null;
		}

		// Token: 0x06003412 RID: 13330 RVA: 0x00105B6C File Offset: 0x00103D6C
		public void Update(float fDeltaTime)
		{
			if (this.m_fReserveDieTime != -1f && this.m_fReserveDieTime > 0f)
			{
				this.m_fReserveDieTime -= fDeltaTime;
				if (this.m_fReserveDieTime <= 0f)
				{
					this.m_fReserveDieTime = 0f;
					this.Stop(false);
				}
			}
			if (this.m_bScaleChange || this.m_bUseZScale)
			{
				if (this.m_EffectInstant != null && this.m_EffectInstant.m_Instant != null)
				{
					if (this.m_bUseZScale)
					{
						float zscaleFactor = NKCScenManager.GetScenManager().GetGameClient().GetZScaleFactor(this.m_EffectInstant.m_Instant.transform.position.z);
						NKCUtil.SetGameObjectLocalScale(this.m_EffectInstant.m_Instant, this.m_fScaleFactorX * this.m_fScaleX * zscaleFactor, this.m_fScaleFactorY * this.m_fScaleY * zscaleFactor, -1f);
					}
					else
					{
						NKCUtil.SetGameObjectLocalScale(this.m_EffectInstant.m_Instant, this.m_fScaleFactorX * this.m_fScaleX, this.m_fScaleFactorY * this.m_fScaleY, -1f);
					}
				}
				this.m_bScaleChange = false;
			}
			if (this.m_UseMasterAnimSpeed)
			{
				float num = this.MakeAnimSpeed();
				if (Mathf.Abs(this.m_fAnimSpeedFinal - num) > 0.01f)
				{
					this.m_fAnimSpeedFinal = num;
					if (this.m_Animator != null)
					{
						this.m_Anim2D.SetPlaySpeed(this.m_fAnimSpeedFinal);
					}
					if (this.m_SPINE_SkeletonAnimation != null)
					{
						this.m_AnimSpine.SetPlaySpeed(this.m_fAnimSpeedFinal);
					}
					else if (this.m_SPINE_SkeletonGraphic != null)
					{
						this.m_AnimSpine.SetPlaySpeed(this.m_fAnimSpeedFinal);
					}
				}
			}
			if (this.m_Animator != null)
			{
				this.m_Anim2D.Update(fDeltaTime);
			}
			if (this.m_SPINE_SkeletonAnimation != null)
			{
				this.m_AnimSpine.Update(fDeltaTime);
				if (this.m_bEndAnim && this.m_AnimName.Length > 1 && this.m_AnimSpine.IsAnimationEnd() && this.m_AnimSpine.GetAnimName().CompareTo("END") != 0)
				{
					this.m_AnimSpine.Play("END", false, 0f);
				}
			}
			else if (this.m_SPINE_SkeletonGraphic != null)
			{
				this.m_AnimSpine.Update(fDeltaTime);
			}
			if (this.m_NKC_FXM_PLAYERs != null && NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME && NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_STATE() == NKC_SCEN_STATE.NSS_START && this.m_NKM_GAME_SPEED_TYPE != NKCScenManager.GetScenManager().GetGameClient().GetGameRuntimeData().m_NKM_GAME_SPEED_TYPE)
			{
				this.m_NKM_GAME_SPEED_TYPE = NKCScenManager.GetScenManager().GetGameClient().GetGameRuntimeData().m_NKM_GAME_SPEED_TYPE;
				for (int i = 0; i < this.m_NKC_FXM_PLAYERs.Length; i++)
				{
					NKC_FXM_PLAYER nkc_FXM_PLAYER = this.m_NKC_FXM_PLAYERs[i];
					switch (this.m_NKM_GAME_SPEED_TYPE)
					{
					case NKM_GAME_SPEED_TYPE.NGST_1:
					case NKM_GAME_SPEED_TYPE.NGST_3:
					case NKM_GAME_SPEED_TYPE.NGST_10:
						nkc_FXM_PLAYER.TimeScale = this.m_NKC_FXM_PLAYER_TimeScaleOrg[i] * 1.1f;
						break;
					case NKM_GAME_SPEED_TYPE.NGST_2:
						nkc_FXM_PLAYER.TimeScale = this.m_NKC_FXM_PLAYER_TimeScaleOrg[i] * 1.5f;
						break;
					case NKM_GAME_SPEED_TYPE.NGST_05:
						nkc_FXM_PLAYER.TimeScale = this.m_NKC_FXM_PLAYER_TimeScaleOrg[i] * 0.6f;
						break;
					}
				}
				for (int j = 0; j < this.m_NKC_FX_DELAY_EXECUTERs.Length; j++)
				{
					NKC_FX_DELAY_EXECUTER nkc_FX_DELAY_EXECUTER = this.m_NKC_FX_DELAY_EXECUTERs[j];
					switch (this.m_NKM_GAME_SPEED_TYPE)
					{
					case NKM_GAME_SPEED_TYPE.NGST_1:
					case NKM_GAME_SPEED_TYPE.NGST_3:
					case NKM_GAME_SPEED_TYPE.NGST_10:
						nkc_FX_DELAY_EXECUTER.TimeScale = this.m_NKC_FX_DELAY_EXECUTER_TimeScaleOrg[j] * 1.1f;
						break;
					case NKM_GAME_SPEED_TYPE.NGST_2:
						nkc_FX_DELAY_EXECUTER.TimeScale = this.m_NKC_FX_DELAY_EXECUTER_TimeScaleOrg[j] * 1.5f;
						break;
					case NKM_GAME_SPEED_TYPE.NGST_05:
						nkc_FX_DELAY_EXECUTER.TimeScale = this.m_NKC_FX_DELAY_EXECUTER_TimeScaleOrg[j] * 0.6f;
						break;
					}
				}
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
			if (this.DieCheck())
			{
				this.SetDie();
			}
		}

		// Token: 0x06003413 RID: 13331 RVA: 0x00105FEC File Offset: 0x001041EC
		private bool DieCheck()
		{
			bool result = true;
			if (this.m_Animator != null && this.m_Anim2D != null && this.m_AnimName.Length > 1)
			{
				if (!this.m_bEndAnim)
				{
					if (!this.m_Anim2D.IsAnimationEnd())
					{
						result = false;
					}
				}
				else if (!this.m_Anim2D.IsAnimationEnd() || this.m_Anim2D.GetAnimName().CompareTo("END") != 0)
				{
					result = false;
				}
			}
			if (this.m_SPINE_SkeletonAnimation != null && this.m_AnimSpine != null && this.m_AnimName.Length > 1)
			{
				if (!this.m_bEndAnim)
				{
					if (!this.m_AnimSpine.IsAnimationEnd())
					{
						result = false;
					}
				}
				else if (!this.m_AnimSpine.IsAnimationEnd() || this.m_AnimSpine.GetAnimName().CompareTo("END") != 0)
				{
					result = false;
				}
			}
			else if (this.m_SPINE_SkeletonGraphic != null && this.m_AnimSpine != null && this.m_AnimName.Length > 1 && !this.m_AnimSpine.IsAnimationEnd())
			{
				result = false;
			}
			if (this.m_ParticleSystems != null)
			{
				for (int i = 0; i < this.m_ParticleSystems.Length; i++)
				{
					if (!(this.m_ParticleSystems[i] == null))
					{
						if (this.m_ParticleSystems[i].isEmitting && this.m_ParticleSystems[i].emission.enabled)
						{
							result = false;
						}
						if (this.m_ParticleSystems[i].particleCount > 0)
						{
							result = false;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06003414 RID: 13332 RVA: 0x0010615C File Offset: 0x0010435C
		public void SetDie()
		{
			this.m_bDie = true;
		}

		// Token: 0x06003415 RID: 13333 RVA: 0x00106165 File Offset: 0x00104365
		public void SetReserveDieTime(float fReserveDieTime)
		{
			this.m_fReserveDieTime = fReserveDieTime;
		}

		// Token: 0x06003416 RID: 13334 RVA: 0x00106170 File Offset: 0x00104370
		public void ReStart()
		{
			this.m_bDie = false;
			if (this.m_EffectInstant != null && this.m_EffectInstant.m_Instant != null)
			{
				if (this.m_EffectInstant.m_Instant.activeSelf)
				{
					this.m_EffectInstant.m_Instant.SetActive(false);
				}
				this.m_EffectInstant.m_Instant.SetActive(true);
			}
			if (this.m_Animator != null && this.m_AnimName.Length > 1)
			{
				this.m_Anim2D.Play(this.m_AnimName, false, 0f);
			}
			if (this.m_SPINE_SkeletonAnimation != null && this.m_AnimName.Length > 1)
			{
				this.m_AnimSpine.Play(this.m_AnimName, false, 0f);
				return;
			}
			if (this.m_SPINE_SkeletonGraphic != null && this.m_AnimName.Length > 1)
			{
				this.m_AnimSpine.Play(this.m_AnimName, false, 0f);
			}
		}

		// Token: 0x06003417 RID: 13335 RVA: 0x00106270 File Offset: 0x00104470
		public void Stop(bool bForce = false)
		{
			if (this.m_bEndAnim && this.m_Animator != null && this.m_AnimName.Length > 1 && this.m_Anim2D.GetAnimName() != "END")
			{
				this.m_Anim2D.Play("END", false, 0f);
			}
			if (this.m_bEndAnim && this.m_SPINE_SkeletonAnimation != null && this.m_AnimName.Length > 1 && this.m_AnimSpine.GetAnimName() != "END")
			{
				this.m_AnimSpine.Play("END", false, 0f);
			}
			if (this.m_ParticleSystems != null)
			{
				for (int i = 0; i < this.m_ParticleSystems.Length; i++)
				{
					this.m_ParticleSystems[i].Stop(true, ParticleSystemStopBehavior.StopEmitting);
				}
			}
			if (bForce)
			{
				this.SetDie();
			}
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x00106354 File Offset: 0x00104554
		private void StopParticleEmit()
		{
			for (int i = 0; i < this.m_ParticleSystems.Length; i++)
			{
				this.m_ParticleSystems[i].Stop(true, ParticleSystemStopBehavior.StopEmitting);
			}
		}

		// Token: 0x06003419 RID: 13337 RVA: 0x00106383 File Offset: 0x00104583
		public bool IsEnd()
		{
			return this.m_bDie;
		}

		// Token: 0x0600341A RID: 13338 RVA: 0x0010638C File Offset: 0x0010458C
		public void PlayAnim(string animName, bool bLoop = false, float fAnimSpeed = 1f)
		{
			this.m_fAnimSpeed = fAnimSpeed;
			float playSpeed = this.MakeAnimSpeed();
			this.m_bPlayed = true;
			if (this.m_Animator != null)
			{
				this.m_AnimName = animName;
				this.m_Anim2D.SetPlaySpeed(playSpeed);
				this.m_Anim2D.Play(this.m_AnimName, bLoop, 0f);
			}
			if (this.m_SPINE_SkeletonAnimation != null)
			{
				this.m_AnimName = animName;
				this.m_AnimSpine.SetPlaySpeed(playSpeed);
				this.m_AnimSpine.Play(this.m_AnimName, bLoop, 0f);
				return;
			}
			if (this.m_SPINE_SkeletonGraphic != null)
			{
				this.m_AnimName = animName;
				this.m_AnimSpine.SetPlaySpeed(playSpeed);
				this.m_AnimSpine.Play(this.m_AnimName, bLoop, 0f);
			}
		}

		// Token: 0x0600341B RID: 13339 RVA: 0x00106458 File Offset: 0x00104658
		public float MakeAnimSpeed()
		{
			float num = this.m_fAnimSpeed;
			if (this.m_UseMasterAnimSpeed && this.GetMasterUnit() != null)
			{
				num *= this.GetMasterUnit().GetUnitFrameData().m_fAnimSpeed;
			}
			return num;
		}

		// Token: 0x0600341C RID: 13340 RVA: 0x00106490 File Offset: 0x00104690
		public void SetRight(bool bRight)
		{
			this.m_bRight = bRight;
			if (this.m_EffectInstant == null || this.m_EffectInstant.m_Instant == null)
			{
				return;
			}
			if (!this.m_bUseBoneRotate)
			{
				if (this.m_bRight)
				{
					this.m_TempVec3 = this.m_EffectInstant.m_Instant.transform.localEulerAngles;
					this.m_TempVec3.y = this.m_RotateOrg.y;
					this.m_EffectInstant.m_Instant.transform.localEulerAngles = this.m_TempVec3;
					return;
				}
				this.m_TempVec3 = this.m_EffectInstant.m_Instant.transform.localEulerAngles;
				this.m_TempVec3.y = this.m_RotateOrg.y + 180f;
				this.m_EffectInstant.m_Instant.transform.localEulerAngles = this.m_TempVec3;
			}
		}

		// Token: 0x0600341D RID: 13341 RVA: 0x00106574 File Offset: 0x00104774
		public void SetLookDir(float fAngle)
		{
			if (this.m_EffectInstant == null || this.m_EffectInstant.m_Instant == null)
			{
				return;
			}
			this.m_TempVec3.x = 0f;
			this.m_TempVec3.y = 0f;
			this.m_TempVec3.z = fAngle;
			this.m_EffectInstant.m_Instant.transform.localEulerAngles = this.m_TempVec3;
		}

		// Token: 0x0600341E RID: 13342 RVA: 0x001065E4 File Offset: 0x001047E4
		public void SetRotate(float fAngle)
		{
			if (this.m_EffectInstant == null || this.m_EffectInstant.m_Instant == null)
			{
				return;
			}
			this.m_TempVec3 = this.m_EffectInstant.m_Instant.transform.localEulerAngles;
			this.m_TempVec3.x = 0f;
			this.m_TempVec3.z = fAngle;
			this.m_EffectInstant.m_Instant.transform.localEulerAngles = this.m_TempVec3;
		}

		// Token: 0x0600341F RID: 13343 RVA: 0x00106660 File Offset: 0x00104860
		public void SetPos(float fX, float fY, float fZ)
		{
			if (this.m_EffectInstant == null || this.m_EffectInstant.m_Instant == null)
			{
				return;
			}
			if (this.m_bRight || this.m_bUseBoneRotate)
			{
				if (this.m_RectTransform == null)
				{
					NKCUtil.SetGameObjectLocalPos(this.m_EffectInstant.m_Instant, fX + this.m_OffsetX, fY + this.m_OffsetY, fZ + this.m_OffsetZ);
					return;
				}
				Vector2 anchoredPosition = this.m_RectTransform.anchoredPosition;
				anchoredPosition.Set(fX + this.m_OffsetX, fY + this.m_OffsetY);
				this.m_RectTransform.anchoredPosition = anchoredPosition;
				return;
			}
			else
			{
				if (this.m_RectTransform == null)
				{
					NKCUtil.SetGameObjectLocalPos(this.m_EffectInstant.m_Instant, fX - this.m_OffsetX, fY + this.m_OffsetY, fZ + this.m_OffsetZ);
					return;
				}
				Vector2 anchoredPosition2 = this.m_RectTransform.anchoredPosition;
				anchoredPosition2.Set(fX - this.m_OffsetX, fY + this.m_OffsetY);
				this.m_RectTransform.anchoredPosition = anchoredPosition2;
				return;
			}
		}

		// Token: 0x06003420 RID: 13344 RVA: 0x00106766 File Offset: 0x00104966
		public void SetScale(float fX, float fY, float fZ)
		{
			this.m_fScaleX = fX;
			this.m_fScaleY = fY;
			this.m_fScaleZ = fZ;
			this.m_bScaleChange = true;
		}

		// Token: 0x06003421 RID: 13345 RVA: 0x00106784 File Offset: 0x00104984
		public void SetScaleFactor(float fX, float fY, float fZ)
		{
			this.m_fScaleFactorX = fX;
			this.m_fScaleFactorY = fY;
			this.m_fScaleFactorZ = fZ;
			this.m_bScaleChange = true;
		}

		// Token: 0x06003422 RID: 13346 RVA: 0x001067A2 File Offset: 0x001049A2
		public void SetGuageRoot(bool value)
		{
			this.m_bUseGuageAsRoot = value;
		}

		// Token: 0x06003423 RID: 13347 RVA: 0x001067AC File Offset: 0x001049AC
		public void SetDissolveOn(bool bOn)
		{
			if (bOn && this.m_DissolveMaterial != null)
			{
				this.m_MeshRenderer.sharedMaterial = this.m_DissolveMaterial;
				return;
			}
			if (this.m_Material != null)
			{
				this.m_MeshRenderer.sharedMaterial = this.m_Material;
			}
		}

		// Token: 0x06003424 RID: 13348 RVA: 0x001067FB File Offset: 0x001049FB
		public void SetDissolveBlend(float fBlend)
		{
			if (this.m_DissolveMaterial == null)
			{
				return;
			}
			this.m_DissolveMaterial.SetFloat("_DissolveBlend", fBlend);
		}

		// Token: 0x06003425 RID: 13349 RVA: 0x00106820 File Offset: 0x00104A20
		public void SetDissolveColor(Color color)
		{
			if (this.m_DissolveMaterial == null)
			{
				return;
			}
			if (color.r == -1f)
			{
				color.r = this.m_DissolveColorOrg.r;
			}
			if (color.g == -1f)
			{
				color.g = this.m_DissolveColorOrg.g;
			}
			if (color.b == -1f)
			{
				color.b = this.m_DissolveColorOrg.b;
			}
			if (color.a == -1f)
			{
				color.a = this.m_DissolveColorOrg.a;
			}
			this.m_DissolveMaterial.SetColor("_DissolveGlowColor", color);
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x001068CC File Offset: 0x00104ACC
		public void DamageTextInit()
		{
			if (this.m_EffectInstant == null || this.m_EffectInstant.m_Instant == null)
			{
				return;
			}
			if (this.m_DamageText == null)
			{
				Transform transform = this.m_EffectInstant.m_Instant.transform.Find("AB_FX_DAMAGE_TEXT_Text");
				if (transform != null)
				{
					GameObject gameObject = transform.gameObject;
					if (gameObject != null)
					{
						this.m_DamageText = gameObject.GetComponent<Text>();
					}
				}
			}
			if (this.m_DamageTextCritical == null)
			{
				Transform transform2 = this.m_EffectInstant.m_Instant.transform.Find("AB_FX_DAMAGE_TEXT_Text/AB_FX_DAMAGE_TEXT_CRITICAL");
				if (transform2 != null)
				{
					GameObject gameObject2 = transform2.gameObject;
					if (gameObject2 != null)
					{
						this.m_DamageTextCritical = gameObject2.GetComponent<Text>();
					}
				}
			}
		}

		// Token: 0x06003427 RID: 13351 RVA: 0x00106994 File Offset: 0x00104B94
		public void BuffTextInit(byte buffDescTextPosYIndex)
		{
			if (this.m_BuffText == null)
			{
				if (this.m_EffectInstant == null || this.m_EffectInstant.m_Instant == null)
				{
					return;
				}
				Transform transform = this.m_EffectInstant.m_Instant.transform.Find("AB_FX_BUFF_TEXT_Text");
				if (transform != null)
				{
					GameObject gameObject = transform.gameObject;
					if (gameObject != null)
					{
						this.m_BuffText = gameObject.GetComponent<Text>();
					}
				}
			}
			this.m_BuffDescTextPosYIndex = buffDescTextPosYIndex;
		}

		// Token: 0x06003428 RID: 13352 RVA: 0x00106A14 File Offset: 0x00104C14
		public void Init_AB_FX_COST()
		{
			if (this.m_AB_FX_COST_COUNT_Text == null)
			{
				if (this.m_EffectInstant == null || this.m_EffectInstant.m_Instant == null)
				{
					return;
				}
				Transform transform = this.m_EffectInstant.m_Instant.transform.Find("AB_FX_COST_CONTENT/AB_FX_COST_COUNT");
				if (transform != null)
				{
					GameObject gameObject = transform.gameObject;
					if (gameObject != null)
					{
						this.m_AB_FX_COST_COUNT_Text = gameObject.GetComponent<Text>();
					}
				}
			}
		}

		// Token: 0x06003429 RID: 13353 RVA: 0x00106A8C File Offset: 0x00104C8C
		public void Init_AB_FX_COOLTIME()
		{
			if (this.m_NKM_UI_HUD_COOLTIME_COUNT_Text == null)
			{
				if (this.m_EffectInstant == null || this.m_EffectInstant.m_Instant == null)
				{
					return;
				}
				Transform transform = this.m_EffectInstant.m_Instant.transform.Find("NKM_UI_COOLTIME_CONTENT/NKM_UI_HUD_COOLTIME_COUNT");
				if (transform != null)
				{
					GameObject gameObject = transform.gameObject;
					if (gameObject != null)
					{
						this.m_NKM_UI_HUD_COOLTIME_COUNT_Text = gameObject.GetComponent<Text>();
					}
				}
			}
		}

		// Token: 0x0600342A RID: 13354 RVA: 0x00106B04 File Offset: 0x00104D04
		public void Init_AB_FX_SKILL_CUTIN_COMMON_DESC()
		{
			if (this.m_AB_FX_SKILL_CUTIN_COMMON_DESC_UNIT_NAME == null)
			{
				if (this.m_EffectInstant == null || this.m_EffectInstant.m_Instant == null)
				{
					return;
				}
				Transform transform = this.m_EffectInstant.m_Instant.transform.Find("DESC/POS_TEXT_CHA_NAME/OFFSET_TEXT_CHA_NAME/TEXT_CHA_NAME");
				if (transform != null)
				{
					GameObject gameObject = transform.gameObject;
					if (gameObject != null)
					{
						this.m_AB_FX_SKILL_CUTIN_COMMON_DESC_UNIT_NAME = gameObject.GetComponent<Text>();
						this.m_AB_FX_SKILL_CUTIN_COMMON_DESC_UNIT_NAME_RectTransform = gameObject.GetComponent<RectTransform>();
					}
				}
				transform = this.m_EffectInstant.m_Instant.transform.Find("DESC/POS_TEXT_SPELL_NAME/OFFSET_TEXT_SPELL_NAME/TEXT_SPELL_NAME");
				if (transform != null)
				{
					GameObject gameObject2 = transform.gameObject;
					if (gameObject2 != null)
					{
						this.m_AB_FX_SKILL_CUTIN_COMMON_DESC_SKILL_NAME = gameObject2.GetComponent<Text>();
						this.m_AB_FX_SKILL_CUTIN_COMMON_DESC_SKILL_NAME_RectTransform = gameObject2.GetComponent<RectTransform>();
					}
				}
			}
		}

		// Token: 0x0400325B RID: 12891
		public int m_EffectUID;

		// Token: 0x0400325C RID: 12892
		public NKM_EFFECT_PARENT_TYPE m_NKM_EFFECT_PARENT_TYPE;

		// Token: 0x0400325D RID: 12893
		public NKCAssetInstanceData m_EffectInstant;

		// Token: 0x0400325E RID: 12894
		public RectTransform m_RectTransform;

		// Token: 0x0400325F RID: 12895
		public Camera[] m_Cameras;

		// Token: 0x04003260 RID: 12896
		public SpriteRenderer[] m_SpriteRenderer;

		// Token: 0x04003261 RID: 12897
		public ParticleSystemRenderer[] m_ParticleSystemRenderer;

		// Token: 0x04003262 RID: 12898
		public bool m_bPlayed;

		// Token: 0x04003263 RID: 12899
		public Animator m_Animator;

		// Token: 0x04003264 RID: 12900
		public ParticleSystem[] m_ParticleSystems;

		// Token: 0x04003265 RID: 12901
		public bool m_bSpine;

		// Token: 0x04003266 RID: 12902
		public bool m_bSpineUI;

		// Token: 0x04003267 RID: 12903
		public string m_AnimName;

		// Token: 0x04003268 RID: 12904
		public float m_fAnimSpeed;

		// Token: 0x04003269 RID: 12905
		public NKCAnim2D m_Anim2D = new NKCAnim2D();

		// Token: 0x0400326A RID: 12906
		public GameObject m_SPINE_SkeletonAnimationOrg;

		// Token: 0x0400326B RID: 12907
		public MeshRenderer m_MeshRendererOrg;

		// Token: 0x0400326C RID: 12908
		public GameObject m_SPINE_SkeletonAnimation;

		// Token: 0x0400326D RID: 12909
		public GameObject m_SPINE_SkeletonGraphic;

		// Token: 0x0400326E RID: 12910
		public MeshRenderer m_MeshRenderer;

		// Token: 0x0400326F RID: 12911
		public NKCAnimSpine m_AnimSpine = new NKCAnimSpine();

		// Token: 0x04003270 RID: 12912
		private Material m_Material;

		// Token: 0x04003271 RID: 12913
		private Material m_DissolveMaterial;

		// Token: 0x04003272 RID: 12914
		private Color m_DissolveColorOrg;

		// Token: 0x04003273 RID: 12915
		private Vector3 m_RotateOrg;

		// Token: 0x04003274 RID: 12916
		public bool m_bDEEffect;

		// Token: 0x04003275 RID: 12917
		private bool m_bDie;

		// Token: 0x04003276 RID: 12918
		public bool m_bEndAnim;

		// Token: 0x04003277 RID: 12919
		public float m_fReserveDieTime;

		// Token: 0x04003278 RID: 12920
		public bool m_bAutoDie;

		// Token: 0x04003279 RID: 12921
		public bool m_bRight;

		// Token: 0x0400327A RID: 12922
		public float m_OffsetX;

		// Token: 0x0400327B RID: 12923
		public float m_OffsetY;

		// Token: 0x0400327C RID: 12924
		public float m_OffsetZ;

		// Token: 0x0400327D RID: 12925
		public float m_fAddRotate;

		// Token: 0x0400327E RID: 12926
		public float m_fScaleX;

		// Token: 0x0400327F RID: 12927
		public float m_fScaleY;

		// Token: 0x04003280 RID: 12928
		public float m_fScaleZ;

		// Token: 0x04003281 RID: 12929
		public float m_fScaleFactorX;

		// Token: 0x04003282 RID: 12930
		public float m_fScaleFactorY;

		// Token: 0x04003283 RID: 12931
		public float m_fScaleFactorZ;

		// Token: 0x04003284 RID: 12932
		public bool m_bUseZScale;

		// Token: 0x04003285 RID: 12933
		public bool m_bScaleChange;

		// Token: 0x04003286 RID: 12934
		public string m_BoneName;

		// Token: 0x04003287 RID: 12935
		public bool m_bUseBoneRotate;

		// Token: 0x04003288 RID: 12936
		public bool m_bStateEndStop;

		// Token: 0x04003289 RID: 12937
		public bool m_bStateEndStopForce;

		// Token: 0x0400328A RID: 12938
		public bool m_bCutIn;

		// Token: 0x0400328B RID: 12939
		public bool m_bUseGuageAsRoot;

		// Token: 0x0400328C RID: 12940
		public bool m_UseMasterAnimSpeed;

		// Token: 0x0400328D RID: 12941
		public short m_MasterUnitGameUID;

		// Token: 0x0400328E RID: 12942
		private NKMUnit m_MasterUnit;

		// Token: 0x0400328F RID: 12943
		private bool m_canIgnoreStopTime;

		// Token: 0x04003290 RID: 12944
		public bool m_applyStopTime;

		// Token: 0x04003291 RID: 12945
		public float m_fAnimSpeedFinal;

		// Token: 0x04003292 RID: 12946
		public Vector3 m_TempVec3;

		// Token: 0x04003293 RID: 12947
		private NKC_FXM_PLAYER[] m_NKC_FXM_PLAYERs;

		// Token: 0x04003294 RID: 12948
		private float[] m_NKC_FXM_PLAYER_TimeScaleOrg;

		// Token: 0x04003295 RID: 12949
		private NKC_FX_DELAY_EXECUTER[] m_NKC_FX_DELAY_EXECUTERs;

		// Token: 0x04003296 RID: 12950
		private float[] m_NKC_FX_DELAY_EXECUTER_TimeScaleOrg;

		// Token: 0x04003297 RID: 12951
		private NKM_GAME_SPEED_TYPE m_NKM_GAME_SPEED_TYPE;

		// Token: 0x04003298 RID: 12952
		private float[] m_ParticleSystem_SimulationSpeedOrg;

		// Token: 0x04003299 RID: 12953
		public Text m_BuffText;

		// Token: 0x0400329A RID: 12954
		public byte m_BuffDescTextPosYIndex;

		// Token: 0x0400329B RID: 12955
		public Text m_DamageText;

		// Token: 0x0400329C RID: 12956
		public Text m_DamageTextCritical;

		// Token: 0x0400329D RID: 12957
		public Text m_AB_FX_COST_COUNT_Text;

		// Token: 0x0400329E RID: 12958
		public Text m_NKM_UI_HUD_COOLTIME_COUNT_Text;

		// Token: 0x0400329F RID: 12959
		public Text m_AB_FX_SKILL_CUTIN_COMMON_DESC_UNIT_NAME;

		// Token: 0x040032A0 RID: 12960
		public Text m_AB_FX_SKILL_CUTIN_COMMON_DESC_SKILL_NAME;

		// Token: 0x040032A1 RID: 12961
		public RectTransform m_AB_FX_SKILL_CUTIN_COMMON_DESC_UNIT_NAME_RectTransform;

		// Token: 0x040032A2 RID: 12962
		public RectTransform m_AB_FX_SKILL_CUTIN_COMMON_DESC_SKILL_NAME_RectTransform;
	}
}
