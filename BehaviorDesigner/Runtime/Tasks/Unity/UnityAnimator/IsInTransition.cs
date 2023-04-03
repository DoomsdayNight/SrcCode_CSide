using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x0200028B RID: 651
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Returns success if the specified AnimatorController layer in a transition.")]
	public class IsInTransition : Conditional
	{
		// Token: 0x06000D3D RID: 3389 RVA: 0x00027648 File Offset: 0x00025848
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00027688 File Offset: 0x00025888
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			if (!this.animator.IsInTransition(this.index.Value))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x000276BF File Offset: 0x000258BF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.index = 0;
		}

		// Token: 0x040008FB RID: 2299
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008FC RID: 2300
		[Tooltip("The layer's index")]
		public SharedInt index;

		// Token: 0x040008FD RID: 2301
		private Animator animator;

		// Token: 0x040008FE RID: 2302
		private GameObject prevGameObject;
	}
}
