using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000285 RID: 645
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Stores the current gravity weight based on current animations that are played. Returns Success.")]
	public class GetGravityWeight : Action
	{
		// Token: 0x06000D26 RID: 3366 RVA: 0x000272F4 File Offset: 0x000254F4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00027334 File Offset: 0x00025534
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.animator.gravityWeight;
			return TaskStatus.Success;
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00027367 File Offset: 0x00025567
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040008E3 RID: 2275
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008E4 RID: 2276
		[Tooltip("The value of the gravity weight")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040008E5 RID: 2277
		private Animator animator;

		// Token: 0x040008E6 RID: 2278
		private GameObject prevGameObject;
	}
}
