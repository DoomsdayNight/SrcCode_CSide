using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000282 RID: 642
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Gets the avatar delta position for the last evaluated frame. Returns Success.")]
	public class GetDeltaPosition : Action
	{
		// Token: 0x06000D1A RID: 3354 RVA: 0x00027118 File Offset: 0x00025318
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00027158 File Offset: 0x00025358
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.animator.deltaPosition;
			return TaskStatus.Success;
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0002718B File Offset: 0x0002538B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x040008D6 RID: 2262
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008D7 RID: 2263
		[Tooltip("The avatar delta position")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x040008D8 RID: 2264
		private Animator animator;

		// Token: 0x040008D9 RID: 2265
		private GameObject prevGameObject;
	}
}
