using System;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NKC.UI.Component
{
	// Token: 0x02000C50 RID: 3152
	public class NKCComSpineTouchAnimator : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		// Token: 0x060092CE RID: 37582 RVA: 0x00321A28 File Offset: 0x0031FC28
		public void OnPointerClick(PointerEventData eventData)
		{
			if (this.m_aSkeletonGraphics == null)
			{
				this.m_aSkeletonGraphics = base.GetComponentsInChildren<SkeletonGraphic>();
			}
			List<string> list = new List<string>();
			if (this.HasAnimation(this.m_aSkeletonGraphics, this.ANIM_TOUCH))
			{
				list.Add(this.ANIM_TOUCH);
			}
			int num = 1;
			while (this.HasAnimation(this.m_aSkeletonGraphics, this.ANIM_TOUCH + num.ToString()))
			{
				list.Add(this.ANIM_TOUCH + num.ToString());
				num++;
			}
			if (list.Count > 0)
			{
				string animName = list[UnityEngine.Random.Range(0, list.Count)];
				this.SetAnimation(this.m_aSkeletonGraphics, animName, false, 1f);
				this.AddAnimation(this.m_aSkeletonGraphics, this.ANIM_BASE, true);
			}
		}

		// Token: 0x060092CF RID: 37583 RVA: 0x00321AF4 File Offset: 0x0031FCF4
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

		// Token: 0x060092D0 RID: 37584 RVA: 0x00321B49 File Offset: 0x0031FD49
		protected bool HasAnimation(SkeletonGraphic target, string AnimName)
		{
			return !(target == null) && target.SkeletonData != null && target.SkeletonData.FindAnimation(AnimName) != null;
		}

		// Token: 0x060092D1 RID: 37585 RVA: 0x00321B70 File Offset: 0x0031FD70
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

		// Token: 0x060092D2 RID: 37586 RVA: 0x00321BAF File Offset: 0x0031FDAF
		protected void AddAnimation(SkeletonGraphic target, string AnimName, bool loop)
		{
			if (this.HasAnimation(target, AnimName))
			{
				target.AnimationState.AddAnimation(0, AnimName, loop, 0f);
			}
		}

		// Token: 0x060092D3 RID: 37587 RVA: 0x00321BD0 File Offset: 0x0031FDD0
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

		// Token: 0x04007FC5 RID: 32709
		[Header("기본 애니 이름. 반드시 존재해야 함")]
		public string ANIM_BASE = "BASE";

		// Token: 0x04007FC6 RID: 32710
		[Header("터치 애니 이름. 여러 개의 애니가 존재하는 경우 TOUCH, TOUCH1, TOUCH2.. 등으로 애니를 만들어 넣으면 랜덤하게 하나를 재생")]
		public string ANIM_TOUCH = "TOUCH";

		// Token: 0x04007FC7 RID: 32711
		private SkeletonGraphic[] m_aSkeletonGraphics;
	}
}
