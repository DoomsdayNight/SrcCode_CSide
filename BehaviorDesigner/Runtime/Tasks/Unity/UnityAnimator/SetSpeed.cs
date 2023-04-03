using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000297 RID: 663
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Sets the playback speed of the Animator. 1 is normal playback speed. Returns Success.")]
	public class SetSpeed : Action
	{
		// Token: 0x06000D72 RID: 3442 RVA: 0x00027FEC File Offset: 0x000261EC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0002802C File Offset: 0x0002622C
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			this.animator.speed = this.speed.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x0002805F File Offset: 0x0002625F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.speed = 0f;
		}

		// Token: 0x04000940 RID: 2368
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000941 RID: 2369
		[Tooltip("The playback speed of the Animator")]
		public SharedFloat speed;

		// Token: 0x04000942 RID: 2370
		private Animator animator;

		// Token: 0x04000943 RID: 2371
		private GameObject prevGameObject;
	}
}
