using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKC.UI;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace NKC.Office
{
	// Token: 0x0200083D RID: 2109
	public class NKCOfficeSpineFurniture : NKCOfficeFuniture
	{
		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x06005407 RID: 21511 RVA: 0x0019A4D6 File Offset: 0x001986D6
		private bool bUseInvert
		{
			get
			{
				return this.m_bInvert && this.m_aSpineInvert != null;
			}
		}

		// Token: 0x06005408 RID: 21512 RVA: 0x0019A4EB File Offset: 0x001986EB
		private SkeletonGraphic[] GetCurrentSpineSet()
		{
			if (!this.bUseInvert)
			{
				return this.m_aSpineFurniture;
			}
			return this.m_aSpineInvert;
		}

		// Token: 0x06005409 RID: 21513 RVA: 0x0019A504 File Offset: 0x00198704
		public override void Init()
		{
			base.Init();
			if (this.m_rtFuniture != null && this.m_aSpineFurniture == null)
			{
				this.m_aSpineFurniture = this.m_rtFuniture.GetComponentsInChildren<SkeletonGraphic>();
			}
			if (this.m_aSpineFurniture != null)
			{
				foreach (SkeletonGraphic skeletonGraphic in this.m_aSpineFurniture)
				{
					skeletonGraphic.raycastTarget = true;
					this.SetAnimation(skeletonGraphic, "BASE", true, 1f);
				}
			}
			if (this.m_rtInverse != null && this.m_aSpineInvert == null)
			{
				this.m_aSpineInvert = this.m_rtInverse.GetComponentsInChildren<SkeletonGraphic>();
			}
			if (this.m_aSpineInvert != null)
			{
				foreach (SkeletonGraphic skeletonGraphic2 in this.m_aSpineInvert)
				{
					skeletonGraphic2.raycastTarget = true;
					this.SetAnimation(skeletonGraphic2, "BASE", true, 1f);
				}
			}
		}

		// Token: 0x0600540A RID: 21514 RVA: 0x0019A5DC File Offset: 0x001987DC
		public override void OnTouchReact()
		{
			base.OnTouchReact();
			List<string> list = new List<string>();
			if (this.HasAnimation(this.m_aSpineFurniture, "TOUCH"))
			{
				list.Add("TOUCH");
			}
			int num = 1;
			while (this.HasAnimation(this.m_aSpineFurniture, "TOUCH" + num.ToString()))
			{
				list.Add("TOUCH" + num.ToString());
				num++;
			}
			if (list.Count > 0)
			{
				string animName = list[UnityEngine.Random.Range(0, list.Count)];
				SkeletonGraphic[] currentSpineSet = this.GetCurrentSpineSet();
				this.SetAnimation(currentSpineSet, animName, false, 1f);
				this.AddAnimation(currentSpineSet, "BASE", true);
			}
		}

		// Token: 0x0600540B RID: 21515 RVA: 0x0019A68F File Offset: 0x0019888F
		public override void InvalidateWorldRect()
		{
			base.InvalidateWorldRect();
			this.m_bRectCalculated = false;
		}

		// Token: 0x0600540C RID: 21516 RVA: 0x0019A69E File Offset: 0x0019889E
		protected override Rect GetFurnitureRect()
		{
			if (!this.m_bRectCalculated)
			{
				this.m_rectWorld = this.CalculateWorldRect();
				this.m_bRectCalculated = true;
			}
			return this.m_rectWorld;
		}

		// Token: 0x0600540D RID: 21517 RVA: 0x0019A6C4 File Offset: 0x001988C4
		private Rect CalculateWorldRect()
		{
			Rect rect;
			if (this.m_aSpineFurniture != null && this.m_aSpineFurniture.Length != 0)
			{
				GameObject gameObject = new GameObject("temp", new Type[]
				{
					typeof(RectTransform)
				});
				RectTransform component = gameObject.GetComponent<RectTransform>();
				rect = new Rect(base.transform.position, Vector2.zero);
				for (int i = 0; i < this.m_aSpineFurniture.Length; i++)
				{
					SkeletonGraphic skeletonGraphic = this.m_aSpineFurniture[i];
					component.SetParent(skeletonGraphic.rectTransform.parent);
					component.localPosition = skeletonGraphic.rectTransform.localPosition;
					component.localRotation = skeletonGraphic.rectTransform.localRotation;
					component.localScale = skeletonGraphic.rectTransform.localScale;
					Bounds bounds = skeletonGraphic.GetLastMesh().bounds;
					Vector3 size = bounds.size;
					Vector3 center = bounds.center;
					Vector2 pivot = new Vector2(0.5f - center.x / size.x, 0.5f - center.y / size.y);
					component.sizeDelta = size;
					component.pivot = pivot;
					Rect worldRect = component.GetWorldRect();
					rect = rect.Union(worldRect);
				}
				UnityEngine.Object.Destroy(gameObject);
			}
			else if (this.m_rtFuniture != null)
			{
				rect = this.m_rtFuniture.GetWorldRect();
			}
			else
			{
				rect = new Rect(base.transform.position, Vector2.zero);
			}
			if (this.m_rtTouchArea != null)
			{
				Rect worldRect2 = this.m_rtTouchArea.GetWorldRect();
				rect = rect.Union(worldRect2);
			}
			return rect;
		}

		// Token: 0x0600540E RID: 21518 RVA: 0x0019A870 File Offset: 0x00198A70
		public override void SetInvert(bool bInvert, bool bEditMode = false)
		{
			base.SetInvert(bInvert, bEditMode);
			if (this.m_rtTouchArea != null)
			{
				this.m_rtTouchArea.rotation = Quaternion.identity;
			}
		}

		// Token: 0x0600540F RID: 21519 RVA: 0x0019A898 File Offset: 0x00198A98
		protected bool HasAnimation(SkeletonGraphic target, string AnimName)
		{
			return !(target == null) && target.SkeletonData != null && target.SkeletonData.FindAnimation(AnimName) != null;
		}

		// Token: 0x06005410 RID: 21520 RVA: 0x0019A8BC File Offset: 0x00198ABC
		protected bool HasAnimation(SkeletonGraphic[] aTarget, string AnimName)
		{
			if (aTarget == null)
			{
				return false;
			}
			foreach (SkeletonGraphic skeletonGraphic in aTarget)
			{
				if (skeletonGraphic.SkeletonData != null && skeletonGraphic.SkeletonData.FindAnimation(AnimName) != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005411 RID: 21521 RVA: 0x0019A8FB File Offset: 0x00198AFB
		protected void SetAnimation(SkeletonGraphic target, string AnimName, bool loop, float timeScale = 1f)
		{
			if (this.HasAnimation(target, AnimName))
			{
				Skeleton skeleton = target.Skeleton;
				if (skeleton != null)
				{
					skeleton.SetToSetupPose();
				}
				target.AnimationState.SetAnimation(0, AnimName, loop).TimeScale = timeScale;
			}
		}

		// Token: 0x06005412 RID: 21522 RVA: 0x0019A930 File Offset: 0x00198B30
		protected void SetAnimation(SkeletonGraphic[] aTarget, string AnimName, bool loop, float timeScale = 1f)
		{
			if (aTarget == null)
			{
				return;
			}
			foreach (SkeletonGraphic skeletonGraphic in aTarget)
			{
				if (this.HasAnimation(skeletonGraphic, AnimName))
				{
					Skeleton skeleton = skeletonGraphic.Skeleton;
					if (skeleton != null)
					{
						skeleton.SetToSetupPose();
					}
					skeletonGraphic.AnimationState.SetAnimation(0, AnimName, loop).TimeScale = timeScale;
				}
			}
		}

		// Token: 0x06005413 RID: 21523 RVA: 0x0019A985 File Offset: 0x00198B85
		protected void AddAnimation(SkeletonGraphic target, string AnimName, bool loop)
		{
			if (this.HasAnimation(target, AnimName))
			{
				target.AnimationState.AddAnimation(0, AnimName, loop, 0f);
			}
		}

		// Token: 0x06005414 RID: 21524 RVA: 0x0019A9A8 File Offset: 0x00198BA8
		protected void AddAnimation(SkeletonGraphic[] aTarget, string AnimName, bool loop)
		{
			if (aTarget == null)
			{
				return;
			}
			foreach (SkeletonGraphic skeletonGraphic in aTarget)
			{
				if (this.HasAnimation(skeletonGraphic, AnimName))
				{
					skeletonGraphic.AnimationState.AddAnimation(0, AnimName, loop, 0f);
				}
			}
		}

		// Token: 0x06005415 RID: 21525 RVA: 0x0019A9EB File Offset: 0x00198BEB
		public override RectTransform MakeHighlightRect()
		{
			if (this.m_rtTouchArea != null)
			{
				return this.m_rtTouchArea;
			}
			return base.MakeHighlightRect();
		}

		// Token: 0x06005416 RID: 21526 RVA: 0x0019AA08 File Offset: 0x00198C08
		public override void SetColor(Color color)
		{
			base.SetColor(color);
			if (this.m_aSpineFurniture != null)
			{
				foreach (SkeletonGraphic skeletonGraphic in this.m_aSpineFurniture)
				{
					skeletonGraphic.DOKill(false);
					skeletonGraphic.color = color;
				}
			}
			if (this.m_aSpineInvert != null)
			{
				foreach (SkeletonGraphic skeletonGraphic2 in this.m_aSpineInvert)
				{
					skeletonGraphic2.DOKill(false);
					skeletonGraphic2.color = color;
				}
			}
		}

		// Token: 0x06005417 RID: 21527 RVA: 0x0019AA78 File Offset: 0x00198C78
		public override void SetAlpha(float value)
		{
			base.SetAlpha(value);
			if (this.m_aSpineFurniture != null)
			{
				foreach (SkeletonGraphic skeletonGraphic in this.m_aSpineFurniture)
				{
					skeletonGraphic.DOKill(false);
					skeletonGraphic.color = new Color(1f, 1f, 1f, value);
				}
			}
			if (this.m_aSpineInvert != null)
			{
				foreach (SkeletonGraphic skeletonGraphic2 in this.m_aSpineInvert)
				{
					skeletonGraphic2.DOKill(false);
					skeletonGraphic2.color = new Color(1f, 1f, 1f, value);
				}
			}
		}

		// Token: 0x06005418 RID: 21528 RVA: 0x0019AB10 File Offset: 0x00198D10
		public override void SetGlow(Color color, float time)
		{
			base.SetGlow(color, time);
			if (this.m_aSpineFurniture != null)
			{
				foreach (SkeletonGraphic skeletonGraphic in this.m_aSpineFurniture)
				{
					skeletonGraphic.DOKill(false);
					skeletonGraphic.color = Color.white;
					skeletonGraphic.DOColor(color, time).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
				}
			}
			if (this.m_aSpineInvert != null)
			{
				foreach (SkeletonGraphic skeletonGraphic2 in this.m_aSpineInvert)
				{
					skeletonGraphic2.DOKill(false);
					skeletonGraphic2.color = Color.white;
					skeletonGraphic2.DOColor(color, time).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
				}
			}
		}

		// Token: 0x06005419 RID: 21529 RVA: 0x0019ABB5 File Offset: 0x00198DB5
		public override void CleanupAnimEvent()
		{
			base.CleanupAnimEvent();
			this.SetAnimation(this.m_aSpineFurniture, "BASE", true, 1f);
			this.SetAnimation(this.m_aSpineInvert, "BASE", true, 1f);
		}

		// Token: 0x0600541A RID: 21530 RVA: 0x0019ABEC File Offset: 0x00198DEC
		public override Vector3 GetBonePosition(string name)
		{
			foreach (SkeletonGraphic skeletonGraphic in this.GetCurrentSpineSet())
			{
				Skeleton skeleton = skeletonGraphic.Skeleton;
				Bone bone = (skeleton != null) ? skeleton.FindBone(name) : null;
				if (bone != null)
				{
					float referencePixelsPerUnit = NKCUIManager.FrontCanvas.referencePixelsPerUnit;
					return skeletonGraphic.transform.TransformPoint(bone.WorldX * referencePixelsPerUnit, bone.WorldY * referencePixelsPerUnit, 0f);
				}
			}
			Debug.LogError("Bone " + name + " not found!");
			return Vector3.zero;
		}

		// Token: 0x0600541B RID: 21531 RVA: 0x0019AC72 File Offset: 0x00198E72
		public override void PlaySpineAnimation(string name, bool loop, float timeScale)
		{
			this.SetAnimation(this.GetCurrentSpineSet(), name, loop, timeScale);
		}

		// Token: 0x0600541C RID: 21532 RVA: 0x0019AC83 File Offset: 0x00198E83
		public override void PlaySpineAnimation(NKCASUIUnitIllust.eAnimation eAnim, bool loop, float timeScale, bool bDefaultAnim)
		{
			Debug.LogError("가구에는 ANIMATION_NAME_SPINE만 써 주세요");
		}

		// Token: 0x0600541D RID: 21533 RVA: 0x0019AC90 File Offset: 0x00198E90
		public override bool IsSpineAnimationFinished(NKCASUIUnitIllust.eAnimation eAnim)
		{
			string animationName = NKCASUIUnitIllust.GetAnimationName(eAnim);
			return this.IsSpineAnimationFinished(animationName);
		}

		// Token: 0x0600541E RID: 21534 RVA: 0x0019ACAC File Offset: 0x00198EAC
		public override bool IsSpineAnimationFinished(string name)
		{
			foreach (SkeletonGraphic skeletonGraphic in this.GetCurrentSpineSet())
			{
				if (skeletonGraphic != null && skeletonGraphic.AnimationState != null)
				{
					TrackEntry current = skeletonGraphic.AnimationState.GetCurrent(0);
					if (current != null && current.Animation != null && !(current.Animation.Name != name) && current.AnimationTime <= current.AnimationEnd)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600541F RID: 21535 RVA: 0x0019AD1F File Offset: 0x00198F1F
		public override bool CanPlaySpineAnimation(NKCASUIUnitIllust.eAnimation eAnim)
		{
			return this.CanPlaySpineAnimation(NKCASUIUnitIllust.GetAnimationName(eAnim));
		}

		// Token: 0x06005420 RID: 21536 RVA: 0x0019AD30 File Offset: 0x00198F30
		public override bool CanPlaySpineAnimation(string animName)
		{
			foreach (SkeletonGraphic skeletonGraphic in this.GetCurrentSpineSet())
			{
				if (!(skeletonGraphic == null) && skeletonGraphic.SkeletonData != null && skeletonGraphic.SkeletonData.FindAnimation(animName) != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04004324 RID: 17188
		private const string ANIM_BASE = "BASE";

		// Token: 0x04004325 RID: 17189
		private const string ANIM_TOUCH = "TOUCH";

		// Token: 0x04004326 RID: 17190
		public RectTransform m_rtTouchArea;

		// Token: 0x04004327 RID: 17191
		protected SkeletonGraphic[] m_aSpineFurniture;

		// Token: 0x04004328 RID: 17192
		protected SkeletonGraphic[] m_aSpineInvert;

		// Token: 0x04004329 RID: 17193
		private bool m_bRectCalculated;

		// Token: 0x0400432A RID: 17194
		private Rect m_rectWorld;
	}
}
