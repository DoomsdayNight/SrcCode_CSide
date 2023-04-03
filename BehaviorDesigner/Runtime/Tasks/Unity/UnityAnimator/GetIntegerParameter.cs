using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000286 RID: 646
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Stores the integer parameter on an animator. Returns Success.")]
	public class GetIntegerParameter : Action
	{
		// Token: 0x06000D2A RID: 3370 RVA: 0x00027388 File Offset: 0x00025588
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x000273C8 File Offset: 0x000255C8
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.animator.GetInteger(this.paramaterName.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00027406 File Offset: 0x00025606
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName = "";
			this.storeValue = 0;
		}

		// Token: 0x040008E7 RID: 2279
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008E8 RID: 2280
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x040008E9 RID: 2281
		[Tooltip("The value of the integer parameter")]
		[RequiredField]
		public SharedInt storeValue;

		// Token: 0x040008EA RID: 2282
		private Animator animator;

		// Token: 0x040008EB RID: 2283
		private GameObject prevGameObject;
	}
}
