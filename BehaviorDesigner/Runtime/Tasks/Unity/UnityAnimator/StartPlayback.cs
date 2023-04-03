using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000299 RID: 665
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Sets the animator in playback mode.")]
	public class StartPlayback : Action
	{
		// Token: 0x06000D7A RID: 3450 RVA: 0x00028114 File Offset: 0x00026314
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00028154 File Offset: 0x00026354
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			this.animator.StartPlayback();
			return TaskStatus.Success;
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0002817C File Offset: 0x0002637C
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000948 RID: 2376
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000949 RID: 2377
		private Animator animator;

		// Token: 0x0400094A RID: 2378
		private GameObject prevGameObject;
	}
}
