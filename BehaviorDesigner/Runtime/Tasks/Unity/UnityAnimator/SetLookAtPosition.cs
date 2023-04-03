using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000295 RID: 661
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Sets the look at position. Returns Success.")]
	public class SetLookAtPosition : Action
	{
		// Token: 0x06000D68 RID: 3432 RVA: 0x00027E7B File Offset: 0x0002607B
		public override void OnStart()
		{
			this.animator = base.GetComponent<Animator>();
			this.positionSet = false;
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00027E90 File Offset: 0x00026090
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			if (!this.positionSet)
			{
				return TaskStatus.Running;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00027EB7 File Offset: 0x000260B7
		public override void OnAnimatorIK()
		{
			if (this.animator == null)
			{
				return;
			}
			this.animator.SetLookAtPosition(this.position.Value);
			this.positionSet = true;
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00027EE5 File Offset: 0x000260E5
		public override void OnReset()
		{
			this.position = Vector3.zero;
		}

		// Token: 0x04000936 RID: 2358
		[Tooltip("The position to lookAt")]
		public SharedVector3 position;

		// Token: 0x04000937 RID: 2359
		private Animator animator;

		// Token: 0x04000938 RID: 2360
		private bool positionSet;
	}
}
