using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x0200029B RID: 667
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Stops the animator playback mode. Returns Success.")]
	public class StopPlayback : Action
	{
		// Token: 0x06000D82 RID: 3458 RVA: 0x00028218 File Offset: 0x00026418
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00028258 File Offset: 0x00026458
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			this.animator.StopPlayback();
			return TaskStatus.Success;
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00028280 File Offset: 0x00026480
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x0400094F RID: 2383
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000950 RID: 2384
		private Animator animator;

		// Token: 0x04000951 RID: 2385
		private GameObject prevGameObject;
	}
}
