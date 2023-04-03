using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000298 RID: 664
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Sets a trigger parameter to active or inactive. Returns Success.")]
	public class SetTrigger : Action
	{
		// Token: 0x06000D76 RID: 3446 RVA: 0x00028080 File Offset: 0x00026280
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x000280C0 File Offset: 0x000262C0
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			this.animator.SetTrigger(this.paramaterName.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x000280F3 File Offset: 0x000262F3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName = "";
		}

		// Token: 0x04000944 RID: 2372
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000945 RID: 2373
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x04000946 RID: 2374
		private Animator animator;

		// Token: 0x04000947 RID: 2375
		private GameObject prevGameObject;
	}
}
