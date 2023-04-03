using System;
using NKC.FX;
using NKC.UI;
using NKM;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200062F RID: 1583
	public class NKCASUISpineIllust : NKCASUIUnitIllust
	{
		// Token: 0x060030F2 RID: 12530 RVA: 0x000F2A1C File Offset: 0x000F0C1C
		public NKCASUISpineIllust(string bundleName, string name, bool bAsync = false)
		{
			this.m_NKM_OBJECT_POOL_TYPE = NKM_OBJECT_POOL_TYPE.NOPT_NKCASUISpineIllust;
			this.m_ObjectPoolBundleName = bundleName;
			this.m_ObjectPoolName = name;
			this.m_bUnloadable = true;
			this.Load(bAsync);
		}

		// Token: 0x060030F3 RID: 12531 RVA: 0x000F2A7F File Offset: 0x000F0C7F
		public override void Load(bool bAsync)
		{
			this.m_SpineIllustInstant = NKCAssetResourceManager.OpenInstance<GameObject>(this.m_ObjectPoolBundleName, this.m_ObjectPoolName, bAsync, null);
		}

		// Token: 0x060030F4 RID: 12532 RVA: 0x000F2A9C File Offset: 0x000F0C9C
		public override bool LoadComplete()
		{
			if (this.m_SpineIllustInstant == null || this.m_SpineIllustInstant.m_Instant == null)
			{
				return false;
			}
			this.m_SpineIllustInstant_RectTransform = this.m_SpineIllustInstant.m_Instant.GetComponentInChildren<RectTransform>();
			SkeletonGraphic[] componentsInChildren = this.m_SpineIllustInstant.m_Instant.GetComponentsInChildren<SkeletonGraphic>(true);
			if (componentsInChildren.Length == 1)
			{
				this.m_SpineIllustInstant_SkeletonGraphic = componentsInChildren[0];
			}
			else if (componentsInChildren.Length > 1)
			{
				foreach (SkeletonGraphic skeletonGraphic in componentsInChildren)
				{
					if (string.Compare(skeletonGraphic.gameObject.name, "SPINE_SkeletonGraphic", true) == 0)
					{
						this.m_SpineIllustInstant_SkeletonGraphic = skeletonGraphic;
					}
					if (string.Compare(skeletonGraphic.gameObject.name, "SPINE_BG", true) == 0)
					{
						this.m_SpineIllustBG = skeletonGraphic;
					}
				}
				if (this.m_SpineIllustInstant_SkeletonGraphic == null)
				{
					Debug.LogError("복수개의 Spine 오브젝트가 존재하나 SPINE_SkeletonGraphic가 존재하지 않음. 가장 앞의 오브젝트를 메인 오브젝트로 사용함");
					this.m_SpineIllustInstant_SkeletonGraphic = componentsInChildren[componentsInChildren.Length - 1];
				}
			}
			else
			{
				this.m_SpineIllustInstant_SkeletonGraphic = null;
			}
			if (this.m_SpineIllustInstant_SkeletonGraphic != null)
			{
				this.m_ColorOrg = this.m_SpineIllustInstant_SkeletonGraphic.color;
				this.m_matDefault = this.m_SpineIllustInstant_SkeletonGraphic.material;
			}
			if (this.m_SpineIllustBG != null)
			{
				this.m_ColorOrgBG = this.m_SpineIllustBG.color;
				this.m_matDefaultBG = this.m_SpineIllustBG.material;
			}
			this.m_NKCComSpineSkeletonAnimationEvent = this.m_SpineIllustInstant.m_Instant.GetComponentInChildren<NKCComSpineSkeletonAnimationEvent>();
			this.m_NKCFxSpineIllust = this.m_SpineIllustInstant.m_Instant.GetComponentInChildren<NKC_FX_SPINE_ILLUST>();
			if (this.m_NKCComSpineSkeletonAnimationEvent != null && this.m_NKCComSpineSkeletonAnimationEvent.m_EFFECT_ROOT != null)
			{
				this.m_NKCFxSpineEventState = this.m_NKCComSpineSkeletonAnimationEvent.m_EFFECT_ROOT.GetComponent<NKC_FX_SPINE_EVENT_STATE>();
			}
			this.m_vfx_back = this.m_SpineIllustInstant.m_Instant.transform.Find("VFX_BACK");
			this.m_vfx_front = this.m_SpineIllustInstant.m_Instant.transform.Find("VFX_FRONT");
			this.m_trTalk_L = this.m_SpineIllustInstant.m_Instant.transform.Find("Root_Speach_Bubble_L");
			this.m_trTalk_R = this.m_SpineIllustInstant.m_Instant.transform.Find("Root_Speach_Bubble_R");
			return true;
		}

		// Token: 0x060030F5 RID: 12533 RVA: 0x000F2CCC File Offset: 0x000F0ECC
		public override void Open()
		{
			if (this.m_SpineIllustInstant == null)
			{
				return;
			}
			this.SetDefaultMaterial();
			if (this.m_vfx_back != null)
			{
				this.m_vfx_back.gameObject.SetActive(true);
			}
			if (this.m_vfx_front != null)
			{
				this.m_vfx_front.gameObject.SetActive(true);
			}
			if (!this.m_SpineIllustInstant.m_Instant.activeSelf)
			{
				this.m_SpineIllustInstant.m_Instant.SetActive(true);
			}
			this.SetColor(1f, 1f, 1f, 1f);
		}

		// Token: 0x060030F6 RID: 12534 RVA: 0x000F2D63 File Offset: 0x000F0F63
		public override void Close()
		{
			if (this.m_SpineIllustInstant == null)
			{
				return;
			}
			this.m_SpineIllustInstant.Close();
			base.Close();
		}

		// Token: 0x060030F7 RID: 12535 RVA: 0x000F2D7F File Offset: 0x000F0F7F
		public override void SetMaterial(Material mat)
		{
			if (this.m_SpineIllustInstant_SkeletonGraphic != null)
			{
				this.m_SpineIllustInstant_SkeletonGraphic.material = mat;
			}
			if (this.m_SpineIllustBG != null)
			{
				this.m_SpineIllustBG.material = mat;
			}
		}

		// Token: 0x060030F8 RID: 12536 RVA: 0x000F2DB5 File Offset: 0x000F0FB5
		public override void SetDefaultMaterial()
		{
			if (this.m_SpineIllustInstant_SkeletonGraphic != null)
			{
				this.m_SpineIllustInstant_SkeletonGraphic.material = this.m_matDefault;
			}
			if (this.m_SpineIllustBG != null)
			{
				this.m_SpineIllustBG.material = this.m_matDefaultBG;
			}
		}

		// Token: 0x060030F9 RID: 12537 RVA: 0x000F2DF8 File Offset: 0x000F0FF8
		protected override NKCASMaterial MakeEffectMaterial(NKCUICharacterView.EffectType effect)
		{
			base.UnloadEffectMaterial();
			switch (effect)
			{
			case NKCUICharacterView.EffectType.Hologram:
			case NKCUICharacterView.EffectType.HologramClose:
			case NKCUICharacterView.EffectType.Gray:
				return (NKCASMaterial)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASMaterial, "ab_material", "MAT_NKC_SPINE_GRAYSCALE", false);
			case NKCUICharacterView.EffectType.VersusMaskL:
				return (NKCASMaterial)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASMaterial, "ab_material", "MAT_NKC_SPINE_VERSUS_MASK_L", false);
			case NKCUICharacterView.EffectType.VersusMaskR:
				return (NKCASMaterial)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASMaterial, "ab_material", "MAT_NKC_SPINE_VERSUS_MASK_R", false);
			case NKCUICharacterView.EffectType.TwopassTransparency:
				return (NKCASMaterial)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASMaterial, "shaders", "SkeletonGraphic2Pass", false);
			default:
				return null;
			}
		}

		// Token: 0x060030FA RID: 12538 RVA: 0x000F2EB5 File Offset: 0x000F10B5
		protected override void ProcessEffect(NKCUICharacterView.EffectType effect)
		{
			if (effect != NKCUICharacterView.EffectType.TwopassTransparency)
			{
				this.SetZSpacing(0f);
				return;
			}
			this.SetZSpacing(-0.0001f);
		}

		// Token: 0x060030FB RID: 12539 RVA: 0x000F2ED4 File Offset: 0x000F10D4
		private void SetZSpacing(float value)
		{
			if (this.m_SpineIllustInstant_SkeletonGraphic != null)
			{
				this.m_SpineIllustInstant_SkeletonGraphic.MeshGenerator.settings.zSpacing = value;
			}
			if (this.m_SpineIllustBG != null)
			{
				this.m_SpineIllustBG.MeshGenerator.settings.zSpacing = value;
			}
		}

		// Token: 0x060030FC RID: 12540 RVA: 0x000F2F2C File Offset: 0x000F112C
		public override void Unload()
		{
			if (this.m_SpineIllustInstant == null)
			{
				return;
			}
			NKCAssetResourceManager.CloseInstance(this.m_SpineIllustInstant);
			this.m_SpineIllustInstant = null;
			this.m_SpineIllustInstant_RectTransform = null;
			this.m_SpineIllustInstant_SkeletonGraphic = null;
			this.m_SpineIllustBG = null;
			this.m_NKCComSpineSkeletonAnimationEvent = null;
			this.m_NKCFxSpineIllust = null;
			this.m_NKCFxSpineEventState = null;
			this.m_matDefault = null;
			this.m_matDefaultBG = null;
			this.m_vfx_back = null;
			this.m_vfx_front = null;
			base.Unload();
		}

		// Token: 0x060030FD RID: 12541 RVA: 0x000F2FA0 File Offset: 0x000F11A0
		public override Color GetColor()
		{
			if (this.m_SpineIllustInstant_SkeletonGraphic == null)
			{
				return Color.white;
			}
			return this.m_SpineIllustInstant_SkeletonGraphic.color;
		}

		// Token: 0x060030FE RID: 12542 RVA: 0x000F2FC1 File Offset: 0x000F11C1
		public override void SetColor(Color color)
		{
			this.SetColor(color.r, color.g, color.b, color.a);
		}

		// Token: 0x060030FF RID: 12543 RVA: 0x000F2FE4 File Offset: 0x000F11E4
		public override void SetColor(float fR = -1f, float fG = -1f, float fB = -1f, float fA = -1f)
		{
			bool flag = false;
			if (fR != -1f && this.m_fR != fR)
			{
				this.m_fR = fR;
				flag = true;
			}
			if (fG != -1f && this.m_fG != fG)
			{
				this.m_fG = fG;
				flag = true;
			}
			if (fB != -1f && this.m_fB != fB)
			{
				this.m_fB = fB;
				flag = true;
			}
			if (fA != -1f && this.m_fA != fA)
			{
				this.m_fA = fA;
				flag = true;
			}
			if (flag)
			{
				if (this.m_SpineIllustInstant_SkeletonGraphic != null)
				{
					this.m_ColorTemp.r = this.m_ColorOrg.r * this.m_fR;
					this.m_ColorTemp.g = this.m_ColorOrg.g * this.m_fG;
					this.m_ColorTemp.b = this.m_ColorOrg.b * this.m_fB;
					this.m_ColorTemp.a = this.m_ColorOrg.a * this.m_fA;
					this.m_SpineIllustInstant_SkeletonGraphic.color = this.m_ColorTemp;
				}
				if (this.m_SpineIllustBG != null)
				{
					this.m_ColorTemp.r = this.m_ColorOrgBG.r * this.m_fR;
					this.m_ColorTemp.g = this.m_ColorOrgBG.g * this.m_fG;
					this.m_ColorTemp.b = this.m_ColorOrgBG.b * this.m_fB;
					this.m_ColorTemp.a = this.m_ColorOrgBG.a * this.m_fA;
					this.m_SpineIllustBG.color = this.m_ColorTemp;
				}
			}
		}

		// Token: 0x06003100 RID: 12544 RVA: 0x000F3190 File Offset: 0x000F1390
		public override void SetParent(Transform parent, bool worldPositionStays)
		{
			if (this.m_SpineIllustInstant != null)
			{
				this.m_SpineIllustInstant.m_Instant.transform.SetParent(parent, worldPositionStays);
			}
		}

		// Token: 0x06003101 RID: 12545 RVA: 0x000F31B4 File Offset: 0x000F13B4
		public override void SetAnimation(string AnimationName, bool loop, int trackIndex = 0, bool bForceRestart = true, float fStartTime = 0f, bool bReturnDefault = true)
		{
			if (this.m_SpineIllustInstant_SkeletonGraphic != null && this.m_SpineIllustInstant_SkeletonGraphic.AnimationState != null)
			{
				if (this.m_SpineIllustInstant_SkeletonGraphic.SkeletonData == null || this.m_SpineIllustInstant_SkeletonGraphic.SkeletonData.FindAnimation(AnimationName) == null)
				{
					Debug.LogError("Animation name " + AnimationName + " does not exist!");
					return;
				}
				this.m_SpineIllustInstant_SkeletonGraphic.SetUseHalfUpdate(false);
				if (bForceRestart)
				{
					if (this.m_NKCComSpineSkeletonAnimationEvent != null)
					{
						this.m_NKCComSpineSkeletonAnimationEvent.AddEvent(true);
					}
					Skeleton skeleton = this.m_SpineIllustInstant_SkeletonGraphic.Skeleton;
					if (skeleton != null)
					{
						skeleton.SetToSetupPose();
					}
					TrackEntry trackEntry = this.m_SpineIllustInstant_SkeletonGraphic.AnimationState.SetAnimation(trackIndex, AnimationName, loop);
					if (fStartTime > 0f)
					{
						trackEntry.TrackTime = fStartTime;
					}
				}
				else
				{
					TrackEntry current = this.m_SpineIllustInstant_SkeletonGraphic.AnimationState.GetCurrent(trackIndex);
					if (current == null || (current != null && current.Animation.Name != AnimationName))
					{
						Skeleton skeleton2 = this.m_SpineIllustInstant_SkeletonGraphic.Skeleton;
						if (skeleton2 != null)
						{
							skeleton2.SetToSetupPose();
						}
						TrackEntry trackEntry2 = this.m_SpineIllustInstant_SkeletonGraphic.AnimationState.SetAnimation(trackIndex, AnimationName, loop);
						if (fStartTime > 0f)
						{
							trackEntry2.TrackTime = fStartTime;
						}
					}
				}
				this.ForceUpdateAnimation();
				if (!loop && bReturnDefault)
				{
					this.AddAnimation(this.m_eDefaultAnimation, true, 0f, 0);
				}
			}
		}

		// Token: 0x06003102 RID: 12546 RVA: 0x000F3308 File Offset: 0x000F1508
		public override void SetAnimation(NKCASUIUnitIllust.eAnimation eAnim, bool loop, int trackIndex = 0, bool bForceRestart = true, float fStartTime = 0f, bool bReturnDefault = true)
		{
			string animationName = NKCASUIUnitIllust.GetAnimationName(eAnim);
			this.SetAnimation(animationName, loop, trackIndex, bForceRestart, fStartTime, bReturnDefault);
			if (this.m_SpineIllustInstant_SkeletonGraphic != null)
			{
				this.m_SpineIllustInstant_SkeletonGraphic.SetUseHalfUpdate(this.IsHalfFrameAnim(eAnim));
			}
			if (this.m_SpineIllustBG != null)
			{
				this.m_SpineIllustBG.SetUseHalfUpdate(this.IsHalfFrameAnim(eAnim));
			}
		}

		// Token: 0x06003103 RID: 12547 RVA: 0x000F336C File Offset: 0x000F156C
		public override bool HasAnimation(NKCASUIUnitIllust.eAnimation eAnim)
		{
			string animationName = NKCASUIUnitIllust.GetAnimationName(eAnim);
			return this.HasAnimation(animationName);
		}

		// Token: 0x06003104 RID: 12548 RVA: 0x000F3387 File Offset: 0x000F1587
		public override bool HasAnimation(string name)
		{
			return !(this.m_SpineIllustInstant_SkeletonGraphic == null) && this.m_SpineIllustInstant_SkeletonGraphic.SkeletonData != null && this.m_SpineIllustInstant_SkeletonGraphic.SkeletonData.FindAnimation(name) != null;
		}

		// Token: 0x06003105 RID: 12549 RVA: 0x000F33BC File Offset: 0x000F15BC
		public override void SetIllustBackgroundEnable(bool bValue)
		{
			this.SetSkin(bValue ? "default" : "ONLY_UNIT");
			if (this.m_NKCFxSpineIllust != null)
			{
				this.m_NKCFxSpineIllust.EnableExceptionalVfx(bValue);
			}
		}

		// Token: 0x06003106 RID: 12550 RVA: 0x000F33ED File Offset: 0x000F15ED
		public override void SetSkin(string skinName)
		{
			this.SetSkin(this.m_SpineIllustInstant_SkeletonGraphic, skinName);
			this.SetSkin(this.m_SpineIllustBG, skinName);
		}

		// Token: 0x06003107 RID: 12551 RVA: 0x000F3409 File Offset: 0x000F1609
		public override bool HasSkin(string skinName)
		{
			return this.HasSkin(this.m_SpineIllustInstant_SkeletonGraphic, skinName) || this.HasSkin(this.m_SpineIllustBG, skinName);
		}

		// Token: 0x06003108 RID: 12552 RVA: 0x000F3429 File Offset: 0x000F1629
		private bool HasSkin(SkeletonGraphic targetSkeleton, string skinName)
		{
			return !(targetSkeleton == null) && targetSkeleton.SkeletonData != null && targetSkeleton.SkeletonData.FindSkin(skinName) != null;
		}

		// Token: 0x06003109 RID: 12553 RVA: 0x000F3450 File Offset: 0x000F1650
		public override int GetSkinOptionCount()
		{
			int num = 0;
			for (;;)
			{
				string skinName = string.Format("SKIN_{0}", num + 1);
				if (!this.HasSkin(skinName))
				{
					break;
				}
				num++;
			}
			return num;
		}

		// Token: 0x0600310A RID: 12554 RVA: 0x000F3484 File Offset: 0x000F1684
		public override void SetSkinOption(int index)
		{
			string skin = string.Format("SKIN_{0}", index + 1);
			this.SetSkin(skin);
		}

		// Token: 0x0600310B RID: 12555 RVA: 0x000F34AC File Offset: 0x000F16AC
		private void SetSkin(SkeletonGraphic targetSkeleton, string skinName)
		{
			if (targetSkeleton == null || targetSkeleton.SkeletonData == null)
			{
				return;
			}
			Skin skin = targetSkeleton.SkeletonData.FindSkin(skinName);
			if (skin == null)
			{
				return;
			}
			targetSkeleton.Skeleton.SetSkin(skin);
			targetSkeleton.Skeleton.SetSlotsToSetupPose();
			if (targetSkeleton.AnimationState != null)
			{
				targetSkeleton.AnimationState.Apply(targetSkeleton.Skeleton);
			}
		}

		// Token: 0x0600310C RID: 12556 RVA: 0x000F350D File Offset: 0x000F170D
		public override void ForceUpdateAnimation()
		{
			if (this.m_SpineIllustInstant_SkeletonGraphic != null)
			{
				this.m_SpineIllustInstant_SkeletonGraphic.Update();
			}
		}

		// Token: 0x0600310D RID: 12557 RVA: 0x000F3528 File Offset: 0x000F1728
		public override void InitializeAnimation()
		{
			if (this.m_SpineIllustInstant_SkeletonGraphic != null)
			{
				this.m_SpineIllustInstant_SkeletonGraphic.Initialize(true);
				if (this.m_SpineIllustInstant_RectTransform != null)
				{
					BoneFollowerGraphic[] componentsInChildren = this.m_SpineIllustInstant_RectTransform.GetComponentsInChildren<BoneFollowerGraphic>(true);
					if (componentsInChildren != null)
					{
						for (int i = 0; i < componentsInChildren.Length; i++)
						{
							componentsInChildren[i].Initialize();
						}
					}
				}
			}
		}

		// Token: 0x0600310E RID: 12558 RVA: 0x000F3583 File Offset: 0x000F1783
		private void AddAnimation(string AnimationName, bool loop, float delay, int trackIndex = 0)
		{
			if (this.m_SpineIllustInstant_SkeletonGraphic != null && this.m_SpineIllustInstant_SkeletonGraphic.AnimationState != null)
			{
				this.m_SpineIllustInstant_SkeletonGraphic.AnimationState.AddAnimation(trackIndex, AnimationName, loop, delay);
			}
		}

		// Token: 0x0600310F RID: 12559 RVA: 0x000F35B8 File Offset: 0x000F17B8
		private void AddAnimation(NKCASUIUnitIllust.eAnimation eAnim, bool loop, float delay = 0f, int trackIndex = 0)
		{
			string animationName = NKCASUIUnitIllust.GetAnimationName(eAnim);
			this.AddAnimation(animationName, loop, delay, trackIndex);
		}

		// Token: 0x06003110 RID: 12560 RVA: 0x000F35D7 File Offset: 0x000F17D7
		public override float GetAnimationTime(NKCASUIUnitIllust.eAnimation eAnim)
		{
			return this.GetAnimationTime(NKCASUIUnitIllust.GetAnimationName(eAnim));
		}

		// Token: 0x06003111 RID: 12561 RVA: 0x000F35E8 File Offset: 0x000F17E8
		public override float GetAnimationTime(string animName)
		{
			if (this.m_SpineIllustInstant_SkeletonGraphic != null && this.m_SpineIllustInstant_SkeletonGraphic.SkeletonData != null)
			{
				Spine.Animation animation = this.m_SpineIllustInstant_SkeletonGraphic.SkeletonData.FindAnimation(animName);
				if (animation != null)
				{
					return animation.Duration;
				}
			}
			return 0f;
		}

		// Token: 0x06003112 RID: 12562 RVA: 0x000F3634 File Offset: 0x000F1834
		public override NKCASUIUnitIllust.eAnimation GetCurrentAnimation(int trackIndex = 0)
		{
			if (this.m_SpineIllustInstant_SkeletonGraphic != null && this.m_SpineIllustInstant_SkeletonGraphic.AnimationState != null)
			{
				TrackEntry current = this.m_SpineIllustInstant_SkeletonGraphic.AnimationState.GetCurrent(trackIndex);
				if (current != null && current.Animation != null)
				{
					string name = current.Animation.Name;
					foreach (object obj in Enum.GetValues(typeof(NKCASUIUnitIllust.eAnimation)))
					{
						NKCASUIUnitIllust.eAnimation eAnimation = (NKCASUIUnitIllust.eAnimation)obj;
						if (name == NKCASUIUnitIllust.GetAnimationName(eAnimation))
						{
							return eAnimation;
						}
					}
					return NKCASUIUnitIllust.eAnimation.NONE;
				}
			}
			return NKCASUIUnitIllust.eAnimation.NONE;
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x000F36F0 File Offset: 0x000F18F0
		public override string GetCurrentAnimationName(int trackIndex = 0)
		{
			if (this.m_SpineIllustInstant_SkeletonGraphic != null && this.m_SpineIllustInstant_SkeletonGraphic.AnimationState != null)
			{
				TrackEntry current = this.m_SpineIllustInstant_SkeletonGraphic.AnimationState.GetCurrent(trackIndex);
				if (current != null && current.Animation != null)
				{
					return current.Animation.Name;
				}
			}
			return "";
		}

		// Token: 0x06003114 RID: 12564 RVA: 0x000F3748 File Offset: 0x000F1948
		public override float GetCurrentAnimationTime(int trackIndex = 0)
		{
			if (this.m_SpineIllustInstant_SkeletonGraphic != null && this.m_SpineIllustInstant_SkeletonGraphic.AnimationState != null)
			{
				TrackEntry current = this.m_SpineIllustInstant_SkeletonGraphic.AnimationState.GetCurrent(trackIndex);
				if (current != null)
				{
					return current.AnimationTime;
				}
			}
			return 0f;
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x000F3791 File Offset: 0x000F1991
		public override RectTransform GetRectTransform()
		{
			if (this.m_SpineIllustInstant != null && this.m_SpineIllustInstant.m_Instant != null)
			{
				return this.m_SpineIllustInstant.m_Instant.GetComponent<RectTransform>();
			}
			return null;
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x000F37C0 File Offset: 0x000F19C0
		private bool IsHalfFrameAnim(NKCASUIUnitIllust.eAnimation eAnim)
		{
			return eAnim != NKCASUIUnitIllust.eAnimation.UNIT_TOUCH;
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x000F37CC File Offset: 0x000F19CC
		public override void SetVFX(bool bSet)
		{
			if (this.m_NKCFxSpineIllust != null)
			{
				this.m_NKCFxSpineIllust.InitEventListener();
			}
			if (this.m_NKCFxSpineEventState != null)
			{
				this.m_NKCFxSpineEventState.InitEventListener();
			}
			if (this.m_vfx_back != null)
			{
				NKCUtil.SetGameobjectActive(this.m_vfx_back.gameObject, bSet);
			}
			if (this.m_vfx_front != null)
			{
				NKCUtil.SetGameobjectActive(this.m_vfx_front.gameObject, bSet);
			}
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x000F3849 File Offset: 0x000F1A49
		public override Transform GetTalkTransform(bool bLeft)
		{
			if (!bLeft)
			{
				return this.m_trTalk_R;
			}
			return this.m_trTalk_L;
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x000F385C File Offset: 0x000F1A5C
		public override Vector3 GetBoneWorldPosition(string boneName)
		{
			SkeletonGraphic spineIllustInstant_SkeletonGraphic = this.m_SpineIllustInstant_SkeletonGraphic;
			Bone bone;
			if (spineIllustInstant_SkeletonGraphic == null)
			{
				bone = null;
			}
			else
			{
				Skeleton skeleton = spineIllustInstant_SkeletonGraphic.Skeleton;
				bone = ((skeleton != null) ? skeleton.FindBone(boneName) : null);
			}
			Bone bone2 = bone;
			if (bone2 != null)
			{
				float referencePixelsPerUnit = NKCUIManager.FrontCanvas.referencePixelsPerUnit;
				return this.m_SpineIllustInstant_SkeletonGraphic.transform.TransformPoint(bone2.WorldX * referencePixelsPerUnit, bone2.WorldY * referencePixelsPerUnit, 0f);
			}
			Debug.LogError("Bone " + boneName + " not found!");
			return Vector3.zero;
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x000F38D7 File Offset: 0x000F1AD7
		public override void SetTimeScale(float value)
		{
			if (this.m_SpineIllustInstant_SkeletonGraphic != null)
			{
				this.m_SpineIllustInstant_SkeletonGraphic.timeScale = value;
			}
			if (this.m_SpineIllustBG != null)
			{
				this.m_SpineIllustBG.timeScale = value;
			}
		}

		// Token: 0x0600311B RID: 12571 RVA: 0x000F3910 File Offset: 0x000F1B10
		public override Rect GetWorldRect(bool bRecalculateBound = false)
		{
			if (this.m_SpineIllustInstant_SkeletonGraphic != null)
			{
				if (!this.m_bRectCalculated)
				{
					Vector2 v = this.m_SpineIllustInstant_SkeletonGraphic.rectTransform.localPosition;
					if (bRecalculateBound)
					{
						this.m_SpineIllustInstant_SkeletonGraphic.GetLastMesh().RecalculateBounds();
					}
					Bounds bounds = this.m_SpineIllustInstant_SkeletonGraphic.GetLastMesh().bounds;
					Vector3 size = bounds.size;
					Vector3 center = bounds.center;
					Vector2 pivot = new Vector2(0.5f - center.x / size.x, 0.5f - center.y / size.y);
					this.m_SpineIllustInstant_SkeletonGraphic.rectTransform.sizeDelta = size;
					this.m_SpineIllustInstant_SkeletonGraphic.rectTransform.pivot = pivot;
					this.m_SpineIllustInstant_SkeletonGraphic.rectTransform.localPosition = v;
					this.m_bRectCalculated = true;
				}
				return this.m_SpineIllustInstant_SkeletonGraphic.rectTransform.GetWorldRect();
			}
			if (this.m_SpineIllustInstant_RectTransform != null)
			{
				return this.m_SpineIllustInstant_RectTransform.GetWorldRect();
			}
			return default(Rect);
		}

		// Token: 0x0400304F RID: 12367
		public NKCAssetInstanceData m_SpineIllustInstant;

		// Token: 0x04003050 RID: 12368
		public RectTransform m_SpineIllustInstant_RectTransform;

		// Token: 0x04003051 RID: 12369
		public SkeletonGraphic m_SpineIllustInstant_SkeletonGraphic;

		// Token: 0x04003052 RID: 12370
		public SkeletonGraphic m_SpineIllustBG;

		// Token: 0x04003053 RID: 12371
		public NKCComSpineSkeletonAnimationEvent m_NKCComSpineSkeletonAnimationEvent;

		// Token: 0x04003054 RID: 12372
		public NKC_FX_SPINE_ILLUST m_NKCFxSpineIllust;

		// Token: 0x04003055 RID: 12373
		public NKC_FX_SPINE_EVENT_STATE m_NKCFxSpineEventState;

		// Token: 0x04003056 RID: 12374
		private Material m_matDefault;

		// Token: 0x04003057 RID: 12375
		private Material m_matDefaultBG;

		// Token: 0x04003058 RID: 12376
		private Transform m_vfx_back;

		// Token: 0x04003059 RID: 12377
		private Transform m_vfx_front;

		// Token: 0x0400305A RID: 12378
		private Transform m_trTalk_L;

		// Token: 0x0400305B RID: 12379
		private Transform m_trTalk_R;

		// Token: 0x0400305C RID: 12380
		private float m_fR = 1f;

		// Token: 0x0400305D RID: 12381
		private float m_fG = 1f;

		// Token: 0x0400305E RID: 12382
		private float m_fB = 1f;

		// Token: 0x0400305F RID: 12383
		private float m_fA = 1f;

		// Token: 0x04003060 RID: 12384
		private Color m_ColorOrg;

		// Token: 0x04003061 RID: 12385
		private Color m_ColorOrgBG;

		// Token: 0x04003062 RID: 12386
		private Color m_ColorTemp;

		// Token: 0x04003063 RID: 12387
		public const string DEFAULT_SKIN_NAME = "default";

		// Token: 0x04003064 RID: 12388
		public const string UNITONLY_SKIN_NAME = "ONLY_UNIT";

		// Token: 0x04003065 RID: 12389
		public const string SKIN_OPTION_NAME = "SKIN_{0}";
	}
}
