using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000290 RID: 656
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Sets if root motion is applied. Returns Success.")]
	public class SetApplyRootMotion : Action
	{
		// Token: 0x06000D51 RID: 3409 RVA: 0x00027A24 File Offset: 0x00025C24
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00027A64 File Offset: 0x00025C64
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			this.animator.applyRootMotion = this.rootMotion.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00027A97 File Offset: 0x00025C97
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.rootMotion = false;
		}

		// Token: 0x04000918 RID: 2328
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000919 RID: 2329
		[Tooltip("Is root motion applied?")]
		public SharedBool rootMotion;

		// Token: 0x0400091A RID: 2330
		private Animator animator;

		// Token: 0x0400091B RID: 2331
		private GameObject prevGameObject;
	}
}
