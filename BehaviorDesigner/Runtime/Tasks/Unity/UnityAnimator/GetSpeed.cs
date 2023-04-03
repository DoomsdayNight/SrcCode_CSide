using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000288 RID: 648
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Stores the playback speed of the animator. 1 is normal playback speed. Returns Success.")]
	public class GetSpeed : Action
	{
		// Token: 0x06000D32 RID: 3378 RVA: 0x000274E0 File Offset: 0x000256E0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x00027520 File Offset: 0x00025720
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.animator.speed;
			return TaskStatus.Success;
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00027553 File Offset: 0x00025753
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040008F1 RID: 2289
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008F2 RID: 2290
		[Tooltip("The playback speed of the Animator")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040008F3 RID: 2291
		private Animator animator;

		// Token: 0x040008F4 RID: 2292
		private GameObject prevGameObject;
	}
}
