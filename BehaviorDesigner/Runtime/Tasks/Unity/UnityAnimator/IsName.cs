using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x0200028C RID: 652
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Returns success if the specified name matches the name of the active state.")]
	public class IsName : Conditional
	{
		// Token: 0x06000D41 RID: 3393 RVA: 0x000276DC File Offset: 0x000258DC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0002771C File Offset: 0x0002591C
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			if (!this.animator.GetCurrentAnimatorStateInfo(this.index.Value).IsName(this.name.Value))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00027771 File Offset: 0x00025971
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.index = 0;
			this.name = "";
		}

		// Token: 0x040008FF RID: 2303
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000900 RID: 2304
		[Tooltip("The layer's index")]
		public SharedInt index;

		// Token: 0x04000901 RID: 2305
		[Tooltip("The state name to compare")]
		public SharedString name;

		// Token: 0x04000902 RID: 2306
		private Animator animator;

		// Token: 0x04000903 RID: 2307
		private GameObject prevGameObject;
	}
}
