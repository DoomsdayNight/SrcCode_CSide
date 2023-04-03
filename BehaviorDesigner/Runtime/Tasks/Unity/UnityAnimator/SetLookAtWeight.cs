using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000296 RID: 662
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Sets the look at weight. Returns success immediately after.")]
	public class SetLookAtWeight : Action
	{
		// Token: 0x06000D6D RID: 3437 RVA: 0x00027EFF File Offset: 0x000260FF
		public override void OnStart()
		{
			this.animator = base.GetComponent<Animator>();
			this.weightSet = false;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00027F14 File Offset: 0x00026114
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			if (!this.weightSet)
			{
				return TaskStatus.Running;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00027F3C File Offset: 0x0002613C
		public override void OnAnimatorIK()
		{
			if (this.animator == null)
			{
				return;
			}
			this.animator.SetLookAtWeight(this.weight.Value, this.bodyWeight, this.headWeight, this.eyesWeight, this.clampWeight);
			this.weightSet = true;
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00027F8D File Offset: 0x0002618D
		public override void OnReset()
		{
			this.weight = 0f;
			this.bodyWeight = 0f;
			this.headWeight = 1f;
			this.eyesWeight = 0f;
			this.clampWeight = 0.5f;
		}

		// Token: 0x04000939 RID: 2361
		[Tooltip("(0-1) the global weight of the LookAt, multiplier for other parameters.")]
		public SharedFloat weight;

		// Token: 0x0400093A RID: 2362
		[Tooltip("(0-1) determines how much the body is involved in the LookAt.")]
		public float bodyWeight;

		// Token: 0x0400093B RID: 2363
		[Tooltip("(0-1) determines how much the head is involved in the LookAt.")]
		public float headWeight = 1f;

		// Token: 0x0400093C RID: 2364
		[Tooltip("(0-1) determines how much the eyes are involved in the LookAt.")]
		public float eyesWeight;

		// Token: 0x0400093D RID: 2365
		[Tooltip("(0-1) 0.0 means the character is completely unrestrained in motion, 1.0 means he's completely clamped (look at becomes impossible), and 0.5 means he'll be able to move on half of the possible range (180 degrees).")]
		public float clampWeight = 0.5f;

		// Token: 0x0400093E RID: 2366
		private Animator animator;

		// Token: 0x0400093F RID: 2367
		private bool weightSet;
	}
}
