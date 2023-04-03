using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000294 RID: 660
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Sets the layer's current weight. Returns Success.")]
	public class SetLayerWeight : Action
	{
		// Token: 0x06000D64 RID: 3428 RVA: 0x00027DD0 File Offset: 0x00025FD0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00027E10 File Offset: 0x00026010
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			this.animator.SetLayerWeight(this.index.Value, this.weight.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x00027E4E File Offset: 0x0002604E
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.index = 0;
			this.weight = 0f;
		}

		// Token: 0x04000931 RID: 2353
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000932 RID: 2354
		[Tooltip("The layer's index")]
		public SharedInt index;

		// Token: 0x04000933 RID: 2355
		[Tooltip("The weight of the layer")]
		public SharedFloat weight;

		// Token: 0x04000934 RID: 2356
		private Animator animator;

		// Token: 0x04000935 RID: 2357
		private GameObject prevGameObject;
	}
}
