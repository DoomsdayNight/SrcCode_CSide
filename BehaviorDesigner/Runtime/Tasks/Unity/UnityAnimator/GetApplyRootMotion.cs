using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000280 RID: 640
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Stores if root motion is applied. Returns Success.")]
	public class GetApplyRootMotion : Action
	{
		// Token: 0x06000D12 RID: 3346 RVA: 0x00026FDC File Offset: 0x000251DC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0002701C File Offset: 0x0002521C
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.animator.applyRootMotion;
			return TaskStatus.Success;
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0002704F File Offset: 0x0002524F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x040008CD RID: 2253
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008CE RID: 2254
		[Tooltip("Is root motion applied?")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x040008CF RID: 2255
		private Animator animator;

		// Token: 0x040008D0 RID: 2256
		private GameObject prevGameObject;
	}
}
