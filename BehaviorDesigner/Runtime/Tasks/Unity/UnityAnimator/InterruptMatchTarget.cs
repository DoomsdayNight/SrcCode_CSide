using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x0200028A RID: 650
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Interrupts the automatic target matching. Returns Success.")]
	public class InterruptMatchTarget : Action
	{
		// Token: 0x06000D39 RID: 3385 RVA: 0x000275B8 File Offset: 0x000257B8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x000275F8 File Offset: 0x000257F8
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			this.animator.InterruptMatchTarget(this.completeMatch);
			return TaskStatus.Success;
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00027626 File Offset: 0x00025826
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.completeMatch = true;
		}

		// Token: 0x040008F7 RID: 2295
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008F8 RID: 2296
		[Tooltip("CompleteMatch will make the gameobject match the target completely at the next frame")]
		public bool completeMatch = true;

		// Token: 0x040008F9 RID: 2297
		private Animator animator;

		// Token: 0x040008FA RID: 2298
		private GameObject prevGameObject;
	}
}
