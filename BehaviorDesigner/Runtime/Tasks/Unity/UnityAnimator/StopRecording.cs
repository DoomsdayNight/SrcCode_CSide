using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x0200029C RID: 668
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Stops animator record mode. Returns Success.")]
	public class StopRecording : Action
	{
		// Token: 0x06000D86 RID: 3462 RVA: 0x00028294 File Offset: 0x00026494
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x000282D4 File Offset: 0x000264D4
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			this.animator.StopRecording();
			return TaskStatus.Success;
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x000282FC File Offset: 0x000264FC
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000952 RID: 2386
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000953 RID: 2387
		private Animator animator;

		// Token: 0x04000954 RID: 2388
		private GameObject prevGameObject;
	}
}
