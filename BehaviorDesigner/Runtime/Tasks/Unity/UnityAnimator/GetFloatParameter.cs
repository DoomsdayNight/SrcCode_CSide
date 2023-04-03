using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000284 RID: 644
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Stores the float parameter on an animator. Returns Success.")]
	public class GetFloatParameter : Action
	{
		// Token: 0x06000D22 RID: 3362 RVA: 0x00027244 File Offset: 0x00025444
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00027284 File Offset: 0x00025484
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.animator.GetFloat(this.paramaterName.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x000272C2 File Offset: 0x000254C2
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName = "";
			this.storeValue = 0f;
		}

		// Token: 0x040008DE RID: 2270
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008DF RID: 2271
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x040008E0 RID: 2272
		[Tooltip("The value of the float parameter")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040008E1 RID: 2273
		private Animator animator;

		// Token: 0x040008E2 RID: 2274
		private GameObject prevGameObject;
	}
}
