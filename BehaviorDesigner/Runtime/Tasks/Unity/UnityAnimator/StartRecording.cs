using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x0200029A RID: 666
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Sets the animator in recording mode. Returns Success.")]
	public class StartRecording : Action
	{
		// Token: 0x06000D7E RID: 3454 RVA: 0x00028190 File Offset: 0x00026390
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x000281D0 File Offset: 0x000263D0
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			this.animator.StartRecording(this.frameCount);
			return TaskStatus.Success;
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x000281FE File Offset: 0x000263FE
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.frameCount = 0;
		}

		// Token: 0x0400094B RID: 2379
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400094C RID: 2380
		[Tooltip("The number of frames (updates) that will be recorded")]
		public int frameCount;

		// Token: 0x0400094D RID: 2381
		private Animator animator;

		// Token: 0x0400094E RID: 2382
		private GameObject prevGameObject;
	}
}
