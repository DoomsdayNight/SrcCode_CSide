using System;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200061B RID: 1563
	[DisallowMultipleComponent]
	public class NKCComSpineActiveReset : MonoBehaviour
	{
		// Token: 0x0600304F RID: 12367 RVA: 0x000EDEB4 File Offset: 0x000EC0B4
		private void Awake()
		{
			this.m_SkeletonAnimation = base.gameObject.GetComponent<SkeletonAnimation>();
			this.m_SkeletonGraphic = base.gameObject.GetComponent<SkeletonGraphic>();
			if (this.m_SkeletonAnimation != null)
			{
				this.m_SkeletonAnimation.Awake();
				this.m_SkeletonAnimation.enabled = false;
			}
			if (this.m_SkeletonGraphic != null)
			{
				this.m_SkeletonGraphic.enabled = false;
			}
		}

		// Token: 0x06003050 RID: 12368 RVA: 0x000EDF24 File Offset: 0x000EC124
		private void OnEnable()
		{
			if (this.m_SkeletonAnimation != null)
			{
				this.m_SkeletonAnimation.AnimationState.SetAnimation(0, this.m_AnimName, this.m_bLoop);
				this.m_TrackEntry = this.m_SkeletonAnimation.AnimationState.GetCurrent(0);
				this.m_SkeletonAnimation.Update(0f);
				this.m_SkeletonAnimation.enabled = true;
			}
			if (this.m_SkeletonGraphic != null)
			{
				this.m_SkeletonGraphic.AnimationState.SetAnimation(0, this.m_AnimName, this.m_bLoop);
				this.m_TrackEntry = this.m_SkeletonGraphic.AnimationState.GetCurrent(0);
				this.m_SkeletonGraphic._Update(0f);
				this.m_SkeletonGraphic.enabled = true;
			}
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x000EDFF0 File Offset: 0x000EC1F0
		private void OnDisable()
		{
			if (this.m_SkeletonAnimation != null)
			{
				this.m_SkeletonAnimation.enabled = false;
			}
			if (this.m_SkeletonGraphic != null)
			{
				this.m_SkeletonGraphic.enabled = false;
			}
			this.m_TrackEntry.TrackTime = 0f;
		}

		// Token: 0x04002FB1 RID: 12209
		public string m_AnimName = "BASE";

		// Token: 0x04002FB2 RID: 12210
		public bool m_bLoop;

		// Token: 0x04002FB3 RID: 12211
		private SkeletonAnimation m_SkeletonAnimation;

		// Token: 0x04002FB4 RID: 12212
		private SkeletonGraphic m_SkeletonGraphic;

		// Token: 0x04002FB5 RID: 12213
		private TrackEntry m_TrackEntry;
	}
}
